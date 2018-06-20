Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.String
Imports MPT.String.ConversionLibrary
Imports MPT.XML.ReaderWriter

Friend Class cXmlReadWriteRegTest
    Shared Event SharedLog(exception As LoggerEventArgs)

#Region "Methods: Friend"
    ''' <summary>
    ''' Reads the data from the specified XML file into an equivalent class that is in memory.
    ''' </summary>
    ''' <param name="p_regTest"></param>
    ''' <remarks></remarks>
    Friend Shared Sub ReadXMLFile(ByRef p_regTest As cRegTest)
        Try
            Dim xmlReader As New cXmlReadWrite()
            If xmlReader.InitializeXML(p_regTest.xmlFile.path) Then
                ReadWriteRegTestXML(True, p_regTest, xmlReader)
                xmlReader.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub


    ''' <summary>
    ''' Saves the RegTest class values to the RegTest.XML
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SaveRegTestXML(ByRef p_regTest As cRegTest)
        Try
            Dim xmlWriter As New cXmlReadWrite()
            If xmlWriter.InitializeXML(p_regTest.xmlFile.path) Then
                ReadWriteRegTestXML(False, p_regTest, xmlWriter)
                xmlWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub


    ''' <summary>
    ''' Saves the regTest XML with only updating the relevant copy options for whether or not destination folder initialization is needed.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SaveFolderInitializationSettingsToXML(ByRef p_regTest As cRegTest)
        Dim xmlWriter As New cXmlReadWrite()

        With xmlWriter
            If .InitializeXML(p_regTest.xmlFile.path) Then
                .WriteNodeText(ConvertYesTrueString(p_regTest.copy_models_mirror_attribRun, eCapitalization.alllower), "//n:copy_models_mirror", "run")
                .SaveXML(p_regTest.xmlFile.path)
                .CloseXML()
            End If
        End With

    End Sub

    ''' <summary>
    ''' Returns the path of the regTest validation results.
    ''' </summary>
    ''' <param name="p_pathRegTestValidation">The path of the regTest file used to perform the validation.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function RegTestResultsPathFromXML(ByVal p_pathRegTestValidation As String) As String
        Dim xmlReader As New cXmlReadWrite()
        Dim resultsPath As String = ""

        xmlReader.GetSingleXMLNodeValue(p_pathRegTestValidation, "//n:general/n:output_directory/n:path", resultsPath, , True)
        Return resultsPath
    End Function
#End Region



#Region "Methods: Private"
    ''' <summary>
    ''' Populates regTest class with data from the regTest XML
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteRegTestXML(ByVal read As Boolean,
                                           ByRef p_regTest As cRegTest,
                                           ByVal p_xmlReaderWriter As cXmlReadWrite)
        Try
            ReadWriteRegTestXmlNodes(read, p_regTest, p_xmlReaderWriter)
            ReadWriteRegTestXmlLists(read, p_regTest, p_xmlReaderWriter)
            'ReadWriteExampleXmlObject(read)
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

        If Not read Then
            p_xmlReaderWriter.SaveXML(p_regTest.xmlFile.path)
        End If

    End Sub

    ''' <summary>
    ''' Reads from or writes to regTest.XML, with unique properties.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteRegTestXmlNodes(ByVal read As Boolean,
                                                ByRef p_regTest As cRegTest,
                                                ByVal p_xmlReaderWriter As cXmlReadWrite)
        Dim pathNode As String
        Dim pathNodeAttrib As String

        With p_regTest
            'Determine relative path offset for regTest.xml from CSiTester.exe
            Dim pathregTestXML As String = cPathRegTest.pathRelativeToRegTest(p_regTest)

            '= Paths =
            '== General ==
            pathNode = "//n:models_database_directory/n:path"
            If read Then
                .models_database_directory.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
            Else
                p_xmlReaderWriter.WriteNodeText(.models_database_directory.pathRelative(pathregTestXML), pathNode, "")
            End If

            pathNode = "//n:models_run_directory/n:path"
            If read Then

                'The following conversion might fail if the file has been copied and then re-read from the new location without first being saved. 
                'In this case, manually carry over the path from before re-initialization of the class
                If Not .reInitialize Then
                    .models_run_directory.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
                End If
            Else
                p_xmlReaderWriter.WriteNodeText(.models_run_directory.pathRelative(pathregTestXML), pathNode, "")
            End If


            pathNode = "//n:output_directory/n:path"
            If read Then
                'The following conversion might fail if the file has been copied and then re-read from the new location without first being saved. 
                'In this case, manually carry over the path from before re-initialization of the class
                If Not .reInitialize Then
                    .output_directory.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
                End If
            Else
                p_xmlReaderWriter.WriteNodeText(.output_directory.pathRelative(pathregTestXML), pathNode, "")
            End If

            '== Testing ==
            pathNode = "//n:program/n:path"
            If read Then
                If String.IsNullOrEmpty(.program_file.path) Then                               'Program path may have already been specified if location of tester has changed.
                    'The following conversion might fail if the file has been copied and then re-read from the new location without first being saved. 
                    'In this case, manually carry over the path from before re-initialization of the class
                    If Not .reInitialize Then
                        .program_file.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
                    End If
                End If
            Else
                p_xmlReaderWriter.WriteNodeText(.program_file.pathRelative(pathregTestXML), pathNode, "")
            End If

            pathNode = "//n:previous_test_results_file/n:path"
            If read Then
                .previous_test_results_file.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
            Else
                p_xmlReaderWriter.WriteNodeText(.previous_test_results_file.pathRelative(pathregTestXML), pathNode, "")
            End If

            '== Advanced ==
            pathNode = "//n:advanced/n:control_file/n:path"
            If read Then
                'The following conversion might fail if the file has been copied and then re-read from the new location without first being saved. 
                'In this case, manually carry over the path from before re-initialization of the class
                If Not .reInitialize Then
                    .control_xml_file.SetProperties(p_xmlReaderWriter.ReadNodeText(pathNode, ""), pathregTestXML)
                End If
            Else
                p_xmlReaderWriter.WriteNodeText(.control_xml_file.pathRelative(pathregTestXML), pathNode, "")
            End If


            '= General Properties =
            pathNodeAttrib = ""

            '== General ==
            pathNode = "//n:computer_id"
            If read Then .computer_id = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.computer_id, pathNode, "")

            pathNode = "//n:computer_description"
            If read Then .computer_description = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.computer_description, pathNode, "")

            pathNode = "//n:write_log_files"
            If read Then .write_log_files = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.write_log_files, eCapitalization.alllower), pathNode, "")

            pathNode = "//n:output_directory"
            If read Then .output_directory_attribSameAsModelsRun = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, "same_as_models_run_directory"))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.output_directory_attribSameAsModelsRun, eCapitalization.alllower), pathNode, "same_as_models_run_directory")

            pathNode = "//n:output_directory/n:path"
            If read Then .output_directory_attribCreateSubDir = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, "create_subdirectory_named_as_test_id"))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.output_directory_attribCreateSubDir, eCapitalization.alllower), pathNode, "create_subdirectory_named_as_test_id")

            pathNode = "//n:output_directory/n:path"
            If read Then .output_directory_attribNoReplace = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, "terminate_if_subdirectory_exists"))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.output_directory_attribNoReplace, eCapitalization.alllower), pathNode, "terminate_if_subdirectory_exists")

            '== Actions ==
            pathNode = "//n:copy_models_mirror"
            pathNodeAttrib = "run"
            If read Then .copy_models_mirror_attribRun = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, pathNodeAttrib))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.copy_models_mirror_attribRun, eCapitalization.alllower), pathNode, pathNodeAttrib)

            pathNode = "//n:run_local_test"
            pathNodeAttrib = "run"
            If read Then .run_local_test = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, pathNodeAttrib))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.run_local_test, eCapitalization.alllower), pathNode, pathNodeAttrib)
            pathNodeAttrib = "run_using_batch_file"
            If read Then .run_using_batch_file = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, pathNodeAttrib))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.run_using_batch_file, eCapitalization.alllower), pathNode, pathNodeAttrib)

            '== Testing ==
            pathNode = "//n:test_id"
            If read Then .test_id = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.test_id, pathNode, "")

            pathNode = "//n:test_description"
            If read Then .test_description = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.test_description, pathNode, "")

            pathNode = "//n:test_to_run"
            If read Then .test_to_run = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.test_to_run, pathNode, "")

            pathNode = "//n:program/n:name"
            If read Then .program_name = ConvertStringToEnumByDescription(Of eCSiProgram)(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
            If Not read Then p_xmlReaderWriter.WriteNodeText(GetEnumDescription(.program_name), pathNode, "")

            pathNode = "//n:program/n:version"
            If read Then .program_version = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.program_version, pathNode, "")

            pathNode = "//n:program/n:build"
            If read Then .program_build = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.program_build, pathNode, "")

            pathNode = "//n:command_line"
            'Gathered from CSiTesterSettings.XML  
            If Not read Then
                'command_line = testerSettings.commandRunAnalysis
                p_xmlReaderWriter.WriteNodeText(.command_line, pathNode, "")
            End If

            '=== Selections ===
            pathNode = "//n:selections/n:model_ids"
            pathNodeAttrib = "use"
            If read Then .model_ids_attribUse = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, pathNodeAttrib))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.model_ids_attribUse, eCapitalization.alllower), pathNode, pathNodeAttrib)

            '=== Runtime Limit Overwrites ===
            pathNode = "//n:email_notifications"
            pathNodeAttrib = "use"
            If read Then .email_notifications_attribUse = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, pathNodeAttrib))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.email_notifications_attribUse, eCapitalization.alllower), pathNode, pathNodeAttrib)

            '== Reporting ==
            pathNode = "//n:percent_difference_decimal_digits"
            If read Then .percent_difference_decimal_digits = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            If Not read Then p_xmlReaderWriter.WriteNodeText(.percent_difference_decimal_digits, pathNode, "")

            '== Advanced
            pathNode = "//n:take_screenshots_after_model_run_timeouts"
            If read Then .take_screenshots_after_model_run_timeouts = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
            If Not read Then p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(.take_screenshots_after_model_run_timeouts, eCapitalization.alllower), pathNode, "")
        End With
    End Sub

    ''' <summary>
    ''' Reads from or writes to regTest.XML, with properties lists.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteRegTestXmlLists(ByVal read As Boolean,
                                                ByRef p_regTest As cRegTest,
                                                ByVal p_xmlReaderWriter As cXmlReadWrite)
        'Reads & writes open-ended lists in the XML
        Dim pathNode As String
        Dim nameListNode As String
        Dim myList As New List(Of String)

        With p_regTest
            pathNode = "//n:testing/n:selections/n:model_ids"
            myList = New List(Of String)
            If read Then
                p_xmlReaderWriter.ReadNodeListText(pathNode, myList)
                .model_id = myList.ToArray
            ElseIf Not read Then
                nameListNode = "model_id"
                p_xmlReaderWriter.WriteNodeListText(.model_id, pathNode, nameListNode)
            End If

            pathNode = "//n:testing/n:email_notifications/n:email_addresses"
            myList = New List(Of String)
            If read Then
                p_xmlReaderWriter.ReadNodeListText(pathNode, myList)
                .email_address_List = myList.ToArray
            ElseIf Not read Then
                nameListNode = "email_address"
                p_xmlReaderWriter.WriteNodeListText(.email_address_List, pathNode, nameListNode)
            End If
        End With

    End Sub

    ''' <summary>
    ''' INCOMPLETE: Reads from or writes to regTest.XML, with properties objects, which may contain lists.
    ''' </summary>
    ''' <param name="read">Specify whether to read values from XML or write values to XML</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteRegTestXmlObject(ByVal read As Boolean,
                                                 ByVal p_xmlReaderWriter As cXmlReadWrite)
        'Reads & writes formatting properties of 
        'This sub assumes that the number of 'contour' formatting groups & criteria is fixed, so changes can only be made to existing criteria.
        'Could expand capability to allow user to insert additional 'contour' groups & criteria

        Dim namedCellStub As String
        Dim pathNode As String
        Dim pathNodeParent As String



        pathNodeParent = "//n:regtest/n:reporting/n:model_results_table_cells_color_coding"

        pathNode = pathNodeParent & "/n:absolute_percent_difference_from_benchmark_or_last_best_value_if_available"
        'namedCellStub = "benchmark_contour_"
        If read Then p_xmlReaderWriter.ReadXmlObjectText(pathNode, "")


        pathNode = pathNodeParent & "/n:absolute_percent_difference_from_theoretical"
        ' namedCellStub = "theoretical_contour_"

        pathNode = pathNodeParent & "/n:total_run_time_change_ratio"
        'namedCellStub = "total_run_time_change_ratio_contour_"

        pathNode = ""
        namedCellStub = ""

    End Sub
#End Region
End Class
