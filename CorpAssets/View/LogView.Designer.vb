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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        PnlHeader = New Panel()
        LblTitle = New Label()
        btnExport = New Button()
        dgvLog = New DataGridView()
        PnlHeader.SuspendLayout()
        CType(dgvLog, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PnlHeader
        ' 
        PnlHeader.BackColor = Color.WhiteSmoke
        PnlHeader.Controls.Add(LblTitle)
        PnlHeader.Controls.Add(btnExport)
        PnlHeader.Dock = DockStyle.Top
        PnlHeader.Location = New Point(0, 0)
        PnlHeader.Name = "PnlHeader"
        PnlHeader.Size = New Size(800, 60)
        PnlHeader.TabIndex = 1
        ' 
        ' LblTitle
        ' 
        LblTitle.AutoSize = True
        LblTitle.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        LblTitle.ForeColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        LblTitle.Location = New Point(20, 15)
        LblTitle.Name = "LblTitle"
        LblTitle.Size = New Size(326, 32)
        LblTitle.TabIndex = 0
        LblTitle.Text = "📜 SYSTEM ACTIVITY LOGS"
        ' 
        ' btnExport
        ' 
        btnExport.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnExport.BackColor = Color.SteelBlue
        btnExport.Cursor = Cursors.Hand
        btnExport.FlatAppearance.BorderSize = 0
        btnExport.FlatStyle = FlatStyle.Flat
        btnExport.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        btnExport.ForeColor = Color.White
        btnExport.Location = New Point(1250, 12)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(120, 35)
        btnExport.TabIndex = 1
        btnExport.Text = "📥 EXPORT CSV"
        btnExport.UseVisualStyleBackColor = False
        ' 
        ' dgvLog
        ' 
        dgvLog.AllowUserToAddRows = False
        dgvLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvLog.BackgroundColor = Color.White
        dgvLog.BorderStyle = BorderStyle.None
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(45), CByte(45), CByte(60))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = Color.White
        dgvLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        dgvLog.ColumnHeadersHeight = 35
        dgvLog.Dock = DockStyle.Fill
        dgvLog.EnableHeadersVisualStyles = False
        dgvLog.Location = New Point(0, 60)
        dgvLog.Name = "dgvLog"
        dgvLog.ReadOnly = True
        dgvLog.RowHeadersVisible = False
        dgvLog.RowHeadersWidth = 51
        dgvLog.Size = New Size(800, 440)
        dgvLog.TabIndex = 0
        ' 
        ' LogView
        ' 
        BackColor = Color.White
        Controls.Add(dgvLog)
        Controls.Add(PnlHeader)
        Name = "LogView"
        Size = New Size(800, 500)
        PnlHeader.ResumeLayout(False)
        PnlHeader.PerformLayout()
        CType(dgvLog, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
End Class