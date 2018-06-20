<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadCaseParameters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadCaseParameters))
        Me.TextBox_DampingRatio = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_TimeStepSize = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_TimeNumberStep = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnRad_PDeltaLargeDisp = New System.Windows.Forms.RadioButton()
        Me.btnRad_PDelta = New System.Windows.Forms.RadioButton()
        Me.btnRad_None = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnRad_TimeNumOutputSteps = New System.Windows.Forms.RadioButton()
        Me.btnRad_TimeTotal = New System.Windows.Forms.RadioButton()
        Me.btnRad_TimeFreeVibration = New System.Windows.Forms.RadioButton()
        Me.btnRad_Ratio = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox_TimeTotal = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox_TimeFreeVibration = New System.Windows.Forms.TextBox()
        Me.TextBox_TimeRatios = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_TimeUnload = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox_DampingRatio
        '
        Me.TextBox_DampingRatio.Location = New System.Drawing.Point(230, 16)
        Me.TextBox_DampingRatio.Name = "TextBox_DampingRatio"
        Me.TextBox_DampingRatio.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_DampingRatio.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Damping Ratio"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBox_DampingRatio)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 307)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(339, 45)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Other Parameters"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Output Time Step Size"
        '
        'TextBox_TimeStepSize
        '
        Me.TextBox_TimeStepSize.Location = New System.Drawing.Point(230, 19)
        Me.TextBox_TimeStepSize.Name = "TextBox_TimeStepSize"
        Me.TextBox_TimeStepSize.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_TimeStepSize.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Number of Output Time Steps"
        '
        'TextBox_TimeNumberStep
        '
        Me.TextBox_TimeNumberStep.Location = New System.Drawing.Point(230, 149)
        Me.TextBox_TimeNumberStep.Name = "TextBox_TimeNumberStep"
        Me.TextBox_TimeNumberStep.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_TimeNumberStep.TabIndex = 33
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnRad_PDeltaLargeDisp)
        Me.GroupBox2.Controls.Add(Me.btnRad_PDelta)
        Me.GroupBox2.Controls.Add(Me.btnRad_None)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(339, 100)
        Me.GroupBox2.TabIndex = 34
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Geometric Nonlinearity Parameters"
        '
        'btnRad_PDeltaLargeDisp
        '
        Me.btnRad_PDeltaLargeDisp.AutoSize = True
        Me.btnRad_PDeltaLargeDisp.Location = New System.Drawing.Point(14, 65)
        Me.btnRad_PDeltaLargeDisp.Name = "btnRad_PDeltaLargeDisp"
        Me.btnRad_PDeltaLargeDisp.Size = New System.Drawing.Size(184, 17)
        Me.btnRad_PDeltaLargeDisp.TabIndex = 2
        Me.btnRad_PDeltaLargeDisp.TabStop = True
        Me.btnRad_PDeltaLargeDisp.Text = "P-Delta plus Large Displacements"
        Me.btnRad_PDeltaLargeDisp.UseVisualStyleBackColor = True
        '
        'btnRad_PDelta
        '
        Me.btnRad_PDelta.AutoSize = True
        Me.btnRad_PDelta.Location = New System.Drawing.Point(14, 42)
        Me.btnRad_PDelta.Name = "btnRad_PDelta"
        Me.btnRad_PDelta.Size = New System.Drawing.Size(60, 17)
        Me.btnRad_PDelta.TabIndex = 1
        Me.btnRad_PDelta.TabStop = True
        Me.btnRad_PDelta.Text = "P-Delta"
        Me.btnRad_PDelta.UseVisualStyleBackColor = True
        '
        'btnRad_None
        '
        Me.btnRad_None.AutoSize = True
        Me.btnRad_None.Location = New System.Drawing.Point(14, 19)
        Me.btnRad_None.Name = "btnRad_None"
        Me.btnRad_None.Size = New System.Drawing.Size(51, 17)
        Me.btnRad_None.TabIndex = 0
        Me.btnRad_None.TabStop = True
        Me.btnRad_None.Text = "None"
        Me.btnRad_None.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.btnRad_TimeNumOutputSteps)
        Me.GroupBox3.Controls.Add(Me.btnRad_TimeTotal)
        Me.GroupBox3.Controls.Add(Me.btnRad_TimeFreeVibration)
        Me.GroupBox3.Controls.Add(Me.btnRad_Ratio)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeNumberStep)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeTotal)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeStepSize)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeFreeVibration)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeRatios)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.TextBox_TimeUnload)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 118)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(339, 183)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Time Parameters"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(304, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 13)
        Me.Label11.TabIndex = 48
        Me.Label11.Text = "Sec"
        '
        'btnRad_TimeNumOutputSteps
        '
        Me.btnRad_TimeNumOutputSteps.AutoSize = True
        Me.btnRad_TimeNumOutputSteps.Location = New System.Drawing.Point(210, 152)
        Me.btnRad_TimeNumOutputSteps.Name = "btnRad_TimeNumOutputSteps"
        Me.btnRad_TimeNumOutputSteps.Size = New System.Drawing.Size(14, 13)
        Me.btnRad_TimeNumOutputSteps.TabIndex = 47
        Me.btnRad_TimeNumOutputSteps.TabStop = True
        Me.btnRad_TimeNumOutputSteps.UseVisualStyleBackColor = True
        '
        'btnRad_TimeTotal
        '
        Me.btnRad_TimeTotal.AutoSize = True
        Me.btnRad_TimeTotal.Location = New System.Drawing.Point(210, 126)
        Me.btnRad_TimeTotal.Name = "btnRad_TimeTotal"
        Me.btnRad_TimeTotal.Size = New System.Drawing.Size(14, 13)
        Me.btnRad_TimeTotal.TabIndex = 46
        Me.btnRad_TimeTotal.TabStop = True
        Me.btnRad_TimeTotal.UseVisualStyleBackColor = True
        '
        'btnRad_TimeFreeVibration
        '
        Me.btnRad_TimeFreeVibration.AutoSize = True
        Me.btnRad_TimeFreeVibration.Location = New System.Drawing.Point(210, 100)
        Me.btnRad_TimeFreeVibration.Name = "btnRad_TimeFreeVibration"
        Me.btnRad_TimeFreeVibration.Size = New System.Drawing.Size(14, 13)
        Me.btnRad_TimeFreeVibration.TabIndex = 45
        Me.btnRad_TimeFreeVibration.TabStop = True
        Me.btnRad_TimeFreeVibration.UseVisualStyleBackColor = True
        '
        'btnRad_Ratio
        '
        Me.btnRad_Ratio.AutoSize = True
        Me.btnRad_Ratio.Location = New System.Drawing.Point(210, 74)
        Me.btnRad_Ratio.Name = "btnRad_Ratio"
        Me.btnRad_Ratio.Size = New System.Drawing.Size(14, 13)
        Me.btnRad_Ratio.TabIndex = 44
        Me.btnRad_Ratio.TabStop = True
        Me.btnRad_Ratio.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(304, 126)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(26, 13)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Sec"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(157, 13)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Total Time of Time History Case"
        '
        'TextBox_TimeTotal
        '
        Me.TextBox_TimeTotal.Enabled = False
        Me.TextBox_TimeTotal.Location = New System.Drawing.Point(230, 123)
        Me.TextBox_TimeTotal.Name = "TextBox_TimeTotal"
        Me.TextBox_TimeTotal.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_TimeTotal.TabIndex = 41
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(304, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(26, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Sec"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(194, 13)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Time of Free Vibration of Failed Member"
        '
        'TextBox_TimeFreeVibration
        '
        Me.TextBox_TimeFreeVibration.Location = New System.Drawing.Point(230, 97)
        Me.TextBox_TimeFreeVibration.Name = "TextBox_TimeFreeVibration"
        Me.TextBox_TimeFreeVibration.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_TimeFreeVibration.TabIndex = 38
        '
        'TextBox_TimeRatios
        '
        Me.TextBox_TimeRatios.Location = New System.Drawing.Point(230, 71)
        Me.TextBox_TimeRatios.Name = "TextBox_TimeRatios"
        Me.TextBox_TimeRatios.Size = New System.Drawing.Size(39, 20)
        Me.TextBox_TimeRatios.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(177, 13)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Ratio of Free Vibration/Unload Time"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(304, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Sec"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Time to Unload Failed Member"
        '
        'TextBox_TimeUnload
        '
        Me.TextBox_TimeUnload.Location = New System.Drawing.Point(230, 45)
        Me.TextBox_TimeUnload.Name = "TextBox_TimeUnload"
        Me.TextBox_TimeUnload.Size = New System.Drawing.Size(69, 20)
        Me.TextBox_TimeUnload.TabIndex = 33
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(85, 358)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 35
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(188, 358)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 36
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmLoadCaseParameters
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(366, 391)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLoadCaseParameters"
        Me.ShowInTaskbar = False
        Me.Text = "Failed Member Load Case Parameters"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextBox_DampingRatio As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeStepSize As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeNumberStep As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_PDeltaLargeDisp As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_PDelta As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_None As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRad_TimeNumOutputSteps As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_TimeTotal As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_TimeFreeVibration As System.Windows.Forms.RadioButton
    Friend WithEvents btnRad_Ratio As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeFreeVibration As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_TimeRatios As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeUnload As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
