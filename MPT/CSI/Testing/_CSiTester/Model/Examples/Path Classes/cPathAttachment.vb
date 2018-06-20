Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

Imports CSiTester.cMCModel
Imports CSiTester.cFileAttachment
Imports CSiTester.cPathModel
Imports CSiTester.cPathOutputSettings

''' <summary>
''' Class representing a path to a file attachment for a model.
''' </summary>
''' <remarks></remarks>
Public Class cPathAttachment
    Inherits cPathModelControlReference
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Event Handlers"
    Protected Sub RaiseMCFolderStructureChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.folderStructure) Then
            SetNewPath()
        End If
    End Sub
#End Region


#Region "Constants"
    Friend Const DIR_NAME_ATTACHMENTS_DEFAULT As String = "attachments"
    Friend Const DIR_NAME_FIGURES_DEFAULT As String = "figures"
#End Region

#Region "Properties"
    ''' <summary>
    ''' Directory type associated with the attachment file.
    ''' </summary>
    ''' <remarks></remarks>
    Protected _directoryType As eAttachmentDirectoryType = eAttachmentDirectoryType.none
    Friend Overridable Property directoryType As eAttachmentDirectoryType
        Get
            Return _directoryType
        End Get
        Set(ByVal value As eAttachmentDirectoryType)
            If Not value = _directoryType Then
                _directoryType = value
                RaisePropertyChanged(Function() Me.directoryType)
                SetNewPath()
            End If
        End Set
    End Property

#End Region

#Region "Initialization"
    ''' <summary>
    ''' Depending on parameters given, sets all properties, including the reference and initializes based on an existing file. 
    ''' Priority of properties is given to the path over the model control object if it is provided.
    ''' </summary>
    ''' <param name="p_mcModel">Model control file to reference.</param>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_directoryType">Directory type to use for the attachment file.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_mcModel As cMCModel = Nothing,
                   Optional ByVal p_path As String = "",
                   Optional ByVal p_directoryType As eAttachmentDirectoryType = eAttachmentDirectoryType.none)
        _pathType = ePathType.FileAny
        If Not String.IsNullOrEmpty(p_path) Then
            SetPropertiesConsideringFileExtensions(p_path, p_mcModel)
        ElseIf (p_mcModel IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(p_mcModel.mcFile.pathDestination.path)) Then
            SetNewPath(p_mcModel)
        End If

        If p_mcModel IsNot Nothing Then SetMCModel(p_mcModel)

        directoryType = p_directoryType

        InitializeCustomHandlers()

    End Sub

    Protected Overrides Sub InitializeCustomHandlers()
        'Dim mcModelChangeListener As ChangedListener = ChangedListener.Create(_mcModel)
        'AddHandler mcModelChangeListener.PropertyChanged, AddressOf RaiseMCFolderStructureChanged
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cPathAttachment = DirectCast(MyBase.Clone(), cPathAttachment)

        With myClone
            .directoryType = directoryType

            .InitializeCustomHandlers()
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathAttachment()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathAttachment) Then Return False

        Dim comparedObject As cPathAttachment = TryCast(p_object, cPathAttachment)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not .directoryType = directoryType Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Updates relevant properties affected by the model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetMCModel(p_mcModel As cMCModel)
        MyBase.SetMCModel(p_mcModel)

        InitializeCustomHandlers()
    End Sub

    ''' <summary>
    ''' Creates a new path object for the destination associated with the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function DestinationPath(ByVal p_mcModel As cMCModel) As cPathAttachment
        Dim newDestinationPath As cPathAttachment = CType(Me.Clone, cPathAttachment)
        newDestinationPath.SetNewPath(p_mcModel)

        Return newDestinationPath
    End Function

    ''' <summary>
    ''' Changes the current path properties to update based on the state of the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <remarks></remarks>
    Friend Sub SetNewPath(Optional ByVal p_mcModel As cMCModel = Nothing)
        Try
            If (p_mcModel Is Nothing AndAlso
                _mcModel Is Nothing) Then
                Exit Sub
            ElseIf p_mcModel Is Nothing Then
                p_mcModel = _mcModel
            End If

            Dim newPath As String = p_mcModel.mcFile.pathDestination.directory
            If p_mcModel.folderStructure = eFolderStructure.Database Then
                Select Case directoryType
                    Case eAttachmentDirectoryType.attachment, eAttachmentDirectoryType.attachmentOutputSettings
                        newPath &= "\" & DIR_NAME_ATTACHMENTS_DEFAULT
                    Case eAttachmentDirectoryType.figure
                        newPath &= "\" & DIR_NAME_FIGURES_DEFAULT
                    Case eAttachmentDirectoryType.supportingFile
                        newPath &= "\" & DIR_NAME_MODELS_DEFAULT
                End Select
            Else
                If directoryType = eAttachmentDirectoryType.attachmentOutputSettings Then
                    newPath &= "\" & DIR_NAME_OUTPUTSETTINGS_FLATTENED_DEFAULT
                End If
            End If

            newPath = TrimPathSlash(newPath)
            If Not String.IsNullOrEmpty(fileNameWithExtension) Then newPath &= "\" & fileNameWithExtension

            SetPropertiesConsideringFileExtensions(newPath)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the destination path taking into account whether or not the file has an extension.
    ''' </summary>
    ''' <param name="p_path">Path to the file.</param>
    ''' <remarks></remarks>
    Private Sub SetPropertiesConsideringFileExtensions(ByVal p_path As String,
                                                       Optional ByVal p_mcModel As cMCModel = Nothing)
        If FileHasNoExtension(p_path) Then
            MyBase.SetProperties(p_path, p_mcModel, p_filenameHasExtension:=False)
        Else
            MyBase.SetProperties(p_path, p_mcModel)
        End If
    End Sub
#End Region
End Class
