<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialTypeParameters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterialTypeParameters))
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnRad_OverStrengthUserSpecified = New System.Windows.Forms.RadioButton()
        Me.btnRad_OverStrengthCodeDefault = New System.Windows.Forms.RadioButton()
        Me.btnRad_OverStrengthNone = New System.Windows.Forms.RadioButton()
        Me.TextBox_OverStrength = New System.Windows.Forms.TextBox()
        Me.ComboBox_CodesList = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GrpBxDuctilityCriteria = New System.Windows.Forms.GroupBox()
        Me.TextBox_TableName = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkBxAccptCriteria = New System.Windows.Forms.CheckBox()
        Me.chkBxHingeState = New System.Windows.Forms.CheckBox()
        Me.ComboBox_AccptCriteria = New System.Windows.Forms.ComboBox()
        Me.ComboBox_HingeState = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GrpBxStrengthCriteria = New System.Windows.Forms.GroupBox()
        Me.CheckBox_UseSeismic = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GrpBxDuctilityCriteria.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GrpBxStrengthCriteria.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnRad_OverStrengthUserSpecified)
        Me.GroupBox5.Controls.Add(Me.btnRad_OverStrengthCodeDefault)
        Me.GroupBox5.Controls.Add(Me.btnRad_OverStrengthNone)
        Me.GroupBox5.Controls.Add(Me.TextBox_OverStrength)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 90)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(144, 100)
        Me.GroupBox5.TabIndex = 39
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Over Strength Factor"
        '
        'btnRad_OverStrengthUserSpecified
        '
        Me.btnRad_OverStrengthUserSpecified.AutoSize = True
        Me.btnRad_OverStrengthUserSpecified.Location = New System.Drawing.Point(6, 65)
        Me.btnRad_OverStrengthUserSpecified.Name = "btnRad_OverStrengthUserSpecified"
        Me.btnRad_OverStrengthUserSpecified.Size = New System.Drawing.Size(47, 17)
        Me.btnRad_OverStrengthUserSpecified.TabIndex = 34
        Me.btnRad_OverStrengthUserSpecified.Text = "User"
        Me.btnRad_OverStrengthUserSpecified.UseVisualStyleBackColor = True
        '
        'btnRad_OverStrengthCodeDefault
        '
        Me.btnRad_OverStrengthCodeDefault.AutoSize = True
        Me.btnRad_OverStrengthCodeDefault.Location = New System.Drawing.Point(6, 42)
        Me.btnRad_OverStrengthCodeDefault.Name = "btnRad_OverStrengthCodeDefault"
        Me.btnRad_OverStrengthCodeDefault.Size = New System.Drawing.Size(126, 17)
        Me.btnRad_OverStrengthCodeDefault.TabIndex = 33
        Me.btnRad_OverStrengthCodeDefault.Text = "Code Seismic Default"
        Me.btnRad_OverStrengthCodeDefault.UseVisualStyleBackColor = True
        '
        'btnRad_OverStrengthNone
        '
        Me.btnRad_OverStrengthNone.AutoSize = True
        Me.btnRad_OverStrengthNone.Location = New System.Drawing.Point(6, 19)
        Me.btnRad_OverStrengthNone.Name = "btnRad_OverStrengthNone"
        Me.btnRad_OverStrengthNone.Size = New System.Drawing.Size(51, 17)
        Me.btnRad_OverStrengthNone.TabIndex = 32
        Me.btnRad_OverStrengthNone.Text = "None"
        Me.btnRad_OverStrengthNone.UseVisualStyleBackColor = True
        '
        'TextBox_OverStrength
        '
        Me.TextBox_OverStrength.Location = New System.Drawing.Point(63, 64)
        Me.TextBox_OverStrength.Name = "TextBox_OverStrength"
        Me.TextBox_OverStrength.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_OverStrength.TabIndex = 31
        '
        'ComboBox_CodesList
        '
        Me.ComboBox_CodesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_CodesList.FormattingEnabled = True
        Me.ComboBox_CodesList.Location = New System.Drawing.Point(6, 33)
        Me.ComboBox_CodesList.MaxDropDownItems = 50
        Me.ComboBox_CodesList.Name = "ComboBox_CodesList"
        Me.ComboBox_CodesList.Size = New System.Drawing.Size(144, 21)
        Me.ComboBox_CodesList.TabIndex = 42
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GrpBxDuctilityCriteria)
        Me.GroupBox1.Controls.Add(Me.GrpBxStrengthCriteria)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(451, 251)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "DC Method"
        '
        'GrpBxDuctilityCriteria
        '
        Me.GrpBxDuctilityCriteria.Controls.Add(Me.TextBox_TableName)
        Me.GrpBxDuctilityCriteria.Controls.Add(Me.GroupBox2)
        Me.GrpBxDuctilityCriteria.Controls.Add(Me.Label13)
        Me.GrpBxDuctilityCriteria.Location = New System.Drawing.Point(171, 19)
        Me.GrpBxDuctilityCriteria.Name = "GrpBxDuctilityCriteria"
        Me.GrpBxDuctilityCriteria.Size = New System.Drawing.Size(269, 226)
        Me.GrpBxDuctilityCriteria.TabIndex = 45
        Me.GrpBxDuctilityCriteria.TabStop = False
        Me.GrpBxDuctilityCriteria.Text = "Ductility Criteria"
        '
        'TextBox_TableName
        '
        Me.TextBox_TableName.Location = New System.Drawing.Point(129, 196)
        Me.TextBox_TableName.Name = "TextBox_TableName"
        Me.TextBox_TableName.Size = New System.Drawing.Size(130, 20)
        Me.TextBox_TableName.TabIndex = 20
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.chkBxAccptCriteria)
        Me.GroupBox2.Controls.Add(Me.chkBxHingeState)
        Me.GroupBox2.Controls.Add(Me.ComboBox_AccptCriteria)
        Me.GroupBox2.Controls.Add(Me.ComboBox_HingeState)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(244, 171)
        Me.GroupBox2.TabIndex = 41
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Hinge Limits"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 147)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 13)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "hinges."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(183, 13)
        Me.Label8.TabIndex = 48
        Me.Label8.Text = "the Hinge State will be used for those"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(186, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Note: If fiber hinges exist in the model,"
        '
        'chkBxAccptCriteria
        '
        Me.chkBxAccptCriteria.AutoSize = True
        Me.chkBxAccptCriteria.Location = New System.Drawing.Point(6, 66)
        Me.chkBxAccptCriteria.Name = "chkBxAccptCriteria"
        Me.chkBxAccptCriteria.Size = New System.Drawing.Size(15, 14)
        Me.chkBxAccptCriteria.TabIndex = 46
        Me.chkBxAccptCriteria.UseVisualStyleBackColor = True
        '
        'chkBxHingeState
        '
        Me.chkBxHingeState.AutoSize = True
        Me.chkBxHingeState.Location = New System.Drawing.Point(6, 19)
        Me.chkBxHingeState.Name = "chkBxHingeState"
        Me.chkBxHingeState.Size = New System.Drawing.Size(15, 14)
        Me.chkBxHingeState.TabIndex = 45
        Me.chkBxHingeState.UseVisualStyleBackColor = True
        '
        'ComboBox_AccptCriteria
        '
        Me.ComboBox_AccptCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_AccptCriteria.FormattingEnabled = True
        Me.ComboBox_AccptCriteria.Location = New System.Drawing.Point(25, 82)
        Me.ComboBox_AccptCriteria.Name = "ComboBox_AccptCriteria"
        Me.ComboBox_AccptCriteria.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_AccptCriteria.TabIndex = 15
        '
        'ComboBox_HingeState
        '
        Me.ComboBox_HingeState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_HingeState.FormattingEnabled = True
        Me.ComboBox_HingeState.Location = New System.Drawing.Point(25, 35)
        Me.ComboBox_HingeState.Name = "ComboBox_HingeState"
        Me.ComboBox_HingeState.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_HingeState.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(133, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Acceptance Criteria Status"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Hinge State"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 199)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(111, 13)
        Me.Label13.TabIndex = 21
        Me.Label13.Text = "Table Name (optional)"
        '
        'GrpBxStrengthCriteria
        '
        Me.GrpBxStrengthCriteria.Controls.Add(Me.CheckBox_UseSeismic)
        Me.GrpBxStrengthCriteria.Controls.Add(Me.Label1)
        Me.GrpBxStrengthCriteria.Controls.Add(Me.ComboBox_CodesList)
        Me.GrpBxStrengthCriteria.Controls.Add(Me.GroupBox5)
        Me.GrpBxStrengthCriteria.Location = New System.Drawing.Point(6, 19)
        Me.GrpBxStrengthCriteria.Name = "GrpBxStrengthCriteria"
        Me.GrpBxStrengthCriteria.Size = New System.Drawing.Size(159, 196)
        Me.GrpBxStrengthCriteria.TabIndex = 44
        Me.GrpBxStrengthCriteria.TabStop = False
        Me.GrpBxStrengthCriteria.Text = "Strength Criteria"
        '
        'CheckBox_UseSeismic
        '
        Me.CheckBox_UseSeismic.AutoSize = True
        Me.CheckBox_UseSeismic.Location = New System.Drawing.Point(7, 61)
        Me.CheckBox_UseSeismic.Name = "CheckBox_UseSeismic"
        Me.CheckBox_UseSeismic.Size = New System.Drawing.Size(112, 17)
        Me.CheckBox_UseSeismic.TabIndex = 44
        Me.CheckBox_UseSeismic.Text = "Use Seismic Code"
        Me.CheckBox_UseSeismic.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Design Code"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(164, 265)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 44
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(245, 265)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 45
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox1)
        Me.GroupBox6.Controls.Add(Me.btnCancel)
        Me.GroupBox6.Controls.Add(Me.btnOK)
        Me.GroupBox6.Location = New System.Drawing.Point(2, 2)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(467, 295)
        Me.GroupBox6.TabIndex = 46
        Me.GroupBox6.TabStop = False
        '
        'frmMaterialTypeParameters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 302)
        Me.Controls.Add(Me.GroupBox6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMaterialTypeParameters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Material Type Parameters"
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GrpBxDuctilityCriteria.ResumeLayout(False)
        Me.GrpBxDuctilityCriteria.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GrpBxStrengthCriteria.ResumeLayout(False)
        Me.GrpBxStrengthCriteria.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_OverStrengthUserSpecified As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_OverStrengthCodeDefault As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_OverStrengthNone As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_OverStrength As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox_CodesList As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GrpBxDuctilityCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents GrpBxStrengthCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_UseSeismic As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkBxAccptCriteria As System.Windows.Forms.CheckBox
    Friend WithEvents chkBxHingeState As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBox_AccptCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox_HingeState As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TableName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
