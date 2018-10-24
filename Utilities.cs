namespace Benchmark
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines some generally useful utilities
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Determines whether the exception is a fatal system exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>
        ///   <c>true</c> if a fatal system exception; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSystemFatal(Exception ex)
        {
            return ex is AccessViolationException
                   || ex is AppDomainUnloadedException
                   || ex is BadImageFormatException
                   || ex is DataMisalignedException
                   || ex is InsufficientExecutionStackException
                   || ex is InvalidOperationException
                   || ex is MemberAccessException
                   || ex is OutOfMemoryException
                   || ex is StackOverflowException
                   || ex is TypeInitializationException
                   || ex is TypeLoadException
                   || ex is TypeUnloadedException
                   || ex is UnauthorizedAccessException
                   || ex is ThreadAbortException
                   || ex is System.IO.InternalBufferOverflowException
                   || ex is System.Security.SecurityException;
        }

        /// <summary>
        /// Gets the slice.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static T[] GetSlice<T>(T[] array, long size)
        {
            if (size > array.LongLength)
            {
                throw new ArgumentException($"must not exceed number of elements in ({nameof(array)})", nameof(size));
            }

            T[] slice = new T[size];
            Array.Copy(array, slice, size);

            return slice;
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>A timestamp</returns>
        public static string GetTimeStamp(DateTime time)
        {
            return time.ToString("yyyyMMddHHmmssffff");
        }
    }
}
