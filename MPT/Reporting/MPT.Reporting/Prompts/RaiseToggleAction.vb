Option Strict On
Option Explicit On

Public NotInheritable Class RaiseToggleAction
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

    ''' <summary>
    ''' Raises a messenger event indicating the start of a function.
    ''' </summary>
    Public Shared Sub StartAction(ByVal method As String)
        RaiseEvent Messenger(New MessengerEventArgs(method & " started."))
    End Sub

    ''' <summary>
    ''' Raises a messenger event indicating the end of a function
    ''' </summary>
    Public Shared Sub EndAction(ByVal method As String)
        RaiseEvent Messenger(New MessengerEventArgs(method & " completed."))
    End Sub
End Class
