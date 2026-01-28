Imports System.Data.SQLite
Imports System.IO

Public Module AppConfig
    Public Const DB_NAME As String = "CorpAssets.db"
    Public Const CONN_STR As String = "Data Source=" & DB_NAME & "; Version=3;"
    Public CurrentUser As String = ""

    Public Sub InitDatabase()
        ' 1. BUAT FILE DATABASE JIKA BELUM ADA
        ' (Tapi kalau sudah ada, dia diam saja, gak dihapus)
        If Not File.Exists(DB_NAME) Then
            SQLiteConnection.CreateFile(DB_NAME)
        End If

        Using con As New SQLiteConnection(CONN_STR)
            con.Open()
            Dim cmd As New SQLiteCommand(con)

            ' 2. BUAT TABEL (HANYA JIKA BELUM ADA)
            ' Kita pakai sintaks "CREATE TABLE IF NOT EXISTS"
            ' Jadi kalau tabelnya sudah ada, datanya GAK AKAN HILANG.

            ' Tabel Users
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, username TEXT, password TEXT, role TEXT)"
            cmd.ExecuteNonQuery()

            ' Tabel Assets
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS assets (id INTEGER PRIMARY KEY AUTOINCREMENT, nama TEXT, merk TEXT, serial TEXT, tgl_beli DATE, harga INTEGER, kondisi TEXT, lokasi TEXT, kategori TEXT)"
            cmd.ExecuteNonQuery()

            ' Tabel Logs
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS logs (id INTEGER PRIMARY KEY AUTOINCREMENT, waktu DATETIME, user TEXT, aktivitas TEXT)"
            cmd.ExecuteNonQuery()

            ' Tabel Locations
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS locations (id INTEGER PRIMARY KEY, nama_lokasi TEXT)"
            cmd.ExecuteNonQuery()

            ' Tabel Categories
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS categories (id INTEGER PRIMARY KEY, nama_kategori TEXT)"
            cmd.ExecuteNonQuery()

            ' 3. ISI DATA PANCINGAN (HANYA JIKA TABEL KOSONG)
            ' Biar gak dobel-dobel kalau dijalankan berkali-kali

            ' Cek User Admin
            cmd.CommandText = "SELECT COUNT(*) FROM users"
            Dim userCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            If userCount = 0 Then
                cmd.CommandText = "INSERT INTO users (username, password, role) VALUES ('admin', 'admin', 'Administrator')"
                cmd.ExecuteNonQuery()
            End If

            ' Cek Lokasi
            cmd.CommandText = "SELECT COUNT(*) FROM locations"
            Dim locCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            If locCount = 0 Then
                cmd.CommandText = "INSERT INTO locations (nama_lokasi) VALUES ('Lobby Utama'), ('Ruang Meeting'), ('Ruang Staff IT'), ('Gudang'), ('Pantry'), ('Ruang Direksi')"
                cmd.ExecuteNonQuery()
            End If

            ' Cek Kategori
            cmd.CommandText = "SELECT COUNT(*) FROM categories"
            Dim catCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            If catCount = 0 Then
                cmd.CommandText = "INSERT INTO categories (nama_kategori) VALUES ('Elektronik'), ('Furniture'), ('Kendaraan'), ('Stationery'), ('Aksesoris')"
                cmd.ExecuteNonQuery()
            End If
        End Using
    End Sub

    ' --- FUNGSI LOGGING (TETAP SAMA) ---
    Public Sub CatatLog(aktivitas As String)
        Dim waktu As DateTime = DateTime.Now
        Dim user As String = If(CurrentUser = "", "System", CurrentUser)

        ' 1. Simpan ke Database
        Try
            Using con As New SQLiteConnection(CONN_STR)
                con.Open()
                Dim cmd As New SQLiteCommand("INSERT INTO logs (waktu, user, aktivitas) VALUES (@w, @u, @a)", con)
                cmd.Parameters.AddWithValue("@w", waktu)
                cmd.Parameters.AddWithValue("@u", user)
                cmd.Parameters.AddWithValue("@a", aktivitas)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
        End Try

        ' 2. Simpan ke CSV (Backup Realtime)
        Try
            Dim barisCsv As String = $"{waktu.ToString("yyyy-MM-dd HH:mm:ss")},{user},{aktivitas}"
            File.AppendAllText("SystemLogs.csv", barisCsv & Environment.NewLine)
        Catch ex As Exception
        End Try
    End Sub
End Module