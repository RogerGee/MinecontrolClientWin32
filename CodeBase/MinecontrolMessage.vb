Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic

Class MinecontrolMessageFormatException
    Inherits Exception

    Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

Class MinecontrolMessage
    Private Const PROTOCOL_HEADER = "MINECONTROL-PROTOCOL"
    Private command As String
    Private fields As New Collection(Of KeyValuePair(Of String, String))

    ReadOnly Property Message As String
        Get
            Dim msg = PROTOCOL_HEADER
            msg += vbCrLf
            msg += command
            msg += vbCrLf

            ' place fields using format Field-Name: FieldValue <CRLF>
            For Each kvp In fields
                msg += kvp.Key
                msg += ": "
                msg += kvp.Value
                msg += vbCrLf
            Next

            msg += vbCrLf
            Return msg
        End Get
    End Property

    Property CommandName As String
        Get
            Return command
        End Get
        Set(value As String)
            command = value
        End Set
    End Property

    Sub New(Optional ByVal CommandName As String = "")
        command = CommandName
    End Sub
    Sub New(ByVal IStream As StreamReader)
        Dim line As String

        line = IStream.ReadLine()
        If line <> PROTOCOL_HEADER Then
            Throw New MinecontrolMessageFormatException("The server did not respond properly.")
        End If

        line = IStream.ReadLine()
        If line.Length = 0 Then
            Throw New MinecontrolMessageFormatException("The server did not send a correct message.")
        End If
        command = line

        ' read fields until empty line
        Dim separator = New Char() {":"c}
        Do
            line = IStream.ReadLine()
            If line.Length = 0 Then
                Exit Do
            End If

            Dim tokens = line.Split(separator)
            If tokens.Length <> 2 Then
                Throw New MinecontrolMessageFormatException("The server didn't send a correct message.")
            End If

            For i = 0 To 1
                tokens(i) = tokens(i).Trim()
            Next

            AddField(tokens(0).ToLower(), tokens(1))
        Loop
    End Sub

    Sub AddField(ByVal Name As String, ByVal Value As String)
        fields.Add(New KeyValuePair(Of String, String)(Name.ToLower(), Value))
    End Sub

    Function GetFieldValue(ByVal Name As String, Optional ByVal Position As Integer = 0) As String
        Dim i = 0

        Do While i < fields.Count
            If fields(i).Key = Name AndAlso Position = i Then
                Return fields(i).Value
            End If

            i += 1
        Loop

        Throw New ArgumentOutOfRangeException
    End Function

    Function IsCommand(ByVal CommandName As String) As Boolean
        Return command.ToLower() = CommandName.ToLower()
    End Function

    Overrides Function ToString() As String
        Return Me.Message
    End Function
End Class
