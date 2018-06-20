Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Level of the program, specified in the settings file. This affects what portions of the program are visible and accessible, as well as various defaults.
''' </summary>
''' <remarks></remarks>
Public Enum eCSiTesterLevel
    <Description("published")> Published = 1
    <Description("internal")> Internal = 2
    <Description("development")> Development = 3
End Enum