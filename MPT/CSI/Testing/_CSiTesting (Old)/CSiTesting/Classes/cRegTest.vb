Option Explicit On
Public Class cRegTest
#Region "Verification Control"
    '================================================================================
    'Verification Control
    '================================================================================
    'Analysis Settings
    '================================================================================
    Private pSolver As String
    Private pDeleteAnalysisFiles As String
    Private pThirtyTwoBit As String
    Private pProcess As String
    '================================================================================
    'excelName: Solver
    'programName: 
    'XMLpath: 
    Public Property Solver() As String
        Get
            Return pSolver
        End Get
        Set(value As String)
            pSolver = value
        End Set
    End Property
    '================================================================================
    'excelName: DeleteAnalysisFiles
    'programName: 
    'XMLpath: 
    Public Property DeleteAnalysisFiles() As String
        Get
            Return pDeleteAnalysisFiles
        End Get
        Set(value As String)
            pDeleteAnalysisFiles = value
        End Set
    End Property
    '================================================================================
    'excelName: ThirtyTwoBit
    'programName: 
    'XMLpath: 
    Public Property ThirtyTwoBit() As String
        Get
            Return pThirtyTwoBit
        End Get
        Set(value As String)
            pThirtyTwoBit = value
        End Set
    End Property
    '================================================================================
    'excelName: Process
    'programName: 
    'XMLpath: 
    Public Property Process() As String
        Get
            Return pProcess
        End Get
        Set(value As String)
            pProcess = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Misc"
    '================================================================================
    'Misc
    Private pmodel_xml_file_schema_file_path_attrib As String
    Private pmodel_xml_file_schema_file_path As String
    Private pcomputer_id As String
    Private pcomputer_description As String
    '================================================================================
    Public Property model_xml_file_schema_file_path_attrib() As String
        Get
            model_xml_file_schema_file_path_attrib = pmodel_xml_file_schema_file_path_attrib
        End Get
        Set(value As String)
            pmodel_xml_file_schema_file_path_attrib = value
        End Set
    End Property
    '================================================================================
    Public Property model_xml_file_schema_file_path() As String
        Get
            model_xml_file_schema_file_path = pmodel_xml_file_schema_file_path
        End Get
        Set(value As String)
            pmodel_xml_file_schema_file_path = value
        End Set
    End Property
    '================================================================================
    Public Property computer_id() As String
        Get
            Return pcomputer_id
        End Get
        Set(value As String)
            pcomputer_id = value
        End Set
    End Property
    '================================================================================
    Public Property computer_description() As String
        Get
            Return pcomputer_description
        End Get
        Set(value As String)
            pcomputer_description = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Test Suite Setup"
    'Test Suite Setup
    Private ptest_description As String
    Private ptest_id As String
    Private pprogram_name As String
    Private pmodels_database_directory As String
    Private pmodels_database_directory_attrib As String
    Private pmodels_run_directory As String
    Private pmodels_run_directory_attrib As String
    Private pcopy_models_AttribRun As String
    Private pcopy_models_flat_attribRun As String
    '================================================================================
    'excelName: test_description
    'programName: 
    'XMLpath: 
    Public Property test_description() As String
        Get
            test_description = ptest_description
        End Get
        Set(value As String)
            ptest_description = value
        End Set
    End Property
    '================================================================================
    'excelName: test_id
    'programName: 
    'XMLpath: 
    Public Property test_id() As String
        Get
            test_id = ptest_id
        End Get
        Set(value As String)
            ptest_id = value
        End Set
    End Property
    '================================================================================
    'excelName: program_name
    'programName: 
    'XMLpath: 
    Public Property program_name() As String
        Get
            program_name = pprogram_name
        End Get
        Set(value As String)
            pprogram_name = value
        End Set
    End Property
    '================================================================================
    'excelName: models_database_directory
    'programName: 
    'XMLpath: 
    Public Property models_database_directory() As String
        Get
            models_database_directory = pmodels_database_directory
        End Get
        Set(value As String)
            pmodels_database_directory = value
        End Set
    End Property
    '================================================================================
    'excelName: models_database_directory_attrib
    'programName: 
    'XMLpath: 
    Public Property models_database_directory_attrib() As String
        Get
            models_database_directory_attrib = pmodels_database_directory_attrib
        End Get
        Set(value As String)
            pmodels_database_directory_attrib = value
        End Set
    End Property
    '================================================================================
    'excelName: models_run_directory
    'programName: 
    'XMLpath: 
    Public Property models_run_directory() As String
        Get
            models_run_directory = pmodels_run_directory
        End Get
        Set(value As String)
            pmodels_run_directory = value
        End Set
    End Property
    '================================================================================
    'excelName: models_run_directory_attrib
    'programName: 
    'XMLpath: 
    Public Property models_run_directory_attrib() As String
        Get
            models_run_directory_attrib = pmodels_run_directory_attrib
        End Get
        Set(value As String)
            pmodels_run_directory_attrib = value
        End Set
    End Property
    '================================================================================
    'excelName: copy_models_AttribRun
    'programName: 
    'XMLpath: 
    Public Property copy_models_AttribRun() As String
        Get
            copy_models_AttribRun = pcopy_models_AttribRun
        End Get
        Set(value As String)
            pcopy_models_AttribRun = value
        End Set
    End Property
    '================================================================================
    'excelName: copy_models_flat_attribRun
    'programName: 
    'XMLpath: 
    Public Property copy_models_flat_attribRun() As String
        Get
            copy_models_flat_attribRun = pcopy_models_flat_attribRun
        End Get
        Set(value As String)
            pcopy_models_flat_attribRun = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Test Suite Setup - Distributed Testing"
    'Test Suite Setup - Distributed Testing
    Private pserver_base_URL As String
    Private pmodels_source As String
    '================================================================================
    'excelName: server_base_URL
    'programName: 
    'XMLpath: 
    Public Property server_base_URL() As String
        Get
            server_base_URL = pserver_base_URL
        End Get
        Set(value As String)
            pserver_base_URL = value
        End Set
    End Property
    '================================================================================
    'excelName: models_source
    'programName: 
    'XMLpath: 
    Public Property models_source() As String
        Get
            models_source = pmodels_source
        End Get
        Set(value As String)
            pmodels_source = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Test Suite Setup - Advanced Settings"
    'Test Suite Setup - Advanced Settings
    Private pcopy_models_flat_attribReuse As String
    Private pwrite_xml_files_list_attrib As String
    Private pcopy_models_flat_attribDryRun As String
    Private ptest_to_run_attrib As String
    Private ptest_to_run As String
    '================================================================================
    'excelName: copy_models_flat_attribReuse
    'programName: 
    'XMLpath: 
    Public Property copy_models_flat_attribReuse() As String
        Get
            copy_models_flat_attribReuse = pcopy_models_flat_attribReuse
        End Get
        Set(value As String)
            pcopy_models_flat_attribReuse = value
        End Set
    End Property
    '================================================================================
    'excelName: write_xml_files_list_attrib
    'programName: 
    'XMLpath: 
    Public Property write_xml_files_list_attrib() As String
        Get
            write_xml_files_list_attrib = pwrite_xml_files_list_attrib
        End Get
        Set(value As String)
            pwrite_xml_files_list_attrib = value
        End Set
    End Property
    '================================================================================
    'excelName: copy_models_flat_attribDryRun
    'programName: 
    'XMLpath: 
    Public Property copy_models_flat_attribDryRun() As String
        Get
            copy_models_flat_attribDryRun = pcopy_models_flat_attribDryRun
        End Get
        Set(value As String)
            pcopy_models_flat_attribDryRun = value
        End Set
    End Property
    '================================================================================
    'excelName: test_to_run_attrib
    'programName: 
    'XMLpath: 
    Public Property test_to_run_attrib() As String
        Get
            test_to_run_attrib = ptest_to_run_attrib
        End Get
        Set(value As String)
            ptest_to_run_attrib = value
        End Set
    End Property
    '================================================================================
    'excelName: test_to_run
    'programName: 
    'XMLpath: 
    Public Property test_to_run() As String
        Get
            test_to_run = ptest_to_run
        End Get
        Set(value As String)
            ptest_to_run = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Run Setup"
    'Run Setup
    Private pprogram_version As String
    Private pprogram_build As String
    Private pprogram_version_build As String
    Private pfileType As String
    Private _programfileType As String()
    Private pprogram_path As String
    Private pprogram_attribType As String
    Private prun_local_test As String
    Private pstart_distributed_test As String
    Private pjoin_distributed_test As String
    '================================================================================
    'excelName: program_version
    'programName: 
    'XMLpath: 
    Public Property program_version() As String
        Get
            program_version = pprogram_version
        End Get
        Set(value As String)
            pprogram_version = value
        End Set
    End Property
    '================================================================================
    'excelName: program_build
    'programName: 
    'XMLpath: NA
    Public Property program_build() As String
        Get
            program_build = pprogram_build
        End Get
        Set(value As String)
            pprogram_build = value
        End Set
    End Property
    '================================================================================
    'excelName: program_version_build
    'programName: 
    'XMLpath: NA
    Public Property program_version_build() As String
        Get
            program_version_build = pprogram_version_build
        End Get
        Set(value As String)
            pprogram_version_build = value
        End Set
    End Property
    '================================================================================
    'excelName: fileType
    'programName: 
    'XMLpath: NA
    Public Property fileType() As String
        Get
            fileType = pfileType
        End Get
        Set(value As String)
            pfileType = value
        End Set
    End Property
    '================================================================================
    'excelName: 
    'programName: 
    'XMLpath: NA
    Public Property programfileType As String()
        Get
            Return _programfileType
        End Get
        Set(value As String())
            _programfileType = value
        End Set
    End Property

    '================================================================================
    'excelName: program_path
    'programName: 
    'XMLpath: 
    Public Property program_path() As String
        Get
            program_path = pprogram_path
        End Get
        Set(value As String)
            pprogram_path = value
        End Set
    End Property
    '================================================================================
    'excelName: program_attribType
    'programName: 
    'XMLpath: 
    Public Property program_attribType() As String
        Get
            program_attribType = pprogram_attribType
        End Get
        Set(value As String)
            pprogram_attribType = value
        End Set
    End Property
    '================================================================================
    'excelName: run_local_test
    'programName: 
    'XMLpath: 
    Public Property run_local_test() As String
        Get
            run_local_test = prun_local_test
        End Get
        Set(value As String)
            prun_local_test = value
        End Set
    End Property
    '================================================================================
    'excelName: start_distributed_test
    'programName: 
    'XMLpath: 
    Public Property start_distributed_test() As String
        Get
            start_distributed_test = pstart_distributed_test
        End Get
        Set(value As String)
            pstart_distributed_test = value
        End Set
    End Property
    '================================================================================
    'excelName: join_distributed_test
    'programName: 
    'XMLpath: 
    Public Property join_distributed_test() As String
        Get
            join_distributed_test = pjoin_distributed_test
        End Get
        Set(value As String)
            pjoin_distributed_test = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Run Setup - Advanced Settings"
    'Run Setup - Advanced Settings
    Private pwrite_log_files As String
    Private pemail_notifications_attrib As String
    Private pruntime_limit_overwrites_multiplier As String
    Private pmaximum_permitted_runtime As String
    Private ppercent_difference_decimal_digits As String
    Private pprevious_test_results_file_path As String
    Private pprevious_test_results_file_path_attrib As String
    '================================================================================
    'excelName: write_log_files
    'programName: 
    'XMLpath: 
    Public Property write_log_files() As String
        Get
            write_log_files = pwrite_log_files
        End Get
        Set(value As String)
            pwrite_log_files = value
        End Set
    End Property
    '================================================================================
    'excelName: email_notifications_attrib
    'programName: 
    'XMLpath: 
    Public Property email_notifications_attrib() As String
        Get
            email_notifications_attrib = pemail_notifications_attrib
        End Get
        Set(value As String)
            pemail_notifications_attrib = value
        End Set
    End Property
    '================================================================================
    'excelName: runtime_limit_overwrites_multiplier
    'programName: 
    'XMLpath: 
    Public Property runtime_limit_overwrites_multiplier() As String
        Get
            runtime_limit_overwrites_multiplier = pruntime_limit_overwrites_multiplier
        End Get
        Set(value As String)
            pruntime_limit_overwrites_multiplier = value
        End Set
    End Property
    '================================================================================
    'excelName: maximum_permitted_runtime
    'programName: 
    'XMLpath: 
    Public Property maximum_permitted_runtime() As String
        Get
            maximum_permitted_runtime = pmaximum_permitted_runtime
        End Get
        Set(value As String)
            pmaximum_permitted_runtime = value
        End Set
    End Property
    '================================================================================
    'excelName: percent_difference_decimal_digits
    'programName: 
    'XMLpath: 
    Public Property percent_difference_decimal_digits() As String
        Get
            percent_difference_decimal_digits = ppercent_difference_decimal_digits
        End Get
        Set(value As String)
            ppercent_difference_decimal_digits = value
        End Set
    End Property
    '================================================================================
    'excelName: previous_test_results_file_path
    'programName: 
    'XMLpath: 
    Public Property previous_test_results_file_path() As String
        Get
            previous_test_results_file_path = pprevious_test_results_file_path
        End Get
        Set(value As String)
            pprevious_test_results_file_path = value
        End Set
    End Property
    '================================================================================
    'excelName: previous_test_results_file_path_attrib
    'programName: 
    'XMLpath: 
    Public Property previous_test_results_file_path_attrib() As String
        Get
            previous_test_results_file_path_attrib = pprevious_test_results_file_path_attrib
        End Get
        Set(value As String)
            pprevious_test_results_file_path_attrib = value
        End Set
    End Property
#End Region

#Region "Excel-Specific"
    'Excel-Specific
    '================================================================================

    'Excel-Specific - Model References
    Private _pathSuite As String
    Private ppathModels As String
    Private pnameRegTest As String
    Private pkeyword() As String
    Private pmodel_id() As String
    '================================================================================
    'excelName: pathSuite
    'programName: 
    'XMLpath: 
    Public Property pathSuite() As String
        Get
            Return _pathSuite
        End Get
        Set(value As String)
            _pathSuite = value
        End Set
    End Property
    '================================================================================
    'excelName: pathModels
    'programName: 
    'XMLpath: 
    Public Property pathModels() As String
        Get
            Return ppathModels
        End Get
        Set(value As String)
            ppathModels = value
        End Set
    End Property
    '================================================================================
    'excelName: nameRegTest
    'programName: 
    'XMLpath: 
    Public Property nameRegTest() As String
        Get
            Return pnameRegTest
        End Get
        Set(value As String)
            pnameRegTest = value
        End Set
    End Property
    '================================================================================
    'excelName: keyword
    'programName:
    'XMLpath: 
    Public Property keyword As String()
        Get
            Return pkeyword
        End Get
        Set(value As String())
            pkeyword = value
        End Set
    End Property
    '================================================================================
    'excelName: model_id
    'programName:
    'XMLpath: 
    Public Property model_id As String()
        Get
            Return pmodel_id
        End Get
        Set(value As String())
            pmodel_id = value
        End Set
    End Property
    '================================================================================

    'Excel-Specific - E-mail
    Private pemail_address() As String
    Private pemail_address_List() As String
    '================================================================================
    'excelName: email_address
    'programName: 
    'XMLpath:
    Public Property email_address As String()
        Get
            Return pemail_address
        End Get
        Set(value As String())
            pemail_address = value
        End Set
    End Property
    '================================================================================
    '================================================================================
    'excelName: email_address_List
    'programName: 
    'XMLpath:
    Public Property email_address_List As String()
        Get
            Return pemail_address_List
        End Get
        Set(value As String())
            pemail_address_List = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Excel-Specific - Report Style"

    'Excel-Specific - Report Style
    Private pbenchmark_contour_1 As String
    Private pbenchmark_contour_2 As String
    Private pbenchmark_contour_3 As String
    Private pbenchmark_contour_4 As String
    Private ptheoretical_contour_1 As String
    Private ptheoretical_contour_2 As String
    Private ptotal_run_time_change_ratio_contour_1 As String
    Private ptotal_run_time_change_ratio_contour_2 As String
    Private ptotal_run_time_change_ratio_contour_3 As String
    Private ptotal_run_time_change_ratio_contour_4 As String
    '================================================================================
    'excelName: benchmark_contour_1
    'programName: 
    'XMLpath: 
    Public Property benchmark_contour_1() As String
        Get
            benchmark_contour_1 = pbenchmark_contour_1
        End Get
        Set(value As String)
            pbenchmark_contour_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: benchmark_contour_2
    'programName: 
    'XMLpath: 
    Public Property benchmark_contour_2() As String
        Get
            benchmark_contour_2 = pbenchmark_contour_2
        End Get
        Set(value As String)
            pbenchmark_contour_2 = value
        End Set
    End Property
    '================================================================================
    'excelName: benchmark_contour_3
    'programName: 
    'XMLpath: 
    Public Property benchmark_contour_3() As String
        Get
            benchmark_contour_3 = pbenchmark_contour_3
        End Get
        Set(value As String)
            pbenchmark_contour_3 = value
        End Set
    End Property
    '================================================================================
    'excelName: benchmark_contour_4
    'programName: 
    'XMLpath: 
    Public Property benchmark_contour_4() As String
        Get
            benchmark_contour_4 = pbenchmark_contour_4
        End Get
        Set(value As String)
            pbenchmark_contour_4 = value
        End Set
    End Property
    '================================================================================
    'excelName: theoretical_contour_1
    'programName: 
    'XMLpath: 
    Public Property theoretical_contour_1() As String
        Get
            theoretical_contour_1 = ptheoretical_contour_1
        End Get
        Set(value As String)
            ptheoretical_contour_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: theoretical_contour_2
    'programName: 
    'XMLpath: 
    Public Property theoretical_contour_2() As String
        Get
            theoretical_contour_2 = ptheoretical_contour_2
        End Get
        Set(value As String)
            ptheoretical_contour_2 = value
        End Set
    End Property
    '================================================================================
    'excelName: total_run_time_change_ratio_contour_1
    'programName: 
    'XMLpath: 
    Public Property total_run_time_change_ratio_contour_1() As String
        Get
            total_run_time_change_ratio_contour_1 = ptotal_run_time_change_ratio_contour_1
        End Get
        Set(value As String)
            ptotal_run_time_change_ratio_contour_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: total_run_time_change_ratio_contour_2
    'programName: 
    'XMLpath: 
    Public Property total_run_time_change_ratio_contour_2() As String
        Get
            total_run_time_change_ratio_contour_2 = ptotal_run_time_change_ratio_contour_2
        End Get
        Set(value As String)
            ptotal_run_time_change_ratio_contour_2 = value
        End Set
    End Property
    '================================================================================
    'excelName: total_run_time_change_ratio_contour_3
    'programName: 
    'XMLpath: 
    Public Property total_run_time_change_ratio_contour_3() As String
        Get
            total_run_time_change_ratio_contour_3 = ptotal_run_time_change_ratio_contour_3
        End Get
        Set(value As String)
            ptotal_run_time_change_ratio_contour_3 = value
        End Set
    End Property
    '================================================================================
    'excelName: total_run_time_change_ratio_contour_4
    'programName: 
    'XMLpath: 
    Public Property total_run_time_change_ratio_contour_4() As String
        Get
            total_run_time_change_ratio_contour_4 = ptotal_run_time_change_ratio_contour_4
        End Get
        Set(value As String)
            ptotal_run_time_change_ratio_contour_4 = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Excel-Specific - Dropdown Lists"

    'Excel-Specific - Dropdown Lists
    Private pdropdownList_YesNo() As String
    Private pdropdownList_Location() As String
    Private pdropdownList_filterCombos() As String
    Private pdropdownList_programs() As String
    Private pdropdownList_SAP2000() As String
    Private pdropdownList_Bridge() As String
    Private pdropdownList_ETABS() As String
    Private pdropdownList_SAFE() As String
    Private pdropdownList_DistTestSource() As String
    Private pdropdownList_TestToRun() As String
    Private pdropdownList_classificationsHeader() As String
    Private pdropdownList_keywordsHeader() As String
    '================================================================================
    'excelName: dropdownList_YesNo
    'programName: 
    'XMLpath: 
    Public Property dropdownList_YesNo As String()
        Get
            Return pdropdownList_YesNo
        End Get
        Set(value As String())
            pdropdownList_YesNo = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_Location
    'programName: 
    'XMLpath: 
    Public Property dropdownList_Location As String()
        Get
            Return pdropdownList_Location
        End Get
        Set(value As String())
            pdropdownList_Location = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_filterCombos
    'programName: 
    'XMLpath: 
    Public Property dropdownList_filterCombos As String()
        Get
            Return pdropdownList_filterCombos
        End Get
        Set(value As String())
            pdropdownList_filterCombos = value
        End Set
    End Property
    '================================================================================
    'excelName: drowpdownList_programs
    'programName: 
    'XMLpath: 
    Public Property dropdownList_programs As String()
        Get
            Return pdropdownList_programs
        End Get
        Set(value As String())
            pdropdownList_programs = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_SAP
    'programName: 
    'XMLpath: 
    Public Property dropdownList_SAP2000 As String()
        Get
            Return pdropdownList_SAP2000
        End Get
        Set(value As String())
            pdropdownList_SAP2000 = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_Bridge
    'programName: 
    'XMLpath: 
    Public Property dropdownList_Bridge As String()
        Get
            Return pdropdownList_Bridge
        End Get
        Set(value As String())
            pdropdownList_Bridge = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_ETABS
    'programName: 
    'XMLpath: 
    Public Property dropdownList_ETABS As String()
        Get
            Return pdropdownList_ETABS
        End Get
        Set(value As String())
            pdropdownList_ETABS = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_SAFE
    'programName: 
    'XMLpath: 
    Public Property dropdownList_SAFE As String()
        Get
            Return pdropdownList_SAFE
        End Get
        Set(value As String())
            pdropdownList_SAFE = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_DistTestSource
    'programName: 
    'XMLpath: 
    Public Property dropdownList_DistTestSource As String()
        Get
            Return pdropdownList_DistTestSource
        End Get
        Set(value As String())
            pdropdownList_DistTestSource = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_TestToRun
    'programName: 
    'XMLpath: 
    Public Property dropdownList_TestToRun As String()
        Get
            Return pdropdownList_TestToRun
        End Get
        Set(value As String())
            pdropdownList_TestToRun = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_classificationsHeader
    'programName: 
    'XMLpath: 
    Public Property dropdownList_classificationsHeader As String()
        Get
            Return pdropdownList_classificationsHeader
        End Get
        Set(value As String())
            pdropdownList_classificationsHeader = value
        End Set
    End Property
    '================================================================================
    'excelName: dropdownList_keywordsHeader
    'programName: 
    'XMLpath: 
    Public Property dropdownList_keywordsHeader As String()
        Get
            Return pdropdownList_keywordsHeader
        End Get
        Set(value As String())
            pdropdownList_keywordsHeader = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Excel-Specific - Filter Model List"
    'Excel-Specific - Filter Model List
    Private pfilter_program() As String
    Private pfilter_program_items() As String
    Private pfilter_run_time_max() As String
    Private pfilter_run_time_total_max() As String
    Private pfilter_classification() As String
    Private pfilter_keyword_select As String
    Private pfilter_keyword_include() As String
    Private pfilter_keyword_exclude() As String
    Private plist_filter_add() As String
    Private plist_filter_id_secondary() As String
    Private plist_filter_title() As String
    Private plist_filter_program() As String
    Private plist_filter_run_time() As String
    Private plist_filter_total_time() As String
    Private plist_filter_id() As String
    '================================================================================
    'excelName: filter_program
    'programName: 
    'XMLpath: 
    Public Property filter_program() As String()
        Get
            Return pfilter_program
        End Get
        Set(value As String())
            pfilter_program = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_program_items
    'programName: 
    'XMLpath: 
    Public Property filter_program_items() As String()
        Get
            Return pfilter_program_items
        End Get
        Set(value As String())
            pfilter_program_items = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_run_time_max
    'programName: 
    'XMLpath: 
    Public Property filter_run_time_max() As String()
        Get
            Return pfilter_run_time_max
        End Get
        Set(value As String())
            pfilter_run_time_max = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_run_time_total_max
    'programName: 
    'XMLpath: 
    Public Property filter_run_time_total_max() As String()
        Get
            Return pfilter_run_time_total_max
        End Get
        Set(value As String())
            pfilter_run_time_total_max = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_classification
    'programName: 
    'XMLpath: 
    Public Property filter_classification() As String()
        Get
            Return pfilter_classification
        End Get
        Set(value As String())
            pfilter_classification = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_keyword_select
    'programName: 
    'XMLpath: 
    Public Property filter_keyword_select() As String
        Get
            Return pfilter_keyword_select
        End Get
        Set(value As String)
            pfilter_keyword_select = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_keyword_include
    'programName: 
    'XMLpath: 
    Public Property filter_keyword_include() As String()
        Get
            Return pfilter_keyword_include
        End Get
        Set(value As String())
            pfilter_keyword_include = value
        End Set
    End Property
    '================================================================================
    'excelName: filter_keyword_exclude
    'programName: 
    'XMLpath: 
    Public Property filter_keyword_exclude() As String()
        Get
            Return pfilter_keyword_exclude
        End Get
        Set(value As String())
            pfilter_keyword_exclude = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_add
    'programName: 
    'XMLpath: 
    Public Property list_filter_add() As String()
        Get
            Return plist_filter_add
        End Get
        Set(value As String())
            plist_filter_add = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_id_secondary
    'programName: 
    'XMLpath: 
    Public Property list_filter_id_secondary() As String()
        Get
            Return plist_filter_id_secondary
        End Get
        Set(value As String())
            plist_filter_id_secondary = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_title
    'programName: 
    'XMLpath: 
    Public Property list_filter_title() As String()
        Get
            Return plist_filter_title
        End Get
        Set(value As String())
            plist_filter_title = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_program
    'programName: 
    'XMLpath: 
    Public Property list_filter_program() As String()
        Get
            Return plist_filter_program
        End Get
        Set(value As String())
            plist_filter_program = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_run_time
    'programName: 
    'XMLpath: 
    Public Property list_filter_run_time() As String()
        Get
            Return plist_filter_run_time
        End Get
        Set(value As String())
            plist_filter_run_time = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_total_time
    'programName: 
    'XMLpath: 
    Public Property list_filter_total_time() As String()
        Get
            Return plist_filter_total_time
        End Get
        Set(value As String())
            plist_filter_total_time = value
        End Set
    End Property
    '================================================================================
    'excelName: list_filter_id
    'programName: 
    'XMLpath: 
    Public Property list_filter_id() As String()
        Get
            Return plist_filter_id
        End Get
        Set(value As String())
            plist_filter_id = value
        End Set
    End Property
    '================================================================================
#End Region

#Region "Excel-Specific - Fill Model Data"
    'Excel-Specific - Fill Model Data
    Private plistModelFilesinFolder_name() As String
    Private pid_1() As String
    Private pid_Secondary_1() As String
    Private ptitle_1() As String
    Private pprogram_1() As String
    Private pkeyword_1() As String
    '================================================================================
    'excelName: listModelFilesinFolder_name
    'programName: 
    'XMLpath: 
    Public Property listModelFilesinFolder_name() As String()
        Get
            Return plistModelFilesinFolder_name
        End Get
        Set(value As String())
            plistModelFilesinFolder_name = value
        End Set
    End Property
    '================================================================================
    'excelName: id_1
    'programName: 
    'XMLpath: 
    Public Property id_1() As String()
        Get
            Return pid_1
        End Get
        Set(value As String())
            pid_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: id_Secondary_1
    'programName: 
    'XMLpath: 
    Public Property id_Secondary_1() As String()
        Get
            Return pid_Secondary_1
        End Get
        Set(value As String())
            pid_Secondary_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: title_1
    'programName: 
    'XMLpath: 
    Public Property title_1() As String()
        Get
            Return ptitle_1
        End Get
        Set(value As String())
            ptitle_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: program_1
    'programName: 
    'XMLpath: 
    Public Property program_1() As String()
        Get
            Return pprogram_1
        End Get
        Set(value As String())
            pprogram_1 = value
        End Set
    End Property
    '================================================================================
    'excelName: keyword_1
    'programName: 
    'XMLpath: 
    Public Property keyword_1() As String()
        Get
            Return pkeyword_1
        End Get
        Set(value As String())
            pkeyword_1 = value
        End Set
    End Property
    '================================================================================
#End Region

    Public Function getDefaultFileType() As String
        Dim i As Long

        For i = 0 To UBound(pdropdownList_programs)
            If pdropdownList_programs(i) = pprogram_name Then pfileType = _programfileType(i)
        Next

        getDefaultFileType = pfileType
    End Function

#Region "Initialization"
    Public Sub New()
        '=== Populate arrays===
        '==== Dropdown list arrays
        ReDim pdropdownList_programs(3)
        pdropdownList_programs(0) = "SAP2000"
        pdropdownList_programs(1) = "CSiBridge"
        pdropdownList_programs(2) = "ETABS"
        pdropdownList_programs(3) = "SAFE"

        ReDim _programfileType(3)
        _programfileType(0) = ".sdb"
        _programfileType(1) = ".bdb"
        _programfileType(2) = ".edb"
        _programfileType(3) = ".fdb"

        ReDim pdropdownList_SAP2000(3)
        pdropdownList_SAP2000(0) = _programfileType(0)
        pdropdownList_SAP2000(1) = ".$2k"
        pdropdownList_SAP2000(2) = ".bdb"
        pdropdownList_SAP2000(3) = ".$br"

        ReDim pdropdownList_Bridge(3)
        pdropdownList_Bridge(0) = _programfileType(1)
        pdropdownList_Bridge(1) = ".$br"
        pdropdownList_Bridge(2) = ".sdb"
        pdropdownList_Bridge(3) = ".$2k"

        ReDim pdropdownList_ETABS(1)
        pdropdownList_ETABS(0) = _programfileType(2)
        pdropdownList_ETABS(1) = ".$et"

        ReDim pdropdownList_SAFE(1)
        pdropdownList_SAFE(0) = _programfileType(3)
        pdropdownList_SAFE(1) = ".$sf"

        ReDim dropdownList_TestToRun(2)
        dropdownList_TestToRun(0) = "run as is"
        dropdownList_TestToRun(1) = "update bridge"
        dropdownList_TestToRun(2) = "update bridge and run"

        ReDim dropdownList_DistTestSource(1)
        dropdownList_DistTestSource(0) = "local models database"
        dropdownList_DistTestSource(1) = "download from server"

        ReDim dropdownList_Location(1)
        dropdownList_Location(0) = "relative"
        dropdownList_Location(1) = "absolute"

        ReDim dropdownList_YesNo(1)
        dropdownList_YesNo(0) = "yes"
        dropdownList_YesNo(1) = "no"

        ReDim dropdownList_filterCombos(1)
        dropdownList_filterCombos(0) = "Any"
        dropdownList_filterCombos(1) = "All"

        ReDim dropdownList_classificationsHeader(2)
        dropdownList_classificationsHeader(0) = "Regression Model"
        dropdownList_classificationsHeader(1) = "Released Examples"
        dropdownList_classificationsHeader(2) = "In-House Examples"

        '===Set individual parameters

    End Sub
#End Region

End Class
