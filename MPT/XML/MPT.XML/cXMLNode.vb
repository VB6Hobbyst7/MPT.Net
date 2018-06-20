Option Explicit On
Option Strict On


''' <summary>
''' XML Node from the file read, including other properties describing its location, children, etc. 
''' Can be used to make a virtual class of the XML file. 
''' Additional properties allow display in the XML editors.
''' </summary>
''' <remarks></remarks>
Public Class cXMLNode
    Implements ICloneable

#Region "Properties"
    '=== General properties
    ''' <summary>
    ''' Name of the node.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
    ''' <summary>
    ''' Value of the node.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property value As String
    ''' <summary>
    ''' Node type, such as text, attribute, header (i.e. node that has no value and only contains other nodes).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property type As eXMLElementType
    ''' <summary>
    ''' Absolute path to the XML node.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlPath As String
    ''' <summary>
    ''' How many levels deep the node is found within the XML tree. Root node is level 0.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property level As Integer
    ''' <summary>
    ''' Node number in XML file. Header numbers match those of the first child value node.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property indexFlat As Integer
    ''' <summary>
    ''' Number of value nodes contained within a given header, regardless of how many levels down the value nodes are.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueNodeSpan As Integer

    ''' <summary>
    ''' All child nodes are stored here in order to mimic the hierarchy of the original XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlChildren As New List(Of cXMLNode)
    ''' <summary>
    ''' All child node elements of a repeating name are stored under this name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlChildrenRepeatName As String

    '=== Editor-specific properties
    ''' <summary>
    ''' Name of the XML file to which the node belongs.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileName As String
    ''' <summary>
    ''' Path to the XML file that contains the node. Used for saving node data back to the XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property filePath As String

    ''' <summary>
    ''' If there is more than one node of this name in a queried XML file or mirrored class, this is the 0-based index number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property result As Integer
    ''' <summary>
    ''' Row in the datagrid that the node first appears. This is the order in which the node was encountered within the list of XML files and nodes within a given XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property gridViewIndex As Integer

    ''' <summary>
    ''' Whether or not the node value is to be saved back into the original XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property saveChanges As Boolean
    ''' <summary>
    ''' Whether or not the value of a node has been changed in the editor since it was last gathered from/saved to the XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueChanged As Boolean

    Public Property readWriteAction As eReadWriteConversion

    Public Property isReadOnly As Boolean

    Public Property defaultValue As String
#End Region

#Region "Initialization"
    Public Sub New()

    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cXMLNode

        With myClone
            .name = name
            .value = value
            .type = type
            .xmlPath = xmlPath
            .level = level
            .indexFlat = indexFlat
            .valueNodeSpan = valueNodeSpan

            For Each xmlChild As cXMLNode In xmlChildren
                .xmlChildren.Add(CType(xmlChild.Clone, cXMLNode))
            Next

            .fileName = fileName
            .filePath = filePath
            .result = result
            .gridViewIndex = gridViewIndex
            .saveChanges = saveChanges
            .valueChanged = valueChanged
            .readWriteAction = readWriteAction
            .isReadOnly = isReadOnly
            .defaultValue = defaultValue
        End With

        Return myClone
    End Function
#End Region

End Class
