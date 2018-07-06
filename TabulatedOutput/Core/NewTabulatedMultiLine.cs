using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabulatedOutput.Core
{
    public class NewTabulatedMultiLine : TabulatedMultiLine
    {
        public NewTabulatedMultiLine(IEnumerable<SplitedLine> splitedLines, int column):base(splitedLines, column)
        {
            
        }

        protected override int GetSplitedColumnWidth()
        {
            return Column - 1;
        }

        protected override int GetColumnWidth(int index)
        {
            return SplitedLines.Where(m => m.WordCount == Column).Max(m => Encoding.Default.GetByteCount(m.Words[index]));
        }

        protected override int GetMaxSingleColumnWith()
        {
            if (SplitedLines.All(m => m.WordCount == Column)) return 0;
            return SplitedLines.Where(m => m.WordCount != Column).Max(item => Encoding.Default.GetByteCount(item.Origin));
        }

        protected override int GetColumnBorderCount(int columnWidth)
        {
            return columnWidth;
        }
    }
}