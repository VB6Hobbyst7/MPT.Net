Option Explicit On
Option Strict On

Imports CSiTester.cProgramControl

Friend Class ConverterEnumUnits

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_enum As eForceUnit) As tabularOutputTableSetOptionsUnitsForceUnit
        Select Case p_enum
            Case eForceUnit.kgf
                Return tabularOutputTableSetOptionsUnitsForceUnit.kgf
            Case eForceUnit.kip
                Return tabularOutputTableSetOptionsUnitsForceUnit.kip
            Case eForceUnit.kN
                Return tabularOutputTableSetOptionsUnitsForceUnit.kN
            Case eForceUnit.lb
                Return tabularOutputTableSetOptionsUnitsForceUnit.lb
            Case eForceUnit.N
                Return tabularOutputTableSetOptionsUnitsForceUnit.N
            Case eForceUnit.tonf
                Return tabularOutputTableSetOptionsUnitsForceUnit.tonf
            Case eForceUnit.none
                Return tabularOutputTableSetOptionsUnitsForceUnit.Item
            Case Else
                Return tabularOutputTableSetOptionsUnitsForceUnit.Item
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_enum As eLengthUnit) As tabularOutputTableSetOptionsUnitsLengthUnit
        Select Case p_enum
            Case eLengthUnit.centimeter
                Return tabularOutputTableSetOptionsUnitsLengthUnit.cm
            Case eLengthUnit.feet
                Return tabularOutputTableSetOptionsUnitsLengthUnit.ft
            Case eLengthUnit.inches
                Return tabularOutputTableSetOptionsUnitsLengthUnit.in
            Case eLengthUnit.meter
                Return tabularOutputTableSetOptionsUnitsLengthUnit.m
            Case eLengthUnit.micron
                Return tabularOutputTableSetOptionsUnitsLengthUnit.micron
            Case eLengthUnit.millimeter
                Return tabularOutputTableSetOptionsUnitsLengthUnit.mm
            Case eLengthUnit.none
                Return tabularOutputTableSetOptionsUnitsLengthUnit.Item
            Case Else
                Return tabularOutputTableSetOptionsUnitsLengthUnit.Item
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_enum As eTemperatureUnit) As tabularOutputTableSetOptionsUnitsTemperatureUnit
        Select Case p_enum
            Case eTemperatureUnit.Celcius
                Return tabularOutputTableSetOptionsUnitsTemperatureUnit.C
            Case eTemperatureUnit.Farenheit
                Return tabularOutputTableSetOptionsUnitsTemperatureUnit.F
            Case eTemperatureUnit.none
                Return tabularOutputTableSetOptionsUnitsTemperatureUnit.Item
            Case Else
                Return tabularOutputTableSetOptionsUnitsTemperatureUnit.Item
        End Select
    End Function



    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_enum As tabularOutputTableSetOptionsUnitsForceUnit) As eForceUnit
        Select Case p_enum
            Case tabularOutputTableSetOptionsUnitsForceUnit.kgf
                Return eForceUnit.kgf
            Case tabularOutputTableSetOptionsUnitsForceUnit.kip
                Return eForceUnit.kip
            Case tabularOutputTableSetOptionsUnitsForceUnit.kN
                Return eForceUnit.kN
            Case tabularOutputTableSetOptionsUnitsForceUnit.lb
                Return eForceUnit.lb
            Case tabularOutputTableSetOptionsUnitsForceUnit.N
                Return eForceUnit.N
            Case tabularOutputTableSetOptionsUnitsForceUnit.tonf
                Return eForceUnit.tonf
            Case tabularOutputTableSetOptionsUnitsForceUnit.Item
                Return eForceUnit.none
            Case Else
                Return eForceUnit.none
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_enum As tabularOutputTableSetOptionsUnitsLengthUnit) As eLengthUnit
        Select Case p_enum
            Case tabularOutputTableSetOptionsUnitsLengthUnit.cm
                Return eLengthUnit.centimeter
            Case tabularOutputTableSetOptionsUnitsLengthUnit.ft
                Return eLengthUnit.feet
            Case tabularOutputTableSetOptionsUnitsLengthUnit.in
                Return eLengthUnit.inches
            Case tabularOutputTableSetOptionsUnitsLengthUnit.m
                Return eLengthUnit.meter
            Case tabularOutputTableSetOptionsUnitsLengthUnit.micron
                Return eLengthUnit.micron
            Case tabularOutputTableSetOptionsUnitsLengthUnit.mm
                Return eLengthUnit.millimeter
            Case tabularOutputTableSetOptionsUnitsLengthUnit.Item
                Return eLengthUnit.none
            Case Else
                Return eLengthUnit.none
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_enum As tabularOutputTableSetOptionsUnitsTemperatureUnit) As eTemperatureUnit
        Select Case p_enum
            Case tabularOutputTableSetOptionsUnitsTemperatureUnit.C
                Return eTemperatureUnit.Celcius
            Case tabularOutputTableSetOptionsUnitsTemperatureUnit.F
                Return eTemperatureUnit.Farenheit
            Case tabularOutputTableSetOptionsUnitsTemperatureUnit.Item
                Return eTemperatureUnit.none
            Case Else
                Return eTemperatureUnit.none
        End Select
    End Function
End Class
