Option Strict On
Option Explicit On

Imports MPT.FileSystem.Validator

Namespace Processor
    ''' <summary>
    ''' Contains a list of file paths whose files have been validated according to IValidateFile.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FileList
        Inherits FileProcessor

        Private ReadOnly _fileValidator As IValidateFile
        ''' <summary>
        ''' Strategy Pattern object that contains the logic for determining whether or not to add a file to the list of file paths.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileValidator As IValidateFile
            Get
                Return _fileValidator
            End Get
        End Property

        Private ReadOnly _paths As New List(Of String)
        ''' <summary>
        ''' List of file paths that have been processed for selection.
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
        ''' <param name="fileValidator">Strategy Pattern object that contains the logic for determining whether or not to add a file to the list of file paths.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal fileValidator As IValidateFile)
            _fileValidator = fileValidator
        End Sub

        ''' <summary>
        ''' Processes the file according to IValidateFile.
        ''' </summary>
        ''' <param name="path">Path to the file to process.</param>
        ''' <remarks></remarks>
        Public Overrides Sub ProcessFile(path As String)
            If (_fileValidator.IsValidFile(path) AndAlso
                Not _paths.Contains(path)) Then 
                _paths.Add(path)
                MyBase.ProcessFile(path)
            End If
        End Sub
    End Class
End Namespace


