Option Strict On
Option Explicit On

''' <summary>
''' Interface for objects that throw messenger events.
''' </summary>
''' <remarks></remarks>
Public Interface IMessengerEvent
    ''' <summary>
    ''' To be used for sending informative messages to the user.
    ''' </summary>
    ''' <param name="messenger"></param>
    ''' <remarks></remarks>
    Event Messenger(messenger As MessengerEventArgs)
End Interface
