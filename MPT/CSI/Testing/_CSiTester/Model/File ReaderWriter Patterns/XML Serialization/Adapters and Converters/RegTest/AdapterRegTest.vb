Option Explicit On
Option Strict On

Imports CSiTester.RegTest

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting
Imports MPT.String
Imports MPT.String.ConversionLibrary

' See: http://stackoverflow.com/questions/3187444/convert-xml-string-to-object#
' See: http://stackoverflow.com/questions/226599/deserializing-xml-to-objects-in-c-sharp

''' <summary>
''' Reads and writes the regTest XML file to/from the program's classes.
''' </summary>
''' <remarks></remarks>
Friend Class AdapterRegTest
    Inherits AdapterXmlFile

    ''' <summary>
    ''' Fills the provided object with data from the correspodning XML file.
    ''' </summary>
    ''' <param name="p_regTest">Object to fill.</param>
    ''' <param name="p_regTestXml">Deserialized XML file to use to fill the object.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Fill(ByRef p_regTest As cRegTest,
                           ByVal p_regTestXml As xmlRegTest)
        Try
            If (p_regTest Is Nothing OrElse
                  p_regTestXml Is Nothing) Then Exit Sub

            'Transfer data to object
            With p_regTestXml

            End With
        Catch ex As Exception
            OnSharedLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Writes the provided object out to an XML file.
    ''' </summary>
    ''' <param name="p_regTest">Object to write out.</param>
    ''' <param name="p_regTestXml">Serialized file originally read in. 
    ''' This will maintain data that was not read into program classes.</param>
    ''' <remarks></remarks>
    Friend Shared Sub Write(ByVal p_regTest As cRegTest,
                           ByRef p_regTestXml As xmlRegTest)
        Try
            If p_regTest Is Nothing Then Exit Sub

            Dim filePath As String = p_regTest.xmlFile.path
            If String.IsNullOrEmpty(filePath) Then Exit Sub

            Dim myRegTest As xmlRegTest = If(p_regTestXml, New xmlRegTest())

            'Transfer data to model data class
            With myRegTest
                With .actions

                    With .copy_models_mirror
                        .run = ConverterYesNo_ToFile(p_regTest.copy_models_mirror_attribRun)
                    End With

                    With .run_local_test
                        .run = ConverterYesNo_ToFile(p_regTest.run_local_test)
                        .run_using_batch_file = ConverterYesNo_ToFile(p_regTest.run_using_batch_file)
                    End With
                End With

                With .advanced
                    .control_file.path.Value = p_regTest.control_xml_file.path
                    .take_screenshots_after_model_run_timeouts = ConverterYesNo_ToFile(p_regTest.take_screenshots_after_model_run_timeouts)
                End With

                With .general
                    .computer_description = p_regTest.computer_description
                    .computer_id = p_regTest.computer_id
                    .models_database_directory.path.Value = p_regTest.models_database_directory.path

                    With .models_run_directory.path
                        .create_subdirectory_named_as_test_id = ConverterYesNo_ToFile(p_regTest.models_run_directory_attribCreateSubDir)
                        .terminate_if_subdirectory_exists = ConverterYesNo_ToFile(p_regTest.models_run_directory_attribNoReplace)
                        .Value = p_regTest.models_run_directory.path
                    End With

                    With .output_directory
                        With .path
                            .create_subdirectory_named_as_test_id = ConverterYesNo_ToFile(p_regTest.output_directory_attribCreateSubDir)
                            .terminate_if_subdirectory_exists = ConverterYesNo_ToFile(p_regTest.output_directory_attribNoReplace)
                            .Value = p_regTest.output_directory.path
                        End With
                        .same_as_models_run_directory = ConverterYesNo_ToFile(p_regTest.output_directory_attribSameAsModelsRun)
                    End With

                    .write_log_files = ConverterYesNo_ToFile(p_regTest.write_log_files)
                End With

                ''.SchemaLocation

                With .testing
                    'Gathered from CSiTesterSettings.XML  
                    '.command_line = p_regTest.command_line

                    With .email_notifications
                        .email_addresses = p_regTest.email_address_List
                        .use = ConverterYesNo_ToFile(p_regTest.email_notifications_attribUse)
                    End With

                    .previous_test_results_file.path.Value = p_regTest.previous_test_results_file.path

                    With .program
                        .build = p_regTest.program_build
                        .name = CType(ConverterProgramNames.ConvertToFile(p_regTest.program_name), program_name)
                        .path.Value = p_regTest.program_file.path
                        .version = p_regTest.program_version
                    End With

                    With .selections
                        With .model_ids
                            .model_id = p_regTest.model_id
                            .use = ConverterYesNo_ToFile(p_regTest.model_ids_attribUse)
                        End With
                    End With

                    .test_description = p_regTest.test_description
                    .test_id = p_regTest.test_id

                    With .test_to_run
                        .Value = p_regTest.test_to_run
                    End With
                End With
            End With
        Catch ex As Exception
            OnSharedLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    Friend Shared Function ConverterYesNo_ToFile(ByVal p_value As Boolean) As yes_no
        Return ConvertStringToEnumByDescription(Of yes_no)(ConvertYesTrueString(p_value, eCapitalization.alllower))
    End Function

    Friend Shared Function ConverterYesNo_FromFile(ByVal p_value As yes_no) As Boolean
        Return ConvertYesTrueBoolean(p_value.ToString())
    End Function

End Class
