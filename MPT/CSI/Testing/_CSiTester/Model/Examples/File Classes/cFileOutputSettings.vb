Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data
Imports System.IO

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter.cXmlReadWrite
Imports MPT.XML.NodeAdapter.cNodeAssemblerXML

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cPathModelControl
Imports CSiTester.cPathOutputSettings
Imports CSiTester.cFileAttachment
Imports CSiTester.cProgramControl

Public Class cFileOutputSettings
    Inherits cMCFile

#Region "Enumerations"
    ''' <summary>
    ''' Reading/writing actions to be taken with the outputSettings file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eReadWriteAction
        readAll
        writeAll
    End Enum

    ''' <summary>
    ''' Specifies whether the outputSettings XML file arrangement should be activated, deactivated, or left in the current state for a given example.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eOutputSettingsActivation
        AsIs = 1
        Activate = 2
        Deactivate = 3
    End Enum
#End Region

#Region "Constants"
    Friend Const TABLE_SET_NAME As String = "DB01"
#End Region

#Region "Variables"
    Private _adapter As New cOutputSettingsAdapter
    Private _mcModel As cMCModel
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' True: The outputSettings parameters are to be saved in the targeted model file.
    ''' False: The outputSettings file will overwrite the current run of the model file, but the overwrites will not be saved to the model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property saveOS As Boolean
#End Region

#Region "Properties: Friend XML"
    Private _tableFileName As String
    ''' <summary>
    ''' File name of the table to export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property tableFileName As String
        Set(value As String)
            _tableFileName = GetPathFileName(value)
        End Set
        Get
            Return _tableFileName
        End Get
    End Property

    ''' <summary>
    ''' If true, then the default exported units are from v9.7.4 and should be maintained in recent versions of ETABS.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property v9Units As Boolean

    ''' <summary>
    ''' Name of the table set associated with the settings defined.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property tableSetName As String

    ''' <summary>
    ''' If true, only the selected elements in the program are included in exported tables.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property showsSelectionOnly As Boolean

    ''' <summary>
    ''' If true, then tables are only available for export if they are actually used in the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property showOnlyIfUsedInModel As Boolean

    ''' <summary>
    ''' If true, then all table fields are shown, even if not used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property showAllFields As Boolean

    ''' <summary>
    ''' Force unit to use in the table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property forceUnit As eForceUnit

    ''' <summary>
    ''' Length unit to use in the table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property lengthUnit As eLengthUnit

    ''' <summary>
    ''' Temperature unit to use in the table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property temperatureUnit As eTemperatureUnit

    ''' <summary>
    ''' Groups to be used in selecting members for table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property groups As New List(Of String)

    ''' <summary>
    ''' Load patterns to include for table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property loadPatterns As New List(Of String)

    ''' <summary>
    ''' Load cases to include for table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property loadCases As New List(Of String)

    ''' <summary>
    ''' Load combinations to include for table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property loadCombinations As New List(Of String)

    ''' <summary>
    ''' Table names to include for table export.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property tables As New List(Of String)

    ''' <summary>
    ''' The type of step recorded for output, such as none, last, max/min, multi-step, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property multiStep As eStepType

    ''' <summary>
    ''' If true, the step type and step number fields are combined into a single field. 
    ''' If false, they are displayed in separate field.s
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property combineCaseStepFields As Boolean
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Creates an object derived from an existing XML file based on the path provided and binds the path and name properties to the supplied model control file.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <param name="p_pathOutputSettingsFile">Path to an existing outputSettings file.</param>
    ''' <param name="p_initializeFromSeed">True: File is initialized by first reading the seed file. Overwrites any provided file path.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_pathOutputSettingsFile As String = "",
                   Optional ByVal p_initializeFromSeed As Boolean = False)
        MyBase.New()
        If p_initializeFromSeed Then p_pathOutputSettingsFile = seedPathMC

        InitializeObject(p_bindTo, p_pathOutputSettingsFile)
    End Sub

    Protected Overrides Sub InitializeFile(Optional ByVal p_bindTo As cMCModel = Nothing,
                                            Optional ByVal p_pathFile As String = "")
        'Not used. Instead, used 'InitializeObject'
    End Sub
    ''' <summary>
    ''' Initializes the object based on the provided input. 
    ''' The default case is to use the seed path.
    ''' </summary>
    ''' <param name="p_bindTo">Model Control object that this object will reference.</param>
    ''' <param name="p_pathOutputSettingsFile">Path to an existing file to initialize from.</param>
    ''' <remarks></remarks>
    Private Sub InitializeObject(Optional ByVal p_bindTo As cMCModel = Nothing,
                                 Optional ByVal p_pathOutputSettingsFile As String = "")
        Try
            'Set path source
            _pathDestination = New cPathOutputSettings(p_bindTo, p_pathOutputSettingsFile)
            CompleteInitialization()

            ' Set path destination to nothing, since this is handled with the generated attachments objects
            _pathDestination = New cPathOutputSettings(p_bindTo, p_setToNothing:=True)

            _mcModel = p_bindTo
            tableSetName = TABLE_SET_NAME

            ' Set properties from file contents
            If StringsMatch(pathDestination.path, seedPathMC) Then
                FillFromSeedFile()
            Else
                FillFromFile(pathDestination.path)
            End If
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    Friend Overloads Overrides Function Clone() As Object
        Return Clone(Nothing)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">If specified, the model control reference will be switched to the one provided. 
    ''' Otherwise, the original reference is kept.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Overrides Function Clone(ByVal p_bindTo As cMCModel) As Object
        Try
            Dim myClone As cFileOutputSettings = DirectCast(MyBase.Clone(p_bindTo), cFileOutputSettings)
            With myClone
                ._pathDestination = DirectCast(PathOutputSettings().Clone(p_bindTo), cPathOutputSettings)
                If p_bindTo IsNot Nothing Then
                    ._mcModel = p_bindTo
                Else
                    ._mcModel = _mcModel
                End If

                'XML properties
                .saveOS = saveOS
                .tableFileName = tableFileName
                .v9Units = v9Units

                .tableSetName = tableSetName

                .showsSelectionOnly = showsSelectionOnly
                .showOnlyIfUsedInModel = showOnlyIfUsedInModel
                .showAllFields = showAllFields

                .forceUnit = forceUnit
                .lengthUnit = lengthUnit
                .temperatureUnit = temperatureUnit

                For Each item As String In loadPatterns
                    .loadPatterns.Add(item)
                Next

                For Each item As String In loadCases
                    .loadCases.Add(item)
                Next

                For Each item As String In loadCombinations
                    .loadCombinations.Add(item)
                Next

                For Each item As String In tables
                    .tables.Add(item)
                Next

                .multiStep = multiStep
                .combineCaseStepFields = combineCaseStepFields
            End With

            Return myClone
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
            Return Nothing
        End Try
    End Function
    Protected Overrides Function Create() As cMCFile
        Return New cFileOutputSettings()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cFileOutputSettings) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cFileOutputSettings = TryCast(p_object, cFileOutputSettings)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(comparedObject) Then Return False

            Dim pathCast As cPathOutputSettings = PathOutputSettings()
            Dim pathCastCompare As cPathOutputSettings = .PathOutputSettings()
            If Not pathCastCompare.Equals(pathCast) Then Return False

            'XML properties
            If Not .saveOS = saveOS Then Return False
            If Not .tableFileName = tableFileName Then Return False
            If Not .v9Units = v9Units Then Return False

            If Not .tableSetName = tableSetName Then Return False

            If Not .showsSelectionOnly = showsSelectionOnly Then Return False
            If Not .showOnlyIfUsedInModel = showOnlyIfUsedInModel Then Return False
            If Not .showAllFields = showAllFields Then Return False

            If Not .forceUnit = forceUnit Then Return False
            If Not .lengthUnit = lengthUnit Then Return False
            If Not .temperatureUnit = temperatureUnit Then Return False

            For Each itemOuter As String In .loadPatterns
                isMatch = False
                For Each itemInner As String In loadPatterns
                    If itemOuter = itemInner Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            For Each itemOuter As String In .loadCases
                isMatch = False
                For Each itemInner As String In loadCases
                    If itemOuter = itemInner Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            For Each itemOuter As String In .loadCombinations
                isMatch = False
                For Each itemInner As String In loadCombinations
                    If itemOuter = itemInner Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            For Each itemOuter As String In .tables
                isMatch = False
                For Each itemInner As String In tables
                    If itemOuter = itemInner Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            If Not .multiStep = multiStep Then Return False
            If Not .combineCaseStepFields = combineCaseStepFields Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Binds the object to the state of the supplied model control object.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub Bind(ByVal p_bindTo As cMCModel)
        PathOutputSettings.SetMCModel(p_bindTo)
        _mcModel = p_bindTo

        _fileManager.SetDestinationPath(pathDestination)
    End Sub

    ''' <summary>
    ''' Returns the path object in the desired downcast class. 
    ''' If the path object was not upcast from this class, 'Nothing' is returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PathOutputSettings() As cPathOutputSettings
        Try
            Return DirectCast(_pathDestination, cPathOutputSettings)
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
            Return Nothing
        End Try
    End Function

    ' Fill Class Properties
    ''' <summary>
    ''' Populates the entire outputsettings object based on the seed file, if it exists.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FillFromSeedFile()
        If IO.File.Exists(seedPathMC) Then
            pathDestination.SetProperties(seedPathMC)
            _adapter.Fill(Me)
        End If
    End Sub

    ''' <summary>
    ''' Populates the entire outputsettings object based on the provided file path to an existing output settings file.
    ''' </summary>
    ''' <param name="p_path">Path to an existing XML file to turn into an object.</param>
    ''' <remarks></remarks>
    Friend Sub FillFromFile(ByVal p_path As String)
        Try
            If IO.File.Exists(p_path) Then
                pathDestination.SetProperties(p_path)
                _adapter.Fill(Me)
            End If
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Populates the entire outputsettings object based on the provided table file path.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the table file to read.</param>
    ''' <remarks></remarks>
    Friend Sub FillFromTable(ByVal p_dataSource As String)
        If Not IO.File.Exists(p_dataSource) Then Exit Sub
        Dim osModelControlFile As cMCModel = PathOutputSettings.modelControlFile

        SetTableValues(p_dataSource)
        SetUnits(osModelControlFile.programControl)
    End Sub

    ''' <summary>
    ''' Populates the entire outputsettings object based on the provided table file path and model object.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FillFromResults()
        Dim osModelControlFile As cMCModel = PathOutputSettings.modelControlFile

        Dim dataSourceFilePath As cPathDataSource = osModelControlFile.dataSource.PathExportedTable

        ' If tableFileName is not set, table file name is taken as the assumed one, since the actual path used might be of a differing file
        tableFileName = dataSourceFilePath.AssumedDatabaseFileNameWithExtension(tableFileName)

        SetTableValues(osModelControlFile.results)
        SetUnits(osModelControlFile.programControl)
    End Sub

    ''' <summary>
    ''' Sets various output setting file parameters based on the specified exported tables file for a given model.
    ''' </summary>
    ''' <param name="p_dataSource">Path to the file that is the exported tables file.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetTableValues(ByVal p_dataSource As String)
        If (String.IsNullOrEmpty(p_dataSource)) Then Exit Sub
        Dim dtController As New cDataTableController
        Dim tableSets As List(Of DataTable) = dtController.DataTablesListFromFile(p_dataSource)
        If tableSets Is Nothing Then Exit Sub

        tableFileName = GetPathFileName(p_dataSource)

        Dim result As New cMCResult

        For Each tableItem As DataTable In tableSets
            tables = AddIfNew(tables, tableItem.TableName, p_placeFirst:=False).ToList

            multiStep = result.tableFieldProperties.GetStepType(multiStep, tableItem)

            If tableItem.Rows.Count > 0 Then
                Dim colIndex As Integer = 0
                Dim loadType As eLoadType = result.tableFieldProperties.GetLoadType(tableItem.Rows(0), colIndex)
                If Not loadType = eLoadType.none Then

                End If
                For Each row As DataRow In tableItem.Rows
                    Select Case result.tableFieldProperties.GetLoadType(row, colIndex)
                        Case eLoadType.loadPattern
                            loadPatterns = AddIfNew(loadPatterns, result.tableFieldProperties.LoadPatternName(row, colIndex)).ToList
                        Case eLoadType.loadCase
                            loadCases = AddIfNew(loadCases, result.tableFieldProperties.LoadCaseName(row, colIndex)).ToList
                            loadPatterns = AddIfNew(loadPatterns, result.tableFieldProperties.LoadPatternName(row, colIndex)).ToList
                        Case eLoadType.loadCombination
                            loadCombinations = AddIfNew(loadCombinations, result.tableFieldProperties.LoadCombinationName(row, colIndex)).ToList
                    End Select
                Next
            End If
        Next
    End Sub

    ' Save Class
    ''' <summary>
    ''' Saves copies of the new file(s) in the appropriate locations relative to the model file.
    ''' </summary>
    ''' <param name="p_paths">File paths to which the outputSettings object is to be saved. 
    ''' Can be multiple file paths as there might be an attachment and a supporting file to keep in sync.</param>
    ''' <remarks></remarks>
    Friend Sub SaveToAllFiles(ByVal p_paths As List(Of String))
        Dim errorMessage As String = ""

        _adapter.Bind(Me)

        For Each path As String In p_paths
            Try
                WriteToFile(path)
            Catch ex As IOException
                errorMessage &= ex.Message & Environment.NewLine & Environment.NewLine
            End Try
        Next

        If Not String.IsNullOrEmpty(errorMessage) Then
            Throw New IOException(errorMessage)
        End If
    End Sub

    ''' <summary>
    ''' Writes the object XML properties to the file at the path specified.
    ''' </summary>
    ''' <param name="p_path">Path of the file to write to.</param>
    ''' <remarks></remarks>
    Friend Sub WriteToFile(ByVal p_path As String)
        If Not IO.File.Exists(p_path) Then Throw New IOException("File to save does not exist: " & Environment.NewLine & p_path)

        If Not _adapter.Write(cOutputSettingsAdapter.eReadWriteAction.writeAll, p_path) Then
            'If Not _adapter.Write(cOutputSettingsAdapter.eReadWriteAction.writeAll) Then
            Throw New IOException("Unable to write to file: " & p_path)
        End If
    End Sub

    ' Create attachment objects
    ''' <summary>
    ''' Sets properties related to being a supporting file, based on the status in the model object provided.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSupportingFile()
        With PathOutputSettings.modelFile
            If (.importedModel AndAlso
                Not String.IsNullOrEmpty(.importTag)) Then

                v9Units = True
            End If
        End With
    End Sub

    ''' <summary>
    ''' Adds the outputSettings object attachments reference.
    ''' </summary>
    ''' <param name="p_supportingFileAttachment">True: The attachment will be created for a supporting file. False: The attachment will be a standard attachment.</param>
    ''' <remarks></remarks>
    Friend Function CreateAttachment(Optional p_supportingFileAttachment As Boolean = False) As cFileAttachment
        Dim osModelControlFile As cMCModel = PathOutputSettings.modelControlFile
        Dim directory As String = osModelControlFile.mcFile.pathDestination.directory

        Dim osAttachment As New cFileAttachment(_fileManager.fileSource.path, p_attachmentType:=eAttachmentDirectoryType.attachmentOutputSettings)
        With osAttachment
            If p_supportingFileAttachment Then
                .title = TAG_ATTACHMENT_SUPPORTING_FILE & pathDestination.fileName
            Else
                .title = TAG_ATTACHMENT_TABLE_SET_FILE & pathDestination.fileName
            End If

            Select Case osModelControlFile.folderStructure
                Case eFolderStructure.Database
                    If p_supportingFileAttachment Then
                        .PathAttachment.SetProperties(directory & "\" & DIR_NAME_MODELS_DEFAULT & "\" & pathDestination.fileNameWithExtension)
                    Else
                        .PathAttachment.SetProperties(directory & "\" & DIR_NAME_ATTACHMENTS_DEFAULT & "\" & pathDestination.fileNameWithExtension)
                    End If
                Case eFolderStructure.Flattened
                    If p_supportingFileAttachment Then
                        .PathAttachment.SetProperties(directory & "\" & pathDestination.fileNameWithExtension)
                    Else
                        .PathAttachment.SetProperties(directory & "\" & DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT & "\" & pathDestination.fileNameWithExtension)
                    End If
                Case Else
                    'No action will be taken, with no resulting file. Either of the above two options must be selected for a save operation to occur
            End Select

            .fileAction = cFileManager.eFileAction.copySourceToDestination
        End With

        Return osAttachment
    End Function

    ''' <summary>
    ''' Adds the outputSettings object supporting file reference, if applicable.
    ''' </summary>
    ''' <param name="p_supportingFileAttachment">False: This will only be added if V9 units are specified to be used, as this indicates being used with an old version.</param>
    ''' <remarks></remarks>
    Friend Function CreateAttachmentAsSupportingFile(Optional p_supportingFileAttachment As Boolean = False) As cFileAttachment
        'Determine if outputSettings is a supporting file if it is not user-specified
        If v9Units Then p_supportingFileAttachment = True

        'Only create an attachment if it is a supporting file.
        If p_supportingFileAttachment Then
            Return CreateAttachment(p_supportingFileAttachment)
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path object provided is of a matching type.
    ''' </summary>
    ''' <param name="p_path">Path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function isMatchingPathType(ByVal p_path As cPath) As Boolean
        If TypeOf p_path Is cPathOutputSettings Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Sets various output setting file parameters based on the specified exported tables file for a given model.
    ''' </summary>
    ''' <param name="p_results">Collection of results to use for determining the table values used.</param>
    ''' <remarks></remarks>
    Private Overloads Sub SetTableValues(ByVal p_results As cMCResults)
        If p_results Is Nothing Then Exit Sub

        Dim resultTables As List(Of String) = p_results.TablesUsed
        Dim resultLoads As List(Of String) = p_results.LoadCaseCombosUsed

        loadCases = resultLoads
        loadCombinations = resultLoads
        tables = resultTables
    End Sub
    ''' <summary>
    ''' Sets various output setting file parameters based on the content of the queries established.
    ''' </summary>
    ''' <param name="p_model">The model control object to base the output settings file on.</param>
    ''' <remarks></remarks>
    Private Overloads Sub SetTableValues(ByVal p_model As cMCModel)
        tableFileName = p_model.dataSource.pathDestination.fileNameWithExtension

        For Each result As cMCResult In p_model.results
            tables = AddIfNew(tables, result.tableName, False).ToList

            Dim multiStepTemp As eStepType = result.GetStepType()
            If Not multiStepTemp = eStepType.none Then multiStep = multiStepTemp

            Select Case result.GetLoadType()
                Case eLoadType.loadPattern
                    loadPatterns = AddIfNew(loadPatterns, result.LoadPatternName()).ToList
                Case eLoadType.loadCase
                    loadCases = AddIfNew(loadCases, result.LoadCaseName()).ToList
                Case eLoadType.loadCombination
                    loadCombinations = AddIfNew(loadCombinations, result.LoadCombinationName()).ToList
            End Select
        Next
    End Sub


    ''' <summary>
    ''' Sets the units of the class based on data present in the program control class.
    ''' </summary>
    ''' <param name="p_programControl">Program control class that contains unit data.</param>
    ''' <remarks></remarks>
    Private Sub SetUnits(ByVal p_programControl As cProgramControl)
        With p_programControl
            If StringsMatch(.unitsLength, "inch") Then
                lengthUnit = eLengthUnit.inches
            Else
                lengthUnit = ConvertStringToEnumByDescription(Of eLengthUnit)(.unitsLength)
            End If

            forceUnit = ConvertStringToEnumByDescription(Of eForceUnit)(.unitsForce)
            temperatureUnit = ConvertStringToEnumByDescription(Of eTemperatureUnit)(.unitsTemperature)
        End With
    End Sub

#End Region

End Class
