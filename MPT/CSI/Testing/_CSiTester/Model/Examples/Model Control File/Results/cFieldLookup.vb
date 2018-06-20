Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.FileSystem.PathLibrary

''' <summary>
''' Class containing information of the table columns and values used to look up a particular cell for getting the output value.
''' </summary>
''' <remarks></remarks>
Public Class cFieldLookup
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _name As String
    ''' <summary>
    ''' Name of the column/header of the field to look up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property name As String
        Set(ByVal value As String)
            If Not _name = value Then
                _name = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("name"))
            End If
        End Set
        Get
            Return _name
        End Get
    End Property

    Private _valueField As String
    ''' <summary>
    ''' Value of the field to look up within the named column/header.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueField As String
        Set(ByVal value As String)
            If Not _valueField = value Then
                TrimQuotesSingle(value)
                _valueField = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("valueField"))
            End If
        End Set
        Get
            Return _valueField
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cFieldLookup

        With myClone
            .name = name
            .valueField = valueField
        End With

        Return myClone
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cFieldLookup) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cFieldLookup = TryCast(p_object, cFieldLookup)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .name = name Then Return False
            If Not .valueField = valueField Then Return False
        End With

        Return True
    End Function
#End Region


End Class
