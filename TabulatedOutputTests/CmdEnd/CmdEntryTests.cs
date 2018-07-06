using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput.CmdEnd;

namespace TabulatedOutputTests.CmdEnd
{
    [TestClass()]
    public class CmdEntryTests
    {
        
        [TestMethod()]
        public void should_return_single_characters_wide_table_border_when_turn_off_compatible_mode()
        {
            var lines = new List<string>()
            {
                "1 2成",
                "1 2 3",
            };
            var cmdParam = new CmdParam(new [] {"3", " "});
            var cmdEntry = new CmdEntry(lines,cmdParam);
            var table = string.Join(Environment.NewLine, cmdEntry.GetExecuteResult());
            Assert.AreEqual("┌─────┐" + Environment.NewLine +
                            "│1 2成│" + Environment.NewLine +
                            "├─┬─┬─┤" + Environment.NewLine +
                            "│1│2│3│" + Environment.NewLine +
                            "└─┴─┴─┘", table);

        }

        [TestMethod()]
        public void should_return_double_characters_wide_table_border_when_turn_on_compatible_mode()
        {
            var lines = new List<string>()
            {
                "1 2成",
                "1 2 3",
            };
            var cmdParam = new CmdParam(new[] { "3", " ", "/x" });
            var cmdEntry = new CmdEntry(lines, cmdParam);
            var table = string.Join(Environment.NewLine, cmdEntry.GetExecuteResult());
            Assert.AreEqual("┌─────┐" + Environment.NewLine +
                            "│1 2成     │" + Environment.NewLine +
                            "├─┬─┬─┤" + Environment.NewLine +
                            "│1 │2 │3 │" + Environment.NewLine +
                            "└─┴─┴─┘", table);

        }
    }
}