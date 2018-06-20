<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResources
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResources))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnAbout = New System.Windows.Forms.Button()
        Me.btnCSiWeb = New System.Windows.Forms.Button()
        Me.btnDocumentation = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnOK)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(207, 166)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(56, 132)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnAbout)
        Me.GroupBox2.Controls.Add(Me.btnCSiWeb)
        Me.GroupBox2.Controls.Add(Me.btnDocumentation)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(185, 116)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Resources"
        '
        'btnAbout
        '
        Me.btnAbout.Location = New System.Drawing.Point(6, 77)
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(169, 23)
        Me.btnAbout.TabIndex = 3
        Me.btnAbout.Text = "About CSiProgressiveCollapse"
        Me.btnAbout.UseVisualStyleBackColor = True
        '
        'btnCSiWeb
        '
        Me.btnCSiWeb.Location = New System.Drawing.Point(6, 48)
        Me.btnCSiWeb.Name = "btnCSiWeb"
        Me.btnCSiWeb.Size = New System.Drawing.Size(169, 23)
        Me.btnCSiWeb.TabIndex = 2
        Me.btnCSiWeb.Text = "CSi on the Web"
        Me.btnCSiWeb.UseVisualStyleBackColor = True
        '
        'btnDocumentation
        '
        Me.btnDocumentation.Location = New System.Drawing.Point(6, 19)
        Me.btnDocumentation.Name = "btnDocumentation"
        Me.btnDocumentation.Size = New System.Drawing.Size(169, 23)
        Me.btnDocumentation.TabIndex = 1
        Me.btnDocumentation.Text = "Documentation"
        Me.btnDocumentation.UseVisualStyleBackColor = True
        '
        'frmResources
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 194)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmResources"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Resources"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnDocumentation As System.Windows.Forms.Button
    Friend WithEvents btnAbout As System.Windows.Forms.Button
    Friend WithEvents btnCSiWeb As System.Windows.Forms.Button
End Class
