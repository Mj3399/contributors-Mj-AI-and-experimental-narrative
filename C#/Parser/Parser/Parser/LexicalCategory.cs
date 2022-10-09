using System;
using System.Collections.Generic;

namespace Parser
{
    /// <summary>
    /// A parse that matches single words from a given list of words
    /// </summary>
    public class LexicalCategory : Category
    {
        /// <summary>
        /// Words we can recognize
        /// </summary>
        private readonly HashSet<string> words;

        /// <summary>
        /// Make a new parser that recognizes the specified set of words
        /// </summary>
        /// <param name="name">Name of this category</param>
        /// <param name="words">Words to recognize</param>
        public LexicalCategory(string name, params string[] words) : base(name)
        {
            this.words = new HashSet<string>(words);
        }

        /// <summary>
        /// Check that the first word in the word list is one of our words
        /// If so, call continuation with the word and the rest of the words,
        /// otherwise, fail.
        /// </summary>
        /// <param name="w">Remaining words to parse</param>
        /// <param name="k">Continuation</param>
        /// <returns>True if both the match and the continuation were successful, fails upon failureS</returns>
        public override bool Parse(WordList w, Continuation k)
        {
            if (w != null && words.Contains(w.FirstWord))
            {
                return k(w.FirstWord, w.Rest);
            }
            return false;
        }
    }
}
