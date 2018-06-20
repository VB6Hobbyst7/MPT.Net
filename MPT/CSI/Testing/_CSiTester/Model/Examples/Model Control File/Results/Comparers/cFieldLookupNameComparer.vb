Option Strict On
Option Explicit On

''' <summary>
''' Compares by name of the lookup field.
''' </summary>
''' <remarks></remarks>
Public Class cFieldLookupNameComparer
    Inherits Comparer(Of cFieldLookup)

    Public Overrides Function Compare(x As cFieldLookup, y As cFieldLookup) As Integer
        Return x.name.CompareTo(y.name)
    End Function
End Class
