using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains set of functions for process management.
    /// </summary>
    [ContractClass(typeof (IProcessManagementContract))]
    [SecurityCritical]
    public interface IProcessManagement
    {
        /// <summary>
        /// Create a new process.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended CreateProcess(string applicationName, string arguments);

        /// <summary>
        /// Create a new process.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <param name="environmentVariables">the list of environment variables to add/modify</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended CreateProcess(string applicationName, string arguments, IDictionary<string, string> environmentVariables);

        /// <summary>
        /// Create a new process with the specified credentials.
        /// </summary>
        /// <param name="applicationName">the name of the module to be executed</param>
        /// <param name="arguments">the arguments for a new process</param>
        /// <param name="userName">user name</param>
        /// <param name="domain">user domain</param>
        /// <param name="password">user password</param>
        /// <returns><see cref="IProcessExtended"/> object which describes created process</returns>
        IProcessExtended CreateProcess(string applicationName, string arguments, string userName, string domain, string password);

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
        IProcessExtended CreateProcess(string applicationName, string arguments, string userName, string domain, string password, IDictionary<string, string> environmentVariables);

        /// <summary>
        /// Close all processes with specified name.
        /// </summary>
        /// <param name="processName">process name</param>
        void CloseProcess(string processName);

        /// <summary>
        /// Terminate all processes 
        /// </summary>
        /// <param name="processName"></param>
        void TerminateProcess(string processName);

        /// <summary>
        /// Run a <see cref="Func{SafeTokeHandle, T}"/> with currently logged user credentials.
        /// </summary>
        /// <typeparam name="T">type of return value</typeparam>
        /// <param name="func">a <see cref="Func{TResult}"/> object</param>
        /// <returns>result value</returns>
        T ActAsLoggedOnUser<T>(Func<SafeTokenHandle, T> func);

        /// <summary>
        /// Run a <see cref="Action{SafeTokeHandle}"/> with currently logged user credentials.
        /// </summary>
        /// <param name="action">a <see cref="Action{SafeTokenHandle}"/> object</param>
        /// <returns>result value</returns>
        void ActAsLoggedOnUser(Action<SafeTokenHandle> action);
    }
}