Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting

''' <summary>
''' Class containing properties and methods used in running end-to-end tests.
''' </summary>
''' <remarks></remarks>
Public Class cE2ETestInstructions
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Properties"
    ''' <summary>
    ''' ID for identifying the test item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As String
    ''' <summary>
    ''' Title of the test item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
    ''' <summary>
    ''' Description of the test item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String

    ''' <summary>
    ''' Time (milliseconds) at which a check is canceled once initialization is complete. If blank, this value will not be used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cancelCheckTime As String
    ''' <summary>
    ''' Model id that will trigger a cancellation of check. If blank, this value will not be used. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cancelCheckModelID As String

    ''' <summary>
    ''' List of examples set to run for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property run As ObservableCollection(Of String)
    ''' <summary>
    ''' List of examples set to be compared for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compare As ObservableCollection(Of String)
    ''' <summary>
    ''' List of examples set to be deslected from running for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property runDeselect As ObservableCollection(Of String)
    ''' <summary>
    ''' List of examples set to be deslected from comparing for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compareDeselect As ObservableCollection(Of String)

    ''' <summary>
    ''' List of examples expected to have been ran for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ran As ObservableCollection(Of String)
    ''' <summary>
    ''' List of examples expected to have been compared for the check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compared As ObservableCollection(Of String)
    ''' <summary>
    ''' List of examples expected to fail from the check. These should appear in a failed test set that is typically shown as a failed test tab.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property failed As ObservableCollection(Of String)
    ''' <summary>
    ''' Expected maximum % difference expected among the examples that the test will compare at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property maxPercDiff As String
    ''' <summary>
    ''' Expected overall result among the examples that the test will compare at the destination.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property overallResult As String

    ''' <summary>
    ''' List of the names of the programs for the test. The current program is changed to the specified entry in this list, or the first by default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programNames As ObservableCollection(Of String)
    ''' <summary>
    ''' List of test suites used for the test. The current models source is changed to the specified entry in this list, or the first by default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testSuiteIDs As ObservableCollection(Of String)
    ''' <summary>
    ''' List of test destinations used for the test. The current destination is changed to the specified entry in this list, or the first by default.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testDestinationIDs As ObservableCollection(Of String)
    ''' <summary>
    ''' ID of the destination actually used in a check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinationIDChecked As String
#End Region

#Region "Initialization"

    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        testSuiteIDs = New ObservableCollection(Of String)
        testDestinationIDs = New ObservableCollection(Of String)
        programNames = New ObservableCollection(Of String)

        run = New ObservableCollection(Of String)
        compare = New ObservableCollection(Of String)
        runDeselect = New ObservableCollection(Of String)
        compareDeselect = New ObservableCollection(Of String)

        ran = New ObservableCollection(Of String)
        compared = New ObservableCollection(Of String)
        failed = New ObservableCollection(Of String)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns the program name of the specified index in the list of program names. 
    ''' If the list is too short, unpopulated, or empty, the currently selected program name is returned.
    ''' </summary>
    ''' <param name="programIndex">Index in the list of programs to retrieve the program name from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetProgramName(Optional ByVal programIndex As Integer = 0) As String
        Dim programName As String = ""

        If (programNames.Count > 0 AndAlso programNames.Count >= programIndex) Then
            programName = programNames(programIndex)
        End If

        If String.IsNullOrEmpty(programName) Then programName = GetEnumDescription(testerSettings.programName)

        Return programName
    End Function

    ''' <summary>
    ''' Sets these properties assuming that all examples in the destination set to run &amp; compare are run &amp; compared.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetRanCompared()
        Dim tempRan As New List(Of String)

        'Assume that each example set to run or compare will be run or compared.
        For Each modelID As String In run
            tempRan.Add(modelID)
        Next

        For Each compareModelID As String In compare
            Dim ranAndCompared As Boolean = False

            compared.Add(compareModelID)

            'Consider cases of examples that are only being compared. This must have been ran, even if not currently set to run
            UpdateRanByCompared(tempRan, compareModelID)
        Next

        'Sort ran lists
        tempRan.Sort()
        ran = ConvertListToObservableCollection(tempRan)
    End Sub

    ''' <summary>
    ''' Adjusts these properties for if the check is canceled.
    ''' </summary>
    ''' <param name="currentModelID">If specified when a cancel is to occur based on time, the ran/compared lists will be updated based on the supplied model ID.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateRanComparedFromCancel(Optional ByVal currentModelID As String = "")
        If Not String.IsNullOrEmpty(cancelCheckModelID) Then
            UpdateRanComparedFromModelID(cancelCheckModelID)
        ElseIf Not String.IsNullOrEmpty(cancelCheckTime) Then
            'Determine model ID at time run was canceled
            If Not String.IsNullOrEmpty(currentModelID) Then UpdateRanComparedFromModelID(currentModelID)
        End If
    End Sub

    ''' <summary>
    ''' Updates the ran &amp; compared lists to include examples that have been run &amp; compared, but are not currently marked to do so. 
    ''' </summary>
    ''' <param name="destinationExamples">List of example objects to check for whether or not an example has been run or compared.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateRanComparedFromExamples(ByVal destinationExamples As ObservableCollection(Of cE2EExample))

        If destinationExamples IsNot Nothing Then
            'Account for if some examples at the destination have already been run or compared
            For Each example As cE2EExample In destinationExamples
                If example.expectedRan Then
                    Dim ranPresent As Boolean = False

                    For Each modelID As String In ran
                        If modelID = example.id Then
                            ranPresent = True
                            Exit For
                        End If
                    Next
                    If Not ranPresent Then ran.Add(example.id)
                End If
                If example.expectedCompared Then
                    Dim comparedPresent As Boolean = False

                    For Each modelID As String In compared
                        If modelID = example.id Then
                            comparedPresent = True
                            Exit For
                        End If
                    Next
                    If Not comparedPresent Then compared.Add(example.id)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Updates the list of failed examples to include only examples expected to fail that have actually been compared.
    ''' </summary>
    ''' <param name="destinationExamples">List of example objects to check for the failure status.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateFailedFromExamples(ByVal destinationExamples As ObservableCollection(Of cE2EExample))
        If destinationExamples IsNot Nothing Then
            failed.Clear()

            For Each modelID As String In compared
                For Each example As cE2EExample In destinationExamples
                    If (example.id = modelID AndAlso example.failed) Then
                        failed.Add(example.id)
                    End If
                Next
            Next
        End If
    End Sub


    ''' <summary>
    ''' In cases of examples that are only being compared, they must have been ran, even if not currently set to run. This corrects the 'ran' list for this.
    ''' </summary>
    ''' <param name="tempRan">List of examples that have been ran.</param>
    ''' <param name="compareModelID">ID of model to be compared, checked against the models that have ran.</param>
    ''' <remarks></remarks>
    Private Sub UpdateRanByCompared(ByRef tempRan As List(Of String), ByVal compareModelID As String)
        Dim ranAndCompared As Boolean = False
        Dim i As Integer

        'Consider cases of examples that are only being compared. This must have been ran, even if not currently set to run
        For Each ranModelID As String In tempRan
            If ranModelID = compareModelID Then
                ranAndCompared = True
                Exit For
            End If
            i += 1
        Next
        If Not ranAndCompared Then
            tempRan.Add(run(i))
        End If
    End Sub

    ''' <summary>
    ''' Updates ran &amp; compared lists based on a check being cancelled.
    ''' </summary>
    ''' <param name="actualCancelCheckModelID">Model ID of the example reached when the check was canceled. This is the first example not included.</param>
    ''' <remarks></remarks>
    Private Sub UpdateRanComparedFromModelID(ByVal actualCancelCheckModelID As String)
        Dim compareOnly As New ObservableCollection(Of String)
        Dim tempRan As New List(Of String)
        Dim tempCompared As New List(Of String)

        Dim cancelIDExistInCompareOnly As Boolean = False

        'Create a list of examples that are only to be compared
        For Each compareModelID As String In compare
            Dim compareAndRun As Boolean = False

            For Each runModelID As String In run
                If runModelID = compareModelID Then
                    compareAndRun = True
                    Exit For
                End If
            Next
            If Not compareAndRun Then compareOnly.Add(compareModelID)
        Next

        'Catch if model is compared and not run
        For Each modelID As String In compareOnly
            If modelID = actualCancelCheckModelID Then
                cancelIDExistInCompareOnly = True
                Exit For
            End If
        Next
        If cancelIDExistInCompareOnly Then                  'No examples are run except those that are only compared
            ran.Clear()
            For Each compareModelID As String In compareOnly
                If compareModelID = actualCancelCheckModelID Then   'Cancelling example reached
                    Exit For
                Else
                    tempCompared.Add(compareModelID)
                    ran.Add(compareModelID)
                End If
            Next
            compared = ConvertListToObservableCollection(tempCompared)
        Else                                                'Get portion of ran list that has actually been ran
            For Each runModelID As String In run
                If runModelID = actualCancelCheckModelID Then       'Cancelling example reached
                    Exit For
                Else
                    tempRan.Add(runModelID)
                    For Each compareModelID In compare      'Add corresponding compared entry if the ran example had been set to compare
                        If compareModelID = runModelID Then
                            tempCompared.Add(runModelID)
                            Exit For
                        End If
                    Next
                End If
            Next

            For Each compareOnlyModelID As String In compareOnly      'Add any example that has only been compared, which must also have then been run
                tempRan.Add(compareOnlyModelID)
                tempCompared.Add(compareOnlyModelID)
            Next

            'Sort lists
            tempRan.Sort()
            ran = ConvertListToObservableCollection(tempRan)

            tempCompared.Sort()
            compared = ConvertListToObservableCollection(tempCompared)
        End If
    End Sub

    'Failure summaries
    ''' <summary>
    ''' Returns the number of examples in the test that are expected to have failed in their runs.
    ''' </summary>
    ''' <param name="p_destinationExamples">List of example objects to check for whether or not an example has been run or compared.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedRunsFailed(ByVal p_destinationExamples As ObservableCollection(Of cE2EExample)) As Integer
        Dim runsFailed As Integer

        For Each modelID As String In failed
            If p_destinationExamples IsNot Nothing Then
                For Each example As cE2EExample In p_destinationExamples
                    With example
                        If (.id = modelID AndAlso
                                (StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.timeOut)) OrElse
                                StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.outputFileMissing)))) Then

                            runsFailed += 1
                        End If
                    End With
                Next
            End If
        Next

        Return runsFailed
    End Function

    ''' <summary>
    ''' Returns the number of examples in the test that are expected to have failed in their comparisons.
    ''' </summary>
    ''' <param name="destinationExamples">List of example objects to check for whether or not an example has been run or compared.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetExpectedComparisonsFailed(ByVal destinationExamples As ObservableCollection(Of cE2EExample)) As Integer
        Dim failedExample As Boolean
        Dim comparisonsFailed As Integer

        If destinationExamples IsNot Nothing Then
            For Each modelID As String In failed
                For Each example As cE2EExample In destinationExamples
                    With example
                        If .id = modelID Then
                            'Consider example passing by default if no expected status was recoreded
                            If String.IsNullOrEmpty(.expectedCompareStatus) Then Return comparisonsFailed

                            'Determine failure from recorded status
                            If (Not StringsMatch(.expectedCompareStatus, GetEnumDescription(eResultCompare.outputFileMissing))) Then                               'Output file was generated
                                If (Not StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.completedRun)) AndAlso
                                    Not StringsMatch(.expectedRunStatus, GetEnumDescription(eResultRun.manual))) Then   'Example failed to run properly
                                    failedExample = True
                                ElseIf Not StringsMatch(.expectedCompareStatus, GetEnumDescription(eResultCompare.successCompared)) Then         'Example failed to check properly
                                    failedExample = True
                                ElseIf Not IsNumeric(GetPrefix(.expectedPercentChange, "%")) Then                                                'Example had some other failure
                                    failedExample = True
                                ElseIf Not CDbl(GetPrefix(.expectedPercentChange, "%")) = 0 Then                                                 '% difference exists
                                    failedExample = True
                                End If
                            Else                                                                                                                'Example output files missing
                                failedExample = True
                            End If

                            If failedExample Then comparisonsFailed += 1
                        End If
                    End With
                Next
            Next
        End If

        Return comparisonsFailed
    End Function

    ''' <summary>
    ''' Sets the max % difference of the expected overall result of the examples that have run in the test. Returns this number. Returns -1000000 if there is an error.
    ''' </summary>
    ''' <param name="setProperty">If ture, sets the property of the class to the value returned. Else, the function returns the value with no other action taken.</param>
    ''' <param name="destinationExamples">List of example objects to check for whether or not an example has been run or compared.</param>
    ''' <remarks></remarks>
    Private Function GetMaxPercDiff(ByVal setProperty As Boolean, ByVal destinationExamples As ObservableCollection(Of cE2EExample)) As String
        Dim currentMaxPercDiff As Double = 0
        Dim maxPercDiffString As String = ""

        Try
            If destinationExamples IsNot Nothing Then
                For Each modelID As String In failed
                    For Each myExample As cE2EExample In destinationExamples
                        Try
                            With myExample
                                If .id = modelID Then
                                    If StringExistInName(.expectedPercentChange, GetEnumDescription(eResultOverall.percDiffNotAvailable)) Then                   'No numerical results
                                        maxPercDiffString = GetEnumDescription(eResultOverall.percDiffNotAvailable)
                                    ElseIf Math.Abs(CDbl(GetPrefix(.expectedPercentChange, "%"))) > Math.Abs(currentMaxPercDiff) Then
                                        currentMaxPercDiff = CDbl(GetPrefix(.expectedPercentChange, "%"))
                                    End If
                                End If
                            End With
                        Catch ex As Exception
                            currentMaxPercDiff = -1000000
                        End Try
                    Next
                Next

                maxPercDiffString = CStr(currentMaxPercDiff) & "%"

                If setProperty Then maxPercDiff = maxPercDiffString
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return maxPercDiffString
    End Function

    ''' <summary>
    ''' Sets the property of the expected overall result &amp; max % difference of examples that have run in the test.
    ''' </summary>
    ''' <param name="destinationExamples">List of example objects to check for whether or not an example has been run or compared.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateOverallResults(ByVal destinationExamples As ObservableCollection(Of cE2EExample))
        Dim tempTestSet As New cExampleTestSet

        If destinationExamples IsNot Nothing Then
            overallResult = tempTestSet.GetOverallResult(GetExpectedRunsFailed(destinationExamples), GetExpectedComparisonsFailed(destinationExamples), GetMaxPercDiff(True, destinationExamples))
        End If
    End Sub

#End Region



End Class
