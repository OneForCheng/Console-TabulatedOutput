using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput.Core;

namespace TabulatedOutputTests.Core
{
    [TestClass()]
    public class LimitedSplitedLineTests
    {
        [TestMethod()]
        public void limited_word_count_equal_default_splited_word_count()
        {
            LimitedSplitedLine splitedLine = new LimitedSplitedLine(" a        simple   test! ", " ", 3);
            var words = splitedLine.Words;
            Assert.AreEqual(" a        simple   test! ", splitedLine.Origin);
            Assert.AreEqual(" ", splitedLine.Separator);
            Assert.AreEqual(3, splitedLine.WordCount);
            Assert.AreEqual("a", words[0]);
            Assert.AreEqual("simple", words[1]);
            Assert.AreEqual("test!", words[2]);

        }

        [TestMethod()]
        public void limited_word_count_less_than_default_splited_word_count()
        {
            LimitedSplitedLine splitedLine = new LimitedSplitedLine(" a        simple   test! ", " ", 2);
            var words = splitedLine.Words;
            Assert.AreEqual(" a        simple   test! ", splitedLine.Origin);
            Assert.AreEqual(" ", splitedLine.Separator);
            Assert.AreEqual(2, splitedLine.WordCount);
            Assert.AreEqual("a", words[0]);
            Assert.AreEqual("simple   test! ", words[1]);

        }

        [TestMethod()]
        public void limited_word_count_more_than_default_splited_word_count()
        {
            LimitedSplitedLine splitedLine = new LimitedSplitedLine(" a        simple   test! ", " ", 4);
            var words = splitedLine.Words;
            Assert.AreEqual(" a        simple   test! ", splitedLine.Origin);
            Assert.AreEqual(" ", splitedLine.Separator);
            Assert.AreEqual(1, splitedLine.WordCount);
            Assert.AreEqual(" a        simple   test! ", words[0]);

        }
    }
}