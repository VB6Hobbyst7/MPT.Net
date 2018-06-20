Option Strict On
Option Explicit On

Public Class DesignResultConcreteJoint
    Inherits DesignResult
#Region "Properties"
    ''' <summary>
    ''' Name of the design combination for which the controlling joint shear ratio associated with the column major axis occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying special, 
    ''' code-specific multipliers to all or part of the specified design load combination, 
    ''' or that the design was based on the capacity of other objects (or other design locations for 
    ''' the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControllingJointShearRatioMajorCombo As String

    ''' <summary>
    ''' Joint Shear ratio associated with the column major axis. 
    ''' This is the joint shear divided by the joint shear capacity.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JointShearRatioMajor As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling joint shear ratio associated with the column minor axis occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying special, 
    ''' code-specific multipliers to all or part of the specified design load combination, 
    ''' or that the design was based on the capacity of other objects (or other design locations for 
    ''' the same object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControllingJointShearRatioMinorCombo As String

    ''' <summary>
    ''' Joint Shear ratio associated with the column minor axis. 
    ''' This is the joint shear divided by the joint shear capacity.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JointShearRatioMinor As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling beam/column capacity ratio associated 
    ''' with the column major axis occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying 
    ''' special, code-specific multipliers to all or part of the specified design load combination, or 
    ''' that the design was based on the capacity of other objects (or other design locations for the same 
    ''' object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControllingBeamColumnCapacityRatioMajorCombo As String

    ''' <summary>
    ''' Beam/column capacity ratio associated with the column major axis. 
    ''' This is the sum of the column capacities divided by the sum of the beam capacities at the 
    ''' top of the specified column.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BeamColumnCapacityRatioMajor As Double

    ''' <summary>
    ''' Name of the design combination for which the controlling beam/column capacity ratio associated 
    ''' with the column minor axis occurs. 
    ''' A combination name followed by (Sp) indicates that the design loads were obtained by applying 
    ''' special, code-specific multipliers to all or part of the specified design load combination, or 
    ''' that the design was based on the capacity of other objects (or other design locations for the same 
    ''' object).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ControllingBeamColumnCapacityRatioMinorCombo As String

    ''' <summary>
    ''' Beam/column capacity ratio associated with the column minor axis. 
    ''' This is the sum of the column capacities divided by the sum of the beam capacities at the 
    ''' top of the specified column.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BeamColumnCapacityRatioMinor As Double
#End Region
End Class
