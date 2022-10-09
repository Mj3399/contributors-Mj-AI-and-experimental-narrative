using System;

namespace Parser
{
    /// <summary>
    /// A parser that matches a sequence of categories
    /// For example, NP VP will match a string that starts with an NP and ends with a VP.
    /// </summary>
    public class Sequence : Category
    {
        /// <summary>
        /// Sequence of constituents (lexical and/or phrasal categories) to match
        /// If constituents are NP, VP then we match strings that start with an NP and end with a VP
        /// </summary>
        private readonly Category[] constituents;

        /// <summary>
        /// Make a parser that recognizes sequences of the specified constituents
        /// </summary>
        /// <param name="name">Name for this sequence</param>
        /// <param name="constituents">Sequence of constituents to match</param>
        public Sequence(string name, params Category[] constituents) : base(name)
        {
            this.constituents = constituents;
        }

        /// <summary>
        /// Match WordList to our sequence of constituents, then call continuation with array of parses from the constituents and the remaining words
        /// </summary>
        /// <param name="w">Words to parse</param>
        /// <param name="k">Continuation to call if successful</param>
        /// <returns>Result of k, or false if can't match</returns>
        public override bool Parse(WordList w, Continuation k)
        {
            object[] answer = new object[constituents.Length + 1];
            answer[0] = this;
            return helpme(w, k, 0, answer);

        }
        public bool helpme(WordList w, Continuation k, int i, object[] answer)
        {
            if (i > constituents.Length - 1)
            {
                return k(answer, w);
            }

            return constituents[i].Parse(w, (first, rest) =>
            {
                answer[i + 1] = first;
                return helpme(rest, k, i + 1, answer);
            }
            );



        }

    }
}
