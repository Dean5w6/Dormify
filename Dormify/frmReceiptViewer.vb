Imports CrystalDecisions.CrystalReports.Engine

Public Class frmReceiptViewer

    Public Sub ShowReceipt(reportDataTable As DataTable)
        Dim reportDocument As New rptReceipt()
        reportDocument.SetDataSource(reportDataTable)
        CrystalReportViewer1.ReportSource = reportDocument
        CrystalReportViewer1.Refresh()
    End Sub
End Class