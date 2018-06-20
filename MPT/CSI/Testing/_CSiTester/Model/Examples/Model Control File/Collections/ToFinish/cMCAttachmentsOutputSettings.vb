Option Explicit On
Option Strict On

Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary

Imports CSiTester.cMCModel
Imports CSiTester.cPathModel
Imports CSiTester.cPathAttachment
Imports CSiTester.cPathOutputSettings
Imports CSiTester.cFileAttachment

Public Class cMCAttachmentsOutputSettings
    Inherits cMCAttachments

#Region "Enumerations"

    Public Enum eOutputSettingsType
        <Description("Not Specified")> none = 0
        ''' <summary>
        ''' File is used occasionally, if moved into the same location as the model file.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Attachment")> attachment
        ''' <summary>
        ''' File is always in use, stored at the model file location.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Supporting File")> supportingFile
    End Enum
#End Region

#Region "Properties"

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Initializes a new collection that is set to reference the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to reference.</param>
    ''' <param name="p_osType">Specify whether or not the outputSettings file is to be in the active location or kept for occasional use.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_mcModel As cMCModel,
                   ByVal p_osType As eOutputSettingsType)
        '_attachmentType = eAttachmentType.images


        If p_osType = eOutputSettingsType.attachment Then
            'Place in position 0
        ElseIf p_osType = eOutputSettingsType.supportingFile Then
            'Place in position 1
        End If

        Dim testCollection As New cMCAttachmentsOutputSettings()

        'testCollection.Replace(

        Bind(p_mcModel)
    End Sub
#End Region

#Region "Methods: Friend"

    ' ''' <summary>
    ' ''' Replaces the matching attachment object with the one provided. 
    ' ''' A match is determined by either title or the file name.
    ' ''' </summary>
    ' ''' <param name="p_item"></param>
    ' ''' <remarks></remarks>
    'Friend Overloads Overrides Sub Replace(Of T)(ByVal p_item As T)
    '    If p_item Is Nothing Then Exit Sub
    '    Try
    '        For Each item As cFileOutputSettings In InnerList
    '            If StringsMatch(item.pathSource.fileNameWithExtension, p_item.pathSource.fileNameWithExtension) Then

    '                item = p_item
    '                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
    '                Exit For
    '            End If
    '        Next
    '    Catch argExc As ArgumentException
    '        RaiseEvent Log(New LoggerEventArgs(ex))
    '    End Try
    'End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the directory path for the attachment files based on the current attachment type and a database directory type.
    ''' </summary>
    ''' <param name="p_mcDirectory">Path to the model control directory.</param>
    ''' <remarks></remarks>
    Protected Overrides Sub SetDirectoryDatabase(ByVal p_mcDirectory As String)
        'Select Case outputSettingsType
        '    Case eOutputSettingsType.attachment
        '        _directoryDestination = p_mcDirectory & "\" & DIR_NAME_ATTACHMENTS_DEFAULT
        '    Case eOutputSettingsType.supportingFile
        '        _directoryDestination = p_mcDirectory & "\" & DIR_NAME_MODELS_DEFAULT
        'End Select
    End Sub

    ''' <summary>
    ''' Sets all attachment path destinations to reflect the same destination directory for the appropriate attachment type.
    ''' </summary>
    ''' <param name="p_folderStructure">Structure of the folders where the attachments will be copied.</param>
    ''' <remarks></remarks>
    Protected Overrides Sub NormalizeAttachmentsPaths(ByVal p_folderStructure As eFolderStructure)
        If String.IsNullOrEmpty(_directoryDestination) Then Exit Sub

        For Each attachment As cFileAttachment In InnerList
            'If (outputSettingsType = eOutputSettingsType.attachment AndAlso
            '    p_folderStructure = eFolderStructure.Flattened) Then

            '    With attachment.PathAttachment
            '        .SetProperties(_directoryDestination & "\" & DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT & "\" & .fileNameWithExtension)
            '    End With
            'Else
            '    With attachment.PathAttachment
            '        .SetProperties(_directoryDestination & "\" & .fileNameWithExtension)
            '    End With
            'End If
        Next
    End Sub

    ''' <summary>
    ''' If directory structure is not flattened, updates the directory path for the attachment type set.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ChangeDirectory()
        Dim mcDirectory As String = _directoryDestination
        Dim filterString As String = GetSuffix(mcDirectory, "\")

        Select Case filterString

        End Select
        mcDirectory = FilterStringFromName(mcDirectory, "\" & DIR_NAME_FIGURES_DEFAULT, p_retainPrefix:=True, p_retainSuffix:=False, p_endDirectory:=True)
        SetDirectoryDatabase(mcDirectory)
    End Sub

    ''' <summary>
    ''' Returns True if the attachment object is an outputSettings attachment.
    ''' </summary>
    ''' <param name="p_attachment">Attachment object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsOutputSettingsAttachment(ByVal p_attachment As cFileAttachment) As Boolean
        If StringExistInName(p_attachment.title, TAG_ATTACHMENT_TABLE_SET_FILE) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

End Class
