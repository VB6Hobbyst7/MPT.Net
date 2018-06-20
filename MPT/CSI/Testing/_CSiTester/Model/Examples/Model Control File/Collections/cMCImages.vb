Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports CSiTester.cLibPath

''' <summary>
''' Class which stores a collection of image objects. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCImages
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#Region "Variables"
    Protected _images As ObservableCollection(Of cMCImage) = New ObservableCollection(Of cMCImage)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cMCImage
        Get
            Return GetItem(p_index)
        End Get
    End Property

    ''' <summary>
    ''' Gets the element of the specified file name.
    ''' </summary>
    ''' <param name="p_fileName"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_fileName As String) As cMCImage
        Get
            Return GetItem(p_fileName)
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
    Friend Overloads Sub Add(ByVal p_items As List(Of cMCImage))
        For Each item As cMCImage In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cMCImage))
        For Each item As cMCImage In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new image item to the list if it is unique in both the file name and title.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_item As cMCImage)
        Try
            Dim itemUnique As Boolean = True

            For Each item As cMCImage In _images
                If (StringsMatch(GetPathFileName(item.path), GetPathFileName(p_item.path)) OrElse
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
            csiLogger.ArgumentExceptionAction(argExc)
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
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _images.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub
    ''' <summary>
    ''' Removes the item that has the specified file name.
    ''' </summary>
    ''' <param name="p_fileName">File name that corresponds to the item to be removed.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_fileName As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCImage)

            If p_fileName = "" Then Throw New ArgumentException("p_fileName is not specified.")

            For Each image As cMCImage In InnerList
                If Not StringsMatch(GetPathFileName(image.path), p_fileName) Then
                    tempList.Add(image)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCImage In tempList
                    InnerList.Add(item)
                Next

                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
            End If
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the matching image object with the one provided. 
    ''' A match is determined by either title or the file name.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_item As cMCImage)
        Try
            For Each item As cMCImage In InnerList
                If (StringsMatch(GetPathFileName(item.path), GetPathFileName(p_item.path)) OrElse
                    StringsMatch(item.title, p_item.title)) Then

                    item = p_item
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
                    Exit For
                End If
            Next
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        End Try
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As List(Of cMCImage))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cMCImage))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCImage)
        Dim templist As New ObservableCollection(Of cMCImage)

        For Each item As cMCImage In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCImage)
        Dim templist As New List(Of cMCImage)

        For Each item As cMCImage In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCImages

        With myClone
            For Each item As cMCImage In InnerList
                .Add(item)
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided does not perfectly match the existing object.
    ''' </summary>
    ''' <param name="p_images">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDifferent(ByVal p_images As cMCImages) As Boolean
        Dim isMatch As Boolean = True
        Dim isMatchTemp As Boolean = False

        For Each imageOuter As cMCImage In p_images
            isMatchTemp = False
            For Each imageInner As cMCImage In InnerList
                If Not imageOuter.IsDifferent(imageInner) Then isMatchTemp = True
            Next
            If Not isMatchTemp Then isMatch = False
        Next

        Return isMatch
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cMCImage
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & _images.Count.ToString)

            Return CType(InnerList(p_index), cMCImage)
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the item specified by file name.
    ''' </summary>
    ''' <param name="p_fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_fileName As String) As cMCImage
        Try
            If p_fileName = "" Then Throw New ArgumentException("p_fileName is not specified.")

            For Each image As cMCImage In InnerList
                If StringsMatch(GetPathFileName(image.path), p_fileName) Then
                    Return image
                End If
            Next
        Catch argExc As ArgumentException
            csiLogger.ArgumentExceptionAction(argExc)
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return Nothing
    End Function
#End Region
End Class
