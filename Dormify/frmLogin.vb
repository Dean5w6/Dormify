Imports MySql.Data.MySqlClient

Public Class frmLogin
    Private ReadOnly ConnectionString As String = "Server=127.0.0.1;Uid=root;Pwd=;Database=dormitory_db;"
    Private isLoggingIn As Boolean = False

    Private Function GetConnection() As MySqlConnection
        Return New MySqlConnection(ConnectionString)
    End Function

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrEmpty(username) OrElse String.IsNullOrEmpty(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim conn As MySqlConnection = GetConnection()
        Dim reader As MySqlDataReader = Nothing

        Try
            Dim sql As String = $"SELECT user_id, role, first_name, last_name FROM users WHERE BINARY username = '{username}' AND password_hash = SHA2('{password}', 256) LIMIT 1"
            Dim cmd As New MySqlCommand(sql, conn)

            conn.Open()
            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                Dim userId As Integer = reader.GetInt32("user_id")
                Dim userRole As String = reader.GetString("role")
                Dim firstName As String = reader.GetString("first_name")
                Dim lastName As String = reader.GetString("last_name")

                isLoggingIn = True

                Dim mainDashboard As New Form1()
                mainDashboard.SetWelcomeAndPermissions(userId, username, userRole, firstName, lastName)
                mainDashboard.Show()
                Me.Hide()

            Else
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If reader IsNot Nothing Then reader.Close()
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim regForm As New frmRegister()
        regForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnGuestView_Click(sender As Object, e As EventArgs)
        Dim guestForm As New frmGuestView()
        guestForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnBackToHomeFromLogin_Click(sender As Object, e As EventArgs) Handles btnBackToHomeFromLogin.Click
        Me.Close()
    End Sub

    Private Sub frmLogin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not isLoggingIn Then
            Application.OpenForms("frmHomePage").Show()
        End If
    End Sub

    Private Sub btnTogglePasswordLogin_Click(sender As Object, e As EventArgs) Handles btnTogglePasswordLogin.Click
        If txtPassword.PasswordChar = "*"c Then
            txtPassword.PasswordChar = Chr(0)

            Me.BackgroundImage = My.Resources.eye_open_bg

        Else
            txtPassword.PasswordChar = "*"c
            Me.BackgroundImage = My.Resources.eye_closed_bg
        End If
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.BringToFront()
        Label2.BringToFront()
        txtPassword.Location = New Point(469, 410)
    End Sub

    Private Sub frmLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtUsername.Focus()
        UpdateUsernamePlaceholder()
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtUsername_Enter(sender As Object, e As EventArgs) Handles txtUsername.Enter
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtUsername_Leave(sender As Object, e As EventArgs) Handles txtUsername.Leave
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtPassword_Enter(sender As Object, e As EventArgs) Handles txtPassword.Enter
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtPassword_Leave(sender As Object, e As EventArgs) Handles txtPassword.Leave
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub UpdateUsernamePlaceholder()
        Label1.Visible = String.IsNullOrEmpty(txtUsername.Text) AndAlso Not txtUsername.Focused
    End Sub

    Private Sub UpdatePasswordPlaceholder()
        Label2.Visible = String.IsNullOrEmpty(txtPassword.Text) AndAlso Not txtPassword.Focused
    End Sub

End Class