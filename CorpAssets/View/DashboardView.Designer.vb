<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardView
    Inherits System.Windows.Forms.UserControl

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblRusak As System.Windows.Forms.Label
    Private components As System.ComponentModel.IContainer

    Private Sub InitializeComponent()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblRusak = New System.Windows.Forms.Label()

        Me.lblTotal.Font = New System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold)
        Me.lblTotal.Location = New System.Drawing.Point(20, 50)
        Me.lblTotal.AutoSize = True

        Me.lblRusak.Font = New System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold)
        Me.lblRusak.ForeColor = System.Drawing.Color.Red
        Me.lblRusak.Location = New System.Drawing.Point(20, 100)
        Me.lblRusak.AutoSize = True

        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblRusak)
        Me.Size = New System.Drawing.Size(600, 400)
    End Sub
End Class