Option Explicit On
Option Strict On

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Friend Class cMCReader
    Inherits cMCGenerator

#Region "Prompts"
    Private Const _TITLE_BROWSE As String = "Model Control Files"
    Private Const _INVALID_MC_FILE As String = "The path provided does not point to a valid model control file: "
#End Region

#Region "Fields"
    ''' <summary>
    ''' Temporary model control object used for populating a collection of a single model control object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _mcModel As New cMCModel
#End Region

#Region "Properties"
    ''' <summary>
    ''' Path to the model control file to read.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property pathMCFileSource As String
#End Region

#Region "Initialization"
    Friend Sub New()
        _canBeSeed = False
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Populates the properties of the class based on the path specified.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function BrowseModelControlFile() As Boolean
        Try
            If BrowseForFile(pathMCFileSource,
                             PathStart(pathMCFileSource),
                             _TITLE_BROWSE,
                             New List(Of String)(New String() {"xml"})) Then

                _mcModel = New cMCModel(pathMCFileSource, p_alertInvalidPath:=True)
                If Not _mcModel.isFromSeedFile Then
                    mcModels.Clear()
                    mcModels.Add(_mcModel)
                    Fill(_mcModel)
                    Return True
                Else
                    OnMessenger(New MessengerEventArgs(_INVALID_MC_FILE & Environment.NewLine & Environment.NewLine &
                                                       _mcModel.mcFile.pathSource.path))
                End If
            End If
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Generates a single file path object that is linked to the associated model control object and updates the class models property.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Fill(ByRef p_mcModel As cMCModel)
        Try
            Dim mcModelsTemp As New cMCModels
            mcModelsTemp.Add(p_mcModel)

            CreatePathsFiltered(p_mcModel.mcFile.pathDestination.directory, p_mcModel)

            SelectPathsAllMatchingFiles(mcModelsTemp)

            FinalizeModelControlAndPathReferenceProperties(mcModelsTemp)
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"

#End Region

End Class
