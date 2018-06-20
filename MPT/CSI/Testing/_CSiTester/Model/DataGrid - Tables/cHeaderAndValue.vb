Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class cHeaderAndValue
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


    Private _value As String
    ''' <summary>
    ''' Value associated with the header for a particular record.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property value As String
        Set(ByVal p_newValue As String)
            If Not _value = p_newValue Then
                _value = p_newValue
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("value"))
            End If
        End Set
        Get
            Return _value
        End Get
    End Property


    Private _header As String
    ''' <summary>
    ''' Header name associated with the value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property header As String
        Set(ByVal p_newValue As String)
            If Not _header = p_newValue Then
                _header = p_newValue
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("header"))
            End If
        End Set
        Get
            Return _header
        End Get
    End Property

End Class
