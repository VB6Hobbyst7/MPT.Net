Option Explicit On
Module XML_WriteFromExcel
    ' '' ===== UNDER CONSTRUCTION
    ''
    ''Sub Write_XML(value As String)
    ''    Dim objDom As DOMDocument
    ''
    ''    Dim Filename As String
    ''    Dim SName As String
    ''    Dim nName As String
    ''    Dim nValue As String
    ''    Dim cell As String
    ''    Dim i As Integer
    ''
    ''    Path = "C:\Users\Mark\Documents\Projects\Work - CSI\Remote Work - Offline\Overall Suite Development"
    ''    Filename = "ExcelToXML.xml"
    ''    SName = "XML_Write"
    ''
    ''    Set objDom = New DOMDocument
    ''
    ''    If value = "xml" Then
    ''        Call Write_XML_Start
    ''    ElseIf value = "R" Then
    ''        Call Write_XML_RootNode
    ''    Else
    ''        Call Write_XML_Element
    ''    End If
    ''
    ''    '~~> Saves XML data to a file
    ''    objDom.Save (Location & "\" & Filename)
    ''End Sub
    ' '' ===== UNDER CONSTRUCTION
    ''Sub Write_XML_Start()
    ''    Dim objXMLNode As IXMLDOMNode
    ''
    ''    '~~> Set reference to Microsoft XML version X.X with encoding format
    ''    Set objXMLNode = objDom.createProcessingInstruction("xml", "version='1.0' encoding='UTF-8'")
    ''    objDom.appendChild objXMLNode
    ''
    ''End Sub
    ' '' ===== UNDER CONSTRUCTION
    ''Sub Write_XML_RootNode(value As String)
    ''    Dim objXMLRootelement As IXMLDOMElement
    ''    Dim nName As String
    ''
    ''    nName = Worksheets(SName).Range(exAddress).Offset(0, 1).value
    ''
    ''    '~~> Creates root element Node
    ''    Set objXMLRootelement = objDom.createElement(nName)
    ''    objDom.appendChild objXMLRootelement
    ''
    ''
    ''    objXMLelementParent = objXMLRootelement
    ''
    ''End Sub
    ' '' ===== UNDER CONSTRUCTION
    ''Sub Write_XML_Element(value As String)
    ''
    ''    Set objXMLelementChild = objDom.createElement(nName)
    ''    objXMLelementParent.appendChild objXMLelementChild
    ''
    ''    objXMLelementParent = objXMLelementChild
    ''
    ''End Sub
    ' '' ===== UNDER CONSTRUCTION
    ''Sub Write_XML_Attribute()
    ''    Dim objXMLattr As IXMLDOMAttribute
    ''
    ''    '~~> Creates Attribute Nodes to the Root Element and set value
    ''     Set objXMLattr = objDom.createAttribute(nName)
    ''     objXMLattr.NodeValue = nValue
    ''     objXMLelementParent.setAttributeNode objXMLattr
    ''
    ''End Sub
    ' '' ===== UNDER CONSTRUCTION
    ''Sub Write_XML_Text()
    ''    Dim objXMLtext As IXMLDOMText
    ''
    ''    Set objXMLtext = objDom.createTextNode(nName)
    ''    objXMLelementParent.appendChild objXMLtext
    ''
    ''End Sub


    'Sub Write_XML2()
    '    Dim objDom As Xml.XmlDocument
    '    Dim objXMLNode As Xml.XmlNode
    '    Dim objXMLRootelement As Xml.XmlElement
    '    Dim objXMLelement0 As Xml.XmlElement
    '    Dim objXMLelement1 As Xml.XmlElement
    '    Dim objXMLelement2 As Xml.XmlElement
    '    Dim objXMLelement3 As Xml.XmlElement
    '    Dim objXMLelement4 As Xml.XmlElement
    '    Dim objXMLelement5 As Xml.XmlElement
    '    Dim objXMLelement6 As Xml.XmlElement
    '    Dim objXMLelement7 As Xml.XmlElement
    '    Dim objXMLelement8 As Xml.XmlElement
    '    Dim objXMLattr As Xml.XmlAttribute
    '    Dim objXMLtext As Xml.XmlText
    '    Dim Location As String
    '    Dim fileName As String
    '    Dim SName As String
    '    Dim nName As String
    '    Dim nValue As String
    '    Dim cell As String
    '    Dim i As Integer

    '    Location = "C:\Users\Mark\Documents\Projects\Work - CSI\Remote Work - Offline\Overall Suite Development"
    '    fileName = "ExcelToXML.xml"
    '    SName = "XML_Write"

    '    objDom = New Xml.XmlDocument

    '    '~~> Set reference to Microsoft XML version X.X with encoding format
    '    objXMLNode = objDom.createProcessingInstruction("xml", "version='1.0' encoding='UTF-8'")
    '    objDom.appendChild objXMLNode

    '    '~~> Creates root element Node
    '    objXMLRootelement = objDom.createElement("model")
    '    objDom.appendChild objXMLRootelement

    '    For i = 1 To 8
    '        Select Case i
    '            Case 1
    '                nName = "xml_schema_version"
    '                cell = "D3"
    '            Case 2
    '                nName = "is_public"
    '                cell = "D4"
    '            Case 3
    '                nName = "is_bug"
    '                cell = "D5"
    '            Case 4
    '                nName = "primary_model_xml_file_path"
    '                cell = "D6"
    '            Case 5
    '                nName = "status"
    '                cell = "D7"
    '            Case 6
    '                nName = "xmlns"
    '                cell = "D8"
    '            Case 7
    '                nName = "xmlns:xsi"
    '                cell = "D9"
    '            Case 8
    '                nName = "xsi:schemaLocation"
    '                cell = "D10"
    '        End Select

    '        nValue = Worksheets(SName).Range(cell).value

    '        '~~> Creates Attribute Nodes to the Root Element and set value
    '        objXMLattr = objDom.createAttribute(nName)
    '        objXMLattr.NodeValue = nValue
    '        objXMLRootelement.setAttributeNode objXMLattr
    '    Next i

    '    '~~> Creates Text Node to the Element 1 level down
    '    objXMLelement0 = objDom.createElement("Element_Uno")
    '    objXMLRootelement.appendChild objXMLelement0

    '    objXMLelement1 = objDom.createElement("Element_Dos")
    '    objXMLelement0.appendChild objXMLelement1

    '    objXMLtext = objDom.createTextNode("testing")
    '    objXMLelement1.appendChild objXMLtext

    '    '~~> Creates Benchmark Node w/ attribute & text
    '    objXMLelement0 = objDom.createElement("Benchmark")
    '    objXMLRootelement.appendChild objXMLelement0

    '    objXMLattr = objDom.createAttribute("is_correct")
    '    objXMLattr.NodeValue = "yes"
    '    objXMLelement0.setAttributeNode objXMLattr

    '    objXMLtext = objDom.createTextNode("testing")
    '    objXMLelement0.appendChild objXMLtext

    '    '~~> Saves XML data to a file
    '    objDom.Save(Location & "\" & fileName)
    'End Sub


End Module
