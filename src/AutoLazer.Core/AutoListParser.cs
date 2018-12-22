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

        public static List<Block> GetBlocks(string inputText)
        {
            var textObjects = GetObjects<string>(inputText, AutoListPatterns.TextPattern);
            var lengths = GetObjects<double>(inputText, AutoListPatterns.LinesLengthPattern);
            var areas = GetObjects<double>(inputText, AutoListPatterns.HatchAreaPattern);

            const string orderValidationPattern = @"(LINE|LWPOLYLINE|HATCH|TEXT|MTEXT|ARC)";
            var matches = Regex.Matches(inputText, orderValidationPattern);

            var textIndex = 0;
            var lineIndex = 0;
            var areaIndex = 0;

            string currentText = null;
            double currentLength = 0;
            double currentArea = 0;
            var blocks = new List<Block>(textObjects.Capacity);

            for ( var matchIndex = 0; matchIndex < matches.Count; ++matchIndex )
            {
                var currentMatch = matches[matchIndex];

                // Get the initial block ID
                if ( currentText == null && ( currentMatch.Value == "TEXT" || currentMatch.Value == "MTEXT" ) )
                {
                    currentText = textObjects[textIndex++];
                    continue;
                }

                // Add length of a line and polyline to the total length
                if ( currentMatch.Value == "LWPOLYLINE" || currentMatch.Value == "LINE" ||currentMatch.Value == "ARC")
                {
                    currentLength += lengths[lineIndex++];
                    continue;
                }

                // If the current item is a hatch then add the area of the hatch
                // to the list
                if ( currentMatch.Value == "HATCH" )
                {
                    currentArea += areas[areaIndex++];
                    continue;
                }

                // Build the block and place it into the list
                if ( currentText != null && ( currentMatch.Value == "TEXT" || currentMatch.Value == "MTEXT" ) )
                {
                    blocks.Add(new Block(currentText, currentLength, currentArea));
                    currentText = textObjects[textIndex++];
                    currentLength = 0;
                    currentArea = 0;
                }
            }

            // Build the final block
            if ( blocks.Count < textObjects.Count )
                blocks.Add(new Block(currentText, currentLength, currentArea));

            return blocks;
        }

    }
}
