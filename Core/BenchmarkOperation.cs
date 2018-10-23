namespace Benchmark.Core
{
    /// <summary>
    /// Types of benchmark operations
    /// </summary>
    internal enum BenchmarkOperation
    {
        /// <summary>
        /// The delete operation
        /// </summary>
        Delete,

        /// <summary>
        /// The find operation
        /// </summary>
        Find,

        /// <summary>
        /// The insert operation
        /// </summary>
        Insert,

        /// <summary>
        /// The iterate operation
        /// </summary>
        Iterate
    }
}
