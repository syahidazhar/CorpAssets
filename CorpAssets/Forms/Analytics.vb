Imports System.Data.SQLite
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class AnalyticsForm
    Inherits System.Windows.Forms.Form

    ' --- CONFIG DATABASE ---
    Private dbName As String = "CorpAssets.db"
    Private connStr As String = "Data Source=" & dbName & "; Version=3;"

    ' --- DATA HOLDERS ---
    Private Structure LocStat
        Public Nama As String
        Public Nilai As Double
    End Structure
    Private Structure CondStat
        Public Kondisi As String
        Public Jumlah As Integer
        Public Warna As Color
    End Structure
    Private Structure YearStat
        Public Tahun As String
        Public Jumlah As Integer
    End Structure
    Private Structure CatStat
        Public Kategori As String
        Public Jumlah As Integer
        Public Persen As Single
    End Structure

    Private ListLoc As New List(Of LocStat)
    Private ListCond As New List(Of CondStat)
    Private ListYear As New List(Of YearStat)
    Private ListCat As New List(Of CatStat)

    ' --- COLORS ---
    Private C_Primary As Color = Color.FromArgb(59, 130, 246)
    Private C_Success As Color = Color.FromArgb(16, 185, 129)
    Private C_Warning As Color = Color.FromArgb(245, 158, 11)
    Private C_Danger As Color = Color.FromArgb(239, 68, 68)
    Private C_Text As Color = Color.FromArgb(31, 41, 55)
    Private C_SubText As Color = Color.FromArgb(107, 114, 128)
    Private C_Track As Color = Color.FromArgb(243, 244, 246)

    Public Sub New()
        InitializeComponent()

        ' Manual Handlers
        AddHandler pnlLoc.Paint, AddressOf pnlLoc_Paint
        AddHandler pnlDonut.Paint, AddressOf pnlDonut_Paint
        AddHandler pnlTrend.Paint, AddressOf pnlTrend_Paint
        AddHandler pnlCat.Paint, AddressOf pnlCat_Paint

        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
    End Sub

    Private Sub AnalyticsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataFromDB()
    End Sub

    ' --- DATA LOADING ---
    Private Sub LoadDataFromDB()
        If Not File.Exists(dbName) Then Exit Sub
        Try
            Using con As New SQLiteConnection(connStr)
                con.Open()
                Dim cmd As New SQLiteCommand(con)

                ' 1. Lokasi
                ListLoc.Clear()
                cmd.CommandText = "SELECT l.nama_lokasi, IFNULL(SUM(a.harga), 0) as total FROM locations l LEFT JOIN assets a ON a.location_id = l.id GROUP BY l.nama_lokasi ORDER BY total DESC LIMIT 5"
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        ListLoc.Add(New LocStat With {.Nama = rdr("nama_lokasi").ToString(), .Nilai = Convert.ToDouble(rdr("total"))})
                    End While
                End Using

                ' 2. Kondisi
                ListCond.Clear()
                cmd.CommandText = "SELECT kondisi, COUNT(*) as jum FROM assets GROUP BY kondisi"
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim k As String = rdr("kondisi").ToString()
                        Dim c As Color = C_SubText
                        If k.ToLower().Contains("baik") Or k.ToLower().Contains("good") Then c = C_Success
                        If k.ToLower().Contains("rusak") Or k.ToLower().Contains("damaged") Then c = C_Danger
                        If k.ToLower().Contains("servis") Or k.ToLower().Contains("perlu") Then c = C_Warning
                        ListCond.Add(New CondStat With {.Kondisi = k, .Jumlah = Convert.ToInt32(rdr("jum")), .Warna = c})
                    End While
                End Using

                ' 3. Trend
                ListYear.Clear()
                cmd.CommandText = "SELECT substr(tgl_beli, 1, 4) as thn, COUNT(*) as jum FROM assets WHERE length(tgl_beli) >= 4 GROUP BY thn ORDER BY thn ASC LIMIT 5"
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        ListYear.Add(New YearStat With {.Tahun = rdr("thn").ToString(), .Jumlah = Convert.ToInt32(rdr("jum"))})
                    End While
                End Using

                ' 4. Kategori
                ListCat.Clear()
                Dim totalAsset As Integer = 0
                cmd.CommandText = "SELECT COUNT(*) FROM assets"
                totalAsset = Convert.ToInt32(cmd.ExecuteScalar())

                cmd.CommandText = "SELECT c.nama_kategori, COUNT(a.id) as jum FROM categories c LEFT JOIN assets a ON a.category_id = c.id GROUP BY c.nama_kategori ORDER BY jum DESC LIMIT 5"
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim jum As Integer = Convert.ToInt32(rdr("jum"))
                        Dim pct As Single = If(totalAsset = 0, 0, CSng(jum) / CSng(totalAsset))
                        ListCat.Add(New CatStat With {.Kategori = rdr("nama_kategori").ToString(), .Jumlah = jum, .Persen = pct})
                    End While
                End Using
            End Using
        Catch ex As Exception
        End Try

        pnlLoc.Invalidate() : pnlDonut.Invalidate()
        pnlTrend.Invalidate() : pnlCat.Invalidate()
    End Sub

    ' ==========================================
    ' PAINTING METHODS (DENGAN CLEAR SCREEN)
    ' ==========================================

    Private Sub SetupGraphics(g As Graphics)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        g.Clear(Color.White) ' <--- INI SOLUSI HANTU (DOUBLE TEXT)
    End Sub

    Private Sub DrawHeader(g As Graphics, title As String, w As Integer)
        g.DrawString(title, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(C_Text), 15, 15)
        Using p As New Pen(Color.FromArgb(230, 230, 230), 1)
            g.DrawLine(p, 0, 40, w, 40)
        End Using
    End Sub

    ' 1. PANEL LOKASI
    Private Sub pnlLoc_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Financial Distribution", pnlLoc.Width)

        If ListLoc.Count = 0 Then Exit Sub

        Dim maxVal As Double = ListLoc.Max(Function(l) l.Nilai)
        If maxVal = 0 Then maxVal = 1

        Dim y As Single = 60
        ' Margin lebar agar teks tidak numpuk
        Dim labelW As Single = 120
        Dim valueW As Single = 110 ' Diperlebar
        Dim barAreaW As Single = pnlLoc.Width - labelW - valueW - 30
        If barAreaW < 50 Then barAreaW = 50

        For Each item In ListLoc
            g.DrawString(item.Nama, New Font("Segoe UI", 9), New SolidBrush(C_SubText), 15, y)

            Dim rectTrack As New RectangleF(labelW + 15, y + 4, barAreaW, 10)
            Using b As New SolidBrush(C_Track) : g.FillRectangle(b, rectTrack) : End Using

            Dim w As Single = CSng((item.Nilai / maxVal) * barAreaW)
            If w < 2 Then w = 2
            Dim rectBar As New RectangleF(labelW + 15, y + 4, w, 10)
            Using b As New SolidBrush(C_Primary) : g.FillRectangle(b, rectBar) : End Using

            Dim price As String = "Rp " & item.Nilai.ToString("N0")
            g.DrawString(price, New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(C_Text), rectTrack.Right + 5, y + 2)

            y += 40
        Next
    End Sub

    ' 2. PANEL DONUT
    Private Sub pnlDonut_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Asset Condition", pnlDonut.Width)

        If ListCond.Count = 0 Then Exit Sub
        Dim total As Integer = ListCond.Sum(Function(c) c.Jumlah)
        If total = 0 Then Exit Sub

        Dim contentH As Single = pnlDonut.Height - 40
        Dim radius As Single = Math.Min(pnlDonut.Width, contentH) * 0.35F
        Dim cx As Single = pnlDonut.Width * 0.35F
        Dim cy As Single = 40 + (contentH / 2)

        Dim rect As New RectangleF(cx - radius, cy - radius, radius * 2, radius * 2)
        Dim startAngle As Single = -90

        For Each item In ListCond
            Dim sweep As Single = CSng((item.Jumlah / total) * 360)
            Using b As New SolidBrush(item.Warna) : g.FillPie(b, Rectangle.Round(rect), startAngle, sweep) : End Using
            startAngle += sweep
        Next

        g.FillEllipse(Brushes.White, New RectangleF(cx - (radius * 0.6F), cy - (radius * 0.6F), radius * 1.2F, radius * 1.2F))

        Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        g.DrawString(total.ToString(), New Font("Segoe UI", 16, FontStyle.Bold), New SolidBrush(C_Text), cx, cy - 8, sf)
        g.DrawString("Total", New Font("Segoe UI", 8), New SolidBrush(C_SubText), cx, cy + 12, sf)

        ' Legend
        Dim lx As Single = cx + radius + 20
        Dim ly As Single = cy - radius
        For Each item In ListCond
            If lx + 100 < pnlDonut.Width Then
                Using b As New SolidBrush(item.Warna) : g.FillEllipse(b, lx, ly, 10, 10) : End Using
                g.DrawString(item.Kondisi, New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(C_Text), lx + 15, ly - 2)
                g.DrawString(item.Jumlah.ToString(), New Font("Segoe UI", 8), New SolidBrush(C_SubText), lx + 15, ly + 12)
                ly += 35
            End If
        Next
    End Sub

    ' 3. PANEL TREND
    Private Sub pnlTrend_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Acquisition Trends", pnlTrend.Width)

        If ListYear.Count = 0 Then Exit Sub
        Dim maxVal As Integer = ListYear.Max(Function(y) y.Jumlah)
        If maxVal = 0 Then maxVal = 1

        Dim marginL As Single = 40
        Dim chartW As Single = pnlTrend.Width - marginL - 30
        Dim chartH As Single = pnlTrend.Height - 80
        Dim chartTop As Single = 50
        Dim chartBot As Single = chartTop + chartH

        Using p As New Pen(Color.FromArgb(230, 230, 230), 1) : g.DrawLine(p, marginL, chartBot, marginL + chartW, chartBot) : End Using

        Dim barCount As Integer = ListYear.Count
        Dim slotW As Single = chartW / barCount
        Dim barW As Single = Math.Min(50.0F, slotW * 0.6F)
        Dim currentX As Single = marginL + (slotW / 2) - (barW / 2)

        For Each item In ListYear
            Dim h As Single = CSng((item.Jumlah / maxVal) * chartH)
            If h < 2 Then h = 2
            Dim rectBar As New RectangleF(currentX, chartBot - h, barW, h)
            Using b As New SolidBrush(C_Primary) : g.FillRectangle(b, rectBar) : End Using

            Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center}
            g.DrawString(item.Jumlah.ToString(), New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(C_Text), currentX + (barW / 2), chartBot - h - 15, sf)
            g.DrawString(item.Tahun, New Font("Segoe UI", 8), New SolidBrush(C_SubText), currentX + (barW / 2), chartBot + 5, sf)
            currentX += slotW
        Next
    End Sub

    ' 4. PANEL KATEGORI
    Private Sub pnlCat_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Inventory Composition", pnlCat.Width)

        If ListCat.Count = 0 Then Exit Sub

        Dim y As Single = 60
        ' Margin Kanan lebih lebar agar "Units" tidak terpotong
        Dim maxW As Single = pnlCat.Width - 40

        For Each item In ListCat
            g.DrawString(item.Kategori, New Font("Segoe UI", 9, FontStyle.Bold), New SolidBrush(C_Text), 20, y)

            Dim txtCount As String = item.Jumlah.ToString() & " Units"
            Dim sizeCount As SizeF = g.MeasureString(txtCount, New Font("Segoe UI", 9))
            ' Pastikan teks rata kanan tidak keluar batas
            g.DrawString(txtCount, New Font("Segoe UI", 9), New SolidBrush(C_SubText), pnlCat.Width - 20 - sizeCount.Width, y)

            y += 20
            Dim rectTrack As New RectangleF(20, y, maxW, 6)
            Using b As New SolidBrush(C_Track) : g.FillRectangle(b, rectTrack) : End Using

            Dim fillW As Single = maxW * item.Persen
            If fillW < 2 Then fillW = 2
            Dim rectFill As New RectangleF(20, y, fillW, 6)
            Dim barColor As Color = If(item.Persen > 0.5, C_Primary, C_Success)
            Using b As New SolidBrush(barColor) : g.FillRectangle(b, rectFill) : End Using

            y += 35
        Next
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadDataFromDB()
    End Sub
End Class