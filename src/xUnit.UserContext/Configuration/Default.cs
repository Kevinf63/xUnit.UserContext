using SimpleImpersonation;

namespace Xunit.UserContext.Configuration
{
    /// <summary>
    /// Default settings for test attributes
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// Display username at the end of the test
        /// </summary>
        public const bool DisplayName = true;
        /// <summary>
        /// Use network login
        /// </summary>
        public const LogonType Logon = LogonType.Network;
    }
}
