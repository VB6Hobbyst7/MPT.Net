Option Strict On
Option Explicit On

Namespace Validator
    ''' <summary>
    ''' Determines if a folder is valid by specified criteria.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValidateFolder
        ''' <summary>
        ''' True: Matching criteria will exclude the folder.
        ''' False: Matching criteria will include the folder.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ReadOnly Property ExcludeFolder As Boolean

        ''' <summary>
        ''' True: folder is valid by the specified criteria.
        ''' </summary>
        ''' <param name="path">Path to the folder to assess.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function IsValidFolder(ByVal path As String) As Boolean
    End Interface
End Namespace