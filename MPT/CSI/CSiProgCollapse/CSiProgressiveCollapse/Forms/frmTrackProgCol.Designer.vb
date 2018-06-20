<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrackProgCol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrackProgCol))
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox_LimitPropagation = New System.Windows.Forms.CheckBox()
        Me.TextBox_ItNumMax = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox_TimeStepMin = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox_TimeStepScaleFactorInitial = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_TimeStepScaleFactor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_FailureFrameLimit = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Max Failure Events"
        '
        'CheckBox_LimitPropagation
        '
        Me.CheckBox_LimitPropagation.AutoSize = True
        Me.CheckBox_LimitPropagation.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_LimitPropagation.Name = "CheckBox_LimitPropagation"
        Me.CheckBox_LimitPropagation.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox_LimitPropagation.TabIndex = 39
        Me.CheckBox_LimitPropagation.Text = "Limit Propagation"
        Me.CheckBox_LimitPropagation.UseVisualStyleBackColor = True
        '
        'TextBox_ItNumMax
        '
        Me.TextBox_ItNumMax.Location = New System.Drawing.Point(231, 40)
        Me.TextBox_ItNumMax.Name = "TextBox_ItNumMax"
        Me.TextBox_ItNumMax.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_ItNumMax.TabIndex = 38
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(75, 207)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 41
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(156, 207)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 42
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.btnClose)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(308, 237)
        Me.GroupBox1.TabIndex = 43
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBox_TimeStepMin)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TextBox_TimeStepScaleFactorInitial)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.TextBox_TimeStepScaleFactor)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TextBox_FailureFrameLimit)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.CheckBox_LimitPropagation)
        Me.GroupBox2.Controls.Add(Me.TextBox_ItNumMax)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 9)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(294, 192)
        Me.GroupBox2.TabIndex = 44
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Limits"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 49
        Me.Label6.Text = "Output Time Steps"
        '
        'TextBox_TimeStepMin
        '
        Me.TextBox_TimeStepMin.Location = New System.Drawing.Point(231, 165)
        Me.TextBox_TimeStepMin.Name = "TextBox_TimeStepMin"
        Me.TextBox_TimeStepMin.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_TimeStepMin.TabIndex = 47
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 168)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Minimum Time Step Size"
        '
        'TextBox_TimeStepScaleFactorInitial
        '
        Me.TextBox_TimeStepScaleFactorInitial.Location = New System.Drawing.Point(231, 113)
        Me.TextBox_TimeStepScaleFactorInitial.Name = "TextBox_TimeStepScaleFactorInitial"
        Me.TextBox_TimeStepScaleFactorInitial.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_TimeStepScaleFactorInitial.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(116, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Initial Reduction Factor"
        '
        'TextBox_TimeStepScaleFactor
        '
        Me.TextBox_TimeStepScaleFactor.Location = New System.Drawing.Point(231, 139)
        Me.TextBox_TimeStepScaleFactor.Name = "TextBox_TimeStepScaleFactor"
        Me.TextBox_TimeStepScaleFactor.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_TimeStepScaleFactor.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Iterated Reduction Factor"
        '
        'TextBox_FailureFrameLimit
        '
        Me.TextBox_FailureFrameLimit.Location = New System.Drawing.Point(231, 66)
        Me.TextBox_FailureFrameLimit.Name = "TextBox_FailureFrameLimit"
        Me.TextBox_FailureFrameLimit.Size = New System.Drawing.Size(56, 20)
        Me.TextBox_FailureFrameLimit.TabIndex = 41
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Max # of Failed Frames per Failure Event"
        '
        'frmTrackProgCol
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(330, 256)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTrackProgCol"
        Me.ShowInTaskbar = False
        Me.Text = "Collapse Propagation Parameters"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox_LimitPropagation As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox_ItNumMax As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_TimeStepScaleFactor As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_FailureFrameLimit As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeStepMin As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeStepScaleFactorInitial As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
