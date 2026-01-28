<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReportView
    Inherits System.Windows.Forms.UserControl

    Friend WithEvents dgvLaporan As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.dgvLaporan = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Button()

        Me.btnRefresh.Text = "REFRESH LAPORAN"
        Me.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnRefresh.Height = 40

        Me.dgvLaporan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvLaporan.Location = New System.Drawing.Point(0, 40)

        Me.Controls.Add(Me.dgvLaporan)
        Me.Controls.Add(Me.btnRefresh)
    End Sub
End Class