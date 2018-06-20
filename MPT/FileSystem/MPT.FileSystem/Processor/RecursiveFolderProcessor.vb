Option Strict On
Option Explicit On

Imports System.IO

Imports MPT.Reporting

Namespace Processor
    ''' <summary>
    ''' Performs folder processing actions on lists of folders determined recursively through file directories.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RecursiveFolderProcessor
        Shared Event Messenger(message As MessengerEventArgs)

        ''ncrunch: no coverage start
        Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
        End Sub
        ''ncrunch: no coverage end

        ''' <summary>
        ''' Process all folders in each directory path passed in, recurse on any directories that are found, and process the files they contain.
        ''' </summary>
        ''' <param name="args">List of root directories within which folders are to be processed.</param>
        ''' <param name="folderProcessor">Object containing the logic to use for folder processing.</param>
        ''' <param name="includeSubDirectories">True: Method will include all paths in subdirectories.</param>
        ''' <remarks></remarks>
        Public Shared Function ProcessPaths(ByVal args() As String,
                                            ByVal folderProcessor As IProcessFolder,
                                            Optional ByVal includeSubDirectories As Boolean = True) As IProcessFolder
            For Each path As String In args
                If Directory.Exists(path) Then
                    folderProcessor = ProcessDirectory(path, folderProcessor, includeSubDirectories)
                Else
                   RaiseMessengerInvalidDirectory(path)
                End If
            Next

            Return folderProcessor
        End Function


        ''' <summary>
        ''' Process all folders in the directory passed in, recurse on any directories that are found, and process the folders they contain.
        ''' </summary>
        ''' <param name="targetDirectory">Path to the root directory within which folders are to be processed.</param>
        ''' <param name="folderProcessor">Object containing the logic to use for folder processing.</param>
        ''' <param name="includeSubDirectories">True: Method will include all paths in subdirectories.</param>
        ''' <remarks></remarks>
        Public Shared Function ProcessDirectory(ByVal targetDirectory As String,
                                                ByVal folderProcessor As IProcessFolder,
                                                Optional ByVal includeSubDirectories As Boolean = True) As IProcessFolder
            ' Process the directory.
            If Directory.Exists(targetDirectory) Then
                folderProcessor.ProcessFolder(targetDirectory)
            Else
                RaiseMessengerInvalidDirectory(targetDirectory)
                Return folderProcessor
            End If

            If includeSubDirectories Then
                ' Recurse into subdirectories of this directory.
                Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
                folderProcessor = subdirectoryEntries.Aggregate(folderProcessor, Function(current, subdirectory) ProcessDirectory(subdirectory, current))
            Else 
                ' This processes the children of the root directory, since they are the intended targets in this case
                Dim subdirectoryEntries As String() = Directory.GetDirectories(targetDirectory)
                For Each directory As String In subdirectoryEntries
                    folderProcessor.ProcessFolder(directory)
                Next
            End If
            Return folderProcessor
        End Function

        Private Shared Sub RaiseMessengerInvalidDirectory(ByVal path As String)
            RaiseEvent Messenger(New MessengerEventArgs("{0} is not a valid directory.", path))
        End Sub

    End Class
End Namespace