Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Module MinecontrolClient

    Sub Main()
        Dim mainWindow As MainForm

        mainWindow = New MainForm
        mainWindow.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)

        Application.Run(mainWindow)
    End Sub

End Module
