Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If
Imports System.Timers
Imports ProgressiveCollapse.cEnumerations

''' <summary>
''' Form containing various development features that have no been incorporated into the main forms.
''' </summary>
''' <remarks></remarks>
Public Class frmDevelopment
#Region "Properties"
    ''' <summary>
    ''' Name of the directory in which the model file is located.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property directory As String
    ''' <summary>
    ''' Name of the model file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileName As String
#End Region

#Region "Initialization"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetData()
        SetControls()
    End Sub

    Sub SetData()
        directory = ProgCol.filePathOriginal
        fileName = ProgCol.fileNameOriginal
    End Sub

    Sub SetControls()
        TextBox_Directory.Text = directory
        TextBox_FileName.Text = fileName
    End Sub
#End Region

#Region "Control Methods"
    ''' <summary>
    ''' Browse for SAP2000 model
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        'TextBox_Directory.Text
        Dim validPath As Boolean = False
        Dim newProgramName As String
        Dim path As String

        path = BrowseForFile("sdb", TextBox_Directory.Text)

        'Retains current value if user cancels out of browse form
        If path = "" Then path = ProgCol.filePathOriginal

        'Extract program type from path
        newProgramName = EndName_FromPath(path)
        newProgramName = Microsoft.VisualBasic.Left(newProgramName, Len(newProgramName) - 4)

        TextBox_Directory.Text = Directory_FromPath(path) & "\"
        TextBox_FileName.Text = newProgramName

    End Sub

    Private Sub btnOpenModel_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub btnCloseModel_Click(sender As Object, e As EventArgs)
        closeProgram()
    End Sub

    ''' <summary>
    ''' Runs analysis of currently activated SAP2000 object. File is not saved.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRunAnalysis_Click(sender As Object, e As EventArgs)
        runAnalysis()
    End Sub

    Private Sub btnRunDesign_Click(sender As Object, e As EventArgs)
        runDesign()
    End Sub
    Private Sub btnUnlock_Click(sender As Object, e As EventArgs)
        unlockModel()
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs)
        test()
    End Sub


    Private Sub btn_PopulateKeyElementFrame_Click(sender As Object, e As EventArgs)
        Dim NumberNames As Integer
        Dim framesListSAP() As String = Nothing

        'get frame object names
        ret = SapModel.FrameObj.GetNameList(NumberNames, framesListSAP)
        retCheck(ret, "SapModel.FrameObj.GetNameList")
        'checkComboRedundant(ComboBox_KeyElementFrame, framesListSAP)

        'ComboBox_KeyElementFrame.Items.AddRange(framesListSAP)

        btn_PopulateKeyElementFrame.Enabled = False
    End Sub
    Private Sub btn_PopulateKeyElementGroup_Click(sender As Object, e As EventArgs)
        Dim NumberNames As Integer
        Dim groupsListKeySAP() As String = Nothing

        'get group names
        ret = SapModel.GroupDef.GetNameList(NumberNames, groupsListKeySAP)
        retCheck(ret, "SapModel.GroupDef.GetNameList")

        'ComboBox_KeyElementGroup.Items.AddRange(groupsListKeySAP)

        btn_PopulateKeyElementGroup.Enabled = False
    End Sub
    Private Sub btn_PopulateCriticalPathGroup_Click(sender As Object, e As EventArgs)
        Dim NumberNames As Integer
        Dim groupsListCriticalPathSAP() As String = Nothing

        'get group names
        ret = SapModel.GroupDef.GetNameList(NumberNames, groupsListCriticalPathSAP)
        retCheck(ret, "SapModel.GroupDef.GetNameList")

        'ComboBox_CriticalPathGroup.Items.AddRange(groupsListCriticalPathSAP)

        btn_PopulateCriticalPathGroup.Enabled = False
    End Sub

    Private Sub TextBox_Directory_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Directory.TextChanged
        directory = TextBox_Directory.Text
    End Sub
    Private Sub TextBox_FileName_TextChanged(sender As Object, e As EventArgs) Handles TextBox_FileName.TextChanged
        fileName = TextBox_FileName.Text
    End Sub
#End Region
End Class