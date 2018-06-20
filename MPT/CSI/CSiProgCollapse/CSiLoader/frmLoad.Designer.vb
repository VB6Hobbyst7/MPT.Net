<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoad
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoad))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.TextBox_Directory = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_FileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOpenModel = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnBrowseProgram = New System.Windows.Forms.Button()
        Me.TxtBxPathProgram = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox3.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnBrowse)
        Me.GroupBox3.Controls.Add(Me.TextBox_Directory)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TextBox_FileName)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 65)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(249, 80)
        Me.GroupBox3.TabIndex = 26
        Me.GroupBox3.TabStop = false
        Me.GroupBox3.Text = "Model"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(200, 16)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowse.TabIndex = 21
        Me.btnBrowse.Text = "..."
        Me.btnBrowse.UseVisualStyleBackColor = true
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
        Me.Label1.AutoSize = true
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
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(7, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "File Name"
        '
        'btnOpenModel
        '
        Me.btnOpenModel.Location = New System.Drawing.Point(24, 151)
        Me.btnOpenModel.Name = "btnOpenModel"
        Me.btnOpenModel.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenModel.TabIndex = 25
        Me.btnOpenModel.Text = "Start"
        Me.btnOpenModel.UseVisualStyleBackColor = true
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(156, 151)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(78, 23)
        Me.btnClose.TabIndex = 27
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = true
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBrowseProgram)
        Me.GroupBox1.Controls.Add(Me.TxtBxPathProgram)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 54)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Program"
        '
        'btnBrowseProgram
        '
        Me.btnBrowseProgram.Location = New System.Drawing.Point(200, 16)
        Me.btnBrowseProgram.Name = "btnBrowseProgram"
        Me.btnBrowseProgram.Size = New System.Drawing.Size(28, 23)
        Me.btnBrowseProgram.TabIndex = 21
        Me.btnBrowseProgram.Text = "..."
        Me.btnBrowseProgram.UseVisualStyleBackColor = true
        '
        'TxtBxPathProgram
        '
        Me.TxtBxPathProgram.Location = New System.Drawing.Point(63, 19)
        Me.TxtBxPathProgram.Name = "TxtBxPathProgram"
        Me.TxtBxPathProgram.Size = New System.Drawing.Size(131, 20)
        Me.TxtBxPathProgram.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Location = New System.Drawing.Point(7, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Path"
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Location = New System.Drawing.Point(207, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "(Optional)"
        '
        'frmLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 184)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnOpenModel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "frmLoad"
        Me.Text = "Progressive Collapse - Load"
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents TextBox_Directory As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_FileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOpenModel As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowseProgram As System.Windows.Forms.Button
    Friend WithEvents TxtBxPathProgram As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
