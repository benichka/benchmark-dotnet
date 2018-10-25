namespace Benchmark.Containers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A benchmark compatible linked list 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Benchmark.Containers.IBenchmarkContainer{T}" />
    internal class LinkedListBenchmarkContainer<T> : IBenchmarkContainer<T>
    {
        /// <summary>
        /// The linked list
        /// </summary>
        private LinkedList<T> linkedList;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedListBenchmarkContainer{T}"/> class.
        /// </summary>
        /// <param name="tuples">The tuples.</param>
        private LinkedListBenchmarkContainer(T[] tuples)
        {
            this.linkedList = new LinkedList<T>(tuples);
            this.Type = BenchmarkContainerType.LinkedList;
            this.Cardinality = tuples.Length;
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
        /// Creates a benchmark container." />.
        /// </summary>
        /// <returns>A benchmark container</returns>
        public static IBenchmarkContainer<T> Create(T[] tuples)
        {
            return new LinkedListBenchmarkContainer<T>(tuples);
        }

        /// <summary>
        /// Deletes the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Delete(T tuple)
        {
            LinkedListNode<T> node = this.linkedList.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value.Equals(tuple))
                {
                    this.linkedList.Remove(node);
                }

                node = next;
            }
        }

        /// <summary>
        /// Finds the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        public T Find(T tuple)
        {
            LinkedListNode<T> node = this.linkedList.Find(tuple);
            return node != null ? node.Value : default(T);
        }

        /// <summary>
        /// Inserts the specified tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public void Insert(T tuple)
        {
            this.linkedList.AddLast(tuple);
        }

        /// <summary>
        /// Iterates through all items in the container.
        /// </summary>
        public void Iterate()
        {
            foreach (var tuple in this.linkedList)
            {
                ;
            }
        }
    }
}
