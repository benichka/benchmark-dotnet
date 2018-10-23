namespace Benchmark.Scoring
{
    /// <summary>
    /// The interface to a strategy used for benchmark scoring
    /// </summary>
    internal interface IScoringStrategy
    {
        /// <summary>
        /// Scores the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>A score</returns>
        long Score(long[] values);
    }
}
