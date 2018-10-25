namespace Benchmark.Containers
{
    using System.Collections.Generic;

    /// <summary>
    /// A benchmark compatible hash set
    /// </summary>
    internal class HashSetBenchmarkContainer<T> : IBenchmarkContainer<T>
    {
        /// <summary>
        /// The hash set
        /// </summary>
        private readonly HashSet<T> hashSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashSetBenchmarkContainer{T}" /> class.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        private HashSetBenchmarkContainer(T[] tuples)
        {
            this.hashSet = new HashSet<T>(tuples);
            this.Type = BenchmarkContainerType.HashSet;
            this.Cardinality = tuples.Length;
        }

        /// <summary>
        /// Creates a benchmark container.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> Create(T[] tuples)
        {
            return new HashSetBenchmarkContainer<T>(tuples);
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public BenchmarkContainerType Type { get; }

        /// <summary>
        /// Gets the cardinality.
        /// </summary>
        /// <value>
        /// The cardinality.
        /// </value>
        public long Cardinality { get; }

        /// <summary>
        /// Deletes the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Delete(T tuple)
        {
            this.hashSet.Remove(tuple);
        }

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>A tuple</returns>
        public T Find(T tuple)
        {
            return this.hashSet.TryGetValue(tuple, out T tupleFound) ? tupleFound : default(T);
        }

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Insert(T tuple)
        {
            this.hashSet.Add(tuple);
        }

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        public void Iterate()
        {
            foreach (var item in this.hashSet)
            {
                ; // do nothing
            }
        }
    }
}
