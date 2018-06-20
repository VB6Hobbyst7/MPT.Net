Option Explicit On
Option Strict On

Public Class cMCRange
    Inherits cMCRangeAll

#Region "Properties"
    Private _valueFirst As String
    ''' <summary>
    ''' Value at which the range starts. If empty, the range begins at the earliest value within the specified field name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueFirst As String
        Set(ByVal value As String)
            If Not _valueFirst = value Then
                _valueFirst = value

                RaisePropertyChanged("valueFirst")
            End If
        End Set
        Get
            Return _valueFirst
        End Get
    End Property

    Private _valueLast As String
    ''' <summary>
    ''' Value at which the range ends. If empty, the range begins at the latest value within the specified field name.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property valueLast As String
        Set(ByVal value As String)
            If Not _valueLast = value Then
                _valueLast = value

                RaisePropertyChanged("valueLast")
            End If
        End Set
        Get
            Return _valueLast
        End Get
    End Property
#End Region

#Region "Initialization"

    Friend Sub New()

    End Sub


    Friend Overrides Function Clone() As Object
        Dim myClone As cMCRange = DirectCast(MyBase.Clone, cMCRange)

        With myClone
            .valueFirst = valueFirst
            .valueLast = valueLast
        End With

        Return myClone
    End Function
    Protected Overrides Function Create() As cMCRangeAll
        Return New cMCRange()
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
        If Not (TypeOf p_object Is cMCRange) Then Return False
        Dim isMatch As Boolean = True
        Dim comparedObject As cMCRange = TryCast(p_object, cMCRange)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not MyBase.Equals(p_object) Then Return False
            If Not .valueFirst = valueFirst Then Return False
            If Not .valueLast = valueLast Then Return False
        End With

        Return True
    End Function
#End Region

End Class
