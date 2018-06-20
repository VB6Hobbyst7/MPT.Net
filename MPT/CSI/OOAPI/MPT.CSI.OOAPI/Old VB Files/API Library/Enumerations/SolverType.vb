Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' The solver type used for the analysis.
''' </summary>
''' <remarks></remarks>
Public Enum SolverType
    <Description("Standard Solver")> Standard
    <Description("Advanced Solver")> Advanced
    <Description("Multi-Threaded Solver")> MultiThreaded
End Enum
