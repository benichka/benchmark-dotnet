namespace Benchmark.Containers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Produces benchmark containers
    /// </summary>
    /// <typeparam name="T">The data type to store in container</typeparam>
    internal class BenchmarkContainerFactory<T>
    {
        /// <summary>
        /// Creates a benchmark container.
        /// </summary>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> CreateBenchmarkContainer(BenchmarkContainerType type, T[] tuples)
        {
            switch (type)
            {
                case BenchmarkContainerType.LinkedList:
                    return LinkedListBenchmarkContainer<T>.Create(tuples);
                case BenchmarkContainerType.Queue:
                    return QueueBenchmarkContainer<T>.Create(tuples);
                default:
                    throw new ArgumentException("Type not supported", nameof(type));
            }
        }
    }
}
