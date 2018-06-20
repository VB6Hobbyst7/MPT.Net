Option Explicit On
Option Strict On

Public Class DesignResultsConcreteColumn
    Inherits DesignResultsFrameConcrete

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsConcreteColumn Then Exit Sub

        For Each designResult As DesignResultConcreteColumn In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultConcreteColumn Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub

    Public Function AvMinor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.AvMinor)
        Next

        Return values
    End Function

    Public Function VMinorCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.VminorCombo)
        Next

        Return values
    End Function

    Public Function DesignOptions() As List(Of DesignOption)
        Dim values As New List(Of DesignOption)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.DesignOption)
        Next

        Return values
    End Function

    Public Function PMMCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.PMMCombo)
        Next

        Return values
    End Function

    Public Function PMMAreas() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.PMMArea)
        Next

        Return values
    End Function

    Public Function PMMRatios() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteColumn In ItemResults
            values.Add(result.PMMRatio)
        Next

        Return values
    End Function
#End Region

End Class
