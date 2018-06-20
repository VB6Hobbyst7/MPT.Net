<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestSuiteSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTestSuiteSetup))
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnAdvanced = New System.Windows.Forms.Button()
        Me.FrameModels = New System.Windows.Forms.GroupBox()
        Me.btnDistTestOpt = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.FrameSource = New System.Windows.Forms.GroupBox()
        Me.FramePath = New System.Windows.Forms.GroupBox()
        Me.Option_SourceAbsolute = New System.Windows.Forms.RadioButton()
        Me.Option_SourceRelative = New System.Windows.Forms.RadioButton()
        Me.TextBox_Source = New System.Windows.Forms.TextBox()
        Me.btnBrowseSource = New System.Windows.Forms.Button()
        Me.FrameDestination = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Option_DestinationAbsolute = New System.Windows.Forms.RadioButton()
        Me.Option_DestinationRelative = New System.Windows.Forms.RadioButton()
        Me.TextBox_Destination = New System.Windows.Forms.TextBox()
        Me.btnBrowseDestination = New System.Windows.Forms.Button()
        Me.Frame_TestingType = New System.Windows.Forms.GroupBox()
        Me.btnSelectModels = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Option_CopyModels = New System.Windows.Forms.RadioButton()
        Me.CheckBox_CopyModelsFlat = New System.Windows.Forms.CheckBox()
        Me.Option_CopyModelsNo = New System.Windows.Forms.RadioButton()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.frameGeneral = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox_Programs = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_TestID = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_TestName = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox7.SuspendLayout()
        Me.FrameModels.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.FrameSource.SuspendLayout()
        Me.FramePath.SuspendLayout()
        Me.FrameDestination.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Frame_TestingType.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.frameGeneral.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.btnAdvanced)
        Me.GroupBox7.Controls.Add(Me.FrameModels)
        Me.GroupBox7.Controls.Add(Me.btnCancel)
        Me.GroupBox7.Controls.Add(Me.frameGeneral)
        Me.GroupBox7.Controls.Add(Me.btnOK)
        Me.GroupBox7.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(583, 375)
        Me.GroupBox7.TabIndex = 8
        Me.GroupBox7.TabStop = False
        '
        'btnAdvanced
        '
        Me.btnAdvanced.Location = New System.Drawing.Point(12, 344)
        Me.btnAdvanced.Name = "btnAdvanced"
        Me.btnAdvanced.Size = New System.Drawing.Size(117, 23)
        Me.btnAdvanced.TabIndex = 6
        Me.btnAdvanced.Text = "Advanced Settings"
        Me.btnAdvanced.UseVisualStyleBackColor = True
        '
        'FrameModels
        '
        Me.FrameModels.Controls.Add(Me.btnDistTestOpt)
        Me.FrameModels.Controls.Add(Me.GroupBox3)
        Me.FrameModels.Controls.Add(Me.Frame_TestingType)
        Me.FrameModels.Location = New System.Drawing.Point(12, 128)
        Me.FrameModels.Name = "FrameModels"
        Me.FrameModels.Size = New System.Drawing.Size(565, 212)
        Me.FrameModels.TabIndex = 0
        Me.FrameModels.TabStop = False
        Me.FrameModels.Text = "Models Setup"
        '
        'btnDistTestOpt
        '
        Me.btnDistTestOpt.Location = New System.Drawing.Point(364, 158)
        Me.btnDistTestOpt.Name = "btnDistTestOpt"
        Me.btnDistTestOpt.Size = New System.Drawing.Size(166, 23)
        Me.btnDistTestOpt.TabIndex = 8
        Me.btnDistTestOpt.Text = "Distributed Testing Setup"
        Me.btnDistTestOpt.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.FrameSource)
        Me.GroupBox3.Controls.Add(Me.FrameDestination)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 14)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(323, 182)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Model Paths"
        '
        'FrameSource
        '
        Me.FrameSource.Controls.Add(Me.FramePath)
        Me.FrameSource.Controls.Add(Me.TextBox_Source)
        Me.FrameSource.Controls.Add(Me.btnBrowseSource)
        Me.FrameSource.Location = New System.Drawing.Point(6, 19)
        Me.FrameSource.Name = "FrameSource"
        Me.FrameSource.Size = New System.Drawing.Size(308, 79)
        Me.FrameSource.TabIndex = 2
        Me.FrameSource.TabStop = False
        Me.FrameSource.Text = "Source"
        '
        'FramePath
        '
        Me.FramePath.Controls.Add(Me.Option_SourceAbsolute)
        Me.FramePath.Controls.Add(Me.Option_SourceRelative)
        Me.FramePath.Location = New System.Drawing.Point(129, 9)
        Me.FramePath.Name = "FramePath"
        Me.FramePath.Size = New System.Drawing.Size(155, 39)
        Me.FramePath.TabIndex = 11
        Me.FramePath.TabStop = False
        Me.FramePath.Text = "Path"
        '
        'Option_SourceAbsolute
        '
        Me.Option_SourceAbsolute.AutoSize = True
        Me.Option_SourceAbsolute.Location = New System.Drawing.Point(73, 14)
        Me.Option_SourceAbsolute.Name = "Option_SourceAbsolute"
        Me.Option_SourceAbsolute.Size = New System.Drawing.Size(66, 17)
        Me.Option_SourceAbsolute.TabIndex = 1
        Me.Option_SourceAbsolute.TabStop = True
        Me.Option_SourceAbsolute.Text = "Absolute"
        Me.Option_SourceAbsolute.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Option_SourceAbsolute.UseVisualStyleBackColor = True
        '
        'Option_SourceRelative
        '
        Me.Option_SourceRelative.AutoSize = True
        Me.Option_SourceRelative.Location = New System.Drawing.Point(6, 13)
        Me.Option_SourceRelative.Name = "Option_SourceRelative"
        Me.Option_SourceRelative.Size = New System.Drawing.Size(64, 17)
        Me.Option_SourceRelative.TabIndex = 0
        Me.Option_SourceRelative.TabStop = True
        Me.Option_SourceRelative.Text = "Relative"
        Me.Option_SourceRelative.UseVisualStyleBackColor = True
        '
        'TextBox_Source
        '
        Me.TextBox_Source.Location = New System.Drawing.Point(6, 52)
        Me.TextBox_Source.Name = "TextBox_Source"
        Me.TextBox_Source.Size = New System.Drawing.Size(296, 20)
        Me.TextBox_Source.TabIndex = 10
        '
        'btnBrowseSource
        '
        Me.btnBrowseSource.Location = New System.Drawing.Point(6, 19)
        Me.btnBrowseSource.Name = "btnBrowseSource"
        Me.btnBrowseSource.Size = New System.Drawing.Size(117, 23)
        Me.btnBrowseSource.TabIndex = 7
        Me.btnBrowseSource.Text = "Browse"
        Me.btnBrowseSource.UseVisualStyleBackColor = True
        '
        'FrameDestination
        '
        Me.FrameDestination.Controls.Add(Me.GroupBox5)
        Me.FrameDestination.Controls.Add(Me.TextBox_Destination)
        Me.FrameDestination.Controls.Add(Me.btnBrowseDestination)
        Me.FrameDestination.Location = New System.Drawing.Point(6, 97)
        Me.FrameDestination.Name = "FrameDestination"
        Me.FrameDestination.Size = New System.Drawing.Size(308, 79)
        Me.FrameDestination.TabIndex = 12
        Me.FrameDestination.TabStop = False
        Me.FrameDestination.Text = "Destination"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Option_DestinationAbsolute)
        Me.GroupBox5.Controls.Add(Me.Option_DestinationRelative)
        Me.GroupBox5.Location = New System.Drawing.Point(129, 9)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(155, 39)
        Me.GroupBox5.TabIndex = 11
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Path"
        '
        'Option_DestinationAbsolute
        '
        Me.Option_DestinationAbsolute.AutoSize = True
        Me.Option_DestinationAbsolute.Location = New System.Drawing.Point(73, 14)
        Me.Option_DestinationAbsolute.Name = "Option_DestinationAbsolute"
        Me.Option_DestinationAbsolute.Size = New System.Drawing.Size(66, 17)
        Me.Option_DestinationAbsolute.TabIndex = 1
        Me.Option_DestinationAbsolute.TabStop = True
        Me.Option_DestinationAbsolute.Text = "Absolute"
        Me.Option_DestinationAbsolute.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Option_DestinationAbsolute.UseVisualStyleBackColor = True
        '
        'Option_DestinationRelative
        '
        Me.Option_DestinationRelative.AutoSize = True
        Me.Option_DestinationRelative.Location = New System.Drawing.Point(6, 13)
        Me.Option_DestinationRelative.Name = "Option_DestinationRelative"
        Me.Option_DestinationRelative.Size = New System.Drawing.Size(64, 17)
        Me.Option_DestinationRelative.TabIndex = 0
        Me.Option_DestinationRelative.TabStop = True
        Me.Option_DestinationRelative.Text = "Relative"
        Me.Option_DestinationRelative.UseVisualStyleBackColor = True
        '
        'TextBox_Destination
        '
        Me.TextBox_Destination.Location = New System.Drawing.Point(6, 52)
        Me.TextBox_Destination.Name = "TextBox_Destination"
        Me.TextBox_Destination.Size = New System.Drawing.Size(296, 20)
        Me.TextBox_Destination.TabIndex = 10
        '
        'btnBrowseDestination
        '
        Me.btnBrowseDestination.Location = New System.Drawing.Point(6, 19)
        Me.btnBrowseDestination.Name = "btnBrowseDestination"
        Me.btnBrowseDestination.Size = New System.Drawing.Size(117, 23)
        Me.btnBrowseDestination.TabIndex = 7
        Me.btnBrowseDestination.Text = "Browse"
        Me.btnBrowseDestination.UseVisualStyleBackColor = True
        '
        'Frame_TestingType
        '
        Me.Frame_TestingType.Controls.Add(Me.btnSelectModels)
        Me.Frame_TestingType.Controls.Add(Me.GroupBox6)
        Me.Frame_TestingType.Location = New System.Drawing.Point(335, 14)
        Me.Frame_TestingType.Name = "Frame_TestingType"
        Me.Frame_TestingType.Size = New System.Drawing.Size(207, 129)
        Me.Frame_TestingType.TabIndex = 3
        Me.Frame_TestingType.TabStop = False
        Me.Frame_TestingType.Text = "Models Used"
        '
        'btnSelectModels
        '
        Me.btnSelectModels.Location = New System.Drawing.Point(17, 18)
        Me.btnSelectModels.Name = "btnSelectModels"
        Me.btnSelectModels.Size = New System.Drawing.Size(166, 23)
        Me.btnSelectModels.TabIndex = 7
        Me.btnSelectModels.Text = "(TBA) Select Models for Suite"
        Me.btnSelectModels.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Option_CopyModels)
        Me.GroupBox6.Controls.Add(Me.CheckBox_CopyModelsFlat)
        Me.GroupBox6.Controls.Add(Me.Option_CopyModelsNo)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 39)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(195, 82)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        '
        'Option_CopyModels
        '
        Me.Option_CopyModels.AutoSize = True
        Me.Option_CopyModels.Location = New System.Drawing.Point(6, 12)
        Me.Option_CopyModels.Name = "Option_CopyModels"
        Me.Option_CopyModels.Size = New System.Drawing.Size(171, 17)
        Me.Option_CopyModels.TabIndex = 1
        Me.Option_CopyModels.Text = "Copy New Models from Source"
        Me.Option_CopyModels.UseVisualStyleBackColor = True
        '
        'CheckBox_CopyModelsFlat
        '
        Me.CheckBox_CopyModelsFlat.AutoSize = True
        Me.CheckBox_CopyModelsFlat.Location = New System.Drawing.Point(23, 36)
        Me.CheckBox_CopyModelsFlat.Name = "CheckBox_CopyModelsFlat"
        Me.CheckBox_CopyModelsFlat.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox_CopyModelsFlat.TabIndex = 3
        Me.CheckBox_CopyModelsFlat.Text = "Copy Models Flat"
        Me.CheckBox_CopyModelsFlat.UseVisualStyleBackColor = True
        '
        'Option_CopyModelsNo
        '
        Me.Option_CopyModelsNo.AutoSize = True
        Me.Option_CopyModelsNo.Location = New System.Drawing.Point(6, 57)
        Me.Option_CopyModelsNo.Name = "Option_CopyModelsNo"
        Me.Option_CopyModelsNo.Size = New System.Drawing.Size(188, 17)
        Me.Option_CopyModelsNo.TabIndex = 2
        Me.Option_CopyModelsNo.Text = "Use Existing Models at Destination"
        Me.Option_CopyModelsNo.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(502, 344)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frameGeneral
        '
        Me.frameGeneral.Controls.Add(Me.Label3)
        Me.frameGeneral.Controls.Add(Me.ComboBox_Programs)
        Me.frameGeneral.Controls.Add(Me.Label2)
        Me.frameGeneral.Controls.Add(Me.TextBox_TestID)
        Me.frameGeneral.Controls.Add(Me.Label1)
        Me.frameGeneral.Controls.Add(Me.TextBox_TestName)
        Me.frameGeneral.Location = New System.Drawing.Point(12, 19)
        Me.frameGeneral.Name = "frameGeneral"
        Me.frameGeneral.Size = New System.Drawing.Size(565, 106)
        Me.frameGeneral.TabIndex = 1
        Me.frameGeneral.TabStop = False
        Me.frameGeneral.Text = "General"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 20)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Program to Run"
        '
        'ComboBox_Programs
        '
        Me.ComboBox_Programs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_Programs.FormattingEnabled = True
        Me.ComboBox_Programs.Location = New System.Drawing.Point(144, 16)
        Me.ComboBox_Programs.Name = "ComboBox_Programs"
        Me.ComboBox_Programs.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_Programs.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(410, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Test ID"
        '
        'TextBox_TestID
        '
        Me.TextBox_TestID.Location = New System.Drawing.Point(413, 62)
        Me.TextBox_TestID.Name = "TextBox_TestID"
        Me.TextBox_TestID.Size = New System.Drawing.Size(146, 20)
        Me.TextBox_TestID.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Test Name"
        '
        'TextBox_TestName
        '
        Me.TextBox_TestName.Location = New System.Drawing.Point(6, 62)
        Me.TextBox_TestName.Name = "TextBox_TestName"
        Me.TextBox_TestName.Size = New System.Drawing.Size(401, 20)
        Me.TextBox_TestName.TabIndex = 11
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(421, 344)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmTestSuiteSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(597, 388)
        Me.Controls.Add(Me.GroupBox7)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTestSuiteSetup"
        Me.Text = "Test Suite Setup"
        Me.GroupBox7.ResumeLayout(False)
        Me.FrameModels.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.FrameSource.ResumeLayout(False)
        Me.FrameSource.PerformLayout()
        Me.FramePath.ResumeLayout(False)
        Me.FramePath.PerformLayout()
        Me.FrameDestination.ResumeLayout(False)
        Me.FrameDestination.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Frame_TestingType.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.frameGeneral.ResumeLayout(False)
        Me.frameGeneral.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdvanced As System.Windows.Forms.Button
    Friend WithEvents FrameModels As System.Windows.Forms.GroupBox
    Friend WithEvents btnDistTestOpt As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents FrameSource As System.Windows.Forms.GroupBox
    Friend WithEvents FramePath As System.Windows.Forms.GroupBox
    Friend WithEvents Option_SourceAbsolute As System.Windows.Forms.RadioButton
    Friend WithEvents Option_SourceRelative As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_Source As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseSource As System.Windows.Forms.Button
    Friend WithEvents FrameDestination As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Option_DestinationAbsolute As System.Windows.Forms.RadioButton
    Friend WithEvents Option_DestinationRelative As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_Destination As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowseDestination As System.Windows.Forms.Button
    Friend WithEvents Frame_TestingType As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectModels As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Option_CopyModels As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox_CopyModelsFlat As System.Windows.Forms.CheckBox
    Friend WithEvents Option_CopyModelsNo As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents frameGeneral As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_Programs As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TestID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TestName As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class
