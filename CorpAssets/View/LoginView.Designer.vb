<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginView
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' KOMPONEN
    Friend WithEvents pnlCard As System.Windows.Forms.Panel
    Friend WithEvents pbLogo As System.Windows.Forms.Panel ' Pura-puranya logo bulat
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubtitle As System.Windows.Forms.Label

    ' Container Input (Biar ada garis bawah/kotak cantik)
    Friend WithEvents pnlUserContainer As System.Windows.Forms.Panel
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents lblUserIcon As System.Windows.Forms.Label

    Friend WithEvents pnlPassContainer As System.Windows.Forms.Panel
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents lblPassIcon As System.Windows.Forms.Label

    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents lnkRegister As System.Windows.Forms.LinkLabel
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.pnlCard = New System.Windows.Forms.Panel()
        Me.pbLogo = New System.Windows.Forms.Panel()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubtitle = New System.Windows.Forms.Label()

        Me.pnlUserContainer = New System.Windows.Forms.Panel()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.lblUserIcon = New System.Windows.Forms.Label()

        Me.pnlPassContainer = New System.Windows.Forms.Panel()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.lblPassIcon = New System.Windows.Forms.Label()

        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lnkRegister = New System.Windows.Forms.LinkLabel()

        Me.SuspendLayout()

        ' --- SETUP UTAMA ---
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246) ' Abu-abu terang modern
        Me.Size = New System.Drawing.Size(800, 600)

        ' --- CARD (KOTAK PUTIH DI TENGAH) ---
        Me.pnlCard.BackColor = System.Drawing.Color.White
        Me.pnlCard.Size = New System.Drawing.Size(400, 500)
        ' Posisi di tengah akan diatur di logic .vb (Resize Event)
        Me.pnlCard.Location = New System.Drawing.Point(200, 50)

        ' --- LOGO BULAT (VISUAL CANDY) ---
        Me.pbLogo.Size = New System.Drawing.Size(60, 60)
        Me.pbLogo.Location = New System.Drawing.Point(170, 30)
        Me.pbLogo.BackColor = System.Drawing.Color.FromArgb(59, 130, 246) ' Biru Modern
        ' Bikin bulat nanti di logic Paint

        ' BRAND INITIAL
        Me.lblBrand.Text = "CA"
        Me.lblBrand.ForeColor = System.Drawing.Color.White
        Me.lblBrand.Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        Me.lblBrand.AutoSize = False
        Me.lblBrand.Size = New System.Drawing.Size(60, 60)
        Me.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBrand.BackColor = System.Drawing.Color.Transparent
        Me.pbLogo.Controls.Add(Me.lblBrand)

        ' --- JUDUL ---
        Me.lblTitle.Text = "Welcome Back"
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(31, 41, 55)
        Me.lblTitle.AutoSize = False
        Me.lblTitle.Size = New System.Drawing.Size(400, 35)
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitle.Location = New System.Drawing.Point(0, 100)

        Me.lblSubtitle.Text = "Enter your credentials to access CorpAssets"
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.Gray
        Me.lblSubtitle.AutoSize = False
        Me.lblSubtitle.Size = New System.Drawing.Size(400, 20)
        Me.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSubtitle.Location = New System.Drawing.Point(0, 135)

        ' --- INPUT USERNAME ---
        Me.pnlUserContainer.Size = New System.Drawing.Size(320, 50)
        Me.pnlUserContainer.Location = New System.Drawing.Point(40, 180)
        Me.pnlUserContainer.BackColor = System.Drawing.Color.FromArgb(249, 250, 251) ' Abu sangat muda

        Me.lblUserIcon.Text = "👤"
        Me.lblUserIcon.Font = New System.Drawing.Font("Segoe UI Emoji", 12)
        Me.lblUserIcon.Location = New System.Drawing.Point(10, 12)
        Me.lblUserIcon.AutoSize = True

        Me.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUser.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.txtUser.Font = New System.Drawing.Font("Segoe UI", 11)
        Me.txtUser.Location = New System.Drawing.Point(45, 15)
        Me.txtUser.Width = 260
        Me.txtUser.PlaceholderText = "Username"

        Me.pnlUserContainer.Controls.Add(Me.lblUserIcon)
        Me.pnlUserContainer.Controls.Add(Me.txtUser)

        ' --- INPUT PASSWORD ---
        Me.pnlPassContainer.Size = New System.Drawing.Size(320, 50)
        Me.pnlPassContainer.Location = New System.Drawing.Point(40, 245)
        Me.pnlPassContainer.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)

        Me.lblPassIcon.Text = "🔒"
        Me.lblPassIcon.Font = New System.Drawing.Font("Segoe UI Emoji", 12)
        Me.lblPassIcon.Location = New System.Drawing.Point(10, 12)
        Me.lblPassIcon.AutoSize = True

        Me.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPass.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.txtPass.Font = New System.Drawing.Font("Segoe UI", 11)
        Me.txtPass.Location = New System.Drawing.Point(45, 15)
        Me.txtPass.Width = 260
        Me.txtPass.PasswordChar = "●"c
        Me.txtPass.PlaceholderText = "Password"

        Me.pnlPassContainer.Controls.Add(Me.lblPassIcon)
        Me.pnlPassContainer.Controls.Add(Me.txtPass)

        ' --- TOMBOL LOGIN ---
        Me.btnLogin.Text = "SIGN IN"
        Me.btnLogin.Location = New System.Drawing.Point(40, 320)
        Me.btnLogin.Size = New System.Drawing.Size(320, 45)
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(59, 130, 246) ' Biru Modern
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand

        ' --- LINK REGISTER ---
        Me.lnkRegister.Text = "Don't have an account? Sign Up"
        Me.lnkRegister.Font = New System.Drawing.Font("Segoe UI", 9)
        Me.lnkRegister.LinkColor = System.Drawing.Color.FromArgb(59, 130, 246)
        Me.lnkRegister.ActiveLinkColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lnkRegister.AutoSize = False
        Me.lnkRegister.Size = New System.Drawing.Size(400, 30)
        Me.lnkRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lnkRegister.Location = New System.Drawing.Point(0, 380)
        Me.lnkRegister.Cursor = System.Windows.Forms.Cursors.Hand

        ' --- RAKIT CARD ---
        Me.pnlCard.Controls.Add(Me.pbLogo)
        Me.pnlCard.Controls.Add(Me.lblTitle)
        Me.pnlCard.Controls.Add(Me.lblSubtitle)
        Me.pnlCard.Controls.Add(Me.pnlUserContainer)
        Me.pnlCard.Controls.Add(Me.pnlPassContainer)
        Me.pnlCard.Controls.Add(Me.btnLogin)
        Me.pnlCard.Controls.Add(Me.lnkRegister)

        Me.Controls.Add(Me.pnlCard)
        Me.ResumeLayout(False)
    End Sub
End Class