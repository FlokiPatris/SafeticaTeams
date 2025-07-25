using SafeticaTests.Config;

namespace SafeticaTests.Utils
{
    public static class ConfigValidator
    {
        public static ValidatedConfig EnsureValid(TestConfig config)
        {
            CustomLogger.Log("Validating configuration...");

            if (string.IsNullOrWhiteSpace(config.Login))
                throw new InvalidOperationException($"❌ {SharedConstants.TeamsLoginSecretName} is missing.");

            if (string.IsNullOrWhiteSpace(config.Password))
                throw new InvalidOperationException($"❌ {SharedConstants.TeamsPasswordSecretName} is missing.");

            if (string.IsNullOrWhiteSpace(config.ChatName))
                throw new InvalidOperationException($"❌ {SharedConstants.TeamsChatNameSecretName} is missing.");

            if (string.IsNullOrWhiteSpace(config.BaseUrl))
                throw new InvalidOperationException($"❌ {SharedConstants.TeamsBaseUrlSecretName} is missing.");

            CustomLogger.Log("Configuration validated successfully.");

            return new ValidatedConfig(
                config.Login,
                config.Password,
                config.ChatName,
                config.BaseUrl
            );
        }
    }
}