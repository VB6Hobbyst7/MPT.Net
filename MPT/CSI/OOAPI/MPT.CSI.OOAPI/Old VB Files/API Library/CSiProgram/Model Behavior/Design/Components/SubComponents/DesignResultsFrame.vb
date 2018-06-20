Option Explicit On
Option Strict On

Public Class DesignResultsFrame
    Inherits DesignResults

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsFrame Then Exit Sub

        For Each designResult As DesignResultFrame In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultFrame Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub

    Public Function Locations() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultFrame In ItemResults
            values.Add(result.Location)
        Next

        Return values
    End Function
#End Region
End Class
