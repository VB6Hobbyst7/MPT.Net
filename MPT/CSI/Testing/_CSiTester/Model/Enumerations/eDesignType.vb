Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' List of design types that can be selected for design in CSi products.
''' </summary>
''' <remarks></remarks>
Friend Enum eDesignType
    <Description("")> myError = 0
    <Description("General")> general
    <Description("Steel Frame")> steelFrame
    <Description("Concrete Frame")> concreteFrame
    <Description("Shear Wall")> shearWall
    <Description("Composite Beam")> compositeBeam
    <Description("Composite Column")> compositeColumn
    <Description("Aluminum Frame")> aluminumFrame
    <Description("Cold-Formed Steel Frame")> coldFormedSteelFrame
End Enum