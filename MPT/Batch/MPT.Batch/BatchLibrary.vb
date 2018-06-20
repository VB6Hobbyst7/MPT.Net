Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Contains routines for writing, running, and removing batch files.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class BatchLibrary

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Deletes all files relating to a supplied list of extensions using a batch file.
    ''' </summary>
    ''' <param name="p_directoryPath">Path to the directory containing the files to be deleted.</param>
    ''' <param name="p_fileExtensionList">List of the file extensions of which all files will be deleted.</param>
    ''' <param name="p_batchPath">Path to the batch file to write and run.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFilesBatch(ByVal p_directoryPath As String,
                                       ByVal p_fileExtensionList As List(Of String),
                                       ByVal p_batchPath As String)
        Dim commandLine As String = ""

        For Each fileExtension As String In p_fileExtensionList
            commandLine &= "FOR /R " & Chr(34) & p_directoryPath & Chr(34) & " %%G IN (" & fileExtension & ") DO del " & Chr(34) & "%%G" & Chr(34) & Environment.NewLine
        Next

        'Appends 'exit' command to batch file
        commandLine &= "exit"    'Adds command in batch file to close command line after batch run
        WriteBatch(commandLine, p_batchPath, True)

        RunBatch(p_batchPath, True, True, True)
    End Sub

    ''' <summary>
    ''' Appends the specified command line to a batch file. If no lines exist, this begins a new batch file.
    ''' </summary>
    ''' <param name="p_commandLine">Line to write to the batch file.</param>
    ''' <param name="p_batchPath">Path to the batch file.</param>
    ''' <param name="p_deleteExisting">True: If a filename already exists at the path specified, it will be deleted so that a new one is created.</param>
    ''' <remarks></remarks>
    Public Shared Sub WriteBatch(ByVal p_commandLine As String,
                                 ByVal p_batchPath As String,
                                 Optional ByVal p_deleteExisting As Boolean = False)
        Try
            ' Deletes Existing Batch File
            If (p_deleteExisting AndAlso File.Exists(p_batchPath)) Then File.Delete(p_batchPath)

            Using objWriter As New StreamWriter(p_batchPath, True)
                objWriter.WriteLine(p_commandLine) ' Append file string
            End Using
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Runs batch file then deletes batch file if specified.
    ''' </summary>
    ''' <param name="p_pathBatch">Path to the batch file, including the file name</param>
    ''' <param name="p_deleteBatchFile">Specify whether to delete the batch file after run</param>
    ''' <param name="p_waitForExit">Wait until batch process has exit before deleting the batch file. (Currently does not seem to work).</param>
    ''' <param name="p_consoleIsNotVisible">True: Batch run will not be visible from a command console window.</param>
    ''' <remarks>Note: DO NOT use relative paths in batch files with this procedure. Use absolute paths.??? Maybe not necessary with this method</remarks>
    Public Shared Sub RunBatch(ByVal p_pathBatch As String,
                               Optional ByVal p_deleteBatchFile As Boolean = False,
                               Optional ByVal p_waitForExit As Boolean = False,
                               Optional ByVal p_consoleIsNotVisible As Boolean = False)
        Dim batchProcess As New Process()
        batchProcess.StartInfo.FileName = p_pathBatch
        batchProcess.StartInfo.WorkingDirectory = PathStartupApp()    'Default location, same spot as location of .EXE
        batchProcess.StartInfo.CreateNoWindow = p_consoleIsNotVisible
        If p_consoleIsNotVisible Then
            batchProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Else
            batchProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
        End If

        batchProcess.Start()

        If p_waitForExit Then batchProcess.WaitForExit()

        ' Deletes Batch File
        System.Threading.Thread.Sleep(1000)
        If p_deleteBatchFile = True Then File.Delete(p_pathBatch)
    End Sub
#End Region
End Class
