Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Public Class cMCQuery
    Inherits PropertyChangerCollections
    Implements ICloneable
    Implements IComparable(Of cMCQuery)
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Enumerations"
    ''' <summary>
    ''' Decscribes the quality of a query for a given result.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eQueryType
        ''' <summary>
        ''' The query is blank.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("None")> None = 0
        ''' <summary>
        ''' Query is not found in a queried table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Not Found")> NotFound
        ''' <summary>
        ''' The query returns multiple matches from a queried table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Incomplete")> Incomplete
        ''' <summary>
        ''' The query returns a single row from a queried table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Unique")> Unique
    End Enum
#End Region

#Region "Properties"

    Private _id As Integer = -1
    ''' <summary>
    ''' Number identifying the unique query.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            If (Not value = _id AndAlso
                value >= 0) Then
                _id = value
                RaisePropertyChanged("ID")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cFieldLookup
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' If 'True', then a query has been specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isSpecified As Boolean
        Get
            Return (Not String.IsNullOrEmpty(asString))
        End Get
    End Property

    Private _isUnique As Boolean
    ''' <summary>
    ''' If 'True', then the minimum data is present for a complete query that only returns one row. 
    ''' Otherwise, the example is not yet complete to minimum requirements.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property isUnique As Boolean
        Get
            Return _isUnique
        End Get
    End Property
    ''' <summary>
    ''' Checks the table object and name provided to determine the status. 
    ''' If 'True', then the minimum data is present for a complete query that only returns one row. 
    ''' Otherwise, the example is not yet complete to minimum requirements.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads ReadOnly Property isUnique(ByVal p_dataTable As DataTable,
                                                ByVal p_tableName As String) As Boolean
        Get
            GetSetQueryType(p_dataTable, p_tableName, p_updateResult:=True)
            Return _isUnique
        End Get
    End Property

    Protected _asString As String
    ''' <summary>
    ''' String of the query produced by the various class components to identify a unique row. Does not include table name or extraction value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property asString As String
        Get
            Return _asString
        End Get
    End Property
#End Region

#Region "Initialization"
    Public Sub New()

    End Sub
#End Region

#Region "Methods: Override/Overload"
    ''' <summary>
    ''' Adds a new query item object to the list if it is unique.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As cFieldLookup)
        Try
            If p_item Is Nothing Then Exit Sub

            If QueryItemIsUnique(p_item) Then
                InnerList.Add(p_item)
                SortItemsByFieldName()
                RaisePropertyChanged("item")
            End If
            UpdateProperties()
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Removes the query item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaisePropertyChanged("item")
            UpdateProperties()
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the query item using the specified header.
    ''' </summary>
    ''' <param name="p_header"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_header As String)
        Try
            If String.IsNullOrEmpty(p_header) Then Exit Sub

            Dim indexRemove As Integer = 0
            For Each queryItem As cFieldLookup In InnerList
                If queryItem.name = p_header Then
                    Exit For
                End If
                indexRemove += 1
            Next

            If indexRemove < InnerList.Count Then
                InnerList.Remove(indexRemove)
                RaisePropertyChanged("item")
                UpdateProperties()
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the matching query object with the one provided. 
    ''' A match is determined by the header name.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_item As cFieldLookup)
        Try
            If p_item Is Nothing Then Exit Sub
            Dim itemChanged As Boolean = False

            For Each item As cFieldLookup In InnerList
                If p_item.Equals(item) Then
                    item = p_item
                    itemChanged = True
                    RaisePropertyChanged("item")
                    UpdateProperties()
                    Exit For
                End If
            Next

            If Not itemChanged Then
                InnerList.Add(p_item)
            End If

        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cFieldLookup)
        Dim templist As New ObservableCollection(Of cFieldLookup)

        For Each item As cFieldLookup In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cFieldLookup)
        Dim templist As New List(Of cFieldLookup)

        For Each item As cFieldLookup In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCQuery

        With myClone
            .ID = ID
            .SetQuery(asString)
            ._isUnique = _isUnique
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCQuery) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCQuery = TryCast(p_object, cMCQuery)

        ' Check for any differences
        If comparedObject Is Nothing Then Return False

        For Each resultOuter As cFieldLookup In comparedObject
            isMatch = False
            For Each resultInner As cFieldLookup In InnerList
                If resultOuter.Equals(resultInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        With comparedObject
            ' Note: Do not compare IDs or string representation.
            If Not ._isUnique = _isUnique Then Return False
        End With

        Return True
    End Function

    Public Function CompareTo(other As cMCQuery) As Integer Implements IComparable(Of cMCQuery).CompareTo
        If ID.CompareTo(other.ID) <> 0 Then
            Return ID.CompareTo(other.ID)
        Else
            Return 0
        End If
    End Function

    Public Overrides Function ToString() As String
        Return (MyBase.ToString() & ": ID " & ID & " - " & asString)
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Adds query string to object and updates cFieldLookup classes.
    ''' </summary>
    ''' <param name="p_query">Current query string to update the MC result to.</param>
    ''' <remarks></remarks>
    Friend Sub SetQuery(ByVal p_query As String)
        If Not _asString = p_query Then ParseToToClasses(p_query)
    End Sub

    ''' <summary>
    ''' Returns 'True' if the provided query item is unique in the called object's list of queries items.
    ''' </summary>
    ''' <param name="p_queryItem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function QueryItemIsUnique(ByVal p_queryItem As cFieldLookup) As Boolean
        For Each queryItem As cFieldLookup In InnerList
            If p_queryItem.Equals(queryItem) Then Return False
        Next
        Return True
    End Function

    ''' <summary>
    ''' Returns the current classification of the query based on a provided DataTable.
    ''' If a proper DataTable is submitted (e.g. matching table name), then the relevant object properties will also be updated.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to apply the query to.</param>
    ''' <param name="p_tableName">Name of the table to use.</param>
    ''' <param name="p_updateResult">If 'True', then the corresponding result properties will be updated based on the result.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSetQueryType(ByVal p_dataTable As DataTable,
                                     ByVal p_tableName As String,
                                     Optional ByVal p_updateResult As Boolean = False) As eQueryType
        Dim rows As DataRow()
        Dim queryStatus As eQueryType

        If ParseTableName(p_dataTable.TableName, True) = ParseTableName(p_tableName, True) Then
            If Not isSpecified Then
                queryStatus = eQueryType.None
            Else
                rows = p_dataTable.Select(asString)

                If rows.Count = 0 Then
                    queryStatus = eQueryType.NotFound
                ElseIf rows.Count = 1 Then
                    queryStatus = eQueryType.Unique
                Else
                    queryStatus = eQueryType.Incomplete
                End If
            End If

            'Set property
            If p_updateResult Then
                If queryStatus = eQueryType.Unique Then
                    _isUnique = True
                Else
                    _isUnique = False
                End If
                RaisePropertyChanged("isUnique")
            End If
        Else
            queryStatus = eQueryType.NotFound
        End If

        Return queryStatus
    End Function

    ''' <summary>
    ''' Sorts the query lookup items alphabetically by the field name.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SortItemsByFieldName()
        Dim queryItemSorter As New cFieldLookupNameComparer
        InnerList.Sort(queryItemSorter)
    End Sub
#End Region

#Region "Methods:  Override/Overload"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cFieldLookup
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cFieldLookup)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Takes the data stored in the cFieldLookup classes and writes them into a single string search query.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function WriteFromClasses() As String
        Dim tempString As String = ""
        Dim fieldLookupIndex = 0
        Try
            If InnerList IsNot Nothing Then
                For Each fieldLookup As cFieldLookup In InnerList
                    If Not fieldLookupIndex = 0 Then tempString = tempString & " AND "
                    tempString = tempString & fieldLookup.name & " = '" & fieldLookup.valueField & "'"
                    fieldLookupIndex += 1
                Next
            End If
            Return tempString
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return ""
    End Function

    ''' <summary>
    ''' Takes a single string specifying a query search, breaks it down into name and field value components, creates a new class for the query set, and assigns the values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ParseToToClasses(ByVal p_query As String)
        Dim keyStart As Integer = 1
        Dim keyEnd As Integer
        Dim valStart As Integer
        Dim valEnd As Integer
        Dim addItem As Boolean = False
        Try
            InnerList.Clear()

            If Not String.IsNullOrEmpty(p_query) Then
                For i = 1 To Len(p_query)

                    If StringsMatch(Mid(p_query, i, 4), "LIKE") Then
                        keyEnd = i - 2
                        valStart = i + 5
                    ElseIf StringsMatch(Mid(p_query, i, 1), "=") Then
                        keyEnd = i - 2
                        valStart = i + 2
                    ElseIf StringsMatch(Mid(p_query, i, 3), "AND") Then
                        Dim fieldLookup As New cFieldLookup
                        valEnd = i - 2

                        fieldLookup.name = Mid(p_query, keyStart, keyEnd - keyStart + 1)
                        fieldLookup.valueField = Mid(p_query, valStart, valEnd - valStart + 1)

                        InnerList.Add(fieldLookup)
                        addItem = True

                        keyStart = i + 4
                    ElseIf i = Len(p_query) Then
                        Dim fieldLookup As New cFieldLookup
                        valEnd = i
                        With fieldLookup
                            .name = Mid(p_query, keyStart, keyEnd - keyStart + 1)
                            .valueField = Mid(p_query, valStart, valEnd - valStart + 1)
                            If Left(.valueField, 1) = "'" Then .valueField = Right(.valueField, Len(.valueField) - 1) 'Remove any starting single quotes
                            If Right(.valueField, 1) = "'" Then .valueField = Left(.valueField, Len(.valueField) - 1) 'Remove any ending single quotes
                        End With

                        InnerList.Add(fieldLookup)
                        addItem = True
                    End If
                Next
            End If

            If addItem Then
                RaisePropertyChanged("item")
                UpdateProperties()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates all properties in the class that are dependent upon the cFieldLookup properties.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateProperties()
        Dim queryStringIsSpecified As Boolean = String.IsNullOrEmpty(_asString)
        Dim queryString As String = WriteFromClasses()
        If Not StringsMatch(queryString, _asString) Then
            _asString = queryString
            RaisePropertyChanged("asString")
        End If
        If (Not queryStringIsSpecified = String.IsNullOrEmpty(queryString)) Then
            RaisePropertyChanged("isSpecified")
        End If
    End Sub
#End Region
End Class
