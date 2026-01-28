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

    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lnkRegister As System.Windows.Forms.LinkLabel ' <--- TAMBAHAN
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lnkRegister = New System.Windows.Forms.LinkLabel()

        Me.lblTitle.Text = "LOGIN SYSTEM"
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(50, 20)
        Me.lblTitle.AutoSize = True

        Me.txtUser.PlaceholderText = "Username"
        Me.txtUser.Location = New System.Drawing.Point(50, 70)
        Me.txtUser.Width = 200

        Me.txtPass.PlaceholderText = "Password"
        Me.txtPass.Location = New System.Drawing.Point(50, 110)
        Me.txtPass.Width = 200
        Me.txtPass.PasswordChar = "*"c

        Me.btnLogin.Text = "MASUK"
        Me.btnLogin.Location = New System.Drawing.Point(50, 150)
        Me.btnLogin.Width = 200
        Me.btnLogin.Height = 40
        Me.btnLogin.BackColor = System.Drawing.Color.Navy
        Me.btnLogin.ForeColor = System.Drawing.Color.White

        ' Konfigurasi Link Register
        Me.lnkRegister.Text = "Belum punya akun? Daftar"
        Me.lnkRegister.Location = New System.Drawing.Point(50, 200)
        Me.lnkRegister.AutoSize = True

        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.lnkRegister)
        Me.Size = New System.Drawing.Size(300, 300)
    End Sub
End Class