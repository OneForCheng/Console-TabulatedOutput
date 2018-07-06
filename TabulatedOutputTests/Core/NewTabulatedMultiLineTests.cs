using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput;
using TabulatedOutput.Core;

namespace TabulatedOutputTests.Core
{
    [TestClass()]
    public class NewTabulatedMultiLineTests
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
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│1 2  │", newTabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1│2│3│", newTabulatedMultiLine.GetLine(2));
            
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
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(splitedLines, splitedLines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│111    │2222│333     │", newTabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1 2222               │", newTabulatedMultiLine.GetLine(2));
            Assert.AreEqual("│1111111│2222│3       │", newTabulatedMultiLine.GetLine(3));
            Assert.AreEqual("│1      │2   │33333333│", newTabulatedMultiLine.GetLine(4));
            Assert.AreEqual("│111    │22  │33 4    │", newTabulatedMultiLine.GetLine(5));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_line_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1 2 3",Separator),
            };
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("│1 2成│", newTabulatedMultiLine.GetLine(1));
            Assert.AreEqual("│1│2│3│", newTabulatedMultiLine.GetLine(2));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_border_when_frequent_column_is_same()
        {
            var lines = new[]
            {
                new SplitedLine("1 2 333333",Separator),
                new SplitedLine("1 2 3 4",Separator),
            };
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌──────────┐", newTabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├─┬─┬─┬────┤", newTabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("└─┴─┴─┴────┘", newTabulatedMultiLine.GetBorder(3));
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
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌───────┬────┬────────┐", newTabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├───────┴────┴────────┤", newTabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("├───────┬────┬────────┤", newTabulatedMultiLine.GetBorder(3));
            Assert.AreEqual("├───────┼────┼────────┤", newTabulatedMultiLine.GetBorder(4));
            Assert.AreEqual("├───────┼────┼────────┤", newTabulatedMultiLine.GetBorder(5));
            Assert.AreEqual("└───────┴────┴────────┘", newTabulatedMultiLine.GetBorder(6));
        }

        [TestMethod()]
        public void should_return_correct_tabulated_border_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1 2 3",Separator),
            };
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.AreEqual("┌─────┐", newTabulatedMultiLine.GetBorder(1));
            Assert.AreEqual("├─┬─┬─┤", newTabulatedMultiLine.GetBorder(2));
            Assert.AreEqual("└─┴─┴─┘", newTabulatedMultiLine.GetBorder(3));
        }

        [TestMethod()]
        public void should_return_empty_result_when_lines_is_empty()
        {
            var lines = new List<SplitedLine>();
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            Assert.IsFalse(newTabulatedMultiLine.GetResult().Any());  
        }

        [TestMethod()]
        public void should_return_correct_tabulated_result_when_line_contains_chinese_character()
        {
            var lines = new[]
            {
                new SplitedLine("1 2成",Separator),
                new SplitedLine("1 2 3",Separator),
            };
            NewTabulatedMultiLine newTabulatedMultiLine = new NewTabulatedMultiLine(lines, lines.GetDefaultTabulatedColumn());
            var table = string.Join(Environment.NewLine, newTabulatedMultiLine.GetResult());
            Assert.AreEqual("┌─────┐" + Environment.NewLine +
                            "│1 2成│"      + Environment.NewLine +
                            "├─┬─┬─┤" + Environment.NewLine +
                            "│1│2│3│"    + Environment.NewLine +
                            "└─┴─┴─┘", table);
        }


    }
}