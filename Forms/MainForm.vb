Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class MainForm
    Private Declare Function HideCaret Lib "User32.dll" (ByVal HWND As IntPtr) As Integer

    ' data
    Private console_mode As Boolean = False ' Are we in console mode?

    Sub New()
        ' This call is required by the pesky designer.
        InitializeComponent()

        ' Set up output box
        rboxOutput.Cursor = Windows.Forms.Cursors.Default
        AddHandler rboxOutput.MouseUp, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.MouseDown, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.GotFocus, AddressOf rboxOutput_HideCaret
        AddHandler rboxOutput.Enter, AddressOf rboxOutput_HideCaret
    End Sub

    Private Sub rboxOutput_HideCaret(sender As Object, e As EventArgs)
        ' Use the Native Windows API function HideCaret to hide the caret
        ' on the output RichTextBox
        HideCaret(rboxOutput.Handle)
    End Sub

    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click

    End Sub

    Private Sub btnDisconnect_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDisconnect.Click

    End Sub

    Private Sub boxCommand_Enter(ByVal sender As Object, ByVal e As KeyEventArgs) Handles boxCommand.KeyUp

    End Sub
End Class
