using System.Collections.Generic;
using System.Linq;
using TabulatedOutput.Core;

namespace TabulatedOutput.CmdEnd
{

    
    public class CmdEntry 
    {
        private readonly List<string> _lines;
        private readonly CmdParam _cmdParam;

        public CmdEntry(List<string> lines, CmdParam cmdParam)
        {
            _lines = lines;
            _cmdParam = cmdParam;
        }

        public IEnumerable<string> GetExecuteResult()
        {
            var lines = GetSplitedLines();
            var column = GetTabulatedColumn(lines);
            var isCompatibleMode = _cmdParam.IsCompatibleModeParam();

            var tabulatedMultiLine = isCompatibleMode
                ? TabulatedMultiLine.CreateOld(lines, column)
                : TabulatedMultiLine.CreateNew(lines, column);
            return tabulatedMultiLine.GetResult();
            
        }

        private int GetTabulatedColumn(SplitedLine[] splitedLines)
        {
            var column = _cmdParam.GetColumnParam();
            if (column <= 0) column = splitedLines.GetDefaultTabulatedColumn();
            return column;
        }

        private SplitedLine[] GetSplitedLines()
        {
            return _lines.Select(m => new SplitedLine(m, _cmdParam.GetSeparatorParam())).ToArray();
        }
    }
}