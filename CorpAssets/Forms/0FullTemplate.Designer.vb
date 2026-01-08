<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FullTemplate
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    '--- KOMPONEN UI ---
    Friend WithEvents PanelMenu As Panel
    Friend WithEvents LabelJudulApp As Label
    Friend WithEvents btnMenuDashboard As Button
    Friend WithEvents btnMenuAset As Button
    Friend WithEvents btnMenuLaporan As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents PanelMain As Panel

    '--- PANEL LOGIN ---
    Friend WithEvents pnlLogin As Panel
    Friend WithEvents GroupBoxLogin As GroupBox
    Friend WithEvents txtLoginPass As TextBox
    Friend WithEvents txtLoginUser As TextBox
    Friend WithEvents btnLoginAction As Button
    Friend WithEvents LabelLoginTitle As Label
    Friend WithEvents LabelUser As Label
    Friend WithEvents LabelPass As Label
    Friend WithEvents LinkKeRegister As LinkLabel ' <--- LINK KE REGISTER

    '--- PANEL REGISTER (BARU) ---
    Friend WithEvents pnlRegister As Panel
    Friend WithEvents GroupBoxRegister As GroupBox
    Friend WithEvents txtRegUser As TextBox
    Friend WithEvents txtRegPass As TextBox
    Friend WithEvents btnRegisterAction As Button
    Friend WithEvents LinkKeLogin As LinkLabel
    Friend WithEvents LabelRegUser As Label
    Friend WithEvents LabelRegPass As Label

    '--- PANEL DASHBOARD ---
    Friend WithEvents pnlDashboard As Panel
    Friend WithEvents LabelDashTitle As Label
    Friend WithEvents FlowStats As FlowLayoutPanel
    Friend WithEvents CardTotalAset As Panel
    Friend WithEvents lblValTotal As Label
    Friend WithEvents CardTotalNilai As Panel
    Friend WithEvents lblValNilai As Label
    Friend WithEvents CardKondisi As Panel
    Friend WithEvents lblValRusak As Label

    '--- PANEL ASET (CRUD) ---
    Friend WithEvents pnlAset As Panel
    Friend WithEvents dgvAset As DataGridView
    Friend WithEvents GroupInput As GroupBox
    Friend WithEvents txtNama As TextBox
    Friend WithEvents LabelNama As Label
    Friend WithEvents txtMerk As TextBox
    Friend WithEvents LabelMerk As Label
    Friend WithEvents txtSerial As TextBox
    Friend WithEvents LabelSerial As Label
    Friend WithEvents cmbKategori As ComboBox
    Friend WithEvents LabelKategori As Label
    Friend WithEvents cmbLokasi As ComboBox
    Friend WithEvents LabelLokasi As Label
    Friend WithEvents cmbKondisi As ComboBox
    Friend WithEvents LabelKondisi As Label
    Friend WithEvents dtpTanggal As DateTimePicker
    Friend WithEvents LabelTanggal As Label
    Friend WithEvents txtHarga As TextBox
    Friend WithEvents LabelHarga As Label
    Friend WithEvents btnSimpan As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnHapus As Button
    Friend WithEvents btnClear As Button
    Friend WithEvents txtID As TextBox
    Friend WithEvents LabelCari As Label
    Friend WithEvents txtCari As TextBox

    '--- PANEL LAPORAN ---
    Friend WithEvents pnlLaporan As Panel
    Friend WithEvents LabelLapTitle As Label
    Friend WithEvents dgvLaporan As DataGridView
    Friend WithEvents btnRefreshLaporan As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ClientSize = New System.Drawing.Size(1000, 650)
        Me.Text = "CORPASSETS - Smart Office Inventory System"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)

        ' --- 1. PANEL MENU SAMPING ---
        Me.PanelMenu = New Panel() With {.Dock = DockStyle.Left, .Width = 220, .BackColor = System.Drawing.Color.FromArgb(30, 30, 45)}
        Me.LabelJudulApp = New Label() With {.Text = "CORPASSETS", .ForeColor = System.Drawing.Color.White, .Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold), .Location = New Point(20, 30), .AutoSize = True}
        Me.btnMenuDashboard = BuatTombolMenu("DASHBOARD", 100)
        Me.btnMenuAset = BuatTombolMenu("MANAJEMEN ASET", 160)
        Me.btnMenuLaporan = BuatTombolMenu("LAPORAN", 220)
        Me.btnLogout = BuatTombolMenu("LOGOUT", 550) : Me.btnLogout.BackColor = System.Drawing.Color.IndianRed
        Me.PanelMenu.Controls.AddRange({LabelJudulApp, btnMenuDashboard, btnMenuAset, btnMenuLaporan, btnLogout})

        ' --- 2. PANEL UTAMA ---
        Me.PanelMain = New Panel() With {.Dock = DockStyle.Fill, .BackColor = System.Drawing.Color.WhiteSmoke}

        ' === A. PANEL LOGIN ===
        Me.pnlLogin = New Panel() With {.Dock = DockStyle.Fill, .BackColor = System.Drawing.Color.WhiteSmoke}
        Me.GroupBoxLogin = New GroupBox() With {.Text = "Administrator Login", .Size = New System.Drawing.Size(350, 280), .Location = New Point(325, 180)}
        Me.LabelUser = New Label() With {.Text = "Username", .Location = New Point(30, 40)}
        Me.txtLoginUser = New TextBox() With {.Location = New Point(30, 65), .Width = 280}
        Me.LabelPass = New Label() With {.Text = "Password", .Location = New Point(30, 110)}
        Me.txtLoginPass = New TextBox() With {.Location = New Point(30, 135), .Width = 280, .PasswordChar = "*"}
        Me.btnLoginAction = New Button() With {.Text = "LOGIN", .Location = New Point(30, 180), .Width = 280, .Height = 40, .BackColor = System.Drawing.Color.Teal, .ForeColor = System.Drawing.Color.White}
        Me.LinkKeRegister = New LinkLabel() With {.Text = "Belum punya akun? Daftar disini", .Location = New Point(80, 240), .AutoSize = True}

        Me.GroupBoxLogin.Controls.AddRange({LabelUser, txtLoginUser, LabelPass, txtLoginPass, btnLoginAction, LinkKeRegister})
        Me.pnlLogin.Controls.Add(Me.GroupBoxLogin)

        ' === A.2 PANEL REGISTER (BARU DITAMBAHKAN) ===
        Me.pnlRegister = New Panel() With {.Dock = DockStyle.Fill, .BackColor = System.Drawing.Color.WhiteSmoke, .Visible = False}
        Me.GroupBoxRegister = New GroupBox() With {.Text = "Registrasi Akun Baru", .Size = New System.Drawing.Size(350, 280), .Location = New Point(325, 180)}
        Me.LabelRegUser = New Label() With {.Text = "Username Baru", .Location = New Point(30, 40)}
        Me.txtRegUser = New TextBox() With {.Location = New Point(30, 65), .Width = 280}
        Me.LabelRegPass = New Label() With {.Text = "Password Baru", .Location = New Point(30, 110)}
        Me.txtRegPass = New TextBox() With {.Location = New Point(30, 135), .Width = 280, .PasswordChar = "*"}
        Me.btnRegisterAction = New Button() With {.Text = "DAFTAR SEKARANG", .Location = New Point(30, 180), .Width = 280, .Height = 40, .BackColor = System.Drawing.Color.CornflowerBlue, .ForeColor = System.Drawing.Color.White}
        Me.LinkKeLogin = New LinkLabel() With {.Text = "Sudah punya akun? Login", .Location = New Point(100, 240), .AutoSize = True}

        Me.GroupBoxRegister.Controls.AddRange({LabelRegUser, txtRegUser, LabelRegPass, txtRegPass, btnRegisterAction, LinkKeLogin})
        Me.pnlRegister.Controls.Add(Me.GroupBoxRegister)

        ' === B. PANEL DASHBOARD ===
        Me.pnlDashboard = New Panel() With {.Dock = DockStyle.Fill, .Visible = False}
        Me.LabelDashTitle = New Label() With {.Text = "Dashboard Monitoring", .Font = New System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}
        Me.FlowStats = New FlowLayoutPanel() With {.Location = New Point(20, 70), .Size = New System.Drawing.Size(740, 150)}
        Me.CardTotalAset = BuatKartuStat("Total Aset", "0 Unit", System.Drawing.Color.SteelBlue, lblValTotal)
        Me.CardTotalNilai = BuatKartuStat("Total Nilai Aset", "Rp 0", System.Drawing.Color.SeaGreen, lblValNilai)
        Me.CardKondisi = BuatKartuStat("Aset Rusak/Service", "0 Unit", System.Drawing.Color.DarkOrange, lblValRusak)
        Me.FlowStats.Controls.AddRange({CardTotalAset, CardTotalNilai, CardKondisi})
        Me.pnlDashboard.Controls.AddRange({LabelDashTitle, FlowStats})

        ' === C. PANEL ASET ===
        Me.pnlAset = New Panel() With {.Dock = DockStyle.Fill, .Visible = False}
        Me.GroupInput = New GroupBox() With {.Text = "Form Input Aset", .Location = New Point(20, 20), .Size = New System.Drawing.Size(740, 260)}

        Me.LabelNama = New Label() With {.Text = "Nama Barang", .Location = New Point(20, 30)}
        Me.txtNama = New TextBox() With {.Location = New Point(20, 55), .Width = 200}
        Me.LabelMerk = New Label() With {.Text = "Merk / Brand", .Location = New Point(20, 90)}
        Me.txtMerk = New TextBox() With {.Location = New Point(20, 115), .Width = 200}
        Me.LabelSerial = New Label() With {.Text = "Serial Number", .Location = New Point(20, 150)}
        Me.txtSerial = New TextBox() With {.Location = New Point(20, 175), .Width = 200}
        Me.LabelKategori = New Label() With {.Text = "Kategori", .Location = New Point(250, 30)}
        Me.cmbKategori = New ComboBox() With {.Location = New Point(250, 55), .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList}
        Me.LabelLokasi = New Label() With {.Text = "Lokasi", .Location = New Point(250, 90)}
        Me.cmbLokasi = New ComboBox() With {.Location = New Point(250, 115), .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList}
        Me.LabelKondisi = New Label() With {.Text = "Kondisi", .Location = New Point(250, 150)}
        Me.cmbKondisi = New ComboBox() With {.Location = New Point(250, 175), .Width = 200, .DropDownStyle = ComboBoxStyle.DropDownList} : Me.cmbKondisi.Items.AddRange({"Baik", "Rusak", "Perlu Servis", "Disposal"})
        Me.LabelTanggal = New Label() With {.Text = "Tanggal Beli", .Location = New Point(480, 30)}
        Me.dtpTanggal = New DateTimePicker() With {.Location = New Point(480, 55), .Width = 200, .Format = DateTimePickerFormat.Short}
        Me.LabelHarga = New Label() With {.Text = "Harga Beli (Rp)", .Location = New Point(480, 90)}
        Me.txtHarga = New TextBox() With {.Location = New Point(480, 115), .Width = 200}
        Me.btnSimpan = New Button() With {.Text = "SIMPAN", .Location = New Point(480, 160), .Width = 95, .Height = 40, .BackColor = System.Drawing.Color.ForestGreen, .ForeColor = System.Drawing.Color.White}
        Me.btnEdit = New Button() With {.Text = "UPDATE", .Location = New Point(585, 160), .Width = 95, .Height = 40, .BackColor = System.Drawing.Color.Orange, .ForeColor = System.Drawing.Color.White}
        Me.btnHapus = New Button() With {.Text = "HAPUS", .Location = New Point(480, 205), .Width = 95, .Height = 40, .BackColor = System.Drawing.Color.Crimson, .ForeColor = System.Drawing.Color.White}
        Me.btnClear = New Button() With {.Text = "RESET", .Location = New Point(585, 205), .Width = 95, .Height = 40}
        Me.GroupInput.Controls.AddRange({LabelNama, txtNama, LabelMerk, txtMerk, LabelSerial, txtSerial, LabelKategori, cmbKategori, LabelLokasi, cmbLokasi, LabelKondisi, cmbKondisi, LabelTanggal, dtpTanggal, LabelHarga, txtHarga, btnSimpan, btnEdit, btnHapus, btnClear})

        Me.LabelCari = New Label() With {.Text = "Cari Aset:", .Location = New Point(20, 295), .AutoSize = True}
        Me.txtCari = New TextBox() With {.Location = New Point(90, 292), .Width = 300}
        Me.dgvAset = New DataGridView() With {.Location = New Point(20, 325), .Size = New Size(740, 300), .AllowUserToAddRows = False, .ReadOnly = True, .SelectionMode = DataGridViewSelectionMode.FullRowSelect, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
        Me.txtID = New TextBox() With {.Visible = False}
        Me.pnlAset.Controls.AddRange({GroupInput, LabelCari, txtCari, dgvAset, txtID})

        ' === D. PANEL LAPORAN ===
        Me.pnlLaporan = New Panel() With {.Dock = DockStyle.Fill, .Visible = False}
        Me.LabelLapTitle = New Label() With {.Text = "Laporan Aset", .Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold), .Location = New Point(20, 20), .AutoSize = True}
        Me.dgvLaporan = New DataGridView() With {.Location = New Point(20, 70), .Size = New Size(740, 500), .ReadOnly = True, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
        Me.btnRefreshLaporan = New Button() With {.Text = "Refresh Data", .Location = New Point(640, 25), .Width = 120, .Height = 35}
        Me.pnlLaporan.Controls.AddRange({LabelLapTitle, dgvLaporan, btnRefreshLaporan})

        ' --- FINISHING ---
        Me.PanelMain.Controls.AddRange({pnlLogin, pnlRegister, pnlDashboard, pnlAset, pnlLaporan})
        Me.Controls.Add(Me.PanelMain)
        Me.Controls.Add(Me.PanelMenu)
        Me.ResumeLayout(False)
    End Sub

    Private Function BuatTombolMenu(text As String, yPos As Integer) As Button
        Dim btn As New Button() With {.Text = text, .Location = New Point(10, yPos), .Size = New Size(200, 45), .FlatStyle = FlatStyle.Flat, .ForeColor = System.Drawing.Color.White, .BackColor = System.Drawing.Color.FromArgb(50, 50, 70), .TextAlign = ContentAlignment.MiddleLeft}
        btn.Padding = New Padding(15, 0, 0, 0)
        Return btn
    End Function
    Private Function BuatKartuStat(judul As String, nilai As String, warna As System.Drawing.Color, ByRef lblOutput As Label) As Panel
        Dim pnl As New Panel() With {.Size = New Size(220, 120), .BackColor = warna, .Margin = New Padding(0, 0, 20, 0)}
        Dim lblT As New Label() With {.Text = judul, .ForeColor = System.Drawing.Color.White, .Font = New System.Drawing.Font("Segoe UI", 10), .Location = New Point(15, 15), .AutoSize = True}
        lblOutput = New Label() With {.Text = nilai, .ForeColor = System.Drawing.Color.White, .Font = New System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold), .Location = New Point(15, 50), .AutoSize = True}
        pnl.Controls.AddRange({lblT, lblOutput})
        Return pnl
    End Function
End Class