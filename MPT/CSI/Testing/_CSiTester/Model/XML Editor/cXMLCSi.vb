Option Strict On
Option Explicit On

Imports System.Xml
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.String
Imports MPT.String.ConversionLibrary
Imports MPT.XML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cSettings
Imports CSiTester.cMCModel


Public Class cXMLCSi
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Fields"
    Private _xmlReaderWriter As New cXmlReadWrite()
#End Region

#Region "Properties"
    Public Shared Property xmlDoc As XmlDocument
    Public Shared Property xmlRoot As XmlElement
    Public Shared Property myXMLNode As XmlNode
    Public Shared Property myXMLObject As XmlNode

    Public Shared Property myXMLFileNode As cXMLNode
    Public Shared Property xmlFile As cXMLObject
#End Region

#Region "CSiTester"

    '=== CSiTesterSettings.xml
    ''' <summary>
    ''' Generates a class structure to store the data read from 'objects' hierarchies in the settings XML.
    ''' </summary>
    ''' <param name="p_pathNode">Path to the parent node.</param>
    ''' <param name="p_objectType">Object type to be read from the settings XML file.</param>
    ''' <remarks></remarks>
    Public Shared Sub ReadXmlSettingsObject(ByVal p_pathNode As String,
                                            ByVal p_objectType As eXMLSettingsObjectType)
        Dim myKeywordEntry As cKeyword
        Dim myClassification As cClassification
        Dim myKeywords As cKeywordsManager

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            'Get object items and properties
            For Each myValueGroup As XmlNode In myXMLNode.ChildNodes
                myClassification = New cClassification
                myKeywords = New cKeywordsManager

                Select Case p_objectType
                    Case eXMLSettingsObjectType.classification
                        myClassification.name = myValueGroup.Attributes.ItemOf("name").Value
                        myClassification.nameNode = myValueGroup.Name
                        myClassification.description = myValueGroup.Attributes.ItemOf("description").Value
                    Case eXMLSettingsObjectType.keyword
                        myKeywords.prefix = myValueGroup.Attributes.ItemOf("prefix").Value
                        myKeywords.name = myValueGroup.Name
                        myKeywords.description = myValueGroup.Attributes.ItemOf("description").Value
                End Select

                'Get entry items and properties
                For Each myValue As XmlNode In myValueGroup.ChildNodes
                    Dim value As String = myValue.InnerText
                    Dim tag As String = GetPrefix(value, ":")
                    If Not StringsMatch(value, tag) Then
                        tag &= ": "
                    Else
                        tag = ""
                    End If
                    If Not String.IsNullOrEmpty(tag) Then value = FilterStringFromName(value, tag, False, True)

                    myKeywordEntry = New cKeyword(value, tag, myValue.Attributes.ItemOf("description").Value)

                    'Add entry to object
                    Select Case p_objectType
                        Case eXMLSettingsObjectType.classification
                            myClassification.subClassification.Add(myKeywordEntry)
                        Case eXMLSettingsObjectType.keyword
                            myKeywords.keywords.Add(myKeywordEntry)
                    End Select

                Next

                'Add object entry to the list of objects
                Select Case p_objectType
                    Case eXMLSettingsObjectType.classification
                        testerSettings.exampleClassifications.Add(myClassification)
                    Case eXMLSettingsObjectType.keyword
                        testerSettings.exampleKeywords.Add(myKeywords)
                End Select
            Next
        Catch ex As Exception
            'TODO - Logger
            'RaiseEvent Log(New LoggerEventArgs(ex,
            '                                 NameOfParam(Function() p_pathNode), p_pathNode,
            '                                 NameOfParam(Function() p_objectType), p_objectType))
        End Try

    End Sub

#End Region


#Region "Creating Node Objects"

    '=== Model control XML
    ''' <summary>
    ''' Creates a new 'Incident' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_incidents">List of incident numbers to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectIncident(ByVal p_pathNode As String,
                                           ByVal p_incidents As List(Of Integer))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each incident As Integer In p_incidents
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "incident", myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "number", myNameSpace)
                objXMLNode.InnerText = CStr(incident)
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging  RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Ticket' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_tickets">List of ticket numbers to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectTicket(ByVal p_pathNode As String,
                                         ByVal p_tickets As List(Of Integer))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each ticket In p_tickets
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "ticket", myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "number", myNameSpace)
                objXMLNode.InnerText = CStr(ticket)
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Link' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_links">List of link values to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectLink(ByVal p_pathNode As String,
                                       ByVal p_links As List(Of cMCLink))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            'Create Header Node
            objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "link", myNameSpace)

            For Each link As cMCLink In p_links
                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
                objXMLNode.InnerText = link.title
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "url", myNameSpace)
                objXMLNode.InnerText = link.URL
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Image' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_images">List of 'image' objects to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectImage(ByVal p_pathNode As String,
                                        ByVal p_images As List(Of cFileAttachment))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each image As cFileAttachment In p_images
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "image", myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
                objXMLNode.InnerText = image.title
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "path", myNameSpace)
                objXMLNode.InnerText = image.PathAttachment.path
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Attachment' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_attachments">List of 'attachment' objects to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectAttachment(ByVal p_pathNode As String,
                                             ByVal p_attachments As List(Of cFileAttachment))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each attachment As cFileAttachment In p_attachments
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "attachment", myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
                objXMLNode.InnerText = attachment.title
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "path", myNameSpace)
                objXMLNode.InnerText = attachment.PathAttachment.path
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Update' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_updates">List of 'updates' objects to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Sub CreateObjectUpdate(ByVal p_pathNode As String,
                                         ByVal p_updates As List(Of cMCUpdate))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNodeSubHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            If _xmlReaderWriter.NodeExists(p_pathNode) Then
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)
            Else
                myXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "updates", myNameSpace)
            End If

            For Each update As cMCUpdate In p_updates
                'Create Sub Header Node for Date
                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "date", myNameSpace)

                'Create child nodes and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "day", myNameSpace)
                objXMLNode.InnerText = CStr(update.updateDate.numDay)
                objXMLNodeSubHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "month", myNameSpace)
                objXMLNode.InnerText = CStr(update.updateDate.numMonth)
                objXMLNodeSubHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "year", myNameSpace)
                objXMLNode.InnerText = CStr(update.updateDate.numYear)
                objXMLNodeSubHeader.AppendChild(objXMLNode)

                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "update", myNameSpace)

                'Append Sub Header Node
                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)

                'Create child nodes and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "person", myNameSpace)
                objXMLNode.InnerText = update.person
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "ticket", myNameSpace)
                If update.ticket > 0 Then
                    objXMLNode.InnerText = CStr(update.ticket)
                Else
                    objXMLNode.InnerText = cMCUpdate.NO_TICKET
                End If
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
                objXMLNode.InnerText = update.comment
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "build", myNameSpace)
                objXMLNode.InnerText = update.build
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "id", myNameSpace)
                objXMLNode.InnerText = CStr(update.id)
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Result' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_results">List of 'results' objects to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectResult(ByVal p_pathNode As String,
                                         ByVal p_results As List(Of cMCResult))
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNodeSubHeader As XmlNode
        Dim objXMLNodeSubSubHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim objXMLAttr As XmlAttribute
        Dim myNameSpace As String
        Dim tempString As String = ""

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each result As cMCResult In p_results
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

                '= Create child nodes and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "id", myNameSpace)
                objXMLNode.InnerText = CStr(result.id)
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
                objXMLNode.InnerText = result.name
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "units", myNameSpace)
                objXMLNode.InnerText = result.units
                objXMLNodeHeader.AppendChild(objXMLNode)
                objXMLAttr = xmlDoc.CreateAttribute("units_conversion")
                objXMLAttr.Value = ConvertYesTrueString(result.unitsConversion, eCapitalization.alllower)
                objXMLNode.Attributes.Append(objXMLAttr)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "table_name", myNameSpace)
                objXMLNode.InnerText = result.tableName
                objXMLNodeHeader.AppendChild(objXMLNode)

                '= Create Sub Header Node for 'updates' if any exist
                If result.updates.Count > 0 Then
                    objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "updates", myNameSpace)
                    For Each resultUpdate As cMCResultUpdate In result.updates
                        '== Create Sub Sub Header for 'update'
                        objXMLNodeSubSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "update", myNameSpace)

                        '=== Create child nodes and append to header node
                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "id", myNameSpace)
                        objXMLNode.InnerText = CStr(resultUpdate.id)
                        objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
                        objXMLNode.InnerText = resultUpdate.comment
                        objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                        '= Append Sub Sub Header Nodes to 'updates'
                        objXMLNodeSubHeader.AppendChild(objXMLNodeSubSubHeader)
                    Next

                    '= Append Sub Sub Header Nodes 'updates'
                    objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)
                End If

                '= Create Sub Header Node for 'lookup_fields'
                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "lookup_fields", myNameSpace)

                For Each field As cFieldLookup In result.query
                    '== Create Sub Sub Header for 'field'
                    objXMLNodeSubSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "field", myNameSpace)

                    '=== Create child nodes and append to header node
                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
                    objXMLNode.InnerText = field.name
                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)
                    objXMLNode.InnerText = field.valueField
                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                    '= Append Sub Sub Header Nodes to 'lookup_fields'
                    objXMLNodeSubHeader.AppendChild(objXMLNodeSubSubHeader)
                Next

                '= Append Sub Sub Header Nodes 'lookup_fields'
                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)


                '= Create Sub Header Node for 'output_field'
                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "output_field", myNameSpace)

                '== Create child nodes and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
                objXMLNode.InnerText = result.benchmark.name
                objXMLNodeSubHeader.AppendChild(objXMLNode)

                '=== Create Sub Sub Header Node for 'value'
                objXMLNodeSubSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)
                objXMLAttr = xmlDoc.CreateAttribute("passing_percent_difference_range")
                objXMLAttr.Value = CStr(result.benchmark.valuePassingPercentDifferenceRange)
                objXMLNodeSubSubHeader.Attributes.Append(objXMLAttr)


                '===== Create child nodes and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "benchmark", myNameSpace)
                objXMLNode.InnerText = result.benchmark.valueBenchmark
                objXMLAttr = xmlDoc.CreateAttribute("is_correct")
                objXMLAttr.Value = GetEnumDescription(result.benchmark.isCorrect)
                objXMLNode.Attributes.Append(objXMLAttr)
                If Not String.IsNullOrEmpty(result.benchmark.roundBenchmark) Then
                    objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
                    objXMLAttr.Value = CStr(result.benchmark.roundBenchmark)
                    objXMLNode.Attributes.Append(objXMLAttr)
                End If
                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                If Not result.benchmark.zeroTolerance = 0 Then
                    objXMLAttr = xmlDoc.CreateAttribute("zero_tolerance")
                    objXMLAttr.Value = CStr(result.benchmark.zeroTolerance)
                    objXMLNode.Attributes.Append(objXMLAttr)
                End If
                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "theoretical", myNameSpace)
                objXMLNode.InnerText = result.benchmark.valueTheoretical
                If Not String.IsNullOrEmpty(result.benchmark.roundTheoretical) Then
                    objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
                    objXMLAttr.Value = CStr(result.benchmark.roundTheoretical)
                    objXMLNode.Attributes.Append(objXMLAttr)
                End If
                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

                If Not String.IsNullOrEmpty(result.benchmark.valueLastBest) Then
                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "last_best", myNameSpace)
                    objXMLNode.InnerText = result.benchmark.valueLastBest
                    If Not String.IsNullOrEmpty(result.benchmark.roundLastBest) Then
                        objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
                        objXMLAttr.Value = CStr(result.benchmark.roundLastBest)
                        objXMLNode.Attributes.Append(objXMLAttr)
                    End If
                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)
                End If

                If Not result.benchmark.shiftCalc = 0 Then
                    objXMLAttr = xmlDoc.CreateAttribute("shift_for_calculating_percent_difference")
                    objXMLAttr.Value = CStr(result.benchmark.shiftCalc)
                    objXMLNodeSubSubHeader.Attributes.Append(objXMLAttr)
                End If

                '= Append Sub Sub Header Nodes to 'output_field'
                objXMLNodeSubHeader.AppendChild(objXMLNodeSubSubHeader)


                '= Append Sub Header Node 'output_field'
                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)


                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            'TODO - error logging   RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Postprocessed_Result' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the node to which object values are to be added.</param>
    ''' <param name="p_resultsPostProcessed">List of results objects to use to fill the relevant parts of the post-processed XML file.</param>
    ''' <param name="p_results">List of 'postprocessed_results' objects to add to the object to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateObjectResultPostProcessed(ByVal p_pathNode As String,
                                                      ByVal p_resultsPostProcessed As List(Of cMCResultPostProcessed),
                                                      ByVal p_results As List(Of cMCResultPostProcessed))
        'TODO: This function currently only uses 'p_results'. Incorporate it and 'p_resultsPostProcessed' correctly! It shoudl mostly be complete.

        'Dim objXMLNodeHeader As XmlNode
        'Dim objXMLNodeSub1Header As XmlNode
        'Dim objXMLNodeSub2Header As XmlNode
        'Dim objXMLNodeSub3Header As XmlNode
        'Dim objXMLNodeSub4Header As XmlNode
        'Dim objXMLNodeSub5Header As XmlNode
        'Dim objXMLNode As XmlNode
        'Dim objXMLAttr As XmlAttribute
        'Dim myNameSpace As String
        'Dim tempString As String = ""

        'Dim rangeType As String

        'Try
        '    'Create an XmlNamespaceManager for resolving namespaces.
        '    Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
        '    nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

        '    'Insert new nodes in XML w text
        '    myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

        '    'Node to append object to
        '    myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

        '    For Each result As cMCResult In p_results
        '        'Create Header Node
        '        objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

        '        '= Create child nodes and append to header node
        '        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
        '        objXMLNode.InnerText = result.name
        '        objXMLNodeHeader.AppendChild(objXMLNode)

        '        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "table_name", myNameSpace)
        '        objXMLNode.InnerText = result.tableName
        '        objXMLNodeHeader.AppendChild(objXMLNode)


        '        If  result.range IsNot Nothing  Then
        '            'Set range type
        '            If result.range.isRangeAll Then
        '                rangeType = rangeTypeAll
        '            Else
        '                rangeType = rangeTypeRange
        '            End If

        '            '= Create Sub Header Node for 'range'
        '            objXMLNodeSub1Header = xmlDoc.CreateNode(XmlNodeType.Element, rangeType, myNameSpace)

        '            '== Create child nodes and append to header node
        '            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "range_operation", myNameSpace)
        '            objXMLNode.InnerText = GetEnumDescription(result.range.rangeOperation)
        '            objXMLNodeSub1Header.AppendChild(objXMLNode)

        '            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "field_name", myNameSpace)
        '            objXMLNode.InnerText = result.range.fieldName
        '            objXMLNodeSub1Header.AppendChild(objXMLNode)

        '            If rangeType = rangeTypeRange Then
        '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_first", myNameSpace)
        '                objXMLNode.InnerText = result.range.valueFirst
        '                objXMLNodeSub1Header.AppendChild(objXMLNode)

        '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_last", myNameSpace)
        '                objXMLNode.InnerText = result.range.valueLast
        '                objXMLNodeSub1Header.AppendChild(objXMLNode)
        '            End If

        '            '= Append Sub Header Nodes 'range'
        '            objXMLNodeHeader.AppendChild(objXMLNodeSub1Header)
        '        End If

        '        '= Create Sub Header Node for 'formula'
        '        objXMLNodeSub1Header = xmlDoc.CreateNode(XmlNodeType.Element, "formula", myNameSpace)

        '        '== Create child nodes and append to header node
        '        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "operation", myNameSpace)
        '        objXMLNode.InnerText = GetEnumDescription(result.formula.operation)
        '        objXMLNodeSub1Header.AppendChild(objXMLNode)


        '        '== Create Sub Sub Header Node for 'variables'
        '        objXMLNodeSub2Header = xmlDoc.CreateNode(XmlNodeType.Element, "variables", myNameSpace)

        '        For Each variable In result.formula.variables
        '            '=== Create Sub Sub Sub Header Node for 'variable'
        '            objXMLNodeSub3Header = xmlDoc.CreateNode(XmlNodeType.Element, "variable", myNameSpace)

        '            '==== Create child nodes and append to header node
        '            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "scale_factor", myNameSpace)
        '            objXMLNode.InnerText = CStr(variable.scaleFactor)
        '            objXMLNodeSub3Header.AppendChild(objXMLNode)

        '            '==== Create Sub Sub Sub Sub header for 'lookup_fields'
        '            objXMLNodeSub4Header = xmlDoc.CreateNode(XmlNodeType.Element, "lookup_fields", myNameSpace)

        '            For Each field As cFieldLookup In result.fieldsLookup
        '                If  result.range IsNot Nothing  Then
        '                    'Set range type
        '                    If result.range.isRangeAll Then
        '                        rangeType = rangeTypeAll
        '                    Else
        '                        rangeType = rangeTypeRange
        '                    End If

        '                    '= Create Sub Header Node for 'range'
        '                    objXMLNodeSub5Header = xmlDoc.CreateNode(XmlNodeType.Element, rangeType, myNameSpace)

        '                    '== Create child nodes and append to header node
        '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "range_operation", myNameSpace)
        '                    objXMLNode.InnerText = GetEnumDescription(variable.range.rangeOperation)
        '                    objXMLNodeSub5Header.AppendChild(objXMLNode)

        '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "field_name", myNameSpace)
        '                    objXMLNode.InnerText = variable.range.fieldName
        '                    objXMLNodeSub5Header.AppendChild(objXMLNode)

        '                    If rangeType = rangeTypeRange Then
        '                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_first", myNameSpace)
        '                        objXMLNode.InnerText = variable.range.valueFirst
        '                        objXMLNodeSub5Header.AppendChild(objXMLNode)

        '                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_last", myNameSpace)
        '                        objXMLNode.InnerText = variable.range.valueLast
        '                        objXMLNodeSub5Header.AppendChild(objXMLNode)
        '                    End If

        '                    '= Append Sub Header Nodes 'range'
        '                    objXMLNodeSub4Header.AppendChild(objXMLNodeSub5Header)
        '                End If

        '                For Each myField As cFieldLookup In variable.fieldsLookup
        '                    '== Create Sub Sub Header for 'field'
        '                    objXMLNodeSub5Header = xmlDoc.CreateNode(XmlNodeType.Element, "field", myNameSpace)

        '                    '=== Create child nodes and append to header node
        '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
        '                    objXMLNode.InnerText = myField.name
        '                    objXMLNodeSub5Header.AppendChild(objXMLNode)

        '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)
        '                    objXMLNode.InnerText = myField.valueField
        '                    objXMLNodeSub5Header.AppendChild(objXMLNode)

        '                    '= Append Sub Header Nodes 'lookup_fields'
        '                    objXMLNodeSub4Header.AppendChild(objXMLNodeSub5Header)
        '                Next
        '            Next

        '            '= Append Sub Sub Sub Sub Header Nodes 'lookup_fields' to 'variable'
        '            objXMLNodeSub3Header.AppendChild(objXMLNodeSub4Header)

        '            '==== Create Sub Sub Sub Sub Header Node for 'output_field'
        '            objXMLNodeSub4Header = xmlDoc.CreateNode(XmlNodeType.Element, "output_field", myNameSpace)

        '            '== Create child nodes and append to header node
        '            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
        '            objXMLNode.InnerText = variable.fieldOutput.name
        '            objXMLNodeSub4Header.AppendChild(objXMLNode)


        '            '= Append Sub Sub Sub Sub Header Nodes 'output_field' to 'variable'
        '            objXMLNodeSub3Header.AppendChild(objXMLNodeSub4Header)

        '            '= Append Sub Sub Sub Header Node for 'variable' to 'variables'
        '            objXMLNodeSub2Header.AppendChild(objXMLNodeSub3Header)
        '        Next

        '        '= Append Sub Sub Header Node for 'variables' to 'formula'
        '        objXMLNodeSub1Header.AppendChild(objXMLNodeSub2Header)



        '        '=== Create Sub Sub Header Node for 'result'
        '        objXMLNodeSub2Header = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

        '        '===== Create child nodes and append to header node
        '        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "benchmark", myNameSpace)
        '        objXMLNode.InnerText = result.formula.result.valueBenchmark
        '        objXMLAttr = xmlDoc.CreateAttribute("is_correct")
        '        objXMLAttr.Value = GetEnumDescription(result.formula.result.isCorrect)
        '        objXMLNode.Attributes.Append(objXMLAttr)
        '        If Not String.IsNullOrEmpty(result.formula.result.roundBenchmark) Then
        '            objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
        '            objXMLAttr.Value = CStr(result.formula.result.roundBenchmark)
        '            objXMLNode.Attributes.Append(objXMLAttr)
        '        End If
        '        objXMLNodeSub2Header.AppendChild(objXMLNode)

        '        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "theoretical", myNameSpace)
        '        objXMLNode.InnerText = result.formula.result.valueTheoretical
        '        objXMLNodeSub2Header.AppendChild(objXMLNode)

        '        If Not String.IsNullOrEmpty(result.formula.result.valueLastBest) Then
        '            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "last_best", myNameSpace)
        '            objXMLNode.InnerText = result.formula.result.valueLastBest
        '            objXMLNodeSub2Header.AppendChild(objXMLNode)
        '        End If

        '        If Not result.formula.result.shiftCalc = Nothing Or Not result.formula.result.shiftCalc = 0 Then
        '            objXMLAttr = xmlDoc.CreateAttribute("shift_for_calculating_percent_difference")
        '            objXMLAttr.Value = CStr(result.formula.result.shiftCalc)
        '            objXMLNodeSub2Header.Attributes.Append(objXMLAttr)
        '        End If

        '        '= Append Sub Sub Header Nodes to 'output_field'
        '        objXMLNodeSub1Header.AppendChild(objXMLNodeSub2Header)


        '        '= Append Sub Header Node 'output_field'
        '        objXMLNodeHeader.AppendChild(objXMLNodeSub1Header)


        '        'Append header node to XML file
        '        myXMLNode.AppendChild(objXMLNodeHeader)
        '    Next
        'Catch ex As Exception
        '    RaiseEvent Log(New LoggerEventArgs(ex))
        'End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'program' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the parent node to which a list of object values are to be added.</param>
    ''' <param name="p_programs">List of 'program' names to add to the object to be created.</param>
    ''' <param name="p_headerTitle">The name of the object value containing node.</param>
    ''' <remarks></remarks>
    Friend Sub CreateObjectTargetProgram(ByVal p_pathNode As String,
                                          ByVal p_programs As List(Of String),
                                          ByVal p_headerTitle As String)
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each program As String In p_programs
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, p_headerTitle, myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
                objXMLNode.InnerText = program
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates a new 'Attachment' object entry in an XML file.
    ''' </summary>
    ''' <param name="p_pathNode">Absolute path to the parent node to which a list of object values are to be added.</param>
    ''' <param name="p_attachments">List of 'attachment' objects to add to the object to be created.</param>
    ''' <param name="p_headerTitle">The name of the object value containing node.</param>
    ''' <remarks></remarks>
    Friend Sub CreateObjectFileAttachment(ByVal p_pathNode As String,
                                          ByVal p_attachments As List(Of cFileAttachment),
                                          ByVal p_headerTitle As String)
        Dim objXMLNodeHeader As XmlNode
        Dim objXMLNode As XmlNode
        Dim myNameSpace As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

            'Insert new nodes in XML w text
            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

            'Node to append object to
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            For Each attachment As cFileAttachment In p_attachments
                'Create Header Node
                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, p_headerTitle, myNameSpace)

                'Create child node and append to header node
                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
                objXMLNode.InnerText = attachment.title
                objXMLNodeHeader.AppendChild(objXMLNode)

                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "path", myNameSpace)
                objXMLNode.InnerText = attachment.PathAttachment.pathRelative
                objXMLNodeHeader.AppendChild(objXMLNode)

                'Append header node to XML file
                myXMLNode.AppendChild(objXMLNodeHeader)
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region


#Region "Reading/Writing Specific Cases"
    '=== Status Form Syncing
    ''' <summary>
    ''' Gets a list of unique model ids from the regtest log. 
    ''' If the list ends in 0, then regTest is no longer running additional models.
    ''' </summary>
    ''' <param name="p_resultsPath">If provided, the specified log file will be read. 
    ''' If not provided, it is assumed that the desired XMl file is already open.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateUniqueModelIDList(Optional ByVal p_resultsPath As String = "") As List(Of String)
        Dim newModelIDs As New List(Of String)

        'Open XML File
        If Not String.IsNullOrEmpty(p_resultsPath) Then
            If _xmlReaderWriter.InitializeXML(p_resultsPath) Then
                newModelIDs = UpdateModelIDListComponent()
                _xmlReaderWriter.CloseXML()
            End If
        Else
            'tempList = UpdateUniqueModelIDListComponent()
            newModelIDs = UpdateModelIDListComponent()
        End If

        Return newModelIDs
    End Function

    ''' <summary>
    ''' Gets a list of model ids from the regtest log.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateModelIDListComponent() As List(Of String)
        Dim pathNode As String = "//n:items"
        Dim tempList As New List(Of String)
        Dim tempEntry As String

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

            For j As Integer = 0 To myXMLNode.ChildNodes.Count - 1
                'Select object root node
                myXMLObject = myXMLNode.ChildNodes(j)

                'Get model id for the item
                tempEntry = myXMLObject.SelectSingleNode("model_id").InnerText

                tempList.Add(tempEntry)
            Next
        Catch ex As Exception
            'TODO - Logger
            'RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return tempList
    End Function


    'TODO: Not used. Not working correctly either
    ''' <summary>
    ''' Gets a list of unique model ids from the regtest log. 
    ''' If the list ends in 0, then regTest is no longer running additional models.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateUniqueModelIDListComponent() As List(Of String)
        Dim j As Integer
        Dim pathNode As String = "//n:items"
        Dim tempList As New List(Of String)
        Dim tempEntry As String
        Dim entryUnique As Boolean

        Try
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

            For j = 0 To myXMLNode.ChildNodes.Count - 1
                myXMLObject = myXMLNode.ChildNodes(j)                               'Select object root node
                tempEntry = myXMLObject.SelectSingleNode("model_id").InnerText      'Get model id for the item

                If j = 0 Then
                    tempList.Add(tempEntry)                         'Adds the first "0" to the list
                Else
                    'Sets up unique status of the value and trigger to check remaining values
                    If tempList.Count <= 2 Then
                        If tempEntry = "0" Then
                            entryUnique = False 'Ensures that the second entry is not "0"
                        Else
                            entryUnique = True
                        End If
                    ElseIf tempList.Count > 2 Then
                        If Not tempList(tempList.Count - 2) = "0" Then entryUnique = True 'Ensures that from the third entry on, the next "0" is only added after non-zero values are added. No more are added after this as they will not be unique in the range checked.
                    End If

                    'Check that id is unique part from the first entry and if so, add it
                    If entryUnique Then
                        For i = 1 To tempList.Count - 1
                            If tempList(i) = tempEntry Then
                                entryUnique = False
                                Exit For
                            End If
                        Next
                    End If
                    If entryUnique Then
                        tempList.Add(tempEntry)
                    End If
                End If
            Next
        Catch ex As Exception
            'TODO - Logger
            'RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return tempList
    End Function

    '=== Model Control XML Validation


    ''' <summary>
    ''' Handles creation and updating of the zero_tolerance element in the MC XML file.
    ''' </summary>
    ''' <param name="p_nodeIndex">Index of the result item to perform the operation on.</param>
    ''' <param name="p_resultType">Whether the result is a post-processed or regular result.</param>
    ''' <param name="p_attributeValue">Value to write to the attribute.</param>
    ''' <param name="p_overWriteExisting">If the attribute exists, if True then the new value will be written. 
    ''' If False, the original value will be maintained.</param>
    ''' <remarks></remarks>
    Public Sub HandleZeroTolerance(ByVal p_nodeIndex As Integer,
                                          ByVal p_resultType As eResultType,
                                          Optional ByVal p_attributeValue As String = "1",
                                          Optional ByVal p_overWriteExisting As Boolean = True)
        Dim pathnode As String = "//n:result[" & p_nodeIndex + 1 & "]"
        Dim attribute As String = "zero_tolerance"

        If p_resultType = eResultType.regular Then
            pathnode = pathnode & "/n:output_field/n:value"
        ElseIf p_resultType = eResultType.postProcessed Then
            pathnode = pathnode & "/n:formula/n:result"
        End If

        If _xmlReaderWriter.NodeExists(pathnode) Then
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
            myXMLNode = xmlRoot.SelectSingleNode(pathnode, nsmgr)

            If Not _xmlReaderWriter.AttributeExists(myXMLNode, attribute) Then
                _xmlReaderWriter.CreateNodeByPath(pathnode, attribute, p_attributeValue,
                                                 eXMLElementType.Attribute,
                                                 eNodeCreate.child)
            Else
                If p_overWriteExisting Then _xmlReaderWriter.WriteNodeText(p_attributeValue, pathnode, attribute)
            End If
        End If

    End Sub

    '=== Syncing Folder/File Operations
    ''' <summary>
    ''' Creates a list of the new paths expected for files associated with the object node as the folder structure is flattened or changed to a DB hierarchy. 
    ''' Also updates the XML files to keep the references in sync.
    ''' </summary>
    ''' <param name="p_path">Path to the XML file to be read.</param>
    ''' <param name="p_pathNode">Absolute path to the parent node from which object values are read.</param>
    ''' <param name="p_queryNodeName">Name of the node to be queried.</param>
    ''' <param name="p_queryNodeValue">Value that the query node is expected to contain for a match. 
    ''' This can be a partial match.</param>
    ''' <param name="p_correspondingNodeName">Name of the node that corresponds to a matching query node.</param>
    ''' <param name="p_action">Whether the class creation and node value change operation reflects a flattening or DB creation folder operation.</param>
    ''' <param name="p_correspondingNodeValueStub">Prefix to the value that the corresponding node should contain.</param>
    ''' <returns>A list of the new paths expected for files associated with the object node.</returns>
    ''' <remarks></remarks>
    Public Function GetXMLNodeObjectValuePairs(ByVal p_path As String,
                                                      ByVal p_pathNode As String,
                                                         ByVal p_queryNodeName As String,
                                                         ByVal p_queryNodeValue As String,
                                                         ByVal p_correspondingNodeName As String,
                                                         ByRef p_action As eXMLEditorAction,
                                                         Optional ByVal p_correspondingNodeValueStub As String = "") As List(Of String)
        Dim readNodeText As String
        Dim triggerTextFound As Boolean
        Dim tempCollection As New List(Of String)

        If _xmlReaderWriter.InitializeXML(p_path) Then
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                For Each myAttachmentNode As XmlNode In myXMLNode.ChildNodes
                    triggerTextFound = False
                    'Check each object parent/root node
                    For Each myChildNode As XmlNode In myAttachmentNode.ChildNodes
                        'Find query node within a given node object parent
                        If myChildNode.Name = p_queryNodeName Then
                            readNodeText = myChildNode.InnerText

                            'If query node contains the trigger text, gather the corresponding node value
                            If StringExistInName(readNodeText, p_queryNodeValue) Then
                                triggerTextFound = True
                                Exit For
                            End If
                        End If
                    Next
                    If triggerTextFound Then
                        For Each myChildNode As XmlNode In myAttachmentNode.ChildNodes
                            'Find corresponding node within a given node object parent
                            If myChildNode.Name = p_correspondingNodeName Then
                                'Add node value to list
                                tempCollection.Add(myChildNode.InnerText)

                                'Adjust node value
                                Select Case p_action
                                    Case eXMLEditorAction.DirectoriesFlatten
                                        'Strip away relative path additions, as files will be placed at the same level as the model file & controlling XML
                                        myChildNode.InnerText = GetSuffix(myChildNode.InnerText, "\")
                                        Exit For
                                    Case eXMLEditorAction.DirectoriesDBGather
                                        'Reapppend relative path additions, if any
                                        If Not String.IsNullOrEmpty(p_correspondingNodeValueStub) Then p_correspondingNodeValueStub = TrimPathSlash(p_correspondingNodeValueStub) & "\"
                                        myChildNode.InnerText = p_correspondingNodeValueStub & myChildNode.InnerText
                                        Exit For
                                End Select
                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
                readNodeText = ""
            Finally
                _xmlReaderWriter.SaveXML(p_path)
                _xmlReaderWriter.CloseXML()
            End Try
        End If

        GetXMLNodeObjectValuePairs = tempCollection
    End Function

#End Region
End Class
