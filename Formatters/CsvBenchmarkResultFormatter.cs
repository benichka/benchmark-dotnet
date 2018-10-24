namespace Benchmark.Formatters
{
    using System.Collections.Generic;
    using System.Linq;
    using Benchmark.Containers;
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
        public string Format(IEnumerable<BenchmarkResult> results)
        {
            HashSet<BenchmarkContainerType> containerTypes = new HashSet<BenchmarkContainerType>();
            HashSet<long> cardinalities = new HashSet<long>();
            HashSet<BenchmarkOperation> operations = new HashSet<BenchmarkOperation>();

            Dictionary<string, double> mappedResults = new Dictionary<string, double>();
            foreach (BenchmarkResult result in results)
            {
                if (!cardinalities.Contains(result.Cardinality)) {
                    cardinalities.Add(result.Cardinality);
                }

                if (!containerTypes.Contains(result.ContainerType))
                {
                    containerTypes.Add(result.ContainerType);
                }

                foreach (KeyValuePair<BenchmarkOperation, double> score in result.Scores)
                {
                    if (!operations.Contains(score.Key))
                    {
                        operations.Add(score.Key);
                    }

                    mappedResults.Add(CsvBenchmarkResultFormatter.GetResultKey(result.ContainerType, result.Cardinality, score.Key), score.Value);
                }
            }

            string output = string.Empty;
            IEnumerable<long> orderedCardinalities = cardinalities.OrderBy(c => c);
            IEnumerable<BenchmarkContainerType> orderedContainerTypes = containerTypes.OrderBy(c => c.ToString());
            IEnumerable<BenchmarkOperation> orderedOperations = operations.OrderBy(o => o.ToString());
            
            foreach (BenchmarkContainerType containerType in orderedContainerTypes)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += "\n";
                }

                output += CsvBenchmarkResultFormatter.GetHeader(containerType, orderedCardinalities);

                foreach (BenchmarkOperation operation in orderedOperations)
                {
                    string line = $"{operation}";

                    foreach (long cardinality in orderedCardinalities)
                    {
                        double score = mappedResults[CsvBenchmarkResultFormatter.GetResultKey(containerType, cardinality, operation)];
                        line = string.Join(',', score);
                    }

                    output += $"{line}\n";
                }
            }

            return output;
        }

        /// <summary>
        /// Gets the header string.
        /// </summary>
        /// <param name="cardinalities">The cardinalities.</param>
        /// <returns>A string</returns>
        private static string GetHeader(BenchmarkContainerType containerType, IEnumerable<long> cardinalities)
        {
            return $"{containerType} Scores\nOperation\\Cardinality," + string.Join(',', cardinalities.Select(c => c.ToString())) + "\n";
        }

        /// <summary>
        /// Gets the result key.
        /// </summary>
        /// <param name="containerType">Type of the container.</param>
        /// <param name="cardinality">The cardinality.</param>
        /// <returns>A string</returns>
        private static string GetResultKey(BenchmarkContainerType containerType, long cardinality, BenchmarkOperation operation)
        {
            return $"{containerType}\\{cardinality}\\{operation}";
        }
    }
}
