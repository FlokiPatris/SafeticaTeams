namespace SafeticaTests.Utils
{
    /// <summary>
    /// Shared constants used across Safetica test utilities.
    /// Includes default timeouts, string lengths, folder names, and environment variable keys.
    /// </summary>
    public static class SharedConstants
    {
        // ------------------------------------------------------------------
        // Default wait times (in milliseconds) for async operations
        // ------------------------------------------------------------------
        public const int WaitVeryShort = 100;     // For quick polling or debounce
        public const int WaitShort = 500;         // For short UI transitions
        public const int WaitMedium = 2000;       // For typical async operations
        public const int WaitLong = 5000;         // For longer waits (e.g. page loads)
        public const int WaitVeryLong = 10000;    // For maximum tolerable delays

        // ------------------------------------------------------------------
        // Default lengths for generated strings
        // ------------------------------------------------------------------
        public const int FileNameLength = 8;              // Random file name length
        public const int MessagePrefixLength = 10;        // Prefix for test messages
        public const int SampleFileContentLength = 100;   // Sample file content size

        // ------------------------------------------------------------------
        // Default folder names
        // ------------------------------------------------------------------
        public const string TestProjectFolder = "SafeticaTests"; // Root folder for test artifacts

        // ------------------------------------------------------------------
        // Environment variable keys
        // ------------------------------------------------------------------

        // General configuration
        public const string ProjectRootEnvVarName = "SAFETICA_PROJECT_ROOT";  // Path to project root

        // Secrets (used for authentication and secure access)
        public const string TeamsLoginSecretName = "TEAMS_LOGIN";             // Teams login username
        public const string TeamsPasswordSecretName = "TEAMS_PASSWORD";       // Teams login password
        public const string TeamsChatNameSecretName = "TEAMS_CHAT_NAME";      // Target Teams chat name
        public const string TeamsBaseUrlSecretName = "TEAMS_BASE_URL";        // Base URL for Teams API

        // Flags (boolean toggles)
        public const string HeadlessFlagEnvVarName = "HEADLESS";              // Run tests in headless mode
    }
}