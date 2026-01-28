Imports System.Data.SQLite

Public Class ReportView
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim da As New SQLiteDataAdapter("SELECT nama, kondisi, tgl_beli, harga FROM assets", con)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvLaporan.DataSource = dt
        End Using
        AppConfig.CatatLog("Melihat Laporan")
    End Sub
End Class