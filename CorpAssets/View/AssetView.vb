Imports System.Data.SQLite
Imports CorpAssets.Config ' Pastikan import ini ada

Public Class AssetView
    ' Saat Form Diload
    Private Sub AssetView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitComboBox()
        LoadData()
        AddHandler PnlInput.Paint, Sub(s, args)
                                       ControlPaint.DrawBorder(args.Graphics, PnlInput.ClientRectangle, Color.Silver, 1, ButtonBorderStyle.Solid, Color.White, 0, ButtonBorderStyle.None, Color.White, 0, ButtonBorderStyle.None, Color.White, 0, ButtonBorderStyle.None)
                                   End Sub
    End Sub

    ' 1. ISI COMBOBOX DARI DATABASE (RELASI)
    Sub InitComboBox()
        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()

            ' Isi Lokasi
            Dim daLoc As New SQLiteDataAdapter("SELECT nama_lokasi FROM locations", con)
            Dim dtLoc As New DataTable() : daLoc.Fill(dtLoc)
            cmbLokasi.DataSource = dtLoc
            cmbLokasi.DisplayMember = "nama_lokasi"
            cmbLokasi.SelectedIndex = -1

            ' Isi Kategori
            Dim daCat As New SQLiteDataAdapter("SELECT nama_kategori FROM categories", con)
            Dim dtCat As New DataTable() : daCat.Fill(dtCat)
            cmbKategori.DataSource = dtCat
            cmbKategori.DisplayMember = "nama_kategori"
            cmbKategori.SelectedIndex = -1
        End Using

        ' Isi Kondisi (Manual)
        cmbKondisi.Items.Clear()
        cmbKondisi.Items.AddRange({"Baik", "Rusak", "Perlu Servis", "Disposal"})
    End Sub

    ' 2. LOAD DATA KE TABEL (READ)
    Sub LoadData()
        Try
            Using con As New SQLiteConnection(AppConfig.CONN_STR)
                con.Open()
                ' Query Join biar kolom yg muncul bukan ID tapi Nama Lokasinya
                Dim query As String = "SELECT id, nama, merk, serial, lokasi, kategori, kondisi, tgl_beli, harga FROM assets WHERE nama LIKE @cari OR merk LIKE @cari ORDER BY id DESC"

                Dim da As New SQLiteDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("@cari", "%" & txtCari.Text & "%")

                Dim dt As New DataTable()
                da.Fill(dt)
                dgvData.DataSource = dt

                ' Rapikan Judul Kolom
                dgvData.Columns("id").Visible = False ' Sembunyikan ID
                dgvData.Columns("tgl_beli").HeaderText = "Tgl Beli"
                dgvData.Columns("harga").DefaultCellStyle.Format = "N0" ' Format angka (Rp)
            End Using
        Catch ex As Exception
            MsgBox("Gagal load data: " & ex.Message)
        End Try
    End Sub

    ' 3. TOMBOL SIMPAN (CREATE)
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not ValidasiInput() Then Exit Sub

        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim sql As String = "INSERT INTO assets (nama, merk, serial, tgl_beli, harga, kondisi, lokasi, kategori) VALUES (@nm, @mrk, @sn, @tgl, @hrg, @kond, @loc, @cat)"
            Using cmd As New SQLiteCommand(sql, con)
                IsiParameter(cmd)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        AppConfig.CatatLog("Menambah aset baru: " & txtNama.Text)
        MsgBox("Data berhasil disimpan!", MsgBoxStyle.Information)
        LoadData()
        BersihkanForm()
    End Sub

    ' 4. TOMBOL UPDATE (UPDATE)
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtID.Text = "" Then
            MsgBox("Pilih data dari tabel dulu ya!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim sql As String = "UPDATE assets SET nama=@nm, merk=@mrk, serial=@sn, tgl_beli=@tgl, harga=@hrg, kondisi=@kond, lokasi=@loc, kategori=@cat WHERE id=@id"
            Using cmd As New SQLiteCommand(sql, con)
                IsiParameter(cmd)
                cmd.Parameters.AddWithValue("@id", txtID.Text)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        AppConfig.CatatLog("Update aset ID: " & txtID.Text)
        MsgBox("Data berhasil diupdate!", MsgBoxStyle.Information)
        LoadData()
        BersihkanForm()
    End Sub

    ' 5. TOMBOL HAPUS (DELETE)
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtID.Text = "" Then Exit Sub

        If MsgBox("Yakin mau hapus data " & txtNama.Text & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            Using con As New SQLiteConnection(AppConfig.CONN_STR)
                con.Open()
                Dim cmd As New SQLiteCommand("DELETE FROM assets WHERE id=@id", con)
                cmd.Parameters.AddWithValue("@id", txtID.Text)
                cmd.ExecuteNonQuery()
            End Using

            AppConfig.CatatLog("Hapus aset: " & txtNama.Text)
            LoadData()
            BersihkanForm()
        End If
    End Sub

    ' 6. EVENT KLIK TABEL (POPULATE FORM)
    Private Sub dgvData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellClick
        If e.RowIndex < 0 Then Exit Sub

        Dim row As DataGridViewRow = dgvData.Rows(e.RowIndex)

        ' Masukkan data tabel ke form input
        txtID.Text = row.Cells("id").Value.ToString()
        txtNama.Text = row.Cells("nama").Value.ToString()
        txtMerk.Text = row.Cells("merk").Value.ToString()
        txtSerial.Text = row.Cells("serial").Value.ToString()
        txtHarga.Text = row.Cells("harga").Value.ToString()

        cmbKondisi.Text = row.Cells("kondisi").Value.ToString()
        cmbLokasi.Text = row.Cells("lokasi").Value.ToString()
        cmbKategori.Text = row.Cells("kategori").Value.ToString()

        Try
            dtpTanggal.Value = Convert.ToDateTime(row.Cells("tgl_beli").Value)
        Catch
        End Try

        ' Ubah warna tombol biar user tau lagi mode edit
        btnSimpan.Enabled = False
        btnEdit.Enabled = True
        btnHapus.Enabled = True
    End Sub

    ' --- HELPER METHODS ---

    ' Helper: Isi parameter command database
    Sub IsiParameter(cmd As SQLiteCommand)
        cmd.Parameters.AddWithValue("@nm", txtNama.Text)
        cmd.Parameters.AddWithValue("@mrk", txtMerk.Text)
        cmd.Parameters.AddWithValue("@sn", txtSerial.Text)
        cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value)
        ' Pastikan harga jadi angka (kalau kosong jadi 0)
        cmd.Parameters.AddWithValue("@hrg", Val(txtHarga.Text))
        cmd.Parameters.AddWithValue("@kond", cmbKondisi.Text)
        cmd.Parameters.AddWithValue("@loc", cmbLokasi.Text)
        cmd.Parameters.AddWithValue("@cat", cmbKategori.Text)
    End Sub

    ' Helper: Validasi Input Kosong
    Function ValidasiInput() As Boolean
        If txtNama.Text = "" Or cmbLokasi.Text = "" Or cmbKategori.Text = "" Then
            MsgBox("Nama, Lokasi, dan Kategori wajib diisi!", MsgBoxStyle.Exclamation)
            Return False
        End If
        Return True
    End Function

    ' Helper: Reset Form
    Sub BersihkanForm()
        txtID.Clear()
        txtNama.Clear()
        txtMerk.Clear()
        txtSerial.Clear()
        txtHarga.Clear()
        cmbKategori.SelectedIndex = -1
        cmbLokasi.SelectedIndex = -1
        cmbKondisi.SelectedIndex = -1
        dtpTanggal.Value = Now

        ' Reset tombol
        btnSimpan.Enabled = True
        btnEdit.Enabled = False
        btnHapus.Enabled = False
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        BersihkanForm()
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        LoadData() ' Auto search pas ngetik
    End Sub
End Class