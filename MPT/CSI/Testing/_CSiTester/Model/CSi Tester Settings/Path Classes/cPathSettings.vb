Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports CSiTester.cRegTest

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Path object representing paths that are always located in a fixed location relative to the CsiTesterSettings.xml file or CSiTester.
''' </summary>
''' <remarks></remarks>
Public Class cPathSettings
    Inherits cPath

#Region "Constants"
    '=== Installation Directories & Files
    ''' <summary>
    ''' Name of the directory containing CSiTester.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const DIR_NAME_CSITESTER As String = "CSiTester"
    ''' <summary>
    ''' Name of the settings file for the CSiTester program.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FILENAME_CSITESTER_SETTINGS As String = "CSiTesterSettings.xml"

    ''' <summary>
    ''' Name of the directory of the installed version of CSiTester.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const DIR_NAME_VERIFICATION As String = "Verification"
#End Region

#Region "Variables"
    ''' <summary>
    ''' Default path to return if the conversion fails.
    ''' It is a good idea to supply this if the path is not known for certain.
    ''' </summary>
    ''' <remarks></remarks>
    Private _defaultPath As String
    ''' <summary>
    ''' True: The path is not certain to convert, so no failure should be announced as a default will be used.
    ''' </summary>
    ''' <remarks></remarks>
    Private _pathUnknown As Boolean = True
#End Region

#Region "Properties: Shared"
    '=== Default Paths
    ''' <summary>
    ''' Root directory letter, such as 'C'.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _rootDirectory As String = Left(pathStartup, 1)

    ''' <summary>
    ''' Default path to the default program to be run in CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property DIR_TESTER_PROGRAM_PATH_DEFAULT As String
        Get
            Return _rootDirectory & ":\CSiTester\ETABS.EXE"
        End Get
    End Property

    ''' <summary>
    ''' Default path to the models source directory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property DIR_TESTER_SOURCE_DIR_DEFAULT As String
        Get
            Return _rootDirectory & ":\CSiTester\Models Source"
        End Get
    End Property

    ''' <summary>
    ''' Default path to the destination directory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property DIR_TESTER_DESTINATION_DIR_DEFAULT As String
        Get
            Return _rootDirectory & ":\CSiTester Destination"
        End Get
    End Property

    ''' <summary>
    ''' Default path to the destination directory of an installed version of CSiTester.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property DIR_TESTER_DESTINATION_DIR_INSTALL_DEFAULT As String
        Get
            Return _rootDirectory & ":\Verification"
        End Get
    End Property
#End Region

#Region "Properties"
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
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property pathRelative() As String
        Get
            Return GetRelativePath()
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
    ''' <param name="p_defaultPath">Default path to return if the conversion fails.
    ''' It is a good idea to supply this if the path is not known for certain.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub SetProperties(ByVal p_path As String,
                                       Optional ByVal p_defaultPath As String = "")
        If String.IsNullOrEmpty(p_path) Then Exit Sub

        p_path = GetAbsolutePath(p_path, _defaultPath, _pathUnknown)
        SetProperties(p_path, p_pathUnknown:=True, p_defaultPath:=p_defaultPath)
    End Sub
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_defaultPath">Default path to return if the conversion fails.
    ''' It is a good idea to supply this if the path is not known for certain.</param>
    ''' <param name="p_pathUnknown">True: The path is not certain to convert, so no failure should be announced as a default will be used.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub SetProperties(ByVal p_path As String,
                                       ByVal p_pathUnknown As Boolean,
                                       Optional ByVal p_defaultPath As String = "")
        If String.IsNullOrEmpty(p_path) Then Exit Sub

        _defaultPath = p_defaultPath
        _pathUnknown = p_pathUnknown
        p_path = GetAbsolutePath(p_path, _defaultPath, _pathUnknown)
        MyBase.SetProperties(p_path, p_filenameHasExtension:=True)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="p_path">File path to return as an absolute path.</param>
    ''' <param name="p_defaultPath">Default path to return if the conversion fails.
    ''' It is a good idea to supply this if the path is not known for certain.</param>
    ''' <param name="p_pathUnknown">True: The path is not certain to convert, so no failure should be announced as a default will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function GetAbsolutePath(ByVal p_path As String,
                                                 Optional ByVal p_defaultPath As String = "",
                                                 Optional ByVal p_pathUnknown As Boolean = True) As String
        If AbsolutePath(p_path,
                        p_ignoreFailure:=p_pathUnknown) Then
            If (Not IO.Directory.Exists(p_path) AndAlso Not IO.File.Exists(p_path)) Then p_path = p_defaultPath
        Else
            p_path = p_defaultPath
        End If

        Return p_path
    End Function

    ''' <summary>
    ''' Return the file path as a relative path to another directory location.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function GetRelativePath() As String
        Dim newPath As String = path

        RelativePath(newPath, Not isDirectoryOnly)

        Return newPath
    End Function
#End Region
End Class
