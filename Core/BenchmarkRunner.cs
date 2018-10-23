namespace Benchmark.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Benchmark.Containers;
    using Benchmark.Scoring;

    /// <summary>
    /// Runs a single benchmark instance
    /// </summary>
    internal sealed class BenchmarkRunner
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly BenchmarkServiceConfiguration configuration;

        /// <summary>
        /// The scoring strategy
        /// </summary>
        private readonly IScoringStrategy scoringStrategy;

        /// <summary>
        /// The stop watch
        /// </summary>
        private readonly Stopwatch stopWatch;

        /// <summary>
        /// The test values
        /// </summary>
        private readonly int[] testValues;

        private BenchmarkRunner(BenchmarkServiceConfiguration configuration, IScoringStrategy scoringStrategy)
        {
            this.configuration = configuration;
            this.scoringStrategy = scoringStrategy;
            this.testValues = BenchmarkRunner.GetTestValues(new Random(this.configuration.Seed), this.configuration.Operations.Sum(kvp => kvp.Value));
            this.stopWatch = new Stopwatch();
        }

        /// <summary>
        /// Creates the specified benchmark runner.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="scoringStrategy">The scoring strategy.</param>
        /// <returns>
        /// A benchmark runner
        /// </returns>
        public static BenchmarkRunner Create(BenchmarkServiceConfiguration configuration, IScoringStrategy scoringStrategy)
        {
            return new BenchmarkRunner(configuration, scoringStrategy);
        }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>A benchmark result</returns>
        public BenchmarkResult GetResult(IBenchmarkContainer<int> container)
        {
            BenchmarkResult result = new BenchmarkResult(container.Type, container.Cardinality);

            int operationIndex = 0;
            foreach (KeyValuePair<BenchmarkOperation, int> kvp in this.configuration.Operations)
            {
                long[] runtimes = new long[kvp.Value];
                for (int iterationIndex = 0; iterationIndex < kvp.Value; iterationIndex++)
                {
                    runtimes[iterationIndex] = this.Run(container, kvp.Key, this.testValues[operationIndex + iterationIndex]);
                }

                result.Record(kvp.Key, this.scoringStrategy.Score(runtimes));
                operationIndex++;
            }

            return result;
        }

        /// <summary>
        /// Runs the specified operation and returns the duration of time taken.
        /// </summary>
        /// <param name="container">The container to benchmark.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// A duration
        /// </returns>
        /// <exception cref="System.ArgumentException">Operation not supported - operation</exception>
        private long Run(IBenchmarkContainer<int> container, BenchmarkOperation operation, int tuple)
        {
            Action method;

            switch (operation)
            {
                case BenchmarkOperation.Delete:
                    method = () => container.Delete(tuple);
                    break;
                case BenchmarkOperation.Find:
                    method = () => container.Find(tuple);
                    break;
                case BenchmarkOperation.Insert:
                    method = () => container.Insert(tuple);
                    break;
                case BenchmarkOperation.Iterate:
                    method = () => container.Iterate();
                    break;
                default:
                    throw new ArgumentException("Operation not supported", nameof(operation));
            }

            this.stopWatch.Restart();
            method();
            this.stopWatch.Stop();

            return this.stopWatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Gets the test values.
        /// </summary>
        /// <param name="random">The random generator.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        /// An array of test values
        /// </returns>
        private static int[] GetTestValues(Random random, int count)
        {
            int[] values = new int[count];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = random.Next();
            }

            return values;
        }
    }
}
