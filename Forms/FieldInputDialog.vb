Option Infer On
Option Explicit On
Option Strict On

Imports System
Imports System.Drawing
Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Class FieldInputDialog
    Private fields As Collection(Of String)
    Private starty As Integer = 0

    ReadOnly Property InputFields() As Collection(Of String)
        Get
            Return fields
        End Get
    End Property

    Sub New(ByVal Title As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = Title
        fields = New Collection(Of String)
    End Sub

    Sub AddFieldInput(ByVal Title As String, Optional ByVal DefaultValue As String = "")
        Dim boxInput As New TextBox
        Dim lblInput As New Label
        Dim sz As SizeF

        lblInput.Text = Title & ": "
        lblInput.Location = New Point(5, starty)
        sz = lblInput.CreateGraphics().MeasureString(lblInput.Text, Me.Font)
        sz += New SizeF(5, 5)
        lblInput.ClientSize = New Size(CInt(sz.Width), CInt(sz.Height))

        boxInput.Location = New Point(lblInput.Location.X + lblInput.Width, starty)
        boxInput.Width = Me.ClientSize.Width - CInt(sz.Width) - 10
        boxInput.Text = DefaultValue

        starty += boxInput.Height + 10
        Me.Controls.Add(lblInput)
        Me.Controls.Add(boxInput)
    End Sub

    Sub AddFieldMultilineInput(ByVal Title As String, Optional ByVal DefaultValue As String = "")
        Dim boxInput As New TextBox
        Dim lblInput As New Label
        Dim sz As SizeF

        lblInput.Text = Title & ": "
        lblInput.Location = New Point(5, starty)
        sz = lblInput.CreateGraphics().MeasureString(lblInput.Text, Me.Font)
        sz += New SizeF(5, 5)
        lblInput.ClientSize = New Size(CInt(sz.Width), CInt(sz.Height))
        starty += lblInput.Height

        boxInput.Multiline = True
        boxInput.Location = New Point(5, starty)
        boxInput.Size = New Size(Me.ClientSize.Width - 10, CInt(sz.Height * 4.0!))
        boxInput.ScrollBars = ScrollBars.Vertical
        boxInput.Text = DefaultValue

        starty += boxInput.Height + 10
        Me.Controls.Add(lblInput)
        Me.Controls.Add(boxInput)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' copy values to fields
        For Each Control As Control In Me.Controls
            If TypeOf Control Is TextBox Then
                Dim lines = Control.Text.Split(vbCrLf.ToCharArray())
                For Each line In lines
                    fields.Add(line)
                Next
            End If
        Next

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
