using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HornClause
{
    /// <summary>
    /// A Goal represents a predicate applied to arguments, e.g. p["a"], p[variable], etc.
    /// Goals are used as the arguments to Prover.Prove, but also as the Head and Body of Rules.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebugName) + "}")]
    public class Goal
    {
        /// <summary>
        /// Predicate being called
        /// </summary>
        public readonly Predicate Predicate;
        
        /// <summary>
        /// Arguments to the predicate
        /// </summary>
        public readonly object[] Arguments;

        /// <summary>
        /// Make a new goal object
        /// </summary>
        /// <param name="predicate">Predicate being called</param>
        /// <param name="arguments">Arguments to the predicate</param>
        public Goal(Predicate predicate, params object[] arguments)
        {
            Predicate = predicate;
            Arguments = arguments;
        }

        /// <summary>
        /// Add this goal as a rule in the predicate's list of rules, but without any subgoals.
        /// The Head of the rule will be this Goal, and the Body will be empty.
        /// </summary>
        public void Fact() => Predicate.AddRule(new Rule(this));

        /// <summary>
        /// Add this goal as a rule in the predicate's list of rules, with the specified subgoals in its body.
        /// So the Head of the rule will be this Goal and the Body will be the subgoals
        /// </summary>
        /// <param name="subgoals">Subgoals to include in the goal's body.</param>
        public void If(params Goal[] subgoals) => Predicate.AddRule(new Rule(this, subgoals));

        /// <summary>
        /// Make a copy of this rule, replacing any arguments that appear in the hashtable with their values in the hash table.
        /// This is only called from Rule.Copy().
        /// </summary>
        /// <param name="vars">Mapping used to selectively rewrite arguments</param>
        /// <returns></returns>
        public Goal Copy(Hashtable vars)
            => new(Predicate, Arguments.Select(arg => vars[arg] ?? arg).ToArray());

        /// <summary>
        /// All the strings in this goal's arguments that look like variable names, i.e. start with ?
        /// </summary>
        public IEnumerable<string> VariableNames => Arguments.Where(arg => arg is string s && Variable.IsVariableName(s)).Cast<string>();

        #region Printing
        /// <summary>
        /// Convert the goal to a human-readable string, for purposes of printing.
        /// </summary>
        public override string ToString()
        {
            var b = new StringBuilder();
            ToString(b);
            return b.ToString();
        }
        
        /// <summary>
        /// Add the printed representation of the goal to this StringBuilder.
        /// </summary>
        public void ToString(StringBuilder b)
        {
            b.Append(Predicate.Name);
            b.Append('(');
            var first = true;
            foreach (var arg in Arguments)
            {
                if (first)
                    first = false;
                else
                    b.Append(", ");

                b.Append(arg);
            }

            b.Append(')');
        }

        /// <summary>
        /// This is just so that this appears in human-readable form in the debugger
        /// </summary>
        public string DebugName => ToString();

        #endregion
    }
}
