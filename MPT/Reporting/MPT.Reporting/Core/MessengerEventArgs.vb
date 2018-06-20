Option Strict On
Option Explicit On

''' <summary>
''' Event argument that relays a message from the libraries to an external assembly.
''' </summary>
''' <remarks></remarks>
Public Class MessengerEventArgs
    Inherits EventArgs

    Private _messageData As New MessageData()
    ''' <summary>
    ''' Title of the message associated with the event.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Title As String
        Get
            Return _messageData.Title
        End Get
    End Property

    ''' <summary>
    ''' Message associated with the event.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Message As String
        Get
            Return _messageData.Message
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Footer As String
        Get
            Return _messageData.Footer
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PromptList As String
        Get
            Return _messageData.PromptList
        End Get
    End Property



    Private _messageDetails As New MessageDetails()
    ''' <summary>
    ''' If a console or message box prompt is used to display the message data, the choice can be recorded here.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Action As eMessageActions
        Get
            Return _messageDetails.Action
        End Get
        Set(value As eMessageActions)
            _messageDetails.Action = value
        End Set
    End Property

    ''' <summary>
    ''' Decision combination types to display with the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActionSet As eMessageActionSets
        Get
            Return _messageDetails.ActionSet
        End Get
    End Property

    ''' <summary>
    ''' Type of message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MessageType As eMessageType
        Get
            Return _messageDetails.MessageType
        End Get
    End Property

    ''' <summary>
    ''' Default action to take.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActionDefault As eMessageActions
        Get
            Return _messageDetails.ActionDefault
        End Get
    End Property



    ''' <summary>
    ''' True: The event has already been handled.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Handled As Boolean



    ''' <summary>
    ''' Initializer.
    ''' </summary>
    ''' <param name="message">Message associated with the event.</param>
    ''' <param name="arg">List of variables to insert into the message.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal message As String,
                   ParamArray arg() As Object)
        _messageData = New MessageData(message:=String.Format(message, arg))
    End Sub

    ''' <summary>
    ''' Initializer.
    ''' </summary>
    ''' <param name="message">Message associated with the event.</param>
    ''' <param name="arg">List of variables to insert into the message.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal title As String,
                   ByVal message As String,
                   ParamArray arg() As Object)
        _messageData = New MessageData(title:=title,
                                       message:=String.Format(message, arg))
    End Sub

    ''' <summary>
    ''' Initializer.
    ''' </summary>
    ''' <param name="message">Message associated with the event.</param>
    ''' <param name="arg">List of variables to insert into the message.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal messageDetails As MessageDetails,
                   ByVal message As String,
                   ParamArray arg() As Object)
        _messageDetails = messageDetails
        _messageData = New MessageData(message:=String.Format(message, arg))
    End Sub

    ''' <summary>
    ''' Initializer.
    ''' </summary>
    ''' <param name="messageData"></param>
    ''' <param name="messageDetails"></param>
    ''' <param name="arg">List of variables to insert into the message.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal messageDetails As MessageDetails,
                   ByVal messageData As MessageData,
                   ParamArray arg() As Object)
        _messageDetails = messageDetails
        _messageData = New MessageData(title:=messageData.Title,
                                       message:=String.Format(messageData.Message, arg),
                                       footer:=messageData.Footer,
                                       promptList:=messageData.PromptList)
    End Sub
End Class
