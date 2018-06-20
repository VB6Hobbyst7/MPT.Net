Option Explicit On
Option Strict On

Imports MPT.Reporting

''' <summary>
''' Contains functions for determining and manipulating strings.
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class StringLibrary
    Shared Event Messenger(message As MessengerEventArgs)

    ''ncrunch: no coverage start
    Private Sub New()
        'Contains only shared members.
        'Private constructor means the class cannot be instantiated.
    End Sub
    ''ncrunch: no coverage end

#Region "String Query"
    ''' <summary>
    ''' Determines if two strings are the same, accounting for capitalization.
    ''' </summary>
    ''' <param name="string1">First string to compare.</param>
    ''' <param name="string2">Second string to compare.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function StringsMatch(ByVal string1 As String,
                                        ByVal string2 As String,
                                        Optional ByVal caseSensitive As Boolean = False) As Boolean
        Return ((String.IsNullOrWhiteSpace(string1) AndAlso
                 String.IsNullOrWhiteSpace(string2)) OrElse
                 String.Compare(string1, string2, ignoreCase:=Not caseSensitive) = 0)
    End Function

    ''' <summary>
    ''' Searches a string and determines if a substring exists.
    ''' </summary>
    ''' <param name="stringSearched">String to be searched.</param>
    ''' <param name="stringToSearchFor">Substring segment to search for.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <returns>True: Substring found within string</returns>
    ''' <remarks></remarks>
    Public Shared Function StringExistInName(ByVal stringSearched As String,
                                             ByVal stringToSearchFor As String,
                                             Optional ByVal caseSensitive As Boolean = False) As Boolean
        If (String.IsNullOrWhiteSpace(stringSearched) AndAlso String.IsNullOrWhiteSpace(stringToSearchFor)) Then Return True
        If (Not String.IsNullOrWhiteSpace(stringSearched) AndAlso String.IsNullOrWhiteSpace(stringToSearchFor)) Then Return False
        If (Len(stringToSearchFor) > Len(stringSearched)) Then Return False

        For i = 1 To Len(stringSearched) - Len(stringToSearchFor) + 1
            If StringFoundAtIndex(stringToSearchFor, i, stringSearched, caseSensitive) Then Return True
        Next
        Return False
    End Function

    ''' <summary>
    ''' Returns True if the string searched for exists within the original text starting at the specified index.
    ''' </summary>
    ''' <param name="stringSearchedFor">The string to search for.</param>
    ''' <param name="index">The 1-based index where the string is to start from.</param>
    ''' <param name="originalText">The original string to search from.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <returns></returns>
    Public Shared Function StringFoundAtIndex(ByVal stringSearchedFor As String,
                                              ByVal index As Integer,
                                              ByVal originalText As String,
                                              Optional Byval caseSensitive As Boolean = False) As Boolean
        If String.IsNullOrEmpty(stringSearchedFor) Then Return False
        Dim stringOfOldSubstringLength As String = Mid(originalText, index, Len(stringSearchedFor))
        Return StringsMatch(stringOfOldSubstringLength, stringSearchedFor, caseSensitive)
    End Function

    ''' <summary>
    ''' Checks for name/string matching based on full name, or partial (if specified).
    ''' Can also be based on capitalization.
    ''' Works on strings of any length, with spaces.
    ''' </summary>
    ''' <param name="nameSource">Source name to be checked against.</param>
    ''' <param name="nameCheck">Name to be checked.</param>
    ''' <param name="partialNameMatch">True: Considered a match as long as the source name contains the checked name, even if the names don match in their entirety.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: Match is made disregarding capitalization.</param>
    ''' <returns></returns>
    Public Shared Function IsNameMatching(Byval nameSource As String,
                                          ByVal nameCheck As String,
                                          ByVal partialNameMatch As Boolean,
                                          Optional ByVal caseSensitive As Boolean = false) As Boolean
        If partialNameMatch Then
            Return StringExistInName(nameSource, nameCheck, caseSensitive)
        Else
            Return StringsMatch(nameSource, nameCheck, caseSensitive)
        End If
    End Function

    ''' <summary>
    ''' Returns true if the text provided contains all white space.
    ''' This does not include null or empty string.
    ''' </summary>
    ''' <param name="text">String to check.</param>
    ''' <returns></returns>
    Public Shared Function HasAllWhiteSpace(ByVal text As String) As Boolean
        If text Is Nothing Then Return False
        Return (text.Length > 0 AndAlso
                text.Trim().Length = 0)
    End Function
#End Region

#Region "Gets-Sets Portion of a String"
    
    ''' <summary>
    ''' Gets the last part of a string after the last occurrence of a designated string. 
    ''' Returns name if the character is not found.
    ''' </summary>
    ''' <param name="name">String to be truncated. 
    ''' Can be a single word or a sentence.</param>
    ''' <param name="character">Character to search for. 
    ''' Function returns what remains of string after the last occurrence of this character.</param>
    ''' <returns></returns>
    ''' <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
    Public Shared Function GetSuffix(ByVal name As String,
                                     ByVal character As String) As String
        If String.IsNullOrWhiteSpace(name) Then Return ""
        If String.IsNullOrEmpty(character) Then Return name

        character = GetCharacter(character)
        Dim characterPosition As Integer = Len(name) - InStrRev(name, character)
        If characterPosition < 1 Then
            Return name
        Else
            Return Right(name, characterPosition)
        End If
    End Function

    ''' <summary>
    ''' Gets the first part of a string before the first occurrence of a designated character.
    ''' Returns name if the character is not found.
    ''' </summary>
    ''' <param name="name">String to be truncated. 
    ''' Can be a single word or a sentence.</param>
    ''' <param name="character">Character to search for. 
    ''' Function returns what remains of string before the first occurence of this character.</param>
    ''' <returns></returns>
    ''' <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
    Public Shared Function GetPrefix(ByVal name As String,
                                     ByVal character As String) As String      
        If String.IsNullOrWhiteSpace(name) Then Return ""
        If String.IsNullOrEmpty(character) Then Return name

        character = GetCharacter(character)
        Dim characterPosition As Integer = InStr(name, character) - 1
        If characterPosition < 1 Then
            Return name
        Else
            Return Left(name, characterPosition)
        End If
    End Function

    ''' <summary>
    ''' Finds the first occurrence of a given substring within a string and returns the prefix and/or suffix of the remaining string. 
    ''' If the substring is not found, the original string is returned.
    ''' </summary>
    ''' <param name="originalText">String to be filtered.</param>
    ''' <param name="filterText">String to filter out.</param>
    ''' <param name="retainPrefix">True: Retain the portion of the string before the filter string.</param>
    ''' <param name="retainSuffix">True: Retain the portion of the string after the filter string.</param>
    ''' <param name="filterEndOfName">True: A match is only valid if the filtered string comprises the end of the string. 
    ''' One use is to filter the last directory in a path while avoiding false positives higher up the path hierarchy.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <returns></returns>
    ''' <remarks>TODO: Refactor into  different functions, simpler execution. Add ability to specify number of occurrences of filter.</remarks>
    Public Shared Function FilterFromText(ByVal originalText As String,
                                            ByVal filterText As String,
                                            ByVal retainPrefix As Boolean,
                                            ByVal retainSuffix As Boolean,
                                            Optional ByVal filterEndOfName As Boolean = False,
                                            Optional ByVal caseSensitive As Boolean = False,
                                            Optional ByVal suppressWarnings As Boolean = True) As String
        If String.IsNullOrEmpty(filterText) Then Return originalText

        Dim nonMatchingLength As Integer = Len(originalText) - Len(filterText)
        If (nonMatchingLength < 0) Then Return originalText
        Dim maxCharacterIndex As Integer = nonMatchingLength + 1
        
        For characterIndex = 1 To maxCharacterIndex
            If Not StringFoundAtIndex(filterText, characterIndex, originalText, caseSensitive) Then Continue For

            'If filtering out the end of a directory name, the following check avoids finding a match farther up the path hierarchy.
            If (filterEndOfName AndAlso (Not characterIndex = maxCharacterIndex)) Then Continue For

            Dim prefix As String = ""
            If retainPrefix Then prefix = LeftOfIndex(originalText, characterIndex)

            Dim suffix As String = ""
            If retainSuffix Then suffix = RightOfIndex(originalText, characterIndex + (Len(filterText) - 1))

            Return prefix & suffix
        Next

        If Not suppressWarnings Then RaiseMessengerStringNotFound(filterText, originalText)
        Return originalText
    End Function

    ''' <summary>
    ''' Replaces a substring in a string. Returns the new string.
    ''' </summary>
    ''' <param name="originalText">String to be searched</param>
    ''' <param name="oldSubString">Substring segment to search for and be replaced</param>
    ''' <param name="newSubString">Substring segment to replace</param>
    ''' <param name="suppressWarnings">True: No warning is given if the old substring is not found in the old string.</param>
    ''' <param name="canReplaceAll">True: If the old substring equals the old string, the entire string is replaced.</param>
    ''' <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
    ''' False: A match is made disregarding capitalization.</param>
    ''' <returns>Returns the new string.</returns>
    ''' <remarks></remarks>
    Public Shared Function ReplaceInText(ByVal originalText As String,
                                        ByVal oldSubString As String,
                                        ByVal newSubString As String,
                                        Optional ByVal suppressWarnings As Boolean = False,
                                        Optional ByVal canReplaceAll As Boolean = False,
                                        Optional ByVal caseSensitive As Boolean = False) As String
        If String.IsNullOrWhiteSpace(originalText) Then Return ""
        If String.IsNullOrEmpty(oldSubString) Then Return originalText

        If StringsMatch(oldSubString, originalText, caseSensitive) Then
            If canReplaceAll Then 
                Return newSubString
            Else
                Return originalText
            End If
        End If
        
        Dim nonMatchingLength As Integer = Len(originalText) - Len(oldSubString)
        If (nonMatchingLength < 0) Then Return originalText
        Dim maxCharacterIndex As Integer = nonMatchingLength + 1

        For characterIndex = 1 To maxCharacterIndex
            If StringFoundAtIndex(oldSubString, characterIndex, originalText, caseSensitive) Then
                Return LeftOfIndex(originalText, characterIndex) & newSubString & RightOfIndex(originalText, characterIndex + (Len(oldSubString)-1))
            End If
        Next

        If Not suppressWarnings Then RaiseMessengerStringNotFound(newSubString, originalText)
        Return originalText
    End Function
    
    ''' <summary>
    ''' Returns the string left of the specified letter index (1-based).
    ''' </summary>
    ''' <param name="text">Text to use.</param>
    ''' <param name="characterIndex">Index of character to return to the left of.</param>
    ''' <returns></returns>
    Public Shared Function LeftOfIndex(ByVal text As String,
                                        ByVal characterIndex As Integer) As String
        If (String.IsNullOrEmpty(text)) Then Return ""
        If (characterIndex > text.Length) Then Return text

        Dim length As Integer = characterIndex - 1
        If (length < 0) Then Return text
        Return Left(text, length)
    End Function

    ''' <summary>
    ''' Returns the string right of the specified letter index (1-based).
    ''' </summary>
    ''' <param name="text">Text to use.</param>
    ''' <param name="characterIndex">Index of character to return to the right of.</param>
    ''' <returns></returns>
    Public Shared Function RightOfIndex(ByVal text As String,
                                         ByVal characterIndex As Integer) As String
        If (String.IsNullOrEmpty(text) OrElse characterIndex > text.Length) Then Return ""
        
        Dim length As Integer = Len(text) - characterIndex '+ 1
        If (length < 0) Then Return text
        Return Right(text, length)
    End Function

    ''' <summary>
    ''' Returns only the portions of the string that are numeric. 
    ''' All non-numeric characters are filtered out.
    ''' </summary>
    ''' <param name="stringToFilter">String to filter.</param>
    ''' <param name="keepSpaces">True: Spaces between numeric characters are preserved.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FilterToNumeric(ByVal stringToFilter As String,
                                           Optional ByVal keepSpaces As Boolean = False) As String
        If String.IsNullOrWhiteSpace(stringToFilter) Then Return ""

        Dim filteredString As String = ""
        For i = 0 To stringToFilter.Length - 1
            Dim currentCharacter As String = stringToFilter(i)
            If (IsNumeric(currentCharacter)) Then
                Dim appendedString As String = currentCharacter
                appendedString = PrependSign(appendedString, stringToFilter, i)
                appendedString = AppendDecimal(appendedString, stringToFilter, i)

                filteredString &= appendedString
            ElseIf (currentCharacter = " " AndAlso keepSpaces) Then
                If (filteredString.Length > 0 AndAlso
                    Not filteredString(filteredString.Length - 1) = " ") Then filteredString &= currentCharacter
            End If
        Next

        Return filteredString.Trim()
    End Function
    
    ''' <summary>
    ''' Pluralizes a word, or not, based on a number, and returns the combination of the number and word.
    ''' </summary>
    ''' <param name="number">Number to base pluralization on.</param>
    ''' <param name="word">Word to append to the number, and pluralize if necessary.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PluralizeString(ByVal number As Double,
                                           ByVal word As String) As String
        If String.IsNullOrWhiteSpace(word) Then Return number.ToString()
        If (number = 1 OrElse number = -1) Then
            PluralizeString = number & " " & word
        Else
            PluralizeString = number & " " & word & "s"
        End If
    End Function


    ''' <summary>
    ''' Replaces the first instance of the string being searched for.
    ''' </summary>
    ''' <param name="text">Text to search within.</param>
    ''' <param name="textSearch">String to search for.</param>
    ''' <param name="textReplace">String to replace the searched string with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ReplaceFirst(ByVal text As String,
                                         ByVal textSearch As String,
                                         ByVal textReplace As String) As String
        If(String.IsNullOrWhiteSpace(text)) Then Return ""
        If (String.IsNullOrEmpty(textSearch)) Then Return text

        Dim position As Integer = text.IndexOf(textSearch)
        If (position < 0) Then
            Return text
        Else
            Return text.Substring(0, position) & textReplace & text.Substring(position + textSearch.Length)
        End If
    End Function

#End Region

#Region "Lists"    
    ''' <summary>
    ''' Takes a list and concatenates it into a single string message with specified joiners.
    ''' </summary>
    ''' <param name="strings">List of items to concatenate.</param>
    ''' <param name="joiner">Joining word to use if there is more than one entry, such as 'and' or 'or'.</param>
    ''' <param name="alwaysUseJoiner">True: Joiner is used in a list of two. Else, the joiner is not used in a list of two.</param>
    ''' <param name="prefix">This is appended to the beginning of each list item. Example "Mr." or "*.".</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ConcatenateListToMessage(ByVal strings As List(Of String),
                                                    ByVal joiner As String,
                                                    Optional ByVal alwaysUseJoiner As Boolean = False,
                                                    Optional ByVal prefix As String = "") As String
        Dim concatenatedList As String = ""
        Dim joinerAndSpaces = ""
        If String.IsNullOrWhiteSpace(joiner) Then joinerAndSpaces = ", "

        For i = 0 To strings.Count - 1
            If i = 0 Then
                concatenatedList = prefix & strings(i)
            Else
                If (strings.Count = 2 AndAlso Not alwaysUseJoiner) OrElse
                        alwaysUseJoiner Then
                    If Not String.IsNullOrWhiteSpace(joiner) Then joinerAndSpaces = " " & joiner & " "
                Else
                    If Not String.IsNullOrWhiteSpace(joiner) Then joinerAndSpaces = ", " & joiner & " "
                End If
                concatenatedList = concatenatedList & joinerAndSpaces & prefix & strings(i)
            End If
        Next

        Return concatenatedList
    End Function

    ''' <summary>
    ''' Takes a message of a list and splits it into a list of items.
    ''' </summary>
    ''' <param name="message">Message of a list of items to split apart.</param>
    ''' <param name="joiner">Joining word to use if there is more than one entry, such as 'and' or 'or'.</param>
    ''' <param name="alwaysUseJoiner">True: Joiner is used in a list of two. Else, the joiner is not used in a list of two.</param>
    ''' <param name="prefix">This is appended to the beginning of each list item. Example "Mr." or "*.".</param>
    ''' <returns></returns>
    Public Shared Function SplitMessageToList(ByVal message As string,
                                              ByVal joiner As String,
                                              Optional ByVal alwaysUseJoiner As Boolean = False,
                                              Optional ByVal prefix As String = "") As List(Of String)
        message = Trim(Right(message, Len(message) - Len(prefix)))

        Dim strings() = message.Split(New String() {joiner}, StringSplitOptions.None)

        If IsTwoItemListWithoutJoiner(message, joiner, alwaysUseJoiner)
            strings = message.Split(New String() {" "}, StringSplitOptions.None)
        End If

        Dim stringsList As List(Of String) = strings.ToList()

        Dim stringsListNoSpaces As New List(Of STring)
        For Each item as String in stringsList
            stringsListNoSpaces.Add(item.Trim())
        Next

        Dim stringsListNoPrefixes As New List(Of String)
        If prefix.Length > 0 Then
            For Each item as String in stringsListNoSpaces
                stringsListNoPrefixes.Add(item.Replace(prefix, ""))
            Next
        Else
            stringsListNoPrefixes = stringsListNoSpaces
        End If
        

        stringsList.Clear()
        For Each item as String in stringsListNoPrefixes
            stringsList.Add(item.TrimEnd(Convert.ToChar(",")))
        Next

        Return stringsList
    End Function

    ''' <summary>
    ''' Removes all matching entries from the core list that exist in the filter list.
    ''' Returns what remains.
    ''' </summary>
    ''' <param name="listCore">Core list to filter items out of.</param>
    ''' <param name="listFilter">List of items to remve from the core list, if present.</param>
    ''' <returns></returns>
    Public Shared Function FilterListFromList(ByVal listCore As List(Of String),
                                              ByVal listFilter As List(Of String)) As List(Of String)
        Dim filteredList As New List(Of String)
        If listCore Is Nothing Then Return filteredList
        If listFilter Is Nothing Then Return listCore

        filteredList.AddRange(From itemCore In listCore Let includeItem = listFilter.All(Function(itemFilter) Not StringsMatch(itemCore, itemFilter)) Where includeItem Select itemCore)

        Return filteredList
    End Function
#End Region

#Region "Adjusting-Cleaning Strings"
    ''' <summary>
    ''' Trims specified character from either or both ends of a string.
    ''' </summary>
    ''' <param name="stringTrim">String to trim.</param>
    ''' <param name="character">Character to trim from the string ends.</param>
    ''' <param name="trimLeft">False: Left side of the string will not have any existing characters trimmed.</param>
    ''' <param name="trimRight">False: Right side of the string will not have any existing characters trimmed.</param>
    ''' <returns></returns>
    Public Shared Function TrimCharacterFromEnds(ByVal stringTrim As String,
                                                 ByVal character As Char,
                                                 Optional ByVal trimLeft As Boolean = True,
                                                 Optional ByVal trimRight As Boolean = True) As String
        If trimLeft Then
            If Left(stringTrim, 1) = character Then stringTrim = Right(stringTrim, Len(stringTrim) - 1)
        End If
        If trimRight Then
            If Right(stringTrim, 1) = character Then stringTrim = Left(stringTrim, Len(stringTrim) - 1)
        End If
        Return stringTrim
    End Function

    ''' <summary>
    ''' Adds specified character to either or both ends of a string.
    ''' </summary>
    ''' <param name="stringAdd">String to add to.</param>
    ''' <param name="character">Character to add to the string ends.</param>
    ''' <param name="addLeft">False: Left side of the string will not have any existing characters added.</param>
    ''' <param name="addRight">False: Right side of the string will not have any existing characters added.</param>
    ''' <returns></returns>
    Public Shared Function AddCharacterToEnds(ByVal stringAdd As String,
                                              ByVal character As Char,
                                              Optional ByVal addLeft As Boolean = True,
                                              Optional ByVal addRight As Boolean = True) As String
        If addLeft Then
            stringAdd = character & stringAdd
        End If
        If addRight Then
            stringAdd &= character
        End If
        Return stringAdd
    End Function
#End Region

#Region "Helper Methods"
    ''' <summary>
    ''' Raises a messenger event with a warning that the string searched for was not found in the original text.
    ''' </summary>
    ''' <param name="stringSearchedFor"></param>
    ''' <param name="originalText"></param>
    Private Shared Sub RaiseMessengerStringNotFound(ByVal stringSearchedFor As String,
                                                    ByVal originalText As String)
        RaiseEvent Messenger(New MessengerEventArgs("String '{0}' not found within text '{1}'. {2}Name will remain unchanged.",
                                                        stringSearchedFor, originalText, Environment.NewLine))
    End Sub

    
    ''' <summary>
    ''' Returns a single string character. 
    ''' If the string has more than one character, only the first one is returned.
    ''' </summary>
    ''' <param name="character">Character to potentially trim.</param>
    ''' <returns></returns>
    Private Shared Function GetCharacter(ByVal character As String) As String
        If character.Length > 1 Then 
            Return Left(character, 1)
        Else
            Return character
        End If
    End Function

    ''' <summary>
    ''' Adds a sign to the beginning of the target string if one exists at the prior character in the string checked.
    ''' </summary>
    ''' <param name="targetString">String to add the character to, if applicable.</param>
    ''' <param name="stringToCheck">String to check for the presence of the character.</param>
    ''' <param name="currentIndex">Index associated with the target string in the string to check.</param>
    ''' <returns></returns>
    Private Shared Function PrependSign(ByVal targetString As String,
                                        ByVal stringToCheck As String,
                                        ByVal currentIndex As Integer) As String
        Dim remainingCharacters As Integer = stringToCheck.Length - 1 - currentIndex
        If (remainingCharacters > 0 AndAlso 0 < currentIndex) Then
            Dim priorCharacter As String = stringToCheck(currentIndex-1)
            If (StringsMatch(priorCharacter, "-") OrElse
                StringsMatch(priorCharacter, "+")) Then Return (priorCharacter & targetString)
        End If
        Return targetString
    End Function

    ''' <summary>
    ''' Adds a decimal to the end of the target string if one exists at the next character in the string checked.
    ''' </summary>
    ''' <param name="targetString">String to add the character to, if applicable.</param>
    ''' <param name="stringToCheck">String to check for the presence of the character.</param>
    ''' <param name="currentIndex">Index associated with the target string in the string to check.</param>
    ''' <returns></returns>
    Private Shared Function AppendDecimal(ByVal targetString As String,
                                           ByVal stringToCheck As String,
                                           ByVal currentIndex As Integer) As String
        Dim remainingCharacters As Integer = stringToCheck.Length - 1 - currentIndex
        If remainingCharacters > 1 Then
            Dim nextCharacter As String = stringToCheck(currentIndex+1)
            Dim twoCharactersAhead As String = stringToCheck(currentIndex+2)

            If (StringsMatch(nextCharacter, ".") AndAlso 
                IsNumeric(twoCharactersAhead)) Then Return (targetString & stringToCheck(currentIndex+1))
        End If
        Return targetString
    End Function
  
    Private Shared Function IsTwoItemListWithoutJoiner(ByVal message As string,
                                                       ByVal joiner As String,
                                                       ByVal alwaysUseJoiner As Boolean) As Boolean
        Dim strings() = message.Split(New String() {joiner}, StringSplitOptions.None)
        Return ((Not alwaysUseJoiner AndAlso
                strings.Count = 1) OrElse
                String.IsNullOrEmpty(joiner))
    End Function
#End Region
End Class
