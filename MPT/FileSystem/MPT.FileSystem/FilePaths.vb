Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary

''' <summary>
''' Class representing a paths to files. 
''' Gathers paths from a folder and records them in list properties, including lists filtered by criteria.
''' </summary>
''' <remarks></remarks>
Public Class FilePaths

#Region "Properties"
    ''' <summary>
    ''' The source folder from which the file paths are generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FolderSource As String

    ''' <summary>
    ''' File paths to all files within a specified directory and all subdirectories.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PathsAll As New List(Of FilePath)

    ''' <summary>
    ''' File extension used to filter the list of paths to all files within a source folder and all sub-folders.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FileExtensionFilter As String

    ''' <summary>
    ''' File paths to all files in the pathsAll collection that are filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PathsFiltered As New List(Of FilePath)

    ''' <summary>
    ''' File paths to all files in the pathsAll collection that are selected to have XML files and examples generated for them.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PathsSelected As New List(Of FilePath)

#End Region

#Region "Initialization"
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="filePaths">List of file paths to add to the class.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal filePaths As List(Of String))
        InitializeFilePaths(filePaths)
    End Sub

    ''' <summary>
    ''' Autogenerates a list of all of the file paths contained within the directory path provided.
    ''' </summary>
    ''' <param name="directorySource">Directory path from which a list of file paths is to be generated.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal directorySource As String)
        InitializeFolderSource(directorySource)
    End Sub


    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="filePaths">List of file paths to add to the class.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFilePaths(ByVal filePaths As List(Of String))
        createPathObjects(filePaths)
    End Sub

    ''' <summary>
    ''' Autogenerates a list of all of the file paths contained within the directory path provided.
    ''' </summary>
    ''' <param name="directorySource">Directory path from which a list of file paths is to be generated.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFolderSource(ByVal directorySource As String)
        FolderSource = directorySource

        Dim filePaths As List(Of String) = ListFilePathsInDirectory(directorySource, True, , , False)
        If filePaths IsNot Nothing Then
            filePaths.Sort()
            createPathObjects(filePaths)
        End If
    End Sub

#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Removes the supplied example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="pathSelectedToRemove">Example path object to remove from the list.</param>
    ''' <remarks></remarks>
    Public Sub RemovePathSelected(ByVal pathSelectedToRemove As FilePath)
        Dim newPathsSelected As List(Of FilePath) = (From pathSelected In PathsSelected Where Not pathSelected.FileName = pathSelectedToRemove.FileName).ToList()

        PathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Removes the corresponding example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="fileNameSelectedToRemove">File name of the example path object to remove.</param>
    ''' <remarks></remarks>
    Public Sub RemovePathSelectedByFileName(ByVal fileNameSelectedToRemove As String)
        Dim newPathsSelected As List(Of FilePath) = (From pathSelected In PathsSelected Where Not pathSelected.FileName = fileNameSelectedToRemove).ToList()

        PathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Removes the corresponding example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="pathSelectedToRemove">Path of the example path object to remove.</param>
    ''' <remarks></remarks>
    Public Sub RemovePathSelectedByPath(ByVal pathSelectedToRemove As String)
        Dim newPathsSelected As List(Of FilePath) = (From pathSelected In PathsSelected Where Not pathSelected.Path = pathSelectedToRemove).ToList()

        PathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Updates the paths list with the filtered list of files. Also updates the relative paths list, for indicating files in subfolders.
    ''' </summary>
    ''' <param name="fileExtension">File extension used to filter the file paths.</param>
    ''' <param name="removeFilesWithFilter">Optional: If true, paths will be searched for XMl files that have corresponding model files. Those files will be removed.</param>
    ''' <remarks></remarks>
    Public Sub SetPathsFiltered(ByVal fileExtension As String,
                                Optional ByVal filesFilter As IFileFilter = Nothing,
                                Optional ByVal removeFilesWithFilter As Boolean = False)
        If String.IsNullOrEmpty(fileExtension) Then Exit Sub
        FileExtensionFilter = fileExtension

        'Generate list of files to ignore
        Dim matchingFiles As New List(Of FilePath)
        If filesFilter IsNot Nothing Then
            matchingFiles = filesFilter.MatchingFiles(PathsAll)
        End If

        'Update filtered list of path objects based on the file extension
        PathsFiltered = New List(Of FilePath)
        For Each myPathObject As FilePath In PathsAll
            If myPathObject.FileExtension IsNot Nothing Then
                If StringsMatch(myPathObject.FileExtension, fileExtension) Then
                    Dim filematch As Boolean = False

                    'Check against list of ignored files
                    If removeFilesWithFilter Then
                        If matchingFiles.Any(Function(file) file.Path = myPathObject.Path) Then
                            filematch = True
                        End If
                    End If
                    If Not filematch Then PathsFiltered.Add(myPathObject)
                End If
            End If
        Next

        'Get path stubs for any files in the parent folder that lie in different sub folders
        If PathsFiltered.Count > 0 Then
            GetSourceFolder()

            'Update relative paths, which indicates files that are in subfolders
            If Not String.IsNullOrEmpty(FolderSource) Then
                For Each pathFiltered As FilePath In PathsFiltered
                    pathFiltered.SetPathChildStub(FolderSource)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Creates a list of the path objects that are selected to be turned into examples.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetPathsSelected()
        PathsSelected = New List(Of FilePath)

        'Update filtered list of path objects based on the file extension
        For Each pathFiltered As FilePath In PathsFiltered
            If pathFiltered.IsSelected Then PathsSelected.Add(pathFiltered)
        Next

    End Sub


    ''' <summary>
    ''' Determine the shared parent directory of all filtered path objects. 
    ''' Checks all paths in the list of path objects and returns the longest path that is shared among all of the paths.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSourceFolder()
        If PathsFiltered.Count = 1 Then
            FolderSource = PathsFiltered(0).Directory
        ElseIf PathsFiltered.Count > 1 Then
            Dim directories As List(Of String) = (From pathFiltered In PathsFiltered Select pathFiltered.Directory).ToList()
            If directories.Count > 0 Then
                'Get longest path
                Dim maxCount As Integer
                Dim maxPath As String = ""
                For Each pathDir As String In directories
                    If maxCount < Len(pathDir) Then
                        maxCount = Len(pathDir)
                        maxPath = pathDir
                    End If
                Next

                Dim sharedPath As Boolean = True
                Dim currentCharacter As String
                For i = 1 To maxCount
                    Dim index As Integer = i
                    currentCharacter = Mid(maxPath, index, 1)
                    If directories.Any(Function(pathDir) Not currentCharacter = Mid(pathDir, index, 1)) Then
                        sharedPath = False
                    End If
                    If Not sharedPath Then
                        FolderSource = Left(maxPath, index - 1)
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Returns a list of all of the paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPathsFiltered() As IList(Of String)
        Return (From pathFiltered In PathsFiltered Select pathFiltered.Path).ToList()
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPathStubsFiltered() As IList(Of String)
        Return (From pathFiltered In PathsFiltered Select pathFiltered.PathChildStub).ToList()
    End Function

    ''' <summary>
    ''' Returns a list of all of the paths of the files selected to be turned into examples.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPathsSelected() As IList(Of String)
        Return (From pathSelected In PathsSelected Select pathSelected.Path).ToList()
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files selected.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPathStubsSelected() As IList(Of String)
        Return (From pathSelected In PathsSelected Select pathSelected.PathChildStub).ToList()
    End Function

    ''' <summary>
    ''' Returns a list of all of the file names of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFileNamesFiltered() As IList(Of String)
        Return (From pathFiltered In PathsFiltered Select pathFiltered.FileName).ToList()
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetRelativePathsFiltered() As IList(Of String)
        Dim relativePathsFiltered As New List(Of String)

        If PathsFiltered.Count = 1 Then
            relativePathsFiltered.Add(PathsFiltered(0).FileName)
        Else
            For Each pathFiltered As FilePath In PathsFiltered
                If String.IsNullOrEmpty(pathFiltered.PathChildStub) Then
                    relativePathsFiltered.Add(pathFiltered.FileName)
                Else
                    relativePathsFiltered.Add(pathFiltered.PathChildStub & "\" & pathFiltered.FileName)
                End If
            Next

        End If
        Return relativePathsFiltered
    End Function


    ''' <summary>
    ''' Checks if the current example path object contains a valid file path.
    ''' </summary>
    ''' <param name="filePath">Path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function IsValidFilePath(ByVal filePath As FilePath) As Boolean
        Return filePath.IsValidPath
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Creates the class objects associated with each path in a list.
    ''' </summary>
    ''' <param name="newFilePaths">List of file paths to create file objects from.</param>
    ''' <remarks></remarks>
    Protected Sub createPathObjects(ByVal newFilePaths As IEnumerable(Of String))
        For Each path As String In newFilePaths
            PathsAll.Add(New FilePath(path))
        Next
    End Sub
#End Region

End Class
