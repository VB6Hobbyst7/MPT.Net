Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.Xml

Imports CSiTester.cLibPath

''' <summary>
''' Complete copy of an XML file in memory. Includes the name of the XML and all nodes and node values and attributes, with node parent-sibling-neighbor relationships preserved from XML file
''' </summary>
''' <remarks></remarks>
Public Class cXMLObject

#Region "Properties"
    ''' <summary>
    ''' Filename (without path) of the XML file used to create xmlMirror.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileName As String
    ''' <summary>
    ''' File path to the XML file used to create xmlMirror.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePath As String
    ''' <summary>
    ''' Class representation of an XML file. Contains 1 or more XML nodes.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlMirror As ObservableCollection(Of cXMLNode)
#End Region

#Region "Initialization"
    Friend Sub New()
        xmlMirror = New ObservableCollection(Of cXMLNode)
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Creates a single row entry in the datagrid view by extracting data from the mirrored XML class
    ''' </summary>
    ''' <param name="p_nodeName">Name of the node to be queried</param>
    ''' <param name="p_xmlNodeDataGridView">Class forming the data for the datagrid view. This is populated through this sub</param>
    ''' <param name="p_xmlCollection">Section of the XML class to be checked. Only the highest level is checked, but truncated levels drill down into the class during recursion.</param>
    ''' <param name="p_gridviewIndex">The row in the datagrid that the node first appears, which coincides with the order by which the node was first read.</param>
    ''' <remarks></remarks>
    Sub CreateXMLDataGridView(ByVal p_nodeName As String,
                              ByRef p_xmlNodeDataGridView As ObservableCollection(Of cXMLNode),
                              ByVal p_xmlCollection As ObservableCollection(Of cXMLNode),
                              ByRef p_gridviewIndex As Integer)
        Dim resultOccurrence As Integer = 0

        For Each myXMLNode As cXMLNode In p_xmlCollection
            If StringsMatch(myXMLNode.name, p_nodeName) Then
                'Add Bulk Editor datagrid properties to node class
                myXMLNode.fileName = fileName
                myXMLNode.gridViewIndex = p_gridviewIndex

                'Consider multiple occurrences of the same node name in the node object
                resultOccurrence = resultOccurrence + 1
                myXMLNode.result = resultOccurrence

                'Add node class to the datagrid view class
                p_xmlNodeDataGridView.Add(myXMLNode)

                'Advance gridview row counter. 
                'Only end up with multiple rows in this function if there are multiple nodes of the same name in the same XML object
                p_gridviewIndex = p_gridviewIndex + 1
            Else        'Drill down to child node
                CreateXMLDataGridView(p_nodeName, p_xmlNodeDataGridView, myXMLNode.xmlChildren, p_gridviewIndex)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Writes the datagrid view data back into the classes that mirror the XML files. 
    ''' These can be fetched later to view the changed results, but without saving them to the XML files.
    ''' </summary>
    ''' <param name="p_nodeName">Node name.</param>
    ''' <param name="p_indexFlat">The index ordering of the node encountered, independent of the node hierarchy. The first child has the same index as the parent.</param>
    ''' <param name="p_indexLevel">Hierarchical index level (e.g. 0 = topmost parent, 1 = first child, 2 = second child, etc.)</param>
    ''' <param name="p_value">Value of the node.</param>
    ''' <param name="p_xmlCollection">Collection of classes that mirror the XML files.</param>
    ''' <param name="p_saveStatus">Indicates whether or not the node is to be saved over an existing node.</param>
    ''' <param name="p_changedStatus">Indicates whether or not a node value has changed from the original value.</param>
    ''' <remarks></remarks>
    Sub PreserveXMLDataGridView(ByVal p_nodeName As String,
                                ByVal p_indexFlat As Integer,
                                ByVal p_indexLevel As Integer,
                                ByVal p_value As String,
                                ByVal p_xmlCollection As ObservableCollection(Of cXMLNode),
                                ByVal p_saveStatus As Boolean,
                                ByVal p_changedStatus As Boolean)

        For Each myXMLNode As cXMLNode In p_xmlCollection
            If IsMatchingXMLNode(myXMLNode, p_nodeName, p_indexFlat, p_indexLevel) Then
                With myXMLNode
                    .value = p_value
                    .valueChanged = p_changedStatus
                    .saveChanges = p_saveStatus
                End With
            Else
                PreserveXMLDataGridView(p_nodeName, p_indexFlat, p_indexLevel, p_value, myXMLNode.xmlChildren, p_saveStatus, p_changedStatus)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Determines if the node object matches the supplied criteria.
    ''' </summary>
    ''' <param name="p_xmlNode">XML node object to check.</param>
    ''' <param name="p_name">Name of the node element.</param>
    ''' <param name="p_indexFlat">The index ordering of the node encountered, independent of the node hierarchy. The first child has the same index as the parent.</param>
    ''' <param name="p_indexLevel">Hierarchical index level (e.g. 0 = topmost parent, 1 = first child, 2 = second child, etc.)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMatchingXMLNode(ByVal p_xmlNode As cXMLNode,
                                      ByVal p_name As String,
                                      ByVal p_indexFlat As Integer,
                                      ByVal p_indexLevel As Integer) As Boolean

        If (p_xmlNode.name = p_name AndAlso
            p_xmlNode.indexFlat = p_indexFlat AndAlso
            p_xmlNode.level = p_indexLevel) Then

            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class



