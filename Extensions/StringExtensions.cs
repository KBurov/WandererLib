﻿namespace Wanderer.Library.Extensions
{
    /// <summary>
    /// <see cref="string"/> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if <paramref name="s"/> contains <see cref="System.Int32"/>
        /// </summary>
        public static bool IsInt32(this string s)
        {
            System.Int32 dummy;

            return !string.IsNullOrWhiteSpace(s) && System.Int32.TryParse(s, out dummy);
        }

        /// <summary>
        /// Returns true if <paramref name="s"/> contains <see cref="System.UInt32"/>
        /// </summary>
        public static bool IsUInt32(this string s)
        {
            System.UInt32 dummy;

            return !string.IsNullOrWhiteSpace(s) && System.UInt32.TryParse(s, out dummy);
        }

        /// <summary>
        /// Returns true if <paramref name="s"/> contains <see cref="System.Decimal"/>
        /// </summary>
        public static bool IsDecimal(this string s)
        {
            System.Decimal dummy;

            return !string.IsNullOrWhiteSpace(s) && System.Decimal.TryParse(s, out dummy);
        }
    }
}