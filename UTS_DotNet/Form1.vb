Imports System.Data.SQLite
Imports System.IO

Public Class Form1
    ' --- KONFIGURASI DATABASE ---
    Private dbName As String = "CorpAssets.db"
    Private connStr As String = "Data Source=" & dbName & "; Version=3; Pooling=False;"
    Private currentUser As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' !!! PENTING: RESET TOTAL DATABASE SETIAP KALI DIJALANKAN !!!
        ' Ini untuk memperbaiki error "Table Not Found" karena database lama masih nyangkut
        InisialisasiDatabase()

        TampilPanel("Login")
    End Sub

    ' ==========================================
    ' BAGIAN 1: DATABASE & HARD RESET
    ' ==========================================
    Sub InisialisasiDatabase()
        Try
            ' 1. HAPUS DATABASE LAMA JIKA ADA (HARD RESET)
            If File.Exists(dbName) Then
                GC.Collect()
                GC.WaitForPendingFinalizers()
                File.Delete(dbName)
            End If

            ' 2. BUAT BARU DARI NOL
            SQLiteConnection.CreateFile(dbName)
            Using con As New SQLiteConnection(connStr)
                con.Open()
                Dim cmd As New SQLiteCommand(con)

                ' Tabel USERS
                cmd.CommandText = "CREATE TABLE users (id INTEGER PRIMARY KEY, username TEXT, password TEXT, role TEXT)"
                cmd.ExecuteNonQuery()
                ' Admin Default
                cmd.CommandText = "INSERT INTO users (username, password, role) VALUES ('admin', 'admin', 'Administrator')"
                cmd.ExecuteNonQuery()

                ' Tabel LOCATIONS (Master Lokasi)
                cmd.CommandText = "CREATE TABLE locations (id INTEGER PRIMARY KEY, nama_lokasi TEXT)"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "INSERT INTO locations (nama_lokasi) VALUES ('Lobby Utama'), ('Ruang Meeting'), ('Ruang Staff IT'), ('Gudang'), ('Pantry')"
                cmd.ExecuteNonQuery()

                ' Tabel CATEGORIES (Master Kategori)
                cmd.CommandText = "CREATE TABLE categories (id INTEGER PRIMARY KEY, nama_kategori TEXT)"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "INSERT INTO categories (nama_kategori) VALUES ('Elektronik'), ('Furniture'), ('Kendaraan'), ('Stationery')"
                cmd.ExecuteNonQuery()

                ' Tabel ASSETS
                cmd.CommandText = "CREATE TABLE assets (id INTEGER PRIMARY KEY AUTOINCREMENT, nama TEXT, merk TEXT, serial TEXT, tgl_beli TEXT, harga INTEGER, kondisi TEXT, location_id INTEGER, category_id INTEGER)"
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox("Gagal Reset DB: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' BAGIAN 2: LOGIN & REGISTER
    ' ==========================================

    ' -- LOGIN --
    Private Sub btnLoginAction_Click(sender As Object, e As EventArgs) Handles btnLoginAction.Click
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim cmd As New SQLiteCommand("SELECT * FROM users WHERE username=@u AND password=@p", con)
            cmd.Parameters.AddWithValue("@u", txtLoginUser.Text)
            cmd.Parameters.AddWithValue("@p", txtLoginPass.Text)

            Using reader = cmd.ExecuteReader()
                If reader.Read() Then
                    currentUser = reader("username").ToString()
                    MsgBox("Login Berhasil!")
                    IsiComboBoxMaster()
                    HitungStatistik()
                    TampilPanel("Dashboard")
                Else
                    MsgBox("Username/Password Salah!")
                End If
            End Using
        End Using
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        currentUser = ""
        txtLoginUser.Clear() : txtLoginPass.Clear()
        TampilPanel("Login")
    End Sub

    ' -- REGISTER (BARU) --
    Private Sub LinkKeRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkKeRegister.LinkClicked
        TampilPanel("Register")
    End Sub

    Private Sub LinkKeLogin_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkKeLogin.LinkClicked
        TampilPanel("Login")
    End Sub

    Private Sub btnRegisterAction_Click(sender As Object, e As EventArgs) Handles btnRegisterAction.Click
        If txtRegUser.Text = "" Or txtRegPass.Text = "" Then MsgBox("Isi data!") : Exit Sub

        Using con As New SQLiteConnection(connStr)
            con.Open()
            ' Cek duplikat
            Dim cmdCek As New SQLiteCommand("SELECT COUNT(*) FROM users WHERE username=@u", con)
            cmdCek.Parameters.AddWithValue("@u", txtRegUser.Text)
            If Convert.ToInt32(cmdCek.ExecuteScalar()) > 0 Then
                MsgBox("Username sudah dipakai!") : Exit Sub
            End If

            ' Insert User Baru
            Dim cmd As New SQLiteCommand("INSERT INTO users (username, password, role) VALUES (@u, @p, 'Staff')", con)
            cmd.Parameters.AddWithValue("@u", txtRegUser.Text)
            cmd.Parameters.AddWithValue("@p", txtRegPass.Text)
            cmd.ExecuteNonQuery()
        End Using

        MsgBox("Registrasi Berhasil! Silakan Login.")
        txtRegUser.Clear() : txtRegPass.Clear()
        TampilPanel("Login")
    End Sub

    ' ==========================================
    ' BAGIAN 3: NAVIGASI PANEL
    ' ==========================================
    Sub TampilPanel(namaPanel As String)
        pnlLogin.Visible = False : pnlRegister.Visible = False
        pnlDashboard.Visible = False : pnlAset.Visible = False : pnlLaporan.Visible = False
        PanelMenu.Visible = (namaPanel <> "Login" And namaPanel <> "Register")

        Select Case namaPanel
            Case "Login" : pnlLogin.Visible = True
            Case "Register" : pnlRegister.Visible = True
            Case "Dashboard" : pnlDashboard.Visible = True
            Case "Aset" : pnlAset.Visible = True : LoadDataAset()
            Case "Laporan" : pnlLaporan.Visible = True : LoadLaporan()
        End Select
    End Sub

    Private Sub btnMenuDashboard_Click(sender As Object, e As EventArgs) Handles btnMenuDashboard.Click
        TampilPanel("Dashboard") : HitungStatistik()
    End Sub
    Private Sub btnMenuAset_Click(sender As Object, e As EventArgs) Handles btnMenuAset.Click
        TampilPanel("Aset")
    End Sub
    Private Sub btnMenuLaporan_Click(sender As Object, e As EventArgs) Handles btnMenuLaporan.Click
        TampilPanel("Laporan")
    End Sub

    ' ==========================================
    ' BAGIAN 4: LOGIKA APLIKASI UTAMA
    ' ==========================================
    Sub HitungStatistik()
        Try
            Using con As New SQLiteConnection(connStr)
                con.Open()
                Dim cmd As New SQLiteCommand(con)
                cmd.CommandText = "SELECT COUNT(*) FROM assets"
                lblValTotal.Text = cmd.ExecuteScalar().ToString() & " Unit"
                cmd.CommandText = "SELECT IFNULL(SUM(harga), 0) FROM assets"
                lblValNilai.Text = "Rp " & FormatNumber(cmd.ExecuteScalar(), 0)
                cmd.CommandText = "SELECT COUNT(*) FROM assets WHERE kondisi IN ('Rusak', 'Perlu Servis', 'Disposal')"
                lblValRusak.Text = cmd.ExecuteScalar().ToString() & " Unit"
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Sub IsiComboBoxMaster()
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim daLoc As New SQLiteDataAdapter("SELECT * FROM locations", con)
            Dim dtLoc As New DataTable() : daLoc.Fill(dtLoc)
            cmbLokasi.DataSource = dtLoc : cmbLokasi.DisplayMember = "nama_lokasi" : cmbLokasi.ValueMember = "id"

            Dim daCat As New SQLiteDataAdapter("SELECT * FROM categories", con)
            Dim dtCat As New DataTable() : daCat.Fill(dtCat)
            cmbKategori.DataSource = dtCat : cmbKategori.DisplayMember = "nama_kategori" : cmbKategori.ValueMember = "id"
        End Using
    End Sub

    Sub LoadDataAset()
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim sql As String = "SELECT a.id, a.nama, a.merk, a.serial, c.nama_kategori, l.nama_lokasi, a.kondisi, a.tgl_beli, a.harga FROM assets a LEFT JOIN locations l ON a.location_id = l.id LEFT JOIN categories c ON a.category_id = c.id WHERE a.nama LIKE @cari"
            Dim da As New SQLiteDataAdapter(sql, con)
            da.SelectCommand.Parameters.AddWithValue("@cari", "%" & txtCari.Text & "%")
            Dim dt As New DataTable() : da.Fill(dt)
            dgvAset.DataSource = dt
        End Using
    End Sub
    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        LoadDataAset()
    End Sub

    Sub LoadLaporan()
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim da As New SQLiteDataAdapter("SELECT * FROM assets", con)
            Dim dt As New DataTable() : da.Fill(dt)
            dgvLaporan.DataSource = dt
        End Using
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If txtNama.Text = "" Or cmbLokasi.SelectedIndex = -1 Then MsgBox("Lengkapi data!") : Exit Sub
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim sql As String = "INSERT INTO assets (nama, merk, serial, tgl_beli, harga, kondisi, location_id, category_id) VALUES (@nm, @mrk, @sn, @tgl, @hrg, @kond, @loc, @cat)"
            Using cmd As New SQLiteCommand(sql, con)
                cmd.Parameters.AddWithValue("@nm", txtNama.Text) : cmd.Parameters.AddWithValue("@mrk", txtMerk.Text)
                cmd.Parameters.AddWithValue("@sn", txtSerial.Text) : cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@hrg", Val(txtHarga.Text)) : cmd.Parameters.AddWithValue("@kond", cmbKondisi.Text)
                cmd.Parameters.AddWithValue("@loc", cmbLokasi.SelectedValue) : cmd.Parameters.AddWithValue("@cat", cmbKategori.SelectedValue)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        MsgBox("Aset tersimpan!") : LoadDataAset() : HitungStatistik() : BersihkanInput()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtID.Text = "" Then MsgBox("Pilih aset!") : Exit Sub
        Using con As New SQLiteConnection(connStr)
            con.Open()
            Dim sql As String = "UPDATE assets SET nama=@nm, merk=@mrk, serial=@sn, tgl_beli=@tgl, harga=@hrg, kondisi=@kond, location_id=@loc, category_id=@cat WHERE id=@id"
            Using cmd As New SQLiteCommand(sql, con)
                cmd.Parameters.AddWithValue("@nm", txtNama.Text) : cmd.Parameters.AddWithValue("@mrk", txtMerk.Text)
                cmd.Parameters.AddWithValue("@sn", txtSerial.Text) : cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@hrg", Val(txtHarga.Text)) : cmd.Parameters.AddWithValue("@kond", cmbKondisi.Text)
                cmd.Parameters.AddWithValue("@loc", cmbLokasi.SelectedValue) : cmd.Parameters.AddWithValue("@cat", cmbKategori.SelectedValue)
                cmd.Parameters.AddWithValue("@id", txtID.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using
        MsgBox("Update sukses!") : LoadDataAset() : HitungStatistik() : BersihkanInput()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtID.Text = "" Then Exit Sub
        If MsgBox("Hapus?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Using con As New SQLiteConnection(connStr)
                con.Open() : Dim cmd As New SQLiteCommand("DELETE FROM assets WHERE id=@id", con)
                cmd.Parameters.AddWithValue("@id", txtID.Text) : cmd.ExecuteNonQuery()
            End Using
            LoadDataAset() : HitungStatistik() : BersihkanInput()
        End If
    End Sub

    Private Sub dgvAset_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAset.CellClick
        If e.RowIndex < 0 Then Exit Sub
        Dim row As DataGridViewRow = dgvAset.Rows(e.RowIndex)
        txtID.Text = row.Cells("id").Value.ToString()
        txtNama.Text = row.Cells("nama").Value.ToString()
        txtMerk.Text = row.Cells("merk").Value.ToString()
        txtSerial.Text = row.Cells("serial").Value.ToString()
        txtHarga.Text = row.Cells("harga").Value.ToString()
        cmbKondisi.Text = row.Cells("kondisi").Value.ToString()
        cmbKategori.Text = row.Cells("nama_kategori").Value.ToString()
        cmbLokasi.Text = row.Cells("nama_lokasi").Value.ToString()
        Try : dtpTanggal.Value = DateTime.Parse(row.Cells("tgl_beli").Value.ToString()) : Catch : End Try
    End Sub

    Sub BersihkanInput()
        txtID.Clear() : txtNama.Clear() : txtMerk.Clear() : txtSerial.Clear() : txtHarga.Clear() : cmbKondisi.SelectedIndex = -1
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        BersihkanInput()
    End Sub
End Class