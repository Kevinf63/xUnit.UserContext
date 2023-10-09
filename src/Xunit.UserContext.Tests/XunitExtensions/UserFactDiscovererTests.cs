using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit.UserContext.XunitExtensions;

namespace Xunit.UserContext.Tests.XunitExtensions
{
    public class UserFactDiscovererTests
    {
        private readonly IMessageSink _subMessageSink;

        public UserFactDiscovererTests()
        {
            _subMessageSink = Substitute.For<IMessageSink>();
        }

        private UserFactDiscoverer CreateUserFactDiscoverer()
        {
            return new UserFactDiscoverer(_subMessageSink);
        }

        [Fact]
        public void Discover_WithNullAttribute_CreatesTestCase()
        {
            // Arrange
            UserFactDiscoverer userFactDiscoverer = CreateUserFactDiscoverer();
            ITestFrameworkDiscoveryOptions discoveryOptions = TestFrameworkOptions.ForDiscovery();
            ITestMethod testMethod = Substitute.For<ITestMethod>();
            IAttributeInfo factAttribute = null;

            // Act
            IEnumerable<IXunitTestCase> result = userFactDiscoverer.Discover(discoveryOptions, testMethod, factAttribute);

            // Assert
            Assert.IsAssignableFrom<UserContextTestCase>(result.Single());
        }
    }
}
