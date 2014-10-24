Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Threading
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic

Class ConsoleModeException
    Inherits Exception
    
    Sub New(ByVal ErrorMessage As String)
        MyBase.New(ErrorMessage)
    End Sub
End Class

Delegate Sub ConsoleMessageReceivedCallback(ByVal MessageString As String)

' we'll use this class to implement the console functionality of the Minecontrol Protocol; it
' provides an interface to a Minecraft server's console
Class MinecraftConsole
    Event ConsoleMessageReceived(ByVal message As String)
    Event ConsoleModeTerminated()
    
    Private con As ProtocolClient
    Private pollthrd As Thread
    Private msgthrd As Thread
    Private checkpoll As Boolean = False
    Private pollobject As Object = Nothing
    Private doterm As Boolean = False

    Sub New(ByVal Connection As ProtocolClient, ByVal ServerID As Integer)
        If Connection Is Nothing Then
            Throw New ArgumentNullException("Connection")
        End If

        con = Connection

        ' issue the CONSOLE command to begin console negotiation
        Dim cmdinfo As GenericOperationInfo
        Dim res As MinecontrolMessage
        cmdinfo.Command = "CONSOLE"
        cmdinfo.Fields = New Collection(Of KeyValuePair(Of String, String))
        cmdinfo.Fields.Add(New KeyValuePair(Of String, String)("ServerID", ServerID.ToString()))
        res = con.IssueGenericCommand_Ex(cmdinfo)

        ' check to see if the server sent back the correct message
        If res.CommandName <> "CONSOLE-MESSAGE" Then
            If res.CommandName = "ERROR" Then
                Try
                    Throw New ConsoleModeException("The server sent back an error: " & res.GetFieldValue("Payload"))
                Catch ex As ArgumentOutOfRangeException

                End Try
            ElseIf res.CommandName = "MESSAGE" Then
                Try
                    Throw New ConsoleModeException("The server says: " & res.GetFieldValue("Payload"))
                Catch ex As ArgumentOutOfRangeException

                End Try
            End If

            Throw New ConsoleModeException("The server did not respond correctly.")
        End If
        Try
            If res.GetFieldValue("Status").ToLower() <> "established" Then
                Throw New ConsoleModeException("The server refused console mode. (status=" & res.GetFieldValue("Status") & ")")
            End If
        Catch ex As ArgumentOutOfRangeException
            Throw New ConsoleModeException("The server did not respond correctly.")
        End Try

        msgthrd = New Thread(New ThreadStart(AddressOf MessageThread))
        pollthrd = New Thread(New ThreadStart(AddressOf PollThread))
        msgthrd.Start()
        pollthrd.Start()
    End Sub
    
    Sub EnterCommand(ByVal Command As String)
        pollobject = Command
        checkpoll = True
    End Sub
    
    Sub CloseConsoleMode()
        pollobject = "quit"
        checkpoll = True

        ' block until quit message received
        While Not doterm
            Thread.Sleep(250)
        End While
    End Sub
    
    Private Sub PollThread()
        Do
            If doterm Then
                checkpoll = False
                pollobject = Nothing
                RaiseEvent ConsoleModeTerminated()
                Exit Do
            End If

            If checkpoll Then
                If TypeOf pollobject Is String Then
                    Dim msg As New GenericOperationInfo

                    ' if we are quitting console mode then the user sent the message 'quit'
                    If CType(pollobject, String).ToLower() = "quit" Then
                        ' request console mode end
                        msg.Command = "CONSOLE-QUIT"
                    Else
                        ' issue a console command
                        msg.Fields = New Collection(Of KeyValuePair(Of String, String))
                        msg.Command = "CONSOLE-COMMAND"
                        msg.Fields.Add(New KeyValuePair(Of String, String)("ServerCommand", CType(pollobject, String)))
                    End If

                    con.IssueGenericCommand_NoResponse(msg)
                End If

                checkpoll = False
            End If

            Thread.Sleep(250)
        Loop
    End Sub
    
    Private Sub MessageThread()
        ' listen for CONSOLE-MESSAGEs; if we receive status=shutdown
        ' then we quit this thread
        Do
            Dim msg = con.BlockForMessage()

            If Not msg.IsCommand("CONSOLE-MESSAGE") Then
                ' server didn't respond correctly
                doterm = True
                Exit Do
            End If

            Try
                If msg.GetFieldValue("Status").ToLower() = "message" Then
                    RaiseEvent ConsoleMessageReceived(msg.GetFieldValue("Payload"))
                Else
                    doterm = True
                    Exit Do
                End If
            Catch
                ' server didn't respond correctly
                doterm = True
                Exit Do
            End Try
        Loop
    End Sub
End Class
