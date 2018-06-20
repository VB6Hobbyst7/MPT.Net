Option Explicit On
Option Strict On

Imports System.Timers

''' <summary>
''' Form for displaying the status of the program. Currently not used.
''' </summary>
''' <remarks></remarks>
Public Class frmStatusForm
    Dim myTimer As New Timer()

    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        formLabel()

        Dim myTimer As New Timer()
        myTimer.Interval = myStatusBox.milliseconds
        myTimer.Enabled = True
        myTimer.Start()

        ' Hook up the Elapsed event for the timer. 
        AddHandler myTimer.Elapsed, AddressOf OnTimedEvent
    End Sub

    Sub formLabel()

        labelStatus.Text = myStatusBox.label
    End Sub

    Private Delegate Sub CloseFormCallback()

    Private Sub CloseForm()
        If InvokeRequired Then
            Dim d As New CloseFormCallback(AddressOf CloseForm)
            Invoke(d, Nothing)
        Else
            'Close()
            Dispose()
        End If
    End Sub

    Private Sub OnTimedEvent(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        'Stops interval from occuring repeatedly
        myTimer.Stop()
        myTimer.Enabled = False

        CloseForm()
    End Sub

End Class