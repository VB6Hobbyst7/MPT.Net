Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Reporting

''' <summary>
''' Benchmark reference of the program and version used for establishing or updating the benchmark values.
''' </summary>
''' <remarks></remarks>
Public Class cMCBenchmarkRef
    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    Private _programName As eCSiProgram
    ''' <summary>
    ''' Name of the program where the benchmarks were established.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programName As eCSiProgram
        Set(ByVal value As eCSiProgram)
            If Not _programName = value Then
                _programName = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programName"))
            End If
        End Set
        Get
            Return _programName
        End Get
    End Property

    Private _programVersion As String
    ''' <summary>
    ''' Version of the program where the benchmarks were most recently updated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programVersion As String
        Set(ByVal value As String)
            If Not _programVersion = value Then
                _programVersion = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programVersion"))
            End If
        End Set
        Get
            Return _programVersion
        End Get
    End Property

    Private _programVersionLastBest As String
    ''' <summary>
    ''' Version of the program where the 'last best value' benchmarks were most recently updated.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programVersionLastBest As String
        Set(ByVal value As String)
            If Not _programVersionLastBest = value Then
                _programVersionLastBest = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("programVersionLastBest"))
            End If
        End Set
        Get
            Return _programVersionLastBest
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCBenchmarkRef

        With myClone
            .programName = programName
            .programVersion = programVersion
            .programVersionLastBest = programVersionLastBest
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
        If Not (TypeOf p_object Is cMCBenchmarkRef) Then Return False
        Dim comparedObject As cMCBenchmarkRef = TryCast(p_object, cMCBenchmarkRef)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .programName = programName Then Return False
            If Not .programVersion = programVersion Then Return False
            If Not .programVersionLastBest = programVersionLastBest Then Return False
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
            If (String.IsNullOrEmpty(programVersion) OrElse
                programName = eCSiProgram.None) Then Return False

            Return IsValidVersion(programVersion)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Determines if the supplied version is expected to be valid based on the string pattern.
    ''' Version is expected to match the following format: *0.0.0
    ''' </summary>
    ''' <param name="p_version"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsValidVersion(ByVal p_version As String) As Boolean
        If Not p_version.Length >= 5 Then Return False

        Dim dotCount As Integer = 0
        Dim dotIndexMax As Integer = 0
        For i = 1 To p_version.Length
            If Mid(p_version, i, 1) = "." Then
                dotCount += 1
                dotIndexMax = i
            End If
        Next

        Return (dotCount >= 2 AndAlso
                p_version.Length >= dotIndexMax + 1)
    End Function
#End Region

End Class
