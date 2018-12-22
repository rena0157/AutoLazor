using System;

namespace AutoLazer.Core
{
    public class AutoListPatterns
    {
        public const string LinesLengthPattern = @"[L,l]ength\s+=?\s*(?<target>\d+\.?\d*)";

        public const string HatchAreaPattern = @"[A]rea\s*(?<target>\d+\.?\d*)";

        public const string TextPattern = @"([T,t]ext|Contents:)\s*(?<target>.*)";
    }
}