Option Explicit On
Option Strict On

Imports System.Windows.Threading

Imports MPT.FileSystem.PathLibrary
Imports MPT.Forms.DataGridLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting

''' <summary>
''' Handles the positioning and sizing of the units buttons that are aligned atop the data grid.
''' </summary>
''' <remarks></remarks>
Public Class cDataGridHeaderButtonsSizer
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Fields"
    ''' <summary>
    ''' The last position that the horizontal scrollbar was in. Used for tracking changes in the scrollbar position.
    ''' </summary>
    ''' <remarks></remarks>
    Private _scrollPositionLast As Double

    ''' <summary>
    ''' The amount that the scrollbar was last moved to achieve the last position.
    ''' Scrolling left produces a negative number.
    ''' </summary>
    ''' <remarks></remarks>
    Private _scrollDelta As Double

    ''' <summary>
    ''' The largest index in the collection of buttons or grids.
    ''' </summary>
    ''' <remarks></remarks>
    Private _maxButtonIndex As Integer

    ''' <summary>
    ''' List of column widths, in order of the columns. 
    ''' Mostly used to determine if column widths have changed, in which case the list is updated.
    ''' These widths are irrespective of the column being in view.
    ''' </summary>
    ''' <remarks></remarks>
    Private _columnWidthsLast As New List(Of Double)

    ''' <summary>
    ''' List of column widths to be displayed, where "0" indicates a collapsed or hidden column.
    ''' </summary>
    ''' <remarks></remarks>
    Private _columnWidthsDisplay As New List(Of Double)

    ''' <summary>
    ''' Width of the dataGrid area that the grid can lie in. 
    ''' This subtracts the following:
    '''     1. Borders (left &amp; right)
    '''     2. Margins (left &amp; right)
    '''     3. Row Column Header Width
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataGridViewWidth As Double

    ''' <summary>
    ''' Programmatically created buttons for setting the column units.
    ''' </summary>
    ''' <remarks></remarks>
    Private _buttons() As Button


    Private _grid As Grid

    Private _dataGrid As DataGrid

    Private _tableView As cTableQueryView
#End Region

#Region "Initialization"
    Public Sub New()

    End Sub

    Public Sub New(ByRef p_dataGrid As DataGrid,
                   ByRef p_grid As Grid,
                   ByRef p_buttons As Button(),
                   ByRef p_tableView As cTableQueryView,
                   ByRef p_scrollPositionCurrent As Double)
        ReInitialize(p_dataGrid,
                     p_grid,
                     p_buttons,
                     p_tableView,
                     p_scrollPositionCurrent)
    End Sub

    Public Sub ReInitialize(ByRef p_dataGrid As DataGrid,
                               ByRef p_grid As Grid,
                               ByRef p_buttons As Button(),
                               ByRef p_tableView As cTableQueryView,
                               ByRef p_scrollPositionCurrent As Double)
        _dataGrid = p_dataGrid
        _grid = p_grid
        _buttons = p_buttons
        _scrollPositionLast = p_scrollPositionCurrent
        _tableView = p_tableView

        If Not PropertiesSet() Then Exit Sub

        _maxButtonIndex = _grid.ColumnDefinitions.Count - 1

        UpdateLayout()
    End Sub
#End Region


#Region "Methods: Public"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateLayout()
        Try
            If Not PropertiesSet() Then Exit Sub

            SetLeftJustifyColumnHeaderPosition()
            UpdateScroll()
            UpdateDataGridVisibleWidth()

            _columnWidthsLast = GetUnitsColumnsWidths()
            If _columnWidthsLast.Count = 0 Then Exit Sub
            _maxButtonIndex = Math.Min(_maxButtonIndex, _columnWidthsLast.Count - 1)
            _columnWidthsDisplay = GetColumnVisibleWidths()

            SizeGridAndButtons(_columnWidthsDisplay, p_fromTheRight:=ScrolledRight)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets left margin of the units buttons container to be flush with the left edge of the left-most data grid column.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLeftJustifyColumnHeaderPosition()
        Dim leftJustify As Double = GetLeftJustifyColumnHeaderPosition(_dataGrid)

        _grid.Margin = New System.Windows.Thickness(leftJustify, 0, 0, 0)
    End Sub

    Private Sub UpdateDataGridVisibleWidth()
        _dataGridViewWidth = _dataGrid.ActualWidth - DGRowHeadersWidth(_dataGrid)
    End Sub

    ''' <summary>
    ''' Returns a list of the column widths for each of the units columns.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitsColumnsWidths() As List(Of Double)
        Dim columnWidths As New List(Of Double)
        If _tableView Is Nothing Then Return columnWidths

        Dim counter As Integer = 0

        For Each header As String In _tableView.columnUnits.columnHeaders
            For Each column As DataGridColumn In _dataGrid.Columns
                If StringsMatch(column.Header.ToString, header) Then
                    columnWidths.Add(_dataGrid.Columns(counter).ActualWidth)
                    Exit For
                End If
            Next
            counter += 1
        Next

        Return columnWidths
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetColumnVisibleWidths() As List(Of Double)
        Dim columnWidthsDisplay As New List(Of Double)
        Dim buttonLeft As Double = 0
        Dim buttonRight As Double = 0
        Dim buttonWidthCurrent As Double = 0
        Dim boundaryLeft As Double = _scrollPositionLast
        Dim boundaryRight As Double = _scrollPositionLast + _dataGridViewWidth

        For i = 0 To _maxButtonIndex
            buttonRight = buttonLeft + _columnWidthsLast(i)

            ' Hide buttons on the left
            If (buttonRight <= boundaryLeft) Then
                columnWidthsDisplay.Add(0)
            End If

            ' Set left button partial width
            If (buttonLeft < boundaryLeft AndAlso
                buttonRight > boundaryLeft) Then
                columnWidthsDisplay.Add(Math.Round(buttonRight - boundaryLeft, 4))
            End If

            ' Use width as specified
            If (buttonLeft >= boundaryLeft AndAlso
                buttonRight <= boundaryRight) Then
                columnWidthsDisplay.Add(_columnWidthsLast(i))
            End If

            ' Set right button partial width
            If (buttonLeft < boundaryRight AndAlso
                buttonRight > boundaryRight) Then
                columnWidthsDisplay.Add(Math.Round(boundaryRight - buttonLeft, 4))
            End If

            ' Hide buttons on the right
            If (buttonLeft >= boundaryRight) Then
                columnWidthsDisplay.Add(0)
            End If

            buttonLeft += _columnWidthsLast(i)
        Next
        Return columnWidthsDisplay
    End Function


    ' Scrolling
    ''' <summary>
    ''' Updates the properties related to a scrolling event.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateScroll()
        Dim scrollPositionCurrent As Double = CurrentScrollPosition()
        _scrollDelta = scrollPositionCurrent - _scrollPositionLast
        _scrollPositionLast = scrollPositionCurrent
    End Sub

    ''' <summary>
    ''' Returns the new offset value for the current scroll position in the horizontal scrolling.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CurrentScrollPosition() As Double
        Dim scrollViewer As ScrollViewer
        scrollViewer = FindVisualChild(Of ScrollViewer)(_dataGrid)

        If scrollViewer Is Nothing Then
            Return 0
        Else
            Return scrollViewer.HorizontalOffset
        End If
    End Function

    ' Sizing
    ''' <summary>
    ''' Hides all grid columns and buttons, starting with the right-most ones.
    ''' </summary>
    ''' <param name="p_fromTheRight">True: The sizing will begin from the right-most element.</param>
    ''' <remarks></remarks>
    Private Sub CollapseGridAndButtons(Optional ByVal p_fromTheRight As Boolean = True)
        SizeGridAndButtons(0, p_fromTheRight)
    End Sub

    ''' <summary>
    ''' Updates the size of the grids and buttons concurrently.
    ''' </summary>
    ''' <param name="p_width">Uniform width to size all elements to.</param>
    ''' <param name="p_fromTheRight">True: The sizing will begin from the right-most element.</param>
    ''' <remarks></remarks>
    Private Sub SizeGridAndButtons(ByVal p_width As Double,
                                   Optional ByVal p_fromTheRight As Boolean = True)
        Dim startIndex As Integer = 0
        Dim endIndex As Integer = 0
        Dim stepNum As Integer = 0
        If p_fromTheRight Then
            startIndex = _maxButtonIndex
            endIndex = 0
            stepNum = -1
        Else
            startIndex = 0
            endIndex = _maxButtonIndex
            stepNum = 1
        End If

        For i = startIndex To endIndex Step stepNum
            _grid.ColumnDefinitions(i).Width = New GridLength(p_width)
            _buttons(i).Width = p_width
        Next
    End Sub
    ''' <summary>
    ''' Updates the sizes of the grids and buttons concurrently.
    ''' </summary>
    ''' <param name="p_widths">Widths to size each elements to.</param>
    ''' <param name="p_fromTheRight">True: The sizing will begin from the right-most element.</param>
    ''' <remarks></remarks>
    Private Sub SizeGridAndButtons(ByVal p_widths As List(Of Double),
                                   Optional ByVal p_fromTheRight As Boolean = True)
        Dim startIndex As Integer = 0
        Dim endIndex As Integer = 0
        Dim stepNum As Integer = 0
        If p_fromTheRight Then
            startIndex = _maxButtonIndex
            endIndex = 0
            stepNum = -1
        Else
            startIndex = 0
            endIndex = _maxButtonIndex
            stepNum = 1
        End If

        For i = startIndex To endIndex Step stepNum
            _grid.ColumnDefinitions(i).Width = New GridLength(p_widths(i))
            _buttons(i).Width = p_widths(i)
        Next
    End Sub

    ' Boolean
    Private Function ScrolledLeft() As Boolean
        Return _scrollDelta < 0
    End Function

    Private Function ScrolledRight() As Boolean
        Return _scrollDelta > 0
    End Function

    Private Function PropertiesSet() As Boolean
        Return Not (_grid Is Nothing OrElse
                    _dataGrid Is Nothing OrElse
                    _buttons Is Nothing OrElse
                    _tableView Is Nothing)
    End Function
#End Region

End Class
