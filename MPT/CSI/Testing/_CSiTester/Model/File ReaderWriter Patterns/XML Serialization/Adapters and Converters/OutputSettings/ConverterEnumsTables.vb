Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary

Friend Class ConverterEnumsTables

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As IList(Of String)) As tabularOutputTableSetTable()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As tabularOutputTableSetTable

        For i = 0 To p_values.Count - 1
            fileArray(i) = ConvertToFile(p_values(i))
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As String) As tabularOutputTableSetTable
        Return ConvertStringToEnumByXMLAttribute(Of tabularOutputTableSetTable)(p_value)
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As tabularOutputTableSetTable()) As List(Of String)
        Dim tables As New List(Of String)
        If (p_value Is Nothing OrElse
            p_value.Count = 0) Then Return tables

        For i = 0 To p_value.Count - 1
            tables.Add(ConvertFromFile(p_value(i)))
        Next

        Return tables
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As tabularOutputTableSetTable) As String
        Return GetEnumXMLAttribute(p_value)
    End Function

End Class
