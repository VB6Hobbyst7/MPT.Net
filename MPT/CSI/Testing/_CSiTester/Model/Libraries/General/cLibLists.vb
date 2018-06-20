Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel
Imports CSiTester.cLibQueries
Imports CSiTester.cLibStringConversion
Imports CSiTester.cLibPath

Public NotInheritable Class cLibLists

    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub

#Region "Parsing"
    ''' <summary>
    ''' Parses Query into a dictionary list of header-value pairs, with a new entry for each 'AND'.
    ''' </summary>
    ''' <param name="p_query">String containting the Query, with only the 'LIKE' 'AND' components.</param>
    ''' <remarks></remarks>
    Public Shared Function ParseQueryToDictionary(ByVal p_query As String) As Dictionary(Of String, String)
        Dim myDictionaryList As New Dictionary(Of String, String)
        Dim keyStart As Integer = 1
        Dim keyEnd As Integer
        Dim valStart As Integer
        Dim valEnd As Integer
        Try
            If p_query.Count > 0 Then
                For i = 1 To Len(p_query)

                    If StringsMatch(Mid(p_query, i, 4), "LIKE") Then
                        keyEnd = i - 2
                        valStart = i + 5
                    ElseIf StringsMatch(Mid(p_query, i, 3), "AND") Then
                        valEnd = i - 2
                        myDictionaryList.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
                        keyStart = i + 4
                    ElseIf i = Len(p_query) Then
                        valEnd = i
                        myDictionaryList.Add(Mid(p_query, keyStart, keyEnd - keyStart + 1), Mid(p_query, valStart, valEnd - valStart + 1))
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

        ParseQueryToDictionary = myDictionaryList
    End Function

    ''' <summary>
    ''' Returns a list of string items that are broken up from the provided string based on the character provided.
    ''' </summary>
    ''' <param name="p_string">String to parse.</param>
    ''' <param name="p_demarcator">Indication of the ending of a list entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseStringToList(ByVal p_string As String,
                                             ByVal p_demarcator As String) As List(Of String)
        Dim parsedList As New List(Of String)
        Dim tempWord As String = ""

        If (String.IsNullOrEmpty(p_string) OrElse String.IsNullOrEmpty(p_demarcator)) Then Return parsedList

        For i = 1 To p_string.Length
            Dim character As String = Mid(p_string, i, 1)
            If character = p_demarcator Then
                TrimWhiteSpace(tempWord)
                parsedList.Add(tempWord)
                tempWord = ""
            Else
                tempWord &= character
            End If
        Next

        If Not String.IsNullOrEmpty(tempWord) Then
            TrimWhiteSpace(tempWord)
            parsedList.Add(tempWord)
        End If

        Return parsedList
    End Function

    ''' <summary>
    ''' Takes a list of string numbers as a single string and separates them as entries in a list, or takes a list of string numbers and appends them into a single string.
    ''' </summary>
    ''' <param name="p_listOfStrings">List of the string items.</param>
    ''' <param name="p_stringOfLists">Single string containing the list of items.</param>
    ''' <param name="p_readList">True: A single string of appended list items is generated from the list. False: A list of string items is extracted from a list within a stingle string.</param>
    ''' <param name="p_sortList">True: List will be sorted. False: List will be kept in the order provided.</param>
    ''' <remarks></remarks>
    Public Shared Sub ParseListString(ByRef p_listOfStrings As IList(Of String),
                        ByRef p_stringOfLists As String,
                        ByVal p_readList As Boolean,
                        Optional p_sortList As Boolean = True)
        If p_readList Then                                'Write model IDs from collection to a single string, separated by commas
            p_stringOfLists = ""
            For i = 0 To p_listOfStrings.Count - 1
                If i = 0 Then
                    p_stringOfLists = p_listOfStrings(i)
                Else
                    p_stringOfLists = p_stringOfLists & ", " & p_listOfStrings(i)
                End If
            Next
        Else                                            'Find the model IDs and add them to the list
            Dim mytempString As String
            Dim mytempStringTrimmed As String
            Dim myTempEntry As String = p_stringOfLists
            Dim myTempList As New List(Of Double)
            Dim demarcator As String = ""
            Dim noDemarcator As Boolean

            While Not myTempEntry = ""                    'Check for what type of demarcator is used between entries
                'Get the entry & add it to the list
                noDemarcator = False
                If StringExistInName(myTempEntry, ",") Then
                    demarcator = ","
                ElseIf StringExistInName(myTempEntry, ";") Then
                    demarcator = ";"
                ElseIf StringExistInName(myTempEntry, " ") Then
                    demarcator = " "
                Else
                    noDemarcator = True
                End If

                'Get the last entry in the list
                mytempString = demarcator & GetSuffix(myTempEntry, demarcator)
                mytempStringTrimmed = Trim(GetSuffix(myTempEntry, demarcator))

                'Check if last entry is a range, and treat accordingly
                If (StringExistInName(mytempStringTrimmed, "-") Or StringExistInName(mytempStringTrimmed, " - ")) Then 'Entry might be a range
                    'Check if hyphen is a range or value sign
                    If Not Left(mytempStringTrimmed, 1) = "-" Then             'Entry is a range. Write range out discreetly.
                        ParseRangeToString(mytempStringTrimmed, myTempList)
                    End If
                Else                                                    'Add entry to list
                    myTempList.Add(CDbl(mytempStringTrimmed))
                End If

                'Strip the model ID from the string
                If noDemarcator Then
                    myTempEntry = Trim(FilterStringFromName(myTempEntry, mytempStringTrimmed, True, False))
                Else
                    myTempEntry = Trim(FilterStringFromName(myTempEntry, mytempString, True, False))
                End If

                'myTempEntry = Trim(FilterStringFromName(myTempEntry, demarcator, True, False))
            End While

            If Not myTempList.Count = 0 Then        'List is not empty, do appropriate operations
                'Sort list, or reverse order, as list was compiled backwards
                If p_sortList Then
                    myTempList.Sort()
                Else
                    myTempList.Reverse()
                End If

                'Transfer list to string list
                For Each myEntry As Double In myTempList
                    p_listOfStrings.Add(CStr(myEntry))
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Takes a specified range and parses it to a string list.
    ''' </summary>
    ''' <param name="p_rangeString">String containing the range specification, e.g. '5-10'.</param>
    ''' <param name="p_rangeCollection">Collection to add the range numbers to.</param>
    ''' <param name="p_numIntegerSpaces">Optional: Number of integer spaces expected, e.g. 3 for 123. Adds leading zeros if greater than result, e.g. 001. 
    ''' No leading zeros will be added if not specified.</param>
    ''' <remarks></remarks>
    Public Shared Sub ParseRangeToString(ByVal p_rangeString As String,
                           ByRef p_rangeCollection As List(Of Double),
                           Optional ByVal p_numIntegerSpaces As Integer = 1)
        Dim minRange As Integer = myCInt(Trim(GetPrefix(p_rangeString, "-")))
        Dim maxRange As Integer = myCInt(Trim(GetSuffix(p_rangeString, "-")))
        Dim currentCount As Integer = 0
        Dim currentCountString As String

        For i = minRange To maxRange
            currentCount = i

            currentCountString = CStr(currentCount)

            'Add preceding zeros to string if it has space
            While Len(currentCountString) < p_numIntegerSpaces
                currentCountString = "0" & currentCountString
            End While

            p_rangeCollection.Add(CDbl(currentCountString))
        Next

    End Sub


    'Not Used
    ''' <summary>
    ''' Creates a list of queries, written in a syntax understood by the tableset class, for filtering rows.
    ''' </summary>
    ''' <param name="p_list">List of queries.</param>
    ''' <remarks></remarks>
    Public Shared Function CreateQueryLists(ByVal p_list As List(Of String)) As List(Of String) 'ObservableCollection(Of Dictionary(Of String, String))
        'Dim myDictionaryLists As New ObservableCollection(Of Dictionary(Of String, String))
        Dim tempList As New List(Of String)

        For Each query As String In p_list
            '   myDictionaryLists.Add(CreateDictionaryList(query))
            tempList.Add(AssembleQuery(ParseQueryToDictionary(query)))
        Next

        Return tempList

    End Function

    'Not Used
    ''' <summary>
    ''' Creates a list of dictionary lists of 'Header'-'Value' pairings for each query.
    ''' </summary>
    ''' <param name="p_list">List of queries.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateDictionaryLists(ByVal p_list As List(Of String)) As ObservableCollection(Of Dictionary(Of String, String))
        Dim dictionaryLists As New ObservableCollection(Of Dictionary(Of String, String))
        For Each query As String In p_list
            dictionaryLists.Add(ParseQueryToDictionary(query))
        Next

        Return dictionaryLists
    End Function
#End Region


#Region "Exists in List"
    ''' <summary>
    ''' Returns 'true' if the provided item exists in the provided list.
    ''' </summary>
    ''' <param name="p_item">Item to search for in the list.</param>
    ''' <param name="p_list">List to search.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInList(ByVal p_item As String,
                                        ByVal p_list As List(Of String),
                                        Optional ByVal p_caseSensitive As Boolean = False) As Boolean
        For Each item As String In p_list
            If StringsMatch(item, p_item, p_caseSensitive) Then Return True
        Next

        Return False
    End Function

    ''' <summary>
    ''' Returns true/false depending on whether the entry provided exists in the list provided.
    ''' </summary>
    ''' <param name="p_entry">Value to search for in the list.</param>
    ''' <param name="p_list">List to search within for the value.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInListString(ByVal p_entry As String,
                                              ByVal p_list As List(Of String),
                                              Optional ByVal p_caseSensitive As Boolean = False) As Boolean
        Try
            If p_list Is Nothing Then Return False

            For Each listEntry As String In p_list
                If StringsMatch(p_entry, listEntry, p_caseSensitive) Then
                    Return True
                End If
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try

        Return False
    End Function

    ''' <summary>
    ''' Returns true/false depending on whether the entry provided exists in the list provided.
    ''' </summary>
    ''' <param name="p_entry">Value to search for in the list.</param>
    ''' <param name="p_list">List to search within for the value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInListInteger(ByVal p_entry As Integer,
                                                ByVal p_list As ObservableCollection(Of Integer)) As Boolean
        ExistsInListInteger = False
        Try
            If p_list Is Nothing Then Exit Function

            For Each myListEntry As Integer In p_list
                If p_entry = myListEntry Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical. Ordering and capitalization are not considered.
    ''' </summary>
    ''' <param name="p_innerList">One list to compare.</param>
    ''' <param name="p_outerList">Another list to compare.</param>
    ''' <param name="p_considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal p_innerList As List(Of String),
                                                        ByVal p_outerList As List(Of String),
                                                        Optional ByVal p_considerOrder As Boolean = False) As Boolean
        Dim isMatch As Boolean = False

        If Not p_outerList.Count = p_innerList.Count Then Return True

        If p_considerOrder Then
            For i = 0 To p_innerList.Count - 1
                If p_innerList(i) = p_outerList(i) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As String In p_outerList
                For Each itemInner As String In p_innerList
                    If StringsMatch(itemInner, itemOuter) Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
            Next
        End If

        Return Not isMatch
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical. Ordering is not considered.
    ''' </summary>
    ''' <param name="p_innerList">One list to compare.</param>
    ''' <param name="p_outerList">Another list to compare.</param>
    ''' <param name="p_considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal p_innerList As ObservableCollection(Of String),
                                                        ByVal p_outerList As ObservableCollection(Of String),
                                                        Optional ByVal p_considerOrder As Boolean = False) As Boolean
        Dim isMatch As Boolean = False

        If Not p_outerList.Count = p_innerList.Count Then Return True

        If p_considerOrder Then
            For i = 0 To p_innerList.Count - 1
                If p_innerList(i) = p_outerList(i) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As String In p_outerList
                For Each itemInner As String In p_innerList
                    If StringsMatch(itemInner, itemOuter) Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
            Next
        End If

        Return Not isMatch
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical.
    ''' </summary>
    ''' <param name="p_innerList">One list to compare.</param>
    ''' <param name="p_outerList">Another list to compare.</param>
    ''' <param name="p_considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal p_innerList As List(Of Double),
                                                        ByVal p_outerList As List(Of Double),
                                                        Optional ByVal p_considerOrder As Boolean = False) As Boolean
        Dim isMatch As Boolean = False

        If Not p_outerList.Count = p_innerList.Count Then Return True

        If p_considerOrder Then
            For i = 0 To p_innerList.Count - 1
                If p_innerList(i) = p_outerList(i) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As Double In p_outerList
                For Each itemInner As Double In p_innerList
                    If itemInner = itemOuter Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
            Next
        End If
        
        Return Not isMatch
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical.
    ''' </summary>
    ''' <param name="p_innerList">One list to compare.</param>
    ''' <param name="p_outerList">Another list to compare.</param>
    ''' <param name="p_considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal p_innerList As List(Of Integer),
                                                        ByVal p_outerList As List(Of Integer),
                                                        Optional ByVal p_considerOrder As Boolean = False) As Boolean
        Dim isMatch As Boolean = False

        If Not p_outerList.Count = p_innerList.Count Then Return True

        If p_considerOrder Then
            For i = 0 To p_innerList.Count - 1
                If p_innerList(i) = p_outerList(i) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As Integer In p_outerList
                For Each itemInner As Integer In p_innerList
                    If itemInner = itemOuter Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
            Next
        End If

        Return Not isMatch
    End Function
#End Region


#Region "Unique Lists: Create, Add"
    ''' <summary>
    ''' Given a new list and base list, if the new list has unique entries, they are added to the base list. The base list can start out empty to create a new unique list from an existing list.
    ''' </summary>
    ''' <param name="p_newList">New list to check.</param>
    ''' <param name="p_baseList">Unique list to check against.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Sub CreateUniqueListString(ByVal p_newList As ObservableCollection(Of String),
                                            ByRef p_baseList As ObservableCollection(Of String),
                                            Optional ByVal p_caseSensitive As Boolean = False)
        Try
            Dim entryMatch As Boolean

            If (p_newList Is Nothing OrElse
                p_baseList Is Nothing) Then Exit Sub

            For Each myNewEntry As String In p_newList
                entryMatch = False
                If Not String.IsNullOrEmpty(myNewEntry) Then

                    For Each myBaseEntry As String In p_baseList
                        If StringsMatch(myNewEntry, myBaseEntry, p_caseSensitive) Then
                            entryMatch = True
                            Exit For
                        End If
                    Next
                    If Not entryMatch Then p_baseList.Add(myNewEntry)
                End If
            Next
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="p_originalList">Original list to check for redundancies.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal p_originalList As List(Of String),
                                                         Optional ByVal p_caseSensitive As Boolean = False) As List(Of String)
        Try
            Dim entryMatch As Boolean
            Dim myNewList As New List(Of String)

            If p_originalList Is Nothing Then Return myNewList

            If p_originalList.Count > 0 Then
                For Each myOriginalEntry As String In p_originalList
                    entryMatch = False
                    If Not String.IsNullOrEmpty(myOriginalEntry) Then
                        For Each myNewEntry As String In myNewList
                            If StringsMatch(myNewEntry, myOriginalEntry, p_caseSensitive) Then
                                entryMatch = True
                                Exit For
                            End If
                        Next
                        If Not entryMatch Then myNewList.Add(myOriginalEntry)
                    End If
                Next
            End If

            p_originalList.Clear()
            p_originalList = myNewList

            Return p_originalList
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)

            Return p_originalList
        End Try
    End Function
    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="p_originalList">Original list to check for redundancies.</param>
    ''' <param name="p_sortList">If true, then the new unique list will be sorted lowest to highest.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal p_originalList As List(Of Integer),
                                                          Optional ByVal p_sortList As Boolean = True) As List(Of Integer)
        Try
            Dim entryMatch As Boolean
            Dim myNewList As New List(Of Integer)

            If p_originalList Is Nothing Then Return myNewList

            If p_originalList.Count > 0 Then
                For Each myOriginalEntry As Integer In p_originalList
                    entryMatch = False
                    For Each myNewEntry As Integer In myNewList
                        If myNewEntry = myOriginalEntry Then
                            entryMatch = True
                            Exit For
                        End If
                    Next
                    If Not entryMatch Then myNewList.Add(myOriginalEntry)
                Next
            End If

            p_originalList.Clear()
            p_originalList = myNewList

            If p_sortList Then p_originalList.Sort()

            Return p_originalList
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)

            Return p_originalList
        End Try
    End Function
    ''' <summary>
    ''' Checks the observable collection provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="p_originalList">Original list to check for redundancies.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal p_originalList As ObservableCollection(Of String),
                                                         Optional ByVal p_caseSensitive As Boolean = False) As ObservableCollection(Of String)
        Try
            Dim entryMatch As Boolean
            Dim myNewList As New ObservableCollection(Of String)

            If p_originalList Is Nothing Then Return myNewList

            If p_originalList.Count > 0 Then
                For Each myOriginalEntry As String In p_originalList
                    entryMatch = False
                    If Not String.IsNullOrEmpty(myOriginalEntry) Then
                        For Each myNewEntry As String In myNewList
                            If StringsMatch(myNewEntry, myOriginalEntry, p_caseSensitive) Then
                                entryMatch = True
                                Exit For
                            End If
                        Next
                        If Not entryMatch Then myNewList.Add(myOriginalEntry)
                    End If
                Next
            End If

            p_originalList.Clear()
            p_originalList = myNewList

            Return p_originalList
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)

            Return p_originalList
        End Try
    End Function

    ''' <summary>
    ''' Checks if a particular string exists in a provided list of strings. If it does not, it is added in the specified first/last entry of the list.
    ''' </summary>
    ''' <param name="p_list">List of string elements to check and,if necessary, modify.</param>
    ''' <param name="p_listItem">List item to check against the list of string elements.</param>
    ''' <param name="p_placeFirst">If true, and the list item is added, it will be added as the first item in the new list. If false, the list item will be added as the last item in the list.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Sub AddToListStringIfNew(ByRef p_list As List(Of String),
                                           ByVal p_listItem As String,
                                           ByVal p_placeFirst As Boolean,
                                           Optional ByVal p_caseSensitive As Boolean = False)
        Dim tempList As New List(Of String)
        Dim newToList As Boolean = True

        'Check it item is unique
        For Each listItem As String In p_list
            If StringsMatch(listItem, p_listItem, p_caseSensitive) Then
                newToList = False
                Exit For
            End If
        Next

        'If unique, add to the desired place in the list
        If newToList Then
            'Add to temp list
            If p_placeFirst Then
                tempList.Add(p_listItem)
                For Each ListItem As String In p_list
                    tempList.Add(ListItem)
                Next
            Else
                For Each ListItem As String In p_list
                    tempList.Add(ListItem)
                Next
                tempList.Add(p_listItem)
            End If

            'Replace provided list with updated temp list
            p_list.Clear()
            p_list = tempList
        End If
    End Sub

    ''' <summary>
    ''' Checks if a particular string exists in a provided list of strings. If it does not, it is added in the specified first/last entry of the list.
    ''' </summary>
    ''' <param name="p_list">List of string elements to check and,if necessary, modify.</param>
    ''' <param name="p_listItem">List item to check against the list of string elements.</param>
    ''' <param name="p_placeFirst">If true, and the list item is added, it will be added as the first item in the new list. If false, the list item will be added as the last item in the list.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Sub AddToObsColStringIfNew(ByRef p_list As ObservableCollection(Of String),
                                               ByVal p_listItem As String,
                                               Optional ByVal p_placeFirst As Boolean = False,
                                                Optional ByVal p_caseSensitive As Boolean = False)
        Dim tempList As New ObservableCollection(Of String)
        Dim newToList As Boolean = True
        If String.IsNullOrEmpty(p_listItem) Then Exit Sub

        'Check if item is unique
        For Each listItem As String In p_list
            If StringsMatch(listItem, p_listItem, p_caseSensitive) Then
                newToList = False
                Exit For
            End If
        Next

        'If unique, add to the desired place in the list
        If newToList Then
            'Add to temp list
            If p_placeFirst Then
                tempList.Add(p_listItem)
                For Each ListItem As String In p_list
                    tempList.Add(ListItem)
                Next
            Else
                For Each ListItem As String In p_list
                    tempList.Add(ListItem)
                Next
                tempList.Add(p_listItem)
            End If

            'Replace provided list with updated temp list
            p_list.Clear()
            p_list = tempList
        End If
    End Sub
#End Region


#Region "List Conversions"

    ''' <summary>
    ''' Converts list of string to an observable collection of string, or the reverse.
    ''' </summary>
    ''' <param name="p_list">List of items to either convert, or fill.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertListToObservableCollection(Of T)(ByRef p_list As List(Of T)) As ObservableCollection(Of T)
        Dim newObsCol As New ObservableCollection(Of T)

        For Each item As T In p_list
            newObsCol.Add(item)
        Next

        Return newObsCol
    End Function

    ''' <summary>
    ''' Converts any list of one type to another type if the types are inheritable.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <typeparam name="U"></typeparam>
    ''' <param name="p_list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Convert(Of T, U)(ByVal p_list As List(Of T)) As List(Of U)
        Dim newList As New List(Of U)
        If GetType(U).IsAssignableFrom(GetType(U)) Then
            For Each item As T In p_list
                newList.Add(DirectCast(DirectCast(item, Object), U))
            Next
        End If

        Return newList
    End Function
#End Region


#Region "Remove, Sort"
    ''' <summary>
    ''' Removes any blank entries in the list.
    ''' </summary>
    ''' <param name="p_list">List to trim.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TrimListOfEmptyItems(ByVal p_list As ICollection(Of String)) As ICollection(Of String)
        Dim tempList As New List(Of String)

        For Each item As String In p_list
            If Not String.IsNullOrWhiteSpace(item) Then tempList.Add(item)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Clears all blank entries from an observable collection of strings.
    ''' </summary>
    ''' <param name="p_list">List of string entries.</param>
    ''' <remarks></remarks>
    Public Shared Sub ClearBlankEntries(ByRef p_list As ObservableCollection(Of String))
        Dim myTempList As New ObservableCollection(Of String)

        For Each entry As String In p_list
            If Not String.IsNullOrEmpty(entry) Then myTempList.Add(entry)
        Next

        p_list.Clear()
        p_list = myTempList
    End Sub

    ''' <summary>
    ''' Removes a list of string items from another list of string items.
    ''' </summary>
    ''' <param name="p_removeList">List of string items to be removed from the base list.</param>
    ''' <param name="p_baseList">Base list from which to remove the provided items.</param>
    ''' <param name="p_caseSensitive">If true, then the differences in capitalization will void a potential match. 
    ''' If false, then a match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Sub RemoveFromList(ByVal p_removeList As ObservableCollection(Of String),
                                     ByRef p_baseList As ObservableCollection(Of String),
                                     Optional ByVal p_caseSensitive As Boolean = False)
        Dim entryMatch As Boolean
        Dim tempList As New ObservableCollection(Of String)

        For Each myBaseEntry As String In p_baseList
            entryMatch = False
            For Each myRemoveEntry As String In p_removeList
                If StringsMatch(myRemoveEntry, myBaseEntry, p_caseSensitive) Then
                    entryMatch = True
                    Exit For
                End If
            Next
            If Not entryMatch Then tempList.Add(myBaseEntry)
        Next

        p_baseList = tempList
    End Sub

    ''' <summary>
    ''' Sorts a list, and takes any collection of lists correlated with the sorted list and sorts then such that they remain in sync.
    ''' </summary>
    ''' <param name="p_sortList">List to be sorted.</param>
    ''' <param name="p_correlatedLists">Lists to be sorted based on the originally sorted list.</param>
    ''' <param name="p_sortAscending">True: List will be sorted in ascending order. False: List will be sorted in descending order.</param>
    ''' <remarks></remarks>
    Public Shared Sub SortCorrelatedLists(ByRef p_sortList As List(Of String),
                            ByRef p_correlatedLists As List(Of List(Of String)),
                            ByVal p_sortAscending As Boolean)
        Dim tempSortList As New List(Of String)
        Dim indexSortList As New List(Of Integer)
        Dim tempCorrelatedLists As New List(Of List(Of String))
        Dim tempCorrelatedList As List(Of String)
        Dim currentMax As String
        Dim j As Integer
        Dim correlatedIndex As Integer

        Try
            'Sort the sortList as ascending
            For i = 0 To p_sortList.Count - 1
                'Get the maximum entry that has not been added to the temp list, and add it. Save the corresponding index in the order in which the entry was added.
                currentMax = ""
                j = 0
                correlatedIndex = 0
                For Each entry As String In p_sortList
                    If entry > currentMax Then
                        If Not ExistsInListString(entry, tempSortList) Then
                            currentMax = entry
                            correlatedIndex = j
                        End If
                    End If
                    j += 1
                Next
                tempSortList.Add(currentMax)
                indexSortList.Add(correlatedIndex)
            Next

            If Not p_sortAscending Then   'Reverse sorting
                tempSortList.Reverse()
                indexSortList.Reverse()
            End If

            'Assign new sorted list
            p_sortList = tempSortList

            'Sort the correlated lists
            For Each correlatedList As List(Of String) In p_correlatedLists
                tempCorrelatedList = New List(Of String)
                For Each sortIndex As Integer In indexSortList
                    tempCorrelatedList.Add(correlatedList(sortIndex))
                Next
                tempCorrelatedLists.Add(tempCorrelatedList)
            Next

            'Assign new correlated lists
            p_correlatedLists = tempCorrelatedLists
        Catch ex As Exception
            csiLogger.ExceptionAction(ex)
        End Try
    End Sub
#End Region


#Region "Combine Lists"
    ''' <summary>
    ''' Appends one list to another.
    ''' </summary>
    ''' <param name="p_listOriginal"></param>
    ''' <param name="p_listAppend"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AppendList(ByVal p_listOriginal As List(Of String),
                                        ByVal p_listAppend As List(Of String)) As List(Of String)

        For Each unit As String In p_listAppend
            p_listOriginal.Add(unit)
        Next

        Return p_listOriginal
    End Function

    ''' <summary>
    ''' Returns a new list that is a combination of the supplied lists.
    ''' </summary>
    ''' <param name="p_listNew">The list to be added from.</param>
    ''' <param name="p_listBase">The list to be added to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineLists(ByVal p_listNew As ObservableCollection(Of String),
                                 ByVal p_listBase As ObservableCollection(Of String)) As ObservableCollection(Of String)
        For Each newItem As String In p_listNew
            p_listBase.Add(newItem)
        Next

        Return p_listBase
    End Function

    ''' <summary>
    ''' Returns a new list that is a combination of the supplied lists, with only unique entries.
    ''' </summary>
    ''' <param name="p_listNew">The list to be added from.</param>
    ''' <param name="p_listBase">The list to be added to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineListsUnique(ByVal p_listNew As ObservableCollection(Of String),
                                        ByVal p_listBase As ObservableCollection(Of String)) As ObservableCollection(Of String)
        Dim uniqueList As New ObservableCollection(Of String)

        uniqueList = CombineLists(p_listNew, p_listBase)
        uniqueList = ConvertToUniqueList(uniqueList)

        Return uniqueList
    End Function
#End Region


End Class
