// AutoListParser.cs
// By: Adam Renaud
// Created: 2018-12-22

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoLazer.Core
{
    /// <summary>
    /// AutoList parser. A class that parses AutoCAD List commands
    /// </summary>
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
            // create the pattern, get matches and create the return list
            var regex = new Regex(pattern);
            var matchCollection = regex.Matches(inputText);
            var returnList = new List<T>();

            // Extract the 'target' from each match
            foreach (Match match in matchCollection)
                returnList.Add((T)Convert
                    .ChangeType(match.Groups["target"]
                    .Value, typeof(T)));

            return returnList;
        }

        /// <summary>
        /// Get all of the blocks from the input text
        /// </summary>
        /// <param name="inputText">The text that the blocks will be extracted from</param>
        /// <returns>A list of blocks</returns>
        public static List<Block> GetBlocks(string inputText)
        {
            // Get all of the objects from the input text
            var textObjects = GetObjects<string>(inputText, AutoListPatterns.TextPattern);
            var lengths = GetObjects<double>(inputText, AutoListPatterns.LinesLengthPattern);
            var areas = GetObjects<double>(inputText, AutoListPatterns.HatchAreaPattern);

            // Order validation pattern to determine that list of objects that in the input text
            const string orderValidationPattern = @"(LINE|LWPOLYLINE|HATCH|TEXT|MTEXT|ARC)";
            var matches = Regex.Matches(inputText, orderValidationPattern);

            var textIndex = 0;
            var lineIndex = 0;
            var areaIndex = 0;

            // Set the default current values
            string currentText = null;
            double currentLength = 0;
            double currentArea = 0;

            // This will be the return list - filled with text objects capactity
            var blocks = new List<Block>(textObjects.Capacity);

            // Main loop to build the blocks and place them into the list
            for ( var matchIndex = 0; matchIndex < matches.Count; ++matchIndex )
            {
                var currentMatch = matches[matchIndex];

                // Get the initial block ID
                if ( textIndex < textObjects.Count && currentText == null 
                    && ( currentMatch.Value == "TEXT" || currentMatch.Value == "MTEXT" ) )
                {
                    currentText = textObjects[textIndex++];
                    continue;
                }

                // Add length of a line and polyline to the total length, now also arcs
                if ( lineIndex < lengths.Count 
                    && (currentMatch.Value == "LWPOLYLINE" || currentMatch.Value == "LINE" 
                    ||currentMatch.Value == "ARC"))
                {
                    currentLength += lengths[lineIndex++];
                    continue;
                }

                // If the current item is a hatch then add the area of the hatch
                // to the list
                if ( areaIndex < areas.Count && currentMatch.Value == "HATCH" )
                {
                    currentArea += (areas[areaIndex++] / 10000);
                    continue;
                }

                // Build the block and place it into the list
                if ( textIndex < textObjects.Count 
                    && currentText != null && ( currentMatch.Value == "TEXT" || currentMatch.Value == "MTEXT" ) )
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

        /// <summary>
        /// Turn a list of blocks into a CSV string
        /// </summary>
        /// <param name="blocks">The blocks that will be added to the CSV</param>
        /// <returns>A CSV formatted string</returns>
        public static string BlocksToCsv(List<Block> blocks)
        {
            // declare string builder and append the headings to it
            StringBuilder sb = new StringBuilder();
            var headers = "Block ID,Frontage,Area";
            sb.AppendLine(headers);

            // For each block append a new line that contains the block
            // information
            foreach(var block in blocks)
                sb.AppendLine($"{block.Id},{block.Frontage},{block.Area}");

            // Return the string
            return sb.ToString();
        }
    }
}
