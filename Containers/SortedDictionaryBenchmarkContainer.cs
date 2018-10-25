namespace Benchmark.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A benchmark compatible sorted dictionary
    /// </summary>
    internal class SortedDictionaryBenchmarkContainer<T> : IBenchmarkContainer<T>
    {
        /// <summary>
        /// The dictionary
        /// </summary>
        private readonly SortedDictionary<T, T> dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedDictionaryBenchmarkContainer{T}" /> class.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        private SortedDictionaryBenchmarkContainer(T[] tuples)
        {
            this.dictionary = new SortedDictionary<T, T>();
            foreach (T t in tuples)
            {
                this.dictionary.TryAdd(t, t);
            }

            this.Type = BenchmarkContainerType.SortedDictionary;
            this.Cardinality = tuples.Length;
        }

        /// <summary>
        /// Creates a benchmark container.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> Create(T[] tuples)
        {
            return new SortedDictionaryBenchmarkContainer<T>(tuples);
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
            this.dictionary.Remove(tuple);
        }

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>A tuple</returns>
        public T Find(T tuple)
        {
            return this.dictionary.TryGetValue(tuple, out T tupleFound) ? tupleFound : default(T);
        }

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Insert(T tuple)
        {
            this.dictionary.TryAdd(tuple, tuple);
        }

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        public void Iterate()
        {
            foreach (var item in this.dictionary)
            {
                ; // do nothing
            }
        }
    }
}
