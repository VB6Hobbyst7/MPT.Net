Option Strict On
Option Explicit On

''' <summary>
''' Compares two model control objects based on the composite ID ([Example ID].[Model ID]).
''' </summary>
''' <remarks></remarks>
Public Class cMCIDComparer
    Inherits Comparer(Of cMCModel)

    Public Overrides Function Compare(x As cMCModel, y As cMCModel) As Integer
        Return x.ID.idComposite.CompareTo(y.ID.idComposite)
    End Function
End Class
