Option Explicit On
Option Strict On


Public Class MessageDetails
    ''' <summary>
    ''' If a console or message box prompt is used to display the message data, the choice can be recorded here.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Action As eMessageActions = eMessageActions.None

    Private _actionSet As eMessageActionSets = eMessageActionSets.OkOnly
    ''' <summary>
    ''' Decision combination types to display with the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActionSet As eMessageActionSets
        Get
            Return _actionSet
        End Get
    End Property

    Private _messageType As eMessageType = eMessageType.None
    ''' <summary>
    ''' Type of message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MessageType As eMessageType
        Get
            Return _messageType
        End Get
    End Property

    Private _actionDefault As eMessageActions = eMessageActions.None
    ''' <summary>
    ''' Default action to take.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ActionDefault As eMessageActions
        Get
            Return _actionDefault
        End Get
    End Property

    Public Sub New(Optional ByVal messageActionSet As eMessageActionSets = eMessageActionSets.OkOnly,
                   Optional ByVal messageType As eMessageType = eMessageType.None,
                   Optional messageDefault As eMessageActions = eMessageActions.None)
        _messageType = messageType
        _actionSet = messageActionSet
        _actionDefault = messageDefault
    End Sub
End Class
