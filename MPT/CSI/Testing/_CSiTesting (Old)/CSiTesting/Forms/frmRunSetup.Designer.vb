<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRunSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRunSetup))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAdvanced = New System.Windows.Forms.Button()
        Me.Frame_TestingType = New System.Windows.Forms.GroupBox()
        Me.CheckBox_DistTestJoin = New System.Windows.Forms.CheckBox()
        Me.CheckBox_DistTestStart = New System.Windows.Forms.CheckBox()
        Me.Option_Distributed = New System.Windows.Forms.RadioButton()
        Me.Option_Local = New System.Windows.Forms.RadioButton()
        Me.FrameLocation = New System.Windows.Forms.GroupBox()
        Me.FramePath = New System.Windows.Forms.GroupBox()
        Me.Option_LocationAbsolute = New System.Windows.Forms.RadioButton()
        Me.Option_LocationRelative = New System.Windows.Forms.RadioButton()
        Me.TextBox_Location = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ComboBox_FileType = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox_Build = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_Version = New System.Windows.Forms.TextBox()
        Me.Label_Program = New System.Windows.Forms.LinkLabel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.Frame_TestingType.SuspendLayout()
        Me.FrameLocation.SuspendLayout()
        Me.FramePath.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnAdvanced)
        Me.GroupBox1.Controls.Add(Me.Frame_TestingType)
        Me.GroupBox1.Controls.Add(Me.FrameLocation)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(469, 198)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnAdvanced
        '
        Me.btnAdvanced.Location = New System.Drawing.Point(6, 169)
        Me.btnAdvanced.Name = "btnAdvanced"
        Me.btnAdvanced.Size = New System.Drawing.Size(117, 23)
        Me.btnAdvanced.TabIndex = 6
        Me.btnAdvanced.Text = "Advanced Settings"
        Me.btnAdvanced.UseVisualStyleBackColor = True
        '
        'Frame_TestingType
        '
        Me.Frame_TestingType.Controls.Add(Me.CheckBox_DistTestJoin)
        Me.Frame_TestingType.Controls.Add(Me.CheckBox_DistTestStart)
        Me.Frame_TestingType.Controls.Add(Me.Option_Distributed)
        Me.Frame_TestingType.Controls.Add(Me.Option_Local)
        Me.Frame_TestingType.Location = New System.Drawing.Point(320, 83)
        Me.Frame_TestingType.Name = "Frame_TestingType"
        Me.Frame_TestingType.Size = New System.Drawing.Size(138, 106)
        Me.Frame_TestingType.TabIndex = 3
        Me.Frame_TestingType.TabStop = False
        Me.Frame_TestingType.Text = "Testing Type"
        '
        'CheckBox_DistTestJoin
        '
        Me.CheckBox_DistTestJoin.AutoSize = True
        Me.CheckBox_DistTestJoin.Location = New System.Drawing.Point(28, 86)
        Me.CheckBox_DistTestJoin.Name = "CheckBox_DistTestJoin"
        Me.CheckBox_DistTestJoin.Size = New System.Drawing.Size(69, 17)
        Me.CheckBox_DistTestJoin.TabIndex = 4
        Me.CheckBox_DistTestJoin.Text = "Join Test"
        Me.CheckBox_DistTestJoin.UseVisualStyleBackColor = True
        '
        'CheckBox_DistTestStart
        '
        Me.CheckBox_DistTestStart.AutoSize = True
        Me.CheckBox_DistTestStart.Location = New System.Drawing.Point(28, 65)
        Me.CheckBox_DistTestStart.Name = "CheckBox_DistTestStart"
        Me.CheckBox_DistTestStart.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox_DistTestStart.TabIndex = 3
        Me.CheckBox_DistTestStart.Text = "Start Test"
        Me.CheckBox_DistTestStart.UseVisualStyleBackColor = True
        '
        'Option_Distributed
        '
        Me.Option_Distributed.AutoSize = True
        Me.Option_Distributed.Location = New System.Drawing.Point(6, 42)
        Me.Option_Distributed.Name = "Option_Distributed"
        Me.Option_Distributed.Size = New System.Drawing.Size(113, 17)
        Me.Option_Distributed.TabIndex = 2
        Me.Option_Distributed.TabStop = True
        Me.Option_Distributed.Text = "Distributed Testing"
        Me.Option_Distributed.UseVisualStyleBackColor = True
        '
        'Option_Local
        '
        Me.Option_Local.AutoSize = True
        Me.Option_Local.Location = New System.Drawing.Point(6, 19)
        Me.Option_Local.Name = "Option_Local"
        Me.Option_Local.Size = New System.Drawing.Size(89, 17)
        Me.Option_Local.TabIndex = 1
        Me.Option_Local.TabStop = True
        Me.Option_Local.Text = "Local Testing"
        Me.Option_Local.UseVisualStyleBackColor = True
        '
        'FrameLocation
        '
        Me.FrameLocation.Controls.Add(Me.FramePath)
        Me.FrameLocation.Controls.Add(Me.TextBox_Location)
        Me.FrameLocation.Controls.Add(Me.btnBrowse)
        Me.FrameLocation.Location = New System.Drawing.Point(6, 83)
        Me.FrameLocation.Name = "FrameLocation"
        Me.FrameLocation.Size = New System.Drawing.Size(308, 79)
        Me.FrameLocation.TabIndex = 2
        Me.FrameLocation.TabStop = False
        Me.FrameLocation.Text = "Location"
        '
        'FramePath
        '
        Me.FramePath.Controls.Add(Me.Option_LocationAbsolute)
        Me.FramePath.Controls.Add(Me.Option_LocationRelative)
        Me.FramePath.Location = New System.Drawing.Point(129, 9)
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
        Me.TextBox_Location.Location = New System.Drawing.Point(6, 52)
        Me.TextBox_Location.Name = "TextBox_Location"
        Me.TextBox_Location.Size = New System.Drawing.Size(296, 20)
        Me.TextBox_Location.TabIndex = 10
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(6, 19)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(117, 23)
        Me.btnBrowse.TabIndex = 7
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ComboBox_FileType)
        Me.GroupBox3.Location = New System.Drawing.Point(320, 10)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(138, 67)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "File Type"
        '
        'ComboBox_FileType
        '
        Me.ComboBox_FileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_FileType.FormattingEnabled = True
        Me.ComboBox_FileType.Location = New System.Drawing.Point(7, 26)
        Me.ComboBox_FileType.Name = "ComboBox_FileType"
        Me.ComboBox_FileType.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_FileType.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TextBox_Build)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TextBox_Version)
        Me.GroupBox2.Controls.Add(Me.Label_Program)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(308, 67)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Program"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(204, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Build"
        '
        'TextBox_Build
        '
        Me.TextBox_Build.Location = New System.Drawing.Point(207, 31)
        Me.TextBox_Build.Name = "TextBox_Build"
        Me.TextBox_Build.Size = New System.Drawing.Size(94, 20)
        Me.TextBox_Build.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(107, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Version"
        '
        'TextBox_Version
        '
        Me.TextBox_Version.Location = New System.Drawing.Point(110, 31)
        Me.TextBox_Version.Name = "TextBox_Version"
        Me.TextBox_Version.Size = New System.Drawing.Size(94, 20)
        Me.TextBox_Version.TabIndex = 11
        '
        'Label_Program
        '
        Me.Label_Program.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_Program.AutoSize = True
        Me.Label_Program.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Program.Location = New System.Drawing.Point(8, 31)
        Me.Label_Program.Name = "Label_Program"
        Me.Label_Program.Size = New System.Drawing.Size(96, 20)
        Me.Label_Program.TabIndex = 9
        Me.Label_Program.TabStop = True
        Me.Label_Program.Text = "LinkLabel1"
        Me.Label_Program.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(147, 211)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(228, 211)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.GroupBox1)
        Me.GroupBox7.Controls.Add(Me.btnCancel)
        Me.GroupBox7.Controls.Add(Me.btnOK)
        Me.GroupBox7.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(494, 241)
        Me.GroupBox7.TabIndex = 7
        Me.GroupBox7.TabStop = False
        '
        'frmRunSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 253)
        Me.Controls.Add(Me.GroupBox7)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRunSetup"
        Me.Text = "Run Setup"
        Me.GroupBox1.ResumeLayout(False)
        Me.Frame_TestingType.ResumeLayout(False)
        Me.Frame_TestingType.PerformLayout()
        Me.FrameLocation.ResumeLayout(False)
        Me.FrameLocation.PerformLayout()
        Me.FramePath.ResumeLayout(False)
        Me.FramePath.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdvanced As System.Windows.Forms.Button
    Friend WithEvents Frame_TestingType As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_DistTestJoin As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_DistTestStart As System.Windows.Forms.CheckBox
    Friend WithEvents Option_Distributed As System.Windows.Forms.RadioButton
    Friend WithEvents Option_Local As System.Windows.Forms.RadioButton
    Friend WithEvents FrameLocation As System.Windows.Forms.GroupBox
    Friend WithEvents FramePath As System.Windows.Forms.GroupBox
    Friend WithEvents Option_LocationAbsolute As System.Windows.Forms.RadioButton
    Friend WithEvents Option_LocationRelative As System.Windows.Forms.RadioButton
    Friend WithEvents TextBox_Location As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox_FileType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Build As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Version As System.Windows.Forms.TextBox
    Friend WithEvents Label_Program As System.Windows.Forms.LinkLabel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
End Class
