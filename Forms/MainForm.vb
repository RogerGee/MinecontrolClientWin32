Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports Microsoft.VisualBasic

Public Class MainForm
    Private Declare Function HideCaret Lib "User32.dll" (ByVal HWND As IntPtr) As Integer

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
End Class