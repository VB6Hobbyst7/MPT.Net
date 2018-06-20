Option Strict On
Option Explicit On

''' <summary>
''' Compares two model control objects based on the example name.
''' </summary>
''' <remarks></remarks>
Public Class cMCExampleNameComparer
    Inherits Comparer(Of cMCModel)

    Public Overrides Function Compare(x As cMCModel, y As cMCModel) As Integer
        Return x.secondaryID.CompareTo(y.secondaryID)
    End Function
End Class
