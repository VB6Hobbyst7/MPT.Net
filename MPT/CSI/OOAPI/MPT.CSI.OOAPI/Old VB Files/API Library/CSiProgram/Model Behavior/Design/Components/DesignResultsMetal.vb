Option Explicit On
Option Strict On


Public Class DesignResultsMetal
    Inherits DesignResultsFrame

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsMetal Then Exit Sub

        For Each designResult As DesignResultMetal In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultMetal Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub


    Public Function Ratios() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultMetal In ItemResults
            values.Add(result.Ratio)
        Next

        Return values
    End Function

    Public Function RatiosType() As List(Of RatioType)
        Dim values As New List(Of RatioType)
        For Each result As DesignResultMetal In ItemResults
            values.Add(result.RatioType)
        Next

        Return values
    End Function

    Public Function ComboNames() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultMetal In ItemResults
            values.Add(result.ComboName)
        Next

        Return values
    End Function
#End Region

End Class
