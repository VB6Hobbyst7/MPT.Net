Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.cEnumerations

Public Class cNodeAssemblerXML
#Region "Methods: Friend"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_nodePath"></param>
    ''' <param name="p_nodeAttribute"></param>
    ''' <param name="p_readWriteType"></param>
    ''' <param name="p_readOnly"></param>
    ''' <param name="p_defaultValue"></param>
    ''' <param name="p_xmlChildrenRepeat">Name of the repeating child element. Used for any list if writing. Not necessary for read only.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function InitializeXMLNode(ByVal p_nodePath As String,
                                 Optional ByVal p_nodeAttribute As String = "",
                                 Optional ByVal p_readWriteType As eReadWriteConversion = eReadWriteConversion.readWriteOnly,
                                 Optional ByVal p_readOnly As Boolean = False,
                                 Optional ByVal p_defaultValue As String = "",
                                 Optional ByVal p_xmlChildrenRepeat As String = "") As cXMLNode

        Dim xmlNode As New cXMLNode
        Dim nodeName As String
        Dim nsDivider As String = "/n:"

        If String.IsNullOrEmpty(p_nodeAttribute) Then
            nodeName = GetSuffix(p_nodePath, nsDivider)
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
            .xmlChildrenRepeatName = p_xmlChildrenRepeat
        End With

        Return xmlNode
    End Function
    Friend Shared Function InitializeXMLNodeAttributes(ByVal p_nodePath As String,
                                              Optional ByVal p_nodeAttributes As List(Of String) = Nothing,
                                              Optional ByVal p_readWriteType As eReadWriteConversion = eReadWriteConversion.readWriteOnly,
                                              Optional ByVal p_readOnly As Boolean = False,
                                              Optional ByVal p_defaultValue As String = "") As cXMLNode
        Dim nsDivider As String = "/n:"
        Dim nodeName As String = GetSuffix(p_nodePath, nsDivider)
        Dim xmlNode As New cXMLNode

        Try
            If p_nodeAttributes IsNot Nothing Then
                For Each nodeAttribute As String In p_nodeAttributes
                    Dim xmlNodeChild As New cXMLNode

                    With xmlNodeChild
                        .xmlPath = p_nodePath
                        .name = nodeAttribute
                        .type = eXMLElementType.Attribute
                        .readWriteAction = p_readWriteType
                        .isReadOnly = p_readOnly
                        .defaultValue = p_defaultValue
                    End With

                    xmlNode.xmlChildren.Add(xmlNodeChild)
                Next
            End If

            With xmlNode
                .name = nodeName
                .xmlPath = p_nodePath
                .readWriteAction = p_readWriteType
                .isReadOnly = p_readOnly
                .defaultValue = p_defaultValue
                .type = eXMLElementType.Header
            End With
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try


        Return xmlNode
    End Function
    Friend Shared Function InitializeXMLNodes(ByVal p_nodePaths As List(Of String),
                                  Optional ByVal p_nodeAttribute As String = "",
                                  Optional ByVal p_readWriteType As eReadWriteConversion = eReadWriteConversion.readWriteOnly,
                                  Optional ByVal p_readOnly As Boolean = False,
                                  Optional ByVal p_defaultValue As String = "") As cXMLNode

        Dim xmlNode As New cXMLNode
        Dim nsDivider As String = "/n:"

        Dim nodePathStub As String = FilterStringFromName(p_nodePaths(0), nsDivider & GetSuffix(p_nodePaths(0), nsDivider), True, False)

        If p_nodePaths IsNot Nothing Then
            For Each nodePath As String In p_nodePaths
                Dim xmlNodeChild As New cXMLNode

                With xmlNodeChild
                    If String.IsNullOrEmpty(p_nodeAttribute) Then
                        .name = GetSuffix(nodePath, nsDivider)
                        .type = eXMLElementType.Node
                    Else
                        .name = p_nodeAttribute
                        .type = eXMLElementType.Attribute
                    End If

                    .xmlPath = nodePath
                    .readWriteAction = p_readWriteType
                    .isReadOnly = p_readOnly
                    .defaultValue = p_defaultValue
                End With

                xmlNode.xmlChildren.Add(xmlNodeChild)
            Next
        End If

        With xmlNode
            .name = GetSuffix(nodePathStub, ":")
            .xmlPath = nodePathStub
            .readWriteAction = p_readWriteType
            .isReadOnly = p_readOnly
            .defaultValue = p_defaultValue
            .type = eXMLElementType.Header
        End With

        Return xmlNode
    End Function


    Friend Shared Function AssemblePath(ByVal p_pathParent As String) As String
        Dim nsStart As String = "//n:"

        Return nsStart & p_pathParent
    End Function
    Friend Shared Function AssemblePathStub(ByVal p_pathParent As String,
                                  Optional ByVal p_pathChildFirst As String = "",
                                  Optional ByVal p_pathChildSecond As String = "",
                                  Optional ByVal p_pathChildThird As String = "") As String
        Dim nsStart As String = "//n:"
        Dim ns As String = "/n:"
        Dim pathAssembled As String

        pathAssembled = nsStart & p_pathParent & ns

        If Not String.IsNullOrEmpty(p_pathChildFirst) Then
            pathAssembled &= p_pathChildFirst & ns
        Else
            Return pathAssembled
        End If

        If Not String.IsNullOrEmpty(p_pathChildSecond) Then
            pathAssembled &= p_pathChildSecond & ns
        Else
            Return pathAssembled
        End If

        If Not String.IsNullOrEmpty(p_pathChildThird) Then
            pathAssembled &= p_pathChildThird & ns
        Else
            Return pathAssembled
        End If

        Return pathAssembled
    End Function


#End Region
End Class
