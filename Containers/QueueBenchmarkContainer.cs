namespace Benchmark.Containers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A benchmark compatible queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Benchmark.Containers.IBenchmarkContainer{T}" />
    internal class QueueBenchmarkContainer<T> : IBenchmarkContainer<T>
    {
        /// <summary>
        /// The queue
        /// </summary>
        private Queue<T> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueBenchmarkContainer{T}"/> class.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        private QueueBenchmarkContainer(T[] tuples)
        {
            this.queue = new Queue<T>(tuples);
            this.Type = BenchmarkContainerType.Queue;
            this.Cardinality = tuples.Length;
        }

        /// <summary>
        /// Creates an instance of <see cref="QueueBenchmarkContainer{T}" />.
        /// </summary>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> Create(T[] tuples)
        {
            return new QueueBenchmarkContainer<T>(tuples);
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
            this.queue = new Queue<T>(this.queue.Where(t => !t.Equals(tuple)));
        }

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public T Find(T tuple)
        {
            return this.queue.FirstOrDefault(t => t.Equals(tuple));
        }

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Insert(T tuple)
        {
            this.queue.Enqueue(tuple);
        }

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        public void Iterate()
        {
            foreach (var tuple in this.queue)
            {
                ; // no op
            }
        }
    }
}
