Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting

''' <summary>
''' Simply contains a collection of examples belonging to the same category (Classification 2). Used for creating datagrid displays. 
''' If examples are grouped into multiple categories, then multiple instances of this class are created.
''' </summary>
''' <remarks></remarks>
Public Class cExampleTestSet
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

    Friend Const CLASS_STRING As String = "cExampleTestSet"
#Region "Constants: Private"
    Private Const PROMPT_EXAMPLES_FAILED_NONE_CHECKED As String = "-"
    Private Const PROMPT_EXAMPLES_PASSED_NONE_CHECKED As String = "-"
#End Region

#Region "Enumerations"
    ''' <summary>
    ''' Classification group for a standard test set at the Level 2 Classification.
    ''' FailedExamples and MergedExamples temporarily overwrite current group labels for different temporary groupings.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eTestSetClassification
        <Description("")> None = 0
        ''' <summary>
        ''' All examples that failed during the most recent check.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Failed Examples")> FailedExamples
        ''' <summary>
        ''' Examples that have not been classified into pre-defined groups.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Unclassified")> DefaultExamples
        ''' <summary>
        ''' All examples currently loaded during the session, combined into a single group.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("All Examples")> MergedExamples
    End Enum
#End Region

#Region "Variables"
    Private _example As cExample                                            'Creates example class
#End Region

#Region "Properties"
    ''' <summary>
    ''' Name for the classification group corresponding to the Level 2 classification shared by all examples contained within.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleClassification As String
    ''' <summary>
    ''' Collection of examples contained within a classification group. This collapses to one group if tabs are merged in the program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesList As ObservableCollection(Of cExample)

    ''' <summary>
    ''' Name given to a particular test and associated with the run summar result files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testID As String

    ''' <summary>
    ''' Time at which the example check was started.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeStarted As String
    ''' <summary>
    ''' Time at which the example check was completed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeCompleted As String
    ''' <summary>
    ''' Time elapsed during the example check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property timeElapsed As String

    ''' <summary>
    ''' Number of examples checked in the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numExamplesChecked As Integer
    ''' <summary>
    ''' Number of examples run during the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numExamplesRun As Integer
    ''' <summary>
    ''' Number of examples compared during the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numExamplesCompared As Integer
    ''' <summary>
    ''' Number of checked examples that passed in the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numExamplesPassed As String                     'Made as string as sometimes a label of none compared is used
    ''' <summary>
    ''' Number of examples that failed in the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property numExamplesFailed As String                     'Made as string as sometimes a label of none compared is used
    ''' <summary>
    ''' Largest % difference between a rounded result and its corresponding benchmark among all results in all examples of the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property maxPercentDifference As String
    ''' <summary>
    ''' Number of examples that failed to run successfully in the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property runsFailed As Integer
    ''' <summary>
    ''' Number of examples that failed to compare successfully in the last check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property comparisonsFailed As Integer
    ''' <summary>
    ''' Overall result of the test. Combination of {[max%Difference], [Runs Failed] and [Comparisons Failed]}.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property overallResult As String
    ''' <summary>
    ''' Final yes/no declaration of whether or not the last check passed with no failures of the run, comparison, or % difference from benchmark of any example included.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkPassed As Boolean = False

#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeExamples()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Initializes an examplesList collection of example objects.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeExamples()
        examplesList = New ObservableCollection(Of cExample)    'Adds collection of examples to test set
    End Sub

    ''' <summary>
    ''' Determines the max % difference in each example in the test set. Returns this number. Returns -1000000 if there is an error.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMaxPercDifference() As Double
        Dim currentMaxPercDiff As Double = 0

        Try
            For Each myExample As cExample In examplesList
                Try
                    With myExample
                        'Check only examples that have been run
                        If Not .percentDifferenceMax = GetEnumDescription(eResultOverall.notChecked) Then
                            If StringExistInName(.percentDifferenceMax, GetEnumDescription(eResultOverall.percDiffNotAvailable)) Then                   'No numerical results
                                If (.runStatus = GetEnumDescription(eResultRun.completedRun) OrElse .runStatus = GetEnumDescription(eResultRun.manual)) Then    'Run has been completed
                                    .percentDifferenceMax = GetEnumDescription(eResultOverall.percDiffNotAvailable)                                     'Assign string result to example, but do not count in max %
                                End If
                            ElseIf Math.Abs(CDbl(GetPrefix(.percentDifferenceMax, "%"))) > Math.Abs(currentMaxPercDiff) Then
                                currentMaxPercDiff = CDbl(GetPrefix(.percentDifferenceMax, "%"))
                            End If
                        End If
                    End With
                Catch ex As Exception
                    currentMaxPercDiff = -1000000
                End Try
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return currentMaxPercDiff
    End Function

    ''' <summary>
    ''' Gets the overall result of compared examples in a given test set. If hypothetical values are supplied, these are used instead of the properties of the cExampleTestSet.
    ''' </summary>
    ''' <param name="expctdRunsFailed">If specified, the number of example runs expected to fail.</param>
    ''' <param name="expctdComparisonsFailed">If specified, the number of example comparisons expected to fail.</param>
    ''' <param name="expctdMaxPercentDifference">If specified, the expected maximum % difference of the examples considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetOverallResult(Optional ByVal expctdRunsFailed As Integer = -1, Optional ByVal expctdComparisonsFailed As Integer = -1, Optional ByVal expctdMaxPercentDifference As String = "expctdMaxPercentDifference") As String
        Dim tempOverallResult As String = ""
        Dim tempRunsFailed As Integer
        Dim tempComparisonsFailed As Integer
        Dim tempMaxPercentDifference As String

        If expctdRunsFailed = -1 Then
            tempRunsFailed = runsFailed
        Else
            tempRunsFailed = expctdRunsFailed
        End If
        If expctdComparisonsFailed = -1 Then
            tempComparisonsFailed = comparisonsFailed
        Else
            tempComparisonsFailed = expctdComparisonsFailed
        End If
        If expctdMaxPercentDifference = "expctdMaxPercentDifference" Then
            tempMaxPercentDifference = maxPercentDifference
        Else
            tempMaxPercentDifference = expctdMaxPercentDifference
        End If

        If tempRunsFailed > 0 Then tempOverallResult = PluralizeString(tempRunsFailed, "model") & " failed to run." & Environment.NewLine
        If tempComparisonsFailed > 0 Then tempOverallResult = tempOverallResult & PluralizeString(tempComparisonsFailed, "model") & " failed to be compared. " & Environment.NewLine
        tempOverallResult = tempMaxPercentDifference & Environment.NewLine & tempOverallResult

        Return tempOverallResult
    End Function

    ''' <summary>
    ''' Query the overall result for # of runs &amp; comparisons failed to account for non-numerical failures. Add these to the number of failures.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumFailedNotNumerical()
        For Each myExample As cExample In examplesList
            With myExample
                If .overallResult = .runStatus Then runsFailed += 1
                If .overallResult = .compareStatus Then comparisonsFailed += 1
            End With
        Next
    End Sub

    ''' <summary>
    ''' Sets the number of examples in the set that have passed and failed. Returns the maximum percent difference from this check.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetNumFailedTotal() As Double
        Dim currentMaxPercDiff As Double = 0

        'Determine the number of examples pass/failed, and establish max % difference if possible
        If examplesList.Count > 0 Then                      'Test set contains examples. If a failed test set, these are all of the failed examples.
            numExamplesFailed = CStr(examplesList.Count)

            'Query the overall max % difference
            currentMaxPercDiff = SetMaxPercDifference()

            'Query the overall result for # of runs & comparisons failed to account for non-numerical failures
            If currentMaxPercDiff = 0 Then SetNumFailedNotNumerical()
        Else                                                'Test set is empty. If a failed test set, that means no examples failed.
            If numExamplesCompared = 0 Then
                numExamplesPassed = PROMPT_EXAMPLES_PASSED_NONE_CHECKED
                numExamplesFailed = PROMPT_EXAMPLES_FAILED_NONE_CHECKED
            Else
                numExamplesPassed = CStr(CDbl(numExamplesCompared))
                numExamplesFailed = CStr(0)
            End If
        End If

        'Set the max percent difference for the example set
        If numExamplesCompared > 0 Then
            maxPercentDifference = CStr(currentMaxPercDiff) & "%"
        Else
            maxPercentDifference = GetEnumDescription(eResultOverall.notChecked)
        End If

        Return currentMaxPercDiff
    End Function

#End Region

#Region "Test Components"

    ''' <summary>
    ''' Validates that all relevant items are reset in the examples contained in the test set.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsList">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="resultIDsList">List of the results by index used to check specific results within examples. If not supplied, all results are checked. (Note: Incomplete. Need to have unique result lists paired with modelIDs).</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResultReset(ByVal className As String, Optional ByVal modelIDsList As ObservableCollection(Of String) = Nothing, Optional ByVal resultIDsList As List(Of Integer) = Nothing) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If modelIDsList Is Nothing Then        'Check all examples
                i = 0
                For Each example As cExample In examplesList
                    If Not example.VldtResetRun(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", True, True) Then Return subTestPass
                    If Not example.VldtResetCompare(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", resultIDsList, True, True) Then Return subTestPass
                    i += 1
                Next
            Else                                    'Check examples list provided
                For Each modelID As String In modelIDsList
                    i = 0
                    For Each example As cExample In examplesList
                        If example.modelID = modelID Then
                            If Not example.VldtResetRun(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", True, True) Then Return subTestPass
                            If Not example.VldtResetCompare(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", resultIDsList, True, True) Then Return subTestPass
                        End If
                        i += 1
                    Next
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that all relevant examples in the test set are set as expected for run and/or compare statuses.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsList">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="selectRun">If true, all examples checked are assumed to be marked to be run.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExamplesSelectedRun(ByVal className As String, Optional ByVal modelIDsList As ObservableCollection(Of String) = Nothing, Optional ByVal selectRun As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        Try
            With e2eTester
                If (modelIDsList Is Nothing OrElse modelIDsList.Count = 0) Then         'Check all examples
                    i = 0
                    For Each example As cExample In examplesList
                        If Not example.VldtExampleSelectedRun(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", selectRun) Then Return subTestPass
                        i += 1
                    Next
                Else                                    'Check examples list provided
                    For Each modelID As String In modelIDsList
                        i = 0
                        For Each example As cExample In examplesList
                            If example.modelID = modelID Then
                                If Not example.VldtExampleSelectedRun(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", selectRun) Then Return subTestPass
                            End If
                            i += 1
                        Next
                    Next
                End If
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that all relevant examples in the test set are set as expected for run and/or compare statuses.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsList">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="selectCompare">If true, all examples checked are assumed to be marked to be compared.</param>    
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExamplesSelectedCompare(ByVal className As String, Optional ByVal modelIDsList As ObservableCollection(Of String) = Nothing, Optional ByVal selectCompare As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If (modelIDsList Is Nothing OrElse modelIDsList.Count = 0) Then         'Check all examples
                i = 0
                For Each example As cExample In examplesList
                    If Not example.VldtExampleSelectedCompare(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", selectCompare) Then Return subTestPass
                    i += 1
                Next
            Else                                    'Check examples list provided
                For Each modelID As String In modelIDsList
                    i = 0
                    For Each example As cExample In examplesList
                        If example.modelID = modelID Then
                            If Not example.VldtExampleSelectedCompare(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", selectCompare) Then Return subTestPass
                        End If
                        i += 1
                    Next
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    'Not used yet
    ''' <summary>
    ''' Validates that all relevant examples in the test that have been run are marked as such.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsList">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="ran">If true, all examples checked are assumed to have been run.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExamplesSelectedRan(ByVal className As String, Optional ByVal modelIDsList As ObservableCollection(Of String) = Nothing, Optional ByVal ran As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If (modelIDsList Is Nothing OrElse modelIDsList.Count = 0) Then         'Check all examples
                i = 0
                For Each example As cExample In examplesList
                    If Not example.VldtExampleRan(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", ran) Then Return subTestPass
                    i += 1
                Next
            Else                                    'Check examples list provided
                For Each modelID As String In modelIDsList
                    i = 0
                    For Each example As cExample In examplesList
                        If example.modelID = modelID Then
                            If Not example.VldtExampleRan(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", ran) Then Return subTestPass
                        End If
                        i += 1
                    Next
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    'Not used yet
    ''' <summary>
    ''' Validates that all relevant examples in the test that have been compared are marked as such.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsList">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="compared">If true, all examples checked are assumed to have been compared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExamplesSelectedCompared(ByVal className As String, Optional ByVal modelIDsList As ObservableCollection(Of String) = Nothing, Optional ByVal compared As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If (modelIDsList Is Nothing OrElse modelIDsList.Count = 0) Then         'Check all examples
                i = 0
                For Each example As cExample In examplesList
                    If Not example.VldtExampleCompared(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", compared) Then Return subTestPass
                    i += 1
                Next
            Else                                    'Check examples list provided
                For Each modelID As String In modelIDsList
                    i = 0
                    For Each example As cExample In examplesList
                        If example.modelID = modelID Then
                            If Not example.VldtExampleCompared(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", compared) Then Return subTestPass
                        End If
                        i += 1
                    Next
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that all examples listed in the failed test set are unique (i.e. not listed more than once).
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtFailedTestSetUniqueList(ByVal className As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer
        Dim examplesListUnique As New List(Of String)
        Dim uniqueExample As Boolean = True

        With e2eTester
            i = 0
            For Each example As cExample In examplesList
                If Not ExistsInListString(example.modelID, examplesListUnique) Then
                    examplesListUnique.Add(example.modelID)
                    uniqueExample = True
                Else
                    uniqueExample = False
                End If

                If Not example.VldtFailedExampleListedOnce(classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]", uniqueExample) Then Return subTestPass
                i += 1
            Next

        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the test set summary is as expected after a check.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="expctdNumRan">Expected number of examples to have been run.</param>
    ''' <param name="expctdNumCompared">Expected number of examples to have been compared.</param>
    ''' <param name="expctdNumPassed">Expected number of examples to have passed.</param>
    ''' <param name="expctdNumFailed">Expected number of examples to have failed.</param>
    ''' <param name="expctdMaxPercDiff">Expected max % difference from examples.</param>
    ''' <param name="expctdOverallResult">Expected overall result from examples.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtFailedTestSetSummary(ByVal className As String, Optional ByVal expctdNumRan As Integer = -1, Optional ByVal expctdNumCompared As Integer = -1, Optional ByVal expctdNumPassed As Integer = -1, _
                                             Optional ByVal expctdNumFailed As Integer = -1, Optional ByVal expctdMaxPercDiff As String = "expctdMaxPercDiff", Optional ByVal expctdOverallResult As String = "expctdOverallResult") As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            'Validate recorded properties are as expected
            .expectation = "Number ran matches the property recorded"
            .resultActual = CStr(CountNumRan())
            .resultActualCall = classIdentifier & "CountNumRan()"
            .resultExpected = CStr(numExamplesRun)
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Number compared matches the property recorded"
            .resultActual = CStr(CountNumCompared())
            .resultActualCall = classIdentifier & "CountNumCompared()"
            .resultExpected = CStr(numExamplesCompared)
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Number passed matches the property recorded"
            .resultActual = CStr(CountNumPassed())
            .resultActualCall = classIdentifier & "CountNumPassed()"
            .resultExpected = numExamplesPassed
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Number failed matches the property recorded"
            .resultActual = CStr(CountNumFailed())
            .resultActualCall = classIdentifier & "CountNumFailed()"
            .resultExpected = numExamplesFailed
            If Not .RunSubTest() Then Return subTestPass

            'Validate results match those expected
            If Not expctdNumRan = -1 Then
                .expectation = "Number ran is as expected"
                .resultActual = CStr(numExamplesRun)
                .resultActualCall = classIdentifier & "numExamplesRun"
                .resultExpected = CStr(expctdNumRan)
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not expctdNumCompared = -1 Then
                .expectation = "Number compared is as expected"
                .resultActual = CStr(numExamplesCompared)
                .resultActualCall = classIdentifier & "numExamplesCompared"
                .resultExpected = CStr(expctdNumCompared)
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not expctdNumPassed = -1 Then
                .expectation = "Number passed is as expected"
                .resultActual = CStr(numExamplesPassed)
                .resultActualCall = classIdentifier & "numExamplesPassed"
                .resultExpected = CStr(expctdNumPassed)
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not expctdNumFailed = -1 Then
                .expectation = "Number failed is as expected"
                .resultActual = CStr(numExamplesFailed)
                .resultActualCall = classIdentifier & "numExamplesFailed"
                .resultExpected = CStr(expctdNumFailed)
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not expctdMaxPercDiff = "expctdMaxPercDiff" Then
                .expectation = "Max % difference is as expected"
                .resultActual = maxPercentDifference
                .resultActualCall = classIdentifier & "maxPercentDifference"
                .resultExpected = expctdMaxPercDiff
                If Not .RunSubTest() Then Return subTestPass
            End If
            If Not expctdOverallResult = "expctdOverallResult" Then
                .expectation = "Overall result is as expected"
                .resultActual = overallResult
                .resultActualCall = classIdentifier & "overallResult"
                .resultExpected = expctdOverallResult
                If Not .RunSubTest() Then Return subTestPass
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates the relevant passed/failed items in all examples listed in the test set.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="modelIDsListCheck">List of model ids used to check specific examples. If not supplied, all examples are checked.</param>
    ''' <param name="modelIDsListRan">List of examples expected to have been run. If not supplied, the example is assumed to have not been run.</param>
    ''' <param name="modelIDsListCompared">List of examples expected to have been compared. If not supplied, the example is assumed to have not been compared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtExamplesCheck(ByVal className As String, _
                                               Optional ByVal modelIDsListCheck As ObservableCollection(Of String) = Nothing, _
                                               Optional ByVal modelIDsListRan As ObservableCollection(Of String) = Nothing, _
                                               Optional ByVal modelIDsListCompared As ObservableCollection(Of String) = Nothing) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim i As Integer

        With e2eTester
            If (modelIDsListCheck Is Nothing OrElse modelIDsListCheck.Count = 0) Then         'Check all examples
                i = 0
                For Each example As cExample In examplesList
                    If Not VldtExamplesCompletedCheckPrivate(className, i, example, modelIDsListRan, modelIDsListCompared) Then Return subTestPass
                    i += 1
                Next
            Else                                    'Check examples list provided
                For Each modelID As String In modelIDsListCheck
                    i = 0
                    For Each example As cExample In examplesList
                        If example.modelID = modelID Then
                            If Not VldtExamplesCompletedCheckPrivate(className, i, example, modelIDsListRan, modelIDsListCompared) Then Return subTestPass
                        End If
                        i += 1
                    Next
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Determines whether the provided example object has been run and/or compared and validates the object as such.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="i">Index of the example object in the list of examples.</param>
    ''' <param name="example">Example object to be validated.</param>
    ''' <param name="modelIDsListRan">List of examples expected to have been ran.</param>
    ''' <param name="modelIDsListCompared">List of examples expected to have been compared.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VldtExamplesCompletedCheckPrivate(ByVal className As String, ByVal i As Integer, ByVal example As cExample, _
                                               Optional ByVal modelIDsListRan As ObservableCollection(Of String) = Nothing, _
                                               Optional ByVal modelIDsListCompared As ObservableCollection(Of String) = Nothing) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim currentClassName As String = classIdentifier & "examplesList[{" & cExample.CLASS_STRING & "} " & i & "{" & example.modelID & "}]"
        Dim ran As Boolean
        Dim compared As Boolean
        Dim runReset As Boolean

        ran = False
        compared = False
        runReset = False

        'Determine if example is expected to have been ran or compared
        If modelIDsListRan IsNot Nothing Then
            For Each modelIDRan As String In modelIDsListRan
                If modelIDRan = example.modelID Then
                    ran = True
                    Exit For
                End If
            Next
        End If
        If modelIDsListCompared IsNot Nothing Then
            For Each modelIDCompared As String In modelIDsListCompared
                If modelIDCompared = example.modelID Then
                    compared = True
                    Exit For
                End If
            Next
        End If

        'Determine if example had been set to be run or compared but not expected to have been ran or compared (e.g. from cancelling a check)
        If Not ran Then
            For Each modelIDRun As String In testerSettings.examplesRunSaved
                If modelIDRun = example.modelID Then
                    runReset = True
                End If
            Next
        End If

        If Not example.VldtExampleRan(currentClassName, ran, runReset) Then Return subTestPass
        If Not example.VldtExampleCompared(currentClassName, compared) Then Return subTestPass

        subTestPass = True

        Return subTestPass
    End Function

#End Region

#Region "Methods: Self-Query"
    ''' <summary>
    ''' Counts the number of examples that have been run in the test set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountNumRan() As Integer
        Dim count As Integer

        For Each example As cExample In examplesList
            If example.ranExample Then count += 1
        Next

        Return count
    End Function

    ''' <summary>
    ''' Counts the number of examples that have been compared in the test set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountNumCompared() As Integer
        Dim count As Integer

        For Each example As cExample In examplesList
            If example.comparedExample Then count += 1
        Next

        Return count
    End Function

    ''' <summary>
    ''' Counts the number of examples that have passed a check in the test set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountNumPassed() As Integer
        Dim count As Integer

        For Each example As cExample In examplesList
            If example.percentDifferenceMax = "0%" Then count += 1
        Next

        Return count
    End Function

    ''' <summary>
    ''' Counts the numberof examples that have failed a check in the test set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CountNumFailed() As Integer
        Dim count As Integer

        For Each example As cExample In examplesList
            If (Not example.percentDifferenceMax = GetEnumDescription(eResultOverall.notChecked) AndAlso Not example.percentDifferenceMax = "0%") Then count += 1
        Next

        Return count
    End Function

#End Region

End Class
