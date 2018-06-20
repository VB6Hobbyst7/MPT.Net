Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.String.Characters
Imports MPT.XML.ReaderWriter

Namespace NodeAdapter
    Public Class cReadWriteXML

#Region "Fields"
        Private _xmlReaderWriter As New cXmlReadWrite()

        Private _nsDivider As String

        Private _xmlNodeItem As cXMLNode
        Private _pathNode As String
        Private _attributeName As String
#End Region

#Region "Initialization"
        Public Sub New()
            InitializeData()
        End Sub

        Public Sub New(ByVal p_xmlNodeItem As cXMLNode)

            InitializeData()
            InitializeVariables(p_xmlNodeItem)

        End Sub

        Public Sub New(ByVal p_nodePath As String,
                        Optional ByVal p_nodeAttribute As String = "",
                        Optional ByVal p_readWriteType As eReadWriteConversion = eReadWriteConversion.readWriteOnly,
                        Optional ByVal p_readOnly As Boolean = False,
                        Optional ByVal p_defaultValue As String = "",
                        Optional ByVal p_subPathNodes As List(Of String) = Nothing)
            InitializeData()
            SetInstructions(p_nodePath, p_nodeAttribute, p_readWriteType, p_readOnly, p_defaultValue, p_subPathNodes)
        End Sub


        Private Sub InitializeData()
            _xmlNodeItem = New cXMLNode
            _nsDivider = "/n:"
        End Sub

        Private Sub InitializeVariables(ByVal p_xmlNodeItem As cXMLNode)
            _xmlNodeItem = p_xmlNodeItem
            _pathNode = _xmlNodeItem.xmlPath

            If _xmlNodeItem.type = eXMLElementType.Attribute Then
                _attributeName = _xmlNodeItem.name
            Else
                _attributeName = ""
            End If
        End Sub
#End Region

#Region "Methods: Public"
        Public Overloads Sub ReadWriteAction(ByRef p_readWriteValue As String,
                                             Optional ByVal p_xmlNodeItem As cXMLNode = Nothing,
                                             Optional ByVal p_isReading As Boolean = True)
            If p_isReading Then
                ReadAction(p_readWriteValue, p_xmlNodeItem)
            Else
                WriteAction(p_readWriteValue, p_xmlNodeItem)
            End If
        End Sub
        Public Overloads Sub ReadWriteAction(ByRef p_readWriteValue As Boolean,
                                              Optional ByVal p_xmlNodeItem As cXMLNode = Nothing,
                                              Optional ByVal p_isReading As Boolean = True)
            If p_isReading Then
                ReadAction(p_readWriteValue, p_xmlNodeItem)
            Else
                WriteAction(p_readWriteValue, p_xmlNodeItem)
            End If
        End Sub
        Public Overloads Sub ReadWriteAction(ByRef p_readWriteValue As Integer,
                                                Optional ByVal p_xmlNodeItem As cXMLNode = Nothing,
                                                Optional ByVal p_isReading As Boolean = True)
            If p_isReading Then
                ReadAction(p_readWriteValue, p_xmlNodeItem)
            Else
                WriteAction(p_readWriteValue, p_xmlNodeItem)
            End If
        End Sub
        Public Overloads Sub ReadWriteAction(ByRef p_readWriteValue As ObservableCollection(Of String),
                                              Optional ByVal p_xmlNodeItem As cXMLNode = Nothing,
                                              Optional ByVal p_isReading As Boolean = True)
            If p_isReading Then
                ReadAction(p_readWriteValue, p_xmlNodeItem)
            Else
                WriteAction(p_readWriteValue, p_xmlNodeItem)
            End If
        End Sub
        Public Overloads Sub ReadWriteAction(Of TEnum)(ByRef p_readWriteValue As TEnum,
                                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing,
                                                        Optional ByVal p_isReading As Boolean = True)
            If p_isReading Then
                ReadAction(p_readWriteValue, p_xmlNodeItem)
            Else
                WriteAction(p_readWriteValue, p_xmlNodeItem)
            End If
        End Sub

        Public Overloads Sub ReadAction(ByRef p_readValue As String,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.readWriteOnly : p_readValue = ReadFromSingleNode()
                Case eReadWriteConversion.convertPathStoredUnknown : p_readValue = ConvertFromPathStoredUnknown(xmlNodeItem.defaultValue)
                Case eReadWriteConversion.convertPathStoredRelative : p_readValue = ConvertFromPathStoredKnown()
            End Select
        End Sub
        Public Overloads Sub ReadAction(ByRef p_readValue As Boolean,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.convertBooleanYesNo : p_readValue = ConvertYesTrueBoolean(ReadFromSingleNode())
                Case eReadWriteConversion.convertBooleanTrueFalse : p_readValue = ConvertTrueTrueBoolean(ReadFromSingleNode())
            End Select
        End Sub
        Public Overloads Sub ReadAction(ByRef p_readValue As Integer,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If _xmlNodeItem.readWriteAction = eReadWriteConversion.convertInteger Then p_readValue = myCInt(ReadFromSingleNode())
        End Sub
        Public Overloads Sub ReadAction(Of TEnum)(ByRef p_readValue As TEnum,
                                                    Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If _xmlNodeItem.readWriteAction = eReadWriteConversion.convertToEnum Then p_readValue = ConvertStringToEnumByDescription(Of TEnum)(ReadFromSingleNode())
        End Sub
        Public Overloads Sub ReadAction(ByRef p_readValue As ObservableCollection(Of String),
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.convertObservableCollection : p_readValue = ConvertToObservableCollection()
                Case eReadWriteConversion.convertPathStoredRelativeFromList : p_readValue = ConvertFromPathListStoredKnown()
                Case eReadWriteConversion.convertObservableCollectionFromList : p_readValue = ConvertFromListToObservableCollection()
                Case eReadWriteConversion.convertObservableCollectionFromUniqueList : p_readValue = ConvertFromUniqueListToObservableCollection()
            End Select

            If p_readValue Is Nothing Then p_readValue = New ObservableCollection(Of String)
        End Sub


        Public Overloads Sub WriteAction(ByVal p_writeValue As String,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If xmlNodeItem.isReadOnly Then Exit Sub

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.readWriteOnly : WriteToSingleNode(p_writeValue)
                Case eReadWriteConversion.convertPathStoredUnknown : ConvertToPathStoredUnknown(p_writeValue)
                Case eReadWriteConversion.convertPathStoredRelative : ConvertToPathStoredKnown(p_writeValue)
            End Select
        End Sub
        Public Overloads Sub WriteAction(ByVal p_writeValue As Boolean,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If xmlNodeItem.isReadOnly Then Exit Sub

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.convertBooleanYesNo : WriteToSingleNode(ConvertYesTrueString(p_writeValue, eCapitalization.AllLower))
                Case eReadWriteConversion.convertBooleanTrueFalse : WriteToSingleNode(ConvertTrueTrueString(p_writeValue, eCapitalization.AllLower))
            End Select
        End Sub
        Public Overloads Sub WriteAction(ByVal p_writeValue As Integer,
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If _xmlNodeItem.readWriteAction = eReadWriteConversion.convertInteger Then WriteToSingleNode(CStr(p_writeValue))

        End Sub
        Public Overloads Sub WriteAction(Of TEnum)(ByVal p_writeValue As TEnum,
                                    Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)

            If _xmlNodeItem.readWriteAction = eReadWriteConversion.convertToEnum Then WriteToSingleNode(GetEnumDescription(p_writeValue))

        End Sub
        Public Overloads Sub WriteAction(ByVal p_writeValue As ObservableCollection(Of String),
                                        Optional ByVal p_xmlNodeItem As cXMLNode = Nothing)
            If (p_writeValue Is Nothing OrElse
                p_writeValue.Count = 0) Then Exit Sub

            Dim xmlNodeItem As cXMLNode = SetXMLNodeItem(p_xmlNodeItem)
            If xmlNodeItem.isReadOnly Then Exit Sub

            Select Case _xmlNodeItem.readWriteAction
                Case eReadWriteConversion.convertObservableCollection : ConvertFromObservableCollection(p_writeValue)
                Case eReadWriteConversion.convertPathStoredRelativeFromList : ConvertToPathListStoredKnown(p_writeValue)
                Case eReadWriteConversion.convertObservableCollectionFromList : ConvertToListFromObservableCollection(p_writeValue)
                Case eReadWriteConversion.convertObservableCollectionFromUniqueList : ConvertToUniqueListFromObservableCollection(p_writeValue)
            End Select
        End Sub

        Public Sub SetInstructions(ByVal p_nodePath As String,
                                      Optional ByVal p_nodeAttribute As String = "",
                                      Optional ByVal p_readWriteType As eReadWriteConversion = eReadWriteConversion.readWriteOnly,
                                      Optional ByVal p_readOnly As Boolean = False,
                                      Optional ByVal p_defaultValue As String = "",
                                      Optional ByVal p_subPathNodes As List(Of String) = Nothing)

            Dim xmlNode As New cXMLNode
            Dim nodeName As String

            If String.IsNullOrEmpty(p_nodeAttribute) Then
                nodeName = GetSuffix(p_nodePath, _nsDivider)
                xmlNode.type = eXMLElementType.Node
            Else
                nodeName = p_nodeAttribute
                xmlNode.type = eXMLElementType.Attribute
            End If

            With xmlNode
                .name = nodeName
                .xmlPath = p_nodePath
                .readWriteAction = p_readWriteType
                .isReadOnly = p_readOnly
                .defaultValue = p_defaultValue
            End With

            InitializeVariables(xmlNode)
        End Sub
#End Region

#Region "Methods: Private"

        Private Function SetXMLNodeItem(ByVal p_xmlNodeItem As cXMLNode) As cXMLNode
            If p_xmlNodeItem Is Nothing Then
                Return _xmlNodeItem
            Else
                InitializeVariables(p_xmlNodeItem)
                Return p_xmlNodeItem
            End If
        End Function

        Private Function ConvertToObservableCollection() As ObservableCollection(Of String)
            Dim nodesList As New ObservableCollection(Of String)

            For Each childNode As cXMLNode In _xmlNodeItem.xmlChildren
                Dim pathAttribute As String = ""

                Select Case childNode.type
                    Case eXMLElementType.Attribute : pathAttribute = childNode.name
                    Case eXMLElementType.Node : pathAttribute = ""
                End Select

                nodesList.Add(_xmlReaderWriter.ReadNodeText(childNode.xmlPath, pathAttribute))
            Next

            Return nodesList
        End Function
        Private Sub ConvertFromObservableCollection(ByVal p_values As ICollection(Of String))
            Dim i As Integer = 0

            For Each childNode As cXMLNode In _xmlNodeItem.xmlChildren
                Dim pathAttribute As String = ""

                Select Case childNode.type
                    Case eXMLElementType.Attribute : pathAttribute = childNode.name
                    Case eXMLElementType.Node : pathAttribute = ""
                End Select

                _xmlReaderWriter.WriteNodeText(p_values(i), pathAttribute)
                i += 1
            Next
        End Sub

        Private Function ConvertFromListToObservableCollection() As ObservableCollection(Of String)
            Dim nodesList As New List(Of String)

            _xmlReaderWriter.ReadNodeListText(_pathNode, nodesList, _attributeName)

            Return New ObservableCollection(Of String)(nodesList)
        End Function
        Private Sub ConvertToListFromObservableCollection(ByVal p_values As ICollection(Of String))
            If p_values.Count > 0 Then
                Dim parentNodeName As String
                If _xmlNodeItem.xmlChildren.Count > 0 Then
                    parentNodeName = _xmlNodeItem.xmlChildren(0).name
                Else
                    parentNodeName = _xmlNodeItem.xmlChildrenRepeatName
                End If

                _xmlReaderWriter.WriteNodeListText(p_values.ToArray, _pathNode, parentNodeName)
            End If
        End Sub

        Private Function ConvertFromUniqueListToObservableCollection() As ObservableCollection(Of String)
            Dim nodesList As New ObservableCollection(Of String)

            nodesList = New ObservableCollection(Of String)(_xmlReaderWriter.ReadUniqueList(GetPathNodeStub()))

            Return nodesList
        End Function
        Private Sub ConvertToUniqueListFromObservableCollection(ByVal p_values As ICollection(Of String))
            Dim subNodeName As String = GetSuffix(_pathNode, _nsDivider)

            _xmlReaderWriter.WriteUniqueList(GetPathNodeStub(), subNodeName, p_values)
        End Sub

        Private Function ConvertFromPathListStoredKnown() As ObservableCollection(Of String)
            Dim nodesList As New ObservableCollection(Of String)

            nodesList = New ObservableCollection(Of String)(_xmlReaderWriter.ReadNodeListPath(GetPathNodeStub()))

            Return nodesList
        End Function
        Private Sub ConvertToPathListStoredKnown(ByVal p_values As ICollection(Of String))
            Dim subNodeName As String = GetSuffix(_pathNode, _nsDivider)

            _xmlReaderWriter.WriteNodeListPath(GetPathNodeStub(), subNodeName, p_values)
        End Sub

        Private Function GetPathNodeStub() As String
            Dim removeString As String = _nsDivider & GetSuffix(_pathNode, _nsDivider)
            Dim pathNodeStub As String = Left(_pathNode, _pathNode.Length - removeString.Length)

            Return pathNodeStub
        End Function

        Private Function ConvertFromPathStoredUnknown(Optional ByVal p_defaultPath As String = "") As String
            Dim nodeValue As String = ReadFromSingleNode()

            If AbsolutePath(nodeValue, , , True) Then
                If Not IO.Directory.Exists(nodeValue) Then nodeValue = p_defaultPath
            Else
                nodeValue = p_defaultPath
            End If

            Return nodeValue
        End Function
        Private Sub ConvertToPathStoredUnknown(Optional ByVal p_value As String = "")
            Dim writeValue As String

            If String.IsNullOrEmpty(p_value) Then
                writeValue = _xmlNodeItem.value
            Else
                writeValue = p_value
            End If

            RelativePath(writeValue)

            WriteToSingleNode(writeValue)
        End Sub

        Private Function ConvertFromPathStoredKnown() As String
            Dim nodeValue As String = ReadFromSingleNode()

            AbsolutePath(nodeValue)

            Return nodeValue
        End Function
        Private Sub ConvertToPathStoredKnown(Optional ByVal p_value As String = "")
            Dim writeValue As String

            If String.IsNullOrEmpty(p_value) Then
                writeValue = _xmlNodeItem.value
            Else
                writeValue = p_value
            End If

            RelativePath(writeValue, True)

            WriteToSingleNode(writeValue)
        End Sub


        Private Function ReadFromSingleNode() As String
            Return _xmlReaderWriter.ReadNodeText(_pathNode, _attributeName)
        End Function
        Private Sub WriteToSingleNode(Optional ByVal p_value As String = "")
            Dim writeValue As String

            If String.IsNullOrEmpty(p_value) Then
                writeValue = _xmlNodeItem.value
            Else
                writeValue = p_value
            End If

            _xmlReaderWriter.WriteNodeText(writeValue, _pathNode, _attributeName)
        End Sub
#End Region

    End Class

End Namespace

