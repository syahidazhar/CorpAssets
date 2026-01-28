Imports System.Data.SQLite

Public Class DashboardView
    Public Sub LoadData()
        Using con As New SQLiteConnection(AppConfig.CONN_STR)
            con.Open()
            Dim cmd As New SQLiteCommand(con)

            cmd.CommandText = "SELECT COUNT(*) FROM assets"
            lblTotal.Text = "Total Aset: " & cmd.ExecuteScalar().ToString()

            cmd.CommandText = "SELECT COUNT(*) FROM assets WHERE kondisi='Rusak'"
            lblRusak.Text = "Aset Rusak: " & cmd.ExecuteScalar().ToString()
        End Using
    End Sub
End Class