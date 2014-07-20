Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Module MinecontrolClient
    Public Const CLIENT_PROGRAM_NAME = "MinecontrolClientWin32"
    Public ReadOnly CLIENT_PROGRAM_VERSION As String = Application.ProductVersion
    Public ReadOnly CLIENT_PROGRAM_CONFIG_DIR As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\" & CLIENT_PROGRAM_NAME
    Public ReadOnly CLIENT_PROGRAM_CONFIG_FILE As String = CLIENT_PROGRAM_CONFIG_DIR & "\config.ini"

    Sub Main()
        Dim mainWindow As MainForm

        ' create the main form
        mainWindow = New MainForm
        mainWindow.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)

        ' create the app data directory if it doesn't exist
        If Not Directory.Exists(CLIENT_PROGRAM_CONFIG_DIR) Then
            Try
                Directory.CreateDirectory(CLIENT_PROGRAM_CONFIG_DIR)
            Catch
                MessageBox.Show("An exception occured and the configuration data folder couldn't be created.", CLIENT_PROGRAM_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

        ' load up configuration if the config file exists
        If File.Exists(CLIENT_PROGRAM_CONFIG_FILE) Then
            Dim reader As New StreamReader(CLIENT_PROGRAM_CONFIG_FILE)

            Do
                Dim entry As New MinecontrolClientConfig
                If Not entry.Read(reader) Then Exit Do

                mainWindow.lboxConfig.Items.Add(entry)
            Loop

            reader.Close()
        End If

        Application.Run(mainWindow)
    End Sub

End Module

Class MinecontrolClientConfig
    Private title As String
    Private host As String
    Private port As Decimal
    Private user As String
    Private pass As String

    Public Property HostName() As String
        Get
            Return host
        End Get
        Set(ByVal value As String)
            host = value
        End Set
    End Property

    Public Property PortNo() As Decimal
        Get
            Return port
        End Get
        Set(ByVal value As Decimal)
            port = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return user
        End Get
        Set(ByVal value As String)
            user = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return pass
        End Get
        Set(ByVal value As String)
            pass = value
        End Set
    End Property
    Sub New()
        ' allow default construction
    End Sub
    Sub New(ByVal TitleName As String, ByVal HostName As String, ByVal PortNo As Decimal, ByVal UserName As String, ByVal Password As String)
        title = TitleName
        host = HostName
        port = PortNo
        user = UserName
        pass = Password
    End Sub

    Function Read(ByVal reader As StreamReader) As Boolean
        Const LF As Integer = 10
        Dim c As Integer

        Read = False
        title = ""
        Do
            c = SeekWhitespace(reader)
            If c = -1 Then Exit Function
            If ChrW(c) = "{" Then Exit Do
            title += ChrW(c)
        Loop

        Do
            Dim found As Boolean
            Dim key, value As String

            c = SeekWhitespace(reader)
            If c = -1 Then
                Exit Function
            End If
            If ChrW(c) = "}" Then
                Read = True
                Exit Do
            End If

            found = False
            key = ChrW(c)
            value = ""

            Do
                c = reader.Read()
                If c = -1 Then
                    Exit Function
                End If
                If ChrW(c) = "}" Then
                    Read = True
                    Exit Do
                End If
                If c = LF Then
                    Exit Do
                End If
                If Char.IsWhiteSpace(ChrW(c)) Then
                    Continue Do
                End If

                If ChrW(c) = ":" Then
                    found = True
                    Continue Do
                End If

                If Not found Then
                    key += ChrW(c)
                Else
                    value += ChrW(c)
                End If
            Loop

            key = key.Trim().ToLower()
            value = value.Trim()

            Select Case key
                Case "host"
                    host = value
                Case "port"
                    If Not Decimal.TryParse(value, port) Then
                        port = 44446
                    End If
                Case "user"
                    user = value
                Case "password"
                    pass = value
            End Select
        Loop While Not Read
    End Function

    Sub Write(ByVal writer As StreamWriter)
        writer.Write(title & "{" & vbCrLf)
        writer.WriteLine("Host: " & host)
        writer.WriteLine("Port: " & port.ToString())
        writer.WriteLine("User: " & user)
        writer.WriteLine("Password: " & pass)
        writer.Write("}" & vbCrLf)
    End Sub

    Overrides Function ToString() As String
        Return title
    End Function

    Protected Function SeekWhitespace(ByVal stream As StreamReader) As Integer
        Dim c As Integer

        Do
            c = stream.Read()
            If c = -1 Then
                Exit Do
            End If
        Loop While Char.IsWhiteSpace(ChrW(c))

        Return c
    End Function
End Class
