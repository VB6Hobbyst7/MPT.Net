Option Explicit On
Module paths

    Sub Path_Source()
        'A relative folder location is set, with a folder to be generated if absent, for "Models"
        'Folder location is stored as a string
        Call BrowseForFolder()
        'Update - Call Excel_Store_Value("Dummy", "Source", path)

    End Sub

    Sub Path_Models()
        'A relative folder location is set, with a folder to be generated if absent, for "Models"
        'Folder location is stored as a string
        Call SetDefaultPath("models")
        'Update - Call Excel_Store_Value("Dummy", "Destination", path)

    End Sub

    Sub Path_SDB()
        '=== Determines the Folder location & reports in Excel
        Call BrowseForFile("SDB")
        'Update - Call Excel_Store_Value("Dummy", "SDBFileSource", path)

    End Sub

    Sub Path_MapSourceCopy()
        '=== Determines the Folder location & reports in Excel
        Call BrowseForFolder()
        'Update - Call Excel_Store_Value("Dummy", "PathMapCopy", path)

    End Sub

    Sub Path_MapSource()
        '=== Determines the Folder location & reports in Excel
        Call BrowseForFolder()
        'Update -  Call Excel_Store_Value("Dummy", "PathMapSource", path)

    End Sub

    Sub Path_MapTarget()
        '=== Determines the Folder location & reports in Excel
        Call BrowseForFolder()
        'Update -     Call Excel_Store_Value("Dummy", "PathMapTarget", path)

    End Sub

    Sub SetDefaultPath(name As String)
        'A relative folder location is set, with a folder to be generated if absent

        path = Application.StartupPath & "\" & name

    End Sub

    Sub BrowseForFolder(Optional description As String = "", Optional startupDir As String = "")
        'User selects a folder location
        'Folder location is stored as a string
        Dim diaFolder As New FolderBrowserDialog

        diaFolder.Description = description
        diaFolder.SelectedPath = startupDir
        diaFolder.ShowNewFolderButton = False

        ' Open the file dialog
        If diaFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            path = diaFolder.SelectedPath
        Else
            path = String.Empty
        End If

        diaFolder = Nothing

    End Sub

    Sub BrowseForFile(ftype As String, Optional dirPath As String = "")
        'User selects a file based on a provided file type
        Dim diaFolder As New OpenFileDialog

        diaFolder.Filter = "Files (*." & ftype & ")| *." & ftype
        diaFolder.FilterIndex = 1
        diaFolder.InitialDirectory = dirPath
        diaFolder.Multiselect = False
        diaFolder.Title = "Browse for File"

        ' Open the file dialog
        If diaFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            path = diaFolder.FileName
        Else
            path = String.Empty
        End If

        diaFolder = Nothing

    End Sub

    Sub BrowseForFiles()
        'User selects an arbitrary set of files
        'File names and absolute paths are stored as an array

        Dim diaFolder As New OpenFileDialog
        Dim FName As Object
        Dim j As Long

        diaFolder.Filter = "All Files (*.*)| *.*"
        diaFolder.FilterIndex = 1
        diaFolder.InitialDirectory = Application.StartupPath
        diaFolder.Multiselect = True
        diaFolder.Title = "Browse for File"

        ' Open the file dialog
        If diaFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            FName = diaFolder.FileName
        Else
            FName = String.Empty
        End If

        diaFolder = Nothing

        j = UBound(FName)   'Determines the number of files selected

    End Sub

    Function FolderName_FromPath(path) As String
        Dim i As Long
        Dim FolderName As String

        i = Len(path) - InStrRev(path, "\")
        FolderName = Right(path, i)
        FolderName_FromPath = FolderName
    End Function

    Function convAbsRel(ByRef currDir As String, ByRef newDir As String, Optional ByVal file As Boolean = False) As String
        'Converts absolute path to relative path
        'Format right now is to end relative path a "\" for folder destinations.
        Dim relPath As String
        Dim i As Long
        Dim strLength As Long
        Dim strMatch As Boolean
        Dim charNoMatch As Long
        Dim endSlash As String

        convAbsRel = currDir
        endSlash = "\"
        If file Then endSlash = ""

        strMatch = True
        relPath = ""

        If newDir = "" Then Exit Function 'If text field is blank

        'Ensures that only one slash is left at the end of a path
        currDir = cleanPathEnd(currDir)
        newDir = cleanPathEnd(newDir)

        'Checks that path is a proper absolute path
        If Len(newDir) <= 3 Then
            MsgBox("Not a valid absolute path.")
            Exit Function
        End If

        strLength = System.Math.Max(Len(newDir), Len(currDir))

        For i = 1 To strLength
            If strMatch Then
                If i <= Len(currDir) Then
                    If Not Mid(currDir, i, 1) = Mid(newDir, i, 1) Then
                        strMatch = False
                        relPath = "..\"
                        charNoMatch = i - 1
                    End If
                    If i = Len(currDir) And strMatch Then    'Folder is within another folder beneath the current location, or is the current location
                        relPath = Right(newDir, Len(newDir) - i)
                        convAbsRel = relPath & endSlash
                        Exit Function
                    End If
                End If
            Else    'Folder lies on a path above the current location. Set up in relation to where path splits or ends sooner
                If Mid(currDir, i, 1) = "\" Then relPath = relPath & "..\"
                If i = Len(currDir) Then Exit For
            End If
        Next i

        'Only if folder lies on a different path above the current location
        convAbsRel = relPath & Right(newDir, Len(newDir) - charNoMatch)
        If Len(newDir) <> charNoMatch And Right(newDir, 1) <> "\" Then convAbsRel = convAbsRel & endSlash

    End Function

    Function convRelAbs(ByRef appDir As String, ByRef newDir As String) As String
        'Converts relative path to absolute path
        'Format right now is to end absolute path without a "\"
        Dim i As Long
        Dim dirCount As Long
        Dim newDirTrunc As String

        dirCount = 0

        If newDir = "" Then
            convRelAbs = ""
            Exit Function 'If text field is blank
        End If


        'Ensures that only one slash is left at the end of a path
        appDir = cleanPathEnd(appDir)
        newDir = cleanPathEnd(newDir)

        If newDir = "\" Then 'For case of path at current location
            convRelAbs = appDir
            Exit Function
        Else
            For i = 1 To Len(newDir)    'Determine how many directories up from the current application to go
                If Mid(newDir, i, 3) = "..\" Then dirCount = dirCount + 1
            Next i
        End If

        newDirTrunc = Right(newDir, Len(newDir) - 3 * dirCount) 'Get remainder of relative path beyond the directory change references, save final 'slash'

        For i = 0 To Len(appDir) - 1  'Compare characters of application path from right to left to track directory changes
            If Mid(appDir, Len(appDir) - i, 1) = "\" Then
                dirCount = dirCount - 1     'Subtract next directory reversed through
            End If
            If dirCount = 0 Then      'Once all directories have been reversed, current count records the character index where this occurs
                convRelAbs = Left(appDir, Len(appDir) - i) & newDirTrunc 'Get portion of directory to current application path shared by the relative, then append remaining segment of relative path
                Exit Function
            End If
            If dirCount = 1 And Mid(appDir, Len(appDir) - i, 1) = ":" Then  'Final directory change is with the path letter, so no more "\" is encountered.
                convRelAbs = newDirTrunc
                Exit Function
            End If
        Next

        MsgBox("Conversion from relative to absolute path failed")
        convRelAbs = appDir
    End Function
    Sub Path_Slash_Truncate(path As String)
        'NOT USED
        'Eliminates slash at the end of file string, if it exists
        'Only works for one ending slash
        If Right(path, 1) = "\" Then
            path = Left(path, Len(path) - 1)
        End If

    End Sub

    Function cleanPathEnd(ByRef dirPath As String)
        'Removes any trailing "\" characters from a file path
        'Works for multiple ending slashes
        Dim searchChar As Boolean
        Dim i As Long

        searchChar = True
        i = 1
        cleanPathEnd = dirPath

        'For case of path at current location, where the address is only a single "\"
        If Len(cleanPathEnd) = 1 And Right(cleanPathEnd, 1) = "\" Then Exit Function

        'For case of only moving one directory up
        If Mid(cleanPathEnd, Len(cleanPathEnd) - 2, 3) = "..\" Then Exit Function

        'All other cases
        While searchChar    'Loops done in case the string has "\\" or similar at the end
            If Right(dirPath, i) = "\" Then cleanPathEnd = Left(dirPath, Len(dirPath) - 1)
            If Right(dirPath, i) <> "\" Then Exit Function
            i = i + 1
        End While
    End Function

    Function slashPathEnd(ByRef dirPath As String)
        'Adds trailing "\" character to end of a file path, if one does not exist. Ensures that only one exists
        slashPathEnd = cleanPathEnd(dirPath)    'Ensures there are no trailing slashes
        slashPathEnd = slashPathEnd & "\"

    End Function

End Module
