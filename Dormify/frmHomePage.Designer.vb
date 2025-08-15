<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHomePage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHomePage))
        Me.btnGoToLogin = New System.Windows.Forms.Button()
        Me.btnGoToRegister = New System.Windows.Forms.Button()
        Me.btnGoToGuestView = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnGoToLogin
        '
        Me.btnGoToLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnGoToLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoToLogin.FlatAppearance.BorderSize = 0
        Me.btnGoToLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGoToLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGoToLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoToLogin.Location = New System.Drawing.Point(1283, 97)
        Me.btnGoToLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGoToLogin.Name = "btnGoToLogin"
        Me.btnGoToLogin.Size = New System.Drawing.Size(243, 62)
        Me.btnGoToLogin.TabIndex = 5
        Me.btnGoToLogin.Text = "    "
        Me.btnGoToLogin.UseVisualStyleBackColor = False
        '
        'btnGoToRegister
        '
        Me.btnGoToRegister.BackColor = System.Drawing.Color.Transparent
        Me.btnGoToRegister.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoToRegister.FlatAppearance.BorderSize = 0
        Me.btnGoToRegister.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGoToRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGoToRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoToRegister.Location = New System.Drawing.Point(1007, 97)
        Me.btnGoToRegister.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGoToRegister.Name = "btnGoToRegister"
        Me.btnGoToRegister.Size = New System.Drawing.Size(243, 62)
        Me.btnGoToRegister.TabIndex = 6
        Me.btnGoToRegister.Text = "    "
        Me.btnGoToRegister.UseVisualStyleBackColor = False
        '
        'btnGoToGuestView
        '
        Me.btnGoToGuestView.BackColor = System.Drawing.Color.Transparent
        Me.btnGoToGuestView.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoToGuestView.FlatAppearance.BorderSize = 0
        Me.btnGoToGuestView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnGoToGuestView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnGoToGuestView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoToGuestView.Location = New System.Drawing.Point(631, 556)
        Me.btnGoToGuestView.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGoToGuestView.Name = "btnGoToGuestView"
        Me.btnGoToGuestView.Size = New System.Drawing.Size(292, 36)
        Me.btnGoToGuestView.TabIndex = 7
        Me.btnGoToGuestView.Text = "    "
        Me.btnGoToGuestView.UseVisualStyleBackColor = False
        '
        'frmHomePage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.Dormify.My.Resources.Resources.homepage
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1613, 838)
        Me.Controls.Add(Me.btnGoToGuestView)
        Me.Controls.Add(Me.btnGoToRegister)
        Me.Controls.Add(Me.btnGoToLogin)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "frmHomePage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Home Page"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnGoToLogin As Button
    Friend WithEvents btnGoToRegister As Button
    Friend WithEvents btnGoToGuestView As Button
End Class
