namespace SafeticaTests.Config
{
    public class ValidatedConfig(string login, string password, string chatName, string baseUrl)
    {
        public string Login { get; } = login ?? throw new ArgumentNullException(nameof(login));
        public string Password { get; } = password ?? throw new ArgumentNullException(nameof(password));
        public string ChatName { get; } = chatName ?? throw new ArgumentNullException(nameof(chatName));
        public string BaseUrl { get; } = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
    }
}