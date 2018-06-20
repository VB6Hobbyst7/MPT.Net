Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cMCModelID
Imports CSiTester.cMCModel
Imports CSiTester.cMCNameSyncer

Public Class frmXMLTemplateGenerator
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Prompts"
    Private Const _fTypeLblModel As String = "Model File"
    Private Const _fTypeLblModels As String = "Model Files"

    Private Const _TITLE_BROWSE As String = "Select Model Files Directory"

    Private Const _PROMPT_ID_RESERVED As String = "The ID entered is currently reserved."
#End Region

#Region "Fields"
    Private _nodesChanged As Boolean
    Private _exampleAction As eExampleAction
    Private _styleBtnRequiredNeeded As Style
    Private _styleBtnRequiredFulfilled As Style
    Private _styleDgRequiredNeeded As Style
    Private _styleDgRequiredFulfilled As Style

    ''' <summary>
    ''' List of model IDs that the edited model control file cannot be set to.
    ''' </summary>
    ''' <remarks></remarks>
    Private _IDsReserved As New List(Of Integer)
#End Region

#Region "Properties: Refactor to Model View Controller?"
    Private _mcModelsSet As New cMCSeedReader

    ''' <summary>
    ''' Path to the folder containing files to be added as examples. Model control XML files are generated for these.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property folderSource As String
        Get
            Return _mcModelsSet.folderSource
        End Get
    End Property

    ''' <summary>
    ''' Path to the folder that will contain the examples generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property folderDestination As String
        Get
            Return _mcModelsSet.folderDestination
        End Get
    End Property

    ''' <summary>
    ''' Model ID to be used for the first example generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property startingModelID As String
        Get
            Return _mcModelsSet.IDSchema.startingModelID
        End Get
    End Property

    Private _skippedModelIDs As String
    ''' <summary>
    ''' If multiple examples are being generated, reserved ID numbers can be specified individually, or as a range, to be skipped.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property skippedModelIDs As String
        Set(ByVal value As String)
            _skippedModelIDs = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("skippedModelIDs"))
        End Set
        Get
            Return _skippedModelIDs
        End Get
    End Property
#End Region

#Region "Properties: Edit Only"
    ''' <summary>
    ''' The last valid model ID entered.
    ''' This is used to reset to the last valid number when changing the model ID in an editing session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property priorExampleID As Integer

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

#Region "Properties"
    ''' <summary>
    ''' Class that contains paths to all files and directories within a specified directory, as well as a list of paths filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePaths As New cPathsExamples

    ''' <summary>
    ''' List of paths to all directories that contain the filtered files listed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsRelativeFiltered As New ObservableCollection(Of String)

    Private _folderSourceOverride As Boolean
    ''' <summary>
    ''' If true, the user is able to manually specify or alter the folder path specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property folderSourceOverride As Boolean
        Set(ByVal value As Boolean)
            If Not _folderSourceOverride = value Then
                _folderSourceOverride = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("folderSourceOverride"))
            End If
        End Set
        Get
            Return _folderSourceOverride
        End Get
    End Property

    Private _fileExtension As String
    ''' <summary>
    ''' File extension of the file type to be added as examples. Files within a given path are filtered by the file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileExtension As String
        Set(ByVal value As String)
            'value only set if a 3-character extension exists
            If Len(GetSuffix(value, ".")) = 3 Then
                If Not _fileExtension = GetSuffix(value, ".") Then
                    _fileExtension = value
                    If filePaths IsNot Nothing Then filePaths.SetPathsFiltered(value, True)
                    CheckRequiredDataFilled()
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileExtension"))
                End If
            End If
        End Set
        Get
            Return _fileExtension
        End Get
    End Property

    Private _fileExtensionOverride As Boolean
    ''' <summary>
    ''' If true, the user is allowed to write in or choose from a list, a file extension. If false, the file extension is left as the default for a given program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileExtensionOverride As Boolean
        Set(ByVal value As Boolean)
            If Not _fileExtensionOverride = value Then
                _fileExtensionOverride = value
                CheckRequiredDataFilled()
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileExtensionOverride"))
            End If
        End Set
        Get
            Return _fileExtensionOverride
        End Get
    End Property

    Private _secondaryIDSameAsModel As Boolean
    ''' <summary>
    ''' If true, the secondary ID is set to be the same as the model file name. If false, the user is allowed to write a custom name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property secondaryIDSameAsModel As Boolean
        Set(ByVal value As Boolean)
            If Not _secondaryIDSameAsModel = value Then
                _secondaryIDSameAsModel = value
                CheckRequiredDataFilled()
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("secondaryIDSameAsModel"))
            End If
        End Set
        Get
            Return _secondaryIDSameAsModel
        End Get
    End Property

    Private _renameXMLOnly As Boolean
    ''' <summary>
    ''' If true, only the XML file will be named according to the model ID or secondary ID. If false, the model file will also be renamed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property renameXMLOnly As Boolean
        Set(ByVal value As Boolean)
            If Not _renameXMLOnly = value Then
                _renameXMLOnly = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("renameXMLOnly"))
            End If
        End Set
        Get
            Return _renameXMLOnly
        End Get
    End Property

    Private _syncModelNameWithModelID As Boolean
    ''' <summary>
    ''' If 'renameXMLOnly' is true. If true, the name of the model file will be made the same as the model ID. If false, the name of the model will be made the same as the secondary ID.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property syncModelNameWithModelID As Boolean
        Set(ByVal value As Boolean)
            If Not _syncModelNameWithModelID = value Then
                _syncModelNameWithModelID = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("syncModelNameWithModelID"))
            End If
        End Set
        Get
            Return _syncModelNameWithModelID
        End Get
    End Property


    Public Property exampleType As String
    Public Property analysisClass As String

    Public Property codeRegion As String
    Public Property designClass As String
    Public Property designType As String

    Private _useSAP2000 As Boolean
    ''' <summary>
    ''' Target program includes SAP2000.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useSAP2000 As Boolean
        Set(ByVal value As Boolean)
            If Not _useSAP2000 = value Then
                _useSAP2000 = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("useSAP2000"))
            End If
        End Set
        Get
            Return _useSAP2000
        End Get
    End Property

    Private _useCSiBridge As Boolean
    ''' <summary>
    ''' Target program includes CSIBridge.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useCSiBridge As Boolean
        Set(ByVal value As Boolean)
            If Not _useCSiBridge = value Then
                _useCSiBridge = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("useCSiBridge"))
            End If
        End Set
        Get
            Return _useCSiBridge
        End Get
    End Property

    Private _useETABS As Boolean
    ''' <summary>
    ''' Target program includes ETABS.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useETABS As Boolean
        Set(ByVal value As Boolean)
            If Not _useETABS = value Then
                _useETABS = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("useETABS"))
            End If
        End Set
        Get
            Return _useETABS
        End Get
    End Property

    Private _useSAFE As Boolean
    ''' <summary>
    ''' Target program includes SAFE.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property useSAFE As Boolean
        Set(ByVal value As Boolean)
            If Not _useSAFE = value Then
                _useSAFE = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("useSAFE"))
            End If
        End Set
        Get
            Return _useSAFE
        End Get
    End Property

    Private _myMCModelSave As cMCModel
    ''' <summary>
    ''' Class representing the model control XML file to be generated or edited.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModelSave As cMCModel
        Set(ByVal value As cMCModel)
            _myMCModelSave = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myMCModelSave"))
        End Set
        Get
            Return _myMCModelSave
        End Get
    End Property

    Private _myMCModelTemp As cMCModel
    ''' <summary>
    ''' Temporary storage property for the base example class that might be updated from the form input.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property myMCModelTemp As cMCModel
        Set(ByVal value As cMCModel)
            _myMCModelTemp = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myMCModelTemp"))
        End Set
        Get
            Return _myMCModelTemp
        End Get
    End Property
#End Region

#Region "Initialization"

    Friend Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Initialize()
    End Sub

    Friend Sub New(ByRef p_mcModel As cMCModel,
                   Optional ByVal p_idsReserved As List(Of Integer) = Nothing,
                   Optional ByVal p_applyToGroup As Boolean = False)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Initialize(p_mcModel, p_idsReserved)
    End Sub

    Private Sub Initialize(Optional ByRef p_mcModel As cMCModel = Nothing,
                           Optional ByVal p_idsReserved As List(Of Integer) = Nothing,
                           Optional ByVal p_applyToGroup As Boolean = False)
        InitializeData(p_mcModel, p_idsReserved)

        SetDefaults()

        InitializeStyles()
        InitializeControls()
        CheckRequiredDataFilled()
    End Sub

    Private Sub InitializeData(Optional ByRef p_mcModel As cMCModel = Nothing,
                                Optional ByVal p_idsReserved As List(Of Integer) = Nothing,
                                Optional ByVal p_applyToGroup As Boolean = False)
        'Set overall form behavior
        If p_mcModel Is Nothing Then
            _exampleAction = eExampleAction.Create
            InitializeFormCreateExample()
        Else
            _exampleAction = eExampleAction.Edit
            priorExampleID = p_mcModel.ID.idExample
            If p_idsReserved IsNot Nothing Then _IDsReserved = p_idsReserved
            applyToGroup = p_applyToGroup

            InitializeFormEditExample(p_mcModel)
        End If

        InitializeTargetProgramDataAndControls(myMCModelTemp)
    End Sub

    Private Sub InitializeFormCreateExample()
        Dim mcModelTemp = New cMCModel()

        With mcModelTemp.author
            .name = testerSettings.userName
            .company = testerSettings.userCompany
        End With

        If mcModelTemp.targetProgram.Count = 0 Then mcModelTemp.targetProgram.Add(testerSettings.programName)

        myMCModelTemp = mcModelTemp
        UpdateDataGrid()
    End Sub

    Private Sub InitializeFormEditExample(ByRef p_mcModel As cMCModel)
        myMCModelTemp = CType(p_mcModel.Clone, cMCModel)
        myMCModelSave = p_mcModel
        secondaryIDSameAsModel = (myMCModelTemp.secondaryID = p_mcModel.modelFile.pathDestination.fileName)

        Me.Title = "Model Control File: General Details"
    End Sub

    Private Sub InitializeTargetProgramDataAndControls(ByVal p_model As cMCModel)
        useSAP2000 = False
        useCSiBridge = False
        useETABS = False
        useSAFE = False
        For Each targetProgram As eCSiProgram In p_model.targetProgram
            Select Case targetProgram
                Case eCSiProgram.SAP2000 : useSAP2000 = True
                Case eCSiProgram.CSiBridge : useCSiBridge = True
                Case eCSiProgram.ETABS : useETABS = True
                Case eCSiProgram.SAFE : useSAFE = True
            End Select
        Next

        chkBxProgramSAP2000.IsEnabled = False
        chkBxProgramCSiBridge.IsEnabled = False
        chkBxProgramETABS.IsEnabled = False
        chkBxProgramSAFE.IsEnabled = False

        'Currently SAFE & ETABS are never enabled, as they cannot contain any of the other program types, or be used by SAP2000/CSiBridge
        If (useCSiBridge OrElse useSAP2000) Then
            chkBxProgramSAP2000.IsEnabled = True
            chkBxProgramCSiBridge.IsEnabled = True
        End If

        txtBxModelIDStart.Text = _mcModelsSet.IDSchema.startingModelID
    End Sub


    Private Sub SetDefaults()
        If _exampleAction = eExampleAction.Create Then
            renameXMLOnly = True
            folderSourceOverride = False
            fileExtensionOverride = False
            secondaryIDSameAsModel = True
        ElseIf _exampleAction = eExampleAction.Edit Then
            If Not myMCModelSave.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then
                chkBxApplyToGroup.IsChecked = applyToGroup
            Else
                chkBxApplyToGroup.IsChecked = False
            End If
        End If
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
        _styleDgRequiredNeeded = New Style()

        mySetterForeground = New Setter()
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
        With _styleDgRequiredNeeded
            .Setters.Add(mySetterForeground)
            .Setters.Add(mySetterFontWeight)
            .TargetType = GetType(DataGrid)
        End With

        'Required button that has been satisfied
        _styleBtnRequiredFulfilled = New Style()
        _styleDgRequiredFulfilled = New Style()

        mySetterForeground = New Setter()
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
        With _styleDgRequiredFulfilled
            .Setters.Add(mySetterForeground)
            .Setters.Add(mySetterFontWeight)
            .TargetType = GetType(DataGrid)
        End With

    End Sub


    Private Sub InitializeControls()
        InitializeComboBoxes()

        'Set Defaults
        cmbBxFileExtension.IsEnabled = False

        If _exampleAction = eExampleAction.Create Then
            btnEditExample.Visibility = Windows.Visibility.Collapsed
            grpBxExampleIDs.Visibility = Windows.Visibility.Collapsed

            btnCreateExamples.IsEnabled = False

            chkBxOverrideFolderSource.IsChecked = False
            txtBxFolderSource.IsEnabled = False

            radBtnSelectFolder.IsChecked = True
            radBtnMultiModelAuto.IsChecked = True
            radBtnSyncSecondaryID.IsChecked = True
            radBtnFolderDB.IsChecked = True

            chkBxImportModel.IsChecked = False
            cmbBxVersion.IsEnabled = False

            'Adjust buttons formatting
            SetBtnRequiredFormatting()
        ElseIf _exampleAction = eExampleAction.Edit Then
            btnCreateExamples.Visibility = Windows.Visibility.Collapsed
            btnKeywords.Visibility = Windows.Visibility.Collapsed

            dgFilesList.Visibility = Windows.Visibility.Collapsed

            grpBxFiles.Visibility = Windows.Visibility.Collapsed
            grpBxExamplesDestination.Visibility = Windows.Visibility.Collapsed
            grpBxProgram.Visibility = Windows.Visibility.Collapsed
            grpBxMultiModel.Visibility = Windows.Visibility.Collapsed
            grpBxSyncNames.Visibility = Windows.Visibility.Collapsed
            grpBxFolderStructure.Visibility = Windows.Visibility.Collapsed
            grpBxIDs.Visibility = Windows.Visibility.Collapsed

            lblModelIDSkip.Visibility = Windows.Visibility.Collapsed
            txtBxModelIDSkip.Visibility = Windows.Visibility.Collapsed

            If myMCModelSave.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then
                chkBxApplyToGroup.Visibility = Windows.Visibility.Collapsed
            End If

            chkBxImportModel.IsChecked = myMCModelTemp.importedModel
            If chkBxImportModel.IsChecked Then
                cmbBxVersion.IsEnabled = True
            Else
                cmbBxVersion.IsEnabled = False
            End If

            grpBxPropertiesInitial.Header = "Database Properties"
        End If
    End Sub

    Private Sub InitializeComboBoxes()
        '===Combo Boxes
        SetComboBoxesProgramFileType()

        'Classification 1
        cmbBxClassification1.ItemsSource = testerSettings.exampleClassificationLvl1
        cmbBxClassification1.SelectedIndex = GetSelectedIndex(myMCModelTemp.classification.level1, testerSettings.exampleClassificationLvl1)

        'Classification 2
        cmbBxClassification2.ItemsSource = testerSettings.exampleClassificationLvl2
        cmbBxClassification2.SelectedIndex = GetSelectedIndex(myMCModelTemp.classification.level2, testerSettings.exampleClassificationLvl2)

        'Example Analysis Type
        cmbBxExampleType.ItemsSource = testerSettings.exampleType
        cmbBxExampleType.SelectedIndex = GetSelectedIndex(myMCModelTemp.exampleType, testerSettings.exampleType)

        'Example Analysis Class
        cmbBxClassAnalysis.ItemsSource = testerSettings.exampleClassAnalysis
        cmbBxClassAnalysis.SelectedIndex = GetSelectedIndex(myMCModelTemp.analysisClass, testerSettings.exampleClassAnalysis)

        'Example Design Type
        cmbBxDesignType.ItemsSource = testerSettings.designType
        cmbBxDesignType.SelectedIndex = GetSelectedIndex(myMCModelTemp.designType, testerSettings.designType)

        'Example Design Code Region
        cmbBxCodeRegion.ItemsSource = testerSettings.exampleCodeRegion
        cmbBxCodeRegion.SelectedIndex = GetSelectedIndex(myMCModelTemp.codeRegion, testerSettings.exampleCodeRegion)

        'Example Design Class
        cmbBxClassDesign.ItemsSource = testerSettings.exampleClassDesign
        cmbBxClassDesign.SelectedIndex = GetSelectedIndex(myMCModelTemp.designClass, testerSettings.exampleClassDesign)

        'Import Version
        SetComboBoxImports()

        'Date/Time Combo Boxes
        SetDateComboBoxes(cmbBxYear, cmbBxMonth, cmbBxDay, True)

        'Adjusts the design combo boxes based on the analysis type
        SetComboBoxesDesign()
    End Sub

#End Region

#Region "Form Controls"
    Private Sub btnCreateExamples_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateExamples.Click
        PopulateMCModelFromForm()

        _mcModelsSet.SetReservedIDs(skippedModelIDs)
        _mcModelsSet.filePaths = filePaths
        If _mcModelsSet.Fill(myMCModelTemp) Then
            If _mcModelsSet.mcModels.Count = 0 Then
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Stop),
                                                            "Please select a model file.",
                                                            "No model file selected"))
                Exit Sub
            Else
                Dim windowXMLTemplateGeneratorUnique As New frmXMLTemplateGeneratorUnique(_mcModelsSet.mcModels, filePaths)
                windowXMLTemplateGeneratorUnique.ShowDialog()

                Me.Close()
            End If
        End If
    End Sub
    Private Sub btnEditExample_Click(sender As Object, e As RoutedEventArgs) Handles btnEditExample.Click
        SetClassifications(myMCModelTemp)
        SetChkBxsTargetProgram(myMCModelTemp)

        myMCModelSave = CType(myMCModelTemp.Clone, cMCModel)

        Me.Close()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnFileSources_Click(sender As Object, e As RoutedEventArgs) Handles btnFileSources.Click
        Dim filePathsTemp As New List(Of String)
        Dim fileExtensionsTemp As New List(Of String)
        Dim startDir As String

        'Check if file extension is valid. If so, use it. Otherwise, use no extension for the filter
        If GetSuffix(fileExtension, ".").Count = 3 Then
            fileExtensionsTemp.Add(fileExtension)
        End If

        If Not String.IsNullOrEmpty(_mcModelsSet.folderSource) Then
            startDir = _mcModelsSet.folderSource
        Else
            startDir = pathStartup()
        End If

        If BrowseForFiles(filePathsTemp, startDir, CStr(cmbBxProgram.SelectedItem) & " " & _fTypeLblModels, fileExtensionsTemp) Then
            filePathsTemp.Sort()
            'If file extension not set, set it to that of the first file selected
            If fileExtensionsTemp Is Nothing Then fileExtension = GetSuffix(filePathsTemp(0), ".")

            'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
            filePaths = New cPathsExamples(filePathsTemp)

            'Generate a list of file paths of only the file extension type provided
            filePaths.SetPathsFiltered(fileExtension, True)
            pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

            'Set button styles to be normal
            btnFileSources.Style = Nothing
            btnFolderSource.Style = Nothing

            UpdateDataGrid()
        End If

        'Add folder source of last set of selected files
        If filePaths.pathsFiltered.Count > 0 Then
            _mcModelsSet.folderSource = filePaths.folderSource
        End If

        CheckRequiredDataFilled()
    End Sub

    Private Sub btnFolderSource_Click(sender As Object, e As RoutedEventArgs) Handles btnFolderSource.Click
        Dim filePathsTemp As New List(Of String)
        Dim dirPathStart As String
        Dim pathTemp As String = ""

        If Not String.IsNullOrEmpty(txtBxFolderSource.Text) Then
            dirPathStart = txtBxFolderSource.Text
        Else
            dirPathStart = pathStartup()
        End If

        pathTemp = BrowseForFolder(_TITLE_BROWSE, dirPathStart)

        'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
        If Not String.IsNullOrEmpty(pathTemp) Then
            _mcModelsSet.folderSource = pathTemp

            filePaths = New cPathsExamples(_mcModelsSet.folderSource)

            'Generate a list of file paths of only the file extension type provided
            filePaths.SetPathsFiltered(fileExtension, True)

            SetProgramAndFileTypeByExistingFiles()

            pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

            txtBxFolderSource.Text = _mcModelsSet.folderSource

            'Set button styles to be normal
            btnFileSources.Style = Nothing
            btnFolderSource.Style = Nothing

            UpdateDataGrid()
        End If

        CheckRequiredDataFilled()
    End Sub

    Private Sub btnFolderDestination_Click(sender As Object, e As RoutedEventArgs) Handles btnFolderDestination.Click
        If _mcModelsSet.BrowseModelDestination() Then txtBxFolderDestination.Text = _mcModelsSet.folderDestination
    End Sub

    Private Sub btnKeywords_Click(sender As Object, e As RoutedEventArgs) Handles btnKeywords.Click
        PopulateMCModelFromForm()
        Dim windowXMLNodeCreateKeywords As New frmMCKeywords(myMCModelTemp.keywords)

        windowXMLNodeCreateKeywords.ShowDialog()

        myMCModelTemp.keywords = windowXMLNodeCreateKeywords.keywordsSave
    End Sub
    Private Sub btnAdvancedOpts_Click(sender As Object, e As RoutedEventArgs) Handles btnAdvancedOpts.Click
        Dim windowXMLTemplateGeneratorAdvanced As New frmXMLTemplateGeneratorAdvanced(myMCModelTemp, _exampleAction)

        windowXMLTemplateGeneratorAdvanced.ShowDialog()

        myMCModelTemp = windowXMLTemplateGeneratorAdvanced.myMCModel
    End Sub

    '=== Checkboxes
    Private Sub chkBxOverrideFolderSource_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideFolderSource.Checked
        txtBxFolderSource.IsEnabled = True
    End Sub
    Private Sub chkBxOverrideFolderSource_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideFolderSource.Unchecked
        txtBxFolderSource.IsEnabled = False
    End Sub

    Private Sub chkBxOverrideFileExtension_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideFileExtension.Checked
        cmbBxFileExtension.IsEnabled = True
    End Sub
    Private Sub chkBxOverrideFileExtension_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideFileExtension.Unchecked
        cmbBxFileExtension.IsEnabled = False
    End Sub

    Private Sub chkBxSecondaryIDAsModelName_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxSecondaryIDAsModelName.Checked
        txtBxSecondaryID.IsEnabled = False
        If myMCModelTemp IsNot Nothing Then myMCModelTemp.secondaryID = ""

        'Undo suppress secondary ID syncing (Done as duplicate secondary IDs will cause problems)
        radBtnSyncSecondaryID.IsEnabled = True
    End Sub
    Private Sub chkBxSecondaryIDAsModelName_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxSecondaryIDAsModelName.Unchecked
        txtBxSecondaryID.IsEnabled = True

        'Suppress secondary ID syncing, as duplicate secondary IDs will cause problems
        radBtnSyncSecondaryID.IsEnabled = False
        radBtnSyncSecondaryID.IsChecked = False
        radBtnSyncModelID.IsChecked = True
        syncModelNameWithModelID = True
    End Sub

    Private Sub chkBxSecondaryIDAsModelNameEdit_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxSecondaryIDAsModelNameEdit.Checked
        txtBxSecondaryIDEdit.IsEnabled = False
        If _exampleAction = eExampleAction.Edit Then
            If myMCModelTemp IsNot Nothing Then txtBxSecondaryIDEdit.Text = myMCModelTemp.modelFile.pathDestination.fileName
        End If
        'myMCModel.secondaryID = ""
    End Sub
    Private Sub chkBxSecondaryIDAsModelNameEdit_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxSecondaryIDAsModelNameEdit.Unchecked
        txtBxSecondaryIDEdit.IsEnabled = True
    End Sub

    Private Sub chkBxImportModel_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxImportModel.Checked
        cmbBxVersion.IsEnabled = True
        myMCModelTemp.importedModel = True
    End Sub
    Private Sub chkBxImportModel_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxImportModel.Unchecked
        cmbBxVersion.IsEnabled = False
        myMCModelTemp.importedModel = False
    End Sub

    '=== Radio Buttons
    Private Sub radBtnSelectFiles_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSelectFiles.Checked
        btnFileSources.IsEnabled = True
        btnFolderSource.IsEnabled = False
        chkBxOverrideFolderSource.IsChecked = False
        SetBtnRequiredFormatting()
    End Sub
    Private Sub radBtnSelectFolder_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSelectFolder.Checked
        btnFileSources.IsEnabled = False
        btnFolderSource.IsEnabled = True
        SetBtnRequiredFormatting()
    End Sub

    Private Sub radBtnMultiModelNone_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnMultiModelNone.Checked
        _mcModelsSet.multiModelMethod = eMultiModelIDNumbering.None
    End Sub
    Private Sub radBtnMultiModelAll_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnMultiModelAll.Checked
        _mcModelsSet.multiModelMethod = eMultiModelIDNumbering.All
    End Sub
    Private Sub radBtnMultiModelAuto_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnMultiModelAuto.Checked
        _mcModelsSet.multiModelMethod = eMultiModelIDNumbering.Auto
    End Sub

    Private Sub radBtnSyncModelID_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSyncModelID.Checked
        syncModelNameWithModelID = True
    End Sub
    Private Sub radBtnSyncSecondaryID_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSyncSecondaryID.Checked
        syncModelNameWithModelID = False
    End Sub

    Private Sub radBtnFolderDB_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnFolderDB.Checked
        _mcModelsSet.folderStructure = eFolderStructure.Database
    End Sub
    Private Sub radBtnFolderFlattened_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnFolderFlattened.Checked
        _mcModelsSet.folderStructure = eFolderStructure.Flattened
    End Sub

    '=== Combo Boxes
    Private Sub cmbBxProgram_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxProgram.SelectionChanged
        ' Update the settings object to use settings for the selected program
        testerSettings.programName = ConvertStringToEnumByDescription(Of eCSiProgram)(CStr(cmbBxProgram.SelectedItem))
        testerSettings.UpdateProgramDefaults()

        ' Update the model control object
        If myMCModelTemp IsNot Nothing Then
            myMCModelTemp.targetProgram.Clear()
            myMCModelTemp.targetProgram.Add(testerSettings.programName)

            myMCModelTemp.program.programName = myMCModelTemp.targetProgram.primary          'Set default of benchmark program equal to specified program for the example.

            'Set file extensions drop box to dynamically update for the selected program
            cmbBxFileExtension.ItemsSource = testerSettings.GetFileTypes(myMCModelTemp.targetProgram.primary)
            cmbBxFileExtension.SelectedIndex = 0

            If exampleType = "Analysis" Then
                designType = ""
                cmbBxDesignType.ItemsSource = Nothing
                cmbBxDesignType.SelectedIndex = 0
                cmbBxDesignType.IsEnabled = False
            Else
                cmbBxDesignType.ItemsSource = testerSettings.designTypeFiltered
                cmbBxDesignType.SelectedIndex = 0
                If testerSettings.designTypeFiltered.Count = 0 Then
                    cmbBxDesignType.IsEnabled = False
                Else
                    cmbBxDesignType.IsEnabled = True
                End If
                designType = CStr(cmbBxDesignType.SelectedItem)
            End If

            SetComboBoxImports()

            InitializeTargetProgramDataAndControls(myMCModelTemp)
        End If
    End Sub
    Private Sub cmbBxFileExtension_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxFileExtension.SelectionChanged
        fileExtension = GetSuffix(CStr(cmbBxFileExtension.SelectedItem), ".")
        pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

        UpdateDataGrid()
    End Sub
    Private Sub cmbBxFileExtension_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxFileExtension.LostKeyboardFocus
        fileExtension = GetSuffix(cmbBxFileExtension.Text, ".")
        pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

        UpdateDataGrid()
    End Sub

    Private Sub cmbBxClassification1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxClassification1.SelectionChanged
        If myMCModelTemp IsNot Nothing Then
            With myMCModelTemp
                .classification.level1 = CStr(cmbBxClassification1.SelectedItem)

                'Update the Classification 2 combo box
                testerSettings.SetClassification2List(.classification.level1)
                cmbBxClassification2.ItemsSource = testerSettings.exampleClassificationLvl2
                cmbBxClassification2.SelectedIndex = 0

                'Update documentation default status
                'TODO: Later, reference name property of classification 1 to property selected to match below
                If Not .classification.level1 = "Regression Example" Then
                    .statusDocumentation = testerSettings.documentationStatusTypes(1)         'TODO
                End If
            End With
        End If
    End Sub
    Private Sub cmbBxClassification2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxClassification2.SelectionChanged
        If myMCModelTemp IsNot Nothing Then myMCModelTemp.classification.level2 = CStr(cmbBxClassification2.SelectedItem)
    End Sub

    Private Sub cmbBxExampleType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxExampleType.SelectionChanged
        exampleType = CStr(cmbBxExampleType.SelectedItem)

        SetComboBoxesDesign()
    End Sub

    Private Sub cmbBxClassAnalysis_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxClassAnalysis.SelectionChanged
        analysisClass = CStr(cmbBxClassAnalysis.SelectedItem)
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxClassAnalysis_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxClassAnalysis.LostKeyboardFocus
        analysisClass = GetSuffix(cmbBxClassAnalysis.Text, ".")
        CheckRequiredDataFilled()
    End Sub

    Private Sub cmbBxDesignType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxDesignType.SelectionChanged
        designType = CStr(cmbBxDesignType.SelectedItem)
    End Sub
    Private Sub cmbBxCodeRegion_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxCodeRegion.SelectionChanged
        codeRegion = CStr(cmbBxCodeRegion.SelectedItem)
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxCodeRegion_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxCodeRegion.LostKeyboardFocus
        codeRegion = GetSuffix(cmbBxCodeRegion.Text, ".")
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxClassDesign_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxClassDesign.SelectionChanged
        designClass = CStr(cmbBxClassDesign.SelectedItem)
        CheckRequiredDataFilled()
    End Sub
    Private Sub cmbBxClassDesign_LostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles cmbBxClassDesign.LostKeyboardFocus
        designClass = GetSuffix(cmbBxClassDesign.Text, ".")
        CheckRequiredDataFilled()
    End Sub

    Private Sub cmbBxYear_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxYear.SelectionChanged
        If myMCModelTemp IsNot Nothing Then myMCModelTemp.exampleDate.numYear = myCInt(cmbBxYear.SelectedItem.ToString)
    End Sub
    Private Sub cmbBxMonth_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxMonth.SelectionChanged
        If myMCModelTemp IsNot Nothing Then
            With myMCModelTemp
                .exampleDate.numMonth = myCInt(cmbBxMonth.SelectedItem.ToString)

                UpdateDayComboBox(.exampleDate.numMonth, cmbBxDay)
            End With
        End If
    End Sub
    Private Sub cmbBxDay_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxDay.SelectionChanged
        If myMCModelTemp IsNot Nothing Then myMCModelTemp.exampleDate.numDay = myCInt(cmbBxDay.SelectedItem.ToString)
    End Sub

    '=== Text Boxes
    Private Sub txtBxFolderSource_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxFolderSource.LostFocus
        _mcModelsSet.folderSource = txtBxFolderSource.Text

        'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
        If Not String.IsNullOrEmpty(_mcModelsSet.folderSource) Then
            If IO.Directory.Exists(_mcModelsSet.folderSource) Then
                filePaths = New cPathsExamples(_mcModelsSet.folderSource)

                'Generate a list of file paths of only the file extension type provided
                filePaths.SetPathsFiltered(fileExtension, True)
                pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

                UpdateDataGrid()
            End If
        End If

        RequiredDataFilled()
    End Sub
    Private Sub txtBxFolderDestination_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxFolderDestination.LostFocus
        _mcModelsSet.folderDestination = txtBxFolderDestination.Text
    End Sub
    Private Sub txtBxModelIDStart_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxModelIDStart.LostFocus
        _mcModelsSet.IDSchema.startingModelID = txtBxModelIDStart.Text
    End Sub

    Private Sub txtBxModelIDEdit_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxIDEdit.LostFocus
        If Not ExampleIDIsValid() Then
            myMCModelTemp.ID.idExample = priorExampleID
        Else
            priorExampleID = myMCModelTemp.ID.idExample
        End If
    End Sub

    Private Sub txtBxAuthor_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxAuthor.TextChanged
        CheckRequiredDataFilled()
    End Sub
    Private Sub txtBxCompany_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxCompany.TextChanged
        CheckRequiredDataFilled()
    End Sub
#End Region

#Region "Form: Behavior"
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        'TODO: In the future, create a temporary settings file as a clone from the settings file and use this for example generation/editing,
        ' with defaults updated for program name.
        ' testerSettings was first modified in: cmbBxProgram_SelectionChanged

        ' Revert the changes in the tester settings object back to that for the testing runs
        testerSettings.programName = myRegTest.program_name
        testerSettings.UpdateProgramDefaults()
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' If the currently selected folder does not have any matching files of the selected file type, the program and default file type are changed until a matching file type is found.
    ''' If no match is found, the system is returned to its initial state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetProgramAndFileTypeByExistingFiles()
        Dim progamIndex As Integer = -1
        Dim originalProgramIndex As Integer = cmbBxProgram.SelectedIndex
        Dim originalFileExtension As String = fileExtension

        While filePaths.pathsFiltered.Count = 0
            'Cycle through dropdown selection to find a matching type
            progamIndex += 1
            If progamIndex > testerSettings.programs.Count - 1 Then 'Undo changes and exit loop
                fileExtension = originalFileExtension
                myMCModelTemp.targetProgram.AddAsFirst(ConvertStringToEnumByDescription(Of eCSiProgram)(testerSettings.programs(originalProgramIndex)))
                SetComboBoxesProgramFileType()
                Exit While
            End If

            myMCModelTemp.targetProgram.AddAsFirst(ConvertStringToEnumByDescription(Of eCSiProgram)(testerSettings.programs(progamIndex)))

            SetComboBoxesProgramFileType()
            fileExtension = GetSuffix(CStr(cmbBxFileExtension.SelectedItem), ".")
            filePaths.SetPathsFiltered(fileExtension, True)
        End While
    End Sub

    ''' <summary>
    ''' Sets the style for the rquired paths browsing buttons for files or folders.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetBtnRequiredFormatting()
        Dim filePathsRequired As Boolean = True

        If (filePaths IsNot Nothing AndAlso
            Not filePaths.pathsAll.Count = 0) Then filePathsRequired = False

        'Adjust buttons formatting
        If filePathsRequired Then
            If radBtnSelectFiles.IsChecked Then
                btnFileSources.Style = _styleBtnRequiredNeeded
                btnFolderSource.Style = Nothing
            ElseIf radBtnSelectFolder.IsChecked Then
                btnFileSources.Style = Nothing
                btnFolderSource.Style = _styleBtnRequiredNeeded
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sets the style for the headers of the datagrid to indicate that valid files need to be references as part of setting up required data.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDgRequiredFormatting()
        dgFilesList.Style = _styleDgRequiredNeeded
        If (filePaths IsNot Nothing AndAlso
            filePaths.pathsFiltered IsNot Nothing AndAlso
            filePaths.pathsFiltered.Count > 0) Then

            dgFilesList.Style = _styleDgRequiredFulfilled
        End If
    End Sub

    ''' <summary>
    ''' Updates the datagrid reference and adjusts column visibilities.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateDataGrid()
        Dim tempString As String = ""
        Dim tempList As New ObservableCollection(Of String)

        dgFilesList.ItemsSource = filePaths.pathsFiltered

        'Update DG style
        SetDgRequiredFormatting()

        'If no relative paths exist, hide the relative paths column
        tempList = filePaths.GetPathStubsFiltered()
        For Each pathStub As String In tempList
            If Not String.IsNullOrEmpty(pathStub) Then tempString = pathStub
        Next
        If String.IsNullOrEmpty(tempString) Then
            dgColExampleRelpath.Visibility = Windows.Visibility.Collapsed
        Else
            dgColExampleRelpath.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    ''' <summary>
    ''' Sets the design-related combo boxes to be enabled or disabled, and adjusts data accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComboBoxesDesign()
        'Disable design-specific controls if they do not apply
        If exampleType = "Analysis" Then
            'Design Type
            designType = ""
            cmbBxDesignType.ItemsSource = Nothing
            cmbBxDesignType.SelectedIndex = 0
            cmbBxDesignType.IsEnabled = False

            'Code Region
            codeRegion = ""
            cmbBxCodeRegion.ItemsSource = Nothing
            cmbBxCodeRegion.SelectedIndex = 0
            cmbBxCodeRegion.IsEnabled = False

            'Design Class
            designClass = ""
            cmbBxClassDesign.ItemsSource = Nothing
            cmbBxClassDesign.SelectedIndex = 0
            cmbBxClassDesign.IsEnabled = False
        Else
            'Design Type
            cmbBxDesignType.ItemsSource = testerSettings.designTypeFiltered
            cmbBxDesignType.SelectedIndex = 0
            If testerSettings.designTypeFiltered.Count = 0 Then
                cmbBxDesignType.IsEnabled = False
            Else
                cmbBxDesignType.IsEnabled = True
            End If
            designType = CStr(cmbBxDesignType.SelectedItem)

            'Code Region
            cmbBxCodeRegion.ItemsSource = testerSettings.exampleCodeRegion
            cmbBxCodeRegion.SelectedIndex = 0
            cmbBxCodeRegion.IsEnabled = True
            codeRegion = CStr(cmbBxCodeRegion.SelectedItem)

            'Design Class
            cmbBxClassDesign.ItemsSource = testerSettings.exampleClassDesign
            cmbBxClassDesign.SelectedIndex = 0
            cmbBxClassDesign.IsEnabled = True
            designClass = CStr(cmbBxClassDesign.SelectedItem)
        End If
    End Sub

    Private Sub SetComboBoxImports()
        cmbBxVersion.ItemsSource = testerSettings.GetProgramVersions(myMCModelTemp.targetProgram.primary)
        cmbBxVersion.SelectedIndex = GetSelectedIndex(myMCModelTemp.importedModelVersion, testerSettings.programVersions)
    End Sub

    Private Sub SetComboBoxesProgramFileType()

        'Program
        cmbBxProgram.ItemsSource = testerSettings.programs

        Dim programName As String = GetEnumDescription(myMCModelTemp.targetProgram.primary)
        cmbBxProgram.SelectedIndex = GetSelectedIndex(programName, testerSettings.programs)

        SetComboBoxesFileExtension()
    End Sub

    Private Sub SetComboBoxesFileExtension()
        Dim programName As String = GetEnumDescription(myMCModelTemp.targetProgram.primary)
        'File Type
        cmbBxFileExtension.ItemsSource = testerSettings.GetFileTypes(myMCModelTemp.targetProgram.primary)
        cmbBxFileExtension.SelectedIndex = GetSelectedIndex(GetSuffix(programName, "."), testerSettings.GetFileTypes(myMCModelTemp.targetProgram.primary))
    End Sub

    ''' <summary>
    ''' Checks whether the minimum required information is present and performs operations based on that state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckRequiredDataFilled()
        If myMCModelTemp IsNot Nothing Then
            If RequiredDataFilled() Then
                btnCreateExamples.IsEnabled = True
                btnEditExample.IsEnabled = True
            Else
                btnCreateExamples.IsEnabled = False
                btnEditExample.IsEnabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Checks whether the minimum required information has been specified for creating example XML control files.
    ''' </summary>
    ''' <returns>True if all required data is filled. Otherwise, false</returns>
    ''' <remarks></remarks>
    Private Function RequiredDataFilled() As Boolean
        RequiredDataFilled = True

        Try
            If _exampleAction = eExampleAction.Create Then
                If String.IsNullOrEmpty(_mcModelsSet.IDSchema.startingModelID) Then Return False
                If (String.IsNullOrEmpty(fileExtension) AndAlso fileExtensionOverride) Then Return False
                If filePaths.pathsFiltered.Count = 0 Then Return False
            ElseIf _exampleAction = eExampleAction.Edit Then
                If myMCModelTemp.ID.idComposite = "0" Then Return False
            End If

            With myMCModelTemp
                If Not .author.RequiredDataFilled Then Return False

                If cmbBxClassAnalysis.IsEnabled Then
                    If String.IsNullOrEmpty(analysisClass) Then Return False
                End If

                If cmbBxDesignType.IsEnabled Then
                    If String.IsNullOrEmpty(codeRegion) Then Return False
                    If String.IsNullOrEmpty(designClass) Then Return False
                End If
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Function

    ''' <summary>
    ''' Assigns form properties to the corresponding model control object properties.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateMCModelFromForm()
        SetNameAndFolderProperties(myMCModelTemp)
        SetClassifications(myMCModelTemp)
        SetChkBxsTargetProgram(myMCModelTemp)
    End Sub

    ''' <summary>
    ''' Sets the names and folder structure properties of the model control object and properties based on the form properties.
    ''' </summary>
    ''' <param name="p_mcModel">MC Model objec to set the properties on.</param>
    ''' <remarks></remarks>
    Private Sub SetNameAndFolderProperties(ByRef p_mcModel As cMCModel)
        With p_mcModel
            ' Begun as assigned as it affects paths. 
            ' Once path sources are all established, this will be assigned to the form value.
            .folderStructure = eFolderStructure.NotSpecified

            ' Name syncing
            If secondaryIDSameAsModel Then
                .secondaryIDSynced = eNameSync.ModelFileName
            Else
                .secondaryIDSynced = eNameSync.Custom
            End If
            If syncModelNameWithModelID Then
                .mcFile.PathModelControl.nameSynced = eNameSync.ModelControlID
            Else
                .mcFile.PathModelControl.nameSynced = eNameSync.ModelControlSecondaryID
            End If
            If renameXMLOnly Then
                .modelFile.PathModelDestination.nameSynced = eNameSync.Custom
            Else
                .modelFile.PathModelDestination.nameSynced = .mcFile.PathModelControl.nameSynced
            End If
        End With
    End Sub

    ''' <summary>
    ''' Sets the classifications of the MC Model based on the form properties.
    ''' </summary>
    ''' <param name="p_mcModel">MC Model objec to set the properties on.</param>
    ''' <remarks></remarks>
    Private Sub SetClassifications(ByRef p_mcModel As cMCModel)
        With p_mcModel
            .analysisClass = analysisClass
            .codeRegion = codeRegion
            .exampleType = exampleType

            .designClass = designClass
            .designType = designType
            If .importedModel Then
                If Not String.IsNullOrEmpty(cmbBxVersion.Text) Then
                    .importedModelVersion = cmbBxVersion.Text
                Else
                    .importedModelVersion = cmbBxVersion.SelectedItem.ToString
                End If
            Else
                .importedModelVersion = ""
            End If

        End With
    End Sub

    ''' <summary>
    ''' Create target program list based on the form properties.
    ''' </summary>
    ''' <param name="p_model">MC Model objec to set the properties on.</param>
    ''' <remarks></remarks>
    Private Sub SetChkBxsTargetProgram(ByRef p_model As cMCModel)
        With p_model
            .targetProgram.Clear()
            If useSAP2000 Then .targetProgram.Add(eCSiProgram.SAP2000)
            If useCSiBridge Then .targetProgram.Add(eCSiProgram.CSiBridge)
            If useETABS Then .targetProgram.Add(eCSiProgram.ETABS)
            If useSAFE Then .targetProgram.Add(eCSiProgram.SAFE)
        End With
    End Sub

    ''' <summary>
    ''' Performs validation of the model ID assigned when editing an existing model ID.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExampleIDIsValid() As Boolean
        If myMCModelTemp.ID.idExample <= 0 OrElse
            _IDsReserved.Contains(myMCModelTemp.ID.idExample) Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), _PROMPT_ID_RESERVED))
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class
