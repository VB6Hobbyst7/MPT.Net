Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Public Class ConverterResultRanges

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCRange) As range
        Dim range As New range()

        With range
            .name = p_value.fieldName
            .range_operation = ConverterRangeOperations.ConvertToFile(p_value.rangeOperation)
            '.range_operationSpecified
            .value_first = p_value.valueFirst
            .value_last = p_value.valueLast
        End With

        Return range
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCRangeAll) As range_all
        Dim rangeAll As New range_all()

        With rangeAll
            .name = p_value.fieldName
            .range_operation = ConverterRangeOperations.ConvertToFile(p_value.rangeOperation)
            '.range_operationSpecified
        End With

        Return rangeAll
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As range) As cMCRange

        Dim mcRange As New cMCRange
        With p_value
            mcRange.fieldName = .name
            mcRange.rangeOperation = ConverterRangeOperations.ConvertFromFile(.range_operation)
            '.range_operationSpecified
            mcRange.valueFirst = .value_first
            mcRange.valueLast = .value_last
        End With

        Return mcRange
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As range_all) As cMCRangeAll

        Dim mcRangeAll As New cMCRangeAll
        With p_value
            mcRangeAll.fieldName = .name
            mcRangeAll.rangeOperation = ConverterRangeOperations.ConvertFromFile(.range_operation)
            '.range_operationSpecified
        End With

        Return mcRangeAll
    End Function
End Class
