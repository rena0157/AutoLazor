using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutoLazer.Core
{
    public static class AutoListParser
    {
        /// <summary>
        /// Get objects from a string using Regular Expressions
        /// </summary>
        /// <param name="inputText">The input text</param>
        /// <param name="pattern">The pattern that is used, note that the
        /// Captured objects must be part of a group called 'target'</param>
        /// <typeparam name="T">The desired return type</typeparam>
        /// <returns>A list containing the objects that were taken from the string</returns>
        public static List<T> GetObjects<T>(string inputText, string pattern)
        {
            var regex = new Regex(pattern);
            var matchCollection = regex.Matches(inputText);
            var returnList = new List<T>();

            foreach (Match match in matchCollection)
                returnList.Add((T)Convert
                    .ChangeType(match.Groups["target"]
                    .Value, typeof(T)));

            return returnList;
        }
    }
}
