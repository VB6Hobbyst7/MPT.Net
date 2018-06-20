Option Explicit On
Public Class frmTestSuiteSetupAdvanced
    Private Sub frmTestSuiteSetupAdvanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmTestSuiteSetupAdvanced()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmTestSuiteSetupAdvanced()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub
End Class