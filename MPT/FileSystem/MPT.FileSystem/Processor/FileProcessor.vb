Option Strict On
Option Explicit On

Imports MPT.Reporting

Namespace Processor
    ''' <summary>
    ''' Abstract class that forms the basis for processing a file based on provided criterie via the Strategy Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class FileProcessor
        Implements IProcessFile

        Shared Event Messenger(message As MessengerEventArgs)

        ''' <summary>
        ''' Processes the file according to criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to process.</param>
        ''' <remarks></remarks>
        Public Overridable Sub ProcessFile(path As String) Implements IProcessFile.ProcessFile
            RaiseEvent Messenger(New MessengerEventArgs("Processed file '{0}'.", path))
        End Sub
    End Class
End Namespace
