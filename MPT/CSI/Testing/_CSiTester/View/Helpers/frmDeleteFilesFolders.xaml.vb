Option Explicit On
Option Strict On

Imports System.ComponentModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Public Class frmDeleteFilesFolders
    Implements INotifyPropertyChanged
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const PROMPT_BROWSE_PARENT_FOLDER As String = "Browse to Folder"

    Private Const PROMPT_TXT_BX_FILE_NAME As String = "File Name (Optional)"
    Private Const PROMPT_TXT_BX_FILE_EXTENSION As String = "File Extension (Optional)"
    Private Const PROMPT_TXT_BX_FOLDER_NAME As String = "Folder Name (Optional)"

    Private Const PROMPT_DELETE_FILES_FINISHED As String = "All specified files have been deleted."
    Private Const PROMPT_DELETE_FILES_FOLDERS_FINISHED As String = "All specified folders and their contents have been deleted."
#End Region

#Region "Properties"
    Private _parentFolderPath As String
    Public Property parentFolderPath As String
        Set(ByVal value As String)
            If Not _parentFolderPath = value Then
                _parentFolderPath = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("parentFolderPath"))
            End If
        End Set
        Get
            Return _parentFolderPath
        End Get
    End Property

    Private _fileName As String
    Public Property fileName As String
        Set(ByVal value As String)
            If Not _fileName = value Then
                If String.IsNullOrWhiteSpace(value) Then
                    chkBxpartialNameMatch.IsEnabled = False
                Else
                    chkBxpartialNameMatch.IsEnabled = True
                End If

                _fileName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileName"))
            End If
        End Set
        Get
            Return _fileName
        End Get
    End Property

    Public Property fileExtension As String
    Public Property folderName As String

    Public Property includeSubFolders As Boolean = True
    Public Property deleteParentFolder As Boolean = False
    Public Property deleteFolders As Boolean = False
    Public Property deleteReadOnly As Boolean = True
    Public Property fullNameMatch As Boolean = True
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeFormControls()

    End Sub

    Sub InitializeFormControls()
        'Set Radio Buttons
        If deleteFolders Then
            radBtnDeleteFiles.IsChecked = False
            radBtnDeleteFolders.IsChecked = True
        Else
            radBtnDeleteFiles.IsChecked = True
            radBtnDeleteFolders.IsChecked = False
        End If

        'Set Checkboxes
        chkBxpartialNameMatch.IsEnabled = False

        'Set Delete Button
        btnDelete.IsEnabled = False
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    ''' <summary>
    ''' Sets the form variable and textbox to a specified folder path
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseFolder_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseFolder.Click
        parentFolderPath = BrowseForFolder(PROMPT_BROWSE_PARENT_FOLDER, pathStartup)
    End Sub
    ''' <summary>
    ''' Runs the specified delete operations
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnDelete.Click

        If Not deleteFolders Then
            Dim fileNames As New List(Of String)
            Dim fileExtensions As New List(Of String)
            Dim partialNameMatch As Boolean = False

            fileNames.Add(fileName)
            fileExtensions.Add(fileExtension)
            If Not fullNameMatch Then partialNameMatch = True

            'DeleteFiles(parentFolderPath, includeSubFolders, fileName, fileExtension, deleteReadOnly, partialNameMatch)
            DeleteFilesBulk(parentFolderPath, includeSubFolders, fileNames, fileExtensions, deleteReadOnly, partialNameMatch)
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_DELETE_FILES_FINISHED))
        Else
            DeleteAllFilesFolders(parentFolderPath, deleteParentFolder, folderName, deleteReadOnly)
            If deleteParentFolder Then parentFolderPath = ""
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_DELETE_FILES_FOLDERS_FINISHED))
        End If

    End Sub
    ''' <summary>
    ''' Closes the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        'MsgBox("parentFolderPath: " & parentFolderPath & Environment.NewLine & "fileName: " & fileName & Environment.NewLine & "fileExtension: " & fileExtension & Environment.NewLine & "folderName: " & folderName)
        Me.Close()
    End Sub

    '=== Radio Buttons
    ''' <summary>
    ''' Sets the form to delete only files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radBtnDeleteFiles_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnDeleteFiles.Checked
        deleteFolders = False

        'Enable file delete controls
        chkBxIncludeSubfolders.IsEnabled = True
        chkBxDeleteReadOnly.IsEnabled = True
        txtBxFileName.IsEnabled = True
        txtBxFileExtension.IsEnabled = True

        'Disable folder delete controls
        radBtnDeleteFolders.IsChecked = False
        chkBxDeleteParentFolder.IsEnabled = False
        txtBxFolderName.IsEnabled = False
    End Sub
    ''' <summary>
    ''' Sets the form to delete files and folders
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radBtnDeleteFolders_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnDeleteFolders.Checked
        deleteFolders = True

        'Enable file delete controls
        radBtnDeleteFiles.IsChecked = False
        chkBxIncludeSubfolders.IsEnabled = False
        chkBxDeleteReadOnly.IsEnabled = False
        txtBxFileName.IsEnabled = False
        txtBxFileExtension.IsEnabled = False

        'Disable folder delete controls
        chkBxDeleteParentFolder.IsEnabled = True
        txtBxFolderName.IsEnabled = True
    End Sub

    '=== Checkbox Buttons
    Private Sub chkBxpartialNameMatch_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxpartialNameMatch.Checked
        fullNameMatch = True
    End Sub
    Private Sub chkBxpartialNameMatch_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxpartialNameMatch.Unchecked
        fullNameMatch = False
    End Sub

    Private Sub chkBxDeleteParentFolder_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxDeleteParentFolder.Checked
        txtBxFolderName.IsEnabled = False
    End Sub
    Private Sub chkBxDeleteParentFolder_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxDeleteParentFolder.Unchecked
        txtBxFolderName.IsEnabled = True
    End Sub

    '=== Text Box Fields
    ''' <summary>
    ''' Sets the form folder path variable, and if it is valid, enables the "delete" button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBxParentFolder_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxParentFolder.TextChanged
        parentFolderPath = txtBxParentFolder.Text

        If IO.Directory.Exists(parentFolderPath) Then
            btnDelete.IsEnabled = True
        Else
            btnDelete.IsEnabled = False
        End If
    End Sub

#End Region

End Class
