Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports MPT.String.ConversionLibrary

Imports MPT.Enums
Imports MPT.Enums.EnumLibrary

Public Class ConverterResultValue

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cFieldOutput) As result_value
        Dim value As New result_value

        With value
            With .benchmark
                .is_correct = ConverterIsCorrectConversionEnum(p_value.isCorrect)
                .significant_digits = p_value.roundBenchmark
                .Value = p_value.valueBenchmark
            End With

            ' Stated in Schema to be deprecated?
            With .last_best
                .significant_digits = p_value.roundLastBest
                .Value = p_value.valueLastBest
            End With

            .passing_percent_difference_range = myCDec(CStr(p_value.valuePassingPercentDifferenceRange))
            '.passing_percent_difference_rangeSpecified
            .shift_for_calculating_percent_difference = myCDec(CStr(p_value.shiftCalc))
            '.shift_for_calculating_percent_differenceSpecified

            With .theoretical
                .significant_digits = p_value.roundTheoretical
                .Value = p_value.valueTheoretical
            End With

            .zero_tolerance = myCDec(CStr(p_value.zeroTolerance))
            '.zero_toleranceSpecified
        End With

        Return value
    End Function


    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As result_value) As cFieldOutput
        Dim value As New cFieldOutput

        With p_value
            With .benchmark
                value.isCorrect = CType(ConvertYesNoUnknownEnum(.is_correct.ToString()), eYesNoUnknown)
                value.roundBenchmark = .significant_digits
                value.valueBenchmark = .Value
            End With

            ' Stated in Schema to be deprecated?
            With .last_best
                value.roundLastBest = .significant_digits
                value.valueLastBest = .Value
            End With

            value.valuePassingPercentDifferenceRange = .passing_percent_difference_range
            '.passing_percent_difference_rangeSpecified
            value.shiftCalc = .shift_for_calculating_percent_difference
            '.shift_for_calculating_percent_differenceSpecified

            With .theoretical
                value.roundTheoretical = .significant_digits
                value.valueTheoretical = .Value
            End With

            value.zeroTolerance = .zero_tolerance
            '.zero_toleranceSpecified
        End With

        Return value
    End Function


    ''' <summary>
    ''' Converts the boolean to the enumeration used for units conversion.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterIsCorrectConversionEnum(ByVal p_value As eYesNoUnknown) As String
        Return GetEnumDescription(p_value).ToLower
    End Function
End Class
