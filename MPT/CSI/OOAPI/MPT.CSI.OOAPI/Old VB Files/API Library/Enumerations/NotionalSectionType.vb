Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Enum NotionalSectionType
    ''' <summary>
    ''' Notional size will not be considered. 
    ''' In other words, the time-dependent effect of this section will not be considered.
    ''' </summary>
    ''' <remarks></remarks>
    None
    ''' <summary>
    ''' Program will determine the notional size based on the average thickness of an area element.
    ''' </summary>
    ''' <remarks></remarks>
    Auto
    ''' <summary>
    ''' The notional size is based on the user-defined value.
    ''' </summary>
    ''' <remarks></remarks>
    User
End Enum
