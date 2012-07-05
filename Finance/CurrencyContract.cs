using System.Diagnostics.Contracts;

namespace Wanderer.Library.Finance
{
    [ContractClassFor(typeof(ICurrency))]
    internal abstract class CurrencyContract : ICurrency
    {
        public string Name
        {
            get
            {
// ReSharper disable InvocationIsSkipped
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()), "property Name can not be null or empty");
// ReSharper restore InvocationIsSkipped
                return default(string);
            }
        }
    }
}
