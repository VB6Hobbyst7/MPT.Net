Option Strict On
Option Explicit On

''' <summary>
''' Exception resulting from example or model IDs.
''' </summary>
''' <remarks></remarks>
Public Class ModelIDException
    Inherits Exception

    Public Sub New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, inner As Exception)
        MyBase.New(message, inner)
    End Sub
End Class
