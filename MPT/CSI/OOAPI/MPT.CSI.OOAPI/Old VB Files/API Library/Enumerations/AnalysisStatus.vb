Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' The analysis status of a given load case.
''' </summary>
''' <remarks></remarks>
Public Enum AnalysisStatus
    <Description("")> EnumError = 0
    <Description("Not Run")> NotRun
    <Description("Could Not Start")> CouldNotStart
    <Description("Not Finished")> NotFinished
    <Description("Finished")> Finished
End Enum
