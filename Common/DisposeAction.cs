using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    /// <summary>
    /// Helper class for using <see cref="IDisposable"/> interface.
    /// </summary>
    public sealed class DisposeAction : IDisposable
    {
        private readonly Action _disposeAction;
        private bool _disposed;

        #region IDisposable implementation
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Contract.Ensures(_disposed);

            if (!_disposed)
            {
                if (disposing)
                    _disposeAction();

                _disposed = true;
            }
        }
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="disposeAction">action that should be called in <see cref="IDisposable.Dispose"/> method</param>
        /// <exception cref="ArgumentNullException"><paramref name="disposeAction"/> is null</exception>
        public DisposeAction(Action disposeAction)
        {
            Contract.Requires<ArgumentNullException>(disposeAction != null, $"{nameof(disposeAction)} cannot be null");
            Contract.Ensures(_disposeAction != null);

            _disposeAction = disposeAction;
            _disposed = false;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~DisposeAction()
        {
            Dispose(false);
        }
    }
}