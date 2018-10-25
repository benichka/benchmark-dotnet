namespace Benchmark.Containers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A benchmark compatible sorted list
    /// </summary>
    internal class SortedListBenchmarkContainer<T> : IBenchmarkContainer<T>
    {
        /// <summary>
        /// The hash set
        /// </summary>
        private readonly SortedList<T, T> list;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedListBenchmarkContainer{T}" /> class.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        private SortedListBenchmarkContainer(T[] tuples)
        {
            this.list = new SortedList<T, T>();
            foreach (T t in tuples)
            {
                this.list.TryAdd(t, t);
            }

            this.Type = BenchmarkContainerType.SortedList;
            this.Cardinality = tuples.Length;
        }

        /// <summary>
        /// Creates a benchmark container.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> Create(T[] tuples)
        {
            return new SortedListBenchmarkContainer<T>(tuples);
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
            this.list.Remove(tuple);
        }

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>A tuple</returns>
        public T Find(T tuple)
        {
            return this.list.TryGetValue(tuple, out T tupleFound) ? tupleFound : default(T);
        }

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Insert(T tuple)
        {
            this.list.TryAdd(tuple, tuple);
        }

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        public void Iterate()
        {
            foreach (var item in this.list)
            {
                ; // do nothing
            }
        }
    }
}
