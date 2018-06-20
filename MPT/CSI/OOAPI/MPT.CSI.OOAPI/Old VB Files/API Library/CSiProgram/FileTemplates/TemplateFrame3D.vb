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
''' Represents the 3D Frame template model that the user can create in the application.
''' </summary>
''' <remarks></remarks>
Public Class TemplateFrame3D
    Inherits TemplateModelFrame

#Region "Fields"
    Protected _frameVolume As IteratedVolume = New IteratedVolume()
    Protected _meshArea As IteratedArea = New IteratedArea()
#End Region

#Region "Properties"
    ''' <summary>
    ''' One of the 3D frame template types in the e3DFrameType enumeration.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TemplateType As e3DFrameType = e3DFrameType.OpenFrame

    ''' <summary>
    ''' The number of bays in the global X direction of the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberBaysX As Integer
        Get
            Return _frameVolume.AxisX.Number
        End Get
        Set(value As Integer)
            _frameVolume.AxisX.Number = value
        End Set
    End Property
    ''' <summary>
    ''' The width of each bay in the global X direction of the frame. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BayWidthX As Double
        Get
            Return _frameVolume.AxisX.Length
        End Get
        Set(value As Double)
            _frameVolume.AxisX.Length = value
        End Set
    End Property

    ''' <summary>
    ''' The number of bays in the global Y direction of the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberBaysY As Integer
        Get
            Return _frameVolume.AxisY.Number
        End Get
        Set(value As Integer)
            _frameVolume.AxisY.Number = value
        End Set
    End Property
    ''' <summary>
    ''' The width of each bay in the global Y direction of the frame. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BayWidthY As Double
        Get
            Return _frameVolume.AxisY.Length
        End Get
        Set(value As Double)
            _frameVolume.AxisY.Length = value
        End Set
    End Property

    ''' <summary>
    ''' The shell section property used for all floor slabs in the frame. 
    ''' This must either be Default or the name of a defined shell section property. 
    ''' This item does not apply to the open and perimeter frames.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Area As String = _default

    ''' <summary>
    ''' The number of divisions for each floor area object in the global X direction. 
    ''' This item does not apply to the open and perimeter frames.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberFloorXDivisions As Integer
        Get
            Return _meshArea.AxisX.Divisions
        End Get
        Set(value As Integer)
            _meshArea.AxisX.Divisions = value
        End Set
    End Property

    ''' <summary>
    ''' The number of divisions for each floor area object in the global Y direction. 
    ''' This item does not apply to the open and perimeter frames.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberFloorYDivisions As Integer
        Get
            Return _meshArea.AxisZ.Divisions
        End Get
        Set(value As Integer)
            _meshArea.AxisZ.Divisions = value
        End Set
    End Property

#End Region

#Region "Initialization"
    Public Sub New(ByVal templateType As e3DFrameType,
                   ByVal numberStories As Integer,
                   ByVal storyHeight As Double,
                   ByVal numberBaysX As Integer,
                   ByVal bayWidthX As Double,
                   ByVal numberBaysY As Integer,
                   ByVal bayWidthY As Double)
        MyBase.New(numberStories, storyHeight)

        _frameVolume.AxisZ = _baseSegment

        Me.NumberBaysX = numberBaysX
        Me.BayWidthX = bayWidthX

        Me.NumberBaysY = numberBaysY
        Me.BayWidthY = bayWidthY

        _TemplateType = templateType

        NumberFloorXDivisions = 4
        NumberFloorYDivisions = 4
    End Sub
#End Region
End Class
