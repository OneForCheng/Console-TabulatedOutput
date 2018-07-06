using System;

namespace TabulatedOutput.CmdEnd
{
    public class CmdMessage
    {
        public static string GetUsageErrorPrompt()
        {
            return $"{Environment.NewLine}信息: 键入 \"{Extentions.GetApplicationShortName().ToUpper()} /? \" 了解用法信息。";
        }

        public static string GetUsage()
        {
            var spaces = new string(' ', 4);
            var firstLine = $"{Environment.NewLine}将重定向的输入表格化输出。{Environment.NewLine}";
            var secondLine = $"DIR | {Extentions.GetApplicationShortName().ToUpper()} [column [split]] [/x]{Environment.NewLine}";
            var thirdLine = $"{spaces}column       表示指定表格化输出的列数。";
            var fourthLine = $"{spaces}split        表示行的分割字符串，默认为单个空格。";
            var fifthLine =  $"{spaces}/x           表示使用兼容模式，支持旧版命令行。";

            return firstLine + Environment.NewLine +
                   secondLine + Environment.NewLine +
                   thirdLine + Environment.NewLine +
                   fourthLine + Environment.NewLine +
                   fifthLine;
        }
    }
}