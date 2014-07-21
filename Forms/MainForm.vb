Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic

Public Class MainForm
    Private Declare Function HideCaret Lib "User32.dll" (ByVal HWND As IntPtr) As Integer
    Private Delegate Sub NetworkOperationCallback(ByVal result As String)

    ' data members
    Private console_mode As Boolean = False ' Are we in console mode?
    Private con As ProtocolClient = Nothing ' This is the connection to the minecontrol server.
    Private op As Thread = Nothing ' This refers to the current asynchronous operation.

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

    Private Sub generic_Async(ByVal param As Object)
        Dim info As GenericOperationInfo
        info = CType(param, GenericOperationInfo)

        generic_Result(con.IssueGenericCommand(info))
    End Sub
    Private Sub generic_Result(ByVal result As String)
        If IsDisposed Then Exit Sub
        If InvokeRequired Then
            Invoke(New NetworkOperationCallback(AddressOf generic_Result), New Object() {result})
            Exit Sub
        End If

        PrependOutput(result & vbCrLf)
        op = Nothing
    End Sub

    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
        If con Is Nothing Then
            If op IsNot Nothing Then Return

            ' check control fields
            If boxHost.TextLength = 0 Then
                MessageBox.Show("Enter a host name", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            ElseIf udPortSelect.Value <= 0 OrElse udPortSelect.Value > 65535 Then
                MessageBox.Show("The port number is invalid", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' display a message indicating the connect process
            PrependOutput("Connecting to " & boxHost.Text & "..." & vbCrLf)

            ' perform the connect operation on another thread
            Dim params As ConnectOperationInfo
            params.Host = boxHost.Text
            params.Port = udPortSelect.Value.ToString()
            op = New Thread(New ParameterizedThreadStart(AddressOf Me.connect_Async))
            op.Start(params)
        Else
            MessageBox.Show("A connection is already established to " & con.ToString(), CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
    Private Sub connect_Async(ByVal param As Object)
        Dim info As ConnectOperationInfo
        info = CType(param, ConnectOperationInfo)

        Try
            con = New ProtocolClient(info)
        Catch ex As ProtocolClientException
            connect_Failure("Failed to connect to " & info.Host & ": " & ex.Message)
            Exit Sub
        End Try

        connect_Success("Successful connection established to " & info.Host)
    End Sub
    Private Sub connect_Success(ByVal result As String)
        If IsDisposed Then Exit Sub
        If InvokeRequired Then
            Invoke(New NetworkOperationCallback(AddressOf connect_Success), New Object() {result})
            Exit Sub
        End If

        ' display result
        PrependOutput(result & vbCrLf)

        ' lock out controls while connection is established
        ConnectControlsState(True)

        op = Nothing
    End Sub
    Private Sub connect_Failure(ByVal result As String)
        If IsDisposed Then Exit Sub
        If InvokeRequired Then
            Invoke(New NetworkOperationCallback(AddressOf connect_Failure), New Object() {result})
            Exit Sub
        End If

        PrependOutput("connect failed" & vbCrLf)

        ' display error message and prompt for retry
        op = Nothing
        If MessageBox.Show(result, CLIENT_PROGRAM_NAME, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) = Windows.Forms.DialogResult.Retry Then
            btnConnect.PerformClick()
        End If
    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisconnect.Click
        con.Disconnect()
        ResetControls()
        con = Nothing
        ConnectControlsState(False)
        boxCommand.Clear()
        PrependOutput("Connection shutdown" & vbCrLf)
    End Sub

    Private Sub boxCommand_Enter(ByVal sender As Object, ByVal e As KeyEventArgs) Handles boxCommand.KeyUp
        If con IsNot Nothing AndAlso op Is Nothing AndAlso e.KeyCode = Keys.Enter Then
            Dim tokens = boxCommand.Text.Split()
            If tokens.Length = 0 Then
                Exit Sub
            End If

            Dim tstart As ParameterizedThreadStart = Nothing
            Dim info As Object = Nothing

            Select Case tokens(0)
                Case "login"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: start login password" & vbCrLf)
                    Else
                        Dim param As LoginOperationInfo
                        param.Username = tokens(1)
                        param.Password = tokens(2)

                        tstart = New ParameterizedThreadStart(AddressOf login_Async)
                        info = param
                    End If
                Case "logout"
                    tstart = New ParameterizedThreadStart(AddressOf logout_Async)
                    info = Nothing
                Case "start"
                    If tokens.Length < 2 Then
                        PrependOutput("usage: start [create] world-name [[prop=key]...]" & vbCrLf)
                    Else
                        Dim params As StartOperationInfo
                        Dim i As Integer
                        If tokens(1).ToLower() = "create" Then
                            If tokens.Length < 3 Then
                                PrependOutput("expected world-name after 'create'" & vbCrLf)
                                Return
                            End If
                            params.ServerName = tokens(2)
                            i = 3
                            params.IsNew = True
                        Else
                            params.ServerName = tokens(1)
                            i = 2
                            params.IsNew = False
                        End If

                        ' recreate the command line for the properties
                        Dim s As String = ""
                        While i < tokens.Length
                            s += tokens(i)
                            s += " "
                            i += 1
                        End While

                        params.ServerTime = -1 ' don't set time unless user specifies it
                        params.ServerProperties = New Collection(Of KeyValuePair(Of String, String))
                        tokens = s.Split("\".ToCharArray())
                        For Each tok In tokens
                            Dim parts = tok.Split("=".ToCharArray())
                            If parts.Length >= 2 Then
                                If parts(0).ToLower() = "servertime" Then
                                    Integer.TryParse(parts(1), params.ServerTime)
                                Else
                                    params.ServerProperties.Add(New KeyValuePair(Of String, String)(parts(0).Trim(), parts(1).Trim()))
                                End If
                            End If
                        Next

                        tstart = New ParameterizedThreadStart(AddressOf start_Async)
                        info = params
                    End If
                Case "stop"
                    If tokens.Length < 2 Then
                        PrependOutput("usage: stop server-id [authority-process-ID]" & vbCrLf)
                    Else
                        Dim params As StopOperationInfo
                        params.AuthPID = -1
                        If Not Integer.TryParse(tokens(1), params.ServerID) Then
                            PrependOutput("ServerID must be an integer" & vbCrLf)
                        ElseIf tokens.Length >= 3 AndAlso Not Integer.TryParse(tokens(2), params.AuthPID) Then
                            PrependOutput("AuthPID must be an integer" & vbCrLf)
                        Else
                            tstart = New ParameterizedThreadStart(AddressOf stop_Async)
                            info = params
                        End If
                    End If
                Case "exec"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: exec server-id command-line")
                    Else
                        Dim params As ExecOperationInfo
                        If Not Integer.TryParse(tokens(1), params.ServerID) Then
                            PrependOutput("ServerID must be an integer" & vbCrLf)
                        Else
                            ' prepare commandline from tokens
                            params.CommandLine = String.Empty
                            For i = 2 To tokens.Length - 1
                                params.CommandLine += tokens(i)
                                params.CommandLine += " "
                            Next

                            tstart = New ParameterizedThreadStart(AddressOf exec_Async)
                            info = params
                        End If
                    End If
                Case "extend"
                    If tokens.Length < 3 Then
                        PrependOutput("usage: extend server-id amount" & vbCrLf)
                    Else
                        Dim params As ExtendOperationInfo

                        If Not Integer.TryParse(tokens(1), params.ServerID) Then
                            PrependOutput("server-id wasn't an integer" & vbCrLf)
                        ElseIf Not Integer.TryParse(tokens(2), params.ByHours) Then
                            PrependOutput("amount wasn't an integer" & vbCrLf)
                        Else
                            tstart = New ParameterizedThreadStart(AddressOf extend_Async)
                            info = params
                        End If
                    End If
                Case "status"
                    tstart = New ParameterizedThreadStart(AddressOf status_Async)
                    info = Nothing
                Case "console"
                Case "shutdown"
                    tstart = New ParameterizedThreadStart(AddressOf shutdown_Async)
                    info = Nothing
                Case Else
                    Dim s As String = ""
                    Dim params As GenericOperationInfo
                    params.Command = tokens(0)
                    params.Fields = New Collection(Of KeyValuePair(Of String, String))

                    For i = 1 To tokens.Length - 1
                        s += tokens(1)
                        s += " "
                    Next

                    tokens = s.Split("\".ToCharArray())
                    For Each tok In tokens
                        Dim parts = tok.Split("=".ToCharArray())
                        If parts.Length >= 2 Then
                            params.Fields.Add(New KeyValuePair(Of String, String)(parts(0).Trim(), parts(1).Trim()))
                        End If
                    Next

                    tstart = New ParameterizedThreadStart(AddressOf generic_Async)
                    info = params
            End Select

            If tstart IsNot Nothing Then
                op = New Thread(tstart)
                op.Start(info)
            End If

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
        If con IsNot Nothing AndAlso op Is Nothing Then
            ' check controls for credentials
            If boxUname.TextLength = 0 OrElse boxPassword.TextLength = 0 Then
                MessageBox.Show("Enter username and password credentials", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If

            Dim param As LoginOperationInfo
            param.Username = boxUname.Text
            param.Password = boxPassword.Text

            op = New Thread(New ParameterizedThreadStart(AddressOf login_Async))
            op.Start(param)
        End If
    End Sub
    Private Sub login_Async(ByVal param As Object)
        Dim info As LoginOperationInfo
        info = CType(param, LoginOperationInfo)

        login_Result(con.CommandLogin(info))
    End Sub
    Private Sub login_Result(ByVal result As String)
        If IsDisposed Then Exit Sub
        If InvokeRequired Then
            Invoke(New NetworkOperationCallback(AddressOf login_Result), New Object() {result})
            Exit Sub
        End If

        PrependOutput(result & vbCrLf)

        boxUname.Clear()
        boxPassword.Clear()
        op = Nothing
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LogoutToolStripMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            op = New Thread(New ParameterizedThreadStart(AddressOf logout_Async))
            op.Start(Nothing)
        End If
    End Sub
    Private Sub logout_Async(ByVal param As Object)
        generic_Result(con.CommandLogout() & vbCrLf)
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StartToolStripMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            Dim dialog As New FieldInputDialog("Start Server")

            dialog.AddFieldInput("World Name")
            dialog.AddFieldInput("[Create New]", "false")
            dialog.AddFieldInput("[Server Time]")
            dialog.AddFieldMultilineInput("[Server Properties]")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count < 4 Then
                    ' assertion
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

                Dim params As StartOperationInfo
                params.ServerName = dialog.InputFields(0)
                params.IsNew = dialog.InputFields(1).ToLower() = "true"
                params.ServerTime = time
                params.ServerProperties = props

                op = New Thread(New ParameterizedThreadStart(AddressOf start_Async))
                op.Start(params)
            End If
        End If
    End Sub
    Private Sub start_Async(ByVal param As Object)
        Dim info As StartOperationInfo
        info = CType(param, StartOperationInfo)

        generic_Result(con.CommandStart(info))
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StopToolStripMenuItem.Click
        If con IsNot Nothing Then
            Dim dialog As New FieldInputDialog("Stop Server")

            dialog.AddFieldInput("Server ID")
            dialog.AddFieldInput("Authority Process ID")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count < 2 Then
                    ' assertion
                    Stop
                End If

                Dim id As Integer
                Dim pid As Integer = -1
                If Not Integer.TryParse(dialog.InputFields(0), id) OrElse (dialog.InputFields(1).Length > 0 AndAlso _
                                                                           Not Integer.TryParse(dialog.InputFields(1), pid)) Then
                    MessageBox.Show("Could not convert ID value to an integer", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                Dim params As StopOperationInfo
                params.ServerID = id
                params.AuthPID = pid

                op = New Thread(New ParameterizedThreadStart(AddressOf stop_Async))
                op.Start(params)
            End If
        End If
    End Sub
    Private Sub stop_Async(ByVal param As Object)
        Dim info As StopOperationInfo
        info = CType(param, StopOperationInfo)

        generic_Result(con.CommandStop(info))
    End Sub

    Private Sub ExtendToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExtendToolStripMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            Dim dialog As New FieldInputDialog("Extend Server Time")
            dialog.AddFieldInput("ServerID")
            dialog.AddFieldInput("Hours to Extend")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count < 2 Then
                    ' assertion
                    Stop
                End If

                Dim params As ExtendOperationInfo

                If Not Integer.TryParse(dialog.InputFields(0), params.ServerID) Then
                    MessageBox.Show("Server ID must be an integer", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                If Not Integer.TryParse(dialog.InputFields(1), params.ByHours) Then
                    MessageBox.Show("Extend amount must be an integer", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If

                op = New Thread(New ParameterizedThreadStart(AddressOf extend_Async))
                op.Start(params)
            End If
        End If
    End Sub
    Private Sub extend_Async(ByVal param As Object)
        Dim info As ExtendOperationInfo
        info = CType(param, ExtendOperationInfo)

        generic_Result(con.CommandExtend(info))
    End Sub

    Private Sub ExecMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExecMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            Dim dialog As New FieldInputDialog("Execute Authority Program")
            dialog.AddFieldInput("ServerID")
            dialog.AddFieldInput("Command Line")

            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If dialog.InputFields.Count < 2 Then
                    ' assertion
                    Stop
                End If

                Dim params As ExecOperationInfo

                If Not Integer.TryParse(dialog.InputFields(0), params.ServerID) Then
                    MessageBox.Show("Server ID must be an integer", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                params.CommandLine = dialog.InputFields(1)

                op = New Thread(New ParameterizedThreadStart(AddressOf exec_Async))
                op.Start(params)
            End If
        End If
    End Sub
    Private Sub exec_Async(ByVal param As Object)
        Dim info As ExecOperationInfo
        info = CType(param, ExecOperationInfo)

        generic_Result(con.CommandExec(info))
    End Sub

    Private Sub StatusToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusToolStripMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            op = New Thread(New ParameterizedThreadStart(AddressOf status_Async))
            op.Start(Nothing)
        End If
    End Sub
    Private Sub status_Async(ByVal param As Object)
        generic_Result(con.CommandStatus())
    End Sub


    Private Sub ConsoleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ConsoleToolStripMenuItem.Click

    End Sub

    Private Sub ShutdownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ShutdownToolStripMenuItem.Click
        If con IsNot Nothing AndAlso op Is Nothing Then
            op = New Thread(New ParameterizedThreadStart(AddressOf shutdown_Async))
            op.Start(Nothing)
        End If
    End Sub
    Private Sub shutdown_Async(ByVal param As Object)
        generic_Result(con.CommandShutdown())
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
