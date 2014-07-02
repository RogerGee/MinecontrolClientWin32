Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Module MinecontrolClient
    Public Const CLIENT_PROGRAM_NAME = "MinecontrolClientWin32"
    Public ReadOnly CLIENT_PROGRAM_VERSION As String = Application.ProductVersion

    Sub Main()
        Dim mainWindow As MainForm

        mainWindow = New MainForm
        mainWindow.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)

        Application.Run(mainWindow)
    End Sub

End Module
