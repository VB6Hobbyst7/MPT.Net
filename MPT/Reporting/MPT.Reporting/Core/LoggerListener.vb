Option Explicit On
Option Strict On

''' <summary>
''' Sample base class for a listener object for the logger events.
''' </summary>
''' <remarks></remarks>
Public Class LoggerListener
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
    Public Shared Sub SubscribeListener(ByVal subscriberEvent As ILoggerEvent)
        AddHandler subscriberEvent.Log, AddressOf ReportLogToConsole
    End Sub

    ''' <summary>
    ''' Unsubscribes the listener from the provided object.
    ''' </summary>
    ''' <param name="subscriberEvent"></param>
    ''' <remarks></remarks>
    Public Shared Sub UnsubscribeListener(ByVal subscriberEvent As ILoggerEvent)
        RemoveHandler subscriberEvent.Log, AddressOf ReportLogToConsole
    End Sub

    ''' <summary>
    ''' Writes the log title and message to the console.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Shared Sub ReportLogToConsole(ByVal e As LoggerEventArgs)
        If Not e.Handled Then
            If Not String.IsNullOrEmpty(e.Title) Then
                Console.WriteLine(GetTitle(e))
                Console.WriteLine()
            End If
            Console.WriteLine(GetMessage(e))
        End If
    End Sub

    ''' <summary>
    ''' Assembles a title from the logger object.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetTitle(ByVal e As LoggerEventArgs) As String
        Return e.Title
    End Function

    ''' <summary>
    ''' Assembles a message from the logger object.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Shared Function GetMessage(ByVal e As LoggerEventArgs) As String
        Dim message As String = ""

        If e.Exception IsNot Nothing Then
            message &= e.Exception.Message & Environment.NewLine
            message &= e.Exception.StackTrace & Environment.NewLine
        End If

        If Not String.IsNullOrEmpty(e.Message) Then message &= e.Message & Environment.NewLine
        If Not e.Parameters.Count = 0 Then
            For Each parameterName As String In e.Parameters.Keys
                message &= parameterName & ": " & e.Parameters(parameterName) & Environment.NewLine
            Next
        End If

        Return message
    End Function
End Class
