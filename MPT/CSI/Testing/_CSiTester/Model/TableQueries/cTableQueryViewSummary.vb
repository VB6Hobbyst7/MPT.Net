
Option Strict On
Option Explicit On

Imports System.Data
Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cTableQueryView

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class cTableQueryViewSummary
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"

#End Region

#Region "Enumerations"

#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' cTableQuery class composed of incomplete results.
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents _summaryTableQueries As cTableQueries
#End Region

#Region "Properties: Friend"
    Private _summaryResultsTable As DataTable
    ''' <summary>
    ''' Class containing the data of the incomplete result objects.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property summaryResultsTable As DataTable
        Get
            Return _summaryResultsTable
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    ''' <summary>
    ''' Constructor that automatically populates data for the DataGrid.
    ''' </summary>
    ''' <param name="p_summaryResults">Class containing all of the results that are to be summarized.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByRef p_summaryResults As cTableQueries)
        InitializeData()

        AddColumns(_summaryResultsTable)

        _summaryTableQueries = p_summaryResults

        'Fill queries & value references into current dataTable
        UpdateDataTable(p_summaryResults)
    End Sub

    Private Sub InitializeData()
        _summaryResultsTable = New DataTable
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Parses data from the incomplete results to the layout of the class DataTable.
    ''' </summary>
    ''' <param name="p_incompleteResults">Class containing all of the results that don't have the minimum complete information.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateDataTable(ByRef p_incompleteResults As cTableQueries)
        If p_incompleteResults IsNot Nothing Then
            InitializeRecordsFromResults(_summaryResultsTable, p_incompleteResults)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("incompleteResultsTable"))
        End If
    End Sub

    'This is duplicated from cResultsDataTable
    ''' <summary>
    ''' Returns the header for the status tag column for the associated header provided.
    ''' </summary>
    ''' <param name="p_header">Header to get the corresponding status tag column header from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetCellStatusTagHeader(ByVal p_header As String) As String
        Dim cellStatusTagHeader As String

        If StringExistInName(p_header, HEADER_SUFFIX) Then
            cellStatusTagHeader = p_header
        Else
            cellStatusTagHeader = p_header & HEADER_SUFFIX
        End If

        Return cellStatusTagHeader
    End Function
#End Region

#Region "Methods: Private"

    Private Sub AddColumns(ByRef p_dataTable As DataTable)
        'Create Query Key column
        p_dataTable.Columns.Add(KEY_QUERY_HEADER, Type.GetType("System.Int32"))

        'Create Benchmark Key column
        p_dataTable.Columns.Add(KEY_BM_HEADER, Type.GetType("System.Int32"))

        'Create Result Name Column
        p_dataTable.Columns.Add(NAME_HEADER, Type.GetType("System.String"))

        'Create BM Column
        p_dataTable.Columns.Add(BM_HEADER, Type.GetType("System.String"))

        'Create Query Column
        p_dataTable.Columns.Add(QUERY_HEADER, Type.GetType("System.String"))


        'Create Result Name Status Column
        p_dataTable.Columns.Add(GetCellStatusTagHeader(NAME_HEADER), Type.GetType("System.String"))

        'Create BM Status Column
        p_dataTable.Columns.Add(GetCellStatusTagHeader(BM_HEADER), Type.GetType("System.String"))

        'Create Query Status Column
        p_dataTable.Columns.Add(GetCellStatusTagHeader(QUERY_HEADER), Type.GetType("System.String"))


        'Set up primary keys
        Dim PrimaryKeyColumns() As DataColumn = New DataColumn(1) {}
        PrimaryKeyColumns(0) = p_dataTable.Columns(KEY_QUERY_HEADER)
        PrimaryKeyColumns(1) = p_dataTable.Columns(KEY_BM_HEADER)
        p_dataTable.PrimaryKey = PrimaryKeyColumns
    End Sub


    ''' <summary>
    ''' Populates the DataTable with the desired properties from the results or derived from the results.
    ''' </summary>
    ''' <param name="p_dataTable">Data table to populate from the results.</param>
    ''' <param name="p_tableQueries">Current cTableQuery class being used for the displayed table. Contains multiple cMCResults classes.</param>
    ''' <remarks></remarks>
    Private Sub InitializeRecordsFromResults(ByRef p_dataTable As DataTable,
                                            ByRef p_tableQueries As cTableQueries)
        Try
            For Each result As cMCResult In p_tableQueries.Results

                Dim newResultRow As DataRow = p_dataTable.NewRow
                newResultRow(KEY_QUERY_HEADER) = result.query.ID
                newResultRow(KEY_BM_HEADER) = result.benchmark.ID
                newResultRow(NAME_HEADER) = result.name
                newResultRow(BM_HEADER) = result.benchmark.name
                newResultRow(QUERY_HEADER) = result.query.asString

                Dim commentStatus As String = result.isDetailsComplete.ToString
                newResultRow(GetCellStatusTagHeader(NAME_HEADER)) = commentStatus

                Dim benchmarkStatus As String = result.isBMComplete.ToString
                newResultRow(GetCellStatusTagHeader(BM_HEADER)) = benchmarkStatus

                Dim queryStatus As String = result.query.isUnique(p_dataTable, result.tableName).ToString
                newResultRow(GetCellStatusTagHeader(QUERY_HEADER)) = queryStatus

                p_dataTable.Rows.Add(newResultRow)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub


    Private Sub IncompleteResults_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _summaryTableQueries.PropertyChanged
        'UpdateDataTable(_incompleteTableQueries)
    End Sub


#End Region

End Class
