namespace Benchmark.Scoring
{
    using System.Linq;

    /// <summary>
    /// A scoring strategy based on the sum of inputs
    /// </summary>
    /// <seealso cref="Benchmark.Scoring.IScoringStrategy" />
    internal class SumScoringStrategy : IScoringStrategy
    {
        /// <summary>
        /// Scores the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>
        /// A score
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public long Score(long[] values)
        {
            return values.Sum();
        }
    }
}
