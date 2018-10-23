namespace Benchmark.Containers
{
    /// <summary>
    /// The interface to a container which can be benchmarked
    /// </summary>
    /// <typeparam name="T">The type of data stored in the container</typeparam>
    internal interface IBenchmarkContainer<T>
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        BenchmarkContainerType Type { get; }

        /// <summary>
        /// Gets the cardinality.
        /// </summary>
        /// <value>
        /// The cardinality.
        /// </value>
        long Cardinality { get; }

        /// <summary>
        /// Deletes the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        void Delete(T tuple);

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        T Find(T tuple);

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        void Insert(T tuple);

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        void Iterate();
    }
}
