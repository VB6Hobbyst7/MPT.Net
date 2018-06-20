Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Xml.Serialization

Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Class that contains all of the enumerations used in CSiTester
''' </summary>
''' <remarks></remarks>
Public Class cEnumerations

    'For literal enum word to string
    '   See: http://msdn.microsoft.com/en-us/library/system.enum.getname(v=vs.110).aspx
    '   See: http://msdn.microsoft.com/en-us/library/16c1xs4z(v=vs.110).aspx
    'For full string-type/phrase-type associated with enum
    '   See: http://stackoverflow.com/questions/18888519/get-vb-net-enum-description-from-value
    '   See: http://bytes.com/topic/visual-basic-net/answers/353496-description-attribute-retrieval-enum-type

#Region "cCSiTester"


    ''' <summary>
    ''' Sets whether or not CSiTester needs to use an *.ini file to start up, with a remote location set up for write/new files
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eCSiInstallMethod
        UseIni = 0
        NoIni = 1
    End Enum

    ''' <summary>
    ''' Level of the program, specified in the settings file. This affects what portions of the program are visible and accessible, as well as various defaults.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eCSiTesterLevel
        <Description("published")> Published = 1
        <Description("internal")> Internal = 2
        <Description("development")> Development = 3
    End Enum

    ''' <summary>
    ''' Possible Cell Operations to do with selected row. 
    ''' Change checkbox status to add, remove, or make the current selection the only checkboxes to be added
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eCellSelectOperation
        Add = 0
        Remove = 1
        Replace = 2     'Add operation, after all cells are cleared of add/remove status
    End Enum

    ''' <summary>
    ''' The various actions that the regTest.exe will perform.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eRegTestAction
        <Description("None")> None = 0
        ''' <summary>
        ''' Will run all the actions specified in the RegTest.exe configuration XML file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Run All")> All
        ''' <summary>
        ''' Validates model XML files in the models database directory. 
        ''' The validation HTML report is saved as 'CONF_DIR'\out\validate_model_xml_files_list_for_models_database_directory.html
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Validate Model Control Files: Source")> MCValidateDatabase
        ''' <summary>
        ''' Validates model XML files in the models run directory. 
        ''' The validation HTML report is saved as 'CONF_DIR'\out\validate_model_xml_files_list_for_models_run_directory.html
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Validate Model Control Files: Destination")> MCValidateRun
        ''' <summary>
        ''' Validates model XML files in the specified custom directory. 
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Validate Model Control Files: Custom")> MCValidateCustom
        ''' <summary>
        ''' Updates model control XML files in the specified directories to the latest schema version specified in settings.xml file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Model Control Files to Latest Schema")> MCUpdateToLatestSchemaCustom
        ''' <summary>
        ''' For selected models, will update benchmark values in the model control XML files based on the calculated values in the model results XML files.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Example: Benchmark Values")> ValuesUpdateBenchmarks
        ''' <summary>
        ''' For selected models, will update last best values in the model control XML files based on the calculated values in the model results XML files. 
        ''' If the optional "last best value" XML elements do not exist in the model control XML file, they will be created.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Example: Last Best Values")> ValuesUpdateLastBest
        ''' <summary>
        ''' Will update test results XML and HTML files from the exported tables. 
        ''' This is used if the exported tables file is generated from a manual run.
        ''' Generates a new models list, making this less efficient than the other results update method. 
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Example: Results (Slow)")> ResultsUpdate
        ''' <summary>
        ''' Will update test results XML and HTML files from the exported tables.
        ''' This is used if the exported tables file is generated from a manual run.
        ''' Uses the existing models list from the last run, making this more efficient than the other results update method.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Example: Results")> ResultsUpdateReuseModelList
        ''' <summary>
        ''' Running RegTest with this command line parameter will update the /model/regtest_internal_use/excel_results XML element in the model control XML file using the values in the Excel file with Excel calculated results.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Example: Excel Results")> ResultsUpdateFromExcel
        ''' <summary>
        '''Running the above will perform the following actions:
        '''    + A backup of the current configuration file is created. If the current configuration file was named regtest.xml, the backup file would be named regtest_backup_YYYY-MM-DD-HH-mm-SS.xml.
        '''    + The existing configuration file is then attempted to be validated against its current and previous schema versions, starting with the latest schema version and progressing to the previous schema versions, until it can be validated.
        '''    + Once validated, it will be incrementally translated to the latest schema version, using either XSLT stylesheets or direct manipulation of the elements via XMLDocument VB.NET object.
        '''Note that automatic translation can be specified in the settings.xml file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update regTest.xml to Latest Schema")> ConfigUpdateToLatestSchema
        ''' <summary>
        ''' Displays message box with the version of the current RegTest.exe instance. 
        ''' Can be used to quickly identify the RegTest.exe version. 
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Version")> Version
    End Enum


#End Region

#Region "Global and Suite Concerns"
    ''' <summary>
    ''' List of valid CSiPrograms that can be selected for analysis.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eCSiProgram
        <Description("")> None
        <Description("SAP2000")> SAP2000
        <Description("CSiBridge")> CSiBridge
        <Description("ETABS")> ETABS
        <Description("SAFE")> SAFE
        <Description("Perform 3D")> Perform3D
    End Enum

    ''' <summary>
    ''' List of design types that can be selected for design in CSi products.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eDesignType
        <Description("")> myError = 0
        <Description("General")> general
        <Description("Steel Frame")> steelFrame
        <Description("Concrete Frame")> concreteFrame
        <Description("Shear Wall")> shearWall
        <Description("Composite Beam")> compositeBeam
        <Description("Composite Column")> compositeColumn
        <Description("Aluminum Frame")> aluminumFrame
        <Description("Cold-Formed Steel Frame")> coldFormedSteelFrame
    End Enum

    ''' <summary>
    ''' The various test types that regTest can run.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eTestType
        <Description("")> myError = 0
        <Description("")> none
        ''' <summary>
        ''' The model runs without any changes of the model prior to running it. 
        ''' This applies to the vast majority of the models.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Run As Is")> runAsIs
        ''' <summary>
        ''' Same as 'Run As Is', but also will run 9 different combinations of analysis parameters while saving the test results into separate subdirectories in the output directory.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Run As Is PSB")> runAsIsDiffAnalyParams
        ''' <summary>
        ''' This test applies only to CSiBridge and serves to verify whether all bridge objects can be successfully updated. 
        ''' If the program gets stuck while updating the bridge objects, it will time out and the model will be subjected to further scrutiny.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Bridge")> updateBridge
        ''' <summary>
        ''' This test applies only to CSiBridge and serves to verify whether the benchmarks remain the same when the model is run after updating bridge objects.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Update Bridge and Run")> updateBridgeAndRun
    End Enum

    ''' <summary>
    ''' Specifies whether a suite is being created or edited.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eSuiteAction
        <Description("None")> None = 0
        <Description("Create")> Create
        <Description("Edit")> Edit
    End Enum

#End Region

#Region "Results"
    ''' <summary>
    ''' Whether to run an example, compare an example's results, or both.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eCheckType
        None = 1
        Run = 2
        Compare = 3
        RunAndCompare = 4
        ''' <summary>
        ''' Examples are run, without being compared, in some cases.
        ''' </summary>
        ''' <remarks></remarks>
        RunAndCompareNoSync = 5
    End Enum

    ''' <summary>
    ''' Sets whether the check action in the main datagrid is to run or compare the example
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eCSiTesterCheckAction
        Run = 0
        Compare = 1
    End Enum

    ''' <summary>
    ''' Test results type to query for. Used for considering different strategies for filling in assumed run and compare times based on example output.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eTestResults
        ModelID = 0
        ActualAnalysisRunTime = 1
        ActualDatabaseRetrievalTime = 2
        ActualTotalRunTime = 3
        Other = 4
    End Enum

    ''' <summary>
    ''' Possible status returned for the run status of a result.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eResultRun
        ''' <summary>
        ''' The analysis/'bridge objects' were successfully updated within the expected time limit.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Completed")> completed = 1

        <Description("Run")> completedRun = 2
        ''' <summary>
        ''' The operation was not completed within the expected time limit.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Time Out")> timeOut = 3
        ''' <summary>
        ''' Designates that the model was run manually and RegTest was only used to retrieve the results from the automatically saved tabular file using the --update-test-results-xml-file command line parameter
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Unknown (Manual Run)")> manual = 4

        <Description("Not Run")> notRun = 5

        <Description("Running")> running = 6

        ''' <summary>
        ''' RegTest label of the example currently running.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Currently Running")> runningCurrently = 7
        ''' <summary>
        ''' RegTest label of an example that is set to be run.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("To Be Run")> toBeRun = 8

        <Description("Not Run Yet")> notRunYet = 9

        <Description("Output File Missing")> outputFileMissing = 10
    End Enum

    ''' <summary>
    ''' Possible status returned for the compared status of a result.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eResultCompare
        ''' <summary>
        ''' All results specified in model XML file were successfully retrieve from the database file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Success")> success = 1

        <Description("Compared")> successCompared = 2
        ''' <summary>
        ''' The database file does not exist, or RegTest was unable to establish connection to the database file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("No DB File")> noDBFile = 3
        ''' <summary>
        ''' RegTest was able to connect to the database file, but was unable to retrieve all the results specified in the model XML.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("DB Read Failure")> dbReadFailure = 4

        <Description("Not Compared")> notCompared = 5

        <Description("Comparing")> comparing = 6

        <Description("Model Needs To Be Run")> notRunYet = 7
        ''' <summary>
        ''' Program is unable to find the exported table files from the analysis programs.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Output File Missing")> outputFileMissing = 8
    End Enum

    ''' <summary>
    ''' Possible status returned for the overall status of a checked result.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eResultOverall
        ''' <summary>
        ''' All individual operations completed successfully.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Checked")> success = 1
        ''' <summary>
        ''' One or more individual operations failed.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Error")> errorResult = 2
        ''' <summary>
        ''' One or more individual operations have "unknown (manual run)" status.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Unknown (Manual Run)")> manual = 3

        <Description("Not Checked")> notChecked = 4
        ''' <summary>
        ''' Percent difference not available.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("N/A")> percDiffNotAvailable = 5

        <Description("Comparison Error")> overallResultError = 6
    End Enum

#End Region


#Region "XML Editor"
    ''' <summary>
    ''' Instructions to the XML Bulk Editor as to what action to take with the selected rows.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eXMLEditorAction
        None = 0
        Save = 1
        Convert = 2
        NodeAdd = 3
        KeywordsAddRemove = 4
        ObjectAdd = 5
        DirectoriesFlatten = 6
        DirectoriesDBGather = 7
        UpdateModelFiles = 8
        UpdateOutputSettingsFiles = 9
        NodeDelete = 10
        ActionToExistingFiles = 11
        ActionToNewFiles = 12
        RenameMCFilesAddSuffix = 13
        RenameOutputSettingsFilesRemoveImportTag = 14
        CreateNewModelSourceFromDestination = 15
    End Enum

    ''' <summary>
    ''' Edit mode types available within the Example details form for editing various datagrid fields.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eExampleEditMode
        None = 1
        Benchmark = 2
        IndependentValue = 3
        OutputParameter = 4
    End Enum
#End Region

#Region "cXMLObject"
    ' ''' <summary>
    ' ''' Classification of the XML node type used for handling of the nodes in other parts of the program.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum eXMLElementType
    '    Attribute
    '    Node
    '    Header
    '    Comment
    'End Enum

    ' ''' <summary>
    ' ''' Specifies whether the node create operation should make a child node to the specified node, 
    ' ''' or insert the node before or after the specified node.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum eNodeCreate
    '    child
    '    insertBefore
    '    insertAfter
    'End Enum

    ''' <summary>
    ''' Classification of the XML node object found in model xml control files.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eXMLObjectType
        <Description("Incident")> Incident
        <Description("Ticket")> Ticket
        <Description("Link")> Link
        <Description("Attachment")> Attachment
        <Description("Image")> Image
        <Description("Update")> Update
        <Description("Excel Result")> ExcelResult
        <Description("Supporting File")> SupportingFile
        <Description("Documentation")> Documentation
        <Description("Output Settings")> OutputSettings
    End Enum


    'Friend Enum eReadWriteConversion
    '    readWriteOnly
    '    convertToEnum
    '    convertPathStoredRelative
    '    convertPathStoredRelativeFromList
    '    convertPathStoredUnknown
    '    convertBooleanYesNo
    '    convertBooleanTrueFalse
    '    convertInteger
    '    convertObservableCollection
    '    convertObservableCollectionFromList
    '    convertObservableCollectionFromUniqueList
    '    aggregateAndConvertObservableCollectionStart
    '    aggregateAndConvertObservableCollection
    '    aggregateAndConvertObservableCollectionEnd
    'End Enum


#End Region


#Region "Misc"


    ' ''' <summary>
    ' ''' Enumeration of how to return the capitalization sense of a string.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum eCapitalization
    '    ALLCAPS = 0
    '    alllower = 1
    '    Firstupper = 2
    'End Enum

    ' ''' <summary>
    ' ''' Used for 3-type yes/no/unknown values.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Enum eYesNoUnknown
    '    <Description("yes")> yes = 1
    '    <Description("no")> no = 2
    '    <Description("")> unknown = 3
    'End Enum

    ''' <summary>
    ''' Enumerations for common prompt actions. To be used for custom forms in the program.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ePromptActions
        None = 0
        <Description("Yes")> Yes
        <Description("No")> No
        <Description("OK")> OK
        <Description("Cancel")> Cancel
        <Description("Abort")> Abort
        <Description("Retry")> Retry
        <Description("Ignore")> Ignore
    End Enum

    ''' <summary>
    ''' Sets of actions for common prompts. To be used for custom forms in the program.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ePromptActionSets
        None = 0
        OkCancel
        OkOnly
        YesNo
        AbortRetryIgnore
        RetryCancel
        YesNoCancel
    End Enum
#End Region


    '#Region "Methods: Public"
    '    ''' <summary>
    '    ''' Returns the enum description (if any), otherwise returns the name of the enum value. 
    '    ''' </summary>
    '    ''' <param name="p_enumObj">Selected enumeration to convert.</param> 
    '    Friend Shared Function GetEnumDescription(Of TEnum)(ByVal p_enumObj As TEnum) As String
    '        Dim fi As Reflection.FieldInfo = p_enumObj.GetType().GetField(p_enumObj.ToString())

    '        Dim attributes As DescriptionAttribute() = CType(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())

    '        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
    '            Return attributes(0).Description
    '        Else
    '            Return p_enumObj.ToString()
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Returns the enum XML attribute (if any), otherwise returns the name of the num value.
    '    ''' </summary>
    '    ''' <typeparam name="TEnum"></typeparam>
    '    ''' <param name="p_enumObj"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Shared Function GetEnumXMLAttribute(Of TEnum)(ByVal p_enumObj As TEnum) As String
    '        Dim fi As Reflection.FieldInfo = p_enumObj.GetType().GetField(p_enumObj.ToString())

    '        Dim attributes As XmlEnumAttribute() = CType(fi.GetCustomAttributes(GetType(XmlEnumAttribute), False), XmlEnumAttribute())

    '        If attributes IsNot Nothing AndAlso attributes.Length > 0 Then
    '            Return attributes(0).Name
    '        Else
    '            Return p_enumObj.ToString()
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Returns the enum if the string matches the enum description. Returns Nothing if no match is found. 
    '    ''' </summary>
    '    ''' <param name="p_string">String item to match to the enum by enum description.</param>
    '    Friend Shared Function ConvertStringToEnumByDescription(Of TEnum)(ByVal p_string As String) As TEnum
    '        Dim enumT As TEnum
    '        Dim enumItems As Array = System.Enum.GetValues(enumT.GetType)

    '        For Each enumItem As TEnum In enumItems
    '            If StringsMatch(GetEnumDescription(enumItem), p_string) Then Return enumItem
    '        Next

    '        Return Nothing
    '    End Function

    '    '''' <param name="p_enumObj">Sample enum from the type. This is for convenience in calling the function rather than getting type.</param> 

    '    ''' <summary>
    '    ''' Returns the enum if the string matches the enum XML attribute (or Enum.ToString() where no attribute exists). Returns Nothing if no match is found. 
    '    ''' </summary>
    '    ''' <param name="p_string">String item to match to the enum by enum description.</param>
    '    Friend Shared Function ConvertStringToEnumByXMLAttribute(Of TEnum)(ByVal p_string As String) As TEnum
    '        Dim enumT As TEnum
    '        Dim enumItems As Array = System.Enum.GetValues(enumT.GetType)

    '        For Each enumItem As TEnum In enumItems
    '            If StringsMatch(GetEnumXMLAttribute(enumItem), p_string) Then Return enumItem
    '        Next

    '        Return Nothing
    '    End Function

    '    ''' <summary>
    '    ''' Returns the list of all descriptions for an enumeration list.
    '    ''' Note that there is not always a description for every enumeration item.
    '    ''' </summary>
    '    ''' <typeparam name="TEnum">Type of enumeration.</typeparam>
    '    ''' <param name="p_enumObj">Sample enumeration object from the list of enums.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Shared Function GetEnumDescriptionList(Of TEnum)(ByVal p_enumObj As TEnum) As List(Of String)
    '        Dim enumDescriptions As New List(Of String)
    '        Dim enumItems As Array = System.Enum.GetValues(p_enumObj.GetType)

    '        For Each enumItem As TEnum In enumItems
    '            enumDescriptions.Add(GetEnumDescription(enumItem))
    '        Next

    '        Return enumDescriptions
    '    End Function

    '    ''' <summary>
    '    ''' Returns the list item that matches the provided enumeration based on the XML attribute or .ToString() property of the enumeration.
    '    ''' Returns nothing if no match was found.
    '    ''' </summary>
    '    ''' <typeparam name="TEnum">Type of enumeration.</typeparam>
    '    ''' <param name="p_enum">EnumerationToMatch</param>
    '    ''' <param name="p_values">List of values to match to the enumeration.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Friend Shared Function GetListItemMatchingEnumByXMLAttribute(Of TEnum)(ByVal p_enum As TEnum,
    '                                                                           ByVal p_values As IList(Of String)) As String
    '        Dim enumValue As String = GetEnumXMLAttribute(p_enum)

    '        For Each value As String In p_values
    '            If StringsMatch(value, enumValue) Then Return value
    '        Next

    '        Return ""
    '    End Function
    '#End Region

End Class
