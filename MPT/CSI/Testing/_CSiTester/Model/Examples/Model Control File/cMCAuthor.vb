Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Class containing information about the person and/or company associated with the example creation.
''' </summary>
''' <remarks></remarks>
Public Class cMCAuthor
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _name As String
    ''' <summary>
    ''' Name of the creator of the example.
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

    Private _company As String
    ''' <summary>
    ''' Company associated with the creation of the example.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property company As String
        Set(ByVal value As String)
            If Not _company = value Then
                _company = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("company"))
            End If
        End Set
        Get
            Return _company
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCAuthor

        With myClone
            .company = company
            .name = name
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
        If Not (TypeOf p_object Is cMCAuthor) Then Return False
        Dim comparedObject As cMCAuthor = TryCast(p_object, cMCAuthor)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .name = name Then Return False
            If Not .company = company Then Return False
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
            If (String.IsNullOrWhiteSpace(name) OrElse
                String.IsNullOrWhiteSpace(company)) Then Return False

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try

        Return True
    End Function
#End Region

End Class
