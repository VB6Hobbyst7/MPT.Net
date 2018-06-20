Option Explicit On
Option Strict On

Imports MPT.Reporting

Imports CSiTester.RegTest

Public Class SetterRegTest
    Shared Event SharedLog(exception As LoggerEventArgs)

    'Change Pattern
    '1. Set data file (sets most basic defaults)
    '2. Set cRegTest (settings should be same as the data file in the matching 'set' function)
    '                (changes in cRegTest that are not in synced 'set' functions will overwrite the data file when syncing)
    '   2a. 'Set' function is in this class and uses Converters to set values of cRegTest to the data file
    '3. Sync cRegTest (Does not matter. Synced when written out)
    '
    'Note: Patterns can derive from prior patterns by calling them first before setting additional/overwriting settings

    'TODO: Minimal values needed to initialize a file from scratch must be read into the program, store in the settings.xml file.
    '   Items:
    '       1. Schema location (regTest, model control, outputSettings)

#Region "Defaults - Standard"

    ''' <summary>
    ''' Sets up defaults for CSiTester.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SetDefaults(ByRef p_regTest As cRegTest,
                                  ByRef p_regTestXml As xmlRegTest)
        '1. Set Data File
        SetterRegTest.SetDefaults(p_regTestXml)

        '2. Sync to Data File
        SetterRegTest.SetDefaults(p_regTest)
    End Sub

    ''' <summary>
    ''' Sets up defaults for CSiTester in the regTest object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub SetDefaults(ByRef p_regTest As cRegTest)
        If p_regTest Is Nothing Then Exit Sub

        'TODO: Fill with any properties of p_regTest that are being changed in the data file
    End Sub
    ''' <summary>
    ''' Sets up defaults for CSiTester in the data object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub SetDefaults(ByRef p_regTestXml As xmlRegTest)
        Try
            If p_regTestXml Is Nothing Then Exit Sub

            'Transfer data to object
            With p_regTestXml
                With .actions
                    With .autogenerate_test_id
                        .run = yes_no.no
                    End With

                    ' Copying should always preserve folder structure to maintain accompanying files, so the following copying method should be done:
                    ' All turned off.
                    With .copy_models
                        .copy_current_test_models_only = yes_no.no
                        .copy_model_files_only = yes_no.no
                        .copy_selection_only = yes_no.no
                        .create_subdirectories_named_as_classification = yes_no.no
                        .reuse_xml_filelist = yes_no.no
                        .run = yes_no.no
                        .translate_model_xml_files = yes_no.no
                    End With

                    ' All turned off.
                    With .copy_models_flat
                        .dry_run = yes_no.no
                        .replace_model_ids_by_sequential_numbers = yes_no.no
                        .reuse_xml_filelist = yes_no.no
                        .run = yes_no.no
                        .translate_model_xml_files = yes_no.no
                    End With

                    With .copy_models_mirror
                        .run = yes_no.yes
                        .translate_model_xml_files = yes_no.no
                    End With

                    .join_distributed_test.run = yes_no.no

                    With .run_job
                        .run = yes_no.no
                        .tasklist_type = tasklist_type.dynamiccontinuoustasklist
                        .test_type = run_job_test_type.distributedtestwithlocalcontrol
                    End With

                    With .run_local_test
                        .run = yes_no.yes
                        .run_using_batch_file = yes_no.no
                    End With

                    .start_distributed_test.run = yes_no.no
                    .update_test_definition_to_use_latest_build.run = yes_no.no

                    .write_xml_files_list.run = yes_no.no
                End With

                With .advanced
                    .take_screenshots_after_model_run_timeouts = yes_no.no
                End With

                With .distributed_testing
                    .models_source = models_source.localmodelsdatabase
                End With

                With .general
                    With .models_run_directory.path
                        .create_subdirectory_named_as_test_id = yes_no.yes
                        .terminate_if_subdirectory_exists = yes_no.no
                    End With

                    With .output_directory
                        With .path
                            .create_subdirectory_named_as_test_id = yes_no.yes
                            .terminate_if_subdirectory_exists = yes_no.no
                            .Value = "out"
                        End With
                        .same_as_models_run_directory = yes_no.yes
                    End With

                    .write_log_files = yes_no.yes
                End With

                ''.SchemaLocation

                With .testing
                    With .email_notifications
                        .use = yes_no.no
                    End With

                    With .selections
                        With .keywords
                            .use = yes_no.no
                        End With

                        With .keywords_exclusive
                            .use = yes_no.no
                        End With

                        With .model_file_extensions
                            .use = yes_no.no
                        End With

                        With .model_id_range
                            .use = yes_no.no
                        End With

                        With .model_ids
                            .use = yes_no.yes
                        End With

                        With .program
                            .use = yes_no.no
                        End With

                        With .runtime_range
                            .use = yes_no.no
                        End With
                    End With

                    With .test_to_run
                        .dry_run = yes_no.no
                    End With
                End With
            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Test Run Results Action"
    ''' <summary>
    ''' Sets up parameters in regTest based on common testing scenarios.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SetTestRunResultsAction(ByRef p_regTest As cRegTest,
                                              ByRef p_regTestXml As xmlRegTest)
        '1. Set Data File
        SetterRegTest.SetTestRunResultsAction(p_regTestXml)

        '2. Sync to Data File
        SetterRegTest.SetTestRunResultsAction(p_regTest)
    End Sub

    ''' <summary>
    ''' Sets up defaults for CSiTester in the regTest object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub SetTestRunResultsAction(ByRef p_regTest As cRegTest)
        If p_regTest Is Nothing Then Exit Sub

        'TODO: Fill with any properties of p_regTest that are being changed in the data file
    End Sub
    ''' <summary>
    ''' Sets up defaults for CSiTester in the data object.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub SetTestRunResultsAction(ByRef p_regTestXml As xmlRegTest)
        Try
            If p_regTestXml Is Nothing Then Exit Sub

            'Transfer data to object
            With p_regTestXml

            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region
End Class


