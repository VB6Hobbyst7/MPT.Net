Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of strings that are unique. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cObsColUniqueString
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
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As String
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified string.
    ''' </summary>
    ''' <param name="p_name"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_name As String) As String
        Get
            Return GetItem(p_name)
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
    Friend Overloads Sub Add(ByVal p_items As IList(Of String))
        If p_items Is Nothing Then Exit Sub

        For Each item As String In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of String))
        If p_items Is Nothing Then Exit Sub

        For Each item As String In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new string item to the list if it is unique and not empty.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As String)
        Try
            Dim itemUnique As Boolean = True

            For Each item As String In InnerList
                If item = p_item Then
                    itemUnique = False
                    Exit For
                End If
            Next

            If (itemUnique AndAlso Not String.IsNullOrEmpty(p_item)) Then
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
    ''' Removes the item that has the specified name.
    ''' </summary>
    ''' <param name="p_name">Name that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_name As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of String)

            If String.IsNullOrEmpty(p_name) Then Throw New ArgumentException("p_name is not specified.")

            For Each stringItem As String In InnerList
                If Not stringItem = p_name Then
                    tempList.Add(stringItem)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As String In tempList
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
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As IList(Of String))
        InnerList.Clear()
        Add(p_items)
    End Sub


    ''' <summary>
    ''' Sorts the list.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Sort()
        Dim sortList As List(Of String) = Me.ToList
        sortList.Sort()

        InnerList.Clear()
        For Each item As String In sortList
            InnerList.Add(item)
        Next
    End Sub

    ''' <summary>
    ''' True: The list contains the provided item.
    ''' </summary>
    ''' <param name="p_item">String item to check.</param>
    ''' <param name="p_ignoreCase">True: Case will be ignored in matching strings.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_item As String,
                             Optional ByVal p_ignoreCase As Boolean = True) As Boolean
        For Each item As String In InnerList
            If String.Compare(p_item, item, p_ignoreCase) = 0 Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Returns the collection of strings as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of String)
        Dim templist As New ObservableCollection(Of String)

        For Each item As String In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection of strings as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of String)
        Dim templist As New List(Of String)

        For Each item As String In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cObsColUniqueString

        With myClone
            For Each item As String In InnerList
                .Add(item)
            Next
        End With

        Return myClone
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As String
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), String)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by name.
    ''' </summary>
    ''' <param name="p_name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_name As String) As String
        Try
            If String.IsNullOrEmpty(p_name) Then Throw New ArgumentException("p_name is not specified.")

            For Each stringItem As String In InnerList
                If stringItem = p_name Then
                    Return stringItem
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
