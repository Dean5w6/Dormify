Public Class frmHomePage
    Private Sub btnGoToLogin_Click(sender As Object, e As EventArgs) Handles btnGoToLogin.Click
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnGoToRegister_Click(sender As Object, e As EventArgs) Handles btnGoToRegister.Click
        Dim regForm As New frmRegister()
        regForm.Show()
        Me.Hide()
    End Sub

    Private Sub btnGoToGuestView_Click(sender As Object, e As EventArgs) Handles btnGoToGuestView.Click
        Dim guestForm As New frmGuestView()
        guestForm.Show()
        Me.Hide()
    End Sub

    Private Sub frmHomePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class