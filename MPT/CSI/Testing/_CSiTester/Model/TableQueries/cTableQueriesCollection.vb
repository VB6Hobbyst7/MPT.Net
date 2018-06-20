Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Class which stores an observable collection of table query objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cTableQueriesCollection
    Inherits System.Collections.CollectionBase
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
#Region "Variables"
    ''' <summary>
    ''' List of cTableQueries objects, which store the names, queries, and benchmarks associated with each table.
    ''' </summary>
    ''' <remarks></remarks>
    Private _tableQueries As ObservableCollection(Of cTableQueries)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cTableQueries
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified table name.
    ''' </summary>
    ''' <param name="p_tableName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_tableName As String) As cTableQueries
        Get
            Return GetItem(p_tableName)
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Creates a new observable collection of cTableQueries objects.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        '_tableQueries = New ObservableCollection(Of cTableQueries)
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Adds a new cTableQueries object to the collection of cTableQuery objects based on the provided table name and results objects.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table that the table query objects correspond to.</param>
    ''' <param name="p_mcResults">Name of the results object from which to derive the table query object.</param>
    ''' <remarks></remarks>
    Friend Sub Add(ByVal p_tableName As String,
                    ByVal p_mcResults As List(Of cMCResult))
        Try
            If p_mcResults Is Nothing Then Throw New ArgumentException("p_mcResults is not initialized.")
            If String.IsNullOrEmpty(p_tableName) Then Throw New ArgumentException("p_tableName is not specified.")

            InnerList.Add(CreateTableQueriesCollection(p_tableName, p_mcResults))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Removes the item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index > InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _tableQueries.Count.ToString)

            InnerList.RemoveAt(p_index)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Removes the item that has the specified table name.
    ''' </summary>
    ''' <param name="p_tableName">Table name that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_tableName As String)
        Try
            If String.IsNullOrWhiteSpace(p_tableName) Then Throw New ArgumentException("p_tableName is not specified.")

            For Each tableQuery As cTableQueries In InnerList
                If tableQuery.tableName = p_tableName Then
                    InnerList.Remove(tableQuery)
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Creates a collection of results objects for the table name provided.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table for which to create the collection of result objects.</param>
    ''' <param name="p_mcResults">Collection of results objects from which the new collection by table name will be created.</param>
    ''' <remarks></remarks>
    Private Function CreateTableQueriesCollection(ByVal p_tableName As String,
                                                  ByVal p_mcResults As List(Of cMCResult)) As cTableQueries
        Dim tableMCResults As New List(Of cMCResult)
        Dim newTableQuery As cTableQueries = Nothing

        Try
            For Each tableMCResult As cMCResult In p_mcResults
                If tableMCResult.tableName = p_tableName Then tableMCResults.Add(tableMCResult)
            Next

            If tableMCResults.Count = 0 Then                        'Add empty collection with table name assigned to the cTableQueries class
                newTableQuery = New cTableQueries(p_tableName)
                Return newTableQuery
            Else                                                    'Add collection of table results to the cTableQueries class
                newTableQuery = New cTableQueries(tableMCResults)

                'Sort by unique query & benchmark, set list keys
                With newTableQuery
                    .id.SetMCResultsQueryIDs()
                    .Results.Sort()
                End With
                Return newTableQuery
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return newTableQuery
    End Function

    ''' <summary>
    ''' Returns the table queries object specified by index.
    ''' </summary>
    ''' <param name="p_index">Index corresponding to the table queries object to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cTableQueries
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index > InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _tableQueries.Count.ToString)

            Return CType(InnerList(p_index), cTableQueries)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the table queries object specified by name.
    ''' </summary>
    ''' <param name="p_tableName">Table name that corresponds to the item to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_tableName As String) As cTableQueries
        Try
            If String.IsNullOrWhiteSpace(p_tableName) Then Throw New ArgumentException("p_tableName is not specified.")

            For Each tableQuery As cTableQueries In InnerList
                If tableQuery.tableName = p_tableName Then
                    Return tableQuery
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function
#End Region

End Class