Imports MySql.Data.MySqlClient
Imports System.Reflection
Imports System.Data

Public Class Form1
    Private ReadOnly ConnectionString As String = "Server=127.0.0.1;Uid=root;Pwd=;Database=dormitory_db;"
    Private loggedInUserId As Integer = 0
    Private loggedInUserRole As String = ""
    Private isLoggingOut As Boolean = False

    Private selectedDormitoryId As Integer = 0
    Private selectedRoomId As Integer = 0
    Private selectedUserId As Integer = 0
    Private selectedExpenseId As Integer = 0

    Private isShowingPendingRequests As Boolean = True
    Private currentDashboardViewType As String = "Pending Requests"

    Public Sub SetWelcomeAndPermissions(userId As Integer, username As String, userRole As String, firstName As String, lastName As String)
        loggedInUserId = userId
        loggedInUserRole = userRole
        dashWelcome.Text = $"Welcome, {firstName} {lastName}!"
        dashWelcomeRole.Text = $"(Role: {userRole})"
        dashLogOut.Location = New Point(1107, 32)


        Dim adminDashboardControls As Control() = {lblAdminKpiRequests, lblAdminKpiOccupancy, lblAdminKpiUnpaid, lblAdminKpiIncome, dgvDashboardView, btnAcceptRequestDashboard, btnRejectRequestDashboard, btnViewPayments, btnViewOccupancy, btnViewUsers, btnViewAvailableRoomsDashboard}
        Dim managerDashboardControls As Control() = {lblManagerKpiRequests, lblManagerKpiUnpaid, dgvDashboardView, btnAcceptRequestDashboard, btnRejectRequestDashboard, btnViewPayments, btnViewOccupancy, btnViewAvailableRoomsDashboard, btnViewUnpaidBills}
        Dim tenantDashboardControls As Control() = {lblTenantStatus, lblTenantDorm, lblTenantNextDueDate, lblTenantNextAmountDue}

        Dim adminNavButtons As New List(Of Button) From {
            dashBtnNavDashboard, dashBtnNavDormitories, dashBtnNavRooms, dashBtnNavUsers, dashBtnNavBilling, dashBtnNavExpenses, dashBtnNavFinancialReports, dashBtnNavActivityLog,
            dormBtnNavDashboard, dormBtnNavDormitories, dormBtnNavRooms, dormBtnNavUsers, dormBtnNavBilling, dormBtnNavExpenses, dormBtnNavFinancialReports, dormBtnNavActivityLog,
            roomBtnNavDashboard, roomBtnNavDormitories, roomBtnNavRooms, roomBtnNavUsers, roomBtnNavBilling, roomBtnNavExpenses, roomBtnNavFinancialReports, roomBtnNavActivityLog,
            userBtnNavDashboard, userBtnNavDormitories, userBtnNavRooms, userBtnNavUsers, userBtnNavBilling, userBtnNavExpenses, userBtnNavFinancialReports, userBtnNavActivityLog,
            billBtnNavDashboard, billBtnNavDormitories, billBtnNavRooms, billBtnNavUsers, billBtnNavBilling, billBtnNavExpenses, billBtnNavFinancialReports, billBtnNavActivityLog,
            expBtnNavDashboard, expBtnNavDormitories, expBtnNavRooms, expBtnNavUsers, expBtnNavBilling, expBtnNavExpenses, expBtnNavFinancialReports, expBtnNavActivityLog,
            finBtnNavDashboard, finBtnNavDormitories, finBtnNavRooms, finBtnNavUsers, finBtnNavBilling, finBtnNavExpenses, finBtnNavFinancialReports, finBtnNavActivityLog,
            logBtnNavDashboard, logBtnNavDormitories, logBtnNavRooms, logBtnNavUsers, logBtnNavBilling, logBtnNavExpenses, logBtnNavFinancialReports, logBtnNavActivityLog
        }
        Dim managerNavButtons As New List(Of Button) From {
            dashBtnNavDashboard, dashBtnNavRooms, dashBtnNavBilling, dashBtnNavExpenses,
            roomBtnNavDashboard, roomBtnNavRooms, roomBtnNavBilling, roomBtnNavExpenses,
            billBtnNavDashboard, billBtnNavRooms, billBtnNavBilling, billBtnNavExpenses,
            expBtnNavDashboard, expBtnNavRooms, expBtnNavBilling, expBtnNavExpenses
        }
        Dim tenantNavButtons As New List(Of Button) From {
            dashBtnNavDashboard, dashBtnNavTenantBooking, dashBtnNavMyAccount,
            bookBtnNavDashboard, bookBtnNavTenantBooking, bookBtnNavMyAccount,
            acctBtnNavDashboard, acctBtnNavTenantBooking, acctBtnNavMyAccount
        }

        Dim allDashboardControls As New List(Of Control)
        allDashboardControls.AddRange(adminDashboardControls)
        allDashboardControls.AddRange(managerDashboardControls)
        allDashboardControls.AddRange(tenantDashboardControls)
        For Each ctrl In allDashboardControls.Distinct()
            ctrl.Visible = False
        Next

        Dim allNavButtons As New List(Of Button)
        allNavButtons.AddRange(adminNavButtons)
        allNavButtons.AddRange(managerNavButtons)
        allNavButtons.AddRange(tenantNavButtons)
        For Each btn In allNavButtons.Distinct()
            btn.Visible = False
        Next

        Select Case userRole
            Case "Main Admin", "Admin"
                tpDashboard.BackgroundImage = My.Resources.dashboard_bg_admin
                For Each ctrl In adminDashboardControls
                    ctrl.Visible = True
                Next

                SetAdminButtonLocations()

                tpExpenses.BackgroundImage = My.Resources.admin_expenses
                tpRoomManagement.BackgroundImage = My.Resources.admin_rooms_bg
                tpBillingManagement.BackgroundImage = My.Resources.billing
                pnlDashboardSearch.Visible = True
                txtDashboardSearch.Visible = True
                For Each btn In adminNavButtons
                    btn.Visible = True
                Next

            Case "Dorm Manager"
                tpDashboard.BackgroundImage = My.Resources.dashboard_bg_manager
                For Each ctrl In managerDashboardControls
                    ctrl.Visible = True
                Next

                SetManagerButtonsLocations()

                tpExpenses.BackgroundImage = My.Resources.mgr_expense
                tpRoomManagement.BackgroundImage = My.Resources.mgr_rooms_bg
                tpBillingManagement.BackgroundImage = My.Resources.mgr_billing

                Label20.Visible = True
                For Each btn In managerNavButtons
                    btn.Visible = True
                Next

            Case "Tenant"
                tpDashboard.BackgroundImage = My.Resources.dashboard_bg_tenant
                For Each ctrl In tenantDashboardControls
                    ctrl.Visible = True
                Next

                SetTenantButtonsLocations()

                For Each btn In tenantNavButtons
                    btn.Visible = True
                Next
                pnlDashboardSearch.Visible = False

            Case Else
                tpDashboard.BackgroundImage = Nothing
        End Select

        tpDashboard.BackgroundImageLayout = ImageLayout.Zoom
        TabControl1.SelectedTab = tpDashboard
        LoadDashboardData()

        dashWelcome.Text = $"Welcome, {firstName} {lastName}!"
        dashWelcome.Location = New Point(135, 26)
        dashWelcomeRole.Text = $"(Role: {userRole})"
        dashWelcomeRole.Location = New Point(135, 63)
        dashLogOut.Location = New Point(1107, 32)

        dormWelcome.Text = $"Welcome, {firstName} {lastName}!"
        dormWelcome.Location = New Point(135, 26)
        dormWelcomeRole.Text = $"(Role: {userRole})"
        dormWelcomeRole.Location = New Point(135, 63)
        dormLogOut.Location = New Point(1107, 32)

        roomWelcome.Text = $"Welcome, {firstName} {lastName}!"
        roomWelcome.Location = New Point(135, 26)
        roomWelcomeRole.Text = $"(Role: {userRole})"
        roomWelcomeRole.Location = New Point(135, 63)
        roomLogOut.Location = New Point(1107, 32)

        userWelcome.Text = $"Welcome, {firstName} {lastName}!"
        userWelcome.Location = New Point(135, 26)
        userWelcomeRole.Text = $"(Role: {userRole})"
        userWelcomeRole.Location = New Point(135, 63)
        userLogOut.Location = New Point(1107, 32)

        billWelcome.Text = $"Welcome, {firstName} {lastName}!"
        billWelcome.Location = New Point(135, 26)
        billWelcomeRole.Text = $"(Role: {userRole})"
        billWelcomeRole.Location = New Point(135, 63)
        billLogOut.Location = New Point(1107, 32)

        expWelcome.Text = $"Welcome, {firstName} {lastName}!"
        expWelcome.Location = New Point(135, 26)
        expWelcomeRole.Text = $"(Role: {userRole})"
        expWelcomeRole.Location = New Point(135, 63)
        expLogOut.Location = New Point(1107, 32)

        finWelcome.Text = $"Welcome, {firstName} {lastName}!"
        finWelcome.Location = New Point(135, 26)
        finWelcomeRole.Text = $"(Role: {userRole})"
        finWelcomeRole.Location = New Point(135, 63)
        finLogOut.Location = New Point(1107, 32)

        logWelcome.Text = $"Welcome, {firstName} {lastName}!"
        logWelcome.Location = New Point(135, 26)
        logWelcomeRole.Text = $"(Role: {userRole})"
        logWelcomeRole.Location = New Point(135, 63)
        logLogOut.Location = New Point(1107, 32)

        bookWelcome.Text = $"Welcome, {firstName} {lastName}!"
        bookWelcome.Location = New Point(135, 26)
        bookWelcomeRole.Text = $"(Role: {userRole})"
        bookWelcomeRole.Location = New Point(135, 63)
        bookLogOut.Location = New Point(1107, 32)

        acctWelcome.Text = $"Welcome, {firstName} {lastName}!"
        acctWelcome.Location = New Point(135, 26)
        acctWelcomeRole.Text = $"(Role: {userRole})"
        acctWelcomeRole.Location = New Point(135, 63)
        acctLogOut.Location = New Point(1107, 32)
    End Sub

    Private Sub NavigateToTab(targetTab As TabPage)
        TabControl1.SelectedTab = targetTab
    End Sub

    Private Sub Navigation_Handler(sender As Object, e As EventArgs) Handles dashBtnNavDashboard.Click, dashBtnNavDormitories.Click, dashBtnNavRooms.Click, dashBtnNavUsers.Click, dashBtnNavTenantBooking.Click, dashBtnNavMyAccount.Click, dashBtnNavBilling.Click, dashBtnNavExpenses.Click, dashBtnNavFinancialReports.Click, dashBtnNavActivityLog.Click,
    dormBtnNavDashboard.Click, dormBtnNavDormitories.Click, dormBtnNavRooms.Click, dormBtnNavUsers.Click, dormBtnNavBilling.Click, dormBtnNavExpenses.Click, dormBtnNavFinancialReports.Click, dormBtnNavActivityLog.Click,
    roomBtnNavDashboard.Click, roomBtnNavDormitories.Click, roomBtnNavRooms.Click, roomBtnNavUsers.Click, roomBtnNavBilling.Click, roomBtnNavExpenses.Click, roomBtnNavFinancialReports.Click, roomBtnNavActivityLog.Click,
    userBtnNavDashboard.Click, userBtnNavDormitories.Click, userBtnNavRooms.Click, userBtnNavUsers.Click, userBtnNavBilling.Click, userBtnNavExpenses.Click, userBtnNavFinancialReports.Click, userBtnNavActivityLog.Click,
    bookBtnNavDashboard.Click, bookBtnNavTenantBooking.Click, bookBtnNavMyAccount.Click,
    acctBtnNavDashboard.Click, acctBtnNavTenantBooking.Click, acctBtnNavMyAccount.Click,
    billBtnNavDashboard.Click, billBtnNavDormitories.Click, billBtnNavRooms.Click, billBtnNavUsers.Click, billBtnNavBilling.Click, billBtnNavExpenses.Click, billBtnNavFinancialReports.Click, billBtnNavActivityLog.Click,
    expBtnNavDashboard.Click, expBtnNavDormitories.Click, expBtnNavRooms.Click, expBtnNavUsers.Click, expBtnNavBilling.Click, expBtnNavExpenses.Click, expBtnNavFinancialReports.Click, expBtnNavActivityLog.Click,
    finBtnNavDashboard.Click, finBtnNavDormitories.Click, finBtnNavRooms.Click, finBtnNavUsers.Click, finBtnNavBilling.Click, finBtnNavExpenses.Click, finBtnNavFinancialReports.Click, finBtnNavActivityLog.Click,
    logBtnNavDashboard.Click, logBtnNavDormitories.Click, logBtnNavRooms.Click, logBtnNavUsers.Click, logBtnNavBilling.Click, logBtnNavExpenses.Click, logBtnNavFinancialReports.Click, logBtnNavActivityLog.Click

        Dim btnName As String = DirectCast(sender, Button).Name

        If btnName.EndsWith("NavDashboard") Then NavigateToTab(tpDashboard)
        If btnName.EndsWith("NavDormitories") Then NavigateToTab(tpDormitoryManagement)
        If btnName.EndsWith("NavRooms") Then NavigateToTab(tpRoomManagement)
        If btnName.EndsWith("NavUsers") Then NavigateToTab(tpUserManagement)
        If btnName.EndsWith("NavTenantBooking") Then NavigateToTab(tpTenantBooking)
        If btnName.EndsWith("NavMyAccount") Then NavigateToTab(tpMyAccount)
        If btnName.EndsWith("NavBilling") Then NavigateToTab(tpBillingManagement)
        If btnName.EndsWith("NavExpenses") Then NavigateToTab(tpExpenses)
        If btnName.EndsWith("NavFinancialReports") Then NavigateToTab(tpFinancialReports)
        If btnName.EndsWith("NavActivityLog") Then NavigateToTab(tpActivityLog)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TabControl1.TabPages.Contains(tpDormitoryManagement) Then LoadDormitoriesForGrid()
        If TabControl1.TabPages.Contains(tpRoomManagement) Then
            LoadDormitoriesForComboBox()
            cboRoomStatusManual.Items.AddRange(New String() {"None", "Under Maintenance"})
            PopulateImageComboBox()
            LoadAvailableTenantsForComboBox()

            HandleDormitorySelectionChange()
        End If
        If TabControl1.TabPages.Contains(tpUserManagement) Then
            LoadUsers()
            cboUserRole.Items.AddRange(New String() {"Main Admin", "Admin", "Dorm Manager", "Tenant"})
        End If
        If TabControl1.TabPages.Contains(tpTenantBooking) Then
            LoadDormitoriesForFilterComboBox()
            btnResetFilter_Click(Nothing, Nothing)
        End If
        If TabControl1.TabPages.Contains(tpBookingManagement) Then LoadPendingBookings()
        If TabControl1.TabPages.Contains(tpBillingManagement) Then
            LoadTenantsToBill()
            LoadBillingHistory()
            rbShowUnpaid.Checked = True
        End If
        If TabControl1.TabPages.Contains(tpMyAccount) Then LoadMyAccountData()
        If TabControl1.TabPages.Contains(tpExpenses) Then
            LoadExpenses()
            cboExpenseCategory.Items.AddRange(New String() {"Maintenance", "Utilities", "Supplies", "Other"})
        End If
        If TabControl1.TabPages.Contains(tpManagerReports) Then
            cboReportType.Items.AddRange(New String() {"Current Occupancy List", "Monthly Payment Summary"})
        End If

        cboFilterRoomStatus.Items.Add("-- All Statuses --")
        cboFilterRoomStatus.Items.AddRange(New String() {"Available", "Occupied", "Under Maintenance"})
        cboFilterRoomStatus.SelectedIndex = 0

        PopulateImageComboBox()
        HandleDormitorySelectionChange()

        btnViewUnpaidBills.Location = New Point(877, 545)
        btnViewUnpaidBills.Text = " "
        lblManagerKpiRequests.Location = New Point(444, 250)
        dgvDashboardView.Location = New Point(132, 419)

        txtSearchDormitories.Clear()
        txtSearchRooms.Clear()
        txtSearchUsers.Clear()
        txtSearchTenantsToBill.Clear()
        txtSearchBillingHistory.Clear()
        txtSearchFinancialReports.Clear()
        txtSearchExpenses.Clear()
        txtSearchActivityLog.Clear()

        Me.Text = TabControl1.SelectedTab.Text
    End Sub

    Private Sub LoadDashboardData()
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            If loggedInUserRole = "Main Admin" OrElse loggedInUserRole = "Admin" Then
                lblAdminKpiRequests.Text = New MySqlCommand("SELECT COUNT(*) FROM bookings WHERE status = 'Pending'", conn).ExecuteScalar().ToString()
                lblAdminKpiUnpaid.Text = New MySqlCommand("SELECT COUNT(*) FROM billings WHERE status = 'Unpaid'", conn).ExecuteScalar().ToString()
                lblAdminKpiUnpaid.Location = New Point(900, 251)

                Dim totalOccupants = Convert.ToDecimal(New MySqlCommand("SELECT COUNT(*) FROM bookings WHERE status = 'Approved'", conn).ExecuteScalar())

                Dim capacityResult = New MySqlCommand("SELECT SUM(capacity) FROM rooms", conn).ExecuteScalar()
                Dim totalCapacity As Decimal = 0
                If capacityResult IsNot DBNull.Value AndAlso capacityResult IsNot Nothing Then
                    totalCapacity = Convert.ToDecimal(capacityResult)
                End If

                If totalCapacity > 0 Then
                    lblAdminKpiOccupancy.Text = $"{(totalOccupants / totalCapacity) * 100:0}%"
                Else
                    lblAdminKpiOccupancy.Text = "0%"
                End If

                Dim lastMonthIncome = New MySqlCommand("SELECT SUM(amount_paid) FROM payments WHERE payment_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY)", conn).ExecuteScalar()
                lblAdminKpiIncome.Text = If(lastMonthIncome IsNot DBNull.Value, Convert.ToDecimal(lastMonthIncome).ToString("C"), "₱0.00")
                LoadDashboardGridView("Pending Requests")

            ElseIf loggedInUserRole = "Dorm Manager" Then
                lblManagerKpiRequests.Text = New MySqlCommand("SELECT COUNT(*) FROM bookings WHERE status = 'Pending'", conn).ExecuteScalar().ToString()
                lblManagerKpiUnpaid.Text = New MySqlCommand("SELECT COUNT(*) FROM billings WHERE status = 'Unpaid'", conn).ExecuteScalar().ToString()
                LoadDashboardGridView("Pending Requests")

            ElseIf loggedInUserRole = "Tenant" Then
                Dim sqlBooking As String = $"SELECT b.status, d.name, r.room_number FROM bookings b JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.tenant_id = {loggedInUserId} ORDER BY b.booking_date DESC LIMIT 1"
                Using cmdBooking As New MySqlCommand(sqlBooking, conn)
                    Using readerBooking = cmdBooking.ExecuteReader()
                        If readerBooking.Read() Then
                            Dim status As String = readerBooking("status").ToString()
                            lblTenantStatus.Text = status

                            If status = "Approved" Then
                                lblTenantStatus.ForeColor = Color.LimeGreen
                                lblTenantDorm.Text = $"{readerBooking("name")}"
                                lblTenantRoom.Visible = True
                                lblTenantRoom.Text = $"{readerBooking("room_number")}"

                                readerBooking.Close()

                                Dim sqlBill As String = $"SELECT due_date, amount_due FROM billings WHERE tenant_id = {loggedInUserId} AND status = 'Unpaid' ORDER BY due_date ASC LIMIT 1"
                                Using cmdBill As New MySqlCommand(sqlBill, conn)
                                    Dim readerBill = cmdBill.ExecuteReader()
                                    If readerBill.Read() Then
                                        lblTenantNextDueDate.Text = Convert.ToDateTime(readerBill("due_date")).ToString("MMMM dd, yyyy")
                                        lblTenantNextAmountDue.Text = Convert.ToDecimal(readerBill("amount_due")).ToString("C")
                                    Else
                                        lblTenantNextDueDate.Text = "You are all paid up!"
                                        lblTenantNextAmountDue.Text = ""
                                    End If
                                End Using
                            ElseIf status = "Pending" Then
                                lblTenantStatus.ForeColor = Color.Gold
                                lblTenantDorm.Text = $"{readerBooking("name")}"
                                lblTenantRoom.Text = $"{readerBooking("room_number")}"
                                lblTenantRoom.Visible = True
                                lblTenantNextDueDate.Text = "N/A"
                                lblTenantNextAmountDue.Text = ""
                            ElseIf status = "Rejected" Then
                                lblTenantStatus.ForeColor = Color.Red
                                lblTenantDorm.Text = "N/A"
                                lblTenantRoom.Text = "N/A"
                                lblTenantRoom.Visible = True
                                lblTenantNextDueDate.Text = "N/A"
                                lblTenantNextAmountDue.Text = ""
                            Else
                                lblTenantStatus.ForeColor = Color.White
                                lblTenantDorm.Text = "N/A"
                                lblTenantRoom.Text = "N/A"
                                lblTenantRoom.Visible = True
                                lblTenantNextDueDate.Text = "N/A"
                                lblTenantNextAmountDue.Text = ""
                            End If
                        Else
                            lblTenantStatus.Text = "No Active Booking"
                            lblTenantDorm.Text = "N/A"
                            lblTenantRoom.Visible = True
                            lblTenantRoom.Text = "N/A"
                            lblTenantNextDueDate.Text = "N/A"
                            lblTenantNextAmountDue.Text = ""
                        End If
                    End Using
                End Using
            End If
        End Using
    End Sub

    Private Sub LoadDashboardGridView(viewType As String)
        currentDashboardViewType = viewType
        txtDashboardSearch.Clear()

        Dim sql As String = ""

        Select Case viewType
            Case "Pending Requests"
                sql = "SELECT b.booking_id AS 'Booking ID', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', d.name AS 'Dormitory', r.room_number AS 'Room Number', b.booking_date AS 'Booking Date' FROM bookings b JOIN users u ON b.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.status = 'Pending' ORDER BY b.booking_date"
                isShowingPendingRequests = True
                lblDashboardSearch.Text = "Search by Tenant Name:"
            Case "Monthly Payments"
                sql = "SELECT p.payment_date AS 'Payment Date', r.room_number AS 'Room Number', p.amount_paid AS 'Amount Paid', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant' FROM payments p JOIN billings b ON p.billing_id = b.billing_id JOIN bookings bk ON b.booking_id = bk.booking_id JOIN users u ON bk.tenant_id = u.user_id JOIN rooms r ON bk.room_id = r.room_id WHERE p.payment_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY) ORDER BY p.payment_date DESC"
                isShowingPendingRequests = False
                lblDashboardSearch.Text = "Search by Tenant Name:"
            Case "Occupancy List"
                sql = "SELECT b.booking_id AS 'Booking ID', d.name AS 'Dormitory', r.room_number AS 'Room Number', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', b.move_in_date AS 'Move-In Date' FROM bookings b JOIN users u ON b.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.status = 'Approved' ORDER BY d.name, r.room_number"
                isShowingPendingRequests = False
                lblDashboardSearch.Text = "Search by Room Number:"
            Case "Users List"
                sql = "SELECT username AS 'Username', CONCAT(first_name, ' ', last_name) AS 'Full Name', role AS 'Role' FROM users ORDER BY last_name"
                isShowingPendingRequests = False
                lblDashboardSearch.Text = "Search by Tenant Name:"

            Case "Available Rooms"
                sql = "SELECT r.room_number AS 'Room Number', CONCAT(IFNULL(b_count.approved_bookings, 0), '/', r.capacity) AS 'Capacity', r.rent_rate AS 'Rent Rate', CASE WHEN IFNULL(b_count.approved_bookings, 0) >= r.capacity THEN 'Occupied' ELSE 'Available' END AS 'Status' FROM rooms r LEFT JOIN (SELECT room_id, COUNT(*) AS approved_bookings FROM bookings WHERE status = 'Approved' GROUP BY room_id) AS b_count ON r.room_id = b_count.room_id WHERE (b_count.approved_bookings IS NULL OR b_count.approved_bookings < r.capacity) ORDER BY r.room_number"
                isShowingPendingRequests = False
                lblDashboardSearch.Text = "Search by Room Number:"

            Case "Unpaid Bills"
                sql = "SELECT bl.billing_id AS 'Billing ID', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', r.room_number AS 'Room Number', bl.amount_due AS 'Amount Due', bl.due_date AS 'Due Date' FROM billings bl JOIN bookings b ON bl.booking_id = b.booking_id JOIN users u ON bl.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id WHERE bl.status = 'Unpaid' ORDER BY bl.due_date DESC"
                isShowingPendingRequests = False
                lblDashboardSearch.Text = "Search by Tenant Name:"
        End Select

        btnRejectRequestDashboard.Visible = isShowingPendingRequests

        If isShowingPendingRequests Then
            If loggedInUserRole = "Main Admin" OrElse loggedInUserRole = "Admin" Then
                tpDashboard.BackgroundImage = My.Resources.dashboard_bg_admin
            ElseIf loggedInUserRole = "Dorm Manager" Then
                tpDashboard.BackgroundImage = My.Resources.dashboard_bg_manager
            End If
        Else
            If loggedInUserRole = "Main Admin" OrElse loggedInUserRole = "Admin" Then
                Select Case viewType
                    Case "Monthly Payments"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_admin_monthly_summary
                    Case "Occupancy List"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_admin_occupancy
                    Case "Users List"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_admin_users
                    Case "Available Rooms"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_admin_rooms
                    Case "Unpaid Bills"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_manager_unpaid
                End Select
            ElseIf loggedInUserRole = "Dorm Manager" Then
                Select Case viewType
                    Case "Monthly Payments"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_manager_monthly_payment_summary
                    Case "Occupancy List"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_manager_occupancy
                    Case "Available Rooms"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_manager_rooms
                    Case "Unpaid Bills"
                        tpDashboard.BackgroundImage = My.Resources.dashboard_manager_unpaid
                End Select
            End If
        End If

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvDashboardView.DataSource = dt
        End Using
    End Sub

    Private Sub btnAcceptRequestDashboard_Click(sender As Object, e As EventArgs) Handles btnAcceptRequestDashboard.Click
        If isShowingPendingRequests Then
            If dgvDashboardView.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a booking from the list to accept.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedBookingId As Integer = Convert.ToInt32(dgvDashboardView.SelectedRows(0).Cells("Booking ID").Value)
            ProcessBooking(selectedBookingId, "Approved")

            LoadDashboardGridView("Pending Requests")
            LoadDashboardData()
        Else
            LoadDashboardGridView("Pending Requests")
        End If
    End Sub

    Private Sub btnRejectRequestDashboard_Click(sender As Object, e As EventArgs) Handles btnRejectRequestDashboard.Click
        If isShowingPendingRequests Then
            If dgvDashboardView.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a booking from the list to reject.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedBookingId As Integer = Convert.ToInt32(dgvDashboardView.SelectedRows(0).Cells("Booking ID").Value)

            ProcessBooking(selectedBookingId, "Rejected")

            LoadDashboardGridView("Pending Requests")
            LoadDashboardData()
        End If
    End Sub
    Private Sub btnDashboardView_Click(sender As Object, e As EventArgs) Handles btnViewPayments.Click, btnViewOccupancy.Click, btnViewUsers.Click, btnViewAvailableRoomsDashboard.Click, btnViewUnpaidBills.Click
        Dim btn = DirectCast(sender, Button)
        Select Case btn.Name
            Case "btnViewPayments"
                LoadDashboardGridView("Monthly Payments")
            Case "btnViewOccupancy"
                LoadDashboardGridView("Occupancy List")
            Case "btnViewUsers"
                LoadDashboardGridView("Users List")
            Case "btnViewAvailableRoomsDashboard"
                LoadDashboardGridView("Available Rooms")
            Case "btnViewUnpaidBills"
                LoadDashboardGridView("Unpaid Bills")
        End Select
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
        If dgvDormitories.Rows.Count > 0 Then
            dgvDormitories.Rows(0).Selected = True
            dgvDormitories_CellClick(dgvDormitories, New DataGridViewCellEventArgs(0, 0))
        End If
    End Sub

    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        If cboReportType.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a report type.", "No Report Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim reportName As String = cboReportType.SelectedItem.ToString()
        Dim sql As String = ""

        Select Case reportName
            Case "Current Occupancy List"
                sql = "SELECT d.name AS Dormitory, r.room_number AS 'Room Number', u.first_name AS 'First Name', u.last_name AS 'Last Name', u.phone_number AS 'Phone Number', b.move_in_date AS 'Move in Date' FROM bookings b JOIN users u ON b.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.status = 'Approved' ORDER BY d.name, r.room_number"
            Case "Monthly Payment Summary"
                sql = "SELECT p.payment_date AS 'Payment Date', p.amount_paid AS 'Amount Paid', u.username AS Tenant, r.room_number AS 'Room Number' FROM payments p JOIN billings b ON p.billing_id = b.billing_id JOIN bookings bk ON b.booking_id = bk.booking_id JOIN users u ON bk.tenant_id = u.user_id JOIN rooms r ON bk.room_id = r.room_id WHERE p.payment_date >= DATE_SUB(CURDATE(), INTERVAL 30 DAY) ORDER BY p.payment_date DESC"
        End Select

        Using conn As New MySqlConnection(ConnectionString)
            Try
                Dim adapter As New MySqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                adapter.Fill(dt)
                dgvReportResults.DataSource = dt
            Catch ex As Exception
                MessageBox.Show("Failed to run report. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnUpdateProfile_Click(sender As Object, e As EventArgs) Handles btnUpdateProfile.Click
        Dim firstName As String = txtMyFirstName.Text.Trim()
        Dim lastName As String = txtMyLastName.Text.Trim()
        Dim email As String = txtMyEmail.Text.Trim()
        Dim phone As String = txtMyPhoneNumber.Text.Trim()
        Dim newPass As String = txtMyNewPassword.Text
        Dim confirmPass As String = txtMyConfirmPassword.Text

        If String.IsNullOrWhiteSpace(firstName) OrElse String.IsNullOrWhiteSpace(lastName) OrElse String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(phone) Then
            MessageBox.Show("First Name, Last Name, Email, and Phone Number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If phone.Length <> 11 OrElse Not phone.StartsWith("09") OrElse Not IsNumeric(phone) Then
            MessageBox.Show("Please enter a valid phone number.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim passwordSqlPart As String = ""
        If Not String.IsNullOrWhiteSpace(newPass) OrElse Not String.IsNullOrWhiteSpace(confirmPass) Then

            If newPass <> confirmPass Then
                MessageBox.Show("The new passwords do not match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If newPass.Length < 8 Then
                MessageBox.Show("The new password must be at least 8 characters long.", "Password Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            passwordSqlPart = $"password_hash = SHA2('{newPass}', 256),"
        End If

        Dim sql As String = $"UPDATE users SET {passwordSqlPart} first_name = '{firstName}', last_name = '{lastName}', email = '{email}', phone_number = '{phone}' WHERE user_id = {loggedInUserId}"

        Using conn As New MySqlConnection(ConnectionString)
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Your profile has been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    txtMyNewPassword.Clear()
                    txtMyConfirmPassword.Clear()
                Catch ex As Exception
                    MessageBox.Show("Failed to update profile. It's possible the email is already in use. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadMyAccountData()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sqlProfile As String = $"SELECT username, first_name, last_name, email, phone_number FROM users WHERE user_id = {loggedInUserId}"
            Dim cmdProfile As New MySqlCommand(sqlProfile, conn)
            conn.Open()
            Dim readerProfile = cmdProfile.ExecuteReader()
            If readerProfile.Read() Then
                txtMyUsername.Text = readerProfile("username").ToString()
                txtMyFirstName.Text = readerProfile("first_name").ToString()
                txtMyLastName.Text = readerProfile("last_name").ToString()
                txtMyEmail.Text = readerProfile("email").ToString()
                txtMyPhoneNumber.Text = readerProfile("phone_number").ToString()
            End If
            readerProfile.Close()
        End Using

        Using conn As New MySqlConnection(ConnectionString)
            Dim sqlBooking As String = $"SELECT d.name, r.room_number, r.rent_rate, b.status FROM bookings b JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.tenant_id = {loggedInUserId} ORDER BY b.booking_date DESC LIMIT 1"
            Dim cmdBooking As New MySqlCommand(sqlBooking, conn)
            conn.Open()
            Dim readerBooking = cmdBooking.ExecuteReader()
            If readerBooking.Read() Then
                Dim status As String = readerBooking("status").ToString()
                txtMyBookingStatus.Text = status

                If status = "Pending" OrElse status = "Approved" Then
                    txtMyDormitory.Text = readerBooking("name").ToString()
                    txtMyRoomNumber.Text = readerBooking("room_number").ToString()
                    txtMyRentRate.Text = Convert.ToDecimal(readerBooking("rent_rate")).ToString("C")
                Else
                    txtMyDormitory.Text = "N/A"
                    txtMyRoomNumber.Text = "N/A"
                    txtMyRentRate.Text = "N/A"
                End If
            Else
                txtMyBookingStatus.Text = "No Active Booking"
                txtMyDormitory.Text = "N/A"
                txtMyRoomNumber.Text = "N/A"
                txtMyRentRate.Text = "N/A"
            End If
            readerBooking.Close()
        End Using

        Using conn As New MySqlConnection(ConnectionString)
            Dim sqlBills As String = $"SELECT due_date AS 'Due Date', amount_due AS 'Amount Due', status AS 'Status', created_at AS 'Bill Generated On' FROM billings WHERE tenant_id = {loggedInUserId} ORDER BY due_date DESC"
            Dim adapterBills As New MySqlDataAdapter(sqlBills, conn)
            Dim dt As New DataTable()
            adapterBills.Fill(dt)
            dgvMyBills.DataSource = dt
        End Using

        LoadMyBillingHistory()
    End Sub

    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        Dim startDate As String = dtpStartDate.Value.ToString("yyyy-MM-dd")
        Dim endDate As String = dtpEndDate.Value.ToString("yyyy-MM-dd")

        Dim sql As String = $"SELECT p.payment_id AS 'Payment ID', p.payment_date AS 'Payment Date', p.amount_paid AS 'Amount Paid', u.username AS Tenant, r.room_number AS 'Room Number' FROM payments p JOIN billings b ON p.billing_id = b.billing_id JOIN bookings bk ON b.booking_id = bk.booking_id JOIN users u ON bk.tenant_id = u.user_id JOIN rooms r ON bk.room_id = r.room_id WHERE p.payment_date BETWEEN '{startDate}' AND '{endDate}' ORDER BY p.payment_date"

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvIncomeReport.DataSource = dt
            Dim totalIncome As Decimal = 0
            For Each row As DataRow In dt.Rows
                totalIncome += Convert.ToDecimal(row("Amount Paid"))
            Next
            lblTotalIncome.Text = $"{totalIncome:C}"
        End Using
    End Sub

    Private Sub LoadTenantsToBill()
        Dim sql As String = "SELECT b.booking_id AS 'Booking ID', b.tenant_id AS 'Tenant ID', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', r.room_number AS 'Room Number', r.rent_rate AS 'Rent Rate' " &
                       "FROM bookings b " &
                       "JOIN users u ON b.tenant_id = u.user_id " &
                       "JOIN rooms r ON b.room_id = r.room_id " &
                       "JOIN ( " &
                       "    SELECT tenant_id, MAX(booking_date) AS latest_booking_date " &
                       "    FROM bookings " &
                       "    GROUP BY tenant_id " &
                       ") AS latest ON b.tenant_id = latest.tenant_id AND b.booking_date = latest.latest_booking_date " &
                       "WHERE b.status = 'Approved'"

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvTenantsToBill.DataSource = dt
        End Using
    End Sub

    Private Sub LoadBillingHistory()
        Dim statusFilter As String = If(rbShowUnpaid.Checked, "Unpaid", "Paid")

        Dim sql As String = $"SELECT bl.billing_id AS 'Billing ID', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', r.room_number AS 'Room Number', bl.amount_due AS 'Amount Due', bl.due_date AS 'Due Date', bl.status AS 'Status' FROM billings bl JOIN bookings b ON bl.booking_id = b.booking_id JOIN users u ON bl.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id WHERE bl.status = '{statusFilter}' ORDER BY bl.due_date DESC"

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvBillingHistory.DataSource = dt
        End Using
    End Sub

    Private Sub btnGenerateBill_Click(sender As Object, e As EventArgs) Handles btnGenerateBill.Click
        If dgvTenantsToBill.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a tenant to generate a bill for.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirmResult = MessageBox.Show("Are you sure you want to generate a new bill for this tenant?", "Confirm Bill Generation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.No Then Return

        Dim row As DataGridViewRow = dgvTenantsToBill.SelectedRows(0)
        Dim bookingId As Integer = Convert.ToInt32(row.Cells("Booking ID").Value)
        Dim tenantId As Integer = Convert.ToInt32(row.Cells("Tenant ID").Value)
        Dim amount As Decimal = Convert.ToDecimal(row.Cells("Rent Rate").Value)
        Dim dueDate As String = dtpDueDate.Value.ToString("yyyy-MM-dd")

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"INSERT INTO billings (booking_id, tenant_id, due_date, amount_due, status) VALUES ({bookingId}, {tenantId}, '{dueDate}', {amount}, 'Unpaid')"
            Using cmd As New MySqlCommand(sql, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Bill generated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBillingHistory()
            End Using
        End Using
    End Sub

    Private Sub btnPrintReceipt_Click(sender As Object, e As EventArgs) Handles btnPrintReceipt.Click
        If dgvBillingHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a paid bill to print a receipt for.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selectedBillingId As Integer = Convert.ToInt32(dgvBillingHistory.SelectedRows(0).Cells("Billing ID").Value)
        Dim paymentIdToPrint As Integer = 0

        Try
            Using conn As New MySqlConnection(ConnectionString)
                Dim sqlFindPayment As String = $"SELECT payment_id FROM payments WHERE billing_id = {selectedBillingId} ORDER BY payment_date DESC LIMIT 1"
                Using cmdFind As New MySqlCommand(sqlFindPayment, conn)
                    conn.Open()
                    Dim result = cmdFind.ExecuteScalar()
                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                        paymentIdToPrint = Convert.ToInt32(result)
                    Else
                        MessageBox.Show("Could not find a payment record for this bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If
                End Using
            End Using

            Dim receiptDataSet As New dsReceipt()
            Dim sqlReceipt As String = $"SELECT CONCAT(u.first_name, ' ', u.last_name) AS TenantName, r.room_number AS RoomNumber, d.name AS DormitoryName, p.payment_date AS PaymentDate, p.amount_paid AS AmountPaid, p.payment_method AS PaymentMethod, p.billing_id AS BillingID FROM payments p JOIN billings b ON p.billing_id = b.billing_id JOIN bookings bk ON b.booking_id = bk.booking_id JOIN users u ON bk.tenant_id = u.user_id JOIN rooms r ON bk.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE p.payment_id = {paymentIdToPrint}"
            Using conn As New MySqlConnection(ConnectionString)
                Using adapter As New MySqlDataAdapter(sqlReceipt, conn)
                    adapter.Fill(receiptDataSet, "DataTable1")
                End Using
            End Using

            Dim viewerForm As New frmReceiptViewer()
            viewerForm.ShowReceipt(receiptDataSet.Tables("DataTable1"))
            viewerForm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show("Could not generate receipt. Error: " & ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadDormitoriesForGrid()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT dormitory_id AS 'Dormitory ID', name AS 'Dormitory Name', address AS 'Address' FROM dormitories ORDER BY Name"
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvDormitories.DataSource = dt
        End Using

        If dgvDormitories.Rows.Count > 0 Then
            dgvDormitories.Rows(0).Selected = True
            dgvDormitories_CellClick(dgvDormitories, New DataGridViewCellEventArgs(0, 0))
        End If
    End Sub

    Private Sub dgvDormitories_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDormitories.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvDormitories.Rows(e.RowIndex)
            selectedDormitoryId = Convert.ToInt32(selectedRow.Cells("Dormitory ID").Value)
            txtDormitoryName.Text = selectedRow.Cells("Dormitory Name").Value.ToString()
            txtDormitoryAddress.Text = selectedRow.Cells("Address").Value.ToString()
        End If
    End Sub

    Private Sub btnClearDormitory_Click(sender As Object, e As EventArgs) Handles btnClearDormitory.Click
        selectedDormitoryId = 0
        txtDormitoryName.Clear()
        txtDormitoryAddress.Clear()
        dgvDormitories.ClearSelection()
    End Sub

    Private Sub btnSaveDormitory_Click(sender As Object, e As EventArgs) Handles btnSaveDormitory.Click
        Dim dormName As String = txtDormitoryName.Text.Trim()
        Dim dormAddress As String = txtDormitoryAddress.Text.Trim()
        If String.IsNullOrEmpty(dormName) Then Return
        Dim sql As String
        If selectedDormitoryId = 0 Then
            sql = $"INSERT INTO dormitories (name, address) VALUES ('{dormName}', '{dormAddress}')"
        Else
            sql = $"UPDATE dormitories SET name = '{dormName}', address = '{dormAddress}' WHERE dormitory_id = {selectedDormitoryId}"
        End If
        Using conn As New MySqlConnection(ConnectionString), cmd As New MySqlCommand(sql, conn)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
        LoadDormitoriesForGrid()
        btnClearDormitory_Click(Nothing, Nothing)
    End Sub

    Private Sub LoadDormitoriesForComboBox()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT dormitory_id, name FROM dormitories ORDER BY name"
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Dim allRow As DataRow = dt.NewRow()
            allRow("dormitory_id") = 0
            allRow("name") = "-- All Dormitories --"
            dt.Rows.InsertAt(allRow, 0)

            cboSelectDormitory.DataSource = dt
            cboSelectDormitory.DisplayMember = "name"
            cboSelectDormitory.ValueMember = "dormitory_id"
        End Using
    End Sub

    Private Sub cboSelectDormitory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSelectDormitory.SelectedIndexChanged
        HandleDormitorySelectionChange()
    End Sub

    Private Sub LoadRooms(dormitoryId As Integer)
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"SELECT r.room_id AS 'Room ID', r.room_number AS 'Room Number', CONCAT(IFNULL(b_count.approved_bookings, 0), '/', r.capacity) AS 'Capacity', r.rent_rate AS 'Rent Rate', CASE WHEN r.status_override = 'Under Maintenance' THEN 'Under Maintenance' WHEN IFNULL(b_count.approved_bookings, 0) >= r.capacity THEN 'Occupied' ELSE 'Available' END AS 'Status', r.description, r.image_path, r.status_override FROM rooms r LEFT JOIN (SELECT room_id, COUNT(*) AS approved_bookings FROM bookings WHERE status = 'Approved' GROUP BY room_id) AS b_count ON r.room_id = b_count.room_id WHERE r.dormitory_id = {dormitoryId} ORDER BY r.room_number"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvRooms.DataSource = dt

            If dgvRooms.Columns.Contains("image_path") Then dgvRooms.Columns("image_path").Visible = False
            If dgvRooms.Columns.Contains("description") Then dgvRooms.Columns("description").Visible = False
            If dgvRooms.Columns.Contains("status_override") Then dgvRooms.Columns("status_override").Visible = False
        End Using
    End Sub

    Private Sub dgvRooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRooms.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvRooms.Rows(e.RowIndex)
            selectedRoomId = Convert.ToInt32(row.Cells("Room ID").Value)
            txtRoomNumber.Text = row.Cells("Room Number").Value.ToString()

            Dim capacityString As String = row.Cells("Capacity").Value.ToString()
            Dim capacityValue As Decimal = 1
            If capacityString.Contains("/") Then
                Dim parts() As String = capacityString.Split("/"c)
                If parts.Length > 1 AndAlso IsNumeric(parts(1)) Then capacityValue = Convert.ToDecimal(parts(1))
            ElseIf IsNumeric(capacityString) Then
                capacityValue = Convert.ToDecimal(capacityString)
            End If
            nudCapacity.Value = capacityValue

            txtRentRate.Text = Convert.ToDecimal(row.Cells("Rent Rate").Value).ToString("F2")

            Dim statusOverride As String = row.Cells("status_override").Value.ToString()
            cboRoomStatusManual.SelectedItem = statusOverride

            txtRoomDescription.Text = row.Cells("description").Value.ToString()
            Dim imageResourceName As String = If(row.Cells("image_path").Value IsNot DBNull.Value, row.Cells("image_path").Value.ToString(), "")
            picRoomImage.Image = Nothing
            If cboSelectImage.Items.Count > 0 Then cboSelectImage.SelectedIndex = 0
            If Not String.IsNullOrWhiteSpace(imageResourceName) Then
                If cboSelectImage.Items.Contains(imageResourceName) Then
                    cboSelectImage.SelectedItem = imageResourceName
                    picRoomImage.Image = CType(My.Resources.ResourceManager.GetObject(imageResourceName), Image)
                End If
            End If

            LoadTenantsForSelectedRoom(selectedRoomId)
        End If
    End Sub

    Private Sub LoadTenantsForSelectedRoom(roomId As Integer)
        cboTenantsInRoom.DataSource = Nothing
        Dim sql As String = $"SELECT u.user_id, CONCAT(u.first_name, ' ', u.last_name) AS FullName FROM bookings b JOIN users u ON b.tenant_id = u.user_id WHERE b.room_id = {roomId} AND b.status = 'Approved'"
        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Dim placeholderRow As DataRow = dt.NewRow()
            placeholderRow("user_id") = 0
            placeholderRow("FullName") = "-- Select a Tenant --"
            dt.Rows.InsertAt(placeholderRow, 0)

            cboTenantsInRoom.DataSource = dt
            cboTenantsInRoom.DisplayMember = "FullName"
            cboTenantsInRoom.ValueMember = "user_id"
        End Using
    End Sub

    Private Sub btnClearRoom_Click(sender As Object, e As EventArgs) Handles btnClearRoom.Click
        selectedRoomId = 0
        txtRoomNumber.Clear()
        nudCapacity.Value = 1
        txtRentRate.Clear()
        txtRoomStatus.Clear()
        txtRoomDescription.Clear()
        dgvRooms.ClearSelection()
        btnClearImage_Click(Nothing, Nothing)
    End Sub

    Private Sub btnSaveRoom_Click(sender As Object, e As EventArgs) Handles btnSaveRoom.Click
        If cboSelectDormitory.SelectedIndex <= 0 OrElse cboSelectDormitory.SelectedValue Is Nothing OrElse Not IsNumeric(cboSelectDormitory.SelectedValue) Then
            MessageBox.Show("Please select a valid dormitory from the list before saving a room.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not IsNumeric(txtRentRate.Text) Then Return

        Dim finalImageResourceName As String = ""
        If cboSelectImage.SelectedIndex > 0 Then
            finalImageResourceName = cboSelectImage.SelectedItem.ToString()
        End If

        Dim dormId As Integer = Convert.ToInt32(cboSelectDormitory.SelectedValue)
        Dim roomNumber As String = txtRoomNumber.Text.Trim()
        Dim capacity As Integer = CInt(nudCapacity.Value)
        Dim rentRate As Decimal = Convert.ToDecimal(txtRentRate.Text)
        Dim description As String = txtRoomDescription.Text.Trim()
        Dim statusOverride As String = cboRoomStatusManual.SelectedItem.ToString()

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()
            Try
                If statusOverride = "Under Maintenance" AndAlso selectedRoomId > 0 Then
                    Dim sqlCompleteBookings As String = $"UPDATE bookings SET status = 'Completed' WHERE room_id = {selectedRoomId} AND status = 'Approved'"
                    Using cmdComplete As New MySqlCommand(sqlCompleteBookings, conn, transaction)
                        Dim bookingsAffected As Integer = cmdComplete.ExecuteNonQuery()
                        If bookingsAffected > 0 Then
                            LogActivity($"Completed {bookingsAffected} bookings for room ID {selectedRoomId} due to maintenance status.")
                        End If
                    End Using
                End If

                Dim sql As String
                If selectedRoomId = 0 Then
                    sql = $"INSERT INTO rooms (dormitory_id, room_number, capacity, rent_rate, status_override, description, image_path) VALUES ({dormId}, '{roomNumber}', {capacity}, {rentRate}, '{statusOverride}', '{description}', '{finalImageResourceName}')"
                Else
                    sql = $"UPDATE rooms SET dormitory_id = {dormId}, room_number = '{roomNumber}', capacity = {capacity}, rent_rate = {rentRate}, status_override = '{statusOverride}', description = '{description}', image_path = '{finalImageResourceName}' WHERE room_id = {selectedRoomId}"
                End If

                Using cmd As New MySqlCommand(sql, conn, transaction)
                    cmd.ExecuteNonQuery()
                End Using

                transaction.Commit()
                MessageBox.Show("Room details saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Failed to save room details. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        LoadRooms(dormId)
        btnClearRoom_Click(Nothing, Nothing)
    End Sub


    Private Sub LoadUsers()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT user_id AS 'User ID', username AS Username, first_name AS 'First Name', last_name AS 'Last Name', email AS Email, phone_number AS 'Phone Number', role AS Role FROM users ORDER BY last_name"
            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvUsers.DataSource = dt
        End Using
        If dgvUsers.Rows.Count > 0 Then
            dgvUsers.Rows(0).Selected = True
            dgvUsers_CellClick(dgvUsers, New DataGridViewCellEventArgs(0, 0))
        End If
    End Sub

    Private Sub dgvUsers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvUsers.Rows(e.RowIndex)
            selectedUserId = Convert.ToInt32(row.Cells("User ID").Value)
            txtUsername.Text = row.Cells("Username").Value.ToString()
            txtFirstName.Text = row.Cells("First Name").Value.ToString()
            txtLastName.Text = row.Cells("Last Name").Value.ToString()
            txtEmail.Text = row.Cells("Email").Value.ToString()
            txtPhoneNumber.Text = row.Cells("Phone Number").Value.ToString()
            cboUserRole.SelectedItem = row.Cells("Role").Value.ToString()
            txtPassword.Clear()

            Dim selectedUserRole As String = row.Cells("role").Value.ToString()

            If loggedInUserRole = "Admin" AndAlso (selectedUserRole = "Admin" OrElse selectedUserRole = "Main Admin") Then
                cboUserRole.Enabled = False
            ElseIf selectedUserRole = "Main Admin" Then
                cboUserRole.Enabled = False
            Else
                cboUserRole.Enabled = True
            End If
        End If
    End Sub

    Private Sub ResetUserForm()
        selectedUserId = 0
        txtUsername.Clear()
        txtPassword.Clear()
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtPhoneNumber.Clear()
        cboUserRole.SelectedIndex = -1
        dgvUsers.ClearSelection()
        cboUserRole.Enabled = True
    End Sub

    Private Sub btnSaveUser_Click(sender As Object, e As EventArgs) Handles btnSaveUser.Click
        If String.IsNullOrWhiteSpace(txtUsername.Text) OrElse String.IsNullOrWhiteSpace(txtFirstName.Text) OrElse String.IsNullOrWhiteSpace(txtLastName.Text) OrElse cboUserRole.SelectedItem Is Nothing Then Return
        If selectedUserId = 0 AndAlso String.IsNullOrWhiteSpace(txtPassword.Text) Then Return

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim phone As String = txtPhoneNumber.Text.Trim()
        Dim role As String = cboUserRole.SelectedItem.ToString()

        Dim sql As String
        Dim passwordPart As String = ""
        If Not String.IsNullOrWhiteSpace(password) Then
            If password.Length < 8 Then
                MessageBox.Show("Password must be at least 8 characters long.", "Password Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            passwordPart = $"password_hash = SHA2('{password}', 256),"
        End If

        If selectedUserId = 0 Then
            sql = $"INSERT INTO users (username, password_hash, first_name, last_name, email, phone_number, role) VALUES ('{username}', SHA2('{password}', 256), '{firstName}', '{lastName}', '{email}', '{phone}', '{role}')"
        Else
            sql = $"UPDATE users SET username = '{username}', {passwordPart} first_name = '{firstName}', last_name = '{lastName}', email = '{email}', phone_number = '{phone}', role = '{role}' WHERE user_id = {selectedUserId}"
        End If

        Using conn As New MySqlConnection(ConnectionString), cmd As New MySqlCommand(sql, conn)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("User saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If selectedUserId = 0 Then
                    LogActivity($"Created new user: {username} with role {role}.")
                Else
                    LogActivity($"Updated user details for: {username}.")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Failed to save user. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
        LoadUsers()
        ResetUserForm()
    End Sub

    Private Sub ProcessBooking(bookingId As Integer, status As String)
        Dim actionVerb As String = ""
        If status = "Approved" Then
            actionVerb = "APPROVE"
        ElseIf status = "Rejected" Then
            actionVerb = "REJECT"
        End If

        Dim confirmResult = MessageBox.Show($"Are you sure you want to {actionVerb} this booking request?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.No Then Return

        Dim roomIdToUpdate As Integer = 0
        Dim tenantIdApproved As Integer = 0

        Using conn As New MySqlConnection(ConnectionString)
            Dim cmd As New MySqlCommand($"SELECT room_id FROM bookings WHERE booking_id = {bookingId}", conn)
            conn.Open()
            roomIdToUpdate = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        If roomIdToUpdate = 0 Then
            MessageBox.Show("Could not find the associated room for this booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()
            Try
                Dim cmdBooking As New MySqlCommand($"UPDATE bookings SET status = '{status}' WHERE booking_id = {bookingId}", conn, transaction)
                cmdBooking.ExecuteNonQuery()

                transaction.Commit()
                MessageBox.Show($"Booking has been successfully {status.ToLower()}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LogActivity($"Processed booking ID {bookingId} with status: {status}")

                If status = "Approved" Then
                    RejectOtherRequestsForFullRoom(roomIdToUpdate, bookingId)
                    RejectOtherRequestsFromTenant(tenantIdApproved, bookingId)
                End If
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("The operation failed. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub btnApproveBooking_Click(sender As Object, e As EventArgs) Handles btnApproveBooking.Click
        If dgvPendingBookings.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a booking to approve.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim selectedBookingId As Integer = Convert.ToInt32(dgvPendingBookings.SelectedRows(0).Cells("Booking ID").Value)
        ProcessBooking(selectedBookingId, "Approved")
        LoadPendingBookings()
        LoadAvailableTenantsForComboBox()
    End Sub

    Private Sub btnRejectBooking_Click(sender As Object, e As EventArgs) Handles btnRejectBooking.Click
        If dgvPendingBookings.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a booking to reject.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim selectedBookingId As Integer = Convert.ToInt32(dgvPendingBookings.SelectedRows(0).Cells("Booking ID").Value)
        ProcessBooking(selectedBookingId, "Rejected")
        LoadPendingBookings()
        LoadAvailableTenantsForComboBox()
    End Sub

    Private Sub btnFilterRooms_Click(sender As Object, e As EventArgs) Handles btnFilterRooms.Click
        Dim dormId As Integer = 0
        If cboFilterDormitory.SelectedIndex > 0 AndAlso cboFilterDormitory.SelectedValue IsNot Nothing Then
            dormId = Convert.ToInt32(cboFilterDormitory.SelectedValue)
        End If

        Dim capacity As Integer = CInt(nudFilterCapacity.Value)
        Dim price As Decimal = nudFilterMaxPrice.Value

        PopulateRoomCards(dormId, capacity, price)
    End Sub

    Private Sub btnResetFilter_Click(sender As Object, e As EventArgs) Handles btnResetFilter.Click
        cboFilterDormitory.SelectedIndex = 0
        nudFilterCapacity.Value = 1
        nudFilterMaxPrice.Value = 0

        PopulateRoomCards()
    End Sub

    Private Sub dgvAvailableRooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAvailableRooms.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvAvailableRooms.Rows(e.RowIndex)
            Dim imageResourceName As String = If(row.Cells("image_path").Value IsNot DBNull.Value, row.Cells("image_path").Value.ToString(), "")
            picAvailableRoom.Image = Nothing
            If Not String.IsNullOrWhiteSpace(imageResourceName) Then
                picAvailableRoom.Image = CType(My.Resources.ResourceManager.GetObject(imageResourceName), Image)
            End If
            lblRoomDescriptionPreview.Text = row.Cells("Description").Value.ToString()
        End If
    End Sub

    Private Sub btnRequestBooking_Click(sender As Object, e As EventArgs) Handles btnRequestBooking.Click
        If dgvAvailableRooms.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a room from the list to book.", "No Room Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If UserHasActiveBooking(loggedInUserId) Then
            MessageBox.Show("You already have a pending or approved booking. You cannot request another room at this time.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim confirmResult = MessageBox.Show("Are you sure you want to send a booking request for this room?", "Confirm Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.No Then
            Return
        End If

        Dim selectedRoomIdToBook As Integer = Convert.ToInt32(dgvAvailableRooms.SelectedRows(0).Cells("Room ID").Value)
        Dim moveInDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"INSERT INTO bookings (tenant_id, room_id, move_in_date, status) VALUES ({loggedInUserId}, {selectedRoomIdToBook}, '{moveInDate}', 'Pending')"
            Using cmd As New MySqlCommand(sql, conn)
                conn.Open()
                cmd.ExecuteNonQuery()
                MessageBox.Show("Your booking request has been sent successfully!", "Request Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnResetFilter_Click(Nothing, Nothing)

                LoadMyAccountData()
            End Using
        End Using
    End Sub

    Private Sub LoadPendingBookings()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT b.booking_id AS 'Booking ID', CONCAT(u.first_name, ' ', u.last_name) AS 'Tenant', d.name AS 'Dormitory', r.room_number AS 'Room Number', b.booking_date AS 'Booking Date' FROM bookings b JOIN users u ON b.tenant_id = u.user_id JOIN rooms r ON b.room_id = r.room_id JOIN dormitories d ON r.dormitory_id = d.dormitory_id WHERE b.status = 'Pending' ORDER BY b.booking_date"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvPendingBookings.DataSource = dt
        End Using
    End Sub

    Private Sub LoadExpenses()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT e.expense_id AS 'Expense ID', e.description AS 'Description', e.amount AS 'Amount', e.category AS 'Category', e.expense_date AS 'Expense Date', CONCAT(u.first_name, ' ', u.last_name) AS 'Recorded By' FROM expenses e JOIN users u ON e.recorded_by = u.user_id ORDER BY e.expense_date DESC"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvExpenses.DataSource = dt
            Dim totalExpenses As Decimal = 0
            For Each row As DataRow In dt.Rows
                totalExpenses += Convert.ToDecimal(row("Amount"))
            Next
            lblTotalExpenses.Text = $"Total Expenses: {totalExpenses:C}"
        End Using

        If dgvExpenses.Rows.Count > 0 Then
            dgvExpenses.Rows(0).Selected = True
            dgvExpensess_CellClick(dgvExpenses, New DataGridViewCellEventArgs(0, 0))
        End If
    End Sub

    Private Sub dgvExpensess_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvExpenses.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvExpenses.Rows(e.RowIndex)
            selectedExpenseId = Convert.ToInt32(row.Cells("Expense ID").Value)
            txtExpenseDescription.Text = row.Cells("Description").Value.ToString()
            nudExpenseAmount.Value = Convert.ToDecimal(row.Cells("Amount").Value)
            cboExpenseCategory.SelectedItem = row.Cells("Category").Value.ToString()
            dtpExpenseDate.Value = Convert.ToDateTime(row.Cells("Expense Date").Value)
        End If
    End Sub

    Private Sub btnClearExpense_Click(sender As Object, e As EventArgs) Handles btnClearExpense.Click
        selectedExpenseId = 0
        txtExpenseDescription.Clear()
        nudExpenseAmount.Value = 0
        cboExpenseCategory.SelectedIndex = -1
        dtpExpenseDate.Value = DateTime.Now
        dgvExpenses.ClearSelection()
    End Sub

    Private Sub btnSaveExpense_Click(sender As Object, e As EventArgs) Handles btnSaveExpense.Click
        If String.IsNullOrWhiteSpace(txtExpenseDescription.Text) OrElse nudExpenseAmount.Value <= 0 OrElse cboExpenseCategory.SelectedItem Is Nothing Then
            MessageBox.Show("Description, a valid amount, and category are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim description As String = txtExpenseDescription.Text.Trim()
        Dim amount As Decimal = nudExpenseAmount.Value
        Dim category As String = cboExpenseCategory.SelectedItem.ToString()
        Dim expenseDate As String = dtpExpenseDate.Value.ToString("yyyy-MM-dd")

        Dim sql As String
        If selectedExpenseId = 0 Then
            sql = $"INSERT INTO expenses (description, amount, category, expense_date, recorded_by) VALUES ('{description}', {amount}, '{category}', '{expenseDate}', {loggedInUserId})"
        Else
            sql = $"UPDATE expenses SET description = '{description}', amount = {amount}, category = '{category}', expense_date = '{expenseDate}' WHERE expense_id = {selectedExpenseId}"
        End If

        Using conn As New MySqlConnection(ConnectionString)
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Expense saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Failed to save expense. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        LoadExpenses()
        btnClearExpense_Click(Nothing, Nothing)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not isLoggingOut Then
            Application.Exit()
        End If
    End Sub

    Private Sub btnDeleteDormitory_Click(sender As Object, e As EventArgs) Handles btnDeleteDormitory.Click
        If selectedDormitoryId = 0 Then
            MessageBox.Show("Please select a dormitory from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim confirmResult = MessageBox.Show("Are you sure you want to delete this dormitory? This may fail if it has associated rooms.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If confirmResult = DialogResult.No Then Return

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"DELETE FROM dormitories WHERE dormitory_id = {selectedDormitoryId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Dormitory deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As MySqlException
                    MessageBox.Show("Could not delete dormitory. Remove its rooms first. Error: " & ex.Message, "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        LoadDormitoriesForGrid()
        btnClearDormitory_Click(Nothing, Nothing)
    End Sub

    Private Sub btnDeleteRoom_Click(sender As Object, e As EventArgs) Handles btnDeleteRoom.Click
        If selectedRoomId = 0 Then
            MessageBox.Show("Please select a room from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim confirmResult = MessageBox.Show("Are you sure you want to delete this room?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If confirmResult = DialogResult.No Then Return

        Dim deletedRoomInfo As String = txtRoomNumber.Text

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"DELETE FROM rooms WHERE room_id = {selectedRoomId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Room deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LogActivity($"Deleted room: {deletedRoomInfo} (ID: {selectedRoomId}).")
                Catch ex As MySqlException
                    MessageBox.Show("Could not delete room. It may have active bookings. Error: " & ex.Message, "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        Dim currentDormId As Integer = Convert.ToInt32(cboSelectDormitory.SelectedValue)
        LoadRooms(currentDormId)
        btnClearRoom_Click(Nothing, Nothing)
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        If selectedUserId = 0 Then
            MessageBox.Show("Please select a user from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If selectedUserId = loggedInUserId Then
            MessageBox.Show("You cannot delete your own account while you are logged in.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim confirmResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If confirmResult = DialogResult.No Then Return

        Dim deletedUsername As String = txtUsername.Text

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"DELETE FROM users WHERE user_id = {selectedUserId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LogActivity($"Deleted user: {deletedUsername} (ID: {selectedUserId}).")
                Catch ex As MySqlException
                    MessageBox.Show("Could not delete user. They may have active records. Error: " & ex.Message, "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        LoadUsers()
        ResetUserForm()
    End Sub

    Private Sub btnDeleteExpense_Click(sender As Object, e As EventArgs) Handles btnDeleteExpense.Click
        If selectedExpenseId = 0 Then
            MessageBox.Show("Please select an expense from the list to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim confirmResult = MessageBox.Show("Are you sure you want to delete this expense record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If confirmResult = DialogResult.No Then Return

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"DELETE FROM expenses WHERE expense_id = {selectedExpenseId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Expense deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Failed to delete expense. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
        LoadExpenses()
        btnClearExpense_Click(Nothing, Nothing)
    End Sub

    Private Sub btnRecordPayment_Click(sender As Object, e As EventArgs) Handles btnRecordPayment.Click
        If dgvBillingHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an unpaid bill to record a payment for.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row As DataGridViewRow = dgvBillingHistory.SelectedRows(0)
        Dim billingId As Integer = Convert.ToInt32(row.Cells("Billing ID").Value)
        Dim amountPaid As Decimal = Convert.ToDecimal(row.Cells("Amount Due").Value)

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()
            Try
                Dim paymentDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim sqlPayment As String = $"INSERT INTO payments (billing_id, amount_paid, payment_date, payment_method, recorded_by) VALUES ({billingId}, {amountPaid}, '{paymentDate}', 'Cash', {loggedInUserId})"
                Using cmdPayment As New MySqlCommand(sqlPayment, conn, transaction)
                    cmdPayment.ExecuteNonQuery()
                End Using

                Dim sqlBillUpdate As String = $"UPDATE billings SET status = 'Paid' WHERE billing_id = {billingId}"
                Using cmdBill As New MySqlCommand(sqlBillUpdate, conn, transaction)
                    cmdBill.ExecuteNonQuery()
                End Using

                transaction.Commit()
                MessageBox.Show("Payment recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadBillingHistory()

                LogActivity($"Recorded payment of {amountPaid:C} for bill ID: {billingId}.")
            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Failed to record payment. Operation cancelled. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub rbFilter_CheckedChanged(sender As Object, e As EventArgs) Handles rbShowUnpaid.CheckedChanged, rbShowPaid.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then
            LoadBillingHistory()
            btnRecordPayment.Enabled = rbShowUnpaid.Checked
            btnPrintReceipt.Enabled = rbShowPaid.Checked

            btnDeleteBill.Enabled = rbShowUnpaid.Checked
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim selectedTab As TabPage = TabControl1.SelectedTab

        If selectedTab Is tpDashboard Then
            LoadDashboardData()
            RefreshWelcomeMessage()
        End If
        If selectedTab Is tpDormitoryManagement Then
            LoadDormitoriesForGrid()
        ElseIf selectedTab Is tpRoomManagement Then
            LoadDormitoriesForComboBox()
            PopulateImageComboBox()
            LoadAvailableTenantsForComboBox()
            HandleDormitorySelectionChange()
        ElseIf selectedTab Is tpUserManagement Then
            LoadUsers()
        ElseIf selectedTab Is tpBookingManagement Then
            LoadPendingBookings()
        ElseIf selectedTab Is tpBillingManagement Then
            LoadTenantsToBill()
            LoadBillingHistory()
        ElseIf selectedTab Is tpExpenses Then
            LoadExpenses()
        ElseIf selectedTab Is tpManagerReports Then
        ElseIf selectedTab Is tpTenantBooking Then
            PopulateRoomCards()
            btnResetFilter_Click(Nothing, Nothing)
        ElseIf selectedTab Is tpMyAccount Then
            LoadMyAccountData()
        ElseIf selectedTab Is tpActivityLog Then
            LoadActivityLog()
        End If

        Me.Text = TabControl1.SelectedTab.Text
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        Dim newPass As String = txtMyNewPassword.Text
        Dim confirmPass As String = txtMyConfirmPassword.Text

        If String.IsNullOrWhiteSpace(newPass) Then
            MessageBox.Show("New password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If newPass <> confirmPass Then
            MessageBox.Show("The new passwords do not match. Please try again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"UPDATE users SET password_hash = '{newPass}' WHERE user_id = {loggedInUserId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Your password has been changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtMyNewPassword.Clear()
                    txtMyConfirmPassword.Clear()
                Catch ex As Exception
                    MessageBox.Show("Failed to change password. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles dashLogOut.Click, dormLogOut.Click, roomLogOut.Click, userLogOut.Click, billLogOut.Click, expLogOut.Click, finLogOut.Click, logLogOut.Click, bookLogOut.Click, acctLogOut.Click
        Dim confirmResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.Yes Then
            isLoggingOut = True

            Dim loginForm As New frmLogin()
            loginForm.Show()

            Me.Close()
        End If
    End Sub

    Private Sub HandleDormitorySelectionChange()
        Dim baseSql As String = "SELECT r.room_id AS 'Room ID', r.room_number AS 'Room Number', " &
                           "CONCAT(IFNULL(b_count.approved_bookings, 0), '/', r.capacity) AS 'Capacity', " &
                           "r.rent_rate AS 'Rent Rate', " &
                           "CASE WHEN r.status_override = 'Under Maintenance' THEN 'Under Maintenance' WHEN IFNULL(b_count.approved_bookings, 0) >= r.capacity THEN 'Occupied' ELSE 'Available' END AS 'Status', " &
                           "r.description, r.image_path, r.status_override FROM rooms r " &
                           "LEFT JOIN (SELECT room_id, COUNT(*) AS approved_bookings FROM bookings WHERE status = 'Approved' GROUP BY room_id) AS b_count " &
                           "ON r.room_id = b_count.room_id"

        Dim whereClauses As New List(Of String)()

        If cboSelectDormitory.SelectedIndex > 0 Then
            whereClauses.Add($"r.dormitory_id = {cboSelectDormitory.SelectedValue}")
        End If

        If cboFilterRoomStatus.SelectedIndex > 0 AndAlso cboFilterRoomStatus.SelectedItem.ToString() <> "-- All Statuses --" Then
            Dim selectedStatus As String = cboFilterRoomStatus.SelectedItem.ToString()
            If selectedStatus = "Available" Then
                whereClauses.Add("(r.status_override <> 'Under Maintenance' AND (b_count.approved_bookings IS NULL OR b_count.approved_bookings < r.capacity))")
            ElseIf selectedStatus = "Occupied" Then
                whereClauses.Add("(r.status_override <> 'Under Maintenance' AND (b_count.approved_bookings IS NOT NULL AND b_count.approved_bookings >= r.capacity))")
            ElseIf selectedStatus = "Under Maintenance" Then
                whereClauses.Add("r.status_override = 'Under Maintenance'")
            End If
        End If

        Dim finalSql As String = baseSql
        If whereClauses.Count > 0 Then
            finalSql &= " WHERE " & String.Join(" AND ", whereClauses)
        End If
        finalSql &= " ORDER BY r.room_number"

        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(finalSql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvRooms.DataSource = dt
            If dgvRooms.Columns.Contains("image_path") Then dgvRooms.Columns("image_path").Visible = False
            If dgvRooms.Columns.Contains("description") Then dgvRooms.Columns("description").Visible = False
            If dgvRooms.Columns.Contains("status_override") Then dgvRooms.Columns("status_override").Visible = False
        End Using

        If dgvRooms.Rows.Count > 0 Then
            dgvRooms.Rows(0).Selected = True
            dgvRooms_CellClick(dgvRooms, New DataGridViewCellEventArgs(0, 0))
        Else
            btnClearRoom_Click(Nothing, Nothing)
        End If
    End Sub

    Private Function UserHasActiveBooking(userId As Integer) As Boolean
        Dim latestStatus As String = ""

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"SELECT status FROM bookings WHERE tenant_id = {userId} ORDER BY booking_date DESC LIMIT 1"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()

                    If result Is Nothing OrElse result Is DBNull.Value Then
                        Return False
                    End If

                    latestStatus = result.ToString()

                Catch ex As Exception
                    MessageBox.Show("Database error while checking for active bookings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return True
                End Try
            End Using
        End Using

        If latestStatus = "Pending" OrElse latestStatus = "Approved" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub LoadAvailableTenantsForComboBox()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT u.user_id, CONCAT(u.first_name, ' ', u.last_name, ' (', u.username, ')', CASE WHEN b.status = 'Approved' THEN ' - Occupied' ELSE '' END) AS FullName FROM users u LEFT JOIN bookings b ON u.user_id = b.tenant_id AND b.status = 'Approved' WHERE u.role = 'Tenant' ORDER BY u.last_name"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Dim placeholderRow As DataRow = dt.NewRow()
            placeholderRow("user_id") = 0
            placeholderRow("FullName") = "-- Select a Tenant --"
            dt.Rows.InsertAt(placeholderRow, 0)

            cboAssignTenant.DataSource = dt
            cboAssignTenant.DisplayMember = "FullName"
            cboAssignTenant.ValueMember = "user_id"
        End Using
    End Sub

    Private Sub btnAssignTenant_Click(sender As Object, e As EventArgs) Handles btnAssignTenant.Click
        If selectedRoomId = 0 Then
            MessageBox.Show("Please select an available room from the list first.", "No Room Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cboAssignTenant.SelectedIndex <= 0 Then
            MessageBox.Show("Please select a tenant to assign.", "No Tenant Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If dgvRooms.SelectedRows(0).Cells("Status").Value.ToString() <> "Available" Then
            MessageBox.Show("This room is not available. You can only assign tenants to available rooms.", "Room Not Available", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim tenantIdToAssign As Integer = Convert.ToInt32(cboAssignTenant.SelectedValue)
        Dim roomNumberInfo As String = dgvRooms.SelectedRows(0).Cells("Room Number").Value.ToString()

        Dim confirmResult = MessageBox.Show($"Are you sure you want to assign this tenant to room {roomNumberInfo}?", "Confirm Assignment", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.No Then Return

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim transaction As MySqlTransaction = conn.BeginTransaction()
            Try
                ClearExistingOccupancy(tenantIdToAssign, transaction)

                Dim moveInDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                Dim sqlBooking As String = $"INSERT INTO bookings (tenant_id, room_id, move_in_date, status) VALUES ({tenantIdToAssign}, {selectedRoomId}, '{moveInDate}', 'Approved')"
                Using cmdBooking As New MySqlCommand(sqlBooking, conn, transaction)
                    cmdBooking.ExecuteNonQuery()
                End Using

                transaction.Commit()
                MessageBox.Show("Tenant successfully assigned to the room.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LogActivity($"Manually assigned tenant ID {tenantIdToAssign} to room ID {selectedRoomId}.")

            Catch ex As Exception
                transaction.Rollback()
                MessageBox.Show("Failed to assign tenant. The operation was cancelled. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        Dim currentDormId As Integer = Convert.ToInt32(cboSelectDormitory.SelectedValue)
        LoadRooms(currentDormId)
        LoadAvailableTenantsForComboBox()
        btnClearRoom_Click(Nothing, Nothing)
        HandleDormitorySelectionChange()
    End Sub

    Private Sub btnRemoveTenant_Click(sender As Object, e As EventArgs) Handles btnRemoveTenant.Click
        If selectedRoomId = 0 Then
            MessageBox.Show("Please select an occupied room from the list first.", "No Room Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cboTenantsInRoom.SelectedIndex <= 0 OrElse cboTenantsInRoom.SelectedValue Is Nothing Then
            MessageBox.Show("Please select a tenant from the list to remove.", "No Tenant Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim tenantIdToRemove As Integer = Convert.ToInt32(cboTenantsInRoom.SelectedValue)
        Dim roomNumberInfo As String = dgvRooms.SelectedRows(0).Cells("Room Number").Value.ToString()
        Dim tenantNameInfo As String = cboTenantsInRoom.Text

        Dim confirmResult = MessageBox.Show($"Are you sure you want to remove {tenantNameInfo} from room {roomNumberInfo}? This will complete their booking record.", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirmResult = DialogResult.No Then Return

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Try
                Dim sqlBooking As String = $"UPDATE bookings SET status = 'Completed' WHERE room_id = {selectedRoomId} AND tenant_id = {tenantIdToRemove} AND status = 'Approved'"
                Using cmdBooking As New MySqlCommand(sqlBooking, conn)
                    cmdBooking.ExecuteNonQuery()
                End Using
                MessageBox.Show("Tenant successfully removed from the room.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LogActivity($"Manually removed tenant ID {tenantIdToRemove} from room ID {selectedRoomId}.")
            Catch ex As Exception
                MessageBox.Show("Failed to remove tenant. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        Dim currentDormId As Integer = Convert.ToInt32(cboSelectDormitory.SelectedValue)
        LoadRooms(currentDormId)
        LoadAvailableTenantsForComboBox()
        btnClearRoom_Click(Nothing, Nothing)
        HandleDormitorySelectionChange()
    End Sub

    Private Sub btnDeleteBill_Click(sender As Object, e As EventArgs) Handles btnDeleteBill.Click
        If dgvBillingHistory.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select an unpaid bill to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If dgvBillingHistory.SelectedRows(0).Cells("Status").Value.ToString() <> "Unpaid" Then
            MessageBox.Show("You can only delete bills that are unpaid.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim selectedBillingId As Integer = Convert.ToInt32(dgvBillingHistory.SelectedRows(0).Cells("Billing ID").Value)

        Dim confirmResult = MessageBox.Show("Are you sure you want to permanently delete this bill? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If confirmResult = DialogResult.No Then Return

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"DELETE FROM billings WHERE billing_id = {selectedBillingId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Bill deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LogActivity($"Deleted unpaid bill ID: {selectedBillingId}.")
                Catch ex As MySqlException
                    MessageBox.Show("Could not delete the bill. Error: " & ex.Message, "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using

        LoadBillingHistory()
    End Sub

    Private Sub PopulateImageComboBox()
        cboSelectImage.Items.Clear()
        cboSelectImage.Items.Add("-- No Image --")

        Dim resourceProperties = GetType(My.Resources.Resources).GetProperties(BindingFlags.Static Or BindingFlags.NonPublic Or BindingFlags.Public)

        For Each prop In resourceProperties
            If prop.PropertyType Is GetType(System.Drawing.Bitmap) AndAlso prop.Name.StartsWith("room_") Then
                cboSelectImage.Items.Add(prop.Name)
            End If
        Next

        cboSelectImage.SelectedIndex = 0
    End Sub

    Private Sub btnClearImage_Click(sender As Object, e As EventArgs) Handles btnClearImage.Click
        If cboSelectImage.Items.Count > 0 Then
            cboSelectImage.SelectedIndex = 0
        End If
        picRoomImage.Image = Nothing
    End Sub

    Private Sub cboSelectImage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSelectImage.SelectedIndexChanged
        Dim selectedResourceName As String = cboSelectImage.SelectedItem.ToString()

        If cboSelectImage.SelectedIndex <= 0 OrElse selectedResourceName = "-- No Image --" Then
            picRoomImage.Image = Nothing
        Else
            Try
                picRoomImage.Image = CType(My.Resources.ResourceManager.GetObject(selectedResourceName), Image)
            Catch ex As Exception
                MessageBox.Show("Could not load the selected image resource.", "Resource Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                picRoomImage.Image = Nothing
            End Try
        End If
    End Sub

    Private Sub cboFilterRoomStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilterRoomStatus.SelectedIndexChanged
        HandleDormitorySelectionChange()
    End Sub

    Private Sub LogActivity(actionDescription As String)
        Dim sanitizedDescription As String = actionDescription.Replace("'", "''")

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"INSERT INTO activity_logs (user_id, action_description) VALUES ({loggedInUserId}, '{sanitizedDescription}')"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine("Failed to write to activity log: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadActivityLog()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = "SELECT u.username AS 'User', CONCAT(u.first_name, ' ', u.last_name) AS 'Full Name', al.action_description AS 'Action', al.log_timestamp AS 'Timestamp' FROM activity_logs al JOIN users u ON al.user_id = u.user_id ORDER BY al.log_timestamp DESC"

            Dim adapter As New MySqlDataAdapter(sql, conn)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            dgvActivityLog.DataSource = dt
        End Using
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnNewUser.Click
        ResetUserForm()
    End Sub

    Private Sub RejectOtherRequestsForFullRoom(roomId As Integer, approvedBookingId As Integer)

        Dim capacity As Integer = 0
        Dim currentOccupancy As Integer = 0
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Dim cmdCap As New MySqlCommand($"SELECT capacity FROM rooms WHERE room_id = {roomId}", conn)
            capacity = Convert.ToInt32(cmdCap.ExecuteScalar())
            Dim cmdCount As New MySqlCommand($"SELECT COUNT(*) FROM bookings WHERE room_id = {roomId} AND status = 'Approved'", conn)
            currentOccupancy = Convert.ToInt32(cmdCount.ExecuteScalar())
        End Using

        If currentOccupancy >= capacity Then
            Using conn As New MySqlConnection(ConnectionString)
                Dim sqlReject As String = $"UPDATE bookings SET status = 'Rejected' WHERE room_id = {roomId} AND status = 'Pending' AND booking_id <> {approvedBookingId}"
                Dim cmdReject As New MySqlCommand(sqlReject, conn)
                conn.Open()
                cmdReject.ExecuteNonQuery()
                LogActivity($"Automatically rejected pending requests for now-full room ID: {roomId}.")
            End Using
        End If
    End Sub

    Private Sub RejectOtherRequestsFromTenant(tenantId As Integer, approvedBookingId As Integer)
        Using conn As New MySqlConnection(ConnectionString)
            Dim sqlReject As String = $"UPDATE bookings SET status = 'Rejected' WHERE tenant_id = {tenantId} AND status = 'Pending' AND booking_id <> {approvedBookingId}"
            Dim cmdReject As New MySqlCommand(sqlReject, conn)
            conn.Open()
            cmdReject.ExecuteNonQuery()
            LogActivity($"Automatically rejected other pending requests for tenant ID: {tenantId}.")
        End Using
    End Sub

    Private Function ClearExistingOccupancy(tenantId As Integer, transaction As MySqlTransaction) As Boolean
        Dim existingBookingId As Object = Nothing
        Dim oldRoomId As Object = Nothing
        Using cmdFind As New MySqlCommand($"SELECT booking_id, room_id FROM bookings WHERE tenant_id = {tenantId} AND status = 'Approved' LIMIT 1", transaction.Connection, transaction)
            Using reader = cmdFind.ExecuteReader()
                If reader.Read() Then
                    existingBookingId = reader("booking_id")
                    oldRoomId = reader("room_id")
                End If
            End Using
        End Using

        If existingBookingId IsNot Nothing Then
            Dim sqlCompleteOld As String = $"UPDATE bookings SET status = 'Completed' WHERE booking_id = {existingBookingId}"
            Using cmdComplete As New MySqlCommand(sqlCompleteOld, transaction.Connection, transaction)
                cmdComplete.ExecuteNonQuery()
                LogActivity($"Completed old booking {existingBookingId} for tenant {tenantId} as part of a room transfer.")
            End Using
            Return True
        End If
        Return False
    End Function

    Private Sub SetAdminButtonLocations()
        Dim buttonMappings As New Dictionary(Of Point, Control()) From {
            {New Point(32, 155), {dashBtnNavDashboard, dormBtnNavDashboard, roomBtnNavDashboard, userBtnNavDashboard, billBtnNavDashboard, expBtnNavDashboard, finBtnNavDashboard, logBtnNavDashboard}},
            {New Point(36, 221), {dashBtnNavDormitories, dormBtnNavDormitories, roomBtnNavDormitories, userBtnNavDormitories, billBtnNavDormitories, expBtnNavDormitories, finBtnNavDormitories, logBtnNavDormitories}},
            {New Point(36, 300), {dashBtnNavRooms, dormBtnNavRooms, roomBtnNavRooms, userBtnNavRooms, billBtnNavRooms, expBtnNavRooms, finBtnNavRooms, logBtnNavRooms}},
            {New Point(36, 375), {dashBtnNavUsers, dormBtnNavUsers, roomBtnNavUsers, userBtnNavUsers, billBtnNavUsers, expBtnNavUsers, finBtnNavUsers, logBtnNavUsers}},
            {New Point(36, 444), {dashBtnNavBilling, dormBtnNavBilling, roomBtnNavBilling, userBtnNavBilling, billBtnNavBilling, expBtnNavBilling, finBtnNavBilling, logBtnNavBilling}},
            {New Point(36, 522), {dashBtnNavFinancialReports, dormBtnNavFinancialReports, roomBtnNavFinancialReports, userBtnNavFinancialReports, billBtnNavFinancialReports, expBtnNavFinancialReports, finBtnNavFinancialReports, logBtnNavFinancialReports}},
            {New Point(36, 596), {dashBtnNavExpenses, dormBtnNavExpenses, roomBtnNavExpenses, userBtnNavExpenses, billBtnNavExpenses, expBtnNavExpenses, finBtnNavExpenses, logBtnNavExpenses}},
            {New Point(36, 681), {dashBtnNavActivityLog, dormBtnNavActivityLog, roomBtnNavActivityLog, userBtnNavActivityLog, billBtnNavActivityLog, expBtnNavActivityLog, finBtnNavActivityLog, logBtnNavActivityLog}}
        }

        For Each mapping In buttonMappings
            Dim targetLocation As Point = mapping.Key
            Dim buttonsToMove As Control() = mapping.Value

            For Each btn In buttonsToMove
                btn.Location = targetLocation
            Next
        Next
    End Sub

    Private Sub SetManagerButtonsLocations()
        Dim locationMap As New Dictionary(Of Point, Control()) From {
            {New Point(32, 155), {dashBtnNavDashboard, roomBtnNavDashboard, billBtnNavDashboard, expBtnNavDashboard}},
            {New Point(33, 237), {dashBtnNavRooms, roomBtnNavRooms, billBtnNavRooms, expBtnNavRooms}},
            {New Point(34, 310), {dashBtnNavBilling, roomBtnNavBilling, billBtnNavBilling, expBtnNavBilling}},
            {New Point(35, 400), {dashBtnNavExpenses, roomBtnNavExpenses, billBtnNavExpenses, expBtnNavExpenses}}
        }

        For Each item In locationMap
            Dim location As Point = item.Key
            Dim buttons As Control() = item.Value

            For Each btn In buttons
                btn.Location = location
            Next
        Next
    End Sub

    Private Sub SetTenantButtonsLocations()
        Dim locationMap As New Dictionary(Of Point, Control()) From {
            {New Point(32, 155), {dashBtnNavDashboard, bookBtnNavDashboard, acctBtnNavDashboard}},
            {New Point(36, 230), {dashBtnNavTenantBooking, bookBtnNavTenantBooking, acctBtnNavTenantBooking}},
            {New Point(36, 308), {dashBtnNavMyAccount, bookBtnNavMyAccount, acctBtnNavMyAccount}}
        }

        For Each mapping In locationMap
            Dim targetLocation As Point = mapping.Key
            Dim buttonsToPosition As Control() = mapping.Value

            For Each btn In buttonsToPosition
                btn.Location = targetLocation
            Next
        Next
    End Sub

    Private Sub ViewDetails_Click(sender As Object, e As EventArgs)
        Dim clickedButton = DirectCast(sender, Button)
        Dim roomData = DirectCast(clickedButton.Tag, Tuple(Of Integer, String))
        Dim roomIdToBook As Integer = roomData.Item1
        Dim roomDescription As String = roomData.Item2

        If UserHasActiveBooking(loggedInUserId) Then
            MessageBox.Show("You already have a pending or approved booking. You cannot request another room at this time.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim message As String = $"{roomDescription}{vbCrLf}{vbCrLf}Would you like to send a booking request for this room?"
        Dim confirmResult = MessageBox.Show(message, "Confirm Booking Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            Using conn As New MySqlConnection(ConnectionString)
                Try
                    Dim moveInDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    Dim sql As String = $"INSERT INTO bookings (tenant_id, room_id, move_in_date, status) VALUES ({loggedInUserId}, {roomIdToBook}, '{moveInDate}', 'Pending')"
                    Using cmd As New MySqlCommand(sql, conn)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("Your booking request has been sent successfully!", "Request Sent", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LogActivity($"Tenant ID {loggedInUserId} requested to book room ID {roomIdToBook}.")

                        PopulateRoomCards()
                        LoadMyAccountData()
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Failed to send booking request. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End If
    End Sub

    Private Sub PopulateRoomCards(Optional dormitoryId As Integer = 0, Optional minCapacity As Integer = 0, Optional maxPrice As Decimal = 0)
        flpTenantRooms.Controls.Clear()

        Dim baseSql As String = "SELECT r.room_id, r.room_number, r.capacity, r.rent_rate, r.description, r.image_path, d.name AS dormitory_name, IFNULL(b_count.approved_bookings, 0) AS current_occupants FROM rooms r JOIN dormitories d ON r.dormitory_id = d.dormitory_id LEFT JOIN (SELECT room_id, COUNT(*) AS approved_bookings FROM bookings WHERE status = 'Approved' GROUP BY room_id) AS b_count ON r.room_id = b_count.room_id"

        Dim whereClauses As New List(Of String)()
        whereClauses.Add("r.status_override <> 'Under Maintenance' AND (b_count.approved_bookings IS NULL OR b_count.approved_bookings < r.capacity)")

        If dormitoryId > 0 Then
            whereClauses.Add($"r.dormitory_id = {dormitoryId}")
        End If
        If minCapacity > 0 Then
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
                flpTenantRooms.Controls.Add(lblNoResults)
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

                Dim roomId As Integer = CInt(row("room_id"))
                Dim description As String = row("description").ToString()
                btnViewDetails.Tag = New Tuple(Of Integer, String)(roomId, description)

                AddHandler btnViewDetails.Click, AddressOf ViewDetails_Click

                card.Controls.Add(btnViewDetails)
                card.Controls.Add(lblBeds)
                card.Controls.Add(lblPrice)
                card.Controls.Add(lblRoomNumber)
                card.Controls.Add(lblDormName)
                card.Controls.Add(pic)

                flpTenantRooms.Controls.Add(card)
            Next
        End Using
    End Sub

    Private Sub LoadMyBillingHistory(Optional startDate As DateTime? = Nothing, Optional endDate As DateTime? = Nothing)
        Using conn As New MySqlConnection(ConnectionString)
            Dim sqlBills As String = $"SELECT due_date AS 'Due Date', amount_due AS 'Amount Due', status AS 'Status', created_at AS 'Bill Generated On' FROM billings WHERE tenant_id = {loggedInUserId}"

            If startDate.HasValue AndAlso endDate.HasValue Then
                Dim startDateString As String = startDate.Value.ToString("yyyy-MM-dd 00:00:00")
                Dim endDateString As String = endDate.Value.ToString("yyyy-MM-dd 23:59:59")
                sqlBills &= $" AND due_date BETWEEN '{startDateString}' AND '{endDateString}'"
            End If

            sqlBills &= " ORDER BY due_date DESC"

            Dim adapterBills As New MySqlDataAdapter(sqlBills, conn)
            Dim dt As New DataTable()
            adapterBills.Fill(dt)
            dgvMyBills.DataSource = dt

            If dt.Rows.Count = 0 AndAlso startDate.HasValue Then
                MessageBox.Show("No billing records found for the selected date range.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub btnFilterMyBills_Click(sender As Object, e As EventArgs) Handles btnFilterMyBills.Click
        Dim sDate As DateTime = dtpMyBillsStart.Value
        Dim eDate As DateTime = dtpMyBillsEnd.Value

        LoadMyBillingHistory(sDate, eDate)
    End Sub

    Private Sub btnResetMyBillsFilter_Click(sender As Object, e As EventArgs) Handles btnResetMyBillsFilter.Click
        LoadMyBillingHistory()
    End Sub

    Private Sub RefreshWelcomeMessage()
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"SELECT first_name, last_name FROM users WHERE user_id = {loggedInUserId}"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    Dim reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim firstName As String = reader("first_name").ToString()
                        Dim lastName As String = reader("last_name").ToString()

                        dashWelcome.Text = $"Welcome, {firstName} {lastName}!"
                    End If
                Catch ex As Exception
                    Console.WriteLine("Failed to refresh welcome message: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    Private Sub txtDashboardSearch_TextChanged(sender As Object, e As EventArgs) Handles txtDashboardSearch.TextChanged
        Dim dt As DataTable = TryCast(dgvDashboardView.DataSource, DataTable)

        If dt Is Nothing Then
            Return
        End If

        Dim searchText As String = txtDashboardSearch.Text.Trim().Replace("'", "''")

        If String.IsNullOrEmpty(searchText) Then
            dt.DefaultView.RowFilter = String.Empty
            Return
        End If

        Dim filterColumn As String = ""

        Select Case currentDashboardViewType
            Case "Pending Requests", "Users List", "Unpaid Bills"
                If currentDashboardViewType = "Pending Requests" Or currentDashboardViewType = "Unpaid Bills" Then
                    filterColumn = "[Tenant]"
                Else
                    filterColumn = "[Full Name]"
                End If

            Case "Available Rooms", "Occupancy List"
                filterColumn = "[Room Number]"
            Case "Monthly Payments"
                filterColumn = "[Tenant]"
            Case Else
                dt.DefaultView.RowFilter = String.Empty
                Return
        End Select

        Dim filterString As String = $"{filterColumn} LIKE '%{searchText}%'"

        Try
            dt.DefaultView.RowFilter = filterString
        Catch ex As Exception
            Console.WriteLine("Error applying RowFilter: " & ex.Message)
            dt.DefaultView.RowFilter = String.Empty
        End Try
    End Sub

    Private Sub txtSearchDormitories_TextChanged(sender As Object, e As EventArgs) Handles txtSearchDormitories.TextChanged
        Try
            Dim dt As DataTable = CType(dgvDormitories.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchDormitories.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"[Dormitory Name] LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchRooms_TextChanged(sender As Object, e As EventArgs) Handles txtSearchRooms.TextChanged
        Try
            Dim dt As DataTable = CType(dgvRooms.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchRooms.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"CONVERT([Room Number], 'System.String') LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchUsers_TextChanged(sender As Object, e As EventArgs) Handles txtSearchUsers.TextChanged
        Try
            Dim dt As DataTable = CType(dgvUsers.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchUsers.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"Username LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchTenantsToBill_TextChanged(sender As Object, e As EventArgs) Handles txtSearchTenantsToBill.TextChanged
        Try
            Dim dt As DataTable = CType(dgvTenantsToBill.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchTenantsToBill.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"Tenant LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchBillingHistory_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBillingHistory.TextChanged
        Try
            Dim dt As DataTable = CType(dgvBillingHistory.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchBillingHistory.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"Tenant LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchFinancialReports_TextChanged(sender As Object, e As EventArgs) Handles txtSearchFinancialReports.TextChanged
        Try
            Dim dt As DataTable = CType(dgvIncomeReport.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchFinancialReports.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"Tenant LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchExpenses_TextChanged(sender As Object, e As EventArgs) Handles txtSearchExpenses.TextChanged
        Try
            Dim dt As DataTable = CType(dgvExpenses.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchExpenses.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"Description LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtSearchActivityLog_TextChanged(sender As Object, e As EventArgs) Handles txtSearchActivityLog.TextChanged
        Try
            Dim dt As DataTable = CType(dgvActivityLog.DataSource, DataTable)
            If dt IsNot Nothing Then
                Dim searchText As String = txtSearchActivityLog.Text.Replace("'", "''")
                If String.IsNullOrWhiteSpace(searchText) Then
                    dt.DefaultView.RowFilter = ""
                Else
                    dt.DefaultView.RowFilter = $"[Full Name] LIKE '%{searchText}%'"
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class