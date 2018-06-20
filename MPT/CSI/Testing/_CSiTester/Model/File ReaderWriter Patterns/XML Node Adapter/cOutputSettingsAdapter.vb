Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.XML
Imports MPT.XML.ReaderWriter
Imports MPT.XML.NodeAdapter
Imports MPT.XML.NodeAdapter.cNodeAssemblerXML

''' <summary>
''' Handles all actions of reading/writing from/to files associated with the Output Settings class.
''' </summary>
''' <remarks></remarks>
Public Class cOutputSettingsAdapter
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

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

#Region "Fields"
    Private _dataWriter As New cReadWriteXML()
    Private _xmlReaderWriter As New cXmlReadWrite()
    Private _isReading As Boolean

    Private _xmlPath As String
    Private _outputSettings As cFileOutputSettings
#End Region

#Region "Variables - XML File"
    Private _path_saveOS As cXMLNode
    Private _path_fileName As cXMLNode
    Private _path_v9Units As cXMLNode
    Private _path_tableSetName As cXMLNode

    Private _path_showsSelectionOnly As cXMLNode
    Private _path_showOnlyIfUsedInModel As cXMLNode
    Private _path_showAllFields As cXMLNode

    Private _path_forceUnit As cXMLNode
    Private _path_lengthUnit As cXMLNode
    Private _path_temperatureUnit As cXMLNode

    Private _path_groups As cXMLNode
    Private _path_loadPatterns As cXMLNode
    Private _path_loadCases As cXMLNode
    Private _path_loadCombinations As cXMLNode

    Private _path_tables As cXMLNode

    Private _path_multiStep As cXMLNode
    Private _path_combineCaseStepFields As cXMLNode
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeXMLNodePaths()
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Populates all xml-related properties in the class with values from the file the class is associated with.
    ''' </summary>
    ''' <param name="p_outputSettings">Class to fill.</param>
    ''' <remarks></remarks>
    Friend Sub Fill(ByRef p_outputSettings As cFileOutputSettings)
        Try
            Bind(p_outputSettings)

            InitializeXMLNodePaths()
            ReadWriteToFromFile(eReadWriteAction.readAll)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    Friend Sub Bind(ByRef p_outputSettings As cFileOutputSettings)
        Try
            If p_outputSettings Is Nothing Then Exit Sub

            _outputSettings = p_outputSettings
            _xmlPath = _outputSettings.pathDestination.path
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Writes the contents of the associated object to a file. Returns true if this is successful.
    ''' </summary>
    ''' <param name="p_writeAction">Write action to perform.</param>
    ''' <param name="p_path">If supplied and the file exists, the file will be written to rather than the one associated with the object.</param>
    ''' <remarks></remarks>
    Friend Function Write(ByVal p_writeAction As eReadWriteAction,
                          Optional ByVal p_path As String = "") As Boolean
        Dim oldPath As String = _xmlPath
        Dim writeSuccess As Boolean = False

        If (p_path.Length > 0 AndAlso IO.File.Exists(p_path)) Then
            _xmlPath = p_path
        End If

        If (p_writeAction = eReadWriteAction.writeAll) Then
            writeSuccess = ReadWriteToFromFile(p_writeAction)
        End If

        If String.Compare(_xmlPath, p_path, True) = 0 Then _xmlPath = oldPath

        Return writeSuccess
    End Function
#End Region

#Region "Methods: Private - General"
    ''' <summary>
    ''' Maps the class properties to the node locations, including necssary type conversions.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeXMLNodePaths()
        Dim isReadOnly As Boolean = True

        Dim pathParent As String
        Dim pathChildFirst As String
        Dim pathChildSecond As String
        Dim pathChildThird As String
        Dim pathAssembled As String


        '= RegTest
        pathParent = "tabularOutput"
        pathAssembled = AssemblePath(pathParent)
        _path_saveOS = InitializeXMLNode(pathAssembled, "save", eReadWriteConversion.convertBooleanTrueFalse)

        pathAssembled = AssemblePathStub(pathParent)
        _path_fileName = InitializeXMLNode(pathAssembled & "filename")
        _path_v9Units = InitializeXMLNode(pathAssembled & "v9units", , eReadWriteConversion.convertBooleanTrueFalse)

        pathChildFirst = "tableSet"
        pathAssembled = AssemblePathStub(pathParent) & pathChildFirst
        _path_tableSetName = InitializeXMLNode(pathAssembled, "name")

        pathChildSecond = "options"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond)
        _path_showsSelectionOnly = InitializeXMLNode(pathAssembled & "showSelectionOnly", , eReadWriteConversion.convertBooleanTrueFalse)
        _path_showOnlyIfUsedInModel = InitializeXMLNode(pathAssembled & "showOnlyIfUsedInModel", , eReadWriteConversion.convertBooleanTrueFalse)
        _path_showAllFields = InitializeXMLNode(pathAssembled & "showAllFields", , eReadWriteConversion.convertBooleanTrueFalse)
        _path_multiStep = InitializeXMLNode(pathAssembled & "multiStep", , eReadWriteConversion.convertToEnum)
        _path_combineCaseStepFields = InitializeXMLNode(pathAssembled & "combineCaseStepFields", , eReadWriteConversion.convertBooleanTrueFalse)

        pathChildThird = "units"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond, pathChildThird)
        _path_forceUnit = InitializeXMLNode(pathAssembled & "forceUnit", , eReadWriteConversion.convertToEnum)
        _path_lengthUnit = InitializeXMLNode(pathAssembled & "lengthUnit", , eReadWriteConversion.convertToEnum)
        _path_temperatureUnit = InitializeXMLNode(pathAssembled & "temperatureUnit", , eReadWriteConversion.convertToEnum)

        'List nodes. Only specified to container node
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond)
        _path_groups = InitializeXMLNode(pathAssembled & "groups", , eReadWriteConversion.convertObservableCollectionFromList, , , "group")
        _path_loadPatterns = InitializeXMLNode(pathAssembled & "loadPatterns", , eReadWriteConversion.convertObservableCollectionFromList, , , "loadPattern")
        _path_loadCases = InitializeXMLNode(pathAssembled & "loadCases", , eReadWriteConversion.convertObservableCollectionFromList, , , "loadCase")
        _path_loadCombinations = InitializeXMLNode(pathAssembled & "loadCombinations", , eReadWriteConversion.convertObservableCollectionFromList, , , "loadCombination")

        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)
        _path_tables = InitializeXMLNode(pathAssembled & "tables", , eReadWriteConversion.convertObservableCollectionFromList, , , "table")
    End Sub

    ''' <summary>
    ''' Does various types of operations that read from or write to a file, depending on the action specified.
    ''' </summary>
    ''' <param name="p_fileAction">Action specified, of either a read or write nature.
    ''' There can be multiple actions for reading ro writing.</param>
    Private Function ReadWriteToFromFile(ByVal p_fileAction As eReadWriteAction) As Boolean
        Dim writeSuccess As Boolean = False
        Try
            OpenReadWriteState(p_fileAction)

            If _xmlReaderWriter.InitializeXML(_xmlPath) Then
                Select Case p_fileAction
                    Case eReadWriteAction.readAll
                        ReadWriteAllFromFile(_isReading)
                    Case eReadWriteAction.writeAll
                        ReadWriteAllFromFile(_isReading)
                End Select

                If Not _isReading Then _xmlReaderWriter.SaveXML(_xmlPath)
                _xmlReaderWriter.CloseXML()
                writeSuccess = True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            CloseReadWriteState()
        End Try

        Return writeSuccess
    End Function

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
        Try
            With _dataWriter
                'Tabular Output
                .ReadWriteAction(_outputSettings.saveOS, _path_saveOS, p_isReading)
                .ReadWriteAction(_outputSettings.tableFileName, _path_fileName, p_isReading)
                .ReadWriteAction(_outputSettings.v9Units, _path_v9Units, p_isReading)

                '= Table Set
                .ReadWriteAction(_outputSettings.tableSetName, _path_tableSetName, p_isReading)

                '== Options
                .ReadWriteAction(_outputSettings.showsSelectionOnly, _path_showsSelectionOnly, p_isReading)
                .ReadWriteAction(_outputSettings.showOnlyIfUsedInModel, _path_showOnlyIfUsedInModel, p_isReading)
                .ReadWriteAction(_outputSettings.showAllFields, _path_showAllFields, p_isReading)
                .ReadWriteAction(_outputSettings.multiStep, _path_multiStep, p_isReading)
                .ReadWriteAction(_outputSettings.combineCaseStepFields, _path_combineCaseStepFields, p_isReading)

                '=== Units
                .ReadWriteAction(_outputSettings.forceUnit, _path_forceUnit, p_isReading)
                .ReadWriteAction(_outputSettings.lengthUnit, _path_lengthUnit, p_isReading)
                .ReadWriteAction(_outputSettings.temperatureUnit, _path_temperatureUnit, p_isReading)

                '=== Groups, Load Patterns, Load Cases, Load Combinations
                'A temporary list is created that is missing the first blank entry, unless it is the only item in the list.
                Dim tempList As List(Of String)

                tempList = OSListClearedForWrite(_outputSettings.groups)
                .ReadWriteAction(tempList, _path_groups, p_isReading)
                tempList = New List(Of String)(TrimListOfEmptyItems(tempList))
                _outputSettings.groups = New List(Of String)(tempList)

                tempList = OSListClearedForWrite(_outputSettings.loadPatterns)
                .ReadWriteAction(tempList, _path_loadPatterns, p_isReading)
                tempList = New List(Of String)(TrimListOfEmptyItems(tempList))
                _outputSettings.loadPatterns = New List(Of String)(tempList)

                tempList = OSListClearedForWrite(_outputSettings.loadCases)
                .ReadWriteAction(tempList, _path_loadCases, p_isReading)
                tempList = New List(Of String)(TrimListOfEmptyItems(tempList))
                _outputSettings.loadCases = New List(Of String)(tempList)

                tempList = OSListClearedForWrite(_outputSettings.loadCombinations)
                .ReadWriteAction(tempList, _path_loadCombinations, p_isReading)
                tempList = New List(Of String)(TrimListOfEmptyItems(tempList))
                _outputSettings.loadCombinations = New List(Of String)(tempList)

                '== Tables
                tempList = OSListClearedForWrite(_outputSettings.tables)
                .ReadWriteAction(tempList, _path_tables, p_isReading)
                tempList = New List(Of String)(TrimListOfEmptyItems(tempList))
                _outputSettings.tables = New List(Of String)(tempList)
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the list to write out to the file. 
    ''' If no item is listed, at least one blank entry is kept for a placeholder node.
    ''' Otherwise, any blank entries are removed.
    ''' </summary>
    ''' <param name="p_originalList">Original list of items to check for writing to file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OSListClearedForWrite(ByVal p_originalList As IEnumerable(Of String)) As List(Of String)
        Dim tempList As New List(Of String)

        If p_originalList.Count = 0 Then
            'Maintain placeholder
            tempList = p_originalList.ToList
        Else
            'Remove placeholder
            For Each item As String In p_originalList
                If Not String.IsNullOrEmpty(item) Then tempList.Add(item)
            Next
        End If

        Return tempList
    End Function
#End Region
End Class
