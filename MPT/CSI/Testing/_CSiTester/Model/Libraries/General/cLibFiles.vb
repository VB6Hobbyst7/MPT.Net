Option Strict On
Option Explicit On

Imports System.IO
Imports CSiTester.cLibFolders
Imports CSiTester.cLibPath

''' <summary>
''' Contains routines used for working with files, such as opening and closing files in general, and reading/writing text files.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibFiles

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Querying"
    Public Shared Function GetFileDateModified(ByVal p_path As String) As String
        Return System.IO.File.GetLastWriteTime(p_path).ToShortDateString()
    End Function
#End Region

#Region "File Access"
    ''' <summary>
    ''' Opens a file of a given path.
    ''' </summary>
    ''' <param name="p_path">Path to the file to open.</param>
    ''' <param name="p_errorMessage">Error message to display if the file does not exist at the specified path</param>
    '''<param name="p_checkFileInUse">True: The file is first checked to see if it is in use. If so, the user can choose to retry or abort. False: No check is done.</param>
    ''' <remarks></remarks>
    Public Shared Function OpenFile(ByVal p_path As String,
                                    Optional p_errorMessage As String = "File Does Not Exist",
                                    Optional p_checkFileInUse As Boolean = False) As Boolean
        Try
            If File.Exists(p_path) Then
                If ((p_checkFileInUse AndAlso
                     Not FileInUseAction(p_path)) OrElse
                    Not p_checkFileInUse) Then

                    Process.Start(p_path)
                    Return True
                End If
            Else
                MsgBox(p_errorMessage)
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Checks if a file is in use. Returns true/false status.
    ''' </summary>
    ''' <param name="p_path">Path to the file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileInUse(ByVal p_path As String) As Boolean
        If File.Exists(p_path) Then
            Try
                Using f As New FileStream(p_path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                    ' thisFileInUse = False
                End Using
            Catch
                Return True
            End Try
        End If
        Return False
    End Function

    ''' <summary>
    ''' Checks if a file is in use and prompts the user if they want to check the file again. User can abort at any time, or try as long as they wish.
    ''' </summary>
    ''' <param name="p_path">Path to the file to check if it is in use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileInUseAction(ByVal p_path As String) As Boolean
        Dim fileCurrentlyInUse As Boolean = True

        If FileInUse(p_path) Then
            While fileCurrentlyInUse
                Select Case MessageBox.Show("The following file is currently in use: " & vbCrLf & vbCrLf & p_path & vbCrLf & vbCrLf &
                                            "Do you want to try again?",
                                            "File in Use",
                                            MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
                    Case MessageBoxResult.Yes
                    Case MessageBoxResult.No : Exit While
                End Select
                fileCurrentlyInUse = FileInUse(p_path)
            End While
        Else
            Return False
        End If

        Return fileCurrentlyInUse
    End Function


    ''' <summary>
    ''' Waits for file to become available. Optional parameters allow control over interval of time checking and number of checks.
    ''' </summary>
    ''' <param name="p_path">Path to the file.</param>
    ''' <param name="p_timeCheckInterval">Delay between retries, in milliseconds.</param>
    ''' <param name="p_attemptsLimit">Maximum number of attempts to make before failure.</param>
    ''' <param name="p_promptUser">True: The user will be prompted that the file is in use, and asked if access should be tried again.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function WaitUntilFileAvailable(ByVal p_path As String,
                                                  Optional ByVal p_timeCheckInterval As Integer = 900,
                                                  Optional ByVal p_attemptsLimit As Integer = 100,
                                                  Optional ByVal p_promptUser As Boolean = False) As Boolean
        Dim attempts As Integer = 0

        If Not FileExists(p_path) Then Return True
        While FileInUse(p_path)
            System.Threading.Thread.Sleep(p_timeCheckInterval)
            attempts += 1
            If attempts = p_attemptsLimit Then
                If p_promptUser Then
                    Select Case MessageBox.Show("The following file is currently in use: " & vbCrLf & vbCrLf & p_path & vbCrLf & vbCrLf & "Do you want to try again?", "File in Use", MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
                        Case MessageBoxResult.Yes : attempts = 0
                        Case MessageBoxResult.No : Return False
                    End Select
                Else
                    Return False
                End If
            End If

            'Check if file has been deleted
            If Not FileExists(p_path) Then Return False
        End While

        Return True
    End Function

#End Region

#Region "Create File"
    ''' <summary>
    ''' Writes a text file of the supplied name/path and containing the string content provided.
    ''' </summary>
    ''' <param name="p_filePath">Path, including filename, of the file to be written.</param>
    ''' <param name="p_content">Content to write to the file.</param>
    ''' <param name="p_deleteExisting">True: The any existing file at the same path will be deleted before writing a new file. 
    ''' False: The content will be appended to the existing file.</param>
    ''' <remarks></remarks>
    Public Shared Sub WriteTextFile(ByVal p_filePath As String,
                                    ByVal p_content As String,
                                    ByVal p_deleteExisting As Boolean)
        If p_deleteExisting Then ComponentDeleteFileAction(p_filePath)

        Using objWriter As New StreamWriter(p_filePath, append:=True)
            objWriter.WriteLine(p_content)
        End Using
    End Sub

#End Region

#Region "Initialization File"

    ''' <summary>
    ''' Reads the INI file to determine the text on the specified line.
    ''' </summary>
    ''' <param name="p_path">Path to the *.ini file to be read.</param>
    ''' <param name="p_readLine">Line to be read in the *.ini: 1 = Model destination directory.</param>
    ''' <returns>Text on the specified line.</returns>
    ''' <remarks>$ indicates new line, for parsing.</remarks>
    Public Shared Function ReadIniFile(ByVal p_path As String,
                                       ByVal p_readLine As Integer) As String
        Dim checkBlanks As Boolean
        Dim i As Integer
        Dim lineStart As Integer
        Dim lineEnd As Integer
        Dim currentLine As Integer = 0

        'path = pathStartup() & "\csiTest.ini"

        Using reader As New StreamReader(p_path)
            ReadIniFile = reader.ReadToEnd()

            'Gathers text for desired line
            For i = 1 To Len(ReadIniFile)
                If Mid(ReadIniFile, i, 1) = "$" Then
                    currentLine = currentLine + 1
                    If currentLine = p_readLine Then lineStart = i + 2 'String start location, subtrating the "$ " portion
                    If currentLine = p_readLine + 1 Then
                        lineEnd = i - 1                                 'String end location, subtracting the "$" portion of the following line that triggers this end line declaration
                        ReadIniFile = Mid(ReadIniFile, lineStart, lineEnd - lineStart)
                    End If
                End If
            Next

            'Removes all blanks at the end of the line
            checkBlanks = True
            If Len(ReadIniFile) > 0 Then
                While checkBlanks
                    If String.IsNullOrWhiteSpace(Mid(ReadIniFile, Len(ReadIniFile), 1)) Then
                        ReadIniFile = Left(ReadIniFile, Len(ReadIniFile) - 1)
                    Else
                        checkBlanks = False
                    End If
                End While
            End If
        End Using
    End Function

    ''' <summary>
    ''' Writes a supplied value to a given line of an *.ini file of a given path. 
    ''' Other lines are preserved. 
    ''' If a line is greater than the contents of the file, no new value will be written.
    ''' $ in the file indicates new line, for parsing.
    ''' </summary>
    ''' <param name="p_path">Path to the directory containig the CSiTester.ini file to write to.</param>
    ''' <param name="p_readLine">Line to write the supplied value to: 1 = Model destination directory.</param>
    ''' <param name="p_writeValue">Value to write to the *.ini file.</param>
    ''' <param name="p_newIniCreated">Flag for the program to know if the ini file has been rewritten or changed.</param>
    ''' <remarks>$ indicates new line, for parsing.</remarks>
    Public Shared Sub WriteIniFile(ByVal p_path As String,
                                   ByVal p_readLine As Integer,
                                   ByVal p_writeValue As String,
                                   Optional ByRef p_newIniCreated As Boolean = False)
        Dim path As String = p_path
        Dim endWrite As Boolean = False
        Dim readValue As String = ""
        Dim i As Integer = 1
        Dim iniPathTemp As String = GetPathDirectoryStub(p_path) & "\CSiTesterTemp.ini"

        Using objWriter As New StreamWriter(iniPathTemp)
            While Not endWrite
                readValue = ReadIniFile(path, i)
                If Left(readValue, 1) = "$" Then
                    endWrite = True
                Else
                    If Not i = p_readLine Then    'Write value back in to file
                        objWriter.WriteLine("$ " & readValue)
                    Else                        'Write new line
                        objWriter.WriteLine("$ " & p_writeValue)
                        If String.IsNullOrEmpty(readValue) Then endWrite = True 'Set function to stop reading lines if the original file is empty
                    End If

                    i += 1
                End If
            End While

            'End File
            objWriter.WriteLine("$")
        End Using

        'Replace ini file with temp ini file
        'DeleteFile(path, True)
        CopyFile(iniPathTemp, path, True, False)

        DeleteFile(iniPathTemp, True)

        p_newIniCreated = True
    End Sub

    ''' <summary>
    ''' Checks if .ini file exists, and if not, writes one with default parameters. 
    ''' This is needed for locating the XML files.
    ''' $ indicates new line, for parsing.
    ''' </summary>
    ''' <param name="p_path">Path to the initialization file. If not valid, a new file will be created in the path directory with default parameters.</param>
    ''' <param name="p_defaultDestination">Path to the default destination directory to be written to the file.</param>
    ''' <param name="p_newIniCreated">Flag for the program to know if the ini file has been rewritten or changed.</param>
    ''' <remarks>$ indicates new line, for parsing.</remarks>
    Public Shared Sub InitializeInstallIniFile(ByVal p_path As String,
                                               ByVal p_defaultDestination As String,
                                               Optional ByRef p_newIniCreated As Boolean = False)
        Dim path As String = p_path

        If Not File.Exists(path) Then
            'Creates new .ini file with default values if file is missing
            Using objWriter As New StreamWriter(path)
                'regTest version to use
                'objWriter.WriteLine("$ " & "regTest.xml")                  ' Append file string

                'In-House or Release?
                'objWriter.WriteLine("$ " & "Release")                   'See InitializeReleaseDefaults for other types

                'Destination Folder Location
                objWriter.WriteLine("$ " & p_defaultDestination)                  ' Append file string

                'End File
                objWriter.WriteLine("$")
            End Using
            p_newIniCreated = True
        End If


    End Sub

    'Not Used
    ''' <summary>
    ''' Changes .ini file parameter. 
    ''' If user creates a new regTest.xml under a different name, the .xml to be used is referenced here.
    ''' </summary>
    ''' <param name="p_regTestName"></param>
    ''' <remarks></remarks>
    Public Shared Sub ChangeInstallIniFile(ByVal p_regTestName As String)
        Dim path As String = pathStartup() & "\csiTest.ini"
        Using objWriter As New StreamWriter(path)
            objWriter.WriteLine(p_regTestName) ' Append file string
        End Using
    End Sub


#End Region

#Region "Processes/Programs"
    ''' <summary>
    ''' Checks to see if a process is currently running.
    ''' </summary>
    ''' <param name="p_processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ProcessIsRunning(ByVal p_processName As String) As Boolean
        Try
            Dim processes() As Process = Process.GetProcessesByName(p_processName)

            If processes.Count > 0 Then
                ' Process is running
                Return True
            Else
                ' Process is not running
                Return False
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks to see if a process is responding.
    ''' </summary>
    ''' <param name="p_processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ProcessIsResponding(ByVal p_processName As String) As Boolean
        Try
            Dim processes() As Process = Process.GetProcessesByName(p_processName)

            ' Tests the Responding property for a True return value. 
            If (processes.Count > 0 AndAlso processes(0).Responding) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Ends a process of a given name.
    ''' </summary>
    ''' <param name="p_processName">Name of the process to check. It does not matter if ".exe" is included.</param>
    ''' <remarks></remarks>
    Public Shared Sub EndProcess(ByVal p_processName As String)
        Try
            Dim processes() As Process = Process.GetProcessesByName(p_processName)
            For Each Process As Process In processes
                Process.Kill()
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

#End Region
End Class
