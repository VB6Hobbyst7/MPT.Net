Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.Reporting

Imports CSiTester.cRegTest
Imports CSiTester.cSettings
Imports CSiTester.cCsiTester
Imports CSiTester.cPathSettings


''' <summary>
''' Main class for operating the CSiTester GUI, limited to features specific to the Internal version of the program.
''' This class coordinates the actions of RegTest, and all of the other classes in the program. If an action or property does not belong to a form or a more specific class, it belongs here.
''' </summary>
''' <remarks></remarks>
Public Class cCSiTesterInt
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

    Friend Const CLASS_STRING As String = "cCSiTesterInt"

#Region "Initialization: Models Destination"
    ''' <summary>
    ''' Initializes a new destination directory by copying over the relevant files, generating directories, and refreshing classes.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="preserveCurrentSettings">Optional: True: Copies settings files from last destination directory to new destination directory. False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="overWriteExisting">Optional: True: Overwrites existing files. False is default.</param>
    ''' <param name="resetSettings">Optional: True: Existing settings files will be overwritten with newly copied source files. All files and folders within the destination directory will be deleted. False: Existing settings files will be preserved.</param>    
    ''' <remarks></remarks>
    Friend Sub InitializeRunningDirectory(ByVal pathDestination As String, Optional ByVal preserveCurrentSettings As Boolean = True, _
                                          Optional ByVal overWriteExisting As Boolean = False, Optional ByVal resetSettings As Boolean = False)
        Dim modelsRunDirectoryNew As String = ""
        Dim modelsOutputPath As String = ""

        Try
            If myRegTest.autoFolders Then
                modelsRunDirectoryNew = pathDestination & "\" & myRegTest.test_id
                modelsOutputPath = modelsRunDirectoryNew & "\" & myRegTest.test_id & DIR_NAME_RESULTS_DESTINATION_SUFFIX
            Else
                modelsRunDirectoryNew = pathDestination & "\" & DIR_NAME_MODELS_DESTINATION 'GetSuffix(regTest.models_database_directory, "\")
                modelsOutputPath = pathDestination & "\" & DIR_NAME_RESULTS_DESTINATION
            End If

            If resetSettings Then   'Delete all files and folders within the destination folder
                If myRegTest.autoFolders Then
                    'TODO: Should anything be done here?
                Else
                    DeleteAllFilesFolders(pathDestination, False)

                    'Set initialization status
                    myCsiTester.initializeModelDestinationFolder = True
                    myCsiTester.SyncDestinationInitializationStatus(True)
                    myRegTest.SetFolderInitialization()
                End If
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Try
            If ReadableWriteableDeletableDirectory(pathDestination) Then
                'Create models run and output directories
                If Not IO.Directory.Exists(modelsRunDirectoryNew) Then ComponentCreateDirectory(modelsRunDirectoryNew)
                If Not IO.Directory.Exists(modelsOutputPath) Then ComponentCreateDirectory(modelsOutputPath)

            Else
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                            PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                            TITLE_MODEL_DESTINATION_INVALID))
            End If

            'TODO:
            'Note: the function below fails in debugger since source control is write-protected. Might not be a necessary check in the Int. version ...
            'If ReadableWriteableDeletableDirectory(myCsiTester.testerSourceDir) Then
            If resetSettings Then
                'Note: testerSourceDir instead of destination as files used are local to program, and refreshed from seed files

                '==== CSiTesterSettings.xml ====
                InitializeCSiTesterSettings(myCsiTester.testerSourceDir)

                '==== RegTest.xml ====
                InitializeRegTest(myCsiTester.testerSourceDir, modelsRunDirectoryNew, modelsOutputPath, True)
                'Else
                '    InitializeRegTest(pathDestination, modelsRunDirectoryNew, modelsOutputPath, False)
            End If
            'Else
            'MessageBox.Show(promptModelDestinationNoPermission, titleModelDestinationNotValid, MessageBoxButton.OK, MessageBoxImage.Exclamation)
            'End If
        Catch ex As Exception
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                        PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                        TITLE_MODEL_DESTINATION_INVALID))
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Copies relevant CSiTesterSettings file and class settings. Used for resetting current destination to default settings.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeCSiTesterSettings(ByVal pathDestination As String)
        Dim pathSettingsDestination As String = pathDestination & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS
        Dim mySettingsSource As String
        Dim csiTesterLevel As eCSiTesterLevel = testerSettings.csiTesterlevel
        Dim programName As eCSiProgram = testerSettings.programName
        Dim programPath As String = testerSettings.programPathStub

        'Copy seed Settings File over existing one
        mySettingsSource = testerSettings.seedDirectory.path & "\" & FILENAME_CSITESTER_SETTINGS
        CopyFile(mySettingsSource, pathSettingsDestination, True, False, , True, True)

        'Generate a settings class based on the Settings file located at another location
        testerSettings = New cSettings(pathSettingsDestination)

        'Apply initialization settings from current CSiTesterSettings class
        With testerSettings
            'Overwrite certain settings for session continuity
            .csiTesterlevel = csiTesterLevel
            .programName = programName
            .programPathStub = programPath
        End With

        'Save newly copied XML file with updated values
        If Not FileInUseAction(pathSettingsDestination) Then testerSettings.Save(True)
    End Sub

    ''' <summary>
    ''' Copies relevant RegTest file and class settings. Used for resetting current destination to default settings.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="modelsRunDirectoryNew">Directory where models will be copied to and run from.</param>
    ''' <param name="modelsOutputPath">Directory where regTest will place output files from runs.</param>
    ''' <param name="resetSettings">Optional: True: Existing regTest files will be overwritten with a newly copied source file. False: regTest will only be updated, but not reset to defaults.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeRegTest(ByVal pathDestination As String, ByVal modelsRunDirectoryNew As String, ByVal modelsOutputPath As String, ByVal resetSettings As Boolean)
        Dim myRegTestSource As String
        Dim myRegTestDestination As String = pathDestination & cPathRegTest.pathRelativeToProgram & FILENAME_REGTEST_CONTROL
        Dim pathControl As String = myRegTest.control_xml_file.path
        Dim programPath As String = myRegTest.program_file.path
        Dim modelsSource As String = myRegTest.models_database_directory.path

        If resetSettings Then
            myRegTestSource = testerSettings.seedDirectory.path & "\" & FILENAME_REGTEST_CONTROL
            CopyFile(myRegTestSource, myRegTestDestination, True, False, , True, True)

            'Generate regTest class
            myRegTest = New cRegTest(eCSiInstallMethod.NoIni, myRegTest.regTestFile.path, myRegTestDestination, True)
        End If

        'Apply initialization settings from current regTest class
        With myRegTest
            'Sync program name to maintain continuity in session
            .program_name = testerSettings.programName
            .program_file.SetProperties(programPath)
            .SetVersionBuild()

            .control_xml_file.SetProperties(pathControl)
            .models_database_directory.SetProperties(modelsSource)

            'Update properties
            .models_run_directory.SetProperties(modelsRunDirectoryNew)
            .output_directory.SetProperties(modelsOutputPath)

            .SetFolderInitialization()
        End With

        'Save newly copied XML file with updated values
        If Not FileInUseAction(myRegTestDestination) Then
            If resetSettings Then
                myRegTest.SaveRegTest(True)
            Else
                myRegTest.SaveRegTest()
            End If
        End If
    End Sub

#End Region

    '=== Path & File Operations
#Region "Methods: File Deleting"
    ''' <summary>
    ''' Deletes all files in the models destination folder.
    ''' </summary>
    ''' <param name="testerDestinationDir">Path to the tester destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub ClearDestinationFolder(ByVal testerDestinationDir As String)
        'TODO: Status bar here in the future
        Dim cursorWait As New cCursorWait

        Dim message As String = ""
        Dim messageType As eMessageType = eMessageType.None

        If IO.Directory.Exists(testerDestinationDir) Then

            If myCsiTester.ValidateDestinationDirectory(True) Then                              'Check that directory can have files written, copied, and deleted there
                If myRegTest.autoFolders Then 'Delete folders containing the models and the results of the last run
                    If IO.Directory.Exists(myRegTest.models_run_directory.path) Then
                        DeleteAllFilesFolders(myRegTest.models_run_directory.path, True, , True)
                    End If
                    If IO.Directory.Exists(myRegTest.output_directory.path) Then
                        DeleteAllFilesFolders(myRegTest.output_directory.path, True, , True)
                    End If
                Else                        'Delete all contents within folders containing the models and the results & Create empty folders for initialization
                    If IO.Directory.Exists(myRegTest.models_run_directory.path) Then
                        DeleteAllFilesFolders(myRegTest.models_run_directory.path, False, , True)
                    End If
                    If IO.Directory.Exists(myRegTest.output_directory.path) Then
                        DeleteAllFilesFolders(myRegTest.output_directory.path, False, , True)
                    End If
                End If
            Else
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                            PROMPT_CLEAR_NOT_WRITEABLE,
                                                            TITLE_CLEAR))
                Exit Sub
            End If

            'Set Initialization Status
            myCsiTester.initializeModelDestinationFolder = True
            myCsiTester.SyncDestinationInitializationStatus(True)
            myRegTest.SetFolderInitialization()
            myCsiTester.InitializeRunningDirectory(testerDestinationDir, False, True)

            'Saves updated values to regTest.xml.
            If Not FileInUseAction(myRegTest.xmlFile.path) Then myRegTest.SaveFolderInitializationSettings()

            'Clear saved state of compared results
            myCsiTester.exampleComparedCollection = Nothing
            testerSettings.examplesRanSaved.Clear()
            testerSettings.examplesComparedSaved.Clear()

            'Saves updated values to CSiTesterSettings.xml
            If Not FileInUseAction(testerSettings.xmlFile.path) Then myCsiTester.SaveSettings()

            message = PROMPT_CLEAR_SUCCESS
        Else
            messageType = eMessageType.Exclamation
            message = PROMPT_CLEAR_DIRECTORY_NOT_EXIST
        End If
        RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, messageType),
                                                    message,
                                                    TITLE_CLEAR))

        cursorWait.EndCursor()
    End Sub
#End Region


#Region "Test Components"


    ''' <summary>
    ''' Validates that the appropriate destination directories have been emptied or deleted.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtClearDestinationFolder(ByVal className As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)

        With e2eTester
            If myRegTest.autoFolders Then 'Delete folders containing the models and the results of the last run
                'Directories have been removed
                .expectation = "Models run directory should no longer exist"
                .resultActual = CStr(IO.Directory.Exists(myRegTest.models_run_directory.path))
                .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.models_run_directory) "
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Models results directory should no longer exist"
                .resultActual = CStr(IO.Directory.Exists(myRegTest.output_directory.path))
                .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.output_directory_path)"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

            Else                        'Delete all contents within folders containing the models and the results & Create empty folders for initialization
                'Directories have been cleared
                .expectation = "Models run directory should be empty of files"
                .resultActual = CStr(DirContainsFiles(myRegTest.models_run_directory.path))
                .resultActualCall = classIdentifier & "DirContainsFiles(regTest.models_run_directory)"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Models run directory should be empty of folders"
                .resultActual = CStr(DirContainsDirs(myRegTest.models_run_directory.path))
                .resultActualCall = classIdentifier & "DirContainsDirs(regTest.models_run_directory)"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Models results directory should be empty of files"
                .resultActual = CStr(DirContainsFiles(myRegTest.output_directory.path))
                .resultActualCall = classIdentifier & "DirContainsFiles(regTest.output_directory_path)"
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass

                .expectation = "Models results directory should be empty of files"
                .resultActual = CStr(DirContainsDirs(myRegTest.output_directory.path))
                .resultActualCall = classIdentifier & "DirContainsDirs(regTest.output_directory_path) "
                .resultExpected = "False"
                If Not .RunSubTest() Then Return subTestPass
            End If
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the appropriate destination directory files have been reset to their default files.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="oldDateModifiedCSiTesterSettingsXML">Date &amp; time at which the CSiTesterSettings.xml file was last modified before the defaults were restored.</param>
    ''' <param name="oldDateModifiedRegTestXML">Date &amp; time at which the regTest.xml file was last modified before the defaults were restored.</param>
    ''' <param name="testerSourceDir">Source being used as the destination for tester files.</param>
    ''' <param name="modelsOutputPath">Path to the model results directory.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtRestoreDefaults(ByVal className As String, ByVal oldDateModifiedCSiTesterSettingsXML As String, ByVal oldDateModifiedRegTestXML As String, ByVal testerSourceDir As String, ByVal modelsOutputPath As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim newDateModifiedCSiTesterSettingsXML As String = GetFileDateModified(testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS)
        Dim newDateModifiedRegTestXML As String = GetFileDateModified(testerSourceDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL)


        With e2eTester
            'Destination: testerSourceDir & "\" & dirNameCSiTester & "\" & fileNameCSiTesterSettings
            .expectation = "The CSiTesterSettings.xml file should be of an equal or older time stamp"
            .resultActual = CStr(newDateModifiedCSiTesterSettingsXML <= oldDateModifiedCSiTesterSettingsXML)
            .resultActualCall = classIdentifier & "{VldtRestoreDefaults}(newDateModifiedCSiTesterSettingsXML <= oldDateModifiedCSiTesterSettingsXML)"
            .resultExpected = "True"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "The regTest.xml file should be of an equal or older time stamp"
            .resultActual = CStr(newDateModifiedRegTestXML <= oldDateModifiedRegTestXML)
            .resultActualCall = classIdentifier & "{VldtRestoreDefaults}(newDateModifiedRegTestXML <= oldDateModifiedRegTestXML)"
            .resultExpected = "True"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

#End Region

End Class
