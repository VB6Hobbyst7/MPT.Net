Option Explicit On
Public Class frmTestSuiteSetupDistributed
    Private Sub frmTestSuiteSetupDistributed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmTestSuiteSetupDistributed()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmTestSuiteSetupDistributed()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub
End Class