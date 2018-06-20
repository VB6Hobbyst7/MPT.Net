Option Explicit On
Option Strict On

Public Class DesignResultsConcreteBeam
    Inherits DesignResultsFrameConcrete

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsConcreteBeam Then Exit Sub

        For Each designResult As DesignResultConcreteBeam In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultConcreteBeam Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub



    Public Function TopLongCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TopLongCombo)
        Next

        Return values
    End Function

    Public Function TopLongAreas() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TopLongArea)
        Next

        Return values
    End Function

    Public Function BottomLongCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.BottomLongCombo)
        Next

        Return values
    End Function

    Public Function BottomLongAreas() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.BottomLongArea)
        Next

        Return values
    End Function

    Public Function TorsionLongCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TorsionLongCombo)
        Next

        Return values
    End Function

    Public Function TorsionLongAreas() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TorsionLongArea)
        Next

        Return values
    End Function

    Public Function TorsionTransverseCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TorsionTransverseCombo)
        Next

        Return values
    End Function

    Public Function TorsionTransverseAreas() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteBeam In ItemResults
            values.Add(result.TorsionTransverseArea)
        Next

        Return values
    End Function
#End Region

End Class
