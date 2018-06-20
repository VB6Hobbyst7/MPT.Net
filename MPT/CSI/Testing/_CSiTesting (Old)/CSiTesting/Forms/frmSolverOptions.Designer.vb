<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSolverOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSolverOptions))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Option_SolverMulti = New System.Windows.Forms.RadioButton()
        Me.Option_SolverAdvanced = New System.Windows.Forms.RadioButton()
        Me.Option_SolverStandard = New System.Windows.Forms.RadioButton()
        Me.Option_SolverDefault = New System.Windows.Forms.RadioButton()
        Me.CheckBox_Delete = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Option_ProcessSeparate = New System.Windows.Forms.RadioButton()
        Me.Option_ProcessSame = New System.Windows.Forms.RadioButton()
        Me.Option_ProcessDefault = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Option_ForceSixtyFour = New System.Windows.Forms.RadioButton()
        Me.Option_ForceThirtyTwo = New System.Windows.Forms.RadioButton()
        Me.Option_ThirtyTwoDefault = New System.Windows.Forms.RadioButton()
        Me.Option_32BitDefault = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Option_32BitDefault.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Option_SolverMulti)
        Me.GroupBox1.Controls.Add(Me.Option_SolverAdvanced)
        Me.GroupBox1.Controls.Add(Me.Option_SolverStandard)
        Me.GroupBox1.Controls.Add(Me.Option_SolverDefault)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(136, 124)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Solver Options"
        '
        'Option_SolverMulti
        '
        Me.Option_SolverMulti.AutoSize = True
        Me.Option_SolverMulti.Location = New System.Drawing.Point(6, 95)
        Me.Option_SolverMulti.Name = "Option_SolverMulti"
        Me.Option_SolverMulti.Size = New System.Drawing.Size(125, 17)
        Me.Option_SolverMulti.TabIndex = 9
        Me.Option_SolverMulti.TabStop = True
        Me.Option_SolverMulti.Text = "Multi-threaded Solver"
        Me.Option_SolverMulti.UseVisualStyleBackColor = True
        '
        'Option_SolverAdvanced
        '
        Me.Option_SolverAdvanced.AutoSize = True
        Me.Option_SolverAdvanced.Location = New System.Drawing.Point(6, 69)
        Me.Option_SolverAdvanced.Name = "Option_SolverAdvanced"
        Me.Option_SolverAdvanced.Size = New System.Drawing.Size(107, 17)
        Me.Option_SolverAdvanced.TabIndex = 8
        Me.Option_SolverAdvanced.TabStop = True
        Me.Option_SolverAdvanced.Text = "Advanced Solver"
        Me.Option_SolverAdvanced.UseVisualStyleBackColor = True
        '
        'Option_SolverStandard
        '
        Me.Option_SolverStandard.AutoSize = True
        Me.Option_SolverStandard.Location = New System.Drawing.Point(6, 45)
        Me.Option_SolverStandard.Name = "Option_SolverStandard"
        Me.Option_SolverStandard.Size = New System.Drawing.Size(101, 17)
        Me.Option_SolverStandard.TabIndex = 7
        Me.Option_SolverStandard.TabStop = True
        Me.Option_SolverStandard.Text = "Standard Solver"
        Me.Option_SolverStandard.UseVisualStyleBackColor = True
        '
        'Option_SolverDefault
        '
        Me.Option_SolverDefault.AutoSize = True
        Me.Option_SolverDefault.Location = New System.Drawing.Point(6, 19)
        Me.Option_SolverDefault.Name = "Option_SolverDefault"
        Me.Option_SolverDefault.Size = New System.Drawing.Size(59, 17)
        Me.Option_SolverDefault.TabIndex = 6
        Me.Option_SolverDefault.TabStop = True
        Me.Option_SolverDefault.Text = "Default"
        Me.Option_SolverDefault.UseVisualStyleBackColor = True
        '
        'CheckBox_Delete
        '
        Me.CheckBox_Delete.AutoSize = True
        Me.CheckBox_Delete.Location = New System.Drawing.Point(331, 114)
        Me.CheckBox_Delete.Name = "CheckBox_Delete"
        Me.CheckBox_Delete.Size = New System.Drawing.Size(122, 17)
        Me.CheckBox_Delete.TabIndex = 7
        Me.CheckBox_Delete.Text = "Delete Analysis Files"
        Me.CheckBox_Delete.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(231, 114)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(150, 114)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Option_ProcessSeparate)
        Me.GroupBox2.Controls.Add(Me.Option_ProcessSame)
        Me.GroupBox2.Controls.Add(Me.Option_ProcessDefault)
        Me.GroupBox2.Location = New System.Drawing.Point(148, 13)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(183, 92)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Analysis Process Options"
        '
        'Option_ProcessSeparate
        '
        Me.Option_ProcessSeparate.AutoSize = True
        Me.Option_ProcessSeparate.Location = New System.Drawing.Point(6, 65)
        Me.Option_ProcessSeparate.Name = "Option_ProcessSeparate"
        Me.Option_ProcessSeparate.Size = New System.Drawing.Size(149, 17)
        Me.Option_ProcessSeparate.TabIndex = 8
        Me.Option_ProcessSeparate.TabStop = True
        Me.Option_ProcessSeparate.Text = "Separate (Out Of) Process"
        Me.Option_ProcessSeparate.UseVisualStyleBackColor = True
        '
        'Option_ProcessSame
        '
        Me.Option_ProcessSame.AutoSize = True
        Me.Option_ProcessSame.Location = New System.Drawing.Point(6, 42)
        Me.Option_ProcessSame.Name = "Option_ProcessSame"
        Me.Option_ProcessSame.Size = New System.Drawing.Size(103, 17)
        Me.Option_ProcessSame.TabIndex = 7
        Me.Option_ProcessSame.TabStop = True
        Me.Option_ProcessSame.Text = "GUI (In) Process"
        Me.Option_ProcessSame.UseVisualStyleBackColor = True
        '
        'Option_ProcessDefault
        '
        Me.Option_ProcessDefault.AutoSize = True
        Me.Option_ProcessDefault.Location = New System.Drawing.Point(6, 19)
        Me.Option_ProcessDefault.Name = "Option_ProcessDefault"
        Me.Option_ProcessDefault.Size = New System.Drawing.Size(59, 17)
        Me.Option_ProcessDefault.TabIndex = 6
        Me.Option_ProcessDefault.TabStop = True
        Me.Option_ProcessDefault.Text = "Default"
        Me.Option_ProcessDefault.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Option_ForceSixtyFour)
        Me.GroupBox4.Controls.Add(Me.Option_ForceThirtyTwo)
        Me.GroupBox4.Controls.Add(Me.Option_ThirtyTwoDefault)
        Me.GroupBox4.Location = New System.Drawing.Point(337, 13)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(116, 92)
        Me.GroupBox4.TabIndex = 11
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "32 Bit"
        '
        'Option_ForceSixtyFour
        '
        Me.Option_ForceSixtyFour.AutoSize = True
        Me.Option_ForceSixtyFour.Location = New System.Drawing.Point(6, 65)
        Me.Option_ForceSixtyFour.Name = "Option_ForceSixtyFour"
        Me.Option_ForceSixtyFour.Size = New System.Drawing.Size(102, 17)
        Me.Option_ForceSixtyFour.TabIndex = 8
        Me.Option_ForceSixtyFour.TabStop = True
        Me.Option_ForceSixtyFour.Text = "64 Bit if Possible"
        Me.Option_ForceSixtyFour.UseVisualStyleBackColor = True
        '
        'Option_ForceThirtyTwo
        '
        Me.Option_ForceThirtyTwo.AutoSize = True
        Me.Option_ForceThirtyTwo.Location = New System.Drawing.Point(6, 42)
        Me.Option_ForceThirtyTwo.Name = "Option_ForceThirtyTwo"
        Me.Option_ForceThirtyTwo.Size = New System.Drawing.Size(82, 17)
        Me.Option_ForceThirtyTwo.TabIndex = 7
        Me.Option_ForceThirtyTwo.TabStop = True
        Me.Option_ForceThirtyTwo.Text = "Force 32 Bit"
        Me.Option_ForceThirtyTwo.UseVisualStyleBackColor = True
        '
        'Option_ThirtyTwoDefault
        '
        Me.Option_ThirtyTwoDefault.AutoSize = True
        Me.Option_ThirtyTwoDefault.Location = New System.Drawing.Point(6, 19)
        Me.Option_ThirtyTwoDefault.Name = "Option_ThirtyTwoDefault"
        Me.Option_ThirtyTwoDefault.Size = New System.Drawing.Size(59, 17)
        Me.Option_ThirtyTwoDefault.TabIndex = 6
        Me.Option_ThirtyTwoDefault.TabStop = True
        Me.Option_ThirtyTwoDefault.Text = "Default"
        Me.Option_ThirtyTwoDefault.UseVisualStyleBackColor = True
        '
        'Option_32BitDefault
        '
        Me.Option_32BitDefault.Controls.Add(Me.GroupBox1)
        Me.Option_32BitDefault.Controls.Add(Me.GroupBox4)
        Me.Option_32BitDefault.Controls.Add(Me.btnOK)
        Me.Option_32BitDefault.Controls.Add(Me.GroupBox2)
        Me.Option_32BitDefault.Controls.Add(Me.btnCancel)
        Me.Option_32BitDefault.Controls.Add(Me.CheckBox_Delete)
        Me.Option_32BitDefault.Location = New System.Drawing.Point(4, 4)
        Me.Option_32BitDefault.Name = "Option_32BitDefault"
        Me.Option_32BitDefault.Size = New System.Drawing.Size(461, 145)
        Me.Option_32BitDefault.TabIndex = 12
        Me.Option_32BitDefault.TabStop = False
        '
        'frmSolverOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(469, 152)
        Me.Controls.Add(Me.Option_32BitDefault)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSolverOptions"
        Me.Text = "Solver Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Option_32BitDefault.ResumeLayout(False)
        Me.Option_32BitDefault.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Option_SolverMulti As System.Windows.Forms.RadioButton
    Friend WithEvents Option_SolverAdvanced As System.Windows.Forms.RadioButton
    Friend WithEvents Option_SolverStandard As System.Windows.Forms.RadioButton
    Friend WithEvents Option_SolverDefault As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox_Delete As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Option_ProcessSeparate As System.Windows.Forms.RadioButton
    Friend WithEvents Option_ProcessSame As System.Windows.Forms.RadioButton
    Friend WithEvents Option_ProcessDefault As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Option_ForceSixtyFour As System.Windows.Forms.RadioButton
    Friend WithEvents Option_ForceThirtyTwo As System.Windows.Forms.RadioButton
    Friend WithEvents Option_ThirtyTwoDefault As System.Windows.Forms.RadioButton
    Friend WithEvents Option_32BitDefault As System.Windows.Forms.GroupBox
End Class
