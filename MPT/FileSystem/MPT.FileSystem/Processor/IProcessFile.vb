Option Strict On
Option Explicit On

Namespace Processor
    ''' <summary>
    ''' Processes a file based on provided criteria via the Strategy Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IProcessFile
        ''' <summary>
        ''' Processes the file according to criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to process.</param>
        ''' <remarks></remarks>
        Sub ProcessFile(ByVal path As String)
    End Interface
End Namespace

