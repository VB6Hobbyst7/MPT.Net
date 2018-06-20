Option Explicit On
'Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting
Imports MPT.XML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cRegTest
Imports CSiTester.cMCValidator

''' <summary>
''' This form deals with editing multiple XML files simultaneously, based on node name. 
''' The nodes are selected via a treeview navigation display that is constructed from a refrenced XML.
''' The form can handle working with any XNL file.
''' Additionally,the form handles the synchronized renaming of model XMLs, model files, and folders.
''' </summary>
''' <remarks></remarks>
Public Class frmXMLEditorBulk
    Implements ILoggerEvent
    Implements IMessengerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Constants: Private"
    Private Const TITLE_NO_XML_FILES As String = "No XML Files Exist"
    Private Const PROMPT_NO_XML_FILES As String = "No XML files exist in the selected directory or subdirectories."

    Private Const PROMPT_NODE_DELETED As String = "Specified node was deleted from the selected XML files."
    Private Const PROMPT_CREATE_BY_PATH_ATTRIBUTE_ERROR As String = "An attribute has been selected. Nodes & attributes can only be added to nodes." & vbNewLine &
                                                                    "Please select a value node or header node."
    Private Const PROMPT_BROWSE_DIRECTORY_XML As String = "Browse for parent directory of XMLs to modify."
#End Region

#Region "Variables"
    Private cellText As String
    Private cellTextUndo As String
    Private cellBeingEdited As DataGridCellEditEndingEventArgs = Nothing

    ''' <summary>
    ''' Path to a model control file to use for editing.
    ''' </summary>
    ''' <remarks></remarks>
    Private _modelControlPath As String

    ''' <summary>
    ''' Path to a directory containing model control files to edit.
    ''' </summary>
    ''' <remarks></remarks>
    Private _modelControlDirectory As String
#End Region

#Region "Properties"

#End Region

#Region "Initialization"
    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        InitializeData()
        SetButtons()

    End Sub

    Private Sub InitializeData()
        If myXMLEditor.suiteEditorXMLObjects.Count > 0 Then myTreeNavigator.DataContext = myXMLEditor.suiteEditorXMLObjects(0)

        dataGrid_ValueNodeContent.DataContext = myXMLEditor.suiteEditorXMLObjects
    End Sub

    Private Sub SetButtons()
        btnConvertXMLs.IsEnabled = False

        radBtn_RenameModelName.IsEnabled = False
        radBtn_RenameModelName.IsChecked = True
        radBtn_RenameModelID.IsEnabled = False

        chkBox_RenameFolder.IsEnabled = False
        chkBox_RenameModel.IsEnabled = False
        chkBox_RenameXML.IsEnabled = False

        'myTreeNavigator.selected
    End Sub
#End Region

#Region "Methods: Treeview"
    ''' <summary>
    ''' Retrieves name of selected node in treeview and updates datagrid view with newly generated classes based on the node name. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TreeViewItem_Selected(sender As Object, e As RoutedEventArgs)
        Dim selectedItem As cXMLNode = sender.datacontext
        '       selectedItem  sender.datacontext
        If e.Source Is e.OriginalSource Then
            txtBoxValueNode.Text = selectedItem.name

            'If data is currently loaded and changed, save the changes into the mirror class
            If dataGrid_ValueNodeContent.ItemsSource IsNot Nothing Then PreserveDataGridChanges()

            'Create and load the next class of data for display
            myXMLEditor.CreateXMLDataGridView(selectedItem.name)
            dataGrid_ValueNodeContent.ItemsSource = myXMLEditor.xmlEditorBulkDataGridObjects
        End If
    End Sub


#End Region

#Region "Methods: Datagrid"
    '====Data Sources
    ''' <summary>
    ''' Preserve the current changes in the class data of the datagrid view by updating the mirror XML class.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreserveDataGridChanges()
        Dim myName As String
        Dim myValue As String
        Dim myFlatIndex As Integer
        Dim myLevel As Integer
        Dim myFilePath As String
        Dim mySaveStatus As Boolean
        Dim myChangedStatus As Boolean

        For Each myEntry As cXMLNode In dataGrid_ValueNodeContent.ItemsSource
            myName = myEntry.name
            myValue = myEntry.value
            myFlatIndex = myEntry.indexFlat
            myLevel = myEntry.level
            myFilePath = myEntry.filePath
            mySaveStatus = myEntry.saveChanges
            myChangedStatus = myEntry.valueChanged
            myXMLEditor.PreserveXMLDataGridView(myName, myFlatIndex, myLevel, myValue, myFilePath, mySaveStatus, myChangedStatus)
        Next
    End Sub

    ''' <summary>
    ''' Creates new class for viewing XML data in the XML editor.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ChangeXMLSource()
        'Creates list of file paths
        myXMLEditor.CreateXMLEditorList()

        'Creates list of XML class objects
        myXMLEditor.MirrorAllEditorXMLS(myXMLEditor.xmlEditorPathList)
        If Not myXMLEditor.xmlEditorPathList.Count = 0 Then
            myXMLEditor.xmlEditorTemplatePath = GetPathDirectoryStub(myXMLEditor.xmlEditorPathList(0)) 'Updates template XML default start path for browser

            'Changes data source of datagrid and updates view
            Me.DataContext = myXMLEditor.suiteEditorXMLObjects 'Passes relevant binding information to sub-class
            myXMLEditor.CreateXMLDataGridView(myXMLEditor.suiteEditorXMLObjects(0).xmlMirror(0).name)
            dataGrid_ValueNodeContent.DataContext = myXMLEditor.suiteEditorXMLObjects
            dataGrid_ValueNodeContent.ItemsSource = myXMLEditor.xmlEditorBulkDataGridObjects

            'Updates datagrid view
            dataGrid_ValueNodeContent.CommitEdit()
            dataGrid_ValueNodeContent.CancelEdit()
            dataGrid_ValueNodeContent.Items.Refresh()

            'Changes data source of treeview and updates view
            myTreeNavigator.DataContext = myXMLEditor.suiteEditorXMLObjects(0)
            myTreeNavigator.Items.Refresh()
        Else
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                        PROMPT_NO_XML_FILES,
                                                        TITLE_NO_XML_FILES))
        End If

    End Sub

    '====Cell Editing
    ''' <summary>
    ''' Copy, paste, and undo actions. Can undo one action.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridCell_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'Dim selectedRowIndex As Integer = dataGrid_ValueNodeContent.SelectedIndex
        ' Dim currentSelectedItem = dataGrid_ValueNodeContent.SelectedItem

        Dim cell As DataGridCell = CType(sender, DataGridCell)
        If cell IsNot Nothing Then
            If Not cell.IsEditing Then
                If Not cell.IsReadOnly Then
                    If Not cell.IsFocused Then cell.Focus()
                    Try
                        If Not cell.IsSelected Then cell.IsSelected = True
                    Catch ex As Exception
                        'If Not suppressExStates Then
                        '    'myLogger
                        '    MsgBox(ex.Message)
                        '    MsgBox(ex.StackTrace)
                        'End If
                    End Try


                    If e.Key = Key.C AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for copy action
                        For Each exampleRow As cXMLNode In dataGrid_ValueNodeContent.SelectedItems
                            cellText = exampleRow.value
                        Next
                    ElseIf e.Key = Key.V AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for paste action
                        Try
                            For Each exampleRow As cXMLNode In dataGrid_ValueNodeContent.SelectedItems
                                cellTextUndo = exampleRow.value
                                exampleRow.value = cellText
                                exampleRow.valueChanged = True
                            Next
                        Catch ex As Exception
                            RaiseEvent Log(New LoggerEventArgs(ex))
                            Exit Sub
                        End Try

                        dataGrid_ValueNodeContent.Items.Refresh()
                    ElseIf e.Key = Key.Z AndAlso (Keyboard.Modifiers And ModifierKeys.Control) = ModifierKeys.Control Then
                        'Code for undo action
                        For Each exampleRow As cXMLNode In dataGrid_ValueNodeContent.SelectedItems
                            exampleRow.value = cellTextUndo
                            exampleRow.valueChanged = False
                        Next
                        dataGrid_ValueNodeContent.Items.Refresh()
                    End If
                End If
            End If

            'TODO: Currently cell is not being re-selected after refreshing datagrid
            'If Not cell.IsFocused Then cell.Focus()
            'If Not cell.IsSelected Then cell.IsSelected = True
            'dataGrid_ValueNodeContent.SelectedIndex = selectedRowIndex
            'dataGrid_ValueNodeContent.SelectedItem = currentSelectedItem

            'For Each ExampleRow As cXMLNodeDataGrid In dataGrid_ValueNodeContent.Items
            '    If ExampleRow Is dataGrid_ValueNodeContent.SelectedItem Then
            '        dataGrid_ValueNodeContent.SelectedItem = ExampleRow
            '    End If
            'Next

        End If

    End Sub

    'TODO: Not currently used. Might not be necessary
    Private Shared Function FindVisualChild(Of T As UIElement)(ByVal element As UIElement) As T
        Dim child As UIElement = element
        While child IsNot Nothing
            Dim correctlyTyped As T = TryCast(child, T)
            If correctlyTyped IsNot Nothing Then Return correctlyTyped
            If VisualTreeHelper.GetChildrenCount(child) < 1 Then Exit While
            child = CType(VisualTreeHelper.GetChild(child, 0), UIElement)
        End While
        Return Nothing
    End Function


    ''' <summary>
    ''' Sets status of cell for whether or not the value has been changed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_ValueNodeContent_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles dataGrid_ValueNodeContent.CellEditEnding

        Try
            Dim cell As TextBox = CType(e.EditingElement, TextBox)
            Dim cellText As String

            cellText = cell.Text

            For Each exampleRow As cXMLNode In dataGrid_ValueNodeContent.SelectedItems
                If cellText = exampleRow.value Then
                    exampleRow.valueChanged = False
                Else
                    exampleRow.value = cellText
                    exampleRow.valueChanged = True
                End If
            Next
        Catch ex As Exception
            'If Not suppressExStates Then
            '    'myLogger
            '    MsgBox(ex.Message)
            '    MsgBox(ex.StackTrace)
            'End If
        End Try

        cellBeingEdited = e
    End Sub

    ''' <summary>
    ''' Determines that whether cell has been edited and if so, refreshes the datagrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dataGrid_ValueNodeContent_CurrentCellChanged(sender As Object, e As EventArgs) Handles dataGrid_ValueNodeContent.CurrentCellChanged
        If (cellBeingEdited IsNot Nothing AndAlso
            cellBeingEdited.EditAction = DataGridEditAction.Commit) Then
            Try
                dataGrid_ValueNodeContent.Items.Refresh()
            Catch ex As Exception

            End Try
        End If
        cellBeingEdited = Nothing
    End Sub

    '====Selections
    ''' <summary>
    ''' Sets all rows to be checked.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckAll()
        For Each myEntry As cXMLNode In dataGrid_ValueNodeContent.ItemsSource
            myEntry.saveChanges = True
        Next

        'Exits edit mode of datagrid and refreshes the grid to display the changed checks
        dataGrid_ValueNodeContent.CommitEdit()
        dataGrid_ValueNodeContent.CancelEdit()
        dataGrid_ValueNodeContent.Items.Refresh()
    End Sub

    ''' <summary>
    ''' Sets all rows to not be checked.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckNone()
        For Each myEntry As cXMLNode In dataGrid_ValueNodeContent.ItemsSource
            myEntry.saveChanges = False
        Next

        'Exits edit mode of datagrid and refreshes the grid to display the changed checks
        dataGrid_ValueNodeContent.CommitEdit()
        dataGrid_ValueNodeContent.CancelEdit()
        dataGrid_ValueNodeContent.Items.Refresh()
    End Sub

    ''' <summary>
    ''' Sets a given selection of cells to be added to or removed from the run and/or compare actions.
    ''' </summary>
    ''' <param name="myCellSelectOperation">Whether the selection operation is to be added to, subtracted from, or is to replace the set marked to be checked.</param>
    ''' <remarks></remarks>
    Private Sub SelectionAddRemoveReplace(ByVal myCellSelectOperation As eCellSelectOperation)
        Dim myRowIndexList As New ObservableCollection(Of Integer)
        Dim booleanStatus As Boolean

        'If replacing selection, clear rows of "True" values
        If myCellSelectOperation = eCellSelectOperation.Replace Then CheckNone() 'Clear all examples status of true

        'Get Indices for selected row
        GetSelectedRowItems(myRowIndexList)

        'Set status for each selected row
        If myCellSelectOperation = eCellSelectOperation.Remove Then
            booleanStatus = False
        Else
            booleanStatus = True
        End If

        For Each selectedRowIndex As Integer In myRowIndexList
            myXMLEditor.xmlEditorBulkDataGridObjects(selectedRowIndex).saveChanges = booleanStatus
        Next

        'Exits edit mode of datagrid and refreshes the grid to display the changed checks
        dataGrid_ValueNodeContent.CommitEdit()
        dataGrid_ValueNodeContent.CancelEdit()
        dataGrid_ValueNodeContent.Items.Refresh()
    End Sub

    ''' <summary>
    ''' Determines which set of rows was selected in the datagrid rows and retrieves their index positions in the data referenced.
    ''' </summary>
    ''' <param name="myRowIndexList">List to be populated with row index list indices.</param>
    ''' <remarks></remarks>
    Private Sub GetSelectedRowItems(ByRef myRowIndexList As ObservableCollection(Of Integer))
        Dim myRowIndex As Integer
        Dim myRowCode As String = ""   'Name of example title clicked. Used to match the cell clicked with the collection index of examples, as these can lose sync

        'Determine which item was clicked in the datagrid rows
        myRowIndex = 0

        'Check each cell that is selected
        For Each exampleRow As cXMLNode In dataGrid_ValueNodeContent.SelectedItems
            myRowCode = CStr(exampleRow.gridViewIndex)
            'For a given selected cell, determine its index in examplesList and store it in a collection
            For Each myEntry As cXMLNode In dataGrid_ValueNodeContent.ItemsSource
                If myEntry.gridViewIndex = CDbl(myRowCode) Then
                    myRowIndexList.Add(myRowIndex)
                    Exit For
                End If
                myRowIndex = myRowIndex + 1
            Next
            myRowIndex = 0
        Next
    End Sub

    Private Sub OnHyperlinkClick(ByVal o As TextBlock, ByVal e As RoutedEventArgs)
        'Dim myExampleIndex As Integer   'Selected Example index within a given test set

        'Determine which example was clicked in the datagrid rows
        'myExampleIndex = GetSelectedExample(myTabIndex)

        ' = examplesTestSetList(myTabIndex).examplesList(myExampleIndex) 'Passes relevant binding information to sub-class

    End Sub

#End Region

#Region "Methods: General"
    ''' <summary>
    ''' Dynamically sets the maximum height of the datagrid so that scrollbars appear if the window is made too small to display all rows
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gridMain_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles gridMain.SizeChanged
        myTreeNavigator.MaxHeight = rowDG.ActualHeight - myTreeNavigator.Margin.Bottom - gridMain.Margin.Bottom
        dataGrid_ValueNodeContent.MaxHeight = rowDG.ActualHeight - dataGrid_ValueNodeContent.Margin.Bottom - gridMain.Margin.Bottom
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons: Saving, Browsing, Actions
    ''' <summary>
    ''' Saves changes to XML mirror class to the actual XML files
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Also updates classes to changed status from save.</remarks>
    Private Sub btnSaveXMLs_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveXMLs.Click
        PreserveDataGridChanges()           'Writes current changes to mirror class
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.Save)

        'Updates the XML classes if a node was deleted
        If myXMLEditor.nodeDeleted Then
            myXMLEditor.MirrorAllEditorXMLS(myXMLEditor.xmlEditorPathList)                  'Creates list of XML class objects

            'Changes data source of datagrid and updates view
            Me.DataContext = myXMLEditor.suiteEditorXMLObjects 'Passes relevant binding information to sub-class
            'myXMLEditor.CreateXMLDataGridView(myXMLEditor.suiteEditorXMLObjects(0).xmlMirror(0).name)

            dataGrid_ValueNodeContent.DataContext = myXMLEditor.suiteEditorXMLObjects
        End If

        myXMLEditor.CreateXMLDataGridView(txtBoxValueNode.Text)     'Writes updates to mirror class back to datagrid classes
        dataGrid_ValueNodeContent.ItemsSource = myXMLEditor.xmlEditorBulkDataGridObjects

        myXMLEditor.XMLChanged = True

        'Updates datagrid view
        dataGrid_ValueNodeContent.CommitEdit()
        dataGrid_ValueNodeContent.CancelEdit()
        dataGrid_ValueNodeContent.Items.Refresh()
    End Sub

    ''' <summary>
    ''' Browse for parent directory of XMLs to modify.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseXMLSource_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseXMLSource.Click
        'Set up default starting path
        Dim currDir As String = myXMLEditor.xmlEditorPath
        SetCurrDir(currDir, "\" & DIR_NAME_REGTEST)

        'Browse for location
        Dim path As String = BrowseForFolder(PROMPT_BROWSE_DIRECTORY_XML, currDir)

        'Retains current value if user cancels out of browse form
        If String.IsNullOrEmpty(path) Then path = myXMLEditor.xmlEditorPath

        'TODO:
        'Updates
        '        btnBrowseXMLSource.Content = path
        myXMLEditor.xmlEditorPath = path

        'Sets tooltip to include path
        btnBrowseXMLSource.ToolTip = path

        'Update Bulk Editor
        ChangeXMLSource()
    End Sub

    Private Sub btnBrowseXMLTemplateSource_Click(sender As Object, e As RoutedEventArgs) Handles btnBrowseXMLTemplateSource.Click
        Dim validPath As Boolean = False
        Dim currDir As String           'Current opening directory. 
        Dim myPath As String = ""
        Dim fileTypes As New List(Of String)

        'Set opening directory
        currDir = myXMLEditor.xmlEditorTemplatePath
        SetCurrDir(currDir, "\" & DIR_NAME_REGTEST)

        'Retains current value if user cancels out of browse form
        fileTypes.Add("xml")
        If Not BrowseForFile(myPath, currDir, "XML Files", fileTypes) Then myPath = currDir

        'Update Bulk Editor
        'TODO: ? btnBrowseXMLTemplateSource.Content = path

        If Not myPath = currDir Then
            myXMLEditor.xmlEditorTemplatePath = myPath

            'Sets tooltip to include path
            btnBrowseXMLTemplateSource.ToolTip = myPath

            'Generates mirrored class of the specified template XML file
            Dim xmlReader As New cXmlReadWrite
            myTreeNavigator.DataContext = xmlReader.MirrorXMLElementsAll(myPath)
        End If

    End Sub

    Private Sub btnConvertXMLs_Click(sender As Object, e As RoutedEventArgs) Handles btnConvertXMLs.Click
        'Set whether to rename by exampe name or model id
        If radBtn_RenameModelName.IsChecked = True Then
            myXMLEditor.ToExampleName = True
        Else
            myXMLEditor.ToExampleName = False
        End If

        'Set whether to convert the XML filenames and data
        If chkBox_RenameXML.IsChecked = True Then
            myXMLEditor.ConvertXMLFile = True
        Else
            myXMLEditor.ConvertXMLFile = False
        End If

        'Set whether to convert the model filenames and any associated table database XML setters
        If chkBox_RenameModel.IsChecked = True Then
            myXMLEditor.ConvertModelFile = True
        Else
            myXMLEditor.ConvertModelFile = False
        End If

        'Set whether to convert parent folder names, if model is in database folder structure
        If chkBox_RenameFolder.IsChecked = True Then
            myXMLEditor.ConvertFolder = True
        Else
            myXMLEditor.ConvertFolder = False
        End If

        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.Convert)

        myXMLEditor.XMLChanged = True
    End Sub

    '=== Buttons: Selections
    Private Sub btnRunAll_Click(sender As Object, e As RoutedEventArgs) Handles btnRunAll.Click
        CheckAll()
    End Sub
    Private Sub btnRunNone_Click(sender As Object, e As RoutedEventArgs) Handles btnRunNone.Click
        CheckNone()
    End Sub
    Private Sub btnRunSelectionAdd_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionAdd.Click
        SelectionAddRemoveReplace(eCellSelectOperation.Add)
    End Sub
    Private Sub btnRunSelectionRemove_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionRemove.Click
        SelectionAddRemoveReplace(eCellSelectOperation.Remove)
    End Sub
    Private Sub btnRunSelectionReplace_Click(sender As Object, e As RoutedEventArgs) Handles btnRunSelectionReplace.Click
        SelectionAddRemoveReplace(eCellSelectOperation.Replace)
    End Sub

    '=== Checkboxes: Naming Parameters for Files & Folders
    Private Sub chkBox_Rename_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_Rename.Checked
        radBtn_RenameModelName.IsEnabled = True
        radBtn_RenameModelID.IsEnabled = True

        chkBox_RenameFolder.IsEnabled = True
        chkBox_RenameModel.IsEnabled = True
        chkBox_RenameXML.IsEnabled = True
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then btnConvertXMLs.IsEnabled = True
    End Sub

    Private Sub chkBox_RenameXML_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameXML.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub
    Private Sub chkBox_RenameModel_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameModel.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub
    Private Sub chkBox_RenameFolder_Checked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameFolder.Checked
        btnConvertXMLs.IsEnabled = True
    End Sub

    Private Sub chkBox_Rename_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_Rename.Unchecked
        btnConvertXMLs.IsEnabled = False

        radBtn_RenameModelName.IsEnabled = False
        radBtn_RenameModelID.IsEnabled = False

        chkBox_RenameFolder.IsEnabled = False
        chkBox_RenameModel.IsEnabled = False
        chkBox_RenameXML.IsEnabled = False
    End Sub

    Private Sub chkBox_RenameXML_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameXML.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub
    Private Sub chkBox_RenameModel_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameModel.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub
    Private Sub chkBox_RenameFolder_Unchecked(sender As Object, e As RoutedEventArgs) Handles chkBox_RenameFolder.Unchecked
        If chkBox_RenameFolder.IsChecked Or chkBox_RenameModel.IsChecked Or chkBox_RenameXML.IsChecked Then
            btnConvertXMLs.IsEnabled = True
        Else
            btnConvertXMLs.IsEnabled = False
        End If
    End Sub

#End Region

    'Temporary? Checking that data is properly preserved and updated
    Private Sub btnRefresh_Click(sender As Object, e As RoutedEventArgs) Handles btnRefresh.Click
        ChangeXMLSource()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub btnXMLNodeCreateByPath_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLNodeCreateByPath.Click
        Dim windowXMLNodeCreateByPath As New frmXMLNodeCreateByPath
        Dim xmlNodePath As String = ""
        Dim xmlNodeType As eXMLElementType
        Dim xmlParentNodevalue As String = ""
        'Gets node path for current selected node for the editor
        If myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.NodeAdd, , xmlNodePath, , xmlNodeType, xmlParentNodevalue) Then
            If xmlNodeType = eXMLElementType.Attribute Then
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_CREATE_BY_PATH_ATTRIBUTE_ERROR))
                Exit Sub
            End If

            windowXMLNodeCreateByPath.nodePathSelected = xmlNodePath
            windowXMLNodeCreateByPath.parentNodeValue = xmlParentNodevalue
            windowXMLNodeCreateByPath.LoadPropertiesLists()
            windowXMLNodeCreateByPath.ShowDialog()
        End If
    End Sub

    Private Sub btnDeleteNode_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteNode.Click
        Dim xmlNodePath As String = ""
        Dim xmlNodeType As eXMLElementType

        'Get node data
        myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.NodeDelete, , xmlNodePath, , xmlNodeType, )

        myXMLEditor.deleteNodePath = xmlNodePath

        'Adjust attributes values depending on type
        If xmlNodeType = eXMLElementType.Attribute Then
            myXMLEditor.deleteNodeAttribute = GetSuffix(txtBoxValueNode.Text, "@")
        Else
            myXMLEditor.deleteNodeAttribute = ""
        End If

        'Delete nodes
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.NodeDelete)

        'Updates the XML classes if a node was deleted
        If myXMLEditor.nodeDeleted Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_NODE_DELETED))
            ChangeXMLSource()
            myXMLEditor.nodeDeleted = False
            myXMLEditor.XMLChanged = True
        End If

    End Sub

    '=== Suite Operations
    Private Sub btnXMLNodeCreateKeywords_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLNodeCreateKeywords.Click
        Dim windowXMLNodeCreateKeywords As New frmXMLNodeCreateKeywords(eXMLEditorAction.ActionToExistingFiles)
        Dim keywordsList As New List(Of String)

        If myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.KeywordsAddRemove, , , , , , keywordsList) Then
            windowXMLNodeCreateKeywords.keywordsList(0).keywordsExisting = New ObservableCollection(Of String)(keywordsList)
        End If

        windowXMLNodeCreateKeywords.InitializeDGClassesExisting()

        windowXMLNodeCreateKeywords.ShowDialog()
    End Sub

    Private Sub btnXMLNodeCreateObjects_Click(sender As Object, e As RoutedEventArgs) Handles btnXMLNodeCreateObjects.Click
        Dim windowXMLNodeCreateObjects As New frmXMLObjectCreate

        If myXMLEditor.GetCheckedXMLsBulkEditor(eXMLEditorAction.ObjectAdd) Then

        End If

        windowXMLNodeCreateObjects.ShowDialog()
    End Sub

    Private Sub btnFlattenDirectories_Click(sender As Object, e As RoutedEventArgs) Handles btnFlattenDirectories.Click
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.DirectoriesFlatten)
    End Sub

    Private Sub btnCreateDBirectories_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateDBirectories.Click
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.DirectoriesDBGather)
    End Sub

    Private Sub btnUpdateOutputSettingsFiles_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateOutputSettingsFiles.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.UpdateOutputSettingsFiles)

        windowFileFolderOperations.ShowDialog()
    End Sub

    Private Sub btnUpdateModels_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateModels.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.UpdateModelFiles)

        windowFileFolderOperations.ShowDialog()
    End Sub

    Private Sub btnCreateTxtFiles_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateTxtFiles.Click
        Dim windowGenerateTextFiles As New frmGenerateTextFiles

        windowGenerateTextFiles.ShowDialog()
    End Sub

    Private Sub btnAddMCXMLSuffix_Click(sender As Object, e As RoutedEventArgs) Handles btnAddMCXMLSuffix.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.RenameMCFilesAddSuffix)

        windowFileFolderOperations.ShowDialog()
    End Sub

    Private Sub btnRemoveOSImportTag_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveOSImportTag.Click
        Dim windowFileFolderOperations As New frmFileFolderOperations(eXMLEditorAction.RenameOutputSettingsFilesRemoveImportTag)

        windowFileFolderOperations.ShowDialog()
    End Sub

    '=== Examples Operations
    Private Sub btnBulkAutoXMLGenerator_Click(sender As Object, e As RoutedEventArgs) Handles btnBulkAutoXMLGenerator.Click
        windowXMLTemplateGenerator = New frmXMLTemplateGenerator()

        windowXMLTemplateGenerator.ShowDialog()
    End Sub

    Private Sub btnEditExample_Click(sender As Object, e As RoutedEventArgs) Handles btnEditExample.Click
        Dim filePathGenerator As New cMCReader
        If filePathGenerator.BrowseModelControlFile() Then
            Dim windowExampleEditor = New frmXMLTemplateGeneratorUnique(filePathGenerator.mcModels, filePathGenerator.filePaths)
            windowExampleEditor.ShowDialog()
        End If
    End Sub


    Private Sub btnEditExamples_Click(sender As Object, e As RoutedEventArgs) Handles btnEditExamples.Click
        Dim filePathGenerator As New cMCReader
        If filePathGenerator.BrowseModelControlFiles() Then
            Dim windowExampleEditor = New frmXMLTemplateGeneratorUnique(filePathGenerator.mcModels, filePathGenerator.filePaths)
            windowExampleEditor.ShowDialog()
        End If
    End Sub

    Private Sub btnUniqueXMLWriter_Click(sender As Object, e As RoutedEventArgs) Handles btnUniqueXMLWriter.Click
        Dim xmlFilePath As String = ""

        'Get node data
        myXMLEditor.ApplyCheckedXMLsBulkEditor(eXMLEditorAction.ActionToExistingFiles)

        'Updates the XML classes if a node was deleted
        If myXMLEditor.nodeDeleted Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(), PROMPT_NODE_DELETED))
            ChangeXMLSource()
            myXMLEditor.nodeDeleted = False
            myXMLEditor.XMLChanged = True
        End If
    End Sub

    Private Sub btnValidateSchema_Click(sender As Object, e As RoutedEventArgs) Handles btnValidateSchema.Click
        Dim windowXMLModelValidate As New frmMCValidate(eSchemaValidate.RunSchemaValidationCustom)

        windowXMLModelValidate.ShowDialog()
    End Sub
End Class
