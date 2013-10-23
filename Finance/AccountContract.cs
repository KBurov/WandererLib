using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Finance
{
    [ContractClassFor(typeof(IAccount<>))]
    internal abstract class AccountContract<T> : IAccount<T> where T : ICurrency
    {
        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()), "property Name can not be null or empty");

                return default(string);
            }
        }

        public IAccountType Type
        {
            get
            {
                Contract.Ensures(Contract.Result<IAccountType>() != null, "property Type can not be null");

                return default(IAccountType);
            }
        }

        public IMoney<T> Balance
        {
            get
            {
                Contract.Ensures(Contract.Result<IMoney<T>>() != null, "property Balance can not be null");

                return default(IMoney<T>);
            }
        }

        public void Withdraw(IMoney<T> money)
        {
            Contract.Requires<ArgumentNullException>(money != null, "parameter money can not be null");
        }

        public void Deposit(IMoney<T> money)
        {
            Contract.Requires<ArgumentNullException>(money != null, "parameter money can not be null");
        }
    }
}
