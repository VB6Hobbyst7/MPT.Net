Option Strict On
Option Explicit On

''' <summary>
''' Object containing all values relevant to a single target period.
''' </summary>
''' <remarks></remarks>
Public Class TargetPeriod
#Region "Properties"
    ''' <summary>
    ''' Name of the modal load case for which the target applies.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModalCase As String

    ''' <summary>
    ''' Mode number associated with the target.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModeNumber As Integer

    ''' <summary>
    ''' Target period. [s]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PeriodTarget As Double


#End Region
End Class
