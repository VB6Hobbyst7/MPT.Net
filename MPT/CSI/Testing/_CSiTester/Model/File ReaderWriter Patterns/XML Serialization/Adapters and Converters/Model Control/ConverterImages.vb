Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.ModelControl
Imports CSiTester.cFileAttachment


Friend Class ConverterImages

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCAttachments) As imagesImage()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As imagesImage

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
    Friend Shared Function ConvertToFile(ByVal p_value As cFileAttachment) As imagesImage
        Dim attachment As New imagesImage() With
            {.title = p_value.title,
             .path = p_value.pathDestination.pathRelative}

        Return attachment
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <param name="p_bindTo">Model control file that this object is to be associated with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As imagesImage(),
                                           ByVal p_bindTo As cMCModel) As cMCAttachments
        Dim obsCol As New cMCAttachments
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return obsCol

        For i = 0 To p_values.Count - 1
            obsCol.Add(ConvertFromFile(p_values(i), p_bindTo))
        Next

        Return obsCol
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <param name="p_bindTo">Model control file that this object is to be associated with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As imagesImage,
                                           ByVal p_bindTo As cMCModel) As cFileAttachment
        Dim directoryType As eAttachmentDirectoryType = cFileAttachment.AttachmentDirectoryType(p_value.title, p_addAsImage:=True)

        Dim attachment As New cFileAttachment(p_value.path, p_bindTo:=p_bindTo, p_attachmentType:=directoryType)
        attachment.title = p_value.title

        Return attachment
    End Function

End Class
