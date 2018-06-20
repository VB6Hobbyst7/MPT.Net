Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Class containing a date in numerical entries for day, month, and year. Automatically first assigns properties to current date.
''' </summary>
''' <remarks></remarks>
Public Class cMCDate
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _numDay As Integer
    Public Property numDay As Integer
        Set(ByVal value As Integer)
            If Not _numDay = value Then
                _numDay = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("numDay"))
            End If
        End Set
        Get
            Return _numDay
        End Get
    End Property

    Private _numMonth As Integer
    Public Property numMonth As Integer
        Set(ByVal value As Integer)
            If Not _numMonth = value Then
                _numMonth = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("numMonth"))
            End If
        End Set
        Get
            Return _numMonth
        End Get
    End Property

    Private _numYear As Integer
    Public Property numYear As Integer
        Set(ByVal value As Integer)
            If Not _numYear = value Then
                _numYear = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("numYear"))
            End If
        End Set
        Get
            Return _numYear
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        SetCurrentDate()
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCDate

        With myClone
            .numDay = numDay
            .numMonth = numMonth
            .numYear = numYear
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
        If Not (TypeOf p_object Is cMCDate) Then Return False
        Dim comparedObject As cMCDate = TryCast(p_object, cMCDate)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .numDay = numDay Then Return False
            If Not .numMonth = numMonth Then Return False
            If Not .numYear = numYear Then Return False
        End With

        Return True
    End Function


    ''' <summary>
    ''' Checks whether the minimum required information has been specified for the object.
    ''' </summary>
    ''' <returns>True if all required data is filled. Otherwise, false</returns>
    ''' <remarks></remarks>
    Friend Function RequiredDataFilled() As Boolean
        Try
            If (numDay = 0 OrElse
                numMonth = 0 OrElse
                numYear = 0) Then Return False

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try

        Return True
    End Function
#End Region

#Region "Methods: Private"
    Private Sub SetCurrentDate()
        numYear = Year(DateTime.Now)
        numMonth = Month(DateTime.Now)
        numDay = Day(DateTime.Now)
    End Sub
#End Region

End Class
