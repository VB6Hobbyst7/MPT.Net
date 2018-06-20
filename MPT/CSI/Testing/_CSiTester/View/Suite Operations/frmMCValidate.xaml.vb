Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Threading

Imports CSiTester.cSettings
Imports CSiTester.cMCValidator

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary

Public Class frmMCValidate
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Prompts"
    Private Const PROMPT_EXAMPLE_DIRECTORY_BROWSE As String = "Select Example Directory"
#End Region

#Region "Fields"
    ''' <summary>
    ''' The actions that the current initialization of the form is intended to do.
    ''' </summary>
    ''' <remarks></remarks>
    Private _formAction As eSchemaValidate
    ''' <summary>
    ''' The path sent to the form during initialization, which might be an empty string.
    ''' </summary>
    ''' <remarks></remarks>
    Private _customPath As String
    ''' <summary>
    ''' The list of paths sent to the form during initialization.
    ''' </summary>
    ''' <remarks></remarks>
    Private _customPaths As New List(Of String)
    ''' <summary>
    ''' Tracks whether or not a validation was run in order to determine if file cleanup is needed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _cleanResultsFiles As Boolean

    Private _validator As cMCValidator
#End Region

#Region "Properties"
    Private _pathDirectory As String
    ''' <summary>
    ''' Folder that contains the model control XML files to be run through the example schema validation test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathDirectory As String
        Set(ByVal value As String)
            If Not _pathDirectory = value Then
                _pathDirectory = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("pathDirectory"))
            End If
        End Set
        Get
            Return _pathDirectory
        End Get
    End Property

    ''' <summary>
    ''' Collection of the validation results for each example validated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property validationResults As New ObservableCollection(Of cMCValidator)

#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()
        InitializeFormControls()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_formAction">The actions that the current initialization of the form is intended to do.</param>
    ''' <param name="p_pathDirectory">Path to the directory where the examples to be validated are located.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_formAction As eSchemaValidate,
                   Optional ByVal p_pathDirectory As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(p_pathDirectory, p_formAction)
        InitializeFormControls()

        AutoRunValidation()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_formAction">The actions that the current initialization of the form is intended to do.</param>
    ''' <param name="p_pathDirectories">Paths to the directories for each example that is to be validated.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_formAction As eSchemaValidate,
                   ByVal p_pathDirectories As List(Of String))

        ' This call is required by the p_pathDirectories.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData(p_pathDirectories, p_formAction)
        InitializeFormControls()

        AutoRunValidations()
    End Sub

    Private Sub InitializeData(ByVal p_pathDirectories As List(Of String),
                               Optional ByVal p_formAction As eSchemaValidate = eSchemaValidate.RunSchemaValidationCustom)
        InitializeData(p_pathDirectories(0), p_formAction)
        _customPaths = p_pathDirectories
    End Sub

    Private Sub InitializeData(Optional ByVal p_pathDirectory As String = "",
                               Optional ByVal p_formAction As eSchemaValidate = eSchemaValidate.RunSchemaValidationCustom)
        _formAction = p_formAction
        _customPath = p_pathDirectory
        _validator = New cMCValidator(p_formAction, myRegTest)

        If Not String.IsNullOrEmpty(p_pathDirectory) Then
            pathDirectory = p_pathDirectory
        ElseIf p_formAction = eSchemaValidate.RunSchemaValidationCustom Then
            pathDirectory = testerSettings.exampleValidationFile.directory
        Else
            pathDirectory = myCsiTester.testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION
        End If

    End Sub

    Private Sub InitializeFormControls()
        Select Case _formAction
            Case eSchemaValidate.RunSchemaValidationCustom
            Case eSchemaValidate.RunSchemaValidationCustomAuto
                If AutoRunValid() Then
                    spFolderSource.Visibility = Windows.Visibility.Collapsed
                    btnValidateModelXMLs.Visibility = Windows.Visibility.Collapsed
                End If
            Case eSchemaValidate.RunSchemaValidation
                spFolderSource.Visibility = Windows.Visibility.Collapsed
                btnValidateModelXMLs.Visibility = Windows.Visibility.Collapsed

                If _validator.ValidateModels(pathDirectory, validationResults) Then SetDataGridView()
            Case eSchemaValidate.ViewSchemaValidation
                spFolderSource.Visibility = Windows.Visibility.Collapsed
                btnValidateModelXMLs.Visibility = Windows.Visibility.Collapsed

                'Create summary class collections & display in datagrid
                validationResults = _validator.CreateSummaryClassCollections(_validator.SchemaValidationResultsLocation())
                SetDataGridView()
        End Select
    End Sub

    ''' <summary>
    ''' Automatically runs the validation if the action is so specified and the target directory exists.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AutoRunValidation()

        If AutoRunValid() Then RunValidation()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AutoRunValidations()
        If AutoRunsValid() Then RunValidations()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AutoRunsValid() As Boolean
        For Each path As String In _customPaths
            If Not AutoRunValid(path) Then Return False
        Next
        Return True
    End Function

    ''' <summary>
    ''' Returns 'True' if the conditions for automatically running the validation are met.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AutoRunValid(Optional p_path As String = "") As Boolean
        If String.IsNullOrWhiteSpace(p_path) Then p_path = _customPath

        Return (_formAction = eSchemaValidate.RunSchemaValidationCustomAuto AndAlso
                IO.Directory.Exists(p_path))
    End Function
#End Region

#Region "Form Controls"
    'Buttons
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        CloseEvent()

        Me.Close()
    End Sub

    Private Sub btnFolderSource_Click(sender As Object, e As RoutedEventArgs) Handles btnFolderSource.Click
        Dim dirPathStart As String

        If Not String.IsNullOrWhiteSpace(txtBxFolderSource.Text) Then
            dirPathStart = txtBxFolderSource.Text
        Else
            dirPathStart = pathStartup()
        End If

        pathDirectory = BrowseForFolder(PROMPT_EXAMPLE_DIRECTORY_BROWSE, dirPathStart)
    End Sub

    Private Sub btnValidateModelXMLs_Click(sender As Object, e As RoutedEventArgs) Handles btnValidateModelXMLs.Click
        RunValidation()
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
        If _cleanResultsFiles Then
            CloseEvent()
        End If
    End Sub

    '=== Dynamically sets the maximum height of the datagrid so that scrollbars appear if not all rows are visible
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        Dim maxHeight As Double

        maxHeight = rowDG.ActualHeight - dataGrid_Results.Margin.Bottom - gridMain.Margin.Bottom

        If maxHeight > dataGrid_Results.Margin.Bottom + gridMain.Margin.Bottom Then dataGrid_Results.MaxHeight = maxHeight
    End Sub

    ''' <summary>
    ''' Sets the datagrid view to display results from an example schema validation test. Adjusts the form to fit the new datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataGridView()
        Me.SizeToContent = Windows.SizeToContent.Manual
        Me.Height = 500
        Me.Width = MinWidth
        Me.MinHeight = rowControls.ActualHeight + 200
        dataGrid_Results.ItemsSource = validationResults
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunValidations()
        If _validator.ValidateModels(_customPaths, validationResults) Then
            SetDataGridView()
            _cleanResultsFiles = True
        End If
    End Sub

    ''' <summary>
    ''' Performs the operations of calling regTest to run the validation, and then reading and displaying the results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunValidation()
        If _validator.ValidateModels(pathDirectory, validationResults) Then
            SetDataGridView()
            _cleanResultsFiles = True
        End If
    End Sub

    ''' <summary>
    ''' Performs the closing action of the form, including validation and saving of files, depending on the OK/Close status. 
    ''' If it returns 'True', then the form will be closed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseEvent()
        'Delete results file if run as a custom validation
        If ((_formAction = eSchemaValidate.RunSchemaValidationCustom OrElse
             _formAction = eSchemaValidate.RunSchemaValidationCustomAuto) AndAlso
            _cleanResultsFiles) Then
            DeleteFile(_validator.SchemaValidationResultsLocation())

            _cleanResultsFiles = False
        End If
    End Sub
#End Region

End Class
