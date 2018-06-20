Option Explicit On
Option Strict On

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger

'CURRENTLY NOT USED

Public Class cMCFileOrganizer
    Inherits PropertyChanger

#Region "Variables"
    ''' <summary>
    ''' New XML path if the XML file is being moved.
    ''' </summary>
    ''' <remarks></remarks>
    Private _newXMLPath As String
#End Region

#Region "Properties"
    ''' <summary>
    ''' Class that contains paths to all selected model files to be edited. 
    ''' From older class, also contains all files and directories within a specified directory, as well as a list of paths filtered by file extension.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property oFilePaths As New cPathsExamples

    Private _folderStructure As eFolderStructure
    ''' <summary>
    ''' The folder structure to which the generated examples will be be organized.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property folderStructure As eFolderStructure
        Set(ByVal value As eFolderStructure)
            If Not _folderStructure = value Then
                _folderStructure = value
            End If
        End Set
        Get
            Return _folderStructure
        End Get
    End Property
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Organizes files in the specified folder structure. 
    ''' Updates any file internal references as necessary, and updates the list of XMLs paths if the files are moved from the current model location.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the CSi product model file.</param>
    ''' <param name="p_modelFilePath">Original path of the model file.</param>
    ''' <param name="p_xmlPathDestination">Path to the destination of the model control XML.</param>
    ''' <param name="p_folderStructure">Flattened vs. Database structure specification.</param>
    ''' <remarks></remarks>
    Private Function OrganizeExampleFiles(ByVal p_modelFileName As String,
                                         ByVal p_modelFilePath As String,
                                         ByVal p_xmlPathDestination As String,
                                         ByVal p_folderStructure As eFolderStructure) As Boolean
        If (p_folderStructure = eFolderStructure.NotSpecified OrElse
            p_folderStructure = eFolderStructure.enumError) Then p_folderStructure = folderStructure

        Select Case p_folderStructure
            Case eFolderStructure.Database
                Return OrganizeExampleFilesDBStructure(p_modelFileName, p_modelFilePath, p_xmlPathDestination)
            Case eFolderStructure.Flattened
                Return OrganizeExampleFilesFlattenedStructure(p_xmlPathDestination)
            Case Else
                _newXMLPath = ""
                Return False
        End Select

    End Function
    ''' <summary>
    ''' Organizes files in the database folder structure. 
    ''' Updates any file internal references as necessary, and updates the list of XMLs paths if the files are moved from the current model location.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the CSi product model file.</param>
    ''' <param name="p_modelFilePath">Original path of the model file.</param>
    ''' <param name="p_xmlPathDestination">Path to the destination of the model control XML.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OrganizeExampleFilesDBStructure(ByVal p_modelFileName As String,
                                                     ByVal p_modelFilePath As String,
                                                     ByVal p_xmlPathDestination As String) As Boolean
        Dim newModelPath As String = AdjustSelectedModelFilePaths(p_modelFileName, p_modelFilePath)

        Return CreateAndPopulateDBDirectories(p_modelFileName, newModelPath, p_xmlPathDestination)
    End Function

    ''' <summary>
    ''' Determines the new model path and updates the file paths stored in the list of selected file paths.
    ''' Returns the new model path.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the CSi product model file.</param>
    ''' <param name="p_modelFilePath">Original path of the model file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AdjustSelectedModelFilePaths(ByVal p_modelFileName As String,
                                                  ByVal p_modelFilePath As String) As String
        Dim newModelPath As String = ""

        For Each oPath As cPathExample In oFilePaths.pathsSelected
            With oPath
                If StringsMatch(.path, p_modelFilePath) Then
                    If Not String.IsNullOrEmpty(.path) Then
                        newModelPath = GetPathDirectoryStub(.path) & "\" & p_modelFileName & "\" & DIR_NAME_MODELS_DEFAULT & "\" & GetSuffix(.path, "\")
                        .SetProperties(newModelPath)
                    End If
                    If Not String.IsNullOrEmpty(.pathChildStub) Then .SetPathChildStub(GetPathDirectoryStub(.path))

                    Exit For
                End If
            End With
        Next

        Return newModelPath
    End Function
    ''' <summary>
    ''' Creates the database folder structure and populates the directories with files. If unsuccessful, any partial action is undone.
    ''' </summary>
    ''' <param name="p_modelFileName">Name of the CSi product model file.</param>
    ''' <param name="p_modelFilePathNew">New path of the model file.</param>
    ''' <param name="p_xmlPathDestination">Path to the destination of the model control XML.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateAndPopulateDBDirectories(ByVal p_modelFileName As String,
                                                    ByVal p_modelFilePathNew As String,
                                                    ByVal p_xmlPathDestination As String) As Boolean
        If myXMLEditor.GatherDBDirectoryFromFlattened(p_xmlPathDestination, , p_modelFileName) Then
            _newXMLPath = GetPathDirectoryStub(p_xmlPathDestination) & "\" & p_modelFileName & "\" & GetSuffix(p_xmlPathDestination, "\")
            Return True
        Else                                'If there are unresolved folder name conflicts, remove the example
            If Not String.IsNullOrEmpty(p_modelFilePathNew) Then oFilePaths.RemovePathSelectedByPath(p_modelFilePathNew)
            DeleteFile(p_xmlPathDestination)
            _newXMLPath = ""
            Return False
        End If
    End Function
    ''' <summary>
    ''' Organizes files in the flattened folder structure. 
    ''' Updates any file internal references as necessary, and updates the list of XMLs paths if the files are moved from the current model location.
    ''' </summary>
    ''' <param name="p_xmlPathDestination">Path to the destination of the model control XML.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OrganizeExampleFilesFlattenedStructure(ByVal p_xmlPathDestination As String) As Boolean
        _newXMLPath = p_xmlPathDestination
        Return True
    End Function
#End Region
End Class
