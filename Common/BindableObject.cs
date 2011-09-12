using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    /// <summary>
    /// Implements the <see cref="INotifyPropertyChanged"/> interface and
    /// exposes a <see cref="RaisePropertyChanged"/> method for derived
    /// classes to raise the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
    /// The event arguments created by this class are cached
    /// to prevent managed heap fragmentation.
    /// </summary>
    [Serializable]
    public abstract class BindableObject : INotifyPropertyChanged
    {
        private const string ErrorMessage = "{0} is not a public property of {1}";

        private static readonly IDictionary<string, PropertyChangedEventArgs> EventArgCache;
        private static readonly object EventArgCacheSync = new object();

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Static constructor.
        /// </summary>
        static BindableObject()
        {
            EventArgCache = new Dictionary<string, PropertyChangedEventArgs>();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected BindableObject() {}
        #endregion

        /// <summary>
        /// Returns an instance of <see cref="PropertyChangedEventArgs"/> for
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to create event args for.</param>
        /// <returns>The instance of <see cref="PropertyChangedEventArgs"/></returns>
        private static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(propertyName), "propertyName cannot be null or empty.");

            PropertyChangedEventArgs result;

            lock (EventArgCacheSync)
            {
                if (!EventArgCache.ContainsKey(propertyName))
                {
                    EventArgCache.Add(propertyName, new PropertyChangedEventArgs(propertyName));
                }

                result = EventArgCache[propertyName];
            }

            return result;
        }

        /// <summary>
        /// Derived classes can override this method to
        /// execute logic after a property is set. The
        /// base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">The property which was changed.</param>
        protected virtual void AfterPropertyChanged(string propertyName) {}

        /// <summary>
        /// Attempts to raise the <see cref="PropertyChanged"/> event, and
        /// invokes the virtual <see cref="AfterPropertyChanged"/> method,
        /// regardless of whether the event was raised or not.
        /// </summary>
        /// <param name="propertyName">The property which was changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            var propertyChanged = this.PropertyChanged;

            if (propertyChanged != null)
            {
                propertyChanged(this, GetPropertyChangedEventArgs(propertyName));
            }

            this.AfterPropertyChanged(propertyName);
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            var type = this.GetType();
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                Debug.Fail(string.Format(ErrorMessage, propertyName, type.FullName));
            }
        }
    }
}
