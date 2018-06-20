Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Windows.Automation.Peers             'Required for clicking buttons programmatically
Imports System.Windows.Automation.Provider          'Required for clicking buttons programmatically

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cE2ETestController
Imports CSiTester.cPathSettings

''' <summary>
''' Controls end-to-end testing and records results.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETester
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Variables"
    ''' <summary>
    ''' Temporary instantiation of the cE2ELogger class. Logs the status of a test and its subtests to be added to the overall test summary.
    ''' </summary>
    ''' <remarks></remarks>
    Private e2eLogger As cE2ELogger
#End Region

#Region "Properties"
    ''' <summary>
    ''' Statement of what the test is expected to show.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property expectation As String
    ''' <summary>
    ''' The actual result of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultActual As String
    ''' <summary>
    ''' The string expression of the code that is used to call the actual result of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultActualCall As String
    ''' <summary>
    ''' The expected result of the test. The test passes if this matches the actual result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultExpected As String
    ''' <summary>
    ''' The result the test is not expected to have. Matching this is a failure of the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property resultNotExpected As String

    ''' <summary>
    ''' Collection of cE2ELogger classes to be used for compiling an overall summary of running one or more test functions that each contain one or more sub-tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTests As ObservableCollection(Of cE2ELogger)

    Private _controller As cE2ETestController
    ''' <summary>
    ''' Controller object that determines expected values &amp; predefined values to assert based on set up test suites &amp; destinations.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property controller As cE2ETestController
        Set(ByVal value As cE2ETestController)
            _controller = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("controller"))
        End Set
        Get
            Return _controller
        End Get
    End Property

    ''' <summary>
    ''' The build of a particular program selected for the expected test result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property build As String
    ''' <summary>
    ''' The version of a particular program selected for the expected test result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property version As String

    ''' <summary>
    ''' The ID of the current test being performed &amp; validated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property currentTestID As String
    ''' <summary>
    ''' The total log of all of the subtests run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testLog As String
    ''' <summary>
    ''' The log of all of the failed subtests run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testSummaryLog As String
    ''' <summary>
    ''' Number of tests run. Also the current highest number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testNum As Integer
    ''' <summary>
    ''' Number of tests that have passed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testPassNum As Integer
    ''' <summary>
    ''' Number of tests that have failed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testFailNum As Integer
    ''' <summary>
    ''' Number of subtests run. Also the current highest number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestNum As Integer
    ''' <summary>
    ''' Number of subtests that have passed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestPassNum As Integer
    ''' <summary>
    ''' Number of subtests that have failed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestFailNum As Integer
    ''' <summary>
    ''' Path to the latest run full log file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePathFull As String
    ''' <summary>
    ''' Path to the latest run summary log file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePathSummary As String
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Friend Sub New(ByVal pathXML As String)
        InitializeData(pathXML)
    End Sub

    Private Sub InitializeData(Optional ByVal pathXML As String = "")
        subTests = New ObservableCollection(Of cE2ELogger)

        If Not String.IsNullOrEmpty(pathXML) Then controller = New cE2ETestController(pathXML)
    End Sub

#End Region

#Region "Methods: SubTests"
    ''' <summary>
    ''' Begins logging and testing for the next testing function that contains one or more sub-tests.
    ''' </summary>
    ''' <param name="functionName"></param>
    ''' <remarks></remarks>
    Friend Sub StartSubTests(ByVal functionName As String)
        e2eLogger = New cE2ELogger(functionName)

        'Set defaults for properties
        expectation = "expectationStatement"
        resultActual = "actualResult"
        resultExpected = "expectedResult"
        resultNotExpected = "notExpectedResult"
    End Sub

    ''' <summary>
    ''' Compares the results of the sub-test to determine if it passed or failed. Returns 'False' if the required e2eLogger object is not initialized, else 'True'.
    ''' </summary>
    ''' <param name="expectationStatement">Statement of what the test is expected to show.</param>
    ''' <param name="actualResult">The actual result of the test.</param>
    ''' <param name="expectedResult">The expected result of the test. The test passes if this matches the actual result.</param>
    ''' <param name="notExpectedResult">The result the test is not expected to have. Matching this is a failure of the test.</param>
    ''' <returns>True if the test runs with some success. False if the required e2eLogger object is not initialized.</returns>
    ''' <remarks></remarks>
    Friend Function RunSubTest(Optional ByVal expectationStatement As String = "N/A", Optional ByVal actualResult As String = "N/A", Optional ByVal actualResultCall As String = "", _
                   Optional ByVal expectedResult As String = "N/A", Optional ByVal notExpectedResult As String = "N/A") As Boolean
        Try
            RunSubTest = True
            If e2eLogger Is Nothing Then
                e2eLogger = New cE2ELogger()
                With e2eLogger
                    .subTestLog = "****** e2eLogger object not initialized! Sub-test will be skipped. ******" & Environment.NewLine & Environment.NewLine
                    .subTestNum = 1
                    .subTestFailNum = 1
                    EndSubTests()

                    RunSubTest = False
                    Exit Try
                End With
            ElseIf (expectation = "N/A" OrElse resultActual = "N/A") Then
                With e2eLogger
                    .subTestLog = .subTestLog & "****** Either no expectation or no actual result was provided! Sub-test will be skipped. ******" & Environment.NewLine & Environment.NewLine
                    .subTestNum += 1
                    .subTestFailNum += 1
                End With
                Exit Function
            ElseIf (resultExpected = "N/A" AndAlso resultNotExpected = "N/A") Then
                With e2eLogger
                    .subTestLog = .subTestLog & "****** Neither an expected result or unexpected result was provided! Sub-test will be skipped. ******" & Environment.NewLine & Environment.NewLine
                    .subTestNum += 1
                    .subTestFailNum += 1
                End With
                Exit Function
            End If

            'If values supplied to function, assign them to properties
            If Not expectationStatement = "N/A" Then expectation = expectationStatement
            If Not actualResult = "N/A" Then resultActual = actualResult
            If Not String.IsNullOrEmpty(actualResultCall) Then resultActualCall = actualResultCall
            If Not expectedResult = "N/A" Then resultExpected = expectedResult
            If Not notExpectedResult = "N/A" Then resultNotExpected = notExpectedResult

            'Run subTest
            e2eLogger.Expect(expectation)
            If Not resultNotExpected = "N/A" Then
                If Not resultNotExpected = resultActual Then
                    If Not resultExpected = "N/A" Then
                        If resultActual = resultExpected Then
                            e2eLogger.SubTestPass()
                        Else
                            e2eLogger.SubTestFail(resultActual, resultActualCall, resultExpected)
                        End If
                    Else
                        e2eLogger.SubTestPass()
                    End If
                Else
                    e2eLogger.SubTestFail(resultActual, resultActualCall, , resultNotExpected)
                End If
            ElseIf Not resultExpected = "N/A" Then
                If resultActual = resultExpected Then
                    e2eLogger.SubTestPass()
                Else
                    e2eLogger.SubTestFail(resultActual, resultActualCall, resultExpected)
                End If
            End If
        Catch ex As Exception
            RunSubTest = False
        Finally
            'Reset properties
            expectation = "N/A"
            resultActual = "N/A"
            resultActualCall = ""
            resultExpected = "N/A"
            resultNotExpected = "N/A"
        End Try
    End Function

    ''' <summary>
    ''' Adds the accumulated properties of the logger object to the list of logger objects that will summarize an entire test suite.
    ''' </summary>
    ''' <param name="subTestPass">Returns as 'True' if all sub-tests pass. Else, returns as 'False'.</param>
    ''' <remarks></remarks>
    Friend Sub EndSubTests(Optional ByRef subTestPass As Boolean = False)
        e2eLogger.SubTestsFinish()
        If e2eLogger.subTestFailNum > 0 Then
            subTestPass = False
        Else
            subTestPass = True
        End If

        subTests.Add(CType(e2eLogger.Clone, cE2ELogger))
    End Sub

    ''' <summary>
    ''' Handles completing the subtests and log when an unhandled exception occurs. This includes copying the ex.message and ex.stackTrace messages to the log.
    ''' </summary>
    ''' <param name="exMessage">Exception error message.</param>
    ''' <param name="exStackTrace">Exception stack trace message.</param>
    ''' <remarks></remarks>
    Friend Sub SubTestException(ByVal exMessage As String, ByVal exStackTrace As String)
        If e2eLogger Is Nothing Then
            e2eLogger = New cE2ELogger("SubTestException")
        End If

        e2eLogger.SubTestException(exMessage, exStackTrace)

        subTests.Add(CType(e2eLogger.Clone, cE2ELogger))
    End Sub

    ''' <summary>
    ''' Handles completing the subtests and log when an unhandled exception occurs. This includes copying the custom message to the log.
    ''' </summary>
    ''' <param name="message">Custom message to use for the unhandled exception.</param>
    ''' <remarks></remarks>
    Friend Sub SubTestExceptionCustom(ByVal message As String)
        If e2eLogger Is Nothing Then
            e2eLogger = New cE2ELogger("SubTestExceptionCustom")
        End If
        e2eLogger.SubTestExceptionCustom(message)

        subTests.Add(CType(e2eLogger.Clone, cE2ELogger))
    End Sub


    ''' <summary>
    ''' Prints the contents of cE2eLogger to a text file for review.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub PrintLogger()
        Dim dateString As String = Year(Now) & "-" & Month(Now) & "-" & Day(Now) & "_" & Hour(Now) & "-" & Minute(Now) & "-" & Second(Now)
        filePathFull = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING & "\logs\" & "E2ETest_" & dateString & ".txt"
        filePathSummary = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_E2E_TESTING & "\logs\" & "E2ETest_Summary_" & dateString & ".txt"
        Try
            If e2eLogger Is Nothing Then
                e2eLogger = New cE2ELogger("Empty Run")
                subTests.Add(e2eLogger)
            End If

            'Complete text and properties for test run
            EndTests()

            If IO.Directory.Exists(GetPathDirectoryStub(filePathFull)) Then WriteTextFile(filePathFull, testLog, True)
            If IO.Directory.Exists(GetPathDirectoryStub(filePathSummary)) Then WriteTextFile(filePathSummary, testSummaryLog, True)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            WriteTextFile(pathStartup() & "\" & "E2ETest_WriteFailure.txt", e2eLogger.subTestLog, True)
        Finally
            e2eLogger = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Complete text and properties for test run of all subtests.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndTests()
        Dim testLogStartText As String
        Dim testLogFinishText As String

        testNum = 0
        testFailNum = 0
        testPassNum = 0
        subTestNum = 0
        subTestFailNum = 0
        subTestPassNum = 0

        'Concatenate all logs, total up all runs, passes & failures
        For Each e2eLoggerItem As cE2ELogger In subTests
            testLog = testLog & e2eLoggerItem.subTestLog
            testSummaryLog = testSummaryLog & e2eLoggerItem.subTestSummaryLog

            subTestNum = subTestNum + e2eLoggerItem.subTestNum
            subTestFailNum = subTestFailNum + e2eLoggerItem.subTestFailNum
            subTestPassNum = subTestPassNum + e2eLoggerItem.subTestPassNum

            testNum += 1
            If (e2eLoggerItem.subTestFailNum = 0 AndAlso e2eLoggerItem.subTestPassNum > 0) Then
                testPassNum += 1
            ElseIf (e2eLoggerItem.subTestFailNum > 0) Then
                testFailNum += 1
            End If
        Next

        'Create header summaries
        testLogStartText = "===============================================================" & Environment.NewLine & _
                           "*****                       CSiTester                     *****" & Environment.NewLine & _
                           "*****               End-To-End Testing Results            *****" & Environment.NewLine & _
                           "---------------------------------------------------------------" & Environment.NewLine & _
                           "    Performed on: " & Now & Environment.NewLine & _
                           "===============================================================" & Environment.NewLine
        testLogFinishText = "===============================================================" & Environment.NewLine & _
                            "*****                    OVERALL RESULTS                 ******" & Environment.NewLine & _
                            "===============================================================" & Environment.NewLine & _
                            "   Tests Run:    " & testNum & "     SubTests Run:    " & subTestNum & Environment.NewLine & _
                            "---------------------------------------------------------------" & Environment.NewLine & _
                            "   Tests Passed: " & testPassNum & "     SubTests Passed: " & subTestPassNum & Environment.NewLine & _
                            "   Tests Failed: " & testFailNum & "     SubTests Failed: " & subTestFailNum & Environment.NewLine & _
                            "===============================================================" & Environment.NewLine & Environment.NewLine

        'Create log messages
        testLog = testLogStartText & testLogFinishText & testLog
        testSummaryLog = testLogStartText & testLogFinishText & testSummaryLog
    End Sub
#End Region

#Region "Methods: Misc"
    ''' <summary>
    ''' Creates the prefix of the stack trace call. Handles {cClass}ClassName vs. ClassName (which contains {cClass}).
    ''' </summary>
    ''' <param name="className">The name assigned to the class, or the total prefix call that includes the name, such as referencing a class as an item in a list.</param>
    ''' <param name="classString">The generic name of the class written as a string.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetClassIdentifier(ByVal className As String, ByVal classString As String) As String
        Dim classIdentifier As String

        If StringExistInName(className, classString) Then
            classIdentifier = className & "."
        Else
            classIdentifier = "{" & classString & "}" & className & "."
        End If

        Return classIdentifier
    End Function

    ''' <summary>
    ''' Returns the test object by test ID.
    ''' </summary>
    ''' <param name="testID">Test ID associated with the desired test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetTestByID(ByVal testID As String) As cE2ETestInstructions
        For Each test As cE2ETestInstructions In controller.testInstructions
            If test.id = testID Then
                Return test
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the test object associated with the current test ID property of the cE2eTester class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCurrentTest() As cE2ETestInstructions
        For Each test As cE2ETestInstructions In controller.testInstructions
            If test.id = currentTestID Then
                Return test
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the suite object by test ID. If there is more than one suite, the first suite is chosen unless otherwise specified by index #.
    ''' </summary>
    ''' <param name="testID">Test ID number that corresponds to the test that is being used for the suite to be retrieved.</param>
    ''' <param name="suiteIndex">0-based index of the desired suite in the list of suites for the test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSuiteByTestID(ByVal testID As String, Optional ByVal suiteIndex As Integer = 0) As cE2ETestSuite
        Dim suiteID As String

        For Each test As cE2ETestInstructions In e2eTester.controller.testInstructions
            If test.id = testID Then
                suiteID = test.testSuiteIDs(suiteIndex)
                Return GetSuiteByID(suiteID)
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the suite object of the specified ID number.
    ''' </summary>
    ''' <param name="suiteID">Suite ID number that corresponds to the suite to be retrieved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSuiteByID(ByVal suiteID As String) As cE2ETestSuite
        For Each suite As cE2ETestSuite In e2eTester.controller.suites
            If suite.id = suiteID Then Return suite
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the destination object by test ID. If there is more than one destination, the first suite is chosen unless otherwise specified by index #.
    ''' </summary>
    ''' <param name="testID">Test ID associated with the desired test.</param>
    ''' <param name="destinationIndex">>0-based index of the desired destination in the list of destinations for the test.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDestinationByTestID(ByVal testID As String, Optional ByVal destinationIndex As Integer = 0) As cE2ETestDestination
        Dim destinationID As String

        For Each test As cE2ETestInstructions In e2eTester.controller.testInstructions
            If test.id = testID Then
                destinationID = test.testDestinationIDs(destinationIndex)
                Return GetDestinationByID(destinationID)
            End If
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the destination object of the specified ID number.
    ''' </summary>
    ''' <param name="p_destinationID">Suite ID number that corresponds to the destination to be retrieved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDestinationByID(ByVal p_destinationID As String) As cE2ETestDestination
        For Each destination As cE2ETestDestination In e2eTester.controller.destinations
            If destination.id = p_destinationID Then Return destination
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the build of the test program of the supplied name.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the expected build number.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetBuild(ByVal p_programName As eCSiProgram) As String
        Select Case p_programName
            Case eCSiProgram.CSiBridge : Return "0"
            Case eCSiProgram.SAP2000 : Return "1099"
            Case eCSiProgram.SAFE : Return "1029"
            Case eCSiProgram.ETABS : Return "1134"
            Case Else : Return "None"
        End Select
    End Function

    ''' <summary>
    ''' Returns the version of the test program of the supplied name.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the expected version number.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetVersion(ByVal p_programName As eCSiProgram) As String
        Select Case p_programName
            Case eCSiProgram.CSiBridge : Return "15.2.0"
            Case eCSiProgram.SAP2000 : Return "17.1.1"
            Case eCSiProgram.SAFE : Return "14.0.0"
            Case eCSiProgram.ETABS : Return "13.2.1"
            Case Else : Return "None"
        End Select
    End Function
#End Region

#Region "Activating Form Controls"

    ''' <summary>
    ''' Programmatically 'clicks' the button object provided.
    ''' </summary>
    ''' <param name="btnObject">Button object that is 'clicked'.</param>
    ''' <remarks></remarks>
    Friend Sub ButtonClick(ByVal btnObject As Button)
        Dim peer As New ButtonAutomationPeer(btnObject)
        Dim invokeProv As IInvokeProvider

        invokeProv = CType(peer.GetPattern(PatternInterface.Invoke), IInvokeProvider)

        invokeProv.Invoke()
    End Sub

    ''' <summary>
    ''' Programmatically 'checks' the checkbox object provided.
    ''' </summary>
    ''' <param name="chkBoxObject">Checkbox object that is 'checked'.</param>
    ''' <remarks></remarks>
    Friend Sub CheckBoxClick(ByVal chkBoxObject As CheckBox)
        Dim peer As New CheckBoxAutomationPeer(chkBoxObject)
        Dim invokeProv As IInvokeProvider

        invokeProv = CType(peer.GetPattern(PatternInterface.Invoke), IInvokeProvider)

        invokeProv.Invoke()
    End Sub

    ''' <summary>
    ''' Programatically 'clicks' the datagrid cell object provided. Can be used check checkboxes?
    ''' </summary>
    ''' <param name="dgCellObject">Datagrid cell object to 'click'.</param>
    ''' <remarks></remarks>
    Friend Sub DataGridCellClick(ByVal dgCellObject As DataGridCell)
        Dim peer As New DataGridCellAutomationPeer(dgCellObject)
        Dim invokeProv As IInvokeProvider

        invokeProv = CType(peer.GetPattern(PatternInterface.Invoke), IInvokeProvider)

        invokeProv.Invoke()
    End Sub

    ''' <summary>
    ''' Programmatically 'clicks' the ribbon menu item provided.
    ''' </summary>
    ''' <param name="ramObject"></param>
    ''' <remarks></remarks>
    Friend Sub RibbonMenuItemClick(ByVal ramObject As RibbonApplicationMenuItem)
        Dim peer As New RibbonMenuItemAutomationPeer(ramObject)

        Dim invokeProv As IInvokeProvider

        invokeProv = CType(peer.GetPattern(PatternInterface.Invoke), IInvokeProvider)

        invokeProv.Invoke()
    End Sub

    ''' <summary>
    ''' Programmatically 'clicks' the tab object provided.
    ''' </summary>
    ''' <param name="tabObject">Tab object that is 'clicked'.</param>
    ''' <remarks></remarks>
    Friend Sub TabClick(ByVal tabObject As TabItem, ByVal tabControl As TabControl)
        Dim tabControlPeer As New TabControlAutomationPeer(tabControl)
        Dim peer As New TabItemAutomationPeer(tabObject, tabControlPeer)
        Dim selectProv As ISelectionItemProvider

        selectProv = CType(peer.GetPattern(PatternInterface.SelectionItem), ISelectionItemProvider)

        selectProv.Select()
    End Sub

#End Region

End Class
