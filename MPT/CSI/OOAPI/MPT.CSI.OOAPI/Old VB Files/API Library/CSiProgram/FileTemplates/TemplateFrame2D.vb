Option Explicit On
Option Strict On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If

''' <summary>
''' Represents the 2D Frame template model that the user can create in the application.
''' </summary>
''' <remarks></remarks>
Public Class TemplateFrame2D
    Inherits TemplateModelFrame

#Region "Fields"
    Protected _frameArea As IteratedArea = New IteratedArea()
#End Region

#Region "Properties"
    ''' <summary>
    ''' One of the 2D frame template types in the e2DFrameType enumeration.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TemplateType As e2DFrameType = e2DFrameType.PortalFrame

    ''' <summary>
    ''' The number of bays in the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberBays As Integer
        Get
            Return _frameArea.AxisX.Number
        End Get
        Set(value As Integer)
            _frameArea.AxisX.Number = value
        End Set
    End Property
    ''' <summary>
    ''' The width of each bay. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BayWidth As Double
        Get
            Return _frameArea.AxisX.Length
        End Get
        Set(value As Double)
            _frameArea.AxisX.Length = value
        End Set
    End Property

    ''' <summary>
    ''' The frame section property used for all braces in the frame. 
    ''' This must either be Default or the name of a defined frame section property. 
    ''' This item does not apply to the portal frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Brace As String = _default
#End Region

#Region "Initialization"
    Public Sub New(ByVal templateType As e2DFrameType,
                   ByVal numberStories As Integer,
                   ByVal storyHeight As Double,
                   ByVal numberBays As Integer,
                   ByVal bayWidth As Double)
        MyBase.New(numberStories, storyHeight)

        _frameArea.AxisZ = _baseSegment

        Me.NumberBays = numberBays
        Me.BayWidth = bayWidth

        _TemplateType = templateType
    End Sub
#End Region
End Class
