Option Strict On
Option Explicit On

''' <summary>
''' Object containing all values relevant to a single target displacement.
''' </summary>
''' <remarks></remarks>
Public Class TargetDisplacement
#Region "Properties"
    ''' <summary>
    ''' Name of the static linear load case associated with the target
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LoadCase As String

    ''' <summary>
    ''' Name of the point object associated with the target.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NamePoint As String

    ''' <summary>
    ''' Lateral displacement targets. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DisplacementTarget As Double

#End Region
End Class
