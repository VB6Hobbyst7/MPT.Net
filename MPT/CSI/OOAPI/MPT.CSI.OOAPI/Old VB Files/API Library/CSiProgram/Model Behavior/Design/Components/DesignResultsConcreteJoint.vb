Option Explicit On
Option Strict On

Public Class DesignResultsConcreteJoint
    Inherits DesignResults

#Region "Methods: Public"

    Public Overrides Sub AddResults(ByVal designResults As DesignResults)
        If Not TypeOf designResults Is DesignResultsConcreteJoint Then Exit Sub

        For Each designResult As DesignResultConcreteJoint In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overrides Sub AddResult(ByVal designResult As DesignResult)
        If Not TypeOf designResult Is DesignResultConcreteJoint Then Exit Sub

        _ItemResults.Add(designResult)
    End Sub


    Public Function ControllingBeamColumnCapacityRatioMajorCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.ControllingBeamColumnCapacityRatioMajorCombo)
        Next

        Return values
    End Function

    Public Function BeamColumnCapacityRatiosMajor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.BeamColumnCapacityRatioMajor)
        Next

        Return values
    End Function

    Public Function ControllingBeamColumnCapacityRatioMinorCombos() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.ControllingBeamColumnCapacityRatioMinorCombo)
        Next

        Return values
    End Function

    Public Function BeamColumnCapacityRatiosMinor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.BeamColumnCapacityRatioMinor)
        Next

        Return values
    End Function

    Public Function ControllingJointShearRatiosMajorCombo() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.ControllingJointShearRatioMajorCombo)
        Next

        Return values
    End Function

    Public Function JointShearRatiosMajor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.JointShearRatioMajor)
        Next

        Return values
    End Function

    Public Function ControllingJointShearRatiosMinorCombo() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.ControllingJointShearRatioMinorCombo)
        Next

        Return values
    End Function

    Public Function JointShearRatiosMinor() As List(Of Double)
        Dim values As New List(Of Double)
        For Each result As DesignResultConcreteJoint In ItemResults
            values.Add(result.JointShearRatioMinor)
        Next

        Return values
    End Function
#End Region

End Class
