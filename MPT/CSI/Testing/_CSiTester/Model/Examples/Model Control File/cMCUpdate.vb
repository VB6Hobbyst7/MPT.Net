Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

''' <summary>
''' Data about any update performed on the example, such as updating benchmarks or changing the model.
''' </summary>
''' <remarks></remarks>
Public Class cMCUpdate
    Implements ICloneable
    Implements IComparable(Of cMCUpdate)
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Friend Const NO_TICKET As String = "N/A"
#End Region

#Region "Properties"
    Private _id As Integer = -1
    ''' <summary>
    ''' ID for the update, which is used to relate this example update to the update entries for individual results.
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

    Private _person As String
    ''' <summary>
    ''' Person who updated the example model, benchmark, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property person As String
        Set(ByVal value As String)
            If Not _person = value Then
                _person = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("person"))
            End If
        End Set
        Get
            Return _person
        End Get
    End Property

    Private _ticket As Integer
    ''' <summary>
    ''' Ticket number associated with the update to the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ticket As Integer
        Set(ByVal value As Integer)
            If Not _ticket = value Then
                _ticket = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("ticket"))
            End If
        End Set
        Get
            Return _ticket
        End Get
    End Property

    Private _build As String
    ''' <summary>
    ''' Program version &amp; build for which the example is being changed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property build As String
        Set(ByVal value As String)
            _build = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("build"))
        End Set
        Get
            Return _build
        End Get
    End Property

    Private _comment As String
    ''' <summary>
    ''' Comment about the update performed to the example.
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

    Private _updateDate As cMCDate
    ''' <summary>
    ''' Date on which the update was performed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property updateDate As cMCDate
        Set(ByVal value As cMCDate)
            _updateDate = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("updateDate"))
        End Set
        Get
            Return _updateDate
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()
        updateDate = New cMCDate
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCUpdate

        With myClone
            .id = id
            .build = build
            .comment = comment
            .person = person
            .ticket = ticket
            .updateDate = CType(updateDate.Clone, cMCDate)
        End With

        Return myClone
    End Function

    Friend Overloads Function CompareTo(ByVal p_mcUpdate As cMCUpdate) As Integer Implements System.IComparable(Of cMCUpdate).CompareTo
        If (updateDate.numYear > p_mcUpdate.updateDate.numYear AndAlso
            updateDate.numMonth > p_mcUpdate.updateDate.numMonth AndAlso
            updateDate.numDay > p_mcUpdate.updateDate.numDay) Then

            Return 1

        ElseIf (updateDate.numYear = p_mcUpdate.updateDate.numYear AndAlso
        updateDate.numMonth = p_mcUpdate.updateDate.numMonth AndAlso
        updateDate.numDay = p_mcUpdate.updateDate.numDay) Then

            Return 0

        Else
            Return -1
        End If

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
        If Not (TypeOf p_object Is cMCUpdate) Then Return False
        Dim comparedObject As cMCUpdate = TryCast(p_object, cMCUpdate)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .id = id Then Return False
            If Not .person = person Then Return False
            If Not .ticket = ticket Then Return False
            If Not .build = build Then Return False
            If Not .comment = comment Then Return False
            If Not .updateDate.Equals(updateDate) Then Return False
        End With

        Return True
    End Function
#End Region

End Class
