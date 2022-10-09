using System;
using System.Collections.Generic;
using System.Linq;

namespace HornClause
{
    /// <summary>
    /// Implements the algorithm for proving Goals.
    /// </summary>
    public static class Prover
    {
        /// <summary>
        /// A continuation to call when a goal or subgoal is successfully proven.
        /// Since proving can fill in values for variables, the prover will pass
        /// the continuation the final Substitution used in the proof.
        /// </summary>
        /// <param name="newSubstitution"></param>
        /// <returns>True if the continuation accepted the final Substitution.  If it's false, then the prover should try to backtrack.</returns>
        public delegate bool SuccessContinuation(Substitution newSubstitution);

        /// <summary>
        /// Try to prove goal using a substitution
        /// </summary>
        /// <param name="g">Goal to prove</param>
        /// <param name="s">Variable substitutions in effect</param>
        /// <param name="k">Success continuation to call with final substitutions</param>
        /// <returns>True if successful and continuation returned true</returns>
        public static bool Prove(Goal g, Substitution s, SuccessContinuation k)
        {
            // G is true if it can be proven by any rule
            // Don't forget to copy the rule before trying to prove it
            foreach(Rule r in g.Predicate.Rules)
            {
                
                if(ProveUsingRule(g, r.Copy(), s, k))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Try to prove goal using the specified rule
        /// This will work if the goal can be unified with the head of the rule and all the subgoals can
        /// also be proven.
        /// </summary>
        /// <param name="g">Goal to prove</param>
        /// <param name="r">Rule to try to use to prove it.</param>
        /// <param name="s">Substitutions in effect</param>
        /// <param name="k">Success continuation to call with final substitutions</param>
        /// <returns>Successful and continuation returned true</returns>
        public static bool ProveUsingRule(Goal g, Rule r, Substitution s, SuccessContinuation k)
        {
            if(Unifier.UnifyArrays(g.Arguments, r.Head.Arguments, s, out Substitution uS) && subgoal(0, r, uS, k))
            {
                return true;
            }
            return false;
        }
        public static bool subgoal(int i, Rule r, Substitution s, SuccessContinuation k)
        {
            if (i < r.Body.Length)
            {
                return Prove(r.Body[i], s, uS => subgoal(i+1, r, uS, k));
               
            }
            else
            {
                return k(s);
            }
        }
        #region Test harness

        /// <summary>
        /// Try to prove goal.  Return true if successful, otherwise false
        /// </summary>
        /// <param name="g">goal to prove</param>
        /// <returns>True if it was successful</returns>
        public static bool CanProve(Goal g) => Prove(g, null, _ => true);

        /// <summary>
        /// Try to prove goal.  If successful, return the final value of the variable.  If not successful, throw an exception.
        /// </summary>
        /// <param name="v">Variable to find the value of</param>
        /// <param name="g">Goal to try to prove; it should include the variable as one of its arguments.</param>
        /// <returns>Final value of the variable</returns>
        public static object SolveFor(Variable v, Goal g)
        {
            object result = null;
            if (!Prove(g, null,
                b =>
                {
                    // Remember solution
                    result = Unifier.Dereference(v, b);
                    return true;
                }))
                throw new Exception("Can't prove goal");
            return result;
        }

        /// <summary>
        /// Find *all* the solutions to the goal and return the list of values of the variable in each one.
        /// </summary>
        /// <param name="v">Variable to find the value of</param>
        /// <param name="g">Goal to prove.  It should include the variable as an argument</param>
        /// <returns>List of all values of the variable for all solutions.  If there are no solutions, the list will be empty.</returns>
        public static List<object> SolveForAll(Variable v, Goal g)
        {
            var result = new List<object>();
            Prove(g, null,
                b =>
                {
                    // Remember this solution
                    result.Add(Unifier.Dereference(v, b));
                    // Force backtracking
                    return false;
                });
            return result;
        }

        #endregion
    }
}
