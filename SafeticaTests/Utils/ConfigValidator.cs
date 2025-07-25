using SafeticaTests.Config;

namespace SafeticaTests.Utils
{
    public static class ConfigValidator
    {
        public static ValidatedConfig EnsureValid(TestConfig config)
        {
            CustomLogger.Log("Validating configuration...");
            if (string.IsNullOrWhiteSpace(config.Login))
                throw new InvalidOperationException("❌ TEAMS_LOGIN is missing.");
            if (string.IsNullOrWhiteSpace(config.Password))
                throw new InvalidOperationException("❌ TEAMS_PASSWORD is missing.");
            if (string.IsNullOrWhiteSpace(config.ChatName))
                throw new InvalidOperationException("❌ TEAMS_CHAT_NAME is missing.");
            if (string.IsNullOrWhiteSpace(config.BaseUrl))
                throw new InvalidOperationException("❌ TEAMS_BASE_URL is missing.");

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