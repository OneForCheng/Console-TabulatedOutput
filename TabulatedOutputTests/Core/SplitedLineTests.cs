using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput.Core;

namespace TabulatedOutputTests.Core
{
    [TestClass()]
    public class SplitedLineTests
    {
        [TestMethod()]
        public void create_a_splited_line()
        {
            SplitedLine splitedLine = new SplitedLine("a  simple  test! ", " ");
            var words = splitedLine.Words;
            Assert.AreEqual("a  simple  test! ", splitedLine.Origin);
            Assert.AreEqual(" ", splitedLine.Separator);
            Assert.AreEqual(3, splitedLine.WordCount);
            Assert.AreEqual("a", words[0]);
            Assert.AreEqual("simple", words[1]);
            Assert.AreEqual("test!", words[2]);
            

        }
        
    }
}