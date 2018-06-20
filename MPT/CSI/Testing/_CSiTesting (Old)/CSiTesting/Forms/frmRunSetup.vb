Option Explicit On

Public Class frmRunSetup
    'Friend Shared lblProgram As LinkLabel
    'Friend Shared cmbbxFileType As ComboBox

    Private Sub frmRunSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmRunSetup()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmRunSetup()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub

    Private Sub Label_Program_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles Label_Program.LinkClicked
        formControls.btnOpen(frmTestSuiteSetup)
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        formControls.btnBrowse(Me)
    End Sub

    Private Sub Option_LocationAbsolute_CheckedChanged(sender As Object, e As EventArgs) Handles Option_LocationAbsolute.CheckedChanged
        'Converts path from relative to absolute
        formControls.optnPathRelAbs(Me, True, 1, frmInitialize)
    End Sub

    Private Sub Option_LocationRelative_CheckedChanged(sender As Object, e As EventArgs) Handles Option_LocationRelative.CheckedChanged
        'Converts path from absolute to relative
        formControls.optnPathRelAbs(Me, False, 1, frmInitialize)
    End Sub

    Private Sub Option_Local_CheckedChanged(sender As Object, e As EventArgs) Handles Option_Local.CheckedChanged
        formControls.frmRunSetup_Option_Local_CheckedChanged()
    End Sub

    Private Sub Option_Distributed_CheckedChanged(sender As Object, e As EventArgs) Handles Option_Distributed.CheckedChanged
        formControls.frmRunSetup_Option_Distributed_CheckedChanged()
    End Sub

    Private Sub btnAdvanced_Click(sender As Object, e As EventArgs) Handles btnAdvanced.Click
        formControls.btnOpen(frmRunSetupAdvanced)
    End Sub

End Class