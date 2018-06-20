Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cFileOutputSettings


Public Class frmFileFolderOperations
    Implements INotifyPropertyChanged
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const PROMPT_MC_XML_SELECT_DIRECTORY_SOURCE As String = "Select Source Directory Containing Model XML Control Files"
    Private Const PROMPT_MC_XML_SELECT_DIRECTORY_DESTINATION As String = "Select Destination Directory Containing Model Files"
    Private Const PROMPT_MC_XML_FILES_COPIED As String = "Models copied from source to destination."

    Private Const PROMPT_OUTPUTSETTINGS_SELECT_DIRECTORY_SOURCE As String = "Select Source Directory Containing OutputSetting.xml Files"
    Private Const PROMPT_OUTPUTSETTINGS_ATTACHMENTS_DIRECTORY_DESTINATION As String = "Select Destination Directory Containing 'attachments' Folders"
    Private Const PROMPT_OUTPUTSETTINGS_ATTACHMENTS_FILES_COPIED As String = " OutputSetting.xml Files copied from source to destination."

    Private Const PROMPT_FILES_RENAMED As String = "Files renamed"

    Private Const PROMPT_NEW_MODEL_SOURCE_CREATED As String = "New model source directory created."
    Private Const PROMPT_NEW_MODEL_SOURCE_CREATED_FAILED As String = "An error occurred. New model source directory was not created."
#End Region

#Region "Variables"
    Private myXmlEditorAction As eXMLEditorAction
    Private promptSelectSourceDir As String
    Private promptSelectDestDir As String
    Private promptFilesAction As String

    Private activateOutputSettings As eOutputSettingsActivation
    Private useModelSource As Boolean
#End Region

#Region "Properties"

    Private _txtFieldDirSource As String
    Public Property txtFieldDirSource As String
        Set(ByVal value As String)
            If Not _txtFieldDirSource = value Then
                _txtFieldDirSource = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("txtFieldDirSource"))
            End If
        End Set
        Get
            Return _txtFieldDirSource
        End Get
    End Property

    Private _txtFieldDirDestination As String
    Public Property txtFieldDirDestination As String
        Set(ByVal value As String)
            If Not _txtFieldDirDestination = value Then
                _txtFieldDirDestination = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("txtFieldDirDestination"))
            End If
        End Set
        Get
            Return _txtFieldDirDestination
        End Get
    End Property

    Private _outputSettingsVersion As String
    ''' <summary>
    ''' ETABS version for the outputSettings.xml file
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputSettingsVersion As String
        Set(ByVal value As String)
            If Not _outputSettingsVersion = value Then
                _outputSettingsVersion = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("outputSettingsVersion"))
            End If
        End Set
        Get
            Return _outputSettingsVersion
        End Get
    End Property

    Private _createAttachmentTag As Boolean
    ''' <summary>
    ''' If true, the MC XML that corresponds to the outputSettings.xml file will have an attachments tag created that marks the file as an attachment file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property createAttachmentTag As Boolean
        Set(ByVal value As Boolean)
            If Not _createAttachmentTag = value Then
                _createAttachmentTag = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("createAttachmentTag"))
            End If
        End Set
        Get
            Return _createAttachmentTag
        End Get
    End Property

    Private _createAttSupportingFileTag As Boolean
    ''' <summary>
    ''' If true, the MC XML that corresponds to the outputSettings.xml file will have an attachments tag created that marks the file as a supporting file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property createAttSupportingFileTag As Boolean
        Set(ByVal value As Boolean)
            If Not _createAttSupportingFileTag = value Then
                _createAttSupportingFileTag = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("createAttSupportingFileTag"))
            End If
        End Set
        Get
            Return _createAttSupportingFileTag
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()
        InitializeControls()
    End Sub

    Friend Sub New(ByVal xmlEditorAction As eXMLEditorAction)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(xmlEditorAction)
        InitializeControls()
    End Sub

    Private Sub InitializeData(Optional ByVal xmlEditorAction As eXMLEditorAction = eXMLEditorAction.None)
        If myXMLEditor Is Nothing Then myXMLEditor = New cXMLEditor

        txtFieldDirSource = myXMLEditor.sourceDirectory
        txtFieldDirDestination = myXMLEditor.destinationDirectory
        outputSettingsVersion = myCsiTester.versionOutputSettings

        If Not xmlEditorAction = eXMLEditorAction.None Then
            myXmlEditorAction = xmlEditorAction
            Select Case myXmlEditorAction
                Case eXMLEditorAction.UpdateModelFiles
                    promptSelectSourceDir = PROMPT_MC_XML_SELECT_DIRECTORY_SOURCE
                    promptSelectDestDir = PROMPT_MC_XML_SELECT_DIRECTORY_DESTINATION
                    promptFilesAction = PROMPT_MC_XML_FILES_COPIED
                Case eXMLEditorAction.UpdateOutputSettingsFiles
                    promptSelectSourceDir = PROMPT_OUTPUTSETTINGS_SELECT_DIRECTORY_SOURCE
                    promptSelectDestDir = PROMPT_OUTPUTSETTINGS_ATTACHMENTS_DIRECTORY_DESTINATION
                    promptFilesAction = PROMPT_OUTPUTSETTINGS_ATTACHMENTS_FILES_COPIED
                    createAttachmentTag = True
                    createAttSupportingFileTag = True
                Case eXMLEditorAction.RenameMCFilesAddSuffix
                    promptSelectSourceDir = PROMPT_MC_XML_SELECT_DIRECTORY_SOURCE
                    promptFilesAction = PROMPT_FILES_RENAMED
                Case eXMLEditorAction.RenameOutputSettingsFilesRemoveImportTag
                    promptSelectSourceDir = PROMPT_MC_XML_SELECT_DIRECTORY_SOURCE
                    promptFilesAction = PROMPT_FILES_RENAMED
                    activateOutputSettings = eOutputSettingsActivation.AsIs
                    useModelSource = True
            End Select
        End If
    End Sub

    Private Sub InitializeControls()
        btnUpdateModels.IsEnabled = False
        If IO.Directory.Exists(txtBxFileSource.Text) Then
            btnUpdateModels.IsEnabled = True
        Else
            btnUpdateModels.IsEnabled = False
        End If

        'Commonly hidden controls
        spDestinationCtrls.Visibility = Windows.Visibility.Collapsed
        spOutputSettingsVersion.Visibility = Windows.Visibility.Collapsed
        grpBxAttachmentTags.Visibility = Windows.Visibility.Collapsed

        'Selectively unhide controls, adjust labels
        Select Case myXmlEditorAction
            Case eXMLEditorAction.UpdateModelFiles
                Me.Title = Title & ": Update Model Files"
                grpBxOutputSettings.Visibility = Windows.Visibility.Collapsed
            Case eXMLEditorAction.UpdateOutputSettingsFiles
                spDestinationCtrls.Visibility = Windows.Visibility.Visible
                grpBxAttachmentTags.Visibility = Windows.Visibility.Visible
                Me.Title = Title & ": Update OutputSettings Files"
                chkBxAttachmentTag.IsChecked = True
                chkBxSupportingFileTag.IsChecked = True
                grpBxOutputSettings.Visibility = Windows.Visibility.Collapsed
            Case eXMLEditorAction.RenameMCFilesAddSuffix
                Me.Title = Title & ": Add Suffix to Model Control XML Files"
                grpBxOutputSettings.Visibility = Windows.Visibility.Collapsed
            Case eXMLEditorAction.RenameOutputSettingsFilesRemoveImportTag
                Me.Title = Title & ": Remove Version Import Tag from Non-Supporting OutputSettings XML Files"
                grpBxOutputSettings.Visibility = Windows.Visibility.Collapsed
            Case eXMLEditorAction.CreateNewModelSourceFromDestination
                Me.Title = Title & ": Create New Source Models from Destination Model Files"
                btnUpdateModels.Content = "Create"

                spDestinationCtrls.Visibility = Windows.Visibility.Visible
                spDirSource.Visibility = Windows.Visibility.Collapsed
                spOutputSettingsVersion.Visibility = Windows.Visibility.Collapsed
                grpBxAttachmentTags.Visibility = Windows.Visibility.Collapsed
                lblModelControlSource.Visibility = Windows.Visibility.Collapsed

                radBtnOSAsIs.IsChecked = True
                radBtnOSSource.IsChecked = True
        End Select
    End Sub

#End Region

#Region "Form Controls"
    '=== Buttons
    Private Sub btnDirSource_Click(sender As Object, e As RoutedEventArgs) Handles btnDirSource.Click
        Dim myStartupDir As String

        If Not String.IsNullOrEmpty(txtFieldDirSource) Then
            myStartupDir = txtFieldDirSource
        Else
            myStartupDir = myXMLEditor.xmlEditorPath
        End If

        txtFieldDirSource = BrowseForFolder(PROMPT_MC_XML_SELECT_DIRECTORY_SOURCE, myStartupDir)
    End Sub
    Private Sub btnDirDestination_Click(sender As Object, e As RoutedEventArgs) Handles btnDirDestination.Click
        Dim myStartupDir As String

        If Not String.IsNullOrEmpty(txtFieldDirDestination) Then
            myStartupDir = txtFieldDirDestination
        Else
            myStartupDir = myXMLEditor.xmlEditorPath
        End If

        If myXmlEditorAction = eXMLEditorAction.CreateNewModelSourceFromDestination Then
            txtFieldDirDestination = BrowseForFolder(PROMPT_MC_XML_SELECT_DIRECTORY_DESTINATION, myStartupDir, p_showNewFolderButton:=True)
        Else
            txtFieldDirDestination = BrowseForFolder(PROMPT_MC_XML_SELECT_DIRECTORY_DESTINATION, myStartupDir)
        End If
    End Sub

    Private Sub btnUpdateModels_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateModels.Click
        With myXMLEditor
            .sourceDirectory = txtFieldDirSource
            .destinationDirectory = txtFieldDirDestination
        End With

        Select Case myXmlEditorAction
            Case eXMLEditorAction.UpdateModelFiles
                myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.UpdateModelFiles)

            Case eXMLEditorAction.UpdateOutputSettingsFiles
                myCsiTester.versionOutputSettings = outputSettingsVersion

                Dim outputSettingsAttachment As New cOSFileCopy
                outputSettingsAttachment.CopyOutputSettingsFilesToAttachmentsDir(txtFieldDirSource, txtFieldDirDestination, createAttachmentTag, createAttSupportingFileTag)
            Case eXMLEditorAction.RenameMCFilesAddSuffix

                myXMLEditor.RenameMCXmlFilesAddSuffix(txtFieldDirSource)
            Case eXMLEditorAction.RenameOutputSettingsFilesRemoveImportTag
                cPathOutputSettings.RemoveImportTagFromFileName(txtFieldDirSource)

            Case eXMLEditorAction.CreateNewModelSourceFromDestination
                If myXMLEditor.CreateNewModelSourceFromDestination(txtFieldDirDestination, useModelSource, activateOutputSettings) Then
                    promptFilesAction = PROMPT_NEW_MODEL_SOURCE_CREATED
                Else
                    promptFilesAction = PROMPT_NEW_MODEL_SOURCE_CREATED_FAILED
                End If

        End Select

        RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), promptFilesAction))
        Me.Close()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '=== Text Box
    Private Sub txtBxFileSource_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxFileSource.TextChanged
        ValidatForm()

    End Sub
    Private Sub txtBxFileDestination_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxFileDestination.TextChanged
        ValidatForm()
    End Sub
    Private Sub txtBxOutputSettingsVersion_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxOutputSettingsVersion.TextChanged
        ValidatForm()
    End Sub
    Private Sub txtBxOutputSettingsVersion_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxOutputSettingsVersion.LostFocus
        If Not StringExistInName(txtBxOutputSettingsVersion.Text, "V") Then outputSettingsVersion = "V" & txtBxOutputSettingsVersion.Text
        ValidatForm()
    End Sub

    '=== Check Box
    Private Sub chkBxAttachmentTag_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxAttachmentTag.Checked
        chkBxSupportingFileTag.IsEnabled = True
    End Sub
    Private Sub chkBxAttachmentTag_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxAttachmentTag.Unchecked
        chkBxSupportingFileTag.IsEnabled = False
        chkBxSupportingFileTag.IsChecked = False
    End Sub


    '=== Radio Buttons
    Private Sub radBtnOSAsIs_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnOSAsIs.Checked
        activateOutputSettings = eOutputSettingsActivation.AsIs
    End Sub
    Private Sub radBtnOSActivate_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnOSActivate.Checked
        activateOutputSettings = eOutputSettingsActivation.Activate
    End Sub
    Private Sub radBtnOSDeactivate_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnOSDeactivate.Checked
        activateOutputSettings = eOutputSettingsActivation.Deactivate
    End Sub

    Private Sub radBtnOSSource_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnOSSource.Checked
        useModelSource = True
    End Sub
    Private Sub radBtnOSDestination_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnOSDestination.Checked
        useModelSource = False
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Checks data in form to determine if sufficient data has been defined to activate the action button.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatForm() As Boolean
        Dim dataComplete As Boolean = True

        Select Case myXmlEditorAction
            Case eXMLEditorAction.UpdateModelFiles
                If Not IO.Directory.Exists(txtBxFileSource.Text) Then dataComplete = False
            Case eXMLEditorAction.UpdateOutputSettingsFiles
                If Not IO.Directory.Exists(txtBxFileSource.Text) Then dataComplete = False
                If Not IO.Directory.Exists(txtBxFileDestination.Text) Then dataComplete = False
                If Not Len(txtBxOutputSettingsVersion.Text) > 2 Then dataComplete = False
        End Select

        If dataComplete Then
            btnUpdateModels.IsEnabled = True
            Return True
        Else
            btnUpdateModels.IsEnabled = False
            Return False
        End If
    End Function
#End Region


End Class
