Option Explicit On
Module formControls
    Public frmInitialize As Boolean
    Public lblProgram As LinkLabel  'Problem. Fix this. Check that this works for updating between forms
    Public cmbbxFileType As ComboBox 'Problem. Fix this. Check that this works for updating between forms

#Region "Common Subs"
    Public Sub btnOpen(ByRef formObj As Form)
        'opens form
        formObj.ShowDialog()
        formObj.Dispose()
    End Sub

    Public Sub btnClose(ByRef formObj As Form)
        'closes form
        formObj.Close()
    End Sub

    Public Sub btnBrowse(ByRef formObj As Form, Optional ByVal frmFuncNum As Long = 1)
        Dim currDir As String

        currDir = Application.StartupPath

        ''============
        If formObj.Name = "frmRunSetup" Then
            Call BrowseForFile("EXE", currDir)

            If path = "" Then path = regTest.program_path 'Retains current value if user cancels out of browse form

            If frmRunSetup.Option_LocationRelative.Checked = True Then
                path = convAbsRel(currDir, path, True)
                frmRunSetup.TextBox_Location.Text = path
            Else
                frmRunSetup.TextBox_Location.Text = path
            End If
        End If
        ''============
        If formObj.Name = "frmRunSetupAdvanced" Then
            Call BrowseForFile("XML", currDir)

            If path = "" Then path = regTest.previous_test_results_file_path 'Retains current value if user cancels out of browse form

            If frmRunSetupAdvanced.Option_LocationRelative.Checked = True Then
                path = convAbsRel(currDir, path, True)
                frmRunSetupAdvanced.TextBox_Location.Text = path
            Else
                frmRunSetupAdvanced.TextBox_Location.Text = path
            End If
        End If
        ''============
        If formObj.Name = "frmTestSuiteSetup" Then

            currDir = currDir & "\regtest"
            If frmFuncNum = 1 Then  'Browse Source
                Call BrowseForFolder("Browse for location from which models are to be copied", currDir)

                If path = "" Then path = regTest.models_database_directory 'Retains current value if user cancels out of browse form

                If frmTestSuiteSetup.Option_SourceRelative.Checked = True Then
                    path = convAbsRel(currDir, path)
                    frmTestSuiteSetup.TextBox_Source.Text = path
                Else
                    frmTestSuiteSetup.TextBox_Source.Text = path
                End If
            ElseIf frmFuncNum = 2 Then  'Browse Destination
                Call BrowseForFolder("Browse for location to which models are to be copied", currDir)

                If path = "" Then path = regTest.models_run_directory 'Retains current value if user cancels out of browse form

                If frmTestSuiteSetup.Option_DestinationRelative.Checked = True Then
                    path = convAbsRel(currDir, path)
                    frmTestSuiteSetup.TextBox_Destination.Text = path
                Else
                    frmTestSuiteSetup.TextBox_Destination.Text = path
                End If
            End If
        End If
    End Sub

    Public Sub optnPathRelAbs(ByRef formObj As Form, ByVal RelToAbs As Boolean, Optional ByVal frmFuncNum As Long = 1, Optional ByVal frmInitialize As Boolean = False)
        If frmInitialize Then Exit Sub 'Prevents this action from ocurring when form is initializing

        Dim currDir As String
        Dim appDir As String

        Dim _textBox As TextBox
        Dim _optnAbs As RadioButton
        Dim _optnRel As RadioButton
        Dim file As Boolean

        'Default startup
        _textBox = frmTestSuiteSetup.TextBox_Source
        _optnAbs = frmTestSuiteSetup.Option_SourceAbsolute
        _optnRel = frmTestSuiteSetup.Option_SourceRelative

        appDir = Application.StartupPath
        Select Case formObj.Name
            Case "frmTestSuiteSetup"
                currDir = appDir & "\regtest"
                file = False
                If frmFuncNum = 1 Then
                    _textBox = frmTestSuiteSetup.TextBox_Source
                    _optnAbs = frmTestSuiteSetup.Option_SourceAbsolute
                    _optnRel = frmTestSuiteSetup.Option_SourceRelative
                ElseIf frmFuncNum = 2 Then
                    '_textBox = frmTestSuiteSetup.TextBox_Destination
                    '_optnAbs = frmTestSuiteSetup.Option_DestinationAbsolute
                    '_optnRel = frmTestSuiteSetup.Option_DestinationRelative
                End If
                'Case "frmRunSetup"
                '    currDir = appDir
                '    file = True
                '    _textBox = frmRunSetup.TextBox_Location
                '    _optnAbs = frmRunSetup.Option_LocationAbsolute
                '    _optnRel = frmRunSetup.Option_LocationRelative
            Case "frmRunSetupAdvanced"
                currDir = appDir & "\regtest"
                file = True
                _textBox = frmRunSetupAdvanced.TextBox_Location
                _optnAbs = frmRunSetupAdvanced.Option_LocationAbsolute
                _optnRel = frmRunSetupAdvanced.Option_LocationRelative
            Case Else   'determine appropriate default objects
                currDir = appDir
                file = True
                _textBox = frmTestSuiteSetup.TextBox_Source
                _optnAbs = frmTestSuiteSetup.Option_SourceAbsolute
                _optnRel = frmTestSuiteSetup.Option_SourceRelative
        End Select

        If RelToAbs And _optnAbs.Checked Then _textBox.Text = convRelAbs(currDir, _textBox.Text)
        If Not RelToAbs And _optnRel.Checked Then _textBox.Text = convAbsRel(currDir, _textBox.Text, file)

    End Sub
#End Region

#Region "Form Initialization Functions"
    Public Sub initFrmRunSetup()
        Dim i As Long
        '        Dim toolTip As String
        Dim programFileType As String()

        frmInitialize = True

        'Assign public variables so that these can be updated by changes in other forms
        lblProgram = frmRunSetup.Label_Program
        cmbbxFileType = frmRunSetup.ComboBox_FileType

        'Text Boxes
        frmRunSetup.Label_Program.Text = regTest.program_name
        frmRunSetup.TextBox_Version.Text = regTest.program_version
        frmRunSetup.TextBox_Build.Text = regTest.program_build
        frmRunSetup.TextBox_Location.Text = regTest.program_path

        'Check Boxes
        If regTest.start_distributed_test = "yes" Then
            frmRunSetup.CheckBox_DistTestStart.CheckState = CheckState.Checked
        Else
            frmRunSetup.CheckBox_DistTestStart.CheckState = CheckState.Unchecked
        End If
        If regTest.join_distributed_test = "yes" Then
            frmRunSetup.CheckBox_DistTestJoin.CheckState = CheckState.Checked
        Else
            frmRunSetup.CheckBox_DistTestJoin.CheckState = CheckState.Unchecked
        End If


        'Radio Buttons
        '~~~~Path
        If regTest.program_attribType = "absolute" Then
            frmRunSetup.Option_LocationAbsolute.Checked = True
            frmRunSetup.Option_LocationRelative.Checked = False
        Else
            frmRunSetup.Option_LocationAbsolute.Checked = False
            frmRunSetup.Option_LocationRelative.Checked = True
        End If
        '~Testing Type
        If regTest.run_local_test = "yes" Then
            frmRunSetup.Option_Local.Checked = True
            frmRunSetup.Option_Distributed.Checked = False
        Else
            frmRunSetup.Option_Local.Checked = False
            frmRunSetup.Option_Distributed.Checked = True
        End If
        If regTest.start_distributed_test = "yes" Or regTest.join_distributed_test = "yes" Then
            frmRunSetup.Option_Distributed.Checked = True
            frmRunSetup.Option_Local.Checked = False
        Else
            frmRunSetup.Option_Local.Checked = True
            frmRunSetup.Option_Distributed.Checked = False
        End If


        'Combo Box
        programFileType = checkDropDownList() 'Collects the file type list that corresponds to a given program and assigns it to dropdown array variable
        frmRunSetup.ComboBox_FileType.Items.AddRange(programFileType)  'Populates listbox from specified range in Excel sheet

        'Combo Box Update.
        For i = 0 To frmRunSetup.ComboBox_FileType.Items.Count - 1  'Selects initial cell
            If i = 0 Then
                If frmRunSetup.ComboBox_FileType.Items(i) <> regTest.fileType Then  'Defaults to standard file type for program selected if there is a mismatch between lists.
                    regTest.fileType = regTest.getDefaultFileType
                End If
            End If
            If frmRunSetup.ComboBox_FileType.Items(i) = regTest.fileType Then
                frmRunSetup.ComboBox_FileType.SelectedItem = regTest.fileType
                Exit For
            End If
        Next i

        ''Add Tooltips
        'toolTip = "Path to program *.exe file to be run"
        'FrameLocation.ControlTipText = toolTip
        'TextBox_Location.ControlTipText = toolTip

        ''    toolTip = "Any combination of these three choices could be specified: Local Testing-for running local test; Start Distributed Test & Join Distributed Test - for starting and joining (from the same machine) distributed test; Join Distributed Test - for joining distributed test that has been initialized by another machine"
        ''    Frame_TestingType.ControlTipText = toolTip

        'toolTip = "Local Testing-for running local test"
        'Option_Local.ControlTipText = toolTip

        'toolTip = "Starting distributed testing on remote servers."
        'CheckBox_DistTestStart.ControlTipText = toolTip
        'Option_Distributed.ControlTipText = toolTip

        'toolTip = "For joining distributed test that has been initialized by another machine, or the same machine if 'start test' is selected."
        'CheckBox_DistTestJoin.ControlTipText = toolTip

        frmInitialize = False
    End Sub
    Public Sub initFrmRunSetupAdvanced()
        '        Dim toolTip As String

        frmInitialize = True

        'Text Boxes
        frmRunSetupAdvanced.TextBox_TimeMultiplier.Text = regTest.runtime_limit_overwrites_multiplier
        frmRunSetupAdvanced.TextBox_MaxRuntime.Text = regTest.maximum_permitted_runtime
        frmRunSetupAdvanced.TextBox_DecimalDigits.Text = regTest.percent_difference_decimal_digits
        frmRunSetupAdvanced.TextBox_Location.Text = regTest.previous_test_results_file_path

        'Check Boxes
        If regTest.write_log_files = "yes" Then
            frmRunSetupAdvanced.CheckBox_LogFiles.CheckState = CheckState.Checked
        Else
            frmRunSetupAdvanced.CheckBox_LogFiles.CheckState = CheckState.Unchecked
        End If

        If regTest.email_notifications_attrib = "yes" Then
            frmRunSetupAdvanced.CheckBox_Email.CheckState = CheckState.Checked
        Else
            frmRunSetupAdvanced.CheckBox_Email.CheckState = CheckState.Unchecked
            frmRunSetupAdvanced.btnEmailList.Enabled = False
        End If

        'Radio Buttons
        If regTest.previous_test_results_file_path_attrib = "absolute" Then
            frmRunSetupAdvanced.Option_LocationAbsolute.Checked = True
            frmRunSetupAdvanced.Option_LocationRelative.Checked = False
        Else
            frmRunSetupAdvanced.Option_LocationAbsolute.Checked = False
            frmRunSetupAdvanced.Option_LocationRelative.Checked = True
        End If

        ''Add Tooltips
        'toolTip = "Send e-mail to selected recipients once test is done."
        'CheckBox_Email.ControlTipText = toolTip

        'toolTip = "Run time limit overwrites can be used to adjust runtimes for individual models."
        'Frame_RuntimeLimitsOverwrites.ControlTipText = toolTip

        'toolTip = "Multiplies each model's estimated time by this number for max allowed time for each model"
        'Label_TimeMultiplier.ControlTipText = toolTip
        'TextBox_TimeMultiplier.ControlTipText = toolTip

        'toolTip = "For any single model. Cap for the overall effects of the multiplier result"
        'Label_MaxRuntime.ControlTipText = toolTip
        'TextBox_MaxRuntime.ControlTipText = toolTip

        'toolTip = "Number of decimal digits for the percent difference values."
        'Frame_PercDiff.ControlTipText = toolTip
        'Label_DecimalDigits.ControlTipText = toolTip
        'TextBox_DecimalDigits.ControlTipText = toolTip

        'toolTip = "Path to XML file"
        'FrameLocation.ControlTipText = toolTip
        'TextBox_Location.ControlTipText = toolTip

        frmInitialize = False
    End Sub
    Public Sub initFrmRunSetupEmailList()
        Dim i As Long

        frmInitialize = True

        frmRunSetupEmailList.ListBox_EmailList.Items.AddRange(regTest.email_address_List.ToArray)  'Populates listbox from specified range in Excel sheet

        For i = 0 To frmRunSetupEmailList.ListBox_EmailList.Items.Count    'Selects initial cell
            If frmRunSetupEmailList.ListBox_EmailList.Items(i) = regTest.email_address_List(i) Then
                frmRunSetupEmailList.ListBox_EmailList.Text = regTest.email_address_List(i)
                Exit For
            End If
        Next i

        frmInitialize = False
    End Sub

    Public Sub initFrmSolverOptions()
        'Sets up defaults if not specified
        If regTest.Solver = "" Then regTest.Solver = "Default"
        If regTest.Process = "" Then regTest.Process = "Default"
        If regTest.ThirtyTwoBit = "" Then regTest.ThirtyTwoBit = "Default"

        'Set Solver Parameter
        If regTest.Solver = "Default" Then
            frmSolverOptions.Option_SolverDefault.Checked = True
        ElseIf regTest.Solver = "Force Standard" Then
            frmSolverOptions.Option_SolverStandard.Checked = True
        ElseIf regTest.Solver = "Force Advanced" Then
            frmSolverOptions.Option_SolverAdvanced.Checked = True
        ElseIf regTest.Solver = "Force Multi-threaded" Then
            frmSolverOptions.Option_SolverMulti.Checked = True
        End If

        'Set Process Parameter
        If regTest.Process = "Default" Then
            frmSolverOptions.Option_ProcessDefault.Checked = True
        ElseIf regTest.Process = "Force Same" Then
            frmSolverOptions.Option_ProcessSame.Checked = True
        ElseIf regTest.Process = "Force Separate" Then
            frmSolverOptions.Option_ProcessSeparate.Checked = True
        End If

        'Set 32 Bit Parameter
        If regTest.ThirtyTwoBit = "Default" Then
            frmSolverOptions.Option_ThirtyTwoDefault.Checked = True
        ElseIf regTest.ThirtyTwoBit = "Force 32 Bit" Then
            frmSolverOptions.Option_ForceThirtyTwo.Checked = True
        ElseIf regTest.ThirtyTwoBit = "Force Not 32 Bit" Then
            frmSolverOptions.Option_ForceSixtyFour.Checked = True
        End If

        'Set File Delete Parameter
        If regTest.DeleteAnalysisFiles = "Yes" Then
            frmSolverOptions.CheckBox_Delete.CheckState = CheckState.Checked
        Else : frmSolverOptions.CheckBox_Delete.CheckState = CheckState.Unchecked
        End If
    End Sub

    Public Sub initFrmTestSuiteSetup()
        Dim i As Long
        '       Dim toolTip As String

        frmInitialize = True

        '~Text Boxes
        frmTestSuiteSetup.TextBox_TestName.Text = regTest.test_description
        frmTestSuiteSetup.TextBox_TestID.Text = regTest.test_id
        frmTestSuiteSetup.TextBox_Source.Text = regTest.models_database_directory
        frmTestSuiteSetup.TextBox_Destination.Text = regTest.models_run_directory

        'Check Boxes
        If regTest.copy_models_flat_attribRun = "yes" Then
            frmTestSuiteSetup.CheckBox_CopyModelsFlat.CheckState = CheckState.Checked
        Else
            frmTestSuiteSetup.CheckBox_CopyModelsFlat.CheckState = CheckState.Unchecked
        End If

        'Radio Buttons
        '~~~Source Path
        If regTest.models_database_directory_attrib = "absolute" Then
            frmTestSuiteSetup.Option_SourceAbsolute.Checked = True
            frmTestSuiteSetup.Option_SourceRelative.Checked = False
        Else
            frmTestSuiteSetup.Option_SourceAbsolute.Checked = False
            frmTestSuiteSetup.Option_SourceRelative.Checked = True
        End If

        '~~~Destination Path
        If regTest.models_run_directory_attrib = "absolute" Then
            frmTestSuiteSetup.Option_DestinationAbsolute.Checked = True
            frmTestSuiteSetup.Option_DestinationRelative.Checked = False
        Else
            frmTestSuiteSetup.Option_DestinationAbsolute.Checked = False
            frmTestSuiteSetup.Option_DestinationRelative.Checked = True
        End If

        '~~Models Used
        If regTest.copy_models_AttribRun = "yes" Then
            frmTestSuiteSetup.Option_CopyModels.Checked = True
            frmTestSuiteSetup.Option_CopyModelsNo.Checked = False
        Else
            frmTestSuiteSetup.Option_CopyModels.Checked = False
            frmTestSuiteSetup.Option_CopyModelsNo.Checked = True
        End If

        'Combo Boxes
        frmTestSuiteSetup.ComboBox_Programs.Items.AddRange(regTest.dropdownList_programs)  'Populates listbox from specified range in Excel sheet

        For i = 0 To frmTestSuiteSetup.ComboBox_Programs.Items.Count    'Selects initial cell
            If frmTestSuiteSetup.ComboBox_Programs.Items(i) = regTest.program_name Then
                frmTestSuiteSetup.ComboBox_Programs.Text = regTest.program_name
                Exit For
            End If
        Next i


        ''Add Tooltips
        'toolTip = "Path to model database directory. The model will not be run in this location. Only data for models with XML files are copied to the destination."
        'FrameSource.ControlTipText = toolTip
        'TextBox_Source.ControlTipText = toolTip

        'toolTip = "Location of the directory in which the models should be run."
        'FrameDestination.ControlTipText = toolTip
        'TextBox_Destination.ControlTipText = toolTip

        'toolTip = "Default uses all models in Destination of the appropriate file type"
        'btnSelectModels.ControlTipText = toolTip

        'toolTip = "Copies models from the models database directory to the models run directory. Select only when copying a fresh batch. Leave blank if models already exist at location."
        'Option_CopyModels.ControlTipText = toolTip

        'toolTip = "Copies models from the models database directory to the models run directory. The source directory is scanned all model XML files and only data for models with XML files are copied to the target directory."
        'CheckBox_CopyModelsFlat.ControlTipText = toolTip

        frmInitialize = False
    End Sub
    Public Sub initFrmTestSuiteSetupAdvanced()
        Dim i As Long
        '       Dim toolTip As String

        frmInitialize = True

        'Checkboxes
        '~XML Operations
        If regTest.copy_models_flat_attribReuse = "yes" Then
            frmTestSuiteSetupAdvanced.CheckBox_ReUseXMLList.CheckState = CheckState.Checked
        Else
            frmTestSuiteSetupAdvanced.CheckBox_ReUseXMLList.CheckState = CheckState.Unchecked
        End If

        If regTest.write_xml_files_list_attrib = "yes" Then
            frmTestSuiteSetupAdvanced.CheckBox_ListXML.CheckState = CheckState.Checked
        Else
            frmTestSuiteSetupAdvanced.CheckBox_ListXML.CheckState = CheckState.Unchecked
        End If

        '~Dry Run Options
        If regTest.copy_models_flat_attribDryRun = "yes" Then
            frmTestSuiteSetupAdvanced.CheckBox_CopyModelsFlat.CheckState = CheckState.Checked
        Else
            frmTestSuiteSetupAdvanced.CheckBox_CopyModelsFlat.CheckState = CheckState.Unchecked
        End If

        If regTest.test_to_run_attrib = "yes" Then
            frmTestSuiteSetupAdvanced.CheckBox_EnableDryRun.CheckState = CheckState.Checked
        Else
            frmTestSuiteSetupAdvanced.CheckBox_EnableDryRun.CheckState = CheckState.Unchecked
        End If

        'Combo Box
        '~Test to Run
        frmTestSuiteSetupAdvanced.ComboBox_TestToRun.Items.AddRange(regTest.dropdownList_TestToRun)  'Populates listbox from specified range in Excel sheet

        For i = 0 To frmTestSuiteSetupAdvanced.ComboBox_TestToRun.Items.Count    'Selects initial cell
            If frmTestSuiteSetupAdvanced.ComboBox_TestToRun.Items(i) = regTest.test_to_run Then
                frmTestSuiteSetupAdvanced.ComboBox_TestToRun.Text = regTest.test_to_run
                Exit For
            End If
        Next i

        ''Add Tooltips
        'toolTip = "Specifies whether the program should rescan the file system for new model XML files or just reuse the previously build list."
        'CheckBox_ReUseXMLList.ControlTipText = toolTip

        'toolTip = "This can be used to only list all model XML files in the models database directory without running any tests. The list of model XML files is written into the regtest/out directory."
        'CheckBox_ListXML.ControlTipText = toolTip

        'toolTip = "Copies models from the models database directory to the models run directory. The source directory is scanned all model XML files and only data for models with XML files are copied to the target directory."
        'CheckBox_CopyModelsFlat.ControlTipText = toolTip

        frmInitialize = False
    End Sub
    Public Sub initFrmTestSuiteSetupDistributed()
        Dim i As Long
        '        Dim toolTip As String

        frmInitialize = True

        frmTestSuiteSetupDistributed.TextBox_ServerBaseURL.Text = regTest.server_base_URL

        frmTestSuiteSetupDistributed.ComboBox_ModelsSource.Items.AddRange(regTest.dropdownList_DistTestSource)  'Populates listbox from specified range in Excel sheet

        For i = 0 To frmTestSuiteSetupDistributed.ComboBox_ModelsSource.Items.Count    'Selects initial cell
            If frmTestSuiteSetupDistributed.ComboBox_ModelsSource.Items(i) = regTest.models_source Then
                frmTestSuiteSetupDistributed.ComboBox_ModelsSource.Text = regTest.models_source
                Exit For
            End If
        Next i

        ''Add tooltips
        'toolTip = "Base url of the regression testing control server. Do not include trailing slash - Current default: http://www.kalny.name/csi/regtest/app"
        'Label_ServerBaseURL.ControlTipText = toolTip
        'TextBox_ServerBaseURL.ControlTipText = toolTip

        'toolTip = "Location from where the model files should be downloaded"
        'Label_ModelsSource.ControlTipText = toolTip
        'ComboBox_ModelsSource.ControlTipText = toolTip

        frmInitialize = False
    End Sub

    Public Sub initFrmVerificationControl()
        ' Add any initialization after the InitializeComponent() call.
        initializeIniFile() 'Checks if .ini file exists, and if not, writes one with default parameters. This is needed for locating the XML files

        regTest = New cRegTest
        regtestXML_read_write_regTest(True) 'For values of unique node 
        regtestXML_read_write_List(True)    'For values of non-unique nodes
        'regtestXML_read_write_Objects(True) 'For non-unique values of non-unique nodes
    End Sub
    Public Sub initFrmVerificationReport()
        '' ProgressBar1
        'labPg1.Tag = labPg1.Width
        'labPg1.Width = 0
        'labPg1v.Caption = ""
        'labPg1va.Caption = ""

        Select Case frmVerificationReport.CheckBox_PrepareReport.CheckState
            Case CheckState.Checked
                frmVerificationReport.CheckBox_DeleteFiles.Enabled = True
                frmVerificationReport.CheckBox_ZipAll.Enabled = True
                frmVerificationReport.CheckBox_ZipIndividual.Enabled = True
            Case CheckState.Unchecked
                frmVerificationReport.CheckBox_DeleteFiles.Enabled = False : frmVerificationReport.CheckBox_DeleteFiles.CheckState = CheckState.Unchecked
                frmVerificationReport.CheckBox_ZipAll.Enabled = False : frmVerificationReport.CheckBox_ZipAll.CheckState = CheckState.Unchecked
                frmVerificationReport.CheckBox_ZipIndividual.Enabled = False : frmVerificationReport.CheckBox_ZipIndividual.CheckState = CheckState.Unchecked
        End Select
    End Sub
#End Region

#Region "Form OK-Save Functions"
    Public Sub OKFrmRunSetup()
        Dim closeForm As Boolean
        closeForm = True    'If an incorrect entry warning appears, form will not close after warning is closed

        regTest.program_version = frmRunSetup.TextBox_Version.Text
        regTest.program_build = frmRunSetup.TextBox_Build.Text
        regTest.program_version_build = frmRunSetup.TextBox_Version.Text & "_" & frmRunSetup.TextBox_Build.Text

        '======Update values in sheet or XML here
        'File Type List: Verify that an item was selected, and if so, assign value to Excel sheet
        If frmRunSetup.ComboBox_FileType.SelectedIndex <> -1 Then
            regTest.fileType = frmRunSetup.ComboBox_FileType.Items(frmRunSetup.ComboBox_FileType.SelectedIndex)
        Else
            MsgBox("No file type was selected!")
            closeForm = False
        End If

        '~~Program Paths
        '~~~Source
        regTest.program_path = frmRunSetup.TextBox_Location.Text
        '~~~~Path
        If frmRunSetup.Option_LocationAbsolute.Checked = True Then
            regTest.program_attribType = "absolute"
        Else
            regTest.program_attribType = "relative"
        End If

        '~Testing Type
        If frmRunSetup.Option_Local.Checked = True Then
            regTest.run_local_test = "yes"
        Else
            regTest.run_local_test = "no"
        End If
        If frmRunSetup.CheckBox_DistTestStart.CheckState = CheckState.Checked Then
            regTest.start_distributed_test = "yes"
        Else
            regTest.start_distributed_test = "no"
        End If
        If frmRunSetup.CheckBox_DistTestJoin.CheckState = CheckState.Checked Then
            regTest.join_distributed_test = "yes"
        Else
            regTest.join_distributed_test = "no"
        End If
        'Error Catching
        If frmRunSetup.Option_Local.Checked = False And frmRunSetup.CheckBox_DistTestStart.CheckState = CheckState.Unchecked And frmRunSetup.CheckBox_DistTestJoin.CheckState = CheckState.Unchecked Then
            MsgBox("Please select a Distributed Test action or a Local Testing type")
            closeForm = False
        End If

        If closeForm Then formControls.btnClose(frmRunSetup)
    End Sub
    Public Sub OKFrmRunSetupAdvanced()
        '===Save Results
        'Text Boxes
        regTest.runtime_limit_overwrites_multiplier = frmRunSetupAdvanced.TextBox_TimeMultiplier.Text
        regTest.maximum_permitted_runtime = frmRunSetupAdvanced.TextBox_MaxRuntime.Text
        regTest.percent_difference_decimal_digits = frmRunSetupAdvanced.TextBox_DecimalDigits.Text
        regTest.previous_test_results_file_path = frmRunSetupAdvanced.TextBox_Location.Text

        'Check Boxes
        If frmRunSetupAdvanced.CheckBox_LogFiles.CheckState = CheckState.Checked Then
            regTest.write_log_files = "yes"
        Else
            regTest.write_log_files = "no"
        End If

        If frmRunSetupAdvanced.CheckBox_Email.CheckState = CheckState.Checked Then
            regTest.email_notifications_attrib = "yes"
        Else
            regTest.email_notifications_attrib = "no"
        End If

        'Radio Buttons
        If frmRunSetupAdvanced.Option_LocationAbsolute.Checked = True Then
            regTest.previous_test_results_file_path_attrib = "absolute"
        Else
            regTest.previous_test_results_file_path_attrib = "relative"
        End If

        formControls.btnClose(frmRunSetupAdvanced)
    End Sub
    Public Sub OKFrmRunSetupEmailList()
        Dim i As Long
        Dim numItems As Long
        Dim listEmail() As String

        '===Save Results
        numItems = frmRunSetupEmailList.ListBox_EmailList.Items.Count
        i = numItems - 1
        ReDim listEmail(i)

        For i = 0 To numItems - 1
            listEmail(i) = frmRunSetupEmailList.ListBox_EmailList.Items(i)
        Next i

        'Check for change in number of entries and adjust accordingly
        If numItems >= regTest.email_address_List.Count Then   'Enlarge array in class to accommodate additional entries
            ReDim regTest.email_address_List(numItems - 1)
            'Fill values entry by entry
            For i = 0 To numItems - 1
                regTest.email_address_List(i) = listEmail(i)
            Next i
        Else 'Create enlarged array to replace old values with empty ones
            i = regTest.email_address_List.Count
            ReDim Preserve listEmail(i - 1)
            For i = 0 To regTest.email_address_List.Count - 1
                regTest.email_address_List(i) = listEmail(i)
            Next i
        End If

        formControls.btnClose(frmRunSetupEmailList)
    End Sub

    Public Sub OKFrmSolverOptions()
        'Set Solver Parameter
        If frmSolverOptions.Option_SolverDefault.Checked = True Then
            regTest.Solver = "Default"
        ElseIf frmSolverOptions.Option_SolverStandard.Checked = True Then
            regTest.Solver = "Force Standard"
        ElseIf frmSolverOptions.Option_SolverAdvanced.Checked = True Then
            regTest.Solver = "Force Advanced"
        ElseIf frmSolverOptions.Option_SolverMulti.Checked = True Then
            regTest.Solver = "Force Multi-threaded"
        End If

        'Set Process Parameter
        If frmSolverOptions.Option_ProcessDefault.Checked = True Then
            regTest.Process = "Default"
        ElseIf frmSolverOptions.Option_ProcessSame.Checked = True Then
            regTest.Process = "Force Same"
        ElseIf frmSolverOptions.Option_ProcessSeparate.Checked = True Then
            regTest.Process = "Force Separate"
        End If

        'Set 32 Bit Parameter
        If frmSolverOptions.Option_ThirtyTwoDefault.Checked = True Then
            regTest.ThirtyTwoBit = "Default"
        ElseIf frmSolverOptions.Option_ForceThirtyTwo.Checked = True Then
            regTest.ThirtyTwoBit = "Force 32 Bit"
        ElseIf frmSolverOptions.Option_ForceSixtyFour.Checked = True Then
            regTest.ThirtyTwoBit = "Force Not 32 Bit"
        End If

        'Set File Delete Parameter
        If frmSolverOptions.CheckBox_Delete.CheckState = CheckState.Checked Then
            regTest.DeleteAnalysisFiles = "Yes"
        Else : regTest.DeleteAnalysisFiles = "No"
        End If

        formControls.btnClose(frmSolverOptions)
    End Sub

    Public Sub OKFrmTestSuiteSetup()
        Dim closeForm As Boolean
        'TO DO:
        '   2. Check if existing models are to be used, and confirm that at least one model of the proper extension exists at the location
        '   3. Check if Destination Directory Exists. If it does not, prompt the user
        '       3a. Ask the user whether they desire one to be created (might be trickier if the path beyond destination folder is different. How should this be handled?

        closeForm = True    'If an incorrect entry warning appears, form will not close after warning is closed

        '======Update values in sheet or XML here
        '~General

        regTest.test_description = frmTestSuiteSetup.TextBox_TestName.Text
        regTest.test_id = frmTestSuiteSetup.TextBox_TestID.Text

        'Program List: Verify that an item was selected, and if so, assign value to Excel sheet
        If frmTestSuiteSetup.ComboBox_Programs.SelectedIndex <> -1 Then
            regTest.program_name = frmTestSuiteSetup.ComboBox_Programs.Items(frmTestSuiteSetup.ComboBox_Programs.SelectedIndex)
        Else
            MsgBox("No program was selected!")
            closeForm = False
        End If

        '~Models Setup

        '~~Model Paths
        '~~~Source
        regTest.models_database_directory = frmTestSuiteSetup.TextBox_Source.Text
        '~~~~Path
        If frmTestSuiteSetup.Option_SourceAbsolute.Checked = True Then
            regTest.models_database_directory_attrib = "absolute"
        Else
            regTest.models_database_directory_attrib = "relative"
        End If

        '~~~Source
        regTest.models_run_directory = frmTestSuiteSetup.TextBox_Destination.Text
        '~~~~Path
        If frmTestSuiteSetup.Option_DestinationAbsolute.Checked = True Then
            regTest.models_run_directory_attrib = "absolute"
        Else
            regTest.models_run_directory_attrib = "relative"
        End If

        '~~Models Used
        If frmTestSuiteSetup.Option_CopyModels.Checked = True Then
            regTest.copy_models_AttribRun = "yes"
        Else
            regTest.copy_models_AttribRun = "no"
        End If

        If frmTestSuiteSetup.CheckBox_CopyModelsFlat.CheckState = CheckState.Checked Then
            regTest.copy_models_flat_attribRun = "yes"
        Else
            regTest.copy_models_flat_attribRun = "no"
        End If

        'Check if other forms are open and need to update values
        If IsUserFormLoaded("frmRunSetup") Then
            If closeForm Then closeFormUpdateOpenForms(frmTestSuiteSetup, frmRunSetup)
        End If

        If closeForm Then formControls.btnClose(frmTestSuiteSetup)
    End Sub
    Public Sub OKFrmTestSuiteSetupAdvanced()
        Dim closeForm As Boolean

        closeForm = True    'If an incorrect entry warning appears, form will not close after warning is closed
        '======Update values in sheet or XML here
        '~XML Operations
        If frmTestSuiteSetupAdvanced.CheckBox_ReUseXMLList.CheckState = CheckState.Checked Then
            regTest.copy_models_flat_attribReuse = "yes"
        Else
            regTest.copy_models_flat_attribReuse = "no"
        End If

        If frmTestSuiteSetupAdvanced.CheckBox_ListXML.CheckState = CheckState.Checked Then
            regTest.write_xml_files_list_attrib = "yes"
        Else
            regTest.write_xml_files_list_attrib = "no"
        End If

        '~Dry Run Options
        If frmTestSuiteSetupAdvanced.CheckBox_CopyModelsFlat.CheckState = CheckState.Checked Then
            regTest.copy_models_flat_attribDryRun = "yes"
        Else
            regTest.copy_models_flat_attribDryRun = "no"
        End If

        If frmTestSuiteSetupAdvanced.CheckBox_EnableDryRun.CheckState = CheckState.Checked Then
            regTest.test_to_run_attrib = "yes"
        Else
            regTest.test_to_run_attrib = "no"
        End If

        '~Test to Run: Verify that an item was selected, and if so, assign value to Excel sheet
        If frmTestSuiteSetupAdvanced.ComboBox_TestToRun.SelectedIndex <> -1 Then
            regTest.test_to_run = frmTestSuiteSetupAdvanced.ComboBox_TestToRun.Items(frmTestSuiteSetupAdvanced.ComboBox_TestToRun.SelectedIndex)
        Else
            MsgBox("No program was selected!")
            closeForm = False
        End If

        If closeForm Then formControls.btnClose(frmTestSuiteSetupAdvanced)
    End Sub
    Public Sub OKFrmTestSuiteSetupDistributed()
        Dim closeForm As Boolean

        closeForm = True    'If an incorrect entry warning appears, form will not close after warning is closed

        '======Update values in sheet or XML here
        regTest.server_base_URL = frmTestSuiteSetupDistributed.TextBox_ServerBaseURL.Text
        'Models Source List: Verify that an item was selected, and if so, assign value to Excel sheet
        If frmTestSuiteSetupDistributed.ComboBox_ModelsSource.SelectedIndex <> -1 Then
            regTest.models_source = frmTestSuiteSetupDistributed.ComboBox_ModelsSource.Items(frmTestSuiteSetupDistributed.ComboBox_ModelsSource.SelectedIndex)
        Else
            MsgBox("No program was selected!")
            closeForm = False
        End If

        If closeForm Then formControls.btnClose(frmTestSuiteSetupDistributed)
    End Sub

    Public Sub OKFrmVerificationControl()
        'Need to finish
        formControls.btnClose(frmVerificationControl)
    End Sub

    Public Sub OKFrmVerificationReport()
        'Need to finish
        formControls.btnClose(frmVerificationReport)
    End Sub

#End Region

#Region "Buttons"
    Public Sub frmTestSuiteSetup_SelectModels()
        '"Excel" 'Sheets("FilterModelList").Activate()
        frmTestSuiteSetup.btnOK.PerformClick()
    End Sub

    Public Sub frmRunSetupEmailList_btnModify_Click()
        Dim stopAdd As Boolean

        stopAdd = False

        'Check valid e-mail address ("@" present, "." present after "@"
        If Not checkValidEmail(frmRunSetupEmailList.TextBox_Input) Then
            stopAdd = True
        Else
            'Check existing e-mail
            stopAdd = checkEntryExistsListBox(frmRunSetupEmailList.ListBox_EmailList, frmRunSetupEmailList.TextBox_Input)
        End If
        'If passed, add value
        If Not stopAdd Then frmRunSetupEmailList.ListBox_EmailList.Items(frmRunSetupEmailList.ListBox_EmailList.SelectedIndex) = frmRunSetupEmailList.TextBox_Input.Text
    End Sub
    Public Sub frmRunSetupEmailList_btnAdd_Click()
        Dim stopAdd As Boolean

        stopAdd = False

        'Check valid e-mail address ("@" present, "." present after "@"
        If Not checkValidEmail(frmRunSetupEmailList.TextBox_Input) Then
            stopAdd = True
        Else
            'Check existing e-mail
            stopAdd = checkEntryExistsListBox(frmRunSetupEmailList.ListBox_EmailList, frmRunSetupEmailList.TextBox_Input)
        End If

        'If passed, add value
        If Not stopAdd Then frmRunSetupEmailList.ListBox_EmailList.Items.Add(frmRunSetupEmailList.TextBox_Input.Text)
    End Sub
    Public Sub frmRunSetupEmailList_btnDelete_Click()
        frmRunSetupEmailList.ListBox_EmailList.Items.Remove(frmRunSetupEmailList.ListBox_EmailList.SelectedItem)
    End Sub

    Public Sub frmVerificationReport_btnCompleteReport()
        Dim iRet As Integer

        iRet = MsgBox("Creating Excel Report", vbOKCancel)
        If iRet = vbCancel Then
            GoTo Cancel
        Else
            'Call CompleteReport()     'Creates report of run in Excel
            If frmVerificationReport.CheckBox_PrepareReport.CheckState = CheckState.Checked Then
                iRet = MsgBox("Copying Non-Matching Examples to Report Folder", vbOKCancel)
                If iRet = vbCancel Then
                    GoTo Cancel
                Else
                    Call Copy_Folders_NoMatch()
                End If
                If frmVerificationReport.CheckBox_DeleteFiles.CheckState = CheckState.Checked Then
                    iRet = MsgBox("Deleting Analysis Files", vbOKCancel)
                    If iRet = vbCancel Then
                        GoTo Cancel
                    Else
                        Call Del_AnalysisFiles()
                    End If
                End If
                If frmVerificationReport.CheckBox_ZipAll.CheckState = CheckState.Checked Then
                    iRet = MsgBox("Zipping Report Folder", vbOKCancel)
                    If iRet = vbCancel Then
                        GoTo Cancel
                    Else
                        Call Zip_All_Files_in_Folder()
                    End If
                End If
                If frmVerificationReport.CheckBox_ZipIndividual.CheckState = CheckState.Checked Then
                    iRet = MsgBox("Zipping Non-Matching Examples", vbOKCancel)
                    If iRet = vbCancel Then
                        GoTo Cancel
                    Else
                        'Call DemoProgress1()    'Calls progress bar
                        '                    Call Zip_All_SubFolders_in_Folder
                    End If
                End If
            End If
        End If

Cancel:
        btnClose(frmVerificationReport)       'closes form
    End Sub
#End Region

#Region "Radio Buttons"
    Public Sub frmRunSetup_Option_Local_CheckedChanged()
        frmRunSetup.CheckBox_DistTestStart.Enabled = False
        frmRunSetup.CheckBox_DistTestStart.CheckState = CheckState.Unchecked
        frmRunSetup.CheckBox_DistTestJoin.Enabled = False
        frmRunSetup.CheckBox_DistTestJoin.CheckState = CheckState.Unchecked
    End Sub
    Public Sub frmRunSetup_Option_Distributed_CheckedChanged()
        frmRunSetup.CheckBox_DistTestStart.Enabled = True
        frmRunSetup.CheckBox_DistTestJoin.Enabled = True
    End Sub

    Public Sub frmTestSuiteSetup_Option_CopyModels_CheckedChanged()
        frmTestSuiteSetup.CheckBox_CopyModelsFlat.Enabled = True
    End Sub
    Public Sub frmTestSuiteSetup_Option_CopyModelsNo_CheckedChanged()
        frmTestSuiteSetup.CheckBox_CopyModelsFlat.Enabled = False
    End Sub
#End Region

#Region "Check Boxes"
    Public Sub frmRunSetupAdvanced_CheckBox_Email_CheckedChanged()
        If frmRunSetupAdvanced.CheckBox_Email.CheckState = CheckState.Checked Then
            frmRunSetupAdvanced.btnEmailList.Enabled = True
        Else
            frmRunSetupAdvanced.btnEmailList.Enabled = False
        End If
    End Sub

    Public Sub frmVerificationReport_CheckBox_PrepareReport_CheckedChanged()
        'This makes checkboxes conditionally active if the first one is selected
        Select Case frmVerificationReport.CheckBox_PrepareReport.CheckState
            Case CheckState.Checked
                frmVerificationReport.CheckBox_DeleteFiles.Enabled = True
                frmVerificationReport.CheckBox_ZipAll.Enabled = True
                frmVerificationReport.CheckBox_ZipIndividual.Enabled = True
            Case CheckState.Unchecked
                frmVerificationReport.CheckBox_DeleteFiles.Enabled = False : frmVerificationReport.CheckBox_DeleteFiles.CheckState = CheckState.Unchecked
                frmVerificationReport.CheckBox_ZipAll.Enabled = False : frmVerificationReport.CheckBox_ZipAll.CheckState = CheckState.Unchecked
                frmVerificationReport.CheckBox_ZipIndividual.Enabled = False : frmVerificationReport.CheckBox_ZipIndividual.CheckState = CheckState.Unchecked
        End Select
    End Sub
#End Region

#Region "List Boxes"
    Public Sub frmRunSetupEmailList_ListBox_EmailList_SelectedIndexChanged()
        frmRunSetupEmailList.TextBox_Input.Text = frmRunSetupEmailList.ListBox_EmailList.Text
    End Sub
#End Region

#Region "Text Boxes"
    Public Sub frmRunSetupAdvanced_TextBox_TimeMultiplier_TextChanged()
        Call CheckFieldNumeric(frmRunSetupAdvanced.TextBox_TimeMultiplier)
    End Sub
    Public Sub frmRunSetupAdvanced_TextBox_MaxRuntime_TextChanged()
        Call CheckFieldNumeric(frmRunSetupAdvanced.TextBox_MaxRuntime)
    End Sub
    Public Sub frmRunSetupAdvanced_TextBox_DecimalDigits_TextChanged()
        Call CheckFieldNumeric(frmRunSetupAdvanced.TextBox_DecimalDigits)
    End Sub
#End Region

#Region "Subs In Progress"
    'frmVerifiactionReport
    'Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    'Sub DemoProgress1()
    '    '
    '    ' Progress Bar
    '    '
    '    Dim intIndex As Integer
    '    Dim sngPercent As Single
    '    Dim intMax As Integer
    '    Dim chkPg1Value As Boolean

    '    '    Label1.Caption = "Zipping Non-Matching Examples"

    '    ' ProgressBar1 Initialize
    '    labPg1.Tag = labPg1.Width
    '    labPg1.Width = 0
    '    labPg1v.Caption = ""
    '    labPg1va.Caption = ""

    '    labPg1v.Visible = True
    '    labPg1va.Visible = True


    '    intMax = 100

    '    Dim FileNameZip
    '    Dim PathZip

    '    Dim oApp As Object
    '    Dim i As Long
    '    Dim j As Long
    '    Dim ArItem() As Object
    '    Dim item As String
    '    Dim PathReport As String
    '    Dim FileNameZipFolder As String
    '    Dim Status As Integer

    '    PathReport = Worksheets("Dummy").Range("PathReport").value      'Retrieves variable from storage in Excel
    '    FileNameZipFolder = PathReport & "\Zipped Models"
    '    MkDir FileNameZipFolder

    '    For j = 1 To 2

    '        Select Case j
    '            Case 1
    '                ArItem() = NoMatchArrayPath()
    '                item = ".zip"
    '            Case 2
    '                ArItem() = NoMatchArrayPathRun()
    '                item = "_Run.zip"
    '        End Select

    '        '    For intIndex = 1 To intMax
    '        '        sngPercent = intIndex / intMax
    '        '        ProgressStyle1 sngPercent, True
    '        '        DoEvents
    '        For i = 0 To UBound(ArItem)
    '            PathZip = ArItem(i)
    '            FileNameZip = FileNameZipFolder & "\" & Right(PathZip, 6) & item

    '            'Create empty Zip File
    '            NewZip(FileNameZip)

    '            oApp = CreateObject("Shell.Application")
    '            oApp.Namespace(FileNameZip).CopyHere oApp.Namespace(PathZip).self

    '            'Keep script waiting until Compressing is done
    '            Do Until oApp.Namespace(FileNameZip).items.Count = 1
    '                Application.Wait(Now + TimeValue("0:00:01"))
    '            Loop

    '            sngPercent = (i + 1) / (UBound(ArItem) + 1)
    '            ProgressStyle1(sngPercent, True)

    '        Next i
    '        '
    '        '        Sleep 100
    '        '    Next

    '    Next j

    '    Status = MsgBox("You find the zipfile here: " & FileNameZipFolder, vbMsgBoxSetForeground)

    'End Sub

    'Sub ProgressStyle1(Percent As Single, ShowValue As Boolean)
    '    '
    '    ' Progress Style 1
    '    ' Label Over Label
    '    '
    '    Const PAD = "                         "

    '    labPg1.Width = Int(Percent)

    '    If ShowValue Then
    '        labPg1v.Caption = PAD & Format(Percent, "0%")
    '        labPg1va.Caption = labPg1v.Caption
    '        labPg1va.Width = labPg1.Width
    '    End If
    '    '    labPg1.Width = Int(labPg1.Tag * Percent)


    'End Sub

    '    Private Sub btnCompleteReport_Click(sender As Object, e As EventArgs) Handles btnCompleteReport.Click
    '        Dim iRet As Integer

    '        iRet = MsgBox("Creating Excel Report", vbOKCancel)
    '        If iRet = vbCancel Then
    '            GoTo Cancel
    '        Else
    '            Call CompleteReport()     'Creates report of run in Excel
    '            If CheckBox_PrepareReport.CheckState = CheckState.Checked Then
    '                iRet = MsgBox("Copying Non-Matching Examples to Report Folder", vbOKCancel)
    '                If iRet = vbCancel Then
    '                    GoTo Cancel
    '                Else
    '                    Call Copy_Folders_NoMatch()
    '                End If
    '                If CheckBox_DeleteFiles.CheckState = CheckState.Checked Then
    '                    iRet = MsgBox("Deleting Analysis Files", vbOKCancel)
    '                    If iRet = vbCancel Then
    '                        GoTo Cancel
    '                    Else
    '                        Call Del_AnalysisFiles()
    '                    End If
    '                End If
    '                If CheckBox_ZipAll.CheckState = CheckState.Checked Then
    '                    iRet = MsgBox("Zipping Report Folder", vbOKCancel)
    '                    If iRet = vbCancel Then
    '                        GoTo Cancel
    '                    Else
    '                        Call Zip_All_Files_in_Folder()
    '                    End If
    '                End If
    '                If CheckBox_ZipIndividual.CheckState = CheckState.Checked Then
    '                    iRet = MsgBox("Zipping Non-Matching Examples", vbOKCancel)
    '                    If iRet = vbCancel Then
    '                        GoTo Cancel
    '                    Else
    '                        Call DemoProgress1()
    '                        '                    Call Zip_All_SubFolders_in_Folder
    '                    End If
    '                End If
    '            End If
    '        End If

    'Cancel:
    '        Close()       'closes form
    '    End Sub
#End Region
End Module
