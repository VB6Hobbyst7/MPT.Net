Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Log of an update for a particular result. 
''' Each result update should be associated with a model update object. 
''' The association can be made by matching their respective IDs.
''' </summary>
''' <remarks></remarks>
Public Class cMCResultUpdate
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _id As Integer = -1
    ''' <summary>
    ''' ID for the update, which is used to relate this result update to the update entry for the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property id As Integer
        Set(ByVal value As Integer)
            _id = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("id"))
        End Set
        Get
            Return _id
        End Get
    End Property

    Private _comment As String
    ''' <summary>
    ''' Comment about the update performed to the result.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property comment As String
        Set(ByVal value As String)
            If Not _comment = value Then
                _comment = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("comment"))
            End If
        End Set
        Get
            Return _comment
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCResultUpdate

        With myClone
            .id = id
            .comment = comment
        End With

        Return myClone
    End Function
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCResultUpdate) Then Return False
        Dim comparedObject As cMCResultUpdate = TryCast(p_object, cMCResultUpdate)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .id = id Then Return False
            If Not .comment = comment Then Return False
        End With

        Return True
    End Function
#End Region
End Class
