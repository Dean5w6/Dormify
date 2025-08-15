<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.btnBackToHomeFromLogin = New System.Windows.Forms.Button()
        Me.btnTogglePasswordLogin = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUsername.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtUsername.Location = New System.Drawing.Point(625, 407)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(356, 30)
        Me.txtUsername.TabIndex = 2
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtPassword.Location = New System.Drawing.Point(668, 284)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(353, 30)
        Me.txtPassword.TabIndex = 3
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Location = New System.Drawing.Point(547, 582)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(527, 78)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "  "
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'btnRegister
        '
        Me.btnRegister.BackColor = System.Drawing.Color.Transparent
        Me.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegister.Location = New System.Drawing.Point(865, 682)
        Me.btnRegister.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(116, 33)
        Me.btnRegister.TabIndex = 5
        Me.btnRegister.Text = "  "
        Me.btnRegister.UseVisualStyleBackColor = False
        '
        'btnBackToHomeFromLogin
        '
        Me.btnBackToHomeFromLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackToHomeFromLogin.FlatAppearance.BorderSize = 0
        Me.btnBackToHomeFromLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackToHomeFromLogin.Location = New System.Drawing.Point(92, 48)
        Me.btnBackToHomeFromLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBackToHomeFromLogin.Name = "btnBackToHomeFromLogin"
        Me.btnBackToHomeFromLogin.Size = New System.Drawing.Size(61, 57)
        Me.btnBackToHomeFromLogin.TabIndex = 6
        Me.btnBackToHomeFromLogin.Text = "  "
        Me.btnBackToHomeFromLogin.UseVisualStyleBackColor = False
        '
        'btnTogglePasswordLogin
        '
        Me.btnTogglePasswordLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnTogglePasswordLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTogglePasswordLogin.FlatAppearance.BorderSize = 0
        Me.btnTogglePasswordLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTogglePasswordLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTogglePasswordLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTogglePasswordLogin.Location = New System.Drawing.Point(987, 507)
        Me.btnTogglePasswordLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnTogglePasswordLogin.Name = "btnTogglePasswordLogin"
        Me.btnTogglePasswordLogin.Size = New System.Drawing.Size(49, 37)
        Me.btnTogglePasswordLogin.TabIndex = 26
        Me.btnTogglePasswordLogin.Text = "  "
        Me.btnTogglePasswordLogin.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(619, 407)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 30)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Username"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Enabled = False
        Me.Label2.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(619, 505)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 30)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Password"
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Dormify.My.Resources.Resources.eye_closed_bg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1613, 838)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTogglePasswordLogin)
        Me.Controls.Add(Me.btnBackToHomeFromLogin)
        Me.Controls.Add(Me.btnRegister)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnRegister As Button
    Friend WithEvents btnBackToHomeFromLogin As Button
    Friend WithEvents btnTogglePasswordLogin As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
End Class
