Option Explicit On
Option Strict On

Imports System.IO

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting


Public Class cLibrary

    Public Shared Event Messenger(messenger As MessengerEventArgs)

    ''' <summary>
    ''' If the program source is no longer valid, this function will prompt the user to choose between correcting the issue, using a stable default value, or closing the program.
    ''' </summary>
    ''' <param name="p_programName">Name of the analysis program being tested.</param>
    ''' <param name="p_programPath">Path to an installation of the analysis program.</param>
    ''' <param name="p_updatePathOptional">If true, then the user is given an option to start CSiTester with the default path. If false, this is done automatically.</param>
    ''' <remarks></remarks>
    Public Shared Sub ExceptionProgramSource(ByRef p_programName As String,
                                             ByRef p_programPath As String,
                                             Optional ByVal p_updatePathOptional As Boolean = True)
        Dim msgBoxMessage As String
        Dim msgBoxMessageOptional As String
        Dim programPathValid As Boolean = False

        If Not p_updatePathOptional Then
            msgBoxMessageOptional = ""
        Else
            msgBoxMessageOptional = "    'No' will begin CSiTester with a default path. " & Environment.NewLine & "    "
        End If

        msgBoxMessage = "The currently specified " & p_programName & " program path is no longer valid. Please specify the location of program to be tested. " & Environment.NewLine & Environment.NewLine & msgBoxMessageOptional & "'Cancel' will exit the program."

        'Set stable dummy starting path 
        'Currently commented out as this selection is not possible.
        'If csiTesterInstallMethod = eCSiInstallMethod.UseIni Then
        '    programPath = "..\" & programName & ".exe"          'Assuming tester is being run from the installation directory
        '    AbsolutePath(programPath)
        'Else
        p_programPath = pathStartup() & "\" & p_programName & ".exe"
        'End If

        'Create prompt for user to enter dialogue to select new valid folder path. Canceling will close the program as this step is critical to the program startup.
        If p_updatePathOptional Then
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNoCancel, eMessageType.Warning),
                                                msgBoxMessage,
                                                p_programName & " Program Path Invalid")
                Case eMessageActions.Yes
                    myCsiTester.BrowseProgram(p_programPath)
                Case eMessageActions.No
                    RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Warning),
                                                                "Path to program will be set as: " & Environment.NewLine & Environment.NewLine &
                                                                p_programPath & Environment.NewLine & Environment.NewLine &
                                                                "Please make sure to have a valid path specified before running any models.",
                                                                p_programName & " Default Path"))
                Case eMessageActions.Cancel
                    End
            End Select
        Else
                While Not programPathValid
                Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.OkCancel, eMessageType.Warning),
                                             msgBoxMessage,
                                             p_programName & " Program Path Invalid")
                    Case eMessageActions.OK
                        myCsiTester.BrowseProgram(p_programPath)
                    Case eMessageActions.Cancel
                        End
                End Select
                    programPathValid = File.Exists(p_programPath)
                End While
        End If
    End Sub



End Class
