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
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()), "property Name can not be null or empty");

                return default(string);
            }
        }
    }
}
