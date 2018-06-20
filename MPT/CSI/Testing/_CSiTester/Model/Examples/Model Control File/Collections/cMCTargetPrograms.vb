Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of unique program types. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCTargetPrograms
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
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As eCSiProgram
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified type.
    ''' </summary>
    ''' <param name="p_program"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_program As eCSiProgram) As eCSiProgram
        Get
            Return GetItem(p_program)
        End Get
    End Property

    ''' <summary>
    ''' The primary program used among the target programs list. Taken as the first item in the list.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property primary As eCSiProgram
        Get
            If InnerList.Count > 0 Then
                Return CType(InnerList(0), eCSiProgram)
            Else
                Return eCSiProgram.None
            End If
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
    Friend Overloads Sub Add(ByVal p_items As List(Of eCSiProgram))
        If p_items Is Nothing Then Exit Sub

        For Each item As eCSiProgram In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of eCSiProgram))
        If p_items Is Nothing Then Exit Sub

        For Each item As eCSiProgram In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new program type to the list if it is unique.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As eCSiProgram)
        Try
            If p_item = eCSiProgram.None Then Exit Sub

            If (ItemIsUnique(p_item) AndAlso Not p_item = eCSiProgram.None) Then
                InnerList.Add(p_item)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    Friend Sub AddAsFirst(ByVal p_item As eCSiProgram)
        Try
            If p_item = eCSiProgram.None Then Exit Sub

            If (ItemIsUnique(p_item) AndAlso Not p_item = eCSiProgram.None) Then
                InnerList.Insert(0, p_item)
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
    ''' Removes the item that has the specified type.
    ''' </summary>
    ''' <param name="p_type">CSi program that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_type As eCSiProgram)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of eCSiProgram)

            If p_type = eCSiProgram.None Then Exit Sub

            For Each programType As eCSiProgram In InnerList
                If Not programType = p_type Then
                    tempList.Add(programType)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As eCSiProgram In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of eCSiProgram))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of eCSiProgram))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of eCSiProgram)
        Dim templist As New ObservableCollection(Of eCSiProgram)

        For Each item As eCSiProgram In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of eCSiProgram)
        Dim templist As New List(Of eCSiProgram)

        For Each item As eCSiProgram In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCTargetPrograms

        With myClone
            For Each item As eCSiProgram In InnerList
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
        If Not (TypeOf p_object Is cMCTargetPrograms) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCTargetPrograms = TryCast(p_object, cMCTargetPrograms)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each programOuter As eCSiProgram In comparedObject
            isMatch = False
            For Each programInner As eCSiProgram In InnerList
                If programOuter = programInner Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns 'true' if the program exists in the list of programs in the current object.
    ''' </summary>
    ''' <param name="p_program">Program to search for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Contains(ByVal p_program As eCSiProgram) As Boolean
        For Each program As eCSiProgram In InnerList
            If p_program = program Then Return True
        Next
        Return False
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As eCSiProgram
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), eCSiProgram)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    Private Function ItemIsUnique(ByVal p_item As eCSiProgram) As Boolean
        Dim itemUnique As Boolean = True

        For Each item As eCSiProgram In InnerList
            If item = p_item Then
                itemUnique = False
                Exit For
            End If
        Next

        Return itemUnique
    End Function
#End Region


End Class
