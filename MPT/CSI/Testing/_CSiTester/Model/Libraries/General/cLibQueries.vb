Option Strict On
Option Explicit On

Imports CSiTester.cLibPath
Imports CSiTester.cLibFolders

''' <summary>
''' Contains functions for working with queries, such as translating between styles, or assembling/dismantling queries into separate components in a dictionary list.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibQueries

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Assembles a query string based on the key-value components of an index list. 
    ''' </summary>
    ''' <param name="myQuery">Dictionary list of key (header) and value (record value) components.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AssembleQuery(ByVal myQuery As Dictionary(Of String, String)) As String
        Dim tempString As String = ""
        Dim i = 0

        If myQuery IsNot Nothing Then
            For Each entry In myQuery
                If Not i = 0 Then tempString = tempString & " AND "
                tempString = tempString & entry.Key & " = '" & entry.Value & "'"
                i += 1
            Next
        End If

        AssembleQuery = tempString
    End Function

    ''' <summary>
    ''' Takes a query and replaces all 'LIKE' with '='.
    ''' </summary>
    ''' <param name="myQuery">Query to perform the replace operation on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TranslateQuery(ByVal myQuery As String) As String
        Try
            If myQuery.Count > 0 Then
                While StringExistInName(myQuery, "LIKE")
                    myQuery = ReplaceStringInName(myQuery, "LIKE", "= '")
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

        TranslateQuery = myQuery
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
            If Not (p_dataSource = "") And FileExists(p_dataSource) Then
                Dim dtController As New cDataTableController
                Dim tempList As List(Of String) = dtController.GetTableValueFromFile(p_dataSource, p_tableName, p_headerName)

                If tempList IsNot Nothing Then
                    If tempList.Count = 1 Then
                        Return tempList(0)
                    Else
                        Throw New ArgumentException("Warning! No table value was selected!")
                    End If
                Else
                    Return "Invalid Table"
                End If
            End If
        Catch exArg As ArgumentException
            csiLogger.ArgumentExceptionAction(exArg)
        Catch ex As Exception
            Return "Invalid Table"
        End Try

        Return ""
    End Function
#End Region
End Class
