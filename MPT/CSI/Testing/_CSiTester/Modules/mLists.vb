Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Module mLists
    ' ''' <summary>
    ' ''' Parses Query into a dictionary list of header-value pairs, with a new entry for each 'AND'.
    ' ''' </summary>
    ' ''' <param name="p_query">String containting the Query, with only the 'LIKE' 'AND' components.</param>
    ' ''' <remarks></remarks>
    'Function CreateDictionaryList(ByVal p_query As String) As Dictionary(Of String, String)
    '    Dim myDictionaryList As New Dictionary(Of String, String)
    '    Dim keyStart As Integer = 1
    '    Dim keyEnd As Integer
    '    Dim valStart As Integer
    '    Dim valEnd As Integer
    '    Try
    '        If p_query.Count > 0 Then
    '            For i = 1 To Len(p_query)

    '                If Mid(p_query, i, 4).ToUpper = "LIKE" Then
    '                    keyEnd = i - 2
    '                    valStart = i + 5
    '                ElseIf Mid(p_query, i, 3).ToUpper = "AND" Then
    '                    valEnd = i - 2
    '                    myDictionaryList.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
    '                    keyStart = i + 4
    '                ElseIf i = Len(p_query) Then
    '                    valEnd = i
    '                    myDictionaryList.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        MsgBox(ex.StackTrace)
    '    End Try

    '    CreateDictionaryList = myDictionaryList
    'End Function

    ' ''' <summary>
    ' ''' Clears all blank entries from an observable collection of strings.
    ' ''' </summary>
    ' ''' <param name="p_list">List of string entries.</param>
    ' ''' <remarks></remarks>
    'Sub ClearBlankEntries(ByRef p_list As ObservableCollection(Of String))
    '    Dim myTempList As New ObservableCollection(Of String)

    '    For Each entry As String In p_list
    '        If Not entry = "" Then myTempList.Add(entry)
    '    Next

    '    p_list.Clear()
    '    p_list = myTempList
    'End Sub

    ' ''' <summary>
    ' ''' Converts list of string to an observable collection of string, or the reverse.
    ' ''' </summary>
    ' ''' <param name="p_list">List of items to either convert, or fill.</param>
    ' ''' <param name="p_obsCol">The observable collection to either convert or fill.</param>
    ' ''' <param name="p_convertListToObsCol">If true, the OC parameter will be filled with the array items. If false, the list parameter is filled with the list item.</param>
    ' ''' <remarks></remarks>
    'Sub ConvertListObservableCollectionString(ByRef p_list As List(Of String),
    '                                          ByRef p_obsCol As ObservableCollection(Of String),
    '                                          ByVal p_convertListToObsCol As Boolean)
    '    If p_convertListToObsCol Then
    '        For Each ListItem As String In p_list
    '            p_obsCol.Add(ListItem)
    '        Next
    '    Else
    '        For Each OCItem As String In p_obsCol
    '            p_list.Add(OCItem)
    '        Next
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Checks if a particular string exists in a provided list of strings. If it does not, it is added in the specified first/last entry of the list.
    ' ''' </summary>
    ' ''' <param name="p_list">List of string elements to check and,if necessary, modify.</param>
    ' ''' <param name="p_listItem">List item to check against the list of string elements.</param>
    ' ''' <param name="p_placeFirst">If true, and the list item is added, it will be added as the first item in the new list. If false, the list item will be added as the last item in the list.</param>
    ' ''' <remarks></remarks>
    'Sub AddToListStringIfNew(ByRef p_list As List(Of String),
    '                         ByVal p_listItem As String,
    '                         ByVal p_placeFirst As Boolean)
    '    Dim tempList As New List(Of String)
    '    Dim newToList As Boolean = True

    '    'Check it item is unique
    '    For Each listItem As String In p_list
    '        If listItem = p_listItem Then
    '            newToList = False
    '            Exit For
    '        End If
    '    Next

    '    'If unique, add to the desired place in the list
    '    If newToList Then
    '        'Add to temp list
    '        If p_placeFirst Then
    '            tempList.Add(p_listItem)
    '            For Each ListItem As String In p_list
    '                tempList.Add(ListItem)
    '            Next
    '        Else
    '            For Each ListItem As String In p_list
    '                tempList.Add(ListItem)
    '            Next
    '            tempList.Add(p_listItem)
    '        End If

    '        'Replace provided list with updated temp list
    '        p_list.Clear()
    '        p_list = tempList
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Checks if a particular string exists in a provided list of strings. If it does not, it is added in the specified first/last entry of the list.
    ' ''' </summary>
    ' ''' <param name="p_list">List of string elements to check and,if necessary, modify.</param>
    ' ''' <param name="p_listItem">List item to check against the list of string elements.</param>
    ' ''' <param name="p_placeFirst">If true, and the list item is added, it will be added as the first item in the new list. If false, the list item will be added as the last item in the list.</param>
    ' ''' <remarks></remarks>
    'Sub AddToObsColStringIfNew(ByRef p_list As ObservableCollection(Of String),
    '                           ByVal p_listItem As String,
    '                           Optional ByVal p_placeFirst As Boolean = False)
    '    Dim tempList As New ObservableCollection(Of String)
    '    Dim newToList As Boolean = True
    '    If p_listItem = "" Then Exit Sub

    '    'Check if item is unique
    '    For Each listItem As String In p_list
    '        If listItem = p_listItem Then
    '            newToList = False
    '            Exit For
    '        End If
    '    Next

    '    'If unique, add to the desired place in the list
    '    If newToList Then
    '        'Add to temp list
    '        If p_placeFirst Then
    '            tempList.Add(p_listItem)
    '            For Each ListItem As String In p_list
    '                tempList.Add(ListItem)
    '            Next
    '        Else
    '            For Each ListItem As String In p_list
    '                tempList.Add(ListItem)
    '            Next
    '            tempList.Add(p_listItem)
    '        End If

    '        'Replace provided list with updated temp list
    '        p_list.Clear()
    '        p_list = tempList
    '    End If
    'End Sub


    ' ''' <summary>
    ' ''' Returns true/false depending on whether the entry provided exists in the list provided.
    ' ''' </summary>
    ' ''' <param name="p_entry">Value to search for in the list.</param>
    ' ''' <param name="p_list">List to search within for the value.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ExistsInListString(ByVal p_entry As String,
    '                            ByVal p_list As ObservableCollection(Of String)) As Boolean
    '    ExistsInListString = False
    '    Try
    '        If IsNothing(p_list) Then Exit Function

    '        For Each myListEntry As String In p_list
    '            If p_entry = myListEntry Then
    '                Return True
    '            End If
    '        Next

    '        Return False

    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' Returns true/false depending on whether the entry provided exists in the list provided.
    ' ''' </summary>
    ' ''' <param name="p_entry">Value to search for in the list.</param>
    ' ''' <param name="p_list">List to search within for the value.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function ExistsInListInteger(ByVal p_entry As Integer,
    '                             ByVal p_list As ObservableCollection(Of Integer)) As Boolean
    '    ExistsInListInteger = False
    '    Try
    '        If IsNothing(p_list) Then Exit Function

    '        For Each myListEntry As Integer In p_list
    '            If p_entry = myListEntry Then
    '                Return True
    '            End If
    '        Next

    '        Return False

    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' Given a new list and base list, if the new list has unique entries, they are added to the base list. The base list can start out empty to create a new unique list from an existing list.
    ' ''' </summary>
    ' ''' <param name="p_newList">New list to check.</param>
    ' ''' <param name="p_baseList">Unique list to check against.</param>
    ' ''' <remarks></remarks>
    'Sub CreateUniqueListString(ByVal p_newList As ObservableCollection(Of String),
    '                           ByRef p_baseList As ObservableCollection(Of String))
    '    Try
    '        Dim entryMatch As Boolean

    '        If IsNothing(p_newList) Then Exit Sub
    '        If IsNothing(p_baseList) Then Exit Sub

    '        For Each myNewEntry As String In p_newList
    '            entryMatch = False
    '            If Not myNewEntry = "" Then

    '                For Each myBaseEntry As String In p_baseList
    '                    If myNewEntry = myBaseEntry Then
    '                        entryMatch = True
    '                        Exit For
    '                    End If
    '                Next
    '                If Not entryMatch Then p_baseList.Add(myNewEntry)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ' ''' </summary>
    ' ''' <param name="p_originalList">Original list to check for redundancies.</param>
    ' ''' <remarks></remarks>
    'Sub ConvertToUniqueListString(ByRef p_originalList As List(Of String))
    '    Try
    '        Dim entryMatch As Boolean
    '        Dim myNewList As New List(Of String)

    '        If IsNothing(p_originalList) Then Exit Sub

    '        If p_originalList.Count > 0 Then
    '            For Each myOriginalEntry As String In p_originalList
    '                entryMatch = False
    '                If Not myOriginalEntry = "" Then
    '                    For Each myNewEntry As String In myNewList
    '                        If myNewEntry = myOriginalEntry Then
    '                            entryMatch = True
    '                            Exit For
    '                        End If
    '                    Next
    '                    If Not entryMatch Then myNewList.Add(myOriginalEntry)
    '                End If
    '            Next
    '        End If

    '        p_originalList.Clear()
    '        p_originalList = myNewList
    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Checks the observable collection provided and creates a new list where any duplicate entries are removed.
    ' ''' </summary>
    ' ''' <param name="p_originalList">Original list to check for redundancies.</param>
    ' ''' <remarks></remarks>
    'Sub ConvertToUniqueObsColString(ByRef p_originalList As ObservableCollection(Of String))
    '    Try
    '        Dim entryMatch As Boolean
    '        Dim myNewList As New ObservableCollection(Of String)

    '        If IsNothing(p_originalList) Then Exit Sub

    '        If p_originalList.Count > 0 Then
    '            For Each myOriginalEntry As String In p_originalList
    '                entryMatch = False
    '                If Not myOriginalEntry = "" Then
    '                    For Each myNewEntry As String In myNewList
    '                        If myNewEntry = myOriginalEntry Then
    '                            entryMatch = True
    '                            Exit For
    '                        End If
    '                    Next
    '                    If Not entryMatch Then myNewList.Add(myOriginalEntry)
    '                End If
    '            Next
    '        End If

    '        p_originalList.Clear()
    '        p_originalList = myNewList
    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Removes a list of string items from another list of string items.
    ' ''' </summary>
    ' ''' <param name="p_removeList">List of string items to be removed from the base list.</param>
    ' ''' <param name="p_baseList">Base list from which to remove the provided items.</param>
    ' ''' <remarks></remarks>
    'Sub RemoveFromList(ByVal p_removeList As ObservableCollection(Of String),
    '                   ByRef p_baseList As ObservableCollection(Of String))
    '    Dim entryMatch As Boolean
    '    Dim tempList As New ObservableCollection(Of String)

    '    For Each myBaseEntry As String In p_baseList
    '        entryMatch = False
    '        For Each myRemoveEntry As String In p_removeList
    '            If myRemoveEntry = myBaseEntry Then
    '                entryMatch = True
    '                Exit For
    '            End If
    '        Next
    '        If Not entryMatch Then tempList.Add(myBaseEntry)
    '    Next

    '    p_baseList = tempList
    'End Sub


    ' ''' <summary>
    ' ''' Takes a list of string numbers as a single string and separates them as entries in a list, or takes a list of string numbers and appends them into a single string.
    ' ''' </summary>
    ' ''' <param name="p_listOfStrings">List of the string items.</param>
    ' ''' <param name="p_stringOfLists">Single string containing the list of items.</param>
    ' ''' <param name="p_readList">True: A single string of appended list items is generated from the list. False: A list of string items is extracted from a list within a stingle string.</param>
    ' ''' <param name="p_sortList">True: List will be sorted. False: List will be kept in the order provided.</param>
    ' ''' <remarks></remarks>
    'Sub ParseListString(ByRef p_listOfStrings As List(Of String),
    '                    ByRef p_stringOfLists As String,
    '                    ByVal p_readList As Boolean,
    '                    Optional p_sortList As Boolean = True)
    '    If p_readList Then                                'Write model IDs from collection to a single string, separated by commas
    '        p_stringOfLists = ""
    '        For i = 0 To p_listOfStrings.Count - 1
    '            If i = 0 Then
    '                p_stringOfLists = p_listOfStrings(i)
    '            Else
    '                p_stringOfLists = p_stringOfLists & ", " & p_listOfStrings(i)
    '            End If
    '        Next
    '    Else                                            'Find the model IDs and add them to the list
    '        Dim mytempString As String
    '        Dim mytempStringTrimmed As String
    '        Dim myTempEntry As String = p_stringOfLists
    '        Dim myTempList As New List(Of Double)
    '        Dim demarcator As String = ""
    '        Dim noDemarcator As Boolean

    '        While Not myTempEntry = ""                    'Check for what type of demarcator is used between entries
    '            'Get the entry & add it to the list
    '            noDemarcator = False
    '            If StringExistInName(myTempEntry, ",") Then
    '                demarcator = ","
    '            ElseIf StringExistInName(myTempEntry, ";") Then
    '                demarcator = ";"
    '            ElseIf StringExistInName(myTempEntry, " ") Then
    '                demarcator = " "
    '            Else
    '                noDemarcator = True
    '            End If

    '            'Get the last entry in the list
    '            mytempString = demarcator & GetSuffix(myTempEntry, demarcator)
    '            mytempStringTrimmed = Trim(GetSuffix(myTempEntry, demarcator))

    '            'Check if last entry is a range, and treat accordingly
    '            If (StringExistInName(mytempStringTrimmed, "-") Or StringExistInName(mytempStringTrimmed, " - ")) Then 'Entry might be a range
    '                'Check if hyphen is a range or value sign
    '                If Not Left(mytempStringTrimmed, 1) = "-" Then             'Entry is a range. Write range out discreetly.
    '                    ParseRangeToString(mytempStringTrimmed, myTempList)
    '                End If
    '            Else                                                    'Add entry to list
    '                myTempList.Add(CDbl(mytempStringTrimmed))
    '            End If

    '            'Strip the model ID from the string
    '            If noDemarcator Then
    '                myTempEntry = Trim(FilterStringFromName(myTempEntry, mytempStringTrimmed, True, False))
    '            Else
    '                myTempEntry = Trim(FilterStringFromName(myTempEntry, mytempString, True, False))
    '            End If

    '            'myTempEntry = Trim(FilterStringFromName(myTempEntry, demarcator, True, False))
    '        End While

    '        If Not myTempList.Count = 0 Then        'List is not empty, do appropriate operations
    '            'Sort list, or reverse order, as list was compiled backwards
    '            If p_sortList Then
    '                myTempList.Sort()
    '            Else
    '                myTempList.Reverse()
    '            End If

    '            'Transfer list to string list
    '            For Each myEntry As Double In myTempList
    '                p_listOfStrings.Add(CStr(myEntry))
    '            Next
    '        End If
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Takes a specified range and parses it to a string list.
    ' ''' </summary>
    ' ''' <param name="p_rangeString">String containing the range specification, e.g. '5-10'.</param>
    ' ''' <param name="p_rangeCollection">Collection to add the range numbers to.</param>
    ' ''' <param name="p_numIntegerSpaces">Optional: Number of integer spaces expected, e.g. 3 for 123. Adds leading zeros if greater than result, e.g. 001. 
    ' ''' No leading zeros will be added if not specified.</param>
    ' ''' <remarks></remarks>
    'Sub ParseRangeToString(ByVal p_rangeString As String,
    '                       ByRef p_rangeCollection As List(Of Double),
    '                       Optional ByVal p_numIntegerSpaces As Integer = 1)
    '    Dim minRange As Integer = myCInt(Trim(GetPrefix(p_rangeString, "-")))
    '    Dim maxRange As Integer = myCInt(Trim(GetSuffix(p_rangeString, "-")))
    '    Dim currentCount As Integer = 0
    '    Dim currentCountString As String

    '    For i = minRange To maxRange
    '        currentCount = i

    '        currentCountString = CStr(currentCount)

    '        'Add preceding zeros to string if it has space
    '        While Len(currentCountString) < p_numIntegerSpaces
    '            currentCountString = "0" & currentCountString
    '        End While

    '        p_rangeCollection.Add(CDbl(currentCountString))
    '    Next

    'End Sub

    ' ''' <summary>
    ' ''' Sorts a list, and takes any collection of lists correlated with the sorted list and sorts then such that they remain in sync.
    ' ''' </summary>
    ' ''' <param name="p_sortList">List to be sorted.</param>
    ' ''' <param name="p_correlatedLists">Lists to be sorted based on the originally sorted list.</param>
    ' ''' <param name="p_sortAscending">If True, list will be sorted in ascending order. If False, list will be sorted in descending order.</param>
    ' ''' <remarks></remarks>
    'Sub SortCorrelatedLists(ByRef p_sortList As ObservableCollection(Of String),
    '                        ByRef p_correlatedLists As ObservableCollection(Of ObservableCollection(Of String)),
    '                        ByVal p_sortAscending As Boolean)
    '    Dim tempSortList As New ObservableCollection(Of String)
    '    Dim indexSortList As New ObservableCollection(Of Integer)
    '    Dim tempCorrelatedLists As New ObservableCollection(Of ObservableCollection(Of String))
    '    Dim tempCorrelatedList As ObservableCollection(Of String)
    '    Dim currentMax As String
    '    Dim j As Integer
    '    Dim correlatedIndex As Integer

    '    Try
    '        'Sort the sortList as ascending
    '        For i = 0 To p_sortList.Count - 1
    '            'Get the maximum entry that has not been added to the temp list, and add it. Save the corresponding index in the order in which the entry was added.
    '            currentMax = ""
    '            j = 0
    '            correlatedIndex = 0
    '            For Each entry As String In p_sortList
    '                If entry > currentMax Then
    '                    If Not ExistsInListString(entry, tempSortList) Then
    '                        currentMax = entry
    '                        correlatedIndex = j
    '                    End If
    '                End If
    '                j += 1
    '            Next
    '            tempSortList.Add(currentMax)
    '            indexSortList.Add(correlatedIndex)
    '        Next

    '        If Not p_sortAscending Then   'Reverse sorting
    '            tempSortList.Reverse()
    '            indexSortList.Reverse()
    '        End If

    '        'Assign new sorted list
    '        p_sortList = tempSortList

    '        'Sort the correlated lists
    '        For Each correlatedList As ObservableCollection(Of String) In p_correlatedLists
    '            tempCorrelatedList = New ObservableCollection(Of String)
    '            For Each sortIndex As Integer In indexSortList
    '                tempCorrelatedList.Add(correlatedList(sortIndex))
    '            Next
    '            tempCorrelatedLists.Add(tempCorrelatedList)
    '        Next

    '        'Assign new correlated lists
    '        p_correlatedLists = tempCorrelatedLists
    '    Catch ex As Exception
    '        csiLogger.ExceptionAction(ex)
    '    End Try
    'End Sub

    ' ''' <summary>
    ' ''' Returns a new list that is a combination of the supplied lists.
    ' ''' </summary>
    ' ''' <param name="p_listNew">The list to be added from.</param>
    ' ''' <param name="p_listBase">The list to be added to.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function CombineLists(ByVal p_listNew As ObservableCollection(Of String),
    '                             ByVal p_listBase As ObservableCollection(Of String)) As ObservableCollection(Of String)
    '    For Each newItem As String In p_listNew
    '        p_listBase.Add(newItem)
    '    Next

    '    Return p_listBase
    'End Function

    ' ''' <summary>
    ' ''' Returns a new list that is a combination of the supplied lists, with only unique entries.
    ' ''' </summary>
    ' ''' <param name="p_listNew">The list to be added from.</param>
    ' ''' <param name="p_listBase">The list to be added to.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function CombineListsUnique(ByVal p_listNew As ObservableCollection(Of String),
    '                                    ByVal p_listBase As ObservableCollection(Of String)) As ObservableCollection(Of String)
    '    Dim uniqueList As New ObservableCollection(Of String)

    '    uniqueList = CombineLists(p_listNew, p_listBase)
    '    ConvertToUniqueObsColString(uniqueList)

    '    Return uniqueList
    'End Function

    ''Not Used
    ' ''' <summary>
    ' ''' Creates a list of queries, written in a syntax understood by the tableset class, for filtering rows.
    ' ''' </summary>
    ' ''' <param name="p_list">List of queries.</param>
    ' ''' <remarks></remarks>
    'Function CreateQueryLists(ByVal p_list As List(Of String)) As List(Of String) 'ObservableCollection(Of Dictionary(Of String, String))
    '    'Dim myDictionaryLists As New ObservableCollection(Of Dictionary(Of String, String))
    '    Dim tempList As New List(Of String)

    '    For Each query As String In p_list
    '        '   myDictionaryLists.Add(CreateDictionaryList(query))
    '        tempList.Add(AssembleQuery(CreateDictionaryList(query)))
    '    Next

    '    Return tempList

    'End Function

    ''Not Used
    ' ''' <summary>
    ' ''' Creates a list of dictionary lists of 'Header'-'Value' pairings for each query.
    ' ''' </summary>
    ' ''' <param name="p_list">List of queries.</param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Function CreateDictionaryLists(ByVal p_list As List(Of String)) As ObservableCollection(Of Dictionary(Of String, String))
    '    Dim dictionaryLists As New ObservableCollection(Of Dictionary(Of String, String))
    '    For Each query As String In p_list
    '        dictionaryLists.Add(CreateDictionaryList(query))
    '    Next

    '    Return dictionaryLists
    'End Function
End Module
