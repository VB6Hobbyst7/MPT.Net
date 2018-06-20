Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.Windows.Controls.Primitives

''' <summary>
''' Module that contains methods specific to the functions of a form, but not any particular form, such as field entry validation.
''' </summary>
''' <remarks></remarks>
Module mForms

    '#Region "Data Validation"
    '    ''' <summary>
    '    ''' Returns True value if it is numeric. Optionally informs user if text is not numeric.
    '    ''' </summary>
    '    ''' <param name="myText">Text to check.</param>
    '    ''' <param name="isPositive">Optional: True if value must be a positive number.</param>
    '    ''' <param name="myAlertNum">Optional: Alert to present to the user if the entry is not numeric.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CheckValidEntryNumeric(ByVal myText As String,
    '                                    Optional ByVal isPositive As Boolean = False,
    '                                    Optional ByVal myAlertNum As String = "") As Boolean
    '        CheckValidEntryNumeric = True

    '        'Check that entry is numeric, and if so, if it is positive (if specified)
    '        If Not IsNumeric(myText) Then
    '            CheckValidEntryNumeric = False
    '        ElseIf isPositive And CDbl(myText) < 0 Then
    '            CheckValidEntryNumeric = False
    '        End If

    '        'Warn user if message is provided
    '        If Not CheckValidEntryNumeric Then
    '            If Not myAlertNum = "" Then MsgBox(myAlertNum)
    '        End If

    '    End Function

    '    ''' <summary>
    '    ''' Returns text value if it is an integer. Returns the provided default if it is not. Optionally informs user if text is not an integer or numeric.
    '    ''' </summary>
    '    ''' <param name="myText">Text to check.</param>
    '    ''' <param name="isPositive">Optional: True if value must be a positive number.</param>
    '    ''' <param name="myAlertNum">Optional: Alert to present to the user if the entry is not numeric.</param>
    '    ''' <param name="myAlertInt">Optional: Alert to present to the user if the entry is not an integer.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CheckValidEntryInteger(ByRef myText As String,
    '                                    Optional ByVal isPositive As Boolean = False,
    '                                    Optional ByVal myAlertNum As String = "",
    '                                    Optional ByVal myAlertInt As String = "") As Boolean
    '        Dim passedFirstCheck As Boolean = True

    '        CheckValidEntryInteger = True

    '        'Check that entry is numeric
    '        If CheckValidEntryNumeric(myText, isPositive, myAlertNum) Then
    '            'Check if entry is an integer
    '            Dim myEntryNumeric As Double = CDbl(myText)
    '            If Not Math.Round(myEntryNumeric) - myEntryNumeric = 0 Then
    '                CheckValidEntryInteger = False
    '            End If
    '        Else
    '            CheckValidEntryInteger = False
    '            passedFirstCheck = False
    '        End If

    '        'Warn user if message is provided
    '        If Not CheckValidEntryInteger And passedFirstCheck Then
    '            If Not myAlertInt = "" Then MsgBox(myAlertInt)
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Returns text value if it is an integer. Returns the provided default if it is not. Optionally informs user if text if various criteria fail to be met.
    '    ''' </summary>
    '    ''' <param name="myText">Text to check.</param>
    '    ''' <param name="myMin">Minimum value allowed for text.</param>
    '    ''' <param name="myMax">Maximum value allowed for text.</param>
    '    ''' <param name="IsInteger">Optional: Specifies if numeric number should also be confirmed as an integer.</param>
    '    ''' <param name="myAlertNum">Optional: Alert to present to the user if the entry is not numeric.</param>
    '    ''' <param name="myAlertInt">Optional: Alert to present to the user if the entry is not an integer.</param>
    '    ''' <param name="myAlertRangeMaxMin">Optional: Alert to present to the user if the entry is not within the specified range. If myAlertRangeMin is specified, this alert is only used if the entry is above the specified maximum.</param>
    '    ''' <param name="myAlertRangeMin">Optional: Alert to present to the user if the entry is below the specified minimum.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CheckValidEntryRange(ByRef myText As String,
    '                                  ByVal myMin As Double,
    '                                  ByVal myMax As Double, _
    '                                  Optional ByVal IsInteger As Boolean = False, _
    '                                  Optional ByVal myAlertRangeMaxMin As String = "", _
    '                                  Optional ByVal myAlertNum As String = "", _
    '                                  Optional ByVal myAlertInt As String = "", _
    '                                  Optional ByVal myAlertRangeMin As String = "") As Boolean
    '        Dim passedFirstCheck As Boolean = True

    '        CheckValidEntryRange = True

    '        If myAlertRangeMin = "" Then myAlertRangeMin = myAlertRangeMaxMin

    '        'Check if entry is an integer, if necessary
    '        If IsInteger Then
    '            If Not CheckValidEntryInteger(myText, , myAlertNum, myAlertInt) Then
    '                CheckValidEntryRange = False
    '                passedFirstCheck = False
    '            End If
    '        Else        'Check that entry is numerical
    '            If Not CheckValidEntryNumeric(myText, , myAlertNum) Then
    '                CheckValidEntryRange = False
    '                passedFirstCheck = False
    '            End If
    '        End If

    '        'Check range criteria
    '        If passedFirstCheck Then
    '            If CDbl(myText) < myMin Then
    '                CheckValidEntryRange = False
    '                If Not myAlertRangeMaxMin = "" Then MsgBox(myAlertRangeMin)
    '            ElseIf CDbl(myText) > myMax Then
    '                CheckValidEntryRange = False
    '                If Not myAlertRangeMaxMin = "" Then MsgBox(myAlertRangeMaxMin)
    '            End If
    '        End If

    '    End Function


    '    ''' <summary>
    '    ''' Checks whether the text in a textbox matches an entry in a listbox.
    '    ''' </summary>
    '    ''' <param name="listBoxName">Name of the listbox to search.</param>
    '    ''' <param name="textBoxName">Name of the textbox that contains the entry to search fo.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function checkEntryExistsListBox(ByRef listBoxName As ListBox, ByRef textBoxName As TextBox) As Boolean
    '        Dim i As Integer
    '        checkEntryExistsListBox = False

    '        For i = 0 To listBoxName.Items.Count - 1  'Selects initial cell
    '            If listBoxName.Items(i) Is textBoxName.Text Then
    '                checkEntryExistsListBox = True
    '                MsgBox("Entry already exists.")
    '                Exit For
    '            End If
    '        Next i
    '    End Function

    '    ''' <summary>
    '    ''' Checks if a textbox entry e-mail is valid by searching for the "@" character, and then whether a "." character follows.
    '    ''' </summary>
    '    ''' <param name="textBoxName">Name of the textbox to check entries from.</param>
    '    ''' <returns>True: e-mail is valid. False: e-mail is not valid.</returns>
    '    ''' <remarks></remarks>
    '    Function checkValidEmail(ByRef textBoxName As TextBox) As Boolean
    '        Dim i As Integer
    '        Dim j As Integer
    '        Dim strEmail As String
    '        Dim strMatch As Boolean

    '        strMatch = False
    '        strEmail = textBoxName.Text

    '        For i = 1 To Len(strEmail)
    '            If Mid(strEmail, i, 1) = "@" Then
    '                strMatch = True
    '            End If
    '            If strMatch = True Then
    '                For j = i To Len(strEmail)
    '                    If Not Mid(strEmail, j, 1) = "." Then
    '                        strMatch = False
    '                    Else
    '                        strMatch = True
    '                        checkValidEmail = True
    '                        Exit Function
    '                    End If
    '                Next j
    '            End If
    '        Next i
    '        MsgBox("Please enter a valid e-mail address.")
    '        checkValidEmail = False
    '    End Function

    '#End Region

    '#Region "Combo Box Methods"
    '    ''' <summary>
    '    ''' Populates combo boxes for a date entry with the correct list of years, and numeric months and days. 
    '    ''' Also automatically sets the drop boxes to the current date and time.
    '    ''' </summary>
    '    ''' <param name="cmbBxYear">Combo Box for selecting the year.</param>
    '    ''' <param name="cmbBxMonth">Combo Box for selecting the month (as number).</param>
    '    ''' <param name="cmbBxDay">Combo Box for selecting the day (as number).</param>
    '    ''' <param name="setCurrentTime">Optional: If true, combo boxes will automatically be set to select the current year, month and day, as applicable.</param>
    '    ''' <remarks></remarks>
    '    Sub SetDateComboBoxes(Optional ByRef cmbBxYear As ComboBox = Nothing, Optional ByRef cmbBxMonth As ComboBox = Nothing, Optional ByRef cmbBxDay As ComboBox = Nothing, Optional setCurrentTime As Boolean = False)
    '        Dim yearCurrent As Integer = Year(DateTime.Now)
    '        Dim monthCurrent As Integer = Month(DateTime.Now)
    '        Dim dayCurrent As Integer = Day(DateTime.Now)

    '        'Add Dates
    '        cmbBxYear.Items.Add(yearCurrent)

    '        'Year
    '        If Not IsNothing(cmbBxYear) Then
    '            'Fill combo box
    '            For i = 1 To 50
    '                cmbBxYear.Items.Add(yearCurrent - i)
    '            Next

    '            'Set combo box
    '            If setCurrentTime Then cmbBxYear.SelectedItem = yearCurrent
    '        End If

    '        'Month
    '        If Not IsNothing(cmbBxMonth) Then
    '            'Fill combo box
    '            For i = 1 To 12
    '                cmbBxMonth.Items.Add(i)
    '            Next

    '            'Set combo box
    '            If setCurrentTime Then cmbBxMonth.SelectedItem = monthCurrent
    '        End If

    '        'Day
    '        If Not IsNothing(cmbBxDay) Then
    '            If Not cmbBxDay.Items.Count > 0 Then
    '                'Fill combo box
    '                For i = 1 To 31
    '                    cmbBxDay.Items.Add(i)
    '                Next
    '            End If

    '            'Set combo box
    '            If setCurrentTime Then cmbBxDay.SelectedItem = dayCurrent
    '        End If





    '    End Sub

    '    ''' <summary>
    '    ''' Updates the 'days' combo box list depending on the month selected.
    '    ''' </summary>
    '    ''' <param name="monthValue">The month (as number), to be referenced.</param>
    '    ''' <param name="cmbBxDay">Combo Box for selecting the day (as number), to be altered.</param>
    '    ''' <remarks></remarks>
    '    Sub UpdateDayComboBox(ByVal monthValue As Integer, ByRef cmbBxDay As ComboBox)
    '        Dim maxDay As Integer

    '        'Resets the 'day' combo box list
    '        cmbBxDay.Items.Clear()

    '        'Sets new 'day' limit depending on the month
    '        If monthValue = 2 Then
    '            maxDay = 28
    '        ElseIf monthValue = 9 Or monthValue = 4 Or monthValue = 6 Or monthValue = 11 Then
    '            maxDay = 30
    '        Else
    '            maxDay = 31
    '        End If

    '        For i = 1 To maxDay
    '            cmbBxDay.Items.Add(i)
    '        Next
    '    End Sub

    '    ''' <summary>
    '    ''' Gets the selected index for a combo box by matching the list item to its position in the list of list items. Returns the 0-based index. If no list item or list are provided, returns 0.
    '    ''' </summary>
    '    ''' <param name="myListItem">Optional: Item to get selected index for.</param>
    '    ''' <param name="myStringList">Optional: List to query for the selected index.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function GetSelectedIndex(Optional ByVal myListItem As String = "", Optional ByVal myStringList As ObservableCollection(Of String) = Nothing) As Integer
    '        Dim i As Integer = 0

    '        If myListItem = "" Then Return 0

    '        If Not IsNothing(myStringList) Then
    '            For Each listItem As String In myStringList
    '                If listItem = myListItem Then Exit For
    '                i += 1
    '            Next
    '        End If

    '        If i = myStringList.Count Then  'No match was found
    '            Return 0
    '        Else                            'Match was found
    '            Return i
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Populates the combo box and sets the selected item.
    '    ''' </summary>
    '    ''' <param name="myComboBox">Name of the combo box to be filled.</param>
    '    ''' <param name="myList">List of items to add to the combo box.</param>
    '    ''' <param name="mySelection">Item to select in the combo box.</param>
    '    ''' <param name="ifEmptyCollapse">For handling a drop box where the list is empty. If true, the control visibility will be collapsed. If false, the control will be visible but disabled.</param>
    '    ''' <remarks></remarks>
    '    Sub LoadComboBoxes(ByRef myComboBox As ComboBox, ByVal myList As ObservableCollection(Of String), ByVal mySelection As String, ByVal ifEmptyCollapse As Boolean)
    '        Try
    '            'If no command line parameters are available for the given analysis setting, the control will be hidden
    '            If myList.Count = 0 Then
    '                If ifEmptyCollapse Then
    '                    myComboBox.Visibility = Windows.Visibility.Collapsed
    '                Else
    '                    myComboBox.IsEnabled = False
    '                End If
    '                Exit Sub
    '            Else
    '                myComboBox.Visibility = Windows.Visibility.Visible
    '            End If

    '            myComboBox.ItemsSource = myList
    '            SetComboBoxSelection(myComboBox, mySelection)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ' ''' <summary>
    '    ' ''' Sets the combo box to select the specified list entry, if it exists in the combo box list.
    '    ' ''' </summary>
    '    ' ''' <param name="p_comboBox">The combo box to be manipulated.</param>
    '    ' ''' <param name="p_selection">Item to select in the combo box.</param>
    '    ' ''' <remarks></remarks>
    '    'Sub SetComboBoxSelection(ByRef p_comboBox As ComboBox,
    '    '                         ByVal p_selection As String)
    '    '    Dim i As Integer
    '    '    Try
    '    '        For i = 0 To p_comboBox.Items.Count - 1   'Selects initial cell
    '    '            If p_comboBox.Items(i).ToString = p_selection Then
    '    '                p_comboBox.Text = p_selection
    '    '                Exit For
    '    '            End If
    '    '        Next i
    '    '    Catch ex As Exception
    '    '        'If Not suppressExStates Then
    '    '        '    'myLogger
    '    '        '    MsgBox(ex.Message)
    '    '        '    MsgBox(ex.StackTrace)
    '    '        'End If
    '    '    End Try
    '    'End Sub
    '#End Region

    '#Region "DataGrid Methods"
    '    ''' <summary>
    '    ''' Changes the maximum displayed height of the DataGrid provided based on the dimensions of the other objects provided so that scrollbars appear where appropriate.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to resize.</param>
    '    ''' <param name="p_gridMain">Main grid system for the form.</param>
    '    ''' <param name="p_dgRow">Grid row that contains the DataGrid.</param>
    '    ''' <param name="p_border">Border surrounding the DataGrid, if present.</param>
    '    ''' <remarks></remarks>
    '    Friend Sub UpdateDataGridHeight(ByRef p_dataGrid As DataGrid,
    '                                    ByVal p_gridMain As Grid,
    '                                    ByVal p_dgRow As RowDefinition,
    '                                    Optional p_border As Border = Nothing)
    '        Dim totalMargin As Double = 0
    '        totalMargin += p_dataGrid.Margin.Bottom
    '        totalMargin += SystemParameters.HorizontalScrollBarHeight
    '        If Not IsNothing(p_border) Then totalMargin += p_border.BorderThickness.Bottom
    '        totalMargin += p_gridMain.Margin.Bottom

    '        If p_dgRow.ActualHeight > totalMargin Then
    '            p_dataGrid.MaxHeight = p_dgRow.ActualHeight - totalMargin
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Gets the minimum height for the DataGrid for the following criteria: Column header, horizontal scrollbar, and at least one row (or more if specified) are visible.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to determine the minimum height from.</param>
    '    ''' <param name="p_colHeadersPresenter">Column headers. If not supplied, they will be derived from the DataGrid object.</param>
    '    ''' <param name="p_numRowsMin">Minimum number of rows to have visible, >= 1. 
    '    ''' This is determined from the DataGrid object, but overwritten by any supplied rows object list.</param>
    '    ''' <param name="p_rows">Minimum DataGrid row objects set to include, starting from the first row. 
    '    ''' If not supplied, they will be derived from the DataGrid object.
    '    ''' This overwrites the specified minimum number of rows if both are specified.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function CalcDataGridHeightMin(ByVal p_dataGrid As DataGrid,
    '                                           Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
    '                                           Optional ByVal p_numRowsMin As Integer = -1,
    '                                           Optional ByVal p_rows As List(Of DataGridRow) = Nothing) As Double
    '        Dim minHeight As Double
    '        Dim rows As New List(Of DataGridRow)
    '        Dim numRows As Integer
    '        Dim colHeadersPresenter As DataGridColumnHeadersPresenter

    '        'Handle optional property assignments
    '        If IsNothing(p_colHeadersPresenter) Then
    '            colHeadersPresenter = FindVisualChild(Of DataGridColumnHeadersPresenter)(p_dataGrid)
    '        Else
    '            colHeadersPresenter = p_colHeadersPresenter
    '        End If

    '        If p_numRowsMin < 1 Then
    '            numRows = 1
    '        Else
    '            numRows = p_numRowsMin
    '        End If

    '        If (IsNothing(p_rows) OrElse p_rows.Count = 0) Then
    '            For i = numRows - 1 To 0 Step -1
    '                rows.Add(GetRowByIndex(p_dataGrid, i))
    '            Next
    '        Else
    '            rows = p_rows
    '        End If

    '        'Determine min height. If the headers are not yet accessible, use the first row height as an approximation
    '        If Not IsNothing(colHeadersPresenter) Then
    '            minHeight += colHeadersPresenter.ActualHeight
    '        Else
    '            minHeight += rows(0).ActualHeight
    '        End If

    '        For Each row As DataGridRow In rows
    '            minHeight += row.ActualHeight
    '        Next

    '        minHeight += SystemParameters.HorizontalScrollBarHeight

    '        Return minHeight
    '    End Function

    '    ''' <summary>
    '    ''' Gets the minimum width for the DataGrid for the following criteria: Row header, vertical scrollbar, and at least one column (or more if specified) are visible.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to determine the minimum height from.</param>
    '    ''' <param name="p_rowHeaders">Row headers. If not supplied, they will be derived from the DataGrid object.</param>
    '    ''' <param name="p_numColumnsMin">Minimum number of columns to have visible, >= 1. 
    '    ''' This is determined from the DataGrid object, but overwritten by any supplied rows object list.</param>
    '    ''' <param name="p_columns">Minimum DataGrid column objects set to include, starting from the first column. 
    '    ''' If not supplied, they will be derived from the DataGrid object.
    '    ''' This overwrites the specified minimum number of rows if both are specified.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function CalcDataGridWidthMin(ByVal p_dataGrid As DataGrid,
    '                                           Optional ByVal p_rowHeaders As DataGridRowHeader = Nothing,
    '                                           Optional ByVal p_numColumnsMin As Integer = -1,
    '                                           Optional ByVal p_columns As List(Of DataGridColumn) = Nothing) As Double
    '        Dim minWidth As Double
    '        Dim columns As New List(Of DataGridColumn)
    '        Dim numColumns As Integer
    '        Dim rowHeaders As DataGridRowHeader

    '        'Handle optional property assignments
    '        If IsNothing(p_rowHeaders) Then
    '            rowHeaders = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
    '        Else
    '            rowHeaders = p_rowHeaders
    '        End If

    '        If p_numColumnsMin < 1 Then
    '            numColumns = 1
    '        Else
    '            numColumns = p_numColumnsMin
    '        End If

    '        If (IsNothing(p_columns) OrElse p_columns.Count = 0) Then
    '            For i = numColumns - 1 To 0 Step -1
    '                columns.Add(p_dataGrid.Columns(i))
    '            Next
    '        Else
    '            columns = p_columns
    '        End If

    '        'Determine min width
    '        minWidth += rowHeaders.ActualWidth

    '        For Each column As DataGridColumn In columns
    '            minWidth += column.ActualWidth
    '        Next

    '        minWidth += SystemParameters.HorizontalScrollBarHeight

    '        Return minWidth
    '    End Function
    '#End Region

    '#Region "Sizing Methods"

    '    'Friend Function GetCurrentScreen(ByVal p_form As Window) As System.Windows.Forms.Screen
    '    '    Dim currentScreen As System.Windows.Forms.Screen

    '    '    currentScreen = System.Windows.Forms.Screen.FromHandle(New System.Windows.Interop.WindowInteropHelper(p_form).Handle)

    '    '    Return currentScreen
    '    'End Function

    '    'Function GetScreenWidthsTotal() As Double
    '    '    Dim screens As System.Windows.Forms.Screen() = System.Windows.Forms.Screen.AllScreens
    '    '    Dim totalScreenWidth As Double = 0

    '    '    If screens.Count > 1 Then
    '    '        For Each screen As System.Windows.Forms.Screen In screens
    '    '            totalScreenWidth += screen.WorkingArea.Size.Width
    '    '        Next
    '    '    Else
    '    '        totalScreenWidth = My.Computer.Screen.WorkingArea.Size.Width
    '    '    End If

    '    '    Return totalScreenWidth
    '    'End Function

    '    ''' <summary>
    '    ''' Assigns the minimum/maximum form dimensions for the parameters provided. 
    '    ''' By default, minimum dimensions are only assigned if they are greater than the current min size (meant for form first-loading).
    '    ''' By default, maximum dimensions are only assigned if they are less than the current max size (meant for form first-loading).
    '    ''' </summary>
    '    ''' <param name="p_form">Form object to size.</param>
    '    ''' <param name="p_minHeight">Minimum height allowed for the DataGrid.</param>
    '    ''' <param name="p_minWidth">Minimum width allowed for the DataGrid.</param>
    '    ''' <param name="p_maxHeight">Maximum height allowed for the DataGrid.</param>
    '    ''' <param name="p_maxWidth">Maximum width allowed for the DataGrid.</param>
    '    ''' <param name="p_overwriteAll">If 'True', DataGrid extents are assigned the provided parameters regardless of the existing sizes. 
    '    ''' Otherwise, assignments are only made based on default considerations that are expected of forms when they are first loading.</param>
    '    ''' <param name="p_setSizeToMaximum">If 'True', then the form size will be set to the maximum size possible.</param>
    '    ''' <remarks></remarks>
    '    Friend Sub AssignFormDimensions(ByRef p_form As Window,
    '                                     Optional ByVal p_minHeight As Double = -1,
    '                                     Optional ByVal p_minWidth As Double = -1,
    '                                     Optional ByVal p_maxHeight As Double = -1,
    '                                     Optional ByVal p_maxWidth As Double = -1,
    '                                     Optional ByVal p_overwriteAll As Boolean = False,
    '                                     Optional ByVal p_setSizeToMaximum As Boolean = False)
    '        Dim currentScreen As System.Windows.Forms.Screen = System.Windows.Forms.Screen.FromHandle(New System.Windows.Interop.WindowInteropHelper(p_form).Handle)
    '        Dim screenWidth As Double = My.Computer.Screen.WorkingArea.Size.Width
    '        Dim totalScreenWidth As Double = 0
    '        Dim screenHeight As Double = My.Computer.Screen.WorkingArea.Size.Height
    '        Dim screens As System.Windows.Forms.Screen() = System.Windows.Forms.Screen.AllScreens

    '        If screens.Count > 1 Then
    '            For Each screen As System.Windows.Forms.Screen In screens
    '                totalScreenWidth += screen.WorkingArea.Size.Width
    '            Next
    '        Else
    '            totalScreenWidth = screenWidth
    '        End If

    '        With p_form
    '            If p_overwriteAll Then
    '                If p_minHeight > 0 Then .MinHeight = p_minHeight
    '                If p_minWidth > 0 Then .MinWidth = p_minWidth
    '                If p_maxHeight > 0 Then .MaxHeight = p_maxHeight
    '                If p_maxWidth > 0 Then .MaxWidth = p_maxWidth
    '            Else
    '                'Minimum dimensions
    '                If (p_minHeight > 0 AndAlso
    '                    .MinHeight < p_minHeight) Then .MinHeight = p_minHeight

    '                If (p_minWidth > 0 AndAlso
    '                    .MinWidth < p_minWidth) Then .MinWidth = p_minWidth

    '                'Maximum dimensions
    '                If (p_maxHeight > 0 AndAlso
    '                    p_maxHeight < screenHeight) Then

    '                    .MaxHeight = p_maxHeight
    '                Else
    '                    .MaxHeight = screenHeight
    '                End If

    '                If (p_maxWidth > 0 AndAlso
    '                    p_maxWidth < totalScreenWidth) Then

    '                    .MaxWidth = p_maxWidth
    '                Else
    '                    .MaxWidth = totalScreenWidth
    '                End If
    '            End If
    '        End With

    '        If p_setSizeToMaximum Then SetSizeToMaximum(p_form)
    '    End Sub

    '    ''' <summary>
    '    ''' Sets the form size to the maximum sizes determined for the form, bounded by screen dimensions.
    '    ''' </summary>
    '    ''' <param name="p_form">Form object to size.</param>
    '    ''' <remarks></remarks>
    '    Friend Sub SetSizeToMaximum(ByRef p_form As Window)
    '        Dim screenHeight As Double = My.Computer.Screen.WorkingArea.Size.Height
    '        Dim screenWidth As Double = My.Computer.Screen.WorkingArea.Size.Width

    '        With p_form
    '            .SizeToContent = SizeToContent.Manual
    '            If .MaxHeight < screenHeight Then
    '                .Height = .MaxHeight
    '            Else
    '                .Height = screenHeight
    '            End If
    '            If .MaxWidth < screenWidth Then
    '                .Width = .MaxWidth
    '            Else
    '                .Width = screenWidth
    '            End If
    '        End With
    '    End Sub

    '    ''' <summary>
    '    ''' Assigns the minimum/maximum DataGrid dimensions for the parameters provided. 
    '    ''' By default, minimum dimensions are only assigned if they are greater than the current min size (meant for form first-loading).
    '    ''' By default, maximum dimensions are only assigned if they are less than the current max size (meant for form first-loading).
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to size.</param>
    '    ''' <param name="p_minHeight">Minimum height allowed for the DataGrid.</param>
    '    ''' <param name="p_minWidth">Minimum width allowed for the DataGrid.</param>
    '    ''' <param name="p_maxHeight">Maximum height allowed for the DataGrid.</param>
    '    ''' <param name="p_maxWidth">Maximum width allowed for the DataGrid.</param>
    '    ''' <param name="p_overwriteAll">If 'True', DataGrid extents are assigned the provided parameters regardless of the existing sizes. 
    '    ''' Otherwise, assignments are only made based on default considerations that are expected of forms when they are first loading.</param>
    '    ''' <remarks></remarks>
    '    Friend Sub AssignDGDimensions(ByRef p_dataGrid As DataGrid,
    '                                  Optional ByVal p_minHeight As Double = -1,
    '                                  Optional ByVal p_minWidth As Double = -1,
    '                                  Optional ByVal p_maxHeight As Double = -1,
    '                                  Optional ByVal p_maxWidth As Double = -1,
    '                                  Optional ByVal p_overwriteAll As Boolean = False)
    '        With p_dataGrid
    '            If p_overwriteAll Then
    '                If p_minHeight > 0 Then .MinHeight = p_minHeight
    '                If p_minWidth > 0 Then .MinWidth = p_minWidth
    '                If p_maxHeight > 0 Then .MaxHeight = p_maxHeight
    '                If p_maxWidth > 0 Then .MaxWidth = p_maxWidth
    '            Else
    '                'Minimum dimensions
    '                If (p_minHeight > 0 AndAlso
    '                    .MinHeight < p_minHeight) Then .MinHeight = p_minHeight

    '                If (p_minWidth > 0 AndAlso
    '                    .MinWidth < p_minWidth) Then .MinWidth = p_minWidth

    '                'Maximum dimensions
    '                If (p_maxHeight > 0 AndAlso
    '                    .MaxHeight > p_maxHeight) Then .MaxHeight = p_minHeight

    '                If (p_maxWidth > 0 AndAlso
    '                    .MaxWidth > p_maxWidth) Then .MaxWidth = p_minWidth
    '            End If
    '        End With
    '    End Sub

    '    '=== Layout Sizes
    '    ''' <summary>
    '    ''' Gets the minimum height of the form based on the standard included elements. 
    '    ''' This includes the form outside border.
    '    ''' </summary>
    '    ''' <param name="p_form">Form object to get the height og.</param>
    '    ''' <param name="p_grpBx">Form main groupbox.</param>
    '    ''' <param name="p_grid">Form main grid.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetFormHeightMinElements(ByVal p_form As Window,
    '                                          ByVal p_grpBx As GroupBox,
    '                                          ByVal p_grid As Grid) As Double
    '        'Determine offset of window boundary vs. canvas
    '        Dim offsetHeight As Double = p_form.ActualHeight - p_grpBx.ActualHeight
    '        Dim minHeight As Double = 0

    '        With p_grpBx
    '            minHeight += .Margin.Top
    '            minHeight += .Margin.Bottom
    '            minHeight += .Padding.Top
    '            minHeight += .Padding.Bottom
    '            minHeight += .BorderThickness.Top
    '            minHeight += .BorderThickness.Bottom
    '        End With
    '        With p_grid
    '            minHeight += .Margin.Top
    '            minHeight += .Margin.Bottom
    '        End With

    '        minHeight += offsetHeight

    '        Return minHeight
    '    End Function

    '    ''' <summary>
    '    ''' Gets the minimum width of the form based on the standard included elements. 
    '    ''' This includes the form outside border.
    '    ''' </summary>
    '    ''' <param name="p_form">Form object to get the height og.</param>
    '    ''' <param name="p_grpBx">Form main groupbox.</param>
    '    ''' <param name="p_grid">Form main grid.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetFormWidthMinElements(ByVal p_form As Window,
    '                                            ByVal p_grpBx As GroupBox,
    '                                            ByVal p_grid As Grid) As Double
    '        Dim offsetWidth As Double = p_form.ActualWidth - p_grpBx.ActualWidth
    '        Dim minWidth As Double = 0

    '        With p_grpBx
    '            minWidth += .Margin.Left
    '            minWidth += .Margin.Right
    '            minWidth += .Padding.Left
    '            minWidth += .Padding.Right
    '            minWidth += .BorderThickness.Left
    '            minWidth += .BorderThickness.Right
    '        End With
    '        With p_grid
    '            minWidth += .Margin.Left
    '            minWidth += .Margin.Right
    '        End With

    '        minWidth += offsetWidth

    '        Return minWidth
    '    End Function

    '    '=== Control Sizes
    '    ''' <summary>
    '    ''' Gets the total outside height of the list of grid rows provided. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_gridRows">List of grid rows to total into one height dimension.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetGridHeights(ByVal p_gridRows As List(Of RowDefinition)) As Double
    '        Dim gridHeights As Double = 0

    '        For Each gridRow As RowDefinition In p_gridRows
    '            gridHeights += gridRow.ActualHeight
    '        Next

    '        Return gridHeights
    '    End Function

    '    ''' <summary>
    '    ''' Gets the total outside width of the list of grid columns provided. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_gridColumns">List of grid columns to total into one width dimension.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetGridWidths(ByVal p_gridColumns As List(Of ColumnDefinition)) As Double
    '        Dim gridWidths As Double = 0

    '        For Each gridColumn As ColumnDefinition In p_gridColumns
    '            gridWidths += gridColumn.ActualWidth
    '        Next

    '        Return gridWidths
    '    End Function

    '    ''' <summary>
    '    ''' Gets the total outside height of the list of buttons provided. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_buttons">List of buttons to total into one height dimension.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetButtonHeights(ByVal p_buttons As List(Of Button)) As Double
    '        Dim minHeight As Double = 0

    '        For Each button In p_buttons
    '            With button
    '                minHeight += .ActualHeight
    '                minHeight += .Margin.Top
    '                minHeight += .Margin.Bottom
    '                minHeight += .Padding.Top
    '                minHeight += .Padding.Bottom
    '            End With
    '        Next

    '        Return minHeight
    '    End Function

    '    ''' <summary>
    '    ''' Gets the total outside width of the list of grid columns provided. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_buttons">List of buttons to total into one width dimension.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetButtonWidths(ByVal p_buttons As List(Of Button)) As Double
    '        Dim minWidth As Double = 0

    '        For Each button In p_buttons
    '            With button
    '                minWidth += .ActualWidth
    '                minWidth += .Margin.Left
    '                minWidth += .Margin.Right
    '                minWidth += .Padding.Left
    '                minWidth += .Padding.Right
    '            End With
    '        Next

    '        Return minWidth
    '    End Function

    '    ''' <summary>
    '    ''' Gets the total outside maximum height of the DataGrid provided, including standard margins &amp; borders. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    '    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    '    ''' <param name="p_rows">Rows to consider for the fully displayed DataGrid for max height. 
    '    ''' If not provided, it is determined from the DataGrid object.
    '    ''' If one is provided, all rows are assumed to be the same size.</param>
    '    ''' <param name="p_colHeadersPresenter">Column headers to consider. 
    '    ''' If not provided, it is determined from the DataGrid object.</param>
    '    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display height.
    '    ''' If not provided, it is determined from the DataGrid object.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetDataGridHeightMax(ByVal p_dataGrid As DataGrid,
    '                                      Optional ByVal p_border As Border = Nothing,
    '                                      Optional ByVal p_rows As List(Of DataGridRow) = Nothing,
    '                                      Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
    '                                      Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
    '        Dim maxHeight As Double
    '        Dim rows As New List(Of DataGridRow)
    '        Dim rowsProvided As Boolean = False
    '        Dim scrollViewer As ScrollViewer

    '        'Handle Optional Parameters
    '        If (IsNothing(p_rows) OrElse p_rows.Count = 0) Then
    '            rowsProvided = False
    '        Else
    '            rowsProvided = True
    '        End If

    '        If IsNothing(p_scrollViewer) Then
    '            scrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)
    '        Else
    '            scrollViewer = p_scrollViewer
    '        End If

    '        'Get total min height assuming 1 row was the minimum visible
    '        maxHeight += GetDataGridHeightMin(p_dataGrid, p_border, -1, p_colHeadersPresenter, p_rows)

    '        If Not (IsNothing(rows) OrElse IsNothing(scrollViewer)) Then
    '            If scrollViewer.CanContentScroll Then
    '                Dim rowHeight As Double

    '                For rowIndex = 1 To scrollViewer.ExtentHeight - 1  'First row skipped as it is already included in the min height =
    '                    rowHeight = 0
    '                    If rowsProvided Then
    '                        If p_rows.Count = 1 Then
    '                            rowHeight += p_rows(0).ActualHeight
    '                        Else
    '                            rowHeight += p_rows(CInt(rowIndex)).ActualHeight
    '                        End If
    '                    Else
    '                        rowHeight += GetRowByIndex(p_dataGrid, CInt(rowIndex)).ActualHeight
    '                    End If
    '                    maxHeight += rowHeight
    '                Next

    '                'Final multiplier of last row height & pixel subtraction is for gap left under row from scrollbar + last row's bottom border
    '                maxHeight += 0.5 * rowHeight - 1
    '            Else
    '                maxHeight += scrollViewer.ExtentHeight
    '            End If
    '        End If

    '        Return maxHeight
    '    End Function

    '    ''' <summary>
    '    ''' Gets the total outside minimum height of the DataGrid provided, including standard margins &amp; borders. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    '    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    '    ''' <param name="p_minDGHeight">Calculated minimum DataGrid height.
    '    ''' If not provided, it is determined from the DataGrid object.</param>
    '    ''' <param name="p_colHeadersPresenter">Column headers to consider. 
    '    ''' If not provided, it is determined from the DataGrid object.</param>
    '    ''' <param name="p_rows"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetDataGridHeightMin(ByVal p_dataGrid As DataGrid,
    '                                         Optional ByVal p_border As Border = Nothing,
    '                                         Optional ByVal p_minDGHeight As Double = -1,
    '                                         Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
    '                                         Optional ByVal p_rows As List(Of DataGridRow) = Nothing) As Double
    '        Dim minHeight As Double
    '        Dim minDGHeight As Double

    '        If Not IsNothing(p_border) Then
    '            With p_border
    '                minHeight += .BorderThickness.Top
    '                minHeight += .BorderThickness.Bottom
    '            End With
    '        End If

    '        With p_dataGrid
    '            minHeight += .Margin.Top
    '            minHeight += .Margin.Bottom
    '            minHeight += .Padding.Top
    '            minHeight += .Padding.Bottom
    '            minHeight += .BorderThickness.Top
    '            minHeight += .BorderThickness.Bottom

    '            If p_minDGHeight <= 0 Then
    '                minDGHeight = CalcDataGridHeightMin(p_dataGrid, p_colHeadersPresenter, , p_rows)
    '            Else
    '                minDGHeight = p_minDGHeight
    '            End If
    '            minHeight += Math.Max(minDGHeight, p_dataGrid.MinHeight)
    '        End With

    '        Return minHeight
    '    End Function

    '    'TODO: Reconcile the next 3 functions
    '    ''' <summary>
    '    '''  Gets the total outside maximum width of the DataGrid provided, including standard margins &amp; borders. 
    '    ''' This includes all components of the control size.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    '    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    '    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display width.
    '    ''' If not provided, it is determined from the DataGrid object.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Function GetDataGridWidthMax(ByVal p_dataGrid As DataGrid,
    '                                        Optional ByVal p_border As Border = Nothing,
    '                                        Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
    '        Dim maxWidth As Double = 0
    '        Dim scrollViewer As ScrollViewer


    '        If IsNothing(p_scrollViewer) Then
    '            scrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)
    '        Else
    '            scrollViewer = p_scrollViewer
    '        End If

    '        maxWidth += scrollViewer.ExtentWidth
    '        maxWidth += SystemParameters.VerticalScrollBarWidth

    '        maxWidth += GetDataGridBorderElementsWidth(p_dataGrid, p_border)

    '        Return maxWidth
    '    End Function

    '    Friend Function GetDataGridBorderElementsWidth(ByVal p_dataGrid As DataGrid,
    '                                                        Optional ByVal p_border As Border = Nothing) As Double
    '        Dim gridWidth As Double = 0

    '        If Not IsNothing(p_border) Then
    '            With p_border
    '                gridWidth += .BorderThickness.Right
    '                gridWidth += .BorderThickness.Left
    '            End With
    '        End If

    '        With p_dataGrid
    '            gridWidth += .Margin.Right
    '            gridWidth += .Margin.Left
    '            gridWidth += .Padding.Left
    '            gridWidth += .Padding.Right
    '            gridWidth += .BorderThickness.Left
    '            gridWidth += .BorderThickness.Right
    '        End With

    '        Return gridWidth
    '    End Function

    '    ''' <summary>
    '    ''' Determines the maximim width of the DataGrid including border elements and all visible columns.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid object to determine the maximum width from.</param>
    '    ''' <param name="p_borders">Border object surrounding DataGrid to include in the width.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CalcDataGridWidthMax(ByRef p_dataGrid As DataGrid,
    '                            Optional ByVal p_borders As Border = Nothing) As Double
    '        Dim maxWidthDG As Double = 0

    '        Dim rowHeaders As DataGridRowHeader = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
    '        maxWidthDG += rowHeaders.ActualWidth
    '        If Not IsNothing(p_borders) Then
    '            With p_borders
    '                maxWidthDG += .BorderThickness.Left
    '                maxWidthDG += .BorderThickness.Right
    '            End With
    '        End If

    '        For Each column As DataGridColumn In p_dataGrid.Columns
    '            If column.Visibility = Windows.Visibility.Visible Then maxWidthDG += column.ActualWidth
    '        Next

    '        Return maxWidthDG
    '    End Function

    '    ''' <summary>
    '    ''' Expand the last column of the provided DataGrid to fill the DataGrid if the grid size cannot match the columns. 
    '    ''' This prevents an empty trailing column from appearing.
    '    ''' </summary>
    '    ''' <param name="p_dataGrid">DataGrid to perform the method on.</param>
    '    ''' <param name="p_minWidth">Minimum width to be allowed for the DataGrid.</param>
    '    ''' <param name="p_maxWidth">Maximum width to be allowed for the DataGrid.</param>
    '    ''' <remarks></remarks>
    '    Sub ExpandLastColumnToFit(ByRef p_dataGrid As DataGrid,
    '                              ByVal p_minWidth As Double,
    '                              ByVal p_maxWidth As Double)
    '        Dim columnReverse As DataGridColumn
    '        If p_maxWidth < p_minWidth Then
    '            For i = p_dataGrid.Columns.Count - 1 To 0 Step -1
    '                columnReverse = p_dataGrid.Columns(i)
    '                If columnReverse.Visibility = Windows.Visibility.Visible Then
    '                    columnReverse.Width = columnReverse.ActualWidth + (p_minWidth - p_maxWidth)
    '                    Exit For
    '                End If
    '            Next
    '        End If
    '    End Sub

    '#End Region








    '    'Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Long) As Long
    '    'Private Const SWP_NOMOVE = &H2
    '    'Private Const SWP_NOSIZE = &H1
    '    'Private Const SWP_SHOWWINDOW = &H40
    '    'Private Const SWP_NOActivate = &H10
    '    'Public Enum WindowPos
    '    '    vbtopmost = -1&
    '    '    vbnottopmost = -2&
    '    'End Enum

    '    'Public Sub SetFormPosition(hWnd As Long, Position As WindowPos)
    '    '    Const wFlags As Long = SWP_NOMOVE Or SWP_NOSIZE Or SWP_SHOWWINDOW Or SWP_NOActivate

    '    '    If Position = vbtopmost Or Position = vbnottopmost Then
    '    '        SetWindowPos(hWnd, Position, 0, 0, 0, 0, wFlags)
    '    '    End If

    '    'End Sub
End Module
