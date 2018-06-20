Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel
Imports System.ComponentModel

Imports MPT.FileSystem.PathLibrary
Imports MPT.Lists.ListLibrary
Imports MPT.PropertyChanger
Imports MPT.Reporting
Imports MPT.String.ConversionLibrary

Imports CSiTester.cMCModel

''' <summary>
''' Class which stores a collection of unique result objects. 
''' It also has special methods for creating and removing entries and adjusting result IDs.
''' </summary>
''' <remarks></remarks>
Public Class cMCResults
    Inherits PropertyChangerCollections
    Implements ICloneable
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log

#Region "Properties"
    ''' <summary>
    ''' Gets the element at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_index As Integer) As cMCResultBasic
        Get
            Return GetItem(p_index)
        End Get
    End Property
    ''' <summary>
    ''' Gets the element at the specified result ID.
    ''' </summary>
    ''' <param name="p_resultID"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Default Friend Overloads ReadOnly Property item(ByVal p_resultID As String) As cMCResultBasic
        Get
            Return GetItem(p_resultID)
        End Get
    End Property

    ''' <summary>
    ''' An observable collection of all of the result objects of the regular type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property resultsRegular() As List(Of cMCResult)
        Get
            Return GetItemsByType(Of cMCResult)(eResultType.regular)
        End Get
    End Property

    ''' <summary>
    ''' An observable collection of all of the result objects of the Excel type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property resultsExcel() As List(Of cMCResultBasic)
        Get
            Return GetItemsByType(Of cMCResultBasic)(eResultType.excelCalculated)
        End Get
    End Property

    ''' <summary>
    ''' An observable collection of all of the result objects of the post-processed type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property resultsPostProcessed() As List(Of cMCResultPostProcessed)
        Get
            Return GetItemsByType(Of cMCResultPostProcessed)(eResultType.postProcessed)
        End Get
    End Property

    ''' <summary>
    ''' An observable collection of all of the result objects of the regular and post-processed type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property resultsNotExcel() As List(Of cMCResult)
        Get
            Dim results As List(Of cMCResult) = GetItemsByType(Of cMCResult)(eResultType.regular)
            Dim resultsPostProcessed As List(Of cMCResultPostProcessed) = GetItemsByType(Of cMCResultPostProcessed)(eResultType.postProcessed)

            For Each result As cMCResult In resultsPostProcessed
                results.Add(result)
            Next

            Return results
        End Get
    End Property

#End Region

#Region "Initialization"
    Friend Sub New()
    End Sub
#End Region

#Region "Methods: Overloads, Overrides, Implements"
    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cMCResults

        With myClone
            For Each item As cMCResultBasic In InnerList
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
        If Not (TypeOf p_object Is cMCResults) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cMCResults = TryCast(p_object, cMCResults)

        'Check for any differences
        If comparedObject Is Nothing Then Return False

        Dim resultsBasicOuter As List(Of cMCResultBasic) = comparedObject.resultsExcel
        Dim resultsBasicInner As List(Of cMCResultBasic) = resultsExcel
        For Each resultOuter As cMCResultBasic In resultsBasicOuter
            isMatch = False
            For Each resultInner As cMCResultBasic In resultsBasicInner
                If Not resultOuter.Equals(resultInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Dim resultsRegularOuter As List(Of cMCResult) = comparedObject.resultsRegular
        Dim resultsRegularInner As List(Of cMCResult) = resultsRegular
        For Each resultOuter As cMCResult In resultsRegularOuter
            isMatch = False
            For Each resultInner As cMCResult In resultsRegularInner
                If Not resultOuter.Equals(resultInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next

        Dim resultsPostProcessedOuter As List(Of cMCResultPostProcessed) = comparedObject.resultsPostProcessed
        Dim resultsPostProcessedInner As List(Of cMCResultPostProcessed) = resultsPostProcessed
        For Each resultOuter As cMCResultPostProcessed In resultsPostProcessedOuter
            isMatch = False
            For Each resultInner As cMCResultPostProcessed In resultsPostProcessedInner
                If Not resultOuter.Equals(resultInner) Then
                    isMatch = True
                    Exit For
                End If
            Next
            If Not isMatch Then Return False
        Next
        Return True
    End Function
#End Region

#Region "Methods: List"
    ''' <summary>
    ''' Adds every element of the provided list to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As List(Of cMCResultBasic))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCResultBasic In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds every element of the provided collection to the list if it is unique to the list.
    ''' </summary>
    ''' <param name="p_items">List of multiple items to add.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Add(ByVal p_items As ObservableCollection(Of cMCResultBasic))
        If p_items Is Nothing Then Exit Sub

        For Each item As cMCResultBasic In p_items
            Add(item)
        Next
    End Sub
    ''' <summary>
    ''' Adds a new result object to the list if it is unique.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <param name="updateResultIDs">True: The added result will have its ID updated to fit the current set. 
    ''' False: The ID will be left as is.</param>
    ''' <remarks></remarks>
    Friend Overloads Function Add(ByVal p_result As cMCResultBasic,
                                  Optional ByVal updateResultIDs As Boolean = True) As Boolean
        Try
            If p_result Is Nothing Then Return False

            If ResultIsUnique(p_result) Then
                Dim results As List(Of cMCResultBasic) = ToList()
                Dim _ids As New cMCResultIDs()

                If updateResultIDs Then p_result = _ids.UpdateID(p_result, results)

                InnerList.Add(p_result)

                RaisePropertyChanged("item")
                Return True
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try

        Return False
    End Function


    ''' <summary>
    ''' Removes the result at the specified index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_index As Integer)
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            InnerList.RemoveAt(p_index)
            RaisePropertyChanged("item")
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the result corresponding to the provided result id.
    ''' </summary>
    ''' <param name="p_resultID">The unique result ID currently assigned to the result.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_resultID As String)
        Try
            Dim itemsRemoved As Boolean = False
            Dim tempList As New List(Of cMCResultBasic)

            If String.IsNullOrEmpty(p_resultID) Then Throw New ArgumentException("p_resultID is not specified.")

            For Each result As cMCResultBasic In InnerList
                If Not result.id = p_resultID Then
                    tempList.Add(result)
                Else
                    itemsRemoved = True
                End If
            Next

            If itemsRemoved Then
                InnerList.Clear()
                For Each item As cMCResultBasic In tempList
                    InnerList.Add(item)
                Next

                RaisePropertyChanged("item")
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
    ''' <summary>
    ''' Removes all of the existing results of the specified type in the MC Model.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_resultType As eResultType)
        If InnerList.Count = 0 Then Exit Sub

        Select Case p_resultType
            Case eResultType.regular
                For Each result As cMCResult In resultsRegular
                    Remove(result)
                Next
                RaisePropertyChanged(Function() Me.resultsRegular)
            Case eResultType.postProcessed
                For Each result As cMCResultPostProcessed In resultsPostProcessed
                    Remove(result)
                Next
                RaisePropertyChanged(Function() Me.resultsPostProcessed)
            Case eResultType.excelCalculated
                For Each result As cMCResultBasic In resultsExcel
                    Remove(result)
                Next
                RaisePropertyChanged(Function() Me.resultsExcel)
        End Select
    End Sub
    ''' <summary>
    ''' Removes the result corresponding to the result object provided.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_result As cMCResultBasic)
        Try
            Dim itemsRemoved As Boolean = False

            If p_result Is Nothing Then Exit Sub

            For Each result As cMCResultBasic In InnerList
                If ResultMatches(result, p_result) Then
                    InnerList.Remove(result)
                    itemsRemoved = True
                    Exit For
                End If
            Next

            If itemsRemoved Then
                RaisePropertyChanged("item")
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the result corresponding to the result object provided.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_result As cMCResult)
        Try
            Dim itemsRemoved As Boolean = False

            If p_result Is Nothing Then Exit Sub

            For Each result As cMCResult In InnerList
                If ResultMatches(result, p_result) Then
                    InnerList.Remove(result)
                    itemsRemoved = True
                    Exit For
                End If
            Next

            If itemsRemoved Then
                RaisePropertyChanged("item")
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub
    ''' <summary>
    ''' Removes the result corresponding to the result object provided.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Remove(ByVal p_result As cMCResultPostProcessed)
        Try
            Dim itemsRemoved As Boolean = False

            If p_result Is Nothing Then Exit Sub

            For Each result As cMCResultPostProcessed In InnerList
                If ResultMatches(result, p_result) Then
                    InnerList.Remove(result)
                    itemsRemoved = True
                    Exit For
                End If
            Next

            If itemsRemoved Then
                RaisePropertyChanged("item")
            End If
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
        End Try
    End Sub



    ''' <summary>
    ''' Replaces the matching result object with the one provided. 
    ''' A match is determined by the index number, or the query string and benchmark.
    ''' </summary>
    ''' <param name="p_item"></param>
    ''' <remarks></remarks>
    Friend Overloads Function Replace(ByVal p_item As cMCResultBasic) As Boolean
        Try
            If p_item Is Nothing Then Return False
            Dim index As Integer = 0

            If InnerList.Count = 0 Then
                InnerList.Add(p_item)
                Return True
            Else
                For Each item As cMCResultBasic In InnerList
                    If ResultMatches(item, p_item) Then
                        Exit For
                    End If
                    index += 1
                Next
                If index < InnerList.Count Then
                    InnerList(index) = p_item
                    RaisePropertyChanged("item")
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
    Friend Overloads Sub Replace(ByVal p_items As List(Of cMCResultBasic))
        InnerList.Clear()
        Add(p_items)
    End Sub
    ''' <summary>
    ''' Replaces the current list of items with one provided. Only unique items will be added.
    ''' </summary>
    ''' <param name="p_items"></param>
    ''' <remarks></remarks>
    Friend Overloads Sub Replace(ByVal p_items As ObservableCollection(Of cMCResultBasic))
        InnerList.Clear()
        Add(p_items)
    End Sub

    ''' <summary>
    ''' Returns the collection object as an observable collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToObservableCollection() As ObservableCollection(Of cMCResultBasic)
        Dim templist As New ObservableCollection(Of cMCResultBasic)

        For Each item As cMCResult In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function

    ''' <summary>
    ''' Returns the collection object as a list.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ToList() As List(Of cMCResultBasic)
        Dim templist As New List(Of cMCResultBasic)

        For Each item As cMCResultBasic In InnerList
            templist.Add(item)
        Next

        Return templist
    End Function
#End Region

#Region "Methods: Public - Update/Insert/Add"
    ''' <summary>
    ''' Updates the results to be saved such that: 
    '''  (1) Any existing results maintain their original IDs and result update objects.
    '''  (2) Any new results that have matching query IDs have their query and Benchmark IDs adjust to fit the existing sets of IDs.
    '''  (3) Any new results with new queries have their query IDs adjust to fit the existing set of query IDs.
    '''  (4) Any removed results are no longer present in the results list, and any updated IDs do not use the IDs from these removed results.
    ''' </summary>
    ''' <param name="p_resultsNew">New result objects to update, insert, or append to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MergeChanges(ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Dim results As List(Of cMCResult) = GetItemsByType(Of cMCResult)(eResultType.regular)
        InitializeTempIDs(results)
        Return MergeChanges(results, p_resultsNew)
    End Function
    ''' <summary>
    ''' Updates the results to be saved such that: 
    '''  (1) Any existing results maintain their original IDs and result update objects.
    '''  (2) Any new results that have matching query IDs have their query and benchmark IDs adjust to fit the existing sets of IDs.
    '''  (3) Any new results with new queries have their query IDs adjust to fit the existing set of query IDs.
    '''  (4) Any removed results are no longer present in the results list, and any updated IDs do not use the IDs from these removed results.
    ''' </summary>
    ''' <param name="p_resultsOriginal">Original result objects.</param>
    ''' <param name="p_resultsNew">New result objects to update, insert, or append to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MergeChanges(ByVal p_resultsOriginal As List(Of cMCResult),
                                        ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Dim updatedResults As New List(Of cMCResult)

        'Update changed results, keep existing results, drop removed results
        updatedResults = MergeEditedResults(p_resultsOriginal, p_resultsNew)

        ' Add new results that only differ by benchmarks.
        ' Existing query IDs are maintained, but benchmark IDs are incremented.
        updatedResults = InsertNewResultBenchmarks(updatedResults, p_resultsNew)

        '  Add new results that differ by by queries
        updatedResults = AddNewResultQueries(updatedResults, p_resultsNew)

        Return updatedResults
    End Function

    ''' <summary>
    ''' Updates the original results that have may have been edited. 
    ''' IDs are set to preserve old IDs.
    ''' Original results are considered removed if they do not exist in the edited results.
    ''' </summary>
    ''' <param name="p_resultsEdited">New result objects to update or insert to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MergeEditedResults(ByVal p_resultsEdited As List(Of cMCResult)) As List(Of cMCResult)
        Dim results As List(Of cMCResult) = GetItemsByType(Of cMCResult)(eResultType.regular)
        InitializeTempIDs(results)
        Return MergeEditedResults(results, p_resultsEdited)
    End Function
    ''' <summary>
    ''' Updates the original results that have may have been edited. 
    ''' IDs are set to preserve old IDs.
    ''' Original results are considered removed if they do not exist in the edited results.
    ''' </summary>
    ''' <param name="p_resultsOriginal">Original result objects from before any editing.</param>
    ''' <param name="p_resultsEdited">New result objects to update or insert to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MergeEditedResults(ByVal p_resultsOriginal As List(Of cMCResult),
                                              ByVal p_resultsEdited As List(Of cMCResult)) As List(Of cMCResult)
        Dim editedResultUpdated As Boolean = False
        Dim updatedResults = New List(Of cMCResult)
        If (p_resultsOriginal Is Nothing OrElse p_resultsEdited Is Nothing) Then Return updatedResults

        For Each resultEdited As cMCResult In p_resultsEdited
            editedResultUpdated = False
            For Each resultOriginal As cMCResult In p_resultsOriginal
                editedResultUpdated = UpdateEditedResult(resultEdited, resultOriginal)
                If editedResultUpdated Then Exit For
            Next

            ' Add currently updated results as they are assumed to still be in the same query id order.
            ' This includes unchanged results that exist in both lists
            If editedResultUpdated Then updatedResults.Add(CType(resultEdited.Clone, cMCResult))
        Next

        updatedResults.Sort()

        Return updatedResults
    End Function

    ''' <summary>
    ''' Inserts new results that share existing queries but have different benchmarks. 
    ''' Query IDs are preserved. 
    ''' Benchmark IDs are assigned to fit with the existing list of results, accounting for possible duplication of the benchmark ID of any removed results. 
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum. 
    ''' This may leave gaps in the ID sequence. 
    ''' </summary>
    ''' <param name="p_resultsNew">New result objects to be inserted into the finalized list of results based on the matching query IDs and new benchmark IDs.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertNewResultBenchmarks(ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Dim results As List(Of cMCResult) = GetItemsByType(Of cMCResult)(eResultType.regular)
        InitializeTempIDs(results)
        Return InsertNewResultBenchmarks(results, p_resultsNew)
    End Function
    ''' <summary>
    ''' Inserts new results that share existing queries but have different benchmarks. 
    ''' Query IDs are preserved. 
    ''' Benchmark IDs are assigned to fit with the existing list of results, accounting for possible duplication of the benchmark ID of any removed results. 
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum. 
    ''' This may leave gaps in the ID sequence. 
    ''' </summary>
    ''' <param name="p_resultsOriginal">Original result objects from before any editing.</param>
    ''' <param name="p_resultsNew">New result objects to be inserted into the finalized list of results based on the matching query IDs and new benchmark IDs.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertNewResultBenchmarks(ByVal p_resultsOriginal As List(Of cMCResult),
                                                     ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Dim newResultUpdated As Boolean = False
        Dim updatedResults = New List(Of cMCResult)
        If (p_resultsOriginal Is Nothing OrElse p_resultsNew Is Nothing) Then Return updatedResults

        For Each resultNew As cMCResult In p_resultsNew
            newResultUpdated = False
            For Each resultOriginal As cMCResult In p_resultsOriginal
                newResultUpdated = UpdateNewResultBenchmark(resultNew, resultOriginal, p_resultsOriginal)
                If newResultUpdated Then Exit For
            Next

            ' Add currently existing or inserted results as they are assumed to still be in the same query id order.
            If newResultUpdated Then updatedResults.Add(CType(resultNew.Clone, cMCResult))
        Next

        updatedResults.Sort()

        Return updatedResults
    End Function

    ''' <summary>
    ''' Adds new results that do not share any queries with existing results. 
    ''' Query IDs are assigned to fit with the existing list of results, accounting for possible duplication of the query ID of any removed results.
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum.
    ''' This may leave gaps in the ID sequence.
    ''' </summary>
    ''' <param name="p_resultsNew">New result objects to append to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AddNewResultQueries(ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Return AddNewResultQueries(GetItemsByType(Of cMCResult)(eResultType.regular), p_resultsNew)
    End Function
    ''' <summary>
    ''' Adds new results that do not share any queries with existing results. 
    ''' Query IDs are assigned to fit with the existing list of results, accounting for possible duplication of the query ID of any removed results.
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum.
    ''' This may leave gaps in the ID sequence.
    ''' </summary>
    ''' <param name="p_resultsOriginal">Original result objects from before any editing.</param>
    ''' <param name="p_resultsNew">New result objects to append to the current list of result objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function AddNewResultQueries(ByVal p_resultsOriginal As List(Of cMCResult),
                                               ByVal p_resultsNew As List(Of cMCResult)) As List(Of cMCResult)
        Dim updatedResults = New List(Of cMCResult)
        If (p_resultsOriginal Is Nothing OrElse p_resultsNew Is Nothing) Then Return updatedResults

        ' The original set is checked in case the last example was removed and it was the only example for a given query ID.
        Dim nextQueryID As Integer = MaxQueryID(p_resultsOriginal) + 1
        p_resultsNew.Sort()

        For i = 0 To p_resultsNew.Count - 1
            Dim currentResult As cMCResult = p_resultsNew(i)
            Dim priorResult As cMCResult
            If i = 0 Then
                priorResult = Nothing
            Else
                priorResult = p_resultsNew(i - 1)
            End If

            UpdateNewResultQuery(nextQueryID, currentResult, priorResult, updatedResults)

            ' Add the new result to the end of the list of results.
            updatedResults.Add(CType(currentResult.Clone, cMCResult))
        Next

        updatedResults.Sort()

        Return updatedResults
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Converts the list of one type to the base type.
    ''' </summary>
    ''' <param name="p_results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConvertToBase(ByVal p_results As List(Of cMCResult)) As List(Of cMCResultBasic)
        Dim resultsBasic As New List(Of cMCResultBasic)
        If p_results Is Nothing Then Return resultsBasic

        For Each result As cMCResult In p_results
            resultsBasic.Add(result)
        Next
        Return resultsBasic
    End Function

    ' Update IDs
    ''' <summary>
    ''' Returns 'True' if the max number of digits among all benchmarks has changed.
    ''' </summary>
    ''' <param name="p_result"></param>
    ''' <param name="p_results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BenchmarkPaddingIncreased(ByVal p_result As cMCResultBasic,
                                               ByVal p_results As List(Of cMCResultBasic)) As Boolean
        If (p_results Is Nothing OrElse p_result Is Nothing) Then Return False

        p_results.Sort()
        Return (CStr(p_result.benchmark.ID).Length > CStr(p_results(p_results.Count - 1).id).Length)
    End Function

    ''' <summary>
    ''' If no result IDs have been set yet, update all of the result IDs.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateResultIDsAllIfEmpty()
        Dim resultIDsSet As Boolean = False

        For Each result As cMCResultBasic In InnerList
            If Not String.IsNullOrEmpty(result.id) Then
                resultIDsSet = True
                Exit For
            End If
        Next

        If Not resultIDsSet Then UpdateResultIDsAll()
    End Sub

    ''' <summary>
    ''' Updates the IDs of all results for all types, creating a contiguous numbering system starting from 0.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UpdateResultIDsAll()
        Dim _ids As New cMCResultIDs()
        Replace(_ids.UpdateIDs(ToList()))
    End Sub

    ''' <summary>
    ''' Updates the corresponding result update IDs to the new update IDs provided.
    ''' </summary>
    ''' <param name="p_oldNewIdentifiers">A dictionary of the old update IDs as keys to the new update IDs as values.
    ''' Used to identify which result update IDs to change, and what to change them to.</param>
    ''' <remarks></remarks>
    Friend Sub UpdateResultUpdateIDs(ByVal p_oldNewIdentifiers As Dictionary(Of Integer, Integer))
        If p_oldNewIdentifiers Is Nothing Then Exit Sub

        'Any result update IDs that link to a non-unique ID will be set to the ID of the first update object encountered.
        For Each result As cMCResultBasic In InnerList
            For Each resultUpdate As cMCResultUpdate In result.updates
                For Each oldIdentifier As Integer In p_oldNewIdentifiers.Keys
                    If resultUpdate.id = oldIdentifier Then resultUpdate.id = p_oldNewIdentifiers(oldIdentifier)
                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' Assign temporary IDs the results to indicate their index in a list.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitializeTempIDs(ByRef p_results As List(Of cMCResult))
        Dim tempID As Integer = 1

        For Each result As cMCResult In p_results
            result.idTemp = tempID
            tempID += 1
        Next
    End Sub

    ' Query
    ''' <summary>
    ''' Returns a result from the provided list based on a match with the provided result and a comparer object.
    ''' Returns null if unsuccessful.
    ''' </summary>
    ''' <param name="p_result">Result object to use for the match.</param>
    ''' <param name="p_results">List of results from which to return a matching object.</param>
    ''' <param name="p_comparer">Object that contains comparison rules.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function MatchingResult(ByVal p_result As cMCResultBasic,
                                           ByVal p_results As List(Of cMCResultBasic),
                                           ByVal p_comparer As IComparer(Of cMCResultBasic)) As cMCResultBasic
        If (p_results Is Nothing OrElse
            p_result Is Nothing OrElse
            p_comparer Is Nothing) Then Return Nothing

        For Each result As cMCResultBasic In p_results
            If p_comparer.Compare(p_result, result) = 0 Then Return result
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Determines if all of the result objects have valid IDs. In order to be valid, all IDs must be unique.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ResultsAllHaveUniqueIDs() As Boolean
        Dim idsList As New List(Of String)
        Dim idsListUnique As New List(Of Integer)

        For Each result As cMCResultBasic In InnerList
            idsList.Add(result.id)
        Next

        'Check that every result has an ID
        If Not idsList.Count = InnerList.Count Then Return False

        'Check that every ID is unique
        For Each id As Integer In idsList
            idsListUnique.Add(id)
        Next

        idsListUnique = ConvertToUniqueList(idsListUnique)

        If (idsListUnique.Count = idsList.Count AndAlso
            idsListUnique.Count = InnerList.Count) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns a unique list of all of the tables referenced in the results in the collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function TablesUsed() As List(Of String)
        Dim uniqueList As New cObsColUniqueString
        Dim results As List(Of cMCResult) = resultsNotExcel

        For Each result As cMCResult In results
            uniqueList.Add(result.tableName)
        Next

        Return uniqueList.ToList
    End Function

    ''' <summary>
    ''' Returns a unique list of all of the load cases and combinations in the collection.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function LoadCaseCombosUsed() As List(Of String)
        Dim uniqueList As New cObsColUniqueString
        Dim results As List(Of cMCResult) = resultsNotExcel

        For Each result As cMCResult In results
            For Each field As cFieldLookup In result.query
                If (StringsMatch(field.name, result.tableFieldProperties.headerLoadCase) OrElse
                    StringsMatch(field.name, result.tableFieldProperties.headerDesignLoadCombo)) Then

                    uniqueList.Add(field.valueField)
                End If
            Next
        Next

        Return uniqueList.ToList

    End Function

    ''' <summary>
    ''' Returns the maximum query id from a collection of results.
    ''' Returns -1 if there is an error or if there are no results.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaxQueryID() As Integer
        Return MaxQueryID(ToList())
    End Function
    ''' <summary>
    ''' Returns the maximum query id from a collection of results.
    ''' Returns -1 if there is an error, if the list has no results, or if no results have an ID set.
    ''' </summary>
    ''' <param name="p_results">Collection of results to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MaxQueryID(Of T)(ByVal p_results As List(Of T)) As Integer
        Dim idQueryMax As Integer = -1
        If p_results Is Nothing Then Return idQueryMax

        Dim results As List(Of cMCResultBasic) = Convert(Of T, cMCResultBasic)(p_results)
        For Each result As cMCResultBasic In results
            If result.query.ID > idQueryMax Then idQueryMax = result.query.ID
        Next

        Return idQueryMax
    End Function

    ''' <summary>
    ''' Returns the maximum benchmark id from a collection of results that share the same query.
    ''' Returns -1 if no matching query id was found or there is an error.
    ''' </summary>
    ''' <param name="p_idQuery">The query within which the maximum benchmark id is ascertained.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MaxBenchmarkID(ByVal p_idQuery As Integer) As Integer
        Return MaxBenchmarkID(ToList(), p_idQuery)
    End Function
    ''' <summary>
    ''' Returns the maximum benchmark id from a collection of results that share the same query.
    ''' Returns -1 if no matching query id was found or there is an error.
    ''' </summary>
    ''' <param name="p_results">Collection of results to check.</param>
    ''' <param name="p_idQuery">The query within which the maximum benchmark id is ascertained.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MaxBenchmarkID(Of T)(ByVal p_results As List(Of T),
                                                ByVal p_idQuery As Integer) As Integer
        Dim idBenchmarkMax As Integer = -1
        If p_results Is Nothing Then Return idBenchmarkMax


        Dim results As List(Of cMCResultBasic) = Convert(Of T, cMCResultBasic)(p_results)
        For Each result As cMCResultBasic In results
            If result.query.ID = p_idQuery Then
                If result.benchmark.ID > idBenchmarkMax Then idBenchmarkMax = result.benchmark.ID
            End If
        Next

        Return idBenchmarkMax
    End Function
#End Region

#Region "Methods: Private"
    ' Get
    ''' <summary>
    ''' Returns the item specified by index.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_index As Integer) As cMCResultBasic
        Try
            If p_index < 0 Then Throw New ArgumentException("Index {1} cannot be a negative number.", p_index.ToString)
            If p_index >= InnerList.Count Then Throw New ArgumentException("Index is greater than the size of the collection: {1} ", "Index: " & p_index.ToString & " Collection Count: " & InnerList.Count.ToString)

            Return CType(InnerList(p_index), cMCResultBasic)
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Returns the result item specified by the current result ID.
    ''' </summary>
    ''' <param name="p_resultID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Overloads Function GetItem(ByVal p_resultID As String) As cMCResultBasic
        Try
            If String.IsNullOrEmpty(p_resultID) Then Throw New ArgumentException("p_resultID is not specified.")

            For Each result As cMCResultBasic In InnerList
                If result.id = p_resultID Then
                    Return result
                End If
            Next
        Catch argExc As ArgumentException
            RaiseEvent Log(New LoggerEventArgs(argExc))
            Return Nothing
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Returns a list of result objects that are all of the specified result type.
    ''' </summary>
    ''' <param name="p_type">Type of result for which a list is to be returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItemsByType(Of T)(ByVal p_type As eResultType) As List(Of T)

        Dim itemIndices As List(Of Integer) = IndicesOfResultByType(p_type)
        Dim results As New List(Of T)
        For Each i As Integer In itemIndices
            results.Add(CType(InnerList(i), T))
        Next

        Return results
    End Function

    ''' <summary>
    ''' Returns a list of indices corresponding to results of the specified type.
    ''' </summary>
    ''' <param name="p_type">Type of result to get indices for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndicesOfResultByType(ByVal p_type As eResultType) As List(Of Integer)
        Dim itemIndex As Integer = 0
        Dim itemIndices As New List(Of Integer)
        For Each result As cMCResultBasic In InnerList
            If result.resultType = p_type Then
                itemIndices.Add(itemIndex)
            End If
            itemIndex += 1
        Next

        Return itemIndices
    End Function

    ' Updating results list
    ''' <summary>
    ''' The entire example is updated, but the original IDs and updates are preserved.
    ''' </summary>
    ''' <param name="p_resultEdited">New result object to update or insert to the current list of result objects.</param>
    ''' <param name="p_resultOriginal">Original result object from before any editing.</param>
    ''' <remarks></remarks>
    Private Shared Function UpdateEditedResult(ByRef p_resultEdited As cMCResult,
                                               ByVal p_resultOriginal As cMCResult) As Boolean
        If ResultMatchesByTempID(p_resultOriginal, p_resultEdited) Then
            p_resultEdited.benchmark.ID = p_resultOriginal.benchmark.ID
            p_resultEdited.query.ID = p_resultOriginal.query.ID

            p_resultEdited.updates.Clear()
            For Each update As cMCResultUpdate In p_resultOriginal.updates
                p_resultEdited.updates.Add(CType(update.Clone, cMCResultUpdate))
            Next

            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Inserts the new result if it shares existing queries but has different benchmarks. 
    ''' Query IDs are preserved. 
    ''' Benchmark IDs are assigned to fit with the existing list of results, accounting for possible duplication of the benchmark ID of any removed results. 
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum. 
    ''' This may leave gaps in the ID sequence. 
    ''' </summary>
    ''' <param name="p_resultNew">New result object to be inserted into the finalized list of results based on the matching query IDs and new benchmark IDs.</param>
    ''' <param name="p_resultOriginal">Original result object from before any editing.</param>
    ''' <param name="p_resultsOriginal">Original result objects from before any editing.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function UpdateNewResultBenchmark(ByRef p_resultNew As cMCResult,
                                                     ByVal p_resultOriginal As cMCResult,
                                                     ByVal p_resultsOriginal As List(Of cMCResult)) As Boolean
        If ResultMatchesByTempID(p_resultOriginal, p_resultNew) Then
            ' Result still exists; Maintain its position in the list.
            Return True
        ElseIf NewBenchmarkForExistingQuery(p_resultOriginal, p_resultNew) Then
            ' Query ID must be set according to existing ones in this set, while the benchmark is incremented.
            p_resultNew.query.ID = p_resultOriginal.query.ID
            p_resultNew.benchmark.ID = MaxBenchmarkID(p_resultsOriginal, p_resultOriginal.query.ID) + 1
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Adds a new result that does not share any queries with existing results. 
    ''' Query ID is assigned to fit with the existing list of results, accounting for possible duplication of the query ID of any removed results.
    ''' This is done by preserving the current numbering, and only taking IDs that are greater than the current maximum.
    ''' This may leave gaps in the ID sequence.
    ''' </summary>
    ''' <param name="p_resultCurrent">The current result from the new results being considered to be added to the updated list.</param>
    ''' <param name="p_resultPrior">The prior result from the new results being considered.</param>
    ''' <param name="p_resultsUpdated">The most recent list of results that has had new results changed, inserted, or added.</param>
    ''' <param name="p_nextQueryID">The next query ID to be used in the updated results list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function UpdateNewResultQuery(ByRef p_nextQueryID As Integer,
                                              ByRef p_resultCurrent As cMCResult,
                                              ByVal p_resultPrior As cMCResult,
                                              ByVal p_resultsUpdated As List(Of cMCResult)) As Boolean
        If Not p_resultCurrent.idReadOnly Then
            If QueryMatchesPriorResult(p_resultCurrent, p_resultPrior) Then
                ' Set the query IDs to be the same
                p_resultCurrent.query.ID = p_resultPrior.query.ID

                ' Set the benchmark ID as appropriate for the query
                p_resultCurrent.benchmark.ID = MaxBenchmarkID(p_resultsUpdated, p_resultCurrent.query.ID) + 1
            Else
                ' Set the query ID to the next one and increment the next query ID.
                p_resultCurrent.query.ID = p_nextQueryID
                p_nextQueryID += 1
            End If

            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Methods: Private - Boolean"
    ''' <summary>
    ''' Determines if the result object is unique to result objects in the list.
    ''' </summary>
    ''' <param name="p_result">Result object to check for uniqueness.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultIsUnique(ByVal p_result As cMCResultBasic) As Boolean
        For Each result As cMCResultBasic In InnerList
            If ResultMatches(result, p_result) Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Determines if the result object is unique compared to the base result object.
    ''' </summary>
    ''' <param name="p_resultBase">Base result object to compare against.</param>
    ''' <param name="p_result">Result object to compare.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ResultMatches(ByVal p_resultBase As cMCResultBasic,
                                   ByVal p_result As cMCResultBasic) As Boolean
        If (p_result.resultType = p_resultBase.resultType AndAlso
            p_result.resultType = eResultType.excelCalculated) Then
            If (p_result.name = p_resultBase.name) Then Return True
        Else
            If (StringsMatch(p_result.tableName, p_resultBase.tableName) AndAlso
                p_result.query.Equals(p_resultBase.query) AndAlso
                StringsMatch(p_result.benchmark.name, p_resultBase.benchmark.name)) Then

                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' The results match based on their temporary IDs.
    ''' This is used to match original results to those that had been passed in to an editing session, possibly changed, but with the same queries and benchmarks.
    ''' </summary>
    ''' <param name="p_resultOriginal"></param>
    ''' <param name="p_resultNew"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ResultMatchesByTempID(ByVal p_resultOriginal As cMCResult,
                                                  ByVal p_resultNew As cMCResult) As Boolean
        Return (p_resultOriginal.idTemp = p_resultNew.idTemp)
        'Return (StringsMatch(p_resultOriginal.tableName, p_resultNew.tableName) AndAlso
        '        p_resultOriginal.query.Equals(p_resultNew.query))
    End Function

    ''' <summary>
    ''' A new result is to be added that contains an existing query but a new benchmark.
    ''' </summary>
    ''' <param name="p_resultOriginal"></param>
    ''' <param name="p_resultNew"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function NewBenchmarkForExistingQuery(ByVal p_resultOriginal As cMCResult,
                                                         ByVal p_resultNew As cMCResult) As Boolean
        Return (p_resultNew.idNewAndSynced AndAlso
                p_resultOriginal.query.Equals(p_resultNew.query))
    End Function

    ''' <summary>
    ''' The query of the current result is the same as that of the prior result in the list.
    ''' </summary>
    ''' <param name="p_resultCurrent">Current result.</param>
    ''' <param name="p_resultPrior">Prior result. May be provided as null if current result is the first in a list.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function QueryMatchesPriorResult(ByVal p_resultCurrent As cMCResult,
                                                    ByVal p_resultPrior As cMCResult) As Boolean
        If p_resultPrior Is Nothing Then
            Return False
        Else
            Return (StringsMatch(p_resultCurrent.tableName, p_resultPrior.tableName) AndAlso
                    p_resultCurrent.query.Equals(p_resultPrior.query))
        End If
    End Function
#End Region


End Class
