Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Data
Imports System.Windows.Controls.Primitives
Imports System.Windows.Threading

Imports MPT.Enums.EnumLibrary
Imports MPT.Forms.DataGridLibrary
Imports MPT.Forms.FormsLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary
Imports MPT.Units

Imports CSiTester.cTableQueryView
Imports CSiTester.cMCResults

''' <summary>
''' This form displays output from CSI products and is used to create table queries and set benchmarks for examples.
''' </summary>
''' <remarks></remarks>
Public Class frmXMLObjectResults
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Enumerations"


#End Region

#Region "Constants"
    ''' <summary>
    ''' Default text to display on the units buttons.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UNITS_BUTTON_CONTENT_DEFAULT As String = "Units"
    ''' <summary>
    ''' Prefix of all of the units button names.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UNITS_BUTTON_NAME As String = "btnUnits"
    ''' <summary>
    ''' The tooltip for the Units button when no units are currently assigned.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UNITS_BUTTON_TT_DEFAULT As String = "Set units for column."

    ''' <summary>
    ''' Name of the formatting template for the cell style to apply to selected rows.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_SELECTED_ROW As String = "dgSelectedRow"
    ''' <summary>
    ''' Name of the formatting template for the cell style to apply to selected cells.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_SELECTED_CELL As String = "dgSelectedCell"
    ''' <summary>
    ''' Name of the formatting template for the cell style to apply to selected benchmarks.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_SELECTED_BM As String = "dgSelectedBM"
    ''' <summary>
    ''' Name of the formatting template for the cell style to apply to result details and the details status cell.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_RESULT_NULL As String = "dgResultNull"
    ''' <summary>
    ''' Name of the formatting template for the cell style to apply to the result details status cell for a complete result.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_RESULT_ADD As String = "dgResultAdd"
    ''' <summary>
    '''  Name of the formatting template for the cell style to apply to the result details status cell for an incomplete result.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DG_RESULT_EDIT As String = "dgResultEdit"

    Private Const TITLE_TABLE_LOAD_ERROR As String = "Table Load Error"
    Private Const PROMPT_TABLE_LOAD_ERROR As String = "Error loading tables. Form cannot be displayed."

#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' Suppresses certain event handler functions while form is first loading.
    ''' </summary>
    ''' <remarks></remarks>
    Private _firstLoad As Boolean

    ''' <summary>
    ''' Suppresses certain event handler functions until a form is fully shown.
    ''' </summary>
    ''' <remarks></remarks>
    Private _shown As Boolean = False

    ''' <summary>
    ''' Triggers actions to reload cell result formatting to all loaded rows when a new table is loaded.
    ''' </summary>
    ''' <remarks></remarks>
    Private _tableLoad As Boolean

    ''' <summary>
    ''' Handles datagrid scrolling customization.
    ''' </summary>
    ''' <remarks></remarks>
    Private _datagridScroller As cDataGridVerticalScroller

    ''' <summary>
    ''' Object that handles the positioning and sizing of the units buttons that are aligned atop the data grid.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataGridHeaderButtonsSizer As New cDataGridHeaderButtonsSizer

    'Units
    ''' <summary>
    ''' Programmatically created buttons for setting the column units.
    ''' </summary>
    ''' <remarks></remarks>
    Private _unitsButtons() As Button
    ''' <summary>
    ''' Collection of units objects that each correspond to a button.
    ''' </summary>
    ''' <remarks></remarks>
    Private _unitsCollectionCurrent As New List(Of cUnitsController)
    ''' <summary>
    ''' Collection of units objects that correspond to a button, grouped for each table.
    ''' </summary>
    ''' <remarks></remarks>
    Private _unitsCollections As New Dictionary(Of String, List(Of cUnitsController))

    ''' <summary>
    ''' Collection of the base units specified in Program Control.
    ''' </summary>
    ''' <remarks></remarks>
    Private _unitsProgramControl As New List(Of cUnitsController)

    'Form size
    ''' <summary>
    ''' Calculated maximum width to allow for the form. Not necessarily assigned to the form size property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _maxWidthForm As Double
    ''' <summary>
    ''' Calculated maximum height to allow for the form. Not necessarily assigned to the form size property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _maxHeightForm As Double
    ''' <summary>
    ''' Calculated minimum width to allow for the form. Not necessarily assigned to the form size property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _minWidthForm As Double
    ''' <summary>
    ''' Calculated minimum height to allow for the form. Not necessarily assigned to the form size property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _minHeightForm As Double
    ''' <summary>
    ''' If 'True', then the form has had its size limitations manually set and cannot be reset. If 'False', the form's size limitations can be set/reset.
    ''' </summary>
    ''' <remarks></remarks>
    Private _formSized As Boolean

    ''' <summary>
    ''' Path to the database file read into the form.
    ''' </summary>
    ''' <remarks></remarks>
    Private _dataSourcePath As String

    ''' <summary>
    ''' Name of the last entry selected in the ComboBox.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastComboBoxItemSelected As String
    ''' <summary>
    ''' If true, an action using the new selection from the ComboBox will attempt to be performed. If false, no action will be taken and the old selected entry will be maintained.
    ''' </summary>
    ''' <remarks></remarks>
    Private _implementNewComboBoxSelection As Boolean

    'DG Other Variables
    ''' <summary>
    ''' Calculated minimum height to allow for the DataGrid. Not necessarily assigned to the DataGrid size property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _minHeightDG As Double
    ''' <summary>
    ''' If 'True' then a sorting operation has started, but the layout has not been updated yet.
    ''' </summary>
    ''' <remarks></remarks>
    Private _updateSorted As Boolean
    ''' <summary>
    ''' List of the DataGridRows loaded after sorting.
    ''' </summary>
    ''' <remarks></remarks>
    Private _rowsLoaded As List(Of DataGridRowEventArgs) = New List(Of DataGridRowEventArgs)
    ''' <summary>
    ''' Button object associated with the RowsLoaded event. Used for calling the event manually.
    ''' </summary>
    ''' <remarks></remarks>
    Private _tempSender As Object

    'DG Current state
    ''' <summary>
    ''' Variable that tracks the current selected DataGrid row. This is needed if any operation is done via button click, as the current row is lost through this action.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentDRVSelected As DataRowView
    ''' <summary>
    ''' Current cell object selected in the DataGrid.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentCellSelected As DataGridCell
    ''' <summary>
    ''' Variable that tracks the current selected column. This is needed if any operation is done via button click, as the current column is lost through this action.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentDGColIndexSelected As Integer
    ''' <summary>
    ''' 'True' if the current selected cell is a selected benchmark.
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsCurrentCellSelectedBenchmarkSelected As Boolean
    ''' <summary>
    ''' The current query key that corresponds to the row viewed at the highest point in the DataGridView. Changes when sorting and scrolling.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentQueryKey As Integer

    ''' <summary>
    ''' Contains the x- &amp; y- coordinates of the cursor within the DataGrid region at any given time that the cursor is over that region.
    ''' This region is the DataGrid area minus the areas containing the row &amp; column headers and the scrollbars
    ''' </summary>
    ''' <remarks></remarks>
    Private _cursorPosition As Point
    ''' <summary>
    ''' List of X- &amp; Y- coordinates for the top-left and bottom-right corners of the DataGrid area that contains the cells.
    ''' Accounts for headers &amp; scrollbars.
    ''' </summary>
    ''' <remarks></remarks>
    Private _activeDataGridArea As List(Of Point) = New List(Of Point)

    'DG Prior state
    ''' <summary>
    ''' Last datagrid cell selected. Used for removing the formatting upon selection change.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastCellSelected As DataGridCell
    ''' <summary>
    ''' 'True' if the last selected cell was a selected benchmark.
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsLastCellSelectedBenchmarkSelected As Boolean

    ''' <summary>
    ''' If 'True', then the BM in a given row has been changed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _isCurrentCellChangedInRow As Boolean
    ''' <summary>
    ''' If 'True', then the last selected benchmark in the last-selected row was changed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastBenchmarkChangedInOtherRow As Boolean

    ''' <summary>
    ''' The header value of the last benchmark shown in the row before the benchmark was changed.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastBenchmarkHeader As String
    ''' <summary>
    ''' Last cell object selected in the DataGrid that was a selected benchmark.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastBenchmarkCell As DataGridCell
    ''' <summary>
    ''' Textblock style for the Query &amp; Benchmark classifications.
    ''' </summary>
    ''' <remarks></remarks>
    Private _queryBMStyle As New Style()

    ''' <summary>
    ''' Collection of all query view objects that have already been generated during this form session. 
    ''' This allows storing the tables in memory instead of regenerating them each time a new table is loaded.
    ''' </summary>
    ''' <remarks></remarks>
    Private _resultsDataTables As cTableQueryViews = New cTableQueryViews

    'Incomplete Results Form
    ''' <summary>
    ''' Form that displays all current incomplete results. Meant to be modeless.
    ''' </summary>
    ''' <remarks></remarks>
    Private _myFrmSummaryResults As frmXMLObjectResultsSummary
    ''' <summary>
    ''' If true, then the summary form is displaying all results, and not just the incomplete ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private _myFrmSummaryResultsShowTotal As Boolean
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Collection of all results for a given example, supplied by the model control XML class. Temporary for the session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property allResults As ObservableCollection(Of cMCResult) = New ObservableCollection(Of cMCResult)
    ''' <summary>
    ''' Collection of all results for a given example, supplied by the model control XML class. The original.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property allResultsSave As ObservableCollection(Of cMCResult) = New ObservableCollection(Of cMCResult)

    ''' <summary>
    ''' List of the names of all of the tables in the referenced exported tables file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property tableNames As List(Of String) = New List(Of String)
    ''' <summary>
    ''' List of cTableQueries objects, which store the names, queries, and benchmarks associated with each table.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property tableQueriesCollection As cTableQueriesCollection = New cTableQueriesCollection
    ''' <summary>
    ''' Class with the current custom dataTable object being viewed in the datagrid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property resultsDataTable As cTableQueryView

    ''' <summary>
    ''' Property that specifies whether or not the form was canceled out, or if new values were potentially saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property formCancel As Boolean = True
#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Temp. Example file for displaying the table.
        _dataSourcePath = "C:\Backups\ETABS v13_1_5_Run 2\Models\Analysis Examples\Example 01.mdb"

        InitializeData()
    End Sub

    ''' <summary>
    ''' Generates a form for adding benchmark results to the model control XML class provided.
    ''' </summary>
    ''' <param name="p_results">Collection of classes that store the results data.</param>
    ''' <param name="p_dataSource">Path to the tables file to be read for displaying the data from which to establish queries and benchmarks.</param>
    ''' <param name="p_units">A string list of the three unit values used in a CSi program, suck as "kip, inch, F".</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_results As ObservableCollection(Of cMCResult),
                   ByVal p_dataSource As String,
                   Optional ByVal p_units As List(Of cUnitsController) = Nothing)
        If p_results Is Nothing Then Throw New ArgumentException("The supplied results collection has not been initialized")
        If Not IO.File.Exists(p_dataSource) Then Throw New ArgumentException("The supplied data source is invalid: " & Environment.NewLine & p_dataSource)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        formCancel = True
        _implementNewComboBoxSelection = True
        If p_units IsNot Nothing Then _unitsProgramControl = p_units


        _dataSourcePath = p_dataSource
        InitializeData(p_results)

        ' Datagrid height is set to avoid memory leaks when loading extremely long lists.
        ' The height chosen is slightly greater than the maximum likely size that the form may use to ensure:
        '   1. The form doesn't load too small.
        '   2. More memory than necessary isn't taken by creating an extremely large DG that is mostly hidden.
        dgAccessTable.Height = My.Computer.Screen.WorkingArea.Height
    End Sub

    ''' <summary>
    ''' Initializes lists, classes, and other data related to the form.
    ''' </summary>
    ''' <param name="p_results">Optional: Collection of classes containing results data.</param>
    ''' <remarks></remarks>
    Private Sub InitializeData(Optional ByVal p_results As ObservableCollection(Of cMCResult) = Nothing)
        Try
            _firstLoad = True
            _maxWidthForm = 0
            _maxHeightForm = 0

            Dim dtController As New cDataTableController
            tableNames = dtController.ListAllTables(_dataSourcePath)

            'Generate list of table classes
            InitializeResultsCollections(p_results)

            InitializeDataGrid()

            InitializeDateComboBoxes()

            _firstLoad = False
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the saved and current collections of result objects to work with. 
    ''' Also sets up the collection of cTableQueries classes that is based on these collections.
    ''' </summary>
    ''' <param name="p_results">Optional: Collection of classes containing results data. If not speciried, the initialized collection will be empty.</param>
    ''' <remarks></remarks>
    Private Sub InitializeResultsCollections(Optional ByVal p_results As ObservableCollection(Of cMCResult) = Nothing)
        Try
            allResults = New ObservableCollection(Of cMCResult)

            If p_results Is Nothing Then Exit Sub

            'Assign results object collection to saved repository
            allResultsSave = p_results

            'Copy results object collection to a local copy
            For Each result As cMCResult In p_results
                allResults.Add(CType(result.Clone, cMCResult))
            Next

            'Create collections grouped by table
            For Each tableName As String In tableNames
                tableQueriesCollection.Add(tableName, allResults.ToList())
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new dataTable object and binds it to the dataGrid. If the creation of the new dataTable fails, the original dataTable is retained.
    ''' </summary>
    ''' <param name="p_tableName">Optional: Table name by which to create a new dataTable object. If none is specified, the first table in the list of tables is used.</param>
    ''' <remarks></remarks>
    Private Sub InitializeDataGrid(Optional ByVal p_tableName As String = "")
        Try
            Dim resultsDataTableTemp As cTableQueryView = Nothing
            _rowsLoaded.Clear()

            'Fetch existing view, or create a new one if one doesn't already exist
            If CanCheckCachedTables(p_tableName) Then resultsDataTableTemp = _resultsDataTables(p_tableName)

            If (resultsDataTableTemp Is Nothing AndAlso
                CanInitializeFirstDefaultTable(p_tableName)) Then resultsDataTableTemp = CreateNewTableQueryView(tableNames(TablesAutoFirstIndex()))

            If resultsDataTableTemp Is Nothing Then resultsDataTableTemp = CreateNewTableQueryView(p_tableName)
            If resultsDataTableTemp Is Nothing Then
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Error),
                                                            PROMPT_TABLE_LOAD_ERROR,
                                                            TITLE_TABLE_LOAD_ERROR))
                Me.Close()
            End If

            resultsDataTable = resultsDataTableTemp

            dgAccessTable.ItemsSource = resultsDataTable.exportedSetTable.DefaultView
            _datagridScroller = New cDataGridVerticalScroller(dgAccessTable)

            ' Set form to record the rows loaded for later formatting if there are results to format
            If resultsDataTable.tableQueriesTotal.Results.Count > 0 Then _tableLoad = True

            dgAccessTable.UpdateLayout()
            SetDataGridCellsExtents(dgAccessTable)

            InitializeUnitsButtons()

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' True: The cached table objects can be checked for the specified table.
    ''' </summary>
    ''' <param name="p_tableName">Table name by which to create a new dataTable object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanCheckCachedTables(ByVal p_tableName As String) As Boolean
        Return (_resultsDataTables IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(p_tableName))
    End Function

    Private Function CanInitializeFirstDefaultTable(ByVal p_tableName As String) As Boolean
        Return (String.IsNullOrEmpty(p_tableName) OrElse
                tableNames.Count = 0 OrElse
                tableNames.IndexOf(p_tableName) < 0)
    End Function


    Private Sub InitializeUnitsButtons()
        InitializeDataGridHeaderButtons()
        _dataGridHeaderButtonsSizer = New cDataGridHeaderButtonsSizer(dgAccessTable,
                                                                      gridUnitButtonsBound,
                                                                      _unitsButtons,
                                                                      resultsDataTable,
                                                                      p_scrollPositionCurrent:=0)
    End Sub

    ''' <summary>
    ''' Creates the set of buttons for setting units atop each of the datagrid columns for the original table exported from the CSi program.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDataGridHeaderButtons()
        Dim numberOfButtons As Integer = GetNumberOfButtons()

        ' Generate controls
        GenerateNewUnitButtonsGrid(numberOfButtons)
        GenerateUnitsButtonsWithInitialProperties(numberOfButtons)

        ' Generate list of units classes that correspond to the buttons
        InitializeUnitsObjects()
        FillUnitsFromResultsForCurrentTable()
        UpdateAllButtonsFromUnits(numberOfButtons)
    End Sub

    Private Function GetNumberOfButtons() As Integer
        Return resultsDataTable.columnUnits.columnHeaders.Count - 1
    End Function


    Private Sub GenerateNewUnitButtonsGrid(ByVal p_numberOfButtons As Integer)
        Me.gridUnitButtonsBound.Children.Clear()
        Me.gridUnitButtonsBound.ColumnDefinitions.Clear()

        For counter As Integer = 0 To p_numberOfButtons
            Dim newColumn As New ColumnDefinition
            Me.gridUnitButtonsBound.ColumnDefinitions.Add(newColumn)
        Next
    End Sub

    Private Sub GenerateUnitsButtonsWithInitialProperties(ByVal p_numberOfButtons As Integer)
        ReDim _unitsButtons(p_numberOfButtons)

        For counter As Integer = 0 To p_numberOfButtons
            Dim newButton As New Button

            With newButton
                .Content = UNITS_BUTTON_CONTENT_DEFAULT
                .Name = UNITS_BUTTON_NAME & counter
                '.Width = 50
                .Height = 24
                .HorizontalAlignment = Windows.HorizontalAlignment.Left
                .VerticalAlignment = Windows.VerticalAlignment.Top
                .Margin = New System.Windows.Thickness(0, 5, 0, 0)
                .ToolTip = UNITS_BUTTON_TT_DEFAULT
            End With

            AddHandler newButton.Click, AddressOf All_Units_Buttons_Clicked

            'Assign the buttons to a containing grid and to the list
            Grid.SetColumn(newButton, counter)
            Me.gridUnitButtonsBound.Children.Add(newButton)

            _unitsButtons(counter) = newButton
        Next
    End Sub


    ''' <summary>
    ''' Initializes the units objects in a collection that is in sync with the auto-generated buttons.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeUnitsObjects(Optional ByVal p_tableName As String = "")
        If String.IsNullOrEmpty(p_tableName) Then p_tableName = resultsDataTable.exportedSetTable.TableName

        If Not _unitsCollections.Keys.Contains(p_tableName) Then
            Dim newUnits As New List(Of cUnitsController)
            For Each btnUnit As Button In _unitsButtons
                newUnits.Add(New cUnitsController)
            Next
            _unitsCollections.Add(p_tableName, newUnits)
        End If

        _unitsCollectionCurrent = _unitsCollections(p_tableName)
    End Sub

    ''' <summary>
    ''' Creates a new table query view object based on the table name provided.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table associated with the view.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateNewTableQueryView(ByVal p_tableName As String) As cTableQueryView
        Dim loadError As Boolean
        Dim resultsDataTableTemp As cTableQueryView = Nothing

        resultsDataTableTemp = New cTableQueryView(_dataSourcePath, tableQueriesCollection, p_tableName, loadError)
        If loadError Then Me.Close()

        Return resultsDataTableTemp
    End Function

    ''' <summary>
    ''' Gets the minimum top-left coordinate and maximum bottom-right coordinate of the cells in the DataGrid, accounting for headers and the scrollbars.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use.</param>
    ''' <remarks></remarks>
    Private Sub SetDataGridCellsExtents(ByVal p_dataGrid As DataGrid)
        _activeDataGridArea.Clear()

        _activeDataGridArea.Add(GetDataGridCellExtentHeaders(p_dataGrid))

        _activeDataGridArea.Add(GetDataGridCellExtentScrollbars(p_dataGrid))
    End Sub

    ''' <summary>
    ''' Sets up the combo boxes in the form.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDateComboBoxes()
        cmbBxTablesList.ItemsSource = tableNames
        cmbBxTablesList.SelectedIndex = TablesAutoFirstIndex()
        _lastComboBoxItemSelected = cmbBxTablesList.SelectedValue.ToString
    End Sub

    ''' <summary>
    '''  Determines the first table index to use for viewing, based on the following criteria:
    '''  1. It should be the first table in the list of tables matching a table used in existing results.
    '''  2. If no results currently exist, it should be 1. 
    '''      That is the first table in the list that is not [Program Control], which is always at index 0.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TablesAutoFirstIndex() As Integer
        If allResultsSave.Count > 0 Then
            For i = 0 To tableNames.Count - 1
                If allResultsSave(0).tableName = tableNames(i) Then Return i
            Next
        Else
            For i = 0 To tableNames.Count - 1
                If Not tableNames(i) = "Program Control" Then Return i
            Next
        End If
        Return Math.Min(tableNames.Count - 1, 1)
    End Function

#End Region

#Region "Initialization: DataGrid - Column Formatting"
    ''' <summary>
    ''' Assigns column customization to the autogenerated columns, such as styles, and hiding certain columns.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dgAccessTable.AutoGeneratingColumn
        Try
            Dim currentColumn As DataGridTextColumn
            Try
                currentColumn = CType(e.Column, DataGridTextColumn)
            Catch ex As Exception
                currentColumn = New DataGridTextColumn
            End Try

            Dim currentHeader As String = currentColumn.Header.ToString
            Dim colRegion As eDataGridRegion = eDataGridRegion.importedDBTable

            colRegion = resultsDataTable.ColumnRegionByHeader(currentHeader)

            'TODO: Hide columns once debugging is finished
            Select Case colRegion
                Case eDataGridRegion.importedDBTable
                    SetQueryBMStyles(currentColumn, currentHeader)
                Case eDataGridRegion.resultsStatus
                    SetResultsDetailsStatusStyle(currentColumn, currentHeader)
                Case eDataGridRegion.resultsDetails
                    SetResultsDetailsStyle(currentColumn, currentHeader)
                Case eDataGridRegion.keyQuery
                    currentColumn.Visibility = Windows.Visibility.Collapsed
                    Exit Sub
                Case eDataGridRegion.keyBenchmark
                    currentColumn.Visibility = Windows.Visibility.Collapsed
                    Exit Sub
                Case eDataGridRegion.queryStatus
                    currentColumn.Visibility = Windows.Visibility.Collapsed
                    Exit Sub
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the styles for the query/lookup part of the table.
    ''' </summary>
    ''' <param name="p_Column">Column object that is to have the style assigned.</param>
    ''' <param name="p_Header">Header that corresponds to the column.</param>
    ''' <remarks></remarks>
    Private Sub SetQueryBMStyles(ByRef p_column As DataGridTextColumn,
                                 ByVal p_header As String)
        _queryBMStyle = New Style()
        Dim triggerQueryPartial As New DataTrigger()
        Dim triggerQueryUnique As New DataTrigger()
        Dim triggerBMValueSelected As New DataTrigger()
        Dim triggerBMValue As New DataTrigger()
        Dim triggerBMValueIncomplete As New DataTrigger()
        Dim setterTextBlockVerticalAlignment As New Setter()
        Dim setterBMValueSelectedFill As New Setter()
        Dim setterBMValueIncompleteForegroundColor As New Setter()
        Dim setterBMValueFill As New Setter()
        Dim setterQueryUniqueFill As New Setter()
        Dim setterQueryPartialFill As New Setter()
        Dim setterFontWeightBlack As New Setter()
        Dim setterFontWeightNormal As New Setter()

        Dim cellStatusTagHeader = resultsDataTable.GetCellStatusTagHeader(p_header)

        Try

            'Create setters
            '' Queries
            With setterQueryPartialFill
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.Tomato
            End With
            With setterQueryUniqueFill
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.LightBlue
            End With

            '' Benchmarks
            With setterBMValueFill
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.LightGreen
            End With


            With setterBMValueSelectedFill
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.GreenYellow
            End With
            With setterBMValueIncompleteForegroundColor
                .Property = DataGridCell.ForegroundProperty
                .Value = Brushes.DeepPink
            End With

            '' Fonts & TextBlock
            With setterFontWeightBlack
                .Property = TextBlock.FontWeightProperty
                .Value = FontWeights.Black
            End With
            With setterFontWeightNormal
                .Property = TextBlock.FontWeightProperty
                .Value = FontWeights.Normal
            End With

            With setterTextBlockVerticalAlignment
                .Property = TextBlock.VerticalAlignmentProperty
                .Value = VerticalAlignment.Center
            End With

            'Create DataTriggers, add setters
            With triggerQueryPartial
                .Binding = New Binding(cellStatusTagHeader)
                .Value = GetEnumDescription(eQueryBMStatus.queryPartial)
                .Setters.Add(setterQueryPartialFill)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
            End With
            With triggerQueryUnique
                .Binding = New Binding(cellStatusTagHeader)
                .Value = GetEnumDescription(eQueryBMStatus.queryUnique)
                .Setters.Add(setterQueryUniqueFill)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
            End With
            With triggerBMValueSelected
                .Binding = New Binding(cellStatusTagHeader)
                .Value = GetEnumDescription(eQueryBMStatus.bmValueSelected)
                .Setters.Add(setterBMValueSelectedFill)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
            End With
            With triggerBMValue
                .Binding = New Binding(cellStatusTagHeader)
                .Value = GetEnumDescription(eQueryBMStatus.bmValue)
                .Setters.Add(setterBMValueFill)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
            End With
            With triggerBMValueIncomplete
                .Binding = New Binding(cellStatusTagHeader)
                .Value = GetEnumDescription(eQueryBMStatus.bmDetailsIncomplete)
                .Setters.Add(setterBMValueFill)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
                .Setters.Add(setterBMValueIncompleteForegroundColor)
            End With

            ' Clear the triggers & form style
            With _queryBMStyle
                .Triggers.Clear()
                .Triggers.Add(triggerQueryPartial)
                .Triggers.Add(triggerQueryUnique)
                .Triggers.Add(triggerBMValue)
                .Triggers.Add(triggerBMValueIncomplete)
                .Triggers.Add(triggerBMValueSelected)
                .TargetType = GetType(TextBlock)
            End With

            ' Assign style
            p_column.ElementStyle = _queryBMStyle
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the styles for the columns related to column details.
    ''' </summary>
    ''' <param name="p_Column">Column object that is to have the style assigned.</param>
    ''' <param name="p_Header">Header that corresponds to the column.</param>
    ''' <remarks></remarks>
    Private Sub SetResultsDetailsStyle(ByRef p_Column As DataGridTextColumn,
                                       ByVal p_Header As String)
        Dim resultDetailsStyle As New Style()
        Dim setterBackground As New Setter()
        Dim setterFont As New Setter()
        Dim setterTextBlockVerticalAlignment As New Setter()

        Try
            'Create setters
            With setterBackground
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.Azure
            End With

            'Create font setters
            With setterFont
                .Property = TextBlock.ForegroundProperty
                .Value = Brushes.Black
            End With
            With setterTextBlockVerticalAlignment
                .Property = TextBlock.VerticalAlignmentProperty
                .Value = VerticalAlignment.Center
            End With

            ''clear the setters & form style
            With resultDetailsStyle
                .Setters.Clear()
                .Setters.Add(setterBackground)
                .Setters.Add(setterFont)
                .Setters.Add(setterTextBlockVerticalAlignment)
                .TargetType = GetType(TextBlock)
            End With

            ''Assign style
            p_Column.ElementStyle = resultDetailsStyle
            p_Column.CellStyle = CType(Me.Resources(DG_RESULT_NULL), Style)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the styles for the results details status column.
    ''' </summary>
    ''' <param name="p_Column">Column object that is to have the style assigned.</param>
    ''' <param name="p_Header">Header that corresponds to the column.</param>
    ''' <remarks></remarks>
    Private Sub SetResultsDetailsStatusStyle(ByRef p_Column As DataGridTextColumn,
                                             ByVal p_Header As String)
        Dim resultDetailsStatusStyle As New Style()
        Dim triggerNull As New DataTrigger()
        Dim triggerAdd As New DataTrigger()
        Dim triggerComplete As New DataTrigger()
        Dim setterNull As New Setter()
        Dim setterAdd As New Setter()
        Dim setterComplete As New Setter()
        Dim setterFontColor As New Setter()
        Dim setterFontWeightBlack As New Setter()
        Dim setterTextBlockVerticalAlignment As New Setter()
        Dim setterTextBlockHorizontalAlignment As New Setter()

        Dim converter = New System.Windows.Media.BrushConverter()

        Try
            'Create setters
            With setterNull
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.Azure
            End With
            With setterAdd
                .Property = TextBlock.BackgroundProperty
                .Value = Brushes.Tomato
            End With
            With setterComplete
                .Property = TextBlock.BackgroundProperty
                .Value = converter.ConvertFromString("#FFCCB9FF")
            End With

            'Font Setters
            With setterFontColor
                .Property = TextBlock.ForegroundProperty
                .Value = Brushes.Black
            End With
            With setterFontWeightBlack
                .Property = TextBlock.FontWeightProperty
                .Value = FontWeights.Black
            End With
            With setterTextBlockVerticalAlignment
                .Property = TextBlock.VerticalAlignmentProperty
                .Value = VerticalAlignment.Center
            End With
            With setterTextBlockHorizontalAlignment
                .Property = TextBlock.HorizontalAlignmentProperty
                .Value = HorizontalAlignment.Center
            End With

            'Create triggers, add setters
            With triggerNull
                .Binding = New Binding(p_Header)
                .Value = ""
                .Setters.Add(setterNull)
                .Setters.Add(setterFontColor)
            End With
            With triggerAdd
                .Binding = New Binding(p_Header)
                .Value = GetEnumDescription(eResultDetailsStatus.add)
                .Setters.Add(setterAdd)
                .Setters.Add(setterFontColor)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
                .Setters.Add(setterTextBlockHorizontalAlignment)
            End With
            With triggerComplete
                .Binding = New Binding(p_Header)
                .Value = GetEnumDescription(eResultDetailsStatus.edit)
                .Setters.Add(setterComplete)
                .Setters.Add(setterFontColor)
                .Setters.Add(setterFontWeightBlack)
                .Setters.Add(setterTextBlockVerticalAlignment)
                .Setters.Add(setterTextBlockHorizontalAlignment)
            End With

            ''clear the triggers & form style
            With resultDetailsStatusStyle
                .Triggers.Clear()
                .Triggers.Add(triggerNull)
                .Triggers.Add(triggerAdd)
                .Triggers.Add(triggerComplete)
                .TargetType = GetType(TextBlock)
            End With

            ''Assign style
            p_Column.ElementStyle = resultDetailsStatusStyle
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Form Behavior"
    ''' <summary>
    ''' All actions to be done just after the form is first loaded.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoad(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles Me.Loaded
        _dataGridHeaderButtonsSizer.UpdateLayout()
    End Sub

    ''' <summary>
    ''' All actions to be done whenever the form is closed occur here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CloseSummaryForm()
    End Sub

    ''' <summary>
    ''' Moves the units buttons horizontally in equal opposition to the horizontal scrollbar to keep them aligned with datagrid columns.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_ScrollChanged(ByVal sender As Object, ByVal e As ScrollChangedEventArgs)
        If Not e.HorizontalChange = 0 Then
            _dataGridHeaderButtonsSizer.UpdateLayout()
        End If
        If Not e.VerticalChange = 0 Then
            _datagridScroller.SetScrolls()
        End If
    End Sub

    ''' <summary>
    ''' For tracking mouse position within the DataGrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_MouseMove(sender As Object, e As MouseEventArgs) Handles dgAccessTable.MouseMove
        _cursorPosition.X = e.GetPosition(dgAccessTable).X
        _cursorPosition.Y = e.GetPosition(dgAccessTable).Y

        'Console.WriteLine("X = " & _cursorPosition.X & " : Y = " & _cursorPosition.Y)
    End Sub

    ''' <summary>
    ''' Used to select a cell based on right-click to allow context-menu actions for those cells.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs) Handles dgAccessTable.MouseRightButtonDown

        SelectDataGridCellByCursor(dgAccessTable, _cursorPosition)

        Dim drv As DataRowView = CType(dgAccessTable.SelectedItem, System.Data.DataRowView)
        Dim lastBenchmarkCell As DataGridCell = Nothing
        Dim colIndex As Integer = 0

        'Note BM shifted and states. Used to 'undo' if SelectedCellsChanged event occurs next.
        If drv IsNot Nothing Then
            _isCurrentCellChangedInRow = True
            _lastBenchmarkHeader = drv.Item(BM_HEADER).ToString
            colIndex = GetColIndexFromHeader(dgAccessTable, _lastBenchmarkHeader)
            If colIndex >= 0 Then _lastBenchmarkCell = GetCellByRowSelection(dgAccessTable, GetSelectedRow(dgAccessTable), colIndex)
        End If

        UpdateDataGridCellFormatting(sender)
    End Sub

    ''' <summary>
    ''' For change in selected cell within a row, adjusts formatting of cell background for selected cells, gets current selected column.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_CurrentCellChanged(sender As Object, e As EventArgs) Handles dgAccessTable.CurrentCellChanged
        Dim drv As DataRowView = CType(dgAccessTable.SelectedItem, System.Data.DataRowView)
        Dim lastBenchmarkCell As DataGridCell = Nothing
        Dim colIndex As Integer = 0

        _datagridScroller.selectionMade = True

        'Note BM shifted and states. Used to 'undo' if SelectedCellsChanged event occurs next.
        If drv IsNot Nothing Then
            _isCurrentCellChangedInRow = True
            _lastBenchmarkHeader = drv.Item(BM_HEADER).ToString
            colIndex = GetColIndexFromHeader(dgAccessTable, _lastBenchmarkHeader)
            If colIndex >= 0 Then _lastBenchmarkCell = GetCellByRowSelection(dgAccessTable, GetSelectedRow(dgAccessTable), colIndex)
        End If

        UpdateDataGridCellFormatting(sender)

        ' Reverse tendency of datagrid to jump back to the top
        _datagridScroller.UpdateVerticalOffset()
    End Sub

    ''' <summary>
    ''' For change in selected row, adjusts formatting of cell background for selected cells, gets current selected column.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles dgAccessTable.SelectedCellsChanged
        'Undo shift selection change if performed before switching rows.
        If _isCurrentCellChangedInRow Then
            Dim droppedcells As IList(Of DataGridCellInfo) = e.RemovedCells
            If droppedcells.Count > 0 Then
                Dim lastDVR As DataRowView
                Try
                    lastDVR = DirectCast(droppedcells(0).Item, DataRowView)
                Catch ex As Exception
                    Exit Sub
                End Try

                Dim row As DataRow = lastDVR.Row

                If resultsDataTable.ChangeBenchmark(row, _lastBenchmarkHeader) Then
                    _lastBenchmarkChangedInOtherRow = True
                End If
            End If
            _isCurrentCellChangedInRow = False
        End If

        'Perform correct change
        UpdateDataGridCellFormatting(sender)

        'Undo shift selection change formatting if performed before switching rows.
        If _lastBenchmarkChangedInOtherRow Then
            UpdateBenchmarksFormat(dgAccessTable, True)
            UpdateResultDetailsFormat(dgAccessTable, True)
            _lastBenchmarkChangedInOtherRow = False
        End If

        ' Reverse tendency of datagrid to jump back to the top
        _datagridScroller.UpdateVerticalOffset()
    End Sub

    ''' <summary>
    ''' Announces beginning of sorting event.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_Sorting(sender As Object, e As DataGridSortingEventArgs) Handles dgAccessTable.Sorting
        _updateSorted = True
        _tempSender = Nothing
        _rowsLoaded.Clear()

        'Console.WriteLine("Sorting: I am sorting! Column: " & e.Column.Header.ToString)
    End Sub
    ''' <summary>
    ''' If a sorting procedure has occurred, the loaded rows event is re-called to perform the operation post-layoutUpdate.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_LayoutUpdated(sender As Object, e As EventArgs) Handles dgAccessTable.LayoutUpdated
        UpdateLayoutResultFormatting()
        _dataGridHeaderButtonsSizer.UpdateLayout()

        'Console.WriteLine("dgAccessTable.LayoutUpdated") 'Fires every time the cursor passes over a header or scroller
    End Sub
    ''' <summary>
    ''' Updates the row cell styles due to a DataGrid row reappearing in the screen. For sorting, scrolling, changing the form size.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_LoadingRow(sender As Object, e As DataGridRowEventArgs) Handles dgAccessTable.LoadingRow
        If e IsNot Nothing Then
            Dim colIndex As Integer = GetColIndexFromHeader(dgAccessTable, RESULT_DETAILS_HEADER)
            Dim resultStatusCell As DataGridCell = GetCellByRowColumnIndex(dgAccessTable, e.Row.GetIndex, colIndex)

            If resultStatusCell IsNot Nothing Then
                Dim drv As DataRowView = CType(e.Row.Item, DataRowView)

                UpdateRowBMCellStyles(dgAccessTable, drv.Row, , e.Row)
                UpdateRowResultDetailsCellStyles(drv.Row, resultStatusCell)
            ElseIf (_updateSorted OrElse _tableLoad) Then
                _rowsLoaded.Add(e)
                _tempSender = sender
            End If
        End If
    End Sub

    '=== Dynamically sets the maximum height of the datagrid so that scrollbars appear if not all rows are visible
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        SetMaxHeight()
    End Sub

    Protected Overrides Sub OnContentRendered(e As EventArgs)
        MyBase.OnContentRendered(e)

        If _shown Then Return

        _shown = True
        SetMaxHeight()
    End Sub

    Private Sub SetMaxHeight()
        If _shown Then
            UpdateDataGridHeight(dgAccessTable, gridMain, rowDG, brdr_DG)
            CheckFormDimensionLimits()
            SetDataGridCellsExtents(dgAccessTable)
            _dataGridHeaderButtonsSizer.UpdateLayout()
        End If
    End Sub

    ''' <summary>
    ''' Determines and sets the maximum &amp; minimum dimension limits for the form and DataGrid if they haven't already been set.
    ''' </summary>
    ''' <param name="p_overrideDimensions">If 'True', then dimensions will be re-sized even after the form has loaded.</param>
    ''' <remarks></remarks>
    Private Sub CheckFormDimensionLimits(Optional ByVal p_overrideDimensions As Boolean = False)
        If (Not _formSized OrElse p_overrideDimensions) Then
            Dim myScrollViewer As ScrollViewer = FindVisualChild(Of ScrollViewer)(dgAccessTable)
            Dim colHeadersPresenter As DataGridColumnHeadersPresenter = FindVisualChild(Of DataGridColumnHeadersPresenter)(dgAccessTable)
            Dim rows As New List(Of DataGridRow)

            'Get row object & update layout so that scrollviewer has updated data
            rows.Add(GetRowByIndex(dgAccessTable, 0))
            dgAccessTable.UpdateLayout()

            SetDGDimensionsMaxMin(colHeadersPresenter, rows)
            _formSized = SetFormDimensionsMaxMin(colHeadersPresenter, rows, myScrollViewer)
        End If
    End Sub

    ''' <summary>
    ''' Set the maximum &amp; minimum dimension limits for the form.
    ''' </summary>
    ''' <param name="p_colHeadersPresenter">Column headers for DataGrid height.</param>
    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display width and height.</param>
    ''' <param name="p_rows">Minimum DataGrid row objects set to include, starting from the first row.</param>
    ''' <remarks></remarks>
    Private Function SetFormDimensionsMaxMin(ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter,
                                        ByVal p_rows As List(Of DataGridRow),
                                        ByVal p_scrollViewer As ScrollViewer) As Boolean
        Dim formSized As Boolean = False

        _minHeightForm = GetFormMinHeight()
        _maxHeightForm = GetFormMaxHeight(p_rows, p_colHeadersPresenter, p_scrollViewer)
        _maxWidthForm = GetFormMaxWidth(p_scrollViewer)

        If (_minHeightForm > 0 AndAlso _maxHeightForm > 0 AndAlso _maxWidthForm > 0) Then formSized = True 'AndAlso _minWidthForm > 0 

        AssignFormDimensions(Me, _minHeightForm, _minWidthForm, _maxHeightForm, _maxWidthForm)

        Return formSized
    End Function

    ''' <summary>
    ''' Set the maximum &amp; minimum dimension limits for the DataGrid.
    ''' </summary>
    ''' <param name="p_colHeadersPresenter">Column headers for DataGrid height.</param>
    ''' <param name="p_rows">Minimum DataGrid row objects set to include, starting from the first row.</param>
    ''' <remarks></remarks>
    Private Sub SetDGDimensionsMaxMin(ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter,
                                      ByVal p_rows As List(Of DataGridRow))
        _minHeightDG = DataGridHeightMin(dgAccessTable, p_colHeadersPresenter, , p_rows)

        AssignDGDimensions(dgAccessTable, _minHeightDG)
    End Sub

    ''' <summary>
    ''' Get the maximum width to allow for the form.
    ''' </summary>
    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display width.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFormMaxWidth(Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
        Dim maxWidth As Double = GetFormWidthMinElements(Me, grpBxMain, gridMain)

        maxWidth += GetDataGridWidthMax(dgAccessTable, brdr_DG, p_scrollViewer)

        Return maxWidth
    End Function

    ''' <summary>
    ''' Get the maximum height to allow for the form. 
    ''' If optional parameters are not provided, results will be automatically determined from the form DataGrid object.
    ''' </summary>
    ''' <param name="p_colHeadersPresenter">Column headers for DataGrid height.</param>
    ''' <param name="p_scrollViewer">Scrollviewer object to query for the total display width and height.</param>
    ''' <param name="p_rows">Minimum DataGrid row objects set to include, starting from the first row.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFormMaxHeight(Optional ByVal p_rows As List(Of DataGridRow) = Nothing,
                                  Optional ByVal p_colHeadersPresenter As DataGridColumnHeadersPresenter = Nothing,
                                  Optional ByVal p_scrollViewer As ScrollViewer = Nothing) As Double
        Dim maxHeight As Double = GetFormHeightMinElements(Me, grpBxMain, gridMain)

        Dim grids As New List(Of RowDefinition)
        grids.Add(TopGrid)
        grids.Add(BottomGrid)

        maxHeight += GetGridHeights(grids)
        maxHeight += GetDataGridHeightMax(dgAccessTable, brdr_DG, p_rows, p_colHeadersPresenter, p_scrollViewer)
        maxHeight += GetUnitsButtonHeight()

        Return maxHeight
    End Function

    ''' <summary>
    ''' Get the minimum height to allow for the form.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFormMinHeight() As Double
        Dim minHeight As Double = GetFormHeightMinElements(Me, grpBxMain, gridMain)
        Dim grids As New List(Of RowDefinition)
        grids.Add(TopGrid)
        grids.Add(BottomGrid)

        minHeight += GetGridHeights(grids)
        minHeight += GetDataGridHeightMin(dgAccessTable, brdr_DG, _minHeightDG)
        minHeight += GetUnitsButtonHeight()

        Return minHeight
    End Function

    ''' <summary>
    ''' Returns the height of the column Units buttons and the vertical margins.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitsButtonHeight() As Double
        Dim height As Double = 0

        If (_unitsButtons IsNot Nothing AndAlso _unitsButtons.Count > 0) Then
            height += _unitsButtons(0).ActualHeight
            height += _unitsButtons(0).Margin.Top
            height += _unitsButtons(0).Margin.Bottom
        End If

        Return height
    End Function
#End Region

#Region "Form Controls"
    '=== Mouse Actions
    ''' <summary>
    ''' Adds queries &amp; benchmarks based on double click actions &amp; ordering.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgAccessTable_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Try
            If CursorWithinDataGridCells() Then DataGrid_DoubleClick()

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    '=== Buttons
    Private Sub btnOK_Click(sender As Object, e As RoutedEventArgs) Handles btnOK.Click
        SaveResults()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub All_Units_Buttons_Clicked(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' Get button clicked by index and click the corresponding units object.
        Dim btnName As String = CType(sender, Button).Name
        Dim columnIndex As Integer = myCInt(Right(btnName, btnName.Length - UNITS_BUTTON_NAME.Length))

        UnitButtonClick(columnIndex)
    End Sub

    Private Sub AddToQuery_Click() Handles btnAddToQuery.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.queryFieldAdd, _currentDGColIndexSelected)
    End Sub
    Private Sub RemoveFromQuery_Click() Handles btnRemoveFromQuery.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.queryFieldRemove, _currentDGColIndexSelected)
    End Sub
    Private Sub AddBenchmark_Click() Handles btnAddBenchmark.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.benchmarkAdd, _currentDGColIndexSelected)
    End Sub
    Private Sub RemoveBenchmark_Click() Handles btnRemoveBenchmark.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.benchmarkRemove)
    End Sub
    Private Sub RemoveResult_Click() Handles btnRemoveLine.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.resultRemove)
    End Sub
    Private Sub AddResultDetails_Click(sender As Object, e As RoutedEventArgs) Handles btnAddResultDetails.Click
        PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.resultDetailsAddEdit)
    End Sub

    Private Sub btnDisplayIncompleteResultsSummary_Click(sender As Object, e As RoutedEventArgs) Handles btnDisplayIncompleteResultsSummary.Click
        DisplayResultsSummaryModeless(resultsDataTable.tableQueriesIncomplete, True)
        _myFrmSummaryResultsShowTotal = False
    End Sub
    Private Sub btnDisplayResultsSummary_Click(sender As Object, e As RoutedEventArgs) Handles btnDisplayResultsSummary.Click
        DisplayResultsSummaryModeless(resultsDataTable.tableQueriesTotal, False)
        _myFrmSummaryResultsShowTotal = True
    End Sub


    '=== Combo Box
    Private Sub cmbBxTablesList_DropDownOpened(sender As Object, e As EventArgs) Handles cmbBxTablesList.DropDownOpened
        _lastComboBoxItemSelected = cmbBxTablesList.SelectedValue.ToString
    End Sub
    Private Sub cmbBxTablesList_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxTablesList.SelectionChanged
        If Not _firstLoad Then
            If _implementNewComboBoxSelection Then
                If resultsDataTable.ResultsNeedCompletion() Then
                    _implementNewComboBoxSelection = False
                    cmbBxTablesList.SelectedItem = _lastComboBoxItemSelected

                    DisplayResultsSummaryModeless(resultsDataTable.tableQueriesIncomplete, True)
                    Exit Sub
                Else
                    ChangeTables()
                End If
            Else
                _implementNewComboBoxSelection = True
            End If
        End If
    End Sub

    '=== Keyboard Actions
    ' TODO: Not capturing events
    Private Sub dgAccessTable_KeyDown(sender As Object, e As KeyEventArgs) Handles dgAccessTable.KeyDown
        'If (e.Key = Key.LeftCtrl AndAlso
        '    e.Key = Key.Enter) Then
        '    DataGrid_DoubleClick()
        'End If

        'If (e.Key = Key.Enter) Then
        '    DataGrid_DoubleClick()
        'End If
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Removes all incomplete result objects and updates the DataGrid cell formatting accordingly.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveIncompleteResults()
        Try
            resultsDataTable.RemoveIncompleteResults()
            UpdateAllEntryCellStyles()

            If (_myFrmSummaryResults IsNot Nothing AndAlso _myFrmSummaryResults.IsLoaded) Then
                If _myFrmSummaryResultsShowTotal Then
                    _myFrmSummaryResults.UpdateForm(resultsDataTable.tableQueriesTotal)
                Else
                    _myFrmSummaryResults.UpdateForm(resultsDataTable.tableQueriesIncomplete)
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Saves the relevant results from the current session, or handles incomplete results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveResults()
        'Save current table queries
        If resultsDataTable.ResultsNeedCompletion() Then
            DisplayResultsSummaryModeless(resultsDataTable.tableQueriesIncomplete, True)
            Exit Sub
        Else
            ' Update result collections
            allResults = ConvertListToObservableCollection(ResultsFromDataTable())
            allResultsSave = allResults

            FillResultsFromUnits(allResultsSave)

            formCancel = False

            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Compiles all of the table query classes and relevant DataTable into a list of result objects.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultsFromDataTable() As List(Of cMCResult)
        With resultsDataTable
            .WriteRecordsOfQueriesToResults()
            .RemoveIncompleteResultsAll()
        End With
        MaintainTempIDs()
        Return resultsDataTable.WriteToXMLResultsClass(tableQueriesCollection)
    End Function

    ''' <summary>
    ''' Maintains the temp IDs brought into the session.
    ''' This is done here as it is impossible to maintain them throughout when swapping result items in the dataTable.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MaintainTempIDs()
        Dim matchFound As Boolean = False

        For Each result As cMCResult In allResultsSave
            matchFound = False
            For Each resultsSet As cTableQueries In tableQueriesCollection
                If (result.tableName.CompareTo(resultsSet.tableName) = 0) Then
                    For Each resultNew As cMCResult In resultsSet.Results
                        ' Find match based on table name and actual query components
                        If (result.query.Equals(resultNew.query) AndAlso
                            result.benchmark.name.CompareTo(resultNew.benchmark.name) = 0) Then
                            resultNew.idTemp = result.idTemp
                            matchFound = True
                            Exit For
                        End If
                    Next
                End If
                If matchFound Then Exit For
            Next
        Next
    End Sub

    ''' <summary>
    ''' Updates the content of all unit buttons.
    ''' </summary>
    ''' <param name="p_numberOfButtons"></param>
    ''' <remarks></remarks>
    Private Sub UpdateAllButtonsFromUnits(ByVal p_numberOfButtons As Integer)
        For i = 0 To p_numberOfButtons
            UpdateButtonsContent(i)
        Next
    End Sub

    ''' <summary>
    ''' If an example currently contains results with units, these are populated into the corresponding column units buttons.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillUnitsFromResultsForCurrentTable()
        ' Get results that apply to the current table
        For Each result As cMCResult In allResultsSave
            If result.tableName = resultsDataTable.exportedSetTable.TableName Then

                ' Determine matching units objects to columns
                Dim unitsIndex As Integer = 0
                For Each column As DataColumn In resultsDataTable.exportedSetTable.Columns
                    If column.ColumnName = result.benchmark.name Then

                        ' Assign table units to unit objects
                        If Not String.IsNullOrWhiteSpace(result.units) Then
                            _unitsCollectionCurrent(unitsIndex).ParseStringToUnits(result.units)
                        End If
                    End If
                    unitsIndex += 1
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Saves the units set into the corresponding model control result objects.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillResultsFromUnits(ByVal p_results As ObservableCollection(Of cMCResult))
        For Each result As cMCResult In p_results
            FillResultFromUnits(result)
        Next
    End Sub

    ''' <summary>
    ''' Saves the units set into the corresponding model control result object.
    ''' </summary>
    ''' <param name="p_result">Result object to fill.</param>
    ''' <remarks></remarks>
    Private Sub FillResultFromUnits(ByRef p_result As cMCResult)
        ' Get units and data table that correspond to the result
        Dim dataTableUnits As List(Of cUnitsController) = Nothing
        If _unitsCollections.Keys.Contains(p_result.tableName) Then
            dataTableUnits = _unitsCollections(p_result.tableName)
        End If
        If (dataTableUnits Is Nothing) Then Exit Sub

        Dim resultDataTable As cTableQueryView = _resultsDataTables(p_result.tableName)
        If resultDataTable Is Nothing Then resultDataTable = CreateNewTableQueryView(p_result.tableName)
        If (resultDataTable Is Nothing) Then Exit Sub

        ' Determine matching units objects to columns
        Dim unitsIndex As Integer = 0
        For Each column As DataColumn In resultDataTable.exportedSetTable.Columns
            If column.ColumnName = p_result.benchmark.name Then
                Dim units As cUnits = dataTableUnits(unitsIndex).units

                ' Assign units names
                If Not String.IsNullOrEmpty(units.shorthandLabel) Then
                    p_result.units = units.shorthandLabel
                Else
                    p_result.units = units.GetUnitsLabel
                End If
                Exit For

            End If
            unitsIndex += 1
        Next
    End Sub

    ''' <summary>
    ''' Performs the action of clicking a unit button.
    ''' </summary>
    ''' <param name="p_columnIndex">The button index, which is needed to select the correct button.</param>
    ''' <remarks></remarks>
    Private Sub UnitButtonClick(ByVal p_columnIndex As Integer)
        Dim unitsCtrl As cUnitsController = _unitsCollectionCurrent(p_columnIndex)
        Dim btnCurrent As Button = _unitsButtons(p_columnIndex)
        Dim btnContent As String = ""

        Dim windowMCUnits = New frmUnits(unitsCtrl, _unitsProgramControl)
        windowMCUnits.ShowDialog()

        If Not windowMCUnits.formCancel Then
            'Update objects
            _unitsCollectionCurrent(p_columnIndex) = windowMCUnits.unitsController
            unitsCtrl = _unitsCollectionCurrent(p_columnIndex)
        End If

        UpdateButtonsContent(btnCurrent, unitsCtrl)
    End Sub


    ''' <summary>
    ''' Displays a modeless form that contains a summary list of the results list provided. 
    ''' </summary>
    ''' <param name="p_tableQueries">List of result objects to display in the form.</param>
    ''' <remarks></remarks>
    Private Sub DisplayResultsSummaryModeless(ByVal p_tableQueries As cTableQueries,
                                              ByVal isIncomplete As Boolean)
        Try
            If (p_tableQueries.Results.Count > 0 AndAlso
                (_myFrmSummaryResults Is Nothing OrElse
                 Not _myFrmSummaryResults.IsVisible)) Then

                btnDisplayIncompleteResultsSummary.IsEnabled = False
                btnDisplayResultsSummary.IsEnabled = False

                _myFrmSummaryResults = New frmXMLObjectResultsSummary(p_tableQueries)
                If isIncomplete Then
                    _myFrmSummaryResults.Title = "Incomplete Results Summary"
                Else
                    _myFrmSummaryResults.Title = "Results Summary"
                End If
                _myFrmSummaryResults.Show()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Saves current complete results and changes the table displayed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeTables()
        Try
            CloseSummaryForm()

            'Save current table queries
            With resultsDataTable
                .WriteRecordsOfQueriesToResults()
                .RemoveIncompleteResultsAll()
            End With

            _resultsDataTables.UpdateEntry(resultsDataTable)

            'Re-Initialize DataGrid
            Dim tableName As String = CStr(cmbBxTablesList.SelectedItem)
            InitializeDataGrid(tableName)
            CheckFormDimensionLimits(True)
            SetSizeToMaximum(Me)

            ' Update formatting of all visible cells if results are present
            'UpdateLayoutSorting()
            'If resultsDataTable.tableQueriesTotal.xmlMCResults.Count > 0 Then
            '    'UpdateDataGridCellFormatting(dgAccessTable)
            '    'UpdateAllEntryCellStyles()
            '    _updateSorted = True
            '    _rowsLoaded.Clear()
            '    dgAccessTable.UpdateLayout()
            'End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        _tableLoad = False
    End Sub

    ''' <summary>
    ''' Closes the summary form if necessary.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseSummaryForm()
        If (_myFrmSummaryResults IsNot Nothing AndAlso _myFrmSummaryResults.IsLoaded) Then _myFrmSummaryResults.Close()
    End Sub

    ''' <summary>
    ''' Updates the button content based on the contents of the corresponding units object.
    ''' </summary>
    ''' <param name="p_columnIndex">Index corresponding to which column in the dataGrid/dataTable is to be updated.</param>
    ''' <remarks></remarks>
    Private Sub UpdateButtonsContent(ByVal p_columnIndex As Integer)
        Dim btnCurrent As Button = _unitsButtons(p_columnIndex)
        Dim unitsCtrl As cUnitsController = _unitsCollectionCurrent(p_columnIndex)
        UpdateButtonsContent(btnCurrent, unitsCtrl)
    End Sub
    Private Sub UpdateButtonsContent(ByVal p_button As Button,
                                     ByVal p_unitsController As cUnitsController)
        Dim btnContent As String = GetUnitButtonsContent(p_unitsController)
        Dim btnToolTip As String = GetUnitButtonContentLong(p_unitsController)
        SetUnitButtonsContent(p_button, btnContent, btnToolTip)
    End Sub

    ''' <summary>
    ''' Returns the message that should be displayed on the units button. 
    ''' If possible, this is the current selection of units for the corresponding column.
    ''' </summary>
    ''' <param name="p_unitsController"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitButtonsContent(ByVal p_unitsController As cUnitsController) As String
        If p_unitsController.units.AreUnitsSet() Then
            If p_unitsController.typeShorthand = cUnitsController.eUnitTypeShorthand.none Then
                Return p_unitsController.units.GetUnitsLabel()
            Else
                Return p_unitsController.units.shorthandLabel
            End If
        ElseIf p_unitsController.units.IsSchemaSet Then
            Return p_unitsController.units.GetSchemaLabel()
        Else
            Return UNITS_BUTTON_CONTENT_DEFAULT
        End If
    End Function

    ''' <summary>
    ''' Returns the message that should be displayed on the units button tooltip.
    ''' This includes the unit type. 
    ''' If possible, this is the current selection of units for the corresponding column.
    ''' </summary>
    ''' <param name="p_unitsController"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitButtonContentLong(ByVal p_unitsController As cUnitsController) As String
        Dim label As String = " (" & GetEnumDescription(p_unitsController.type) & ")"
        Return GetUnitButtonsContent(p_unitsController) & label
    End Function

    ''' <summary>
    ''' Set tooltips and text on the corresponding unit button.
    ''' </summary>
    ''' <param name="p_button"></param>
    ''' <param name="p_content"></param>
    ''' <remarks></remarks>
    Private Sub SetUnitButtonsContent(ByVal p_button As Button,
                                      ByVal p_content As String,
                                      Optional ByVal p_contentTooltip As String = "")

        p_button.Content = p_content
        If p_content = UNITS_BUTTON_CONTENT_DEFAULT Then
            p_button.ToolTip = UNITS_BUTTON_TT_DEFAULT
        ElseIf Not String.IsNullOrEmpty(p_contentTooltip) Then
            p_button.ToolTip = p_contentTooltip
        Else
            p_button.ToolTip = p_content
        End If
    End Sub

    ''' <summary>
    ''' Check if cursor is in the valid region of DataGrid cells.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CursorWithinDataGridCells() As Boolean
        If (Not (_activeDataGridArea(0).X < _cursorPosition.X AndAlso _cursorPosition.X < _activeDataGridArea(1).X) OrElse
            Not (_activeDataGridArea(0).Y < _cursorPosition.Y AndAlso _cursorPosition.Y < _activeDataGridArea(1).Y)) Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Performs the basic action when a datagrid cell is double-clicked.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DataGrid_DoubleClick()
        Try
            Dim colIndex As Integer = dgAccessTable.CurrentColumn.DisplayIndex
            Dim colRegion As eDataGridRegion = resultsDataTable.ColumnRegionByColIndex(colIndex)

            Select Case colRegion
                Case eDataGridRegion.importedDBTable
                    PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.benchmarkQueryAddRemove)
                Case eDataGridRegion.resultsStatus,
                     eDataGridRegion.resultsDetails
                    PerformActionToDataTableFromDataGrid(dgAccessTable, eResultOperation.resultDetailsAddEdit)
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        ' Reverse tendency of datagrid to jump back to the top
        _datagridScroller.UpdateVerticalOffset()
    End Sub


#End Region

#Region "Methods: Private - DataGrid/DataTable Interface"
    ''' <summary>
    ''' Handles datagrid style initialization and preservation when loading rows or when sorting by passing the previous row state to the current row state.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateLayoutResultFormatting()
        If (_updateSorted OrElse _tableLoad) Then
            Dim rowsLoaded As New List(Of DataGridRowEventArgs)
            For Each eRow As DataGridRowEventArgs In _rowsLoaded
                rowsLoaded.Add(eRow)
            Next

            For Each eRow As DataGridRowEventArgs In rowsLoaded
                dgAccessTable_LoadingRow(_tempSender, eRow)
            Next
            _updateSorted = False
        End If
    End Sub

    ''' <summary>
    ''' Determines the column index (if needed) of the desired column in a DataGrid object, assigns the corresponding header string in the DataGrid, and assigns the corresponding DataRow object. 
    ''' Returns 'True' if successful, else returns 'False'.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to which to apply the function.</param>
    ''' <param name="p_dataGridColIndex">Supplied DataGrid column index for button-click calls used in assigning the p_header value. If supplied as -1, the currently selected column will be used.</param>
    ''' <param name="p_cellItem">Header and associated value of the item associated with the dataGrid cell object.</param>
    ''' <param name="p_dataRow">DataRow object from the supplied DataTable that corresponds to the DataGrid entry selected.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AssignSelectedCellReferences(ByVal p_dataGrid As DataGrid,
                                                  ByRef p_dataRow As DataRow,
                                                  Optional ByRef p_dataGridColIndex As Integer = -1,
                                                  Optional ByRef p_cellItem As cHeaderAndValue = Nothing) As Boolean
        Try
            'Establish the column index as that provided, or currently selected
            If p_dataGridColIndex = -1 Then
                p_dataGridColIndex = GetColumnIndexFromSelectionDG(p_dataGrid, _currentDGColIndexSelected)
                If p_dataGridColIndex = -1 Then Return False
            End If

            p_dataRow = _currentDRVSelected.Row

            Dim dtController As New cDataTableController
            Dim header As String = dtController.HeaderFromColumnIndex(resultsDataTable.exportedSetTable, p_dataGridColIndex)
            If p_cellItem IsNot Nothing Then
                With p_cellItem
                    .header = header
                    .value = p_dataRow.Item(.header).ToString
                End With
            End If

            If (Not String.IsNullOrEmpty(header) AndAlso p_dataRow IsNot Nothing) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try
    End Function


    '=== Actions to add/remove cells from query, BM, add result details, etc.
    ''' <summary>
    ''' Performs the specified action to the DataTable based on the DataGrid.
    ''' </summary>
    ''' <param name="p_dataGrid">Data grid object from which to apply the action.</param>
    ''' <param name="p_dataGridAction">Action to perfrom between the DataGrid and DataTable.</param>
    ''' <param name="p_dataGridColIndex">Supplied column index of the DataGrid for button-click calls. If not supplied, the current DataGrid selection is used.</param>
    ''' <remarks></remarks>
    Private Sub PerformActionToDataTableFromDataGrid(ByVal p_dataGrid As DataGrid,
                                                     ByVal p_dataGridAction As eResultOperation,
                                                     Optional ByVal p_dataGridColIndex As Integer = -1)
        Try
            If p_dataGrid Is Nothing Then Exit Sub

            'Get the DataRow object and associated header name for the selection
            Dim cellItem As New cHeaderAndValue
            Dim row As DataRow = Nothing
            Dim resultOperation As eResultOperation = p_dataGridAction
            If Not AssignSelectedCellReferences(p_dataGrid, row, p_dataGridColIndex, cellItem) Then Exit Sub

            With resultsDataTable
                Select Case p_dataGridAction
                    Case eResultOperation.benchmarkAdd
                        .AddBenchmark(row, cellItem.header)
                    Case eResultOperation.benchmarkRemove
                        .RemoveBenchmark(row, cellItem.header)
                    Case eResultOperation.queryFieldAdd
                        .UpdateQuery(row, cellItem, True)
                    Case eResultOperation.queryFieldRemove
                        .UpdateQuery(row, cellItem, False)
                    Case eResultOperation.benchmarkQueryAddRemove
                        .AddRemoveQueryComponentOrBM(row, cellItem, resultsDataTable.exportedSetTable, resultOperation)       'resultOperation is set to either 'add' or 'remove' here
                    Case eResultOperation.resultDetailsAddEdit
                        .AddResultDetailsFromForm(row)
                    Case eResultOperation.resultRemove
                        .RemoveRowResults(row)
                End Select
            End With

            UpdateDataGridCellFormatting(p_dataGrid)

            If (_myFrmSummaryResults IsNot Nothing AndAlso _myFrmSummaryResults.IsLoaded) Then
                If _myFrmSummaryResultsShowTotal Then
                    _myFrmSummaryResults.UpdateForm(resultsDataTable.tableQueriesTotal)
                Else
                    _myFrmSummaryResults.UpdateForm(resultsDataTable.tableQueriesIncomplete)
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Private - Cell Formatting"
    'For Entire DataGrid
    ''' <summary>
    ''' Resets the cell style formatting for the entire DataGrid.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateAllEntryCellStyles()
        ClearAllDBImportCellStyles(dgAccessTable)
        ClearAllResultDetailsCellStyles(dgAccessTable)
        UpdateAllBMCellStyles(dgAccessTable, resultsDataTable)
        UpdateAllResultDetailsCellStyles(dgAccessTable, resultsDataTable)
    End Sub

    '' Clear/Reset Formatting
    ''' <summary>
    ''' Clears the cell style formatting for all cells in the imported database values region of the table.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <remarks></remarks>
    Private Sub ClearAllDBImportCellStyles(ByVal p_dataGrid As DataGrid)
        'For all rows & columns, clear the cell formatting until the result details column is reached
        For rowIndex = 0 To p_dataGrid.Items.Count - 1
            ClearRowDBImportCellStyles(rowIndex)
        Next
    End Sub

    ''' <summary>
    ''' Clears the result details cell styles for the entire DataGrid.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <remarks></remarks>
    Private Sub ClearAllResultDetailsCellStyles(ByVal p_dataGrid As DataGrid)
        Dim resultStatusCell As DataGridCell

        Dim colIndex As Integer = GetColIndexFromHeader(p_dataGrid, RESULT_DETAILS_HEADER)
        If colIndex < 0 Then Exit Sub

        For rowIndex = 0 To p_dataGrid.Items.Count - 1
            resultStatusCell = GetCellByRowColumnIndex(p_dataGrid, rowIndex, colIndex)
            resultStatusCell.Style = CType(Me.Resources(DG_RESULT_NULL), Style)
        Next
    End Sub

    '' Update Formatting
    ''' <summary>
    ''' Updates the benchmark cell styles for the entire DataGrid.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <param name="p_resultsDataTable">Corresponding DataTable object containing the data.</param>
    ''' <remarks></remarks>
    Private Sub UpdateAllBMCellStyles(ByVal p_dataGrid As DataGrid,
                                      ByVal p_resultsDataTable As cTableQueryView)
        'Note - rows between the DataGrid & DataTable are not in sync here.
        'Since all rows are being cleared, this does not matter so long as the index bounds are the same (they should be).
        For rowIndex = 0 To p_dataGrid.Items.Count - 1
            Dim dtRow As DataRow = p_resultsDataTable.exportedSetTable(rowIndex)

            UpdateRowBMCellStyles(p_dataGrid, dtRow, rowIndex)
        Next
    End Sub

    ''' <summary>
    '''  Updates the result details cell styles for the entire DataGrid.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <param name="p_resultsDataTable">Corresponding DataTable object containing the data.</param>
    ''' <remarks></remarks>
    Private Sub UpdateAllResultDetailsCellStyles(ByVal p_dataGrid As DataGrid,
                                                ByVal p_resultsDataTable As cTableQueryView)
        Dim resultStatusCell As DataGridCell
        Dim dtRow As DataRow

        Dim colIndex As Integer = GetColIndexFromHeader(p_dataGrid, RESULT_DETAILS_HEADER)
        If colIndex < 0 Then Exit Sub

        For rowIndex = 0 To p_dataGrid.Items.Count - 1
            dtRow = p_resultsDataTable.GetDataRowByKey(rowIndex)(0)
            resultStatusCell = GetCellByRowColumnIndex(p_dataGrid, rowIndex, colIndex)

            UpdateRowResultDetailsCellStyles(dtRow, resultStatusCell)
        Next
    End Sub

    'For Row
    '' Clear/Reset Formatting
    ''' <summary>
    ''' 'For all columns of the specified row, clear the cell formatting until the result details column is reached
    ''' </summary>
    ''' <param name="p_rowIndex">Cells for the specified row index will be formatted.</param>
    ''' <remarks></remarks>
    Private Sub ClearRowDBImportCellStyles(ByVal p_rowIndex As Integer)
        Dim cell As DataGridCell
        Dim colRegion As eDataGridRegion

        For colIndex = 0 To dgAccessTable.Columns.Count - 1
            colRegion = resultsDataTable.ColumnRegionByColIndex(colIndex)
            If colRegion = eDataGridRegion.importedDBTable Then
                cell = GetCellByRowColumnIndex(dgAccessTable, p_rowIndex, colIndex)
                cell.Style = Nothing
            End If
        Next
    End Sub

    ''' <summary>
    ''' Clears the cell formatting for the results details cell for the specified row.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <param name="p_rowIndex">Cell for the specified row index will be formatted.</param>
    ''' <remarks></remarks>
    Private Sub ClearRowResultDetailsCellStyles(ByVal p_dataGrid As DataGrid,
                                                ByVal p_rowIndex As Integer)

        Dim colIndex As Integer = GetColIndexFromHeader(p_dataGrid, RESULT_DETAILS_HEADER)
        Dim resultStatusCell As DataGridCell = GetCellByRowColumnIndex(p_dataGrid, p_rowIndex, colIndex)

        resultStatusCell.Style = CType(Me.Resources(DG_RESULT_NULL), Style)
    End Sub

    '' Update Formatting
    ''' <summary>
    ''' Check all columns in the imported database region for a given row and assign the appropriate cell style based on the cell benchmark status.
    ''' If neither a row index nor a DataGridRow is provided, then no action will be taken.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid to apply the formatting to.</param>
    ''' <param name="p_dtRow">DataTable row used to determine the cell status.</param>
    ''' <param name="p_rowIndex">Cells for the specified row index will be formatted if not DataGridRow is provided.</param>
    ''' <param name="p_dgRow">Cells for the provided DataGridRow will be formatted.</param>
    ''' <remarks></remarks>
    Private Sub UpdateRowBMCellStyles(ByVal p_dataGrid As DataGrid,
                                   ByVal p_dtRow As DataRow,
                                   Optional ByVal p_rowIndex As Integer = -1,
                                   Optional ByVal p_dgRow As DataGridRow = Nothing)
        Dim bmQueryCell As DataGridCell = Nothing
        Dim bmQueryCellStatus As eQueryBMStatus = eQueryBMStatus.undefined
        Dim bmQueryCellStyle As Style = CType(Me.Resources(DG_SELECTED_ROW), Style)
        Dim colHeader As String
        Dim colRegion As eDataGridRegion = eDataGridRegion.undefinedRegion

        For colIndex = 0 To p_dataGrid.Columns.Count - 1
            colRegion = resultsDataTable.ColumnRegionByColIndex(colIndex)
            If colRegion = eDataGridRegion.importedDBTable Then
                colHeader = p_dataGrid.Columns(colIndex).Header.ToString

                'Determine cell style based on cell status
                bmQueryCellStatus = resultsDataTable.GetCellStatus(colHeader, p_dtRow)
                Select Case bmQueryCellStatus
                    Case eQueryBMStatus.bmValueSelected
                        bmQueryCellStyle = CType(Me.Resources(DG_SELECTED_BM), Style)
                    Case Else
                        bmQueryCellStyle = Nothing 'CType(Me.Resources(DG_SELECTED_ROW), Style)
                End Select

                'Get the DataGridCell to change the cell style on
                If (p_dgRow Is Nothing AndAlso p_rowIndex >= 0) Then
                    bmQueryCell = GetCellByRowColumnIndex(p_dataGrid, p_rowIndex, colIndex)
                ElseIf p_dgRow IsNot Nothing Then
                    bmQueryCell = GetCellFromDGRowAndColIndex(p_dataGrid, p_dgRow, colIndex)
                End If

                If bmQueryCell IsNot Nothing Then bmQueryCell.Style = bmQueryCellStyle
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sets the specified row's result details formatting to the appropriate style based on the cell status.
    ''' </summary>
    ''' <param name="p_row">DataTable row used to determine the cell status.</param>
    ''' <param name="p_resultStatusCell">Cell to apply the formatting to.</param>
    ''' <remarks></remarks>
    Private Sub UpdateRowResultDetailsCellStyles(ByVal p_row As DataRow,
                                                 ByVal p_resultStatusCell As DataGridCell)
        Dim cellStyle As Style

        Select Case p_row.Item(RESULT_DETAILS_HEADER).ToString
            Case GetEnumDescription(eResultDetailsStatus.edit) : cellStyle = CType(Me.Resources(DG_RESULT_EDIT), Style)
            Case GetEnumDescription(eResultDetailsStatus.add) : cellStyle = CType(Me.Resources(DG_RESULT_ADD), Style)
            Case Else : cellStyle = CType(Me.Resources(DG_RESULT_NULL), Style)
        End Select

        p_resultStatusCell.Style = cellStyle
    End Sub
#End Region

#Region "Methods: Private - Cell Click Events"
    ''' <summary>
    ''' Controller function that performs all operations related to a cell selection event.
    ''' </summary>
    ''' <param name="sender">Object to be cast as a DataGrid object.</param>
    ''' <remarks></remarks>
    Private Sub UpdateDataGridCellFormatting(ByVal sender As Object)
        Dim grid As DataGrid = CType(sender, DataGrid)
        Dim bmRemoved As Boolean = False

        If Not UpdateSelectionStatesVariables(grid) Then Exit Sub

        UpdateSelectedCellsFormatting(grid, DG_SELECTED_CELL, DG_SELECTED_BM)

        UpdateBMSelection(grid, True)
        UpdateBenchmarksFormat(grid)
        UpdateResultDetailsFormat(grid)

        CheckFormDimensionLimits(True)
    End Sub

    'Form State
    ''' <summary>
    ''' Updates various references based on the cell selection, such as:
    ''' 1. Current column index 
    ''' 2. Current DataRowView
    ''' 3. Current and last query and benchmark selections
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSelectionStatesVariables(ByVal p_dataGrid As DataGrid) As Boolean
        Dim noErrors As Boolean = True
        Dim colIndex As Integer
        Dim currentDRV As DataRowView
        Dim currentCell As DataGridCell

        Try
            'Update column index selected
            colIndex = GetColumnIndexFromSelectionDG(p_dataGrid)
            If Not colIndex = -1 Then
                _currentDGColIndexSelected = colIndex
            Else
                'Current data is correct. Selection of DG was lost
                'noErrors = False
            End If

            'Update selected DRV
            If p_dataGrid.SelectedItem IsNot Nothing Then
                'Get DataRowView from selection
                currentDRV = CType(p_dataGrid.SelectedItem, System.Data.DataRowView)
                _currentDRVSelected = currentDRV
            Else
                noErrors = False
            End If

            'Update selected cell
            currentCell = GetSelectedCell(p_dataGrid, _currentDGColIndexSelected)
            If currentCell IsNot Nothing Then
                _lastCellSelected = _currentCellSelected
                _currentCellSelected = currentCell
            Else
                noErrors = False
            End If

            'Determine current state of last/next cells
            _IsLastCellSelectedBenchmarkSelected = IsLastCellSelectedBenchmarkSelected()
            _IsCurrentCellSelectedBenchmarkSelected = IsNextCellSelectedBenchmarkSelected(p_dataGrid)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            noErrors = False
        End Try

        Return noErrors
    End Function

    ''' <summary>
    ''' Returns 'True' if the last cell selected corresponds with a selected benchmark. Otherwise, returns 'False'.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsLastCellSelectedBenchmarkSelected() As Boolean
        Try
            If _lastCellSelected Is Nothing Then Return False

            If _lastCellSelected.Column.Header.ToString = _lastBenchmarkHeader Then 'lastBMCellValue Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Returns 'True' if the next cell selected corresponds with a selected benchmark. Otherwise, returns 'False'.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsNextCellSelectedBenchmarkSelected(ByVal p_dataGrid As DataGrid) As Boolean
        Dim nextCellSelected As DataGridCell = Nothing
        Dim nextRowSelected As DataGridRow
        Dim nextBMCellValue As String = ""

        Try
            'Get next selected cell & the benchmark value for its row
            nextCellSelected = GetSelectedCell(p_dataGrid, _currentDGColIndexSelected)
            If nextCellSelected Is Nothing Then Return False

            nextRowSelected = GetRowFromCell(nextCellSelected)
            If nextRowSelected Is Nothing Then Return False

            nextBMCellValue = GetValueFromRowAndHeader(nextRowSelected, BM_HEADER)
            If nextBMCellValue Is Nothing Then Return False

            'Determine Benchmark selection state
            If nextCellSelected.Column.Header.ToString = nextBMCellValue Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Updates the data in the DataGrid if a new benchmark is selected for a given row. 
    ''' Returns 'True' if a benchmark is successfully changed (changing to the same benchmark returns 'True' as well).
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <param name="p_benchmarkRemoved">If 'True', the cell formatted will be the one marked as the currently selected benchmark.</param>
    ''' <remarks></remarks>
    Private Function SelectedCellChangeBMSelection(ByVal p_dataGrid As DataGrid,
                                                   Optional ByVal p_benchmarkRemoved As Boolean = False) As Boolean
        Dim bmChanged As Boolean = False
        Dim row As DataRow = Nothing
        Dim cellItem As New cHeaderAndValue

        Try
            If p_dataGrid Is Nothing Then Return False

            'Get the DataRow object and associated header name for the selection
            If Not AssignSelectedCellReferences(p_dataGrid, row, , cellItem) Then Return False

            bmChanged = resultsDataTable.ChangeBenchmark(row, cellItem.header, p_benchmarkRemoved)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return bmChanged
    End Function

    ''' <summary>
    ''' Changes the benchmark selection, updates the benchnmark &amp; query keys, and applies updated formatting for the selection change.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <param name="p_benchmarkRemoved">If 'True', the cell formatted will be the one marked as the currently selected benchmark.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBMSelection(ByVal p_dataGrid As DataGrid,
                                  Optional ByVal p_benchmarkRemoved As Boolean = False)
        Dim bmChanged As Boolean = False
        Dim row As DataRow = _currentDRVSelected.Row

        'Change benchmark
        bmChanged = SelectedCellChangeBMSelection(p_dataGrid, p_benchmarkRemoved)

        'Update benchmark & query key references
        If row IsNot Nothing Then resultsDataTable.SetKeysByDataRow(row)
    End Sub

    'Cell Formatting Controller Functions
    ''' <summary>
    ''' Adjusts formatting of cell background for selected cells, gets current selected column and DataRowView.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <param name="p_formatNameCurrentCell">Name of the formatting style to apply to the current selected cell.</param>
    ''' <param name="p_formatNameSelectedBM">Name of the formatting style to apply to the selected benchmark.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSelectedCellsFormatting(ByVal p_dataGrid As DataGrid,
                                         ByVal p_formatNameCurrentCell As String,
                                         ByVal p_formatNameSelectedBM As String)
        Try
            'Update formatting of deselected cell
            If _lastCellSelected IsNot Nothing Then
                If _IsLastCellSelectedBenchmarkSelected Then
                    'It should be reverted to that style
                    _lastCellSelected.Style = CType(Me.Resources(p_formatNameSelectedBM), Style)
                ElseIf Not _lastCellSelected.Column.Header.ToString = RESULT_DETAILS_HEADER Then
                    'Clear last formatting
                    _lastCellSelected.Style = Nothing
                End If
            End If

            'Format current selected cell
            If _currentCellSelected IsNot Nothing Then _currentCellSelected.Style = CType(Me.Resources(p_formatNameCurrentCell), Style)

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Adjusts formatting of the results details DataGrid cell based on the selected benchmark cell.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <param name="p_undoLastFormat">If 'True', cells from a prior selection will be used to apply the formatting change. Otherwise, the current selected cells are used.</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultDetailsFormat(ByVal p_dataGrid As DataGrid,
                                          Optional ByVal p_undoLastFormat As Boolean = False)
        Try
            Dim colIndex As Integer = GetColIndexFromHeader(p_dataGrid, RESULT_DETAILS_HEADER)
            Dim resultStatusCell As DataGridCell
            Dim row As DataRow
            Dim rowIndex As Integer

            'Apply prior BM selection to undo the last BM formatting change if applicable
            If (p_undoLastFormat AndAlso _lastBenchmarkCell IsNot Nothing) Then
                Dim dgRow As DataGridRow = GetRowFromCell(_lastBenchmarkCell)
                Dim keyQuery As Integer = myCInt(GetValueFromRowAndHeader(dgRow, KEY_QUERY_HEADER))

                row = resultsDataTable.GetDataRowByKey(keyQuery)(0)
                rowIndex = keyQuery
            Else
                row = _currentDRVSelected.Row
                rowIndex = GetRowIndexFromSelection(p_dataGrid)
            End If

            resultStatusCell = GetCellByRowColumnIndex(p_dataGrid, rowIndex, colIndex)

            'Assign cell style based on the status of the Result Details status cell
            UpdateRowResultDetailsCellStyles(row, resultStatusCell)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Adjusts formatting of the DataGrid cell for the selected benchmark cell, and resets all other benchmarks for the same query to the standard cell formatting.
    ''' </summary>
    ''' <param name="p_dataGrid">DataGrid object to use for the operation.</param>
    ''' <param name="p_undoLastFormat">If 'True', cells from a prior selection will be used to apply the formatting change. Otherwise, the current selected cells are used.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarksFormat(ByVal p_dataGrid As DataGrid,
                                       Optional ByVal p_undoLastFormat As Boolean = False)
        Try
            Dim dtRow As DataRow
            Dim dgRow As DataGridRow = Nothing
            Dim rowIndex As Integer

            Dim keyQuery As Integer

            'Apply prior BM selection to undo the last BM formatting change if applicable
            If (p_undoLastFormat AndAlso _lastBenchmarkCell IsNot Nothing) Then
                dgRow = GetRowFromCell(_lastBenchmarkCell)
                keyQuery = myCInt(GetValueFromRowAndHeader(dgRow, KEY_QUERY_HEADER))
                dtRow = resultsDataTable.GetDataRowByKey(keyQuery)(0)
                'rowIndex = keyQuery
            Else
                rowIndex = GetRowIndexFromSelection(p_dataGrid)
                dtRow = _currentDRVSelected.Row
            End If

            'Check all columns in the imported database region and assign the appropriate cell style based on the cell status
            UpdateRowBMCellStyles(p_dataGrid, dtRow, rowIndex, dgRow)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

End Class