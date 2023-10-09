using System.Net;
using Xunit;
using Xunit.UserContext.Services;

namespace Xunit.UserContext.Tests.Utilities
{
    public class UserSecretsCredentialUtilityTests
    {
        [Fact]
        public void GetCredentials_WithExistingSecretsId_ReturnsValidCredentials()
        {
            // Arrange
            const string userSecretsId = "IntergrationTestID123";
            UserSecretsProvider userProvider = new(userSecretsId);

            // Act
            NetworkCredential result = userProvider.GetCredentials();

            // Assert
            Assert.IsAssignableFrom<NetworkCredential>(result);
        }
        [Fact]
        public void GetCredentials_WithUnsetSecretsId_ReturnsEmptyAndDoesntThrow()
        {
            // Arrange
            const string userSecretsId = "NonExistantId";
            UserSecretsProvider userProvider = new(userSecretsId);

            // Act
            NetworkCredential result = userProvider.GetCredentials();

            // Assert
            Assert.True(string.IsNullOrEmpty(result.UserName));
        }
    }
}
