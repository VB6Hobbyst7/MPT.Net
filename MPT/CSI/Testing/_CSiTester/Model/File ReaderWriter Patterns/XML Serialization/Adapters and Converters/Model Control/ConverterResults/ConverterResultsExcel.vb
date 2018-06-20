Option Explicit On
Option Strict On

Imports MPT.String.ConversionLibrary

Imports CSiTester.ModelControl

Public Class ConverterResultsExcel

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCResults) As regtest_internal_useResult()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As regtest_internal_useResult

        For i = 0 To p_values.Count - 1
            If p_values(i).resultType = cMCModel.eResultType.excelCalculated Then
                fileArray(i) = ConvertToFile(p_values(i))
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
    Friend Shared Function ConvertToFile(ByVal p_value As cMCResultBasic) As regtest_internal_useResult
        Dim result As regtest_internal_useResult = New regtest_internal_useResult()

        With result
            .id = myCDec(p_value.id)
            .name = p_value.name
            .value = ConverterResultValue.ConvertToFile(p_value.benchmark)
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
    Friend Shared Function ConvertFromFile(ByVal p_values As regtest_internal_useResult(),
                                           Optional ByRef p_mcModel As cMCModel = Nothing) As cMCResults
        Dim results As New cMCResults
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return results

        If p_mcModel IsNot Nothing Then p_mcModel.ClearUpdates()

        For i = 0 To p_values.Count - 1
            Dim result As cMCResultBasic = ConvertFromFile(p_values(i))
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
    Friend Shared Function ConvertFromFile(ByVal p_value As regtest_internal_useResult) As cMCResultBasic
        Dim result As New cMCResultBasic()

        With p_value
            result.id = CStr(.id)
            result.name = .name
            result.benchmark = ConverterResultValue.ConvertFromFile(.value)
            result = ConverterResultUnits.ConvertFromFile(.units, result)
            result.updates = ConverterResultsUpdates.ConvertFromFile(.updates)
        End With

        Return result
    End Function
End Class
