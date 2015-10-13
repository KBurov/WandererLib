using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Wanderer.Library.Extensions
{
    internal sealed class SortedEnumerableMergeEnumerator<T> : IEnumerator<T>
    {
        private const string ObjectDisposedExceptionMessage = "IEnumerator already disposed";

        private readonly IEnumerable<T>[] _sources;
        private readonly IComparer<T> _comparer;

        private IEnumerator<T>[] _enumerators;
        private T _current;

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        #region IEnumerator<T> implementation
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return _current;
            }
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection</returns>
        public bool MoveNext()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            var result = false;

            if (_enumerators == null) {
                var enumerators = _sources
                    .Select(list => list.GetEnumerator())
                    .Where(enumerator => enumerator.MoveNext())
                    .ToArray();

                if (enumerators.Length > 0) {
                    Array.Sort(enumerators, (enumerator1, enumerator2) => _comparer.Compare(enumerator1.Current, enumerator2.Current));

                    _enumerators = enumerators;
                }
            }
            else {
                if (_enumerators.Length > 0) {
                    if (_enumerators[0].MoveNext()) {
                        _enumerators[0].Dispose();

                        _enumerators = _enumerators
                            .Skip(1)
                            .ToArray();
                    }

                    for (var i = 0;i < _enumerators.Length - 1;++i) {
                        if (_comparer.Compare(_enumerators[i].Current, _enumerators[i + 1].Current) > 0) {
                            var temp = _enumerators[i + 1];
                            _enumerators[i + 1] = _enumerators[i];
                            _enumerators[i] = temp;
                        }
                        else {
                            break;
                        }
                    }
                }
            }

            if (_enumerators != null && _enumerators.Length > 0) {
                _current = _enumerators[0].Current;

                result = true;
            }

            return result;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            DisposeEnumeratos();

            _current = default(T);
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        object IEnumerator.Current => Current;
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
                DisposeEnumeratos();
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
            _enumerators = null;
            _current = default(T);
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~SortedEnumerableMergeEnumerator()
        {
            Dispose(false);
        }
        #endregion

        private void DisposeEnumeratos()
        {
            var enumerators = _enumerators;

            if (enumerators != null) {
                _enumerators = null;

                for (var i = 0;i < enumerators.Length;++i) {
                    enumerators[i].Dispose();
                }
            }
        }
    }
}