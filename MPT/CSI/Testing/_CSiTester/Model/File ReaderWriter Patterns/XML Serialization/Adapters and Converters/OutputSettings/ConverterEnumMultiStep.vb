Option Explicit On
Option Strict On

Imports CSiTester.cProgramControl

Friend Class ConverterEnumMultiStep

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_enum As eStepType) As tabularOutputTableSetOptionsMultiStep
        Select Case p_enum
            Case eStepType.envelope
                Return tabularOutputTableSetOptionsMultiStep.Envelopes
            Case eStepType.lastStep
                Return tabularOutputTableSetOptionsMultiStep.LastStep
            Case eStepType.stepByStep
                Return tabularOutputTableSetOptionsMultiStep.StepbyStep
            Case Else
                Return tabularOutputTableSetOptionsMultiStep.Envelopes
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_enum As tabularOutputTableSetOptionsMultiStep) As eStepType
        Select Case p_enum
            Case tabularOutputTableSetOptionsMultiStep.Envelopes
                Return eStepType.envelope
            Case tabularOutputTableSetOptionsMultiStep.LastStep
                Return eStepType.lastStep
            Case tabularOutputTableSetOptionsMultiStep.StepbyStep
                Return eStepType.stepByStep
            Case Else
                Return eStepType.none
        End Select
    End Function

End Class
