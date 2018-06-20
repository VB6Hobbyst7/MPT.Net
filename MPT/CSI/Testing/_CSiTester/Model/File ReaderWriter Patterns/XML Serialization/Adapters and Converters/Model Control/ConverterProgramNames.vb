Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Namespace ModelControl
    Public Class ConverterProgramNames

        ''' <summary>
        ''' Converts the value from the program to the file.
        ''' </summary>
        ''' <param name="p_values"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertToFile(ByVal p_values As cMCTargetPrograms) As target_programProgram()
            If (p_values Is Nothing OrElse
                p_values.Count = 0) Then Return Nothing
            Dim fileArray(p_values.Count - 1) As target_programProgram

            For i = 0 To p_values.Count - 1
                fileArray(i).name = ConvertToFile(p_values(i))
            Next

            Return fileArray
        End Function

        ''' <summary>
        ''' Converts the value from the program to the file.
        ''' </summary>
        ''' <param name="p_enum"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertToFile(ByVal p_enum As eCSiProgram) As program_name
            Select Case p_enum
                Case eCSiProgram.CSiBridge
                    Return program_name.CSiBridge
                Case eCSiProgram.ETABS
                    Return program_name.ETABS
                Case eCSiProgram.Perform3D
                    Return program_name.Perform3D
                Case eCSiProgram.SAFE
                    Return program_name.SAFE
                Case eCSiProgram.SAP2000
                    Return program_name.SAP2000
                Case eCSiProgram.None
                    Return program_name.SAP2000
                Case Else
                    Return program_name.SAP2000
            End Select
        End Function

        ''' <summary>
        ''' Converts the value from the file to the program.
        ''' </summary>
        ''' <param name="p_values"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertFromFile(ByVal p_values As target_programProgram()) As cMCTargetPrograms
            Dim targetPrograms As New cMCTargetPrograms
            If (p_values Is Nothing OrElse
                p_values.Count = 0) Then Return targetPrograms

            For i = 0 To p_values.Count - 1
                targetPrograms.Add(ConvertFromFile(p_values(i).name))
            Next

            Return targetPrograms
        End Function

        ''' <summary>
        ''' Converts the value from the file to the program.
        ''' </summary>
        ''' <param name="p_enum"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertFromFile(ByVal p_enum As program_name) As eCSiProgram
            Select Case p_enum
                Case program_name.CSiBridge
                    Return eCSiProgram.CSiBridge
                Case program_name.ETABS
                    Return eCSiProgram.ETABS
                Case program_name.Perform3D
                    Return eCSiProgram.Perform3D
                Case program_name.SAFE
                    Return eCSiProgram.SAFE
                Case program_name.SAP2000
                    Return eCSiProgram.SAP2000
                Case Else
                    Return eCSiProgram.None
            End Select
        End Function

    End Class
End Namespace


