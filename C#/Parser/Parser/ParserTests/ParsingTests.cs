using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Parser.Tests.Utilities;
using static Parser.Grammar;

namespace Parser.Tests
{
    /// <summary>
    /// Tests of the parser using the parsers defined in the Grammar class
    /// </summary>
    [TestClass]
    public class ParsingTests
    {
        [TestMethod]
        public void NounTests()
        {
            AssertParse(N, "cat", "cat");
            AssertParse(N, "knife", null);
            AssertParse(N, "cat other words", null);
        }

        [TestMethod]
        public void SequenceTest()
        {
            var seq = new Sequence("Test", Det, N);
            AssertParse(seq, "the cat", new [] { "Test", "the", "cat"});
            AssertParse(seq, "the", null);
            AssertParse(seq, "cat the", null);
            AssertParse(seq, "the cat the", null);
            AssertParse(seq, "the the cat", null);
        }

        [TestMethod]
        public void NPTests()
        {
            AssertParse(NP, "the cat", new object[] { NP, "the", "cat" });
            AssertParse(NP, "the cat eats", null);
            AssertParse(NP, "the", null);
            AssertParse(NP, "cat", null);
            AssertParse(NP, "I", "I");
        }

        [TestMethod]
        public void IntransitiveVerbTest()
        {
            AssertParse(S, "I stopped",
                new object[]
                {
                    "DeclarativeSentence",
                    "I",
                    new object[]
                    {
                        VP, "stopped",
                        new [] { "OptPP" }
                    }
                });
        }

        [TestMethod]
        public void TransitiveVerbTest()
        {
            AssertParse(S, "I gave blood", 
                new object[]
                {
                    "DeclarativeSentence", 
                    "I",
                    new object[]
                    {
                        VP, "gave", "blood", new [] { "OptPP" }
                    }
                });
        }

        [TestMethod]
        public void DiTransitiveVerbTest()
        {
            AssertParse(S, "I gave you blood",
                new object[]
                {
                    "DeclarativeSentence",
                    "I",
                    new object[]
                    {
                        VP, "gave", "you", "blood",
                        new [] { "OptPP" }
                    }
                });
        }

        [TestMethod]
        public void PPTest()
        {
            AssertParse(S, "I gave you blood for your birthday",
                new object[]
                {
                    "DeclarativeSentence",
                    "I",
                    new object[]
                    {
                        VP, "gave", "you", "blood",
                        new object[] { "OptPP", 
                            new object[] {
                                "PP",
                                "for",
                            new [] { "NP", "your", "birthday" }
                            },
                            new [] { "OptPP" }
                        }
                    }
                });
        }

        [TestMethod]
        public void PPTest2()
        {
            AssertParse(S, "the cat ate the cake with a fork for her birthday",
                new object[]
                {
                    "DeclarativeSentence",
                    new [] { "NP", "the", "cat" },
                    new object[]
                    {
                        VP, "ate",
                        new [] { "NP", "the", "cake" },
                        new object[] { "OptPP",
                            new object[] {
                                "PP",
                                "with",
                                new [] { "NP", "a", "fork" }
                            },
                            new object[] { "OptPP",
                                new object[] {
                                    "PP",
                                    "for",
                                    new [] { "NP", "her", "birthday" }
                                },
                                new [] { "OptPP" }
                            }
                        }
                    }
                });
        }

        [TestMethod]
        public void WhSubjectTest()
        {
            AssertParse(S, "who ate the apple",
                new object[]
                {
                    "WhQuestionSubject",
                    "who",
                    new object[] {
                        "VP",
                        "ate",
                        new object[] { NP, "the", "apple" },
                        new [] { "OptPP" }
                    }
                });
        }

        [TestMethod]
        public void WhObjectTest()
        {
            AssertParse(S, "what did the cat eat",
                new object[]
                {
                    "WhQuestionObject",
                    "what",
                    "did",
                    new object[] { NP, "the", "cat" },
                    "eat"
                });
        }

        [TestMethod]
        public void PropositionalAttitudeTest()
        {
            AssertParse(S, "I believe I gave you blood for your birthday",
                new object[]
                {
                    "DeclarativeSentence",
                    "I",
                    new object[] {
                        "VP",
                        "believe",
                        new object[]
                        {
                            "DeclarativeSentence",
                            "I",
                            new object[]
                            {
                                VP, "gave", "you", "blood",
                                new object[] { "OptPP",
                                    new object[] {
                                        "PP",
                                        "for",
                                        new [] { "NP", "your", "birthday" }
                                    },
                                    new [] { "OptPP" }
                                }
                            }
                        }
                    }
                });
        }
    }
}
