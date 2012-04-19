using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    public sealed class DisposeAction : IDisposable
    {
        public DisposeAction(Action disposeAction)
        {
            Contract.Requires<ArgumentNullException>(disposeAction != null);
// ReSharper disable InvocationIsSkipped
            Contract.Ensures(_disposeAction != null);
// ReSharper restore InvocationIsSkipped

            _disposeAction = disposeAction;
            _disposed = false;
        }

        ~DisposeAction()
        {
            Dispose(false);
        }

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

        #region Variables
        private readonly Action _disposeAction;
        private bool _disposed;
        #endregion
    }
}
