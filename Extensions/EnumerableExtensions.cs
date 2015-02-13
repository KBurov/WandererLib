using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Extensions
{
    /// <summary>
    /// Extension and helper methods to process <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Create object with <see cref="IEnumerator{T}"/> interface to enumerate through merged sorted enumerables.
        /// </summary>
        /// <typeparam name="T">type of enumerable elements</typeparam>
        /// <param name="sources">collections of sorted enumerables</param>
        /// <param name="comparer">enumerable elemenets comparer (<see cref="IComparer{T}"/></param>
        /// <returns>object with <see cref="IEnumerator{T}"/> interface</returns>
        public static IEnumerator<T> GetSortedEnumerableMergeEnumerator<T>(this IEnumerable<T>[] sources, IComparer<T> comparer)
        {
            Contract.Requires<ArgumentNullException>(sources != null, "sources cannot be null");
            Contract.Requires<ArgumentNullException>(comparer != null, "comparer cannot be null");

            return new SortedEnumerableMergeEnumerator<T>(sources, comparer);
        }

        //public static IEnumerable<T> MergeSortedEnumerable<T>(IEnumerable<T>[] sources, IComparer<T> comparer)
        //{
        //    Contract.Requires<ArgumentNullException>(sources != null, "sources cannot be null");
        //    Contract.Requires<ArgumentNullException>(comparer != null, "comparer cannot be null");

        //    if (sources.Length == 0) {
        //        return Enumerable.Empty<T>();
        //    }

        //    if (sources.Length == 1) {
        //        return sources[0];
        //    }

        //    var enumerators = sources
        //        .Select(list => list.GetEnumerator())
        //        .Where(e => e.MoveNext());
        //}
    }
}