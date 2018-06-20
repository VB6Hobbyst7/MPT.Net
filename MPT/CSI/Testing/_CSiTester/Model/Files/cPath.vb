Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reflections.ReflectionLibrary

''' <summary>
''' Class that takes a filepath and also stores the filename &amp; extension, and filepath directory.
''' </summary>
''' <remarks></remarks>
Public Class cPath
    Inherits PropertyChanger
    Implements ICloneable

#Region "Enumerations"
    Public Enum ePathType
        Any = 0
        Directory
        FileAny
        FileWithExtension
        FileWithoutExtension
    End Enum
#End Region

#Region "Properties"
    Protected _pathType As ePathType = ePathType.Any
    Protected _path As String
    ''' <summary>
    ''' File path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Overloads ReadOnly Property path() As String
        Get
            Return ConstructPath()
        End Get
    End Property

    ''' <summary>
    ''' File path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is to be relative.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Overloads ReadOnly Property path(ByVal p_pathRelativeReference As String) As String
        Get
            Return GetRelativePath(p_pathRelativeReference)
        End Get
    End Property

    Protected WithEvents _directory As String
    ''' <summary>
    ''' Directory of a file contained in a path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property directory As String
        Get
            Return _directory
        End Get
    End Property

    Protected WithEvents _fileName As String
    ''' <summary>
    ''' File name included in the associated filepath.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property fileName As String
        Get
            Return _fileName
        End Get
    End Property

    Protected WithEvents _fileExtension As String
    ''' <summary>
    ''' File extension of the associated filepath &amp; name. Does not include ".".
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property fileExtension As String
        Get
            Return _fileExtension
        End Get
    End Property

    Protected _fileNameWithExtension As String
    ''' <summary>
    ''' File name included in the associated filepath.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property fileNameWithExtension As String
        Get
            Return _fileNameWithExtension
        End Get
    End Property


    Protected _isFileNameOnly As Boolean
    ''' <summary>
    ''' If true, the filepath is only a file name with no directories listed. If false, the filepath includes directories.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isFileNameOnly As Boolean
        Get
            Return isFileNameOnly
        End Get
    End Property

    Protected _isDirectoryOnly As Boolean
    ''' <summary>
    ''' If true, the filepath is to a directory. If false, the filepath is to a file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isDirectoryOnly As Boolean
        Get
            Return _isDirectoryOnly
        End Get
    End Property

    Protected _isValidPath As Boolean = False
    ''' <summary>
    ''' True if the path points to an existing file or directory (as applicable).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isValidPath As Boolean
        Get
            Return _isValidPath
        End Get
    End Property


    Protected _pathChildStub As String
    ''' <summary>
    ''' Relative path to the file relative to a specified parent directory.
    ''' For example {stripped path: parent directory}\[pathChildStub]\{stripped: fileName}.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property pathChildStub As String
        Get
            Return _pathChildStub
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
    ''' <param name="p_setPathType">Limits the path validity criteria based on an expected path type.</param>
    Friend Sub New(ByVal p_path As String,
                   Optional ByVal p_setPathType As ePathType = ePathType.Any)
        _pathType = p_setPathType
        SetProperties(p_path)
    End Sub

    Friend Overridable Function Clone() As Object Implements ICloneable.Clone
        Return CloneStatic()
    End Function
    Protected Overridable Function Create() As cPath
        Return New cPath()
    End Function

    ''' <summary>
    ''' Only clones the read-only properties associated with the base path class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CloneStatic() As Object
        Dim myClone As cPath = Create()

        With myClone
            ._path = _path
            ._directory = _directory
            ._fileName = _fileName
            ._fileExtension = _fileExtension
            ._fileNameWithExtension = _fileNameWithExtension
            ._isDirectoryOnly = _isDirectoryOnly
            ._isFileNameOnly = _isFileNameOnly
            ._isValidPath = _isValidPath
            ._pathChildStub = _pathChildStub
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPath) Then Return False

        Dim comparedObject As cPath = TryCast(p_object, cPath)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not ._path = _path Then Return False
            If Not ._isDirectoryOnly = _isDirectoryOnly Then Return False
            If Not ._isFileNameOnly = _isFileNameOnly Then Return False
            If Not ._isValidPath = _isValidPath Then Return False
            If Not ._pathChildStub = _pathChildStub Then Return False
        End With

        Return True
    End Function

    'Public Function BaseEquals
#End Region

#Region "Methods: Overrides/Overloads/Implements"
    Public Overrides Function ToString() As String
        Return MyBase.ToString() & " - " & path
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <remarks></remarks>
    Friend Overridable Sub SetProperties(ByVal p_path As String,
                                         Optional ByVal p_pathRelativeReference As String = "")
        SetProperties(p_path, p_filenameHasExtension:=True, p_pathRelativeReference:=p_pathRelativeReference)
    End Sub

    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <param name="p_filenameHasExtension">False: Path contains a filename that has no extension.</param>
    ''' <remarks></remarks>
    Friend Overridable Sub SetProperties(ByVal p_path As String,
                                         ByVal p_filenameHasExtension As Boolean,
                                         Optional ByVal p_pathRelativeReference As String = "")
        If (String.IsNullOrEmpty(p_path) OrElse
            Not PathIsOfExpectedType(p_path) OrElse
            StringsMatch(p_path, path)) Then Exit Sub

        SetPath(GetAbsolutePath(p_path, p_pathRelativeReference))

        SetFileName(_path, p_filenameHasExtension)
        SetFileExtension(_fileName)
        SetFileNameWithExtension()
        SetIsFileNameOnly(_path, _fileNameWithExtension)

        SetIsDirectoryOnly(_fileName, _isFileNameOnly)
        SetDirectoryPath(_path, _isDirectoryOnly, _isFileNameOnly)

        SetIsValidPath(_path, _isDirectoryOnly, _isFileNameOnly)
    End Sub


    ''' <summary>
    ''' Sets a path stub of a filepath (without the file) or a directory path contained within the specified parent directory path. 
    ''' For example {stripped path: p_sourceFolder}\[pathChildStub]\{stripped: fileName}.
    ''' </summary>
    ''' <param name="p_sourceFolder">Path to the parent directory.</param>
    ''' <remarks></remarks>
    Friend Sub SetPathChildStub(ByVal p_sourceFolder As String)
        If String.IsNullOrEmpty(p_sourceFolder) Then Exit Sub

        Dim value As String

        'Get portion of stub after the specified folder
        value = FilterStringFromName(directory, p_sourceFolder, p_retainPrefix:=False, p_retainSuffix:=True)
        If (Not value = p_sourceFolder AndAlso
            Not String.IsNullOrEmpty(value)) Then

            value = FilterStringFromName(directory, p_sourceFolder & "\", p_retainPrefix:=False, p_retainSuffix:=True)
        End If

        If Not _pathChildStub = value Then
            _pathChildStub = value
            RaisePropertyChanged(Function() Me.pathChildStub)
        End If
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path is of an expected path type based on the presence of a file extension.
    ''' </summary>
    ''' <param name="p_path">File path to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathIsOfExpectedType(ByVal p_path As String) As Boolean
        If String.IsNullOrEmpty(p_path) Then Return False
        Dim fileExtension As String = GetSuffix(p_path, ".")
        Dim fileHasExtension As Boolean = (fileExtension.CompareTo(p_path) <> 0)

        Select Case _pathType
            Case ePathType.Any
                Return True
            Case ePathType.Directory
                Return Not fileHasExtension
            Case ePathType.FileAny
                Return True
            Case ePathType.FileWithExtension
                Return fileHasExtension
            Case ePathType.FileWithoutExtension
                Return Not fileHasExtension
            Case Else
                Return False
        End Select
    End Function

    'Set/Derive ReadOnly Properties
    Private Sub SetPath(ByVal p_path As String)
        If Not _path = p_path Then
            RaisePropertyChanging(Function() Me.path)
            _path = p_path
            RaisePropertyChanged(Function() Me.path)
        End If
    End Sub
    Private Sub SetFileName(ByVal p_path As String,
                            Optional p_filenameHasExtension As Boolean = True)
        Dim value As String = GetPathFileName(_path, p_noExtension:=False)
        If p_filenameHasExtension Then
            If StringExistInName(value, ".") Then
                value = GetPathFileName(value, p_noExtension:=True)
            Else
                Exit Sub
            End If
        End If

        If Not _fileName = value Then
            RaisePropertyChanging(Function() Me.fileName)
            _fileName = value
            RaisePropertyChanged(Function() Me.fileName)
        End If
    End Sub
    Private Sub SetFileExtension(ByVal p_fileName As String)
        Dim value As String = GetSuffix(_path, ".")

        If Not _fileExtension = value Then
            RaisePropertyChanging(Function() Me.fileExtension)
            _fileExtension = value
            If (StringsMatch(_fileExtension, p_fileName) OrElse
                StringsMatch(_fileExtension, _path)) Then _fileExtension = "" 'Filename has no extension
            RaisePropertyChanged(Function() Me.fileExtension)
        End If
    End Sub
    Private Sub SetFileNameWithExtension()
        Dim value As String = ConstructFileNameWithExtension()

        If Not _fileNameWithExtension = value Then
            _fileNameWithExtension = value
            RaisePropertyChanged(Function() Me.fileNameWithExtension)
        End If
    End Sub
    Private Sub SetIsFileNameOnly(ByVal p_path As String,
                                  ByVal p_fileNameWithExtension As String)
        Dim trialFileName As String = p_fileNameWithExtension
        Dim value As Boolean
        If StringsMatch(trialFileName, p_path) Then
            value = True
        Else
            value = False
        End If

        If Not _isFileNameOnly = value Then
            _isFileNameOnly = value
            RaisePropertyChanged(Function() Me.isFileNameOnly)
        End If
    End Sub

    Private Sub SetIsDirectoryOnly(ByVal p_fileName As String,
                                   ByVal p_isFileNameOnly As Boolean)
        Dim value As Boolean
        If (p_isFileNameOnly OrElse
            Not String.IsNullOrEmpty(p_fileName)) Then
            value = False
        Else
            value = True
        End If

        If Not _isDirectoryOnly = value Then
            _isDirectoryOnly = value
            RaisePropertyChanged(Function() Me.isDirectoryOnly)
        End If
    End Sub
    Private Sub SetDirectoryPath(ByVal p_path As String,
                                 ByVal p_isDirectoryOnly As Boolean,
                                 ByVal p_isFileNameOnly As Boolean)
        Dim value As String
        If p_isDirectoryOnly Then
            value = p_path
        ElseIf p_isFileNameOnly Then
            value = ""
        Else
            value = GetPathDirectoryStub(p_path)
        End If
        value = TrimPathSlash(value)

        If Not _directory = value Then
            RaisePropertyChanging(Function() Me.directory)
            _directory = value
            RaisePropertyChanged(Function() Me.directory)
        End If
    End Sub
    Private Sub SetIsValidPath(ByVal p_path As String,
                                ByVal p_isDirectory As Boolean,
                                ByVal p_isFileNameOnly As Boolean)
        Dim value As Boolean
        If ((_pathType = ePathType.Any OrElse
             _pathType = ePathType.Directory) AndAlso
            p_isDirectory AndAlso
             IO.Directory.Exists(p_path)) Then
            value = True
        ElseIf ((_pathType = ePathType.Any OrElse
                 _pathType = ePathType.FileAny OrElse
                 _pathType = ePathType.FileWithExtension OrElse
                 _pathType = ePathType.FileWithoutExtension) AndAlso
                Not p_isDirectory AndAlso
                Not p_isFileNameOnly AndAlso
                IO.File.Exists(p_path)) Then
            value = True
        Else
            value = False
        End If

        If Not _isValidPath = value Then
            _isValidPath = value
            RaisePropertyChanged(Function() Me.isValidPath)
        End If
    End Sub

    ''' <summary>
    ''' Creates a new path string by combining the present components of directory, file and file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstructPath() As String
        Dim newPath As String = ""
        If Not String.IsNullOrEmpty(_directory) Then newPath &= _directory & "\"
        If Not String.IsNullOrEmpty(_fileName) Then newPath &= ConstructFileNameWithExtension()

        Return newPath
    End Function

    ''' <summary>
    ''' Creates a new filename with extension string by combining the present components of file and file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstructFileNameWithExtension() As String
        Dim newFileNameWithExtension As String = _fileName
        If Not String.IsNullOrEmpty(_fileExtension) Then
            newFileNameWithExtension &= "." & fileExtension
        End If

        Return newFileNameWithExtension
    End Function

    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="p_path">File path to return as an absolute path.</param>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function GetAbsolutePath(ByVal p_path As String,
                                       Optional ByVal p_pathRelativeReference As String = "") As String

        AbsolutePath(p_path, p_referencePath:=p_pathRelativeReference)

        Return p_path
    End Function

    ''' <summary>
    ''' Return the file path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is to be relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function GetRelativePath(Optional ByVal p_pathRelativeReference As String = "") As String
        Dim newPath As String = path
        Dim pathIsToFile As Boolean = Not isDirectoryOnly

        RelativePath(newPath, p_isFile:=pathIsToFile, p_referencePath:=p_pathRelativeReference)

        Return newPath
    End Function
#End Region


End Class
