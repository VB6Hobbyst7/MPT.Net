Option Strict On
Option Explicit On

Public Class frmDeleteFiles
#Region "Variables"
    Private deleteAll As Boolean
    Private numDeleted As Integer
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
    End Sub

    Private Sub SetDefaults()
        deleteAll = True
        radBtnDeleteAll.IsChecked = True
    End Sub
#End Region



#Region "Form Controls"
    Private Sub radBtnDeleteAll_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnDeleteAll.Checked
        deleteAll = True
    End Sub
    Private Sub radBtnDeleteSelected_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnDeleteSelected.Checked
        deleteAll = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click
        DeleteAction()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region

#Region "Methods"

    Private Sub DeleteAction()
        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait

        numDeleted = myCsiTester.DeleteAnalysisFilesExisting(deleteAll)

        'Check if all potential existing analysis files are likely cleared
        If deleteAll Then
            testerSettings.analysisFilesPresent = False
        Else
            If numDeleted = testerSettings.examplesRanSaved.Count Then testerSettings.analysisFilesPresent = False
        End If

        'If status has changed, save CSiTester
        If Not testerSettings.analysisFilesPresent Then testerSettings.Save()

        cursorWait.EndCursor()
    End Sub

#End Region


End Class
