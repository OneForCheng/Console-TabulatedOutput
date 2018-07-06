using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput;
using TabulatedOutput.Core;

namespace TabulatedOutputTests
{
    [TestClass()]
    public class ExtentionsTests
    {
        private const string Separator = " ";

        [TestMethod()]
        public void should_return_frequent_column_when_column_is_different()
        {
            var lines = new[]
            {
                new SplitedLine("1 2", Separator),
                new SplitedLine("1 2 3", Separator),
                new SplitedLine("1 2 3", Separator),
                new SplitedLine("1 2 3", Separator),
                new SplitedLine("1 2 3 4", Separator),
                new SplitedLine("1 2 3 4", Separator),
            };
         
            Assert.AreEqual(3, lines.GetDefaultTabulatedColumn());
        }

        [TestMethod()]
        public void should_return_max_column_when_frequent_column_is_same()
        {

            var lines = new[]
            {
                new SplitedLine("1 2",Separator),
                new SplitedLine("1 2 3",Separator),
                new SplitedLine("1 2 3 4",Separator),
            };
            Assert.AreEqual(4, lines.GetDefaultTabulatedColumn());
        }

        [TestMethod()]
        public void get_a_limited_splited_line_from_a_splited_line()
        {
            SplitedLine splitedLine = new SplitedLine(" a        simple   test! ", " ");
            var limitedSplitedLine = splitedLine.GetLimitedSplitedLine(2);
            var words = limitedSplitedLine.Words;
            Assert.AreEqual(" a        simple   test! ", limitedSplitedLine.Origin);
            Assert.AreEqual(" ", limitedSplitedLine.Separator);
            Assert.AreEqual(2, limitedSplitedLine.WordCount);
            Assert.AreEqual("a", words[0]);
            Assert.AreEqual("simple   test! ", words[1]);
        }
    }
}