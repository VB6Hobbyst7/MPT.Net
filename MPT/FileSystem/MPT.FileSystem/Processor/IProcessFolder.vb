Option Strict On
Option Explicit On

Namespace Processor
    ''' <summary>
    ''' Processes a folder based on provided criteria via the Strategy Pattern.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IProcessFolder
        ''' <summary>
        ''' Processes the folder according to criteria.
        ''' </summary>
        ''' <param name="path">Path to the folder to process.</param>
        ''' <remarks></remarks>
        Sub ProcessFolder(ByVal path As String)
    End Interface
End Namespace