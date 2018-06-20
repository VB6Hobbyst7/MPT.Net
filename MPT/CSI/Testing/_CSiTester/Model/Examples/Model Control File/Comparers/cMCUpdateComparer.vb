Option Strict On
Option Explicit On

''' <summary>
''' Compares two update objects based on the date, ticket, and comment.
''' Ticket is not considered if it is 0.
''' </summary>
''' <remarks></remarks>
Public Class cMCUpdateComparer
    Inherits Comparer(Of cMCUpdate)

    Public Overrides Function Compare(x As cMCUpdate, y As cMCUpdate) As Integer
        If Not x.updateDate.numYear.CompareTo(y.updateDate.numYear) = 0 Then
            Return x.updateDate.numYear.CompareTo(y.updateDate.numYear)

        ElseIf Not x.updateDate.numMonth.CompareTo(y.updateDate.numMonth) = 0 Then
            Return x.updateDate.numMonth.CompareTo(y.updateDate.numMonth)

        ElseIf Not x.updateDate.numDay.CompareTo(y.updateDate.numDay) = 0 Then
            Return x.updateDate.numDay.CompareTo(y.updateDate.numDay)

        ElseIf (Not x.ticket.CompareTo(y.ticket) = 0 AndAlso Not x.ticket = 0) Then
            Return x.ticket.CompareTo(y.ticket)

        ElseIf Not x.comment.CompareTo(y.comment) = 0 Then
            Return x.comment.CompareTo(y.comment)

        Else
            Return 0
        End If
    End Function
End Class
