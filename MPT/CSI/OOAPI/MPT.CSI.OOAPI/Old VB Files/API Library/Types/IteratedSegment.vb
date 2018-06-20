Option Strict On
Option Explicit On

Public Class IteratedSegment

#Region "Frields"
    Protected _baseDimension As Double = 144 'Assumed [L] = inches
    Protected _minLength As Double = 0
    Protected _minNumber As Integer = 1
    Protected _minDivisions As Integer = 1
#End Region

#Region "Properties"
    Private _Length As Double
    Public Property Length As Double
        Get
            Return _Length
        End Get
        Set(value As Double)
            If value > _minLength Then _Length = value
        End Set
    End Property

    Private _Number As Integer = _minNumber
    Public Property Number As Integer
        Get
            Return _Number
        End Get
        Set(value As Integer)
            If value > _minNumber Then _Number = value
        End Set
    End Property

    Private _Divisions As Integer = _minDivisions
    Public Property Divisions As Integer
        Get
            Return _Divisions
        End Get
        Set(value As Integer)
            If value > _minDivisions Then _Divisions = value
        End Set
    End Property
#End Region
End Class
