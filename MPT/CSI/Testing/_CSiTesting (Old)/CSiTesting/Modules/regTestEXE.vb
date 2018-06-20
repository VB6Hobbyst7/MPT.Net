Option Explicit On
Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Module regTestEXE
    
    Dim xmlDoc As XmlDocument
    Dim xmlRoot As XmlElement
    Dim myXMLNode As XmlNode
#Region "Add these as buttons in a form, functions in a form class"
    Sub selectRegTest()
        'The regTest xml file to be specified is selected and listed in Excel. This allows for multiple regTest XML files in the same location with different parameters
        Dim fileName As String

        Call BrowseForFile("XML", Application.StartupPath & "\regtest")

        If path = "" Then path = regTest.nameRegTest 'Retains current value if user cancels out of browse form

        fileName = Right(path, Len(path) - (InStrRev(path, "\"))) 'Extracts only the filename from the path

        regTest.nameRegTest = fileName

    End Sub

    Sub getSuitePath()
        'Sets global variable and Excel field to contain the path to the Excel file
        pathSuite = Application.StartupPath
        If Right(pathSuite, 1) <> "\" Then pathSuite = pathSuite & "\"
        regTest.pathSuite = pathSuite
    End Sub

    Sub runRegTestAll()
        'Will run all the actions specified in the RegTest.exe configuration XML file
        Call runRegTest(True, regTest.nameRegTest)
    End Sub

    Sub runRegTestUpdate()
        'update-test-results-xml-file
        Call runRegTest(False, regTest.nameRegTest, True)
    End Sub

    Sub runRegTestValidate()
        'Validates model XML files in the models run directory. The validation HTML report is saved as <CONF_DIR>\out\validate_model_xml_files_list_for_models_run_directory.html
        Call runRegTest(False, regTest.nameRegTest, False, True)
    End Sub

#End Region

    Sub runRegTest(Optional All As Boolean = False, Optional regTestName As String = "", Optional Update As Boolean = False, Optional Validate As Boolean = False)
        'writes a batch file and runs the regTest program with the selected parameters
        Dim filePathName As String
        Dim fileName As String

        ChDir(regTest.pathSuite)
        fileName = "run_verification2.bat"
        filePathName = regTest.pathSuite & fileName

        ' Creates batch file
        Dim filehandle As Integer

        filehandle = FreeFile()

        ' Deletes Existing Batch File
        Call File.Delete(filePathName)

        Dim objWriter As New System.IO.StreamWriter(filePathName)
        objWriter.WriteLine("""" & regTest.pathSuite & "regtest.exe" & """")
        If Not regTestName = "" Then
            objWriter.WriteLine(" --conf-file " & """" & regTest.pathSuite & "regtest\" & regTestName & """")
        End If
        If All Then objWriter.WriteLine(" --run-actions-from-xml") 'Will run all the actions specified in the RegTest.exe configuration XML file
        If Validate Then objWriter.WriteLine(" --validate-model-xml-files-in-models-run-directory") 'Validates model XML files in the models run directory. The validation HTML report is saved as <CONF_DIR>\out\validate_model_xml_files_list_for_models_run_directory.html
        If Update Then objWriter.WriteLine(" --update-test-results-xml-file") 'Will update test results XML and HTML files by
        objWriter.Close()

        Call Run_Batch(filePathName, True)
    End Sub

    Sub initializeIniFile()
        'Checks if .ini file exists, and if not, writes one with default parameters. 
        'This is needed for locating the XML files
        Dim path As String
        path = Application.StartupPath & "\csiTest.ini"

        If Not File.Exists(path) Then
            Dim objWriter As New System.IO.StreamWriter(path)
            objWriter.WriteLine("regTest.xml") ' Append file string
            objWriter.Close()
        End If
    End Sub
    Sub changeIniFile(ByVal regTestName As String)
        'Changes .ini file parameter.
        'If user creates a new regTest.xml under a different name, the .xml to be used is referenced here
        Dim path As String
        path = Application.StartupPath & "\csiTest.ini"

        Dim objWriter As New System.IO.StreamWriter(path)
        objWriter.WriteLine(regTestName) ' Append file string
        objWriter.Close()
    End Sub
    Function readIniFile() As String
        'Reads the INI file to determine the name of the startup XML
        Dim path As String
        Dim checkBlanks As Boolean
        path = Application.StartupPath & "\csiTest.ini"

        Dim sr As New StreamReader(path)
        readIniFile = sr.ReadToEnd()
        readIniFile = Left(readIniFile, Len(readIniFile) - 1)

        checkBlanks = True
        While checkBlanks
            If Not Mid(readIniFile, Len(readIniFile), 1) = "l" Then
                readIniFile = Left(readIniFile, Len(readIniFile) - 1)
            Else
                checkBlanks = False
            End If
        End While

    End Function
    ''' <summary>
    ''' 'Reads the contents of the regText.XML (or custom user XML) into the regTest Class
    ''' </summary>
    ''' <param name="read">Set routine to read XML or write to it</param>
    ''' <remarks></remarks>
    Sub regtestXML_read_write_regTest(read As Boolean)
        Dim PathSuite As String
        Dim fileName As String

        Dim pathNode As String
        Dim pathNodeAttrib As String

        'Initialize XML
        PathSuite = Application.StartupPath
        regTest.pathSuite = PathSuite

        fileName = readIniFile()
        regTest.nameRegTest = fileName

        xmlPathName = PathSuite & "\regtest\" & fileName

        xmlDoc = New XmlDocument
        xmlDoc.Load(xmlPathName)

        xmlRoot = xmlDoc.DocumentElement

        'Iterate over desired cases

        pathNode = "//n:model_xml_file_schema_file/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.model_xml_file_schema_file_path_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.model_xml_file_schema_file_path_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:model_xml_file_schema_file/n:path"
        pathNodeAttrib = ""
        If read Then regTest.model_xml_file_schema_file_path = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.model_xml_file_schema_file_path, pathNode, pathNodeAttrib)

        pathNode = "//n:computer_id"
        pathNodeAttrib = ""
        If read Then regTest.computer_id = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.computer_id, pathNode, pathNodeAttrib)

        pathNode = "//n:computer_description"
        pathNodeAttrib = ""
        If read Then regTest.computer_description = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.computer_description, pathNode, pathNodeAttrib)

        pathNode = "//n:write_log_files"
        pathNodeAttrib = ""
        If read Then regTest.write_log_files = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.write_log_files, pathNode, pathNodeAttrib)

        pathNode = "//n:model_xml_file_schema_file/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.model_xml_file_schema_file_path_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.model_xml_file_schema_file_path_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:models_database_directory/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.models_database_directory_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.models_database_directory_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:models_database_directory/n:path"
        pathNodeAttrib = ""
        If read Then regTest.models_database_directory = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.models_database_directory, pathNode, pathNodeAttrib)

        pathNode = "//n:models_run_directory/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.models_run_directory_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.models_run_directory_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:models_run_directory/n:path"
        pathNodeAttrib = ""
        If read Then regTest.models_run_directory = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.models_run_directory, pathNode, pathNodeAttrib)

        'Actions
        pathNode = "//n:copy_models"
        pathNodeAttrib = "run"
        If read Then regTest.copy_models_AttribRun = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.copy_models_AttribRun, pathNode, pathNodeAttrib)

        'pathNode = "//n:copy_models"
        'pathNodeAttrib = "reuse_xml_filelist"
        'If read Then regTest.copy_models_attribReuse = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.copy_models_attribReuse, pathNode, pathNodeAttrib)

        pathNode = "//n:copy_models_flat"
        pathNodeAttrib = "run"
        If read Then regTest.copy_models_flat_attribRun = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.copy_models_flat_attribRun, pathNode, pathNodeAttrib)

        pathNode = "//n:copy_models_flat"
        pathNodeAttrib = "reuse_xml_filelist"
        If read Then regTest.copy_models_flat_attribReuse = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.copy_models_flat_attribReuse, pathNode, pathNodeAttrib)

        pathNode = "//n:copy_models_flat"
        pathNodeAttrib = "dry_run"
        If read Then regTest.copy_models_flat_attribDryRun = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.copy_models_flat_attribDryRun, pathNode, pathNodeAttrib)

        'pathNode = "//n:custom_model_file_directory_path"
        'pathNodeAttrib = "type"
        'If read Then regTest.custom_model_file_directory_path_attrib = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.custom_model_file_directory_path_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:write_xml_files_list"
        pathNodeAttrib = "run"
        If read Then regTest.write_xml_files_list_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.write_xml_files_list_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:run_local_test"
        pathNodeAttrib = "run"
        If read Then regTest.run_local_test = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.run_local_test, pathNode, pathNodeAttrib)

        pathNode = "//n:start_distributed_test"
        pathNodeAttrib = "run"
        If read Then regTest.start_distributed_test = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.start_distributed_test, pathNode, pathNodeAttrib)

        pathNode = "//n:join_distributed_test"
        pathNodeAttrib = "run"
        If read Then regTest.join_distributed_test = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.join_distributed_test, pathNode, pathNodeAttrib)


        'Testing
        pathNode = "//n:test_id"
        pathNodeAttrib = ""
        If read Then regTest.test_id = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.test_id, pathNode, pathNodeAttrib)

        pathNode = "//n:test_description"
        pathNodeAttrib = ""
        If read Then regTest.test_description = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.test_description, pathNode, pathNodeAttrib)

        pathNode = "//n:test_to_run"
        pathNodeAttrib = "dry_run"
        If read Then regTest.test_to_run_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.test_to_run_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:test_to_run"
        pathNodeAttrib = ""
        If read Then regTest.test_to_run = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.test_to_run, pathNode, pathNodeAttrib)

        pathNode = "//n:program/n:name"
        pathNodeAttrib = ""
        If read Then regTest.program_name = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.program_name, pathNode, pathNodeAttrib)

        pathNode = "//n:program/n:version"
        pathNodeAttrib = ""
        If read Then regTest.program_version = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.program_version, pathNode, pathNodeAttrib)

        pathNode = "//n:program/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.program_attribType = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.program_attribType, pathNode, pathNodeAttrib)

        pathNode = "//n:program/n:path"
        pathNodeAttrib = ""
        If read Then regTest.program_path = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.program_path, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:keywords"
        'pathNodeAttrib = "use"
        'If read Then regTest.keywords_attrib = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.keywords_attrib, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:model_ids"
        'pathNodeAttrib = "use"
        'If read Then regTest.model_ids_attrib = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.model_ids_attrib, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:model_id_range"
        'pathNodeAttrib = "use"
        'If read Then regTest.model_id_range_attrib = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.model_id_range_attrib, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:model_id_range/n:min_model_id"
        'pathNodeAttrib = ""
        'If read Then regTest.min_model_id = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.min_model_id, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:model_id_range/n:max_model_id"
        'pathNodeAttrib = ""
        'If read Then regTest.max_model_id = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.max_model_id, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:runtime_range"
        'pathNodeAttrib = "use"
        'If read Then regTest.runtime_range_attrib = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.runtime_range_attrib, pathNode, pathNodeAttrib)

        'pathNode = "//n:selections/n:runtime_range/n:min_runtime"
        'pathNodeAttrib = ""
        'If read Then regTest.min_runtime = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.min_runtime, pathNode, pathNodeAttrib)

        '        Case "max_runtime"
        'pathNode = "//n:selections/n:runtime_range/n:max_runtime"
        'pathNodeAttrib = ""
        'If read Then regTest.max_runtime = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.max_runtime, pathNode, pathNodeAttrib)

        pathNode = "//n:previous_test_results_file/n:path"
        pathNodeAttrib = "type"
        If read Then regTest.previous_test_results_file_path_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.previous_test_results_file_path_attrib, pathNode, pathNodeAttrib)

        pathNode = "//n:previous_test_results_file/n:path"
        pathNodeAttrib = ""
        If read Then regTest.previous_test_results_file_path = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.previous_test_results_file_path, pathNode, pathNodeAttrib)

        pathNode = "//n:runtime_limit_overwrites/n:multiplier"
        pathNodeAttrib = ""
        If read Then regTest.runtime_limit_overwrites_multiplier = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.runtime_limit_overwrites_multiplier, pathNode, pathNodeAttrib)

        pathNode = "//n:maximum_permitted_runtime/n:minutes"
        pathNodeAttrib = ""
        If read Then regTest.maximum_permitted_runtime = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.maximum_permitted_runtime, pathNode, pathNodeAttrib)

        pathNode = "//n:email_notifications"
        pathNodeAttrib = "use"
        If read Then regTest.email_notifications_attrib = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.email_notifications_attrib, pathNode, pathNodeAttrib)

        'Distributed Testing
        pathNode = "//n:distributed_testing/n:server_base_url"
        pathNodeAttrib = ""
        If read Then regTest.server_base_URL = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.server_base_URL, pathNode, pathNodeAttrib)

        pathNode = "//n:distributed_testing/n:models_source"
        pathNodeAttrib = ""
        If read Then regTest.models_source = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.models_source, pathNode, pathNodeAttrib)

        'Reporting
        pathNode = "//n:percent_difference_decimal_digits"
        pathNodeAttrib = ""
        If read Then regTest.percent_difference_decimal_digits = readNodeText(pathNode, pathNodeAttrib)
        If Not read Then writeNodeText(regTest.percent_difference_decimal_digits, pathNode, pathNodeAttrib)

        'pathNode = "//n:xml_stylesheet_files/n:test_results/n:path"
        'pathNodeAttrib = "type"
        'If read Then regTest.xml_stylesheet_files_attribType = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.xml_stylesheet_files_attribType, pathNode, pathNodeAttrib)

        'pathNode = "//n:xml_stylesheet_files/n:test_results/n:path"
        'pathNodeAttrib = ""
        'If read Then regTest.xml_stylesheet_files = readNodeText(pathNode, pathNodeAttrib)
        'If Not read Then writeNodeText(regTest.xml_stylesheet_files, pathNode, pathNodeAttrib)

        If Not read Then Call xmlDoc.Save(xmlPathName) 'Saves file if XML is modified
        xmlDoc = Nothing
        xmlRoot = Nothing

    End Sub

    Sub regtestXML_read_write_List(read As Boolean)
        'Reads & writes open-ended lists in the XML
        'Dim xmlNodeList As XmlNodeList
        'Dim objXMLelement As XmlElement
        'Dim objXMLtext As XmlText
        'Dim item As Object
        'Dim myNameSpace As String

        Dim fileName As String

        Dim pathNode As String
        Dim pathListNode As String
        Dim myList As New List(Of String)

        'Initialize XML
        pathSuite = regTest.pathSuite   'Disconnect after adding to button?
        fileName = regTest.nameRegTest
        xmlPathName = pathSuite & "\regtest\" & fileName

        '==== Set this boolean as an input. Determines whether the file is read or modified ===
        'read = True 'True means the XML is read. False means the XML is modified.

        xmlDoc = New XmlDocument
        xmlDoc.Load(xmlPathName)

        xmlRoot = xmlDoc.DocumentElement

        pathNode = "//n:testing/n:selections/n:keywords"
        If read Then
            readNodeListText(pathNode, myList)
            regTest.keyword = myList.ToArray
        ElseIf Not read Then
            pathListNode = pathNode & "/keyword"
            writeNodeListText(pathNode, pathListNode)
        End If

        pathNode = "//n:testing/n:selections/n:model_ids"
        If read Then
            readNodeListText(pathNode, myList)
            regTest.model_id = myList.ToArray
        ElseIf Not read Then
            pathListNode = pathNode & "/model_id"
            writeNodeListText(pathNode, pathListNode)
        End If

        pathNode = "//n:testing/n:email_notifications"  '/n:email_addresses"
        If read Then
            readNodeListText(pathNode, myList)
            regTest.email_address_List = myList.ToArray
        ElseIf Not read Then
            pathListNode = pathNode & "/email_address"
            writeNodeListText(pathNode, pathListNode)
        End If

        If Not read Then Call xmlDoc.Save(xmlPathName) 'Saves file if XML is modified
    End Sub


    Function readNodeText(ByVal pathNode As String, Optional ByVal pathNodeAttrib As String = "") As String
        'Selects node value assignment or attribute value assignment, based on specified node and optional attribute

        'Create an XmlNamespaceManager for resolving namespaces.
        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
        myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

        If pathNodeAttrib = "" Then
            readNodeText = myXMLNode.InnerText
        Else
            readNodeText = myXMLNode.Attributes.GetNamedItem(pathNodeAttrib).InnerText
        End If
    End Function
    Function readNodeListText(ByVal pathNode As String, ByRef myList As List(Of String)) As String
        'Handles multiple child nodes of the same name
        'Selects node value assignment or attribute value assignment, based on specified node and optional attribute
        Dim i As Long
        myList.Clear()  'Ensures that list starts out empty

        'Create an XmlNamespaceManager for resolving namespaces.
        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
        myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

        'Lookup node or attribute within XML file
        For i = 0 To myXMLNode.ChildNodes.Count - 1
            If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then myList.Add(myXMLNode.ChildNodes(i).InnerText())
        Next i

        readNodeListText = ""
    End Function

    Sub writeNodeText(ByVal propValue As String, ByVal pathNode As String, Optional ByVal pathNodeAttrib As String = "")
        'Write node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the regTest class object

        'Create an XmlNamespaceManager for resolving namespaces.
        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
        myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

        If pathNodeAttrib = "" Then
            myXMLNode.InnerText = propValue
        Else
            myXMLNode.Attributes.GetNamedItem(pathNodeAttrib).InnerText = propValue
        End If
    End Sub
    Sub writeNodeListText(ByVal pathNode As String, ByVal pathListNode As String)
        'Write node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the regTest class object

        'Create an XmlNamespaceManager for resolving namespaces.
        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
        myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

        'If pathNodeAttrib = "" Then
        '    myXMLNode.InnerText = propValue
        'Else
        '    myXMLNode.Attributes.GetNamedItem(pathNodeAttrib).InnerText = propValue
        'End If



        'Write new values to XML
        '    'Delete existing nodes in XML
        '    xmlNodeList = xmlDoc.SelectNodes(pathListNode)
        '    For Each item In xmlNodeList
        '        item.ParentNode.RemoveChild(item)
        '    Next

        '    'Insert new nodes in XML w text
        '    cellBlank = False
        '        cellText = "" 'Update - cellText = Range(namedCell).Offset(i, 0)

        '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing
        '            objXMLelement = xmlDoc.CreateNode("1", namedCell, myNameSpace)
        '            xmlNode.AppendChild(objXMLelement)

        '            objXMLtext = xmlDoc.CreateTextNode(cellText)
        '            objXMLelement.AppendChild(objXMLtext)

    End Sub
    Sub regtestXML_read_write_Objects(read As Boolean)
        'Reads & writes formatting properties of 
        'This sub assumes that the number of 'contour' formatting groups & criteria is fixed, so changes can only be made to existing criteria.
        'Could expand capability to allow user to insert additional 'contour' groups & criteria

        Dim namedCell As String
        Dim namedCellStub As String
        Dim pathNode As String

        Dim m As Long
        Dim i As Long
        Dim j As Long
        Dim n As Long

        Dim cellText As String
        Dim fileName As String

        'Initialize XML
        pathSuite = regTest.pathSuite   'Disconnect after adding to button?
        fileName = regTest.nameRegTest
        xmlPathName = pathSuite & "\regtest\" & fileName

        '==== Set this boolean as an input. Determines whether the file is read or modified ===
        'read = True 'True means the XML is read. False means the XML is modified.

        xmlDoc = New XmlDocument

        xmlDoc.Load(xmlPathName)

        xmlRoot = xmlDoc.DocumentElement

        For n = 0 To 2
            pathNode = "//regtest/reporting/model_results_table_cells_color_coding"
            Select Case n
                Case 0
                    pathNode = pathNode & "/absolute_percent_difference_from_benchmark_or_last_best_value_if_available"
                    namedCellStub = "benchmark_contour_"
                Case 1
                    pathNode = pathNode & "/absolute_percent_difference_from_theoretical"
                    namedCellStub = "theoretical_contour_"
                Case 2
                    pathNode = pathNode & "/total_run_time_change_ratio"
                    namedCellStub = "total_run_time_change_ratio_contour_"
                Case Else
                    pathNode = ""
                    namedCellStub = ""
            End Select

            'Lookup node or attribute within XML file
            xmlNode = xmlRoot.SelectSingleNode(pathNode)

            If read = True Then 'Read XML to Excel
                'Place values in sheet
                For j = 0 To xmlNode.ChildNodes.Count - 1
                    namedCell = namedCellStub & j + 1
                    xmlNode = xmlRoot.SelectSingleNode(pathNode).ChildNodes(j)
                    i = 1
                    For m = 0 To xmlNode.ChildNodes.Count - 1
                        'Update -                       Range(namedCell).Offset(i, 0) = xmlNode.ChildNodes(m).Value
                        i = i + 1
                    Next m
                Next j
            Else    'Write new values to XML
                For j = 0 To xmlNode.ChildNodes.Count - 1
                    namedCell = namedCellStub & j + 1
                    xmlNode = xmlRoot.SelectSingleNode(pathNode).ChildNodes(j)
                    i = 1
                    For m = 0 To xmlNode.ChildNodes.Count - 1
                        'Update -                         cellText = Range(namedCell).Offset(i, 0) 'Gather values from sheet
                        cellText = ""
                        xmlNode.ChildNodes(m).Value = cellText    'Set values in XML
                        i = i + 1
                    Next m
                Next j
            End If
        Next n
        If read = False Then Call xmlDoc.Save(xmlPathName) 'Saves file if XML is modified
    End Sub



    Function checkDropDownList() As String()
        Dim keyTitle As String
        Dim listName As String()

        keyTitle = regTest.program_name

        If keyTitle = "SAP2000" Then
            listName = regTest.dropdownList_SAP2000
        ElseIf keyTitle = "CSiBridge" Then
            listName = regTest.dropdownList_Bridge
        ElseIf keyTitle = "ETABS" Then
            listName = regTest.dropdownList_ETABS
        ElseIf keyTitle = "SAFE" Then
            listName = regTest.dropdownList_SAFE
        Else
            ReDim listName(0)
            listName(0) = ""
            MsgBox("Warning. No matching program type is selected. File type list will be empty")
        End If
        
        checkDropDownList = listName
    End Function

End Module
