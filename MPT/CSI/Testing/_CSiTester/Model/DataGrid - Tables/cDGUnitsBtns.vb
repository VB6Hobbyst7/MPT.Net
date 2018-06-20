Option Explicit On
Option Strict On


Imports MPT.Units

Public Class cDGUnitsBtns

#Region "Properties: Friend"
    Friend Property columnHeaders As List(Of String) = New List(Of String)

    Friend Property columnUnits As List(Of cUnitsController) = New List(Of cUnitsController)

#End Region

#Region "Methods: Friend"
    Friend Sub AddHeader(ByVal p_header As String)
        columnHeaders.Add(p_header)
        columnUnits.Add(New cUnitsController)
    End Sub
#End Region


End Class
