Option Explicit On
Option Strict On

Public MustInherit Class TemplateModelMesh2D
    Inherits TemplateModel

#Region "Fields"
    Protected _meshArea As IteratedArea = New IteratedArea()
#End Region

#Region "Properties"
    ''' <summary>
    ''' The number of area objects in the global X direction.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberMeshX As Integer
        Get
            Return _meshArea.AxisX.Number
        End Get
        Set(value As Integer)
            _meshArea.AxisX.Number = value
        End Set
    End Property

    ''' <summary>
    ''' The width of each area object measured in the global X direction. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WidthMeshX As Double
        Get
            Return _meshArea.AxisX.Length
        End Get
        Set(value As Double)
            _meshArea.AxisX.Length = value
        End Set
    End Property

    ''' <summary>
    ''' The number of area objects in the global z direction.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NumberMeshZ As Integer
        Get
            Return _meshArea.AxisZ.Number
        End Get
        Set(value As Integer)
            _meshArea.AxisZ.Number = value
        End Set
    End Property

    ''' <summary>
    ''' The height of each area object measured in the global Z direction. [L]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HeightMeshZ As Double
        Get
            Return _meshArea.AxisZ.Length
        End Get
        Set(value As Double)
            _meshArea.AxisZ.Length = value
        End Set
    End Property
#End Region

#Region "Initialization"
    Public Sub New(ByVal widthMeshX As Double,
                   ByVal heightMeshZ As Double)

        Me.WidthMeshX = widthMeshX
        Me.HeightMeshZ = heightMeshZ
    End Sub

#End Region
End Class
