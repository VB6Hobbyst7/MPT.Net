Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Class containing a title and URL of a link associated with the example.
''' </summary>
''' <remarks></remarks>
Public Class cMCLink
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _title As String
    ''' <summary>
    ''' Title of the link.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property title As String
        Set(ByVal value As String)
            If Not _title = value Then
                _title = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("title"))
            End If
        End Set
        Get
            Return _title
        End Get
    End Property

    Private _URL As String
    ''' <summary>
    ''' URL to web pages that contain additional information about the model. These links would be typically referencing pages in CSI wiki.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property URL As String
        Set(ByVal value As String)
            If Not _URL = value Then
                _URL = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("URL"))
            End If
        End Set
        Get
            Return _URL
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCLink

        With myClone
            .title = title
            .URL = URL
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
        If Not (TypeOf p_object Is cMCLink) Then Return False
        Dim comparedObject As cMCLink = TryCast(p_object, cMCLink)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .title = title Then Return False
            If Not .URL = URL Then Return False
        End With

        Return True
    End Function
#End Region

End Class
