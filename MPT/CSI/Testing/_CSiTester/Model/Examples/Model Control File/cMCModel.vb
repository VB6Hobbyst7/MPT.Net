Option Explicit On
Option Strict On

Imports System.Xml
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.IO

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cXmlReadWriteModelControl

Imports CSiTester.cPathModel
Imports CSiTester.cPathModelControl
Imports CSiTester.cKeywordTags
Imports CSiTester.cFileAttachment
Imports CSiTester.cExample
Imports CSiTester.cMCAttachments
Imports CSiTester.cMCNameSyncer
Imports CSiTester.cMCModelID

''' <summary>
''' Class that serves as a virtual copy of the model control XML file.
''' </summary>
''' <remarks></remarks>
Public Class cMCModel
    Inherits PropertyChanger
    Implements ICloneable
    Implements IComparable(Of cMCModel)
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Event Handlers"

    Private Sub SetMultiModelKeywords(sender As Object, e As PropertyChangedEventArgs) Handles _ID.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _ID.multiModelType) Then
            UpdateMultiModelKeyWords()
        ElseIf e.PropertyName = NameOfProp(Function() _ID.exampleName) Then
            UpdateMultiModelKeyWords()
            mcFile.PathModelControl.SetNewPath()
        End If
    End Sub

    Private Sub SyncSecondaryID(sender As Object, e As PropertyChangedEventArgs) Handles _modelFile.PropertyChanged
        SyncSecondaryIDName()
    End Sub

    Private Sub SyncOutputSettingsAttachments(sender As Object, e As PropertyChangedEventArgs) Handles _outputSettingsFile.PropertyChanged
        If e.PropertyName = NameOfProp(Function() outputSettingsFile.pathDestination) Then
            If e.PropertyName = NameOfProp(Function() outputSettingsFile.pathDestination.fileName) Then
                ' TODO: Update attachments
            End If
        End If
    End Sub
#End Region

#Region "Enumerations"
    ''' <summary>
    ''' Specifies the folder structure type that the model control file is to be stored in.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eFolderStructure
        <Description("error")> enumError = 0
        <Description("Not Specified")> NotSpecified
        ''' <summary>
        ''' The example files are all at the same directory path as the model file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Flattened")> Flattened
        ''' <summary>
        ''' The example files are organized within a hierarchical folder structure beneath a parent directory that is named after the example file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Database")> Database
    End Enum

    ' NOTUSED
    ''' <summary>
    ''' The file type associated with the example.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eExampleFileType
        <Description("error")> enumError = 0
        ''' <summary>
        ''' Any image file associated with and accessible from the model control. 
        ''' The following formats are supported: *.*
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Image File")> image
        ''' <summary>
        ''' Any file to be kept loosely associated to and accessible from the model control. 
        ''' The following formats are supported: *.*
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Attachment File")> attachment
        ''' <summary>
        ''' Any file that is required for the model file to run properly, such as ground motion data. 
        ''' These files are kept in sync with the model location.
        ''' The following formats are supported: *.*
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Supporting File")> supportingFile
        ''' <summary>
        ''' File that sets or overwrites table export options in a model file, allowing programmatic setting and resetting of model files.
        ''' These files are only active when located with the model file, with a synced name of '[modelFileName]_outputsettings.xml'.
        ''' Currently only works with ETABS examples.
        ''' The following formats are supported: *.xml
        ''' </summary>
        ''' <remarks></remarks>
        <Description("OutputSettings File")> outputSettings
        ''' <summary>
        ''' Model file that is run by a CSi product to produce the results that are to be checked.
        ''' The following formats are supported, depending on what is allowed for a given program: (see settings *.xml file)
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Model File")> model
        ''' <summary>
        ''' The file that controls the example with regTest runs, and with directory setup and file management.
        ''' There is always one file associated with each model, but there might be multiple model control files and models that are part of one example.
        ''' The following formats are supported: *.xml, with a valid namespace.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Model Control File")> modelControl
    End Enum

    ''' <summary>
    ''' Specifies whether an example is being created or edited.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eExampleAction
        <Description("None")> None = 0
        ''' <summary>
        ''' Example is to be created. Directories and files might not exist.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Create")> Create
        ''' <summary>
        ''' An existing example is being edited.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Edit")> Edit
    End Enum

    ''' <summary>
    ''' Specifies in what state an example is in during creation or editing, hinting at further action.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eExampleStatus
        <Description("Unspecified")> Unspecified
        ''' <summary>
        ''' Certain required properties must still be set.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Incomplete")> Incomplete
        ''' <summary>
        ''' All required properties have been set.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Complete")> Complete
        ''' <summary>
        ''' Example is complete and has been saved since last changed.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Saved")> Saved
    End Enum

    ''' <summary>
    ''' Result type, which is used for instructions on how to gather results based on how regTest handles these various types.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eResultType
        ''' <summary>
        ''' Each table entry is compared against a benchmark.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("None")> none
        ''' <summary>
        ''' Each table entry is compared against a benchmark.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Regular")> regular
        ''' <summary>
        ''' Table entries are combined in a defined mathematical function, the result of which is compared against a benchmark.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Post-Processed")> postProcessed
        ''' <summary>
        ''' Table entries are further combined into calculations in Excel, the result of which is compared against a benchmark.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Excel Calculated")> excelCalculated
    End Enum
#End Region

#Region "Properties: Private"
    Private _xmlDoc As XmlDocument
    'Private _adapter As New cModelControlAdapter
#End Region

#Region "Properties: General"
    Private _isFromSeedFile As Boolean
    ''' <summary>
    ''' True, if the object was generated from a seed file.
    ''' False, if the object was read from any other model control file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property isFromSeedFile As Boolean
        Get
            Return (StringsMatch(mcFile.pathSource.fileNameWithExtension, FILENAME_SEED_XML))
        End Get
    End Property

    ' Objects Corresponding To Files
    Private WithEvents _mcFile As New cFileModelControl
    ''' <summary>
    ''' Path to the location of the model control XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property mcFile As cFileModelControl
        Get
            Return _mcFile
        End Get
    End Property

    Private _programControl As New cProgramControl
    ''' <summary>
    ''' Program control object that contains general information about a model file and the exported table data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property programControl As cProgramControl
        Set(ByVal value As cProgramControl)
            _programControl = value
            RaisePropertyChanged(Function() Me.programControl)
        End Set
        Get
            Return _programControl
        End Get
    End Property

    Private WithEvents _outputSettingsFile As New cFileOutputSettings
    ''' <summary>
    ''' If this is an ETABS example, the corresponding outputSettings object is stored here.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property outputSettingsFile As cFileOutputSettings
        Set(ByVal value As cFileOutputSettings)
            If targetProgram.primary = eCSiProgram.ETABS Then
                _outputSettingsFile = value
                RaisePropertyChanged(Function() Me.outputSettingsFile)
            End If
        End Set
        Get
            Return _outputSettingsFile
        End Get
    End Property

    ' Directory Structure and File Names
    Private _folderStructure As eFolderStructure = eFolderStructure.NotSpecified
    ''' <summary>
    ''' Program control object that contains general information about a model file and the exported table data.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property folderStructure() As eFolderStructure
        Get
            Return _folderStructure
        End Get
        Set(ByVal value As eFolderStructure)
            If Not _folderStructure = value Then
                _folderStructure = value
                RaisePropertyChanged("folderStructure")
            End If
        End Set
    End Property

    Private WithEvents _namesSynced As New cMCNameSyncer
    ''' <summary>
    ''' Contains the syncing schema of the example between the model file name, model control file name, and secondary ID name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property namesSynced As cMCNameSyncer
        Get
            Return _namesSynced
        End Get
    End Property

    ''' <summary>
    ''' States in what way the model control secondary id is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property secondaryIDSynced As eNameSync
        Set(ByVal value As eNameSync)
            If Not namesSynced.mcSecondaryIDSynced = value Then
                namesSynced.mcSecondaryIDSynced = value
                If (value = eNameSync.ModelControlID AndAlso
                    Not secondaryID = ID.idComposite) Then

                    secondaryID = ID.idComposite
                ElseIf (value = eNameSync.ModelFileName AndAlso
                        Not secondaryID = modelFile.pathDestination.fileName) Then

                    secondaryID = modelFile.pathDestination.fileName
                End If
            End If
        End Set
        Get
            Return _namesSynced.mcSecondaryIDSynced
        End Get
    End Property

    ''' <summary>
    ''' If true, the model file is old and will undergo an import process that will change the file name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property importedModel() As Boolean
        Set(ByVal value As Boolean)
            If Not _modelFile.PathModelDestination.importedModel = value Then
                _modelFile.PathModelDestination.importedModel = value
                RaisePropertyChanged(Function() Me.importedModel)

                If _modelFile.PathModelDestination.importedModel Then
                    UpdateUniqueKeywordTag(importedModelVersion, TAG_KEYWORD_IMPORTED)
                Else
                    keywords.RemoveByTag(TAG_KEYWORD_IMPORTED)
                End If
            End If
        End Set
        Get
            Return _modelFile.PathModelDestination.importedModel
        End Get
    End Property

    ''' <summary>
    ''' The release version from which the imported model was saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property importedModelVersion As String
        Set(ByVal value As String)
            If Not _modelFile.PathModelDestination.importedModelVersion = value Then
                _modelFile.PathModelDestination.importedModelVersion = value
                RaisePropertyChanged(Function() Me.importedModelVersion)
            End If
        End Set
        Get
            Return _modelFile.PathModelDestination.importedModelVersion
        End Get
    End Property

    ' Classifications
    Private _exampleType As String
    ''' <summary>
    ''' Classification of the example primarily demonstrating confirmation of analysis, design, or both.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property exampleType As String                                   'TODO: Change to enum?
        Get
            Return _exampleType
        End Get
        Set(value As String)
            If Not _exampleType = value Then
                _exampleType = value
                If Not String.IsNullOrEmpty(_exampleType) Then
                    UpdateExampleTypeKeywords(_exampleType)
                End If
            End If
        End Set
    End Property

    Private _analysisClass As String
    ''' <summary>
    ''' Groupings of analysis example types by element, such as 'frame', 'shell', etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property analysisClass As String                                 'TODO: Change to enums?
        Get
            Return _analysisClass
        End Get
        Set(value As String)
            If Not _analysisClass = value Then
                _analysisClass = value
                If Not String.IsNullOrEmpty(_analysisClass) Then
                    UpdateUniqueKeywordTag(_analysisClass, TAG_KEYWORD_ANALYSIS_CLASS)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Analysis cases tested in the example, such as 'Time History'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property analysisTypes As New ObservableCollection(Of String)   'TODO: Not used yet
    ''' <summary>
    ''' Element type tested in the example, such as 'Link' or 'Hinge'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property elementTypes As New ObservableCollection(Of String)    'TODO: Not used yet

    Private _codeRegion As String
    ''' <summary>
    ''' Overall region within which a particular code is used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property codeRegion As String                                    'TODO: Change to enum?
        Get
            Return _codeRegion
        End Get
        Set(value As String)
            If Not _codeRegion = value Then
                _codeRegion = value
                If Not String.IsNullOrEmpty(_codeRegion) Then
                    UpdateUniqueKeywordTag(_codeRegion, TAG_KEYWORD_CODE_REGION)
                Else
                    keywords.RemoveByTag(TAG_KEYWORD_CODE_REGION)
                End If
            End If
        End Set
    End Property

    Private _designClass As String
    ''' <summary>
    ''' Groupings of design example types, such as 'Bending Member' or 'Axial Section'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property designClass As String                                   'TODO: Change to list
        Get
            Return _designClass
        End Get
        Set(value As String)
            If Not _designClass = value Then
                _designClass = value
                If Not String.IsNullOrEmpty(_designClass) Then
                    UpdateUniqueKeywordTag(_designClass, TAG_KEYWORD_DESIGN_CLASS)
                Else
                    keywords.RemoveByTag(TAG_KEYWORD_DESIGN_CLASS)
                End If
            End If
        End Set
    End Property

    Private _designType As String
    ''' <summary>
    ''' Design types tested in the example, such as 'Steel Frame' or 'Shear Wall'. 
    ''' Needed for instructing the program to run the design. 
    ''' Most entries are generated based on if command line parameters allow the design to be called. 
    ''' Order of list is fixed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property designType As String                                    'TODO: Change to list (of enums?)
        Get
            Return _designType
        End Get
        Set(value As String)
            If Not _designType = value Then
                _designType = value
                If Not String.IsNullOrEmpty(_designType) Then
                    UpdateUniqueKeywordTag(_designType, TAG_KEYWORD_DESIGN_TYPE)
                Else
                    keywords.RemoveByTag(TAG_KEYWORD_DESIGN_TYPE)
                End If
            End If
        End Set
    End Property


    ' Object State
    Private _changedSinceSave As Boolean = True
    ''' <summary>
    ''' If true, some property of the class has been changed since the class was last saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property changedSinceSave As Boolean
        Set(ByVal value As Boolean)
            If Not _changedSinceSave = value Then
                _changedSinceSave = value
                RaisePropertyChanged(Function() Me.changedSinceSave)
            End If
        End Set
        Get
            Return _changedSinceSave
        End Get
    End Property

    Private _updateResultsFromExcel As New Boolean
    ''' <summary>
    ''' If true, then when the class is saved, regTest will be run to update the results based on the Excel file provided in the Excel result object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property updateResultsFromExcel As Boolean
        Set(ByVal value As Boolean)
            If Not _updateResultsFromExcel = value Then
                _updateResultsFromExcel = value
                RaisePropertyChanged(Function() Me.updateResultsFromExcel)
            End If
        End Set
        Get
            Return _updateResultsFromExcel
        End Get
    End Property
#End Region

#Region "Properties: XML File"
    Private _xmlSchemaVersion As String
    ''' <summary>
    ''' Schema version that the control XML file was last updated to.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlSchemaVersion As String
        Set(ByVal value As String)
            If Not _xmlSchemaVersion = value Then
                _xmlSchemaVersion = value
                RaisePropertyChanged(Function() Me.xmlSchemaVersion)
            End If
        End Set
        Get
            Return _xmlSchemaVersion
        End Get
    End Property

    Private _isPublic As Boolean
    ''' <summary>
    ''' Indicates whether the model can be publicly published and does not contain any proprietary or sensitive information.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isPublic As Boolean
        Set(ByVal value As Boolean)
            If Not _isPublic = value Then
                _isPublic = value
                RaisePropertyChanged(Function() Me.isPublic)
            End If
        End Set
        Get
            Return _isPublic
        End Get
    End Property

    Private _isBug As Boolean
    ''' <summary>
    ''' Indicates whether or not the model demonstrates a known bug.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isBug As Boolean
        Set(ByVal value As Boolean)
            If Not _isBug = value Then
                _isBug = value
                RaisePropertyChanged(Function() Me.isBug)
            End If
        End Set
        Get
            Return _isBug
        End Get
    End Property

    Private _statusExample As String
    ''' <summary>
    ''' Status of the model as it is developed into an example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property statusExample As String
        Set(ByVal value As String)
            If Not _statusExample = value Then
                _statusExample = value
                RaisePropertyChanged(Function() Me.statusExample)
            End If
        End Set
        Get
            Return _statusExample
        End Get
    End Property

    Private _statusDocumentation As String
    ''' <summary>
    ''' Status of the documentation associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property statusDocumentation As String
        Set(ByVal value As String)
            If Not _statusDocumentation = value Then
                _statusDocumentation = value
                RaisePropertyChanged(Function() Me.statusDocumentation)
            End If
        End Set
        Get
            Return _statusDocumentation
        End Get
    End Property

    Private _xmlns As String
    ''' <summary>
    ''' The XML namespace of the XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlns As String
        Set(ByVal value As String)
            If Not _xmlns = value Then
                _xmlns = value
                RaisePropertyChanged(Function() Me.xmlns)
            End If
        End Set
        Get
            Return _xmlns
        End Get
    End Property

    Private _xmlnsXSI As String
    ''' <summary>
    ''' The schema instance of the XMLNS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlnsXSI As String
        Set(ByVal value As String)
            If Not _xmlnsXSI = value Then
                _xmlnsXSI = value
                RaisePropertyChanged(Function() Me.xmlnsXSI)
            End If
        End Set
        Get
            Return _xmlnsXSI
        End Get
    End Property

    Private _xsiSchemaLocation As String
    ''' <summary>
    ''' The schema location of the xmnls:XSI
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xsiSchemaLocation As String
        Set(ByVal value As String)
            If Not _xsiSchemaLocation = value Then
                _xsiSchemaLocation = value
                RaisePropertyChanged(Function() Me.xsiSchemaLocation)
            End If
        End Set
        Get
            Return _xsiSchemaLocation
        End Get
    End Property

    Private WithEvents _ID As cMCModelID = New cMCModelID()
    ''' <summary>
    ''' Unique number corresponding to the [Example].[Model] file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ID As cMCModelID
        Set(ByVal value As cMCModelID)
            If Not _ID.Equals(value) Then
                _ID = value
                RaisePropertyChanged(Function() Me.ID)
            End If
        End Set
        Get
            Return _ID
        End Get
    End Property

    Private _secondaryID As String
    ''' <summary>
    ''' Unique name associated with the example. 
    ''' Usually the same as the model file name without the extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property secondaryID As String
        Set(ByVal value As String)
            If Not _secondaryID = value Then
                _secondaryID = value
                RaisePropertyChanged(Function() Me.secondaryID)
            End If
        End Set
        Get
            Return _secondaryID
        End Get
    End Property

    Private _title As String
    ''' <summary>
    ''' Title of the example associated with the control XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
        Set(ByVal value As String)
            If Not _title = value Then
                _title = value
                RaisePropertyChanged(Function() Me.title)
            End If
        End Set
        Get
            Return _title
        End Get
    End Property

    Private WithEvents _modelFile As New cFileModel
    ''' <summary>
    ''' Path to the model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property modelFile As cFileModel
        Get
            Return _modelFile
        End Get
    End Property

    Private WithEvents _dataSource As New cFileModelExportedTable
    ''' <summary>
    ''' Path to the database file used to establish results queries for examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property dataSource As cFileModelExportedTable
        Get
            Return _dataSource
        End Get
    End Property

    Private _commandLine As String
    ''' <summary>
    ''' Command line for command line calls unique to one example, such as calling for steel design.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property commandLine As String
        Set(ByVal value As String)
            If Not _commandLine = value Then
                _commandLine = value
                RaisePropertyChanged(Function() Me.commandLine)
            End If
        End Set
        Get
            Return _commandLine
        End Get
    End Property

    Private _description As String
    ''' <summary>
    ''' Description of the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property description As String
        Set(ByVal value As String)
            If Not _description = value Then
                _description = value
                RaisePropertyChanged(Function() Me.description)
            End If
        End Set
        Get
            Return _description
        End Get
    End Property

    Private _comments As String
    ''' <summary>
    ''' Comments related to regression testing, administration, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property comments As String
        Set(ByVal value As String)
            If Not _comments = value Then
                _comments = value
                RaisePropertyChanged(Function() Me.comments)
            End If
        End Set
        Get
            Return _comments
        End Get
    End Property

    Private _tests As New cMCTestTypes
    ''' <summary>
    ''' Applicable test types, between 'As-Is', or the two various bridge object tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tests As cMCTestTypes
        Set(ByVal value As cMCTestTypes)
            _tests = value
            RaisePropertyChanged(Function() Me.tests)
        End Set
        Get
            Return _tests
        End Get
    End Property

    Private _updates As New cMCUpdates
    ''' <summary>
    ''' Collection of data about any updates performed on the example, such as updating benchmarks or changing the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property updates As cMCUpdates
        Get
            Return _updates
        End Get
    End Property

    Private _keywords As New cKeywords
    ''' <summary>
    ''' List of keywords used for classifying and referencing the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property keywords As cKeywords
        Set(ByVal value As cKeywords)
            _keywords = value
            RaisePropertyChanged(Function() Me.keywords)
        End Set
        Get
            Return _keywords
        End Get
    End Property

    Private _classification As New cMCClassification
    ''' <summary>
    ''' Overall and secondary classifications of the example, such as 'Published Verification' of the type 'ETABS Analysis Verification Suite'.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property classification As cMCClassification
        Set(ByVal value As cMCClassification)
            _classification = value
            RaisePropertyChanged(Function() Me.classification)
        End Set
        Get
            Return _classification
        End Get
    End Property

    Private _images As New cMCAttachments(Me)
    ''' <summary>
    ''' A collection of classes containing a titles and paths to image files associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property images As cMCAttachments
        Set(ByVal value As cMCAttachments)
            _images = value
            RaisePropertyChanged(Function() Me.images)
        End Set
        Get
            Return _images
        End Get
    End Property

    Private _attachments As New cMCAttachments(Me)
    ''' <summary>
    ''' A collection of classes containing a titles and paths to attachment files associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property attachments As cMCAttachments
        Set(ByVal value As cMCAttachments)
            _attachments = value
            RaisePropertyChanged(Function() Me.attachments)
        End Set
        Get
            Return _attachments
        End Get
    End Property

    Private _supportingFiles As New cMCAttachments(Me)
    ''' <summary>
    ''' A collection of classes containing a titles and paths to supporting files associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property supportingFiles As cMCAttachments
        Set(ByVal value As cMCAttachments)
            _supportingFiles = value
            RaisePropertyChanged(Function() Me.supportingFiles)
        End Set
        Get
            Return _supportingFiles
        End Get
    End Property

    Private _links As New cMCLinks
    ''' <summary>
    ''' A collection of classes containing a titles and URLs of links associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property links As cMCLinks
        Set(ByVal value As cMCLinks)
            _links = value
            RaisePropertyChanged(Function() Me.links)
        End Set
        Get
            Return _links
        End Get
    End Property

    Private _incidents As New cMCIncidentsTickets
    ''' <summary>
    ''' List of incident numbers associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property incidents As cMCIncidentsTickets
        Set(ByVal value As cMCIncidentsTickets)
            _incidents = value
            RaisePropertyChanged(Function() Me.incidents)
        End Set
        Get
            Return _incidents
        End Get
    End Property

    Private _tickets As New cMCIncidentsTickets
    ''' <summary>
    ''' List of ticket numbers associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tickets As cMCIncidentsTickets
        Set(ByVal value As cMCIncidentsTickets)
            _tickets = value
            RaisePropertyChanged(Function() Me.tickets)
        End Set
        Get
            Return _tickets
        End Get
    End Property

    Private _program As New cMCBenchmarkRef
    ''' <summary>
    ''' Benchmark reference of the program and version used for establishing or updating the benchmark values.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property program As cMCBenchmarkRef
        Set(ByVal value As cMCBenchmarkRef)
            _program = value
            RaisePropertyChanged(Function() Me.program)
        End Set
        Get
            Return _program
        End Get
    End Property

    Private _targetProgram As New cMCTargetPrograms
    ''' <summary>
    ''' Program for which the model is to be used. There can be more than one program, so this is taken to be the first one specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property targetProgram As cMCTargetPrograms
        Set(ByVal value As cMCTargetPrograms)
            _targetProgram = value
            RaisePropertyChanged(Function() Me.targetProgram)

            With _modelFile.PathModelDestination
                If .program = eCSiProgram.None Then
                    .program = targetProgram.primary
                End If
            End With
        End Set
        Get
            Return _targetProgram
        End Get
    End Property

    Private _author As New cMCAuthor
    ''' <summary>
    ''' Person and/or company associated with the example creation.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property author As cMCAuthor
        Set(ByVal value As cMCAuthor)
            _author = value
            RaisePropertyChanged(Function() Me.author)
        End Set
        Get
            Return _author
        End Get
    End Property

    Private _exampleDate As New cMCDate
    ''' <summary>
    ''' Date on which the example was created.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleDate As cMCDate
        Set(ByVal value As cMCDate)
            _exampleDate = value
            RaisePropertyChanged(Function() Me.exampleDate)
        End Set
        Get
            Return _exampleDate
        End Get
    End Property

    Private _runTime As Double = 1
    ''' <summary>
    ''' Estimated total time to run a single model. [min]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property runTime As Double
        Set(ByVal value As Double)
            If Not _runTime = value Then
                _runTime = value
                RaisePropertyChanged(Function() Me.runTime)
            End If
        End Set
        Get
            Return _runTime
        End Get
    End Property

    Private _results As New cMCResults
    ''' <summary>
    ''' A collection of results that are checked for the example to determine if results have changed, and how closely they match independent values.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property results As cMCResults
        Get
            Return _results
        End Get
    End Property

    Private _resultsExcel As New cMCResultsExcel(Me)
    ''' <summary>
    ''' A path to an Excel file that is checked for the example to determine if results have changed, and how closely they match independent values.
    ''' Results may be calculated in Excel to get values not able to be extracted directly from the analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property resultsExcel As cMCResultsExcel
        Get
            Return _resultsExcel
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Generates a model control XML class.
    ''' If a path is provided, the class is populated by reading from an existing XML file at the specified location
    ''' </summary>
    ''' <param name="p_pathMCFile">Path to the model control XML file from which the class is generated.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_pathMCFile As String = "",
                   Optional ByVal p_alertInvalidPath As Boolean = False)

        p_pathMCFile = ValidatedPath(p_pathMCFile, p_alertInvalidPath)

        InitializeFileObjects(p_pathMCFile)
        FillFileObjects(p_pathMCFile)
        ReInitializeModelFile(modelFile.pathDestination.path)

        SetAdditionalData()
    End Sub

    ''' <summary>
    ''' Initializes the various objects related to the model control file, including setting them up to reference the current class.
    ''' </summary>
    ''' <param name="p_pathMCFile">Path to the model control XML file from which the class is generated.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFileObjects(Optional ByVal p_pathMCFile As String = "")
        Try
            _mcFile = New cFileModelControl(p_pathFile:=p_pathMCFile, p_bindTo:=Me)
            _ID = New cMCModelID(p_bindTo:=Me)
            _modelFile = New cFileModel(p_bindTo:=Me)
            _dataSource = New cFileModelExportedTable(p_bindTo:=Me)

            attachments.attachmentType = eAttachmentType.attachments
            images.attachmentType = eAttachmentType.images
            supportingFiles.attachmentType = eAttachmentType.supportingFiles
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Fills file object properties from files, and does final initializations based on the data read.
    ''' </summary>
    ''' <param name="p_pathMCFile">Path to the model control XML file from which the class is generated.</param>
    ''' <remarks></remarks>
    Private Sub FillFileObjects(Optional ByVal p_pathMCFile As String = "")
        Try
            If String.IsNullOrEmpty(p_pathMCFile) Then
                ' Initialize from seed
                InitializeDataFromFilePath(seedPathMC)
                ReadFile(seedPathMC)
                _outputSettingsFile = New cFileOutputSettings(p_bindTo:=Me)
            Else
                ' Initialize from existing file if possible
                InitializeDataFromFilePath(p_pathMCFile)
                ReadFile(p_pathMCFile)
                _outputSettingsFile = New cFileOutputSettings(p_bindTo:=Me)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Re-initializes the model file object with the provided file path as the source file path.
    ''' </summary>
    ''' <param name="p_pathModelFile">Path to the existing source model file.</param>
    ''' <remarks></remarks>
    Friend Sub ReInitializeModelFile(ByVal p_pathModelFile As String)
        Try
            If p_pathModelFile Is Nothing Then Exit Sub
            'Set model file source & related names
            _modelFile = New cFileModel(p_pathModelFile, p_bindTo:=Me)
            SyncSecondaryIDName()

            'Set related file destinations
            mcFile.PathModelControl.SyncDestinationPathWithModelFile()
            mcFile.PathModelControl.SetNewPath()
            outputSettingsFile.PathOutputSettings.UpdateFileName()

            ReInitializeDataSource()

            ' Sets property based on reinitialization being from an existing saved file
            If (mcFile.pathSource.isValidPath AndAlso
                Not isFromSeedFile) Then
                _changedSinceSave = False

            Else
                _changedSinceSave = True
            End If
        Catch ex As Exception
            csiLogger.Equals(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Re-initializes the model file source path, if it is blank.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReInitializeDataSource()
        If String.IsNullOrEmpty(_dataSource.pathDestination.fileName) Then
            _dataSource = New cFileModelExportedTable(p_bindTo:=Me)
        End If
    End Sub

    ''' <summary>
    ''' Sets other data in the class based on the provided path to the model control XML file..
    ''' </summary>
    ''' <param name="p_pathMCFile">Path to the model control XML  file from which the class is generated.</param>
    ''' <remarks></remarks>
    Private Sub InitializeDataFromFilePath(ByVal p_pathMCFile As String)
        mcFile.pathDestination.SetProperties(p_pathMCFile)
        If IO.File.Exists(p_pathMCFile) Then SetFolderStructureFromFile()
    End Sub

    ''' <summary>
    ''' Sets other properties based on the current state of the file after reading the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetAdditionalData()
        If IsModelNameSameAsID() Then
            modelFile.PathModelDestination.nameSynced = eNameSync.ModelControlID
        Else
            modelFile.PathModelDestination.nameSynced = eNameSync.Custom
        End If

        If (secondaryID = "0" OrElse secondaryID = "") Then SyncSecondaryIDName()

        If tests.Count = 0 Then tests.Add(eTestType.runAsIs)
    End Sub
#End Region

#Region "Methods: Overoads/Overrides/Implements"
    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCModel

        Try
            With myClone
                ' Properties XML File
                ._attachments = CType(attachments.Clone(p_bindTo:=myClone), cMCAttachments)
                ._author = CType(author.Clone, cMCAuthor)

                ._classification = CType(classification.Clone, cMCClassification)
                ._commandLine = commandLine
                ._comments = comments

                ._dataSource = CType(dataSource.Clone(p_bindTo:=myClone), cFileModelExportedTable)
                ._description = description

                ._exampleDate = CType(exampleDate.Clone, cMCDate)

                ._images = CType(images.Clone(p_bindTo:=myClone), cMCAttachments)
                ._incidents = CType(incidents.Clone, cMCIncidentsTickets)
                ._isBug = isBug
                ._isPublic = isPublic

                ._keywords = CType(keywords.Clone, cKeywords)

                ._links = CType(links.Clone, cMCLinks)

                ._ID = CType(ID.Clone(p_bindTo:=myClone), cMCModelID)

                ._modelFile = CType(modelFile.Clone(p_bindTo:=myClone), cFileModel)
                ._program = CType(program.Clone, cMCBenchmarkRef)

                ._results = CType(results.Clone, cMCResults)
                ._resultsExcel = CType(resultsExcel.Clone, cMCResultsExcel)
                ._runTime = runTime

                ._secondaryID = secondaryID
                ._statusDocumentation = statusDocumentation
                ._statusExample = statusExample
                ._supportingFiles = CType(supportingFiles.Clone(p_bindTo:=myClone), cMCAttachments)

                ._targetProgram = CType(targetProgram.Clone, cMCTargetPrograms)
                ._tests = CType(tests.Clone, cMCTestTypes)
                ._tickets = CType(tickets.Clone, cMCIncidentsTickets)
                ._title = title

                ._updates = CType(updates.Clone, cMCUpdates)

                ._xmlns = xmlns
                ._xmlnsXSI = xmlnsXSI
                ._xmlSchemaVersion = xmlSchemaVersion
                ._xsiSchemaLocation = xsiSchemaLocation

                ' General Properties
                '    Keyword-Related
                ._analysisClass = analysisClass
                If analysisTypes IsNot Nothing Then
                    ._analysisTypes = New ObservableCollection(Of String)
                    For Each analysisType As String In analysisTypes
                        ._analysisTypes.Add(analysisType)
                    Next
                End If

                ._codeRegion = codeRegion

                If elementTypes IsNot Nothing Then
                    ._elementTypes = New ObservableCollection(Of String)
                    For Each elementType As String In elementTypes
                        ._elementTypes.Add(elementType)
                    Next
                End If
                ._exampleType = exampleType

                ._designClass = designClass
                ._designType = designType

                '   Objects
                ._outputSettingsFile = CType(outputSettingsFile.Clone(p_bindTo:=myClone), cFileOutputSettings)

                ._programControl = CType(programControl.Clone, cProgramControl)

                ._updateResultsFromExcel = updateResultsFromExcel

                ._mcFile = CType(mcFile.Clone(p_bindTo:=myClone), cFileModelControl)

                ._namesSynced = CType(namesSynced.Clone, cMCNameSyncer)

                '   State
                ._changedSinceSave = changedSinceSave

                ._folderStructure = folderStructure
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If p_object Is Nothing Then Return False
        If Not (TypeOf p_object Is cMCModel) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCModel = TryCast(p_object, cMCModel)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .author.Equals(author) Then Return False
            If Not .classification.Equals(classification) Then Return False
            If Not .commandLine = commandLine Then Return False
            If Not .comments = comments Then Return False
            'If Not .dataSource.Equals(dataSource) Then Return False
            If Not .description = description Then Return False
            If Not .exampleDate.Equals(exampleDate) Then Return False

            If Not .isBug = isBug Then Return False
            If Not .isPublic = isPublic Then Return False

            If Not .attachments.Equals(attachments) Then Return False
            If Not .supportingFiles.Equals(supportingFiles) Then Return False
            If Not .images.Equals(images) Then Return False

            If Not .incidents.Equals(incidents) Then Return False
            If Not .tickets.Equals(tickets) Then Return False

            If Not .keywords.Equals(keywords) Then Return False

            If Not .links.Equals(links) Then Return False

            If Not .updates.Equals(updates) Then Return False

            If Not .ID.Equals(ID) Then Return False

            If Not .modelFile.Equals(modelFile) Then Return False

            If Not .program.Equals(program) Then Return False

            If Not .results.Equals(results) Then Return False
            If Not .resultsExcel.Equals(resultsExcel) Then Return False

            If Not .runTime = runTime Then Return False
            If Not .secondaryID = secondaryID Then Return False
            If Not StringsMatch(.statusDocumentation, statusDocumentation) Then Return False
            If Not StringsMatch(.statusExample, statusExample) Then Return False

            If Not .targetProgram.Equals(targetProgram) Then Return False

            If Not .tests.Equals(tests) Then Return False

            If Not .title = title Then Return False

            If Not .xmlns = xmlns Then Return False
            If Not .xmlnsXSI = xmlnsXSI Then Return False
            If Not .xmlSchemaVersion = xmlSchemaVersion Then Return False
            If Not .xsiSchemaLocation = xsiSchemaLocation Then Return False

            'General Properties
            If Not .mcFile.Equals(mcFile) Then Return False

            If Not .exampleType = exampleType Then Return False
            If Not .analysisClass = analysisClass Then Return False
            If analysisTypes IsNot Nothing Then
                For Each analysisTypeOuter As String In .analysisTypes
                    isMatch = False
                    For Each analysisTypeInner As String In analysisTypes
                        If StringsMatch(analysisTypeOuter, analysisTypeInner) Then
                            isMatch = True
                            Exit For
                        End If
                    Next
                    If Not isMatch Then Return False
                Next
            End If
            If elementTypes IsNot Nothing Then
                For Each elementTypeOuter As String In .elementTypes
                    isMatch = False
                    For Each elementTypeInner As String In elementTypes
                        If StringsMatch(elementTypeOuter, elementTypeInner) Then
                            isMatch = True
                            Exit For
                        End If
                    Next
                    If Not isMatch Then Return False
                Next
            End If
            If Not StringsMatch(.codeRegion, codeRegion) Then Return False
            If Not StringsMatch(.designClass, designClass) Then Return False
            If Not StringsMatch(.designType, designType) Then Return False

            If Not .outputSettingsFile.Equals(outputSettingsFile) Then Return False

            If Not .namesSynced.Equals(namesSynced) Then Return False

            If Not .updateResultsFromExcel = updateResultsFromExcel Then Return False

            If Not .folderStructure = folderStructure Then Return False

            If Not .programControl.Equals(programControl) Then Return False

            If Not .changedSinceSave = changedSinceSave Then Return False
        End With

        Return True
    End Function

    ''' <summary>
    ''' Sorts first by group ID, then by sub-group ID.
    ''' </summary>
    ''' <param name="other"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CompareTo(other As cMCModel) As Integer Implements IComparable(Of cMCModel).CompareTo
        Return ID.idCompositeDecimal.CompareTo(other.ID.idCompositeDecimal)
    End Function

    Public Overrides Function ToString() As String
        Return (MyBase.ToString() & ": ID " & ID.idComposite & " - " & secondaryID)
    End Function

#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Finalizes preparation of the example and saves data to the Model Control file.
    ''' Other relevant files, such as attachments, images, Excel files, and outputSettings files are saved to the appropriate locations.
    ''' Returns an error message if any major action failed.
    ''' </summary>
    ''' <param name="p_throwExceptions">True: Exceptions will be thrown and must be handled elsewhere. False: Likely exceptions will be handled within the method.</param>
    ''' <param name="p_suppressMessages">True: No result messages will appear.</param>
    ''' <remarks></remarks>
    Friend Function SaveExampleToFiles(Optional p_throwExceptions As Boolean = False,
                                       Optional p_suppressMessages As Boolean = False) As Boolean
        Dim errorMessages As String = ""
        Dim status As frmLongListPrompt

        ' Confirm that the object is ready to be saved
        If Not RequiredDataFilled(True) Then
            errorMessages = "Some of the required data is incomplete."
            If p_throwExceptions Then
                Throw New ArgumentException(errorMessages)
            ElseIf Not p_suppressMessages Then
                status = New frmLongListPrompt(eMessageActionSets.OkOnly, "Example Not Saved", "Some of the required data is incomplete.", "", "", MessageBoxImage.Exclamation)
                Return False
            End If
        End If

        'Copy seed files, if necessary, and save to the file
        Try
            CopySaveModelControlFile()
        Catch ex As IOException
            If p_throwExceptions Then
                Throw New IOException(ex.Message)
            ElseIf Not p_suppressMessages Then
                status = New frmLongListPrompt(eMessageActionSets.OkOnly, "Example Not Saved", "", "", ex.Message, MessageBoxImage.Exclamation)
                status.Show()
                Return False
            End If
        End Try


        ' Copy existing files if specified.
        '   Core files
        Try
            CopyModelFile()
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try
        Try
            CopySupportingFiles()  'Outputsettings file is not included
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try
        Try
            CopyImages()
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try
        Try
            CopyAttachments()  'Outputsettings file is not included
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try

        '   Extra files
        Try
            CopySaveOutputSettingsFile()
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try
        Try
            CopyExcelFile()
        Catch ex As IOException
            errorMessages = AppendErrorMessage(errorMessages, ex.Message)
        End Try

        If String.IsNullOrEmpty(errorMessages) Then
            changedSinceSave = False
            If Not p_suppressMessages Then
                status = New frmLongListPrompt(eMessageActionSets.OkOnly, "Example Saved", "Example Saved", "", mcFile.pathDestination.path, MessageBoxImage.None)
                status.Show()
                Return True
            End If
        Else
            If p_throwExceptions Then
                Throw New IOException(errorMessages)
            ElseIf Not p_suppressMessages Then
                status = New frmLongListPrompt(eMessageActionSets.OkOnly, "Example Not Saved", "", "", errorMessages, MessageBoxImage.Exclamation)
                status.Show()
            End If
        End If
        Return False
    End Function

    ''' <summary>
    ''' Saves the data from the model control XML class that is in memory back into the XML file at the destination path.
    ''' </summary>
    ''' <param name="p_path">Path to the model control XML. 
    ''' If not specified, the current class XML path property is used.
    ''' If specified, the current class XML path property is updated to this.</param>
    ''' <remarks></remarks>
    Friend Function SaveFile(Optional ByVal p_path As String = "") As Boolean
        Try
            ' Set model control path
            If String.IsNullOrEmpty(p_path) Then
                p_path = mcFile.pathDestination.path
            ElseIf Not StringsMatch(p_path, mcFile.pathDestination.path) Then
                mcFile.pathDestination.SetProperties(p_path)
            End If
            If Not IO.File.Exists(mcFile.pathDestination.path) Then Return False

            SetKeywordsData(p_read:=False)
            SetCommandLineData()

            Return SaveXMLFile(Me)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Checks whether the minimum required information has been specified for creating model control files.
    ''' Updates the status based on the current state.
    ''' </summary>
    ''' <param name="p_checkForSave">True: Extra parameters are checked that are only guaranteed to be filled before saving the example.</param>
    ''' <returns>True if all required data is filled. Otherwise, false</returns>
    ''' <remarks></remarks>
    Friend Function RequiredDataFilled(Optional ByVal p_checkForSave As Boolean = False) As Boolean
        Try
            'Check that basic data is set/read in correctly

            If String.IsNullOrEmpty(mcFile.pathSource.path) Then Return False

            '= XML Setup Data
            If (String.IsNullOrEmpty(xmlns) OrElse
                String.IsNullOrEmpty(xmlnsXSI) OrElse
                String.IsNullOrEmpty(xsiSchemaLocation)) Then Return False

            '= These items may be handled in the final actions before saving, so may not need to be checked until the model is actually saving.
            If p_checkForSave Then
                If String.IsNullOrEmpty(mcFile.pathDestination.path) Then Return False
                If keywords.Count = 0 Then Return False
            End If

            '= Other
            If Not tests.RequiredDataFilled Then Return False

            If Not exampleDate.RequiredDataFilled Then Return False

            If Not classification.RequiredDataFilled Then Return False

            'Items affected by the user
            If String.IsNullOrEmpty(title) Then Return False

            If ID.idComposite = "0" Then Return False

            If Not author.RequiredDataFilled Then Return False

            '= Benchmark Source is required
            If Not program.RequiredDataFilled Then Return False

            '= At least one result is required
            If (results.Count = 0 AndAlso
                resultsExcel.Count = 0) Then

                UpdateStatus(testerSettings.statusTypes(5)) ' = Add Benchmark Values
                'TODO: Status: If results overwrite is specified, then return True
                Return False
            End If

            '= At least one program type needs to be specified
            If targetProgram.Count = 0 Then Return False

            UpdateStatus(testerSettings.statusTypes(4))    ' = Done
            Return True

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates the example results with the provided set.
    ''' </summary>
    ''' <param name="p_results">Set of result objects to use for the example.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateExampleResults(ByVal p_results As List(Of cMCResultBasic))
        Try
            If p_results Is Nothing OrElse p_results.Count = 0 Then Exit Sub

            Dim resultType As eResultType

            If TypeOf p_results(0) Is cMCResultPostProcessed Then
                resultType = eResultType.postProcessed
            ElseIf TypeOf p_results(0) Is cMCResult Then
                resultType = eResultType.regular
            ElseIf TypeOf p_results(0) Is cMCResultBasic Then
                resultType = eResultType.excelCalculated
            Else
                resultType = eResultType.none
            End If

            ClearResults(resultType)
            AddResults(p_results)

            UpdateExampleStatus()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Updates the model path based on the folder structure of the example and the path to the Model Control xml file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateModelPath()
        Dim modelPath As String = mcFile.pathDestination.directory
        If folderStructure = eFolderStructure.Database Then modelPath &= "\" & DIR_NAME_MODELS_DEFAULT
        modelPath &= "\" & GetPathFileName(myCsiTester.pathGlobal)

        myCsiTester.pathGlobal = modelPath
    End Sub

    ''' <summary>
    ''' Updates the table name and data source object based on the new path.
    ''' </summary>
    ''' <param name="p_dataSource"></param>
    ''' <remarks></remarks>
    Friend Sub UpdateTableSource(ByVal p_dataSource As String)
        _dataSource = New cFileModelExportedTable(p_dataSource, p_bindTo:=Me)
    End Sub

    ''' <summary>
    ''' Checks that all Update objects have unique IDs. 
    ''' If not, new IDs are assigned in order of the date of the update.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetUpdateIDs()
        'Update IDs and get a list of the corresponce of old IDs to new IDs
        Dim oldNewIdentifiers As Dictionary(Of Integer, Integer) = updates.UpdateIDs()

        'Update corresponding result update IDs.
        results.UpdateResultUpdateIDs(oldNewIdentifiers)
    End Sub

    ''' <summary>
    ''' Sets default program name if not already set.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDefaultProgramName()
        If program.programName = eCSiProgram.None Then
            program.programName = targetProgram.primary
        End If
    End Sub

    ''' <summary>
    ''' Sets default program version if not already set.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetDefaultProgramVersion() As Boolean

        If (String.IsNullOrEmpty(program.programVersion) AndAlso
            Not String.IsNullOrEmpty(programControl.version)) Then

            program.programVersion = programControl.version
        End If

        Return True
    End Function
#End Region


#Region "Methods: Friend - OutputSettings File"
    ''' <summary>
    ''' Creates an outputSettings object property of the MC model object, if applicable, with properties set from the current model result queries.
    ''' </summary>
    ''' <param name="p_fillFromResults">True: The results data in the current class is used to populate the object. 
    ''' False: If a datasource path is provided, the object will be populated based on the table file. If not path is given, then the object will only have basic initialization.</param>
    ''' <param name="p_dataSource">Path to the table file to use for populating the object. If not provided, the results data in the current class is used.</param>
    ''' <remarks></remarks>
    Friend Sub CreateOutputSettingsObject(ByVal p_fillFromResults As Boolean,
                                          Optional ByVal p_dataSource As String = "")
        If Not targetProgram.primary = eCSiProgram.ETABS Then Exit Sub

        'Create outputSettings object from a file (if one exists) or a seed file 
        outputSettingsFile = New cFileOutputSettings(Me)

        If Not String.IsNullOrWhiteSpace(p_dataSource) Then
            outputSettingsFile.tableFileName = GetPathFileName(p_dataSource)
        End If

        'Populate the table settings properties.
        If (Not p_fillFromResults AndAlso
            Not String.IsNullOrWhiteSpace(p_dataSource)) Then
            outputSettingsFile.FillFromTable(p_dataSource)
        Else
            outputSettingsFile.FillFromResults()
        End If

        AddOutputSettings(outputSettingsFile)
    End Sub

    ''' <summary>
    ''' Adds the outputsettings object to the Model Control file if it is of the right program. 
    ''' Creates relevant attachment objects as well.
    ''' </summary>
    ''' <param name="p_outputSettings">Outputsettings object to add.</param>
    ''' <param name="p_createAttSupportingFileTag">True: The object will always be considered to be a supporting file, regardless of its properties.
    ''' False: The object will only be considered to be a supporting file if it is set to use v9 units.</param>
    ''' <remarks></remarks>
    Friend Sub AddOutputSettings(ByVal p_outputSettings As cFileOutputSettings,
                                 Optional p_createAttSupportingFileTag As Boolean = False)
        If (targetProgram.primary = eCSiProgram.ETABS AndAlso
            Not String.IsNullOrEmpty(p_outputSettings.tableFileName)) Then

            outputSettingsFile = DirectCast(p_outputSettings.Clone, cFileOutputSettings)

            AddOutputSettingsAttachmentFile(outputSettingsFile)
            AddOutputSettingsSupportingFile(outputSettingsFile, p_createAttSupportingFileTag)
        End If
    End Sub

    ''' <summary>
    ''' Updates all relevant properties of the outputSettings file(s) for a given model, and saves copies of the new file(s) in the appropriate locations relative to the model file.
    ''' </summary>
    ''' <param name="p_paths">File paths to which the outputSettings object is to be saved. 
    ''' Can be multiple file paths as there might be both an attachment and a supporting file to keep in sync.</param>
    ''' <remarks></remarks>
    Friend Sub SaveOutputSettingsFile(ByVal p_paths As List(Of String))
        outputSettingsFile.SaveToAllFiles(p_paths)
    End Sub
#End Region
#Region "Methods: Private - OutputSettings File"
    ''' <summary>
    ''' Adds the outputSettings object attachments reference.
    ''' </summary>
    ''' <param name="p_outputSettings">Outputsettings object to add.</param>
    ''' <remarks></remarks>
    Private Sub AddOutputSettingsAttachmentFile(ByVal p_outputSettings As cFileOutputSettings)
        attachments.Replace(p_outputSettings.CreateAttachment())
    End Sub

    ''' <summary>
    ''' Adds the outputSettings object supporting file reference, if applicable.
    ''' </summary>
    ''' <param name="p_outputSettings">Outputsettings object to add.</param>
    ''' <param name="p_createAttSupportingFileTag">False: This will only be added if V9 units are specified to be used, as this indicates being used with an old version.</param>
    ''' <remarks></remarks>
    Private Sub AddOutputSettingsSupportingFile(ByVal p_outputSettings As cFileOutputSettings,
                                                Optional p_createAttSupportingFileTag As Boolean = False)
        Dim newAttachment As cFileAttachment
        newAttachment = p_outputSettings.CreateAttachmentAsSupportingFile(p_createAttSupportingFileTag)
        supportingFiles.Replace(newAttachment)
    End Sub
#End Region

#Region "Methods: Friend - Add/Remove/etc. for Object Collections"

    'Updates
    ''' <summary>
    ''' Adds the update object to the class as long as the update object does not already exist.
    ''' Also adds the corresponding ticket.
    ''' </summary>
    ''' <param name="p_update">Update object to add to the class.</param>
    ''' <remarks></remarks>
    Friend Sub AddUpdate(ByVal p_update As cMCUpdate)
        If _updates.Add(p_update) Then 'Update was successfully added
            RaisePropertyChanged(Function() Me.updates)

            'If ticket has not already been added to the class, then one is automatically generated.
            tickets.Add(p_update.ticket)
        End If
    End Sub
    ''' <summary>
    ''' Removes the update object at the specified index number. 
    ''' Also removes the corresponding ticket.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Sub RemoveUpdate(ByVal p_index As Integer)
        Dim updateCount As Integer = updates.Count
        Dim ticket As Integer = _updates.item(p_index).ticket

        _updates.Remove(p_index)

        If updates.Count > updateCount Then 'Update was successfully removed
            RaisePropertyChanged(Function() Me.updates)
            tickets.Remove(ticket.ToString)
        End If
    End Sub
    ''' <summary>
    ''' Removes all of the existing updates in the MC Model.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearUpdates()
        'Remove corresponding tickets
        For Each update As cMCUpdate In updates
            tickets.Remove(update.ticket.ToString)
        Next

        _updates = New cMCUpdates
        RaisePropertyChanged(Function() Me.updates)
    End Sub

    'Results
    ''' <summary>
    ''' Places the provided collection of results into the collection of model results.
    ''' </summary>
    ''' <param name="p_results">Collection of 1 or more results.</param>
    ''' <remarks></remarks>
    Friend Sub AddResults(ByVal p_results As List(Of cMCResultBasic))
        If p_results Is Nothing Then Exit Sub

        'Set Table Headers of regular results
        For Each result In p_results
            If TypeOf result Is cMCResult Then DirectCast(result, cMCResult).SetTableHeaders(targetProgram.primary)
        Next

        _results.Add(p_results)
        RaisePropertyChanged(Function() Me.results)
    End Sub
    ''' <summary>
    ''' Places the added result into the results collection, with adjustments of the result ID.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <param name="updateResultIDs">True: The added result will have its ID updated to fit the current set. 
    ''' False: The ID will be left as is.</param>
    ''' <remarks></remarks>
    Friend Sub AddResult(ByVal p_result As cMCResultBasic,
                         Optional ByVal updateResultIDs As Boolean = True)
        If p_result Is Nothing Then Exit Sub

        'Set Table Headers of regular results
        If TypeOf p_result Is cMCResult Then DirectCast(p_result, cMCResult).SetTableHeaders(targetProgram.primary)

        _results.Add(p_result, updateResultIDs)
        RaisePropertyChanged(Function() Me.results)
    End Sub
    ''' <summary>
    ''' Removes all of the existing results in the MC Model.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearResults()
        _results = New cMCResults
        RaisePropertyChanged(Function() Me.results)
    End Sub
    ''' <summary>
    ''' Removes all of the existing results of the specified type in the MC Model.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearResults(ByVal p_resultType As eResultType)
        If p_resultType = eResultType.none Then Exit Sub

        _results.Remove(p_resultType)
        RaisePropertyChanged(Function() Me.results)
    End Sub

    'Results Excel
    ''' <summary>
    ''' Adds the Excel results object to the Excel results container. 
    ''' Only one Excel results object is allowed at a time for an example.
    ''' </summary>
    ''' <param name="p_excelResult">Excel result object to add to the class.</param>
    ''' <param name="p_replaceExisting">If true, then if an Excel results object already exists, it will be replaced by the one supplied to the routine.
    ''' If false, then any existing Excel results object will be preserved and the add routine will be aborted.</param>
    ''' <remarks></remarks>
    Friend Sub AddResultExcel(ByVal p_excelResult As cFileExcelResult,
                               ByVal p_replaceExisting As Boolean)

        If p_excelResult Is Nothing Then Exit Sub

        If Not resultsExcel.Add(p_excelResult) Then
            If p_replaceExisting Then
                resultsExcel.Replace(p_excelResult)
                RaisePropertyChanged(Function() Me.resultsExcel)
            End If
        Else
            RaisePropertyChanged(Function() Me.resultsExcel)
        End If
    End Sub
    ''' <summary>
    ''' Removes the existing Excel results object.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveResultExcel()
        _resultsExcel = New cMCResultsExcel
        RaisePropertyChanged(Function() Me.resultsExcel)
    End Sub

    ''' <summary>
    ''' If the example has Excel result data populated in the Model Control file, populate them into this class.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddExcelResultsFromMCFile()
        If IO.File.Exists(mcFile.pathDestination.path) Then ReadResultsExcel(mcFile.pathDestination.path)
    End Sub

    ''' <summary>
    ''' If the example has Excel results noted for a saved Model Control file, update this file to contain the results from the Excel file.
    ''' RegTest will save these under an "internal use" branch of the Model Control file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddResultsFromExcelFileToMCFile()
        If Not File.Exists(resultsExcel.filePath) Then Exit Sub

        Dim directories As New List(Of String)
        directories.Add(mcFile.pathDestination.directory)

        myCsiTester.RunRegTest(eRegTestAction.ResultsUpdateFromExcel, testerSettings.exampleUpdateFile.fileNameWithExtension, , directories)
    End Sub

    ''' <summary>
    ''' Updates the Model Control file of the Excel results and then populates the class with those results.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetUpdateResultsFromExcel()
        AddResultsFromExcelFileToMCFile()
        ReadResultsExcel(mcFile.pathDestination.path)
    End Sub

    ''' <summary>
    ''' Adds example results for Excel results if they are stored in the Model Control file.
    ''' </summary>
    ''' <param name="p_path">Path to the Model Control file.</param>
    ''' <remarks></remarks>
    Friend Sub ReadResultsExcel(ByVal p_path As String)
        ReadResultsExcelXML(p_path, Me)
    End Sub
#End Region

#Region "Methods: Friend - Copying/Moving Files"
    ''' <summary>
    ''' Saves Model Control object to an existing file at the path destination.
    ''' If the file does not exist, one is created, first by copying from a source location, or if such a location does not exist, then by copying from a seed file.
    ''' If file name properties have changed in session since a file had been created, the file will be renamed.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopySaveModelControlFile()
        Dim mcFileDestination As String = mcFile.pathDestination.path

        ' Rename the file if the file exists at the target location, but under a different name than the destination path
        RenameModelControlFile()

        'Copy the file if it is not already at the target location
        If Not IO.File.Exists(mcFileDestination) Then
            If Not IO.File.Exists(mcFile.pathSource.path) Then
                'Copy seed file
                If Not IO.File.Exists(seedPathMC) Then Throw New IOException("Unable to find Model Control seed file: " & seedPathMC & Environment.NewLine & Environment.NewLine)
            End If

            If Not mcFile.FileCopy() Then Throw New IOException("Model Control file " & CopyFailureMessage(mcFile))
        End If

        If Not SaveFile() Then Throw New IOException("The Model Control XML file at " & mcFile.pathDestination.path & " was unable to be saved.")
    End Sub

    ''' <summary>
    ''' Renames the model control file if the file is not a seed file and it exists at the target location, but under a different name than the destination path.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RenameModelControlFile()
        If isFromSeedFile Then Exit Sub

        ' If the directory is the same but the file name is different, then the source file needs to be renamed.
        If (StringsMatch(mcFile.pathSource.directory, mcFile.pathDestination.directory) AndAlso
            Not StringsMatch(mcFile.pathSource.fileName, mcFile.pathDestination.fileName) AndAlso
            IO.File.Exists(mcFile.pathSource.path) AndAlso
            Not IO.File.Exists(mcFile.pathDestination.path)) Then

            mcFile.FileRename()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyModelFile()
        If Not modelFile.FileCopy() Then Throw New IOException("The Model file " & CopyFailureMessage(modelFile))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyImages()
        Dim errorMessage As String = ""

        For Each image As cFileAttachment In images
            If Not image.FileCopy() Then
                errorMessage &= "The image " & CopyFailureMessage(image)
            End If
        Next

        If Not String.IsNullOrEmpty(errorMessage) Then Throw New IOException(errorMessage)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyAttachments()
        Dim errorMessage As String = ""

        For Each attachment As cFileAttachment In attachments
            If Not FileIsOutputSettings(attachment.title) Then
                If Not attachment.FileCopy() Then errorMessage &= "The attachment " & CopyFailureMessage(attachment)
            End If
        Next

        If Not String.IsNullOrEmpty(errorMessage) Then Throw New IOException(errorMessage)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopySupportingFiles()
        Dim errorMessage As String = ""

        For Each supportingFile As cFileAttachment In supportingFiles
            If Not FileSupportingIsOutputSettings(supportingFile.title) Then
                If Not supportingFile.FileCopy() Then errorMessage &= "The supporting file attachment " & CopyFailureMessage(supportingFile)
            End If
        Next

        If Not String.IsNullOrEmpty(errorMessage) Then Throw New IOException(errorMessage)
    End Sub

    ''' <summary>
    ''' Saves the outputSettings object to an existing file at the path destinations.
    ''' Destinations are for attachments and supporting files if each each exists.
    ''' If the file does not exist, one is created, first by copying from a source location, or if such a location does not exist, then by copying from a seed file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopySaveOutputSettingsFile()
        Dim errorMessage As String = ""
        Dim paths As New List(Of String)

        If targetProgram.primary = eCSiProgram.ETABS Then
            For Each attachment As cFileAttachment In attachments
                If FileIsOutputSettings(attachment.title) Then
                    If Not attachment.FileCopy() Then errorMessage &= "The outputSettings attachment " & CopyFailureMessage(attachment)
                    If String.IsNullOrEmpty(errorMessage) Then paths.Add(attachment.pathDestination.path)
                End If
            Next

            For Each supportingFile As cFileAttachment In supportingFiles
                If FileSupportingIsOutputSettings(supportingFile.title) Then
                    If Not supportingFile.FileCopy() Then errorMessage &= "The outputSettings supporting file attachment " & CopyFailureMessage(supportingFile)
                    If String.IsNullOrEmpty(errorMessage) Then paths.Add(supportingFile.pathDestination.path)
                End If
            Next

            If Not String.IsNullOrEmpty(errorMessage) Then Throw New IOException(errorMessage)

            Try
                SaveOutputSettingsFile(paths)
            Catch ex As Exception
                errorMessage = AppendErrorMessage(errorMessage, ex.Message)
                Throw New IOException(errorMessage)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyExcelFile()
        Dim errorMessages As String = ""

        For Each excelFile As cMCFile In resultsExcel
            If excelFile.FileCopy() Then
                If updateResultsFromExcel Then
                    GetUpdateResultsFromExcel()
                    updateResultsFromExcel = False
                End If
            Else
                Throw New IOException("The Excel file " & CopyFailureMessage(excelFile))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sets the class folder structure of the object based on the directory context of the corresponding file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFolderStructureFromFile()

        If IsMCFileInDatabaseStructure() Then
            folderStructure = eFolderStructure.Database
        Else
            folderStructure = eFolderStructure.Flattened
        End If
    End Sub

    ''' <summary>
    ''' Determines if the file path property of the class points to a database folder structure.
    ''' Will always return 'False' if the folder structure does not yet exist.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsMCFileInDatabaseStructure() As Boolean
        Try
            Dim fileDirectory As String = mcFile.pathDestination.directory

            ' For a database structure:
            ' A "models" folder should always exist
            If Not IO.Directory.Exists(fileDirectory & "\" & DIR_NAME_MODELS_DEFAULT) Then Return False

            ' There should be at least one Model Control file in the folder
            Dim mcFilesList As New ObservableCollection(Of String)
            mcFilesList = New ObservableCollection(Of String)(cMCGenerator.ListModelControlFilesInFolders(fileDirectory))
            If Not mcFilesList.Count > 0 Then Return False

            ' The Model Control file should be one directory level above the model file.
            ' Check that it is not in any subfolders of the highest directory
            If Not GetPathDirectoryStub(mcFilesList(0)) = mcFile.pathDestination.directory Then Return False

            Return True
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Change the destination of the example to the path provided. 
    ''' Any database folder structure will be created inside of this.
    ''' </summary>
    ''' <param name="p_path">Path to the destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub ChangeDestination(ByVal p_path As String)
        If Not IO.Directory.Exists(p_path) Then Exit Sub

        Dim oldParentDirectory As String = GetPathDirectorySubStub(mcFile.pathDestination.directory, p_numberOfDirectories:=1)
        Dim destinationStub As String = FilterStringFromName(mcFile.pathDestination.directory, oldParentDirectory, p_retainPrefix:=False, p_retainSuffix:=True)
        mcFile.pathDestination.SetProperties(p_path & destinationStub & "\" & mcFile.pathDestination.fileNameWithExtension)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Checks the supplied path and clears it if it does not point to a valid Model Control file.
    ''' </summary>
    ''' <param name="p_pathMCFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatedPath(ByVal p_pathMCFile As String,
                                   ByVal p_alertInvalidPath As Boolean) As String
        If Not String.IsNullOrWhiteSpace(p_pathMCFile) Then
            If Not IsModelControlXML(p_pathMCFile) Then
                If p_alertInvalidPath Then
                    RaiseEvent Messenger(New MessengerEventArgs("The following file is not a valid Model Control file: " & Environment.NewLine & Environment.NewLine &
                                                                p_pathMCFile & Environment.NewLine & Environment.NewLine &
                                                                "An object will be generated from a seed file."))
                End If

                p_pathMCFile = ""
            End If
        End If

        Return p_pathMCFile
    End Function

    ''' <summary>
    ''' Returns a copy failure message for the file object provided.
    ''' </summary>
    ''' <param name="p_file">File object that had failed to copy.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyFailureMessage(ByVal p_file As cMCFile) As String
        Return "'" & p_file.pathDestination.fileNameWithExtension & "'" & " was unable to be copied:" & Environment.NewLine & Environment.NewLine &
                "Source Directory: " & Environment.NewLine &
                p_file.pathSource.directory & Environment.NewLine & Environment.NewLine &
                "Destination Directory: " & Environment.NewLine &
                p_file.pathDestination.directory & Environment.NewLine & Environment.NewLine
    End Function

    ''' <summary>
    ''' Returns a string of the new error message appended to the older error message, with a divider if the message is not the first one.
    ''' </summary>
    ''' <param name="p_errorMessage">The current accumulated error message.</param>
    ''' <param name="p_newErrorMessage">The new error message to add to the current error message.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AppendErrorMessage(ByVal p_errorMessage As String,
                                    ByVal p_newErrorMessage As String) As String
        If Not String.IsNullOrEmpty(p_errorMessage) Then
            p_errorMessage &= Environment.NewLine & "======================================================" & Environment.NewLine
        End If
        p_errorMessage &= p_newErrorMessage

        Return p_errorMessage
    End Function

    ''' <summary>
    ''' Updates the model control example status to the provided status.
    ''' </summary>
    ''' <param name="p_status">New status to update the example to.</param>
    ''' <remarks></remarks>
    Private Sub UpdateStatus(ByVal p_status As String)
        statusExample = p_status
    End Sub

    ''' <summary>
    ''' Reads the data from the specified XML file into an equivalent class that is in memory.
    ''' </summary>
    ''' <param name="p_path">Path to the model control XML.</param>
    ''' <remarks></remarks>
    Private Sub ReadFile(ByVal p_path As String)
        Try
            If ReadXMLFile(p_path, Me) Then
                SetKeywordsData(p_read:=True)
                SetCommandLineData()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ' Keywords & Tags
    ''' <summary>
    ''' Creates the necessary keyword tag with the corresponding prefix, and adds the special keyword to the keywords list. Also accounts for maintaining single entries for unique tags.
    ''' </summary>
    ''' <param name="p_read">If 'true', then the classfications are set based on the keywords. 
    ''' If 'false', then the key words are set based on the classifications.</param>
    ''' <remarks></remarks>
    Private Sub SetKeywordsData(ByVal p_read As Boolean)
        If p_read Then
            'Unique Tags
            GetExampleType()
            GetClassRegion()
            GetDesignTypeClass()
            GetImportStatus()
            UpdateMultiModelKeyWords()
        Else
            'Unique Tags
            SetExampleTypes()
            SetClassRegion()
            SetDesignTypeClass()
            SetImportStatus()
            UpdateMultiModelKeyWords()

            'Non-Unique Tags
            'AnalysisTypes
            'ElementTypes
            'DesignTypes? Currently Unique
            'DesignClass? Currently Unique

            'Multi-keyword
            'keywords.Add(tagTypeAnalysis & analysisType)
            'keywords.Add(tagTypeElement & elementType)
            'keywords.Add(tagTypeWarning & warningType)
        End If
    End Sub

    ''' <summary>
    ''' Sets example as either Analysis, Design, 'Analysis and Design', or 'Not Specified'. 
    ''' This is used for title headers and possible future organization
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetExampleType()
        Try
            Dim typeAnalysis As Boolean = False
            Dim typeDesign As Boolean = False
            keywords.GetExampleTypes(typeAnalysis, typeDesign)

            If (typeAnalysis AndAlso typeDesign) Then
                exampleType = TYPE_ANALYSIS_DESIGN
            ElseIf typeAnalysis Then
                exampleType = TYPE_ANALYSIS
            ElseIf typeDesign Then
                exampleType = TYPE_DESIGN
            Else
                exampleType = TYPE_DEFAULT
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    Private Sub SetExampleTypes()
        Dim typeAnalysis As Boolean = False
        Dim typeDesign As Boolean = False

        If StringsMatch(exampleType, TYPE_ANALYSIS_DESIGN) Then
            typeAnalysis = True
            typeDesign = True
        ElseIf StringsMatch(exampleType, TYPE_ANALYSIS) Then
            typeAnalysis = True
        ElseIf StringsMatch(exampleType, TYPE_DESIGN) Then
            typeDesign = True
        End If

        UpdateAnalysisAndDesignKeywords(typeAnalysis, typeDesign)
    End Sub

    ''' <summary>
    ''' Determines whether an example can be grouped in a class or region, depending on keyword specification. 
    ''' This is used for a table header and possible future organization.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetClassRegion()
        Try
            Dim keywordCodeRegion As String = ""
            Dim keywordAnalysisClass As String = ""
            keywords.GetClassRegions(keywordCodeRegion, keywordAnalysisClass)

            codeRegion = keywordCodeRegion
            analysisClass = keywordAnalysisClass

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    Private Sub SetClassRegion()
        UpdateUniqueKeywordTag(analysisClass, TAG_KEYWORD_ANALYSIS_CLASS)
        UpdateUniqueKeywordTag(codeRegion, TAG_KEYWORD_CODE_REGION)
    End Sub

    ''' <summary>
    ''' Sets the design type and class of the example by searching for the corresponding keyword tags.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDesignTypeClass()
        Try
            Dim keywordDesignType As String = ""
            Dim keywordDesignClass As String = ""
            keywords.GetDesignTagValues(keywordDesignType, keywordDesignClass)

            designType = keywordDesignType
            designClass = keywordDesignClass

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    Private Sub SetDesignTypeClass()
        UpdateUniqueKeywordTag(designType, TAG_KEYWORD_DESIGN_TYPE)
        UpdateUniqueKeywordTag(designClass, TAG_KEYWORD_DESIGN_CLASS)
    End Sub

    ''' <summary>
    ''' Determines whether or not a model will undergo an import process by searching for the corresponding keyword tag.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetImportStatus()
        Try
            Dim isImportedModel As Boolean
            keywords.GetImportStatus(isImportedModel)
            _modelFile.PathModelDestination.importedModel = isImportedModel
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    Private Sub SetImportStatus()
        If importedModel = True Then
            UpdateUniqueKeywordTag(importedModelVersion, TAG_KEYWORD_IMPORTED)
        Else
            keywords.RemoveByTag(TAG_KEYWORD_IMPORTED)
        End If
    End Sub

    ''' <summary>
    ''' Replaces any existing unique keyword with the specified keyword, according to the tag. 
    ''' If none currently exists, the specified keyword &amp; tag are added.
    ''' </summary>
    ''' <param name="p_keyword">Keyword to update.</param>
    ''' <param name="p_tag">Tag associated with the keyword or any keyword it may potentially replace.</param>
    ''' <remarks></remarks>
    Private Sub UpdateUniqueKeywordTag(ByVal p_keyword As String,
                                       Optional ByVal p_tag As String = "")
        ' Remove existing tag
        If Not String.IsNullOrEmpty(p_tag) Then keywords.RemoveByTag(p_tag)

        'Add new tag
        If Not String.IsNullOrWhiteSpace(p_keyword) Then
            p_keyword = p_tag & p_keyword
            keywords.Add(p_keyword)
        End If
    End Sub

    ''' <summary>
    ''' Updates the analysis and design keywords based on the status of the example.
    ''' </summary>
    ''' <param name="isAnlaysis">Indicates that the model is focused on analysis aspects.</param>
    ''' <param name="isDesign">Indicates that the model is focused on design aspects.</param>
    ''' <remarks></remarks>
    Private Sub UpdateAnalysisAndDesignKeywords(ByVal isAnlaysis As Boolean,
                                                ByVal isDesign As Boolean)
        ' Remove existing tag
        keywords.RemoveByTag(TAG_KEYWORD_EXAMPLE_TYPE)
        Dim keyword As String = ""

        'Add new tags
        If isAnlaysis Then
            keyword = TAG_KEYWORD_EXAMPLE_TYPE & TYPE_ANALYSIS
            keywords.Add(keyword)
        End If
        If isDesign Then
            keyword = TAG_KEYWORD_EXAMPLE_TYPE & TYPE_DESIGN
            keywords.Add(keyword)
        End If
    End Sub

    ''' <summary>
    ''' Replaces the example type keywords with a new set based on the provided keyword.
    ''' </summary>
    ''' <param name="p_keyword">Keyword of the example type.</param>
    ''' <remarks></remarks>
    Private Sub UpdateExampleTypeKeywords(ByVal p_keyword As String)
        ' Remove existing tag
        keywords.RemoveByTag(TAG_KEYWORD_EXAMPLE_TYPE)

        'Add new tag
        If Not String.IsNullOrWhiteSpace(p_keyword) Then
            If p_keyword = TYPE_ANALYSIS_DESIGN Then
                p_keyword = TAG_KEYWORD_EXAMPLE_TYPE & TYPE_ANALYSIS
                keywords.Add(p_keyword)

                p_keyword = TAG_KEYWORD_EXAMPLE_TYPE & TYPE_DESIGN
                keywords.Add(p_keyword)
            Else
                p_keyword = TAG_KEYWORD_EXAMPLE_TYPE & p_keyword
                keywords.Add(p_keyword)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Updates the key words for grouped models, depending on the grouping enumeration type.
    ''' Auto-updates based on changing model ID properties.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateMultiModelKeyWords()
        keywords.RemoveByTag(TAG_KEYWORD_MULTI_MODEL)

        If Not ID.multiModelType = eMultiModelType.singleModel Then
            keywords.Add(TAG_KEYWORD_MULTI_MODEL & ID.exampleName)
        End If
    End Sub

    ' Misc
    ''' <summary>
    ''' Updates the secondary ID based on syncing status.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SyncSecondaryIDName()
        If secondaryIDSynced = eNameSync.ModelFileName Then
            secondaryID = modelFile.pathDestination.fileName
        ElseIf secondaryIDSynced = eNameSync.ModelControlID Then
            secondaryID = ID.idComposite
        End If
    End Sub

    ''' <summary>
    ''' Sets the property to be written in the XML for the command line calls. Currently only relevant for design examples.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCommandLineData()
        Dim commandLineDesignType As String = ""
        If Not String.IsNullOrEmpty(designType) Then
            'Get the corresponding command line call
            Select Case designType
                Case "General" : commandLineDesignType = ""
                Case "Steel Frame" : commandLineDesignType = testerSettings.commandRunDesignSteel
                Case "Concrete Frame" : commandLineDesignType = testerSettings.commandRunDesignConcrete
                Case "Shear Wall" : commandLineDesignType = testerSettings.commandRunDesignWall
                Case "Composite Beam" : commandLineDesignType = testerSettings.commandRunDesignCompositeBeam
                Case "Composite Column" : commandLineDesignType = testerSettings.commandRunDesignCompositeColumn
                Case "Aluminum Frame" : commandLineDesignType = testerSettings.commandRunDesignAluminum
                Case "Cold-Formed Steel Frame" : commandLineDesignType = testerSettings.commandRunDesignColdFormed
                Case "Slab" : commandLineDesignType = testerSettings.commandRunDesignSlab
            End Select

            'Write the rest of the command line
            commandLine = testerSettings.commandRunAnalysis & " " & testerSettings.commandRunDesign & " " & commandLineDesignType & " " & testerSettings.commandClose & " " & testerSettings.commandBatchRun
        End If
    End Sub

    ''' <summary>
    ''' Returns 'True' if the model name is the same as the model ID number. Zero padding is ignored.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsModelNameSameAsID() As Boolean
        Dim modelFileName As String = GetPathFileName(myCsiTester.pathGlobal)
        Dim modelNameAsNumber As Integer = -1
        If IsNumeric(modelFileName) Then modelNameAsNumber = CInt(modelFileName)

        If modelNameAsNumber = myCDbl(ID.idComposite) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Updates the example status based on the results.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateExampleStatus()
        If results.Count > 0 Then
            If statusExample = testerSettings.statusTypes(5) Then                   'Add benchmark values
                statusExample = testerSettings.statusTypes(4)                       'Done
            End If
        End If
    End Sub
#End Region

#Region "Methods: Boolean"

    Private Function FileSupportingIsOutputSettings(ByVal p_fileTitle As String) As Boolean
        Return (StringExistInName(p_fileTitle, TAG_ATTACHMENT_SUPPORTING_FILE & outputSettingsFile.tableFileName))
    End Function

    Private Function FileIsOutputSettings(ByVal p_fileTitle As String) As Boolean
        Return (StringExistInName(p_fileTitle, TAG_ATTACHMENT_TABLE_SET_FILE))
    End Function
#End Region

End Class
