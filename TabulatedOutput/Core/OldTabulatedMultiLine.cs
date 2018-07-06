using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabulatedOutput.Core
{
    public class OldTabulatedMultiLine : TabulatedMultiLine
    {
        
        public OldTabulatedMultiLine(IEnumerable<SplitedLine> splitedLines, int column) :base(splitedLines, column)
        {
        }


        protected override int GetSplitedColumnWidth()
        {
            return (Column - 1)*2;
        }

        protected override int GetColumnWidth(int index)
        {
            var columnWidth = SplitedLines.Where(m => m.WordCount == Column).Max(m => Encoding.Default.GetByteCount(m.Words[index]));
            return columnWidth + columnWidth % 2;
        }

        protected override int GetMaxSingleColumnWith()
        {
            if (SplitedLines.All(m => m.WordCount == Column)) return 0;
            var columnWith = SplitedLines.Where(m => m.WordCount != Column).Max(item => Encoding.Default.GetByteCount(item.Origin));
            return columnWith + columnWith % 2;
        }

        protected override int GetColumnBorderCount(int columnWidth)
        {
            return columnWidth/2;
        }

    }
}