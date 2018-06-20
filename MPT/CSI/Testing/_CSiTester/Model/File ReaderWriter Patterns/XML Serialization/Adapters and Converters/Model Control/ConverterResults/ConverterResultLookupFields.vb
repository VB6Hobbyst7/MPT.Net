Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports CSiTester.ModelControl


Public Class ConverterResultLookupFields

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCQuery) As lookup_field()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As lookup_field

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
    Friend Shared Function ConvertToFile(ByVal p_value As cFieldLookup) As lookup_field
        Return New lookup_field() With {.name = p_value.name,
                                        .value = p_value.valueField
                                        }
    End Function


    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As lookup_field()) As cMCQuery
        Dim values As New cMCQuery

        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return values

        For i = 0 To p_values.Count - 1
            values.Add(ConvertFromFile(p_values(i)))
        Next

        Return values
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As lookup_field) As cFieldLookup
        Return New cFieldLookup() With {.name = p_value.name,
                                        .valueField = p_value.value
                                        }
    End Function
End Class
