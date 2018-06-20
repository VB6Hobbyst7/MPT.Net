<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerificationControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVerificationControl))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnRunSetup = New System.Windows.Forms.Button()
        Me.btnTestSuiteSetup = New System.Windows.Forms.Button()
        Me.btnSolverOptions = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(39, 64)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(826, 374)
        Me.DataGridView1.TabIndex = 0
        '
        'btnRunSetup
        '
        Me.btnRunSetup.Location = New System.Drawing.Point(145, 12)
        Me.btnRunSetup.Name = "btnRunSetup"
        Me.btnRunSetup.Size = New System.Drawing.Size(75, 23)
        Me.btnRunSetup.TabIndex = 1
        Me.btnRunSetup.Text = "Run Setup"
        Me.btnRunSetup.UseVisualStyleBackColor = True
        '
        'btnTestSuiteSetup
        '
        Me.btnTestSuiteSetup.Location = New System.Drawing.Point(39, 12)
        Me.btnTestSuiteSetup.Name = "btnTestSuiteSetup"
        Me.btnTestSuiteSetup.Size = New System.Drawing.Size(100, 23)
        Me.btnTestSuiteSetup.TabIndex = 2
        Me.btnTestSuiteSetup.Text = "Test Suite Setup"
        Me.btnTestSuiteSetup.UseVisualStyleBackColor = True
        '
        'btnSolverOptions
        '
        Me.btnSolverOptions.Location = New System.Drawing.Point(226, 12)
        Me.btnSolverOptions.Name = "btnSolverOptions"
        Me.btnSolverOptions.Size = New System.Drawing.Size(94, 23)
        Me.btnSolverOptions.TabIndex = 3
        Me.btnSolverOptions.Text = "Solver Options"
        Me.btnSolverOptions.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(326, 12)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(94, 23)
        Me.btnReport.TabIndex = 4
        Me.btnReport.Text = "Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'frmVerificationControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(914, 477)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnSolverOptions)
        Me.Controls.Add(Me.btnTestSuiteSetup)
        Me.Controls.Add(Me.btnRunSetup)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVerificationControl"
        Me.Text = "CSi Test"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnRunSetup As System.Windows.Forms.Button
    Friend WithEvents btnTestSuiteSetup As System.Windows.Forms.Button
    Friend WithEvents btnSolverOptions As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button

End Class
