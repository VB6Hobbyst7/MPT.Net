Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter
Imports MPT.XML.NodeAdapter.cNodeAssemblerXML

Imports CSiTester.cPathSettings

''' <summary>
''' Settings class handles program settings and the accompanying CSiTesterSettings.xml file.
''' The class serves both to hold the xml file in memory, and read/write from/to the file.
''' The class also does settings-related functions and coordination within the program
''' </summary>
''' <remarks></remarks>
Public Class cSettings
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

    Friend Const CLASS_STRING As String = "cSettings"

#Region "Enumerations"
    ''' <summary>
    ''' Classification of the XML node type used in the settings file for handling read/write actions.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eXMLSettingsObjectType
        classification = 0
        keyword = 1
    End Enum
#End Region

#Region "Fields"
    Private _allTabsSelectRunXML As String
    Private _allTabsSelectCompareXML As String
    Private _singleTabXML As String

    Private _adapter As New cSettingsAdapter()
#End Region

#Region "Constants"
    '=== Files
    Friend Const INI_INSTALLATION As String = "CSiTester.ini"
    Friend Const INI_INTERNAL_KEY As String = "internal_key.ini"
    Friend Const FILENAME_BATCH_DELETE As String = "deleteAnalysisFiles.bat"

    '=== Destination Directories
    Friend Const DIR_NAME_MODELS_DESTINATION As String = "Models"
    Friend Const DIR_NAME_RESULTS_DESTINATION As String = "Results"
    Friend Const DIR_NAME_RESULTS_DESTINATION_SUFFIX As String = "_Results"

    Private _outputSettingsVersionSession As String
    ''' <summary>
    ''' Version import tag that must be included in the filename for it to be activated by the imported model.
    ''' First populated by default setting.
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property outputSettingsVersionSession As String
        Get
            Return _outputSettingsVersionSession
        End Get
    End Property
#End Region

#Region "Properties: Public"
    ''' <summary>
    ''' Path to the CSiSettings.xml file that is to be read to and written from.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlFile As New cPath
    ''' <summary>
    ''' Path to the CSiSettings.xml file that is installed with the corresponding analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlFileInstalled As New cPath

    ''' <summary>
    ''' Indicates if the relative path between CSiTester and the specified analysis program has changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programLocationChanged As Boolean

    ''' <summary>
    ''' List of the processes available for specifying in the analysis options in a given analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisProcesses As New ObservableCollection(Of String)
    ''' <summary>
    ''' List of the solver options available for specifying in the analysis options in a given analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisSolver As New ObservableCollection(Of String)
    ''' <summary>
    ''' List of the bit types for specifying in the analysis solver in a given analysis program. 32/64 bit modes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisBitType As New ObservableCollection(Of String)

    ''' <summary>
    ''' List of command line calls to set the analysis process of a given analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisProcessCommands As New ObservableCollection(Of String)
    ''' <summary>
    ''' List of command line calls to set the analysis solver of a given analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisSolverCommands As New ObservableCollection(Of String)
    ''' <summary>
    ''' List of command line calls to set the analysis bit mode of a given analysis program. 32/64 bit modes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisBitTypeCommands As New ObservableCollection(Of String)

    ''' <summary>
    ''' "True/False list for combo boxes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property trueFalseBooleans As New ObservableCollection(Of String)
    ''' <summary>
    ''' "Yes/No list for combo boxes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property yesNoBooleans As New ObservableCollection(Of String)

    ''' <summary>
    ''' Path to the *.ini file used for initialization. Whether or not this is used depends on the csiTesterInstallMethod property.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property iniFile As cPath

    ''' <summary>
    ''' Version number of a program opening an old model file. This is added to the file name, and must also be tracked with the outputSettings XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property versionImportTag As String
#End Region

#Region "Properties: XML - RegTest"
    ''' <summary>
    ''' Name of the regTest.xml currently being used by CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regTestName As String

    ''' <summary>
    ''' Directory where the regTest.xml to be used is located.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regTestDirectory As String

    ''' <summary>
    ''' Name of the analysis program currently being used by CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property programName As eCSiProgram

    ''' <summary>
    ''' Version of the analysis program currently being used by CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programVersion As String

    ''' <summary>
    ''' Build of the analysis program currently being used by CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programBuild As String

    ''' <summary>
    ''' Alternative designation associated with a version of the analysis program currently being used by CSiTester. 
    ''' e.g. '2014' for 'v17.2.0'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property versionDesignation As String

    ''' <summary>
    ''' True: Models destination folder will be initialized for the next run.
    ''' False: The current models destination folder will be used unaltered.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property initializeModelDestinationFolder As Boolean

    ' TODO: Move into regTest schema for different loading into the program
    Public Property testsToRun As New ObservableCollection(Of String)

    ' TODO: Move into regTest schema for different loading into the program
    Public Property distributedTestSources As New ObservableCollection(Of String)

    ' TODO: Move into regTest schema for different loading into the program
    Public Property copyModelOptions As New ObservableCollection(Of String)

    ' TODO: Once XML serialization is fully implemented, this can be set internally and a new file generated on the fly.
    '       When that is done, phase out this property.
    ''' <summary>
    ''' RegTest control file that is used to validate a set of model files in the session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleValidationFile As New cPathSettings

    ' TODO: Once XML serialization is fully implemented, this can be set internally and a new file generated on the fly.
    '       When that is done, phase out this property.
    ''' <summary>
    ''' RegTest control file that is used to update a set of model files in the session.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleUpdateFile As New cPathSettings

#End Region
#Region "Properties: XML - CSiTester"
    ''' <summary>
    '''Path to the parent directory of where all of the CSiTester files will be copied. This will always contain a directory of model files that have been run or will be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testerDestination As New cPathSettings

    ''' <summary>
    ''' Current set level for CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property csiTesterlevel As eCSiTesterLevel

    ''' <summary>
    ''' List of the available levels that CSiTester can be set for.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property testerLevels As New ObservableCollection(Of String)

    ''' <summary>
    ''' Path recorded in the CSiTesterSettings.xml for the location of the XML relative to the CSiTester.exe.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property csiTesterFile As New cPathSettings

    ''' <summary>
    ''' Path to the folder containing seed files for copying, such as model control XMl files, retTest XML files for file validation, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property seedDirectory As New cPathSettings

    ''' <summary>
    ''' Name of the current user in the session.
    ''' Used for auto-filling dialogs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userName As String
    ''' <summary>
    ''' Name of the current user's company.
    ''' Used for auto-filling dialogs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userCompany As String
#End Region
#Region "Properties: XML - Programs"
    ''' <summary>
    ''' List of the programs available for use through the tester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programs As New ObservableCollection(Of String)

    ''' <summary>
    ''' List of versions available for a given program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programVersions As New ObservableCollection(Of String)

    ''' <summary>
    ''' List of versions available for a given program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programReleaseDates As New ObservableCollection(Of String)

    ''' <summary>
    ''' List of versions available for a given program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programBuilds As New ObservableCollection(Of String)

    '== Examples
    ''' <summary>
    ''' List of the XML paths for all of the examples in the suite.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplePathsSaved As New List(Of String)
    ' TODO: Swap the property below once implementing XML Serialization Method
    'Public Property examplePathsSaved As New ObservableCollection(Of cPathSettings)

    ''' <summary>
    ''' List of all examples set to be run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesRunSaved As New List(Of String)

    ''' <summary>
    ''' List of examples that have been run, with run results displayed in CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesRanSaved As New List(Of String)

    ''' <summary>
    ''' List of all examples set to be compared.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesCompareSaved As New List(Of String)

    ''' <summary>
    ''' List of examples that have been run and compared, with results displayed in CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property examplesComparedSaved As New List(Of String)

    ''' <summary>
    ''' Number of digits in the model sub id for grouped models, such as 10 = 2, and 01 = 2.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property modelSubIDIntegerLength As Integer

    Public Property statusTypes As New ObservableCollection(Of String)
    Public Property documentationStatusTypes As New ObservableCollection(Of String)


    '== General session settings
    ''' <summary>
    ''' If true, all examples are set to run with outputSettings.xml files, if they exist in the attachments folder.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputSettingsUsedAll As Boolean

    ''' <summary>
    ''' If all examples are set to be run with a uniform file extension type for the exported table files, it is imposed by assigning the global file extension to this property.
    ''' For this to be used, outputSettingsUsedAll = True.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tableExportFileExtensionAll As String

    ''' <summary>
    ''' If true, then analysis output files are expected to exist at the model level in the run directory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property analysisFilesPresent As Boolean


    '== Analysis Settings
    ''' <summary>
    ''' The solver type selected by the user to use in a run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property solverSaved As String

    ''' <summary>
    ''' The process type selected by the user to use in a run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property processSaved As String

    ''' <summary>
    ''' The bit type selected by the user to use in a run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bitTypeSaved As String


    '== Delete Files
    ''' <summary>
    ''' If true, analysis files will be deleted after the model has been run. If false, any files generated will remain.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteAnalysisFilesStatus As Boolean

    ''' <summary>
    ''' If analysis files are to be deleted after a run, log and warning files will be deleted in addition to the standard default files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteAnalysisFilesLogWarning As Boolean

    ''' <summary>
    ''' If analysis files are to be deleted after a run, exported table files will be deleted in addition to the standard default files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteAnalysisFilesTables As Boolean

    ''' <summary>
    ''' If analysis files are to be deleted after a run, model text files will be deleted in addition to the standard default files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteAnalysisFilesModelText As Boolean

    ''' <summary>
    ''' If analysis files are to be deleted after a run, all files with the same name as the model file will be deleted at the model location, except for supporting files and the model files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteAnalysisFilesAll As Boolean

    '== Main Gridview
    ''' <summary>
    ''' Sets whether run status operations apply to all tabs or only the visible tab.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property allTabsSelectRun As Boolean

    ''' <summary>
    ''' Sets whether compare status operations apply to all tabs or only the visible tab.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property allTabsSelectCompare As Boolean

    ''' <summary>
    ''' Sets whether examples should be automatically separated into different display tabs based on Classification 2 specification in the model XML.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property singleTab As Boolean
#End Region
#Region "Properties: XML - Defaults"
    'Defaults: Analysis Settings
    ''' <summary>
    ''' The solver type that is the default for a given CSi anlaysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property solverDefault As String
    ''' <summary>
    ''' The process type that is the default for a given CSi anlaysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property processDefault As String
    ''' <summary>
    ''' The bit type that is the default for a given CSi anlaysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property bitTypeDefault As String


    'Defaults Program: Program & Models Paths
    Public Property programPathStub As String
    Public Property modelSourcePathStub As String
    Public Property modelDestinationPathStub As String

    'Defaults Program: Documentation Paths
    Public Property documentsPathStubAnalysis As String
    Public Property documentsPathStubDesignSteelFrame As String
    Public Property documentsPathStubDesignConcreteFrame As String
    Public Property documentsPathStubDesignShearWall As String
    Public Property documentsPathStubDesignCompositeBeam As String
    Public Property documentsPathStubDesignCompositeColumn As String
    Public Property documentsPathStubDesignSlab As String


    'Defaults Program: Command Line Parameters: Analysis
    Public Property commandRunAnalysis As String
    Public Property commandClose As String
    Public Property commandBatchRun As String

    Public Property commandProcessSame As New ObservableCollection(Of String)
    Public Property commandProcessSeparate As New ObservableCollection(Of String)
    Public Property commandProcessAuto As New ObservableCollection(Of String)

    Public Property commandSolverStandard As New ObservableCollection(Of String)
    Public Property commandSolverAdvanced As New ObservableCollection(Of String)
    Public Property commandSolverMultiThreaded As New ObservableCollection(Of String)

    Public Property commandBit32 As New ObservableCollection(Of String)
    Public Property commandBit64 As New ObservableCollection(Of String)


    'Defaults Program: Command Line Parameters: Design
    Public Property commandRunDesign As String
    Public Property commandRunDesignSteel As String
    Public Property commandRunDesignConcrete As String
    Public Property commandRunDesignWall As String
    Public Property commandRunDesignCompositeBeam As String
    Public Property commandRunDesignCompositeColumn As String
    Public Property commandRunDesignAluminum As String
    Public Property commandRunDesignColdFormed As String
    Public Property commandRunDesignSlab As String


    'Defaults Program: Command Line Parameters: Misc
    Public Property commandCreateInfoFile As String
    Public Property commandSave As String

    'Defaults Program: File Types
    ''' <summary>
    ''' List of all of the CSi types that will open directly in a given CSi product.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileTypes As New ObservableCollection(Of String)

    'Defaults Program: Import Tags
    ''' <summary>
    ''' List of potential tags generated by a CSi product if opening/importing an older model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property importTags As New List(Of String)

    ''' <summary>
    ''' CSi product version associated with the earliest implementation of a given tag. This list is in sync with 'listImportTags'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property importTagVersions As New List(Of String)

    'Defaults Program: Design Types
    Public Property designTypeFiltered As New ObservableCollection(Of String)
#End Region
#Region "Properties: XML - Examples"
    'TO DO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    Public Property exampleClassifications As New ObservableCollection(Of cClassification)

    Public Property exampleClassificationLvl1 As New ObservableCollection(Of String)
    Public Property exampleClassificationLvl2 As New ObservableCollection(Of String)

    'TO DO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    ''' <summary>
    ''' Keywords stored in the settings file, including the text and description of the text.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleKeywords As New ObservableCollection(Of cKeywordsManager)

    Public Property exampleClassAnalysis As New ObservableCollection(Of String)
    Public Property exampleClassDesign As New ObservableCollection(Of String)
    Public Property exampleCodeRegion As New ObservableCollection(Of String)
    Public Property exampleType As New ObservableCollection(Of String)
    Public Property analysisType As New ObservableCollection(Of String)
    Public Property designType As New ObservableCollection(Of String)
    Public Property elementType As New ObservableCollection(Of String)
    Public Property warningType As New ObservableCollection(Of String)

#End Region

#Region "Initialization"
    ''' <summary>
    ''' Generates class populated with XML data based on the default location or optional user-specified location.
    ''' </summary>
    ''' <param name="myPath">Path to the XML file.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal myPath As String = "")
        Try
            xmlFile.SetProperties(myPath)

            'Set default location
            If String.IsNullOrEmpty(xmlFile.path) Then
                xmlFile.SetProperties(pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS)
            End If

            SetDefaults()

            _adapter.Fill(Me)

            InitializeControl()

            PopulateAnalysisSettingsLists()

            SetData()

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates all program-specific default values.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateProgramDefaults()
        _adapter.UpdateProgramDefaults()
    End Sub

    ''' <summary>
    ''' Currently adds True/False &amp; Yes/No lists to the class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetData()
        trueFalseBooleans = New ObservableCollection(Of String)
        With trueFalseBooleans
            .Add("True")
            .Add("False")
        End With

        yesNoBooleans = New ObservableCollection(Of String)
        With yesNoBooleans
            .Add("Yes")
            .Add("No")
        End With
    End Sub

    ''' <summary>
    ''' Initializes XML reading methods of the Settings class
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeControl()
        Try
            SortLists()
            SetImportedModelStubDefault()

            'Check if default location has changed from last time settings file was changed
            If Not StringsMatch(csiTesterFile.path, xmlFile.path) Then
                csiTesterFile.SetProperties(pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS, p_pathUnknown:=False)
                programLocationChanged = True
            Else
                programLocationChanged = False
            End If

            'Determines the default imported model path stub
            SetImportedModelStubDefault()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    'TODO: Finish implementation after cleanly separating into new class. Currently tightly coupled with ReadXmlSettingsObject in mFiles
    ''' <summary>
    ''' Initializes the 'object' hierarchy items in the settings XML, such as classification and keywords lists.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeExamplesObjects()
        Try
            _adapter.FillExamplesObjects(Me)

            'Create settings lists
            SetClassification1List()
            SetKeywordsLists()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    Private Sub SetDefaults()
        deleteAnalysisFilesStatus = False
        deleteAnalysisFilesLogWarning = False
        deleteAnalysisFilesTables = False
        deleteAnalysisFilesAll = False
    End Sub

    ''' <summary>
    ''' Sort paired lists to be ordered by a specified key list.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SortLists()
        'Import Tags Lists
        Dim correlatedLists As New List(Of List(Of String))
        correlatedLists.Add(importTags)
        SortCorrelatedLists(importTagVersions, correlatedLists, True)
    End Sub

    ''' <summary>
    ''' Populates class classification lists from the sub-classes generated from reading the settings XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetClassification1List()
        exampleClassificationLvl1 = New ObservableCollection(Of String)

        For Each myClassification As cClassification In exampleClassifications
            exampleClassificationLvl1.Add(myClassification.name)
        Next

        'Set initial default 'classification 2' list to match the first 'classification 1' entry
        SetClassification2List(exampleClassificationLvl1(0))

    End Sub

    ''' <summary>
    ''' Populates class keywords lists from the sub-classes generated from reading the settings XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKeywordsLists()
        exampleClassAnalysis = New ObservableCollection(Of String)
        exampleClassDesign = New ObservableCollection(Of String)
        exampleCodeRegion = New ObservableCollection(Of String)
        exampleType = New ObservableCollection(Of String)
        analysisType = New ObservableCollection(Of String)
        designType = New ObservableCollection(Of String)
        elementType = New ObservableCollection(Of String)
        warningType = New ObservableCollection(Of String)

        For Each myKeywords As cKeywordsManager In exampleKeywords
            Select Case myKeywords.name
                Case "analysis_class"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        exampleClassAnalysis.Add(myKeyWordEntry.name)
                    Next
                Case "design_class"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        exampleClassDesign.Add(myKeyWordEntry.name)
                    Next
                Case "code_region"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        exampleCodeRegion.Add(myKeyWordEntry.name)
                    Next
                Case "example_type"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        exampleType.Add(myKeyWordEntry.name)
                    Next
                Case "analysis_type"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        analysisType.Add(myKeyWordEntry.name)
                    Next
                Case "design_type"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        designType.Add(myKeyWordEntry.name)
                    Next
                Case "element_type"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        elementType.Add(myKeyWordEntry.name)
                    Next
                Case "warning"
                    For Each myKeyWordEntry As cKeyword In myKeywords.keywords
                        warningType.Add(myKeyWordEntry.name)
                    Next
            End Select
        Next

    End Sub

    ''' <summary>
    ''' Clears all saved example paths, run statuses, &amp; saved statuses, to create a fresh load.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub NewSourceDestination()
        examplePathsSaved.Clear()
        examplesRunSaved.Clear()
        examplesRanSaved.Clear()
        examplesCompareSaved.Clear()
        examplesComparedSaved.Clear()
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Updates properties related to regTest.
    ''' </summary>
    ''' <param name="p_regTest">RegTest object to base updates on.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateSettings(ByVal p_regTest As cRegTest)
        programName = p_regTest.program_name
        programVersion = p_regTest.program_version
        programBuild = p_regTest.program_build
        ChangeProgram()
        SetImportedModelStubDefault()
    End Sub

    ''' <summary>
    ''' Gets the list of file types applicable to a specified program.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the list for.</param>
    ''' <param name="p_setSettings">True: The settings class property list updates to the return value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetFileTypes(ByVal p_programName As eCSiProgram,
                                           Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Dim fileTypesNew As New ObservableCollection(Of String)
        Try
            fileTypesNew = _adapter.GetProgramFileTypes(p_programName, p_setSettings)
            If (p_setSettings AndAlso fileTypesNew.Count > 0) Then
                fileTypes = fileTypesNew
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            GetFileTypes = fileTypesNew
        End Try
    End Function
    ''' <summary>
    ''' Gets the list of file types applicable to a specified program.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the list for.</param>
    ''' <param name="p_setSettings">True: The settings class property list updates to the return value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetFileTypes(ByVal p_programName As String,
                                            Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Dim newProgramName As eCSiProgram = ConvertStringToEnumByDescription(Of eCSiProgram)(p_programName)

        Return GetFileTypes(newProgramName, p_setSettings)

    End Function

    ''' <summary>
    ''' Gets the list of program versions applicable to a specified program.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the list for.</param>
    ''' <param name="p_setSettings">True: The settings class property list updates to the return value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetProgramVersions(ByVal p_programName As eCSiProgram,
                                                Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Dim programVersionsNew As New ObservableCollection(Of String)
        Try
            programVersionsNew = _adapter.GetProgramVersions(p_programName, p_setSettings)
            If (p_setSettings AndAlso programVersionsNew.Count > 0) Then
                programVersions = programVersionsNew
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        Finally
            GetProgramVersions = programVersionsNew
        End Try
    End Function
    ''' <summary>
    ''' Gets the list of program versions applicable to a specified program.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to get the list for.</param>
    ''' <param name="p_setSettings">True: The settings class property list updates to the return value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetProgramVersions(ByVal p_programName As String,
                                                Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
        Dim newprogramName As eCSiProgram = ConvertStringToEnumByDescription(Of eCSiProgram)(p_programName)

        Return GetProgramVersions(newprogramName, p_setSettings)
    End Function

    ''' <summary>
    ''' Updates the settings file of cleared results, which are to no longer be displayed unless run again. Maintains saved selected state and saves trigger to regTest to reinitialize.
    ''' </summary>
    ''' <param name="updateRunRanCompareComparedLists">If true, then the settings recording what examples have been set to be run and compared, and successfully run and compared, will be saved. If false, only the initialization status will be saved.</param>
    ''' <remarks></remarks>
    Friend Sub SaveFolderInitializationSettings(Optional ByVal updateRunRanCompareComparedLists As Boolean = False)

        _adapter.Write(cSettingsAdapter.eReadWriteAction.writeFolderInitializationSettings)

        If updateRunRanCompareComparedLists Then _adapter.Write(cSettingsAdapter.eReadWriteAction.writeRunRanCompareComparedLists)
    End Sub

    ''' <summary>
    ''' Checks if the supplied program name is valid for the tester
    ''' </summary>
    ''' <param name="p_newProgramName">Name of the program</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function ValidProgram(ByVal p_newProgramName As eCSiProgram) As Boolean
        Dim newProgramName As String = GetEnumDescription(p_newProgramName)
        ValidProgram = False


        For Each program As String In programs
            If newProgramName = program Then
                ValidProgram = True
                Exit For
            End If
        Next
    End Function
    ''' <summary>
    ''' Checks if the supplied program name is valid for the tester
    ''' </summary>
    ''' <param name="p_newProgramName">Name of the program</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function ValidProgram(ByVal p_newProgramName As String) As Boolean
        Dim newprogramName As eCSiProgram = ConvertStringToEnumByDescription(Of eCSiProgram)(p_newProgramName)

        Return ValidProgram(newprogramName)
    End Function

    ''' <summary>
    ''' Enforces user to only have access to features of the program set in the settings file, including what *.exe is a valid one to select for test runs.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to be the only one available in lists and cross-reference checks for a valid program. If not specified, taken as the program written in the settings file.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub EnforceSetProgram(Optional ByVal p_programName As eCSiProgram = eCSiProgram.None)
        Dim newListPrograms As New List(Of String)

        If p_programName = eCSiProgram.None Then p_programName = programName

        newListPrograms.Add(GetEnumDescription(p_programName))

        programs.Clear()
        programs = ConvertListToObservableCollection(newListPrograms)
    End Sub
    ''' <summary>
    ''' Enforces user to only have access to features of the program set in the settings file, including what *.exe is a valid one to select for test runs.
    ''' </summary>
    ''' <param name="p_programName">Name of the program to be the only one available in lists and cross-reference checks for a valid program. If not specified, taken as the program written in the settings file.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub EnforceSetProgram(ByVal p_programName As String)
        Dim newprogramName As eCSiProgram = ConvertStringToEnumByDescription(Of eCSiProgram)(p_programName)

        EnforceSetProgram(newprogramName)
    End Sub

    ''' <summary>
    ''' Gathers a program's names for analysis default types from the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ConvertAnalysisDefaults()
        _adapter.FillAnalysisSettings(Me)
    End Sub

    ''' <summary>
    ''' Writes the command line commands with analysis settings specified.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub WriteCommandLineAnalysisSettings()
        Dim process As String = ""
        Dim solver As String = ""
        Dim bitType As String = ""

        MatchCommandToName(process, processSaved, analysisProcesses, analysisProcessCommands)
        MatchCommandToName(solver, solverSaved, analysisSolver, analysisSolverCommands)
        MatchCommandToName(bitType, bitTypeSaved, analysisBitType, analysisBitTypeCommands)

        myRegTest.command_line = Trim(commandRunAnalysis & " " & process & " " & solver & " " & bitType & " " & commandClose & " " & commandBatchRun)
    End Sub

    ''' <summary>
    ''' Updates the settings class and form combo boxes to reflect the selection of a different program
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ChangeProgram()
        Save()
        InitializeControl()
        PopulateAnalysisSettingsLists()
    End Sub

    ''' <summary>
    ''' Updates settings properties and saves relevant properties to the XML file
    ''' </summary>
    ''' <param name="p_resetDefaults">True: The class won't be automatically updated before save.</param>
    ''' <remarks></remarks>
    Friend Sub Save(Optional ByVal p_resetDefaults As Boolean = False)
        'Update class of any values not automatically updated
        If Not p_resetDefaults Then UpdateSettings(myCsiTester)

        _adapter.Write(cSettingsAdapter.eReadWriteAction.writeAll)
    End Sub

    ''' <summary>
    ''' Creates the 'Classification 2' classification list based on the 'Classification 1' specified.
    ''' </summary>
    ''' <param name="myClassification1Name">Name of the 'Classification 1' group for which to create a list of the sub-classifications.</param>
    ''' <remarks></remarks>
    Friend Sub SetClassification2List(ByVal myClassification1Name As String)
        exampleClassificationLvl2 = New ObservableCollection(Of String)

        For Each myClassification1 As cClassification In exampleClassifications
            If myClassification1.name = myClassification1Name Then
                For Each myClassification2 As cKeyword In myClassification1.subClassification
                    exampleClassificationLvl2.Add(myClassification2.name)
                Next
            End If
        Next

    End Sub

    ''' <summary>
    ''' Sets the default model import stub that is expected to accompany any imported model.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetImportedModelStubDefault()
        Dim i As Integer = 0

        If importTags.Count = 0 Then Exit Sub

        If (Not String.IsNullOrEmpty(programVersion) OrElse
            Not String.IsNullOrEmpty(programBuild)) Then      'Get the tag corresponding to the most recent implementation that is older than the current program version+build
            For Each version As String In importTagVersions
                If FilterNumeric(version) <= programVersion & "." & programBuild Then
                    versionImportTag = importTags(i)
                    Exit For
                End If
                i += 1
            Next
        End If
        If String.IsNullOrEmpty(versionImportTag) Then versionImportTag = importTags(0) 'Get the tag corresponding to the most recent implementation

        'Post-process default stubs in case they were not entered correctly
        If Not programName = eCSiProgram.SAFE Then
            If Not StringExistInName(versionImportTag, "V") Then
                versionImportTag = "V" & versionImportTag
                If Not StringExistInName(versionImportTag, "_") Then
                    versionImportTag = "_" & versionImportTag
                End If
            End If
        End If

        _outputSettingsVersionSession = versionImportTag
    End Sub

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Updates paths, run selections, and compare selections from CsiTester class.
    ''' </summary>
    ''' <param name="p_csiTester">CSiTester object to base updates on.</param>
    ''' <remarks></remarks>
    Private Sub UpdateSettings(ByVal p_csiTester As cCsiTester)
        examplePathsSaved = p_csiTester.suiteXMLPathList

        'Get list of examples selected to be compared
        p_csiTester.GetExamplesRunCompare()
        examplesRunSaved = p_csiTester.exampleRunIDs
        examplesRanSaved = p_csiTester.exampleRanIDs
        examplesCompareSaved = p_csiTester.exampleCompareIDs
        examplesComparedSaved = p_csiTester.exampleComparedIDs

        initializeModelDestinationFolder = p_csiTester.initializeModelDestinationFolder

    End Sub

    ''' <summary>
    ''' Path to the icons for each of the programs checked
    ''' </summary>
    ''' <param name="p_programName">If specified, will overwrite the program name gathered from the settings file.</param>
    ''' <returns>Relative path</returns>
    ''' <remarks></remarks>
    Private Function IconPathString(Optional ByVal p_programName As eCSiProgram = eCSiProgram.None) As String
        If p_programName = eCSiProgram.None Then p_programName = programName
        Dim programNameString As String = GetEnumDescription(p_programName)
        Dim iconExtension As String = ".ico"

        IconPathString = ".\Resources\"

        Select Case p_programName
            Case eCSiProgram.SAP2000 : IconPathString = IconPathString & programNameString & " Icon" & iconExtension
            Case eCSiProgram.CSiBridge : IconPathString = IconPathString & programNameString & iconExtension
            Case eCSiProgram.ETABS : IconPathString = IconPathString & programNameString & iconExtension
            Case eCSiProgram.SAFE : IconPathString = IconPathString & programNameString & "v12 Icon" & iconExtension
            Case Else : IconPathString = IconPathString & "CSI_icon_logo" & iconExtension
        End Select
    End Function

    ''' <summary>
    ''' Populates the lists of available analysis settings
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateAnalysisSettingsLists()
        analysisProcesses = New ObservableCollection(Of String)
        analysisSolver = New ObservableCollection(Of String)
        analysisBitType = New ObservableCollection(Of String)

        analysisProcessCommands = New ObservableCollection(Of String)
        analysisSolverCommands = New ObservableCollection(Of String)
        analysisBitTypeCommands = New ObservableCollection(Of String)

        'Adds the name of the option to the list if there is a corresponding command line to write to regTest.
        'Process
        CreateCommandLists(commandProcessSame, analysisProcesses, analysisProcessCommands)
        CreateCommandLists(commandProcessSeparate, analysisProcesses, analysisProcessCommands)
        CreateCommandLists(commandProcessAuto, analysisProcesses, analysisProcessCommands)

        'Solver
        CreateCommandLists(commandSolverStandard, analysisSolver, analysisSolverCommands)
        CreateCommandLists(commandSolverAdvanced, analysisSolver, analysisSolverCommands)
        CreateCommandLists(commandSolverMultiThreaded, analysisSolver, analysisSolverCommands)

        'Bit Type
        CreateCommandLists(commandBit32, analysisBitType, analysisBitTypeCommands)
        CreateCommandLists(commandBit64, analysisBitType, analysisBitTypeCommands)

    End Sub

    ''' <summary>
    ''' Creates lists of the commands and their corresponding names. 
    ''' An entry for a given command is only created if the XML command entry is filled in.
    ''' </summary>
    ''' <param name="p_command">Collection of command line elements gathered from the XML file.</param>
    ''' <param name="p_listName">List to be populated with command line names.</param>
    ''' <param name="p_listCommand">List to be populated with command line commands.</param>
    ''' <remarks></remarks>
    Private Sub CreateCommandLists(ByVal p_command As ObservableCollection(Of String),
                                   ByRef p_listName As ObservableCollection(Of String),
                                   ByRef p_listCommand As ObservableCollection(Of String))
        Dim indexCommand As Integer = 1
        Dim indexName As Integer = 2

        If (Not indexName > p_command.Count - 1 AndAlso
            Not String.IsNullOrEmpty(p_command(indexCommand))) Then

            p_listName.Add(p_command(indexName))
            p_listCommand.Add(p_command(indexCommand))
        End If
    End Sub


    ''' <summary>
    ''' Takes the analysis command selected by the user and fetches the corresponding command call
    ''' </summary>
    ''' <param name="myCommand">Command call type needed</param>
    ''' <param name="mySelection">Command call name selected</param>
    ''' <param name="myNames">List of command call names</param>
    ''' <param name="myCommands">List of commands for the call type</param>
    ''' <remarks></remarks>
    Private Sub MatchCommandToName(ByRef myCommand As String, ByVal mySelection As String, ByVal myNames As ObservableCollection(Of String), ByVal myCommands As ObservableCollection(Of String))
        Dim i As Integer

        i = 0
        For Each myName As String In myNames
            If myName = mySelection Then
                myCommand = myCommands(i)
            End If
            i += 1
        Next
    End Sub
#End Region

#Region "Test Components"

    ''' <summary>
    ''' Validates that the desired paths to be recorded in the XML file have been cleared.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="expctExamplesNum"></param>
    ''' <param name="expctAbsPathsExample"></param>
    ''' <param name="expctRelPathsExample"></param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtNewPaths(ByVal className As String, Optional ByVal expctExamplesNum As Integer = -1, Optional ByVal expctRelPathsExample As ObservableCollection(Of String) = Nothing, Optional ByVal expctAbsPathsExample As ObservableCollection(Of String) = Nothing) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If expctExamplesNum >= 0 Then
                .expectation = "Number of saved example paths to be as expected"
                .resultActual = CStr(examplePathsSaved.Count)
                .resultActualCall = classIdentifier & "examplePathsSaved.Count"
                .resultExpected = CStr(expctExamplesNum)
                If Not .RunSubTest() Then Return subTestPass
            End If
            If expctRelPathsExample IsNot Nothing Then
                Dim relPaths As New List(Of String)
                Dim xmlReaderWriter As New cXmlReadWrite()
                relPaths = xmlReaderWriter.GetSingleXMLNodeListValues(xmlFile.path, "//n:csitester/n:example_paths")

                For Each expctRelPath As String In expctRelPathsExample
                    .expectation = "Relative path recorded in XML is present and as expected"
                    .resultActual = "Not Present"
                    .resultActualCall = classIdentifier & "relPath"
                    .resultExpected = expctRelPath
                    For Each relPath As String In relPaths
                        If relPath = expctRelPath Then
                            .resultActual = relPath
                            Exit For
                        End If
                    Next
                    If Not .RunSubTest() Then Return subTestPass
                Next
            End If
            If expctAbsPathsExample IsNot Nothing Then
                Dim absPaths As New List(Of String)
                absPaths = testerSettings.examplePathsSaved

                For Each expctAbsPath As String In expctAbsPathsExample
                    .expectation = "Absolute path in program is present and as expected"
                    .resultActual = "Not Present"
                    .resultActualCall = classIdentifier & "absPath"
                    .resultExpected = expctAbsPath
                    For Each absPath As String In absPaths
                        If absPath = expctAbsPath Then
                            .resultActual = absPath
                            Exit For
                        End If
                    Next
                    If Not .RunSubTest() Then Return subTestPass
                Next
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that desired settings to be recorded in the XML file have been cleared.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="clearRun">Check if examples set to run have been cleared.</param>
    ''' <param name="clearCompare">Check if examples set to be compared have been cleared.</param>
    ''' <param name="clearRan">Check if examples that are marked as having been run are cleared.</param>
    ''' <param name="clearCompared">Check if examples that are marked as having been compared are cleared.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtClearExampleSettings(ByVal className As String, Optional ByVal clearRun As Boolean = False, Optional ByVal clearCompare As Boolean = False, _
                                 Optional ByVal clearRan As Boolean = False, Optional ByVal clearCompared As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If clearRun Then
                .expectation = "Saved list of examples set to be run to be cleared"
                .resultActual = CStr(examplesRunSaved.Count)
                .resultActualCall = classIdentifier & "examplesRunSaved.Count"
                .resultExpected = "0"
                If Not .RunSubTest() Then Return subTestPass
            End If
            If clearCompare Then
                .expectation = "Saved list of examples set to be compared to be cleared"
                .resultActual = CStr(examplesCompareSaved.Count)
                .resultActualCall = classIdentifier & "examplesCompareSaved.Count"
                .resultExpected = "0"
                If Not .RunSubTest() Then Return subTestPass
            End If
            If clearRan Then
                .expectation = "Saved list of examples that have been run to be cleared"
                .resultActual = CStr(examplesRanSaved.Count)
                .resultActualCall = classIdentifier & "examplesRanSaved.Count"
                .resultExpected = "0"
                If Not .RunSubTest() Then Return subTestPass
            End If
            If clearCompared Then
                .expectation = "Saved list of examples that have been compared to be cleared"
                .resultActual = CStr(examplesComparedSaved.Count)
                .resultActualCall = classIdentifier & "examplesComparedSaved.Count"
                .resultExpected = "0"
                If Not .RunSubTest() Then Return subTestPass
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function


    ''' <summary>
    ''' Validates that ...
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="testSwitch">Check if ...</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtTemplate(ByVal className As String, Optional ByVal testSwitch As Boolean = False) As Boolean
        Dim subTestPass As Boolean
        'Dim classIdentifier As String   = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        'With e2eTester
        '    If testSwitch Then
        '        .expectation = "Saved example paths to be cleared"
        '        .resultActual = CStr(examplePathsSaved.Count)
        '        .resultActualCall = classIdentifier & "examplePathsSaved.Count"
        '        .resultExpected = ""
        '        .resultNotExpected = ""
        '        If Not .runSubTest() Then Return subTestPass
        '    End If
        'End With

        subTestPass = True

        Return subTestPass
    End Function
#End Region

#Region "XML Reading Backups (Temp)"

    ' ''' <summary>
    ' ''' Name of the regTest.xml currently being used by CSiTester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property regTestName As String
    ' ''' <summary>
    ' ''' Directory where the regTest.xml to be used is located.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property regTestDirectory As String
    ' ''' <summary>
    ' ''' Name of the analysis program currently being used by CSiTester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property programName As String
    'Public Property versionDesignation As String
    'Public Property programVersion As String
    'Public Property initializeModelDestinationFolder As Boolean
    'Public Property programBuild As String
    'Public Property listTestsToRun As ObservableCollection(Of String)
    'Public Property listDistributedTestSources As ObservableCollection(Of String)
    'Public Property listCopyModelOptions As ObservableCollection(Of String)
    'Public Property exampleValidationFile As String
    'Public Property exampleValidationDirectory As String
    'Public Property exampleUpdateFile As String
    'Public Property exampleUpdateDirectory As String

    ''= CSiTester
    ' ''' <summary>
    ' '''Path to the parent directory of where all of the CSiTester files will be copied. This will always contain a directory of model files that have been run or will be run.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property testerDestinationDir As String
    ' ''' <summary>
    ' ''' Current set level for CSiTester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property csiTesterlevel As eCSiTesterLevel
    ' ''' <summary>
    ' ''' List of the available levels that CSiTester can be set for.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listTesterLevels As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' Path recorded in the CSiTesterSettings.xml for the location of the XML relative to the CSiTester.exe.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property csiTesterPath As String
    ' ''' <summary>
    ' ''' Path to the folder containing seed files for copying, such as model control XMl files, retTest XML files for file validation, etc.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property seedPath As String
    ' ''' <summary>
    ' ''' Path to the *.ini file used for initialization. Whether or not this is used depends on the csiTesterInstallMethod property.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property pathIni As String

    'Public Property userName As String
    'Public Property userCompany As String

    ''= Programs
    ' ''' <summary>
    ' ''' List of the programs available for use through the tester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listPrograms As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' List of versions available for a given program.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listProgramVersions As ObservableCollection(Of String)

    ''== Examples
    ' ''' <summary>
    ' ''' List of the XML paths for all of the examples in the suite.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property examplePathsSaved As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' List of all examples set to be run.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property examplesRunSaved As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' List of examples that have been run, with run results displayed in CSiTester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property examplesRanSaved As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' List of all examples set to be compared.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property examplesCompareSaved As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' List of examples that have been run and compared, with results displayed in CSiTester.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property examplesComparedSaved As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' Number of digits in the model id, such as 100 = 3, and 001 = 3.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property modelIDIntegerLength As Integer
    ' ''' <summary>
    ' ''' Number of digits in the model sub id for grouped models, such as 10 = 2, and 01 = 2.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property modelSubIDIntegerLength As Integer
    'Public Property listStatus As ObservableCollection(Of String)
    'Public Property listDocumentationStatus As ObservableCollection(Of String)

    ''== General session settings
    ' ''' <summary>
    ' ''' If true, all examples are set to run with outputSettings.xml files, if they exist in the attachments folder.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property outputSettingsUsedAll As Boolean
    ' ''' <summary>
    ' ''' If all examples are set to be run with a uniform file extension type for the exported table files, it is imposed by assigning the global file extension to this property.
    ' ''' For this to be used, outputSettingsUsedAll = True.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property tableExportFileExtensionAll As String
    ' ''' <summary>
    ' ''' Version number of a program opening an old model file. This is added to the file name, and must also be tracked with the outputSettings XML file.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property versionImportTag As String
    ' ''' <summary>
    ' ''' If true, then analysis output files are expected to exist at the model level in the run directory.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property analysisFilesPresent As Boolean


    ''== Analysis Settings
    ' ''' <summary>
    ' ''' The solver type selected by the user to use in a run.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property solverSaved As String
    ' ''' <summary>
    ' ''' The process type selected by the user to use in a run.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property processSaved As String
    ' ''' <summary>
    ' ''' The bit type selected by the user to use in a run.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property bitTypeSaved As String


    ''== Delete Files
    ' ''' <summary>
    ' ''' If true, analysis files will be deleted after the model has been run. If false, any files generated will remain.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property deleteAnalysisFilesStatus As Boolean
    ' ''' <summary>
    ' ''' If analysis files are to be deleted after a run, log and warning files will be deleted in addition to the standard default files.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property deleteAnalysisFilesLogWarning As Boolean
    ' ''' <summary>
    ' ''' If analysis files are to be deleted after a run, exported table files will be deleted in addition to the standard default files.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property deleteAnalysisFilesTables As Boolean
    ' ''' <summary>
    ' ''' If analysis files are to be deleted after a run, model text files will be deleted in addition to the standard default files.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property deleteAnalysisFilesModelText As Boolean
    ' ''' <summary>
    ' ''' If analysis files are to be deleted after a run, all files with the same name as the model file will be deleted at the model location, except for supporting files and the model files.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property deleteAnalysisFilesAll As Boolean

    ''== Main Gridview
    ' ''' <summary>
    ' ''' Sets whether run status operations apply to all tabs or only the visible tab.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property allTabsSelectRun As Boolean
    ' ''' <summary>
    ' ''' Sets whether compare status operations apply to all tabs or only the visible tab.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property allTabsSelectCompare As Boolean
    ' ''' <summary>
    ' ''' Sets whether examples should be automatically separated into different display tabs based on Classification 2 specification in the model XML.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property singleTab As Boolean

    ''Defaults: Analysis Settings
    ' ''' <summary>
    ' ''' The solver type that is the default for a given CSi anlaysis program.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property solverDefault As String
    ' ''' <summary>
    ' ''' The process type that is the default for a given CSi anlaysis program.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property processDefault As String
    ' ''' <summary>
    ' ''' The bit type that is the default for a given CSi anlaysis program.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property bitTypeDefault As String

    ''Defaults Program: Program & Models Paths
    'Public Property programPath As String
    'Public Property modelSourcePath As String
    'Public Property modelDestinationPath As String

    ''Defaults Program: Documentation Paths
    'Public Property documentsPathsAnalysis As String
    'Public Property documentsPathsDesignSteelFrame As String
    'Public Property documentsPathsDesignConcreteFrame As String
    'Public Property documentsPathsDesignShearWall As String
    'Public Property documentsPathsDesignCompositeBeam As String
    'Public Property documentsPathsDesignCompositeColumn As String
    'Public Property documentsPathsDesignSlab As String

    ''Defaults Program: Command Line Parameters: Analysis
    'Public Property commandRunAnalysis As String
    'Public Property commandClose As String
    'Public Property commandBatchRun As String

    'Public Property commandProcessSame As ObservableCollection(Of String)
    'Public Property commandProcessSeparate As ObservableCollection(Of String)
    'Public Property commandProcessAuto As ObservableCollection(Of String)

    'Public Property commandSolverStandard As ObservableCollection(Of String)
    'Public Property commandSolverAdvanced As ObservableCollection(Of String)
    'Public Property commandSolverMultiThreaded As ObservableCollection(Of String)

    'Public Property commandBit32 As ObservableCollection(Of String)
    'Public Property commandBit64 As ObservableCollection(Of String)

    ''Defaults Program: Command Line Parameters: Design
    'Public Property commandRunDesign As String
    'Public Property commandRunDesignSteel As String
    'Public Property commandRunDesignConcrete As String
    'Public Property commandRunDesignWall As String
    'Public Property commandRunDesignCompositeBeam As String
    'Public Property commandRunDesignCompositeColumn As String
    'Public Property commandRunDesignAluminum As String
    'Public Property commandRunDesignColdFormed As String

    ''Defaults Program: Command Line Parameters: Misc
    'Public Property commandCreateInfoFile As String
    'Public Property commandSave As String

    ''Defaults Program: File Types
    ' ''' <summary>
    ' ''' List of all of the CSi types that will open directly in a given CSi product.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listFileTypes As ObservableCollection(Of String)

    ''Defaults Program: Import Tags
    ' ''' <summary>
    ' ''' List of potential tags generated by a CSi product if opening/importing an older model file.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listImportTags As ObservableCollection(Of String)
    ' ''' <summary>
    ' ''' CSi product version associated with the earliest implementation of a given tag. This list is in sync with 'listImportTags'.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property listImportTagVersions As ObservableCollection(Of String)

    ''Defaults Program: Design Types
    'Public Property designTypeFiltered As ObservableCollection(Of String)

    ' ''' <summary>
    ' ''' Initializes XML reading methods of the Settings class
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub InitializeControl()
    '    Try
    '        'Initializization XML Properties
    '        '=== XML Operations ===
    '        'If InitializeXML(xmlPath) Then
    '        '    ReadWriteSettingsXML(True)
    '        '    CloseXML()
    '        'End If
    '        '=== End XML Operations ===

    '        SortLists()
    '        SetImportedModelStubDefault()

    '        'Check if default location has changed from last time settings file was changed
    '        If Not csiTesterPath = xmlPath Then
    '            csiTesterPath = pathStartup() & "\" & dirNameCSiTester & "\" & fileNameCSiTesterSettings
    '            'SaveSettings()
    '            programLocationChanged = True
    '        Else
    '            programLocationChanged = False
    '        End If

    '        'Determines the default imported model path stub
    '        SetImportedModelStubDefault()
    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Populates Settings class with data from the XML.
    ' ''' </summary>
    ' ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ' ''' <remarks></remarks>
    'Private Sub ReadWriteSettingsXML(ByVal read As Boolean)
    '    Try
    '        ReadWriteSettingsXmlNodes(read)
    '        ReadWriteSettingsXmlList(read)
    '        'ReadWriteExampleXmlObject(read)

    '        If Not read Then
    '            SaveXML(xmlPath)
    '        End If
    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Gets the list of file types applicable to a specified program.
    ' ''' </summary>
    ' ''' <param name="p_programName">Name of the program to get the list for.</param>
    ' ''' <param name="p_setSettings">Optional: If true, the settings class property list of file types updates to the return value.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Friend Function GetFileTypes(ByRef p_programName As String,
    '                             Optional ByVal p_setSettings As Boolean = False) As ObservableCollection(Of String)
    '   Dim pathNode As String
    '    Dim fileTypes As New ObservableCollection(Of String)
    '    Try
    '        If InitializeXML(xmlPath) Then
    '        pathNode = "//n:defaults_" & programName & "/n:file_types"
    '        If ReadNodeListText(pathNode, myTempList) Then
    '            If setSettings Then listFileTypes = myTempList
    '        End If
    '        CloseXML()
    '        End If
    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    Finally
    '        GetFileTypes = fileTypes
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' Updates settings properties and saves relevant properties to the XML file
    ' ''' </summary>
    ' ''' <param name="resetDefaults">Optional: If true, the class won't be automatically updated before save.</param>
    ' ''' <remarks></remarks>
    'Friend Sub SaveSettings(Optional ByVal resetDefaults As Boolean = False)

    '    'Update class of any values not automatically updated
    '    If Not resetDefaults Then UpdateSettings()

    '    ReadWriteToFromFile(eSettingsReadWriteAction.writeAll)

    '    'Updates XML File
    '    'SaveSettingsXML()
    'End Sub

    ' ''' <summary>
    ' ''' Updates the settings file of cleared results, which are to no longer be displayed unless run again. Maintains saved selected state and saves trigger to regTest to reinitialize.
    ' ''' </summary>
    ' ''' <param name="updateRunRanCompareComparedLists">If true, then the settings recording what examples have been set to be run and compared, and successfully run and compared, will be saved. If false, only the initialization status will be saved.</param>
    ' ''' <remarks></remarks>
    'Friend Sub SaveFolderInitializationSettings(Optional ByVal updateRunRanCompareComparedLists As Boolean = False)

    '    ReadWriteToFromFile(eSettingsReadWriteAction.writeFolderInitializationSettings)

    '    If updateRunRanCompareComparedLists Then ReadWriteToFromFile(eSettingsReadWriteAction.writeRunRanCompareComparedLists)

    '    '=== XML Operations ===
    '    'If InitializeXML(xmlPath) Then
    '    '    WriteNodeText(ConvertYesTrueString(initializeModelDestinationFolder, eCapitalization.alllower), "//n:regtest/n:models_run_dir", "initialize")
    '    '    If updateRunRanCompareComparedLists Then
    '    '        WriteNodeListText(examplesRunSaved.ToArray, "//n:run_examples", "run_example")
    '    '        WriteNodeListText(examplesRanSaved.ToArray, "//n:ran_examples", "ran_example")
    '    '        WriteNodeListText(examplesCompareSaved.ToArray, "//n:compare_examples", "compare_example")
    '    '        WriteNodeListText(examplesComparedSaved.ToArray, "//n:compared_examples", "compared_example")
    '    '    End If
    '    '    SaveXML(xmlPath)
    '    '    CloseXML()
    '    'End If
    '    '=== End XML Operations ===
    'End Sub

    ' ''' <summary>
    ' ''' Gathers a program's names for analysis default types from the XML file.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend Sub ConvertAnalysisDefaults()
    '    'Initializization XML Properties

    '    '=== XML Operations ===
    '    'Try
    '    '    If InitializeXML(xmlPath) Then
    '    '        ReadDefaultAnalysisSettingsXmlNodes()
    '    '        CloseXML()
    '    '    End If
    '    'Catch ex As Exception
    '    '    RaiseEvent Log(New LoggerEventArgs(ex))
    '    'End Try
    '    '=== End XML Operations ===

    'End Sub

    ' ''' <summary>
    ' ''' Saves the Settings class values to the XML
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub SaveSettingsXML()
    '    Try
    '        'If InitializeXML(xmlPath) Then
    '        '    ReadWriteSettingsXML(False)
    '        '    CloseXML()
    '        'End If

    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Assigns the name corresponding to the general default types within the XML file to the appropriate class property.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub ReadDefaultAnalysisSettingsXmlNodes()
    '    'Dim pathNode As String

    '    'pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:process_options/n:process_" & processDefault
    '    'processSaved = ReadNodeText(pathNode, "name")

    '    'pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:solver_options/n:solver_" & solverDefault
    '    'solverSaved = ReadNodeText(pathNode, "name")

    '    'pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:bit_options/n:bit_" & bitTypeDefault
    '    'bitTypeSaved = ReadNodeText(pathNode, "name")
    'End Sub

    ' ''' <summary>
    ' ''' Reads from or writes to XML, with unique properties.
    ' ''' </summary>
    ' ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ' ''' <remarks></remarks>
    'Private Sub ReadWriteSettingsXmlNodes(ByVal read As Boolean)
    '    Dim pathNode As String
    '    Dim subPathNodes As List(Of String)
    '    Dim tempPath As String

    '    '= RegTest
    '    pathNode = "//n:regtest/n:regtest_name"
    '    If read Then regTestName = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(regTestName, pathNode, "")

    '    pathNode = "//n:regtest/n:regtest_directory"
    '    If read Then regTestDirectory = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(regTestDirectory, pathNode, "")

    '    pathNode = "//n:regtest/n:program_name"
    '    If read Then programName = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(programName, pathNode, "")

    '    pathNode = "//n:regtest/n:program_version_designation"
    '    If read Then versionDesignation = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(versionDesignation, pathNode, "")

    '    pathNode = "//n:regtest/n:program_version"
    '    If read Then programVersion = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(programVersion, pathNode, "")

    '    pathNode = "//n:regtest/n:program_build"
    '    If read Then programBuild = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(programBuild, pathNode, "")


    '    pathNode = "//n:regtest/n:models_run_dir"
    '    If read Then initializeModelDestinationFolder = ConvertYesTrueBoolean(ReadNodeText(pathNode, "initialize"))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(initializeModelDestinationFolder, eCapitalization.alllower), pathNode, "initialize")


    '    pathNode = "//n:regtest/n:example_validation"
    '    If read Then exampleValidationFile = ReadNodeText(pathNode, "file_name")
    '    If Not read Then WriteNodeText(exampleValidationFile, pathNode, "file_name")

    '    If read Then
    '        exampleValidationDirectory = ReadNodeText(pathNode, "example_directory")
    '        If AbsolutePath(exampleValidationDirectory, , , True) Then
    '            If Not IO.Directory.Exists(exampleValidationDirectory) Then exampleValidationDirectory = ""
    '        Else
    '            exampleValidationDirectory = ""
    '        End If
    '    ElseIf Not read Then
    '        tempPath = exampleValidationDirectory
    '        RelativePath(tempPath)

    '        WriteNodeText(tempPath, pathNode, "example_directory")
    '    End If


    '    pathNode = "//n:regtest/n:example_update_results"
    '    If read Then exampleUpdateFile = ReadNodeText(pathNode, "file_name")
    '    If Not read Then WriteNodeText(exampleUpdateFile, pathNode, "file_name")

    '    If read Then
    '        exampleUpdateDirectory = ReadNodeText(pathNode, "example_directory")
    '        If AbsolutePath(exampleUpdateDirectory, , , True) Then
    '            If Not IO.Directory.Exists(exampleUpdateDirectory) Then exampleUpdateDirectory = ""
    '        Else
    '            exampleUpdateDirectory = ""
    '        End If
    '    ElseIf Not read Then
    '        tempPath = exampleUpdateDirectory
    '        RelativePath(tempPath)

    '        WriteNodeText(tempPath, pathNode, "example_directory")
    '    End If


    '    pathNode = "//n:csitester/n:tester_destination_directory"
    '    If read Then
    '        testerDestinationDir = ReadNodeText(pathNode, "")
    '        If AbsolutePath(testerDestinationDir, , , True) Then
    '            If Not IO.Directory.Exists(testerDestinationDir) Then
    '                testerDestinationDir = dirTesterDestinationDirDefault
    '            End If
    '        Else
    '            testerDestinationDir = dirTesterDestinationDirDefault
    '        End If
    '    ElseIf Not read Then
    '        tempPath = testerDestinationDir
    '        RelativePath(tempPath)

    '        WriteNodeText(tempPath, pathNode)
    '    End If

    '    pathNode = "//n:csitester/n:tester_level"
    '    If read Then
    '        csiTesterlevel = ConvertStringToEnumByDescription(ReadNodeText(pathNode, ""), eCSiTesterLevel.Published)
    '    End If
    '    If Not read Then
    '        WriteNodeText(GetEnumDescription(csiTesterlevel), pathNode, "")
    '    End If

    '    pathNode = "//n:csitester/n:csitester_path"
    '    If read Then
    '        csiTesterPath = ReadNodeText(pathNode, "")
    '        AbsolutePath(csiTesterPath)
    '    ElseIf Not read Then
    '        tempPath = csiTesterPath
    '        RelativePath(tempPath, True)

    '        WriteNodeText(tempPath, pathNode, "")
    '    End If

    '    pathNode = "//n:csitester/n:seed_files_path"
    '    If read Then
    '        seedPath = ReadNodeText(pathNode, "")
    '        AbsolutePath(seedPath)
    '    ElseIf Not read Then
    '        tempPath = seedPath
    '        RelativePath(tempPath, True)

    '        WriteNodeText(tempPath, pathNode, "")
    '    End If

    '    '== Analysis Settings
    '    pathNode = "//n:csitester/n:analysis_settings/n:solver"
    '    If read Then solverSaved = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(solverSaved, pathNode, "")

    '    pathNode = "//n:csitester/n:analysis_settings/n:process"
    '    If read Then processSaved = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(processSaved, pathNode, "")

    '    pathNode = "//n:csitester/n:analysis_settings/n:bit_type"
    '    If read Then bitTypeSaved = ReadNodeText(pathNode, "")
    '    If Not read Then WriteNodeText(bitTypeSaved, pathNode, "")

    '    pathNode = "//n:csitester/n:delete_files/n:delete_after_run"
    '    If read Then deleteAnalysisFilesStatus = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(deleteAnalysisFilesStatus, eCapitalization.alllower), pathNode, "")

    '    pathNode = "//n:csitester/n:delete_files/n:extra_log_warning"
    '    If read Then deleteAnalysisFilesLogWarning = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(deleteAnalysisFilesLogWarning, eCapitalization.alllower), pathNode, "")

    '    pathNode = "//n:csitester/n:delete_files/n:extra_exported_tables"
    '    If read Then deleteAnalysisFilesTables = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(deleteAnalysisFilesTables, eCapitalization.alllower), pathNode, "")

    '    pathNode = "//n:csitester/n:delete_files/n:extra_model_text"
    '    If read Then deleteAnalysisFilesModelText = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(deleteAnalysisFilesModelText, eCapitalization.alllower), pathNode, "")

    '    pathNode = "//n:csitester/n:delete_files/n:extra_all"
    '    If read Then deleteAnalysisFilesAll = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(deleteAnalysisFilesAll, eCapitalization.alllower), pathNode, "")



    '    pathNode = "//n:csitester/n:analysis_files_present"
    '    If read Then analysisFilesPresent = ConvertYesTrueBoolean(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(ConvertYesTrueString(analysisFilesPresent, eCapitalization.alllower), pathNode, "")



    '    '== Main Gridview
    '    pathNode = "//n:csitester/n:gridview_main"
    '    If read Then allTabsSelectRun = ConvertTrueTrueBoolean(ReadNodeText(pathNode, "all_tabs_select_run"))
    '    If Not read Then WriteNodeText(ConvertTrueTrueString(allTabsSelectRun, eCapitalization.alllower), pathNode, "all_tabs_select_run")

    '    pathNode = "//n:csitester/n:gridview_main"
    '    If read Then allTabsSelectCompare = ConvertTrueTrueBoolean(ReadNodeText(pathNode, "all_tabs_select_compare"))
    '    If Not read Then WriteNodeText(ConvertTrueTrueString(allTabsSelectCompare, eCapitalization.alllower), pathNode, "all_tabs_select_compare")

    '    pathNode = "//n:csitester/n:gridview_main"
    '    If read Then singleTab = ConvertTrueTrueBoolean(ReadNodeText(pathNode, "single_tab"))
    '    If Not read Then WriteNodeText(ConvertTrueTrueString(singleTab, eCapitalization.alllower), pathNode, "single_tab")

    '    '== Table Export Settings
    '    pathNode = "//n:csitester/n:table_export_settings"

    '    If read Then outputSettingsUsedAll = ConvertTrueTrueBoolean(ReadNodeText(pathNode, "output_settings_used_all"))
    '    If Not read Then WriteNodeText(ConvertTrueTrueString(outputSettingsUsedAll, eCapitalization.alllower), pathNode, "output_settings_used_all")

    '    If read Then tableExportFileExtensionAll = ReadNodeText(pathNode, "file_extension_all")
    '    If Not read Then WriteNodeText(tableExportFileExtensionAll, pathNode, "file_extension_all")

    '    'Defaults
    '    '== Analysis Settings
    '    pathNode = "//n:defaults_released/n:analysis_settings/n:solver"
    '    If read Then solverDefault = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_released/n:analysis_settings/n:process"
    '    If read Then processDefault = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_released/n:analysis_settings/n:bit_type"
    '    If read Then bitTypeDefault = ReadNodeText(pathNode, "")

    '    'Defaults Program
    '    '= Program & Models Paths
    '    pathNode = "//n:defaults_" & programName & "/n:program_path"
    '    If read Then programPath = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:model_source_path"
    '    If read Then modelSourcePath = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:model_destination_path"
    '    If read Then modelDestinationPath = ReadNodeText(pathNode, "")

    '    '= Documentation
    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:analysis"
    '    If read Then documentsPathsAnalysis = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_steel_frame"
    '    If read Then documentsPathsDesignSteelFrame = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_concrete_frame"
    '    If read Then documentsPathsDesignConcreteFrame = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_shear_wall"
    '    If read Then documentsPathsDesignShearWall = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_composite_beam"
    '    If read Then documentsPathsDesignCompositeBeam = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_composite_column"
    '    If read Then documentsPathsDesignCompositeColumn = ReadNodeText(pathNode, "")

    '    pathNode = "//n:defaults_" & programName & "/n:documentation_path/n:design_slab"
    '    If read Then documentsPathsDesignSlab = ReadNodeText(pathNode, "")

    '    '= Command Line Parameters
    '    '== Analysis
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:run_analysis"
    '    If read Then commandRunAnalysis = ReadNodeText(pathNode, "")

    '    '=== Process
    '    subPathNodes = New List(Of String)
    '    subPathNodes.Add("key")
    '    subPathNodes.Add("command")
    '    subPathNodes.Add("name")

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:process_options/n:process_same"
    '    If read Then
    '        commandProcessSame = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandProcessSame.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:process_options/n:process_separate"
    '    If read Then
    '        commandProcessSeparate = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandProcessSeparate.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:process_options/n:process_auto"
    '    If read Then
    '        commandProcessAuto = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandProcessAuto.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    '=== Solver
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:solver_options/n:solver_standard"
    '    If read Then
    '        commandSolverStandard = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandSolverStandard.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:solver_options/n:solver_advanced"
    '    If read Then
    '        commandSolverAdvanced = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandSolverAdvanced.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:solver_options/n:solver_multithreaded"
    '    If read Then
    '        commandSolverMultiThreaded = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandSolverMultiThreaded.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    '=== Bit
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:bit_options/n:bit_32"
    '    If read Then
    '        commandBit32 = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandBit32.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:analysis/n:bit_options/n:bit_64"
    '    If read Then
    '        commandBit64 = New ObservableCollection(Of String)
    '        For Each mySubNode As String In subPathNodes
    '            commandBit64.Add(ReadNodeText(pathNode, mySubNode))
    '        Next
    '    End If

    '    '== Design
    '    If read Then designTypeFiltered = New ObservableCollection(Of String)

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_steel"
    '    If read Then
    '        commandRunDesignSteel = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignSteel) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_concrete"
    '    If read Then
    '        commandRunDesignConcrete = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignConcrete) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_wall"
    '    If read Then
    '        commandRunDesignWall = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignWall) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_composite"
    '    If read Then
    '        commandRunDesignCompositeBeam = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignCompositeBeam) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_compositecolumn"
    '    If read Then
    '        commandRunDesignCompositeColumn = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignCompositeColumn) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_aluminum"
    '    If read Then
    '        commandRunDesignAluminum = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignAluminum) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:design/n:design_types/n:design_coldformed"
    '    If read Then
    '        commandRunDesignColdFormed = ReadNodeText(pathNode, "")
    '        If Not String.IsNullOrEmpty(commandRunDesignColdFormed) Then designTypeFiltered.Add(ReadNodeText(pathNode, "name"))
    '    End If

    '    '== Misc
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:misc/n:create_info_file"
    '    If read Then commandCreateInfoFile = ReadNodeText(pathNode, "")

    '    '== Save
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:save"
    '    If read Then commandSave = ReadNodeText(pathNode, "")

    '    '== Close
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:close"
    '    If read Then commandClose = ReadNodeText(pathNode, "")

    '    '== Batch Run Flag
    '    pathNode = "//n:defaults_" & programName & "/n:command_line/n:batch_run"
    '    If read Then commandBatchRun = ReadNodeText(pathNode, "")

    '    '=Programs
    '    pathNode = "//n:examples/n:id_settings/n:id_integer_length"
    '    If read Then modelIDIntegerLength = myCInt(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(CStr(modelIDIntegerLength), pathNode, "")

    '    pathNode = "//n:examples/n:id_settings/n:sub_id_integer_length"
    '    If read Then modelSubIDIntegerLength = myCInt(ReadNodeText(pathNode, ""))
    '    If Not read Then WriteNodeText(CStr(modelSubIDIntegerLength), pathNode, "")

    'End Sub

    ' ''' <summary>
    ' ''' Reads from or writes to XML, with properties lists.
    ' ''' </summary>
    ' ''' <param name="read">Specify whether to read values from XML or write values to XML.</param>
    ' ''' <remarks></remarks>
    'Private Sub ReadWriteSettingsXmlList(ByVal read As Boolean)
    '    'Reads & writes open-ended lists in the XML
    '    Dim pathNode As String

    '    Try
    '        pathNode = "//n:tests_to_run"
    '        If read Then
    '            listTestsToRun = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listTestsToRun)
    '        End If

    '        pathNode = "//n:distrib_test_sources"
    '        If read Then
    '            listDistributedTestSources = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listDistributedTestSources)
    '        End If

    '        pathNode = "//n:copy_model_options"
    '        If read Then
    '            listCopyModelOptions = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listCopyModelOptions)
    '        End If

    '        pathNode = "//n:attributes/n:status"
    '        If read Then
    '            listStatus = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listStatus)
    '        End If

    '        pathNode = "//n:attributes/n:documentation_status"
    '        If read Then
    '            listDocumentationStatus = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listDocumentationStatus)
    '        End If

    '        pathNode = "//n:example_paths"
    '        If read Then
    '            examplePathsSaved = ReadNodeListPath(pathNode)
    '        ElseIf Not read Then
    '            WriteNodeListPath(pathNode, "example_path", examplePathsSaved)
    '        End If

    '        pathNode = "//n:run_examples"
    '        If read Then
    '            examplesRunSaved = ReadUniqueList(pathNode)
    '        ElseIf Not read Then
    '            WriteUniqueList(pathNode, "run_example", examplesRunSaved)
    '        End If

    '        pathNode = "//n:ran_examples"
    '        If read Then
    '            examplesRanSaved = ReadUniqueList(pathNode)
    '        ElseIf Not read Then
    '            WriteUniqueList(pathNode, "ran_example", examplesRanSaved)
    '        End If

    '        pathNode = "//n:compare_examples"
    '        If read Then
    '            examplesCompareSaved = ReadUniqueList(pathNode)
    '        ElseIf Not read Then
    '            WriteUniqueList(pathNode, "compare_example", examplesCompareSaved)
    '        End If

    '        pathNode = "//n:compared_examples"
    '        If read Then
    '            examplesComparedSaved = ReadUniqueList(pathNode)
    '        ElseIf Not read Then
    '            WriteUniqueList(pathNode, "compared_example", examplesComparedSaved)
    '        End If

    '        If Not csiTesterlevel = eCSiTesterLevel.Published Then          'Otherwise skipped, as this node is removed from the XML that ships
    '            pathNode = "//n:tester_levels"
    '            If read Then
    '                listTesterLevels = New ObservableCollection(Of String)
    '                ReadNodeListText(pathNode, listTesterLevels)
    '            End If
    '        End If

    '        pathNode = "//n:programs"
    '        If read Then
    '            ReadNodeListText(pathNode, listPrograms)
    '        End If

    '        'Defaults Program
    '        '= File Types
    '        pathNode = "//n:defaults_" & programName & "/n:file_types"
    '        If read Then
    '            listFileTypes = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listFileTypes)
    '        End If

    '        '= Version Import Tags
    '        pathNode = "//n:defaults_" & programName & "/n:version_import_tags"
    '        If read Then
    '            listImportTags = New ObservableCollection(Of String)
    '            listImportTagVersions = New ObservableCollection(Of String)
    '            ReadNodeListText(pathNode, listImportTags)
    '            ReadNodeListText(pathNode, listImportTagVersions, "version_enacted")
    '        End If
    '    Catch ex As Exception
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try
    'End Sub
#End Region

End Class