Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reflections.ReflectionLibrary

''' <summary>
''' Class that gathers paths from a folder and records them in list properties, including lists filtered by criteria.
''' </summary>
''' <remarks></remarks>
Public Class cPaths
   Inherits PropertyChanger

#Region "Properties"
    Private _folderSource As String
    ''' <summary>
    ''' The source folder from which the file paths are generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property folderSource As String
        Set(ByVal value As String)
            If Not _folderSource = value Then
                _folderSource = value
                RaisePropertyChanged(Function() Me.folderSource)
            End If
        End Set
        Get
            Return _folderSource
        End Get
    End Property

    ''' <summary>
    ''' File paths to all files within a specified directory and all subdirectories.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsAll As ObservableCollection(Of cPath)

    Private _fileExtensionFilter As String
    ''' <summary>
    ''' File extension used to filter the list of paths to all files within a source folder and all sub-folders.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileExtensionFilter As String
        Set(ByVal value As String)
            If Not _fileExtensionFilter = value Then
                _fileExtensionFilter = value
                RaisePropertyChanged(Function() Me.fileExtensionFilter)
            End If
        End Set
        Get
            Return _fileExtensionFilter
        End Get
    End Property

    ''' <summary>
    ''' File paths to all files in the pathsAll collection that are filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsFiltered As ObservableCollection(Of cPath)
#End Region


#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="filePathsTemp">List of file paths to add to the class.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal filePathsTemp As List(Of String))
        InitializeFilePaths(filePathsTemp)
    End Sub

    ''' <summary>
    ''' Autogenerates a list of all of the file paths contained within the directory path provided.
    ''' </summary>
    ''' <param name="myFolderSource">Directory path from which a list of file paths is to be generated.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal myFolderSource As String)
        InitializeFolderSource(myFolderSource)
    End Sub

    Private Sub InitializeData()
        pathsAll = New ObservableCollection(Of cPath)
        pathsFiltered = New ObservableCollection(Of cPath)

        'pathsAll = New ObservableCollection(Of String)
        'pathsFiltered = New ObservableCollection(Of String)
        'pathsFilteredStubs = New ObservableCollection(Of String)
        '_pathsFilteredStubs = New ObservableCollection(Of String)
        'fileNamesFiltered = New ObservableCollection(Of String)
    End Sub

    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="filePathsTemp">List of file paths to add to the class.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFilePaths(ByVal filePathsTemp As List(Of String))
        InitializeData()

        CreatePathObjects(filePathsTemp)

        'For Each myPath As String In filePathsTemp
        '    pathsAll.Add(myPath)
        'Next
    End Sub

    ''' <summary>
    ''' Autogenerates a list of all of the file paths contained within the directory path provided.
    ''' </summary>
    ''' <param name="myFolderSource">Directory path from which a list of file paths is to be generated.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFolderSource(ByVal myFolderSource As String)
        Dim filePathsTemp As New List(Of String)

        InitializeData()

        folderSource = myFolderSource
        filePathsTemp = ListFilePathsInDirectory(myFolderSource, True, , , False)

        If filePathsTemp IsNot Nothing Then CreatePathObjects(filePathsTemp)
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Creates the class objects associated with each path in a list.
    ''' </summary>
    ''' <param name="filePathsTemp"></param>
    ''' <remarks></remarks>
    Private Sub CreatePathObjects(ByVal filePathsTemp As List(Of String))
        For Each myPath As String In filePathsTemp
            Dim myPathObject As New cPath(myPath)
            pathsAll.Add(myPathObject)
        Next
    End Sub

    ''' <summary>
    ''' Updates the paths list with the filtered list of files. Also updates the relative paths list, for indicating files in subfolders.
    ''' </summary>
    ''' <param name="myFileExtension">File extension used to filter the file paths.</param>
    ''' <remarks></remarks>
    Private Sub SetPathsFiltered(ByVal myFileExtension As String)
        fileExtensionFilter = myFileExtension

        'Update filtered list of path objects based on the file extension
        For Each myPathObject As cPath In pathsAll
            If myPathObject.fileExtension IsNot Nothing Then
                If StringsMatch(myPathObject.fileExtension, myFileExtension) Then
                    pathsFiltered.Add(myPathObject)
                End If
            End If
        Next
        If pathsFiltered.Count > 0 Then
            GetSourceFolder()
            If Not String.IsNullOrEmpty(folderSource) Then                       'Update relative paths, which indicates files that are in subfolders
                For Each pathFiltered As cPath In pathsFiltered
                    pathFiltered.SetPathChildStub(folderSource)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Checks all paths in the list of path objects and returns the longest path that is shared among all of the paths.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSourceFolder()
        'Determine the shared parent directory of all filtered path objects
        Dim tempList As New List(Of String)
        Dim sharedPath As Boolean = True
        Dim currentCharacter As String = ""
        Dim maxCount As Integer
        Dim maxPath As String = ""

        For Each pathFiltered As cPath In pathsFiltered
            tempList.Add(pathFiltered.directory)
        Next
        If tempList.Count > 0 Then
            'Get longest path
            For Each pathDir As String In tempList
                If maxCount < Len(pathDir) Then
                    maxCount = Len(pathDir)
                    maxPath = pathDir
                End If

            Next

            For i = 1 To maxCount
                currentCharacter = Mid(maxPath, i, 1)
                For Each pathDir As String In tempList
                    If Not currentCharacter = Mid(pathDir, i, 1) Then
                        sharedPath = False
                        Exit For
                    End If
                Next
                If Not sharedPath Then
                    folderSource = Left(maxPath, i - 1)
                    Exit For
                End If
            Next
        End If
    End Sub
#End Region

End Class
