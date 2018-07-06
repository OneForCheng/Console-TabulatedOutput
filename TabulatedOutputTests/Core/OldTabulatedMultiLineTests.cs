using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput;
using TabulatedOutput.Core;

namespace TabulatedOutputTests.Core
{
    [TestClass()]
    public class OldTabulatedMultiLineTests
    {
        private const string Separator = " ";

        [TestMethod()]
        public void should_return_correct_tabulated_line_when_frequent_column_is_same()
        {
            var lines = new[]
            {
                new SplitedLine("1 2",Separator),
                new SplitedLine("1 2 3",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│1 2       │", tabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1 │2 │3 │", tabulatedMultiLine.GetLine(2));
            
        }


        [TestMethod()]
        public void should_return_correct_tabulated_line_when_column_is_different()
        {
            var splitedLines = new[]
            {
                new SplitedLine("111 2222 333",Separator),
                new SplitedLine("1 2222",Separator),
                new SplitedLine("1111111 2222 3",Separator),
                new SplitedLine("1 2 33333333",Separator),
                new SplitedLine("111 22 33 4",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(splitedLines, splitedLines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│111     │2222│333     │", tabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1 2222                  │", tabulatedMultiLine.GetLine(2));
            Assert.AreEqual("│1111111 │2222│3       │", tabulatedMultiLine.GetLine(3));
            Assert.AreEqual("│1       │2   │33333333│", tabulatedMultiLine.GetLine(4));
            Assert.AreEqual("│111     │22  │33 4    │", tabulatedMultiLine.GetLine(5));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_line_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1 2 3",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│1 2成     │", tabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1 │2 │3 │", tabulatedMultiLine.GetLine(2));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_border_when_frequent_column_is_same()
        {
            var lines = new[]
            {
                new SplitedLine("1 2 333333",Separator),
                new SplitedLine("1 2 3 4",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌───────┐", tabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├─┬─┬─┬─┤", tabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("└─┴─┴─┴─┘", tabulatedMultiLine.GetBorder(3));
        }


        [TestMethod()]
        public void should_return_correct_tabulated_border_when_column_is_different()
        {
            var lines = new[]
            {
                new SplitedLine("111 2222 333",Separator),
                new SplitedLine("1 2222",Separator),
                new SplitedLine("1111111 2222 3",Separator),
                new SplitedLine("1 2 33333333",Separator),
                new SplitedLine("111 22 33 4",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌────┬──┬────┐", tabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├────┴──┴────┤", tabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("├────┬──┬────┤", tabulatedMultiLine.GetBorder(3));
            Assert.AreEqual("├────┼──┼────┤", tabulatedMultiLine.GetBorder(4));
            Assert.AreEqual("├────┼──┼────┤", tabulatedMultiLine.GetBorder(5));
            Assert.AreEqual("└────┴──┴────┘", tabulatedMultiLine.GetBorder(6));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_border_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌─┬──┐", tabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├─┴──┤", tabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("└────┘", tabulatedMultiLine.GetBorder(3));
        }

        [TestMethod()]
        public void should_return_empty_result_when_lines_is_empty()
        {
            var lines = new List<SplitedLine>();
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.IsFalse(tabulatedMultiLine.GetResult().Any());  
        }

        [TestMethod()]
        public void should_return_correct_tabulated_result_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1",Separator),
            };
            OldTabulatedMultiLine tabulatedMultiLine = new OldTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            var table = string.Join(Environment.NewLine, tabulatedMultiLine.GetResult());
            Assert.AreEqual("┌─┬──┐" + Environment.NewLine +
                            "│1 │2成 │" + Environment.NewLine +
                            "├─┴──┤" + Environment.NewLine +
                            "│1       │"    + Environment.NewLine +
                            "└────┘", table);
        }


    }
}