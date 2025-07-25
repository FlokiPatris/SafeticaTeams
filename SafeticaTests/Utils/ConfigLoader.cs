using SafeticaTests.Config;

namespace SafeticaTests.Utils
{
    public static class ConfigLoader
    {
        public static TestConfig Load()
        {
            return new TestConfig
            {
                Login = Environment.GetEnvironmentVariable(SharedConstants.TeamsLoginSecretName),
                Password = Environment.GetEnvironmentVariable(SharedConstants.TeamsPasswordSecretName),
                ChatName = Environment.GetEnvironmentVariable(SharedConstants.TeamsChatNameSecretName),
                BaseUrl = Environment.GetEnvironmentVariable(SharedConstants.TeamsBaseUrlSecretName),
            };
        }
    }
}