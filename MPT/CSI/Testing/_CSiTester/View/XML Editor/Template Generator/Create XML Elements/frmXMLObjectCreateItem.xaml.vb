Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cFileAttachment
Imports CSiTester.cMCModelID

Public Class frmXMLObjectCreateItem
    Implements INotifyPropertyChanged
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const TITLE_VALIDATION_FILE As String = "Invalid File Location"
    Private Const TITLE_VALIDATION_DESTINATION As String = "Invalid Destination"
    Private Const PROMPT_VALIDATION_DESTINATION As String = "Destination must lie within a subdirectory of the folder containing the model control XMl file." & vbNewLine & vbNewLine &
                                        "Please select a valid directory."
    Private Const PROMPT_BROWSE_VALIDATION_DESTINATION As String = "Select Destination for File"
    Private Const PROMPT_VALIDATION_FILE As String = "File must lie within a subdirectory of the folder containing the model control XMl file." & vbNewLine & vbNewLine &
                                        "Please select a valid source or copy the file to a valid destination."


    Private Const PROMPT_OBJECT_CREATED As String = "Object successfully added to selected XML files."

    'TODO: Make use of a more global enum?
    Private Const fTypeLblImage As String = "Image File"
    Private Const fTypeLblAttachment As String = "Attachment File"
    Private Const fTypeLblSupportingFile As String = "Supporting File"
    Private Const fTypeLblDocumentation As String = "Documentation File"
    Private Const fTypeLblExcelResult As String = "Excel Result File"
#End Region

#Region "Variables"
    Private _nodesChanged As Boolean
    Private _cleanUpForm As Boolean
#End Region

#Region "Properties"
    Friend Property frmXMLObjectType As eXMLObjectType

    Public Property yearValue As Integer
    Public Property monthValue As Integer
    Public Property dayValue As Integer

    Private _addExcelResultToMCFile As Boolean
    ''' <summary>
    ''' Model control file will be populated with the results from the assigned Excel results file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addExcelResultToMCFile As Boolean
        Set(ByVal value As Boolean)
            If Not _addExcelResultToMCFile = value Then
                _addExcelResultToMCFile = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("addExcelResultToMCFile"))
            End If
        End Set
        Get
            Return _addExcelResultToMCFile
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

    Private _txtField1Value As String
    Public Property txtField1Value As String
        Set(ByVal value As String)
            If Not _txtField1Value = value Then
                _txtField1Value = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("txtField1Value"))
            End If
        End Set
        Get
            Return _txtField1Value
        End Get
    End Property

    Private _txtField2Value As String
    Public Property txtField2Value As String
        Set(ByVal value As String)
            If Not _txtField2Value = value Then
                _txtField2Value = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("txtField2Value"))
            End If
        End Set
        Get
            Return _txtField2Value
        End Get
    End Property

    Private _fileName As String
    Public Property fileName As String
        Set(ByVal value As String)
            If Not _fileName = value Then
                _fileName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileName"))
            End If
        End Set
        Get
            Return _fileName
        End Get
    End Property

    Private _fileExtension As String
    Public Property fileExtension As String
        Set(ByVal value As String)
            If Not _fileExtension = value Then
                _fileExtension = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileExtension"))
            End If
        End Set
        Get
            Return _fileExtension
        End Get
    End Property

    Private _fileSource As String
    Public Property fileSource As String
        Set(ByVal value As String)
            If Not _fileSource = value Then
                _fileSource = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileSource"))
            End If
        End Set
        Get
            Return _fileSource
        End Get
    End Property

    Private _fileDestination As String
    Public Property fileDestination As String
        Set(ByVal value As String)
            If Not _fileDestination = value Then
                _fileDestination = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileDestination"))
            End If
        End Set
        Get
            Return _fileDestination
        End Get
    End Property

    Private _fileSameAsModel As Boolean
    Public Property fileSameAsModel As Boolean
        Set(ByVal value As Boolean)
            If Not _fileSameAsModel = value Then
                _fileSameAsModel = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileSameAsModel"))
            End If
        End Set
        Get
            Return _fileSameAsModel
        End Get
    End Property

    Private _copySourceToDestination As Boolean
    Public Property copySourceToDestination As Boolean
        Set(ByVal value As Boolean)
            If Not _copySourceToDestination = value Then
                _copySourceToDestination = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("copySourceToDestination"))
            End If
        End Set
        Get
            Return _copySourceToDestination
        End Get
    End Property

    Private _destinationIsDefault As Boolean
    Public Property destinationIsDefault As Boolean
        Set(ByVal value As Boolean)
            If Not _destinationIsDefault = value Then
                _destinationIsDefault = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("destinationIsSource"))
            End If
        End Set
        Get
            Return _destinationIsDefault
        End Get
    End Property

    Private _txtNumberValue As Integer
    Public Property txtNumberValue As Integer
        Set(ByVal value As Integer)
            If Not _txtNumberValue = value Then
                _txtNumberValue = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("txtNumberValue"))
            End If
        End Set
        Get
            Return _txtNumberValue
        End Get
    End Property

    Private _comment As String
    Public Property comment As String
        Set(ByVal value As String)
            If Not _comment = value Then
                _comment = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("comment"))
            End If
        End Set
        Get
            Return _comment
        End Get
    End Property

    Private _myMCModel As cMCModel
    ''' <summary>
    ''' Class representing the model control XML file to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModel As cMCModel
        Set(ByVal value As cMCModel)
            _myMCModel = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myMCModel"))
        End Set
        Get
            Return _myMCModel
        End Get
    End Property

    ''' <summary>
    ''' Temporary storage property for the base example class that might be updated from the form input.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModelSave As cMCModel

    Public Property formCanceled As Boolean
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _cleanUpForm = True
    End Sub

    ''' <summary>
    ''' Initialization of the main form for generating various objects for the model control XML files.
    ''' </summary>
    ''' <param name="p_xmlObjectType">Classification of the XML node object found in model xml control files. Determines appearance and function of the form.</param>
    ''' <param name="p_baseMCModel">Optional: Example in memory to add the values to, ultimately generating a new XML file. If not provided, values are directly added to the selected XML files.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_xmlObjectType As eXMLObjectType,
                   Optional ByRef p_baseMCModel As cMCModel = Nothing,
                   Optional ByVal p_applyToGroup As Boolean = False)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        frmXMLObjectType = p_xmlObjectType

        InitializeData(p_baseMCModel, p_applyToGroup)
        InitializeControls()
        InitializeDefaults()
    End Sub

    Private Sub InitializeControls()
        gridDate.Height = New GridLength(0)
        gridTtBx1.Height = New GridLength(0)
        gridChkBx1.Height = New GridLength(0)
        gridTtBx2.Height = New GridLength(0)
        gridTtBxFileName.Height = New GridLength(0)
        gridTtBxCopy.Height = New GridLength(0)
        gridTtBxNumber.Height = New GridLength(0)
        gridComment.Height = New GridLength(0)

        If Not myMCModel.ID.multiModelType = eMultiModelType.singleModel Then
            gridChkBx1.Height = New GridLength(1, GridUnitType.Star)
            chkBxAddExcelResultToMCFile.Visibility = Windows.Visibility.Collapsed
            chkBxApplyToGroup.Visibility = Windows.Visibility.Visible
        End If

        txtBxFileExtension.IsEnabled = False
        txtBxFileDestination.IsEnabled = False
        chkBxDefaultDestination.IsEnabled = False
        btnFileDestination.IsEnabled = False
        btnAdd.IsEnabled = False
        InitializeBtnRemove(False)

        txtBxFileSource.ToolTip = fileSource
        txtBxFileDestination.ToolTip = fileDestination

        Select Case frmXMLObjectType
            Case eXMLObjectType.Ticket
                InitializeTicket()
            Case eXMLObjectType.Incident
                InitializeIncident()
            Case eXMLObjectType.Link
                InitializeLink()
            Case eXMLObjectType.Image
                InitializeImage()
            Case eXMLObjectType.Attachment
                InitializeAttachment()
            Case eXMLObjectType.SupportingFile
                InitializeSupportingFile()
            Case eXMLObjectType.Documentation
                InitializeDocumentation()
            Case eXMLObjectType.ExcelResult
                InitializeExcelResult()
            Case eXMLObjectType.Update
                InitializeUpdate()
        End Select
    End Sub
    Private Sub InitializeTicket()
        'Form Title & Labels
        Me.Title = "Add Ticket"
        btnAdd.Content = "Add Ticket"
        btnAdd.Width = Double.NaN
        lblNumber.Content = "Ticket Number"

        'Show/Hide Grid Regions
        gridTtBxNumber.Height = New GridLength(1, GridUnitType.Star)
    End Sub
    Private Sub InitializeIncident()
        'Form Title & Labels
        Me.Title = "Add Incident"
        btnAdd.Content = "Add Incident"
        btnAdd.Width = Double.NaN
        lblNumber.Content = "Incident Number"

        'Show/Hide Grid Regions
        gridTtBxNumber.Height = New GridLength(1, GridUnitType.Star)
    End Sub
    Private Sub InitializeLink()
        'Form Title & Labels
        Me.Title = "Add Link"
        btnAdd.Content = "Add Link"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Link Title"
        lblText2.Content = "Link URL"
        lblText2.ToolTip = "Link web address"

        'Show/Hide Grid Regions
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBx2.Height = New GridLength(1, GridUnitType.Star)
    End Sub
    Private Sub InitializeImage()
        'Form Title & Labels
        Me.Title = "Add Image"
        btnAdd.Content = "Add Image"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Image Title"

        'Show/Hide Grid Regions
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBxCopy.Height = New GridLength(211, GridUnitType.Pixel)
    End Sub
    Private Sub InitializeAttachment()
        'Form Title & Labels
        Me.Title = "Add Attachment"
        btnAdd.Content = "Add Attachment"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Attachment Title"

        'Show/Hide Grid Regions
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBxCopy.Height = New GridLength(211, GridUnitType.Pixel)
    End Sub
    Private Sub InitializeSupportingFile()
        'Form Title & Labels
        Me.Title = "Add Supporting File"
        btnAdd.Content = "Add Supporting File"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Supporting File Title"

        'Show/Hide Grid Regions
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBxCopy.Height = New GridLength(211, GridUnitType.Pixel)
    End Sub
    Private Sub InitializeDocumentation()
        'Form Title & Labels
        Me.Title = "Add Documentation File"
        btnAdd.Content = "Add Documenation File"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Documentation File Title"

        'Show/Hide Grid Regions
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBxCopy.Height = New GridLength(211, GridUnitType.Pixel)
    End Sub
    Private Sub InitializeExcelResult()
        'Form Title & Labels
        Me.Title = "Add Excel Result File"

        btnClose.Width = 100

        'Show/Hide Grid Regions
        gridTtBxCopy.Height = New GridLength(211, GridUnitType.Pixel)
        chkBxAddExcelResultToMCFile.Visibility = Windows.Visibility.Visible

        If myMCModel.resultsExcel.Count > 0 Then
            InitializeBtnRemove(True)
            btnAdd.Content = "Change Excel File"
            btnAdd.Width = Double.NaN
            fileSource = myMCModel.resultsExcel.filePath
        Else
            InitializeBtnRemove(False)
            btnAdd.Content = "Add Excel File"
            btnAdd.Width = Double.NaN
            myMCModel.resultsExcel.Add(New cFileExcelResult())
        End If

        addExcelResultToMCFile = myMCModel.updateResultsFromExcel
    End Sub
    Private Sub InitializeUpdate()
        'Form Title & Labels
        Me.Title = "Add Update"
        btnAdd.Content = "Add Update"
        btnAdd.Width = Double.NaN
        lblText1.Content = "Person"
        txtBxText1.Text = testerSettings.userName
        lblNumber.Content = "Ticket"

        'Show/Hide Grid Regions
        gridDate.Height = New GridLength(1, GridUnitType.Star)
        gridTtBx1.Height = New GridLength(1, GridUnitType.Star)
        gridTtBxNumber.Height = New GridLength(1, GridUnitType.Star)
        gridComment.Height = New GridLength(1, GridUnitType.Star)

        InitializeDateComboBoxes()
    End Sub

    Private Sub InitializeDateComboBoxes()

        SetDateComboBoxes(cmbBxYear, cmbBxMonth, cmbBxDay, True)

    End Sub

    ''' <summary>
    ''' Sets the proper initialization for the 'remove' button based on the parameter passed.
    ''' </summary>
    ''' <param name="p_usable"></param>
    ''' <remarks></remarks>
    Private Sub InitializeBtnRemove(ByVal p_usable As Boolean)
        If p_usable Then
            btnRemove.Width = 100
            btnRemove.IsEnabled = True
            btnRemove.Visibility = Windows.Visibility.Visible
        Else
            btnRemove.IsEnabled = False
            btnRemove.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub

    Private Sub InitializeData(ByRef p_baseMCModel As cMCModel,
                               ByVal p_applyToGroup As Boolean)
        _cleanUpForm = True
        _nodesChanged = False
        formCanceled = False

        If p_baseMCModel IsNot Nothing Then
            myMCModel = CType(p_baseMCModel.Clone, cMCModel)
            myMCModelSave = p_baseMCModel
            applyToGroup = p_applyToGroup
        Else
            myMCModel = New cMCModel()
        End If
    End Sub

    Private Sub InitializeDefaults()
        chkBxDefaultDestination.IsChecked = True
        copySourceToDestination = True
        If Not myMCModel.ID.multiModelType = eMultiModelType.singleModel Then
            chkBxApplyToGroup.IsChecked = applyToGroup
        Else
            chkBxApplyToGroup.IsChecked = False
        End If
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    Private Sub btnAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnAdd.Click
        'Set properties for operation
        Select Case frmXMLObjectType
            Case eXMLObjectType.Ticket
                AddTicket()
            Case eXMLObjectType.Incident
                AddIncident()
            Case eXMLObjectType.Link
                AddLink()
            Case eXMLObjectType.Update
                AddUpdate()
            Case eXMLObjectType.Attachment, eXMLObjectType.SupportingFile, eXMLObjectType.Documentation
                AddAttachment()
            Case eXMLObjectType.Image
                AddImage()
            Case eXMLObjectType.ExcelResult
                AddExcelResult()
        End Select

        CreateItem()

        CloseEvent()
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnRemove.Click
        Select Case frmXMLObjectType
            Case eXMLObjectType.ExcelResult
                myMCModel.resultsExcel.Clear()
        End Select

        CleanUpForm()
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        CleanUpForm()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Performs the closing action of the form, including validation and saving of files, depending on the OK/Close status. If it returns 'True', then the form will be closed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseEvent()
        _cleanUpForm = False

        Me.Close()
    End Sub

    Private Sub btnFileSource_Click(sender As Object, e As RoutedEventArgs) Handles btnFileSource.Click
        Dim xmlDir As String = myMCModel.modelFile.pathSource.directory

        Select Case frmXMLObjectType
            Case eXMLObjectType.Image : BrowseForFile(fileSource, xmlDir, fTypeLblImage)
            Case eXMLObjectType.Attachment : BrowseForFile(fileSource, xmlDir, fTypeLblAttachment)
            Case eXMLObjectType.SupportingFile : BrowseForFile(fileSource, xmlDir, fTypeLblSupportingFile)
            Case eXMLObjectType.Documentation : BrowseForFile(fileSource, xmlDir, fTypeLblDocumentation)
            Case eXMLObjectType.ExcelResult : BrowseForFile(fileSource, xmlDir, fTypeLblExcelResult)
        End Select

    End Sub

    Private Sub btnFileDestination_Click(sender As Object, e As RoutedEventArgs) Handles btnFileDestination.Click
        Dim xmlDir As String = myMCModel.mcFile.pathDestination.directory

        fileDestination = SelectFileDestination(xmlDir)

        'Validation that enforces location to be within the Model Control XML directory
        While Not StringExistInName(fileDestination, xmlDir)
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                            PROMPT_VALIDATION_DESTINATION,
                                            TITLE_VALIDATION_DESTINATION)
                Case eMessageActions.OK : fileDestination = SelectFileDestination(xmlDir)
                Case eMessageActions.Cancel : SetDefaultDestination()
            End Select
        End While
    End Sub

    '=== Combo Boxes
    Private Sub cmbBxYear_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxYear.SelectionChanged
        yearValue = myCInt(cmbBxYear.SelectedItem.ToString)
    End Sub
    Private Sub cmbBxMonth_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxMonth.SelectionChanged
        monthValue = myCInt(cmbBxMonth.SelectedItem.ToString)

        UpdateDayComboBox(myCInt(cmbBxMonth.SelectedItem.ToString), cmbBxDay)

    End Sub
    Private Sub cmbBxDay_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxDay.SelectionChanged
        dayValue = myCInt(cmbBxDay.SelectedItem.ToString)
    End Sub


    '=== Check Boxes
    Private Sub chkBxCopySource_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxCopySource.Checked
        If Not frmXMLObjectType = eXMLObjectType.SupportingFile Then chkBxDefaultDestination.IsEnabled = True
    End Sub
    Private Sub chkBxCopySource_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxCopySource.Unchecked
        chkBxDefaultDestination.IsChecked = True
        chkBxDefaultDestination.IsEnabled = False
    End Sub

    Private Sub chkBxDefaultDestination_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxDefaultDestination.Checked
        txtBxFileDestination.IsEnabled = False
        btnFileDestination.IsEnabled = False
        destinationIsDefault = True
        SetDefaultDestination()
    End Sub
    Private Sub chkBxDefaultDestination_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxDefaultDestination.Unchecked
        txtBxFileDestination.IsEnabled = True
        btnFileDestination.IsEnabled = True
        destinationIsDefault = False
    End Sub


    '=== Textboxes (Validation for Form Completion)
    Private Sub txtBxText1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxText1.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub
    Private Sub txtBxText2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxText2.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub
    Private Sub txtBxFileSource_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxFileSource.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub
    Private Sub txtBxFileDestination_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxFileDestination.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub
    Private Sub txtBxNumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxNumber.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub
    Private Sub txtFieldComment_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtFieldComment.TextChanged
        If btnAdd IsNot Nothing Then
            If ObjectValidated() Then
                btnAdd.IsEnabled = True
            Else
                btnAdd.IsEnabled = False
            End If
        End If
    End Sub

    'TODO: Remove?
    ''' <summary>
    ''' Checks if the filename typed contains an extension. If so, the extension is automatically filled in the appropriate property
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtBxFilename_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxFilename.TextChanged
        If NameIncludesFileExtension(txtBxFilename.Text, 3) Then
            fileExtension = GetSuffix(txtBxFilename.Text, ".")
        ElseIf Not String.IsNullOrEmpty(txtBxFileExtension.Text) Then
            fileExtension = GetSuffix(txtBxFileExtension.Text, ".")
        End If
    End Sub

    Private Sub chkBxFileNameAsModel_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxFileNameAsModel.Checked
        txtBxFilename.IsEnabled = False
        txtBxFileExtension.IsEnabled = True

        fileName = ""
    End Sub
    Private Sub chkBxFileNameAsModel_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxFileNameAsModel.Unchecked
        txtBxFilename.IsEnabled = True
        txtBxFileExtension.IsEnabled = False
    End Sub
#End Region

#Region "Form Behavior"

    ''' <summary>
    ''' All actions to be done whenever the form is closed occur here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        If _cleanUpForm Then
            CleanUpForm()
        End If
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Check if XML files were changed. If so, the editor is refreshed to show the changes.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanUpForm()
        If _nodesChanged Then
            windowXMLEditorBulk.ChangeXMLSource()
        Else
            formCanceled = True
        End If
        _cleanUpForm = False

        CloseEvent()
    End Sub

    ''' <summary>
    ''' Sets the default destination path for an image or attachment file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultDestination()
        fileDestination = myMCModel.mcFile.pathDestination.directory & "\"

        Select Case frmXMLObjectType
            Case eXMLObjectType.Attachment : fileDestination = fileDestination & DIR_NAME_ATTACHMENTS_DEFAULT
            Case eXMLObjectType.Documentation : fileDestination = fileDestination & DIR_NAME_ATTACHMENTS_DEFAULT
            Case eXMLObjectType.SupportingFile
                If myMCModel.folderStructure = cMCModel.eFolderStructure.Database Then
                    fileDestination = fileDestination & DIR_NAME_MODELS_DEFAULT
                End If
            Case eXMLObjectType.Image : fileDestination = fileDestination & DIR_NAME_FIGURES_DEFAULT
            Case eXMLObjectType.ExcelResult
                If myMCModel.folderStructure = cMCModel.eFolderStructure.Database Then
                    fileDestination = fileDestination & DIR_NAME_MODELS_DEFAULT
                End If
            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' Returns true or false, depending on if the necessary required and valid information has been entered into the form.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObjectValidated() As Boolean
        ObjectValidated = False

        Select Case frmXMLObjectType
            Case eXMLObjectType.Attachment, eXMLObjectType.SupportingFile, eXMLObjectType.Documentation, eXMLObjectType.Image
                Dim xmlDir As String = myMCModel.mcFile.pathDestination.directory
                If Not String.IsNullOrEmpty(txtBxText1.Text) Then
                    If IO.File.Exists(txtBxFileSource.Text) Then
                        If Not String.IsNullOrEmpty(txtBxFileDestination.Text) Then
                            If StringExistInName(fileDestination, xmlDir) Then Return True
                        End If
                    End If
                End If
            Case eXMLObjectType.ExcelResult
                Dim xmlDir As String = myMCModel.mcFile.pathDestination.directory
                If IO.File.Exists(txtBxFileSource.Text) Then
                    If Not String.IsNullOrEmpty(txtBxFileDestination.Text) Then
                        If StringExistInName(fileDestination, xmlDir) Then Return True
                    End If
                End If
            Case eXMLObjectType.Incident, eXMLObjectType.Ticket : If Not txtBxNumber.Text = "0" Then Return True
            Case eXMLObjectType.Link
                If Not String.IsNullOrEmpty(txtBxText1.Text) Then
                    If Not String.IsNullOrEmpty(txtBxText2.Text) Then Return True
                End If
            Case eXMLObjectType.Update
                If Not String.IsNullOrEmpty(txtBxText1.Text) Then
                    'If Not String.IsNullOrEmpty(txtBxText2.Text) Then
                    If Not String.IsNullOrEmpty(txtFieldComment.Text) Then Return True
                    'End If
                End If
        End Select
    End Function

    Private Sub AddTicket()
        myMCModel.tickets.Add(txtNumberValue)
    End Sub
    Private Sub AddIncident()
        myMCModel.incidents.Add(txtNumberValue)
    End Sub
    Private Sub AddLink()
        Dim link As New cMCLink

        With link
            .title = txtField1Value
            .URL = txtField2Value
        End With

        myMCModel.links.Add(link)
    End Sub
    Private Sub AddUpdate()
        Dim update As New cMCUpdate

        With update
            .updateDate.numDay = dayValue
            .updateDate.numMonth = monthValue
            .updateDate.numYear = yearValue
            .person = txtField1Value
            .ticket = txtNumberValue
            .comment = comment
        End With

        'Set build if possible
        If (myMCModel.programControl IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(myMCModel.programControl.version)) Then
            update.build = myMCModel.programControl.version
        ElseIf Not String.IsNullOrEmpty(myRegTest.program_build) Then
            update.build = myRegTest.program_version & "." & myRegTest.program_build
        End If

        myMCModel.AddUpdate(update)
    End Sub
    Private Sub AddAttachment()
        If Not ValidFileTargetLocation() Then Exit Sub

        Dim attachment As New cFileAttachment(fileSource)
        attachment = DirectCast(SetCommonFileProperties(attachment), cFileAttachment)
        With attachment
            Select Case frmXMLObjectType
                Case eXMLObjectType.Attachment
                    .title = txtField1Value
                Case eXMLObjectType.Documentation
                    .title = TAG_ATTACHMENT_DOCUMENTATION & txtField1Value
                Case eXMLObjectType.SupportingFile
                    .title = TAG_ATTACHMENT_SUPPORTING_FILE & txtField1Value
            End Select
        End With

        myMCModel.attachments.Add(attachment)
    End Sub
    Private Sub AddImage()
        If Not ValidFileTargetLocation() Then Exit Sub

        Dim image As New cFileAttachment(fileSource)
        image.title = txtField1Value
        SetCommonFileProperties(image)

        myMCModel.images.Add(image)
    End Sub
    Private Sub AddExcelResult()
        If Not ValidFileTargetLocation() Then Exit Sub

        Dim resultExcel As New cFileExcelResult(fileSource)
        resultExcel = DirectCast(SetCommonFileProperties(resultExcel), cFileExcelResult)

        With myMCModel
            .AddResultExcel(resultExcel, p_replaceExisting:=True)
            .updateResultsFromExcel = addExcelResultToMCFile

            If .statusExample = testerSettings.statusTypes(5) Then                   'Add benchmark values
                .statusExample = testerSettings.statusTypes(4)                       'Done
            End If
        End With
    End Sub

    ''' <summary>
    ''' Sets the common file properties that are set in all form variations, such as file destination and copy actions.
    ''' </summary>
    ''' <param name="p_file">File object to set and return.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCommonFileProperties(ByVal p_file As cMCFile) As cMCFile
        p_file.SetDestination(fileDestination)

        If copySourceToDestination Then
            p_file.fileAction = cFileManager.eFileAction.copySourceToDestination
        Else
            p_file.fileAction = cFileManager.eFileAction.none
        End If

        Return p_file
    End Function

    ''' <summary>
    ''' Validation that enforces location to be within the Model Control XML directory.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidFileTargetLocation() As Boolean
        Dim xmlDir As String = myMCModel.mcFile.pathDestination.directory

        'Validation that enforces location to be within the Model Control XML directory
        If Not copySourceToDestination Then
            While Not StringExistInName(fileSource, xmlDir)
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                            PROMPT_VALIDATION_FILE,
                                            TITLE_VALIDATION_FILE)
                    Case eMessageActions.OK
                        Return False
                    Case eMessageActions.Cancel
                        CloseEvent()
                        Return False
                End Select
            End While
        End If

        Return True
    End Function

    ''' <summary>
    ''' Creates the item in the model control file in memory, or if it does not exist, then applies the operation through the editor class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateItem()
        If myMCModelSave IsNot Nothing Then       'Update XMl file in memory
            myMCModelSave = CType(myMCModel.Clone, cMCModel)
        Else                    'Edit existing selected XML files
            'Apply operation through editor class
            myXMLEditor.addObjectType = frmXMLObjectType

            If myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.ObjectAdd) Then
                _nodesChanged = True
                fileName = ""
                fileExtension = ""
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_OBJECT_CREATED))
            End If
        End If
    End Sub


    Private Function SelectFileDestination(ByVal p_xmlDirectory As String) As String
        Return BrowseForFolder(PROMPT_BROWSE_VALIDATION_DESTINATION, p_xmlDirectory, p_showNewFolderButton:=True)
    End Function
#End Region

End Class
