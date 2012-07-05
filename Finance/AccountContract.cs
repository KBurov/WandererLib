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
// ReSharper disable InvocationIsSkipped
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()), "property Name can not be null or empty");
// ReSharper restore InvocationIsSkipped
                return default(string);
            }
        }

        public IAccountType Type
        {
            get
            {
// ReSharper disable InvocationIsSkipped
                Contract.Ensures(Contract.Result<IAccountType>() != null, "property Type can not be null");
// ReSharper restore InvocationIsSkipped
                return default(IAccountType);
            }
        }

        public IMoney<T> Balance
        {
            get
            {
// ReSharper disable InvocationIsSkipped
                Contract.Ensures(Contract.Result<IMoney<T>>() != null, "property Balance can not be null");
// ReSharper restore InvocationIsSkipped
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
