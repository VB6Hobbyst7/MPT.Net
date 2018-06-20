Option Strict On
Option Explicit On

Imports MPT.String.StringLibrary

Namespace Validator
    ''' <summary>
    ''' Determines whether or not a folder is valid based on matching folder name.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NameFolderValidator
        Inherits FolderValidator

        Private ReadOnly _name As String
        ''' <summary>
        ''' Folder name.
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
        ''' <param name="name">Folder name.</param>
        ''' <param name="excludeFolder">True: Matching criteria will exclude the folder.
        ''' False: Matching criteria will include the folder.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal name As String,
                       Optional ByVal excludeFolder As Boolean = False)
            MyBase.New(excludeFolder)
            _name = GetSuffix(name, "\")
        End Sub

        ''' <summary>
        ''' True: File is a valid file based on the selection criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to validate.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function IsValidFolder(path As String) As Boolean
            Dim folderName As String = GetSuffix(path, "\")
            Dim isValid As Boolean = StringsMatch(folderName, _name)
            Return Result(isValid)
        End Function
    End Class
End Namespace