Option Explicit On
Option Strict On

Imports System.Xml
Imports System.Collections.ObjectModel
Imports CSiTester.cEnumerations
Imports CSiTester.cLibLists

''' <summary>
''' Module that contains routines for reading/writing to XML files, including creating and removing elements
''' </summary>
''' <remarks></remarks>
Module mFilesXML
    '#Region "Module Variables"
    '    Dim xmlDoc As XmlDocument
    '    Dim xmlRoot As XmlElement
    '    Dim myXMLNode As XmlNode
    '    Dim myXMLObject As XmlNode
    '    Dim myXMLAttribute As XmlAttribute

    '    Dim myXMLFileNode As cXMLNode
    '    Dim xmlFile As cXMLObject
    '#End Region

    '#Region "XML Read/Write Master Functions"

    '    ''' <summary>
    '    ''' Opens a new XML object for reading and writing
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML to be accessed</param>
    '    ''' <param name="suppressWarning">Optional overwrite that prevents catch prompts from appearing.</param>
    '    ''' <remarks></remarks>
    '    Function InitializeXML(ByVal myPath As String, Optional ByVal suppressWarning As Boolean = False) As Boolean
    '        Dim xmlPathName As String

    '        'Initialize XML
    '        xmlPathName = myPath

    '        xmlDoc = New XmlDocument
    '        Try
    '            If FileExists(xmlPathName) Then
    '                xmlDoc.Load(xmlPathName)
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            If Not suppressWarning Then
    '                csiLogger.ExceptionAction(ex)
    '            End If

    '            Return False
    '        End Try

    '        xmlRoot = xmlDoc.DocumentElement

    '        Return True
    '    End Function

    '    ''' <summary>
    '    ''' Saves XML file according to a supplied file path.
    '    ''' </summary>
    '    ''' <param name="myPath"></param>
    '    ''' <remarks></remarks>
    '    Sub SaveXML(ByVal myPath As String, Optional ByVal NoOverWrite As Boolean = False)
    '        Try
    '            If FileExists(myPath) Then
    '                If Not NoOverWrite Then SingleFileNotReadOnly(myPath) 'Sets the specified file to being readable if it is 'read only'.

    '                xmlDoc.Save(myPath) 'Saves file if XML is modified
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Empties XML objects.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Sub CloseXML()
    '        Try
    '            xmlDoc = Nothing
    '            xmlRoot = Nothing
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Clears all nodes within a specified node name.
    '    ''' </summary>
    '    ''' <param name="pathNode">Path to the highest level node to remain.</param>
    '    ''' <param name="nameListNode">Name of the child nodes to remove.</param>
    '    ''' <remarks></remarks>
    '    Sub ClearObjects(ByVal pathNode As String, ByVal nameListNode As String)
    '        Dim myXmlNodeList As XmlNodeList

    '        Try
    '            If NodeExists(pathNode) Then
    '                'Create an XmlNamespaceManager for resolving namespaces.
    '                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '                myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '                'Write new values to XML
    '                pathNode = pathNode & "/n:" & nameListNode

    '                'Delete existing nodes in XML
    '                myXmlNodeList = xmlDoc.SelectNodes(pathNode, nsmgr)
    '                For Each myXMLItem As XmlNode In myXmlNodeList
    '                    myXMLItem.ParentNode.RemoveChild(myXMLItem)
    '                Next
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '#End Region

    '#Region "Get XML/Node Information"
    '    ''' <summary>
    '    ''' Returns the count of child nodes of a specified node path. No filtering of node types is performed.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML node to be checked.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ChildCount(ByVal myPath As String) As Integer
    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(myPath, nsmgr)

    '            Return myXMLNode.ChildNodes.Count
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            Return 0
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Counts the number of 1st-level child nodes of a given node, not including comment nodes.
    '    ''' Usually used to determine if there are child nodes to be searched in recursive functions.
    '    ''' </summary>
    '    ''' <param name="pathNode">Path to the desired XML node.</param>
    '    ''' <remarks></remarks>
    '    Function CountChildNodes(ByVal pathNode As String) As Integer
    '        Dim i As Integer
    '        Dim childNodesCount As Integer = 0

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Lookup node or attribute within XML file
    '            For i = 0 To myXMLNode.ChildNodes.Count - 1
    '                If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then childNodesCount = childNodesCount + 1
    '            Next i

    '            Return childNodesCount
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            Return 0
    '        End Try

    '        'TODO: experiment with below instead of above in the 'Try' statement
    '        ''Counts children based on 3 cases
    '        'If Not myXMLNode.ChildNodes.Count > 0 Then                  'Case 1: Node has no child
    '        '    childNodesCount = 0
    '        'ElseIf Not myXMLNode.FirstChild.Name = "#text" Then         'Case 2: Node has child, but child has no text (e.g. folder/header node)
    '        '    'Screen out comment nodes from count
    '        '    For i = 0 To myXMLNode.ChildNodes.Count - 1             'Case 2a: Node has children, but the first child is a comment node
    '        '        If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then childNodesCount = childNodesCount + 1
    '        '    Next i
    '        'Else                                                        'Case 3: Node has child, children have text. Count skips a node layer as each text item is yet another node
    '        '    childNodesCount = myXMLNode.ChildNodes(0).ChildNodes.Count
    '        'End If

    '    End Function

    '    ''' <summary>
    '    ''' Counts number of children that a node has, considering no child, a child with no text, and a child with text.
    '    ''' </summary>
    '    ''' <param name="myNode">XML node object being queried.</param>
    '    ''' <remarks></remarks>
    '    Function XMLChildCount(ByVal myNode As XmlNode) As Integer
    '        'Counts children based on 3 cases
    '        If Not myNode.ChildNodes.Count > 0 Then                'Case 1: Node has no child
    '            XMLChildCount = 0
    '        ElseIf Not myNode.FirstChild.Name = "#text" Then       'Case 2: Node has child, but child has no text (e.g. folder/header node)
    '            XMLChildCount = myNode.ChildNodes.Count
    '        Else                                                        'Case 3: Node has child, children have text. Count skips a node layer as each text item is yet another node
    '            XMLChildCount = myNode.ChildNodes(0).ChildNodes.Count
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Returns 'true' if the node exists in the currently active xml file. Otherwise, returns false.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the node to write the new value to.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function NodeExists(ByVal pathNode As String) As Boolean
    '        'Create an XmlNamespaceManager for resolving namespaces.
    '        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '        If IsNothing(xmlRoot.SelectSingleNode(pathNode, nsmgr)) Then
    '            Return False
    '        Else
    '            Return True
    '        End If

    '    End Function

    '    ''' <summary>
    '    ''' Returns 'true' if the attribute exists in the provided node object. Otherwise, returns false.
    '    ''' </summary>
    '    ''' <param name="node">Node object to check.</param>
    '    ''' <param name="pathNodeAttrib">Name of the attribute to look for.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function AttributeExists(ByVal node As XmlNode, ByVal pathNodeAttrib As String) As Boolean
    '        If node.NodeType = XmlNodeType.Comment Then Return False
    '        If IsNothing(node.Attributes.GetNamedItem(pathNodeAttrib)) Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    End Function

    '#End Region

    '#Region "Reading/Writing Nodes"
    '    '=== Component Function
    '    ''' <summary>
    '    ''' Handles single nodes. Reads node value assignment or attribute value assignment, based on specified node and optional attribute
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node</param>
    '    ''' <param name="pathNodeAttrib">Attribute of the desired node</param>
    '    ''' <returns>Node value or node attribute value</returns>
    '    ''' <remarks></remarks>
    '    Function ReadNodeText(ByVal pathNode As String, Optional ByVal pathNodeAttrib As String = "") As String
    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            If NodeExists(pathNode) Then
    '                myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '                If pathNodeAttrib = "" Then
    '                    Return myXMLNode.InnerText
    '                Else
    '                    If AttributeExists(myXMLNode, pathNodeAttrib) Then Return myXMLNode.Attributes.GetNamedItem(pathNodeAttrib).InnerText
    '                End If
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        ReadNodeText = ""
    '    End Function

    '    ''' <summary>
    '    ''' Handles single nodes. Writes node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the regTest class object.
    '    ''' </summary>
    '    ''' <param name="propValue">Property value, usually from the regTest class creat for virtual memory storage.</param>
    '    ''' <param name="pathNode">XML path to the node to write the new value to.</param>
    '    ''' <param name="pathNodeAttrib">Attribute to write the new attribute value to.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteNodeText(ByVal propValue As String, ByVal pathNode As String, Optional ByVal pathNodeAttrib As String = "")
    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            If Not IsNothing(myXMLNode) Then
    '                If pathNodeAttrib = "" Then
    '                    'The loop below is necessary if a node containing text also has child nodes. 
    '                    'Otherwise, the child nodes may have the same value applied to them as well is using the 'InnerText' property
    '                    For Each myChildNode As XmlNode In myXMLNode.ChildNodes
    '                        If myChildNode.Name = "#text" Then
    '                            myChildNode.InnerText = propValue
    '                            Exit Sub
    '                        End If
    '                    Next

    '                    myXMLNode.InnerText = propValue             'Contingency if node currently does not contain text
    '                Else
    '                    If AttributeExists(myXMLNode, pathNodeAttrib) Then myXMLNode.Attributes.GetNamedItem(pathNodeAttrib).InnerText = propValue
    '                End If
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Returns the string content of the specified child and subchild zero-based index numbers.
    '    ''' </summary>
    '    ''' <param name="pathNode">Path to the node to be read.</param>
    '    ''' <param name="childNum">Index number of the first child node.</param>
    '    ''' <param name="childChildNum">Index number of the second child node.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadChildText(ByVal pathNode As String, ByVal childNum As Integer, ByVal childChildNum As Integer) As String
    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            If Not IsNothing(myXMLNode.ChildNodes.Item(childNum).ChildNodes.Item(childChildNum)) Then
    '                Return myXMLNode.ChildNodes.Item(childNum).ChildNodes.Item(childChildNum).InnerText
    '            Else
    '                Return ""
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            Return ""
    '        End Try
    '    End Function

    '    '=== Direct call to an XML File
    '    ''' <summary>
    '    ''' Gets a single node value from a single XML file from a single query.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML file.</param>
    '    ''' <param name="myPathNode">Path to the node within the XML file.</param>
    '    ''' <param name="myNodeValue">Text value of the node queried.</param>
    '    ''' <param name="myPathNodeAttrib">Name of the attribute to the node specified. Will return the attribute value.</param>
    '    ''' <param name="cleanPath">If true, the path will be trimmed of white space, leading and ending "\", and all "\\".</param>
    '    ''' <remarks></remarks>
    '    Sub GetSingleXMLNodeValue(ByVal myPath As String, ByVal myPathNode As String, ByRef myNodeValue As String, Optional ByVal myPathNodeAttrib As String = "", Optional ByVal cleanPath As Boolean = False)
    '        If InitializeXML(myPath) Then
    '            myNodeValue = ReadNodeText(myPathNode, myPathNodeAttrib)
    '            CloseXML()

    '            If cleanPath Then
    '                TrimWhiteSpace(myNodeValue)
    '                myNodeValue = trimPathSlash(myNodeValue, True)
    '                CleanDoubleSlash(myNodeValue)
    '            End If
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Returns a list of node velus from a single XML file from a single query of a single node with multiple child nodes.
    '    ''' </summary>
    '    ''' <param name="pPath">Path to the XML file.</param>
    '    ''' <param name="pPathNode">Path to the node within the XML file.</param>
    '    ''' <param name="pPathNodeAttrib">Name of the attribute to the node specified. Will return the attribute value</param>
    '    ''' <param name="cleanPath">If true, the path will be trimmed of white space, leading and ending "\", and all "\\".</param>
    '    ''' <remarks></remarks>
    '    Function GetSingleXMLNodeListValues(ByVal pPath As String, ByVal pPathNode As String, Optional ByVal pPathNodeAttrib As String = "", Optional ByVal cleanPath As Boolean = False) As ObservableCollection(Of String)
    '        Dim nodeValues As New ObservableCollection(Of String)

    '        If InitializeXML(pPath) Then
    '            ReadNodeListText(pPathNode, nodeValues, pPathNodeAttrib)
    '            CloseXML()

    '            If cleanPath Then
    '                For Each nodeValue As String In nodeValues
    '                    TrimWhiteSpace(nodeValue)
    '                    nodeValue = trimPathSlash(nodeValue, True)
    '                    CleanDoubleSlash(nodeValue)
    '                Next
    '            End If
    '        End If

    '        Return nodeValues
    '    End Function

    '#End Region

    '#Region "Reading/Writing Node Lists"
    '    ''' <summary>
    '    ''' Handles multiple child nodes of the same name. Reads node value assignment based on specified node.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node.</param>
    '    ''' <param name="myList">List array to be populated by multiple node child elements.</param>
    '    ''' <param name="pathNodeAttrib">If provided, the function will perform the operation on the specified attribute of the node.</param>
    '    ''' <returns>Node value or node attribute value.</returns>
    '    ''' <remarks></remarks>
    '    Function ReadNodeListText(ByVal pathNode As String, ByRef myList As ObservableCollection(Of String), Optional ByVal pathNodeAttrib As String = "") As Boolean
    '        Dim i As Integer
    '        myList.Clear()  'Ensures that list starts out empty

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Lookup node or attribute within XML file
    '            If Not IsNothing(myXMLNode) Then
    '                If pathNodeAttrib = "" Then                 'Get node text
    '                    For i = 0 To myXMLNode.ChildNodes.Count - 1
    '                        If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then myList.Add(myXMLNode.ChildNodes(i).InnerText())
    '                    Next i
    '                Else                                'Get node attribute
    '                    For i = 0 To myXMLNode.ChildNodes.Count - 1
    '                        If AttributeExists(myXMLNode.ChildNodes(i), pathNodeAttrib) Then myList.Add(myXMLNode.ChildNodes(i).Attributes.GetNamedItem(pathNodeAttrib).InnerText)
    '                    Next i
    '                End If
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        Finally
    '            ReadNodeListText = False
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Handles multiple child nodes. Writes node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the XML class object.
    '    ''' </summary>
    '    ''' <param name="propValue">Array of property values that will become the text values of the list of nodes.</param>
    '    ''' <param name="pathNode">Path to the parent node in the XML file.</param>
    '    ''' <param name="nameListNode">Name of the list node (the name that is repeated).</param>
    '    ''' <remarks></remarks>
    '    Sub WriteNodeListText(ByVal propValue As String(), ByVal pathNode As String, ByVal nameListNode As String)
    '        'Reads & writes open-ended lists in the XML
    '        Dim myXmlNodeList As XmlNodeList
    '        Dim objXMLNode As XmlNode

    '        Dim myNameSpace As String
    '        Dim i As Integer

    '        If IsNothing(propValue) Then
    '            ReDim propValue(0)
    '            propValue(0) = ""
    '        End If

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Write new values to XML
    '            pathNode = pathNode & "/n:" & nameListNode

    '            'Delete existing nodes in XML
    '            myXmlNodeList = xmlDoc.SelectNodes(pathNode, nsmgr)
    '            For Each myXMLItem As XmlNode In myXmlNodeList
    '                myXMLItem.ParentNode.RemoveChild(myXMLItem)
    '            Next

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            For i = 0 To UBound(propValue)
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, nameListNode, myNameSpace)
    '                objXMLNode.InnerText = propValue(i)
    '                myXMLNode.AppendChild(objXMLNode)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a unique list of absolute paths read from a list of relative paths in the open XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node.</param>
    '    ''' <param name="myRelativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadNodeListPath(ByVal pathNode As String, Optional ByVal myRelativeToProgram As String = "") As ObservableCollection(Of String)
    '        Dim myList As New ObservableCollection(Of String)
    '        Dim myListTemp As New ObservableCollection(Of String)

    '        ReadNodeListText(pathNode, myList)
    '        ConvertToUniqueObsColString(myList)
    '        For Each myPathItem As String In myList  'Convert to absolute path
    '            'Check that paths can be converted to absolute paths, and that the path is valid. Else, clear the list so that a new one is built during initialization
    '            If Not AbsolutePath(myPathItem, myRelativeToProgram, , True) Then
    '                myList.Clear()
    '                Exit For
    '            ElseIf Not FileExists(myPathItem) Then
    '                myList.Clear()
    '                Exit For
    '            End If

    '            myListTemp.Add(myPathItem)
    '        Next

    '        Return myListTemp
    '    End Function

    '    ''' <summary>
    '    ''' Writes a unique list of relative paths from a list of absolute paths to the open XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node.</param>
    '    ''' <param name="nameListNode">Name of the list node (the name that is repeated).</param>
    '    ''' <param name="pathList">List to write to the XML file.</param>
    '    ''' <param name="myRelativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteNodeListPath(ByVal pathNode As String, ByVal nameListNode As String, ByVal pathList As ObservableCollection(Of String), Optional ByVal myRelativeToProgram As String = "")
    '        Dim tempList As New List(Of String)
    '        Dim tempPath As String

    '        For Each myPathItem As String In pathList  'Convert to relative path
    '            tempPath = myPathItem
    '            RelativePath(tempPath, True, myRelativeToProgram)
    '            tempList.Add(tempPath)
    '        Next
    '        ConvertToUniqueListString(tempList)
    '        WriteNodeListText(tempList.ToArray, pathNode, nameListNode)
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a unique list of text read from a list of text in the open XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadUniqueList(ByVal pathNode As String) As ObservableCollection(Of String)
    '        Dim myList As New ObservableCollection(Of String)

    '        ReadNodeListText(pathNode, myList)
    '        ConvertToUniqueObsColString(myList)

    '        Return myList
    '    End Function

    '    ''' <summary>
    '    ''' Writes a unique list of text from a list of text to the open XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">XML path to the desired node.</param>
    '    ''' <param name="nameListNode">Name of the list node (the name that is repeated).</param>
    '    ''' <param name="writeList">List to write to the XML file.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteUniqueList(ByVal pathNode As String, ByVal nameListNode As String, ByVal writeList As ObservableCollection(Of String))
    '        Dim tempList As New List(Of String)

    '        tempList = writeList.ToList
    '        ConvertToUniqueListString(tempList)
    '        WriteNodeListText(tempList.ToArray, pathNode, nameListNode)
    '    End Sub

    '    '=== Direct call to an XML File
    '    ''' <summary>
    '    ''' Writes a value to a single node value in a single XML file from a replace action.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML file.</param>
    '    ''' <param name="myPathNode">Path to the node within the XML file.</param>
    '    ''' <param name="myValueNew">Text value to be written to the node.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteSingleXMLNodeValue(ByVal myPath As String, ByVal myPathNode As String, ByRef myValueNew As String)
    '        If InitializeXML(myPath) Then
    '            WriteNodeText(myValueNew, myPathNode)
    '            SaveXML(myPath)
    '            CloseXML()
    '        End If
    '    End Sub
    '#End Region

    '#Region "Reading/Writing Node Objects"
    '    ''' <summary>
    '    ''' Queries a list of non-unique nodes by tag name, and within them, queries a node specified by node path and adds the value to a supplied list.
    '    ''' </summary>
    '    ''' <param name="tagName">Name of the repeating node element to query.</param>
    '    ''' <param name="singleNodePath">Path to the child node within the repeating node element.</param>
    '    ''' <param name="myList">Optional: List to be populated by the text values of multiple child elements.</param>
    '    ''' <param name="attributeName">Optional: Attribute to populate to the list, if supplied.</param>
    '    ''' <param name="useNameSpace">Optional: If true, a namespace element is used in the paths and must be supplied with the input paths.</param>
    '    ''' <param name="parentNodes">Optional: Name of parent node for truncating the scope of where the list of nodes is gathered.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadXmlObjectText(ByVal tagName As String, ByVal singleNodePath As String, Optional ByRef myList As ObservableCollection(Of String) = Nothing, _
    '                               Optional useNameSpace As Boolean = False, Optional attributeName As String = "", Optional ByVal parentNodes As List(Of String) = Nothing) As String   'Optional confirmationNode As String = ""   ''' <param name="confirmationNode">Optional: If provided, the node list retrieved by the tag name will be checked to see if the confirmation node exists as a child element. If it does not, the function will exit.</param>
    '        Dim nodeValue As String
    '        Dim nodelist As XmlNodeList = Nothing

    '        myList.Clear()  'Ensures that list starts out empty
    '        ReadXmlObjectText = ""

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            If useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Get list of nodes to read
    '            If Not GetNodeList(nodelist, tagName, useNameSpace, xmlDoc.DocumentElement.NamespaceURI, parentNodes) Then Exit Function

    '            'Read the specified node within the node list
    '            For Each node As XmlNode In nodelist
    '                nodeValue = SelectSingleNode(node, singleNodePath, useNameSpace, nsmgr, attributeName)
    '                If Not nodeValue = "" Then myList.Add(nodeValue)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            CloseXML()
    '            Exit Function
    '        End Try

    '        ReadXmlObjectText = ""

    '    End Function

    '    ''' <summary>
    '    ''' Gets a node list of results elements based on the parents node list provided. Also returns the namespace manager object.
    '    ''' </summary>
    '    ''' <param name="tagName">Name of the non-unique nodes to gather as a list.</param>
    '    ''' <param name="myNsmgr">Namespace manager object to assign.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function GetResultsNodeList(ByVal tagName As String, ByRef myNsmgr As XmlNamespaceManager, ByVal parentNodes As List(Of String)) As XmlNodeList
    '        Dim nodeList As XmlNodeList = Nothing

    '        Try
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Get list of results nodes to read
    '            If Not IsNothing(xmlDoc.Item(parentNodes(0))) Then
    '                If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1))) Then
    '                    If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)) Then
    '                        nodeList = xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)
    '                    End If
    '                End If
    '            End If
    '            myNsmgr = nsmgr
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        Return nodeList
    '    End Function

    '    ''' <summary>
    '    ''' Gets a list of non-unique XML nodes based on a tag name.
    '    ''' </summary>
    '    ''' <param name="nodeList">List object to populate with a set of non-unique XML nodes based on tag name.</param>
    '    ''' <param name="tagName">Name of the repeating node element to query.</param>
    '    ''' <param name="useNameSpace">If true, the Xpath needs to use name spaces, and the nsmgr object will be used.</param>
    '    ''' <param name="nameSpaceURI">Namepsace URI used if the Xpath needs to use name spaces.</param>
    '    ''' <param name="parentNodes">Optional: Listed names of parent nodes for truncating the scope of where the list of nodes is gathered.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function GetNodeList(ByRef nodeList As XmlNodeList, ByVal tagName As String, ByVal useNameSpace As Boolean, Optional ByVal nameSpaceURI As String = "", Optional ByVal parentNodes As List(Of String) = Nothing) As Boolean
    '        Try
    '            If useNameSpace Then
    '                If (IsNothing(parentNodes) OrElse parentNodes.Count = 0) Then
    '                    If Not IsNothing(xmlDoc.GetElementsByTagName(tagName, nameSpaceURI)) Then
    '                        nodeList = xmlDoc.GetElementsByTagName(tagName, nameSpaceURI)
    '                        Return True
    '                    End If
    '                Else
    '                    If Not IsNothing(xmlDoc.Item(parentNodes(0))) Then
    '                        If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1))) Then
    '                            If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)) Then
    '                                nodeList = xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)
    '                                Return True
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                If (IsNothing(parentNodes) OrElse parentNodes.Count = 0) Then
    '                    If Not IsNothing(xmlDoc.GetElementsByTagName(tagName)) Then
    '                        nodeList = xmlDoc.GetElementsByTagName(tagName)
    '                        Return True
    '                    End If
    '                Else
    '                    If Not IsNothing(xmlDoc.Item(parentNodes(0))) Then
    '                        If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1))) Then
    '                            If Not IsNothing(xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)) Then
    '                                nodeList = xmlDoc.Item(parentNodes(0)).Item(parentNodes(1)).GetElementsByTagName(tagName)
    '                                Return True
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '        Return False
    '    End Function

    '    ''' <summary>
    '    ''' Returns the text value of a single node or attribute, selected by the name of the node without an Xpath.
    '    ''' </summary>
    '    ''' <param name="node">Node object to select from.</param>
    '    ''' <param name="singleNodePath">Path to the child node within the repeating node element.</param>
    '    ''' <param name="useNameSpace">If true, the Xpath needs to use name spaces, and the nsmgr object will be used.</param>
    '    ''' <param name="nsmgr">Namespace manager object.</param>
    '    ''' <param name="attributeName">Optional: Attribute to populate to the list, if supplied.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function SelectSingleNode(ByVal node As XmlNode, ByVal singleNodePath As String, ByVal useNameSpace As Boolean, Optional nsmgr As XmlNamespaceManager = Nothing, Optional ByVal attributeName As String = "") As String
    '        Dim nodeValue As String = ""

    '        Try
    '            If useNameSpace Then
    '                If Not IsNothing(node.SelectSingleNode(singleNodePath, nsmgr)) Then
    '                    If Not attributeName = "" Then          'Get attribute value
    '                        If Not IsNothing(node.SelectSingleNode(singleNodePath, nsmgr).Attributes.GetNamedItem(attributeName)) Then
    '                            nodeValue = node.SelectSingleNode(singleNodePath, nsmgr).Attributes.GetNamedItem(attributeName).InnerText
    '                        End If
    '                    Else                                    'Get node value
    '                        nodeValue = node.SelectSingleNode(singleNodePath, nsmgr).InnerText
    '                    End If
    '                End If
    '            Else
    '                If Not IsNothing(node.SelectSingleNode(singleNodePath)) Then
    '                    If Not attributeName = "" Then          'Get attribute value
    '                        If Not IsNothing(node.SelectSingleNode(singleNodePath).Attributes.GetNamedItem(attributeName)) Then
    '                            nodeValue = node.SelectSingleNode(singleNodePath).Attributes.GetNamedItem(attributeName).InnerText
    '                        End If
    '                    Else                                    'Get node value
    '                        nodeValue = node.SelectSingleNode(singleNodePath).InnerText
    '                    End If
    '                End If
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        Return nodeValue
    '    End Function

    '    'Not used VVVVVVV
    '    ''' <summary>
    '    ''' Check if the confirmation node exists, if specifed. If so, the function returns True.
    '    ''' </summary>
    '    ''' <param name="confirmationNode">Name of the confirmation node to look for in the node list.</param>
    '    ''' <param name="useNameSpace">If true, the Xpath needs to use name spaces, and the nsmgr object will be used.</param>
    '    ''' <param name="nsmgr">Namespace manager object.</param>
    '    ''' <param name="nodeList">List of XML nodes to check for confirmation.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ConfirmationNodeExists(ByVal confirmationNode As String, ByVal useNameSpace As Boolean, ByVal nsmgr As XmlNamespaceManager, ByVal nodeList As XmlNodeList) As Boolean
    '        Try
    '            If Not confirmationNode = "" Then
    '                If useNameSpace Then
    '                    For Each node As XmlNode In nodeList
    '                        If Not IsNothing(node.SelectSingleNode(confirmationNode, nsmgr)) Then Return True
    '                    Next
    '                Else
    '                    For Each node As XmlElement In nodeList
    '                        If Not IsNothing(node.SelectSingleNode(confirmationNode)) Then Return True
    '                    Next
    '                End If
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        Return True
    '    End Function

    '    'Not used ^^^^^^

    '    ''' <summary>
    '    ''' Updates a particular node value or attribute value that is paired with a specified node value.
    '    ''' </summary>
    '    ''' <param name="tagName">Name of the repeating node element to query.</param>
    '    ''' <param name="lookupNodePath">Path to the child node within the repeating node element to check.</param>
    '    ''' <param name="writeNodePath">Path to the child node within the repeating node element to write to if the check matches.</param>
    '    ''' <param name="nodeValueLookup">String that is contained within the value of the node that is checked, such as the identifying tag.</param>
    '    ''' <param name="nodeValueWrite">String to write to the node value of the corresponding node if a check matches.</param>
    '    ''' <param name="useNameSpace">Optional: If true, a namespace element is used in the paths and must be supplied with the input paths.</param>
    '    ''' <param name="attributeName">Optional: Attribute to write to, if supplied.</param>
    '    ''' <remarks></remarks>
    '    Sub UpdateObjectByTag(ByVal tagName As String, ByVal lookupNodePath As String, ByVal writeNodePath As String, ByVal nodeValueLookup As String, ByVal nodeValueWrite As String, _
    '                               Optional useNameSpace As Boolean = False, Optional attributeName As String = "")
    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            If useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            Dim nodelist As XmlNodeList
    '            If useNameSpace Then
    '                If Not IsNothing(xmlDoc.GetElementsByTagName(tagName, xmlDoc.DocumentElement.NamespaceURI)) Then
    '                    nodelist = xmlDoc.GetElementsByTagName(tagName, xmlDoc.DocumentElement.NamespaceURI)
    '                Else
    '                    Exit Sub
    '                End If
    '            Else
    '                If Not IsNothing(xmlDoc.GetElementsByTagName(tagName)) Then
    '                    nodelist = xmlDoc.GetElementsByTagName(tagName)
    '                Else
    '                    Exit Sub
    '                End If
    '            End If

    '            If useNameSpace Then
    '                For Each node As XmlNode In nodelist
    '                    If Not IsNothing(node.SelectSingleNode(lookupNodePath, nsmgr)) Then
    '                        If Not attributeName = "" Then  'Get attribute value
    '                            If Not IsNothing(node.SelectSingleNode(lookupNodePath, nsmgr).Attributes.GetNamedItem(attributeName)) Then
    '                                If StringExistInName(node.SelectSingleNode(lookupNodePath, nsmgr).InnerText, nodeValueLookup) Then
    '                                    node.SelectSingleNode(writeNodePath, nsmgr).Attributes.GetNamedItem(attributeName).InnerText = nodeValueWrite
    '                                End If
    '                            End If
    '                        Else                            'Get node value
    '                            If StringExistInName(node.SelectSingleNode(lookupNodePath, nsmgr).InnerText, nodeValueLookup) Then
    '                                node.SelectSingleNode(writeNodePath, nsmgr).InnerText = nodeValueWrite
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            Else
    '                For Each node As XmlElement In nodelist
    '                    If Not IsNothing(node.SelectSingleNode(lookupNodePath)) Then
    '                        If Not attributeName = "" Then  'Get attribute value
    '                            If StringExistInName(node.SelectSingleNode(lookupNodePath).InnerText, nodeValueLookup) Then
    '                                If AttributeExists(node, attributeName) Then node.SelectSingleNode(writeNodePath).Attributes.GetNamedItem(attributeName).InnerText = nodeValueWrite
    '                            End If
    '                        Else                            'Get node value
    '                            If StringExistInName(node.SelectSingleNode(lookupNodePath).InnerText, nodeValueLookup) Then
    '                                node.SelectSingleNode(writeNodePath).InnerText = nodeValueWrite
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End If

    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            CloseXML()
    '            Exit Sub
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Queries a list of non-unique nodes by a tag name within a tag name, and within them, queries a node specified by node path and adds the value to a supplied list. The sub-tag name can also target a single node within the main tag name.
    '    ''' </summary>
    '    ''' <param name="tagName">Name of the repeating node element to query.</param>
    '    ''' <param name="tagIdentifier">Name of a child node of the repeated tag element that distinguishes one tag selection from another.</param>
    '    ''' <param name="tagIdentifierValue">Content of a child node of the repeated tag element that distinguishes one tag selection from another.</param>
    '    ''' <param name="singleNodePath">Path to the child node within the repeating node element.</param>
    '    ''' <param name="myList">List to be populated by the text values of multiple child elements.</param>
    '    ''' <param name="singleNodePathParent">Optional: Name of the single node to select within the tag name within which to search for the subTagName. 
    '    ''' If this is blank, the node value is retrieved. If not, the value retrieved is for the child of the node.</param>
    '    ''' <param name="useNameSpace">Optional: If true, a namespace element is used in the paths and must be supplied with the input paths.</param>
    '    ''' <param name="singleNodePathChildren">Optional: If singleNodePathParent is used, this parameter can be used to select individual sub-nodes of the singleNodePathParent sub-node.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadXmlObjectTextSubTag(ByVal tagName As String, ByVal tagIdentifier As String, ByVal tagIdentifierValue As String, ByVal singleNodePath As String, ByRef myList As ObservableCollection(Of String), Optional ByVal singleNodePathParent As String = "", _
    '                                     Optional useNameSpace As Boolean = False, Optional ByVal singleNodePathChildren As String = "") As String
    '        Dim nodeValue As String = ""
    '        Dim nodeList As XmlNodeList = Nothing
    '        Dim nodeSubList As XmlNodeList = Nothing

    '        myList.Clear()  'Ensures that list starts out empty
    '        ReadXmlObjectTextSubTag = ""

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            If useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Get list of nodes to read
    '            If Not GetNodeList(nodeList, tagName, useNameSpace, xmlDoc.DocumentElement.NamespaceURI) Then Exit Function

    '            'Get sub-list of nodes to read
    '            For Each node As XmlNode In nodeList
    '                If Not IsNothing(node.SelectSingleNode(tagIdentifier, nsmgr)) Then
    '                    If node.SelectSingleNode(tagIdentifier, nsmgr).InnerText = tagIdentifierValue Then
    '                        If Not singleNodePathParent = "" Then
    '                            If useNameSpace Then
    '                                If Not IsNothing(node.SelectSingleNode(singleNodePathParent, nsmgr)) Then
    '                                    nodeSubList = node.SelectSingleNode(singleNodePathParent, nsmgr).ChildNodes
    '                                End If
    '                            Else
    '                                If Not IsNothing(node.SelectSingleNode(singleNodePathParent)) Then
    '                                    nodeSubList = node.SelectSingleNode(singleNodePathParent).ChildNodes
    '                                End If
    '                            End If
    '                        Else
    '                            nodeSubList = node.ChildNodes
    '                        End If

    '                        ''Read the specified node within the node list
    '                        If Not IsNothing(nodeSubList) Then
    '                            For Each subNode As XmlNode In nodeSubList
    '                                nodeValue = ""
    '                                If singleNodePath = "" Then
    '                                    If Not subNode.NodeType = XmlNodeType.Comment Then
    '                                        If singleNodePathChildren = "" Then
    '                                            nodeValue = subNode.InnerText
    '                                        Else
    '                                            If useNameSpace Then
    '                                                If Not IsNothing(subNode.SelectSingleNode(singleNodePathChildren, nsmgr)) Then
    '                                                    nodeValue = subNode.SelectSingleNode(singleNodePathChildren, nsmgr).InnerText
    '                                                End If
    '                                            Else
    '                                                If Not IsNothing(subNode.Item(singleNodePathChildren)) Then
    '                                                    nodeValue = subNode.Item(singleNodePathChildren).InnerText
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    Else
    '                                        Continue For
    '                                    End If
    '                                Else
    '                                    nodeValue = SelectSingleNode(subNode, singleNodePath, useNameSpace, nsmgr)
    '                                End If
    '                                If Not nodeValue = "" Then myList.Add(nodeValue)
    '                            Next
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            CloseXML()
    '            Exit Function
    '        End Try

    '        ReadXmlObjectTextSubTag = ""
    '    End Function

    '    'Currently not used
    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="tagName"></param>
    '    ''' <param name="subTagName"></param>
    '    ''' <param name="subSubTagName"></param>
    '    ''' <param name="singleNodePath"></param>
    '    ''' <param name="myList"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadXmlObjectTextSubSubTag(ByVal tagName As String, ByVal subTagName As String, ByVal subSubTagName As String, ByVal singleNodePath As String, ByRef myList As ObservableCollection(Of String)) As String
    '        Dim nodeValue As String

    '        myList.Clear()  'Ensures that list starts out empty
    '        ReadXmlObjectTextSubSubTag = ""

    '        Try
    '            Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName(tagName)
    '            Dim nodeSubList As XmlNodeList
    '            Dim nodeSubSubList As XmlNodeList

    '            For Each node As XmlNode In nodeList
    '                nodeSubList = node.ChildNodes
    '                For Each subNode As XmlNode In nodeSubList
    '                    nodeSubSubList = subNode.ChildNodes
    '                    For Each subSubNode As XmlNode In nodeSubSubList
    '                        nodeValue = node.SelectSingleNode(singleNodePath).InnerText
    '                        myList.Add(nodeValue)
    '                    Next
    '                Next
    '            Next

    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            CloseXML()
    '            Exit Function
    '        End Try

    '        ReadXmlObjectTextSubSubTag = ""

    '    End Function

    '    ''' <summary>
    '    ''' Takes the node name of a collection of identically named child objects, and queries those children's child elements. 
    '    ''' If one element matches the search term, the function will set its value to the corresponding element specified.
    '    ''' </summary>
    '    ''' <param name="pathNode">Path to the lowest unique level in the XML.</param>
    '    ''' <param name="keyNodeValue">Value that the node queried should match.</param>
    '    ''' <param name="lookupNodeName">Name of the node to return a value from if a match is found.</param>
    '    ''' <returns>Text value of a node corresponding to the key node specified.</returns>
    '    ''' <remarks></remarks>
    '    Function ListObjectByKey(ByVal pathNode As String, ByVal keyNodeValue As String, ByVal lookupNodeName As String) As String
    '        Dim matchingListObject As Boolean

    '        matchingListObject = False

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Lookup node or attribute within XML file
    '            For Each nodeParent As XmlNode In myXMLNode

    '                'Check each child node value to see if it matches the key node value
    '                For Each nodeChild As XmlNode In nodeParent
    '                    If nodeChild.InnerText = keyNodeValue Then
    '                        matchingListObject = True                                        'Object key matches
    '                        Exit For
    '                    End If
    '                Next

    '                'If a match is found, get the value from the node to be looked up
    '                If matchingListObject Then
    '                    ''For' loop begun again in case the key node was encountered after the desired lookup node
    '                    For Each nodeChild As XmlNode In nodeParent
    '                        If nodeChild.Name = lookupNodeName Then
    '                            ListObjectByKey = nodeChild.InnerText
    '                            Exit Function
    '                        End If
    '                    Next
    '                End If
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        ListObjectByKey = ""
    '    End Function

    '    '=== CSiTesterSettings.xml
    '    ''' <summary>
    '    ''' Generates a class structure to store the data read from 'objects' hierarchies in the settings XML.
    '    ''' </summary>
    '    ''' <param name="pathNode">Path to the parent node.</param>
    '    ''' <param name="myObjectType">Object type to be read from the settings XML file.</param>
    '    ''' <remarks></remarks>
    '    Sub ReadXmlSettingsObject(ByVal pathNode As String, ByVal myObjectType As eXMLSettingsObjectType)
    '        Dim myKeywordEntry As cKeyword
    '        Dim myClassification As cClassification
    '        Dim myKeywords As cKeywords

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Get object items and properties
    '            For Each myValueGroup As XmlNode In myXMLNode.ChildNodes
    '                myClassification = New cClassification
    '                myKeywords = New cKeywords

    '                Select Case myObjectType
    '                    Case eXMLSettingsObjectType.classification
    '                        myClassification.name = myValueGroup.Attributes.ItemOf("name").Value
    '                        myClassification.nameNode = myValueGroup.Name
    '                        myClassification.description = myValueGroup.Attributes.ItemOf("description").Value
    '                    Case eXMLSettingsObjectType.keyword
    '                        myKeywords.prefix = myValueGroup.Attributes.ItemOf("prefix").Value
    '                        myKeywords.name = myValueGroup.Name
    '                        myKeywords.description = myValueGroup.Attributes.ItemOf("description").Value
    '                End Select

    '                'Get entry items and properties
    '                For Each myValue As XmlNode In myValueGroup.ChildNodes
    '                    myKeywordEntry = New cKeyword
    '                    myKeywordEntry.valueKeyword = myValue.InnerText
    '                    myKeywordEntry.description = myValue.Attributes.ItemOf("description").Value

    '                    'Add entry to object
    '                    Select Case myObjectType
    '                        Case eXMLSettingsObjectType.classification
    '                            myClassification.subClassification.Add(myKeywordEntry)
    '                        Case eXMLSettingsObjectType.keyword
    '                            myKeywords.keywords.Add(myKeywordEntry)
    '                    End Select

    '                Next

    '                'Add object entry to the list of objects
    '                Select Case myObjectType
    '                    Case eXMLSettingsObjectType.classification
    '                        testerSettings.exampleClassifications.Add(myClassification)
    '                    Case eXMLSettingsObjectType.keyword
    '                        testerSettings.exampleKeywords.Add(myKeywords)
    '                End Select
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '    End Sub

    '#End Region

    '#Region "Reading/Writing Specific Cases"
    '    '=== Status Form Syncing
    '    ''' <summary>
    '    ''' Gets a list of unique model ids from the regtest log. If the list ends in 0, then regTest is no longer running additional models.
    '    ''' </summary>
    '    ''' <param name="resultsPath">Optional: If provided, the specified log file will be read. If not provided, it is assumed that the desired XMl file is already open.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function UpdateUniqueModelIDList(Optional ByVal resultsPath As String = "") As List(Of String)
    '        Dim tempList As New List(Of String)

    '        'Open XML File
    '        If Not resultsPath = "" Then
    '            If InitializeXML(resultsPath) Then
    '                'tempList = UpdateUniqueModelIDListComponent()
    '                tempList = UpdateModelIDListComponent()
    '                CloseXML()
    '            End If
    '        Else
    '            'tempList = UpdateUniqueModelIDListComponent()
    '            tempList = UpdateModelIDListComponent()
    '        End If

    '        Return tempList
    '    End Function

    '    ''' <summary>
    '    ''' Gets a list of model ids from the regtest log.
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function UpdateModelIDListComponent() As List(Of String)
    '        Dim j As Integer
    '        Dim pathNode As String = "//n:items"
    '        Dim tempList As New List(Of String)
    '        Dim tempEntry As String
    '        'Dim entryUnique As Boolean

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For j = 0 To myXMLNode.ChildNodes.Count - 1
    '                myXMLObject = myXMLNode.ChildNodes(j)                               'Select object root node
    '                tempEntry = myXMLObject.SelectSingleNode("model_id").InnerText      'Get model id for the item

    '                tempList.Add(tempEntry)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        Return tempList
    '    End Function


    '    'TODO: Not used. Not working correctly either
    '    ''' <summary>
    '    ''' Gets a list of unique model ids from the regtest log. If the list ends in 0, then regTest is no longer running additional models.
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function UpdateUniqueModelIDListComponent() As List(Of String)
    '        Dim j As Integer
    '        Dim pathNode As String = "//n:items"
    '        Dim tempList As New List(Of String)
    '        Dim tempEntry As String
    '        Dim entryUnique As Boolean

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For j = 0 To myXMLNode.ChildNodes.Count - 1
    '                myXMLObject = myXMLNode.ChildNodes(j)                               'Select object root node
    '                tempEntry = myXMLObject.SelectSingleNode("model_id").InnerText      'Get model id for the item

    '                If j = 0 Then
    '                    tempList.Add(tempEntry)                         'Adds the first "0" to the list
    '                Else
    '                    'Sets up unique status of the value and trigger to check remaining values
    '                    If tempList.Count <= 2 Then
    '                        If tempEntry = "0" Then
    '                            entryUnique = False 'Ensures that the second entry is not "0"
    '                        Else
    '                            entryUnique = True
    '                        End If
    '                    ElseIf tempList.Count > 2 Then
    '                        If Not tempList(tempList.Count - 2) = "0" Then entryUnique = True 'Ensures that from the third entry on, the next "0" is only added after non-zero values are added. No more are added after this as they will not be unique in the range checked.
    '                    End If

    '                    'Check that id is unique part from the first entry and if so, add it
    '                    If entryUnique Then
    '                        For i = 1 To tempList.Count - 1
    '                            If tempList(i) = tempEntry Then
    '                                entryUnique = False
    '                                Exit For
    '                            End If
    '                        Next
    '                    End If
    '                    If entryUnique Then
    '                        tempList.Add(tempEntry)
    '                    End If
    '                End If
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try

    '        Return tempList
    '    End Function

    '    '=== Model Control XML Validation
    '    ''' <summary>
    '    ''' Reads the results of the file for validating model control XML Files.
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadValidationEntries(ByVal resultsPath As String) As ObservableCollection(Of cValidationModelXML) 'ByVal pathNode As String, ByVal subPathNode As String)
    '        Dim j As Integer
    '        Dim myValidationEntry As cValidationModelXML
    '        Dim myValidationEntries As New ObservableCollection(Of cValidationModelXML)
    '        Dim pathNode As String = "//n:files"

    '        'Open XML File
    '        If InitializeXML(resultsPath) Then
    '            Try
    '                'Create an XmlNamespaceManager for resolving namespaces.
    '                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '                myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '                For j = 0 To myXMLNode.ChildNodes.Count - 1
    '                    myValidationEntry = New cValidationModelXML                     'Creates new entry class
    '                    'myXMLObject = xmlRoot.SelectSingleNode(pathNode).ChildNodes(j)  'Selects object root node
    '                    myXMLObject = myXMLNode.ChildNodes(j)  'Selects object root node

    '                    'Populate entry class properties
    '                    myValidationEntry.modelID = myXMLObject.SelectSingleNode("model_id").InnerText
    '                    myValidationEntry.filePath = myXMLObject.SelectSingleNode("file_path").InnerText
    '                    myValidationEntry.XMLFileName = GetSuffix(myValidationEntry.filePath, "\")
    '                    myValidationEntry.schemaVersion = myXMLObject.SelectSingleNode("schema_version").InnerText
    '                    myValidationEntry.statusValidation = myXMLObject.SelectSingleNode("status").InnerText
    '                    myValidationEntry.commentValidation = myXMLObject.SelectSingleNode("//n:comment/n:item/n:validation_message", nsmgr).InnerText
    '                    myValidationEntry.statusDuplicateID = myXMLObject.SelectSingleNode("duplicate_model_id_status").InnerText
    '                    myValidationEntry.commentDuplicateID = myXMLObject.SelectSingleNode("duplicate_model_id_message").InnerText

    '                    'Add class to list
    '                    myValidationEntries.Add(myValidationEntry)
    '                Next j
    '            Catch ex As Exception
    '                csiLogger.ExceptionAction(ex)
    '            Finally
    '                CloseXML()
    '            End Try
    '        End If

    '        ReadValidationEntries = myValidationEntries
    '    End Function

    '    ''' <summary>
    '    ''' Handles creation and updating of the zero_tolerance element in the MC XML file.
    '    ''' </summary>
    '    ''' <param name="nodeIndex">Index of the result item to perform the operation on.</param>
    '    ''' <param name="resultType">Whether the result is a post-processed or regular result.</param>
    '    ''' <param name="attributeValue">Value to write to the attribute.</param>
    '    ''' <param name="overWriteExisting">If the attribute exists, if True then the new value will be written. If False, the original value will be maintained.</param>
    '    ''' <remarks></remarks>
    '    Sub HandleZeroTolerance(ByVal nodeIndex As Integer, ByVal resultType As eResultType, Optional ByVal attributeValue As String = "1", Optional ByVal overWriteExisting As Boolean = True)
    '        Dim pathnode As String = "//n:result[" & nodeIndex + 1 & "]"
    '        Dim attribute As String = "zero_tolerance"

    '        If resultType = eResultType.regular Then
    '            pathnode = pathnode & "/n:output_field/n:value"
    '        ElseIf resultType = eResultType.postProcessed Then
    '            pathnode = pathnode & "/n:formula/n:result"
    '        End If

    '        If NodeExists(pathnode) Then
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathnode, nsmgr)

    '            If Not AttributeExists(myXMLNode, attribute) Then
    '                CreateNodeByPath(pathnode, attribute, attributeValue, eXMLElementType.Attribute, eNodeCreate.child)
    '            Else
    '                If overWriteExisting Then WriteNodeText(attributeValue, pathnode, attribute)
    '            End If
    '        End If

    '    End Sub

    '    '=== Syncing Folder/File Operations
    '    ''' <summary>
    '    ''' Creates a list of the new paths expected for files associated with the object node as the folder structure is flattened or changed to a DB hierarchy. 
    '    ''' Also updates the XML files to keep the references in sync.
    '    ''' </summary>
    '    ''' <param name="myXMLPath">Path to the XML file to be read.</param>
    '    ''' <param name="pathNode">Absolute path to the parent node from which object values are read.</param>
    '    ''' <param name="queryNodeName">Name of the node to be queried.</param>
    '    ''' <param name="queryNodeValue">Value that the query node is expected to contain for a match. This can be a partial match.</param>
    '    ''' <param name="corrNodeName">Name of the node that corresponds to a matching query node.</param>
    '    ''' <param name="myAction">Whether the class creation and node value change operation reflects a flattening or DB creation folder operation.</param>
    '    ''' <param name="corrNodeValueStub">Prefix to the value that the corresponding node should contain.</param>
    '    ''' <returns>A list of the new paths expected for files associated with the object node.</returns>
    '    ''' <remarks></remarks>
    '    Function GetXMLNodeObjectValuePairs(ByVal myXMLPath As String, ByVal pathNode As String, _
    '                                             ByVal queryNodeName As String, ByVal queryNodeValue As String, _
    '                                             ByVal corrNodeName As String, ByRef myAction As eXMLEditorAction, _
    '                                             Optional ByVal corrNodeValueStub As String = "") As ObservableCollection(Of String)
    '        Dim readNodeText As String
    '        Dim triggerTextFound As Boolean
    '        Dim tempCollection As New ObservableCollection(Of String)

    '        If InitializeXML(myXMLPath) Then
    '            Try
    '                'Create an XmlNamespaceManager for resolving namespaces.
    '                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '                myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '                For Each myAttachmentNode As XmlNode In myXMLNode.ChildNodes
    '                    triggerTextFound = False
    '                    'Check each object parent/root node
    '                    For Each myChildNode As XmlNode In myAttachmentNode.ChildNodes
    '                        'Find query node within a given node object parent
    '                        If myChildNode.Name = queryNodeName Then
    '                            readNodeText = myChildNode.InnerText

    '                            'If query node contains the trigger text, gather the corresponding node value
    '                            If StringExistInName(readNodeText.ToUpper, queryNodeValue.ToUpper) Then
    '                                triggerTextFound = True
    '                                Exit For
    '                            End If
    '                        End If
    '                    Next
    '                    If triggerTextFound Then
    '                        For Each myChildNode As XmlNode In myAttachmentNode.ChildNodes
    '                            'Find corresponding node within a given node object parent
    '                            If myChildNode.Name = corrNodeName Then
    '                                'Add node value to list
    '                                tempCollection.Add(myChildNode.InnerText)

    '                                'Adjust node value
    '                                Select Case myAction
    '                                    Case eXMLEditorAction.DirectoriesFlatten
    '                                        'Strip away relative path additions, as files will be placed at the same level as the model file & controlling XML
    '                                        myChildNode.InnerText = GetSuffix(myChildNode.InnerText, "\")
    '                                        Exit For
    '                                    Case eXMLEditorAction.DirectoriesDBGather
    '                                        'Reapppend relative path additions, if any
    '                                        If Not corrNodeValueStub = "" Then corrNodeValueStub = trimPathSlash(corrNodeValueStub) & "\"
    '                                        myChildNode.InnerText = corrNodeValueStub & myChildNode.InnerText
    '                                        Exit For
    '                                End Select
    '                            End If
    '                        Next
    '                    End If
    '                Next
    '            Catch ex As Exception
    '                readNodeText = ""
    '            End Try
    '            SaveXML(myXMLPath)
    '            CloseXML()
    '        End If

    '        GetXMLNodeObjectValuePairs = tempCollection
    '    End Function

    '#End Region

    '#Region "Creating/Inserting/Deleting Nodes"
    '    '=== Component Function
    '    ''' <summary>
    '    ''' Creates a node of a given xml path, name, value and type. 
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the parent node from which to read the object child nodes.</param>
    '    ''' <param name="nameNode">Name of the new node.</param>
    '    ''' <param name="valueNode">Value of the new node.</param>
    '    ''' <param name="typeNode">New node type.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateNodeByPath(ByVal pathNode As String, ByVal nameNode As String, ByVal valueNode As String, _
    '                         ByVal typeNode As eXMLElementType, ByVal createMethod As eNodeCreate)
    '        Dim objXMLNode As XmlNode
    '        Dim objXMLAttr As XmlAttribute
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            If typeNode = eXMLElementType.Attribute Then
    '                objXMLAttr = xmlDoc.CreateAttribute(nameNode)
    '                objXMLAttr.Value = valueNode
    '                myXMLNode.Attributes.Append(objXMLAttr)
    '            Else
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, nameNode, myNameSpace)
    '                objXMLNode.InnerText = valueNode

    '                Select Case createMethod
    '                    Case eNodeCreate.child
    '                        myXMLNode.AppendChild(objXMLNode)

    '                    Case eNodeCreate.insertBefore
    '                        Dim myXMLNodeParent As XmlNode
    '                        Dim parentPathNode As String = FilterStringFromName(pathNode, "/n:" & GetSuffix(pathNode, ":"), True, False)

    '                        myXMLNodeParent = xmlRoot.SelectSingleNode(parentPathNode, nsmgr)

    '                        myXMLNodeParent.InsertBefore(objXMLNode, myXMLNode)

    '                    Case eNodeCreate.insertAfter
    '                        Dim myXMLNodeParent As XmlNode
    '                        Dim parentPathNode As String = FilterStringFromName(pathNode, "/n:" & GetSuffix(pathNode, ":"), True, False)

    '                        myXMLNodeParent = xmlRoot.SelectSingleNode(parentPathNode, nsmgr)

    '                        myXMLNodeParent.InsertAfter(objXMLNode, myXMLNode)
    '                End Select
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '            Exit Sub
    '        End Try


    '    End Sub

    '    ''' <summary>
    '    ''' Deletes a node of a given xml path. If a path node attribute is specified, the attribute is removed.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to be removed, or that contains the attribute to be removed</param>
    '    ''' <param name="pathNodeAttrib">Optional: Name of the attribute to be removed</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteNodeByPath(ByVal pathNode As String, Optional ByVal pathNodeAttrib As String = "")
    '        'Create an XmlNamespaceManager for resolving namespaces.
    '        Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '        nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
    '        myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '        If pathNodeAttrib = "" Then
    '            'If node has children, clean out contents
    '            If ChildCount(pathNode) > 0 Then
    '                myXMLNode.RemoveAll()
    '            End If

    '            'Delete node
    '            myXMLNode.ParentNode.RemoveChild(myXMLNode)
    '        Else
    '            pathNodeAttrib = GetSuffix(pathNodeAttrib, "@")         'Remove parent node name if appended as 'node@attribute' form

    '            myXMLNode.Attributes.RemoveNamedItem(pathNodeAttrib)
    '        End If
    '    End Sub

    '    '=== Direct call to an XML File
    '    ''' <summary>
    '    ''' Adds new nodes to the specified XML file.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to and including the XML file name.</param>
    '    ''' <param name="pathNode">Absolute path to the parent node to add the current node to.</param>
    '    ''' <param name="nameNode">Name of the new node.</param>
    '    ''' <param name="valueNode">Value of the new node.</param>
    '    ''' <param name="typeNode">New node type.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateNodeInXMLFile(ByVal myPath As String, ByVal pathNode As String, ByVal nameNode As String, ByVal valueNode As String, _
    '                            ByVal typeNode As eXMLElementType, ByVal createMethod As eNodeCreate)
    '        Try
    '            If InitializeXML(myPath) Then
    '                CreateNodeByPath(pathNode, nameNode, valueNode, typeNode, createMethod)   'Create nodes
    '                SaveXML(myPath)
    '                CloseXML()
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Deletes the node of a specified path from a specified XML file.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML file.</param>
    '    ''' <param name="deleteNodePath">Path to the node to delte.</param>
    '    ''' <param name="deleteNodeAttribute">Optional: Attribute name to delete. If specified, only the attribute will be deleted.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteNodeInXMLFile(ByVal myPath As String, ByVal deleteNodePath As String, Optional ByVal deleteNodeAttribute As String = "")
    '        Try
    '            If InitializeXML(myPath) Then
    '                DeleteNodeByPath(deleteNodePath, deleteNodeAttribute)   'Create nodes
    '                SaveXML(myPath)
    '                CloseXML()
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "Creating Node Objects"

    '    '=== Model control XML
    '    ''' <summary>
    '    ''' Creates a new 'Incident' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="incidents">List of incident numbers to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectIncident(ByVal pathNode As String, ByVal incidents As ObservableCollection(Of Integer))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each incident As Integer In incidents
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "incident", myNameSpace)

    '                'Create child node and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "number", myNameSpace)
    '                objXMLNode.InnerText = CStr(incident)
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Ticket' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="tickets">List of ticket numbers to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectTicket(ByVal pathNode As String, ByVal tickets As ObservableCollection(Of Integer))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each ticket In tickets
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "ticket", myNameSpace)

    '                'Create child node and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "number", myNameSpace)
    '                objXMLNode.InnerText = CStr(ticket)
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Link' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="links">List of link values to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectLink(ByVal pathNode As String, ByVal links As ObservableCollection(Of cMCLink))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            'Create Header Node
    '            objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "link", myNameSpace)

    '            For Each link As cMCLink In links
    '                'Create child node and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
    '                objXMLNode.InnerText = link.title
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "url", myNameSpace)
    '                objXMLNode.InnerText = link.URL
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Image' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="images">List of 'image' objects to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectImage(ByVal pathNode As String, ByVal images As ObservableCollection(Of cMCImage))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each image As cMCImage In images
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "image", myNameSpace)

    '                'Create child node and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
    '                objXMLNode.InnerText = image.title
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "path", myNameSpace)
    '                objXMLNode.InnerText = image.path
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Attachment' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="attachments">List of 'attachment' objects to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectAttachment(ByVal pathNode As String, ByVal attachments As ObservableCollection(Of cMCAttachment))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each attachment As cMCAttachment In attachments
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "attachment", myNameSpace)

    '                'Create child node and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "title", myNameSpace)
    '                objXMLNode.InnerText = attachment.title
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "path", myNameSpace)
    '                objXMLNode.InnerText = attachment.path
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Update' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="updates">List of 'updates' objects to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectUpdate(ByVal pathNode As String, ByVal updates As ObservableCollection(Of cMCUpdate))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNodeSubHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim myNameSpace As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            If NodeExists(pathNode) Then
    '                myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)
    '            Else
    '                myXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "updates", myNameSpace)
    '            End If

    '            For Each update As cMCUpdate In updates
    '                'Create Sub Header Node for Date
    '                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "date", myNameSpace)

    '                'Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "day", myNameSpace)
    '                objXMLNode.InnerText = CStr(update.updateDate.numDay)
    '                objXMLNodeSubHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "month", myNameSpace)
    '                objXMLNode.InnerText = CStr(update.updateDate.numMonth)
    '                objXMLNodeSubHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "year", myNameSpace)
    '                objXMLNode.InnerText = CStr(update.updateDate.numYear)
    '                objXMLNodeSubHeader.AppendChild(objXMLNode)

    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "update", myNameSpace)

    '                'Append Sub Header Node
    '                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)

    '                'Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "person", myNameSpace)
    '                objXMLNode.InnerText = update.person
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "ticket", myNameSpace)
    '                If update.ticket > 0 Then
    '                    objXMLNode.InnerText = CStr(update.ticket)
    '                Else
    '                    objXMLNode.InnerText = updateNoTicket
    '                End If
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
    '                objXMLNode.InnerText = update.comment
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Result' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="results">List of 'results' objects to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectResult(ByVal pathNode As String, ByVal results As ObservableCollection(Of cMCResult))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNodeSubHeader As XmlNode
    '        Dim objXMLNodeSubSubHeader As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim objXMLAttr As XmlAttribute
    '        Dim myNameSpace As String
    '        Dim tempString As String = ""

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each result As cMCResult In results
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

    '                '= Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
    '                objXMLNode.InnerText = result.comment
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "table_name", myNameSpace)
    '                objXMLNode.InnerText = result.tableName
    '                objXMLNodeHeader.AppendChild(objXMLNode)


    '                '= Create Sub Header Node for 'lookup_fields'
    '                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "lookup_fields", myNameSpace)

    '                For Each field As cFieldLookup In result.fieldsLookup
    '                    '== Create Sub Sub Header for 'field'
    '                    objXMLNodeSubSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "field", myNameSpace)

    '                    '=== Create child nodes and append to header node
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
    '                    objXMLNode.InnerText = field.name
    '                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)

    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)
    '                    objXMLNode.InnerText = field.valueField
    '                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)

    '                    '= Append Sub Sub Header Nodes to 'lookup_fields'
    '                    objXMLNodeSubHeader.AppendChild(objXMLNodeSubSubHeader)
    '                Next

    '                '= Append Sub Sub Header Nodes 'lookup_fields'
    '                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)


    '                '= Create Sub Header Node for 'output_field'
    '                objXMLNodeSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "output_field", myNameSpace)

    '                '== Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
    '                objXMLNode.InnerText = result.fieldOutput.name
    '                objXMLNodeSubHeader.AppendChild(objXMLNode)

    '                '=== Create Sub Sub Header Node for 'value'
    '                objXMLNodeSubSubHeader = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)

    '                '===== Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "benchmark", myNameSpace)
    '                objXMLNode.InnerText = result.fieldOutput.valueBenchmark
    '                objXMLAttr = xmlDoc.CreateAttribute("is_correct")
    '                objXMLAttr.Value = GetEnumDescription(result.fieldOutput.isCorrect)
    '                objXMLNode.Attributes.Append(objXMLAttr)
    '                If Not result.fieldOutput.roundBenchmark = "" Then
    '                    objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
    '                    objXMLAttr.Value = CStr(result.fieldOutput.roundBenchmark)
    '                    objXMLNode.Attributes.Append(objXMLAttr)
    '                End If
    '                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

    '                If Not result.fieldOutput.zeroTolerance = "" Then
    '                    objXMLAttr = xmlDoc.CreateAttribute("zero_tolerance")
    '                    objXMLAttr.Value = CStr(result.fieldOutput.zeroTolerance)
    '                    objXMLNode.Attributes.Append(objXMLAttr)
    '                End If
    '                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "theoretical", myNameSpace)
    '                objXMLNode.InnerText = result.fieldOutput.valueTheoretical
    '                objXMLNodeSubSubHeader.AppendChild(objXMLNode)

    '                If Not result.fieldOutput.valueLastBest = "" Then
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "last_best", myNameSpace)
    '                    objXMLNode.InnerText = result.fieldOutput.valueLastBest
    '                    objXMLNodeSubSubHeader.AppendChild(objXMLNode)
    '                End If

    '                If Not result.fieldOutput.shiftCalc = 0 Then
    '                    objXMLAttr = xmlDoc.CreateAttribute("shift_for_calculating_percent_difference")
    '                    objXMLAttr.Value = CStr(result.fieldOutput.shiftCalc)
    '                    objXMLNodeSubSubHeader.Attributes.Append(objXMLAttr)
    '                End If

    '                '= Append Sub Sub Header Nodes to 'output_field'
    '                objXMLNodeSubHeader.AppendChild(objXMLNodeSubSubHeader)


    '                '= Append Sub Header Node 'output_field'
    '                objXMLNodeHeader.AppendChild(objXMLNodeSubHeader)


    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a new 'Postprocessed_Result' object entry in an XML file.
    '    ''' </summary>
    '    ''' <param name="pathNode">Absolute path to the node to which object values are to be added.</param>
    '    ''' <param name="results">List of 'postprocessed_results' objects to add to the object to be created.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateObjectResultPostProcessed(ByVal pathNode As String, ByVal results As ObservableCollection(Of cMCResult))
    '        Dim objXMLNodeHeader As XmlNode
    '        Dim objXMLNodeSub1Header As XmlNode
    '        Dim objXMLNodeSub2Header As XmlNode
    '        Dim objXMLNodeSub3Header As XmlNode
    '        Dim objXMLNodeSub4Header As XmlNode
    '        Dim objXMLNodeSub5Header As XmlNode
    '        Dim objXMLNode As XmlNode
    '        Dim objXMLAttr As XmlAttribute
    '        Dim myNameSpace As String
    '        Dim tempString As String = ""

    '        Dim rangeType As String

    '        Try
    '            'Create an XmlNamespaceManager for resolving namespaces.
    '            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
    '            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

    '            'Insert new nodes in XML w text
    '            myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

    '            'Node to append object to
    '            myXMLNode = xmlRoot.SelectSingleNode(pathNode, nsmgr)

    '            For Each result As cMCResult In results
    '                'Create Header Node
    '                objXMLNodeHeader = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

    '                '= Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "comment", myNameSpace)
    '                objXMLNode.InnerText = result.comment
    '                objXMLNodeHeader.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "table_name", myNameSpace)
    '                objXMLNode.InnerText = result.tableName
    '                objXMLNodeHeader.AppendChild(objXMLNode)


    '                If Not IsNothing(result.range) Then
    '                    'Set range type
    '                    If result.range.isRangeAll Then
    '                        rangeType = rangeTypeAll
    '                    Else
    '                        rangeType = rangeTypeRange
    '                    End If

    '                    '= Create Sub Header Node for 'range'
    '                    objXMLNodeSub1Header = xmlDoc.CreateNode(XmlNodeType.Element, rangeType, myNameSpace)

    '                    '== Create child nodes and append to header node
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "range_operation", myNameSpace)
    '                    objXMLNode.InnerText = GetEnumDescription(result.range.rangeOperation)
    '                    objXMLNodeSub1Header.AppendChild(objXMLNode)

    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "field_name", myNameSpace)
    '                    objXMLNode.InnerText = result.range.fieldName
    '                    objXMLNodeSub1Header.AppendChild(objXMLNode)

    '                    If rangeType = rangeTypeRange Then
    '                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_first", myNameSpace)
    '                        objXMLNode.InnerText = result.range.valueFirst
    '                        objXMLNodeSub1Header.AppendChild(objXMLNode)

    '                        objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_last", myNameSpace)
    '                        objXMLNode.InnerText = result.range.valueLast
    '                        objXMLNodeSub1Header.AppendChild(objXMLNode)
    '                    End If

    '                    '= Append Sub Header Nodes 'range'
    '                    objXMLNodeHeader.AppendChild(objXMLNodeSub1Header)
    '                End If

    '                '= Create Sub Header Node for 'formula'
    '                objXMLNodeSub1Header = xmlDoc.CreateNode(XmlNodeType.Element, "formula", myNameSpace)

    '                '== Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "operation", myNameSpace)
    '                objXMLNode.InnerText = GetEnumDescription(result.formula.operation)
    '                objXMLNodeSub1Header.AppendChild(objXMLNode)


    '                '== Create Sub Sub Header Node for 'variables'
    '                objXMLNodeSub2Header = xmlDoc.CreateNode(XmlNodeType.Element, "variables", myNameSpace)

    '                For Each variable In result.formula.variables
    '                    '=== Create Sub Sub Sub Header Node for 'variable'
    '                    objXMLNodeSub3Header = xmlDoc.CreateNode(XmlNodeType.Element, "variable", myNameSpace)

    '                    '==== Create child nodes and append to header node
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "scale_factor", myNameSpace)
    '                    objXMLNode.InnerText = CStr(variable.scaleFactor)
    '                    objXMLNodeSub3Header.AppendChild(objXMLNode)

    '                    '==== Create Sub Sub Sub Sub header for 'lookup_fields'
    '                    objXMLNodeSub4Header = xmlDoc.CreateNode(XmlNodeType.Element, "lookup_fields", myNameSpace)

    '                    For Each field As cFieldLookup In result.fieldsLookup
    '                        If Not IsNothing(result.range) Then
    '                            'Set range type
    '                            If result.range.isRangeAll Then
    '                                rangeType = rangeTypeAll
    '                            Else
    '                                rangeType = rangeTypeRange
    '                            End If

    '                            '= Create Sub Header Node for 'range'
    '                            objXMLNodeSub5Header = xmlDoc.CreateNode(XmlNodeType.Element, rangeType, myNameSpace)

    '                            '== Create child nodes and append to header node
    '                            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "range_operation", myNameSpace)
    '                            objXMLNode.InnerText = GetEnumDescription(variable.range.rangeOperation)
    '                            objXMLNodeSub5Header.AppendChild(objXMLNode)

    '                            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "field_name", myNameSpace)
    '                            objXMLNode.InnerText = variable.range.fieldName
    '                            objXMLNodeSub5Header.AppendChild(objXMLNode)

    '                            If rangeType = rangeTypeRange Then
    '                                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_first", myNameSpace)
    '                                objXMLNode.InnerText = variable.range.valueFirst
    '                                objXMLNodeSub5Header.AppendChild(objXMLNode)

    '                                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value_last", myNameSpace)
    '                                objXMLNode.InnerText = variable.range.valueLast
    '                                objXMLNodeSub5Header.AppendChild(objXMLNode)
    '                            End If

    '                            '= Append Sub Header Nodes 'range'
    '                            objXMLNodeSub4Header.AppendChild(objXMLNodeSub5Header)
    '                        End If

    '                        For Each myField As cFieldLookup In variable.fieldsLookup
    '                            '== Create Sub Sub Header for 'field'
    '                            objXMLNodeSub5Header = xmlDoc.CreateNode(XmlNodeType.Element, "field", myNameSpace)

    '                            '=== Create child nodes and append to header node
    '                            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
    '                            objXMLNode.InnerText = myField.name
    '                            objXMLNodeSub5Header.AppendChild(objXMLNode)

    '                            objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "value", myNameSpace)
    '                            objXMLNode.InnerText = myField.valueField
    '                            objXMLNodeSub5Header.AppendChild(objXMLNode)

    '                            '= Append Sub Header Nodes 'lookup_fields'
    '                            objXMLNodeSub4Header.AppendChild(objXMLNodeSub5Header)
    '                        Next
    '                    Next

    '                    '= Append Sub Sub Sub Sub Header Nodes 'lookup_fields' to 'variable'
    '                    objXMLNodeSub3Header.AppendChild(objXMLNodeSub4Header)

    '                    '==== Create Sub Sub Sub Sub Header Node for 'output_field'
    '                    objXMLNodeSub4Header = xmlDoc.CreateNode(XmlNodeType.Element, "output_field", myNameSpace)

    '                    '== Create child nodes and append to header node
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "name", myNameSpace)
    '                    objXMLNode.InnerText = variable.fieldOutput.name
    '                    objXMLNodeSub4Header.AppendChild(objXMLNode)


    '                    '= Append Sub Sub Sub Sub Header Nodes 'output_field' to 'variable'
    '                    objXMLNodeSub3Header.AppendChild(objXMLNodeSub4Header)

    '                    '= Append Sub Sub Sub Header Node for 'variable' to 'variables'
    '                    objXMLNodeSub2Header.AppendChild(objXMLNodeSub3Header)
    '                Next

    '                '= Append Sub Sub Header Node for 'variables' to 'formula'
    '                objXMLNodeSub1Header.AppendChild(objXMLNodeSub2Header)



    '                '=== Create Sub Sub Header Node for 'result'
    '                objXMLNodeSub2Header = xmlDoc.CreateNode(XmlNodeType.Element, "result", myNameSpace)

    '                '===== Create child nodes and append to header node
    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "benchmark", myNameSpace)
    '                objXMLNode.InnerText = result.formula.result.valueBenchmark
    '                objXMLAttr = xmlDoc.CreateAttribute("is_correct")
    '                objXMLAttr.Value = GetEnumDescription(result.formula.result.isCorrect)
    '                objXMLNode.Attributes.Append(objXMLAttr)
    '                If Not result.formula.result.roundBenchmark = "" Then
    '                    objXMLAttr = xmlDoc.CreateAttribute("significant_digits")
    '                    objXMLAttr.Value = CStr(result.formula.result.roundBenchmark)
    '                    objXMLNode.Attributes.Append(objXMLAttr)
    '                End If
    '                objXMLNodeSub2Header.AppendChild(objXMLNode)

    '                objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "theoretical", myNameSpace)
    '                objXMLNode.InnerText = result.formula.result.valueTheoretical
    '                objXMLNodeSub2Header.AppendChild(objXMLNode)

    '                If Not result.formula.result.valueLastBest = "" Then
    '                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, "last_best", myNameSpace)
    '                    objXMLNode.InnerText = result.formula.result.valueLastBest
    '                    objXMLNodeSub2Header.AppendChild(objXMLNode)
    '                End If

    '                If Not result.formula.result.shiftCalc = Nothing Or Not result.formula.result.shiftCalc = 0 Then
    '                    objXMLAttr = xmlDoc.CreateAttribute("shift_for_calculating_percent_difference")
    '                    objXMLAttr.Value = CStr(result.formula.result.shiftCalc)
    '                    objXMLNodeSub2Header.Attributes.Append(objXMLAttr)
    '                End If

    '                '= Append Sub Sub Header Nodes to 'output_field'
    '                objXMLNodeSub1Header.AppendChild(objXMLNodeSub2Header)


    '                '= Append Sub Header Node 'output_field'
    '                objXMLNodeHeader.AppendChild(objXMLNodeSub1Header)


    '                'Append header node to XML file
    '                myXMLNode.AppendChild(objXMLNodeHeader)
    '            Next
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '#End Region

    '#Region "Mirror XML Functions Into Memory"
    '    '=== Creating the object in memory drawn from the XML file
    '    ''' <summary>
    '    ''' For each XML file in the specified directory, runs the 'Mirror XML' file function to generate a mirrored class, and adds it to a collection.
    '    ''' </summary>
    '    ''' <param name="myXMLPathList">Optional list of paths to gather for creating the list of XML class objects.</param>
    '    ''' <param name="ReplaceExisting">Optional specification of whether the routine is creating a new list of objects (True), or adding to the existing list (False).</param>
    '    ''' <remarks></remarks>
    '    Sub MirrorAllEditorXMLS(Optional ByVal myXMLPathList As ObservableCollection(Of String) = Nothing, Optional ByVal ReplaceExisting As Boolean = True)
    '        If IsNothing(myXMLPathList) Then myXMLPathList = myCsiTester.suiteXMLPathList

    '        If ReplaceExisting Then myXMLEditor.suiteEditorXMLObjects.Clear()

    '        For Each myXMLpath As String In myXMLPathList
    '            myXMLEditor.suiteEditorXMLObjects.Add(mirrorXMLElementsAll(myXMLpath))
    '        Next

    '    End Sub

    '    ''' <summary>
    '    ''' Creates a list of all nodes and attributes in an XML document, with various properties recorded. This sub sets up the xml file then calls another sub.
    '    ''' </summary>
    '    ''' <param name="p_xmlFilePath">Path to the XML file to be used.</param>
    '    ''' <remarks></remarks>
    '    Function mirrorXMLElementsAll(ByVal p_xmlFilePath As String) As cXMLObject
    '        Dim childLevel As Integer = 1
    '        Dim nodePath As String = "//n:"
    '        Dim xmlFileNode As New cXMLNode

    '        xmlFile = New cXMLObject

    '        Try
    '            If InitializeXML(p_xmlFilePath) Then
    '                xmlFile.fileName = GetPathFileName(p_xmlFilePath)

    '                nodePath = nodePath & xmlRoot.Name

    '                'Create root node
    '                With xmlFileNode
    '                    .filePath = p_xmlFilePath
    '                    .name = xmlRoot.Name
    '                    .xmlPath = nodePath
    '                    .level = 0
    '                    .indexFlat = 0
    '                    .type = eXMLElementType.Header
    '                End With

    '                xmlFile.xmlMirror.Add(xmlFileNode)

    '                'Set XML path and create attributes and child nodes
    '                Mirror_ChildNodes(p_xmlFilePath, childLevel, xmlRoot.ChildNodes, 0, nodePath, xmlFileNode)

    '                CloseXML()
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        Finally
    '            mirrorXMLElementsAll = xmlFile
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Creates a list of all child nodes in an XML document, sorted by node vs. attribute type, and header vs. value node. Hierarchy levels are recorded, as well as value node index and header info.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the XML file to be used.</param>
    '    ''' <param name="myChildLevel">Level in the nodal hierarchy. Level 1 is the first level as the root node is at level 0.</param>
    '    ''' <param name="myChildNodes">Node object from the XML file that includes all child nodes.</param>
    '    ''' <param name="myCounter">Current value node index in file.</param>
    '    ''' <param name="myParentNode">Optional: Parent node object, for attaching newly created child classes to.</param>
    '    ''' <param name="myXMLPath">Absolute XML path within the XML file.</param>
    '    ''' <remarks></remarks>
    '    Sub Mirror_ChildNodes(ByVal myPath As String, ByRef myChildLevel As Integer, ByVal myChildNodes As XmlNodeList, _
    '                          ByRef myCounter As Integer, ByRef myXMLPath As String, _
    '                          Optional ByVal myParentNode As cXMLNode = Nothing)
    '        Dim childCount As Integer
    '        Dim nodeHeader As String
    '        Dim indexFlat As Integer
    '        Dim lastXMLPathNode As String

    '        lastXMLPathNode = myXMLPath

    '        'Gather root node attributes, if they exist
    '        If Not IsNothing(xmlRoot.Attributes) And Not myChildLevel > 1 Then
    '            For Each xmlAttr As XmlAttribute In xmlRoot.Attributes
    '                myCounter = myCounter + 1
    '                indexFlat = myCounter

    '                CreateMirrorNode(myPath, myChildLevel, indexFlat, myParentNode, myXMLPath, eXMLElementType.Attribute, xmlAttr)
    '            Next
    '        End If

    '        For Each xmlNodeItem As Xml.XmlNode In myChildNodes
    '            childCount = 0
    '            Err.Clear()
    '            On Error Resume Next

    '            'Counts children based on 3 cases
    '            childCount = XMLChildCount(xmlNodeItem)

    '            If childCount > 0 Then      'Header Node
    '                'Counter is not advanced for header, but header index should correspond to the next value node encountered
    '                indexFlat = myCounter + 1

    '                CreateMirrorNode(myPath, myChildLevel, indexFlat, myParentNode, myXMLPath, eXMLElementType.Header, , xmlNodeItem)

    '                'Update XML Path variable & write node path property
    '                myXMLPath = myXMLPath & "/n:" & myXMLFileNode.name

    '                'Advance child level and call function again
    '                myChildLevel = myChildLevel + 1
    '                Call Mirror_ChildNodes(myPath, myChildLevel, xmlNodeItem.ChildNodes, myCounter, myXMLPath, myXMLFileNode)
    '            Else                        'Value Node or attribute
    '                myCounter = myCounter + 1
    '                indexFlat = myCounter

    '                CreateMirrorNode(myPath, myChildLevel, indexFlat, myParentNode, myXMLPath, eXMLElementType.Node, , xmlNodeItem)

    '                'Gather attributes, if they exist
    '                If Not IsNothing(xmlNodeItem.Attributes) Then
    '                    For Each xmlAttr As XmlAttribute In xmlNodeItem.Attributes
    '                        myCounter = myCounter + 1
    '                        indexFlat = myCounter

    '                        CreateMirrorNode(myPath, myChildLevel, indexFlat, myParentNode, myXMLPath, eXMLElementType.Attribute, xmlAttr, xmlNodeItem)
    '                    Next
    '                End If
    '            End If

    '            'Reverse XML path variable back
    '            myXMLPath = lastXMLPathNode
    '        Next

    '        'Reverse child level back
    '        myChildLevel = myChildLevel - 1

    '        'lastXMLPathNode = GetSuffix(myXMLPath, ":")
    '        'myXMLPath = FilterStringFromName(myXMLPath, "/n:" & lastXMLPathNode, True, False)

    '        'Establish column span of header at prior child level
    '        For Each myHeader As cXMLNode In myParentNode.xmlChildren
    '            If IsMatchingHeaderWithoutChildren(myHeader, myChildLevel) Then
    '                myHeader.valueNodeSpan = myCounter - myHeader.indexFlat + 1
    '                Exit For
    '            End If
    '        Next
    '    End Sub

    '    ''' <summary>
    '    ''' Returns 'True' if the node object provided is a header, of the specified child level, with no children.
    '    ''' </summary>
    '    ''' <param name="p_xmlNode">Node object to check.</param>
    '    ''' <param name="p_childLevel">The number of levels down in the parent-child hierachy the node is.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function IsMatchingHeaderWithoutChildren(ByVal p_xmlNode As cXMLNode,
    '                                                     ByVal p_childLevel As Integer) As Boolean

    '        If (p_xmlNode.type = eXMLElementType.Header AndAlso
    '            p_xmlNode.level = p_childLevel AndAlso
    '            p_xmlNode.valueNodeSpan = 0) Then

    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function


    '    ''' <summary>
    '    ''' Creates the individual node property to be mirrored in memory.
    '    ''' </summary>
    '    ''' <param name="p_nodePath">Path to the XML file to be used.</param>
    '    ''' <param name="p_childLevel">Level in the nodal hierarchy. Level 1 is the first level as the root node is at level 0.</param>
    '    ''' <param name="p_indexFlat">Number assigned to the node in the order that the node was encountered. Exception: The index is the same for the first node at two different child levels.</param>
    '    ''' <param name="p_parentNode">Parent node object, for attaching newly created child classes to.</param>
    '    ''' <param name="p_xmlPath">Absolute XML path within the XML file.</param>
    '    ''' <param name="p_elementType">Node type of attribute, header (node with no text value), or node (node with text value).</param>
    '    ''' <param name="p_xmlAttr">Optional: XML attribute node.</param>
    '    ''' <param name="p_xmlNodeItem">Optional: XML item node.</param>
    '    ''' <remarks></remarks>
    '    Sub CreateMirrorNode(ByVal p_nodePath As String,
    '                         ByRef p_childLevel As Integer,
    '                         ByRef p_indexFlat As Integer,
    '                         ByRef p_parentNode As cXMLNode,
    '                         ByRef p_xmlPath As String,
    '                         ByVal p_elementType As eXMLElementType,
    '                         Optional ByVal p_xmlAttr As XmlAttribute = Nothing,
    '                         Optional ByVal p_xmlNodeItem As Xml.XmlNode = Nothing)

    '        Dim tempXMLNodeName As String

    '        'Create new node class
    '        myXMLFileNode = New cXMLNode

    '        'Set node names & XML Paths
    '        With myXMLFileNode
    '            If IsNothing(p_xmlNodeItem) Then                              'Root Node Attributes
    '                .name = p_xmlAttr.Name
    '                .xmlPath = p_xmlPath
    '            Else
    '                If p_xmlNodeItem.Name = "#text" Then                      'Case 1: Value node with text (Value nodes only)
    '                    tempXMLNodeName = p_xmlNodeItem.ParentNode.Name
    '                Else                                                    'Case 2: Value node with no text
    '                    tempXMLNodeName = p_xmlNodeItem.Name
    '                End If

    '                'Set Node Name
    '                If IsNothing(p_xmlAttr) Then                              'Node
    '                    .name = tempXMLNodeName
    '                Else                                                    'Other Node Attributes
    '                    .name = p_xmlNodeItem.Name & "@" & p_xmlAttr.Name
    '                End If

    '                .xmlPath = p_xmlPath & "/n:" & tempXMLNodeName
    '            End If

    '            'Populate Common Properties
    '            .indexFlat = p_indexFlat
    '            .type = p_elementType
    '            .filePath = p_nodePath
    '            .level = p_childLevel

    '            'Populate node properties
    '            Select Case p_elementType
    '                Case eXMLElementType.Attribute : .value = p_xmlAttr.InnerText
    '                Case eXMLElementType.Header 'No action needed
    '                Case eXMLElementType.Node : .value = p_xmlNodeItem.InnerText
    '            End Select
    '        End With

    '        'Add node class to parent classes
    '        p_parentNode.xmlChildren.Add(myXMLFileNode)
    '    End Sub

    '    '=== Manipulating the XML file based on the object in memory.
    '    ''' <summary>
    '    ''' Saves the XML mirror class back over the XML file.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to and including the XML file name.</param>
    '    ''' <param name="myXMLCollection">Branch of 'nodes' recorded in the XML mirror class.</param>
    '    ''' <remarks></remarks>
    '    Sub UpdateXMLFile(ByVal myPath As String, ByRef myXMLCollection As ObservableCollection(Of cXMLNode))
    '        Dim ChildLevel As Integer = 1

    '        Try
    '            If InitializeXML(myPath) Then
    '                UpdateXMLFileNodes(ChildLevel, xmlRoot.ChildNodes, -1, myXMLCollection)

    '                'Delete nodes, if specified
    '                myXMLEditor.nodeDeleted = False

    '                SaveXML(myPath)
    '                CloseXML()

    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Overwrites a given XML node value with the node value from the mirror class, if the class property is set to be recorded. This saves an edited XML node-by-node as specified.
    '    ''' </summary>
    '    ''' <param name="myChildLevel">Level in the nodal hierarchy. Level 1 is the first level as the root node is at level 0.</param>
    '    ''' <param name="myChildNodes">Node object from the XML file that includes all child nodes.</param>
    '    ''' <param name="myCounter">Current 0-based value node index in file.</param>
    '    ''' <param name="myXMLCollection">Branch of 'nodes' recorded in the XML mirror class</param>
    '    ''' <remarks>This assumes that the class that is being saved to the XML has the exact same structure of the XML. Will not work if any nodes have been added or removed in either object</remarks>
    '    Sub UpdateXMLFileNodes(ByRef myChildLevel As Integer, ByVal myChildNodes As XmlNodeList, ByRef myCounter As Integer, ByRef myXMLCollection As ObservableCollection(Of cXMLNode), Optional ByVal myCounterTemp As Integer = -2)
    '        Dim childCount As Integer = 0
    '        Dim myMirrorNode As cXMLNode
    '        Dim nameNode As String
    '        Dim useMyTempCount As Boolean = False

    '        'Update root node attributes, if they exist
    '        If Not IsNothing(xmlRoot.Attributes) And Not myChildLevel > 1 Then

    '            For Each xmlAttr As XmlAttribute In xmlRoot.Attributes
    '                myCounter = myCounter + 1
    '                myMirrorNode = myXMLCollection(myCounter)

    '                'Writes changes and updates class
    '                If myMirrorNode.saveChanges And xmlAttr.Name = myMirrorNode.name Then
    '                    xmlAttr.InnerText = myMirrorNode.value

    '                    'Update node status in class
    '                    myMirrorNode.saveChanges = False
    '                    myMirrorNode.valueChanged = False
    '                End If
    '            Next
    '        End If

    '        'Updates regular nodes and any subsequent attributes
    '        For Each xmlNodeItem As Xml.XmlNode In myChildNodes
    '            'Increments node count
    '            'If node is child node, counter needs to be reset, since it needs to be in sync with the child branch passed into the function
    '            If Not myCounterTemp = -2 Then useMyTempCount = True
    '            If useMyTempCount Then
    '                myCounterTemp = myCounterTemp + 1
    '                myMirrorNode = myXMLCollection(myCounterTemp)
    '            Else
    '                myCounter = myCounter + 1
    '                myMirrorNode = myXMLCollection(myCounter)
    '            End If

    '            childCount = 0
    '            Err.Clear()
    '            On Error Resume Next

    '            'Counts children based on 3 cases
    '            childCount = XMLChildCount(xmlNodeItem)

    '            If childCount > 0 Then
    '                'Counter is not advanced for header, but header index should correspond to the next value node encountered
    '                'Advance child level and call function again
    '                myChildLevel = myChildLevel + 1
    '                UpdateXMLFileNodes(myChildLevel, xmlNodeItem.ChildNodes, myCounter, myMirrorNode.xmlChildren, -1)
    '            Else
    '                'Check node data
    '                If xmlNodeItem.Name = "#text" Then                      'Case 1: Value node with text
    '                    nameNode = xmlNodeItem.ParentNode.Name
    '                Else                                                    'Case 2: Value node with no text
    '                    nameNode = xmlNodeItem.Name
    '                End If

    '                'Writes changes and updates class
    '                If myMirrorNode.saveChanges And nameNode = myMirrorNode.name And myMirrorNode.level = myChildLevel Then
    '                    xmlNodeItem.InnerText = myMirrorNode.value

    '                    'Update node status in class
    '                    myMirrorNode.saveChanges = False
    '                    myMirrorNode.valueChanged = False
    '                End If

    '                'Gather attributes, if they exist
    '                If Not IsNothing(xmlNodeItem.Attributes) Then
    '                    For Each xmlAttr As XmlAttribute In xmlNodeItem.Attributes
    '                        'If node is child node, counter needs to be reset, since it needs to be in sync with the child branch passed into the function
    '                        If useMyTempCount Then
    '                            myCounterTemp = myCounterTemp + 1
    '                            myMirrorNode = myXMLCollection(myCounterTemp)
    '                        Else
    '                            myCounter = myCounter + 1
    '                            myMirrorNode = myXMLCollection(myCounter)
    '                        End If

    '                        nameNode = GetSuffix(myMirrorNode.name, "@")

    '                        'Writes changes and updates class
    '                        If myMirrorNode.saveChanges And xmlAttr.Name = nameNode And myMirrorNode.level = myChildLevel Then
    '                            xmlAttr.InnerText = myMirrorNode.value

    '                            'Update node status in class
    '                            myMirrorNode.saveChanges = False
    '                            myMirrorNode.valueChanged = False
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next

    '        'Reverse child level back
    '        myChildLevel = myChildLevel - 1
    '    End Sub

    '#End Region

End Module

'Node types
'The documentElement property of the XML document is the root node.
'
'node.getElementsByTagName("tagname")   ' Returns all elements with a specified tag name "tagname"
'node.getNamedItem("name")              ' Returns the specified node (by name "name")
'node.Length                            ' Returns number of nodes in list
'====node.item(0)                           ' Returns the node at the specified index in a NodeList
'
'http://www.w3schools.com/dom/dom_nodetype.asp
'xmlNode.nodeName      'Returns node name
'====xmlNode.nodeValue
'====xmlNode.nodeType       '   Returns node type:  1 = ELEMENT_NODE, 2 = ATTRIBUTE_Node, 3 = TEXT_NODE, 7 = PROCESSING_INSTRUCTION NODE, 8 = Comment_Node, 9 = DOCUMENT_NODE
'xmlNode.Text          'Returns text value, if present, of node. Else, returns blank. Returns all text values of all subnodes as well.

'=====To change node value
'x=xmlDoc.getElementsByTagName("title")[0].childNodes[0];
'x.nodeValue="Easy Cooking";
'=====