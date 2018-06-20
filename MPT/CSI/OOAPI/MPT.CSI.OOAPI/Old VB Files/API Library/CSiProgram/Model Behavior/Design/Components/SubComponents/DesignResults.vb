Option Strict On
Option Explicit On

#If COMPILE_ETABS2013 Then
Imports ETABS2013
#ElseIf COMPILE_ETABS2014 Then
Imports ETABS2014
#ElseIf COMPILE_SAP2000v16 Then
Imports SAP2000v16
#ElseIf COMPILE_SAP2000v17 Then
Imports SAP2000v17
#End If

'TODO: Inherit from a collection class.

''' <summary>
''' Collection of design results for each frame object associated with this object.
''' </summary>
''' <remarks></remarks>
Public Class DesignResults

#Region "Properties"
    ''' <summary>
    ''' Name of an existing frame object or group, depending on the value of the ItemType item.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemName As String

    Protected _ItemResults As New List(Of DesignResult)
    ''' <summary>
    ''' Collection of design results for each frame object associated with this object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemResults As List(Of DesignResult)
        Get
            Return _ItemResults
        End Get
    End Property


    ''' <summary>
    ''' Selection type used for obtaining the results.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemType As eItemType = eItemType.Object
#End Region

#Region "Methods: Public"
    Public Overridable Sub AddResults(ByVal designResults As DesignResults)
        For Each designResult As DesignResult In designResults.ItemResults
            AddResult(designResult)
        Next
    End Sub

    Public Overridable Sub AddResult(ByVal designResult As DesignResult)
        _ItemResults.Add(designResult)
    End Sub

    Public Function FrameNames() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResult In ItemResults
            values.Add(result.FrameName)
        Next

        Return values
    End Function

    Public Function ErrorSummaries() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResult In ItemResults
            values.Add(result.ErrorSummary)
        Next

        Return values
    End Function

    Public Function WarningSummaries() As List(Of String)
        Dim values As New List(Of String)
        For Each result As DesignResult In ItemResults
            values.Add(result.WarningSummary)
        Next

        Return values
    End Function
#End Region

End Class
