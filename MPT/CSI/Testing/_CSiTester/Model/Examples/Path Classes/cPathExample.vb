Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.PropertyChanger

''' <summary>
''' Class representing a path to an example comprising a set of models.
''' </summary>
''' <remarks></remarks>
Public Class cPathExample
    Inherits cPathModelControlReference

#Region "Event Handlers"
    Protected Sub RaiseModelNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.modelFile) Then
            RaisePropertyChanged("fileName")
        End If
    End Sub
    Protected Sub RaiseTableNameChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.dataSource) Then
            SetDataSourceName(_mcModel.dataSource.pathDestination.fileNameWithExtension)
            RaisePropertyChanged("dataSource")
        End If
    End Sub
    Protected Sub RaiseModelIDChanged(sender As Object, e As PropertyChangedEventArgs) Handles _mcModel.PropertyChanged
        If e.PropertyName = NameOfProp(Function() _mcModel.ID) Then
            RaisePropertyChanged("modelID")
        End If
    End Sub
#End Region

#Region "Properties"
    Private _exampleStatus As String
    ''' <summary>
    ''' Status of the example associated with the corresponding file names and paths.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property exampleStatus As String
        Set(ByVal value As String)
            If Not _exampleStatus = value Then
                _exampleStatus = value
                RaisePropertyChanged("exampleStatus")
            End If
        End Set
        Get
            Return _exampleStatus
        End Get
    End Property

    Private _fileNameUse As Boolean
    ''' <summary>
    ''' If true, indicates that the example path is to be used for various operations.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fileNameUse As Boolean
        Set(ByVal value As Boolean)
            If Not _fileNameUse = value Then
                _fileNameUse = value
                RaisePropertyChanged("fileNameUse")
            End If
        End Set
        Get
            Return _fileNameUse
        End Get
    End Property

    Private _dataSource As New cPathDataSource
    ''' <summary>
    ''' Path to the database file used to establish results queries for examples.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dataSource As cPathDataSource
        Set(ByVal value As cPathDataSource)
            _dataSource = value
            RaisePropertyChanged("dataSource")
        End Set
        Get
            Return _dataSource
        End Get
    End Property

    ''' <summary>
    ''' Composite ID associated with the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ID As Decimal
        Get
            If _mcModel Is Nothing Then
                Return 0
            Else
                Return _mcModel.ID.idCompositeDecimal
            End If
        End Get
    End Property

    ''' <summary>
    ''' String representation of the composite ID associated with the example.
    ''' This includes maintaining trailing zeros if this is a multi-model example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IDString As String
        Get
            If _mcModel Is Nothing Then
                Return "0"
            Else
                Return _mcModel.ID.idComposite
            End If
        End Get
    End Property

    ''' <summary>
    ''' File name of the model file associated with the example.
    ''' If this is being changed, the new file name is used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides ReadOnly Property fileName As String
        Get
            If _mcModel Is Nothing Then
                Return MyBase.fileName
            Else
                Return _mcModel.modelFile.pathDestination.fileName
            End If
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Autogenerates a list of file paths with the list of file paths.
    ''' </summary>
    ''' <param name="myPath">File paths to add to the class.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal myPath As String)
        MyBase.SetProperties(myPath)

        fileNameUse = True
    End Sub

#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Changes the name of the data source object while preserving the original path.
    ''' </summary>
    ''' <param name="p_name">Name to swap in to the data source path.</param>
    ''' <remarks></remarks>
    Friend Sub SetDataSourceName(ByVal p_name As String)
        _dataSource.SetProperties(_dataSource.directory & "\" & p_name, p_suppressUserInput:=True)
    End Sub

    Friend Sub SetDefaultDataSourceDirectory()
        _dataSource.SetDefaultPath()
    End Sub
#End Region

End Class
