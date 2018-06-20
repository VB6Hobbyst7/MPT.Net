Option Explicit On
Option Strict On

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.cMCModel
Imports CSiTester.cPathAttachment

Public Class cMCAttachmentImages
    Inherits cMCAttachments

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Initializes a new collection that is set to reference the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_mcModel As cMCModel)
        _attachmentType = eAttachmentType.images
        Bind(p_mcModel)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the directory path for the attachment files based on the current attachment type and a database directory type.
    ''' </summary>
    ''' <param name="p_mcDirectory">Path to the model control directory.</param>
    ''' <remarks></remarks>
    Protected Overrides Sub SetDirectoryDatabase(ByVal p_mcDirectory As String)
        _directoryDestination = p_mcDirectory & "\" & DIR_NAME_FIGURES_DEFAULT
    End Sub

    ''' <summary>
    ''' Sets all attachment path destinations to reflect the same destination directory for the appropriate attachment type.
    ''' </summary>
    ''' <param name="p_folderStructure">Structure of the folders where the attachments will be copied.</param>
    ''' <remarks></remarks>
    Protected Overrides Sub NormalizeAttachmentsPaths(ByVal p_folderStructure As eFolderStructure)
        If String.IsNullOrEmpty(_directoryDestination) Then Exit Sub

        For Each attachment As cFileAttachment In InnerList
            With attachment.PathAttachment
                .SetProperties(_directoryDestination & "\" & .fileNameWithExtension)
            End With
        Next
    End Sub


    ''' <summary>
    ''' If directory structure is not flattened, updates the directory path for the attachment type set.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ChangeDirectory()
        Dim mcDirectory As String = _directoryDestination

        mcDirectory = FilterStringFromName(mcDirectory, "\" & DIR_NAME_FIGURES_DEFAULT, p_retainPrefix:=True, p_retainSuffix:=False, p_endDirectory:=True)
        SetDirectoryDatabase(mcDirectory)
    End Sub
#End Region
End Class
