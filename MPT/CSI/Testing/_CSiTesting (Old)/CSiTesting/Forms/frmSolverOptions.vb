Option Explicit On
Public Class frmSolverOptions

    Private Sub frmSolverOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmSolverOptions()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmSolverOptions()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub
End Class