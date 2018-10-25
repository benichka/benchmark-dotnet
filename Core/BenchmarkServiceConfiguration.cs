namespace Benchmark.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Benchmark.Containers;
    using Benchmark.Exceptions;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The configuration for the benchmark service
    /// </summary>
    internal sealed class BenchmarkServiceConfiguration
    {
        /// <summary>
        /// The minimum cardinality power
        /// </summary>
        private const int MinCardinalityPower = 0;

        /// <summary>
        /// The maximum cardinality power
        /// </summary>
        private const int MaxCardinalityPower = 31;

        /// <summary>
        /// The default configuration file name
        /// </summary>
        private const string DefaultConfigurationFileName = "appsettings.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkServiceConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        private BenchmarkServiceConfiguration(IConfiguration configuration)
        {
            BenchmarkSettingsSection benchmarkSettings = configuration.GetSection(BenchmarkSettingsSection.SectionName)
                .Get<BenchmarkSettingsSection>();

            BenchmarkServiceConfiguration.Validate(benchmarkSettings);

            this.Cardinalities = benchmarkSettings.CardinalityPowers.Select(BenchmarkServiceConfiguration.GetCardinalityFromPower);
            this.ContainerTypes = benchmarkSettings.ContainerTypes;
            this.Seed = benchmarkSettings.Seed;
            this.Operations = benchmarkSettings.Operations;
        }

        /// <summary>
        /// Gets the cardinalities.
        /// </summary>
        /// <value>
        /// The cardinalities.
        /// </value>
        public IEnumerable<long> Cardinalities { get; }

        /// <summary>
        /// Gets the container types.
        /// </summary>
        /// <value>
        /// The container types.
        /// </value>
        public IEnumerable<BenchmarkContainerType> ContainerTypes { get; }

        /// <summary>
        /// The operations to run and their counts
        /// </summary>
        public Dictionary<BenchmarkOperation, int> Operations;

        /// <summary>
        /// Gets the test iteration count scalar.
        /// </summary>
        /// <value>
        /// The test iteration count scalar.
        /// </value>
        public int TestIterationCountScalar
        {
            get { return 100; }
        }

        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        public int Seed { get; }

        /// <summary>
        /// Loads a new configuration.
        /// </summary>
        /// <returns>A configuration</returns>
        public static BenchmarkServiceConfiguration Load()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile(BenchmarkServiceConfiguration.DefaultConfigurationFileName).Build();
            return new BenchmarkServiceConfiguration(config);
        }

        /// <summary>
        /// Gets the cardinality from power.
        /// </summary>
        /// <param name="power">The power.</param>
        /// <returns></returns>
        private static long GetCardinalityFromPower(int power)
        {
            return Convert.ToInt64(Math.Pow(2, power));
        }

        /// <summary>
        /// Validates the specified benchmark settings.
        /// </summary>
        /// <param name="benchmarkSettings">The benchmark settings.</param>
        /// <exception cref="ConfigurationException">Configuration not valid</exception>
        private static void Validate(BenchmarkSettingsSection benchmarkSettings)
        {
            if (benchmarkSettings.CardinalityPowers == null || benchmarkSettings.CardinalityPowers.Length == 0)
            {
                throw new ConfigurationException($"'{nameof(benchmarkSettings.CardinalityPowers)}' be a non-empty array of integers");
            }

            if (benchmarkSettings.CardinalityPowers.Any(v => v < BenchmarkServiceConfiguration.MinCardinalityPower || v > BenchmarkServiceConfiguration.MaxCardinalityPower))
            {
                throw new ConfigurationException($"'{nameof(benchmarkSettings.CardinalityPowers)}' must not contain any values less than {BenchmarkServiceConfiguration.MinCardinalityPower} or greater than {BenchmarkServiceConfiguration.MaxCardinalityPower}");
            }

            if (benchmarkSettings.ContainerTypes == null || benchmarkSettings.ContainerTypes.Length == 0)
            {
                throw new ConfigurationException($"'{nameof(benchmarkSettings.ContainerTypes)}' must be a non-empty array of '{nameof(BenchmarkContainerType)}'");
            }

            if (benchmarkSettings.Operations == null || benchmarkSettings.Operations.Count == 0)
            {
                throw new ConfigurationException($"'{nameof(benchmarkSettings.Operations)}' must be a non-empty map of '{nameof(BenchmarkOperation)}:int'");
            }

            if (benchmarkSettings.Operations.Values.Any(v => v < 0))
            {
                throw new ConfigurationException($"'{nameof(benchmarkSettings.Operations)}' must not contain any values less than 0");
            }
        }

        /// <summary>
        /// The benchmark settings section
        /// </summary>
        private class BenchmarkSettingsSection
        {
            /// <summary>
            /// The section name
            /// </summary>
            public const string SectionName = "BenchmarkSettings";

            /// <summary>
            /// Gets or sets the cardinality powers.
            /// </summary>
            /// <value>
            /// The cardinality powers.
            /// </value>
            public int[] CardinalityPowers { get; set; }

            /// <summary>
            /// Gets or sets the classes.
            /// </summary>
            /// <value>
            /// The classes.
            /// </value>
            public BenchmarkContainerType[] ContainerTypes { get; set; }

            /// <summary>
            /// Gets the seed.
            /// </summary>
            /// <value>
            /// The seed.
            /// </value>
            public int Seed { get; set; }

            /// <summary>
            /// The operations
            /// </summary>
            public Dictionary<BenchmarkOperation, int> Operations { get; set; }
        }
    }
}