Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting

''' <summary>
''' Contains relevant properties and methods for copying a file from one location to another.
''' </summary>
''' <remarks></remarks>
Public Class cFileManager
    Inherits PropertyChanger
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Enumerations"
    Public Enum eFileAction
        <Description("None")> none = 0
        <Description("Rename File")> renameSourceFromDestination
        <Description("Copy File")> copySourceToDestination
        <Description("Move File")> moveSourceToDestination
        <Description("Delete File")> deleteSource
    End Enum
#End Region

#Region "Properties"
    Private WithEvents _fileSource As New cPath
    ''' <summary>
    ''' Path to the location of the original file. 
    ''' Can only be set upon initialization, or set to the destination after any file operation.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property fileSource As cPath
        Get
            Return _fileSource
        End Get
    End Property

    Private WithEvents _fileDestination As New cPath
    ''' <summary>
    ''' Path to the location of the new file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property fileDestination As cPath
        Get
            Return _fileDestination
        End Get
    End Property

    Private _action As eFileAction
    ''' <summary>
    ''' Specifies the action to be taken with the source file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property action As eFileAction
        Set(ByVal value As eFileAction)
            If Not _action = value Then
                _action = value
                RaisePropertyChanged(Function() Me.action)
            End If
        End Set
        Get
            Return _action
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Initializes the object with the path to an existing file.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_path As cPath)
        SetFileSource(CType(p_path.CloneStatic, cPath))
        SetFileDestination(p_path)
    End Sub

    Friend Overloads Function Clone() As Object Implements System.ICloneable.Clone
        Return Clone()
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_bindTo">Path destination object to set a new reference to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function Clone(Optional ByVal p_bindTo As cPathModelControlReference = Nothing) As Object
        Dim myClone As New cFileManager

        Try
            With myClone
                ._fileSource = DirectCast(fileSource.CloneStatic, cPath)
                If p_bindTo Is Nothing Then
                    ._fileDestination = DirectCast(fileDestination.Clone, cPath)
                Else
                    ._fileDestination = p_bindTo
                End If
                .action = action
            End With
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            myClone = New cFileManager
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
        If Not (TypeOf p_object Is cFileManager) Then Return False

        Dim comparedObject As cFileManager = TryCast(p_object, cFileManager)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            Dim pathComparer As New cPathComparer

            If Not pathComparer.Compare(fileSource, .fileSource) = 0 Then Return False
            If Not pathComparer.Compare(fileDestination, .fileDestination) = 0 Then Return False
            If Not .action = action Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Updates the file destination path object.
    ''' </summary>
    ''' <param name="p_destination">Path object to replace the current destination with.</param>
    ''' <remarks></remarks>
    Friend Sub SetDestinationPath(ByVal p_destination As cPath)
        _fileDestination = p_destination
    End Sub

    ''' <summary>
    ''' Performs the currently set action to the source file.
    ''' </summary>
    ''' <returns>'True' if it can be confirmed that the operation was successful, or if no operation was needed to be performed.</returns>
    ''' <remarks></remarks>
    Friend Overloads Function UpdateSourceFiles() As Boolean
        Dim success As Boolean = False

        If action = eFileAction.deleteSource Then
            success = DeleteFile(fileSource.path, p_includeReadOnly:=True)
            If success Then
                ' Clear both paths
                _fileSource = New cPath()
                RaisePropertyChanged(Function() Me.fileSource)

                _fileDestination = New cPath()
                RaisePropertyChanged(Function() Me.fileDestination)
            End If
        ElseIf Not StringsMatch(fileSource.path, fileDestination.path) Then
            Select Case action
                Case eFileAction.copySourceToDestination
                    If StringsMatch(fileSource.path, fileDestination.path) Then
                        success = True
                    Else
                        success = CopyFile(fileSource.path, fileDestination.path, p_overWriteFile:=True, p_includeReadOnly:=True)
                    End If
                Case eFileAction.renameSourceFromDestination
                    RenameFile(fileSource.path, fileDestination.fileNameWithExtension)
                    success = True
                Case eFileAction.moveSourceToDestination
                    If StringsMatch(fileSource.path, fileDestination.path) Then
                        success = True
                    Else
                        success = MoveFile(fileSource.path, fileDestination.path, p_deleteOriginal:=True)
                    End If
            End Select

            If success Then
                ' Update old path to the new one
                _fileSource = DirectCast(fileDestination.CloneStatic, cPath)
                RaisePropertyChanged(Function() Me.fileSource)
            End If
        Else
            success = True
        End If

        If success Then action = eFileAction.none

        Return success
    End Function
    ''' <summary>
    ''' Performs the currently set action to the source file based on the provided destination object.
    ''' </summary>
    ''' <param name="p_fileDestination">Path destination object to use for the operation.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function UpdateSourceFiles(ByVal p_fileDestination As cPath) As Boolean
        _fileDestination = p_fileDestination
        Return UpdateSourceFiles()
    End Function
    ''' <summary>
    ''' Performs the specified file action to the source file based on the provided destination object.
    ''' </summary>
    ''' <param name="p_fileDestination">Path destination object to use for the operation.</param>
    ''' <param name="p_fileAction">File action to perform on the source file.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function UpdateSourceFiles(ByVal p_fileDestination As cPath,
                                                ByVal p_fileAction As eFileAction) As Boolean
        action = p_fileAction
        _fileDestination = p_fileDestination
        Return UpdateSourceFiles()
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets the file source object.
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Private Sub SetFileSource(ByVal p_path As cPath)
        Try
            If Not _fileSource.Equals(p_path) Then
                _fileSource = CType(p_path.CloneStatic, cPath)
                RaisePropertyChanged(Function() Me.fileSource)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Sets the file destination object,
    ''' </summary>
    ''' <param name="p_path"></param>
    ''' <remarks></remarks>
    Private Sub SetFileDestination(ByVal p_path As cPath)
        Try
            If Not _fileDestination.Equals(p_path) Then
                _fileDestination = p_path
                RaisePropertyChanged(Function() Me.fileDestination)
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region
End Class
