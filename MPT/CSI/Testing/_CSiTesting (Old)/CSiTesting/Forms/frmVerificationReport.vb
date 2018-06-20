Option Explicit On
Public Class frmVerificationReport

    Private Sub frmVerificationReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmVerificationReport()
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmVerificationReport()
    End Sub
    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub

    Private Sub CheckBox_PrepareReport_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_PrepareReport.CheckedChanged
        formControls.frmVerificationReport_CheckBox_PrepareReport_CheckedChanged()
    End Sub

    Private Sub btnCompleteReport_Click(sender As Object, e As EventArgs) Handles btnCompleteReport.Click
        formControls.frmVerificationReport_btnCompleteReport()
    End Sub
End Class