Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' Code type used for design of the various frame elements.
''' </summary>
''' <remarks></remarks>
Public Enum CodeType
    <Description("None")> None
    <Description("Steel Frame")> SteelFrame
    <Description("Concrete Frame")> ConcreteFrame
    <Description("Aluminum Frame")> AluminumFrame
    <Description("Cold Formed Steel Frame")> ColdFormedSteelFrame
End Enum
