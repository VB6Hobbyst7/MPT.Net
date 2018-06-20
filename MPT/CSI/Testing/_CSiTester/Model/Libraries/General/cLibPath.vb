Option Strict On
Option Explicit On

Imports Scripting
Imports System.Collections.ObjectModel

Imports CSiTester.cLibFolders

''' <summary>
''' Contains functions for determining and manipulating file paths, as well as strings in general.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibPath

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Relative-Absolute Paths"
    ''' <summary>
    ''' Converts absolute path to relative path. Format is not to end relative path a "\" for folder destinations. Returns currDir[\relativeToProgram] if fails.
    ''' </summary>
    ''' <param name="p_currDir">Path to the directory of the application.</param>
    ''' <param name="p_newDir">Absolute path to target directory.</param>
    ''' <param name="p_file">If the path is to a file, this is true. Otherwise, path is assumed to be to a directory.</param>
    ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter. The presence of starting and endings slashes does not matter.</param>
    ''' <returns></returns>
    ''' <remarks>Format right now is to end relative path a "\" for folder destinations.</remarks>
    Public Shared Function ConvertPathAbsoluteToRelative(ByVal p_currDir As String,
                                                          ByVal p_newDir As String,
                                                          Optional ByVal p_file As Boolean = False,
                                                          Optional ByVal p_relativeToProgram As String = "") As String
        Dim newDirFile As String = ""
        Dim newDirs As New List(Of String)
        Dim newDirUnique As String
        Dim currDirUp As String
        Dim currDirs As New List(Of String)

        Dim dirNum As Integer
        Dim dirNumEqual As Integer
        Dim dirEqual As Boolean

        Try
            '=== Initialize data
            'Ensures that no slash is left at the end of a path
            p_newDir = TrimPathSlash(p_newDir)
            p_currDir = TrimPathSlash(p_currDir)
            p_relativeToProgram = TrimPathSlash(p_relativeToProgram, True)      'Slash removed at beginning & end

            'Reformulate currDir with the program relative offset
            If Not String.IsNullOrEmpty(p_relativeToProgram) Then p_currDir = p_currDir & "\" & p_relativeToProgram

            '=== Validate data
            If String.IsNullOrEmpty(p_newDir) Then                                 'Checks that path is provided
                Return p_currDir
            ElseIf Len(p_newDir) <= 2 Then                        'Checks that path is a proper absolute path
                Throw New ArgumentException("Not a valid absolute path. New directory does not begin with '{driveLetter}:\'")
            ElseIf Not Left(p_currDir, 1) = Left(p_newDir, 1) Then  'Checks that both paths are in the same drive
                'If Not suppressExStates Then MsgBox("Not a valid absolute path. Paths are in different drives.")
                'Return currDir
            End If

            '=== Assemble data
            If p_file Then
                newDirFile = GetPathFileName(p_newDir)
                If Not String.IsNullOrEmpty(newDirFile) Then             'Path includes a file name
                    newDirFile = "\" & newDirFile
                    p_newDir = GetPathDirectoryStub(p_newDir)
                End If
            End If

            'Collect Current & New Directory path segments into lists
            currDirs = ListPathDirectories(p_currDir)
            newDirs = ListPathDirectories(p_newDir)

            'Determine which is the first directory where the paths are not equal
            dirNum = System.Math.Min(currDirs.Count, newDirs.Count)
            dirNumEqual = 0
            For i = 0 To dirNum - 1
                dirEqual = True
                'Rough check: Check directory length for each directory
                If Len(currDirs(i)) = Len(newDirs(i)) Then
                    'Detailed check: Check that each character matches
                    For j = 1 To Len(currDirs(i))
                        If Not Mid(currDirs(i), j, 1) = Mid(newDirs(i), j, 1) Then
                            dirEqual = False
                            Exit For
                        End If
                    Next
                Else
                    dirEqual = False
                End If
                'Increment the matching directories count or stop checking
                If dirEqual Then
                    dirNumEqual += 1
                Else
                    Exit For
                End If
            Next

            'Get portion of newDir that is unique
            newDirUnique = ""                                               'Assumed newDir is at or above currDir on the same branch, to start
            newDirUnique = UniquePathSegment(newDirs, dirNumEqual)
            If Not String.IsNullOrEmpty(newDirUnique) Then newDirUnique = "\" & newDirUnique 'NewDir is below currDir or on a separate brance from currDir, so it starts with a slash for appending

            'Handle cases
            'If dirNumEqual = 0 Then                                 '= newDir is in a different directory than currDir
            'If Not suppressExStates Then MsgBox("There are no matching directories between the two paths.")
            'Return currDir
            'ElseIf dirNumEqual = currDirs.Count Then                '== newDir is at or below currDir
            If dirNumEqual = currDirs.Count Then                '== newDir is at or below currDir
                'Append portion of newDir that is unique to the currDir path
                Return TrimPathSlash(newDirUnique, True, False) & newDirFile
            Else                                                    '== newDir is above currDir, or on a separate branch from currDir
                'Determine how many directories to move up in relative path. i.e. determine how many unique directories currDir has
                currDirUp = ""                                              'Assumed no directories to move up, to start
                currDirUp = UniquePathSegment(currDirs, dirNumEqual, "..")
                If Not String.IsNullOrEmpty(currDirUp) Then currDirUp = currDirUp & "\" 'Relative paths only moving up should always end in a slash

                'Append number of unique directories of currDir to move up to the unique newDir path
                Return currDirUp & TrimPathSlash(newDirUnique & newDirFile, True, False)        'Appended path should never start with a slash to avoid double slashes
            End If
        Catch argEx As ArgumentException
            csiLogger.ArgumentExceptionAction(argEx)
            Return p_currDir
        End Try
    End Function

    ''' <summary>
    ''' Returns a unique path segment for a supplied path, as compared to some other compared path, expressed by other parameters. Path segment will not have a trailing slash.
    ''' </summary>
    ''' <param name="p_directories">List of directory components of the path.</param>
    ''' <param name="p_numberDirectoriesEqual">Number of directory components in the path that are equal to the path compared.</param>
    ''' <param name="p_substituteCharacter">Optional: For each unique directory component. If blank, the directory components themselves are used. If specified, the specification will be an overridden constant.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UniquePathSegment(ByVal p_directories As List(Of String),
                                             ByVal p_numberDirectoriesEqual As Integer,
                                             Optional ByVal p_substituteCharacter As String = "") As String
        Dim myUniquePathSegment As String = ""
        Dim dirNumUnique As Integer
        Dim currentSubstituteChar As String = p_substituteCharacter

        dirNumUnique = p_directories.Count - p_numberDirectoriesEqual
        If dirNumUnique > 0 Then
            For i = 0 To dirNumUnique - 1
                If String.IsNullOrEmpty(p_substituteCharacter) Then
                    currentSubstituteChar = p_directories(p_numberDirectoriesEqual + i)
                Else
                    currentSubstituteChar = p_substituteCharacter
                End If
                myUniquePathSegment = myUniquePathSegment & currentSubstituteChar & "\"
                currentSubstituteChar = ""
            Next
            myUniquePathSegment = TrimPathSlash(myUniquePathSegment)          'Remove final trailing slash
        End If
        Return myUniquePathSegment
    End Function

    ''' <summary>
    ''' Collect specified path segments into a list of the component directories. This will include any file name if included in the path.
    ''' </summary>
    ''' <param name="p_dirPath">Path to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListPathDirectories(ByVal p_dirPath As String) As List(Of String)
        Dim tempDir As String = ""
        Dim tempDirs As New List(Of String)
        Dim charCurrent As String = ""

        For i = 1 To Len(p_dirPath)
            charCurrent = Mid(p_dirPath, i, 1)
            If charCurrent = "\" Then       'Directory change encountered
                If Not String.IsNullOrEmpty(tempDir) Then tempDirs.Add(tempDir) 'Checking tempDir="" handles cases of multiple "\"
                tempDir = ""
            ElseIf i = Len(p_dirPath) Then    'Final directory encountered
                tempDir = tempDir & charCurrent
                tempDirs.Add(tempDir)
            Else
                tempDir = tempDir & charCurrent
            End If
        Next

        Return tempDirs
    End Function

    'Below is an old function, which goes character-by-charater. Worked in many cases, but has some blind spots that are tricky to avoid.

    ' ''' <summary>
    ' ''' Converts absolute path to relative path.
    ' ''' </summary>
    ' ''' <param name="currDir">Path to the directory of the application.</param>
    ' ''' <param name="newDir">Absolute path to target directory.</param>
    ' ''' <param name="file">If the path is to a file, this is true. Otherwise, path is assumed to be to a directory.</param>
    ' ''' <param name="myRelativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter. The presence of starting and endings slashes does not matter.</param>
    ' ''' <returns></returns>
    ' ''' <remarks>Format right now is to end relative path a "\" for folder destinations.</remarks>
    'Function convAbsRel(ByRef currDir As String, ByRef newDir As String, Optional ByVal file As Boolean = False, Optional ByVal myRelativeToProgram As String = "") As String
    '    'Format right now is to end relative path a "\" for folder destinations.
    '    Dim relPath As String
    '    Dim i As Integer
    '    Dim jNewDir As Integer = 0
    '    Dim iCurrDirAtjCurrDir As Integer = 0
    '    Dim strLength As Integer
    '    Dim strMatch As Boolean
    '    Dim charNoMatch As Integer
    '    Dim dirFilterStart As Integer = 0
    '    Dim endSlash As String

    '    convAbsRel = currDir
    '    endSlash = "\"
    '    If file Then endSlash = ""

    '    strMatch = True
    '    relPath = ""

    '    If String.IsNullOrEmpty(newDir) Then Exit Function 'If text field is blank

    '    'Ensures that no slash is left at the end of a path
    '    currDir = cleanPathEnd(currDir)
    '    myRelativeToProgram = cleanPathEnd(myRelativeToProgram)
    '    If Left(myRelativeToProgram, 1) = "\" Then myRelativeToProgram = Right(myRelativeToProgram, Len(myRelativeToProgram) - 1)
    '    currDir = currDir & "\" & myRelativeToProgram
    '    currDir = cleanPathEnd(currDir)
    '    'newDir = Left(newDir, Len(newDir) - Len(myRelativeToProgram))   'Removes portion of path relative to another location than the program, if specified
    '    newDir = cleanPathEnd(newDir)

    '    'Checks that path is a proper absolute path
    '    If Len(newDir) <= 3 And Not suppressExStates Then
    '        MsgBox("Not a valid absolute path.")
    '        Exit Function
    '    End If

    '    strLength = System.Math.Max(Len(newDir), Len(currDir))

    '    For i = 1 To strLength
    '        If strMatch Then
    '            If i <= Len(currDir) Then
    '                If Not Mid(currDir.ToUpper, i, 1) = Mid(newDir.ToUpper, i, 1) Then
    '                    strMatch = False
    '                    relPath = "..\"
    '                    charNoMatch = i - 1
    '                    If Mid(currDir, i, 1) = "\" Then iCurrDirAtjCurrDir = i
    '                Else
    '                    If Mid(newDir, i, 1) = "\" Then 'Reset j-counter for latest drive slashes if both newDir & currDir have it
    '                        jNewDir = -1
    '                    End If
    '                End If
    '                If i = Len(currDir) And strMatch Then    'Folder is within another folder beneath the current location, or is the current location
    '                    relPath = Right(newDir, Len(newDir) - i)
    '                    If Left(relPath, 1) = "\" Then relPath = Right(relPath, Len(relPath) - 1) 'Removes preceding slash, if present
    '                    convAbsRel = relPath
    '                    If Not suppressExStates Then
    '                        If Not DirectoryExists(convRelAbs(currDir, convAbsRel)) Then
    '                            MsgBox("Debug!")
    '                        End If
    '                    End If
    '                    Exit Function
    '                End If
    '            End If
    '        Else    'Folder lies on a path above the current location. Set up in relation to where path splits or ends sooner
    '            If (i <= Len(newDir) AndAlso Mid(newDir, i - 1, 1) = "\") Then
    '                If (relPath = "..\" AndAlso Mid(currDir, i - jNewDir, 1) = Mid(newDir, i - jNewDir, 1)) Then
    '                    'This is the first slash encountered in newDir since the paths began to differ, and at least the first character of the last directory matches
    '                    If dirFilterStart = 0 Then
    '                        dirFilterStart = i - jNewDir - 1      'Only set dirReverse the first time
    '                        If (iCurrDirAtjCurrDir > 0 AndAlso i > iCurrDirAtjCurrDir) Then             'currDir has ended while newDir has not
    '                            relPath = relPath & "..\"   'Add additional directory jump, as the last directory is skipped in its entirety, but this isn't caught as currDir finishes processing 
    '                        End If
    '                    End If
    '                    jNewDir = -1
    '                End If
    '            End If
    '            If Mid(currDir, i, 1) = "\" Then
    '                relPath = relPath & "..\"
    '                iCurrDirAtjCurrDir = i
    '            End If
    '            If i = Len(currDir) Then Exit For
    '        End If
    '        jNewDir += 1
    '    Next i

    '    'Adjust length of non-matching characters to rewind to last directory slash
    '    If Not dirFilterStart = 0 Then charNoMatch = dirFilterStart

    '    'Only if folder lies on a different path above the current location
    '    convAbsRel = relPath & Right(newDir, Len(newDir) - charNoMatch)
    '    If Left(convAbsRel, 1) = "\" Then convAbsRel = Right(convAbsRel, Len(convAbsRel) - 1) 'Removes preceding slash, if present
    '    If Not suppressExStates Then
    '        If Not DirectoryExists(convRelAbs(currDir, convAbsRel)) Then
    '            MsgBox("Debug!")
    '        End If
    '    End If
    'End Function

    ''' <summary>
    ''' Converts relative path to absolute path. Returns appDir[/relativeToProgram] if fails.
    ''' </summary>
    ''' <param name="p_appDir">Path to the directory of the application.</param>
    ''' <param name="p_newDir">Relative path to target directory.</param>
    ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter. The presence of starting and endings slashes does not matter.</param>
    ''' <param name="p_ignoreFailure">Optional: If true, then even in development mode the failure warning message is suppressed, but the function returns the relative path.</param>
    ''' <returns></returns>
    ''' <remarks>Format right now is to end absolute path without a "\"</remarks>
    Public Shared Function ConvertPathRelativeToAbsolute(ByVal p_appDir As String,
                                                         ByVal p_newDir As String,
                                                         Optional ByVal p_relativeToProgram As String = "",
                                                         Optional ByVal p_ignoreFailure As Boolean = False) As String
        'Format right now is to end absolute path without a "\"
        Dim i As Integer
        Dim dirCount As Integer
        Dim dirCountRelativeCorrection As Integer
        Dim newDirTrunc As String

        dirCount = 0
        dirCountRelativeCorrection = 0

        Try
            '=== Initialize data
            'Ensures that no slash is left at the end of a path
            p_newDir = TrimPathSlash(p_newDir)
            p_appDir = TrimPathSlash(p_appDir)
            p_relativeToProgram = TrimPathSlash(p_relativeToProgram, True)      'Slash removed at beginning & end

            'Reformulate currDir with the program relative offset
            If Not String.IsNullOrEmpty(p_relativeToProgram) Then p_appDir = p_appDir & "\" & p_relativeToProgram

            '=== Determines how many directories to move up
            If String.IsNullOrEmpty(p_newDir) Then     'For case of path at current location
                Return p_appDir
            Else
                For i = 1 To Len(p_newDir)    'Determine how many directories up from the current application to go
                    If Mid(p_newDir, i, 3) = "..\" Then dirCount = dirCount + 1
                Next i
            End If

            newDirTrunc = Right(p_newDir, Len(p_newDir) - 3 * dirCount) 'Get remainder of relative path beyond the directory change references

            '=== Compare characters of application path from right to left to track directory changes
            For i = 0 To Len(p_appDir) - 1
                If Mid(p_appDir, Len(p_appDir) - i, 1) = "\" Then   'Reads right to left
                    dirCount = dirCount - 1     'Subtract next directory reversed through
                End If
                If dirCount = 0 Then      'Once all directories have been reversed, current count records the character index where this occurs
                    Return TrimPathSlash(Left(p_appDir, Len(p_appDir) - i)) & "\" & newDirTrunc 'Get portion of directory to current application path shared by the relative, then append remaining segment of relative path
                End If
                If dirCount = 1 And Mid(p_appDir, Len(p_appDir) - i + 1, 1) = ":" Then  'Final directory change is with the path letter, so no more "\" is encountered.
                    Return newDirTrunc
                End If
            Next

            If p_ignoreFailure Then
                Return p_newDir
            Else
                Throw New ArgumentException("Conversion from relative to absolute path failed." & vbCrLf & vbCrLf _
                                           & " p_appdir: " & p_appDir & vbCrLf & vbCrLf _
                                           & " p_newDir: " & p_newDir & vbCrLf & vbCrLf _
                                           & " p_relativeToProgram: " & p_relativeToProgram)
            End If
        Catch exArg As ArgumentException
            csiLogger.ArgumentExceptionAction(exArg)
        End Try

        Return p_appDir
    End Function

    ''' <summary>
    ''' Removes any doubles slashes in a path and replaces them with a single slash.
    ''' </summary>
    ''' <param name="p_path">Path to clean.</param>
    ''' <remarks></remarks>
    Public Shared Sub CleanDoubleSlash(ByRef p_path As String)
        Dim tempString As String = ""

        For i = 1 To Len(p_path)
            If Mid(p_path, i, 2) = "\\" Then
                tempString = tempString & "\"
            ElseIf (i - 1 > 0 AndAlso Not Mid(p_path, i - 1, 2) = "\\") Then
                tempString = tempString & Mid(p_path, i, 1)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Checks whether a path is relative or absolute by the presence of the ":" symbol.
    ''' </summary>
    ''' <param name="p_path">Path to be checked for relative vs. absolute type</param>
    ''' <returns>True if the path is relative, false if it is absolute</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckPathRelative(ByVal p_path As String) As Boolean
        If Not InStr(p_path, ":") = 2 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Checks if path is relative or absolute. If path is relative, path is converted to absolute. Returns true if an absolute path was created. False otherwise.
    ''' </summary>
    ''' <param name="p_path">Path to be checked and, if necessary, converted.</param>
    ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    ''' <param name="p_referencePath">Path to which the conversion is related. This is an absolute path.</param>
    ''' <param name="p_ignoreFailure">Optional: If true, then even in development mode the failure warning message is suppressed, but the function returns the relative path.</param>
    ''' <param name="p_cleanPath">True: The path will be trimmed of white space, leading and ending "\", and all "\\".</param>
    ''' <remarks></remarks>
    Public Shared Function AbsolutePath(ByRef p_path As String,
                                        Optional ByVal p_relativeToProgram As String = "",
                                        Optional ByVal p_referencePath As String = "",
                                        Optional ByVal p_ignoreFailure As Boolean = False,
                                        Optional ByVal p_cleanPath As Boolean = True) As Boolean
        Dim myPathTemp As String

        AbsolutePath = True

        If CheckPathRelative(p_path) Then
            If p_cleanPath Then
                TrimWhiteSpace(p_path)
                p_path = TrimPathSlash(p_path, True)
                CleanDoubleSlash(p_path)
            End If

            If String.IsNullOrEmpty(p_referencePath) Then p_referencePath = pathStartup()
            myPathTemp = ConvertPathRelativeToAbsolute(p_referencePath, p_path, p_relativeToProgram, p_ignoreFailure)

            If StringsMatch(p_path, myPathTemp) Then AbsolutePath = False 'conversion failed

            p_path = myPathTemp

            If p_cleanPath Then
                TrimPathSlash(p_path, True)
                CleanDoubleSlash(p_path)
            End If
        End If
    End Function

    ''' <summary>
    ''' Checks if path is relative or absolute. If path is absolute, path is converted to relative.
    ''' </summary>
    ''' <param name="p_path">Path to be checked and, if necessary, converted.</param>
    ''' <param name="p_isFile">True: Path is to a file. False (default): Path is to a directory.</param>
    ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    ''' <param name="p_referencePath">Path to which the conversion is related. This is an absolute path.</param>
    ''' <param name="p_cleanPath">True: The path will be trimmed of white space, leading and ending "\", and all "\\".</param>
    ''' <remarks></remarks>
    Public Shared Function RelativePath(ByRef p_path As String,
                                        Optional ByVal p_isFile As Boolean = False,
                                        Optional ByVal p_relativeToProgram As String = "",
                                        Optional ByVal p_referencePath As String = "",
                                        Optional ByVal p_cleanPath As Boolean = True) As Boolean
        RelativePath = True

        If Not CheckPathRelative(p_path) Then
            If p_cleanPath Then
                TrimWhiteSpace(p_path)
                p_path = TrimPathSlash(p_path, True)
                CleanDoubleSlash(p_path)
            End If

            If String.IsNullOrEmpty(p_referencePath) Then p_referencePath = pathStartup()
            p_path = ConvertPathAbsoluteToRelative(p_referencePath, p_path, p_isFile, p_relativeToProgram)

            'Validate converted relative path
            RelativePath = ValidateRelativePathConversion(p_path, p_referencePath, p_relativeToProgram)

        End If
    End Function

    ''' <summary>
    ''' Checks whether or not the relative path was successfully converted.
    ''' </summary>
    ''' <param name="p_path">Original path that was to be converted.</param>
    ''' <param name="p_referencePath">Path to which the conversion is related.</param>
    ''' <param name="p_relativeToProgram">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateRelativePathConversion(ByVal p_path As String,
                                                          ByVal p_referencePath As String,
                                                          ByVal p_relativeToProgram As String) As Boolean
        Dim checkpath As String
        Dim myRefPath As String = p_referencePath
        Dim myRelProgPath As String = p_relativeToProgram

        myRefPath = TrimPathSlash(myRefPath)
        myRelProgPath = TrimPathSlash(myRelProgPath, True)      'Slash removed at beginning & end

        'Reformulate currDir with the program relative offset
        If Not String.IsNullOrEmpty(myRelProgPath) Then
            checkpath = myRefPath & "\" & myRelProgPath
        Else
            checkpath = myRefPath
        End If

        If checkpath = p_path Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Browsing-related"
    ''' <summary>
    ''' Obtains path to the CSiTester.EXE file, not including the file name.
    ''' </summary>
    ''' <returns>Path to the CSiTester.EXE file, not including the file name.</returns>
    ''' <remarks></remarks>
    Public Shared Function pathStartup() As String
        Return System.Windows.Forms.Application.StartupPath
    End Function

    ''' <summary>
    ''' Sets starting directory to a folder or file, accounting for relative vs. absolute paths, and whether the path is valid. Assigns a default if path is not valid.
    ''' </summary>
    ''' <param name="p_path">Path to be checked.</param>
    ''' <param name="p_defaultRelPath">Default path is to the program. Optional: For the conditional default path, add any additional path to a subdirectory of the program.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetCurrDir(ByRef p_path As String,
                                 Optional ByVal p_defaultRelPath As String = "")
        'Make adjustments if path is relative
        If Not String.IsNullOrEmpty(p_path) Then            'If value exists, change to absolute path if it is a relative path
            If CheckPathRelative(p_path) Then
                If Not GetChar(p_path, 1) = "\" Then p_path = "\" & p_path 'Ensures relative paths start with a "\" for appending to absolute path
                p_path = ConvertPathRelativeToAbsolute(pathStartup(), p_path)
            End If
        End If

        'Ensures default relative path extensions start with a "\" for appending to absolute path
        If Not String.IsNullOrEmpty(p_defaultRelPath) Then
            If Not GetChar(p_defaultRelPath, 1) = "\" Then p_defaultRelPath = "\" & p_defaultRelPath
        End If

        'Sets default relative location if path is empty
        If String.IsNullOrEmpty(p_path) Then
            p_path = pathStartup() & p_defaultRelPath
            If Not DirectoryExists(p_path) Then p_path = pathStartup()
            Exit Sub
        End If

        'Sets default relative location if path does not exist
        If Not FileExists(p_path) Then
            If Not DirectoryExists(p_path) Then
                p_path = pathStartup() & p_defaultRelPath
                If Not DirectoryExists(p_path) Then p_path = pathStartup()
            End If
        Else
            p_path = GetPathDirectoryStub(p_path)                           'Removes the filename from the path so that the path is to a directory
        End If
    End Sub

    ''' <summary>
    ''' Relative folder location is set, with a folder to be generated if absent
    ''' </summary>
    ''' <param name="p_name">Name of folder on the next level relative to the program location</param>
    ''' <remarks></remarks>
    Public Shared Sub SetDefaultPath(ByVal p_name As String)
        path = pathStartup() & "\" & p_name
    End Sub


    ''' <summary>
    ''' Browses for a single file and updates the file path. Returns 'False' if canceled.
    ''' </summary>
    ''' <param name="p_path">Variable to write the file path to.</param>
    ''' <param name="p_dirPath">Starting directory. Do not include filename.</param>
    ''' <param name="p_label">Single label that describes the file type(s) provided.</param>
    ''' <param name="p_fileTypes">List of file types to find. Multiple file types will apply to the same single label.</param>
    ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    Public Shared Function BrowseForFile(ByRef p_path As String,
                                         Optional ByVal p_dirPath As String = "",
                                         Optional ByVal p_label As String = "",
                                         Optional ByVal p_fileTypes As List(Of String) = Nothing) As Boolean
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        Dim result? As Boolean

        dlg = BrowseForFilesComponent(result, p_dirPath, p_label, p_fileTypes)

        ' Process open file dialog box results 
        If result = True Then
            p_path = dlg.FileName
            BrowseForFile = True
        Else
            'myPath = ""
            BrowseForFile = False
        End If

        dlg = Nothing
    End Function

    ''' <summary>
    ''' Browses for multiple files and updates the file paths list provided. Returns 'False' if canceled.
    ''' </summary>
    ''' <param name="p_paths">List to populate with one or more selected file paths.</param>
    ''' <param name="p_dirPath">Starting directory. Do not include filename.</param>
    ''' <param name="p_label">Single label that describes the file type(s) provided.</param>
    ''' <param name="p_fileTypes">List of file types to find. Multiple file types will apply to the same single label.</param>
    ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    Public Shared Function BrowseForFiles(ByRef p_paths As List(Of String),
                                          Optional ByVal p_dirPath As String = "",
                                          Optional ByVal p_label As String = "",
                                          Optional ByVal p_fileTypes As List(Of String) = Nothing) As Boolean
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        Dim result? As Boolean

        dlg = BrowseForFilesComponent(result, p_dirPath, p_label, p_fileTypes, True)

        ' Process open file dialog box results 
        If result = True Then
            For Each fileName As String In dlg.FileNames
                p_paths.Add(fileName)
            Next
            BrowseForFiles = True
        Else
            BrowseForFiles = False
        End If

        dlg = Nothing
    End Function

    ''' <summary>
    ''' Browses for a file or files.
    ''' </summary>
    ''' <param name="p_result">Returns true if one or more files is selected, and false if the form is canceled.</param>
    ''' <param name="p_dirPath">Starting directory. Do not include filename.</param>
    ''' <param name="p_label">Single label that describes the file type(s) provided.</param>
    ''' <param name="p_fileTypes">List of file types to find. Multiple file types will apply to the same single label.</param>
    ''' <param name="p_multiSelect">If true, the user can select multiple files, and multiple file paths will be returned.</param>
    ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    Public Shared Function BrowseForFilesComponent(ByRef p_result? As Boolean,
                                                   Optional ByVal p_dirPath As String = "",
                                                   Optional ByVal p_label As String = "",
                                                   Optional ByVal p_fileTypes As List(Of String) = Nothing,
                                                   Optional ByVal p_multiSelect As Boolean = False) As Microsoft.Win32.OpenFileDialog
        ' Configure open file dialog box 
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        Dim myFilter As String = ""
        'Dim myFileTypes As String = ""

        'Clean starting path
        CleanDoubleSlash(p_dirPath)

        'Create filter property
        If p_fileTypes Is Nothing Then                                           'Show all files
            If String.IsNullOrEmpty(p_label) Then
                myFilter = "All Files|*.*"
            Else
                myFilter = p_label & "|*.*"
            End If
        Else                                                                'Filter files by extension 
            'TODO: This was not working for multiple files
            'myFileTypes = ConcatenateListToMessage(fTypes, ";", True, "*.")
            'If String.IsNullOrEmpty(label) Then
            'myFilter = "Files (" & myFileTypes & ")|" & myFileTypes
            'Else
            ' myFilter = label & "|" & myFileTypes
            Dim secondaryType As Boolean = False

            If String.IsNullOrEmpty(p_label) Then p_label = "Files"
            For Each fileType As String In p_fileTypes
                If secondaryType Then myFilter = myFilter & "|"
                myFilter = myFilter & p_label & " (*." & fileType & ")|*." & fileType

                secondaryType = True
            Next
            'End If
        End If

        'dlg.FileName = "Document" ' Default file name 
        If (p_fileTypes IsNot Nothing AndAlso p_fileTypes.Count > 0) Then dlg.DefaultExt = "." & p_fileTypes(0) ' Default file extension 
        dlg.FilterIndex = 1
        dlg.Filter = myFilter                       ' Filter files by extension 
        dlg.RestoreDirectory = True
        dlg.InitialDirectory = p_dirPath
        dlg.Multiselect = p_multiSelect
        If p_multiSelect = False Then
            dlg.Title = "Browse for File"
        Else
            dlg.Title = "Browse for Files"
        End If


        'Validate User Manual Entries
        dlg.CheckFileExists = True
        dlg.CheckPathExists = True

        ' Show open file dialog box 
        Dim myResult? As Boolean = dlg.ShowDialog()

        p_result = myResult

        Return dlg
    End Function

    'TODO: Change code to WinForms
    ''' <summary>
    ''' User selects a folder location. Folder location is stored as a string.
    ''' </summary>
    ''' <param name="p_description">Description to be provided in the file browser.</param>
    ''' <param name="p_startupDir">Default starting directory.</param>
    ''' <param name="p_path">Path to set as the directory navigated to in the browser.</param>
    ''' <param name="p_showNewFolderButton">True, a button appears for creating new folders. False is default.</param>
    ''' <remarks></remarks>
    Public Shared Sub BrowseForFolder(Optional ByVal p_description As String = "",
                                      Optional ByVal p_startupDir As String = "",
                                      Optional ByRef p_path As String = "",
                                      Optional ByVal p_showNewFolderButton As Boolean = False)
        Dim diaFolder As New System.Windows.Forms.FolderBrowserDialog

        'Clean starting path
        CleanDoubleSlash(p_startupDir)

        diaFolder.Description = p_description
        diaFolder.SelectedPath = p_startupDir
        diaFolder.ShowNewFolderButton = p_showNewFolderButton


        ' Open the file dialog
        If diaFolder.ShowDialog = 1 Then    'Path selected
            path = diaFolder.SelectedPath
            p_path = path
        Else                                'Path not selected
            path = String.Empty
        End If

        diaFolder = Nothing

    End Sub


    ' ''' <summary>
    ' ''' Browses for a single file and updates the file path. Returns 'False' if canceled.
    ' ''' </summary>
    ' ''' <param name="myPath">Variable to write the file path to.</param>
    ' ''' <param name="ftype">File type to find.</param>
    ' ''' <param name="dirPath">Optional starting directory. Do not include filename.</param>
    ' ''' <returns>True if filename is chosen and valid. False otherwise.</returns>
    ' ''' <remarks>dirPath currently is not working with this dialog.</remarks>
    'Function BrowseForFile(ByRef myPath As String, ByVal ftype As String, Optional ByVal dirPath As String = "") As Boolean
    '    ' Configure open file dialog box 
    '    Dim dlg As New Microsoft.Win32.OpenFileDialog()
    '    'dlg.FileName = "Document" ' Default file name 
    '    dlg.DefaultExt = "." & ftype ' Default file extension 
    '    dlg.FilterIndex = 1
    '    dlg.Filter = "Files (*." & ftype & ")| *." & ftype ' Filter files by extension 
    '    dlg.RestoreDirectory = True
    '    dlg.InitialDirectory = dirPath
    '    dlg.Multiselect = False
    '    dlg.Title = "Browse for File"

    '    'Validate User Manual Entries
    '    dlg.CheckFileExists = True
    '    dlg.CheckPathExists = True

    '    ' Show open file dialog box 
    '    Dim result? As Boolean = dlg.ShowDialog()

    '    ' Process open file dialog box results 
    '    If result = True Then
    '        myPath = dlg.FileName
    '        BrowseForFile = True
    '    Else
    '        'myPath = ""
    '        BrowseForFile = False
    '    End If

    '    dlg = Nothing

    'End Function
#End Region

#Region "Check String Properties"
    ''' <summary>
    ''' Determines if two strings are the same, accounting for capitalization.
    ''' </summary>
    ''' <param name="p_string1">First string to compare.</param>
    ''' <param name="p_string2">Second string to compare.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StringsMatch(ByVal p_string1 As String,
                                        ByVal p_string2 As String,
                                        Optional ByVal p_caseSensitive As Boolean = False) As Boolean
        If ((String.IsNullOrWhiteSpace(p_string1) AndAlso
             String.IsNullOrWhiteSpace(p_string2)) OrElse
            String.Compare(p_string1, p_string2, ignoreCase:=Not p_caseSensitive) = 0) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Searches a string and determines if a substring exists.
    ''' </summary>
    ''' <param name="p_string">String to be searched</param>
    ''' <param name="p_subString">Substring segment to search for</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns>True: Substring found within string</returns>
    ''' <remarks></remarks>
    Public Shared Function StringExistInName(ByVal p_string As String,
                                             ByVal p_subString As String,
                                             Optional ByVal p_caseSensitive As Boolean = False) As Boolean
        StringExistInName = False

        If String.IsNullOrEmpty(p_subString) Then Exit Function
        If Len(p_subString) > Len(p_string) Then Exit Function

        For i = 1 To Len(p_string) - Len(p_subString) + 1

            If StringsMatch(Mid(p_string, i, Len(p_subString)), p_subString, p_caseSensitive) Then
                StringExistInName = True
                Exit For
            End If
        Next
    End Function

    ''' <summary>
    ''' Checks name provided and determines if name includes file extension by checking for the existing of a "." at the specified spacing from the end.  Returns true if extension is found.
    ''' </summary>
    ''' <param name="p_name">Name to check, as a string.</param>
    ''' <param name="p_extensionLength">Number of characters in the file extension after the period. e.g. sdb = 3.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function NameIncludesFileExtension(ByVal p_name As String,
                                                    Optional ByVal p_extensionLength As Integer = 3) As Boolean
        NameIncludesFileExtension = False

        If String.IsNullOrEmpty(p_name) Then Exit Function

        If Mid(Right(p_name, p_extensionLength + 1), 1, 1) = "." Then NameIncludesFileExtension = True

    End Function

    ''' <summary>
    ''' Returns true or false if a match is found of either the file name or extension to the full filename and extension
    ''' </summary>
    ''' <param name="p_fileNameSource">Full file name with extension.</param>
    ''' <param name="p_fileName">File name to compare.</param>
    ''' <param name="p_fileExtension">File extension to compare.</param>
    ''' <param name="p_partialNameMatch">Optional: True: File will be deleted if any part of the name provided matches the file name. False(default): File will only be deleted if by the name criteria if there is an exact match.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileNameExtensionMatch(ByVal p_fileNameSource As String,
                                                  Optional ByVal p_fileName As String = "",
                                                  Optional ByVal p_fileExtension As String = "",
                                                  Optional ByVal p_partialNameMatch As Boolean = False) As Boolean
        Dim nameMatch As Boolean = False

        'Get only the file extension without the period. This is for a normalized comparison, so it won't matter if the user writes *., ., or merely the extension
        Dim fileExtensionTemp As String = GetSuffix(p_fileExtension, ".")

        'Get the file extension supplied with the file name
        Dim fileExtensionSourceTemp As String = GetSuffix(p_fileNameSource, ".")

        'Get the supplied file name without any extension. This is for a normalized comparison, so capitalization won't matter
        Dim fileNameSourceTemp As String = GetPathFileName(GetSuffix(p_fileNameSource, "\"), True)

        'Normalize the provided name
        Dim fileNameTemp As String = p_fileName

        'Checks for name matching based on full file name, or partial (if specified)
        If p_partialNameMatch Then
            If StringExistInName(fileNameSourceTemp, fileNameTemp) Then
                nameMatch = True
            End If
        Else
            If StringsMatch(fileNameSourceTemp, fileNameTemp) Then
                nameMatch = True
            End If
        End If

        'Based on supplied file name & file extension, sets function value
        If (String.IsNullOrEmpty(p_fileName) AndAlso
            String.IsNullOrEmpty(p_fileExtension)) Then
            Return True
        ElseIf (Not String.IsNullOrEmpty(p_fileName) AndAlso
                Not String.IsNullOrEmpty(p_fileExtension) AndAlso
                nameMatch) Then
            If StringsMatch(fileExtensionSourceTemp, fileExtensionTemp) Then
                Return True
            ElseIf GenericExtensionMatch(fileExtensionTemp, fileExtensionSourceTemp) Then       'Check generic format, such as *.y???
                Return True
            End If
        Else
            If (Not String.IsNullOrEmpty(p_fileName) AndAlso nameMatch) Then
                Return True
            ElseIf Not String.IsNullOrEmpty(p_fileExtension) Then
                If StringsMatch(fileExtensionSourceTemp, fileExtensionTemp) Then
                    Return True
                ElseIf GenericExtensionMatch(fileExtensionTemp, fileExtensionSourceTemp) Then       'Check generic format, such as *.y???
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Checks if the generic file extension, such as *.k??? for any value after k of 3 spaces, matches. Returns true if a match is found.
    ''' </summary>
    ''' <param name="p_fileExtension">Generic file extention to search for.</param>
    ''' <param name="p_fileExtensionSource">File extension of the file being checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GenericExtensionMatch(ByVal p_fileExtension As String,
                                                 ByVal p_fileExtensionSource As String) As Boolean
        GenericExtensionMatch = False

        If Right(p_fileExtension, 1) = "?" Then
            If StringsMatch(Left(p_fileExtension, 1), Left(p_fileExtensionSource, 1)) Then
                If Len(p_fileExtension) = Len(p_fileExtensionSource) Then
                    GenericExtensionMatch = True
                End If
            End If
        End If

    End Function
#End Region

#Region "Gets-Sets Portion of a String"
    ''' <summary>
    ''' Returns the filename without a file extension. 
    ''' This works regardless of whether or not the provided filename has an extension.
    ''' </summary>
    ''' <param name="p_fileName">File name to return without an extension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileNameWithoutExtension(ByVal p_fileName As String) As String
        If NameIncludesFileExtension(p_fileName, 3) Then
            Return GetPathFileName(p_fileName, True)
        Else
            Return p_fileName
        End If
    End Function

    ''' <summary>
    ''' Determines whether or not the file path provided points to a file with no extension.
    ''' </summary>
    ''' <param name="p_path">Path to a file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileHasNoExtension(ByVal p_path As String) As Boolean
        If StringsMatch(GetPrefix(p_path, "."), p_path) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Gets the file name. If no filename exists, the last folder name in the path is returned.
    ''' </summary>
    ''' <param name="p_path">Path to file or folder. Can be relative or absolute.</param>
    ''' <param name="p_noExtension">Optional: True: Returns only the name without any file extension. Will not work for files with no extension, but containing a "." in the filename. 
    ''' False(default): File extension, if present, is not removed.</param>
    ''' <returns>File name or folder name</returns>
    ''' <remarks>Currently cannot auto determine if file. This due to the two possible cases: 1. Filename w/o extension listed, 2. Directories with "." in the names</remarks>
    Public Shared Function GetPathFileName(ByVal p_path As String,
                                            Optional ByVal p_noExtension As Boolean = False) As String
        Dim pathFileName As String
        Dim extension As String

        'Gets the file name from the path
        pathFileName = GetSuffix(p_path, "\")

        'Removes the file extension, if specified
        If p_noExtension Then
            extension = GetSuffix(pathFileName, ".")
            pathFileName = FilterStringFromName(pathFileName, "." & extension, True, False)
        End If

        Return pathFileName
    End Function

    ''' <summary>
    ''' Gets the path to the directory of the filepath provided. If no filename exists, the path is returned minus the lowest directory.
    ''' </summary>
    ''' <param name="p_path">Path to be checked for the directory.</param>
    ''' <returns>Path to a directory.</returns>
    ''' <remarks>Currently cannot auto determine if file is in path. This due to the two possible cases: 1. Filename w/o extension listed, 2. Directories with "." in the names</remarks>
    Public Shared Function GetPathDirectoryStub(ByVal p_path As String) As String
        Dim i As Integer

        i = Len(p_path) - (Len(p_path) - InStrRev(p_path, "\")) - 1
        If i < 0 Then
            Return p_path
        Else
            Return Left(p_path, i)
        End If
    End Function

    ''' <summary>
    ''' Returns the path to a directory at a number of directories specified above the lowest directory in the path supplied.
    ''' </summary>
    ''' <param name="p_path">Path to retrieve path directory component from. If this is to a file, the directory containing the file is the lowest one returned (numDir=0).</param>
    ''' <param name="p_numberOfDirectories">Number of directories to move up in the path supplied. For numDir=0, if the path is to a directory, the same path is returned, and if the path is to a file, the path to the directory containing the file is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPathDirectorySubStub(ByVal p_path As String,
                                     ByVal p_numberOfDirectories As Integer) As String
        Dim tempPath As String = p_path

        If FileExists(p_path) Then tempPath = GetPathDirectoryStub(p_path)

        If p_numberOfDirectories <= 0 Then
            Return tempPath
        Else
            For i = 1 To p_numberOfDirectories
                tempPath = FilterStringFromName(tempPath, "\" & GetSuffix(tempPath, "\"), True, False, True)
            Next

            Return tempPath
        End If
    End Function

    ''' <summary>
    ''' Gets the last part of a string after the last occurrence of a designated character. 
    ''' Returns name if the character is not found.
    ''' </summary>
    ''' <param name="p_name">String to be truncated. Can be a single word or a sentence.</param>
    ''' <param name="p_character">Character to search for. Function returns what is left of string after the last occurrence of this character</param>
    ''' <returns>Function returns what is left of string after the last occurrence of this character</returns>
    ''' <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
    Public Shared Function GetSuffix(ByVal p_name As String,
                                    ByVal p_character As String) As String
        Dim i As Integer

        If String.IsNullOrEmpty(p_name) Then Return p_name

        If p_character.Length > 1 Then p_character = Right(p_character, 1)

        i = Len(p_name) - InStrRev(p_name, p_character)
        If i < 1 Then
            Return p_name
        Else
            Return Right(p_name, i)
        End If
    End Function

    ''' <summary>
    ''' Gets the first part of a string before the first occurrence of a designated character.
    ''' Returns name if the character is not found.
    ''' </summary>
    ''' <param name="p_name">String to be truncated. Can be a single word or a sentence</param>
    ''' <param name="p_character">Character to search for. Function returns what is left of string before the first occurence of this character</param>
    ''' <returns>Function returns what is left of string before the first occurence of this character</returns>
    ''' <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
    Public Shared Function GetPrefix(ByVal p_name As String,
                       ByVal p_character As String) As String
        Dim i As Integer
        Dim prefix As String

        If String.IsNullOrEmpty(p_name) Then Return p_name

        If p_character.Length > 1 Then p_character = Left(p_character, 1)

        i = InStr(p_name, p_character) - 1
        If i < 1 Then
            Return p_name
        Else
            prefix = Left(p_name, i)
            Return prefix
        End If
    End Function

    ''' <summary>
    ''' Finds a given substring within a string and returns the prefix and/or suffix of the remaining string. If the substring is not found, the original string is returned.
    ''' </summary>
    ''' <param name="p_oldString">String to be filtered.</param>
    ''' <param name="p_filterString">String to filter out.</param>
    ''' <param name="p_retainPrefix">True: Retain the portion of the string before the filter string.</param>
    ''' <param name="p_retainSuffix">True: Retain the portion of the string after the filter string.</param>
    ''' <param name="p_endDirectory">If true, then a match is only valid if the filtered string is the last directory in a path string. Used to prevent false positives higher up the path hierarchy.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterStringFromName(ByVal p_oldString As String,
                                  ByVal p_filterString As String,
                                  ByVal p_retainPrefix As Boolean,
                                  ByVal p_retainSuffix As Boolean,
                                  Optional ByVal p_endDirectory As Boolean = False,
                                  Optional ByVal p_caseSensitive As Boolean = False) As String
        Dim prefix As String = ""
        Dim suffix As String = ""

        'If unsuccessful, return the original string
        FilterStringFromName = p_oldString

        'Don't filter if the filter string is longer than the original string.
        If Len(p_oldString) < Len(p_filterString) Then Exit Function

        'Find segment with filtered string and strip accordingly
        For i = 1 To Len(p_oldString) - Len(p_filterString) + 1
            If StringsMatch(Mid(p_oldString, i, Len(p_filterString)), p_filterString, p_caseSensitive) Then  'Old String was found in name. Replace with new string.
                'If filtering out the end of a directory name, the following check avoids finding a match farther up the path hierarchy
                If p_endDirectory Then
                    If Not i = Len(p_oldString) - Len(p_filterString) + 1 Then
                        Continue For
                    End If
                End If

                If p_retainPrefix Then prefix = Left(p_oldString, i - 1)
                If p_retainSuffix Then suffix = Right(p_oldString, Len(p_oldString) - (i + Len(p_filterString) - 1))
                FilterStringFromName = prefix & suffix
                Exit Function
            End If
        Next

    End Function

    ''' <summary>
    ''' Replaces a substring in a string. Returns the new string.
    ''' </summary>
    ''' <param name="p_oldString">String to be searched</param>
    ''' <param name="p_oldSubString">Substring segment to search for and be replaced</param>
    ''' <param name="p_newSubString">Substring segment to replace</param>
    ''' <param name="p_suppressWarnings">If true, no warning is given if the old substring is not found in the old string.</param>
    ''' <param name="p_clearOK">If true, then if the old substring equals the old string, the entire string is replaced.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns>Returns the new string.</returns>
    ''' <remarks></remarks>
    Public Shared Function ReplaceStringInName(ByVal p_oldString As String,
                                 ByVal p_oldSubString As String,
                                 ByVal p_newSubString As String,
                                 Optional ByVal p_suppressWarnings As Boolean = False,
                                 Optional ByVal p_clearOK As Boolean = False,
                                 Optional ByVal p_caseSensitive As Boolean = False) As String
        Dim OldStringFound As Boolean = False
        ReplaceStringInName = p_oldString

        Try
            If p_oldSubString = p_oldString Then
                If p_clearOK Then ReplaceStringInName = p_newSubString
                Exit Function
            Else
                For i = 1 To Len(p_oldString) - Len(p_oldSubString) + 1
                    If StringsMatch(Mid(p_oldString, i, Len(p_oldSubString)), p_oldSubString, p_caseSensitive) Then  'Old String was found in name. Replace with new string.
                        ReplaceStringInName = Left(p_oldString, i - 1) & p_newSubString & Right(p_oldString, Len(p_oldString) - i + 1 - Len(p_oldSubString))
                        OldStringFound = True
                    End If
                Next
            End If

            If (Not OldStringFound AndAlso Not p_suppressWarnings) Then Throw New ArgumentException("String not found within name. Name will remain unchanged")
        Catch exArg As ArgumentException
            If Not p_suppressWarnings Then csiLogger.ArgumentExceptionAction(exArg)
        End Try
    End Function

    ''' <summary>
    ''' Returns only the portions of the string that are numeric. All non-numeric characters are filtered out.
    ''' </summary>
    ''' <param name="p_string">String to filter.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterNumeric(ByVal p_string As String,
                           Optional ByVal p_keepSpaces As Boolean = False) As String
        Dim filteredString As String = ""

        For i = 1 To p_string.Length
            Dim currentCharacter As String = Mid(p_string, i, 1)
            If (IsNumeric(currentCharacter) OrElse
                (currentCharacter = " " AndAlso p_keepSpaces)) Then

                filteredString &= currentCharacter
            End If
        Next

        Return filteredString
    End Function

    ''' <summary>
    ''' Get the folder source from the files selected if a folder source was not specified.
    ''' </summary>
    ''' <param name="p_folderSource">File path to the source folder containing the examples to be worked with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetFolderSource(ByVal p_folderSource As String,
                                            ByVal p_filePaths As cPathsExamples) As String
        If String.IsNullOrEmpty(p_folderSource) Then
            If String.IsNullOrEmpty(p_filePaths.folderSource) Then p_filePaths.GetSourceFolder()
            p_folderSource = p_filePaths.folderSource
        End If

        Return p_folderSource
    End Function

    ''' <summary>
    ''' Pluralizes a word, or not, based on a number, and returns the combination of the number &amp; word.
    ''' </summary>
    ''' <param name="p_number">Number to base pluralization on.</param>
    ''' <param name="p_word">Word to append to the number, and pluralize if necessary.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PluralizeString(ByVal p_number As Double,
                                           ByVal p_word As String) As String
        If p_number = 1 Then
            PluralizeString = p_number & " " & p_word
        Else
            PluralizeString = p_number & " " & p_word & "s"
        End If
    End Function

    ''' <summary>
    ''' Takes a list and concatenates it into a single string message with specified joiners.
    ''' </summary>
    ''' <param name="p_strings">List of items to concatenate.</param>
    ''' <param name="p_joiner">Joining word to use if there is more than one entry, such as 'and' or 'or'.</param>
    ''' <param name="p_useJoinerAlways">True: Rhe joiner is used in a list of two. Else, the joiner is not used in a list of two.</param>
    ''' <param name="p_prefix">This is appended to the beginning of each list item. Example "Mr." or "*.".</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConcatenateListToMessage(ByRef p_strings As List(Of String),
                                                    ByVal p_joiner As String,
                                                    Optional ByVal p_useJoinerAlways As Boolean = False,
                                                    Optional ByVal p_prefix As String = "") As String
        Dim myStringOfLists As String = ""

        For i = 0 To p_strings.Count - 1
            If i = 0 Then
                myStringOfLists = p_prefix & p_strings(i)
            ElseIf (p_strings.Count = 2 AndAlso Not p_useJoinerAlways) OrElse
                p_useJoinerAlways Then

                myStringOfLists = myStringOfLists & " " & p_joiner & " " & p_prefix & p_strings(i)
            Else
                myStringOfLists = myStringOfLists & ", " & p_joiner & " " & p_prefix & p_strings(i)
            End If
        Next

        Return myStringOfLists
    End Function

    ''' <summary>
    ''' Replaces the first instance of the string being searched for.
    ''' </summary>
    ''' <param name="p_text">Text to search within.</param>
    ''' <param name="p_search">String to search for.</param>
    ''' <param name="p_replace">String to replace the searched string with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReplaceFirst(ByVal p_text As String,
                                 ByVal p_search As String,
                                 ByVal p_replace As String) As String
        Dim position As Integer = p_text.IndexOf(p_search)
        If (position < 0) Then
            Return p_text
        Else
            Return p_text.Substring(0, position) & p_replace & p_text.Substring(position + p_search.Length)
        End If
    End Function
#End Region

#Region "Adjusting-Cleaning Strings"
    ''' <summary>
    ''' Removes any trailing "\" characters from a file path. Works for multiple ending slashes.
    ''' </summary>
    ''' <param name="p_dirPath">Directory Path.</param>
    ''' <param name="p_trimStart">If true, any slash at the start of the path will also be trimmed.</param>
    ''' <param name="p_trimEnd">If false, the slash at the end of the path will not be trimmed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TrimPathSlash(ByRef p_dirPath As String,
                                         Optional ByVal p_trimStart As Boolean = False,
                                         Optional ByVal p_trimEnd As Boolean = True) As String
        'Removes any trailing "\" characters from a file path
        'Works for multiple ending slashes
        Dim searchChar As Boolean
        Dim i As Integer

        If String.IsNullOrEmpty(p_dirPath) Then Return ""

        searchChar = True
        i = 1
        TrimPathSlash = p_dirPath

        'For case of path at current location, where the address is only a single "\"
        If Len(TrimPathSlash) = 1 AndAlso Right(TrimPathSlash, 1) = "\" Then Return TrimPathSlash

        'For case of only moving one directory up
        'If Mid(trimPathSlash, Len(trimPathSlash) - 2, 3) = "..\" Then Return trimPathSlash

        'All other cases
        'Trim preceding slash if specified
        If p_trimStart Then
            If Left(TrimPathSlash, 1) = "\" Then TrimPathSlash = Right(TrimPathSlash, Len(TrimPathSlash) - 1)
        End If
        'Trim trailing slash (done by default)
        If p_trimEnd Then
            While searchChar    'Loops done in case the string has "\\" or similar at the end
                If Right(TrimPathSlash, i) = "\" Then TrimPathSlash = Left(p_dirPath, Len(p_dirPath) - 1)
                If Right(TrimPathSlash, i) <> "\" Then Return TrimPathSlash
                i = i + 1
            End While
        End If
    End Function

    ''' <summary>
    ''' Adds trailing "\" character to end of a file path, if one does not exist. Ensures that only one exists
    ''' </summary>
    ''' <param name="p_dirPath">Directory Path</param>
    ''' <param name="p_addStart">Optional: If true, adds slash to the start of the path.</param>
    ''' <param name="p_addEnd">Optional: If false, slash will not be addeed to the end of the path.</param>
    ''' <returns>File path with a single trailing "\" character at the end</returns>
    ''' <remarks></remarks>
    Public Shared Function AddPathSlash(ByRef p_dirPath As String,
                                        Optional ByVal p_addStart As Boolean = False,
                                        Optional ByVal p_addEnd As Boolean = True) As String
        'Ensures there are no extra slashes
        If p_addStart Then
            AddPathSlash = TrimPathSlash(p_dirPath, True)
        Else
            AddPathSlash = TrimPathSlash(p_dirPath)
        End If

        'Add slashes
        If p_addStart Then AddPathSlash = "\" & AddPathSlash
        If p_addEnd Then AddPathSlash = AddPathSlash & "\" 'Done by default

    End Function

    ''' <summary>
    ''' Takes any string and eliminates all white space before the first character and after the last character. Does nothing if string is empty.
    ''' </summary>
    ''' <param name="p_stringTrim">String to be trimmed of white spaces</param>
    ''' <remarks></remarks>
    Public Shared Sub TrimWhiteSpace(ByRef p_stringTrim As String)
        If String.IsNullOrEmpty(p_stringTrim) Then Exit Sub

        'Removes preceding white spaces
        While Left(p_stringTrim, 1) = " "
            p_stringTrim = Right(p_stringTrim, Len(p_stringTrim) - 1)
        End While

        'Removes ending white spaces
        While Right(p_stringTrim, 1) = " "
            p_stringTrim = Left(p_stringTrim, Len(p_stringTrim) - 1)
        End While
    End Sub

    ''' <summary>
    ''' Trims single quotes "'" from both ends of a string.
    ''' </summary>
    ''' <param name="p_stringTrim">String to trim the quotes from.</param>
    ''' <param name="P_trimLeft">False: the left side of the string will not have any existing quotes trimmed.</param>
    ''' <param name="p_trimRight">False: the right side of the string will not have any existing quotes trimmed.</param>
    ''' <remarks></remarks>
    Public Shared Sub TrimQuotesSingle(ByRef p_stringTrim As String,
                                       Optional ByVal P_trimLeft As Boolean = True,
                                       Optional ByVal p_trimRight As Boolean = True)
        If P_trimLeft Then
            If Left(p_stringTrim, 1) = "'" Then p_stringTrim = Right(p_stringTrim, Len(p_stringTrim) - 1)
        End If
        If p_trimRight Then
            If Right(p_stringTrim, 1) = "'" Then p_stringTrim = Left(p_stringTrim, Len(p_stringTrim) - 1)
        End If
    End Sub

    ''' <summary>
    ''' Ensures that a file extension is returned in the desired format, either with or without a period.
    ''' </summary>
    ''' <param name="p_fileExtension">File extension to check, and modify if needed.</param>
    ''' <param name="p_noPeriod">If true, then the returned extension will have no period. If false, then the returned extension will have a period.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileExtensionCleanComplete(ByVal p_fileExtension As String,
                                                      ByVal p_noPeriod As Boolean) As String
        If p_noPeriod Then
            If Left(p_fileExtension, 1) = "." Then p_fileExtension = Right(p_fileExtension, Len(p_fileExtension) - 1)
        Else
            If Not Left(p_fileExtension, 1) = "." Then p_fileExtension = "." & p_fileExtension
        End If

        Return p_fileExtension
    End Function
#End Region

#Region "Lists"
    'TODO: Next two functions could be combined ...
    ''' <summary>
    ''' Creates a list of paths of a specified file type in the source folder specified.
    ''' </summary>
    ''' <param name="p_sourceFolderName">Path to the highest level folder to check.</param>
    ''' <param name="p_includeSubfolders">True = subfolders are also checked for XML files.</param>
    ''' <param name="p_fileExtension">File type extension, such as ".xml".</param>
    ''' <param name="p_pathList">Collection of path names that is filled by the routine.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    Public Shared Sub CreateListOfFilesInFolder(ByVal p_sourceFolderName As String,
                                                ByVal p_includeSubfolders As Boolean,
                                                ByVal p_fileExtension As String,
                                                ByRef p_pathList As ObservableCollection(Of String),
                                                Optional ByVal p_caseSensitive As Boolean = False)
        Dim FSO As New FileSystemObject
        Dim SourceFolder As Folder
        Dim SubFolder As Folder
        Dim myFile As File

        Try
            SourceFolder = FSO.GetFolder(p_sourceFolderName)
            For Each FileItem As File In SourceFolder.Files
                'display file properties
                myFile = FileItem
                If StringsMatch(Right(myFile.Name, Len(p_fileExtension)), p_fileExtension, p_caseSensitive) Then
                    p_pathList.Add(myFile.Path)
                End If
            Next FileItem

            If p_includeSubfolders Then
                For Each SubFolder In SourceFolder.SubFolders
                    CreateListOfFilesInFolder(SubFolder.Path, True, p_fileExtension, p_pathList)
                Next SubFolder
            End If
        Catch ex As Exception

        End Try

        myFile = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    ''' <summary>
    ''' Creates a list of paths to all files in a directory and sub-directories (if desired) of a given file name, or type, or all files. 
    ''' If both file name and type are specified, only files matching both criteria will be listed. 
    ''' If neither file name nor type are specified, all files will be listed.
    ''' </summary>
    ''' <param name="p_path">Path to the parent directory</param>
    ''' <param name="p_includeSubFolders">True: Function will check within all subfolders of the parent directory. False: Function will only check directory specified.</param>
    ''' <param name="p_fileName">Only files matching the name specified, but of any file type, will be deleted</param>
    ''' <param name="p_fileExtension">Only files with the same filetype will be deleted</param>
    ''' <param name="p_excludeFile">List returned will exclude files of the given filename.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFilePathsInDirectory(ByVal p_path As String,
                                                    ByVal p_includeSubFolders As Boolean,
                                                    Optional ByVal p_fileName As String = "",
                                                    Optional ByVal p_fileExtension As String = "",
                                                    Optional ByVal p_excludeFile As Boolean = False) As List(Of String)
        Dim FSO As New FileSystemObject
        Dim SourceFolder As Folder
        Dim SubFolder As Folder
        Dim FileItem As File
        Dim myPathList As New List(Of String)

        If Not DirectoryExists(p_path) Then Return Nothing

        SourceFolder = FSO.GetFolder(p_path)

        If Not p_excludeFile Then
            'Add the file path to the list if it matches the specified criteria
            For Each FileItem In SourceFolder.Files
                If FileNameExtensionMatch(CStr(FileItem.Name), p_fileName, p_fileExtension) Then myPathList.Add(FileItem.Path)
            Next FileItem
        Else
            'Add any file path to the list so long as it does not match the specified criteria of file name & extension
            For Each FileItem In SourceFolder.Files
                If Not FileNameExtensionMatch(CStr(FileItem.Name), p_fileName, p_fileExtension) Then myPathList.Add(FileItem.Path)
            Next FileItem
        End If

        'Rescursion to check sub-folders
        If p_includeSubFolders Then
            For Each SubFolder In SourceFolder.SubFolders
                Dim myTempPathList As New List(Of String)
                myTempPathList = ListFilePathsInDirectory(SubFolder.Path, True, p_fileName, p_fileExtension)
                If myTempPathList IsNot Nothing Then
                    'Append the subdirectory list to the main path list being collected
                    For Each path As String In myTempPathList
                        myPathList.Add(path)
                    Next
                End If
            Next SubFolder
        End If

        ListFilePathsInDirectory = myPathList

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing
    End Function

    ''' <summary>
    ''' Creates a collection of filepath objects to all files in a directory and sub-directories (if desired) of a given file name, or type, or all files. 
    ''' If both file name and type are specified, only files matching both criteria will be listed. 
    ''' If neither file name nor type are specified, all files will be listed.
    ''' </summary>
    ''' <param name="p_path">Path to the parent directory.</param>
    ''' <param name="p_includeSubFolders">True: Function will check within all subfolders of the parent directory. False: Function will only check directory specified.</param>
    ''' <param name="p_fileName">Only files matching the name specified, but of any file type, will be deleted.</param>
    ''' <param name="p_fileExtension">Only files with the same filetype will be deleted.</param>
    ''' <param name="p_excludeFile">List returned will exclude files of the given filename.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFilePathObjsInDir(ByVal p_path As String,
                                                 ByVal p_includeSubFolders As Boolean,
                                                 Optional ByVal p_fileName As String = "",
                                                 Optional ByVal p_fileExtension As String = "",
                                                 Optional ByVal p_excludeFile As Boolean = False) As List(Of File)
        Dim FSO As New FileSystemObject
        Dim SourceFolder As Folder
        Dim SubFolder As Folder
        Dim FileItem As File
        Dim myFilesList As New List(Of File)

        If Not DirectoryExists(p_path) Then Return Nothing

        SourceFolder = FSO.GetFolder(p_path)

        If Not p_excludeFile Then
            'Add the file path to the list if it matches the specified criteria
            For Each FileItem In SourceFolder.Files
                If FileNameExtensionMatch(CStr(FileItem.Name), p_fileName, p_fileExtension) Then myFilesList.Add(FileItem)
            Next FileItem
        Else
            'Add any file path to the list so long as it does not match the specified criteria of file name & extension
            For Each FileItem In SourceFolder.Files
                If Not FileNameExtensionMatch(CStr(FileItem.Name), p_fileName, p_fileExtension) Then myFilesList.Add(FileItem)
            Next FileItem
        End If

        'Rescursion to check sub-folders
        If p_includeSubFolders Then
            For Each SubFolder In SourceFolder.SubFolders
                Dim myTempPathList As New List(Of File)
                myTempPathList = ListFilePathObjsInDir(SubFolder.Path, True, p_fileName, p_fileExtension)
                If myTempPathList IsNot Nothing Then
                    'Append the subdirectory list to the main path list being collected
                    For Each oFile As File In myTempPathList
                        myFilesList.Add(oFile)
                    Next
                End If
            Next SubFolder
        End If

        ListFilePathObjsInDir = myFilesList

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing
    End Function

    'See mFolders for directory list functions

#End Region

#Region "Misc"
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
            msgBoxMessageOptional = "    'No' will begin CSiTester with a default path. " & vbCrLf & "    "
        End If

        msgBoxMessage = "The currently specified " & p_programName & " program path is no longer valid. Please specify the location of program to be tested. " & vbCrLf & vbCrLf & msgBoxMessageOptional & "'Cancel' will exit the program."

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
            Select Case MessageBox.Show(msgBoxMessage, p_programName & " Program Path Invalid", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning)
                Case MessageBoxResult.Yes
                    myCsiTester.BrowseProgram(p_programPath)
                Case MessageBoxResult.No
                    MessageBox.Show("Path to program will be set as: " & vbCrLf & vbCrLf & p_programPath & vbCrLf & vbCrLf & "Please make sure to have a valid path specified before running any models.", _
                                    p_programName & " Default Path", MessageBoxButton.OK, MessageBoxImage.Warning)
                Case MessageBoxResult.Cancel
                    End
            End Select
        Else
            While Not programPathValid
                Select Case MessageBox.Show(msgBoxMessage, p_programName & " Program Path Invalid", MessageBoxButton.OKCancel, MessageBoxImage.Warning)
                    Case MessageBoxResult.OK
                        myCsiTester.BrowseProgram(p_programPath)
                    Case MessageBoxResult.Cancel
                        End
                End Select
                programPathValid = FileExists(p_programPath)
            End While
        End If
    End Sub
#End Region
End Class
