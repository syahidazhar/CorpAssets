Imports System.Data.SQLite
Imports CorpAssets.Config ' Pastikan ini sesuai nama project kamu

Public Class LoginView
    Public Event LoginBerhasil()
    Private IsModeRegister As Boolean = False

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If IsModeRegister Then
            ProsesRegister()
        Else
            ProsesLogin()
        End If
    End Sub

    Private Sub ProsesLogin()
        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim cmd As New SQLiteCommand("SELECT * FROM users WHERE username=@u AND password=@p", con)
            cmd.Parameters.AddWithValue("@u", txtUser.Text)
            cmd.Parameters.AddWithValue("@p", txtPass.Text)

            Using rdr = cmd.ExecuteReader()
                If rdr.Read() Then
                    AppConfig.CurrentUser = rdr("username").ToString()
                    AppConfig.CatatLog("User Login: " & AppConfig.CurrentUser)
                    RaiseEvent LoginBerhasil()
                Else
                    MsgBox("Username/Password Salah!")
                End If
            End Using
        End Using
    End Sub

    Private Sub ProsesRegister()
        If txtUser.Text = "" Or txtPass.Text = "" Then
            MsgBox("Isi Username dan Password!")
            Exit Sub
        End If

        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            ' Cek apakah user sudah ada
            Dim cmdCek As New SQLiteCommand("SELECT COUNT(*) FROM users WHERE username=@u", con)
            cmdCek.Parameters.AddWithValue("@u", txtUser.Text)

            If Convert.ToInt32(cmdCek.ExecuteScalar()) > 0 Then
                MsgBox("Username sudah dipakai!")
                Exit Sub
            End If

            ' Simpan User Baru
            Dim cmd As New SQLiteCommand("INSERT INTO users (username, password, role) VALUES (@u, @p, 'Staff')", con)
            cmd.Parameters.AddWithValue("@u", txtUser.Text)
            cmd.Parameters.AddWithValue("@p", txtPass.Text)
            cmd.ExecuteNonQuery()

            MsgBox("Registrasi Berhasil! Silakan Login.")
            GantiMode() ' Balik ke mode login
        End Using
    End Sub

    Private Sub lnkRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRegister.LinkClicked
        GantiMode()
    End Sub

    Private Sub GantiMode()
        IsModeRegister = Not IsModeRegister
        txtUser.Clear() : txtPass.Clear()

        If IsModeRegister Then
            lblTitle.Text = "REGISTER USER"
            btnLogin.Text = "DAFTAR SEKARANG"
            btnLogin.BackColor = System.Drawing.Color.SeaGreen
            lnkRegister.Text = "Sudah punya akun? Login"
        Else
            lblTitle.Text = "LOGIN SYSTEM"
            btnLogin.Text = "MASUK"
            btnLogin.BackColor = System.Drawing.Color.Navy
            lnkRegister.Text = "Belum punya akun? Daftar"
        End If
    End Sub
End Class