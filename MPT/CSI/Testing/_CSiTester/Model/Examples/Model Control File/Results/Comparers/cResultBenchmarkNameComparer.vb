Option Strict On
Option Explicit On

''' <summary>
''' Compares by name of the benchmark.
''' </summary>
''' <remarks></remarks>
Public Class cResultBenchmarkNameComparer
    Inherits Comparer(Of cMCResultBasic)

    Public Overrides Function Compare(x As cMCResultBasic, y As cMCResultBasic) As Integer
        Return x.benchmark.name.CompareTo(y.benchmark.name)
    End Function
End Class
