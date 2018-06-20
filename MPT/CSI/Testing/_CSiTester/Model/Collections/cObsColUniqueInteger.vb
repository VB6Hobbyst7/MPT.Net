Option Explicit On
Option Strict On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of integers. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cObsColUniqueInteger
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
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As Integer
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified integer.
    ''' </summary>
    ''' <param name="p_number"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_number As String) As Integer
        Get
            Return GetItem(p_number)
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
    Friend Overloads Sub Add(ByVal p_items As List(Of Integer))
        If p_items Is Nothing Then Exit Sub

        For Each item As Integer In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of Integer))
        If p_items Is Nothing Then Exit Sub

        For Each item As Integer In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new integer item to the list if it is unique.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As Integer)
        Try
            Dim itemUnique As Boolean = True

            For Each item As Integer In InnerList
                If item = p_item Then
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
    ''' Removes the item that has the specified name.
    ''' </summary>
    ''' <param name="p_integer">Integer that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_integer As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of Integer)

            If String.IsNullOrEmpty(p_integer) Then Throw New ArgumentException("p_integer is not specified.")

            Dim integerRemove As Integer = CInt(p_integer)
            For Each integerItem As Integer In InnerList
                If Not integerItem = integerRemove Then
                    tempList.Add(integerItem)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As Integer In tempList
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
    Friend Overloads Sub Replace(ByVal p_items As List(Of Integer))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of Integer))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of Integer)
        Dim templist As New ObservableCollection(Of Integer)

        For Each item As Integer In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of Integer)
        Dim templist As New List(Of Integer)

        For Each item As Integer In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Overridable Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cObsColUniqueInteger

        With myClone
            For Each item As Integer In InnerList
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
    Private Overloads Function GetItem(ByVal p_index As Integer) As Integer
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), Integer)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by name.
    ''' </summary>
    ''' <param name="p_integer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_integer As String) As Integer
        Try
            If String.IsNullOrEmpty(p_integer) Then Throw New ArgumentException("p_integer is not specified.")

            Dim integerGet As Integer = CInt(p_integer)
            For Each stringItem As Integer In InnerList
                If stringItem = integerGet Then
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
