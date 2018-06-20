Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.XML.ReaderWriter

''' <summary>
''' Class representing a paths to examples.
''' </summary>
''' <remarks></remarks>
Public Class cPathsExamples
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub RaisePropertyChanged(ByVal sender As Object, ByVal propertyName As String)
        RaiseEvent PropertyChanged(sender, New PropertyChangedEventArgs(propertyName))
    End Sub

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
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("folderSource"))
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
    Public Property pathsAll As New ObservableCollection(Of cPathExample)

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
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileExtensionFilter"))
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
    Public Property pathsFiltered As New ObservableCollection(Of cPathExample)

    ''' <summary>
    ''' File paths to all files in the pathsAll collection that are selected to have XML files and examples generated for them.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pathsSelected As New ObservableCollection(Of cPathExample)

#End Region

#Region "Initialization"
    Friend Sub New()

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


    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="filePathsTemp">List of file paths to add to the class.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFilePaths(ByVal filePathsTemp As List(Of String))
        CreatePathObjects(filePathsTemp)
    End Sub

    ''' <summary>
    ''' Autogenerates a list of all of the file paths contained within the directory path provided.
    ''' </summary>
    ''' <param name="myFolderSource">Directory path from which a list of file paths is to be generated.</param>
    ''' <remarks></remarks>
    Private Sub InitializeFolderSource(ByVal myFolderSource As String)
        Dim filePathsTemp As New List(Of String)

        folderSource = myFolderSource
        filePathsTemp = ListFilePathsInDirectory(myFolderSource, True, , , False)

        If filePathsTemp IsNot Nothing Then
            filePathsTemp.Sort()
            CreatePathObjects(filePathsTemp)
        End If
    End Sub

#End Region

#Region "Methods: Friend"

    ''' <summary>
    ''' Removes the supplied example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="p_pathSelectedToRemove">Example path object to remove from the list.</param>
    ''' <remarks></remarks>
    Friend Sub RemovePathSelected(ByVal p_pathSelectedToRemove As cPathExample)
        Dim newPathsSelected As New ObservableCollection(Of cPathExample)

        For Each pathSelected As cPathExample In pathsSelected
            If Not pathSelected.fileName = p_pathSelectedToRemove.fileName Then
                newPathsSelected.Add(pathSelected)
            End If
        Next

        pathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Removes the corresponding example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="p_fileNameSelectedToRemove">File name of the example path object to remove.</param>
    ''' <remarks></remarks>
    Friend Sub RemovePathSelectedByFileName(ByVal p_fileNameSelectedToRemove As String)
        Dim newPathsSelected As New ObservableCollection(Of cPathExample)

        For Each pathSelected As cPathExample In pathsSelected
            If Not pathSelected.fileName = p_fileNameSelectedToRemove Then
                newPathsSelected.Add(pathSelected)
            End If
        Next

        pathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Removes the corresponding example path object from the list of selected paths.
    ''' </summary>
    ''' <param name="p_pathSelectedToRemove">Path of the example path object to remove.</param>
    ''' <remarks></remarks>
    Friend Sub RemovePathSelectedByPath(ByVal p_pathSelectedToRemove As String)
        Dim newPathsSelected As New ObservableCollection(Of cPathExample)

        For Each pathSelected As cPathExample In pathsSelected
            If Not pathSelected.path = p_pathSelectedToRemove Then
                newPathsSelected.Add(pathSelected)
            End If
        Next

        pathsSelected = newPathsSelected
    End Sub

    ''' <summary>
    ''' Updates the paths list with the filtered list of files. Also updates the relative paths list, for indicating files in subfolders.
    ''' </summary>
    ''' <param name="p_fileExtension">File extension used to filter the file paths.</param>
    ''' <param name="p_removeFilesWithMCxml">Optional: If true, paths will be searched for XMl files that have corresponding model files. Those files will be removed.</param>
    ''' <remarks></remarks>
    Friend Sub SetPathsFiltered(ByVal p_fileExtension As String,
                                Optional ByVal p_removeFilesWithMCxml As Boolean = False)
        Dim filesWMcXml As New List(Of cPathExample)
        Dim filematch As Boolean

        If String.IsNullOrEmpty(p_fileExtension) Then Exit Sub

        fileExtensionFilter = p_fileExtension
        pathsFiltered = New ObservableCollection(Of cPathExample)

        'Generate list of files to ignore
        If p_removeFilesWithMCxml Then
            For Each myPathObject As cPathExample In pathsAll
                If cPathModelControl.IsModelControlXML(myPathObject.path) Then
                    Dim pathModelAssembled As String = ""

                    'Get the relative path to the corresponding model file
                    Dim xmlReader As New cXmlReadWrite
                    xmlReader.GetSingleXMLNodeValue(myPathObject.path, "//n:model/n:path", pathModelAssembled, , True)

                    'Append relative path to the directory of the MC XML file as this is the reference pt.
                    pathModelAssembled = GetPathDirectoryStub(myPathObject.path) & "\" & pathModelAssembled

                    'Check assembled path against those in the original complete list
                    For Each possibleModelFile As cPathExample In pathsAll
                        If possibleModelFile.path = pathModelAssembled Then
                            filesWMcXml.Add(possibleModelFile)
                            Exit For
                        End If
                    Next
                End If
            Next
        End If

        'Update filtered list of path objects based on the file extension
        For Each myPathObject As cPathExample In pathsAll
            If myPathObject.fileExtension IsNot Nothing Then
                If StringsMatch(myPathObject.fileExtension, p_fileExtension) Then
                    filematch = False
                    If p_removeFilesWithMCxml Then               'Check against list of ignored files
                        For Each myMCxmlModel As cPathExample In filesWMcXml
                            If myMCxmlModel.path = myPathObject.path Then
                                filematch = True
                                Exit For
                            End If
                        Next
                    End If
                    If Not filematch Then pathsFiltered.Add(myPathObject)
                End If
            End If
        Next

        'Get path stubs for any files in the parent folder that lie in different sub folders
        If pathsFiltered.Count > 0 Then
            GetSourceFolder()
            If Not String.IsNullOrEmpty(folderSource) Then                       'Update relative paths, which indicates files that are in subfolders
                For Each pathFiltered As cPathExample In pathsFiltered
                    pathFiltered.SetPathChildStub(folderSource)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Creates a list of the path objects that are selected to be turned into examples.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetPathsSelected()
        pathsSelected = New ObservableCollection(Of cPathExample)

        'Update filtered list of path objects based on the file extension
        For Each pathFiltered As cPathExample In pathsFiltered
            If pathFiltered.fileNameUse Then pathsSelected.Add(pathFiltered)
        Next

    End Sub

    ''' <summary>
    ''' Sets all matching file path object model control references to the corresponding model control in the list provided.
    ''' Matches are determined by the source of the model file.
    ''' </summary>
    ''' <param name="p_models">List of model control objects.</param>
    ''' <remarks></remarks>
    Friend Sub SetMCReferenceInFilePaths(ByRef p_models As cMCModels)
        For Each model As cMCModel In p_models
            For Each filePath As cPathExample In pathsSelected
                If StringsMatch(model.modelFile.pathSource.path, filePath.path) Then
                    filePath.SetMCModel(model, p_addListeners:=True)
                    filePath.dataSource.SetMCModel(model, p_addListeners:=True)
                    filePath.SetDefaultDataSourceDirectory()
                End If
            Next
        Next
    End Sub


    ''' <summary>
    ''' Checks all paths in the list of path objects and returns the longest path that is shared among all of the paths.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetSourceFolder()
        'Determine the shared parent directory of all filtered path objects
        Dim tempList As New List(Of String)
        Dim sharedPath As Boolean = True
        Dim currentCharacter As String = ""
        Dim maxCount As Integer
        Dim maxPath As String = ""

        If pathsFiltered.Count = 1 Then
            folderSource = pathsFiltered(0).directory
        ElseIf pathsFiltered.Count > 1 Then
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
        End If
    End Sub

    ''' <summary>
    ''' Returns a list of all of the paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetPathsFiltered() As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)

        For Each pathFiltered As cPathExample In pathsFiltered
            tempList.Add(pathFiltered.path)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetPathStubsFiltered() As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)

        For Each pathFiltered As cPathExample In pathsFiltered
            tempList.Add(pathFiltered.pathChildStub)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all of the paths of the files selected to be turned into examples.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetPathsSelected() As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)

        For Each pathSelected As cPathExample In pathsSelected
            tempList.Add(pathSelected.path)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files selected.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetPathStubsSelected() As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)

        For Each pathSelected As cPathExample In pathsSelected
            tempList.Add(pathSelected.pathChildStub)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all of the file names of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetFileNamesFiltered() As ObservableCollection(Of String)
        Dim tempList As New ObservableCollection(Of String)

        For Each pathFiltered As cPathExample In pathsFiltered
            tempList.Add(pathFiltered.fileName)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Returns a list of all of the relative paths of the files filtered by file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetRelativePathsFiltered() As ObservableCollection(Of String)
        Dim relativePathsFiltered As New ObservableCollection(Of String)

        If pathsFiltered.Count = 1 Then
            relativePathsFiltered.Add(pathsFiltered(0).fileName)
        Else
            For Each pathFiltered As cPathExample In pathsFiltered
                If String.IsNullOrEmpty(pathFiltered.pathChildStub) Then
                    relativePathsFiltered.Add(pathFiltered.fileName)
                Else
                    relativePathsFiltered.Add(pathFiltered.pathChildStub & "\" & pathFiltered.fileName)
                End If
            Next

        End If
        Return relativePathsFiltered
    End Function


    ''' <summary>
    ''' Checks if the current example path object contains a valid file path.
    ''' </summary>
    ''' <param name="p_path">Example path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsValidFilePath(ByVal p_path As cPathExample) As Boolean
        If p_path.isValidPath AndAlso p_path.dataSource.isValidPath Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Creates the class objects associated with each path in a list.
    ''' </summary>
    ''' <param name="filePathsTemp"></param>
    ''' <remarks></remarks>
    Private Sub CreatePathObjects(ByVal filePathsTemp As List(Of String))
        For Each myPath As String In filePathsTemp
            Dim myPathObject As New cPathExample(myPath)
            pathsAll.Add(myPathObject)
        Next
    End Sub
#End Region
End Class
