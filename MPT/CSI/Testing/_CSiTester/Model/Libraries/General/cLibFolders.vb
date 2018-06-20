Option Strict On
Option Explicit On

Imports System.IO

Imports CSiTester.cLibPath

''' <summary>
''' Contains routines for working with folders and file locations within folders. Used for folder/file renaming, deleting, locating, relocating, etc.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class cLibFolders

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Querying"
    ''' <summary>
    ''' Checks if a supplied folder directory exists.
    ''' </summary>
    ''' <param name="p_pathDirectory">Path to the directory checked.</param>
    ''' <returns>True if the directory exists, false if it does not.</returns>
    ''' <remarks></remarks>
    Public Shared Function DirectoryExists(ByVal p_pathDirectory As String) As Boolean
        Try
            If String.IsNullOrEmpty(p_pathDirectory) Then Return False
            If Directory.Exists(p_pathDirectory) Then Return True
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Checks if a supplied file exists at the specified location.
    ''' </summary>
    ''' <param name="p_pathFile">Path to the file to be checked.</param>
    ''' <returns>True if the file exists, false if it does not.</returns>
    ''' <remarks></remarks>
    Public Shared Function FileExists(ByVal p_pathFile As String) As Boolean
        Try
            If String.IsNullOrEmpty(p_pathFile) Then Return False
            If File.Exists(p_pathFile) Then Return True
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Performs a variety of folder and file creation and deletion tests to see if the program has read/write access to a folder.
    ''' </summary>
    ''' <param name="pathDir">Path of the directory that is being checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReadableWriteableDeletableDirectory(ByVal pathDir As String) As Boolean
        Dim pathTemp As String

        ReadableWriteableDeletableDirectory = True

        If DirectoryExists(pathDir) Then
            Try
                'Test Folder Creation & Deletion
                pathTemp = pathDir & "\ReadableWriteableDeletableDirectory"
                ComponentCreateDirectory(pathTemp, True)
                If Not DirectoryExists(pathTemp) Then ReadableWriteableDeletableDirectory = False
                ComponentDeleteDirectoryAction(pathTemp, True, True)
                If DirectoryExists(pathTemp) Then ReadableWriteableDeletableDirectory = False

                If ReadableWriteableDeletableDirectory Then
                    'Test File Creation
                    pathTemp = pathDir & "\ReadableWriteableDeletableDirectory.ini"
                    Using objWriter As New System.IO.StreamWriter(pathTemp)
                        objWriter.WriteLine("$ " & "C:\Verification")
                    End Using
                    If Not FileExists(pathTemp) Then ReadableWriteableDeletableDirectory = False

                    'Test File Delete
                    ComponentDeleteFileAction(pathTemp, True)
                    If FileExists(pathTemp) Then ReadableWriteableDeletableDirectory = False
                End If

            Catch ex As Exception
                ReadableWriteableDeletableDirectory = False
            End Try

        End If

    End Function

    ''' <summary>
    ''' Gets all sub-directory folders and adds them to a supplied list of paths.
    ''' </summary>
    ''' <param name="StartPath">Path to the parent directory to begin the check.</param>
    ''' <param name="DirectoryList">List of paths to populate with directory names.</param>
    ''' <param name="listSubDirectories">If true, all subdirectories will be listed. If false, only the highest level of subdirectories will be listed.</param>
    ''' <remarks></remarks>
    Public Shared Sub GetDirectories(ByVal startPath As String, ByRef directoryList As List(Of String), Optional ByVal listSubDirectories As Boolean = True)
        Try
            Dim Dirs() As String = Directory.GetDirectories(startPath)

            For Each Dir As String In Dirs
                directoryList.Add(Dir)
            Next

            If listSubDirectories Then
                For Each Dir As String In Dirs
                    GetDirectories(Dir, directoryList)
                Next
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Lists the paths of all of the folders and subfolders within the specified directory.
    ''' </summary>
    ''' <param name="myPath">Path to the directory to check.</param>
    ''' <param name="myFolderName">Optional: Only paths for the specified Folder name will be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFoldersInFolder(ByVal myPath As String, Optional myFolderName As String = "") As List(Of String)

        Dim DirList As New List(Of String)
        GetDirectories(myPath, DirList)

        'If a Folder name is specified, trim the list to only include these names
        If Not String.IsNullOrEmpty(myFolderName) Then
            Dim DirListFolder As String
            Dim DirListTemp As New List(Of String)

            For Each myFolder As String In DirList
                DirListFolder = GetSuffix(myFolder, "\")
                If DirListFolder = myFolderName Then
                    DirListTemp.Add(myFolder)
                End If
            Next

            'Clears the first list of folders and assigns it to the reduced list
            DirList.Clear()
            DirList = DirListTemp
        End If

        Return DirList

    End Function

    ''' <summary>
    ''' Determines if a directory contains files. Returns true if it does.
    ''' </summary>
    ''' <param name="pathDir">Path to the directory to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DirContainsFiles(ByVal pathDir As String) As Boolean
        Dim myDir As DirectoryInfo = New DirectoryInfo(pathDir)

        If myDir.EnumerateFiles().Any() Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Determines if a directory contains directories. Returns false if it does.
    ''' </summary>
    ''' <param name="pathDir">Path to the directory to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DirContainsDirs(ByVal pathDir As String) As Boolean
        Dim myDir As DirectoryInfo = New DirectoryInfo(pathDir)

        If myDir.EnumerateFiles().Any() Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Naming"
    ''' <summary>
    ''' Renames a file.
    ''' </summary>
    ''' <param name="p_path">Path to file to renamed, including the file name and extension.</param>
    ''' <param name="p_newName">New file name and extension.</param>
    ''' <remarks></remarks>
    Public Shared Sub RenameFile(ByVal p_path As String,
                                 ByVal p_newName As String)
        If GetSuffix(p_path, "\") = p_newName Then
            Exit Sub                                'Exits sub if name already exists
        End If

        Try
            My.Computer.FileSystem.RenameFile(p_path, p_newName)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Renames a folder.
    ''' </summary>
    ''' <param name="p_path">Path to folder to renamed, including the folder name.</param>
    ''' <param name="p_newName">New folder name.</param>
    ''' <remarks></remarks>
    Public Shared Sub RenameFolder(ByVal p_path As String,
                                   ByVal p_newName As String)
        Try
            My.Computer.FileSystem.RenameDirectory(p_path, p_newName)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub
#End Region

#Region "Read Only Attributes"
    ''' <summary>
    ''' Sets all files in a folder and subfolders to not be read only.
    ''' </summary>
    ''' <param name="p_sourceFolderName">Path to the highest level folder to apply the readable overwrite to.</param>
    ''' <remarks>'Modified from Leith Ross http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html </remarks>
    Public Shared Sub SetDirectoryFilesNotReadOnly(ByVal p_sourceFolderName As String)
        Dim FSO As New Scripting.FileSystemObject
        Dim SourceFolder As Scripting.Folder
        Dim SubFolder As Scripting.Folder
        Dim FileItem As Scripting.File

        If Not DirectoryExists(p_sourceFolderName) Then Exit Sub
        SourceFolder = FSO.GetFolder(p_sourceFolderName)

        For Each FileItem In SourceFolder.Files
            ComponentSetAttributeAction(FileItem.Path, FileAttributes.Normal)       'Sets each file to not be read only
            'SetAttr(FileItem.Path, vbNormal)       'Sets each file to not be read only
        Next FileItem
        For Each SubFolder In SourceFolder.SubFolders
            SetDirectoryFilesNotReadOnly(SubFolder.path)
        Next SubFolder

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    ''' <summary>
    ''' Sets a specified file to not be read only.
    ''' </summary>
    ''' <param name="p_path">File path, including the filename</param>
    ''' <remarks></remarks>
    Public Shared Sub SetFileNotReadOnly(ByVal p_path As String)
        Dim sourceFolderName As String
        Dim fileName As String
        Dim FSO As New Scripting.FileSystemObject
        Dim SourceFolder As Scripting.Folder
        Dim FileItem As Scripting.File

        sourceFolderName = GetPathDirectoryStub(p_path)
        fileName = GetPathFileName(p_path)

        If Not DirectoryExists(sourceFolderName) Then Exit Sub
        SourceFolder = FSO.GetFolder(sourceFolderName)

        'Sets each file to not be read only
        For Each FileItem In SourceFolder.Files
            If StringsMatch(CStr(FileItem.Name), fileName) Then ComponentSetAttributeAction(FileItem.Path, FileAttributes.Normal)
        Next FileItem

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    ''' <summary>
    ''' Checks if a file has any of the Read Only attributes assigned.
    ''' </summary>
    ''' <param name="p_path">Path to the file to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsFileReadOnly(ByVal p_path As String) As Boolean
        If Not FileExists(p_path) Then Return False

        If (File.GetAttributes(p_path) = FileAttributes.ReadOnly OrElse
            File.GetAttributes(p_path) = 33 OrElse
            File.GetAttributes(p_path) = 8225) Then

            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Deleting"
    ''' <summary>
    ''' Deletes a single file of a specified path. 
    ''' Returns true if this results in the file no longer existing.
    ''' </summary>
    ''' <param name="p_path">Path to the file to be deleted.</param>
    ''' <param name="p_includeReadOnly">True: Removes read-only protection. False: Read-only files will not be deleted. Function will return False for read-only files.</param>
    ''' <remarks></remarks>
    Public Shared Function DeleteFile(ByVal p_path As String,
                                      Optional ByVal p_includeReadOnly As Boolean = False) As Boolean

        Try
            If Not FileExists(p_path) Then Return True
            If (Not p_includeReadOnly AndAlso IsFileReadOnly(p_path)) Then Return False

            'Remove read-only attributes & delete file
            ComponentSetAttributeAction(p_path, FileAttributes.Normal)
            ComponentDeleteFileAction(p_path)

            If Not FileExists(p_path) Then Return True
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Deletes all files fitting the specified criteria. If no file name or extension is given, then all files will be deleted.
    ''' </summary>
    ''' <param name="myPath">Path to the parent folder containing files to be deleted.</param>
    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. False: Files will only be deleted in the specified folder.</param>
    ''' <param name="myFileName">Optional: All files of this name will be deleted. Combine with myFileExtension to limit this to files of a particular name and type.</param>
    ''' <param name="myFileExtension">Optional: All files of this extension will be deleted.</param>
    ''' <param name="includeReadOnly">Optional: True: Read only files will also be deleted. False: Read only files will be skipped.</param>
    ''' <param name="fileNamesExclude">Optional: List of filenames to preserve during the delete process.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFiles(ByVal myPath As String, ByVal includeSubFolders As Boolean, Optional ByVal myFileName As String = "", Optional ByVal myFileExtension As String = "", Optional ByVal includeReadOnly As Boolean = False, Optional ByVal fileNamesExclude As List(Of String) = Nothing)
        'Get list of files to delete
        Dim deleteList As New List(Of String)
        Dim cursorWait As New cCursorWait

        deleteList = ListFilePathsInDirectory(myPath, includeSubFolders, myFileName, myFileExtension)
        If deleteList Is Nothing Then Exit Sub

        'Filter out files to save from the delete list
        If fileNamesExclude IsNot Nothing Then
            Dim deleteListTemp As New List(Of String)
            Dim deleteFile = True

            For Each fileToDelete As String In deleteList
                deleteFile = True
                For Each saveFile As String In fileNamesExclude
                    If GetPathFileName(fileToDelete) = saveFile Then
                        deleteFile = False
                        Exit For
                    End If
                Next

                If deleteFile Then deleteListTemp.Add(fileToDelete)
            Next

            deleteList = deleteListTemp
        End If

        'Delete all files in the list
        For Each myFileDelete In deleteList
            If includeReadOnly Then
                'SetAttr(myFileDelete, vbNormal)
                ComponentSetAttributeAction(myFileDelete, FileAttributes.Normal)
                ComponentDeleteFileAction(myFileDelete)
            Else
                'Check if file is read only, and if so, skip it
                If FileExists(myFileDelete) Then
                    If Not File.GetAttributes(myFileDelete) = FileAttributes.ReadOnly Then
                        If Not File.GetAttributes(myFileDelete) = 33 Then
                            ComponentDeleteFileAction(myFileDelete)
                        End If
                    End If
                End If
            End If
        Next

        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Deletes all files fitting the specified criteria lists. If no file name or extension is given, then all files will be deleted.
    ''' </summary>
    ''' <param name="myPath">Path to the parent folder containing files to be deleted.</param>
    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. False: Files will only be deleted in the specified folder.</param>
    ''' <param name="myFileNames">Optional: All files of the names in this list will be deleted. Combine with myFileExtensions to limit this to files of a particular name and type.</param>
    ''' <param name="myFileExtensions">Optional: All files of the extensions in this list will be deleted.</param>
    ''' <param name="includeReadOnly">True: Read only files will also be deleted. False: Read only files will be skipped.</param>
    ''' <param name="partialNameMatch">Optional: If true, a file will be considered a match for deletion by filename if the name is at least present in the overall filename. If false, only exact matches will be considered.</param>
    ''' <param name="waitCursor">If true, the cursor changes to a wait cursor while the function runs.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFilesBulk(ByVal myPath As String, ByVal includeSubFolders As Boolean, Optional ByVal myFileNames As List(Of String) = Nothing, Optional ByVal myFileExtensions As List(Of String) = Nothing, _
                        Optional ByVal includeReadOnly As Boolean = False, Optional ByVal partialNameMatch As Boolean = False, Optional waitCursor As Boolean = False)
        'Get list of files to delete
        Dim deleteList As New List(Of String)
        Dim cursorWait As New cCursorWait(waitCursor)

        If myFileNames Is Nothing Then
            myFileNames = New List(Of String)
            myFileNames.Add("")
        End If
        If myFileExtensions Is Nothing Then
            myFileExtensions = New List(Of String)
            myFileExtensions.Add("")
        End If

        'Get list of all files in the folders & subfolders (if specified)
        deleteList = ListFilePathsInDirectory(myPath, includeSubFolders)
        If deleteList Is Nothing Then Exit Sub

        'Delete all files in the list
        For Each myFileDelete In deleteList
            If includeReadOnly Then
                'SetAttr(myFileDelete, vbNormal)
                For Each myFileName As String In myFileNames
                    For Each myFileExtension As String In myFileExtensions
                        If FileNameExtensionMatch(myFileDelete, myFileName, myFileExtension, partialNameMatch) Then
                            ComponentSetAttributeAction(myFileDelete, FileAttributes.Normal)
                            ComponentDeleteFileAction(myFileDelete)
                        End If
                    Next
                Next
            Else
                'Check if file is read only, and if so, skip it
                For Each myFileName As String In myFileNames
                    For Each myFileExtension As String In myFileExtensions
                        If FileNameExtensionMatch(myFileDelete, myFileName, myFileExtension, partialNameMatch) Then
                            If FileExists(myFileDelete) Then
                                If Not File.GetAttributes(myFileDelete) = FileAttributes.ReadOnly Then
                                    If Not File.GetAttributes(myFileDelete) = 33 Then
                                        ComponentDeleteFileAction(myFileDelete)
                                    End If
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        Next
        cursorWait.EndCursor()
    End Sub

    ''' <summary>
    ''' Deletes all files and folders.
    ''' Specifications allow the root folder to be preserved. 
    ''' </summary>
    ''' <param name="myPath">Path to the root folder.</param>
    ''' <param name="deleteRootFolder">True: The root folder and all files and folders within will be deleted. False: Root folder is preserved but will be empty.</param>
    ''' <param name="myFolderName">Optional: Name of a specific folder to delete.</param>
    ''' <param name="includeReadOnly">True: Read only files and folders will also be deleted. False: Read only files and folders will be skipped.</param>
    ''' <param name="foldersPreserve">Optional: List of folder paths to preserve during the deleting process. Only works if not deleting the root folder.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAllFilesFolders(ByVal myPath As String, ByVal deleteRootFolder As Boolean, Optional ByVal myFolderName As String = "", Optional ByVal includeReadOnly As Boolean = False, Optional ByVal foldersPreserve As List(Of String) = Nothing)
        Dim deleteList As New List(Of String)
        Dim cursorWait As New cCursorWait

        'Delete folders & subdirectories
        If deleteRootFolder Then
            deleteList.Add(myPath)
        Else
            'Delete files in root directory first
            DeleteFiles(myPath, False)

            'Create list of subdirectories to delete
            If foldersPreserve Is Nothing Then
                deleteList = ListFoldersInFolder(myPath, myFolderName)
            Else
                Dim deleteListTemp = ListFoldersInFolder(myPath, myFolderName)

                For Each folderPreserve As String In foldersPreserve
                    For Each folderDelete As String In deleteListTemp
                        If Not folderDelete = folderPreserve Then deleteList.Add(folderDelete)
                    Next
                Next
            End If

        End If

        'If any folders are specified for deleting, delete folders
        SetDirectoryFilesNotReadOnly(myPath)     'Currently function cannot skip 'read-only' portions
        'If includeReadOnly Then AllFilesNotReadOnly(myPath)

        If deleteList.Count > 0 Then
            For Each myFolderDelete In deleteList
                Try
                    ComponentDeleteDirectoryAction(myFolderDelete, True, True)
                Catch ex As Exception
                    'MyLogger
                    'This is to skip the subfolders, which have been deleted by deleting the parent folders
                End Try
            Next
        End If
        cursorWait.EndCursor()
    End Sub

#End Region

#Region "Copying/Moving"
    ''' <summary>
    ''' Copies a file from one directory to another. 
    ''' If directory does not exist, directory will be created. 
    ''' Returns true if it is confirmed that a new file exists at the destination.
    ''' </summary>
    ''' <param name="p_pathSource">File path source, including the file name.</param>
    ''' <param name="p_pathDestination">File path destination, including the file name. If filename is different, file will be renamed.</param>
    ''' <param name="p_overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    ''' <param name="p_includeReadOnly">True: Removes read-only protection. False: Read-only files will not be copied.</param>
    ''' <param name="p_promptMessage">Message to display to the user if a file exists.</param>
    ''' <param name="p_waitNewFileExist">True: Code will loop within this routine until the copy action has completed. Recommended if reading the copied file during same process as copying the file.</param>
    ''' <param name="p_noSourceExistPrompt">True: If a source file does not exist for copying, the user is notified of this and that the copy action will not take place.</param>
    ''' <remarks></remarks>
    Public Shared Function CopyFile(ByVal p_pathSource As String,
                                    ByVal p_pathDestination As String,
                                    ByVal p_overWriteFile As Boolean,
                                    Optional ByVal p_includeReadOnly As Boolean = False,
                                    Optional ByVal p_promptMessage As String = "",
                                    Optional ByVal p_waitNewFileExist As Boolean = False,
                                    Optional ByVal p_noSourceExistPrompt As Boolean = False) As Boolean

        'Check that source file exists
        If Not FileExists(p_pathSource) Then
            If p_noSourceExistPrompt Then MessageBox.Show("Original file does not exist. File will not be copied")
            Return False
        End If

        Try
            'Remove read-only attributes if specified
            If p_includeReadOnly Then
                ComponentSetAttributeAction(p_pathSource, FileAttributes.Normal)
                If FileExists(p_pathDestination) Then ComponentSetAttributeAction(p_pathDestination, FileAttributes.Normal)
            End If

            'Copy if appropriate
            If (Not p_includeReadOnly AndAlso
                (IsFileReadOnly(p_pathSource) OrElse
                 IsFileReadOnly(p_pathDestination))) Then Return False
            ComponentCopyFileAction(p_pathSource, p_pathDestination, p_overWriteFile, p_promptMessage)

            'Check that a file exists at the destination
            Dim newFileExist As Boolean = False
            If p_waitNewFileExist Then
                Dim counter As Integer = 0
                'Wait until file copy action has been completed
                While Not newFileExist
                    counter += 1
                    If counter > 20 Then Exit While

                    If FileExists(p_pathDestination) Then newFileExist = True

                    System.Threading.Thread.Sleep(500)
                End While
            Else
                Return FileExists(p_pathDestination)
            End If

            Return True
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Makes a duplicate folder,including sub-folders.
    ''' If the destination already exists it will overwrite existing files in this folder. 
    ''' If the destination does not exist, it will be made for you.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="p_pathSource">Source directory path.</param>
    ''' <param name="p_pathDestination">Destination directory path.</param>
    ''' <param name="p_overWriteExisting">True: Destination directory and files will be overwritten if they already exist.</param>
    ''' <remarks></remarks>
    Public Shared Function CopyFolder(ByVal p_pathSource As String,
                                     ByVal p_pathDestination As String,
                                     Optional ByVal p_overWriteExisting As Boolean = False) As Boolean
        'Check that source is valid
        If Not DirectoryExists(p_pathSource) Then
            MsgBox(p_pathSource & " doesn't exist. Directory will not be copied.")
            Return False
        End If

        'Check if destination already exists
        If (DirectoryExists(p_pathDestination) AndAlso Not p_overWriteExisting) Then
            Select Case MessageBox.Show("Directory already exists and is set to not be overwritten. Do you wish to overwrite?", "Directory Location Already Exists", MessageBoxButton.YesNo, MessageBoxImage.Warning)
                Case MessageBoxResult.Yes 'Function will continue and overwrite the existing directory
                Case MessageBoxResult.No : Return False
            End Select
        End If

        Dim cursorWait As New cCursorWait
        ComponentCopyDirectory(p_pathSource, p_pathDestination)
        cursorWait.EndCursor()

        If DirectoryExists(p_pathDestination) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Moves a file from a specified source to destination. 
    ''' May or may not remove the original file depending on parameter inputs.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="p_pathSource">Path to the original file.</param>
    ''' <param name="p_pathDestination">Path to where the original file is to be moved.</param>
    ''' <param name="p_deleteOriginal">True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    ''' <remarks></remarks>
    Public Shared Function MoveFile(ByVal p_pathSource As String,
                                    ByVal p_pathDestination As String,
                                    ByVal p_deleteOriginal As Boolean) As Boolean
        If FileExists(p_pathSource) Then
            CopyFile(p_pathSource, p_pathDestination, p_overWriteFile:=True, p_includeReadOnly:=True)
            If p_deleteOriginal AndAlso FileExists(p_pathDestination) Then DeleteFile(p_pathSource, p_includeReadOnly:=True)
        End If

        If (p_deleteOriginal AndAlso FileExists(p_pathSource)) Then Return False
        If FileExists(p_pathDestination) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Moves a file from a specified source to destination. 
    ''' May or may not remove the original file depending on parameter inputs.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="p_pathSource">Path to the original folder.</param>
    ''' <param name="p_pathDestination">Path to where the original folder is to be moved.</param>
    ''' <param name="p_deleteOriginal">True: Original folder will be deleted after copy action. False: Original folder will be left as is.</param>
    ''' <remarks></remarks>
    Public Shared Function MoveFolder(ByVal p_pathSource As String,
                                     ByVal p_pathDestination As String,
                                     ByVal p_deleteOriginal As Boolean) As Boolean
        If DirectoryExists(p_pathSource) Then
            CopyFolder(p_pathSource, p_pathDestination, p_overWriteExisting:=True)
            If p_deleteOriginal Then DeleteAllFilesFolders(p_pathSource, deleteRootFolder:=True)
        End If

        If (p_deleteOriginal AndAlso DirectoryExists(p_pathSource)) Then Return False
        If DirectoryExists(p_pathDestination) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Misc"
    ''' <summary>
    ''' Opens Windows Explorer at the specified directory
    ''' </summary>
    ''' <param name="myFolderPath">Path to the folder to be opened</param>
    ''' <param name="myErrorMessage">Optional error message if the folder does not exist at the specified location</param>
    ''' <remarks></remarks>
    Public Shared Sub OpenExplorerAtFolder(ByVal myFolderPath As String, Optional myErrorMessage As String = "Folder Does Not Exist")
        'Similar for files. See: http://msdn.microsoft.com/en-us/library/system.io.directory.exists.aspx
        Try
            If Directory.Exists(myFolderPath) Then
                Try
                    Process.Start("explorer.exe", myFolderPath)
                Catch ex As Exception
                    csiLogger.ExceptionAction(ex)
                End Try
            Else
                MsgBox(myErrorMessage)
            End If
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Creates a database structure directory system for a given example name within a specified parent folder
    ''' </summary>
    ''' <param name="p_baseDir">Parent folder within which to create the database directory</param>
    ''' <param name="p_exampleName">Example name by which to name the database directory</param>
    ''' <remarks></remarks>
    Public Shared Function CreateDatabaseDirectory(ByVal p_baseDir As String,
                                            ByVal p_exampleName As String) As Boolean

        Dim dirPath As String = p_baseDir & "\" & p_exampleName

        If DirectoryExists(dirPath) Then
            Select Case MessageBox.Show("'" & p_exampleName & "'" & " cannot be gathered into a database structure because the following directory already exists: " & vbCrLf & vbCrLf & dirPath _
                                        & vbCrLf & vbCrLf & "Would you like to replace the existing directory? If not, example " & "'" & p_exampleName & "'" & " will be skipped.",
                                   "Directory Conflict", MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
                Case MessageBoxResult.Yes : DeleteAllFilesFolders(dirPath, True)
                Case MessageBoxResult.No : Return False
            End Select
        End If

        ComponentCreateDirectory(dirPath)
        ComponentCreateDirectory(dirPath & "\" & cPathModel.DIR_NAME_MODELS_DEFAULT)
        ComponentCreateDirectory(dirPath & "\" & cPathAttachment.DIR_NAME_ATTACHMENTS_DEFAULT)
        ComponentCreateDirectory(dirPath & "\" & cPathAttachment.DIR_NAME_FIGURES_DEFAULT)

        Return True
    End Function

    ''' <summary>
    ''' Creates a temprorary directory of the specified path &amp; name. 
    ''' If a current directory exists, the name will either have an incremented number, or if at the max allowed, the highest numbered folder will be deleted first. 
    ''' The function returns the resulting path of the created folder.
    ''' </summary>
    ''' <param name="pathTemp">Initial path to use to create the temporary folder.</param>
    ''' <param name="numTempDirsMax">If creating a new temporary destination, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <returns>The resulting path of the created folder.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateTempDirectory(ByVal pathTemp As String, Optional ByVal numTempDirsMax As Integer = 1) As String
        Dim i As Integer
        For i = 1 To numTempDirsMax
            'Adjust new temp folder name if greater than 1
            If Not i = 1 Then
                pathTemp = pathTemp & "i"
            End If
            If Not DirectoryExists(pathTemp) Then                                        'Exit loop & create new folder
                Exit For
            Else
                If i = numTempDirsMax Then DeleteAllFilesFolders(pathTemp, True, , True) 'If the max folder number, delete the latest temp folder
            End If
        Next

        ComponentCreateDirectory(pathTemp, True)
        Return pathTemp
    End Function
#End Region

#Region "Component Functions"
    ''' <summary>
    ''' Component function. Copies a file. Includes error messages.
    ''' </summary>
    ''' <param name="p_pathSource">File path source, including the file name.</param>
    ''' <param name="p_pathDestination">File path destination, including the file name. If filename is different, file will be renamed.</param>
    ''' <param name="p_overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    ''' <param name="p_promptMessage">Message to display to the user if a file exists.</param>
    ''' <param name="p_mySuppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCopyFileAction(ByVal p_pathSource As String,
                                              ByVal p_pathDestination As String,
                                              ByVal p_overWriteFile As Boolean,
                                              Optional ByVal p_promptMessage As String = "",
                                              Optional ByVal p_mySuppressExStates As Boolean = False)
        Try
            If StringsMatch(p_pathSource, p_pathDestination) Then Exit Sub

            If (Not p_overWriteFile AndAlso
                FileExists(p_pathDestination) AndAlso
                Not String.IsNullOrEmpty(p_promptMessage)) Then

                MessageBox.Show(p_promptMessage)
                Exit Sub
            End If

            If FileExists(p_pathSource) Then My.Computer.FileSystem.CopyFile(p_pathSource, p_pathDestination, p_overWriteFile)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_mySuppressExStates)
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Deletes a file. Includes error messages.
    ''' </summary>
    ''' <param name="p_path">Path of the file to be deleted.</param>
    ''' <param name="p_suppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentDeleteFileAction(ByVal p_path As String,
                                                Optional ByVal p_suppressExStates As Boolean = False)
        Try
            If FileExists(p_path) Then File.Delete(p_path)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_suppressExStates)
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Deletes a directory. Includes error messages.
    ''' </summary>
    ''' <param name="p_path">Path of the file to be deleted.</param>
    ''' <param name="p_removeOtherFilesDirectoriesInPath">If 'true', other files, directories, and subdirectories below this path will be deleted. Default is 'false'.</param>
    ''' <param name="p_suppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentDeleteDirectoryAction(ByVal p_path As String,
                                                     Optional p_removeOtherFilesDirectoriesInPath As Boolean = False,
                                                     Optional ByVal p_suppressExStates As Boolean = False)
        Try
            If DirectoryExists(p_path) Then Directory.Delete(p_path, p_removeOtherFilesDirectoriesInPath)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_suppressExStates)
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Sets the attribute of a file. Includes error messages.
    ''' </summary>
    ''' <param name="p_path">Path to the file to set the attributes of.</param>
    ''' <param name="p_attribute">Attribute to set the file to.</param>
    ''' <param name="p_suppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentSetAttributeAction(ByVal p_path As String,
                                                  ByVal p_attribute As FileAttributes,
                                                  Optional ByVal p_suppressExStates As Boolean = False)
        Try
            If FileExists(p_path) Then File.SetAttributes(p_path, FileAttributes.Normal)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_suppressExStates)
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Creates a new directory. Includes error messages.
    ''' </summary>
    ''' <param name="p_path">Path of the directory to be created.</param>
    ''' <param name="p_suppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCreateDirectory(ByVal p_path As String,
                                               Optional ByVal p_suppressExStates As Boolean = False)
        Try
            Directory.CreateDirectory(p_path)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_suppressExStates)
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Copies a direcotry, or creates a new one if it does not exist at the destination. Includes error messages.
    ''' </summary>
    ''' <param name="p_pathSource">Source directory path.</param>
    ''' <param name="p_pathDestination">Destination directory path.</param>
    ''' <param name="p_suppressExStates">If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCopyDirectory(ByVal p_pathSource As String,
                                             ByVal p_pathDestination As String,
                                             Optional ByVal p_suppressExStates As Boolean = False)
        Try
            If DirectoryExists(p_pathSource) Then My.Computer.FileSystem.CopyDirectory(p_pathSource, p_pathDestination, True)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex, , p_suppressExStates)
        End Try
    End Sub

#End Region

#Region "Routines to Finish/Remove/Check"
    'TODO: Not Used Yet


    'TODO: Currently does nothing
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mypath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DirectoryIsFlattened(ByVal mypath As String) As Boolean
        DirectoryIsFlattened = True
        'TODO: 

    End Function

    'Not used. defunct? Remove?
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SourceFolderName">Path to the folder to be checked.</param>
    ''' <param name="IncludeSubfolders">True: Subfolders will also be checked.</param>
    ''' <remarks></remarks>
    Public Shared Sub listModelFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
        'Modified from
        ' Leith Ross
        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
        '
        ' lists information about the files in SourceFolder
        '
        Dim FSO As New Scripting.FileSystemObject
        Dim SourceFolder As Scripting.Folder
        Dim SubFolder As Scripting.Folder
        Dim FileItem As Scripting.File
        Dim r As Long

        If Not DirectoryExists(SourceFolderName) Then Exit Sub
        SourceFolder = FSO.GetFolder(SourceFolderName)

        'Update - r = Range("A65536").End(xlUp).row + 1
        'r = 6


        For Each FileItem In SourceFolder.Files
            'display file properties
            If FileItem.name = "model.xml" Then
                'Update - Cells(r, 1).value = FileItem.name
                'Update - Cells(r, 2).value = FileItem.path

                '***** Remove the single ' character in the below lines to see this information *****
                'FileItem.Size
                'FileItem.DateCreated
                'FileItem.DateLastModified

                r = r + 1 ' next row number
            End If
        Next FileItem
        If IncludeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                listModelFilesInFolder(SubFolder.path, True)
            Next SubFolder
        End If

        FileItem = Nothing
        SourceFolder = Nothing
        FSO = Nothing

    End Sub

    ''Not used. Template for such a function, but currently does not do anything. Remove?
    ' ''' <summary>
    ' ''' 
    ' ''' </summary>
    ' ''' <param name="SourceFolderName">Path to the folder to be checked.</param>
    ' ''' <param name="IncludeSubfolders">True: Subfolders will also be checked.</param>
    ' ''' <remarks></remarks>
    'Public Shared Sub ListFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
    '    'Modified from
    '    ' Leith Ross
    '    ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
    '    '
    '    ' lists information about the files in SourceFolder
    '    '
    '    Dim FSO As New Scripting.FileSystemObject
    '    Dim SourceFolder As Scripting.Folder
    '    Dim SubFolder As Scripting.Folder
    '    Dim FileItem As Scripting.File
    '    Dim r As Long

    '    If Not DirectoryExists(SourceFolderName) Then Exit Sub
    '    SourceFolder = FSO.GetFolder(SourceFolderName)
    '    'Update - r = Range("A65536").End(xlUp).row + 1
    '    For Each FileItem In SourceFolder.Files
    '        'display file properties
    '        'Update -   Cells(r, 1).Formula = FileItem.Name

    '        '***** Remove the single ' character in the below lines to see this information *****
    '        'FileItem.Path
    '        'FileItem.Size
    '        'FileItem.DateCreated
    '        'FileItem.DateLastModified

    '        r = r + 1 ' next row number
    '    Next FileItem
    '    If IncludeSubfolders Then
    '        For Each SubFolder In SourceFolder.SubFolders
    '            ListFilesInFolder(SubFolder.path, True)
    '        Next SubFolder
    '    End If

    '    FileItem = Nothing
    '    SourceFolder = Nothing
    '    FSO = Nothing

    'End Sub
#End Region

    Private Shared Sub ListFilesInFolder(p1 As String, p2 As Boolean)
        Throw New NotImplementedException
    End Sub

End Class
