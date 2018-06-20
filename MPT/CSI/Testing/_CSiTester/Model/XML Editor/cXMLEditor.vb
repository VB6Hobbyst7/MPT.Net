Option Explicit On
Option Strict On

Imports Scripting
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.Reporting
Imports MPT.XML
Imports MPT.XML.ReaderWriter

Imports CSiTester.cMCModel
Imports CSiTester.cExampleTestSet

Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cFileAttachment

Imports CSiTester.cFileOutputSettings

Imports CSiTester.cXMLCSi

''' <summary>
''' Main class for the XML bulk editor form and related properties and functions.
''' </summary>
''' <remarks></remarks>
Public Class cXMLEditor
    Implements INotifyPropertyChanged
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


#Region "Constants: Private"
    Private Const TITLE_RENAME_MODEL_FILE As String = "Rename Model File"
    Private Const PROMPT_RENAME_MODEL_FILE As String = "Folder structure is not that of the expected database organization. Is the model file in subfolder 'Models'?"

    Private Const TITLE_RENAME_PARENT_FOLDER As String = "Rename Parent Folder"
    Private Const PROMPT_RENAME_PARENT_FOLDER As String = "Folder structure is not that of the expected database organization. Renaming the parent folder may result in unexpected consequences. Do you wish to continue with renaming the folder?"

    Private Const PROMPT_MODEL_SOURCE_DIRECTORY_NOT_FOUND As String = "Model source directory path cannot be found."
    Private Const PROMPT_MODEL_SOURCE_DIRECTORY_NO_XML_FILES As String = "Model source directory does not contain any model XML files."
    Private Const PROMPT_MODEL_SOURCE_DIRECTORY_SPECIFY_BULK_EDITOR As String = " " & vbNewLine & vbNewLine &
        "Please specify the location of the models to be tested. Models must have corresponding XML files to be used in CSiTester." & vbNewLine &
        "'Cancel' will exit the program."""


    Private Const PROMPT_NO_NODE_SELECTED As String = "Please select a node from the navigator and model from the datagrid for adding the node to. Currently either no node or no model is selected."

    Private Const TITLE_PRESERVE_FILES As String = "Preserve Files?"
    Private Const PROMPT_PRESERVE_OUTPUTSETTINGS_ATTACHMENT As String = "OutputSettings.xml file attachments exist in some of the examples selected." & vbNewLine & vbNewLine &
                                       "Once flattened, the files will override table settings in the model files if retained." & vbNewLine & vbNewLine &
                                       "Do you wish to retain these files?"
#End Region

#Region "Fields"
    ''' <summary>
    ''' If true, then all outputSettings.xml files in the attachment folders will be copied down to the model level is a DB-structured folder is flattened. 
    ''' If false, this action will not occur and the attachment will be lost.
    ''' </summary>
    ''' <remarks></remarks>
    Private _preserveOutputSettingsAttachment As Boolean

    Private _xmlReaderWriter As New cXmlReadWrite
#End Region

#Region "Properties"
    ''' <summary>
    ''' Indicates if the underlying XML files have been changed. If so, this will trigger various form refreshing, and creating of new objects in memory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property XMLChanged As Boolean = False

    '=== Path properties
    ''' <summary>
    ''' Path to the template file used for created the treeview navigator.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlEditorTemplatePath As String
    ''' <summary>
    ''' Path to the parent folder containing various XML files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlEditorPath As String
    ''' <summary>
    ''' List of paths to the various XML files to be loaded in the editor.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlEditorPathList As New List(Of String)

    '=== Collections
    ''' <summary>
    ''' Collection of mirrored XML object classes. This is the collection source used for making various constructions of xmlEditorBulkDataGridObjects.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property suiteEditorXMLObjects As List(Of cXMLObject) = New List(Of cXMLObject)
    ''' <summary>
    ''' Collection of node classes to be displayed in a datagridvide.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlEditorBulkDataGridObjects As List(Of cXMLNode) = New List(Of cXMLNode)

    '=== File/Folder Operations properties
    ''' <summary>
    ''' Convert names to example name. Else, names converted to model ID.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ToExampleName As Boolean
    ''' <summary>
    ''' Convert XML file name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConvertXMLFile As Boolean
    ''' <summary>
    ''' Convert model file name and any synched XML filenames and values.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConvertModelFile As Boolean
    ''' <summary>
    ''' Convert folder name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConvertFolder As Boolean
    ''' <summary>
    ''' Directory from which files &amp; folders are to be read, copied, or deleted from.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sourceDirectory As String
    ''' <summary>
    ''' Directory to which files are to be copied, folders created.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property destinationDirectory As String

    ''' <summary>
    ''' Signals form to refresh if a node has been deleted.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property nodeDeleted As Boolean

    '=== Adding/Removing Properties
    ''' <summary>
    ''' Type of node to be added to an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addNodeType As eXMLElementType
    ''' <summary>
    ''' Name of the node to be added to an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addNodeName As String
    ''' <summary>
    ''' Value to be assigned to the node added to an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addNodeValue As String
    ''' <summary>
    ''' Path specifying the location where the node is to be added within an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addNodePath As String
    ''' <summary>
    ''' Path specifying the location where the node is to be deleted from within an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteNodePath As String
    ''' <summary>
    ''' Name of the attribute to be deleted from an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property deleteNodeAttribute As String
    ''' <summary>
    ''' Specifies whether the node to be created is to be inserted before or after a node, or made as a child node.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addNodeMethod As eNodeCreate
    ''' <summary>
    ''' If true, the parent node will be cleared of any text values, making it a 'header node'. False, any existing text will remain in the header node when child nodes are created.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property clearParentNodeValue As Boolean = False
    ''' <summary>
    ''' List of keywords to add to the XML file, if new.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addKeywordList As List(Of String) = New List(Of String)
    ''' <summary>
    ''' List of keywords to remove from the XMl file, if present.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property removeKeywordList As List(Of String) = New List(Of String)
    ''' <summary>
    ''' Object type, such as attachment, image, or incident object, to be added to an XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property addObjectType As eXMLObjectType

    Private _myMCModel As cMCModel = New cMCModel
    ''' <summary>
    ''' Class representing the model control XML file to be generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property myMCModel As cMCModel
        Set(ByVal value As cMCModel)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("myMCModel"))
        End Set
        Get
            Return _myMCModel
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()
        'Default of XML editor opening in the model source directory
        suiteEditorXMLObjects = New List(Of cXMLObject)

        'By default, when first loading, the editor will use the paths and models referenced by regTest as the source models directory
        xmlEditorPath = myRegTest.models_database_directory.path
        xmlEditorPathList = myCsiTester.suiteXMLPathList
    End Sub

    Private Property g_outputSettingsVersionSession As String

    ''' <summary>
    ''' Used in startup form to initialize program data
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeXMLEditorData()
        'For XML editor, sets the default assignment of the model XML template to be the first model in the list
        If xmlEditorPathList.Count > 0 Then xmlEditorTemplatePath = xmlEditorPathList(0)
    End Sub

    ''' <summary>
    ''' Also used when doing a global refresh of the program data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReLoadXMLEditorData()
        myCsiTester.InitializeCSiTesterData()

        'For XML editor, sets the default assignment of the model XML template to be the first model in the list
        xmlEditorTemplatePath = xmlEditorPathList(0)
    End Sub
#End Region

#Region "Methods: Bulk Editor"
    '==== XML Bulk Editor
    ''' <summary>
    ''' Creates the list of file paths to all of the XMLs in the directory and subdirectories of the path specified
    ''' </summary>
    ''' <remarks></remarks>
    Sub CreateXMLEditorList()
        xmlEditorPathList.Clear()
        xmlEditorPathList = ListFilePathsInDirectory(xmlEditorPath, True, , ".xml")
    End Sub

    ''' <summary>
    ''' Creates the class of data to be viewed in the bulk editor for standard nodes. 
    ''' Displays node value for all XML files, provides classes and variables for referencing and listing in lower functions.
    ''' </summary>
    ''' <param name="myNodeName">Node name</param>
    ''' <remarks></remarks>
    Sub CreateXMLDataGridView(ByVal myNodeName As String)
        Dim gridviewIndex As Integer = 0

        'Create class to be used to display datagrid view
        xmlEditorBulkDataGridObjects = New List(Of cXMLNode)

        For Each myXMLObject As cXMLObject In suiteEditorXMLObjects
            If myNodeName = myXMLObject.xmlMirror(0).name Then      'Create view for root node
                myXMLObject.CreateXMLDataGridView(myNodeName, xmlEditorBulkDataGridObjects, myXMLObject.xmlMirror, gridviewIndex)
            Else                                                    'Create view for child nodes
                myXMLObject.CreateXMLDataGridView(myNodeName, xmlEditorBulkDataGridObjects, myXMLObject.xmlMirror(0).xmlChildren, gridviewIndex)
            End If


            'Advance gridview row counter
            gridviewIndex = gridviewIndex + 1
        Next
    End Sub

    ''' <summary>
    ''' Saves the datagrid view data back into the basic XML mirror objects. This saves the data for the session, although another step is needed to save the data to an XML file.
    ''' </summary>
    ''' <param name="myNodeName">Node name</param>
    ''' <param name="myIndexFlat">Index number of the value node in the XML file</param>
    ''' <param name="myLevel">Hierarchy level of the node in the XML file</param>
    ''' <param name="myFilePath">Name of the XML file</param>
    ''' <remarks></remarks>
    Sub PreserveXMLDataGridView(ByVal myNodeName As String, ByVal myIndexFlat As Integer, ByVal myLevel As Integer, ByVal myValue As String, ByVal myFilePath As String, ByVal mySaveStatus As Boolean, ByVal myChangedStatus As Boolean)
        For Each myXMLObject As cXMLObject In suiteEditorXMLObjects
            If myXMLObject.xmlMirror(0).filePath = myFilePath Then
                myXMLObject.PreserveXMLDataGridView(myNodeName,
                                                    myIndexFlat,
                                                    myLevel,
                                                    myValue,
                                                    myXMLObject.xmlMirror(0).xmlChildren,
                                                    mySaveStatus,
                                                    myChangedStatus)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Gathers list of selected XML files and performs a specifide action to them. If necessary, subsequent actions are taken for updating and refreshing other parts of the program.
    ''' </summary>
    ''' <param name="myAction">Action to perform to the selected XML files.</param>
    ''' <remarks></remarks>
    Function ApplyCheckedXMLsBulkEditor(ByVal myAction As eXMLEditorAction) As Boolean
        Dim myXMLPaths As New List(Of String)
        Dim myXMLObjects As New List(Of cXMLObject)

        ApplyCheckedXMLsBulkEditor = False

        'Apply operations to each model selected
        If xmlEditorBulkDataGridObjects IsNot Nothing Then
            'Get list of XML files selected for saving or other actions, then apply the actions
            For Each myXMLGridviewObject As cXMLNode In xmlEditorBulkDataGridObjects
                'XML class objects in the datagrid viewer
                If myXMLGridviewObject.saveChanges Then
                    'Proceeds to save changes specified
                    Try
                        'Get list of selected examples
                        For Each myXMLObject As cXMLObject In suiteEditorXMLObjects
                            'XML class objects that are mirrors of the files
                            If myXMLObject.xmlMirror(0).filePath = myXMLGridviewObject.filePath Then
                                'XML classes are matched, for passing the 'saved' status from the datagrid viewer object through the mirror class
                                myXMLPaths.Add(myXMLObject.xmlMirror(0).filePath)
                                If myAction = eXMLEditorAction.Save Then myXMLObjects.Add(myXMLObject)
                            End If
                        Next
                    Catch ex As Exception
                        RaiseEvent Log(New LoggerEventArgs(ex))
                        Exit Function
                    End Try
                End If
            Next

            'Apply actions to the list of examples
            Select Case myAction
                Case eXMLEditorAction.Save : ApplyEditorActionSaveToSelection(myXMLPaths, myXMLObjects)
                Case Else
                    SetupEditorActions(myXMLPaths, myAction)
                    ApplyEditorActionsToSelection(myXMLPaths, myAction)
            End Select

            'Main form will be updated once returned
            ApplyCheckedXMLsBulkEditor = True
            XMLChanged = True

            'Do any global operations for setup after applying operations to each model selected
            FinalizeEditorAction(myAction)
        End If
    End Function

    ''' <summary>
    ''' Does various preparation actions to selected files before performing operations.
    ''' </summary>
    ''' <param name="myAction"></param>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="myXMLNodePath">Path to the XML node within the XML file.</param>
    ''' <param name="myXMLNodeName">Name of the XML node to add to the XML file.</param>
    ''' <param name="myXMLNodeType">Type of node to be added to the XML file.</param>
    ''' <param name="myXMLNodeValue">Value of the XML node to add to the XML file.</param>
    ''' <param name="myXMLKeywordsList">Keywords list to be filled with all keywords for a given example from the example XML file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCheckedXMLsBulkEditor(ByVal myAction As eXMLEditorAction, _
                                      Optional ByRef myXMLPath As String = "", _
                                      Optional ByRef myXMLNodePath As String = "", _
                                      Optional ByRef myXMLNodeName As String = "", _
                                      Optional ByRef myXMLNodeType As eXMLElementType = eXMLElementType.Node, _
                                      Optional ByRef myXMLNodeValue As String = "", _
                                      Optional ByRef myXMLKeywordsList As List(Of String) = Nothing) As Boolean
        GetCheckedXMLsBulkEditor = False

        Try
            For Each myXMLGridviewObject As cXMLNode In xmlEditorBulkDataGridObjects
                'XML class objects in the datagrid viewer
                If myXMLGridviewObject.saveChanges Then
                    'Proceeds to save changes specified
                    GetCheckedXMLsBulkEditor = True
                    Try
                        With myXMLGridviewObject
                            myXMLPath = .filePath
                            myXMLNodeName = .name
                            myXMLNodeType = .type
                            myXMLNodePath = .xmlPath
                            myXMLNodeValue = .value
                        End With

                        Select Case myAction
                            Case eXMLEditorAction.NodeAdd, eXMLEditorAction.NodeDelete
                                Exit Function
                            Case eXMLEditorAction.KeywordsAddRemove
                                'Create unique list of keywords XML files from the XMLs to be edited, including new key words
                                myXMLKeywordsList = CreateUniqueListString(GetKeywordsListFromExample(myXMLPath), myXMLKeywordsList).ToList
                            Case eXMLEditorAction.ObjectAdd

                        End Select
                    Catch ex As Exception
                        Exit Function
                    End Try
                End If
            Next

            Select Case myAction
                Case eXMLEditorAction.NodeAdd
                    If Not GetCheckedXMLsBulkEditor Then
                        RaiseEvent Messenger(New MessengerEventArgs(PROMPT_NO_NODE_SELECTED))
                    End If
                Case eXMLEditorAction.KeywordsAddRemove
                    addKeywordList = New List(Of String)
                    removeKeywordList = New List(Of String)
                Case eXMLEditorAction.ObjectAdd

            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Exit Function
        End Try
    End Function

    ''' <summary>
    '''Performs any global operations for setup before applying operations to each model selected.
    ''' </summary>
    ''' <param name="xmlPaths">ByVal xmlPaths As List(Of String), </param>
    ''' <param name="myAction">Action to perform to the selected XML files.</param>
    ''' <remarks></remarks>
    Sub SetupEditorActions(ByVal xmlPaths As List(Of String), ByVal myAction As eXMLEditorAction)
        Try
            Select Case myAction
                Case eXMLEditorAction.DirectoriesFlatten
                    'Account for global action of preserving any attachment files not stored at the model level.
                    Dim outPutSettingsAttachmentExist As Boolean = False

                    'Check to see if there exists an outputSettings XMl file that exists in the attachments folder in any of the examples.
                    For Each xmlPath As String In xmlPaths
                        If CheckAttachment(xmlPath, TAG_ATTACHMENT_TABLE_SET_FILE) Then
                            outPutSettingsAttachmentExist = True
                            Exit For
                        End If
                    Next
                    If outPutSettingsAttachmentExist Then
                        'A file does exist, and a global action should be decided as to whether or not to preserve the files
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Question),
                                            PROMPT_PRESERVE_OUTPUTSETTINGS_ATTACHMENT,
                                            TITLE_PRESERVE_FILES)
                            Case eMessageActions.Yes
                                _preserveOutputSettingsAttachment = True
                            Case eMessageActions.No
                                _preserveOutputSettingsAttachment = False
                        End Select
                    End If
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Performs final intializing, updating, refreshing, and any other actions necessary after performing the specified action to the selected XML files.
    ''' </summary>
    ''' <param name="myAction">Action to perform to the selected XML files.</param>
    ''' <remarks></remarks>
    Sub FinalizeEditorAction(ByVal myAction As eXMLEditorAction)
        Try
            Select Case myAction
                Case eXMLEditorAction.Save

                Case eXMLEditorAction.Convert
                    'Update all relevant parts of the program
                    myCsiTester.InitializeCSiTesterData()                                                        'Refreshes CSiTester Class Information with new paths

                    'TODO: Below will work, but:
                    '   2. Grid defaults back to blank. Would be nice to save user view
                    If windowXMLEditorBulk IsNot Nothing Then windowXMLEditorBulk.ChangeXMLSource() 'Refreshes Bulk Editor, if it is open
                Case eXMLEditorAction.DirectoriesFlatten
                    'Update all relevant parts of the program
                    myCsiTester.InitializeCSiTesterData()                                                        'Refreshes CSiTester Class Information with new paths

                    If windowXMLEditorBulk IsNot Nothing Then windowXMLEditorBulk.ChangeXMLSource() 'Refreshes Bulk Editor, if it is open
                Case eXMLEditorAction.DirectoriesDBGather
                    'Update all relevant parts of the program
                    myCsiTester.InitializeCSiTesterData()                                                        'Refreshes CSiTester Class Information with new paths

                    If windowXMLEditorBulk IsNot Nothing Then windowXMLEditorBulk.ChangeXMLSource() 'Refreshes Bulk Editor, if it is open
                Case eXMLEditorAction.UpdateModelFiles
                    'Update all relevant parts of the program
                    myCsiTester.InitializeCSiTesterData()                                                        'Refreshes CSiTester Class Information with new paths

                    If windowXMLEditorBulk IsNot Nothing Then windowXMLEditorBulk.ChangeXMLSource() 'Refreshes Bulk Editor, if it is open
                Case eXMLEditorAction.ActionToExistingFiles
                    If windowXMLEditorBulk IsNot Nothing Then windowXMLEditorBulk.ChangeXMLSource() 'Refreshes Bulk Editor, if it is open
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Saves changed values to the selected XML files.
    ''' </summary>
    ''' <param name="xmlPaths">List of paths to the XML files to which the saving action is to be performed.</param>
    ''' <param name="myXMLObjects"></param>
    ''' <remarks></remarks>
    Sub ApplyEditorActionSaveToSelection(ByVal xmlPaths As List(Of String), ByVal myXMLObjects As List(Of cXMLObject))
        Dim i As Integer = 0
        For Each xmlPath As String In xmlPaths
            'Saves/Updates XML
            If _xmlReaderWriter.UpdateXMLFile(xmlPath, myXMLObjects(i).xmlMirror(0).xmlChildren) Then Me.nodeDeleted = False
            i += 1
        Next
    End Sub

    ''' <summary>
    ''' Applies the selected action to the list of selected XML files.
    ''' </summary>
    ''' <param name="p_xmlPaths">List of paths to the XML files to which the actions are performed.</param>
    ''' <param name="p_action">Action to perform to the selected XML files.</param>
    ''' <remarks></remarks>
    Sub ApplyEditorActionsToSelection(ByVal p_xmlPaths As List(Of String),
                                      ByVal p_action As eXMLEditorAction)
        Try
            For Each xmlPath As String In p_xmlPaths
                Select Case p_action
                    Case eXMLEditorAction.Convert
                        'Applies file & folder name changes to examples
                        ConvertXMLs(xmlPath, ToExampleName, ConvertXMLFile, ConvertModelFile, ConvertFolder)
                    Case eXMLEditorAction.DirectoriesFlatten
                        FlattenDirectory(xmlPath)
                    Case eXMLEditorAction.DirectoriesDBGather
                        GatherDBDirectoryFromFlattened(xmlPath)
                    Case eXMLEditorAction.UpdateModelFiles
                        UpdateModel(xmlPath, sourceDirectory)
                    Case eXMLEditorAction.NodeAdd
                        If addNodeMethod = eNodeCreate.child Then
                            'Clear text if parent node has text and child node is not an attribute
                            If clearParentNodeValue Then _xmlReaderWriter.WriteSingleXMLNodeValue(xmlPath, addNodePath, "")
                        End If
                        'Create nodes
                        _xmlReaderWriter.CreateNodeInXMLFile(xmlPath, addNodePath, addNodeName, addNodeValue, addNodeType, addNodeMethod)
                    Case eXMLEditorAction.NodeDelete

                    Case eXMLEditorAction.KeywordsAddRemove
                        Dim newKeywordList As New List(Of String)

                        'Creates fresh list with no blank keyword entries
                        newKeywordList = CreateUniqueListString(GetKeywordsListFromExample(xmlPath), newKeywordList).ToList

                        'Creates new list with added nodes, including blank ones
                        newKeywordList = CreateUniqueListString(addKeywordList, newKeywordList).ToList

                        'Remove any keywords specified to be removed
                        RemoveFromList(removeKeywordList, newKeywordList)

                        'Write new keywords list
                        WriteNewKeywordsList(xmlPath, newKeywordList)
                    Case eXMLEditorAction.ActionToExistingFiles
                        ''Create new control model XML populated from the existing XML file
                        'Dim myMCModel As New cMCModel(xmlPath)

                        ''Open set of forms for changing/specifying additional values to write/change in the existing XML file.
                        'Dim windowXMLTemplateGeneratorUnique As New frmXMLTemplateGeneratorUnique(myMCModel)

                        'windowXMLTemplateGeneratorUnique.ShowDialog()
                End Select
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Gather Data from Classes & Files"

    ''' <summary>
    ''' Creates list of keywords from the XML file specified, by searching the mirrored XML classes.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the XML file upon which the XML class is based.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKeywordsListFromExample(ByVal myXMLPath As String) As List(Of String)
        Dim keywordsList As New List(Of String)

        For Each myXMLFile As cXMLObject In suiteEditorXMLObjects
            If myXMLFile.xmlMirror(0).filePath = myXMLPath Then
                GatherKeywords(myXMLFile.xmlMirror, keywordsList)
                GetKeywordsListFromExample = keywordsList
                Exit Function
            End If
        Next

        GetKeywordsListFromExample = Nothing
    End Function

    ''' <summary>
    ''' Gathers all keywords from the provided XML class object
    ''' </summary>
    ''' <param name="myXMLNodes">List of XML nodes to check.</param>
    ''' <param name="myKeywordsList">List of keywords to fill.</param>
    ''' <remarks></remarks>
    Sub GatherKeywords(ByVal myXMLNodes As List(Of cXMLNode), ByRef myKeywordsList As List(Of String))
        For Each myXMLNode As cXMLNode In myXMLNodes
            'If node has children, call recursive function
            GatherKeywords(myXMLNode.xmlChildren, myKeywordsList)

            'If node is of the right type, add keywords
            If myXMLNode.name = "keyword" Then myKeywordsList.Add(myXMLNode.value)
        Next
    End Sub

#End Region

#Region "Methods: Create XML Entries"
    ''' <summary>
    ''' Lists file paths of model control XMLs in the source folder. Determines valid XML by checking the XMNLS reference.
    ''' </summary>
    ''' <param name="SourceFolderName">Path to the highest level folder to check.</param>
    ''' <param name="IncludeSubfolders">True = subfolders are also checked for XML files.</param>
    Sub GetModelXMLs(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean, ByRef myXMLPathList As List(Of String), Optional ByVal setXMLWarning As Boolean = True)
        Dim FSO As New FileSystemObject
        Dim SourceFolder As Folder
        Dim SubFolder As Folder
        Dim newPath As String
        Dim msgBoxMessage As String

        msgBoxMessage = PROMPT_MODEL_SOURCE_DIRECTORY_SPECIFY_BULK_EDITOR

        'If directory cannot be found, user is prompted to select a valid directory
        If Not IO.Directory.Exists(SourceFolderName) Then
            msgBoxMessage = PROMPT_MODEL_SOURCE_DIRECTORY_NOT_FOUND & msgBoxMessage
            newPath = myCsiTester.ExceptionModelsSource(msgBoxMessage)
            GetModelXMLs(newPath, True, myXMLPathList, setXMLWarning)
            Exit Sub
        End If

        SourceFolder = FSO.GetFolder(SourceFolderName)
        For Each FileItem As File In SourceFolder.Files
            If cPathModelControl.IsModelControlXML(FileItem.Path) Then myXMLPathList.Add(FileItem.Path)
        Next FileItem

        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                GetModelXMLs(SubFolder.Path, True, myXMLPathList, setXMLWarning)
            Next SubFolder
        End If

        'If directory exists, but no model XMLs exist in the folder
        If setXMLWarning Then
            If (myXMLPathList Is Nothing OrElse
                myXMLPathList.Count = 0) Then

                msgBoxMessage = PROMPT_MODEL_SOURCE_DIRECTORY_NO_XML_FILES & msgBoxMessage
                newPath = myCsiTester.ExceptionModelsSource(msgBoxMessage)
                GetModelXMLs(newPath, True, myXMLPathList)
                Exit Sub
            End If
        End If

        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    ''' <summary>
    ''' Creates nodes with values for a new incident to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="incidents">List of the incident numbers to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateIncident(ByVal myXMLPath As String, ByVal incidents As List(Of Integer))
        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                CreateObjectIncident("//n:incidents", incidents)
                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates nodes with values for a new ticket to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="tickets">List of ticket numbers to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateTicket(ByVal myXMLPath As String, ByVal tickets As List(Of Integer))
        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                CreateObjectTicket("//n:tickets", tickets)
                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates nodes with values for a new link to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="links">List of links to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateLink(ByVal myXMLPath As String, ByVal links As List(Of cMCLink))
        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                CreateObjectLink("//n:links", links)
                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates nodes with values for a new update to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="updates">List of updates to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateUpdate(ByVal myXMLPath As String, ByVal updates As List(Of cMCUpdate))
        Dim tempList As New List(Of Integer)

        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                Dim xmlCSi As New cXMLCSi()
                xmlCSi.CreateObjectUpdate("//n:updates", updates)

                For Each update As cMCUpdate In updates
                    tempList.Add(update.ticket)

                    CreateObjectTicket("//n:tickets", tempList)
                Next

                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the relative path to be written in the object by appending various strings. Creates placeholder for absolute path.
    ''' </summary>
    ''' <param name="myPath">Relative path to the file that the object refers to.</param>
    ''' <param name="myCopyAction">Class containing information related to copying the file that the object refers to.</param>
    ''' <remarks></remarks>
    Sub SetObjectRelativePath(ByVal myPath As String, ByVal myCopyAction As cFileManager)
        Dim tempRelativePath As String = ""

        With myCopyAction
            '=== Create full relative path for writing to XML
            'Add path segment to filename 
            If Not String.IsNullOrEmpty(myPath) Then
                tempRelativePath = myPath & "\" & .fileSource.fileNameWithExtension
            Else
                tempRelativePath = .fileSource.fileNameWithExtension
            End If

            'Add file extension to the path. File extension will not be added for files that do not have them
            If (.fileSource.fileExtension IsNot Nothing OrElse
                Not String.IsNullOrEmpty(.fileSource.fileExtension)) Then

                tempRelativePath = tempRelativePath & "." & .fileSource.fileExtension
            End If

            'Update relative path value with complete path
            myPath = tempRelativePath
        End With
    End Sub

    ''' <summary>
    ''' Adjusts values necessary for operations involving attachment and image files and XML object additions.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="myPath">Relative path to the file that the object refers to.</param>
    ''' <param name="myCopyAction">Class containing information related to copying the file that the object refers to.</param>
    ''' <remarks></remarks>
    Sub SetObjectValues(ByVal myXMLPath As String, ByVal myPath As String, ByVal myCopyAction As cFileManager)
        Dim tempExtension As String
        Dim fileSource As String
        Dim fileName As String

        With myCopyAction
            If .action = cFileManager.eFileAction.copySourceToDestination Then
                tempExtension = GetSuffix(myXMLPath, ".")

                'Filter out file name & assign to property
                fileName = FilterStringFromName(GetSuffix(myXMLPath, "\"), tempExtension, True, False)

                'Adjust source filepath
                'Strips filename from  source filepath and add new filename to source filepath
                fileSource = .fileSource.directory & "\" & fileName & "." & .fileSource.fileExtension

                .fileSource.SetProperties(fileSource)
            End If

            '=== Create full destination file path for copying supporting files
            .fileDestination.SetProperties(myXMLPath & myPath)
        End With
    End Sub



    ''' <summary>
    ''' Creates nodes with values for a new image attachment to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="images">Image information to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateImage(ByVal myXMLPath As String,
                    ByVal images As List(Of cFileAttachment))
        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                CreateObjectImage("//n:images", images)
                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Creates nodes with values for a new attachment to an example xml file.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="attachments">Attachment information to add to the XML file.</param>
    ''' <remarks></remarks>
    Sub CreateAttachment(ByVal myXMLPath As String, ByVal attachments As List(Of cFileAttachment))
        Try
            If _xmlReaderWriter.InitializeXML(myXMLPath) Then
                CreateObjectAttachment("//n:attachments", attachments)
                _xmlReaderWriter.SaveXML(myXMLPath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Writes new set of keywords to an example xml file.
    ''' </summary>
    ''' <param name="XMLExamplePath">Path to the example XML file.</param>
    ''' <param name="myXMLKeywordsList">List of keywords to write to example XML file</param>
    ''' <remarks></remarks>
    Sub WriteNewKeywordsList(ByVal XMLExamplePath As String, ByRef myXMLKeywordsList As List(Of String))
        Dim pathNode As String
        Dim nameListNode As String
        Dim propValue As String()

        Try
            If _xmlReaderWriter.InitializeXML(XMLExamplePath) Then
                pathNode = "//n:keywords"
                nameListNode = "keyword"
                propValue = myXMLKeywordsList.ToArray

                _xmlReaderWriter.WriteNodeListText(propValue, pathNode, nameListNode)
                _xmlReaderWriter.SaveXML(XMLExamplePath)
                _xmlReaderWriter.CloseXML()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Files & Folder Operations"
    '===Convert Folder & File Names Operations
    ''' <summary>
    ''' Renames various files and folders, and changes internal properties and renames other files where necessary to keep the examples in sync.
    ''' </summary>
    ''' <param name="myPath">Path to the model XML</param>
    ''' <param name="ToModelFileName">True: New name is that specified in "secondary_id" node of the model XML. False: New name is that specidied in "id" node.</param>
    ''' <param name="ConvertXMLFile">True: XML file will be renamed</param>
    ''' <param name="ConvertModelFile">True: Model file will be renamed, and any synced XML files will be updated and renamed.</param>
    ''' <param name="ConvertFolder">True: Parent directory folder will be renamed. Only use this if the path references a models database-style directory arrangement</param>
    ''' <remarks></remarks>
    Sub ConvertXMLs(ByVal myPath As String, ByVal ToModelFileName As Boolean, ByVal ConvertXMLFile As Boolean, ByVal ConvertModelFile As Boolean, ByVal ConvertFolder As Boolean)
        Dim nameNew As String = ""
        Dim myPathNode As String
        Dim nameFolderNew As String
        Dim nameModelFileOld As String = ""

        Dim renameContinue As Boolean = True
        Dim modelsSubFolder As Boolean = True
        Dim renameParentFolder As Boolean = False

        'Set new name for conversion
        'Get new name
        If ToModelFileName Then
            myPathNode = "//n:model/n:id_secondary"
            _xmlReaderWriter.GetSingleXMLNodeValue(myPath, myPathNode, nameNew)
            nameFolderNew = nameNew

            'If secondary ID does not exist or is empty, the example should be skipped
            If String.IsNullOrEmpty(nameNew) Then Exit Sub
        Else
            myPathNode = "//n:model/n:id"
            _xmlReaderWriter.GetSingleXMLNodeValue(myPath, myPathNode, nameNew)
            nameFolderNew = "id" & nameNew
        End If

        'Adjust for decimals
        'nameNew = ReplaceStringInName(nameNew, ".", "_")
        'nameFolderNew = ReplaceStringInName(nameFolderNew, ".", "_")

        'Set up additional model file and folder parameters
        If ConvertModelFile Or ConvertFolder Then
            CheckFolderInDBStructure(myPath, renameContinue, modelsSubFolder, ConvertModelFile, ConvertFolder)  'Checks if model is in database structure, which affects renaming
            If renameContinue Then nameModelFileOld = GetFileNameInXML(myPath) 'Gets old filename to be used for both operations, to keep them in sync without conflicts
        End If

        'Apply specified conversions
        If renameContinue Then
            If ConvertModelFile Then RenameModelFiles(myPath, nameNew, modelsSubFolder, nameModelFileOld)
            If ConvertXMLFile Then RenameXMLFiles(myPath, nameNew)
            If ConvertFolder Then RenameFolders(myPath, nameFolderNew, nameModelFileOld)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Checks if folder structure containing the model XML is of the database organization type
    ''' </summary>
    ''' <param name="p_path">Path to the model XML</param>
    ''' <param name="p_renameContinue">True: Renaming process will continue.</param>
    ''' <param name="p_modelsSubFolder">True: Model lies within subfolder 'Models',. False: Model lies at same location as model XML</param>
    ''' <param name="p_convertModelFile">True: Model file conversions will be carried out</param>
    ''' <param name="p_convertFolder">True: Parent folder will be renamed</param>
    ''' <remarks></remarks>
    Sub CheckFolderInDBStructure(ByVal p_path As String,
                                 ByRef p_renameContinue As Boolean,
                                 ByRef p_modelsSubFolder As Boolean,
                                 ByVal p_convertModelFile As Boolean,
                                 ByRef p_convertFolder As Boolean)
        Dim pathParentDir As String
        Dim foldersList As New List(Of String)
        Dim parentFolderName As String

        pathParentDir = GetPathDirectoryStub(p_path)
        foldersList = ListFoldersInFolder(pathParentDir)
        p_renameContinue = False

        For Each myFolder As String In foldersList
            parentFolderName = GetSuffix(myFolder, "\")
            If (StringsMatch(parentFolderName, DIR_NAME_ATTACHMENTS_DEFAULT) OrElse
                StringsMatch(parentFolderName, DIR_NAME_MODELS_DEFAULT) OrElse
                StringsMatch(parentFolderName, DIR_NAME_FIGURES_DEFAULT)) Then

                p_renameContinue = True
            End If
        Next
        'If folder structure is not of the database organization
        If Not p_renameContinue Then
            p_renameContinue = True
            'prompt user as to location of model file
            If p_convertModelFile Then
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Warning),
                                            PROMPT_RENAME_MODEL_FILE,
                                            TITLE_RENAME_MODEL_FILE)
                    Case eMessageActions.No
                        p_modelsSubFolder = False
                    Case eMessageActions.Yes
                        p_modelsSubFolder = True
                    Case eMessageActions.Cancel
                        p_renameContinue = False
                End Select
            End If
            'prompt user if the parent folder name should be overwritten or not renamed
            If p_convertFolder Then
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Warning),
                                            PROMPT_RENAME_PARENT_FOLDER,
                                            TITLE_RENAME_PARENT_FOLDER)
                    Case eMessageActions.No
                        p_convertFolder = False
                    Case eMessageActions.Yes
                        p_convertFolder = True
                    Case eMessageActions.Cancel
                        p_renameContinue = False
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Renames the model XML file to the name of the either the 'secondary ID' or 'ID' listed in the model XML
    ''' </summary>
    ''' <param name="myPath">Path to the model XML</param>
    ''' <param name="myName">Name to rename the model XML file to</param>
    ''' <remarks></remarks>
    Sub RenameXMLFiles(ByVal myPath As String, ByVal myName As String)
        myName = myName & ".xml"

        'Change XML file name if different
        If Not GetSuffix(myPath, "\") = myName Then RenameFile(myPath, myName)
    End Sub

    ''' <summary>
    ''' Gets the current model name recorded in the model XML. 
    ''' </summary>
    ''' <param name="myPath">Path to the model XML.</param>
    ''' <returns>Current model name recorded in the model XML</returns>
    ''' <remarks></remarks>
    Function GetFileNameInXML(ByVal myPath As String) As String
        'Old filename
        Dim pathNode As String
        Dim nameModelOld As String = ""

        pathNode = "//n:model/n:path"

        'Gets file name
        _xmlReaderWriter.GetSingleXMLNodeValue(myPath, pathNode, nameModelOld, , True)   'Get the old relative file path
        nameModelOld = GetSuffix(nameModelOld, "\")

        Return nameModelOld
    End Function

    ''' <summary>
    ''' Updates the model path in the model control XML file.
    ''' </summary>
    ''' <param name="p_pathXML">>Path to the model XML.</param>
    ''' <param name="p_adjustPathStub">Relative path stub for file location. Make blank if model is at same level as model control XML.</param>
    ''' <param name="p_nameModelOld">Optional: Name of the model. If not provided, it is gathered automatically from a separate XML query.</param>
    ''' <remarks></remarks>
    Sub SetFileNamePathInXML(ByVal p_pathXML As String,
                             ByVal p_adjustPathStub As String,
                             Optional ByVal p_nameModelOld As String = "")
        'Adjusts file name path, if specified
        Dim pathNode As String
        pathNode = "//n:model/n:path"

        If String.IsNullOrEmpty(p_nameModelOld) Then p_nameModelOld = GetFileNameInXML(p_pathXML)

        If Not String.IsNullOrEmpty(p_adjustPathStub) Then p_adjustPathStub = TrimPathSlash(p_adjustPathStub) & "\"
        _xmlReaderWriter.WriteSingleXMLNodeValue(p_pathXML, pathNode, p_adjustPathStub & p_nameModelOld)
    End Sub

    ''' <summary>
    ''' Updates the model XML to contain the updated model name
    ''' </summary>
    ''' <param name="myPath">Path to the model XML</param>
    ''' <param name="myValueNew">New name of the model. May include 'Models' folder prefix.</param>
    ''' <remarks></remarks>
    Sub SetFileNameInXML(ByVal myPath As String, ByVal myValueNew As String)
        Dim pathNode As String
        Dim nameModelOld As String = ""

        pathNode = "//n:model/n:path"
        _xmlReaderWriter.WriteSingleXMLNodeValue(myPath, pathNode, myValueNew)       'Update model XML entry of the model name reference  
    End Sub

    ''' <summary>
    ''' Renames model file to the name of the either the 'secondary ID' or 'ID' listed in the model XML. Updates corresponding XML files.
    ''' </summary>
    ''' <param name="p_path">Path to the model XML.</param>
    ''' <param name="p_name">Name to rename the model file to.</param>
    ''' <param name="p_modelsSubFolder">True: Model stored in a subfolder 'Models'. False: Model is assumed to be at the same location as the model XML file.</param>
    ''' <param name="p_nameModelOld">Used to create old filename path. If not provided, the filename listed in the control XML is used.</param>
    ''' <remarks></remarks>
    Sub RenameModelFiles(ByVal p_path As String,
                         ByVal p_name As String,
                         ByVal p_modelsSubFolder As Boolean,
                         Optional ByVal p_nameModelOld As String = "")
        Dim nameModelNew As String
        Dim pathDirectory As String
        Dim pathFile As String

        Try
            'Old Names
            pathDirectory = GetPathDirectoryStub(p_path)
            If p_modelsSubFolder Then pathDirectory = pathDirectory & "\" & DIR_NAME_MODELS_DEFAULT

            If String.IsNullOrEmpty(p_nameModelOld) Then p_nameModelOld = GetFileNameInXML(p_path) 'Get old filename if not supplied
            pathFile = pathDirectory & "\" & p_nameModelOld                       'Create old filename path

            'New Names
            nameModelNew = p_name & "." & GetSuffix(p_nameModelOld, ".")          'Create new filename
            If p_modelsSubFolder Then nameModelNew = "\" & DIR_NAME_MODELS_DEFAULT & "\" & nameModelNew
            SetFileNameInXML(p_path, nameModelNew)                                   'Update model XML entry of the model name reference       

            'Change model file name if different
            If Not GetSuffix(pathFile, "\") = GetSuffix(nameModelNew, "\") Then RenameFile(pathFile, GetSuffix(nameModelNew, "\"))

            'Check if "Output Settings" XML Exists at same level, and if so, update the file
            cPathOutputSettings.SyncOutputSettingsFilesToModelName(pathDirectory, p_name)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Renames the parent folder. User is prompted whether to continue if the parent folder is not of a database structure, and if the existing name does not contain either the new or old name segment.
    ''' </summary>
    ''' <param name="myPath">Path to the model XML</param>
    ''' <param name="myName">Name to rename the parent folder to</param>
    ''' <remarks></remarks>
    Sub RenameFolders(ByVal myPath As String, ByVal myName As String, Optional ByVal nameModelOld As String = "")
        Dim pathParentDir As String
        Dim parentFolder As String
        Dim newNameFolder As String
        'Dim msgBoxPrompt As String

        Try
            pathParentDir = GetPathDirectoryStub(myPath)
            parentFolder = GetSuffix(pathParentDir, "\")
            If String.IsNullOrEmpty(nameModelOld) Then nameModelOld = GetFileNameInXML(myPath) 'Get old filename if not supplied
            nameModelOld = GetPrefix(nameModelOld, ".")

            'Determine New Folder Name
            'Only the model name/id portion is overwritten in compound names

            'TODO: Get prior id or id_secondary for the below search to work.
            'newNameFolder = ReplaceStringInName(parentFolder, nameModelOld, myName)

            'If newNameFolder = parentFolder Then    'If segment never found
            '    'prompt user if the parent folder name should be overwritten or not renamed
            '    newNameFolder = myName
            '    msgBoxPrompt = "Folder name does not include the example name or model ID. Renaming will replace the entire parent folder name with the specified name. Do you wish to continue?"
            '    Select Case MessageBox.Show(msgBoxPrompt, "Rename Parent Folder", MessageBoxButton.YesNo, MessageBoxImage.Warning)
            '        Case MessageBoxResult.No
            '            Exit Sub
            '        Case MessageBoxResult.Yes
            '    End Select
            'End If

            'TODO: Temp until above is uncommented
            newNameFolder = myName

            'Change Parent Folder Name if different
            If Not GetSuffix(pathParentDir, "\") = newNameFolder Then RenameFolder(pathParentDir, newNameFolder)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    '===Flatten Folder Operations
    ''' <summary>
    ''' Takes an example, assumed to be stored in an example directory as a database structure, and copies all associated files into a flattened structure in the parent directory of the original example directory.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <remarks></remarks>
    Sub FlattenDirectory(ByVal myXMLPath As String)
        Dim modelXMLName As String
        Dim modelName As String
        Dim modelExtension As String

        Dim examplePathSourceStub As String
        Dim examplePathDestinationStub As String

        Dim cursorWait As New cCursorWait

        Try
            'Compile Model Names & Paths
            modelXMLName = GetSuffix(myXMLPath, "\")

            'Get path to example directory
            examplePathSourceStub = FilterStringFromName(myXMLPath, "\" & modelXMLName, True, False)

            'Back up one directory to get the path to the parent directory for examples
            examplePathDestinationStub = FilterStringFromName(myXMLPath, "\" & GetSuffix(examplePathSourceStub, "\"), True, False)

            'Get model file name
            modelName = GetFileNameInXML(myXMLPath)
            modelExtension = GetSuffix(modelName, ".")

            'Update XML for file move
            SetFileNamePathInXML(myXMLPath, "", modelName)

            'Copy synched model files
            CopyModel(myXMLPath, examplePathSourceStub, examplePathDestinationStub, modelName, eXMLEditorAction.DirectoriesFlatten)
            CopyOutputSettings(myXMLPath, examplePathSourceStub, examplePathDestinationStub, modelName, modelExtension, eXMLEditorAction.DirectoriesFlatten)
            CopySupportFiles(myXMLPath, examplePathSourceStub, examplePathDestinationStub, eXMLEditorAction.DirectoriesFlatten)
            CopyModelXML(myXMLPath, examplePathSourceStub, examplePathDestinationStub, modelXMLName)                                'This should come last as the 'CopySupportFiles' function makes changes to the XML file.

            'Delete example database directory
            DeleteAllFilesFolders(examplePathSourceStub, True, , True)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        cursorWait.EndCursor()
    End Sub

    '===Database Folder Create/Move Operations
    ''' <summary>
    ''' Takes an example stored in a flattened directory structure, and copies all associated files into an example directory as a database structure created within the flattened directory.
    ''' Returns 'True' if successful.
    ''' </summary>
    ''' <param name="p_pathXML">Path to the example XML file.</param>
    ''' <param name="p_deleteOriginal">True: Files remaining in parent folder will be deleted after creating the database structure for examples. False: Original files will be left as is.</param>
    ''' <param name="p_exampleName">If specified, the directory name and moved model file name will use this name instead of the existing model file name.</param>
    ''' <remarks></remarks>
    Friend Function GatherDBDirectoryFromFlattened(ByVal p_pathXML As String,
                                                   Optional ByVal p_deleteOriginal As Boolean = False,
                                                   Optional ByVal p_exampleName As String = "") As Boolean
        Dim cursorWait As New cCursorWait
        GatherDBDirectoryFromFlattened = False

        Try
            Dim modelXMLName As String = GetSuffix(p_pathXML, "\")
            Dim examplePathSourceStub As String = FilterStringFromName(p_pathXML, "\" & modelXMLName, True, False)
            Dim modelFileName As String = GetFileNameInXML(p_pathXML)
            Dim modelExtension As String = GetSuffix(modelFileName, ".")

            If String.IsNullOrEmpty(p_exampleName) Then p_exampleName = FilterStringFromName(modelFileName, "." & modelExtension, True, False)

            'Create new directory named after the example, with internal database structure
            If CreateDatabaseDirectory(examplePathSourceStub, p_exampleName) Then
                'Set example directory destination path
                Dim examplePathDestinationStub As String = examplePathSourceStub & "\" & p_exampleName

                'Update XML for file move
                SetFileNamePathInXML(p_pathXML, DIR_NAME_MODELS_DEFAULT, modelFileName)

                'Copy synced model files & delete flattened files outside of examples database directories
                CopyModel(p_pathXML, examplePathSourceStub, examplePathDestinationStub, modelFileName, eXMLEditorAction.DirectoriesDBGather, True)
                CopyOutputSettings(p_pathXML, examplePathSourceStub, examplePathDestinationStub, modelFileName, modelExtension, eXMLEditorAction.DirectoriesDBGather, True)
                CopySupportFiles(p_pathXML, examplePathSourceStub, examplePathDestinationStub, eXMLEditorAction.DirectoriesDBGather)                                                'Note: Original support files will not be deleted, as they might be referenced by several examples

                CopyModelXML(p_pathXML, examplePathSourceStub, examplePathDestinationStub, modelXMLName, True)                              'This should come last as the 'CopySupportFiles' function makes changes to the XML file.

                'If specified, all remaining files in the parent directory will be deleted
                If p_deleteOriginal Then DeleteFiles(examplePathSourceStub, False, , , True)

                GatherDBDirectoryFromFlattened = True
            Else
                GatherDBDirectoryFromFlattened = False
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        cursorWait.EndCursor()
    End Function

    ''' <summary>
    ''' Creates a database structure directory system for a given example name within a specified parent folder
    ''' </summary>
    ''' <param name="p_baseDir">Parent folder within which to create the database directory</param>
    ''' <param name="p_exampleName">Example name by which to name the database directory</param>
    ''' <remarks></remarks>
    Public Shared Function CreateDatabaseDirectory(ByVal p_baseDir As String,
                                            ByVal p_exampleName As String) As Boolean

        Dim dirPath As String = p_baseDir & "\" & p_exampleName

        If IO.Directory.Exists(dirPath) Then
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Exclamation),
                                                "'{1}' cannot be gathered into a database structure because the following directory already exists: " &
                                                "{0}{0} {2} {0}{0} " &
                                                "Would you like to replace the existing directory? If not, example '{1}' will be skipped.",
                                                "Directory Conflict",
                                                 Environment.NewLine, p_exampleName, dirPath)
                Case eMessageActions.Yes
                    DeleteAllFilesFolders(dirPath, True)
                Case eMessageActions.No
                    Return False
            End Select
        End If

        ComponentCreateDirectory(dirPath)
        ComponentCreateDirectory(dirPath & "\" & cPathModel.DIR_NAME_MODELS_DEFAULT)
        ComponentCreateDirectory(dirPath & "\" & cPathAttachment.DIR_NAME_ATTACHMENTS_DEFAULT)
        ComponentCreateDirectory(dirPath & "\" & cPathAttachment.DIR_NAME_FIGURES_DEFAULT)

        Return True
    End Function

    ''' <summary>
    ''' For each XML file in the specified directory, runs the 'Mirror XML' file function to generate a mirrored class, and adds it to a collection.
    ''' </summary>
    ''' <param name="myXMLPathList">Optional list of paths to gather for creating the list of XML class objects.</param>
    ''' <param name="ReplaceExisting">Optional specification of whether the routine is creating a new list of objects (True), or adding to the existing list (False).</param>
    ''' <remarks></remarks>
    Public Sub MirrorAllEditorXMLS(Optional ByVal myXMLPathList As List(Of String) = Nothing,
                                          Optional ByVal ReplaceExisting As Boolean = True)
        If myXMLPathList Is Nothing Then myXMLPathList = myCsiTester.suiteXMLPathList

        If ReplaceExisting Then myXMLEditor.suiteEditorXMLObjects.Clear()

        For Each myXMLpath As String In myXMLPathList
            myXMLEditor.suiteEditorXMLObjects.Add(_xmlReaderWriter.MirrorXMLElementsAll(myXMLpath))
        Next

    End Sub

    '===Create New Source Directory from Destination Models
    ''' <summary>
    ''' Performs all actions necessary to make a new Models Source Folder with model files from the Destination Directory. 
    ''' Optionally the outputSettings file at the Destination can be added, and the overall activation of outputSettings files can be specified. 
    ''' </summary>
    ''' <param name="pathNewSource">Path to the new model source directory.</param>
    ''' <param name="useModelSource">If True, the model source outputSettings XML files will be used.</param>
    ''' <param name="activateOutputSettings">Used to specify whether to leave the files 'as-is', or do a global activation/deactivation of outputSettings files that are not supporting files.</param>
    ''' <remarks></remarks>
    Function CreateNewModelSourceFromDestination(ByVal pathNewSource As String, ByVal useModelSource As Boolean, ByVal activateOutputSettings As eOutputSettingsActivation) As Boolean
        Dim backupSuffix As String = "_Backup"
        Dim pathModelFileDest As String = ""
        Dim pathXmlOutputSettingsSource As String = ""

        CreateNewModelSourceFromDestination = True

        'Make backup copy of Model Source
        CopyFolder(myRegTest.models_database_directory.path, myRegTest.models_database_directory.path & backupSuffix, True)

        Try
            'TODO: Status bar here in the future
            Dim cursorWait As New cCursorWait

            'Prepare Model Source Folder
            For Each myExampleTestSet As cExampleTestSet In examplesTestSetList
                If Not (StringsMatch(myExampleTestSet.exampleClassification, GetEnumDescription(eTestSetClassification.FailedExamples))) Then
                    For Each myExample As cExample In myExampleTestSet.examplesList
                        With myExample
                            If Not useModelSource Then 'Update OutputSettings file at Source Folder from Destination Folder
                                CopyFile(.pathXmlOutputSettingsDest, .pathXmlOutputSettingsSrc, True)

                                If activateOutputSettings = eOutputSettingsActivation.AsIs Then 'Clear Source Folder 
                                    .DeactivateOutPutSettingsXMLFile(True)

                                    If .outputSettingsUsed Then         'Copy over active outputSettings file
                                        pathXmlOutputSettingsSource = GetPathDirectoryStub(myCsiTester.ConvertPathModelSourceToDestination(.pathModelFile)) & "\" & GetPathFileName(.pathXmlOutputSettingsDest)
                                        CopyFile(pathXmlOutputSettingsSource, GetPathDirectoryStub(.pathModelFile) & "\" & GetPathFileName(.pathXmlOutputSettingsSrc), True)
                                    End If
                                End If

                                'Merge Activated OutputSettings files over to those in the Source folder if they are not going to be cleared
                                If Not activateOutputSettings = eOutputSettingsActivation.Deactivate Then

                                End If

                            End If

                            'Perform activation/deactivation action at Source Folder
                            Select Case activateOutputSettings
                                Case eOutputSettingsActivation.AsIs 'No action
                                Case eOutputSettingsActivation.Activate : .ActivateOutputSettingsXMLFile("", False, True)
                                Case eOutputSettingsActivation.Deactivate : .DeactivateOutPutSettingsXMLFile(True)
                            End Select

                            'Update model file at Source Folder from Destination Folder
                            pathModelFileDest = myCsiTester.ConvertPathModelSourceToDestination(.pathModelFile)

                            'Merge Destination models over to those in the new folder
                            CopyFile(pathModelFileDest, .pathModelFile, True)
                        End With
                    Next
                End If
            Next

            cursorWait.EndCursor()

            'Copy Model Source contents to new folder
            CopyFolder(myRegTest.models_database_directory.path, pathNewSource, True)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            CreateNewModelSourceFromDestination = False
        Finally
            'Delete Modified Models Source Folder
            DeleteAllFilesFolders(myRegTest.models_database_directory.path, True, , True)

            'Recreate Models Source Folder from Backup
            CopyFolder(myRegTest.models_database_directory.path & backupSuffix, myRegTest.models_database_directory.path, True)

            'Delete Backup Folder
            DeleteAllFilesFolders(myRegTest.models_database_directory.path & backupSuffix, True, , True)
        End Try

    End Function

    '===Update Model Operations
    ''' <summary>
    ''' Updates the path of a specified attachment item.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the model control XML file.</param>
    ''' <param name="examplePathDestination">New path to associate with the attachment item.</param>
    ''' <param name="tagAttachment">Tag that uniquely identifies the attachment of an official type. Other strings can also be used, but results might not be precise if more than one title contains the string.</param>
    ''' <remarks></remarks>
    Sub UpdateAttachmentsPath(ByVal myXMLPath As String, ByVal examplePathDestination As String, ByVal tagAttachment As String)
        Dim tagName As String = "attachment"
        Dim tagNameLookupComponent As String = "n:" & "title"
        Dim tagNameWriteComponent As String = "n:" & "path"

        If _xmlReaderWriter.InitializeXML(myXMLPath) Then
            _xmlReaderWriter.UpdateObjectByTag(tagName, tagNameLookupComponent, tagNameWriteComponent, tagAttachment, examplePathDestination, True)
            _xmlReaderWriter.CloseXML()
        End If
    End Sub

    ''' <summary>
    ''' Copies models from a specified directory source over to the current model set, replacing the existing models.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the example XML file.</param>
    ''' <param name="examplePathSource">Path to the model file to be copied.</param>
    ''' <remarks></remarks>
    Sub UpdateModel(ByVal myXMLPath As String, ByVal examplePathSource As String)
        Dim modelName As String
        Dim modelExtension As String
        Dim modelOldPath As String = ""
        Dim modelOldPathStub As String
        Dim sourcePath As String
        Dim sourcePathCollection As New List(Of String)

        Try
            'Get model file name
            modelName = GetFileNameInXML(myXMLPath)
            modelExtension = GetSuffix(modelName, ".")
            modelName = FilterStringFromName(modelName, "." & modelExtension, True, False)

            'Get old model file path
            _xmlReaderWriter.GetSingleXMLNodeValue(myXMLPath, "//n:model/n:path", modelOldPath, , True)
            modelOldPathStub = FilterStringFromName(myXMLPath, GetSuffix(myXMLPath, "\"), True, False)
            modelOldPath = modelOldPathStub & modelOldPath

            'Get new model file path of the model file specified in the model control XML
            sourcePathCollection = ListFilePathsInDirectory(examplePathSource, True, modelName, modelExtension, False)

            If sourcePathCollection IsNot Nothing Then
                If sourcePathCollection.Count = 1 Then
                    sourcePath = sourcePathCollection(0)

                    'Copy new model file from specified source to the destination folder directory
                    CopyFile(sourcePath, modelOldPath, True, True)
                ElseIf sourcePathCollection.Count > 1 Then
                    Dim tempPathOldStub As String = GetPathDirectoryStub(modelOldPath)
                    Dim tempPathStub As String

                    If Not myCsiTester.IsDirectoryFlattened(myXMLPath) Then     'back up from 'models' folder, back up from '{example name/id} folder
                        tempPathOldStub = GetPathDirectorySubStub(tempPathOldStub, 2)
                    End If

                    For Each sourcePathCurrent As String In sourcePathCollection
                        tempPathStub = GetPathDirectoryStub(sourcePathCurrent)
                        If GetSuffix(tempPathStub, "\") = GetSuffix(tempPathOldStub, "\") Then
                            'Copy new model file from specified source to the destination folder directory
                            CopyFile(sourcePathCurrent, modelOldPath, True, True)
                            Exit For
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Exit Sub
        End Try
    End Sub

    ''' <summary>
    ''' Renames all model control XML files to include the suffix, if missing.
    ''' </summary>
    ''' <param name="myPath">Path to the parent folder, within which all files will be renamed.</param>
    ''' <remarks></remarks>
    Sub RenameMCXmlFilesAddSuffix(ByVal myPath As String)
        Dim xmlFilesList As New List(Of String)
        Dim newXmlFileName As String

        Try
            xmlFilesList = ListFilePathsInDirectory(myPath, True, , ".xml")                 'Gets list of XML paths
            For Each xmlFilePath As String In xmlFilesList
                newXmlFileName = ""
                If (Not StringExistInName(xmlFilePath, cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML) AndAlso
                    Not StringExistInName(xmlFilePath, cPathModelControl.FILE_NAME_SUFFIX_MC_XML)) Then      'If identifying parts of filenames match

                    newXmlFileName = GetPathFileName(xmlFilePath, True) & cPathModelControl.FILE_NAME_SUFFIX_MC_XML
                    RenameFile(xmlFilePath, newXmlFileName)
                End If
            Next
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub


    '===Supporting Copying Operations
    ''' <summary>
    ''' Copies the model control XML to a new directory.
    ''' </summary>
    ''' <param name="p_xmlPath">Path to the example XML file.</param>
    ''' <param name="p_examplePathSourceStub">Path to the example directory.</param>
    ''' <param name="p_examplePathDestinationStub">Path to the example parent directory.</param>
    ''' <param name="p_modelXMLName">Name of the model control XMl file.</param>
    ''' <param name="p_deleteOriginal">Optional: True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    ''' <remarks></remarks>
    Sub CopyModelXML(ByVal p_xmlPath As String,
                     ByVal p_examplePathSourceStub As String,
                     ByVal p_examplePathDestinationStub As String,
                     ByVal p_modelXMLName As String,
                     Optional ByVal p_deleteOriginal As Boolean = False)
        Dim examplePathSource As String
        Dim examplePathDestination As String

        examplePathSource = p_xmlPath
        examplePathDestination = p_examplePathDestinationStub & "\" & p_modelXMLName

        MoveFile(examplePathSource, examplePathDestination, p_deleteOriginal)

    End Sub

    ''' <summary>
    ''' Copies example model file to a new directory.
    ''' </summary>
    ''' <param name="p_xmlPath">Path to the example XML file.</param>
    ''' <param name="p_examplePathSourceStub">Path to the example directory.</param>
    ''' <param name="p_examplePathDestinationStub">Path to the example parent directory.</param>
    ''' <param name="p_modelName">Name of the example model file.</param>
    ''' <param name="p_moveAction">Specifies whether or not the files are being flattened or moved into a database structure.</param>
    ''' <param name="p_deleteOriginal">Optional: True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    ''' <remarks></remarks>
    Sub CopyModel(ByVal p_xmlPath As String,
                  ByVal p_examplePathSourceStub As String,
                  ByVal p_examplePathDestinationStub As String,
                  ByVal p_modelName As String,
                  ByRef p_moveAction As eXMLEditorAction,
                  Optional ByVal p_deleteOriginal As Boolean = False)
        Dim pathFlattened As String = "\" & p_modelName
        Dim pathDB As String = "\" & DIR_NAME_MODELS_DEFAULT & "\" & p_modelName

        Try
            Select Case p_moveAction
                Case eXMLEditorAction.DirectoriesFlatten
                    pathFlattened = p_examplePathDestinationStub & pathFlattened
                    pathDB = p_examplePathSourceStub & pathDB

                    MoveFile(pathDB, pathFlattened, p_deleteOriginal)
                Case eXMLEditorAction.DirectoriesDBGather
                    pathFlattened = p_examplePathSourceStub & pathFlattened
                    pathDB = p_examplePathDestinationStub & pathDB

                    MoveFile(pathFlattened, pathDB, p_deleteOriginal)
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Copies example outputsettings XML file to a new directory.
    ''' </summary>
    ''' <param name="p_xmlPath">Path to the example XML file.</param>
    ''' <param name="p_examplePathSourceStub">Path to the example directory.</param>
    ''' <param name="p_examplePathDestinationStub">Path to the example parent directory.</param>
    ''' <param name="p_modelName">Name of the example model file.</param>
    ''' <param name="p_modelExtension">File Extension of the example model file.</param>
    ''' <param name="p_copyAction">Specifies whether or not the files are being flattened or moved into a database structure.</param>
    ''' <param name="p_deleteOriginal">True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    ''' <remarks></remarks>
    Sub CopyOutputSettings(ByVal p_xmlPath As String,
                           ByVal p_examplePathSourceStub As String,
                           ByVal p_examplePathDestinationStub As String,
                           ByVal p_modelName As String, _
                           ByVal p_modelExtension As String,
                           ByRef p_copyAction As eXMLEditorAction,
                           Optional ByVal p_deleteOriginal As Boolean = False)
        Dim pathAttachments As String = ""
        Dim outputSettingsXMLName As String
        Dim pathFlattened As String = ""
        Dim pathDB As String = ""

        Try


            'Change XML file name to stay in sync with new model name, if different
            'Try with import tag first, if the original file does not exist, change the file name.
            outputSettingsXMLName = FilterStringFromName(p_modelName, "." & p_modelExtension, True, False) & testerSettings.outputSettingsVersionSession & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
            If Not IO.File.Exists(GetPathDirectoryStub(p_xmlPath) & "\" & DIR_NAME_MODELS_DEFAULT & "\" & outputSettingsXMLName) Then
                outputSettingsXMLName = FilterStringFromName(p_modelName, "." & p_modelExtension, True, False) & cPathOutputSettings.FILE_NAME_SUFFIX_OUTPUT_SETTINGS_XML
            End If

            pathFlattened = "\" & outputSettingsXMLName
            pathDB = "\" & DIR_NAME_MODELS_DEFAULT & "\" & outputSettingsXMLName

            Select Case p_copyAction
                Case eXMLEditorAction.DirectoriesFlatten
                    'Handle XML at model location, if it exists
                    pathFlattened = p_examplePathDestinationStub & pathFlattened
                    pathDB = p_examplePathSourceStub & pathDB

                    MoveFile(pathDB, pathFlattened, p_deleteOriginal)

                    'Handle XML at attachments location, if it exists
                    If _preserveOutputSettingsAttachment Then
                        MoveFile(pathDB, pathFlattened, p_deleteOriginal)
                        UpdateAttachmentsPath(p_xmlPath, pathFlattened, TAG_ATTACHMENT_TABLE_SET_FILE)
                    End If
                Case eXMLEditorAction.DirectoriesDBGather
                    'Handle XML at model location, if it exists
                    pathFlattened = p_examplePathSourceStub & pathFlattened
                    pathDB = p_examplePathDestinationStub & pathDB

                    MoveFile(pathFlattened, pathDB, p_deleteOriginal)

                    'Handle XML at attachments location, if it exists
                    If CheckAttachment(p_xmlPath, TAG_ATTACHMENT_TABLE_SET_FILE) Then
                        pathAttachments = p_examplePathDestinationStub & "\" & DIR_NAME_ATTACHMENTS_DEFAULT & "\" & outputSettingsXMLName
                        MoveFile(pathFlattened, pathAttachments, p_deleteOriginal)
                        UpdateAttachmentsPath(p_xmlPath, pathAttachments, TAG_ATTACHMENT_TABLE_SET_FILE)
                    End If
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Checks the list of attachments for a specified model control XML file and returns 'True' if the attachment exists. This is done by title, but paths can also be checked.
    ''' </summary>
    ''' <param name="myXMLPath">Path to the model control XML file.</param>
    ''' <param name="tagAttachment">Tag that uniquely identifies the attachment of an official type. Other strings can also be used, but results might not be precise if more than one title contains the string.</param>
    ''' <param name="checkPath">Optional: If True, the path is checked. If False (default), the title is checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckAttachment(ByVal myXMLPath As String, ByVal tagAttachment As String, Optional ByVal checkPath As Boolean = False) As Boolean
        Dim attachmentsTitleList As New List(Of String)
        Dim tagName As String = "attachment"
        Dim tagNameComponent As String

        If checkPath Then
            tagNameComponent = "n:" & "path"
        Else
            tagNameComponent = "n:" & "title"
        End If

        If _xmlReaderWriter.InitializeXML(myXMLPath) Then
            _xmlReaderWriter.ReadXmlObjectText(tagName, tagNameComponent, attachmentsTitleList, True)
            _xmlReaderWriter.CloseXML()
        End If

        For Each attachmentTitle As String In attachmentsTitleList
            If StringExistInName(attachmentTitle, tagAttachment) Then Return True
        Next

        Return False
    End Function

    ''' <summary>
    ''' Copies all example support files to a new directory
    ''' </summary>
    ''' <param name="p_xmlPath">Path to the example XML file.</param>
    ''' <param name="p_examplePathSourceStub">Path to the example directory.</param>
    ''' <param name="p_examplePathDestinationStub">Path to the example parent directory.</param>
    ''' <param name="p_copyAction">Specifies whether or not the files are being flattened or moved into a database structure.</param>
    ''' <param name="p_deleteOriginal">True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    ''' <remarks></remarks>
    Sub CopySupportFiles(ByVal p_xmlPath As String,
                         ByVal p_examplePathSourceStub As String,
                         ByVal p_examplePathDestinationStub As String,
                         ByRef p_copyAction As eXMLEditorAction,
                         Optional ByVal p_deleteOriginal As Boolean = False)
        Dim examplePathSource As String = ""
        Dim examplePathDestination As String = ""

        Dim supportFileName As String
        Dim supportFiles As List(Of String)

        Try
            'Get the list of all file paths for supporting files & update XML for moving files
            supportFiles = GetExampleFilesSupportingInDBDirectory(p_xmlPath, p_copyAction)

            Select Case p_copyAction
                Case eXMLEditorAction.DirectoriesFlatten
                    For Each supportFilePath As String In supportFiles
                        'Get support file name
                        supportFileName = GetSuffix(supportFilePath, "\")

                        'Adjust relative paths to be absolute paths
                        examplePathSource = p_examplePathSourceStub & "\" & supportFilePath

                        'Set destination path
                        examplePathDestination = p_examplePathDestinationStub & "\" & supportFileName

                        MoveFile(examplePathSource, examplePathDestination, p_deleteOriginal)
                    Next
                Case eXMLEditorAction.DirectoriesDBGather
                    For Each supportFilePath As String In supportFiles
                        'Get support file name
                        supportFileName = GetSuffix(supportFilePath, "\")

                        'Adjust relative paths to be absolute paths
                        examplePathSource = p_examplePathSourceStub & "\" & supportFilePath

                        'Set destination path
                        examplePathDestination = p_examplePathDestinationStub & "\" & DIR_NAME_MODELS_DEFAULT & "\" & supportFileName

                        MoveFile(examplePathSource, examplePathDestination, p_deleteOriginal)
                    Next
            End Select
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Gets collection of file paths of the required supporting files for the example. Also updates the attachments path in the model control XML file.
    ''' </summary>
    ''' <param name="p_path">Path to the example XML file.</param>
    ''' <param name="p_copyAction">Specifies whether or not the files are being flattened or moved into a database structure.</param>
    ''' <returns>Collection of file paths to required supporting files.</returns>
    ''' <remarks></remarks>
    Function GetExampleFilesSupportingInDBDirectory(ByVal p_path As String,
                                                    ByRef p_copyAction As eXMLEditorAction) As List(Of String)

        Dim queryNodeName As String = "title"
        Dim queryNodeValue As String = TAG_ATTACHMENT_SUPPORTING_FILE
        Dim corrNodeName As String = "path"
        Dim pathNode As String = "//n:model/n:attachments"
        Dim xmlCSi As New cXMLCSi()

        Try
            Select Case p_copyAction
                Case eXMLEditorAction.DirectoriesFlatten
                    Return xmlCSi.GetXMLNodeObjectValuePairs(p_path, pathNode, queryNodeName, queryNodeValue, corrNodeName, p_copyAction)
                Case eXMLEditorAction.DirectoriesDBGather
                    Return xmlCSi.GetXMLNodeObjectValuePairs(p_path, pathNode, queryNodeName, queryNodeValue, corrNodeName, p_copyAction, DIR_NAME_MODELS_DEFAULT)
            End Select

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

#End Region
End Class
