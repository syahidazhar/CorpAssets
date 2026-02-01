<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnalyticsForm
    Inherits System.Windows.Forms.Form

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

    ' Menggunakan Custom Panel agar tidak flickering
    Friend WithEvents pnlLoc As BufferedPanel
    Friend WithEvents pnlDonut As BufferedPanel
    Friend WithEvents pnlTrend As BufferedPanel
    Friend WithEvents pnlCat As BufferedPanel

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 750)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "CorpAssets Analytics"
        Me.BackColor = System.Drawing.Color.FromArgb(244, 246, 248)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)

        ' === HEADER ===
        Me.PnlHeader = New Panel()
        Me.PnlHeader.Dock = DockStyle.Top
        Me.PnlHeader.Height = 70
        Me.PnlHeader.BackColor = System.Drawing.Color.White
        Me.PnlHeader.Padding = New Padding(20, 0, 20, 0)

        Me.lblTitle = New Label()
        Me.lblTitle.Text = "Analytics Overview"
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16, FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41)
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New Point(20, 20)

        Me.btnRefresh = New Button()
        Me.btnRefresh.Text = "Refresh Data"
        Me.btnRefresh.Size = New Size(120, 35)
        Me.btnRefresh.Location = New Point(1020, 18)
        Me.btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.btnRefresh.FlatStyle = FlatStyle.Flat
        Me.btnRefresh.BackColor = System.Drawing.Color.White
        Me.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41)
        Me.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200)
        Me.btnRefresh.Cursor = Cursors.Hand

        Me.PnlHeader.Controls.AddRange({lblTitle, btnRefresh})

        ' === MAIN BODY (GRID LAYOUT) ===
        Me.PnlMain = New Panel()
        Me.PnlMain.Dock = DockStyle.Fill
        Me.PnlMain.Padding = New Padding(20)

        Me.TableLayout = New TableLayoutPanel()
        Me.TableLayout.Dock = DockStyle.Fill
        Me.TableLayout.ColumnCount = 2
        Me.TableLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 55.0!))
        Me.TableLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 45.0!))
        Me.TableLayout.RowCount = 2
        Me.TableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0!))
        Me.TableLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0!))

        ' Helper Setup
        Dim SetupPanel As Action(Of BufferedPanel) = Sub(p)
                                                         p.Dock = DockStyle.Fill
                                                         p.BackColor = Color.White
                                                         p.Margin = New Padding(10)
                                                     End Sub

        Me.pnlLoc = New BufferedPanel() : SetupPanel(Me.pnlLoc)
        Me.pnlDonut = New BufferedPanel() : SetupPanel(Me.pnlDonut)
        Me.pnlTrend = New BufferedPanel() : SetupPanel(Me.pnlTrend)
        Me.pnlCat = New BufferedPanel() : SetupPanel(Me.pnlCat)

        Me.TableLayout.Controls.Add(Me.pnlLoc, 0, 0)
        Me.TableLayout.Controls.Add(Me.pnlDonut, 1, 0)
        Me.TableLayout.Controls.Add(Me.pnlTrend, 0, 1)
        Me.TableLayout.Controls.Add(Me.pnlCat, 1, 1)

        Me.PnlMain.Controls.Add(Me.TableLayout)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.PnlHeader)
        Me.ResumeLayout(False)
    End Sub
End Class