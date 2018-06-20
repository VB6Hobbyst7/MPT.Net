Option Strict On
Option Explicit On

Imports System.Xml
Imports System.IO

Imports MPT.Lists.ListLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

Namespace ReaderWriter
    ''' <summary>
    ''' Contains routines for reading/writing to XML files, including creating and removing elements.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cXmlReadWrite
        Shared Event Log(exception As LoggerEventArgs)
        Shared Event Message(message As MessengerEventArgs)

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

#Region "Properties"
        Public Property xmlDoc As New XmlDocument
        Public Property xmlRoot As XmlElement
        Friend Property myXMLNode As XmlNode
        Friend Property myXMLObject As XmlNode

        Friend Property myXMLFileNode As New cXMLNode
        Friend Property xmlFile As New cXMLObject
#End Region

#Region "Initialization"
        Public Sub New()


        End Sub
#End Region

#Region "XML Read/Write Master Functions"

        ''' <summary>
        ''' Opens a new XML object for reading and writing.
        ''' </summary>
        ''' <param name="p_path">Path to the XML to be accessed.</param>
        ''' <param name="p_suppressWarning">True: Prevents catch prompts from appearing.</param>
        ''' <remarks></remarks>
        Public Function InitializeXML(ByVal p_path As String,
                                             Optional ByVal p_suppressWarning As Boolean = False) As Boolean
            Dim xmlPathName As String

            'Initialize XML
            xmlPathName = p_path

            xmlDoc = New XmlDocument

            Try
                If File.Exists(xmlPathName) Then
                    xmlDoc.Load(xmlPathName)
                Else
                    Return False
                End If
            Catch ex As Exception
                If Not p_suppressWarning Then
                    RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_path), p_path))
                End If

                Return False
            End Try

            xmlRoot = xmlDoc.DocumentElement

            Return True
        End Function

        ''' <summary>
        ''' Saves XML file according to a supplied file path.
        ''' </summary>
        ''' <param name="p_path">Path to the XML to be accessed.</param>
        ''' <param name="p_noOverWrite">True: Sets 'read only' files to being readable..</param>
        ''' <remarks></remarks>
        Public Sub SaveXML(ByVal p_path As String,
                                  Optional ByVal p_noOverWrite As Boolean = False)
            Try
                If File.Exists(p_path) Then
                    'Sets the specified file to being readable if it is 'read only'.
                    If Not p_noOverWrite Then SetFileNotReadOnly(p_path)

                    'Saves file if XML is modified
                    xmlDoc.Save(p_path)
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                NameOfParam(Function() p_path), p_path,
                                                NameOfParam(Function() p_noOverWrite), p_noOverWrite))
            End Try
        End Sub

        ''' <summary>
        ''' Empties XML objects.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub CloseXML()
            Try
                xmlDoc = Nothing
                xmlRoot = Nothing
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex))
            End Try
        End Sub


        ''' <summary>
        ''' Clears all nodes within a specified node name.
        ''' </summary>
        ''' <param name="p_pathNode">Path to the highest level node to remain.</param>
        ''' <param name="p_nameListNode">Name of the child nodes to remove.</param>
        ''' <remarks></remarks>
        Public Sub ClearObjects(ByVal p_pathNode As String,
                                       ByVal p_nameListNode As String)
            Dim myXmlNodeList As XmlNodeList

            Try
                If NodeExists(p_pathNode) Then
                    'Create an XmlNamespaceManager for resolving namespaces.
                    Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                    nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                    myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                    'Delete existing nodes in XML
                    p_pathNode = p_pathNode & "/n:" & p_nameListNode

                    myXmlNodeList = xmlDoc.SelectNodes(p_pathNode, nsmgr)
                    For Each myXMLItem As XmlNode In myXmlNodeList
                        myXMLItem.ParentNode.RemoveChild(myXMLItem)
                    Next
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_pathNode), p_pathNode,
                                                 NameOfParam(Function() p_nameListNode), p_nameListNode))
            End Try
        End Sub

#End Region

#Region "Get XML/Node Information"
        ''' <summary>
        ''' Returns the count of child nodes of a specified node path. No filtering of node types is performed.
        ''' </summary>
        ''' <param name="p_pathNode">Path to the XML node to be checked.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ChildCount(ByVal p_pathNode As String) As Integer
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                Return myXMLNode.ChildNodes.Count
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_pathNode), p_pathNode))
                Return 0
            End Try
        End Function

        ''' <summary>
        ''' Counts the number of 1st-level child nodes of a given node, not including comment nodes.
        ''' Usually used to determine if there are child nodes to be searched in recursive functions.
        ''' </summary>
        ''' <param name="p_pathNode">Path to the desired XML node.</param>
        ''' <remarks></remarks>
        Public Function CountChildNodes(ByVal p_pathNode As String) As Integer
            Dim childNodesCount As Integer = 0

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                'Lookup node or attribute within XML file
                For i As Integer = 0 To myXMLNode.ChildNodes.Count - 1
                    If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then childNodesCount = childNodesCount + 1
                Next i

                Return childNodesCount
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_pathNode), p_pathNode))
                Return 0
            End Try

            'TODO: experiment with below instead of above in the 'Try' statement
            ''Counts children based on 3 cases
            'If Not myXMLNode.ChildNodes.Count > 0 Then                  'Case 1: Node has no child
            '    childNodesCount = 0
            'ElseIf Not myXMLNode.FirstChild.Name = "#text" Then         'Case 2: Node has child, but child has no text (e.g. folder/header node)
            '    'Screen out comment nodes from count
            '    For i = 0 To myXMLNode.ChildNodes.Count - 1             'Case 2a: Node has children, but the first child is a comment node
            '        If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then childNodesCount = childNodesCount + 1
            '    Next i
            'Else                                                        'Case 3: Node has child, children have text. Count skips a node layer as each text item is yet another node
            '    childNodesCount = myXMLNode.ChildNodes(0).ChildNodes.Count
            'End If

        End Function

        ''' <summary>
        ''' Counts number of children that a node has, considering no child, a child with no text, and a child with text.
        ''' </summary>
        ''' <param name="p_node">XML node object being queried.</param>
        ''' <remarks></remarks>
        Public Function XMLChildCount(ByVal p_node As XmlNode) As Integer
            'Counts children based on 3 cases
            If Not p_node.ChildNodes.Count > 0 Then                'Case 1: Node has no child
                XMLChildCount = 0
            ElseIf Not p_node.FirstChild.Name = "#text" Then       'Case 2: Node has child, but child has no text (e.g. folder/header node)
                XMLChildCount = p_node.ChildNodes.Count
            Else                                                        'Case 3: Node has child, children have text. Count skips a node layer as each text item is yet another node
                XMLChildCount = p_node.ChildNodes(0).ChildNodes.Count
            End If
        End Function

        ''' <summary>
        ''' Returns 'true' if the node exists in the currently active xml file. Otherwise, returns false.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the node to write the new value to.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function NodeExists(ByVal p_pathNode As String) As Boolean
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                If xmlRoot.SelectSingleNode(p_pathNode, nsmgr) Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_pathNode), p_pathNode))
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Returns 'true' if the attribute exists in the provided node object. Otherwise, returns false.
        ''' </summary>
        ''' <param name="p_node">Node object to check.</param>
        ''' <param name="p_pathNodeAttrib">Name of the attribute to look for.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AttributeExists(ByVal p_node As XmlNode,
                                               ByVal p_pathNodeAttrib As String) As Boolean
            If p_node.NodeType = XmlNodeType.Comment Then Return False
            If p_node.Attributes.GetNamedItem(p_pathNodeAttrib) Is Nothing Then
                Return False
            Else
                Return True
            End If
        End Function

#End Region

#Region "Reading/Writing Nodes"
        '=== Component Function
        ''' <summary>
        ''' Handles single nodes. Reads node value assignment or attribute value assignment, based on specified node and optional attribute.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <param name="p_pathNodeAttrib">Attribute of the desired node.</param>
        ''' <returns>Node value or node attribute value</returns>
        ''' <remarks></remarks>
        Public Function ReadNodeText(ByVal p_pathNode As String,
                                            Optional ByVal p_pathNodeAttrib As String = "") As String
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                If NodeExists(p_pathNode) Then
                    myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                    If String.IsNullOrEmpty(p_pathNodeAttrib) Then
                        Return myXMLNode.InnerText
                    Else
                        If AttributeExists(myXMLNode, p_pathNodeAttrib) Then Return myXMLNode.Attributes.GetNamedItem(p_pathNodeAttrib).InnerText
                    End If
                End If
            Catch ex As Exception

            End Try

            ReadNodeText = ""
        End Function

        ''' <summary> 
        ''' Handles single nodes. Writes node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the regTest class object. 
        ''' </summary> 
        ''' <param name="p_propValue">Property value, usually from the regTest class creat for virtual memory storage.</param> 
        ''' <param name="p_pathNode">XML path to the node to write the new value to.</param> 
        ''' <param name="p_pathNodeAttrib">Attribute to write the new attribute value to.</param> 
        ''' <remarks></remarks> 
        Public Sub WriteNodeText(ByVal p_propValue As String,
                                 ByVal p_pathNode As String,
                                 Optional ByVal p_pathNodeAttrib As String = "")
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                If myXMLNode IsNot Nothing Then
                    If String.IsNullOrEmpty(p_pathNodeAttrib) Then
                        'The loop below is necessary if a node containing text also has child nodes. 
                        'Otherwise, the child nodes may have the same value applied to them as well is using the 'InnerText' property
                        For Each myChildNode As XmlNode In myXMLNode.ChildNodes
                            If myChildNode.Name = "#text" Then
                                myChildNode.InnerText = p_propValue
                                Exit Sub
                            End If
                        Next

                        myXMLNode.InnerText = p_propValue             'Contingency if node currently does not contain text
                    Else
                        If AttributeExists(myXMLNode, p_pathNodeAttrib) Then myXMLNode.Attributes.GetNamedItem(p_pathNodeAttrib).InnerText = p_propValue
                    End If
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                    NameOfParam(Function() p_propValue), p_propValue,
                                                    NameOfParam(Function() p_pathNode), p_pathNode,
                                                    NameOfParam(Function() p_pathNodeAttrib), p_pathNodeAttrib))
            End Try
        End Sub

        ''' <summary>
        ''' Returns the string content of the specified child and subchild zero-based index numbers.
        ''' </summary>
        ''' <param name="p_pathNode">Path to the node to be read.</param>
        ''' <param name="p_firstChildIndex">Index number of the first child node.</param>
        ''' <param name="p_secondChildIndex">Index number of the second child node.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReadChildText(ByVal p_pathNode As String,
                                             ByVal p_firstChildIndex As Integer,
                                             ByVal p_secondChildIndex As Integer) As String
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                If myXMLNode.ChildNodes.Item(p_firstChildIndex).ChildNodes.Item(p_secondChildIndex) IsNot Nothing Then
                    Return myXMLNode.ChildNodes.Item(p_firstChildIndex).ChildNodes.Item(p_secondChildIndex).InnerText
                Else
                    Return ""
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                   NameOfParam(Function() p_pathNode), p_pathNode,
                                                   NameOfParam(Function() p_firstChildIndex), p_firstChildIndex,
                                                   NameOfParam(Function() p_secondChildIndex), p_secondChildIndex))
                Return ""
            End Try
        End Function

        '=== Direct call to an XML File
        ''' <summary>
        ''' Gets a single node value from a single XML file from a single query.
        ''' </summary>
        ''' <param name="p_path">Path to the XML file.</param>
        ''' <param name="p_pathNode">Path to the node within the XML file.</param>
        ''' <param name="p_nodeValue">Text value of the node queried.</param>
        ''' <param name="p_pathNodeAttrib">Name of the attribute to the node specified. 
        ''' Will return the attribute value.</param>
        ''' <param name="p_cleanPath">True: Path will be trimmed of white space, leading and ending "\", and all "\\".</param>
        ''' <remarks></remarks>
        Public Sub GetSingleXMLNodeValue(ByVal p_path As String,
                                            ByVal p_pathNode As String,
                                            ByRef p_nodeValue As String,
                                            Optional ByVal p_pathNodeAttrib As String = "",
                                            Optional ByVal p_cleanPath As Boolean = False)
            If InitializeXML(p_path) Then
                p_nodeValue = ReadNodeText(p_pathNode, p_pathNodeAttrib)
                CloseXML()

                If p_cleanPath Then
                    TrimWhiteSpace(p_nodeValue)
                    p_nodeValue = TrimPathSlash(p_nodeValue, True)
                    CleanDoubleSlash(p_nodeValue)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Returns a list of node velus from a single XML file from a single query of a single node with multiple child nodes.
        ''' </summary>
        ''' <param name="p_path">Path to the XML file.</param>
        ''' <param name="p_pathNode">Path to the node within the XML file.</param>
        ''' <param name="p_pathNodeAttrib">Name of the attribute to the node specified. Will return the attribute value</param>
        ''' <param name="p_cleanPath">True: Path will be trimmed of white space, leading and ending "\", and all "\\".</param>
        ''' <remarks></remarks>
        Public Function GetSingleXMLNodeListValues(ByVal p_path As String,
                                                    ByVal p_pathNode As String,
                                                    Optional ByVal p_pathNodeAttrib As String = "",
                                                    Optional ByVal p_cleanPath As Boolean = False) As List(Of String)
            Dim nodeValues As New List(Of String)

            If InitializeXML(p_path) Then
                ReadNodeListText(p_pathNode, nodeValues, p_pathNodeAttrib)
                CloseXML()

                If p_cleanPath Then
                    For Each nodeValue As String In nodeValues
                        TrimWhiteSpace(nodeValue)
                        nodeValue = TrimPathSlash(nodeValue, True)
                        CleanDoubleSlash(nodeValue)
                    Next
                End If
            End If

            Return nodeValues
        End Function
#End Region

#Region "Reading/Writing Node Lists"
        ''' <summary>
        ''' Handles multiple child nodes of the same name. Reads node value assignment based on specified node.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <param name="p_childNodesValues">List array to be populated by multiple node child elements.</param>
        ''' <param name="p_pathNodeAttrib">If provided, the function will perform the operation on the specified attribute of the node.</param>
        ''' <returns>Node value or node attribute value.</returns>
        ''' <remarks></remarks>
        Public Function ReadNodeListText(ByVal p_pathNode As String,
                                        ByRef p_childNodesValues As List(Of String),
                                        Optional ByVal p_pathNodeAttrib As String = "") As Boolean
            p_childNodesValues.Clear()  'Ensures that list starts out empty

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                'Lookup node or attribute within XML file
                If myXMLNode IsNot Nothing Then
                    If String.IsNullOrEmpty(p_pathNodeAttrib) Then                 'Get node text
                        For i As Integer = 0 To myXMLNode.ChildNodes.Count - 1
                            If Not myXMLNode.ChildNodes(i).NodeType = XmlNodeType.Comment Then p_childNodesValues.Add(myXMLNode.ChildNodes(i).InnerText())
                        Next i
                    Else                                'Get node attribute
                        For i As Integer = 0 To myXMLNode.ChildNodes.Count - 1
                            If AttributeExists(myXMLNode.ChildNodes(i), p_pathNodeAttrib) Then p_childNodesValues.Add(myXMLNode.ChildNodes(i).Attributes.GetNamedItem(p_pathNodeAttrib).InnerText)
                        Next i
                    End If
                End If
                Return True
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                   NameOfParam(Function() p_pathNode), p_pathNode,
                                                   NameOfParam(Function() p_pathNodeAttrib), p_pathNodeAttrib))
            Finally
                ReadNodeListText = False
            End Try
        End Function

        ''' <summary>
        ''' Handles multiple child nodes. Writes node value assignment or attribute value assignment into the XML file, based on specified node and optional attribute recorded in the XML class object.
        ''' </summary>
        ''' <param name="p_propValue">Array of property values that will become the text values of the list of nodes.</param>
        ''' <param name="p_pathNode">Path to the parent node in the XML file.</param>
        ''' <param name="p_nameListNode">Name of the list node (the name that is repeated).</param>
        ''' <remarks></remarks>
        Public Sub WriteNodeListText(ByVal p_propValue As String(),
                                    ByVal p_pathNode As String,
                                    ByVal p_nameListNode As String)
            'Reads & writes open-ended lists in the XML
            Dim myXmlNodeList As XmlNodeList
            Dim objXMLNode As XmlNode

            Dim myNameSpace As String
            Dim i As Integer

            If p_propValue Is Nothing Then
                ReDim p_propValue(0)
                p_propValue(0) = ""
            End If

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                'Write new values to XML
                p_pathNode = p_pathNode & "/n:" & p_nameListNode

                'Delete existing nodes in XML
                myXmlNodeList = xmlDoc.SelectNodes(p_pathNode, nsmgr)
                For Each myXMLItem As XmlNode In myXmlNodeList
                    myXMLItem.ParentNode.RemoveChild(myXMLItem)
                Next

                'Insert new nodes in XML w text
                myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

                For i = 0 To UBound(p_propValue)
                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, p_nameListNode, myNameSpace)
                    objXMLNode.InnerText = p_propValue(i)
                    myXMLNode.AppendChild(objXMLNode)
                Next
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                   NameOfParam(Function() p_pathNode), p_pathNode,
                                                   NameOfParam(Function() p_propValue), p_propValue,
                                                   NameOfParam(Function() p_nameListNode), p_nameListNode))
            End Try
        End Sub

        ''' <summary>
        ''' Creates a unique list of absolute paths read from a list of relative paths in the open XML file.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReadNodeListPath(ByVal p_pathNode As String,
                                        Optional ByVal p_relativeToProgram As String = "") As List(Of String)
            Dim myList As New List(Of String)
            Dim myListTemp As New List(Of String)

            ReadNodeListText(p_pathNode, myList)
            myList = ConvertToUniqueList(myList)
            For Each myPathItem As String In myList  'Convert to absolute path
                'Check that paths can be converted to absolute paths, and that the path is valid. Else, clear the list so that a new one is built during initialization
                If Not AbsolutePath(myPathItem, p_relativeToProgram, , True) Then
                    myList.Clear()
                    Exit For
                ElseIf Not File.Exists(myPathItem) Then
                    myList.Clear()
                    Exit For
                End If

                myListTemp.Add(myPathItem)
            Next

            Return myListTemp
        End Function

        ''' <summary>
        ''' Writes a unique list of relative paths from a list of absolute paths to the open XML file.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <param name="p_nameListNode">Name of the list node (the name that is repeated).</param>
        ''' <param name="p_pathList">List to write to the XML file.</param>
        ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
        ''' <remarks></remarks>
        Public Sub WriteNodeListPath(ByVal p_pathNode As String,
                                    ByVal p_nameListNode As String,
                                    ByVal p_pathList As ICollection(Of String),
                                    Optional ByVal p_relativeToProgram As String = "")
            Dim tempList As New List(Of String)
            Dim tempPath As String

            For Each myPathItem As String In p_pathList  'Convert to relative path
                tempPath = myPathItem
                RelativePath(tempPath, True, p_relativeToProgram)
                tempList.Add(tempPath)
            Next
            tempList = ConvertToUniqueList(tempList)
            WriteNodeListText(tempList.ToArray, p_pathNode, p_nameListNode)
        End Sub

        ''' <summary>
        ''' Creates a unique list of text read from a list of text in the open XML file.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReadUniqueList(ByVal p_pathNode As String) As List(Of String)
            Dim myList As New List(Of String)

            ReadNodeListText(p_pathNode, myList)
            myList = ConvertToUniqueList(myList)

            Return myList
        End Function

        ''' <summary>
        ''' Writes a unique list of text from a list of text to the open XML file.
        ''' </summary>
        ''' <param name="p_pathNode">XML path to the desired node.</param>
        ''' <param name="p_nameListNode">Name of the list node (the name that is repeated).</param>
        ''' <param name="p_writeList">List to write to the XML file.</param>
        ''' <remarks></remarks>
        Public Sub WriteUniqueList(ByVal p_pathNode As String,
                                    ByVal p_nameListNode As String,
                                    ByVal p_writeList As ICollection(Of String))
            Dim tempList As New List(Of String)

            tempList = p_writeList.ToList
            tempList = ConvertToUniqueList(tempList)
            WriteNodeListText(tempList.ToArray, p_pathNode, p_nameListNode)
        End Sub

        '=== Direct call to an XML File
        ''' <summary>
        ''' Writes a value to a single node value in a single XML file from a replace action.
        ''' </summary>
        ''' <param name="p_path">Path to the XML file.</param>
        ''' <param name="p_pathNode">Path to the node within the XML file.</param>
        ''' <param name="p_valueNew">Text value to be written to the node.</param>
        ''' <remarks></remarks>
        Public Sub WriteSingleXMLNodeValue(ByVal p_path As String,
                                            ByVal p_pathNode As String,
                                            ByRef p_valueNew As String)
            If InitializeXML(p_path) Then
                WriteNodeText(p_valueNew, p_pathNode)
                SaveXML(p_path)
                CloseXML()
            End If
        End Sub
#End Region

#Region "Reading/Writing Node Objects"
        ''' <summary>
        ''' Queries a list of non-unique nodes by tag name, and within them, queries a node specified by node path and adds the value to a supplied list.
        ''' </summary>
        ''' <param name="p_tagName">Name of the repeating node element to query.</param>
        ''' <param name="p_singleNodePath">Path to the child node within the repeating node element.</param>
        ''' <param name="p_childValues">List to be populated by the text values of multiple child elements.</param>
        ''' <param name="p_attributeName">Attribute to populate to the list, if supplied.</param>
        ''' <param name="p_useNameSpace">True: A namespace element is used in the paths and must be supplied with the input paths.</param>
        ''' <param name="p_parentNodes">Name of parent node for truncating the scope of where the list of nodes is gathered.</param>
        ''' <remarks></remarks>
        Public Sub ReadXmlObjectText(ByVal p_tagName As String,
                                    ByVal p_singleNodePath As String,
                                    Optional ByRef p_childValues As List(Of String) = Nothing,
                                    Optional ByVal p_useNameSpace As Boolean = False,
                                    Optional ByVal p_attributeName As String = "",
                                    Optional ByVal p_parentNodes As List(Of String) = Nothing)
            Dim nodeValue As String
            Dim nodelist As XmlNodeList = Nothing

            p_childValues.Clear()  'Ensures that list starts out empty

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                If p_useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                'Get list of nodes to read
                If Not GetNodeList(nodelist, p_tagName, p_useNameSpace, xmlDoc.DocumentElement.NamespaceURI, p_parentNodes) Then Exit Sub

                'Read the specified node within the node list
                For Each node As XmlNode In nodelist
                    nodeValue = SelectSingleNode(node, p_singleNodePath, p_useNameSpace, nsmgr, p_attributeName)
                    If Not String.IsNullOrEmpty(nodeValue) Then p_childValues.Add(nodeValue)
                Next
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_singleNodePath), p_singleNodePath,
                                                 NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                 NameOfParam(Function() p_attributeName), p_attributeName,
                                                 NameOfParam(Function() p_parentNodes), p_parentNodes))
                CloseXML()
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' Gets a node list of results elements based on the parents node list provided. Also returns the namespace manager object.
        ''' </summary>
        ''' <param name="p_tagName">Name of the non-unique nodes to gather as a list.</param>
        ''' <param name="p_nsmgr">Namespace manager object to assign.</param>
        ''' <param name="p_parentNodes">List of unique parent nodes that lie above the non-unique node. Currently handles 3 levels deep.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetResultsNodeList(ByVal p_tagName As String,
                                            ByRef p_nsmgr As XmlNamespaceManager,
                                            ByVal p_parentNodes As List(Of String)) As XmlNodeList
            Dim nodeList As XmlNodeList = Nothing

            Try
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                'Get list of results nodes to read
                If (p_parentNodes.Count > 0 AndAlso
                    xmlDoc.Item(p_parentNodes(0)) IsNot Nothing) Then
                    If p_parentNodes.Count > 1 Then
                        If xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)) IsNot Nothing Then
                            If (p_parentNodes.Count > 2 AndAlso
                                xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).Item(p_parentNodes(2)) IsNot Nothing AndAlso
                                xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).Item(p_parentNodes(2)).GetElementsByTagName(p_tagName) IsNot Nothing) Then

                                nodeList = xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).Item(p_parentNodes(2)).GetElementsByTagName(p_tagName)
                            Else
                                If xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName) IsNot Nothing Then
                                    nodeList = xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName)
                                End If
                            End If
                        End If
                    Else
                        If xmlDoc.Item(p_parentNodes(0)).GetElementsByTagName(p_tagName) IsNot Nothing Then
                            nodeList = xmlDoc.Item(p_parentNodes(0)).GetElementsByTagName(p_tagName)
                        End If
                    End If
                End If

                p_nsmgr = nsmgr
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_parentNodes), p_parentNodes))
            End Try

            Return nodeList
        End Function

        ''' <summary>
        ''' Gets a list of non-unique XML nodes based on a tag name.
        ''' </summary>
        ''' <param name="p_nodeList">List object to populate with a set of non-unique XML nodes based on tag name.</param>
        ''' <param name="p_tagName">Name of the repeating node element to query.</param>
        ''' <param name="p_useNameSpace">True: Xpath needs to use name spaces, and the nsmgr object will be used.</param>
        ''' <param name="p_nameSpaceURI">Namepsace URI used if the Xpath needs to use name spaces.</param>
        ''' <param name="p_parentNodes">Listed names of parent nodes for truncating the scope of where the list of nodes is gathered.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetNodeList(ByRef p_nodeList As XmlNodeList,
                                    ByVal p_tagName As String,
                                    ByVal p_useNameSpace As Boolean,
                                    Optional ByVal p_nameSpaceURI As String = "",
                                    Optional ByVal p_parentNodes As List(Of String) = Nothing) As Boolean
            Try
                If p_useNameSpace Then
                    If ((p_parentNodes Is Nothing OrElse
                         p_parentNodes.Count = 0) AndAlso
                        xmlDoc.GetElementsByTagName(p_tagName, p_nameSpaceURI) IsNot Nothing) Then

                        p_nodeList = xmlDoc.GetElementsByTagName(p_tagName, p_nameSpaceURI)
                        Return True
                    Else
                        If (xmlDoc.Item(p_parentNodes(0)) IsNot Nothing AndAlso
                            xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)) IsNot Nothing AndAlso
                            xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName) IsNot Nothing) Then

                            p_nodeList = xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName)
                            Return True
                        End If
                    End If
                Else
                    If ((p_parentNodes Is Nothing OrElse
                         p_parentNodes.Count = 0) AndAlso
                         xmlDoc.GetElementsByTagName(p_tagName) IsNot Nothing) Then

                        p_nodeList = xmlDoc.GetElementsByTagName(p_tagName)
                        Return True
                    ElseIf (xmlDoc.Item(p_parentNodes(0)) IsNot Nothing AndAlso
                            xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)) IsNot Nothing AndAlso
                            xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName) IsNot Nothing) Then

                        p_nodeList = xmlDoc.Item(p_parentNodes(0)).Item(p_parentNodes(1)).GetElementsByTagName(p_tagName)
                        Return True
                    End If
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                 NameOfParam(Function() p_parentNodes), p_parentNodes,
                                                 NameOfParam(Function() p_nameSpaceURI), p_nameSpaceURI))
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Returns the text value of a single node or attribute, selected by the name of the node without an Xpath.
        ''' </summary>
        ''' <param name="p_node">Node object to select from.</param>
        ''' <param name="p_singleNodePath">Path to the child node within the repeating node element.</param>
        ''' <param name="p_useNameSpace">True: Xpath needs to use name spaces, and the nsmgr object will be used.</param>
        ''' <param name="p_nsmgr">Namespace manager object.</param>
        ''' <param name="p_attributeName">Attribute to populate to the list, if supplied.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SelectSingleNode(ByVal p_node As XmlNode,
                                        ByVal p_singleNodePath As String,
                                        ByVal p_useNameSpace As Boolean,
                                        Optional p_nsmgr As XmlNamespaceManager = Nothing,
                                        Optional ByVal p_attributeName As String = "") As String
            Dim nodeValue As String = ""

            Try
                If p_useNameSpace Then
                    If p_node.SelectSingleNode(p_singleNodePath, p_nsmgr) IsNot Nothing Then
                        If (Not String.IsNullOrEmpty(p_attributeName) AndAlso
                            p_node.SelectSingleNode(p_singleNodePath, p_nsmgr).Attributes.GetNamedItem(p_attributeName) IsNot Nothing) Then

                            nodeValue = p_node.SelectSingleNode(p_singleNodePath, p_nsmgr).Attributes.GetNamedItem(p_attributeName).InnerText
                        Else                                    'Get node value
                            nodeValue = p_node.SelectSingleNode(p_singleNodePath, p_nsmgr).InnerText
                        End If
                    End If
                ElseIf p_node.SelectSingleNode(p_singleNodePath) IsNot Nothing Then
                    If (Not String.IsNullOrEmpty(p_attributeName) AndAlso
                          p_node.SelectSingleNode(p_singleNodePath).Attributes.GetNamedItem(p_attributeName) IsNot Nothing) Then

                        nodeValue = p_node.SelectSingleNode(p_singleNodePath).Attributes.GetNamedItem(p_attributeName).InnerText
                    Else                                    'Get node value
                        nodeValue = p_node.SelectSingleNode(p_singleNodePath).InnerText
                    End If
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                NameOfParam(Function() p_node), p_node,
                                                NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                NameOfParam(Function() p_singleNodePath), p_singleNodePath,
                                                NameOfParam(Function() p_nsmgr), p_nsmgr,
                                                NameOfParam(Function() p_attributeName), p_attributeName))
            End Try

            Return nodeValue
        End Function

        'Not used VVVVVVV
        ''' <summary>
        ''' Check if the confirmation node exists, if specifed. If so, the function returns True.
        ''' </summary>
        ''' <param name="p_confirmationNode">Name of the confirmation node to look for in the node list.</param>
        ''' <param name="p_useNameSpace">If true, the Xpath needs to use name spaces, and the nsmgr object will be used.</param>
        ''' <param name="p_nsmgr">Namespace manager object.</param>
        ''' <param name="p_nodes">List of XML nodes to check for confirmation.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ConfirmationNodeExists(ByVal p_confirmationNode As String,
                                                ByVal p_useNameSpace As Boolean,
                                                ByVal p_nsmgr As XmlNamespaceManager,
                                                ByVal p_nodes As XmlNodeList) As Boolean
            Try
                If Not String.IsNullOrEmpty(p_confirmationNode) Then
                    If p_useNameSpace Then
                        For Each node As XmlNode In p_nodes
                            If node.SelectSingleNode(p_confirmationNode, p_nsmgr) IsNot Nothing Then Return True
                        Next
                    Else
                        For Each node As XmlElement In p_nodes
                            If node.SelectSingleNode(p_confirmationNode) IsNot Nothing Then Return True
                        Next
                    End If
                    Return False
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                    NameOfParam(Function() p_confirmationNode), p_confirmationNode,
                                                    NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                    NameOfParam(Function() p_nsmgr), p_nsmgr,
                                                    NameOfParam(Function() p_nodes), p_nodes))
            End Try

            Return True
        End Function

        'Not used ^^^^^^

        ''' <summary>
        ''' Updates a particular node value or attribute value that is paired with a specified node value.
        ''' </summary>
        ''' <param name="p_tagName">Name of the repeating node element to query.</param>
        ''' <param name="p_lookupNodePath">Path to the child node within the repeating node element to check.</param>
        ''' <param name="p_writeNodePath">Path to the child node within the repeating node element to write to if the check matches.</param>
        ''' <param name="p_nodeValueLookup">String that is contained within the value of the node that is checked, such as the identifying tag.</param>
        ''' <param name="p_nodeValueWrite">String to write to the node value of the corresponding node if a check matches.</param>
        ''' <param name="p_useNameSpace">True: Namespace element is used in the paths and must be supplied with the input paths.</param>
        ''' <param name="p_attributeName">Attribute to write to, if supplied.</param>
        ''' <remarks></remarks>
        Public Sub UpdateObjectByTag(ByVal p_tagName As String,
                                    ByVal p_lookupNodePath As String,
                                    ByVal p_writeNodePath As String,
                                    ByVal p_nodeValueLookup As String,
                                    ByVal p_nodeValueWrite As String,
                                    Optional ByVal p_useNameSpace As Boolean = False,
                                    Optional ByVal p_attributeName As String = "")
            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                If p_useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                Dim nodelist As XmlNodeList
                If p_useNameSpace Then
                    If xmlDoc.GetElementsByTagName(p_tagName, xmlDoc.DocumentElement.NamespaceURI) IsNot Nothing Then
                        nodelist = xmlDoc.GetElementsByTagName(p_tagName, xmlDoc.DocumentElement.NamespaceURI)
                    Else
                        Exit Sub
                    End If
                Else
                    If xmlDoc.GetElementsByTagName(p_tagName) IsNot Nothing Then
                        nodelist = xmlDoc.GetElementsByTagName(p_tagName)
                    Else
                        Exit Sub
                    End If
                End If

                If p_useNameSpace Then
                    For Each node As XmlNode In nodelist
                        If node.SelectSingleNode(p_lookupNodePath, nsmgr) IsNot Nothing Then
                            If Not String.IsNullOrEmpty(p_attributeName) Then  'Get attribute value
                                If node.SelectSingleNode(p_lookupNodePath, nsmgr).Attributes.GetNamedItem(p_attributeName) IsNot Nothing Then
                                    If StringExistInName(node.SelectSingleNode(p_lookupNodePath, nsmgr).InnerText, p_nodeValueLookup) Then
                                        node.SelectSingleNode(p_writeNodePath, nsmgr).Attributes.GetNamedItem(p_attributeName).InnerText = p_nodeValueWrite
                                    End If
                                End If
                            Else                            'Get node value
                                If StringExistInName(node.SelectSingleNode(p_lookupNodePath, nsmgr).InnerText, p_nodeValueLookup) Then
                                    node.SelectSingleNode(p_writeNodePath, nsmgr).InnerText = p_nodeValueWrite
                                End If
                            End If
                        End If
                    Next
                Else
                    For Each node As XmlElement In nodelist
                        If node.SelectSingleNode(p_lookupNodePath) IsNot Nothing Then
                            If Not String.IsNullOrEmpty(p_attributeName) Then  'Get attribute value
                                If StringExistInName(node.SelectSingleNode(p_lookupNodePath).InnerText, p_nodeValueLookup) Then
                                    If AttributeExists(node, p_attributeName) Then node.SelectSingleNode(p_writeNodePath).Attributes.GetNamedItem(p_attributeName).InnerText = p_nodeValueWrite
                                End If
                            Else                            'Get node value
                                If StringExistInName(node.SelectSingleNode(p_lookupNodePath).InnerText, p_nodeValueLookup) Then
                                    node.SelectSingleNode(p_writeNodePath).InnerText = p_nodeValueWrite
                                End If
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_lookupNodePath), p_lookupNodePath,
                                                 NameOfParam(Function() p_writeNodePath), p_writeNodePath,
                                                 NameOfParam(Function() p_nodeValueLookup), p_nodeValueLookup,
                                                 NameOfParam(Function() p_nodeValueWrite), p_nodeValueWrite,
                                                 NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                 NameOfParam(Function() p_attributeName), p_attributeName))
                CloseXML()
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' Queries a list of non-unique nodes by a tag name within a tag name, and within them, queries a node specified by node path and adds the value to a supplied list. The sub-tag name can also target a single node within the main tag name.
        ''' </summary>
        ''' <param name="p_tagName">Name of the repeating node element to query.</param>
        ''' <param name="p_tagIdentifier">Name of a child node of the repeated tag element that distinguishes one tag selection from another.</param>
        ''' <param name="p_tagIdentifierValue">Content of a child node of the repeated tag element that distinguishes one tag selection from another.</param>
        ''' <param name="p_singleNodePath">Path to the child node within the repeating node element.</param>
        ''' <param name="p_childValues">List to be populated by the text values of multiple child elements.</param>
        ''' <param name="p_singleNodePathParent">Name of the single node to select within the tag name within which to search for the subTagName. 
        ''' If this is blank, the node value is retrieved. If not, the value retrieved is for the child of the node.</param>
        ''' <param name="p_useNameSpace">True: Namespace element is used in the paths and must be supplied with the input paths.</param>
        ''' <param name="p_singleNodePathChildren">If singleNodePathParent is used, this parameter can be used to select individual sub-nodes of the singleNodePathParent sub-node.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReadXmlObjectTextSubTag(ByVal p_tagName As String,
                                                ByVal p_tagIdentifier As String,
                                                ByVal p_tagIdentifierValue As String,
                                                ByVal p_singleNodePath As String,
                                                ByRef p_childValues As List(Of String),
                                                Optional ByVal p_singleNodePathParent As String = "",
                                                Optional p_useNameSpace As Boolean = False,
                                                Optional ByVal p_singleNodePathChildren As String = "") As String
            Dim nodeValue As String = ""
            Dim nodeList As XmlNodeList = Nothing
            Dim nodeSubList As XmlNodeList = Nothing

            p_childValues.Clear()  'Ensures that list starts out empty
            ReadXmlObjectTextSubTag = ""

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                If p_useNameSpace Then nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)

                'Get list of nodes to read
                If Not GetNodeList(nodeList, p_tagName, p_useNameSpace, xmlDoc.DocumentElement.NamespaceURI) Then Exit Function

                'Get sub-list of nodes to read
                For Each node As XmlNode In nodeList
                    If node.SelectSingleNode(p_tagIdentifier, nsmgr) IsNot Nothing Then
                        If node.SelectSingleNode(p_tagIdentifier, nsmgr).InnerText = p_tagIdentifierValue Then
                            If Not String.IsNullOrEmpty(p_singleNodePathParent) Then
                                If p_useNameSpace Then
                                    If node.SelectSingleNode(p_singleNodePathParent, nsmgr) IsNot Nothing Then
                                        nodeSubList = node.SelectSingleNode(p_singleNodePathParent, nsmgr).ChildNodes
                                    End If
                                Else
                                    If node.SelectSingleNode(p_singleNodePathParent) IsNot Nothing Then
                                        nodeSubList = node.SelectSingleNode(p_singleNodePathParent).ChildNodes
                                    End If
                                End If
                            Else
                                nodeSubList = node.ChildNodes
                            End If

                            ''Read the specified node within the node list
                            If nodeSubList IsNot Nothing Then
                                For Each subNode As XmlNode In nodeSubList
                                    nodeValue = ""
                                    If String.IsNullOrEmpty(p_singleNodePath) Then
                                        If Not subNode.NodeType = XmlNodeType.Comment Then
                                            If String.IsNullOrEmpty(p_singleNodePathChildren) Then
                                                nodeValue = subNode.InnerText
                                            Else
                                                If p_useNameSpace Then
                                                    If subNode.SelectSingleNode(p_singleNodePathChildren, nsmgr) IsNot Nothing Then
                                                        nodeValue = subNode.SelectSingleNode(p_singleNodePathChildren, nsmgr).InnerText
                                                    End If
                                                Else
                                                    If subNode.Item(p_singleNodePathChildren) IsNot Nothing Then
                                                        nodeValue = subNode.Item(p_singleNodePathChildren).InnerText
                                                    End If
                                                End If
                                            End If
                                        Else
                                            Continue For
                                        End If
                                    Else
                                        nodeValue = SelectSingleNode(subNode, p_singleNodePath, p_useNameSpace, nsmgr)
                                    End If
                                    If Not String.IsNullOrEmpty(nodeValue) Then p_childValues.Add(nodeValue)
                                Next
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_tagIdentifier), p_tagIdentifier,
                                                 NameOfParam(Function() p_tagIdentifierValue), p_tagIdentifierValue,
                                                 NameOfParam(Function() p_singleNodePath), p_singleNodePath,
                                                 NameOfParam(Function() p_useNameSpace), p_useNameSpace,
                                                 NameOfParam(Function() p_singleNodePathParent), p_singleNodePathParent,
                                                 NameOfParam(Function() p_singleNodePathChildren), p_singleNodePathChildren))
                CloseXML()
                Exit Function
            End Try

            ReadXmlObjectTextSubTag = ""
        End Function

        'Currently not used
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="p_tagName"></param>
        ''' <param name="p_subTagName"></param>
        ''' <param name="p_subSubTagName"></param>
        ''' <param name="p_singleNodePath"></param>
        ''' <param name="p_nodeValues"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReadXmlObjectTextSubSubTag(ByVal p_tagName As String,
                                                    ByVal p_subTagName As String,
                                                    ByVal p_subSubTagName As String,
                                                    ByVal p_singleNodePath As String,
                                                    ByRef p_nodeValues As List(Of String)) As String
            Dim nodeValue As String

            p_nodeValues.Clear()  'Ensures that list starts out empty
            ReadXmlObjectTextSubSubTag = ""

            Try
                Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName(p_tagName)
                Dim nodeSubList As XmlNodeList
                Dim nodeSubSubList As XmlNodeList

                For Each node As XmlNode In nodeList
                    nodeSubList = node.ChildNodes
                    For Each subNode As XmlNode In nodeSubList
                        nodeSubSubList = subNode.ChildNodes
                        For Each subSubNode As XmlNode In nodeSubSubList
                            nodeValue = node.SelectSingleNode(p_singleNodePath).InnerText
                            p_nodeValues.Add(nodeValue)
                        Next
                    Next
                Next

            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_tagName), p_tagName,
                                                 NameOfParam(Function() p_subTagName), p_subTagName,
                                                 NameOfParam(Function() p_subSubTagName), p_subSubTagName,
                                                 NameOfParam(Function() p_singleNodePath), p_singleNodePath))
                CloseXML()
                Exit Function
            End Try

            ReadXmlObjectTextSubSubTag = ""

        End Function

        ''' <summary>
        ''' Takes the node name of a collection of identically named child objects, and queries those children's child elements. 
        ''' If one element matches the search term, the function will set its value to the corresponding element specified.
        ''' </summary>
        ''' <param name="p_pathNode">Path to the lowest unique level in the XML.</param>
        ''' <param name="p_keyNodeValue">Value that the node queried should match.</param>
        ''' <param name="p_lookupNodeName">Name of the node to return a value from if a match is found.</param>
        ''' <returns>Text value of a node corresponding to the key node specified.</returns>
        ''' <remarks></remarks>
        Public Function ListObjectByKey(ByVal p_pathNode As String,
                                        ByVal p_keyNodeValue As String,
                                        ByVal p_lookupNodeName As String) As String
            Dim matchingListObject As Boolean

            matchingListObject = False

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                'Lookup node or attribute within XML file
                For Each nodeParent As XmlNode In myXMLNode

                    'Check each child node value to see if it matches the key node value
                    For Each nodeChild As XmlNode In nodeParent
                        If nodeChild.InnerText = p_keyNodeValue Then
                            matchingListObject = True                                        'Object key matches
                            Exit For
                        End If
                    Next

                    'If a match is found, get the value from the node to be looked up
                    If matchingListObject Then
                        ''For' loop begun again in case the key node was encountered after the desired lookup node
                        For Each nodeChild As XmlNode In nodeParent
                            If nodeChild.Name = p_lookupNodeName Then
                                ListObjectByKey = nodeChild.InnerText
                                Exit Function
                            End If
                        Next
                    End If
                Next
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_pathNode), p_pathNode,
                                                 NameOfParam(Function() p_keyNodeValue), p_keyNodeValue,
                                                 NameOfParam(Function() p_lookupNodeName), p_lookupNodeName))
            End Try

            ListObjectByKey = ""
        End Function
#End Region

#Region "Creating/Inserting/Deleting Nodes"
        '=== Component Function
        ''' <summary>
        ''' Creates a node of a given xml path, name, value and type. 
        ''' </summary>
        ''' <param name="p_pathNode">Absolute path to the parent node from which to read the object child nodes.</param>
        ''' <param name="p_nameNode">Name of the new node.</param>
        ''' <param name="p_valueNode">Value of the new node.</param>
        ''' <param name="p_typeNode">New node type.</param>
        ''' <param name="p_createMethod">Method of node creation, such as before/after.</param>
        ''' <remarks></remarks>
        Public Sub CreateNodeByPath(ByVal p_pathNode As String,
                                    ByVal p_nameNode As String,
                                    ByVal p_valueNode As String,
                                    ByVal p_typeNode As eXMLElementType,
                                    ByVal p_createMethod As eNodeCreate)
            Dim objXMLNode As XmlNode
            Dim objXMLAttr As XmlAttribute
            Dim myNameSpace As String

            Try
                'Create an XmlNamespaceManager for resolving namespaces.
                Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
                nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
                myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

                'Insert new nodes in XML w text
                myNameSpace = xmlDoc.DocumentElement.NamespaceURI  'Needed in order to prevent blank xmnls attribute from appearing

                If p_typeNode = eXMLElementType.Attribute Then
                    objXMLAttr = xmlDoc.CreateAttribute(p_nameNode)
                    objXMLAttr.Value = p_valueNode
                    myXMLNode.Attributes.Append(objXMLAttr)
                Else
                    objXMLNode = xmlDoc.CreateNode(XmlNodeType.Element, p_nameNode, myNameSpace)
                    objXMLNode.InnerText = p_valueNode

                    Select Case p_createMethod
                        Case eNodeCreate.child
                            myXMLNode.AppendChild(objXMLNode)

                        Case eNodeCreate.insertBefore
                            Dim myXMLNodeParent As XmlNode
                            Dim parentPathNode As String = FilterStringFromName(p_pathNode, "/n:" & GetSuffix(p_pathNode, ":"), True, False)

                            myXMLNodeParent = xmlRoot.SelectSingleNode(parentPathNode, nsmgr)

                            myXMLNodeParent.InsertBefore(objXMLNode, myXMLNode)

                        Case eNodeCreate.insertAfter
                            Dim myXMLNodeParent As XmlNode
                            Dim parentPathNode As String = FilterStringFromName(p_pathNode, "/n:" & GetSuffix(p_pathNode, ":"), True, False)

                            myXMLNodeParent = xmlRoot.SelectSingleNode(parentPathNode, nsmgr)

                            myXMLNodeParent.InsertAfter(objXMLNode, myXMLNode)
                    End Select
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_pathNode), p_pathNode,
                                                 NameOfParam(Function() p_nameNode), p_nameNode,
                                                 NameOfParam(Function() p_valueNode), p_valueNode,
                                                 NameOfParam(Function() p_typeNode), p_typeNode,
                                                 NameOfParam(Function() p_createMethod), p_createMethod))
                Exit Sub
            End Try
        End Sub

        ''' <summary>
        ''' Deletes a node of a given xml path. If a path node attribute is specified, the attribute is removed.
        ''' </summary>
        ''' <param name="p_pathNode">Absolute path to the node to be removed, or that contains the attribute to be removed.</param>
        ''' <param name="p_pathNodeAttrib">Name of the attribute to be removed.</param>
        ''' <remarks></remarks>
        Public Sub DeleteNodeByPath(ByVal p_pathNode As String,
                                    Optional ByVal p_pathNodeAttrib As String = "")
            'Create an XmlNamespaceManager for resolving namespaces.
            Dim nsmgr As New XmlNamespaceManager(xmlDoc.NameTable)
            nsmgr.AddNamespace("n", xmlDoc.DocumentElement.NamespaceURI)
            myXMLNode = xmlRoot.SelectSingleNode(p_pathNode, nsmgr)

            If String.IsNullOrEmpty(p_pathNodeAttrib) Then
                'If node has children, clean out contents
                If ChildCount(p_pathNode) > 0 Then
                    myXMLNode.RemoveAll()
                End If

                'Delete node
                myXMLNode.ParentNode.RemoveChild(myXMLNode)
            Else
                p_pathNodeAttrib = GetSuffix(p_pathNodeAttrib, "@")         'Remove parent node name if appended as 'node@attribute' form

                myXMLNode.Attributes.RemoveNamedItem(p_pathNodeAttrib)
            End If
        End Sub

        '=== Direct call to an XML File
        ''' <summary>
        ''' Adds new nodes to the specified XML file.
        ''' </summary>
        ''' <param name="p_path">Path to and including the XML file name.</param>
        ''' <param name="p_pathNode">Absolute path to the parent node to add the current node to.</param>
        ''' <param name="p_nameNode">Name of the new node.</param>
        ''' <param name="p_valueNode">Value of the new node.</param>
        ''' <param name="p_typeNode">New node type.</param>
        ''' <param name="p_createMethod">Method of node creation, such as before/after.</param>
        ''' <remarks></remarks>
        Public Sub CreateNodeInXMLFile(ByVal p_path As String,
                                        ByVal p_pathNode As String,
                                        ByVal p_nameNode As String,
                                        ByVal p_valueNode As String,
                                        ByVal p_typeNode As eXMLElementType,
                                        ByVal p_createMethod As eNodeCreate)
            Try
                If InitializeXML(p_path) Then
                    CreateNodeByPath(p_pathNode, p_nameNode, p_valueNode, p_typeNode, p_createMethod)   'Create nodes
                    SaveXML(p_path)
                    CloseXML()
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_path), p_path,
                                                 NameOfParam(Function() p_pathNode), p_pathNode,
                                                 NameOfParam(Function() p_nameNode), p_nameNode,
                                                 NameOfParam(Function() p_valueNode), p_valueNode,
                                                 NameOfParam(Function() p_typeNode), p_typeNode,
                                                 NameOfParam(Function() p_createMethod), p_createMethod))
            End Try
        End Sub

        ''' <summary>
        ''' Deletes the node of a specified path from a specified XML file.
        ''' </summary>
        ''' <param name="p_path">Path to the XML file.</param>
        ''' <param name="p_deleteNodePath">Path to the node to delete.</param>
        ''' <param name="p_deleteNodeAttribute">Attribute name to delete. 
        ''' If specified, only the attribute will be deleted.</param>
        ''' <remarks></remarks>
        Public Sub DeleteNodeInXMLFile(ByVal p_path As String,
                                        ByVal p_deleteNodePath As String,
                                        Optional ByVal p_deleteNodeAttribute As String = "")
            Try
                If InitializeXML(p_path) Then
                    DeleteNodeByPath(p_deleteNodePath, p_deleteNodeAttribute)   'Create nodes
                    SaveXML(p_path)
                    CloseXML()
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex,
                                                 NameOfParam(Function() p_path), p_path,
                                                 NameOfParam(Function() p_deleteNodePath), p_deleteNodePath,
                                                 NameOfParam(Function() p_deleteNodeAttribute), p_deleteNodeAttribute))
            End Try
        End Sub
#End Region

#Region "Mirror XML Functions Into Memory"
        '=== Creating the object in memory drawn from the XML file
        ''' <summary>
        ''' Creates a list of all nodes and attributes in an XML document, with various properties recorded. This sub sets up the xml file then calls another sub.
        ''' </summary>
        ''' <param name="p_xmlFilePath">Path to the XML file to be used.</param>
        ''' <remarks></remarks>
        Public Function MirrorXMLElementsAll(ByVal p_xmlFilePath As String) As cXMLObject
            Dim childLevel As Integer = 1
            Dim nodePath As String = "//n:"
            Dim xmlFileNode As New cXMLNode

            xmlFile = New cXMLObject

            Try
                If InitializeXML(p_xmlFilePath) Then
                    xmlFile.fileName = GetPathFileName(p_xmlFilePath)

                    nodePath = nodePath & xmlRoot.Name

                    'Create root node
                    With xmlFileNode
                        .filePath = p_xmlFilePath
                        .name = xmlRoot.Name
                        .xmlPath = nodePath
                        .level = 0
                        .indexFlat = 0
                        .type = eXMLElementType.Header
                    End With

                    xmlFile.xmlMirror.Add(xmlFileNode)

                    'Set XML path and create attributes and child nodes
                    Mirror_ChildNodes(p_xmlFilePath, childLevel, xmlRoot.ChildNodes, 0, nodePath, xmlFileNode)

                    CloseXML()
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_xmlFilePath), p_xmlFilePath))
            Finally
                MirrorXMLElementsAll = xmlFile
            End Try
        End Function

        ''' <summary>
        ''' Creates a list of all child nodes in an XML document, sorted by node vs. attribute type, and header vs. value node. 
        ''' Hierarchy levels are recorded, as well as value node index and header info.
        ''' </summary>
        ''' <param name="p_path">Path to the XML file to be used.</param>
        ''' <param name="p_childLevel">Level in the nodal hierarchy. 
        ''' Level 1 is the first level as the root node is at level 0.</param>
        ''' <param name="p_childNodes">Node object from the XML file that includes all child nodes.</param>
        ''' <param name="p_counter">Current value node index in file.</param>
        ''' <param name="p_parentNode">Parent node object, for attaching newly created child classes to.</param>
        ''' <param name="p_pathNode">Absolute XML path within the XML file.</param>
        ''' <remarks></remarks>
        Public Sub Mirror_ChildNodes(ByVal p_path As String,
                                    ByRef p_childLevel As Integer,
                                    ByVal p_childNodes As XmlNodeList,
                                    ByRef p_counter As Integer,
                                    ByRef p_pathNode As String,
                                    Optional ByVal p_parentNode As cXMLNode = Nothing)
            Dim childCount As Integer
            Dim nodeHeader As String
            Dim indexFlat As Integer
            Dim lastXMLPathNode As String

            lastXMLPathNode = p_pathNode

            'Gather root node attributes, if they exist
            If (xmlRoot.Attributes IsNot Nothing And Not p_childLevel > 1) Then
                For Each xmlAttr As XmlAttribute In xmlRoot.Attributes
                    p_counter = p_counter + 1
                    indexFlat = p_counter

                    CreateMirrorNode(p_path, p_childLevel, indexFlat, p_parentNode, p_pathNode, eXMLElementType.Attribute, xmlAttr)
                Next
            End If

            For Each xmlNodeItem As XmlNode In p_childNodes
                childCount = 0
                Err.Clear()
                On Error Resume Next

                'Counts children based on 3 cases
                childCount = XMLChildCount(xmlNodeItem)

                If childCount > 0 Then      'Header Node
                    'Counter is not advanced for header, but header index should correspond to the next value node encountered
                    indexFlat = p_counter + 1

                    CreateMirrorNode(p_path, p_childLevel, indexFlat, p_parentNode, p_pathNode, eXMLElementType.Header, , xmlNodeItem)

                    'Update XML Path variable & write node path property
                    p_pathNode = p_pathNode & "/n:" & myXMLFileNode.name

                    'Advance child level and call function again
                    p_childLevel = p_childLevel + 1
                    Call Mirror_ChildNodes(p_path, p_childLevel, xmlNodeItem.ChildNodes, p_counter, p_pathNode, myXMLFileNode)
                Else                        'Value Node or attribute
                    p_counter = p_counter + 1
                    indexFlat = p_counter

                    CreateMirrorNode(p_path, p_childLevel, indexFlat, p_parentNode, p_pathNode, eXMLElementType.Node, , xmlNodeItem)

                    'Gather attributes, if they exist
                    If xmlNodeItem.Attributes IsNot Nothing Then
                        For Each xmlAttr As XmlAttribute In xmlNodeItem.Attributes
                            p_counter = p_counter + 1
                            indexFlat = p_counter

                            CreateMirrorNode(p_path, p_childLevel, indexFlat, p_parentNode, p_pathNode, eXMLElementType.Attribute, xmlAttr, xmlNodeItem)
                        Next
                    End If
                End If

                'Reverse XML path variable back
                p_pathNode = lastXMLPathNode
            Next

            'Reverse child level back
            p_childLevel = p_childLevel - 1

            'Establish column span of header at prior child level
            For Each myHeader As cXMLNode In p_parentNode.xmlChildren
                If IsMatchingHeaderWithoutChildren(myHeader, p_childLevel) Then
                    myHeader.valueNodeSpan = p_counter - myHeader.indexFlat + 1
                    Exit For
                End If
            Next
        End Sub

        ''' <summary>
        ''' Returns 'True' if the node object provided is a header, of the specified child level, with no children.
        ''' </summary>
        ''' <param name="p_xmlNode">Node object to check.</param>
        ''' <param name="p_childLevel">The number of levels down in the parent-child hierachy the node is.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsMatchingHeaderWithoutChildren(ByVal p_xmlNode As cXMLNode,
                                                        ByVal p_childLevel As Integer) As Boolean

            If (p_xmlNode.type = eXMLElementType.Header AndAlso
                p_xmlNode.level = p_childLevel AndAlso
                p_xmlNode.valueNodeSpan = 0) Then

                Return True
            Else
                Return False
            End If
        End Function


        ''' <summary>
        ''' Creates the individual node property to be mirrored in memory.
        ''' </summary>
        ''' <param name="p_nodePath">Path to the XML file to be used.</param>
        ''' <param name="p_childLevel">Level in the nodal hierarchy. Level 1 is the first level as the root node is at level 0.</param>
        ''' <param name="p_indexFlat">Number assigned to the node in the order that the node was encountered. Exception: The index is the same for the first node at two different child levels.</param>
        ''' <param name="p_parentNode">Parent node object, for attaching newly created child classes to.</param>
        ''' <param name="p_xmlPath">Absolute XML path within the XML file.</param>
        ''' <param name="p_elementType">Node type of attribute, header (node with no text value), or node (node with text value).</param>
        ''' <param name="p_xmlAttr">Optional: XML attribute node.</param>
        ''' <param name="p_xmlNodeItem">Optional: XML item node.</param>
        ''' <remarks></remarks>
        Public Sub CreateMirrorNode(ByVal p_nodePath As String,
                                    ByRef p_childLevel As Integer,
                                    ByRef p_indexFlat As Integer,
                                    ByRef p_parentNode As cXMLNode,
                                    ByRef p_xmlPath As String,
                                    ByVal p_elementType As eXMLElementType,
                                    Optional ByVal p_xmlAttr As XmlAttribute = Nothing,
                                    Optional ByVal p_xmlNodeItem As XmlNode = Nothing)

            Dim tempXMLNodeName As String

            'Create new node class
            myXMLFileNode = New cXMLNode

            'Set node names & XML Paths
            With myXMLFileNode
                If p_xmlNodeItem Is Nothing Then                              'Root Node Attributes
                    .name = p_xmlAttr.Name
                    .xmlPath = p_xmlPath
                Else
                    If p_xmlNodeItem.Name = "#text" Then                      'Case 1: Value node with text (Value nodes only)
                        tempXMLNodeName = p_xmlNodeItem.ParentNode.Name
                    Else                                                    'Case 2: Value node with no text
                        tempXMLNodeName = p_xmlNodeItem.Name
                    End If

                    'Set Node Name
                    If p_xmlAttr Is Nothing Then                              'Node
                        .name = tempXMLNodeName
                    Else                                                    'Other Node Attributes
                        .name = p_xmlNodeItem.Name & "@" & p_xmlAttr.Name
                    End If

                    .xmlPath = p_xmlPath & "/n:" & tempXMLNodeName
                End If

                'Populate Common Properties
                .indexFlat = p_indexFlat
                .type = p_elementType
                .filePath = p_nodePath
                .level = p_childLevel

                'Populate node properties
                Select Case p_elementType
                    Case eXMLElementType.Attribute : .value = p_xmlAttr.InnerText
                    Case eXMLElementType.Header 'No action needed
                    Case eXMLElementType.Node : .value = p_xmlNodeItem.InnerText
                End Select
            End With

            'Add node class to parent classes
            p_parentNode.xmlChildren.Add(myXMLFileNode)
        End Sub

        '=== Manipulating the XML file based on the object in memory.
        ''' <summary>
        ''' Saves the XML mirror class back over the XML file.
        ''' </summary>
        ''' <param name="p_path">Path to and including the XML file name.</param>
        ''' <param name="p_XMLNodes">Branch of 'nodes' recorded in the XML mirror class.</param>
        ''' <remarks></remarks>
        Public Function UpdateXMLFile(ByVal p_path As String,
                                        ByRef p_XMLNodes As List(Of cXMLNode)) As Boolean
            Dim ChildLevel As Integer = 1

            Try
                If InitializeXML(p_path) Then
                    UpdateXMLFileNodes(ChildLevel, xmlRoot.ChildNodes, -1, p_XMLNodes)

                    SaveXML(p_path)
                    CloseXML()
                    Return True
                End If
            Catch ex As Exception
                RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() p_path), p_path))
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Overwrites a given XML node value with the node value from the mirror class, if the class property is set to be recorded. This saves an edited XML node-by-node as specified.
        ''' </summary>
        ''' <param name="p_childLevel">Level in the nodal hierarchy. 
        ''' Level 1 is the first level as the root node is at level 0.</param>
        ''' <param name="p_childNodes">Node object from the XML file that includes all child nodes.</param>
        ''' <param name="p_counter">Current 0-based value node index in file.</param>
        ''' <param name="p_XMLNodes">Branch of 'nodes' recorded in the XML mirror class</param>
        ''' <remarks>This assumes that the class that is being saved to the XML has the exact same structure of the XML. 
        ''' Will not work if any nodes have been added or removed in either object</remarks>
        Public Sub UpdateXMLFileNodes(ByRef p_childLevel As Integer,
                                        ByVal p_childNodes As XmlNodeList,
                                        ByRef p_counter As Integer,
                                        ByRef p_XMLNodes As List(Of cXMLNode),
                                        Optional ByVal p_counterSecondary As Integer = -2)
            Dim childCount As Integer = 0
            Dim myMirrorNode As cXMLNode
            Dim nameNode As String
            Dim useMyTempCount As Boolean = False

            'Update root node attributes, if they exist
            If (xmlRoot.Attributes IsNot Nothing And Not p_childLevel > 1) Then

                For Each xmlAttr As XmlAttribute In xmlRoot.Attributes
                    p_counter = p_counter + 1
                    myMirrorNode = p_XMLNodes(p_counter)

                    'Writes changes and updates class
                    If myMirrorNode.saveChanges And xmlAttr.Name = myMirrorNode.name Then
                        xmlAttr.InnerText = myMirrorNode.value

                        'Update node status in class
                        myMirrorNode.saveChanges = False
                        myMirrorNode.valueChanged = False
                    End If
                Next
            End If

            'Updates regular nodes and any subsequent attributes
            For Each xmlNodeItem As XmlNode In p_childNodes
                'Increments node count
                'If node is child node, counter needs to be reset, since it needs to be in sync with the child branch passed into the function
                If Not p_counterSecondary = -2 Then useMyTempCount = True
                If useMyTempCount Then
                    p_counterSecondary = p_counterSecondary + 1
                    myMirrorNode = p_XMLNodes(p_counterSecondary)
                Else
                    p_counter = p_counter + 1
                    myMirrorNode = p_XMLNodes(p_counter)
                End If

                childCount = 0
                Err.Clear()
                On Error Resume Next

                'Counts children based on 3 cases
                childCount = XMLChildCount(xmlNodeItem)

                If childCount > 0 Then
                    'Counter is not advanced for header, but header index should correspond to the next value node encountered
                    'Advance child level and call function again
                    p_childLevel = p_childLevel + 1
                    UpdateXMLFileNodes(p_childLevel, xmlNodeItem.ChildNodes, p_counter, myMirrorNode.xmlChildren, -1)
                Else
                    'Check node data
                    If xmlNodeItem.Name = "#text" Then                      'Case 1: Value node with text
                        nameNode = xmlNodeItem.ParentNode.Name
                    Else                                                    'Case 2: Value node with no text
                        nameNode = xmlNodeItem.Name
                    End If

                    'Writes changes and updates class
                    If (myMirrorNode.saveChanges AndAlso
                        nameNode = myMirrorNode.name AndAlso
                        myMirrorNode.level = p_childLevel) Then

                        xmlNodeItem.InnerText = myMirrorNode.value

                        'Update node status in class
                        myMirrorNode.saveChanges = False
                        myMirrorNode.valueChanged = False
                    End If

                    'Gather attributes, if they exist
                    If xmlNodeItem.Attributes IsNot Nothing Then
                        For Each xmlAttr As XmlAttribute In xmlNodeItem.Attributes
                            'If node is child node, counter needs to be reset, since it needs to be in sync with the child branch passed into the function
                            If useMyTempCount Then
                                p_counterSecondary = p_counterSecondary + 1
                                myMirrorNode = p_XMLNodes(p_counterSecondary)
                            Else
                                p_counter = p_counter + 1
                                myMirrorNode = p_XMLNodes(p_counter)
                            End If

                            nameNode = GetSuffix(myMirrorNode.name, "@")

                            'Writes changes and updates class
                            If (myMirrorNode.saveChanges AndAlso
                                xmlAttr.Name = nameNode AndAlso
                                myMirrorNode.level = p_childLevel) Then

                                xmlAttr.InnerText = myMirrorNode.value

                                'Update node status in class
                                myMirrorNode.saveChanges = False
                                myMirrorNode.valueChanged = False
                            End If
                        Next
                    End If
                End If
            Next

            'Reverse child level back
            p_childLevel = p_childLevel - 1
        End Sub
#End Region

    End Class


End Namespace

