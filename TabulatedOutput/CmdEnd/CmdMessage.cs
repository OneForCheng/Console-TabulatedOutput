using System;

namespace TabulatedOutput.CmdEnd
{
    public class CmdMessage
    {
        public static string GetUsageErrorPrompt()
        {
            return $"{Environment.NewLine}��Ϣ: ���� \"{Extentions.GetApplicationShortName().ToUpper()} /? \" �˽��÷���Ϣ��";
        }

        public static string GetUsage()
        {
            var spaces = new string(' ', 4);
            var firstLine = $"{Environment.NewLine}���ض���������������{Environment.NewLine}";
            var secondLine = $"DIR | {Extentions.GetApplicationShortName().ToUpper()} [column [split]] [/x]{Environment.NewLine}";
            var thirdLine = $"{spaces}column       ��ʾָ����������������";
            var fourthLine = $"{spaces}split        ��ʾ�еķָ��ַ�����Ĭ��Ϊ�����ո�";
            var fifthLine =  $"{spaces}/x           ��ʾʹ�ü���ģʽ��֧�־ɰ������С�";

            return firstLine + Environment.NewLine +
                   secondLine + Environment.NewLine +
                   thirdLine + Environment.NewLine +
                   fourthLine + Environment.NewLine +
                   fifthLine;
        }
    }
}