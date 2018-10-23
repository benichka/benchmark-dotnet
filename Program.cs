namespace Benchmark
{
    using System;
    using System.Collections.Generic;
    using Benchmark.Core;
    using Benchmark.Formatters;

    /// <summary>
    /// The main program
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            BenchmarkServiceConfiguration configuration = BenchmarkServiceConfiguration.Load();
            BenchmarkService benchmarkService = BenchmarkService.Create(configuration);

            IEnumerable<BenchmarkResult> results = benchmarkService.Run();
            
            IBenchmarkResultFormatter formatter = new CsvBenchmarkResultFormatter();

            Console.WriteLine(formatter.Format(results));
        }
    }
}
