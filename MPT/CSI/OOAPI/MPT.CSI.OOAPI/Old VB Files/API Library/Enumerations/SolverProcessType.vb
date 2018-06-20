Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' The solver process used for the analysis.
''' </summary>
''' <remarks></remarks>
Public Enum SolverProcessType
    <Description("Auto (Program Determined)")> Auto
    <Description("GUI (Same) Process")> Same
    <Description("Separate Process")> Separate
End Enum
