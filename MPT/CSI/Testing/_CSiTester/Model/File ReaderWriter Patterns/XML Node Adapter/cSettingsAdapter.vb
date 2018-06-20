Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.XML
Imports MPT.XML.NodeAdapter
Imports MPT.XML.NodeAdapter.cNodeAssemblerXML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cSettings
Imports CSiTester.cPathSettings

''' <summary>
''' Handles all actions of reading/writing from/to files associated with the CSi Tester Settings class.
''' </summary>
''' <remarks></remarks>
Public Class cSettingsAdapter
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Enumerations"
    ''' <summary>
    ''' Read/write actions available for this class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eReadWriteAction
        readAll
        readProgramDefaults
        readFileTypes
        readDefaultAnalysisSettings
        readDefaultVersions
        writeAll
        writeFolderInitializationSettings
        writeRunRanCompareComparedLists
    End Enum
#End Region

#Region "Fields"
    Private _dataWriter As New cReadWriteXML()
    Private _xmlReaderWriter As New cXmlReadWrite()
    Private _isReading As Boolean

    Private _xmlPath As String
    Private _settings As cSettings
#End Region

#Region "Properties: XML File"
    '= RegTest
    Private _path_regTestName As cXMLNode
    Private _path_regTestDirectory As cXMLNode

    Private _path_versionDesignation As cXMLNode

    Private _path_programName As cXMLNode
    Private _path_programVersion As cXMLNode
    Private _path_programBuild As cXMLNode

    Private _path_initializeModelDestinationFolder As cXMLNode

    Private _path_testsToRun As cXMLNode
    Private _path_distributedTestSources As cXMLNode
    Private _path_copyModelOptions As cXMLNode

    Private _path_exampleValidationFile As cXMLNode
    Private _path_exampleValidationDirectory As cXMLNode
    Private _path_exampleUpdateFile As cXMLNode
    Private _path_exampleUpdateDirectory As cXMLNode

    '= CSiTester
    Private _path_testerDestinationDir As cXMLNode
    Private _path_csiTesterPath As cXMLNode
    Private _path_seedPath As cXMLNode

    Private _path_csiTesterlevel As cXMLNode
    Private _path_listTesterLevels As cXMLNode

    Private _path_userName As cXMLNode
    Private _path_userCompany As cXMLNode


    '= Programs
    Private _path_programs As cXMLNode
    Private _path_programVersions As cXMLNode
    Private _path_programReleaseDates As cXMLNode
    Private _path_programBuilds As cXMLNode

    '== Examples
    Private _path_examplePathsSaved As cXMLNode
    Private _path_examplesRunSaved As cXMLNode
    Private _path_examplesRanSaved As cXMLNode
    Private _path_examplesCompareSaved As cXMLNode
    Private _path_examplesComparedSaved As cXMLNode
    Private _path_modelIDIntegerLength As cXMLNode
    Private _path_modelSubIDIntegerLength As cXMLNode
    Private _path_statuseTypes As cXMLNode
    Private _path_documentationStatusTypes As cXMLNode

    '== General session settings
    Private _path_outputSettingsUsedAll As cXMLNode
    Private _path_tableExportFileExtensionAll As cXMLNode
    Private _path_analysisFilesPresent As cXMLNode


    '== Analysis Settings
    Private _path_solverSaved As cXMLNode
    Private _path_processSaved As cXMLNode
    Private _path_bitTypeSaved As cXMLNode

    '== Delete Files
    Private _path_deleteAnalysisFilesStatus As cXMLNode
    Private _path_deleteAnalysisFilesLogWarning As cXMLNode
    Private _path_deleteAnalysisFilesTables As cXMLNode
    Private _path_deleteAnalysisFilesModelText As cXMLNode
    Private _path_deleteAnalysisFilesAll As cXMLNode

    '== Main Gridview
    Private _path_allTabsSelectRun As cXMLNode
    Private _path_allTabsSelectCompare As cXMLNode
    Private _path_singleTab As cXMLNode

    'Defaults: Analysis Settings
    Private _path_solverDefault As cXMLNode
    Private _path_processDefault As cXMLNode
    Private _path_bitTypeDefault As cXMLNode

    'Defaults Program: Program & Models Paths
    Private _path_programPath As cXMLNode
    Private _path_modelSourcePath As cXMLNode
    Private _path_modelDestinationPath As cXMLNode

    'Defaults Program: Documentation Paths
    Private _path_documentsPathsAnalysis As cXMLNode
    Private _path_documentsPathsDesignSteelFrame As cXMLNode
    Private _path_documentsPathsDesignConcreteFrame As cXMLNode
    Private _path_documentsPathsDesignShearWall As cXMLNode
    Private _path_documentsPathsDesignCompositeBeam As cXMLNode
    Private _path_documentsPathsDesignCompositeColumn As cXMLNode
    Private _path_documentsPathsDesignSlab As cXMLNode


    'Defaults Program: Command Line Parameters: Analysis
    Private _path_commandRunAnalysis As cXMLNode
    Private _path_commandClose As cXMLNode
    Private _path_commandBatchRun As cXMLNode
    Private _path_commandProcessSame As cXMLNode
    Private _path_commandProcessSeparate As cXMLNode
    Private _path_commandProcessAuto As cXMLNode
    Private _path_commandSolverStandard As cXMLNode
    Private _path_commandSolverAdvanced As cXMLNode
    Private _path_commandSolverMultiThreaded As cXMLNode
    Private _path_commandBit32 As cXMLNode
    Private _path_commandBit64 As cXMLNode

    'Defaults Program: Command Line Parameters: Design
    Private _path_commandRunDesign As cXMLNode
    Private _path_commandRunDesignSteel As cXMLNode
    Private _path_commandRunDesignConcrete As cXMLNode
    Private _path_commandRunDesignWall As cXMLNode
    Private _path_commandRunDesignCompositeBeam As cXMLNode
    Private _path_commandRunDesignCompositeColumn As cXMLNode
    Private _path_commandRunDesignAluminum As cXMLNode
    Private _path_commandRunDesignColdFormed As cXMLNode
    Private _path_commandRunDesignSlab As cXMLNode

    'Defaults Program: Command Line Parameters: Misc
    Private _path_commandCreateInfoFile As cXMLNode
    Private _path_commandSave As cXMLNode

    'Defaults Program: File Types
    Private _path_fileTypes As cXMLNode

    'Defaults Program: Import Tags
    Private _path_importTags As cXMLNode
    Private _path_importTagVersions As cXMLNode

    'Defaults Program: Design Types
    Private _path_designTypeFiltered As cXMLNode

    '=? Examples
    'TO DO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    Private _path_exampleClassifications As cXMLNode

    'TO DO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    Private _path_exampleKeywords As cXMLNode
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
    ''' <param name="p_settings">Class to fill.</param>
    ''' <remarks></remarks>
    Friend Sub Fill(ByRef p_settings As cSettings)
        If p_settings Is Nothing Then Exit Sub

        _xmlPath = p_settings.xmlFile.path
        _settings = p_settings

        ReadWriteToFromFile(eReadWriteAction.readAll)

        ' Must occur after first read as node paths are dependent on properties read in
        UpdateProgramDefaults()
    End Sub

    ''' <summary>
    ''' Updates all program-specific default values.
    ''' Note: Must occur after first read as node paths are dependent on properties read in.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateProgramDefaults()
        InitializeProgramDefaultsXMLNodePaths()
        ReadWriteToFromFile(eReadWriteAction.readProgramDefaults)
    End Sub

    'TODO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_settings">Class to fill.</param>
    ''' <remarks></remarks>
    Friend Sub FillExamplesObjects(ByVal p_settings As cSettings)
        If p_settings Is Nothing Then Exit Sub

        _xmlPath = p_settings.xmlFile.path
        _settings = p_settings

        '=== XML Operations
        If _xmlReaderWriter.InitializeXML(_xmlPath) Then
            ReadWriteObjectsXML(True, eXMLSettingsObjectType.classification)
            ReadWriteObjectsXML(True, eXMLSettingsObjectType.keyword)

            _xmlReaderWriter.CloseXML()
        End If
        '=== End XML Operations
    End Sub

    ''' <summary>
    ''' Updates the class analysis settings properties from the associated file.
    ''' </summary>
    ''' <param name="p_settings">Class to fill.</param>
    ''' <remarks></remarks>
    Friend Sub FillAnalysisSettings(ByVal p_settings As cSettings)
        If p_settings Is Nothing Then Exit Sub

        _xmlPath = p_settings.xmlFile.path
        _settings = p_settings

        ReadWriteToFromFile(eReadWriteAction.readDefaultAnalysisSettings)
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
        If (p_path.Length > 0 AndAlso IO.File.Exists(p_path)) Then
            _xmlPath = p_path
        End If

        If (p_writeAction = eReadWriteAction.writeAll OrElse
            p_writeAction = eReadWriteAction.writeFolderInitializationSettings OrElse
            p_writeAction = eReadWriteAction.writeRunRanCompareComparedLists) Then

            ReadWriteToFromFile(p_writeAction)
        End If

        If String.Compare(_xmlPath, p_path, True) = 0 Then _xmlPath = oldPath
    End Sub

    ''' <summary>
    ''' Returns a list of all file types associated with the program name supplied.
    ''' </summary>
    ''' <param name="p_programName">Program name associated with the desired items.</param>
    ''' <param name="p_setSettings">True: Node path is updated to reflect the provided program name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetProgramFileTypes(ByVal p_programName As eCSiProgram,
                                                Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Return GetProgramDefaults(p_programName, eReadWriteAction.readFileTypes, _path_fileTypes, p_setSettings)
    End Function

    ''' <summary>
    ''' Returns a list of all version numbers associated with the program name supplied.
    ''' </summary>
    ''' <param name="p_programName">Program name associated with the desired items.</param>
    ''' <param name="p_setSettings">True: Node path is updated to reflect the provided program name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetProgramVersions(ByVal p_programName As eCSiProgram,
                                                Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Return GetProgramDefaults(p_programName, eReadWriteAction.readDefaultVersions, _path_programVersions, p_setSettings)
    End Function
#End Region

#Region "Methods: Private - General"
    ''' <summary>
    ''' Maps the class properties to the node locations, including necssary type conversions.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeXMLNodePaths()
        Dim isReadOnly As Boolean = True

        Dim ns As String = "/n:"

        Dim pathParent As String
        Dim pathChildFirst As String
        Dim pathAssembled As String


        '= RegTest
        pathParent = "regtest"
        pathAssembled = AssemblePathStub(pathParent)

        _path_regTestName = InitializeXMLNode(pathAssembled & "regtest_name")
        _path_regTestDirectory = InitializeXMLNode(pathAssembled & "regtest_directory")
        _path_programName = InitializeXMLNode(pathAssembled & "program_name", , eReadWriteConversion.convertToEnum)
        _path_versionDesignation = InitializeXMLNode(pathAssembled & "program_version_designation")
        _path_programVersion = InitializeXMLNode(pathAssembled & "program_version")
        _path_programBuild = InitializeXMLNode(pathAssembled & "program_build")
        _path_initializeModelDestinationFolder = InitializeXMLNode(pathAssembled & "models_run_dir", "initialize", eReadWriteConversion.convertBooleanYesNo)
        _path_exampleValidationFile = InitializeXMLNode(pathAssembled & "example_validation", "file_name")
        _path_exampleValidationDirectory = InitializeXMLNode(pathAssembled & "example_validation", "example_directory", eReadWriteConversion.convertPathStoredUnknown)
        _path_exampleUpdateFile = InitializeXMLNode(pathAssembled & "example_update_results", "file_name")
        _path_exampleUpdateDirectory = InitializeXMLNode(pathAssembled & "example_update_results", "example_directory", eReadWriteConversion.convertPathStoredUnknown)

        _path_testsToRun = InitializeXMLNode(pathAssembled & "tests_to_run", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_distributedTestSources = InitializeXMLNode(pathAssembled & "distrib_test_sources", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_copyModelOptions = InitializeXMLNode(pathAssembled & "copy_model_options", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)

        '= CSiTester
        pathParent = "csitester"
        pathAssembled = AssemblePathStub(pathParent)

        _path_testerDestinationDir = InitializeXMLNode(pathAssembled & "tester_destination_directory", , eReadWriteConversion.convertPathStoredUnknown, , DIR_TESTER_DESTINATION_DIR_DEFAULT)
        _path_csiTesterlevel = InitializeXMLNode(pathAssembled & "tester_level", , eReadWriteConversion.convertToEnum)
        _path_csiTesterPath = InitializeXMLNode(pathAssembled & "csitester_path", , eReadWriteConversion.convertPathStoredRelative)
        _path_seedPath = InitializeXMLNode(pathAssembled & "seed_files_path", , eReadWriteConversion.convertPathStoredRelative)

        _path_examplePathsSaved = InitializeXMLNode(pathAssembled & "example_paths" & ns & "example_path", , eReadWriteConversion.convertPathStoredRelativeFromList)
        _path_examplesRunSaved = InitializeXMLNode(pathAssembled & "run_examples" & ns & "run_example", , eReadWriteConversion.convertObservableCollectionFromUniqueList)
        _path_examplesRanSaved = InitializeXMLNode(pathAssembled & "ran_examples" & ns & "ran_example", , eReadWriteConversion.convertObservableCollectionFromUniqueList)
        _path_examplesCompareSaved = InitializeXMLNode(pathAssembled & "compare_examples" & ns & "compare_example", , eReadWriteConversion.convertObservableCollectionFromUniqueList)
        _path_examplesComparedSaved = InitializeXMLNode(pathAssembled & "compared_examples" & ns & "compared_example", , eReadWriteConversion.convertObservableCollectionFromUniqueList)

        '== Analysis Settings
        pathChildFirst = "analysis_settings"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_solverSaved = InitializeXMLNode(pathAssembled & "solver")
        _path_processSaved = InitializeXMLNode(pathAssembled & "process")
        _path_bitTypeSaved = InitializeXMLNode(pathAssembled & "bit_type")

        pathChildFirst = "delete_files"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_deleteAnalysisFilesStatus = InitializeXMLNode(pathAssembled & "delete_after_run", , eReadWriteConversion.convertBooleanYesNo)
        _path_deleteAnalysisFilesLogWarning = InitializeXMLNode(pathAssembled & "extra_log_warning", , eReadWriteConversion.convertBooleanYesNo)
        _path_deleteAnalysisFilesTables = InitializeXMLNode(pathAssembled & "extra_exported_tables", , eReadWriteConversion.convertBooleanYesNo)
        _path_deleteAnalysisFilesModelText = InitializeXMLNode(pathAssembled & "extra_model_text", , eReadWriteConversion.convertBooleanYesNo)
        _path_deleteAnalysisFilesAll = InitializeXMLNode(pathAssembled & "extra_all", , eReadWriteConversion.convertBooleanYesNo)

        pathAssembled = AssemblePathStub(pathParent)

        _path_analysisFilesPresent = InitializeXMLNode(pathAssembled & "analysis_files_present", , eReadWriteConversion.convertBooleanYesNo)

        '== Main Gridview
        pathAssembled = AssemblePathStub(pathParent)

        _path_allTabsSelectRun = InitializeXMLNode(pathAssembled & "gridview_main", "all_tabs_select_run", eReadWriteConversion.convertBooleanYesNo)
        _path_allTabsSelectCompare = InitializeXMLNode(pathAssembled & "gridview_main", "all_tabs_select_compare", eReadWriteConversion.convertBooleanYesNo)
        _path_singleTab = InitializeXMLNode(pathAssembled & "gridview_main", "single_tab", eReadWriteConversion.convertBooleanYesNo)

        '== Table Export Settings
        pathAssembled = AssemblePathStub(pathParent)

        _path_outputSettingsUsedAll = InitializeXMLNode(pathAssembled & "table_export_settings", "output_settings_used_all", eReadWriteConversion.convertBooleanYesNo)
        _path_tableExportFileExtensionAll = InitializeXMLNode(pathAssembled & "table_export_settings", "file_extension_all", eReadWriteConversion.convertBooleanYesNo)


        _path_userName = InitializeXMLNode(pathAssembled & "user_profile", "name")
        _path_userCompany = InitializeXMLNode(pathAssembled & "user_profile", "company")

        'Defaults
        '== Analysis Settings
        pathParent = "defaults_released"
        pathChildFirst = "analysis_settings"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_solverDefault = InitializeXMLNode(pathAssembled & "solver", , , isReadOnly)
        _path_processDefault = InitializeXMLNode(pathAssembled & "process", , , isReadOnly)
        _path_bitTypeDefault = InitializeXMLNode(pathAssembled & "bit_type", , , isReadOnly)

        '= Programs
        pathAssembled = AssemblePath("programs")
        _path_programs = InitializeXMLNode(pathAssembled, , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)

        '= Examples
        pathParent = "examples"

        '== Classifications
        pathAssembled = AssemblePathStub(pathParent)
        _path_exampleClassifications = InitializeXMLNode(pathAssembled & ns & "classifications")

        '== Keywords
        pathAssembled = AssemblePathStub(pathParent)
        _path_exampleKeywords = InitializeXMLNode(pathAssembled & ns & "keywords")


        '== ID Settings
        pathChildFirst = "id_settings"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_modelIDIntegerLength = InitializeXMLNode(pathAssembled & "id_integer_length", , eReadWriteConversion.convertInteger)
        _path_modelSubIDIntegerLength = InitializeXMLNode(pathAssembled & "sub_id_integer_length", , eReadWriteConversion.convertInteger)

        '== Attributes
        pathChildFirst = "attributes"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_statuseTypes = InitializeXMLNode(pathAssembled & "status", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_documentationStatusTypes = InitializeXMLNode(pathAssembled & "documentation_status", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)

        'Other
        pathAssembled = AssemblePath("tester_levels")
        _path_listTesterLevels = InitializeXMLNode(pathAssembled, , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
    End Sub

    ''' <summary>
    ''' Maps the class properties to the node locations associated with the current program, including necssary type conversions. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeProgramDefaultsXMLNodePaths()
        If _settings.programName = eCSiProgram.None Then Exit Sub

        Dim isReadOnly As Boolean = True
        Dim ns As String = "/n:"

        Dim pathParent As String
        Dim pathChildFirst As String
        Dim pathChildSecond As String
        Dim pathChildThird As String
        Dim pathAssembled As String

        'Defaults Program
        pathParent = "defaults_" & GetEnumDescription(_settings.programName)
        pathAssembled = AssemblePathStub(pathParent)

        '= Program & Models Paths
        _path_programPath = InitializeXMLNode(pathAssembled & "program_path", , , isReadOnly)
        _path_modelSourcePath = InitializeXMLNode(pathAssembled & "model_source_path", , , isReadOnly)
        _path_modelDestinationPath = InitializeXMLNode(pathAssembled & "model_destination_path", , , isReadOnly)

        '= Documentation
        pathChildFirst = "documentation_path"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_documentsPathsAnalysis = InitializeXMLNode(pathAssembled & "analysis", , , isReadOnly)
        _path_documentsPathsDesignSteelFrame = InitializeXMLNode(pathAssembled & "design_steel_frame", , , isReadOnly)
        _path_documentsPathsDesignConcreteFrame = InitializeXMLNode(pathAssembled & "design_concrete_frame", , , isReadOnly)
        _path_documentsPathsDesignShearWall = InitializeXMLNode(pathAssembled & "design_shear_wall", , , isReadOnly)
        _path_documentsPathsDesignCompositeBeam = InitializeXMLNode(pathAssembled & "design_composite_beam", , , isReadOnly)
        _path_documentsPathsDesignCompositeColumn = InitializeXMLNode(pathAssembled & "design_composite_column", , , isReadOnly)
        _path_documentsPathsDesignSlab = InitializeXMLNode(pathAssembled & "design_slab", , , isReadOnly)

        '= Command Line Parameters
        '== Analysis
        pathChildFirst = "command_line"
        pathChildSecond = "analysis"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond)

        _path_commandRunAnalysis = InitializeXMLNode(pathAssembled & "run_analysis", , , isReadOnly)

        '=== Process
        Dim nodeAttributes = New List(Of String)
        nodeAttributes.Add("key")
        nodeAttributes.Add("command")
        nodeAttributes.Add("name")

        pathChildThird = "process_options"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond, pathChildThird)

        _path_commandProcessSame = InitializeXMLNodeAttributes(pathAssembled & "process_same", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)
        _path_commandProcessSeparate = InitializeXMLNodeAttributes(pathAssembled & "process_separate", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)
        _path_commandProcessAuto = InitializeXMLNodeAttributes(pathAssembled & "process_auto", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)

        '=== Solver
        pathChildThird = "solver_options"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond, pathChildThird)

        _path_commandSolverStandard = InitializeXMLNodeAttributes(pathAssembled & "solver_standard", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)
        _path_commandSolverAdvanced = InitializeXMLNodeAttributes(pathAssembled & "solver_advanced", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)
        _path_commandSolverMultiThreaded = InitializeXMLNodeAttributes(pathAssembled & "solver_multithreaded", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)

        '=== Bit
        pathChildThird = "bit_options"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond, pathChildThird)

        _path_commandBit32 = InitializeXMLNodeAttributes(pathAssembled & "bit_32", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)
        _path_commandBit64 = InitializeXMLNodeAttributes(pathAssembled & "bit_64", nodeAttributes, eReadWriteConversion.convertObservableCollection, isReadOnly)

        '== Design
        pathChildSecond = "design"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond)
        _path_commandRunDesign = InitializeXMLNode(pathAssembled & "run_design", , , isReadOnly)

        pathChildThird = "design_types"
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst, pathChildSecond, pathChildThird)

        Dim nodePaths As New List(Of String)
        nodePaths.Add(pathAssembled & "design_steel")
        nodePaths.Add(pathAssembled & "design_concrete")
        nodePaths.Add(pathAssembled & "design_wall")
        nodePaths.Add(pathAssembled & "design_composite")
        nodePaths.Add(pathAssembled & "design_compositecolumn")
        nodePaths.Add(pathAssembled & "design_aluminum")
        nodePaths.Add(pathAssembled & "design_coldformed")
        nodePaths.Add(pathAssembled & "design_slab")

        _path_commandRunDesignSteel = InitializeXMLNode(nodePaths(0), , , isReadOnly)
        _path_commandRunDesignConcrete = InitializeXMLNode(nodePaths(1), , , isReadOnly)
        _path_commandRunDesignWall = InitializeXMLNode(nodePaths(2), , , isReadOnly)
        _path_commandRunDesignCompositeBeam = InitializeXMLNode(nodePaths(3), , , isReadOnly)
        _path_commandRunDesignCompositeColumn = InitializeXMLNode(nodePaths(4), , , isReadOnly)
        _path_commandRunDesignAluminum = InitializeXMLNode(nodePaths(5), , , isReadOnly)
        _path_commandRunDesignColdFormed = InitializeXMLNode(nodePaths(6), , , isReadOnly)
        _path_commandRunDesignSlab = InitializeXMLNode(nodePaths(7), , , isReadOnly)

        _path_designTypeFiltered = InitializeXMLNodes(nodePaths, "name", eReadWriteConversion.convertObservableCollection, isReadOnly)

        '== Misc
        pathAssembled = AssemblePathStub(pathParent, pathChildFirst)

        _path_commandCreateInfoFile = InitializeXMLNode(pathAssembled & "create_info_file", , , isReadOnly)
        _path_commandSave = InitializeXMLNode(pathAssembled & "save", , , isReadOnly)
        _path_commandClose = InitializeXMLNode(pathAssembled & "close", , , isReadOnly)
        _path_commandBatchRun = InitializeXMLNode(pathAssembled & "batch_run", , , isReadOnly)

        '=Other Program Data
        pathAssembled = AssemblePathStub(pathParent)
        _path_fileTypes = InitializeXMLNode(pathAssembled & "file_types", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_importTags = InitializeXMLNode(pathAssembled & "version_import_tags", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_importTagVersions = InitializeXMLNode(pathAssembled & "version_import_tags", "version_enacted", eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)

        _path_programVersions = InitializeXMLNode(pathAssembled & "releases", , eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_programReleaseDates = InitializeXMLNode(pathAssembled & "releases", "date", eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)
        _path_programBuilds = InitializeXMLNode(pathAssembled & "releases", "build", eReadWriteConversion.convertObservableCollectionFromList, isReadOnly)

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

            If _xmlReaderWriter.InitializeXML(_xmlPath) Then
                Select Case p_fileAction
                    Case eReadWriteAction.readAll
                        ReadWriteAllFromFile(_isReading)

                    Case eReadWriteAction.readProgramDefaults
                        ReadWriteProgramDefaults(_isReading)

                    Case eReadWriteAction.readFileTypes
                        p_list = ReadWriteFileTypes(_isReading, False, p_pathListFileTypes)

                    Case eReadWriteAction.readDefaultVersions
                        p_list = ReadWriteProgramVersions(_isReading, False, p_pathListFileTypes)

                    Case eReadWriteAction.readDefaultAnalysisSettings
                        ReadWriteAnalysisSettings(_isReading)

                    Case eReadWriteAction.writeAll
                        ReadWriteAllFromFile(_isReading)

                    Case eReadWriteAction.writeFolderInitializationSettings
                        ReadWriteFolderInitialization(_isReading)

                    Case eReadWriteAction.writeRunRanCompareComparedLists
                        ReadWriteRunRanCompareCompared(_isReading)
                End Select

                If Not _isReading Then _xmlReaderWriter.SaveXML(_xmlPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
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
            Case eReadWriteAction.readAll,
                eReadWriteAction.readProgramDefaults,
                eReadWriteAction.readFileTypes,
                eReadWriteAction.readDefaultVersions,
                eReadWriteAction.readDefaultAnalysisSettings

                _isReading = True
            Case eReadWriteAction.writeAll,
                eReadWriteAction.writeFolderInitializationSettings,
                eReadWriteAction.writeRunRanCompareComparedLists

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

#Region "Methods: Private - With Passed Object"
    ''' <summary>
    ''' Reads all data from a file, or writes all data to a file.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    Private Sub ReadWriteAllFromFile(ByVal p_isReading As Boolean)
        With _dataWriter
            '= RegTest
            .ReadWriteAction(_settings.regTestName, _path_regTestName, p_isReading)
            .ReadWriteAction(_settings.regTestDirectory, _path_regTestDirectory, p_isReading)
            .ReadWriteAction(_settings.programName, _path_programName, p_isReading)
            .ReadWriteAction(_settings.versionDesignation, _path_versionDesignation, p_isReading)
            .ReadWriteAction(_settings.programVersion, _path_programVersion, p_isReading)

            ReadWriteFolderInitialization(p_isReading)

            .ReadWriteAction(_settings.programBuild, _path_programBuild, p_isReading)
            .ReadWriteAction(_settings.testsToRun, _path_testsToRun, p_isReading)
            .ReadWriteAction(_settings.distributedTestSources, _path_distributedTestSources, p_isReading)
            .ReadWriteAction(_settings.copyModelOptions, _path_copyModelOptions, p_isReading)

            Dim exampleValidationFile As String = _settings.exampleValidationFile.fileNameWithExtension
            .ReadWriteAction(exampleValidationFile, _path_exampleValidationFile, p_isReading)
            Dim exampleValidationDirectory As String = _settings.exampleValidationFile.directory
            .ReadWriteAction(exampleValidationDirectory, _path_exampleValidationDirectory, p_isReading)
            _settings.exampleValidationFile.SetProperties(exampleValidationDirectory & "\" & exampleValidationFile, p_pathUnknown:=False)

            Dim exampleUpdateFile As String = _settings.exampleValidationFile.fileNameWithExtension
            .ReadWriteAction(exampleUpdateFile, _path_exampleUpdateFile, p_isReading)
            Dim exampleUpdateDirectory As String = _settings.exampleValidationFile.directory
            .ReadWriteAction(exampleUpdateDirectory, _path_exampleUpdateDirectory, p_isReading)
            _settings.exampleUpdateFile.SetProperties(exampleUpdateDirectory & "\" & exampleUpdateFile, p_pathUnknown:=False)

            '= CSiTester
            Dim path As String = _settings.testerDestination.path
            .ReadWriteAction(path, _path_testerDestinationDir, p_isReading)
            _settings.testerDestination.SetProperties(path)

            .ReadWriteAction(_settings.csiTesterlevel, _path_csiTesterlevel, p_isReading)
            If Not _settings.csiTesterlevel = eCSiTesterLevel.Published Then
                .ReadWriteAction(_settings.testerLevels, _path_listTesterLevels, p_isReading)
            End If

            path = _settings.csiTesterFile.path
            .ReadWriteAction(path, _path_csiTesterPath, p_isReading)
            _settings.csiTesterFile.SetProperties(path)

            path = _settings.seedDirectory.path
            .ReadWriteAction(path, _path_seedPath, p_isReading)
            _settings.seedDirectory.SetProperties(path)

            .ReadWriteAction(_settings.userName, _path_userName, p_isReading)
            .ReadWriteAction(_settings.userCompany, _path_userCompany, p_isReading)

            '=Programs
            .ReadWriteAction(_settings.programs, _path_programs, p_isReading)

            '== Examples
            .ReadWriteAction(_settings.examplePathsSaved, _path_examplePathsSaved, p_isReading)

            ReadWriteRunRanCompareCompared(p_isReading)

            .ReadWriteAction(_settings.modelSubIDIntegerLength, _path_modelSubIDIntegerLength, p_isReading)
            .ReadWriteAction(_settings.statusTypes, _path_statuseTypes, p_isReading)
            .ReadWriteAction(_settings.documentationStatusTypes, _path_documentationStatusTypes, p_isReading)

            '== General session settings
            .ReadWriteAction(_settings.outputSettingsUsedAll, _path_outputSettingsUsedAll, p_isReading)
            .ReadWriteAction(_settings.tableExportFileExtensionAll, _path_tableExportFileExtensionAll, p_isReading)
            .ReadWriteAction(_settings.analysisFilesPresent, _path_analysisFilesPresent, p_isReading)

            '== Analysis Settings
            ReadWriteAnalysisSettings(p_isReading)

            '== Delete Files
            .ReadWriteAction(_settings.deleteAnalysisFilesStatus, _path_deleteAnalysisFilesStatus, p_isReading)
            .ReadWriteAction(_settings.deleteAnalysisFilesLogWarning, _path_deleteAnalysisFilesLogWarning, p_isReading)
            .ReadWriteAction(_settings.deleteAnalysisFilesTables, _path_deleteAnalysisFilesTables, p_isReading)
            .ReadWriteAction(_settings.deleteAnalysisFilesModelText, _path_deleteAnalysisFilesModelText, p_isReading)
            .ReadWriteAction(_settings.deleteAnalysisFilesAll, _path_deleteAnalysisFilesAll, p_isReading)

            '== Main Gridview
            .ReadWriteAction(_settings.allTabsSelectRun, _path_allTabsSelectRun, p_isReading)
            .ReadWriteAction(_settings.allTabsSelectCompare, _path_allTabsSelectCompare, p_isReading)
            .ReadWriteAction(_settings.singleTab, _path_singleTab, p_isReading)



            'Defaults: Analysis Settings
            .ReadWriteAction(_settings.solverDefault, _path_solverDefault, p_isReading)
            .ReadWriteAction(_settings.processDefault, _path_processDefault, p_isReading)
            .ReadWriteAction(_settings.bitTypeDefault, _path_bitTypeDefault, p_isReading)

            'TO DO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
            '.ReadWriteAction(_exampleClassifications, _path_exampleClassifications, p_isReading)
            '.ReadWriteAction(_exampleKeywords, _path_exampleKeywords, p_isReading)
        End With
    End Sub

    ''' <summary>
    ''' Reads the selected program default data from a file, or writes all data to a file.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteProgramDefaults(ByVal p_isReading As Boolean)
        With _dataWriter
            'Defaults Program: Program & Models Paths
            .ReadWriteAction(_settings.programPathStub, _path_programPath, p_isReading)
            .ReadWriteAction(_settings.modelSourcePathStub, _path_programPath, p_isReading)
            .ReadWriteAction(_settings.modelDestinationPathStub, _path_modelDestinationPath, p_isReading)

            'Defaults Program: Documentation Paths
            .ReadWriteAction(_settings.documentsPathStubAnalysis, _path_documentsPathsAnalysis, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignSteelFrame, _path_documentsPathsDesignSteelFrame, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignConcreteFrame, _path_documentsPathsDesignConcreteFrame, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignShearWall, _path_documentsPathsDesignShearWall, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignCompositeBeam, _path_documentsPathsDesignCompositeBeam, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignCompositeColumn, _path_documentsPathsDesignCompositeColumn, p_isReading)
            .ReadWriteAction(_settings.documentsPathStubDesignSlab, _path_documentsPathsDesignSlab, p_isReading)

            'Defaults Program: Command Line Parameters: Analysis
            .ReadWriteAction(_settings.commandRunAnalysis, _path_commandRunAnalysis, p_isReading)
            .ReadWriteAction(_settings.commandClose, _path_commandClose, p_isReading)
            .ReadWriteAction(_settings.commandBatchRun, _path_commandBatchRun, p_isReading)
            .ReadWriteAction(_settings.commandProcessSame, _path_commandProcessSame, p_isReading)
            .ReadWriteAction(_settings.commandProcessSeparate, _path_commandProcessSeparate, p_isReading)
            .ReadWriteAction(_settings.commandProcessAuto, _path_commandProcessAuto, p_isReading)
            .ReadWriteAction(_settings.commandSolverStandard, _path_commandSolverStandard, p_isReading)
            .ReadWriteAction(_settings.commandSolverAdvanced, _path_commandSolverAdvanced, p_isReading)
            .ReadWriteAction(_settings.commandSolverMultiThreaded, _path_commandSolverMultiThreaded, p_isReading)
            .ReadWriteAction(_settings.commandBit32, _path_commandBit32, p_isReading)
            .ReadWriteAction(_settings.commandBit64, _path_commandBit64, p_isReading)

            'Defaults Program: Command Line Parameters: Design
            .ReadWriteAction(_settings.commandRunDesign, _path_commandRunDesign, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignSteel, _path_commandRunDesignSteel, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignConcrete, _path_commandRunDesignConcrete, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignWall, _path_commandRunDesignWall, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignCompositeBeam, _path_commandRunDesignCompositeBeam, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignCompositeColumn, _path_commandRunDesignCompositeColumn, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignAluminum, _path_commandRunDesignAluminum, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignColdFormed, _path_commandRunDesignColdFormed, p_isReading)
            .ReadWriteAction(_settings.commandRunDesignSlab, _path_commandRunDesignSlab, p_isReading)

            'Defaults Program: Command Line Parameters: Misc
            .ReadWriteAction(_settings.commandCreateInfoFile, _path_commandCreateInfoFile, p_isReading)
            .ReadWriteAction(_settings.commandSave, _path_commandSave, p_isReading)

            'Defaults Program: File Types
            ReadWriteFileTypes(p_isReading)

            '=Other Program Data
            .ReadWriteAction(_settings.importTags, _path_importTags, p_isReading)
            .ReadWriteAction(_settings.importTagVersions, _path_importTagVersions, p_isReading)
            .ReadWriteAction(_settings.designTypeFiltered, _path_designTypeFiltered, p_isReading)

            'Program Versions
            ReadWriteProgramVersions(p_isReading)
            .ReadWriteAction(_settings.programReleaseDates, _path_programReleaseDates, p_isReading)
            .ReadWriteAction(_settings.programBuilds, _path_programBuilds, p_isReading)
        End With
    End Sub

    ''' <summary>
    ''' Reads from a file all analysis settings used in a prior session, or writes this data to a file.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteAnalysisSettings(ByVal p_isReading As Boolean)
        With _dataWriter
            .ReadWriteAction(_settings.solverSaved, _path_solverSaved, p_isReading)
            .ReadWriteAction(_settings.processSaved, _path_processSaved, p_isReading)
            .ReadWriteAction(_settings.bitTypeSaved, _path_bitTypeSaved, p_isReading)
        End With
    End Sub

    ''' <summary>
    ''' Returns a list of all file types associated with the current program.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <param name="p_updateClass">True: The class properties list will be updated.</param>
    ''' <param name="p_pathListFileTypes">Node path associated with the default item to be retrieved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadWriteFileTypes(ByVal p_isReading As Boolean,
                                        Optional ByVal p_updateClass As Boolean = True,
                                        Optional ByVal p_pathListFileTypes As cXMLNode = Nothing) As ObservableCollection(Of String)

        Return ReadWriteList(p_isReading, _settings.fileTypes, _path_fileTypes, p_updateClass, p_pathListFileTypes)
    End Function

    ''' <summary>
    ''' Returns a list of all program versions associated with the current program.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <param name="p_updateClass">True: The class properties list will be updated.</param>
    ''' <param name="p_pathListFileTypes">NNode path associated with the default item to be retrieved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadWriteProgramVersions(ByVal p_isReading As Boolean,
                                        Optional ByVal p_updateClass As Boolean = True,
                                        Optional ByVal p_pathListFileTypes As cXMLNode = Nothing) As ObservableCollection(Of String)

        Return ReadWriteList(p_isReading, _settings.programVersions, _path_programVersions, p_updateClass, p_pathListFileTypes)
    End Function

    ''' <summary>
    ''' Reads or writes a list from or to a file. 
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <param name="p_privateVariable">Class property list. </param>
    ''' <param name="p_nodePath">Original node path associated with the default item to be retrieved.</param>
    ''' <param name="p_updateClass">True: The class properties list will be updated.</param>
    ''' <param name="p_pathListFileTypes">Node path associated with the default item to be retrieved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadWriteList(ByVal p_isReading As Boolean,
                                   ByRef p_privateVariable As ObservableCollection(Of String),
                                   ByRef p_nodePath As cXMLNode,
                                    Optional ByVal p_updateClass As Boolean = True,
                                    Optional ByVal p_pathListFileTypes As cXMLNode = Nothing) As ObservableCollection(Of String)
        Dim fileTypes As New ObservableCollection(Of String)

        If Not p_isReading Then fileTypes = p_privateVariable
        If p_pathListFileTypes Is Nothing Then p_pathListFileTypes = p_nodePath

        _dataWriter.ReadWriteAction(fileTypes, p_pathListFileTypes, p_isReading)

        If (p_updateClass AndAlso Not fileTypes.Count = 0) Then p_privateVariable = fileTypes

        Return fileTypes
    End Function

    ''' <summary>
    ''' Reads or writes the values associated with examples being set to be run/compared, and examples that have been run/compared.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteRunRanCompareCompared(ByVal p_isReading As Boolean)
        With _dataWriter
            .ReadWriteAction(_settings.examplesRunSaved, _path_examplesRunSaved, p_isReading)
            .ReadWriteAction(_settings.examplesRanSaved, _path_examplesRanSaved, p_isReading)
            .ReadWriteAction(_settings.examplesCompareSaved, _path_examplesCompareSaved, p_isReading)
            .ReadWriteAction(_settings.examplesComparedSaved, _path_examplesComparedSaved, p_isReading)
        End With
    End Sub

    ''' <summary>
    ''' Reads or writes data related to initializing the program with paths saved from a prior session.
    ''' </summary>
    ''' <param name="p_isReading">True: Data will be read from the file. Else, it will be written to the file.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteFolderInitialization(ByVal p_isReading As Boolean)
        _dataWriter.ReadWriteAction(_settings.initializeModelDestinationFolder, _path_initializeModelDestinationFolder, p_isReading)
    End Sub


    'TODO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeXMLNodePathsClassificationsKeywords()

    End Sub

    'TODO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    ''' <summary>
    ''' Reads and writes 'object' hierarchy items to/from the settings XML file.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ''' <param name="myObjectType">Which object type is being read from the settings XML file.</param>
    ''' <remarks></remarks>
    Private Sub ReadWriteObjectsXML(ByVal read As Boolean, ByVal myObjectType As eXMLSettingsObjectType)
        Try
            Dim pathNode As String = ""

            If read Then
                Select Case myObjectType
                    Case eXMLSettingsObjectType.classification : pathNode = "//n:settings/n:examples/n:classifications"
                    Case eXMLSettingsObjectType.keyword : pathNode = "//n:settings/n:examples/n:keywords"
                End Select

                cXMLCSi.ReadXmlSettingsObject(pathNode, myObjectType)
            Else
                'TODO: 'Write' functions
                _xmlReaderWriter.SaveXML(_xmlPath)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"

    ''' <summary>
    ''' Returns a list of values of the specified program default item.
    ''' </summary>
    ''' <param name="p_programName">Program name associated with the desired defaults.</param>
    ''' <param name="p_defaultValue">Action as to what default item should be retrieved.</param>
    ''' <param name="p_nodePath">Node path associated with the default item to be retrieved.</param>
    ''' <param name="p_setSettings">True: Node path is updated to reflect the provided program name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProgramDefaults(ByVal p_programName As eCSiProgram,
                            ByVal p_defaultValue As eReadWriteAction,
                            ByRef p_nodePath As cXMLNode,
                            Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)
        Dim programNew As String = GetEnumDescription(p_programName)
        Dim programOld As String = GetEnumDescription(_settings.programName)

        Try
            'Create copy of existing node and swap the current program name with the specified program name
            Dim pathListTemp As cXMLNode = CType(p_nodePath.Clone, cXMLNode)
            pathListTemp.xmlPath = ReplaceStringInName(pathListTemp.xmlPath, programOld, programNew)

            ReadWriteToFromFile(p_defaultValue, tempList, pathListTemp)
            If (p_setSettings AndAlso tempList.Count > 0) Then
                p_nodePath = pathListTemp
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            GetProgramDefaults = tempList
        End Try
    End Function
#End Region

End Class
