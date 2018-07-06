using System;
using System.Linq;

namespace TabulatedOutput.Core
{
    public class LimitedSplitedLine : SplitedLine
    {
        public LimitedSplitedLine(string source, string separator, int limitedWordCount) : base(source, separator)
        {
            if (limitedWordCount >= 1 && limitedWordCount <= WordCount)
            {
                if (limitedWordCount != WordCount)
                {
                    var words = new string[limitedWordCount];
                    if (limitedWordCount == 1)
                    {
                        words[0] = source;
                    }
                    else
                    {
                        var splits = source.Split(new[] { separator }, StringSplitOptions.None);
                        for (int i = 0; i < limitedWordCount - 1; i++)
                        {
                            words[i] = Words[i];
                        }
                        var count = 0;
                        for (int i = 0; i < splits.Length; i++)
                        {
                            if (splits[i] != string.Empty)
                            {
                                count++;
                                if (count == limitedWordCount)
                                {
                                    words[limitedWordCount - 1] = string.Join(separator, splits.Skip(i));
                                    break;
                                }
                            }

                        }
                       
                    }
                    Words = words;
                    WordCount = limitedWordCount;
                }
            }
            else
            {
                Words = new []{source};
                WordCount = 1;
            }
        }
    }
}