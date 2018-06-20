Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports CSiTester.cMCNameSyncer
Imports CSiTester.cMCModel
Imports CSiTester.cPathModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reflections.ReflectionLibrary
Imports MPT.XML.ReaderWriter

''' <summary>
''' Class representing the path to the model control file.
''' </summary>
''' <remarks></remarks>
Public Class cPathModelControl
    Inherits cPathModelControlReference
    Implements ICloneable

#Region "Event Handlers"
    Protected Sub RaiseMCFolderStructureChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.folderStructure) Then
            SetNewPath()
        End If
    End Sub

    Protected Sub RaiseMCNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If (e.PropertyName = NameOfProp(Function() _mcModel.mcFile) OrElse
            e.PropertyName = NameOfProp(Function() _mcModel.secondaryID) OrElse
            e.PropertyName = NameOfProp(Function() _mcModel.ID)) Then
            SyncFileName(e)
        End If
    End Sub

    'Protected Overrides Sub RaiseMCPathChanging(sender As Object, e As PropertyChangingEventArgs) Handles _mcModel.PropertyChanging
    '    ' No action. This is overriding an action taken with this object in order to avoid an infinite loop.
    'End Sub

    'Protected Overrides Sub RaiseMCPathChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
    '    ' No action. This is overriding an action taken with this object in order to avoid an infinite loop.
    'End Sub
#End Region

#Region "Constants"
    Private Const _PATHNODE As String = "//n:model"

    Friend Const FILENAME_SEED_XML As String = "seed model.xml"
    Friend Const FILE_NAME_SUFFIX_MC_XML As String = "_MC.xml"
#End Region

#Region "Properties"
    ''' <summary>
    ''' File path as an absolute path to another directory location.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Overrides ReadOnly Property path() As String
        Get
            Return GetAbsolutePath(_path, "")
        End Get
    End Property

    ''' <summary>
    ''' Path by which any relative path is based on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property pathRelativeReference As String
        Get
            Return ""
        End Get
    End Property

    ''' <summary>
    ''' Path to the location of the seed model control XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Property seedPathMC As String = testerSettings.seedDirectory.path & "\" & FILENAME_SEED_XML

    Private _isValidModelControlFile As Boolean = False
    ''' <summary>
    ''' True if the path points to an existing file that is a valid model control file..
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property isValidModelControlFile As Boolean
        Get
            Return _isValidModelControlFile
        End Get
    End Property

    Private _nameSynced As eNameSync = eNameSync.enumError
    ''' <summary>
    ''' States in what way the model control XML file name is synced with other properties or files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property nameSynced As eNameSync
        Set(ByVal value As eNameSync)
            _nameSynced = value
            If _mcModel IsNot Nothing Then
                _mcModel.namesSynced.mcFileNameSynced = value
            End If
        End Set
        Get
            If _mcModel IsNot Nothing Then
                Return _mcModel.namesSynced.mcFileNameSynced
            ElseIf _nameSynced = eNameSync.enumError Then
                _nameSynced = eNameSync.ModelControlSecondaryID
                Return _nameSynced
            Else
                Return _nameSynced
            End If
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary> 
    ''' Depending on parameters given, sets all properties, including the reference and initializes based on an existing file. 
    ''' Priority of properties is given to the model control file reference if it is provided.
    ''' </summary>
    ''' <param name="p_bindTo">Model control file to reference.</param>
    ''' <param name="p_path">Path to be used.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_bindTo As cMCModel = Nothing,
                   Optional ByVal p_path As String = "")
        _pathType = ePathType.FileWithExtension
        If (p_bindTo IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(p_bindTo.mcFile.pathDestination.path) AndAlso
            Not StringsMatch(p_bindTo.mcFile.pathDestination.path, seedPathMC)) Then

            SetProperties(p_bindTo.mcFile.pathDestination.path)
        ElseIf Not String.IsNullOrEmpty(p_path) Then
            SetProperties(p_path)
        Else
            SetProperties(seedPathMC)
        End If

        If p_bindTo IsNot Nothing Then SetMCModel(p_bindTo)

        InitializeCustomHandlers()
    End Sub

    Protected Overrides Sub InitializeCustomHandlers()
        '' Create an event handler to catch when the model control file path changes in any way, and pass its arguments to another method.
        'Dim mcNameChangeListener As ChangedListener = ChangedListener.Create(_mcModel)
        'AddHandler mcNameChangeListener.PropertyChanged, AddressOf RaiseMCNameChanged
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As cPathModelControl = DirectCast(MyBase.Clone, cPathModelControl)

        With myClone
            ._isValidModelControlFile = _isValidModelControlFile
            .nameSynced = nameSynced

            .InitializeCustomHandlers()
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathModelControl()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathModelControl) Then Return False

        Dim comparedObject As cPathModelControl = TryCast(p_object, cPathModelControl)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not ._isValidModelControlFile = _isValidModelControlFile Then Return False
            If Not .nameSynced = nameSynced Then Return False
        End With

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
        SetIsValidModelControlFile(_path)
    End Sub

    ''' <summary>
    ''' Sets the reference to the supplied Model Contol object.
    ''' </summary>
    ''' <param name="p_mcModel"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetMCModel(ByVal p_mcModel As cMCModel)
        MyBase.SetMCModel(p_mcModel, p_addListeners:=False)

        InitializeCustomHandlers()
    End Sub

    ''' <summary>
    ''' Sets the name and path of the model control file to be created based on the path to the model file associated with the model control object, as well as other associated properties.
    ''' </summary>
    ''' <param name="p_appendMCExtension">True: The standard model control file extension will be added to the file name.</param>
    ''' <remarks></remarks>
    Friend Sub SyncDestinationPathWithModelFile(Optional ByVal p_appendMCExtension As Boolean = True)
        If _mcModel Is Nothing Then Exit Sub

        ' Set Model Control XML file name
        Dim newXMLName As String = SyncFileNameCore()

        If p_appendMCExtension Then newXMLName &= cPathModelControl.FILE_NAME_SUFFIX_MC_XML

        'Set file path
        Dim pathDirectory As String = NewDirectoryPathSynced()
        If Not String.IsNullOrEmpty(pathDirectory) Then SetProperties(pathDirectory & "\" & newXMLName)
    End Sub

    ''' <summary>
    ''' Changes the current path properties to update based on the state of the model control object.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNewPath()
        If _mcModel Is Nothing Then Exit Sub

        Dim newPath As String = NewDirectoryPathSynced()

        If Not String.IsNullOrEmpty(newPath) Then
            If (Not String.IsNullOrEmpty(fileNameWithExtension)) Then newPath &= "\" & fileNameWithExtension
            MyBase.SetProperties(newPath)
        End If
    End Sub

    'TODO: Ondrej has a method. Maybe check schema. Write initialization file to speed up, as this is slow to do every time.
    ''' <summary>
    ''' Confirms that the XML file is a Model XMl file
    ''' </summary>
    ''' <param name="p_path">Path to the XML file</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    Friend Shared Function IsModelControlXML(ByVal p_path As String) As Boolean
        'Check that the file type is valid
        If Not IO.File.Exists(p_path) Then Return False
        If Not StringsMatch(GetSuffix(p_path, "."), "XML") Then Return False

        Dim pathNodeAttribute As String = "xmlns"
        Dim nodeValue As String = ""

        Dim xmlReader As New cXmlReadWrite
        xmlReader.GetSingleXMLNodeValue(p_path, _PATHNODE, nodeValue, pathNodeAttribute)

        If StringsMatch(nodeValue, "http://www.csiberkeley.com") Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Protected"
    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="p_path">File path to return as an absolute path.</param>
    ''' <param name="p_mcModel">Model control reference that a possible relative path would be based on.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function GetAbsolutePath(ByVal p_path As String,
                                                 ByVal p_mcModel As cMCModel) As String
        Dim pathReference As String = ""
        If p_mcModel IsNot Nothing Then pathReference = p_mcModel.mcFile.pathDestination.path

        Return MyBase.GetAbsolutePath(p_path, pathReference)
    End Function

#End Region

#Region "Methods: Private"

    ''' <summary>
    ''' Returns the path to the directoy that should contain the model control file, synced based on the folder structure.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NewDirectoryPathSynced() As String
        Dim newPath As String = _mcModel.modelFile.pathDestination.directory

        If _mcModel.folderStructure = eFolderStructure.Database Then
            newPath = PathNotInModelsFolder(newPath)
            newPath = PathInSyncedFolder(newPath)
        ElseIf _mcModel.folderStructure = eFolderStructure.Flattened Then
            newPath = PathNotInDataBaseParentFolder(newPath)
        Else
            newPath = _mcModel.mcFile.pathDestination.directory
        End If
        newPath = TrimPathSlash(newPath)

        Return newPath
    End Function

    Private Sub SetIsValidModelControlFile(ByVal p_path As String)
        If cPathModelControl.IsModelControlXML(p_path) Then
            _isValidModelControlFile = True
        Else
            _isValidModelControlFile = False
        End If
        RaisePropertyChanged(Function() Me.isValidModelControlFile)
    End Sub

    ' Sync File Name
    ''' <summary>
    ''' Changes the model control file name to be synced with either the id, secondary id, or model file name if the currently synced property has changed..
    ''' </summary>
    ''' <param name="e">Used to determine which changed property this corresponds to.</param>
    ''' <remarks></remarks>
    Private Sub SyncFileName(ByVal e As PropertyChangedEventArgs)
        SyncFileName(SyncFileNameCore(e))
    End Sub
    ''' <summary>
    ''' Changes the model control file name to be synced with either the id, secondary id, or model file name.
    ''' </summary>
    ''' <param name="p_syncedFileNameCore">Part of the filename before the suffix &amp; extension. If this is not provided, it is automatically set.</param>
    ''' <remarks></remarks>
    Private Sub SyncFileName(Optional ByVal p_syncedFileNameCore As String = "")
        If String.IsNullOrEmpty(p_syncedFileNameCore) Then p_syncedFileNameCore = SyncFileNameCore()
        Dim newFileName As String = p_syncedFileNameCore & SyncFileNameSuffixAndExtension()
        Dim newPath As String = MyBase.directory & "\" & newFileName

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
                If e.PropertyName = NameOfProp(Function() _mcModel.ID) Then Return SyncFileNameCore()
            Case eNameSync.ModelControlSecondaryID
                If e.PropertyName = NameOfProp(Function() _mcModel.secondaryID) Then Return SyncFileNameCore()
            Case eNameSync.ModelFileName
                If e.PropertyName = NameOfProp(Function() _mcModel.modelFile.pathDestination.fileName) Then Return SyncFileNameCore()
            Case eNameSync.Custom
                'Do not change name
        End Select

        Return MyBase.fileName
    End Function

    ''' <summary>
    ''' Returns the core part of the filename before the suffix based on the referenced model control file.
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
            Case eNameSync.ModelFileName
                Return _mcModel.modelFile.pathDestination.fileName
            Case eNameSync.AddExtension
                'Do not change core name
            Case eNameSync.Custom
                'Do not change name
        End Select

        Return MyBase.fileName
    End Function

    ''' <summary>
    ''' Adds any suffix to the filename as well as the file extension.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SyncFileNameSuffixAndExtension() As String
        'Maintain name suffix or add new one if specified
        If (StringExistInName(MyBase.fileNameWithExtension, FILE_NAME_SUFFIX_MC_XML) OrElse
            nameSynced = eNameSync.AddExtension) Then

            'Adding extension is reset. User must specify a different syncing if they want anything beyond the current name
            If nameSynced = eNameSync.AddExtension Then nameSynced = eNameSync.Custom

            Return FILE_NAME_SUFFIX_MC_XML
        Else
            Return ".xml"
        End If
    End Function

    ' Sync directory path
    ''' <summary>
    ''' Remove model control parent folder name if it is the same as the model folder.
    ''' Path taken as one directory higher.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathNotInModelsFolder(ByVal p_path As String) As String
        If StringsMatch(GetSuffix(p_path, "\"), DIR_NAME_MODELS_DEFAULT) Then
            p_path = GetPathDirectoryStub(p_path)
        End If
        Return p_path
    End Function

    ''' <summary>
    ''' Set model control parent folder name if it is not in sync.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathInSyncedFolder(ByVal p_path As String) As String
        If _mcModel Is Nothing Then Return p_path

        Dim expectedParentDirectoryName As String = GetParentDirectoryName()
        If PathParentDirectoryDifferent(p_path, expectedParentDirectoryName) Then
            If PathParentDirectoryIsSecondaryID(p_path, expectedParentDirectoryName) Then
                p_path = GetPathDirectoryStub(p_path)
            End If

            p_path &= "\" & expectedParentDirectoryName
        End If
        Return p_path
    End Function

    Private Function PathParentDirectoryDifferent(ByVal p_path As String,
                                                  ByVal p_expectedParentDirectoryName As String) As Boolean
        Return Not StringsMatch(GetSuffix(p_path, "\"), p_expectedParentDirectoryName)
    End Function

    Private Function PathParentDirectoryIsSecondaryID(ByVal p_path As String,
                                                      ByVal p_expectedParentDirectoryName As String) As Boolean
        Return (StringsMatch(GetSuffix(p_path, "\"), _mcModel.secondaryID) AndAlso
                Not StringsMatch(p_expectedParentDirectoryName, _mcModel.secondaryID))
    End Function

    ''' <summary>
    ''' Remove parent folder name if it is the same as the database parent folder name.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PathNotInDataBaseParentFolder(ByVal p_path As String) As String
        If _mcModel Is Nothing Then Return p_path

        If StringsMatch(GetSuffix(p_path, "\"), _mcModel.secondaryID) Then
            p_path = GetPathDirectoryStub(p_path)
        End If
        Return p_path
    End Function

    ''' <summary>
    ''' Returns the expected name of the parent directory in a database folder structure based on the multi-model type and the secondary ID syncing.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetParentDirectoryName() As String
        If _mcModel.ID.multiModelType = cMCModelID.eMultiModelType.singleModel Then
            Return _mcModel.secondaryID
        Else
            Select Case _mcModel.namesSynced.mcSecondaryIDSynced
                Case eNameSync.ModelControlID
                    Return _mcModel.ID.idExampleLabel
                Case eNameSync.ModelFileName
                    Return _mcModel.ID.exampleName
                Case eNameSync.Custom
                    Return _mcModel.ID.exampleName
                Case Else
                    Return _mcModel.ID.exampleName
            End Select
        End If
    End Function
#End Region

End Class
