Option Explicit On
Option Strict On

Public Class cCSiTesterLogger
#Region "Properties"
    ''' <summary>
    ''' If true, various error messages will be suppressed. Especially used for ex. messages from try/catch statements.
    ''' </summary>
    ''' <remarks></remarks>
    Private _suppressExStates As Boolean
#End Region


#Region "Initialization"
    Sub New()

    End Sub

    Sub New(ByVal suppressExStates As Boolean)
        _suppressExStates = suppressExStates
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Either displays exception prompts to the user, or records them in the e2eTester log, or does nothing depending on the state of the program.
    ''' </summary>
    ''' <param name="ex">Exception</param>
    ''' <param name="overrideSuppressExStates">If specified as 'false', the messagebox may still appear even if the logger property is set to 'true'.</param>
    ''' <param name="newSuppressExStates">If 'true', messages will be suppressed regardless of the logger property.</param>
    ''' <remarks></remarks>
    Sub ExceptionAction(ByVal ex As Exception,
                        Optional ByVal overrideSuppressExStates As Boolean = True,
                        Optional newSuppressExStates As Boolean = False)

        If (e2eTestingRunning AndAlso e2eTester IsNot Nothing) Then
            e2eTester.SubTestException(ex.Message, ex.StackTrace)
        Else
            If Not newSuppressExStates Then
                If Not _suppressExStates Then
                    MsgBox(ex.Message)
                    MsgBox(ex.StackTrace)
                ElseIf Not overrideSuppressExStates Then
                    MsgBox(ex.Message)
                    MsgBox(ex.StackTrace)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Either displays argument exception prompts to the user, or records them in the e2eTester log, or does nothing depending on the state of the program.
    ''' </summary>
    ''' <param name="p_ex">Argument exception</param>
    ''' <param name="p_overrideSuppressExStates">If specified as 'false', the messagebox may still appear even if the logger property is set to 'true'.</param>
    ''' <param name="p_newSuppressExStates">If 'true', messages will be suppressed regardless of the logger property.</param>
    ''' <remarks></remarks>
    Sub ArgumentExceptionAction(ByVal p_ex As ArgumentException,
                                Optional ByVal p_overrideSuppressExStates As Boolean = True,
                                Optional p_newSuppressExStates As Boolean = False)

        If (e2eTestingRunning AndAlso e2eTester IsNot Nothing) Then
            e2eTester.SubTestException(p_ex.Message, p_ex.StackTrace)
        Else
            If Not p_newSuppressExStates Then
                If Not _suppressExStates Then
                    MsgBox(p_ex.Message)
                    MsgBox(p_ex.Source)
                    MsgBox(p_ex.StackTrace)
                ElseIf Not p_overrideSuppressExStates Then
                    MsgBox(p_ex.Message)
                    MsgBox(p_ex.Source)
                    MsgBox(p_ex.StackTrace)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Either displays a custom message box to the user, or records the message in the e2eTester log, or does nothing depending on the state of the program.
    ''' </summary>
    ''' <param name="message">Message to have displayed in the message box.</param>
    ''' <remarks></remarks>
    Sub CustomMessageAction(ByVal message As String)

        If (e2eTestingRunning AndAlso e2eTester IsNot Nothing) Then
            e2eTester.SubTestExceptionCustom(message)
        Else
            If Not _suppressExStates Then
                MsgBox(message)
            End If
        End If
    End Sub
#End Region




End Class
