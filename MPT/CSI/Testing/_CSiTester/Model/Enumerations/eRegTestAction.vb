Option Explicit On
Option Strict On

Imports System.ComponentModel


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