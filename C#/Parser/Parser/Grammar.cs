namespace Parser
{
    /// <summary>
    /// Defines the grammatical categories for a very simple grammar for English
    /// This doesn't even come close to being a useful grammar, but it's enough to test your parser.
    /// </summary>
    public static class Grammar
    {
        // Lexical categories, i.e. kinds of words
        // Note that these overlap: gave can be used as a transitive (I gave blood),
        // ditransitive (I gave you blood), or intransitive (I gave) verb.

        // Many kinds of nouns
        public static readonly Category N = new LexicalCategory("CommonNoun", "cat", "dog", "apple", "birthday", "fork", "cake");
        public static readonly Category MassNoun = new LexicalCategory("MassNoun", "blood", "water", "rice");
        public static readonly Category ProperNoun = new LexicalCategory("ProperNoun", "alisha", "jane", "tyrone");
        public static readonly Category Pronoun = new LexicalCategory("Pronoun", "I", "me", "you", "us", "he", "she", "him", "her", "they");

        public static readonly Category WhWord = new LexicalCategory("WhWord", "who", "what", "where");

        // Determiners, sometimes known as articles
        public static readonly Category Det = new LexicalCategory("Det", "the", "a", "an", "your", "her");

        // Many kinds of verbs
        public static readonly Category DoSupport = new LexicalCategory("DoSupport", "do", "does", "did");

        public static readonly Category IntransitiveVerb = new LexicalCategory("IntransitiveVerb", "stopped", "gave");
        public static readonly Category TransitiveVerb = new LexicalCategory("TransitiveVerb", "eat", "ate", "drank", "gave");
        public static readonly Category DitransitiveVerb = new LexicalCategory("DitransitiveVerb", "gave");

        public static readonly Category PropositionalAttitudeVerb
            = new LexicalCategory("PropositionalAttitudeVerb", "believe", "think", "doubt");

        public static readonly Category Preposition
            = new LexicalCategory("Preposition", "from", "to", "for", "by", "with");

        // Phrasal categories, i.e. kinds of phrases

        // Noun phrase
        public static readonly Category NP = new PhrasalCategory("NP",
            new Sequence("NP", Det, N),
            Pronoun,
            MassNoun,
            ProperNoun);

        public static readonly Category PP = new PhrasalCategory("PP",
            new Sequence("PP", Preposition, NP));

        public static readonly PhrasalCategory OptPP = new PhrasalCategory("OptPP",
            new Sequence("OptPP"));
        
        // Verb phrase
        public static readonly PhrasalCategory VP = new PhrasalCategory("VP",
            new Sequence("VP", IntransitiveVerb, OptPP),
            new Sequence("VP", TransitiveVerb, NP, OptPP),
            new Sequence("VP", DitransitiveVerb, NP, NP, OptPP));

        // Sentence
        public static readonly Category S = new PhrasalCategory("S",
            new Sequence("DeclarativeSentence", NP, VP),
            new Sequence("WhQuestionSubject", WhWord, VP),
            new Sequence("WhQuestionObject", WhWord, DoSupport, NP, TransitiveVerb),
            new Sequence("YesNoQuestion", DoSupport, NP, VP));

        static Grammar()
        {
            VP.AddRule(new Sequence("VP", PropositionalAttitudeVerb, S));
            OptPP.AddRule(new Sequence("OptPP", PP, OptPP));
        }
    }
}
