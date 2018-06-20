Option Strict On
Option Explicit On

Imports MPT.Reporting

Namespace Processor
    ''' <summary>
    ''' Abstract class that forms the basis for processing a folder based on provided criterie via the Strategy Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class FolderProcessor
        Implements IProcessFolder

        Shared Event Messenger(message As MessengerEventArgs)

        ''' <summary>
        ''' Processes the file according to criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to process.</param>
        ''' <remarks></remarks>
        Public Overridable Sub ProcessFolder(path As String) Implements IProcessFolder.ProcessFolder
            RaiseEvent Messenger(New MessengerEventArgs("Processed folder '{0}'.", path))
        End Sub
    End Class
End Namespace