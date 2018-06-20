Option Explicit On
Public Class frmRunSetupEmailList

    Private Sub frmRunSetupEmailList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        formControls.initFrmRunSetupEmailList()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        formControls.OKFrmRunSetupEmailList()
    End Sub

    Private Sub btnClose(sender As Object, e As EventArgs) Handles btnCancel.Click
        formControls.btnClose(Me)
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        formControls.frmRunSetupEmailList_btnModify_Click()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        formControls.frmRunSetupEmailList_btnAdd_Click()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        formControls.frmRunSetupEmailList_btnDelete_Click()
    End Sub

    Private Sub ListBox_EmailList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox_EmailList.SelectedIndexChanged
        formControls.frmRunSetupEmailList_ListBox_EmailList_SelectedIndexChanged()
    End Sub
End Class