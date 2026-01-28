Imports System.Data.SQLite
Imports System.IO
Imports CorpAssets.Config

Public Class LogView
    Public Sub LoadLogs()
        Try
            Using con As New SQLiteConnection(AppConfig.CONN_STR)
                con.Open()

                ' --- PERBAIKAN: GANTI NAMA KOLOM LANGSUNG DI SQL ---
                ' Kita pakai "AS" supaya pas masuk tabel, namanya sudah otomatis bagus.
                ' Jadi kita gak perlu koding ubah-ubah HeaderText lagi (yang bikin error).
                Dim query As String = "SELECT waktu AS 'Waktu Kejadian', " &
                                      "user AS 'Pengguna', " &
                                      "aktivitas AS 'Aktivitas' " &
                                      "FROM logs ORDER BY waktu DESC"

                Dim da As New SQLiteDataAdapter(query, con)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Pasang data ke tabel
                dgvLog.DataSource = dt

                ' --- PERBAIKAN: UKURAN OTOMATIS ---
                ' Daripada kita set Width = 150 manual (yang bikin error),
                ' Kita suruh dia nyesuain diri sendiri.
                dgvLog.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

                ' Khusus kolom Aktivitas biar memenuhi sisa layar
                If dgvLog.Columns.Count > 2 Then
                    dgvLog.Columns("Aktivitas").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If

            End Using
        Catch ex As Exception
            ' Kalau ada error lagi, kita tangkap disini biar aplikasi gak mati mendadak
            Debug.WriteLine("Error LoadLogs: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ' Cek dulu ada datanya gak
        If dgvLog.Rows.Count = 0 Then
            MsgBox("Data log masih kosong, tidak ada yang bisa diexport.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Dialog untuk memilih lokasi simpan file
        Dim sfd As New SaveFileDialog()
        sfd.Filter = "CSV File|*.csv"
        sfd.FileName = "Log_Backup_" & DateTime.Now.ToString("yyyyMMdd_HHmm") & ".csv"

        If sfd.ShowDialog() = DialogResult.OK Then
            Try
                ' Membuat Header CSV
                Dim sb As New Text.StringBuilder()
                sb.AppendLine("Waktu,User,Aktivitas")

                ' Loop isi tabel dan tulis ke CSV
                For Each row As DataGridViewRow In dgvLog.Rows
                    If Not row.IsNewRow Then
                        ' Handle null value biar gak error
                        Dim wkt = If(row.Cells("waktu").Value IsNot Nothing, row.Cells("waktu").Value.ToString(), "")
                        Dim usr = If(row.Cells("user").Value IsNot Nothing, row.Cells("user").Value.ToString(), "")
                        Dim act = If(row.Cells("aktivitas").Value IsNot Nothing, row.Cells("aktivitas").Value.ToString(), "")

                        ' Bersihkan koma dalam teks biar format CSV gak rusak
                        act = act.Replace(",", " ")

                        sb.AppendLine($"{wkt},{usr},{act}")
                    End If
                Next

                ' Simpan File
                File.WriteAllText(sfd.FileName, sb.ToString())
                MsgBox("Log berhasil diexport ke CSV!", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox("Gagal export: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
End Class