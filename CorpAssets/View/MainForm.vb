Imports System.Data.SQLite
Imports CorpAssets.Config ' Sesuaikan nama project

Public Class MainForm
    ' Variabel View
    Private V_Login As LoginView
    Private V_Dashboard As DashboardView
    Private V_Aset As AssetView
    Private V_Laporan As ReportView
    Private V_Log As LogView

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppConfig.InitDatabase()
        TampilLogin()
    End Sub

    ' --- MANAJEMEN NAVIGASI ---

    Sub TampilLogin()
        PnlSidebar.Visible = False ' Sembunyikan Sidebar saat Login
        PnlContent.Controls.Clear()

        V_Login = New LoginView()
        V_Login.Dock = DockStyle.Fill
        AddHandler V_Login.LoginBerhasil, AddressOf OnLoginSuccess
        PnlContent.Controls.Add(V_Login)
    End Sub

    Sub OnLoginSuccess()
        PnlSidebar.Visible = True ' Munculkan Sidebar
        btnDash.PerformClick() ' Otomatis klik dashboard
    End Sub

    Sub GantiHalaman(halaman As UserControl)
        PnlContent.Controls.Clear()
        halaman.Dock = DockStyle.Fill
        PnlContent.Controls.Add(halaman)
    End Sub

    ' --- LOGIKA HIGHLIGHT TOMBOL (Biar cantik) ---
    Private Sub SetActiveButton(btnAktif As Button)
        ' 1. Reset semua tombol jadi warna biasa
        For Each ctrl As Control In PnlMenuContainer.Controls
            If TypeOf ctrl Is Button Then
                ctrl.BackColor = Color.Transparent
                ctrl.ForeColor = Color.Silver
            End If
        Next

        ' 2. Highlight tombol yang diklik
        btnAktif.BackColor = Color.FromArgb(45, 45, 60) ' Sedikit lebih terang
        btnAktif.ForeColor = Color.White
    End Sub

    ' --- EVENT HANDLERS MENU ---

    Private Sub btnDash_Click(s As Object, e As EventArgs) Handles btnDash.Click
        SetActiveButton(btnDash)
        V_Dashboard = New DashboardView()
        V_Dashboard.LoadData()
        GantiHalaman(V_Dashboard)
    End Sub

    Private Sub btnAset_Click(s As Object, e As EventArgs) Handles btnAset.Click
        SetActiveButton(btnAset)
        V_Aset = New AssetView()
        V_Aset.LoadData()
        GantiHalaman(V_Aset)
    End Sub

    Private Sub btnLapor_Click(s As Object, e As EventArgs) Handles btnLapor.Click
        SetActiveButton(btnLapor)
        V_Laporan = New ReportView()
        GantiHalaman(V_Laporan)
    End Sub

    Private Sub btnLog_Click(s As Object, e As EventArgs) Handles btnLog.Click
        SetActiveButton(btnLog)
        V_Log = New LogView()
        V_Log.LoadLogs()
        GantiHalaman(V_Log)
    End Sub

    Private Sub btnOut_Click(s As Object, e As EventArgs) Handles btnOut.Click
        If MsgBox("Yakin mau logout?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            AppConfig.CatatLog("Logout")
            AppConfig.CurrentUser = ""
            TampilLogin()
        End If
    End Sub
End Class