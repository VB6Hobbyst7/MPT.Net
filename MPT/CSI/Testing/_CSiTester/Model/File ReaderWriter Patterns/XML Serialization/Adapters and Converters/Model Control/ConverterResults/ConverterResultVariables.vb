Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports CSiTester.ModelControl

Public Class ConverterResultVariables

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As ObservableCollection(Of cMCVariable)) As variablesVariable()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As variablesVariable

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
    Friend Shared Function ConvertToFile(ByVal p_value As cMCVariable) As variablesVariable
        Dim variable As New variablesVariable

        With variable
            With .lookup_fields
                .field = ConverterResultLookupFields.ConvertToFile(p_value.query)
                .range = ConverterResultRanges.ConvertToFile(p_value.range)
                .range_all = ConverterResultRanges.ConvertToFile(p_value.rangeAll)
            End With

            .output_field.name = p_value.name
            .scale_factor = p_value.scaleFactor
        End With

        Return variable
    End Function


    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As variablesVariable()) As ObservableCollection(Of cMCVariable)
        Dim resultUpdates As New ObservableCollection(Of cMCVariable)
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return resultUpdates

        For i = 0 To p_values.Count - 1
            resultUpdates.Add(ConvertFromFile(p_values(i)))
        Next

        Return resultUpdates
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As variablesVariable) As cMCVariable
        Dim mcVariable As New cMCVariable
        With p_value
            With .lookup_fields
                mcVariable.query = ConverterResultLookupFields.ConvertFromFile(.field)
                mcVariable.range = ConverterResultRanges.ConvertFromFile(.range)
                mcVariable.rangeAll = ConverterResultRanges.ConvertFromFile(.range_all)
            End With
            mcVariable.name = .output_field.name
            mcVariable.scaleFactor = .scale_factor
        End With

        Return mcVariable
    End Function
End Class
