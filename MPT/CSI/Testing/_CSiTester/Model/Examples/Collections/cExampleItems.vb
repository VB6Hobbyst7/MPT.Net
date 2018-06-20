Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Reporting

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class cExampleItems
    'TODO: Consider if this is necessary or should be absorbed into cMCModel results.
    'TODO: Create based on other collection classes.
    'TODO: Look for methods to bring in, likely from cExample.

    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Properties"

#End Region

#Region "Properties - List"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cExampleItem
        Get
            Return GetItem(p_index)
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cExampleItems

        With myClone
            For Each item As cExampleItem In InnerList
                .Add(item)
            Next
        End With

        Return myClone
    End Function

    'TODO: Review
    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cExampleItems) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cExampleItems = TryCast(p_object, cExampleItems)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each updateOuter As cExampleItem In comparedObject
            isMatch = False
            For Each updateInner As cExampleItem In InnerList
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

#Region "Methods: Friend - List"
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ICollection(Of cExampleItem))
        If p_items Is Nothing Then Exit Sub

        For Each item As cExampleItem In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds an item to the list if it is unique.
    ''' </summary>
    ''' <param name="p_object"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_object As cExampleItem) As Boolean
        Try
            If p_object Is Nothing Then Return False

            'If update entry is unique, add it
            If MCModelUnique(p_object) Then
                InnerList.Add(p_object)
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
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCModel)
        Return TryCast(ToCollection(), ObservableCollection(Of cMCModel))
    End Function
    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCModel)
        Return TryCast(ToCollection(), List(Of cMCModel))
    End Function
    ''' <summary>
    ''' Returns the collection object as any collection type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToCollection() As ICollection(Of cMCModel)
        Dim templist As New ObservableCollection(Of cMCModel)

        For Each item As cMCModel In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function
#End Region

#Region "Methods: Private - List"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cExampleItem
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cExampleItem)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Determines if the provided object is unique to the list of the current list.
    ''' </summary>
    ''' <param name="p_mcModel">Object to check for uniqueness.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MCModelUnique(ByVal p_mcModel As cExampleItem) As Boolean
        'Dim mcModelIDs As New ObservableCollection(Of String)

        'For Each mcModel As cExampleItem In InnerList
        '    mcModelIDs.Add(mcModel.id.idComposite)
        'Next

        ''If update entry is unique, add it
        'If Not ExistsInListString(p_mcModel.id.idComposite, mcModelIDs) Then
        '    Return True
        'Else
        '    Return False
        'End If
        Return True
    End Function

#End Region
End Class
