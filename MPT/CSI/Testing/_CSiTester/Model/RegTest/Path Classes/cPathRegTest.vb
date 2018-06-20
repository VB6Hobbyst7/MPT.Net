Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports CSiTester.cRegTest

Imports CSiTester.cPathSettings

Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Path object representing paths that are always located in a fixed location relative to the regTest.xml file or CSiTester.
''' </summary>
''' <remarks></remarks>
Public Class cPathRegTest
    Inherits cPath

#Region "Properties"
    ''' <summary>
    ''' Path by which any relative path is based on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property pathRelativeToProgram As String
        Get
            Return "\" & DIR_NAME_CSITESTER & "\" & DIR_NAME_REGTEST & "\"
        End Get
    End Property

    ''' <summary>
    ''' Path by which any relative path is based on.
    ''' </summary>
    ''' <value></value>
    ''' <param name="p_regTest">RegTest object from which a relative path is to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property pathRelativeToRegTest(ByVal p_regTest As cRegTest) As String
        Get
            Return p_regTest.xmlFile.directory
        End Get
    End Property


    ''' <summary>
    ''' File path as an absolute path to another directory location.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Overrides ReadOnly Property path() As String
        Get
            Return _path
        End Get
    End Property

    ''' <summary>
    ''' File path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is to be relative.
    ''' If not provided, the default path relative to the program is used.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property pathRelative(Optional ByVal p_pathRelativeReference As String = "") As String
        Get
            If String.IsNullOrEmpty(p_pathRelativeReference) Then p_pathRelativeReference = pathRelativeToProgram
            Return GetRelativePath(p_pathRelativeReference)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Sets the class properties using the provided path. 
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    Friend Sub New(ByVal p_path As String)
        MyBase.New(p_path, ePathType.FileWithExtension)
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub SetProperties(ByVal p_path As String,
                                       Optional ByVal p_pathRelativeReference As String = "")
        If String.IsNullOrEmpty(p_path) Then Exit Sub

        p_path = GetAbsolutePath(p_path, p_pathRelativeReference)
        MyBase.SetProperties(p_path, p_filenameHasExtension:=True, p_pathRelativeReference:=p_pathRelativeReference)
    End Sub

    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <param name="p_filenameHasExtension">False: Path contains a filename that has no extension.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub SetProperties(ByVal p_path As String,
                                       ByVal p_filenameHasExtension As Boolean,
                                       Optional ByVal p_pathRelativeReference As String = "")
        If String.IsNullOrEmpty(p_path) Then Exit Sub

        p_path = GetAbsolutePath(p_path, p_pathRelativeReference)
        MyBase.SetProperties(p_path, p_filenameHasExtension:=p_filenameHasExtension, p_pathRelativeReference:=p_pathRelativeReference)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="p_path">File path to return as an absolute path.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetAbsolutePath(ByVal p_path As String,
                                                 Optional ByVal p_pathRelativeReference As String = "") As String
        If String.IsNullOrEmpty(p_pathRelativeReference) Then
            AbsolutePath(p_path,
                         p_relativeToProgram:=pathRelativeToProgram,
                         p_ignoreFailure:=True)
        Else
            AbsolutePath(p_path,
                         p_referencePath:=p_pathRelativeReference,
                         p_ignoreFailure:=True)
        End If

        Return p_path
    End Function

    ''' <summary>
    ''' Return the file path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is to be relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetRelativePath(Optional ByVal p_pathRelativeReference As String = "") As String
        Dim newPath As String = path
        Dim pathIsToFile As Boolean = Not isDirectoryOnly

        If String.IsNullOrEmpty(p_pathRelativeReference) Then
            RelativePath(newPath,
                         p_isFile:=pathIsToFile,
                         p_relativeToProgram:=pathRelativeToProgram)
        Else
            RelativePath(newPath,
                         p_isFile:=pathIsToFile,
                         p_referencePath:=p_pathRelativeReference)
        End If

        Return newPath
    End Function
#End Region
End Class
