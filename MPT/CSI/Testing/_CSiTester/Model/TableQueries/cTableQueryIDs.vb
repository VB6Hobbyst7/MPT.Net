Option Explicit On
Option Strict On

Imports MPT.Reporting

''' <summary>
''' Represents the IDs associated with the rows and columns of a cTableQuery object.
''' Contains methods to set and update the IDs.
''' </summary>
''' <remarks></remarks>
Friend Class cTableQueryIDs
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Fields"
    Private _xmlMCResults As New List(Of cMCResult)
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Initializes the class with a workable set of results to apply methods to.
    ''' </summary>
    ''' <param name="p_xmlResults">List of results to bind the object to.
    ''' Nearly all methods act on results in this list.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef p_xmlResults As List(Of cMCResult))
        UpdateSource(p_xmlResults)
    End Sub

#End Region

#Region "Methods: Public"
    Friend Sub UpdateSource(ByRef p_xmlResults As List(Of cMCResult))
        _xmlMCResults = p_xmlResults
    End Sub

    ' Set Query ID
    ''' <summary>
    ''' Assigns the query ID to the MC result objects based on the ordering of the result query in a list of unique queries.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMCResultsQueryIDs()
        Dim queryLists As New List(Of Dictionary(Of String, String))

        For Each mcResult As cMCResult In _xmlMCResults
            SetMCResultQueryID(mcResult, queryLists)
        Next
    End Sub

    ' Set Benchmark ID
    ''' <summary>
    ''' Sorts the benchmarks IDs of the specified query to the order in which their names are listed in the column indices and updates the IDs accordingly.
    ''' </summary>
    ''' <param name="p_queryID">Query ID that corresponds to the query row within which to order the benchmarks.</param>
    ''' <param name="p_benchmarkIDs">Dictionary list of the original benchmark keys and their paired column indices.</param>
    ''' <remarks></remarks>
    Friend Sub SetBenchmarkIDsByColIndex(ByVal p_queryID As Integer,
                                         ByVal p_benchmarkIDs As Dictionary(Of Integer, Integer))

        If p_benchmarkIDs Is Nothing Then Exit Sub

        'Create a collection of all result objects that match the specified query key
        Dim queryIDResultSet As New List(Of cMCResult)
        For Each resultSubset As cMCResult In _xmlMCResults
            If resultSubset.query.ID = p_queryID Then queryIDResultSet.Add(resultSubset)
        Next

        SetBenchmarkIDsByColIndex(queryIDResultSet, p_benchmarkIDs)
    End Sub

    ''' <summary>
    ''' Updates all of the query key the benchmark key numbers, as well as the query ordering and benchmark sub-ordering.
    ''' </summary>
    ''' <param name="p_benchmarkNames">If provided, the routine will use the list of names provided for updating benchmark indices and ordering benchmarks.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateSortAllBenchmarkIDs(Optional ByVal p_benchmarkNames As Dictionary(Of Integer, List(Of String)) = Nothing)
        Try
            If p_benchmarkNames Is Nothing Then Exit Sub

            SetMCResultsBenchmarkIDs(p_benchmarkNames, _xmlMCResults)
            _xmlMCResults.Sort()

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the benchmark key numbers for all benchmarks that share a common lookup query.
    ''' </summary>
    ''' <param name="p_benchmarkNames">The list of unique benchmark names, indexed by query ID, provided for assigning benchmark indices. 
    ''' Use caution with this parameter if the routine is operating on more than one query key, as results may be unexpected.</param>
    ''' <param name="p_mcResults">If provided, the routine will perform the operation on the collection. 
    ''' Otherwise, the routine will perform the operation on the class collection.</param>
    ''' <remarks></remarks>
    Friend Sub SetMCResultsBenchmarkIDs(ByRef p_benchmarkNames As Dictionary(Of Integer, List(Of String)),
                                         ByRef p_mcResults As List(Of cMCResult))
        Try
            If ((p_benchmarkNames Is Nothing OrElse p_benchmarkNames.Keys.Count = 0) OrElse
                (p_mcResults Is Nothing OrElse p_mcResults.Count = 0)) Then Exit Sub

            Dim queryIDCurrentMax As Integer = p_mcResults.Max().query.ID
            Dim queryIDCurrentMin As Integer = p_mcResults.Min().query.ID

            For queryID = queryIDCurrentMin To queryIDCurrentMax
                For Each mcResult As cMCResult In p_mcResults
                    SetMCResultBenchmarkID(mcResult, p_benchmarkNames, queryID)
                Next
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ' Update IDs
    ''' <summary>
    ''' Updates IDs that match the provided old list to the corresponding new ID in the new list.
    ''' </summary>
    ''' <param name="p_oldIDs">List of the old IDs.</param>
    ''' <param name="p_newIDs">List of the new IDs correlated with each index of the list of old IDs.</param>
    ''' <param name="p_maxID">Length of the list containing the IDs list (ID numbers are not necessarily in order or contiguous).</param>
    ''' <param name="p_queryID">The update will only be performed for the benchmark IDs of the specified query ID. 
    ''' Operation will be performed to all results if not specified.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateIDs(ByVal p_oldIDs As List(Of Integer),
                          ByVal p_newIDs As List(Of Integer),
                          ByVal p_maxID As Integer,
                          Optional ByVal p_queryID As Integer = -1)

        If (p_oldIDs Is Nothing OrElse
            p_newIDs Is Nothing) Then Exit Sub

        'IDs shifted the length of the new reference set in order to avoid reassignment to an existing ID.
        ShiftIDs(p_maxID, p_oldIDs, p_queryID)

        If p_queryID < 0 Then
            UpdateQueryIDs(p_oldIDs, p_newIDs)
        Else
            UpdateBenchmarkIDs(p_oldIDs, p_newIDs, p_queryID)
        End If
    End Sub

    ' Query Result Object IDs
    ''' <summary>
    ''' Returns a unique and ordered list of query IDs for all result objects.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function QueryIDsFromResults() As List(Of Integer)
        Dim queryIDs As New List(Of Integer)
        Dim queryIDUnique As Boolean
        Dim queryIDCurrent As Integer

        For Each result As cMCResult In _xmlMCResults
            queryIDUnique = True
            queryIDCurrent = result.query.ID
            For Each queryKey As Integer In queryIDs
                If queryKey = queryIDCurrent Then queryIDUnique = False
            Next
            If queryIDUnique Then queryIDs.Add(result.query.ID)
            queryIDCurrent += 1
        Next
        queryIDs.Sort()

        Return queryIDs
    End Function
#End Region

#Region "Methods: Private"
    ' Set Query ID
    ''' <summary>
    ''' Sets the result query ID based on a matching query ID from a provided list.
    ''' The list is modified so that it can be cumulative if this method is called in a loop.
    ''' </summary>
    ''' <param name="p_result">Result to determine and set a query ID.</param>
    ''' <param name="p_queryLists">List of unique queries.
    ''' This is modified so that it can be cumulative if this method is called in a loop.</param>
    ''' <remarks></remarks>
    Private Sub SetMCResultQueryID(ByRef p_result As cMCResult,
                                    ByRef p_queryLists As List(Of Dictionary(Of String, String)))

        'Check if the query dictionary is unique to the list, or if not, increment the list position
        Dim query As Dictionary(Of String, String) = CreateQuery(p_result)
        Dim queryID As Integer

        'Add new query list to overall list if a match is never found
        If Not QueryMatch(queryID, query, p_queryLists) Then p_queryLists.Add(query)

        'Add entry number as key to current result item
        p_result.query.ID = queryID
    End Sub

    ''' <summary>
    ''' Returns a dictionary of name/value pairs for each query component in the result.
    ''' </summary>
    ''' <param name="p_result">Result to use for formulating the query.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateQuery(ByVal p_result As cMCResult) As Dictionary(Of String, String)
        Dim queries As New Dictionary(Of String, String)
        For Each fieldLookup As cFieldLookup In p_result.query
            queries.Add(fieldLookup.name, fieldLookup.valueField)
        Next

        Return queries
    End Function

    ''' <summary>
    ''' True: The queries list contains a matching query to the one provided. Otherwise, false.
    ''' A query ID argument is also modified that is the appropriate index for the provided query in the total list.
    ''' It will be larger than the current queries list if the query is unique to the list.
    ''' </summary>
    ''' <param name="p_queryID">Index position were a query match was found. 
    ''' If no match was found, this is the next index to use in the queries for a new entry.</param>
    ''' <param name="p_query">Name/value pairs for each query component in a result.</param>
    ''' <param name="p_queries">Current list of known existing unique queries.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QueryMatch(ByRef p_queryID As Integer,
                                ByVal p_query As Dictionary(Of String, String),
                                ByVal p_queries As List(Of Dictionary(Of String, String))) As Boolean
        p_queryID = 0
        For Each queryExisting As Dictionary(Of String, String) In p_queries
            If QueryMatch(p_query, queryExisting) Then
                Return True
            Else
                p_queryID += 1
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' True: The two queries match for all components.
    ''' </summary>
    ''' <param name="p_query1">Name/value pairs for each query component in a result.</param>
    ''' <param name="p_query2">Current known existing unique query.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QueryMatch(ByVal p_query1 As Dictionary(Of String, String),
                                ByVal p_query2 As Dictionary(Of String, String)) As Boolean
        Dim isMatching As Boolean = True

        ' Check if all query items have a match 
        For i = 0 To p_query1.Count - 1
            Dim queryName As String = p_query1.Keys(i)
            If (Not p_query2.ContainsKey(queryName) OrElse
                Not p_query2.Item(queryName) = p_query1.Item(queryName)) Then

                Return False
            End If
        Next

        Return True
    End Function

    ' Set Benchmark ID
    ''' <summary>
    ''' Sorts the benchmark IDs of the provided results to the order in which their names are listed in the column indices and updates the IDs accordingly.
    ''' </summary>
    ''' <param name="p_benchmarkIDs">Dictionary list of the original benchmark IDs and their paired column indices.</param>
    ''' <param name="p_resultsByQuery">The results that share the same query.</param>
    ''' <remarks></remarks>
    Private Sub SetBenchmarkIDsByColIndex(ByRef p_resultsByQuery As List(Of cMCResult),
                                          ByVal p_benchmarkIDs As Dictionary(Of Integer, Integer))
        ''Create list of benchmark keys sorted by the column indices
        Dim benchmarkIDs As New List(Of Integer)
        Dim columnIndices As List(Of Integer) = p_benchmarkIDs.Keys.ToList
        columnIndices.Sort()

        For Each colIndex As Integer In columnIndices
            benchmarkIDs.Add(p_benchmarkIDs.Item(colIndex))
        Next

        '' Sync BM old list to new order
        Dim currentBenchmarkID As Integer = 0
        For Each benchmarkID As Integer In benchmarkIDs
            For Each result As cMCResult In p_resultsByQuery
                If result.benchmark.ID = benchmarkID Then result.benchmark.ID = currentBenchmarkID
            Next

            currentBenchmarkID += 1
        Next
    End Sub

    ''' <summary>
    ''' Update benchmark ID according to the corresponding benchmarks name list.
    ''' </summary>
    ''' <param name="mcResult">Result to determine and set a benchmark ID.</param>
    ''' <param name="p_benchmarkNames">The list of unique benchmark names, indexed by query ID, provided for assigning benchmark indices.</param>
    ''' <param name="p_queryID">Current query ID by for which benchmarks are to be set.</param>
    ''' <remarks></remarks>
    Private Sub SetMCResultBenchmarkID(ByRef mcResult As cMCResult,
                                       ByRef p_benchmarkNames As Dictionary(Of Integer, List(Of String)),
                                       ByVal p_queryID As Integer)
        'MC result only applies to the current unique query considered
        If (Not mcResult.query.ID = p_queryID OrElse
            Not p_benchmarkNames.Keys.Contains(p_queryID)) Then Exit Sub

        SetMCResultBenchmarkIDByListOrder(mcResult, p_benchmarkNames(p_queryID))

    End Sub

    ''' <summary>
    ''' Sets the benchmark ID for the supplied MC result object. 
    ''' Incrementally adds the current benchmark name to the supplied benchmark list if the name is unique to the list and the list is not from an external source.
    ''' </summary>
    ''' <param name="p_benchmarksList">Current list of unique benchmarks used for a given unique query.</param>
    ''' <param name="p_mcResult">Current MC result object being altered.</param>
    ''' <remarks></remarks>
    Private Sub SetMCResultBenchmarkIDByListOrder(ByRef p_mcResult As cMCResult,
                                                  ByRef p_benchmarksList As List(Of String))
        Dim benchmarkName As String = p_mcResult.benchmark.name
        Dim benchmarkID As Integer = GetBenchmarkIDByListOrder(benchmarkName, p_benchmarksList)

        'Assign new benchmark to overall list if a match is never found. This is only for lists generated from the existing benchmarks.
        If (Not benchmarkID >= 0 OrElse
            (Not String.IsNullOrEmpty(benchmarkName) AndAlso
             p_benchmarksList.Count = 0)) Then

            p_benchmarksList.Add(benchmarkName)
        End If

        If benchmarkID >= 0 Then p_mcResult.benchmark.ID = benchmarkID
    End Sub

    ''' <summary>
    ''' Gets the index of the benchmark of the supplied name from within the list of supplied benchmark names. 
    ''' Returns 0 if the benchmark name is blank, since there is, at a minimum, 1 benchmark associated with each result, even if it is blank.
    ''' Returns -1 if no match is found.
    ''' </summary>
    ''' <param name="p_benchmarkName">Name of the benchmark value for which the benchmark ID is being determined.</param>
    ''' <param name="p_benchmarkNames">Current list of unique benchmark names used for a given unique query.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBenchmarkIDByListOrder(ByVal p_benchmarkName As String,
                                               ByVal p_benchmarkNames As List(Of String)) As Integer
        If String.IsNullOrEmpty(p_benchmarkName) Then Return 0
        Dim benchmarkID As Integer = 0

        If p_benchmarkNames.Count >= 0 Then
            For Each benchmarkName As String In p_benchmarkNames
                If benchmarkName = p_benchmarkName Then
                    Return benchmarkID
                Else
                    benchmarkID += 1
                End If
            Next
        End If

        Return -1
    End Function

    ' Update IDs
    ''' <summary>
    ''' Updates query IDs that match the provided old list to the corresponding new ID in the new list.
    ''' </summary>
    ''' <param name="p_oldIDs">List of the old IDs.</param>
    ''' <param name="p_newIDs">List of the new IDs correlated with each index of the list of old IDs.</param>
    ''' <remarks></remarks>
    Private Sub UpdateQueryIDs(ByVal p_oldIDs As List(Of Integer),
                               ByVal p_newIDs As List(Of Integer))
        For i = 0 To p_oldIDs.Count - 1
            For Each mcResult As cMCResult In _xmlMCResults
                If mcResult.query.ID = p_oldIDs(i) Then
                    mcResult.query.ID = p_newIDs(i)
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Updates benchmark IDs that match the provided old list to the corresponding new ID in the new list.
    ''' </summary>
    ''' <param name="p_oldIDs">List of the old IDs.</param>
    ''' <param name="p_newIDs">List of the new IDs correlated with each index of the list of old IDs.</param>
    ''' <param name="p_queryID">The update will only be performed for the benchmark IDs of the specified query ID.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarkIDs(ByVal p_oldIDs As List(Of Integer),
                                   ByVal p_newIDs As List(Of Integer),
                                   ByVal p_queryID As Integer)
        For i = 0 To p_oldIDs.Count - 1
            For Each mcResult As cMCResult In _xmlMCResults
                If (mcResult.query.ID = p_queryID AndAlso mcResult.benchmark.ID = p_oldIDs(i)) Then
                    mcResult.benchmark.ID = p_newIDs(i)
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Shifts all key indices by the length specified.
    ''' No cleanup is done of potential IDs beyond the length of the new reference set, so unmatched values might still exist.
    ''' This is intentional as it might be undesirable to remove results in the case of viewing a shortened table that happens to not include the results.
    ''' </summary>
    ''' <param name="p_shiftID">Length +1 by which to shift all current IDs.</param>
    ''' <param name="p_oldIDs">The old IDs to shift in sync with the shift in object properties.</param>
    ''' <param name="p_queryID">The shift will be performed on the benchmark IDs of the specified query ID.</param>
    ''' <remarks></remarks>
    Private Sub ShiftIDs(ByVal p_shiftID As Integer,
                          ByRef p_oldIDs As List(Of Integer),
                          Optional ByVal p_queryID As Integer = -1)
        If p_queryID < 0 Then
            ShiftQueryIDs(p_shiftID)
        Else
            ShiftBenchmarkIDs(p_shiftID, p_queryID)
        End If

        ShiftOldIDs(p_shiftID, p_oldIDs)
    End Sub

    ''' <summary>
    ''' Shifts the query IDs by the length specified.
    ''' </summary>
    ''' <param name="p_shiftID">Length +1 by which to shift all current IDs.</param>
    ''' <remarks></remarks>
    Private Sub ShiftQueryIDs(ByVal p_shiftID As Integer)
        For Each mcResult As cMCResult In _xmlMCResults
            mcResult.query.ID += p_shiftID + 1
        Next
    End Sub

    ''' <summary>
    ''' Shifts the benchmark IDs corresponding to the specified query ID by the length specified.
    ''' </summary>
    ''' <param name="p_shiftID">Length +1 by which to shift all current IDs.</param>
    ''' <param name="p_queryID">The shift will be performed on the benchmark IDs of the specified query ID.</param>
    ''' <remarks></remarks>
    Private Sub ShiftBenchmarkIDs(ByVal p_shiftID As Integer,
                                  ByVal p_queryID As Integer)
        For Each mcResult As cMCResult In _xmlMCResults
            If mcResult.query.ID = p_queryID Then
                mcResult.benchmark.ID += p_shiftID + 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates the old IDs by the length specified to match the new IDs.
    ''' </summary>
    ''' <param name="p_shiftID">Length +1 by which to shift all current IDs.</param>
    ''' <param name="p_oldIDs">The old IDs to shift in sync with the shift in object properties.</param>
    ''' <remarks></remarks>
    Private Sub ShiftOldIDs(ByVal p_shiftID As Integer,
                            ByRef p_oldIDs As List(Of Integer))
        For i = 0 To p_oldIDs.Count - 1
            p_oldIDs(i) = p_oldIDs(i) + p_shiftID + 1
        Next
    End Sub
#End Region
End Class
