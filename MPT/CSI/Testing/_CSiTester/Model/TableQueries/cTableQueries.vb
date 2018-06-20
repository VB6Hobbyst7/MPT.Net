Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Classes which store the names, queries, and benchmarks associated with each table.
''' </summary>
''' <remarks></remarks>
Public Class cTableQueries
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _id As New cTableQueryIDs
    ''' <summary>
    ''' Represents the IDs associated with the rows and columns of a cTableQuery object and methods to set and update them.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property id As cTableQueryIDs
        Get
            Return _id
        End Get
    End Property

    Private _xmlMCResults As New List(Of cMCResult)
    ''' <summary>
    ''' Collection of all results for a given example for the corresponding table, supplied by the model control XML class.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Results As List(Of cMCResult)
        Set(ByVal value As List(Of cMCResult))
            _xmlMCResults = value
            _id.UpdateSource(value)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("xmlMCResults"))
        End Set
        Get
            Return _xmlMCResults
        End Get
    End Property

    Private _tableName As String
    ''' <summary>
    ''' Name of the table associated with the class and associated lists of queries and benchmarks.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableName As String
        Set(ByVal value As String)
            If Not _tableName = value Then
                _tableName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("tableName"))
            End If
        End Set
        Get
            Return _tableName
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Creates the class with a table name assigned, but is empty of results.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table to be associated with this class.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_tableName As String)
        tableName = p_tableName
    End Sub

    ''' <summary>
    ''' Creates the class with a table name and other pre-defined properties from the model control XML class.
    ''' </summary>
    ''' <param name="p_xmlResults">Current list of queries from a model control XMl file to be loaded into the session.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef p_xmlResults As List(Of cMCResult))
        If p_xmlResults IsNot Nothing Then
            Results = p_xmlResults
            If p_xmlResults.Count > 0 Then tableName = p_xmlResults(0).tableName
        End If
    End Sub

#End Region

#Region "Friend Methods"
    ' Results & IDs
    ''' <summary>
    ''' Updates all of the query key the benchmark key numbers, as well as the query ordering and benchmark sub-ordering.
    ''' </summary>
    ''' <param name="p_benchmarkNames">If provided, the routine will use the list of names provided for updating benchmark indices and ordering benchmarks.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateSortAllBenchmarkIDs(Optional ByVal p_benchmarkNames As Dictionary(Of Integer, List(Of String)) = Nothing)
        Try
            If (p_benchmarkNames Is Nothing OrElse
                Results.Count = 0) Then Exit Sub

            id.SetMCResultsBenchmarkIDs(p_benchmarkNames, Results)
            Results.Sort()

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ' Set All Result Objects
    ''' <summary>
    ''' Returns a collection of all of the cMCResult objects that match the provided query.
    ''' </summary>
    ''' <param name="p_queryID">Query ID to get all results for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetResultsByQuery(ByVal p_queryID As Integer) As List(Of cMCResult)
        Dim resultsByQuery As New List(Of cMCResult)

        For Each result As cMCResult In Results
            If result.query.ID = p_queryID Then resultsByQuery.Add(result)
        Next

        Return resultsByQuery
    End Function
    ''' <summary>
    ''' Replaces a collection of all of the cMCResult objects that match the provided query.
    ''' </summary>
    ''' <param name="p_queryID">Query ID to replace all results for.</param>
    ''' <param name="p_resultsNew">The collection of results for a specified query key that is to replace the original set.</param>
    ''' <param name="p_resultsOriginal">Routine is performed on the provided collection, else routine is performed on the class's collection.</param>
    ''' <remarks></remarks>
    Private Sub SetResultsByQuery(ByVal p_queryID As Integer,
                                  ByVal p_resultsNew As List(Of cMCResult),
                                  ByRef p_resultsOriginal As List(Of cMCResult))

        If (p_resultsNew Is Nothing OrElse
            p_resultsOriginal Is Nothing) Then Exit Sub

        Dim resultsByQuery As New List(Of cMCResult)
        Dim resultIsUnique As Boolean

        'Maintain collection of all queries not matching the provided query key, and for those that do, add the results from the replace list
        For Each resultCurrent As cMCResult In p_resultsOriginal
            If Not resultCurrent.query.ID = p_queryID Then
                resultsByQuery.Add(resultCurrent)
            Else
                For Each resultNew As cMCResult In p_resultsNew
                    If resultNew.query.ID = p_queryID Then

                        'Make sure the results do not already exist in the temp list
                        resultIsUnique = True
                        For Each resultTemp As cMCResult In resultsByQuery
                            If resultTemp.benchmark.ID = resultNew.benchmark.ID Then
                                resultIsUnique = False
                            End If
                        Next
                        If resultIsUnique Then resultsByQuery.Add(resultNew)
                    End If
                Next
            End If
        Next

        p_resultsOriginal = resultsByQuery
    End Sub

    ' Get/Set Result Object
    ''' <summary>
    ''' Returns the cMCResult object based on the provided query &amp; benchmark IDs.
    ''' </summary>
    ''' <param name="p_queryID">Query ID to limit the search to a subset of benchmarks. 
    ''' This corresponds to the 0-indexed row number in the dataTable.</param>
    ''' <param name="p_benchmarkIDNew">Benchmark key to search for in the subset of benchmarks.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetResultObject(ByVal p_queryID As Integer,
                                              ByVal p_benchmarkIDNew As Integer) As cMCResult
        For Each result As cMCResult In Results
            If result.query.ID = p_queryID AndAlso result.benchmark.ID = p_benchmarkIDNew Then
                Return result
            End If
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Returns the cMCResult object based on the provided query &amp; benchmark header name.
    ''' </summary>
    ''' <param name="p_queryID">Query key to limit the search to a subset of benchmarks.</param>
    ''' <param name="p_benchmarkNameNew">Name of the benchmark header value to search for in the subset of benchmarks.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetResultObject(ByVal p_queryID As Integer,
                                              ByVal p_benchmarkNameNew As String) As cMCResult
        For Each result As cMCResult In Results
            If result.query.ID = p_queryID AndAlso result.benchmark.name = p_benchmarkNameNew Then
                Return result
            End If
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Updates the cMCResult object in the collection based on the provided cMCResult object. Returns 'True' if successful.
    ''' </summary>
    ''' <param name="p_newResult">cMCResult object that is populated with the new values.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetResultObject(ByVal p_newResult As cMCResult) As Boolean

        If p_newResult Is Nothing Then Return Nothing

        For Each result As cMCResult In Results
            If result.query.ID = p_newResult.query.ID AndAlso result.benchmark.ID = p_newResult.benchmark.ID Then
                result = p_newResult
                Return True
            End If
        Next

        Return False
    End Function

    ' Remove Result
    ''' <summary>
    ''' Removes the specified result object from the list of results.
    ''' </summary>
    ''' <param name="p_result">Results object to remove.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub RemoveResult(ByVal p_result As cMCResult)
        Dim newResultsList As New List(Of cMCResult)

        For Each result As cMCResult In Results
            If Not (result.query.ID = p_result.query.ID AndAlso
                    result.benchmark.ID = p_result.benchmark.ID) Then

                newResultsList.Add(result)
            End If
        Next

        Results = newResultsList
    End Sub
    ''' <summary>
    ''' Removes the specified result object from the list of results.
    ''' </summary>
    ''' <param name="p_queryID">ID of the query group to remove.</param>
    ''' <param name="p_benchmarkID">ID of the benchmark within the query to remove.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub RemoveResult(ByVal p_queryID As Integer,
                                      ByVal p_benchmarkID As Integer)
        Dim newResultsList As New List(Of cMCResult)

        For Each result As cMCResult In Results
            If Not (result.query.ID = p_queryID AndAlso
                    result.benchmark.ID = p_benchmarkID) Then

                newResultsList.Add(result)
            End If
        Next

        Results = newResultsList
    End Sub

#End Region

End Class
