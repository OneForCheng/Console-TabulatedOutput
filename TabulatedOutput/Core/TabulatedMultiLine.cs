using System.Collections.Generic;
using System.Linq;
using System.Text;
using TabulatedOutput.Data;

namespace TabulatedOutput.Core
{
    public abstract class TabulatedMultiLine
    {
        public static TabulatedMultiLine CreateNew(IEnumerable<SplitedLine> splitedLines, int column)
        {
            return new NewTabulatedMultiLine(splitedLines, column);
        }

        public static TabulatedMultiLine CreateOld(IEnumerable<SplitedLine> splitedLines, int column)
        {   
            return new OldTabulatedMultiLine(splitedLines, column);
        }

        protected readonly int Column;
        protected readonly LimitedSplitedLine[] SplitedLines;

        protected TabulatedMultiLine(IEnumerable<SplitedLine> splitedLines, int column)
        {
            SplitedLines = splitedLines.Select(m => m.GetLimitedSplitedLine(column)).ToArray();
            Column = SplitedLines.Any(m => m.WordCount == column) ? column : SplitedLines.GetDefaultTabulatedColumn();

        }

        public IEnumerable<string> GetResult()
        {
            if (!SplitedLines.Any()) yield break;
            var rows = SplitedLines.Length * 2 + 1;
            for (int i = 1; i <= rows; i++)
            {
                if (i % 2 == 1)
                    yield return GetBorder(i / 2 + 1);
                else
                    yield return GetLine(i / 2);
            }
        }

        public string GetLine(int row)
        {
            var splitedLine = SplitedLines[row - 1];
            if (splitedLine.WordCount == Column)
                return GetMultiColumnLine(splitedLine);
            else
                return GetSingleColumnLine(splitedLine);
        }

        private string GetMultiColumnLine(SplitedLine splitedLine)
        {
            var tabulatedLine = TableBorderCharacters.Vertical.ToString();
            var leftAlignWides = GetColumnWidths();
            for (int i = 0; i < Column; i++)
            {
                tabulatedLine += splitedLine.Words[i] + string.Empty.PadRight(leftAlignWides[i] - Encoding.Default.GetByteCount(splitedLine.Words[i]), ' ') + TableBorderCharacters.Vertical;
            }
            return tabulatedLine;
        }

        private string GetSingleColumnLine(SplitedLine splitedLine)
        {
            return TableBorderCharacters.Vertical + splitedLine.Origin + string.Empty.PadRight(GetLineWidth() - Encoding.Default.GetByteCount(splitedLine.Origin), ' ') + TableBorderCharacters.Vertical;
        }

        private int GetLineWidth()
        {
            return GetColumnWidths().Sum() + GetSplitedColumnWidth();
        }

        protected abstract int GetSplitedColumnWidth();

        private int[] GetColumnWidths()
        {
            var columnWidths = new int[Column];
            for (int i = 0; i < Column; i++) columnWidths[i] = GetColumnWidth(i);

            var lineWide = columnWidths.Sum() + GetSplitedColumnWidth();
            columnWidths[Column - 1] += GetLineWidthAlignOffset(lineWide);

            return columnWidths;
        }

        protected abstract int GetColumnWidth(int index);

        protected abstract int GetMaxSingleColumnWith();

        private int GetLineWidthAlignOffset(int lineWide)
        {
            var offset = GetMaxSingleColumnWith() - lineWide;
            if (offset < 0) return 0;
            return offset;
        }

        public string GetBorder(int row)
        {
            if (row <= 1) return GetTopBorder();
            if (row > SplitedLines.Length) return GetBottomBorder();
            return GetLineTopBorder(row);
        }

        private string GetTopBorder()
        {
            var separator = SplitedLines.First().WordCount == Column ? TableBorderCharacters.BottomHalfCross : TableBorderCharacters.Horizontal;
            return GetBorder(TableBorderCharacters.TopLeft, separator, TableBorderCharacters.TopRight);
        }

        private string GetBottomBorder()
        {
            var separator = SplitedLines.Last().WordCount == Column ? TableBorderCharacters.TopHalfCross : TableBorderCharacters.Horizontal;
            return GetBorder(TableBorderCharacters.BottomLeft, separator, TableBorderCharacters.BottomRight);

        }

        private string GetLineTopBorder(int row)
        {
            var separator = GetBorderSeparator(row);
            return GetBorder(TableBorderCharacters.RightHalfCross, separator, TableBorderCharacters.LeftHalfCross);
        }

        private string GetBorder(char startChar, char separator, char endChar)
        {
            var columnWidths = GetColumnWidths();
            var border = startChar.ToString();
            for (int i = 0; i < Column; i++)
            {
                if (i > 0) border += separator;
                border += string.Empty.PadRight(GetColumnBorderCount(columnWidths[i]), TableBorderCharacters.Horizontal); 
            }
            border += endChar;
            return border;
        }

        protected abstract int GetColumnBorderCount(int columnWidth);

        private char GetBorderSeparator(int row)
        {
            var curLineWordCount = SplitedLines[row - 1].WordCount;
            var preLineWordCount = SplitedLines[row - 2].WordCount;
            if (curLineWordCount == Column && preLineWordCount == Column) return TableBorderCharacters.Cross;
            if (curLineWordCount != Column && preLineWordCount == Column) return TableBorderCharacters.TopHalfCross;
            if (curLineWordCount == Column && preLineWordCount != Column) return TableBorderCharacters.BottomHalfCross;
            return TableBorderCharacters.Horizontal;

        }
    }
}