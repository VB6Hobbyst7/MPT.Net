Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data

Imports MPT.Enums
Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary
Imports MPT.Verification.ObjectValidation

Imports CSiTester.cMCResult
Imports CSiTester.cMCQuery

''' <summary>
''' Creates a customized dataTable object for holding data that relates both to exported data from a CSi product as well as the correlated results object data.
''' The class also contains methods for adding, removing, and editing the results object information using the original dataTable, as well as for writing the results to an XML file.
''' </summary>
''' <remarks></remarks>
Public Class cTableQueryView
    Inherits PropertyChanger
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Enumerations"
    ''' <summary>
    ''' Regions demarcated within the DataTable object by column. Different regions have different formatting and behavior, and are meant for different purposes.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eDataGridRegion
        undefinedRegion = 1
        ''' <summary>
        ''' 1:1 corresponence with data imported from an external file. 
        ''' Static and not editable beyond formatting for tracking selections of queries and benchmarks.
        ''' </summary>
        ''' <remarks></remarks>
        importedDBTable = 2
        ''' <summary>
        ''' Status as to whether or not details for a given result benchmark can be added or are complete. 
        ''' Serves as an entry point to a form to complete the data and updates based on result details and benchmark selections. 
        ''' Formatting changes to indicate status.
        ''' If multiple benchmarks exist, this is for the following order of precedence: 1. Benchmark selection, 2. Incomplete benchmark of the highest column index #, 3. First benchmark of the lowest column index #.
        ''' </summary>
        ''' <remarks></remarks>
        resultsStatus = 3
        ''' <summary>
        ''' Additional data details set up for a given result benchmark. 
        ''' Serves as an entry point to a form to complete the data and updates based on selections.
        ''' If multiple benchmarks exist, this is for the following order of precedence: 1. Benchmark selection, 2. Incomplete benchmark of the highest column index #, 3. First benchmark of the lowest column index #.
        ''' </summary>
        ''' <remarks></remarks>
        resultsDetails = 4
        ''' <summary>
        ''' Contains unique numbers to serve as a row/query lookup key in the table. 
        ''' Is not visible.
        ''' Does not change in session.
        ''' </summary>
        ''' <remarks></remarks>
        keyQuery = 5
        ''' <summary>
        ''' Contains unique numbers to serve as a benchmark lookup key in the table. 
        ''' Is not visible.
        ''' Changes in session in response to user selection (primary) or incomplete benchmarks (secondary).
        ''' </summary>
        ''' <remarks></remarks>
        keyBenchmark = 6
        ''' <summary>
        ''' Columns mirror 1:1 the importedDBTable region, including header names &amp; the suffix "_qStatus" for convenient column syncing.
        ''' Either empty, or populated with values that indicate the status and usage of the corresponding cell in the importedDBTable region as follows: "", "partial" (item of incomplete query), "unique" (item of complete query), and "value" (item of benchmark).
        ''' Is not visible.
        ''' Changes in session in reaction to actions in the importedDBTable region.
        ''' </summary>
        ''' <remarks></remarks>
        queryStatus = 7

        'Table is divided up into regions as below. 
        'key & queryStatus aren hidden in the form.
        'xmlResults has different formatting than importedDBTable
        'Result Details has additional formatting to the xmlResults
        '|||------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|||
        '|||                  importedDBTable                     ||   resultsStatus  ||                resultsDetails                     ||  keyQuery  ||   keyBenchmark   ||                                        queryStatus                                                  |||
        '|||------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|||
        '|||   Story  |   Label   |   CaseCombo   |   UX  |   UY  ||  Result Details  ||   Comment |   Query   |   (other Result Details)  ||  Query Key ||   Benchmark Key  ||  Story_qStatus    |   Label_qStatus   |   CaseCombo_qStatus   |      UX_qStatus     |   UY_qStatus  |||
        '|||------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|||
        '|||    x     |     x     |               |   x   |       ||    /Add/Edit     ||           |           |                           ||      0     ||        0         ||   unique          |      unique       |                       |       value         |               |||
        '|||------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|||
        '|||          |     x     |               |       |       ||    /Add/Edit     ||           |           |                           ||      1     ||        3         ||                   |      partial      |                       | statusValueSelected |    value      |||
        '|||------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|||
    End Enum

    ''' <summary>
    ''' Values within the queryStatus region of the DataGrid that trigger different behaviors in the form.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eQueryBMStatus
        ''' <summary>
        ''' No action is taken.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("")> undefined = 1
        ''' <summary>
        ''' Part of a query that currently matches more than one row in the table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Partial Query")> queryPartial = 2
        ''' <summary>
        '''  Part of a query that matches only one row in the table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Unique Query")> queryUnique = 3
        ''' <summary>
        ''' Used as a benchmark value.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("BM Value")> bmValue = 4
        ''' <summary>
        ''' Used as a benchmark value and also currently selected in the table.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("BM Value Selected")> bmValueSelected = 5
        ''' <summary>
        ''' Used as a benchmark value, but the corresponding results details is incomplete.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("BM Value Details Incomplete")> bmDetailsIncomplete = 6
    End Enum

    ''' <summary>
    ''' Actions that can be taken for a given result benchmark details.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eResultDetailsStatus
        <Description("")> none = 1
        ''' <summary>
        ''' Add new details. Current details are incomplete for minimum required entries.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add")> add = 2
        ''' <summary>
        ''' Edit existing details. Current details are complete for minimum required entries.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Edit")> edit = 3
    End Enum

    ''' <summary>
    ''' Operations related to the result object that can be performed through the DataGrid.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eResultOperation
        <Description("None")> none = 1
        ''' <summary>
        ''' Adds selected cell's header as that for the benchmark value.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add Benchmark")> benchmarkAdd = 2
        ''' <summary>
        ''' Removes the current row's header assigned for the benchmark value.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Remove Benchmark")> benchmarkRemove = 3
        ''' <summary>
        ''' Adds selected cell to query.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add Query Field")> queryFieldAdd = 4
        ''' <summary>
        ''' Removes selected cell from query.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Remove Query Field")> queryFieldRemove = 5
        ''' <summary>
        ''' Adds the result details for the specified query if they are complete.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add/Edit Result Details")> resultDetailsAddEdit = 6
        ''' <summary>
        ''' Removes all query and benchmark, and comment assignments for the active row.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Remove Result")> resultRemove = 7
        ''' <summary>
        ''' Adds the additional query or benchmark item, depending on the current state of the selected row.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Add/Remove Benchmark/Query")> benchmarkQueryAddRemove = 8
    End Enum
#End Region

#Region "Constants: Private"
    Private Const TITLE_INCOMPLETE_QUERY As String = "Missing/Incomplete Queries"
    Private Const PROMPT_INCOMPLETE_QUERY As String = "Warning! Some queries are either not specified or not unique to a single row in this table. " & vbNewLine & vbNewLine & _
                        "Results with these incomplete queries will not be retained if you leave this table." & vbNewLine & vbNewLine & _
                        "Do you wish to complete these queries?"

    Private Const TITLE_INCOMPLETE_BENCHMARK As String = "Incomplete Benchmarks"
    Private Const PROMPT_INCOMPLETE_BENCHMARK As String = "Warning! Some benchmark entries have not been specified. " & vbNewLine & vbNewLine & _
                    "Results with these incomplete benchmarks will not be retained if you leave this table." & vbNewLine & vbNewLine & _
                    "Do you wish to complete these queries?"

    Private Const TITLE_INCOMPLETE_RESULT As String = "Incomplete Results"
    Private Const PROMPT_INCOMPLETE_RESULT As String = "Warning! Some results are incomplete. " & vbNewLine & vbNewLine & _
                        "These results will not be retained if you leave this table." & vbNewLine & vbNewLine & _
                        "Do you wish to complete these results?"

    Private Const TITLE_CONFLICT As String = "Use Conflict"
    Private Const PROMPT_CONFLICT_BENCHMARK As String = "The benchmark value is already set." & vbNewLine & vbNewLine & "Please remove the existing benchmark designation before attempting to add a new one."
    Private Const PROMPT_CONFLICT_QUERY_TO_BENCHMARK As String = "Selected value is already set to be part of the query for a benchmark value." & vbNewLine & vbNewLine & "Please choose another benchmark value, or remove the query designation from the current cell before adding it as the bechmark."
    Private Const PROMPT_CONFLICT_BENCHMARK_TO_QUERY As String = "Selected query value is already set to be the benchmark." & vbNewLine & vbNewLine & "Please choose another query value, or remove the benchmark designation from the current cell before adding it to the query."
#End Region

#Region "Constants: Friend"
    Friend Const RESULT_DETAILS_HEADER As String = "Result Details"
    Friend Const NAME_HEADER As String = "Name"
    Friend Const QUERY_HEADER As String = "Query"
    Friend Const BM_HEADER As String = "Benchmark"
    Friend Const BM_VALUE_HEADER As String = "Benchmark Values"
    Friend Const BM_ROUND_HEADER As String = "Benchmark Sig Figs"
    Friend Const BM_ISCORRECT_HEADER As String = "Correct"
    Friend Const THEORETICAL_HEADER As String = "Theoretical Result"
    Friend Const THEORETICAL_ROUND_HEADER As String = "Theoretical Sig Figs"
    Friend Const LAST_BEST_HEADER As String = "Last Best Result"
    Friend Const LAST_BEST_ROUND_HEADER As String = "Last Best Sig Figs"
    Friend Const UNITS_CONVERT_HEADER As String = "Convert Units"
    'Friend Const UNITS_HEADER As String = "Units" ' Hidden for now
    Friend Const PASSING_PERCENT_DIFFERENCE_HEADER As String = "Passing % Diff Range" '"Passing " & Chr(43) & Chr(47) & Chr(45) & " " & Chr(37) & " Diff"
    Friend Const SHIFT_CALC_HEADER As String = "% Difference Shift"
    Friend Const ZERO_TOLERANCE_HEADER As String = "Zero Tolerance"
    Friend Const KEY_QUERY_HEADER As String = "QueryKey"
    Friend Const KEY_BM_HEADER As String = "BenchmarkKey"

    Friend Const HEADER_SUFFIX As String = "_qStatus"
#End Region

#Region "Properties: Private"
    Private _convertYesNoUnknownEnum As eYesNoUnknown

    'Properties of static references within the DataTable
    ''' <summary>
    ''' Index number of the column that contains the Result Details.
    ''' </summary>
    ''' <remarks></remarks>
    Private _colIndexResultDetailsStatus As Integer
    ''' <summary>
    ''' Index number of the column that contains the row Key. This is the same as the query key.
    ''' </summary>
    ''' <remarks></remarks>
    Private _colIndexKey As Integer

    'Properties of the current selection
    ''' <summary>
    ''' Name of the currently displayed table.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentTableName As String

    ''' <summary>
    ''' Query key number of the current query row selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentQueryKeySelected As Integer
    ''' <summary>
    ''' Name of the header for the status column of the current benchmark selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private _currentBenchmarkSelected As String

    'Properties of the last selection
    ''' <summary>
    ''' Query key number of the last query row selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastQueryKeySelected As Integer
    ''' <summary>
    ''' Name of the header for the status column of the last benchmark selected.
    ''' </summary>
    ''' <remarks></remarks>
    Private _lastBenchmarkSelected As String
#End Region

#Region "Properties: Friend"
    Private _columnUnits As cDGUnitsBtns = New cDGUnitsBtns
    ''' <summary>
    ''' Class containing the list of headers in the original order from the exported table, with unit controller objects associated with each header.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property columnUnits As cDGUnitsBtns
        Set(ByVal value As cDGUnitsBtns)
            _columnUnits = value
            RaisePropertyChanged("columnUnits")
        End Set
        Get
            Return _columnUnits
        End Get
    End Property

    Private _exportedSetTable As DataTable
    ''' <summary>
    ''' Class containing the data of the exported tables file read into memory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property exportedSetTable As DataTable
        Set(ByVal value As DataTable)
            _exportedSetTable = value
            RaisePropertyChanged("exportedSetTable")
        End Set
        Get
            Return _exportedSetTable
        End Get
    End Property

    Private WithEvents _tableQueriesIncomplete As cTableQueries
    ''' <summary>
    ''' List of the result objects that have not been completed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property tableQueriesIncomplete As cTableQueries
        Get
            Return _tableQueriesIncomplete
        End Get
    End Property

    Private _tableQueriesTotal As cTableQueries
    ''' <summary>
    ''' Current cTableQuery class being used for the displayed table.
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property tableQueriesTotal As cTableQueries
        Get
            Return _tableQueriesTotal
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    ''' <summary>
    ''' Generates the customized variation of the dataTable class based on the provided parameters.
    ''' </summary>
    ''' <param name="p_dataSourcePath">Path to the database file read into the form.</param>
    ''' <param name="p_tableQueriesCollection">Collection of tableQueries to be searched for matching table query data.</param>
    ''' <param name="p_tableName">Name of the table to load in the DataTable object from the exported tables file. Also set as the initial table viewed. 
    ''' If not specified, the first table name contained in the tableQueries collection will be used.</param>
    ''' <param name="p_loadError">If true, then the class failed to load properly.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_dataSourcePath As String,
                    ByVal p_tableQueriesCollection As cTableQueriesCollection,
                    Optional ByVal p_tableName As String = "",
                    Optional ByRef p_loadError As Boolean = True)
        Try
            Dim dtController As New cDataTableController

            InitializeData()

            If String.IsNullOrEmpty(p_tableName) Then
                _currentTableName = p_tableQueriesCollection.item(0).tableName
            Else
                _currentTableName = p_tableName
            End If

            'Create data table from persistent data
            exportedSetTable = dtController.DataTableFromFile(p_dataSourcePath, ParseTableName(_currentTableName, False))
            For Each column As DataColumn In exportedSetTable.Columns
                columnUnits.AddHeader(column.ColumnName)
            Next

            If exportedSetTable IsNot Nothing Then
                AddColumnsResultDetails(exportedSetTable)

                'Set up cells for formatting unique queries & value
                AddColumnsQueryStatus(exportedSetTable)

                'Fill queries & value references into current dataTable
                _tableQueriesTotal = CurrentResultsCollection(_currentTableName, p_tableQueriesCollection)
                If _tableQueriesTotal IsNot Nothing Then
                    InitializeRecordsOfQueriesFromResults(exportedSetTable, _tableQueriesTotal)
                    p_loadError = False
                End If

                'Sets up incomplete results list
                UpdateIncompleteResultsCollection(_tableQueriesTotal.Results, _tableQueriesTotal)
            Else
                Throw New ArgumentException("No DataTable was generated for dtController. Form will not load.")
                Exit Sub
            End If
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    Private Sub InitializeData()
        _tableQueriesTotal = New cTableQueries
        _tableQueriesIncomplete = New cTableQueries
    End Sub
#End Region

#Region "Methods: Friend - Navigation/Query"
    ''' <summary>
    ''' Returns the column region enumeration for a given column according to the column index.
    ''' </summary>
    ''' <param name="p_colIndex">Column index, starting from 0.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ColumnRegionByColIndex(ByVal p_colIndex As Integer) As eDataGridRegion
        If p_colIndex < _colIndexResultDetailsStatus Then
            Return eDataGridRegion.importedDBTable
        ElseIf p_colIndex = _colIndexResultDetailsStatus Then
            Return eDataGridRegion.resultsStatus
        ElseIf p_colIndex < _colIndexKey Then
            Return eDataGridRegion.resultsDetails
        ElseIf p_colIndex = _colIndexKey Then
            Return eDataGridRegion.keyQuery
        ElseIf p_colIndex = _colIndexKey + 1 Then
            Return eDataGridRegion.keyBenchmark
        ElseIf p_colIndex > _colIndexKey + 1 Then
            Return eDataGridRegion.queryStatus
        Else
            Return eDataGridRegion.undefinedRegion
        End If
    End Function
    ''' <summary>
    ''' Returns column region enumeration for a column with the supplied header name. Returns 'undefinedRegion' if no header is found.
    ''' </summary>
    ''' <param name="p_header">Title of the header.</param>
    ''' <param name="p_dataTable">DataTable object containing the columns to be checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ColumnRegionByHeader(ByVal p_header As String,
                                         Optional ByVal p_dataTable As DataTable = Nothing) As eDataGridRegion
        Dim colIndex As Integer = 0
        Dim columnRegion As eDataGridRegion = eDataGridRegion.undefinedRegion

        If p_dataTable Is Nothing Then p_dataTable = exportedSetTable

        For Each column As DataColumn In p_dataTable.Columns
            If column.ColumnName = p_header Then
                columnRegion = ColumnRegionByColIndex(colIndex)
                Exit For
            End If
            colIndex += 1
        Next

        Return columnRegion
    End Function

    ''' <summary>
    ''' Returns the maximum index number for the columns within the imported columns region. Determines the maximum number of columns exported from the database. Returns -1 if no matches were found.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable to perform the operation on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetMaxColumnDBIndex(ByVal p_dataTable As DataTable) As Integer
        Dim colIndex As Integer = -1

        If p_dataTable Is Nothing Then Return colIndex

        'Determine maximum numebr of columns exported from the database
        For Each column As DataColumn In p_dataTable.Columns
            If ColumnRegionByHeader(column.ColumnName, p_dataTable) = eDataGridRegion.importedDBTable Then colIndex += 1
        Next

        Return colIndex
    End Function

    ''' <summary>
    ''' Updates the 'last selected' and 'current selected' key references.
    ''' </summary>
    ''' <param name="p_row">Selected DataRow to use for determining the current selection keys.</param>
    ''' <remarks></remarks>
    Friend Sub SetKeysByDataRow(ByVal p_row As DataRow)
        'Update last selected
        _lastQueryKeySelected = _currentQueryKeySelected
        _lastBenchmarkSelected = _currentBenchmarkSelected

        If p_row Is Nothing Then Exit Sub

        'Update current selected
        _currentQueryKeySelected = CInt(p_row.Item(KEY_QUERY_HEADER).ToString)
        If Not String.IsNullOrEmpty(p_row.Item(BM_HEADER).ToString) Then
            _currentBenchmarkSelected = GetCellStatusTagHeader(p_row.Item(BM_HEADER).ToString)
        Else
            _currentBenchmarkSelected = ""
        End If
    End Sub

    ''' <summary>
    ''' Returns the array of row objects that are selected based on the supplied query key and benchmark keys.
    ''' </summary>
    ''' <param name="p_keyQuery">Key for the query that select the rows.</param>
    ''' <param name="p_keyBenchmark">Key for the benchmarks that select the rows.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetDataRowByKey(ByVal p_keyQuery As Integer,
                                    Optional ByVal p_keyBenchmark As String = "") As DataRow()
        Try
            Dim expression As String

            expression = KEY_QUERY_HEADER & " = " & p_keyQuery

            If Not String.IsNullOrEmpty(p_keyBenchmark) Then
                expression = expression & " AND " & KEY_BM_HEADER & " = " & p_keyBenchmark
            End If

            'Return corresponding DataRow object in base DataTable object
            Return exportedSetTable.Select(expression)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns a list of all of the benchmark column indices for a single table row, in the order in which they appear in the table columns.
    ''' </summary>
    ''' <param name="p_row">DataRow object that the routine searches.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetRowBenchmarkColumnIndices(ByVal p_row As DataRow) As List(Of Integer)
        Dim columnIndex As Integer = 0
        Dim columnIndexOffset As Integer = 0
        Dim bmColIndexList As New List(Of Integer)

        If p_row Is Nothing Then Return bmColIndexList

        For Each column As DataColumn In p_row.Table.Columns
            If ColumnRegionByHeader(column.ColumnName, p_row.Table) = eDataGridRegion.queryStatus Then
                If CellUsedAsBenchmark(p_row.Item(columnIndex).ToString) Then
                    bmColIndexList.Add(columnIndexOffset)
                End If
                columnIndexOffset += 1
            End If
            columnIndex += 1
        Next

        Return bmColIndexList
    End Function

    ''' <summary>
    ''' Returns the header for the status tag column for the associated header provided.
    ''' </summary>
    ''' <param name="p_header">Header to get the corresponding status tag column header from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCellStatusTagHeader(ByVal p_header As String) As String
        Dim cellStatusTagHeader As String

        If (String.IsNullOrEmpty(p_header) OrElse StringExistInName(p_header, HEADER_SUFFIX)) Then
            cellStatusTagHeader = p_header
        Else
            cellStatusTagHeader = p_header & HEADER_SUFFIX
        End If

        Return cellStatusTagHeader
    End Function

    ''' <summary>
    ''' Returns the query/benchmark status of the cell indicated by the provided header &amp; row.
    ''' </summary>
    ''' <param name="p_header">Header name of the cell to check.</param>
    ''' <param name="p_row">DataRow to check the status in.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCellStatus(ByVal p_header As String,
                                  ByVal p_row As DataRow) As eQueryBMStatus
        Dim statusTagHeader As String = GetCellStatusTagHeader(p_header)
        Dim columnIndex As Integer = 0
        Dim cellStatusString As String

        If p_row Is Nothing Then Return eQueryBMStatus.undefined

        For Each column As DataColumn In p_row.Table.Columns
            If ColumnRegionByHeader(column.ColumnName, p_row.Table) = eDataGridRegion.queryStatus Then
                If column.ColumnName = statusTagHeader Then
                    cellStatusString = p_row.Item(columnIndex).ToString

                    If Not String.IsNullOrEmpty(cellStatusString) Then Return ConvertStringToEnumByDescription(Of eQueryBMStatus)(cellStatusString)
                End If
            End If
            columnIndex += 1
        Next

        Return eQueryBMStatus.undefined
    End Function

    ''' <summary>
    ''' Returns the number of benchmarks that have been declared for the DataRow provided.
    ''' </summary>
    ''' <param name="p_row">DataRow object within which the items tagged as benchmarks are counted.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CountBenchmark(ByVal p_row As DataRow) As Integer
        Dim bmCountCurrent As Integer = -1
        Dim header As String
        Dim colRegion As eDataGridRegion

        If p_row Is Nothing Then Return bmCountCurrent

        bmCountCurrent = 0
        For Each column As DataColumn In exportedSetTable.Columns
            header = column.ColumnName
            colRegion = ColumnRegionByHeader(header, exportedSetTable)

            If colRegion = eDataGridRegion.queryStatus Then
                If (p_row.Item(header).ToString = GetEnumDescription(eQueryBMStatus.bmValue) OrElse
                    p_row.Item(header).ToString = GetEnumDescription(eQueryBMStatus.bmDetailsIncomplete) OrElse
                    p_row.Item(header).ToString = GetEnumDescription(eQueryBMStatus.bmValueSelected)) Then

                    bmCountCurrent += 1
                End If
            End If
        Next

        Return bmCountCurrent
    End Function
#End Region

#Region "Methods: Friend - Results Validation"
    ''' <summary>
    ''' Checks if a specified cell is already set, such as a query item being added as a benchmark value, or a benchmark value being added as a query item.
    ''' </summary>
    ''' <param name="p_row">DataRow object containing the row of data selected in the datagrid.</param>
    ''' <param name="p_header">Header name of the column of the cell being checked.</param>
    ''' <param name="p_IsQuery">If true, item is to be checked against a query item. If false, item is to be checked against a benchmark item.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function BenchmarkQueryConflict(ByVal p_row As DataRow,
                                             ByVal p_header As String,
                                             ByVal p_IsQuery As Boolean) As Boolean
        Try
            If Not IsValidObjectDBStringFilled(p_row.Item(p_header)) Then Return False
            Dim cellStatusTagHeader As String = GetCellStatusTagHeader(p_header)
            Dim cellValue As String = p_row.Item(cellStatusTagHeader).ToString

            If (Not p_IsQuery AndAlso
                CellUsedAsQuery(cellValue)) Then
                'Conflict with query designation
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                            PROMPT_CONFLICT_QUERY_TO_BENCHMARK,
                                                            TITLE_CONFLICT))
                Return True
            ElseIf (p_IsQuery AndAlso
                    CellUsedAsBenchmark(cellValue)) Then
                'Conflict with BM designtation
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                            PROMPT_CONFLICT_BENCHMARK_TO_QUERY,
                                                            TITLE_CONFLICT))
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Returns the status according to whether the provided query returns one or multiple matches in the provided DataTable. 
    ''' </summary>
    ''' <param name="p_queryString">Query string to use in searching for a row.</param>
    ''' <param name="p_DataTable">DataTable object checked for query status.</param>
    ''' <remarks></remarks>
    Friend Function QueryStatus(ByVal p_queryString As String,
                                ByVal p_DataTable As DataTable) As String

        If p_DataTable Is Nothing Then Return GetEnumDescription(eQueryBMStatus.undefined)

        Try
            If String.IsNullOrEmpty(p_queryString) Then  'Filter might return different # of matches, but it is not a correct query
                Return GetEnumDescription(eQueryBMStatus.undefined)
            Else
                Dim queryMatches As Integer = p_DataTable.Select(p_queryString).Count

                Select Case queryMatches
                    Case 0 : Return GetEnumDescription(eQueryBMStatus.undefined)
                    Case 1 : Return GetEnumDescription(eQueryBMStatus.queryUnique)
                    Case Else : Return GetEnumDescription(eQueryBMStatus.queryPartial)
                End Select
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return GetEnumDescription(eQueryBMStatus.undefined)
    End Function

    ''' <summary>
    ''' Checks to see the completion status of the results and only saves completes results. 
    ''' User is given the option to abort losing incomplete results in order to correct them. 
    ''' Otherwise, current table queries are updated to only include complete queries. 
    ''' Returns True if actions following this function are expected to be aborted and the original table queries are preserved.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable from which to look up the queries &amp; benchmarks.</param>
    ''' <param name="p_tableQueries">cTableQueries to be updated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ResultsNeedCompletion(Optional ByRef p_dataTable As DataTable = Nothing,
                                          Optional ByRef p_tableQueries As cTableQueries = Nothing) As Boolean
        Dim updateTableQueries As Boolean = False
        Dim queryUnique As Boolean = True
        Dim benchmarkComplete As Boolean = True
        Dim detailsComplete As Boolean = True
        Dim completeResults As New List(Of cMCResult)
        Dim incompleteResults As New List(Of cMCResult)

        Try
            If p_dataTable Is Nothing Then p_dataTable = exportedSetTable
            If p_tableQueries Is Nothing Then p_tableQueries = _tableQueriesTotal

            For Each result As cMCResult In p_tableQueries.Results
                'Determine example completion state
                If Not result.isBMComplete Then benchmarkComplete = False
                If Not result.isDetailsComplete Then detailsComplete = False
                If Not result.query.isUnique Then queryUnique = False

                result.GetQueryType(p_dataTable, True)

                'If example is complete, save it, else, don't save it and mark that incomplete examples exist
                If (result.isComplete AndAlso result.query.isUnique(p_dataTable, result.tableName)) Then
                    completeResults.Add(result)
                Else
                    incompleteResults.Add(result)
                End If
            Next

            'Determine if incomplete results are to be saved for correction, or dropped for changing views
            If Not queryUnique Then
                updateTableQueries = Not CompleteIncompleteResults(PROMPT_INCOMPLETE_QUERY, TITLE_INCOMPLETE_QUERY)
            ElseIf Not benchmarkComplete Then
                updateTableQueries = Not CompleteIncompleteResults(TITLE_INCOMPLETE_BENCHMARK, PROMPT_INCOMPLETE_BENCHMARK)
            ElseIf Not detailsComplete Then
                updateTableQueries = Not CompleteIncompleteResults(PROMPT_INCOMPLETE_RESULT, TITLE_INCOMPLETE_RESULT)
            Else
                updateTableQueries = True
            End If

            'Save current complete set of results or retain list of incomplete results
            If updateTableQueries Then
                _tableQueriesTotal.Results = completeResults
                Return False
            Else
                ReplaceIncompleteResults(incompleteResults)
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return True
    End Function
#End Region

#Region "Methods: Friend - Actions to add/remove cells from query, BM, add result details, etc."
    ''' <summary>
    ''' Adds the additional query or benchmark item, depending on the current state of the selected row.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to which to apply the routine.</param>
    ''' <param name="p_cellItem">Header and associated value of the item being added or removed as a benchmark or query.</param>
    ''' <param name="p_row">DataRow object for the row of interest. If not provided, it is assumed to come from the selected row.</param>
    ''' <param name="p_resultOperation">Returned value of waht query component or BM operation was performed.</param>
    ''' <remarks></remarks>
    Friend Sub AddRemoveQueryComponentOrBM(ByVal p_row As DataRow,
                                     ByVal p_cellItem As cHeaderAndValue,
                                     Optional ByVal p_dataTable As DataTable = Nothing,
                                     Optional ByRef p_resultOperation As eResultOperation = eResultOperation.none)

        If p_row Is Nothing Then Exit Sub
        If p_dataTable Is Nothing Then p_dataTable = exportedSetTable

        Dim bmCount As Integer = CountBenchmark(p_row)
        Dim currentRowQueryStatus As eQueryBMStatus

        'Remove benchmark or query
        p_resultOperation = ClearCellStatusQueryOrBM(p_row, p_cellItem)

        If Not p_resultOperation = eResultOperation.none Then
            'Current states associated with the selected cell, if any, have been undone.
        Else                                                'Add cell as new benchmark or query
            'Check row status to determine whether to add a benchmark or query
            currentRowQueryStatus = RowQueryStatus(p_row)

            'Perform action based on row status      
            If currentRowQueryStatus = eQueryBMStatus.queryUnique Then
                If bmCount > 0 Then                 'Add duplicated result details and additional benchmark
                    AddBenchmark(p_row, p_cellItem.header)
                Else                                'Add as benchmark
                    AddBenchmark(p_row, p_cellItem.header)
                    AddResultDetailsStatus(p_row)   'Only added for first benchmark.
                End If
                p_resultOperation = eResultOperation.benchmarkAdd
            Else                                    'Partial query. Add to query
                UpdateQuery(p_row, p_cellItem, True)
                p_resultOperation = eResultOperation.queryFieldAdd
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates the query to include or remove the specified cell item.
    ''' </summary>
    ''' <param name="p_row">Row where the query is being updated.</param>
    ''' <param name="p_cellItem">Header-value pair that is to be added or removed from the query.</param>
    ''' <param name="p_addChange">If 'True', the cell item is added to the query if it is not already included. Otherwise, the item is removed from the query if it exists in the query.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateQuery(ByVal p_row As DataRow,
                            ByVal p_cellItem As cHeaderAndValue,
                            ByVal p_addChange As Boolean)
        Try
            If p_row Is Nothing Then Exit Sub
            If BenchmarkQueryConflict(p_row, p_cellItem.header, True) Then Exit Sub

            'Get current value, accounting for blank values
            Dim queryStringCurrent As String
            If IsDBNull(p_row.Item(QUERY_HEADER)) Then
                queryStringCurrent = ""
            Else
                queryStringCurrent = p_row.Item(QUERY_HEADER).ToString
            End If
            If (Not p_addChange AndAlso
                String.IsNullOrEmpty(queryStringCurrent)) Then Exit Sub

            'Assemble the current query 
            Dim queryStringNew As String = UpdateQueryString(queryStringCurrent, p_cellItem, p_addChange)
            If queryStringNew = queryStringCurrent Then Exit Sub

            'Update the DataTable
            Dim cellStatusQuery As String = QueryStatus(queryStringNew, exportedSetTable)   'Determine if new query returns one or more matches and classify accordingly
            Dim cellStatusTagHeader As String = GetCellStatusTagHeader(p_cellItem.header)
            UpdateRowQuery(p_row, queryStringNew, cellStatusQuery, cellStatusTagHeader, p_addChange)

            'Update the Result Object
            If (Not p_addChange AndAlso IsResultEmpty(p_row)) Then  'Remove the object
                RemoveMCResultsByDataRow(p_row)
            Else                                                    'Update the object
                WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False)

                Dim keyQuery As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
                UpdateAllResultQueries(_tableQueriesTotal.Results, exportedSetTable, keyQuery)
                UpdateAllResultQueries(_tableQueriesIncomplete.Results, exportedSetTable, keyQuery)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Adds selected cell's header as that for the benchmark value.
    ''' </summary>
    ''' <param name="p_bmHeader">Header of the benchmark to add.</param>
    ''' <param name="p_row">DataRow object to which to apply the function.</param>
    ''' <remarks></remarks>
    Friend Sub AddBenchmark(ByRef p_row As DataRow,
                            ByVal p_bmHeader As String)
        Try
            If (p_row Is Nothing OrElse
                BenchmarkQueryConflict(p_row, p_bmHeader, False)) Then Exit Sub

            ResetBenchmarkSelectionStatuses(p_row)

            'Add new cell status tag
            Dim cellStatusTagHeader As String = GetCellStatusTagHeader(p_bmHeader)
            p_row.Item(cellStatusTagHeader) = GetEnumDescription(eQueryBMStatus.bmValueSelected)

            'Add benchmark cell value
            p_row.Item(BM_HEADER) = p_bmHeader
            p_row.Item(BM_VALUE_HEADER) = p_row.Item(p_bmHeader).ToString

            ' If result details name/comment is not blank for the last selected benchmark, copy it with a suffix for the new benchmark
            ' Make sure to eliminate any potential suffix from prior benchmarks
            If Not String.IsNullOrEmpty(p_row.Item(NAME_HEADER).ToString) Then
                Dim newName As String = FilterStringFromName(p_row.Item(NAME_HEADER).ToString, "(" & GetSuffix(p_row.Item(NAME_HEADER).ToString, "("), p_retainPrefix:=True, p_retainSuffix:=False)
                p_row.Item(NAME_HEADER) = newName & "(" & p_bmHeader & ")"
                'p_row.Item(NAME_HEADER) = p_row.Item(NAME_HEADER).ToString & "(" & p_bmHeader & ")"
            End If

            'Update query list
            WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False, , , True)

            'Update selected BM Index
            Dim queryKey As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
            p_row.Item(KEY_BM_HEADER) = GetBenchmarkKey(queryKey, p_bmHeader)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns the benchmark ID key based on the provided row and header.
    ''' </summary>
    ''' <param name="p_queryKey">Query key ID that is used to limit the group of benchmarks searched.</param>
    ''' <param name="p_header">Header to use to look up the ID.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetBenchmarkKey(ByVal p_queryKey As Integer, ByVal p_header As String) As Integer
        Dim keyBM As Integer = -1

        For Each result As cMCResult In _tableQueriesTotal.Results
            If (result.query.ID = p_queryKey AndAlso result.benchmark.name = p_header) Then
                keyBM = result.benchmark.ID
                Exit For
            End If
        Next

        Return keyBM
    End Function

    ''' <summary>
    ''' Returns the header of the benchmark specified.
    ''' </summary>
    ''' <param name="p_row">Row containing the benchmark specified.</param>
    ''' <param name="p_benchmarkKey">ID of the benchmark to obtain the header from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetBenchmarkHeader(ByVal p_row As DataRow,
                                        ByVal p_benchmarkKey As Integer) As String
        Dim benchmarkHeader As String = ""
        If p_row Is Nothing Then Return benchmarkHeader

        If (p_benchmarkKey = -1 AndAlso IsValidObjectDBStringFilled(p_row.Item(BM_HEADER))) Then
            benchmarkHeader = p_row.Item(BM_HEADER).ToString
        ElseIf IsValidObjectDBStringFilled(p_row.Item(KEY_QUERY_HEADER)) Then
            Dim queryKeyRemove As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
            Dim result As cMCResult = tableQueriesTotal.GetResultObject(queryKeyRemove, p_benchmarkKey)

            If result IsNot Nothing Then benchmarkHeader = result.benchmark.name
        End If

        Return benchmarkHeader
    End Function

    ''' <summary>
    ''' Removes the current row's header assigned for the benchmark value.
    ''' </summary>
    ''' <param name="p_row">DataRow object to which to apply the function.</param>
    ''' <param name="p_bmHeader">Header of the benchmark to remove.</param>
    ''' <remarks></remarks>
    Friend Sub RemoveBenchmark(ByVal p_row As DataRow,
                               ByVal p_bmHeader As String)
        Try
            If (p_row Is Nothing OrElse
                BenchmarkQueryConflict(p_row, p_bmHeader, False)) Then Exit Sub

            'This is done instead of supplying the header as a parameter as this 
            'looks up for the currently selected BM rather than the currently selected cell
            Dim statusTagCellHeader As String = GetCellStatusTagHeader(p_bmHeader)

            'Clear cell status tag
            p_row.Item(statusTagCellHeader) = ""

            'Remove selected BM data if it is the BM key specified
            If p_bmHeader = p_row.Item(BM_HEADER).ToString Then
                p_row.Item(BM_HEADER) = ""
                p_row.Item(BM_VALUE_HEADER) = ""
            End If

            'Clear result details if this is the last benchmark
            If CountBenchmark(p_row) <= 0 Then ClearResultDetailsEntries(p_row)

            'Update MC Result objects
            RemoveBenchmarkFromResultsList(p_row, p_bmHeader)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Changes the entries in the DataTable object based on a specified benchmark by header and row object. 
    ''' Returns 'True' if a benchmark was successfully changed (changing to the same benchmark returns 'True' as well).
    ''' </summary>
    ''' <param name="p_row">DataRow object upon which the routine will act.</param>
    ''' <param name="p_header">Name of the benchmark entry for which the DataTable should populate data.</param>
    ''' <param name="p_benchmarkRemoved">If 'True', the cell formatted will be the one marked as the currently selected benchmark.</param>
    ''' <remarks></remarks>
    Friend Function ChangeBenchmark(ByRef p_row As DataRow,
                                    ByVal p_header As String,
                                    Optional ByVal p_benchmarkRemoved As Boolean = False) As Boolean
        Dim mcResult As cMCResult
        Dim queryKey As Integer
        Dim bmHeader As String

        If (p_row Is Nothing OrElse String.IsNullOrEmpty(p_header)) Then Return False
        bmHeader = p_row.Item(BM_HEADER).ToString

        'No need to change benchmark if it is the same or does not exist
        If (bmHeader = p_header OrElse
            (String.IsNullOrEmpty(bmHeader) AndAlso
             CountBenchmark(p_row) = 0)) Then Return False

        Try
            queryKey = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)

            mcResult = _tableQueriesTotal.GetResultObject(queryKey, p_header)

            'Case where a benchmark is removed, as the BM column is still selected but the BM should be changed
            If (mcResult Is Nothing AndAlso p_benchmarkRemoved) Then
                mcResult = _tableQueriesTotal.GetResultObject(queryKey, bmHeader)

                If mcResult Is Nothing Then
                    Dim bmKeyDefault As Integer = GetBenchmarkSelectionIndexDefault(p_row, _tableQueriesTotal)
                    mcResult = _tableQueriesTotal.GetResultObject(queryKey, bmKeyDefault)
                End If
            End If

            If mcResult Is Nothing Then
                Return False
            Else
                ResetBenchmarkSelectionStatuses(p_row)
                UpdateSelectedBMEntry(p_row, mcResult)
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Adds the result details for the specified query if they are complete.
    ''' </summary>
    ''' <param name="p_row">DataRow object to which to apply the function.</param>
    ''' <param name="p_rowChange">If 'True', the result object being added/edited is of a different query than the last result object added/edited.</param>
    ''' <remarks></remarks>
    Friend Sub AddResultDetailsFromForm(ByVal p_row As DataRow,
                                        Optional ByVal p_rowChange As Boolean = True)
        Dim xmlResultIndex As Integer = 0
        Dim resultUpdated As cMCResult = Nothing
        Dim keyQuery As Integer
        Dim keyBenchmark As Integer

        Try
            If p_row Is Nothing Then Exit Sub

            keyQuery = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
            keyBenchmark = myCInt(p_row.Item(KEY_BM_HEADER).ToString)

            'Get the result class
            For Each result As cMCResult In _tableQueriesTotal.Results
                If (result.query.ID = keyQuery AndAlso result.benchmark.ID = keyBenchmark) Then

                    'Open form to edit result class
                    Dim myResultDetails As New frmXMLObjectResultDetails(result)
                    myResultDetails.ShowDialog()

                    'Check that result details are complete before updating
                    If myResultDetails.ResultDetailsComplete Then
                        p_row.Item(RESULT_DETAILS_HEADER) = GetEnumDescription(eResultDetailsStatus.edit)

                        AddResultDetailsFromMCResult(p_row, myResultDetails.resultSave)

                        resultUpdated = CType(myResultDetails.resultSave.Clone, cMCResult)
                    End If
                    Exit For
                End If
                xmlResultIndex += 1
            Next

            If resultUpdated IsNot Nothing Then
                'Add new result object back into collection
                With _tableQueriesTotal
                    .Results(xmlResultIndex) = resultUpdated
                    .UpdateSortAllBenchmarkIDs()
                End With

                'Update list of complete/incomplete results
                AddResultByKeysCompleteAsIncomplete(resultUpdated.query.ID, resultUpdated.benchmark.ID)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    ''' <summary>
    ''' Removes all query and benchmark, and comment assignments for the active row and the corresponding results object(s). Returns 'True' if successful.
    ''' </summary>
    ''' <param name="p_row">DataRow object to which to apply the function.</param>
    ''' <remarks></remarks>
    Friend Function RemoveRowResults(ByVal p_row As DataRow) As Boolean
        Dim dataCleared As Boolean = True
        Dim header As String
        Dim colRegion As eDataGridRegion = eDataGridRegion.undefinedRegion

        If p_row Is Nothing Then Return False

        Try
            'Clear all entries in the DataTable
            For Each column As DataColumn In exportedSetTable.Columns
                header = column.ColumnName
                colRegion = ColumnRegionByHeader(header, exportedSetTable)
                'Clear all columns except the imported database and the key columns
                If Not (colRegion = eDataGridRegion.importedDBTable OrElse _
                        colRegion = eDataGridRegion.keyQuery OrElse _
                        colRegion = eDataGridRegion.keyBenchmark) Then
                    p_row.Item(header) = Nothing
                ElseIf colRegion = eDataGridRegion.keyBenchmark Then
                    p_row.Item(header) = 0
                End If
            Next

            'Update MC Result objects
            If dataCleared Then dataCleared = RemoveMCResultsByDataRow(p_row)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return dataCleared
    End Function

    ''' <summary>
    ''' Removes all incomplete result objects from the DataTable and TableQueries.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveIncompleteResults()
        Dim tempResults As New ObservableCollection(Of cMCResult)

        Try
            'Create static collection, since iterations will otherwise update the collection
            For Each result As cMCResult In tableQueriesIncomplete.Results
                tempResults.Add(result)
            Next

            For Each result As cMCResult In tempResults
                Dim row As DataRow() = GetDataRowByKey(result.query.ID)

                'See if row still exists, since multiple rows might have been removed, and remove the next result(s)
                If (row IsNot Nothing AndAlso row.Count > 0) Then
                    If Not result.query.isSpecified Then
                        'Remove all results for that row
                        RemoveRowResults(row(0))
                    Else
                        'Remove the benchmark
                        Dim bmHeaderRemove As String = GetBenchmarkHeader(row(0), result.benchmark.ID)
                        RemoveBenchmark(row(0), bmHeaderRemove)
                        If CountBenchmark(row(0)) = 0 Then
                            RemoveRowResults(row(0))
                        Else
                            ChangeBenchmark(row(0), bmHeaderRemove, True)
                            SetKeysByDataRow(row(0))
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Friend - Reading/Writing"
    ''' <summary>
    ''' Creates a new cMCResult object populated with data from a DataRow object from a DataTable object.
    ''' </summary>
    ''' <param name="p_row">DataRow object for the row to check in the DataTable.</param>
    ''' <param name="p_RequireResultsDetails">If 'true', then the additional criteria of results details is required before a row is considered ready for updating.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CreateMCResultFromRow(ByVal p_row As DataRow,
                                          Optional ByVal p_RequireResultsDetails As Boolean = False) As cMCResult

        If (p_row Is Nothing AndAlso
            p_RequireResultsDetails AndAlso
            IsDBNull(p_row.Item(QUERY_HEADER))) Then Return Nothing

        Dim newResult As New cMCResult

        Try
            With newResult
                .tableName = ParseTableName(p_row.Table.TableName, True)
                .query.ID = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
                .benchmark.ID = myCInt(p_row.Item(KEY_BM_HEADER).ToString)

                'Set queries
                .query.SetQuery(p_row.Item(QUERY_HEADER).ToString)
                .GetQueryType(p_row.Table, True)

                'Set benchmarks
                With .benchmark
                    If Not p_row.Item(BM_HEADER).ToString = .name Then
                        .name = p_row.Item(BM_HEADER).ToString
                        If Not String.IsNullOrEmpty(.name) Then
                            .valueTable = p_row.Item(.name).ToString
                            If String.IsNullOrEmpty(p_row.Item(BM_VALUE_HEADER).ToString) Then
                                .valueBenchmark = .valueTable
                            Else
                                .valueBenchmark = p_row.Item(BM_VALUE_HEADER).ToString
                            End If
                        Else
                            .valueTable = ""
                            .valueBenchmark = ""
                        End If
                    End If
                    .roundBenchmark = p_row.Item(BM_ROUND_HEADER).ToString
                    .isCorrect = CType(ConvertYesNoUnknownEnum(p_row.Item(BM_ISCORRECT_HEADER).ToString), eYesNoUnknown)
                    .valueTheoretical = p_row.Item(THEORETICAL_HEADER).ToString
                    .roundTheoretical = p_row.Item(THEORETICAL_ROUND_HEADER).ToString
                    .valueLastBest = p_row.Item(LAST_BEST_HEADER).ToString
                    .roundLastBest = p_row.Item(LAST_BEST_ROUND_HEADER).ToString
                    If String.IsNullOrEmpty(p_row.Item(PASSING_PERCENT_DIFFERENCE_HEADER).ToString) Then p_row.Item(PASSING_PERCENT_DIFFERENCE_HEADER) = 0
                    .valuePassingPercentDifferenceRange = CDbl(p_row.Item(PASSING_PERCENT_DIFFERENCE_HEADER).ToString)
                    If String.IsNullOrEmpty(p_row.Item(SHIFT_CALC_HEADER).ToString) Then p_row.Item(SHIFT_CALC_HEADER) = 0
                    .shiftCalc = CDbl(p_row.Item(SHIFT_CALC_HEADER).ToString)
                    If String.IsNullOrEmpty(p_row.Item(ZERO_TOLERANCE_HEADER).ToString) Then p_row.Item(ZERO_TOLERANCE_HEADER) = 0
                    .zeroTolerance = CDbl(p_row.Item(ZERO_TOLERANCE_HEADER).ToString)
                End With


                '.units = p_row.Item(UNITS_HEADER).ToString ' Hidden for now
                .unitsConversion = ConvertTrueTrueBoolean(p_row.Item(UNITS_CONVERT_HEADER).ToString)

                'Set name
                .name = p_row.Item(NAME_HEADER).ToString
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return newResult
    End Function

    ''' <summary>
    ''' Writes the current table query and benchmark values into the corresponding DataTable class and updates the result objects list accordingly.
    ''' </summary>
    ''' <param name="p_dataTable">Data table from which to look up the queries &amp; benchmarks.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes. 
    ''' If not specified, the class' tableQueries object is used.</param>
    ''' <param name="p_requireResultsDetails">If 'False', then results are saved if there is a unique query &amp; benchmark. If 'true', then the additional criteria of results details is required.</param>
    ''' <param name="p_benchmarkNameRemove">Benchmark key that corresponds to the result objects of the specified query not be removed.</param>
    ''' <param name="p_queryKeyRemove">Query name that corresponds to the result objects to be removed.</param>
    ''' <remarks></remarks>
    Friend Sub WriteRecordsOfQueriesToResults(Optional ByVal p_dataTable As DataTable = Nothing,
                                              Optional ByRef p_tableQueries As cTableQueries = Nothing,
                                              Optional ByVal p_requireResultsDetails As Boolean = True,
                                              Optional ByVal p_queryKeyRemove As Integer = -1,
                                              Optional ByVal p_benchmarkNameRemove As String = "",
                                              Optional ByVal p_renumberBenchmarksBeforeSort As Boolean = False)
        Try
            If p_dataTable Is Nothing Then p_dataTable = exportedSetTable
            If p_tableQueries Is Nothing Then p_tableQueries = _tableQueriesTotal

            Dim filterRows() As DataRow
            Dim mcResultsDisplayed As New List(Of cMCResult)
            Dim mcResultsTotal As New List(Of cMCResult)

            'Add new MCResult objects for the currently selected benchmarks in the table
            filterRows = p_dataTable.Select(QUERY_HEADER & " Is Not Null Or " & BM_HEADER & " Is Not Null")

            For Each row As DataRow In filterRows
                'If last result for a row is removed, clean up any non-string values that should be blank rather than 0
                If CountBenchmark(row) = 0 Then
                    row.Item(SHIFT_CALC_HEADER) = ""
                    row.Item(ZERO_TOLERANCE_HEADER) = ""
                End If

                Dim newResult As cMCResult = CreateMCResultFromRow(row, p_requireResultsDetails)
                If (newResult IsNot Nothing AndAlso
                    (Not (String.IsNullOrEmpty(row.Item(QUERY_HEADER).ToString) AndAlso
                            String.IsNullOrEmpty(row.Item(BM_HEADER).ToString)))) Then       'Last comparison is for removing the last benchmark of a result that is not to be removed.

                    mcResultsDisplayed.Add(newResult)
                End If
            Next

            mcResultsTotal = UpdateResultsCollectionNotDisplayed(p_tableQueries, mcResultsDisplayed)

            If p_queryKeyRemove >= 0 Then
                Dim benchmarkKey As Integer = GetBenchmarkKey(p_queryKeyRemove, p_benchmarkNameRemove)
                mcResultsTotal = UpdateResultsCollectionToRemove(mcResultsTotal, p_queryKeyRemove, benchmarkKey)
            End If

            'Update MC Results collection
            p_tableQueries.Results = mcResultsTotal

            'Update the list of incomplete results
            UpdateIncompleteResultsCollection(mcResultsTotal, p_tableQueries)

            'Update sorting, and any necessary renumbering of BMs
            UpdateAllKeysAndSorting(p_renumberBenchmarksBeforeSort)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Removes all result objects that are incomplete in the current table queries object.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveIncompleteResultsAll()
        For Each result As cMCResult In _tableQueriesIncomplete.Results
            If Not (result.isComplete AndAlso result.query.isUnique) Then
                WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False, result.query.ID, result.benchmark.name)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Takes the separated classes of results and combines them back into a single collection for re-merging back with the control model class.
    ''' </summary>
    ''' <param name="p_tableQueriesCollection">TableQueries collection from which the results are gathered.</param>
    ''' <remarks></remarks>
    Friend Function WriteToXMLResultsClass(ByVal p_tableQueriesCollection As cTableQueriesCollection) As List(Of cMCResult)
        Dim allXmlResultsNew As New List(Of cMCResult)

        If p_tableQueriesCollection Is Nothing Then Return Nothing

        For Each dataTableObject As cTableQueries In p_tableQueriesCollection
            For Each xmlResult As cMCResult In dataTableObject.Results
                allXmlResultsNew.Add(xmlResult)
            Next
        Next

        Return allXmlResultsNew
    End Function

    ''' <summary>
    ''' Replaces the existing list of incomplete results with the new one specified.
    ''' </summary>
    ''' <param name="p_mcResults">List of result objects to replace the existing list with.</param>
    ''' <remarks></remarks>
    Friend Sub ReplaceIncompleteResults(ByRef p_mcResults As List(Of cMCResult))
        If p_mcResults Is Nothing Then Exit Sub

        Try
            _tableQueriesIncomplete = New cTableQueries(p_mcResults)
            _tableQueriesIncomplete.Results.Sort()
            RaisePropertyChanged("incompleteResults")
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Adds a result object to the list of incomplete results.
    ''' </summary>
    ''' <param name="p_mcResult">Result object to add to the list.</param>
    ''' <remarks></remarks>
    Friend Sub AddIncompleteResultsItem(ByRef p_mcResult As cMCResult)
        Dim tempResults As New List(Of cMCResult)
        Dim resultAdded As Boolean = False

        'Preserve all existing results, but replace a result if it matches the incomplete result to be added
        For Each result As cMCResult In _tableQueriesIncomplete.Results
            If Not (result.query.ID = p_mcResult.query.ID AndAlso result.benchmark.ID = p_mcResult.benchmark.ID) Then
                tempResults.Add(result)
            Else
                tempResults.Add(p_mcResult)
                resultAdded = True
            End If
        Next

        If Not resultAdded Then tempResults.Add(p_mcResult)

        ReplaceIncompleteResults(tempResults)
    End Sub

    ''' <summary>
    ''' Removes a result object from the list of incomplete results.
    ''' </summary>
    ''' <param name="p_mcResult">Result object to remove from the list.</param>
    ''' <remarks></remarks>
    Friend Sub RemoveIncompleteResultsItem(ByRef p_mcResult As cMCResult)
        Dim tempResults As New List(Of cMCResult)

        For Each result As cMCResult In _tableQueriesIncomplete.Results
            If Not (result.query.ID = p_mcResult.query.ID AndAlso result.benchmark.ID = p_mcResult.benchmark.ID) Then
                tempResults.Add(result)
            End If
        Next

        ReplaceIncompleteResults(tempResults)
    End Sub
#End Region



#Region "Methods: Private - Adding Data & Structure"
    ''' <summary>
    ''' Adds new columns of 'Query' and 'Benchmark' to the dataset, and their primary key columns.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to manipulate,</param>
    ''' <remarks></remarks>
    Private Sub AddColumnsResultDetails(ByRef p_dataTable As DataTable)
        Dim i As Integer
        Dim column As New DataColumn

        _colIndexResultDetailsStatus = p_dataTable.Columns.Count

        'Create Result Details Column
        With column
            .ColumnName = RESULT_DETAILS_HEADER
            .DataType = Type.GetType("System.String")
            .DefaultValue = ""
        End With
        p_dataTable.Columns.Add(column)
        'myDataTable.Columns.Add(resultDetailsHeader, Type.GetType("System.String"))

        'Create Comments Column
        p_dataTable.Columns.Add(NAME_HEADER, Type.GetType("System.String"))

        'Create Query Column
        p_dataTable.Columns.Add(QUERY_HEADER, Type.GetType("System.String"))

        'Create BM Column
        p_dataTable.Columns.Add(BM_HEADER, Type.GetType("System.String"))

        'Create BM Value Column
        p_dataTable.Columns.Add(BM_VALUE_HEADER, Type.GetType("System.String"))

        'Create BM Sig Fig Column
        p_dataTable.Columns.Add(BM_ROUND_HEADER, Type.GetType("System.String"))

        'Create BM Is Correct Column
        p_dataTable.Columns.Add(BM_ISCORRECT_HEADER, Type.GetType("System.String"))

        'Create Result Theoretical Column
        p_dataTable.Columns.Add(THEORETICAL_HEADER, Type.GetType("System.String"))

        'Create Result Theoretical Sig Fig Column
        p_dataTable.Columns.Add(THEORETICAL_ROUND_HEADER, Type.GetType("System.String"))

        'Create Result Last Best Column
        p_dataTable.Columns.Add(LAST_BEST_HEADER, Type.GetType("System.String"))

        'Create Result Last Best Sig Fig Column
        p_dataTable.Columns.Add(LAST_BEST_ROUND_HEADER, Type.GetType("System.String"))

        'Create Units Conversion Column
        p_dataTable.Columns.Add(UNITS_CONVERT_HEADER, Type.GetType("System.String"))

        'Create Passing % Difference Column
        p_dataTable.Columns.Add(PASSING_PERCENT_DIFFERENCE_HEADER, Type.GetType("System.String"))

        'Create Calc Shift Column
        p_dataTable.Columns.Add(SHIFT_CALC_HEADER, Type.GetType("System.String"))

        ' Create Zero Tolerance Column
        p_dataTable.Columns.Add(ZERO_TOLERANCE_HEADER, Type.GetType("System.String"))


        _colIndexKey = p_dataTable.Columns.Count

        'Create Query Key column
        p_dataTable.Columns.Add(KEY_QUERY_HEADER, Type.GetType("System.Int32"))
        i = 0
        For Each row As DataRow In p_dataTable.Rows
            row.Item(KEY_QUERY_HEADER) = i
            i += 1
        Next

        'Create Benchmark Key column
        p_dataTable.Columns.Add(KEY_BM_HEADER, Type.GetType("System.Int32"))
        For Each row As DataRow In p_dataTable.Rows
            row.Item(KEY_BM_HEADER) = 0               'Sets value to first benchmark by default
        Next

        'Set up primary keys
        Dim PrimaryKeyColumns() As DataColumn = New DataColumn(1) {}
        PrimaryKeyColumns(0) = p_dataTable.Columns(KEY_QUERY_HEADER)
        PrimaryKeyColumns(1) = p_dataTable.Columns(KEY_BM_HEADER)
        p_dataTable.PrimaryKey = PrimaryKeyColumns
    End Sub

    ''' <summary>
    ''' Adds columns that contains the query status of an associated data entry.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to manipulate.</param>
    ''' <remarks></remarks>
    Private Sub AddColumnsQueryStatus(ByRef p_dataTable As DataTable)
        Dim myNewHeaders As New List(Of String)

        'Create list of headers
        For Each column As DataColumn In p_dataTable.Columns
            Dim myNewHeader As String

            myNewHeader = column.ColumnName
            'Limit iterations to columns brought in from the db file, and stop at the first custom added column
            If myNewHeader = RESULT_DETAILS_HEADER Then Exit For

            myNewHeaders.Add(GetCellStatusTagHeader(myNewHeader))
        Next

        'Assign new columns
        For Each header As String In myNewHeaders
            p_dataTable.Columns.Add(header, Type.GetType("System.String"))
        Next

    End Sub

    ''' <summary>
    ''' Sets the current result object collection to be the one contained in the tableQueries collection that matches the table name provided. Returns Nothing if no table is found.
    ''' </summary>
    ''' <param name="p_tableName">Name of the table to be found in the collection of results.</param>
    ''' <param name="p_tableQueriesCollection">Collection of cTableQueries from which to return the collection matching the table name provided.</param>
    ''' <remarks></remarks>
    Private Function CurrentResultsCollection(ByVal p_tableName As String,
                                              ByVal p_tableQueriesCollection As cTableQueriesCollection) As cTableQueries

        For Each dataTableObject As cTableQueries In p_tableQueriesCollection
            If dataTableObject.tableName = p_tableName Then Return dataTableObject
        Next

        Return Nothing
    End Function

    ''' <summary>
    ''' Fills in benchmarks and queries on the appropriate rows, sets the query and benchmark keys to the appropriate numbers and orders the collections by these keys.
    ''' </summary>
    ''' <param name="p_dataTable">Data table from which to look up the queries &amp; benchmarks.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <remarks></remarks>
    Private Sub InitializeRecordsOfQueriesFromResults(ByRef p_dataTable As DataTable,
                                                      ByRef p_tableQueries As cTableQueries)
        Try
            MatchQueryKeysBenchmarkKeysToDataTable(p_dataTable, p_tableQueries)

            'Order results by the updated query and benchmark keys
            p_tableQueries.Results.Sort()

            AddEditableColumnData(p_dataTable, p_tableQueries)

            'Select default benchnmarks
            For Each row As DataRow In p_dataTable.Rows
                SetBenchmarkSelectionToDefault(row, p_tableQueries)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Fills the rows with data in the columns that are additional to those created from the external database file.
    ''' </summary>
    ''' <param name="p_dataTable">Data table from which to look up the queries &amp; benchmarks.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <remarks></remarks>
    Private Sub AddEditableColumnData(ByRef p_dataTable As DataTable,
                                      ByVal p_tableQueries As cTableQueries)
        'Note that rows are filled for cases of filterRows.Count > 1. 
        'This is in the event that incoming data has non-unique queries, the data is at least pasted over all matching rows to allow the user to correct the problem.
        'For data validation, the user is unable to save the state of non-unique queries in the table unless they cancel, so this method allows data correction and forces it in cases where changes are desired to be saved.

        Try
            _lastQueryKeySelected = -1
            _lastBenchmarkSelected = ""

            For Each result As cMCResult In p_tableQueries.Results
                If Not AddResultToTable(p_dataTable, result) Then Continue For 'Skip result iteration if no matches are found to add to the table
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Fills the provided row object with data in the columns that are additional to those created from the external database file.
    ''' </summary>
    ''' <param name="p_row">DataRow object to reference and modify.</param>
    ''' <param name="p_result">Result object to read data from.</param>
    ''' <param name="p_statusQuery">Status of the query for the provided row.</param>
    ''' <param name="p_overrideWithResult">If 'true', then even if the default selection is different, the result object provided by the current selection will be used.</param>
    ''' <remarks></remarks>
    Private Sub AddEditableColumnDataToRow(ByRef p_row As DataRow,
                                           ByVal p_result As cMCResult,
                                           ByVal p_statusQuery As String,
                                           ByVal p_overrideWithResult As Boolean)
        'Add the benchmark data and status triggers
        If (String.IsNullOrEmpty(p_row.Item(BM_HEADER).ToString) OrElse
            p_statusQuery = GetEnumDescription(eQueryBMStatus.queryPartial) OrElse
            IsResultsDetailsIncomplete(p_row) OrElse
            p_overrideWithResult) Then 'Add benchmark status and overwrite any existing result details

            If p_overrideWithResult Then
                Dim resultNewBMKeyToSwap As Integer = p_result.benchmark.ID

                UpdateSelectedBMEntry(p_row, , resultNewBMKeyToSwap)
            Else
                UpdateSelectedBMEntry(p_row, p_result)
            End If
        Else                                                    'A benchmark has already been added for the unique query and the current additional benchmark is complete
            'Add the secondary benchmark status trigger
            UpdateBenchmarkNotSelectedRowStatusTag(p_row, p_result)
        End If

        'Add the query status triggers
        AddQueryStatusFromResult(p_row, p_result, p_statusQuery)
    End Sub

    ''' <summary>
    ''' Adds the values of the provided result object to the provided DataTable object.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to add the result data to.</param>
    ''' <param name="p_result">Result object to add to the DataTable.</param>
    ''' <param name="p_overrideWithResult">If 'true', then even if the default selection is different, the result object provided by the current selection will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddResultToTable(ByRef p_dataTable As DataTable,
                                      ByVal p_result As cMCResult,
                                      Optional ByVal p_overrideWithResult As Boolean = False) As Boolean
        Dim filterRows() As DataRow
        Dim resultStatusQuery As String

        'WARNING! This is only meant for first reading in data, which is assumed to be complete. 
        '   Non-unique results will not enter into the table correctly and may have unintended interaction consequences.

        filterRows = p_dataTable.Select(p_result.query.asString)
        If filterRows Is Nothing Then Return False

        'Determine if query returns one or more matches and classify accordingly
        resultStatusQuery = QueryStatus(p_result.query.asString, p_dataTable)

        For Each row As DataRow In filterRows
            AddEditableColumnDataToRow(row, p_result, resultStatusQuery, p_overrideWithResult)
        Next

        Return True
    End Function

    ''' <summary>
    ''' Updates the benchmark entry of the provided DataRow object. Depending on specifications, the current data is either overwritten or read back to the original result object.
    ''' </summary>
    ''' <param name="p_row">DataRow object to reference and modify.</param>
    ''' <param name="p_resultNew">New result object to read data from.</param>
    ''' <param name="p_resultNewBMKeyToSwap">Key corresponding to the new result object to be swapped into the DataRow object. 
    ''' If specified, the current data will be read back into the current result object and p_resultNew will be assigned based on this key rather than as specified.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSelectedBMEntry(ByRef p_row As DataRow,
                                      Optional ByVal p_resultNew As cMCResult = Nothing,
                                      Optional ByVal p_resultNewBMKeyToSwap As Integer = -1)
        If p_resultNewBMKeyToSwap >= 0 Then p_resultNew = SwapOldResult(p_row, p_resultNewBMKeyToSwap)
        If p_resultNew Is Nothing Then Exit Sub

        Try
            AddResultDetailsFromMCResult(p_row, p_resultNew)

            'Add the benchmark status trigger & update any prior added triggers
            If (IsValidObjectDB(p_row.Item(BM_HEADER)) AndAlso
                Not String.IsNullOrEmpty(p_resultNew.benchmark.name)) Then
                p_row.Item(GetCellStatusTagHeader(p_resultNew.benchmark.name)) = GetEnumDescription(eQueryBMStatus.bmValueSelected)
                UpdateLastBMQuerySelection(p_row, p_resultNew)
            End If

            'Add the benchmark key for the currently selected benchmark
            p_row.Item(KEY_BM_HEADER) = p_resultNew.benchmark.ID
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Looks up the result object corresponding to the keys listed in the provided DataRow object and reads the current row data back into the object. 
    ''' This new result object is returned if the operation is successful.
    ''' </summary>
    ''' <param name="p_row">DataRow object to reference.</param>
    ''' <param name="p_resultNewBMKeyToSwap">Key corresponding to the new result object to be swapped into the DataRow object.</param>
    ''' <remarks></remarks>
    Private Function SwapOldResult(ByVal p_row As DataRow,
                                   ByVal p_resultNewBMKeyToSwap As Integer) As cMCResult

        Dim resultQueryKey As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
        Dim resultOldBMKey As Integer = myCInt(p_row.Item(KEY_BM_HEADER).ToString)
        Dim resultOld As cMCResult = CreateMCResultFromRow(p_row, True)
        '_tableQueriesCurrent
        Dim resultNew As cMCResult = Nothing

        If resultOld Is Nothing Then Return Nothing

        'Lookup the currently selected result object
        For Each result As cMCResult In _tableQueriesTotal.Results
            If (result.query.ID = resultQueryKey AndAlso result.benchmark.ID = resultOldBMKey) Then
                'Record the data back into the currently selected result object
                result = resultOld
                Exit For
            End If
        Next

        'Look up p_resultNew based on p_resultNewKeyToSwap
        For Each result As cMCResult In _tableQueriesTotal.Results
            If (result.query.ID = resultQueryKey AndAlso result.benchmark.ID = p_resultNewBMKeyToSwap) Then
                resultNew = result
                Exit For
            End If
        Next

        Return resultNew
    End Function

    ''' <summary>
    ''' Updates the status of the last-selected benchmark if the selection has changed from the provided result object.
    ''' </summary>
    ''' <param name="p_row">DataRow object to reference and modify.</param>
    ''' <param name="p_result">Result object to read data from.</param>
    ''' <remarks></remarks>
    Private Sub UpdateLastBMQuerySelection(ByRef p_row As DataRow,
                                           ByVal p_result As cMCResult)
        Dim bmRemoved As Boolean = True
        Try
            'Ensure that the prior selected benchmark is marked otherwise
            If (_lastQueryKeySelected = p_result.query.ID AndAlso _
                Not _lastBenchmarkSelected = GetCellStatusTagHeader(p_result.benchmark.name) AndAlso
                Not String.IsNullOrEmpty(_lastBenchmarkSelected)) Then

                'Check if last benchmark selected still is a benchmark (will not be if it is being removed)
                For Each mcResult As cMCResult In _tableQueriesTotal.Results
                    If mcResult.benchmark.name = FilterStringFromName(_lastBenchmarkSelected, HEADER_SUFFIX, True, False) Then
                        bmRemoved = False
                        Exit For
                    End If
                Next

                'Update the last bm status tag selected if it was not removed
                If Not bmRemoved Then
                    UpdateBenchmarkNotSelectedRowStatusTag(p_row, p_result)
                    'p_row.Item(_lastBenchmarkSelected) = GetEnumDescription(eQueryBMStatus.bmValue)
                End If

                'Record current data in case it needs to be changed due to changes in benchmark selection
                If Not String.IsNullOrEmpty(p_result.benchmark.name) Then _lastBenchmarkSelected = GetCellStatusTagHeader(p_result.benchmark.name)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Adds result details from the MC results object to the DataRow.
    ''' </summary>
    ''' <param name="p_row">DataRow to populate.</param>
    ''' <param name="p_result">MC result object to reference.</param>
    ''' <remarks></remarks>
    Private Sub AddResultDetailsFromMCResult(ByRef p_row As DataRow,
                                             ByVal p_result As cMCResult)
        With p_row
            .Item(NAME_HEADER) = p_result.name
            .Item(QUERY_HEADER) = p_result.query.asString
            .Item(BM_HEADER) = p_result.benchmark.name
            .Item(BM_VALUE_HEADER) = p_result.benchmark.valueBenchmark
            .Item(BM_ROUND_HEADER) = p_result.benchmark.roundBenchmark
            .Item(BM_ISCORRECT_HEADER) = GetEnumDescription(p_result.benchmark.isCorrect)
            .Item(THEORETICAL_HEADER) = p_result.benchmark.valueTheoretical
            .Item(THEORETICAL_ROUND_HEADER) = p_result.benchmark.roundTheoretical
            .Item(LAST_BEST_HEADER) = p_result.benchmark.valueLastBest
            .Item(LAST_BEST_ROUND_HEADER) = p_result.benchmark.roundLastBest
            .Item(UNITS_CONVERT_HEADER) = p_result.unitsConversion
            '.Item(UNITS_HEADER) = p_result.units ' Hidden for now
            .Item(PASSING_PERCENT_DIFFERENCE_HEADER) = p_result.benchmark.valuePassingPercentDifferenceRange
            .Item(SHIFT_CALC_HEADER) = p_result.benchmark.shiftCalc
            .Item(ZERO_TOLERANCE_HEADER) = p_result.benchmark.zeroTolerance
        End With

        AddResultDetailsStatus(p_row)
    End Sub

    ''' <summary>
    ''' Finalizes setting query keys and benchmark keys to match the exported data.
    ''' </summary>
    ''' <param name="p_dataTable">Data table from which to look up the queries &amp; benchmarks.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <remarks></remarks>
    Private Sub MatchQueryKeysBenchmarkKeysToDataTable(ByRef p_dataTable As DataTable,
                                                        ByRef p_tableQueries As cTableQueries)
        'Note that rows are filled for cases of filterRows.Count > 1. 
        'This is in the event that incoming data has non-unique queries, the data is at least pasted over all matching rows to allow the user to correct the problem.
        'For data validation, the user is unable to save the state of non-unique queries in the table unless they cancel, so this method allows data correction and forces it in cases where changes are desired to be saved.

        Dim oldKeysList As New List(Of Integer)
        Dim newKeysList As New List(Of Integer)
        Dim currentQueryKey As Integer = 0
        Dim bmKeysColDictionary As New Dictionary(Of Integer, Integer)

        Try
            For Each result As cMCResult In p_tableQueries.Results
                Dim filterRows() As DataRow

                filterRows = p_dataTable.Select(result.query.asString)

                'Skip result iteration if no matches are found
                If filterRows Is Nothing Then Continue For

                For Each row As DataRow In filterRows
                    'Update the query key lists
                    oldKeysList.Add(result.query.ID)                             'Existing index
                    newKeysList.Add(myCInt(row.Item(KEY_QUERY_HEADER).ToString)) 'Current row count for new index

                    UpdateBenchmarkKeyAndCurrentQueryKey(row, result, currentQueryKey, bmKeysColDictionary, p_tableQueries)
                Next
            Next

            'Update the query keys
            p_tableQueries.id.UpdateIDs(oldKeysList, newKeysList, p_dataTable.Rows.Count)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates the benchmark keys to be sorted in the order by which the benchmark falls in the matching column index for a given query key. 
    ''' The current query key is also tracked and updated.
    ''' </summary>
    ''' <param name="p_row">DataRow object to get the column index from.</param>
    ''' <param name="p_result">Current result object being used for the current query key and benchmark key.</param>
    ''' <param name="p_currentQueryKey">Current query key being considered for ordering benchmarks.</param>
    ''' <param name="p_bmKeysColList">List of paired benchmark keys to column indices. 
    ''' This is either updated (current query key matches that of the result), or sorted, used, and cleared.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarkKeyAndCurrentQueryKey(ByVal p_row As DataRow,
                                                     ByVal p_result As cMCResult,
                                                     ByRef p_currentQueryKey As Integer,
                                                     ByRef p_bmKeysColList As Dictionary(Of Integer, Integer),
                                                     ByRef p_tableQueries As cTableQueries)

        Dim dtController As New cDataTableController

        If p_currentQueryKey = p_result.query.ID Then   'This is the second or greater benchmark being added to the query row
            'No special action
        Else                                            'Benchmark is the first one being added to the query row
            'Back up and perform operations across prior queryKey
            p_tableQueries.id.SetBenchmarkIDsByColIndex(p_currentQueryKey, p_bmKeysColList)

            'Clear BM Dictionary list
            p_bmKeysColList = New Dictionary(Of Integer, Integer)
        End If

        'Set new current key
        p_currentQueryKey = p_result.query.ID

        'Add benchmarkKey and column index to dictionary list
        p_bmKeysColList.Add(p_result.benchmark.ID, dtController.ColumnIndexFromHeader(p_result.benchmark.name, p_row))
    End Sub
#End Region

#Region "Methods: Private - Query"
    ''' <summary>
    ''' For a given row, gets the provided parameters to indicate the query status, and whether or not a benchmark exists.
    ''' </summary>
    ''' <param name="p_row">DataRow object for the row of interest.</param>
    ''' <remarks></remarks>
    Private Function RowQueryStatus(ByVal p_row As DataRow) As eQueryBMStatus

        For i = 0 To exportedSetTable.Columns.Count - 1
            If (ColumnRegionByColIndex(i) = eDataGridRegion.queryStatus AndAlso _
                IsValidObjectDBStringFilled(p_row.Item(i))) Then
                Dim cellValue As String = p_row.Item(i).ToString

                'It is assumed that all row queries are of the same value, so only the first match is needed
                Select Case cellValue
                    Case GetEnumDescription(eQueryBMStatus.queryPartial) : Return eQueryBMStatus.queryPartial
                    Case GetEnumDescription(eQueryBMStatus.queryUnique) : Return eQueryBMStatus.queryUnique
                    Case Else : Return eQueryBMStatus.undefined
                End Select
            End If
        Next

        Return eQueryBMStatus.undefined
    End Function

    ''' <summary>
    ''' Returns the query status of the specified column in the row provided.
    ''' </summary>
    ''' <param name="p_importedColumnHeader">Header corresponding to the column being checked.</param>
    ''' <param name="p_row">DataRow object that is being checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ItemQueryStatus(ByVal p_importedColumnHeader As String,
                                     ByVal p_row As DataRow) As eQueryBMStatus

        For i = 0 To exportedSetTable.Columns.Count - 1
            If (ColumnRegionByColIndex(i) = eDataGridRegion.queryStatus AndAlso _
                exportedSetTable.Columns(i).ColumnName = GetCellStatusTagHeader(p_importedColumnHeader) AndAlso _
                IsValidObjectDBStringFilled(p_row.Item(i))) Then

                Dim cellValue As String = p_row.Item(i).ToString

                Return ConvertStringToEnumByDescription(Of eQueryBMStatus)(cellValue)
            End If
        Next

        Return eQueryBMStatus.undefined
    End Function



    ''' <summary>
    ''' Creates a dictionary of all BM header lists corresponding to each query key.
    ''' </summary>
    ''' <param name="p_queryKeyList">List of query keys from which to determine the benchmarks lists.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BMHeadersListsFromQueries(ByVal p_queryKeyList As List(Of Integer)) As Dictionary(Of Integer, List(Of String))
        Dim bmHeadersLists As New Dictionary(Of Integer, List(Of String))

        For Each queryKey As Integer In p_queryKeyList
            Dim row As DataRow = GetDataRowByKey(queryKey)(0)
            bmHeadersLists.Add(queryKey, GetOrderedBenchmarkList(row))
        Next

        Return bmHeadersLists
    End Function

    ''' <summary>
    ''' Returns a list of all of the benchmarks recorded for a single table row, in the order in which they appear in the table columns.
    ''' </summary>
    ''' <param name="p_row">DataRow object that the routine searches.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOrderedBenchmarkList(ByVal p_row As DataRow) As List(Of String)
        Dim columnIndex As Integer = 0
        Dim bmHeaderList As New List(Of String)
        For Each column As DataColumn In p_row.Table.Columns
            If ColumnRegionByHeader(column.ColumnName, p_row.Table) = eDataGridRegion.queryStatus Then
                If CellUsedAsBenchmark(p_row.Item(columnIndex).ToString) Then
                    bmHeaderList.Add(FilterStringFromName(column.ColumnName, HEADER_SUFFIX, True, False))
                End If
            End If
            columnIndex += 1
        Next

        Return bmHeaderList
    End Function

    'Not used
    ''' <summary>
    ''' Returns 'True' if the DataRow and cMCResult objects provided are a unique match.
    ''' </summary>
    ''' <param name="p_row">DataRow object that is used for determining part of the match.</param>
    ''' <param name="p_result">Result object that is used for determining part of the match.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RowResultMatch(ByVal p_row As DataRow,
                                    ByVal p_result As cMCResult) As Boolean
        If (p_result.query.ID = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString) AndAlso p_result.benchmark.ID = myCInt(p_row.Item(KEY_BM_HEADER).ToString)) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Private - Results Validation"
    '=== Benchmark
    ''' <summary>
    ''' Returns 'True' if a table cell is used as a benchmark based on the provided value.
    ''' </summary>
    ''' <param name="p_status">Status to consider.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function CellUsedAsBenchmark(ByVal p_status As String) As Boolean
        If (p_status = GetEnumDescription(eQueryBMStatus.bmValue) OrElse _
            p_status = GetEnumDescription(eQueryBMStatus.bmValueSelected) OrElse _
            p_status = GetEnumDescription(eQueryBMStatus.bmDetailsIncomplete)) Then
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' Returns 'True' if a table cell is used as a benchmark based on the provided value.
    ''' </summary>
    ''' <param name="p_status">Status to consider.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function CellUsedAsBenchmark(ByVal p_status As eQueryBMStatus) As Boolean
        If (p_status = eQueryBMStatus.bmValue OrElse
            p_status = eQueryBMStatus.bmValueSelected OrElse
            p_status = eQueryBMStatus.bmDetailsIncomplete) Then
            Return True
        Else
            Return False
        End If
    End Function

    '=== Query
    ''' <summary>
    ''' Returns 'True' if a table cell is used as a query based on the provided value.
    ''' </summary>
    ''' <param name="p_status">Table cell value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CellUsedAsQuery(ByVal p_status As String) As Boolean
        If (p_status = GetEnumDescription(eQueryBMStatus.queryPartial) OrElse p_status = GetEnumDescription(eQueryBMStatus.queryUnique)) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns 'True' if query recorded in the row returns a unique row in the DataTable. 
    ''' </summary>
    ''' <param name="p_row">DataRow object for the row to check in the DataTable.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function QueryComplete(ByVal p_row As DataRow) As Boolean
        For i = 0 To p_row.ItemArray.Count - 1
            If p_row.Item(i).ToString = GetEnumDescription(eQueryBMStatus.queryPartial) Then Return False
        Next

        Return True
    End Function

    '=== Details
    ''' <summary>
    ''' Returns 'True' if the results details are incomplete. Otherwise, returns 'False'.
    ''' </summary>
    ''' <param name="p_row">Row item from the DataTable.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsResultsDetailsIncomplete(ByVal p_row As DataRow) As Boolean
        If (String.IsNullOrEmpty(p_row.Item(NAME_HEADER).ToString) OrElse
            String.IsNullOrEmpty(p_row.Item(RESULT_DETAILS_HEADER).ToString)) Then
            Return True
        Else
            Return False
        End If
    End Function

    '=== Overall
    ''' <summary>
    ''' Determines whether the cMCResult object referenced by query &amp; benchmark keys is complete in terms of an established benchmark, unique query and minimum required result details.
    ''' Updates the current list of incomplete results to reflect this. 
    ''' </summary>
    ''' <param name="p_queryKey">Key number referencing the query that contains the 1 or more benchmarks.</param>
    ''' <param name="p_benchmarkKey">Key number referencing the benchmark to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddResultByKeysCompleteAsIncomplete(ByVal p_queryKey As Integer,
                                                        ByVal p_benchmarkKey As Integer) As Boolean
        Dim currentResult As cMCResult

        currentResult = _tableQueriesTotal.GetResultObject(p_queryKey, p_benchmarkKey)
        currentResult.GetQueryType(exportedSetTable, True)

        If (currentResult.isComplete AndAlso _
            currentResult.GetQueryType(exportedSetTable, True) = eQueryType.Unique) Then
            RemoveIncompleteResultsItem(currentResult)
            Return True
        Else
            AddIncompleteResultsItem(currentResult)
            Return False
        End If
    End Function

    ''' <summary>
    ''' Checks all result objects related to the query in the row and updates the incomplete results list accordingly.
    ''' </summary>
    ''' <param name="p_row">DataRow object to which to apply the routine.</param>
    ''' <remarks></remarks>
    Private Sub UpdateIncompleteResultListByRow(ByVal p_row As DataRow)
        Dim keyQuery As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)

        'Update list of complete/incomplete results for all results that share the affected query
        For Each result As cMCResult In _tableQueriesTotal.Results
            If (result.query.ID = keyQuery AndAlso
                (result.isBMComplete OrElse result.query.isSpecified)) Then
                AddResultByKeysCompleteAsIncomplete(result.query.ID, result.benchmark.ID)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Prompts the user for action based on the provided messages. Returns 'True' if the results are to be corrected.
    ''' </summary>
    ''' <param name="p_message">Message to be displayed in the prompt.</param>
    ''' <param name="p_title">Title of the prompt.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CompleteIncompleteResults(ByVal p_message As String,
                                               ByVal p_title As String) As Boolean
        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            p_message,
                                            p_title)
            Case eMessageActions.Yes
                Return True
            Case eMessageActions.No
                Return False
        End Select

            'If error
            Return True
    End Function
#End Region

#Region "Methods: Private - Actions to add/remove cells from query, BM, add result details, etc."
    'Update Query
    ''' <summary>
    ''' Returns a new SQL query string with the change added to or removed from the query.
    ''' </summary>
    ''' <param name="p_queryStringCurrent">String representing the current query.</param>
    ''' <param name="p_cellItem">The header-value pair that is to be added to or removed from the current query.</param>
    ''' <param name="p_addChange">If 'True', the change will be added to the current query. If 'False', the change will be removed from the current query.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateQueryString(ByVal p_queryStringCurrent As String,
                                       ByVal p_cellItem As cHeaderAndValue,
                                       ByVal p_addChange As Boolean) As String
        If p_cellItem Is Nothing Then Return p_queryStringCurrent

        Dim queryStringAddRemove As String = p_cellItem.header & " = '" & p_cellItem.value & "'"
        If (p_addChange AndAlso StringExistInName(p_queryStringCurrent, queryStringAddRemove)) Then Return p_queryStringCurrent

        Dim andJoiner As String = " AND "
        Dim p_queryStringNew As String = ""

        If p_addChange Then
            'If the value is being added to an existing query, append with 'AND'
            If Not String.IsNullOrEmpty(p_queryStringCurrent) Then queryStringAddRemove = andJoiner & queryStringAddRemove

            p_queryStringNew = p_queryStringCurrent & queryStringAddRemove
        Else
            'Looks up string and removes it, accounting for cases where it is preceded with an 'AND'.
            If StringExistInName(p_queryStringCurrent, andJoiner & queryStringAddRemove) Then   'Value to remove is a later one in the query
                p_queryStringNew = ReplaceStringInName(p_queryStringCurrent, andJoiner & queryStringAddRemove, "", , True)
            ElseIf StringExistInName(p_queryStringCurrent, queryStringAddRemove) Then           'value to remove is the first one in the query
                p_queryStringNew = ReplaceStringInName(p_queryStringCurrent, queryStringAddRemove, "", , True)
            End If

            'Check if string has an 'AND' in the beginning
            If Microsoft.VisualBasic.Left(p_queryStringNew, 5) = andJoiner Then
                p_queryStringNew = Microsoft.VisualBasic.Right(p_queryStringNew, Len(p_queryStringNew) - 5)
            End If
        End If

        Return p_queryStringNew
    End Function

    ''' <summary>
    ''' Updates the cell containing the query string, as well as any other cells related to the query change.
    ''' </summary>
    ''' <param name="p_row">DataRow object to which to apply the function.</param>
    ''' <param name="p_queryStringNew">String representing the newly formed query string.</param>
    ''' <param name="p_cellStatusTagHeader">Header of the hidden column where the cell status is recorded for the currently selected cell.</param>
    ''' <param name="p_addChange">If 'True', the change will be added to the current query. If 'False', the change will be removed from the current query.</param>
    ''' <remarks></remarks>
    Private Sub UpdateRowQuery(ByVal p_row As DataRow,
                               ByVal p_queryStringNew As String,
                               ByVal p_cellStatusQuery As String,
                               ByVal p_cellStatusTagHeader As String,
                               ByVal p_addChange As Boolean)

        'Update query value
        p_row.Item(QUERY_HEADER) = p_queryStringNew

        'Update cell status tags
        If p_addChange Then
            p_row.Item(p_cellStatusTagHeader) = p_cellStatusQuery
        Else
            p_row.Item(p_cellStatusTagHeader) = ""
        End If
        UpdateCellQueryStatusTags(p_row, p_cellStatusQuery)

    End Sub

    ''' <summary>
    ''' Removes the benchmarks from the result objects, either by removing the object itself, or updating the object with the cleared data from the DataTable.
    ''' </summary>
    ''' <param name="p_row">Row to be used to gather the updated data.</param>
    ''' <param name="p_benchmarkHeaderRemove">Corresponds to the specific benchmark to be removed, if it is not the last remaining benchmark for the row.</param>
    ''' <remarks></remarks>
    Private Sub RemoveBenchmarkFromResultsList(ByVal p_row As DataRow,
                                               ByVal p_benchmarkHeaderRemove As String)
        Dim queryKeyRemove As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)

        'If this is the only remaining benchmark, just update the list, otherwise, remove the MC Result object corresponding to the benchmark
        If CountBenchmark(p_row) <= 0 Then
            If IsResultEmpty(p_row) Then            'Remove all result objects for the row
                RemoveMCResultsByDataRow(p_row)
            Else                                    'Update the object, which will only have a query
                WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False)
            End If
        Else
            'If this is one of several benchmarks, remove the MC Result object corresponding to the benchmark
            WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False, queryKeyRemove, p_benchmarkHeaderRemove, True)
        End If
    End Sub

    ''' <summary>
    ''' Checks the relevant result columns of the DataRow and determines whether or not they are all empty.
    ''' </summary>
    ''' <param name="p_row">DataRow to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsResultEmpty(ByVal p_row As DataRow) As Boolean
        Dim dataCleared As Boolean = True

        Try
            Dim queryKeyRemove As Integer = -1
            Dim header As String
            Dim colRegion As eDataGridRegion = eDataGridRegion.undefinedRegion

            For Each column As DataColumn In exportedSetTable.Columns
                header = column.ColumnName
                colRegion = ColumnRegionByHeader(header, exportedSetTable)

                If Not (colRegion = eDataGridRegion.importedDBTable OrElse
                        colRegion = eDataGridRegion.keyQuery OrElse
                        colRegion = eDataGridRegion.keyBenchmark) Then

                    'Check if items are all cleared
                    If Not (String.IsNullOrEmpty(p_row.Item(header).ToString) OrElse
                            p_row.Item(header).ToString = GetEnumDescription(eResultDetailsStatus.add) OrElse
                            p_row.Item(header).ToString = "0") Then
                        dataCleared = False
                    End If
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return dataCleared
    End Function

    ''' <summary>
    ''' Removes the MC Result object from the current Table Queries object based on the DataRow provided.
    ''' </summary>
    ''' <param name="p_row">DataRow to use for collecting data for the result to remove.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RemoveMCResultsByDataRow(ByVal p_row As DataRow) As Boolean
        Try
            Dim queryKeyRemove As Integer = -1

            queryKeyRemove = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)
            If queryKeyRemove >= 0 Then
                WriteRecordsOfQueriesToResults(exportedSetTable, _tableQueriesTotal, False, queryKeyRemove)
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Get list of BM Headers in column order, update the BM keys to this order, and update list sorting. 
    ''' Typically used after the following operations: 1) Add benchmark.  2) Remove benchmark.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateAllKeysAndSorting(ByVal p_sortBenchmarks As Boolean)
        Dim bmHeadersLists As Dictionary(Of Integer, List(Of String))

        If p_sortBenchmarks Then
            Dim queryKeyList As List(Of Integer) = _tableQueriesTotal.id.QueryIDsFromResults()
            bmHeadersLists = BMHeadersListsFromQueries(queryKeyList)
        Else
            bmHeadersLists = Nothing
        End If

        _tableQueriesTotal.UpdateSortAllBenchmarkIDs(bmHeadersLists)
        _tableQueriesIncomplete.UpdateSortAllBenchmarkIDs(bmHeadersLists)
    End Sub
#End Region

#Region "Methods: Private - Add/Remove Status Indicators In Cells"
    ''' <summary>
    ''' Clears the status associated with a cell beneath the provided header if a status is set. This this case the function returns 'True'. 
    ''' This undoes a prior status setting operation for the particular cell. 
    ''' </summary>
    ''' <param name="p_cellItem">Header and associated value of the item being removed as a benchmark or query.</param>
    ''' <param name="p_row">DataRow object for the row of interest.</param>
    ''' <remarks></remarks>
    Private Function ClearCellStatusQueryOrBM(ByVal p_row As DataRow,
                                              ByVal p_cellItem As cHeaderAndValue) As eResultOperation
        Dim cellStatusTagHeader As String = GetCellStatusTagHeader(p_cellItem.header)
        If Not IsValidObjectDBStringFilled(p_row.Item(cellStatusTagHeader)) Then Return eResultOperation.none

        Dim cellValue As String = p_row.Item(cellStatusTagHeader).ToString

        'If cell status tag is not blank, make cell status tag blank
        If CellUsedAsBenchmark(cellValue) Then
            RemoveBenchmark(p_row, p_cellItem.header)
            Return eResultOperation.benchmarkRemove
        ElseIf CellUsedAsQuery(cellValue) Then
            'RemoveQueryComponent(p_header, p_row)
            UpdateQuery(p_row, p_cellItem, False)
            Return eResultOperation.queryFieldRemove
        End If

        Return eResultOperation.none
    End Function

    ''' <summary>
    ''' Removes all table data corresponding to the currently displayed results details.
    ''' </summary>
    ''' <param name="p_row">Row that the procedure acts on.</param>
    ''' <remarks></remarks>
    Private Sub ClearResultDetailsEntries(ByVal p_row As DataRow)
        Dim dataCleared As Boolean = True
        Dim header As String
        Dim colRegion As eDataGridRegion = eDataGridRegion.undefinedRegion

        If p_row Is Nothing Then Exit Sub

        Try
            For Each column As DataColumn In exportedSetTable.Columns
                header = column.ColumnName
                colRegion = ColumnRegionByHeader(header, exportedSetTable)

                'Clear all columns in the details region except for the original recorded BM & query values
                If (colRegion = eDataGridRegion.resultsDetails OrElse
                    colRegion = eDataGridRegion.resultsStatus) AndAlso
                    Not (header = BM_HEADER OrElse
                         header = BM_VALUE_HEADER OrElse
                         header = QUERY_HEADER) Then
                    p_row.Item(header) = Nothing
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Resets the selection status of all benchmarks that are marked as 'selected' to be unselected.
    ''' </summary>
    ''' <param name="p_row">DataRow object upon which the routine acts.</param>
    ''' <remarks></remarks>
    Private Sub ResetBenchmarkSelectionStatuses(ByRef p_row As DataRow)
        Dim columnIndex As Integer = 0
        Dim tempResult As cMCResult

        'Find the selected BM tag and change it to a deselected value
        For Each column As DataColumn In p_row.Table.Columns
            If ColumnRegionByHeader(column.ColumnName, p_row.Table) = eDataGridRegion.queryStatus Then
                If p_row.Item(columnIndex).ToString = GetEnumDescription(eQueryBMStatus.bmValueSelected) Then
                    'Update status tag to not be selected (necessary before updating based on object status
                    p_row.Item(columnIndex) = GetEnumDescription(eQueryBMStatus.bmValue)
                End If
            End If
            columnIndex += 1
        Next

        'Fine tune the non-selected BM status to be a subset of a non-selected BM
        tempResult = CreateMCResultFromRow(p_row)
        UpdateBenchmarkNotSelectedRowStatusTag(p_row, tempResult)
    End Sub

    ''' <summary>
    ''' Checks the details status of a given benchmark and assigns a cell status tag accordingly.
    ''' </summary>
    ''' <param name="p_row">Row object to update.</param>
    ''' <param name="p_result">Result to check the status of.</param>
    ''' <remarks></remarks>
    Private Sub UpdateBenchmarkNotSelectedRowStatusTag(ByVal p_row As DataRow,
                                                       ByVal p_result As cMCResult)
        If String.IsNullOrEmpty(p_result.benchmark.name) Then Exit Sub
        Dim bmCellStatusTag As String = GetCellStatusTagHeader(p_result.benchmark.name)

        If Not p_row.Item(bmCellStatusTag).ToString = GetEnumDescription(eQueryBMStatus.bmValueSelected) Then
            If p_result.isDetailsComplete Then
                p_row.Item(bmCellStatusTag) = GetEnumDescription(eQueryBMStatus.bmValue)
            Else
                p_row.Item(bmCellStatusTag) = GetEnumDescription(eQueryBMStatus.bmDetailsIncomplete)
            End If
        End If
    End Sub

    '=== Adds Status indicators in cells
    ''' <summary>
    ''' Sets the 'Result Details' status of the data in a DataTable object.
    ''' </summary>
    ''' <param name="p_row">Row item from the DataTable.</param>
    ''' <remarks></remarks>
    Private Sub AddResultDetailsStatus(ByRef p_row As DataRow)
        If IsResultsDetailsIncomplete(p_row) Then
            p_row.Item(RESULT_DETAILS_HEADER) = GetEnumDescription(eResultDetailsStatus.add)
        Else
            p_row.Item(RESULT_DETAILS_HEADER) = GetEnumDescription(eResultDetailsStatus.edit)
        End If
    End Sub

    ''' <summary>
    ''' Adds the query status provided to each relevant query cell of the provided DataRow object, determined by the queries listed in the result object.
    ''' </summary>
    ''' <param name="p_row">DataRow object to add the query statuses to.</param>
    ''' <param name="p_result">Result object to be read for each query status component to associate with a cell in the DataRow object.</param>
    ''' <param name="p_queryStatus">Query status to write into the cells of the DataRow object.</param>
    ''' <remarks></remarks>
    Private Sub AddQueryStatusFromResult(ByRef p_row As DataRow,
                                         ByVal p_result As cMCResult,
                                         ByVal p_queryStatus As String)
        If IsValidObjectDBStringFilled(p_row.Item(QUERY_HEADER)) Then
            For Each fieldLookup As cFieldLookup In p_result.query
                p_row.Item(GetCellStatusTagHeader(fieldLookup.name)) = p_queryStatus
            Next
        End If
    End Sub

    ''' <summary>
    ''' Updates the hidden cell that tags the status of a data selected in a table as either partial or unique. Update is to maintain the status query, or change it.
    ''' </summary>
    ''' <param name="p_row">DataRow object containing the row of data selected in the datagrid.</param>
    ''' <param name="p_statusQuery">Either the value corresponding with partial (multiple rows match the query), or unique (only one row matches the query).</param>
    ''' <remarks></remarks>
    Private Sub UpdateCellQueryStatusTags(ByVal p_row As DataRow,
                                          ByVal p_statusQuery As String)
        Try
            Dim header As String

            For i = 0 To exportedSetTable.Columns.Count - 1
                header = exportedSetTable.Columns(i).ColumnName

                'If a cell is in the corresponding status region, is not empty, and not tagged as a benchmark, set its status tag to that provided
                If (StringExistInName(header, HEADER_SUFFIX) AndAlso _
                    IsValidObjectDBStringFilled(p_row.Item(i)) AndAlso _
                    Not CellUsedAsBenchmark(p_row.Item(i).ToString)) Then

                    p_row.Item(i) = p_statusQuery
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private - Benchmark Selection Events"
    ''' <summary>
    ''' Changes the values in the table for the current benchmark selection to be that of the default.
    ''' </summary>
    ''' <param name="p_row">DataRow object of the selected row within which to make the changes.</param>
    ''' <param name="p_tableQueries">Source of the result objects from which the DataTable is updated.</param>
    ''' <remarks></remarks>
    Private Sub SetBenchmarkSelectionToDefault(ByVal p_row As DataRow,
                                                ByRef p_tableQueries As cTableQueries)
        Dim queryKey As Integer
        Dim benchmarkKeyOld As Integer
        Dim benchmarkKeyNew As Integer
        Dim newResult As cMCResult = Nothing

        'Triggered by:
        '   1. Removing Benchmark (done)
        '   2. Adding/Removing Query Items (done)
        '   3. Adding/Removing Details Items (done)

        Try
            If CountBenchmark(p_row) = 0 Then Exit Sub

            'Get the new default selection & old selection      
            queryKey = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)

            benchmarkKeyNew = GetBenchmarkSelectionIndexDefault(p_row, p_tableQueries)
            benchmarkKeyOld = myCInt(p_row.Item(KEY_BM_HEADER).ToString)
            If benchmarkKeyOld = -1 Then benchmarkKeyOld = benchmarkKeyNew

            newResult = p_tableQueries.GetResultObject(queryKey, benchmarkKeyNew)
            If newResult IsNot Nothing Then ChangeBenchmarkSelection(p_row.Table, newResult)

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns the index of the benchmark key to select based on the default criteria. Returns -1 if none of the criteria is satisfied.
    ''' </summary>
    ''' <param name="p_row">DataRow object to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBenchmarkSelectionIndexDefault(ByVal p_row As DataRow,
                                                  ByVal p_tableQueries As cTableQueries) As Integer
        Dim bmKeySelected As Integer = -1
        Dim bmKeyIncompleteMax As Integer = -1
        Dim bmKeyCompleteMin As Integer = -1
        Dim bmCount As Integer
        Dim allBMComplete As Boolean
        Dim keyQuery As Integer = myCInt(p_row.Item(KEY_QUERY_HEADER).ToString)

        'Read through the exported data table row records and note completeness & the bmKeys for the criteria for default ordering:
        bmCount = 0
        allBMComplete = True
        For i = 0 To exportedSetTable.Columns.Count - 1
            If (ColumnRegionByColIndex(i) = eDataGridRegion.importedDBTable AndAlso _
                IsValidObjectDBStringFilled(p_row.Item(i))) Then

                Dim columnHeader As String = exportedSetTable.Columns(i).ColumnName
                Dim queryStatusCurrent As eQueryBMStatus = ItemQueryStatus(columnHeader, p_row)

                If CellUsedAsBenchmark(queryStatusCurrent) Then
                    If (queryStatusCurrent = eQueryBMStatus.bmValueSelected AndAlso bmKeySelected = -1) Then bmKeySelected = bmCount

                    Dim currentResult As cMCResult = p_tableQueries.GetResultObject(keyQuery, bmCount)
                    'TODO: Remove? Dim bmComplete As Boolean = AddResultByKeysCompleteAsIncomplete(keyQuery, bmCount)
                    If (currentResult IsNot Nothing AndAlso
                        currentResult.isBMComplete AndAlso
                        currentResult.isDetailsComplete) Then
                        If (bmKeyCompleteMin = -1 AndAlso bmCount >= bmKeySelected) Then bmKeyCompleteMin = bmCount
                    Else
                        allBMComplete = False
                        If bmCount > bmKeyIncompleteMax Then bmKeyIncompleteMax = bmCount
                    End If

                    bmCount += 1
                End If
            End If
        Next

        'Default ordering is as follows:
        '   1. Largest index # of an incomplete result
        '   2. Current BM selection
        '   3. Smallest index # of a complete result
        Try
            If Not allBMComplete Then
                Return bmKeyIncompleteMax
            ElseIf bmKeySelected >= 0 Then
                Return myCInt(p_row.Item(KEY_BM_HEADER).ToString)
            ElseIf bmKeyCompleteMin >= 0 Then
                Return bmKeyCompleteMin
            Else
                Throw New ArgumentException("None of the matching criteria was satisfied." & vbNewLine & _
                                            "   bmIncompleteExists = " & allBMComplete & _
                                            "   bmKeySelected = " & bmKeySelected & _
                                            "   bmKeyCompleteMin = " & bmKeyCompleteMin)
                Return -1
            End If
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Changes the result present in the DataTable object to be that of the provided result object.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to change the displayed benchmark values in.</param>
    ''' <param name="p_result">Result object of the newly selected benchmark value to display.</param>
    ''' <remarks></remarks>
    Private Sub ChangeBenchmarkSelection(ByVal p_dataTable As DataTable,
                                         ByVal p_result As cMCResult)
        Dim filteredRows() As DataRow = p_dataTable.Select(p_result.query.asString)
        Dim benchmarkKeyLast As Integer 'TODO: Not used!

        Try
            If filteredRows.Count = 1 Then
                benchmarkKeyLast = myCInt(filteredRows(0).Item(KEY_BM_HEADER).ToString) 'TODO: Not used!
            Else
                Throw New ArgumentException("Multiple rows selected in sub 'ChangeBenchmarkSelection'. Action not taken.")
            End If

            'Change the selection if the new default is different than the current selection
            If p_result IsNot Nothing Then AddResultToTable(p_dataTable, p_result, True)
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        End Try
    End Sub
#End Region

#Region "Methods: Private - Reading/Writing"
    ''' <summary>
    ''' Updates all cMCResult objects that share the same query to the new query string. 
    ''' It is assumed that by sharing the old query, any changes to the query should be updated to all shared results.
    ''' </summary>
    ''' <param name="p_mcResults">Collection of cMCResult objects to update.</param>
    ''' <param name="p_dataTable">DataTable object from which the new query string is obtained.</param>
    ''' <param name="p_queryKey">Query key that corresponds to the set of result objects to update.</param>
    ''' <remarks></remarks>
    Private Sub UpdateAllResultQueries(ByRef p_mcResults As List(Of cMCResult),
                                       ByVal p_dataTable As DataTable,
                                       ByVal p_queryKey As Integer)

        Try
            Dim filterQueryRows() As DataRow = p_dataTable.Select(KEY_QUERY_HEADER & " = " & p_queryKey)
            Dim benchmarkKey As Integer = -1
            Dim newQueryString As String = ""
            Dim updateQueries As Boolean = False

            benchmarkKey = myCInt(filterQueryRows(0).Item(KEY_BM_HEADER).ToString)
            If Not benchmarkKey = -1 Then
                'Get the new query string
                For Each result As cMCResult In p_mcResults
                    If (result.query.ID = p_queryKey AndAlso result.benchmark.ID = benchmarkKey) Then
                        newQueryString = result.query.asString
                        updateQueries = True
                        Exit For
                    End If
                Next

                'Apply the query string to all of the benchmarks sharing the same query
                If updateQueries Then
                    For Each result As cMCResult In p_mcResults
                        If result.query.ID = p_queryKey Then
                            result.query.SetQuery(newQueryString)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns a combined list of cMCResult objects that includes all objects that are stored in either the DataTable object or cTableQueries object.
    ''' </summary>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <param name="p_mcResultsDisplayed">List of cMCResults objects provided, which are typically those present in the DataTable object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultsCollectionNotDisplayed(ByVal p_tableQueries As cTableQueries,
                                                         ByVal p_mcResultsDisplayed As List(Of cMCResult)) As List(Of cMCResult)
        Dim mcResultKeyMatch As Boolean
        Dim mcResultBMHeader As String
        Dim mcResultDisplayedBMHeader As String = "-1"
        Dim mcResultsHidden As New List(Of cMCResult)
        Dim mcResultsAll As New List(Of cMCResult)

        For Each mcResult As cMCResult In p_tableQueries.Results
            mcResultKeyMatch = False

            mcResultBMHeader = mcResult.benchmark.name

            For Each mcResultDisplayed As cMCResult In p_mcResultsDisplayed
                If mcResultDisplayed.query.ID = mcResult.query.ID Then
                    mcResultDisplayedBMHeader = mcResultDisplayed.benchmark.name

                    'Blank BM check from total collection is to avoid keeping the initial blank entry. 
                    'Blank BM check and benchmark key for displayed BM is to avoid keeping the non-blank entry if removing the last result. 
                    'Any added BM will not be blank unless the benchmark is being removed.
                    If (mcResultBMHeader = mcResultDisplayedBMHeader OrElse
                        String.IsNullOrEmpty(mcResultBMHeader) OrElse
                        (String.IsNullOrEmpty(mcResultDisplayedBMHeader) AndAlso
                         mcResultDisplayed.benchmark.ID = 0) AndAlso
                        mcResult.benchmark.ID = 0) Then

                        mcResultKeyMatch = True
                        Exit For
                    Else
                        'No match found yet
                    End If
                End If
            Next
            If Not mcResultKeyMatch Then mcResultsHidden.Add(mcResult)
        Next

        'Create total combined collection
        mcResultsAll = p_mcResultsDisplayed
        For Each result As cMCResult In mcResultsHidden
            mcResultsAll.Add(result)
        Next

        Return mcResultsAll
    End Function

    ''' <summary>
    ''' Removes a result object from the provided list that corresponds to the provided keys.
    ''' </summary>
    ''' <param name="p_mcResultsAll">Total collection of cMCResult objects to check for result objects to remove.</param>
    ''' <param name="p_queryKeyRemove">Query key that corresponds to the result objects not to be preserved.</param>
    ''' <param name="p_benchmarkKeyRemove">Benchmark key that corresponds to the result objects of the specified query not to be preserved. 
    ''' If blank, no results will be preserved for the specified row.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultsCollectionToRemove(ByVal p_mcResultsAll As List(Of cMCResult),
                                                    ByVal p_queryKeyRemove As Integer,
                                                    ByVal p_benchmarkKeyRemove As Integer) As List(Of cMCResult)
        Dim mcResultsFinal As New List(Of cMCResult)

        For Each mcResult As cMCResult In p_mcResultsAll
            If (mcResult.query.ID = p_queryKeyRemove AndAlso mcResult.benchmark.ID = p_benchmarkKeyRemove) Then
                RemoveIncompleteResultsItem(mcResult)
            Else
                mcResultsFinal.Add(mcResult)
            End If
        Next

        Return mcResultsFinal
    End Function

    ''' <summary>
    ''' Updates the form's property collection of incomplete results.
    ''' </summary>
    ''' <param name="p_mcResultsTotal">Collection of result objects to check for completion.</param>
    ''' <param name="p_tableQueries">Table queries used to determine the result objects collection.</param>
    ''' <remarks></remarks>
    Private Sub UpdateIncompleteResultsCollection(ByVal p_mcResultsTotal As List(Of cMCResult),
                                                    ByVal p_tableQueries As cTableQueries)

        Try
            Dim newIncompleteResults As New List(Of cMCResult)
            For Each result As cMCResult In p_mcResultsTotal
                If Not (result.isComplete AndAlso result.query.isUnique(exportedSetTable, result.tableName)) Then
                    newIncompleteResults.Add(result)
                End If
            Next

            'Ensure that the table queries. being used is referencing the form's table queries before updating the form's property
            If p_tableQueries Is _tableQueriesTotal Then
                ReplaceIncompleteResults(newIncompleteResults)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Private - Event Handlers"
    'Not currently used.
    Private Sub IncompleteResults_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _tableQueriesIncomplete.PropertyChanged
        'RaiseEvent Messenger(New MessengerEventArgs(e.PropertyName))
    End Sub
#End Region

End Class
