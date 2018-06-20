Option Explicit On
Option Strict On

Public Class DesignResultsFrameConcrete
    Inherits DesignResultsFrame

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsFrameConcrete Then Exit Sub

        For Each designResult As DesignResultFrameConcrete In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultFrameConcrete Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub


    Public Function AvMajor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultFrameConcrete In ItemResults
            values.Add(result.AvMajor)
        Next

        Return values
    End Function

    Public Function VMajorCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultFrameConcrete In ItemResults
            values.Add(result.VmajorCombo)
        Next

        Return values
    End Function
#End Region
End Class
