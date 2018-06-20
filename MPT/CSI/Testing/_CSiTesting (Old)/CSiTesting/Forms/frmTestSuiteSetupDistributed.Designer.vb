<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTestSuiteSetupDistributed
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTestSuiteSetupDistributed))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label_ModelsSource = New System.Windows.Forms.Label()
        Me.Label_ServerBaseURL = New System.Windows.Forms.Label()
        Me.TextBox_ServerBaseURL = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ComboBox_ModelsSource = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label_ModelsSource)
        Me.GroupBox1.Controls.Add(Me.Label_ServerBaseURL)
        Me.GroupBox1.Controls.Add(Me.TextBox_ServerBaseURL)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.ComboBox_ModelsSource)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(254, 149)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label_ModelsSource
        '
        Me.Label_ModelsSource.AutoSize = True
        Me.Label_ModelsSource.Location = New System.Drawing.Point(6, 68)
        Me.Label_ModelsSource.Name = "Label_ModelsSource"
        Me.Label_ModelsSource.Size = New System.Drawing.Size(78, 13)
        Me.Label_ModelsSource.TabIndex = 17
        Me.Label_ModelsSource.Text = "Models Source"
        '
        'Label_ServerBaseURL
        '
        Me.Label_ServerBaseURL.AutoSize = True
        Me.Label_ServerBaseURL.Location = New System.Drawing.Point(6, 20)
        Me.Label_ServerBaseURL.Name = "Label_ServerBaseURL"
        Me.Label_ServerBaseURL.Size = New System.Drawing.Size(90, 13)
        Me.Label_ServerBaseURL.TabIndex = 16
        Me.Label_ServerBaseURL.Text = "Server Base URL"
        '
        'TextBox_ServerBaseURL
        '
        Me.TextBox_ServerBaseURL.Location = New System.Drawing.Point(9, 36)
        Me.TextBox_ServerBaseURL.Name = "TextBox_ServerBaseURL"
        Me.TextBox_ServerBaseURL.Size = New System.Drawing.Size(239, 20)
        Me.TextBox_ServerBaseURL.TabIndex = 15
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(131, 115)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(50, 115)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 11
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'ComboBox_ModelsSource
        '
        Me.ComboBox_ModelsSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ModelsSource.FormattingEnabled = True
        Me.ComboBox_ModelsSource.Location = New System.Drawing.Point(9, 84)
        Me.ComboBox_ModelsSource.Name = "ComboBox_ModelsSource"
        Me.ComboBox_ModelsSource.Size = New System.Drawing.Size(158, 21)
        Me.ComboBox_ModelsSource.TabIndex = 10
        '
        'frmTestSuiteSetupDistributed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 159)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTestSuiteSetupDistributed"
        Me.Text = "Distributed Testing Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents ComboBox_ModelsSource As System.Windows.Forms.ComboBox
    Friend WithEvents Label_ModelsSource As System.Windows.Forms.Label
    Friend WithEvents Label_ServerBaseURL As System.Windows.Forms.Label
    Friend WithEvents TextBox_ServerBaseURL As System.Windows.Forms.TextBox
End Class
