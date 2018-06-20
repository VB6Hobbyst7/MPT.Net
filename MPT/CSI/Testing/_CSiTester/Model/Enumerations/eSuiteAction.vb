Option Explicit On
Option Strict On

Imports System.ComponentModel

''' <summary>
''' Specifies whether a suite is being created or edited.
''' </summary>
''' <remarks></remarks>
Friend Enum eSuiteAction
    <Description("None")> None = 0
    <Description("Create")> Create
    <Description("Edit")> Edit
End Enum