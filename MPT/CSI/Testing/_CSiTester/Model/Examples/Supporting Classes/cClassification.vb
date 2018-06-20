Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

''' <summary>
''' Class for creating various example classifications that are to be added to the keywords list.
''' </summary>
''' <remarks></remarks>
Public Class cClassification
#Region "Variables"
    Dim keywordEntry As cKeyword
#End Region

#Region "Properties"
    Public Property name As String
    Public Property nameNode As String
    Public Property description As String
    Public Property subClassification As ObservableCollection(Of cKeyword)
#End Region

#Region "Initialization"
    Friend Sub New()
        subClassification = New ObservableCollection(Of cKeyword)

    End Sub
#End Region

#Region "Methods"
    Private Sub AddEntry()
        keywordEntry = New cKeyword
        subClassification.Add(keywordEntry)
    End Sub
#End Region

End Class
