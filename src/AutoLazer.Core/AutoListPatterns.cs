using System;

namespace AutoLazer.Core
{
    enum AutoListPatterns
    {
        LinesLengthPattern = @"[L,l]ength\s+=?\s*(?<double>\d+\.?\d*)",

        HatchAreaPattern = @"[A,a]rea\s*(?<double>\d+\.?\d*)",

        TextPattern = @"(text|Contents:)\s*(?<string>.*)",
    }
}