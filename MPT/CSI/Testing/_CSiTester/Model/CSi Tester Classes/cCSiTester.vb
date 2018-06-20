Option Explicit On
Option Strict On

Imports Scripting
Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Files.BatchLibrary
Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.Time.TimeLibrary
Imports MPT.XML.ReaderWriter

Imports CSiTester.cRegTest
Imports CSiTester.cSettings
Imports CSiTester.cExample
Imports CSiTester.cExampleTestSet

Imports CSiTester.cPathSettings
Imports CSiTester.cPathModel

''' <summary>
''' Main class for operating the CSiTester GUI.
''' This class coordinates the actions of RegTest, and all of the other classes in the program. 
''' If an action or property does not belong to a form or a more specific class, it belongs here.
''' </summary>
''' <remarks></remarks>
Public Class cCsiTester
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

    Friend Const CLASS_STRING As String = "cCsiTester"

#Region "Private Classes"
    ''' <summary>
    ''' Subclass that records data for component models of a multi-model example. This is used to construct the parent class later, as the parent class is constructed after all child classes are created.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class cMultiModel
        ''' <summary>
        ''' Model ID before the period. e.g. 250 is the base ID of 250.004.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property baseID As String
        ''' <summary>
        ''' List of model IDs of all component models of the example.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property modelIDs As New List(Of String)
        ''' <summary>
        ''' Paths of all of the models that are components of the example.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property modelPaths As New List(Of String)
    End Class
#End Region

    ' TODO: For prompts (sometimes in "Constants: Private" regions), see about making specialized browsing classes that are used within classes.
    ' This reduces code redundancy and class sizes, and makes the prompt constants private.
#Region "Prompts"
    Private Const _TITLE_RUN_MODELS_DIR_NOT_EXIST As String = "Missing Folder"
    Private Const _PROMPT_RUN_MODELS_DIR_NOT_EXIST As String = "Warning! '" & cSettings.DIR_NAME_MODELS_DESTINATION & _
        "' directory does not exist in the destination directory. Model files must reside within this folder." & vbNewLine & vbNewLine & _
        "A new '" & cSettings.DIR_NAME_MODELS_DESTINATION & "' directory will be created and initialized with models from the source directory."

    Private Const _TITLE_DIRECTORY_MISMATCH As String = "Directory Mismatch"
    Private Const _PROMPT_DIRECTORY_MISMATCH As String = "Warning! Destination model files and control XML files do not match those in the source directory. Files may have been moved or deleted." _
                                               & vbNewLine & vbNewLine & "In order to run tests in the current arrangement, the destination folder needs to be cleared and re-initialized." _
                                               & vbNewLine & vbNewLine & "Do you wish to continue?"

    Private Const _PROMPT_RUN_CHECK_NONE As String = "No examples have been selected to be run or compared."
    Private Const _PROMPT_RUN_CHECK_EXAMPLES_COMPARE As String = "Examples will only be compared."
    Private Const _PROMPT_RUN_CHECK_EXAMPLES_RUN As String = "Examples will only be run."
    Private Const _PROMPT_RUN_CHECK_EXAMPLES_RUN_COMPARE As String = "Running and comparing examples"
    Private Const _PROMPT_RUN_CHECK_EXAMPLES_OUT_OF_SYNC As String = "Out of sync examples will be compared first. Then remaining examples will be run and compared in sync."

    Private Const _PROMPT_CONVERT_MODEL_PATH_TO_DESTINATION_FAIL As String = "Path supplied does not match any part of the source name"
    Private Const _PROMPT_CONVERT_MODEL_PATH_TO_SOURCE_FAIL As String = "Path supplied does not match any part of the destination name"

    Private Const _PROMPT_CRITICAL_MODEL_SOURCE_ERROR As String = "Critical Model Source Error!"

    Private Const _TITLE_NEXT_EXAMPLE_NOT_FOUND As String = "Example Increment Error"
    Private Const _PROMPT_NEXT_EXAMPLE_NOT_FOUND As String = "Next example to run not found in list of examples selected to run."

    Private Const _PROMPT_SPECIFY_MODEL_SOURCE As String = " " & vbNewLine & vbNewLine &
        "Please specify the location of the models to be tested. " & vbNewLine &
        "Models must have corresponding XML files to be used in CSiTester." & vbNewLine & vbNewLine &
        "'Cancel' will exit the program."""
    Private Const _PROMPT_MODEL_SOURCE_NOT_FOUND As String = "Model source directory path cannot be found."
    Private Const _PROMPT_MODEL_SOURCE_NO_VALID_XML As String = "Model source directory does not contain any model XML files."

    Private Const _TITLE_TABLE_EXPORT_SETTINGS As String = "Table Export Settings"
    Private Const _PROMPT_OUTPUTSETTINGS_ACTIVATE As String = "OutputSettings.xml files have been activated for all applicable examples in the run directory."
    Private Const _PROMPT_OUTPUTSETTINGS_DEACTIVATE As String = "OutputSettings.xml files have been deactivated for all examples in the run directory."


    Private Const _TITLE_INVALID_DRIVE As String = "Invalid Drive"
    Private Const _PROMPT_INVALID_DRIVE As String = "Selected path must reside in the same drive as CSiTester. Please select a path from the following drive: "


    ' Clearing Destination Folder
    Friend Const TITLE_CLEAR As String = "Clear Destination Folder"
    Friend Const PROMPT_CLEAR_NOT_WRITEABLE As String = "CSiTester currently does not have write/copy/delete permissions for the specified destination folder. Folder will not be cleared."
    Friend Const PROMPT_CLEAR_SUCCESS As String = "The destination tester directory been cleared. Folders & files will be re-initialized on the next run."
    Friend Const PROMPT_CLEAR_DIRECTORY_NOT_EXIST As String = "The destination directory does not exist. No action will be taken."

    ' Program
    Private Const _TITLE_PROGRAM_BROWSE As String = "Browse for Program"
    Private Const _PROMPT_PROGRAM_BROWSE As String = "Browse for path to the program to be run."
    Private Const _PROMPT_PROGRAM_PATH_INVALID As String = " is not valid. Please select the program file to be run."
    Private Const _PROMPT_PROGRAM_TYPE_INVALID As String = "File selected is not of a valid program type. Please select a valid CSi product."
    Private Const _PROMPT_PROGRAM_TYPE_INVALID_REQUIRED As String = _PROMPT_PROGRAM_TYPE_INVALID & vbNewLine & vbNewLine &
                                                              "'Cancel' will abort the run"
    'Model Source
    Private Const _TITLE_MODEL_SOURCE_INVALID As String = "Source Not Valid"
    Private Const _PROMPT_MODEL_SOURCE_BROWSE As String = "Browse for models source. This is the location from which models are to be copied out of."
    Private Const _PROMPT_MODEL_SOURCE_WITHIN_DESTINATION_DIRECTORY As String = "Source cannot be the same as or lie within the destination directory. Please choose another directory."


    'Model Destination
    Friend Const TITLE_MODEL_DESTINATION_INVALID As String = "Destination Not Valid"
    Private Const _PROMPT_FILE_DESTINATION_BROWSE As String = "Select Destination for File"
    Friend Const PROMPT_MODEL_DESTINATION_BROWSE As String = "Browse for tester directories destination. This is the location to which models are to be copied into and results will be generated."
    Friend Const PROMPT_MODEL_DESTINATION_ACTION As String = "Please specify a location for the models to be copied to."
    Friend Const PROMPT_MODEL_DESTINATION_SAME As String = "Model destination folder must be different from the source folder. " & vbNewLine & PROMPT_MODEL_DESTINATION_ACTION
    Friend Const PROMPT_MODEL_DESTINATION_NOT_EXIST As String = "Specified model destination does not exist. " & vbNewLine & PROMPT_MODEL_DESTINATION_ACTION
    Friend Const PROMPT_MODEL_DESTINATION_NOT_EXIST_PUBLISHED As String = "Model destination directory does not exist. CSiTester cannot save to this location."
    Friend Const PROMPT_MODEL_DESTINATION_NO_PERMISSION As String = "CSiTester does not have rights to write to this folder. " & vbNewLine & PROMPT_MODEL_DESTINATION_ACTION
    Friend Const PROMPT_DESTINATION_AS_SUBDIRECTORY_OF_DESTINATION As String = "Destination cannot be set to a sub-directory created for the model destination."
    Friend Const PROMPT_DESTINATION_WITHIN_SOURCE_DIRECTORY As String = "Destination cannot be the same as or lie within the source directory. Please choose another directory."
#End Region

#Region "Fields"
    ''' <summary>
    ''' Release status of the program, as set in the 'CSiTesterSettings.xml'. This affects various settings and behavior of the program. See cSettings for the related 'csiTesterInstallMethod' property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _programLevel As eCSiTesterLevel

    Private WithEvents _xmlReaderWriter As New cXmlReadWrite()

    ''' <summary>
    ''' The tester installation type to start up with.
    ''' </summary>
    ''' <remarks></remarks>
    Private _csiTesterInstallMethod As eCSiInstallMethod

    Private _example As New cExample
    Private _examplesTestSet As New cExampleTestSet
    Private _examplesList As New ObservableCollection(Of cExample)
    Private _multiModels As New ObservableCollection(Of cMultiModel)
#End Region

#Region "Properties"
    Public Property pathGlobal As String

    '=== General Setup
    ''' <summary>
    ''' Path to the parent directory of where all of the CSiTester files will be copied. This will always contain a directory of model files that have been run or will be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testerDestinationDir As String
    ''' <summary>
    ''' Path to the parent directory where all of the original CSiTester files are located. This may or may not include the directory of source model files.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property testerSourceDir As String
    ''' <summary>
    ''' If 'true', regTest will be recorded with settings to copy model files to the destination directory, and initialize all supporting regTest files. 
    ''' Otherwise, files existing at the destination directory will be used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property initializeModelDestinationFolder As Boolean
    ''' <summary>
    ''' Extension of the CSiTester class that includes properties, routines, and variations of routines unique to the published version of the tester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myCSiTesterPub As cCSiTesterPub
    ''' <summary>
    ''' Extension of the CSiTester class that includes properties, routines, and variations of routines unique to the internal version of the tester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myCSiTesterInt As cCSiTesterInt
    ''' <summary>
    ''' ETABS version that is refleced in the outputSettings.xml filename, such as'V13'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property versionOutputSettings As String

    '=== Lists of Example Paths & Classes
    ''' <summary>
    ''' List of the names of all of the XMLs used for the current session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suiteXMLList As New ObservableCollection(Of String)
    ''' <summary>
    ''' List of the paths of all of the XMLs used for the current session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suiteXMLPathList As New List(Of String)
    ''' <summary>
    ''' List of the different example classifications included in the session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleClassificationList As New ObservableCollection(Of String)
    ''' <summary>
    ''' Path to the previous test results. This is for dynamically changing output locations, such as new directories named after an auto-generated test ID. Otherwise, it is redundant to the path derived from the 'regTest.xml'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathPreviousTestResults As String
    ' ''' <summary>
    ' ''' Collection of classes generated for mulit-model examples.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property multiModels As ObservableCollection(Of cMultiModel)
    ''' <summary>
    ''' Test set generated that only includes examples that have differing results from the recorded benchmarks. Used as a filter for reviewing failed examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property failedExamplesTestSet As New cExampleTestSet

    '=== Check Properties
    ''' <summary>
    ''' Needed to temporarily suppress event notifiers for the datagrid. Else, binding and INotifications in the class interfere with single-click editing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkboxClick As Boolean
    ''' <summary>
    ''' Whether to run an example, compare an example's results, or both.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkType As eCheckType
    ''' <summary>
    ''' Status of whether regTest is still running examples and producing results.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkRunOngoing As Boolean
    ''' <summary>
    ''' Name of the current example being checked, whether it is being run or compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property currentExampleCheckName As String
    ''' <summary>
    ''' If the regTest run fails, this boolean is triggered to generate an informative prompt to the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkRunError As Boolean
    ''' <summary>
    ''' If the regTest program fails, such as the process ending early, or becoming unresponsive, this boolean is triggered to generate an informative prompt to the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regTestFail As Boolean

    '=== Time Properties: Estimated times for doing the run, comparison, and total check. Activated by selecting/deselecting examples to run/compare
    ''' <summary>
    ''' Estimated total time to run all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalRunTimeNum As Double
    ''' <summary>
    ''' Estimated total time to compare all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalCompareTimeNum As Double
    ''' <summary>
    ''' Estimated total time to check all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalCheckTimeNum As Double
    ''' <summary>
    ''' Estimated total time to run all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalRunTime As String
    ''' <summary>
    ''' Estimated total time to compare all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalCompareTime As String
    ''' <summary>
    ''' Estimated total time to check all selected examples (in minutes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property estimatedTotalCheckTime As String

    '=== Run/Compare Properties
    ''' <summary>
    ''' List of model IDs of the examples to be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRunIDs As New List(Of String)
    ''' <summary>
    ''' List of model IDs of the examples that have been run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRanIDs As New List(Of String)
    ''' <summary>
    ''' Collection of examples to be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRunCollection As New List(Of cExample)
    ''' <summary>
    ''' Collection of examples that have been run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRanCollection As New List(Of cExample)
    ''' <summary>
    ''' Number of examples that have been run in the current/latest check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleRunNumLatest As Integer
    ''' <summary>
    ''' List of model IDs of the examples to be compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleCompareIDs As New List(Of String)
    ''' <summary>
    ''' List of model IDs of the examples that have been compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleComparedIDs As New List(Of String)
    ''' <summary>
    ''' List of model IDs of the examples that were not compared even though they were selected to be. Such a case occurs if the comparison is done for an example that has not been run yet.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleNotComparedIDs As New ObservableCollection(Of String)
    ''' <summary>
    ''' Collection of examples to be compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleCompareCollection As New List(Of cExample)
    ''' <summary>
    ''' Collection of examples to be compared, but not run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleCompareCollectionNoSync As New List(Of cExample)
    ''' <summary>
    ''' Collection of examples that have been compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleComparedCollection As New List(Of cExample)
    ''' <summary>
    ''' Number of examples that have been compared in the current/latest check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleCompareNumLatest As Integer
    ''' <summary>
    ''' Next example to be checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property nextExample As New List(Of cExample)
#End Region

#Region "Initialization: General & Examples"
    '=== Initialization
    Friend Sub New()
        Initialize()
    End Sub
    ''' <summary>
    ''' Basic initialization of the class with only the installation method and corresponding defaults set.
    ''' </summary>
    ''' <param name="p_csiTesterInstallMethod">Installation method of CSiTester.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_csiTesterInstallMethod As eCSiInstallMethod)
        Initialize(p_csiTesterInstallMethod)
    End Sub

    Private Sub Initialize(Optional ByVal p_csiTesterInstallMethod As eCSiInstallMethod = eCSiInstallMethod.UseIni)
        _csiTesterInstallMethod = p_csiTesterInstallMethod

        testerSourceDir = pathStartup()

        If testerSettings.initializeModelDestinationFolder Then
            initializeModelDestinationFolder = True
        Else
            initializeModelDestinationFolder = False
        End If
    End Sub

    ''' <summary>
    ''' Initializes much of the class state based on the supplied parameters.
    ''' </summary>
    ''' <param name="p_programLevel">Level of the current instance of CSiTester.</param>
    ''' <param name="p_firstLoad">Specifies if this is the first time the tester is being loaded. 
    ''' Else, subsequent calls will refresh the class.</param>
    ''' <param name="p_testerDestinationDir">Path to the parent directory of where all of the CSiTester files will be copied. 
    ''' This will always contain a directory of model files that have been run or will be run.</param>
    ''' <remarks></remarks>
    Friend Sub Initialize(ByVal p_programLevel As eCSiTesterLevel,
                           ByVal p_firstLoad As Boolean,
                           ByVal p_testerDestinationDir As String)
        'Sets the program level and creates the sub cCSiTester classes, which are correlated with the program level.
        InitializeSpecializedClasses(p_programLevel)

        InitializeCSiTesterData(p_firstLoad)
        AutoTestId()
        InitializeExamples()
        InitializeExamplesAssumedTime()
        UpdateEstimatedTimes()
        InitializeComparedResults()
        testerDestinationDir = p_testerDestinationDir
    End Sub

    ''' <summary>
    ''' Initializes the related cCSiTester classes specific to published or internal use.
    ''' </summary>
    ''' <param name="p_programLevel">Level of the current instance of CSiTester.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeSpecializedClasses(ByVal p_programLevel As eCSiTesterLevel)
        _programLevel = p_programLevel

        Select Case _programLevel
            Case eCSiTesterLevel.Published : myCSiTesterPub = New cCSiTesterPub
            Case Else : myCSiTesterInt = New cCSiTesterInt
        End Select
    End Sub

    ''' <summary>
    ''' Used in startup form to initialize program data. Also used when doing a global refresh of the program data.
    ''' </summary>
    ''' <param name="p_firstLoad">True: Initialization will be done as if first loading the form, clearing and rebuilding all classes, refreshing all views.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeCSiTesterData(Optional ByVal p_firstLoad As Boolean = False)
        Dim initializeFromSettings As Boolean = False

        versionOutputSettings = testerSettings.outputSettingsVersionSession

        Try 'Check if settings file has a list of valid XML files generated in a prior session
            'Load an existing list from the settings file if valid file paths exist 
            If (p_firstLoad AndAlso
                testerSettings.examplePathsSaved.Count > 0 AndAlso
                IO.File.Exists(testerSettings.examplePathsSaved(0))) Then

                suiteXMLPathList.Clear()
                For Each myPath As String In testerSettings.examplePathsSaved
                    suiteXMLPathList.Add(myPath)
                Next

                suiteXMLList.Clear()
                For Each myPath As String In suiteXMLPathList
                    suiteXMLList.Add(GetPathFileName(myPath))
                Next

                exampleRanIDs = testerSettings.examplesRanSaved
                exampleComparedIDs = testerSettings.examplesComparedSaved

                initializeFromSettings = True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))

            ''Populates Tester with list of XMLs and their paths
            'ListXMLFilesInFolder(regTest.models_database_directory, True, True)
        End Try

        'Populates Tester with list of XMLs and their paths. Create a new list from the files in the specified folder
        If Not initializeFromSettings Then UpdateMCFilesProjectList(myRegTest.models_database_directory.path)
    End Sub

    ''' <summary>
    ''' Initializes Dummy set of examples, filled with dummy data.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeExamplesDummy()
        _examplesList = New ObservableCollection(Of cExample)    'Adds collection of examples to test set

        '===Dummy placeholder
        Dim i As Long
        For i = 0 To 50
            _example = New cExample()
            _examplesList.Add(_example)
        Next
        '===
    End Sub


    '=== Initialize Examples & Results
    ''' <summary>
    ''' Initializes all cExample Classes and adds them to the cExampleTestSet class. Groups classes by Classification 2.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExamples()
        Try
            Dim exampleIndex As Integer = 0
            Dim multiModelsPresent As Boolean = False

            examplesTestSetList = New ObservableCollection(Of cExampleTestSet)
            examplesTestSetList.Clear()

            'Generates a collection of XMLS, which are populated by the list of XMLs stored in csiTester
            For Each examplePath As String In suiteXMLPathList

                'Generates an example class and populates most of its properties and sub-classes
                _example = New cExample(examplePath, exampleIndex)

                'Create unique list of multimodels if any exist
                If _example.isMultiModel Then
                    CreateMultiModelGroups(multiModelsPresent, _example.modelID, _example.pathXmlMC)
                    multiModelsPresent = True
                End If

                'Generates the XML class groups for the GUI
                If testerSettings.singleTab Then
                    'Add all examples to only one test set group
                    If exampleIndex = 0 Then
                        'Initialize new test set for first example
                        _examplesTestSet = CreateTestSet(_example)

                        'Add the test set to the collection of test sets
                        examplesTestSetList.Add(_examplesTestSet)
                    Else
                        examplesTestSetList(0).examplesList.Add(_example)
                    End If
                Else
                    'Create Unique Test Set Groups
                    CreateTestSetGroup(_example)
                End If

                exampleIndex += 1
            Next

            'Create MultiModel Parent Classes if any multimodels exist and add 
            'If multiModelsPresent Then CreateMultiModelParents(exampleIndex)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Fills the run time, check time, and total time from the last run of the example.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExamplesAssumedTime()
        Try
            Dim XMLResultsPath As String

            XMLResultsPath = myRegTest.previous_test_results_file.path

            '=== XML Operations ===
            If _xmlReaderWriter.InitializeXML(XMLResultsPath) Then
                ReadExampleXMLLatestResults()  'Populate Data
                _xmlReaderWriter.CloseXML()
            End If
            '=== End XML Operations ===
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Add example class to list of examples for auto-compare on load if it was compared during the last saved session.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeComparedResults()
        Try
            'Get list of examples that had been compared in the last saved session, or if not compared, at least run.
            For Each testSet As cExampleTestSet In examplesTestSetList
                For Each example As cExample In testSet.examplesList
                    If example.comparedExample Then exampleComparedCollection.Add(example)
                    If example.ranExample Then exampleRanCollection.Add(example)
                Next
            Next

            'Create test set to be filled with failed examples, if any.
            'failedExamplesTestSet = New cExampleTestSet

            'Update example results if they have been saved as having been run, but not compared.
            If exampleRanCollection.Count > 0 Then GetExampleResults(exampleRanCollection, failedExamplesTestSet, True)

            'Update example results if they have been saved as having been compared
            If exampleComparedCollection.Count > 0 Then GetExampleResults(exampleComparedCollection, failedExamplesTestSet)

            'SetPropertiesFailedTestSet(failedExamplesTestSet)
            CreateTestSetFailed(failedExamplesTestSet)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub


    '=== Initialize Multi-Model
    ''' <summary>
    ''' Creates a collection of classes that each contain a list of model IDs and their base ID. Used to create parent classes.
    ''' </summary>
    ''' <param name="p_multiModelsPresent">Boolean indicating if multi-models are present. This is 'false' on the first occurrence of a multi-model.</param>
    ''' <param name="p_modelID">Unique model ID.</param>
    ''' <param name="p_modelPath">Path to the model control file.</param>
    ''' <remarks></remarks>
    Private Sub CreateMultiModelGroups(ByVal p_multiModelsPresent As Boolean,
                                       ByVal p_modelID As String,
                                       ByVal p_modelPath As String)
        If Not p_multiModelsPresent Then
            'Create a new collection to hold the classes, and a new class to hold the lists, if this is the first multi-model found
            _multiModels = New ObservableCollection(Of cMultiModel)
            CreateNewMultiModel(p_modelID, p_modelPath)
        Else
            'Create a new class to hold the lists, if this is a new multi-model class
            For Each multiModel As cMultiModel In _multiModels
                'If the existing base model ID exists, add the model to the class
                If multiModel.baseID = GetPrefix(p_modelID, ".") Then
                    multiModel.modelIDs.Add(p_modelID)
                    multiModel.modelPaths.Add(p_modelPath)
                    Exit Sub
                End If
            Next

            'Model base ID does not match any existing base IDs
            'Create a new class, add it to the collection, and populate its properties
            CreateNewMultiModel(p_modelID, p_modelPath)
        End If
    End Sub

    ''' <summary>
    ''' Creates a multi-model class and adds the first set of properties to it.
    ''' </summary>
    ''' <param name="p_modelID">Unique model ID.</param>
    ''' <param name="p_modelPath">Path to the model control file.</param>
    ''' <remarks></remarks>
    Private Sub CreateNewMultiModel(ByVal p_modelID As String,
                                    ByVal p_modelPath As String)
        Dim multiModel As cMultiModel

        multiModel = New cMultiModel

        'Assign base model ID  & first model ID to class
        multiModel.baseID = GetPrefix(p_modelID, ".")
        multiModel.modelIDs.Add(p_modelID)
        multiModel.modelPaths.Add(p_modelPath)

        'Add multimodels class to collection
        _multiModels.Add(multiModel)
    End Sub

    ''' <summary>
    ''' Creates new class that serves as a parent class for the multi-model examples.
    ''' </summary>
    ''' <param name="p_exampleIndex">Example index counter.</param>
    ''' <remarks></remarks>
    Private Sub CreateMultiModelParents(ByRef p_exampleIndex As Integer)
        For Each myMultiModel As cMultiModel In _multiModels
            Dim examplePath As String = ""

            'Generates an example class and populates most of its properties and sub-classes
            examplePath = myMultiModel.modelPaths(0)
            _example = New cExample(examplePath, p_exampleIndex, True, myMultiModel.baseID, myMultiModel.modelIDs, myMultiModel.modelPaths)

            p_exampleIndex += 1

            'Adds example to the appropriate test set
            examplesTestSetList(_example.testSetNumber).examplesList.Add(_example)
        Next
    End Sub


    '=== Initialize Test Sets
    ''' <summary>
    ''' Creates multiple test sets from examples that are already loaded into memory.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateTestSetMultiple()
        Dim tempTestSetList As New ObservableCollection(Of cExampleTestSet)
        Dim tempTestSetFailed As New cExampleTestSet

        'Set aside the 'failed tests' set, and move the rest to the temporary test set list
        For Each myTestSet As cExampleTestSet In examplesTestSetList
            If StringsMatch(myTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                tempTestSetFailed = myTestSet
            Else
                tempTestSetList.Add(myTestSet)
            End If
        Next

        'Initialize new test set list
        examplesTestSetList = New ObservableCollection(Of cExampleTestSet)

        'For each example in the current single test set, create new test sets where applicable and add to the new test set list
        For Each myExample As cExample In tempTestSetList(0).examplesList
            CreateTestSetGroup(myExample, True)
        Next

        'Add the 'failed examples' test set back to the list, if it exists
        If tempTestSetFailed.examplesList.Count > 0 Then
            examplesTestSetList.Add(tempTestSetFailed)
        End If
    End Sub

    ''' <summary>
    ''' Creates single test sets from examples that are already loaded into memory.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateTestSetSingle()
        Dim tempTestSetList As New ObservableCollection(Of cExampleTestSet)
        Dim tempTestSet As New cExampleTestSet
        Dim tempTestSetFailed As New cExampleTestSet

        'Construct new test sets
        tempTestSetList.Add(tempTestSet)

        'Add all examples from split classes into new temp class
        For Each myTestSet As cExampleTestSet In examplesTestSetList

            'Set aside the 'failed examples' test set, if it exists, to preserve it.
            If StringsMatch(myTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                tempTestSetFailed = myTestSet
            Else
                'For all other test sets, transfer the examples to the temporary test set
                For Each myExample As cExample In myTestSet.examplesList
                    tempTestSetList(0).examplesList.Add(myExample)
                Next

            End If
        Next

        'Clear current test set list & assign new temp set to cleared test set list
        examplesTestSetList = New ObservableCollection(Of cExampleTestSet)
        examplesTestSetList = tempTestSetList

        'Add the 'failed examples' test set back to the list, if it exists
        If tempTestSetFailed.examplesList.Count > 0 Then
            examplesTestSetList.Add(tempTestSetFailed)
        End If

    End Sub

    ''' <summary>
    ''' Creates group of unique test sets, which each contain at least one example.
    ''' </summary>
    ''' <param name="p_example">Example class to be added to the test set group.</param>
    ''' <param name="p_fromExistingList">True: A list of classifications has already been created upon first initialization. Compare against this list directly.</param>
    ''' <remarks></remarks>
    Private Sub CreateTestSetGroup(ByRef p_example As cExample,
                                   Optional ByVal p_fromExistingList As Boolean = False)
        Dim exampleClassification As String = p_example.classificationLevel2
        Dim testSetNumber As Integer

        testSetNumber = 0

        'If the classification does not yet have a test set, make a new one and assign the example to it
        If Not exampleListMatch(exampleClassification, testSetNumber, p_fromExistingList) Then                  'Classification is unique
            _examplesTestSet = CreateTestSet(p_example, exampleClassification)                 'Initialize new test set
            examplesTestSetList.Add(_examplesTestSet)                                        'Add the test set to the collection of test sets
        Else
            p_example.testSetIndex = examplesTestSetList(testSetNumber).examplesList.Count    'Sets example index within the test set
            examplesTestSetList(testSetNumber).examplesList.Add(p_example)                    'Add the test set to the collection of test sets
        End If
    End Sub

    ''' <summary>
    ''' Creates a new classification test set and assigns the example class to it.
    ''' </summary>
    ''' <param name="p_example">Example class object to be added as the first example in the test set.</param>
    ''' <param name="p_exampleClassification">Classification to apply to the test set.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateTestSet(ByVal p_example As cExample,
                                   Optional ByVal p_exampleClassification As String = "None") As cExampleTestSet
        _examplesTestSet = New cExampleTestSet                               'Create the new test set

        'Set test set classification by whether it has results data, and whether it contains 'failed examples'
        If p_example.percentDifferenceMax = GetEnumDescription(eResultOverall.notChecked) Then                             'Example is being filled for the first time.
            _examplesTestSet.exampleClassification = p_exampleClassification         'Name the test set as the classification name
        ElseIf CDbl(GetPrefix(p_example.percentDifferenceMax, "%")) > 0 Then    'Test set is one of 'failed examples'
            _examplesTestSet.exampleClassification = GetEnumDescription(eTestSetClassification.FailedExamples)               'Name the test set as 'failed examples'
        Else                                                                    'Example is being filled not for the first time, and is not a 'failed example'
            _examplesTestSet.exampleClassification = p_exampleClassification
        End If

        _examplesTestSet.examplesList.Add(p_example)                           'Add the example to the test set
        _example.testSetIndex = 0                                            'Sets example index within the test set

        CreateTestSet = _examplesTestSet
    End Function

    ''' <summary>
    ''' Checks whether or not a given example classification exists in a list. 
    ''' If so, function returns true and adds the classification to the list. 
    ''' If an index number is provided, it is incremented up.
    ''' </summary>
    ''' <param name="p_exampleClassification">Classification to search.</param>
    ''' <param name="p_testSetNumber">If provided, this increments up by 1 if the classification is not found in the list.</param>
    ''' <param name="p_fromExistingList">True: A list of classifications has already been created upon first initialization. Compare against this list directly.</param>
    ''' <returns>True if the example classification exists in the provided list of classifications. Else, returns False.</returns>
    ''' <remarks></remarks>
    Private Function exampleListMatch(ByVal p_exampleClassification As String,
                                      Optional ByRef p_testSetNumber As Integer = 0,
                                      Optional p_fromExistingList As Boolean = False) As Boolean
        exampleListMatch = False

        If Not p_fromExistingList Then
            'Start list out if it is currently empty
            If myCsiTester.exampleClassificationList.Count = 0 Then
                p_testSetNumber = p_testSetNumber + 1
                myCsiTester.exampleClassificationList.Add(p_exampleClassification)        'Add the new classification name
                Exit Function
            End If

            'Check if example matches classification matches any existing in the list of classifications.
            For Each exampleItem As String In myCsiTester.exampleClassificationList
                If p_exampleClassification = exampleItem Then               'Example classification exists. Set True & Continue
                    exampleListMatch = True
                    Exit Function
                Else                                                        'Example classification does not exist.  Increment number
                    p_testSetNumber = p_testSetNumber + 1
                End If
            Next

            myCsiTester.exampleClassificationList.Add(p_exampleClassification)        'Add the new classification name

        Else
            p_testSetNumber = 0
            For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
                If myExampleTestSet.exampleClassification = p_exampleClassification Then
                    exampleListMatch = True
                    Exit Function
                Else                                                        'Example classification does not exist.  Increment number
                    p_testSetNumber = p_testSetNumber + 1
                End If
            Next
        End If

    End Function

    '=== Misc
    ''' <summary>
    ''' Loads the logging class for recording errors
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadLogger()
        'myLogger = New CLogger

        'SingleFileNotReadOnly("csitester_log_template.xml")
        'My.Computer.FileSystem.CopyFile("csitester_log_template.xml", "csitester_log.xml", True)
        'SingleFileNotReadOnly("csitester_log_template.xml")

        'myLogger.SetLogFilePath("csitester_log.xml")
        'myLogger.SetLogFileStylesheetPath("\CSiTester\regtest\xlst\log.xsl")
    End Sub
#End Region

#Region "Initialization: Models Destination"
    ''' <summary>
    ''' Initializes a new destination directory by copying over the relevant files, generating directories, and refreshing classes. Based on CSiTesterLevel.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="preserveCurrentSettings">Optional: True: Copies settings files from last destination directory to new destination directory. False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="overWriteExisting">Optional: True: Overwrites existing files. False is default.</param>
    ''' <param name="resetSettings">Optional: True: Existing settings files will be overwritten with newly copied source files. All files and folders within the destination directory will be deleted. False: Existing settings files will be preserved.</param>    
    ''' <remarks></remarks>
    Friend Sub InitializeRunningDirectory(ByVal pathDestination As String, Optional ByVal preserveCurrentSettings As Boolean = True, Optional ByVal overWriteExisting As Boolean = False, Optional ByVal resetSettings As Boolean = False)
        If _programLevel = eCSiTesterLevel.Published Then
            myCSiTesterPub.InitializeRunningDirectory(pathDestination, preserveCurrentSettings, overWriteExisting, resetSettings)
        Else
            myCSiTesterInt.InitializeRunningDirectory(pathDestination)
        End If
    End Sub

    '=== The following can apply to source or destination locations, depending on tester level.

    ''' <summary>
    ''' Copies relevant CSiTesterSettings file and class settings. 
    ''' If the tester version is published, this is used for specifying a new desination or resetting current destination to default settings.
    ''' If the tester version is internal, this is for resetting to defaults from seed files.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="preserveCurrentSettings">Optional: True: Copies settings files from last destination directory to new destination directory. False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="overWriteExisting">Optional: True: Overwrites existing files. False is default.</param> 
    ''' <param name="resetSettings">Optional: True: Existing settings files will be overwritten with newly copied source files. False: Existing settings files will be preserved.</param>   
    ''' <remarks></remarks>
    Friend Sub InitializeCSiTesterSettings(ByVal pathDestination As String, Optional ByVal preserveCurrentSettings As Boolean = True, Optional ByVal overWriteExisting As Boolean = False, Optional ByVal resetSettings As Boolean = False)
        If _programLevel = eCSiTesterLevel.Published Then
            myCSiTesterPub.InitializeCSiTesterSettings(pathDestination, preserveCurrentSettings, overWriteExisting, resetSettings)
        Else
            myCSiTesterInt.InitializeCSiTesterSettings(testerSourceDir)                                                         'Note: testerSourceDir instead of destination as files used are local to program, and refreshed from seed files
        End If
    End Sub

    ''' <summary>
    ''' Copies relevant RegTest file and class settings. 
    ''' If the tester version is published, this is used for specifying a new desination or resetting current destination to default settings.
    ''' If the tester version is internal, this is for resetting to defaults from seed files.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="modelsRunDirectoryNew">Directory where models will be copied to and run from.</param>
    ''' <param name="modelsOutputPath">Directory where regTest will place output files from runs.</param>
    ''' <param name="preserveCurrentSettings">Optional: True: Copies settings files from last destination directory to new destination directory. False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="overWriteExisting">Optional: True: Overwrites existing files. False is default.</param>    
    ''' <param name="resetSettings">Optional: True: Existing settings files will be overwritten with newly copied source files. False: Existing settings files will be preserved.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeRegTest(ByVal pathDestination As String, ByVal modelsRunDirectoryNew As String, ByVal modelsOutputPath As String, Optional ByVal preserveCurrentSettings As Boolean = True, _
                          Optional ByVal overWriteExisting As Boolean = False, Optional ByVal resetSettings As Boolean = False)
        If _programLevel = eCSiTesterLevel.Published Then
            myCSiTesterPub.InitializeRegTest(pathDestination, modelsRunDirectoryNew, modelsOutputPath, preserveCurrentSettings, overWriteExisting, resetSettings, _csiTesterInstallMethod)
        Else
            myCSiTesterInt.InitializeRegTest(testerSourceDir, modelsRunDirectoryNew, modelsOutputPath, resetSettings)           'Note: testerSourceDir instead of destination as files used are local to program, and refreshed from seed files
        End If
    End Sub
#End Region

    '=== Misc
#Region "Methods: Model XML Validation"
    ''=== Checks

    ''TODO: Ondrej has a method. Maybe check schema. Write initialization file to speed up, as this is slow to do every time.
    ' ''' <summary>
    ' ''' Confirms that the XML file is a Model XMl file
    ' ''' </summary>
    ' ''' <param name="p_path">Path to the XML file</param>
    ' ''' <returns>True/False</returns>
    ' ''' <remarks></remarks>
    'Friend Function IsModelControlXML(ByVal p_path As String) As Boolean
    '    Dim pathNode As String = "//n:model"
    '    Dim pathNodeAttribute As String = "xmlns"
    '    Dim nodeValue As String = ""

    '    'Check that the file type is valid
    '    If Not StringsMatch(GetSuffix(p_path, "."), "XML") Then Return False

    '    GetSingleXMLNodeValue(p_path, pathNode, nodeValue, pathNodeAttribute)

    '    If nodeValue = "http://www.csiberkeley.com" Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    '=== Validate Model Control XML Files
    ''' <summary>
    ''' Validates the model control XML files in a specified directory against the schema, using regTest. 
    ''' Returns True if all model files pass validation.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateModelControl(ByVal p_validationAction As eRegTestAction,
                                            Optional ByVal p_paths As List(Of String) = Nothing) As Boolean
        ValidateModelControl = True

        Try
            Dim cursorWait As New cCursorWait

            Dim validationPath As String = testerSettings.seedDirectory.path & "\" & testerSettings.exampleValidationFile.fileNameWithExtension
            Dim validationPathNew As String = myRegTest.regTestFile.path & "\" & DIR_NAME_REGTEST & "\" & testerSettings.exampleValidationFile.fileNameWithExtension

            'Copy XML file to location to be used
            If Not CopyFile(validationPath, validationPathNew, True, True) Then Return False

            'Write directory name to XML file
            WriteModelXMLValidationDirToRegTest(validationPathNew)

            'Run regTest validation
            RunRegTest(p_validationAction, testerSettings.exampleValidationFile.fileNameWithExtension, , p_paths)

            'Watch if regTest has problems and abort if it does
            ValidateModelControl = RegTestSuccess()

            cursorWait.EndCursor()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            ValidateModelControl = False
        End Try

    End Function

    ''' <summary>
    ''' Handles running the regTest process and returns whether or not the process exited normally.
    ''' If the process stalls, it is shut down.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RegTestSuccess() As Boolean
        Dim regTestRunning As Boolean = True

        While regTestRunning
            If ProcessIsRunning(PROCESS_REGTEST) Then
                If Not ProcessIsResponding(PROCESS_REGTEST) Then
                    EndProcess(PROCESS_REGTEST)
                    Return False
                End If
            Else
                Return True
            End If
        End While
        Return True
    End Function

    ''' <summary>
    ''' Writes directory path of the model XML files to be checked to the database directory path location of the temp regTest XML file.
    ''' </summary>
    ''' <param name="p_path">Path to the temporary regTest XML file to be written to.</param>
    ''' <remarks></remarks>
    Private Sub WriteModelXMLValidationDirToRegTest(ByVal p_path As String)
        Dim modelXMLDirectory As String = testerSettings.exampleValidationFile.directory
        Dim pathNode As String

        'Models Source Directory
        pathNode = "//n:models_database_directory/n:path"
        _xmlReaderWriter.WriteSingleXMLNodeValue(p_path, pathNode,
                                myRegTest.models_database_directory.pathRelative(modelXMLDirectory))

        'Models Run Directory
        pathNode = "//n:models_run_directory/n:path"
        _xmlReaderWriter.WriteSingleXMLNodeValue(p_path, pathNode,
                                myRegTest.models_run_directory.pathRelative(modelXMLDirectory))

        'Program Path
        pathNode = "//n:program/n:path"
        _xmlReaderWriter.WriteSingleXMLNodeValue(p_path, pathNode,
                                myRegTest.program_file.pathRelative(modelXMLDirectory))
        'writeNodeText(convertedvalue, pathNode, "")


        'RelativePath(modelXMLDirectory, , "\" & dirNameCSiTester & "\" & dirNameRegTest & "\")
        'WriteSingleXMLNodeValue(myPath, "//n:models_database_directory/n:path", convertedValue)
    End Sub

#End Region

#Region "Methods: Model XML Results Updates"
    ''' <summary>
    ''' Generates new regTest XML results for all models, or only models that have failed. If this returns true, then regTest results files have been updated.
    ''' </summary>
    ''' <param name="p_updateAll">True: All models in the current suite will be updated. If false, only the models that failed to generate numerical results will be updated.</param>
    ''' <param name="p_updateModels">List of model IDs for specific models to be updated. Voids any effect of updateAll.</param>
    ''' <remarks></remarks>
    Friend Function UpdateModelResults(ByVal p_updateAll As Boolean,
                                       Optional ByVal p_updateModels As List(Of String) = Nothing) As Boolean
        Dim exampleUpdateXMLPath As String = testerSettings.seedDirectory.path & "\" & testerSettings.exampleUpdateFile.fileNameWithExtension
        Dim updateModels As New List(Of String)

        Try
            If p_updateModels Is Nothing Then                       'Get list of models to check
                If p_updateAll Then                                       'Update results for all models
                    With myRegTest
                        For i = 0 To .model_id.Count - 1
                            updateModels.Add(myRegTest.model_id(i))
                        Next
                    End With
                Else                                                    'Get list of models that have the 'no DB file' error and recheck those.
                    updateModels = GetModelsWithErrors()
                End If
            Else                                                    'Use supplied list of model IDs to check
                updateModels = p_updateModels
            End If

            If Not updateModels.Count = 0 Then 'Update regTest file
                'Write model selections and locations to regTest
                WriteModelXmlUpdateResultToRegTest(exampleUpdateXMLPath, updateModels)

                'Copy XML file to location to be used
                CopyFile(exampleUpdateXMLPath, myRegTest.regTestFile.path & "\" & DIR_NAME_REGTEST & "\" & testerSettings.exampleUpdateFile.fileNameWithExtension, True, True)

                'Run regTest results update
                RunRegTest(eRegTestAction.ResultsUpdateReuseModelList, testerSettings.exampleUpdateFile.fileNameWithExtension)
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Writes directory path of the model XML files to be checked to the models run directory path location of the temp regTest XML file. Also writes the list of models to be updated.
    ''' </summary>
    ''' <param name="p_path">Path to the temporary regTest XML file to be written to.</param>
    ''' <param name="p_selections">List of model ids to use for selection the examples to update.</param>
    ''' <remarks></remarks>
    Private Sub WriteModelXmlUpdateResultToRegTest(ByVal p_path As String,
                                                   ByVal p_selections As List(Of String))
        Dim pathNode As String
        Dim pathNodeAttrib As String = "type"
        Try
            testerSettings.exampleUpdateFile.SetProperties(myRegTest.models_run_directory.path, p_pathUnknown:=True)

            If _xmlReaderWriter.InitializeXML(p_path) Then
                'Set model source location
                pathNode = "//n:models_database_directory/n:path"
                _xmlReaderWriter.WriteNodeText("relative", pathNode, pathNodeAttrib)
                _xmlReaderWriter.WriteNodeText(myRegTest.models_database_directory.pathRelative, pathNode, "")

                'Set model run location
                pathNode = "//n:models_run_directory/n:path"
                _xmlReaderWriter.WriteNodeText("relative", pathNode, pathNodeAttrib)
                _xmlReaderWriter.WriteNodeText(myRegTest.models_run_directory.pathRelative, pathNode, "")

                'Set model results location
                pathNode = "//n:output_directory/n:path"
                _xmlReaderWriter.WriteNodeText("relative", pathNode, pathNodeAttrib)
                _xmlReaderWriter.WriteNodeText(myRegTest.output_directory.pathRelative, pathNode, "")

                'Set program name, version & build
                pathNode = "//n:program/n:name"
                _xmlReaderWriter.WriteNodeText(GetEnumDescription(myRegTest.program_name), pathNode, "")

                pathNode = "//n:program/n:version"
                _xmlReaderWriter.WriteNodeText(myRegTest.program_version, pathNode, "")

                pathNode = "//n:program/n:build"
                _xmlReaderWriter.WriteNodeText(myRegTest.program_build, pathNode, "")

                'Set Test ID to be the same as for the last run
                pathNode = "//n:test_id"
                _xmlReaderWriter.WriteNodeText(myRegTest.test_id, pathNode, "")

                'Set regTest last results location
                pathNode = "//n:previous_test_results_file/n:path"
                _xmlReaderWriter.WriteNodeText("relative", pathNode, pathNodeAttrib)
                _xmlReaderWriter.WriteNodeText(myRegTest.previous_test_results_file.pathRelative, pathNode, "")

                pathNode = "//n:program/n:path"
                _xmlReaderWriter.WriteNodeText("relative", pathNode, pathNodeAttrib)
                _xmlReaderWriter.WriteNodeText(myRegTest.program_file.pathRelative, pathNode, "")

                'Set list of models to update
                pathNode = "//n:testing/n:selections/n:model_ids"

                Dim myList As String()
                ReDim myList(p_selections.Count - 1)
                For i = 0 To p_selections.Count - 1
                    myList(i) = p_selections(i)
                Next

                Dim nameListNode As String = "model_id"
                _xmlReaderWriter.WriteNodeListText(myList, pathNode, nameListNode)

                _xmlReaderWriter.SaveXML(p_path)

                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Gets the list of model IDs for all models that had an error in regTest retrieving results.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetModelsWithErrors() As List(Of String)
        Dim tempList As New List(Of String)
        Dim tempListDBNameChange As New List(Of cExample)
        Dim tempListDBExtension As New List(Of String)

        Try
            For Each example As cExample In failedExamplesTestSet.examplesList
                If StringExistInName(example.runStatus, "error") Then
                    tempList.Add(example.modelID)
                ElseIf StringExistInName(example.compareStatus, "error") Then
                    tempList.Add(example.modelID)
                End If

                'If the database file generated happens to be named differently, correct the table name.
                If Not _programLevel = eCSiTesterLevel.Published Then
                    If (example.compareStatus = GetEnumDescription(eResultCompare.noDBFile) OrElse example.compareStatus = GetEnumDescription(eResultCompare.dbReadFailure)) Then
                        tempListDBNameChange.Add(example)
                        tempListDBExtension.Add(example.outputFileExtension)
                    End If
                End If
            Next

            If Not _programLevel = eCSiTesterLevel.Published Then

                CorrelateDBFileName(tempListDBNameChange, tempListDBExtension)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return tempList
    End Function

    ''' <summary>
    ''' Renames each database file that corresponds to a model file such that the names are the same.
    ''' </summary>
    ''' <param name="p_examples">List of examples where regTest could not find corresponding database files.</param>
    ''' <param name="p_fileExtensions">File extension of the database file to be correlated. The list of extensions has a given extension as being in sync with the examples list.</param>
    ''' <remarks></remarks>
    Private Sub CorrelateDBFileName(ByVal p_examples As List(Of cExample),
                                    ByVal p_fileExtensions As List(Of String))
        Dim fileName As String = ""
        Dim modelDir As String = ""
        Dim filesList As List(Of String)
        Dim DBStructure As Boolean = True
        Dim i As Integer = 0

        Try
            For Each fileExtension As String In p_fileExtensions
                fileExtension = FileExtensionCleanComplete(fileExtension, False)
            Next

            'Check if model file is in DB structure or flattened
            If p_examples.Count = 0 Then Exit Sub
            For Each example As cExample In p_examples
                modelDir = GetPathDirectoryStub(example.pathXmlMC) & "\" & DIR_NAME_MODELS_DEFAULT
                If Not IO.Directory.Exists(modelDir) Then
                    DBStructure = False
                End If
            Next

            'If DBStructure Then                                                             'All model files are in DB structure
            For Each example As cExample In p_examples
                filesList = New List(Of String)
                modelDir = GetPathDirectoryStub(example.pathXmlMC) & "\" & DIR_NAME_MODELS_DEFAULT
                modelDir = myRegTest.models_run_directory.path & FilterStringFromName(modelDir, myRegTest.models_database_directory.path, False, True)

                filesList = ListFilePathsInDirectory(modelDir, False, , p_fileExtensions(i))

                If filesList.Count = 1 Or filesList.Count = 2 Then
                    'Get model name
                    _xmlReaderWriter.GetSingleXMLNodeValue(example.pathXmlMC, "//n:model/n:path", fileName, , True)         'XML model path
                    If StringExistInName(fileName, "\") Then fileName = GetSuffix(fileName, "\") 'Considers if model path only lists model name, or additional path info
                    fileName = GetPrefix(fileName, ".") & p_fileExtensions(i)

                    'Rename database file
                    If filesList.Count = 1 Then
                        RenameFile(modelDir & "\" & filesList(0), fileName)
                    Else                                                                'An exported tables file exists of the correct name, but it is not the one generated by the model.
                        If IO.File.Exists(modelDir & "\" & fileName) Then DeleteFile(modelDir & "\" & fileName)
                        If Not modelDir & "\" & filesList(0) = modelDir & "\" & fileName Then           'Deleted file was not the first one in the list of exported tables files
                            RenameFile(modelDir & "\" & filesList(0), fileName)
                        Else
                            RenameFile(modelDir & "\" & filesList(1), fileName)
                        End If
                    End If
                End If
                i += 1
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: CSiTester Updates"
    ''' <summary>
    ''' Performs all of the operations necessary to update and save CSiTester into the relevant XML files.
    ''' </summary>
    ''' <param name="p_updateCSiTester">Populated in function. True: Form may need to be updated.</param>
    ''' <param name="p_testerDestinationDirectory">Path to the destination directory.</param>
    ''' <param name="p_updateCollectionsCommandLines">True: The collections of selected examples will be re-created, and the command line analysis settins will be updated.</param>
    ''' <remarks></remarks>
    Friend Sub SaveCSiTester(ByRef p_updateCSiTester As Boolean,
                             ByVal p_testerDestinationDirectory As String,
                             Optional ByVal p_updateCollectionsCommandLines As Boolean = True)
        Dim cursorWait As New cCursorWait

        Try
            If p_updateCollectionsCommandLines Then
                'Creates collections and lists of the examples selected to be run and/or compared
                CreateSelectedCollections()

                'Updates command line string
                testerSettings.WriteCommandLineAnalysisSettings()
            End If

            'Saves models selected to the relevant XML files
            If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                Dim updateCSiTester As Boolean = False

                myCSiTesterPub.SaveAndInitializeSettings(updateCSiTester, p_testerDestinationDirectory)

                'CSiTester GUI is updated if the source has been re-specified. Specifying the current source will refresh the GUI.
                If updateCSiTester Then
                    'Reset all examples' compared status
                    ResetExampleResultStatus(False)

                    'Clear Failed Examples
                    RemoveLastFailedTestSet()

                    p_updateCSiTester = True
                End If
            Else
                SaveSettings()
            End If

            p_updateCSiTester = False
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Sets the properties of the class based on values in the settings class.
    ''' </summary>
    ''' <param name="p_setSettings">True: Settings class values updated by CSiTester class values. False: CSiTester class values updated by settings class values.</param>
    ''' <remarks></remarks>
    Friend Sub SyncDestinationInitializationStatus(ByVal p_setSettings As Boolean)
        If Not p_setSettings Then
            initializeModelDestinationFolder = testerSettings.initializeModelDestinationFolder
        Else
            testerSettings.initializeModelDestinationFolder = initializeModelDestinationFolder
        End If
    End Sub

    ''' <summary>
    ''' Updates estimated time properties based on directly updated sub-properties.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateEstimatedTimes()
        Dim exampleCounter As Integer = 0

        estimatedTotalRunTimeNum = 0
        estimatedTotalCompareTimeNum = 0

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            For Each example As cExample In exampleTestSet.examplesList
                If example.runExample Then
                    estimatedTotalRunTimeNum += ConvertTimesNumberMinute(example.timeRunAssumed)
                    exampleCounter += 1
                End If
                If example.compareExample Then estimatedTotalRunTimeNum += ConvertTimesNumberMinute(example.timeCompareAssumed)
            Next
        Next

        estimatedTotalCheckTimeNum = estimatedTotalRunTimeNum + estimatedTotalCompareTimeNum

        'Check for no estimated times
        If estimatedTotalCheckTimeNum = 0 Then      'Apply default of 1 minute per example selected to run
            estimatedTotalRunTimeNum = 1 * exampleCounter
            estimatedTotalCheckTimeNum = 1 * exampleCounter
        Else
            estimatedTotalRunTime = ConvertTimesStringMinutes(estimatedTotalRunTimeNum)
            estimatedTotalCompareTime = ConvertTimesStringMinutes(estimatedTotalCompareTimeNum)
            estimatedTotalCheckTime = ConvertTimesStringMinutes(estimatedTotalCheckTimeNum)
        End If
    End Sub


    ''' <summary>
    ''' Updates the example data as it is read into the program, including updated destination file and results comparisons.
    ''' </summary>
    ''' <param name="p_example">Example object to update.</param>
    ''' <param name="p_updateResults">Updates the regTest results files.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateExampleData(ByVal p_example As cExample,
                                 ByVal p_updateResults As Boolean)
        Dim modelsUpdateList As New List(Of String)

        'Refresh Example as it is read into the suite
        p_example.InitializeExampleData()

        'Copy currently updated example from source to destination
        CopyFile(p_example.pathXmlMC, ConvertPathModelSourceToDestination(p_example.pathXmlMC), True)

        'Run regTest results update on the current example
        If p_updateResults Then
            modelsUpdateList.Add(p_example.modelID)
            UpdateModelResults(False, modelsUpdateList)

            'Causes program to wait until regTest is done
            System.Threading.Thread.Sleep(1000)
            While ProcessIsRunning(PROCESS_REGTEST)
                System.Threading.Thread.Sleep(500)
            End While
        End If

        'Refresh CSiTester
        If p_example.comparedExample Then UpdateSingleExample(p_example)
    End Sub
#End Region


    '=== Path & File Operations
#Region "Methods: Paths"
    ''' <summary>
    ''' Performs various validation checks of the destination directory. If checks fail, the directory is either re-initialized or the run is aborted, depending on the condition and user selections to prompts.
    ''' </summary>
    ''' <param name="p_saveCSiTester">Set by the method to determine if saving operations should be done.</param>
    ''' <param name="p_testerDestinationDir">Destination directory of CSiTester.</param>
    ''' <param name="p_testerLevel">Level of the current instance of CSiTester.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateDestination(ByVal p_testerLevel As eCSiTesterLevel,
                                        ByVal p_testerDestinationDir As String,
                                        ByRef p_saveCSiTester As Boolean) As Boolean
        Dim reInitializeDest As Boolean = False
        Dim checkDestinationMatchSource As Boolean = True

        p_saveCSiTester = False

        ' Check that the destination directory exists
        If Not IO.Directory.Exists(testerDestinationDir) Then
            BrowseModelDestination(, False)
            p_saveCSiTester = True
        End If

        If myRegTest.autoFolders Then
            ' Auto folders actions?
        Else
            If Not initializeModelDestinationFolder Then
                ' Check if 'Models' folder exists (in case user has copied model files to a different directory within the destination and cleared 'Models'
                If Not p_testerLevel = eCSiTesterLevel.Published Then
                    If Not IO.Directory.Exists(myRegTest.models_run_directory.path) Then
                        reInitializeDest = True
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Warning),
                                                           _PROMPT_RUN_MODELS_DIR_NOT_EXIST,
                                                           _TITLE_RUN_MODELS_DIR_NOT_EXIST)
                            Case eMessageActions.OK : checkDestinationMatchSource = False
                            Case eMessageActions.Cancel : Return False
                        End Select
                    End If
                End If

                ' Check if the destination matches the source for the necessary XML and model files. 
                ' If there is not a match, files will be re-copied.
                If (checkDestinationMatchSource AndAlso
                    Not ModelDestinationMatchSource(myRegTest.models_run_directory.path)) Then

                    reInitializeDest = True
                    If Not p_testerLevel = eCSiTesterLevel.Published Then
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Stop),
                                                           _PROMPT_DIRECTORY_MISMATCH,
                                                           _TITLE_DIRECTORY_MISMATCH)
                            Case eMessageActions.Yes : DeleteAllFilesFolders(testerDestinationDir, False)
                            Case eMessageActions.No : Return False
                        End Select
                    End If
                End If
            Else
                reInitializeDest = True
            End If
        End If

        If reInitializeDest Then
            ' Set initialization status & re-initialize destination folders if missing or mismatching.
            initializeModelDestinationFolder = True
            SyncDestinationInitializationStatus(True)
            myRegTest.SetFolderInitialization()

            InitializeRunningDirectory(p_testerDestinationDir, False, True)
            p_saveCSiTester = True
        End If

        Return True
    End Function

    ''' <summary>
    ''' If models are selected to be run, and the program path is not valid, prompt the user to specify a valid program path
    ''' </summary>
    ''' <param name="p_updateProgramData">True: Form program data should be updated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateProgram(ByRef p_updateProgramData As Boolean) As Boolean
        If myRegTest.model_id.Count > 0 Then
            If (Not IO.File.Exists(myRegTest.program_file.path) OrElse
                Not testerSettings.ValidProgram(myRegTest.program_name)) Then

                If Not BrowseProgram(, , True) Then Return False
                If Not testerSettings.ValidProgram(myRegTest.program_name) Then Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Updates data related to the program path specified.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateProgramData()
        'Update regTest & settings properties
        If Not testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then myRegTest.SaveRegTest()
        testerSettings.UpdateSettings(myRegTest)
    End Sub

    ''' <summary>
    ''' Takes a path to a file related to an example, and returns a conversion of the path from the source directory to the destination directory, or the opposite.
    ''' </summary>
    ''' <param name="p_path">Path to convert.</param>
    ''' <param name="p_convertToDestination">Optional. If true, the path is from the source and converted to the destination. If false, the path is from the destination and converted to the source.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConvertPathModelSourceToDestination(ByVal p_path As String,
                                                        Optional ByVal p_convertToDestination As Boolean = True) As String
        Dim pathStub As String


        If p_convertToDestination Then
            'Check if path is already for Destination
            If StringExistInName(p_path, myRegTest.models_run_directory.path) Then Return p_path

            'See where myPath matches the source directory. Take trailing stub.
            pathStub = FilterStringFromName(p_path, myRegTest.models_database_directory.path, False, True)

            If StringsMatch(pathStub, p_path) Then
                RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_CONVERT_MODEL_PATH_TO_DESTINATION_FAIL))
                Return p_path
            End If

            'Append trailing stub to destination directory
            Return myRegTest.models_run_directory.path & pathStub
        Else
            'Check if path is already for Source
            If StringExistInName(p_path, myRegTest.models_database_directory.path) Then Return p_path

            'See where myPath matches the destination directory. Take trailing stub.
            pathStub = FilterStringFromName(p_path, myRegTest.models_run_directory.path, False, True)

            If StringsMatch(pathStub, p_path) Then
                RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_CONVERT_MODEL_PATH_TO_SOURCE_FAIL))
                Return p_path
            End If

            'Append trailing stub to destination directory
            Return myRegTest.models_database_directory.path & pathStub
        End If

    End Function

    ''' <summary>
    ''' Returns a list of all model files that are generated during the import process.
    ''' </summary>
    ''' <param name="example">cExample class associated with the example.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetImportedModelFileNamesList(ByVal example As cExample,
                                                   Optional ByVal noExtension As Boolean = False,
                                                   Optional ByVal noPath As Boolean = False) As List(Of String)
        Dim filesList As New List(Of String)
        Dim filesListTemp As List(Of String)
        Dim destinationDir As String
        Dim fileExtension As String

        If example Is Nothing Then Return filesList

        If testerSettings.fileTypes.Count > 0 Then
            fileExtension = testerSettings.fileTypes(0)
        Else
            fileExtension = "." & GetSuffix(example.pathModelFile, ".")
        End If

        With example
            destinationDir = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(.pathModelFile))

            'Get list of all model files in the folder, without the extension
            filesList = ListFilePathsInDirectory(destinationDir, False, , fileExtension)
            filesListTemp = New List(Of String)
            For Each filePath As String In filesList
                filesListTemp.Add(GetPathFileName(filePath, True))
            Next
            filesList = filesListTemp

            'Refine list to only include model files of the current example
            filesListTemp = New List(Of String)
            For Each modelFile As String In filesList
                If StringExistInName(modelFile, GetPathFileName(.pathModelFile, True)) Then filesListTemp.Add(modelFile)
            Next
            filesList = filesListTemp

            'Check if any of the remaining model files contain any of the import stubs, and if so, add them to the list with the specified details.
            filesListTemp = New List(Of String)
            If noExtension Then fileExtension = ""
            If noPath Then
                destinationDir = ""
            Else
                destinationDir = destinationDir & "\"
            End If
            For Each modelFile As String In filesList
                For Each importTag As String In testerSettings.importTags
                    If StringExistInName(modelFile, importTag) Then
                        If Not ExistsInListString(modelFile, filesListTemp) Then filesListTemp.Add(destinationDir & modelFile & fileExtension)
                    End If
                Next
            Next
            filesList = filesListTemp
        End With

        Return filesList
    End Function

    '=== Browsing For Program, Directories

    ''' <summary>
    ''' Checks path and ensures that it is in the same drive as a compared path. Default compared path is the program path.
    ''' </summary>
    ''' <param name="p_pathNew">Path to check.</param>
    ''' <param name="p_pathCompare">Explicitly defined path to compare. 
    ''' If not specified, the running program path is used.</param>
    ''' <param name="p_canceled">Indicates whether or not the method was canceled.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidDrive(ByVal p_pathNew As String,
                                Optional ByRef p_canceled As Boolean = False,
                                Optional ByVal p_pathCompare As String = "") As Boolean
        If String.IsNullOrEmpty(p_pathCompare) Then p_pathCompare = pathStartup()
        Dim driveCompare As String = Left(p_pathCompare, 1)
        Dim driveNew As String = Left(p_pathNew, 1)

        If StringsMatch(driveNew, driveCompare) Then
            Return True
        Else
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.None),
                                                _PROMPT_INVALID_DRIVE & driveCompare & ":\",
                                                _TITLE_INVALID_DRIVE)
                Case eMessageActions.OK
                Case eMessageActions.Cancel : p_canceled = True
            End Select

            Return False
        End If
    End Function
    ''' <summary>
    ''' Allows user to set path for executable to be run. Program automatically updates CSi program name in regTest based on executable chosen.
    ''' </summary>
    ''' <param name="p_path">Variable to write the path name to.</param>
    ''' <param name="p_currentDirectory">Current opening directory.</param>
    ''' <param name="p_validPathRequired">True means that a user must either select a valid file path or the calling function will end.</param>
    ''' <param name="p_pathExist">Indicates to the function that the current path does not exist.</param>
    ''' <param name="p_paramsList">Function is being used to fetch parameters, but not change the overall program settings. 
    ''' This list is populated with the program path and name.</param>
    ''' <remarks></remarks>
    Friend Function BrowseProgram(Optional ByRef p_path As String = "",
                                  Optional ByVal p_currentDirectory As String = "",
                                  Optional ByVal p_validPathRequired As Boolean = False,
                                  Optional ByRef p_pathExist As Boolean = True,
                                  Optional ByRef p_paramsList As List(Of String) = Nothing) As Boolean
        Dim validPath As Boolean = False
        Dim programPath As String = ""
        Dim newProgramName As String = ""
        Dim newProgramNameEnum As eCSiProgram = eCSiProgram.None
        Dim browseCanceled As Boolean = False
        Dim fileTypes As New List(Of String)

        'Entry warning if a valid path is required.
        If p_validPathRequired Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                        "Path to " & testerSettings.programName & _PROMPT_PROGRAM_PATH_INVALID,
                                                        _TITLE_PROGRAM_BROWSE))
        End If

        'Set up default starting path
        p_currentDirectory = SetAnalysisProgramCurrDir()

        While Not validPath
            'Retains current value if user cancels out of browse form
            fileTypes.Add("exe")
            If Not BrowseForFile(programPath, p_currentDirectory, GetEnumDescription(testerSettings.programName), fileTypes) Then
                programPath = myRegTest.program_file.path
                browseCanceled = True
            End If

            'Extract program type from path
            newProgramName = GetPathFileName(programPath)
            newProgramName = Microsoft.VisualBasic.Left(newProgramName, Len(newProgramName) - 4)
            newProgramNameEnum = ConvertStringToEnumByDescription(Of eCSiProgram)(newProgramName)

            'If program is not valid, prompt the user, and the file browser dialogue will reopen
            If Not testerSettings.ValidProgram(newProgramNameEnum) Then
                If p_validPathRequired Then
                    'A valid path is required. If one is not provided, the function will close with a false value.
                    Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                                        _PROMPT_PROGRAM_TYPE_INVALID_REQUIRED,
                                                        _TITLE_PROGRAM_BROWSE)
                        Case eMessageActions.OK
                            BrowseProgram(, , p_validPathRequired:=True)
                        Case eMessageActions.Cancel
                            Return False
                    End Select
                Else
                    If browseCanceled Then
                        'Don't prompt user to browse for program
                        Return True
                    Else
                        'A valid path is required if selected. If the user opts out of selection, the function will end with a true value.
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Warning),
                                                            _PROMPT_PROGRAM_TYPE_INVALID,
                                                            _TITLE_PROGRAM_BROWSE)
                            Case eMessageActions.OK
                                If Not BrowseProgram() Then
                                    Return True
                                End If
                            Case eMessageActions.Cancel
                                browseCanceled = True
                                Return True
                        End Select

                        'Double check for cancel, in case user opted to cancel in the select case above after first selecting a file. 
                        '(this creates a function within a function to escape)
                        Return True
                    End If
                End If
            Else
                If ValidDrive(programPath, browseCanceled) Then
                    validPath = True
                Else
                    If browseCanceled Then Return True 'Return value use is uncertain.
                End If
            End If
        End While

        'Set Results and close object
        'Validate program name and adjust if user didn't select a program in the optional case
        If p_paramsList Is Nothing Then
            If testerSettings.ValidProgram(newProgramNameEnum) Then
                testerSettings.programName = newProgramNameEnum
            Else
                testerSettings.programName = eCSiProgram.None
            End If
            testerSettings.UpdateProgramDefaults()

            myRegTest.program_name = testerSettings.programName
            myRegTest.program_file.SetProperties(programPath)
            p_path = programPath
        Else
            p_paramsList.Add(programPath)
            p_paramsList.Add(newProgramName)
        End If

        BrowseProgram = True
    End Function

    ''' <summary>
    ''' Allows user to set path for models directory where models are copied to for the run
    ''' </summary>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <param name="p_pathExist">Indicates to the function that the current path does not exist.</param>
    ''' <remarks></remarks>
    Friend Sub BrowseModelDestination(Optional ByRef p_updateCSiTester As Boolean = False,
                                      Optional ByRef p_pathExist As Boolean = True)
        Dim currentOpeningDirectory As String
        Dim pathTemp As String
        Dim pathValidated As Boolean = False
        Dim validationCanceled As Boolean = False

        If Not p_pathExist Then RaiseEvent Messenger(New MessengerEventArgs(PROMPT_MODEL_DESTINATION_NOT_EXIST))

        'Set up default starting path
        currentOpeningDirectory = myCsiTester.SetDestinationCurrDir()

        'Main call to browsing to a folder
        currentOpeningDirectory = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)

        'If user cancels out of form
        If String.IsNullOrEmpty(pathGlobal) Then
            SetBrowseModelDestinationSettings(False, p_updateCSiTester)
            Exit Sub
        Else
            SetBrowseModelDestinationSettings(True, p_updateCSiTester)
        End If

        pathTemp = pathGlobal

        '=== Validate path
        While Not pathValidated
            'If user selects the database directory
            pathValidated = ValidateModelDestinationDBDir(currentOpeningDirectory, pathTemp, p_updateCSiTester, validationCanceled)
            If validationCanceled Then Exit Sub

            'If the user selects the model run directory (or any child directory within) instead of the parent directory
            If pathValidated Then pathValidated = ValidateModelDestinationModelsRunDir(currentOpeningDirectory, pathTemp, p_updateCSiTester, validationCanceled)
            If validationCanceled Then Exit Sub

            'If the folder is not able to be written to
            If pathValidated Then pathValidated = ValidateModelDestinationWriteable(currentOpeningDirectory, pathTemp, p_updateCSiTester, validationCanceled)
            If validationCanceled Then Exit Sub

            'If the user selects any folder within the installation directory
            If _programLevel = eCSiTesterLevel.Published Then
                If pathValidated Then pathValidated = myCSiTesterPub.ValidateModelDestinationDBParentFolder(currentOpeningDirectory, pathTemp, p_updateCSiTester, validationCanceled)
                If validationCanceled Then Exit Sub
            End If

            'If the drive letter is different than CSiTester
            If pathValidated Then pathValidated = ValidDrive(pathTemp, validationCanceled)
            If validationCanceled Then Exit Sub
        End While

        'If the user selects the current destination directory, keep exising path and perform no updates
        If pathTemp = testerDestinationDir Then
            SetBrowseModelDestinationSettings(False, p_updateCSiTester)
            Exit Sub
        End If

        pathTemp = pathGlobal

        'Final treatment of path
        testerDestinationDir = pathTemp
        myRegTest.SetModelsRunDirectory(pathTemp)

        'Update *.ini file and folder initialization status if location has changed to an appropriate path
        If _csiTesterInstallMethod = eCSiInstallMethod.UseIni Then myCSiTesterPub.UpdateModelDestination(pathTemp, p_updateCSiTester)
    End Sub

    ''' <summary>
    ''' Checks to see if the user selects a folder that is not writeable, and prevents this action.
    ''' </summary>
    ''' <param name="p_currentOpeningDirectory">Current opening directory.</param>
    ''' <param name="p_pathTemp">Temporary path being checked</param>
    ''' <param name="p_updateCSiTester">Parameter that indicates that the path has been changed.</param>
    ''' <param name="p_canceled">This is modified by the function to indicate if the function ended by being canceled, or being resolved.</param>
    ''' <remarks></remarks>
    Private Function ValidateModelDestinationWriteable(ByRef p_currentOpeningDirectory As String,
                                                       ByRef p_pathTemp As String,
                                                       Optional ByRef p_updateCSiTester As Boolean = False,
                                                       Optional ByRef p_canceled As Boolean = False) As Boolean
        ValidateModelDestinationWriteable = True

        While Not ValidateDestinationDirectory(True, , p_updateCSiTester)
            ValidateModelDestinationWriteable = False

            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                                PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                TITLE_MODEL_DESTINATION_INVALID)
                Case eMessageActions.OK
                    p_currentOpeningDirectory = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)
                    p_pathTemp = pathGlobal
                    If String.IsNullOrEmpty(pathGlobal) Then
                        SetBrowseModelDestinationSettings(False, p_updateCSiTester)
                        p_canceled = True
                        Exit Function
                    Else
                        SetBrowseModelDestinationSettings(True, p_updateCSiTester)
                    End If
                Case eMessageActions.Cancel
                    p_canceled = True
                    Exit Function
            End Select

            RaiseEvent Messenger(New MessengerEventArgs(PROMPT_MODEL_DESTINATION_NO_PERMISSION))
        End While
    End Function

    ''' <summary>
    ''' Checks to see if the user selects a folder that is the model database directory, and prevents this action.
    ''' </summary>
    ''' <param name="p_currentOpeningDirectory">Current opening directory.</param>
    ''' <param name="p_pathTemp">Temporary path being checked</param>
    ''' <param name="p_updateCSiTester">Optional:Parameter that indicates that the path has been changed.</param>
    ''' <param name="p_canceled">Optional: This is modified by the function to indicate if the function ended by being canceled, or being resolved.</param>
    ''' <remarks></remarks>
    Private Function ValidateModelDestinationDBDir(ByRef p_currentOpeningDirectory As String,
                                                   ByRef p_pathTemp As String,
                                                   Optional ByRef p_updateCSiTester As Boolean = False,
                                                   Optional ByRef p_canceled As Boolean = False) As Boolean
        ValidateModelDestinationDBDir = True

        If StringsMatch(p_pathTemp, myRegTest.models_database_directory.path) Then
            ValidateModelDestinationDBDir = False
            While StringsMatch(p_pathTemp, myRegTest.models_database_directory.path)
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                                PROMPT_MODEL_DESTINATION_SAME,
                                                TITLE_MODEL_DESTINATION_INVALID)
                    Case eMessageActions.OK
                        p_currentOpeningDirectory = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)
                        p_pathTemp = pathGlobal
                        If String.IsNullOrEmpty(pathGlobal) Then
                            SetBrowseModelDestinationSettings(False, p_updateCSiTester)
                            p_canceled = True
                            Exit Function
                        Else
                            SetBrowseModelDestinationSettings(True, p_updateCSiTester)
                        End If
                    Case eMessageActions.Cancel
                        p_canceled = True
                        Exit Function
                End Select
            End While
        End If
    End Function

    ''' <summary>
    ''' Checks to see if the user selects a folder that is the model run directory or within the model run directory, and prevents this action.
    ''' </summary>
    ''' <param name="p_currentOpeningDirectory">Current opening directory.</param>
    ''' <param name="p_pathTemp">Temporary path being checked</param>
    ''' <param name="p_updateCSiTester">Parameter that indicates that the path has been changed.</param>
    ''' <param name="p_canceled">This is modified by the function to indicate if the function ended by being canceled, or being resolved.</param>
    ''' <remarks></remarks>
    Private Function ValidateModelDestinationModelsRunDir(ByRef p_currentOpeningDirectory As String,
                                                          ByRef p_pathTemp As String,
                                                          Optional ByRef p_updateCSiTester As Boolean = False,
                                                          Optional ByRef p_canceled As Boolean = False) As Boolean
        Dim validFolder As Boolean = True
        Dim invalidDirectory As String = myRegTest.models_run_directory.path

        ValidateModelDestinationModelsRunDir = True
        If Not StringsMatch(p_pathTemp, testerDestinationDir) Then
            If StringExistInName(p_pathTemp, invalidDirectory) Then                       'The user has selected the run directory or a child of the run directory
                ValidateModelDestinationModelsRunDir = False
                validFolder = False
                While Not validFolder
                    Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                                        PROMPT_DESTINATION_AS_SUBDIRECTORY_OF_DESTINATION,
                                                        TITLE_MODEL_DESTINATION_INVALID)
                        Case eMessageActions.OK
                            p_currentOpeningDirectory = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)
                            p_pathTemp = pathGlobal
                            If String.IsNullOrEmpty(pathGlobal) Then
                                SetBrowseModelDestinationSettings(False, p_updateCSiTester)
                                p_canceled = True
                                Exit Function
                            Else
                                SetBrowseModelDestinationSettings(True, p_updateCSiTester)
                            End If
                        Case eMessageActions.Cancel
                            p_canceled = True
                            Exit Function
                    End Select
                End While
            End If
        End If
    End Function

    ''' <summary>
    ''' Allows user to set path for models directory from where models are copied from for the run.
    ''' </summary>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <param name="p_pathExist">Indicates to the function that the current path does not exist.</param>
    ''' <remarks></remarks>
    Friend Sub BrowseModelSource(Optional ByRef p_updateCSiTester As Boolean = False,
                                 Optional ByRef p_pathExist As Boolean = True)
        Dim currentOpeningDirectory As String
        Dim pathTemp As String
        Dim pathValidated As Boolean = False
        Dim validationCanceled As Boolean = False

        'Set up default starting path
        currentOpeningDirectory = SetSourceCurrDir()

        currentOpeningDirectory = BrowseForFolder(_PROMPT_MODEL_SOURCE_BROWSE, , p_showNewFolderButton:=False)

        If String.IsNullOrEmpty(pathGlobal) Then
            pathGlobal = myRegTest.models_database_directory.path 'Retains current value if user cancels out of browse form
        Else
            p_updateCSiTester = True
        End If

        pathTemp = pathGlobal

        '=== Validate path
        If Not _programLevel = eCSiTesterLevel.Published Then
            While Not pathValidated
                'If user selects the destination directory or any child directories within
                pathValidated = ValidateModelDestinationDir(currentOpeningDirectory, pathTemp, p_updateCSiTester, validationCanceled)
                If validationCanceled Then
                    p_updateCSiTester = False
                    Exit Sub
                End If

                'If the drive letter is different than CSiTester
                If pathValidated Then pathValidated = ValidDrive(pathTemp, validationCanceled)
                If validationCanceled Then
                    p_updateCSiTester = False
                    Exit Sub
                End If
            End While
        End If

        myRegTest.models_database_directory.SetProperties(pathGlobal)
    End Sub

    ''' <summary>
    ''' Checks to see if the user selects a folder that is the model destination directory, or any sub-directories, and prevents this action.
    ''' </summary>
    ''' <param name="p_currentOpeningDirectory">Current opening directory.</param>
    ''' <param name="p_pathTemp">Temporary path being checked</param>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <param name="p_canceled">This is modified by the function to indicate if the function ended by being canceled, or being resolved.</param>
    ''' <remarks></remarks>
    Private Function ValidateModelDestinationDir(ByRef p_currentOpeningDirectory As String,
                                                 ByRef p_pathTemp As String,
                                                 Optional ByRef p_updateCSiTester As Boolean = False,
                                                 Optional ByRef p_canceled As Boolean = False) As Boolean
        Dim validFolder As Boolean = True
        Dim invalidDirectory As String = testerDestinationDir

        ValidateModelDestinationDir = True

        If StringExistInName(p_pathTemp, invalidDirectory) Then
            'The user has selected the destination directory or a child folder.
            ValidateModelDestinationDir = False
            validFolder = False
            While Not validFolder
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Stop),
                                            _PROMPT_MODEL_SOURCE_WITHIN_DESTINATION_DIRECTORY,
                                            _TITLE_MODEL_SOURCE_INVALID)
                    Case eMessageActions.OK
                        p_currentOpeningDirectory = BrowseForFolder(_PROMPT_MODEL_SOURCE_BROWSE, , p_showNewFolderButton:=True)
                        If String.IsNullOrEmpty(pathGlobal) Then
                            p_canceled = True
                            Exit Function
                        End If
                        If StringExistInName(p_pathTemp, invalidDirectory) Then
                            validFolder = False
                        Else
                            validFolder = True
                            p_pathTemp = pathGlobal
                        End If
                    Case eMessageActions.Cancel
                        p_canceled = True
                        Exit Function
                End Select
            End While
        End If

    End Function

    ''' <summary>
    ''' Queries the model control XML file to determine if the example structure is flattened. False means it is a database structure.
    ''' </summary>
    ''' <param name="p_xmlFilePath">File path to the model control XML file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDirectoryFlattened(ByVal p_xmlFilePath As String) As Boolean
        Dim pathStub As String = ""

        _xmlReaderWriter.GetSingleXMLNodeValue(p_xmlFilePath, "//n:model/n:path", pathStub, , True)

        If StringExistInName(pathStub, "\") Then
            'If Not Left(pathStub, 1) = "\" Then
            '    Return False
            'Else
            'If StringExistInName(Right(pathStub, Len(pathStub) - 1), "\") Then
            '    Return False
            'Else
            '    Return True
            'End If
            'End If
            Return False
        Else
            Return True
        End If
    End Function

    '=== Supporting Routines
    ''' <summary>
    ''' Sets the starting directory for the browser of the model run/destination directory. Returns a string of this directory path.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestinationCurrDir() As String
        Dim currDir As String

        'Order of attempted startup locations is as follows:
        '   1. Current specified path destination (either read from the settings XML, or changed by the user in session)
        '   2. Up one directory from Models run directory in regTest
        '   3. Model source directory in regTest [If Internal]
        '   4. Analysis program location
        '   5. CSiTester location

        currDir = testerDestinationDir
        SetCurrDir(currDir) ', "\CSiTester\regTest")

        If currDir = pathStartup() Then                                                 'Try some other starting locations before allowing pathStartup to be used
            currDir = GetPathDirectorySubStub(myRegTest.models_run_directory.path, 1)          'Try starting one directory above the Models Run directory
            SetCurrDir(currDir) ', "\CSiTester\regTest")
            If currDir = pathStartup() Then
                If Not _programLevel = eCSiTesterLevel.Published Then                    'Try starting at the model source if being used for in-house testing
                    currDir = myRegTest.models_database_directory.path
                    SetCurrDir(currDir)
                End If

                If currDir = pathStartup() Then                                         'Try starting at the specified program
                    currDir = GetPathDirectoryStub(myRegTest.program_file.path)
                    SetCurrDir(currDir)
                End If
            End If
        End If

        Return currDir
    End Function

    ''' <summary>
    ''' Sets the starting directory for the browser of the model source directory. Returns a string of this directory path.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSourceCurrDir() As String
        Dim currDir As String

        'Order of attempted startup locations is as follows:
        '   1. Models source directory in regTest
        '   2. Analysis program location
        '   3. CSiTester location

        currDir = myRegTest.models_database_directory.path
        SetCurrDir(currDir) ', "\CSiTester\regTest")
        If currDir = pathStartup() Then
            If currDir = pathStartup() Then                             'Try starting at the specified program
                currDir = GetPathDirectoryStub(myRegTest.program_file.path)
                SetCurrDir(currDir)
            End If
        End If
        Return currDir
    End Function

    ''' <summary>
    ''' Sets the starting path for the browser of the analysis program. Returns a string of this directory path.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetAnalysisProgramCurrDir() As String
        Dim currDir As String

        'Order of attempted startup locations is as follows:
        '   1. Current specified analysis program source in regTest
        '   2. CSiTester location

        currDir = myRegTest.program_file.path
        SetCurrDir(currDir)           'If currDir is invalid, current CSiTester path is used

        Return currDir
    End Function

    ''' <summary>
    ''' Applies the appropriate settings based on how a path is to be treated, such as reverting to the default path, and indicating/syncing if the path has changed.
    ''' </summary>
    ''' <param name="p_newPath">True: Path is different than the one before browsing. False: The path has not changed after browsing.</param>
    ''' <param name="p_updateCSiTester">Optional:Parameter that indicates that the path has been changed.</param>
    ''' <remarks></remarks>
    Friend Sub SetBrowseModelDestinationSettings(ByVal p_newPath As Boolean,
                                                 Optional ByRef p_updateCSiTester As Boolean = False)
        If p_newPath Then
            p_updateCSiTester = True
            initializeModelDestinationFolder = True
        Else
            pathGlobal = testerDestinationDir 'Retains current value if user cancels out of browse form or a path is invalid
            p_updateCSiTester = False
            initializeModelDestinationFolder = False
        End If

        myCsiTester.SyncDestinationInitializationStatus(True)
        myRegTest.SetFolderInitialization()
    End Sub

    ''' <summary>
    ''' Checks if directory can have files and directories created within it, deleted, and written to. If not, performs various actions.
    ''' </summary>
    ''' <param name="p_browseFolder">True: User will be prompted to correct the problem if a directory is invalid. 
    ''' False: Function will just exit as failed.</param>
    ''' <param name="p_currentOpeningDirectory">Current opening directory. 
    ''' If not specified, the default settings for the destination directory are applied.</param>
    ''' <param name="p_updateCSiTester">Indicates that the path has been changed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateDestinationDirectory(ByVal p_browseFolder As Boolean,
                                                 Optional ByVal p_currentOpeningDirectory As String = "",
                                                 Optional ByRef p_updateCSiTester As Boolean = False) As Boolean
        ValidateDestinationDirectory = False

        If Not ReadableWriteableDeletableDirectory(pathGlobal) Then
            Dim validFolder As Boolean = False

            While Not validFolder
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                            PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                            "File Destination Not Valid"))
                If p_browseFolder Then
                    If String.IsNullOrEmpty(p_currentOpeningDirectory) Then p_currentOpeningDirectory = SetDestinationCurrDir()

                    p_currentOpeningDirectory = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)
                    If String.IsNullOrEmpty(pathGlobal) Then
                        SetBrowseModelDestinationSettings(False, p_updateCSiTester)
                        Exit Function
                    Else
                        SetBrowseModelDestinationSettings(True, p_updateCSiTester)
                    End If
                Else
                    SetBrowseModelDestinationSettings(False, p_updateCSiTester)
                    Exit Function
                End If

                validFolder = ReadableWriteableDeletableDirectory(pathGlobal)
            End While
        End If

        ValidateDestinationDirectory = True
    End Function

    ''' <summary>
    ''' If a path is not valid for any reason, the user is propmpted to specify an appropriate folder and regTest is updated accordingly.
    ''' </summary>
    ''' <param name="p_message">Message to prompt the user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ExceptionModelsSource(ByVal p_message As String) As String

        ExceptionModelsSource = pathGlobal

        'Create prompt for user to enter dialogue to select new valid folder path. Canceling will close the program as this step is critical to the program startup.
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Error),
                                            p_message,
                                            _PROMPT_CRITICAL_MODEL_SOURCE_ERROR)
            Case eMessageActions.OK
                Dim currDir As String
                currDir = pathStartup() & "\" & DIR_NAME_REGTEST
                SetCurrDir(currDir)

                BrowseForFolder(_PROMPT_MODEL_SOURCE_BROWSE, currDir)

                If String.IsNullOrEmpty(pathGlobal) Then
                    'Retains current value if user cancels out of browser form
                    pathGlobal = myRegTest.models_database_directory.path
                Else
                    myRegTest.models_database_directory.SetProperties(pathGlobal)
                End If

                ExceptionModelsSource = pathGlobal
            Case eMessageActions.Cancel
                End
        End Select
    End Function
#End Region

#Region "Methods: File Deleting"
    '===Deleting Files
    ''' <summary>
    ''' Deletes all appropriate files in the models destination folder, depending on CSiTester level.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearDestinationFolder()
        If _programLevel = eCSiTesterLevel.Published Then
            myCSiTesterPub.ClearDestinationFolder(testerDestinationDir)
        Else
            myCSiTesterInt.ClearDestinationFolder(testerDestinationDir)
        End If
    End Sub

    ''' <summary>
    ''' Deletes all additional model files saved during the import process.
    ''' </summary>
    ''' <param name="p_example">cExample class associated with the example.</param>
    ''' <remarks></remarks>
    Private Sub DeleteImportedModelFiles(ByVal p_example As cExample)
        Dim filesList As New List(Of String)

        filesList = GetImportedModelFileNamesList(p_example)

        For Each fileName As String In filesList
            DeleteFile(fileName, True)
        Next
    End Sub

    ''' <summary>
    ''' Deletes existing analysis files. 
    ''' Depending on input, will either delete all existing analysis files, or only those for models known to have been run.
    ''' Returns the number of examples for which analysis files have been deleted.
    ''' </summary>
    ''' <param name="p_deleteAll">True: Specified file types for all examples will be deleted. 
    ''' False: Only examples selected to be run or compared will be considered for the deleting action.</param>
    ''' <remarks></remarks>
    Friend Function DeleteAnalysisFilesExisting(ByVal p_deleteAll As Boolean) As Integer
        Dim modelDir As String
        Dim deleteFile As Boolean = True
        Dim numDelete As Integer = 0

        Dim modelPath As String = ""
        Dim fileName As String

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait

        For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
            If Not StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then            'Done to avoid double-counting examples, as they might be repeated on the failed list.
                For Each myExample As cExample In myExampleTestSet.examplesList
                    If Not p_deleteAll Then
                        If (myExample.runExample OrElse myExample.compareExample) Then
                            deleteFile = True
                        Else
                            deleteFile = False
                        End If
                    End If
                    If deleteFile Then
                        If Not (myExample.runStatus = GetEnumDescription(eResultRun.notRun) OrElse
                                myExample.runStatus = GetEnumDescription(eResultRun.notRunYet)) Then
                            modelDir = GetPathDirectoryStub(ConvertPathModelSourceToDestination(myExample.pathModelFile))

                            'Account for analysis files using an imported file name if the model file was imported
                            If myExample.myMCModel.importedModel Then
                                fileName = myExample.fileNameModelImported
                            Else
                                fileName = GetPathFileName(myExample.pathModelFile, True)
                            End If

                            'Delete analysis files
                            DeleteAnalysisFiles(modelDir,
                                                testerSettings.deleteAnalysisFilesLogWarning,
                                                testerSettings.deleteAnalysisFilesTables,
                                                testerSettings.deleteAnalysisFilesModelText,
                                                testerSettings.deleteAnalysisFilesAll,
                                                fileName,
                                                False,
                                                True,
                                                True)
                            DeleteImportedModelFiles(myExample)

                            If (testerSettings.deleteAnalysisFilesTables AndAlso
                                Not StringsMatch(myExample.outputFileExtension, "mdb")) Then     'Delete exported xml table file

                                Dim filenames As New List(Of String)
                                Dim fileExtensions As New List(Of String)

                                filenames.Add(myExample.outputFileName)
                                fileExtensions.Add(myExample.outputFileExtension)
                                DeleteFilesBulk(modelDir, True, filenames, fileExtensions)
                            End If
                            numDelete += 1
                        End If
                    End If
                Next
            End If
        Next
        cursorWait.EndCursor()

        Return numDelete
    End Function

    ''' <summary>
    ''' Deletes analysis files for all examples set to run, if the destination folder has been initialized.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteAnalysisFilesRunExamples()
        If initializeModelDestinationFolder Then Exit Sub
        'If regTest.SetFolderInitialization Then

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait
        For Each example As cExample In exampleRunCollection
            DeleteAnalysisFilesNextExample(example, True) ', True)
        Next
        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Deletes the specified analysis files from the models destination folder for a given model after each model has been run and processed.
    ''' </summary>
    ''' <param name="p_path">Path to the directory within which the analysis files will be deleted.</param>
    ''' <param name="p_deleteLogFiles">True: The following will also be deleted: *.LOG, *.WRN, *.EKO, *.LOH, *.OUT, *.MON, *.$OG, *.$OS, *.$OT</param>
    ''' <param name="p_deleteTableFiles">True: The following will also be deleted: *.mdb</param>
    ''' <param name="p_deleteOtherFiles">True: The following will also be deleted: *.mdb, *.ebk, *.$et</param>
    ''' <param name="p_deleteAllFiles">True: All files with the model file name will be deleted at the model file location.</param>
    ''' <param name="p_fileName">Name of the file to be deleted.</param>
    ''' <param name="p_useBatchFile">True: Files will be deleted in a separate process using a batch file. 
    ''' False: Files will be deleted with vb.net code from the main program process.</param>
    ''' <param name="p_overrideGlobal">True: Then the files will be deleted regardless of whether the option was selected in the GUI.</param>
    ''' <param name="p_waitCursor">True: The cursor changes to a wait cursor while the function runs.</param>
    ''' <remarks></remarks>
    Private Sub DeleteAnalysisFiles(ByVal p_path As String,
                                    Optional ByVal p_deleteLogFiles As Boolean = False,
                                    Optional ByVal p_deleteTableFiles As Boolean = False,
                                    Optional ByVal p_deleteOtherFiles As Boolean = False,
                                    Optional ByVal p_deleteAllFiles As Boolean = False,
                                    Optional ByVal p_fileName As String = "",
                                    Optional ByVal p_useBatchFile As Boolean = False,
                                    Optional ByVal p_overrideGlobal As Boolean = False,
                                    Optional ByVal p_waitCursor As Boolean = False)
        Dim fileExtensions As New List(Of String)
        Dim filenames As New List(Of String)

        'Exit sub if the delete option is not set, nor being overwritten in this call
        If Not testerSettings.deleteAnalysisFilesStatus AndAlso Not p_overrideGlobal Then Exit Sub

        'TODO: Status bar here in the future
        'Set up wait cursor
        Dim cursorWait As New cCursorWait(p_waitCursor)

        If p_deleteAllFiles Then
            DeleteFilesBulk(p_path, False, filenames, , , , p_waitCursor)
        Else
            fileExtensions = SetDeleteAnalysisFilesExtensionList(p_deleteLogFiles, p_deleteTableFiles, p_deleteOtherFiles)
            filenames.Add(p_fileName)

            'Deletes files by the specified method
            If p_useBatchFile Then
                'Note: Cannot delete specific filenames or filenames w/ extensions, so will not delete example exported tables, or outputSettings files.
                DeleteFilesBatch(p_path, fileExtensions, myRegTest.models_run_directory.path & "\" & FILENAME_BATCH_DELETE)
            Else
                DeleteFilesBulk(p_path, True, filenames, fileExtensions, , , p_waitCursor)
            End If
        End If

        'Set up wait cursor
        cursorWait.EndCursor()

    End Sub

    '''' <param name="deleteSingleExample">Optional: If true, only the example XML directory and subdirectories will be checked for file deletions. If false (default), all destination directories are checked.</param>

    ''' <summary>
    ''' Deletes the analysis results of the last run example.
    ''' </summary>
    ''' <param name="p_example">Optional: Example class object for which the analysis files are to be deleted. If not provided, the default is from nextExample(0) of cCSiTester.</param>
    ''' <param name="p_overrideGlobal">Optional: If true, then the files will be deleted regardless of whether the option was selected in the GUI.</param>
    ''' <param name="p_waitCursor">If true, the cursor changes to a wait cursor while the function runs.</param>
    ''' <remarks></remarks>
    Friend Sub DeleteAnalysisFilesNextExample(Optional ByVal p_example As cExample = Nothing,
                                              Optional ByVal p_overrideGlobal As Boolean = False,
                                              Optional p_waitCursor As Boolean = False) ', Optional ByVal deleteSingleExample As Boolean = False)
        'Exit sub if the delete option is not set, nor being overwritten in this call
        If Not testerSettings.deleteAnalysisFilesStatus Then
            If Not p_overrideGlobal Then
                Exit Sub
            End If
        End If

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait(p_waitCursor)

        'Delete analysis files from last file run

        'Get absolute path to model
        Dim modelDir As String = ""
        Dim fileName As String
        Dim myExample As cExample

        Try
            If p_example Is Nothing Then
                myExample = nextExample(0)
            Else
                myExample = p_example
            End If

            modelDir = GetPathDirectoryStub(ConvertPathModelSourceToDestination(myExample.pathModelFile))

            'Account for analysis files using an imported file name if the model file was imported
            If myExample.myMCModel.importedModel Then
                fileName = myExample.fileNameModelImported
            Else
                fileName = GetPathFileName(myExample.pathModelFile, True)
            End If

            'If Not deleteSingleExample Then         'For deleting analysis files in sync with analysis runs.
            '    DeleteAnalysisFiles(regTest.models_run_directory, testerSettings.deleteAnalysisLogFilesStatus, testerSettings.deleteAnalysisOtherFilesStatus, fileName, False, overrideGlobal)
            '    DeleteImportedModelFiles(myExample)
            'Else
            DeleteAnalysisFiles(modelDir, testerSettings.deleteAnalysisFilesLogWarning, testerSettings.deleteAnalysisFilesTables, testerSettings.deleteAnalysisFilesModelText, testerSettings.deleteAnalysisFilesAll, fileName, False, p_overrideGlobal, p_waitCursor)
            DeleteImportedModelFiles(myExample)

            If (testerSettings.deleteAnalysisFilesTables AndAlso Not myExample.outputFileExtension = "mdb") Then     'Delete exported xml table file
                Dim filenames As New List(Of String)
                Dim fileExtensions As New List(Of String)

                filenames.Add(myExample.outputFileName)
                fileExtensions.Add(myExample.outputFileExtension)
                DeleteFilesBulk(modelDir, True, filenames, fileExtensions)
            End If
            'End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Creates a list of all of the file extension types to delete.
    ''' </summary>
    ''' <param name="p_deleteLogFiles">True: The following will also be deleted: *.LOG, *.WRN, *.EKO, *.LOH, *.OUT, *.MON, *.$OG, *.$OS, *.$OT</param>
    ''' <param name="p_deleteTableFiles">True: The following will also be deleted: *.mdb</param>
    ''' <param name="p_deleteOtherFiles">True: The following will also be deleted: *.mdb, *.ebk, *.$et</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDeleteAnalysisFilesExtensionList(Optional ByVal p_deleteLogFiles As Boolean = False,
                                                         Optional ByVal p_deleteTableFiles As Boolean = False,
                                                         Optional ByVal p_deleteOtherFiles As Boolean = False) As List(Of String)
        Dim tempExtensionList As New List(Of String)

        With tempExtensionList
            'Analysis Files
            .Add("*.ico")
            .Add("*.suo")
            .Add("*.docstates.suo")
            .Add("*.msh")
            .Add("*.$$?")
            .Add("*.BRG")
            .Add("*.BRH")
            .Add("*.C1")
            .Add("*.C2")
            .Add("*.C3")
            .Add("*.C4")
            .Add("*.C5")
            .Add("*.C6")
            .Add("*.C7")
            .Add("*.C8")
            .Add("*.C9")
            .Add("*.C10")
            .Add("*.C11")
            .Add("*.CSE")
            .Add("*.CSG")
            .Add("*.CSJ")
            .Add("*.CSP")
            .Add("*.D1")
            .Add("*.D2")
            .Add("*.D3")
            .Add("*.D4")
            .Add("*.D5")
            .Add("*.D6")
            .Add("*.D7")
            .Add("*.D8")
            .Add("*.D9")
            .Add("*.F1")
            .Add("*.F2")
            .Add("*.F3")
            .Add("*.F4")
            .Add("*.F5")
            .Add("*.F6")
            .Add("*.F7")
            .Add("*.F8")
            .Add("*.F9")
            .Add("*.F10")
            .Add("*.F11")
            .Add("*.FUN")
            .Add("*.HX3")
            .Add("*.HX4")
            .Add("*.HX5")
            .Add("*.HX7")
            .Add("*.HX8")
            .Add("*.ID")
            .Add("*.IDS")
            .Add("*.JCJ")
            .Add("*.JCP")
            .Add("*.JCT")
            .Add("*.JOB")
            .Add("*.K?")
            .Add("*.K??")
            .Add("*.K???")
            .Add("*.K????")
            .Add("*.L3")
            .Add("*.L3M")
            .Add("*.LBK")
            .Add("*.LBL")
            .Add("*.LBM")
            .Add("*.LBN")
            .Add("*.LIH")
            .Add("*.LIM")
            .Add("*.LIN")
            .Add("*.M1")
            .Add("*.M2")
            .Add("*.M3")
            .Add("*.M4")
            .Add("*.M5")
            .Add("*.M6")
            .Add("*.M7")
            .Add("*.M8")
            .Add("*.M9")
            .Add("*.M10")
            .Add("*.M11")
            .Add("*.MAS")
            .Add("*.MTL")
            .Add("*.NPR")
            .Add("*.PAT")
            .Add("*.P1H")
            .Add("*.P1M")
            .Add("*.P1S")
            .Add("*.P3")
            .Add("*.P4")
            .Add("*.P5")
            .Add("*.P6")
            .Add("*.P61")
            .Add("*.P62")
            .Add("*.P7")
            .Add("*.P8")
            .Add("*.P9")
            .Add("*.P9P")
            .Add("*.R")
            .Add("*.RSI")
            .Add("*.RU")
            .Add("*.SCP")
            .Add("*.SEC")
            .Add("*.SEV")
            .Add("*.SFI")
            .Add("*.T3")
            .Add("*.T4")
            .Add("*.T5")
            .Add("*.T6")
            .Add("*.T7")
            .Add("*.T8")
            .Add("*.U3S")
            .Add("*.U4S")
            .Add("*.XMJ")
            .Add("*.XYZ")
            .Add("*.Y")
            .Add("*.Y?")
            .Add("*.Y??")
            .Add("*.xsdm")

            'Stiffness Matrix Dump Files
            .Add("*.TXA")
            .Add("*.TXC")
            .Add("*.TXE")
            .Add("*.TXK")
            .Add("*.TXM")

            'Log Files
            .Add("*GOModel.txt")

            If p_deleteLogFiles Then
                'Log Files
                .Add("*.LOG")
                .Add("*.WRN")
                .Add("*.EKO")
                .Add("*.LOH")
                .Add("*.OUT")
                .Add("*.MON")
                .Add("*.$OG")
                .Add("*.$OS")
                .Add("*.$OT")
                .Add("*.tlog")
            End If

            If p_deleteTableFiles Then
                .Add("*.mdb")
            End If

            If p_deleteOtherFiles Then
                'Other Files
                .Add("*.ebk")
                .Add("*.$et")

                .Add("*.e2k")
                .Add("*.$2k")

                .Add("*.b2k")
                .Add("*.$br")

                .Add("*.f2k")
                .Add("*.$sf")
            End If

        End With

        SetDeleteAnalysisFilesExtensionList = tempExtensionList

    End Function
#End Region

#Region "Methods: File Saving"

    ''' <summary>
    ''' Saves settings to the appropriate XML files.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SaveSettings()
        If Not FileInUseAction(myRegTest.xmlFile.path) Then myRegTest.SaveRegTest()
        If Not FileInUseAction(testerSettings.xmlFile.path) Then testerSettings.Save()
    End Sub

#End Region

    '=== Tests
#Region "Methods: Test Setup"
    '=== Test Setup
    ''' <summary>
    ''' Automatically generates a unique and descriptive test ID.
    ''' </summary>
    ''' <returns>Test ID as a string.</returns>
    ''' <remarks></remarks>
    Friend Function AutoTestId() As String
        Dim testProgram As String
        Dim testVersion As String
        Dim testBuild As String     'Hide for published

        Dim d As New DateTime
        Dim testDate As String
        Dim testTime As String

        Dim testType As String      'Hide for published

        'Set program name
        testProgram = GetEnumDescription(myRegTest.program_name)

        'Set program version if it is specified
        If String.IsNullOrEmpty(myRegTest.program_version) Then
            testVersion = ""
        Else
            testVersion = " v" & myRegTest.program_version
        End If

        'Set program build if it is specified
        If String.IsNullOrEmpty(myRegTest.program_build) Then
            testBuild = ""
        Else
            testBuild = " Build " & myRegTest.program_build
        End If

        'Set date & time components
        d = Now
        testDate = d.ToString("yyyy-MM-dd")
        testTime = "_" & CStr(DateTime.Now.Hour) & "_" & CStr(DateTime.Now.Minute) & "_" & CStr(DateTime.Now.Second)

        testType = myRegTest.test_to_run

        If _programLevel = eCSiTesterLevel.Published Then
            testBuild = ""
            testType = ""
        End If

        AutoTestId = testProgram & testVersion & testBuild & " " & testDate & testTime
    End Function

    ''' <summary>
    ''' Lists file names and paths of .XMLs in the source folder.
    ''' </summary>
    ''' <param name="p_sourceFolderName">Path to the highest level folder to check.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateMCFilesProjectList(ByVal p_sourceFolderName As String)
        Dim xmlFilesList As New List(Of String)

        'If directory cannot be found, user is prompted to select a valid directory
        If Not IO.Directory.Exists(p_sourceFolderName) Then
            p_sourceFolderName = ExceptionModelsSource(_PROMPT_MODEL_SOURCE_NOT_FOUND & _PROMPT_SPECIFY_MODEL_SOURCE)
        End If

        xmlFilesList = cMCGenerator.ListModelControlFilesInFolders(p_sourceFolderName)

        'If directory exists, but no model XMLs exist in the folder
        If xmlFilesList.Count = 0 Then
            p_sourceFolderName = ExceptionModelsSource(_PROMPT_MODEL_SOURCE_NO_VALID_XML & _PROMPT_SPECIFY_MODEL_SOURCE)
            xmlFilesList = cMCGenerator.ListModelControlFilesInFolders(p_sourceFolderName)
        End If

        suiteXMLPathList = New List(Of String)(xmlFilesList)
    End Sub

    ''' <summary>
    ''' Adds/removes outputSettings XML files to/from the activated position of the model location. Optionally can apply a uniform file extension for table exports, either in the activated file, or also in the original file.
    ''' </summary>
    ''' <param name="p_outputSettingsUsedAll">True: OutputSettings files will be copied from the attachments folder to the model location for all examples. 
    ''' If False, all outputSettings files will be deleted from the model location unless they are marked as a supporting file in the model control XML.</param>
    ''' <param name="p_tableExportFileExtensionAll">File extension to enforce over all of the files. If left blank, the file extension in the original file in the attachments folder is used.</param>
    ''' <param name="p_updateAttachments">True: File extension will be written to the original file. If False, the values will only be written to the files at the model location.</param>
    ''' <param name="p_noResultPrompt">True: No prompt is given at the end of the actions. If False, a prompt will appear declaring what was done.</param>
    ''' <remarks></remarks>
    Friend Sub ApplyOutputSettingsActions(ByVal p_outputSettingsUsedAll As Boolean,
                                          ByVal p_tableExportFileExtensionAll As String,
                                          ByVal p_updateAttachments As Boolean,
                                          Optional ByVal p_noResultPrompt As Boolean = False)
        Dim cursorWait As New cCursorWait
        Try
            If p_outputSettingsUsedAll Then
                ' TODO
                'Below does not work as it is only checking examples set to run. First function is needed for the ModelDestinationSource function
                'Validate that destination directory matches source directory
                'myCsiTester.CreateSelectedCollections()
                'If Not myCsiTester.ModelDestinationMatchSource(regTest.models_run_directory) Then
                '    Select Case MessageBox.Show(promptDestinationMismatch, titleDestinationMismatch, MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
                '        Case MessageBoxResult.Yes
                '            DeleteAllFilesFolders(regTest.models_run_directory, True)
                '            CopyFolder(regTest.models_database_directory, regTest.models_run_directory, True)
                '        Case MessageBoxResult.No : Exit Sub
                '    End Select
                'End If

                'Copy all outputSettings.xml files from the attachments folder to the model file level
                For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
                    If Not StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                        For Each myExample As cExample In myExampleTestSet.examplesList
                            myExample.ActivateOutputSettingsXMLFile(p_tableExportFileExtensionAll, p_updateAttachments)
                        Next
                    End If
                Next
            Else
                'Delete all outputSettings.xml files at the model level, unless the file is listed as a supporting file in the MC attachments
                For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
                    If Not StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then
                        For Each myExample As cExample In myExampleTestSet.examplesList
                            myExample.DeactivateOutPutSettingsXMLFile()
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        cursorWait.EndCursor()

        'Declare result
        If Not p_noResultPrompt Then
            Dim message As String = ""
            If p_outputSettingsUsedAll Then
                message = _PROMPT_OUTPUTSETTINGS_ACTIVATE
            Else
                message = _PROMPT_OUTPUTSETTINGS_DEACTIVATE
            End If
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly),
                                                            message,
                                                            _TITLE_TABLE_EXPORT_SETTINGS))
        End If
    End Sub

    '=== Prep for Test Check
    ''' <summary>
    ''' Creates the collections of examples that are selected to be run or compared.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateSelectedCollections()
        exampleRunCollection = New List(Of cExample)           'Collection of examples to be run
        exampleCompareCollection = New List(Of cExample)

        'Get list of examples selected to be run or compared
        GetExamplesRunCompare(exampleRunCollection, exampleCompareCollection)
    End Sub

    ''' <summary>
    ''' Checks list of examples in the suite and adds all examples set to run or be compared to the corresponding list of model IDs. 
    ''' Optionally does this with the example classes as a whole.
    ''' </summary>
    ''' <param name="p_runCollection">Collection that is populated with examples to be run.</param>
    ''' <param name="p_compareCollection">Collection that is populated with examples to be compared.</param>
    ''' <remarks></remarks>
    Friend Sub GetExamplesRunCompare(Optional ByRef p_runCollection As List(Of cExample) = Nothing,
                                     Optional ByRef p_compareCollection As List(Of cExample) = Nothing)
        Dim modelIDsRun As New List(Of String)
        Dim modelIDsCompare As New List(Of String)

        For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
            If Not StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then            'Done to avoid double-counting examples, as they might be repeated on the failed list.
                For Each myExample As cExample In myExampleTestSet.examplesList
                    'If example is set to run, add it to the list
                    If myExample.runExample Then
                        If p_runCollection IsNot Nothing Then p_runCollection.Add(myExample)
                        modelIDsRun.Add(myExample.modelID)
                    End If
                    'If example is set to compare, add it to the list
                    If myExample.compareExample Then
                        If p_compareCollection IsNot Nothing Then p_compareCollection.Add(myExample)
                        modelIDsCompare.Add(myExample.modelID)
                    End If
                Next
            End If
        Next

        exampleRunIDs = modelIDsRun
        exampleCompareIDs = modelIDsCompare
    End Sub

    ''' <summary>
    ''' Checks if the model destination folder has all of the same model files and model control XMLs as the model source, including the same relative file paths from the parent directory. 
    ''' Returns true if so, else, false.
    ''' </summary>
    ''' <param name="p_path">Path of the directory to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ModelDestinationMatchSource(ByVal p_path As String) As Boolean
        Dim destinationMatchSource As Boolean = True
        If exampleRunCollection.Count = 0 Then Return destinationMatchSource

        Dim destinationXMLPathList As New List(Of String)

        'Validate that all XMLs to be run exist at the destination folder
        If _programLevel = eCSiTesterLevel.Published Then
            destinationXMLPathList = cMCGenerator.ListModelControlFilesInFolders(p_path)
            destinationMatchSource = ModelDestinationMatchSourcePublished(destinationXMLPathList)
        Else
            For Each example As cExample In exampleRunCollection
                Dim tempPath As String = ConvertPathModelSourceToDestination(example.pathXmlMC)
                destinationXMLPathList.Add(tempPath)
            Next

            destinationMatchSource = ModelDestinationMatchSourceNotPublished(destinationXMLPathList)
        End If

        If Not destinationMatchSource Then
            ModelDestinationMatchSource = False
            'TODO: Check if models & results folders created but empty
            Exit Function
        End If

        'Check to see if the models specified in the XML files exist in the destination folder
        For Each xmlPath As String In destinationXMLPathList
            Dim filePath As String
            Dim fileName As String = ""

            _xmlReaderWriter.GetSingleXMLNodeValue(xmlPath, "//n:model/n:path", fileName, , True)
            filePath = GetPathDirectoryStub(xmlPath) & "\" & fileName

            If Not IO.File.Exists(filePath) Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns all example statuses back to default. 
    ''' </summary>
    ''' <param name="p_emptyRunStatusOnly">True: limits reset to whether examples had been compared without run results in a prior check, and are not set to be run on the current check.</param>
    ''' <remarks></remarks>
    Friend Sub ResetExampleResultStatus(ByVal p_emptyRunStatusOnly As Boolean)
        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            For Each example As cExample In exampleTestSet.examplesList
                With example
                    If p_emptyRunStatusOnly Then
                        If (.runStatus = GetEnumDescription(eResultRun.notRunYet) OrElse .compareStatus = GetEnumDescription(eResultCompare.notRunYet)) Then
                            .ResetRun(False)
                        End If
                    Else
                        .ResetRun(False)
                    End If
                End With
            Next
        Next
    End Sub
#End Region

#Region "Methods: Private - Test Setup"
    'Test Setup
    ''' <summary>
    ''' Gets the path to the folder containing test results from prior tests. Also updates the path stored in the class.
    ''' </summary>
    ''' <param name="p_nameProgram">Name of the program tested.</param>
    ''' <param name="p_testType">Test type run. Default is "run as is".</param>
    ''' <remarks></remarks>
    Private Function GetPreviousTestResultsPath(ByVal p_nameProgram As String,
                                                Optional ByVal p_testType As String = "run as is") As String
        pathPreviousTestResults = pathStartup() & "/regtest/previous_test_results/" & p_nameProgram & "/" & p_testType
        GetPreviousTestResultsPath = pathPreviousTestResults
    End Function

    ''' <summary>
    ''' Reads the latestResults.xml, populates the matching examples properties with their index in the xml, then populates their last run time data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadExampleXMLLatestResults()
        Dim myResultModelIDList As New List(Of String)
        Dim myRunningAnalysisList As New List(Of String)
        Dim myRetrievingDatabaseResultsList As New List(Of String)
        Dim myActualRuntimeTotalList As New List(Of String)
        Dim timeRunAssumedTemp As String
        Dim timeCompareAssumedTemp As String
        Dim timeCheckAssumedTemp As String


        Dim i As Integer

        'Generate results lists
        GetActualTimes(eTestResults.ModelID, myResultModelIDList)
        GetActualTimes(eTestResults.ActualAnalysisRunTime, myRunningAnalysisList)
        GetActualTimes(eTestResults.ActualDatabaseRetrievalTime, myRetrievingDatabaseResultsList)
        GetActualTimes(eTestResults.ActualTotalRunTime, myActualRuntimeTotalList)

        'Cross-reference lists to model id to assign values to example classes
        For Each myTestSet As cExampleTestSet In examplesTestSetList
            For Each myExample As cExample In myTestSet.examplesList
                i = 0
                timeRunAssumedTemp = ""
                timeCompareAssumedTemp = ""
                timeCheckAssumedTemp = ""

                'Get model index
                For Each resultModelID As String In myResultModelIDList
                    If myExample.modelID = resultModelID Then
                        Exit For
                    ElseIf i = myResultModelIDList.Count - 1 Then   'End of list encountered without a match
                        i = -1                                      '-1 indicates that no match is found. 
                        Exit For
                    End If
                    i = i + 1
                Next

                With myExample
                    'Assign corresponding results
                    If i >= 0 Then
                        .testResultLatestIndex = i
                        timeRunAssumedTemp = ConvertTimesStringMinutes(CDbl(myRunningAnalysisList(.testResultLatestIndex)))
                        timeCompareAssumedTemp = ConvertTimesStringMinutes(CDbl(myRetrievingDatabaseResultsList(.testResultLatestIndex)))
                        timeCheckAssumedTemp = ConvertTimesString(ConvertTimesNumber(.timeRunAssumed) + ConvertTimesNumber(.timeCompareAssumed))
                    End If

                    'Overwrite values written from model control file if the latest results exist
                    If Not String.IsNullOrEmpty(timeRunAssumedTemp) Then .timeRunAssumed = timeRunAssumedTemp
                    If Not String.IsNullOrEmpty(timeCompareAssumedTemp) Then .timeCompareAssumed = timeCompareAssumedTemp
                    If Not String.IsNullOrEmpty(timeCheckAssumedTemp) Then .timeCheckAssumed = timeCheckAssumedTemp

                    'Set defaults for empty values
                    If String.IsNullOrEmpty(.timeRunAssumed) Then .timeRunAssumed = TIME_RUN_ASSUMED_DEFAULT
                    If String.IsNullOrEmpty(.timeCompareAssumed) Then .timeCompareAssumed = TIME_COMPARE_ASSUMED_DEFAULT
                    If String.IsNullOrEmpty(.timeCheckAssumed) Then .timeCheckAssumed = TIME_CHECK_ASSUMED_DEFAULT
                End With
            Next
        Next
    End Sub

    ''' <summary>
    ''' Gets analysis time results from the last run to be used as the assumed time for the next run.
    ''' </summary>
    ''' <param name="p_queryType">'Model' for first looking up the model, then 'ActualRunTime' for retrieving the results.</param>
    ''' <param name="p_list">List to be populated by the text values of multiple child elements.</param>
    ''' <remarks></remarks>
    Private Sub GetActualTimes(ByVal p_queryType As eTestResults,
                               Optional ByRef p_list As List(Of String) = Nothing)
        Dim tagName As String
        Dim singleNodePath As String

        tagName = "model"

        Select Case p_queryType
            Case eTestResults.ModelID
                singleNodePath = "general/model_id"
                _xmlReaderWriter.ReadXmlObjectText(tagName, singleNodePath, p_list)
            Case eTestResults.ActualAnalysisRunTime
                singleNodePath = "general/actual_runtime/running_analysis/minutes"
                _xmlReaderWriter.ReadXmlObjectText(tagName, singleNodePath, p_list)
            Case eTestResults.ActualDatabaseRetrievalTime
                singleNodePath = "general/actual_runtime/retrieving_database_results/minutes"
                _xmlReaderWriter.ReadXmlObjectText(tagName, singleNodePath, p_list)
            Case eTestResults.ActualTotalRunTime
                singleNodePath = "general/actual_runtime/total/minutes"
                _xmlReaderWriter.ReadXmlObjectText(tagName, singleNodePath, p_list)
            Case eTestResults.Other

        End Select
    End Sub

    'Prep for Test Check
    ''' <summary>
    ''' Creates collection listing the XML paths of all of the examples set to only compared and not run.
    ''' </summary>
    ''' <param name="p_compareNoSyncCollection">Collection of examples referenced for populating results.</param>
    ''' <remarks></remarks>
    Private Sub GetExamplesCompareNoAnalysis(ByRef p_compareNoSyncCollection As List(Of cExample))
        Dim compareSyncCollection As New List(Of cExample)
        Dim exampleMatch As Boolean

        'Generate a collection of only synced run/compare cases
        For Each myExampleCompare As cExample In exampleCompareCollection
            For Each myExampleRun As cExample In exampleRunCollection
                If myExampleCompare.modelID = myExampleRun.modelID Then
                    compareSyncCollection.Add(myExampleCompare)
                End If
            Next
        Next

        'Compare the synced compare cases against all compare cases and see where there are non-matching examples. Add these to the non-synced collection.
        For Each myExampleCompare As cExample In exampleCompareCollection
            exampleMatch = False
            For Each myExampleSynced As cExample In compareSyncCollection
                If myExampleCompare.modelID = myExampleSynced.modelID Then
                    exampleMatch = True
                    Exit For
                End If
            Next
            If Not exampleMatch Then
                p_compareNoSyncCollection.Add(myExampleCompare)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Checks if the model destination folder matches the source, as arranged in the published version of CSiTester.
    ''' </summary>
    ''' <param name="p_destinationXMLPathList">List of XML paths in the destination directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModelDestinationMatchSourcePublished(ByVal p_destinationXMLPathList As List(Of String)) As Boolean
        Dim destinationMatchSource As Boolean = False
        Dim destinationXMLNameList As New List(Of String)

        'Create list of valid model XML control files in the folder based on the files in the folder
        For Each xmlPath As String In p_destinationXMLPathList
            destinationXMLNameList.Add(GetPathFileName(xmlPath))
        Next

        'Compare the list of model IDs to run to that of the database directory
        For Each destinationFileName As String In destinationXMLNameList
            destinationMatchSource = False
            For Each exampleRun As cExample In exampleRunCollection
                If destinationFileName = GetPathFileName(exampleRun.pathXmlMC) Then
                    destinationMatchSource = True
                    Exit For
                End If
            Next
            If Not destinationMatchSource Then Exit For
        Next

        Return destinationMatchSource
    End Function

    ''' <summary>
    ''' Checks if the model destination folder matches the source, as arranged in the internal version of CSiTester.
    ''' </summary>
    ''' <param name="p_destinationXMLPathList">List of XML paths in the destination directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModelDestinationMatchSourceNotPublished(ByVal p_destinationXMLPathList As List(Of String)) As Boolean
        Dim destinationMatchSource As Boolean = True

        'Check that the xml paths derived from the database directory exist at the source
        For Each xmlSourcePath As String In suiteXMLPathList
            destinationMatchSource = False
            For Each xmlDestinationPath As String In p_destinationXMLPathList
                If IO.File.Exists(xmlDestinationPath) Then
                    destinationMatchSource = True
                    Exit For
                End If
            Next
            If Not destinationMatchSource Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend - Test Check Running"
    'Runs a Check
    ''' <summary>
    ''' For selected examples, runs the example with RegTest and displays the run results.
    ''' </summary>
    ''' <param name="p_processName">Name of the process started by the function.</param>
    ''' <remarks></remarks>
    Friend Sub CheckController(Optional ByVal p_processName As String = "")
        checkRunError = False
        regTestFail = False

        'Check if action is only running or comparing
        If exampleRunCollection.Count = 0 Then                                  'No models set to run
            If exampleCompareCollection.Count = 0 Then                          'No examples set to be compared
                exampleRunNumLatest = 0
                exampleCompareNumLatest = 0

                RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_RUN_CHECK_NONE))
                checkType = eCheckType.None
                Exit Sub                                                        'Stop routine
            Else                                                                'Examples will only be compared.
                RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_RUN_CHECK_EXAMPLES_COMPARE))
                CheckCompareOnly()
            End If
        ElseIf exampleCompareCollection.Count = 0 Then                          'No models set to be compared
            RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_RUN_CHECK_EXAMPLES_RUN))
            CheckRunOnly()
        Else                                                                    'Models set to be checked and run. Checked line-by-line
            CheckRunCompare()
        End If

        'Temp commented. Seems only to be necessary in initialization and status form, which covers all cases except strict comparison
        'CreateTestSetFailed(failedExamplesTestSet)

        If _programLevel = eCSiTesterLevel.Published Then myCSiTesterPub.SaveAndInitializeSettings(False, testerDestinationDir)
    End Sub

    ''' <summary>
    ''' Only compares results from existing RegTest results files.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CheckCompareOnly()
        exampleRunNumLatest = 0
        exampleCompareNumLatest = 0

        checkType = eCheckType.Compare

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait                                                    'Set up wait cursor
        For Each exampleCompare As cExample In exampleCompareCollection                         'Reset values of the compare examples for future comparison
            exampleCompare.ResetCompare()
        Next
        'TODO: Include below, and modify to work with compared lists, if this is ever able to be canceled, say in a status form display.
        'failedExamplesTestSet = ClearFailedExamplesTestSet()                                    'Clears examples set to run from failed test set, if present, in case run is canceled

        GetExampleResults(exampleCompareCollection, failedExamplesTestSet)               'Compare Examples / Get Results
        CreateTestSetFailed(failedExamplesTestSet)
        CreateFailedTabs()
        cursorWait.EndCursor()                                                              'Set up wait cursor
        'UpdateDBErrors(processName)
    End Sub


    'RegTest Coordination
    ''' <summary>
    ''' Writes a batch file and runs the regTest program with the selected parameters.
    ''' </summary>
    ''' <param name="p_regTestAction">Name of regTest.XML file if calling one other than the default.</param>
    ''' <param name="p_regTestName">Specify which action RegTest is to perform.</param>
    ''' <param name="p_consoleIsNotVisible">Optional: If true, batch run will not be visible from a command console window.</param>
    ''' <param name="p_directories">If provided, this list of 1 or more directory parameter values are appended to the batch file after the regTest action.</param>
    ''' <remarks></remarks>
    Friend Sub RunRegTest(ByVal p_regTestAction As eRegTestAction,
                           Optional ByVal p_regTestName As String = "",
                           Optional ByVal p_consoleIsNotVisible As Boolean = False,
                           Optional ByVal p_directories As List(Of String) = Nothing)

        If p_regTestAction = eRegTestAction.None Then Exit Sub

        Dim pathBatch As String = SetBatchPath("run_verification.bat")

        If (IO.Directory.Exists(myRegTest.regTestFile.path) AndAlso
            WriteRegTestBatchFile(pathBatch, p_regTestName, p_regTestAction, p_directories)) Then

            My.Computer.FileSystem.CurrentDirectory = myRegTest.regTestFile.path
            RunBatch(pathBatch, False, False, p_consoleIsNotVisible)
        End If
    End Sub

    ''' <summary>
    ''' Updates a specific example and updates the count for which example RegTest is running.
    ''' </summary>
    ''' <param name="p_exampleCount">Counter for keeping track of what number in the regTest select list is currently being run.</param>
    ''' <param name="p_waitCursor">True: Cursor changes to a wait cursor while the function runs.</param>
    ''' <remarks></remarks>
    Friend Function UpdateExample(ByRef p_exampleCount As Integer,
                                  Optional ByVal p_waitCursor As Boolean = False) As Boolean
        Dim pathNode As String = "//n:update_gui"
        Dim updateRunOnly As Boolean

        UpdateExample = False
        Dim cursorWait As New cCursorWait(p_waitCursor)

        Try
            If nextExample.Count = 0 Then Exit Function

            If _xmlReaderWriter.ReadNodeText(pathNode) = "yes" Then
                UpdateExample = True

                'Update example run or total result
                If checkType = eCheckType.RunAndCompareNoSync Then
                    If exampleCompareCollection.Count > 0 Then
                        updateRunOnly = False                       'Update particular example
                    Else
                        updateRunOnly = True                        'Update only run results for the example
                    End If

                    For Each example As cExample In exampleCompareCollection
                        If example.modelID = nextExample(0).modelID Then
                            If nextExample(0).runStatus = GetEnumDescription(eResultRun.completedRun) Then nextExample(0).compareStatus = GetEnumDescription(eResultCompare.comparing)
                            GetExampleResults(nextExample, failedExamplesTestSet, updateRunOnly)
                            If Not example.runStatus = GetEnumDescription(eResultRun.timeOut) Then DeleteAnalysisFilesNextExample(example) 'Clear analysis files (if specified). Note done if timed out as results files might still be in use by program.
                            Exit For
                        End If
                    Next
                End If

                p_exampleCount = p_exampleCount + 1
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            cursorWait.EndCursor()
        End Try
    End Function

    ''' <summary>
    ''' Grabs the next example class to be updated. Also updates the name property of the next example to be updated, and the example run status.
    ''' </summary>
    ''' <param name="p_exampleCount">Counter for keeping track of what number in the regTest select list is currently being run</param>
    ''' <param name="p_nextExampleID">Model id of the next example to be run.</param>
    ''' <remarks></remarks>
    Friend Sub GetNextExample(ByRef p_exampleCount As Integer,
                              Optional ByRef p_nextExampleID As String = "")
        Dim nextExampleObject As cExample

        ''Check if at end of example list to run
        'If myExampleCount > exampleRunIDs.Count Then Exit Sub
        Try
            'Update class for next example check
            If p_exampleCount <= exampleRunIDs.Count - 1 Then
                p_nextExampleID = exampleRunIDs(p_exampleCount) 'Gets current model ID
            Else
                Exit Sub
            End If

            nextExample = New List(Of cExample)                 'Prepares spot for next example

            For Each myExample As cExample In exampleRunCollection              'Grabs the corresponding example from the classes list based on the regTest id
                If myExample.modelID = p_nextExampleID Then
                    nextExampleObject = exampleRunCollection(p_exampleCount)
                    nextExampleObject.runStatus = GetEnumDescription(eResultRun.running)
                    nextExample.Add(nextExampleObject)
                    Exit For
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        'Warn user if the next example to be checked was not initialized
        If nextExample.Count = 0 Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                        _PROMPT_NEXT_EXAMPLE_NOT_FOUND,
                                                        _TITLE_NEXT_EXAMPLE_NOT_FOUND))
            Exit Sub
        End If

        'Update status form example check
        currentExampleCheckName = nextExample.Item(0).numberCodeExample
    End Sub

    ''' <summary>
    ''' Cancels regTest and the checking operations. Used if canceling a regTest run from outside of the status form thread.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CancelRegTest()
        If IO.File.Exists(myRegTest.control_xml_file.path) Then
            If WaitUntilFileAvailable(myRegTest.control_xml_file.path) Then
                If _xmlReaderWriter.InitializeXML(myRegTest.control_xml_file.path) Then
                    _xmlReaderWriter.WriteNodeText("yes", "//n:cancel_run")

                    _xmlReaderWriter.SaveXML(myRegTest.control_xml_file.path)
                    _xmlReaderWriter.CloseXML()

                    checkRunOngoing = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Reset the switch, and settings in regTest for the next run. Affects copying over of files from the tester source to tester destination directories.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ResetModelDestinationSwitches()
        If initializeModelDestinationFolder Then              'Reset the switch, and settings in regTest for the next run
            initializeModelDestinationFolder = False
            testerSettings.initializeModelDestinationFolder = False
            myRegTest.SetFolderInitialization()
            If Not FileInUseAction(testerSettings.xmlFile.path) Then testerSettings.SaveFolderInitializationSettings()
            If Not FileInUseAction(myRegTest.xmlFile.path) Then myRegTest.SaveFolderInitializationSettings()
        End If
    End Sub

    ''' <summary>
    ''' Removes 'Running' status from all examples.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ResetRunStatus()
        For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
            If Not StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then            'Done to avoid double-counting examples, as they might be repeated on the failed list.
                For Each myExample As cExample In myExampleTestSet.examplesList
                    If myExample.runStatus = GetEnumDescription(eResultRun.running) Then myExample.runStatus = GetEnumDescription(eResultRun.notRun)
                Next
            End If
        Next
    End Sub
#End Region

#Region "Methods: Private - Test Check Running"
    'Runs a Check
    ''' <summary>
    ''' Only runs regTest. No comparison results will be displayed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckRunOnly()
        exampleRunNumLatest = 0
        exampleCompareNumLatest = 0

        checkType = eCheckType.RunAndCompareNoSync

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait                                                    'Set up wait cursor
        For Each exampleRun As cExample In exampleRunCollection                                 'Reset values of the run examples for future comparison, and delete any existing analysis results
            If initializeModelDestinationFolder Then
                exampleRun.ResetRun(False)
            Else
                exampleRun.ResetRun(True)
            End If
        Next
        failedExamplesTestSet = ClearFailedExamplesTestSet()                                    'Clears examples set to run from failed test set, if present, in case run is canceled
        cursorWait.EndCursor()                                                  'Set up wait cursor

        RunRegTest(eRegTestAction.All, myRegTest.regTestXmlName, True)                               'Run all the actions specified in the RegTest.exe configuration XML file
        StartStatusForm()                                                                       'Compare Examples / Get Results, in sync with RegTest
    End Sub

    ''' <summary>
    ''' Runs and compares selected examples.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckRunCompare()
        exampleRunNumLatest = 0
        exampleCompareNumLatest = 0

        checkType = eCheckType.RunAndCompare

        'Create collection to be filled with examples that are compared before regTest runs
        exampleCompareCollectionNoSync = New List(Of cExample)

        'Prepares the list of models to be checked but not run
        GetExamplesCompareNoAnalysis(exampleCompareCollectionNoSync)
        If exampleCompareCollectionNoSync.Count > 0 Then
            CheckCompareNonSync()
        Else
            RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_RUN_CHECK_EXAMPLES_RUN_COMPARE))
            checkType = eCheckType.RunAndCompareNoSync
        End If

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait                                                       'Set up wait cursor
        For Each exampleRun As cExample In exampleRunCollection                                 'Reset values of the run examples for future comparison, and delete any existing analysis results
            If initializeModelDestinationFolder Then
                exampleRun.ResetRun(False)
            Else
                exampleRun.ResetRun(True)
            End If
        Next
        failedExamplesTestSet = ClearFailedExamplesTestSet()                                    'Clears examples set to run from failed test set, if present, in case run is canceled
        cursorWait.EndCursor()                                                                  'Set up wait cursor

        'Runs regTest & checks
        RunRegTest(eRegTestAction.All, myRegTest.regTestXmlName, True)                                         'Starts regTest and synced run/compare
        StartStatusForm()                                                                       'Compare Examples / Get Results, in sync with RegTest
    End Sub

    ''' <summary>
    ''' Compares examples that are set to be compared but not to run.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCompareNonSync()
        RaiseEvent Messenger(New MessengerEventArgs(_PROMPT_RUN_CHECK_EXAMPLES_OUT_OF_SYNC))

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait                                                     'Set up wait cursor
        For Each exampleCompare As cExample In exampleCompareCollectionNoSync                   'Reset values of the compare examples for future comparison
            exampleCompare.ResetCompare()
        Next
        GetExampleResults(exampleCompareCollectionNoSync, failedExamplesTestSet)         'Compares non-synced examples
        CreateTestSetFailed(failedExamplesTestSet)
        CreateFailedTabs()

        cursorWait.EndCursor()                                                    'Set up wait cursor

        'UpdateDBErrors(processName)
        checkType = eCheckType.RunAndCompareNoSync
    End Sub

    ''' <summary>
    ''' Updates regTest-derived example results if there is a possibility that the database issues have been corrected.
    ''' </summary>
    ''' <param name="p_processName">Name of the regTest process to check.</param>
    ''' <remarks></remarks>
    Private Sub UpdateDBErrors(Optional ByVal p_processName As String = "")
        Dim notRespondingWait As Integer = 5000

        If UpdateModelResults(False) Then
            'Some of the compared examples had errors from an earlier run, which have since been corrected and updated.

            If Not String.IsNullOrEmpty(p_processName) Then
                'Wait until regTest is done
                While ProcessIsRunning(p_processName)
                    If Not ProcessIsResponding(p_processName) Then
                        System.Threading.Thread.Sleep(notRespondingWait)
                        If Not ProcessIsResponding(p_processName) Then Exit While
                    End If
                End While
            End If
            'failedExamplesTestSet = New cExampleTestSet
            GetExampleResults(exampleCompareCollection, failedExamplesTestSet)
        End If
    End Sub

    '=== RegTest Coordination
    ''' <summary>
    ''' Returns one path composed of the list of directory names.
    ''' </summary>
    ''' <param name="p_directories">List of directory names.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertDirectoriesToParameterValues(ByVal p_directories As List(Of String)) As String
        Dim directories As String = ""

        If p_directories IsNot Nothing Then
            For Each directory As String In p_directories
                directories &= Chr(34) & directory & Chr(34) & " "
            Next
        End If

        Return directories
    End Function
#End Region

#Region "Methods: Private - regTest Run"
    ''' <summary>
    ''' Writes the batch file to be called in order to run regTest with command line parameters.
    ''' </summary>
    ''' <param name="p_path">Path to the regTest batch file to run.</param>
    ''' <param name="p_regTestName">Name of the regTest XML file that is to be used to run regTest.</param>
    ''' <param name="p_regTestAction">Action to perform with regTest.</param>
    ''' <param name="p_directories">Directories for regTest to use for certain actions.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WriteRegTestBatchFile(ByVal p_path As String,
                                              ByVal p_regTestName As String,
                                              ByVal p_regTestAction As eRegTestAction,
                                              ByVal p_directories As List(Of String)) As Boolean
        ' Deletes Existing Batch File
        If IO.File.Exists(p_path) Then ComponentDeleteFileAction(p_path)

        ' Writes New Batch File
        Using objWriter As New System.IO.StreamWriter(p_path)

            If Not WriteRegTestBatchPathInitial(p_regTestName, objWriter) Then Return False

            Dim directories As String = ConvertDirectoriesToParameterValues(p_directories)
            Select Case p_regTestAction
                Case eRegTestAction.All
                    objWriter.WriteLine(" --run-actions-from-xml ^")
                Case eRegTestAction.MCValidateDatabase
                    objWriter.WriteLine(" --validate-model-xml-files-in-models-database-directory ^")
                Case eRegTestAction.MCValidateRun
                    objWriter.WriteLine(" --validate-model-xml-files-in-models-run-directory ^")
                Case eRegTestAction.MCValidateCustom
                    If String.IsNullOrEmpty(directories) Then Return False
                    objWriter.WriteLine(" --validate-model-xml-files-in-custom-directory " & directories & " ^")
                Case eRegTestAction.MCUpdateToLatestSchemaCustom
                    If String.IsNullOrEmpty(directories) Then Return False
                    objWriter.WriteLine(" --update-model-control-xml-files-to-the-latest-schema-version-in-custom-directory " & directories & " ^")
                Case eRegTestAction.ValuesUpdateBenchmarks
                    objWriter.WriteLine(" --update-benchmarks-in-model-control-files ^")
                Case eRegTestAction.ValuesUpdateLastBest
                    objWriter.WriteLine(" --update-last-best-values-in-model-control-files ^")
                Case eRegTestAction.ResultsUpdate
                    objWriter.WriteLine(" --update-test-results-xml-file ^")
                Case eRegTestAction.ResultsUpdateReuseModelList
                    objWriter.WriteLine(" --update-test-results-xml-file-reuse-models-list ^")
                Case eRegTestAction.ResultsUpdateFromExcel
                    If String.IsNullOrEmpty(directories) Then Return False
                    objWriter.WriteLine(" --update-excel-results-in-model-control-xml-file-from-excel-file-in-custom-directory " & directories & " ^")
                Case eRegTestAction.ConfigUpdateToLatestSchema
                    objWriter.WriteLine(" --update-conf-file-to-the-latest-schema-version ^")
                Case eRegTestAction.Version
                    objWriter.WriteLine(" --version")
            End Select
        End Using

        Return True
    End Function

    ''' <summary>
    ''' Writes the initial portion of the regTest batch file that is the same regardless of the operations to be done.
    ''' </summary>
    ''' <param name="p_regTestName">Name of the regTest XML file to use for directing the regTest program.</param>
    ''' <param name="objWriter">Current streamwriter that is writing the file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WriteRegTestBatchPathInitial(ByVal p_regTestName As String,
                                                    ByRef objWriter As System.IO.StreamWriter) As Boolean
        Try
            objWriter.WriteLine("""" & myRegTest.regTestFile.path & "\" & "regtest.exe" & """" & "^")

            If _csiTesterInstallMethod = eCSiInstallMethod.NoIni Then
                If String.IsNullOrEmpty(p_regTestName) Then Return False
                objWriter.WriteLine(" --conf-dir " & """" & myRegTest.regTestFile.path & "\" & "regtest" & """" & "^")
                objWriter.WriteLine(" --conf-file " & """" & p_regTestName & """" & "^")
            Else
                objWriter.WriteLine(" --conf-dir " & """" & myRegTest.xmlFile.directory & """" & "^")
                If Not String.IsNullOrEmpty(p_regTestName) Then objWriter.WriteLine(" --conf-file " & """" & p_regTestName & """" & "^")
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Sets the path to the batch file to create.
    ''' </summary>
    ''' <param name="p_fileName">File name for the batch file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetBatchPath(ByVal p_fileName As String) As String
        If _csiTesterInstallMethod = eCSiInstallMethod.NoIni Then
            Return myRegTest.regTestFile.path & "\" & p_fileName
        Else
            Return myRegTest.xmlFile.directory & "\" & p_fileName
        End If
    End Function
#End Region

#Region "Methods: Test Results"
    '=== Test Results

    ''' <summary>
    ''' Updates the results of a single example programmatically.
    ''' </summary>
    ''' <param name="p_example">Example object to be updated.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSingleExample(ByVal p_example As cExample)
        Dim exampleSet As New List(Of cExample)
        Dim ranSavedTemp As New List(Of String)
        Dim comparedSavedTemp As New List(Of String)

        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait                                                    'Set up wait cursor

        Try
            'Backup current list of ran & compared examples
            For Each item As String In testerSettings.examplesRanSaved
                ranSavedTemp.Add(item)
            Next
            For Each item As String In testerSettings.examplesComparedSaved
                comparedSavedTemp.Add(item)
            Next

            'Reset values of the compare examples for future comparison
            exampleSet.Add(p_example)
            p_example.ResetCompare()
            GetExampleResults(exampleSet, failedExamplesTestSet)               'Compare Examples / Get Results

            'Update & restore current list of ran & compared examples
            ranSavedTemp = AddIfNew(ranSavedTemp, p_example.modelID, p_placeFirst:=False).ToList
            testerSettings.examplesRanSaved = ranSavedTemp

            comparedSavedTemp = AddIfNew(comparedSavedTemp, p_example.modelID, p_placeFirst:=False).ToList
            testerSettings.examplesComparedSaved = comparedSavedTemp

            'Set up failed examples tab
            'CheckExamplePassFail(example, failedExamplesTestSet)
            CreateTestSetFailed(failedExamplesTestSet)
            CreateFailedTabs()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        cursorWait.EndCursor()                                                    'Set up wait cursor
        'UpdateDBErrors(processName)
    End Sub

    ''' <summary>
    ''' Populates selected examples with the test results, if available. Also updates results using regTest if results have error and are not synced with the run.
    ''' </summary>
    ''' <param name="p_compareCollection">Collection of examples referenced for populating results.</param>
    ''' <param name="p_failedExamplesTestSet">Test set to populate with failed examples.</param>
    ''' <param name="p_updateRunOnly">True: Only run results are checked. False: Entire example is compared &amp; checked.</param>
    ''' <remarks></remarks>
    Private Sub GetExampleResults(ByVal p_compareCollection As List(Of cExample),
                                  Optional ByRef p_failedExamplesTestSet As cExampleTestSet = Nothing,
                                  Optional ByVal p_updateRunOnly As Boolean = False)
        Try
            For Each myExampleCompare As cExample In p_compareCollection
                'Update properties of the exported table set filename & extension
                myExampleCompare.GetExportedTableNameWExtension()

                'Construct XML results path
                myExampleCompare.SetResultsXMLPath(myRegTest.output_directory.path)

                'Update data from regTest output files
                If Not UpdateFromRegTestResults(myExampleCompare.pathXmlModelResults, p_updateRunOnly, myExampleCompare) Then
                    SetResultsFilesMissing(myExampleCompare)
                End If
                CheckExamplePassFail(myExampleCompare, p_failedExamplesTestSet)
            Next

            'Update settings file to record which examples have been run and/or compared
            UpdateRanComparedLists()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates the example properties from the corresponding model results XML file. Returns true if this is successful, false otherwise.
    ''' </summary>
    ''' <param name="p_xmlResultsPath">Path to the model results XML file.</param>
    ''' <param name="p_updateRunOnly">True: Only run results are checked. False: Entire example is compared &amp; checked.</param>
    ''' <param name="p_exampleCompare">Example object to perform the routine on.</param>
    ''' <remarks></remarks>
    Private Function UpdateFromRegTestResults(ByVal p_xmlResultsPath As String,
                                              ByVal p_updateRunOnly As Boolean,
                                              ByVal p_exampleCompare As cExample) As Boolean
        If IO.File.Exists(p_xmlResultsPath) Then
            If Not FileInUseAction(p_xmlResultsPath) Then
                If _xmlReaderWriter.InitializeXML(p_xmlResultsPath) Then
                    If p_updateRunOnly Then
                        p_exampleCompare.UpdateRun()            'Populate run data
                        exampleRunNumLatest += 1
                    Else
                        p_exampleCompare.UpdateRunAndCompare()  'Populate run & compare Data
                        exampleRunNumLatest += 1
                        exampleCompareNumLatest += 1
                    End If
                    _xmlReaderWriter.CloseXML()
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Updates the list of ran &amp; compared examples.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateRanComparedLists()
        Dim addRanID As Boolean = True
        Dim addComparedID As Boolean = True

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            If Not StringsMatch(exampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples)) Then            'Done to avoid double-counting examples, as they might be repeated on the failed list.
                For Each example As cExample In exampleTestSet.examplesList
                    With example
                        If (Not .runStatus = GetEnumDescription(eResultRun.notRun) OrElse Not .runStatus = GetEnumDescription(eResultRun.notRunYet)) Then
                            If Not ExistsInListString(.modelID, exampleRanIDs) Then exampleRanIDs.Add(.modelID)
                        End If
                        If (Not .compareStatus = GetEnumDescription(eResultCompare.notCompared) OrElse Not .compareStatus = GetEnumDescription(eResultCompare.notRunYet)) Then
                            If Not ExistsInListString(.modelID, exampleComparedIDs) Then exampleComparedIDs.Add(.modelID)
                        End If
                    End With
                Next
            End If
        Next

        'Sync tester
        testerSettings.examplesRanSaved = exampleRanIDs
        testerSettings.examplesComparedSaved = exampleComparedIDs
    End Sub

    ''' <summary>
    ''' Updates properties of the example to indicate a missing output file, and add the example to the failed comparison list.
    ''' </summary>
    ''' <param name="p_exampleCompare">Example object to perform the routine on.</param>
    ''' <remarks></remarks>
    Private Sub SetResultsFilesMissing(ByVal p_exampleCompare As cExample)
        With p_exampleCompare
            .runStatus = GetEnumDescription(eResultRun.outputFileMissing)
            .compareStatus = GetEnumDescription(eResultCompare.outputFileMissing)
        End With

        exampleNotComparedIDs.Add(p_exampleCompare.modelID)
    End Sub

    ''' <summary>
    ''' Begins the status form that displays the regTest log, example being run, and takes over operations of updating example results after each example run.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartStatusForm()
        System.Threading.Thread.Sleep(500)

        'Set status of synced run/compare with RegTest
        checkRunOngoing = True

        'Load status form and new threads that check the RegTest control file
        myStatusForm = Nothing
        myStatusForm = New frmStatus
        myStatusForm.Show()
    End Sub

    ''' <summary>
    ''' Checks examples to see if they are to be added to a failed example list set. Returns pass/fail status, and if a failed test set is provided, updating actions will be performed.
    ''' </summary>
    ''' <param name="p_exampleCompare">Example class to be checked.</param>
    ''' <param name="p_failedExamplesTestSet">Failure set to add any failed classes to. If not provided, this function will only check for failures, but take no action.</param>
    ''' <remarks></remarks>
    Private Function CheckExamplePassFail(ByVal p_exampleCompare As cExample,
                                          Optional ByRef p_failedExamplesTestSet As cExampleTestSet = Nothing) As Boolean
        Dim failedExample As Boolean
        Try
            With p_exampleCompare
                'Determine whether or not the example failed
                If Not StringsMatch(.compareStatus, GetEnumDescription(eResultCompare.notCompared)) Then                                     'Example has been compared.
                    failedExample = False
                    .GetOverallResult(failedExample)
                ElseIf (StringsMatch(.runStatus, GetEnumDescription(eResultRun.timeOut)) OrElse
                        StringsMatch(.runStatus, GetEnumDescription(eResultRun.outputFileMissing)) OrElse
                        String.IsNullOrEmpty(.runStatus)) Then 'Example failed to run properly
                    failedExample = True
                End If
            End With

            'Update the failed examples set and increment the number of examples checked by 1
            If p_failedExamplesTestSet IsNot Nothing Then
                p_failedExamplesTestSet = UpdateFailedExamplesTestSet(p_exampleCompare, failedExample, p_failedExamplesTestSet)
                p_failedExamplesTestSet.numExamplesChecked += 1
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return failedExample
    End Function

    ''' <summary>
    ''' Queries the list of examples set for being compared and returns the number that have no % difference.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NumExamplesPassedCheck() As Integer
        Dim percentDifference As Double
        Dim numberPassed As Integer = 0

        If (Not exampleRunNumLatest = 0 AndAlso Not exampleCompareNumLatest = 0) Then
            If exampleCompareCollection IsNot Nothing Then
                For Each comparedExample As cExample In exampleCompareCollection
                    If StringExistInName(comparedExample.percentDifferenceMax, "%") Then
                        percentDifference = CDbl(Left(comparedExample.percentDifferenceMax, Len(comparedExample.percentDifferenceMax) - 1))     'Extract Number from % Difference
                        If percentDifference = 0 Then numberPassed = numberPassed + 1
                    End If
                Next
            End If
        End If

        Return numberPassed
    End Function

    ''' <summary>
    ''' Queries the list of all examples and returns the number that have no % difference.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NumExamplesPassedAll() As Integer
        Dim percentDifference As Double
        Dim numberPassed As Integer

        For Each exampleTestSet As cExampleTestSet In examplesTestSetList
            For Each example As cExample In exampleTestSet.examplesList
                If StringExistInName(example.percentDifferenceMax, "%") Then
                    percentDifference = CDbl(Left(example.percentDifferenceMax, Len(example.percentDifferenceMax) - 1))     'Extract Number from % Difference
                    If percentDifference = 0 Then numberPassed = numberPassed + 1
                End If
            Next
        Next

        Return numberPassed
    End Function


#End Region

#Region "Methods: Failed Examples"

    ''' <summary>
    ''' Returns an updated failed example test set that has removed examples set to run that were never run. This removes them from the failed test set if they were set to run and the run was canceled.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ClearFailedExamplesTestSet() As cExampleTestSet
        Dim tempTestSet As New cExampleTestSet
        Dim exampleRemove As Boolean = False

        For Each myTestSet As cExampleTestSet In examplesTestSetList
            If Not String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then                                 'Test set is a failed test set
                For Each failedExample As cExample In myTestSet.examplesList
                    For Each runExample As cExample In exampleRunCollection
                        If runExample.modelID = failedExample.modelID Then                      'Example in failed example is set to run and has been cleared. Check to see if the example ran or not.
                            If runExample.runStatus = GetEnumDescription(eResultRun.notRun) Then                  'Example did not run. Remove it from the test set.
                                exampleRemove = True
                            Else                                                                    'Example ran. Maintain the example.
                                tempTestSet.examplesList.Add(runExample)
                            End If
                            Exit For
                        Else
                            tempTestSet.examplesList.Add(failedExample)                         'Example in failed example is not set to run. Maintain the example.
                        End If
                    Next
                Next
                Exit For
            End If
        Next

        Return tempTestSet

    End Function

    ''' <summary>
    ''' Updates the class failed examples test set based on the provided example and its pass/fail status. This includes removing failures that now pass, and ensuring a unique list.
    ''' </summary>
    ''' <param name="p_exampleCompare">Example object to use for updating the list.</param>
    ''' <param name="p_failedExample">Status of whether or not the provided example object passed or failed testing checks.</param>
    ''' <param name="p_failedExamplesTestSet">Failure set to add any failed classes to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateFailedExamplesTestSet(ByRef p_exampleCompare As cExample,
                                                 ByVal p_failedExample As Boolean,
                                                 ByRef p_failedExamplesTestSet As cExampleTestSet) As cExampleTestSet
        Dim tempFailedExamples As New cExampleTestSet
        Dim tempFailedExamplesUnique As New cExampleTestSet
        Dim exampleMatch As Boolean = False

        If Not p_failedExample Then
            'Account for example failing in the past by creating a new failed examples list without it
            For Each example As cExample In p_failedExamplesTestSet.examplesList
                If Not p_exampleCompare.modelID = example.modelID Then
                    tempFailedExamples.examplesList.Add(example)                            'Add non-matching failed examples
                End If
            Next
        Else    'Example failed
            For Each example As cExample In p_failedExamplesTestSet.examplesList
                'Add failed example to the list, but if it already exists, make sure to replace the existing one
                If Not p_exampleCompare.modelID = example.modelID Then
                    tempFailedExamples.examplesList.Add(example)                            'Add non-matching failed examples
                Else
                    tempFailedExamples.examplesList.Add(p_exampleCompare)                   'Add matching new failed example instead of the old one
                    exampleMatch = True
                End If
            Next
            If Not exampleMatch Then
                tempFailedExamples.examplesList.Add(p_exampleCompare)                       'Add new failed example if it has not been added yet due to being a newly failed example
            End If
        End If

        'Ensure that the list of examples only contains one of each example
        For Each example As cExample In tempFailedExamples.examplesList
            If tempFailedExamplesUnique.examplesList.Count = 0 Then
                tempFailedExamplesUnique.examplesList.Add(example)
            Else
                exampleMatch = False
                For Each exampleUnique In tempFailedExamplesUnique.examplesList
                    If exampleUnique.modelID = example.modelID Then exampleMatch = True
                Next
                If Not exampleMatch Then tempFailedExamplesUnique.examplesList.Add(example)
            End If
        Next

        Return tempFailedExamplesUnique
    End Function

    ''' <summary>
    ''' Removes any existing test set class and adds the new one to the test set list.
    ''' </summary>
    ''' <param name="p_failedExamplesTestSet">Failure set to add any failed classes to.</param>
    ''' <remarks></remarks>
    Friend Sub CreateTestSetFailed(ByRef p_failedExamplesTestSet As cExampleTestSet)
        Dim failedTestSetsRemain As Boolean = True

        'Sets the unset properties of the test set that are unique to the new test result summary.
        SetPropertiesFailedTestSet(p_failedExamplesTestSet)

        'Check test set list for existing failed example test set
        If p_failedExamplesTestSet.examplesList.Count > 0 Then
            'Remove any potential failed test sets. Remove until none are left.
            While failedTestSetsRemain
                failedTestSetsRemain = RemoveLastFailedTestSet()
            End While

            'Add test set to test set list
            examplesTestSetList.Add(p_failedExamplesTestSet)
        End If
    End Sub

    ''' <summary>
    ''' Sets the unset properties of the test set that are unique to a test result summary.
    ''' </summary>
    ''' <param name="p_failedExamplesTestSet">Failure set to add any failed classes to.</param>
    ''' <remarks></remarks>
    Private Sub SetPropertiesFailedTestSet(ByRef p_failedExamplesTestSet As cExampleTestSet)
        Dim currentMaxPercDiff As Double = 0
        Dim overallStatus As String = ""

        Try
            With p_failedExamplesTestSet
                'Initialize properties
                .runsFailed = 0
                .comparisonsFailed = 0
                .numExamplesPassed = CStr(0)

                .exampleClassification = GetEnumDescription(eTestSetClassification.FailedExamples)
                .testID = myRegTest.test_id

                'Get number of examples run or compared in the last check
                .numExamplesRun = exampleRunNumLatest
                .numExamplesCompared = exampleCompareNumLatest

                'Get number of examples passed in the last check
                .numExamplesPassed = CStr(NumExamplesPassedCheck())

                'Query failed examples in the test set list for the max % difference, and update this and the number of examples passed/failed 
                currentMaxPercDiff = .SetNumFailedTotal()

                'Set overall status
                .overallResult = .GetOverallResult()

                'Determine overall test success
                If currentMaxPercDiff = 0 And .runsFailed = 0 And .comparisonsFailed = 0 Then
                    .checkPassed = True
                Else
                    .checkPassed = False
                End If
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Removes the first failed test set encountered in the list of example test sets. Returns true if this is done. Returns false if no failed test sets are found.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function RemoveLastFailedTestSet() As Boolean
        Dim testSetIndex As Integer = 0

        For Each myTestSet As cExampleTestSet In examplesTestSetList
            If Not String.IsNullOrEmpty(myTestSet.maxPercentDifference) Then
                Exit For
            Else
                testSetIndex += 1
            End If
        Next
        'If testSetIndex < length of the test set, that "1" offset indicates the last test set is a failed test set. Remove it. If the difference is greater, there is more than one failed test set.
        If testSetIndex < examplesTestSetList.Count Then
            examplesTestSetList.RemoveAt(testSetIndex)
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Creates and selects the failed results tab in CSiTester. Currently only applied pure examples comparison, as the operation is also done via the status form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateFailedTabs()
        Dim tabCounter As Integer = 0

        Try
            'Method for calling another window without a named class instantiation: http://www.vbforums.com/showthread.php?628245-RESOLVED-Call-a-function-in-one-form-from-another
            Dim strWindowToLookFor As String = GetType(CSiTester).Name
            Dim win = ( _
                  From w In Application.Current.Windows _
                  Where DirectCast(w, Window).GetType.Name = strWindowToLookFor _
                  Select w _
               ).FirstOrDefault

            'Generate new tabs with the summary tab of failed examples
            DirectCast(win, CSiTester).InitializeDataGridTabs(True)

            'Save CSiTester
            DirectCast(win, CSiTester).SaveCSiTester()

            'Select summary tab of failed examples if it exists
            'get the tab index that the failed summary tab is
            For Each examplesListItem As cExampleTestSet In examplesTestSetList
                If Not String.IsNullOrEmpty(examplesListItem.maxPercentDifference) Then Exit For
                tabCounter += 1
            Next

            'If a failed tab summary was generated, the following condition is true, and select the tab by index
            If Not tabCounter > examplesTestSetList.Count - 1 Then
                If win IsNot Nothing Then
                    DirectCast(win, CSiTester).myTabControlSummary.SelectedIndex = tabCounter
                End If
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Test Components"
    ''' <summary>
    '''Validates that the appropriate destination directories have been emptied or deleted.
    ''' </summary>
    ''' <param name="p_className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtClearDestinationFolder(ByVal p_className As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(p_className, CLASS_STRING)

        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            If myCSiTesterPub Is Nothing Then
                Return subTestPass
            Else
                If Not myCSiTesterPub.VldtClearDestinationFolder(p_className, testerDestinationDir) Then Return subTestPass
            End If
        Else
            If myCSiTesterInt Is Nothing Then
                Return subTestPass
            Else
                If Not myCSiTesterInt.VldtClearDestinationFolder(p_className) Then Return subTestPass
            End If
        End If

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the appropriate destination directory files have been reset to their default files.
    ''' </summary>
    ''' <param name="p_className">Name assigned to the class where this function resides.</param>
    ''' <param name="p_oldDateModifiedCSiTesterSettingsXML">Date &amp; time at which the CSiTesterSettings.xml file was last modified before the defaults were restored.</param>
    ''' <param name="p_oldDateModifiedRegTestXML">Date &amp; time at which the regTest.xml file was last modified before the defaults were restored.</param>
    ''' <param name="p_oldDateModifiedregTestLog">Date &amp; time at which the regTest log xml file was last modified before the defaults were restored.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtRestoreDefaults(ByVal p_className As String,
                                        ByVal p_oldDateModifiedCSiTesterSettingsXML As String,
                                        ByVal p_oldDateModifiedRegTestXML As String,
                                        Optional ByVal p_oldDateModifiedregTestLog As String = "") As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(p_className, CLASS_STRING)
        Dim modelsOutputPath As String

        modelsOutputPath = testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION

        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            If myCSiTesterPub Is Nothing Then
                Return subTestPass
            Else
                If Not myCSiTesterPub.VldtRestoreDefaults(p_className, p_oldDateModifiedCSiTesterSettingsXML, p_oldDateModifiedRegTestXML, p_oldDateModifiedregTestLog, testerDestinationDir, modelsOutputPath) Then Return subTestPass
            End If
        Else
            If myCSiTesterInt Is Nothing Then
                Return subTestPass
            Else
                If Not myCSiTesterInt.VldtRestoreDefaults(p_className, p_oldDateModifiedCSiTesterSettingsXML, p_oldDateModifiedRegTestXML, testerSourceDir, modelsOutputPath) Then Return subTestPass
            End If
        End If

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the appropriate destination directory has been properly reset.
    ''' </summary>
    ''' <param name="p_className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResetDestinationFolder(ByVal p_className As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(p_className, CLASS_STRING)
        Dim modelsOutputPath As String

        modelsOutputPath = testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION

        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            If myCSiTesterPub Is Nothing Then
                Return subTestPass
            Else
                If Not myCSiTesterPub.VldtResetDestinationFolder(p_className, testerDestinationDir, modelsOutputPath) Then Return subTestPass
            End If
        End If

        subTestPass = True

        Return subTestPass
    End Function
#End Region

#Region "Querying"

    ''' <summary>
    ''' Returns the date modified of the desired supporting file.
    ''' </summary>
    ''' <param name="p_testerLevel">Level of the CSiTester program. This affects where the function searches for the file. 
    ''' Specify true for only ONE of the following parameters. Otherwise, the function will return the date/time modified for the first parameter specified as 'true'.</param>
    ''' <param name="p_csiTesterSettingsXML">If true, the date/time modified will be for this file.</param>
    ''' <param name="p_regTestXML">If true, the date/time modified will be for this file.</param>
    ''' <param name="p_regTestLog">If true, the date/time modified will be for this file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDateModified(ByVal p_testerLevel As eCSiTesterLevel,
                                    ByVal p_csiTesterSettingsXML As Boolean,
                                    ByVal p_regTestXML As Boolean,
                                    ByVal p_regTestLog As Boolean) As String
        Try
            If p_testerLevel = eCSiTesterLevel.Published Then
                If p_csiTesterSettingsXML = True Then
                    Return GetFileDateModified(testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS)
                ElseIf p_regTestXML = True Then
                    Return GetFileDateModified(testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL)
                ElseIf p_regTestLog = True Then
                    Return GetFileDateModified(testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION & "\" & FILENAME_REGTEST_LOG)
                End If
            Else
                If p_csiTesterSettingsXML = True Then
                    Return GetFileDateModified(testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS)
                ElseIf p_regTestXML = True Then
                    Return GetFileDateModified(testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL)
                ElseIf p_regTestLog = True Then
                    Return ""
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return ""
    End Function

#End Region

End Class




