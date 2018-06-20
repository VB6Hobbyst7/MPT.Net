Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports CSiTester.cNodeAssemblerXML
Imports CSiTester.cLibFilesXML

'TODO: Not implemented yet. Current code is a template from the cSettings class, adjusted to be read only.
''' <summary>
''' Handles all actions of reading/writing from/to files associated with the Model Results class.
''' </summary>
''' <remarks></remarks>
Public Class cModelResultsAdapter
#Region "Enumerations"
    ''' <summary>
    ''' Read/write actions available for this class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eReadWriteAction
        readAll
    End Enum
#End Region

#Region "Variables"
    Private _dataWriter As New cReadWriteXML()
    Private _isReading As Boolean = True

    Private _xmlPath As String
    Private _settings As cSettings
#End Region

#Region "Properties: XML File"
    '= RegTest
    Private _path_regTestName As cXMLNode

#End Region

#Region "Methods: Friend"
    Friend Sub Fill(ByRef p_settings As cSettings)
        If p_settings Is Nothing Then Exit Sub

        _xmlPath = p_settings.xmlPath
        _settings = p_settings

        InitializeXMLNodePaths()
        ReadWriteToFromFile(eReadWriteAction.readAll)
    End Sub
#End Region

#Region "Methods: Private - General"
    ''' <summary>
    ''' Maps the class properties to the node locations, including necssary type conversions.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeXMLNodePaths()
        'Dim isReadOnly As Boolean = True

        'Dim ns As String = "/n:"

        'Dim pathParent As String
        'Dim pathChildFirst As String
        'Dim pathAssembled As String


        ''= RegTest
        'pathParent = "regtest"
        'pathAssembled = AssemblePathStub(pathParent)

        '_path_regTestName = InitializeXMLNode(pathAssembled & "regtest_name")

    End Sub

    ''' <summary>
    ''' Does various types of operations that read from or write to a file, depending on the action specified.
    ''' </summary>
    ''' <param name="p_fileAction">Action specified, of either a read or write nature.
    ''' There can be multiple actions for reading ro writing.</param>
    ''' <param name="p_list">List to populate from a read option.</param>
    ''' <param name="p_pathListFileTypes">Node mapping and instructions for the relavant file types.</param>
    Private Sub ReadWriteToFromFile(ByVal p_fileAction As eReadWriteAction,
                                    Optional ByRef p_list As ObservableCollection(Of String) = Nothing,
                                    Optional ByVal p_pathListFileTypes As cXMLNode = Nothing)
        Try
            If InitializeXML(_xmlPath) Then
                Select Case p_fileAction
                    Case eReadWriteAction.readAll
                        ReadWriteAllFromFile(_isReading)
                End Select

                If Not _isReading Then SaveXML(_xmlPath)
                CloseXML()
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub
#End Region

#Region "Methods: Private - Passed Object"
    ''' <summary>
    ''' Reads all data from a file, or writes all data to a file.
    ''' </summary>
    ''' <param name="p_isReading">If true, then all data will be read from the file into the object.
    ''' If false, the the existing data will be read to the file.</param>
    Private Sub ReadWriteAllFromFile(ByVal p_isReading As Boolean)
        With _dataWriter
            '= RegTest
            .ReadWriteAction(_settings.regTestName, _path_regTestName, p_isReading)
        End With
    End Sub
#End Region

#Region "Methods: Private - Reading"

#End Region
End Class
