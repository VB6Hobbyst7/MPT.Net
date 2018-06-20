Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Files.BatchLibrary
Imports MPT.Files.FileLibrary
Imports MPT.FileSystem
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.DataGridLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Reporting
Imports MPT.Time.TimeLibrary

Imports CSiTester.cLibrary

Imports CSiTester.cPathSettings
Imports CSiTester.cExample

'Notes
'1. Open Example PDF documentation within window. Resources below:
'   http://hugeonion.com/2009/04/06/displaying-a-pdf-file-within-a-wpf-application/
'   http://stackoverflow.com/questions/7675282/view-pdf-document-inside-the-same-wpf-windows

''' <summary>
''' This form displays detailed example information, such as the comparisons of benchmarks to calculated values and results for all specified output. 
''' Queries are displayed, as is time information. Buttons are available to display attachments in Explorer, Excel Files, and PDFs as applicable.
''' The form is also set up to navgiate to the next and preceding examples, based on the order in which the examples were first populated in the summary list.
''' </summary>
''' <remarks></remarks>
Public Class frmExample
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Prompts"
    Private Const promptMCSourceUpdateWarning As String = "Warning! The following model source files will be updated by this operation: "
    Private Const promptMCSourceUpdateQuestion As String = "Do you wish to continue?"
    Private Const titleMCSourceUpdate As String = "Updating Model Source"

    Private Const promptUpdateBMsSuccess As String = "Example benchmark values have been updated. To view updated values, close and reopen CSiTester."
    Private Const promptUpdateTimesSuccess As String = "Example assumed run time values have been updated. To view updated values, close and reopen CSiTester."
    Private Const titleUpdateExample As String = "Example Updated"

    Private Const _PROMPT_POSSIBLE_TRANSLATED_MODEL As String = "There are multiple model files for this example in the same directory. The model may have been translated into a newer version of "
    Private Const _PROMPT_POSSIBLE_TRANSLATED_MODEL_QUESTION As String = "Would you like to specify which model file to open?"
    Private Const _TITLE_POSSIBLE_TRANSLATED_MODEL As String = "Possible Translated Model File"

#End Region

#Region "Tooltips"
    Private Const ttTPreviousExample As String = "Previous Example"
    Private Const ttPreviousExample As String = "View previous example results in the set."
    Private Const ttTNextExample As String = "Next Example"
    Private Const ttNextExample As String = "View next example results in the set."

    Private Const ttTAttachment As String = "View Attachments"
    Private Const ttAttachment As String = "View various files included with the example."
    Private Const ttTAttachmentDocumentation As String = "Documentation"
    Private Const ttAttachmentDocumentation As String = "Opens a PDF of calculation documentation included with the example."
    Private Const ttTAttachmentGeneral As String = "General Attachments"
    Private Const ttAttachmentGeneral As String = "Opens a folder in Windows Explorer that contains attachments included with the example."
    Private Const ttTAttachmentLink As String = "Links"
    Private Const ttAttachmentLink As String = "Opens a list of internet links included with the example."
    Private Const ttTAttachmentExcel As String = "Excel Files"
    Private Const ttAttachmentExcel As String = "Opens an Excel file included with the example."

    Private Const ttTViewResults As String = "View Results"
    Private Const ttViewResults As String = "View various files related to the results of a run."
    Private Const ttTViewResultsExcel As String = "Export View to Excel"
    Private Const ttViewResultsExcel As String = "Exports the current example details to Excel."
    Private Const ttTViewResultsTables As String = "Open Exported Tables File"
    Private Const ttViewResultsTables As String = "Opens the MDB or XML tables file used to query the results."
    Private Const ttTViewResultsModel As String = "Open Run Model File"
    Private Const ttViewResultsModel As String = "Opens the run model file. If analysis files have not been deleted, the post-analysis state can be investigated."

    Private Const ttTEditExample As String = "Edit Example"
    Private Const ttEditExample As String = "Perform various editing operations to example data."
    Private Const ttTEditExampleMode As String = "Edit Result Items"
    Private Const ttEditExampleMode As String = "Manually edit the Output Parameter, Independent result, or Benchmark result on any particular result entry."
    Private Const ttTEditExampleUpdateBM As String = "Update Benchmarks"
    Private Const ttEditExampleUpdateBM As String = "Update all Benchmark result entries to the current Result Rounded"
    Private Const ttTEditExampleUpdateTime As String = "Update Times"
    Private Const ttEditExampleUpdateTime As String = "Update estimated run time to a specified multiple of the actual run time."
#End Region

#Region "Variables"
    Private cellText As String
    Private cellTextUndo As String
    Private cellBeingEdited As DataGridCellEditEndingEventArgs = Nothing
    Private outputParameterOriginal As List(Of String)
    Private independentOriginal As List(Of String)
    Private benchmarksOriginal As List(Of String)
    Private myEditMode As eExampleEditMode
    Private editBenchmark As Boolean
    Private editIndependent As Boolean
    Private editOutputParameter As Boolean
    Private subExampleWidth As Double
    Private OutputParameterWidth As Double
    Private IndependentValueWidth As Double
    Private BenchmarkValueWidth As Double
    Private PercentDifferentIndependentWidth As Double
    Private BenchmarkResultWidth As Double
    Private BenchmarkResultRoundedWidth As Double
    Private PercentDifferencBenchmarkWidth As Double
    Private TableQueryWidth As Double
#End Region

#Region "Properties"
    Private _examplesList As ObservableCollection(Of cExample)
    Friend Property examplesList As ObservableCollection(Of cExample)
        Set(ByVal value As ObservableCollection(Of cExample))
            _examplesList = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("examplesList"))
        End Set
        Get
            Return _examplesList
        End Get
    End Property
    Private _example As cExample
    Friend Property example As cExample
        Set(ByVal value As cExample)
            _example = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("example"))
        End Set
        Get
            Return _example
        End Get
    End Property

    Friend Property tabSelectedIndex As Integer
    Friend Property dgSummaryHeight As Double
#End Region

#Region "Initialization"

    ''' <summary>
    ''' Initializes a dummy example, filled with dummy values
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        example = New cExample  'Necessary for storing current example data for programmatic reference

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeControls()
        AssignValues()
        SetButtonTooltips()
    End Sub

    ''' <summary>
    ''' Initializes an examples form populated with data from an XML file
    ''' </summary>
    ''' <param name="myExample"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef myExample As cExample)
        'example = New cExample

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If myExample Is Nothing Then
            example = New cExample
        Else
            example = myExample
        End If
        InitializeControls()
        AssignValues()
        SetButtonTooltips()
    End Sub

    ''' <summary>
    ''' Sets up the initial display of the form, including the activation adn visibility of buttons.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeControls()
        outputParameterOriginal = New List(Of String)
        independentOriginal = New List(Of String)
        benchmarksOriginal = New List(Of String)

        myEditMode = eExampleEditMode.None

        'TODO: Maybe remove these later. Hiding for now.
        btnArrowLeft.Visibility = Windows.Visibility.Collapsed
        btnArrowRight.Visibility = Windows.Visibility.Collapsed

        menuItem_ExportExcel.IsEnabled = False
        spEditModeControls.Visibility = Windows.Visibility.Collapsed

        Me.labelExampleTitle.GetBindingExpression(Label.ContentProperty).UpdateTarget()

        'Adjust for release status
        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            updatedBenchmarkLabels.Visibility = Windows.Visibility.Collapsed
            menuItem_Attachments.Visibility = Windows.Visibility.Collapsed
            menuItem_Links.Visibility = Windows.Visibility.Collapsed
            menuItem_ExcelCalcs.Visibility = Windows.Visibility.Collapsed
            btnEditExample.Visibility = Windows.Visibility.Collapsed
            btnEditBM.Visibility = Windows.Visibility.Collapsed
            btnEditIndependent.Visibility = Windows.Visibility.Collapsed
            btnSaveEditBM.Visibility = Windows.Visibility.Collapsed
            DirectCast(FindResource("cellColorResult"), Style).Triggers.Clear()                 'Turn off cell coloring style
        Else
            'Adjust for presence of values
            If String.IsNullOrEmpty(example.linkAttachments) Then menuItem_Attachments.IsEnabled = False
            If example.linksLinks.Count = 0 Then menuItem_Links.IsEnabled = False
            If String.IsNullOrEmpty(example.linkExcel) Then menuItem_ExcelCalcs.IsEnabled = False
            If (Not example.runStatus = GetEnumDescription(eResultRun.completedRun) AndAlso Not example.runStatus = GetEnumDescription(eResultRun.manual)) Then menuItem_OpenTables.IsEnabled = False
            If Not IO.File.Exists(example.pathModelFile) Then menuItem_OpenModel.IsEnabled = False
            If Not example.comparedExample Then btnEditBM.IsEnabled = False
            btnSaveEditBM.IsEnabled = False
            If Not example.comparedExample Then menuItem_UpdateBenchmarks.IsEnabled = False
            If (Not ConvertTimesNumberMinute(example.timeRunActual) > 0) Then menuItem_UpdateTimes.IsEnabled = False
        End If

        'Adjust for presence of values
        If String.IsNullOrEmpty(example.linkDocumentation) Then menuItem_Documentation.IsEnabled = False

        'Set Datagrid Column Visibility
        If String.IsNullOrEmpty(example.itemList(0).subExample) Then
            subExample.Visibility = Windows.Visibility.Collapsed
        Else
            subExample.Visibility = Windows.Visibility.Visible
        End If

        'Assign styles to datagrid
        AssignStyles()
    End Sub

    ''' <summary>
    ''' Programmatically assigns values to individual labels and other form properties directly from the regTest class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignValues()
        'Title Elements
        If String.IsNullOrEmpty(example.numberCodeExample) Then
            Me.Title = "CSiTester - " & example.modelID
            'labelCodeExampleNumber.Content = example.modelID
        Else
            Me.Title = "CSiTester - " & example.numberCodeExample
            'labelCodeExampleNumber.Content = example.numberCodeExample
        End If

        'Datagrid
        BenchmarkValue.Header = GetEnumDescription(example.targetProgram) & " Benchmark"
        BenchmarkResultRounded.Header = GetEnumDescription(example.targetProgram) & " Result"


        'Below is being adjusted to be two-way binding. For now, times use default <!--Content="00:00:00"--> 

        labelExampleTitle.Content = example.titleExample
        labelBenchmarkVersion.Content = example.benchmarkLastVersion

        'Times
        LabelRunAssumed.Content = example.timeRunAssumed
        LabelRunActual.Content = example.timeRunActual
        LabelCheckAssumed.Content = example.timeCompareAssumed
        LabelCheckActual.Content = example.timeCompareActual
        LabelTotalAssumed.Content = example.timeCheckAssumed
        LabelTotalActual.Content = example.timeCheckActual

    End Sub

    ''' <summary>
    ''' Sets tooltips for buttons programmatically. 
    ''' </summary>
    ''' <remarks>Phase out later and use binding.</remarks>
    Private Sub SetButtonTooltips()
        'For some reason these look bad. Maybe because the button isn't in a ribbon?
        'btnArrowLeft.ToolTipTitle = ttTPreviousExample
        'btnArrowLeft.ToolTipDescription = ttPreviousExample
        'btnArrowRight.ToolTipTitle = ttTNextExample
        'btnArrowRight.ToolTipDescription = ttNextExample

        btnArrowLeft.ToolTip = ttPreviousExample
        btnArrowRight.ToolTip = ttNextExample

        btnArrowLeftBlue.ToolTip = ttPreviousExample
        btnArrowRightBlue.ToolTip = ttNextExample

        btnViewAttachments.ToolTipTitle = ttTAttachment
        menuItem_Documentation.ToolTipTitle = ttTAttachmentDocumentation
        menuItem_Attachments.ToolTipTitle = ttTAttachmentGeneral
        menuItem_Links.ToolTipTitle = ttTAttachmentLink
        menuItem_ExcelCalcs.ToolTipTitle = ttTAttachmentExcel

        btnViewMoreResults.ToolTipTitle = ttTViewResults
        menuItem_ExportExcel.ToolTipTitle = ttTViewResultsExcel
        menuItem_OpenTables.ToolTipTitle = ttTViewResultsTables
        menuItem_OpenModel.ToolTipTitle = ttTViewResultsModel

        btnEditExample.ToolTipTitle = ttTEditExample
        menuItem_EditMode.ToolTipTitle = ttTEditExampleMode
        menuItem_UpdateBenchmarks.ToolTipTitle = ttTEditExampleUpdateBM
        menuItem_UpdateTimes.ToolTipTitle = ttTEditExampleUpdateTime

        btnViewAttachments.ToolTipDescription = ttAttachment
        menuItem_Documentation.ToolTipDescription = ttAttachmentDocumentation
        menuItem_Attachments.ToolTipDescription = ttAttachmentGeneral
        menuItem_Links.ToolTipDescription = ttAttachmentLink
        menuItem_ExcelCalcs.ToolTipDescription = ttAttachmentExcel

        btnViewMoreResults.ToolTipDescription = ttViewResults
        menuItem_ExportExcel.ToolTipDescription = ttViewResultsExcel
        menuItem_OpenTables.ToolTipDescription = ttViewResultsTables
        menuItem_OpenModel.ToolTipDescription = ttViewResultsModel

        btnEditExample.ToolTipDescription = ttEditExample
        menuItem_EditMode.ToolTipDescription = ttEditExampleMode
        menuItem_UpdateBenchmarks.ToolTipDescription = ttEditExampleUpdateBM
        menuItem_UpdateTimes.ToolTipDescription = ttEditExampleUpdateTime
    End Sub

    ''' <summary>
    ''' Assigns styles for editing datagrid elements.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignStyles()
        BenchmarkValue.ElementStyle = CreateStyles(editBenchmark)
        IndependentValue.ElementStyle = CreateStyles(editIndependent)
        OutputParameter.ElementStyle = CreateStyles(editOutputParameter)
    End Sub

    ''' <summary>
    ''' Creates form styles for editing datagrid elements.
    ''' </summary>
    ''' <param name="editCol">If True, the editing style will be applied. If false, the non-editing style will be applied.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateStyles(ByVal editCol As Boolean) As Style
        Dim myStyle As New Style()
        Dim mysetterBackground As New Setter()
        Dim mysetterFontWeight As New Setter()
        Dim bc = New BrushConverter
        Try

            'Create setters
            If editCol Then
                With mysetterFontWeight
                    .Property = TextBlock.FontWeightProperty
                    .Value = FontWeights.Black
                End With
                With mysetterBackground
                    .Property = TextBlock.BackgroundProperty
                    .Value = bc.ConvertFrom("#FF5AFDF6")
                End With
            Else
                With mysetterFontWeight
                    .Property = TextBlock.FontWeightProperty
                    .Value = FontWeights.Normal
                End With
                With mysetterBackground
                    .Property = TextBlock.BackgroundProperty
                    .Value = Brushes.Transparent
                End With
            End If

            ''clear the triggers & form style
            With myStyle
                .Triggers.Clear()
                .Setters.Add(mysetterBackground)
                .Setters.Add(mysetterFontWeight)
                .TargetType = GetType(TextBlock)
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return myStyle
    End Function
#End Region

#Region "Form Controls"
    '=== Buttons
    ''' <summary>
    ''' Opens window to next example in the set, in decreasing index order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnArrowLeft_Click(sender As Object, e As RoutedEventArgs) Handles btnArrowLeft.Click, btnArrowLeftBlue.Click
        'Dim indexCurrent As Integer
        Dim indexNext As Integer = 0

        'Get the index for the previous example
        For Each nextExample As cExample In examplesTestSetList(tabSelectedIndex).examplesList
            If nextExample.modelID = example.modelID Then   'Current example index found
                indexNext = indexNext - 1                   'Set current index back by one
                Exit For
            End If
            indexNext += 1
        Next

        'If reaching the beginning of the list, set next index to loop to end of the list
        If indexNext < 0 Then indexNext = examplesTestSetList(tabSelectedIndex).examplesList.Count - 1

        Dim window As New frmExample(examplesTestSetList(tabSelectedIndex).examplesList(indexNext))
        window.DataContext = examplesTestSetList(tabSelectedIndex).examplesList(indexNext) 'Passes relevant binding information to sub-class
        window.tabSelectedIndex = tabSelectedIndex
        window.Show()
        window.Width = Me.Width
        window.Height = Me.Height
        Close()

    End Sub

    ''' <summary>
    ''' Opens window to next example in the set, in increasing index order
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnArrowRight_Click(sender As Object, e As RoutedEventArgs) Handles btnArrowRight.Click, btnArrowRightBlue.Click
        'Dim indexCurrent As Integer
        Dim indexNext As Integer

        'Get the index for the next example
        For Each nextExample As cExample In examplesTestSetList(tabSelectedIndex).examplesList
            If nextExample.modelID = example.modelID Then   'Current example index found
                indexNext = indexNext + 1                   'Set current index forward by one
                Exit For
            End If
            indexNext += 1
        Next

        'If reaching the end of the list, set next index to loop to beginning of the list
        If indexNext > examplesTestSetList(tabSelectedIndex).examplesList.Count - 1 Then indexNext = 0

        Dim window As New frmExample(examplesTestSetList(tabSelectedIndex).examplesList(indexNext))
        window.DataContext = examplesTestSetList(tabSelectedIndex).examplesList(indexNext) 'Passes relevant binding information to sub-class
        window.tabSelectedIndex = tabSelectedIndex
        window.Width = Me.Width
        window.Height = Me.Height
        ' window.WindowState = Windows.WindowState.Normal
        window.Show()
        Close()
    End Sub



    ''' <summary>
    ''' Opens PDF within the form, and unhides the splitter in order to allow the user to expand the sub-window.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_Documentation_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_Documentation.Click
        If IO.File.Exists(example.linkDocumentation) Then
            If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                OpenFile(example.linkDocumentation)
            Else
                DisplayDocumentationInForm()
            End If
        Else
            Dim msgBoxMessage As String = "Documentation PDF file does not exist at the correct relative location."

            If Not testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                msgBoxMessage = msgBoxMessage & Environment.NewLine & Environment.NewLine & " Please select a valid path to the relevant program in the main form."
            End If

            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), msgBoxMessage))

            If Not testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                ExceptionProgramSource(GetEnumDescription(testerSettings.programName), testerSettings.programPathStub)
                If IO.File.Exists(testerSettings.programPathStub) Then
                    DisplayDocumentationInForm()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Opens the PDF as a file in a separate viewer/program
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeparatePDFWindow_Click(sender As Object, e As RoutedEventArgs) Handles btnSeparatePDFWindow.Click
        OpenFile(example.linkDocumentation, "Documentation PDF file does not exist at the correct relative location." & Environment.NewLine & Environment.NewLine & " Please select a valid path to the relevant program in the main form.")
    End Sub

    ''' <summary>
    ''' Closes the PDF that is shown within the form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClosePDF_Click(sender As Object, e As RoutedEventArgs) Handles btnClosePDF.Click
        rowBrowser.Height = New GridLength(0)
        rowBrowserBtn.Height = New GridLength(0)
        Me.Splitter.Visibility = Windows.Visibility.Collapsed
        'dataGrid_Summary.Items.Refresh()
        'rowDG.Height = New GridLength(1, GridUnitType.Star)
        rowDG.Height = New GridLength(dgSummaryHeight)
        'dataGrid_Summary.Height = dgSummaryHeight
        'dataGrid_Summary.MaxHeight = dgSummaryHeight

        menuItem_Documentation.IsEnabled = True
    End Sub

    ''' <summary>
    ''' Opens the attachments folder within Microsoft Explorer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_Attachments_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_Attachments.Click
        OpenExplorerAtFolder(example.linkAttachments, "'Attachments' folder does not exist at the correct relative location.")
    End Sub

    ''' <summary>
    ''' Opens the database file from which the run results have been exported to. Right now this is only Access, but in the future could be an XML file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_OpenDBFile_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_OpenTables.Click
        OpenDBFile()
    End Sub

    ''' <summary>
    ''' Opens the model file of the current example in the selected program.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_OpenModelFile_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_OpenModel.Click
        Dim destinationModelPath As String = myCsiTester.ConvertPathModelSourceToDestination(example.pathModelFile, True)

        'Account for translated/imported model files, where the file name is changed
        If Not ValidateTranslatedModel(destinationModelPath) Then Exit Sub

        'Account for improper destination folder state
        If Not ValidateDestination(destinationModelPath) Then Exit Sub

        'Check that currently selected program is in the list of applicable programs for the example viewed.
        If Not ValidateProgram() Then Exit Sub

        'Check program release version
        If Not ValidateReleaseVersion() Then Exit Sub

        OpenModelFile(myRegTest.program_file.path, destinationModelPath)
    End Sub

    ''' <summary>
    ''' Makes the rounded results field editable.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditBM_Click(sender As Object, e As RoutedEventArgs) Handles btnEditBM.Click
        'Toggles edit mode & variable
        If myEditMode = eExampleEditMode.None Then
            myEditMode = eExampleEditMode.Benchmark
            EnableEditMode(myEditMode)
        Else
            DisableEditMode(myEditMode, True)
        End If
    End Sub

    ''' <summary>
    ''' Makes various other fields editable, such as independent results and the output parameter label.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEditIndependent_Click(sender As Object, e As RoutedEventArgs) Handles btnEditIndependent.Click
        'Toggles edit mode & variable
        If myEditMode = eExampleEditMode.None Then
            myEditMode = eExampleEditMode.IndependentValue
            EnableEditMode(myEditMode)
        Else
            DisableEditMode(myEditMode, True)
        End If
    End Sub

    Private Sub btnEditOutputParameter_Click(sender As Object, e As RoutedEventArgs) Handles btnEditOutputParameter.Click
        'Toggles edit mode & variable
        If myEditMode = eExampleEditMode.None Then
            myEditMode = eExampleEditMode.OutputParameter
            EnableEditMode(myEditMode)
        Else
            DisableEditMode(myEditMode, True)
        End If
    End Sub

    ''' <summary>
    ''' Saves any edits made to the rounded results field, and makes the field read only again.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveEditBM_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveEditBM.Click
        If myEditMode = eExampleEditMode.Benchmark Then
            UpdateBenchmarks(False)
        Else
            UpdateOther()
        End If

        DisableEditMode(myEditMode, False)  'Note: This must come after the update procedures as it changes the edit mode
    End Sub

    ''' <summary>
    ''' Opens the edit mode view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_EditMode_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_EditMode.Click
        rowEditButtons.Height = New GridLength(60)
        spEditModeControls.Visibility = Windows.Visibility.Visible
        dataGrid_Summary.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
        dataGrid_Summary.MaxHeight = dataGrid_Summary.MaxHeight - 60
    End Sub

    ''' <summary>
    ''' Closes the edit mode view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExitEditMode_Click(sender As Object, e As RoutedEventArgs) Handles btnExitEditMode.Click
        rowEditButtons.Height = New GridLength(0)
        spEditModeControls.Visibility = Windows.Visibility.Collapsed
        dataGrid_Summary.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
        dataGrid_Summary.MaxHeight = dataGrid_Summary.MaxHeight + 60
    End Sub

    ''' <summary>
    ''' Updates the benchmarks of the current example to the current results, both in the program view and in the model control XML files.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_UpdateBM_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_UpdateBenchmarks.Click
        UpdateBenchmarks(True)
    End Sub

    ''' <summary>
    ''' Updates the assumed run time of the current example to the actual run time of the results, multiplied by a specified factor to account for differences in computer speeds.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub menuItem_UpdateTimes_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_UpdateTimes.Click
        UpdateTimes()
    End Sub

    'TODO: Finish
    Private Sub menuItem_ExcelCalcs_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_ExcelCalcs.Click
        MsgBox("Opens Excel calculation file, if a calculation Excel file exists in 'attachments'.")
    End Sub
    Private Sub menuItem_ExportExcel_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_ExportExcel.Click
        MsgBox("Opens example in Excel by auto-generating a page from the displayed results.")
    End Sub
    Private Sub menuItem_Links_Click(sender As Object, e As RoutedEventArgs) Handles menuItem_Links.Click
        MsgBox("Opens clickable list of internet links.")
    End Sub
#End Region

#Region "Methods: Form Controls"
    '=== Buttons
    ''' <summary>
    ''' Makes the benchmark field editable.
    ''' </summary>
    ''' <param name="editMode">Edit mode types available within the Example details form for editing various datagrid fields.</param>
    ''' <remarks></remarks>
    Private Sub EnableEditMode(ByVal editMode As eExampleEditMode)
        Dim btnContent As String = "Undo Edit"

        btnSaveEditBM.IsEnabled = True

        Select Case editMode
            Case eExampleEditMode.Benchmark
                editBenchmark = True
                editIndependent = False
                editOutputParameter = False

                BenchmarkValue.IsReadOnly = False
                btnEditBM.Content = btnContent
                btnEditIndependent.IsEnabled = False
                btnEditOutputParameter.IsEnabled = False

                'Update row class properties & backup the original values
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.Benchmark
                    benchmarksOriginal.Add(exampleResult.benchmarkValue)
                Next
            Case eExampleEditMode.IndependentValue
                editBenchmark = False
                editIndependent = True
                editOutputParameter = False

                IndependentValue.IsReadOnly = False
                btnEditIndependent.Content = btnContent
                btnEditBM.IsEnabled = False
                btnEditOutputParameter.IsEnabled = False

                'Update row class properties & backup the original values
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.IndependentValue
                    independentOriginal.Add(exampleResult.independentValue)
                Next
            Case eExampleEditMode.OutputParameter
                editBenchmark = False
                editIndependent = False
                editOutputParameter = True

                OutputParameter.IsReadOnly = False
                btnEditOutputParameter.Content = btnContent
                btnEditBM.IsEnabled = False
                btnEditIndependent.IsEnabled = False

                'Update row class properties & backup the original values
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.OutputParameter
                    outputParameterOriginal.Add(exampleResult.outputParameter)
                Next
        End Select

        AssignStyles()
    End Sub

    ''' <summary>
    ''' Disables the edit mode of the rounded results.
    ''' </summary>
    ''' <param name="Undo">If true, then the original values will be reinstated. If false, the newly edited values will remain.</param>
    ''' <param name="editMode">Edit mode types available within the Example details form for editing various datagrid fields.</param>
    ''' <remarks></remarks>
    Private Sub DisableEditMode(ByVal editMode As eExampleEditMode, ByVal Undo As Boolean)
        Dim i As Integer = 0
        Dim btnContent As String = "Edit"

        myEditMode = eExampleEditMode.None
        editBenchmark = False
        editIndependent = False
        editOutputParameter = False

        btnSaveEditBM.IsEnabled = False
        btnEditIndependent.IsEnabled = True
        btnEditOutputParameter.IsEnabled = True
        btnEditIndependent.Content = btnContent
        btnEditOutputParameter.Content = btnContent
        If example.comparedExample Then
            btnEditBM.IsEnabled = True
            btnEditBM.Content = btnContent
        End If

        BenchmarkValue.IsReadOnly = True
        IndependentValue.IsReadOnly = True
        OutputParameter.IsReadOnly = True

        Select Case editMode
            Case eExampleEditMode.Benchmark
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.None

                    If Undo Then    'Retrieve the original values
                        exampleResult.benchmarkValue = benchmarksOriginal(i)
                        i += 1
                    End If
                Next
            Case eExampleEditMode.IndependentValue
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.None

                    If Undo Then    'Retrieve the original values
                        exampleResult.independentValue = independentOriginal(i)
                        i += 1
                    End If
                Next
            Case eExampleEditMode.OutputParameter
                For Each exampleResult As cExampleItem In example.itemList
                    exampleResult.valueChanged = False
                    exampleResult.editMode = eExampleEditMode.None

                    If Undo Then    'Retrieve the original values
                        exampleResult.outputParameter = outputParameterOriginal(i)
                        i += 1
                    End If
                Next
        End Select

        AssignStyles()
    End Sub

    ''' <summary>
    ''' Updates all other changed values, such as independent values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateOther()
        'Create update object
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Update, example.myMCModel)
        windowXMLNodeCreateObject.ShowDialog()

        'Check if form was canceled
        If windowXMLNodeCreateObject.formCanceled Then Exit Sub

        Select Case csiMessageBox.Show(eMessageActionSets.YesNo, titleMCSourceUpdate, promptMCSourceUpdateWarning, promptMCSourceUpdateQuestion, example.pathXmlMC, MessageBoxImage.Warning)
            Case eMessageActions.Yes
                'TODO: Status bar here in the future
                'Set up wait cursor
                Dim cursorWait As New cCursorWait

                'Updates the class with the added 'update' & 'ticket' objects
                example.myMCModel = CType(windowXMLNodeCreateObject.myMCModelSave.Clone, cMCModel)

                'Updates XML file
                example.UpdateOthers()

                'Updates data, files, and refreshes form, including updates comparisons.
                myCsiTester.UpdateExampleData(example, False)

                'Update display
                labelBenchmarkVersion.Content = example.benchmarkLastVersion
                For Each exampleResult As cExampleItem In dataGrid_Summary.SelectedItems
                    exampleResult.valueChanged = False
                Next

                'Set up wait cursor
                cursorWait.EndCursor()
            Case eMessageActions.No
        End Select
    End Sub

    ''' <summary>
    ''' Updates the benchmark values in the model control XML and updates the benchmark version reference.
    ''' </summary>
    ''' <param name="matchResult">If true, benchmarks will be saved to match the rounded results of the examples results. 
    ''' If false, the benchmarks will be saved with whatever values are stored in the class.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarks(ByVal matchResult As Boolean)
        'Create update object
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Update, example.myMCModel)
        windowXMLNodeCreateObject.ShowDialog()

        'Check if form was canceled
        If windowXMLNodeCreateObject.formCanceled Then Exit Sub

        Select Case csiMessageBox.Show(eMessageActionSets.YesNo, titleMCSourceUpdate, promptMCSourceUpdateWarning, promptMCSourceUpdateQuestion, example.pathXmlMC, MessageBoxImage.Warning)
            Case eMessageActions.Yes
                'TODO: Status bar here in the future
                'Set up wait cursor
                Dim cursorWait As New cCursorWait

                'Updates the class with the added 'update' & 'ticket' objects
                example.myMCModel = CType(windowXMLNodeCreateObject.myMCModelSave.Clone, cMCModel)

                'Updates XML file
                example.UpdateBenchmarks(matchResult)

                'Updates data, files, and refreshes form, including updates comparisons.
                myCsiTester.UpdateExampleData(example, True)

                'Update display
                labelBenchmarkVersion.Content = example.benchmarkLastVersion
                For Each exampleResult As cExampleItem In dataGrid_Summary.SelectedItems
                    exampleResult.valueChanged = False
                Next

                'Set up wait cursor
                cursorWait.EndCursor()
            Case eMessageActions.No
        End Select
    End Sub

    ''' <summary>
    ''' Updates the assumed run time of the current example to the actual run time of the results, multiplied by a specified factor to account for differences in computer speeds.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateTimes()
        Dim windowTimeFactor As New frmTimeFactor(ConvertTimesNumberMinute(example.timeRunActual), 1.5)
        windowTimeFactor.ShowDialog()

        'Check if form was canceled
        If windowTimeFactor.formCanceled Then Exit Sub

        Select Case csiMessageBox.Show(eMessageActionSets.YesNo, titleMCSourceUpdate, promptMCSourceUpdateWarning, promptMCSourceUpdateQuestion, example.pathXmlMC, MessageBoxImage.Warning)
            Case eMessageActions.Yes
                'TODO: Status bar here in the future
                'Set up wait cursor
                Dim cursorWait As New cCursorWait

                example.UpdateTimes(CStr(ConvertTimesStringMinutes(windowTimeFactor.myRunTimeAssumed)))

                'Updates data, files, and refreshes form.
                myCsiTester.UpdateExampleData(example, False)

                'Update display
                LabelRunAssumed.Content = example.timeRunAssumed
                LabelCheckAssumed.Content = example.timeCompareAssumed
                LabelTotalAssumed.Content = example.timeCheckAssumed

                'Set up wait cursor
                cursorWait.EndCursor()
            Case eMessageActions.No
        End Select
    End Sub

    '=== Datagrid Cell Editing
    ''' <summary>
    ''' Copy, paste, and undo actions. Can undo one action.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridCell_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'Dim selectedRowIndex As Integer = dataGrid_ValueNodeContent.SelectedIndex
        ' Dim currentSelectedItem = dataGrid_ValueNodeContent.SelectedItem
        Dim cell As DataGridCell = CType(sender, DataGridCell)

        If cell IsNot Nothing Then
            If Not cell.IsEditing Then
                If Not cell.IsReadOnly Then
                    If Not cell.IsFocused Then cell.Focus()
                    Try
                        If Not cell.IsSelected Then cell.IsSelected = True
                    Catch ex As Exception

                    End Try


                    If e.Key = Key.C AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for copy action
                        For Each exampleResult As cExampleItem In dataGrid_Summary.SelectedItems
                            Select Case exampleResult.editMode
                                Case eExampleEditMode.Benchmark : cellText = exampleResult.checkResultRounded
                                Case eExampleEditMode.IndependentValue : cellText = exampleResult.independentValue
                                Case eExampleEditMode.OutputParameter : cellText = exampleResult.outputParameter
                            End Select
                        Next
                    ElseIf e.Key = Key.V AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for paste action
                        Try
                            For Each exampleResult As cExampleItem In dataGrid_Summary.SelectedItems
                                Select Case exampleResult.editMode
                                    Case eExampleEditMode.Benchmark
                                        cellTextUndo = exampleResult.checkResultRounded
                                        exampleResult.checkResultRounded = cellText
                                    Case eExampleEditMode.IndependentValue
                                        cellTextUndo = exampleResult.independentValue
                                        exampleResult.independentValue = cellText
                                    Case eExampleEditMode.OutputParameter
                                        cellTextUndo = exampleResult.outputParameter
                                        exampleResult.outputParameter = cellText
                                End Select

                                exampleResult.valueChanged = True
                            Next
                        Catch ex As Exception
                            RaiseEvent Log(New LoggerEventArgs(ex))
                            Exit Sub
                        End Try

                        dataGrid_Summary.Items.Refresh()
                    ElseIf e.Key = Key.Z AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for undo action
                        For Each exampleResult As cExampleItem In dataGrid_Summary.SelectedItems
                            Select Case exampleResult.editMode
                                Case eExampleEditMode.Benchmark : exampleResult.checkResultRounded = cellTextUndo
                                Case eExampleEditMode.IndependentValue : exampleResult.independentValue = cellTextUndo
                                Case eExampleEditMode.OutputParameter : exampleResult.outputParameter = cellTextUndo
                            End Select
                            exampleResult.valueChanged = False
                        Next
                        dataGrid_Summary.Items.Refresh()
                    End If
                End If
            End If

            'TODO: Currently cell is not being re-selected after refreshing datagrid
            'If Not cell.IsFocused Then cell.Focus()
            'If Not cell.IsSelected Then cell.IsSelected = True
            'dataGrid_ValueNodeContent.SelectedIndex = selectedRowIndex
            'dataGrid_ValueNodeContent.SelectedItem = currentSelectedItem

            'For Each ExampleRow As cXMLNodeDataGrid In dataGrid_ValueNodeContent.Items
            '    If ExampleRow Is dataGrid_ValueNodeContent.SelectedItem Then
            '        dataGrid_ValueNodeContent.SelectedItem = ExampleRow
            '    End If
            'Next

        End If
    End Sub

    ''' <summary>
    ''' Sets status of cell for whether or not the value has been changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_Summary_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles dataGrid_Summary.CellEditEnding

        Try
            Dim cell As TextBox = CType(e.EditingElement, TextBox)
            Dim cellText As String

            cellText = cell.Text

            For Each exampleBM As cExampleItem In dataGrid_Summary.SelectedItems
                Select Case exampleBM.editMode
                    Case eExampleEditMode.Benchmark
                        If cellText = exampleBM.checkResultRounded Then
                            exampleBM.valueChanged = False
                        Else
                            exampleBM.checkResultRounded = cellText
                            exampleBM.valueChanged = True
                        End If
                    Case eExampleEditMode.IndependentValue
                        If cellText = exampleBM.independentValue Then
                            exampleBM.valueChanged = False
                        Else
                            exampleBM.independentValue = cellText
                            exampleBM.valueChanged = True
                        End If
                    Case eExampleEditMode.OutputParameter
                        If cellText = exampleBM.outputParameter Then
                            exampleBM.valueChanged = False
                        Else
                            exampleBM.outputParameter = cellText
                            exampleBM.valueChanged = True
                        End If
                End Select
            Next
        Catch ex As Exception

        End Try

        cellBeingEdited = e
    End Sub

    ''' <summary>
    ''' Determines that whether cell has been edited and if so, refreshes the datagrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_Summary_CurrentCellChanged(sender As Object, e As EventArgs) Handles dataGrid_Summary.CurrentCellChanged
        If cellBeingEdited IsNot Nothing Then
            If cellBeingEdited.EditAction = DataGridEditAction.Commit Then
                Try
                    dataGrid_Summary.Items.Refresh()
                Catch ex As Exception

                End Try
            End If
        End If
        cellBeingEdited = Nothing
    End Sub

#End Region

#Region "Methods: Private"
    'Not used, but may be useful, say for 'Help' hyperlinks?
    Private Sub Hyperlink_RequestNavigate(ByVal sender As Object, ByVal e As RequestNavigateEventArgs)
        Process.Start(New ProcessStartInfo(e.Uri.AbsoluteUri))
        e.Handled = True
    End Sub

    '=== Dynamically ties buttons to the width and position of certain datagrid columns
    ''' <summary>
    ''' Dynamically sets the layout grid position of buttons to match the display index position of the datagrid columns, and sizes the layout grid accordingly.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_Summary_ColumnReordered(sender As Object, e As DataGridColumnEventArgs) Handles dataGrid_Summary.ColumnReordered
        SizeGridToDataGrid(gridEditBtns.ColumnDefinitions, dataGrid_Summary.Columns)

        btnEditOutputParameter.SetValue(Grid.ColumnProperty, OutputParameter.DisplayIndex)
        btnEditIndependent.SetValue(Grid.ColumnProperty, IndependentValue.DisplayIndex)
        btnEditBM.SetValue(Grid.ColumnProperty, BenchmarkValue.DisplayIndex)
    End Sub
    ''' <summary>
    ''' Dynamically sets buttons and a layout grid to the width of the datagrid columns.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_Summary_LayoutUpdated(sender As Object, e As EventArgs) Handles dataGrid_Summary.LayoutUpdated
        btnEditOutputParameter.Width = OutputParameter.ActualWidth
        btnEditIndependent.Width = IndependentValue.ActualWidth
        btnEditBM.Width = BenchmarkValue.ActualWidth

        SizeGridToDataGrid(gridEditBtns.ColumnDefinitions, dataGrid_Summary.Columns)
    End Sub

    '=== Dynamically sets the maximum height of the datagrid so that scrollbars appear if not all rows are visible
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        UpdateDataGridHeight(dataGrid_Summary, gridMain, rowDG, brdr_DG_ExampleSummary)
    End Sub
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the splitter is dragged over the content
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Splitter_DragDelta(sender As Object, e As Controls.Primitives.DragDeltaEventArgs) Handles Splitter.DragDelta
        UpdateDataGridHeight(dataGrid_Summary, gridMain, rowDG, brdr_DG_ExampleSummary)
    End Sub
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the loaded browser overlaps the datagrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WebBrowserPDFViewer_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles WebBrowserPDFViewer.LoadCompleted
        UpdateDataGridHeight(dataGrid_Summary, gridMain, rowDG, brdr_DG_ExampleSummary)
    End Sub

    ''' <summary>
    ''' Displayes the documentation PDF in the examples details form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisplayDocumentationInForm()
        WebBrowserPDFViewer.Navigate("file:///" & example.linkDocumentation)

        dgSummaryHeight = rowDG.ActualHeight
        menuItem_Documentation.IsEnabled = False

        rowDG.Height = New GridLength(1, GridUnitType.Star)     'Sizes the datagrid portion (1*)
        rowBrowser.Height = New GridLength(1, GridUnitType.Star)     'Sizes the browser portion to be equal to the datagrid to start (1*)
        rowBrowserBtn.Height = New GridLength(37, GridUnitType.Pixel)   'Sizes the buttons beneath the browser viewer
        Me.Splitter.Visibility = Windows.Visibility.Visible
    End Sub

    ''' <summary>
    ''' Opens the database file of the currently viewed example.  Right now this is only Access, but in the future could be an XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenDBFile()
        Dim dbFileName As String = ""
        Dim dbFilePath As String = ""
        Dim destinationModelPath As String = myCsiTester.ConvertPathModelSourceToDestination(example.pathModelFile, True)

        dbFileName = example.outputFileName & "." & example.outputFileExtension

        If (Not String.IsNullOrEmpty(example.outputFileName) OrElse
            Not String.IsNullOrEmpty(example.outputFileExtension)) Then

            dbFilePath = GetPathDirectoryStub(destinationModelPath) & "\" & dbFileName 'Concatenate to model control XML path
        End If

        If Not IO.File.Exists(dbFilePath) Then
            dbFileName = example.GetExportedTableNameWExtension()
            dbFilePath = GetPathDirectoryStub(destinationModelPath) & "\" & dbFileName
        End If

        If IO.File.Exists(dbFilePath) Then OpenFile(dbFilePath, , True)
    End Sub

    ''' <summary>
    ''' Open model file in selected program with temporary batch file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenModelFile(ByVal programPath As String, ByVal destinationModelPath As String)
        Dim batchCommand As String = ""
        Dim batchPath As String

        batchPath = myCsiTester.testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & "OpenModelFile.bat"

        'Get the selected file paths
        batchCommand = batchCommand & """" & programPath & """ """ & destinationModelPath & """ "

        If Not String.IsNullOrEmpty(batchCommand) Then
            'Write batch file
            WriteBatch(batchCommand, batchPath, True)

            'Run batch file
            RunBatch(batchPath, True, False, True)
        End If
    End Sub


    ''' <summary>
    ''' Account for translated/imported model files, where the file name is changed.
    ''' </summary>
    ''' <param name="destinationModelPath">Path to the model file that is to be opened. This may be modified in this function to the model file chosen.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateTranslatedModel(ByRef destinationModelPath As String) As Boolean
        'Get list of model files in the directory that contain the model file name and extension
        Dim matchByName As New FileValidator.PartialNameMatchValidator(example.pathModelFile)
        Dim fileProcessor As New FileProcessor.FileList(matchByName)
        fileProcessor = CType(RecursiveFileProcessor.ProcessDirectory(GetPathDirectoryStub(destinationModelPath), fileProcessor), MPT.FileSystem.FileProcessor.FileList)
        Dim files As List(Of String) = fileProcessor.Paths

        'Sort by date modified, starting with the most recent
        files.OrderByDescending(Function(x) IO.File.GetLastWriteTime(x))

        'If multiple potential model files, prompt the user, showing the list.
        If files.Count > 1 Then
            Dim modelFilePathsString As String = ""

            'Create list of model file names and their date modified
            For Each filePath As String In files
                modelFilePathsString = modelFilePathsString & GetPathFileName(filePath) & " last modified on: " & IO.File.GetLastWriteTime(filePath) & Environment.NewLine
            Next

            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Exclamation),
                                            _PROMPT_POSSIBLE_TRANSLATED_MODEL & "{1}: {0} {2} {0}{0}" & _PROMPT_POSSIBLE_TRANSLATED_MODEL_QUESTION,
                                            _TITLE_POSSIBLE_TRANSLATED_MODEL,
                                            Environment.NewLine, testerSettings.programName, modelFilePathsString)
                Case eMessageActions.Yes
                    For Each filePath As String In files
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Question),
                                            "Open the following model file? {0}{0} {1}",
                                            _TITLE_POSSIBLE_TRANSLATED_MODEL,
                                            Environment.NewLine, GetPathFileName(filePath))
                            Case eMessageActions.Yes
                                'Cycle through each path, where the user can choose to use the file or proceed to the next one.
                                destinationModelPath = filePath
                                Exit For
                            Case eMessageActions.No
                            Case eMessageActions.Cancel
                                Return False
                        End Select
                    Next
                Case eMessageActions.No
                    Return False
            End Select
        End If
        Return True
    End Function

    ''' <summary>
    ''' Checks whether or not the expected model file actually exists.
    ''' </summary>
    ''' <param name="destinationModelPath">Path to the model file that is to be opened.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDestination(ByVal destinationModelPath As String) As Boolean
        If Not IO.File.Exists(destinationModelPath) Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Information),
                                                        "The following file does not exist: {0}{0} {1} {0}{0}" &
                                                        "Please initialize a destination directory in order to proceed",
                                                        "File Missing",
                                                        Environment.NewLine, destinationModelPath))
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Checks whether or not the CSi anlaysis program set to open the model file is compatible with the model file. Based on list specified in the model control XML.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateProgram() As Boolean
        Dim validProgram As Boolean = False

        For Each program As eCSiProgram In example.myMCModel.targetProgram
            If testerSettings.programName = program Then
                validProgram = True
                Exit For
            End If
        Next
        If Not validProgram Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Information),
                                                        "The {1} model cannot be opened in {2}. {0}{0} Please select a compatible analysis program before opening the model file.",
                                                        "Invalid Program Selected",
                                                        Environment.NewLine, GetPathFileName(example.pathModelFile), testerSettings.programName))
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Determines if the program version is compatible with the model version. If there are possible incompatibilities, the user is given choices on how to proceed.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateReleaseVersion() As Boolean
        If Not example.programVersion = PROGRAM_VERSION_DEFAULT Then
            If testerSettings.programVersion < example.programVersion Then
                'Version is older, so model will not open. Notify user
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Information),
                                                        "The selected {1} program is of version {2}. The model was saved in version {3}. {0}{0} " &
                                                        "Please select a version of {1} that is equal to or greater than the model's version.",
                                                        "Older Version",
                                                        Environment.NewLine, testerSettings.programName, testerSettings.programVersion, example.programVersion))
                Return False
            ElseIf testerSettings.programVersion > example.programVersion Then
                'Version is newer, so model, if saved, will be of the newer version. Notify user
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            "Warning! The selected {1} program is of version {2}. The model was saved in version {3}. {0}{0}" &
                                            "If the model is saved, it will can only be opened in versions equal to or greater than version {2}. {0}{0}" &
                                            "Do you wish to continue?",
                                            "Newer Version",
                                            Environment.NewLine, testerSettings.programName, testerSettings.programVersion, example.programVersion)
                    Case eMessageActions.Yes
                    Case eMessageActions.No
                        Return False
                End Select
            End If
        Else
            'Version is unknown, as the program has not been run
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            "The version of {1} that the model file was saved in cannot be determined. The currently selected version of {2} {0}{0} " &
                                            "might be of a version older or newer than the model file. Unpredictable results may occur by opening this model file." &
                                            "Do you wish to continue?",
                                            "Unknown Model Version",
                                            Environment.NewLine, testerSettings.programName, testerSettings.programVersion)
                Case eMessageActions.Yes
                Case eMessageActions.No
                    Return False
            End Select
        End If
        Return True
    End Function
#End Region

End Class
