Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Public Class ConverterResultsPostProcessed

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCResults) As postprocessed_result()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As postprocessed_result

        For i = 0 To p_values.Count - 1
            If TypeOf p_values(i) Is cMCResultPostProcessed Then
                fileArray(i) = ConvertToFile(DirectCast(p_values(i), cMCResultPostProcessed))
            End If
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCResultPostProcessed) As postprocessed_result
        Dim result As postprocessed_result = New postprocessed_result()

        With result
            .id = myCDec(p_value.id)
            .name = p_value.name
            .table_name = p_value.tableName

            .range = ConverterResultRanges.ConvertToFile(p_value.range)
            .range_all = ConverterResultRanges.ConvertToFile(p_value.rangeAll)

            With .formula
                .operation = ConverterRangeOperations.ConvertToFile(p_value.rangeOperation)
                '.operationSpecified
                .result = ConverterResultValue.ConvertToFile(p_value.benchmark)
                .variables = ConverterResultVariables.ConvertToFile(p_value.variables)
            End With

            .units = ConverterResultUnits.ConvertToFile(p_value)
            .updates = ConverterResultsUpdates.ConvertToFile(p_value.updates)
        End With

        Return result
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <param name="p_mcModel">Model Control object to add the converted values to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As postprocessed_result(),
                                           Optional ByRef p_mcModel As cMCModel = Nothing) As cMCResults
        Dim results As New cMCResults
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return results

        If p_mcModel IsNot Nothing Then p_mcModel.ClearUpdates()

        For i = 0 To p_values.Count - 1
            Dim result As cMCResultPostProcessed = ConvertFromFile(p_values(i))
            results.Add(result)
            If p_mcModel IsNot Nothing Then p_mcModel.AddResult(result)
        Next

        Return results
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As postprocessed_result) As cMCResultPostProcessed
        Dim result As New cMCResultPostProcessed()

        With p_value
            result.id = CStr(.id)
            result.name = .name
            result.tableName = .table_name

            result.range = ConverterResultRanges.ConvertFromFile(.range)
            result.rangeAll = ConverterResultRanges.ConvertFromFile(.range_all)

            With .formula
                result.rangeOperation = ConverterRangeOperations.ConvertFromFile(.operation())
                '.operationSpecified
                result.benchmark = ConverterResultValue.ConvertFromFile(.result)
                result.variables = ConverterResultVariables.ConvertFromFile(.variables)
            End With

            result.units = ConverterResultUnits.ConvertFromFile(.units, New cMCResult).units
            result.updates = ConverterResultsUpdates.ConvertFromFile(.updates)
        End With

        Return result
    End Function
End Class
