Option Explicit On
Option Strict On

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class MessengerListener
    ''ncrunch: no coverage start
    Protected Sub New()
        ' This class is meant to not be initialized and only have shared methods.
    End Sub
     ''ncrunch: no coverage end

    ''' <summary>
    ''' Subscribes the listener to the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub SubscribeListener(ByVal subscriberEvent As IMessengerEvent)
        AddHandler subscriberEvent.Messenger, AddressOf ReportMessageToConsole
    End Sub

    ''' <summary>
    ''' Unsubscribes the listener from the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub UnsubscribeListener(ByVal subscriberEvent As IMessengerEvent)
        RemoveHandler subscriberEvent.Messenger, AddressOf ReportMessageToConsole
    End Sub

    ''' <summary>
    ''' Writes the messenger title and message to the console.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Shared Sub ReportMessageToConsole(ByVal e As MessengerEventArgs)
        If Not e.Handled Then
            Console.WriteLine(GetTitle(e))
            Console.WriteLine(GetMessage(e))
        End If
    End Sub

    ''' <summary>
    ''' Assembles a title from the messenger object.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetTitle(ByVal e As MessengerEventArgs) As String
        Return e.Title
    End Function

    ''' <summary>
    ''' Assembles a message from the messenger object.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetMessage(ByVal e As MessengerEventArgs) As String
        Return e.Message
    End Function

End Class
