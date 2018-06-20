Option Strict On
Option Explicit On

''' <summary>
''' Enumeration of how to return the capitalization sense of a string.
''' </summary>
''' <remarks></remarks>
Public Enum eCapitalization
    ''' <summary>
    ''' Every character is capitalized.
    ''' </summary>
    ''' <remarks></remarks>
    ALLCAPS = 0
    ''' <summary>
    ''' Every character is lower-case.
    ''' </summary>
    ''' <remarks></remarks>
    AllLower = 1
    ''' <summary>
    ''' The first character of every word is capitalized, with all other characters as lower-case.
    ''' </summary>
    ''' <remarks></remarks>
    Firstupper = 2
End Enum