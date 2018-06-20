Option Strict On
Option Explicit On

'TODO: Inherit from collections

''' <summary>
''' Object containing all values relevant to target displacements.
''' </summary>
''' <remarks></remarks>
Public Class TargetDisplacements
#Region "Properties"
    Public Property Targets As New List(Of TargetDisplacement)

    ''' <summary>
    ''' True: All specified targets are active. 
    ''' False: They are inactive.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllSpecifedTargetsActive As Boolean = True
#End Region

#Region "Methods: Public"
    Public Function LoadCases() As List(Of String)
        Dim values As New List(Of String)

        For Each target As TargetDisplacement In _Targets
            values.Add(target.LoadCase)
        Next

        Return values
    End Function

    Public Function PointNames() As List(Of String)
        Dim values As New List(Of String)

        For Each target As TargetDisplacement In _Targets
            values.Add(target.NamePoint)
        Next

        Return values
    End Function

    Public Function DisplacementTargets() As List(Of Double)
        Dim values As New List(Of Double)

        For Each target As TargetDisplacement In _Targets
            values.Add(target.DisplacementTarget)
        Next

        Return values
    End Function
#End Region

End Class
