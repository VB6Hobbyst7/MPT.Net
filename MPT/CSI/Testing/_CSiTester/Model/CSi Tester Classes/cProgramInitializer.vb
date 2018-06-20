Option Explicit On
Option Strict On

Imports MPT.Enums.EnumLibrary
Imports MPT.Files.FileLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cLibrary

Imports CSiTester.cSettings
Imports CSiTester.cPathSettings
Imports CSiTester.cRegTest

Public Class cProgramInitializer
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Prompts"
    Private Const titleInstallationInvalidRunFolder As String = "Not Run from Installation"
    Private Const promptInstallationInvalidRunFolder As String = "CSiTester must be run from the 'Verification' folder installed with "
    Private Const promptInstallationInvalidRunFolderAction As String = "Program will close."

    Private Const titleTesterPathFailures As String = "Invalid or Default Paths"
    Private Const promptTesterPathFailures As String = "Please review the following paths as they might have set to defaults: "
#End Region

#Region "Variables"
    ''' <summary>
    ''' The tester installation type to start up with.
    ''' </summary>
    ''' <remarks></remarks>
    Private _csiTesterInstallMethod As eCSiInstallMethod

    ''' <summary>
    ''' True: A new *.ini file was created because either the original file was missing or the data was incorrect.
    ''' </summary>
    ''' <remarks></remarks>
    Private _newIniCreated As Boolean

    ''' <summary>
    ''' Path to where the analysis program is installed, and where the analysis program .exe is run from.
    ''' </summary>
    ''' <remarks></remarks>
    Private _pathProgramInstall As String
    ''' <summary>
    ''' Path to the analysis program *.exe file to be used by regTest to run model files.
    ''' </summary>
    ''' <remarks></remarks>
    Private _pathProgramSaved As String
    ''' <summary>
    ''' True: The original path to the analysis *.exe file was incorrect and has recently been updated.
    ''' </summary>
    ''' <remarks></remarks>
    Private _programPathUpdated As Boolean

    ''' <summary>
    ''' Default path to the destination directory of an installed version of CSiTester.
    ''' </summary>
    ''' <remarks></remarks>
    Private _defaultDestinationDir As String = DIR_TESTER_DESTINATION_DIR_INSTALL_DEFAULT
#End Region

#Region "Properties"
    ''' <summary>
    ''' CSiTester level specified in the settings file in the installation directory.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property csiTesterLevel As eCSiTesterLevel

    ''' <summary>
    ''' Path to the parent directory of where all of the CSiTester files will be copied. 
    ''' This will always contain a directory of model files that have been run or will be run.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property testerDestinationDir As String

    ''' <summary>
    ''' Path to the initialization file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathIni As String

    ''' <summary>
    ''' Path to the local CSiTesterSettings.xml file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property localSettingsPath As String

    ''' <summary>
    ''' Name of the program that CSiTester has been shipped with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property programName As eCSiProgram
    ''' <summary>
    ''' Relative path to the program that CSiTester has been shipped with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property relPathProgram As String
    ''' <summary>
    ''' Absolute path to the program directory that contains the program that CSiTester has been shipped with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property absPathProgramDir As String
#End Region

#Region "Initialization"
    Friend Sub New()
        MessengerListenerShared.SubscribeSharedListenerToMessageBox()
        LoggerListenerShared.SubscribeSharedListenerToCSiLogger()

        csiLogger = New cCSiTesterLogger(False)
        csiMessageBox = New MessageBoxLong
    End Sub

#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Main initialization of all major program objects that are present throughout the session.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializationMain()
        Try
            '====== Initialization Routines for Data Only ====== 
            'Initializes startup as if the program is either installed, needing an *ini file, or is a loose file.
            InitializeStartupMethod()

            'Initialize classes for startup only
            LoadSettingsFile(_csiTesterInstallMethod)
            LoadRegTest(_csiTesterInstallMethod)

            If _csiTesterInstallMethod = eCSiInstallMethod.UseIni Then
                'Program should set itself up as if installed, using an *.ini file in the user settings folder.
                LoadCSiTester(, True)
                LoadAsInstallation()
                LoadSettingsFile(_csiTesterInstallMethod, False)
                LoadRegTest(_csiTesterInstallMethod, False)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Final initialization action involving the main program objects that are present throughout the session.
    ''' </summary>
    ''' <param name="p_saveCSiTester">Set. True: Form CSiTester method should be called.</param>
    ''' <remarks></remarks>
    Friend Sub InitializationCompletion(ByRef p_saveCSiTester As Boolean)
        Try
            'Sync tester class destination initialization based on settings class
            myCsiTester.SyncDestinationInitializationStatus(p_setSettings:=False)

            'Update Settings file if program location has changed
            If (testerSettings.programLocationChanged AndAlso
                Not _csiTesterInstallMethod = eCSiInstallMethod.UseIni) Then

                testerSettings.programLocationChanged = False
                p_saveCSiTester = True
            End If

            'Notify user if paths were not converted properly, and reset to defaults
            If _csiTesterInstallMethod = eCSiInstallMethod.NoIni Then ValidatePaths()

            CommandLineTest()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Loads the controlling CSi Tester class. Creates a list of XML models and their paths if a valid one does not exist in the settings file.
    ''' </summary>
    ''' <param name="p_firstLoad">Specifies if this is the first time the tester is being loaded. 
    ''' Else, subsequent calls will refresh the class.</param>
    ''' <param name="p_firstProgramInitialization">Specifies if this is the first time the program is being initialized. 
    ''' Else, subsequent calls will re-initialize the class with additional data.</param>
    ''' <remarks></remarks>
    Friend Sub LoadCSiTester(Optional ByVal p_firstLoad As Boolean = False,
                             Optional ByVal p_firstProgramInitialization As Boolean = False)
        Try
            myCsiTester = New cCsiTester(_csiTesterInstallMethod)

            If Not p_firstProgramInitialization Then
                myCsiTester.Initialize(csiTesterLevel, p_firstLoad, testerDestinationDir)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

#End Region

#Region "Methods: Private"
    '=== General Initialization and Installation-Specific Routines
    ''' <summary>
    ''' Checks whether an unlocking file exists that allows the program to run all operations locally and not use an *.ini file.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeStartupMethod()
        Try
            Dim pathInternalKey As String = pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & INI_INTERNAL_KEY
            If Not IO.File.Exists(pathInternalKey) Then
                _csiTesterInstallMethod = eCSiInstallMethod.UseIni

                'Affects suppression of path conversions. This might not be necessary here
                testerLocationChanged = False
            Else
                _csiTesterInstallMethod = eCSiInstallMethod.NoIni
                ReadKeyIni()
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads the key ini file for non-installation startup to determine if the program is opening up in a new or different location from where it last updated the settings XML files.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadKeyIni()
        Dim dirIni As String

        Try
            'Set the path to the *.ini file.
            dirIni = pathStartup() & "\" & DIR_NAME_CSITESTER

            'Read *.ini file to get destination directory
            If IO.Directory.Exists(dirIni) Then
                If ReadableWriteableDeletableDirectory(dirIni) Then
                    iniAccessible = True
                    pathIni = dirIni & "\" & INI_INTERNAL_KEY

                    If pathStartup() = ReadIniFile(pathIni, 1) Then
                        'Program is starting up the same location as last recorded.
                        testerLocationChanged = False
                    Else
                        'Program is starting up in a different location than last recorded. Update *.ini file
                        WriteIniFile(pathIni, 1, pathStartup)
                        testerLocationChanged = True
                    End If
                Else
                    iniAccessible = False
                End If
            Else
                iniAccessible = False
                'Select Case MessageBox.Show("The following directory does not exist: " & Environment.NewLine & Environment.NewLine & pathIni & Environment.NewLine & Environment.NewLine & "Program will close.", "Directory Does Not Exist", MessageBoxButton.OK)
                '    Case MessageBoxResult.OK : End
                'End Select
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Performs the operations of setting up the program as an installation. This uses an existing *.ini file, or creates a new one, and sets up the program in particular ways, with particular restrictions, assuming that CSiTester resides in an installation folder.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAsInstallation()
        'Program should set itself up as if installed, using an *.ini file in the user settings folder.
        Try
            _programPathUpdated = False
            localSettingsPath = "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS                                                            'Populate initial values that should not be changed & fileNameCSiTesterSettings

            _pathProgramInstall = myRegTest.program_file.path

            '==== Initialization File Operations
            InstallationInitializationOperations()

            '=== Destination Directory Setup
            InstallationDestinationDirectorySetup()

            '=== Analysis Program Setup
            InstallationAnalysisProgramSetup()

            'Set destination intitialization status in myCSiTester & testerSettings
            With myCsiTester
                If _newIniCreated Then
                    .initializeModelDestinationFolder = True
                ElseIf Not iniAccessible Then
                    .initializeModelDestinationFolder = True
                Else
                    .initializeModelDestinationFolder = False
                End If

                .SyncDestinationInitializationStatus(True)
                myRegTest.SetFolderInitialization()
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads the *.ini file, or generates a new one if necessary. Determines if the *.ini file is accessible.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstallationInitializationOperations()
        Dim folderName As String
        Dim dirIni As String

        Try
            'Set the path to the *.ini file.
            folderName = testerSettings.programName & " " & testerSettings.versionDesignation
            dirIni = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & IO.Path.DirectorySeparatorChar & "Computers and Structures" & IO.Path.DirectorySeparatorChar & folderName

            'Read *.ini file to get destination directory
            If IO.Directory.Exists(dirIni) Then
                If ReadableWriteableDeletableDirectory(dirIni) Then
                    iniAccessible = True
                    pathIni = dirIni & "\" & INI_INSTALLATION

                    'Check if initialization file exists, and if not, write a new one with default values
                    InitializeInstallIniFile(pathIni, _defaultDestinationDir, _newIniCreated)
                Else
                    iniAccessible = False
                End If
            Else
                iniAccessible = False
                'Select Case MessageBox.Show("The following directory does not exist: " & Environment.NewLine & Environment.NewLine & pathIni & Environment.NewLine & Environment.NewLine & "Program will close.", "Directory Does Not Exist", MessageBoxButton.OK)
                '    Case MessageBoxResult.OK : End
                'End Select
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Verifies location of destination directory and takes corrective action if not valid.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstallationDestinationDirectorySetup()
        Try
            Dim directoryExist As Boolean = True

            'Read .ini file to get destination directory
            If iniAccessible Then
                testerSettings.iniFile.SetProperties(pathIni)
                testerDestinationDir = ReadIniFile(pathIni, 1)
            Else
                testerDestinationDir = _defaultDestinationDir
                WriteIniFile(pathIni, 1, testerDestinationDir, _newIniCreated) 'Updates *.ini file
            End If

            myCsiTester.testerDestinationDir = testerDestinationDir
            myRegTest.models_run_directory.SetProperties(testerDestinationDir & "\" & DIR_NAME_MODELS_DESTINATION)

            'Below is currently not applicable as long as it is enforced that the level is 'Published' if being treated as an installation.
            ''Verify location of destination directory and take corrective action if not valid
            'If Not IO.Directory.Exists(testerDestinationDir) Then
            '    directoryExist = False
            '    If Not testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then           'Take corrective action if not a published version
            '        While Not directoryExist
            '            myCsiTester.BrowseModelDestination(, False)
            '            testerDestinationDir = regTest.models_run_directory
            '            If IO.Directory.Exists(testerDestinationDir) And iniAccessible Then
            '                WriteIniFile(pathIni, 1, testerDestinationDir, newIniCreated)                                                    'Updates *.ini file
            '                directoryExist = True
            '            Else
            '                End
            '            End If
            '        End While
            '    ElseIf testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then           'Initialize with installation files if a published version
            '        testerDestinationDir = defaultDestinationDir
            '        If iniAccessible Then WriteIniFile(pathIni, 1, testerDestinationDir, newIniCreated) 'Updates *.ini file
            '    End If
            'End If

            'myCsiTester.testerDestinationDir = testerDestinationDir
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' The relative path of the program is assumed and checked. Corrective actions are provided if this fails.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InstallationAnalysisProgramSetup()
        Try
            'Verify location of installated testing program and accompanying CSiTester files
            relPathProgram = "..\" & programName & ".exe"                                                                       'Assumed location of program, to check if the program being started is in the installation directory
            _pathProgramInstall = relPathProgram
            AbsolutePath(_pathProgramInstall)

            'Check if current CSiTester program is being run from the installation directory

            If Not IO.File.Exists(_pathProgramInstall) Then 'The local regTest.xml read for startup is also not in the installation directory and can be updated. 
                'It will have the program location if a proper location had been set up on a prior run for the given file configurations.

                If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                    RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Stop),
                                                                promptInstallationInvalidRunFolder & programName & "." & Environment.NewLine & Environment.NewLine &
                                                                promptInstallationInvalidRunFolderAction,
                                                                titleInstallationInvalidRunFolder))
                    End
                Else    'Take corrective action if not a published version

                    'Check the local regTest file for the program directory
                    _pathProgramInstall = myRegTest.program_file.path

                    'If the CSiTester is not in the installation directory and a valid path for the installation directory _
                    'has not been saved in the local regTest.xml, an updated path is requested in order to establish the installation location
                    If Not IO.File.Exists(_pathProgramInstall) Then
                        ExceptionProgramSource(GetEnumDescription(programName), _pathProgramInstall, False)

                        myRegTest.program_file.SetProperties(_pathProgramInstall)
                        myRegTest.programFileInstall.SetProperties(_pathProgramInstall)

                        _pathProgramSaved = myRegTest.program_file.path
                        myRegTest.SaveRegTest()
                        _programPathUpdated = True
                    End If
                End If
            End If

            absPathProgramDir = GetPathDirectoryStub(_pathProgramInstall)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Checks absolute paths for certain paths used in the tester. If the paths are invalid, or have been switched to defaults, the user is notified.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidatePaths()
        Dim pathFailed As Boolean = False
        Dim pathFailedPrompt As String = ""

        With myRegTest
            If (Not IO.File.Exists(.program_file.path) OrElse
                StringsMatch(.program_file.path, DIR_TESTER_PROGRAM_PATH_DEFAULT)) Then

                pathFailedPrompt = pathFailedPrompt & "Program: " & .program_file.path & Environment.NewLine & Environment.NewLine
                pathFailed = True
            End If
            If (Not IO.Directory.Exists(.models_database_directory.path) OrElse
                StringsMatch(.models_database_directory.path, DIR_TESTER_SOURCE_DIR_DEFAULT)) Then

                pathFailedPrompt = pathFailedPrompt & "Source: " & .models_database_directory.path & Environment.NewLine & Environment.NewLine
                pathFailed = True
            End If
        End With
        If (Not IO.Directory.Exists(testerSettings.testerDestination.path) OrElse
            StringsMatch(testerSettings.testerDestination.path, DIR_TESTER_DESTINATION_DIR_DEFAULT)) Then

            pathFailedPrompt = pathFailedPrompt & "Destination: " & testerSettings.testerDestination.path & Environment.NewLine & Environment.NewLine
            pathFailed = True
        End If

        If pathFailed Then
            RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Asterisk),
                                                        promptTesterPathFailures & Environment.NewLine & Environment.NewLine &
                                                        pathFailedPrompt,
                                                        titleTesterPathFailures))
        End If
    End Sub

    '=== Other Loadings
    ''' <summary>
    ''' Loads the XML settings data, which is needed for setting up the rest of the suite, including the published/development views.
    ''' </summary>
    ''' <param name="p_installMethod">Specifies whether an initialization file is being used for the CSiTester program.</param>
    ''' <param name="p_firstLoad">Optional: If false, additional and different initialization steps are taken. 'False' is coupled with installMethod = UseIni.</param>
    ''' <remarks></remarks>
    Private Sub LoadSettingsFile(ByVal p_installMethod As eCSiInstallMethod,
                                 Optional ByVal p_firstLoad As Boolean = True)
        Try
            ' Check if program is published or internal
            If p_firstLoad Then
                ' Generate an unfiltered settings class from the local file
                testerSettings = New cSettings()
                With testerSettings
                    ' If setting is specified as not published, allow Catch/Try exception prompts to be visible
                    ' If Not .csiTesterlevel = eCSiTesterLevel.Published Then suppressExStates = False
                    csiTesterLevel = .csiTesterlevel

                    If p_installMethod = eCSiInstallMethod.UseIni Then
                        programName = .programName
                        .EnforceSetProgram()
                        .programLocationChanged = False

                        ' Set settings file destination to that stored in the *.ini file, now represented in the form. 
                        ' This will propagate into other parts of the program.
                        .testerDestination.SetProperties(testerDestinationDir, DIR_TESTER_DESTINATION_DIR_DEFAULT)
                    Else
                        ' Set form destination to that stored in the settings file. 
                        ' This will propagate into other parts of the program.
                        testerDestinationDir = .testerDestination.path
                        .xmlFileInstalled.SetProperties(.xmlFile.path)
                    End If
                End With
            Else
                Dim pathSettingsSeed As String = absPathProgramDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS
                Dim pathSettingsDestination As String = testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_CSITESTER_SETTINGS

                ' Generate a settings class based on the Settings file located at another location. If fails, continue with original class.
                Try
                    If IO.File.Exists(pathSettingsDestination) Then testerSettings = New cSettings(pathSettingsDestination)
                Catch ex As Exception
                    RaiseEvent Log(New LoggerEventArgs(ex))
                End Try

                ' Overwrite certain settings that the user might have changed
                With testerSettings
                    ' Regardless of what is in the settings file, this is the only level allowed for eCSiInstallMethod.UseIni
                    .csiTesterlevel = eCSiTesterLevel.Published

                    ' Set settings file destination to that stored in the *.ini file, now represented in the form. 
                    ' This will propagate into other parts of the program.
                    .testerDestination.SetProperties(testerDestinationDir, DIR_TESTER_DESTINATION_DIR_DEFAULT)
                    .programName = programName

                    ' Set external properties
                    .iniFile.SetProperties(pathIni)
                    .xmlFileInstalled.SetProperties(pathSettingsSeed)
                    .seedDirectory.SetProperties(absPathProgramDir & "\" & DIR_NAME_VERIFICATION & "\" & DIR_NAME_CSITESTER, p_pathUnknown:=False)
                    .csiTesterFile.SetProperties(pathSettingsDestination, p_pathUnknown:=False)
                End With
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Reads regTest XML values into memory. Assigns defaults to parameters not yet added to regTest.
    ''' </summary>
    ''' <param name="p_installMethod">Specifies whether an initialization file is being used for the CSiTester program.</param>
    ''' <param name="p_firstLoad">Optional: If false, additional and different initialization steps are taken. 'False' is coupled with installMethod = UseIni.</param>
    ''' <remarks></remarks>
    Private Sub LoadRegTest(ByVal p_installMethod As eCSiInstallMethod,
                            Optional ByVal p_firstLoad As Boolean = True)
        Try
            If p_firstLoad Then
                myRegTest = New cRegTest(p_installMethod)
                myRegTest.xmlInstallationFile.SetProperties(myRegTest.xmlFile.path)
            Else                                                'Operations only for Published version
                Dim pathRegTestSeed As String = absPathProgramDir & "\" & DIR_NAME_VERIFICATION & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_REGTEST_CONTROL
                Dim pathRegTestDestinationDir As String = testerDestinationDir & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST
                Dim pathControlSeed As String = absPathProgramDir & "\" & DIR_NAME_VERIFICATION & "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\" & FILENAME_CONTROL
                Dim modelsOutputPath As String = testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION

                'Generate regTest class based on the regTest file located at another location. If fails, continue with original class.
                Try
                    If IO.File.Exists(pathRegTestDestinationDir & "/" & FILENAME_REGTEST_CONTROL) Then
                        Dim newSuitePath As String = absPathProgramDir & "\" & DIR_NAME_VERIFICATION & "\" & DIR_NAME_CSITESTER
                        Dim newXmlPath As String = pathRegTestDestinationDir & "\" & FILENAME_REGTEST_CONTROL

                        myRegTest = New cRegTest(p_installMethod,
                                                 newSuitePath,
                                                 newXmlPath,
                                                 reInitializeAfterCopy:=False)
                    End If
                Catch ex As Exception
                    RaiseEvent Log(New LoggerEventArgs(ex))
                End Try

                With myRegTest
                    ' Sync program name to enforce settings shipped.
                    .program_name = testerSettings.programName

                    ' Set other properties.
                    .xmlInstallationFile.SetProperties(pathRegTestSeed)
                    .xmlControlInstallationFile.SetProperties(pathControlSeed)

                    If Not String.IsNullOrEmpty(_pathProgramSaved) Then
                        ' Set program path to the new one specified during startup.
                        .program_file.SetProperties(_pathProgramSaved)
                    ElseIf _newIniCreated Then
                        ' Set program path to the original installed path. 
                        ' Else, the path saved in the local regTest.xml file will be used.
                        .program_file.SetProperties(_pathProgramInstall)
                    End If
                    .programFileInstall.SetProperties(_pathProgramInstall)

                    .models_database_directory.SetProperties(absPathProgramDir & "\" & DIR_NAME_VERIFICATION & "\" & DIR_NAME_MODELS_DESTINATION)
                    .models_run_directory.SetProperties(testerDestinationDir & "\" & GetSuffix(.models_database_directory.path, "\"))

                    .control_xml_file.SetProperties(testerDestinationDir & cPathRegTest.pathRelativeToProgram & FILENAME_CONTROL)
                    .output_directory.SetProperties(modelsOutputPath)
                End With
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    '=== Misc
    Private Sub CommandLineTest()
        'TODO: Temporary.  Done to check the command line content. Remove once this feature is fully implemented.
        'Command Line Parameters are passed in to project
        Dim args As String() = Application.mArgs
        If args Is Nothing Then Exit Sub

        Dim msg As String = ""
        For i = 0 To args.Length - 1
            msg = msg & args(i) & Environment.NewLine & Environment.NewLine
        Next
        WriteTextFile(pathStartup() & "\commandLine.txt", msg, True)
    End Sub
#End Region

End Class
