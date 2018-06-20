Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.Reporting

Namespace Processor
''' <summary>
''' Performs file processing actions on lists of files determined recursively through file directories.
''' </summary>
''' <remarks></remarks>
Public Class RecursiveFileProcessor
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

    ''' <summary>
    ''' Process all files in each directory path passed in, recurse on any directories that are found, and process the files they contain.
    ''' </summary>
    ''' <param name="args">List of root directories within which files are to be processed.</param>
    ''' <param name="fileProcessor">Object containing the logic to use for file processing.</param>
    ''' <param name="includeSubDirectories">True: Method will include all paths in subdirectories.</param>
    ''' <remarks></remarks>
    Public Shared Function ProcessPaths(ByVal args() As String,
                                        ByVal fileProcessor As IProcessFile,
                                        Optional ByVal includeSubDirectories As Boolean = True) As IProcessFile
        For Each path As String In args
            If File.Exists(path) Then
                fileProcessor = ProcessFile(path, fileProcessor)
            ElseIf Directory.Exists(path) Then
                fileProcessor = ProcessDirectory(path, fileProcessor, includeSubDirectories)
            Else
                RaiseMessengerInvalidFile(path)
            End If
        Next

        Return fileProcessor
    End Function


    ''' <summary>
    ''' Process all files in the directory passed in, recurse on any directories that are found, and process the files they contain.
    ''' </summary>
    ''' <param name="targetDirectory">Path to the root directory within which files are to be processed.</param>
    ''' <param name="fileProcessor">Object containing the logic to use for file processing.</param>
    ''' <param name="includeSubDirectories">True: Method will include all paths in subdirectories.</param>
    ''' <remarks></remarks>
    Public Shared Function ProcessDirectory(ByVal targetDirectory As String,
                                            ByVal fileProcessor As IProcessFile,
                                            Optional ByVal includeSubDirectories As Boolean = True) As IProcessFile
        If Not Directory.Exists(targetDirectory) Then
            RaiseMessengerInvalidDirectory(targetDirectory)
            Return fileProcessor
        End If

        ' Process the list of files found in the directory.
        Dim fileEntries As String() = Directory.GetFiles(targetDirectory)
        fileProcessor = fileEntries.Aggregate(fileProcessor, Function(current, fileName) ProcessFile(fileName, current))

        If includeSubDirectories Then
            ' Recurse into subdirectories of this directory.
            Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
            fileProcessor = subdirectoryEntries.Aggregate(fileProcessor, Function(current, subdirectory) ProcessDirectory(subdirectory, current))
        End If
        Return fileProcessor
    End Function

    ''' <summary>
    ''' Performs the file processing operation on the file specified with the supplied object.
    ''' </summary>
    ''' <param name="path">Path to the file to process.</param>
    ''' <param name="fileProcessor">Object containing the logic to use for file processing.</param>
    ''' <remarks></remarks>
    Public Shared Function ProcessFile(ByVal path As String,
                                       ByVal fileProcessor As IProcessFile) As IProcessFile
        If File.Exists(path) Then
            fileProcessor.ProcessFile(path)
        Else
            RaiseMessengerInvalidFile(path)
        End If
        Return fileProcessor
    End Function


    Private Shared Sub RaiseMessengerInvalidDirectory(ByVal path As String)
        RaiseEvent Messenger(New MessengerEventArgs("{0} is not a valid directory.", path))
    End Sub

    Private Shared Sub RaiseMessengerInvalidFile(ByVal path As String)
        RaiseEvent Messenger(New MessengerEventArgs("{0} is not a valid file.", path))
    End Sub
End Class
End Namespace
