Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic
Imports ManagedSSL

Class ProtocolClientException
    Inherits Exception

    Sub New(ByVal ErrorMessage As String)
        MyBase.New(ErrorMessage)
    End Sub
End Class

Class ProtocolClient
    Private sock As Socket
    Private remote As EndPoint
    Private writer As StreamWriter
    Private reader As StreamReader
    Private server_name As String
    Private server_version As String
    Private encryptor As CryptoSession

    Sub New(ByVal ConnectInfo As ConnectOperationInfo)
        Dim nport As Integer

        If Not Integer.TryParse(ConnectInfo.Port, nport) Then
            Throw New ProtocolClientException("The port number you entered is incorrect.")
        End If

        ' create the beloved socket, to cherish and to hold, till finalization do us part
        sock = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        remote = sock.RemoteEndPoint

        Try
            ' try each address that was mapped from the host value
            sock.Connect(ConnectInfo.Host, nport)
        Catch ex As SocketException
            If ex.SocketErrorCode = SocketError.HostUnreachable Then
                Throw New ProtocolClientException("The remote host is unreachable.")
            ElseIf ex.SocketErrorCode = SocketError.ConnectionRefused Then
                Throw New ProtocolClientException("The remote host refused our connection.")
            ElseIf ex.SocketErrorCode = SocketError.HostNotFound Then
                Throw New ProtocolClientException("The specified host could not be found.")
            Else
                Throw New ProtocolClientException("An error occurred and the connection was not established.")
            End If
        End Try

        ' setup stream reader and writer to simplify IO operations
        Dim netStream = New NetworkStream(sock)
        writer = New StreamWriter(netStream)
        writer.AutoFlush = True
        reader = New StreamReader(netStream)

        ' attempt to perform HELLO negotiation
        Dim hello = New MinecontrolMessage("HELLO"), response As MinecontrolMessage
        hello.AddField("Name", CLIENT_PROGRAM_NAME)
        hello.AddField("Version", CLIENT_PROGRAM_VERSION)
        writer.Write(hello.Message)

        response = New MinecontrolMessage(reader)
        If Not response.IsCommand("GREETINGS") Then
            Throw New ProtocolClientException("The server didn't complete HELLO negotiation.")
        End If
        Try
            server_name = response.GetFieldValue("name")
            server_version = response.GetFieldValue("version")
            encryptor = New CryptoSession(response.GetFieldValue("encryptkey"))
        Catch ex As Exception
            Throw New ProtocolClientException("The server didn't greet us correctly.")
        End Try
    End Sub

    Sub Disconnect()
        writer.Close()
        reader.Close()
        sock.Shutdown(SocketShutdown.Both)
        sock.Close()
        sock = Nothing
    End Sub

    Function IssueGenericCommand(ByVal GenericInfo As GenericOperationInfo) As String
        Dim request = New MinecontrolMessage(GenericInfo.Command)
        Dim response As MinecontrolMessage

        If GenericInfo.Fields IsNot Nothing Then
            For Each kvp In GenericInfo.Fields
                request.AddField(kvp.Key, kvp.Value)
            Next
        End If

        writer.Write(request.Message)
        response = New MinecontrolMessage(reader)
        Return response_to_string(response)
    End Function
    
    ' Return the raw response object instead of the message string from the response
    Function IssueGenericCommand_Ex(ByVal GenericInfo As GenericOperationInfo) As MinecontrolMessage
        Dim request = New MinecontrolMessage(GenericInfo.Command)
        Dim response As MinecontrolMessage

        If GenericInfo.Fields IsNot Nothing Then
            For Each kvp In GenericInfo.Fields
                request.AddField(kvp.Key, kvp.Value)
            Next
        End If

        writer.Write(request.Message)
        response = New MinecontrolMessage(reader)
        Return response
    End Function
    
    ' Don't wait for an immediate response after sending the message
    Sub IssueGenericCommand_NoResponse(ByVal GenericInfo As GenericOperationInfo)
        Dim request = New MinecontrolMessage(GenericInfo.Command)

        If GenericInfo.Fields IsNot Nothing Then
            For Each kvp In GenericInfo.Fields
                request.AddField(kvp.Key, kvp.Value)
            Next
        End If
        
        writer.Write(request.Message)
    End Sub

    ' Waits for a message from the minecontrol server (doesn't send a message first)
    Function BlockForMessage() As MinecontrolMessage
        Return New MinecontrolMessage(reader)
    End Function

    Function CommandStatus() As String
        writer.Write(New MinecontrolMessage("STATUS"))
        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandLogin(ByVal LoginInfo As LoginOperationInfo) As String
        Dim request, response As MinecontrolMessage
        Dim enc_password As String = ""

        If Not encryptor.EncryptBuffer(LoginInfo.Password, enc_password) Then
            Throw New ProtocolClientException("Could not encrypt password using ManagedSSL module")
        End If
        request = New MinecontrolMessage("LOGIN")
        request.AddField("Username", LoginInfo.Username)
        request.AddField("Password", enc_password)
        writer.Write(request.Message)

        response = New MinecontrolMessage(reader)
        Return response_to_string(response)
    End Function

    Function CommandLogout() As String
        writer.Write(New MinecontrolMessage("LOGOUT"))
        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandStart(ByVal StartInfo As StartOperationInfo) As String
        Dim request As New MinecontrolMessage("START")
        request.AddField("ServerName", StartInfo.ServerName)
        request.AddField("IsNew", If(StartInfo.IsNew, "true", "false"))
        If StartInfo.ServerTime >= 0 Then
            request.AddField("ServerTime", StartInfo.ServerTime.ToString())
        End If
        If StartInfo.ServerProperties IsNot Nothing Then
            For Each kvp In StartInfo.ServerProperties
                request.AddField(kvp.Key, kvp.Value)
            Next
        End If
        writer.Write(request.Message)

        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandExtend(ByVal ExtendInfo As ExtendOperationInfo) As String
        Dim request = New MinecontrolMessage("EXTEND")
        request.AddField("ServerID", ExtendInfo.ServerID.ToString())
        request.AddField("Amount", ExtendInfo.ByHours.ToString())
        writer.Write(request.Message)

        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandExec(ByVal ExecInfo As ExecOperationInfo) As String
        Dim request = New MinecontrolMessage("EXEC")
        request.AddField("ServerID", ExecInfo.ServerID.ToString())
        request.AddField("Command", ExecInfo.CommandLine)
        writer.Write(request.Message)

        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandShutdown() As String
        Dim request = New MinecontrolMessage("SHUTDOWN")
        writer.Write(request.Message)

        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Function CommandStop(ByVal StopInfo As StopOperationInfo) As String
        Dim request = New MinecontrolMessage("STOP")
        request.AddField("ServerID", StopInfo.ServerID.ToString())
        If StopInfo.AuthPID > 0 Then
            request.AddField("AuthPID", StopInfo.AuthPID.ToString())
        End If
        writer.Write(request.Message)

        Return response_to_string(New MinecontrolMessage(reader))
    End Function

    Overrides Function ToString() As String
        Return remote.ToString()
    End Function

    Private Function response_to_string(ByVal response As MinecontrolMessage) As String
        Try
            If response.IsCommand("MESSAGE") Then
                Return response.GetFieldValue("Payload") & " [OKAY]"
            ElseIf response.IsCommand("LIST-MESSAGE") Then
                Dim res = ""
                Dim index = 0
                Do
                    Try
                        res += response.GetFieldValue("Item", index) & vbCrLf
                        index += 1
                    Catch ex As ArgumentOutOfRangeException
                        Exit Do
                    End Try
                Loop
                res += "[OKAY]"
                Return res
            ElseIf response.IsCommand("ERROR") Then
                Return response.GetFieldValue("Payload") & " [ERROR]"
            ElseIf response.IsCommand("LIST-ERROR") Then
                Dim res = ""
                Dim index = 0
                Do
                    Try
                        res += response.GetFieldValue("Item", index) & vbCrLf
                        index += 1
                    Catch
                        Exit Do
                    End Try
                Loop
                res += "[ERROR]"
                Return res
            End If
        Catch
            Return "Server gave bad message [BAD]"
        End Try

        Return "Server gave unrecognized message [BAD]"
    End Function
End Class
