using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Parser.Tests
{
    /// <summary>
    /// Useful procedures for testing parsers
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Prints the output of a parser in something approximating the bracketed expressions
        /// used in linguistics
        /// </summary>
        /// <param name="tree">The tree, expressed as nested arrays</param>
        /// <param name="w">The stream to print to</param>
        public static void PrintParseTree(object tree, TextWriter w)
        {
            if (tree is IList l)
            {
                bool firstOne = true;
                w.Write('[');
                foreach (var item in l)
                {
                    if (firstOne)
                        firstOne = false;
                    else
                        w.Write(' ');
                    PrintParseTree(item, w);
                }
                w.Write(']');
            }
            else 
                w.Write(tree);
        }
        
        /// <summary>
        /// Print a parse tree to a human-readable string
        /// </summary>
        /// <param name="tree">Parse to print</param>
        /// <returns>Tree in bracket notation</returns>
        public static string ParseTreeToString(object tree)
        {
            using (var w = new StringWriter())
            {
                PrintParseTree(tree, w);
                return w.ToString();
            }
        }

        /// <summary>
        /// True if two parse trees are the same
        /// This just recurses to make sure all the constituents are the same
        /// Also replaces parsers with their names, so the parser NP and the string "NP"
        /// are allowed to match.  That just makes typing test cases easier.
        /// </summary>
        /// <param name="a">Parse tree</param>
        /// <param name="b">Other parse tree</param>
        /// <returns>True if they're equivalent</returns>
        public static bool ParseTreesEqual(object a, object b)
        {
            if (a == null || b == null)
                return ReferenceEquals(a, b);

            if (a is Category ap)
                a = ap.Name;

            if (b is Category bp)
                b = bp.Name;

            if (a.Equals(b))
                return true;
            if (a is IList aList && b is IList bList && aList.Count == bList.Count)
            {
                for (int i = 0; i < aList.Count; i++)
                    if (!ParseTreesEqual(aList[i], bList[i]))
                        return false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Test that a parser generates a specific parse.
        /// Equivalent to Assert.IsTrue(ParseTreesEqual(...)), but generates better
        /// error messages.
        /// </summary>
        /// <param name="p">Category to run, e.g. Grammar.S or Grammar.NP</param>
        /// <param name="text">Text to parse</param>
        /// <param name="expected">Expected parser tree</param>
        [DebuggerHidden]
        public static void AssertParse(Category p, string text, object expected)
        {
            var actual = p.Parse(text);
            if (ParseTreesEqual(expected, actual))
            {
                // Test succeeded
                Assert.IsTrue(true); // Make the test succeed
                return;
            }

            // Test failed
            string error;

            if (actual == null)
                error = $"Parse failed for \"{text}\" using parser {p}";
            else if (expected == null)
                error = $"Test failed for parse of \"{text}\" using parser {p}\nExpected failure, but parse succeeded with output:\n{ParseTreeToString(actual)}";
            else
                error = $"Test failed for parse of \"{text}\" using parser {p}\nExpected:\n{ParseTreeToString(expected)}\nActual value:\n{ParseTreeToString(actual)}";

            Assert.Fail(error);
        }
    }
}
