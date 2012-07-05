﻿using System.Diagnostics.Contracts;

namespace Wanderer.Library.Finance
{
    /// <summary>
    /// Currency interface.
    /// </summary>
    [ContractClass(typeof(CurrencyContract))]
    public interface ICurrency
    {
        /// <summary>
        /// Currency name.
        /// </summary>
        string Name { get; }
    }
}
