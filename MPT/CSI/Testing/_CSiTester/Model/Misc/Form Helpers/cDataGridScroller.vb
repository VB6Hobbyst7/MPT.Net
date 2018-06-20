Option Explicit On
Option Strict On

Imports MPT.Forms.DataGridLibrary

''' <summary>
''' Handles behaviors of the data grid vertical scroller.
''' </summary>
''' <remarks></remarks>
Public Class cDataGridVerticalScroller
#Region "Fields"
    ''' <summary>
    ''' Vertical position of the scroller.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentOffset As Double

    ''' <summary>
    ''' Previous vertical position of the scroller.
    ''' </summary>
    ''' <remarks></remarks>
    Private _priorOffset As Double

    ''' <summary>
    ''' Change in the position of the scroller.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentDelta As Double

    ''' <summary>
    ''' Previous change in the position of the scroller.
    ''' </summary>
    ''' <remarks></remarks>
    Private _priorDelta As Double

    ''' <summary>
    ''' True: Scroller event might be tripped due to a selection change instead of scrolling.
    ''' This captures the difference between automatic resetting of the scroller due to a selection vs. scrolling to the top.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property selectionMade As Boolean

    ''' <summary>
    ''' Datagrid object that the class queries and manipulates.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataGrid As DataGrid
#End Region

#Region "Initialization"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object that the class queries and manipulates.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef p_dataGrid As DataGrid)
        _dataGrid = p_dataGrid
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Returns the current vertical offset position of the datagrid.
    ''' </summary>
    ''' <param name="p_priorOffset">Prior vertical offset position of the scroll viewer.
    ''' This is returned if the function fails.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentVerticalOffset(ByVal p_dataGrid As DataGrid,
                                                 ByVal p_priorOffset As Double) As Double
        Dim myScrollViewer As ScrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)

        If (myScrollViewer IsNot Nothing AndAlso
            Not (myScrollViewer.VerticalOffset = 0 AndAlso
                 p_priorOffset > 1)) Then
            Return myScrollViewer.VerticalOffset
        Else
            Return p_priorOffset
        End If
    End Function

    ''' <summary>
    ''' Resets the datagrid scroll viewer to a specified prior vertical offset.
    ''' </summary>
    ''' <param name="p_priorOffset">Prior vertical offset position of the scroll viewer. 
    ''' This is what the scrollviewer is reset to.</param>
    ''' <remarks></remarks>
    Public Shared Sub ResetVerticalOffset(ByVal p_dataGrid As DataGrid,
                                          ByVal p_priorOffset As Double)
        Dim myScrollViewer As ScrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)

        If (myScrollViewer IsNot Nothing) Then
            myScrollViewer.ScrollToVerticalOffset(p_priorOffset)
        End If
    End Sub

    ''' <summary>
    ''' Sets the scroll positions for tracking.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetScrolls()
        _priorOffset = _currentOffset
        _currentOffset = CurrentVerticalOffset(_dataGrid, _currentOffset)

        _priorDelta = _currentDelta
        _currentDelta = _currentOffset - _priorOffset

        If ScrolledToTop() Then
            _currentOffset = 0
        Else
            _selectionMade = False
        End If
    End Sub

    ''' <summary>
    ''' Updates the scroller vertical offset, to counteract any automatic setting.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateVerticalOffset()
        If (Not Math.Floor(_currentDelta) = 0 OrElse _currentOffset > 0) Then
            ResetVerticalOffset(_dataGrid, _currentOffset)
        End If
    End Sub
#End Region

#Region "Methods: Private"

    ''' <summary>
    ''' True: Scroller has scrolled to the top.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ScrolledToTop() As Boolean
        Return (_currentDelta = 0 AndAlso
                Not _priorDelta = 0 AndAlso
                Not _selectionMade)
    End Function

#End Region
End Class
