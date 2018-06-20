Option Strict On
Option Explicit On

Public Class DesignResultFrameConcrete
    Inherits DesignResultFrame
#Region "Properties"
    ''' <summary>
    ''' Name of the design combination for which the controlling major shear occurs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VmajorCombo As String

    ''' <summary>
    ''' The required area of transverse shear reinforcing per unit length along the frame object 
    ''' for major shear at the specified location. [L^2/L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvMajor As Double
#End Region
End Class
