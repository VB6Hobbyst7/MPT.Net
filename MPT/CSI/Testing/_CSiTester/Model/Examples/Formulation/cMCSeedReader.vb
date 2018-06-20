Option Explicit On
Option Strict On

Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting

Imports CSiTester.cMCModelID
Imports CSiTester.cMCModel

Friend Class cMCSeedReader
    Inherits cMCGenerator

#Region "Prompts"
    Private Const _TITLE_BROWSE As String = "Select Destination for File"

    Private Const _TITLE_BROWSE_INVALID As String = "Directory Does Not Exist"
    Private Const _PROMPT_BROWSE_INVALID As String = "Destination directory does not exist and will be reset." & vbNewLine & vbNewLine &
                                                     "Please choose a valid path if you want to overwrite the default destination." & vbNewLine & vbNewLine &
                                                     "The default destination is in the directory in which the model file resides."


#End Region

#Region "Properties"
    ''' <summary>
    ''' Setting indicating how to include multi-model cases when generating examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property multiModelMethod As eMultiModelIDNumbering

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
                RaisePropertyChanged("folderStructure")
            End If
        End Set
        Get
            Return _folderStructure
        End Get
    End Property

    Private _folderDestination As String
    ''' <summary>
    ''' Path to the folder that will contain the examples generated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property folderDestination As String
        Set(ByVal value As String)
            If (Not _folderDestination = value AndAlso
                ValidateExampleDestination(value)) Then
                 _folderDestination = value
                RaisePropertyChanged("folderDestination")
            End If
        End Set
        Get
            Return _folderDestination
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        _canBeSeed = True
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Sets the destination for the model files being generated.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function BrowseModelDestination() As Boolean
        Try
            Dim dirPathStart As String = PathStartDestination(folderDestination, folderSource)
            Dim tempPath As String = ""

            tempPath = BrowseForFolder(_TITLE_BROWSE,
                                        dirPathStart,
                                        p_showNewFolderButton:=True)
            ValidateExampleDestination(tempPath)
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Populates the remaining unfinished properties of the class based on the current object state and the provided parameters.
    ''' </summary>
    ''' <param name="p_mcModelBase">Model control object to use as a template for populating values.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function Fill(ByVal p_mcModelBase As cMCModel) As Boolean
        Try
            filePaths.SetPathsSelected()

            mcModels = GenerateModelsFromSeed(p_mcModelBase)
            filePaths.SetMCReferenceInFilePaths(mcModels)
            Return True
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_reservedIDs"></param>
    ''' <remarks></remarks>
    Friend Sub SetReservedIDs(ByVal p_reservedIDs As String)
        IDSchema.SetReservedIDs(p_reservedIDs)
    End Sub

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Generates a list containing model control objects for each path selected.
    ''' </summary>
    ''' <param name="p_mcModelBase">Template model control file to use for filling.
    ''' This object may already have some commonly shared properties set.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateModelsFromSeed(ByVal p_mcModelBase As cMCModel) As cMCModels
        Dim newModels As New cMCModels
        Try
            With newModels
                .SetSkippedModelIDsList(IDSchema.skippedModelIDsList)
                .startingExampleID = IDSchema.startingModelID
                .multiModelMethod = multiModelMethod
            End With

            For Each pathSelected As cPathExample In filePaths.pathsSelected
                Dim mcModelAdd As cMCModel = CType(p_mcModelBase.Clone, cMCModel)
                mcModelAdd.ReInitializeModelFile(pathSelected.path)

                ' Update folder structure and path to what the user has set:
                mcModelAdd.folderStructure = folderStructure
                If Not String.IsNullOrWhiteSpace(folderDestination) Then mcModelAdd.ChangeDestination(folderDestination)

                newModels.Add(mcModelAdd, p_incrementID:=True)

                ' Set up a reference between the model control file and the selected path object
                pathSelected.SetMCModel(mcModelAdd)
            Next
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return newModels
    End Function

    ''' <summary>
    ''' Validates that the destination directory exists.
    ''' The user is prompted if it does not, and the incorrect path is reset.
    ''' </summary>
    ''' <param name="p_path">Path to validate.</param>
    ''' <remarks></remarks>
    Private Function ValidateExampleDestination(ByVal p_path As String) As Boolean
        If (Not String.IsNullOrWhiteSpace(p_path) AndAlso
            Not IO.Directory.Exists(p_path)) Then
            OnMessenger(New MessengerEventArgs(_PROMPT_BROWSE_INVALID, _TITLE_BROWSE_INVALID))
            Return False
        Else
            Return True
        End If
    End Function



    ''' <summary>
    ''' Generates an appropriate starting path for browsing for a file destination.
    ''' </summary>
    ''' <param name="p_pathDestination">Current path to the destination file or directory.</param>
    ''' <param name="p_pathSource">Current path to the source file or directory.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathStartDestination(ByVal p_pathDestination As String,
                                          ByVal p_pathSource As String) As String
        If Not String.IsNullOrEmpty(p_pathDestination) Then
            Return p_pathDestination
        ElseIf Not String.IsNullOrEmpty(p_pathSource) Then
            Return p_pathSource
        Else
            Return pathStartup()
        End If
    End Function
#End Region
End Class
