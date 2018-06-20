Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Enumerations for common prompt actions. To be used for custom forms in the program.
''' </summary>
''' <remarks></remarks>
Public Enum eMessageActions
    None = 0
    <Description("Yes")> Yes
    <Description("No")> No
    <Description("OK")> OK
    <Description("Cancel")> Cancel
    <Description("Abort")> Abort
    <Description("Retry")> Retry
    <Description("Ignore")> Ignore
End Enum