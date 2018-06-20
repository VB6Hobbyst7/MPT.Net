Option Strict On
Option Explicit On

''' <summary>
''' Class representing the exponent power of a number or variable, including both the characteristics of the power, and operations of combination.
''' Currently this class' combination methods only work with numbers. Symbolic combinations are not supported.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicBlockPower
#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
    Private Const _DECIMAL As String = "."
    Private Const _POWER_DENOMINATOR As String = "-"
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' If true, then the powers are assumed to have a format of ^(n/m).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property fractionFormat As Boolean
    ''' <summary>
    ''' If true, then the powers are assumed to have a format of ^n.mmm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property decimalFormat As Boolean

    ''' <summary>
    ''' The numerator value that is an integer. This is not included in the decimal component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property numeratorInt As Integer
    ''' <summary>
    ''' The numerator value that is an decimal. This is not included in the integer component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property numeratorDbl As Double
    ''' <summary>
    ''' The denominator value that is an integer. This is not included in the decimal component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property denominatorInt As Integer
    ''' <summary>
    ''' The denominator value that is an decimal. This is not included in the integer component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property denominatorDbl As Double

    ''' <summary>
    ''' The string representation of the combined result of the class properties.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property powerString As String
#End Region

#Region "Initialization"
    Friend Sub New()
        SetDefaults()
    End Sub

    Friend Sub New(ByVal p_power As String)
        SetDefaults()
        ParsePowerNumeratorDenominator(p_power)
    End Sub

    Friend Sub SetDefaults()
        fractionFormat = False
        decimalFormat = False

        numeratorInt = 1
        numeratorDbl = 1
        denominatorInt = 1
        denominatorDbl = 1

        powerString = ""
    End Sub
#End Region

#Region "Method: Friend"
    ''' <summary>
    ''' Populates the class properties based on the power provided.
    ''' </summary>
    ''' <param name="p_power">The power to be used to populate the class properties.</param>
    ''' <remarks></remarks>
    Friend Sub ParsePowerNumeratorDenominator(ByVal p_power As String)
        'Note: Keep combinations as integer or double until the end, in an attempt to maintain integers

        Dim currentString As String = ""

        'Determine "/" format
        'If "/" format, isolate numerator/denominator
        For Each letter As Char In p_power
            If letter = _DECIMAL Then
                decimalFormat = True
            ElseIf (letter = _DIVISOR AndAlso Not fractionFormat) Then
                fractionFormat = True

                If decimalFormat Then
                    numeratorDbl = CDbl(currentString)
                Else
                    numeratorInt = CInt(currentString)
                End If
                currentString = ""
            ElseIf letter = _DIVISOR Then   'Multiple divisions are occurring
                If decimalFormat Then
                    denominatorDbl *= CDbl(currentString)
                Else
                    denominatorInt *= CInt(currentString)
                End If
                currentString = ""
            ElseIf letter = _MULTIPLIER Then
                If decimalFormat Then
                    numeratorDbl *= CDbl(currentString)
                Else
                    numeratorInt *= CInt(currentString)
                End If
                currentString = ""
            Else
                currentString &= letter
            End If
        Next
    End Sub

    ''' <summary>
    ''' Updates the power string property based on the current numerical properties. 
    ''' Returns the result that has been set in the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdatePowerString() As String
        Dim currentString As String = ""

        If decimalFormat Then
            numeratorDbl *= numeratorInt
            currentString = CStr(numeratorDbl / denominatorDbl)
        ElseIf fractionFormat Then
            currentString = numeratorInt & _DIVISOR & denominatorInt
        Else
            currentString = CStr(numeratorInt)
        End If

        powerString = currentString

        Return currentString
    End Function

    ''' <summary>
    ''' Combines the power numerator &amp; denominator components as if the bases were being multiplied.
    ''' </summary>
    ''' <param name="p_power1">First power to multiply.</param>
    ''' <param name="p_power2">Second power to multiply.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CombinePowersBaseMultiply(ByVal p_power1 As cSymbolicBlockPower,
                                              Optional ByVal p_power2 As cSymbolicBlockPower = Nothing) As cSymbolicBlockPower
        If p_power2 Is Nothing Then p_power2 = Me

        Dim resultPower As New cSymbolicBlockPower

        With resultPower
            .numeratorInt = p_power1.numeratorInt + p_power2.numeratorInt
            .numeratorDbl = p_power1.numeratorDbl + p_power2.numeratorDbl

            .denominatorInt = p_power1.denominatorInt + p_power2.denominatorInt
            .denominatorDbl = p_power1.denominatorDbl + p_power2.denominatorDbl

            If (p_power1.fractionFormat OrElse p_power2.fractionFormat) Then .fractionFormat = True
            If (p_power1.decimalFormat OrElse p_power2.decimalFormat) Then .decimalFormat = True

            .powerString = .UpdatePowerString()
        End With

        Return resultPower
    End Function

    ''' <summary>
    ''' Combines the power numerator &amp; denominator components as if the bases were being divided.
    ''' </summary>
    ''' <param name="p_powerDenominator">Power that is part of a base in the denominator position.</param>
    ''' <param name="p_powerNumerator">Power that is part of a base in the numerator position.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CombinePowersBaseDivide(ByVal p_powerDenominator As cSymbolicBlockPower,
                                            Optional ByVal p_powerNumerator As cSymbolicBlockPower = Nothing) As cSymbolicBlockPower
        If p_powerNumerator Is Nothing Then p_powerNumerator = Me

        Dim resultPower As New cSymbolicBlockPower

        With resultPower
            .numeratorInt = p_powerNumerator.numeratorInt - p_powerDenominator.numeratorInt
            .numeratorDbl = p_powerNumerator.numeratorDbl - p_powerDenominator.numeratorDbl

            .denominatorInt = p_powerNumerator.denominatorInt - p_powerDenominator.denominatorInt
            .denominatorDbl = p_powerNumerator.denominatorDbl - p_powerDenominator.denominatorDbl

            If (p_powerNumerator.fractionFormat OrElse p_powerDenominator.fractionFormat) Then .fractionFormat = True
            If (p_powerNumerator.decimalFormat OrElse p_powerDenominator.decimalFormat) Then .decimalFormat = True

            .powerString = .UpdatePowerString()
        End With

        Return resultPower
    End Function

    ''' <summary>
    ''' Checks the power property of the block and returns true if it is negative, indicating a denominator position.
    ''' Else, returns False.
    ''' </summary>
    ''' <param name="p_blockPower">The power string to be checked for the status. 
    ''' If not specified, then the value of the class is used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPowerDenominator(Optional ByVal p_blockPower As String = "") As Boolean
        If String.IsNullOrEmpty(p_blockPower) Then p_blockPower = powerString

        If Left(p_blockPower, 1) = _POWER_DENOMINATOR Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

End Class
