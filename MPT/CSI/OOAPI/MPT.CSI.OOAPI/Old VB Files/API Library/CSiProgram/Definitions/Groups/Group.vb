Option Strict On
Option Explicit On

''' <summary>
''' Represents a group in the application.
''' </summary>
''' <remarks></remarks>
Public Class Group
#Region "Properties"
    ''' <summary>
    ''' Assignments to a group, such as the name, and contained objects.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Assignment As New GroupAssignments

    ''' <summary>
    ''' Properties of the group, such as color and specified uses.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Properties As New GroupProperties
#End Region
End Class
