Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cPathSettings

Imports MPT.Enums.EnumLibrary
Imports MPT.Files.BatchLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Public Class frmGenerateTextFiles
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Prompts"
    Private Const _fTypeLblModel As String = "Model File"
    Private Const _fTypeLblModels As String = "Model Files"

    Private Const _TITLE_INVALID_PATH As String = "Invalid Path"
    Private Const _PROMPT_INVALID_PATH As String = "File does not exist in the path specified. The last valid path will be used."

    Private Const _TITLE_INVALID_PROGRAM As String = "Invalid Program"
    Private Const _PROMPT_INVALID_PROGRAM As String = "Invalid program selected. The last valid path will be used."

    Private Const _TITLE_BROWSE As String = "Select Destination for File"
#End Region

#Region "Variables"
    Private Const _FILE_CONVERT As String = "OpenSaveClose.bat"

    Private tempText As String
#End Region

#Region "Properties"

    ''' <summary>
    ''' Class that contains paths to all files and directories within a specified directory, as well as a list of paths filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePaths As cPathsExamples

    ''' <summary>
    ''' List of paths to all directories that contain the filtered files listed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsRelativeFiltered As ObservableCollection(Of String)

    Private _programPath As String
    ''' <summary>
    ''' Path to the CSi analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programPath As String
        Set(ByVal value As String)
            If Not _programPath = value Then
                _programPath = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programPath"))
            End If
        End Set
        Get
            Return _programPath
        End Get
    End Property

    Private _programName As String
    ''' <summary>
    ''' Name of the program selected. Must be a valid CSi analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programName As String
        Set(ByVal value As String)
            If Not _programName = value Then
                _programName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programName"))
            End If
        End Set
        Get
            Return _programName
        End Get
    End Property

    Private _programSourceOverride As Boolean
    ''' <summary>
    ''' If true, the user is able to manually specify or alter the program path specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programSourceOverride As Boolean
        Set(ByVal value As Boolean)
            If Not _programSourceOverride = value Then
                _programSourceOverride = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programSourceOverride"))
            End If
        End Set
        Get
            Return _programSourceOverride
        End Get
    End Property

    Private _folderSource As String
    ''' <summary>
    ''' Path to the folder containing files to be added as examples. Model control XML files are generated for these.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property folderSource As String
        Set(ByVal value As String)
            If Not _folderSource = value Then
                _folderSource = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("folderSource"))
            End If
        End Set
        Get
            Return _folderSource
        End Get
    End Property

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
                    filePaths.SetPathsFiltered(value)
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
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileExtensionOverride"))
            End If
        End Set
        Get
            Return _fileExtensionOverride
        End Get
    End Property

#End Region


#Region "Initialization"

    Friend Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()

        SetDefaults()

        InitializeControls()
    End Sub

    Private Sub InitializeData()
        filePaths = New cPathsExamples
        pathsRelativeFiltered = New ObservableCollection(Of String)

        UpdateDataGrid()
    End Sub

    Private Sub SetDefaults()
        folderSourceOverride = False
        fileExtensionOverride = False
        radBtnSelectFiles.IsChecked = True
    End Sub


    Private Sub InitializeControls()
        InitializeDateComboBoxes()

        'Set Defaults
        cmbBxFileExtension.IsEnabled = False
        txtBxProgramSource.IsEnabled = False
        txtBxFolderSource.IsEnabled = False
    End Sub

    Private Sub InitializeDateComboBoxes()
        '===Combo Boxes
        'Extensions
        cmbBxFileExtension.ItemsSource = testerSettings.GetFileTypes(testerSettings.programName)
        cmbBxFileExtension.SelectedIndex = 0
    End Sub
#End Region


#Region "Form Controls"
    Private Sub btnConvert_Click(sender As Object, e As RoutedEventArgs) Handles btnConvert.Click
        Dim batchCommand As String = ""
        Dim batchPath As String

        If programName = GetEnumDescription(eCSiProgram.SAFE) Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(),
                                                        "Currently {0} cannot generate text files through methods available to CSiTester.",
                                                        programName))
            Exit Sub
        End If

        batchPath = myCsiTester.testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & _FILE_CONVERT

        'Get the selected file paths
        filePaths.SetPathsSelected()

        For Each filepath As cPathExample In filePaths.pathsSelected
            batchCommand = batchCommand & """" & programPath & """ """ & filepath.path & """ " & testerSettings.commandSave & " " & testerSettings.commandClose & " " & testerSettings.commandBatchRun & Environment.NewLine
        Next

        If Not String.IsNullOrEmpty(batchCommand) Then
            'Write batch file
            WriteBatch(batchCommand, batchPath, True)

            'Run batch file
            RunBatch(batchPath, True, True)
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnProgramSource_Click(sender As Object, e As RoutedEventArgs) Handles btnProgramSource.Click
        Dim myList As New List(Of String)

        myCsiTester.BrowseProgram(, , , , myList)

        programPath = myList(0)
        programName = myList(1)

        'Set file extensions drop box to dynamically update for the selected program
        cmbBxFileExtension.ItemsSource = testerSettings.GetFileTypes(programName)
        cmbBxFileExtension.SelectedIndex = 0

        CheckRequiredDataFilled()
    End Sub

    Private Sub btnFileSources_Click(sender As Object, e As RoutedEventArgs) Handles btnFileSources.Click
        Dim filePathsTemp As New List(Of String)
        Dim fileExtensionTemp As String
        Dim fileExtensionsTemp As New List(Of String)
        Dim startDir As String

        'Check if file extension is valid. If so, use it. Otherwise, use no extension for the filter
        If Not GetSuffix(fileExtension, ".").Count = 3 Then
            fileExtensionTemp = ""
        Else
            fileExtensionTemp = fileExtension
        End If
        fileExtensionsTemp.Add(fileExtension)

        If Not String.IsNullOrEmpty(folderSource) Then
            startDir = folderSource
        Else
            startDir = pathStartup()
        End If


        'If BrowseForFilesWinForm(filePathsTemp, fileExtensionTemp, startDir) Then
        If BrowseForFiles(filePathsTemp, startDir, programName & " " & _fTypeLblModels, fileExtensionsTemp) Then
            'If file extension not set, set it to that of the first file selected
            If String.IsNullOrEmpty(fileExtensionTemp) Then fileExtension = GetSuffix(filePathsTemp(0), ".")

            'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
            filePaths = New cPathsExamples(filePathsTemp)

            'Generate a list of file paths of only the file extension type provided
            filePaths.SetPathsFiltered(fileExtension)
            pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

            UpdateDataGrid()
        End If

        'Add folder source of last set of selected files
        If filePaths.pathsFiltered.Count > 0 Then
            folderSource = filePaths.folderSource
        End If

        CheckRequiredDataFilled()
    End Sub
    Private Sub btnFolderSource_Click(sender As Object, e As RoutedEventArgs) Handles btnFolderSource.Click
        Dim filePathsTemp As New List(Of String)
        Dim dirPathStart As String

        If Not String.IsNullOrEmpty(txtBxFolderSource.Text) Then
            dirPathStart = txtBxFolderSource.Text
        Else
            dirPathStart = pathStartup()
        End If

        folderSource = BrowseForFolder(_TITLE_BROWSE, dirPathStart)

        'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
        If Not String.IsNullOrEmpty(folderSource) Then
            filePaths = New cPathsExamples(folderSource)

            'Generate a list of file paths of only the file extension type provided
            filePaths.SetPathsFiltered(fileExtension)
            pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

            UpdateDataGrid()
        End If

        CheckRequiredDataFilled()
    End Sub

    '=== Checkboxes
    Private Sub chkBxOverrideProgramSource_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideProgramSource.Checked
        txtBxProgramSource.IsEnabled = True
    End Sub
    Private Sub chkBxOverrideProgramSource_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxOverrideProgramSource.Unchecked
        txtBxProgramSource.IsEnabled = False
    End Sub

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

    '=== Radio Buttons
    Private Sub radBtnSelectFiles_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSelectFiles.Checked
        btnFileSources.IsEnabled = True
        btnFolderSource.IsEnabled = False
        chkBxOverrideFolderSource.IsChecked = False
    End Sub
    Private Sub radBtnSelectFolder_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnSelectFolder.Checked
        btnFileSources.IsEnabled = False
        btnFolderSource.IsEnabled = True
    End Sub

    '=== Combo Boxes
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

    '=== Text Boxes
    Private Sub txtBxProgramSource_GotFocus(sender As Object, e As RoutedEventArgs) Handles txtBxProgramSource.GotFocus
        tempText = txtBxProgramSource.Text
    End Sub
    Private Sub txtBxProgramSource_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxProgramSource.LostFocus
        If IO.File.Exists(programPath) Then
            If testerSettings.ValidProgram(programName) Then
                'Set file extensions drop box to dynamically update for the selected program
                cmbBxFileExtension.ItemsSource = testerSettings.GetFileTypes(programName)
                cmbBxFileExtension.SelectedIndex = 0
            Else
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                            _PROMPT_INVALID_PROGRAM,
                                                            _TITLE_INVALID_PROGRAM))
                txtBxProgramSource.Text = tempText
            End If
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_INVALID_PATH,
                                                        _TITLE_INVALID_PATH))
            txtBxProgramSource.Text = tempText
        End If

        CheckRequiredDataFilled()
    End Sub
    Private Sub txtBxFolderSource_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxFolderSource.LostFocus
        'Creates new file list. If dialog was cancelled, whatever earlier list existed will be maintained
        If Not String.IsNullOrEmpty(folderSource) Then
            If IO.Directory.Exists(folderSource) Then
                filePaths = New cPathsExamples(folderSource)

                'Generate a list of file paths of only the file extension type provided
                filePaths.SetPathsFiltered(fileExtension)
                pathsRelativeFiltered = filePaths.GetRelativePathsFiltered

                UpdateDataGrid()
            End If
        End If

        CheckRequiredDataFilled()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Updates the datagrid reference and adjusts column visibilities.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateDataGrid()
        Dim tempString As String = ""
        Dim tempList As New ObservableCollection(Of String)

        dgFilesList.ItemsSource = filePaths.pathsFiltered

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
    ''' Checks whether the minimum required information is present and performs operations based on that state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckRequiredDataFilled()
        If RequiredDataFilled() Then
            btnConvert.IsEnabled = True
        Else
            btnConvert.IsEnabled = False
        End If
    End Sub

    ''' <summary>
    ''' Checks whether the minimum required information has been specified for doing the text file generation process.
    ''' </summary>
    ''' <returns>True if all required data is filled. Otherwise, false</returns>
    ''' <remarks></remarks>
    Private Function RequiredDataFilled() As Boolean
        RequiredDataFilled = True

        Try
            If (String.IsNullOrEmpty(fileExtension) AndAlso fileExtensionOverride) Then Return False
            If filePaths.pathsFiltered.Count = 0 Then Return False
            If Not IO.File.Exists(programPath) Then Return False
            If Not testerSettings.ValidProgram(programName) Then Return False
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Function

#End Region


End Class
