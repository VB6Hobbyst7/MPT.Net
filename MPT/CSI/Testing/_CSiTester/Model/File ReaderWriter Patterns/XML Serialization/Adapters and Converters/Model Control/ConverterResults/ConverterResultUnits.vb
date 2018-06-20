Option Explicit On
Option Strict On

Imports MPT.String
Imports MPT.String.ConversionLibrary

Imports CSiTester.ModelControl

Public Class ConverterResultUnits

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCResultBasic) As result_units
        Dim resultUnits As New result_units()

        resultUnits.units_conversion = ConverterUnitsConversionEnum(p_value.unitsConversion)
        resultUnits.Value = p_value.units

        Return resultUnits
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As result_units,
                                           ByRef p_results As cMCResultBasic) As cMCResultBasic

        p_results.unitsConversion = ConvertYesTrueBoolean(p_value.units_conversion.ToString())
        '.units_conversionSpecified
        p_results.units = p_value.Value

        Return p_results
    End Function

    ''' <summary>
    ''' Converts the boolean to the enumeration used for units conversion.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverterUnitsConversionEnum(ByVal p_value As Boolean) As yes_no
        Dim value As String = ConvertYesTrueString(p_value, p_capitalization:=eCapitalization.AllLower)

        Select Case value
            Case "yes"
                Return yes_no.yes
            Case "no"
                Return yes_no.no
            Case Else
                Return yes_no.no
        End Select
    End Function
End Class
