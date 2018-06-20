Option Strict On
Option Explicit On

Imports MPT.FileSystem.FileLibrary

Namespace Validator
    ''' <summary>
    ''' Determines whether or not a file is valid based on ReadOnly status.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ReadOnlyFileValidator
        Inherits FileValidator

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="excludeFile">True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal excludeFile As Boolean = False)
            MyBase.New(excludeFile)
        End Sub

        ''' <summary>
        ''' True: File is a valid file based on the selection criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to validate.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function IsValidFile(path As String) As Boolean
            Dim isValid As Boolean = IsFileReadOnly(path)
            Return Result(isValid)
        End Function
    End Class

End Namespace

