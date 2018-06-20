Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Public Class ConverterResults

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCResults) As result()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As result

        For i = 0 To p_values.Count - 1
            If p_values(i).resultType = cMCModel.eResultType.regular Then
                fileArray(i) = ConvertToFile(DirectCast(p_values(i), cMCResult))
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
    Friend Shared Function ConvertToFile(ByVal p_value As cMCResult) As result
        Dim result As result = New result()

        With result
            .id = myCDec(p_value.id)
            .name = p_value.name
            .table_name = p_value.tableName

            .lookup_fields = ConverterResultLookupFields.ConvertToFile(p_value.query)

            With .output_field
                .value = ConverterResultValue.ConvertToFile(p_value.benchmark)
                .name = p_value.benchmark.name
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
    Friend Shared Function ConvertFromFile(ByVal p_values As result(),
                                           Optional ByRef p_mcModel As cMCModel = Nothing) As cMCResults
        Dim results As New cMCResults
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return results

        If p_mcModel IsNot Nothing Then p_mcModel.ClearUpdates()

        For i = 0 To p_values.Count - 1
            Dim result As cMCResult = ConvertFromFile(p_values(i))
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
    Friend Shared Function ConvertFromFile(ByVal p_value As result) As cMCResult
        Dim result As New cMCResult()

        With p_value
            result.id = CStr(.id)
            result.name = .name
            result.tableName = .table_name

            result.query = ConverterResultLookupFields.ConvertFromFile(.lookup_fields)

            With .output_field
                result.benchmark = ConverterResultValue.ConvertFromFile(.value)
                result.benchmark.name = .name
            End With

            Dim resultForUnit As New cMCResultBasic
            resultForUnit = ConverterResultUnits.ConvertFromFile(.units, resultForUnit)
            result.units = resultForUnit.units
            result.unitsConversion = resultForUnit.unitsConversion
            result.updates = ConverterResultsUpdates.ConvertFromFile(.updates)
        End With

        Return result
    End Function
End Class
