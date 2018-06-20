Option Strict On
Option Explicit On

Imports System.Collections.ObjectModel

Imports MPT.String.StringLibrary

Public NotInheritable Class ListLibrary
    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end
#Region "Parsing"


    ''' <summary>
    ''' Returns a list of string items that are broken up from the provided string based on the character provided.
    ''' </summary>
    ''' <param name="value">String to parse.</param>
    ''' <param name="demarcator">Indication of the ending of a list entry.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParseStringToList(ByVal value As String,
                                             ByVal demarcator As String) As List(Of String)
        Dim parsedList As New List(Of String)
        If (String.IsNullOrEmpty(value)) Then Return parsedList
        If (String.IsNullOrEmpty(demarcator)) Then 
            parsedList.Add(value)
            Return parsedList
        End If
        
        Dim tempWord As String = ""
        For i = 1 To value.Length
            Dim character As String = Mid(value, i, 1)
            If character = demarcator Then
                tempWord = tempWord.Trim()
                parsedList.Add(tempWord)
                tempWord = ""
            Else
                tempWord &= character
            End If
        Next

        If Not String.IsNullOrEmpty(tempWord) Then
            tempWord = tempWord.Trim()
            parsedList.Add(tempWord)
        End If

        Return parsedList
    End Function

    ''' <summary>
    ''' Parses the list to string.
    ''' </summary>
    ''' <param name="listOfStrings">The list of items to parse to a string representation.</param>
    ''' <returns>System.String.</returns>
    Public Shared Function ParseListToString(ByVal listOfStrings As IList(Of String)) As String
        If listOfStrings Is Nothing Then Return ""
        Dim stringOfLists As String = ""
        For i = 0 To listOfStrings.Count - 1
            If i = 0 Then
                stringOfLists = listOfStrings(i)
            Else
                stringOfLists = stringOfLists & ", " & listOfStrings(i)
            End If
        Next
        Return stringOfLists
    End Function


    ''' <summary>
    ''' Takes a list of string numbers as a single string and separates them as entries in a list, or takes a list of string numbers and appends them into a single string.
    ''' Negative numbers are not supported.
    ''' </summary>
    ''' <param name="stringOfLists">Single string containing the list of items.</param>
    ''' <param name="sortAscending">True: List will be sorted. 
    ''' False: List will be kept in the order provided.</param>
    ''' <remarks></remarks>
    Public Shared Function ParseNumericStringToList(ByVal stringOfLists As String,
                                                    Optional sortAscending As Boolean = True) As IList(Of String)
        If String.IsNullOrWhiteSpace(stringOfLists) Then Return New List(Of String)

        ' Check if range has negative numbers
        If (stringOfLists IsNot Nothing AndAlso 
            stringOfLists.Count > 0 AndAlso
            stringOfLists(0) = "-"c) Then Return New List(Of String)

        Dim numbersFromString As List(Of Double) = GetNumbersFromString(stringOfLists)
        If numbersFromString.Count = 0 Then Return New List(Of STring)

        'Sort list, or reverse order, as list was compiled backwards
        numbersFromString.Sort()
        If sortAscending Then
            numbersFromString.Sort()
        Else
            numbersFromString.Reverse()
        End If
        
        Return (From number In numbersFromString Select CStr(number)).ToList()
    End Function

    Private Shared Function GetNumbersFromString(ByVal stringOfLists As String) As List(Of Double)
        Dim numbersFromString As New List(Of Double)
        Dim remainingStringOfLists As String = stringOfLists
        While Not remainingStringOfLists = ""
            Dim demarcator As String = GetDemarcator(remainingStringOfLists)

            'Get the last entry & add it to the list
            Dim lastEntryInList As String = Trim(GetSuffix(remainingStringOfLists, demarcator))
            numbersFromString = UpdateNumbersFromString(lastEntryInList, numbersFromString)
            
            ' Strip last entry from the string
            If HasDemarcator(demarcator) Then 
                Dim lastEntryInListWithDemarcator As String = demarcator & GetSuffix(remainingStringOfLists, demarcator)
                remainingStringOfLists = Trim(FilterFromText(remainingStringOfLists, lastEntryInListWithDemarcator, True, False))
            Else
                remainingStringOfLists = Trim(FilterFromText(remainingStringOfLists, lastEntryInList, True, False))
            End If
        End While
        Return numbersFromString
    End Function

    Private Shared Function HasDemarcator(ByVal demarcator As STring) As Boolean
        Return Not (demarcator = "")
    End Function

    Private Shared Function GetDemarcator(ByVal remainingStringOfLists As String) As String
            If StringExistInName(remainingStringOfLists, ",") Then
                Return ","
            ElseIf StringExistInName(remainingStringOfLists, ";") Then
                Return ";"
            ElseIf StringExistInName(remainingStringOfLists, " ") Then
                Return " "
            Else
                Return ""
            End If
    End Function


    Private Shared Function UpdateNumbersFromString(ByVal lastEntryInList As String,
                                                    ByVal numbersFromString As List(Of Double)) As List(Of Double)
        Dim updatedNumbersFromString = numbersFromString
        If (EntryIsARange(lastEntryInList)) Then 
            'Write range out discreetly.
            updatedNumbersFromString = New List(Of Double)(ParseRangeToListOfDouble(lastEntryInList))
        Else                                                    
            'Add entry to list
            updatedNumbersFromString.Add(CDbl(lastEntryInList))
        End If
        Return updatedNumbersFromString
    End Function

    Private Shared Function EntryIsARange(ByVal lastEntryInList As String) As Boolean
        'Check if hyphen is present to indicate a range
        Dim numbersRanges As String() = lastEntryInList.Split("-"c)
        Return (numbersRanges.Count = 2)
    End Function


    ''' <summary>
    ''' Takes a specified range and parses it to a list.
    ''' Only positive values are supported.
    ''' Negative values with throw an exception.
    ''' </summary>
    ''' <param name="rangeString">String containing the range specification, e.g. '5-10'.</param>
    ''' <remarks></remarks>
    Public Shared Function ParseRangeToListOfDouble(ByVal rangeString As String) As IEnumerable(Of Double)
        If (rangeString IsNot Nothing AndAlso 
            rangeString.Count > 0 AndAlso
            rangeString(0) = "-"c) Then Throw New ArgumentException("Negative values are not supported.")

        Dim numbers As New List(Of Double)
        Dim minRangeDbl As Double
        If Not Double.TryParse(Trim(GetPrefix(rangeString, "-")), minRangeDbl) Then Return numbers
        minRangeDbl = Math.Round(minRangeDbl)
        Dim minRange As Integer = CInt(minRangeDbl)

        Dim maxRangeDbl As Double
        If Not Double.TryParse(Trim(GetSuffix(rangeString, "-")), maxRangeDbl) Then Return numbers
        maxRangeDbl = Math.Round(maxRangeDbl)
        Dim maxRange As Integer = CInt(maxRangeDbl)

        For currentCount As Integer = minRange To maxRange
            numbers.Add(currentCount)
        Next

        Return numbers
    End Function

    ''' <summary>
    ''' Takes a specified range and parses it to a list.
    ''' Only positive values are supported.
    ''' Negative values with throw an exception.
    ''' </summary>
    ''' <param name="rangeString">String containing the range specification, e.g. '5-10'.</param>
    ''' <remarks></remarks>
    Public Shared Function ParseRangeToListOfInteger(ByVal rangeString As String) As IEnumerable(Of Integer)
        If (rangeString IsNot Nothing AndAlso 
            rangeString.Count > 0 AndAlso
            rangeString(0) = "-"c) Then Throw New ArgumentException("Negative values are not supported.")

        Dim numbers As New List(Of Integer)
        Dim minRange As Integer
        If Not Integer.TryParse(Trim(GetPrefix(rangeString, "-")), minRange) Then Return numbers

        Dim maxRange As Integer
        If Not Integer.TryParse(Trim(GetSuffix(rangeString, "-")), maxRange) Then Return numbers

        For currentCount As Integer = minRange To maxRange
            numbers.Add(currentCount)
        Next

        Return numbers
    End Function
#End Region


#Region "Exists in List"
    ''' <summary>
    ''' Returns 'true' if the provided item exists in the provided list.
    ''' </summary>
    ''' <param name="entry">Item to search for in the list.</param>
    ''' <param name="list">List to search.</param>
    ''' <param name="caseSensitive">True: The differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInList(ByVal entry As String,
                                        ByVal list As IEnumerable(Of String),
                                        Optional ByVal caseSensitive As Boolean = False) As Boolean
        If list Is Nothing Then Return false
        Return list.Any(Function(item) StringsMatch(item, entry, caseSensitive))
    End Function

    ''' <summary>
    ''' Returns true/false depending on whether the entry provided exists in the list provided.
    ''' </summary>
    ''' <param name="entry">Value to search for in the list.</param>
    ''' <param name="list">List to search within for the value.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExistsInList(ByVal entry As Integer,
                                        ByVal list As IEnumerable(Of Integer)) As Boolean
        If list Is Nothing Then Return False
        Return list.Any(Function(myListEntry) entry = myListEntry)
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical. Ordering and capitalization are not considered.
    ''' </summary>
    ''' <param name="innerList">One list to compare.</param>
    ''' <param name="outerList">Another list to compare.</param>
    ''' <param name="considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal innerList As IList(Of String),
                                                        ByVal outerList As IList(Of String),
                                                        Optional ByVal considerOrder As Boolean = False,
                                                        Optional ByVal caseSensitive As Boolean = False) As Boolean
        If (innerList Is Nothing AndAlso outerList Is Nothing) Then Return False
        If ((innerList Is Nothing AndAlso outerList IsNot Nothing) OrElse
            (innerList IsNot Nothing AndAlso outerList Is Nothing)) Then Return True

        Dim isMatch As Boolean = False
        If Not outerList.Count = innerList.Count Then Return True

        If considerOrder Then
            For i = 0 To innerList.Count - 1
                If StringsMatch(innerList(i), outerList(i), caseSensitive) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As String In outerList
                For Each itemInner As String In innerList
                    If StringsMatch(itemInner, itemOuter, caseSensitive) Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
                If Not isMatch Then Return Not isMatch
            Next
        End If

        Return Not isMatch
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical.
    ''' </summary>
    ''' <param name="innerList">One list to compare.</param>
    ''' <param name="outerList">Another list to compare.</param>
    ''' <param name="considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal innerList As IList(Of Double),
                                                        ByVal outerList As IList(Of Double),
                                                        Optional ByVal considerOrder As Boolean = False,
                                                        Optional ByVal tolerance As Double = 1E-3) As Boolean
        If (innerList Is Nothing AndAlso outerList Is Nothing) Then Return False
        If ((innerList Is Nothing AndAlso outerList IsNot Nothing) OrElse
            (innerList IsNot Nothing AndAlso outerList Is Nothing)) Then Return True

        Dim isMatch As Boolean = False
        If Not outerList.Count = innerList.Count Then Return True

        If considerOrder Then
            For i = 0 To innerList.Count - 1
                If Math.Abs(innerList(i) - outerList(i)) < tolerance Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As Double In outerList
                For Each itemInner As Double In innerList
                    If Math.Abs(itemInner - itemOuter) < tolerance Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
                If Not isMatch Then Return Not isMatch
            Next
        End If

        Return Not isMatch
    End Function

    ''' <summary>
    ''' Returns true if the lists are not identical.
    ''' </summary>
    ''' <param name="innerList">One list to compare.</param>
    ''' <param name="outerList">Another list to compare.</param>
    ''' <param name="considerOrder">True: The order will be considered. False: The order will not be considered.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function ListsAreDifferent(ByVal innerList As IList(Of Integer),
                                                        ByVal outerList As IList(Of Integer),
                                                        Optional ByVal considerOrder As Boolean = False) As Boolean
        If (innerList Is Nothing AndAlso outerList Is Nothing) Then Return False
        If ((innerList Is Nothing AndAlso outerList IsNot Nothing) OrElse
            (innerList IsNot Nothing AndAlso outerList Is Nothing)) Then Return True

        Dim isMatch As Boolean = False
        If Not outerList.Count = innerList.Count Then Return True

        If considerOrder Then
            For i = 0 To innerList.Count - 1
                If innerList(i) = outerList(i) Then
                    isMatch = True
                Else
                    isMatch = False
                    Exit For
                End If
            Next
        Else
            For Each itemOuter As Integer In outerList
                For Each itemInner As Integer In innerList
                    If itemInner = itemOuter Then
                        isMatch = True
                        Exit For
                    Else
                        isMatch = False
                    End If
                Next
                If Not isMatch Then Return Not isMatch
            Next
        End If

        Return Not isMatch
    End Function
#End Region


#Region "Unique Lists: Create, Add"

    ''' <summary>
    ''' Given a new list and base list, if the new list has unique entries, they are added to the base list. 
    ''' The base list can start out empty to create a new unique list from an existing list.
    ''' </summary>
    ''' <param name="checkList">New list to check.</param>
    ''' <param name="baseList">Unique list to check against.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function CreateUniqueList(ByVal checkList As IEnumerable(Of String),
                                             ByVal baseList As IList(Of String),
                                             Optional ByVal caseSensitive As Boolean = False) As List(Of String)
        Return New List(Of String)(MergeUnique(checkList, baseList, caseSensitive))
    End Function


    ''' <summary>
    ''' Given a new list and base list, if the new list has unique entries, they are added to the base list. 
    ''' The base list can start out empty to create a new unique list from an existing list.
    ''' </summary>
    ''' <param name="checkList">New list to check.</param>
    ''' <param name="baseList">Unique list to check against.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Function CreateUniqueObservableCollection(ByVal checkList As IEnumerable(Of String),
                                                             ByVal baseList As IList(Of String),
                                                             Optional ByVal caseSensitive As Boolean = False) As ObservableCollection(Of String)   
        Return New ObservableCollection(Of String)(MergeUnique(checkList, baseList, caseSensitive))
    End Function

    ''' <summary>
    ''' Given a new list and base list, if the new list has unique entries, they are added to the base list. 
    ''' The base list can start out empty to create a new unique list from an existing list.
    ''' </summary>
    ''' <param name="checkList">New list to check.</param>
    ''' <param name="baseList">Unique list to check against.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function MergeUnique(ByVal checkList As IEnumerable(Of String),
                                        ByVal baseList As IList(Of String),
                                        Optional ByVal caseSensitive As Boolean = False) As IEnumerable(Of String)
        Dim newList As New List(Of String)
        If (checkList Is Nothing AndAlso
             baseList Is Nothing) Then Return newList

        If (checkList IsNot Nothing AndAlso
            baseList Is Nothing) Then
            For Each myNewEntry As String In checkList
                newList.Add(myNewEntry)
            Next
            Return newList
        End If 
        
        For Each myNewEntry As String In baseList
            newList.Add(myNewEntry)
        Next

        If checkList IsNot Nothing Then
            Dim entryMatch As Boolean
            For Each myNewEntry As String In checkList
                entryMatch = baseList.Any(Function(myBaseEntry) StringsMatch(myNewEntry, myBaseEntry, caseSensitive))
                If Not entryMatch Then newList.Add(myNewEntry)
            Next
        End If
        Return newList
    End Function


    

    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <param name="caseSensitive">True: The differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal originalList As IEnumerable(Of String),
                                                         Optional ByVal caseSensitive As Boolean = False) As List(Of String)
        Return New List(Of String)(ConvertToUnique(originalList, caseSensitive))
    End Function

    ''' <summary>
    ''' Checks the observable collection provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <param name="caseSensitive">True: The differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertToUniqueObservableCollection(ByVal originalList As IEnumerable(Of String),
                                                               Optional ByVal caseSensitive As Boolean = False) As ObservableCollection(Of String)
        Return New ObservableCollection(Of String)(ConvertToUnique(originalList, caseSensitive))
    End Function

    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <param name="caseSensitive">True: The differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUnique(ByVal originalList As IEnumerable(Of String),
                                                    Optional ByVal caseSensitive As Boolean = False) As IEnumerable(Of String)
        Dim newList As New List(Of String)
        If originalList Is Nothing Then Return newList

        Dim entryMatch As Boolean
        For Each myOriginalEntry As String In originalList
            entryMatch = newList.Any(Function(myNewEntry) StringsMatch(myNewEntry, myOriginalEntry, caseSensitive))
            If Not entryMatch Then newList.Add(myOriginalEntry)
        Next
        Return newList
    End Function




    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <param name="sortList">True: The new unique list will be sorted lowest to highest.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal originalList As IEnumerable(Of Integer),
                                                          Optional ByVal sortList As Boolean = True) As List(Of Integer)
        Dim finalList As New List(Of Integer)(ConvertToUniqueList(originalList))
        If sortList Then finalList.Sort()

        Return finalList
    End Function

    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <param name="sortList">True: The new unique list will be sorted lowest to highest.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertToUniqueObservableCollection(ByVal originalList As IEnumerable(Of Integer),
                                                               Optional ByVal sortList As Boolean = True) As ObservableCollection(Of Integer)
        Dim finalList As New List(Of Integer)(ConvertToUniqueList(originalList))
        If sortList Then finalList.Sort()

        Return New ObservableCollection(Of Integer)(finalList)
    End Function

    ''' <summary>
    ''' Checks the list provided and creates a new list where any duplicate entries are removed.
    ''' </summary>
    ''' <param name="originalList">Original list to check for redundancies.</param>
    ''' <remarks></remarks>
    Public Overloads Shared Function ConvertToUniqueList(ByVal originalList As IEnumerable(Of Integer)) As IEnumerable(Of Integer)
        Dim newList As New List(Of Integer)
        If originalList Is Nothing Then Return newList

        Dim entryMatch As Boolean
        For Each myOriginalEntry As Integer In originalList
            entryMatch = newList.Any(Function(myNewEntry) myNewEntry = myOriginalEntry)
            If Not entryMatch Then newList.Add(myOriginalEntry)
        Next
        Return newList
    End Function



    ''' <summary>
    ''' Checks if a particular string exists in a provided list of strings. 
    ''' If it does not, it is added in the specified first/last entry of the list.
    ''' </summary>
    ''' <param name="baseList">String items to check and, if necessary, modify.</param>
    ''' <param name="checkItem">Item to check against the list of string items.</param>
    ''' <param name="placeFirst">If true, and the list item is added, it will be added as the first item in the new list. 
    ''' False: List item will be added as the last item in the list.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Function AddIfNew(ByVal baseList As IList(Of String),
                                    ByVal checkItem As String,
                                    Optional ByVal placeFirst As Boolean = False,
                                    Optional ByVal caseSensitive As Boolean = False) As IEnumerable(Of String)
        Dim tempList As New List(Of String)
        If String.IsNullOrWhiteSpace(checkItem) Then 
            If baseList Is Nothing Then
                Return tempList
            Else
                Return baseList
            End If
        End If
        If baseList Is Nothing Then
            tempList.Add(checkItem)
            Return tempList
        End If

        'Check if item is unique
        Dim newToList As Boolean = baseList.All(Function(listItem) Not StringsMatch(listItem, checkItem, caseSensitive))
        
        'If unique, add to the desired place in the list
        If newToList Then
            'Add to temp list
            If placeFirst Then
                tempList.Add(checkItem)
                tempList.AddRange(baseList)
            Else
                tempList.AddRange(baseList)
                tempList.Add(checkItem)
            End If
            Return tempList
        Else
            Return baseList
        End If
    End Function
#End Region


#Region "List Conversions"

    ''' <summary>
    ''' Converts list of string to an observable collection of string, or the reverse.
    ''' </summary>
    ''' <param name="listToConvert">List of items to either convert.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertListToObservableCollection(Of T)(ByVal listToConvert As IList(Of T)) As ObservableCollection(Of T)
        If listToConvert Is Nothing Then Return New ObservableCollection(Of T)
        Return New ObservableCollection(Of T)(listToConvert)
    End Function

    ''' <summary>
    ''' Converts any list of one type to another type if the types are inheritable.
    ''' </summary>
    ''' <typeparam name="TFrom">Type to convert from.</typeparam>
    ''' <typeparam name="TTo">Type to convert to.</typeparam>
    ''' <param name="listToConvertFrom">The list containing the item types to convert</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Convert(Of TFrom, TTo)(ByVal listToConvertFrom As IList(Of TFrom)) As IList(Of TTo)
        Dim newList As New List(Of TTo)
        If listToConvertFrom Is Nothing Then Return newList

        If GetType(TTo).IsAssignableFrom(GetType(TFrom)) Then
            newList.AddRange(From item In listToConvertFrom Select DirectCast(DirectCast(item, Object), TTo))
        End If

        Return newList
    End Function
#End Region


#Region "Remove, Sort"
    ''' <summary>
    ''' Removes any blank entries in the list.
    ''' </summary>
    ''' <param name="listToTrim">List to trim.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TrimListOfEmptyItems(ByVal listToTrim As IEnumerable(Of String)) As IEnumerable(Of String)
        If listToTrim Is Nothing Then Return New List(Of String)()
        Return (From item In listToTrim Where Not String.IsNullOrWhiteSpace(item)).ToList()
    End Function

    ''' <summary>
    ''' Removes a list of string items from another list of string items.
    ''' </summary>
    ''' <param name="listToRemove">List of string items to be removed from the base list.</param>
    ''' <param name="originalList">Base list from which to remove the provided items.</param>
    ''' <param name="caseSensitive">True: The differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <remarks></remarks>
    Public Shared Function RemoveFromList(ByVal listToRemove As IList(Of String),
                                          ByVal originalList As IEnumerable(Of String),
                                          Optional ByVal caseSensitive As Boolean = False) As IEnumerable(Of String)
        Dim tempList As New List(Of String)
        If listToRemove Is Nothing AndAlso originalList Is Nothing Then
            Return tempList
        Else If listToRemove Is Nothing Then
            Return originalList
        Else If originalList Is Nothing Then
            Return tempList
        End If
        
        Dim entryMatch As Boolean
        For Each myBaseEntry As String In originalList
            entryMatch = listToRemove.Any(Function(myRemoveEntry) StringsMatch(myRemoveEntry, myBaseEntry, caseSensitive))
            If Not entryMatch Then tempList.Add(myBaseEntry)
        Next

        Return tempList
    End Function

    ''' <summary>
    ''' Sorts a list, and takes any collection of lists correlated with the sorted list and sorts them such that they remain in sync.
    ''' </summary>
    ''' <param name="listToSort">List to be sorted.</param>
    ''' <param name="correlatedLists">Lists to be sorted based on the originally sorted list.</param>
    ''' <param name="sortAscending">True: List will be sorted in ascending order. 
    ''' False: List will be sorted in descending order.</param>
    ''' <remarks></remarks>
    Public Shared Sub SortCorrelatedLists(ByRef listToSort As IList(Of String),
                                          ByRef correlatedLists As IList(Of IList(Of String)),
                                          Optional ByVal sortAscending As Boolean = True)
        ' Check lists, adjust where necessary for processing.
        If listToSort Is Nothing OrElse listToSort.Count = 0 Then Return
        If correlatedLists IsNot Nothing Then
            For Each list As IList(Of String) In correlatedLists
                If list IsNot Nothing AndAlso Not list.Count = 0  ' We can handle empty lists by leaving them alone.
                    If Not list.Count = listToSort.Count Then Throw New IndexOutOfRangeException(
                        "Method cannot correlate sorting of jagged lists! " & vbCrLf & 
                        "Make sure all lists provided are of equal length to each other and the primary list being sorted.")
                End If
            Next
        End If

        ' Sort the sortList as ascending
        Dim tempSortList As New List(Of String)
        Dim indexSortList As New List(Of Integer)
        For i = 0 To listToSort.Count - 1
            ' Get the maximum entry that has not been added to the temp list, and add it. 
            ' Save the corresponding index in the order in which the entry was added.
            Dim currentMax As String = ""
            Dim j As Integer = 0
            Dim correlatedIndex As Integer = 0
            For Each entry As String In listToSort
                If entry > currentMax Then
                    If Not ExistsInList(entry, tempSortList) Then
                        currentMax = entry
                        correlatedIndex = j
                    End If
                End If
                j += 1
            Next
            tempSortList.Add(currentMax)
            indexSortList.Add(correlatedIndex)
        Next

        If sortAscending Then   'Reverse sorting
            tempSortList.Reverse()
            indexSortList.Reverse()
        End If

        'Assign new sorted list
        listToSort = tempSortList

        'Sort the correlated lists
        If correlatedLists Is Nothing Then Return

        Dim tempCorrelatedLists As New List(Of IList(Of String))
        Dim tempCorrelatedList As List(Of String)
        For Each correlatedList As IList(Of String) In correlatedLists
            If correlatedList Is Nothing OrElse correlatedList.Count = 0 Then 
                tempCorrelatedList = New List(Of String)
            Else
                tempCorrelatedList = (From sortIndex In indexSortList Select correlatedList(sortIndex)).ToList()
            End If
            tempCorrelatedLists.Add(tempCorrelatedList)
        Next

        'Assign new correlated lists
        correlatedLists = tempCorrelatedLists
    End Sub
#End Region


#Region "Combine Lists"
    ''' <summary>
    ''' Appends one list to another.
    ''' </summary>
    ''' <param name="originalList">The list to be added to.</param>
    ''' <param name="listToAppend">The list to add to the end.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AppendToList(ByVal originalList As IList(Of String),
                                      ByVal listToAppend As IList(Of String)) As IList(Of String)
        If (originalList Is Nothing AndAlso 
            listToAppend Is Nothing ) Then
            Return New List(Of String)
        ElseIf originalList Is Nothing Then
             Return listToAppend
        ElseIf listToAppend Is Nothing Then
            Return originalList
        End If

        Dim newList As New List(Of String)(originalList)
        newList.AddRange(listToAppend)

        Return newList
    End Function

    ''' <summary>
    ''' Returns a new list that is a combination of the supplied lists, with only unique entries.
    ''' </summary>
    ''' <param name="newList">The list to be added from.</param>
    ''' <param name="originalList">The list to be added to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CombineListsUnique(ByVal newList As IList(Of String),
                                              ByVal originalList As IList(Of String)) As IList(Of String)
        If (originalList Is Nothing AndAlso 
            newList Is Nothing ) Then
            Return New List(Of String)
        ElseIf originalList Is Nothing Then
             Return newList
        ElseIf newList Is Nothing Then
            Return originalList
        End If

        Dim uniqueList As New ObservableCollection(Of String)(AppendToList(newList, originalList))
        uniqueList = ConvertToUniqueObservableCollection(uniqueList)

        Return uniqueList
    End Function
#End Region


End Class
