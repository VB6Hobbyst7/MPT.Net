Option Explicit On
Option Strict On


Public Class MessageData

    Private _title As String
    ''' <summary>
    ''' Title of the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Title As String
        Get
            Return _title
        End Get
    End Property

    Private _message As String
    ''' <summary>
    ''' Message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Message As String
        Get
            Return _message
        End Get
    End Property

    Private _footer As String
    ''' <summary>
    ''' Footer to the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Footer As String
        Get
            Return _footer
        End Get
    End Property

    Private _promptList As String
    ''' <summary>
    ''' List content of the message.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PromptList As String
        Get
            Return _promptList
        End Get
    End Property

    Public Sub New(Optional ByVal title As String = "",
                   Optional ByVal message As String = "",
                   Optional ByVal footer As String = "",
                   Optional ByVal promptList As String = "")
        _title = title
        _message = message
        _footer = footer
        _promptList = promptList
    End Sub
End Class
