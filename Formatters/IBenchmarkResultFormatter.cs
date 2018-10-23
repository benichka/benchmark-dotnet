namespace Benchmark.Formatters
{
    using System.Collections.Generic;
    using Benchmark.Core;

    /// <summary>
    /// The interface for a benchmark result formatter
    /// </summary>
    internal interface IBenchmarkResultFormatter
    {
        /// <summary>
        /// Formats the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>A formatted string representation</returns>
        string Format(IEnumerable<BenchmarkResult> results);
    }
}
