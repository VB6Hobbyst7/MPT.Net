Option Strict On
Option Explicit On

Imports Scripting

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting

Imports CSiTester.cMCModelID
Imports CSiTester.cMCModel

Friend Class cMCGenerator
    Inherits PropertyChanger
    Implements IMessengerEvent
    Implements ILoggerEvent

    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Prompts"
    Private Const _TITLE_BROWSE As String = "Model Control Files Parent Directory"

    Private Const _PROMPT_NO_MC_FILES_FOUND As String = "No Model Control files were found in the specified directory:"

    Private Const _TITLE_INVALID_MODEL_CONTROL_FILE As String = "Invalid Model Control Files"
    Private Const _PROMPT_INVALID_MODEL_CONTROL_FILE As String = "The following paths do not point to a valid model control file: " & vbNewLine & vbNewLine

    Private Const _TITLE_ILLEGAL_MODEL_ID As String = "Illegal Model IDs"
    Private Const _PROMPT_ILLEGAL_MODEL_ID As String = "One or more model files has an illegal model ID. "
    Private Const _FOOTER_ILLEGAL_MODEL_ID As String = "These models have had their IDs automatically set for the session." & vbNewLine & vbNewLine &
                                                                "Please review the model IDs."
#End Region

#Region "Fields"
    ''' <summary>
    ''' Determines if the model control object can be read in from a seed file.
    ''' </summary>
    ''' <remarks></remarks>
    Protected _canBeSeed As Boolean = False


#End Region

#Region "Properties"
    Private _folderSource As String
    ''' <summary>
    ''' Path to the folder containing files to be added as examples. Model control XML files are generated for these.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property folderSource As String
        Set(ByVal value As String)
            If Not _folderSource = value Then
                _folderSource = value
                RaisePropertyChanged("folderSource")
            End If
        End Set
        Get
            Return _folderSource
        End Get
    End Property

    ''' <summary>
    ''' File path objects associated with the generated model control objects.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property filePaths As New cPathsExamples

    ''' <summary>
    ''' Collection of model control objects generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property mcModels As New cMCModels

    ''' <summary>
    ''' Object that handles the validation and generatio of model IDs.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Property IDSchema As New cMCIDGenerator
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Initializes the object to allow reading of seed files.
    ''' </summary>
    ''' <param name="p_canBeSeed">Sets the object to allow reading of seed files.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_canBeSeed As Boolean)
        _canBeSeed = p_canBeSeed
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Populates the properties of the class based on the path specified.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function BrowseModelControlFiles() As Boolean
        Try
            Dim tempPath As String = ""
            tempPath = BrowseForFolder(_TITLE_BROWSE, PathStart(folderSource))
            If Not String.IsNullOrWhiteSpace(tempPath) Then
                _canBeSeed = False
                folderSource = tempPath
                Fill(folderSource)
                Return True
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Populates the properties of the class based on files found at the provided directory path.
    ''' </summary>
    ''' <param name="p_pathDir">Path to the directory to use for filling the object properties.</param>
    ''' <param name="p_invalidIDs">List of IDs that are not allowed to be used.</param>
    ''' <remarks></remarks>
    Friend Sub Fill(ByVal p_pathDir As String,
                    Optional ByVal p_invalidIDs As List(Of Decimal) = Nothing)
        Try
            If Not IO.Directory.Exists(p_pathDir) Then Exit Sub

            Dim mcFilePaths As New List(Of String)
            If Not ListModelControlFilesInFolder(p_pathDir, mcFilePaths, p_alertEmpty:=True) Then Exit Sub

            Dim invalidIDs As List(Of Decimal) = GenerateInvalidIDs(p_invalidIDs)
            Dim mcModelsTemp As New cMCModels

            GenerateModelControlCollection(mcModelsTemp, mcFilePaths, invalidIDs, p_alertIssues:=True)

            CreatePathsFiltered(p_pathDir, mcModelsTemp.item(0))

            SelectPathsAllMatchingFiles(mcModelsTemp)

            FinalizeModelControlAndPathReferenceProperties(mcModelsTemp)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Returns a list of paths for all model control files in the specified directory.
    ''' </summary>
    ''' <param name="p_dirPath">Path to the highest level folder to check.</param>
    ''' <param name="p_includeSubfolders">True: Subfolders are also checked for model control files.</param>
    Friend Shared Function ListModelControlFilesInFolders(ByVal p_dirPath As String,
                                                          Optional ByVal p_includeSubfolders As Boolean = True) As List(Of String)
        Dim mcFilesList As New List(Of String)

        If Not IO.Directory.Exists(p_dirPath) Then Return mcFilesList

        Dim FSO As New FileSystemObject
        Dim SourceFolder As Folder
        Dim SubFolder As Folder

        SourceFolder = FSO.GetFolder(p_dirPath)
        For Each fileItem As File In SourceFolder.Files
            If cPathModelControl.IsModelControlXML(fileItem.Path) Then mcFilesList.Add(fileItem.Path)
        Next fileItem

        If p_includeSubfolders Then
            For Each SubFolder In SourceFolder.SubFolders
                Dim subMCFilesList As List(Of String) = ListModelControlFilesInFolders(SubFolder.Path)

                For Each mcFilePath As String In subMCFilesList
                    mcFilesList.Add(mcFilePath)
                Next
            Next SubFolder
        End If

        Return mcFilesList
    End Function
#End Region


#Region "Methods: Private"
    ''' <summary>
    ''' Fills the provided list with paths to all model control files in the specifed path and sub-directories.
    ''' Return 'True' if at least one path is found.
    ''' </summary>
    ''' <param name="p_path">Path to the directory to search within.</param>
    ''' <param name="p_mcFilePaths">List to fill with model control file paths.</param>
    ''' <param name="p_alertEmpty">True: An alert will appear if no file paths are found.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function ListModelControlFilesInFolder(ByVal p_path As String,
                                                      ByRef p_mcFilePaths As List(Of String),
                                                      Optional ByVal p_alertEmpty As Boolean = False) As Boolean
        p_mcFilePaths = ListModelControlFilesInFolders(p_path)
        If p_mcFilePaths.Count = 0 Then
            If p_alertEmpty Then ShowAlertNoModelControlFilesFound(p_path)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Returns a complete list of all IDs that are invalid for model control objects being added.
    ''' </summary>
    ''' <param name="p_invalidIDs">List of invalid IDs to add to the defaults.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateInvalidIDs(ByVal p_invalidIDs As List(Of Decimal)) As List(Of Decimal)
        Dim incorrectIDs As New List(Of Decimal)
        If Not _canBeSeed Then incorrectIDs.Add(0)
        If p_invalidIDs IsNot Nothing Then
            For Each incorrectID As Decimal In p_invalidIDs
                incorrectIDs.Add(incorrectID)
            Next
        End If
        Return incorrectIDs
    End Function

    ''' <summary>
    ''' Generates a collectio of model control objects based o the file paths and ID constraints provided.
    ''' </summary>
    ''' <param name="p_mcModels">Collection to fill with model control objects.</param>
    ''' <param name="p_mcFilePaths">File paths to read from for the model control objects.</param>
    ''' <param name="p_invalidIDs">List ofmodel control IDs that are not allowed to be used.</param>
    ''' <param name="p_alertIssues">True: Alets will prompt the user of any issues and default corrections.</param>
    ''' <remarks></remarks>
    Protected Sub GenerateModelControlCollection(ByRef p_mcModels As cMCModels,
                                                 ByVal p_mcFilePaths As List(Of String),
                                                 Optional ByVal p_invalidIDs As List(Of Decimal) = Nothing,
                                                 Optional ByVal p_alertIssues As Boolean = False)
        Dim mcModelsIncorrectID As New cMCModels
        Dim invalidPaths As String = ""

        For Each mcFilePath As String In p_mcFilePaths
            Dim mcModel As New cMCModel(mcFilePath, p_alertInvalidPath:=True)
            Dim modelIDIsValid As Boolean = (Not AddModelControlIncorrectID(mcModel, mcModelsIncorrectID, p_invalidIDs))
            ' TODO: Do something similar for redundant IDs, but with user prompt first as to how to handle them.

            If (modelIDIsValid AndAlso
                (_canBeSeed OrElse
                 Not mcModel.isFromSeedFile)) Then
                p_mcModels.Add(mcModel)
            ElseIf p_alertIssues Then
                invalidPaths &= mcModel.mcFile.pathSource.path & Environment.NewLine & Environment.NewLine
            End If
        Next

        If (p_alertIssues AndAlso Not String.IsNullOrEmpty(invalidPaths)) Then ShowAlertIncorrectModelControlFiles(invalidPaths)

        AddMCModelsWithIncorrectIDs(p_mcModels, mcModelsIncorrectID, p_alert:=p_alertIssues)
    End Sub

    ''' <summary>
    ''' If the model control object has an incorrect model ID, it is added to the provided collection and the function returns 'True'.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to check.</param>
    ''' <param name="p_mcModelsIncorrectID">List of model control objects with incorrect IDs.</param>
    ''' <param name="p_incorrectIDs">List of IDs that are not permitted.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AddModelControlIncorrectID(ByRef p_mcModel As cMCModel,
                                                ByRef p_mcModelsIncorrectID As cMCModels,
                                                ByVal p_incorrectIDs As List(Of Decimal)) As Boolean
        If (p_incorrectIDs IsNot Nothing AndAlso
            p_incorrectIDs.Contains(p_mcModel.ID.idCompositeDecimal)) Then

            p_mcModelsIncorrectID.Add(p_mcModel)
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Adds the incorrect model control objects at the end of the collection for propert ID incrementing.
    ''' </summary>
    ''' <param name="p_mcModels">Model control collection to update.</param>
    ''' <param name="p_mcModelsIncorrectID">Collection of model control objects that have invalid IDs.</param>
    ''' <param name="p_alert">True: An informative alert prompt will appear.</param>
    ''' <remarks></remarks>
    Protected Sub AddMCModelsWithIncorrectIDs(ByRef p_mcModels As cMCModels,
                                              ByVal p_mcModelsIncorrectID As cMCModels,
                                              Optional ByVal p_alert As Boolean = False)
        If p_mcModelsIncorrectID.Count = 0 Then Exit Sub

        Dim invalidPaths As String = ""
        For Each mcModel As cMCModel In p_mcModelsIncorrectID
            p_mcModels.Add(mcModel, p_incrementID:=True)
            invalidPaths &= "Old ID: " & mcModel.ID.idComposite & Environment.NewLine &
                            "New ID: " & p_mcModels(p_mcModels.Count - 1).ID.idComposite & Environment.NewLine &
                            mcModel.mcFile.pathSource.path & Environment.NewLine & Environment.NewLine
        Next

        If p_alert Then ShowAlertIncorrectModelIDs(invalidPaths)
    End Sub


    ''' <summary>
    ''' Generate a list of file paths of only the file extension type for the model file associated with the model control object.
    ''' The corresponding path object is returned.
    ''' </summary>
    ''' <param name="p_pathDirSource">Directory from which to gather file paths.</param>
    ''' <param name="p_mcModel">Model control object to use for associated model file data.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreatePathsFiltered(ByVal p_pathDirSource As String,
                                        ByVal p_mcModel As cMCModel) As cPathModel
        filePaths = New cPathsExamples(p_pathDirSource)
        Dim modelFile As cPathModel = p_mcModel.modelFile.PathModelDestination

        filePaths.SetPathsFiltered(modelFile.fileExtension, p_removeFilesWithMCxml:=False)

        Return modelFile
    End Function

    ''' <summary>
    ''' Sets all file path objects to be selected that correspond to the model control objects provided.
    ''' </summary>
    ''' <param name="p_mcModels">Model control objects to use as a basis for selecting file path objects.</param>
    ''' <remarks></remarks>
    Protected Sub SelectPathsAllMatchingFiles(ByVal p_mcModels As cMCModels)
        For Each pathFiltered As cPathExample In filePaths.pathsFiltered
            pathFiltered.fileNameUse = False
            For Each mcModel As cMCModel In p_mcModels
                If StringsMatch(pathFiltered.path, mcModel.modelFile.pathSource.path) Then
                    pathFiltered.fileNameUse = True
                    Exit For
                End If
            Next
        Next
        filePaths.SetPathsSelected()
    End Sub

    ''' <summary>
    ''' Sets up final property assignments and reference links between the file paths and the corresponding model control objects.
    ''' </summary>
    ''' <param name="p_mcModels">Model control objects to use for completing the properties setup.</param>
    ''' <remarks></remarks>
    Protected Sub FinalizeModelControlAndPathReferenceProperties(ByRef p_mcModels As cMCModels)
        mcModels = New cMCModels
        For Each mcModel As cMCModel In p_mcModels
            mcModels.Add(CType(mcModel.Clone, cMCModel))
        Next

        filePaths.SetMCReferenceInFilePaths(p_mcModels)
    End Sub


    ''' <summary>
    ''' Generates an appropriate starting path for browsing.
    ''' </summary>
    ''' <param name="p_path">Current path to the file or directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function PathStart(ByVal p_path As String) As String
        If String.IsNullOrWhiteSpace(p_path) Then
            If String.IsNullOrWhiteSpace(folderSource) Then
                Return pathStartup()
            Else
                Return folderSource
            End If
        Else
            Return p_path
        End If
    End Function
#End Region

#Region "Methods: Prompts"
    Private Sub ShowAlertNoModelControlFilesFound(ByVal p_pathDir As String)
        OnMessenger(New MessengerEventArgs(_PROMPT_NO_MC_FILES_FOUND & Environment.NewLine & Environment.NewLine &
                                           p_pathDir))
    End Sub
    Private Sub ShowAlertIncorrectModelControlFiles(ByVal p_invalidPaths As String)
        Dim incorrectModels As New frmLongListPrompt(eMessageActionSets.None,
                                                     _TITLE_INVALID_MODEL_CONTROL_FILE,
                                                     _PROMPT_INVALID_MODEL_CONTROL_FILE,
                                                      p_invalidPaths, ,
                                                      MessageBoxImage.Warning)
        incorrectModels.Show()
    End Sub
    Private Sub ShowAlertIncorrectModelIDs(ByVal p_invalidPaths As String)
        Dim incorrectModels As New frmLongListPrompt(eMessageActionSets.None,
                                                _TITLE_ILLEGAL_MODEL_ID,
                                                _PROMPT_ILLEGAL_MODEL_ID,
                                                _FOOTER_ILLEGAL_MODEL_ID,
                                                p_invalidPaths,
                                                 MessageBoxImage.Warning)
        incorrectModels.Show()
    End Sub
#End Region

    Protected Overridable Sub OnMessenger(e As MessengerEventArgs)
        RaiseEvent Messenger(e)
    End Sub
    Protected Overridable Sub OnLogger(e As LoggerEventArgs)
        RaiseEvent Log(e)
    End Sub
End Class
