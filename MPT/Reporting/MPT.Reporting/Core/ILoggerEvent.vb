Option Strict On
Option Explicit On

''' <summary>
''' Interface for objects that throw log events.
''' </summary>
''' <remarks></remarks>
Public Interface ILoggerEvent
    ''' <summary>
    ''' To be used for sending error messages that are not meant for the user. 
    ''' Captured events are likely to be sent to the console, log file, or only displayed to the user when debugging.
    ''' A common case is when an exception is caught where no action is taken, or where an exception would normally be thrown to short-cicruit a method.
    ''' </summary>
    ''' <param name="exception"></param>
    ''' <remarks></remarks>
    Event Log(exception As LoggerEventArgs)
End Interface
