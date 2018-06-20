Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' List of valid CSiPrograms that can be selected for analysis.
''' </summary>
''' <remarks></remarks>
Public Enum eCSiProgram
    <Description("")> None
    <Description("SAP2000")> SAP2000
    <Description("CSiBridge")> CSiBridge
    <Description("ETABS")> ETABS
    <Description("SAFE")> SAFE
    <Description("Perform 3D")> Perform3D
End Enum