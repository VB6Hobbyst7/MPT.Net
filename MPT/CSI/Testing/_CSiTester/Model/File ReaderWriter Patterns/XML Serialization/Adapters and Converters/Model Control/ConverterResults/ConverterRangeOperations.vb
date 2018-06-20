Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports CSiTester.cMCResultPostProcessed

Public Class ConverterRangeOperations
    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As eCalcOperations) As range_operation
        Select Case p_value
            Case eCalcOperations.avg
                Return range_operation.avg
            Case eCalcOperations.avgabs
                Return range_operation.avgabs
            Case eCalcOperations.max
                Return range_operation.max
            Case eCalcOperations.maxabs
                Return range_operation.maxabs
            Case eCalcOperations.min
                Return range_operation.min
            Case eCalcOperations.min
                Return range_operation.min
            Case eCalcOperations.minabs
                Return range_operation.minabs
            Case eCalcOperations.srss
                Return range_operation.srss
            Case eCalcOperations.sum
                Return range_operation.sum
            Case eCalcOperations.sumabs
                Return range_operation.sumabs
            Case Else
                Return range_operation.avg
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As range_operation) As eCalcOperations
        Select Case p_value
            Case range_operation.avg
                Return eCalcOperations.avg
            Case range_operation.avgabs
                Return eCalcOperations.avgabs
            Case range_operation.max
                Return eCalcOperations.max
            Case range_operation.maxabs
                Return eCalcOperations.maxabs
            Case range_operation.min
                Return eCalcOperations.min
            Case range_operation.min
                Return eCalcOperations.min
            Case range_operation.minabs
                Return eCalcOperations.minabs
            Case range_operation.srss
                Return eCalcOperations.srss
            Case range_operation.sum
                Return eCalcOperations.sum
            Case range_operation.sumabs
                Return eCalcOperations.sumabs
            Case Else
                Return eCalcOperations.avg
        End Select
    End Function
End Class
