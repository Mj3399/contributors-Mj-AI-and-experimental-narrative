using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Parser.Tests.Utilities;

namespace Parser.Tests
{
    /// <summary>
    /// Tests the utility functions
    /// You don't need to worry about these.
    /// </summary>
    [TestClass]
    public class UtilitiesTests
    {
        [TestMethod]
        public void ParseTreesEqualTest()
        {
            Assert.IsTrue(ParseTreesEqual("a", "a"));
            Assert.IsFalse(ParseTreesEqual("a", "b"));
            Assert.IsTrue(ParseTreesEqual(new [] { "a", "b"}, new [] { "a", "b" }));
            Assert.IsFalse(ParseTreesEqual(new[] { "a", "b" }, new[] { "a" }));
            Assert.IsFalse(ParseTreesEqual(new[] { "a", "b" }, new[] { "a", "c" }));
            Assert.IsFalse(ParseTreesEqual(new object [] { "a", "b" }, new object [] { "a", new [] { "c" } }));
            Assert.IsTrue(ParseTreesEqual(new object[] { "a", new [] { "c" } }, new object[] { "a", new[] { "c" } }));
        }

        [TestMethod]
        public void PrintTest()
        {
            Assert.AreEqual("a", ParseTreeToString("a"));
            Assert.AreEqual("[a]", ParseTreeToString(new [] { "a" }));
            Assert.AreEqual("[a b]", ParseTreeToString(new[] { "a", "b" }));
            Assert.AreEqual("[a b [c]]", ParseTreeToString(new object[] { "a", "b", new [] { "c" } }));
            Assert.AreEqual("[Det the]", ParseTreeToString(new object [] { Grammar.Det, "the" }));
        }
    }
}