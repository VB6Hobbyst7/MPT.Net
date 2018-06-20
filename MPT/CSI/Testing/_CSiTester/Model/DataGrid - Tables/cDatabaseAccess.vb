Option Strict On
Option Explicit On

Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Access
Imports System.Data.OleDb
Imports System.Data

Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

''' <summary>
''' Class that performs various operations with Microsoft Access *.mdb files.
''' </summary>
''' <remarks></remarks>
Public Class cDatabaseAccess
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

    'Set up COM reference to:
    '1. Microsoft Access 14.0 Object Library

    'References:
    'http://www.accessforums.net/import-export-data/multiple-table-xml-export-15117.html
    'Access object model reference (Access 2013 developer reference): http://msdn.microsoft.com/en-us/library/ff192120(v=office.15).aspx
    'Application.ExportXML Method (Access): http://msdn.microsoft.com/en-us/library/ff193212.aspx
    'OleDb Basics in VB.Net: http://www.dreamincode.net/forums/topic/33908-oledb-basics-in-vbnet/

    Dim XML_PathEdit As String = "Test"
    Friend oAccess As Access.Application


#Region "Properties"

#End Region

#Region "Initialization"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Exports an XML file adn a schema file of the specified access file.
    ''' </summary>
    ''' <param name="pathMDB">Path to the existing Access *.mdb file.</param>
    ''' <param name="pathXML">Path to the XML file to be created.</param>
    ''' <param name="pathXSD">Path to the XSD schema file to be created.</param>
    ''' <remarks></remarks>
    Friend Sub ExportMDBtoXMLSchema(ByVal pathMDB As String, ByVal pathXML As String, ByVal pathXSD As String)
        Try
            'export query to selected XML file
            Dim exportFilePath As String
            Dim exportSchemaPath As String
            Dim objSchedule As AdditionalData
            Dim tableObject As AcExportXMLObjectType = AcExportXMLObjectType.acExportTable
            Dim tablesList As New List(Of String)

            ' Dim appAccess As New Access.Application

            'OpenAccessInWindow("C:\Backups\ETABS v13_1_5_Run 2\Models\Analysis Examples\Example 01.mdb")

            'exportFilePath = "C:\Backups\ETABS v13_1_5_Run 2\Models\Analysis Examples\" & "Test.xml"
            'exportSchemaPath = "C:\Backups\ETABS v13_1_5_Run 2\Models\Analysis Examples\" & "Text.xsd"

            OpenAccessInWindow(pathMDB)

            exportFilePath = pathXML
            exportSchemaPath = pathXSD

            objSchedule = oAccess.CreateAdditionalData

            If Right(exportFilePath, 4) <> ".xml" Then
                exportFilePath = exportFilePath & ".xml"
            End If

            'objSchedule = objSchedule.Add("Beam Forces")
            'objSchedule.Add("Column Forces")
            'objSchedule.Add("Program Control")

            tablesList = ListAllAccessTables(pathMDB)
            objSchedule = objSchedule.Add(tablesList(1))

            If tablesList.Count > 2 Then
                For i = 2 To tablesList.Count - 1
                    objSchedule.Add(tablesList(i))
                Next
            End If


            oAccess.ExportXML(tableObject, tablesList(0), exportFilePath, exportSchemaPath, , , , AcExportXMLOtherFlags.acExportAllTableAndFieldProperties, , objSchedule) 'objSchedule)
            'appAccess.ExportXML(tableObject, CStr(tableObject), exportFilePath, exportSchemaPath, , , , AcExportXMLOtherFlags.acExportAllTableAndFieldProperties, , objSchedule)
            'appAccess.ExportXML(ObjectType:=acExportQuery, DataSource:="AddressUpdateReturn", DataTarget:=ExportFileStr, SchemaTarget:=, PresentationTarget:=, ImageTarget:=, Encoding:=, OtherFlags:=acEmbedSchema, WhereCondition:=, AdditionalData:=objSchedule)

            CloseAccessInWindow()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
            CloseAccessInWindow()
        End Try
    End Sub

    ''' <summary>
    ''' Opens the Access file to extract information.
    ''' </summary>
    ''' <param name="DBpath">Path to the Access *.mdb file.</param>
    ''' <remarks></remarks>
    Private Sub OpenAccessInWindow(ByVal DBpath As String)
        oAccess = New Access.Application
        'Dim appAccess As New Access.Application

        oAccess.OpenCurrentDatabase(DBpath)
    End Sub

    ''' <summary>
    ''' Closes the Access file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseAccessInWindow()
        If oAccess IsNot Nothing Then
            oAccess.CloseCurrentDatabase()
            oAccess = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Returns a list of values gathered from an Access *.mdb file, based on search parameters specified.
    ''' </summary>
    ''' <param name="DBPath">Path to the Access *.mdb file.</param>
    ''' <param name="tableName">Name of the table to search.</param>
    ''' <param name="colName">Name of the column to retrieve data from.</param>
    ''' <param name="query">Optional: Search query, for filtering results.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetTableValue(ByVal DBPath As String, ByVal tableName As String, ByVal colName As String, Optional query As String = "") As List(Of String)
        Try
            'Dim provider As String = "Microsoft.Jet.OLEDB.4.0"
            Dim provider As String = "Microsoft.ACE.OLEDB.12.0"
            Dim connectionString As String
            Dim objCmd As OleDbDataAdapter
            Dim dt As DataTable
            Dim tempList As New List(Of String)

            connectionString = "Provider=" & provider & ";Data Source=" & DBPath

            Using conn As New OleDbConnection(connectionString)
                If String.IsNullOrEmpty(query) Then
                    objCmd = New OleDbDataAdapter("SELECT * FROM " & tableName, conn)
                Else
                    objCmd = New OleDbDataAdapter("SELECT " & query & " FROM " & tableName, conn)
                End If

                dt = New DataTable(tableName)
                objCmd.Fill(dt)

                For Each row As DataRow In dt.Rows
                    tempList.Add(row.Item(colName).ToString)
                Next

                Return tempList
            End Using

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return Nothing
        Finally

        End Try

    End Function


    ''' <summary>
    ''' Generates a list of all of the tables contained within the specified Access *.mdb file.
    ''' </summary>
    ''' <param name="pathMDB">Path to the Access file to get the table list from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ListAllAccessTables(ByVal pathMDB As String) As List(Of String)
        Dim userTables As DataTable = Nothing
        Dim connection As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection()
        Dim tablesList As New List(Of String)
        Dim sourcePath As String

        Try
            'TODO: Improve this similar to adding tables in the DT Controller.

            'sourcePath = "C:\Backups\ETABS v13_1_5_Run 2\Models\Analysis Examples\Example 01.mdb"
            sourcePath = pathMDB

            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & sourcePath

            ' We only want user tables, not system tables
            Dim restrictions() As String = New String(3) {}
            restrictions(3) = "Table"
            connection.Open()

            ' Get list of user tables
            userTables = connection.GetSchema("Tables", restrictions)

            ' Add list of table names to listBox
            Dim i As Integer
            For i = 0 To userTables.Rows.Count - 1 Step i + 1
                tablesList.Add(userTables.Rows(i)(2).ToString())
            Next

            ListAllAccessTables = tablesList
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)

            ListAllAccessTables = Nothing
        Finally
            connection.Close()
            connection = Nothing
        End Try

    End Function

    'Not used
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DBpath">Path to the Access *.mdb file.</param>
    ''' <remarks></remarks>
    Private Sub OpenAccessWoWindow(ByVal DBpath As String)
        Dim dataSource As OleDbConnection
        Dim strCon As String

        'Establish connection to database
        strCon = "provider=Microsoft.Jet.OLEDB.4.0; data source=" & DBpath
        dataSource = New OleDbConnection(strCon)

        'Close connection w/o window
        dataSource.Close()
    End Sub

    'Not working yet. See ex below
    Friend Function XmlExport(ByVal strDBpath As String, ByVal strTableName As String, ByVal strXmlFileName As String) As Boolean
        Try
            Dim objDataSet As New DataSet
            Dim objXmlDocument As New System.Xml.XmlDocument
            Dim objCmd As OleDbDataAdapter
            Dim objCon As OleDbConnection
            Dim strCon As String

            strTableName = ParseTableName(strTableName, False)

            'Establish connection to database
            'strCon = "provider=Microsoft.Jet.OLEDB.4.0; data source=" & strDBpath
            strCon = "provider=Microsoft.ACE.OLEDB.12.0; data source=" & strDBpath
            objCon = New OleDbConnection(strCon)

            'Get values from database
            objCmd = New OleDbDataAdapter("select * from " & strTableName, objCon)
            objCmd.Fill(objDataSet)     'Ex: Unrecognized database format (.mdb)
            objCon.Close()

            'Save as XML
            objXmlDocument.LoadXml(objDataSet.GetXml())
            objXmlDocument.Save(strXmlFileName)

            XmlExport = True
        Catch ex As Exception
            XmlExport = False
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try
    End Function

#End Region
End Class
