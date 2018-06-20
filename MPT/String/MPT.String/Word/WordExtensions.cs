// ***********************************************************************
// Assembly         : MPT.String
// Author           : Mark Thomas
// Created          : 12-01-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2017
// ***********************************************************************
// <copyright file="WordExtensions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace MPT.String.Word
{
    /// <summary>
    /// Contains methods for operating on words or particular words within phrases.
    /// </summary>
    public static class WordExtensions
    {
        /// <summary>
        /// Applies an action to individual words.
        /// </summary>
        /// <param name="wordOrPhrase">The word or phrase.</param>
        /// <param name="action">The action.</param>
        /// <returns>System.String.</returns>
        public static string ApplyToIndividualWords(this string wordOrPhrase, Func<string, string> action)
        {
            string[] words = wordOrPhrase.Split(' ');
            string modifiedWordOrPhrase = string.Empty;
            foreach (string word in words)
            {
                string modifiedWord = action(word);
                if (!string.IsNullOrEmpty(modifiedWord)) modifiedWord += ' ';   // Only add the space character if the word is not empty.
                modifiedWordOrPhrase += modifiedWord;
            }
            modifiedWordOrPhrase = modifiedWordOrPhrase.Trim();

            return modifiedWordOrPhrase;
        }

        /// <summary>
        /// Applies an action to individual words.
        /// </summary>
        /// <param name="wordOrPhrase">The word or phrase.</param>
        /// <param name="action">The action.</param>
        /// <param name="actionInput">The action input.</param>
        /// <returns>System.String.</returns>
        public static string ApplyToIndividualWords(this string wordOrPhrase, Func<string, string, string> action, string actionInput)
        {
            string[] words = wordOrPhrase.Split(' ');
            string modifiedWordOrPhrase = string.Empty;
            foreach (string word in words)
            {
                string modifiedWord = action(word, actionInput);
                if (!string.IsNullOrEmpty(modifiedWord)) modifiedWord += ' ';   // Only add the space character if the word is not empty.
                modifiedWordOrPhrase += modifiedWord;
            }
            modifiedWordOrPhrase = modifiedWordOrPhrase.Trim();

            return modifiedWordOrPhrase;
        }

        /// <summary>
        /// Applies an action to individual words.
        /// </summary>
        /// <param name="wordOrPhrase">The word or phrase.</param>
        /// <param name="action">The action.</param>
        /// <param name="actionInputs">The action inputs.</param>
        /// <param name="actionInput">The action input.</param>
        /// <returns>System.String.</returns>
        public static string ApplyToIndividualWords(this string wordOrPhrase, Func<string, List<string>, string, string> action, List<string> actionInputs, string actionInput)
        {
            string[] words = wordOrPhrase.Split(' ');
            string modifiedWordOrPhrase = string.Empty;
            foreach (string word in words)
            {
                string modifiedWord = action(word, actionInputs, actionInput);
                if (!string.IsNullOrEmpty(modifiedWord)) modifiedWord += ' ';   // Only add the space character if the word is not empty.
                modifiedWordOrPhrase += modifiedWord;
            }
            modifiedWordOrPhrase = modifiedWordOrPhrase.Trim();

            return modifiedWordOrPhrase;
        }

        /// <summary>
        /// Applies an action to individual words.
        /// </summary>
        /// <param name="wordOrPhrase">The word or phrase.</param>
        /// <param name="action">The action.</param>
        /// <param name="actionInputs">The action inputs.</param>
        /// <returns>System.String.</returns>
        public static string ApplyToIndividualWords(this string wordOrPhrase, Func<string, Dictionary<string, string>, string> action, Dictionary<string, string> actionInputs)
        {
            string[] words = wordOrPhrase.Split(' ');
            string modifiedWordOrPhrase = string.Empty;
            foreach (string word in words)
            {
                string modifiedWord = action(word, actionInputs);
                if (!string.IsNullOrEmpty(modifiedWord)) modifiedWord += ' ';   // Only add the space character if the word is not empty.
                modifiedWordOrPhrase += modifiedWord;
            }
            modifiedWordOrPhrase = modifiedWordOrPhrase.Trim();

            return modifiedWordOrPhrase;
        }
    }
}
