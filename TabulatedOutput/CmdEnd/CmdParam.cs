using System.Linq;

namespace TabulatedOutput.CmdEnd
{
    public class CmdParam
    {
        private readonly string[] _args;

        public CmdParam(string[] args)
        {
            _args = args;
        }

        public string GetSeparatorParam()
        {
            if (_args.Length <= 1 || _args.Length == 2 && _args[1].ToLower() == "/x") return " ";
            return _args[1];
        }

        public int GetColumnParam()
        {
            if (_args.Length == 0)return 0;
            int column;
            if (!int.TryParse(_args[0], out column)) column = 0;
            return column;
        }

        public bool IsCompatibleModeParam()
        {
            return _args.Length > 0 && _args.Last().ToLower() == "/x";
        }

        public bool ContainsUasgeParam()
        {
            return _args.Any(item => item.Contains("/?"));
        }
    }
}