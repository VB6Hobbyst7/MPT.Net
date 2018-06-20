Option Explicit On
Public Class frmTestSuiteSetup
    Dim frmInitialize As Boolean
    'TO DO:
    '1. Create & link to the following forms from existing buttons
    '   1a. Select Models for Suite
    Private Sub frmTestSuiteSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmTestSuiteSetup()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmTestSuiteSetup()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub

    Private Sub btnBrowseSource_Click(sender As Object, e As EventArgs) Handles btnBrowseSource.Click
        formControls.btnBrowse(Me, 1)
    End Sub

    Private Sub Option_SourceAbsolute_CheckedChanged(sender As Object, e As EventArgs) Handles Option_SourceAbsolute.CheckedChanged
        'Converts path from relative to absolute
        formControls.optnPathRelAbs(Me, True, 1, frmInitialize)
    End Sub

    Private Sub Option_SourceRelative_CheckedChanged(sender As Object, e As EventArgs) Handles Option_SourceRelative.CheckedChanged
        'Converts path from absolute to relative
        formControls.optnPathRelAbs(Me, False, 1, frmInitialize)
    End Sub

    Private Sub btnBrowseDestination_Click(sender As Object, e As EventArgs) Handles btnBrowseDestination.Click
        formControls.btnBrowse(Me, 2)
    End Sub

    Private Sub Option_DestinationAbsolute_CheckedChanged(sender As Object, e As EventArgs) Handles Option_DestinationAbsolute.CheckedChanged
        'Converts path from relative to absolute
        formControls.optnPathRelAbs(Me, True, 2, frmInitialize)
    End Sub

    Private Sub Option_DestinationRelative_CheckedChanged(sender As Object, e As EventArgs) Handles Option_DestinationRelative.CheckedChanged
        'Converts path from absolute to relative
        formControls.optnPathRelAbs(Me, False, 2, frmInitialize)
    End Sub

    Private Sub btnSelectModels_Click(sender As Object, e As EventArgs) Handles btnSelectModels.Click
        formControls.frmTestSuiteSetup_SelectModels()
    End Sub

    Private Sub Option_CopyModels_CheckedChanged(sender As Object, e As EventArgs) Handles Option_CopyModels.CheckedChanged
        formControls.frmTestSuiteSetup_Option_CopyModels_CheckedChanged()
    End Sub

    Private Sub Option_CopyModelsNo_CheckedChanged(sender As Object, e As EventArgs) Handles Option_CopyModelsNo.CheckedChanged
        formControls.frmTestSuiteSetup_Option_CopyModelsNo_CheckedChanged()
    End Sub

    Private Sub btnDistTestOpt_Click(sender As Object, e As EventArgs) Handles btnDistTestOpt.Click
        formControls.btnOpen(frmTestSuiteSetupDistributed)
    End Sub

    Private Sub btnAdvanced_Click(sender As Object, e As EventArgs) Handles btnAdvanced.Click
        formControls.btnOpen(frmTestSuiteSetupDistributed)
    End Sub
End Class