using SafeticaTests.Config;

namespace SafeticaTests.Utils
{
    public static class ConfigLoader
    {
        public static TestConfig Load()
        {
            return new TestConfig
            {
                Login = Environment.GetEnvironmentVariable("TEAMS_LOGIN"),
                Password = Environment.GetEnvironmentVariable("TEAMS_PASSWORD"),
                ChatName = Environment.GetEnvironmentVariable("TEAMS_CHAT_NAME"),
                BaseUrl = Environment.GetEnvironmentVariable("TEAMS_BASE_URL"),
            };
        }
    }
}