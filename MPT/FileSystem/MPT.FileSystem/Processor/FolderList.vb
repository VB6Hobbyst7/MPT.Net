Option Strict On
Option Explicit On

Imports MPT.FileSystem.Validator

Namespace Processor
    ''' <summary>
    ''' Contains a list of folder paths whose folders have been validated according to IValidateFolder.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FolderList
        Inherits FolderProcessor

        Private ReadOnly _folderValidator As IValidateFolder
        ''' <summary>
        ''' Strategy Pattern object that contains the logic for determining whether or not to add a folder to the list of folder paths.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FolderValidator As IValidateFolder
            Get
                Return _folderValidator
            End Get
        End Property

        Private ReadOnly _paths As New List(Of String)
        ''' <summary>
        ''' List of folder paths that have been processed for selection.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Paths As List(Of String)
            Get
                Return _paths
            End Get
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="folderValidator">Strategy Pattern object that contains the logic for determining whether or not to add a folder to the list of folder paths.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal folderValidator As IValidateFolder)
            _folderValidator = folderValidator
        End Sub

        ''' <summary>
        ''' Processes the folder according to IValidateFolder.
        ''' </summary>
        ''' <param name="path">Path to the folder to process.</param>
        ''' <remarks></remarks>
        Public Overrides Sub ProcessFolder(path As String)
            If (_folderValidator.IsValidFolder(path) AndAlso
                Not _paths.Contains(path)) Then
                _paths.Add(path)
                MyBase.ProcessFolder(path)
            End If 
        End Sub
    End Class
End Namespace