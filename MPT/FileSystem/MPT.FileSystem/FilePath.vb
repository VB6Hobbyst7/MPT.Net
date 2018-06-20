Option Explicit On
Option Strict On

Imports MPT.FileSystem.PathLibrary
Imports MPT.String.StringLibrary

''' <summary>
''' Class that takes a filepath and also stores the filename &amp; extension, and filepath directory.
''' </summary>
''' <remarks></remarks>
Public Class FilePath
    Implements ICloneable

#Region "Properties"
    Protected _pathType As ePathType = ePathType.Any
    Protected _path As String = String.Empty
    ''' <summary>
    ''' File path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Overloads Function Path() As String
        Return ConstructPath()
    End Function

    ''' <summary>
    ''' File path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="pathRelativeReference">The directory/file location to which the path is to be relative.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Overloads Function Path(ByVal pathRelativeReference As String) As String
        Return GetRelativePath(pathRelativeReference)
    End Function

    Protected WithEvents _directory As String = String.Empty
    ''' <summary>
    ''' Directory of a file contained in a path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Directory As String
        Get
            Return _directory
        End Get
    End Property

    Protected WithEvents _fileName As String = String.Empty
    ''' <summary>
    ''' File name included in the associated filepath.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property FileName As String
        Get
            Return _fileName
        End Get
    End Property

    Protected WithEvents _fileExtension As String = String.Empty
    ''' <summary>
    ''' File extension of the associated filepath &amp; name. Does not include ".".
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileExtension As String
        Get
            Return _fileExtension
        End Get
    End Property

    Protected _fileNameWithExtension As String = String.Empty
    ''' <summary>
    ''' File name included in the associated filepath.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileNameWithExtension As String
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
    Public ReadOnly Property IsFileNameOnly As Boolean
        Get
            Return _isFileNameOnly
        End Get
    End Property

    Protected _isDirectoryOnly As Boolean
    ''' <summary>
    ''' If true, the filepath is to a directory. If false, the filepath is to a file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsDirectoryOnly As Boolean
        Get
            Return _isDirectoryOnly
        End Get
    End Property

    Protected _isValidPath As Boolean
    ''' <summary>
    ''' True if the path points to an existing file or directory (as applicable).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsValidPath As Boolean
        Get
            Return _isValidPath
        End Get
    End Property

    Protected _pathChildStub As String = String.Empty
    ''' <summary>
    ''' Relative path to the file relative to a specified parent directory.
    ''' For example {stripped path: parent directory}\[pathChildStub]\{stripped: fileName}.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PathChildStub As String
        Get
            Return _pathChildStub
        End Get
    End Property

    
    ''' <summary>
    ''' True if the path is considered to be seleted for some further use or operation.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsSelected As Boolean
#End Region

#Region "Initialization"
    Public Sub New()

    End Sub
    ''' <summary>
    ''' Sets the class properties using the provided path. 
    ''' </summary>
    ''' <param name="newPath">Path to be used.</param>
    ''' <param name="setPathType">Limits the path validity criteria based on an expected path type.</param>
    Public Sub New(ByVal newPath As String,
                   Optional ByVal setPathType As ePathType = ePathType.Any)
        _pathType = setPathType
        SetProperties(newPath)
    End Sub

    Public Overridable Function Clone() As Object Implements ICloneable.Clone
        Return CloneStatic()
    End Function

    ''' <summary>
    ''' Only clones the read-only properties associated with the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CloneStatic() As FilePath
        Dim myClone As FilePath = Create()

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
    

    Protected Overridable Function Create() As FilePath
        Return New FilePath()
    End Function



    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="object1">Object to check for equality.</param>
    ''' <param name="object2">Object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator =(ByVal object1 As FilePath, ByVal object2 As FilePath) As Boolean
        Return object1.Equals(object2)
    End Operator

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="object1">Object to check for equality.</param>
    ''' <param name="object2">Object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Operator <>(ByVal object1 As FilePath, ByVal object2 As FilePath) As Boolean
        Return Not object1.Equals(object2)
    End Operator

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="[object]">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal [object] As Object) As Boolean
        If Not (TypeOf [object] Is FilePath) Then Return False

        Dim comparedObject As FilePath = TryCast([object], FilePath)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        Return Equals(comparedObject)
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="filePath">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function Equals(ByVal filePath As FilePath) As Boolean
        With filePath
            If Not ._path = _path Then Return False
            If Not ._isDirectoryOnly = _isDirectoryOnly Then Return False
            If Not ._isFileNameOnly = _isFileNameOnly Then Return False
            If Not ._isValidPath = _isValidPath Then Return False
            If Not ._pathChildStub = _pathChildStub Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Overrides/Overloads/Implements"
    Public Overrides Function ToString() As String
        Return MyBase.ToString() & " - " & Path
    End Function
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="newPath">Path to be used.</param>
    ''' <param name="pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <remarks></remarks>
    Public Overridable Sub SetProperties(ByVal newPath As String,
                                         Optional ByVal pathRelativeReference As String = "")
        SetProperties(newPath, filenameHasExtension:=True, pathRelativeReference:=pathRelativeReference)
    End Sub

    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="newPath">Path to be used.</param>
    ''' <param name="pathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <param name="filenameHasExtension">False: Path contains a filename that has no extension.</param>
    ''' <remarks></remarks>
    Public Overridable Sub SetProperties(ByVal newPath As String,
                                         ByVal filenameHasExtension As Boolean,
                                         Optional ByVal pathRelativeReference As String = "")
        If (String.IsNullOrEmpty(newPath) OrElse
            Not PathIsOfExpectedType(newPath) OrElse
            StringsMatch(newPath, Path)) Then Return 
       
        SetPath(newPath, pathRelativeReference) 

        SetFileName(_path, filenameHasExtension) 
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
    ''' <param name="sourceDirectory">Path to the parent directory.</param>
    ''' <remarks></remarks>
    Public Sub SetPathChildStub(ByVal sourceDirectory As String) 
        If String.IsNullOrWhitespace(sourceDirectory) Then 
            _pathChildStub = String.Empty
            Return
        End If
        
        'Get portion of stub after the specified folder
        Dim value As String = FilterFromText(Directory, sourceDirectory, retainPrefix:=False, retainSuffix:=True)
        If (Not value = sourceDirectory AndAlso
            Not String.IsNullOrEmpty(value)) Then

            value = FilterFromText(Directory, sourceDirectory & "\", retainPrefix:=False, retainSuffix:=True)
        End If
         _pathChildStub = value
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path is of an expected path type based on the presence of a file extension.
    ''' </summary>
    ''' <param name="pathChecked">File path to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathIsOfExpectedType(ByVal pathChecked As String) As Boolean
        If String.IsNullOrEmpty(pathChecked) Then Return False
        Dim fileCheckedExtension As String = GetSuffix(pathChecked, ".")
        Dim fileHasExtension As Boolean = (String.Compare(fileCheckedExtension, pathChecked, StringComparison.Ordinal) <> 0)

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
    Private Sub SetPath(ByVal newpath As String,
                        ByVal pathRelativeReference As String)
        If (newpath.Contains("\")) Then
            GetAbsolutePath(newPath, pathRelativeReference)
        End If
        
        _path = newpath
    End Sub
    Private Sub SetFileName(ByVal newPath As String,
                            Optional filenameHasExtension As Boolean = True) 
        Dim value As String = PathLibrary.FileName(newPath, noExtension:=False)
        If filenameHasExtension Then
            If StringExistInName(value, ".") Then
                _fileName = PathLibrary.FileName(value, noExtension:=True)
                Return
            Else
                _fileName = String.Empty
                Return
            End If
        End If
        _fileName = value
    End Sub

    Private Sub SetFileExtension(ByVal newFileName As String)
        If (String.IsNullOrEmpty(_fileName))
            _fileExtension = ""
            Return
        End If

        Dim value As String = GetSuffix(_path, ".")

        If Not _fileExtension = value Then
            _fileExtension = value
            If (StringsMatch(_fileExtension, newFileName) OrElse
                StringsMatch(_fileExtension, _path)) Then _fileExtension = "" 'Filename has no extension
        End If
    End Sub
    Private Sub SetFileNameWithExtension()
        _fileNameWithExtension = ConstructFileNameWithExtension()
    End Sub
    Private Sub SetIsFileNameOnly(ByVal newPath As String,
                                  ByVal newFileNameWithExtension As String) 
        _isFileNameOnly =  StringsMatch(newFileNameWithExtension, newPath) 
    End Sub

    Private Sub SetIsDirectoryOnly(ByVal newFileName As String,
                                   ByVal isNewFileNameOnly As Boolean) 
        _isDirectoryOnly = Not (isNewFileNameOnly OrElse
                                    Not String.IsNullOrEmpty(newFileName)) 
    End Sub
    Private Sub SetDirectoryPath(ByVal newPath As String,
                                 ByVal newPathIsDirectoryOnly As Boolean,
                                 ByVal newPathIsFileNameOnly As Boolean) 
        Dim value As String
        If newPathIsDirectoryOnly Then
            value = newPath
        ElseIf newPathIsFileNameOnly Then
            value = ""
        Else
            value = PathDirectoryStub(newPath)
        End If
        value = TrimBackSlash(value)

        _directory = value
    End Sub
    Private Sub SetIsValidPath(ByVal newPath As String,
                                ByVal newPathIsDirectory As Boolean,
                                ByVal newPathIsFileNameOnly As Boolean) 
        If ((_pathType = ePathType.Any OrElse
             _pathType = ePathType.Directory) AndAlso
            newPathIsDirectory AndAlso
             IO.Directory.Exists(newPath)) Then
            _isValidPath = True
        ElseIf ((_pathType = ePathType.Any OrElse
                 _pathType = ePathType.FileAny OrElse
                 _pathType = ePathType.FileWithExtension OrElse
                 _pathType = ePathType.FileWithoutExtension) AndAlso
                Not newPathIsDirectory AndAlso
                Not newPathIsFileNameOnly AndAlso
                IO.File.Exists(newPath)) Then
            _isValidPath = True
        Else
            _isValidPath = False
        End If
    End Sub

    ''' <summary>
    ''' Creates a new path string by combining the present components of directory, file and file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstructPath() As String
        Dim newPath As String = ""
        If Not String.IsNullOrEmpty(_directory) Then newPath &= _directory
        If (Not String.IsNullOrEmpty(_directory) AndAlso Not String.IsNullOrEmpty(_fileName)) Then newPath &= "\"
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
            newFileNameWithExtension &= "." & FileExtension
        End If

        Return newFileNameWithExtension
    End Function

    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="newPath">File path to return as an absolute path.</param>
    ''' <param name="newPathRelativeReference">The directory/file location to which the path is relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function GetAbsolutePath(ByVal newPath As String,
                                                    Optional ByVal newPathRelativeReference As String = "") As String
        AbsolutePath(newPath, basePath:=newPathRelativeReference)

        Return newPath
    End Function

    ''' <summary>
    ''' Return the file path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="newPathRelativeReference">The directory/file location to which the path is to be relative.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overridable Function GetRelativePath(Optional ByVal newPathRelativeReference As String = "") As String
        Dim newPath As String = Path
        Dim pathIsToFile As Boolean = Not IsDirectoryOnly

        RelativePath(newPath, isFile:=pathIsToFile, basePath:=newPathRelativeReference)

        Return newPath
    End Function
#End Region

End Class
