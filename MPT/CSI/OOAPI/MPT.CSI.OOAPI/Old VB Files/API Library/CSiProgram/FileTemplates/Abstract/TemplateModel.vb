Option Explicit On
Option Strict On

Public MustInherit Class TemplateModel
#Region "Fields"
    Protected Const _default As String = "Default"
    Protected _baseSegment As IteratedSegment = New IteratedSegment()

#End Region

#Region "Properties"
    ''' <summary>
    ''' True: Joint restraints are provided at the base of the frame.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Restraint As Boolean = True
#End Region

#Region "Methods"


#End Region

End Class
