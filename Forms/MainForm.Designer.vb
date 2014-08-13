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
        Me.tsSaveProfile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsSaveProfileAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveServerOutputAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExecMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutdownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.lboxConfig = New System.Windows.Forms.ListBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.udPortSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.CommandToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(773, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsSaveProfile, Me.tsSaveProfileAs, Me.SaveServerOutputAsToolStripMenuItem, Me.ToolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'tsSaveProfile
        '
        Me.tsSaveProfile.Name = "tsSaveProfile"
        Me.tsSaveProfile.Size = New System.Drawing.Size(199, 22)
        Me.tsSaveProfile.Text = "Save Profile"
        '
        'tsSaveProfileAs
        '
        Me.tsSaveProfileAs.Name = "tsSaveProfileAs"
        Me.tsSaveProfileAs.Size = New System.Drawing.Size(199, 22)
        Me.tsSaveProfileAs.Text = "Save Profile As..."
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
        Me.CommandToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoginToolStripMenuItem, Me.LogoutToolStripMenuItem, Me.StartToolStripMenuItem, Me.StopToolStripMenuItem, Me.ExtendToolStripMenuItem, Me.ExecMenuItem, Me.StatusToolStripMenuItem, Me.ConsoleToolStripMenuItem, Me.ShutdownToolStripMenuItem})
        Me.CommandToolStripMenuItem.Name = "CommandToolStripMenuItem"
        Me.CommandToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.CommandToolStripMenuItem.Text = "Command"
        '
        'LoginToolStripMenuItem
        '
        Me.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem"
        Me.LoginToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.LoginToolStripMenuItem.Text = "Login"
        '
        'LogoutToolStripMenuItem
        '
        Me.LogoutToolStripMenuItem.Name = "LogoutToolStripMenuItem"
        Me.LogoutToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.LogoutToolStripMenuItem.Text = "Logout"
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'ExtendToolStripMenuItem
        '
        Me.ExtendToolStripMenuItem.Name = "ExtendToolStripMenuItem"
        Me.ExtendToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ExtendToolStripMenuItem.Text = "Extend"
        '
        'ExecMenuItem
        '
        Me.ExecMenuItem.Name = "ExecMenuItem"
        Me.ExecMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ExecMenuItem.Text = "Exec"
        '
        'StatusToolStripMenuItem
        '
        Me.StatusToolStripMenuItem.Name = "StatusToolStripMenuItem"
        Me.StatusToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.StatusToolStripMenuItem.Text = "Status"
        '
        'ConsoleToolStripMenuItem
        '
        Me.ConsoleToolStripMenuItem.Name = "ConsoleToolStripMenuItem"
        Me.ConsoleToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ConsoleToolStripMenuItem.Text = "Console"
        '
        'ShutdownToolStripMenuItem
        '
        Me.ShutdownToolStripMenuItem.Name = "ShutdownToolStripMenuItem"
        Me.ShutdownToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.ShutdownToolStripMenuItem.Text = "Shutdown"
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
        Me.boxUname.Location = New System.Drawing.Point(434, 37)
        Me.boxUname.Name = "boxUname"
        Me.boxUname.Size = New System.Drawing.Size(169, 20)
        Me.boxUname.TabIndex = 2
        '
        'udPortSelect
        '
        Me.udPortSelect.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.udPortSelect.Location = New System.Drawing.Point(50, 69)
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
        Me.lblPort.Location = New System.Drawing.Point(9, 71)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(29, 13)
        Me.lblPort.TabIndex = 7
        Me.lblPort.Text = "Port:"
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Location = New System.Drawing.Point(374, 40)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(58, 13)
        Me.lblUsername.TabIndex = 8
        Me.lblUsername.Text = "Username:"
        '
        'boxPassword
        '
        Me.boxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxPassword.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxPassword.Location = New System.Drawing.Point(434, 69)
        Me.boxPassword.Name = "boxPassword"
        Me.boxPassword.Size = New System.Drawing.Size(169, 20)
        Me.boxPassword.TabIndex = 3
        Me.boxPassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(374, 71)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 10
        Me.lblPassword.Text = "Password:"
        '
        'rboxOutput
        '
        Me.rboxOutput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rboxOutput.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rboxOutput.Location = New System.Drawing.Point(0, 124)
        Me.rboxOutput.Name = "rboxOutput"
        Me.rboxOutput.ReadOnly = True
        Me.rboxOutput.Size = New System.Drawing.Size(773, 282)
        Me.rboxOutput.TabIndex = 13
        Me.rboxOutput.TabStop = False
        Me.rboxOutput.Text = ""
        '
        'boxCommand
        '
        Me.boxCommand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.boxCommand.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxCommand.Location = New System.Drawing.Point(12, 98)
        Me.boxCommand.Name = "boxCommand"
        Me.boxCommand.Size = New System.Drawing.Size(749, 20)
        Me.boxCommand.TabIndex = 4
        '
        'btnConnect
        '
        Me.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnect.Location = New System.Drawing.Point(256, 34)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 17
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnDisconnect
        '
        Me.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDisconnect.Location = New System.Drawing.Point(256, 66)
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
        Me.btnDisconnect.TabIndex = 18
        Me.btnDisconnect.Text = "Disconnect"
        Me.btnDisconnect.UseVisualStyleBackColor = True
        '
        'lboxConfig
        '
        Me.lboxConfig.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lboxConfig.FormattingEnabled = True
        Me.lboxConfig.ItemHeight = 14
        Me.lboxConfig.Location = New System.Drawing.Point(609, 32)
        Me.lboxConfig.Name = "lboxConfig"
        Me.lboxConfig.Size = New System.Drawing.Size(152, 60)
        Me.lboxConfig.TabIndex = 19
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 403)
        Me.Controls.Add(Me.lboxConfig)
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
    Friend WithEvents SaveServerOutputAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents ExecMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConsoleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShutdownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsSaveProfile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lboxConfig As System.Windows.Forms.ListBox
    Friend WithEvents tsSaveProfileAs As System.Windows.Forms.ToolStripMenuItem
End Class
