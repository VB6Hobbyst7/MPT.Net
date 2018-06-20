Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.Windows.Controls.Primitives
Imports CSiTester.cLibDataGrid

''' <summary>
''' Contains methods specific to the functions of a form, but not any particular form, such as field entry validation.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibForms

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Data Validation"
    ''' <summary>
    ''' Returns True value if it is numeric. Optionally informs user if text is not numeric.
    ''' </summary>
    ''' <param name="p_value">Text to check.</param>
    ''' <param name="p_isPositive">True if value must be a positive number.</param>
    ''' <param name="p_alertUser">Alert to present to the user if the entry is not numeric.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckValidEntryNumeric(ByVal p_value As String,
                                                    Optional ByVal p_isPositive As Boolean = False,
                                                    Optional ByVal p_alertUser As String = "") As Boolean
        CheckValidEntryNumeric = True

        'Check that entry is numeric, and if so, if it is positive (if specified)
        If Not IsNumeric(p_value) Then
            CheckValidEntryNumeric = False
        ElseIf p_isPositive And CDbl(p_value) < 0 Then
            CheckValidEntryNumeric = False
        End If

        'Warn user if message is provided
        If Not CheckValidEntryNumeric Then
            If Not String.IsNullOrEmpty(p_alertUser) Then MsgBox(p_alertUser)
        End If

    End Function

    ''' <summary>
    ''' Returns text value if it is an integer. Returns the provided default if it is not. Optionally informs user if text is not an integer or numeric.
    ''' </summary>
    ''' <param name="p_value">Text to check.</param>
    ''' <param name="p_isPositive">True if value must be a positive number.</param>
    ''' <param name="p_alertUserNum">Alert to present to the user if the entry is not numeric.</param>
    ''' <param name="p_alertUserInt">Alert to present to the user if the entry is not an integer.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckValidEntryInteger(ByRef p_value As String,
                                                    Optional ByVal p_isPositive As Boolean = False,
                                                    Optional ByVal p_alertUserNum As String = "",
                                                    Optional ByVal p_alertUserInt As String = "") As Boolean
        Dim passedFirstCheck As Boolean = True

        CheckValidEntryInteger = True

        'Check that entry is numeric
        If CheckValidEntryNumeric(p_value, p_isPositive, p_alertUserNum) Then
            'Check if entry is an integer
            Dim myEntryNumeric As Double = CDbl(p_value)
            If Not Math.Round(myEntryNumeric) - myEntryNumeric = 0 Then
                CheckValidEntryInteger = False
            End If
        Else
            CheckValidEntryInteger = False
            passedFirstCheck = False
        End If

        'Warn user if message is provided
        If Not CheckValidEntryInteger And passedFirstCheck Then
            If Not String.IsNullOrEmpty(p_alertUserInt) Then MsgBox(p_alertUserInt)
        End If
    End Function

    ''' <summary>
    ''' Returns text value if it is an integer. Returns the provided default if it is not. Optionally informs user if text if various criteria fail to be met.
    ''' </summary>
    ''' <param name="p_value">Text to check.</param>
    ''' <param name="p_min">Minimum value allowed for text.</param>
    ''' <param name="p_max">Maximum value allowed for text.</param>
    ''' <param name="p_isInteger">Specifies if numeric number should also be confirmed as an integer.</param>
    ''' <param name="p_alertUserNum">Alert to present to the user if the entry is not numeric.</param>
    ''' <param name="p_alertUserInt">Alert to present to the user if the entry is not an integer.</param>
    ''' <param name="p_alertUserRangeMaxMin">Alert to present to the user if the entry is not within the specified range. 
    ''' If myAlertRangeMin is specified, this alert is only used if the entry is above the specified maximum.</param>
    ''' <param name="p_alertUserRangeMin">Alert to present to the user if the entry is below the specified minimum.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CheckValidEntryRange(ByRef p_value As String,
                                                  ByVal p_min As Double,
                                                  ByVal p_max As Double,
                                                  Optional ByVal p_isInteger As Boolean = False,
                                                  Optional ByVal p_alertUserRangeMaxMin As String = "",
                                                  Optional ByVal p_alertUserNum As String = "",
                                                  Optional ByVal p_alertUserInt As String = "",
                                                  Optional ByVal p_alertUserRangeMin As String = "") As Boolean
        Dim passedFirstCheck As Boolean = True

        CheckValidEntryRange = True

        If String.IsNullOrEmpty(p_alertUserRangeMin) Then p_alertUserRangeMin = p_alertUserRangeMaxMin

        'Check if entry is an integer, if necessary
        If p_isInteger Then
            If Not CheckValidEntryInteger(p_value, , p_alertUserNum, p_alertUserInt) Then
                CheckValidEntryRange = False
                passedFirstCheck = False
            End If
        Else        'Check that entry is numerical
            If Not CheckValidEntryNumeric(p_value, , p_alertUserNum) Then
                CheckValidEntryRange = False
                passedFirstCheck = False
            End If
        End If

        'Check range criteria
        If passedFirstCheck Then
            If CDbl(p_value) < p_min Then
                CheckValidEntryRange = False
                If Not String.IsNullOrEmpty(p_alertUserRangeMaxMin) Then MsgBox(p_alertUserRangeMin)
            ElseIf CDbl(p_value) > p_max Then
                CheckValidEntryRange = False
                If Not String.IsNullOrEmpty(p_alertUserRangeMaxMin) Then MsgBox(p_alertUserRangeMaxMin)
            End If
        End If

    End Function


    ''' <summary>
    ''' Checks whether the text in a textbox matches an entry in a listbox.
    ''' </summary>
    ''' <param name="p_listBox">Name of the listbox to search.</param>
    ''' <param name="p_textBox">Name of the textbox that contains the entry to search fo.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function checkEntryExistsListBox(ByRef p_listBox As ListBox,
                                                   ByRef p_textBox As TextBox) As Boolean
        Dim i As Integer
        checkEntryExistsListBox = False

        For i = 0 To p_listBox.Items.Count - 1  'Selects initial cell
            If p_listBox.Items(i) Is p_textBox.Text Then
                checkEntryExistsListBox = True
                MsgBox("Entry already exists.")
                Exit For
            End If
        Next i
    End Function

    ''' <summary>
    ''' Checks if a textbox entry e-mail is valid by searching for the "@" character, and then whether a "." character follows.
    ''' </summary>
    ''' <param name="p_textBox">Textbox object to check entries from.</param>
    ''' <returns>True: e-mail is valid. False: e-mail is not valid.</returns>
    ''' <remarks></remarks>
    Public Shared Function checkValidEmail(ByRef p_textBox As TextBox) As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim strEmail As String
        Dim strMatch As Boolean

        strMatch = False
        strEmail = p_textBox.Text

        For i = 1 To Len(strEmail)
            If Mid(strEmail, i, 1) = "@" Then
                strMatch = True
            End If
            If strMatch = True Then
                For j = i To Len(strEmail)
                    If Not Mid(strEmail, j, 1) = "." Then
                        strMatch = False
                    Else
                        strMatch = True
                        checkValidEmail = True
                        Exit Function
                    End If
                Next j
            End If
        Next i
        MsgBox("Please enter a valid e-mail address.")
        checkValidEmail = False
    End Function

#End Region

#Region "Combo Box Methods"
    ''' <summary>
    ''' Populates combo boxes for a date entry with the correct list of years, and numeric months and days. 
    ''' Also automatically sets the drop boxes to the current date and time.
    ''' </summary>
    ''' <param name="p_cmbBxYear">Combo Box for selecting the year.</param>
    ''' <param name="p_cmbBxMonth">Combo Box for selecting the month (as number).</param>
    ''' <param name="p_cmbBxDay">Combo Box for selecting the day (as number).</param>
    ''' <param name="p_setCurrentTime">Optional: If true, combo boxes will automatically be set to select the current year, month and day, as applicable.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetDateComboBoxes(Optional ByRef p_cmbBxYear As ComboBox = Nothing,
                                        Optional ByRef p_cmbBxMonth As ComboBox = Nothing,
                                        Optional ByRef p_cmbBxDay As ComboBox = Nothing,
                                        Optional p_setCurrentTime As Boolean = False)
        Dim yearCurrent As Integer = Year(DateTime.Now)
        Dim monthCurrent As Integer = Month(DateTime.Now)
        Dim dayCurrent As Integer = Day(DateTime.Now)

        'Add Dates
        p_cmbBxYear.Items.Add(yearCurrent)

        'Year
        If p_cmbBxYear IsNot Nothing Then
            'Fill combo box
            For i = 1 To 50
                p_cmbBxYear.Items.Add(yearCurrent - i)
            Next

            'Set combo box
            If p_setCurrentTime Then p_cmbBxYear.SelectedItem = yearCurrent
        End If

        'Month
        If p_cmbBxMonth IsNot Nothing Then
            'Fill combo box
            For i = 1 To 12
                p_cmbBxMonth.Items.Add(i)
            Next

            'Set combo box
            If p_setCurrentTime Then p_cmbBxMonth.SelectedItem = monthCurrent
        End If

        'Day
        If p_cmbBxDay IsNot Nothing Then
            If Not p_cmbBxDay.Items.Count > 0 Then
                'Fill combo box
                For i = 1 To 31
                    p_cmbBxDay.Items.Add(i)
                Next
            End If

            'Set combo box
            If p_setCurrentTime Then p_cmbBxDay.SelectedItem = dayCurrent
        End If





    End Sub

    ''' <summary>
    ''' Updates the 'days' combo box list depending on the month selected.
    ''' </summary>
    ''' <param name="p_monthValue">The month (as number), to be referenced.</param>
    ''' <param name="p_cmbBxDay">Combo Box for selecting the day (as number), to be altered.</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateDayComboBox(ByVal p_monthValue As Integer,
                                        ByRef p_cmbBxDay As ComboBox)
        Dim maxDay As Integer

        'Resets the 'day' combo box list
        p_cmbBxDay.Items.Clear()

        'Sets new 'day' limit depending on the month
        If p_monthValue = 2 Then
            maxDay = 28
        ElseIf p_monthValue = 9 Or p_monthValue = 4 Or p_monthValue = 6 Or p_monthValue = 11 Then
            maxDay = 30
        Else
            maxDay = 31
        End If

        For i = 1 To maxDay
            p_cmbBxDay.Items.Add(i)
        Next
    End Sub

    ''' <summary>
    ''' Gets the selected index for a combo box by matching the list item to its position in the list of list items. Returns the 0-based index. If no list item or list are provided, returns 0.
    ''' </summary>
    ''' <param name="p_item">Item to get selected index for.</param>
    ''' <param name="p_items">List to query for the selected index.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSelectedIndex(Optional ByVal p_item As String = "",
                                            Optional ByVal p_items As ObservableCollection(Of String) = Nothing) As Integer
        Dim i As Integer = 0

        If String.IsNullOrEmpty(p_item) Then Return 0

        If p_items IsNot Nothing Then
            For Each listItem As String In p_items
                If listItem = p_item Then Exit For
                i += 1
            Next
        End If

        If i = p_items.Count Then  'No match was found
            Return 0
        Else                            'Match was found
            Return i
        End If
    End Function

    ''' <summary>
    ''' Populates the combo box and sets the selected item.
    ''' </summary>
    ''' <param name="p_comboBox">Name of the combo box to be filled.</param>
    ''' <param name="p_items">List of items to add to the combo box.</param>
    ''' <param name="p_selection">Item to select in the combo box.</param>
    ''' <param name="p_ifEmptyCollapse">For handling a drop box where the list is empty. 
    ''' True: Control visibility will be collapsed. 
    ''' False: Control will be visible but disabled.</param>
    ''' <param name="p_caseSensitive">True: Selection will consider case.
    ''' False: Case will be ignored when matching the selection value to those in the list.</param>
    ''' <remarks></remarks>
    Public Shared Sub LoadComboBoxes(ByRef p_comboBox As ComboBox,
                                     ByVal p_items As ObservableCollection(Of String),
                                     ByVal p_selection As String,
                                     ByVal p_ifEmptyCollapse As Boolean,
                                     Optional ByVal p_caseSensitive As Boolean = True)
        Try
            'If no command line parameters are available for the given analysis setting, the control will be hidden
            If p_items.Count = 0 Then
                If p_ifEmptyCollapse Then
                    p_comboBox.Visibility = Windows.Visibility.Collapsed
                Else
                    p_comboBox.IsEnabled = False
                End If
                Exit Sub
            Else
                p_comboBox.Visibility = Windows.Visibility.Visible
            End If

            p_comboBox.ItemsSource = p_items
            SetComboBoxSelection(p_comboBox, p_selection, p_caseSensitive)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Sets the combo box to select the specified list entry, if it exists in the combo box list.
    ''' </summary>
    ''' <param name="p_comboBox">The combo box to be manipulated.</param>
    ''' <param name="p_selection">Item to select in the combo box.</param>
    ''' <param name="p_useIndex">True: Selection is made by using the combo box index, rather than the selected text. 
    ''' This might be desireable as sometimes if the text is the same as other items that occur earlier in the list, an incorrect or no selection might be made.</param>
    ''' <param name="p_caseSensitive">True: Selection will consider case.
    ''' False: Case will be ignored when matching the selection value to those in the list.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetComboBoxSelection(ByRef p_comboBox As ComboBox,
                                           ByVal p_selection As String,
                                           Optional ByVal p_useIndex As Boolean = False,
                                           Optional ByVal p_caseSensitive As Boolean = True)
        Dim i As Integer
        Try
            For i = 0 To p_comboBox.Items.Count - 1   'Selects initial cell
                If (p_comboBox.Items(i).ToString = p_selection OrElse
                    (Not p_caseSensitive AndAlso
                     p_comboBox.Items(i).ToString.ToUpper = p_selection.ToUpper)) Then
                    If p_useIndex Then
                        p_comboBox.SelectedIndex = i
                    Else
                        p_comboBox.Text = p_selection
                    End If
                    Exit For
                End If
            Next i
        Catch ex As Exception
            'If Not suppressExStates Then
            '    'myLogger
            '    MsgBox(ex.Message)
            '    MsgBox(ex.StackTrace)
            'End If
        End Try
    End Sub
#End Region

#Region "DataGrid Methods"
    ''' <summary>
    ''' Changes the maximum displayed height of the DataGrid provided based on the dimensions of the other objects provided so that scrollbars appear where appropriate.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to resize.</param>
    ''' <param name="p_gridMain">Main grid system for the form.</param>
    ''' <param name="p_dgRow">Grid row that contains the DataGrid.</param>
    ''' <param name="p_border">Border surrounding the DataGrid, if present.</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateDataGridHeight(ByRef p_dataGrid As DataGrid,
                                            ByVal p_gridMain As Grid,
                                            ByVal p_dgRow As RowDefinition,
                                            Optional p_border As Border = Nothing)
        Dim totalMargin As Double = 0
        totalMargin += p_dataGrid.Margin.Bottom
        totalMargin += SystemParameters.HorizontalScrollBarHeight
        If p_border IsNot Nothing Then totalMargin += p_border.BorderThickness.Bottom
        totalMargin += p_gridMain.Margin.Bottom

        If p_dgRow.ActualHeight > totalMargin Then
            p_dataGrid.MaxHeight = p_dgRow.ActualHeight - totalMargin
        End If
    End Sub
#End Region

#Region "Sizing Methods"

    'Friend Function GetCurrentScreen(ByVal p_form As Window) As System.Windows.Forms.Screen
    '    Dim currentScreen As System.Windows.Forms.Screen

    '    currentScreen = System.Windows.Forms.Screen.FromHandle(New System.Windows.Interop.WindowInteropHelper(p_form).Handle)

    '    Return currentScreen
    'End Function

    'Function GetScreenWidthsTotal() As Double
    '    Dim screens As System.Windows.Forms.Screen() = System.Windows.Forms.Screen.AllScreens
    '    Dim totalScreenWidth As Double = 0

    '    If screens.Count > 1 Then
    '        For Each screen As System.Windows.Forms.Screen In screens
    '            totalScreenWidth += screen.WorkingArea.Size.Width
    '        Next
    '    Else
    '        totalScreenWidth = My.Computer.Screen.WorkingArea.Size.Width
    '    End If

    '    Return totalScreenWidth
    'End Function

    ''' <summary>
    ''' Assigns the minimum/maximum form dimensions for the parameters provided. 
    ''' By default, minimum dimensions are only assigned if they are greater than the current min size (meant for form first-loading).
    ''' By default, maximum dimensions are only assigned if they are less than the current max size (meant for form first-loading).
    ''' </summary>
    ''' <param name="p_form">Form object to size.</param>
    ''' <param name="p_minHeight">Minimum height allowed for the DataGrid.</param>
    ''' <param name="p_minWidth">Minimum width allowed for the DataGrid.</param>
    ''' <param name="p_maxHeight">Maximum height allowed for the DataGrid.</param>
    ''' <param name="p_maxWidth">Maximum width allowed for the DataGrid.</param>
    ''' <param name="p_overwriteAll">If 'True', DataGrid extents are assigned the provided parameters regardless of the existing sizes. 
    ''' Otherwise, assignments are only made based on default considerations that are expected of forms when they are first loading.</param>
    ''' <param name="p_setSizeToMaximum">If 'True', then the form size will be set to the maximum size possible.</param>
    ''' <remarks></remarks>
    Public Shared Sub AssignFormDimensions(ByRef p_form As Window,
                                     Optional ByVal p_minHeight As Double = -1,
                                     Optional ByVal p_minWidth As Double = -1,
                                     Optional ByVal p_maxHeight As Double = -1,
                                     Optional ByVal p_maxWidth As Double = -1,
                                     Optional ByVal p_overwriteAll As Boolean = False,
                                     Optional ByVal p_setSizeToMaximum As Boolean = False)
        Dim currentScreen As System.Windows.Forms.Screen = System.Windows.Forms.Screen.FromHandle(New System.Windows.Interop.WindowInteropHelper(p_form).Handle)
        Dim screenWidth As Double = My.Computer.Screen.WorkingArea.Size.Width
        Dim totalScreenWidth As Double = 0
        Dim screenHeight As Double = My.Computer.Screen.WorkingArea.Size.Height
        Dim screens As System.Windows.Forms.Screen() = System.Windows.Forms.Screen.AllScreens

        If screens.Count > 1 Then
            For Each screen As System.Windows.Forms.Screen In screens
                totalScreenWidth += screen.WorkingArea.Size.Width
            Next
        Else
            totalScreenWidth = screenWidth
        End If

        With p_form
            If p_overwriteAll Then
                If p_minHeight > 0 Then .MinHeight = p_minHeight
                If p_minWidth > 0 Then .MinWidth = p_minWidth
                If p_maxHeight > 0 Then .MaxHeight = p_maxHeight
                If p_maxWidth > 0 Then .MaxWidth = p_maxWidth
            Else
                'Minimum dimensions
                If (p_minHeight > 0 AndAlso
                    .MinHeight < p_minHeight) Then .MinHeight = p_minHeight

                If (p_minWidth > 0 AndAlso
                    .MinWidth < p_minWidth) Then .MinWidth = p_minWidth

                'Maximum dimensions
                If (p_maxHeight > 0 AndAlso
                    p_maxHeight < screenHeight) Then

                    .MaxHeight = p_maxHeight
                Else
                    .MaxHeight = screenHeight
                End If

                If (p_maxWidth > 0 AndAlso
                    p_maxWidth < totalScreenWidth) Then

                    .MaxWidth = p_maxWidth
                Else
                    .MaxWidth = totalScreenWidth
                End If
            End If
        End With

        If p_setSizeToMaximum Then SetSizeToMaximum(p_form)
    End Sub

    ''' <summary>
    ''' Sets the form size to the maximum sizes determined for the form, bounded by screen dimensions.
    ''' </summary>
    ''' <param name="p_form">Form object to size.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetSizeToMaximum(ByRef p_form As Window)
        Dim screenHeight As Double = My.Computer.Screen.WorkingArea.Size.Height
        Dim screenWidth As Double = My.Computer.Screen.WorkingArea.Size.Width

        With p_form
            .SizeToContent = SizeToContent.Manual
            If .MaxHeight < screenHeight Then
                .Height = .MaxHeight
            Else
                .Height = screenHeight
            End If
            If .MaxWidth < screenWidth Then
                .Width = .MaxWidth
            Else
                .Width = screenWidth
            End If
        End With
    End Sub

    ''' <summary>
    ''' Assigns the minimum/maximum DataGrid dimensions for the parameters provided. 
    ''' By default, minimum dimensions are only assigned if they are greater than the current min size (meant for form first-loading).
    ''' By default, maximum dimensions are only assigned if they are less than the current max size (meant for form first-loading).
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to size.</param>
    ''' <param name="p_minHeight">Minimum height allowed for the DataGrid.</param>
    ''' <param name="p_minWidth">Minimum width allowed for the DataGrid.</param>
    ''' <param name="p_maxHeight">Maximum height allowed for the DataGrid.</param>
    ''' <param name="p_maxWidth">Maximum width allowed for the DataGrid.</param>
    ''' <param name="p_overwriteAll">If 'True', DataGrid extents are assigned the provided parameters regardless of the existing sizes. 
    ''' Otherwise, assignments are only made based on default considerations that are expected of forms when they are first loading.</param>
    ''' <remarks></remarks>
    Public Shared Sub AssignDGDimensions(ByRef p_dataGrid As DataGrid,
                                  Optional ByVal p_minHeight As Double = -1,
                                  Optional ByVal p_minWidth As Double = -1,
                                  Optional ByVal p_maxHeight As Double = -1,
                                  Optional ByVal p_maxWidth As Double = -1,
                                  Optional ByVal p_overwriteAll As Boolean = False)
        With p_dataGrid
            If p_overwriteAll Then
                If p_minHeight > 0 Then .MinHeight = p_minHeight
                If p_minWidth > 0 Then .MinWidth = p_minWidth
                If p_maxHeight > 0 Then .MaxHeight = p_maxHeight
                If p_maxWidth > 0 Then .MaxWidth = p_maxWidth
            Else
                'Minimum dimensions
                If (p_minHeight > 0 AndAlso
                    .MinHeight < p_minHeight) Then .MinHeight = p_minHeight

                If (p_minWidth > 0 AndAlso
                    .MinWidth < p_minWidth) Then .MinWidth = p_minWidth

                'Maximum dimensions
                If (p_maxHeight > 0 AndAlso
                    .MaxHeight > p_maxHeight) Then .MaxHeight = p_minHeight

                If (p_maxWidth > 0 AndAlso
                    .MaxWidth > p_maxWidth) Then .MaxWidth = p_minWidth
            End If
        End With
    End Sub

    '=== Layout Sizes
    ''' <summary>
    ''' Gets the minimum height of the form based on the standard included elements. 
    ''' This includes the form outside border.
    ''' </summary>
    ''' <param name="p_form">Form object to get the height og.</param>
    ''' <param name="p_grpBx">Form main groupbox.</param>
    ''' <param name="p_grid">Form main grid.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFormHeightMinElements(ByVal p_form As Window,
                                          ByVal p_grpBx As GroupBox,
                                          ByVal p_grid As Grid) As Double
        'Determine offset of window boundary vs. canvas
        Dim offsetHeight As Double = p_form.ActualHeight - p_grpBx.ActualHeight
        Dim minHeight As Double = 0

        With p_grpBx
            minHeight += .Margin.Top
            minHeight += .Margin.Bottom
            minHeight += .Padding.Top
            minHeight += .Padding.Bottom
            minHeight += .BorderThickness.Top
            minHeight += .BorderThickness.Bottom
        End With
        With p_grid
            minHeight += .Margin.Top
            minHeight += .Margin.Bottom
        End With

        minHeight += offsetHeight

        Return minHeight
    End Function

    ''' <summary>
    ''' Gets the minimum width of the form based on the standard included elements. 
    ''' This includes the form outside border.
    ''' </summary>
    ''' <param name="p_form">Form object to get the height og.</param>
    ''' <param name="p_grpBx">Form main groupbox.</param>
    ''' <param name="p_grid">Form main grid.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFormWidthMinElements(ByVal p_form As Window,
                                            ByVal p_grpBx As GroupBox,
                                            ByVal p_grid As Grid) As Double
        Dim offsetWidth As Double = p_form.ActualWidth - p_grpBx.ActualWidth
        Dim minWidth As Double = 0

        With p_grpBx
            minWidth += .Margin.Left
            minWidth += .Margin.Right
            minWidth += .Padding.Left
            minWidth += .Padding.Right
            minWidth += .BorderThickness.Left
            minWidth += .BorderThickness.Right
        End With
        With p_grid
            minWidth += .Margin.Left
            minWidth += .Margin.Right
        End With

        minWidth += offsetWidth

        Return minWidth
    End Function

    '=== Control Sizes
    ''' <summary>
    ''' Gets the total outside height of the list of grid rows provided. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_gridRows">List of grid rows to total into one height dimension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGridHeights(ByVal p_gridRows As List(Of RowDefinition)) As Double
        Dim gridHeights As Double = 0

        For Each gridRow As RowDefinition In p_gridRows
            gridHeights += gridRow.ActualHeight
        Next

        Return gridHeights
    End Function

    ''' <summary>
    ''' Gets the total outside width of the list of grid columns provided. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_gridColumns">List of grid columns to total into one width dimension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGridWidths(ByVal p_gridColumns As List(Of ColumnDefinition)) As Double
        Dim gridWidths As Double = 0

        For Each gridColumn As ColumnDefinition In p_gridColumns
            gridWidths += gridColumn.ActualWidth
        Next

        Return gridWidths
    End Function

    ''' <summary>
    ''' Gets the total outside height of the list of buttons provided. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_buttons">List of buttons to total into one height dimension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtonHeights(ByVal p_buttons As List(Of Button)) As Double
        Dim minHeight As Double = 0

        For Each button In p_buttons
            With button
                minHeight += .ActualHeight
                minHeight += .Margin.Top
                minHeight += .Margin.Bottom
                minHeight += .Padding.Top
                minHeight += .Padding.Bottom
            End With
        Next

        Return minHeight
    End Function

    ''' <summary>
    ''' Gets the total outside width of the list of grid columns provided. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_buttons">List of buttons to total into one width dimension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetButtonWidths(ByVal p_buttons As List(Of Button)) As Double
        Dim minWidth As Double = 0

        For Each button In p_buttons
            With button
                minWidth += .ActualWidth
                minWidth += .Margin.Left
                minWidth += .Margin.Right
                minWidth += .Padding.Left
                minWidth += .Padding.Right
            End With
        Next

        Return minWidth
    End Function

    ''' <summary>
    ''' Gets the total outside maximum height of the DataGrid provided, including standard margins &amp; borders. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    ''' <param name="p_rows">Rows to consider for the fully displayed DataGrid for max height. 
    ''' If not provided, it is determined from the DataGrid object.
    ''' If one is provided, all rows are assumed to be the same size.</param>
    ''' <param name="p_colHeadersPresenter">Column headers to consider. 
    ''' If not provided, it is determined from the DataGrid object.</param>
    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display height.
    ''' If not provided, it is determined from the DataGrid object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataGridHeightMax(ByVal p_dataGrid As DataGrid,
                                      Optional ByVal p_border As Border = Nothing,
                                      Optional ByVal p_rows As List(Of DataGridRow) = Nothing,
                                      Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
                                      Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
        Dim maxHeight As Double
        Dim rows As New List(Of DataGridRow)
        Dim rowsProvided As Boolean = False
        Dim scrollViewer As ScrollViewer

        'Handle Optional Parameters
        If (p_rows Is Nothing OrElse p_rows.Count = 0) Then
            rowsProvided = False
        Else
            rowsProvided = True
        End If

        If p_scrollViewer Is Nothing Then
            scrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)
        Else
            scrollViewer = p_scrollViewer
        End If

        'Get total min height assuming 1 row was the minimum visible
        maxHeight += GetDataGridHeightMin(p_dataGrid, p_border, -1, p_colHeadersPresenter, p_rows)

        If Not (rows Is Nothing OrElse scrollViewer Is Nothing) Then
            If scrollViewer.CanContentScroll Then
                Dim rowHeight As Double

                For rowIndex = 1 To scrollViewer.ExtentHeight - 1  'First row skipped as it is already included in the min height =
                    rowHeight = 0
                    If rowsProvided Then
                        If p_rows.Count = 1 Then
                            rowHeight += p_rows(0).ActualHeight
                        Else
                            rowHeight += p_rows(CInt(rowIndex)).ActualHeight
                        End If
                    Else
                        rowHeight += GetRowByIndex(p_dataGrid, CInt(rowIndex)).ActualHeight
                    End If
                    maxHeight += rowHeight
                Next

                'Final multiplier of last row height & pixel subtraction is for gap left under row from scrollbar + last row's bottom border
                maxHeight += 0.5 * rowHeight - 1
            Else
                maxHeight += scrollViewer.ExtentHeight
            End If
        End If

        Return maxHeight
    End Function

    ''' <summary>
    ''' Gets the total outside minimum height of the DataGrid provided, including standard margins &amp; borders. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    ''' <param name="p_minDGHeight">Calculated minimum DataGrid height.
    ''' If not provided, it is determined from the DataGrid object.</param>
    ''' <param name="p_colHeadersPresenter">Column headers to consider. 
    ''' If not provided, it is determined from the DataGrid object.</param>
    ''' <param name="p_rows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataGridHeightMin(ByVal p_dataGrid As DataGrid,
                                         Optional ByVal p_border As Border = Nothing,
                                         Optional ByVal p_minDGHeight As Double = -1,
                                         Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
                                         Optional ByVal p_rows As List(Of DataGridRow) = Nothing) As Double
        Dim minHeight As Double
        Dim minDGHeight As Double

        If p_border IsNot Nothing Then
            With p_border
                minHeight += .BorderThickness.Top
                minHeight += .BorderThickness.Bottom
            End With
        End If

        With p_dataGrid
            minHeight += .Margin.Top
            minHeight += .Margin.Bottom
            minHeight += .Padding.Top
            minHeight += .Padding.Bottom
            minHeight += .BorderThickness.Top
            minHeight += .BorderThickness.Bottom

            If p_minDGHeight <= 0 Then
                minDGHeight = DataGridHeightMin(p_dataGrid, p_colHeadersPresenter, , p_rows)
            Else
                minDGHeight = p_minDGHeight
            End If
            minHeight += Math.Max(minDGHeight, p_dataGrid.MinHeight)
        End With

        Return minHeight
    End Function

    'TODO: Reconcile the next 3 functions
    ''' <summary>
    '''  Gets the total outside maximum width of the DataGrid provided, including standard margins &amp; borders. 
    ''' This includes all components of the control size.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to query for the max height.</param>
    ''' <param name="p_border">Border object that is typically included with the DataGrid.</param>
    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display width.
    ''' If not provided, it is determined from the DataGrid object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDataGridWidthMax(ByVal p_dataGrid As DataGrid,
                                        Optional ByVal p_border As Border = Nothing,
                                        Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
        Dim maxWidth As Double = 0
        Dim scrollViewer As ScrollViewer


        If p_scrollViewer Is Nothing Then
            scrollViewer = FindVisualChild(Of ScrollViewer)(p_dataGrid)
        Else
            scrollViewer = p_scrollViewer
        End If

        maxWidth += scrollViewer.ExtentWidth
        maxWidth += SystemParameters.VerticalScrollBarWidth

        maxWidth += GetDataGridBorderElementsWidth(p_dataGrid, p_border)

        Return maxWidth
    End Function

    Public Shared Function GetDataGridBorderElementsWidth(ByVal p_dataGrid As DataGrid,
                                                        Optional ByVal p_border As Border = Nothing) As Double
        Dim gridWidth As Double = 0

        If p_border IsNot Nothing Then
            With p_border
                gridWidth += .BorderThickness.Right
                gridWidth += .BorderThickness.Left
            End With
        End If

        With p_dataGrid
            gridWidth += .Margin.Right
            gridWidth += .Margin.Left
            gridWidth += .Padding.Left
            gridWidth += .Padding.Right
            gridWidth += .BorderThickness.Left
            gridWidth += .BorderThickness.Right
        End With

        Return gridWidth
    End Function

    ''' <summary>
    ''' Determines the maximim width of the DataGrid including border elements and all visible columns.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to determine the maximum width from.</param>
    ''' <param name="p_borders">Border object surrounding DataGrid to include in the width.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CalcDataGridWidthMax(ByRef p_dataGrid As DataGrid,
                            Optional ByVal p_borders As Border = Nothing) As Double
        Dim maxWidthDG As Double = 0

        Dim rowHeaders As DataGridRowHeader = FindVisualChild(Of DataGridRowHeader)(p_dataGrid)
        maxWidthDG += rowHeaders.ActualWidth
        If p_borders IsNot Nothing Then
            With p_borders
                maxWidthDG += .BorderThickness.Left
                maxWidthDG += .BorderThickness.Right
            End With
        End If

        For Each column As DataGridColumn In p_dataGrid.Columns
            If column.Visibility = Windows.Visibility.Visible Then maxWidthDG += column.ActualWidth
        Next

        Return maxWidthDG
    End Function

    ''' <summary>
    ''' Expand the last column of the provided DataGrid to fill the DataGrid if the grid size cannot match the columns. 
    ''' This prevents an empty trailing column from appearing.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to perform the method on.</param>
    ''' <param name="p_minWidth">Minimum width to be allowed for the DataGrid.</param>
    ''' <param name="p_maxWidth">Maximum width to be allowed for the DataGrid.</param>
    ''' <remarks></remarks>
    Public Shared Sub ExpandLastColumnToFit(ByRef p_dataGrid As DataGrid,
                              ByVal p_minWidth As Double,
                              ByVal p_maxWidth As Double)
        Dim columnReverse As DataGridColumn
        If p_maxWidth < p_minWidth Then
            For i = p_dataGrid.Columns.Count - 1 To 0 Step -1
                columnReverse = p_dataGrid.Columns(i)
                If columnReverse.Visibility = Windows.Visibility.Visible Then
                    columnReverse.Width = columnReverse.ActualWidth + (p_minWidth - p_maxWidth)
                    Exit For
                End If
            Next
        End If
    End Sub

#End Region








    'Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As Long) As Long
    'Private Const SWP_NOMOVE = &H2
    'Private Const SWP_NOSIZE = &H1
    'Private Const SWP_SHOWWINDOW = &H40
    'Private Const SWP_NOActivate = &H10
    'Public Enum WindowPos
    '    vbtopmost = -1&
    '    vbnottopmost = -2&
    'End Enum

    'Public Sub SetFormPosition(hWnd As Long, Position As WindowPos)
    '    Const wFlags As Long = SWP_NOMOVE Or SWP_NOSIZE Or SWP_SHOWWINDOW Or SWP_NOActivate

    '    If Position = vbtopmost Or Position = vbnottopmost Then
    '        SetWindowPos(hWnd, Position, 0, 0, 0, 0, wFlags)
    '    End If

    'End Sub
End Class
