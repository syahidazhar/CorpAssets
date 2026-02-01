Imports System.Data.SQLite
Imports System.Drawing.Drawing2D
Imports System.IO
Imports CorpAssets.Config

Public Class ReportView
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

        ' Manual Handlers untuk Painting
        AddHandler pnlLoc.Paint, AddressOf pnlLoc_Paint
        AddHandler pnlDonut.Paint, AddressOf pnlDonut_Paint
        AddHandler pnlTrend.Paint, AddressOf pnlTrend_Paint
        AddHandler pnlCat.Paint, AddressOf pnlCat_Paint

        ' Optimasi Grafis (Anti Kedip)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
    End Sub

    Private Sub ReportView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataFromDB()
    End Sub

    ' ==========================================
    ' BAGIAN UTAMA: LOAD DATA (SESUAI SKEMA TEXT)
    ' ==========================================
    Public Sub LoadDataFromDB()
        If Not File.Exists(AppConfig.DB_NAME) Then Exit Sub

        Try
            Using con As New SQLiteConnection(AppConfig.CONN_STR)
                con.Open()
                Dim cmd As New SQLiteCommand(con)

                ' 1. LOKASI (Financial Distribution)
                ' Menggunakan kolom 'lokasi' (TEXT) sesuai InitDatabase kamu
                ListLoc.Clear()
                cmd.CommandText = "SELECT lokasi, IFNULL(SUM(harga), 0) as total " &
                                  "FROM assets " &
                                  "WHERE lokasi IS NOT NULL AND lokasi != '' " &
                                  "GROUP BY lokasi " &
                                  "ORDER BY total DESC LIMIT 5"

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        ListLoc.Add(New LocStat With {
                            .Nama = rdr("lokasi").ToString(),
                            .Nilai = Convert.ToDouble(rdr("total"))
                        })
                    End While
                End Using

                ' 2. KONDISI (Asset Condition)
                ListCond.Clear()
                ' UPDATE QUERY: Urutkan dari jumlah terbanyak (DESC) biar yang banyak muncul duluan
                cmd.CommandText = "SELECT kondisi, COUNT(*) as jum FROM assets GROUP BY kondisi ORDER BY jum DESC"

                ' Palet untuk kondisi aneh-aneh (Bocor, Kulit Sobek, dll)
                Dim palette() As Color = {
                    Color.FromArgb(99, 102, 241),  ' Indigo
                    Color.FromArgb(236, 72, 153),  ' Pink
                    Color.FromArgb(20, 184, 166),  ' Teal
                    Color.FromArgb(139, 92, 246),  ' Violet
                    Color.FromArgb(107, 114, 128), ' Gray
                    Color.FromArgb(59, 130, 246)   ' Blue
                }
                Dim palIndex As Integer = 0

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim k As String = rdr("kondisi").ToString()
                        Dim kl As String = k.ToLower()
                        Dim c As Color

                        ' LOGIKA WARNA PRIORITAS
                        If kl.Contains("baik") Or kl.Contains("good") Then
                            c = C_Success ' Hijau
                        ElseIf kl.Contains("rusak") Or kl.Contains("mati") Or kl.Contains("hancur") Then
                            c = C_Danger ' Merah
                        ElseIf kl.Contains("servis") Or kl.Contains("perlu") Or kl.Contains("macet") Then
                            c = C_Warning ' Kuning/Orange
                        ElseIf kl.Contains("disposal") Then
                            c = Color.DimGray ' Abu Gelap
                        Else
                            ' Sisanya (Bocor, Gores, dll) pakai warna-warni giliran
                            c = palette(palIndex Mod palette.Length)
                            palIndex += 1
                        End If

                        ListCond.Add(New CondStat With {.Kondisi = k, .Jumlah = Convert.ToInt32(rdr("jum")), .Warna = c})
                    End While
                End Using

                ' 3. TREND (Tahun Pembelian)
                ListYear.Clear()
                cmd.CommandText = "SELECT substr(tgl_beli, 1, 4) as thn, COUNT(*) as jum FROM assets WHERE length(tgl_beli) >= 4 GROUP BY thn ORDER BY thn ASC LIMIT 5"
                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        ListYear.Add(New YearStat With {.Tahun = rdr("thn").ToString(), .Jumlah = Convert.ToInt32(rdr("jum"))})
                    End While
                End Using

                ' 4. KATEGORI (Inventory Composition)
                ' Menggunakan kolom 'kategori' (TEXT) sesuai InitDatabase kamu
                ListCat.Clear()
                Dim totalAsset As Integer = 0
                cmd.CommandText = "SELECT COUNT(*) FROM assets"
                totalAsset = Convert.ToInt32(cmd.ExecuteScalar())

                cmd.CommandText = "SELECT kategori, COUNT(*) as jum " &
                                  "FROM assets " &
                                  "WHERE kategori IS NOT NULL AND kategori != '' " &
                                  "GROUP BY kategori " &
                                  "ORDER BY jum DESC LIMIT 5"

                Using rdr = cmd.ExecuteReader()
                    While rdr.Read()
                        Dim jum As Integer = Convert.ToInt32(rdr("jum"))
                        Dim pct As Single = If(totalAsset = 0, 0, CSng(jum) / CSng(totalAsset))
                        ListCat.Add(New CatStat With {
                            .Kategori = rdr("kategori").ToString(),
                            .Jumlah = jum,
                            .Persen = pct
                        })
                    End While
                End Using
            End Using
        Catch ex As Exception
            ' Silent error agar UI tidak crash, bisa diganti Debug.WriteLine
        End Try

        ' Redraw Semua Panel
        pnlLoc.Invalidate() : pnlDonut.Invalidate()
        pnlTrend.Invalidate() : pnlCat.Invalidate()
    End Sub

    ' ==========================================
    ' BAGIAN PAINTING (VISUALISASI)
    ' ==========================================

    Private Sub SetupGraphics(g As Graphics)
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        g.Clear(Color.White)
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
        DrawHeader(g, "Financial Distribution (By Location)", pnlLoc.Width)

        If ListLoc.Count = 0 Then Exit Sub

        Dim maxVal As Double = ListLoc.Max(Function(l) l.Nilai)
        If maxVal = 0 Then maxVal = 1

        Dim y As Single = 60
        Dim labelW As Single = 120
        Dim valueW As Single = 110
        Dim barAreaW As Single = pnlLoc.Width - labelW - valueW - 30

        ' Safety check layout
        If barAreaW < 50 Then barAreaW = 50

        For Each item In ListLoc
            ' Nama Lokasi
            g.DrawString(item.Nama, New Font("Segoe UI", 9), New SolidBrush(C_SubText), 15, y)

            ' Bar Background
            Dim rectTrack As New RectangleF(labelW + 15, y + 4, barAreaW, 10)
            Using b As New SolidBrush(C_Track) : g.FillRectangle(b, rectTrack) : End Using

            ' Bar Value
            Dim w As Single = CSng((item.Nilai / maxVal) * barAreaW)
            If w < 2 Then w = 2
            Dim rectBar As New RectangleF(labelW + 15, y + 4, w, 10)
            Using b As New SolidBrush(C_Primary) : g.FillRectangle(b, rectBar) : End Using

            ' Teks Rupiah
            Dim price As String = "Rp " & item.Nilai.ToString("N0")
            g.DrawString(price, New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(C_Text), rectTrack.Right + 5, y + 2)

            y += 40
        Next
    End Sub

    ' 2. PANEL DONUT (DENGAN LAYOUT RESPONSIF BIAR GAK KEPOTONG)
    Private Sub pnlDonut_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Asset Condition", pnlDonut.Width)

        If ListCond.Count = 0 Then Exit Sub
        Dim total As Integer = ListCond.Sum(Function(c) c.Jumlah)
        If total = 0 Then Exit Sub

        ' --- LAYOUT ---
        Dim availableHeight As Single = pnlDonut.Height - 60
        Dim radius As Single = Math.Min(pnlDonut.Width * 0.3F, availableHeight * 0.45F) ' Radius sedikit diperbesar
        Dim cx As Single = pnlDonut.Width * 0.3F
        Dim cy As Single = 50 + (availableHeight / 2)

        Dim rect As New RectangleF(cx - radius, cy - radius, radius * 2, radius * 2)
        Dim startAngle As Single = -90

        ' Gambar Pie
        For Each item In ListCond
            Dim sweep As Single = CSng((item.Jumlah / total) * 360)
            ' Pastikan minimal kelihatan (1 derajat) kalau datanya kecil
            If sweep < 1 Then sweep = 1
            Using b As New SolidBrush(item.Warna) : g.FillPie(b, Rectangle.Round(rect), startAngle, sweep) : End Using
            startAngle += sweep
        Next

        ' Lubang Donut
        g.FillEllipse(Brushes.White, New RectangleF(cx - (radius * 0.6F), cy - (radius * 0.6F), radius * 1.2F, radius * 1.2F))

        ' Teks Total
        Dim sf As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        g.DrawString(total.ToString(), New Font("Segoe UI", 14, FontStyle.Bold), New SolidBrush(C_Text), cx, cy - 8, sf)
        g.DrawString("Total", New Font("Segoe UI", 8), New SolidBrush(C_SubText), cx, cy + 12, sf)

        ' --- LEGENDA (LEBIH RAPAT) ---
        Dim lx As Single = cx + radius + 25
        Dim ly As Single = 55 ' Mulai lebih atas sedikit

        For Each item In ListCond
            If ly + 25 > pnlDonut.Height Then Exit For ' Batas bawah

            ' Dot Warna
            Using b As New SolidBrush(item.Warna) : g.FillEllipse(b, lx, ly + 2, 8, 8) : End Using

            ' Teks (Satu baris saja biar muat banyak: "Baik (10)")
            Dim text As String = item.Kondisi & " (" & item.Jumlah & ")"

            ' Kalau teks kepanjangan, potong
            If text.Length > 20 Then text = text.Substring(0, 18) & "..."

            g.DrawString(text, New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(C_Text), lx + 12, ly)

            ' Jarak diperpendek dari 35 jadi 22 biar muat banyak
            ly += 22
        Next
    End Sub

    ' 3. PANEL TREND
    Private Sub pnlTrend_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Acquisition Trends (Yearly)", pnlTrend.Width)

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

    ' 4. PANEL KATEGORI (RESPONSIF)
    Private Sub pnlCat_Paint(sender As Object, e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        SetupGraphics(g)
        DrawHeader(g, "Inventory Composition", pnlCat.Width)

        If ListCat.Count = 0 Then Exit Sub

        Dim y As Single = 60
        For Each item In ListCat
            If y + 40 > pnlCat.Height Then Exit For

            g.DrawString(item.Kategori, New Font("Segoe UI", 9, FontStyle.Bold), New SolidBrush(C_Text), 20, y)

            Dim txtCount As String = item.Jumlah.ToString() & " Units"
            Dim sizeCount As SizeF = g.MeasureString(txtCount, New Font("Segoe UI", 9))
            Dim textX As Single = pnlCat.Width - sizeCount.Width - 20
            g.DrawString(txtCount, New Font("Segoe UI", 9), New SolidBrush(C_SubText), textX, y)

            y += 20
            Dim maxBarW As Single = pnlCat.Width - 40 - sizeCount.Width - 20

            Dim rectTrack As New RectangleF(20, y, maxBarW, 6)
            Using b As New SolidBrush(C_Track) : g.FillRectangle(b, rectTrack) : End Using

            Dim fillW As Single = maxBarW * item.Persen
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