using System;

namespace TabulatedOutput.Core
{
    public class SplitedLine
    {
        public string Origin { get; }
        public string Separator { get; }
        public string[] Words { get; protected set; }
        public int WordCount { get; protected set; }
        
        public SplitedLine(string source, string separator)
        {
            Origin = source;
            Separator = separator;
            Words = source.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            WordCount = Words.Length;
        }

    }
}