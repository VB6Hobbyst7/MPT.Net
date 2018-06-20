Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' Used for 3-type yes/no/unknown values.
''' </summary>
''' <remarks></remarks>
Public Enum eYesNoUnknown
    <Description("yes")> yes = 1
    <Description("no")> no = 2
    <Description("")> unknown = 3
End Enum