using System;
using SimpleImpersonation;
using Xunit.UserContext.Configuration;

namespace Xunit.UserContext.Tests.Configuration
{
    public class UserContextSettingsTests
    {
        [Fact]
        public void GetCredentials_WithNullCredentials_ThrowsOnGetCredentials()
        {
            // Arrange
            UserContextSettings unitUnderTest = new(
                username: null,
                password: null,
                domain: null,
                logonType: Default.Logon,
                displayNameOnTest: false);

            // Act
            void act()
            {
                unitUnderTest.GetCredentials();
            }

            // Assert
            Assert.Throws<ArgumentException>(act);
        }
        [Fact]
        public void GetCredentials_WithValidStrings_ReturnsUserCredentials()
        {
            // Arrange
            UserContextSettings unitUnderTest = new(
                username: "hello",
                password: "world",
                logonType: Default.Logon,
                displayNameOnTest: false);

            // Act
            UserCredentials credentials = unitUnderTest.GetCredentials();

            // Assert
            Assert.IsAssignableFrom<UserCredentials>(credentials);
        }
    }
}
