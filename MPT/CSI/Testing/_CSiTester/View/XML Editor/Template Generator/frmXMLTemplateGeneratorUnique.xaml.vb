Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Data
Imports System.IO

Imports MPT.Database.QueryLibrary
Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.DataGridLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting

Imports CSiTester.cMCModel


Public Class frmXMLTemplateGeneratorUnique
    Implements INotifyPropertyChanged
    Implements INotifyCollectionChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Implements INotifyCollectionChanged.CollectionChanged
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Event Handlers"
    ''' <summary>
    ''' Signals that a propery change has occurred for a property that uses another object's properties as a backing field.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RaiseTablePathChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModelSelected.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModelSelected.dataSource) Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pathTable"))
        End If
    End Sub

    ''' <summary>
    ''' Keeps the collection referenced by the dataGrid in sync with changes to the _filePathSelected object.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RaiseFilePathChanged(sender As Object, e As PropertyChangedEventArgs) Handles _filePathSelected.PropertyChanged
        If Not e.PropertyName = NameOfProp(Function() _filePathSelected.fileNameUse) Then
            UpdatePathCollectionFromPathSelected()
        End If
    End Sub

    Protected Sub RaiseExampleChangedSinceSave(sender As Object, e As PropertyChangedEventArgs) Handles _mcModelSelected.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModelSelected.changedSinceSave) Then
            btnValidate.IsEnabled = ExampleCanBeValidated(mcModelSelected)
            btnValidateAll.IsEnabled = AllExamplesCanBeValidated()
            SetPopulateExcelResultsButtons()
        End If
    End Sub
#End Region

#Region "Constants: Private"
    Private Const TITLE_DATABASE_FILENAME_CONFIRMATION As String = "Database File Name Confirmation"
    Private Const PROMPT_DATABASE_FILENAME_VALID As String = "Current database filename has been confirmed to be valid."
    Private Const PROMPT_DATABASE_FILENAME_INVALID As String = "Current database filename is invalid or missing at the model file location!" & vbNewLine & vbNewLine &
                                                                "Please choose another file for confirmation."
    Private Const TITLE_FORM_DATA_INVALID As String = "Model Data Confirmation"
    Private Const PROMPT_FORM_DATA_INVALID As String = "Some of the form data entered is incorrect, such as possibly the table name extension." & vbNewLine & vbNewLine &
                            "Incorrect data will not be saved to the model control file." & vbNewLine & vbNewLine &
                            "Do you wish to proceed?"
#End Region

#Region "Fields"
    ''' <summary>
    ''' Copy of the currently selected model control file that is checked to see if the model has been changed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcModelSelectedUnedited As New cMCModel

    ''' <summary>
    ''' Collection of the models that have been saved. 
    ''' Used in order to compare models in the session for potential changes.
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcModelsSaved As New cMCModels

    ''' <summary>
    ''' Path to the current data source to be accessed.
    ''' This is used regardless of whether or not the path is to be recorded in the model control file.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataSourceCurrent As String
    ''' <summary>
    ''' True: The current data source path has been validated.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataSourceCurrentIsValid As Boolean

    ''' <summary>
    ''' The model id of the selected example, as soon as the selection occures. 
    ''' This may differ from the model ID if the model ID is then changed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _modelIDCurrent As Decimal

    Private _excelResultsExist As Boolean

    Private _styleBtnRequiredNeeded As New Style
    Private _styleBtnRequiredFulfilled As New Style

    Private _changingSelection As Boolean

    Private _formLoading As Boolean
#End Region

#Region "Properties"
    ''' <summary>
    ''' Convenient get/set layer for the table file associated with the currently selected model file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Property _pathTable As String
        Get
            Return _filePathSelected.dataSource.path
        End Get
        Set(value As String)
            If Not _filePathSelected.dataSource.path = value Then
                _filePathSelected.SetDataSourceName(GetPathFileName(value, p_noExtension:=False))
            End If
        End Set
    End Property

    ''' <summary>
    ''' File name of the table file to associate with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileNameTable As String
        Get
            Dim fileName As String = _filePathSelected.dataSource.fileNameWithExtension
            If (String.IsNullOrWhiteSpace(fileName) OrElse
                txtBxDFileNameShouldBeEmpty()) Then
                Return ""
            Else
                Return fileName
            End If
        End Get
        Set(value As String)
            If Not fileNameTable = value Then
                _pathTable = value
                If Not _changingSelection Then CheckExampleChanged()
            End If
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileNameTable"))
        End Set
    End Property

    Private WithEvents _mcModelSelected As New cMCModel
    ''' <summary>
    ''' Class representing the currently selected model control XML file to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property mcModelSelected As cMCModel
        Set(ByVal value As cMCModel)
            _mcModelSelected = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("mcModelSelected"))
        End Set
        Get
            Return _mcModelSelected
        End Get
    End Property

    Private WithEvents _filePathSelected As New cPathExample
    ''' <summary>
    ''' Class representing the currently selected path object, which coincides with the currently selected cMCModel object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePathSelected As cPathExample
        Set(ByVal value As cPathExample)
            _filePathSelected = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("filePath"))
        End Set
        Get
            Return _filePathSelected
        End Get
    End Property

    ''' <summary>
    ''' Class that contains paths to all files and directories within a specified directory, as well as a list of paths filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePaths As New cPathsExamples

    ''' <summary>
    ''' Collection of paths to model files for the examples being created/edited.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsSelected As New ObservableCollection(Of cPathSelected)

    Private _modelSelectedDir As String
    ''' <summary>
    ''' The optionally displayed "models\" path prefix to the model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property modelSelectedDir As String
        Get
            Return _modelSelectedDir
        End Get
        Set(value As String)
            If Not _modelSelectedDir = value Then
                _modelSelectedDir = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("modelSelectedDir"))
            End If
        End Set
    End Property

    Private _examplesMCModels As New cMCModels
    ''' <summary>
    ''' List of classes for the model control XML files to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesMCModels As cMCModels
        Set(ByVal value As cMCModels)
            If Not _examplesMCModels.Equals(value) Then
                _examplesMCModels = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("examplesMCModels"))
                RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace))
            End If
        End Set
        Get
            Return _examplesMCModels
        End Get
    End Property

    Private _applyToGroup As Boolean
    ''' <summary>
    ''' Item defined will be applied to all models in the same example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property applyToGroup As Boolean
        Set(ByVal value As Boolean)
            If Not _applyToGroup = value Then
                _applyToGroup = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("applyToGroup"))
            End If
        End Set
        Get
            Return _applyToGroup
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Generates a model control generator form based on a collection of model control objects.
    ''' </summary>
    ''' <param name="p_models">Collection of model control objects to use.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_models As cMCGenerator)
        _formLoading = True
        ' This call is required by the designer.
        InitializeComponent()
        _formLoading = False

        ' Add any initialization after the InitializeComponent() call.
        Initialize(p_models.mcModels, p_models.filePaths)
    End Sub

    ''' <summary>
    ''' Generates a model control generator form based on a collection of model control and file path objects.
    ''' </summary>
    ''' <param name="p_models">Collection of model control objects to use.</param>
    ''' <param name="p_filePaths">File path objects corresponding to the collection of model control objects provided.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_models As cMCModels,
                   ByVal p_filePaths As cPathsExamples)
        _formLoading = True
        ' This call is required by the designer.
        InitializeComponent()
        _formLoading = False

        ' Add any initialization after the InitializeComponent() call.
        Initialize(p_models, p_filePaths)
    End Sub

    Private Sub Initialize(ByVal p_models As cMCModels,
                           ByVal p_filePaths As cPathsExamples)
        If p_filePaths IsNot Nothing Then filePaths = p_filePaths
        If p_models IsNot Nothing Then
            examplesMCModels = p_models
            examplesMCModels.AutoSetExamplePropertiesSync()
            UpdateSavedMCModels()
        End If

        InitializeData()
        InitializeStyles()
        InitializeControls()

        CheckRequiredDataFilled()
        SelectRowByIndex(dgFilesList, 0, 2, True)
    End Sub

    Private Sub InitializeData()

        'Assign filepaths and set all selections to false.
        SetFilePathsStatus()

        dgFilesList.ItemsSource = pathsSelected

        'Set the form to display the first model in the list
        filePathSelected = filePaths.pathsSelected(0)
        mcModelSelected = examplesMCModels(0)
        applyToGroup = IsCommonPropertiesAppliedToGroup()

        CheckExcelResultsExist()
    End Sub

    ''' <summary>
    ''' Creates styles for the form that are to be assigned programmatically.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeStyles()
        Dim mySetterForeground As New Setter()
        Dim mySetterFontWeight As New Setter()

        'Required button that needs to be satisfied
        _styleBtnRequiredNeeded = New Style()

        mySetterForeground = New Setter
        With mySetterForeground
            .Property = Button.ForegroundProperty
            .Value = Brushes.Tomato
        End With

        mySetterFontWeight = New Setter()
        With mySetterFontWeight
            .Property = Button.FontWeightProperty
            .Value = FontWeights.Bold
        End With

        With _styleBtnRequiredNeeded
            .Setters.Add(mySetterForeground)
            .Setters.Add(mySetterFontWeight)
            .TargetType = GetType(Button)
        End With

        'Required button that has been satisfied
        _styleBtnRequiredFulfilled = New Style()

        mySetterForeground = New Setter
        With mySetterForeground
            .Property = Button.ForegroundProperty
            .Value = Brushes.Black
        End With

        mySetterFontWeight = New Setter()
        With mySetterFontWeight
            .Property = Button.FontWeightProperty
            .Value = FontWeights.Normal
        End With

        With _styleBtnRequiredFulfilled
            .Setters.Add(mySetterForeground)
            .Setters.Add(mySetterFontWeight)
            .TargetType = GetType(Button)
        End With

    End Sub

    Private Sub InitializeControls()
        btnSave.IsEnabled = False
        btnSaveAll.IsEnabled = False
        btnResults.Style = _styleBtnRequiredNeeded

        SetRequiredButtonFormat()

        btnValidate.IsEnabled = ExampleCanBeValidated(mcModelSelected)
        btnValidateAll.IsEnabled = AllExamplesCanBeValidated()
        SetPopulateExcelResultsButtons()

        SetExamplesGroupCheckBox()
    End Sub

#End Region

#Region "Form Controls"
    ''' <summary>
    ''' Saves the currently selected example.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        UpdateCurrentlySelectedMCModel()
        If (ConfirmSave() AndAlso
            mcModelSelected.SaveExampleToFiles()) Then

            btnValidate.IsEnabled = True
            SetPopulateExcelResultsButtons()

            UpdateCurrentlySelectedMCModel()
            UpdateSavedMCModel()
            CheckRequiredDataFilled()
        End If
    End Sub

    ''' <summary>
    ''' Saves all completed or changed examples in the suite.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveAll_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveAll.Click
        UpdateCurrentlySelectedMCModel()
        If (ConfirmSaveAll() AndAlso
            examplesMCModels.SaveExamplesToFiles()) Then

            btnValidate.IsEnabled = True
            btnValidateAll.IsEnabled = True

            SetPopulateExcelResultsButtons()

            UpdateSavedMCModels()
            mcModelSelected = examplesMCModels(mcModelSelected.ID.idCompositeDecimal)
            CheckRequiredDataFilled(p_updateAll:=True)
        End If
    End Sub

    Private Sub btnPopulateExcelResult_Click(sender As Object, e As RoutedEventArgs) Handles btnPopulateExcelResult.Click
        mcModelSelected.AddResultsFromExcelFileToMCFile()

        mcModelSelected = New cMCModel(mcModelSelected.mcFile.pathDestination.path)
        UpdateSavedMCModel()
        CheckRequiredDataFilled()
        SetButtonContentBySelectedExample()
    End Sub


    Private Sub btnPopulateExcelResults_Click(sender As Object, e As RoutedEventArgs) Handles btnPopulateExcelResults.Click
        UpdateModelsExcelResults()

        mcModelSelected = New cMCModel(mcModelSelected.mcFile.pathDestination.path)
        UpdateSavedMCModels()
        CheckRequiredDataFilled()
        SetButtonContentBySelectedExample()
    End Sub

    ''' <summary>
    ''' Validates the example with RegTest and displays the results.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValidate_Click(sender As Object, e As RoutedEventArgs) Handles btnValidate.Click
        Dim frmValidation As New frmMCValidate(cMCValidator.eSchemaValidate.RunSchemaValidationCustomAuto, mcModelSelected.mcFile.pathDestination.directory)
        frmValidation.Show()
    End Sub

    ''' <summary>
    ''' Validates all examples with RegTest and displays the results.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnValidateAll_Click(sender As Object, e As RoutedEventArgs) Handles btnValidateAll.Click
        Dim paths As New List(Of String)
        For Each model As cMCModel In examplesMCModels
            paths.Add(model.mcFile.pathDestination.directory)
        Next

        Dim frmValidation As New frmMCValidate(cMCValidator.eSchemaValidate.RunSchemaValidationCustomAuto, paths)
        frmValidation.Show()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnEditGeneralDetails_Click(sender As Object, e As RoutedEventArgs) Handles btnEditGeneralDetails.Click
        If mcModelSelected Is Nothing Then Exit Sub

        windowXMLTemplateGenerator = New frmXMLTemplateGenerator(mcModelSelected, examplesMCModels.totalReservedExampleIDs, IsExampleIDAppliedToGroup())
        windowXMLTemplateGenerator.ShowDialog()

        ' Update corresponding objects
        UpdateEditedMCModels(windowXMLTemplateGenerator.myMCModelSave,
                             windowXMLTemplateGenerator.applyToGroup)

        CheckExampleChanged()
    End Sub

    '=== Add Objects
    Private Sub btnCreateIncident_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateIncident.Click
        DisplayObjectForm(eXMLObjectType.Incident)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateTicket_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateTicket.Click
        DisplayObjectForm(eXMLObjectType.Ticket)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateLink_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateLink.Click
        DisplayObjectForm(eXMLObjectType.Link)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateAttachment_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateAttachment.Click
        DisplayObjectForm(eXMLObjectType.Attachment)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateSupportingFile_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateSupportingFile.Click
        DisplayObjectForm(eXMLObjectType.SupportingFile)
        CheckExampleChanged()
    End Sub
    Private Sub btnDocumentationFile_Click(sender As Object, e As RoutedEventArgs) Handles btnDocumentationFile.Click
        DisplayObjectForm(eXMLObjectType.Documentation)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateImage_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateImage.Click
        DisplayObjectForm(eXMLObjectType.Image)
        CheckExampleChanged()
    End Sub
    Private Sub btnCreateUpdate_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateUpdate.Click
        DisplayObjectForm(eXMLObjectType.Update)
        CheckExampleChanged()
    End Sub

    '=== Add Keywords
    Private Sub btnAddKeywords_Click(sender As Object, e As RoutedEventArgs) Handles btnAddKeywords.Click
        Dim windowXMLNodeCreateKeywords As New frmMCKeywords(mcModelSelected.keywords)
        windowXMLNodeCreateKeywords.ShowDialog()

        mcModelSelected.keywords = windowXMLNodeCreateKeywords.keywordsSave
        CheckExampleChanged()
    End Sub

    '=== Add Benchmarks & Results
    ''' <summary>
    ''' Sets the program version and name for the results associated with the selected example.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBenchmark_Click(sender As Object, e As RoutedEventArgs) Handles btnBenchmark.Click
        Try
            If (Not OnlyExcelResultsSet() AndAlso
                Not InitializeTableSourceAndProgramControl()) Then Exit Sub

            Dim windowXMLObjectBenchmarkVersion As New frmXMLObjectBenchmarkVersion(mcModelSelected.program, IsExcelMultiModel(), IsBenchmarksAppliedToGroup())
            windowXMLObjectBenchmarkVersion.ShowDialog()

            mcModelSelected.program = windowXMLObjectBenchmarkVersion.myBenchmarkSave
            UpdateBenchmarkReferences(windowXMLObjectBenchmarkVersion.myBenchmarkSave, windowXMLObjectBenchmarkVersion.applyToGroup)

            CheckExampleChanged()
            UpdateFormByRequiredData()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the results associated with the selected example.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnResults_Click(sender As Object, e As RoutedEventArgs) Handles btnResults.Click
        Try
            If Not InitializeTableSourceAndProgramControl() Then Exit Sub

            GetExampleResults()
            UpdateCurrentlySelectedMCModel()
            CheckExampleChanged()
            UpdateFormByRequiredData()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    Private Sub btnResultsExcel_Click(sender As Object, e As RoutedEventArgs) Handles btnResultsExcel.Click
        If mcModelSelected Is Nothing Then Exit Sub

        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.ExcelResult, mcModelSelected, IsExcelResultAppliedToGroup())
        windowXMLNodeCreateObject.ShowDialog()

        mcModelSelected = windowXMLNodeCreateObject.myMCModelSave
        UpdateExcelResults(windowXMLNodeCreateObject.myMCModelSave.resultsExcel, windowXMLNodeCreateObject.applyToGroup)
        CheckExcelResultsExist()

        UpdateCurrentlySelectedMCModel()
        CheckExampleChanged()
        UpdateFormByRequiredData()
    End Sub

    Private Sub btnConfirmValidTableFile_Click(sender As Object, e As RoutedEventArgs) Handles btnConfirmValidTableFile.Click
        CheckValidDataSource()
    End Sub

    Private Sub btnBrowseValidTableFile_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseValidTableFile.Click
        Dim tempPath As String = ""
        If filePathSelected.dataSource.BrowseForTableFile(tempPath) Then
            _dataSourceCurrent = tempPath
            _dataSourceCurrentIsValid = True
            _filePathSelected.dataSource.SetProperties(tempPath)
            fileNameTable = IO.Path.GetFileName(_dataSourceCurrent)
        End If
    End Sub

    '=== Text Boxes
    Private Sub txtBxTitle_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxTitle.LostFocus
        'CheckExampleChanged()
        'CheckRequiredDataFilled()
    End Sub
    Private Sub txtBxTitle_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxTitle.TextChanged
        If Not _changingSelection Then
            UpdateExampleGroupModels(applyToGroup)
            CheckExampleChanged()
            CheckRequiredDataFilled()
        End If
    End Sub

    Private Sub txtBxDescription_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxDescription.LostFocus
        'CheckExampleChanged()
        'CheckRequiredDataFilled()
    End Sub
    Private Sub txtBxDescription_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxDescription.TextChanged
        If Not _changingSelection Then
            UpdateExampleGroupModels(applyToGroup)
            CheckExampleChanged()
            CheckRequiredDataFilled()
        End If
    End Sub

    Private Sub txtFieldComment_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtFieldComment.LostFocus
        'CheckExampleChanged()
        'CheckRequiredDataFilled()
    End Sub
    Private Sub txtFieldComment_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtFieldComment.TextChanged
        If Not _changingSelection Then
            CheckExampleChanged()
            CheckRequiredDataFilled()
        End If
    End Sub

    Private Sub txtBxRunTime_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxRunTime.LostFocus
        'CheckExampleChanged()
        'CheckRequiredDataFilled()
    End Sub
    Private Sub txtBxRunTime_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxRunTime.TextChanged
        If Not _changingSelection Then
            CheckExampleChanged()
            CheckRequiredDataFilled()
        End If
    End Sub

    Private Sub txtBxDBFileName_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxDBFileName.LostFocus
        ' Example status not checked, as the data table name is saved as a final step and therefore cannot be checked until then.
    End Sub
    Private Sub txtBxDBFileName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxDBFileName.TextChanged
        If Not _changingSelection Then
            CheckExampleChanged()
            CheckRequiredDataFilled()
        End If
    End Sub


    '=== Datagrid
    ''' <summary>
    ''' Changes the currently visible MC XML properties.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgFilesList_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles dgFilesList.SelectionChanged
        If e.AddedItems.Count > 0 AndAlso Not _changingSelection Then
            _changingSelection = True

            UpdateCurrentlySelectedMCModel()
            UpdateExampleGroupModels(applyToGroup)

            _dataSourceCurrentIsValid = False
            SetCurrentlySelectedModelPath()
            SetCurrentlySelectedMCModel()

            SetButtonContentBySelectedExample()
            SetRequiredButtonFormat()
            SetExamplesGroupCheckBox()

            btnValidate.IsEnabled = ExampleCanBeValidated(mcModelSelected)
            SetPopulateExcelResultsButtons()

            ' Added to ensure the process always ends with a selected row
            Dim addedExample As cPathSelected = DirectCast(e.AddedItems(0), cPathSelected)
            Dim selectedID As Decimal = addedExample.ID
            Dim index As Integer = 0
            For Each pathSelected As cPathSelected In pathsSelected
                If pathSelected.ID = selectedID Then Exit For
                index += 1
            Next
            SelectRowByIndex(dgFilesList, index, 2, True)

            _changingSelection = False
        End If
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Updates all model control objects based on the supplied model control object and whether or not corresponding multimodel examples should be updated in sync.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to update to.</param>
    ''' <param name="p_applyToGroup">True: All model control objects associated with the example will be updated.
    ''' False: Only the selected model control object will be updated.</param>
    ''' <remarks></remarks>
    Private Sub UpdateEditedMCModels(ByVal p_mcModel As cMCModel,
                                     ByVal p_applyToGroup As Boolean)
        UpdateExampleIDs(p_mcModel, p_applyToGroup)
        UpdateSelectedModelAndPath(p_mcModel)
    End Sub

    ''' <summary>
    ''' Updates the example ID in all relevant locations for all relevant models.
    ''' </summary>
    ''' <param name="p_mcModel">Model to use for the new ID.</param>
    ''' <param name="p_applyToGroup">True: Updates will be applied to all models in the same example group.</param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleIDs(ByVal p_mcModel As cMCModel,
                                 ByVal p_applyToGroup As Boolean)
        Dim oldID As Integer = mcModelSelected.ID.idExample
        Dim newID As Integer = p_mcModel.ID.idExample
        If (Not oldID = newID) Then
            If p_applyToGroup Then
                UpdateMCModelExampleIDForGroup(oldID, newID)
            Else
                UpdateMCModelExampleID(p_mcModel, newID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates the example ID for all model files in the example group.
    ''' Update is performed in the colleciton of model files and file paths.
    ''' </summary>
    ''' <param name="p_oldID">Original ID of the example group.</param>
    ''' <param name="p_newID">New ID of the example group.</param>
    ''' <remarks></remarks>
    Private Sub UpdateMCModelExampleIDForGroup(ByVal p_oldID As Integer,
                                               ByVal p_newID As Integer)
        ' Update models
        examplesMCModels.ChangeExampleID(p_oldID, p_newID)

        ' Update paths
        For Each pathSelected As cPathSelected In pathsSelected
            Dim pathModelID As Decimal = pathSelected.ID - Math.Floor(pathSelected.ID)
            Dim pathExampleID As Decimal = pathSelected.ID - pathModelID

            If pathExampleID = p_oldID Then pathSelected.ID = p_newID + pathModelID
        Next
    End Sub

    ''' <summary>
    ''' Updates the example ID for the provided model control object.
    ''' Update is performed in the colleciton of model files and file paths.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object update to in the model collection.</param>
    ''' <param name="p_newID">New ID of the example group.</param>
    ''' <remarks></remarks>
    Private Sub UpdateMCModelExampleID(ByVal p_mcModel As cMCModel,
                                       ByVal p_newID As Integer)
        ' Update models
        examplesMCModels.Replace(p_mcModel, p_pathIsSource:=False)

        ' Update paths
        For Each pathSelected As cPathSelected In pathsSelected
            Dim pathModelID As Decimal = pathSelected.ID - Math.Floor(pathSelected.ID)
            Dim pathExampleID As Decimal = pathSelected.ID - pathModelID

            If pathExampleID = mcModelSelected.ID.idCompositeDecimal Then
                pathSelected.ID = p_newID + pathModelID
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates all data related to the model control object in the selected model control and filepath objects.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to update to.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSelectedModelAndPath(ByVal p_mcModel As cMCModel)
        mcModelSelected = CType(p_mcModel.Clone, cMCModel)
        filePathSelected.SetMCModel(mcModelSelected)
    End Sub

    ''' <summary>
    ''' Updates the collection of all saved model control files.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateSavedMCModels()
        For Each model As cMCModel In examplesMCModels
            UpdateSavedMCModel(model)
        Next
    End Sub

    ''' <summary>
    ''' Updates the saved model control file.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <remarks></remarks>
    Private Sub UpdateSavedMCModel(Optional ByVal p_mcModel As cMCModel = Nothing)
        If p_mcModel Is Nothing Then p_mcModel = mcModelSelected
        Dim newModel As cMCModel = DirectCast(p_mcModel.Clone, cMCModel)

        ' The path is used to determine the object to replace in case the model ID was changed.
        If (Not _mcModelsSaved.Replace(newModel) OrElse
            Not _mcModelsSaved.Replace(newModel, p_pathIsSource:=True) OrElse
            Not _mcModelsSaved.Replace(newModel, p_pathIsSource:=False)) Then
            _mcModelsSaved.Add(newModel)
        End If
    End Sub

    ''' <summary>
    ''' Updates the currently selected model in the current collection of model objects.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateCurrentlySelectedMCModel()
        examplesMCModels.Replace(DirectCast(mcModelSelected.Clone, cMCModel))
    End Sub

    ''' <summary>
    ''' Sets data associated with the currently selected path that corresponds to the model file for a selected example.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCurrentlySelectedModelPath()
        For Each exampleRow As cPathSelected In dgFilesList.SelectedItems
            For Each filePath As cPathExample In filePaths.pathsSelected
                If filePath.ID = exampleRow.ID Then
                    filePathSelected = filePath
                    fileNameTable = filePathSelected.dataSource.fileNameWithExtension
                    _modelIDCurrent = filePathSelected.ID
                    Exit For
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Sets the currentlt selected model control object based on the selected model file path.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCurrentlySelectedMCModel()
        Dim nextMCModel As cMCModel = examplesMCModels(filePathSelected.ID)
        If nextMCModel IsNot Nothing Then
            mcModelSelected = nextMCModel
            _mcModelSelectedUnedited = DirectCast(mcModelSelected.Clone, cMCModel)

            If mcModelSelected.folderStructure = eFolderStructure.Database Then
                modelSelectedDir = "\" & cPathModel.DIR_NAME_MODELS_DEFAULT & "\"
            Else
                modelSelectedDir = ""
            End If

            applyToGroup = IsCommonPropertiesAppliedToGroup()
        End If
    End Sub


    ''' <summary>
    ''' Fills the cPathSelected collection with data from the cPathExample objects in the selected file paths.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillPathCollectionFromPathsSelected()
        pathsSelected.Clear()
        For Each pathExample As cPathExample In filePaths.pathsSelected
            pathsSelected.Add(New cPathSelected() With {.ID = pathExample.ID,
                                                        .IDString = pathExample.IDString,
                                                        .fileName = pathExample.fileName,
                                                        .exampleStatus = pathExample.exampleStatus
                                                       }
                                                   )
        Next
    End Sub

    ''' <summary>
    ''' Updates data in the cPathSelected collection from the cPathExample objects in the selected file paths.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdatePathCollectionFromPathSelected()
        For Each pathSelected As cPathSelected In pathsSelected
            If pathSelected.ID = _modelIDCurrent Then
                With pathSelected
                    .exampleStatus = filePathSelected.exampleStatus
                    .fileName = filePathSelected.fileName
                    .ID = filePathSelected.ID
                    .IDString = filePathSelected.IDString
                End With
                _modelIDCurrent = filePathSelected.ID
            End If
        Next
    End Sub

    ''' <summary>
    ''' Ensures a valid table file is being used, and automatically sets certain values based on this file, such as the current benchmark.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InitializeTableSourceAndProgramControl() As Boolean
        Try
            If mcModelSelected Is Nothing Then Return False

            If Not _dataSourceCurrentIsValid Then _dataSourceCurrentIsValid = SetValidDataSource()
            If Not _dataSourceCurrentIsValid Then Return False

            With mcModelSelected
                .programControl.FillData(_dataSourceCurrent)
                .SetDefaultProgramVersion()
                .SetDefaultProgramName()
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Updates the form, including the required buttons format, based on the state of the model control objects.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateFormByRequiredData()
        Try
            SetRequiredButtonFormat()
            CheckRequiredDataFilled()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Displays the form by which users add new objects to the model control file. The form is tailored to the type of object.
    ''' </summary>
    ''' <param name="p_objectType">Type of object for which the form is being created to add.</param>
    ''' <remarks></remarks>
    Private Sub DisplayObjectForm(ByVal p_objectType As eXMLObjectType)
        Dim windowCreateItem As frmXMLObjectCreateItem
        If p_objectType = eXMLObjectType.ExcelResult Then Exit Sub

        ' TODO: Make form able to load an existing object for editing.
        ' In this case, use 'IsItemAppliedToGroup' to determine whether or not to maintain the checkbox.
        ' The title of the item (or number/ID as string) will be used in the target form to fetch the appropriate object.
        'Dim applyToItemGroup As Boolean = IsItemAppliedToGroup(p_objectType, p_itemTitle)
        'windowCreateItem = New frmXMLObjectCreateItem(p_objectType, mcModelSelected, applyToItemGroup, p_itemTitle)
        Dim applyToItemGroup As Boolean = Not (mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel)
        windowCreateItem = New frmXMLObjectCreateItem(p_objectType, mcModelSelected, applyToItemGroup)

        windowCreateItem.ShowDialog()
        ApplyUpdatesToExampleGroup(windowCreateItem.myMCModelSave, p_objectType, windowCreateItem.applyToGroup)

        mcModelSelected = CType(windowCreateItem.myMCModelSave.Clone, cMCModel)
    End Sub


    ' Example updates
    '   Query Regarding Updates
    ''' <summary>
    ''' Returns 'True' if the item object corresponding to the type and title for the currently selected object is set to be applied to the entire example group.
    ''' </summary>
    ''' <param name="p_objectType">Object type for collections of multiple objects.</param>
    ''' <param name="p_itemTitle">Unique title by which the corresponding object can be identified from within the selected object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsItemAppliedToGroup(ByVal p_objectType As eXMLObjectType,
                                          ByVal p_itemTitle As String) As Boolean
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Return False

        Dim propSync As cMCPropsSync = examplesMCModels.propertiesSync(mcModelSelected.ID.idExample)
        Select Case p_objectType
            Case eXMLObjectType.Attachment, eXMLObjectType.Documentation, eXMLObjectType.SupportingFile
                Return DictionaryValue(propSync.attachments, p_itemTitle)
            Case eXMLObjectType.Image
                Return DictionaryValue(propSync.images, p_itemTitle)
            Case eXMLObjectType.Incident
                Return DictionaryValue(propSync.incidents, p_itemTitle)
            Case eXMLObjectType.Link
                Return DictionaryValue(propSync.links, p_itemTitle)
            Case eXMLObjectType.Ticket
                Return DictionaryValue(propSync.tickets, p_itemTitle)
            Case eXMLObjectType.Update
                Return DictionaryValue(propSync.updates, p_itemTitle)
            Case Else
                Return False
        End Select
    End Function
    ''' <summary>
    ''' Returns 'True' if the Excel result for the currently selected object is set to be applied to the entire example group.
    ''' Returns 'False' if there is no Excel result.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExcelResultAppliedToGroup() As Boolean
        Dim propSync As cMCPropsSync = examplesMCModels.propertiesSync(mcModelSelected.ID.idExample)
        Return propSync.excelResults
    End Function
    ''' <summary>
    ''' Returns 'True' if the benchmark reference for the currently selected object is set to be applied to the entire example group.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsBenchmarksAppliedToGroup() As Boolean
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Return False

        Dim propSync As cMCPropsSync = examplesMCModels.propertiesSync(mcModelSelected.ID.idExample)
        Return propSync.benchmarkReferences
    End Function
    ''' <summary>
    ''' Returns 'True' if the common properties for the currently selected object is set to be applied to the entire example group.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCommonPropertiesAppliedToGroup() As Boolean
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Return False

        Dim propSync As cMCPropsSync = examplesMCModels.propertiesSync(mcModelSelected.ID.idExample)
        Return propSync.common
    End Function
    ''' <summary>
    ''' Returns 'True' if the Example ID for the currently selected object is set to be applied to the entire example group.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExampleIDAppliedToGroup() As Boolean
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Return False

        Dim propSync As cMCPropsSync = examplesMCModels.propertiesSync(mcModelSelected.ID.idExample)
        Return propSync.idExample
    End Function

    ''' <summary>
    ''' Returns the dictionary value corresponding to the provided key.
    ''' Also returns 'False' if the key is not found.
    ''' </summary>
    ''' <param name="p_dictionary">Dictionary to check.</param>
    ''' <param name="p_key">Key to look up.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DictionaryValue(ByVal p_dictionary As Dictionary(Of String, Boolean),
                                     ByVal p_key As String) As Boolean
        If p_dictionary.ContainsKey(p_key) Then
            Return p_dictionary(p_key)
        Else
            Return False
        End If
    End Function

    '   Apply Example Updates
    ''' <summary>
    ''' For each object type, applies any changes between the currently selected and provided model control objects to all model control objects that are contained within the same example.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to compare against the selected model control object.</param>
    ''' <param name="p_objectType">Object type to check for differences and update.</param>
    ''' <remarks></remarks>
    Private Sub ApplyUpdatesToExampleGroup(ByVal p_mcModel As cMCModel,
                                           ByVal p_objectType As eXMLObjectType,
                                           Optional ByVal p_sync As Boolean = True)
        Dim example As List(Of cMCModel) = examplesMCModels.GetExampleModels(mcModelSelected)
        If example.Count > 1 Then
            Select Case p_objectType
                Case eXMLObjectType.Attachment,
                     eXMLObjectType.Documentation,
                     eXMLObjectType.SupportingFile

                    UpdateExampleAttachments(p_mcModel.attachments, p_sync)
                Case eXMLObjectType.Image
                    UpdateExampleImages(p_mcModel.images, p_sync)
                Case eXMLObjectType.Link
                    UpdateExampleLinks(p_mcModel.links, p_sync)
                Case eXMLObjectType.Incident
                    UpdateExampleIncidents(p_mcModel.incidents, p_sync)
                Case eXMLObjectType.Ticket
                    UpdateExampleTickets(p_mcModel.tickets, p_sync)
                Case eXMLObjectType.Update
                    UpdateExampleUpdates(p_mcModel.updates, p_sync)
            End Select
        End If
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleAttachments(ByVal p_items As cMCAttachments,
                                         Optional ByVal p_sync As Boolean = True)
        For Each newAttachment As cFileAttachment In p_items
            If (p_sync AndAlso Not mcModelSelected.attachments.Contains(newAttachment, New cMCAttachmentComparer)) Then
                examplesMCModels.AddReplaceAttachment(newAttachment, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newAttachment.title) = p_sync
        Next
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleImages(ByVal p_items As cMCAttachments,
                                    Optional ByVal p_sync As Boolean = True)
        For Each newImage As cFileAttachment In p_items
            If (p_sync AndAlso Not mcModelSelected.images.Contains(newImage, New cMCAttachmentComparer)) Then
                examplesMCModels.AddReplaceImage(newImage, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newImage.title) = p_sync
        Next
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleLinks(ByVal p_items As cMCLinks,
                                    Optional ByVal p_sync As Boolean = True)
        For Each newLink As cMCLink In p_items
            If (p_sync AndAlso Not mcModelSelected.links.Contains(newLink, New cMCLinkComparer)) Then
                examplesMCModels.AddReplaceLink(newLink, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newLink.title) = p_sync
        Next
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleIncidents(ByVal p_items As cMCIncidentsTickets,
                                        Optional ByVal p_sync As Boolean = True)
        For Each newIncident As Integer In p_items
            If (p_sync AndAlso Not mcModelSelected.incidents.Contains(newIncident)) Then
                examplesMCModels.AddIncident(newIncident, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newIncident.ToString) = p_sync
        Next
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleTickets(ByVal p_items As cMCIncidentsTickets,
                                    Optional ByVal p_sync As Boolean = True)
        For Each newTicket As Integer In p_items
            If (p_sync AndAlso Not mcModelSelected.tickets.Contains(newTicket)) Then
                examplesMCModels.AddTicket(newTicket, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newTicket.ToString) = p_sync
        Next
    End Sub
    ''' <summary>
    ''' If the selected model control object does not have a matching list of the objects provided, then all model control objects in the example will have their lists updated to include the new items.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleUpdates(ByVal p_items As cMCUpdates,
                                    Optional ByVal p_sync As Boolean = True)
        For Each newUpdate As cMCUpdate In p_items
            If (p_sync AndAlso Not mcModelSelected.updates.Contains(newUpdate, New cMCUpdateComparer)) Then
                examplesMCModels.AddReplaceUpdate(newUpdate, mcModelSelected.ID.idExample)
            End If
            examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).attachments(newUpdate.id.ToString) = p_sync
        Next
    End Sub

    Private Sub UpdateBenchmarkReferences(ByVal p_item As cMCBenchmarkRef,
                                          Optional ByVal p_sync As Boolean = True)
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Exit Sub

        examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).benchmarkReferences = p_sync

        If p_sync Then examplesMCModels.AddReplaceBenchmark(p_item, mcModelSelected.ID.idExample)
    End Sub


    Private Sub UpdateExcelResults(ByVal p_item As cMCResultsExcel,
                                    Optional ByVal p_sync As Boolean = True)
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then Exit Sub

        examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).excelResults = p_sync

        If p_sync Then examplesMCModels.AddReplaceExcelResult(p_item, mcModelSelected.ID.idExample)
    End Sub
    ''' <summary>
    ''' Updates relevant data in the currently selected model control object to the rest of the model control objects in the same example.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateExampleGroupModels(ByVal p_applyToGroup As Boolean)
        examplesMCModels.propertiesSync(mcModelSelected.ID.idExample).common = p_applyToGroup
        If p_applyToGroup Then
            With mcModelSelected
                examplesMCModels.AddReplaceCommonProperties(.title, .description, .ID.idExample)
            End With
        End If
    End Sub


    ' ==========
    ''' <summary>
    ''' Determines the example results by allowing the user to add and edit results based on an existing database file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetExampleResults()
        Try
            If Not _dataSourceCurrentIsValid Then Exit Sub

            mcModelSelected.programControl.FillData(_dataSourceCurrent)

            Dim results As List(Of cMCResult) = mcModelSelected.results.resultsRegular.ToList()
            mcModelSelected.results.InitializeTempIDs(results)

            windowXMLObjectResults = New frmXMLObjectResults(New ObservableCollection(Of cMCResult)(results), _dataSourceCurrent, mcModelSelected.programControl.baseUnits.ToList)
            windowXMLObjectResults.ShowDialog()

            If Not windowXMLObjectResults.formCancel Then
                Dim newResults As New List(Of cMCResult)(windowXMLObjectResults.allResultsSave)

                With mcModelSelected
                    Dim mergedResults As List(Of cMCResult) = .results.MergeChanges(newResults)
                    .UpdateExampleResults(.results.ConvertToBase(mergedResults))
                    .CreateOutputSettingsObject(p_fillFromResults:=True, p_dataSource:=filePathSelected.dataSource.path)
                End With
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the format of the buttons that are required to be clicked if certain data is missing.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRequiredButtonFormat()
        btnResults.Style = _styleBtnRequiredFulfilled
        btnBenchmark.Style = _styleBtnRequiredFulfilled

        With mcModelSelected
            If NoResultsSet() Then
                btnResults.Style = _styleBtnRequiredNeeded
            End If
            If (OnlyExcelResultsSet() AndAlso
                String.IsNullOrEmpty(mcModelSelected.program.programVersion)) Then
                btnBenchmark.Style = _styleBtnRequiredNeeded
            End If
        End With
    End Sub

    ''' <summary>
    ''' Sets the visibility and values of the 'apply to groups' checkbox.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExamplesGroupCheckBox()
        If mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then
            chkBxApplyToGroup.Visibility = Windows.Visibility.Collapsed
        Else
            chkBxApplyToGroup.Visibility = Windows.Visibility.Visible
            chkBxApplyToGroup.IsChecked = applyToGroup
        End If
    End Sub

    ''' <summary>
    ''' Returns 'True' if no results are set.
    ''' Does not count post-processed results.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NoResultsSet() As Boolean
        Return (mcModelSelected.results IsNot Nothing AndAlso
                mcModelSelected.results.Count = 0 AndAlso
                mcModelSelected.resultsExcel IsNot Nothing AndAlso
                mcModelSelected.resultsExcel.Count = 0)
    End Function

    ''' <summary>
    ''' Returns 'True' if the only results set are Excel results, if the benchmark is not set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OnlyExcelResultsSet() As Boolean
        Return (mcModelSelected.resultsExcel IsNot Nothing AndAlso
                mcModelSelected.resultsExcel.Count > 0 AndAlso
                mcModelSelected.results IsNot Nothing AndAlso
                mcModelSelected.results.Count = 0)
    End Function

    ''' <summary>
    ''' Sets the status of the files that are being turned into examples.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFilePathsStatus()
        For Each pathObject As cPathExample In filePaths.pathsSelected
            For Each mcModel As cMCModel In examplesMCModels
                If mcModel.ID.idCompositeDecimal = pathObject.ID Then
                    pathObject.exampleStatus = GetExampleStatus(mcModel)
                    Exit For
                End If
            Next
        Next
        FillPathCollectionFromPathsSelected()
    End Sub

    ''' <summary>
    ''' Sets the status of the model control object corresponding to the currently selected file path.
    ''' </summary>
    ''' <param name="p_currentFilePath">Currently selected file path.</param>
    ''' <param name="p_mcModel">Currently selected model control file.</param>
    ''' <remarks></remarks>
    Private Sub SetFilePathSelectedStatus(ByRef p_currentFilePath As cPathExample,
                                          ByVal p_mcModel As cMCModel)
        p_currentFilePath.exampleStatus = GetExampleStatus(p_mcModel)
    End Sub

    ''' <summary>
    ''' Sets the status of all model control objects in the example corresponding to the currently selected file path.
    ''' </summary>
    ''' <param name="p_currentModel">Currently selected model control object.</param>
    ''' <remarks></remarks>
    Private Sub SetFilePathSelectedGroupStatus(ByVal p_currentModel As cMCModel)
        Dim example As New List(Of cMCModel)

        example = examplesMCModels.GetExampleModels(p_currentModel)
        For Each mcModel As cMCModel In example
            For Each filepath As cPathExample In filePaths.pathsSelected
                If filepath.ID = mcModel.ID.idCompositeDecimal Then
                    filepath.exampleStatus = GetExampleStatus(mcModel)
                End If
            Next
        Next
        FillPathCollectionFromPathsSelected()
    End Sub

    ''' <summary>
    ''' For a given model control class, returns the status.
    ''' </summary>
    ''' <param name="p_mcModel">Model control class to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExampleStatus(ByVal p_mcModel As cMCModel) As String
        Return GetEnumDescription(GetExampleStatusEnum(p_mcModel))
    End Function
    ''' <summary>
    ''' For a given model control class, returns the status.
    ''' </summary>
    ''' <param name="p_mcModel">Model control class to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExampleStatusEnum(ByVal p_mcModel As cMCModel) As eExampleStatus
        If p_mcModel.RequiredDataFilled() Then
            If p_mcModel.changedSinceSave Then
                Return eExampleStatus.Complete
            Else
                Return eExampleStatus.Saved
            End If
        Else
            Return eExampleStatus.Incomplete
        End If
    End Function


    ''' <summary>
    ''' Checks whether the minimum required information is present and performs operations based on that state.
    ''' </summary>
    ''' <param name="p_updateAll">True: All model entries will be checked.</param>
    ''' <remarks></remarks>
    Private Sub CheckRequiredDataFilled(Optional p_updateAll As Boolean = False)
        If _formLoading Then Exit Sub

        btnSave.IsEnabled = mcModelSelected.RequiredDataFilled()
        UpdateCurrentlySelectedMCModel()
        btnSaveAll.IsEnabled = examplesMCModels.AllRequiredDataFilled()

        If p_updateAll Then
            SetFilePathsStatus()
        Else
            'Updates the currently selected model
            SetFilePathSelectedStatus(filePathSelected, mcModelSelected)
            If Not mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then SetFilePathSelectedGroupStatus(mcModelSelected)
        End If
        UpdateLayout()
        SetButtonContentBySelectedExample()
    End Sub

    ''' <summary>
    ''' Checks if the currently selected example has been changed since it was last saved during the current session.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckExampleChanged()
        Dim modelSaved As cMCModel = _mcModelsSaved(mcModelSelected.modelFile.pathDestination)
        If modelSaved IsNot Nothing AndAlso
            IO.File.Exists(mcModelSelected.mcFile.pathDestination.path) Then
            mcModelSelected.changedSinceSave = Not modelSaved.Equals(mcModelSelected)
        End If
    End Sub

    ''' <summary>
    ''' Checks all of the examples and determines if any have been edited but not saved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExamplesUnsaved() As Boolean
        For Each pathObject As cPathExample In filePaths.pathsSelected
            If ExampleUnsaved(pathObject) Then Return True
        Next

        Return False
    End Function

    ''' <summary>
    ''' Checks the example associated with the provided path (or selected path if not provided) and returns its edited status.
    ''' Returns 'True' if an example has been edited and not saved.
    ''' </summary>
    ''' <param name="p_path">Path object associated with the example to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExampleUnsaved(Optional p_path As cPathExample = Nothing) As Boolean
        If p_path Is Nothing Then p_path = filePathSelected
        If p_path.exampleStatus = GetEnumDescription(eExampleStatus.Saved) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Updates the results for saved model files that have Excel results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateModelsExcelResults()
        For Each mcModel As cMCModel In examplesMCModels
            If mcModel.resultsExcel.Count > 0 Then mcModel.updateResultsFromExcel = True
        Next

        examplesMCModels.UpdateResultsFromExcelFilesToMCFiles(eExampleStatus.Complete)
    End Sub

    ''' <summary>
    ''' Checks the collection of models to determine if any Excel results exist.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckExcelResultsExist()
        For Each model As cMCModel In examplesMCModels
            If model.resultsExcel.Count > 0 Then
                _excelResultsExist = True
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sets the text for each button that might change based on the selected example, such as add vs. edit, or add &amp; edit.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetButtonContentBySelectedExample()
        If String.IsNullOrEmpty(mcModelSelected.program.programVersion) Then
            btnBenchmark.Content = "Add Benchmark Version"
        Else
            btnBenchmark.Content = "Edit Benchmark Version"
        End If

        If mcModelSelected.results.resultsRegular.Count > 0 Then
            btnResults.Content = "Add/Edit Results"
        Else
            btnResults.Content = "Add Results"
        End If

        If mcModelSelected.results.resultsExcel.Count > 0 Then
            btnPopulateExcelResult.Content = "Repopulate Excel Results"
        Else
            btnPopulateExcelResult.Content = "Populate Excel Results"
        End If

        If mcModelSelected.resultsExcel.Count > 0 Then
            btnResultsExcel.Content = "Edit Excel Results"
        Else
            btnResultsExcel.Content = "Add Excel Results"
        End If
    End Sub

    ''' <summary>
    ''' Sets the visibility and enabled properties of the buttons that populate Excel results based on the states of examples.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPopulateExcelResultsButtons()
        If mcModelSelected.resultsExcel.Count > 0 Then
            btnPopulateExcelResult.Visibility = Windows.Visibility.Visible
            btnPopulateExcelResult.IsEnabled = btnValidate.IsEnabled
        Else
            btnPopulateExcelResult.Visibility = Windows.Visibility.Collapsed
        End If

        If _excelResultsExist Then
            btnPopulateExcelResults.Visibility = Windows.Visibility.Visible
            btnPopulateExcelResults.IsEnabled = btnValidateAll.IsEnabled
        Else
            btnPopulateExcelResults.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub

    ''' <summary>
    ''' True: The example has Excel results and is part of a multi-model Example.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExcelMultiModel() As Boolean
        Return (mcModelSelected.resultsExcel.Count > 0 AndAlso
                Not mcModelSelected.ID.multiModelType = cMCModelID.eMultiModelType.singleModel)
    End Function


    ' Validation
    ''' <summary>
    ''' Performs final checks of data validation and transfer to the selected example to be saved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmSave() As Boolean
        If Not FormDataIsValid() Then Return PromptFormDataIsInvalid()

        'Copy table data to model control object
        Dim dataSource As String = filePathSelected.dataSource.path
        mcModelSelected.dataSource.PathExportedTable.SetProperties(dataSource, p_suppressUserInput:=True)

        Return True
    End Function

    ''' <summary>
    ''' Performs final checks of data validation and transfer to all examples that are to be saved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmSaveAll() As Boolean
        If Not FormDataIsValidForAll() Then Return PromptFormDataIsInvalid()

        'Copy table data to all model control objects
        For Each filePath In filePaths.pathsSelected
            Dim mcModel As cMCModel = examplesMCModels(filePath.ID.ToString)

            Dim dataSource As String = filePath.dataSource.path
            mcModel.dataSource.PathExportedTable.SetProperties(dataSource, p_suppressUserInput:=True)
        Next

        Return True
    End Function

    ''' <summary>
    ''' Prompts the user as to the fact that some form data is invalid and will be skipped.
    ''' The user has the option to continue with the save.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PromptFormDataIsInvalid() As Boolean
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Hand),
                                            PROMPT_FORM_DATA_INVALID,
                                            TITLE_FORM_DATA_INVALID)
            Case eMessageActions.Yes
                Return True
            Case eMessageActions.No
                Return False
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Determines the validity of any data in the form that has not yet been validated for the current selection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormDataIsValid() As Boolean
        Dim tableExtension As String = filePathSelected.dataSource.fileExtension

        ' Without requiring check by loading a table, table names can only be verified by allowed extensions
        Return filePathSelected.dataSource.hasValidExtension
    End Function

    ''' <summary>
    ''' Determines the validity of any data in the form that has not yet been validated for the all examples.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormDataIsValidForAll() As Boolean
        For Each filePath As cPathExample In filePaths.pathsSelected
            If Not filePath.dataSource.hasValidExtension Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns 'True' if it is determined that the example has an existing file that has been saved.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExampleIsSaved() As Boolean
        Return (FormDataIsValid() AndAlso
                Not mcModelSelected.isFromSeedFile AndAlso
                IO.File.Exists(mcModelSelected.mcFile.pathDestination.path))
    End Function

    ''' <summary>
    ''' Determines if the all examples can be validated based on their status.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AllExamplesCanBeValidated() As Boolean
        For Each mcModel As cMCModel In examplesMCModels
            If Not ExampleCanBeValidated(mcModel) Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Determines if the specified example can be validated based on status.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExampleCanBeValidated(ByVal p_mcModel As cMCModel) As Boolean
        Return (GetExampleStatusEnum(p_mcModel) = eExampleStatus.Saved)
    End Function

#End Region

#Region "Methods: Private - Validate Table File Paths"

    ''' <summary>
    ''' Ensures that an existing and valid data source path is set. 
    ''' Returns 'True' when successful, or 'False' if aborted.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetValidDataSource() As Boolean
        _dataSourceCurrent = filePathSelected.dataSource.SetValidDataSourceByPath(_pathTable)

        If (Not String.IsNullOrEmpty(_dataSourceCurrent)) Then
            If CurrentDataSourceIsToBeSaved() Then fileNameTable = IO.Path.GetFileName(_dataSourceCurrent)
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns a prompt informing the user whether or not the database file chosen is valid based on the current database path.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckValidDataSource()
        If filePathSelected.dataSource.IsCurrentDataSourceValid(_pathTable) Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(),
                                                        PROMPT_DATABASE_FILENAME_VALID,
                                                        TITLE_DATABASE_FILENAME_CONFIRMATION))
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                        PROMPT_DATABASE_FILENAME_INVALID,
                                                        TITLE_DATABASE_FILENAME_CONFIRMATION))
        End If
    End Sub

    ''' <summary>
    ''' Returns 'True' if the criteria is met such that the database file name entry field should be left blank.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function txtBxDFileNameShouldBeEmpty() As Boolean
        If (TableFileNameMatchesModelAndView() AndAlso
            _filePathSelected.dataSource.isPathDefault) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Compares the table file names between those stored in the the model control file selected (model) 
    ''' and file path selected (view).
    ''' File extensions are ignored.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TableFileNameMatchesModelAndView() As Boolean
        Dim fileNameFromTable As String = IO.Path.GetFileNameWithoutExtension(_pathTable)
        'Dim fileNameFromModel As String = mcModelSelected.modelFile.pathDestination.fileName ' Currently highlighted out because this tends to be called after the file path selection is changed but before the model selection is changed.
        Dim fileNameFromModel As String = _filePathSelected.dataSource.fileName

        Return StringsMatch(fileNameFromTable, fileNameFromModel)
    End Function

    ''' <summary>
    ''' Determines whether or not the current database path is being saved with the model control file.
    ''' Otherwise, the path is being used on a temporary basis.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CurrentDataSourceIsToBeSaved() As Boolean
        Return StringsMatch(_dataSourceCurrent, _filePathSelected.dataSource.path)
    End Function

#End Region

End Class


