<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPCControl
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPCControl))
        Me.btnProgressiveCollapseRun = New System.Windows.Forms.Button()
        Me.GrpBxKeyElement = New System.Windows.Forms.GroupBox()
        Me.btnCreateGroupKeyCheck = New System.Windows.Forms.Button()
        Me.btnRad_Group = New System.Windows.Forms.RadioButton()
        Me.ComboBox_KeyElementGroup = New System.Windows.Forms.ComboBox()
        Me.btnRad_Frame = New System.Windows.Forms.RadioButton()
        Me.ComboBox_KeyElementFrame = New System.Windows.Forms.ComboBox()
        Me.TreeView_PC = New System.Windows.Forms.TreeView()
        Me.GrpBxCriticalPath = New System.Windows.Forms.GroupBox()
        Me.btnCreateGroupPath = New System.Windows.Forms.Button()
        Me.ComboBox_CriticalPathGroup = New System.Windows.Forms.ComboBox()
        Me.lblCriticalPathGrp = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.btnLoadCaseParameters = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ComboBox_LoadCombo = New System.Windows.Forms.ComboBox()
        Me.btnRad_LoadComboUser = New System.Windows.Forms.RadioButton()
        Me.btnRad_LoadComboGSA = New System.Windows.Forms.RadioButton()
        Me.btn_Resources = New System.Windows.Forms.Button()
        Me.btnDevelopment = New System.Windows.Forms.Button()
        Me.CheckBox_SaveSeparate = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.GrpBxDCMethod = New System.Windows.Forms.GroupBox()
        Me.GrpBxMaterialTypeParams = New System.Windows.Forms.GroupBox()
        Me.CheckBox_ColdFormSteelFrame = New System.Windows.Forms.CheckBox()
        Me.CheckBox_AluminumFrame = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ConcreteFrame = New System.Windows.Forms.CheckBox()
        Me.CheckBox_SteelFrame = New System.Windows.Forms.CheckBox()
        Me.btnParametersColdFormSteel = New System.Windows.Forms.Button()
        Me.btnParametersAluminum = New System.Windows.Forms.Button()
        Me.btnParametersConcrete = New System.Windows.Forms.Button()
        Me.btnParametersSteel = New System.Windows.Forms.Button()
        Me.btnRad_DCDuctility = New System.Windows.Forms.RadioButton()
        Me.btnRad_DCStrength = New System.Windows.Forms.RadioButton()
        Me.GrpBxAnalysisMethod = New System.Windows.Forms.GroupBox()
        Me.btnOptionsTrackProgCol = New System.Windows.Forms.Button()
        Me.btnRad_CollapsePropagate = New System.Windows.Forms.RadioButton()
        Me.btnRad_CollapseInitiate = New System.Windows.Forms.RadioButton()
        Me.CheckBox_FailedGroup = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.btnCloseAll = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GrpBxKeyElement.SuspendLayout
        Me.GrpBxCriticalPath.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox11.SuspendLayout
        Me.GroupBox5.SuspendLayout
        Me.GroupBox10.SuspendLayout
        Me.GrpBxDCMethod.SuspendLayout
        Me.GrpBxMaterialTypeParams.SuspendLayout
        Me.GrpBxAnalysisMethod.SuspendLayout
        Me.GroupBox8.SuspendLayout
        Me.SuspendLayout
        '
        'btnProgressiveCollapseRun
        '
        Me.btnProgressiveCollapseRun.Location = New System.Drawing.Point(6, 500)
        Me.btnProgressiveCollapseRun.Name = "btnProgressiveCollapseRun"
        Me.btnProgressiveCollapseRun.Size = New System.Drawing.Size(150, 23)
        Me.btnProgressiveCollapseRun.TabIndex = 14
        Me.btnProgressiveCollapseRun.Text = "Run Progressive Collapse"
        Me.btnProgressiveCollapseRun.UseVisualStyleBackColor = true
        '
        'GrpBxKeyElement
        '
        Me.GrpBxKeyElement.Controls.Add(Me.btnCreateGroupKeyCheck)
        Me.GrpBxKeyElement.Controls.Add(Me.btnRad_Group)
        Me.GrpBxKeyElement.Controls.Add(Me.ComboBox_KeyElementGroup)
        Me.GrpBxKeyElement.Controls.Add(Me.btnRad_Frame)
        Me.GrpBxKeyElement.Controls.Add(Me.ComboBox_KeyElementFrame)
        Me.GrpBxKeyElement.Location = New System.Drawing.Point(6, 18)
        Me.GrpBxKeyElement.Name = "GrpBxKeyElement"
        Me.GrpBxKeyElement.Size = New System.Drawing.Size(265, 98)
        Me.GrpBxKeyElement.TabIndex = 21
        Me.GrpBxKeyElement.TabStop = false
        Me.GrpBxKeyElement.Text = "Key Element"
        Me.ToolTip1.SetToolTip(Me.GrpBxKeyElement, "Element to be removed to check for collapse propagation.")
        '
        'btnCreateGroupKeyCheck
        '
        Me.btnCreateGroupKeyCheck.Location = New System.Drawing.Point(63, 71)
        Me.btnCreateGroupKeyCheck.Name = "btnCreateGroupKeyCheck"
        Me.btnCreateGroupKeyCheck.Size = New System.Drawing.Size(121, 23)
        Me.btnCreateGroupKeyCheck.TabIndex = 29
        Me.btnCreateGroupKeyCheck.TabStop = false
        Me.btnCreateGroupKeyCheck.Text = "Create Default Group"
        Me.ToolTip1.SetToolTip(Me.btnCreateGroupKeyCheck, "Adds existing list of frame elements from SAP2000 to the combo box")
        Me.btnCreateGroupKeyCheck.UseVisualStyleBackColor = true
        '
        'btnRad_Group
        '
        Me.btnRad_Group.AutoSize = true
        Me.btnRad_Group.Location = New System.Drawing.Point(7, 50)
        Me.btnRad_Group.Name = "btnRad_Group"
        Me.btnRad_Group.Size = New System.Drawing.Size(54, 17)
        Me.btnRad_Group.TabIndex = 1
        Me.btnRad_Group.TabStop = true
        Me.btnRad_Group.Text = "Group"
        Me.ToolTip1.SetToolTip(Me.btnRad_Group, "Select to check a group of key elements for individual removal")
        Me.btnRad_Group.UseVisualStyleBackColor = true
        '
        'ComboBox_KeyElementGroup
        '
        Me.ComboBox_KeyElementGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_KeyElementGroup.FormattingEnabled = true
        Me.ComboBox_KeyElementGroup.Location = New System.Drawing.Point(63, 46)
        Me.ComboBox_KeyElementGroup.Name = "ComboBox_KeyElementGroup"
        Me.ComboBox_KeyElementGroup.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_KeyElementGroup.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.ComboBox_KeyElementGroup, "Group of 1 or more frame elements to be removed individually")
        '
        'btnRad_Frame
        '
        Me.btnRad_Frame.AutoSize = true
        Me.btnRad_Frame.Location = New System.Drawing.Point(6, 23)
        Me.btnRad_Frame.Name = "btnRad_Frame"
        Me.btnRad_Frame.Size = New System.Drawing.Size(54, 17)
        Me.btnRad_Frame.TabIndex = 0
        Me.btnRad_Frame.TabStop = true
        Me.btnRad_Frame.Text = "Frame"
        Me.ToolTip1.SetToolTip(Me.btnRad_Frame, "Select to remove a single frame element")
        Me.btnRad_Frame.UseVisualStyleBackColor = true
        '
        'ComboBox_KeyElementFrame
        '
        Me.ComboBox_KeyElementFrame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_KeyElementFrame.FormattingEnabled = true
        Me.ComboBox_KeyElementFrame.Location = New System.Drawing.Point(63, 19)
        Me.ComboBox_KeyElementFrame.Name = "ComboBox_KeyElementFrame"
        Me.ComboBox_KeyElementFrame.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_KeyElementFrame.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.ComboBox_KeyElementFrame, "Single frame element to be removed")
        '
        'TreeView_PC
        '
        Me.TreeView_PC.Location = New System.Drawing.Point(518, 12)
        Me.TreeView_PC.Name = "TreeView_PC"
        Me.TreeView_PC.Size = New System.Drawing.Size(282, 511)
        Me.TreeView_PC.TabIndex = 22
        '
        'GrpBxCriticalPath
        '
        Me.GrpBxCriticalPath.Controls.Add(Me.btnCreateGroupPath)
        Me.GrpBxCriticalPath.Controls.Add(Me.ComboBox_CriticalPathGroup)
        Me.GrpBxCriticalPath.Controls.Add(Me.lblCriticalPathGrp)
        Me.GrpBxCriticalPath.Location = New System.Drawing.Point(276, 18)
        Me.GrpBxCriticalPath.Name = "GrpBxCriticalPath"
        Me.GrpBxCriticalPath.Size = New System.Drawing.Size(221, 98)
        Me.GrpBxCriticalPath.TabIndex = 23
        Me.GrpBxCriticalPath.TabStop = false
        Me.GrpBxCriticalPath.Text = "Critical Path"
        '
        'btnCreateGroupPath
        '
        Me.btnCreateGroupPath.Location = New System.Drawing.Point(63, 46)
        Me.btnCreateGroupPath.Name = "btnCreateGroupPath"
        Me.btnCreateGroupPath.Size = New System.Drawing.Size(121, 23)
        Me.btnCreateGroupPath.TabIndex = 30
        Me.btnCreateGroupPath.TabStop = false
        Me.btnCreateGroupPath.Text = "Create Default Group"
        Me.ToolTip1.SetToolTip(Me.btnCreateGroupPath, "Adds existing list of frame elements from SAP2000 to the combo box")
        Me.btnCreateGroupPath.UseVisualStyleBackColor = true
        '
        'ComboBox_CriticalPathGroup
        '
        Me.ComboBox_CriticalPathGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CriticalPathGroup.FormattingEnabled = true
        Me.ComboBox_CriticalPathGroup.Location = New System.Drawing.Point(63, 19)
        Me.ComboBox_CriticalPathGroup.Name = "ComboBox_CriticalPathGroup"
        Me.ComboBox_CriticalPathGroup.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_CriticalPathGroup.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.ComboBox_CriticalPathGroup, "Group of 1 or more frame elements that  indicate progressive collapse if they fai"& _ 
        "l due to removal of a key element")
        '
        'lblCriticalPathGrp
        '
        Me.lblCriticalPathGrp.AutoSize = true
        Me.lblCriticalPathGrp.Location = New System.Drawing.Point(21, 21)
        Me.lblCriticalPathGrp.Name = "lblCriticalPathGrp"
        Me.lblCriticalPathGrp.Size = New System.Drawing.Size(36, 13)
        Me.lblCriticalPathGrp.TabIndex = 21
        Me.lblCriticalPathGrp.Text = "Group"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox3)
        Me.GroupBox4.Controls.Add(Me.GroupBox10)
        Me.GroupBox4.Controls.Add(Me.GroupBox8)
        Me.GroupBox4.Controls.Add(Me.btnCloseAll)
        Me.GroupBox4.Controls.Add(Me.TreeView_PC)
        Me.GroupBox4.Controls.Add(Me.btnProgressiveCollapseRun)
        Me.GroupBox4.Controls.Add(Me.btnClose)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(807, 534)
        Me.GroupBox4.TabIndex = 25
        Me.GroupBox4.TabStop = false
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox11)
        Me.GroupBox3.Controls.Add(Me.btn_Resources)
        Me.GroupBox3.Controls.Add(Me.btnDevelopment)
        Me.GroupBox3.Controls.Add(Me.CheckBox_SaveSeparate)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 323)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(506, 171)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = false
        '
        'GroupBox11
        '
        Me.GroupBox11.Controls.Add(Me.btnLoadCaseParameters)
        Me.GroupBox11.Controls.Add(Me.GroupBox5)
        Me.GroupBox11.Location = New System.Drawing.Point(6, 14)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(271, 151)
        Me.GroupBox11.TabIndex = 32
        Me.GroupBox11.TabStop = false
        Me.GroupBox11.Text = "Loading"
        '
        'btnLoadCaseParameters
        '
        Me.btnLoadCaseParameters.Location = New System.Drawing.Point(6, 16)
        Me.btnLoadCaseParameters.Name = "btnLoadCaseParameters"
        Me.btnLoadCaseParameters.Size = New System.Drawing.Size(201, 23)
        Me.btnLoadCaseParameters.TabIndex = 28
        Me.btnLoadCaseParameters.Text = "Failed Member Load Case Parameters"
        Me.btnLoadCaseParameters.UseVisualStyleBackColor = true
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.ComboBox_LoadCombo)
        Me.GroupBox5.Controls.Add(Me.btnRad_LoadComboUser)
        Me.GroupBox5.Controls.Add(Me.btnRad_LoadComboGSA)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 45)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(257, 100)
        Me.GroupBox5.TabIndex = 27
        Me.GroupBox5.TabStop = false
        Me.GroupBox5.Text = "Load Cases/Combinations"
        '
        'ComboBox_LoadCombo
        '
        Me.ComboBox_LoadCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_LoadCombo.FormattingEnabled = true
        Me.ComboBox_LoadCombo.Location = New System.Drawing.Point(25, 66)
        Me.ComboBox_LoadCombo.Name = "ComboBox_LoadCombo"
        Me.ComboBox_LoadCombo.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_LoadCombo.TabIndex = 28
        '
        'btnRad_LoadComboUser
        '
        Me.btnRad_LoadComboUser.AutoSize = true
        Me.btnRad_LoadComboUser.Location = New System.Drawing.Point(6, 42)
        Me.btnRad_LoadComboUser.Name = "btnRad_LoadComboUser"
        Me.btnRad_LoadComboUser.Size = New System.Drawing.Size(92, 17)
        Me.btnRad_LoadComboUser.TabIndex = 27
        Me.btnRad_LoadComboUser.TabStop = true
        Me.btnRad_LoadComboUser.Text = "User-Selected"
        Me.btnRad_LoadComboUser.UseVisualStyleBackColor = true
        '
        'btnRad_LoadComboGSA
        '
        Me.btnRad_LoadComboGSA.AutoSize = true
        Me.btnRad_LoadComboGSA.Location = New System.Drawing.Point(6, 19)
        Me.btnRad_LoadComboGSA.Name = "btnRad_LoadComboGSA"
        Me.btnRad_LoadComboGSA.Size = New System.Drawing.Size(166, 17)
        Me.btnRad_LoadComboGSA.TabIndex = 26
        Me.btnRad_LoadComboGSA.TabStop = true
        Me.btnRad_LoadComboGSA.Text = "GSA Default (2*(DL+0.25*LL))"
        Me.btnRad_LoadComboGSA.UseVisualStyleBackColor = true
        '
        'btn_Resources
        '
        Me.btn_Resources.Location = New System.Drawing.Point(339, 78)
        Me.btn_Resources.Name = "btn_Resources"
        Me.btn_Resources.Size = New System.Drawing.Size(78, 23)
        Me.btn_Resources.TabIndex = 24
        Me.btn_Resources.Text = "Resources"
        Me.btn_Resources.UseVisualStyleBackColor = true
        '
        'btnDevelopment
        '
        Me.btnDevelopment.Location = New System.Drawing.Point(339, 107)
        Me.btnDevelopment.Name = "btnDevelopment"
        Me.btnDevelopment.Size = New System.Drawing.Size(78, 23)
        Me.btnDevelopment.TabIndex = 33
        Me.btnDevelopment.Text = "Development"
        Me.btnDevelopment.UseVisualStyleBackColor = true
        '
        'CheckBox_SaveSeparate
        '
        Me.CheckBox_SaveSeparate.AutoSize = true
        Me.CheckBox_SaveSeparate.Location = New System.Drawing.Point(283, 24)
        Me.CheckBox_SaveSeparate.Name = "CheckBox_SaveSeparate"
        Me.CheckBox_SaveSeparate.Size = New System.Drawing.Size(214, 17)
        Me.CheckBox_SaveSeparate.TabIndex = 27
        Me.CheckBox_SaveSeparate.Text = "Save Iterations As Separate Model Files"
        Me.ToolTip1.SetToolTip(Me.CheckBox_SaveSeparate, "Only runs first step of collapse to determine if key element causes progressive c"& _ 
        "ollapse. If unchecked, collapse will be simulated.")
        Me.CheckBox_SaveSeparate.UseVisualStyleBackColor = true
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.GrpBxDCMethod)
        Me.GroupBox10.Controls.Add(Me.GrpBxAnalysisMethod)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 138)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(506, 185)
        Me.GroupBox10.TabIndex = 32
        Me.GroupBox10.TabStop = false
        Me.GroupBox10.Text = "Methods"
        '
        'GrpBxDCMethod
        '
        Me.GrpBxDCMethod.Controls.Add(Me.GrpBxMaterialTypeParams)
        Me.GrpBxDCMethod.Controls.Add(Me.btnRad_DCDuctility)
        Me.GrpBxDCMethod.Controls.Add(Me.btnRad_DCStrength)
        Me.GrpBxDCMethod.Location = New System.Drawing.Point(6, 16)
        Me.GrpBxDCMethod.Name = "GrpBxDCMethod"
        Me.GrpBxDCMethod.Size = New System.Drawing.Size(265, 163)
        Me.GrpBxDCMethod.TabIndex = 27
        Me.GrpBxDCMethod.TabStop = false
        Me.GrpBxDCMethod.Text = "D/C Method"
        '
        'GrpBxMaterialTypeParams
        '
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.CheckBox_ColdFormSteelFrame)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.CheckBox_AluminumFrame)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.CheckBox_ConcreteFrame)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.CheckBox_SteelFrame)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.btnParametersColdFormSteel)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.btnParametersAluminum)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.btnParametersConcrete)
        Me.GrpBxMaterialTypeParams.Controls.Add(Me.btnParametersSteel)
        Me.GrpBxMaterialTypeParams.Location = New System.Drawing.Point(99, 17)
        Me.GrpBxMaterialTypeParams.Name = "GrpBxMaterialTypeParams"
        Me.GrpBxMaterialTypeParams.Size = New System.Drawing.Size(158, 141)
        Me.GrpBxMaterialTypeParams.TabIndex = 27
        Me.GrpBxMaterialTypeParams.TabStop = false
        Me.GrpBxMaterialTypeParams.Text = "Material Type Parameters"
        '
        'CheckBox_ColdFormSteelFrame
        '
        Me.CheckBox_ColdFormSteelFrame.AutoSize = true
        Me.CheckBox_ColdFormSteelFrame.Enabled = false
        Me.CheckBox_ColdFormSteelFrame.Location = New System.Drawing.Point(6, 111)
        Me.CheckBox_ColdFormSteelFrame.Name = "CheckBox_ColdFormSteelFrame"
        Me.CheckBox_ColdFormSteelFrame.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_ColdFormSteelFrame.TabIndex = 33
        Me.CheckBox_ColdFormSteelFrame.UseVisualStyleBackColor = true
        '
        'CheckBox_AluminumFrame
        '
        Me.CheckBox_AluminumFrame.AutoSize = true
        Me.CheckBox_AluminumFrame.Enabled = false
        Me.CheckBox_AluminumFrame.Location = New System.Drawing.Point(6, 83)
        Me.CheckBox_AluminumFrame.Name = "CheckBox_AluminumFrame"
        Me.CheckBox_AluminumFrame.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_AluminumFrame.TabIndex = 32
        Me.CheckBox_AluminumFrame.UseVisualStyleBackColor = true
        '
        'CheckBox_ConcreteFrame
        '
        Me.CheckBox_ConcreteFrame.AutoSize = true
        Me.CheckBox_ConcreteFrame.Location = New System.Drawing.Point(6, 52)
        Me.CheckBox_ConcreteFrame.Name = "CheckBox_ConcreteFrame"
        Me.CheckBox_ConcreteFrame.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_ConcreteFrame.TabIndex = 31
        Me.CheckBox_ConcreteFrame.UseVisualStyleBackColor = true
        '
        'CheckBox_SteelFrame
        '
        Me.CheckBox_SteelFrame.AutoSize = true
        Me.CheckBox_SteelFrame.Location = New System.Drawing.Point(6, 24)
        Me.CheckBox_SteelFrame.Name = "CheckBox_SteelFrame"
        Me.CheckBox_SteelFrame.Size = New System.Drawing.Size(15, 14)
        Me.CheckBox_SteelFrame.TabIndex = 30
        Me.CheckBox_SteelFrame.UseVisualStyleBackColor = true
        '
        'btnParametersColdFormSteel
        '
        Me.btnParametersColdFormSteel.Enabled = false
        Me.btnParametersColdFormSteel.Location = New System.Drawing.Point(27, 106)
        Me.btnParametersColdFormSteel.Name = "btnParametersColdFormSteel"
        Me.btnParametersColdFormSteel.Size = New System.Drawing.Size(124, 23)
        Me.btnParametersColdFormSteel.TabIndex = 29
        Me.btnParametersColdFormSteel.Text = "Cold Form Steel Frame"
        Me.btnParametersColdFormSteel.UseVisualStyleBackColor = true
        '
        'btnParametersAluminum
        '
        Me.btnParametersAluminum.Enabled = false
        Me.btnParametersAluminum.Location = New System.Drawing.Point(27, 77)
        Me.btnParametersAluminum.Name = "btnParametersAluminum"
        Me.btnParametersAluminum.Size = New System.Drawing.Size(124, 23)
        Me.btnParametersAluminum.TabIndex = 28
        Me.btnParametersAluminum.Text = "Aluminum Frame"
        Me.btnParametersAluminum.UseVisualStyleBackColor = true
        '
        'btnParametersConcrete
        '
        Me.btnParametersConcrete.Location = New System.Drawing.Point(27, 48)
        Me.btnParametersConcrete.Name = "btnParametersConcrete"
        Me.btnParametersConcrete.Size = New System.Drawing.Size(124, 23)
        Me.btnParametersConcrete.TabIndex = 27
        Me.btnParametersConcrete.Text = "Concrete Frame"
        Me.btnParametersConcrete.UseVisualStyleBackColor = true
        '
        'btnParametersSteel
        '
        Me.btnParametersSteel.Location = New System.Drawing.Point(27, 19)
        Me.btnParametersSteel.Name = "btnParametersSteel"
        Me.btnParametersSteel.Size = New System.Drawing.Size(124, 23)
        Me.btnParametersSteel.TabIndex = 26
        Me.btnParametersSteel.Text = "Steel Frame"
        Me.btnParametersSteel.UseVisualStyleBackColor = true
        '
        'btnRad_DCDuctility
        '
        Me.btnRad_DCDuctility.AutoSize = true
        Me.btnRad_DCDuctility.Location = New System.Drawing.Point(6, 38)
        Me.btnRad_DCDuctility.Name = "btnRad_DCDuctility"
        Me.btnRad_DCDuctility.Size = New System.Drawing.Size(62, 17)
        Me.btnRad_DCDuctility.TabIndex = 35
        Me.btnRad_DCDuctility.Text = "Ductility"
        Me.ToolTip1.SetToolTip(Me.btnRad_DCDuctility, "Select to check a group of key elements for individual removal")
        Me.btnRad_DCDuctility.UseVisualStyleBackColor = true
        '
        'btnRad_DCStrength
        '
        Me.btnRad_DCStrength.AutoSize = true
        Me.btnRad_DCStrength.Location = New System.Drawing.Point(6, 17)
        Me.btnRad_DCStrength.Name = "btnRad_DCStrength"
        Me.btnRad_DCStrength.Size = New System.Drawing.Size(65, 17)
        Me.btnRad_DCStrength.TabIndex = 34
        Me.btnRad_DCStrength.Text = "Strength"
        Me.ToolTip1.SetToolTip(Me.btnRad_DCStrength, "Select to check a group of key elements for individual removal")
        Me.btnRad_DCStrength.UseVisualStyleBackColor = true
        '
        'GrpBxAnalysisMethod
        '
        Me.GrpBxAnalysisMethod.Controls.Add(Me.btnOptionsTrackProgCol)
        Me.GrpBxAnalysisMethod.Controls.Add(Me.btnRad_CollapsePropagate)
        Me.GrpBxAnalysisMethod.Controls.Add(Me.btnRad_CollapseInitiate)
        Me.GrpBxAnalysisMethod.Controls.Add(Me.CheckBox_FailedGroup)
        Me.GrpBxAnalysisMethod.Location = New System.Drawing.Point(277, 16)
        Me.GrpBxAnalysisMethod.Name = "GrpBxAnalysisMethod"
        Me.GrpBxAnalysisMethod.Size = New System.Drawing.Size(223, 163)
        Me.GrpBxAnalysisMethod.TabIndex = 26
        Me.GrpBxAnalysisMethod.TabStop = false
        Me.GrpBxAnalysisMethod.Text = "Analysis Method"
        '
        'btnOptionsTrackProgCol
        '
        Me.btnOptionsTrackProgCol.Location = New System.Drawing.Point(23, 85)
        Me.btnOptionsTrackProgCol.Name = "btnOptionsTrackProgCol"
        Me.btnOptionsTrackProgCol.Size = New System.Drawing.Size(78, 23)
        Me.btnOptionsTrackProgCol.TabIndex = 38
        Me.btnOptionsTrackProgCol.Text = "Options"
        Me.btnOptionsTrackProgCol.UseVisualStyleBackColor = true
        '
        'btnRad_CollapsePropagate
        '
        Me.btnRad_CollapsePropagate.AutoSize = true
        Me.btnRad_CollapsePropagate.Location = New System.Drawing.Point(6, 62)
        Me.btnRad_CollapsePropagate.Name = "btnRad_CollapsePropagate"
        Me.btnRad_CollapsePropagate.Size = New System.Drawing.Size(125, 17)
        Me.btnRad_CollapsePropagate.TabIndex = 34
        Me.btnRad_CollapsePropagate.Text = "Collapse Propagation"
        Me.ToolTip1.SetToolTip(Me.btnRad_CollapsePropagate, "Select to check a group of key elements for individual removal")
        Me.btnRad_CollapsePropagate.UseVisualStyleBackColor = true
        '
        'btnRad_CollapseInitiate
        '
        Me.btnRad_CollapseInitiate.AutoSize = true
        Me.btnRad_CollapseInitiate.Location = New System.Drawing.Point(6, 19)
        Me.btnRad_CollapseInitiate.Name = "btnRad_CollapseInitiate"
        Me.btnRad_CollapseInitiate.Size = New System.Drawing.Size(107, 17)
        Me.btnRad_CollapseInitiate.TabIndex = 33
        Me.btnRad_CollapseInitiate.Text = "Collapse Initiation"
        Me.ToolTip1.SetToolTip(Me.btnRad_CollapseInitiate, "Select to check a group of key elements for individual removal")
        Me.btnRad_CollapseInitiate.UseVisualStyleBackColor = true
        '
        'CheckBox_FailedGroup
        '
        Me.CheckBox_FailedGroup.AutoSize = true
        Me.CheckBox_FailedGroup.Location = New System.Drawing.Point(24, 42)
        Me.CheckBox_FailedGroup.Name = "CheckBox_FailedGroup"
        Me.CheckBox_FailedGroup.Size = New System.Drawing.Size(182, 17)
        Me.CheckBox_FailedGroup.TabIndex = 28
        Me.CheckBox_FailedGroup.Text = "Add Failed Frames to New Group"
        Me.ToolTip1.SetToolTip(Me.CheckBox_FailedGroup, "Only runs first step of collapse to determine if key element causes progressive c"& _ 
        "ollapse. If unchecked, collapse will be simulated.")
        Me.CheckBox_FailedGroup.UseVisualStyleBackColor = true
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GrpBxKeyElement)
        Me.GroupBox8.Controls.Add(Me.GrpBxCriticalPath)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(506, 122)
        Me.GroupBox8.TabIndex = 31
        Me.GroupBox8.TabStop = false
        Me.GroupBox8.Text = "System"
        '
        'btnCloseAll
        '
        Me.btnCloseAll.Location = New System.Drawing.Point(420, 500)
        Me.btnCloseAll.Name = "btnCloseAll"
        Me.btnCloseAll.Size = New System.Drawing.Size(78, 23)
        Me.btnCloseAll.TabIndex = 31
        Me.btnCloseAll.Text = "Close All"
        Me.btnCloseAll.UseVisualStyleBackColor = true
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(336, 500)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(78, 23)
        Me.btnClose.TabIndex = 26
        Me.btnClose.Text = "Close Plugin"
        Me.btnClose.UseVisualStyleBackColor = true
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = true
        '
        'frmPCControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = true
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(831, 554)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmPCControl"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Progressive Collapse"
        Me.GrpBxKeyElement.ResumeLayout(false)
        Me.GrpBxKeyElement.PerformLayout
        Me.GrpBxCriticalPath.ResumeLayout(false)
        Me.GrpBxCriticalPath.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox11.ResumeLayout(false)
        Me.GroupBox5.ResumeLayout(false)
        Me.GroupBox5.PerformLayout
        Me.GroupBox10.ResumeLayout(false)
        Me.GrpBxDCMethod.ResumeLayout(false)
        Me.GrpBxDCMethod.PerformLayout
        Me.GrpBxMaterialTypeParams.ResumeLayout(false)
        Me.GrpBxMaterialTypeParams.PerformLayout
        Me.GrpBxAnalysisMethod.ResumeLayout(false)
        Me.GrpBxAnalysisMethod.PerformLayout
        Me.GroupBox8.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents btnProgressiveCollapseRun As System.Windows.Forms.Button
    Friend WithEvents GrpBxKeyElement As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_Group As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_Frame As System.Windows.Forms.RadioButton
    Friend WithEvents TreeView_PC As System.Windows.Forms.TreeView
    Friend WithEvents GrpBxCriticalPath As System.Windows.Forms.GroupBox
    Friend WithEvents lblCriticalPathGrp As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox_CriticalPathGroup As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_KeyElementGroup As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_KeyElementFrame As System.Windows.Forms.ComboBox
    Friend WithEvents btn_Resources As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents CheckBox_SaveSeparate As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_FailedGroup As System.Windows.Forms.CheckBox
    Friend WithEvents GrpBxAnalysisMethod As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_CollapsePropagate As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_CollapseInitiate As System.Windows.Forms.RadioButton
    Friend WithEvents GrpBxDCMethod As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_DCDuctility As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_DCStrength As System.Windows.Forms.RadioButton
    Friend WithEvents btnParametersSteel As System.Windows.Forms.Button
    Friend WithEvents GrpBxMaterialTypeParams As System.Windows.Forms.GroupBox
    Friend WithEvents btnParametersColdFormSteel As System.Windows.Forms.Button
    Friend WithEvents btnParametersAluminum As System.Windows.Forms.Button
    Friend WithEvents btnParametersConcrete As System.Windows.Forms.Button
    Friend WithEvents CheckBox_ColdFormSteelFrame As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_AluminumFrame As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_ConcreteFrame As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_SteelFrame As System.Windows.Forms.CheckBox
    Friend WithEvents btnRad_LoadComboGSA As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox_LoadCombo As System.Windows.Forms.ComboBox
    Friend WithEvents btnRad_LoadComboUser As System.Windows.Forms.RadioButton
    Friend WithEvents btnCloseAll As System.Windows.Forms.Button
    Friend WithEvents btnCreateGroupKeyCheck As System.Windows.Forms.Button
    Friend WithEvents btnCreateGroupPath As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents btnDevelopment As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLoadCaseParameters As System.Windows.Forms.Button
    Friend WithEvents btnOptionsTrackProgCol As System.Windows.Forms.Button

End Class
