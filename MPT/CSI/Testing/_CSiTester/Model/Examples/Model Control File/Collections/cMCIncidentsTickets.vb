Option Explicit On
Option Strict On

''' <summary>
''' Class which stores a collection of incident or ticket objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCIncidentsTickets
    Inherits cObsColUniqueInteger

    Friend Overloads Sub Add(ByVal p_item As Integer)
        If Not p_item = 0 Then MyBase.Add(p_item)
    End Sub

    Friend Overrides Function Clone() As Object
        Dim myClone As New cMCIncidentsTickets

        With myClone
            For Each item As Integer In InnerList
                .Add(item)
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cMCIncidentsTickets) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCIncidentsTickets = TryCast(p_object, cMCIncidentsTickets)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each incidentOuter As Integer In comparedObject
            If Not InnerList.Contains(incidentOuter) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns 'True' if the collection contains the provided object, based on the comparer provided.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_item As Integer) As Boolean
        Try
            Return InnerList.Contains(p_item)
        Catch ex As Exception
            ' If an incompatible comparer is passed in, it should also return false.
        End Try
        Return False
    End Function
End Class
