Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary

''' <summary>
''' Contains routines for working with file locations within folders. 
''' Used for file renaming, deleting, locating, relocating, etc.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class FileLibrary
    Shared Event Log(exception As LoggerEventArgs)
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Naming"
     ''' <summary>
    ''' Renames a file.
    ''' </summary>
    ''' <param name="path">Path to file to renamed, including the file name and extension.</param>
    ''' <param name="newName">New file name and extension.</param>
    ''' <remarks></remarks>
    Public Shared Sub RenameFile(ByVal path As String,
                                 ByVal newName As String)
        'Exits sub if name already exists
        If GetSuffix(path, "\") = newName Then Exit Sub
        
        Try
            My.Computer.FileSystem.RenameFile(path, newName)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() path), path,
                                               NameOfParam(Function() newName), newName))
        End Try
    End Sub
#End Region

#Region "Read Only Attributes"
    ''' <summary>
    ''' Sets a specified file to not be read only.
    ''' </summary>
    ''' <param name="path">File path, including the filename</param>
    ''' <remarks></remarks>
    Public Shared Sub SetFileNotReadOnly(ByVal path As String)
        ComponentRemoveAttributeAction(path, FileAttributes.ReadOnly)
    End Sub

    ''' <summary>
    ''' Checks if a file has any of the Read Only attributes assigned.
    ''' </summary>
    ''' <param name="path">Path to the file to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsFileReadOnly(ByVal path As String) As Boolean
        If Not File.Exists(path) Then Return False

        Dim infoReader As FileInfo = My.Computer.FileSystem.GetFileInfo(path)
        Return infoReader.IsReadOnly
    End Function

#End Region

#Region "Deleting"
    ''' <summary>
    ''' Deletes a single file of a specified path. 
    ''' Returns true if this results in the file no longer existing.
    ''' </summary>
    ''' <param name="path">Path to the file to be deleted.</param>
    ''' <param name="includeReadOnly">True: Removes read-only protection. 
    ''' False: Read-only files will not be deleted. 
    ''' Function will return False for read-only files.</param>
    ''' <remarks></remarks>
    Public Shared Function DeleteFile(ByVal path As String,
                                      Optional ByVal includeReadOnly As Boolean = False) As Boolean
        If Not File.Exists(path) Then Return True
        If (Not includeReadOnly AndAlso IsFileReadOnly(path)) Then Return False
        
        ComponentRemoveAttributeAction(path, FileAttributes.ReadOnly)
        ComponentDeleteFileAction(path)

        Return (Not File.Exists(path))
    End Function

    ''' <summary>
    ''' Deletes all files fitting the specified criteria. 
    ''' If no file name or extension is given, then all files will be deleted.
    ''' </summary>
    ''' <param name="rootPath">Path to the parent folder containing files to be deleted.</param>
    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. 
    ''' False: Files will only be deleted in the specified folder.</param>
    ''' <param name="fileName">All files of this name will be deleted. 
    ''' Combine with myFileExtension to limit this to files of a particular name and type.</param>
    ''' <param name="fileExtension">All files of this extension will be deleted.</param>
    ''' <param name="includeReadOnly">True: Read only files will also be deleted. 
    ''' False: Read only files will be skipped.</param>
    ''' <param name="fileNamesExclude">List of filenames to preserve during the delete process.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFiles(ByVal rootPath As String,
                                  ByVal includeSubFolders As Boolean,
                                  Optional ByVal fileName As String = "",
                                  Optional ByVal fileExtension As String = "",
                                  Optional ByVal includeReadOnly As Boolean = False,
                                  Optional ByVal fileNamesExclude As List(Of String) = Nothing)
        RaiseToggleAction.StartAction(NameOf(DeleteFiles))

        'TODO: Get just filenames from deleteList e.g. ListFileNamesInDirectory
        Dim pathsToDelete As List(Of String) = ListFilePathsInDirectory(rootPath, includeSubFolders, fileName, fileExtension)
        If pathsToDelete Is Nothing Then Exit Sub

        ' TODO: Fix this. Insufficient.
        If fileNamesExclude IsNot Nothing Then pathsToDelete = FilterListFromList(pathsToDelete, fileNamesExclude)

        For Each path In pathsToDelete
            DeleteFile(path, includeReadOnly:=includeReadOnly)
        Next

        RaiseToggleAction.EndAction(NameOf(DeleteFiles))
    End Sub

    ''' <summary>
    ''' Deletes all files fitting the specified criteria lists. 
    ''' If no file name or extension is given, then all files will be deleted.
    ''' </summary>
    ''' <param name="rootPath">Path to the parent folder containing files to be deleted.</param>
    ''' <param name="includeSubFolders">True: Files will be deleted in all subfolders. 
    ''' False: Files will only be deleted in the specified folder.</param>
    ''' <param name="fileNames">All files of the names in this list will be deleted. 
    ''' Combine with myFileExtensions to limit this to files of a particular name and type.</param>
    ''' <param name="fileExtensions">All files of the extensions in this list will be deleted.</param>
    ''' <param name="includeReadOnly">True: Read only files will also be deleted. 
    ''' False: Read only files will be skipped.</param>
    ''' <param name="partialNameMatch">True: A file will be considered a match for deletion by filename if the name is at least present in the overall filename. 
    ''' False: Only exact matches will be considered.</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteFilesBulk(ByVal rootPath As String,
                                      ByVal includeSubFolders As Boolean,
                                      Optional ByVal fileNames As List(Of String) = Nothing,
                                      Optional ByVal fileExtensions As List(Of String) = Nothing,
                                      Optional ByVal includeReadOnly As Boolean = False,
                                      Optional ByVal partialNameMatch As Boolean = False)
        If fileNames Is Nothing Then fileNames = New List(Of String) From {""}
        If fileExtensions Is Nothing Then fileExtensions = New List(Of String) From {""}
        
        RaiseToggleAction.StartAction(NameOf(DeleteFilesBulk))

        Dim deleteList As List(Of String) = ListFilePathsInDirectory(rootPath, includeSubFolders)
        For Each myFileDelete In deleteList
            For Each myFileName As String In fileNames
                For Each myFileExtension As String In fileExtensions
                    If FileNameExtensionsMatch(myFileDelete, myFileName, myFileExtension, partialNameMatch) Then
                        DeleteFile(myFileDelete, includeReadOnly:=includeReadOnly)
                    End If
                Next
            Next
        Next
        RaiseToggleAction.EndAction(NameOf(DeleteFilesBulk))
    End Sub
#End Region

#Region "Copying/Moving"
    ''' <summary>
    ''' Copies a file from one directory to another. 
    ''' If directory does not exist, directory will be created. 
    ''' Returns true if it is confirmed that a new file exists at the destination.
    ''' </summary>
    ''' <param name="pathSource">File path source, including the file name.</param>
    ''' <param name="pathDestination">File path destination, including the file name. 
    ''' If filename is different, file will be renamed.</param>
    ''' <param name="overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    ''' <param name="includeReadOnly">True: Removes read-only protection. 
    ''' False: Read-only files will not be copied.</param>
    ''' <param name="promptMessage">Message to display to the user if a file exists.</param>
    ''' <remarks></remarks>
    Public Shared Function CopyFile(ByVal pathSource As String,
                                    ByVal pathDestination As String,
                                    ByVal overWriteFile As Boolean,
                                    Optional ByVal includeReadOnly As Boolean = False,
                                    Optional ByVal promptMessage As String = "") As Boolean
        Dim fileCopied As Boolean = False

        'Check that source file exists
        If Not File.Exists(pathSource) Then
            RaiseEvent Messenger(New MessengerEventArgs("Original file does not exist. File will not be copied: {0}{1}",
                                                                        Environment.NewLine, pathSource))
            Return False
        End If

        ' Check if destination already exists
        If (File.Exists(pathDestination) AndAlso Not overWriteFile) Then
            ' TODO: Create seam to intercept this to handle programmatically
            Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Warning),
                                            "File already exists and is set to not be overwritten. Do you wish to overwrite?",
                                            "File Already Exists at Location")
                Case eMessageActions.Yes
                    ' Function will continue and overwrite the existing directory
                    overWriteFile = True
                Case eMessageActions.No
                    Return False
            End Select
        End If

        'Copy if appropriate
        If (Not includeReadOnly AndAlso
            (IsFileReadOnly(pathSource) OrElse
             IsFileReadOnly(pathDestination))) Then Return False

        RaiseToggleAction.StartAction(NameOf(CopyFile))

        SetFileNotReadOnly(pathSource)
        SetFileNotReadOnly(pathDestination)

        If (IsFileReadOnly(pathSource) OrElse
            IsFileReadOnly(pathDestination)) Then
            RaiseToggleAction.EndAction(NameOf(CopyFile))
            Return False
        End If 

        ComponentCopyFileAction(pathSource, pathDestination, overWriteFile, promptMessage)

        'Wait until file copy action has been completed, which is when a file exists at the destination
        For counter As Integer = 0 To 200
            If File.Exists(pathDestination) Then 
                fileCopied = True
                Exit For
            End If
            Threading.Thread.Sleep(50)
        Next

        RaiseToggleAction.EndAction(NameOf(CopyFile))
        Return fileCopied
    End Function

    ''' <summary>
    ''' Moves a file from a specified source to destination. 
    ''' May or may not remove the original file depending on parameter inputs.
    ''' Returns true if all desired operations were successful.
    ''' </summary>
    ''' <param name="pathSource">Path to the original file.</param>
    ''' <param name="pathDestination">Path to where the original file is to be moved.</param>
    ''' <param name="deleteOriginal">True: Original file will be deleted after copy action. 
    ''' False: Original file will be left as is.</param>
    ''' <param name="overWriteExisting">True: Destination directory and files will be overwritten if they already exist.</param>
    ''' <remarks></remarks>
    Public Shared Function MoveFile(ByVal pathSource As String,
                                    ByVal pathDestination As String,
                                    ByVal deleteOriginal As Boolean,
                                    Optional ByVal overWriteExisting As Boolean = False) As Boolean
        If Not File.Exists(pathSource) Then Return False

        RaiseToggleAction.StartAction(NameOf(MoveFile))

        CopyFile(pathSource, pathDestination, overWriteFile:=overWriteExisting, includeReadOnly:=True)

        If (deleteOriginal AndAlso File.Exists(pathDestination)) Then DeleteFile(pathSource, includeReadOnly:=True)
        RaiseToggleAction.EndAction(NameOf(MoveFile))

        If (deleteOriginal AndAlso File.Exists(pathSource)) Then Return False

        Return File.Exists(pathDestination)
    End Function
#End Region

#Region "Component Functions"
    ''' <summary>
    ''' Component function. Copies a file. Includes error messages.
    ''' </summary>
    ''' <param name="pathSource">File path source, including the file name.</param>
    ''' <param name="pathDestination">File path destination, including the file name. 
    ''' If filename is different, file will be renamed.</param>
    ''' <param name="overWriteFile">True: If file already exists at destination, file will be overwritten.</param>
    ''' <param name="promptMessage">Message to display to the user if a file exists.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentCopyFileAction(ByVal pathSource As String,
                                              ByVal pathDestination As String,
                                              ByVal overWriteFile As Boolean,
                                              Optional ByVal promptMessage As String = "")
        Try
            If StringsMatch(pathSource, pathDestination) Then Exit Sub

            If (Not overWriteFile AndAlso
                File.Exists(pathDestination) AndAlso
                Not String.IsNullOrEmpty(promptMessage)) Then
                RaiseEvent Messenger(New MessengerEventArgs(promptMessage))
                Exit Sub
            End If

            If File.Exists(pathSource) Then My.Computer.FileSystem.CopyFile(pathSource, pathDestination, overWriteFile)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex,
                                               NameOfParam(Function() pathSource), pathSource,
                                               NameOfParam(Function() pathDestination), pathDestination,
                                               NameOfParam(Function() overWriteFile), overWriteFile,
                                               NameOfParam(Function() promptMessage), promptMessage))
        End Try
    End Sub

    ''' <summary>
    ''' Component function. Deletes a file. Includes error messages.
    ''' </summary>
    ''' <param name="path">Path of the file to be deleted.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentDeleteFileAction(ByVal path As String)
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex, NameOfParam(Function() path), path))
        End Try
    End Sub


    ''' <summary>
    ''' Component function. Sets the attribute of a file.
    ''' </summary>
    ''' <param name="path">Path to the file to set the attributes of.</param>
    ''' <param name="attribute">Attribute to set the file to.</param>
    ''' <remarks></remarks>
    Public Shared Sub ComponentSetAttributeAction(ByVal path As String,
                                                  ByVal attribute As FileAttributes)
        If File.Exists(path) Then File.SetAttributes(path, attribute)
    End Sub

    ''' <summary>
    ''' Component function.
    ''' Removes the attribute of a file.
    ''' </summary>
    ''' <param name="path">Path to the file to set the attributes of.</param>
    ''' <param name="attribute">Attribute to set the file to.</param>
    Public Shared Sub ComponentRemoveAttributeAction(ByVal path As String,
                                                     ByVal attribute As FileAttributes)
        If Not File.Exists(path) Then Exit Sub

        Dim attributes As FileAttributes = File.GetAttributes(path)
        attributes = attributes And (Not attribute)
        File.SetAttributes(path, attributes)
    End Sub
#End Region

#Region "Helper Methods"
    
#End Region

End Class
