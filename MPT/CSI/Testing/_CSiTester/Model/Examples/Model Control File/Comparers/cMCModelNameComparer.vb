Option Strict On
Option Explicit On

''' <summary>
''' Compares two model control objects based on the model file name.
''' </summary>
''' <remarks></remarks>
Public Class cMCModelNameComparer
    Inherits Comparer(Of cMCModel)

    Public Overrides Function Compare(x As cMCModel, y As cMCModel) As Integer
        Return x.modelFile.pathDestination.fileName.CompareTo(y.modelFile.pathDestination.fileName)
    End Function
End Class
