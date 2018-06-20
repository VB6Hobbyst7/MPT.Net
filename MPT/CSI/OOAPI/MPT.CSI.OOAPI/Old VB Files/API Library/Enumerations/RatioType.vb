Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' Indicates the controlling stress or capacity ratio type for a given frame object.
''' </summary>
''' <remarks></remarks>
Public Enum RatioType
    <Description("")> EnumError = 0
    <Description("PMM")> PMM = 1
    <Description("Major Shear")> MajorShear = 3
    <Description("Minor Shear")> MinorShear = 4
End Enum
