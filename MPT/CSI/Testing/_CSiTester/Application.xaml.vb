Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    ''' <summary>
    ''' Array containing the command line arguments.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared mArgs As String()

    ''' <summary>
    ''' Events that occur when the application first starts up are handled here, such as passing in command line arguments.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Application_Startup(sender As Object, e As StartupEventArgs)
        'Handle command line arguments
        If e.Args.Length > 0 Then
            mArgs = e.Args
        End If
    End Sub

End Class
