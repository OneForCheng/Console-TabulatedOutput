using System;
using System.Collections.Generic;
using System.Linq;
using TabulatedOutput.Core;

namespace TabulatedOutput
{
    public static class Extentions
    {
        public static string GetApplicationShortName()
        {
            var applicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
            var applicationShortName = applicationName.Substring(0, applicationName.LastIndexOf('.'));
            return applicationShortName;
        }

        public static int GetDefaultTabulatedColumn(this IEnumerable<SplitedLine> splitedLines)
        {
            var lines = splitedLines as SplitedLine[] ?? splitedLines.ToArray();
            if (lines.Any())
            {
                var groups =
                    lines.Select(m => m.WordCount)
                        .GroupBy(m => m)
                        .OrderByDescending(m => m.Count())
                        .ThenByDescending(m => m.Key);
                var first = groups.First();
                return first.Key;
            }
            return 0;
        }

        public static LimitedSplitedLine GetLimitedSplitedLine(this SplitedLine splitedLine, int limitedWordCount)
        {
            return new LimitedSplitedLine(splitedLine.Origin, splitedLine.Separator, limitedWordCount);
        }
    }
}