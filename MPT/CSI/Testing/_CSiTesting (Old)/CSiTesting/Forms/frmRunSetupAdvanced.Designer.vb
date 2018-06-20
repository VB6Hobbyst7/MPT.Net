<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRunSetupAdvanced
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRunSetupAdvanced))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Frame_TestingType = New System.Windows.Forms.GroupBox()
        Me.btnEmailList = New System.Windows.Forms.Button()
        Me.CheckBox_Email = New System.Windows.Forms.CheckBox()
        Me.CheckBox_LogFiles = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnReportFormatStyles = New System.Windows.Forms.Button()
        Me.Frame_PercDiff = New System.Windows.Forms.GroupBox()
        Me.Label_DecimalDigits = New System.Windows.Forms.Label()
        Me.TextBox_DecimalDigits = New System.Windows.Forms.TextBox()
        Me.FrameLocation = New System.Windows.Forms.GroupBox()
        Me.FramePath = New System.Windows.Forms.GroupBox()
        Me.Option_LocationAbsolute = New System.Windows.Forms.RadioButton()
        Me.Option_LocationRelative = New System.Windows.Forms.RadioButton()
        Me.TextBox_Location = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Frame_RuntimeLimitsOverwrites = New System.Windows.Forms.GroupBox()
        Me.Label_MaxRuntime = New System.Windows.Forms.Label()
        Me.TextBox_MaxRuntime = New System.Windows.Forms.TextBox()
        Me.Label_TimeMultiplier = New System.Windows.Forms.Label()
        Me.TextBox_TimeMultiplier = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.Frame_TestingType.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Frame_PercDiff.SuspendLayout()
        Me.FrameLocation.SuspendLayout()
        Me.FramePath.SuspendLayout()
        Me.Frame_RuntimeLimitsOverwrites.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Frame_TestingType)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.Frame_RuntimeLimitsOverwrites)
        Me.GroupBox1.Location = New System.Drawing.Point(4, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(334, 327)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Frame_TestingType
        '
        Me.Frame_TestingType.Controls.Add(Me.btnEmailList)
        Me.Frame_TestingType.Controls.Add(Me.CheckBox_Email)
        Me.Frame_TestingType.Controls.Add(Me.CheckBox_LogFiles)
        Me.Frame_TestingType.Location = New System.Drawing.Point(167, 12)
        Me.Frame_TestingType.Name = "Frame_TestingType"
        Me.Frame_TestingType.Size = New System.Drawing.Size(160, 101)
        Me.Frame_TestingType.TabIndex = 3
        Me.Frame_TestingType.TabStop = False
        Me.Frame_TestingType.Text = "Reporting"
        '
        'btnEmailList
        '
        Me.btnEmailList.Location = New System.Drawing.Point(15, 63)
        Me.btnEmailList.Name = "btnEmailList"
        Me.btnEmailList.Size = New System.Drawing.Size(121, 23)
        Me.btnEmailList.TabIndex = 9
        Me.btnEmailList.Text = "E-mail List"
        Me.btnEmailList.UseVisualStyleBackColor = True
        '
        'CheckBox_Email
        '
        Me.CheckBox_Email.AutoSize = True
        Me.CheckBox_Email.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_Email.Name = "CheckBox_Email"
        Me.CheckBox_Email.Size = New System.Drawing.Size(143, 17)
        Me.CheckBox_Email.TabIndex = 4
        Me.CheckBox_Email.Text = "Send E-mail Notifications"
        Me.CheckBox_Email.UseVisualStyleBackColor = True
        '
        'CheckBox_LogFiles
        '
        Me.CheckBox_LogFiles.AutoSize = True
        Me.CheckBox_LogFiles.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_LogFiles.Name = "CheckBox_LogFiles"
        Me.CheckBox_LogFiles.Size = New System.Drawing.Size(96, 17)
        Me.CheckBox_LogFiles.TabIndex = 3
        Me.CheckBox_LogFiles.Text = "Write Log Files"
        Me.CheckBox_LogFiles.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(167, 295)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnReportFormatStyles)
        Me.GroupBox3.Controls.Add(Me.Frame_PercDiff)
        Me.GroupBox3.Controls.Add(Me.FrameLocation)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 115)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(321, 174)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Comparison Parameters"
        '
        'btnReportFormatStyles
        '
        Me.btnReportFormatStyles.Location = New System.Drawing.Point(105, 35)
        Me.btnReportFormatStyles.Name = "btnReportFormatStyles"
        Me.btnReportFormatStyles.Size = New System.Drawing.Size(203, 23)
        Me.btnReportFormatStyles.TabIndex = 8
        Me.btnReportFormatStyles.Text = "(TBD) HTML Report Formatting Styles"
        Me.btnReportFormatStyles.UseVisualStyleBackColor = True
        '
        'Frame_PercDiff
        '
        Me.Frame_PercDiff.Controls.Add(Me.Label_DecimalDigits)
        Me.Frame_PercDiff.Controls.Add(Me.TextBox_DecimalDigits)
        Me.Frame_PercDiff.Location = New System.Drawing.Point(6, 19)
        Me.Frame_PercDiff.Name = "Frame_PercDiff"
        Me.Frame_PercDiff.Size = New System.Drawing.Size(93, 61)
        Me.Frame_PercDiff.TabIndex = 3
        Me.Frame_PercDiff.TabStop = False
        Me.Frame_PercDiff.Text = "% Difference"
        '
        'Label_DecimalDigits
        '
        Me.Label_DecimalDigits.AutoSize = True
        Me.Label_DecimalDigits.Location = New System.Drawing.Point(5, 16)
        Me.Label_DecimalDigits.Name = "Label_DecimalDigits"
        Me.Label_DecimalDigits.Size = New System.Drawing.Size(74, 13)
        Me.Label_DecimalDigits.TabIndex = 14
        Me.Label_DecimalDigits.Text = "Decimal Digits"
        '
        'TextBox_DecimalDigits
        '
        Me.TextBox_DecimalDigits.Location = New System.Drawing.Point(8, 32)
        Me.TextBox_DecimalDigits.Name = "TextBox_DecimalDigits"
        Me.TextBox_DecimalDigits.Size = New System.Drawing.Size(71, 20)
        Me.TextBox_DecimalDigits.TabIndex = 13
        '
        'FrameLocation
        '
        Me.FrameLocation.Controls.Add(Me.FramePath)
        Me.FrameLocation.Controls.Add(Me.TextBox_Location)
        Me.FrameLocation.Controls.Add(Me.btnBrowse)
        Me.FrameLocation.Location = New System.Drawing.Point(6, 86)
        Me.FrameLocation.Name = "FrameLocation"
        Me.FrameLocation.Size = New System.Drawing.Size(308, 82)
        Me.FrameLocation.TabIndex = 2
        Me.FrameLocation.TabStop = False
        Me.FrameLocation.Text = "Previous Test Results Location"
        '
        'FramePath
        '
        Me.FramePath.Controls.Add(Me.Option_LocationAbsolute)
        Me.FramePath.Controls.Add(Me.Option_LocationRelative)
        Me.FramePath.Location = New System.Drawing.Point(129, 12)
        Me.FramePath.Name = "FramePath"
        Me.FramePath.Size = New System.Drawing.Size(155, 39)
        Me.FramePath.TabIndex = 11
        Me.FramePath.TabStop = False
        Me.FramePath.Text = "Path"
        '
        'Option_LocationAbsolute
        '
        Me.Option_LocationAbsolute.AutoSize = True
        Me.Option_LocationAbsolute.Location = New System.Drawing.Point(73, 14)
        Me.Option_LocationAbsolute.Name = "Option_LocationAbsolute"
        Me.Option_LocationAbsolute.Size = New System.Drawing.Size(66, 17)
        Me.Option_LocationAbsolute.TabIndex = 1
        Me.Option_LocationAbsolute.TabStop = True
        Me.Option_LocationAbsolute.Text = "Absolute"
        Me.Option_LocationAbsolute.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Option_LocationAbsolute.UseVisualStyleBackColor = True
        '
        'Option_LocationRelative
        '
        Me.Option_LocationRelative.AutoSize = True
        Me.Option_LocationRelative.Location = New System.Drawing.Point(6, 13)
        Me.Option_LocationRelative.Name = "Option_LocationRelative"
        Me.Option_LocationRelative.Size = New System.Drawing.Size(64, 17)
        Me.Option_LocationRelative.TabIndex = 0
        Me.Option_LocationRelative.TabStop = True
        Me.Option_LocationRelative.Text = "Relative"
        Me.Option_LocationRelative.UseVisualStyleBackColor = True
        '
        'TextBox_Location
        '
        Me.TextBox_Location.Location = New System.Drawing.Point(6, 55)
        Me.TextBox_Location.Name = "TextBox_Location"
        Me.TextBox_Location.Size = New System.Drawing.Size(296, 20)
        Me.TextBox_Location.TabIndex = 10
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(6, 22)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(117, 23)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(86, 295)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Frame_RuntimeLimitsOverwrites
        '
        Me.Frame_RuntimeLimitsOverwrites.Controls.Add(Me.Label_MaxRuntime)
        Me.Frame_RuntimeLimitsOverwrites.Controls.Add(Me.TextBox_MaxRuntime)
        Me.Frame_RuntimeLimitsOverwrites.Controls.Add(Me.Label_TimeMultiplier)
        Me.Frame_RuntimeLimitsOverwrites.Controls.Add(Me.TextBox_TimeMultiplier)
        Me.Frame_RuntimeLimitsOverwrites.Location = New System.Drawing.Point(6, 12)
        Me.Frame_RuntimeLimitsOverwrites.Name = "Frame_RuntimeLimitsOverwrites"
        Me.Frame_RuntimeLimitsOverwrites.Size = New System.Drawing.Size(155, 101)
        Me.Frame_RuntimeLimitsOverwrites.TabIndex = 1
        Me.Frame_RuntimeLimitsOverwrites.TabStop = False
        Me.Frame_RuntimeLimitsOverwrites.Text = "Runtime Limits Overwrites"
        '
        'Label_MaxRuntime
        '
        Me.Label_MaxRuntime.AutoSize = True
        Me.Label_MaxRuntime.Location = New System.Drawing.Point(6, 55)
        Me.Label_MaxRuntime.Name = "Label_MaxRuntime"
        Me.Label_MaxRuntime.Size = New System.Drawing.Size(134, 13)
        Me.Label_MaxRuntime.TabIndex = 14
        Me.Label_MaxRuntime.Text = "Max Allowed Runtime (min)"
        '
        'TextBox_MaxRuntime
        '
        Me.TextBox_MaxRuntime.Location = New System.Drawing.Point(9, 71)
        Me.TextBox_MaxRuntime.Name = "TextBox_MaxRuntime"
        Me.TextBox_MaxRuntime.Size = New System.Drawing.Size(94, 20)
        Me.TextBox_MaxRuntime.TabIndex = 13
        '
        'Label_TimeMultiplier
        '
        Me.Label_TimeMultiplier.AutoSize = True
        Me.Label_TimeMultiplier.Location = New System.Drawing.Point(6, 16)
        Me.Label_TimeMultiplier.Name = "Label_TimeMultiplier"
        Me.Label_TimeMultiplier.Size = New System.Drawing.Size(48, 13)
        Me.Label_TimeMultiplier.TabIndex = 12
        Me.Label_TimeMultiplier.Text = "Multiplier"
        '
        'TextBox_TimeMultiplier
        '
        Me.TextBox_TimeMultiplier.Location = New System.Drawing.Point(9, 32)
        Me.TextBox_TimeMultiplier.Name = "TextBox_TimeMultiplier"
        Me.TextBox_TimeMultiplier.Size = New System.Drawing.Size(94, 20)
        Me.TextBox_TimeMultiplier.TabIndex = 11
        '
        'frmRunSetupAdvanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 333)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRunSetupAdvanced"
        Me.Text = "Run Setup - Advanced Settings"
        Me.GroupBox1.ResumeLayout(False)
        Me.Frame_TestingType.ResumeLayout(False)
        Me.Frame_TestingType.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Frame_PercDiff.ResumeLayout(False)
        Me.Frame_PercDiff.PerformLayout()
        Me.FrameLocation.ResumeLayout(False)
        Me.FrameLocation.PerformLayout()
        Me.FramePath.ResumeLayout(False)
        Me.FramePath.PerformLayout()
        Me.Frame_RuntimeLimitsOverwrites.ResumeLayout(False)
        Me.Frame_RuntimeLimitsOverwrites.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Frame_TestingType As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_Email As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_LogFiles As System.Windows.Forms.CheckBox
    Friend WithEvents FrameLocation As System.Windows.Forms.GroupBox
    Friend WithEvents FramePath As System.Windows.Forms.GroupBox
    Friend WithEvents Option_LocationAbsolute As System.Windows.Forms.RadioButton
    Friend WithEvents Option_LocationRelative As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_Location As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Frame_RuntimeLimitsOverwrites As System.Windows.Forms.GroupBox
    Friend WithEvents Label_MaxRuntime As System.Windows.Forms.Label
    Friend WithEvents TextBox_MaxRuntime As System.Windows.Forms.TextBox
    Friend WithEvents Label_TimeMultiplier As System.Windows.Forms.Label
    Friend WithEvents TextBox_TimeMultiplier As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnEmailList As System.Windows.Forms.Button
    Friend WithEvents btnReportFormatStyles As System.Windows.Forms.Button
    Friend WithEvents Frame_PercDiff As System.Windows.Forms.GroupBox
    Friend WithEvents Label_DecimalDigits As System.Windows.Forms.Label
    Friend WithEvents TextBox_DecimalDigits As System.Windows.Forms.TextBox
End Class
