Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary

Namespace Validator
    ''' <summary>
    ''' Determines whether or not a file is valid based on matching file name and/or extension.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NameExtensionFileValidator
        Inherits FileValidator

        Private ReadOnly _name As String
        ''' <summary>
        ''' File name, with no extension.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Private ReadOnly _extension As String
        ''' <summary>
        ''' File extension.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Extension As String
            Get
                Return _extension
            End Get
        End Property

        
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="name">File name, without extension.</param>
        ''' <param name="extension">File extension.</param>
        ''' <param name="excludeFile">True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal name As String,
                       ByVal extension As String,
                       Optional ByVal excludeFile As Boolean = False)
            MyBase.New(excludeFile)
            _name = name
            _extension = GetSuffix(extension, ".")
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="nameWithExtension">File name, with extension.</param>
        ''' <param name="excludeFile">True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nameWithExtension As String,
                       Optional ByVal excludeFile As Boolean = False)
            MyBase.New(excludeFile)
            _name = FileNameWithoutExtension(nameWithExtension)
            _extension = GetSuffix(nameWithExtension, ".")
        End Sub


        ''' <summary>
        ''' True: File is a valid file based on the selection criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to validate.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function IsValidFile(path As String) As Boolean
            Dim isValid As Boolean = FileNameExtensionsMatch(path, _name, _extension)
            Return Result(isValid)
        End Function
    End Class
End Namespace

