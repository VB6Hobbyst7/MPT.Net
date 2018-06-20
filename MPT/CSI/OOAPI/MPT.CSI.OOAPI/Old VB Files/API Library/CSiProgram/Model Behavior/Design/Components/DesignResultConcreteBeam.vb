Option Strict On
Option Explicit On

Public Class DesignResultConcreteBeam
    Inherits DesignResultFrameConcrete

#Region "Properties"
    ''' <summary>
    ''' Name of the design combination for which the controlling top longitudinal rebar area for 
    ''' flexure occurs. A combination name followed by (Sp) indicates that the design loads were 
    ''' obtained by applying special, code-specific multipliers to all or part of the specified design 
    ''' load combination, or that the design was based on the capacity of other objects 
    ''' (or other design locations for the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TopLongCombo As String

    ''' <summary>
    ''' Total top longitudinal rebar area required for the flexure at the specified location. 
    ''' It does not include the area of steel required for torsion. [L^2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TopLongArea As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling bottom longitudinal rebar area for 
    ''' flexure occurs. A combination name followed by (Sp) indicates that the design loads were 
    ''' obtained by applying special, code-specific multipliers to all or part of the specified design 
    ''' load combination, or that the design was based on the capacity of other objects 
    ''' (or other design locations for the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BottomLongCombo As String

    ''' <summary>
    ''' Total bottom longitudinal rebar area required for the flexure at the specified location. 
    ''' It does not include the area of steel required for torsion. [L^2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BottomLongArea As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling longitudinal rebar area for torsion occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying special, 
    ''' code-specific, multipliers to all or part of the specified design load combination, or that the design 
    ''' was based on the capacity of other objects (or other design locations for the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TorsionLongCombo As String

    ''' <summary>
    ''' The total longitudinal rebar area required for torsion. [L^2]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TorsionLongArea As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling transverse reinforcing for torsion occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying special, 
    ''' code-specific, multipliers to all or part of the specified design load combination, or that the design 
    ''' was based on the capacity of other objects (or other design locations for the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TorsionTransverseCombo As String

    ''' <summary>
    ''' Required area of transverse torsional shear reinforcing per unit length along the frame object for 
    ''' torsion at the specified location. [L^2/L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TorsionTransverseArea As Double
#End Region
End Class
