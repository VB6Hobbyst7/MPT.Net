Option Strict On
Option Explicit On

''' <summary>
''' Basic generic symbolic element that may compose a set of units, a symbolic equation, etc.
''' Such elements are assumed to be defined by either opening/closing parentheses, and/or multipliers, and/or divisors.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicBlock
#Region "Properties: Friend"
    ''' <summary>
    ''' String composing the base type of the block object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property blockName As String
    ''' <summary>
    ''' String composing the superscript associated with the block object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property blockSuperscript As String
    ''' <summary>
    ''' If true, then the block object is to be considered in the numerator position. 
    ''' If false, then the block object is to be considered in the denominator position. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property isNumerator As Boolean
#End Region
End Class
