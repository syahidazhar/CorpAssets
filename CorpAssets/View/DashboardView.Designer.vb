<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardView
    Inherits System.Windows.Forms.UserControl ' <-- Perhatikan ini berubah jadi UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    '--- KOMPONEN DARI A ---
    Friend WithEvents PnlHeader As Panel
    Friend WithEvents PnlBody As Panel
    Friend WithEvents TableLayoutStats As TableLayoutPanel
    Friend WithEvents TableLayoutMain As TableLayoutPanel
    Friend WithEvents lblBrand As Label
    Friend WithEvents lblSubHeader As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnImport As Button
    Friend WithEvents btnMode As Button
    Friend WithEvents Separator As Panel
    Friend WithEvents pnlChartContainer As Panel
    Friend WithEvents pnlChart As Panel
    Friend WithEvents lblChartTitle As Label
    Friend WithEvents pnlRecentContainer As Panel
    Friend WithEvents dgvRecent As DataGridView
    Friend WithEvents lblRecentTitle As Label
    Friend WithEvents pnlStat1 As Panel
    Friend WithEvents pnlStat2 As Panel
    Friend WithEvents pnlStat3 As Panel

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PnlHeader = New System.Windows.Forms.Panel()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.lblSubHeader = New System.Windows.Forms.Label()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnMode = New System.Windows.Forms.Button()
        Me.Separator = New System.Windows.Forms.Panel()
        Me.PnlBody = New System.Windows.Forms.Panel()
        Me.TableLayoutMain = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlChartContainer = New System.Windows.Forms.Panel()
        Me.lblChartTitle = New System.Windows.Forms.Label()
        Me.pnlChart = New System.Windows.Forms.Panel()
        Me.pnlRecentContainer = New System.Windows.Forms.Panel()
        Me.lblRecentTitle = New System.Windows.Forms.Label()
        Me.dgvRecent = New System.Windows.Forms.DataGridView()
        Me.TableLayoutStats = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlStat1 = New System.Windows.Forms.Panel()
        Me.pnlStat2 = New System.Windows.Forms.Panel()
        Me.pnlStat3 = New System.Windows.Forms.Panel()
        Me.PnlHeader.SuspendLayout()
        Me.PnlBody.SuspendLayout()
        Me.TableLayoutMain.SuspendLayout()
        Me.pnlChartContainer.SuspendLayout()
        Me.pnlRecentContainer.SuspendLayout()
        CType(Me.dgvRecent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutStats.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlHeader
        '
        Me.PnlHeader.BackColor = System.Drawing.Color.White
        Me.PnlHeader.Controls.Add(Me.lblBrand)
        Me.PnlHeader.Controls.Add(Me.lblSubHeader)
        Me.PnlHeader.Controls.Add(Me.btnImport)
        Me.PnlHeader.Controls.Add(Me.btnRefresh)
        Me.PnlHeader.Controls.Add(Me.btnMode)
        Me.PnlHeader.Controls.Add(Me.Separator)
        Me.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlHeader.Height = 90
        Me.PnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.PnlHeader.Name = "PnlHeader"
        Me.PnlHeader.Size = New System.Drawing.Size(1280, 90)
        Me.PnlHeader.TabIndex = 0
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.Font = New System.Drawing.Font("Segoe UI", 22.0!, System.Drawing.FontStyle.Bold)
        Me.lblBrand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(23, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.lblBrand.Location = New System.Drawing.Point(30, 20)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(193, 41)
        Me.lblBrand.TabIndex = 0
        Me.lblBrand.Text = "AssetMaster"
        '
        'lblSubHeader
        '
        Me.lblSubHeader.AutoSize = True
        Me.lblSubHeader.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubHeader.ForeColor = System.Drawing.Color.Gray
        Me.lblSubHeader.Location = New System.Drawing.Point(35, 60)
        Me.lblSubHeader.Name = "lblSubHeader"
        Me.lblSubHeader.Size = New System.Drawing.Size(215, 19)
        Me.lblSubHeader.TabIndex = 1
        Me.lblSubHeader.Text = "Monitoring && Tracking Dashboard"
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImport.BackColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(229, Byte), Integer))
        Me.btnImport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImport.FlatAppearance.BorderSize = 0
        Me.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImport.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnImport.ForeColor = System.Drawing.Color.White
        Me.btnImport.Location = New System.Drawing.Point(880, 25)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(130, 45)
        Me.btnImport.TabIndex = 2
        Me.btnImport.Text = "📂 IMPORT CSV"
        Me.btnImport.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.Gray
        Me.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1020, 25)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(130, 45)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "↻ REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnMode
        '
        Me.btnMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMode.FlatAppearance.BorderSize = 0
        Me.btnMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMode.Font = New System.Drawing.Font("Segoe UI", 14.0!)
        Me.btnMode.Location = New System.Drawing.Point(1160, 25)
        Me.btnMode.Name = "btnMode"
        Me.btnMode.Size = New System.Drawing.Size(50, 45)
        Me.btnMode.TabIndex = 4
        Me.btnMode.Text = "☾"
        Me.btnMode.UseVisualStyleBackColor = True
        '
        'Separator
        '
        Me.Separator.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Separator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Separator.Height = 1
        Me.Separator.Location = New System.Drawing.Point(0, 89)
        Me.Separator.Name = "Separator"
        Me.Separator.Size = New System.Drawing.Size(1280, 1)
        Me.Separator.TabIndex = 5
        '
        'PnlBody
        '
        Me.PnlBody.Controls.Add(Me.TableLayoutMain)
        Me.PnlBody.Controls.Add(Me.TableLayoutStats)
        Me.PnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlBody.Location = New System.Drawing.Point(0, 90)
        Me.PnlBody.Name = "PnlBody"
        Me.PnlBody.Padding = New System.Windows.Forms.Padding(30)
        Me.PnlBody.Size = New System.Drawing.Size(1280, 710)
        Me.PnlBody.TabIndex = 1
        '
        'TableLayoutStats
        '
        Me.TableLayoutStats.ColumnCount = 3
        Me.TableLayoutStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33!))
        Me.TableLayoutStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33!))
        Me.TableLayoutStats.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33!))
        Me.TableLayoutStats.Controls.Add(Me.pnlStat1, 0, 0)
        Me.TableLayoutStats.Controls.Add(Me.pnlStat2, 1, 0)
        Me.TableLayoutStats.Controls.Add(Me.pnlStat3, 2, 0)
        Me.TableLayoutStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutStats.Location = New System.Drawing.Point(30, 30)
        Me.TableLayoutStats.Name = "TableLayoutStats"
        Me.TableLayoutStats.RowCount = 1
        Me.TableLayoutStats.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutStats.Size = New System.Drawing.Size(1220, 170)
        Me.TableLayoutStats.TabIndex = 0
        '
        'pnlStat1
        '
        Me.pnlStat1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStat1.Location = New System.Drawing.Point(0, 0)
        Me.pnlStat1.Margin = New System.Windows.Forms.Padding(0, 0, 20, 20)
        Me.pnlStat1.Name = "pnlStat1"
        Me.pnlStat1.Size = New System.Drawing.Size(386, 150)
        Me.pnlStat1.TabIndex = 0
        '
        'pnlStat2
        '
        Me.pnlStat2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStat2.Location = New System.Drawing.Point(406, 0)
        Me.pnlStat2.Margin = New System.Windows.Forms.Padding(0, 0, 20, 20)
        Me.pnlStat2.Name = "pnlStat2"
        Me.pnlStat2.Size = New System.Drawing.Size(386, 150)
        Me.pnlStat2.TabIndex = 1
        '
        'pnlStat3
        '
        Me.pnlStat3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlStat3.Location = New System.Drawing.Point(812, 0)
        Me.pnlStat3.Margin = New System.Windows.Forms.Padding(0, 0, 0, 20)
        Me.pnlStat3.Name = "pnlStat3"
        Me.pnlStat3.Size = New System.Drawing.Size(408, 150)
        Me.pnlStat3.TabIndex = 2
        '
        'TableLayoutMain
        '
        Me.TableLayoutMain.ColumnCount = 2
        Me.TableLayoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutMain.Controls.Add(Me.pnlChartContainer, 0, 0)
        Me.TableLayoutMain.Controls.Add(Me.pnlRecentContainer, 1, 0)
        Me.TableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutMain.Location = New System.Drawing.Point(30, 200)
        Me.TableLayoutMain.Name = "TableLayoutMain"
        Me.TableLayoutMain.RowCount = 1
        Me.TableLayoutMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutMain.Size = New System.Drawing.Size(1220, 480)
        Me.TableLayoutMain.TabIndex = 1
        '
        'pnlChartContainer
        '
        Me.pnlChartContainer.Controls.Add(Me.lblChartTitle)
        Me.pnlChartContainer.Controls.Add(Me.pnlChart)
        Me.pnlChartContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlChartContainer.Location = New System.Drawing.Point(0, 0)
        Me.pnlChartContainer.Margin = New System.Windows.Forms.Padding(0, 0, 20, 0)
        Me.pnlChartContainer.Name = "pnlChartContainer"
        Me.pnlChartContainer.Size = New System.Drawing.Size(590, 480)
        Me.pnlChartContainer.TabIndex = 0
        '
        'lblChartTitle
        '
        Me.lblChartTitle.AutoSize = True
        Me.lblChartTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblChartTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblChartTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblChartTitle.Name = "lblChartTitle"
        Me.lblChartTitle.Size = New System.Drawing.Size(148, 21)
        Me.lblChartTitle.TabIndex = 0
        Me.lblChartTitle.Text = "Category Analysis"
        '
        'pnlChart
        '
        Me.pnlChart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlChart.Location = New System.Drawing.Point(15, 50)
        Me.pnlChart.Name = "pnlChart"
        Me.pnlChart.Size = New System.Drawing.Size(560, 415)
        Me.pnlChart.TabIndex = 1
        '
        'pnlRecentContainer
        '
        Me.pnlRecentContainer.Controls.Add(Me.lblRecentTitle)
        Me.pnlRecentContainer.Controls.Add(Me.dgvRecent)
        Me.pnlRecentContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRecentContainer.Location = New System.Drawing.Point(610, 0)
        Me.pnlRecentContainer.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlRecentContainer.Name = "pnlRecentContainer"
        Me.pnlRecentContainer.Size = New System.Drawing.Size(610, 480)
        Me.pnlRecentContainer.TabIndex = 1
        '
        'lblRecentTitle
        '
        Me.lblRecentTitle.AutoSize = True
        Me.lblRecentTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblRecentTitle.ForeColor = System.Drawing.Color.DimGray
        Me.lblRecentTitle.Location = New System.Drawing.Point(15, 15)
        Me.lblRecentTitle.Name = "lblRecentTitle"
        Me.lblRecentTitle.Size = New System.Drawing.Size(159, 21)
        Me.lblRecentTitle.TabIndex = 0
        Me.lblRecentTitle.Text = "Recent Asset Status"
        '
        'dgvRecent
        '
        Me.dgvRecent.AllowUserToAddRows = False
        Me.dgvRecent.AllowUserToDeleteRows = False
        Me.dgvRecent.AllowUserToResizeRows = False
        Me.dgvRecent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvRecent.BackgroundColor = System.Drawing.Color.White
        Me.dgvRecent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvRecent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvRecent.ColumnHeadersHeight = 45
        Me.dgvRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvRecent.EnableHeadersVisualStyles = False
        Me.dgvRecent.Location = New System.Drawing.Point(15, 50)
        Me.dgvRecent.Name = "dgvRecent"
        Me.dgvRecent.ReadOnly = True
        Me.dgvRecent.RowHeadersVisible = False
        Me.dgvRecent.RowTemplate.Height = 45
        Me.dgvRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRecent.Size = New System.Drawing.Size(580, 415)
        Me.dgvRecent.TabIndex = 1
        '
        'DashboardView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.Size = New System.Drawing.Size(1280, 800) ' <-- Perhatikan ini pakai Size, bukan ClientSize
        Me.Controls.Add(Me.PnlBody)
        Me.Controls.Add(Me.PnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Name = "DashboardView"
        Me.PnlHeader.ResumeLayout(False)
        Me.PnlHeader.PerformLayout()
        Me.PnlBody.ResumeLayout(False)
        Me.TableLayoutMain.ResumeLayout(False)
        Me.pnlChartContainer.ResumeLayout(False)
        Me.pnlChartContainer.PerformLayout()
        Me.pnlRecentContainer.ResumeLayout(False)
        Me.pnlRecentContainer.PerformLayout()
        CType(Me.dgvRecent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutStats.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class