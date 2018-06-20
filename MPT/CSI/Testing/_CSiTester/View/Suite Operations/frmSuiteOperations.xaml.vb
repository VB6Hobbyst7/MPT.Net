Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Public Class frmSuiteOperations
#Region "Variables"

#End Region

#Region "Properties"

#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetButtons()
    End Sub

    Private Sub SetButtons()
        btnConvertXMLs.IsEnabled = False

        radBtn_RenameModelName.IsEnabled = False
        radBtn_RenameModelName.IsChecked = True
        radBtn_RenameModelID.IsEnabled = False

        chkBox_RenameFolder.IsEnabled = False
        chkBox_RenameModel.IsEnabled = False
        chkBox_RenameXML.IsEnabled = False
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    'NOTE: Uses myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.UpdateModelFiles)
    'NOTE!: This does not sync within the OutputSettings.xml files. Add this!
    ''' <summary>
    ''' Takes all selected examples and renames any selection of the following files to be synced wither with the model ID or secondary ID, including any references within the files:
    ''' Model Control XML
    ''' Model File
    ''' Folder Name
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConvertXMLs_Click(sender As Object, e As RoutedEventArgs) Handles btnConvertXMLs.Click
        'Set whether to rename by exampe name or model id
        If radBtn_RenameModelName.IsChecked = True Then
            myXMLEditor.ToExampleName = True
        Else
            myXMLEditor.ToExampleName = False
        End If

        'Set whether to convert the XML filenames and data
        If chkBox_RenameXML.IsChecked = True Then
            myXMLEditor.ConvertXMLFile = True
        Else
            myXMLEditor.ConvertXMLFile = False
        End If

        'Set whether to convert the model filenames and any associated table database XML setters
        If chkBox_RenameModel.IsChecked = True Then
            myXMLEditor.ConvertModelFile = True
        Else
            myXMLEditor.ConvertModelFile = False
        End If

        'Set whether to convert parent folder names, if model is in database folder structure
        If chkBox_RenameFolder.IsChecked = True Then
            myXMLEditor.ConvertFolder = True
        Else
            myXMLEditor.ConvertFolder = False
        End If

        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.Convert)

        myXMLEditor.XMLChanged = True
    End Sub

    ''' <summary>
    ''' Takes all examples in a database-style directory and flattens them at the level of the example-name directory.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnFlattenDirectories_Click(sender As Object, e As RoutedEventArgs) Handles btnFlattenDirectories.Click
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.DirectoriesFlatten)
    End Sub

    ''' <summary>
    ''' Takes all examples in a flattened-style directory and gathers them into a database-style directory within an example-name directory.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateDBirectories_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateDBirectories.Click
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.DirectoriesDBGather)
    End Sub

    ''' <summary>
    ''' Generates text files for all examples in the specified folder by opening, saving, and closing each example.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateTxtFiles_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateTxtFiles.Click
        Dim windowGenerateTextFiles As New frmGenerateTextFiles

        windowGenerateTextFiles.ShowDialog()
    End Sub

    ''' <summary>
    ''' Runs regTest validation of all model control XMLs in the specified folder. Displays the pass/fail results of the validation.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValidateSchema_Click(sender As Object, e As RoutedEventArgs) Handles btnValidateSchema.Click
        Dim windowXMLModelValidate As New frmMCValidate

        windowXMLModelValidate.ShowDialog()
    End Sub

    'NOTE: Uses myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.UpdateModelFiles)
    ''' <summary>
    ''' Copies models from a source and places them in the correct location in the destination. 
    ''' Used for populating an existing database-style destination with models from any source, for either an empty destination, or for updating the models from a primary source.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdateModels_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateModels.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.UpdateModelFiles)

        windowFileFolderOperations.ShowDialog()
    End Sub

    ''' <summary>
    ''' Copies all of the outputSettings.xml files from a source folder to the stored attachments folder. 
    ''' Also creates the proper tags within the corresponding model control XML, and adds the appropriate version tag, if applicable.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdateOutputSettingsFiles_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateOutputSettingsFiles.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.UpdateOutputSettingsFiles)

        windowFileFolderOperations.ShowDialog()
    End Sub

    ''' <summary>
    ''' Renames all model control XML files to include the suffix, if missing.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddMCXMLSuffix_Click(sender As Object, e As RoutedEventArgs) Handles btnAddMCXMLSuffix.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.RenameMCFilesAddSuffix)

        windowFileFolderOperations.ShowDialog()
    End Sub

    ''' <summary>
    ''' Renames the outputsettings files to remove the import tag in the outputsettings.xml file if the file is not a supporting file.
    ''' The tag is the "_V{13}" portion of the name.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRemoveOSImportTag_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveOSImportTag.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.RenameOutputSettingsFilesRemoveImportTag)

        windowFileFolderOperations.ShowDialog()
    End Sub

    'NOTE: Uses myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.UpdateModelFiles)
    ''' <summary>
    ''' Adds keywords to the model control files for all selected examples.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnXMLNodeCreateKeywords_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLNodeCreateKeywords.Click
        Dim windowXMLNodeCreateKeywords As New frmXMLNodeCreateKeywords(eXMLEditorAction.ActionToExistingFiles)
        Dim keywordsList As New List(Of String)

        If myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.KeywordsAddRemove, , , , , , keywordsList) Then
            windowXMLNodeCreateKeywords.keywordsList(0).keywordsExisting = New ObservableCollection(Of String)(keywordsList)
        End If

        windowXMLNodeCreateKeywords.InitializeDGClassesExisting()

        windowXMLNodeCreateKeywords.ShowDialog()
    End Sub

    'NOTE: Uses myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.UpdateModelFiles)
    ''' <summary>
    ''' For any object type, adds it to all selected examples. Types available are:
    ''' Incident
    ''' Ticket
    ''' Link
    ''' Attachment
    ''' Image
    ''' Update
    ''' Excel Result
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnXMLNodeCreateObjects_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLNodeCreateObjects.Click
        Dim windowXMLNodeCreateObjects As New frmXMLObjectCreate

        If myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.ObjectAdd) Then

        End If

        windowXMLNodeCreateObjects.ShowDialog()
    End Sub


    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '=== Checkboxes: Naming Parameters for Files & Folders
    Private Sub chkBox_Rename_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_Rename.Checked
        radBtn_RenameModelName.IsEnabled = True
        radBtn_RenameModelID.IsEnabled = True

        chkBox_RenameFolder.IsEnabled = True
        chkBox_RenameModel.IsEnabled = True
        chkBox_RenameXML.IsEnabled = True
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then btnConvertXMLs.IsEnabled = True
    End Sub

    Private Sub chkBox_RenameXML_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameXML.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub
    Private Sub chkBox_RenameModel_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameModel.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub
    Private Sub chkBox_RenameFolder_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameFolder.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub

    Private Sub chkBox_Rename_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_Rename.Unchecked
        btnConvertXMLs.IsEnabled = False

        radBtn_RenameModelName.IsEnabled = False
        radBtn_RenameModelID.IsEnabled = False

        chkBox_RenameFolder.IsEnabled = False
        chkBox_RenameModel.IsEnabled = False
        chkBox_RenameXML.IsEnabled = False
    End Sub

    Private Sub chkBox_RenameXML_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameXML.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub
    Private Sub chkBox_RenameModel_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameModel.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub
    Private Sub chkBox_RenameFolder_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameFolder.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub
#End Region

#Region "Methods"

#End Region

End Class
