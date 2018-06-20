Option Strict On
Option Explicit On

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary

Namespace Validator
    ''' <summary>
    ''' Determines whether or not a file is valid based on partially matching file name.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PartialNameMatchValidator
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
        
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="name">File name, without extension.</param>
        ''' <param name="excludeFile">True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal name As String,
                       Optional ByVal excludeFile As Boolean = False)
            MyBase.New(excludeFile)
             _name = FileNameWithoutExtension(name)
        End Sub

        ''' <summary>
        ''' True: File is a valid file based on the selection criteria.
        ''' </summary>
        ''' <param name="partialMatch">Name of or path to the file to validate as matching part of the base file name.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function IsValidFile(partialMatch As String) As Boolean
            Dim isValid As Boolean = StringExistInName(_name, FileNameWithoutExtension(partialMatch))
            Return Result(isValid)
        End Function
    End Class
End Namespace

