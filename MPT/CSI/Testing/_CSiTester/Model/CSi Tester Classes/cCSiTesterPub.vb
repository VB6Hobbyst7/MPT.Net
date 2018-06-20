Option Explicit On
Option Strict On

Imports Scripting
Imports System.Collections.ObjectModel

Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cRegTest
Imports CSiTester.cSettings
Imports CSiTester.cCsiTester
Imports CSiTester.cPathSettings

''' <summary>
''' Main class for operating the CSiTester GUI, limited to features specific to the Published version of the program.
''' This class coordinates the actions of RegTest, and all of the other classes in the program. If an action or property does not belong to a form or a more specific class, it belongs here.
''' </summary>
''' <remarks></remarks>
Public Class cCSiTesterPub
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

    Friend Const CLASS_STRING As String = "cCSiTesterPub"

#Region "Initialization: Models Destination"
    ''' <summary>
    ''' Initializes a new destination directory by copying over the relevant files, generating directories, and refreshing classes.
    ''' </summary>
    ''' <param name="p_pathDestination">Path to the destination directory.</param>
    ''' <param name="p_preserveCurrentSettings">True: Copies settings files from last destination directory to new destination directory. 
    ''' False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="p_overWriteExisting">True: Overwrites existing files. 
    ''' False is default.</param>
    ''' <param name="p_resetSettings">True: Existing settings files will be overwritten with newly copied source files. 
    ''' All files and folders within the destination directory will be deleted. 
    ''' False: Existing settings files will be preserved.</param>    
    ''' <remarks></remarks>
    Friend Sub InitializeRunningDirectory(ByVal p_pathDestination As String,
                                          Optional ByVal p_preserveCurrentSettings As Boolean = True,
                                          Optional ByVal p_overWriteExisting As Boolean = False,
                                          Optional ByVal p_resetSettings As Boolean = False)
        Dim modelsRunDirectoryNew As String = p_pathDestination & "\" & GetSuffix(myRegTest.models_database_directory.path, "\")
        Dim modelsOutputPath As String = p_pathDestination & "\" & DIR_NAME_RESULTS_DESTINATION

        Try
            If p_resetSettings Then   'Delete all files and folders within the destination folder
                DeleteAllFilesFolders(p_pathDestination, False)

                'Set initialization status
                myCsiTester.initializeModelDestinationFolder = True
                myCsiTester.SyncDestinationInitializationStatus(True)
                myRegTest.SetFolderInitialization()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Try
            If ReadableWriteableDeletableDirectory(p_pathDestination) Then
                'Create models run and output directories
                If Not IO.Directory.Exists(modelsOutputPath) Then ComponentCreateDirectory(modelsOutputPath)
                If Not IO.Directory.Exists(modelsRunDirectoryNew) Then ComponentCreateDirectory(modelsRunDirectoryNew)
                If Not IO.Directory.Exists(p_pathDestination & "\" & DIR_NAME_CSITESTER) Then ComponentCreateDirectory(p_pathDestination & "\" & DIR_NAME_CSITESTER)

                '==== CSiTesterSettings.xml ====
                InitializeCSiTesterSettings(p_pathDestination, p_preserveCurrentSettings, p_overWriteExisting, p_resetSettings)

                '==== RegTest.xml ====
                InitializeRegTest(p_pathDestination, modelsRunDirectoryNew, modelsOutputPath, p_preserveCurrentSettings, p_overWriteExisting, p_resetSettings)
            Else
                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                        PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                        TITLE_MODEL_DESTINATION_INVALID))
            End If
        Catch ex As Exception
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Exclamation),
                                                        PROMPT_MODEL_DESTINATION_NO_PERMISSION,
                                                        TITLE_MODEL_DESTINATION_INVALID))
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Copies relevant CSiTesterSettings file and class settings. Used for specifying a new destination or resetting current destination to default settings.
    ''' </summary>
    ''' <param name="pathDestination">Path to the destination directory.</param>
    ''' <param name="preserveCurrentSettings">Optional: True: Copies settings files from last destination directory to new destination directory. False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="overWriteExisting">Optional: True: Overwrites existing files. False is default.</param> 
    ''' <param name="resetSettings">Optional: True: Existing settings files will be overwritten with newly copied source files. False: Existing settings files will be preserved.</param>   
    ''' <remarks></remarks>
    Friend Sub InitializeCSiTesterSettings(ByVal pathDestination As String, Optional ByVal preserveCurrentSettings As Boolean = True, Optional ByVal overWriteExisting As Boolean = False, Optional ByVal resetSettings As Boolean = False)
        Dim pathSettingsDestination As String = pathDestination & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS
        Dim mySettingsSource As String
        Dim csiTesterLevel As eCSiTesterLevel
        Dim programName As eCSiProgram = eCSiProgram.None
        Dim pathIni As String = ""
        Dim pathSettingsSeed As String = ""
        Dim seedPath As String = ""
        Dim filesCopied As Boolean = False

        'Set whether to copy the original installation files, or to copy the files from the currently used directory
        If preserveCurrentSettings Then
            mySettingsSource = testerSettings.xmlFile.path
        Else
            mySettingsSource = testerSettings.xmlFileInstalled.path
        End If

        'Copy new Settings File to location
        If resetSettings Then
            CopyFile(mySettingsSource, pathSettingsDestination, True, False, , True, True)
            filesCopied = True
        ElseIf myCsiTester.initializeModelDestinationFolder And Not IO.File.Exists(pathSettingsDestination) Then
            CopyFile(mySettingsSource, pathSettingsDestination, False, False, , True, True)
            filesCopied = True
        End If


        'Gather initialization settings from current CSiTesterSettings class
        If Not preserveCurrentSettings Then
            With testerSettings
                'Overwrite certain settings that the user might have changed
                csiTesterLevel = .csiTesterlevel
                programName = .programName
                'Set external properties
                pathIni = .iniFile.path
                pathSettingsSeed = .xmlFileInstalled.path
                seedPath = .seedDirectory.path
            End With
        End If

        'Generate a settings class based on the Settings file located at another location
        If filesCopied Then testerSettings = New cSettings(pathSettingsDestination)

        'Apply initialization settings from current CSiTesterSettings class
        If Not preserveCurrentSettings Then
            With testerSettings
                'Overwrite certain settings that the user might have changed
                .csiTesterlevel = csiTesterLevel
                .programName = programName
                'Set external properties
                .iniFile.SetProperties(pathIni)
                .xmlFileInstalled.SetProperties(pathSettingsSeed)
                .seedDirectory.SetProperties(seedPath, p_pathUnknown:=False)
                .csiTesterFile.SetProperties(pathSettingsDestination, p_pathUnknown:=False)
            End With
        End If

        'Save newly copied XML file with updated values
        If Not FileInUseAction(pathSettingsDestination) Then
            If resetSettings Then
                testerSettings.Save(True)          '
            Else
                testerSettings.Save()
            End If
        End If


    End Sub

    ''' <summary>
    ''' Copies relevant RegTest file and class settings. Used for specifying a new desination or resetting current destination to default settings.
    ''' </summary>
    ''' <param name="p_pathDestination">Path to the destination directory.</param>
    ''' <param name="p_modelsRunDirectoryNew">Directory where models will be copied to and run from.</param>
    ''' <param name="p_modelsOutputPath">Directory where regTest will place output files from runs.</param>
    ''' <param name="p_preserveCurrentSettings">True: Copies settings files from last destination directory to new destination directory. 
    ''' False: Copies settings files from installation directory to the specified destination directory, such as when resetting to default settings.</param>
    ''' <param name="p_overWriteExisting">True: Overwrites existing files.</param>    
    ''' <param name="p_resetSettings">True: Existing settings files will be overwritten with newly copied source files. 
    ''' False: Existing settings files will be preserved.</param>
    ''' <param name="p_csiTesterInstallMethod">Whether or not the program is being treated as an installation.</param>
    ''' <remarks></remarks>
    Friend Sub InitializeRegTest(ByVal p_pathDestination As String,
                                 ByVal p_modelsRunDirectoryNew As String,
                                 ByVal p_modelsOutputPath As String,
                                 Optional ByVal p_preserveCurrentSettings As Boolean = True,
                                 Optional ByVal p_overWriteExisting As Boolean = False,
                                 Optional ByVal p_resetSettings As Boolean = False,
                                 Optional p_csiTesterInstallMethod As eCSiInstallMethod = eCSiInstallMethod.NoIni)
        Dim myRegTestSource As String
        Dim myRegTestDestination As String = p_pathDestination & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL
        Dim pathRegTestSeed As String = ""
        Dim pathControlSeed As String = ""
        Dim programPath As String = ""
        Dim modelsDataBaseDirectory As String = ""
        Dim filesCopied As Boolean = False

        'Set whether to copy the original installation files, or to copy the files from the currently used directory
        If p_preserveCurrentSettings Then
            myRegTestSource = myRegTest.xmlFile.path
        Else
            myRegTestSource = myRegTest.xmlInstallationFile.path
        End If

        'Copies entire 'regtest' folder over to the model run location
        'CopyFolder(GetPathDirectoryStub(regTest.xmlInstallationPath), pathDestination & "\CSiTester\regtest", False)
        If p_resetSettings Then
            CopyFolder(GetPathDirectoryStub(myRegTestSource), p_pathDestination & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST, True)

            'If Not IO.Directory.Exists(modelsOutputPath) Then ComponentCreateDirectory(modelsOutputPath, suppressExStates)
            'Copies xml manually. On initialization, sometimes regTest has an error finding this file if copying files to a new location.
            CopyFile(p_pathDestination & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST_TEMPLATE & "\" & FILENAME_REGTEST_LOG, p_modelsOutputPath & "\" & FILENAME_REGTEST_LOG, False)
            filesCopied = True
        ElseIf myCsiTester.initializeModelDestinationFolder Then
            CopyFile(myRegTestDestination, myCsiTester.testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL, True)
            CopyFolder(GetPathDirectoryStub(myRegTestSource), p_pathDestination & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST, True)
            CopyFile(myCsiTester.testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL, myRegTestDestination, True)
            DeleteFile(myCsiTester.testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL)

            'If Not IO.Directory.Exists(modelsOutputPath) Then ComponentCreateDirectory(modelsOutputPath, suppressExStates)
            'Copies xml manually. On initialization, sometimes regTest has an error finding this file if copying files to a new location.
            CopyFile(p_pathDestination & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST_TEMPLATE & "\" & FILENAME_REGTEST_LOG, p_modelsOutputPath & "\" & FILENAME_REGTEST_LOG, False)
            filesCopied = True
        End If


        'Gather initialization settings from current regTest class
        If Not p_preserveCurrentSettings Then
            With myRegTest
                pathRegTestSeed = .xmlInstallationFile.path
                pathControlSeed = .xmlControlInstallationFile.path
                programPath = .programFileInstall.path
                modelsDataBaseDirectory = .models_database_directory.path
            End With
        End If

        'Generate regTest class
        If filesCopied Then myRegTest = New cRegTest(p_csiTesterInstallMethod, myRegTest.regTestFile.path, myRegTestDestination, True)

        'Apply initialization settings from current regTest class
        With myRegTest
            'Sync program name to enforce settings shipped
            .program_name = testerSettings.programName

            'Set other properties
            .models_run_directory.SetProperties(p_modelsRunDirectoryNew)
            .control_xml_file.SetProperties(p_pathDestination & cPathRegTest.pathRelativeToProgram & FILENAME_CONTROL)
            .output_directory.SetProperties(p_modelsOutputPath)
            .SetFolderInitialization()

            If Not p_preserveCurrentSettings Then
                .xmlInstallationFile.SetProperties(pathRegTestSeed)
                .xmlControlInstallationFile.SetProperties(pathControlSeed)
                .program_file.SetProperties(programPath)
                .programFileInstall.SetProperties(programPath)
                .models_database_directory.SetProperties(modelsDataBaseDirectory)
            End If
            .SetVersionBuild()
        End With

        'Save newly copied XML file with updated values
        If Not FileInUseAction(myRegTestDestination) Then
            If p_resetSettings Then
                myRegTest.SaveRegTest(True)
            Else
                myRegTest.SaveRegTest()
            End If
        End If
    End Sub

#End Region

    '=== Path & File Operations
#Region "Methods: Paths"
    ''' <summary>
    ''' Checks to see if the user selects the parent folder of the database folder, and prevents this action.
    ''' </summary>
    ''' <param name="currDir">Current opening directory.</param>
    ''' <param name="pathTemp">Temporary path being checked</param>
    ''' <param name="updateCSiTester">Optional:Parameter that indicates that the path has been changed.</param>
    ''' <param name="canceled">Optional: This is modified by the function to indicate if the function ended by being canceled, or being resolved.</param>
    ''' <remarks></remarks>
    Friend Function ValidateModelDestinationDBParentFolder(ByRef currDir As String, ByRef pathTemp As String, Optional ByRef updateCSiTester As Boolean = False, Optional ByRef canceled As Boolean = False) As Boolean
        Dim validFolder As Boolean = True
        Dim invalidDirectory As String = GetPathDirectoryStub(testerSettings.xmlFileInstalled.path)

        invalidDirectory = Left(invalidDirectory, InStrRev(invalidDirectory, "\") - 1)
        ValidateModelDestinationDBParentFolder = True

        If StringExistInName(pathTemp, invalidDirectory) Then   'The user has selected the parent folder of the database folder
            ValidateModelDestinationDBParentFolder = False
            validFolder = False
            While Not validFolder
                RaiseEvent Messenger(New MessengerEventArgs(PROMPT_DESTINATION_WITHIN_SOURCE_DIRECTORY))
                currDir = BrowseForFolder(PROMPT_MODEL_DESTINATION_BROWSE, , p_showNewFolderButton:=True)
                pathTemp = myCsiTester.pathGlobal
                If String.IsNullOrEmpty(myCsiTester.pathGlobal) Then
                    myCsiTester.SetBrowseModelDestinationSettings(False, updateCSiTester)
                    canceled = True
                    Exit Function
                Else
                    myCsiTester.SetBrowseModelDestinationSettings(True, updateCSiTester)
                End If
                If StringExistInName(pathTemp, invalidDirectory) Then
                    validFolder = False
                Else
                    validFolder = True
                End If
            End While
        End If
    End Function

    ''' <summary>
    ''' Performs the necessary updates when changing the model destination, if the tester is being treated as an installation.
    ''' </summary>
    ''' <param name="testerDestinationDir">Path to the tester destination directory.</param>
    ''' <param name="updateCSiTester">Optional:Parameter that indicates that the path has been changed.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateModelDestination(ByRef testerDestinationDir As String, Optional ByRef updateCSiTester As Boolean = False)
        If Not StringsMatch(testerDestinationDir, myRegTest.models_database_directory.path) Then
            If iniAccessible Then WriteIniFile(testerSettings.iniFile.path, 1, testerDestinationDir) 'Updates *.ini file
            myCsiTester.SetBrowseModelDestinationSettings(True, updateCSiTester)

            testerSettings.examplesRanSaved.Clear()
            testerSettings.examplesComparedSaved.Clear()

            myCsiTester.testerDestinationDir = testerDestinationDir
            myRegTest.models_run_directory.SetProperties(testerDestinationDir & "\" & DIR_NAME_MODELS_DESTINATION)
        End If
    End Sub


#End Region

#Region "Methods: File Deleting"
    '===Deleting Files
    ''' <summary>
    ''' Deletes all files in the models destination folder, leaving intact the "CSiTester' subfolder and all files contained within.
    ''' </summary>
    ''' <param name="p_testerDestinationDir">Path to the tester destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub ClearDestinationFolder(ByVal p_testerDestinationDir As String)
        Dim csiTesterSettingsPath As String = p_testerDestinationDir & "\" & FILENAME_CSITESTER_SETTINGS
        Dim regTestSettingsPath As String = p_testerDestinationDir & cPathRegTest.pathRelativeToProgram & FILENAME_REGTEST_CONTROL

        Dim messageType As eMessageType = eMessageType.None
        Dim message As String = ""

        If IO.Directory.Exists(p_testerDestinationDir) Then

            If myCsiTester.ValidateDestinationDirectory(True) Then                              'Check that directory can have files written, copied, and deleted there
                'Delete the folders containing the models and the results
                If IO.Directory.Exists(myRegTest.models_run_directory.path) Then
                    DeleteAllFilesFolders(myRegTest.models_run_directory.path, True, , True)
                End If
                If IO.Directory.Exists(myRegTest.output_directory.path) Then
                    DeleteAllFilesFolders(myRegTest.output_directory.path, True, , True)
                End If

                'Delete files within CSiTester & regTest folders, while preserving the settings files
                If IO.Directory.Exists(p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST) Then
                    If IO.File.Exists(regTestSettingsPath) Then
                        CopyFile(regTestSettingsPath, p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_REGTEST_CONTROL, False, , , , False)
                        DeleteAllFilesFolders(p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST, False, , True)
                        CopyFile(p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_REGTEST_CONTROL, regTestSettingsPath, False, , , , False)
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

            'Save the regTest.xml if it exists. If not, copy over a new file and save updated values to it.
            If IO.File.Exists(regTestSettingsPath) Then
                If Not FileInUseAction(regTestSettingsPath) Then myRegTest.SaveFolderInitializationSettings()
            Else
                Dim modelsRunDirectoryNew As String = p_testerDestinationDir & "\" & GetSuffix(myRegTest.models_database_directory.path, "\")
                Dim modelsOutputPath As String = p_testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION

                InitializeRegTest(p_testerDestinationDir, modelsRunDirectoryNew, modelsOutputPath, False, False, True)
            End If

            'Clear saved state of compared results
            myCsiTester.exampleComparedCollection = Nothing
            testerSettings.examplesRanSaved.Clear()
            testerSettings.examplesComparedSaved.Clear()

            'Save the CSiTesterSettings.xml if it exists. If not, copy over a new file and save updated values to it.
            If IO.File.Exists(csiTesterSettingsPath) Then
                If Not FileInUseAction(csiTesterSettingsPath) Then testerSettings.SaveFolderInitializationSettings(True)
            Else
                InitializeCSiTesterSettings(p_testerDestinationDir, False, False, True)
            End If

            message = PROMPT_CLEAR_SUCCESS
        Else
            messageType = eMessageType.Exclamation
            message = PROMPT_CLEAR_DIRECTORY_NOT_EXIST
        End If
        RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, messageType),
                                                    message,
                                                    TITLE_CLEAR))
    End Sub
#End Region

#Region "Methods: File Saving"
    ''' <summary>
    ''' Depending on various criteria, the destination folder is set up and new files are copied over and updated, or existing files are saved to.
    ''' </summary>
    ''' <param name="p_updateCSiTester">Parameter that indicates that the path has been changed.</param>
    ''' <param name="p_testerDestinationDir">Path to the tester destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub SaveAndInitializeSettings(ByVal p_updateCSiTester As Boolean,
                                         ByVal p_testerDestinationDir As String)
        If IO.Directory.Exists(p_testerDestinationDir) Then                                                                                                        'Check that directory exists
            If myCsiTester.ValidateDestinationDirectory(True, , p_updateCSiTester) Then                                                                                 'Check that directory can have files written, copied, and deleted there
                If (IO.File.Exists(p_testerDestinationDir & "\" & FILENAME_CSITESTER_SETTINGS) AndAlso
                    IO.File.Exists(p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL)) Then       'Perform action depending on if both settings files exist

                    InitializeRunningDirectory(p_testerDestinationDir, True, False)
                Else
                    myCsiTester.initializeModelDestinationFolder = True
                    myCsiTester.SyncDestinationInitializationStatus(True)
                    myRegTest.SetFolderInitialization()
                    InitializeRunningDirectory(p_testerDestinationDir, False, True)
                End If
            End If
        Else
            RaiseEvent Messenger(New MessengerEventArgs(PROMPT_MODEL_DESTINATION_NOT_EXIST_PUBLISHED))
        End If
    End Sub
#End Region

#Region "Test Components"


    ''' <summary>
    ''' Validates that the appropriate destination directories have been emptied or deleted.
    ''' </summary>
    ''' <param name="className">Name assigned to the class where this function resides.</param>
    ''' <param name="testerDestinationDir">Path to the parent directory of where all of the CSiTester files will be copied. This will always contain a directory of model files that have been run or will be run.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtClearDestinationFolder(ByVal className As String, ByVal testerDestinationDir As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim csiTesterSettingsPath As String = testerDestinationDir & "\" & FILENAME_CSITESTER_SETTINGS
        Dim regTestSettingsPath As String = testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL
        Dim destFilePaths As New List(Of String)

        destFilePaths = ListFilePathsInDirectory(testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST, True)

        With e2eTester
            'Directories have been removed
            .expectation = "Models run directory has been removed"
            .resultActual = CStr(IO.Directory.Exists(myRegTest.models_run_directory.path))
            .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.models_run_directory)"
            .resultExpected = "False"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Models results directory has been removed"
            .resultActual = CStr(IO.Directory.Exists(myRegTest.output_directory.path))
            .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.output_directory_path)"
            .resultExpected = "False"
            If Not .RunSubTest() Then Return subTestPass


            'CSiTesterSettings.xml & regTest.xml are only files in their directory
            .expectation = "There are only two files remaining in the directory and any subdirectories"
            .resultActual = CStr(destFilePaths.Count)
            .resultActualCall = classIdentifier & "destFilePaths.Count"
            .resultExpected = "2"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "regTest.xml file still exists"
            .resultActual = CStr(IO.File.Exists(regTestSettingsPath))
            .resultActualCall = classIdentifier & "IO.File.Exists(regTestSettingsPath)"
            .resultExpected = ""
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "CSiTesterSettings.xml file still exists"
            .resultActual = CStr(IO.File.Exists(csiTesterSettingsPath))
            .resultActualCall = classIdentifier & "IO.File.Exists(csiTesterSettingsPath)"
            .resultExpected = ""
            If Not .RunSubTest() Then Return subTestPass
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
    ''' <param name="oldDateModifiedregTestLog">Date &amp; time at which the regTest log xml file was last modified before the defaults were restored.</param>
    ''' <param name="testerDestinationDir">Directory being used as the destination for tester files.</param>
    ''' <param name="modelsOutputPath">Path to the model results directory.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtRestoreDefaults(ByVal className As String, ByVal oldDateModifiedCSiTesterSettingsXML As String, ByVal oldDateModifiedRegTestXML As String, ByVal oldDateModifiedregTestLog As String, ByVal testerDestinationDir As String, ByVal modelsOutputPath As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(className, CLASS_STRING)
        Dim newDateModifiedCSiTesterSettingsXML As String = GetFileDateModified(testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS)
        Dim newDateModifiedRegTestXML As String = GetFileDateModified(testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST)
        Dim newDateModifiedregTestLog As String = GetFileDateModified(modelsOutputPath & "\" & FILENAME_REGTEST_LOG)


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

            .expectation = "The regTest log xml file should be of an equal or older time stamp"
            .resultActual = CStr(newDateModifiedregTestLog <= oldDateModifiedregTestLog)
            .resultActualCall = classIdentifier & "{VldtRestoreDefaults}(newDateModifiedregTestLog <= oldDateModifiedregTestLog)"
            .resultExpected = "True"
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function

    ''' <summary>
    ''' Validates that the appropriate destination directories have been emptied or deleted.
    ''' </summary>
    ''' <param name="p_className">Name assigned to the class where this function resides.</param>
    ''' <param name="p_testerDestinationDir">Path to the parent directory of where all of the CSiTester files will be copied. This will always contain a directory of model files that have been run or will be run.</param>
    ''' <param name="p_modelsOutputPath">Path to the model results directory.</param>
    ''' <returns>True if validation completed without errors.</returns>
    ''' <remarks></remarks>
    Friend Function VldtResetDestinationFolder(ByVal p_className As String,
                                               ByVal p_testerDestinationDir As String,
                                               ByVal p_modelsOutputPath As String) As Boolean
        Dim subTestPass As Boolean
        Dim classIdentifier As String = e2eTester.SetClassIdentifier(p_className, CLASS_STRING)
        Dim csiTesterSettingsPath As String = p_testerDestinationDir & "\" & FILENAME_CSITESTER_SETTINGS
        Dim regTestSettingsPath As String = p_testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL
        Dim regTestLogPath As String = p_modelsOutputPath & "\" & FILENAME_REGTEST_LOG
        Dim runFilePaths As New List(Of String)
        Dim resultsFilePaths As New List(Of String)

        runFilePaths = ListFilePathsInDirectory(myRegTest.models_run_directory.path, True)
        resultsFilePaths = ListFilePathsInDirectory(myRegTest.output_directory.path, True)

        With e2eTester
            'Removed directories have been recreated & repopulated as expected
            .expectation = "Models run directory exists"
            .resultActual = CStr(IO.Directory.Exists(myRegTest.models_run_directory.path))
            .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.models_run_directory)"
            .resultExpected = "False"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Models run directory is empty"
            .resultActual = CStr(runFilePaths.Count)
            .resultActualCall = classIdentifier & "destFilePaths.Count"
            .resultExpected = "0"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Models results directory exists"
            .resultActual = CStr(IO.Directory.Exists(myRegTest.output_directory.path))
            .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.output_directory_path)"
            .resultExpected = "False"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Models results directory has one file"
            .resultActual = CStr(resultsFilePaths.Count)
            .resultActualCall = classIdentifier & "destFilePaths.Count"
            .resultExpected = "1"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "regTest log xml file still exists"
            .resultActual = CStr(IO.File.Exists(regTestLogPath))
            .resultActualCall = classIdentifier & "IO.File.Exists(regTestLogPath)"
            .resultExpected = ""
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "Destination CSiTester directory exists"
            .resultActual = CStr(IO.Directory.Exists(p_testerDestinationDir & "\" & DIR_NAME_CSITESTER))
            .resultActualCall = classIdentifier & "IO.Directory.Exists(regTest.output_directory_path)"
            .resultExpected = "False"
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "CSiTesterSettings.xml file still exists"
            .resultActual = CStr(IO.File.Exists(csiTesterSettingsPath))
            .resultActualCall = classIdentifier & "IO.File.Exists(csiTesterSettingsPath)"
            .resultExpected = ""
            If Not .RunSubTest() Then Return subTestPass

            .expectation = "regTest.xml file still exists"
            .resultActual = CStr(IO.File.Exists(regTestSettingsPath))
            .resultActualCall = classIdentifier & "IO.File.Exists(regTestSettingsPath)"
            .resultExpected = ""
            If Not .RunSubTest() Then Return subTestPass
        End With

        subTestPass = True

        Return subTestPass
    End Function


#End Region

    
End Class
