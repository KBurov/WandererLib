using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains code contracts definition for interface <see cref="IProcessManagement"/>.
    /// </summary>
    [ContractClassFor(typeof (IProcessManagement))]
    // ReSharper disable once InconsistentNaming
    internal abstract class IProcessManagementContract : IProcessManagement
    {
        private const string ApplicationNameOrArgumentsExceptionMessage = "applicationName or arguments cannot be null or empty";
        private const string EnvironmentVariablesExceptionMessage = "environmentVariables cannot be null";
        private const string ProcessNameExceptionMessage = "processName cannot be null or empty";

        #region IProcessManagement implementation
        /// <summary>
        /// Create a new process.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended IProcessManagement.CreateProcess(string applicationName, string arguments)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(applicationName) || !string.IsNullOrWhiteSpace(arguments),
                                                 ApplicationNameOrArgumentsExceptionMessage);
            Contract.Ensures(Contract.Result<IProcessExtended>() != null);

            return default(IProcessExtended);
        }

        /// <summary>
        /// Create a new process.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <param name="environmentVariables">the list of environment variables to add/modify</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended IProcessManagement.CreateProcess(string applicationName, string arguments, IDictionary<string, string> environmentVariables)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(applicationName) || !string.IsNullOrWhiteSpace(arguments),
                                                 ApplicationNameOrArgumentsExceptionMessage);
            Contract.Requires<ArgumentNullException>(environmentVariables != null, EnvironmentVariablesExceptionMessage);
            Contract.Ensures(Contract.Result<IProcessExtended>() != null);

            return default(IProcessExtended);
        }

        /// <summary>
        /// Create a new process with the specified credentials.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <param name="userName">user name</param>
        /// <param name="domain">user domain</param>
        /// <param name="password">user password</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended IProcessManagement.CreateProcess(string applicationName, string arguments, string userName, string domain, string password)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(applicationName) || !string.IsNullOrWhiteSpace(arguments),
                                                 ApplicationNameOrArgumentsExceptionMessage);
            Contract.Ensures(Contract.Result<IProcessExtended>() != null);

            return default(IProcessExtended);
        }

        /// <summary>
        /// Create a new process with the specified credentials.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <param name="userName">user name</param>
        /// <param name="domain">user domain</param>
        /// <param name="password">user password</param>
        /// <param name="environmentVariables">the list of environment variables to add/modify</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended IProcessManagement.CreateProcess(string applicationName, string arguments, string userName, string domain, string password,
                                                          IDictionary<string, string> environmentVariables)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(applicationName) || !string.IsNullOrWhiteSpace(arguments),
                                                 ApplicationNameOrArgumentsExceptionMessage);
            Contract.Requires<ArgumentNullException>(environmentVariables != null, EnvironmentVariablesExceptionMessage);
            Contract.Ensures(Contract.Result<IProcessExtended>() != null);

            return default(IProcessExtended);
        }

        /// <summary>
        /// Close all processes with specified name.
        /// </summary>
        /// <param name="processName">process name</param>
        void IProcessManagement.CloseProcess(string processName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(processName), ProcessNameExceptionMessage);
        }

        /// <summary>
        /// Terminate all processes 
        /// </summary>
        /// <param name="processName"></param>
        void IProcessManagement.TerminateProcess(string processName)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(processName), ProcessNameExceptionMessage);
        }

        /// <summary>
        /// Run a <see cref="Func{SafeTokeHandle, T}"/> with currently logged user credentials.
        /// </summary>
        /// <typeparam name="T">type of return value</typeparam>
        /// <param name="func">a <see cref="Func{TResult}"/> object</param>
        /// <returns>result value</returns>
        T IProcessManagement.ActAsLoggedOnUser<T>(Func<SafeTokenHandle, T> func)
        {
            Contract.Requires<ArgumentNullException>(func != null, "func cannot be null");

            return default(T);
        }

        /// <summary>
        /// Run a <see cref="Action{SafeTokeHandle}"/> with currently logged user credentials.
        /// </summary>
        /// <param name="action">a <see cref="Action{SafeTokenHandle}"/> object</param>
        /// <returns>result value</returns>
        void IProcessManagement.ActAsLoggedOnUser(Action<SafeTokenHandle> action)
        {
            Contract.Requires<ArgumentNullException>(action != null, "action cannot be null");
        }
        #endregion
    }
}