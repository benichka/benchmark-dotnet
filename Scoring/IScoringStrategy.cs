namespace Benchmark.Scoring
{
    /// <summary>
    /// The interface to a strategy used for benchmark scoring
    /// </summary>
    internal interface IScoringStrategy
    {
        /// <summary>
        /// Scores the specified value.
        /// </summary>
        /// <param name="values">The value.</param>
        /// <returns>A score</returns>
        long Score(long value);
    }
}
