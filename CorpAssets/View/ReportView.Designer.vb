<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportView
    Inherits System.Windows.Forms.UserControl

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

    '--- KOMPONEN ---
    Friend WithEvents PnlHeader As Panel
    Friend WithEvents PnlMain As Panel
    Friend WithEvents TableLayout As TableLayoutPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnRefresh As Button

    ' Gunakan class yang ada di file ReportView.vb
    Friend WithEvents pnlLoc As BufferedPanel
    Friend WithEvents pnlDonut As BufferedPanel
    Friend WithEvents pnlTrend As BufferedPanel
    Friend WithEvents pnlCat As BufferedPanel

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.TableLayout = New System.Windows.Forms.TableLayoutPanel()

        ' Inisialisasi
        Me.pnlLoc = New BufferedPanel()
        Me.pnlDonut = New BufferedPanel()
        Me.pnlTrend = New BufferedPanel()
        Me.pnlCat = New BufferedPanel()

        Me.PnlHeader.SuspendLayout()
        Me.PnlMain.SuspendLayout()
        Me.TableLayout.SuspendLayout()
        Me.SuspendLayout()

        ' PnlHeader
        Me.PnlHeader.BackColor = System.Drawing.Color.White
        Me.PnlHeader.Controls.Add(Me.btnRefresh)
        Me.PnlHeader.Controls.Add(Me.lblTitle)
        Me.PnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlHeader.Height = 70
        Me.PnlHeader.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)

        ' lblTitle
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(20, 20)
        Me.lblTitle.Text = "Analytics & Report"

        ' btnRefresh
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.White
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(850, 18)
        Me.btnRefresh.Size = New System.Drawing.Size(120, 35)
        Me.btnRefresh.Text = "Refresh Data"
        Me.btnRefresh.UseVisualStyleBackColor = False

        ' PnlMain
        Me.PnlMain.Controls.Add(Me.TableLayout)
        Me.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.PnlMain.Location = New System.Drawing.Point(0, 70)
        Me.PnlMain.Size = New System.Drawing.Size(1000, 630)

        ' TableLayout
        Me.TableLayout.ColumnCount = 2
        Me.TableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayout.Controls.Add(Me.pnlLoc, 0, 0)
        Me.TableLayout.Controls.Add(Me.pnlDonut, 1, 0)
        Me.TableLayout.Controls.Add(Me.pnlTrend, 0, 1)
        Me.TableLayout.Controls.Add(Me.pnlCat, 1, 1)
        Me.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayout.RowCount = 2
        Me.TableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))

        ' Setup Helper Panel
        Dim SetupPanel As Action(Of BufferedPanel) = Sub(p)
                                                         p.Dock = System.Windows.Forms.DockStyle.Fill
                                                         p.BackColor = System.Drawing.Color.White
                                                         p.Margin = New System.Windows.Forms.Padding(10)
                                                     End Sub
        SetupPanel(Me.pnlLoc)
        SetupPanel(Me.pnlDonut)
        SetupPanel(Me.pnlTrend)
        SetupPanel(Me.pnlCat)

        ' ReportView
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.PnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Size = New System.Drawing.Size(1000, 700)

        Me.PnlHeader.ResumeLayout(False)
        Me.PnlHeader.PerformLayout()
        Me.PnlMain.ResumeLayout(False)
        Me.TableLayout.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class