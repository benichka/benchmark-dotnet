namespace Benchmark
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
            DateTime startTime = DateTime.UtcNow;

            try
            {
                BenchmarkServiceConfiguration configuration = BenchmarkServiceConfiguration.Load();
                BenchmarkService benchmarkService = BenchmarkService.Create(configuration);

                IEnumerable<BenchmarkResult> results = benchmarkService.Run();

                IBenchmarkResultFormatter formatter = new CsvBenchmarkResultFormatter();
                File.WriteAllText(Program.GetOutputFileName(startTime), formatter.Format(results));
            }
            catch(Exception ex)
            {
                if(Utilities.IsSystemFatal(ex))
                {
                    throw;
                }

                File.WriteAllText(Program.GetErrorFileName(startTime), $"Program hit exception: {ex}");
            }   
        }

        /// <summary>
        /// Gets the name of the output file.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>
        /// A file name string
        /// </returns>
        private static string GetOutputFileName(DateTime time)
        {
            return $"output.{Utilities.GetTimeStamp(time)}.csv";
        }

        /// <summary>
        /// Gets the name of the error file.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>A file name string</returns>
        private static string GetErrorFileName(DateTime time)
        {
            DateTime now = DateTime.UtcNow;
            return $"error.{Utilities.GetTimeStamp(time)}.log";
        }
    }
}
