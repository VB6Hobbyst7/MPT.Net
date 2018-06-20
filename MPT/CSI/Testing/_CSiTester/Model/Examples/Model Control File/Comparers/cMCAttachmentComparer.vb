Option Strict On
Option Explicit On

''' <summary>
''' Compares two attachment objects based on the example name and file name.
''' Objects are considered equal if either value is the same.
''' </summary>
''' <remarks></remarks>
Public Class cMCAttachmentComparer
    Inherits Comparer(Of cFileAttachment)

    Public Overrides Function Compare(x As cFileAttachment, y As cFileAttachment) As Integer

        If (x.title.CompareTo(y.title) = 0 OrElse
            x.PathAttachment.fileNameWithExtension.CompareTo(y.PathAttachment.fileNameWithExtension) = 0) Then
            Return 0
        ElseIf Not x.title.CompareTo(y.title) = 0 Then
            Return x.title.CompareTo(y.title)
        ElseIf Not x.PathAttachment.fileNameWithExtension.CompareTo(y.PathAttachment.fileNameWithExtension) = 0 Then
            Return x.PathAttachment.fileNameWithExtension.CompareTo(y.PathAttachment.fileNameWithExtension)
        Else
            Return 0
        End If
    End Function
End Class
