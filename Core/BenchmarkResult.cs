namespace Benchmark.Core
{
    using System;
    using System.Collections.Generic;
    using Benchmark.Containers;

    /// <summary>
    /// The result of a benchmark
    /// </summary>
    internal sealed class BenchmarkResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkResult" /> class.
        /// </summary>
        public BenchmarkResult(BenchmarkContainerType containerType, long cardinality)
        {
            this.ContainerType = containerType;
            this.Cardinality = cardinality;
            this.Scores = new Dictionary<BenchmarkOperation, double>();
        }

        /// <summary>
        /// Gets the type of the container.
        /// </summary>
        /// <value>
        /// The type of the container.
        /// </value>
        public BenchmarkContainerType ContainerType { get; }

        /// <summary>
        /// Gets the cardinality.
        /// </summary>
        /// <value>
        /// The cardinality.
        /// </value>
        public long Cardinality { get; }

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <value>
        /// The scores.
        /// </value>
        public Dictionary<BenchmarkOperation, double> Scores { get; }

        /// <summary>
        /// Records a score for the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="score">The score.</param>
        /// <exception cref="System.ArgumentException">Only one score can be recorded per operation</exception>
        public void Record(BenchmarkOperation operation, double score)
        {
            if (!this.Scores.TryAdd(operation, score))
            {
                throw new ArgumentException("Only one score can be recorded per operation");
            };
        }
    }
}
