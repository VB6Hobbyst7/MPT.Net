Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Friend Class ConverterTests
    Friend Const RUN_AS_IS As String = "run as is"
    Friend Const RUN_AS_IS_WITH_DIFFERENT_PARAMS As String = "run as is with different sets of analysis parameters"
    Friend Const UPDATE_BRIDGE As String = "update bridge"
    Friend Const UPDATE_BRIDGE_AND_RUN As String = "update bridge and run"

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_values As cMCTestTypes) As String()
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return Nothing
        Dim fileArray(p_values.Count - 1) As String

        For i = 0 To p_values.Count - 1
            fileArray(i) = ConvertToFile(p_values(i))
        Next

        Return fileArray
    End Function

    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_enum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_enum As eTestType) As String
        Select Case p_enum
            Case eTestType.runAsIs
                Return RUN_AS_IS
            Case eTestType.runAsIsDiffAnalyParams
                Return RUN_AS_IS_WITH_DIFFERENT_PARAMS
            Case eTestType.updateBridge
                Return UPDATE_BRIDGE
            Case eTestType.updateBridgeAndRun
                Return UPDATE_BRIDGE_AND_RUN     
            Case eTestType.none
                Return ""
            Case Else
                Return ""
        End Select
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_values As String()) As cMCTestTypes
        Dim testTypes As New cMCTestTypes
        If (p_values Is Nothing OrElse
            p_values.Count = 0) Then Return testTypes

        For i = 0 To p_values.Count - 1
            testTypes.Add(ConvertFromFile(p_values(i)))
        Next

        Return testTypes
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As String) As eTestType
        Select Case p_value
            Case RUN_AS_IS
                Return eTestType.runAsIs
            Case RUN_AS_IS_WITH_DIFFERENT_PARAMS
                Return eTestType.runAsIsDiffAnalyParams
            Case UPDATE_BRIDGE
                Return eTestType.updateBridge
            Case UPDATE_BRIDGE_AND_RUN
                Return eTestType.updateBridgeAndRun
            Case ""
                Return eTestType.none
            Case Else
                Return eTestType.myError
        End Select
    End Function

End Class
