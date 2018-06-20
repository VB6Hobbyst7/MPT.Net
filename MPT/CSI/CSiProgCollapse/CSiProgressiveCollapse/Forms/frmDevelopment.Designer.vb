<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevelopment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDevelopment))
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.TextBox_Directory = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_FileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btn_PopulateCriticalPathGroup = New System.Windows.Forms.Button()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btn_PopulateKeyElementGroup = New System.Windows.Forms.Button()
        Me.btnOpenModel = New System.Windows.Forms.Button()
        Me.btn_PopulateKeyElementFrame = New System.Windows.Forms.Button()
        Me.btnCloseModel = New System.Windows.Forms.Button()
        Me.btnRunAnalysis = New System.Windows.Forms.Button()
        Me.btnUnlock = New System.Windows.Forms.Button()
        Me.btnRunDesign = New System.Windows.Forms.Button()
        Me.GroupBox12.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.GroupBox3)
        Me.GroupBox12.Controls.Add(Me.btn_PopulateCriticalPathGroup)
        Me.GroupBox12.Controls.Add(Me.btnTest)
        Me.GroupBox12.Controls.Add(Me.btn_PopulateKeyElementGroup)
        Me.GroupBox12.Controls.Add(Me.btnOpenModel)
        Me.GroupBox12.Controls.Add(Me.btn_PopulateKeyElementFrame)
        Me.GroupBox12.Controls.Add(Me.btnCloseModel)
        Me.GroupBox12.Controls.Add(Me.btnRunAnalysis)
        Me.GroupBox12.Controls.Add(Me.btnUnlock)
        Me.GroupBox12.Controls.Add(Me.btnRunDesign)
        Me.GroupBox12.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(436, 136)
        Me.GroupBox12.TabIndex = 32
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Development (To Be Removed)"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnBrowse)
        Me.GroupBox3.Controls.Add(Me.TextBox_Directory)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TextBox_FileName)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(234, 80)
        Me.GroupBox3.TabIndex = 24
        Me.GroupBox3.TabStop = False
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(200, 16)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowse.TabIndex = 21
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'TextBox_Directory
        '
        Me.TextBox_Directory.Location = New System.Drawing.Point(63, 19)
        Me.TextBox_Directory.Name = "TextBox_Directory"
        Me.TextBox_Directory.Size = New System.Drawing.Size(131, 20)
        Me.TextBox_Directory.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Directory"
        '
        'TextBox_FileName
        '
        Me.TextBox_FileName.Location = New System.Drawing.Point(63, 45)
        Me.TextBox_FileName.Name = "TextBox_FileName"
        Me.TextBox_FileName.Size = New System.Drawing.Size(131, 20)
        Me.TextBox_FileName.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "File Name"
        '
        'btn_PopulateCriticalPathGroup
        '
        Me.btn_PopulateCriticalPathGroup.Location = New System.Drawing.Point(389, 101)
        Me.btn_PopulateCriticalPathGroup.Name = "btn_PopulateCriticalPathGroup"
        Me.btn_PopulateCriticalPathGroup.Size = New System.Drawing.Size(38, 23)
        Me.btn_PopulateCriticalPathGroup.TabIndex = 30
        Me.btn_PopulateCriticalPathGroup.TabStop = False
        Me.btn_PopulateCriticalPathGroup.Text = "<..."
        Me.btn_PopulateCriticalPathGroup.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(6, 107)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(102, 23)
        Me.btnTest.TabIndex = 12
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'btn_PopulateKeyElementGroup
        '
        Me.btn_PopulateKeyElementGroup.Location = New System.Drawing.Point(389, 72)
        Me.btn_PopulateKeyElementGroup.Name = "btn_PopulateKeyElementGroup"
        Me.btn_PopulateKeyElementGroup.Size = New System.Drawing.Size(38, 23)
        Me.btn_PopulateKeyElementGroup.TabIndex = 29
        Me.btn_PopulateKeyElementGroup.TabStop = False
        Me.btn_PopulateKeyElementGroup.Text = "<..."
        Me.btn_PopulateKeyElementGroup.UseVisualStyleBackColor = True
        '
        'btnOpenModel
        '
        Me.btnOpenModel.Location = New System.Drawing.Point(246, 19)
        Me.btnOpenModel.Name = "btnOpenModel"
        Me.btnOpenModel.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenModel.TabIndex = 0
        Me.btnOpenModel.Text = "Open Model"
        Me.btnOpenModel.UseVisualStyleBackColor = True
        '
        'btn_PopulateKeyElementFrame
        '
        Me.btn_PopulateKeyElementFrame.Location = New System.Drawing.Point(389, 43)
        Me.btn_PopulateKeyElementFrame.Name = "btn_PopulateKeyElementFrame"
        Me.btn_PopulateKeyElementFrame.Size = New System.Drawing.Size(38, 23)
        Me.btn_PopulateKeyElementFrame.TabIndex = 28
        Me.btn_PopulateKeyElementFrame.TabStop = False
        Me.btn_PopulateKeyElementFrame.Text = "<..."
        Me.btn_PopulateKeyElementFrame.UseVisualStyleBackColor = True
        '
        'btnCloseModel
        '
        Me.btnCloseModel.Location = New System.Drawing.Point(352, 19)
        Me.btnCloseModel.Name = "btnCloseModel"
        Me.btnCloseModel.Size = New System.Drawing.Size(75, 23)
        Me.btnCloseModel.TabIndex = 1
        Me.btnCloseModel.Text = "Close Model"
        Me.btnCloseModel.UseVisualStyleBackColor = True
        '
        'btnRunAnalysis
        '
        Me.btnRunAnalysis.Location = New System.Drawing.Point(246, 43)
        Me.btnRunAnalysis.Name = "btnRunAnalysis"
        Me.btnRunAnalysis.Size = New System.Drawing.Size(102, 23)
        Me.btnRunAnalysis.TabIndex = 8
        Me.btnRunAnalysis.Text = "Run Analysis"
        Me.btnRunAnalysis.UseVisualStyleBackColor = True
        '
        'btnUnlock
        '
        Me.btnUnlock.Location = New System.Drawing.Point(246, 101)
        Me.btnUnlock.Name = "btnUnlock"
        Me.btnUnlock.Size = New System.Drawing.Size(102, 23)
        Me.btnUnlock.TabIndex = 10
        Me.btnUnlock.Text = "Unlock Model"
        Me.btnUnlock.UseVisualStyleBackColor = True
        '
        'btnRunDesign
        '
        Me.btnRunDesign.Location = New System.Drawing.Point(246, 72)
        Me.btnRunDesign.Name = "btnRunDesign"
        Me.btnRunDesign.Size = New System.Drawing.Size(102, 23)
        Me.btnRunDesign.TabIndex = 9
        Me.btnRunDesign.Text = "Run Design"
        Me.btnRunDesign.UseVisualStyleBackColor = True
        '
        'frmDevelopment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(457, 159)
        Me.Controls.Add(Me.GroupBox12)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDevelopment"
        Me.ShowInTaskbar = False
        Me.Text = "Development"
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents TextBox_Directory As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_FileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btn_PopulateCriticalPathGroup As System.Windows.Forms.Button
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btn_PopulateKeyElementGroup As System.Windows.Forms.Button
    Friend WithEvents btnOpenModel As System.Windows.Forms.Button
    Friend WithEvents btn_PopulateKeyElementFrame As System.Windows.Forms.Button
    Friend WithEvents btnCloseModel As System.Windows.Forms.Button
    Friend WithEvents btnRunAnalysis As System.Windows.Forms.Button
    Friend WithEvents btnUnlock As System.Windows.Forms.Button
    Friend WithEvents btnRunDesign As System.Windows.Forms.Button
End Class
