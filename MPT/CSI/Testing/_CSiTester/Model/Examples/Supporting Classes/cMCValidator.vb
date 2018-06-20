Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Xml

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.XML.ReaderWriter

Imports CSiTester.cRegTest
Imports CSiTester.cSettings

''' <summary>
''' Class that is used for storing data for a given entry in the model validation output.
''' </summary>
''' <remarks></remarks>
Public Class cMCValidator
    Inherits PropertyChanger
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Enumerations"
    ''' <summary>
    ''' Determines what action is taken regarding Model Control files being validated against the regTest schema.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eSchemaValidate
        <Description("None")> None = 0
        ''' <summary>
        ''' Displays the existing schema validation results that are generated from a regTest run.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("View Schema Validation")> ViewSchemaValidation
        ''' <summary>
        ''' Validates all examples that have been loaded in the suite to be run with regTest.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Run Schema Validation")> RunSchemaValidation
        ''' <summary>
        ''' Validates all examples in a specified directory, including all sub-directories.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Run Custom Schema Validation")> RunSchemaValidationCustom
        ''' <summary>
        ''' Validates all examples in a specified directory, including all sub-directories.
        ''' This version is automated, meaning that the path has already been supplied so the form merely displays the results after the schema validation is run.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Auto Run Custom Schema Validation")> RunSchemaValidationCustomAuto
    End Enum
#End Region

#Region "Constants"
    ''' <summary>
    ''' Filename of the xml file used by regTest for validating the models in the run directory.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FILENAME_MODELS_RUN_VALIDATION As String = "validate_model_xml_files_list_for_models_run_directory.xml"

    ''' <summary>
    ''' Filename of the xml file used by regTest for validating the models in the run directory.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FILENAME_MODELS_CUSTOM_VALIDATION As String = "validate_model_xml_files_list_for_custom_directory.xml"
#End Region

#Region "Fields"
    ''' <summary>
    ''' Type of validation to perform.
    ''' </summary>
    ''' <remarks></remarks>
    Private _operation As eSchemaValidate

    ''' <summary>
    ''' RegTest object to use for validation.
    ''' </summary>
    ''' <remarks></remarks>
    Private _regTest As cRegTest

    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties"
    Private _modelID As String
    Public Property modelID As String
        Set(ByVal value As String)
            If Not _modelID = value Then
                _modelID = value
                RaisePropertyChanged("modelID")
            End If
        End Set
        Get
            Return _modelID
        End Get
    End Property

    Private _fileName As String
    Public Property fileName As String
        Set(ByVal value As String)
            If Not _fileName = value Then
                _fileName = value
                RaisePropertyChanged("fileName")
            End If
        End Set
        Get
            Return _fileName
        End Get
    End Property

    Private _modelName As String
    Public Property modelName As String
        Set(ByVal value As String)
            If Not _modelName = value Then
                _modelName = value
                RaisePropertyChanged("modelName")
            End If
        End Set
        Get
            Return _modelName
        End Get
    End Property

    Private _statusValidation As String
    Public Property statusValidation As String
        Set(ByVal value As String)
            If Not _statusValidation = value Then
                _statusValidation = value
                RaisePropertyChanged("statusValidation")
            End If
        End Set
        Get
            Return _statusValidation
        End Get
    End Property

    Private _commentValidation As String
    Public Property commentValidation As String
        Set(ByVal value As String)
            If Not _commentValidation = value Then
                _commentValidation = value
                RaisePropertyChanged("commentValidation")
            End If
        End Set
        Get
            Return _commentValidation
        End Get
    End Property

    Private _statusDuplicateID As String
    Public Property statusDuplicateID As String
        Set(ByVal value As String)
            If Not _statusDuplicateID = value Then
                _statusDuplicateID = value
                RaisePropertyChanged("statusDuplicateID")
            End If
        End Set
        Get
            Return _statusDuplicateID
        End Get
    End Property

    Private _commentDuplicateID As String
    Public Property commentDuplicateID As String
        Set(ByVal value As String)
            If Not _commentDuplicateID = value Then
                _commentDuplicateID = value
                RaisePropertyChanged("commentDuplicateID")
            End If
        End Set
        Get
            Return _commentDuplicateID
        End Get
    End Property

    Private _filePath As String
    Public Property filePath As String
        Set(ByVal value As String)
            If Not _filePath = value Then
                _filePath = value
                RaisePropertyChanged("filePath")
            End If
        End Set
        Get
            Return _filePath
        End Get
    End Property

    Private _schemaVersion As String
    Public Property schemaVersion As String
        Set(ByVal value As String)
            If Not _schemaVersion = value Then
                _schemaVersion = value
                RaisePropertyChanged("schemaVersion")
            End If
        End Set
        Get
            Return _schemaVersion
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New(ByVal p_operation As eSchemaValidate,
                   ByRef p_regTest As cRegTest)
        _operation = p_operation
        _regTest = p_regTest
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Gets the results location and validates the model control files. Returns true if this is successful, false if not.
    ''' </summary>
    ''' <param name="p_path">Path to the directory that contains the model control files to be run through the example schema validation test.</param>
    ''' <param name="p_validationResults">Collection of validation results classes that retain the results in memory for use in CSiTester.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateModels(ByVal p_path As String,
                                   ByRef p_validationResults As ObservableCollection(Of cMCValidator)) As Boolean
        Return ValidateModels(New List(Of String)(New String() {p_path}), p_validationResults)
    End Function

    ''' <summary>
    ''' Gets the results location and validates the model control files. Returns true if this is successful, false if not.
    ''' </summary>
    ''' <param name="p_paths">Paths to the directories that contain the model control files to be run through the example schema validation test.</param>
    ''' <param name="p_validationResults">Collection of validation results classes that retain the results in memory for use in CSiTester.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ValidateModels(ByVal p_paths As List(Of String),
                                   ByRef p_validationResults As ObservableCollection(Of cMCValidator)) As Boolean
        For Each path As String In p_paths
            If Not IO.Directory.Exists(path) Then Return False
        Next

        'Get Results Location
        Dim resultsPath As String = SchemaValidationResultsLocation()

        Dim oldFileTime As Date
        Dim infoReader As System.IO.FileInfo = Nothing

        If WaitUntilFileAvailable(resultsPath) Then
            infoReader = My.Computer.FileSystem.GetFileInfo(resultsPath)
            'If Not infoReader.Exists Then Return False
            oldFileTime = infoReader.LastWriteTimeUtc
        End If

        'Validate XML files & show results
        If myCsiTester.ValidateModelControl(SetRegTestAction(_operation), p_paths) Then

            ''Wait until output file is updated
            While infoReader.LastWriteTimeUtc = oldFileTime
                If WaitUntilFileAvailable(resultsPath) Then
                    infoReader = My.Computer.FileSystem.GetFileInfo(resultsPath)
                    If Not infoReader.Exists Then Return False
                End If
            End While

            'Create summary class collections
            If IO.File.Exists(resultsPath) Then
                p_validationResults = CreateSummaryClassCollections(resultsPath)
                If p_validationResults.Count = 0 Then
                    RaiseEvent Messenger(New MessengerEventArgs("No validation results found."))
                End If

                Return True
            Else
                RaiseEvent Messenger(New MessengerEventArgs("File does not exist: " & Environment.NewLine & Environment.NewLine &
                                resultsPath))
                Return False
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Returns the path to the file that contains the results of the schema validation test.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SchemaValidationResultsLocation() As String

        If _operation = eSchemaValidate.ViewSchemaValidation Then
            Return myCsiTester.testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION & "\" & FILENAME_MODELS_RUN_VALIDATION
        Else
            Dim validationRegTestPath As String = _regTest.regTestFile.path & "\" & DIR_NAME_REGTEST & "\" & testerSettings.exampleValidationFile.fileNameWithExtension

            Dim resultsPath As String = _regTest.RegTestResultsPath(validationRegTestPath)
            resultsPath = _regTest.regTestFile.path & "\" & DIR_NAME_REGTEST & "\" & resultsPath & "\"

            If (_operation = eSchemaValidate.RunSchemaValidationCustom OrElse
                _operation = eSchemaValidate.RunSchemaValidationCustomAuto) Then

                resultsPath &= FILENAME_MODELS_CUSTOM_VALIDATION
            Else
                resultsPath &= FILENAME_MODELS_RUN_VALIDATION
            End If

            Return resultsPath
        End If
    End Function

    ''' <summary>
    ''' Populates the collection of classes that summarize the example schema validation tests.
    ''' </summary>
    ''' <param name="resultsPath">The path to the file that contains the results of the schema validation test.</param>
    ''' <remarks></remarks>
    Friend Function CreateSummaryClassCollections(ByVal resultsPath As String) As ObservableCollection(Of cMCValidator)
        Dim validationResults As ObservableCollection(Of cMCValidator) = ReadValidationEntries(resultsPath)

        For Each validationResult As cMCValidator In validationResults
            validationResult.SetModelName()
        Next

        Return validationResults
    End Function


#End Region

#Region "Methods: Private"
    Private Sub SetModelName()
        Try
            _xmlReaderWriter.GetSingleXMLNodeValue(filePath, "//n:id_secondary", modelName)
        Catch ex As Exception
            'Node may not exist. Not a required node.
        End Try
    End Sub

    ''' <summary>
    ''' Reads the results of the file for validating Model Control Files.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadValidationEntries(ByVal resultsPath As String) As ObservableCollection(Of cMCValidator)
        Dim validationEntries As New List(Of cMCValidator)

        'Open XML File
        If _xmlReaderWriter.InitializeXML(resultsPath) Then
            Try
                Dim xmlDoc As XmlDocument = _xmlReaderWriter.xmlDoc
                Dim xmlRoot As XmlElement = _xmlReaderWriter.xmlRoot

                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                Dim myXMLNode As XmlNode = xmlRoot.SelectSingleNode("//n:files", nsmgr)

                For j As Integer = 0 To myXMLNode.ChildNodes.Count - 1
                    Dim myXMLObject As XmlNode = myXMLNode.ChildNodes(j)  'Selects object root node

                    Dim validationEntry As New cMCValidator(_operation, _regTest)
                    With validationEntry
                        .modelID = myXMLObject.SelectSingleNode("model_id").InnerText
                        .filePath = myXMLObject.SelectSingleNode("file_path").InnerText
                        .fileName = GetSuffix(.filePath, "\")
                        .schemaVersion = myXMLObject.SelectSingleNode("schema_version").InnerText
                        .statusValidation = myXMLObject.SelectSingleNode("status").InnerText
                        .commentValidation = myXMLObject.SelectSingleNode("//n:comment/n:item/n:validation_message", nsmgr).InnerText
                        .statusDuplicateID = myXMLObject.SelectSingleNode("duplicate_model_id_status").InnerText
                        .commentDuplicateID = myXMLObject.SelectSingleNode("duplicate_model_id_message").InnerText
                    End With

                    validationEntries.Add(validationEntry)
                Next j
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex))
            Finally
                _xmlReaderWriter.CloseXML()
            End Try
        End If

        Return New ObservableCollection(Of cMCValidator)(validationEntries)
    End Function

    ''' <summary>
    ''' Returns the corresponding regTest action to the provided schema validation action.
    ''' </summary>
    ''' <param name="p_operation">Schema validation action to convert.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SetRegTestAction(ByVal p_operation As eSchemaValidate) As eRegTestAction
        Select Case p_operation
            Case eSchemaValidate.RunSchemaValidation
                Return eRegTestAction.MCValidateRun
            Case eSchemaValidate.RunSchemaValidationCustom
                Return eRegTestAction.MCValidateCustom
            Case eSchemaValidate.RunSchemaValidationCustomAuto
                Return eRegTestAction.MCValidateCustom
            Case Else
                Return eRegTestAction.MCValidateRun
        End Select
    End Function
#End Region


End Class
