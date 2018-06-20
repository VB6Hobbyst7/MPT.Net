Option Explicit On
Module XML_ReadToExcel

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

    Sub Read_XML_Elements_Text_All()
        'Creates a list of all nodes in an XML document
        'This sub sets up the xml file then calls another sub
        Dim xmlDoc As Xml.XmlDocument
        Dim xmlRoot As Xml.XmlElement

        Dim i As Integer

        Dim Location As String
        Dim fileName As String

        Location = "C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\models\ETABS Released Models - Wiki (renamed from Batch)\id0232"
        fileName = "model.xml"

        xmlDoc = New Xml.XmlDocument

        xmlDoc.Load(Location & "\" & fileName)

        xmlRoot = xmlDoc.DocumentElement

        i = 1

        Call List_ChildNodes(xmlRoot, i, "A", "B")

    End Sub

    Sub List_ChildNodes(xmlRoot, i, NameColumn, ValueColumn)
        'Creates a list of all child noedes in an XML document
        Dim L As Integer
        Dim xmlNode As Xml.XmlNode
        Dim SName As String

        SName = "XML_Read"

        For Each xmlNode In xmlRoot.ChildNodes
            L = 0
            Err.Clear()
            On Error Resume Next
            L = xmlNode.ChildNodes(0).ChildNodes.Count
            If L > 0 Then
                'UPdate -                 Worksheets(SName).Cells(i, NameColumn) = xmlNode.Name
                i = i + 1
                Call List_ChildNodes(xmlNode, i, NameColumn, ValueColumn)
            Else
                'UPdate -                 Worksheets(SName).Cells(i, NameColumn) = xmlNode.Name
                'UPdate -                 Worksheets(SName).Cells(i, ValueColumn) = xmlNode.Value
                i = i + 1
            End If
        Next
    End Sub

    Sub Read_XML_Elements_Text_Specified()
        'Fetches values from all instances of the occurrence of a specified node name
        'This sub sets up the file and calls another sub to lookup the node
        Dim xmlDoc As Xml.XmlDocument
        Dim xmlRoot As Xml.XmlElement

        Dim i As Integer

        Dim Location As String
        Dim fileName As String
        Dim nameParent As String
        Dim nameNode As String

        Location = "C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\models\id0232"
        fileName = "model.xml"

        xmlDoc = New Xml.XmlDocument

        xmlDoc.Load(Location & "\" & fileName)

        xmlRoot = xmlDoc.DocumentElement

        i = 1
        '    nameParent = "keywords"
        '    nameNode = "keyword"
        nameParent = "benchmark"
        nameNode = "last_best"
        '    nameParent = "field"
        '    nameNode = "name"
        Call List_Keywords(nameParent, nameNode, xmlRoot, i, "A")

    End Sub

    Sub List_Keywords(nameParent, nameNode, xmlRoot, i, ValueColumn)
        'Fetches values from all instances of the occurrence of a specified node name
        Dim L As Integer
        Dim xmlNode As Xml.XmlNode
        Dim SName As String

        SName = "XML_Read"

        For Each xmlNode In xmlRoot.ChildNodes

            L = 0
            Err.Clear()
            On Error Resume Next
            L = xmlNode.ChildNodes(0).ChildNodes.Count
            If L > 0 Then
                If xmlNode.Name = nameParent Then
                    Call List_Keywords(nameNode, nameNode, xmlNode, i, ValueColumn)
                Else
                    Call List_Keywords(nameParent, nameNode, xmlNode, i, ValueColumn)
                End If
            Else
                If xmlNode.Name = nameParent Then
                    'Update -                     Worksheets(SName).Cells(i, ValueColumn) = xmlNode.Value
                    i = i + 1
                End If
            End If
        Next
    End Sub

    Sub Read_XML_byNodes()
        'Reads XML by specifying nodes by number
        Dim xmlDoc As Xml.XmlDocument
        Dim xmlRoot As Xml.XmlElement
        Dim xmlChildren As Xml.XmlNodeList

        Dim Location As String
        Dim fileName As String

        Location = "C:\MPT-CSI\Regression & Verification Suites\Overall Suite Development\models\ETABS Released Models - Wiki (renamed from Batch)\id0232"
        fileName = "model.xml"

        xmlDoc = New Xml.XmlDocument

        xmlDoc.Load(Location & "\" & fileName)

        xmlRoot = xmlDoc.DocumentElement
        xmlChildren = xmlRoot.ChildNodes(19).ChildNodes(3).ChildNodes
        MsgBox(xmlChildren(1).Name & ": " & xmlChildren(1).Value)

    End Sub
    Sub readXMLbyPath()
        'Reads a particular node text and attribute based on the path within the XML
        Dim xmlDoc As Xml.XmlDocument
        Dim xmlRoot As Xml.XmlElement

        Dim Location As String
        Dim fileName As String
        Dim pathNode As String
        Dim pathNodeAttrib As String

        Dim curNode As String
        Dim curNodeAttrib As String

        Location = "C:\MPT-CSI\Regression & Verification Suites\verification_demo_2013-12-01\regtest"
        fileName = "regtest.xml"

        xmlDoc = New Xml.XmlDocument
        xmlDoc.Load(Location & "\" & fileName)

        xmlRoot = xmlDoc.DocumentElement

        pathNode = "//regtest/general/models_database_directory/path"
        pathNodeAttrib = "type"

        curNode = xmlRoot.SelectSingleNode(pathNode).Value
        curNodeAttrib = xmlRoot.SelectSingleNode(pathNode).Attributes.GetNamedItem(pathNodeAttrib).Value

        MsgBox(curNode)
        MsgBox(curNodeAttrib)

    End Sub



End Module
