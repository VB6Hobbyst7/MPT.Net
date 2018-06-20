''' <summary>
''' Compares two link objects based on the URL and link name.
''' Objects are considered equal if either value is the same.
''' </summary>
''' <remarks></remarks>
Public Class cMCLinkComparer
    Inherits Comparer(Of cMCLink)


    Public Overrides Function Compare(x As cMCLink, y As cMCLink) As Integer
        If (x.URL.CompareTo(y.URL) = 0 OrElse
            x.title.CompareTo(y.title) = 0) Then
            Return 0
        ElseIf Not x.URL.CompareTo(y.URL) = 0 Then
            Return x.URL.CompareTo(y.URL)
        ElseIf Not x.title.CompareTo(y.title) = 0 Then
            Return x.title.CompareTo(y.title)
        Else
            Return 0
        End If
    End Function
End Class
