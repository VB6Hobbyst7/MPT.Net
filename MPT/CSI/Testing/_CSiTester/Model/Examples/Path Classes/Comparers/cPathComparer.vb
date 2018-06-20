Option Explicit On
Option Strict On

''' <summary>
''' Compares by only the properties of the base class cPath.
''' </summary>
''' <remarks></remarks>
Public Class cPathComparer
    Inherits Comparer(Of cPath)

    Public Overrides Function Compare(x As cPath, y As cPath) As Integer
        If Not x.path = y.path Then Return x.path.CompareTo(y.path)
        If Not x.pathChildStub = y.pathChildStub Then Return x.pathChildStub.CompareTo(y.pathChildStub)

        Return 0
    End Function
End Class
