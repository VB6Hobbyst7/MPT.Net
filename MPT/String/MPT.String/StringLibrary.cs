using System;
using System.Collections.Generic;
using System.Linq;

namespace MPT.String
{
    /// <summary>
    /// Contains functions for determining and manipulating strings.
    /// </summary>
    public static class StringLibrary
    {
        #region String Query
        // May add more international characters later. See: https://stackoverflow.com/questions/17764680/check-if-a-character-is-a-vowel-or-consonant
        private const string vowels = "aeiouAEIOU";  

        /// <summary>
        /// Determines whether the specified character is a vowel.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns><c>true</c> if the specified character is a vowel; otherwise, <c>false</c>.</returns>
        public static bool IsVowel(char character)
        {
            return vowels.Contains(character);
        }

        /// <summary>
        /// Determines if two strings are the same, accounting for capitalization.
        /// </summary>
        /// <param name="string1">First string to compare.</param>
        /// <param name="string2">Second string to compare.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. False: Match is made disregarding capitalization.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool StringsMatch(string string1, 
            string string2, 
            bool caseSensitive = false)
        {
            return ((string.IsNullOrWhiteSpace(string1) && string.IsNullOrWhiteSpace(string2)) || 
                     string.Compare(string1, string2, ignoreCase: !caseSensitive) == 0);
        }

        /// <summary>
        /// Searches a string and determines if a substring exists.
        /// </summary>
        /// <param name="stringSearched">String to be searched.</param>
        /// <param name="stringToSearchFor">Substring segment to search for.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
        /// False: Match is made disregarding capitalization.</param>
        /// <returns>True: Substring found within string</returns>
        /// <remarks></remarks>
        public static bool StringExistInName(string stringSearched, 
            string stringToSearchFor, 
            bool caseSensitive = false)
        {
            if ((string.IsNullOrWhiteSpace(stringSearched) && string.IsNullOrWhiteSpace(stringToSearchFor)))
                return true;
            if ((!string.IsNullOrWhiteSpace(stringSearched) && string.IsNullOrEmpty(stringToSearchFor)) ||
                string.IsNullOrWhiteSpace(stringSearched))
                return false;
            if (stringToSearchFor.Length > stringSearched.Length)
                return false;

            for (int i = 1; i <= stringSearched.Length - stringToSearchFor.Length + 1; i++)
            {
                if (StringFoundAtIndex(stringToSearchFor, i, stringSearched, caseSensitive))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Returns True if the string searched for exists within the original text starting at the specified index.
        /// </summary>
        /// <param name="stringSearchedFor">The string to search for.</param>
        /// <param name="index">The 1-based index where the string is to start from.</param>
        /// <param name="originalText">The original string to search from.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
        /// False: A match is made disregarding capitalization.</param>
        /// <returns></returns>
        public static bool StringFoundAtIndex(string stringSearchedFor, 
            int index, 
            string originalText, 
            bool caseSensitive = false)
        {
            if (stringSearchedFor == " " && originalText == " " && index == 1) return true;
            if (string.IsNullOrWhiteSpace(originalText) ||
                string.IsNullOrEmpty(stringSearchedFor) ||
                index - 1 > originalText.Length)
                return false;
            string stringOfOldSubstringLength = originalText.Substring(index - 1, stringSearchedFor.Length);
            return StringsMatch(stringOfOldSubstringLength, stringSearchedFor, caseSensitive);
        }

        /// <summary>
        /// Checks for name/string matching based on full name, or partial (if specified).
        /// Can also be based on capitalization.
        /// Works on strings of any length, with spaces.
        /// </summary>
        /// <param name="nameSource">Source name to be checked against.</param>
        /// <param name="nameCheck">Name to be checked.</param>
        /// <param name="partialNameMatch">True: Considered a match as long as the source name contains the checked name, even if the names don match in their entirety.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
        /// False: Match is made disregarding capitalization.</param>
        /// <returns></returns>
        public static bool IsNameMatching(string nameSource, 
            string nameCheck, 
            bool partialNameMatch, 
            bool caseSensitive = false)
        {
            return partialNameMatch ? StringExistInName(nameSource, nameCheck, caseSensitive) : StringsMatch(nameSource, nameCheck, caseSensitive);
        }

        /// <summary>
        /// Returns true if the text provided contains all white space.
        /// This does not include null or empty string.
        /// </summary>
        /// <param name="text">String to check.</param>
        /// <returns></returns>
        public static bool HasAllWhiteSpace(string text)
        {
            if (text == null)
                return false;
            return (text.Length > 0 && text.Trim().Length == 0);
        }

        #endregion

        #region Gets-Sets Portion of a String
        /// <summary>
        /// Gets the last part of a string after the last occurrence of a designated string. 
        /// Returns name if the character is not found.
        /// </summary>
        /// <param name="name">String to be truncated. 
        /// Can be a single word or a sentence.</param>
        /// <param name="character">Character to search for. 
        /// Function returns what remains of string after the last occurrence of this character.</param>
        /// <returns></returns>
        /// <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
        public static string GetSuffix(string name, 
            string character)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            if (string.IsNullOrEmpty(character))
                return name;
            
            character = GetCharacter(character);
            int characterPosition = name.Length - name.LastIndexOf(character, StringComparison.Ordinal) - 1;
            return characterPosition < 1 ? name : name.Substring(name.Length - characterPosition);
        }


        /// <summary>
        /// Gets the first part of a string before the first occurrence of a designated character.
        /// Returns name if the character is not found.
        /// </summary>
        /// <param name="name">String to be truncated. 
        /// Can be a single word or a sentence.</param>
        /// <param name="character">Character to search for. 
        /// Function returns what remains of string before the first occurence of this character.</param>
        /// <returns></returns>
        /// <remarks>TODO: Add ability to specify number of occurrences of character</remarks>
        public static string GetPrefix(string name, 
            string character)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;
            if (string.IsNullOrEmpty(character))
                return name;

            character = GetCharacter(character);
            int characterPosition = name.IndexOf(character, StringComparison.Ordinal);
            return characterPosition < 1 ? name : name.Substring(0, characterPosition);
        }

        /// <summary>
        /// Finds the first occurrence of a given substring within a string and returns the prefix and/or suffix of the remaining string. 
        /// If the substring is not found, the original string is returned.
        /// </summary>
        /// <param name="originalText">String to be filtered.</param>
        /// <param name="filterText">String to filter out.</param>
        /// <param name="retainPrefix">True: Retain the portion of the string before the filter string.</param>
        /// <param name="retainSuffix">True: Retain the portion of the string after the filter string.</param>
        /// <param name="filterEndOfName">True: A match is only valid if the filtered string comprises the end of the string. 
        /// One use is to filter the last directory in a path while avoiding false positives higher up the path hierarchy.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
        /// False: A match is made disregarding capitalization.</param>
        /// <returns></returns>
        /// <remarks>TODO: Refactor into  different functions, simpler execution. Add ability to specify number of occurrences of filter.</remarks>
        public static string FilterFromText(string originalText, 
            string filterText, 
            bool retainPrefix, 
            bool retainSuffix, 
            bool filterEndOfName = false, 
            bool caseSensitive = false, 
            bool suppressWarnings = true)
        {
            if (string.IsNullOrEmpty(filterText))
                return originalText;

            int nonMatchingLength = originalText.Length - filterText.Length;
            if ((nonMatchingLength < 0))
                return originalText;
            int maxCharacterIndex = nonMatchingLength + 1;

            for (int characterIndex = 1; characterIndex <= maxCharacterIndex; characterIndex++)
            {
                if (!StringFoundAtIndex(filterText, characterIndex, originalText, caseSensitive))
                    continue;

                //If filtering out the end of a directory name, the following check avoids finding a match farther up the path hierarchy.
                if ((filterEndOfName && (characterIndex != maxCharacterIndex)))
                    continue;

                string prefix = "";
                if (retainPrefix)
                    prefix = LeftOfIndex(originalText, characterIndex);

                string suffix = "";
                if (retainSuffix)
                    suffix = RightOfIndex(originalText, characterIndex + (filterText.Length - 1));

                return prefix + suffix;
            }

            if (!suppressWarnings)
                RaiseMessengerStringNotFound(filterText, originalText);
            return originalText;
        }

        /// <summary>
        /// Replaces a substring in a string. Returns the new string.
        /// </summary>
        /// <param name="originalText">String to be searched</param>
        /// <param name="oldSubString">Substring segment to search for and be replaced</param>
        /// <param name="newSubString">Substring segment to replace</param>
        /// <param name="suppressWarnings">True: No warning is given if the old substring is not found in the old string.</param>
        /// <param name="canReplaceAll">True: If the old substring equals the old string, the entire string is replaced.</param>
        /// <param name="caseSensitive">True: Differences in capitalization will void a potential match. 
        /// False: A match is made disregarding capitalization.</param>
        /// <returns>Returns the new string.</returns>
        /// <remarks></remarks>
        public static string ReplaceInText(string originalText, 
            string oldSubString, 
            string newSubString, 
            bool suppressWarnings = false,
            bool canReplaceAll = false, 
            bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(originalText))
                return string.Empty;
            if (string.IsNullOrWhiteSpace(oldSubString))
                return originalText;

            if (StringsMatch(oldSubString, originalText, caseSensitive))
            {
                return canReplaceAll ? newSubString : originalText;
            }

            int nonMatchingLength = originalText.Length - oldSubString.Length;
            if ((nonMatchingLength < 0))
                return originalText;
            int maxCharacterIndex = nonMatchingLength + 1;

            for (int characterIndex = 1; characterIndex <= maxCharacterIndex; characterIndex++)
            {
                if (StringFoundAtIndex(oldSubString, characterIndex, originalText, caseSensitive))
                {
                    return LeftOfIndex(originalText, characterIndex) + newSubString + RightOfIndex(originalText, characterIndex + (oldSubString.Length - 1));
                }
            }

            if (!suppressWarnings)
                RaiseMessengerStringNotFound(newSubString, originalText);
            return originalText;
        }


        /// <summary>
        /// Returns the string left of the specified letter index (1-based).
        /// </summary>
        /// <param name="text">Text to use.</param>
        /// <param name="characterIndex">Index of character to return to the left of.</param>
        /// <returns></returns>
        public static string LeftOfIndex(string text, 
            int characterIndex)
        {
            if ((string.IsNullOrWhiteSpace(text)))
                return string.Empty;
            if ((characterIndex > text.Length))
                return text;

            int length = characterIndex - 1;
            return (length < 0) ? text : text.Substring(0, length);
        }

        /// <summary>
        /// Returns the string right of the specified letter index (1-based).
        /// </summary>
        /// <param name="text">Text to use.</param>
        /// <param name="characterIndex">Index of character to return to the right of.</param>
        /// <returns></returns>
        public static string RightOfIndex(string text, 
            int characterIndex)
        {
            if ((string.IsNullOrWhiteSpace(text) || characterIndex > text.Length))
                return string.Empty;
            if (characterIndex < 1) return text;

            int length = text.Length - characterIndex;
            return (length < 0) ? text : text.Substring(text.Length - length);
        }



        /// <summary>
        /// Returns only the portions of the string that are numeric. 
        /// All non-numeric characters are filtered out.
        /// </summary>
        /// <param name="stringToFilter">String to filter.</param>
        /// <param name="keepSpaces">True: Spaces between numeric characters are preserved.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FilterToNumeric(string stringToFilter, 
            bool keepSpaces = false)
        {

            if (string.IsNullOrWhiteSpace(stringToFilter))
                return string.Empty;

            string filteredString = "";
            for (int i = 0; i <= stringToFilter.Length - 1; i++)
            {
                char currentCharacter = stringToFilter[i];
                if ((Char.IsNumber(currentCharacter)))
                {
                    string appendedString = currentCharacter.ToString();
                    appendedString = PrependSign(appendedString, stringToFilter, i);
                    appendedString = AppendDecimal(appendedString, stringToFilter, i);

                    filteredString += appendedString;
                }
                else if ((currentCharacter == ' ' && keepSpaces))
                {
                    if ((filteredString.Length > 0 && filteredString[filteredString.Length - 1] != ' '))
                        filteredString += currentCharacter;
                }
            }

            return filteredString.Trim();
        }


        /// <summary>
        /// Replaces the first instance of the string being searched for.
        /// </summary>
        /// <param name="text">Text to search within.</param>
        /// <param name="textSearch">String to search for.</param>
        /// <param name="textReplace">String to replace the searched string with.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ReplaceFirst(string text, 
            string textSearch, 
            string textReplace)
        {
            if ((string.IsNullOrWhiteSpace(text)))
                return string.Empty;
            if ((string.IsNullOrEmpty(textSearch)))
                return text;

            int position = text.IndexOf(textSearch, StringComparison.Ordinal);
            if (position < 0)
            {
                return text;
            }
            return text.Substring(0, position) + textReplace + text.Substring(position + textSearch.Length);
        }
        #endregion

        #region Lists

        /// <summary>
        /// Takes a list and concatenates it into a single string message with specified joiners.
        /// </summary>
        /// <param name="strings">List of items to concatenate.</param>
        /// <param name="joiner">Joining word to use if there is more than one entry, such as 'and' or 'or'.</param>
        /// <param name="alwaysUseJoiner">True: Joiner is used in a list of two. Else, the joiner is not used in a list of two.</param>
        /// <param name="prefix">This is appended to the beginning of each list item. Example "Mr." or "*.".</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string ConcatenateListToMessage(List<string> strings, 
            string joiner, 
            bool alwaysUseJoiner = false, 
            string prefix = "")
        {

            string concatenatedList = "";
            dynamic joinerAndSpaces = "";
            if (string.IsNullOrWhiteSpace(joiner))
                joinerAndSpaces = ", ";

            for (int i = 0; i <= strings.Count - 1; i++)
            {
                if (i == 0)
                {
                    concatenatedList = prefix + strings[i];
                }
                else
                {
                    if ((strings.Count == 2 && !alwaysUseJoiner) || alwaysUseJoiner)
                    {
                        if (!string.IsNullOrWhiteSpace(joiner))
                            joinerAndSpaces = " " + joiner + " ";
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(joiner))
                            joinerAndSpaces = ", " + joiner + " ";
                    }
                    concatenatedList = concatenatedList + joinerAndSpaces + prefix + strings[i];
                }
            }

            return concatenatedList;
        }


        /// <summary>
        /// Takes a message of a list and splits it into a list of items.
        /// </summary>
        /// <param name="message">Message of a list of items to split apart.</param>
        /// <param name="joiner">Joining word to use if there is more than one entry, such as 'and' or 'or'.</param>
        /// <param name="alwaysUseJoiner">True: Joiner is used in a list of two. Else, the joiner is not used in a list of two.</param>
        /// <param name="prefix">This is appended to the beginning of each list item. Example "Mr." or "*.".</param>
        /// <returns></returns>
        public static List<string> SplitMessageToList(string message, 
            string joiner, 
            bool alwaysUseJoiner = false, 
            string prefix = "")
        {
            message = message.Substring(message.Length - (message.Length - prefix.Length)).Trim();


            string [] strings = message.Split(new [] { joiner }, StringSplitOptions.None);

	        if (IsTwoItemListWithoutJoiner(message, joiner, alwaysUseJoiner)) {
		        strings = message.Split(new [] { " " }, StringSplitOptions.None);
	        }
            
            List<string> stringsListNoSpaces = strings.ToList().Select(item => item.Trim()).ToList();

            List<string> stringsListNoPrefixes = new List<string>();
	        if (prefix.Length > 0) {
	            stringsListNoPrefixes.AddRange(stringsListNoSpaces.Select(item => item.Replace(prefix, "")));
	        } else {
		        stringsListNoPrefixes = stringsListNoSpaces;
	        }

            List<string> stringsList = new List<string>();
            stringsList.AddRange(stringsListNoPrefixes.Select(item => item.TrimEnd(Convert.ToChar(","))));

            return stringsList;
        }


        /// <summary>
        /// Removes all matching entries from the core list that exist in the filter list.
        /// Returns what remains.
        /// </summary>
        /// <param name="listCore">Core list to filter items out of.</param>
        /// <param name="listFilter">List of items to remve from the core list, if present.</param>
        /// <returns></returns>
        public static List<string> FilterListFromList(List<string> listCore, 
            List<string> listFilter)
        {
            List<string> filteredList = new List<string>();
            if (listCore == null)
                return filteredList;
            if (listFilter == null)
                return listCore;

            filteredList.AddRange(from itemCore in listCore let includeItem = listFilter.All(itemFilter => !StringsMatch(itemCore, itemFilter)) where includeItem select itemCore);

            return filteredList;
        }
        #endregion

        #region Adjusting-Cleaning Strings

        /// <summary>
        /// Trims specified character from either or both ends of a string.
        /// </summary>
        /// <param name="stringTrim">String to trim.</param>
        /// <param name="character">Character to trim from the string ends.</param>
        /// <param name="trimLeft">False: Left side of the string will not have any existing characters trimmed.</param>
        /// <param name="trimRight">False: Right side of the string will not have any existing characters trimmed.</param>
        /// <returns></returns>
        public static string TrimCharacterFromEnds(string stringTrim, 
            char character, 
            bool trimLeft = true, 
            bool trimRight = true)
        {
            if (string.IsNullOrEmpty(stringTrim)) return string.Empty;
            if (trimLeft &&
                stringTrim.Substring(0, 1) == character.ToString())
            { 
                stringTrim = stringTrim.Substring(stringTrim.Length - (stringTrim.Length - 1));
            }
            if (!trimRight) return stringTrim;
            if (stringTrim.Substring(stringTrim.Length - 1) == character.ToString())
                stringTrim = stringTrim.Substring(0, stringTrim.Length - 1);
            return stringTrim;
        }

        /// <summary>
        /// Adds specified character to either or both ends of a string.
        /// </summary>
        /// <param name="stringAdd">String to add to.</param>
        /// <param name="character">Character to add to the string ends.</param>
        /// <param name="addLeft">False: Left side of the string will not have any existing characters added.</param>
        /// <param name="addRight">False: Right side of the string will not have any existing characters added.</param>
        /// <returns></returns>
        public static string AddCharacterToEnds(string stringAdd, 
            char character, 
            bool addLeft = true, 
            bool addRight = true)
        {
            if (addLeft)
            {
                stringAdd = character + stringAdd;
            }
            if (addRight)
            {
                stringAdd += character;
            }
            return stringAdd;
        }


        /// <summary>
        /// Removes the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="wordToRemove">The word to remove.</param>
        /// <param name="isCaseSensitive">True: Case sensitivity is considered.</param>
        /// <returns>string.</returns>
        public static string RemoveWord(string word,
            string wordToRemove,
            bool isCaseSensitive = false)
        {
            if (word == null) return string.Empty;
            if (isCaseSensitive)
            {
                return string.CompareOrdinal(word, wordToRemove) == 0 ? string.Empty : word;
            }
            return string.CompareOrdinal(word.ToLower(), wordToRemove.ToLower()) == 0 ? string.Empty : word;
        }

        /// <summary>
        /// Normalizes irregular terms such that if it is a common variation, it is set to the specified base word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="wordsToReplace">The words to replace.</param>
        /// <param name="wordToUse">The constant word to use for all replacements.</param>
        /// <param name="isCaseSensitive">True: Case sensitivity is considered.</param>
        /// <returns>string.</returns>
        public static string ReplaceWithConstant(string word,
            List<string> wordsToReplace,
            string wordToUse,
            bool isCaseSensitive = false)
        {
            if (word == null) return string.Empty;
            foreach (string wordToReplace in wordsToReplace)
            {
                if (isCaseSensitive)
                {
                    if (string.CompareOrdinal(word, wordToReplace) == 0) return wordToUse;
                }
                else
                {
                    if (string.CompareOrdinal(word.ToLower(), wordToReplace.ToLower()) == 0) return wordToUse;
                }
            }
            return word;
        }

        /// <summary>
        /// Map multiple word replacements to apply in a dictionary.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="wordMap">The word map in the form of {oldWord, newWord}.</param>
        /// <param name="isCaseSensitive">True: Case sensitivity is considered.</param>
        /// <returns>string.</returns>
        public static string ReplaceUsingMap(string word,
            Dictionary<string, string> wordMap,
            bool isCaseSensitive = false)
        {
            if (word == null) return string.Empty;
            foreach (string wordKey in wordMap.Keys)
            {
                if (isCaseSensitive)
                {
                    if (string.CompareOrdinal(word, wordKey) == 0) return wordMap[wordKey];
                }
                else
                {
                    if (string.CompareOrdinal(word.ToLower(), wordKey.ToLower()) == 0) return wordMap[wordKey];
                }
                
            }

            return word;
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Raises a messenger event with a warning that the string searched for was not found in the original text.
        /// </summary>
        /// <param name="stringSearchedFor"></param>
        /// <param name="originalText"></param>
        private static void RaiseMessengerStringNotFound(string stringSearchedFor, string originalText)
        {
            //if (Messenger != null)
            //{
            //    Messenger(new MessengerEventArgs("String '{0}' not found within text '{1}'. {2}Name will remain unchanged.", stringSearchedFor, originalText, Environment.NewLine));
            //}
        }


        /// <summary>
        /// Returns a single string character. 
        /// If the string has more than one character, only the first one is returned.
        /// </summary>
        /// <param name="character">Character to potentially trim.</param>
        /// <returns></returns>
        private static string GetCharacter(string character)
        {
            return character.Length > 1 ? character[0].ToString() : character;
        }


        /// <summary>
        /// Adds a sign to the beginning of the target string if one exists at the prior character in the string checked.
        /// </summary>
        /// <param name="targetString">String to add the character to, if applicable.</param>
        /// <param name="stringToCheck">String to check for the presence of the character.</param>
        /// <param name="currentIndex">Index associated with the target string in the string to check.</param>
        /// <returns></returns>
        private static string PrependSign(string targetString, string stringToCheck, int currentIndex)
        {
            int remainingCharacters = stringToCheck.Length - 1 - currentIndex;
            if ((remainingCharacters <= 0 || 0 >= currentIndex)) return targetString;

            char priorCharacter = stringToCheck[currentIndex - 1];
            if (priorCharacter == '-' || priorCharacter == '+')
                return (priorCharacter + targetString);
            return targetString;
        }


        /// <summary>
        /// Adds a decimal to the end of the target string if one exists at the next character in the string checked.
        /// </summary>
        /// <param name="targetString">String to add the character to, if applicable.</param>
        /// <param name="stringToCheck">String to check for the presence of the character.</param>
        /// <param name="currentIndex">Index associated with the target string in the string to check.</param>
        /// <returns></returns>
        private static string AppendDecimal(string targetString, string stringToCheck, int currentIndex)
        {

            int remainingCharacters = stringToCheck.Length - 1 - currentIndex;
            if (remainingCharacters <= 1) return targetString;

            char nextCharacter = stringToCheck[currentIndex + 1];
            char twoCharactersAhead = stringToCheck[currentIndex + 2];

            if ((nextCharacter == '.') && Char.IsNumber(twoCharactersAhead))
                return (targetString + stringToCheck[currentIndex + 1]);
            return targetString;
        }


        private static bool IsTwoItemListWithoutJoiner(string message, string joiner, bool alwaysUseJoiner)
        {
            string[] strings = message.Split(new [] { joiner }, StringSplitOptions.None);
                return ((!alwaysUseJoiner && strings.Length == 1) || string.IsNullOrWhiteSpace(joiner));
        }
        #endregion
    }
}
