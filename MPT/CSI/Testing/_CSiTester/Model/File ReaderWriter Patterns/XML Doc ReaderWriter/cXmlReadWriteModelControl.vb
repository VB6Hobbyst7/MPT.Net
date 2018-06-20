Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.Xml

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.String
Imports MPT.String.ConversionLibrary
Imports MPT.XML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cFileAttachment
Imports CSiTester.cXMLCSi

Friend Class cXmlReadWriteModelControl
    Shared Event SharedLog(exception As LoggerEventArgs)

#Region "Methods: Friend"
    ''' <summary>
    ''' Reads the data from the specified XML file into an equivalent class that is in memory.
    ''' </summary>
    ''' <param name="p_path">Path to the model control XML.</param>
    ''' <remarks></remarks>
    Friend Shared Function ReadXMLFile(ByVal p_path As String,
                                       ByRef p_mcModel As cMCModel) As Boolean
        Try
            Dim xmlReader As New cXmlReadWrite()
            Dim xmlPathDir As String = p_mcModel.mcFile.pathDestination.directory
            Dim read As Boolean = True

            With xmlReader
                If xmlReader.InitializeXML(p_path) Then
                    ReadWriteMCModelXmlNodes(read, xmlPathDir, p_mcModel, xmlReader)
                    ReadWriteMCModelTestXmlLists(read, p_mcModel, xmlReader)
                    ReadWriteMCModelTestXmlObjects(read, xmlPathDir, p_mcModel, xmlReader)

                    'TEMP: for stripping out V13 from OS
                    'SaveXML(myPath)
                    '^^^^^^

                    xmlReader.CloseXML()
                    Return True
                End If
            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Saves the data from the model control XML class that is in memory back into the XML file at the destination path.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function SaveXMLFile(ByVal p_mcModel As cMCModel) As Boolean
        Try
            Dim xmlWriter As New cXmlReadWrite()
            Dim read As Boolean = False
            Dim xmlPathDir As String = p_mcModel.mcFile.pathDestination.directory

            If xmlWriter.InitializeXML(p_mcModel.mcFile.pathDestination.path) Then
                ReadWriteMCModelXmlNodes(read, xmlPathDir, p_mcModel, xmlWriter)
                ReadWriteMCModelTestXmlLists(read, p_mcModel, xmlWriter)
                ReadWriteMCModelTestXmlObjects(read, xmlPathDir, p_mcModel, xmlWriter)

                xmlWriter.SaveXML(p_mcModel.mcFile.pathDestination.path)
                xmlWriter.CloseXML()
                Return True
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Adds example results for Excel results if they are stored in the Model Control file.
    ''' </summary>
    ''' <param name="p_path">Path to the Model Control file.</param>
    ''' <remarks></remarks>
    Friend Shared Sub ReadResultsExcelXML(ByVal p_path As String,
                                          ByVal p_mcModel As cMCModel)
        Try
            Dim xmlReader As New cXmlReadWrite()
            If xmlReader.InitializeXML(p_path) Then
                ReadResultsExcel(p_mcModel, xmlReader)

                xmlReader.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Reads from or writes to model control *.XML, with unique properties.
    ''' </summary>
    ''' <param name="p_read">Specify whether to read values from XML or write values to XML.</param>
    ''' <param name="p_xmlPathDir">Path to the directory where the model control XMl resides. Images are stored relative to this.</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteMCModelXmlNodes(ByVal p_read As Boolean,
                                         ByRef p_xmlPathDir As String,
                                         ByRef p_mcModel As cMCModel,
                                         ByVal p_xmlReaderWriter As cXmlReadWrite)
        Try
            Dim pathNode As String
            Dim convertedValue As String
            Dim nodeLast As String

            '== Attributes ==
            pathNode = "//n:model"
            If p_read Then
                p_mcModel.xmlSchemaVersion = p_xmlReaderWriter.ReadNodeText(pathNode, "xml_schema_version")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.xmlSchemaVersion, pathNode, "xml_schema_version")
            End If

            If p_read Then
                p_mcModel.isPublic = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, "is_public"))
            Else
                p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(p_mcModel.isPublic, eCapitalization.alllower), pathNode, "is_public")
            End If

            If p_read Then
                p_mcModel.isBug = ConvertYesTrueBoolean(p_xmlReaderWriter.ReadNodeText(pathNode, "is_bug"))
            Else
                p_xmlReaderWriter.WriteNodeText(ConvertYesTrueString(p_mcModel.isBug, eCapitalization.alllower), pathNode, "is_bug")
            End If

            If p_read Then
                p_mcModel.statusExample = p_xmlReaderWriter.ReadNodeText(pathNode, "status")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.statusExample.ToLower, pathNode, "status")
            End If

            If p_read Then
                p_mcModel.statusDocumentation = p_xmlReaderWriter.ReadNodeText(pathNode, "documentation_status")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.statusDocumentation.ToLower, pathNode, "documentation_status")
            End If

            If p_read Then
                p_mcModel.xmlns = p_xmlReaderWriter.ReadNodeText(pathNode, "xmlns")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.xmlns, pathNode, "xmlns")
            End If

            If p_read Then
                p_mcModel.xmlnsXSI = p_xmlReaderWriter.ReadNodeText(pathNode, "xmlns:xsi")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.xmlnsXSI, pathNode, "xmlns:xsi")
            End If

            If p_read Then
                p_mcModel.xsiSchemaLocation = p_xmlReaderWriter.ReadNodeText(pathNode, "xsi:schemaLocation")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.xsiSchemaLocation, pathNode, "xsi:schemaLocation")
            End If


            '=== Basic Properties
            pathNode = "//n:model/n:id"
            If p_read Then
                p_mcModel.ID.idComposite = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                p_xmlReaderWriter.WriteNodeText(CStr(p_mcModel.ID.idComposite), pathNode, "")
            End If

            pathNode = "//n:model/n:id_secondary"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then p_mcModel.secondaryID = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    p_xmlReaderWriter.WriteNodeText(p_mcModel.secondaryID, pathNode, "")
                ElseIf Not String.IsNullOrEmpty(p_mcModel.secondaryID) Then
                    p_xmlReaderWriter.CreateNodeByPath("//n:model/n:id", "id_secondary", p_mcModel.secondaryID, eXMLElementType.Node, eNodeCreate.insertAfter)
                End If
            End If

            pathNode = "//n:model/n:title"
            If p_read Then
                p_mcModel.title = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                p_xmlReaderWriter.WriteNodeText(p_mcModel.title, pathNode, "")
            End If

            '= Paths =
            pathNode = "//n:model/n:path"
            nodeLast = pathNode
            If p_read Then
                Dim filePath As String = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                If Not String.IsNullOrWhiteSpace(filePath) Then
                    AbsolutePath(filePath, , p_xmlPathDir)
                End If
                p_mcModel.modelFile.pathDestination.SetProperties(filePath)
            Else
                convertedValue = p_mcModel.modelFile.pathDestination.path
                RelativePath(convertedValue, True, , p_xmlPathDir)
                p_xmlReaderWriter.WriteNodeText(convertedValue, pathNode, "")
            End If

            pathNode = "//n:model/n:excel_results/n:excel_file/n:path"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    Dim filePath As String = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                    If Not String.IsNullOrWhiteSpace(filePath) Then
                        AbsolutePath(filePath, , p_xmlPathDir)
                        p_mcModel.AddResultExcel(New cFileExcelResult(p_bindTo:=p_mcModel, p_pathFile:=filePath), True)
                    End If
                End If
            Else
                If p_mcModel.resultsExcel.Count > 0 Then
                    convertedValue = p_mcModel.resultsExcel.filePath
                    RelativePath(convertedValue, True, , p_xmlPathDir)
                    If p_xmlReaderWriter.NodeExists(pathNode) Then
                        p_xmlReaderWriter.WriteNodeText(convertedValue, pathNode, "")
                    Else
                        p_xmlReaderWriter.CreateNodeByPath("//n:model", "excel_results", "", eXMLElementType.Header, eNodeCreate.child)
                        p_xmlReaderWriter.CreateNodeByPath("//n:model/n:excel_results", "excel_file", "", eXMLElementType.Header, eNodeCreate.child)
                        p_xmlReaderWriter.CreateNodeByPath("//n:model/n:excel_results/n:excel_file", "path", convertedValue, eXMLElementType.Node, eNodeCreate.child)
                    End If
                End If
            End If

            '===
            pathNode = "//n:model/n:database_file_name"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    Dim filePath As String = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                    If Not String.IsNullOrWhiteSpace(filePath) Then
                        AbsolutePath(filePath, , p_xmlPathDir)
                        p_mcModel.dataSource.pathDestination.SetProperties(filePath)
                    End If
                End If
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    If IsWriteableTableFileName(p_mcModel) Then
                        p_xmlReaderWriter.WriteNodeText(p_mcModel.dataSource.pathDestination.fileNameWithExtension, pathNode, "")
                    Else
                        p_xmlReaderWriter.WriteNodeText("", pathNode, "")
                    End If
                ElseIf IsWriteableTableFileName(p_mcModel) Then
                    p_xmlReaderWriter.CreateNodeByPath(nodeLast, "database_file_name", p_mcModel.dataSource.pathDestination.fileNameWithExtension, eXMLElementType.Node, eNodeCreate.insertAfter)
                    nodeLast = pathNode

                End If
            End If

            pathNode = "//n:model/n:command_line"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then p_mcModel.commandLine = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    p_xmlReaderWriter.WriteNodeText(p_mcModel.commandLine, pathNode, "")
                Else
                    If Not String.IsNullOrEmpty(p_mcModel.commandLine) Then
                        p_xmlReaderWriter.CreateNodeByPath(nodeLast, "command_line", p_mcModel.commandLine, eXMLElementType.Node, eNodeCreate.insertAfter)
                        nodeLast = pathNode
                    End If
                End If
            End If

            pathNode = "//n:model/n:description"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then p_mcModel.description = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    p_xmlReaderWriter.WriteNodeText(p_mcModel.description, pathNode, "")
                Else
                    If Not String.IsNullOrEmpty(p_mcModel.description) Then
                        p_xmlReaderWriter.CreateNodeByPath(nodeLast, "description", p_mcModel.description, eXMLElementType.Node, eNodeCreate.insertAfter)
                        nodeLast = pathNode
                    End If
                End If
            End If

            pathNode = "//n:model/n:comments"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then p_mcModel.comments = p_xmlReaderWriter.ReadNodeText(pathNode, "")
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    p_xmlReaderWriter.WriteNodeText(p_mcModel.comments, pathNode, "")
                Else
                    If Not String.IsNullOrEmpty(p_mcModel.comments) Then
                        p_xmlReaderWriter.CreateNodeByPath(nodeLast, "comments", p_mcModel.comments, eXMLElementType.Node, eNodeCreate.insertAfter)
                        nodeLast = pathNode
                    End If
                End If
            End If

            '===
            With p_mcModel.classification
                pathNode = "//n:classification/n:value/n:level_1"
                If p_read Then
                    .level1 = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    p_xmlReaderWriter.WriteNodeText(.level1, pathNode, "")
                End If

                pathNode = "//n:classification/n:value/n:level_2"
                If p_read Then
                    .level2 = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    p_xmlReaderWriter.WriteNodeText(.level2, pathNode, "")
                End If
            End With

            With p_mcModel.program
                pathNode = "//n:model/n:program/n:name"
                If p_read Then
                    .programName = ConvertStringToEnumByDescription(Of eCSiProgram)(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
                Else
                    p_xmlReaderWriter.WriteNodeText(GetEnumDescription(.programName), pathNode, "")
                End If

                pathNode = "//n:model/n:program/n:version"
                If p_read Then
                    .programVersion = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    p_xmlReaderWriter.WriteNodeText(.programVersion, pathNode, "")
                End If

                pathNode = "//n:model/n:program/n:version_for_last_best_value"
                If p_read Then
                    If p_xmlReaderWriter.NodeExists(pathNode) Then .programVersionLastBest = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    If Not String.IsNullOrEmpty(.programVersionLastBest) Then
                        If p_xmlReaderWriter.NodeExists(pathNode) Then
                            p_xmlReaderWriter.WriteNodeText(.programVersionLastBest, pathNode, "")
                        ElseIf Not String.IsNullOrEmpty(.programVersionLastBest) Then
                            p_xmlReaderWriter.CreateNodeByPath("//n:model/n:program", "version_for_last_best_value", .programVersionLastBest, eXMLElementType.Node, eNodeCreate.child)
                        End If
                    End If
                End If
            End With

            With p_mcModel.author
                pathNode = "//n:model/n:author/n:name"
                If p_read Then
                    .name = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    p_xmlReaderWriter.WriteNodeText(.name, pathNode, "")
                End If

                pathNode = "//n:model/n:author/n:company"
                If p_read Then
                    .company = p_xmlReaderWriter.ReadNodeText(pathNode, "")
                Else
                    p_xmlReaderWriter.WriteNodeText(.company, pathNode, "")
                End If
            End With

            With p_mcModel.exampleDate
                pathNode = "//n:model/n:date/n:day"
                If p_read Then
                    If Not String.IsNullOrEmpty(p_xmlReaderWriter.ReadNodeText(pathNode, "")) Then .numDay = myCInt(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
                Else
                    p_xmlReaderWriter.WriteNodeText(CStr(.numDay), pathNode, "")
                End If

                pathNode = "//n:model/n:date/n:month"
                If p_read Then
                    If Not String.IsNullOrEmpty(p_xmlReaderWriter.ReadNodeText(pathNode, "")) Then .numMonth = myCInt(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
                Else
                    p_xmlReaderWriter.WriteNodeText(CStr(.numMonth), pathNode, "")
                End If

                pathNode = "//n:model/n:date/n:year"
                If p_read Then
                    If Not String.IsNullOrEmpty(p_xmlReaderWriter.ReadNodeText(pathNode, "")) Then .numYear = myCInt(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
                Else
                    p_xmlReaderWriter.WriteNodeText(CStr(.numYear), pathNode, "")
                End If

                nodeLast = "//n:model/n:date"
            End With

            pathNode = "//n:model/n:run_time/n:minutes"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    If Not String.IsNullOrEmpty(p_xmlReaderWriter.ReadNodeText(pathNode, "")) Then p_mcModel.runTime = myCDbl(p_xmlReaderWriter.ReadNodeText(pathNode, ""))
                End If
            Else
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    p_xmlReaderWriter.WriteNodeText(p_mcModel.runTime.ToString, pathNode, "")
                Else
                    If Not String.IsNullOrEmpty(p_mcModel.runTime.ToString) Then
                        If Not p_mcModel.runTime = 0 Then
                            p_xmlReaderWriter.CreateNodeByPath(nodeLast, "run_time", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                            p_xmlReaderWriter.CreateNodeByPath("//n:model/n:run_time", "minutes", p_mcModel.runTime.ToString, eXMLElementType.Node, eNodeCreate.child)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads from or writes to model control *.XML, with list properties.
    ''' </summary>
    ''' <param name="p_read">Specify whether to read values from XML or write values to XML.</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteMCModelTestXmlLists(ByVal p_read As Boolean,
                                             ByRef p_mcModel As cMCModel,
                                             ByVal p_xmlReaderWriter As cXmlReadWrite)
        'Reads & writes open-ended lists in the XML
        Dim pathNode As String
        Dim nameListNode As String
        Dim tempList As List(Of String)
        Dim tempOC As List(Of String)

        Try
            pathNode = "//n:model/n:tests"
            If p_read Then
                tempOC = New List(Of String)
                p_xmlReaderWriter.ReadNodeListText(pathNode, tempOC)
                p_mcModel.tests.Add(tempOC)
            ElseIf Not p_read Then
                tempList = New List(Of String)
                tempList = p_mcModel.tests.ToListAsRegTestTerm

                nameListNode = "test"
                p_xmlReaderWriter.WriteNodeListText(tempList.ToArray, pathNode, nameListNode)
            End If

            pathNode = "//n:model/n:keywords"
            If p_read Then
                If p_xmlReaderWriter.NodeExists(pathNode) Then
                    tempOC = New List(Of String)
                    p_xmlReaderWriter.ReadNodeListText(pathNode, tempOC)
                    p_mcModel.keywords.Add(tempOC)
                End If
            ElseIf Not p_read Then
                If Not p_xmlReaderWriter.NodeExists(pathNode) Then p_xmlReaderWriter.CreateNodeByPath("//n:model/n:classification", "keywords", "", eXMLElementType.Header, eNodeCreate.insertBefore)

                tempList = New List(Of String)
                For Each keyword As String In p_mcModel.keywords.NamesToObservableCollection()
                    tempList.Add(keyword)
                Next

                nameListNode = "keyword"
                p_xmlReaderWriter.WriteNodeListText(tempList.ToArray, pathNode, nameListNode)
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads from or writes to model control *.XML, with object properties. (e.g. results)
    ''' </summary>
    ''' <param name="p_read">Specify whether to read values from XML or write values to XML.</param>
    ''' <param name="p_xmlPathDir">Path to the directory where the model control XMl resides. Images are stored relative to this.</param>
    ''' <remarks></remarks>
    Private Shared Sub ReadWriteMCModelTestXmlObjects(ByVal p_read As Boolean,
                                                       ByRef p_xmlPathDir As String,
                                                       ByRef p_mcModel As cMCModel,
                                                       ByVal p_xmlReaderWriter As cXmlReadWrite)
        Try
            If p_read Then
                ReadIncidents(p_mcModel, p_xmlReaderWriter)
                ReadTickets(p_mcModel, p_xmlReaderWriter)
                ReadLinks(p_mcModel, p_xmlReaderWriter)
                ReadFileAttachments("image", p_mcModel.images, p_mcModel, p_xmlReaderWriter)
                ReadFileAttachments("attachment", p_mcModel.attachments, p_mcModel, p_xmlReaderWriter)
                'TEMP: for stripping out V13 from OS
                'WriteFileAttachments(xmlPathDir)
                '^^^^^^

                ReadTargetPrograms("program", p_mcModel, p_xmlReaderWriter)

                ReadUpdates(p_mcModel, p_xmlReaderWriter)
                p_mcModel.SetUpdateIDs()

                ReadResults(p_mcModel, p_xmlReaderWriter)
                'ReadResultsPostProcessed()     'TODO: Not finished.
                ReadResultsExcel(p_mcModel, p_xmlReaderWriter)  'This is read only
                p_mcModel.results.UpdateResultIDsAllIfEmpty()
            Else
                Dim nodeLast As String = "//n:model/n:classification"

                WriteIncidents(p_mcModel, p_xmlReaderWriter, nodeLast)
                WriteTickets(p_mcModel, p_xmlReaderWriter, nodeLast)
                WriteLinks(p_mcModel, p_xmlReaderWriter, nodeLast)
                WriteFileAttachments("image", p_mcModel.images.ToList, p_xmlReaderWriter, nodeLast)
                WriteFileAttachments("attachment", p_mcModel.attachments.ToList, p_xmlReaderWriter, nodeLast)
                WriteTargetPrograms("program", p_mcModel.targetProgram, p_xmlReaderWriter, nodeLast)

                WriteUpdates(p_mcModel, p_xmlReaderWriter, nodeLast)
                WriteResults(p_mcModel, p_xmlReaderWriter)
                'WriteResultsPostProcessed()     'TODO: Not implemented. Ready to test. Maybe after read function.
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private - Reading"

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadIncidents(ByRef p_mcModel As cMCModel,
                              ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "incident"
        Dim singleNodePath As String = "n:number"
        Dim myList As New List(Of String)

        p_xmlReader.ReadXmlObjectText(tagName, singleNodePath, myList, True)

        For Each incident As String In myList
            If IsNumeric(incident) Then p_mcModel.incidents.Add(myCInt(incident))
        Next
    End Sub

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadTickets(ByRef p_mcModel As cMCModel,
                            ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "ticket"
        Dim singleNodePath As String = "n:number"
        Dim myList As New List(Of String)

        p_xmlReader.ReadXmlObjectText(tagName, singleNodePath, myList, True)

        For Each ticket As String In myList
            If IsNumeric(ticket) Then p_mcModel.tickets.Add(myCInt(ticket))
        Next
    End Sub

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadLinks(ByRef p_mcModel As cMCModel,
                          ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "link"
        Dim myListTitle As New List(Of String)
        Dim myListURL As New List(Of String)

        'Get the list of all object entries, separated into lists by property
        p_xmlReader.ReadXmlObjectText(tagName, "n:title", myListTitle, True)
        p_xmlReader.ReadXmlObjectText(tagName, "n:url", myListURL, True)

        'Create new object for each entry, and assign listed properties to each object
        For i = 0 To myListTitle.Count - 1
            Dim newLink As New cMCLink
            newLink.title = myListTitle(i)
            newLink.URL = myListURL(i)
            p_mcModel.links.Add(newLink)
        Next
    End Sub

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadTargetPrograms(ByVal p_nameListNode As String,
                                          ByRef p_mcModel As cMCModel,
                                          ByVal p_xmlReader As cXmlReadWrite)
        Dim programNames As New List(Of String)

        Try
            'Get the list of all object entries, separated into lists by property
            Dim parentNode As New List(Of String)(New String() {"target_program"})
            p_xmlReader.ReadXmlObjectText(p_nameListNode, "n:name", programNames, p_useNameSpace:=True, p_parentNodes:=parentNode)

            'Create new object for each entry, and assign listed properties to each object
            p_mcModel.targetProgram.Clear()
            For Each programName As String In programNames
                p_mcModel.targetProgram.Add(ConvertStringToEnumByDescription(Of eCSiProgram)(programName))
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadFileAttachments(ByVal p_nameListNode As String,
                                           ByRef p_attachments As cMCAttachments,
                                            ByRef p_mcModel As cMCModel,
                                            ByVal p_xmlReader As cXmlReadWrite)
        Dim myListTitle As New List(Of String)
        Dim myListPath As New List(Of String)

        Try
            'Get the list of all object entries, separated into lists by property
            p_xmlReader.ReadXmlObjectText(p_nameListNode, "n:title", myListTitle, True)
            p_xmlReader.ReadXmlObjectText(p_nameListNode, "n:path", myListPath, True)

            'Create new object for each entry, and assign listed properties to each object
            For i = 0 To myListTitle.Count - 1
                Dim directoryType As eAttachmentDirectoryType
                directoryType = cFileAttachment.AttachmentDirectoryType(p_addAsImage:=True)

                Dim newAttachment As New cFileAttachment(myListPath(i), p_bindTo:=p_mcModel, p_attachmentType:=directoryType)
                newAttachment.title = myListTitle(i)

                'TEMP: For removing V13 in OS files.
                'If StringExistInName(newAttachment.title, outputSettingsVersionSession & fileNameSuffixOutputSettingsXml) Then
                '    If Not IO.File.Exists(xmlPathDir & "\" & dirNameModelsDest & "\" & newAttachment.path) Then
                '        newAttachment.title = FilterStringFromName(newAttachment.title, outputSettingsVersionSession, True, True)
                '        newAttachment.path = FilterStringFromName(newAttachment.path, outputSettingsVersionSession, True, True)
                '    End If
                'End If
                '^^^^^^^^^^

                p_attachments.Add(newAttachment)
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Creates new objects for the class and populates properties from those within the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadUpdates(ByRef p_mcModel As cMCModel,
                            ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "update"
        Dim myListPerson As New List(Of String)
        Dim myListTicket As New List(Of String)
        Dim myListComment As New List(Of String)
        Dim myListBuild As New List(Of String)
        Dim myListID As New List(Of String)

        Dim myDateDays As New List(Of String)
        Dim myDateMonths As New List(Of String)
        Dim myDateYears As New List(Of String)

        Try
            'Get the list of all object entries, separated into lists by property
            With p_xmlReader
                .ReadXmlObjectText(tagName, "n:person", myListPerson, True)
                .ReadXmlObjectText(tagName, "n:ticket", myListTicket, True)
                .ReadXmlObjectText(tagName, "n:comment", myListComment, True)
                .ReadXmlObjectText(tagName, "n:build", myListBuild, True)
                .ReadXmlObjectText(tagName, "n:id", myListID, True)

                .ReadXmlObjectText(tagName, "n:date/n:day", myDateDays, True)
                .ReadXmlObjectText(tagName, "n:date/n:month", myDateMonths, True)
                .ReadXmlObjectText(tagName, "n:date/n:year", myDateYears, True)
            End With

            'Create new object for each entry, and assign listed properties to each object
            For i = 0 To myListPerson.Count - 1
                Dim newUpdate As New cMCUpdate
                Dim newDate As New cMCDate

                With newDate
                    If Not String.IsNullOrEmpty(myDateDays(i)) Then .numDay = myCInt(myDateDays(i))
                    If Not String.IsNullOrEmpty(myDateMonths(i)) Then .numMonth = myCInt(myDateMonths(i))
                    If Not String.IsNullOrEmpty(myDateYears(i)) Then .numYear = myCInt(myDateYears(i))
                End With

                With newUpdate
                    .updateDate = newDate
                    .person = myListPerson(i)
                    If myListTicket.Count > i Then
                        If IsNumeric(myListTicket(i)) Then .ticket = myCInt(myListTicket(i))
                    End If
                    .comment = myListComment(i)
                    If myListBuild.Count > i Then .build = myListBuild(i)
                    If myListID.Count > i Then .id = CInt(myListID(i))
                End With

                p_mcModel.AddUpdate(newUpdate)
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates new Results objects for the class, read from the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadResults(ByRef p_mcModel As cMCModel,
                                   ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "result"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)
        parentNodes.Add("model")
        parentNodes.Add("results")

        Try
            'Get list of results nodes to read
            nodeList = p_xmlReader.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        Dim regularResult As cMCResult = CreateResult(tagName, node, nsmgr, p_mcModel, p_xmlReader)
                        'regularResult.resultType = eResultType.regular
                        p_mcModel.AddResult(regularResult, False)
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates new Results objects for the class from the regtest_internal_use/excel_results nodes, read from the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadResultsExcel(ByRef p_mcModel As cMCModel,
                                        ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "result"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)
        parentNodes.Add("model")
        parentNodes.Add("regtest_internal_use")
        parentNodes.Add("excel_results")

        Try
            'Get list of results nodes to read
            nodeList = p_xmlReader.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        Dim excelResult As cMCResultBasic = CreateExcelResult(tagName, node, nsmgr, p_mcModel, p_xmlReader)
                        'excelResult.resultType = eResultType.excelCalculated
                        p_mcModel.AddResult(excelResult, False)
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates new Results objects for the class and populates properties from those within the XML file. 
    ''' Also creates an accompanying ResultPostProcessed object that contains extra post-processing information for the result.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub ReadResultsPostProcessed(ByRef p_mcModel As cMCModel,
                                         ByVal p_xmlReader As cXmlReadWrite)
        Dim tagName As String = "result"
        Dim nodeList As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = Nothing
        Dim parentNodes As New List(Of String)

        parentNodes.Add("model")
        parentNodes.Add("postprocessed_results")

        Try
            'Get list of results nodes to read
            nodeList = p_xmlReader.GetResultsNodeList(tagName, nsmgr, parentNodes)

            If nodeList IsNot Nothing Then
                'Read the specified node within the node list to get the properties of each result
                If nodeList.Count > 0 Then
                    For Each node As XmlNode In nodeList
                        Dim postProcessedResult As cMCResult = CreateResultPostProcessed(tagName, node, nsmgr)
                        'postProcessedResult.resultType = eResultType.postProcessed

                        Dim postProcessedDetails As cMCResultPostProcessed = CreateResultDetailsPostProcessed(tagName, node, nsmgr)
                        'postProcessedResult.postProcessedDetails = postProcessedDetails

                        p_mcModel.AddResult(postProcessedResult, False)
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a result entry to be added to the list of results in the cMCModels class, one for each 'result' element included under 'results' within the model control XML file.
    ''' </summary>
    ''' <param name="p_tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="p_node">xmlNode object that corresponds to the result entry.</param>
    ''' <param name="p_nsmgr">namespace manager object that corresponds to the result entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateResult(ByVal p_tagName As String,
                                  ByVal p_node As XmlNode,
                                  ByVal p_nsmgr As XmlNamespaceManager,
                                  ByRef p_mcModel As cMCModel,
                                  ByVal p_xmlReader As cXmlReadWrite) As cMCResult
        Dim result As New cMCResult
        Dim lookupFields As New List(Of cFieldLookup)
        Dim resultUpdates As New List(Of cMCResultUpdate)
        Dim queryStringPartial As String = ""
        Dim nameReplacedCommentNode As Boolean = True

        Try
            'Create result object
            With result
                'Create header elements

                '' First, the 'comment' node has been renamed to 'name'. Check for both.
                Dim tempNodeValue As String = p_xmlReader.SelectSingleNode(p_node, "n:name", True, p_nsmgr, "")
                If String.IsNullOrEmpty(tempNodeValue) Then
                    tempNodeValue = p_xmlReader.SelectSingleNode(p_node, "n:comment", True, p_nsmgr, "")
                    nameReplacedCommentNode = False
                End If
                .name = tempNodeValue

                .id = p_xmlReader.SelectSingleNode(p_node, "n:id", True, p_nsmgr, "")

                .units = p_xmlReader.SelectSingleNode(p_node, "n:units", True, p_nsmgr, "")
                .unitsConversion = ConvertYesTrueBoolean(p_xmlReader.SelectSingleNode(p_node, "n:units", True, p_nsmgr, "units_conversion"))

                .tableName = p_xmlReader.SelectSingleNode(p_node, "n:table_name", True, p_nsmgr, "")

                'Create lookup fields object & result update object
                If p_mcModel.results.ResultsAllHaveUniqueIDs() Then
                    lookupFields = CreateResultFields(p_tagName, nameReplacedCommentNode, p_xmlReader, , .id)
                    resultUpdates = CreateResultUpdates(p_tagName, nameReplacedCommentNode, p_xmlReader, , .id)
                Else
                    lookupFields = CreateResultFields(p_tagName, nameReplacedCommentNode, p_xmlReader, .name)
                    resultUpdates = CreateResultUpdates(p_tagName, nameReplacedCommentNode, p_xmlReader, .name)
                End If

                'Add field lookup object and assemble query used to look up the collection of fields
                For Each fieldLookup As cFieldLookup In lookupFields
                    .query.Add(fieldLookup)

                    If Not String.IsNullOrEmpty(queryStringPartial) Then queryStringPartial = queryStringPartial & " AND "
                    queryStringPartial = queryStringPartial & fieldLookup.name & " = " & "'" & fieldLookup.valueField & "'"
                Next
                .query.SetQuery(queryStringPartial)

                'Add result update object
                For Each update As cMCResultUpdate In resultUpdates
                    .updates.Add(update)
                Next

                'Create field output object
                .benchmark = SetFieldOutput(p_node, p_nsmgr, p_xmlReader)
            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
        Return result
    End Function

    ''' <summary>
    ''' Creates a result entry to be added to the list of results in the cMCModels class, one for each 'result' element included under 'results' within the model control XML file.
    ''' </summary>
    ''' <param name="p_tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="p_node">xmlNode object that corresponds to the result entry.</param>
    ''' <param name="p_nsmgr">namespace manager object that corresponds to the result entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateExcelResult(ByVal p_tagName As String,
                                       ByVal p_node As XmlNode,
                                       ByVal p_nsmgr As XmlNamespaceManager,
                                       ByRef p_mcModel As cMCModel,
                                       ByVal p_xmlReader As cXmlReadWrite) As cMCResultBasic
        Dim result As New cMCResultBasic
        Dim lookupFields As New List(Of cFieldLookup)
        Dim resultUpdates As New List(Of cMCResultUpdate)
        Dim queryStringPartial As String = ""
        Dim nameReplacedCommentNode As Boolean = True

        Try
            'Create result object
            With result
                'Create header elements

                '' First, the 'comment' node has been renamed to 'name'. Check for both.
                Dim tempNodeValue As String = p_xmlReader.SelectSingleNode(p_node, "n:name", True, p_nsmgr, "")
                If String.IsNullOrEmpty(tempNodeValue) Then
                    tempNodeValue = p_xmlReader.SelectSingleNode(p_node, "n:comment", True, p_nsmgr, "")
                    nameReplacedCommentNode = False
                End If
                .name = tempNodeValue

                .id = p_xmlReader.SelectSingleNode(p_node, "n:id", True, p_nsmgr, "")

                .units = p_xmlReader.SelectSingleNode(p_node, "n:units", True, p_nsmgr, "")
                .unitsConversion = ConvertYesTrueBoolean(p_xmlReader.SelectSingleNode(p_node, "n:units", True, p_nsmgr, "units_conversion"))

                'Create result update object
                If p_mcModel.results.ResultsAllHaveUniqueIDs() Then
                    resultUpdates = CreateResultUpdates(p_tagName, nameReplacedCommentNode, p_xmlReader, , .id)
                Else
                    resultUpdates = CreateResultUpdates(p_tagName, nameReplacedCommentNode, p_xmlReader, .name)
                End If

                'Add result update object
                For Each update As cMCResultUpdate In resultUpdates
                    .updates.Add(update)
                Next

                'Create field output object
                .benchmark = SetFieldOutput(p_node, p_nsmgr, p_xmlReader)
            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
        Return result
    End Function

    Private Shared Function CreateResultPostProcessed(ByVal p_tagName As String,
                                      ByVal p_node As XmlNode,
                                      ByVal p_nsmgr As XmlNamespaceManager) As cMCResult
        Dim myresult As New cMCResult

        Return myresult
    End Function

    Private Shared Function CreateResultDetailsPostProcessed(ByVal p_tagName As String,
                                                      ByVal p_node As XmlNode,
                                                      ByVal p_nsmgr As XmlNamespaceManager) As cMCResultPostProcessed
        Dim myPostProcessedResult As New cMCResultPostProcessed

        Return myPostProcessedResult
    End Function

    ''' <summary>
    ''' Creates a collection of cMCResultUpdate objects, populated with values from the model control XML file.
    ''' </summary>
    ''' <param name="p_tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="p_nameReplacedCommentNode">If true, then the 'name' node is being used in the XML file. 
    ''' Otherwise, the 'comment' node is being used.</param>
    ''' <param name="p_resultName">Content of the 'name' child of the 'result' node, used to uniquely identify the result.</param>
    ''' <param name="p_resultID">ID of the 'result' node, used to uniquely identify the result.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateResultUpdates(ByVal p_tagName As String,
                                         ByVal p_nameReplacedCommentNode As Boolean,
                                         ByVal p_xmlReader As cXmlReadWrite,
                                         Optional ByVal p_resultName As String = "",
                                         Optional ByVal p_resultID As String = "-1") As List(Of cMCResultUpdate)
        Dim resultUpdates As New List(Of cMCResultUpdate)
        Dim myIDs As New List(Of String)
        Dim myComments As New List(Of String)

        Dim idNode As String = "n:id"
        Dim commentNode As String = "n:comment"
        Dim containerNode As String = "n:updates"

        Try
            'Get lists from lookup field sub-objects
            If Not p_resultID = "-1" Then
                myIDs = GetValuesListFromXMLObjectSubTag(p_tagName, CStr(p_resultID), True, p_nameReplacedCommentNode, idNode, containerNode, p_xmlReader)
                myComments = GetValuesListFromXMLObjectSubTag(p_tagName, CStr(p_resultID), True, p_nameReplacedCommentNode, commentNode, containerNode, p_xmlReader)
            ElseIf Not String.IsNullOrEmpty(p_resultName) Then
                myIDs = GetValuesListFromXMLObjectSubTag(p_tagName, p_resultName, False, p_nameReplacedCommentNode, idNode, containerNode, p_xmlReader)
                myComments = GetValuesListFromXMLObjectSubTag(p_tagName, p_resultName, False, p_nameReplacedCommentNode, commentNode, containerNode, p_xmlReader)
            Else
                Return resultUpdates
            End If

            'Add lookup field sub-objects
            For j = 0 To myIDs.Count - 1
                Dim resultUpdate As New cMCResultUpdate
                resultUpdate.id = myCInt(myIDs(j))
                If myComments.Count > j Then resultUpdate.comment = myComments(j)

                resultUpdates.Add(resultUpdate)
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

        Return resultUpdates
    End Function

    ''' <summary>
    ''' Creates a collection of cFieldLookup objects, populated with values from the model control XML file.
    ''' </summary>
    ''' <param name="p_tagName">Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="p_nameReplacedCommentNode">If true, then the 'name' node is being used in the XML file. 
    ''' Otherwise, the 'comment' node is being used.</param>
    ''' <param name="p_resultName">Content of the 'name' child of the 'result' node, used to uniquely identify the result.</param>
    ''' <param name="p_resultID">ID of the 'result' node, used to uniquely identify the result.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Shared Function CreateResultFields(ByVal p_tagName As String,
                                                  ByVal p_nameReplacedCommentNode As Boolean,
                                                  ByVal p_xmlReader As cXmlReadWrite,
                                                  Optional ByVal p_resultName As String = "",
                                                  Optional ByVal p_resultID As String = "-1") As List(Of cFieldLookup)
        Dim myLookupFields As New List(Of cFieldLookup)
        Dim myLookupFieldsName As New List(Of String)
        Dim myLookupFieldsValue As New List(Of String)

        Dim nameNode As String = "n:name"
        Dim commentNode As String = "n:comment"
        Dim valueNode As String = "n:value"
        Dim idNode As String = "n:id"
        Dim containerNode As String = "n:lookup_fields"

        Try
            'Get lists from lookup field sub-objects
            If Not p_resultID = "-1" Then
                myLookupFieldsName = GetValuesListFromXMLObjectSubTag(p_tagName, CStr(p_resultID), True, p_nameReplacedCommentNode, nameNode, containerNode, p_xmlReader)
                myLookupFieldsValue = GetValuesListFromXMLObjectSubTag(p_tagName, CStr(p_resultID), True, p_nameReplacedCommentNode, valueNode, containerNode, p_xmlReader)
            ElseIf Not String.IsNullOrEmpty(p_resultName) Then
                myLookupFieldsName = GetValuesListFromXMLObjectSubTag(p_tagName, p_resultName, False, p_nameReplacedCommentNode, nameNode, containerNode, p_xmlReader)
                myLookupFieldsValue = GetValuesListFromXMLObjectSubTag(p_tagName, p_resultName, False, p_nameReplacedCommentNode, valueNode, containerNode, p_xmlReader)
            Else
                Return myLookupFields
            End If

            'Add lookup field sub-objects
            For j = 0 To myLookupFieldsName.Count - 1
                Dim myLookupField As New cFieldLookup
                myLookupField.name = myLookupFieldsName(j)
                myLookupField.valueField = myLookupFieldsValue(j)
                myLookupFields.Add(myLookupField)
            Next
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

        Return myLookupFields
    End Function

    ''' <summary>
    ''' Returns a list of all values found for the specified node within the specifed container node within the parent node identified by the tag name and lookup value.
    ''' </summary>
    ''' <param name="p_tagName">>Name of the non-unique node element that corresponds to the result entry.</param>
    ''' <param name="p_tagNameLookupValue">Content of the child of the tagName node, used to uniquely identify the tagName node.</param>
    ''' <param name="p_useTagNameID">If true, then the lookup value is the id of the tag. Otherwise, it is the string name/comment value.</param>
    ''' <param name="p_nameReplacedCommentNode">If true, then the 'name' node is being used in the XML file. 
    ''' Otherwise, the 'comment' node is being used.</param>
    ''' <param name="p_nodeForList">The name of the node whose value is to be added to the list.</param>
    ''' <param name="p_containerNode">The name of the container node for the parent node that is of a repeating name.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetValuesListFromXMLObjectSubTag(ByVal p_tagName As String,
                                                       ByVal p_tagNameLookupValue As String,
                                                       ByVal p_useTagNameID As Boolean,
                                                       ByVal p_nameReplacedCommentNode As Boolean,
                                                       ByVal p_nodeForList As String,
                                                       ByVal p_containerNode As String,
                                                       ByVal p_xmlReader As cXmlReadWrite) As List(Of String)
        Dim nameNode As String = "n:name"
        Dim idNode As String = "n:id"
        Dim commentNode As String = "n:comment"
        Dim valuesList As New List(Of String)

        If p_nameReplacedCommentNode Then
            If p_useTagNameID Then
                p_xmlReader.ReadXmlObjectTextSubTag(p_tagName, idNode, p_tagNameLookupValue, p_nodeForList, valuesList, p_containerNode, True)
            Else
                p_xmlReader.ReadXmlObjectTextSubTag(p_tagName, nameNode, p_tagNameLookupValue, p_nodeForList, valuesList, p_containerNode, True)
            End If
        Else
            If p_useTagNameID Then
                p_xmlReader.ReadXmlObjectTextSubTag(p_tagName, idNode, p_tagNameLookupValue, p_nodeForList, valuesList, p_containerNode, True)
            Else
                p_xmlReader.ReadXmlObjectTextSubTag(p_tagName, commentNode, p_tagNameLookupValue, p_nodeForList, valuesList, p_containerNode, True)
            End If
        End If

        Return valuesList
    End Function

    ''' <summary>
    ''' Creates a cFieldOutput object to add to the cMCModel class, populated with values from the model control XML file.
    ''' </summary>
    ''' <param name="p_node">xmlNode object that corresponds to the result entry.</param>
    ''' <param name="p_nsmgr">namespace manager object that corresponds to the result entry.</param>
    ''' <param name="p_isExcelResult">If true, the results are being read in from the regtest_internal_use/excel_results element, so some slight changes in node path and properties gathered will occur.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SetFieldOutput(ByVal p_node As XmlNode,
                                            ByVal p_nsmgr As XmlNamespaceManager,
                                            ByVal p_xmlReader As cXmlReadWrite,
                                            Optional ByVal p_isExcelResult As Boolean = False) As cFieldOutput
        Dim isCorrectString As String
        Dim tempNodeValue As String
        Dim myFieldOutput As New cFieldOutput
        Dim nodePathStub As String = ""

        Try
            With myFieldOutput
                If Not p_isExcelResult Then nodePathStub = "n:output_field/"

                'General value properties
                If Not p_isExcelResult Then .name = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:name", True, p_nsmgr, "")

                tempNodeValue = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value", True, p_nsmgr, "shift_for_calculating_percent_difference")       'Optional element
                If Not String.IsNullOrEmpty(tempNodeValue) Then .shiftCalc = myCDbl(tempNodeValue)

                tempNodeValue = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value", True, p_nsmgr, "passing_percent_difference_range")               'Optional element
                If Not String.IsNullOrEmpty(tempNodeValue) Then .valuePassingPercentDifferenceRange = myCDbl(tempNodeValue)

                tempNodeValue = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value", True, p_nsmgr, "zero_tolerance")                                 'Optional element
                If Not String.IsNullOrEmpty(tempNodeValue) Then .zeroTolerance = myCDbl(tempNodeValue)

                'Benchmark Value
                .valueBenchmark = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:benchmark", True, p_nsmgr, "")
                isCorrectString = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:benchmark", True, p_nsmgr, "is_correct")
                .isCorrect = CType(ConvertYesNoUnknownEnum(isCorrectString), MPT.Enums.eYesNoUnknown)
                .roundBenchmark = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:benchmark", True, p_nsmgr, "significant_digits")               'Optional element

                'Theoretical Value
                .valueTheoretical = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:theoretical", True, p_nsmgr, "")
                .roundTheoretical = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:theoretical", True, p_nsmgr, "significant_digits")           'Optional element

                'Last Best Value
                If Not p_isExcelResult Then
                    .valueLastBest = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:last_best", True, p_nsmgr, "")                              'Optional element
                    .roundLastBest = p_xmlReader.SelectSingleNode(p_node, nodePathStub & "n:value/n:last_best", True, p_nsmgr, "significant_digits")            'Optional element
                End If
            End With
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try

        Return myFieldOutput
    End Function
#End Region


#Region "Methods: Private - Writing"
    ''' <summary>
    ''' Clears the existing 'incident' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteIncidents(ByRef p_mcModel As cMCModel,
                                        ByVal p_xmlWriter As cXmlReadWrite,
                                        Optional ByRef nodeLast As String = "")
        Dim pathnode = "//n:model/n:incidents"
        Dim nameListNode = "incident"

        If Not p_mcModel.incidents.Count = 0 Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath(nodeLast, "incidents", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                nodeLast = pathnode
            End If

            'Write new objects
            CreateObjectIncident(pathnode, p_mcModel.incidents.ToList)
        End If
    End Sub

    ''' <summary>
    ''' Clears the existing 'ticket' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteTickets(ByRef p_mcModel As cMCModel,
                                    ByVal p_xmlWriter As cXmlReadWrite,
                                    Optional ByRef nodeLast As String = "")
        Dim pathnode = "//n:model/n:tickets"
        Dim nameListNode = "ticket"

        If Not p_mcModel.tickets.Count = 0 Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath(nodeLast, "tickets", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                nodeLast = pathnode
            End If

            'Write new objects
            CreateObjectTicket(pathnode, p_mcModel.tickets.ToList)
        End If

    End Sub

    ''' <summary>
    ''' Clears the existing 'link' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteLinks(ByRef p_mcModel As cMCModel,
                            ByVal p_xmlWriter As cXmlReadWrite,
                            Optional ByRef nodeLast As String = "")
        Dim pathnode = "//n:model/n:links"
        Dim nameListNode = "link"

        If Not p_mcModel.links.Count = 0 Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath(nodeLast, "links", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                nodeLast = pathnode
            End If

            'Write new objects
            CreateObjectLink(pathnode, p_mcModel.links.ToList)
        End If
    End Sub

    ''' <summary>
    ''' Clears the existing 'program' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <param name="p_nameListNode">Singular name of the program node.</param>
    ''' <param name="p_programs"></param>
    ''' <param name="p_xmlWriter"></param>
    ''' <param name="nodeLast">Tracks ordering of the nodes.</param>
    ''' <remarks></remarks>
    Private Shared Sub WriteTargetPrograms(ByVal p_nameListNode As String,
                                            ByVal p_programs As cMCTargetPrograms,
                                            ByVal p_xmlWriter As cXmlReadWrite,
                                            Optional ByRef nodeLast As String = "")
        Dim nameListNodes = "target_program"
        Dim pathnode = "//n:model/n:" & nameListNodes

        Try
            If Not p_programs.Count = 0 Then
                If p_xmlWriter.NodeExists(pathnode) Then
                    'Clear existing objects
                    p_xmlWriter.ClearObjects(pathnode, p_nameListNode)
                Else
                    'Create header node since it does not exist
                    p_xmlWriter.CreateNodeByPath(nodeLast, nameListNodes, "", eXMLElementType.Header, eNodeCreate.insertAfter)
                    nodeLast = pathnode
                End If

                Dim programs As New List(Of String)
                For Each programName As eCSiProgram In p_programs
                    programs.Add(GetEnumDescription(programName))
                Next

                'Write new objects
                Dim xmlCSi As New cXMLCSi
                xmlCSi.CreateObjectTargetProgram(pathnode, programs, p_nameListNode)
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Clears the existing 'attachment' objects, if any, and writes a list of entirely new ones.
    ''' Such objects include attachments, images, and possibly others.
    ''' </summary>
    ''' <param name="p_nameListNode">Singular name of the file attachment node.</param>
    ''' <param name="p_attachments"></param>
    ''' <param name="p_xmlWriter"></param>
    ''' <param name="nodeLast">Tracks ordering of the nodes.</param>
    ''' <remarks></remarks>
    Private Shared Sub WriteFileAttachments(ByVal p_nameListNode As String,
                                            ByVal p_attachments As List(Of cFileAttachment),
                                            ByVal p_xmlWriter As cXmlReadWrite,
                                            Optional ByRef nodeLast As String = "")
        Dim nameListNodes = p_nameListNode & "s"
        Dim pathnode = "//n:model/n:" & nameListNodes

        Try
            If Not p_attachments.Count = 0 Then
                If p_xmlWriter.NodeExists(pathnode) Then
                    'Clear existing objects
                    p_xmlWriter.ClearObjects(pathnode, p_nameListNode)
                Else
                    'Create header node since it does not exist
                    p_xmlWriter.CreateNodeByPath(nodeLast, nameListNodes, "", eXMLElementType.Header, eNodeCreate.insertAfter)
                    nodeLast = pathnode
                End If

                'Write new objects
                Dim xmlCSi As New cXMLCSi
                xmlCSi.CreateObjectFileAttachment(pathnode, p_attachments, p_nameListNode)
            End If
        Catch ex As Exception
            RaiseEvent SharedLog(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Clears the existing 'update' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteUpdates(ByRef p_mcModel As cMCModel,
                                    ByVal p_xmlWriter As cXmlReadWrite,
                                    Optional ByRef nodeLast As String = "")
        Dim pathnode = "//n:model/n:updates"
        Dim nameListNode = "update"

        If Not p_mcModel.updates.Count = 0 Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath(nodeLast, "updates", "", eXMLElementType.Header, eNodeCreate.insertAfter)
                nodeLast = pathnode
            End If

            'Write new objects
            Dim xmlCSi As New cXMLCSi()
            xmlCSi.CreateObjectUpdate(pathnode, p_mcModel.updates.ToList)
        End If
    End Sub

    ''' <summary>
    ''' Clears the existing 'result' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteResults(ByRef p_mcModel As cMCModel,
                                    ByVal p_xmlWriter As cXmlReadWrite)
        Dim pathnode = "//n:model/n:results"
        Dim nameListNode = "result"

        If Not p_mcModel.results.resultsRegular.Count = 0 Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath("//n:model", "results", "", eXMLElementType.Header, eNodeCreate.child)
            End If

            'Write new objects
            CreateObjectResult(pathnode, p_mcModel.results.resultsRegular)
        End If
    End Sub

    ''' <summary>
    ''' Clears the existing 'postprocessed_result' objects, if any, and writes a list of entirely new ones.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub WriteResultsPostProcessed(ByRef p_mcModel As cMCModel,
                                                ByVal p_xmlWriter As cXmlReadWrite)
        Dim pathnode = "//n:model/n:postprocessed_results"
        Dim nameListNode = "result"
        Dim resultsPostProcessed As List(Of cMCResultPostProcessed) = p_mcModel.results.resultsPostProcessed

        If (Not resultsPostProcessed.Count = 0 OrElse
            Not p_mcModel.results.resultsPostProcessed.Count = 0) Then
            If p_xmlWriter.NodeExists(pathnode) Then
                'Clear existing objects
                p_xmlWriter.ClearObjects(pathnode, nameListNode)
            Else
                'Create header node since it does not exist
                p_xmlWriter.CreateNodeByPath("//n:model", "postprocessed_results", "", eXMLElementType.Header, eNodeCreate.child)
            End If

            'Write new objects
            CreateObjectResultPostProcessed(pathnode, resultsPostProcessed, p_mcModel.results.resultsPostProcessed)
        End If
    End Sub
#End Region

#Region "Methods - Private"
    ''' <summary>
    ''' True: The table file name exists and is not a default.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsWriteableTableFileName(ByVal p_mcModel As cMCModel) As Boolean
        Return (Not String.IsNullOrEmpty(p_mcModel.dataSource.pathDestination.fileNameWithExtension) AndAlso
                 Not p_mcModel.dataSource.PathExportedTable.isPathDefault)
    End Function
#End Region

End Class
