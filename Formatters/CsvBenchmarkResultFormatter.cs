namespace Benchmark.Formatters
{
    using System.Collections.Generic;
    using Benchmark.Core;

    /// <summary>
    /// Formats benchmark results into a CSV
    /// </summary>
    /// <seealso cref="Benchmark.Formatters.IBenchmarkResultFormatter" />
    internal class CsvBenchmarkResultFormatter : IBenchmarkResultFormatter
    {
        /// <summary>
        /// Formats the specified results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <returns>
        /// A formatted string representation
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string Format(IEnumerable<BenchmarkResult> results)
        {
            throw new System.NotImplementedException();
        }
    }
}
