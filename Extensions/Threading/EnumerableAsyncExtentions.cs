using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable MethodSupportsCancellation
// TODO: Review cancellation support
// TODO: Review LongRunning task support

namespace Wanderer.Library.Extensions.Threading
{
    /// <summary>
    /// Extension methods to process <see cref="IEnumerable{T}"/> in async way.
    /// </summary>
    public static class EnumerableAsyncExtentions
    {
        private const string ProducerNullErrorMessage = "producer cannot be null";
        private const string SourceNullErrorMessage = "source cannot be null";
        private const string SelectorNullErrorMessage = "selector cannot be null";
        private const string DegreeOfParallelismOutOfRangeErrorMessage = "degreeOfPrallelism cannot be less than 1";

        #region Consuming
        /// <summary>
        /// Consume elements from <paramref name="producer"/> in async way through internal thread-safe collection.
        /// </summary>
        /// <typeparam name="T">type of elements in <paramref name="producer"/></typeparam>
        /// <param name="producer">source of elements for consuming</param>
        /// <param name="boundedCapacity">bounded size of the internal collection</param>
        /// <returns>enumeration of elements from <paramref name="producer"/></returns>
        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, int boundedCapacity = 0)
        {
            Contract.Requires<ArgumentNullException>(producer != null, ProducerNullErrorMessage);

            return ConsumeWithQueue(producer, CancellationToken.None, null, boundedCapacity);
        }

        /// <summary>
        /// Consume elements from <paramref name="producer"/> in async way through internal thread-safe collection.
        /// </summary>
        /// <typeparam name="T">type of elements in <paramref name="producer"/></typeparam>
        /// <param name="producer">source of elements for consuming</param>
        /// <param name="cancellationToken">notify when cancellation is needed</param>
        /// <param name="boundedCapacity">bounded size of the internal collection</param>
        /// <returns>enumeration of elements from <paramref name="producer"/></returns>
        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, CancellationToken cancellationToken, int boundedCapacity = 0)
        {
            Contract.Requires<ArgumentNullException>(producer != null, ProducerNullErrorMessage);

            return ConsumeWithQueue(producer, cancellationToken, null, boundedCapacity);
        }

        /// <summary>
        /// Consume elements from <paramref name="producer"/> in async way through internal thread-safe collection.
        /// </summary>
        /// <typeparam name="T">type of elements in <paramref name="producer"/></typeparam>
        /// <param name="producer">source of elements for consuming</param>
        /// <param name="producedCountTracker">action to count produced elements</param>
        /// <param name="boundedCapacity">bounded size of the internal collection</param>
        /// <returns>enumeration of elements from <paramref name="producer"/></returns>
        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, Action<int> producedCountTracker, int boundedCapacity = 0)
        {
            Contract.Requires<ArgumentNullException>(producer != null, ProducerNullErrorMessage);

            return ConsumeWithQueue(producer, CancellationToken.None, producedCountTracker, boundedCapacity);
        }

        /// <summary>
        /// Consume elements from <paramref name="producer"/> in async way through internal thread-safe collection.
        /// </summary>
        /// <typeparam name="T">type of elements in <paramref name="producer"/></typeparam>
        /// <param name="producer">source of elements for consuming</param>
        /// <param name="cancellationToken">notify when cancellation is needed</param>
        /// <param name="producedCountTracker">action to count produced elements</param>
        /// <param name="boundedCapacity">bounded size of the internal collection</param>
        /// <returns>enumeration of elements from <paramref name="producer"/></returns>
        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, CancellationToken cancellationToken, Action<int> producedCountTracker,
                                                         int boundedCapacity = 0)
        {
            Contract.Requires<ArgumentNullException>(producer != null, ProducerNullErrorMessage);

            var queue = boundedCapacity > 0
                ? new BlockingCollection<T>(boundedCapacity)
                : new BlockingCollection<T>();

            var task = Task.Factory.StartNew(
                () =>
                    {
                        try {
                            var i = 0;

                            foreach (var item in producer) {
                                cancellationToken.ThrowIfCancellationRequested();

                                if (producedCountTracker != null) {
                                    producedCountTracker(i++);
                                }

                                queue.Add(item);
                            }
                        }
                        finally {
                            queue.CompleteAdding();
                        }
                    },
                cancellationToken);

            foreach (var item in queue.GetConsumingEnumerable()) {
                yield return item;
            }

            task.Wait();
        }
        #endregion

        #region Parallel helpers
        public static IEnumerable<TResult> ParallelSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, int degreeOfParallelism)
        {
            Contract.Requires<ArgumentNullException>(source != null, SourceNullErrorMessage);
            Contract.Requires<ArgumentNullException>(selector != null, SelectorNullErrorMessage);
            Contract.Requires<ArgumentOutOfRangeException>(degreeOfParallelism < 1, DegreeOfParallelismOutOfRangeErrorMessage);

            return source
                .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .WithDegreeOfParallelism(degreeOfParallelism)
                .Select(selector);
        }

        public static IEnumerable<TResult> ParallelUnorderedSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector,
                                                                                     int degreeOfParallelism)
        {
            Contract.Requires<ArgumentNullException>(source != null, SourceNullErrorMessage);
            Contract.Requires<ArgumentNullException>(selector != null, SelectorNullErrorMessage);
            Contract.Requires<ArgumentOutOfRangeException>(degreeOfParallelism < 1, DegreeOfParallelismOutOfRangeErrorMessage);

            return source
                .AsParallel()
                .AsUnordered()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .WithDegreeOfParallelism(degreeOfParallelism)
                .Select(selector);
        }
        #endregion
    }
}