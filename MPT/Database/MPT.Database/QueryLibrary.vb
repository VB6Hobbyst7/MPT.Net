Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.FileSystem.PathLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting
Imports MPT.Reflections.ReflectionLibrary


''' <summary>
''' Contains functions for working with queries, such as translating between styles, or assembling/dismantling queries into separate components in a dictionary list.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class QueryLibrary
    Shared Event Message(message As MessengerEventArgs)
    Shared Event Log(exception As LoggerEventArgs)

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Assembles a query string based on the key-value components of an index list. 
    ''' </summary>
    ''' <param name="p_query">Dictionary list of key (header) and value (record value) components.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AssembleQuery(ByVal p_query As Dictionary(Of String, String)) As String
        If p_query Is Nothing Then Return ""

        Dim tempString As String = ""
        Dim i = 0

        For Each entry As KeyValuePair(Of String, String) In p_query
            If Not i = 0 Then tempString &= " AND "
            tempString &= entry.Key & " = '" & entry.Value & "'"
            i += 1
        Next

        Return tempString
    End Function

    ''' <summary>
    ''' Takes a query and replaces all 'LIKE' with '='.
    ''' </summary>
    ''' <param name="p_query">Query to perform the replace operation on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TranslateQuery(ByVal p_query As String) As String
        If String.IsNullOrEmpty(p_query) Then Return ""

        If p_query.Length > 0 Then
            While StringExistInName(p_query, "LIKE")
                p_query = ReplaceStringInName(p_query, "LIKE", "= '")
            End While
        End If

        Return p_query
    End Function

    ''' <summary>
    ''' Returns the value for the specified header, if the table only has a single row.
    ''' If the table has more than a single row, or the function throws an exception, then "Invalid Table" is returned.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table to query.</param>
    ''' <param name="p_headerName">Name of the header corresponding with the desired value.</param>
    ''' <param name="p_dataSource">Path to the table file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TableSingleRowQuery(ByVal p_tableName As String,
                                               ByVal p_headerName As String,
                                               ByVal p_dataSource As String) As String

        If String.IsNullOrEmpty(p_dataSource) Then Return ""

        Try
            If Not File.Exists(p_dataSource) Then
                RaiseEvent Message(New MessengerEventArgs("Datasource does not exist {0}", p_dataSource))
                Return ""
            End If
            Dim dtController As New cDataTableController
            Dim tempList As List(Of String) = dtController.GetTableValueFromFile(p_dataSource, p_tableName, p_headerName)

            If tempList Is Nothing Then
                RaiseEvent Message(New MessengerEventArgs("No data table can be obtained from:  {0}File: {1} {0}Table Name: {2} {0}Header Name:{3}",
                                                           Environment.NewLine, p_dataSource, p_tableName, p_headerName))
                Return ""
            End If

            Select Case tempList.Count
                Case 0
                    RaiseEvent Message(New MessengerEventArgs("Warning! No table value was selected!  {0}File: {1} {0}Table Name: {2} {0}Header Name:{3}",
                                                               Environment.NewLine, p_dataSource, p_tableName, p_headerName))
                    Return ""
                Case 1
                    Return tempList(0)
                Case Else
                    RaiseEvent Message(New MessengerEventArgs("Warning! More than one table value was selected! Only the first value is returned.  {0}File: {1} {0}Table Name: {2} {0}Header Name:{3}",
                                                               Environment.NewLine, p_dataSource, p_tableName, p_headerName))
                    Return tempList(0)
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                            NameOfParam(Function() p_tableName), p_tableName,
                            NameOfParam(Function() p_headerName), p_headerName,
                            NameOfParam(Function() p_dataSource), p_dataSource))
        End Try

        Return ""
    End Function

    ''' <summary>
    ''' Creates a list of queries, written in a syntax understood by the tableset class, for filtering rows.
    ''' </summary>
    ''' <param name="p_list">List of queries.</param>
    ''' <remarks></remarks>
    Public Shared Function CreateQueryLists(ByVal p_list As List(Of String)) As List(Of String)
        Dim tempList As New List(Of String)

        For Each query As String In p_list
            tempList.Add(AssembleQuery(ParseQueryToDictionary(query)))
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Creates a list of dictionary lists of 'Header'-'Value' pairings for each query.
    ''' </summary>
    ''' <param name="p_list">List of queries.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateDictionaryLists(ByVal p_list As List(Of String)) As List(Of Dictionary(Of String, String))
        Dim dictionaryLists As New List(Of Dictionary(Of String, String))
        For Each query As String In p_list
            dictionaryLists.Add(ParseQueryToDictionary(query))
        Next

        Return dictionaryLists
    End Function

    ''' <summary>
    ''' Parses Query into a dictionary list of header-value pairs, with a new entry for each 'AND'.
    ''' </summary>
    ''' <param name="p_query">String containting the Query, with only the 'LIKE' 'AND' components.</param>
    ''' <remarks></remarks>
    Public Shared Function ParseQueryToDictionary(ByVal p_query As String) As Dictionary(Of String, String)
        Dim query As New Dictionary(Of String, String)
        Dim keyStart As Integer = 1
        Dim keyEnd As Integer
        Dim valStart As Integer
        Dim valEnd As Integer
        Try
            If p_query.Count > 0 Then
                For i = 1 To Len(p_query)

                    If StringsMatch(Mid(p_query, i, 4), "LIKE") Then
                        keyEnd = i - 2
                        valStart = i + 5
                    ElseIf StringsMatch(Mid(p_query, i, 3), "AND") Then
                        valEnd = i - 2
                        query.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
                        keyStart = i + 4
                    ElseIf i = Len(p_query) Then
                        valEnd = i
                        query.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
                    End If
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_query), p_query))
        End Try

        Return query
    End Function
#End Region
End Class
