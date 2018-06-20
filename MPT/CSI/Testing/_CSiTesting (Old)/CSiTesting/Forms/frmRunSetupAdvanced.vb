Option Explicit On
Public Class frmRunSetupAdvanced

    Private Sub frmRunSetupAdvanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmRunSetupAdvanced()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmRunSetupAdvanced()
    End Sub


    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub

    Private Sub TextBox_TimeMultiplier_TextChanged(sender As Object, e As EventArgs) Handles TextBox_TimeMultiplier.TextChanged
        formControls.frmRunSetupAdvanced_TextBox_TimeMultiplier_TextChanged()
    End Sub

    Private Sub TextBox_MaxRuntime_TextChanged(sender As Object, e As EventArgs) Handles TextBox_MaxRuntime.TextChanged
        formControls.frmRunSetupAdvanced_TextBox_MaxRuntime_TextChanged()
    End Sub

    Private Sub TextBox_DecimalDigits_TextChanged(sender As Object, e As EventArgs) Handles TextBox_DecimalDigits.TextChanged
        formControls.frmRunSetupAdvanced_TextBox_DecimalDigits_TextChanged()
    End Sub

    Private Sub CheckBox_Email_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_Email.CheckedChanged
        formControls.frmRunSetupAdvanced_CheckBox_Email_CheckedChanged()
    End Sub

    Private Sub btnEmailList_Click(sender As Object, e As EventArgs) Handles btnEmailList.Click
        formControls.btnOpen(frmRunSetupEmailList)
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        formControls.btnBrowse(Me)
    End Sub

    Private Sub Option_LocationAbsolute_CheckedChanged(sender As Object, e As EventArgs) Handles Option_LocationAbsolute.CheckedChanged
        'Converts path from relative to absolute
        formControls.optnPathRelAbs(Me, True, , frmInitialize)
    End Sub

    Private Sub Option_LocationRelative_CheckedChanged(sender As Object, e As EventArgs) Handles Option_LocationRelative.CheckedChanged
        'Converts path from absolute to relative
        formControls.optnPathRelAbs(Me, False, , frmInitialize)
    End Sub

End Class