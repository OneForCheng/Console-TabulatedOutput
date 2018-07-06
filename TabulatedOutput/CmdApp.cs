using System;
using System.Collections.Generic;
using TabulatedOutput.CmdEnd;

namespace TabulatedOutput
{
    public class CmdApp
    {
        private const int Error = 1;
        private const int Success = 0;

        private static List<string> GetRedirectedInput()
        {
            var lines = new List<string>();
            var line = Console.ReadLine();
            while (line != null)
            {
                if (!string.IsNullOrWhiteSpace(line)) lines.Add(line);
                line = Console.ReadLine();
            }
            return lines;
        }

        public static int Main(string[] args)
        {
            var cmdParam = new CmdParam(args);
            if (cmdParam.ContainsUasgeParam())
            {
                Console.WriteLine(CmdMessage.GetUsage());
            }
            else if (Console.IsInputRedirected)
            {
                var cmdEntry = new CmdEntry(GetRedirectedInput(), cmdParam);
                foreach (var line in cmdEntry.GetExecuteResult()) Console.WriteLine(line);
            }
            else
            {
                Console.WriteLine(CmdMessage.GetUsageErrorPrompt());
                return Error;
            }
            return Success;
        }   
    }
}
