using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    public sealed class DisposeAction : IDisposable
    {
        private readonly Action _disposeAction;
        private bool _disposed = false;

        public DisposeAction(Action disposeAction)
        {
            Contract.Requires<ArgumentNullException>(disposeAction != null);
            Contract.Ensures(_disposeAction != null);

            _disposeAction = disposeAction;
        }

        ~DisposeAction()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Contract.Ensures(_disposed);

            if (!_disposed)
            {
                if (disposing)
                {
                    _disposeAction();
                }

                _disposed = true;
            }
        }
    }
}
