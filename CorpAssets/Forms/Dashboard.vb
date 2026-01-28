Imports System.Data.SQLite
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class DashboardForm
    Inherits System.Windows.Forms.Form

    ' --- CONFIG DB ---
    Private dbName As String = "CorpAssets.db"
    Private connStr As String = "Data Source=" & dbName & "; Version=3;"

    ' --- VARIABLES ---
    Private ValTotal As Integer = 0
    Private ValValue As Double = 0
    Private ValAlert As Integer = 0
    Private Structure KategoriStat
        Public Nama As String
        Public Jumlah As Integer
        Public Warna As Color
    End Structure
    Private ListKategori As New List(Of KategoriStat)
    Private isDarkMode As Boolean = False

    ' --- THEME STRUCT ---
    Private Structure ThemeColors
        Public Bg As Color, PanelBg As Color, TextMain As Color, TextSub As Color
        Public Accent1 As Color, Accent2 As Color, Accent3 As Color, GridLine As Color
    End Structure
    Private Theme As ThemeColors

    Public Sub New()
        InitializeComponent()
        SetupCustomUI()
        ApplyTheme(False)
    End Sub

    Private Sub SetupCustomUI()
        ' Setup Custom Painting untuk Cards
        pnlStat1.BackColor = Color.White
        pnlStat2.BackColor = Color.White
        pnlStat3.BackColor = Color.White

        AddHandler pnlStat1.Paint, AddressOf DrawStatCard_Total
        AddHandler pnlStat2.Paint, AddressOf DrawStatCard_Value
        AddHandler pnlStat3.Paint, AddressOf DrawStatCard_Alert

        ' Setup Chart
        pnlChartContainer.Controls.Remove(pnlChart)
        pnlChart = New ModernPanel()
        pnlChart.Location = New Point(15, 50)
        pnlChart.Size = New Size(560, 415)
        pnlChart.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        AddHandler pnlChart.Paint, AddressOf DrawResponsiveChart
        pnlChartContainer.Controls.Add(pnlChart)

        ' Setup Tabel Badge
        AddHandler dgvRecent.CellPainting, AddressOf dgvRecent_CellPainting
    End Sub

    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateDatabaseIfNotExists()
        LoadData()
    End Sub

    ' ==========================================
    ' BAGIAN UTAMA: PEMAKSAAN 4 KOLOM MUNCUL
    ' ==========================================
    Private Sub LoadData()
        If Not File.Exists(dbName) Then Exit Sub
        Try
            Using con As New SQLiteConnection(connStr)
                con.Open()

                ' 1. Load Stats
                ValTotal = Convert.ToInt32(New SQLiteCommand("SELECT COUNT(*) FROM assets", con).ExecuteScalar())
                ValValue = Convert.ToDouble(New SQLiteCommand("SELECT IFNULL(SUM(harga), 0) FROM assets", con).ExecuteScalar())
                ValAlert = Convert.ToInt32(New SQLiteCommand("SELECT COUNT(*) FROM assets WHERE kondisi IN ('Rusak', 'Perlu Servis', 'Broken', 'Damaged', 'Disposal')", con).ExecuteScalar())

                ' 2. Load Table Data
                Dim da As New SQLiteDataAdapter("SELECT nama, kondisi, tgl_beli, harga FROM assets ORDER BY id DESC LIMIT 20", con)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' --- SETTING KOLOM ---
                dgvRecent.AutoGenerateColumns = False
                dgvRecent.DataSource = Nothing
                dgvRecent.Columns.Clear()

                ' KUNCI: Gunakan FillWeight untuk memaksa pembagian lebar (Total 100%)
                dgvRecent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

                ' KOLOM 1: NAMA (40%)
                Dim col1 As New DataGridViewTextBoxColumn()
                col1.DataPropertyName = "nama"
                col1.HeaderText = "ASSET NAME"
                col1.FillWeight = 40
                dgvRecent.Columns.Add(col1)

                ' KOLOM 2: KONDISI (20%)
                Dim col2 As New DataGridViewTextBoxColumn()
                col2.DataPropertyName = "kondisi"
                col2.HeaderText = "CONDITION"
                col2.FillWeight = 20
                dgvRecent.Columns.Add(col2)

                ' KOLOM 3: TANGGAL (20%)
                Dim col3 As New DataGridViewTextBoxColumn()
                col3.DataPropertyName = "tgl_beli"
                col3.HeaderText = "DATE"
                col3.DefaultCellStyle.Format = "dd MMM yyyy"
                col3.FillWeight = 20
                dgvRecent.Columns.Add(col3)

                ' KOLOM 4: HARGA (20%)
                Dim col4 As New DataGridViewTextBoxColumn()
                col4.DataPropertyName = "harga"
                col4.HeaderText = "PRICE"
                col4.DefaultCellStyle.Format = "N0"
                col4.FillWeight = 20
                dgvRecent.Columns.Add(col4)

                ' Masukkan Data
                dgvRecent.DataSource = dt
                dgvRecent.ClearSelection()

                ' 3. Load Chart Data
                ListKategori.Clear()
                Dim cmd As New SQLiteCommand("SELECT c.nama_kategori, COUNT(a.id) as jum FROM categories c LEFT JOIN assets a ON a.category_id = c.id GROUP BY c.nama_kategori", con)
                Using rdr = cmd.ExecuteReader()
                    Dim pal() As Color = {Color.FromArgb(99, 102, 241), Color.FromArgb(16, 185, 129), Color.FromArgb(245, 158, 11), Color.FromArgb(236, 72, 153)}
                    Dim i As Integer = 0
                    While rdr.Read()
                        ListKategori.Add(New KategoriStat With {.Nama = rdr("nama_kategori").ToString(), .Jumlah = Convert.ToInt32(rdr("jum")), .Warna = pal(i Mod pal.Length)})
                        i += 1
                    End While
                End Using
            End Using
        Catch ex As Exception
        End Try

        pnlStat1.Invalidate() : pnlStat2.Invalidate() : pnlStat3.Invalidate() : pnlChart.Invalidate()
    End Sub

    ' --- VISUALISASI ---
    Private Sub DrawStatCard_Total(sender As Object, e As PaintEventArgs)
        DrawCard(e.Graphics, pnlStat1.Width, pnlStat1.Height, "TOTAL ASSETS", ValTotal.ToString("N0"), "📦", Theme.Accent1)
    End Sub
    Private Sub DrawStatCard_Value(sender As Object, e As PaintEventArgs)
        DrawCard(e.Graphics, pnlStat2.Width, pnlStat2.Height, "TOTAL VALUE", "Rp " & FormatNumber(ValValue, 0), "Rp", Theme.Accent2)
    End Sub
    Private Sub DrawStatCard_Alert(sender As Object, e As PaintEventArgs)
        Dim suffix As String = If(ValAlert > 1, " Items", " Item")
        DrawCard(e.Graphics, pnlStat3.Width, pnlStat3.Height, "NEEDS ATTENTION", ValAlert.ToString() & suffix, "⚠️", Theme.Accent3)
    End Sub

    Private Sub DrawCard(g As Graphics, w As Integer, h As Integer, title As String, value As String, icon As String, accent As Color)
        g.SmoothingMode = SmoothingMode.AntiAlias

        ' Background Rounded
        Dim path As New GraphicsPath()
        Dim r As Integer = 15
        path.AddArc(0, 0, r, r, 180, 90)
        path.AddArc(w - r - 1, 0, r, r, 270, 90)
        path.AddArc(w - r - 1, h - r - 1, r, r, 0, 90)
        path.AddArc(0, h - r - 1, r, r, 90, 90)
        path.CloseFigure()

        Using bBg As New SolidBrush(Theme.PanelBg)
            g.FillPath(bBg, path)
        End Using

        Dim cSize As Integer = 60
        Dim cX As Integer = w - cSize - 20
        Dim cY As Integer = (h - cSize) \ 2

        Using bCir As New SolidBrush(Color.FromArgb(25, accent))
            g.FillEllipse(bCir, cX, cY, cSize, cSize)
        End Using

        Using bIcon As New SolidBrush(accent)
            Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
            Dim fIcon As Font = If(icon = "Rp", New Font("Segoe UI", 18, FontStyle.Bold), New Font("Segoe UI Emoji", 24))
            g.DrawString(icon, fIcon, bIcon, New Rectangle(cX, cY + 2, cSize, cSize), sf)
        End Using

        Using bT As New SolidBrush(Theme.TextSub)
            g.DrawString(title, New Font("Segoe UI", 9, FontStyle.Bold), bT, 20, 25)
        End Using
        Using bV As New SolidBrush(Theme.TextMain)
            Dim fSize As Single = 22
            If value.Length > 10 Then fSize = 18
            g.DrawString(value, New Font("Segoe UI", fSize, FontStyle.Bold), bV, 18, 55)
        End Using

        Using bS As New SolidBrush(accent)
            g.FillRectangle(bS, 0, 15, 5, h - 30)
        End Using
    End Sub

    Private Sub DrawResponsiveChart(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        If ListKategori.Count = 0 Then Exit Sub

        Dim maxVal As Integer = ListKategori.Max(Function(k) k.Jumlah)
        If maxVal = 0 Then maxVal = 1

        Dim w As Integer = pnlChart.Width
        Dim yPos As Integer = 15
        Dim maxBarW As Integer = w - 200
        If maxBarW < 50 Then maxBarW = 50

        For Each item In ListKategori
            Using bTxt As New SolidBrush(Theme.TextSub)
                g.DrawString(item.Nama, New Font("Segoe UI", 9, FontStyle.Bold), bTxt, 10, yPos + 5)
            End Using

            ' Function Rounded
            Dim GetRoundedRect As Func(Of Rectangle, Integer, GraphicsPath) = Function(rect, radius)
                                                                                  Dim p As New GraphicsPath()
                                                                                  p.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
                                                                                  p.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
                                                                                  p.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
                                                                                  p.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
                                                                                  p.CloseFigure()
                                                                                  Return p
                                                                              End Function

            Dim rectTrack As New Rectangle(130, yPos + 5, maxBarW, 25)
            Using bTrack As New SolidBrush(Color.FromArgb(240, 242, 245))
                g.FillPath(bTrack, GetRoundedRect(rectTrack, 8))
            End Using

            Dim itemW As Integer = CInt((item.Jumlah / maxVal) * maxBarW)
            If itemW < 15 Then itemW = 15
            Dim rectBar As New Rectangle(130, yPos + 5, itemW, 25)

            Using bBar As New SolidBrush(item.Warna)
                g.FillPath(bBar, GetRoundedRect(rectBar, 8))
            End Using

            Using bNum As New SolidBrush(Theme.TextMain)
                g.DrawString(item.Jumlah.ToString() & " Units", New Font("Segoe UI", 9, FontStyle.Bold), bNum, 130 + maxBarW + 10, yPos + 7)
            End Using
            yPos += 55
        Next
    End Sub

    Private Sub dgvRecent_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs)
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 1 Then
            e.PaintBackground(e.CellBounds, True)
            Dim raw As String = If(e.FormattedValue Is Nothing, "", e.FormattedValue.ToString())
            Dim bg As Color = Color.WhiteSmoke
            Dim txt As Color = Color.Gray

            Select Case raw.ToLower()
                Case "baik", "good" : bg = Color.FromArgb(220, 252, 231) : txt = Color.FromArgb(22, 163, 74)
                Case "rusak", "damaged" : bg = Color.FromArgb(254, 226, 226) : txt = Color.FromArgb(220, 38, 38)
                Case "perlu servis", "service needed" : bg = Color.FromArgb(255, 237, 213) : txt = Color.FromArgb(194, 65, 12)
            End Select

            Dim w As Integer = 110 : Dim h As Integer = 26
            Dim x As Integer = e.CellBounds.X + (e.CellBounds.Width - w) \ 2
            Dim y As Integer = e.CellBounds.Y + (e.CellBounds.Height - h) \ 2
            If x < e.CellBounds.X Then x = e.CellBounds.X

            ' Function Rounded Local
            Dim GetRoundedRect As Func(Of Rectangle, Integer, GraphicsPath) = Function(rect, radius)
                                                                                  Dim p As New GraphicsPath()
                                                                                  p.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
                                                                                  p.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90)
                                                                                  p.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90)
                                                                                  p.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90)
                                                                                  p.CloseFigure()
                                                                                  Return p
                                                                              End Function

            Using b As New SolidBrush(bg)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
                e.Graphics.FillPath(b, GetRoundedRect(New Rectangle(x, y, w, h), 8))
            End Using
            TextRenderer.DrawText(e.Graphics, raw, New Font("Segoe UI", 8, FontStyle.Bold), New Rectangle(x, y, w, h), txt, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            e.Handled = True
        End If
    End Sub

    Private Sub ApplyTheme(dark As Boolean)
        If dark Then
            Theme.Bg = Color.FromArgb(15, 23, 42) : Theme.PanelBg = Color.FromArgb(30, 41, 59)
            Theme.TextMain = Color.White : Theme.TextSub = Color.FromArgb(148, 163, 184)
            Theme.Accent1 = Color.FromArgb(129, 140, 248) : Theme.Accent2 = Color.FromArgb(52, 211, 153) : Theme.Accent3 = Color.FromArgb(251, 113, 133)
            Theme.GridLine = Color.FromArgb(71, 85, 105)
            btnMode.Text = "☀" : btnMode.ForeColor = Color.Gold
        Else
            Theme.Bg = Color.FromArgb(248, 250, 252) : Theme.PanelBg = Color.White
            Theme.TextMain = Color.FromArgb(15, 23, 42) : Theme.TextSub = Color.FromArgb(100, 116, 139)
            Theme.Accent1 = Color.FromArgb(79, 70, 229) : Theme.Accent2 = Color.FromArgb(16, 185, 129) : Theme.Accent3 = Color.FromArgb(244, 63, 94)
            Theme.GridLine = Color.FromArgb(226, 232, 240)
            btnMode.Text = "☾" : btnMode.ForeColor = Color.Gray
        End If
        Me.BackColor = Theme.Bg : PnlHeader.BackColor = Theme.PanelBg : Separator.BackColor = Theme.GridLine
        lblBrand.ForeColor = Theme.TextMain : lblSubHeader.ForeColor = Theme.TextSub
        lblChartTitle.ForeColor = Theme.TextMain : lblRecentTitle.ForeColor = Theme.TextMain
        pnlChartContainer.BackColor = Theme.PanelBg : pnlRecentContainer.BackColor = Theme.PanelBg : pnlChart.BackColor = Theme.PanelBg

        dgvRecent.BackgroundColor = Theme.PanelBg : dgvRecent.GridColor = Theme.GridLine
        dgvRecent.DefaultCellStyle.BackColor = Theme.PanelBg : dgvRecent.DefaultCellStyle.ForeColor = Theme.TextMain
        dgvRecent.ColumnHeadersDefaultCellStyle.BackColor = Theme.Bg : dgvRecent.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextSub
    End Sub

    Private Sub btnMode_Click(sender As Object, e As EventArgs) Handles btnMode.Click
        isDarkMode = Not isDarkMode : ApplyTheme(isDarkMode) : Me.Refresh()
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim opf As New OpenFileDialog() With {.Filter = "CSV Files (*.csv)|*.csv"}
        If opf.ShowDialog() = DialogResult.OK Then
            Dim lines = File.ReadAllLines(opf.FileName)
            Using con As New SQLiteConnection(connStr)
                con.Open()
                Using trans = con.BeginTransaction()
                    Try
                        For i As Integer = 1 To lines.Length - 1
                            Dim p = lines(i).Split(","c)
                            If p.Length < 8 Then Continue For
                            Dim cid = GetId(con, "categories", "nama_kategori", p(3).Trim())
                            Dim lid = GetId(con, "locations", "nama_lokasi", p(4).Trim())
                            Dim sql = "INSERT INTO assets (nama, merk, serial, category_id, location_id, kondisi, harga, tgl_beli) VALUES (@n, @m, @s, @c, @l, @k, @h, @t)"
                            Using cmd As New SQLiteCommand(sql, con)
                                cmd.Parameters.AddWithValue("@n", p(0)) : cmd.Parameters.AddWithValue("@m", p(1)) : cmd.Parameters.AddWithValue("@s", p(2))
                                cmd.Parameters.AddWithValue("@c", cid) : cmd.Parameters.AddWithValue("@l", lid) : cmd.Parameters.AddWithValue("@k", p(5))
                                cmd.Parameters.AddWithValue("@h", Val(p(6))) : cmd.Parameters.AddWithValue("@t", p(7))
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        trans.Commit() : LoadData()
                    Catch ex As Exception
                        trans.Rollback()
                    End Try
                End Using
            End Using
        End If
    End Sub
    Private Function GetId(con As SQLiteConnection, tbl As String, col As String, val As String) As Integer
        Dim res = New SQLiteCommand($"SELECT id FROM {tbl} WHERE {col} = '{val}'", con).ExecuteScalar()
        Return If(res IsNot Nothing, Convert.ToInt32(res), Convert.ToInt32(New SQLiteCommand($"INSERT INTO {tbl} ({col}) VALUES ('{val}'); SELECT last_insert_rowid()", con).ExecuteScalar()))
    End Function
    Private Sub CreateDatabaseIfNotExists()
        If Not File.Exists(dbName) Then
            SQLiteConnection.CreateFile(dbName)
            Using con As New SQLiteConnection(connStr)
                con.Open() : Dim cmd As New SQLiteCommand(con)
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS categories (id INTEGER PRIMARY KEY AUTOINCREMENT, nama_kategori TEXT)" : cmd.ExecuteNonQuery()
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS locations (id INTEGER PRIMARY KEY AUTOINCREMENT, nama_lokasi TEXT)" : cmd.ExecuteNonQuery()
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS assets (id INTEGER PRIMARY KEY AUTOINCREMENT, nama TEXT, merk TEXT, serial TEXT, category_id INTEGER, location_id INTEGER, kondisi TEXT, harga REAL, tgl_beli TEXT)" : cmd.ExecuteNonQuery()
            End Using
        End If
    End Sub
End Class

Public Class ModernPanel
    Inherits Panel
    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
    End Sub
End Class