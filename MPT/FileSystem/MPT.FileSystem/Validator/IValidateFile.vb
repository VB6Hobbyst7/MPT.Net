Option Strict On
Option Explicit On

Namespace Validator
    ''' <summary>
    ''' Determines if a file is valid by specified criteria.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValidateFile
        ''' <summary>
        ''' True: Matching criteria will exclude the file.
        ''' False: Matching criteria will include the file.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ExcludeFile As Boolean

        ''' <summary>
        ''' True: File is valid by the specified criteria.
        ''' </summary>
        ''' <param name="path">Path to the file to assess.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function IsValidFile(ByVal path As String) As Boolean
    End Interface
End Namespace

