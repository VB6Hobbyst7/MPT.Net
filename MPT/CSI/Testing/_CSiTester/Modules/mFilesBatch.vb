Option Explicit On
Option Strict On

''' <summary>
''' Module that contains routines for writing, running, and removing batch files.
''' </summary>
''' <remarks></remarks>
Module mFilesBatch

    '    ''' <summary>
    '    ''' Deletes all files relating to a supplied list of extensions using a batch file.
    '    ''' </summary>
    '    ''' <param name="fileExtensionList">List of the file extensions of which all files will be deleted.</param>
    '    ''' <param name="batchPath">Path to the batch file to write and run.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteFilesBatch(ByVal fileExtensionList As List(Of String), ByVal batchPath As String)
    '        Dim commandLine As String = ""

    '        For Each fileExtension As String In fileExtensionList
    '            commandLine = commandLine & "FOR /R " & Chr(34) & path & Chr(34) & " %%G IN (" & fileExtension & ") DO del " & Chr(34) & "%%G" & Chr(34) & vbCrLf
    '        Next

    '        'Appends 'exit' command to batch file
    '        commandLine = commandLine & "exit"    'Adds command in batch file to close command line after batch run
    '        WriteBatch(commandLine, batchPath, True)

    '        RunBatch(batchPath, True, True, True)
    '    End Sub

    '    ''' <summary>
    '    ''' Appends the specified command line to a batch file. If no lines exist, this begins a new batch file.
    '    ''' </summary>
    '    ''' <param name="commandLine">Line to write to the batch file.</param>
    '    ''' <param name="batchPath">Path to the batch file.</param>
    '    ''' <param name="deleteExisting">Optional: If true, then if a filename already exists at the path specified, it will be deleted so that a new one is created.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteBatch(ByVal commandLine As String, ByVal batchPath As String, Optional ByVal deleteExisting As Boolean = False)
    '        On Error GoTo Err_Handler

    '        ' Deletes Existing Batch File
    '        If deleteExisting Then
    '            If FileExists(batchPath) Then ComponentDeleteFileAction(batchPath)
    '        End If

    '        Dim objWriter As New System.IO.StreamWriter(batchPath, True)
    '        objWriter.WriteLine(commandLine) ' Append file string
    '        objWriter.Close()

    'Exit_Err_Handler:
    '        Exit Sub

    'Err_Handler:
    '        MsgBox("The following error has occured" & vbCrLf & vbCrLf & _
    '            "Error Number: " & Err.Number & vbCrLf & _
    '            "Error Source: AppendTxt" & vbCrLf & _
    '            "Error Description: " & Err.Description, vbCritical, "An Error has Occured!")
    '        GoTo Exit_Err_Handler

    '    End Sub

    '    ''' <summary>
    '    ''' Runs batch file then deletes batch file if specified
    '    ''' </summary>
    '    ''' <param name="pathBatch">Path to the batch file, including the file name</param>
    '    ''' <param name="deleteBatchFile">Optional: Specify whether to delete the batch file after run</param>
    '    ''' <param name="waitForExit">Optional: Wait until batch process has exit before deleting the batch file. (Currently does not seem to work).</param>
    '    ''' <param name="consoleIsNotVisible">Optional: If true, batch run will not be visible from a command console window.</param>
    '    ''' <remarks>Note: DO NOT use relative paths in batch files with this procedure. Use absolute paths.??? Maybe not necessary with this method</remarks>
    '    Sub RunBatch(ByVal pathBatch As String, Optional ByVal deleteBatchFile As Boolean = False, Optional ByVal waitForExit As Boolean = False, Optional ByVal consoleIsNotVisible As Boolean = False)
    '        Dim batchProcess As New Process()
    '        batchProcess.StartInfo.FileName = pathBatch
    '        batchProcess.StartInfo.WorkingDirectory = pathStartup()    'Default location, same spot as location of .EXE
    '        batchProcess.StartInfo.CreateNoWindow = consoleIsNotVisible
    '        If consoleIsNotVisible Then
    '            batchProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
    '        Else
    '            batchProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal
    '        End If

    '        batchProcess.Start()

    '        If waitForExit Then batchProcess.WaitForExit()
    '        'MsgBox("""" & BatchPath & """")

    '        ' Deletes Batch File
    '        System.Threading.Thread.Sleep(1000)
    '        If deleteBatchFile = True Then System.IO.File.Delete(pathBatch)

    '        'batchProcess = Nothing
    '    End Sub
End Module
