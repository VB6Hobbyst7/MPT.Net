Option Explicit On
Option Strict On

Imports System.ComponentModel

Imports MPT.PropertyChanger
Imports MPT.Reporting

Imports CSiTester.cFileManager

''' <summary>
''' Base class for file objects. Contains elements for file managing.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class cMCFile
    Inherits PropertyChanger

    Implements ICloneable
    Implements ILoggerEvent

    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Variables"
    ''' <summary>
    ''' Handles the execution of the file action and maintains prior path data.
    ''' </summary>
    ''' <remarks></remarks>
    Protected WithEvents _fileManager As New cFileManager
#End Region

#Region "Properties"
    Protected _fileAction As eFileAction
    ''' <summary>
    ''' Specifies the action to be taken with the file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileAction As eFileAction
        Set(ByVal value As eFileAction)
            If Not _fileAction = value Then
                _fileAction = value
                _fileManager.action = _fileAction
                RaisePropertyChanged(Function() Me.fileAction)
            End If
        End Set
        Get
            Return _fileAction
        End Get
    End Property

    Protected WithEvents _pathDestination As New cPathModelControlReference
    ''' <summary>
    ''' The file object representing the file destination. 
    ''' Typically upcast from a derived class, such as cPathModelControlFile.
    ''' This can be any path-derived object type upon initialiation, but after that, only the current object type may be assigned.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Property pathDestination As cPathModelControlReference
        Get
            Return _pathDestination
        End Get
        Set(ByVal value As cPathModelControlReference)
            If (isMatchingPathType(value) AndAlso
                 Not _pathDestination.Equals(value)) Then
                _pathDestination = value
                RaisePropertyChanged(Function() Me.pathDestination)

                ' Keep the rest of the class and file manager in sync.
                _fileManager.SetDestinationPath(_pathDestination)
            End If
        End Set
    End Property

    ''' <summary>
    ''' A copy of the file object representing the file source. 
    ''' If blank, then the associated file does not yet exist.
    ''' Typically upcast from a derived class, such as cPathModelControlFile.
    ''' This can be any path-derived object type upon initialiation, but after that, only the current object type may be assigned.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property pathSource As cPath
        Get
            Return CType(_fileManager.fileSource.Clone, cPath)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Initializes the object as a file type specified with a reference set to the provided model control object.
    ''' </summary>
    ''' <param name="p_pathFile">Path to the existing file associated with the specified file type.</param>
    ''' <param name="p_bindTo">Model control object to bind to.</param>
    ''' <remarks></remarks>
    Friend Sub New(Optional ByVal p_pathFile As String = "",
                   Optional ByVal p_bindTo As cMCModel = Nothing)
        InitializeFile(p_bindTo, p_pathFile)
        CompleteInitialization()
    End Sub

    ''' <summary>
    ''' Initializes the object as a file type specified with a reference set to the provided model control object.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to bind to.</param>
    ''' <param name="p_pathFile">Path to the existing file associated with the specified file type.</param>
    ''' <remarks></remarks>
    Protected MustOverride Sub InitializeFile(Optional ByVal p_bindTo As cMCModel = Nothing,
                                                Optional ByVal p_pathFile As String = "")

    ''' <summary>
    ''' Final common operations of initializing the object with any effects on path and/or file type.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CompleteInitialization()
        RaisePropertyChanged(Function() Me.pathDestination)

        ' Initializes file manager with the path source the same as the path destination.
        _fileManager = New cFileManager(_pathDestination)
    End Sub

 
    Protected MustOverride Function Create() As cMCFile
#End Region

#Region "Methods: Overloads/Overrides/Implements"
    Friend Overridable Overloads Function Clone() As Object Implements System.ICloneable.Clone
        Return Clone(Nothing)
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">If specified, the model control reference will be switched to the one provided. 
    ''' Otherwise, the original reference is kept.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overridable Overloads Function Clone(ByVal p_bindTo As cMCModel) As Object
        Dim myClone As cMCFile = Create()
        Try
            With myClone
                ._pathDestination = CType(_pathDestination.Clone(p_bindTo), cPathModelControlReference)
                ._fileManager = CType(_fileManager.Clone(p_bindTo:=_pathDestination), cFileManager)
                .fileAction = fileAction
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCFile) Then Return False

        Dim comparedObject As cMCFile = TryCast(p_object, cMCFile)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not ._fileManager.Equals(_fileManager) Then Return False
            If Not ._pathDestination.Equals(_pathDestination) Then Return False
            If Not .fileAction = fileAction Then Return False
        End With

        Return True
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString() & " - " & pathSource.fileNameWithExtension & " - S - " & pathSource.directory & " - D - " & pathDestination.directory
    End Function
#End Region

#Region "Methods: Friend"
    'Binding After Initialization
    ''' <summary>
    ''' Binds the object to the state of the supplied model control object.
    ''' </summary>
    ''' <param name="p_bindTo">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend MustOverride Sub Bind(ByVal p_bindTo As cMCModel)

    'File Actions
    ''' <summary>
    ''' Copies the corresponding file to the correct locations relative to the model file, based on the directory structure chosen. 
    ''' Returns true if successful.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function FileCopy() As Boolean
        If _fileManager.UpdateSourceFiles(pathDestination, cFileManager.eFileAction.copySourceToDestination) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Renames the corresponding file to the correct locations relative to the model file, based on the directory structure chosen. 
    ''' Returns true if successful.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function FileRename() As Boolean
        If _fileManager.UpdateSourceFiles(pathDestination, cFileManager.eFileAction.renameSourceFromDestination) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Moves the corresponding file to the correct locations relative to the model file, based on the directory structure chosen. 
    ''' The source file will be deleted if the copy action is successful.
    ''' Returns true if successful.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function FileMove() As Boolean
        If _fileManager.UpdateSourceFiles(pathDestination, cFileManager.eFileAction.moveSourceToDestination) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Deletes the corresponding file at the correct locations relative to the model file, based on the directory structure chosen. 
    ''' Returns true if successful.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function FileDelete() As Boolean
        If _fileManager.UpdateSourceFiles(pathDestination, cFileManager.eFileAction.deleteSource) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Sets the destination path to the provided directory, maintaining the current file name.
    ''' </summary>
    ''' <param name="p_pathDirectory">Path to the target directory.</param>
    ''' <remarks></remarks>
    Friend Sub SetDestination(ByVal p_pathDirectory As String)
        pathDestination.SetProperties(p_pathDirectory & "\" & pathSource.fileNameWithExtension)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Determines if the path object provided is of a matching type.
    ''' </summary>
    ''' <param name="p_path">Path object to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected MustOverride Function isMatchingPathType(ByVal p_path As cPath) As Boolean
#End Region

    Protected Overridable Sub OnLogger(e As LoggerEventArgs)
        RaiseEvent Log(e)
    End Sub

End Class
