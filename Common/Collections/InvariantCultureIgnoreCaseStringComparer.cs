using System;
using System.Collections.Generic;

namespace Wanderer.Library.Common.Collections
{
    /// <summary>
    /// Implement <see cref="IComparer{T}" /> for strings with <see cref="StringComparison.InvariantCultureIgnoreCase" /> option.
    /// </summary>
    public sealed class InvariantCultureIgnoreCaseStringComparer : IComparer<string>
    {
        #region IComparer<string> implementation
        /// <summary>
        /// Compares two strings and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">the first string to compare</param>
        /// <param name="y">the second string to compare</param>
        /// <returns>a signed integer that indicates the relative values of x and y</returns>
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion
    }
}