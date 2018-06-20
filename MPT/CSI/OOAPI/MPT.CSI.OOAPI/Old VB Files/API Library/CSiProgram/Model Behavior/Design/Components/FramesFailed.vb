Option Explicit On
Option Strict On

''' <summary>
''' Contains relevant information about frame objects that have not passed design.
''' </summary>
''' <remarks></remarks>
Public Class FramesNotPassed
#Region "Properties"
    ''' <summary>
    ''' Number of frame objects that did not pass the design check or have not yet been checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberNotPassed As Integer

    ''' <summary>
    ''' Number of frame objects that did not pass the design check.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberFailed As Integer

    ''' <summary>
    ''' Number of frame objects that have not yet been checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberNotChecked As Integer

    ''' <summary>
    ''' Name of each frame object that did not pass the design check or has not yet been checked.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NamesNotPassed As New List(Of String)
#End Region

#Region "Initialization"
    Public Sub New(ByRef numberNotPassed As Integer,
                    ByRef numberFailed As Integer,
                    ByRef numberNotChecked As Integer,
                    ByRef namesNotPassed As List(Of String))
        _NumberNotPassed = numberNotPassed
        _NumberFailed = numberFailed
        _NumberNotChecked = numberNotChecked
        _NamesNotPassed = namesNotPassed
    End Sub
#End Region
End Class
