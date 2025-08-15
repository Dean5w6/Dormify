<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGuestView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGuestView))
        Me.dgvGuestRooms = New System.Windows.Forms.DataGridView()
        Me.btnResetFilter = New System.Windows.Forms.Button()
        Me.btnFilterRooms = New System.Windows.Forms.Button()
        Me.nudFilterMaxPrice = New System.Windows.Forms.NumericUpDown()
        Me.nudFilterCapacity = New System.Windows.Forms.NumericUpDown()
        Me.cboFilterDormitory = New System.Windows.Forms.ComboBox()
        Me.btnBackToHomeFromGuest = New System.Windows.Forms.Button()
        Me.lblGuestRoomDescriptionPreview = New System.Windows.Forms.Label()
        Me.picGuestRoom = New System.Windows.Forms.PictureBox()
        Me.flpGuestRooms = New System.Windows.Forms.FlowLayoutPanel()
        CType(Me.dgvGuestRooms, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFilterMaxPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudFilterCapacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picGuestRoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvGuestRooms
        '
        Me.dgvGuestRooms.AllowUserToAddRows = False
        Me.dgvGuestRooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvGuestRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGuestRooms.Location = New System.Drawing.Point(-904, 348)
        Me.dgvGuestRooms.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgvGuestRooms.Name = "dgvGuestRooms"
        Me.dgvGuestRooms.ReadOnly = True
        Me.dgvGuestRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGuestRooms.Size = New System.Drawing.Size(1001, 302)
        Me.dgvGuestRooms.TabIndex = 9
        Me.dgvGuestRooms.Visible = False
        '
        'btnResetFilter
        '
        Me.btnResetFilter.BackColor = System.Drawing.Color.Transparent
        Me.btnResetFilter.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnResetFilter.FlatAppearance.BorderSize = 0
        Me.btnResetFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnResetFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetFilter.Location = New System.Drawing.Point(956, 212)
        Me.btnResetFilter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnResetFilter.Name = "btnResetFilter"
        Me.btnResetFilter.Size = New System.Drawing.Size(268, 46)
        Me.btnResetFilter.TabIndex = 35
        Me.btnResetFilter.Text = " "
        Me.btnResetFilter.UseVisualStyleBackColor = False
        '
        'btnFilterRooms
        '
        Me.btnFilterRooms.BackColor = System.Drawing.Color.Transparent
        Me.btnFilterRooms.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFilterRooms.FlatAppearance.BorderSize = 0
        Me.btnFilterRooms.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFilterRooms.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFilterRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilterRooms.Location = New System.Drawing.Point(1241, 207)
        Me.btnFilterRooms.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnFilterRooms.Name = "btnFilterRooms"
        Me.btnFilterRooms.Size = New System.Drawing.Size(273, 49)
        Me.btnFilterRooms.TabIndex = 34
        Me.btnFilterRooms.Text = " "
        Me.btnFilterRooms.UseVisualStyleBackColor = False
        '
        'nudFilterMaxPrice
        '
        Me.nudFilterMaxPrice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nudFilterMaxPrice.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudFilterMaxPrice.ForeColor = System.Drawing.Color.MidnightBlue
        Me.nudFilterMaxPrice.Increment = New Decimal(New Integer() {500, 0, 0, 0})
        Me.nudFilterMaxPrice.Location = New System.Drawing.Point(677, 219)
        Me.nudFilterMaxPrice.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.nudFilterMaxPrice.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.nudFilterMaxPrice.Name = "nudFilterMaxPrice"
        Me.nudFilterMaxPrice.Size = New System.Drawing.Size(255, 23)
        Me.nudFilterMaxPrice.TabIndex = 33
        '
        'nudFilterCapacity
        '
        Me.nudFilterCapacity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.nudFilterCapacity.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudFilterCapacity.ForeColor = System.Drawing.Color.MidnightBlue
        Me.nudFilterCapacity.Location = New System.Drawing.Point(392, 220)
        Me.nudFilterCapacity.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.nudFilterCapacity.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudFilterCapacity.Name = "nudFilterCapacity"
        Me.nudFilterCapacity.Size = New System.Drawing.Size(255, 23)
        Me.nudFilterCapacity.TabIndex = 32
        Me.nudFilterCapacity.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cboFilterDormitory
        '
        Me.cboFilterDormitory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboFilterDormitory.Font = New System.Drawing.Font("Century Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFilterDormitory.ForeColor = System.Drawing.Color.MidnightBlue
        Me.cboFilterDormitory.FormattingEnabled = True
        Me.cboFilterDormitory.Location = New System.Drawing.Point(105, 215)
        Me.cboFilterDormitory.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cboFilterDormitory.Name = "cboFilterDormitory"
        Me.cboFilterDormitory.Size = New System.Drawing.Size(257, 29)
        Me.cboFilterDormitory.TabIndex = 31
        Me.cboFilterDormitory.Text = "Test"
        '
        'btnBackToHomeFromGuest
        '
        Me.btnBackToHomeFromGuest.BackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromGuest.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackToHomeFromGuest.FlatAppearance.BorderSize = 0
        Me.btnBackToHomeFromGuest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromGuest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBackToHomeFromGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackToHomeFromGuest.Location = New System.Drawing.Point(105, 43)
        Me.btnBackToHomeFromGuest.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBackToHomeFromGuest.Name = "btnBackToHomeFromGuest"
        Me.btnBackToHomeFromGuest.Size = New System.Drawing.Size(60, 50)
        Me.btnBackToHomeFromGuest.TabIndex = 36
        Me.btnBackToHomeFromGuest.Text = " "
        Me.btnBackToHomeFromGuest.UseVisualStyleBackColor = False
        '
        'lblGuestRoomDescriptionPreview
        '
        Me.lblGuestRoomDescriptionPreview.Location = New System.Drawing.Point(1565, 676)
        Me.lblGuestRoomDescriptionPreview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblGuestRoomDescriptionPreview.Name = "lblGuestRoomDescriptionPreview"
        Me.lblGuestRoomDescriptionPreview.Size = New System.Drawing.Size(515, 105)
        Me.lblGuestRoomDescriptionPreview.TabIndex = 37
        Me.lblGuestRoomDescriptionPreview.Text = "Description"
        Me.lblGuestRoomDescriptionPreview.Visible = False
        '
        'picGuestRoom
        '
        Me.picGuestRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picGuestRoom.Location = New System.Drawing.Point(1568, 348)
        Me.picGuestRoom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.picGuestRoom.Name = "picGuestRoom"
        Me.picGuestRoom.Size = New System.Drawing.Size(511, 301)
        Me.picGuestRoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picGuestRoom.TabIndex = 20
        Me.picGuestRoom.TabStop = False
        Me.picGuestRoom.Visible = False
        '
        'flpGuestRooms
        '
        Me.flpGuestRooms.AutoScroll = True
        Me.flpGuestRooms.BackColor = System.Drawing.Color.White
        Me.flpGuestRooms.Location = New System.Drawing.Point(105, 281)
        Me.flpGuestRooms.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.flpGuestRooms.Name = "flpGuestRooms"
        Me.flpGuestRooms.Padding = New System.Windows.Forms.Padding(13, 12, 13, 12)
        Me.flpGuestRooms.Size = New System.Drawing.Size(1401, 514)
        Me.flpGuestRooms.TabIndex = 38
        '
        'frmGuestView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Dormify.My.Resources.Resources.guest_view
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(1613, 838)
        Me.Controls.Add(Me.flpGuestRooms)
        Me.Controls.Add(Me.lblGuestRoomDescriptionPreview)
        Me.Controls.Add(Me.btnBackToHomeFromGuest)
        Me.Controls.Add(Me.btnResetFilter)
        Me.Controls.Add(Me.btnFilterRooms)
        Me.Controls.Add(Me.nudFilterMaxPrice)
        Me.Controls.Add(Me.nudFilterCapacity)
        Me.Controls.Add(Me.cboFilterDormitory)
        Me.Controls.Add(Me.picGuestRoom)
        Me.Controls.Add(Me.dgvGuestRooms)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmGuestView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Guest View"
        CType(Me.dgvGuestRooms, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFilterMaxPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudFilterCapacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picGuestRoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvGuestRooms As DataGridView
    Friend WithEvents picGuestRoom As PictureBox
    Friend WithEvents btnResetFilter As Button
    Friend WithEvents btnFilterRooms As Button
    Friend WithEvents nudFilterMaxPrice As NumericUpDown
    Friend WithEvents nudFilterCapacity As NumericUpDown
    Friend WithEvents cboFilterDormitory As ComboBox
    Friend WithEvents btnBackToHomeFromGuest As Button
    Friend WithEvents lblGuestRoomDescriptionPreview As Label
    Friend WithEvents flpGuestRooms As FlowLayoutPanel
End Class
