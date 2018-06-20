Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

''' <summary>
''' Class which stores an observable collection of table view objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cTableQueryViews
    Inherits System.Collections.CollectionBase
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Variables"
    ''' <summary>
    ''' List of table view objects, which store the names, queries, and benchmarks associated with each table that are currently visible for a display (no redundant rows).
    ''' </summary>
    ''' <remarks></remarks>
    Private _tableViews As ObservableCollection(Of cTableQueryView)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cTableQueryView
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
    Default Friend Overloads ReadOnly Property item(ByVal p_tableName As String) As cTableQueryView
        Get
            Return GetItem(p_tableName)
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Creates a new observable collection of objects.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()

    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Adds a new table view object to the collection of table view objects based on the provided table view.
    ''' </summary>
    ''' <param name="p_tableQueryView">Table view object to add.</param>
    ''' <remarks></remarks>
    Friend Sub Add(ByVal p_tableQueryView As cTableQueryView)
        Try
            If p_tableQueryView Is Nothing Then Throw New ArgumentException("p_tableQueryView is not initialized.")

            InnerList.Add(p_tableQueryView)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Adds a new table view object to the collection of table view objects based on the provided table view, if it does not currently exist in the list. 
    ''' Uniqueness is based on the table name.
    ''' </summary>
    ''' <param name="p_tableQueryView">Table view object to add.</param>
    ''' <remarks></remarks>
    Friend Sub AddIfUnique(ByVal p_tableQueryView As cTableQueryView)
        Try
            If p_tableQueryView Is Nothing Then Throw New ArgumentException("p_tableQueryView is not initialized.")

            Dim newTableName As String = p_tableQueryView.exportedSetTable.TableName
            Dim isUnique As Boolean = True

            For Each tableView As cTableQueryView In InnerList
                If tableView.exportedSetTable.TableName = newTableName Then
                    isUnique = False
                    Exit For
                End If
            Next

            If isUnique Then InnerList.Add(p_tableQueryView)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Updates an existing table view object to the collection of table view objects based on the provided table view, if it currently exists in the list. 
    ''' The table view is identified based on the table name.
    ''' </summary>
    ''' <param name="p_tableQueryView">Table view object to add.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateEntry(ByVal p_tableQueryView As cTableQueryView)
        Dim isTableUpdated As Boolean = False
        Try
            If p_tableQueryView Is Nothing Then Throw New ArgumentException("p_tableQueryView is not initialized.")

            Dim newTableName As String = p_tableQueryView.exportedSetTable.TableName
            Dim isUnique As Boolean = True

            For Each tableView As cTableQueryView In InnerList
                If tableView.exportedSetTable.TableName = newTableName Then
                    tableView = p_tableQueryView
                    isTableUpdated = True
                    Exit For
                End If
            Next

            If Not isTableUpdated Then AddIfUnique(p_tableQueryView)
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
            If p_index > InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _tableViews.Count.ToString)

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
            If String.IsNullOrEmpty(p_tableName) Then Throw New ArgumentException("p_tableName is not specified.")

            For Each tableQuery As cTableQueryView In InnerList
                If tableQuery.exportedSetTable.TableName = p_tableName Then
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
    ''' Returns the table queries object specified by index.
    ''' </summary>
    ''' <param name="p_index">Index corresponding to the table queries object to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cTableQueryView
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index > InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _tableViews.Count.ToString)

            Return CType(InnerList(p_index), cTableQueryView)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the table queriey view object specified by name.
    ''' </summary>
    ''' <param name="p_tableName">Table name that corresponds to the item to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_tableName As String) As cTableQueryView
        Try
            If String.IsNullOrEmpty(p_tableName) Then Throw New ArgumentException("p_tableName is not specified.")

            p_tableName = ParseTableName(p_tableName, True)

            For Each tableQuery As cTableQueryView In InnerList
                If ParseTableName(tableQuery.exportedSetTable.TableName, True) = p_tableName Then
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
