Option Strict On
Option Explicit On

Public Class cLibMisc
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

    Public Shared Function PointIsEmpty(ByVal p_point As Point) As Boolean
        If p_point.X = 0 AndAlso p_point.Y = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function IntPtrIsEmpty(ByVal p_intPtr As IntPtr) As Boolean
        If p_intPtr.ToInt32 = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
