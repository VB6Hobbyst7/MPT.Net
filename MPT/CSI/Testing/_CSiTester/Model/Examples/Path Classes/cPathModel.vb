Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports CSiTester.cMCModel
Imports CSiTester.cMCNameSyncer

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.Reporting

''' <summary>
''' Class representing the path to the model file.
''' </summary>
''' <remarks></remarks>
Public Class cPathModel
    Inherits cPathModelControlReference
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Constants"
    Friend Const DIR_NAME_MODELS_DEFAULT As String = "models"
#End Region

#Region "Properties"
    Private _program As eCSiProgram
    ''' <summary>
    ''' Current program associated with the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property program As eCSiProgram
        Set(value As eCSiProgram)
            If Not value = _program Then
                _program = value
                RaisePropertyChanged(NameOfProp(Function() Me.program))
                SyncFileExtension()
                SetImportTag()
            End If
        End Set
        Get
            Return _program
        End Get
    End Property

    Private _importTag As String
    ''' <summary>
    ''' Import tag currently associated with the model file.
    ''' Presence of the tag is dependent upon the current program and the imported program version associated with the model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property importTag As String
        Get
            Return _importTag
        End Get
    End Property

    Private _importedModel As Boolean
    ''' <summary>
    ''' If true, the model file is old and will undergo an import process that will change the file name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property importedModel As Boolean
        Set(value As Boolean)
            If Not _importedModel = value Then
                _importedModel = value
                RaisePropertyChanged(NameOfProp(Function() Me.importedModel))
                SetImportTag()
            End If
        End Set
        Get
            Return _importedModel
        End Get
    End Property
    Private _importedModelVersion As String
    ''' <summary>
    ''' The release version from which the imported model was saved.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property importedModelVersion As String
        Set(value As String)
            If Not value = _importedModelVersion Then
                value = _importedModelVersion
                RaisePropertyChanged(NameOfProp(Function() Me.importedModelVersion))
                SetImportTag()
            End If
        End Set
        Get
            Return _importedModelVersion
        End Get
    End Property

    ''' <summary>
    ''' States in what way the model file name is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property nameSynced As eNameSync
        Get
            If _mcModel IsNot Nothing Then
                Return _mcModel.namesSynced.modelFileNameSynced
            Else
                Return eNameSync.Custom
            End If
        End Get
        Set(ByVal value As eNameSync)
            If _mcModel IsNot Nothing Then
                _mcModel.namesSynced.modelFileNameSynced = value
            End If
        End Set
    End Property
#End Region

#Region "Event Handlers"
    Protected Sub RaiseMCNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.mcFile) Then
            SyncFileName(e)
        End If
    End Sub
    Protected Sub RaiseMCFolderStructureChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.folderStructure) Then
            SetNewPath()
        End If
    End Sub
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Depending on parameters given, sets all properties, including the reference and initializes based on an existing file. 
    ''' Priority of properties is given to the path if provided. 
    ''' Note: Providing a path reference will update the model control object referenced.
    ''' </summary>
    ''' <param name="p_bindTo">Model Control object to reference in the object.</param>
    ''' <param name="p_pathModelFile">Path to the existing model file.</param>
    ''' <param name="p_program">Program type used to set properties.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_pathModelFile As String = "",
                   Optional ByVal p_program As eCSiProgram = eCSiProgram.None)
        _pathType = ePathType.FileWithExtension
        If Not String.IsNullOrEmpty(p_pathModelFile) Then
            SetProperties(p_pathModelFile, p_bindTo)
        ElseIf (p_bindTo IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(p_bindTo.modelFile.pathDestination.path) AndAlso
            IO.File.Exists(p_bindTo.modelFile.pathDestination.path)) Then
            SetProperties(p_bindTo.modelFile.pathDestination.path, p_bindTo)
        End If

        If p_bindTo IsNot Nothing Then
            MyBase.SetMCModel(p_bindTo)
            If program = eCSiProgram.None Then program = _mcModel.targetProgram.primary
        End If
    End Sub


    Friend Overrides Function Clone() As Object
        Dim myClone As cPathModel = DirectCast(MyBase.Clone, cPathModel)

        With myClone
            ._program = _program
            ._importTag = _importTag
            .importedModel = importedModel
            .importedModelVersion = importedModelVersion
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathModel()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathModel) Then Return False

        Dim comparedObject As cPathModel = TryCast(p_object, cPathModel)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not ._importTag = importTag Then Return False
            If Not .program = program Then Return False
            If Not .importedModel = importedModel Then Return False
            If Not .importedModelVersion = importedModelVersion Then Return False
        End With

        Return True
    End Function

    Public Function BaseEquals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathModel) Then Return False
        If Not MyBase.Equals(p_object) Then Return False

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetProperties(ByVal p_path As String)
        SetProperties(p_path, p_mcModel:=Nothing)
    End Sub

    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Automatically converts relative paths based on the provided model control object.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_mcModel">Model Control that any relative path is based on.</param>
    ''' <remarks></remarks>
    Friend Overloads Overrides Sub SetProperties(ByVal p_path As String,
                                                 ByVal p_mcModel As cMCModel)
        MyBase.SetProperties(p_path, p_mcModel)
        'TODO: Set Program
    End Sub

    Friend Sub SyncFileExtension()
        'TODO: Sync file extension?
    End Sub

    ''' <summary>
    ''' Changes the current path properties to update based on the state of the model control object.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <remarks></remarks>
    Friend Sub SetNewPath(Optional ByVal p_mcModel As cMCModel = Nothing)
        If (p_mcModel Is Nothing AndAlso
            _mcModel Is Nothing) Then
            Exit Sub
        ElseIf p_mcModel Is Nothing Then
            p_mcModel = _mcModel
        End If

        Dim newPath As String = p_mcModel.mcFile.pathDestination.directory

        If p_mcModel.folderStructure = eFolderStructure.Database Then
            newPath &= "\" & DIR_NAME_MODELS_DEFAULT
        End If
        newPath = TrimPathSlash(newPath)
        If Not String.IsNullOrEmpty(fileNameWithExtension) Then newPath &= "\" & fileNameWithExtension
        MyBase.SetProperties(newPath)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the import tag expected to be attached to the model file after being opened and run in the currently selected build of a CSi product.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImportTag()
        Try
            Dim newImportTag As String = ""
            If (importedModel AndAlso
                importedModelVersion < "13.0.0") Then

                newImportTag = testerSettings.outputSettingsVersionSession
            Else
                newImportTag = ""
            End If

            If Not StringsMatch(newImportTag, importTag) Then
                _importTag = newImportTag
                RaisePropertyChanged(NameOfProp(Function() Me.importTag))
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Changes the model file name to be synced with either the id, secondary id, or model file name if the currently synced property has changed..
    ''' </summary>
    ''' <param name="e">Used to determine which changed property this corresponds to.</param>
    ''' <remarks></remarks>
    Private Sub SyncFileName(ByVal e As PropertyChangedEventArgs)
        SyncFileName(SyncFileNameCore(e))
    End Sub
    ''' <summary>
    ''' Changes the model file name to be synced with either the model control id or secondary id.
    ''' </summary>
    ''' <param name="p_syncedFileNameCore">Part of the filename before the suffix &amp; extension. If this is not provided, it is automatically set.</param>
    ''' <remarks></remarks>
    Private Sub SyncFileName(Optional ByVal p_syncedFileNameCore As String = "")
        If String.IsNullOrEmpty(p_syncedFileNameCore) Then p_syncedFileNameCore = SyncFileNameCore()
        Dim newPath As String = MyBase.directory & "\" & p_syncedFileNameCore

        SetProperties(newPath)
    End Sub

    ''' <summary>
    ''' Updates the core part of the filename before the suffix based on the referenced model control file if the currently synced property has changed.
    ''' </summary>
    ''' <param name="e">Used to determine which changed property this corresponds to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SyncFileNameCore(e As PropertyChangedEventArgs) As String
        If _mcModel Is Nothing Then Return MyBase.fileName

        Select Case nameSynced
            Case eNameSync.ModelControlID
                If e.PropertyName = NameOfProp(Function() _mcModel.ID) Then SyncFileNameCore()
            Case eNameSync.ModelControlSecondaryID
                If e.PropertyName = NameOfProp(Function() _mcModel.secondaryID) Then SyncFileNameCore()
        End Select

        Return MyBase.fileName
    End Function

    ''' <summary>
    ''' Updates the core part of the filename before the suffix based on the referenced model control file.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SyncFileNameCore() As String
        If _mcModel Is Nothing Then Return MyBase.fileName

        Select Case nameSynced
            Case eNameSync.ModelControlID
                Return _mcModel.ID.idComposite
            Case eNameSync.ModelControlSecondaryID
                If Not String.IsNullOrEmpty(_mcModel.secondaryID) Then Return _mcModel.secondaryID
        End Select

        Return MyBase.fileName
    End Function
#End Region


End Class
