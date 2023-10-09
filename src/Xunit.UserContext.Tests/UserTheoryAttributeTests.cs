namespace Xunit.UserContext.Tests
{
    public class UserTheoryAttributeTests
    {
        [Theory]
        [InlineData("Username", "Password", "Domain", true)]
        [InlineData("Username", "Password", "Domain", false)]
        public void UserTheoryAttribute_WithDomain_BuildsUserContextSettings(string username, string password, string domain, bool displayName)
        {
            // Arrange
            UserTheoryAttribute attribute = new(username: username,
                password: password,
                domain: domain,
                displayNameOnTest: displayName);

            // Act
            string result = attribute.UserContext.GetCredentials().ToString();

            // Assert
            Assert.Equal($"{username}@{domain}", result);
            Assert.Equal(displayName, attribute.UserContext.DisplayNameOnTest);
        }
        [Theory]
        [InlineData("Username", "Password", true)]
        [InlineData("Username", "Password", false)]
        public void UserTheoryAttribute_WithUsernamen_BuildsUserContextSettings(string username, string password, bool displayName)
        {
            // Arrange
            UserTheoryAttribute attribute = new(
                username: username,
                password: password,
                displayNameOnTest: displayName);

            // Act
            string result = attribute.UserContext.GetCredentials().ToString();

            // Assert
            Assert.Equal($"{username}", result);
            Assert.Equal(displayName, attribute.UserContext.DisplayNameOnTest);
        }
    }
}
