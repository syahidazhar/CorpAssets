Imports System.Data.SQLite
Imports System.Drawing.Drawing2D
Imports CorpAssets.Config

Public Class LoginView
    Public Event LoginBerhasil()
    Private IsModeRegister As Boolean = False

    ' --- AGAR POSISI SELALU DI TENGAH ---
    Private Sub LoginView_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If pnlCard IsNot Nothing Then
            pnlCard.Left = (Me.Width - pnlCard.Width) \ 2
            pnlCard.Top = (Me.Height - pnlCard.Height) \ 2
        End If
    End Sub

    ' --- BIKIN LOGO BULAT & CARD ROUNDED ---
    Private Sub LoginView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Bikin Logo Bulat
        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, pbLogo.Width, pbLogo.Height)
        pbLogo.Region = New Region(path)

        ' Bikin Panel Input Rounded (Dikit)
        MakeRounded(pnlUserContainer, 10)
        MakeRounded(pnlPassContainer, 10)
        MakeRounded(btnLogin, 10)
    End Sub

    Private Sub MakeRounded(ctrl As Control, radius As Integer)
        Dim p As New GraphicsPath()
        p.AddArc(0, 0, radius, radius, 180, 90)
        p.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90)
        p.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90)
        p.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90)
        p.CloseFigure()
        ctrl.Region = New Region(p)
    End Sub

    ' --- LOGIKA LOGIN (TETAP SAMA KAYAK KEMARIN) ---
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
                    MsgBox("Username/Password Salah!", MsgBoxStyle.Exclamation, "Login Failed")
                End If
            End Using
        End Using
    End Sub

    Private Sub ProsesRegister()
        If txtUser.Text = "" Or txtPass.Text = "" Then
            MsgBox("Isi Username dan Password!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim cmdCek As New SQLiteCommand("SELECT COUNT(*) FROM users WHERE username=@u", con)
            cmdCek.Parameters.AddWithValue("@u", txtUser.Text)

            If Convert.ToInt32(cmdCek.ExecuteScalar()) > 0 Then
                MsgBox("Username sudah dipakai!", MsgBoxStyle.Critical)
                Exit Sub
            End If

            Dim cmd As New SQLiteCommand("INSERT INTO users (username, password, role) VALUES (@u, @p, 'Staff')", con)
            cmd.Parameters.AddWithValue("@u", txtUser.Text)
            cmd.Parameters.AddWithValue("@p", txtPass.Text)
            cmd.ExecuteNonQuery()

            MsgBox("Registrasi Berhasil! Silakan Login.", MsgBoxStyle.Information)
            GantiMode()
        End Using
    End Sub

    Private Sub lnkRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkRegister.LinkClicked
        GantiMode()
    End Sub

    Private Sub GantiMode()
        IsModeRegister = Not IsModeRegister
        txtUser.Clear() : txtPass.Clear()

        If IsModeRegister Then
            lblTitle.Text = "Create Account"
            lblSubtitle.Text = "Join CorpAssets team today"
            btnLogin.Text = "SIGN UP"
            btnLogin.BackColor = Color.FromArgb(16, 185, 129) ' Hijau
            lnkRegister.Text = "Already have an account? Sign In"
        Else
            lblTitle.Text = "Welcome Back"
            lblSubtitle.Text = "Enter your credentials to access CorpAssets"
            btnLogin.Text = "SIGN IN"
            btnLogin.BackColor = Color.FromArgb(59, 130, 246) ' Biru
            lnkRegister.Text = "Don't have an account? Sign Up"
        End If
    End Sub

    ' Paint Effect untuk Card Shadow (Opsional, simpel border aja biar gak berat)
    Private Sub pnlCard_Paint(sender As Object, e As PaintEventArgs) Handles pnlCard.Paint
        ControlPaint.DrawBorder(e.Graphics, pnlCard.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid)
    End Sub
End Class