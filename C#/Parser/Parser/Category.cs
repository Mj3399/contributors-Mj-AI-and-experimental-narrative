using System.Diagnostics;

namespace Parser
{
    /// <summary>
    /// Represents a kind of phrase or word in the grammar
    /// </summary>
    [DebuggerDisplay("Category {Name}")]
    public abstract class Category
    {
        /// <summary>
        /// Name of the category, for testing and debugging purposes
        /// </summary>
        public readonly string Name;

        protected Category(string name)
        {
            Name = name;
        }

        /// <summary>
        /// A continuation used by the parser: takes a WordList as an argument and returns true if successful, else false.
        /// </summary>
        /// <param name="parse">Result of the parse of this category</param>
        /// <param name="words">Remaining words not matched by this category</param>
        /// <returns>True if parse was successful</returns>
        public delegate bool Continuation(object parse, WordList words);

        /// <summary>
        /// Try to parse the WordList.  If successful, call continuation with the result of the
        /// parse, and any remaining words left over afterward.  If the continuation is also
        /// successful, then return true.  Return false if the parse fails.
        /// NOTE: since categories can match a given string of words in multiple ways, the
        /// continuation can be called more than once for a given call of Parse.
        /// </summary>
        /// <param name="w">Words to parse</param>
        /// <param name="k">Continuation to call for each successful match</param>
        /// <returns>True upon a successful parse.</returns>
        public abstract bool Parse(WordList w, Continuation k);

        /// <summary>
        /// Try to parse the specified text as an instance of this category
        /// </summary>
        /// <param name="text">Text to parse</param>
        /// <returns>Output of parser (the parse tree) or null, if the parse failed.</returns>
        public object Parse(string text)
        {
            object result = null;
            Parse(new WordList(text), (parse, words) =>
            {
                if (words == null)
                {
                    result = parse;
                    return true;
                }
                else return false;
            });
            return result;
        }

        public override string ToString() => Name;
    }
}
