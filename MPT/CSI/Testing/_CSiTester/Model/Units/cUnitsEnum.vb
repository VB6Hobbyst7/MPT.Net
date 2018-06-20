Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' Contains the basic units type enum.
''' </summary>
''' <remarks></remarks>
Public Class cUnitsEnum
    ''' <summary>
    ''' The basic unit types from which all other units are derived.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnitType
        <Description("")> none
        <Description("unitless")> unitless
        <Description("length")> length
        <Description("location")> location
        <Description("mass")> mass
        <Description("rotation")> rotation
        <Description("temperature")> temperature
        <Description("time")> time
        ''' <summary>
        ''' Derived unit, but commonly used instead of breaking units down into mass*length/time^2.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("force")> force
    End Enum

End Class
