Option Explicit On
Option Strict On

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.PathLibrary

Imports CSiTester.ModelControl

Friend Class ConverterDocumentationStatus

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As String) As status_documentation
        Return ConvertStringToEnumByXMLAttribute(Of status_documentation)(p_value)
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <param name="p_values">List of documentation statuses.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As status_documentation,
                                           ByVal p_values As IList(Of String)) As String
        Return GetListItemMatchingEnumByXMLAttribute(p_value, p_values)
    End Function

End Class
