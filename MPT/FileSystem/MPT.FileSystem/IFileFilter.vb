Option Strict On
Option Explicit On

''' <summary>
''' Used for returning a list of files that are filtered based on some criteria.
''' </summary>
Public Interface IFileFilter
    ''' <summary>
    ''' Returns a list of path objects for all matching files.
    ''' </summary>
    ''' <param name="searchedFiles">List of files to search.</param>
    ''' <returns></returns>
    Function MatchingFiles(ByVal searchedFiles As List(Of FilePath)) As List(Of FilePath)

End Interface
