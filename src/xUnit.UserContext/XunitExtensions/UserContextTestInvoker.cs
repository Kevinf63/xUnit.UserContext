using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using SimpleImpersonation;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.XunitExtensions
{
    /// <summary>
    /// Implementation of XunitTestInvoker that invokes test under a user context by using impersonation
    /// </summary>
    public class UserContextTestInvoker : XunitTestInvoker
    {
        private readonly UserContextSettings _userContext;

        /// <summary>
        /// Initialises a new instance of the <see cref="UserContextTestInvoker"/> class.
        /// </summary>
        /// <param name="test">The test that this invocation belongs to.</param>
        /// <param name="messageBus">The message bus to report run status to.</param>
        /// <param name="testClass">The test class that the test method belongs to.</param>
        /// <param name="constructorArguments">The arguments to be passed to the test class constructor.</param>
        /// <param name="testMethod">The test method that will be invoked.</param>
        /// <param name="testMethodArguments">The arguments to be passed to the test method.</param>
        /// <param name="beforeAfterAttributes">The list of <see cref="BeforeAfterTestAttribute"/>s for this test invocation.</param>
        /// <param name="aggregator">The exception aggregator used to run code and collect exceptions.</param>
        /// <param name="cancellationTokenSource">The task cancellation token source, used to cancel the test run.</param>
        /// <param name="userContext">The user context settings for impersonation</param>
        public UserContextTestInvoker(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments,
            MethodInfo testMethod, object[] testMethodArguments, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
            ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, UserContextSettings userContext)

            : base(test, messageBus, testClass, constructorArguments, testMethod,
                  testMethodArguments, beforeAfterAttributes, aggregator, cancellationTokenSource)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// Invokes the test method under the context of the provided user on the given test class instance. This method sets up support
        /// for "async void" test methods, ensures that the test method has the correct number of arguments, then calls <see cref="CallTestMethod"/>
        /// to do the actual method invocation. It ensure that any async test method is fully completed before returning, and
        /// returns the measured clock time that the invocation took.
        /// </summary>
        /// <param name="testClassInstance">The test class instance</param>
        /// <returns>Returns the time taken to invoke the test method</returns>
        [SupportedOSPlatform("windows")]
        protected override async Task<decimal> InvokeTestMethodAsync(object testClassInstance)
        {
            decimal timerTotal = default;

            using (SafeAccessTokenHandle userHandle = _userContext.GetCredentials().LogonUser(_userContext.LogonType))
            {
                timerTotal = await WindowsIdentity.RunImpersonatedAsync(userHandle, async () => await base.InvokeTestMethodAsync(testClassInstance));
            }

            return timerTotal;
        }
    }
}
