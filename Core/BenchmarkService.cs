namespace Benchmark.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Benchmark.Containers;
    using Benchmark.Scoring;

    /// <summary>
    /// A service that runs performance benchmarks
    /// </summary>
    internal sealed class BenchmarkService
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly BenchmarkServiceConfiguration configuration;

        /// <summary>
        /// The data generator
        /// </summary>
        private readonly Random generator;

        /// <summary>
        /// The data
        /// </summary>
        private readonly int[] data;

        /// <summary>
        /// The scoring strategy
        /// </summary>
        private readonly BenchmarkRunner runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkService" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="scoringStrategy">The scoring strategy.</param>
        private BenchmarkService(BenchmarkServiceConfiguration configuration, IScoringStrategy scoringStrategy)
        {
            this.configuration = configuration;
            this.generator = new Random(this.configuration.Seed);
            this.data = this.GetData(this.configuration.Cardinalities.Max());
            this.runner = BenchmarkRunner.Create(configuration, scoringStrategy);

        }

        /// <summary>
        /// Creates a benchmark service.
        /// </summary>
        /// <returns>A benchmark service</returns>
        public static BenchmarkService Create(BenchmarkServiceConfiguration configuration)
        {
            return new BenchmarkService(configuration, new SimpleScoringStrategy());
        }

        /// <summary>
        /// Called when starting.
        /// </summary>
        /// <returns>
        /// An asynchronous task
        /// </returns>
        public IEnumerable<BenchmarkResult> Run()
        {
            List<BenchmarkResult> results = new List<BenchmarkResult>();

            foreach (BenchmarkContainerType containerType in this.configuration.ContainerTypes)
            {
                foreach (long cardinality in this.configuration.Cardinalities)
                {
                    int[] containerData = Utilities.GetSlice(this.data, cardinality);

                    IBenchmarkContainer<int> container = BenchmarkContainerFactory<int>.CreateBenchmarkContainer(containerType, containerData);
                    BenchmarkResult result = this.runner.GetResult(container);
                    results.Add(result);
                }
            }

            return results;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>An array of data points</returns>
        private int[] GetData(long count)
        {
            int[] randoms = new int[count];
            long index = 0;

            while (index < randoms.Length)
            {
                randoms[index] = this.generator.Next();
                index++;
            }

            return randoms;
        }
    }
}
