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
                Action<int> action = this.GetAction(container, kvp.Key);

                this.stopWatch.Restart();
                for (int iterationIndex = 0; iterationIndex < kvp.Value; iterationIndex++)
                {
                    action(this.testValues[operationIndex + iterationIndex]);
                }
                this.stopWatch.Stop();

                result.Record(kvp.Key, this.scoringStrategy.Score(this.stopWatch.ElapsedMilliseconds));
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
        private Action<T> GetAction<T>(IBenchmarkContainer<T> container, BenchmarkOperation operation)
        {
            Action<T> method;

            switch (operation)
            {
                case BenchmarkOperation.Delete:
                    method = (t) => container.Delete(t);
                    break;
                case BenchmarkOperation.Find:
                    method = (t) => container.Find(t);
                    break;
                case BenchmarkOperation.Insert:
                    method = (t) => container.Insert(t);
                    break;
                case BenchmarkOperation.Iterate:
                    method = (t) => container.Iterate();
                    break;
                default:
                    throw new ArgumentException("Operation not supported", nameof(operation));
            }

            return method;
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
