Option Explicit On
Option Strict On

''' <summary>
''' Represents the Wall template model that the user can create in the application.
''' </summary>
''' <remarks></remarks>
Public Class TemplateWall
    Inherits TemplateModelMesh2D


#Region "Properties"
    ''' <summary>
    ''' The shell section property used for the wall. 
    ''' This must either be Default or the name of a defined shell section property. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Area As String = _default
#End Region

#Region "Initialization"
    Public Sub New(ByVal numberMeshX As Integer,
                   ByVal widthMeshX As Double,
                   ByVal numberMeshZ As Integer,
                   ByVal heightMeshZ As Double)
        MyBase.New(widthMeshX, heightMeshZ)

        MyBase.NumberMeshX = numberMeshX
        MyBase.NumberMeshZ = numberMeshZ
    End Sub

#End Region
End Class
