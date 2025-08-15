Imports MySql.Data.MySqlClient

Public Class frmRegister
    Private ReadOnly ConnectionString As String = "Server=127.0.0.1;Uid=root;Pwd=;Database=dormitory_db;"

    Private Function UsernameExists(username As String) As Boolean
        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"SELECT COUNT(*) FROM users WHERE BINARY username = '{username}'"
            Using cmd As New MySqlCommand(sql, conn)
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    Private Sub btnSubmitRegistration_Click(sender As Object, e As EventArgs) Handles btnSubmitRegistration.Click
        Dim username As String = txtRegUsername.Text.Trim()
        Dim password As String = txtRegPassword.Text.Trim()
        Dim confirmPass As String = txtRegConfirmPassword.Text.Trim()
        Dim firstName As String = txtRegFirstName.Text.Trim()
        Dim lastName As String = txtRegLastName.Text.Trim()
        Dim email As String = txtRegEmail.Text.Trim()
        Dim phone As String = txtRegPhoneNumber.Text.Trim()

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) OrElse
       String.IsNullOrWhiteSpace(confirmPass) OrElse String.IsNullOrWhiteSpace(firstName) OrElse
       String.IsNullOrWhiteSpace(lastName) OrElse String.IsNullOrWhiteSpace(email) OrElse
       String.IsNullOrWhiteSpace(phone) Then
            MessageBox.Show("All fields are required. Please fill out the entire form.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If phone.Length <> 11 OrElse Not phone.StartsWith("09") OrElse Not IsNumeric(phone) Then
            MessageBox.Show("Please enter a valid phone number.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If password <> confirmPass Then
            MessageBox.Show("Passwords do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If password.Length < 8 Then
            MessageBox.Show("Password must be at least 8 characters long.", "Password Too Short", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If UsernameExists(username) Then
            MessageBox.Show("This username is already taken. Please choose another.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using conn As New MySqlConnection(ConnectionString)
            Dim sql As String = $"INSERT INTO users (username, password_hash, first_name, last_name, email, phone_number, role) VALUES ('{username}', SHA2('{password}', 256), '{firstName}', '{lastName}', '{email}', '{phone}', 'Tenant')"
            Using cmd As New MySqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Registration successful! You can now log in with your new account.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    frmLogin.Show()
                Catch ex As MySqlException
                    MessageBox.Show("A database error occurred. It's possible the email is already in use. Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show("An unexpected error occurred. Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub btnBackToHomeFromRegister_Click(sender As Object, e As EventArgs) Handles btnBackToHomeFromRegister.Click
        Me.Close()
        frmHomePage.Show()
    End Sub

    Private Sub btnTogglePasswordRegister_Click(sender As Object, e As EventArgs) Handles btnTogglePasswordRegister.Click, Button1.Click
        If txtRegPassword.PasswordChar = "*"c Then
            txtRegPassword.PasswordChar = Chr(0)
            txtRegConfirmPassword.PasswordChar = Chr(0)
            Me.BackgroundImage = My.Resources.eye_open_bg1
        Else
            txtRegPassword.PasswordChar = "*"c
            txtRegConfirmPassword.PasswordChar = "*"c
            Me.BackgroundImage = My.Resources.eye_closed_bg1
        End If
    End Sub

    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.BringToFront()
        Label2.BringToFront()
        Label3.BringToFront()
        Label4.BringToFront()
        Label5.BringToFront()
        Label6.BringToFront()
        Label7.BringToFront()
    End Sub

    Private Sub frmRegister_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtRegUsername.Focus()
        UpdatePlaceholders()
    End Sub

    Private Sub txtRegUsername_TextChanged(sender As Object, e As EventArgs) Handles txtRegUsername.TextChanged
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtRegUsername_Enter(sender As Object, e As EventArgs) Handles txtRegUsername.Enter
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtRegUsername_Leave(sender As Object, e As EventArgs) Handles txtRegUsername.Leave
        UpdateUsernamePlaceholder()
    End Sub

    Private Sub txtRegPassword_TextChanged(sender As Object, e As EventArgs) Handles txtRegPassword.TextChanged
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtRegPassword_Enter(sender As Object, e As EventArgs) Handles txtRegPassword.Enter
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtRegPassword_Leave(sender As Object, e As EventArgs) Handles txtRegPassword.Leave
        UpdatePasswordPlaceholder()
    End Sub

    Private Sub txtRegConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtRegConfirmPassword.TextChanged
        UpdateConfirmPasswordPlaceholder()
    End Sub

    Private Sub txtRegConfirmPassword_Enter(sender As Object, e As EventArgs) Handles txtRegConfirmPassword.Enter
        UpdateConfirmPasswordPlaceholder()
    End Sub

    Private Sub txtRegConfirmPassword_Leave(sender As Object, e As EventArgs) Handles txtRegConfirmPassword.Leave
        UpdateConfirmPasswordPlaceholder()
    End Sub

    Private Sub txtRegEmail_TextChanged(sender As Object, e As EventArgs) Handles txtRegEmail.TextChanged
        UpdateEmailPlaceholder()
    End Sub

    Private Sub txtRegEmail_Enter(sender As Object, e As EventArgs) Handles txtRegEmail.Enter
        UpdateEmailPlaceholder()
    End Sub

    Private Sub txtRegEmail_Leave(sender As Object, e As EventArgs) Handles txtRegEmail.Leave
        UpdateEmailPlaceholder()
    End Sub

    Private Sub txtRegFirstName_TextChanged(sender As Object, e As EventArgs) Handles txtRegFirstName.TextChanged
        UpdateFirstNamePlaceholder()
    End Sub

    Private Sub txtRegFirstName_Enter(sender As Object, e As EventArgs) Handles txtRegFirstName.Enter
        UpdateFirstNamePlaceholder()
    End Sub

    Private Sub txtRegFirstName_Leave(sender As Object, e As EventArgs) Handles txtRegFirstName.Leave
        UpdateFirstNamePlaceholder()
    End Sub

    Private Sub txtRegLastName_TextChanged(sender As Object, e As EventArgs) Handles txtRegLastName.TextChanged
        UpdateLastNamePlaceholder()
    End Sub

    Private Sub txtRegLastName_Enter(sender As Object, e As EventArgs) Handles txtRegLastName.Enter
        UpdateLastNamePlaceholder()
    End Sub

    Private Sub txtRegLastName_Leave(sender As Object, e As EventArgs) Handles txtRegLastName.Leave
        UpdateLastNamePlaceholder()
    End Sub

    Private Sub txtRegPhoneNumber_TextChanged(sender As Object, e As EventArgs) Handles txtRegPhoneNumber.TextChanged
        UpdatePhoneNumberPlaceholder()
    End Sub

    Private Sub txtRegPhoneNumber_Enter(sender As Object, e As EventArgs) Handles txtRegPhoneNumber.Enter
        UpdatePhoneNumberPlaceholder()
    End Sub

    Private Sub txtRegPhoneNumber_Leave(sender As Object, e As EventArgs) Handles txtRegPhoneNumber.Leave
        UpdatePhoneNumberPlaceholder()
    End Sub

    Private Sub UpdatePlaceholders()
        UpdateUsernamePlaceholder()
        UpdatePasswordPlaceholder()
        UpdateConfirmPasswordPlaceholder()
        UpdateEmailPlaceholder()
        UpdateFirstNamePlaceholder()
        UpdateLastNamePlaceholder()
        UpdatePhoneNumberPlaceholder()
    End Sub

    Private Sub UpdateUsernamePlaceholder()
        Label1.Visible = String.IsNullOrEmpty(txtRegUsername.Text) AndAlso Not txtRegUsername.Focused
    End Sub

    Private Sub UpdatePasswordPlaceholder()
        Label2.Visible = String.IsNullOrEmpty(txtRegPassword.Text) AndAlso Not txtRegPassword.Focused
    End Sub

    Private Sub UpdateConfirmPasswordPlaceholder()
        Label3.Visible = String.IsNullOrEmpty(txtRegConfirmPassword.Text) AndAlso Not txtRegConfirmPassword.Focused
    End Sub

    Private Sub UpdateEmailPlaceholder()
        Label4.Visible = String.IsNullOrEmpty(txtRegEmail.Text) AndAlso Not txtRegEmail.Focused
    End Sub

    Private Sub UpdateFirstNamePlaceholder()
        Label5.Visible = String.IsNullOrEmpty(txtRegFirstName.Text) AndAlso Not txtRegFirstName.Focused
    End Sub

    Private Sub UpdateLastNamePlaceholder()
        Label6.Visible = String.IsNullOrEmpty(txtRegLastName.Text) AndAlso Not txtRegLastName.Focused
    End Sub

    Private Sub UpdatePhoneNumberPlaceholder()
        Label7.Visible = String.IsNullOrEmpty(txtRegPhoneNumber.Text) AndAlso Not txtRegPhoneNumber.Focused
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnLogIn.Click
        Me.Hide()
        frmLogin.Show()
    End Sub
End Class