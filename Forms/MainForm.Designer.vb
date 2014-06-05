<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImportProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportUserToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveServerOutputAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.boxHost = New System.Windows.Forms.TextBox()
        Me.lblHost = New System.Windows.Forms.Label()
        Me.boxUname = New System.Windows.Forms.TextBox()
        Me.udPortSelect = New System.Windows.Forms.NumericUpDown()
        Me.lblPort = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.boxPassword = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.rboxOutput = New System.Windows.Forms.RichTextBox()
        Me.boxCommand = New System.Windows.Forms.TextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnDisconnect = New System.Windows.Forms.Button()
        Me.LoginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.udPortSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.CommandToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(365, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportProfileToolStripMenuItem, Me.ExportUserToolStripMenuItem, Me.ToolStripSeparator3, Me.ImportProfileToolStripMenuItem, Me.ImportUserToolStripMenuItem, Me.ToolStripSeparator1, Me.SaveServerOutputAsToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExportProfileToolStripMenuItem
        '
        Me.ExportProfileToolStripMenuItem.Name = "ExportProfileToolStripMenuItem"
        Me.ExportProfileToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ExportProfileToolStripMenuItem.Text = "Export Profile"
        '
        'ExportUserToolStripMenuItem
        '
        Me.ExportUserToolStripMenuItem.Name = "ExportUserToolStripMenuItem"
        Me.ExportUserToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ExportUserToolStripMenuItem.Text = "Export User"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(196, 6)
        '
        'ImportProfileToolStripMenuItem
        '
        Me.ImportProfileToolStripMenuItem.Name = "ImportProfileToolStripMenuItem"
        Me.ImportProfileToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ImportProfileToolStripMenuItem.Text = "Import Profile"
        '
        'ImportUserToolStripMenuItem
        '
        Me.ImportUserToolStripMenuItem.Name = "ImportUserToolStripMenuItem"
        Me.ImportUserToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ImportUserToolStripMenuItem.Text = "Import User"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(196, 6)
        '
        'SaveServerOutputAsToolStripMenuItem
        '
        Me.SaveServerOutputAsToolStripMenuItem.Name = "SaveServerOutputAsToolStripMenuItem"
        Me.SaveServerOutputAsToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.SaveServerOutputAsToolStripMenuItem.Text = "Save Server Output As..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(196, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'CommandToolStripMenuItem
        '
        Me.CommandToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoginToolStripMenuItem, Me.LogoutToolStripMenuItem, Me.StartToolStripMenuItem, Me.StopToolStripMenuItem, Me.ExtendToolStripMenuItem, Me.StatusToolStripMenuItem})
        Me.CommandToolStripMenuItem.Name = "CommandToolStripMenuItem"
        Me.CommandToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.CommandToolStripMenuItem.Text = "Command"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'boxHost
        '
        Me.boxHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxHost.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxHost.Location = New System.Drawing.Point(50, 37)
        Me.boxHost.Name = "boxHost"
        Me.boxHost.Size = New System.Drawing.Size(188, 20)
        Me.boxHost.TabIndex = 0
        '
        'lblHost
        '
        Me.lblHost.AutoSize = True
        Me.lblHost.Location = New System.Drawing.Point(9, 39)
        Me.lblHost.Name = "lblHost"
        Me.lblHost.Size = New System.Drawing.Size(35, 13)
        Me.lblHost.TabIndex = 4
        Me.lblHost.Text = "Host: "
        '
        'boxUname
        '
        Me.boxUname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxUname.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxUname.Location = New System.Drawing.Point(69, 63)
        Me.boxUname.Name = "boxUname"
        Me.boxUname.Size = New System.Drawing.Size(169, 20)
        Me.boxUname.TabIndex = 2
        '
        'udPortSelect
        '
        Me.udPortSelect.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.udPortSelect.Location = New System.Drawing.Point(285, 32)
        Me.udPortSelect.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.udPortSelect.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.udPortSelect.Name = "udPortSelect"
        Me.udPortSelect.Size = New System.Drawing.Size(68, 20)
        Me.udPortSelect.TabIndex = 1
        Me.udPortSelect.Value = New Decimal(New Integer() {44446, 0, 0, 0})
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(250, 34)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(29, 13)
        Me.lblPort.TabIndex = 7
        Me.lblPort.Text = "Port:"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(9, 66)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 8
        Me.lblUsername.Text = "Username:"
        '
        'boxPassword
        '
        Me.boxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxPassword.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxPassword.Location = New System.Drawing.Point(69, 91)
        Me.boxPassword.Name = "boxPassword"
        Me.boxPassword.Size = New System.Drawing.Size(169, 20)
        Me.boxPassword.TabIndex = 3
        Me.boxPassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(9, 94)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 10
        Me.lblPassword.Text = "Password:"
        '
        'rboxOutput
        '
        Me.rboxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rboxOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rboxOutput.Location = New System.Drawing.Point(0, 144)
        Me.rboxOutput.Name = "rboxOutput"
        Me.rboxOutput.ReadOnly = True
        Me.rboxOutput.Size = New System.Drawing.Size(365, 262)
        Me.rboxOutput.TabIndex = 13
        Me.rboxOutput.TabStop = False
        Me.rboxOutput.Text = ""
        '
        'boxCommand
        '
        Me.boxCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxCommand.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxCommand.Location = New System.Drawing.Point(12, 118)
        Me.boxCommand.Name = "boxCommand"
        Me.boxCommand.Size = New System.Drawing.Size(341, 20)
        Me.boxCommand.TabIndex = 4
        '
        'btnConnect
        '
        Me.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnect.Location = New System.Drawing.Point(258, 63)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 17
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDisconnect.Location = New System.Drawing.Point(258, 89)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btnDisconnect.TabIndex = 18
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'LoginToolStripMenuItem
        '
        Me.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem"
        Me.LoginToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LoginToolStripMenuItem.Text = "Login"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ExtendToolStripMenuItem
        '
        Me.ExtendToolStripMenuItem.Name = "ExtendToolStripMenuItem"
        Me.ExtendToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExtendToolStripMenuItem.Text = "Extend"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 403)
        Me.Controls.Add(Me.btnDisconnect)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.boxCommand)
        Me.Controls.Add(Me.rboxOutput)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.boxPassword)
        Me.Controls.Add(Me.lblUsername)
        Me.Controls.Add(Me.lblPort)
        Me.Controls.Add(Me.udPortSelect)
        Me.Controls.Add(Me.boxUname)
        Me.Controls.Add(Me.lblHost)
        Me.Controls.Add(Me.boxHost)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Minecontrol Client"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.udPortSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CommandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents boxHost As System.Windows.Forms.TextBox
    Friend WithEvents lblHost As System.Windows.Forms.Label
    Friend WithEvents boxUname As System.Windows.Forms.TextBox
    Friend WithEvents udPortSelect As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblPort As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents boxPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents ExportProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SaveServerOutputAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ImportProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportUserToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents rboxOutput As System.Windows.Forms.RichTextBox
    Friend WithEvents boxCommand As System.Windows.Forms.TextBox
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnDisconnect As System.Windows.Forms.Button
    Friend WithEvents LoginToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExtendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
