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

' we'll use this class to implement the console functionality of the Minecontrol Protocol; it
' provides an interface to a Minecraft server's console
Class MinecraftConsole
	Delegate Sub ConsoleMessageReceivedCallback(ByVal MessageString As String)
	
	Private con As ProtocolClient
	Private pollthread As Thread
	Private msgthread As Thread
	Private callback As ConsoleMessageReceivedCallback
	Private checkpoll As Boolean = False
	Private pollobject As Object = Nothing

	Sub New(ByVal Connection As ProtocolClient,ByVal ServerID As Integer,ByVal MessageCallback As ConsoleMessageReceivedCallback)
		If Connection Is Nothing Then
			Throw New ArgumentNullException("Connection")
		End If
		
		con = Connection
		callback = MessageCallback
		
		' issue the CONSOLE command to begin console negotiation
		Dim cmdinfo As GenericCommand
		Dim res As MinecontrolMessage
		cmdinfo.Command = "CONSOLE"
		cmdinfo.Fields = New Collection(Of KeyValuePair(Of String, String))
		cmdinfo.Fields.Add(New KeyValuePair(Of String, String)("ServerID",ServerID.ToString()))
		res = con.IssueGenericCommand_Ex(cmdinfo)
		
		' check to see if the server sent back the correct message
		If res.CommandName <> "CONSOLE-MESSAGE" Then
			Throw New ConsoleModeException("The server did not respond correctly.")
		End If
		Try
			If res.GetFieldValue("Status").ToLower() <> "established" Then
				Throw New ConsoleModeException("The server refused console mode.")
			End If
		Catch ex As ArgumentOutOfRangeException
			Throw New ConsoleModeException("The server did not respond correctly.")
		End Try
		
		msgthread = New Thread(New ThreadStart(AddressOf MessageThread))
		pollthread = New Thread(New ThreadStart(AddressOf PollThread))
		msgthread.Start()
		pollthread.Start()
	End Sub
	
	Sub EnterCommand(ByVal Command As String)
		pollobject = Command
		checkpoll = True
	End Sub
	
	Sub CloseConsoleMode()
		pollobject = False
		checkpoll = True
	End Sub
	
	Private Sub PollThread()
		Do
			If checkpoll Then
				If Typeof pollobject Is Boolean Then
					If Not pollobject Then
						' we're all done
						checkpoll = False
						Exit Do
					End If
				ElseIf Typeof pollobject Is String Then
					' issue a console command
					
				End If
				
				checkpoll = False
			End If
		Loop
	End Sub
	
	Private Sub MessageThread()
		' listen for CONSOLE-MESSAGEs; if we receive status=quit
		' then we quit this thread
		Do
			
		Loop
	End Sub
	
End Class
