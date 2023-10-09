using System.Collections.Generic;
using NSubstitute;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.XunitExtensions;

namespace Xunit.UserContext.Tests.XunitExtensions
{
    public class UserTheoryDiscovererTests
    {
        private readonly IMessageSink subMessageSink;

        public UserTheoryDiscovererTests()
        {
            subMessageSink = Substitute.For<IMessageSink>();
        }

        private UserTheoryDiscoverer CreateUserTheoryDiscoverer()
        {
            return new UserTheoryDiscoverer(subMessageSink);
        }

        [Fact]
        public void Discover_WithNullAttribute_CreatesTestCase()
        {
            // Arrange
            UserTheoryDiscoverer userTheoryDiscoverer = CreateUserTheoryDiscoverer();
            ITestFrameworkDiscoveryOptions discoveryOptions = TestFrameworkOptions.ForDiscovery();
            ITestMethod testMethod = Substitute.For<ITestMethod>();
            IAttributeInfo theoryAttribute = Substitute.For<IAttributeInfo>();

            // Act
            IEnumerable<IXunitTestCase> result = userTheoryDiscoverer.Discover(discoveryOptions, testMethod, theoryAttribute);

            // Assert
            Assert.IsAssignableFrom<IEnumerable<UserContextTestCase>>(result);
        }
    }
}
