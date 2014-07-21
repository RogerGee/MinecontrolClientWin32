Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic

Structure ConnectOperationInfo
    Dim Host As String
    Dim Port As String
End Structure

Structure LoginOperationInfo
    Dim Username As String
    Dim Password As String
End Structure

Structure StartOperationInfo
    Dim ServerName As String
    Dim IsNew As Boolean
    Dim ServerTime As Integer
    Dim ServerProperties As Collection(Of KeyValuePair(Of String, String))
End Structure

Structure ExtendOperationInfo
    Dim ServerID As Integer
    Dim ByHours As Integer
End Structure

Structure ExecOperationInfo
    Dim ServerID As Integer
    Dim CommandLine As String
End Structure

Structure StopOperationInfo
    Dim ServerID As Integer
    Dim AuthPID As Integer
End Structure

Structure GenericOperationInfo
    Dim Command As String
    Dim Fields As Collection(Of KeyValuePair(Of String, String))
End Structure
