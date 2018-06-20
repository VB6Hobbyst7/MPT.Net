Option Strict On
Option Explicit On

Imports System.Windows

Public Class MessengerPromptListener
    Inherits MessengerListener
      
    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

    ''' <summary>
    ''' Subscribes the listener to the provided object and displays a standard message box.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeListenerToMessageBox(ByVal subscriberEvent As IMessengerEvent)
        AddHandler subscriberEvent.Messenger, AddressOf ReportMessageToMessageBox
    End Sub

    ''' <summary>
    ''' Unsubscribes the listener from the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub UnsubscribeListenerToMessageBox(ByVal subscriberEvent As IMessengerEvent)
        RemoveHandler subscriberEvent.Messenger, AddressOf ReportMessageToMessageBox
    End Sub

    ''' <summary>
    ''' Subscribes shared classes to the listener and displays a standard message box.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeLibraryListenerToMessageBox()
        AddHandler MessengerPrompt.Message, AddressOf ReportMessageToMessageBox
    End Sub

    ''' <summary>
    ''' Writes the messenger title and message to the console.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Shared Sub ReportMessageToMessageBox(ByVal e As MessengerEventArgs)
        If Not e.Handled Then
            Select Case MessageBox.Show(GetMessage(e), GetTitle(e), ToMessageBox(e.ActionSet), ToMessageBox(e.MessageType))
                Case MessageBoxResult.Cancel
                    e.Action = eMessageActions.Cancel
                Case MessageBoxResult.No
                    e.Action = eMessageActions.No
                Case MessageBoxResult.None
                    e.Action = eMessageActions.None
                Case MessageBoxResult.OK
                    e.Action = eMessageActions.OK
                Case MessageBoxResult.Yes
                    e.Action = eMessageActions.Yes
                Case Else
                    e.Action = eMessageActions.None
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Converts the message enum to the corresponding MessageBox enum.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function ToMessageBox(ByVal value As eMessageActionSets) As MessageBoxButton
        Select Case value
            Case eMessageActionSets.AbortRetryIgnore
                Return MessageBoxButton.YesNoCancel
            Case eMessageActionSets.None
                Return MessageBoxButton.OK
            Case eMessageActionSets.OkCancel
                Return MessageBoxButton.OKCancel
            Case eMessageActionSets.OkOnly
                Return MessageBoxButton.OK
            Case eMessageActionSets.RetryCancel
                Return MessageBoxButton.OKCancel
            Case eMessageActionSets.YesNo
                Return MessageBoxButton.YesNo
            Case eMessageActionSets.YesNoCancel
                Return MessageBoxButton.YesNoCancel
            Case Else
                Return MessageBoxButton.OK
        End Select
    End Function

    ''' <summary>
    ''' Converts the message enum to the corresponding MessageBox enum.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function ToMessageBox(ByVal value As eMessageType) As MessageBoxImage
        Select Case value
            Case eMessageType.Asterisk
                Return MessageBoxImage.Asterisk
            Case eMessageType.Error
                Return MessageBoxImage.Error
            Case eMessageType.Exclamation
                Return MessageBoxImage.Exclamation
            Case eMessageType.Hand
                Return MessageBoxImage.Hand
            Case eMessageType.Information
                Return MessageBoxImage.Information
            Case eMessageType.None
                Return MessageBoxImage.None
            Case eMessageType.Question
                Return MessageBoxImage.Question
            Case eMessageType.Stop
                Return MessageBoxImage.Stop
            Case eMessageType.Warning
                Return MessageBoxImage.Warning
            Case Else
                Return MessageBoxImage.None
        End Select
    End Function
End Class
