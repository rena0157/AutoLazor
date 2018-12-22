using System;

namespace AutoLazer.Core
{
    public class AutoListPatterns
    {
        public static readonly string LinesLengthPattern = @"[L,l]ength\s+=?\s*(?<double>\d+\.?\d*)";

        public static readonly string HatchAreaPattern = @"[A,a]rea\s*(?<double>\d+\.?\d*)";

        public static readonly string TextPattern = @"(text|Contents:)\s*(?<string>.*)";
    }
}