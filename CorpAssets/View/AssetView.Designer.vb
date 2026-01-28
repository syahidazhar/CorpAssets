<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AssetView
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' --- KOMPONEN UI ---
    Friend WithEvents PnlMain As Panel
    Friend WithEvents PnlGrid As Panel
    Friend WithEvents PnlInput As Panel

    ' Header & Search di Panel Kiri
    Friend WithEvents PnlHeaderGrid As Panel
    Friend WithEvents LblTitle As Label
    Friend WithEvents txtCari As TextBox
    Friend WithEvents dgvData As DataGridView

    ' Form Input di Panel Kanan
    Friend WithEvents LblFormTitle As Label
    Friend WithEvents txtID As TextBox ' Hidden ID

    Friend WithEvents txtNama As TextBox
    Friend WithEvents LblNama As Label
    Friend WithEvents txtMerk As TextBox
    Friend WithEvents LblMerk As Label
    Friend WithEvents txtSerial As TextBox
    Friend WithEvents LblSerial As Label

    Friend WithEvents cmbKategori As ComboBox
    Friend WithEvents LblKategori As Label
    Friend WithEvents cmbLokasi As ComboBox
    Friend WithEvents LblLokasi As Label
    Friend WithEvents cmbKondisi As ComboBox
    Friend WithEvents LblKondisi As Label

    Friend WithEvents dtpTanggal As DateTimePicker
    Friend WithEvents LblTanggal As Label
    Friend WithEvents txtHarga As TextBox
    Friend WithEvents LblHarga As Label

    ' Tombol-tombol
    Friend WithEvents btnSimpan As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnHapus As Button
    Friend WithEvents btnClear As Button

    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()

        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.PnlGrid = New System.Windows.Forms.Panel()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.PnlHeaderGrid = New System.Windows.Forms.Panel()
        Me.txtCari = New System.Windows.Forms.TextBox()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.PnlInput = New System.Windows.Forms.Panel()

        ' Inisialisasi Kontrol Input
        Me.LblFormTitle = New System.Windows.Forms.Label()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.txtNama = New System.Windows.Forms.TextBox() : Me.LblNama = New System.Windows.Forms.Label()
        Me.txtMerk = New System.Windows.Forms.TextBox() : Me.LblMerk = New System.Windows.Forms.Label()
        Me.txtSerial = New System.Windows.Forms.TextBox() : Me.LblSerial = New System.Windows.Forms.Label()
        Me.cmbKategori = New System.Windows.Forms.ComboBox() : Me.LblKategori = New System.Windows.Forms.Label()
        Me.cmbLokasi = New System.Windows.Forms.ComboBox() : Me.LblLokasi = New System.Windows.Forms.Label()
        Me.cmbKondisi = New System.Windows.Forms.ComboBox() : Me.LblKondisi = New System.Windows.Forms.Label()
        Me.dtpTanggal = New System.Windows.Forms.DateTimePicker() : Me.LblTanggal = New System.Windows.Forms.Label()
        Me.txtHarga = New System.Windows.Forms.TextBox() : Me.LblHarga = New System.Windows.Forms.Label()

        Me.btnSimpan = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnHapus = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()

        Me.SuspendLayout()
        Me.Size = New System.Drawing.Size(1000, 650)
        Me.BackColor = System.Drawing.Color.WhiteSmoke

        ' === 1. PANEL INPUT (KANAN) ===
        Me.PnlInput.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlInput.Width = 320
        Me.PnlInput.BackColor = System.Drawing.Color.White
        Me.PnlInput.Padding = New System.Windows.Forms.Padding(20)

        ' -- Form Title --
        Me.LblFormTitle.Text = "📝 FORM INPUT ASET"
        Me.LblFormTitle.Font = New System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
        Me.LblFormTitle.Location = New System.Drawing.Point(20, 20)
        Me.LblFormTitle.AutoSize = True

        ' -- Helper Layout --
        Dim yPos As Integer = 60
        Dim spacing As Integer = 50

        ' Fungsi Helper buat bikin Label & Input biar codenya gak panjang banget
        SetupInput(LblNama, "Nama Aset", txtNama, yPos)
        SetupInput(LblMerk, "Merk / Brand", txtMerk, yPos + spacing)
        SetupInput(LblSerial, "Serial Number", txtSerial, yPos + spacing * 2)
        SetupCombo(LblKategori, "Kategori", cmbKategori, yPos + spacing * 3)
        SetupCombo(LblLokasi, "Lokasi Penempatan", cmbLokasi, yPos + spacing * 4)
        SetupCombo(LblKondisi, "Kondisi Saat Ini", cmbKondisi, yPos + spacing * 5)

        ' Tanggal Beli
        Me.LblTanggal.Text = "Tanggal Beli"
        Me.LblTanggal.Location = New System.Drawing.Point(20, yPos + spacing * 6)
        Me.dtpTanggal.Location = New System.Drawing.Point(20, yPos + spacing * 6 + 20)
        Me.dtpTanggal.Width = 280
        Me.dtpTanggal.Format = DateTimePickerFormat.Short

        ' Harga
        SetupInput(LblHarga, "Harga Beli (Rp)", txtHarga, yPos + spacing * 7)

        ' Hidden ID
        Me.txtID.Visible = False

        ' -- Tombol Aksi --
        Dim btnY As Integer = yPos + spacing * 8 + 10

        SetupButton(btnSimpan, "SIMPAN", System.Drawing.Color.SeaGreen, 20, btnY)
        SetupButton(btnEdit, "UPDATE", System.Drawing.Color.Orange, 115, btnY)
        SetupButton(btnHapus, "HAPUS", System.Drawing.Color.Crimson, 210, btnY)

        Me.btnClear.Text = "RESET FORM"
        Me.btnClear.Location = New System.Drawing.Point(20, btnY + 50)
        Me.btnClear.Width = 280
        Me.btnClear.Height = 35
        Me.btnClear.FlatStyle = FlatStyle.Flat
        Me.btnClear.BackColor = System.Drawing.Color.Gainsboro

        ' Masukkan kontrol ke Panel Input
        Me.PnlInput.Controls.AddRange({LblFormTitle, txtID, LblNama, txtNama, LblMerk, txtMerk, LblSerial, txtSerial, LblKategori, cmbKategori, LblLokasi, cmbLokasi, LblKondisi, cmbKondisi, LblTanggal, dtpTanggal, LblHarga, txtHarga, btnSimpan, btnEdit, btnHapus, btnClear})

        ' === 2. PANEL GRID (KIRI) ===
        Me.PnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlGrid.Padding = New System.Windows.Forms.Padding(20)

        ' -- Header Grid & Search --
        Me.PnlHeaderGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlHeaderGrid.Height = 60

        Me.LblTitle.Text = "📦 Data Aset Kantor"
        Me.LblTitle.Font = New System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold)
        Me.LblTitle.Location = New System.Drawing.Point(0, 10)
        Me.LblTitle.AutoSize = True

        Me.txtCari.PlaceholderText = "🔍 Cari nama aset..."
        Me.txtCari.Location = New System.Drawing.Point(350, 15)
        Me.txtCari.Width = 250
        Me.txtCari.Font = New System.Drawing.Font("Segoe UI", 10)

        Me.PnlHeaderGrid.Controls.AddRange({LblTitle, txtCari})

        ' -- DataGridView Styling --
        Me.dgvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvData.BackgroundColor = System.Drawing.Color.White
        Me.dgvData.BorderStyle = BorderStyle.None
        Me.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.AllowUserToAddRows = False

        ' Header Style
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(45, 45, 60)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(45, 45, 60)
        Me.dgvData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvData.ColumnHeadersHeight = 40
        Me.dgvData.EnableHeadersVisualStyles = False

        ' Row Style
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(230, 240, 255)
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvData.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvData.RowTemplate.Height = 35

        Me.PnlGrid.Controls.Add(Me.dgvData)
        Me.PnlGrid.Controls.Add(Me.PnlHeaderGrid)

        ' === FINAL ===
        Me.Controls.Add(Me.PnlGrid)
        Me.Controls.Add(Me.PnlInput)
        Me.ResumeLayout(False)
    End Sub

    ' -- HELPER METHODS UNTUK DESIGNER BIAR RAPI --
    Private Sub SetupInput(lbl As Label, txt As String, box As TextBox, y As Integer)
        lbl.Text = txt
        lbl.Location = New System.Drawing.Point(20, y)
        lbl.AutoSize = True
        lbl.ForeColor = System.Drawing.Color.Gray

        box.Location = New System.Drawing.Point(20, y + 20)
        box.Width = 280
        box.Font = New System.Drawing.Font("Segoe UI", 10)
    End Sub

    Private Sub SetupCombo(lbl As Label, txt As String, box As ComboBox, y As Integer)
        lbl.Text = txt
        lbl.Location = New System.Drawing.Point(20, y)
        lbl.AutoSize = True
        lbl.ForeColor = System.Drawing.Color.Gray

        box.Location = New System.Drawing.Point(20, y + 20)
        box.Width = 280
        box.DropDownStyle = ComboBoxStyle.DropDownList
        box.Font = New System.Drawing.Font("Segoe UI", 10)
    End Sub

    Private Sub SetupButton(btn As Button, txt As String, warna As System.Drawing.Color, x As Integer, y As Integer)
        btn.Text = txt
        btn.Location = New System.Drawing.Point(x, y)
        btn.Width = 90
        btn.Height = 40
        btn.BackColor = warna
        btn.ForeColor = System.Drawing.Color.White
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        btn.Cursor = Cursors.Hand
    End Sub
End Class