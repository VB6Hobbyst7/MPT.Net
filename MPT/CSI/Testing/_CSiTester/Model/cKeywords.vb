Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Public Class cKeywords
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _keywordExisting As String
    Public Property keywordExisting As String
        Set(ByVal value As String)
            If Not _keywordAdd = value Then
                _keywordExisting = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordExisting"))
            End If
        End Set
        Get
            Return _keywordExisting
        End Get
    End Property

    Private _keywordAdd As String
    Public Property keywordAdd As String
        Set(ByVal value As String)
            If Not _keywordAdd = value Then
                _keywordAdd = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordAdd"))
            End If
        End Set
        Get
            Return _keywordAdd
        End Get
    End Property

    Private _keywordRemove As String
    Public Property keywordRemove As String
        Set(ByVal value As String)
            If Not _keywordRemove = value Then
                _keywordRemove = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("keywordRemove"))
            End If
        End Set
        Get
            Return _keywordRemove
        End Get
    End Property


    Public Property keywordExistingList As ObservableCollection(Of String)
    Public Property keywordAddList As ObservableCollection(Of String)
    Public Property keywordRemoveList As ObservableCollection(Of String)
    Public Property keywordOfficialList As ObservableCollection(Of String)
    
#End Region

#Region "Initialization"
    Sub New()
        keywordExistingList = New ObservableCollection(Of String)
        keywordAddList = New ObservableCollection(Of String)
        keywordRemoveList = New ObservableCollection(Of String)
        keywordOfficialList = New ObservableCollection(Of String)
    End Sub
#End Region
End Class
