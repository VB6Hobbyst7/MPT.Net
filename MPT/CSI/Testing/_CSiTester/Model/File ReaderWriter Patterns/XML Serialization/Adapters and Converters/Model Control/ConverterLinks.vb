Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Friend Class ConverterLinks

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCLinks) As linksLink()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As linksLink

        For i = 0 To p_values.Count - 1
            fileArray(i) = ConvertToFile(p_values(i))
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As cMCLink) As linksLink
        Return New linksLink() With {.title = p_value.title, .url = p_value.URL}
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As linksLink()) As cMCLinks
        Dim links As New cMCLinks
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return links

        For i = 0 To p_values.Count - 1
            links.Add(ConvertFromFile(p_values(i)))
        Next

        Return links
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As linksLink) As cMCLink
        Return New cMCLink() With {.title = p_value.title, .URL = p_value.url}
    End Function

End Class
