using System.Diagnostics;

namespace HornClause
{
    /// <summary>
    /// A variable in a goal
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebugName) + "}")]
    public class Variable
    {
        /// <summary>
        /// Name of the variable, for debugging purposes
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Serial number for the next variable we create
        /// </summary>
        public static int SerialNumberCounter;

        /// <summary>
        /// Unique number attached to this variable so if there are two variables with the same name,
        /// you can tell them apart in the debugger.  This is added to the end of the name when it's
        /// printed so you can tell which variable you're seeing.
        /// </summary>
        public int SerialNumber = SerialNumberCounter++;

        /// <summary>
        /// Make a new variable
        /// </summary>
        /// <param name="name">Human-readable name of the variable</param>
        public Variable(string name)
        {
            Name = name;
        }

        /// <summary>
        /// True if the string looks like a variable name
        /// Used to identify variables in Rules
        /// </summary>
        public static bool IsVariableName(string name) => name.StartsWith("?");

        public override string ToString() => Name+SerialNumber;

        public string DebugName => ToString();
    }
}
