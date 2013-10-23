using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Wanderer.Library.Common.ConsoleTests")]

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

        #region Variables
        private static readonly IDictionary<string, PropertyChangedEventArgs> _eventArgCache;
        private static readonly object _eventArgCacheSync = new object();
        #endregion

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
            _eventArgCache = new Dictionary<string, PropertyChangedEventArgs>();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
// ReSharper disable EmptyConstructor
        protected BindableObject() {}
// ReSharper restore EmptyConstructor
        #endregion

        /// <summary>
        /// Returns an instance of <see cref="PropertyChangedEventArgs"/> for
        /// the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to create event args for.</param>
        /// <returns>The instance of <see cref="PropertyChangedEventArgs"/></returns>
        private static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            PropertyChangedEventArgs result;

            lock (_eventArgCacheSync)
            {
                if (!_eventArgCache.TryGetValue(propertyName, out result))
                {
                    _eventArgCache.Add(
                        propertyName,
                        result = new PropertyChangedEventArgs(propertyName));
                }
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
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(propertyName), "propertyName cannot be null or empty");

            VerifyPropertyName(propertyName);

            var propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, GetPropertyChangedEventArgs(propertyName));

            AfterPropertyChanged(propertyName);
        }

        internal void RaisePropertyChangedNotVerified(string propertyName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(propertyName), "propertyName cannot be null or empty");

            var propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                propertyChanged(this, GetPropertyChangedEventArgs(propertyName));

            AfterPropertyChanged(propertyName);
        }

        internal void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            RaisePropertyChanged(GetPropertyNameFromExpression(property));
        }

        internal void RaisePropertyChangedNotVerified<T>(Expression<Func<T>> property)
        {
            RaisePropertyChangedNotVerified(GetPropertyNameFromExpression(property));
        }

        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            var type = GetType();
            var propertyInfo = type.GetProperty(propertyName);

            if (propertyInfo == null)
                Debug.Fail(string.Format(ErrorMessage, propertyName, type.FullName));
        }

        private static string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
        {
            var lambda = (LambdaExpression) property;
            var unaryExpression = lambda.Body as UnaryExpression;
            MemberExpression memberExpression;

            if (unaryExpression != null)
                memberExpression = (MemberExpression) unaryExpression.Operand;
            else
                memberExpression = (MemberExpression) lambda.Body;

            return memberExpression.Member.Name;
        }
    }
}
