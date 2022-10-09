using System.Collections.Generic;
using System.Diagnostics;

namespace HornClause
{
    /// <summary>
    /// Represents a predicate you can call as a goal.
    /// A predicate has a name, and a set of rules for when it can be true.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Predicate
    {
        /// <summary>
        /// Human-readable name for this predicate
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Make a new predicate
        /// </summary>
        /// <param name="name">Human-readable name of the predicate</param>
        public Predicate(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Rules that can be used to prove goals involving this predicate
        /// </summary>
        public readonly List<Rule> Rules = new();

        /// <summary>
        /// Add a new rule to the predicate
        /// </summary>
        /// <param name="r"></param>
        public void AddRule(Rule r) => Rules.Add(r);

        /// <summary>
        /// Make a Goal from this predicate with the specified argument value.
        /// </summary>
        public Goal this[object arg1] => new(this, arg1);

        /// <summary>
        /// Make a Goal from this predicate with the specified argument values.
        /// </summary>
        public Goal this[object arg1, object arg2] => new(this, arg1, arg2);

        /// <summary>
        /// Make a Goal from this predicate with the specified argument values.
        /// </summary>
        public Goal this[object arg1, object arg2, object arg3] => new(this, arg1, arg2, arg3);

        /// <summary>
        /// Make a Goal from this predicate with the specified argument values.
        /// </summary>
        public Goal this[object arg1, object arg2, object arg3, object arg4] => new(this, arg1, arg2, arg3, arg4);

        /// <summary>
        /// Make a Goal from this predicate with the specified argument values.
        /// </summary>
        public Goal this[object arg1, object arg2, object arg3, object arg4, object arg5] => new(this, arg1, arg2, arg3, arg4, arg5);
    }
}
