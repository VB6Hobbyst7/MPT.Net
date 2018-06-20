Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary
Imports MPT.FileSystem.FileLibrary

''' <summary>
''' Contains routines for working with folders locations within folders. 
''' Used for folder renaming, deleting, locating, relocating, etc.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class FolderLibrary
    Shared Event Log(exception As LoggerEventArgs)
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Querying"
    ''' <summary>
    ''' Performs a variety of folder and file creation and deletion tests to see if the program has read/write access to a folder.
    ''' </summary>
    ''' <param name="pathDirectory">Path of the directory that is being checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReadableWriteableDeletableDirectory(ByVal pathDirectory As String) As Boolean
        If Not Directory.Exists(pathDirectory) Then Return False

        Try
            Dim testPath As String = Path.Combine(pathDirectory, "ReadableWriteableDeletableDirectory")

            'Test Folder Creation
            ComponentCreateDirectory(testPath)
            If Not Directory.Exists(testPath) Then Return False

            'Test Folder Deletion
            ComponentDeleteDirectoryAction(testPath, removeOtherFilesDirectoriesInPath:= True)
            If Directory.Exists(testPath) Then Return False

            'Test File Creation
            testPath = Path.Combine(pathDirectory, "ReadableWriteableDeletableDirectory.ini")
            Using objWriter As New StreamWriter(testPath)
                objWriter.WriteLine("$ " & "C:\Verification")
            End Using
            If Not File.Exists(testPath) Then Return False

            'Test File Delete
            ComponentDeleteFileAction(testPath)
            If File.Exists(testPath) Then Return False
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() pathDirectory), pathDirectory))
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' True if a directory contains files.
    ''' This does not include subdirectories.
    ''' </summary>
    ''' <param name="pathDirectory">Path to the directory to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DirectoryContainsFiles(ByVal pathDirectory As String) As Boolean
        If (Not Directory.Exists(pathDirectory)) Then Return False

        Dim dirInfo As New DirectoryInfo(pathDirectory)

        Return dirInfo.EnumerateFiles().Any()
    End Function

    ''' <summary>
    ''' True if a directory contains directories.
    ''' This does not include subdirectories.
    ''' </summary>
    ''' <param name="pathDirectory">Path to the directory to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DirectoryContainsDirectories(ByVal pathDirectory As String) As Boolean
        If (Not Directory.Exists(pathDirectory)) Then Return False

        Dim dirInfo As New DirectoryInfo(pathDirectory)

        Return dirInfo.EnumerateDirectories().Any()
    End Function
#End Region

#Region "Naming"
    ''' <summary>
    ''' Renames a folder.
    ''' </summary>
    ''' <param name="path">Path to folder to renamed, including the folder name.</param>
    ''' <param name="newname">New folder name.</param>
    ''' <remarks></remarks>
    Public Shared Sub RenameFolder(ByVal path As String,
                                   ByVal newname As String)
         'Exits sub if name already exists
        If GetSuffix(path, "\") = newName Then Exit Sub
        
        Try
            My.Computer.FileSystem.RenameDirectory(path, newname)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() path), path,
                                               NameOfParam(Function() newname), newname))
        End Try
    End Sub
#End Region

#Region "Read Only Attributes"
    ''' <summary>
    ''' Sets all files in a folder and subfolders to not be read only.
    ''' </summary>
    ''' <param name="rootPath">Path to the highest level folder to apply the readable overwrite to.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetDirectoryFilesNotReadOnly(ByVal rootPath As String)
        Dim files As List(Of String) = ListFilePathsInDirectory(rootPath, includeSubDirectories:=True)

        'Sets each file to not be read only
        For Each file As String In files
            SetFileNotReadOnly(file)
        Next
    End Sub

#End Region

#Region "Deleting"
    ''' <summary>
    ''' Deletes all files and folders, including Read Only.
    ''' Specifications allow the root folder and other specified folders to be preserved. 
    ''' </summary>
    ''' <param name="rootPath">Path to the root folder.</param>
    ''' <param name="includeRootFolder">True: The root folder and all files and folders within will be deleted. 
    ''' False: Root folder is preserved but will be empty.</param>
    ''' <param name="folderName">Name of a specific folder to delete.</param>
    ''' <param name="foldersPreserve">List of folder paths to preserve during the deleting process. 
    ''' Only works if not deleting the root folder.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteAllFilesAndFolders(ByVal rootPath As String,
                                               ByVal includeRootFolder As Boolean,
                                               Optional ByVal folderName As String = "",
                                               Optional ByVal includeReadOnly As Boolean = True,
                                               Optional ByVal foldersPreserve As List(Of String) = Nothing)
       
        RaiseToggleAction.StartAction(NameOf(DeleteAllFilesAndFolders))
        
        If includeReadOnly Then
            DeleteAllFilesAndFoldersIncludingReadOnly(rootPath, includeRootFolder, folderName, foldersPreserve)
        Else 
            DeleteAllFilesAndFoldersNotReadOnly(rootPath, includeRootFolder, folderName, foldersPreserve)
        End If 
        
        RaiseToggleAction.EndAction(NameOf(DeleteAllFilesAndFolders))
    End Sub

    Private Shared Sub DeleteAllFilesAndFoldersIncludingReadOnly(ByVal rootPath As String,
                                                                ByVal includeRootFolder As Boolean,
                                                                Optional ByVal folderName As String = "",
                                                                Optional ByVal foldersPreserve As List(Of String) = Nothing)
        Dim deleteListTotal As List(Of String) = GetDeleteList(rootPath, includeRootFolder, folderName, foldersPreserve)
        SetDirectoryFilesNotReadOnly(rootPath)

        'Delete files in root directory first
        DeleteFiles(rootPath, includeSubFolders:=False)

        If deleteListTotal.Count > 0 Then
            For Each pathDelete In deleteListTotal
                ComponentDeleteDirectoryAction(pathDelete, removeOtherFilesDirectoriesInPath:=True)
            Next
        End If
    End Sub

    Private Shared Sub DeleteAllFilesAndFoldersNotReadOnly(ByVal rootPath As String,
                                                            ByVal includeRootFolder As Boolean,
                                                            Optional ByVal folderName As String = "",
                                                            Optional ByVal foldersPreserve As List(Of String) = Nothing)
        ' TODO Finish
        Dim foldersWithReadOnlyFiles As New List(Of String)
        ' Populate

        foldersPreserve.AddRange(foldersWithReadOnlyFiles)
        Dim doNotIncludeRootFolder As Boolean = False
        Dim deleteListTotal As List(Of String) = GetDeleteList(rootPath, doNotIncludeRootFolder, folderName, foldersPreserve)
        
        If deleteListTotal.Count > 0 Then
            For Each pathDelete In deleteListTotal
                ComponentDeleteDirectoryAction(pathDelete, removeOtherFilesDirectoriesInPath:=True)
            Next
        End If

        For Each pathDelete In foldersWithReadOnlyFiles
            ' Delete all files that are not readOnly
        Next
        
        'Delete files in root directory first
        DeleteFiles(rootPath, includeSubFolders:=False, includeReadOnly:=False)
    End Sub

    ''' <summary>
    ''' Returns a list of directories and subdirectories to delete.
    ''' </summary>
    ''' <param name="rootPath">Path to the root folder.</param>
    ''' <param name="includeRootFolder">True: The root folder and all files and folders within will be deleted. 
    ''' False: Root folder is preserved but will be empty.</param>
    ''' <param name="folderName">Name of a specific folder to delete.</param>
    ''' <param name="foldersPreserve">List of folder paths to preserve during the deleting process. 
    ''' Only works if not deleting the root folder.</param>
    ''' <returns></returns>
    Private Shared Function GetDeleteList(ByVal rootPath As String,
                                          ByVal includeRootFolder As Boolean,
                                          Optional ByVal folderName As String = "",
                                          Optional ByVal foldersPreserve As List(Of String) = Nothing) As List(Of String)
        Dim deleteListTotal As New List(Of String)

        If includeRootFolder Then
            ' Only the highest level path is needed for deleting.
            deleteListTotal.Add(rootPath)
            Return deleteListTotal
        End If

        ' Create list of subdirectories to delete.
        Dim subDirectriesDelete = ListDirectoriesByName(rootPath, folderName)
        If (foldersPreserve Is Nothing OrElse
            foldersPreserve.Count = 0) Then
            Return subDirectriesDelete
        End If

        ' Remove the paths to any folders that are to not be deleted.
        For Each folderPreserve As String In foldersPreserve
            For Each folderDelete As String In subDirectriesDelete
                If Not StringsMatch(folderDelete, folderPreserve) Then deleteListTotal.Add(folderDelete)
            Next
        Next
        Return deleteListTotal
    End Function

#End Region

#Region "Copying/Moving"
    ''' <summary>
    ''' Makes a duplicate folder,including sub-folders.
    ''' If the destination already exists it will overwrite existing files in this folder. 
    ''' If the destination does not exist, it will be made for you.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="pathSource">Source directory path.</param>
    ''' <param name="pathDestination">Destination directory path.</param>
    ''' <param name="overWriteExisting">True: Destination directory and files will be overwritten if they already exist.</param>
    ''' <remarks></remarks>
    Public Shared Function CopyFolder(ByVal pathSource As String,
                                      ByVal pathDestination As String,
                                      Optional ByVal overWriteExisting As Boolean = False) As Boolean
        ' Check that source is valid
        If Not Directory.Exists(pathSource) Then
            RaiseEvent Messenger(New MessengerEventArgs("{0} doesn't exist. Directory will not be copied.", pathSource))
            Return False
        End If

        ' Check if destination already exists
        If (Directory.Exists(pathDestination) AndAlso Not overWriteExisting) Then
            ' TODO: Create seam to intercept this to handle programmatically
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            "Directory already exists and is set to not be overwritten. Do you wish to overwrite?",
                                            "Directory Location Already Exists")
                Case eMessageActions.Yes
                    ' Function will continue and overwrite the existing directory
                Case eMessageActions.No
                    Return False
            End Select
        End If

        RaiseToggleAction.StartAction(NameOf(CopyFolder))
        ComponentCopyDirectory(pathSource, pathDestination)
        RaiseToggleAction.EndAction(NameOf(CopyFolder))

        Return Directory.Exists(pathDestination) 
    End Function

    ''' <summary>
    ''' Moves a file from a specified source to destination. 
    ''' May or may not remove the original file depending on parameter inputs.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="pathSource">Path to the original folder.</param>
    ''' <param name="pathDestination">Path to where the original folder is to be moved.</param>
    ''' <param name="deleteOriginal">True: Original folder will be deleted after copy action. 
    ''' False: Original folder will be left as is.</param>
    ''' <param name="overWriteExisting">True: Destination directory and files will be overwritten if they already exist.</param>
    ''' <remarks></remarks>
    Public Shared Function MoveFolder(ByVal pathSource As String,
                                     ByVal pathDestination As String,
                                     ByVal deleteOriginal As Boolean,
                                      Optional ByVal overWriteExisting As Boolean = False) As Boolean
        If Not Directory.Exists(pathSource) Then Return False

        CopyFolder(pathSource, pathDestination, overWriteExisting:=overWriteExisting)
        If deleteOriginal Then DeleteAllFilesAndFolders(pathSource, includeRootFolder:=True)
        If (deleteOriginal AndAlso Directory.Exists(pathSource)) Then Return False
        
        Return Directory.Exists(pathDestination) 
    End Function
#End Region

#Region "Misc"
    ''' <summary>
    ''' Creates a directory of the specified path &amp; name. 
    ''' If a current directory exists, the name will either have an incremented number, or if at the max allowed, the highest numbered folder will be deleted first. 
    ''' The function returns the resulting path of the created folder.
    ''' </summary>
    ''' <param name="path">Initial path to use to create the temporary folder.</param>
    ''' <param name="highestIncrementNumber"> &gt;= 1. If creating a new temporary destination, more than one can exist if specified. 
    ''' If a current directory exists, the last directory (i.e. the highest number permitted) will be deleted and replaced by a new, blank directory.</param>
    ''' <returns>The resulting path of the created folder.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateReplaceDirectoryAndIncrementNameIfDuplicate(ByVal path As String,
                                                                            Optional ByVal highestIncrementNumber As Integer = 1) As String
        If String.IsNullOrWhiteSpace(path) Then Return ""
        If highestIncrementNumber < 1 Then highestIncrementNumber = 1

        Dim newPath As String = path
        
        If Directory.Exists(path) Then
            ' Create a numbered folder
            For i = 1 To highestIncrementNumber
                newPath = path & i
                If Not Directory.Exists(newPath) Then Exit For

                'If the max folder number allowed is present, delete the latest temp folder
                If i = highestIncrementNumber Then DeleteAllFilesAndFolders(newPath, includeRootFolder:=True, includeReadOnly:=True)
            Next
        End If
        
        ComponentCreateDirectory(newPath)
        If Directory.Exists(newPath) Then
            Return newPath
        Else
            Return ""
        End If
    End Function
#End Region

#Region "Component Functions"
     ''' <summary>
    ''' Component function. Creates a new directory. Includes error messages.
    ''' </summary>
    ''' <param name="path">Path of the directory to be created.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCreateDirectory(ByVal path As String)
        Try
            Directory.CreateDirectory(path)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() path), path))
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Copies a direcotry, or creates a new one if it does not exist at the destination. Includes error messages.
    ''' </summary>
    ''' <param name="pathSource">Source directory path.</param>
    ''' <param name="pathDestination">Destination directory path.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCopyDirectory(ByVal pathSource As String,
                                             ByVal pathDestination As String)
        Try
            If Directory.Exists(pathSource) Then My.Computer.FileSystem.CopyDirectory(pathSource, pathDestination, Overwrite:= True)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                              NameOfParam(Function() pathSource), pathSource,
                                              NameOfParam(Function() pathDestination), pathDestination))
        End Try
    End Sub

     ''' <summary>
    ''' Component function. Deletes a directory. Includes error messages.
    ''' </summary>
    ''' <param name="path">Path of the file to be deleted.</param>
    ''' <param name="removeOtherFilesDirectoriesInPath">True: Other files, directories, and subdirectories below this path will be deleted.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentDeleteDirectoryAction(ByVal path As String,
                                                     Optional removeOtherFilesDirectoriesInPath As Boolean = False)
        Try
            If Directory.Exists(path) Then Directory.Delete(path, removeOtherFilesDirectoriesInPath)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() path), path,
                                               NameOfParam(Function() removeOtherFilesDirectoriesInPath), removeOtherFilesDirectoriesInPath))
        End Try
    End Sub
#End Region

#Region "Helper Methods"
    
#End Region
End Class
