Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If
Imports System.Timers
Imports ProgressiveCollapse.cEnumerations

''' <summary>
''' Main form for setting up and running the CSiProgessiveCollapse program.
''' </summary>
''' <remarks></remarks>
Public Class frmPCControl
    Private _Plugin As cPlugin

#Region "Variables"
    Dim isPlugin As Boolean
    Dim myTimer As New Timer()
    Dim defaultGroupKeyCheck As String
    Dim defaultGroupPath As String
    Dim defaultNonlinearStaticCase As String
#End Region

#Region "Initialization"
    Sub New()
        ProgCol = New cProgressiveCollapse  'Needs to come first
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        csiLogger = New cCSiTesterLogger
        MsgBox("Is 64-bit? " & Environment.Is64BitProcess.ToString)
        isPlugin = False

        SetDefaults()
        SetButtons()
        SetToolTips()

        'Question: Sometimes an error occurs on load as these elements are loaded before the objects are ready. How to resolve?
        'Currently add a try/catch, a delay, and a second attempt before closing
        Try
            PopulateComboBoxes()
        Catch ex As Exception
            System.Threading.Thread.Sleep(1000)
            Try
                PopulateComboBoxes()
            Catch ex2 As Exception
                Application.Exit()
            End Try
        End Try

        'TODO: Turn below on with timer, and end by timer
        'Me.TopMost = True
        'FormTimerTopMost()
    End Sub

#If COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
    Public Sub New(ByVal plugIn As cPlugin)
        ProgCol = New cProgressiveCollapse  'Needs to come first
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MsgBox("Is 64-bit? " & Environment.Is64BitProcess.ToString)
        _Plugin = plugIn
        isPlugin = True

        SapModel = _Plugin.sapModel

        SetDefaults()
        SetButtons()
        SetToolTips()

        'Question: Sometimes an error occurs on load as these elements are loaded before the objects are ready. How to resolve?
        'Currently add a try/catch, a delay, and a second attempt before closing
        Try
            PopulateComboBoxes()
        Catch ex As Exception
            System.Threading.Thread.Sleep(1000)
            Try
                PopulateComboBoxes()
            Catch ex2 As Exception
                Application.Exit()
            End Try
        End Try
    End Sub
#End If

    Sub SetDefaults()
        defaultGroupKeyCheck = ProgCol.keyElementGroup
        defaultGroupPath = ProgCol.criticalPathGroup
    End Sub

    Sub SetButtons()
        'Radio Buttons
        btnRad_Frame.Checked = True

        If ProgCol.findKeyFrame Then btnRad_CollapseInitiate.Checked = True
        If Not ProgCol.findKeyFrame Then btnRad_CollapsePropagate.Checked = True

        If ProgCol.DCMethod = eDCMethod.Strength Then
            btnRad_DCStrength.Checked = True

            'Disable propagation options
            btnRad_CollapseInitiate.Checked = True
            btnRad_CollapsePropagate.Enabled = False
            btnOptionsTrackProgCol.Enabled = False
        End If

        If ProgCol.DCMethod = eDCMethod.Ductility Then btnRad_DCDuctility.Checked = True

        btnRad_LoadComboGSA.Checked = True

        'Checkboxes
        CheckBox_FailedGroup.Checked = ProgCol.createFailedGroup
        CheckBox_SaveSeparate.Checked = ProgCol.saveSeparateModel

        'Buttons
        btnParametersSteel.Enabled = False
        btnParametersConcrete.Enabled = False
        btnParametersAluminum.Enabled = False
        btnParametersColdFormSteel.Enabled = False

        btnProgressiveCollapseRun.Enabled = EnableRunButton()

    End Sub

    ''' <summary>
    ''' Sets the tooltips for the form.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetToolTips()
        Me.ToolTip1.SetToolTip(Me.GrpBxKeyElement, ttKeyElement)
        Me.ToolTip1.SetToolTip(Me.btnRad_Frame, ttKeyElementFrame)
        Me.ToolTip1.SetToolTip(Me.ComboBox_KeyElementFrame, ttKeyElementFrame)
        Me.ToolTip1.SetToolTip(Me.btnRad_Group, ttKeyElementGroup)
        Me.ToolTip1.SetToolTip(Me.ComboBox_KeyElementGroup, ttKeyElementGroup)
        Me.ToolTip1.SetToolTip(Me.btnCreateGroupKeyCheck, ttKeyElementCreateGroup)

        Me.ToolTip1.SetToolTip(Me.GrpBxCriticalPath, ttCriticalPath)
        Me.ToolTip1.SetToolTip(Me.lblCriticalPathGrp, ttCriticalPathGroup)
        Me.ToolTip1.SetToolTip(Me.ComboBox_CriticalPathGroup, ttCriticalPathGroup)
        Me.ToolTip1.SetToolTip(Me.btnCreateGroupPath, ttCriticalPathCreateGroup)

        Me.ToolTip1.SetToolTip(Me.GrpBxDCMethod, ttMethodsDC)
        Me.ToolTip1.SetToolTip(Me.btnRad_DCStrength, ttStrength)
        Me.ToolTip1.SetToolTip(Me.btnRad_DCDuctility, ttDuctility)
        Me.ToolTip1.SetToolTip(Me.GrpBxMaterialTypeParams, ttMaterialTypeParameters)

        Me.ToolTip1.SetToolTip(Me.GrpBxAnalysisMethod, ttMethodAnalysis)
        Me.ToolTip1.SetToolTip(Me.btnRad_CollapseInitiate, ttCollapseInitiation)
        Me.ToolTip1.SetToolTip(Me.CheckBox_FailedGroup, ttCollapseInitiationGroup)
        Me.ToolTip1.SetToolTip(Me.btnRad_CollapsePropagate, ttCollapsePropagation)

        Me.ToolTip1.SetToolTip(Me.btnLoadCaseParameters, ttLoadingLoadCaseParameters)
        Me.ToolTip1.SetToolTip(Me.btnRad_LoadComboUser, ttLoadingCaseUser)
        Me.ToolTip1.SetToolTip(Me.ComboBox_LoadCombo, ttLoadingCaseUser)

        Me.ToolTip1.SetToolTip(Me.CheckBox_SaveSeparate, ttSaveIterationsModels)
    End Sub

    ''' <summary>
    ''' Checks criteria that determines whether or not the 'run' button is to be enabled.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function EnableRunButton() As Boolean
        EnableRunButton = False

        If CheckBox_SteelFrame.Checked = True Then EnableRunButton = True
        If CheckBox_ConcreteFrame.Checked = True Then EnableRunButton = True
        If CheckBox_AluminumFrame.Checked = True Then EnableRunButton = True
        If CheckBox_ColdFormSteelFrame.Checked = True Then EnableRunButton = True

    End Function

    Sub PopulateComboBoxes()

        PopulateComboBoxesKeyElementsFrame()
        PopulateComboBoxesKeyElementsGroup()

        PopulateComboBoxesCriticalPathGroup()

        PopulateComboBoxesLoadCases()

    End Sub


    Sub PopulateComboBoxesKeyElementsFrame()
        Dim NumberNames As Integer
        Dim framesListSAP() As String = Nothing

        'Clear combo box
        ComboBox_KeyElementFrame.Items.Clear()
        'get frame object names
        ret = SapModel.FrameObj.GetNameList(NumberNames, framesListSAP)
        retCheck(ret, "SapModel.FrameObj.GetNameList")

        ComboBox_KeyElementFrame.Items.AddRange(framesListSAP)
        ComboBox_KeyElementFrame.SelectedItem = ComboBox_KeyElementFrame.Items(0)
    End Sub

    Sub PopulateComboBoxesKeyElementsGroup()
        Dim NumberNames As Integer
        Dim myGroupName As String
        Dim groupsListKeySAP() As String = Nothing

        'Clear combo box
        ComboBox_KeyElementGroup.Items.Clear()

        'get group names of key elements & assign to combo box
        ret = SapModel.GroupDef.GetNameList(NumberNames, groupsListKeySAP)
        retCheck(ret, "SapModel.GroupDef.GetNameList")
        ComboBox_KeyElementGroup.Items.AddRange(groupsListKeySAP)

        'Check for existing default key elements group, and assign first selected item in combo box 
        myGroupName = CheckExistingGroups(groupsListKeySAP, ProgCol.keyElementGroup)
        If myGroupName = "" Then
            ComboBox_KeyElementGroup.SelectedItem = ComboBox_KeyElementGroup.Items(0)
        Else
            ComboBox_KeyElementGroup.SelectedItem = myGroupName
            btnCreateGroupKeyCheck.Enabled = False
        End If
    End Sub

    Sub PopulateComboBoxesCriticalPathGroup()
        Dim NumberNames As Integer
        Dim myGroupName As String
        Dim groupsListCriticalPathSAP() As String = Nothing

        'Clear combo box
        ComboBox_CriticalPathGroup.Items.Clear()

        'get group names of critical paths & assign to combo box
        ret = SapModel.GroupDef.GetNameList(NumberNames, groupsListCriticalPathSAP)
        retCheck(ret, "SapModel.GroupDef.GetNameList")
        ComboBox_CriticalPathGroup.Items.AddRange(groupsListCriticalPathSAP)

        'Check for existing default critical paths group, and assign first selected item in combo box 
        myGroupName = CheckExistingGroups(groupsListCriticalPathSAP, ProgCol.criticalPathGroup)
        If myGroupName = "" Then
            ComboBox_CriticalPathGroup.SelectedItem = ComboBox_CriticalPathGroup.Items(0)
        Else
            ComboBox_CriticalPathGroup.SelectedItem = myGroupName
            btnCreateGroupPath.Enabled = False
        End If

    End Sub

    Sub PopulateComboBoxesLoadCases()
        Dim myGroupName As String
        Dim myName() As String = Nothing

        'Clear combo box
        ComboBox_LoadCombo.Items.Clear()

        'Get list of load cases & assign to combo box
        GetLoadCases(myName)
        ComboBox_LoadCombo.Items.AddRange(myName)

        'Check for existing default load case, and assign first selected item in combo box 
        'TODO: Currently ProCol.loadCaseNLS is empty here, so the first item is always checked. This might be desired
        myGroupName = CheckExistingGroups(myName, ProgCol.loadCaseNLS)
        If myGroupName = "" Then
            ComboBox_LoadCombo.SelectedItem = ComboBox_LoadCombo.Items(0)
        Else
            ComboBox_LoadCombo.SelectedItem = myGroupName
        End If
    End Sub


    ''' <summary>
    ''' If  critical path group is defined, scan group and auto select material parameter buttons if frames of a given type exist in the group
    ''' </summary>
    ''' <remarks></remarks>
    Sub CheckMaterialTypesOfGroups()
        Dim myMatTypeList As New List(Of eMatType)

        CheckBox_SteelFrame.Checked = False
        CheckBox_ConcreteFrame.Checked = False
        CheckBox_AluminumFrame.Checked = False
        CheckBox_ColdFormSteelFrame.Checked = False

        Try
            GetGroupDesignTypes(CStr(ComboBox_CriticalPathGroup.SelectedItem), myMatTypeList)

            For Each matType As eMatType In myMatTypeList
                Select Case matType
#If COMPILE_SAP2000v16 Then
                    Case eMatType.MATERIAL_STEEL : CheckBox_SteelFrame.Checked = True
                    Case eMatType.MATERIAL_CONCRETE : CheckBox_ConcreteFrame.Checked = True
                    Case eMatType.MATERIAL_ALUMINUM : CheckBox_AluminumFrame.Checked = False
                    Case eMatType.MATERIAL_COLDFORMED : CheckBox_ColdFormSteelFrame.Checked = False
#Else
                    Case eMatType.Steel : CheckBox_SteelFrame.Checked = True
                    Case eMatType.Concrete : CheckBox_ConcreteFrame.Checked = True
                    Case eMatType.Aluminum : CheckBox_AluminumFrame.Checked = False
                    Case eMatType.ColdFormed : CheckBox_ColdFormSteelFrame.Checked = False
#End If
                End Select
            Next
        Catch ex As Exception
            If Not suppressWarning Then
                MsgBox(ex.Message)
                MsgBox(ex.StackTrace)
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Automatically checks the possible material design types in the form, based on the properties of the frame elements in the group that is used
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_CriticalPathGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_CriticalPathGroup.SelectedIndexChanged
        CheckMaterialTypesOfGroups()
    End Sub


#End Region

#Region "Methods"
    'TODO: Still used? Check if necessary
    Sub checkComboRedundant(ByRef myComboBox As ComboBox, ByVal listArray() As String)
        Dim i As Integer
        Dim j As Integer
        For i = 0 To myComboBox.Items.Count - 1
            For j = 0 To UBound(listArray)
                If myComboBox.Items.Item(i) Is listArray(j) Then myComboBox.Items.Remove(myComboBox.Items(i))
            Next j

            'For i = 0 To ComboBox_FileType.Items.Count - 1  'Selects initial cell
            '    If Items(i) = regTest.fileType Then
            '        frmRunSetup.ComboBox_FileType.SelectedItem = regTest.fileType
            '        Exit For
            '    End If
            'Next
        Next i
    End Sub

    ''' <summary>
    ''' Checks if a group of a given name exists in the model. If so, the function returns the name of the group.
    ''' </summary>
    ''' <param name="myGroupsList">List of group names to check</param>
    ''' <param name="myGroupName">Name to search for in the group list</param>
    ''' <returns>Nothing if the group does not exist. Returns the group name if it does exist.</returns>
    ''' <remarks></remarks>
    Function CheckExistingGroups(ByVal myGroupsList() As String, ByVal myGroupName As String) As String

        For Each groupItem As String In myGroupsList
            If groupItem = myGroupName Then
                CheckExistingGroups = myGroupName
                Exit Function
            End If
        Next

        CheckExistingGroups = ""

    End Function

#End Region

#Region "Tree Subs"

    ''' <summary>
    ''' Causes treeview to select node on right click.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This is necessary for extracting the node's content if choosing a right-click menu action</remarks>
    Private Sub TreeView_PC_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_PC.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            TreeView_PC.SelectedNode = e.Node
        End If
    End Sub

    ''' <summary>
    ''' Removes key frame element from key element group.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Performs action on current model object. Under current file renaming behavior, this means that this function removes the element from a group in the original model only, and not the last saved one.</remarks>
    Private Sub tsmFailedFrame_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim myNameRemove As String
        Dim myGroupName As String = ""
        Dim AListFrame As ArrayList = New ArrayList
        Dim AListOther As ArrayList = New ArrayList

        Dim NumberItems As Integer
        Dim ObjectType() As Integer = Nothing
        Dim ObjectName() As String = Nothing

        Dim i As Integer

        myGroupName = ComboBox_KeyElementGroup.Text
        myNameRemove = TreeView_PC.SelectedNode.Text

        'Strip additional text from node text to retrieve frame name
        myNameRemove = Microsoft.VisualBasic.Strings.Right(myNameRemove, Len(myNameRemove) - 6)

        'get group assignments
        ret = SapModel.GroupDef.GetAssignments(myGroupName, NumberItems, ObjectType, ObjectName)
        retCheck(ret, "SapModel.GroupDef.GetAssignments")

        'Change group assignment to array list. 
        For i = 0 To UBound(ObjectType)
            If ObjectType(i) = 2 Then
                AListFrame.Add(ObjectName(i)) 'Make separate array lists for frames vs. other group elements.
            Else
                AListOther.Add(ObjectName(i))
            End If
        Next

        '==TODO:Check for possible problems
        'Current function will not reapply non-frame elements. User is warned that these will be lost if they were included in the original group.
        If AListOther.Count > 0 Then
            Dim Result As DialogResult
            'Displays the MessageBox
            Result = MessageBox.Show("Warning! Group contains elements other than frames. These will be removed. Continue with operation?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            ' Gets the result of the MessageBox display.
            If Not Result = DialogResult.OK Then Exit Sub
        End If

        'Stops action if element to be removed is the only one in the group.
        If AListFrame.Count <= 1 Then
            MsgBox("Element cannot be removed from group. Group will be empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '==

        'Remove frame from frame array list
        AListFrame.Remove(myNameRemove)

        'clear group
        ret = SapModel.GroupDef.Clear(myGroupName)
        retCheck(ret, "SapModel.GroupDef.Clear")

        'add frame objects to group
        For i = 0 To AListFrame.Count - 1
            ret = SapModel.FrameObj.SetGroupAssign(CStr(AListFrame(i)), myGroupName)
            retCheck(ret, "SapModel.FrameObj.SetGroupAssign")
        Next i

    End Sub

    Private Sub tsmProgressiveFailures_click(ByVal sender As Object, ByVal e As EventArgs)
        'TODO: Is it possible to shift focus to design details form of the appropriate member in SAP2000?
        MsgBox("TODO: Is it possible to shift focus to design details form of the appropriate member in SAP2000?")
    End Sub

    Public Sub populateTreeView(ByRef failedFrameList As List(Of cFrame))
        Dim nodeString As String
        'Test
        Dim ctxmFailedFrame As New ContextMenuStrip
        Dim ctxmProgressiveFailures As New ContextMenuStrip
        Dim tsmFailedFrame As New ToolStripMenuItem("Remove from Key Element Group")
        Dim tsmProgressiveFailures As New ToolStripMenuItem("See Design Details")

        'Create Context Menus for tree nodes
        'Allows user to remove key element from key element group. Only allowed if a group is set to be checked
        If ComboBox_KeyElementGroup.Enabled Then
            ctxmFailedFrame.Items.Add(tsmFailedFrame)
            AddHandler tsmFailedFrame.Click, AddressOf tsmFailedFrame_click
        End If

        'Allows user to jump to design details of the given failed frame (TODO)
        ctxmProgressiveFailures.Items.Add(tsmProgressiveFailures)
        AddHandler tsmProgressiveFailures.Click, AddressOf tsmProgressiveFailures_click

        Dim i As Integer
        i = 0

        TreeView_PC.Nodes.Clear()

        'Creating the root node
        Dim root = New TreeNode("Key Frames Removal Results")
        TreeView_PC.Nodes.Add(root)

        For Each failedFrame As cFrame In failedFrameList
            Dim progressiveFailuresList As New List(Of cProgressiveFailures)

            'Create progressive failures list for a given failed frame/key element
            For Each progressiveFailedFrame As cFrame In failedFrame.progressiveFailedFrames
                progressiveFailuresList.Add(progressiveFailedFrame.progressiveFailure)
            Next

            'Creating the first node & Assigning properties
            nodeString = "Frame " & failedFrame.frameName
            Dim nodeFailedFrame As New TreeNode(nodeString)
            nodeString = failedFrame.section.nameSection
            nodeFailedFrame.ToolTipText = nodeString
            nodeFailedFrame.ContextMenuStrip = ctxmFailedFrame

            'Add node to specific place in tree
            TreeView_PC.Nodes(0).Nodes.Add(nodeFailedFrame)

            'Creating child nodes under the first child
            Select Case ProgCol.DCMethod
                Case eDCMethod.Strength
                    For Each progressiveFailure As cProgressiveFailures In progressiveFailuresList

                        'Define Node and assign properties
                        nodeString = "Frame " & progressiveFailure.frameName & ": D/C = " & Math.Round(progressiveFailure.frameDC, 5)
                        Dim nodeDC As New TreeNode(nodeString)

                        'Create context menu
                        nodeDC.ContextMenuStrip = ctxmProgressiveFailures

                        'Create Tooltip string
                        nodeString = progressiveFailure.designSection & vbCrLf

                        If progressiveFailure.designType = eCodeType.SteelFrame Then
                            nodeString = nodeString & "   " & Chr(149) & "P:    " & Math.Round(progressiveFailure.frameDC_P, 5) & vbCrLf
                            nodeString = nodeString & "   " & Chr(149) & "M2: " & Math.Round(progressiveFailure.frameDC_M2, 5) & vbCrLf
                            nodeString = nodeString & "   " & Chr(149) & "M3: " & Math.Round(progressiveFailure.frameDC_M3, 5) & vbCrLf
                        Else
                            nodeString = nodeString & "   " & Chr(149) & "P:    " & "N/A for Code Type" & vbCrLf
                            nodeString = nodeString & "   " & Chr(149) & "M2: " & "N/A for Code Type" & vbCrLf
                            nodeString = nodeString & "   " & Chr(149) & "M3: " & "N/A for Code Type" & vbCrLf
                        End If

                        nodeDC.ToolTipText = nodeString

                        'Add node to specific place in tree
                        TreeView_PC.Nodes(0).Nodes(i).Nodes.Add(nodeDC)
                    Next
                    i += 1

                Case eDCMethod.Ductility
                    For Each progressiveFailure As cProgressiveFailures In progressiveFailuresList

                        'Define Node and assign properties
                        nodeString = "Frame " & progressiveFailure.frameName & ": D/C = " & Math.Round(progressiveFailure.frameDC, 5)
                        Dim nodeDC As New TreeNode(nodeString)

                        'Create context menu
                        nodeDC.ContextMenuStrip = ctxmProgressiveFailures

                        'Create Tooltip string

                        nodeString = "Hinge at Node " & progressiveFailure.nodeNameI & " (Section:" & progressiveFailure.designSectionI & ")" & ": D/C = " & progressiveFailure.hingeI_DC & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "P:    " & Math.Round(progressiveFailure.hingeI_DC_P, 5) & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "M2: " & Math.Round(progressiveFailure.hingeI_DC_M2, 5) & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "M3: " & Math.Round(progressiveFailure.hingeI_DC_M3, 5) & vbCrLf

                        nodeString = nodeString & vbCrLf

                        nodeString = nodeString & "Hinge at Node " & progressiveFailure.nodeNameJ & "(Section:" & progressiveFailure.designSectionJ & ")" & ": D/C = " & progressiveFailure.hingeJ_DC & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "P:    " & Math.Round(progressiveFailure.hingeJ_DC_P, 5) & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "M2: " & Math.Round(progressiveFailure.hingeJ_DC_M2, 5) & vbCrLf
                        nodeString = nodeString & "   " & Chr(149) & "M3: " & Math.Round(progressiveFailure.hingeJ_DC_M3, 5) & vbCrLf

                        nodeDC.ToolTipText = nodeString

                        'Add node to specific place in tree
                        TreeView_PC.Nodes(0).Nodes(i).Nodes.Add(nodeDC)
                    Next
                    i += 1
            End Select
        Next
        TreeView_PC.ShowNodeToolTips = True
    End Sub
#End Region

#Region "Control Methods"
    'Buttons
    ''' <summary>
    ''' Runs the progressive collapse scenario set up in the form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnProgressiveCollapseRun_Click(sender As Object, e As EventArgs) Handles btnProgressiveCollapseRun.Click
        'Create frame and node class objects of the critical path
        ProgCol.criticalPathGroup = ComboBox_CriticalPathGroup.Text
        ProgCol.CreateCriticalPathFrames()
        ProgCol.CreateCriticalPathFrameNodes()
        ProgCol.AssignCriticalPathNodesToFrames()

        'ProgCol.filePathOriginal = myDevelopmentForm.directory & myDevelopmentForm.fileName

        If btnRad_LoadComboGSA.Checked = True Then ProgCol.loadCaseInitialUser = False
        If btnRad_LoadComboUser.Checked = True Then
            ProgCol.loadCaseInitialUser = True
            ProgCol.loadCaseNLSUser = CStr(ComboBox_LoadCombo.SelectedItem)
        End If

        If btnRad_Frame.Checked = True Then ProgCol.ProgCollapseController(ProgCol.findKeyFrame, ComboBox_KeyElementFrame.Text)
        If btnRad_Group.Checked = True Then ProgCol.ProgCollapseController(ProgCol.findKeyFrame, , ComboBox_KeyElementGroup.Text)
    End Sub

    ''' <summary>
    ''' Closes CSiProgressive Collapse. Leaves SAP2000 open.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
#If COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        If isPlugin Then _Plugin.sapPlugIn.Finish(0)
#End If

        Application.Exit()
    End Sub
    ''' <summary>
    ''' Closes CSiProgressive Collapse and the associated SAP2000 instance.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCloseAll_Click(sender As Object, e As EventArgs) Handles btnCloseAll.Click
#If COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        If isPlugin Then _Plugin.sapPlugIn.Finish(0)
#End If

        closeProgram()
        Application.Exit()
    End Sub

    ''' <summary>
    ''' Handles any closing event, including clicking the "x" on the form title.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPCControl_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
#If COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        If isPlugin Then _Plugin.sapPlugIn.Finish(0)
#End If
    End Sub

    Private Sub btnParametersSteel_Click(sender As Object, e As EventArgs) Handles btnParametersSteel.Click
        Dim materialParameters As frmMaterialTypeParameters
        materialParameters = New frmMaterialTypeParameters(eCodeType.SteelFrame)
        materialParameters.Show()
    End Sub
    Private Sub btnParametersConcrete_Click(sender As Object, e As EventArgs) Handles btnParametersConcrete.Click
        Dim materialParameters As frmMaterialTypeParameters
        materialParameters = New frmMaterialTypeParameters(eCodeType.ConcreteFrame)
        materialParameters.Show()
    End Sub
    Private Sub btnParametersAluminum_Click(sender As Object, e As EventArgs) Handles btnParametersAluminum.Click
        Dim materialParameters As frmMaterialTypeParameters
        materialParameters = New frmMaterialTypeParameters(eCodeType.AluminumFrame)
        materialParameters.Show()
    End Sub
    Private Sub btnParametersColdFormSteel_Click(sender As Object, e As EventArgs) Handles btnParametersColdFormSteel.Click
        Dim materialParameters As frmMaterialTypeParameters
        materialParameters = New frmMaterialTypeParameters(eCodeType.ColdFormedSteelFrame)
        materialParameters.Show()
    End Sub

    Private Sub btnCreateGroupKeyCheck_Click(sender As Object, e As EventArgs) Handles btnCreateGroupKeyCheck.Click
        'define new group
        ret = SapModel.GroupDef.SetGroup("PC_KeyCheck")
        retCheck(ret, "SapModel.GroupDef.SetGroup")

        btnCreateGroupKeyCheck.Enabled = False
        PopulateComboBoxesKeyElementsGroup()
        ComboBox_KeyElementGroup.SelectedItem = ProgCol.keyElementGroup

        MsgBox("Default progressive collapse group '" & defaultGroupKeyCheck & "' has been created. Please assign the appropriate members to this groups before running CSiProgressive Collapse")
    End Sub
    Private Sub btnCreateGroupPath_Click(sender As Object, e As EventArgs) Handles btnCreateGroupPath.Click
        'define new group
        ret = SapModel.GroupDef.SetGroup("PC_Path")
        retCheck(ret, "SapModel.GroupDef.SetGroup")

        btnCreateGroupPath.Enabled = False
        PopulateComboBoxesCriticalPathGroup()
        ComboBox_CriticalPathGroup.SelectedItem = ProgCol.criticalPathGroup

        MsgBox("Default progressive collapse group '" & defaultGroupPath & "' has been created. Please assign the appropriate members to this group before running CSiProgressive Collapse")
    End Sub

    Private Sub btnOptionsTrackProgCol_Click(sender As Object, e As EventArgs) Handles btnOptionsTrackProgCol.Click
        Dim trackProgCol As New frmTrackProgCol

        trackProgCol.ShowDialog()

    End Sub

    Private Sub btnLoadCaseParameters_Click(sender As Object, e As EventArgs) Handles btnLoadCaseParameters.Click
        Dim LoadCaseParameter As New frmLoadCaseParameters

        LoadCaseParameter.ShowDialog()

    End Sub

    Private Sub btn_Resources_Click(sender As Object, e As EventArgs) Handles btn_Resources.Click
        Dim myFrmResources As New frmResources

        myFrmResources.Show()
    End Sub
    'Private Sub btnDevelopment_Click(sender As Object, e As EventArgs) Handles btnDevelopment.Click
    '    myDevelopmentForm = New frmDevelopment

    '    myDevelopmentForm.Show()
    'End Sub

    'Check Boxes
    Private Sub CheckBox_SaveSeparate_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_SaveSeparate.CheckedChanged
        If CheckBox_SaveSeparate.Checked = True Then ProgCol.saveSeparateModel = True
        If CheckBox_SaveSeparate.Checked = False Then ProgCol.saveSeparateModel = False
    End Sub
    Private Sub CheckBox_FailedGroup_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_FailedGroup.CheckedChanged
        If CheckBox_FailedGroup.Checked = True Then ProgCol.createFailedGroup = True
        If CheckBox_FailedGroup.Checked = False Then ProgCol.createFailedGroup = False
    End Sub


    Private Sub CheckBox_SteelFrame_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_SteelFrame.CheckedChanged
        If CheckBox_SteelFrame.Checked Then
            btnParametersSteel.Enabled = True
            ProgCol.designCodes.CreateDesignCode(eCodeType.SteelFrame)
        Else
            btnParametersSteel.Enabled = False
            ProgCol.designCodes.RemoveDesignCode(eCodeType.SteelFrame)
        End If

        btnProgressiveCollapseRun.Enabled = EnableRunButton()
    End Sub
    Private Sub CheckBox_ConcreteFrame_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ConcreteFrame.CheckedChanged
        If CheckBox_ConcreteFrame.Checked Then
            btnParametersConcrete.Enabled = True
            ProgCol.designCodes.CreateDesignCode(eCodeType.ConcreteFrame)
        Else
            btnParametersConcrete.Enabled = False
            ProgCol.designCodes.RemoveDesignCode(eCodeType.ConcreteFrame)
        End If

        btnProgressiveCollapseRun.Enabled = EnableRunButton()
    End Sub
    Private Sub CheckBox_AluminumFrame_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_AluminumFrame.CheckedChanged
        If CheckBox_AluminumFrame.Checked Then
            btnParametersAluminum.Enabled = True
            ProgCol.designCodes.CreateDesignCode(eCodeType.AluminumFrame)
        Else
            btnParametersAluminum.Enabled = False
            ProgCol.designCodes.RemoveDesignCode(eCodeType.AluminumFrame)
        End If

        btnProgressiveCollapseRun.Enabled = EnableRunButton()
    End Sub
    Private Sub CheckBox_ColdFormSteelFrame_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox_ColdFormSteelFrame.CheckedChanged
        If CheckBox_ColdFormSteelFrame.Checked Then
            btnParametersColdFormSteel.Enabled = True
            ProgCol.designCodes.CreateDesignCode(eCodeType.ColdFormedSteelFrame)
        Else
            btnParametersColdFormSteel.Enabled = False
            ProgCol.designCodes.RemoveDesignCode(eCodeType.ColdFormedSteelFrame)
        End If

        btnProgressiveCollapseRun.Enabled = EnableRunButton()
    End Sub

    'Radio Buttons
    Private Sub btnRad_Frame_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_Frame.CheckedChanged
        If btnRad_Frame.Checked Then
            ComboBox_KeyElementFrame.Enabled = True
            ComboBox_KeyElementGroup.Enabled = False
            btnCreateGroupKeyCheck.Enabled = False
        End If
    End Sub
    Private Sub btnRad_Group_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_Group.CheckedChanged
        If btnRad_Group.Checked Then
            'Disable other radio buttons, enables current button
            ComboBox_KeyElementFrame.Enabled = False
            ComboBox_KeyElementGroup.Enabled = True

            'Enables button for adding default group, if the default group does not already exist
            btnCreateGroupKeyCheck.Enabled = True
            For Each groupName As String In ComboBox_KeyElementGroup.Items
                If groupName = ProgCol.keyElementGroup Then btnCreateGroupKeyCheck.Enabled = False
            Next
        End If
    End Sub

    Private Sub btnRad_CollapseInitiate_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_CollapseInitiate.CheckedChanged
        If btnRad_CollapseInitiate.Checked Then
            ProgCol.findKeyFrame = True
            btnOptionsTrackProgCol.Enabled = False
        End If
    End Sub
    Private Sub btnRad_Propagation_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_CollapsePropagate.CheckedChanged
        If btnRad_CollapsePropagate.Checked Then
            ProgCol.findKeyFrame = False
            btnOptionsTrackProgCol.Enabled = True
        End If
    End Sub

    Private Sub btnRad_DCStrength_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_DCStrength.CheckedChanged
        If btnRad_DCStrength.Checked Then
            ProgCol.DCMethod = eDCMethod.Strength

            'Disable propagation options
            btnRad_CollapseInitiate.Checked = True
            btnRad_CollapsePropagate.Enabled = False
            btnOptionsTrackProgCol.Enabled = False
        End If

    End Sub
    Private Sub btnRad_DCDuctility_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_DCDuctility.CheckedChanged
        If btnRad_DCDuctility.Checked Then
            ProgCol.DCMethod = eDCMethod.Ductility

            'Enable propagation options
            btnRad_CollapsePropagate.Enabled = True
        End If
    End Sub

    Private Sub btnRad_LoadComboGSA_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_LoadComboGSA.CheckedChanged
        ComboBox_LoadCombo.Enabled = False
    End Sub
    Private Sub btnRad_LoadComboUser_CheckedChanged(sender As Object, e As EventArgs) Handles btnRad_LoadComboUser.CheckedChanged
        ComboBox_LoadCombo.Enabled = True
    End Sub

#End Region

#Region "Timer"
    'TODO:
    'Used to bring form to front, but has problems.
    'Inactive right now.
    'Resolved or remove.
    Private Delegate Sub CloseFormCallback()

    Sub FormTimerTopMost()
        Dim myTimer As New Timer()
        myTimer.Interval = 250
        myTimer.Enabled = True
        myTimer.Start()

        ' Hook up the Elapsed event for the timer. 
        AddHandler myTimer.Elapsed, AddressOf OnTimedEvent

    End Sub

    Private Sub MoveFormBack()
        If InvokeRequired Then
            Dim d As New CloseFormCallback(AddressOf MoveFormBack)
            Invoke(d, Nothing)
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub OnTimedEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        'Stops interval from occuring repeatedly
        myTimer.Stop()
        myTimer.Enabled = False

        MoveFormBack()
    End Sub

    Private Sub frmPCControl_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Me.TopMost = True
        'FormTimerTopMost()
    End Sub

    Private Sub frmPCControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'Me.TopMost = True
        'FormTimerTopMost()
    End Sub

#End Region

End Class
