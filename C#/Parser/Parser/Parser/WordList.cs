namespace Parser
{
    /// <summary>
    /// A linked list of words (strings)
    /// </summary>
    public class WordList
    {
        /// <summary>
        /// The first word in the list
        /// </summary>
        public readonly string FirstWord;

        /// <summary>
        /// Remaining words in the list
        /// </summary>
        public readonly WordList Rest;

        /// <summary>
        /// Creates a WordList from a string, splitting the words at spaces
        /// </summary>
        public WordList(string words) : this(words.Split(), 0)
        { }
        
        private WordList(string[] words, int index)
        {
            FirstWord = words[index];
            Rest = index == words.Length - 1 ? null : new WordList(words, index + 1);
        }
    }
}
