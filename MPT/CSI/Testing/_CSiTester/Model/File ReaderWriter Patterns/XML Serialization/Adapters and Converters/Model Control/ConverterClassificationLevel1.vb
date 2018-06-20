Option Explicit On
Option Strict On

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary

Imports CSiTester.ModelControl

Friend Class ConverterClassificationLevel1
    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As String) As classifications_level1
        Return ConvertStringToEnumByXMLAttribute(Of classifications_level1)(p_value)
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <param name="p_values">List of documentation statuses.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As classifications_level1,
                                           ByVal p_values As IList(Of String)) As String
        Return GetListItemMatchingEnumByXMLAttribute(p_value, p_values)
    End Function
End Class
