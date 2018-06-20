Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.XML

Public Class frmXMLNodeCreateByPath
    Implements INotifyPropertyChanged
    Implements IMessengerEvent

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Variables"
    Private _nodesChanged As Boolean
    Private _cleanUpForm As Boolean
#End Region

#Region "Properties"
    Private _nodeName As String
    Public Property nodeName As String
        Set(ByVal value As String)
            If Not _nodeName = value Then
                'Convert name to appropriate value for XML files
                TrimWhiteSpace(value)
                value = ReplaceStringInName(value, "&", "_and_", True)

                _nodeName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("nodeName"))
            End If
        End Set
        Get
            Return _nodeName
        End Get
    End Property

    Private _nodeValue As String
    Public Property nodeValue As String
        Set(ByVal value As String)
            If Not _nodeValue = value Then
                _nodeValue = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("nodeValue"))
            End If
        End Set
        Get
            Return _nodeValue
        End Get
    End Property

    Private _nodePathSelected As String
    Public Property nodePathSelected As String
        Set(ByVal value As String)
            If Not _nodePathSelected = value Then
                _nodePathSelected = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("nodePathSelected"))
            End If
        End Set
        Get
            Return _nodePathSelected
        End Get
    End Property

    Private _nodeCreate As eNodeCreate
    Public Property nodeCreate As eNodeCreate
        Set(ByVal value As eNodeCreate)
            If Not _nodeCreate = value Then
                _nodeCreate = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("nodeCreate"))
            End If
        End Set
        Get
            Return _nodeCreate
        End Get
    End Property

    'Private options As cTestOptions

    Public Property nodeType As eXMLElementType

    Public Property parentNodeValue As String

#End Region

#Region "Initialization:"
    Friend Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        LoadPropertiesLists()

        'options = New cTestOptions
        ''options.booleanProperty = True
        'options.enumProperty = eNodeCreate.child
        'radBtnChildNode.DataContext = options
        'radBtnInsertBefore.DataContext = options
        'radBtnInsertAfter.DataContext = options

        _nodesChanged = False
        _cleanUpForm = True
    End Sub

    Friend Sub LoadPropertiesLists()
        nodeCreate = eNodeCreate.child

        radBtnChildNode.IsChecked = True
        btnCreateNode.IsEnabled = False

        '===Combo Box
        cmbBx_NodeType.ItemsSource = [Enum].GetValues(GetType(eXMLElementType))
        cmbBx_NodeType.SelectedIndex = 0

        '===Text Box
        txtBx_XMLPathExisting.Text = nodePathSelected
    End Sub

#End Region



#Region "Form Controls"
    '=== Buttons
    Private Sub btnCreateNode_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNode.Click

        'Check for warnings
        If (Not nodeType = eXMLElementType.Attribute AndAlso
            Not String.IsNullOrEmpty(parentNodeValue) AndAlso
            nodeCreate = eNodeCreate.child) Then

            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            "Current parent node has text. Non-attribute nodes will only be added after removing text values of the parent node. {0}Do you wish to continue?",
                                            "Warning",
                                            Environment.NewLine)
                Case eMessageActions.Yes
                    myXMLEditor.clearParentNodeValue = True
                Case eMessageActions.No
                    Exit Sub
            End Select
        End If

        'Set properties for operation
        myXMLEditor.addNodeType = nodeType
        myXMLEditor.addNodeName = nodeName
        myXMLEditor.addNodeValue = nodeValue
        myXMLEditor.addNodePath = nodePathSelected
        myXMLEditor.addNodeMethod = nodeCreate

        'Apply operation through editor class
        If myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.NodeAdd) Then
            _nodesChanged = True
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), "Node successfully added to selected XML files."))
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        CloseEvent()

        Me.Close()
    End Sub
    '===Combo Box
    Private Sub cmbBx_NodeType_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBx_NodeType.SelectionChanged
        nodeType = CType(cmbBx_NodeType.SelectedItem, eXMLElementType)

        If nodeType = eXMLElementType.Header Then
            txtBx_NodeValue.IsEnabled = False
            nodeValue = ""

            radBtnInsertAfter.IsEnabled = True
            radBtnInsertBefore.IsEnabled = True
            radBtnChildNode.IsEnabled = True
        Else
            txtBx_NodeValue.IsEnabled = True

            If nodeType = eXMLElementType.Node Then
                radBtnInsertAfter.IsEnabled = True
                radBtnInsertBefore.IsEnabled = True
                radBtnChildNode.IsEnabled = True
            ElseIf nodeType = eXMLElementType.Attribute Then
                radBtnInsertAfter.IsEnabled = False
                radBtnInsertBefore.IsEnabled = False
                radBtnChildNode.IsEnabled = False
                radBtnChildNode.IsChecked = True
            End If

        End If

        If nodeCreate = eNodeCreate.child Then

        End If

        CheckCreateNodeAllow()
    End Sub

    '=== Text Boxes
    Private Sub txtBx_NodeValue_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBx_NodeValue.TextChanged
        CheckCreateNodeAllow()
    End Sub
    Private Sub txtBx_NodeName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBx_NodeName.TextChanged
        CheckCreateNodeAllow()
    End Sub

    '=== Radio Buttons
    Private Sub radBtnChildNode_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnChildNode.Checked
        nodeCreate = eNodeCreate.child
    End Sub
    Private Sub radBtnInsertBefore_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnInsertBefore.Checked
        nodeCreate = eNodeCreate.insertBefore
    End Sub
    Private Sub radBtnInsertAfter_Checked(sender As Object, e As RoutedEventArgs) Handles radBtnInsertAfter.Checked
        nodeCreate = eNodeCreate.insertAfter
    End Sub

#End Region

#Region "Form Behavior"

    ''' <summary>
    ''' All actions to be done whenever the form is closed occur here.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Window_Closing(sender As Object, e As CancelEventArgs)
        If _cleanUpForm Then
            CloseEvent()
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Checks whether the form input conditions are sufficient to create the node of the specified type. If so, the button to do so is activated.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCreateNodeAllow()
        If (Not String.IsNullOrEmpty(nodeName) AndAlso nodeType = eXMLElementType.Header) Then
            btnCreateNode.IsEnabled = True
        ElseIf (Not String.IsNullOrEmpty(nodeName) AndAlso Not String.IsNullOrEmpty(nodeValue)) Then
            btnCreateNode.IsEnabled = True
        Else
            btnCreateNode.IsEnabled = False
        End If
    End Sub

    ''' <summary>
    ''' Performs the closing action of the form, including validation and saving of files, depending on the OK/Close status. If it returns 'True', then the form will be closed.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseEvent()
        'Check if XML files were changed. If so, the editor is refreshed to show the changes.
        If _nodesChanged Then
            windowXMLEditorBulk.ChangeXMLSource()
        End If

        _cleanUpForm = False
    End Sub
#End Region

End Class
