Option Strict On
Option Explicit On

''' <summary>
''' Compares by query object equality, then by query ID.
''' </summary>
''' <remarks></remarks>
Public Class cResultQueryComparer
    Inherits Comparer(Of cMCResultBasic)

    Public Overrides Function Compare(x As cMCResultBasic, y As cMCResultBasic) As Integer
        If x.query.Equals(y.query) Then
            Return 0
        Else
            Return x.query.ID.CompareTo(y.query.ID)
        End If
    End Function
End Class
