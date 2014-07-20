Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic

Public Class MainForm
    Private Declare Function HideCaret Lib "User32.dll" (ByVal HWND As IntPtr) As Integer

    ' data
    Private console_mode As Boolean = False ' Are we in console mode?
    Private con As ProtocolClient

    Sub New()
        ' This call is required by the pesky designer.
        InitializeComponent()

        ' Set up output box
        rboxOutput.Cursor = Windows.Forms.Cursors.Default
        AddHandler rboxOutput.MouseUp, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.MouseDown, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.GotFocus, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.Enter, AddressOf rboxOutput_HideCaret

        ' Set up initial control state
        ConnectControlsState(False)
    End Sub

    Private Sub rboxOutput_HideCaret(sender As Object, e As EventArgs)
        ' Use the Native Windows API function HideCaret to hide the caret
        ' on the output RichTextBox
        HideCaret(rboxOutput.Handle)
    End Sub

    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
        If con Is Nothing Then
            Dim cont As Boolean

            ' check control fields
            If boxHost.TextLength = 0 Then
                MessageBox.Show("Enter a host name", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            ElseIf udPortSelect.Value <= 0 OrElse udPortSelect.Value > 65535 Then
                MessageBox.Show("The port number is invalid", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Do
                cont = False
                Try
                    con = New ProtocolClient(boxHost.Text, udPortSelect.Value.ToString())
                Catch ex As ProtocolClientException
                    If MessageBox.Show("Failed to connect: " & ex.Message, CLIENT_PROGRAM_NAME, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) _
                        = Windows.Forms.DialogResult.Retry Then
                        cont = True
                    Else
                        Return
                    End If
                End Try
            Loop While cont

            ' lock out controls while connection is established
            ConnectControlsState(True)
        Else
            MessageBox.Show("A connection is already established to " & con.ToString(), CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisconnect.Click
        con.Disconnect()
        ResetControls()
        con = Nothing
        ConnectControlsState(False)
        rboxOutput.Clear()
        boxCommand.Clear()
    End Sub

    Private Sub boxCommand_Enter(ByVal sender As Object, ByVal e As KeyEventArgs) Handles boxCommand.KeyUp
        If con IsNot Nothing AndAlso e.KeyCode = Keys.Enter Then
            Dim tokens = boxCommand.Text.Split()
            If tokens.Length = 0 Then
                Exit Sub
            End If

            Select Case tokens(0)
                Case "login"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: start login password" & vbCrLf)
                    Else
                        PrependOutput(con.CommandLogin(tokens(1), tokens(2)) & vbCrLf)
                    End If
                Case "logout"
                    PrependOutput(con.CommandLogout() & vbCrLf)
                Case "start"
                    If tokens.Length < 2 Then
                        PrependOutput("usage: start [create] world-name [[prop=key]...]" & vbCrLf)
                    Else
                        Dim i As Integer
                        Dim world As String
                        Dim isnew As Boolean
                        If tokens(1).ToLower() = "create" Then
                            If tokens.Length < 3 Then
                                PrependOutput("expected world-name after 'create'" & vbCrLf)
                                Return
                            End If
                            world = tokens(2)
                            i = 3
                            isnew = True
                        Else
                            world = tokens(1)
                            i = 2
                            isnew = False
                        End If

                        ' recreate the command line for the properties
                        Dim s As String = ""
                        While i < tokens.Length
                            s += tokens(i)
                            s += " "
                            i += 1
                        End While

                        Dim serverTime = -1
                        Dim props As New Collection(Of KeyValuePair(Of String, String))
                        tokens = s.Split("\".ToCharArray())
                        For Each tok In tokens
                            Dim parts = tok.Split("=".ToCharArray())
                            If parts.Length >= 2 Then
                                If parts(0).ToLower() = "servertime" Then
                                    Integer.TryParse(parts(1), serverTime)
                                Else
                                    props.Add(New KeyValuePair(Of String, String)(parts(0).Trim(), parts(1).Trim()))
                                End If
                            End If
                        Next

                        PrependOutput(con.CommandStart(world, isnew, serverTime, props) & vbCrLf)
                    End If
                Case "stop"
                    If tokens.Length < 2 Then
                        PrependOutput("usage: stop server-id" & vbCrLf)
                    Else
                        Dim id As Integer
                        If Not Integer.TryParse(tokens(1), id) Then
                            PrependOutput("ServerID must be an integer" & vbCrLf)
                        Else
                            PrependOutput(con.CommandStop(id) & vbCrLf)
                        End If
                    End If
                Case "exec"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: exec server-id command-line")
                    Else
                        Dim id As Integer
                        If Not Integer.TryParse(tokens(1), id) Then
                            PrependOutput("ServerID must be an integer" & vbCrLf)
                        Else
                            Dim cmdline As String = ""

                            ' prepare commandline from tokens
                            For i = 2 To tokens.Length - 1
                                cmdline += tokens(i)
                                cmdline += " "
                            Next

                            PrependOutput(con.CommandExec(id, cmdline) & vbCrLf)
                        End If
                    End If
                Case "extend"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: extend server-id hours" & vbCrLf)
                    Else
                        Dim hours As Integer
                        Dim id As Integer

                        If Not Integer.TryParse(tokens(1), id) Then
                            PrependOutput("server-id wasn't an integer" & vbCrLf)
                        ElseIf Not Integer.TryParse(tokens(2), hours) Then
                            PrependOutput("hours wasn't an integer" & vbCrLf)
                        Else
                            PrependOutput(con.CommandExtend(id, hours) & vbCrLf)
                        End If
                    End If
                Case "status"
                    PrependOutput(con.CommandStatus() & vbCrLf)
                Case "console"
                Case "shutdown"
                    PrependOutput(con.CommandShutdown() & vbCrLf)
                Case Else
                    Dim s As String = ""
                    Dim cmd = tokens(0)
                    Dim props As New Collection(Of KeyValuePair(Of String, String))

                    For i = 1 To tokens.Length - 1
                        s += tokens(1)
                        s += " "
                    Next

                    tokens = s.Split("\".ToCharArray())
                    For Each tok In tokens
                        Dim parts = tok.Split("=".ToCharArray())
                        If parts.Length >= 2 Then
                            props.Add(New KeyValuePair(Of String, String)(parts(0).Trim(), parts(1).Trim()))
                        End If
                    Next

                    PrependOutput(con.IssueGenericCommand(cmd, props) & vbCrLf)
            End Select

            boxCommand.Clear()
            boxCommand.Focus()
        End If
    End Sub

    Private Sub ConnectControlsState(ByVal locked As Boolean)
        boxHost.Enabled = Not locked
        udPortSelect.Enabled = Not locked
        btnConnect.Enabled = Not locked
        btnDisconnect.Enabled = locked
        lboxConfig.SelectedIndex = -1
        lboxConfig.Enabled = Not locked
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If con IsNot Nothing Then
            con.Disconnect()
        End If

        ' save the configuration
        Dim writer As New StreamWriter(CLIENT_PROGRAM_CONFIG_FILE)
        For Each item In lboxConfig.Items
            If TypeOf (item) Is MinecontrolClientConfig Then
                CType(item, MinecontrolClientConfig).Write(writer)
            End If
        Next
        writer.Close()
    End Sub

    ' Minecontrol server commands
    Private Sub LoginToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginToolStripMenuItem.Click
        If con IsNot Nothing Then
            ' check controls for credentials
            If boxUname.TextLength = 0 OrElse boxPassword.TextLength = 0 Then
                MessageBox.Show("Enter username and password credentials", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            Dim result = con.CommandLogin(boxUname.Text, boxPassword.Text)
            PrependOutput(result & vbCrLf)

            boxUname.Clear()
            boxPassword.Clear()
        End If
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LogoutToolStripMenuItem.Click
        If con IsNot Nothing Then
            PrependOutput(con.CommandLogout() & vbCrLf)
        End If
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StartToolStripMenuItem.Click
        If con IsNot Nothing Then
            Dim dialog As New FieldInputDialog("Start Server")

            dialog.AddFieldInput("World Name")
            dialog.AddFieldInput("[Create New]", "false")
            dialog.AddFieldInput("[Server Time]")
            dialog.AddFieldMultilineInput("[Server Properties]")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count < 4 Then
                    Stop
                End If

                Dim time As Integer = -1
                If dialog.InputFields(2).Length > 0 Then
                    If Not Integer.TryParse(dialog.InputFields(2), time) Then
                        MessageBox.Show("Could not convert Server Time to an integer number of hours", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End If

                Dim props As Collection(Of KeyValuePair(Of String, String)) = Nothing
                If dialog.InputFields(3).Length > 0 Then
                    props = New Collection(Of KeyValuePair(Of String, String))
                    For i = 3 To dialog.InputFields.Count - 1
                        Dim iter = 0
                        Dim key, value As String

                        key = ""
                        While iter < dialog.InputFields(i).Length AndAlso dialog.InputFields(i)(iter) <> "="
                            key += dialog.InputFields(i)(iter)
                            iter += 1
                        End While
                        If iter >= dialog.InputFields(i).Length Then
                            Continue For
                        End If

                        iter += 1
                        value = ""
                        While iter < dialog.InputFields(i).Length
                            value += dialog.InputFields(i)(iter)
                            iter += 1
                        End While

                        key = key.Trim()
                        value = value.Trim()

                        props.Add(New KeyValuePair(Of String, String)(key, value))
                    Next
                End If

                PrependOutput(con.CommandStart(dialog.InputFields(0), dialog.InputFields(1).ToLower() = "true", time, props) & vbCrLf)
            End If
        End If
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StopToolStripMenuItem.Click
        If con IsNot Nothing Then
            Dim dialog As New FieldInputDialog("Stop Server")

            dialog.AddFieldInput("Server ID")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count = 0 Then
                    Stop
                End If

                Dim id As Integer
                If Not Integer.TryParse(dialog.InputFields(0), id) Then
                    MessageBox.Show("Could not convert ID value to an integer", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                PrependOutput(con.CommandStop(id) & vbCrLf)
            End If
        End If
    End Sub

    Private Sub ExtendToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExtendToolStripMenuItem.Click

    End Sub

    Private Sub ExecMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExecMenuItem.Click

    End Sub

    Private Sub StatusToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusToolStripMenuItem.Click
        If con IsNot Nothing Then
            PrependOutput(con.CommandStatus() & vbCrLf)
        End If
    End Sub

    Private Sub ConsoleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ConsoleToolStripMenuItem.Click

    End Sub

    Private Sub ShutdownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShutdownToolStripMenuItem.Click
        If con IsNot Nothing Then
            PrependOutput(con.CommandShutdown() & vbCrLf)
        End If
    End Sub

    Private Sub ResetControls()
        boxHost.Clear()
        udPortSelect.Value = 44446
        boxUname.Clear()
        boxPassword.Clear()
    End Sub

    Private Sub PrependOutput(ByVal output As String)
        rboxOutput.SelectionStart = 0
        rboxOutput.SelectionLength = 0
        rboxOutput.SelectedText = output
    End Sub

    Private Sub tsSaveProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsSaveProfile.Click
        If boxHost.TextLength = 0 Then
            MessageBox.Show("Host name must be specified", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If udPortSelect.Value <= 0 OrElse udPortSelect.Value > 65535 Then
            MessageBox.Show("The port number is invalid", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If boxUname.TextLength = 0 Then
            MessageBox.Show("User name must be specified", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If boxPassword.TextLength = 0 Then
            MessageBox.Show("Password must be specified", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim title = InputBox("Enter name for the profile", "Enter profile name", "profile")
        If title.Length > 0 Then
            Dim entry As New MinecontrolClientConfig(title, boxHost.Text, udPortSelect.Value, boxUname.Text, boxPassword.Text)
            lboxConfig.Items.Add(entry)
        End If
    End Sub

    Private Sub SaveServerOutputAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveServerOutputAsToolStripMenuItem.Click

    End Sub

    Private Sub lboxConfig_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles lboxConfig.KeyUp
        If con Is Nothing AndAlso lboxConfig.SelectedIndex >= 0 AndAlso e.KeyCode = Keys.Delete Then
            lboxConfig.Items.Remove(lboxConfig.SelectedItem)
        End If
    End Sub

    Private Sub lboxConfig_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lboxConfig.SelectedIndexChanged
        If con Is Nothing Then
            If lboxConfig.SelectedIndex >= 0 Then
                If TypeOf (lboxConfig.SelectedItem) Is MinecontrolClientConfig Then
                    Dim entry As MinecontrolClientConfig
                    entry = CType(lboxConfig.SelectedItem, MinecontrolClientConfig)

                    boxHost.Text = entry.HostName
                    If entry.PortNo >= udPortSelect.Minimum AndAlso entry.PortNo <= udPortSelect.Maximum Then
                        udPortSelect.Value = entry.PortNo
                    Else
                        udPortSelect.Value = 44446
                    End If
                    boxUname.Text = entry.UserName
                    boxPassword.Text = entry.Password
                End If
            Else
                ResetControls()
            End If
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
