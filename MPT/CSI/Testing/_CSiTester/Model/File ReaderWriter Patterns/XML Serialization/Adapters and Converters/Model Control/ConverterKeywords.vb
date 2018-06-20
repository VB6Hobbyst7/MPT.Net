Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Friend Class ConverterKeywords
    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cKeywords) As String()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing

        Dim fileArray(p_values.Count - 1) As String
        Dim keywords As List(Of String) = p_values.NamesToList()
        For i = 0 To keywords.Count - 1
            fileArray(i) = keywords(i)
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As String()) As cKeywords
        Dim obsCol As New cKeywords
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return obsCol

        For i = 0 To p_values.Count - 1
            obsCol.Add(New cKeyword(p_values(i)))
        Next

        Return obsCol
    End Function
End Class
