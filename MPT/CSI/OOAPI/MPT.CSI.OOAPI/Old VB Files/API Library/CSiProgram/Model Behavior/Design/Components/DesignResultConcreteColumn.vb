Option Strict On
Option Explicit On

Public Class DesignResultConcreteColumn
    Inherits DesignResultFrameConcrete

#Region "Properties"
    ''' <summary>
    ''' The design option for the frame object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DesignOption As DesignOption

    ''' <summary>
    ''' Name of the design combination for which the controlling minor shear occurs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VminorCombo As String

    ''' <summary>
    ''' The required area of transverse shear reinforcing per unit length along the frame object 
    ''' for minor shear at the specified location. [L^2/L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AvMinor As Double

    ''' <summary>
    ''' name of the design combination for which the controlling PMM ratio or rebar area occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by 
    ''' applying special, code-specific multipliers to all or part of the specified design 
    ''' load combination, or that the design was based on the capacity of other objects 
    ''' (or other design locations for the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PMMCombo As String

    ''' <summary>
    ''' Total longitudinal rebar area required for the axial force plus biaxial moment (PMM) 
    ''' design at the specified location. [L^2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PMMArea As Double

    ''' <summary>
    ''' Axial force plus biaxial moment (PMM) stress ratio at the specified location.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PMMRatio As Double
#End Region
End Class
