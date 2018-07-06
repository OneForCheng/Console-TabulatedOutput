using Microsoft.VisualStudio.TestTools.UnitTesting;
using TabulatedOutput.CmdEnd;

namespace TabulatedOutputTests.CmdEnd
{
    [TestClass()]
    public class CmdParamTests
    {
        [TestMethod()]
        public void should_return_given_separator_when_second_parameter_given_the_separator()
        {
            var cmdParam = new CmdParam(new[] { "6", "-", "/x" });
            Assert.AreEqual("-", cmdParam.GetSeparatorParam());
        }

        [TestMethod()]
        public void should_return_a_blank_space_when_second_parameter_is_the_reserved_parameter()
        {
            var cmdParam = new CmdParam(new[] { "6", "/x" });
            Assert.AreEqual(" ", cmdParam.GetSeparatorParam());
        }

        [TestMethod()]
        public void should_return_a_blank_space_when_second_parameter_is_not_exist()
        {
            var cmdParam = new CmdParam(new[] { "6"});
            Assert.AreEqual(" ", cmdParam.GetSeparatorParam());
        }

        [TestMethod()]
        public void should_return_a_blank_space_when_args_is_empty()
        {
            var cmdParam = new CmdParam(new string[] { });
            Assert.AreEqual(" ", cmdParam.GetSeparatorParam());
        }

        [TestMethod()]
        public void should_return_given_number_when_first_parameter_is_valid_numeric_string()
        {
            var cmdParam = new CmdParam(new[] { "6", "-", "/x" });
            Assert.AreEqual(6, cmdParam.GetColumnParam());
        }

        [TestMethod()]
        public void should_return_0_when_first_parameter_is_invalid_numeric_string()
        {
            var cmdParam = new CmdParam(new[] { "/x" });
            Assert.AreEqual(0, cmdParam.GetColumnParam());
        }

        [TestMethod()]
        public void should_return_0_when_args_is_empty()
        {
            var cmdParam = new CmdParam(new string[] { });
            Assert.AreEqual(0, cmdParam.GetColumnParam());
        }


        [TestMethod()]
        public void should_return_true_when_last_parameter_is_the_reserved_parameter()
        {
            var cmdParam1 = new CmdParam(new[] { "6", " ", "/x" });
            var cmdParam2 = new CmdParam(new[] { "6", "/x" });
            var cmdParam3 = new CmdParam(new[] { "/x" });
            Assert.AreEqual(true, cmdParam1.IsCompatibleModeParam());
            Assert.AreEqual(true, cmdParam2.IsCompatibleModeParam());
            Assert.AreEqual(true, cmdParam3.IsCompatibleModeParam());
        }


        [TestMethod()]
        public void should_return_false_when_last_parameter_is_not_the_reserved_parameter()
        {
            var cmdParam1 = new CmdParam(new[] { "6", "/x", " " });
            var cmdParam2 = new CmdParam(new[] { "6" });
            var cmdParam3 = new CmdParam(new string[] { });
            Assert.AreEqual(false, cmdParam1.IsCompatibleModeParam());
            Assert.AreEqual(false, cmdParam2.IsCompatibleModeParam());
            Assert.AreEqual(false, cmdParam3.IsCompatibleModeParam());
        }

        [TestMethod()]
        public void should_return_true_when_args_contains_the_usage_parameter()
        {
            var cmdParam1 = new CmdParam(new[] { "6", " ", "/?" });
            var cmdParam2 = new CmdParam(new[] { "6", "/?" });
            var cmdParam3 = new CmdParam(new[] { "/?", " " });
            Assert.AreEqual(true, cmdParam1.ContainsUasgeParam());
            Assert.AreEqual(true, cmdParam2.ContainsUasgeParam());
            Assert.AreEqual(true, cmdParam3.ContainsUasgeParam());
            
        }


        [TestMethod()]
        public void should_return_false_when_args_not_contains_the_usage_parameter()
        {
            
            var cmdParam1 = new CmdParam(new[] { "6", " ", "/x" });
            var cmdParam2 = new CmdParam(new string[] { });
            
            Assert.AreEqual(false, cmdParam1.ContainsUasgeParam());
            Assert.AreEqual(false, cmdParam2.ContainsUasgeParam());
        }
    }
}