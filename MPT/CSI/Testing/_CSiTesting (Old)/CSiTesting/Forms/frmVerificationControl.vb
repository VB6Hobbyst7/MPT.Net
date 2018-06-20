Option Explicit On
Public Class frmVerificationControl
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        formControls.initFrmVerificationControl()
    End Sub

    Private Sub btnRunSetup_Click(sender As Object, e As EventArgs) Handles btnRunSetup.Click
        formControls.btnOpen(frmRunSetup)
    End Sub

    Private Sub btnTestSuiteSetup_Click(sender As Object, e As EventArgs) Handles btnTestSuiteSetup.Click
        formControls.btnOpen(frmTestSuiteSetup)
    End Sub

    Private Sub btnSolverOptions_Click(sender As Object, e As EventArgs) Handles btnSolverOptions.Click
        formControls.btnOpen(frmSolverOptions)
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        formControls.btnOpen(frmVerificationReport)
    End Sub
End Class
