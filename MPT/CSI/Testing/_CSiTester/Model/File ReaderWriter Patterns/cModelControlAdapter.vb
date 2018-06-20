Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports CSiTester.cNodeAssemblerXML
Imports CSiTester.cLibFilesXML
Imports CSiTester.cLibFolders

'TODO: Not implemented yet. Current code is a template from the cSettings class
''' <summary>
''' Handles all actions of reading/writing from/to files associated with the Model Control class.
''' </summary>
''' <remarks></remarks>
Public Class cModelControlAdapter
#Region "Enumerations"
    ''' <summary>
    ''' Read/write actions available for this class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eReadWriteAction
        readAll
        writeAll
    End Enum
#End Region

#Region "Variables"
    Private _dataWriter As New cReadWriteXML()
    Private _isReading As Boolean

    Private _xmlPath As String
    Private _settings As cSettings
#End Region

#Region "Variables - XML File"
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

    ''' <summary>
    ''' Writes the contents of the associated object to a file.
    ''' </summary>
    ''' <param name="p_writeAction">Write action to perform.</param>
    ''' <param name="p_path">If supplied and the file exists, the file will be written to rather than the one associated with the object.</param>
    ''' <remarks></remarks>
    Friend Sub Write(ByVal p_writeAction As eReadWriteAction,
                     Optional ByVal p_path As String = "")
        Dim oldPath As String = _xmlPath
        If (p_path.Length > 0 AndAlso FileExists(p_path)) Then
            _xmlPath = p_path
        End If

        If (p_writeAction = eReadWriteAction.writeAll) Then
            ReadWriteToFromFile(p_writeAction)
        End If

        If String.Compare(_xmlPath, p_path, True) = 0 Then _xmlPath = oldPath
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
            OpenReadWriteState(p_fileAction)

            If InitializeXML(_xmlPath) Then
                Select Case p_fileAction
                    Case eReadWriteAction.readAll
                        ReadWriteAllFromFile(_isReading)
                    Case eReadWriteAction.writeAll
                        ReadWriteAllFromFile(_isReading)
                End Select

                If Not _isReading Then SaveXML(_xmlPath)
                CloseXML()
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        Finally
            CloseReadWriteState()
        End Try
    End Sub

    ''' <summary>
    ''' Triggers the appropriate reading/writing flags based on the file action specified.
    ''' </summary>
    ''' <param name="p_fileAction">File action that is used to determined if a reading or writing operation is taking place.</param>
    ''' <remarks></remarks>
    Private Sub OpenReadWriteState(ByVal p_fileAction As eReadWriteAction)
        Select Case p_fileAction
            Case eReadWriteAction.readAll
                _isReading = True
            Case eReadWriteAction.writeAll
                _isReading = False
        End Select
    End Sub

    ''' <summary>
    ''' Resets any set reading or writing flags.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseReadWriteState()
        _isReading = False
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
