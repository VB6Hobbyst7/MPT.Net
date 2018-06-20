Option Strict On
Option Explicit On

''' <summary>
''' Relevant design results for a given frame object at a given location within the frame or for a given governing load combination.
''' </summary>
''' <remarks></remarks>
Public Class DesignResult
#Region "Properties"

    ''' <summary>
    ''' Frame object name for which results are obtained.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FrameName As String

    ''' <summary>
    ''' The design error messages for the frame object, if any.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ErrorSummary As String

    ''' <summary>
    ''' The design warning messages for the frame object, if any.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarningSummary As String
#End Region


End Class
