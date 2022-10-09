using System;
using System.Collections.Generic;

namespace Parser
{
    /// <summary>
    /// Category for a kind of phrase that has a set of "rules" for recognizing it
    /// The rules are represented as parsers and this parser succeeds if any of the
    /// rule parsers succeeds.
    /// </summary>
    public class PhrasalCategory : Category
    {
        /// <summary>
        /// Parsers that can recognize different versions of this phrase category
        /// </summary>
        private readonly List<Category> rules;

        /// <summary>
        /// Make a parser for a new kind of phrase
        /// </summary>
        /// <param name="name">Name of the category</param>
        /// <param name="rules">Parsers that can recognize instances of the phrase</param>
        public PhrasalCategory(string name, params Category[] rules) : base(name)
        {
            this.rules = new List<Category>(rules);
        }

        /// <summary>
        /// Add a rule of the form this category -> c
        /// </summary>
        /// <param name="c">Category that the new rule should expand this category into</param>
        public void AddRule(Category c) => rules.Add(c);

        /// <summary>
        /// Succeed if any of the parsers in rules can recognize the phrase
        /// </summary>
        /// <param name="w">Words to parse</param>
        /// <param name="k">Continuation to call upon success</param>
        /// <returns>True if both the match and the continuation were successful</returns>
        public override bool Parse(WordList w, Continuation k)
        {
            foreach (Category c in rules)
            {
                if (c.Parse(w, k))
                {
                    return true;
                }

            }
            return false;
        }
    }
}
