Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls.Primitives
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Collections.ObjectModel
Imports System.Reflection

Imports CSiTester.cLibMisc

'References: 
'http://msdn.microsoft.com/en-us/library/bb613579(v=vs.110).aspx
'http://svgvijay.blogspot.com/2013/01/how-to-get-datagrid-cell-in-wpf.html
'http://social.msdn.microsoft.com/forums/vstudio/en-US/b7299e55-92e2-4a6b-8987-869fef8f22eb/wpf-datagrid-cell-content-by-code
'http://social.technet.microsoft.com/wiki/contents/articles/21202.wpf-programmatically-selecting-and-focusing-a-row-or-cell-in-a-datagrid.aspx

''' <summary>
''' Contains routines used with dataGrids for programmatic selection and manipulation of objects.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibDataGrid

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Get Objects Or Indices"
    ''' <summary>
    ''' Returns the selected DataGridRow object.
    ''' </summary>
    ''' <param name="p_grid">DataGrid object that contains the selection.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSelectedRow(ByVal p_grid As DataGrid) As DataGridRow
        If p_grid Is Nothing Then Return Nothing

        Return CType(p_grid.ItemContainerGenerator.ContainerFromItem(p_grid.SelectedItem), DataGridRow)

    End Function

    ''' <summary>
    ''' Returns a DataGridRow object based on the provided row index.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object that contains the selection.</param>
    ''' <param name="p_index">0-based index that corresponds to a row in the DataGrid to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRowByIndex(ByVal p_dataGrid As DataGrid,
                           ByVal p_index As Integer) As DataGridRow
        If (p_dataGrid Is Nothing OrElse p_index < 0) Then Return Nothing

        Dim row As DataGridRow

        row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_index), DataGridRow)
        If row Is Nothing Then
            '' May be virtualized, bring into view and try again.
            p_dataGrid.UpdateLayout()
            p_dataGrid.ScrollIntoView(p_dataGrid.Items(p_index))
            row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_index), DataGridRow)
        End If

        Return row
    End Function

    ''' <summary>
    ''' Returns the DataGridCell object of the selected cell.
    ''' </summary>
    ''' <param name="p_grid">DataGrid object that contains the selection.</param>
    ''' <param name="p_column">0-based index of the column that contains the selection within the DataGrid object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSelectedCell(ByVal p_grid As DataGrid,
                             Optional ByVal p_column As Integer = -1) As DataGridCell
        If p_grid Is Nothing Then Return Nothing

        Dim selectedCell As DataGridCell

        If p_column = -1 Then p_column = GetColumnIndexFromSelectionDG(p_grid)

        selectedCell = GetCellByRowSelection(p_grid, GetSelectedRow(p_grid), p_column)

        Return selectedCell
    End Function

    ''' <summary>
    ''' Returns the DataGridCell object of the selected row.
    ''' </summary>
    ''' <param name="p_grid">DataGrid object that contains the selection.</param>
    ''' <param name="p_row">DataGridRow object that is selected.</param>
    ''' <param name="p_column">0-based index of the column that contains the selection within the DataGrid object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCellByRowSelection(ByVal p_grid As DataGrid,
                                   ByVal p_row As DataGridRow,
                                   ByVal p_column As Integer) As DataGridCell
        If (p_grid Is Nothing OrElse
             p_row Is Nothing OrElse
            p_column < 0 OrElse
            p_column > p_grid.Columns.Count - 1) Then Return Nothing

        Dim presenter As DataGridCellsPresenter
        presenter = FindVisualChild(Of DataGridCellsPresenter)(p_row)

        If presenter Is Nothing Then
            p_grid.ScrollIntoView(p_row, p_grid.Columns(p_column))
            presenter = FindVisualChild(Of DataGridCellsPresenter)(p_row)
        End If

        If presenter IsNot Nothing Then
            Dim cell As DataGridCell
            cell = CType(presenter.ItemContainerGenerator.ContainerFromIndex(p_column), DataGridCell)
            Return cell
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the DataGridCell object specified by row and column indices.
    ''' </summary>
    ''' <param name="p_grid">DataGrid object that contains the selection.</param>
    ''' <param name="p_row">0-based index of the row that contains the selection within the DataGrid object.</param>
    ''' <param name="p_column">0-based index of the column that contains the selection within the DataGrid object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCellByRowColumnIndex(ByVal p_grid As DataGrid,
                                     ByVal p_row As Integer,
                                     ByVal p_column As Integer) As DataGridCell
        If (p_grid Is Nothing OrElse
            p_row < 0 OrElse p_row > p_grid.Items.Count - 1 OrElse
            p_column < 0 OrElse p_column > p_grid.Columns.Count - 1) Then Return Nothing

        Dim rowContainer As DataGridRow = GetRowByIndex(p_grid, p_row)
        Dim cell As DataGridCell = GetCellByRowSelection(p_grid, rowContainer, p_column)

        Return cell
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <typeparam name="childItem"></typeparam>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FindVisualChild(Of childItem As DependencyObject)(ByVal obj As DependencyObject) As childItem
        If obj Is Nothing Then Return Nothing
        Try
            For i As Integer = 0 To VisualTreeHelper.GetChildrenCount(obj) - 1
                Dim child As DependencyObject = VisualTreeHelper.GetChild(obj, i)
                If child IsNot Nothing AndAlso TypeOf child Is childItem Then
                    Return CType(child, childItem)
                Else
                    Dim childOfChild As childItem = FindVisualChild(Of childItem)(child)
                    If childOfChild IsNot Nothing Then
                        Return childOfChild
                    End If
                End If
            Next i
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return Nothing
    End Function


    ''' <summary>
    ''' Determines the column index for a selected cell. Returns -1 if there is no selection.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object.</param>
    ''' <param name="p_priorColIndex">Integer of the prior column index, in case the function fails, to prevent resetting to zero.</param>
    ''' <remarks></remarks>
    Public Shared Function GetColumnIndexFromSelectionDG(ByVal p_dataGrid As DataGrid,
                                            Optional ByVal p_priorColIndex As Integer = -1) As Integer
        Dim colIndex As Integer = -1

        If p_dataGrid Is Nothing Then Return colIndex

        Try
            If p_dataGrid.CurrentColumn IsNot Nothing Then
                colIndex = p_dataGrid.CurrentColumn.DisplayIndex
                'MsgBox(colIndex)
            Else    'Selection was lost. Use last selection index
                colIndex = p_priorColIndex
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return colIndex
    End Function

    ''' <summary>
    ''' Gets the header name of the column containing the specified cell. If no column index is given, the selected cell is used.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object.</param>
    ''' <param name="p_colIndex">Optional: Column index of the selected cell.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnHeader(ByVal p_dataGrid As DataGrid,
                             Optional ByVal p_colIndex As Integer = 0) As String
        Dim colHeader As String = ""

        If p_dataGrid Is Nothing Then Return colHeader

        Try
            'If column index is not provided, get it from the selection
            If (p_colIndex = 0 AndAlso p_dataGrid.CurrentColumn IsNot Nothing) Then p_colIndex = p_dataGrid.CurrentColumn.DisplayIndex

            colHeader = p_dataGrid.Columns(p_colIndex).Header.ToString
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return colHeader
    End Function

    ''' <summary>
    ''' Gets the value of the specified cell. If no column index is given, the selected cell value is returned.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object.</param>
    ''' <param name="p_colIndex">Optional: Column index of the selected cell.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCellValue(ByVal p_dataGrid As DataGrid,
                          Optional ByVal p_colIndex As Integer = -1) As String
        Dim cellValue As String = ""

        If p_dataGrid Is Nothing Then Return cellValue

        Dim drv As System.Data.DataRowView

        Try
            'If column index is not provided, get it from the selection
            If p_colIndex = -1 Then
                If p_dataGrid.CurrentColumn IsNot Nothing Then
                    p_colIndex = p_dataGrid.CurrentColumn.DisplayIndex
                Else
                    Return ""
                End If
            End If

            'Get the value of the cell
            drv = CType(p_dataGrid.SelectedItem, System.Data.DataRowView)
            If drv IsNot Nothing Then cellValue = drv.Item(p_colIndex).ToString()
            'MsgBox(cellValue)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return cellValue
    End Function

    ''' <summary>
    ''' Determines the row index for a selected cell.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object queried for the row index.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRowIndexFromSelection(ByVal p_dataGrid As DataGrid) As Integer
        Dim rowIndex As Integer = -1

        If p_dataGrid Is Nothing Then Return rowIndex

        rowIndex = p_dataGrid.SelectedIndex

        Return rowIndex
    End Function

    ''' <summary>
    ''' Returns the row and column indices of the cells in the provided datagrid that contain the value provided. Returns -1 if no match is found.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object queried for the indices.</param>
    ''' <param name="p_cellValue">Value to search for in the DataGrid.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndicesFromDataGridByCellValue(ByVal p_dataGrid As DataGrid,
                                                ByVal p_cellValue As String) As Dictionary(Of Integer, Integer)
        Dim row As DataGridRow
        Dim cellContent As TextBlock
        Dim rowColIndex As New Dictionary(Of Integer, Integer)

        'Add failure values
        rowColIndex.Add(-1, -1)

        If p_dataGrid Is Nothing Then Return rowColIndex

        'Search rows and columns for a cell that has a matching value to the cell provided.
        For rowIndex = 0 To p_dataGrid.Items.Count - 1
            row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex), DataGridRow)

            If row IsNot Nothing Then
                For colIndex = 0 To p_dataGrid.Columns.Count - 1
                    cellContent = CType(p_dataGrid.Columns(colIndex).GetCellContent(row), TextBlock)

                    'Check matching cell content
                    If Not (cellContent Is Nothing AndAlso cellContent.Text.Equals(p_cellValue)) Then
                        rowColIndex.Add(rowIndex, colIndex)
                    End If
                Next
            End If
        Next

        Return rowColIndex
    End Function

    ''' <summary>
    ''' Returns the row object associated with the cell object provided.
    ''' </summary>
    ''' <param name="p_cell">Cell object which is queried.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRowFromCell(ByVal p_cell As DataGridCell) As DataGridRow
        If p_cell Is Nothing Then Return Nothing

        Dim parent As DependencyObject = VisualTreeHelper.GetParent(p_cell)

        While (parent IsNot Nothing AndAlso Not parent.GetType() = GetType(DataGridRow))
            parent = VisualTreeHelper.GetParent(parent)
        End While

        Return CType(parent, DataGridRow)
    End Function

    ''' <summary>
    ''' Returns the DataGridColumn object of the cell object provided.
    ''' </summary>
    ''' <param name="p_cell">Cell object which is queried.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnFromCell(ByVal p_cell As DataGridCell) As DataGridColumn
        If p_cell Is Nothing Then Return Nothing

        Dim parent As DependencyObject = VisualTreeHelper.GetParent(p_cell)

        While (parent IsNot Nothing AndAlso Not parent.GetType() = GetType(DataGridColumn))
            parent = VisualTreeHelper.GetParent(parent)
        End While

        Return CType(parent, DataGridColumn)
    End Function

    ''' <summary>
    ''' Returns the ColumnHeader object of the cell object provided.
    ''' </summary>
    ''' <param name="p_cell">Cell object which is queried.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnHeaderFromCell(ByVal p_cell As DataGridCell) As DataGridColumnHeader
        If p_cell Is Nothing Then Return Nothing

        Dim parent As DependencyObject = VisualTreeHelper.GetParent(p_cell)

        While (parent IsNot Nothing AndAlso Not parent.GetType() = GetType(DataGridColumnHeader))
            parent = VisualTreeHelper.GetParent(parent)
        End While

        Return CType(parent, DataGridColumnHeader)
    End Function

    ''' <summary>
    ''' Returns the column index that corresponds to the header. If no match is found, -1 is returned.
    ''' </summary>
    ''' <param name="p_grid">Grid object to search.</param>
    ''' <param name="p_header">Header to search for in the DataGrid.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColIndexFromHeader(ByVal p_grid As DataGrid,
                                    ByVal p_header As String) As Integer
        If p_grid Is Nothing Then Return -1
        Dim colIndex As Integer = 0

        For i = 0 To p_grid.Columns.Count - 1
            If p_grid.Columns(i).Header.ToString = p_header Then Exit For
            colIndex += 1
        Next

        If colIndex = p_grid.Columns.Count Then colIndex = -1

        Return colIndex
    End Function

    ''' <summary>
    ''' Returns the value of a cell in a given DataGridRow at the specified column index.
    ''' </summary>
    ''' <param name="p_row">DataGridRow to search for the value.</param>
    ''' <param name="p_colIndex">Index corresponding to the column that contains the value in the row.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetValueFromRowAndIndex(ByVal p_row As DataGridRow,
                                     ByVal p_colIndex As Integer) As String
        Dim lastBMCellValue As String = ""

        If (p_row Is Nothing OrElse p_colIndex < 0) Then Return lastBMCellValue

        Dim drv As System.Data.DataRowView

        drv = CType(p_row.Item, System.Data.DataRowView)

        If drv IsNot Nothing Then lastBMCellValue = drv.Item(p_colIndex).ToString()

        Return lastBMCellValue
    End Function

    ''' <summary>
    ''' Returns the value of a cell in a given DataGridRow at the specified column header.
    ''' </summary>
    ''' <param name="p_row">DataGridRow to search for the value.</param>
    ''' <param name="p_header">Header corresponding to the column that contains the value in the row.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetValueFromRowAndHeader(ByVal p_row As DataGridRow,
                                     ByVal p_header As String) As String
        Dim lastBMCellValue As String = ""

        If p_row Is Nothing Then Return lastBMCellValue

        Dim drv As System.Data.DataRowView

        drv = CType(p_row.Item, System.Data.DataRowView)

        If drv IsNot Nothing Then lastBMCellValue = drv.Item(p_header).ToString()

        Return lastBMCellValue
    End Function

    ''' <summary>
    ''' Returns the DataGridCell that was right clicked.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCellFromRightClick(ByVal sender As Object,
                                   ByVal e As MouseButtonEventArgs) As DataGridCell
        If (sender Is Nothing OrElse e Is Nothing) Then Return Nothing

        Try
            Dim hit = VisualTreeHelper.HitTest(CType(sender, Visual), e.GetPosition(CType(sender, IInputElement)))

            Dim hitParent As DependencyObject = VisualTreeHelper.GetParent(hit.VisualHit)

            While (hitParent IsNot Nothing AndAlso Not hitParent.GetType() = GetType(DataGridCell))
                hitParent = VisualTreeHelper.GetParent(hitParent)
            End While

            Return CType(hitParent, DataGridCell)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Returns the row and column indices of a datagrid cell based on the provided x- &amp; y- coordinates.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to query.</param>
    ''' <param name="p_position">X- &amp; Y- position to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIndicesFromPosition(ByVal p_dataGrid As DataGrid,
                                                    ByVal p_position As Point) As Dictionary(Of Integer, Integer)
        Dim total As Double
        Dim rowIndex As Integer
        Dim columnIndex As Integer
        Dim indices As New Dictionary(Of Integer, Integer)
        indices.Add(-1, -1)

        If (p_dataGrid Is Nothing AndAlso PointIsEmpty(p_position)) Then Return indices

        Try
            Dim myScrollViewer As ScrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)

            columnIndex = -1
            total = 0
            Dim rowHeaders As DataGridRowHeader = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
            p_position.X -= (rowHeaders.ActualWidth - myScrollViewer.HorizontalOffset)
            For Each column As DataGridColumn In p_dataGrid.Columns
                If p_position.X < total Then Exit For

                columnIndex += 1
                total += column.Width.DisplayValue
            Next

            rowIndex = -1
            total = 0

            Dim originalOffset As Double = myScrollViewer.VerticalOffset
            Dim colHeadersPresenter As DataGridColumnHeadersPresenter = FindVisualChild(Of DataGridColumnHeadersPresenter)(p_dataGrid)
            p_position.Y -= colHeadersPresenter.ActualHeight
            For Each row As System.Data.DataRowView In p_dataGrid.Items
                If p_position.Y < total Then Exit For

                rowIndex += 1
                Dim dgRow As DataGridRow = GetRowByIndex(p_dataGrid, rowIndex)
                total += dgRow.ActualHeight
                p_dataGrid.UpdateLayout()
                If Not myScrollViewer.VerticalOffset = originalOffset Then p_dataGrid.ScrollIntoView(p_dataGrid.Items(CInt(myScrollViewer.ViewportHeight + originalOffset - 1)))
                p_dataGrid.UpdateLayout()
                If myScrollViewer.VerticalOffset > rowIndex Then p_position.Y += dgRow.ActualHeight
            Next

        Catch ex As Exception
            columnIndex = -1
            rowIndex = -1

            csiLogger.ExceptionAction(ex)
        End Try

        indices.Clear()
        indices.Add(rowIndex, columnIndex)

        Return indices
    End Function
#End Region
    
#Region "Programmatic Manipulation"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dataGrid"></param>
    ''' <param name="p_rowIndices"></param>
    ''' <param name="p_columnIndex"></param>
    ''' <remarks></remarks>
    Public Shared Sub SelectRowByIndices(ByVal p_dataGrid As DataGrid,
                           ByVal p_rowIndices As List(Of Integer),
                           Optional ByVal p_columnIndex As Integer = 0)
        If (p_dataGrid Is Nothing OrElse
             p_rowIndices Is Nothing) Then Exit Sub

        Dim cell As DataGridCell = Nothing

        Try
            If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.")
            If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")
            If (p_rowIndices.Count = 0 OrElse p_rowIndices.Count > p_dataGrid.Items.Count) Then Throw New ArgumentException("Invalid number of indexes.")

            p_dataGrid.SelectedItems.Clear()

            For Each rowIndex As Integer In p_rowIndices
                SelectRowByIndex(p_dataGrid, rowIndex, p_columnIndex, True)
            Next
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try

    End Sub

    ''' <summary>
    ''' Programmatically selects the row in a DataGrid.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid within which a row is to be selected.</param>
    ''' <param name="p_rowIndex">Index for the row to select.</param>
    ''' <param name="p_columnIndex">If provided, the specific column cell in the row will receive focus. Otherwise, the firs cell will be selected.</param>
    ''' <param name="p_selectRowByIndices">If true, the specific column cell in the row will also be selected.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectRowByIndex(ByVal p_dataGrid As DataGrid,
                         ByVal p_rowIndex As Integer,
                         Optional ByVal p_columnIndex As Integer = 0,
                         Optional ByVal p_selectRowByIndices As Boolean = False)
        If p_dataGrid Is Nothing Then Exit Sub

        Dim item As Object
        Dim row As DataGridRow
        Dim cell As DataGridCell = Nothing

        Try
            If Not p_selectRowByIndices Then
                If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.")
                If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")

                p_dataGrid.SelectedItems.Clear()
            End If

            If (p_rowIndex < 0 OrElse p_rowIndex > (p_dataGrid.Items.Count - 1)) Then Throw New ArgumentException("{0} is an invalid row index.", CStr(p_rowIndex))

            'Set the selectedItem property
            item = p_dataGrid.Items(p_rowIndex)

            If (p_selectRowByIndices AndAlso Not p_dataGrid.SelectionMode = DataGridSelectionMode.Single) Then
                p_dataGrid.SelectedItems.Add(item)
            Else
                p_dataGrid.SelectedItem = item
            End If

            'Select the row
            row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_rowIndex), DataGridRow)

            'Bring the data item into view in case it has been virtualized away
            If row Is Nothing Then
                p_dataGrid.ScrollIntoView(item)
                row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_rowIndex), DataGridRow)
            End If

            If p_columnIndex >= 0 Then
                If row IsNot Nothing Then
                    cell = GetCellFromDGRowAndColIndex(p_dataGrid, row, p_columnIndex)
                    If cell IsNot Nothing Then
                        cell.Focus()
                        ''Below is required to allow the user to resume 'shift-click' operations after programmatically selecting rows & cells
                        'Dim method As System.Reflection.MethodInfo
                        'method = Type.GetType("dataGrid").GetMethod("HandleSelectionForCellInput", BindingFlags.Instance Or BindingFlags.NonPublic)
                        'method.Invoke(New DataGrid, Nothing)
                    End If
                End If
            End If

        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub


    ''' <summary>
    ''' Selects the DataGridCell objects by the provided row-column indices.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object within which to select the cells.</param>
    ''' <param name="p_cellIndices">Row-column indices to select within the DataGrid.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectCellByIndices(ByVal p_dataGrid As DataGrid,
                            ByVal p_cellIndices As Dictionary(Of Integer, Integer))
        If (p_dataGrid Is Nothing OrElse p_cellIndices Is Nothing) Then Exit Sub

        Dim rowIndex As Integer
        Dim columnIndex As Integer

        Try
            If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to Cell.")
            If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")

            p_dataGrid.SelectedCells.Clear()

            For Each cellIndex As KeyValuePair(Of Integer, Integer) In p_cellIndices
                rowIndex = cellIndex.Key
                columnIndex = cellIndex.Value
                SelectCellsByIndex(p_dataGrid, rowIndex, columnIndex, True)
            Next
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub

    ''' <summary>
    ''' Selects the DataGridCell object by the provided row-column index pairs.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object within which to select the cells.</param>
    ''' <param name="p_rowIndex">Row index for the cell to be selected.</param>
    ''' <param name="p_columnIndex">Column index for the cell to be selected.</param>
    ''' <param name="p_selectByIndices">If 'True', then multiple selections are being perfomed, so the current selection will not be cleared. 
    ''' If 'False', the current selection will be cleared.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectCellsByIndex(ByVal p_dataGrid As DataGrid,
                           ByVal p_rowIndex As Integer,
                           ByVal p_columnIndex As Integer,
                           Optional ByVal p_selectByIndices As Boolean = False)
        If p_dataGrid Is Nothing Then Exit Sub

        Dim item As Object
        Dim row As DataGridRow
        Dim cell As DataGridCell
        Dim dataGridCellInfo As DataGridCellInfo

        Try
            If Not p_selectByIndices Then
                If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.Cell) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to Cell.")

                p_dataGrid.SelectedCells.Clear()
            End If

            If (p_rowIndex < 0 OrElse p_rowIndex > (p_dataGrid.Items.Count - 1)) Then Throw New ArgumentException("{0} is an invalid row index.", CStr(p_rowIndex))
            If (p_columnIndex < 0 OrElse p_columnIndex > (p_dataGrid.Columns.Count - 1)) Then Throw New ArgumentException("{0} is an invalid column index.", CStr(p_columnIndex))

            item = p_dataGrid.Items(p_rowIndex)

            'Select the row
            row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_rowIndex), DataGridRow)

            'Bring the data item into view in case it has been virtualized away
            If row Is Nothing Then
                p_dataGrid.ScrollIntoView(item)
                row = CType(p_dataGrid.ItemContainerGenerator.ContainerFromIndex(p_rowIndex), DataGridRow)
            End If

            If row IsNot Nothing Then
                cell = GetCellFromDGRowAndColIndex(p_dataGrid, row, p_columnIndex)
                If cell IsNot Nothing Then
                    dataGridCellInfo = New DataGridCellInfo(cell)
                    p_dataGrid.SelectedCells.Add(dataGridCellInfo)
                    cell.Focus()


                    'Below is required to allow the user to resume 'shift-click' operations after programmatically selecting rows & cells
                    'Dim method As System.Reflection.MethodInfo
                    'method = Type.GetType("dataGrid").GetMethod("HandleSelectionForCellInput", BindingFlags.Instance Or BindingFlags.NonPublic)
                    'method.Invoke(New DataGrid, Nothing)
                End If
            End If
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub

    ''' <summary>
    ''' Selects the DataGridCell(s) corresponding to the provided DataGridCell object. If multiple matches are found, multiple selections will be made.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid from within which to select a cell.</param>
    ''' <param name="p_dataGridCell">DataGridCell object to select.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectDataGridCells(ByVal p_dataGrid As DataGrid,
                           ByVal p_dataGridCell As DataGridCell)
        If (p_dataGrid Is Nothing OrElse p_dataGridCell Is Nothing) Then Exit Sub

        Try
            Dim cellValue As String
            Dim cellIndices As New Dictionary(Of Integer, Integer)

            If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.")
            If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")


            cellValue = p_dataGridCell.Content.ToString
            cellIndices = GetIndicesFromDataGridByCellValue(p_dataGrid, cellValue)
            SelectCellByIndices(p_dataGrid, cellIndices)
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub

    ''' <summary>
    ''' Selects the DataGridCell(s) corresponding to the provided cell value. If multiple matches are found, multiple selections will be made.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid from within which to select a cell.</param>
    ''' <param name="p_cellValue">Value of the cell to select.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectDataGridCellsByValue(ByVal p_dataGrid As DataGrid,
                                   ByVal p_cellValue As String)
        If p_dataGrid Is Nothing Then Exit Sub
        Try
            Dim cellIndices As New Dictionary(Of Integer, Integer)

            If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.")
            If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")

            cellIndices = GetIndicesFromDataGridByCellValue(p_dataGrid, p_cellValue)
            SelectCellByIndices(p_dataGrid, cellIndices)

        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub

    ''' <summary>
    ''' Selects the DataGridCell corresponding to the specified coordinates.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid from within which to select a cell.</param>
    ''' <param name="p_position">X- &amp; Y- coordinates of the position within the datagrid to grab.</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectDataGridCellByCursor(ByVal p_dataGrid As DataGrid,
                                                ByVal p_position As Point)
        If (p_dataGrid Is Nothing OrElse PointIsEmpty(p_position)) Then Exit Sub
        Try
            Dim cellIndices As New Dictionary(Of Integer, Integer)

            If Not p_dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow) Then Throw New ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.")
            If Not p_dataGrid.SelectionMode.Equals(DataGridSelectionMode.Extended) Then Throw New ArgumentException("The SelectionMode of the DataGrid must be set to Extended.")

            cellIndices = GetIndicesFromPosition(p_dataGrid, p_position)

            SelectRowByIndex(p_dataGrid, cellIndices.Keys(0), cellIndices.Values(0))

        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dataGrid"></param>
    ''' <param name="p_rowContainer"></param>
    ''' <param name="p_columnIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetCellFromDGRowAndColIndex(ByVal p_dataGrid As DataGrid,
                                         ByVal p_rowContainer As DataGridRow,
                                         ByVal p_columnIndex As Integer) As DataGridCell
        If (p_dataGrid Is Nothing OrElse
            p_rowContainer Is Nothing OrElse
            p_columnIndex < 0 OrElse
            p_columnIndex > p_dataGrid.Columns.Count - 1) Then Return Nothing

        Dim presenter As DataGridCellsPresenter
        Dim cell As DataGridCell

        presenter = FindVisualChild(Of DataGridCellsPresenter)(p_rowContainer)

        'If the row has been virtualized away, call its ApplyTemplate() method to build its visual tree in order for the DataGridCellsPresenter and the DataGridCells to be created
        If presenter Is Nothing Then
            p_rowContainer.ApplyTemplate()
            presenter = FindVisualChild(Of DataGridCellsPresenter)(p_rowContainer)
        End If
        If presenter IsNot Nothing Then
            cell = CType(presenter.ItemContainerGenerator.ContainerFromIndex(p_columnIndex), DataGridCell)
            Return cell
        End If

        Return Nothing
    End Function
#End Region

#Region "Manipulate By DataGrid"
    ''' <summary>
    ''' Adjusts the width of the supplied grid layout columns to match those of the supplied datagrid.
    ''' </summary>
    ''' <param name="p_gridColumns">Columns of a layout grid.</param>
    ''' <param name="p_dgColumns">Collection of columns from a datagrid.</param>
    ''' <param name="p_indexRange">If specified, only the key=start;value=end indices will be considered. Otherwise, all datagrid columns will be used.</param>
    ''' <remarks></remarks>
    Public Shared Sub SizeGridToDataGrid(ByRef p_gridColumns As ColumnDefinitionCollection,
                                         ByVal p_dgColumns As ObservableCollection(Of DataGridColumn),
                                        Optional ByVal p_indexRange As Dictionary(Of Integer, Integer) = Nothing)
        If (p_gridColumns Is Nothing OrElse
            p_dgColumns Is Nothing OrElse
            Not p_gridColumns.Count = p_dgColumns.Count) Then Exit Sub

        If p_indexRange Is Nothing Then
            If Not p_gridColumns.Count = p_dgColumns.Count Then Exit Sub
        Else
            If Not p_indexRange.Count = 1 Then Exit Sub

            Dim colRangeCount As Integer = p_indexRange.Values(0) - p_indexRange.Keys(0)
            If Not p_gridColumns.Count = colRangeCount Then Exit Sub
        End If
        ' COlla

        Dim minColIndex As Integer = 0
        If p_indexRange IsNot Nothing Then minColIndex = p_indexRange.Keys(0)

        Dim maxColIndex As Integer = p_dgColumns.Count - 1
        If p_indexRange IsNot Nothing Then maxColIndex = p_indexRange.Values(0)

        For i = minColIndex To maxColIndex
            If Not p_dgColumns(i).Visibility = Windows.Visibility.Collapsed Then
                p_gridColumns.Item(p_dgColumns(i).DisplayIndex).Width = New GridLength(p_dgColumns(i).ActualWidth)
            Else
                p_gridColumns.Item(p_dgColumns(i).DisplayIndex).Width = New GridLength(0)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Adjusts the width of the supplied buttons to match those of the supplied datagrid.
    ''' </summary>
    ''' <param name="p_buttons">Array of buttons.</param>
    ''' <param name="p_dgColumns">Collection of columns from a datagrid.</param>
    ''' <param name="p_indexRange">If specified, only the key=start;value=end indices will be considered. Otherwise, all datagrid columns will be used.</param>
    ''' <remarks></remarks>
    Public Shared Sub SizeButtonsToDataGrid(ByRef p_buttons As Button(),
                                            ByVal p_dgColumns As ObservableCollection(Of DataGridColumn),
                                            Optional ByVal p_indexRange As Dictionary(Of Integer, Integer) = Nothing)
        If (p_buttons Is Nothing OrElse
            p_buttons.Count = 0 OrElse
             p_dgColumns Is Nothing) Then Exit Sub

        If p_indexRange Is Nothing Then
            If Not p_buttons.Count = p_dgColumns.Count Then Exit Sub
        Else
            If Not p_indexRange.Count = 1 Then Exit Sub

            Dim colRangeCount As Integer = p_indexRange.Values(0) - p_indexRange.Keys(0)
            If Not p_buttons.Count = colRangeCount Then Exit Sub
        End If

        Dim minColIndex As Integer = 0
        If p_indexRange IsNot Nothing Then minColIndex = p_indexRange.Keys(0)

        Dim maxColIndex As Integer = p_dgColumns.Count - 1
        If p_indexRange IsNot Nothing Then maxColIndex = p_indexRange.Values(0)

        For i = minColIndex To maxColIndex
            If Not p_dgColumns(i).Visibility = Windows.Visibility.Collapsed Then
                p_buttons(p_dgColumns(i).DisplayIndex).Width = p_dgColumns(i).ActualWidth
            Else
                p_buttons(p_dgColumns(i).DisplayIndex).Width = 0
            End If
        Next
    End Sub


#End Region

#Region "Query DataGrid"
    ''' <summary>
    ''' Returns the offset value for the left side of the unit buttons such that they are flush with the left edge of the left-most data grid column.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLeftJustifyColumnHeaderPosition(ByVal p_dataGrid As DataGrid) As Double
        Dim marginOffsetLeft As Double = 0

        ' Get the width of the row headers, which are on the left side of the datagrid.
        marginOffsetLeft += DGRowHeadersWidth(p_dataGrid)
        If marginOffsetLeft = 0 Then Return marginOffsetLeft

        ' Account for the margin and border
        marginOffsetLeft += p_dataGrid.Margin.Left
        marginOffsetLeft += p_dataGrid.BorderThickness.Left

        Return marginOffsetLeft
    End Function

    ''' <summary>
    ''' Returns the width of the row headers element of the datagrid.
    ''' </summary>
    ''' <param name="p_dataGrid">Datagrid object to query.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DGRowHeadersWidth(ByVal p_dataGrid As DataGrid) As Double
        Dim rowHeaders As DataGridRowHeader = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)

        If rowHeaders Is Nothing Then
            Return 0
        Else
            Return rowHeaders.ActualWidth
        End If
    End Function

    ''' <summary>
    ''' Gets the minimum height for the DataGrid for the following criteria: Column header, horizontal scrollbar, and at least one row (or more if specified) are visible.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to determine the minimum height from.</param>
    ''' <param name="p_colHeadersPresenter">Column headers. If not supplied, they will be derived from the DataGrid object.</param>
    ''' <param name="p_numRowsMin">Minimum number of rows to have visible, >= 1. 
    ''' This is determined from the DataGrid object, but overwritten by any supplied rows object list.</param>
    ''' <param name="p_rows">Minimum DataGrid row objects set to include, starting from the first row. 
    ''' If not supplied, they will be derived from the DataGrid object.
    ''' This overwrites the specified minimum number of rows if both are specified.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DataGridHeightMin(ByVal p_dataGrid As DataGrid,
                                           Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
                                           Optional ByVal p_numRowsMin As Integer = -1,
                                           Optional ByVal p_rows As List(Of DataGridRow) = Nothing) As Double
        Dim minHeight As Double
        Dim rows As New List(Of DataGridRow)
        Dim numRows As Integer
        Dim colHeadersPresenter As DataGridColumnHeadersPresenter

        'Handle optional property assignments
        If p_colHeadersPresenter Is Nothing Then
            colHeadersPresenter = FindVisualChild(Of DataGridColumnHeadersPresenter)(p_dataGrid)
        Else
            colHeadersPresenter = p_colHeadersPresenter
        End If

        If p_numRowsMin < 1 Then
            numRows = 1
        Else
            numRows = p_numRowsMin
        End If

        If (p_rows Is Nothing OrElse p_rows.Count = 0) Then
            For i = numRows - 1 To 0 Step -1
                rows.Add(GetRowByIndex(p_dataGrid, i))
            Next
        Else
            rows = p_rows
        End If

        'Determine min height. If the headers are not yet accessible, use the first row height as an approximation
        If colHeadersPresenter IsNot Nothing Then
            minHeight += colHeadersPresenter.ActualHeight
        Else
            minHeight += rows(0).ActualHeight
        End If

        For Each row As DataGridRow In rows
            minHeight += row.ActualHeight
        Next

        minHeight += SystemParameters.HorizontalScrollBarHeight

        Return minHeight
    End Function

    ''' <summary>
    ''' Gets the minimum width for the DataGrid for the following criteria: Row header, vertical scrollbar, and at least one column (or more if specified) are visible.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to determine the minimum height from.</param>
    ''' <param name="p_rowHeaders">Row headers. If not supplied, they will be derived from the DataGrid object.</param>
    ''' <param name="p_numColumnsMin">Minimum number of columns to have visible, >= 1. 
    ''' This is determined from the DataGrid object, but overwritten by any supplied rows object list.</param>
    ''' <param name="p_columns">Minimum DataGrid column objects set to include, starting from the first column. 
    ''' If not supplied, they will be derived from the DataGrid object.
    ''' This overwrites the specified minimum number of rows if both are specified.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DataGridWidthMin(ByVal p_dataGrid As DataGrid,
                                           Optional ByVal p_rowHeaders As DataGridRowHeader = Nothing,
                                           Optional ByVal p_numColumnsMin As Integer = -1,
                                           Optional ByVal p_columns As List(Of DataGridColumn) = Nothing) As Double
        Dim minWidth As Double
        Dim columns As New List(Of DataGridColumn)
        Dim numColumns As Integer
        Dim rowHeaders As DataGridRowHeader

        'Handle optional property assignments
        If p_rowHeaders Is Nothing Then
            rowHeaders = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
        Else
            rowHeaders = p_rowHeaders
        End If

        If p_numColumnsMin < 1 Then
            numColumns = 1
        Else
            numColumns = p_numColumnsMin
        End If

        If (p_columns Is Nothing OrElse p_columns.Count = 0) Then
            For i = numColumns - 1 To 0 Step -1
                columns.Add(p_dataGrid.Columns(i))
            Next
        Else
            columns = p_columns
        End If

        'Determine min width
        minWidth += rowHeaders.ActualWidth

        For Each column As DataGridColumn In columns
            minWidth += column.ActualWidth
        Next

        minWidth += SystemParameters.HorizontalScrollBarHeight

        Return minWidth
    End Function

    ''' <summary>
    ''' Gets the minimum upper-left coordinate of the cells in the DataGrid accounting for headers.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use.</param>
    ''' <remarks></remarks>
    Public Shared Function GetDataGridCellExtentHeaders(ByVal p_dataGrid As DataGrid) As Point
        If p_dataGrid Is Nothing Then Return Nothing
        Try
            Dim pointTopLeft As Point
            Dim rowHeaders As DataGridRowHeader = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
            Dim colHeadersPresenter As DataGridColumnHeadersPresenter = FindVisualChild(Of DataGridColumnHeadersPresenter)(p_dataGrid)

            If Not (rowHeaders Is Nothing OrElse
                    colHeadersPresenter Is Nothing) Then

                With pointTopLeft
                    .X = rowHeaders.ActualWidth
                    .Y = colHeadersPresenter.ActualHeight
                End With

                Return pointTopLeft
            End If

        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Gets the maximum lower-right coordinate of the cells in the DataGrid accounting for scrollbars.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use.</param>
    ''' <remarks></remarks>
    Public Shared Function GetDataGridCellExtentScrollbars(ByVal p_dataGrid As DataGrid) As Point
        If p_dataGrid Is Nothing Then Return Nothing

        Try
            Dim pointBottomRight As Point
            Dim myScrollViewer As ScrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)

            If myScrollViewer IsNot Nothing Then
                p_dataGrid.UpdateLayout()
                With pointBottomRight
                    .X = myScrollViewer.ActualWidth - SystemParameters.VerticalScrollBarWidth
                    .Y = myScrollViewer.ActualHeight - SystemParameters.HorizontalScrollBarHeight
                End With

                Return pointBottomRight
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return Nothing
    End Function

#End Region
End Class
