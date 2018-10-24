namespace Benchmark.Scoring
{ 
    /// <summary>
    /// A simple scoring strategy
    /// </summary>
    /// <seealso cref="Benchmark.Scoring.IScoringStrategy" />
    internal class SimpleScoringStrategy : IScoringStrategy
    {
        /// <summary>
        /// Scores the specified value.
        /// </summary>
        /// <param name="values">The value.</param>
        /// <returns>
        /// A score
        /// </returns>
        public long Score(long value)
        {
            return value;
        }
    }
}
