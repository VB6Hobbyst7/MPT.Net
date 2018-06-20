<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestSuiteSetupAdvanced
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTestSuiteSetupAdvanced))
        Me.ComboBox_TestToRun = New System.Windows.Forms.ComboBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.CheckBox_ReUseXMLList = New System.Windows.Forms.CheckBox()
        Me.FrameXML = New System.Windows.Forms.GroupBox()
        Me.CheckBox_ListXML = New System.Windows.Forms.CheckBox()
        Me.FrameDryRun = New System.Windows.Forms.GroupBox()
        Me.CheckBox_EnableDryRun = New System.Windows.Forms.CheckBox()
        Me.CheckBox_CopyModelsFlat = New System.Windows.Forms.CheckBox()
        Me.FrameTestToRun = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.FrameXML.SuspendLayout()
        Me.FrameDryRun.SuspendLayout()
        Me.FrameTestToRun.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox_TestToRun
        '
        Me.ComboBox_TestToRun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_TestToRun.FormattingEnabled = True
        Me.ComboBox_TestToRun.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox_TestToRun.Name = "ComboBox_TestToRun"
        Me.ComboBox_TestToRun.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox_TestToRun.TabIndex = 6
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(183, 147)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(102, 147)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'CheckBox_ReUseXMLList
        '
        Me.CheckBox_ReUseXMLList.AutoSize = True
        Me.CheckBox_ReUseXMLList.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_ReUseXMLList.Name = "CheckBox_ReUseXMLList"
        Me.CheckBox_ReUseXMLList.Size = New System.Drawing.Size(213, 17)
        Me.CheckBox_ReUseXMLList.TabIndex = 7
        Me.CheckBox_ReUseXMLList.Text = "Re-use XML File List (Copy Models Flat)"
        Me.CheckBox_ReUseXMLList.UseVisualStyleBackColor = True
        '
        'FrameXML
        '
        Me.FrameXML.Controls.Add(Me.CheckBox_ListXML)
        Me.FrameXML.Controls.Add(Me.CheckBox_ReUseXMLList)
        Me.FrameXML.Location = New System.Drawing.Point(6, 19)
        Me.FrameXML.Name = "FrameXML"
        Me.FrameXML.Size = New System.Drawing.Size(224, 66)
        Me.FrameXML.TabIndex = 10
        Me.FrameXML.TabStop = False
        Me.FrameXML.Text = "XML Operations"
        '
        'CheckBox_ListXML
        '
        Me.CheckBox_ListXML.AutoSize = True
        Me.CheckBox_ListXML.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_ListXML.Name = "CheckBox_ListXML"
        Me.CheckBox_ListXML.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox_ListXML.TabIndex = 8
        Me.CheckBox_ListXML.Text = "List XMLs"
        Me.CheckBox_ListXML.UseVisualStyleBackColor = True
        '
        'FrameDryRun
        '
        Me.FrameDryRun.Controls.Add(Me.CheckBox_EnableDryRun)
        Me.FrameDryRun.Controls.Add(Me.CheckBox_CopyModelsFlat)
        Me.FrameDryRun.Location = New System.Drawing.Point(236, 19)
        Me.FrameDryRun.Name = "FrameDryRun"
        Me.FrameDryRun.Size = New System.Drawing.Size(133, 66)
        Me.FrameDryRun.TabIndex = 11
        Me.FrameDryRun.TabStop = False
        Me.FrameDryRun.Text = "Dry Run Options"
        '
        'CheckBox_EnableDryRun
        '
        Me.CheckBox_EnableDryRun.AutoSize = True
        Me.CheckBox_EnableDryRun.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_EnableDryRun.Name = "CheckBox_EnableDryRun"
        Me.CheckBox_EnableDryRun.Size = New System.Drawing.Size(125, 17)
        Me.CheckBox_EnableDryRun.TabIndex = 8
        Me.CheckBox_EnableDryRun.Text = "Enable Dry Run Test"
        Me.CheckBox_EnableDryRun.UseVisualStyleBackColor = True
        '
        'CheckBox_CopyModelsFlat
        '
        Me.CheckBox_CopyModelsFlat.AutoSize = True
        Me.CheckBox_CopyModelsFlat.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_CopyModelsFlat.Name = "CheckBox_CopyModelsFlat"
        Me.CheckBox_CopyModelsFlat.Size = New System.Drawing.Size(107, 17)
        Me.CheckBox_CopyModelsFlat.TabIndex = 7
        Me.CheckBox_CopyModelsFlat.Text = "Copy Models Flat"
        Me.CheckBox_CopyModelsFlat.UseVisualStyleBackColor = True
        '
        'FrameTestToRun
        '
        Me.FrameTestToRun.Controls.Add(Me.ComboBox_TestToRun)
        Me.FrameTestToRun.Location = New System.Drawing.Point(110, 91)
        Me.FrameTestToRun.Name = "FrameTestToRun"
        Me.FrameTestToRun.Size = New System.Drawing.Size(138, 51)
        Me.FrameTestToRun.TabIndex = 12
        Me.FrameTestToRun.TabStop = False
        Me.FrameTestToRun.Text = "Test to Run"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FrameXML)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.FrameTestToRun)
        Me.GroupBox1.Controls.Add(Me.FrameDryRun)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(379, 181)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'frmTestSuiteSetupAdvanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(392, 191)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTestSuiteSetupAdvanced"
        Me.Text = "Test Suite Setup - Advanced Settings"
        Me.FrameXML.ResumeLayout(False)
        Me.FrameXML.PerformLayout()
        Me.FrameDryRun.ResumeLayout(False)
        Me.FrameDryRun.PerformLayout()
        Me.FrameTestToRun.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox_TestToRun As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents CheckBox_ReUseXMLList As System.Windows.Forms.CheckBox
    Friend WithEvents FrameXML As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_ListXML As System.Windows.Forms.CheckBox
    Friend WithEvents FrameDryRun As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_EnableDryRun As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_CopyModelsFlat As System.Windows.Forms.CheckBox
    Friend WithEvents FrameTestToRun As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
