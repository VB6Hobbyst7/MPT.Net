Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Overall and secondary classifications of the example, such as 'Published Verification' of the type 'ETABS Analysis Verification Suite'.
''' </summary>
''' <remarks></remarks>
Public Class cMCClassification
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _level1 As String
    ''' <summary>
    ''' Overall classification of the example, such as "Published Verification", "Regression", or "Internal Verification".
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property level1 As String
        Set(ByVal value As String)
            If Not _level1 = value Then
                _level1 = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("level1"))
            End If
        End Set
        Get
            Return _level1
        End Get
    End Property

    Private _level2 As String
    ''' <summary>
    ''' Secondary classification of the example. Options available are dependent upon the Level 1 classification. 
    ''' For "Published Verification", this is the section of the verification suite, such as 'Analysis', or 'Composite Beam', etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property level2 As String
        Set(ByVal value As String)
            If Not _level2 = value Then
                _level2 = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("level2"))
            End If
        End Set
        Get
            Return _level2
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCClassification

        With myClone
            .level1 = level1
            .level2 = level2
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
        If Not (TypeOf p_object Is cMCClassification) Then Return False
        Dim comparedObject As cMCClassification = TryCast(p_object, cMCClassification)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .level1 = level1 Then Return False
            If Not .level2 = level2 Then Return False
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
            If (String.IsNullOrEmpty(level1) OrElse
                String.IsNullOrEmpty(level2)) Then Return False

        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
            Return False
        End Try

        Return True
    End Function
#End Region

End Class
