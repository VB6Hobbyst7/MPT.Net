Option Explicit On
Option Strict On

''' <summary>
''' Represents the Solid Block template model that the user can create in the application.
''' </summary>
''' <remarks></remarks>
Public Class TemplateSolidBlock
    Inherits TemplateModelMesh2D

#Region "Fields"
    Protected _meshVolume As IteratedVolume = New IteratedVolume
#End Region

#Region "Properties"
    ''' <summary>
    ''' The solid property used for the wall. 
    ''' This must either be Default or the name of a defined solid property. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Solid As String = _default

    ''' <summary>
    ''' The number of area objects in the global y direction of the wall.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberMeshY As Integer
        Get
            Return _meshVolume.AxisY.Number
        End Get
        Set(value As Integer)
            _meshVolume.AxisY.Number = value
        End Set
    End Property

    ''' <summary>
    ''' The width of each area object measured in the global y direction. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WidthMeshY As Double
        Get
            Return _meshVolume.AxisY.Length
        End Get
        Set(value As Double)
            _meshVolume.AxisY.Length = value
        End Set
    End Property
#End Region

#Region "Initialization"
    Public Sub New(ByVal widthMeshX As Double,
                   ByVal widthMeshY As Double,
                   ByVal heightMeshZ As Double)
        MyBase.New(widthMeshX, heightMeshZ)

        _meshVolume.AxisX = _meshArea.AxisX
        _meshVolume.AxisZ = _meshArea.AxisZ

        Me.WidthMeshY = widthMeshY


        Me.NumberMeshX = 5
        Me.NumberMeshY = 8
        Me.NumberMeshZ = 10
    End Sub

#End Region
End Class
