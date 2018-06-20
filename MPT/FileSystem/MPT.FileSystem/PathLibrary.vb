Option Strict On
Option Explicit On

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Imports MPT.Reporting

Imports MPT.FileSystem.Processor
Imports MPT.FileSystem.Validator
Imports MPT.String.StringLibrary

''' <summary>
''' Contains functions for determining and manipulating file paths.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class PathLibrary
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "Relative-Absolute Paths"
 
    ''' <summary>
    ''' Checks if path is relative or absolute. 
    ''' If path is relative, path is converted to absolute. 
    ''' Returns true if a valid absolute path was created, and False indicates that the new value is possibly incorrect
    ''' </summary>
    ''' <param name="pathCheckConvert">Path to return as an absolute path.</param>
    ''' <param name="relativePathFromBase">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    ''' <param name="basePath">Absolute path to the base directory. 
    ''' Default path is to the of the running application.</param>
    ''' <remarks></remarks>
    Public Shared Function AbsolutePath(ByRef pathCheckConvert As String,
                                        Optional ByVal relativePathFromBase As String = "",
                                        Optional ByVal basePath As String = "") As Boolean
        If String.IsNullOrWhiteSpace(pathCheckConvert) Then pathCheckConvert = ""
         ' Base path must be empty or an absolute path for method to be valid
        Dim currentPathIsValid = (String.IsNullOrWhiteSpace(basePath) OrElse Not IsRelativePath(basePath))
        If (currentPathIsValid AndAlso 
            IsRelativePath(pathCheckConvert)) Then
            ' Assign default base path
            If String.IsNullOrWhiteSpace(basePath) Then 
                basePath = PathStartupApp()
                'basePath = PathStartupDll()
                If StringsMatch(basePath, pathCheckConvert) AndAlso String.IsNullOrWhiteSpace(relativePathFromBase) Then Return currentPathIsValid
            End If
            Dim convertedPath As String =  ConvertPathRelativeToAbsolute(basePath, pathCheckConvert, relativePathFromBase)
            pathCheckConvert = convertedPath
        End If
        Return currentPathIsValid
    End Function

    ''' <summary>
    ''' Checks if path is relative or absolute. 
    ''' If path is absolute, path is converted to relative. 
    ''' Returns true if a valid relative path was created, and False indicates that the new value is possibly incorrect.
    ''' </summary>
    ''' <param name="pathCheckConvert">Path to return as a relative path.</param>
    ''' <param name="isFile">True: Path is to a file. False: Path is to a directory.</param>
    ''' <param name="relativePathFromBase">If base reference is relative to the program, specify the relative path difference with this parameter.</param>
    ''' <param name="basePath">Absolute path to the base directory. 
    ''' Default path is to the of the running application.</param>
    ''' <remarks></remarks>
    Public Shared Function RelativePath(ByRef pathCheckConvert As String,
                                        Optional ByVal isFile As Boolean = False,
                                        Optional ByVal relativePathFromBase As String = "",
                                        Optional ByVal basePath As String = "") As Boolean
        ' Base path must be empty or an absolute path for method to be valid
        Dim currentPathIsValid = (String.IsNullOrWhiteSpace(basePath) OrElse Not IsRelativePath(basePath))
        If (currentPathIsValid AndAlso
            Not IsRelativePath(pathCheckConvert)) Then
            ' Assign default base path
            If String.IsNullOrWhiteSpace(basePath) Then 
                basePath = PathStartupApp()
                'basePath = PathStartupDll()
                If StringsMatch(basePath, pathCheckConvert) AndAlso String.IsNullOrWhiteSpace(relativePathFromBase) Then Return currentPathIsValid
            End If

            pathCheckConvert = ConvertPathAbsoluteToRelative(basePath, pathCheckConvert, isFile, relativePathFromBase)
        End If
        Return currentPathIsValid
    End Function
    
    ''' <summary>
    ''' Converts absolute path to relative path. 
    ''' Format is not to end relative path with a "\" for folder destinations. 
    ''' Returns basePath[\relativePathFromBase] if fails.
    ''' </summary>
    ''' <param name="basePath">Absolute path to the base directory, such as that of the running application or DLL.</param>
    ''' <param name="targetPath">Absolute path to target directory.</param>
    ''' <param name="isTargetPathToFile">True: Path is to a file. False: Path is assumed to be to a directory.</param>
    ''' <param name="relativePathFromBase">If base reference is to be relative to the base directory, specify the relative path difference with this parameter. 
    ''' The presence of starting and endings slashes does not matter. 
    ''' Currently only supports relative path to a child directory of the base path.</param>
    ''' <returns></returns>
    ''' <remarks>Format right now is to end relative path a "\" for folder destinations.</remarks>
    Public Shared Function ConvertPathAbsoluteToRelative(ByVal basePath As String,
                                                          ByVal targetPath As String,
                                                          Optional ByVal isTargetPathToFile As Boolean = False,
                                                          Optional ByVal relativePathFromBase As String = "") As String
        ' Clean
        targetPath = CleanPath(targetPath)
        basePath = CleanPath(basePath)
        relativePathFromBase = CleanPath(relativePathFromBase)

        ' Validate
        If Not IsValidPath(basePath) Then
            RaiseInvalidAbsolutePath(basePath)
            Return ""
        End If
        If Not IsRelativePath(relativePathFromBase) Then
            RaiseInvalidRelativePath(relativePathFromBase)
            Return ""
        ElseIf Not String.IsNullOrEmpty(relativePathFromBase) Then
            basePath = Path.Combine(basePath, relativePathFromBase)
        End If 
        If Not IsValidPath(targetPath) Then
            RaiseInvalidAbsolutePath(targetPath)
            Return ""
        End If 

        ' Assemble
        Dim fileNameWithExtension As String = ""
        If isTargetPathToFile Then
            fileNameWithExtension = FileName(targetPath)
            If Not String.IsNullOrEmpty(fileNameWithExtension) Then targetPath = PathDirectoryStub(targetPath)
        End If

        Dim basePathDirectories As List(Of String) = ListDirectoryNames(basePath)
        Dim targetPathDirectories As List(Of String) = ListDirectoryNames(targetPath)

        'Assumed targetPathDirectories is at or above basePathDirectories on the same branch, to start
        Dim numberOfSharedDirectories As Integer = NumberOfMatchingDirectories(basePathDirectories, targetPathDirectories)
        Dim uniquePath As String = Path.Combine(UniquePathSegment(targetPathDirectories, numberOfSharedDirectories), fileNameWithExtension)

        'newDirUnique = Path.Combine(newDirUnique, fileNameWithExtension)

        ' Case: targetPathDirectories is at or below basePathDirectories
        If (numberOfSharedDirectories = basePathDirectories.Count) Then Return uniquePath

        ' Case: targetPathDirectories is above basePathDirectories, or on a separate branch from basePathDirectories
        '   Append number of unique directories of basePathDirectories to move up to the unique targetPathDirectories path. 
        '   i.e. determine how many unique directories basePathDirectories has
        Dim movingUpDirectoriesPath As String = UniquePathSegment(basePathDirectories, numberOfSharedDirectories, "..")
        Return Path.Combine(movingUpDirectoriesPath, uniquePath)       
    End Function

    ''' <summary>
    ''' Converts relative path to absolute path.
    ''' Format is not to end relative path with a "\" for folder destinations.
    ''' Returns basePath[/relativePathFromBase] if fails.
    ''' </summary>
    ''' <param name="basePath">Absolute path to the base directory, such as that of the running application or DLL.</param>
    ''' <param name="targetPath">Relative path to target directory.</param>
    ''' <param name="relativePathFromBase">If base reference is to be relative to the base directory, specify the relative path difference with this parameter. 
    ''' The presence of starting and endings slashes does not matter. 
    ''' Currently only supports relative path to a child directory of the base path.</param>
    ''' <returns></returns>
    ''' <remarks>Format right now is to end absolute path without a "\".</remarks>
    Public Shared Function ConvertPathRelativeToAbsolute(ByVal basePath As String,
                                                         ByVal targetPath As String,
                                                         Optional ByVal relativePathFromBase As String = "") As String
        ' Clean
        targetPath = CleanPath(targetPath)
        basePath = CleanPath(basePath)
        relativePathFromBase = CleanPath(relativePathFromBase) 
        
        ' Validate
        If Not IsValidPath(basePath) Then
            RaiseInvalidAbsolutePath(basePath)
            Return ""
        End If
        If Not IsRelativePath(relativePathFromBase) Then
            RaiseInvalidRelativePath(relativePathFromBase)
            Return ""
        ElseIf Not String.IsNullOrEmpty(relativePathFromBase) Then
            basePath = Path.Combine(basePath, relativePathFromBase)
        End If
        If Not IsRelativePath(targetPath) Then
            RaiseInvalidRelativePath(targetPath)
            Return ""
        End If 
       
         'Case: Path at current location
        If String.IsNullOrEmpty(targetPath) Then Return basePath
        
        Dim basePathDirectories As List(Of String) = ListDirectoryNames(basePath)
        Dim targetPathDirectories As List(Of String) = ListDirectoryNames(targetPath)

        ' Determine truncated relative path based on the number of directories to move up
        Dim numberOfDirectoriesAbove As Integer = 0
        Dim truncatedPath As String = ""
        For Each directory As String In targetPathDirectories
            If directory = ".." Then
                numberOfDirectoriesAbove += 1
            Else
                truncatedPath = Path.Combine(truncatedPath, directory)
            End If 
        Next

        Dim finalPath As String = ""
        Dim maxBaseDirectoriesIndex As Integer = basePathDirectories.Count - numberOfDirectoriesAbove - 1
        If maxBaseDirectoriesIndex > 0 Then
            For i As Integer = 1 To maxBaseDirectoriesIndex
                finalPath = Path.Combine(finalPath, basePathDirectories(i))
            Next
        End If
        Return Path.Combine(basePath.Remove(3), finalPath, truncatedPath)
    End Function
#End Region

#Region "Browsing-Related"
    ''' <summary>
    ''' Obtains path to the application, not including the file name.
    ''' </summary>
    ''' <returns>Path to the application, not including the file name.</returns>
    ''' <remarks></remarks>
    Public Shared Function PathStartupApp() As String
        Return Application.StartupPath
    End Function

     ''' <summary>
    ''' Obtains path to the running *.dll file, not including the file name.
    ''' </summary>
    ''' <returns>Path to the running *.dll file, not including the file name.</returns>
    ''' <remarks></remarks>
    Public Shared Function PathStartupDll() As String
        Return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    End Function

    ''' <summary>
    ''' Returns a default path where the directory name provided is located within the application directory.
    ''' </summary>
    ''' <param name="name">Name of folder on the next level relative to the application location</param>
    ''' <remarks></remarks>
    Public Shared Function DefaultPathApp(ByVal name As String) As String
        Return Path.Combine(PathStartupApp(), name)
    End Function

    ''' <summary>
    ''' Returns a default path where the directory name provided is located within the running *.dll directory.
    ''' </summary>
    ''' <param name="name">Name of folder on the next level relative to the running *.dll location</param>
    ''' <remarks></remarks>
    Public Shared Function DefaultPathDll(ByVal name As String) As String
        Return Path.Combine(PathStartupDll(), name)
    End Function

    ''' <summary>
    ''' Sets starting directory to an existing folder, accounting for relative vs. absolute paths, and paths to files. 
    ''' Assigns a default if path is not valid.
    ''' If it is already valid, path is returned unmodified (apart from excluding any existing file name).
    ''' </summary>
    ''' <param name="pathCheckConvert">Path to be checked.</param>
    ''' <param name="defaultRelativePath">Default path is to the program. 
    ''' Add any additional relative path to a subdirectory of the program for any altered path returned.</param>
    ''' <remarks></remarks>
    Public Shared Function CurrentDirectoryPath(ByVal pathCheckConvert As String,
                                                Optional ByVal defaultRelativePath As String = "") As String
        Dim defaultApplicationPath As String = PathStartupApp()
        
        'If value exists, change to absolute path if it is a relative path
        If (Not String.IsNullOrEmpty(pathCheckConvert) AndAlso
            IsRelativePath(pathCheckConvert)) Then
            pathCheckConvert = ConvertPathRelativeToAbsolute(defaultApplicationPath, pathCheckConvert)
        End If

        'Sets default relative location if path is empty
        If String.IsNullOrEmpty(pathCheckConvert) Then pathCheckConvert = NewDefaultPath(defaultRelativePath)

        'Removes the filename from the path so that the path is to a directory
        If (File.Exists(pathCheckConvert)) Then pathCheckConvert = PathDirectoryStub(pathCheckConvert)

        'Sets default relative location if path does not exist
        If (Not Directory.Exists(pathCheckConvert)) Then pathCheckConvert = NewDefaultPath(defaultRelativePath)

        Return pathCheckConvert
    End Function

#End Region

#Region "Check Path Properties"
    
    ''' <summary>
    ''' Checks whether a path is relative or absolute by the presence of the ":" symbol.
    ''' </summary>
    ''' <param name="path">Path to be checked for relative vs. absolute type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsRelativePath(ByVal path As String) As Boolean
        If (path Is Nothing OrElse
            HasAllWhiteSpace(path)) Then Return False
        
        Return (Not InStr(path, ":") = 2 )
    End Function

    ''' <summary>
    ''' Returns true if the absolute path is valid for a Windows file or directory.
    ''' </summary>
    ''' <param name="pathCheck">Absolute path to check.</param>
    ''' <returns></returns>
    Public Shared Function IsValidPath(ByVal pathCheck As String) As Boolean
        If Not IsValidDirectory(pathCheck) Then Return False
        If Not IsValidName(pathCheck) Then Return False
        Return True
    End Function

    ''' <summary>
    ''' Returns true if the directory is valid for a Windows path.
    ''' </summary>
    ''' <param name="pathCheck">Absolute path to check.</param>
    ''' <returns></returns>
    Public Shared Function IsValidDirectory(ByVal pathCheck As String) As Boolean
        If (String.IsNullOrWhiteSpace(pathCheck) OrElse
            pathCheck.Length < 3)Then Return False

        Dim driveCheck As new Regex("^[a-zA-Z]:\\$")
        Return (driveCheck.IsMatch(pathCheck.Substring(0, 3)))
    End Function

    ''' <summary>
    ''' Returns true if the name is valid for a Windows directory, file or file and its extension.
    ''' </summary>
    ''' <param name="nameCheck">Name or path to check.</param>
    ''' <returns></returns>
    Public Shared Function IsValidName(ByVal nameCheck As String) As Boolean
        If String.IsNullOrWhiteSpace(nameCheck) Then Return False

        Dim invalidFileNameCharacters As New string(Path.GetInvalidPathChars())
        invalidFileNameCharacters += ":/?*" + """"
        Dim containsABadCharacter As new Regex("[" + Regex.Escape(invalidFileNameCharacters) + "]")

        Dim offset As Integer = 0
        If IsValidDirectory(nameCheck) Then
            offset = 3
            If nameCheck.Length = 3 Then Return False 'Not a valid file or folder name if it is only a directory name.
        End If 
 
        Return (Not containsABadCharacter.IsMatch(nameCheck.Substring(offset, nameCheck.Length - offset)))
    End Function

    ''' <summary>
    ''' Checks name provided and determines if name includes file extension by checking for the existing of a "." at the specified spacing from the end.  
    ''' Returns true if extension is found.
    ''' </summary>
    ''' <param name="name">Name to check, as a string.</param>
    ''' <param name="extensionLength">Number of characters in the file extension after the period. e.g. sdb = 3.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileNameHasExtension(ByVal name As String,
                                                ByVal extensionLength As Integer) As Boolean
        If (String.IsNullOrEmpty(name) OrElse
            extensionLength <= 0) Then Return False

        Return (Mid(Right(name, extensionLength + 1), 1, 1) = ".")
    End Function

    ''' <summary>
    ''' Determines whether or not the file path provided points to a file with an extension.
    ''' </summary>
    ''' <param name="pathFile">Path to a file, or the file name.</param>
    ''' <returns></returns>
    ''' <remarks>Currently cannot auto determine if path points to a directory vs. a file. 
    ''' This due to the two possible cases: 
    ''' 1. Filename w/o extension listed. 
    ''' 2. Directories with "." in the names.</remarks>
    Public Shared Function FileNameHasExtension(ByVal pathFile As String) As Boolean
        Return Not FileNameHasNoExtension(pathFile)
    End Function

    ''' <summary>
    ''' Determines whether or not the file path provided points to a file with no extension.
    ''' </summary>
    ''' <param name="pathFile">Path to a file, or the file name.</param>
    ''' <returns></returns>
    ''' <remarks>Currently cannot auto determine if path points to a directory vs. a file. 
    ''' This due to the two possible cases: 
    ''' 1. Filename w/o extension listed. 
    ''' 2. Directories with "." in the names.</remarks>
    Public Shared Function FileNameHasNoExtension(ByVal pathFile As String) As Boolean
        Dim fileName As String = PathLibrary.FileName(pathFile)
        Return StringsMatch(GetPrefix(fileName, "."), fileName)
    End Function


    ''' <summary>
    ''' Returns true if a match is found of either the file name, extension, or both to the full filename and extension.
    ''' If a name/extension parameter is omitted, it is assumed that any value is a match.
    ''' </summary>
    ''' <param name="fileNameWithExtensionSource">Full file name with extension.
    ''' Can also be full file path.</param>
    ''' <param name="fileNameCompare">File name to compare.</param>
    ''' <param name="fileExtensionCompare">File extension to compare.</param>
    ''' <param name="partialNameMatch">True: File will be matched if any part of the name provided matches the file name. 
    ''' False: File will only be matched if by the name criteria if there is an exact match.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileNameExtensionsMatch(ByVal fileNameWithExtensionSource As String,
                                                  Optional ByVal fileNameCompare As String = "",
                                                  Optional ByVal fileExtensionCompare As String = "",
                                                  Optional ByVal partialNameMatch As Boolean = False) As Boolean
        If String.IsNullOrWhiteSpace(fileNameWithExtensionSource) Then Return False
        
        Dim noFileComparisonCriteriaGiven As Boolean = (String.IsNullOrEmpty(fileNameCompare) AndAlso
                                                        String.IsNullOrEmpty(fileExtensionCompare))
        ' No criteria given. File matches itself
        If noFileComparisonCriteriaGiven Then Return True

        ' Get only the file extension without the period. 
        ' This is for a normalized comparison, so it won't matter if the user writes *., ., or merely the extension
        Dim fileExtensionSource As String = GetSuffix(fileNameWithExtensionSource, ".")
        fileExtensionCompare = GetSuffix(fileExtensionCompare, ".")

        Dim fileNameSource As String = FileName(GetSuffix(fileNameWithExtensionSource, "\"), noExtension:=True)
        If String.IsNullOrWhiteSpace(fileNameSource) Then Return False

        Dim nameMatch As Boolean = IsNameMatching(fileNameSource, fileNameCompare, partialNameMatch)

        'Based on supplied file name & file extension, sets function value
        If nameMatch Then
            ' Name matches, no extension provided for comparison
            If String.IsNullOrEmpty(fileExtensionCompare) Then Return True

            ' Final check based on file extensions
            Return FileNameExtensionsMatchByExtensionsOnly(fileExtensionSource, fileExtensionCompare)
        ElseIf (Not String.IsNullOrEmpty(fileExtensionCompare) AndAlso
                String.IsNullOrWhiteSpace(fileNameCompare)) Then
            Return FileNameExtensionsMatchByExtensionsOnly(fileExtensionSource, fileExtensionCompare)
        Else
            Return False
        End If
    End Function

   

    '' TODO: See implementation in CSiTester. See about improving this, e.g. handling of '?'. Is it necessary? If so, perhaps make the number of spaces matching or generic variable?
    ''' <summary>
    ''' Checks if the generic file extension, such as *.k??? for any value after k of 3 spaces, matches. Returns true if a match is found.
    ''' </summary>
    ''' <param name="fileExtensionGeneric">Generic file extension to search for.</param>
    ''' <param name="fileExtensionActual">File extension of the file being checked.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GenericExtensionsMatch(ByVal fileExtensionGeneric As String,
                                                  ByVal fileExtensionActual As String) As Boolean
        If Left(fileExtensionGeneric, 1) = "." Then fileExtensionGeneric = fileExtensionGeneric.Remove(0, 1)
        If Left(fileExtensionActual, 1) = "." Then fileExtensionActual = fileExtensionActual.Remove(0, 1)

        Return (Right(fileExtensionGeneric, 1) = "?" AndAlso
                StringsMatch(Left(fileExtensionGeneric, 1), Left(fileExtensionActual, 1)) AndAlso
                Len(fileExtensionGeneric) = Len(fileExtensionActual))
    End Function
#End Region

#Region "Gets Portion of Path"
    ''' <summary>
    ''' Returns the file name included in a path.
    ''' </summary>
    ''' <param name="pathFile">Path to file. Can be relative or absolute.</param>
    ''' <param name="noExtension">True: Returns only the name without any file extension. 
    ''' Will not work for files with no extension, but containing a "." in the filename. 
    ''' False: File extension, if present, is not removed.</param>
    ''' <returns>File name or folder name</returns>
    ''' <remarks>Currently cannot auto determine if path points to a directory unless the path already exists. 
    ''' This due to the two possible cases: 
    ''' 1. Filename w/o extension listed. 
    ''' 2. Directories with "." in the names.</remarks>
    Public Shared Function FileName(ByVal pathFile As String,
                                    Optional ByVal noExtension As Boolean = False) As String

        If (Directory.Exists(pathFile) OrElse
            String.IsNullOrWhiteSpace(pathFile)) Then Return ""

        Dim currentFileName As String = GetSuffix(pathFile, "\")
        If noExtension Then
             Dim extension As String = GetSuffix(currentFileName, ".")
            currentFileName = FilterFromText(currentFileName, "." & extension, retainPrefix:= True, retainSuffix:= False)
        End If

        Return currentFileName
    End Function

    ''' <summary>
    ''' Returns the filename without a file extension. 
    ''' This works regardless of whether or not the provided filename has an extension.
    ''' </summary>
    ''' <param name="fileNameCheck">File name to return without an extension.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileNameWithoutExtension(ByVal fileNameCheck As String) As String
        Return FileName(fileNameCheck, noExtension:= true)
    End Function

    ''' <summary>
    ''' Gets the path to the directory of the filepath provided. 
    ''' If no filename exists, the path is returned minus the lowest directory.
    ''' </summary>
    ''' <param name="pathDirectory">Path to be checked for the directory.</param>
    ''' <returns>Path to a directory.</returns>
    ''' <remarks>Currently cannot auto determine if file is in path. 
    ''' This due to the two possible cases: 
    ''' 1. Filename w/o extension listed, 
    ''' 2. Directories with "." in the names</remarks>
    Public Shared Function PathDirectoryStub(ByVal pathDirectory As String) As String
        If String.IsNullOrWhiteSpace(pathDirectory) Then Return ""

        Dim i As Integer = Len(pathDirectory) - (Len(pathDirectory) - InStrRev(pathDirectory, "\")) - 1
        If i < 0 Then
            Return pathDirectory
        Else
            Return Left(pathDirectory, i)
        End If
    End Function

    ''' <summary>
    ''' Returns the path to a directory at a number of directories specified above the lowest directory in the path supplied.
    ''' </summary>
    ''' <param name="pathDirectory">Path to retrieve path directory component from. 
    ''' If this is to a file, the directory containing the file is the lowest one returned (numberOfDirectories=0).</param>
    ''' <param name="numberOfDirectories">Number of directories to move up in the path supplied. 
    ''' For numberOfDirectories=0, if the path is to a directory, the same path is returned, and if the path is to a file, the path to the directory containing the file is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PathDirectorySubStub(ByVal pathDirectory As String,
                                                ByVal numberOfDirectories As Integer) As String
        If String.IsNullOrWhiteSpace(pathDirectory) Then Return ""
        Dim pathSubStub As String = pathDirectory

        If File.Exists(pathDirectory) Then pathSubStub = PathDirectoryStub(pathDirectory)

        If numberOfDirectories <= 0 Then
            Return pathSubStub
        Else
            For i = 1 To numberOfDirectories
                pathSubStub = FilterFromText(pathSubStub, "\" & GetSuffix(pathSubStub, "\"), True, False, True)
            Next

            Return pathSubStub
        End If
    End Function
    
    ''' <summary>
    ''' Returns a unique path segment for a supplied path, as compared to some other compared path, expressed by other parameters. 
    ''' Path segment will not have a trailing slash.
    ''' </summary>
    ''' <param name="directories">List of directory components of the path, in the order occurring in the path.</param>
    ''' <param name="numberDirectoriesEqual">&gt;=0 Number of directory components in the path that are equal to the path compared.</param>
    ''' <param name="substituteDirectory">For each unique directory component. 
    ''' If blank, the directory components themselves are used. 
    ''' If specified, the specification will be an overridden constant.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UniquePathSegment(ByVal directories As List(Of String),
                                             ByVal numberDirectoriesEqual As Integer,
                                             Optional ByVal substituteDirectory As String = "") As String
        Dim currentUniquePathSegment As String = ""
        If (directories Is Nothing) Then Return currentUniquePathSegment

        If numberDirectoriesEqual < 0 Then numberDirectoriesEqual = 0

        Dim numberOfUniqueDirectories As Integer = directories.Count - numberDirectoriesEqual
        If (numberOfUniqueDirectories > 0) Then
            For i = 0 To numberOfUniqueDirectories - 1
                Dim currentDirectory As String
                If String.IsNullOrEmpty(substituteDirectory) Then
                    currentDirectory = directories(numberDirectoriesEqual + i)
                Else
                    currentDirectory = substituteDirectory
                End If
                currentUniquePathSegment &= currentDirectory & "\"
            Next

            currentUniquePathSegment = TrimBackSlash(currentUniquePathSegment)
        End If
        Return currentUniquePathSegment
    End Function
#End Region

#Region "Adjusting-Cleaning Paths"
    ''' <summary>
    ''' Cleans the path by removing all whitespace, all starting and ending backslashes, and cleaning all repeating backslashes.
    ''' </summary>
    ''' <param name="path">Path to clean.</param>
    ''' <returns></returns>
    Public Shared Function CleanPath(Byval path As String) As String
        If String.IsNullOrEmpty(path) Then Return ""
        path = path.Trim()
        path = TrimBackSlash(path, trimStart:=True, trimEnd:= True)
        path = CleanAllRepeatingBackSlashes(path)
        Return path
    End Function

    ''' <summary>
    ''' Reduces all repeating slashes in a path to a single slash.
    ''' </summary>
    ''' <param name="path">Path to clean.</param>
    ''' <remarks></remarks>
    Public Shared Function CleanAllRepeatingBackSlashes(ByVal path As String) As String
        Dim cleanedPath As String = ""
        If String.IsNullOrEmpty(path) Then Return cleanedPath

        For index = 0 To path.Length - 1
            If (0 < index AndAlso
                Not (path(index) = "\" AndAlso
                     path(index-1) = "\")) Then
                cleanedPath &= path(index)
            ElseIf (index = 0) Then
                cleanedPath &= path(index)
            End If 
        Next

        Return cleanedPath
    End Function
    
    ''' <summary>
    ''' Removes any trailing "\" characters from a path. 
    ''' Works for multiple ending slashes.
    ''' </summary>
    ''' <param name="path">Path to trim.</param>
    ''' <param name="trimStart">True: Slashes at the start of the path will also be trimmed.</param>
    ''' <param name="trimEnd">False: Slashes at the end of the path will not be trimmed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TrimBackSlash(ByVal path As String,
                                         Optional ByVal trimStart As Boolean = False,
                                         Optional ByVal trimEnd As Boolean = True) As String
        If String.IsNullOrEmpty(path) Then Return ""
       
        'For case of path at current location, where the address is only a single "\"
        If (path.Length = 1 AndAlso 
            path(0) = "\") Then Return path
       
        If trimStart Then path = TrimStartingSlashes(path)
        If trimEnd Then path = TrimEndingSlashes(path)
        
        Return path
    End Function

    ''' <summary>
    ''' Adds leading and/or trailing "\" character to a path, if one does not exist where specified. 
    ''' Ensures that only one exists at these locations.
    ''' </summary>
    ''' <param name="path">Path to add slashes to.</param>
    ''' <param name="addStart">True: Adds slash to the start of the path.</param>
    ''' <param name="addEnd">False: Slash will not be added to the end of the path.</param>
    ''' <returns>File path with a single trailing "\" character at the end.</returns>
    ''' <remarks></remarks>
    Public Shared Function AddBackSlash(ByVal path As String,
                                        Optional ByVal addStart As Boolean = False,
                                        Optional ByVal addEnd As Boolean = True) As String
        If String.IsNullOrWhiteSpace(path) Then Return ""

        'For case of path at current location, where the address is only a single "\"
        If (path IsNot Nothing AndAlso
            path.Length = 1 AndAlso 
            path(0) = "\") Then Return path

        If (Not addStart AndAlso Not addEnd) Then Return path

        Dim newPath As String = ""
        If addStart Then
            newPath = TrimBackSlash(path, trimStart:=True, trimEnd:=False)
            newPath = "\" & newPath
        End If
        
        If addEnd Then
            newPath = TrimBackSlash(path, trimStart:=False, trimEnd:=True)
            newPath &= "\"
        End If

        Return newPath
    End Function

    ''' <summary>
    ''' Ensures that a file extension is returned without a period.
    ''' </summary>
    ''' <param name="fileExtension">File extension to check, and modify if needed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileExtensionClean(ByVal fileExtension As String) As String
        If String.IsNullOrWhiteSpace(fileExtension) Then Return ""

        If Left(fileExtension, 1) = "." Then fileExtension = Right(fileExtension, Len(fileExtension) - 1)

        Return fileExtension
    End Function

     ''' <summary>
    ''' Ensures that a file extension is returned with a period.
    ''' </summary>
    ''' <param name="fileExtension">File extension to check, and modify if needed.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileExtensionWithPeriod(ByVal fileExtension As String) As String
        If String.IsNullOrWhiteSpace(fileExtension) Then Return ""

        If Not Left(fileExtension, 1) = "." Then fileExtension = "." & fileExtension

        Return fileExtension
    End Function
#End Region

#Region "Lists"
    
    ''' <summary>
    ''' Collect specified path segments into a list of the component directories. 
    ''' This will include any file name if included in the path.
    ''' </summary>
    ''' <param name="path">Path to use.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDirectoryNames(ByVal path As String) As List(Of String)
        Dim directories As New List(Of String)
        If String.IsNullOrWhiteSpace(path) Then Return directories

        Dim currentDirectory As String = ""
        Dim lastIndex As Integer = Len(path)
        For i = 1 To lastIndex
            Dim currentCharacter As String = Mid(path, i, 1)
            If currentCharacter = "\" Then       
                'Directory change encountered
                'Checking currentDirectory="" handles cases of multiple "\"
                If Not String.IsNullOrEmpty(currentDirectory) Then directories.Add(currentDirectory)
                currentDirectory = ""
            ElseIf i = lastIndex Then    
                'Final directory encountered
                currentDirectory &= currentCharacter
                directories.Add(currentDirectory)
            Else
                currentDirectory &= currentCharacter
            End If
        Next

        Return directories
    End Function

    ''' <summary>
    ''' Creates a list of paths to all files within a directory of a given file name, or type, or all files. 
    ''' If both file name and type are specified, only files matching both criteria will be listed. 
    ''' If neither file name nor type are specified, all files will be listed.
    ''' </summary>
    ''' <param name="rootPath">Path to the root directory</param>
    ''' <param name="includeSubDirectories">True: Function will check within all subfolders of the root directory. 
    ''' False: Function will only check root directory.</param>
    ''' <param name="ofFileName">Only files matching the name specified, but of any file type, will be added.</param>
    ''' <param name="ofFileExtension">Only files with the same filetype will be added.</param>
    ''' <param name="excludeOfFileName">List returned will exclude files of the given filename.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListFilePathsInDirectory(ByVal rootPath As String,
                                                    Optional ByVal includeSubDirectories As Boolean = True,
                                                    Optional ByVal ofFileName As String = "",
                                                    Optional ByVal ofFileExtension As String = "",
                                                    Optional ByVal excludeOfFileName As Boolean = False) As List(Of String)
        Dim pathList As New FileList(New NameExtensionFileValidator(ofFileName, ofFileExtension, excludeOfFileName))

        pathList = CType(RecursiveFileProcessor.ProcessDirectory(rootPath, pathList, includeSubDirectories), FileList)

        Return pathList.Paths
    End Function

    
    ''' <summary>
    ''' Creates a list of all paths to files that are Read Only.
    ''' </summary>
    ''' <param name="rootPath">Path to the root directory</param>
    ''' <param name="includeSubDirectories">True: Function will check within all subfolders of the root directory. 
    ''' False: Function will only check root directory.</param>
    ''' <param name="excludeFile">List returned will exclude files that are Read Only.</param>
    ''' <returns></returns>
    Public Shared Function ListFilePathsReadOnly(ByVal rootPath As String,
                                                 Optional ByVal includeSubDirectories As Boolean = True,
                                                 Optional ByVal excludeFile As Boolean = False) As List(of String)
        Dim pathList As New FileList(New ReadOnlyFileValidator(excludeFile))

        pathList = CType(RecursiveFileProcessor.ProcessDirectory(rootPath, pathList, includeSubDirectories), FileList)

        Return pathList.Paths
    End Function


    ''' <summary>
    ''' Gets all sub-directory folders and adds them to a supplied list of paths.
    ''' </summary>
    ''' <param name="rootPath">Path to the parent directory to begin the check.</param>
    ''' <param name="listSubDirectories">True: All subdirectories will be listed. 
    ''' False: Only the highest level of subdirectories will be listed.</param>
    ''' <remarks></remarks>
    Public Shared Function ListDirectories(ByVal rootPath As String,
                                           Optional ByVal listSubDirectories As Boolean = True) As List(Of String)
        Dim directoryList As New List(Of String)
        If (String.IsNullOrWhiteSpace(rootPath) OrElse
            Not Directory.Exists(rootPath)) Then Return directoryList

        Dim firstChildDirectories As List(Of String) = Directory.GetDirectories(rootPath).ToList()
            
        directoryList.AddRange(firstChildDirectories)
        If listSubDirectories Then
            For Each firstChildDirectory As String In firstChildDirectories
                directoryList.AddRange(ListDirectories(firstChildDirectory))
            Next
        End If

        Return directoryList
    End Function


    ''' <summary>
    ''' Lists the paths of all of the folders and subfolders within the specified directory that match the provided name.
    ''' </summary>
    ''' <param name="rootPath">Path to the directory to check.</param>
    ''' <param name="folderName">Only paths for the specified folder name will be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ListDirectoriesByName(ByVal rootPath As String,
                                                 ByVal folderName As String,
                                                 Optional ByVal includeSubDirectories As Boolean = True,
                                                 Optional ByVal excludeFolder As Boolean = False) As List(Of String)
        Dim pathList As New FolderList(New NameFolderValidator(folderName, excludeFolder))

        pathList = CType(RecursiveFolderProcessor.ProcessDirectory(rootPath, pathList, includeSubDirectories), FolderList)

        Return pathList.Paths
    End Function

#End Region

#Region "Helper Methods"
    Private Shared Sub RaiseInvalidAbsolutePath(ByVal path As String)
        RaiseEvent Messenger(New MessengerEventArgs("{0} is not a valid absolute path.", path))
    End Sub

    Private Shared Sub RaiseInvalidRelativePath(ByVal path As String)
        RaiseEvent Messenger(New MessengerEventArgs("{0} is not a valid relative path.", path))
    End Sub


    ''' <summary>
    ''' Determines the number of directories in the two lists match in name and index.
    ''' </summary>
    ''' <param name="directories1"></param>
    ''' <param name="directories2"></param>
    ''' <returns></returns>
    Private Shared Function NumberOfMatchingDirectories(ByVal directories1 As List(Of String),
                                                        ByVal directories2 As List(Of String)) As Integer
        Dim numberOfMatches As Integer = 0
        Dim numberOfDirectories As Integer = Math.Min(directories1.Count, directories2.Count)
        For i = 0 To numberOfDirectories - 1
            If Not StringsMatch(directories1(i), directories2(i)) Then Return numberOfMatches
            numberOfMatches += 1
        Next
        Return numberOfMatches
    End Function
  
    ''' <summary>
    ''' Retrusn the new default path of the existing running application or combination of it and the default relative path.
    ''' </summary>
    ''' <param name="defaultRelativePath">Any additional relative path to a subdirectory of the program for any altered path returned.</param>
    ''' <returns></returns>
    Private Shared Function NewDefaultPath(ByVal defaultRelativePath As String) As String
        Dim defaultApplicationPath As String = PathStartupApp()
        Dim pathCheckConvert As String = Path.Combine(defaultApplicationPath, defaultRelativePath)
        If Directory.Exists(pathCheckConvert) Then 
            Return pathCheckConvert  
        Else
            Return defaultApplicationPath  
        End If
    End Function

    ''' <summary>
    ''' Removes any "\" characters from the beginning of a path. 
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private Shared Function TrimStartingSlashes(ByVal path As String) As String
        Dim trimmingSlashes = True
        Dim trimmedPath As String = ""
        For leftIndex As Integer = 0 To (path.Length - 1)
            If (Not trimmingSlashes OrElse
                Not path(leftIndex) = "\") Then
                trimmingSlashes = False
                trimmedPath &= path(leftIndex)
            End If 
        Next
        Return trimmedPath
    End Function

    ''' <summary>
    ''' Removes any "\" characters from the end of a path.
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private Shared Function TrimEndingSlashes(ByVal path As String) As String
        Dim trimmingSlashes = True
        Dim trimmedPath As String = ""
        For rightIndex As Integer = (path.Length - 1) To 0 Step -1
            If (Not trimmingSlashes OrElse
                Not path(rightIndex) = "\") Then
                trimmingSlashes = False
                trimmedPath = path(rightIndex) & trimmedPath
            End If 
        Next
        Return trimmedPath
    End Function

    ''' <summary>
    ''' Determines exact or generic match of cleaned file extensions, regardless of capitalization.
    ''' </summary>
    ''' <param name="fileExtensionSource">Existing file extension, which cannot be generic.</param>
    ''' <param name="fileExtensionCompare">File extension to compare, which may be generic.</param>
    ''' <returns></returns>
    Private Shared Function FileNameExtensionsMatchByExtensionsOnly(ByVal fileExtensionSource As String,
                                                                    ByVal fileExtensionCompare As String) As Boolean
        If StringsMatch(fileExtensionCompare, fileExtensionSource) Then
            Return True
        ElseIf GenericExtensionsMatch(fileExtensionCompare, fileExtensionSource) Then       'Check generic format, such as *.y???
            Return True
        End If
        Return False
    End Function
#End Region

End Class
