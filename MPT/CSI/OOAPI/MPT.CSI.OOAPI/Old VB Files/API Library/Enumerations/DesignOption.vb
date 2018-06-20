Option Explicit On
Option Strict On

''' <summary>
''' The design option for each frame object.
''' </summary>
''' <remarks></remarks>
Public Enum DesignOption
    EnumError = 0
    ''' <summary>
    ''' The capacity of the current frame design will be checked.
    ''' </summary>
    ''' <remarks></remarks>
    Check = 1
    ''' <summary>
    ''' Design values will be calculated for the current frame such that capacity is not exceeded.
    ''' </summary>
    ''' <remarks></remarks>
    Design = 2
End Enum
