Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.Reporting

''' <summary>
''' Class which stores a collection of one Excel result object. It also has special methods for creating and removing entries.
''' </summary>
''' <remarks></remarks>
Public Class cMCResultsExcel
    Inherits System.Collections.CollectionBase

    Implements ICloneable
    Implements INotifyPropertyChanged
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#Region "Variables"
    Protected _mcModel As cMCModel
#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property file() As cFileExcelResult
        Get
            Return GetItem()
        End Get
    End Property

    ''' <summary>
    ''' Returns the file path to the element.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property filePath() As String
        Get
            Return GetItem().pathDestination.path
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()

    End Sub
    ''' <summary>
    ''' Initializes a new collection that is set to reference the provided model control object.
    ''' </summary>
    ''' <param name="p_mcModel">Model control object to reference.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal p_mcModel As cMCModel)
        Bind(p_mcModel)
    End Sub

#End Region

#Region "Methods: Friend - Collection"
    ''' <summary>
    ''' Adds a new Excel result object to the list if one does not currently exist.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_item As cFileExcelResult) As Boolean
        Try
            If p_item Is Nothing Then Return False

            If InnerList.Count = 0 Then
                InnerList.Add(p_item)
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))

                'Set binding to added item (not before adding, as that will change the object outside!)
                Dim newItem As cFileExcelResult = CType(InnerList.Item(InnerList.Count - 1), cFileExcelResult)
                newItem.Bind(_mcModel)

                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function


    ''' <summary>
    ''' Removes the Excel result.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove()
        Try
            InnerList.RemoveAt(0)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("item"))
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub

    ''' <summary>
    ''' Replaces the current Excel result object.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_item As cFileExcelResult)
        Try
            If p_item Is Nothing Then Exit Sub

            If InnerList.Count = 0 Then
                InnerList.Add(p_item)
            Else
                InnerList(0) = p_item
            End If

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
    Friend Function ToObservableCollection() As ObservableCollection(Of cFileExcelResult)
        Dim templist As New ObservableCollection(Of cFileExcelResult)

        For Each item As cFileExcelResult In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cFileExcelResult)
        Dim templist As New List(Of cFileExcelResult)

        For Each item As cFileExcelResult In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCResultsExcel

        With myClone
            For Each item As cFileExcelResult In InnerList
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
        If Not (TypeOf p_object Is cMCResultsExcel) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCResultsExcel = TryCast(p_object, cMCResultsExcel)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        For Each excelResultOuter As cFileExcelResult In comparedObject
            isMatch = False
            For Each excelResultInner As cFileExcelResult In InnerList
                If excelResultOuter.Equals(excelResultInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Associates the entire list with a particular model control file.
    ''' </summary>
    ''' <param name="p_bindTo">Model control file object to bind to the item.</param>
    ''' <remarks></remarks>
    Friend Sub Bind(ByVal p_bindTo As cMCModel)
        _mcModel = p_bindTo
        For Each excelResult As cFileExcelResult In InnerList
            excelResult.Bind(p_bindTo)
        Next
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem() As cFileExcelResult
        'If Not _resultExcel.Count > 0 Then           
        '    _resultExcel.Add(New cMCResultExcel)
        'End If
        If InnerList.Count > 0 Then
            Return CType(InnerList(0), cFileExcelResult)
        Else
            Return Nothing
        End If
    End Function
#End Region

End Class
