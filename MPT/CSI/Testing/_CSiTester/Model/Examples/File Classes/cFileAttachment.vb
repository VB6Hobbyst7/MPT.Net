Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cMCAttachments

''' <summary>
''' Class containing a title and path to an attachment file associated with the example.
''' </summary>
''' <remarks></remarks>
Public Class cFileAttachment
    Inherits cMCFile

#Region "Enumerations"
    Public Enum eAttachmentDirectoryType
        none = 0
        attachment
        figure
        supportingFile
        attachmentOutputSettings
        supportingFileOutputSettings
    End Enum
#End Region

#Region "Constants"
    '=== Attachments Tags
    'TODO: Make these reference an enum with description, with ': "' appended at this location
    Friend Const TAG_ATTACHMENT_SUPPORTING_FILE As String = "Supporting File: "
    Friend Const TAG_ATTACHMENT_DOCUMENTATION As String = "Documentation: "
    Friend Const TAG_ATTACHMENT_DOCUMENTATION_PUBLISHED As String = "Published Documentation: "
    ''' <summary>
    ''' Used to indicate the presence of an outputSettings file.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TAG_ATTACHMENT_TABLE_SET_FILE As String = "Table Set File: "
#End Region

#Region "Properties"
    Private _title As String
    ''' <summary>
    ''' Title of the attachment.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
        Set(ByVal value As String)
            If Not _title = value Then
                _title = value
                RaisePropertyChanged(Function() Me.title)
            End If
        End Set
        Get
            Return _title
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Initializes the object with the path to an existing file.
    ''' </summary>
    ''' <param name="p_pathFile">Path to the attachment file.</param>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <param name="p_attachmentType">Type of attachment the object represents.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_pathFile As String = "",
                   Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_attachmentType As cFileAttachment.eAttachmentDirectoryType = cFileAttachment.eAttachmentDirectoryType.none)
        MyBase.New()
        InitializeObject(p_bindTo, p_attachmentType, p_pathFile)
    End Sub

    Protected Overloads Overrides Sub InitializeFile(Optional ByVal p_bindTo As cMCModel = Nothing,
                                                     Optional ByVal p_pathFile As String = "")
        'Not used. Instead, used 'InitializeObject'
    End Sub
    Protected Overloads Sub InitializeObject(Optional ByVal p_bindTo As cMCModel = Nothing,
                                            Optional ByVal p_attachmentType As cFileAttachment.eAttachmentDirectoryType = cFileAttachment.eAttachmentDirectoryType.none,
                                            Optional ByVal p_pathFile As String = "")
        Try
            _pathDestination = New cPathAttachment(p_bindTo, p_pathFile, p_attachmentType)
            CompleteInitialization()
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try
    End Sub

    Friend Overloads Overrides Function Clone() As Object
        Return Clone(Nothing)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">If specified, the model control reference will be switched to the one provided. 
    ''' Otherwise, the original reference is kept.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Overrides Function Clone(ByVal p_bindTo As cMCModel) As Object
        Dim myClone As cFileAttachment = DirectCast(MyBase.Clone(p_bindTo), cFileAttachment)

        Try
            With myClone
                ._pathDestination = CType(PathAttachment().Clone(p_bindTo), cPathAttachment)
                .title = title
            End With
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function
    Protected Overrides Function Create() As cMCFile
        Return New cFileAttachment()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cFileAttachment) Then Return False
        Dim comparedObject As cFileAttachment = TryCast(p_object, cFileAttachment)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(comparedObject) Then Return False

            Dim pathCast As cPathAttachment = PathAttachment()
            Dim pathCastCompare As cPathAttachment = .PathAttachment()
            If Not pathCastCompare.Equals(pathCast) Then Return False

            If Not .title = title Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend Shared"
    ''' <summary>
    ''' Returns the attachment directory type associated with the file based on the attachment title or overwrite.
    ''' Default is "Attachment".
    ''' </summary>
    ''' <param name="p_attachmentTitle">Title of the attachment file.</param>
    ''' <param name="p_addAsImage">True: The directory type associated with images will be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function AttachmentDirectoryType(Optional ByVal p_attachmentTitle As String = "",
                                                   Optional ByVal p_addAsImage As Boolean = False) As eAttachmentDirectoryType
        If String.IsNullOrEmpty(p_attachmentTitle) Then
            Return eAttachmentDirectoryType.attachment
        ElseIf StringExistInName(p_attachmentTitle, TAG_ATTACHMENT_TABLE_SET_FILE) Then
            Return eAttachmentDirectoryType.attachmentOutputSettings
        ElseIf StringExistInName(p_attachmentTitle, TAG_ATTACHMENT_SUPPORTING_FILE) Then
            Return eAttachmentDirectoryType.supportingFile
        ElseIf p_addAsImage Then
            Return eAttachmentDirectoryType.figure
        Else
            Return eAttachmentDirectoryType.attachment
        End If
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Binds the object to the state of the supplied model control object.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Overrides Sub Bind(ByVal p_bindTo As cMCModel)
        PathAttachment.SetMCModel(p_bindTo)

        _fileManager.SetDestinationPath(pathDestination)
    End Sub

    ''' <summary>
    ''' Returns the path object in the desired downcast class. 
    ''' If the path object was not upcast from this class, 'Nothing' is returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PathAttachment() As cPathAttachment
        Try
            Return DirectCast(_pathDestination, cPathAttachment)
        Catch ex As Exception
            OnLogger(New LoggerEventArgs(ex))
            Return Nothing
        End Try
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path object provided is of a matching type.
    ''' </summary>
    ''' <param name="p_path">Path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function isMatchingPathType(ByVal p_path As cPath) As Boolean
        If TypeOf p_path Is cPathAttachment Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
End Class
