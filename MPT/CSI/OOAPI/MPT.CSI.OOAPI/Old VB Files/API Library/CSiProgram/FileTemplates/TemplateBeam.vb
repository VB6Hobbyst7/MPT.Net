Option Explicit On
Option Strict On

''' <summary>
''' Represents the Beam template model that the user can create in the application.
''' </summary>
''' <remarks></remarks>
Public Class TemplateBeam
    Inherits TemplateModelBeam

#Region "Properties"
    ''' <summary>
    ''' The number of spans for the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberSpans As Integer
        Get
            Return _baseSegment.Number
        End Get
        Set(value As Integer)
            _baseSegment.Number = value
        End Set
    End Property

    ''' <summary>
    ''' The length of each span. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpanLength As Double
        Get
            Return _baseSegment.Length
        End Get
        Set(value As Double)
            _baseSegment.Length = value
        End Set
    End Property
#End Region

#Region "Initialization"
    Public Sub New(ByVal numberSpans As Integer,
                   ByVal spanLength As Double)
        Me.NumberSpans = numberSpans
        Me.SpanLength = spanLength
    End Sub
#End Region
End Class
