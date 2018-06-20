Option Strict On
Option Explicit On

'TODO: Inherit from collections

''' <summary>
''' Object containing all values relevant to target periods.
''' </summary>
''' <remarks></remarks>
Public Class TargetPeriods

#Region "Properties"
    Public Property Targets As New List(Of TargetPeriod)

    ''' <summary>
    ''' True: All specified targets are active. 
    ''' False: They are inactive.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllSpecifedTargetsActive As Boolean = True

    ''' <summary>
    ''' Name of the modal load case for which the target applies.
    ''' Applies across all targets.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ModalCase As String
        Get
            If Targets.Count > 0 Then
                Return Targets(0).ModalCase
            Else
                Return ""
            End If
        End Get
    End Property
#End Region



#Region "Methods: Public"
    Public Function ModeNumbers() As List(Of Integer)
        Dim values As New List(Of Integer)

        For Each target As TargetPeriod In _Targets
            values.Add(target.ModeNumber)
        Next

        Return values
    End Function


    Public Function PeriodTargets() As List(Of Double)
        Dim values As New List(Of Double)

        For Each target As TargetPeriod In _Targets
            values.Add(target.PeriodTarget)
        Next

        Return values
    End Function
#End Region

#Region "Methods: Private"

#End Region

End Class
