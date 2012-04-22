using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    /// <summary>
    /// Helper class for using <see cref="IDisposable"/> interface.
    /// </summary>
    public sealed class DisposeAction : IDisposable
    {
        #region Variables
        private readonly Action _disposeAction;
        private bool _disposed;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="disposeAction">action that should be called in <see cref="IDisposable.Dispose"/> method</param>
        public DisposeAction(Action disposeAction)
        {
            Contract.Requires<ArgumentNullException>(disposeAction != null);
// ReSharper disable InvocationIsSkipped
            Contract.Ensures(_disposeAction != null);
// ReSharper restore InvocationIsSkipped

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
// ReSharper disable InvocationIsSkipped
            Contract.Ensures(_disposed);
// ReSharper restore InvocationIsSkipped

            if (!_disposed)
            {
                if (disposing)
                    _disposeAction();

                _disposed = true;
            }
        }
        #endregion
    }
}
