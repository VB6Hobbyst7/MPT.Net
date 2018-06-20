Option Strict On
Option Explicit On

Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Access
Imports System.Data.OleDb
Imports System.Data

Imports MPT.Reporting
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.String.ConversionLibrary

''' <summary>
''' Class that performs various operations with Microsoft Access *.mdb files.
''' </summary>
''' <remarks></remarks>
Public Class cDatabaseAccess
    Public Event Message(message As MessengerEventArgs)
    Public Event Log(exception As LoggerEventArgs)

    'Set up COM reference to:
    '1. Microsoft Access 14.0 Object Library

    'References:
    'http://www.accessforums.net/import-export-data/multiple-table-xml-export-15117.html
    'Access object model reference (Access 2013 developer reference): http://msdn.microsoft.com/en-us/library/ff192120(v=office.15).aspx
    'Application.ExportXML Method (Access): http://msdn.microsoft.com/en-us/library/ff193212.aspx
    'OleDb Basics in VB.Net: http://www.dreamincode.net/forums/topic/33908-oledb-basics-in-vbnet/
#Region "Properties"
    Friend oAccess As Access.Application
#End Region

#Region "Methods: Public"

    ''' <summary>
    ''' Exports an XML file and a schema file of the specified access file.
    ''' </summary>
    ''' <param name="p_pathMDB">Path to the existing Access *.mdb file.</param>
    ''' <param name="p_pathXML">Path to the XML file to be created.</param>
    ''' <param name="p_pathXSD">Path to the XSD schema file to be created.</param>
    ''' <remarks></remarks>
    Public Sub ExportMDBtoXMLSchema(ByVal p_pathMDB As String,
                                    ByVal p_pathXML As String,
                                    ByVal p_pathXSD As String)
        Try
            OpenAccessInWindow(p_pathMDB)

            Dim tablesList As List(Of String) = ListAllAccessTables(p_pathMDB)
            Dim objSchedule As AdditionalData = oAccess.CreateAdditionalData
            objSchedule = objSchedule.Add(tablesList(1))
            If tablesList.Count > 2 Then
                For i = 2 To tablesList.Count - 1
                    objSchedule.Add(tablesList(i))
                Next
            End If

            If Right(p_pathXML, 4) <> ".xml" Then p_pathXML &= ".xml"
            If Right(p_pathXSD, 4) <> ".xsd" Then p_pathXML &= ".xsd"
            oAccess.ExportXML(AcExportXMLObjectType.acExportTable,
                              tablesList(0),
                              p_pathXML,
                              p_pathXSD, , , ,
                              AcExportXMLOtherFlags.acExportAllTableAndFieldProperties, ,
                              objSchedule)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                    NameOfParam(Function() p_pathMDB), p_pathMDB,
                                    NameOfParam(Function() p_pathXML), p_pathXML,
                                    NameOfParam(Function() p_pathXSD), p_pathXSD))
        Finally
            CloseAccessInWindow()
        End Try
    End Sub


    ''' <summary>
    ''' Returns a list of values gathered from an Access *.mdb file, based on search parameters specified.
    ''' </summary>
    ''' <param name="p_path">Path to the Access *.mdb file.</param>
    ''' <param name="p_tableName">Name of the table to search.</param>
    ''' <param name="p_colName">Name of the column to retrieve data from.</param>
    ''' <param name="p_query">Optional: Search query, for filtering results.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTableValue(ByVal p_path As String,
                                  ByVal p_tableName As String,
                                  ByVal p_colName As String,
                                  Optional p_query As String = "") As List(Of String)
        Try
            Dim provider As String = "Microsoft.ACE.OLEDB.12.0"
            Dim connectionString As String
            Dim objCmd As OleDbDataAdapter
            Dim dt As DataTable
            Dim tempList As New List(Of String)

            connectionString = "Provider=" & provider & ";Data Source=" & p_path

            Using conn As New OleDbConnection(connectionString)
                If String.IsNullOrEmpty(p_query) Then
                    objCmd = New OleDbDataAdapter("SELECT * FROM " & p_tableName, conn)
                Else
                    objCmd = New OleDbDataAdapter("SELECT " & p_query & " FROM " & p_tableName, conn)
                End If

                dt = New DataTable(p_tableName)
                objCmd.Fill(dt)

                For Each row As DataRow In dt.Rows
                    tempList.Add(row.Item(p_colName).ToString)
                Next

                Return tempList
            End Using

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() p_path), p_path,
                                               NameOfParam(Function() p_tableName), p_tableName,
                                               NameOfParam(Function() p_query), p_query,
                                               NameOfParam(Function() p_colName), p_colName))
            Return Nothing
        Finally

        End Try

    End Function


    ''' <summary>
    ''' Generates a list of all of the tables contained within the specified Access *.mdb file.
    ''' </summary>
    ''' <param name="p_path">Path to the Access *.mdb file to get the table list from.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAccessTables(ByVal p_path As String) As List(Of String)
        Dim tablesList As New List(Of String)

        Dim connection As System.Data.OleDb.OleDbConnection = New System.Data.OleDb.OleDbConnection()
        Try
            'TODO: Improve this similar to adding tables in the DT Controller.
            connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & p_path

            ' We only want user tables, not system tables
            Dim restrictions() As String = New String(3) {}
            restrictions(3) = "Table"
            connection.Open()

            ' Get list of user tables
            Dim userTables As DataTable = connection.GetSchema("Tables", restrictions)

            ' Add list of table names to listBox
            For i As Integer = 0 To userTables.Rows.Count - 1 Step i + 1
                tablesList.Add(userTables.Rows(i)(2).ToString())
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_path), p_path))
        Finally
            connection.Close()
            connection = Nothing
        End Try

        Return tablesList
    End Function



    'Not working yet. See ex below
    Public Function XmlExport(ByVal p_path As String,
                              ByVal p_tableName As String,
                              ByVal p_fileName As String) As Boolean
        Try
            Dim objDataSet As New DataSet
            Dim objXmlDocument As New System.Xml.XmlDocument
            Dim objCmd As OleDbDataAdapter
            Dim objCon As OleDbConnection
            Dim strCon As String

            p_tableName = ParseTableName(p_tableName, False)

            'Establish connection to database
            'strCon = "provider=Microsoft.Jet.OLEDB.4.0; data source=" & strDBpath
            strCon = "provider=Microsoft.ACE.OLEDB.12.0; data source=" & p_path
            objCon = New OleDbConnection(strCon)

            'Get values from database
            objCmd = New OleDbDataAdapter("select * from " & p_tableName, objCon)
            objCmd.Fill(objDataSet)     'Ex: Unrecognized database format (.mdb)
            objCon.Close()

            'Save as XML
            objXmlDocument.LoadXml(objDataSet.GetXml())
            objXmlDocument.Save(p_fileName)

            Return True
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() p_path), p_path,
                                               NameOfParam(Function() p_tableName), p_tableName,
                                               NameOfParam(Function() p_fileName), p_fileName))
        End Try
        Return False
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Opens the Access file to extract information.
    ''' </summary>
    ''' <param name="p_path">Path to the Access *.mdb file.</param>
    ''' <remarks></remarks>
    Private Sub OpenAccessInWindow(ByVal p_path As String)
        oAccess = New Access.Application

        oAccess.OpenCurrentDatabase(p_path)
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

    'Not used
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DBpath">Path to the Access *.mdb file.</param>
    ''' <remarks></remarks>
    Private Sub OpenAccessWoWindow(ByVal DBpath As String)
        Dim strCon As String = "provider=Microsoft.Jet.OLEDB.4.0; data source=" & DBpath
        

        Using dataSource As New OleDbConnection(strCon)
            'Establish connection to database

        End Using
    End Sub
#End Region
End Class
