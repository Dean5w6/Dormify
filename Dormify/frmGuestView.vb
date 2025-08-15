Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmGuestView
    Private ReadOnly ConnectionString As String = "Server=127.0.0.1;Uid=root;Pwd=;Database=dormitory_db;"

    Private Sub frmGuestView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDormitoriesForFilterComboBox()
        btnResetFilter_Click(Nothing, Nothing)
    End Sub

    Private Sub LoadDormitoriesForFilterComboBox()
        Using conn As New MySqlConnection(ConnectionString)
            Try
                Dim sql As String = "SELECT dormitory_id, name FROM dormitories ORDER BY name"
                Dim adapter As New MySqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                Dim allRow As DataRow = dt.NewRow()
                allRow("dormitory_id") = 0
                allRow("name") = "-- All Dormitories --"
                dt.Rows.InsertAt(allRow, 0)
                cboFilterDormitory.DataSource = dt
                cboFilterDormitory.DisplayMember = "name"
                cboFilterDormitory.ValueMember = "dormitory_id"
            Catch ex As Exception
                MessageBox.Show("Failed to load dormitories list for filter. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub PopulateGuestRoomCards(Optional dormitoryId As Integer = 0, Optional minCapacity As Integer = 0, Optional maxPrice As Decimal = 0)
        flpGuestRooms.Controls.Clear()

        Dim baseSql As String = "SELECT r.room_id, r.room_number, r.capacity, r.rent_rate, r.description, r.image_path, d.name AS dormitory_name, IFNULL(b_count.approved_bookings, 0) AS current_occupants FROM rooms r JOIN dormitories d ON r.dormitory_id = d.dormitory_id LEFT JOIN (SELECT room_id, COUNT(*) AS approved_bookings FROM bookings WHERE status = 'Approved' GROUP BY room_id) AS b_count ON r.room_id = b_count.room_id"
        Dim whereClauses As New List(Of String)()
        whereClauses.Add("r.status_override <> 'Under Maintenance' AND (b_count.approved_bookings IS NULL OR b_count.approved_bookings < r.capacity)")

        If dormitoryId > 0 Then
            whereClauses.Add($"r.dormitory_id = {dormitoryId}")
        End If
        If minCapacity > 1 Then
            whereClauses.Add($"r.capacity >= {minCapacity}")
        End If
        If maxPrice > 0 Then
            whereClauses.Add($"r.rent_rate <= {maxPrice}")
        End If

        Dim finalSql As String = baseSql & " WHERE " & String.Join(" AND ", whereClauses) & " ORDER BY d.name, r.room_number"

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(finalSql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            If dt.Rows.Count = 0 Then
                Dim lblNoResults As New Label With {
                    .Text = "No available rooms found matching your criteria.",
                    .Font = New Font("Century Gothic", 14, FontStyle.Bold),
                    .ForeColor = Color.Gray,
                    .AutoSize = False,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Dock = DockStyle.Fill
                }
                flpGuestRooms.Controls.Add(lblNoResults)
                Return
            End If

            For Each row As DataRow In dt.Rows
                Dim card As New Panel With {
                    .Width = 278,
                    .Height = 350,
                    .Margin = New Padding(15),
                    .BorderStyle = BorderStyle.FixedSingle,
                    .BackColor = Color.White
                }
                Dim pic As New PictureBox With {
                    .Dock = DockStyle.Top,
                    .Height = 150,
                    .SizeMode = PictureBoxSizeMode.Zoom,
                    .Padding = New Padding(5)
                }
                Dim lblDormName As New Label With {
                    .Dock = DockStyle.Top,
                    .Font = New Font("Century Gothic", 12, FontStyle.Bold),
                    .ForeColor = Color.MidnightBlue,
                    .Padding = New Padding(12, 10, 10, 0),
                    .Height = 35
                }
                Dim lblRoomNumber As New Label With {
                    .Dock = DockStyle.Top,
                    .Font = New Font("Century Gothic", 10),
                    .ForeColor = Color.MidnightBlue,
                    .Padding = New Padding(12, 0, 10, 0),
                    .Height = 25
                }
                Dim lblPrice As New Label With {
                    .Dock = DockStyle.Top,
                    .Font = New Font("Century Gothic", 11, FontStyle.Bold),
                    .ForeColor = Color.MidnightBlue,
                    .Padding = New Padding(12, 5, 10, 0),
                    .Height = 30
                }
                Dim lblBeds As New Label With {
                    .Dock = DockStyle.Top,
                    .Font = New Font("Century Gothic", 9, FontStyle.Italic),
                    .ForeColor = Color.Gray,
                    .Padding = New Padding(12, 0, 10, 10),
                    .Height = 30
                }
                Dim btnViewDetails As New Button With {
                    .Size = New Size(248, 45),
                    .Location = New Point(15, 280),
                    .Font = New Font("Century Gothic", 12, FontStyle.Bold),
                    .Text = "View Details",
                    .BackColor = Color.MidnightBlue,
                    .ForeColor = Color.White,
                    .FlatStyle = FlatStyle.Flat,
                    .Cursor = Cursors.Hand
                }
                btnViewDetails.FlatAppearance.BorderSize = 0

                lblDormName.Text = row("dormitory_name").ToString()
                lblRoomNumber.Text = "Room " & row("room_number").ToString()
                lblPrice.Text = Convert.ToDecimal(row("rent_rate")).ToString("C")
                Dim bedsAvailable As Integer = CInt(row("capacity")) - CInt(row("current_occupants"))
                lblBeds.Text = $"{bedsAvailable} / {row("capacity")} Beds available"

                Dim imagePath As String = row("image_path").ToString()
                If Not String.IsNullOrWhiteSpace(imagePath) Then
                    Try
                        pic.Image = CType(My.Resources.ResourceManager.GetObject(imagePath), Image)
                    Catch ex As Exception
                        pic.Image = Nothing
                    End Try
                End If

                Dim description As String = row("description").ToString()
                btnViewDetails.Tag = description

                AddHandler btnViewDetails.Click, AddressOf GuestViewDetails_Click

                card.Controls.Add(btnViewDetails)
                card.Controls.Add(lblBeds)
                card.Controls.Add(lblPrice)
                card.Controls.Add(lblRoomNumber)
                card.Controls.Add(lblDormName)
                card.Controls.Add(pic)

                flpGuestRooms.Controls.Add(card)
            Next
        End Using
    End Sub

    Private Sub GuestViewDetails_Click(sender As Object, e As EventArgs)
        Dim clickedButton = DirectCast(sender, Button)
        Dim roomDescription As String = clickedButton.Tag.ToString()

        Dim message As String = $"{roomDescription}{vbCrLf}{vbCrLf}To book this room, please log in or register a new account."
        MessageBox.Show(message, "Room Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnFilterRooms_Click(sender As Object, e As EventArgs) Handles btnFilterRooms.Click
        Dim dormId As Integer = 0
        If cboFilterDormitory.SelectedIndex > 0 AndAlso cboFilterDormitory.SelectedValue IsNot Nothing Then
            dormId = Convert.ToInt32(cboFilterDormitory.SelectedValue)
        End If
        Dim capacity As Integer = CInt(nudFilterCapacity.Value)
        Dim price As Decimal = nudFilterMaxPrice.Value

        PopulateGuestRoomCards(dormId, capacity, price)
    End Sub

    Private Sub btnResetFilter_Click(sender As Object, e As EventArgs) Handles btnResetFilter.Click
        cboFilterDormitory.SelectedIndex = 0
        nudFilterCapacity.Value = 1
        nudFilterMaxPrice.Value = 0
        PopulateGuestRoomCards()
    End Sub

    Private Sub btnBackToHomeFromGuest_Click(sender As Object, e As EventArgs) Handles btnBackToHomeFromGuest.Click
        Me.Close()
        frmHomePage.Show()
    End Sub
End Class