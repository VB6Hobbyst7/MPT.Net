Option Explicit On
Option Strict On

Imports System.IO

''' <summary>
''' Module that contains routines used for working with files, such as opening and closing files in general, and reading/writing text files.
''' </summary>
''' <remarks></remarks>
Module mFiles

    '#Region "Querying"
    '    Function GetFileDateModified(ByVal pathFile As String) As String
    '        Dim strLastModified As String

    '        strLastModified = System.IO.File.GetLastWriteTime(pathFile).ToShortDateString()

    '        Return strLastModified
    '    End Function
    '#End Region

    '#Region "File Access"
    '    ''' <summary>
    '    ''' Opens a file of a given path.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file to open.</param>
    '    ''' <param name="myErrorMessage">Optional error message to display if the file does not exist at the specified path</param>
    '    '''<param name="checkFileInUse">Optional. If true, the file is first checked to see if it is in use. If so, the user can choose to retry or abort. If false, no check is done.</param>
    '    ''' <remarks></remarks>
    '    Function OpenFile(ByVal myPath As String, Optional myErrorMessage As String = "File Does Not Exist", Optional checkFileInUse As Boolean = False) As Boolean
    '        OpenFile = False
    '        Try
    '            If File.Exists(myPath) Then
    '                If checkFileInUse Then
    '                    If Not FileInUseAction(myPath) Then
    '                        OpenFile = True
    '                        Process.Start(myPath)
    '                    End If
    '                Else
    '                    OpenFile = True
    '                    Process.Start(myPath)
    '                End If
    '            Else
    '                MsgBox(myErrorMessage)
    '            End If
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '            Exit Function
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Checks if a file is in use. Returns true/false status.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function FileInUse(ByVal myPath As String) As Boolean
    '        Dim thisFileInUse As Boolean = False

    '        If File.Exists(myPath) Then
    '            Try
    '                Using f As New FileStream(myPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
    '                    ' thisFileInUse = False
    '                End Using
    '            Catch
    '                thisFileInUse = True
    '            End Try
    '        End If
    '        Return thisFileInUse
    '    End Function

    '    ''' <summary>
    '    ''' Checks if a file is in use and prompts the user if they want to check the file again. User can abort at any time, or try as long as they wish.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file to check if it is in use.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function FileInUseAction(ByVal myPath As String) As Boolean
    '        Dim fileCurrentlyInUse As Boolean = True

    '        If FileInUse(myPath) Then
    '            While fileCurrentlyInUse
    '                Select Case MessageBox.Show("The following file is currently in use: " & vbCrLf & vbCrLf & myPath & vbCrLf & vbCrLf & "Do you want to try again?", "File in Use", MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
    '                    Case MessageBoxResult.Yes
    '                    Case MessageBoxResult.No : Exit While
    '                End Select
    '                fileCurrentlyInUse = FileInUse(myPath)
    '            End While
    '        Else
    '            fileCurrentlyInUse = False
    '        End If

    '        Return fileCurrentlyInUse
    '    End Function


    '    ''' <summary>
    '    ''' Waits for file to become available. Optional parameters allow control over interval of time checking and number of checks.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file.</param>
    '    ''' <param name="timeCheckInterval">Delay between retries, in milliseconds.</param>
    '    ''' <param name="attemptsLimit">Maximum number of attempts to make before failure.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function WaitUntilFileAvailable(ByVal myPath As String, Optional ByVal timeCheckInterval As Integer = 900, Optional ByVal attemptsLimit As Integer = 100, Optional ByVal promptUser As Boolean = False) As Boolean
    '        Dim attempts As Integer = 0

    '        If Not FileExists(myPath) Then Return True
    '        While FileInUse(myPath)
    '            System.Threading.Thread.Sleep(timeCheckInterval)
    '            attempts += 1
    '            If attempts = attemptsLimit Then
    '                If promptUser Then
    '                    Select Case MessageBox.Show("The following file is currently in use: " & vbCrLf & vbCrLf & myPath & vbCrLf & vbCrLf & "Do you want to try again?", "File in Use", MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
    '                        Case MessageBoxResult.Yes : attempts = 0
    '                        Case MessageBoxResult.No : Return False
    '                    End Select
    '                Else
    '                    Return False
    '                End If
    '            End If

    '            'Check if file has been deleted
    '            If Not FileExists(myPath) Then Return False
    '        End While

    '        Return True
    '    End Function

    '#End Region

    '#Region "Create File"
    '    ''' <summary>
    '    ''' Writes a text file of the supplied name/path and containing the string content provided.
    '    ''' </summary>
    '    ''' <param name="filePath">Path, including filename, of the file to be written.</param>
    '    ''' <param name="content">Content to write to the file.</param>
    '    ''' <param name="deleteExisting">If true, the any existing file at the same path will be deleted before writing a new file. If false, the content will be appended to the existing file.</param>
    '    ''' <remarks></remarks>
    '    Sub WriteTextFile(ByVal filePath As String, ByVal content As String, ByVal deleteExisting As Boolean)
    '        ' Deletes Existing Batch File
    '        If deleteExisting Then
    '            If FileExists(filePath) Then ComponentDeleteFileAction(filePath)
    '        End If

    '        Dim objWriter As New System.IO.StreamWriter(filePath, True)
    '        objWriter.WriteLine(content) ' Append file string
    '        objWriter.Close()
    '    End Sub

    '#End Region

    '#Region "Initialization File"

    '    ''' <summary>
    '    ''' Reads the INI file to determine the text on the specified line.
    '    ''' </summary>
    '    ''' <param name="pathIni">Path to the *.ini file to be read.</param>
    '    ''' <param name="readLine">Line to be read in the *.ini: 1 = Model destination directory.</param>
    '    ''' <returns>Text on the specified line.</returns>
    '    ''' <remarks>$ indicates new line, for parsing.</remarks>
    '    Function ReadIniFile(ByVal pathIni As String, ByVal readLine As Integer) As String
    '        Dim checkBlanks As Boolean
    '        Dim i As Integer
    '        Dim lineStart As Integer
    '        Dim lineEnd As Integer
    '        Dim currentLine As Integer = 0

    '        'path = pathStartup() & "\csiTest.ini"

    '        Dim sr As New StreamReader(pathIni)
    '        ReadIniFile = sr.ReadToEnd()

    '        'Gathers text for desired line
    '        For i = 1 To Len(ReadIniFile)
    '            If Mid(ReadIniFile, i, 1) = "$" Then
    '                currentLine = currentLine + 1
    '                If currentLine = readLine Then lineStart = i + 2 'String start location, subtrating the "$ " portion
    '                If currentLine = readLine + 1 Then
    '                    lineEnd = i - 1                                 'String end location, subtracting the "$" portion of the following line that triggers this end line declaration
    '                    ReadIniFile = Mid(ReadIniFile, lineStart, lineEnd - lineStart)
    '                End If
    '            End If
    '        Next

    '        'Removes all blanks at the end of the line
    '        checkBlanks = True
    '        If Len(ReadIniFile) > 0 Then
    '            While checkBlanks
    '                If String.IsNullOrWhiteSpace(Mid(ReadIniFile, Len(ReadIniFile), 1)) Then
    '                    ReadIniFile = Left(ReadIniFile, Len(ReadIniFile) - 1)
    '                Else
    '                    checkBlanks = False
    '                End If
    '            End While
    '        End If

    '        sr.Close()
    '        sr = Nothing
    '    End Function

    '    ''' <summary>
    '    ''' Writes a supplied value to a given line of an *.ini file of a given path. Other lines are preserved. If a line is greater than the contents of the file, no new value will be written.
    '    ''' </summary>
    '    ''' <param name="iniPath">Path to the directory containig the CSiTester.ini file to write to.</param>
    '    ''' <param name="readLine">Line to write the supplied value to: 1 = Model destination directory.</param>
    '    ''' <param name="writeValue">Value to write to the *.ini file.</param>
    '    ''' <param name="newIniCreated">Optional: Flag for the program to know if the ini file has been rewritten or changed.</param>
    '    ''' <remarks>$ indicates new line, for parsing.</remarks>
    '    Sub WriteIniFile(ByVal iniPath As String, ByVal readLine As Integer, ByVal writeValue As String, Optional ByRef newIniCreated As Boolean = False)
    '        Dim path As String = iniPath
    '        Dim endWrite As Boolean = False
    '        Dim readValue As String = ""
    '        Dim i As Integer = 1
    '        Dim iniPathTemp As String = GetPathDirectoryStub(iniPath) & "\CSiTesterTemp.ini"

    '        Dim objWriter As New System.IO.StreamWriter(iniPathTemp)

    '        While Not endWrite
    '            readValue = ReadIniFile(path, i)
    '            If Left(readValue, 1) = "$" Then
    '                endWrite = True
    '            Else
    '                If Not i = readLine Then    'Write value back in to file
    '                    objWriter.WriteLine("$ " & readValue)
    '                Else                        'Write new line
    '                    objWriter.WriteLine("$ " & writeValue)
    '                    If readValue = "" Then endWrite = True 'Set function to stop reading lines if the original file is empty
    '                End If

    '                i += 1
    '            End If
    '        End While

    '        'End File
    '        objWriter.WriteLine("$")

    '        objWriter.Close()
    '        objWriter = Nothing

    '        'Replace ini file with temp ini file
    '        'DeleteFile(path, True)
    '        CopyFile(iniPathTemp, path, True, False)

    '        DeleteFile(iniPathTemp, True)

    '        newIniCreated = True
    '    End Sub

    '    ''' <summary>
    '    ''' Checks if .ini file exists, and if not, writes one with default parameters. This is needed for locating the XML files.
    '    ''' </summary>
    '    ''' <param name="iniPath">Path to the initialization file. If not valid, a new file will be created in the path directory with default parameters.</param>
    '    ''' <param name="defaultDestination">Path to the default destination directory to be written to the file.</param>
    '    ''' <param name="newIniCreated">Optional: Flag for the program to know if the ini file has been rewritten or changed.</param>
    '    ''' <remarks>$ indicates new line, for parsing.</remarks>
    '    Sub InitializeInstallIniFile(ByVal iniPath As String, ByVal defaultDestination As String, Optional ByRef newIniCreated As Boolean = False)
    '        Dim path As String = iniPath

    '        If Not File.Exists(path) Then                           'Creates new .ini file with default values if file is missing
    '            Dim objWriter As New System.IO.StreamWriter(path)

    '            'regTest version to use
    '            'objWriter.WriteLine("$ " & "regTest.xml")                  ' Append file string

    '            'In-House or Release?
    '            'objWriter.WriteLine("$ " & "Release")                   'See InitializeReleaseDefaults for other types

    '            'Destination Folder Location
    '            objWriter.WriteLine("$ " & defaultDestination)                  ' Append file string

    '            'End File
    '            objWriter.WriteLine("$")

    '            objWriter.Close()
    '            objWriter = Nothing

    '            newIniCreated = True
    '        End If


    '    End Sub

    '    'Not Used
    '    ''' <summary>
    '    ''' Changes .ini file parameter. If user creates a new regTest.xml under a different name, the .xml to be used is referenced here
    '    ''' </summary>
    '    ''' <param name="regTestName"></param>
    '    ''' <remarks></remarks>
    '    Sub ChangeInstallIniFile(ByVal regTestName As String)
    '        Dim path As String = pathStartup() & "\csiTest.ini"
    '        Dim objWriter As New System.IO.StreamWriter(path)

    '        objWriter.WriteLine(regTestName) ' Append file string
    '        objWriter.Close()
    '    End Sub


    '#End Region

    '#Region "Processes/Programs"
    '    ''' <summary>
    '    ''' Checks to see if a process is currently running.
    '    ''' </summary>
    '    ''' <param name="processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CheckIfProcessRunning(ByVal processName As String) As Boolean
    '        Try
    '            Dim myProcesses() As Process = Process.GetProcessesByName(processName)

    '            If myProcesses.Count > 0 Then
    '                ' Process is running
    '                CheckIfProcessRunning = True
    '            Else
    '                ' Process is not running
    '                CheckIfProcessRunning = False
    '            End If
    '        Catch ex As Exception
    '            CheckIfProcessRunning = False
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Checks to see if a process is responding.
    '    ''' </summary>
    '    ''' <param name="processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function CheckIfProcessResponding(ByVal processName As String) As Boolean
    '        Try
    '            Dim myProcesses() As Process = Process.GetProcessesByName(processName)

    '            ' Tests the Responding property for a True return value. 
    '            If (myProcesses.Count > 0 AndAlso myProcesses(0).Responding) Then
    '                CheckIfProcessResponding = True
    '            Else
    '                CheckIfProcessResponding = False
    '            End If
    '        Catch ex As Exception
    '            CheckIfProcessResponding = False

    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Function

    '    ''' <summary>
    '    ''' Ends a process of a given name.
    '    ''' </summary>
    '    ''' <param name="processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    '    ''' <remarks></remarks>
    '    Sub EndProcess(ByVal processName As String)
    '        Try
    '            Dim myProcesses() As Process = Process.GetProcessesByName(processName)
    '            For Each Process As Process In myProcesses
    '                Process.Kill()
    '            Next
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '#End Region



End Module
