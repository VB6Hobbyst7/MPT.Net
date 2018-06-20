Option Strict On
Option Explicit On

''' <summary>
''' Used for returning a list of folders that are filtered based on some criteria.
''' </summary>
Public Interface IFolderFilter
    ''' <summary>
    ''' Returns a list of path objects for all matching folders.
    ''' </summary>
    ''' <param name="searchedFolders">List of folders to search.</param>
    ''' <returns></returns>
    Function MatchingFolders(ByVal searchedFolders As List(Of FilePath)) As List(Of FilePath)
    
End Interface