Option Explicit On
Option Strict On


Public Class TemplateModelBeam
    Inherits TemplateModel

#Region "Properties"
    ''' <summary>
    ''' The frame section property used for all beams in the frame. 
    ''' This must either be Default or the name of a defined frame section property.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Beam As String = _default
#End Region
End Class
