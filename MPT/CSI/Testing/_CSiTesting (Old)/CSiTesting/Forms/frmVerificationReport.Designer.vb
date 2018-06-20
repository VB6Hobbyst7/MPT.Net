<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerificationReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerificationReport))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnCompleteReport = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_ZipIndividual = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ZipAll = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_DeleteFiles = New System.Windows.Forms.CheckBox()
        Me.CheckBox_PrepareReport = New System.Windows.Forms.CheckBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 282)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnCompleteReport)
        Me.GroupBox4.Controls.Add(Me.ProgressBar1)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 162)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(312, 81)
        Me.GroupBox4.TabIndex = 24
        Me.GroupBox4.TabStop = False
        '
        'btnCompleteReport
        '
        Me.btnCompleteReport.Location = New System.Drawing.Point(93, 13)
        Me.btnCompleteReport.Name = "btnCompleteReport"
        Me.btnCompleteReport.Size = New System.Drawing.Size(120, 23)
        Me.btnCompleteReport.TabIndex = 23
        Me.btnCompleteReport.Text = "Complete Report"
        Me.btnCompleteReport.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(54, 43)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(197, 23)
        Me.ProgressBar1.TabIndex = 22
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckBox_ZipIndividual)
        Me.GroupBox3.Controls.Add(Me.CheckBox_ZipAll)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 92)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(312, 68)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Zip Copied Non-Matching Examples (Optional)"
        '
        'CheckBox_ZipIndividual
        '
        Me.CheckBox_ZipIndividual.AutoSize = True
        Me.CheckBox_ZipIndividual.Location = New System.Drawing.Point(6, 42)
        Me.CheckBox_ZipIndividual.Name = "CheckBox_ZipIndividual"
        Me.CheckBox_ZipIndividual.Size = New System.Drawing.Size(303, 17)
        Me.CheckBox_ZipIndividual.TabIndex = 19
        Me.CheckBox_ZipIndividual.Text = "Zip Individual Examples (Each Example Zipped Separately)"
        Me.CheckBox_ZipIndividual.UseVisualStyleBackColor = True
        '
        'CheckBox_ZipAll
        '
        Me.CheckBox_ZipAll.AutoSize = True
        Me.CheckBox_ZipAll.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_ZipAll.Name = "CheckBox_ZipAll"
        Me.CheckBox_ZipAll.Size = New System.Drawing.Size(204, 17)
        Me.CheckBox_ZipAll.TabIndex = 18
        Me.CheckBox_ZipAll.Text = "Zip Report (All Models in One Zip File)"
        Me.CheckBox_ZipAll.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CheckBox_DeleteFiles)
        Me.GroupBox2.Controls.Add(Me.CheckBox_PrepareReport)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(312, 67)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Copy Non-Matching Examples (Optional)"
        '
        'CheckBox_DeleteFiles
        '
        Me.CheckBox_DeleteFiles.AutoSize = True
        Me.CheckBox_DeleteFiles.Location = New System.Drawing.Point(18, 42)
        Me.CheckBox_DeleteFiles.Name = "CheckBox_DeleteFiles"
        Me.CheckBox_DeleteFiles.Size = New System.Drawing.Size(290, 17)
        Me.CheckBox_DeleteFiles.TabIndex = 19
        Me.CheckBox_DeleteFiles.Text = "Delete Analysis Files (leaves Log, text import && mdb files)"
        Me.CheckBox_DeleteFiles.UseVisualStyleBackColor = True
        '
        'CheckBox_PrepareReport
        '
        Me.CheckBox_PrepareReport.AutoSize = True
        Me.CheckBox_PrepareReport.Location = New System.Drawing.Point(6, 19)
        Me.CheckBox_PrepareReport.Name = "CheckBox_PrepareReport"
        Me.CheckBox_PrepareReport.Size = New System.Drawing.Size(235, 17)
        Me.CheckBox_PrepareReport.TabIndex = 18
        Me.CheckBox_PrepareReport.Text = "Prepare Report Models (Non-Matching Only)"
        Me.CheckBox_PrepareReport.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(166, 247)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(85, 247)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 11
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmVerificationReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 292)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVerificationReport"
        Me.Text = "Prepare Verification Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCompleteReport As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_ZipIndividual As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_ZipAll As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_DeleteFiles As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_PrepareReport As System.Windows.Forms.CheckBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class
