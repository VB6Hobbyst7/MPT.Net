Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class cPathSelected
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _ID As Decimal
    Public Property ID As Decimal
        Get
            Return _ID
        End Get
        Set(value As Decimal)
            If Not _ID = value Then
                _ID = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ID"))
            End If
        End Set
    End Property

    Private _IDString As String
    Public Property IDString As String
        Get
            Return _IDString
        End Get
        Set(value As String)
            If Not _IDString = value Then
                _IDString = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("IDString"))
            End If
        End Set
    End Property

    Private _fileName As String
    Public Property fileName As String
        Get
            Return _fileName
        End Get
        Set(value As String)
            If Not _fileName = value Then
                _fileName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("fileName"))
            End If
        End Set
    End Property

    Private _exampleStatus As String
    Public Property exampleStatus As String
        Get
            Return _exampleStatus
        End Get
        Set(value As String)
            If Not _exampleStatus = value Then
                _exampleStatus = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("exampleStatus"))
            End If
        End Set
    End Property
End Class