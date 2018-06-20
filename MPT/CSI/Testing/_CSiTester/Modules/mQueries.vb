Option Strict On
Option Explicit On

''' <summary>
''' Module containing functions for working with queries, such as translating between styles, or assembling/dismantling queries into separate components in a dictionary list.
''' </summary>
''' <remarks></remarks>
Module mQueries
    ' ''' <summary>
    ' ''' Assembles a query string based on the key-value components of an index list. 
    ' ''' </summary>
    ' ''' <param name="myQuery">Dictionary list of key (header) and value (record value) components.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function AssembleQuery(ByVal myQuery As Dictionary(Of String, String)) As String
    '    Dim tempString As String = ""
    '    Dim i = 0

    '    If Not IsNothing(myQuery) Then
    '        For Each entry In myQuery
    '            If Not i = 0 Then tempString = tempString & " AND "
    '            tempString = tempString & entry.Key & " = '" & entry.Value & "'"
    '            i += 1
    '        Next
    '    End If

    '    AssembleQuery = tempString
    'End Function

    ' ''' <summary>
    ' ''' Takes a query and replaces all 'LIKE' with '='.
    ' ''' </summary>
    ' ''' <param name="myQuery">Query to perform the replace operation on.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function TranslateQuery(ByVal myQuery As String) As String
    '    Try
    '        If myQuery.Count > 0 Then
    '            While StringExistInName(myQuery, "LIKE")
    '                myQuery = ReplaceStringInName(myQuery, "LIKE", "= '")
    '            End While
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        MsgBox(ex.StackTrace)
    '    End Try

    '    TranslateQuery = myQuery
    'End Function
End Module
