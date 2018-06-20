Option Explicit On
Option Strict On

Imports System.Data
Imports System.Data.OleDb

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Public Class cDataTableController
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Initialization"
    ''' <summary>
    ''' Returns a list of DataTables from the specifed data source.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DataTablesListFromFile(ByVal p_dataSource As String) As List(Of DataTable)
        Dim fileType As String = GetSuffix(p_dataSource, ".").ToUpper
        Dim importedTableSet As DataSet = Nothing
        Dim importedDataTables As New List(Of DataTable)

        Try
            Select Case fileType
                Case "MDB"
                    importedDataTables = DataTableListFromMDB(p_dataSource)
                Case "XML"
                    importedTableSet = DataSetFromXML(p_dataSource)
                    For Each tableItem As DataTable In importedTableSet.Tables
                        importedDataTables.Add(tableItem)
                    Next
                Case Else
                    Throw New ArgumentException("The file extension of the following data source is not valid. Please choose either an *.mdb or *.xml file: " & Environment.NewLine & Environment.NewLine & p_dataSource)
            End Select
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        End Try

        Return importedDataTables
    End Function

    Friend Function DataSetFromMDB(ByVal p_dataSource As String) As DataSet
        'Dim objCmd As OleDbDataAdapter = Nothing
        If Not IO.File.Exists(p_dataSource) Then Return Nothing

        Dim ds As DataSet = Nothing

        Dim currentProvider As String = ""
        Dim providers As New List(Of String)
        providers.Add("Microsoft.ACE.OLEDB.12.0")
        providers.Add("Microsoft.Jet.OLEDB.4.0")

        Try
            For Each provider As String In providers
                currentProvider = provider
                ds = GetDataSet(provider, p_dataSource)
                'objCmd = GetAdapter(provider, p_dataSource, p_tableName, p_query)
                If ds IsNot Nothing Then Exit For
            Next

            If (ds Is Nothing OrElse ds.Tables.Count = 0) Then
                Throw New ArgumentException("The provider {1} was unable to work with the file.", currentProvider)
            End If
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        End Try

        Return ds
    End Function

    Private Function GetDataSet(ByVal p_provider As String,
                            ByVal p_dataSource As String) As DataSet
        Dim ds As New DataSet
        Dim objCmd As OleDbDataAdapter = Nothing
        Dim connectionString As String = "Provider=" & p_provider & ";Data Source=" & p_dataSource

        Try
            Dim accessDataBase As New cDatabaseAccess
            Dim tablesList As New List(Of String)
            tablesList = accessDataBase.ListAllAccessTables(p_dataSource)

            'Using conn As New OleDbConnection(connectionString)
            For Each tableName In tablesList
                'objCmd = New OleDbDataAdapter("SELECT * FROM " & tableName, conn)
                objCmd = GetAdapter(p_provider, p_dataSource, tableName, "")
                If objCmd IsNot Nothing Then objCmd.Fill(ds, tableName)
            Next
            'End Using
        Catch ex As Exception
            Return Nothing
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Returns a list of DataTable objects from the specified table file.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DataTableListFromMDB(ByVal p_dataSource As String) As List(Of DataTable)
        Dim dataTables As New List(Of DataTable)

        Try
            Dim fileType As String = GetSuffix(p_dataSource, ".").ToUpper
            If (Not IO.File.Exists(p_dataSource) OrElse
                Not StringsMatch(fileType, "MDB")) Then
                Return dataTables
            End If

            Dim accessDataBase As New cDatabaseAccess
            Dim tablesList As New List(Of String)
            tablesList = accessDataBase.ListAllAccessTables(p_dataSource)

            For Each tableName As String In tablesList
                Dim dt As DataTable = DataTableFromMDB(p_dataSource, tableName)

                If dt IsNot Nothing Then dataTables.Add(dt)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return dataTables
    End Function

    ''' <summary>
    ''' Returns a datatable object from the specified file. Handles XML &amp; Access formats.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <param name="p_tableName">Name of the table to load in the datagrid from the exported tables file.</param>
    ''' <param name="p_query">Query to limit what data from the table to return. If not specified, everything is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DataTableFromFile(ByVal p_dataSource As String,
                                      ByVal p_tableName As String,
                                      Optional ByVal p_query As String = "") As DataTable
        Dim fileType As String = GetSuffix(p_dataSource, ".").ToUpper
        Dim importedTable As DataTable = Nothing

        p_tableName = ParseTableName(p_tableName, True)

        Try
            Select Case fileType
                Case "MDB" : importedTable = DataTableFromMDB(p_dataSource, p_tableName, p_query)
                Case "XML" : importedTable = DataTableFromXML(p_dataSource, p_tableName)
                Case Else : Throw New ArgumentException("The file extension of the following datasource is not valid. Please choose either an *.mdb or *.xml file: " & Environment.NewLine & Environment.NewLine & p_dataSource)
            End Select
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        End Try

        Return importedTable
    End Function

    ''' <summary>
    ''' Creates a datatable filled with data from a specified *.mdb file.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <param name="p_tableName">Name of the table to load in the datagrid from the exported tables file.</param>
    ''' <param name="p_query">Query to limit what data from the table to return. If not specified, everything is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DataTableFromMDB(ByVal p_dataSource As String,
                                      ByVal p_tableName As String,
                                      Optional ByVal p_query As String = "") As DataTable
        If Not IO.File.Exists(p_dataSource) Then Return Nothing

        Dim dt As DataTable = Nothing

        Dim currentProvider As String = ""
        Dim providers As New List(Of String)
        providers.Add("Microsoft.ACE.OLEDB.12.0")
        providers.Add("Microsoft.Jet.OLEDB.4.0")

        Try
            For Each provider As String In providers
                currentProvider = provider
                dt = DataTableFromMicrosoftProvider(provider, p_dataSource, p_tableName, p_query)
                If dt IsNot Nothing Then Exit For
            Next

            If (dt Is Nothing OrElse dt.Rows.Count = 0) Then
                Throw New ArgumentException("The provider {1} was unable to work with the file.", currentProvider)
            End If
        Catch exArg As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(exArg))
        End Try

        Return dt
    End Function

    Private Function DataTableFromMicrosoftProvider(ByVal p_provider As String,
                                   ByVal p_dataSource As String,
                                   ByVal p_tableName As String,
                                   ByVal p_query As String) As DataTable
        Dim dt As New DataTable(p_tableName)

        p_tableName = ParseTableName(p_tableName, False)

        Dim objCmd As OleDbDataAdapter = Nothing
        Dim connectionString As String = "Provider=" & p_provider & ";Data Source=" & p_dataSource

        Try
            Using conn As New OleDbConnection(connectionString)
                'objCmd = ReadConnection(conn, p_tableName, p_query)

                If String.IsNullOrEmpty(p_query) Then
                    objCmd = New OleDbDataAdapter("SELECT * FROM " & p_tableName, conn)
                Else
                    objCmd = New OleDbDataAdapter("SELECT " & p_query & " FROM " & p_tableName, conn)
                End If
                If objCmd IsNot Nothing Then objCmd.Fill(dt)
            End Using
        Catch ex As Exception
            Return Nothing
        End Try

        Return dt
    End Function

    'TODO: Remove VVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_provider"></param>
    ''' <param name="p_dataSource"></param>
    ''' <param name="p_tableName"></param>
    ''' <param name="p_query"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAdapter(ByVal p_provider As String,
                                ByVal p_dataSource As String,
                                ByVal p_tableName As String,
                                ByVal p_query As String) As OleDbDataAdapter
        Dim objCmd As OleDbDataAdapter = Nothing
        Dim connectionString As String = "Provider=" & p_provider & ";Data Source=" & p_dataSource

        Try
            Using conn As New OleDbConnection(connectionString)
                objCmd = ReadConnection(conn, p_tableName, p_query)
            End Using
        Catch ex As Exception

        End Try

        Return objCmd
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_conn"></param>
    ''' <param name="p_tableName"></param>
    ''' <param name="p_query"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadConnection(ByVal p_conn As OleDbConnection,
                                    ByVal p_tableName As String,
                                    ByVal p_query As String) As OleDbDataAdapter
        Dim objCmd As OleDbDataAdapter
        If String.IsNullOrEmpty(p_query) Then
            objCmd = New OleDbDataAdapter("SELECT * FROM " & p_tableName, p_conn)
        Else
            objCmd = New OleDbDataAdapter("SELECT " & p_query & " FROM " & p_tableName, p_conn)
        End If

        Return objCmd
    End Function

    '^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    ''' <summary>
    ''' Creates a datatable filled with data from a specified *.mdb file.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <param name="p_tableName">Name of the table to load in the datagrid from the exported tables file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DataTableFromXML(ByVal p_dataSource As String,
                                      ByVal p_tableName As String) As DataTable
        Try
            If Not IO.File.Exists(p_dataSource) Then Return Nothing
            Dim xmlResults As DataSet = DataSetFromXML(p_dataSource)

            Dim table As New DataTable
            table = xmlResults.Tables(p_tableName)

            Return table
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_dataSource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DataSetFromXML(ByVal p_dataSource As String) As DataSet
        Dim xmlResults As New DataSet ' data sets with results
        'Dim dataTable As DataTable

        Try
            'This and the ending For Loop done for best performance on large files:
            ' https://msdn.microsoft.com/en-us/library/fx29c3yd(v=vs.110).aspx
            For Each dataTable As DataTable In xmlResults.Tables
                dataTable.BeginLoadData()
            Next

            xmlResults.ReadXml(p_dataSource)

            For Each dataTable As DataTable In xmlResults.Tables
                dataTable.EndLoadData()
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return xmlResults
    End Function

    ''' <summary>
    ''' Returns a list of values contained within the specifed column of the specified table file.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <param name="p_tableName">Name of the table to load in the datagrid from the exported tables file.</param>
    ''' <param name="p_colName">Name of the column header from which to return values.</param>
    ''' <param name="p_query">Query to limit what data from the table to return. If not specified, everything is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetTableValueFromFile(ByVal p_dataSource As String,
                                          ByVal p_tableName As String,
                                          ByVal p_colName As String,
                                          Optional p_query As String = "") As List(Of String)
        Dim tempList As New List(Of String)
        Try
            Dim table As DataTable = DataTableFromFile(p_dataSource, p_tableName)
            If table Is Nothing Then Return Nothing

            If String.IsNullOrEmpty(p_query) Then
                For Each row As DataRow In table.Rows
                    tempList.Add(row.Item(p_colName).ToString)
                Next
            Else
                Dim rows As DataRow()
                rows = table.Select(p_query)
                For Each row As DataRow In rows
                    tempList.Add(row.Item(p_colName).ToString)
                Next
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return Nothing
        End Try

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all tables in the specified file.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the exported tables file to be read.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ListAllTables(ByVal p_dataSource As String) As List(Of String)
        Dim tablesList As New List(Of String)
        Dim fileType As String = GetSuffix(p_dataSource, ".").ToUpper

        Select Case fileType
            Case "MDB"
                Dim accessDataBase As New cDatabaseAccess
                tablesList = accessDataBase.ListAllAccessTables(p_dataSource)
            Case "XML"
                Dim xmlResults As DataSet = DataSetFromXML(p_dataSource)
                If xmlResults Is Nothing Then Return Nothing
                For Each Table As DataTable In xmlResults.Tables
                    tablesList.Add(Table.TableName)
                Next

            Case Else : Throw New ArgumentException("The file extension of the following datasource is not valid. Please choose either an *.mdb or *.xml file: " & Environment.NewLine & Environment.NewLine & p_dataSource)
        End Select

        Return tablesList
    End Function



#End Region

#Region "Methods: Navigation/Query, Validation"
    '=== Navigation/Query
    ''' <summary>
    ''' Returns the header string of the specified column index for a DataTable.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to which to apply the function.</param>
    ''' <param name="p_colIndex">Column index for the desired column header.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function HeaderFromColumnIndex(ByVal p_dataTable As DataTable,
                                          ByVal p_colIndex As Integer) As String
        Dim header As String = ""
        Try
            If IsValidDataTableColumnIndex(p_dataTable, p_colIndex) Then
                header = p_dataTable.Columns(p_colIndex).ColumnName
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return header
    End Function
    ''' <summary>
    ''' Returns the column index of the column header that matches the provided name. Returns -1 if no match is found.
    ''' </summary>
    ''' <param name="p_header">Header name to match.</param>
    ''' <param name="p_row">DataRow object to search.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ColumnIndexFromHeader(ByVal p_header As String,
                                          ByVal p_row As DataRow) As Integer
        Dim colIndex As Integer = -1
        Dim colIndexMatch As Integer = -1

        For Each dc As DataColumn In p_row.Table.Columns
            colIndex += 1

            If dc.ColumnName = p_header Then
                colIndexMatch = colIndex
                Exit For
            End If
        Next

        Return colIndexMatch
    End Function

    '=== Validation
    ''' <summary>
    ''' For DataTable, column index should be 0 or greater, and no more than the number of columns in the DataTable. Returns true if this is the case, false otherwise.
    ''' </summary>
    ''' <param name="p_dataTable">DataTable object to which to apply the function.</param>
    ''' <param name="p_colIndex">Column index to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsValidDataTableColumnIndex(ByVal p_dataTable As DataTable,
                                                ByVal p_colIndex As Integer) As Boolean
        If (0 <= p_colIndex AndAlso p_colIndex <= p_dataTable.Columns.Count - 1) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
