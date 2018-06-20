Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reflections.ReflectionLibrary

''' <summary>
''' Class representing a path to a model control file that is referenced by other components of the example.
''' </summary>
''' <remarks></remarks>
Public Class cPathModelControlReference
    Inherits cPath
    Implements ICloneable

#Region "Variables"
    Private mcPathChangingListener As ChangingListener
    Private mcPathChangedListener As ChangedListener

    ''' <summary>
    ''' Old model control file directory.
    ''' Used as temporary storage of tracking the changing of this property.
    ''' </summary>
    ''' <remarks></remarks>
    Private _oldMCDirectory As String

    Protected WithEvents _mcModel As cMCModel
#End Region

#Region "Event Handlers"

    Protected Overridable Sub RaiseMCPathChanging(sender As Object, e As PropertyChangingEventArgs)
        Dim currentProperty As String = GetSuffix(e.PropertyName, ".")

        If (currentProperty = NameOfProp(Function() _mcModel.mcFile.pathDestination.path) OrElse
            currentProperty = NameOfProp(Function() _mcModel.mcFile.pathDestination.directory)) Then
            _oldMCDirectory = _mcModel.mcFile.pathDestination.directory
        End If
    End Sub

    Protected Overridable Sub RaiseMCPathChanged(sender As Object, e As PropertyChangedEventArgs)
        Dim currentProperty As String = GetSuffix(e.PropertyName, ".")

        'Update current part of directory that is shared with the model control file
        If (Not String.IsNullOrEmpty(_oldMCDirectory) AndAlso
            (currentProperty = NameOfProp(Function() _mcModel.mcFile.pathDestination.path) OrElse
             currentProperty = NameOfProp(Function() _mcModel.mcFile.pathDestination.directory))) Then

            Dim newPath As String = FilterStringFromName(path, _oldMCDirectory, p_retainPrefix:=False, p_retainSuffix:=True)
            _oldMCDirectory = ""

            If (Not StringsMatch(newPath, path) AndAlso
                Not String.IsNullOrEmpty(newPath) AndAlso
                Not newPath.Contains(":")) Then

                newPath = _mcModel.mcFile.pathDestination.directory & newPath

                If Not String.IsNullOrEmpty(newPath) Then MyBase.SetProperties(newPath)
            End If
        End If
    End Sub

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
            Return GetAbsolutePath(_path, _mcModel)
        End Get
    End Property

    ''' <summary>
    ''' File path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathRelativeReference">The directory/file location to which the path is to be relative.
    ''' If not provided, the referenced model control file destination path will be used.
    ''' If there is no model control reference, there will be no reference path.</param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property pathRelative(Optional ByVal p_pathRelativeReference As String = "") As String
        Get
            Return GetRelativePath(p_pathRelativeReference)
        End Get
    End Property

    ''' <summary>
    ''' Path by which any relative path is based on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property pathRelativeReference As String
        Get
            If _mcModel IsNot Nothing Then
                Return _mcModel.mcFile.pathDestination.path
            Else
                Return ""
            End If
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Sets properties based on the path provided.
    ''' </summary>
    ''' <param name="p_path">Path to use for setting class properties.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_path As String)
        _pathType = ePathType.FileWithExtension
        MyBase.SetProperties(p_path)
    End Sub
    ''' <summary>
    ''' Sets all properties and references based.
    ''' </summary>
    ''' <param name="p_bindTo"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_bindTo As cMCModel)
        _pathType = ePathType.FileWithExtension
        SetProperties(p_bindTo.mcFile.pathDestination.path, p_bindTo)
        SetMCModel(p_bindTo)
    End Sub

    ''' <summary>
    ''' Create an event handler to catch when the model control file path changes in any way, and pass its arguments to another method.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub InitializeCustomHandlers()
        If _mcModel Is Nothing Then Exit Sub

        mcPathChangingListener = ChangingListener.Create(_mcModel.mcFile)
        If Not mcPathChangingListener Is Nothing Then
            AddHandler mcPathChangingListener.PropertyChanging, AddressOf RaiseMCPathChanging
        End If

        mcPathChangedListener = ChangedListener.Create(_mcModel.mcFile)
        If Not mcPathChangedListener Is Nothing Then
            AddHandler mcPathChangedListener.PropertyChanged, AddressOf RaiseMCPathChanged
        End If
    End Sub

    ''' <summary>
    ''' Remove the handlers to avoid unwanted listening.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overridable Sub RemoveCustomHandlers()
        If Not mcPathChangingListener Is Nothing Then
            RemoveHandler mcPathChangingListener.PropertyChanging, AddressOf RaiseMCPathChanging
            mcPathChangingListener = Nothing
        End If

        If Not mcPathChangedListener Is Nothing Then
            RemoveHandler mcPathChangedListener.PropertyChanged, AddressOf RaiseMCPathChanged
            mcPathChangedListener = Nothing
        End If
    End Sub

    Friend Overrides Function Clone() As Object
        Return Clone(Nothing)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">If specified, the model control reference will be switched to the one provided. 
    ''' Otherwise, the original reference is kept.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overridable Overloads Function Clone(Optional ByVal p_bindTo As cMCModel = Nothing) As Object
        Dim myClone As cPathModelControlReference = DirectCast(MyBase.Clone, cPathModelControlReference)

        With myClone
            ' Passing reference instead of cloning to maintain reference. DO NOT make a copy!
            If p_bindTo Is Nothing Then
                ' Maintain current reference
                ._mcModel = _mcModel
            Else
                ' Shift reference to newly specified model control object.
                ._mcModel = p_bindTo
            End If

            ' Maintain event handlers if they have been attached.
            If (Not mcPathChangedListener Is Nothing AndAlso
                Not mcPathChangingListener Is Nothing) Then .InitializeCustomHandlers()
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cPath
        Return New cPathModelControlReference()
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cPathModelControlReference) Then Return False

        Dim comparedObject As cPathModelControlReference = TryCast(p_object, cPathModelControlReference)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False

            If (_mcModel IsNot Nothing AndAlso
                ._mcModel IsNot Nothing) Then
                If Not ._mcModel.ID.idCompositeDecimal.Equals(_mcModel.ID.idCompositeDecimal) Then Return False
            End If
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Automatically converts relative paths based on the provided model control object.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_mcModel">Model Control that any relative path is based on.</param>
    ''' <remarks></remarks>
    Friend Overridable Overloads Sub SetProperties(ByVal p_path As String,
                                                   ByVal p_mcModel As cMCModel)
        MyBase.SetProperties(GetAbsolutePath(p_path, p_mcModel))
        If _mcModel Is Nothing Then SetMCModel(p_mcModel)
    End Sub

    ''' <summary>
    ''' Sets the class properties using the provided path.
    ''' Path is converted to absolute if it is relative.
    ''' </summary>
    ''' <param name="p_path">Path to be used.</param>
    ''' <param name="p_mcModel">Model Control that any relative path is based on.</param>
    ''' <param name="p_filenameHasExtension">False: Path contains a filename that has no extension.</param>
    ''' <remarks></remarks>
    Friend Overridable Overloads Sub SetProperties(ByVal p_path As String,
                                                   ByVal p_mcModel As cMCModel,
                                                   Optional p_filenameHasExtension As Boolean = True)
        MyBase.SetProperties(GetAbsolutePath(p_path, p_mcModel), p_filenameHasExtension)
        If _mcModel Is Nothing Then SetMCModel(p_mcModel)
    End Sub

    ''' <summary>
    ''' Sets the reference to the supplied Model Contol object.
    ''' </summary>
    ''' <param name="p_mcModel">Model object to bind to the class.</param>
    ''' <param name="p_addListeners">True: Custom event handlers will subscribe to changes in the bound model object.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub SetMCModel(ByVal p_mcModel As cMCModel,
                                    Optional ByVal p_addListeners As Boolean = True)
        If p_mcModel Is Nothing Then Exit Sub

        _mcModel = p_mcModel

        If p_addListeners Then InitializeCustomHandlers()
    End Sub
    ''' <summary>
    ''' Retuns the Model Control ID of the referenced Model Control object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetMCModelID() As String
        Return _mcModel.ID.idComposite
    End Function
    ''' <summary>
    ''' Retuns the Model Control file path of the referenced Model Control object.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetMCModelPath() As String
        Return _mcModel.mcFile.pathDestination.path
    End Function
#End Region

#Region "Methods: Protected"
    ''' <summary>
    ''' Returns an absolute path version of the path provided.
    ''' If the path is already absolute, it is returned as-is.
    ''' </summary>
    ''' <param name="p_path">File path to return as an absolute path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function GetAbsolutePath(ByVal p_path As String) As String
        Dim pathReference As String = pathRelativeReference

        Return MyBase.GetAbsolutePath(p_path, pathReference)
    End Function
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
        If p_mcModel IsNot Nothing Then
            pathReference = p_mcModel.mcFile.pathDestination.path
        Else
            pathReference = ""
        End If

        Return MyBase.GetAbsolutePath(p_path, pathReference)
    End Function

    ''' <summary>
    ''' Return the file path as a relative path to another directory location.
    ''' </summary>
    ''' <param name="p_pathReference">The directory/file location to which the path is to be relative.
    ''' If not provided, the referenced model control file destination path will be used.
    ''' If there is no model control reference, there will be no reference path.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overloads Function GetRelativePath(ByVal p_pathReference As String) As String
        If String.IsNullOrEmpty(p_pathReference) Then
            If _mcModel Is Nothing Then Return ""
            p_pathReference = IO.Path.GetDirectoryName(pathRelativeReference)
        End If
        Return MyBase.GetRelativePath(p_pathReference)
    End Function
#End Region


End Class
