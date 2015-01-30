using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        #region Consuming
        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, int boundedCapacity = 0)
        {
            return ConsumeWithQueue(producer, CancellationToken.None, null, boundedCapacity);
        }

        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, CancellationToken cancellationToken, int boundedCapacity = 0)
        {
            return ConsumeWithQueue(producer, cancellationToken, null, boundedCapacity);
        }

        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, Action<int> producedCountTracker, int boundedCapacity = 0)
        {
            return ConsumeWithQueue(producer, CancellationToken.None, producedCountTracker, boundedCapacity);
        }

        public static IEnumerable<T> ConsumeWithQueue<T>(this IEnumerable<T> producer, CancellationToken cancellationToken, Action<int> producedCountTracker, int boundedCapacity = 0)
        {
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
    }
}