using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Extensions
{
    internal sealed class SortedEnumerableMergeEnumerator<T> : IEnumerator<T>
    {
        private const string ObjectDisposedExceptionMessage = "IEnumerator already disposed";

        private readonly IEnumerable<T>[] _sources;
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
// ReSharper disable MemberCanBePrivate.Global
        public bool IsDisposed { get; private set; }
// ReSharper restore MemberCanBePrivate.Global

        #region IEnumerator<T> implementation
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection</returns>
        public bool MoveNext()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current { get { return Current; } }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        void IDisposable.Dispose()
        {
            Contract.Ensures(IsDisposed);

            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Contract.Ensures(IsDisposed);

            if (IsDisposed) {
                return;
            }

            if (disposing) {
                //
            }

            IsDisposed = true;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="comparer"></param>
        public SortedEnumerableMergeEnumerator(IEnumerable<T>[] sources, IComparer<T> comparer)
        {
            Contract.Requires<ArgumentNullException>(sources != null, "sources cannot be null");
            Contract.Requires<ArgumentNullException>(comparer != null, "comparer cannot be null");

            _comparer = comparer;
            _sources = sources;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~SortedEnumerableMergeEnumerator()
        {
            Dispose(false);
        }
        #endregion
    }
}