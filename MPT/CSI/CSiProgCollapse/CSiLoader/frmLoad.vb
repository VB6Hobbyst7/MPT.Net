Option Explicit On
Option Strict On

Imports ProgressiveCollapse.mPaths
Imports ProgressiveCollapse.mProgram

''' <summary>
''' Form for loading a particular model file for running the progressive collapse analyses.
''' </summary>
''' <remarks></remarks>
Public Class frmLoad
#Region "Variables"
    Dim pcFilename As String
    Dim pcFileDirectory As String

    Dim pathToEXE As String
    Dim programName As String
    Dim fileExtension As String
    Dim ttBrowse As String = "Browse for the model file to use."
#End Region

#Region "Initialization"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetDefaults()
        SetToolTips()
    End Sub

    ''' <summary>
    ''' Sets the default path and fileename for the form to display.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetDefaults()
        btnOpenModel.Enabled = False
#If COMPILE_ETABS2013 Or COMPILE_ETABS2014 Then
        fileExtension = ".edb"
        programName = "ETABS"
        #IF COMPILE_ETABS2013 Then
            pathToEXE = "C:\Computers and Structures\ETABS 2013\ETABS.exe"
        #ElseIf COMPILE_ETABS2014 Then
            pathToEXE = "C:\Computers and Structures\ETABS 2014\ETABS.exe"
        #End If
#ElseIf COMPILE_SAP2000v16 Or COMPILE_SAP2000v17 Then
        fileExtension = ".sdb"
        programName = "SAP2000"
#If COMPILE_SAP2000v16 Then
        pathToEXE = "C:\Computers and Structures\SAP2000 16\SAP2000.exe"
#ElseIf COMPILE_SAP2000v17 Then
        pathToEXE = "C:\Computers and Structures\SAP2000 17\SAP2000.exe"
#End If
#End If
        pcFileDirectory = "C:\Users\Mark\Documents\Projects\Work - CSI\Remote Work - Offline\Projects - Solo\Progressive Collapse Pushover\PC Models\"
        pcFilename = "PC Model 00"

        TxtBxPathProgram.Text = pathToEXE
        TextBox_FileName.Text = pcFilename
        TextBox_Directory.Text = pcFileDirectory
    End Sub

    ''' <summary>
    ''' Sets the tooltips for the form.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetToolTips()
        Me.ToolTip1.SetToolTip(Me.TxtBxPathProgram, pathToEXE)
        Me.ToolTip1.SetToolTip(Me.TextBox_Directory, pcFileDirectory)
        Me.ToolTip1.SetToolTip(Me.TextBox_FileName, pcFilename)
        Me.ToolTip1.SetToolTip(Me.btnBrowse, ttBrowse)
    End Sub
#End Region

#Region "Form Controls"
    '=== Buttons
    ''' <summary>
    ''' Open selected model and initialize main form and class
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnOpenModel_Click(sender As Object, e As EventArgs) Handles btnOpenModel.Click
        Dim myPath As String
        Dim mydirectory As String
        Dim myFileName As String

        mydirectory = SetFileDirectory(TextBox_Directory.Text)
        myFileName = SetFileName(TextBox_FileName.Text)

        myPath = mydirectory & myFileName

        openProgram(pathToEXE, myPath)

        ProgressiveCollapse.mFriendVariables.myPCControl = New ProgressiveCollapse.frmPCControl
        ProgressiveCollapse.mFriendVariables.ProgCol.filePathOriginal = myPath
        ProgressiveCollapse.mFriendVariables.ProgCol.fileNameOriginal = myFileName

        ProgressiveCollapse.mFriendVariables.myPCControl.Show()

        Me.Hide()
    End Sub

    ''' <summary>
    ''' Closes the program.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    ''' <summary>
    ''' Browses for the program.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowseProgram_Click(sender As Object, e As EventArgs) Handles btnBrowseProgram.Click
        'TextBox_Directory.Text
        Dim validPath As Boolean = False
        Dim newProgramName As String = ""

        BrowseProgram(pathToEXE, newProgramName)

        'Enforces that the user can only select the appropriate program.
        While Not newProgramName = programName
            Select Case MsgBox("Invalid program selected. Please select the installed " & programName & " program to be run.", MsgBoxStyle.RetryCancel, "Not a Valid Program")
                Case MsgBoxResult.Retry : BrowseProgram(pathToEXE, newProgramName)
                Case MsgBoxResult.Cancel : Close()
            End Select
        End While

        TxtBxPathProgram.Text = pathToEXE
    End Sub

    ''' <summary>
    ''' Browse for model file.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        'TextBox_Directory.Text
        Dim validPath As Boolean = False
        Dim newModelName As String
        Dim path As String

        path = BrowseForFile(Microsoft.VisualBasic.Right(fileExtension, Len(fileExtension) - 1), TextBox_Directory.Text)

        'Retains current value if user cancels out of browse form
        If path = "" Then path = pcFileDirectory & pcFilename

        'Extract program type from path
        newModelName = EndName_FromPath(path)
        If Mid(path, Len(path) - 3, 1) = "." Then newModelName = Microsoft.VisualBasic.Left(newModelName, Len(newModelName) - 4)

        TextBox_Directory.Text = Directory_FromPath(path) & "\"
        TextBox_FileName.Text = newModelName
    End Sub

    '=== Text Boxes
    Private Sub TxtBxPathProgram_TextChanged(sender As Object, e As EventArgs) Handles TxtBxPathProgram.TextChanged
        pathToEXE = TxtBxPathProgram.Text
        Me.ToolTip1.SetToolTip(Me.TxtBxPathProgram, pathToEXE)

        'Enable/Disable Open Model button depending on if required parameters have been defined.
        ValidateSession()
    End Sub
    Private Sub TextBox_FileName_TextChanged(sender As Object, e As EventArgs) Handles TextBox_FileName.TextChanged
        pcFilename = TextBox_FileName.Text
        Me.ToolTip1.SetToolTip(Me.TextBox_FileName, pcFilename)

        'Enable/Disable Open Model button depending on if required parameters have been defined.
         ValidateSession()
    End Sub
    Private Sub TextBox_Directory_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Directory.TextChanged
        pcFileDirectory = TextBox_Directory.Text
        Me.ToolTip1.SetToolTip(Me.TextBox_Directory, pcFileDirectory)

        'Enable/Disable Open Model button depending on if required parameters have been defined.
        ValidateSession()
    End Sub
#End Region

#Region "Methods"

    Sub ValidateSession()
        Dim pathToModel As String
        pathToModel = SetFileDirectory(TextBox_Directory.Text) & SetFileName(TextBox_FileName.Text)

        'Enable/Disable Open Model button depending on if required parameters have been defined.
        If System.IO.File.Exists(pathToModel) Then
            If System.IO.File.Exists(pathToEXE) Then btnOpenModel.Enabled = True
        Else
            btnOpenModel.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Creates a browser for selecting the path to the desired program. Returns the program path and name.
    ''' </summary>
    ''' <param name="path">Path to the program to be run.</param>
    ''' <param name="newProgramName">Name of the program, to be determined from the path.</param>
    ''' <remarks></remarks>
    Sub BrowseProgram(ByRef path As String, ByRef newProgramName As String)
        Dim mypath As String = path

        mypath = BrowseForFile("EXE", TextBox_Directory.Text)

        'Retains current value if user cancels out of browse form
        If mypath = "" Then
            mypath = path
            newProgramName = programName
            Exit Sub
        Else
            path = mypath
        End If

        'Extract program type from path, and validate against the type allowed.
        newProgramName = EndName_FromPath(mypath)
        If Mid(newProgramName, Len(newProgramName) - 3, 1) = "." Then newProgramName = Microsoft.VisualBasic.Left(newProgramName, Len(newProgramName) - 4)
    End Sub

    ''' <summary>
    ''' Creates the filename with the appropriate extension, from the value supplied.
    ''' </summary>
    ''' <param name="myFileName">Name of the model file. Does not need the file extension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetFileName(ByVal myFileName As String) As String
        myFileName = TextBox_FileName.Text
        If Not Microsoft.VisualBasic.Right(myFileName, 4) = "." Then myFileName = myFileName & fileExtension

        SetFileName = myFileName
    End Function

    ''' <summary>
    ''' Creates a directory path with the appropriate ending for combining with the filename, from the directory value supplied.
    ''' </summary>
    ''' <param name="mydirectory">Path to the directory. May or may not end in a "\".</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetFileDirectory(ByVal mydirectory As String) As String
        mydirectory = TextBox_Directory.Text
        If Not Microsoft.VisualBasic.Right(mydirectory, 1) = "\" Then mydirectory = mydirectory & "\"

        SetFileDirectory = mydirectory
    End Function

#End Region

End Class