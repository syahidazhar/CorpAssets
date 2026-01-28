<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LogView
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend WithEvents PnlHeader As System.Windows.Forms.Panel
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents dgvLog As System.Windows.Forms.DataGridView
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()

        Me.PnlHeader = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.dgvLog = New System.Windows.Forms.DataGridView()

        Me.SuspendLayout()
        Me.Size = New System.Drawing.Size(800, 500)
        Me.BackColor = System.Drawing.Color.White

        ' --- HEADER PANEL ---
        Me.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlHeader.Height = 60
        Me.PnlHeader.BackColor = System.Drawing.Color.WhiteSmoke

        ' JUDUL
        Me.LblTitle.Text = "📜 SYSTEM ACTIVITY LOGS"
        Me.LblTitle.Font = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        Me.LblTitle.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64)
        Me.LblTitle.Location = New System.Drawing.Point(20, 15)
        Me.LblTitle.AutoSize = True

        ' TOMBOL EXPORT CSV
        Me.btnExport.Text = "📥 EXPORT CSV"
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        Me.btnExport.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.FlatAppearance.BorderSize = 0
        Me.btnExport.Size = New System.Drawing.Size(120, 35)
        Me.btnExport.Location = New System.Drawing.Point(650, 12)
        Me.btnExport.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand

        Me.PnlHeader.Controls.Add(Me.LblTitle)
        Me.PnlHeader.Controls.Add(Me.btnExport)

        ' --- DATAGRIDVIEW (TABEL LOG) ---
        Me.dgvLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLog.BackgroundColor = System.Drawing.Color.White
        Me.dgvLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvLog.RowHeadersVisible = False
        Me.dgvLog.AllowUserToAddRows = False
        Me.dgvLog.ReadOnly = True
        Me.dgvLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill

        ' Styling Header Tabel
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(45, 45, 60)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
        Me.dgvLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLog.ColumnHeadersHeight = 35
        Me.dgvLog.EnableHeadersVisualStyles = False

        Me.Controls.Add(Me.dgvLog)
        Me.Controls.Add(Me.PnlHeader)
        Me.ResumeLayout(False)
    End Sub
End Class