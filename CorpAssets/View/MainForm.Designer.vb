<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ' Komponen UI
    Friend WithEvents PnlSidebar As System.Windows.Forms.Panel
    Friend WithEvents PnlLogo As System.Windows.Forms.Panel
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents LblSubTitle As System.Windows.Forms.Label
    Friend WithEvents PnlMenuContainer As System.Windows.Forms.Panel

    Friend WithEvents btnDash As System.Windows.Forms.Button
    Friend WithEvents btnAset As System.Windows.Forms.Button
    Friend WithEvents btnLapor As System.Windows.Forms.Button
    Friend WithEvents btnLog As System.Windows.Forms.Button
    Friend WithEvents btnOut As System.Windows.Forms.Button

    Friend WithEvents PnlContent As System.Windows.Forms.Panel
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.PnlSidebar = New System.Windows.Forms.Panel()
        Me.PnlLogo = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.LblSubTitle = New System.Windows.Forms.Label()
        Me.PnlMenuContainer = New System.Windows.Forms.Panel()
        Me.btnDash = New System.Windows.Forms.Button()
        Me.btnAset = New System.Windows.Forms.Button()
        Me.btnLapor = New System.Windows.Forms.Button()
        Me.btnLog = New System.Windows.Forms.Button()
        Me.btnOut = New System.Windows.Forms.Button()
        Me.PnlContent = New System.Windows.Forms.Panel()

        ' --- FORM UTAMA ---
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Text = "CORPASSETS - Smart Inventory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)

        ' --- SIDEBAR ---
        Me.PnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.PnlSidebar.Width = 240
        Me.PnlSidebar.BackColor = System.Drawing.Color.FromArgb(30, 30, 45)

        ' --- LOGO (ATAS) ---
        Me.PnlLogo.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlLogo.Height = 100
        Me.PnlLogo.BackColor = System.Drawing.Color.FromArgb(25, 25, 35)

        Me.LblTitle.Text = "CORP ASSETS"
        Me.LblTitle.ForeColor = System.Drawing.Color.White
        Me.LblTitle.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        Me.LblTitle.AutoSize = True
        Me.LblTitle.Location = New System.Drawing.Point(20, 30)

        Me.LblSubTitle.Text = "Kelompok 5 System"
        Me.LblSubTitle.ForeColor = System.Drawing.Color.Gray
        Me.LblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9)
        Me.LblSubTitle.AutoSize = True
        Me.LblSubTitle.Location = New System.Drawing.Point(22, 60)
        Me.PnlLogo.Controls.Add(Me.LblTitle)
        Me.PnlLogo.Controls.Add(Me.LblSubTitle)

        ' --- TOMBOL LOGOUT (BAWAH) ---
        ' Ini diset DOCK BOTTOM biar nempel tanah
        Me.btnOut.Text = "🚪 LOGOUT"
        Me.btnOut.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnOut.Height = 55
        Me.btnOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOut.FlatAppearance.BorderSize = 0
        Me.btnOut.BackColor = System.Drawing.Color.FromArgb(220, 53, 69)
        Me.btnOut.ForeColor = System.Drawing.Color.White
        Me.btnOut.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        Me.btnOut.Cursor = System.Windows.Forms.Cursors.Hand

        ' --- MENU CONTAINER (TENGAH) ---
        Me.PnlMenuContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMenuContainer.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)

        ' Setup Tombol Menu (Pakai helper method nanti di .vb)
        SetupMenuButton(Me.btnLog, "📜  System Logs")
        SetupMenuButton(Me.btnLapor, "📊  Laporan")
        SetupMenuButton(Me.btnAset, "📦  Data Aset")
        SetupMenuButton(Me.btnDash, "🏠  Dashboard")

        ' Masukkan ke Container (Urutan Add dibalik karena Dock Top)
        Me.PnlMenuContainer.Controls.Add(Me.btnLog)
        Me.PnlMenuContainer.Controls.Add(Me.btnLapor)
        Me.PnlMenuContainer.Controls.Add(Me.btnAset)
        Me.PnlMenuContainer.Controls.Add(Me.btnDash)

        ' --- RAKIT SIDEBAR (URUTAN PENTING UNTUK DOCKING) ---
        ' 1. Masukkan Menu Container (Fill) - dia akan menempati sisa ruang
        Me.PnlSidebar.Controls.Add(Me.PnlMenuContainer)
        ' 2. Masukkan Logo (Top)
        Me.PnlSidebar.Controls.Add(Me.PnlLogo)
        ' 3. Masukkan Logout (Bottom)
        Me.PnlSidebar.Controls.Add(Me.btnOut)

        ' --- CONTENT PANEL ---
        Me.PnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlContent.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PnlContent.Padding = New System.Windows.Forms.Padding(20)

        Me.Controls.Add(Me.PnlContent)
        Me.Controls.Add(Me.PnlSidebar)
    End Sub

    Private Sub SetupMenuButton(btn As System.Windows.Forms.Button, text As String)
        btn.Text = text
        btn.Dock = System.Windows.Forms.DockStyle.Top
        btn.Height = 55
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.BackColor = System.Drawing.Color.Transparent
        btn.ForeColor = System.Drawing.Color.Silver
        btn.Font = New System.Drawing.Font("Segoe UI", 11)
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        btn.Padding = New System.Windows.Forms.Padding(25, 0, 0, 0)
        btn.Cursor = System.Windows.Forms.Cursors.Hand
    End Sub
End Class