Option Explicit On
Module VerificationControl

    Friend Const VerificationControl = "New Verification Control Layout"

    Sub AnalysisSwitch()

        frmSolverOptions.ShowDialog()
        frmSolverOptions.Dispose()

    End Sub

End Module
