using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AutoLazer.Core;
using System.Collections.Generic;
using System.Linq;

namespace AutoLazer.App
{
    /// <summary>
    /// The Index Component Code backing class
    /// </summary>
    public class IndexComponent : BlazorComponent
    {
        private double _totalLength = 0.0;
        private double _totalArea = 0.0;

        public IndexComponent()
        {
            Blocks = new List<Block>();
        }

        /// <summary>
        /// The Input Text
        /// </summary>
        public string InputText;

        /// <summary>
        /// The total length from the input text
        /// </summary>
        public string TotalLength => $"{_totalLength:N3} {LinearUnit}";

        /// <summary>
        /// The Total Area string from the input text
        /// </summary>
        /// <value></value>
        public string TotalArea => $"{_totalArea:N3} {AreaUnit}";

        /// <summary>
        /// The Current linear unit
        /// </summary>
        /// <value></value>
        public string LinearUnit {get; set;} = "m";

        /// <summary>
        /// The Current area unit
        /// </summary>
        /// <value></value>
        public string AreaUnit {get; set;} = "ha";

        /// <summary>
        /// A List of Blocks from the input text
        /// </summary>
        /// <value></value>
        public List<Block> Blocks {get; set;}

        /// <summary>
        /// Takes in the input area text and updates
        /// all of the other fields
        /// </summary>
        /// <param name="e"></param>
        public void InputAreaOnKeyUp(UIChangeEventArgs e)
        {
            InputText = e.Value.ToString();

            _totalLength = AutoListParser
                .GetObjects<double>(InputText, AutoListPatterns.LinesLengthPattern)
                .Sum();

            // Note that I divide by 10000 to convert to ha
            _totalArea = AutoListParser
                .GetObjects<double>(InputText, AutoListPatterns.HatchAreaPattern)
                .Sum() / 10000;

            Blocks = AutoListParser.GetBlocks(InputText);
        }
    }
}