Option Explicit On
Option Strict On

Imports MPT.Reporting

Imports CSiTester.cMCModel

Friend Class cMCResultIDs
#Region "Constants"
    Friend Const MAX_BENCHMARK_ID As Integer = 99
#End Region

#Region "Fields"
    ''' <summary>
    ''' List of results provided with a public function, to be used across methods in the object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _results As New List(Of cMCResultBasic)
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Updates all of the IDs in all of the results in the provided list.
    ''' </summary>
    ''' <param name="p_results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdateIDs(ByVal p_results As List(Of cMCResultBasic)) As List(Of cMCResultBasic)
        If (p_results Is Nothing OrElse p_results.Count = 0) Then Return p_results

        _results = p_results

        UpdateQueryIDs()
        UpdateBenchmarkIDs()
        Return _results
    End Function

    ''' <summary>
    ''' Updates the ID of the provided result based on the provided list.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <param name="p_results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdateID(ByVal p_result As cMCResultBasic,
                             ByRef p_results As List(Of cMCResultBasic)) As cMCResultBasic
        If (p_results Is Nothing OrElse p_results.Count = 0) Then Return p_result


        If p_result.idSet Then
            If Not UpdateNewResultIDOrExistingResult(p_result, p_results) Then Return p_result
        End If

        _results = p_results

        p_result = UpdateQueryID(p_result)
        p_result = UpdateBenchmarkID(p_result)

        p_results = _results
        Return p_result
    End Function

    Private Function UpdateNewResultIDOrExistingResult(ByVal p_result As cMCResultBasic,
                                                       ByRef p_results As List(Of cMCResultBasic)) As Boolean
        Dim idIsUnique As Boolean = True
        Dim existingResult As cMCResultBasic = New cMCResultBasic
        For Each result As cMCResultBasic In p_results
            If p_result.id = result.id Then
                idIsUnique = False
                existingResult = result
                Exit For
            End If
        Next

        ' If IDs are not assigned, then they should automatically be updated.
        If String.IsNullOrWhiteSpace(p_result.id) Then
            Return True
        ElseIf idIsUnique Then
            Return False
        ElseIf OverwriteExistingResultID(p_result, existingResult, p_results) Then
            Return False
        ElseIf Not UpdateNewResultID() Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function OverwriteExistingResultID(ByVal p_result As cMCResultBasic,
                                               ByVal p_existingResult As cMCResultBasic,
                                               ByRef p_results As List(Of cMCResultBasic)) As Boolean
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning, eMessageActions.No),
                                            "The ID " & p_result.id & " is not unique to the list it is being added to." & Environment.NewLine & Environment.NewLine &
                                            "Would you like to overwrite the existing result of the same ID?" & Environment.NewLine & Environment.NewLine &
                                            "Existing Result: " & Environment.NewLine &
                                            "    Query: " & p_existingResult.query.asString & Environment.NewLine &
                                            "    Benchmark: " & p_existingResult.benchmark.name & Environment.NewLine & Environment.NewLine &
                                            "New Result: " & Environment.NewLine &
                                            "    Query: " & p_result.query.asString & Environment.NewLine &
                                            "    Benchmark: " & p_result.benchmark.name,
                                            "Result ID Conflict")
            Case eMessageActions.Yes
                p_results(p_results.IndexOf(p_existingResult)) = p_result
                Return True
            Case eMessageActions.No
                Return False
        End Select
        Return False
    End Function

    Private Function UpdateNewResultID() As Boolean
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Question, eMessageActions.Yes),
                                            "Would you like to update the result ID?" & Environment.NewLine & Environment.NewLine &
                                            "If not, the result will not be added to the list",
                                            "ResultID Conflict")
            Case eMessageActions.Yes
                Return True
            Case eMessageActions.No
                Return False
        End Select
        Return False
    End Function
#End Region

#Region "Methods: Private"

    ' Query ID
    ''' <summary>
    ''' Updates all query IDs in the object list of results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateQueryIDs()
        For Each result As cMCResultBasic In _results
            result = UpdateQueryID(result)
        Next
    End Sub
    ''' <summary>
    ''' Updates the query ID of a copy of the provided result. 
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateQueryID(ByVal p_result As cMCResultBasic) As cMCResultBasic
        If p_result Is Nothing Then Return p_result

        Select Case p_result.resultType
            Case eResultType.regular
                p_result.query.ID = QueryIDValid(p_result)
            Case eResultType.excelCalculated, eResultType.postProcessed
                p_result.id = CStr(QueryIDValid(p_result))
        End Select

        Return p_result
    End Function

    ''' <summary>
    ''' Returns the maximum valid query ID to be used for the provided result within the provided list of results.
    ''' A valid ID is taken as either a current matching ID based on the unique query, or one number higher than the current highest ID in the list of results.
    ''' </summary>
    ''' <param name="p_result">Result to use for determining the query ID.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QueryIDValid(ByVal p_result As cMCResultBasic) As Integer
        Dim currentMatchingResult As cMCResultBasic = cMCResults.MatchingResult(p_result, _results, New cResultQueryComparer)
        If MatchingResultIsValid(currentMatchingResult) Then
            Return currentMatchingResult.query.ID
        Else
            currentMatchingResult = _results.Max()
            Return (currentMatchingResult.query.ID + 1)
        End If
    End Function


    ' Benchmark ID
    ''' <summary>
    ''' Updates all benchmark IDs in the object list of results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarkIDs()
        For Each result As cMCResultBasic In _results
            result = UpdateBenchmarkID(result)
        Next
    End Sub

    ''' <summary>
    ''' Updates the benchmark ID of a copy of the provided result. 
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateBenchmarkID(ByVal p_result As cMCResultBasic) As cMCResultBasic
        Dim benchmarkResults As List(Of cMCResultBasic) = ResultsByQuery(p_result.query.ID)
        Select Case p_result.resultType
            Case eResultType.regular
                p_result.benchmark.ID = BenchmarkIDValid(p_result)
            Case eResultType.excelCalculated, eResultType.postProcessed
                ' No action taken
        End Select

        Return p_result
    End Function

    ''' <summary>
    ''' Returns a list of results that all have the same query ID as the one provided.
    ''' </summary>
    ''' <param name="p_queryID">Query ID to filter results by.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultsByQuery(ByVal p_queryID As Integer) As List(Of cMCResultBasic)
        Dim results As New List(Of cMCResultBasic)
        For Each result As cMCResultBasic In _results
            If result.query.ID = p_queryID Then results.Add(result)
        Next
        Return results
    End Function

    ''' <summary>
    ''' Returns the maximum valid benchmark ID to be used for the provided result within the provided list of results.
    ''' A valid ID is taken as either the benchmark component of a current matching result ID, or one number higher than the current highest benchmark ID in the list of results.
    ''' </summary>
    ''' <param name="p_result">Result to use for determining the query ID.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BenchmarkIDValid(ByVal p_result As cMCResultBasic) As Integer
        Dim currentMatchingResult As cMCResultBasic = cMCResults.MatchingResult(p_result, _results, New cResultBenchmarkNameComparer)
        If MatchingResultIsValid(currentMatchingResult) Then
            Return currentMatchingResult.benchmark.ID
        Else
            currentMatchingResult = _results.Max()
            Return (currentMatchingResult.benchmark.ID + 1)
        End If
    End Function

    ''' <summary>
    ''' Matching result is valid if it is not nothing and the query ID has been initialized.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MatchingResultIsValid(ByVal p_result As cMCResultBasic) As Boolean
        Return (p_result IsNot Nothing AndAlso
                Not p_result.query.ID = 0)
    End Function
#End Region
End Class
