using System;
using System.Threading.Tasks;

namespace CountryVATCalculator.Commands
{
    /// <summary>
    /// Make a query request with a return type of <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Return object type from query</typeparam>
    public class SafeCallServiceQuery<T>
    {
        /// <summary>
        /// Async query operation
        /// </summary>
        public Func<Task<T>> Func { get; private set; }

        /// <summary>
        /// Information useful in logging this query operation
        /// </summary>
        public SafeCallServiceLogMetadata Metadata { get; private set; }

        /// <summary>
        /// Construct a new instance
        /// </summary>
        /// <param name="func">Async operation for query</param>
        /// <param name="metadata">Logging information</param>
        public SafeCallServiceQuery(Func<Task<T>> func, SafeCallServiceLogMetadata metadata)
        {
            Func = func ?? throw new ArgumentNullException(nameof(func), $"Require {nameof(func)} to execute");
            Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata), $"Require {nameof(metadata)} for logging");
        }
    }
}
