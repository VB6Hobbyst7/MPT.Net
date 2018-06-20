Option Strict On
Option Explicit On

''' <summary>
''' Logs the status of a test and its subtests to be added to the overall test summary.
''' </summary>
''' <remarks></remarks>
Public Class cE2ELogger
    Implements ICloneable
#Region "Variables"
    Private expectCurrent As String
#End Region


#Region "Properties"
    ''' <summary>
    ''' Name of the function that is running the series of sub-tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property fName As String
    ''' <summary>
    ''' Number of sub-tests run. Also the current highest number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestNum As Integer
    ''' <summary>
    ''' Number of sub-tests that have passed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestPassNum As Integer
    ''' <summary>
    ''' Number of sub-tests that have failed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestFailNum As Integer
    ''' <summary>
    ''' Concatenated multi-line string that records the results of the sub-tests, including additional information on failed sub-tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestLog As String
    ''' <summary>
    ''' Concatenated multi-line string that records onyl the failed results of the sub-tests.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property subTestSummaryLog As String
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    ''' <summary>
    ''' Initialization function that begins the log with the name of the test function.
    ''' </summary>
    ''' <param name="functionName">Name of the function that contains all sub-tests to be logged.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal functionName As String)
        fName = functionName
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cE2ELogger

        With myClone
            .fName = fName
            .subTestNum = subTestNum
            .subTestPassNum = subTestPassNum
            .subTestFailNum = subTestFailNum
            .subTestLog = subTestLog
            .subTestSummaryLog = subTestSummaryLog
        End With

        Return myClone
    End Function
#End Region

#Region "Methods"
    ''' <summary>
    ''' Begins the log entry for the next sub-test. Writes the expectation statement on the first line and increments the count.
    ''' </summary>
    ''' <param name="expectationStatement"></param>
    ''' <remarks></remarks>
    Friend Sub Expect(ByVal expectationStatement As String)
        subTestNum += 1
        expectCurrent = "  Expectation: " & expectationStatement & Environment.NewLine
    End Sub

    ''' <summary>
    ''' Increments the number of passed sub-tests and adds the appropriate log entry.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SubTestPass()
        subTestPassNum += 1
        subTestLog = subTestLog & expectCurrent & "      Result: " & "Pass" & Environment.NewLine & Environment.NewLine
    End Sub

    ''' <summary>
    ''' Increments the number of failed sub-tests and adds the appropriate log entries with information related to the failure. 
    ''' At least one of either the 'expectedResult' or 'notExpectedResult' is required for the function to work properly.
    ''' </summary>
    ''' <param name="actualResult">The actual result of the test.</param>
    ''' <param name="actualResultCall">The string expression of the code called to get the result.</param>
    ''' <param name="expectedResult">The expected result of the test. The test passes if this matches the actual result.</param>
    ''' <param name="notExpectedResult">The result the test is not expected to have. Matching this is a failure of the test.</param>
    ''' <remarks></remarks>
    Friend Sub SubTestFail(ByVal actualResult As String, Optional ByVal actualResultCall As String = "", Optional ByVal expectedResult As String = "expectedResult", Optional ByVal notExpectedResult As String = "notExpectedResult")
        Dim subTestFailText As String

        subTestFailNum += 1
        subTestFailText = expectCurrent
        subTestFailText = subTestFailText & "      Result: " & "FAIL!!!!" & Environment.NewLine
        If Not expectedResult = "expectedResult" Then
            subTestFailText = subTestFailText & "          Expected: " & expectedResult & Environment.NewLine
        End If
        If Not notExpectedResult = "notExpectedResult" Then
            subTestFailText = subTestFailText & "          Not Expected: " & notExpectedResult & Environment.NewLine
        End If
        subTestFailText = subTestFailText & "          Actual: " & actualResultCall & " = " & actualResult & Environment.NewLine & Environment.NewLine

        subTestLog = subTestLog & subTestFailText
        subTestSummaryLog = subTestSummaryLog & subTestFailText
    End Sub

    ''' <summary>
    ''' Records the unhandled exception that has interrupted the test.
    ''' </summary>
    ''' <param name="exMessage">Exception error message.</param>
    ''' <param name="exStackTrace">Exception stack trace message.</param>
    ''' <remarks></remarks>
    Friend Sub SubTestException(ByVal exMessage As String, exStackTrace As String)
        Dim subTestExceptionText As String

        subTestExceptionText = "      Result: Exception!!!!" & Environment.NewLine & _
                               "        Message: " & Environment.NewLine & _
                               "            " & exMessage & Environment.NewLine & _
                               "        Stack Trace: " & Environment.NewLine & _
                               "            " & exStackTrace & Environment.NewLine & Environment.NewLine

        subTestLog = subTestLog & subTestExceptionText
        subTestSummaryLog = subTestSummaryLog & subTestExceptionText

        SubTestsFinish()
    End Sub

    ''' <summary>
    ''' Records the unhandled exception that has interrupted the test.
    ''' </summary>
    ''' <param name="message">Custom message to use for the unhandled exception.</param>
    ''' <remarks></remarks>
    Friend Sub SubTestExceptionCustom(ByVal message As String)
        Dim subTestExceptionText As String

        subTestExceptionText = "      Result: Exception!!!!" & Environment.NewLine & _
                               "        Custom Message: " & Environment.NewLine & _
                               "            " & message & Environment.NewLine & Environment.NewLine

        subTestLog = subTestLog & subTestExceptionText
        subTestSummaryLog = subTestSummaryLog & subTestExceptionText

        SubTestsFinish()
    End Sub

    ''' <summary>
    ''' Ends the log for the series of sub-tests run for a given test function.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SubTestsFinish()
        Dim subTestFinishText As String

        subTestFinishText = "===============================================================" & Environment.NewLine & _
                            "Begin test function: " & fName & Environment.NewLine & _
                            "===============================================================" & Environment.NewLine & _
                            "   SubTests Run:    " & subTestNum & Environment.NewLine & _
                            "---------------------------------------------------------------" & Environment.NewLine & _
                            "   SubTests Passed: " & subTestPassNum & Environment.NewLine & _
                            "   SubTests Failed: " & subTestFailNum & Environment.NewLine & _
                            "===============================================================" & Environment.NewLine

        subTestLog = subTestFinishText & subTestLog
        subTestSummaryLog = subTestFinishText & subTestSummaryLog
    End Sub

#End Region

End Class
