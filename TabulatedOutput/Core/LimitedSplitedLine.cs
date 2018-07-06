using System;
using System.Linq;

namespace TabulatedOutput.Core
{
    public class LimitedSplitedLine : SplitedLine
    {
        public LimitedSplitedLine(string source, string separator, int limitedWordCount) : base(source, separator)
        {
            if (IsValidRange(limitedWordCount))
            {
                if (limitedWordCount == WordCount) return;
                Words = GetLimitedWords(source, separator, limitedWordCount);
                WordCount = limitedWordCount;
            }
            else
            {
                Words = new []{source};
                WordCount = 1;
            }
        }

        private bool IsValidRange(int limitedWordCount)
        {
            return limitedWordCount >= 1 && limitedWordCount <= WordCount;
        }

        private string[] GetLimitedWords(string source, string separator, int limitedWordCount)
        {
            if (limitedWordCount == 1)
            {
                return new[] {source};
            }
            else
            {
                var words = new string[limitedWordCount];
                var splits = source.Split(new[] {separator}, StringSplitOptions.None);
                for (var i = 0; i < limitedWordCount - 1; i++) words[i] = Words[i];
                var skipCount = GetSkipCount(splits, limitedWordCount);
                words[limitedWordCount - 1] = string.Join(separator, splits.Skip(skipCount));
                return words;
            }
            
        }

        private int GetSkipCount(string[] splits, int limitedWordCount)
        {
            var nonEmptyWordCount = 0;
            for (var i = 0; i < splits.Length; i++)
            {
                if (splits[i] == string.Empty) continue;
                nonEmptyWordCount++;
                if (nonEmptyWordCount == limitedWordCount) return i;
            }
            return 0;
        }
    }
}