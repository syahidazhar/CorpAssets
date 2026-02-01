Imports System.Windows.Forms
Imports System.Drawing

' INI ADALAH DEFINISI TUNGGAL BUFFEREDPANEL
' Bisa dipakai oleh ReportView, DashboardForm, dll.
Public Class BufferedPanel
    Inherits Panel
    Public Sub New()
        Me.DoubleBuffered = True
        Me.SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
        Me.UpdateStyles()
    End Sub
End Class