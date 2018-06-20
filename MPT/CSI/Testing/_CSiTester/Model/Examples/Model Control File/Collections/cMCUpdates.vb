Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Lists.ListLibrary
Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of update objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCUpdates
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cMCUpdate
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified non-zero ticket number.
    ''' </summary>
    ''' <param name="p_ticket"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_ticket As String) As cMCUpdate
        Get
            Return GetItem(p_ticket)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
    End Sub

    Private Sub InitializeData()

    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As List(Of cMCUpdate))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCUpdate In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cMCUpdate))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCUpdate In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new attachment item to the list if it is unique by date and either ticket or comment.
    ''' </summary>
    ''' <param name="p_update"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_update As cMCUpdate) As Boolean
        Try
            If p_update Is Nothing Then Return False

            'If update entry is unique, add it
            If UpdateObjectUnique(p_update) Then
                p_update.id = GetUpdateIDNext()
                InnerList.Add(p_update)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Removes the item at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the item that has the specified ticket number.
    ''' </summary>
    ''' <param name="p_ticket">Ticket that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_ticket As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCUpdate)

            If String.IsNullOrWhiteSpace(p_ticket) Then Throw New ArgumentException("p_ticket is not specified.")
            If p_ticket = "0" Then Throw New ArgumentException("p_ticket cannot be 0 for removal. Updates without tickets use this, so it is not a unique identifier.")

            For Each item As cMCUpdate In InnerList
                If Not item.ticket.ToString = p_ticket Then
                    tempList.Add(item)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCUpdate In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the matching update object with the one provided. 
    ''' A match is determined by date and either ticket or comment.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Replace(ByVal p_item As cMCUpdate) As Boolean
        Try
            If p_item Is Nothing Then Return False
            Dim index As Integer = 0

            If InnerList.Count = 0 Then
                InnerList.Add(p_item)
                Return True
            Else
                Dim updateComparer As New cMCUpdateComparer
                For Each item As cMCUpdate In InnerList
                    If updateComparer.Compare(item, p_item) = 0 Then
                        Exit For
                    End If
                    index += 1
                Next
                If index < InnerList.Count Then
                    InnerList(index) = p_item
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                    Return True
                End If
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of cMCUpdate))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cMCUpdate))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns 'True' if the collection contains the provided object, based on the comparer provided.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_item As cMCUpdate,
                             ByVal p_comparer As IComparer) As Boolean
        Try
            For Each item As cMCUpdate In InnerList
                If p_comparer.Compare(item, p_item) = 0 Then
                    Return True
                End If
            Next
        Catch ex As Exception
            ' If an incompatible comparer is passed in, it should also return false.
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Returns the what should be the next ID number for an update to the example.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetUpdateIDNext() As Integer
        Dim idNext As Integer = 0

        For Each update As cMCUpdate In InnerList
            If update.id > idNext Then idNext = update.id
        Next

        If InnerList.Count > 0 Then idNext += 1

        Return idNext
    End Function

    ''' <summary>
    ''' Updates all update object IDs to be in ascending order in order of the date recorded. 
    ''' Returns a dictionary of the old ID and the corresponding new ID in order to sync changes elsewhere.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdateIDs() As Dictionary(Of Integer, Integer)
        Dim oldNewIdentifiers As New Dictionary(Of Integer, Integer)
        Dim oldIdentifiers As New List(Of Integer)
        Dim idLast As Integer = 0

        If UpdatesAllHaveUniqueIDs() Then Return oldNewIdentifiers

        InnerList.Sort()

        For Each update As cMCUpdate In InnerList
            oldIdentifiers.Add(update.id)
            update.id = idLast
            idLast += 1
        Next

        For i = 0 To InnerList.Count - 1
            oldNewIdentifiers.Add(oldIdentifiers(i), CType(InnerList(i), cMCUpdate).id)
        Next

        Return oldNewIdentifiers
    End Function

    ''' <summary>
    ''' Determines if all of the update objects have valid IDs. In order to be valid, all IDs must be unique.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function UpdatesAllHaveUniqueIDs() As Boolean
        Dim idsList As New List(Of Integer)
        Dim idsListUnique As New List(Of Integer)

        For Each update As cMCUpdate In InnerList
            idsList.Add(update.id)
        Next

        For Each id As Integer In idsList
            idsListUnique.Add(id)
        Next

        idsListUnique = ConvertToUniqueList(idsListUnique)

        If idsListUnique.Count = idsList.Count Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCUpdate)
        Dim templist As New ObservableCollection(Of cMCUpdate)

        For Each item As cMCUpdate In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCUpdate)
        Dim templist As New List(Of cMCUpdate)

        For Each item As cMCUpdate In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCUpdates

        With myClone
            For Each item As cMCUpdate In InnerList
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
        If Not (TypeOf p_object Is cMCUpdates) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCUpdates = TryCast(p_object, cMCUpdates)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each updateOuter As cMCUpdate In comparedObject
            isMatch = False
            For Each updateInner As cMCUpdate In InnerList
                If Not updateOuter.Equals(updateInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cMCUpdate
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cMCUpdate)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by ticket number.
    ''' </summary>
    ''' <param name="p_ticket"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_ticket As String) As cMCUpdate
        Try
            If String.IsNullOrWhiteSpace(p_ticket) Then Throw New ArgumentException("p_ticket is not specified.")
            If p_ticket = "0" Then Throw New ArgumentException("p_ticket cannot be 0.")

            For Each item As cMCUpdate In InnerList
                If item.ticket.ToString = p_ticket Then
                    Return item
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Determines if the provided update object is unique to the list of the current list.
    ''' </summary>
    ''' <param name="p_update">Update object to check for uniqueness.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateObjectUnique(ByVal p_update As cMCUpdate) As Boolean
        Dim updateComparer As New cMCUpdateComparer
        For Each update As cMCUpdate In InnerList
            If updateComparer.Compare(update, p_update) = 0 Then Return False
        Next
        Return True
    End Function

#End Region
End Class
