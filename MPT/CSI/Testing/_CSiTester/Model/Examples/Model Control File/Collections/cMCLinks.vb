Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of internet link objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCLinks
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
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cMCLink
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified URL.
    ''' </summary>
    ''' <param name="p_url"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_url As String) As cMCLink
        Get
            Return GetItem(p_url)
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
    Friend Overloads Sub Add(ByVal p_items As List(Of cMCLink))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCLink In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cMCLink))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCLink In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new internet link item to the list if it is unique in both the URL and title.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As cMCLink)
        Try
            Dim itemUnique As Boolean = True

            If p_item Is Nothing Then Exit Sub

            For Each item As cMCLink In InnerList
                If (StringsMatch(item.URL, p_item.URL) OrElse
                    StringsMatch(item.title, p_item.title)) Then

                    itemUnique = False
                    Exit For
                End If
            Next

            If itemUnique Then
                InnerList.Add(p_item)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub


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
    ''' Removes the item that has the specified web URL.
    ''' </summary>
    ''' <param name="p_url">Web URL that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_url As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCLink)

            If String.IsNullOrWhiteSpace(p_url) Then Throw New ArgumentException("p_url is not specified.")

            For Each link As cMCLink In InnerList
                If Not StringsMatch(link.URL, p_url) Then
                    tempList.Add(link)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCLink In tempList
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
    ''' Replaces the matching internet link object with the one provided. 
    ''' A match is determined by either title or the URL.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Replace(ByVal p_item As cMCLink) As Boolean
        Try
            If p_item Is Nothing Then Return False
            Dim index As Integer = 0
            Dim linksComparer As New cMCLinkComparer

            For Each item As cMCLink In InnerList
                If linksComparer.Compare(item, p_item) = 0 Then Exit For
                index += 1
            Next
            If index < InnerList.Count Then
                InnerList(index) = p_item
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                Return True
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
    Friend Overloads Sub Replace(ByVal p_items As List(Of cMCLink))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cMCLink))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns 'True' if the collection contains the provided object, based on the comparer provided.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_item As cMCLink,
                             ByVal p_comparer As IComparer) As Boolean
        Try
            For Each item As cMCLink In InnerList
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
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCLink)
        Dim templist As New ObservableCollection(Of cMCLink)

        For Each item As cMCLink In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCLink)
        Dim templist As New List(Of cMCLink)

        For Each item As cMCLink In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCLinks

        With myClone
            For Each item As cMCLink In InnerList
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
        If Not (TypeOf p_object Is cMCLinks) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCLinks = TryCast(p_object, cMCLinks)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each linkOuter As cMCLink In comparedObject
            isMatch = False
            For Each linkInner As cMCLink In InnerList
                If Not linkOuter.Equals(linkInner) Then
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
    Private Overloads Function GetItem(ByVal p_index As Integer) As cMCLink
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cMCLink)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by internet URL.
    ''' </summary>
    ''' <param name="p_url"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_url As String) As cMCLink
        Try
            If String.IsNullOrWhiteSpace(p_url) Then Throw New ArgumentException("p_url is not specified.")

            For Each link As cMCLink In InnerList
                If StringsMatch(link.URL, p_url) Then
                    Return link
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

        Return Nothing
    End Function
#End Region

End Class
