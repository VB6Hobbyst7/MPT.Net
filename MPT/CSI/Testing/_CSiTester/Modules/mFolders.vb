Option Explicit On
Option Strict On

'Imports System
Imports System.IO
'Imports System.Collections

''' <summary>
''' Module that contains routines for working with folders and file locations within folders. Used for folder/file renaming, deleting, locating, relocating, etc.
''' </summary>
''' <remarks></remarks>
Module mFolders

    '#Region "Querying"
    '    ''' <summary>
    '    ''' Checks if a supplied folder directory exists.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the directory checked.</param>
    '    ''' <returns>True if the directory exists, false if it does not.</returns>
    '    ''' <remarks></remarks>
    '    Function DirectoryExists(ByVal myPath As String) As Boolean
    '        Try
    '            If Directory.Exists(myPath) Then Return True
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '        Return False
    '    End Function

    '    ''' <summary>
    '    ''' Checks if a supplied file exists at the specified location.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file to be checked.</param>
    '    ''' <returns>True if the file exists, false if it does not.</returns>
    '    ''' <remarks></remarks>
    '    Function FileExists(ByVal myPath As String) As Boolean
    '        Try
    '            If File.Exists(myPath) Then Return True
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '        Return False
    '    End Function

    '    ''' <summary>
    '    ''' Performs a variety of folder and file creation and deletion tests to see if the program has read/write access to a folder.
    '    ''' </summary>
    '    ''' <param name="pathDir">Path of the directory that is being checked.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ReadableWriteableDeletableDirectory(ByVal pathDir As String) As Boolean
    '        Dim pathTemp As String

    '        ReadableWriteableDeletableDirectory = True

    '        If DirectoryExists(pathDir) Then
    '            Try
    '                'Test Folder Creation & Deletion
    '                pathTemp = pathDir & "\ReadableWriteableDeletableDirectory"
    '                ComponentCreateDirectory(pathTemp, True)
    '                If Not DirectoryExists(pathTemp) Then ReadableWriteableDeletableDirectory = False
    '                ComponentDeleteDirectoryAction(pathTemp, True, True)
    '                If DirectoryExists(pathTemp) Then ReadableWriteableDeletableDirectory = False

    '                If ReadableWriteableDeletableDirectory Then
    '                    'Test File Creation
    '                    pathTemp = pathDir & "\ReadableWriteableDeletableDirectory.ini"
    '                    Dim objWriter As New System.IO.StreamWriter(pathTemp)
    '                    objWriter.WriteLine("$ " & "C:\Verification")
    '                    objWriter.Close()
    '                    objWriter = Nothing
    '                    If Not FileExists(pathTemp) Then ReadableWriteableDeletableDirectory = False

    '                    'Test File Delete
    '                    ComponentDeleteFileAction(pathTemp, True)
    '                    If FileExists(pathTemp) Then ReadableWriteableDeletableDirectory = False
    '                End If

    '            Catch ex As Exception
    '                ReadableWriteableDeletableDirectory = False
    '            End Try

    '        End If

    '    End Function

    '    ''' <summary>
    '    ''' Gets all sub-directory folders and adds them to a supplied list of paths.
    '    ''' </summary>
    '    ''' <param name="StartPath">Path to the parent directory to begin the check.</param>
    '    ''' <param name="DirectoryList">List of paths to populate with directory names.</param>
    '    ''' <param name="listSubDirectories">If true, all subdirectories will be listed. If false, only the highest level of subdirectories will be listed.</param>
    '    ''' <remarks></remarks>
    '    Sub GetDirectories(ByVal startPath As String, ByRef directoryList As List(Of String), Optional ByVal listSubDirectories As Boolean = True)
    '        Try
    '            Dim Dirs() As String = Directory.GetDirectories(startPath)

    '            For Each Dir As String In Dirs
    '                directoryList.Add(Dir)
    '            Next

    '            If listSubDirectories Then
    '                For Each Dir As String In Dirs
    '                    GetDirectories(Dir, directoryList)
    '                Next
    '            End If
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Lists the paths of all of the folders and subfolders within the specified directory.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the directory to check.</param>
    '    ''' <param name="myFolderName">Optional: Only paths for the specified Folder name will be returned.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function ListFoldersInFolder(ByVal myPath As String, Optional myFolderName As String = "") As List(Of String)

    '        Dim DirList As New List(Of String)
    '        GetDirectories(myPath, DirList)

    '        'If a Folder name is specified, trim the list to only include these names
    '        If Not myFolderName = "" Then
    '            Dim DirListFolder As String
    '            Dim DirListTemp As New List(Of String)

    '            For Each myFolder As String In DirList
    '                DirListFolder = GetSuffix(myFolder, "\")
    '                If DirListFolder = myFolderName Then
    '                    DirListTemp.Add(myFolder)
    '                End If
    '            Next

    '            'Clears the first list of folders and assigns it to the reduced list
    '            DirList.Clear()
    '            DirList = DirListTemp
    '        End If

    '        Return DirList

    '    End Function

    '    ''' <summary>
    '    ''' Populates a list of all of the files within a Folder that are of a specified extension.
    '    ''' </summary>
    '    ''' <param name="SourceFolderName">Name of the highest level Folder searched.</param>
    '    ''' <param name="IncludeSubfolders">True: check all subfolders of source Folder.</param>
    '    ''' <param name="myExtension">Filename extension.</param>
    '    ''' <param name="myFilesList">List to populate of all files with the specified filename extension.</param>
    '    ''' <remarks></remarks>
    '    Sub ListFilesInFolderByExtension(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean, ByVal myExtension As String, ByRef myFilesList As List(Of String))
    '        Dim FSO As New Scripting.FileSystemObject
    '        Dim SourceFolder As Scripting.Folder
    '        Dim SubFolder As Scripting.Folder
    '        Dim FileItem As Scripting.File
    '        Dim r As Integer
    '        Dim fileExtension As String

    '        If Not DirectoryExists(SourceFolderName) Then Exit Sub
    '        SourceFolder = FSO.GetFolder(SourceFolderName)
    '        For Each FileItem In SourceFolder.Files
    '            fileExtension = GetSuffix(FileItem.Name, ".").ToUpper
    '            If fileExtension = myExtension.ToUpper Or "." & fileExtension = myExtension.ToUpper Then
    '                myFilesList.Add(FileItem.Name)
    '            End If
    '            r = r + 1
    '        Next FileItem
    '        If IncludeSubfolders Then
    '            For Each SubFolder In SourceFolder.SubFolders
    '                ListFilesInFolder(SubFolder.path, True)
    '            Next SubFolder
    '        End If

    '        FileItem = Nothing
    '        SourceFolder = Nothing
    '        FSO = Nothing

    '    End Sub

    '    ''' <summary>
    '    ''' Determines if a directory contains files. Returns true if it does.
    '    ''' </summary>
    '    ''' <param name="pathDir">Path to the directory to check.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function DirContainsFiles(ByVal pathDir As String) As Boolean
    '        Dim myDir As DirectoryInfo = New DirectoryInfo(pathDir)

    '        If myDir.EnumerateFiles().Any() Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function

    '    ''' <summary>
    '    ''' Determines if a directory contains directories. Returns false if it does.
    '    ''' </summary>
    '    ''' <param name="pathDir">Path to the directory to check.</param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function DirContainsDirs(ByVal pathDir As String) As Boolean
    '        Dim myDir As DirectoryInfo = New DirectoryInfo(pathDir)

    '        If myDir.EnumerateFiles().Any() Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function
    '#End Region

    '#Region "Naming"
    '    ''' <summary>
    '    ''' Renames a file.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to file to renamed, including the file name and extension.</param>
    '    ''' <param name="myNewName">New file name and extension.</param>
    '    ''' <remarks></remarks>
    '    Sub RenameFile(ByVal myPath As String, ByVal myNewName As String)
    '        If GetSuffix(myPath, "\") = myNewName Then
    '            Exit Sub                                'Exits sub if name already exists
    '        End If

    '        Try
    '            My.Computer.FileSystem.RenameFile(myPath, myNewName)
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Renames a folder.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to folder to renamed, including the folder name.</param>
    '    ''' <param name="myNewName">New folder name.</param>
    '    ''' <remarks></remarks>
    '    Sub RenameFolder(ByVal myPath As String, ByVal myNewName As String)
    '        Try
    '            My.Computer.FileSystem.RenameDirectory(myPath, myNewName)
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub
    '#End Region

    '#Region "Read Only Attributes"
    '    ''' <summary>
    '    ''' Sets all files in a folder and subfolders to not be read only.
    '    ''' </summary>
    '    ''' <param name="SourceFolderName">Path to the highest level folder to apply the readable overwrite to.</param>
    '    ''' <remarks>'Modified from Leith Ross http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html </remarks>
    '    Sub AllFilesNotReadOnly(ByVal SourceFolderName As String)
    '        Dim FSO As New Scripting.FileSystemObject
    '        Dim SourceFolder As Scripting.Folder
    '        Dim SubFolder As Scripting.Folder
    '        Dim FileItem As Scripting.File

    '        If Not DirectoryExists(SourceFolderName) Then Exit Sub
    '        SourceFolder = FSO.GetFolder(SourceFolderName)

    '        For Each FileItem In SourceFolder.Files
    '            ComponentSetAttributeAction(FileItem.Path, FileAttributes.Normal)       'Sets each file to not be read only
    '            'SetAttr(FileItem.Path, vbNormal)       'Sets each file to not be read only
    '        Next FileItem
    '        For Each SubFolder In SourceFolder.SubFolders
    '            AllFilesNotReadOnly(SubFolder.path)
    '        Next SubFolder

    '        FileItem = Nothing
    '        SourceFolder = Nothing
    '        FSO = Nothing

    '    End Sub

    '    ''' <summary>
    '    ''' Sets a specified file to not be read only.
    '    ''' </summary>
    '    ''' <param name="myPath">File path, including the filename</param>
    '    ''' <remarks></remarks>
    '    Sub SingleFileNotReadOnly(ByVal myPath As String)
    '        Dim sourceFolderName As String
    '        Dim fileName As String
    '        Dim FSO As New Scripting.FileSystemObject
    '        Dim SourceFolder As Scripting.Folder
    '        Dim FileItem As Scripting.File

    '        sourceFolderName = GetPathDirectoryStub(myPath)
    '        fileName = GetPathFileName(myPath)

    '        If Not DirectoryExists(sourceFolderName) Then Exit Sub
    '        SourceFolder = FSO.GetFolder(sourceFolderName)

    '        For Each FileItem In SourceFolder.Files
    '            If CStr(FileItem.Name).ToUpper = fileName.ToUpper Then ComponentSetAttributeAction(FileItem.Path, FileAttributes.Normal) 'Sets each file to not be read only
    '            'If CStr(FileItem.Name).ToUpper = fileName.ToUpper Then SetAttr(FileItem.Path, vbNormal) 'Sets each file to not be read only
    '        Next FileItem

    '        FileItem = Nothing
    '        SourceFolder = Nothing
    '        FSO = Nothing

    '    End Sub
    '#End Region

    '#Region "Deleting"
    '    ''' <summary>
    '    ''' Deletes a single file of a specified path.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file to be deleted.</param>
    '    ''' <param name="includeReadOnly">True: Removes read-only protection. False: Read-only files will not be deleted.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteFile(ByVal myPath As String, Optional ByVal includeReadOnly As Boolean = False)

    '        If FileExists(myPath) Then
    '            Try
    '                If includeReadOnly Then                                                 'Remove read-only attributes & delete file
    '                    ComponentSetAttributeAction(myPath, FileAttributes.Normal)
    '                    ComponentDeleteFileAction(myPath)
    '                Else
    '                    If Not File.GetAttributes(myPath) = FileAttributes.ReadOnly Then    'Check for read-only status & skip files that have them
    '                        If Not File.GetAttributes(myPath) = 33 Then
    '                            ComponentDeleteFileAction(myPath)
    '                        End If
    '                    End If
    '                End If

    '            Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '            End Try
    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Deletes all files fitting the specified criteria. If no file name or extension is given, then all files will be deleted.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the parent folder containing files to be deleted.</param>
    '    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. False: Files will only be deleted in the specified folder.</param>
    '    ''' <param name="myFileName">Optional: All files of this name will be deleted. Combine with myFileExtension to limit this to files of a particular name and type.</param>
    '    ''' <param name="myFileExtension">Optional: All files of this extension will be deleted.</param>
    '    ''' <param name="includeReadOnly">Optional: True: Read only files will also be deleted. False: Read only files will be skipped.</param>
    '    ''' <param name="fileNamesExclude">Optional: List of filenames to preserve during the delete process.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteFiles(ByVal myPath As String, ByVal includeSubFolders As Boolean, Optional ByVal myFileName As String = "", Optional ByVal myFileExtension As String = "", Optional ByVal includeReadOnly As Boolean = False, Optional ByVal fileNamesExclude As List(Of String) = Nothing)
    '        'Get list of files to delete
    '        Dim deleteList As New List(Of String)
    '        Dim cursorWait As New cCursorWait

    '        deleteList = ListFilePathsInDirectory(myPath, includeSubFolders, myFileName, myFileExtension)
    '        If IsNothing(deleteList) Then Exit Sub

    '        'Filter out files to save from the delete list
    '        If Not IsNothing(fileNamesExclude) Then
    '            Dim deleteListTemp As New List(Of String)
    '            Dim deleteFile = True

    '            For Each fileToDelete As String In deleteList
    '                deleteFile = True
    '                For Each saveFile As String In fileNamesExclude
    '                    If GetPathFileName(fileToDelete) = saveFile Then
    '                        deleteFile = False
    '                        Exit For
    '                    End If
    '                Next

    '                If deleteFile Then deleteListTemp.Add(fileToDelete)
    '            Next

    '            deleteList = deleteListTemp
    '        End If

    '        'Delete all files in the list
    '        For Each myFileDelete In deleteList
    '            If includeReadOnly Then
    '                'SetAttr(myFileDelete, vbNormal)
    '                ComponentSetAttributeAction(myFileDelete, FileAttributes.Normal)
    '                ComponentDeleteFileAction(myFileDelete)
    '            Else
    '                'Check if file is read only, and if so, skip it
    '                If FileExists(myFileDelete) Then
    '                    If Not File.GetAttributes(myFileDelete) = FileAttributes.ReadOnly Then
    '                        If Not File.GetAttributes(myFileDelete) = 33 Then
    '                            ComponentDeleteFileAction(myFileDelete)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next

    '        cursorWait.EndCursor()
    '    End Sub

    '    ''' <summary>
    '    ''' Deletes all files fitting the specified criteria lists. If no file name or extension is given, then all files will be deleted.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the parent folder containing files to be deleted.</param>
    '    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. False: Files will only be deleted in the specified folder.</param>
    '    ''' <param name="myFileNames">Optional: All files of the names in this list will be deleted. Combine with myFileExtensions to limit this to files of a particular name and type.</param>
    '    ''' <param name="myFileExtensions">Optional: All files of the extensions in this list will be deleted.</param>
    '    ''' <param name="includeReadOnly">True: Read only files will also be deleted. False: Read only files will be skipped.</param>
    '    ''' <param name="partialNameMatch">Optional: If true, a file will be considered a match for deletion by filename if the name is at least present in the overall filename. If false, only exact matches will be considered.</param>
    '    ''' <param name="waitCursor">If true, the cursor changes to a wait cursor while the function runs.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteFilesBulk(ByVal myPath As String, ByVal includeSubFolders As Boolean, Optional ByVal myFileNames As List(Of String) = Nothing, Optional ByVal myFileExtensions As List(Of String) = Nothing, _
    '                        Optional ByVal includeReadOnly As Boolean = False, Optional ByVal partialNameMatch As Boolean = False, Optional waitCursor As Boolean = False)
    '        'Get list of files to delete
    '        Dim deleteList As New List(Of String)
    '        Dim cursorWait As New cCursorWait(waitCursor)

    '        If IsNothing(myFileNames) Then
    '            myFileNames = New List(Of String)
    '            myFileNames.Add("")
    '        End If
    '        If IsNothing(myFileExtensions) Then
    '            myFileExtensions = New List(Of String)
    '            myFileExtensions.Add("")
    '        End If

    '        'Get list of all files in the folders & subfolders (if specified)
    '        deleteList = ListFilePathsInDirectory(myPath, includeSubFolders)
    '        If IsNothing(deleteList) Then Exit Sub

    '        'Delete all files in the list
    '        For Each myFileDelete In deleteList
    '            If includeReadOnly Then
    '                'SetAttr(myFileDelete, vbNormal)
    '                For Each myFileName As String In myFileNames
    '                    For Each myFileExtension As String In myFileExtensions
    '                        If FileNameExtensionMatch(myFileDelete, myFileName, myFileExtension, partialNameMatch) Then
    '                            ComponentSetAttributeAction(myFileDelete, FileAttributes.Normal)
    '                            ComponentDeleteFileAction(myFileDelete)
    '                        End If
    '                    Next
    '                Next
    '            Else
    '                'Check if file is read only, and if so, skip it
    '                For Each myFileName As String In myFileNames
    '                    For Each myFileExtension As String In myFileExtensions
    '                        If FileNameExtensionMatch(myFileDelete, myFileName, myFileExtension, partialNameMatch) Then
    '                            If FileExists(myFileDelete) Then
    '                                If Not File.GetAttributes(myFileDelete) = FileAttributes.ReadOnly Then
    '                                    If Not File.GetAttributes(myFileDelete) = 33 Then
    '                                        ComponentDeleteFileAction(myFileDelete)
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Next
    '        cursorWait.EndCursor()
    '    End Sub

    '    ''' <summary>
    '    ''' Deletes all files and folders.
    '    ''' Specifications allow the root folder to be preserved. 
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the root folder.</param>
    '    ''' <param name="deleteRootFolder">True: The root folder and all files and folders within will be deleted. False: Root folder is preserved but will be empty.</param>
    '    ''' <param name="myFolderName">Optional: Name of a specific folder to delete.</param>
    '    ''' <param name="includeReadOnly">True: Read only files and folders will also be deleted. False: Read only files and folders will be skipped.</param>
    '    ''' <param name="foldersPreserve">Optional: List of folder paths to preserve during the deleting process. Only works if not deleting the root folder.</param>
    '    ''' <remarks></remarks>
    '    Sub DeleteAllFilesFolders(ByVal myPath As String, ByVal deleteRootFolder As Boolean, Optional ByVal myFolderName As String = "", Optional ByVal includeReadOnly As Boolean = False, Optional ByVal foldersPreserve As List(Of String) = Nothing)
    '        Dim deleteList As New List(Of String)
    '        Dim cursorWait As New cCursorWait

    '        'Delete folders & subdirectories
    '        If deleteRootFolder Then
    '            deleteList.Add(myPath)
    '        Else
    '            'Delete files in root directory first
    '            DeleteFiles(myPath, False)

    '            'Create list of subdirectories to delete
    '            If IsNothing(foldersPreserve) Then
    '                deleteList = ListFoldersInFolder(myPath, myFolderName)
    '            Else
    '                Dim deleteListTemp = ListFoldersInFolder(myPath, myFolderName)

    '                For Each folderPreserve As String In foldersPreserve
    '                    For Each folderDelete As String In deleteListTemp
    '                        If Not folderDelete = folderPreserve Then deleteList.Add(folderDelete)
    '                    Next
    '                Next
    '            End If

    '        End If

    '        'If any folders are specified for deleting, delete folders
    '        AllFilesNotReadOnly(myPath)     'Currently function cannot skip 'read-only' portions
    '        'If includeReadOnly Then AllFilesNotReadOnly(myPath)

    '        If deleteList.Count > 0 Then
    '            For Each myFolderDelete In deleteList
    '                Try
    '                    ComponentDeleteDirectoryAction(myFolderDelete, True, True)
    '                Catch ex As Exception
    '                    'MyLogger
    '                    'This is to skip the subfolders, which have been deleted by deleting the parent folders
    '                End Try
    '            Next
    '        End If
    '        cursorWait.EndCursor()
    '    End Sub

    '#End Region

    '#Region "Copying/Moving"
    '    ''' <summary>
    '    ''' Copies a file from one directory to another. If directory does not exist, directory will be created.
    '    ''' </summary>
    '    ''' <param name="pathSource">File path source, including the file name.</param>
    '    ''' <param name="pathDestination">File path destination, including the file name. If filename is different, file will be renamed.</param>
    '    ''' <param name="overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    '    ''' <param name="includeReadOnly">Optional: True: Removes read-only protection. False: Read-only files will not be copied.</param>
    '    ''' <param name="promptMessage">Optional: Message to display to the user if a file exists.</param>
    '    ''' <param name="waitNewFileExist">Optional: If true, code will loop within this routine until the copy action has completed. Recommended if reading the copied file during same process as copying the file. Default is false.</param>
    '    ''' <param name="noSourceExistPrompt">Optional: If true, then if a source file does not exist for copying, the user is notified of this and that the copy action will not take place.</param>
    '    ''' <remarks></remarks>
    '    Sub CopyFile(ByVal pathSource As String, ByVal pathDestination As String, ByVal overWriteFile As Boolean, Optional ByVal includeReadOnly As Boolean = False, _
    '                 Optional ByVal promptMessage As String = "", Optional ByVal waitNewFileExist As Boolean = False, Optional ByVal noSourceExistPrompt As Boolean = False)
    '        Dim newFileExist As Boolean = False

    '        'Check that source file exists
    '        If Not FileExists(pathSource) Then
    '            If noSourceExistPrompt Then MessageBox.Show("Original file does not exist. File will not be copied")
    '            Exit Sub
    '        End If

    '        Try
    '            If Not FileExists(pathDestination) Then
    '                If includeReadOnly Then                                                 'Remove read-only attributes & delete file
    '                    ComponentSetAttributeAction(pathSource, FileAttributes.Normal)
    '                    If FileExists(pathDestination) Then ComponentSetAttributeAction(pathDestination, FileAttributes.Normal)
    '                    ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)
    '                Else
    '                    If Not File.GetAttributes(pathSource) = FileAttributes.ReadOnly Then    'Check for read-only status & skip files that have them
    '                        If Not File.GetAttributes(pathSource) = 3 Then
    '                            ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)
    '                        End If
    '                    End If
    '                End If
    '            Else
    '                If includeReadOnly Then                                                 'Remove read-only attributes & delete file
    '                    ComponentSetAttributeAction(pathSource, FileAttributes.Normal)
    '                    ComponentSetAttributeAction(pathDestination, FileAttributes.Normal)
    '                    ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)
    '                Else
    '                    If Not File.GetAttributes(pathSource) = FileAttributes.ReadOnly Then    'Check for read-only status & skip files that have them
    '                        If Not File.GetAttributes(pathSource) = 33 Then
    '                            If Not File.GetAttributes(pathSource) = 8225 Then
    '                                If FileExists(pathDestination) Then                             'Check destination for read-only status
    '                                    If Not File.GetAttributes(pathDestination) = FileAttributes.ReadOnly Then
    '                                        If Not File.GetAttributes(pathDestination) = 33 Then
    '                                            If Not File.GetAttributes(pathDestination) = 8225 Then
    '                                                ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)
    '                                            End If
    '                                        End If
    '                                    End If
    '                                Else
    '                                    ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            If waitNewFileExist Then
    '                Dim counter As Integer = 0
    '                'Wait until file copy action has been completed
    '                While Not newFileExist
    '                    If FileExists(pathDestination) Then newFileExist = True
    '                    System.Threading.Thread.Sleep(500)
    '                    counter += 1
    '                    If counter > 20 Then Exit While
    '                End While
    '            End If
    '        Catch ex As Exception
    'csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Makes a duplicate folder,including sub-folders.
    '    ''' If PathTarget already exists it will overwrite existing files in this folder. If PathTarget does not exist, it will be made for you.
    '    ''' </summary>
    '    ''' <param name="pathSource">Source directory path.</param>
    '    ''' <param name="pathDestination">Destination directory path.</param>
    '    ''' <param name="overWriteExisting">Optional: If 'true', then destination directory and files will be overwritten if they already exist. Default is 'false'.</param>
    '    ''' <remarks></remarks>
    '    Sub CopyFolder(ByVal pathSource As String, ByVal pathDestination As String, Optional ByVal overWriteExisting As Boolean = False)
    '        Dim waitCursor As Boolean = False

    '        'Check that source is valid
    '        If Not DirectoryExists(pathSource) Then
    '            MsgBox(pathSource & " doesn't exist. Directory will not be copied.")
    '            Exit Sub
    '        End If


    '        Dim cursorWait As New cCursorWait

    '        'Check if destination already exists
    '        If (DirectoryExists(pathDestination) AndAlso Not overWriteExisting) Then
    '            Select Case MessageBox.Show("Directory already exists and is set to not be overwritten. Do you wish to overwrite?", "Directory Location Already Exists", MessageBoxButton.YesNo, MessageBoxImage.Warning)
    '                Case MessageBoxResult.Yes 'Function will continue and overwrite the existing directory
    '                Case MessageBoxResult.No : Exit Sub
    '            End Select
    '        End If


    '        ComponentCopyDirectory(pathSource, pathDestination)

    '        cursorWait.EndCursor()
    '    End Sub

    '    ''' <summary>
    '    ''' Moves a file from a specified source to destination. May or may not remove the original file depending on parameter inputs.
    '    ''' </summary>
    '    ''' <param name="pathSource">Path to the original file.</param>
    '    ''' <param name="pathDestination">Path to where the original file is to be moved.</param>
    '    ''' <param name="deleteOriginal">True: Original file will be deleted after copy action. False: Original file will be left as is.</param>
    '    ''' <remarks></remarks>
    '    Sub MoveFile(ByVal pathSource As String, ByVal pathDestination As String, ByVal deleteOriginal As Boolean)
    '        If FileExists(pathSource) Then
    '            'Copy files & overwrite existing
    '            CopyFile(pathSource, pathDestination, True, True)

    '            'Delete originals, if specified
    '            If deleteOriginal Then DeleteFile(pathSource, True)
    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Moves a file from a specified source to destination. May or may not remove the original file depending on parameter inputs.
    '    ''' </summary>
    '    ''' <param name="pathSource">Path to the original folder.</param>
    '    ''' <param name="pathDestination">Path to where the original folder is to be moved.</param>
    '    ''' <param name="deleteOriginal">True: Original folder will be deleted after copy action. False: Original folder will be left as is.</param>
    '    ''' <remarks></remarks>
    '    Sub MoveFolder(ByVal pathSource As String, ByVal pathDestination As String, ByVal deleteOriginal As Boolean)
    '        If DirectoryExists(pathSource) Then
    '            'Copy files & overwrite existing
    '            CopyFolder(pathSource, pathDestination, True)

    '            'Delete originals, if specified
    '            If deleteOriginal Then DeleteAllFilesFolders(pathSource, True)
    '        End If
    '    End Sub
    '#End Region

    '#Region "Misc"
    '    ''' <summary>
    '    ''' Opens Windows Explorer at the specified directory
    '    ''' </summary>
    '    ''' <param name="myFolderPath">Path to the folder to be opened</param>
    '    ''' <param name="myErrorMessage">Optional error message if the folder does not exist at the specified location</param>
    '    ''' <remarks></remarks>
    '    Sub OpenExplorerAtFolder(ByVal myFolderPath As String, Optional myErrorMessage As String = "Folder Does Not Exist")
    '        'Similar for files. See: http://msdn.microsoft.com/en-us/library/system.io.directory.exists.aspx
    '        Try
    '            If Directory.Exists(myFolderPath) Then
    '                Try
    '                    Process.Start("explorer.exe", myFolderPath)
    '                Catch ex As Exception
    '                    csiLogger.ExceptionAction(ex)
    '                End Try
    '            Else
    '                MsgBox(myErrorMessage)
    '            End If
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Creates a database structure directory system for a given example name within a specified parent folder
    '    ''' </summary>
    '    ''' <param name="p_baseDir">Parent folder within which to create the database directory</param>
    '    ''' <param name="p_exampleName">Example name by which to name the database directory</param>
    '    ''' <remarks></remarks>
    '    Friend Function CreateDatabaseDirectory(ByVal p_baseDir As String,
    '                                            ByVal p_exampleName As String) As Boolean

    '        Dim dirPath As String = p_baseDir & "\" & p_exampleName

    '        If DirectoryExists(dirPath) Then
    '            Select Case MessageBox.Show("'" & p_exampleName & "'" & " cannot be gathered into a database structure because the following directory already exists: " & vbCrLf & vbCrLf & dirPath _
    '                                        & vbCrLf & vbCrLf & "Would you like to replace the existing directory? If not, example " & "'" & p_exampleName & "'" & " will be skipped.",
    '                                   "Directory Conflict", MessageBoxButton.YesNo, MessageBoxImage.Exclamation)
    '                Case MessageBoxResult.Yes : DeleteAllFilesFolders(dirPath, True)
    '                Case MessageBoxResult.No : Return False
    '            End Select
    '        End If

    '        ComponentCreateDirectory(dirPath)
    '        ComponentCreateDirectory(dirPath & "\" & DIR_NAME_MODELS_DEFAULT)
    '        ComponentCreateDirectory(dirPath & "\" & DIR_NAME_ATTACHMENTS_DEFAULT)
    '        ComponentCreateDirectory(dirPath & "\" & DIR_NAME_FIGURES_DEFAULT)

    '        Return True
    '    End Function

    '    ''' <summary>
    '    ''' Creates a temprorary directory of the specified path &amp; name. 
    '    ''' If a current directory exists, the name will either have an incremented number, or if at the max allowed, the highest numbered folder will be deleted first. 
    '    ''' The function returns the resulting path of the created folder.
    '    ''' </summary>
    '    ''' <param name="pathTemp">Initial path to use to create the temporary folder.</param>
    '    ''' <param name="numTempDirsMax">If creating a new temporary destination, more than one can exist if specified. 
    '    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    '    ''' <returns>The resulting path of the created folder.</returns>
    '    ''' <remarks></remarks>
    '    Function CreateTempDirectory(ByVal pathTemp As String, Optional ByVal numTempDirsMax As Integer = 1) As String
    '        Dim i As Integer
    '        For i = 1 To numTempDirsMax
    '            'Adjust new temp folder name if greater than 1
    '            If Not i = 1 Then
    '                pathTemp = pathTemp & "i"
    '            End If
    '            If Not DirectoryExists(pathTemp) Then                                        'Exit loop & create new folder
    '                Exit For
    '            Else
    '                If i = numTempDirsMax Then DeleteAllFilesFolders(pathTemp, True, , True) 'If the max folder number, delete the latest temp folder
    '            End If
    '        Next

    '        ComponentCreateDirectory(pathTemp, True)
    '        Return pathTemp
    '    End Function
    '#End Region


    '#Region "Component Functions"
    '    ''' <summary>
    '    ''' Component function. Copies a file. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="pathSource">File path source, including the file name.</param>
    '    ''' <param name="pathDestination">File path destination, including the file name. If filename is different, file will be renamed.</param>
    '    ''' <param name="overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    '    ''' <param name="promptMessage">Optional: Message to display to the user if a file exists.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentCopyFileAction(ByVal pathSource As String, ByVal pathDestination As String, ByVal overWriteFile As Boolean, Optional ByVal promptMessage As String = "", Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            If Not overWriteFile Then
    '                If FileExists(pathDestination) Then
    '                    If Not promptMessage = "" Then MessageBox.Show(promptMessage)
    '                    Exit Sub
    '                End If
    '            End If

    '            If FileExists(pathSource) Then My.Computer.FileSystem.CopyFile(pathSource, pathDestination, overWriteFile)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Component function. Deletes a file. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="myPath">Path of the file to be deleted.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentDeleteFileAction(ByVal myPath As String, Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            If FileExists(myPath) Then System.IO.File.Delete(myPath)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Component function. Deletes a directory. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="myPath">Path of the file to be deleted.</param>
    '    ''' <param name="removeOtherFilesDirectoriesInPath">Optional: If 'true', other files, directories, and subdirectories below this path will be deleted. Default is 'false'.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentDeleteDirectoryAction(ByVal myPath As String, Optional removeOtherFilesDirectoriesInPath As Boolean = False, Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            If DirectoryExists(myPath) Then System.IO.Directory.Delete(myPath, removeOtherFilesDirectoriesInPath)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Component function. Sets the attribute of a file. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="myPath">Path to the file to set the attributes of.</param>
    '    ''' <param name="attribute">Attribute to set the file to.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentSetAttributeAction(ByVal myPath As String, ByVal attribute As System.IO.FileAttributes, Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            If FileExists(myPath) Then File.SetAttributes(myPath, FileAttributes.Normal)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Component function. Creates a new directory. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="myPath">Path of the directory to be created.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentCreateDirectory(ByVal myPath As String, Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            Directory.CreateDirectory(myPath)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Component function. Copies a direcotry, or creates a new one if it does not exist at the destination. Includes error messages.
    '    ''' </summary>
    '    ''' <param name="myPathSource">Source directory path.</param>
    '    ''' <param name="myPathDestination">Destination directory path.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ComponentCopyDirectory(ByVal myPathSource As String, ByVal myPathDestination As String, Optional ByVal mySuppressExStates As Boolean = False)
    '        Try
    '            If DirectoryExists(myPathSource) Then My.Computer.FileSystem.CopyDirectory(myPathSource, myPathDestination, True)
    '        Catch ex As Exception
    '            csiLogger.ExceptionAction(ex, , mySuppressExStates)
    '        End Try
    '    End Sub

    '#End Region

    '#Region "Routines to Finish/Remove/Check"
    '    'TODO: Not Used Yet
    '    ''' <summary>
    '    ''' If the new directory specified does not match the existing directory, new folders are automatically generated
    '    ''' </summary>
    '    ''' <param name="myExampleDir">Name of the example directory.</param>
    '    ''' <param name="myNewDir">Name of the new directory. Unique portions of the path will create new folders.</param>
    '    ''' <param name="mySuppressExStates">Optional: If true and an exception occurs, informative prompts will be given. Default is to suppress the messages.</param>
    '    ''' <remarks></remarks>
    '    Sub ModifyDatabaseDirectory(ByVal myExampleDir As String, ByVal myNewDir As String, Optional ByVal mySuppressExStates As Boolean = True)
    '        Try
    '            Directory.CreateDirectory(myExampleDir & "\" & myNewDir)
    '        Catch ex As Exception
    '            If Not mySuppressExStates Then
    '                'myLogger
    '                MsgBox(ex.Message)
    '                MsgBox(ex.StackTrace)
    '            End If
    '        End Try
    '    End Sub

    '    'TODO: Currently does nothing
    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="mypath"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Function DirectoryIsFlattened(ByVal mypath As String) As Boolean
    '        DirectoryIsFlattened = True
    '        'TODO: 

    '    End Function

    '    'Not used. defunct? Remove?
    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="SourceFolderName">Path to the folder to be checked.</param>
    '    ''' <param name="IncludeSubfolders">True: Subfolders will also be checked.</param>
    '    ''' <remarks></remarks>
    '    Sub listModelFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
    '        'Modified from
    '        ' Leith Ross
    '        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
    '        '
    '        ' lists information about the files in SourceFolder
    '        ' example: ListFilesInFolder "C:\FolderName\", True
    '        '
    '        Dim FSO As New Scripting.FileSystemObject
    '        Dim SourceFolder As Scripting.Folder
    '        Dim SubFolder As Scripting.Folder
    '        Dim FileItem As Scripting.File
    '        Dim r As Long

    '        If Not DirectoryExists(SourceFolderName) Then Exit Sub
    '        SourceFolder = FSO.GetFolder(SourceFolderName)

    '        'Update - r = Range("A65536").End(xlUp).row + 1
    '        'r = 6


    '        For Each FileItem In SourceFolder.Files
    '            'display file properties
    '            If FileItem.name = "model.xml" Then
    '                'Update - Cells(r, 1).value = FileItem.name
    '                'Update - Cells(r, 2).value = FileItem.path

    '                '***** Remove the single ' character in the below lines to see this information *****
    '                'FileItem.Size
    '                'FileItem.DateCreated
    '                'FileItem.DateLastModified

    '                r = r + 1 ' next row number
    '            End If
    '        Next FileItem
    '        If IncludeSubfolders Then
    '            For Each SubFolder In SourceFolder.SubFolders
    '                listModelFilesInFolder(SubFolder.path, True)
    '            Next SubFolder
    '        End If

    '        FileItem = Nothing
    '        SourceFolder = Nothing
    '        FSO = Nothing

    '    End Sub

    '    'Not used. Template for such a function, but currently does not do anything. Remove?
    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="SourceFolderName">Path to the folder to be checked.</param>
    '    ''' <param name="IncludeSubfolders">True: Subfolders will also be checked.</param>
    '    ''' <remarks></remarks>
    '    Sub ListFilesInFolder(ByVal SourceFolderName As String, ByVal IncludeSubfolders As Boolean)
    '        'Modified from
    '        ' Leith Ross
    '        ' http://www.excelforum.com/excel-programming/645683-list-files-in-folder.html
    '        '
    '        ' lists information about the files in SourceFolder
    '        ' example: ListFilesInFolder "C:\FolderName\", True
    '        '
    '        Dim FSO As New Scripting.FileSystemObject
    '        Dim SourceFolder As Scripting.Folder
    '        Dim SubFolder As Scripting.Folder
    '        Dim FileItem As Scripting.File
    '        Dim r As Long

    '        If Not DirectoryExists(SourceFolderName) Then Exit Sub
    '        SourceFolder = FSO.GetFolder(SourceFolderName)
    '        'Update - r = Range("A65536").End(xlUp).row + 1
    '        For Each FileItem In SourceFolder.Files
    '            'display file properties
    '            'Update -   Cells(r, 1).Formula = FileItem.Name

    '            '***** Remove the single ' character in the below lines to see this information *****
    '            'FileItem.Path
    '            'FileItem.Size
    '            'FileItem.DateCreated
    '            'FileItem.DateLastModified

    '            r = r + 1 ' next row number
    '        Next FileItem
    '        If IncludeSubfolders Then
    '            For Each SubFolder In SourceFolder.SubFolders
    '                ListFilesInFolder(SubFolder.path, True)
    '            Next SubFolder
    '        End If

    '        FileItem = Nothing
    '        SourceFolder = Nothing
    '        FSO = Nothing

    '    End Sub
    '#End Region

End Module
