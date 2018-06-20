Option Explicit On
Option Strict On

'TODO: Full Version
'Examples Form
'   1. Globally accessible collections strategy? For now assigning me.list to public list in friendVariables.
'   2. Is closing the existing form the best way? Wanting to move 'within the same window'

'1. Filtering to load only XMLs that are model XMLs:
'   Use Ondrej's method?
'   Speed up process. Perhaps using saved list?
'   Consider 'legal' file types, e.g. edb for SAP, bdb for SAP

'1. Perhaps use this for option of setting to default? 
'       XMLResultsPath = csiTester.GetPreviousTestResultsPath(regTest.program_name) & "\latest.xml"
'       Folders & files not generated if not exist. Instead, RegTest fails. Autogenerate method?
'9. RegTest/testing/test_to_run = "run as is with different sets of analysis parameters" runs all analysis permutations.
'   9a. Make option for user to specify this

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.DataGridLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary
Imports MPT.Time.TimeLibrary
Imports MPT.XML.ReaderWriter.cXmlReadWrite

Imports CSiTester.cRegTest
Imports CSiTester.cSettings
Imports CSiTester.cCsiTester
Imports CSiTester.cExampleTestSet
Imports CSiTester.cMCValidator

Imports CSiTester.cPathSettings

''' <summary>
''' This is the main startup form of the program.
''' All examples are displayed as a summary on one line per example, and results summaries are also populated here. 
''' This is the location where examples are set to be run and compared, and the check executed.
''' All of the parameters for running the suite are set from this form.
''' </summary>
''' <remarks></remarks>
Class CSiTester
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

    Friend Const CLASS_STRING As String = "CSiTester"

#Region "Prompts"
    Private Const _TITLE_MC_SOURCE_UPDATE As String = "Updating Model Source"
    Private Const _PROMPT_MC_SOURCE_UPDATE_WARNING As String = "Warning! The following model source files will be updated by this operation: "
    Private Const _PROMPT_MC_SOURCE_UPDATE_QUESTION As String = "Do you wish to continue?"

    Private Const _TITLE_ILLEGAL_ACTION_DURING_CHECK As String = "Action Not Allowed"
    Private Const _PROMPT_ILLEGAL_ACTION_DURING_CHECK As String = "Action not allowed during example check."

    Private Const _TITLE_RESET_SESSION As String = "Reset Session"
    Private Const _PROMPT_RESET_SESSION As String = "Warning! All files and folders within the destination directory will be deleted. Do you wish to continue?"


    Private Const _TITLE_RUN_AND_COMPARE_FAILED As String = "Run and Compare Failed"
    Private Const _PROMPT_CHECK_RUN_ERROR As String = "Running and comparing of examples was aborted due to a supporting file error. It is recommended that you either perform the run and compare procedures through CSiTester separately, or clear and reset the program files and try again." &
                                    vbNewLine & vbNewLine & "Would you like to clear and reset all CSiTester and files?"
    Private Const _PROMPT_REGTEST_FAILED As String = "regtest.exe experienced a problem and had to close before all analyses had been run. It is recommended that you refresh the CSiTester files and try again." &
                                    vbNewLine & vbNewLine & "Would you like to clear and reset all CSiTester and files?"
    Private Const _PROMPT_REGTEST_FAILED_GENERAL As String = "regtest.exe experienced a problem and had to close before all operations had been run."

    Private Const _TITLE_TESTER_LOCATION_CHANGED As String = "New Run Location"
    Private Const _PROMPT_TESTER_LOCATION_CHANGED As String = "CSiTester is loading from a different location than when it had last been saved."
#End Region

#Region "Tooltips - CSiTester"
    '=== Note
    '=  TODO
    '=  The tool tip title (ttT) values should match the button names in the progam. This has been fixed on buttons currently using the title in the published level, 
    '=  but changes might be needed for other buttons later.
    '===
    Private Const ttTCheck As String = "Check Examples"
    Private Const ttCheck As String = "Check example models for selected runs and comparisons."

    Private Const ttTDeleteRunFiles As String = "Analysis Files"
    Private Const ttDeleteRunFiles As String = "Deletes files generated for analysis."

    Private Const ttTDeleteAfterRun As String = "Delete After Run"
    Private Const ttDeleteAfterRun As String = "Files will be deleted after an analysis is run."
    Private Const ttTDeleteDefault As String = "Standard Files"
    Private Const ttDeleteDefault As String = "Standard analysis files will be deleted (majority of file types)."
    Private Const ttTDeleteLogWarning As String = "Log & Warning Files"
    Private Const ttDeleteLogWarning As String = "Deletes log and warning files."
    Private Const ttTDeleteExportedTables As String = "Exported Table Files"
    Private Const ttDeleteExportedTables As String = "Deletes any exported table files."
    Private Const ttTDeleteModelText As String = "Model Text Files"
    Private Const ttDeleteModelText As String = "Deletes any text model files, such as those formed by opening or saving a model."
    Private Const ttTDeleteRunAll As String = "All Except Model File"
    Private Const ttDeleteRunAll As String = "Deletes all files at the location of the model file that share the same name."

    Private Const ttTClearResults As String = "Destination Directories"
    Private Const ttClearResults As String = "Deletes the most recently generated models & results directories from the tester destination directory."
    Private Const ttTResetDefaults As String = "Restore Default Settings"
    Private Const ttResetDefaults As String = "Resets the settings back to the default settings. Run results are preserved but will need to be refilled in the display."
    Private Const ttTReset As String = "Reset Session"
    Private Const ttReset As String = "Complete reset by restoring default settings and deleting the most recently generated models and results directories in the tester destination directory."

    Private Const ttTBrowseProgram As String = "Program"
    Private Const ttBrowseProgram As String = "Browse for the CSi program to test."
    Private Const ttTBrowseSource As String = "Source"
    Private Const ttBrowseSource As String = "Browse for the location of the original models." 'and tester files."
    Private Const ttTBrowseDestination As String = "Destination"
    Private Const ttBrowseDestination As String = "Browse for the location to which models and tester files are to be copied."

    Private Const ttTSelectRunAll As String = "Run All Examples"
    Private Const ttSelectRunAll As String = "Selects all examples to be run."
    Private Const ttTSelectRunNone As String = "Run No Examples"
    Private Const ttSelectRunNone As String = "Deselects all examples from a run selection."
    Private Const ttTSelectRunAdd As String = "Add to Run Selection"
    Private Const ttSelectRunAdd As String = "Adds selected example rows to the list of examples to be run."
    Private Const ttTSelectRunRemove As String = "Remove from Run Selection"
    Private Const ttSelectRunRemove As String = "Removes selected example rows from the list of examples to be run."
    Private Const ttTSelectRunReplace As String = "Replace Run Selection"
    Private Const ttSelectRunReplace As String = "Replaces any existing run selection with the currently selected example rows."
    Private Const ttTSelectRunCompareLink As String = ""

    Private Const ttSelectRunCompareLink As String = "Links run and compare selections."

    Private Const ttTSelectCompareAll As String = "Compare All Examples"
    Private Const ttSelectCompareAll As String = "Selects all examples to be compared."
    Private Const ttTSelectCompareNone As String = "Compare No Examples"
    Private Const ttSelectCompareNone As String = "Deselects all examples from a compare selection."
    Private Const ttTSelectCompareAdd As String = "Add to Compare Selection"
    Private Const ttSelectCompareAdd As String = "Adds selected example rows to the list of examples to be compared."
    Private Const ttTSelectCompareRemove As String = "Remove from Compare Selection"
    Private Const ttSelectCompareRemove As String = "Removes selected example rows from the list of examples to be compared."
    Private Const ttTSelectCompareReplace As String = "Replace Compare Selection"
    Private Const ttSelectCompareReplace As String = "Replaces any existing compare selection with the currently selected example rows."

    Private Const ttTSelectAllTabs As String = "Apply to All Tabs"
    Private Const ttSelectAllTabs As String = "Applies selection operation to all tabs."

    Private Const ttTRunCommandLine As String = "Run Method: Command Line"
    Private Const ttRunCommandLine As String = "Runs the program using command line calls. A new instance of the program opens for each example run."
    Private Const ttTRunBatchFile As String = "Run Method: Batch File"
    Private Const ttRunBatchFile As String = "Runs the program using a batch file. A single instance of the program remains open as each example is run."
    Private Const ttTViewBatchFile As String = "View Batch File"
    Private Const ttViewBatchFile As String = "View the batch file generated for the current run in a text editor."
    Private Const ttTSolver As String = "Analysis Options: Solver"
    Private Const ttSolver As String = "Select which solver is to be used for analysis. Default: Advanced Solver."
    Private Const ttTProcess As String = "Analysis Options: Process"
    Private Const ttProcess As String = "Select which process is to be used for analysis. Default: Separate Process."
    Private Const ttTBitMode As String = "Analysis Options: Bit Mode"
    Private Const ttBitMode As String = "Selects whether to run the program as 64-bit, if possible, or to only run in 32-bit. Default: Force 32 Bit."
    Private Const ttTTabsMerge As String = "Merge Tabs"
    Private Const ttTabsMerge As String = "Merges all example set tabs into one set to view."
    Private Const ttTTabsSeparate As String = "Separate Tabs"
    Private Const ttTabsSeparate As String = "Separates current view into example set tabs organized by example classification."

    Private Const ttTSave As String = "Save Settings"
    Private Const ttSave As String = "Saves settings such as examples to run or check, or results viewed."
#End Region

#Region "Constants"
    'Private _defaultDestinationDir As String = DIR_TESTER_DESTINATION_DIR_INSTALL_DEFAULT
    Private Const _BTN_ICON_CLEAR_READY As String = "/CSiTester;component/Resources/Icons/32px/Lock2-Locked.png"  '".\Resources\Icons\64px\Lock2-Locked.png"
    Private Const _BTN_ICON_CLEAR_DONE As String = "/CSiTester;component/Resources/Icons/32px/Lock2-Unlocked.png"   '".\Resources\Icons\64px\Lock2-Unlocked.png"

    Private Const _SUITE_ANALYSIS As String = "Analysis"
    Private Const _SUITE_SAP2000 As String = "SAP2000"
    Private Const _SUITE_ETABS As String = "ETABS"
    Private Const _SUITE_SAFE As String = "SAFE"
    Private Const _SUITE_DESIGN_STEELFRAME As String = "Steel Frame Design"
    Private Const _SUITE_DESIGN_CONCRETEFRAME As String = "Concrete Frame Design"
    Private Const _SUITE_DESIGN_SHEARWALL As String = "Shear Wall Design"
    Private Const _SUITE_DESIGN_COMPOSITEBEAM As String = "Composite Beam Design"
    Private Const _SUITE_DESIGN_COMPOSITECOLUMN As String = "Composite Column Design"
    Private Const _SUITE_SUFFIX As String = " VS"

    Private Const _FORM_TITLE_PREFIX As String = "CSiTester - "
    Private Const _FORM_TITLE_SUFFIX As String = " Verification Examples"
#End Region

#Region "Enums"
    ''' <summary>
    ''' Main/official suite classifications.
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum eSuite
        <Description("")> None = 0
        <Description("")> Unknown
        <Description("SAP2000 Analysis Verification Suite")> SAP2000
        <Description("ETABS Analysis Verification Suite")> ETABS
        <Description("SAFE Verification Suite")> SAFE
        <Description("Design Verification Suite - Steel Frame")> DesignSteelFrame
        <Description("Design Verification Suite - Concrete Frame")> DesignConcreteFrame
        <Description("Design Verification Suite - Shear Wall")> DesignShearWall
        <Description("Design Verification Suite - Composite Beam")> DesignCompositeBeam
        <Description("Design Verification Suite - Composite Column")> DesignCompositeColumn
    End Enum
#End Region

#Region "Variables"
    Private _programInitializer As cProgramInitializer

    ''' <summary>
    ''' Specifies whether a form is initializing to coordinate certain actions in the program.
    ''' </summary>
    ''' <remarks></remarks>
    Private _frmInitialize As Boolean = False

    Private _classRegionHeaderList As List(Of String)
    Private _codeExampleNumberHeaderList As List(Of String)
    Private _syncRunCompareSelections As Boolean = False

    Private _testerSourceDir As String

#End Region


#Region "Initialization: Data"
    ''' <summary>
    ''' First and main initialization function of the entire program, as well as the startup form.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Try
            'Suppress any exception messages and other error messages, or not. Uncomment the appropriate one for Published level only.
            'suppressExStates = True
            'suppressExStates = False

            _frmInitialize = True
            _programInitializer = New cProgramInitializer

            '====== Microsoft's Function ====== 
            ' This call is required by the designer.
            InitializeComponent()

            _testerSourceDir = pathStartup()

            _programInitializer.InitializationMain()

            'The commented section was originally used here. Seemed necessary at the time to do after data initialization.
            '   It needs to be at the top for handling command line parameters for timely initialization of end-to-end testing.
            '   In 'internal' mode this seems to be working fine. 
            '   It might break down in certain uses, or 'published' mode, so keep this until the program is proven to be OK with the new arrangement.
            ''====== Microsoft's Function ====== 
            '' This call is required by the designer.
            'InitializeComponent()
            '' Add any initialization after the InitializeComponent() call.

            '====== Remainder of the Form Loading Process ====== 
            'Load CSiTester and the remainder of the form initialization
            RefreshForm(True)

            SetButtons()
            InitializeReleaseDefaultsOnce()

            Dim isCSiTesterSaved As Boolean
            _programInitializer.InitializationCompletion(isCSiTesterSaved)

            If isCSiTesterSaved Then SaveCSiTester()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Initializes all aspects of the form after gathering RegTest data.
    ''' </summary>
    ''' <param name="p_firstLoad">Specifies if this is the first time the tester is being loaded.</param>
    ''' <remarks></remarks>
    Private Sub RefreshForm(Optional ByVal p_firstLoad As Boolean = False)
        Try
            _programInitializer.LoadCSiTester(p_firstLoad)

            'Set form title
            Me.Title = GetFormTitle()

            InitializeReleaseDefaults()

            'Set up form tabs and datagrid
            Me.DataContext = examplesTestSetList
            InitializeDataGridTabs(True)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Loads the XML editor and any referenced xml files into memory. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadXMLEditor() As Boolean
        Try
            myXMLEditor = New cXMLEditor
            myXMLEditor.InitializeXMLEditorData()
            Return True
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try
    End Function
#End Region

#Region "Initialization: Form Controls"
    ''' <summary>
    ''' Assigns cRegTest and cSettings values to components of the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CSiTester_Load(sender As Object, e As EventArgs) Handles MyBase.Loaded
        Try
            _frmInitialize = True

            '~Buttons
            '~~Program Path
            RefreshBtnBrowseProgram()

            '~~Models Source Path
            btnBrowseSource.ToolTip = GetPrefix(ttBrowseSource, ".") & ": " & Environment.NewLine & myRegTest.models_database_directory.path 'promptModelSourceBrowse & Chr(10) & regTest.models_database_directory
            btnBrowseSourceBasic.ToolTipDescription = GetPrefix(ttBrowseSource, ".") & ": " & Environment.NewLine & myRegTest.models_database_directory.path 'promptModelSourceBrowse & Chr(10) & regTest.models_database_directory

            '~~Models Destination Path
            btnBrowseDestination.ToolTip = GetPrefix(ttBrowseDestination, ".") & ": " & Environment.NewLine & _programInitializer.testerDestinationDir 'promptModelDestinationBrowse & Chr(10) & myCsiTester.pathDestination 'regTest.models_run_directory
            btnBrowseDestinationBasic.ToolTipDescription = GetPrefix(ttBrowseDestination, ".") & ": " & Environment.NewLine & _programInitializer.testerDestinationDir 'btnBrowseDestination.ToolTip

            '~~Run Options
            '~~~Set File Delete Parameter
            If testerSettings.deleteAnalysisFilesStatus Then
                'btnDeleteAnalysisFilesToggle.IsChecked = True
                chkMenuItem_DeleteAfterRun.IsChecked = True
            Else
                'btnDeleteAnalysisFilesToggle.IsChecked = False
                chkMenuItem_DeleteAfterRun.IsChecked = False
            End If

            '~Radio Buttons
            '~~Run Options
            If Not myRegTest.run_using_batch_file Then
                radBtn_CommandLineRun.IsChecked = True
                radBtn_BatchRun.IsChecked = False
            Else
                radBtn_CommandLineRun.IsChecked = False
                radBtn_BatchRun.IsChecked = True
            End If

            '~Combo Boxes
            InitializeAnalysisSettings()

            _frmInitialize = False
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets values for the buttons used to browse to a testing program
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshBtnBrowseProgram()
        btnBrowseProgram.ToolTip = GetPrefix(ttBrowseProgram, ".") & ": " & Environment.NewLine & myRegTest.program_file.path
        btnBrowseProgramBasic.ToolTipDescription = GetPrefix(ttBrowseProgram, ".") & ": " & Environment.NewLine & myRegTest.program_file.path
    End Sub

    '=== Combo Boxes
    ''' <summary>
    ''' Initializes combo boxes for selecting analysis settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAnalysisSettings()
        Try
            '~~Run Selections
            'Set Up Combo Boxes
            LoadComboBoxes(cmbBox_Solver, testerSettings.analysisSolver, testerSettings.solverSaved, False)
            LoadComboBoxes(cmbBox_Process, testerSettings.analysisProcesses, testerSettings.processSaved, False)
            LoadComboBoxes(cmbBox_32Bit, testerSettings.analysisBitType, testerSettings.bitTypeSaved, False)

            '=== Table Settings
            If testerSettings.programName = eCSiProgram.ETABS Then
                btnTableExportSettings.IsEnabled = True
            Else
                btnTableExportSettings.IsEnabled = False
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    '=== Datagrid Headers & Tabs
    ''' <summary>
    ''' Generates additional tabs for each example unique Classification_2 entry
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeDataGridTabs(Optional ByVal ReplaceTabs As Boolean = False)
        Try
            'Add DataGrid Tabs programmatically
            Dim myTab As System.Windows.Controls.TabItem
            Dim i As Integer = 0

            'Clears existing tabs if specified
            If ReplaceTabs Then
                Dim numTabs As Integer = myTabControlSummary.Items.Count
                Dim j As Integer = numTabs

                For i = 0 To numTabs
                    If Not j = 1 Then
                        myTabControlSummary.Items.RemoveAt(j - 1)
                        j = j - 1
                    Else
                        Exit For
                    End If
                Next
            End If

            'Populates first tab
            i = 0
            dataGrid_ExampleSummary.DataContext = examplesTestSetList(0)
            'Tab Header
            If (String.IsNullOrEmpty(examplesTestSetList(0).exampleClassification) OrElse
                examplesTestSetList(0).exampleClassification = "None") Then

                TabMaster.Header = GetEnumDescription(eTestSetClassification.DefaultExamples)
            Else
                TabMaster.Header = TabHeaderFilter(examplesTestSetList(0).exampleClassification)
            End If

            'Adds additional tabs, if necessary. 
            'Skips first examplesListItem as it is already loaded in the first tab
            For Each examplesListItem As cExampleTestSet In examplesTestSetList
                If Not i = 0 Then
                    ''If this is the published level, do not create a failed examples set in order to avoid generating a failed examples tab.
                    If (StringsMatch(examplesListItem.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) AndAlso
                        testerSettings.csiTesterlevel = eCSiTesterLevel.Published) Then
                        Continue For
                    End If

                    'If String.IsNullOrEmpty(examplesListItem.maxPercentDifference) Then              'Create standard tab for standard test set
                    myTab = New System.Windows.Controls.TabItem

                    'Tab Header
                    If String.IsNullOrEmpty(examplesListItem.exampleClassification) Then
                        myTab.Header = GetEnumDescription(eTestSetClassification.DefaultExamples)
                    Else
                        myTab.Header = TabHeaderFilter(examplesListItem.exampleClassification)
                    End If
                    myTabControlSummary.Items.Add(myTab)

                    If StringsMatch(examplesListItem.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                        myTab.Background = Brushes.Red
                    End If
                End If
                i = i + 1
            Next

            GetClassRegionHeader()
            GetCodeExampleNumberHeader()

            'Initialized first tab headers
            AssignClassRegionHeader(0)
            AssignCodeExampleNumberHeader(0)

            'Hide tab if there is only one visible
            If myTabControlSummary.Items.Count = 1 Then
                myTabControlSummary.Visibility = Windows.Visibility.Collapsed
            Else
                If (myTabControlSummary.Items.Count = 2 AndAlso
                    examplesTestSetList(0).exampleClassification = "None") Then

                    TabMaster.Header = GetEnumDescription(eTestSetClassification.MergedExamples)
                End If
                myTabControlSummary.Visibility = Windows.Visibility.Visible
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Takes the Classification 2 node value to be applied to the tab header, and adjusts the name if it matches a particular header name
    ''' </summary>
    ''' <param name="p_tabHeader">String displayed on the tab.</param>
    ''' <remarks></remarks>
    Private Function TabHeaderFilter(ByRef p_tabHeader As String) As String
        Try
            If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then
                Select Case p_tabHeader
                    Case GetEnumDescription(eSuite.SAP2000) : Return _SUITE_ANALYSIS
                    Case GetEnumDescription(eSuite.ETABS) : Return _SUITE_ANALYSIS
                    Case GetEnumDescription(eSuite.SAFE) : Return _SUITE_SAFE
                    Case GetEnumDescription(eSuite.DesignSteelFrame) : Return _SUITE_DESIGN_STEELFRAME
                    Case GetEnumDescription(eSuite.DesignConcreteFrame) : Return _SUITE_DESIGN_CONCRETEFRAME
                    Case GetEnumDescription(eSuite.DesignShearWall) : Return _SUITE_DESIGN_SHEARWALL
                    Case GetEnumDescription(eSuite.DesignCompositeBeam) : Return _SUITE_DESIGN_COMPOSITEBEAM
                    Case GetEnumDescription(eSuite.DesignCompositeColumn) : Return _SUITE_DESIGN_COMPOSITECOLUMN
                    Case Else : Return p_tabHeader
                End Select
            Else
                Select Case p_tabHeader
                    Case GetEnumDescription(eSuite.SAP2000) : Return _SUITE_SAP2000 & " " & _SUITE_ANALYSIS & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.ETABS) : Return _SUITE_ETABS & " " & _SUITE_ANALYSIS & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.SAFE) : Return _SUITE_SAFE & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.DesignSteelFrame) : Return _SUITE_DESIGN_STEELFRAME & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.DesignConcreteFrame) : Return _SUITE_DESIGN_CONCRETEFRAME & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.DesignShearWall) : Return _SUITE_DESIGN_SHEARWALL & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.DesignCompositeBeam) : Return _SUITE_DESIGN_COMPOSITEBEAM & _SUITE_SUFFIX
                    Case GetEnumDescription(eSuite.DesignCompositeColumn) : Return _SUITE_DESIGN_COMPOSITECOLUMN & _SUITE_SUFFIX
                    Case Else : Return p_tabHeader
                End Select
            End If

            'TODO: In the future, perhaps based on level, or user selection of "Combine Published/Internal", retitle design classifications so they are lumped with unpublished versions
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return p_tabHeader
    End Function

    ''' <summary>
    ''' Determines what header text should be used for the Class/Region datagrid column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetClassRegionHeader()
        Try
            Dim myHeader As String = ""
            Dim headerClass As Boolean
            Dim headerRegion As Boolean
            Dim headerClassRegion As Boolean

            _classRegionHeaderList = New List(Of String)

            'Determine from list of examples if all examples are of either type, mixed type, or none
            For Each examplesListItem As cExampleTestSet In examplesTestSetList
                headerClass = False
                headerRegion = False
                headerClassRegion = False

                For Each myExample As cExample In examplesListItem.examplesList
                    If Not String.IsNullOrEmpty(myExample.exampleClass) Then headerClass = True
                    If Not String.IsNullOrEmpty(myExample.exampleRegion) Then headerRegion = True
                Next

                If headerClass And headerRegion Then headerClassRegion = True

                'Assign header name based on class/region type
                If headerClassRegion Then
                    myHeader = "Class\Region"
                ElseIf headerRegion Then
                    myHeader = "Region"
                ElseIf headerClass Then
                    myHeader = "Class"
                Else
                    myHeader = ""
                End If

                _classRegionHeaderList.Add(myHeader)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Assigns the header text for the Class/Region datagrid column, or collapses the column if no example has either classification
    ''' </summary>
    ''' <param name="myTabIndex">Tab index selected. This is used to fetch the corresponding header.</param>
    ''' <remarks></remarks>
    Private Sub AssignClassRegionHeader(ByVal myTabIndex As Integer)
        Try
            Dim myHeader As String

            myHeader = _classRegionHeaderList(myTabIndex)

            'Name header accordingly, or collapse column if header is empty
            If String.IsNullOrEmpty(myHeader) Then
                ClassRegion.Visibility = Windows.Visibility.Collapsed
            Else
                ClassRegion.Visibility = Windows.Visibility.Visible
                ClassRegion.Header = myHeader
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines what header text should be used for the Code and Example Number datagrid column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCodeExampleNumberHeader()
        Try
            Dim myHeader As String = ""
            Dim headerAnalysis As Boolean
            Dim headerDesign As Boolean
            Dim headerAnalysisDesign As Boolean
            Dim modelIDTitle As Boolean
            Dim secondaryIDTitle As Boolean

            _codeExampleNumberHeaderList = New List(Of String)

            For Each examplesListItem As cExampleTestSet In examplesTestSetList
                headerAnalysis = False
                headerDesign = False
                headerAnalysisDesign = False
                modelIDTitle = False
                secondaryIDTitle = False

                'Determine from list of examples if all examples are of either type, mixed type, or none. Also if model is the only or any example title
                For Each myExample As cExample In examplesListItem.examplesList
                    If (Not String.IsNullOrEmpty(myExample.exampleType) OrElse
                        Not myExample.exampleType = "Not Specified") Then

                        If myExample.exampleType = "Analysis" Then headerAnalysis = True
                        If myExample.exampleType = "Design" Then headerDesign = True
                        If myExample.exampleType = "Analysis & Design" Then headerAnalysisDesign = True
                    End If
                    If myExample.numberCodeExample = "id " & myExample.modelID Then
                        modelIDTitle = True
                        'Suppress concurrent classification that occurs on same example as one using model ID for the example name
                        If myExample.exampleType = "Analysis" Then headerAnalysis = False
                        If myExample.exampleType = "Design" Then headerDesign = False
                        If myExample.exampleType = "Analysis & Design" Then headerAnalysisDesign = False
                    Else
                        secondaryIDTitle = True
                    End If
                Next

                If modelIDTitle And Not secondaryIDTitle Then       'All examples on tab are referenced by model id
                    myHeader = "Model ID"
                Else
                    If headerAnalysis And headerDesign Then headerAnalysisDesign = True 'Title is combination of analysis and design

                    'Assign header name based on analysis/design type
                    If headerAnalysis Then
                        myHeader = "Example Number"
                    ElseIf Not headerAnalysis And Not headerDesign And Not headerAnalysisDesign Then        'Example not classified as any type
                        myHeader = "Example Number"
                    Else
                        myHeader = "Code & Example Number"
                    End If

                    If modelIDTitle Then myHeader = myHeader & " or Model ID" 'Append model ID title if some examples have this as the title

                End If

                _codeExampleNumberHeaderList.Add(myHeader)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Assigns the header text should be used for the Code and Example Number datagrid column
    ''' </summary>
    ''' <param name="myTabIndex">Tab index selected. This is used to fetch the corresponding header.</param>
    ''' <remarks></remarks>
    Private Sub AssignCodeExampleNumberHeader(ByVal myTabIndex As Integer)
        Try
            Dim myHeader As String

            If _codeExampleNumberHeaderList Is Nothing Then Exit Sub

            myHeader = _codeExampleNumberHeaderList(myTabIndex)

            'Name header accordingly
            CodeExampleNumber.Header = myHeader
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    '=== Defaults
    ''' <summary>
    ''' Sets all button defaults and tooltips in the form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetButtons()
        SetButtonDefaults()
        SetButtonTooltips()
    End Sub

    ''' <summary>
    ''' Sets button default parameters, specified in the settings file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetButtonDefaults()
        Try
            If testerSettings.singleTab Then
                'Set 'all tabs' toggle buttons to be hidden
                chkBxRunAllTabs.Visibility = Windows.Visibility.Collapsed
                chkBxCompareAllTabs.Visibility = Windows.Visibility.Collapsed

                'Adjust menu display options
                ramMultiTab.IsEnabled = True
                ramSingleTab.IsEnabled = False

                'Adjust border around datagrid
                brdr_DG_ExampleSummary.BorderThickness = New Thickness(1, 1, 1, 1)
            Else
                'Set 'all tabs' toggle buttons to the appropriate state
                'Run selection
                If testerSettings.allTabsSelectRun Then
                    chkBxRunAllTabs.IsChecked = True
                    chkMenuItem_ApplyAllTabsRun.IsChecked = True
                Else
                    chkBxRunAllTabs.IsChecked = False
                    chkMenuItem_ApplyAllTabsRun.IsChecked = False
                End If

                'Compare selection
                If testerSettings.allTabsSelectCompare Then
                    chkBxCompareAllTabs.IsChecked = True
                    chkMenuItem_ApplyAllTabsCompare.IsChecked = True
                Else
                    chkBxCompareAllTabs.IsChecked = False
                    chkMenuItem_ApplyAllTabsCompare.IsChecked = False
                End If

                'Adjust menu display options
                ramMultiTab.IsEnabled = False
                ramSingleTab.IsEnabled = True

                'Adjust border around datagrid
                brdr_DG_ExampleSummary.BorderThickness = New Thickness(1, 0, 1, 1)
            End If

            'Delete analysis files
            chkMenuItem_StandardFiles.IsChecked = True
            If testerSettings.deleteAnalysisFilesStatus Then
                'btnDeleteAnalysisFilesToggle.IsChecked = True
                chkMenuItem_DeleteAfterRun.IsChecked = True
            Else
                'btnDeleteAnalysisFilesToggle.IsChecked = False
                chkMenuItem_DeleteAfterRun.IsChecked = False
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets tooltips for buttons programmatically. 
    ''' </summary>
    ''' <remarks>Phase out later and use binding.</remarks>
    Private Sub SetButtonTooltips()
        Try
            '=== Ribbon Application Menu
            ramSingleTab.ToolTipTitle = ttTTabsMerge
            ramSingleTab.ToolTipDescription = ttTabsMerge
            ramMultiTab.ToolTipTitle = ttTTabsSeparate
            ramMultiTab.ToolTipDescription = ttTabsSeparate
            'ramRestoreDefaults.ToolTipTitle = ttTResetDefaults
            'ramRestoreDefaults.ToolTipDescription = ttResetDefaults
            'ramResetDestination.ToolTipTitle = ttTReset
            'ramResetDestination.ToolTipDescription = ttReset
            ramSaveSettings.ToolTipTitle = ttTSave
            ramSaveSettings.ToolTipDescription = ttSave

            '=== Quick Access
            qatBtnSave.ToolTipTitle = ttTSave
            qatBtnSave.ToolTipDescription = ttSave
            qatResetDestination.ToolTipTitle = ttTReset
            qatResetDestination.ToolTipDescription = ttReset

            '=== Ribbon
            btnBCheck.ToolTipTitle = ttTCheck
            btnBCheck.ToolTipDescription = ttCheck

            btnBClear.ToolTipTitle = ttTClearResults
            btnBClear.ToolTipDescription = ttClearResults

            btnBrowseProgramBasic.ToolTipTitle = ttTBrowseProgram
            btnBrowseProgramBasic.ToolTipDescription = ttBrowseProgram
            btnBrowseSourceBasic.ToolTipTitle = ttTBrowseSource
            btnBrowseSourceBasic.ToolTipDescription = ttBrowseSource
            btnBrowseDestinationBasic.ToolTipTitle = ttTBrowseDestination
            btnBrowseDestinationBasic.ToolTipDescription = ttBrowseDestination

            btnRunAll.ToolTip = ttSelectRunAll
            btnRunNone.ToolTip = ttSelectRunNone
            chkBxRunAllTabs.ToolTip = ttSelectAllTabs

            btnCompareAll.ToolTip = ttSelectCompareAll
            btnCompareNone.ToolTip = ttSelectCompareNone
            chkBxCompareAllTabs.ToolTip = ttSelectAllTabs

            btnRunSelectionAdd.ToolTip = ttSelectRunAdd
            btnRunSelectionRemove.ToolTip = ttSelectRunRemove
            btnRunSelectionReplace.ToolTip = ttSelectRunReplace

            btnCompareSelectionAdd.ToolTip = ttSelectCompareAdd
            btnCompareSelectionRemove.ToolTip = ttSelectCompareRemove
            btnCompareSelectionReplace.ToolTip = ttSelectCompareReplace

            btnDeleteAnalysisFilesToggleSplit.ToolTipTitle = ttTDeleteRunFiles
            chkMenuItem_DeleteAfterRun.ToolTipTitle = ttTDeleteAfterRun
            chkMenuItem_StandardFiles.ToolTipTitle = ttTDeleteDefault
            chkMenuItem_LogWarningFiles.ToolTipTitle = ttTDeleteLogWarning
            chkMenuItem_ExportedTableFiles.ToolTipTitle = ttTDeleteExportedTables
            chkMenuItem_ModelTextFiles.ToolTipTitle = ttTDeleteModelText
            chkMenuItem_AllExceptModelFile.ToolTipTitle = ttTDeleteRunAll

            btnDeleteAnalysisFilesToggleSplit.ToolTipDescription = ttDeleteRunFiles
            chkMenuItem_DeleteAfterRun.ToolTipDescription = ttDeleteAfterRun
            chkMenuItem_StandardFiles.ToolTipDescription = ttDeleteDefault
            chkMenuItem_LogWarningFiles.ToolTipDescription = ttDeleteLogWarning
            chkMenuItem_ExportedTableFiles.ToolTipDescription = ttDeleteExportedTables
            chkMenuItem_ModelTextFiles.ToolTipDescription = ttDeleteModelText
            chkMenuItem_AllExceptModelFile.ToolTipDescription = ttDeleteRunAll

            radBtn_CommandLineRun.ToolTip = ttRunCommandLine
            radBtn_BatchRun.ToolTip = ttRunBatchFile

            cmbBox_Solver.ToolTip = ttSolver
            cmbBox_Process.ToolTip = ttProcess
            cmbBox_32Bit.ToolTip = ttBitMode


            '=== Datagrid Context Menu
            chkMenuItem_LinkRunCompare.ToolTip = ttSelectRunCompareLink

            chkMenuItem_RunAll.ToolTip = ttSelectRunAll
            chkMenuItem_RunNone.ToolTip = ttSelectRunNone
            chkMenuItem_ApplyAllTabsRun.ToolTip = ttSelectAllTabs
            chkMenuItem_RunSelectionAdd.ToolTip = ttSelectRunAdd
            chkMenuItem_RunSelectionRemove.ToolTip = ttSelectRunRemove
            chkMenuItem_RunSelectionReplace.ToolTip = ttSelectRunReplace

            chkMenuItem_CompareAll.ToolTip = ttSelectCompareAll
            chkMenuItem_CompareNone.ToolTip = ttSelectCompareNone
            chkMenuItem_ApplyAllTabsCompare.ToolTip = ttSelectAllTabs
            chkMenuItem_CompareSelectionAdd.ToolTip = ttSelectCompareAdd
            chkMenuItem_CompareSelectionRemove.ToolTip = ttSelectCompareRemove
            chkMenuItem_CompareSelectionReplace.ToolTip = ttSelectCompareReplace
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Determines which buttons to enable based on certain factors.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExampleSchemaValidationControls()
        If Not testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            btnVerifyExampleSchema.IsEnabled = False
            btnViewVerifiedExampleSchema.IsEnabled = False

            If Not testerSettings.initializeModelDestinationFolder Then
                Dim _validator As New cMCValidator(eSchemaValidate.ViewSchemaValidation, myRegTest)
                btnVerifyExampleSchema.IsEnabled = True
                If IO.File.Exists(_validator.SchemaValidationResultsLocation()) Then btnViewVerifiedExampleSchema.IsEnabled = True
            Else
                btnVerifyExampleSchema.IsEnabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Defaults to only be initialized once, on program load.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeReleaseDefaultsOnce()
        Try
            Select Case _programInitializer.csiTesterLevel
                Case eCSiTesterLevel.Published
                    ' Popup
                    ' Turn off cell coloring style
                    DirectCast(FindResource("cellColorRun"), Style).Triggers.Clear()
                    DirectCast(FindResource("cellColorCompare"), Style).Triggers.Clear()
                    DirectCast(FindResource("cellColorMaxResult"), Style).Triggers.Clear()
                    DirectCast(FindResource("cellColorResult"), Style).Triggers.Clear()
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets features of the program based on the release status.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeReleaseDefaults()
        Try
            'Apply global settings based on program level
            btnDeleteAnalysisFilesToggleSmall.Visibility = Windows.Visibility.Collapsed                  'Remove later?
            chkBxDeleteAnalysisFiles.Visibility = Windows.Visibility.Collapsed
            'btnDeleteAnalysisFilesToggle.Visibility = Windows.Visibility.Collapsed

            ramTab.Background = Brushes.Blue                                                'Change Ribbon Application Menu tab appearance
            ramTab.Width = 60
            rtBasicHeader.Foreground = Brushes.CornflowerBlue

            'Set the lock button image depending on if results exist. It is assumed that this is in sync with if they have been viewed in the program. 
            'TODO: Might need a separate trigger.
            With btnBClear
                If testerSettings.examplesComparedSaved.Count > 0 Then
                    .LargeImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
                    .SmallImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
                    chkMenuItem_Clear.ImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
                Else
                    .LargeImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                    .SmallImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                    chkMenuItem_Clear.ImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                End If
            End With

            Select Case _programInitializer.csiTesterLevel
                Case eCSiTesterLevel.Published
                    testerSettings.EnforceSetProgram(testerSettings.programName)

                    'Ribbon
                    rtSuiteSetup.Visibility = Windows.Visibility.Collapsed                          'Hide Ribbon Tabs

                    rbnGrpBClear.Header = "Clear"
                    btnBClear.Label = "Clear Results"
                    'btnDeleteAnalysisFilesToggle.Label = "Delete Run Files"

                    btnBrowseProgramBasic.Visibility = Windows.Visibility.Collapsed
                    btnBrowseSourceBasic.Visibility = Windows.Visibility.Collapsed

                    radBtn_CommandLineRun.Visibility = Windows.Visibility.Collapsed                 'Hide various ribbon controls
                    radBtn_BatchRun.Visibility = Windows.Visibility.Collapsed
                    gridSelectionCurrent.Visibility = Windows.Visibility.Collapsed
                    btnTableExportSettings.Visibility = Windows.Visibility.Collapsed

                    rbnGrpBMisc.Visibility = Windows.Visibility.Collapsed                           'Hide ribbon group
                    rbnGrpBRunOptions.Margin = New Thickness(0, 2, -5, 0)                           'Adjust next ribbon group margin to prevent last vertical divider from showing

                    rqaToolbar.Visibility = Windows.Visibility.Collapsed                            'Hide the Quick Access Toolbar

                    'Datagrid
                    Build.Visibility = Windows.Visibility.Collapsed                                 'Hide build column

                    'Old button layout
                    'lblProgramLocation.Visibility = Windows.Visibility.Collapsed                    'Hide 'browse' button for program.exe
                    'rbnGrpBPaths2.Visibility = Windows.Visibility.Collapsed

                Case eCSiTesterLevel.Internal
                    rtBasic.Visibility = Windows.Visibility.Collapsed                               'Hide Ribbon Tabs

                    'Example Schema Validation Buttons
                    InitializeExampleSchemaValidationControls()
                Case eCSiTesterLevel.Development
                    'suppressExStates = False

                    rbnGrpCheck.Visibility = Windows.Visibility.Collapsed
                    rbnGrpClear.Visibility = Windows.Visibility.Collapsed
                    rbnGrpProgram.Visibility = Windows.Visibility.Collapsed
                    rbnGrpModels.Visibility = Windows.Visibility.Collapsed

                    'Example Schema Validation Buttons
                    InitializeExampleSchemaValidationControls()
                Case Else
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Form Controls: Buttons"

    ''' <summary>
    ''' Runs the models and compares results, based on the selection.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCheck_Click(sender As Object, e As RoutedEventArgs) Handles btnCheck.Click, btnBCheck.Click, chkMenuItem_Check.Click
        If Not myCsiTester.checkRunOngoing Then
            Try
                'Setup up regTest
                myRegTest.SetTestRunResultsAction()

                'Save Settings of CSiTester.xml & regTest.xml
                SaveCSiTester()

                'If models are selected to be run, and the program path is not valid, prompt the user to specify a valid program path
                If Not ValidateProgram() Then Exit Sub

                'Validate the destination directory. If fails and actions allowed, correct folder. If failed and actions not allowed, stop run.
                If Not ValidateDestination() Then Exit Sub

                With myCsiTester
                    'Deletes possible existing analysis files for examples selected to run
                    '.DeleteAnalysisFilesRunExamples()

                    ''Reset the switch, and settings in regTest for the next run. Affects copying over of files from the tester source to tester destination directories.
                    '.ResetModelDestinationSwitches()

                    'Reset example status if currently set to exampleResultRunEmptyRunStatus = "Not Run Yet"
                    .ResetExampleResultStatus(True)

                    'Runs selected operations
                    .CheckController(PROCESS_REGTEST)
                End With

                'Set status of analysis files presence
                With testerSettings
                    'This is commented out as there can be cases where examplesRanSaved can be a mix of run files with and without the deleteAnalysisFiles trigger used
                    '   If testerSettings.deleteAnalysisFiles = True Then              
                    '       If testerSettings.examplesRanSaved.Count > 0 Then testerSettings.analysisFilesPresent = False
                    '   Else
                    If .examplesRanSaved.Count > 0 Then
                        .analysisFilesPresent = True
                    Else
                        .analysisFilesPresent = False
                    End If
                    '   End If
                End With

                'Reset the switch, and settings in regTest for the next run. Affects copying over of files from the tester source to tester destination directories.
                myCsiTester.ResetModelDestinationSwitches()

                'Handles overall run result behavior
                OverallRunSummaryActions()
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex))
            End Try
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_ILLEGAL_ACTION_DURING_CHECK,
                                                        _TITLE_ILLEGAL_ACTION_DURING_CHECK))
        End If
    End Sub

    ''' <summary>
    ''' Deletes all files in the models destination folder, leaving intact the "CSiTester' subfolder and all files contained within.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(sender As Object, e As RoutedEventArgs) Handles btnClear.Click, btnBClear.Click
        ClearSession()
    End Sub

    ''' <summary>
    ''' For ETABS only: Opens the form for enforcing using the outputSettings file for all models, and if so, allows the ability to enforce table set file output file types.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTableExportSettings_Click(sender As Object, e As RoutedEventArgs) Handles btnTableExportSettings.Click
        Dim windowSettingsTableExport As New frmSettingsTableExport
        windowSettingsTableExport.ShowDialog()
    End Sub

    ''' <summary>
    ''' Runs the XML Bulk editor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnXMLBulkEditor_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLBulkEditor.Click
        If Not LoadXMLEditor() Then Exit Sub

        myXMLEditor.MirrorAllEditorXMLS()

        'Add additional settings values to memory from the settings XML file
        testerSettings.InitializeExamplesObjects()

        'Create editor form
        windowXMLEditorBulk = New frmXMLEditorBulk
        windowXMLEditorBulk.DataContext = myXMLEditor.suiteEditorXMLObjects 'Passes relevant binding information to sub-class
        windowXMLEditorBulk.ShowDialog()

        'If path is unchanged from load, and XML files have been modified, update the main form view
        If (myXMLEditor.xmlEditorPath = myRegTest.models_database_directory.path AndAlso myXMLEditor.XMLChanged) Then
            RefreshForm()
        Else
            'Example Schema Validation Buttons
            InitializeExampleSchemaValidationControls()
        End If

        myXMLEditor.XMLChanged = False

    End Sub

    ''' <summary>
    ''' Opens a form where the user can delete many files and folders at once.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBulkDelete_Click(sender As Object, e As RoutedEventArgs) Handles btnBulkDelete.Click
        Dim deleteForm = New frmDeleteFilesFolders
        deleteForm.Show()
    End Sub

    'TODO: Displays the batch file generated in the batch file run mode.
    ''' <summary>
    ''' Displays the batch file generated in the batch file run mode.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button_Click_ViewBatchFile(sender As Object, e As RoutedEventArgs) Handles btnViewBatchFile.Click

    End Sub

    '=== Buttons: Setting Paths
    ''' <summary>
    ''' Allows user to set path for executable to be run. Program automatically updates CSi program name based on executable chosen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseProgram_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseProgramBasic.Click, btnBrowseProgram.Click
        Dim oldPath As String = myRegTest.program_file.path

        myCsiTester.BrowseProgram()

        ProgramPathActions(oldPath)
    End Sub

    ''' <summary>
    ''' Allows user to set path for models directory from where models are copied from for the run
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseSource_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseSource.Click, btnBrowseSourceBasic.Click, chkMenuItem_BrowseSource.Click 'Handles btnBrowseSource.Click
        Dim updateCSiTester As Boolean = False

        myCsiTester.BrowseModelSource(updateCSiTester)

        SourcePathActions(updateCSiTester)
    End Sub

    ''' <summary>
    ''' Allows user to set path for models directory where models are copied to for the run
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseDestination_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseDestinationBasic.Click, chkMenuItem_BrowseDestination.Click 'Handles btnBrowseDestinationBasic.Click, btnBrowseDestination.Click
        Dim updateCSiTester As Boolean = False

        myCsiTester.BrowseModelDestination(updateCSiTester)

        DestinationPathActions(updateCSiTester)
    End Sub

    '=== Misc Testing for Development
    Private Sub Button_Click_Test(sender As Object, e As RoutedEventArgs)



        'windowXMLEditorBulk = New frmXMLEditorBulk
        'windowXMLEditorBulk.DataContext = myCsiTester.suiteEditorXMLObjects 'Passes relevant binding information to sub-class
        'windowXMLEditorBulk.ShowDialog()

        'Read_XML_Elements_All("C:\MPT-CSI\DevelTFS\Regression Tester\Main\CSiTester\CSiTester\bin\Debug\models\Verification\Analysis Examples\EX1.xml")


        'Also, order of entries is that of the rows selected, rather than displayed
        'For Each exampleRow As cExample In dataGrid_ExampleSummary.SelectedItems
        '    MsgBox(exampleRow.numberCodeExample)
        'Next


        'Selecting an individual cell: http://stackoverflow.com/questions/8013213/get-cell-value-from-wpf-datagrid-in-vb
        'Dim dt As New System.Data.DataTable

        'For Each dtRow As System.Data.DataRow In dt.Rows

        'Next

        dataGrid_ExampleSummary.Focus()

        Dim modelIDsList As New ObservableCollection(Of String)
        modelIDsList.Add("180")
        modelIDsList.Add("181.02")
        modelIDsList.Add("249")
        modelIDsList.Add("151")
        modelIDsList.Add("184.01")
        'SelectRowsAllTabs(modelIDsList, btnRunSelectionAdd)
        myTabControlSummary.SelectedIndex = 1
        SelectRows(examplesTestSetList(1), modelIDsList, btnRunSelectionAdd)
        'dataGrid_ExampleSummary.UpdateLayout()
        'System.Threading.Thread.Sleep(1000)
        myTabControlSummary.SelectedIndex = 2
        SelectRows(examplesTestSetList(2), modelIDsList, btnRunSelectionAdd)
        'dataGrid_ExampleSummary.UpdateLayout()
        'System.Threading.Thread.Sleep(1000)
        myTabControlSummary.SelectedIndex = 0
        SelectRows(examplesTestSetList(0), modelIDsList, btnRunSelectionAdd)
        'System.Threading.Thread.Sleep(1000)


        ''Select rows
        'Dim rowIndices As New List(Of Integer)
        'rowIndices.Add(1)
        'rowIndices.Add(3)
        'rowIndices.Add(7)
        'rowIndices.Add(8)

        'SelectRowByIndices(dataGrid_ExampleSummary, rowIndices)

        ''Select cells
        'Dim cellIndices As New Dictionary(Of Integer, Integer)
        'cellIndices.Add(9, 5)
        'cellIndices.Add(11, 4)
        'cellIndices.Add(12, 6)

        'SelectCellByIndices(dataGrid_ExampleSummary, cellIndices)


        'Apply button action
        e2eTester = New cE2ETester
        'If  btnRunSelectionAdd IsNot Nothing  Then e2eTester.ButtonClick(btnRunSelectionAdd)
        'e2eTester.TabClick(CType(myTabControlSummary.Items(1), TabItem), myTabControlSummary)
        'myTabControlSummary.SelectedIndex = 1
    End Sub

    ''' <summary>
    ''' Updates the assumed run time of the selected examples to the actual run time of the results, multiplied by a specified factor to account for differences in computer speeds.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdateTimes_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateTimes.Click
        Dim modelFilePathsString As String = ""

        SaveCSiTester()
        If myCsiTester.exampleCompareCollection.Count = 0 Then Exit Sub

        Dim windowTimeFactor As New frmTimeFactor(ConvertTimesNumberMinute(myCsiTester.exampleCompareCollection(0).timeRunActual), 1.5, True)
        windowTimeFactor.ShowDialog()

        'Check if form was canceled
        If windowTimeFactor.formCanceled Then Exit Sub

        'Get list of examples to be updated
        For Each example As cExample In myCsiTester.exampleCompareCollection
            modelFilePathsString = modelFilePathsString & example.pathXmlMC & Environment.NewLine & Environment.NewLine
        Next

        Select Case csiMessageBox.Show(eMessageActionSets.YesNo, _TITLE_MC_SOURCE_UPDATE, _PROMPT_MC_SOURCE_UPDATE_WARNING, _PROMPT_MC_SOURCE_UPDATE_QUESTION, modelFilePathsString, MessageBoxImage.Warning)
            Case eMessageActions.Yes
                'TODO: Status bar here in the future
                'Set up wait cursor
                Dim cursorWait As New cCursorWait

                For Each example As cExample In myCsiTester.exampleCompareCollection
                    example.UpdateTimes(CStr(ConvertTimesStringMinutes(ConvertTimesNumberMinute(example.timeRunActual) * windowTimeFactor.timeFactor)))

                    'Refresh Example as it is read into the suite
                    example.InitializeExampleData()

                    'Copy currently updated example from source to destination
                    CopyFile(example.pathXmlMC, myCsiTester.ConvertPathModelSourceToDestination(example.pathXmlMC), True)
                Next

                'Set up wait cursor
                cursorWait.EndCursor()

                'Updates compared examples
                myCsiTester.CheckCompareOnly()
            Case eMessageActions.No
        End Select
    End Sub

    ''' <summary>
    ''' Updates the benchmarks of the selected examples to the current results, both in the program view and in the model control XML files.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUpdateBenchmarks_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateBenchmarks.Click
        Dim modelFilePathsString As String = ""
        Dim ticketsCount As Integer

        SaveCSiTester()
        If myCsiTester.exampleCompareCollection.Count = 0 Then Exit Sub

        'Create update object for first example selected for compare
        Dim windowXMLNodeCreateObject As New frmXMLObjectCreateItem(eXMLObjectType.Update, myCsiTester.exampleCompareCollection(0).myMCModel)
        windowXMLNodeCreateObject.ShowDialog()

        'Check if form was canceled
        If windowXMLNodeCreateObject.formCanceled Then Exit Sub

        'Get list of examples to be updated
        For Each example As cExample In myCsiTester.exampleCompareCollection
            modelFilePathsString = modelFilePathsString & example.pathXmlMC & Environment.NewLine & Environment.NewLine
        Next

        Select Case csiMessageBox.Show(eMessageActionSets.YesNo, _TITLE_MC_SOURCE_UPDATE, _PROMPT_MC_SOURCE_UPDATE_WARNING, _PROMPT_MC_SOURCE_UPDATE_QUESTION, modelFilePathsString, MessageBoxImage.Warning)
            Case eMessageActions.Yes
                'TODO: Status bar here in the future
                'Set up wait cursor
                Dim cursorWait As New cCursorWait

                For Each example As cExample In myCsiTester.exampleCompareCollection
                    'Updates the class with the added 'update' & 'ticket' objects (i.e. the last ones in the list for the first example where they are added)
                    example.myMCModel.AddUpdate(CType(windowXMLNodeCreateObject.myMCModelSave.updates(windowXMLNodeCreateObject.myMCModelSave.updates.Count - 1).Clone, cMCUpdate))

                    'Ticket object might not be added, if the ticket is a number of 0
                    If example.myMCModel.tickets IsNot Nothing Then ticketsCount = example.myMCModel.tickets.Count
                    If windowXMLNodeCreateObject.myMCModelSave.tickets IsNot Nothing Then
                        If windowXMLNodeCreateObject.myMCModelSave.tickets.Count > ticketsCount Then example.myMCModel.tickets.Add(windowXMLNodeCreateObject.myMCModelSave.tickets(windowXMLNodeCreateObject.myMCModelSave.tickets.Count - 1))
                    End If

                    'Updates XML file
                    example.UpdateBenchmarks(True)

                    'Refresh Example as it is read into the suite
                    example.InitializeExampleData()

                    'Copy currently updated example from source to destination
                    CopyFile(example.pathXmlMC, myCsiTester.ConvertPathModelSourceToDestination(example.pathXmlMC), True)
                Next

                'Run regTest results update on the current example
                myCsiTester.UpdateModelResults(False, testerSettings.examplesCompareSaved.ToList)

                'Causes program to wait until regTest is done
                System.Threading.Thread.Sleep(1000)
                While ProcessIsRunning(PROCESS_REGTEST)
                    System.Threading.Thread.Sleep(500)
                End While

                'Set up wait cursor
                cursorWait.EndCursor()

                'Updates compared examples
                myCsiTester.CheckCompareOnly()
            Case eMessageActions.No
        End Select
    End Sub

    ''' <summary>
    ''' Runs a schema validation test of all destination examples in CSiTester using regTest, and displays the results.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnVerifyExamplesSchema_Click(sender As Object, e As RoutedEventArgs) Handles btnVerifyExampleSchema.Click
        Dim windowXMLModelValidate As New frmMCValidate(eSchemaValidate.RunSchemaValidation)

        windowXMLModelValidate.ShowDialog()
    End Sub

    ''' <summary>
    ''' Views the results from the last example schema validation test of the destination examples run by regTest.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnViewVerifiedExampleSchema_Click(sender As Object, e As RoutedEventArgs) Handles btnViewVerifiedExampleSchema.Click
        Dim windowXMLModelValidate As New frmMCValidate(eSchemaValidate.ViewSchemaValidation)

        windowXMLModelValidate.ShowDialog()
    End Sub

    ''' <summary>
    ''' Makes a new directory to be used as source models, using the models from the destination directory, and MC XMl files from the source directory.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewSourceModels_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewSourceModels.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.CreateNewModelSourceFromDestination)
        windowFileFolderOperations.ShowDialog()
    End Sub

    ''' <summary>
    ''' Loads a form for running end-to-end tests.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnE2ETest_Click(sender As Object, e As RoutedEventArgs) Handles btnE2ETest.Click
        'Load status form and new threads
        myE2EForm = Nothing
        myE2EForm = New frmTestingE2E
        myE2EForm.Show()
    End Sub
#End Region

#Region "Form Controls: Example Selection (Buttons & Toggles)"
    '=== Button Selection Options for Run
    Private Sub btnRunAll_Click(sender As Object, e As RoutedEventArgs) Handles btnRunAll.Click
        RunAll()
        If _syncRunCompareSelections Then CompareAll()
    End Sub
    Private Sub btnRunNone_Click(sender As Object, e As RoutedEventArgs) Handles btnRunNone.Click
        RunNone()
        If _syncRunCompareSelections Then CompareNone()
    End Sub
    Private Sub btnRunSelectionAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionAdd.Click
        SelectionAddRemoveReplace(eCheckType.Run, eCellSelectOperation.Add)
    End Sub
    Private Sub btnRunSelectionRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionRemove.Click
        SelectionAddRemoveReplace(eCheckType.Run, eCellSelectOperation.Remove)
    End Sub
    Private Sub btnRunSelectionReplace_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionReplace.Click
        SelectionAddRemoveReplace(eCheckType.Run, eCellSelectOperation.Replace)
    End Sub

    '=== Button Selection Options for Compare
    Private Sub btnCompareAll_Click(sender As Object, e As RoutedEventArgs) Handles btnCompareAll.Click
        CompareAll()
        If _syncRunCompareSelections Then RunAll()
    End Sub
    Private Sub btnCompareNone_Click(sender As Object, e As RoutedEventArgs) Handles btnCompareNone.Click
        CompareNone()
        If _syncRunCompareSelections Then RunNone()
    End Sub
    Private Sub btnCompareSelectionAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnCompareSelectionAdd.Click
        SelectionAddRemoveReplace(eCheckType.Compare, eCellSelectOperation.Add)
    End Sub
    Private Sub btnCompareSelectionRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnCompareSelectionRemove.Click
        SelectionAddRemoveReplace(eCheckType.Compare, eCellSelectOperation.Remove)
    End Sub
    Private Sub btnCompareSelectionReplace_Click(sender As Object, e As RoutedEventArgs) Handles btnCompareSelectionReplace.Click
        SelectionAddRemoveReplace(eCheckType.Compare, eCellSelectOperation.Replace)
    End Sub

    '=== Toggle Button - Sets whether run status operations apply to all tabs or only the visible tab
    Private Sub chkBxRunAllTabs_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAllTabs.Checked
        testerSettings.allTabsSelectRun = True
        chkMenuItem_ApplyAllTabsRun.IsChecked = True
    End Sub
    Private Sub chkBxRunAllTabs_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxRunAllTabs.Unchecked
        testerSettings.allTabsSelectRun = False
        chkMenuItem_ApplyAllTabsRun.IsChecked = False
    End Sub

    '====Toggle Button -  Sets whether compare status operations apply to all tabs or only the visible tab
    Private Sub chkBxCompareAllTabs_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxCompareAllTabs.Checked
        testerSettings.allTabsSelectCompare = True
        chkMenuItem_ApplyAllTabsCompare.IsChecked = True
    End Sub
    Private Sub chkBxCompareAllTabs_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBxCompareAllTabs.Unchecked
        testerSettings.allTabsSelectCompare = False
        chkMenuItem_ApplyAllTabsCompare.IsChecked = False
    End Sub

    '=== Currently only called by context menu. No buttons yet exist
    ''' <summary>
    ''' Links run and compare selections, such that both are selected/unselected for a given row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLinkSelection_Checked(sender As Object, e As RoutedEventArgs)
        _syncRunCompareSelections = True
    End Sub
    ''' <summary>
    ''' Links run and compare selections, such that both are selected/unselected for a given row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLinkSelection_Unchecked(sender As Object, e As RoutedEventArgs)
        _syncRunCompareSelections = False
    End Sub
#End Region

#Region "Form Controls: Run Options (Radio Buttons, Combo Boxes, Toggles)"

    '=== Radio Buttons - Command Line vs. Batch File Run Options
    ''' <summary>
    ''' Run will be done one instance of the program for each model, via the calls from the command line
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radBtn_CommandLineRun_Checked(sender As Object, e As RoutedEventArgs) Handles radBtn_CommandLineRun.Checked
        If radBtn_CommandLineRun.IsChecked Then
            myRegTest.run_local_test = True
            myRegTest.run_using_batch_file = False
        End If

    End Sub

    ''' <summary>
    ''' Run will be done on one instance of the program for all models, via a batch file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radBtn_BatchRun_Checked(sender As Object, e As RoutedEventArgs) Handles radBtn_BatchRun.Checked
        If radBtn_BatchRun.IsChecked Then
            myRegTest.run_local_test = False
            myRegTest.run_using_batch_file = True
        End If
    End Sub

    '=== Combo Boxes - Solver Parameter
    ''' <summary>
    ''' Sets the analysis solver parameter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbBox_Solver_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_Solver.SelectionChanged
        testerSettings.solverSaved = cmbBox_Solver.Text
    End Sub

    '=== Combo Boxes - Process Parameter
    ''' <summary>
    ''' Sets the analysis process parameter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbBox_Process_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_Process.SelectionChanged
        testerSettings.processSaved = cmbBox_Process.Text
    End Sub

    '=== Combo Boxes - Bit Parameter
    ''' <summary>
    ''' Sets the 32/64 bit run parameter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbBox_32Bit_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBox_32Bit.SelectionChanged
        testerSettings.bitTypeSaved = cmbBox_32Bit.Text
    End Sub

    '=== Toggle Buttons
    ''' <summary>
    ''' Analysis files will be deleted after run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkBxDeleteAnalysisFiles_Checked(sender As Object, e As RoutedEventArgs) Handles chkBxDeleteAnalysisFiles.Checked
        testerSettings.deleteAnalysisFilesStatus = True
    End Sub
    ''' <summary>
    ''' Analysis files will not be deleted after run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkBxDeleteAnalysisFiles_UnChecked(sender As Object, e As RoutedEventArgs) Handles chkBxDeleteAnalysisFiles.Unchecked
        testerSettings.deleteAnalysisFilesStatus = False
    End Sub
    ''' <summary>
    ''' Deletes analysis files. Also deletes analysis files after every run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDeleteAnalysisFilesToggle_Checked(sender As Object, e As RoutedEventArgs) Handles btnDeleteAnalysisFilesToggleSmall.Checked, chkMenuItem_DeleteAfterRun.Checked ',btnDeleteAnalysisFilesToggle.Checked
        testerSettings.deleteAnalysisFilesStatus = True
        'DeleteAnalysisFiles()
    End Sub
    ''' <summary>
    ''' Analysis files will not be deleted after run.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDeleteAnalysisFilesToggle_Unchecked(sender As Object, e As RoutedEventArgs) Handles btnDeleteAnalysisFilesToggleSmall.Unchecked, chkMenuItem_DeleteAfterRun.Unchecked ', btnDeleteAnalysisFilesToggle.Unchecked
        testerSettings.deleteAnalysisFilesStatus = False
    End Sub

    Private Sub btnDeleteAnalysisFilesToggleSplit_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteAnalysisFilesToggleSplit.Click
        DeleteAnalysisFiles()
    End Sub

    Private Sub chkMenuItem_DeleteAfterRun_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_DeleteAfterRun.Checked
        testerSettings.deleteAnalysisFilesStatus = True
    End Sub
    Private Sub chkMenuItem_DeleteAfterRun_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_DeleteAfterRun.Unchecked
        testerSettings.deleteAnalysisFilesStatus = False
    End Sub

    Private Sub chkMenuItem_StandardFiles_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_StandardFiles.Checked
        'Standard is always checked.
    End Sub
    Private Sub chkMenuItem_StandardFiles_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_StandardFiles.Unchecked
        'Standard is always checked.
    End Sub

    Private Sub chkMenuItem_LogWarningFiles_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_LogWarningFiles.Checked
        testerSettings.deleteAnalysisFilesLogWarning = True
    End Sub
    Private Sub chkMenuItem_LogWarningFiles_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_LogWarningFiles.Unchecked
        testerSettings.deleteAnalysisFilesLogWarning = False
    End Sub

    Private Sub chkMenuItem_ExportedTableFiles_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_ExportedTableFiles.Checked
        testerSettings.deleteAnalysisFilesTables = True
    End Sub
    Private Sub chkMenuItem_ExportedTableFiles_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_ExportedTableFiles.Unchecked
        testerSettings.deleteAnalysisFilesTables = False
    End Sub

    Private Sub chkMenuItem_ModelTextFiles_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_ModelTextFiles.Checked
        testerSettings.deleteAnalysisFilesModelText = True
    End Sub
    Private Sub chkMenuItem_ModelTextFiles_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_ModelTextFiles.Unchecked
        testerSettings.deleteAnalysisFilesModelText = False
    End Sub

    Private Sub chkMenuItem_AllExceptModelFile_Checked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_AllExceptModelFile.Checked
        chkMenuItem_LogWarningFiles.IsChecked = True
        chkMenuItem_ExportedTableFiles.IsChecked = True
        chkMenuItem_AllExceptModelFile.IsChecked = True
        chkMenuItem_ModelTextFiles.IsChecked = True

        'Other Action
        With testerSettings
            .deleteAnalysisFilesLogWarning = True
            .deleteAnalysisFilesTables = True
            .deleteAnalysisFilesModelText = True
            .deleteAnalysisFilesAll = True
        End With
    End Sub
    Private Sub chkMenuItem_AllExceptModelFile_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_AllExceptModelFile.Unchecked
        chkMenuItem_LogWarningFiles.IsChecked = False
        chkMenuItem_ExportedTableFiles.IsChecked = False
        chkMenuItem_AllExceptModelFile.IsChecked = False
        chkMenuItem_ModelTextFiles.IsChecked = False

        'Other Action
        With testerSettings
            .deleteAnalysisFilesLogWarning = False
            .deleteAnalysisFilesTables = False
            .deleteAnalysisFilesModelText = False
            .deleteAnalysisFilesAll = False
        End With
    End Sub

    'TODO: add to feature. Currently not accessible from GUI
    ''' <summary>
    ''' Resets the combo box selection to the default settings.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultAnalysisParameters()
        'Converts generic setting term to the term used for the program
        testerSettings.ConvertAnalysisDefaults()

        SetComboBoxSelection(cmbBox_Solver, testerSettings.solverSaved)
        SetComboBoxSelection(cmbBox_Process, testerSettings.processSaved)
        SetComboBoxSelection(cmbBox_32Bit, testerSettings.bitTypeSaved)
    End Sub
#End Region

#Region "Form Controls: Ribbon Application Menu (RAM) Dropdown"
    '=== These are the menu items available on the dropdown 'file' menu on the ribbon
    ''' <summary>
    ''' Displays examples on a single tab, regardless of Classification 2, and updates GUI controls accordingly.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ramSingleTab_Clicked(sender As Object, e As RoutedEventArgs) Handles ramSingleTab.Click
        SetSingleTab()
    End Sub

    ''' <summary>
    ''' Displays examples multiple tabs grouped by Classification 2, and updates GUI controls accordingly.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ramMultiTab_Clicked(sender As Object, e As RoutedEventArgs) Handles ramMultiTab.Click
        SetMultiTab()
    End Sub

    ''' <summary>
    ''' Copies fresh files from the installation folder to the destination folder location and reinitializes the program with the newly copied files.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ramRestoreDefaults_Click(sender As Object, e As RoutedEventArgs) Handles chkMenuItem_RestoreDefaultSettings.Click ',ramRestoreDefaults.Click
        RestoreDefaults()
    End Sub

    ''' <summary>
    ''' Resets the entire session, such as deleting all files and folders within the destination directory, and copying over new files and regenerating classes and views within the program.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ramResetDestination_Click(sender As Object, e As RoutedEventArgs) Handles qatResetDestination.Click, chkMenuItem_ResetSettings.Click ',ramResetDestination.Click
        If Not myCsiTester.checkRunOngoing Then
            ResetSession()
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_ILLEGAL_ACTION_DURING_CHECK,
                                                        _TITLE_ILLEGAL_ACTION_DURING_CHECK))
        End If
    End Sub

    ''' <summary>
    ''' Saves settings back into target XML file
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ramSaveSettings_Click(sender As Object, e As RoutedEventArgs) Handles ramSaveSettings.Click, qatBtnSave.Click
        SaveCSiTester()
    End Sub

    ''' <summary>
    ''' Closes application
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Currently only closes starting window. Other windows will stay open</remarks>
    Private Sub ramExit_Click(sender As Object, e As RoutedEventArgs) Handles ramExit.Click
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Question),
                                            "Do you wish to save your settings and results?",
                                            "Exiting Program")
            Case eMessageActions.Yes
                SaveCSiTester()
                Me.Close()
            Case eMessageActions.No
                Me.Close()
            Case eMessageActions.Cancel
        End Select
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

        'Any open modeless forms, such as Example forms, should be closed.
        For Each currentWindow As Window In System.Windows.Application.Current.Windows
            If Not currentWindow Is Me Then currentWindow.Close()
        Next
    End Sub
#End Region

#Region "Methods: Updating Classes & Forms from Paths"
    ''' <summary>
    ''' Prompts the user if the run ended unexpectedly. Otherwise, the lock icon is changed to being 'locked'. Also handles 'Running' run status.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OverallRunSummaryActions()
        Try
            'Remove 'Running' status
            myCsiTester.ResetRunStatus()

            'Handle run failures, or otherwise, update buttons
            If myCsiTester.checkRunError Then
                EndProcess(PROCESS_REGTEST)
                If myStatusForm IsNot Nothing Then myStatusForm.Close() 'Closes the status form thread to avoid redundant error messages as that form fails to work with RegTest.
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Error),
                                            _PROMPT_CHECK_RUN_ERROR,
                                            _TITLE_RUN_AND_COMPARE_FAILED)
                    Case eMessageActions.Yes
                        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                            'Clears model results as well
                            ResetSession()
                        Else
                            'Preserves current run results and only replaces all files in the CSiTester folder
                            RestoreDefaults()
                        End If
                    Case eMessageActions.No
                End Select
            ElseIf myCsiTester.regTestFail Then
                    EndProcess(PROCESS_REGTEST)
                    If myStatusForm IsNot Nothing Then myStatusForm.Close() 'Closes the status form thread to avoid redundant error messages as that form fails to work with RegTest.
                    If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then
                    Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Error),
                                        _PROMPT_REGTEST_FAILED,
                                        _TITLE_RUN_AND_COMPARE_FAILED)
                        Case eMessageActions.Yes
                            If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                                'Clears model results as well
                                ResetSession()
                            Else
                                'Preserves current run results and only replaces all files in the CSiTester folder. This path is never called.
                                RestoreDefaults()
                            End If
                        Case eMessageActions.No
                    End Select
                    Else
                        RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), _PROMPT_REGTEST_FAILED_GENERAL))
                    End If
            Else
                    btnBClear.LargeImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
                    btnBClear.SmallImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
                    chkMenuItem_Clear.ImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_READY, UriKind.Relative))
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub


    ''' <summary>
    ''' Gets the title of the form based on the program name property of the regTest class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFormTitle() As String
        Return _FORM_TITLE_PREFIX & myRegTest.program_name & _FORM_TITLE_SUFFIX
    End Function

    ''' <summary>
    ''' Updates data in the GUI related to the program path specified.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateProgramData()
        'Set up wait cursor
        Dim cursorWait As New cCursorWait

        myCsiTester.UpdateProgramData()

        Me.Title = GetFormTitle()

        'Sets tooltip to include path, in case path is too long to display on the button
        RefreshBtnBrowseProgram()

        'Update Analysis Settings Controls and select defaults to start
        InitializeAnalysisSettings()
        SetDefaultAnalysisParameters()

        'Set up wait cursor
        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Updates form if the program path has changed. Else, only ensures button is up to date.
    ''' </summary>
    ''' <param name="p_oldPath">Old path to compare to the current path.</param>
    ''' <remarks></remarks>
    Private Sub ProgramPathActions(ByVal p_oldPath As String)
        If Not StringsMatch(p_oldPath, myRegTest.program_file.path) Then
            myRegTest.SetVersionBuild()
            UpdateProgramData()
            SaveCSiTester(True)
        Else
            RefreshBtnBrowseProgram()
        End If
    End Sub

    ''' <summary>
    ''' Updates form if the source has been re-specified. 
    ''' </summary>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <remarks></remarks>
    Private Sub SourcePathActions(Optional ByVal p_updateCSiTester As Boolean = False)
        If p_updateCSiTester Then
            testerSettings.NewSourceDestination()
            UpdateModelSourceData()
            SaveCSiTester()
        End If
    End Sub

    ''' <summary>
    ''' Updates destination directory properties. 
    ''' Updates form if the source has been re-specified.
    ''' </summary>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <remarks></remarks>
    Private Sub DestinationPathActions(Optional ByVal p_updateCSiTester As Boolean = False)
        _programInitializer.testerDestinationDir = myCsiTester.testerDestinationDir
        testerSettings.testerDestination.SetProperties(myCsiTester.testerDestinationDir, DIR_TESTER_DESTINATION_DIR_DEFAULT)

        If p_updateCSiTester Then
            testerSettings.NewSourceDestination()
            UpdateModelDestinationData()
            SaveCSiTester()
        End If
    End Sub

    ''' <summary>
    ''' Updates data in the GUI related to the models source specified
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateModelSourceData()
        'Set up wait cursor
        Dim cursorWait As New cCursorWait

        'Updates
        btnBrowseSource.Content = myCsiTester.pathGlobal

        'Sets tooltip to include path, in case path is too long to display on the button
        btnBrowseSource.ToolTip = GetPrefix(ttBrowseSource, ".") & ": " & Environment.NewLine & myRegTest.models_database_directory.path
        btnBrowseSourceBasic.ToolTipDescription = GetPrefix(ttBrowseSource, ".") & ": " & Environment.NewLine & myRegTest.models_database_directory.path

        RefreshForm()
        dataGrid_ExampleSummary.CommitEdit()
        dataGrid_ExampleSummary.CancelEdit()
        dataGrid_ExampleSummary.Items.Refresh()

        'Set up wait cursor
        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Updates data in the GUI related to the models destination specified
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateModelDestinationData()
        'Set up wait cursor
        Dim cursorWait As New cCursorWait

        'Updates
        btnBrowseDestination.Content = myCsiTester.pathGlobal

        'Sets tooltip to include path, in case path is too long to display on the button
        btnBrowseDestination.ToolTip = GetPrefix(ttBrowseDestination, ".") & ": " & Environment.NewLine & _programInitializer.testerDestinationDir
        btnBrowseDestinationBasic.ToolTipDescription = GetPrefix(ttBrowseDestination, ".") & ": " & Environment.NewLine & _programInitializer.testerDestinationDir

        'Example Schema Validation Buttons
        InitializeExampleSchemaValidationControls()

        'Clear Failed Examples
        RemoveTabFailedExamples()

        'Set up wait cursor
        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Copies fresh files from the installation folder to the destination folder location and reinitializes the program with the newly copied files.
    ''' </summary>
    ''' <param name="p_allowRefreshForm">Optional: If False, the refreshing actions of the function will not occur.</param>
    ''' <remarks></remarks>
    Private Sub RestoreDefaults(Optional ByVal p_allowRefreshForm As Boolean = True)
        Dim modelsRunDirectoryNew As String
        Dim modelsOutputPath As String

        If Not myCsiTester.checkRunOngoing Then
            'TODO: Status bar here in the future
            'Set up wait cursor
            Dim cursorWait As New cCursorWait

            If myRegTest.autoFolders Then
                modelsRunDirectoryNew = _programInitializer.testerDestinationDir & "\" & myRegTest.test_id
            Else
                modelsRunDirectoryNew = _programInitializer.testerDestinationDir & "\" & DIR_NAME_MODELS_DESTINATION
            End If
            modelsOutputPath = _programInitializer.testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION

            myCsiTester.InitializeCSiTesterSettings(_programInitializer.testerDestinationDir, False, True, True)

            'Somewhat redundant to the last function call, but this replaces all of the regTest files for running regtest.exe
            myCsiTester.InitializeRegTest(_programInitializer.testerDestinationDir, modelsRunDirectoryNew, modelsOutputPath, False, True, True)

            If p_allowRefreshForm Then
                RefreshForm(True)
                SetButtonDefaults()
                RefreshBtnBrowseProgram()
            End If

            'Set up wait cursor
            cursorWait.EndCursor()
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_ILLEGAL_ACTION_DURING_CHECK,
                                                        _TITLE_ILLEGAL_ACTION_DURING_CHECK))
        End If
    End Sub
#End Region

#Region "Methods: Supporting"

    ''' <summary>
    ''' If models are selected to be run, and the program path is not valid, prompt the user to specify a valid program path
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateProgram() As Boolean
        Dim programDataUpdate As Boolean
        ValidateProgram = myCsiTester.ValidateProgram(programDataUpdate)

        If programDataUpdate Then
            UpdateProgramData()
            SaveCSiTester(False)
        End If
    End Function

    ''' <summary>
    ''' Performs various validation checks of the destination directory. If checks fail, the directory is either re-initialized or the run is aborted, depending on the condition and user selections to prompts.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateDestination() As Boolean
        Dim saveCurrentCSiTester As Boolean

        ValidateDestination = myCsiTester.ValidateDestination(_programInitializer.csiTesterLevel, _programInitializer.testerDestinationDir, saveCurrentCSiTester)

        If saveCurrentCSiTester Then SaveCSiTester(False)
    End Function

    ''' <summary>
    ''' Performs all of the operations necessary to update and save CSiTester into the relevant XML files and update the form.
    ''' </summary>
    ''' <param name="updateCollectionsCommandLines">True: The collections of selected examples will be re-created, and the command line analysis settins will be updated.</param>
    ''' <remarks></remarks>
    Friend Sub SaveCSiTester(Optional updateCollectionsCommandLines As Boolean = True)
        Dim updateCSiTester As Boolean

        myCsiTester.SaveCSiTester(updateCSiTester, _programInitializer.testerDestinationDir, updateCollectionsCommandLines)

        If updateCSiTester Then
            Dim cursorWait As New cCursorWait

            UpdateModelDestinationData()
            RefreshForm(True)

            cursorWait.EndCursor()
        End If
    End Sub

    ''' <summary>
    ''' Resets the entire session, such as deleting all files and folders within the destination directory, and copying over new files and regenerating classes and views within the program.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetSession()
        If Not myCsiTester.checkRunOngoing Then
            'Dim runDirectory As String = FilterStringFromName(regTest.models_run_directory, "\" & GetSuffix(regTest.models_run_directory, "\"), True, False)
            'myCsiTester.ClearDestinationFolder()
            'RestoreDefaults()

            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Exclamation),
                                            _PROMPT_RESET_SESSION,
                                            _TITLE_RESET_SESSION)
                Case eMessageActions.Yes
                    'TODO: Status bar here in the future
                    'Set up wait cursor
                    Dim cursorWait As New cCursorWait

                    If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then
                        'Commented in case runDirectory is needed
                        'myCsiTester.InitializeRunningDirectory(runDirectory, False, True, True)
                        myCsiTester.InitializeRunningDirectory(_programInitializer.testerDestinationDir, False, True, True)
                        btnBClear.LargeImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                        btnBClear.SmallImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                        chkMenuItem_Clear.ImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
                    Else
                        ClearSession(False)
                        RestoreDefaults(False)
                    End If

                    RefreshForm(True)
                    SetButtonDefaults()
                    RefreshBtnBrowseProgram()

                    'Set up wait cursor
                    cursorWait.EndCursor()
                Case eMessageActions.No

            End Select
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_ILLEGAL_ACTION_DURING_CHECK,
                                                        _TITLE_ILLEGAL_ACTION_DURING_CHECK))
        End If
    End Sub

    ''' <summary>
    ''' Deletes analysis files. Also deletes analysis files after every run.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAnalysisFiles()
        If (Not _frmInitialize) Then ' AndAlso testerSettings.analysisFilesPresent) Then

            Dim windowDeleteFiles As New frmDeleteFiles

            windowDeleteFiles.ShowDialog()

        End If
    End Sub

    ''' <summary>
    ''' Deletes all files in the models destination folder, leaving intact the "CSiTester' subfolder and all files contained within.
    ''' </summary>
    ''' <param name="allowRefreshForm">Optional: If False, the refreshing actions of the function will not occur.</param>
    ''' <remarks></remarks>
    Private Sub ClearSession(Optional ByVal allowRefreshForm As Boolean = True)
        If Not myCsiTester.checkRunOngoing Then
            'TODO: Status bar here in the future
            'Set up wait cursor
            Dim cursorWait As New cCursorWait

            'Reset all examples' compared status
            myCsiTester.ResetExampleResultStatus(False)

            myCsiTester.ClearDestinationFolder()
            SaveCSiTester()

            btnBClear.LargeImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
            btnBClear.SmallImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))
            chkMenuItem_Clear.ImageSource = New BitmapImage(New Uri(_BTN_ICON_CLEAR_DONE, UriKind.Relative))

            If allowRefreshForm Then RefreshForm(True)

            'Set up wait cursor
            cursorWait.EndCursor()
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Hand),
                                                        _PROMPT_ILLEGAL_ACTION_DURING_CHECK,
                                                        _TITLE_ILLEGAL_ACTION_DURING_CHECK))
        End If
    End Sub

    ''' <summary>
    ''' Displays examples multiple tabs grouped by Classification 2, and updates GUI controls accordingly.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMultiTab()
        Try
            'Update settings parameter
            testerSettings.singleTab = False

            'Update GUI
            'RefreshForm()
            myCsiTester.CreateTestSetMultiple()
            InitializeDataGridTabs(True)

            'Update dropdown menu
            ramMultiTab.IsEnabled = False
            ramSingleTab.IsEnabled = True

            'Set 'all tabs' toggle buttons to be visible
            chkBxRunAllTabs.Visibility = Windows.Visibility.Visible
            chkBxCompareAllTabs.Visibility = Windows.Visibility.Visible

            'Adjust border around datagrid
            brdr_DG_ExampleSummary.BorderThickness = New Thickness(1, 0, 1, 1)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Displays examples on a single tab, regardless of Classification 2, and updates GUI controls accordingly.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSingleTab()
        Try
            'Update settings parameter
            testerSettings.singleTab = True

            'Update GUI
            'RefreshForm()
            myCsiTester.CreateTestSetSingle()
            InitializeDataGridTabs(True)
            TabMaster.Header = "All Examples"

            'Update dropdown menu
            ramMultiTab.IsEnabled = True
            ramSingleTab.IsEnabled = False

            'Set 'all tabs' toggle buttons to be hidden
            chkBxRunAllTabs.Visibility = Windows.Visibility.Collapsed
            chkBxCompareAllTabs.Visibility = Windows.Visibility.Collapsed

            'Adjust border around datagrid
            brdr_DG_ExampleSummary.BorderThickness = New Thickness(1, 1, 1, 1)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Tab & DataGrid"
    ''' <summary>
    ''' Opens a new window displaying detailed data for the example clicked
    ''' </summary>
    ''' <param name="o"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnHyperlinkClick(ByVal o As TextBlock, ByVal e As RoutedEventArgs)
        Dim myTabIndex As Integer    'Tab Index, corresponding to a test set
        Dim myExampleIndex As Integer   'Selected Example index within a given test set

        Try
            'Get selected tab, which corresponds to the example test set needed
            myTabIndex = Math.Max(myTabControlSummary.SelectedIndex, 0)


            'Determine which example was clicked in the datagrid rows
            myExampleIndex = GetSelectedExample(myTabIndex)

            Dim window As New frmExample(examplesTestSetList(myTabIndex).examplesList(myExampleIndex))
            window.DataContext = examplesTestSetList(myTabIndex).examplesList(myExampleIndex) 'Passes relevant binding information to sub-class
            window.tabSelectedIndex = myTabIndex
            window.Show()
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Updates summary tab information when tab selection is changed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub myTabControlSummary_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles myTabControlSummary.SelectionChanged
        Dim i As Integer

        'Determine Selected Tab Index
        i = Math.Max(myTabControlSummary.SelectedIndex, 0)
        If i = -1 Then i = 0
        If i > examplesTestSetList.Count - 1 Then i = examplesTestSetList.Count - 1

        'Assign corresponding data collection to the datagrid
        dataGrid_ExampleSummary.DataContext = examplesTestSetList(i)

        'Assign corresponding datagrid headers
        AssignClassRegionHeader(i)
        AssignCodeExampleNumberHeader(i)
    End Sub

    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        dataGrid_ExampleSummary.MaxHeight = rowDG.ActualHeight - dataGrid_ExampleSummary.Margin.Bottom - gridMain.Margin.Bottom - brdr_DG_ExampleSummary.BorderThickness.Top - brdr_DG_ExampleSummary.BorderThickness.Bottom

        'Below is turned off for now. As long as columns are resizable and auto-sizeable and scale with the screen, the scrollbar does not really make sense. Choose one system or the other
        'dataGrid_ExampleSummary.MaxWidth = colMain.ActualWidth - dataGrid_ExampleSummary.Margin.Left - dataGrid_ExampleSummary.Margin.Right - gridMain.Margin.Left - gridMain.Margin.Right - brdr_DG_ExampleSummary.BorderThickness.Left - brdr_DG_ExampleSummary.BorderThickness.Right
    End Sub

    ''' <summary>
    ''' Removes the 'Failed Examples' tab if it exists.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveTabFailedExamples()
        Dim i As Integer = 0

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            If StringsMatch(exampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                'myTabControlSummary.Items.RemoveAt(myTabControlSummary.Items.Count - 1)
                myTabControlSummary.Items.RemoveAt(i)
                Exit For
            End If
            i += 1
        Next

    End Sub
#End Region

#Region "Methods: Example Selection (DataGrid)"
    '=== General Subs & Functions
    ''' <summary>
    ''' Determines which example was clicked in the datagrid rows and retrieves its index position in the examplesTestSetList referenced
    ''' </summary>
    ''' <param name="mySelectedTabIndex">0-based index corresponding to the tab number selected. This determines which examplesTestSetList to reference</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSelectedExample(ByVal mySelectedTabIndex As Integer) As Integer
        Dim exampleIndex As Integer
        Dim myExampleNumberCode As String = ""   'Name of example title clicked. Used to match the cell clicked with the collection index of examples, as these can lose sync

        'Determine which example was clicked in the datagrid rows
        exampleIndex = 0
        mySelectedTabIndex = Math.Max(mySelectedTabIndex, 0)

        For Each exampleRow As cExample In dataGrid_ExampleSummary.SelectedItems
            myExampleNumberCode = exampleRow.numberCodeExample
        Next

        For Each exampleEntry As cExample In examplesTestSetList(mySelectedTabIndex).examplesList
            If exampleEntry.numberCodeExample = myExampleNumberCode Then Exit For
            exampleIndex = exampleIndex + 1
        Next

        GetSelectedExample = exampleIndex
        'gridRow = dataGrid_ExampleSummary.SelectedIndex      'Original alternative. 
        'DO NOT USE the above if user is allowed to sort rows, as the index will not match the examples order
    End Function

    ''' <summary>
    ''' Determines which set of examples was selected in the datagrid rows and retrieves their index positions in the examplesTestSetList referenced
    ''' </summary>
    ''' <param name="mySelectedTabIndex">0-based index corresponding to the tab number selected. This determines which examplesTestSetList to reference</param>
    ''' <param name="myExampleIndexList">List to be populated with examplesTestSetList indices</param>
    ''' <remarks></remarks>
    Private Sub GetSelectedExamples(ByVal mySelectedTabIndex As Integer, ByRef myExampleIndexList As ObservableCollection(Of Integer))
        Dim myExampleIndex As Integer
        Dim myExampleNumberCode As String = ""   'Name of example title clicked. Used to match the cell clicked with the collection index of examples, as these can lose sync

        'Determine which example was clicked in the datagrid rows
        myExampleIndex = 0
        mySelectedTabIndex = Math.Max(mySelectedTabIndex, 0)

        'Check each cell that is selected
        For Each exampleRow As cExample In dataGrid_ExampleSummary.SelectedItems
            myExampleNumberCode = exampleRow.modelID
            'For a given selected cell, determine its index in examplesList and store it in a collection
            For Each exampleEntry As cExample In examplesTestSetList(mySelectedTabIndex).examplesList
                If exampleEntry.modelID = myExampleNumberCode Then
                    myExampleIndexList.Add(myExampleIndex)
                    Exit For
                End If
                myExampleIndex = myExampleIndex + 1
            Next
            myExampleIndex = 0
        Next
    End Sub

    ''' <summary>
    ''' Determines whether to check or uncheck a cell based on which cell was checked and what its current check status is 
    ''' </summary>
    ''' <param name="myCellSelectOperation"></param>
    ''' <param name="myCheckType"></param>
    ''' <remarks></remarks>
    Private Sub CellSelectionOperation(ByRef myCellSelectOperation As eCellSelectOperation, ByVal myCheckType As eCheckType)
        Dim tabIndex As Integer    'Tab Index, corresponding to a test set
        Dim exampleIndexList As New ObservableCollection(Of Integer)
        Dim myExampleSelected As cExample

        'Get selected tab, which corresponds to the example test set needed
        tabIndex = Math.Max(myTabControlSummary.SelectedIndex, 0)

        'Get Indeces for selected examples
        GetSelectedExamples(tabIndex, exampleIndexList)

        'Set status for each selected example
        For Each selectedExampleIndex As Integer In exampleIndexList
            myExampleSelected = examplesTestSetList(tabIndex).examplesList(selectedExampleIndex)

            'Set boolean status
            If myCheckType = eCheckType.Run And myExampleSelected.runExample Then
                myCellSelectOperation = eCellSelectOperation.Remove
            ElseIf myCheckType = eCheckType.Compare And myExampleSelected.compareExample Then
                myCellSelectOperation = eCellSelectOperation.Remove
            Else
                myCellSelectOperation = eCellSelectOperation.Add
            End If
        Next
    End Sub

    ''' <summary>
    ''' Checks/unchecks a clicked cell in the 'run' column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridCellRun_PreviewMouseLeftButtonDown(sender As Object, e As RoutedEventArgs)
        Dim checkType As eCheckType
        Dim cellSelectOperation As eCellSelectOperation

        myCsiTester.checkboxClick = True

        checkType = eCheckType.Run

        CellSelectionOperation(cellSelectOperation, checkType)

        SelectionAddRemoveReplace(checkType, cellSelectOperation)

        'Synchronizes operation if this option is selected
        'TODO: Not quite working. Be very careful not to double count time estimate within cExample
        'If synchRunCompareSelections Then
        '    checkType = eCheckType.Compare
        '    CellSelectionOperation(cellSelectOperation, checkType)

        '    SelectionAddRemoveReplace(checkType, cellSelectOperation)
        'End If

        myCsiTester.checkboxClick = False
    End Sub

    ''' <summary>
    ''' Checks/unchecks a clicked cell in the 'compare' column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridCellCompare_PreviewMouseLeftButtonDown(sender As Object, e As RoutedEventArgs)
        Dim checkType As eCheckType
        Dim cellSelectOperation As eCellSelectOperation

        myCsiTester.checkboxClick = True

        checkType = eCheckType.Compare

        CellSelectionOperation(cellSelectOperation, checkType)

        SelectionAddRemoveReplace(checkType, cellSelectOperation)

        ''Synchronizes operation if this option is selected
        ''TODO: Not quite working. Be very careful not to double count time estimate within cExample
        'If synchRunCompareSelections Then
        '    checkType = eCheckType.Run
        '    CellSelectionOperation(cellSelectOperation, checkType)

        '    SelectionAddRemoveReplace(checkType, cellSelectOperation)
        'End If

        myCsiTester.checkboxClick = False

    End Sub

    '=== Nonsynchronized "All" Selections
    ''' <summary>
    ''' Sets all examples to be run
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunAll()
        Dim i As Integer    'Tab Index, corresponding to a test set
        Dim tabSelect As New ObservableCollection(Of Integer)

        'Creates list of tabs indices to run operation on (either 'selected', or 'all)
        If Not testerSettings.allTabsSelectRun Then
            i = Math.Max(myTabControlSummary.SelectedIndex, 0)
            tabSelect.Add(i)
        Else
            Dim numTabs As Integer = myTabControlSummary.Items.Count
            For i = 0 To numTabs - 1
                tabSelect.Add(i)
            Next
        End If

        'Runs operation
        For Each myTabIndex As Integer In tabSelect
            'Applies the operation to the given tab index
            For Each exampleRow As cExample In examplesTestSetList(myTabIndex).examplesList
                exampleRow.runExample = True
            Next
        Next
    End Sub
    ''' <summary>
    ''' Sets all examples to not be run
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RunNone()
        Dim i As Integer    'Tab Index, corresponding to a test set
        Dim tabSelect As New ObservableCollection(Of Integer)

        'Creates list of tabs indices to run operation on (either 'selected', or 'all)
        If Not testerSettings.allTabsSelectRun Then
            i = Math.Max(myTabControlSummary.SelectedIndex, 0)
            tabSelect.Add(i)
        Else
            Dim numTabs As Integer = myTabControlSummary.Items.Count
            For i = 0 To numTabs - 1
                tabSelect.Add(i)
            Next
        End If

        'Runs operation
        For Each myTabIndex As Integer In tabSelect
            'Applies the operation to the given tab index
            For Each exampleRow As cExample In examplesTestSetList(myTabIndex).examplesList
                exampleRow.runExample = False
            Next
        Next
    End Sub

    ''' <summary>
    ''' Sets all examples to be compared
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CompareAll()
        Dim i As Integer    'Tab Index, corresponding to a test set
        Dim tabSelect As New ObservableCollection(Of Integer)

        'Creates list of tabs indeces to run operation on (either 'selected', or 'all)
        If Not testerSettings.allTabsSelectCompare Then
            i = Math.Max(myTabControlSummary.SelectedIndex, 0)
            tabSelect.Add(i)
        Else
            Dim numTabs As Integer = myTabControlSummary.Items.Count
            For i = 0 To numTabs - 1
                tabSelect.Add(i)
            Next
        End If

        'Runs operation
        For Each myTabIndex As Integer In tabSelect
            'Applies the operation to the given tab index
            For Each exampleRow As cExample In examplesTestSetList(myTabIndex).examplesList
                exampleRow.compareExample = True
            Next
        Next
    End Sub
    ''' <summary>
    ''' Sets all examples to not be compared
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CompareNone()
        Dim i As Integer    'Tab Index, corresponding to a test set
        Dim tabSelect As New ObservableCollection(Of Integer)

        'Creates list of tabs indeces to run operation on (either 'selected', or 'all)
        If Not testerSettings.allTabsSelectCompare Then
            i = Math.Max(myTabControlSummary.SelectedIndex, 0)
            tabSelect.Add(i)
        Else
            Dim numTabs As Integer = myTabControlSummary.Items.Count
            For i = 0 To numTabs - 1
                tabSelect.Add(i)
            Next
        End If

        'Runs operation
        For Each myTabIndex As Integer In tabSelect
            'Applies the operation to the given tab index
            For Each exampleRow As cExample In examplesTestSetList(myTabIndex).examplesList
                exampleRow.compareExample = False
            Next
        Next
    End Sub

    '=== Synchronized "All" Selections
    ''' <summary>
    ''' Sets all examples to be run and compared
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectAll()
        RunAll()
        CompareAll()
    End Sub
    ''' <summary>
    ''' Sets all examples to not be run or compared
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectNone()
        RunNone()
        CompareNone()
    End Sub

    '=== Selections based on selected rows/cells
    ''' <summary>
    ''' Sets a given selection of cells to be added to or removed from the run and/or compare actions
    ''' </summary>
    ''' <param name="myCheck">Whether selection operation is to involve the status of run, compare, or both</param>
    ''' <param name="myCellSelectOperation">Whether the selection operation is to be added to, subtracted from, or is to replace the set marked to be checked</param>
    ''' <remarks></remarks>
    Private Sub SelectionAddRemoveReplace(ByVal myCheck As eCheckType, ByVal myCellSelectOperation As eCellSelectOperation)
        Dim myTabIndex As Integer    'Tab Index, corresponding to a test set
        Dim myExampleIndexList As New ObservableCollection(Of Integer)
        Dim myExampleSelected As cExample
        Dim booleanStatus As Boolean

        'Synchronizes operation if this option is selected
        If _syncRunCompareSelections Then myCheck = eCheckType.RunAndCompare

        'If replacing selection, clear examples of "True" values
        If myCellSelectOperation = eCellSelectOperation.Replace Then 'Clear all examples status of true
            Select Case myCheck
                Case eCheckType.Run
                    RunNone()
                Case eCheckType.Compare
                    CompareNone()
                Case eCheckType.RunAndCompare
                    RunNone()
                    CompareNone()
            End Select
        End If

        'Get selected tab, which corresponds to the example test set needed
        myTabIndex = Math.Max(myTabControlSummary.SelectedIndex, 0)

        'Get Indices for selected examples
        GetSelectedExamples(myTabIndex, myExampleIndexList)

        'Set boolean status
        If myCellSelectOperation = eCellSelectOperation.Remove Then
            booleanStatus = False
        Else
            booleanStatus = True
        End If

        'Set status for each selected example
        For Each selectedExampleIndex As Integer In myExampleIndexList
            myExampleSelected = examplesTestSetList(myTabIndex).examplesList(selectedExampleIndex)
            Select Case myCheck
                Case eCheckType.Run
                    myExampleSelected.runExample = booleanStatus
                Case eCheckType.Compare
                    myExampleSelected.compareExample = booleanStatus
                Case eCheckType.RunAndCompare
                    myExampleSelected.runExample = booleanStatus
                    myExampleSelected.compareExample = booleanStatus
            End Select
            myCsiTester.UpdateEstimatedTimes()
        Next

    End Sub

#End Region




#Region "Test Components"
    ''' <summary>
    ''' List of test functions used for running e2e tests of the form.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eTestFunctions
        <Description("TestChangeProgramPath")> TestChangeProgramPath = 1
        <Description("TestChangeSource")> TestChangeSource = 2
        <Description("TestChangeDestination")> TestChangeDestination = 3
        <Description("TestDeselectExamplesRun")> TestDeselectExamplesRun = 4
        <Description("TestDeselectExamplesCompare")> TestDeselectExamplesCompare = 5
        <Description("TestSelectExamplesRun")> TestSelectExamplesRun = 6
        <Description("TestSelectExamplesCompare")> TestSelectExamplesCompare = 7
        <Description("TestClearDestination")> TestClearDestination = 8
        <Description("TestRestoreDefaults")> TestRestoreDefaults = 9
        <Description("TestResetSession")> TestResetSession = 10
        <Description("TestRunCheck")> TestRunCheck = 11
    End Enum

    Friend Sub TestForm(ByVal tests As ObservableCollection(Of cE2eTest))
        e2eTestingRunning = True

        Try
            'Set up initial state for running tests
            TestFirstAssertions()

            'Run selected tests
            For Each test As cE2eTest In tests
                For Each testComponent As cE2ETestComponent In test.components
                    If testComponent.formName = CLASS_STRING Then   'Component applies to this form. Run component tests.
                        For Each testComponentSub As cE2ETestComponentSub In testComponent.componentSubs
                            Dim testID As String = testComponentSub.testID
                            e2eTester.currentTestID = testID
                            Select Case testComponentSub.functionName
                                Case GetEnumDescription(eTestFunctions.TestChangeProgramPath) : TestChangeProgramPath(testID)
                                Case GetEnumDescription(eTestFunctions.TestChangeSource) : TestChangeSource(testID)
                                Case GetEnumDescription(eTestFunctions.TestChangeDestination) : TestChangeDestination(testID)
                                Case GetEnumDescription(eTestFunctions.TestDeselectExamplesRun) : TestDeselectExamplesRun(testID, False)            'Note - capture True & False in the future
                                Case GetEnumDescription(eTestFunctions.TestDeselectExamplesCompare) : TestDeselectExamplesCompare(testID, False)    'Note - capture True & False in the future
                                Case GetEnumDescription(eTestFunctions.TestSelectExamplesRun) : TestSelectExamplesRun(testID, False)                'Note - capture True & False in the future
                                Case GetEnumDescription(eTestFunctions.TestSelectExamplesCompare) : TestSelectExamplesCompare(testID, False)        'Note - capture True & False in the future
                                Case GetEnumDescription(eTestFunctions.TestClearDestination) : TestClearDestination()
                                Case GetEnumDescription(eTestFunctions.TestRestoreDefaults) : TestRestoreDefaults()
                                Case GetEnumDescription(eTestFunctions.TestResetSession) : TestResetSession()
                                Case GetEnumDescription(eTestFunctions.TestRunCheck) : TestRunCheck(testID)
                            End Select
                        Next
                    End If
                Next
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            'Print Report to file
            e2eTester.PrintLogger()

            'Cleanup
            e2eTestingRunning = False
        End Try

    End Sub

    ''' <summary>
    ''' Resets CSiTester values back to the original values from before running the tests.
    ''' </summary>
    ''' <param name="originalPathProgram">Original path to the CSi analysis program to use.</param>
    ''' <param name="originalPathDestination">Original path to the CSiTester files destination.</param>
    ''' <param name="originalPathSource">Original path to the models source.</param>
    ''' <param name="originalApplyAllTabs">Original selection of apply select all/none to all tabs or just the current tab.</param>
    ''' <param name="originalSingleTab">Original state of the examples being displayed on one tab or multiple tabs.</param>
    ''' <remarks></remarks>
    Friend Sub TesterRevert(ByVal originalPathProgram As String, ByVal originalPathDestination As String, ByVal originalPathSource As String, ByVal originalSingleTab As Boolean, ByVal originalApplyAllTabs As Boolean)
        Try
            '==== Set program path
            Dim oldPath As String = myRegTest.program_file.path

            'Using btnBrowseProgram_Click
            myRegTest.program_file.SetProperties(originalPathProgram)
            ProgramPathActions(oldPath)

            '==== Set destination path
            'Using btnBrowseDestination_Click
            myCsiTester.SetBrowseModelDestinationSettings(True, True)

            ''Final treatment of path
            myCsiTester.testerDestinationDir = originalPathDestination
            myRegTest.SetModelsRunDirectory(originalPathDestination)

            DestinationPathActions(True)

            '==== Set source path
            myRegTest.models_database_directory.SetProperties(originalPathSource)

            'Using btnBrowseSource_Click
            SourcePathActions(True)

            '==== Clear example selections
            testerSettings.allTabsSelectRun = originalApplyAllTabs
            chkMenuItem_ApplyAllTabsRun.IsChecked = originalApplyAllTabs

            e2eTester.ButtonClick(btnRunNone)
            e2eTester.ButtonClick(btnCompareNone)

            '==== Display examples on one tab
            If originalSingleTab Then
                SetSingleTab()
            Else
                SetMultiTab()
            End If

            SaveCSiTester()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the CSiTester program to be in a constant predefined state before running tests.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function TestFirstAssertions() As Boolean
        Try
            '==== Set program path
            Dim newPath As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & "testing\programs\" & "ETABS"
            Dim oldPath As String = myRegTest.program_file.path

            'Using btnBrowseProgram_Click
            myRegTest.program_file.SetProperties(newPath)
            ProgramPathActions(oldPath)

            '==== Set destination path
            'Using btnBrowseDestination_Click
            myCsiTester.SetBrowseModelDestinationSettings(True, True)

            ''Final treatment of path
            Dim pathDestination As String = DIR_TESTER_DESTINATION_DIR_DEFAULT
            myCsiTester.testerDestinationDir = pathDestination
            myRegTest.SetModelsRunDirectory(pathDestination)

            DestinationPathActions(True)

            '==== Set source path
            Dim suiteDir As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & "sourceDefault"
            myRegTest.models_database_directory.SetProperties(suiteDir)

            'Using btnBrowseSource_Click
            SourcePathActions(True)

            '==== Display examples on multiple tabs
            'SetSingleTab()
            SetMultiTab()

            '==== Clear example selections
            e2eTester.ButtonClick(btnRunNone)
            e2eTester.ButtonClick(btnCompareNone)

            SaveCSiTester()

            Return True
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    '=== Test Routines

    ''' <summary>
    ''' Tests the effects of changing the program path. Selects a program in the tester directory.
    ''' </summary>
    ''' <param name="testID">Test ID associated with the program name to be used.</param>
    ''' <param name="programIndex">The index number to use for the list of programs in the test.</param>
    ''' <param name="newProgramName">If specified, the program of the specified name will be what is selected.</param>
    ''' <returns>True if no errors occurred.</returns>
    ''' <remarks></remarks>
    Private Function TestChangeProgramPath(Optional ByVal testID As String = "", Optional programIndex As Integer = 0, Optional ByVal newProgramName As String = "") As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim program As String = ""
        Dim oldPath As String
        Dim newPath As String
        Dim oldProgramName As String
        Dim oldProgramVersion As String
        Dim oldProgramBuild As String
        Dim oldFormTitle As String
        Dim oldToolTip As String
        Dim oldList As New List(Of String)
        Dim newList As New List(Of String)
        Dim test As cE2ETestInstructions = Nothing

        Dim subTestPass As Boolean

        '=== Initialize Log
        e2eTester.StartSubTests("TestChangeProgramPath")
        Try
            '=== Set Benchmarks
            If Not String.IsNullOrEmpty(testID) Then
                test = e2eTester.GetTestByID(testID)
            End If

            'oldList.Add(testerSettings.programName)
            With myRegTest
                oldProgramName = GetEnumDescription(.program_name)
                oldList.Add(oldProgramName)
                oldPath = .program_file.path
                oldList.Add(oldPath)
                oldProgramVersion = .program_version
                oldList.Add(oldProgramVersion)
                oldProgramBuild = .program_build
                oldList.Add(oldProgramBuild)
            End With

            oldFormTitle = Me.Title
            oldList.Add(oldFormTitle)
            oldToolTip = btnBrowseProgramBasic.ToolTipDescription 'btnBrowseProgram.ToolTip.ToString
            oldList.Add(oldToolTip)

            '=== Make Assertions
            If (Not String.IsNullOrEmpty(testID) AndAlso test IsNot Nothing) Then
                program = test.GetProgramName(programIndex)
                newList.Add(program)
            ElseIf Not String.IsNullOrEmpty(newProgramName) Then
                program = newProgramName
                newList.Add(newProgramName)
            End If

            program = program & "\" & program & ".exe"

            newPath = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & "testing\programs\" & program
            newList.Add(newPath)

            'Using btnBrowseProgram_Click
            myRegTest.program_file.SetProperties(newPath)
            ProgramPathActions(oldPath)

            '=== Validate Expectations
            If Not VldtChangePathProgram("", oldList, newList) Then Exit Try

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    ''' <summary>
    ''' Tests the effects of changing the models source directory. Changes the source to that specified by the suite ID.
    ''' </summary>
    ''' <param name="testID">Test ID associated with the test suite to be used.</param>
    ''' <param name="suiteIndex">The index number to use for the list of suites in the test.</param>
    ''' <param name="suiteID">ID that identifies the testing suite that the source is to be changed to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestChangeSource(ByVal testID As String, Optional ByVal suiteIndex As Integer = 0, Optional ByVal suiteID As String = "") As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean

        '=== Initialize Log
        e2eTester.StartSubTests("TestChangeSource")

        Try
            '=== Set Benchmarks
            Dim suite As cE2ETestSuite
            Dim expctExamplesNum As Integer
            Dim expctTabsNum As Integer
            Dim suiteDir As String
            Dim expctRelExamplePaths As New ObservableCollection(Of String)
            Dim expctAbsExamplePaths As New ObservableCollection(Of String)
            Dim pathAbsStubCount As Integer = Len(pathStartup)

            If String.IsNullOrEmpty(suiteID) Then
                suite = e2eTester.GetSuiteByTestID(testID, suiteIndex)
            Else
                suite = e2eTester.GetSuiteByID(suiteID)
            End If

            expctExamplesNum = suite.examples.Count

            If testerSettings.singleTab Then
                expctTabsNum = 1
            Else
                expctTabsNum = suite.classificationLevel2List.Count
            End If

            For Each example As cE2EExample In suite.examples
                expctAbsExamplePaths.Add(example.pathMCxml)
            Next
            For Each absPath As String In expctAbsExamplePaths
                'Currently this data can only be deduced if the path is beneath CSiTester.exe in the path.
                If StringExistInName(absPath, pathStartup) Then
                    expctRelExamplePaths.Add(Right(absPath, Len(absPath) - pathAbsStubCount - 1))
                End If
            Next

            'Change suite if specified in the source, else, use the existing source
            suiteDir = suite.GetSuitePath

            '=== Make Assertions
            myRegTest.models_database_directory.SetProperties(suiteDir)

            'Using btnBrowseSource_Click
            SourcePathActions(True)

            '=== Validate Expectations
            'Clear saved examples run, ran, compare, compared in the settings file
            With testerSettings
                Dim className As String = "testerSettings"
                If Not .VldtNewPaths(className, expctExamplesNum, expctRelExamplePaths, expctAbsExamplePaths) Then Exit Try
                If Not .VldtClearExampleSettings(className, True, True, True, True) Then Exit Try
            End With

            If Not VldtChangeSource("", expctExamplesNum, expctTabsNum) Then Exit Try
            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    ''' <summary>
    ''' Tests the effects of changing the destination directory. Changes the destination to that specified.
    ''' </summary>
    ''' <param name="testID">Test ID associated with the test suite to be used.</param>
    ''' <param name="destinationIndex">The index number to use for the list of destinations in the test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestChangeDestination(ByVal testID As String, Optional ByVal destinationIndex As Integer = 0) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean = False      'When returned, indicates if all validations passed
        Dim pathDestination As String
        Dim oldNumTestSetFailed As Integer
        Dim oldNumFailedTabs As Integer
        Dim destinationRun As Boolean       'If true, the destination files have been run, so results files are expected to exist.

        '=== Initialize Log
        e2eTester.StartSubTests("TestChangeDestination")

        Try
            '=== Set Benchmarks
            Dim destination As cE2ETestDestination
            Dim suite As cE2ETestSuite
            Dim expctExamplesFailNum As Integer
            Dim expctTabsNum As Integer
            Dim suiteDir As String

            destination = e2eTester.GetDestinationByTestID(testID, destinationIndex)
            suite = e2eTester.GetSuiteByID(destination.idSuite)

            expctTabsNum = suite.classificationLevel2List.Count
            suiteDir = suite.GetSuitePath

            With destination
                pathDestination = .GetDestinationPath

                'Determine current status parameters of the destination
                destinationRun = .destinationRun
                oldNumTestSetFailed = .testSetFailedNumOriginal
                oldNumFailedTabs = .tabsFailedNumOriginal
                expctExamplesFailNum = .examplesFailNumOriginal
            End With

            '=== Make Assertions

            'Using btnBrowseDestination_Click
            myCsiTester.SetBrowseModelDestinationSettings(True, True)

            ''Final treatment of path
            myCsiTester.testerDestinationDir = pathDestination
            myRegTest.SetModelsRunDirectory(pathDestination)

            DestinationPathActions(True)

            '=== Validate Expectations
            'Destination path and references to it should be updated
            If Not VldtPathDestination("") Then Exit Try
            If Not VldtClearChangeDestination("") Then Exit Try

            ''Reset Failed Examples
            If Not VldtCheckFailedExamples("", oldNumFailedTabs, oldNumTestSetFailed, expctExamplesFailNum) Then Return subTestPass

            'Other actions related to a change in destination should occur
            If Not VldtControlsEnabled("", destinationRun) Then Exit Try

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try
        Return subTestPass
    End Function

    'TODO: Add checkbox selection
    ''' <summary>
    ''' Tests the effects of deselecting examples from being run.
    ''' </summary>
    ''' <param name="testID">Test ID associated with a pre-defined selection list.</param>
    ''' <param name="checkCheckBoxes">If true, the GUI checkboxes will be checked. If false, the data value will be changed directly.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestDeselectExamplesRun(ByVal testID As String, ByVal checkCheckBoxes As Boolean) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean
        Dim i As Integer


        '=== Initialize Log
        e2eTester.StartSubTests("TestDeselectExamplesRun")

        Try
            '=== Set Benchmarks
            Dim test As cE2ETestInstructions
            Dim modelIDsList As New ObservableCollection(Of String)

            test = e2eTester.GetTestByID(testID)
            modelIDsList = test.runDeselect

            '=== Make Assertions
            If (modelIDsList Is Nothing OrElse
                modelIDsList.Count = 0) Then     'Clear selection
                e2eTester.ButtonClick(btnRunNone)
            Else                                                                'Deselect certain examples
                If checkCheckBoxes Then
                    SelectRowsAllTabs(modelIDsList)
                    'e2eTester.DataGridCellClick()
                Else
                    SelectRowsAllTabs(modelIDsList, btnRunSelectionRemove)
                End If
            End If

            '=== Validate Expectations
            i = 0
            For Each myTestSet As cExampleTestSet In examplesTestSetList
                If String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                    If Not myTestSet.VldtExamplesSelectedRun("examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]", modelIDsList, False) Then Exit Try
                End If
                i += 1
            Next

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Add checkbox selection
    ''' <summary>
    ''' Tests the effects of deselecting examples from being compared.
    ''' </summary>
    ''' <param name="testID">Test ID associated with a pre-defined selection list.</param>
    ''' <param name="checkCheckBoxes">If true, the GUI checkboxes will be checked. If false, the data value will be changed directly.</param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestDeselectExamplesCompare(ByVal testID As String, ByVal checkCheckBoxes As Boolean) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean
        Dim i As Integer


        '=== Initialize Log
        e2eTester.StartSubTests("TestDeselectExamplesCompare")

        Try
            '=== Set Benchmarks
            Dim test As cE2ETestInstructions
            Dim modelIDsList As New ObservableCollection(Of String)

            test = e2eTester.GetTestByID(testID)
            modelIDsList = test.compareDeselect

            '=== Make Assertions
            If (modelIDsList Is Nothing OrElse
                modelIDsList.Count = 0) Then     'Clear selection
                e2eTester.ButtonClick(btnCompareNone)
            Else                                                                'Deselect certain examples
                If checkCheckBoxes Then
                    SelectRowsAllTabs(modelIDsList)
                    'e2eTester.DataGridCellClick()
                Else
                    SelectRowsAllTabs(modelIDsList, btnRunSelectionRemove)
                End If
            End If

            '=== Validate Expectations
            i = 0
            For Each myTestSet As cExampleTestSet In examplesTestSetList
                If String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                    If Not myTestSet.VldtExamplesSelectedRun("examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]", modelIDsList, False) Then Exit Try
                End If
                i += 1
            Next

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Add checkbox selection
    ''' <summary>
    ''' Tests the effects of selecting a set of examples to run.
    ''' </summary>
    ''' <param name="testID">Test ID associated with a pre-defined selection list.</param>
    ''' <param name="checkCheckBoxes">If true, the GUI checkboxes will be checked. If false, the data value will be changed directly.</param>
    ''' <param name="clearCurrentSelection">If true, the entire current selection, if any, will be cleared before making the selection defined in the test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestSelectExamplesRun(ByVal testID As String, ByVal checkCheckBoxes As Boolean, Optional clearCurrentSelection As Boolean = True) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean
        Dim i As Integer

        '=== Initialize Log
        e2eTester.StartSubTests("TestSelectExamplesRun")

        Try
            '=== Set Benchmarks
            Dim test As cE2ETestInstructions
            Dim modelIDsList As New ObservableCollection(Of String)

            test = e2eTester.GetTestByID(testID)
            modelIDsList = test.run

            '=== Make Assertions
            'Clear selected rows
            If clearCurrentSelection Then e2eTester.ButtonClick(btnRunNone)

            'Add selection
            If checkCheckBoxes Then
                SelectRowsAllTabs(modelIDsList)
                'e2eTester.DataGridCellClick()
            Else
                SelectRowsAllTabs(modelIDsList, btnRunSelectionAdd)
            End If

            '=== Validate Expectations
            i = 0
            For Each myTestSet As cExampleTestSet In examplesTestSetList
                If String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                    If Not myTestSet.VldtExamplesSelectedRun("examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]", modelIDsList, True) Then Exit Try
                End If
                i += 1
            Next

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Add checkbox selection
    ''' <summary>
    ''' Tests the effects of selecting a set of examples to run.
    ''' </summary>
    ''' <param name="testID">Test ID associated with a pre-defined selection list.</param>
    ''' <param name="checkCheckBoxes">If true, the GUI checkboxes will be checked. If false, the data value will be changed directly.</param>
    ''' <param name="clearCurrentSelection">If true, the entire current selection, if any, will be cleared before making the selection defined in the test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestSelectExamplesCompare(ByVal testID As String, ByVal checkCheckBoxes As Boolean, Optional clearCurrentSelection As Boolean = True) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean
        Dim i As Integer

        '=== Initialize Log
        e2eTester.StartSubTests("TestSelectExamplesCompare")

        Try
            '=== Set Benchmarks
            Dim test As cE2ETestInstructions
            Dim modelIDsList As New ObservableCollection(Of String)

            test = e2eTester.GetTestByID(testID)
            modelIDsList = test.compare

            '=== Make Assertions
            'Clear selected rows
            If clearCurrentSelection Then e2eTester.ButtonClick(btnCompareNone)

            'Add selection
            If checkCheckBoxes Then
                SelectRowsAllTabs(modelIDsList)
                'e2eTester.DataGridCellClick()
            Else
                SelectRowsAllTabs(modelIDsList, btnCompareSelectionAdd)
            End If

            '=== Validate Expectations
            i = 0
            For Each myTestSet As cExampleTestSet In examplesTestSetList
                If String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                    If Not myTestSet.VldtExamplesSelectedCompare("examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]", modelIDsList, True) Then Exit Try
                End If
                i += 1
            Next

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Once CSiTester reads in data from destination based on settings file, add checks for the read-in information, including failed examples, checking the number of failed examples.
    ''' <summary>
    ''' Tests the effects of clearing the destination by running the routine.
    ''' </summary>
    ''' <param name="specOldNumFailedTabs">Number of failed examples tabs to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <param name="specOldNumTestSetFailed">Number of failed examples test sets to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestClearDestination(Optional ByVal specOldNumFailedTabs As Integer = -1, Optional ByVal specOldNumTestSetFailed As Integer = -1) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean

        '=== Initialize Log
        e2eTester.StartSubTests("TestClearDestination")

        Try
            '=== Set Benchmarks
            Dim destinationRun As Boolean = False
            Dim oldNumFailedTabs As Integer
            Dim oldNumTestSetFailed As Integer

            If specOldNumFailedTabs >= 0 Then
                oldNumFailedTabs = specOldNumFailedTabs
            Else
                oldNumFailedTabs = CountTotalTabsFailedExamples()
            End If

            If specOldNumTestSetFailed >= 0 Then
                oldNumTestSetFailed = specOldNumTestSetFailed
            Else
                oldNumTestSetFailed = CountTotalTestSetFailedExamples()
            End If

            '=== Make Assertions
            ClearSession()

            '=== Validate Expectations
            If Not VldtClearChangeDestination("") Then Exit Try

            ''Reset Failed Examples
            If Not VldtCheckFailedExamples("", oldNumFailedTabs, oldNumTestSetFailed, 0, 0, 0, 0, 0, "", "") Then Return subTestPass

            With myCsiTester
                Dim className As String = "myCSiTester"
                If Not .VldtClearDestinationFolder(className) Then Exit Try
            End With

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Once CSiTester reads in data from destination based on settings file, add checks for the read-in information, including failed examples, checking the number of failed examples.
    ''' <summary>
    ''' Tests the effects of resetting settings to the defaults by running the routine.
    ''' </summary>
    ''' <param name="specOldNumFailedTabs">Number of failed examples tabs to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <param name="specOldNumTestSetFailed">Number of failed examples test sets to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestRestoreDefaults(Optional ByVal specOldNumFailedTabs As Integer = -1, Optional ByVal specOldNumTestSetFailed As Integer = -1) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean

        '=== Initialize Log
        e2eTester.StartSubTests("TestRestoreDefaults")

        Try
            '=== Set Benchmarks
            Dim destinationRun As Boolean = False
            Dim oldNumFailedTabs As Integer
            Dim oldNumTestSetFailed As Integer
            Dim oldDateModifiedCSiTesterSettingsXML As String
            Dim oldDateModifiedRegTestXML As String
            Dim oldDateModifiedregTestLog As String = ""

            If specOldNumFailedTabs >= 0 Then
                oldNumFailedTabs = specOldNumFailedTabs
            Else
                oldNumFailedTabs = CountTotalTabsFailedExamples()
            End If

            If specOldNumTestSetFailed >= 0 Then
                oldNumTestSetFailed = specOldNumTestSetFailed
            Else
                oldNumTestSetFailed = CountTotalTestSetFailedExamples()
            End If

            oldDateModifiedCSiTesterSettingsXML = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, True, False, False)
            oldDateModifiedRegTestXML = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, False, True, False)
            If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then oldDateModifiedregTestLog = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, False, False, True)

            '=== Make Assertions
            RestoreDefaults()

            '=== Validate Expectations
            If Not VldtClearChangeDestination("") Then Exit Try

            ''Reset Failed Examples
            If Not VldtCheckFailedExamples("", oldNumFailedTabs, oldNumTestSetFailed, 0, 0, 0, 0, 0, "", "") Then Return subTestPass

            With myCsiTester
                Dim className As String = "myCSiTester"
                If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then
                    If Not .VldtRestoreDefaults(className, oldDateModifiedCSiTesterSettingsXML, oldDateModifiedRegTestXML, oldDateModifiedregTestLog) Then Exit Try
                Else
                    If Not .VldtRestoreDefaults(className, oldDateModifiedCSiTesterSettingsXML, oldDateModifiedRegTestXML) Then Exit Try
                End If
            End With

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    'TODO: Once CSiTester reads in data from destination based on settings file, add checks for the read-in information, including failed examples, checking the number of failed examples.
    ''' <summary>
    ''' Tests the effects of resetting the current session by running the routine.
    ''' </summary>
    ''' <param name="specOldNumFailedTabs">Number of failed examples tabs to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <param name="specOldNumTestSetFailed">Number of failed examples test sets to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestResetSession(Optional ByVal specOldNumFailedTabs As Integer = -1, Optional ByVal specOldNumTestSetFailed As Integer = -1) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean

        '=== Initialize Log
        e2eTester.StartSubTests("TestResetSession")

        Try
            '=== Set Benchmarks
            Dim destinationRun As Boolean = False
            Dim oldNumFailedTabs As Integer
            Dim oldNumTestSetFailed As Integer
            Dim oldDateModifiedCSiTesterSettingsXML As String
            Dim oldDateModifiedRegTestXML As String
            Dim oldDateModifiedregTestLog As String = ""

            If specOldNumFailedTabs >= 0 Then
                oldNumFailedTabs = specOldNumFailedTabs
            Else
                oldNumFailedTabs = CountTotalTabsFailedExamples()
            End If

            If specOldNumTestSetFailed >= 0 Then
                oldNumTestSetFailed = specOldNumTestSetFailed
            Else
                oldNumTestSetFailed = CountTotalTestSetFailedExamples()
            End If

            oldDateModifiedCSiTesterSettingsXML = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, True, False, False)
            oldDateModifiedRegTestXML = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, False, True, False)
            If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then oldDateModifiedregTestLog = myCsiTester.GetDateModified(_programInitializer.csiTesterLevel, False, False, True)

            '=== Make Assertions
            ResetSession()

            '=== Validate Expectations
            If Not VldtClearChangeDestination("") Then Exit Try

            ''Reset Failed Examples
            If Not VldtCheckFailedExamples("", oldNumFailedTabs, oldNumTestSetFailed, 0, 0, 0, 0, 0, "", "") Then Return subTestPass

            With myCsiTester
                Dim className As String = "myCSiTester"
                If _programInitializer.csiTesterLevel = eCSiTesterLevel.Published Then
                    If Not .VldtResetDestinationFolder(className) Then Exit Try
                    If Not .VldtRestoreDefaults(className, oldDateModifiedCSiTesterSettingsXML, oldDateModifiedRegTestXML, oldDateModifiedregTestLog) Then Exit Try
                Else
                    If Not .VldtClearDestinationFolder(className) Then Exit Try
                    If Not .VldtRestoreDefaults(className, oldDateModifiedCSiTesterSettingsXML, oldDateModifiedRegTestXML) Then Exit Try
                End If
            End With

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    ''' <summary>
    ''' Tests the effects of running a check with various examples set to be run, compared, or run &amp; compared. 
    ''' The test will be canceled prematurely if the cancellation parameters are specified in the test properties.
    ''' </summary>
    ''' <param name="testID">Test ID associated with a pre-defined selection list.</param>
    ''' <param name="specOldNumFailedTabs">Number of failed examples tabs to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <param name="specOldNumTestSetFailed">Number of failed examples test sets to expect to be present before running the test. If not specified, it is gathered from the current state of the GUI.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TestRunCheck(ByVal testID As String, Optional ByVal specOldNumFailedTabs As Integer = -1, Optional ByVal specOldNumTestSetFailed As Integer = -1) As Boolean
        'Note: If changing function name, update the form enum description as well
        Dim subTestPass As Boolean
        Dim checkForm As Boolean = True

        '=== Initialize Log
        e2eTester.StartSubTests("TestRunCheck")

        Try
            '=== Set Benchmarks
            Dim i As Integer
            Dim test As cE2ETestInstructions

            test = e2eTester.GetTestByID(testID)

            Dim oldNumFailedTabs As Integer
            Dim oldNumTestSetFailed As Integer
            Dim numFailedExamples As Integer = CountTotalFailedExamples()
            Dim expctNumRan As Integer = test.ran.Count
            Dim expctNumCompared As Integer = test.compared.Count
            Dim expctNumPassed As Integer = test.compared.Count - test.failed.Count
            Dim expctNumFailed As Integer = test.failed.Count
            Dim expctMaxPercDiff As String = test.maxPercDiff
            Dim expctOverallResult As String = test.overallResult
            Dim expctModelsRanList As New ObservableCollection(Of String)
            Dim expctModelsComparedList As New ObservableCollection(Of String)

            If specOldNumFailedTabs >= 0 Then
                oldNumFailedTabs = specOldNumFailedTabs
            Else
                oldNumFailedTabs = CountTotalTabsFailedExamples()
            End If

            If specOldNumTestSetFailed >= 0 Then
                oldNumTestSetFailed = specOldNumTestSetFailed
            Else
                oldNumTestSetFailed = CountTotalTestSetFailedExamples()
            End If

            '=== Make Assertions
            e2eTester.ButtonClick(btnCheck)

            'Cancel model at specified time or model ID.
            If (Not String.IsNullOrEmpty(test.cancelCheckTime) AndAlso
                myCInt(test.cancelCheckTime) >= 0) Then

                While checkForm
                    If Not myStatusForm.timeRemaining = "TBD" Then
                        System.Threading.Thread.Sleep(myCInt(test.cancelCheckTime))
                        test.cancelCheckModelID = myStatusForm.nextModelID
                        myStatusForm.TestFctnCancelCheck()
                        checkForm = False

                        'Update model ran/compared lists for when the suite was actually canceled
                        test.UpdateRanComparedFromCancel(test.cancelCheckModelID)
                    Else
                        System.Threading.Thread.Sleep(1000)
                    End If
                End While
            ElseIf Not String.IsNullOrEmpty(test.cancelCheckModelID) Then
                While checkForm
                    If myStatusForm.nextModelID = test.cancelCheckModelID Then
                        myStatusForm.TestFctnCancelCheck()
                        checkForm = False
                    Else
                        System.Threading.Thread.Sleep(1000)
                    End If
                End While
            End If

            expctModelsRanList = test.ran
            expctModelsComparedList = test.compared

            '=== Validate Expectations
            'Check summary of any failed test sets
            If Not VldtCheckFailedExamples("", oldNumFailedTabs, oldNumTestSetFailed, numFailedExamples, expctNumRan, expctNumCompared, expctNumPassed, expctNumFailed, expctMaxPercDiff, expctOverallResult) Then Exit Try

            'Check examples ran/compared results
            i = 0
            For Each exampleTestSet As cExampleTestSet In examplesTestSetList
                If Not exampleTestSet.VldtExamplesCheck("examplesTestSetList[{" & cExampleItem.CLASS_STRING & "} " & i & "].", , expctModelsRanList, expctModelsComparedList) Then Exit Try
                i += 1
            Next

            subTestPass = True
        Catch ex As Exception
            subTestPass = False
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function


    '=== Validation Routines

    ''' <summary>
    ''' Validates that the expected changes occur when the program path is changed.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="oldList">List of the old values that may or may not be expected in the changed path.</param>
    ''' <param name="newList">List of the new values that are expected in the changed path.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtChangePathProgram(ByVal className As String, ByVal oldList As List(Of String), ByVal newList As List(Of String)) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "Program path to change"
            .resultActual = myRegTest.program_file.path
            .resultActualCall = "regTest.program_path"
            .resultExpected = newList(1)        'New path
            .resultNotExpected = oldList(1)     'Old path
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Program name in regTest to be the same as in cCSiTester"
            .resultActual = GetEnumDescription(myRegTest.program_name)
            .resultActualCall = "regTest.program_name vs. testerSettings.programName"
            .resultExpected = GetEnumDescription(testerSettings.programName)
            If Not .RunSubTest() Then Return subTestPass

            If oldList(0) = newList(0) Then     'Program name has not changed
                .expectation = "Program name to be the same (in this case)"
                .resultActual = GetEnumDescription(testerSettings.programName)
                .resultActualCall = "testerSettings.programName"
                .resultExpected = oldList(0)    'Old program name
            Else
                .expectation = "Program name to be different (in this case)"
                .resultActual = GetEnumDescription(testerSettings.programName)
                .resultActualCall = "testerSettings.programName"
                .resultNotExpected = oldList(0)     'Old program name
                .resultExpected = newList(0)        'New program name
            End If
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Program version to be a known version"
            .resultActual = testerSettings.programVersion
            .resultActualCall = "testerSettings.programVersion"
            .resultExpected = .GetVersion(ConvertStringToEnumByDescription(Of eCSiProgram)(newList(0)))
            If Not .resultExpected = oldList(2) Then .resultNotExpected = oldList(2) 'Old Program Version
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Program build to be a known build"
            .resultActual = testerSettings.programBuild
            .resultActualCall = "testerSettings.programBuild"
            .resultExpected = .GetBuild(ConvertStringToEnumByDescription(Of eCSiProgram)(newList(0)))
            If Not .resultExpected = oldList(3) Then .resultNotExpected = oldList(3) 'Old Program Build
            If Not .RunSubTest() Then Return subTestPass

            If oldList(0) = newList(0) Then     'Program name has not changed
                .expectation = "CSiTester form title to be the same (in this case)"
                .resultActual = Me.Title
                .resultActualCall = "Me.Title"
                .resultExpected = oldList(4)     'Old Form Title
            Else
                .expectation = "CSiTester form title to be different (in this case)"
                .resultActual = Me.Title
                .resultActualCall = "Me.Title"
                .resultNotExpected = oldList(4)     'Old Form Title
                .resultExpected = GetFormTitle()
            End If
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Program path tooltips to be a known path"
            .resultActual = btnBrowseProgramBasic.ToolTipDescription
            .resultActualCall = "btnBrowseProgramBasic.ToolTip.ToString"
            .resultExpected = GetPrefix(ttBrowseProgram, ".") & ": " & Environment.NewLine & newList(1)
            .resultNotExpected = oldList(5)     'Old Tool Tip
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the expected changes occur when the source path is changed.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="expctExamplesNum">Expected number of examples to be present.</param>
    ''' <param name="expctTabsNum">Expected number of tabs to be created.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtChangeSource(ByVal className As String, ByVal expctExamplesNum As Integer, ByVal expctTabsNum As Integer) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "Source path tooltips to be a known path"
            .resultActual = btnBrowseSourceBasic.ToolTipDescription
            .resultActualCall = ""
            .resultExpected = GetPrefix(ttBrowseSource, ".") & ": " & Environment.NewLine & myRegTest.models_database_directory.path
            If Not .RunSubTest() Then Return subTestPass

            'RefreshForm()
            .expectation = "ExamplesTestSetList to have a known number of examples"
            .resultActual = CStr(CountTotalExamples())
            .resultActualCall = "CountTotalExamples()"
            .resultExpected = CStr(expctExamplesNum)
            If Not .RunSubTest() Then Return subTestPass

            'InitializeDataGridTabs(True)
            .expectation = "Grid Tabs to have a known number of tabs"
            .resultActual = CStr(myTabControlSummary.Items.Count)
            .resultActualCall = "myTabControlSummary.Items.Count"
            .resultExpected = CStr(expctTabsNum)
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the new destination path is updated in all applicable parts of the program.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtPathDestination(ByVal className As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "Destination path to be of a known value for the CSiTester form property"
            .resultActual = _programInitializer.testerDestinationDir
            .resultActualCall = classIdentifier & "testerDestinationDir"
            .resultExpected = myCsiTester.testerDestinationDir
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Destination path to be of a known value for the tester settings property"
            .resultActual = testerSettings.testerDestination.path
            .resultActualCall = classIdentifier & "{cSettings}testerSettings.testerDestinationDir"
            .resultExpected = myCsiTester.testerDestinationDir
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Destination path tooltips to be a known path"
            .resultActual = btnBrowseDestinationBasic.ToolTipDescription
            .resultActualCall = classIdentifier & "btnBrowseDestinationBasic.ToolTipDescription"
            .resultExpected = GetPrefix(ttBrowseDestination, ".") & ": " & Environment.NewLine & _programInitializer.testerDestinationDir
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    'TODO: Once CSiTester reads in data from destination based on settings file, 
    '   add checks for the read-in information, including failed examples, checking the number of failed examples.
    ''' <summary>
    ''' Validates that the expected changes occur when the destination path has been changed or the destination directory has been cleared.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VldtClearChangeDestination(ByVal className As String) As Boolean
        Dim subTestPass As Boolean = False      'When returned, indicates if all validations passed
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        'Clear saved examples run, ran, compare, compared in the settings file
        If Not testerSettings.VldtClearExampleSettings(classIdentifier & "testerSettings", True, True, True, True) Then Return subTestPass

        'Other aspects of all results in all examples should be reset
        i = 0
        For Each myTestSet As cExampleTestSet In examplesTestSetList
            If String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                If Not myTestSet.VldtResultReset(classIdentifier & "examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]") Then Return subTestPass
            End If
            i += 1
        Next

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the failed example sets have been properly cleared from memory and the GUI.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="destinationRun">If True, the expected results are based on the condition that examples have been run and analysis results filed exist.</param>
    ''' <param name="failedExamples">If True, the expected results are based on the condition that there have been run examples that have failed.</param>
    ''' <param name="oldNumFailedTabs">Original number of tabs for failed test sets, declared if the number is expected to change. Should be either 1 or 0.</param>
    ''' <param name="oldNumTestSetFailed">Original number of example test sets for failed examples, declared if the number is expected to change. Should be either 1 or 0.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtClearResetFailedExamples(ByVal className As String, ByVal destinationRun As Boolean, ByVal failedExamples As Boolean, _
                                     Optional ByVal oldNumFailedTabs As Integer = -1, Optional ByVal oldNumTestSetFailed As Integer = -1) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "GUI: There should be no failed examples tab"
            .resultExpected = "0"
            If failedExamples Then
                If destinationRun Then
                    .expectation = "GUI: There should be one failed examples tab"
                    .resultExpected = "1"
                End If
            End If
            .resultActual = CStr(CountTotalTabsFailedExamples())
            .resultActualCall = classIdentifier & "CountTotalTabsFailedExamples()"
            .resultExpected = "0"
            If Not oldNumFailedTabs = -1 Then .resultNotExpected = CStr(oldNumFailedTabs)
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "There should be no failed test sets"
            If failedExamples Then
                If destinationRun Then
                    .expectation = "There should be one failed test set"
                    .resultExpected = "1"
                End If
            End If
            .resultActual = CStr(CountTotalTestSetFailedExamples())
            .resultActualCall = classIdentifier & "CountTotalTestSetFailedExamples()"
            .resultExpected = "0"
            If Not oldNumTestSetFailed = -1 Then .resultNotExpected = CStr(oldNumTestSetFailed)
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that certain controls have been enabled/disabled based on certain states of the testing files and program.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="destinationRun">If True, expected results are based on the condition that examples have been run and analysis results filed exist.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtControlsEnabled(ByVal className As String, ByVal destinationRun As Boolean) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            .expectation = "GUI: Example schema verification button is of an expected enabled state"
            .resultActual = CStr(btnVerifyExampleSchema.IsEnabled)
            .resultActualCall = classIdentifier & "btnVerifyExampleSchema.IsEnabled"
            If destinationRun Then
                .resultExpected = "True"
            Else
                .resultExpected = "False"
            End If
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "GUI: Example schema view verification button is of an expected enabled state"
            .resultActual = CStr(btnViewVerifiedExampleSchema.IsEnabled)
            .resultActualCall = classIdentifier & "btnViewVerifiedExampleSchema.IsEnabled"
            If destinationRun Then
                .resultExpected = "True"
            Else
                .resultExpected = "False"
            End If
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the expected changes with failed examples occur when a check is performed.
    ''' </summary>
    ''' <param name="oldNumFailedTabs">Number of tabs for 'failed' examples before the check is performed.</param>
    ''' <param name="oldNumTestSetFailed">Number of test sets of the 'failed' type before the check is performed.</param>
    ''' <param name="numFailedExamples">Number of failed examples expected after the check is performed.</param>
    ''' <param name="expctdNumRan">Expected number of examples to have been run.</param>
    ''' <param name="expctdNumCompared">Expected number of examples to have been compared.</param>
    ''' <param name="expctdNumPassed">Expected number of examples to have passed.</param>
    ''' <param name="expctdNumFailed">Expected number of examples to have failed.</param>
    ''' <param name="expctdMaxPercDiff">Expected max % difference from examples.</param>
    ''' <param name="expctdOverallResult">Expected overall result from examples.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VldtCheckFailedExamples(ByVal className As String, ByVal oldNumFailedTabs As Integer, ByVal oldNumTestSetFailed As Integer, ByVal numFailedExamples As Integer, _
                                             Optional ByVal expctdNumRan As Integer = -1, Optional ByVal expctdNumCompared As Integer = -1, Optional ByVal expctdNumPassed As Integer = -1, _
                                             Optional ByVal expctdNumFailed As Integer = -1, Optional ByVal expctdMaxPercDiff As String = "expctdMaxPercDiff", Optional ByVal expctdOverallResult As String = "expctdOverallResult", _
                                             Optional ByVal vldtFailedTestSet As Boolean = False) As Boolean
        Dim subTestPass As Boolean = False      'When returned, indicates if all validations passed
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer
        Dim testSetListClassName As String

        ''Reset Failed Examples
        If numFailedExamples > 0 Then
            'Check for expected number of failed test sets, failed examples tabs
            If Not VldtClearResetFailedExamples("", True, True, oldNumFailedTabs, oldNumTestSetFailed) Then Return subTestPass

            'Check for redundant listed failed examples in any failed examples test sets
            i = 0
            For Each myTestSet As cExampleTestSet In examplesTestSetList
                If Not String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                    testSetListClassName = "examplesTestSetList[{" & cExampleTestSet.CLASS_STRING & "} " & i & "]"
                    If Not myTestSet.VldtFailedTestSetUniqueList(testSetListClassName) Then Return subTestPass
                    If Not myTestSet.VldtFailedTestSetSummary(testSetListClassName, expctdNumRan, expctdNumCompared, expctdNumPassed, expctdNumFailed, expctdMaxPercDiff, expctdOverallResult) Then Return subTestPass
                    If vldtFailedTestSet Then
                        If Not myTestSet.VldtExamplesCheck(testSetListClassName) Then Return subTestPass
                    End If
                End If
                i += 1
            Next
        Else
            If Not VldtClearResetFailedExamples("", True, False, oldNumFailedTabs, oldNumTestSetFailed) Then Return subTestPass
        End If

        subTestPass = True

        Return subTestPass
    End Function


    Private Function TestTemplate() As Boolean
        Dim subTestPass As Boolean = False      'When returned, indicates if all validations passed

        '=== Initialize Log
        e2eTester.StartSubTests("TestTemplate")

        Try
            '=== Set Benchmarks


            '=== Make Assertions

            '    '=== Validate Expectations

            '...Call validations from within class

            '...Call validations from other classes
            '        With testerSettings
            '            Dim className As String = "testerSettings"
            '            If Not .VldtClearPaths(className, True) Then Exit Try
            '            If Not .VldtClearExampleSettings(className, True, True, True, True) Then Exit Try
            '        End With

            '...Call validations from within class


            subTestPass = True
        Catch ex As Exception
            subTestPass = False
        Finally
            e2eTester.EndSubTests(subTestPass)
        End Try

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that ...
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="testSwitch">Check if ...</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Private Function VldtTemplate(ByVal className As String, Optional ByVal testSwitch As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        'With e2eTester
        '    If testSwitch Then
        '        .expectation = "Saved example paths to be cleared"
        '        .resultActual = CStr(examplePathsSaved)
        '        .resultActualCall = classIdentifier & "examplePathsSaved"
        '        .resultExpected = ""
        '        .resultNotExpected = ""
        '        If Not .runSubTest() Then Return subTestPass
        '    End If

        'Dim i As Integer
        '   'Result Items
        '    i = 0
        '    For Each resultItem As cExampleItem In itemList
        '        resultItem.VldtResultReset(classIdentifier & "itemList[{" & cExampleItem.CLASS_STRING & "} " & i & "].", True)
        '        i += 1
        '    Next

        'End With

        subTestPass = True

        Return subTestPass
    End Function
#End Region

#Region "Methods: Self-Query/Operate"

    ''' <summary>
    ''' Counts the total number of examples in all of the test sets.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountTotalExamples() As Integer
        Dim numExamples As Integer

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            If String.IsNullOrEmpty(exampleTestSet.maxPercentDifference) Then numExamples += exampleTestSet.examplesList.Count 'If test set is not a failed test set, count all examples within the set
        Next

        Return numExamples
    End Function

    ''' <summary>
    ''' Counts the total number of failed examples in all test sets. This counts the existing examples rather than querying the property in order to better check the result.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountTotalFailedExamples() As Integer
        Dim numFailedExamples As Integer = 0

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            If Not String.IsNullOrEmpty(exampleTestSet.maxPercentDifference) Then numFailedExamples += exampleTestSet.examplesList.Count
        Next

        Return numFailedExamples
    End Function

    ''' <summary>
    ''' Counts the number of tabs generated for failed test sets.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountTotalTabsFailedExamples() As Integer
        Dim numTabs As Integer

        For Each exampleTab As System.Windows.Controls.TabItem In myTabControlSummary.Items
            If exampleTab.Header.ToString = GetEnumDescription(eTestSetClassification.FailedExamples) Then numTabs += 1
        Next

        Return numTabs
    End Function

    ''' <summary>
    ''' Counts the number of tabs generated for general views of examples. This should not include the failed test set tab.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountTotalTabsNonFailedExamples() As Integer
        Dim numTabs As Integer

        For Each exampleTab As System.Windows.Controls.TabItem In myTabControlSummary.Items
            If Not exampleTab.Header.ToString = GetEnumDescription(eTestSetClassification.FailedExamples) Then numTabs += 1
        Next

        Return numTabs
    End Function

    ''' <summary>
    ''' Counts the total number of failed examples test sets. There should be at most one, but if there is an error in the program, more than one might be found.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountTotalTestSetFailedExamples() As Integer
        Dim numFailedTestSet As Integer

        For Each myTestSet As cExampleTestSet In examplesTestSetList
            If Not String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                numFailedTestSet += 1
            End If
        Next

        Return numFailedTestSet
    End Function

    ''' <summary>
    ''' Selects the desired rows in the datagrid based on the provided IDs list, and applies the appropriate action to the control specified. Repeated across all tabs.
    ''' </summary>
    ''' <param name="p_modelIDsList">List of models to select, by model ID.</param>
    ''' <param name="p_buttonToClick">Button object to 'click' with the rows selected.</param>
    ''' <remarks></remarks>
    Private Sub SelectRowsAllTabs(ByVal p_modelIDsList As ObservableCollection(Of String),
                                  Optional ByVal p_buttonToClick As Button = Nothing)
        Dim j As Integer

        Try
            'Select desired rows
            j = 0
            If testerSettings.singleTab Then
                myTabControlSummary.SelectedIndex = j                               'Specified in case a 'failed examples' tab has been created.
                e2eTester.TabClick(CType(myTabControlSummary.Items(j), TabItem), myTabControlSummary)   'Does same as above
                dataGrid_ExampleSummary.Focus()
                SelectRows(examplesTestSetList(0), p_modelIDsList, p_buttonToClick)
            Else
                j = 0
                For Each testSet As cExampleTestSet In examplesTestSetList
                    myTabControlSummary.SelectedIndex = j
                    e2eTester.TabClick(CType(myTabControlSummary.Items(j), TabItem), myTabControlSummary)
                    dataGrid_ExampleSummary.Focus()
                    SelectRows(testSet, p_modelIDsList, p_buttonToClick)
                    j += 1
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Selects the desired rows in the datagrid of the provided test set based on the provided IDs list, and applies the appropriate action to the control specified.
    ''' </summary>
    ''' <param name="p_testSet">Test set of examples by which the rows are selected.</param>
    ''' <param name="p_modelIDsList">List of models to select, by model ID.</param>
    ''' <param name="p_buttonToClick">Button object to 'click' with the rows selected.</param>
    ''' <remarks></remarks>
    Private Sub SelectRows(ByVal p_testSet As cExampleTestSet,
                           ByVal p_modelIDsList As ObservableCollection(Of String),
                           Optional ByVal p_buttonToClick As Button = Nothing)
        Dim i As Integer
        Dim rowIndices As New List(Of Integer)

        Try
            'Get row indexes
            i = 0
            For Each example As cExample In p_testSet.examplesList
                For Each modelID As String In p_modelIDsList
                    If example.modelID = modelID Then
                        rowIndices.Add(i)
                    End If
                Next
                i += 1
            Next
            If rowIndices.Count > 0 Then
                'Select rows
                SelectRowByIndices(dataGrid_ExampleSummary, rowIndices)

                'Apply button action
                If p_buttonToClick IsNot Nothing Then e2eTester.ButtonClick(p_buttonToClick)
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

End Class
