using SafeticaTests.Config;
using SafeticaTests.Helpers;
using SafeticaTests.Pages;
using SafeticaTests.Utils;

namespace SafeticaTests.Fixtures
{
    public class TeamsFixture : IAsyncLifetime
    {
        private readonly PlaywrightFixture _playwright = new();
        private ValidatedConfig _config = null!;
        public TeamsPage TeamsPage { get; private set; } = null!;

        public string TestDataFolder { get; private set; } = null!;
        public string DownloadFolder { get; private set; } = null!;
        public string LogFolder { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            // Ensure folders exist before using them
            FileHelpers.EnsureFoldersExist("TestData", "Downloads", "Logs");

            TestDataFolder = FileHelpers.GetProjectPath("TestData");
            DownloadFolder = FileHelpers.GetProjectPath("Downloads");
            LogFolder = FileHelpers.GetProjectPath("Logs");

            // Initialize logger after log folder is ready
            CustomLogger.Initialize(LogFolder);

            CustomLogger.Log("Starting TeamsFixture initialization...");
            CustomLogger.Log($"TestDataFolder: {TestDataFolder}");
            CustomLogger.Log($"DownloadFolder: {DownloadFolder}");
            CustomLogger.Log($"LogFolder: {LogFolder}");

            // Clear folders after logger is initialized
            FileHelpers.ClearFolders("TestData", "Downloads", "Logs");
            CustomLogger.Log("Test folders cleared.");

            await _playwright.InitializeAsync();

            _config = _playwright.Config;

            TeamsPage = new TeamsPage(_playwright.Page, _config.BaseUrl);
            await TeamsPage.LoginAsync(_config.Login, _config.Password);
            CustomLogger.Log($"Logged into TeamsPage at {_config.BaseUrl} as user '{_config.Login}'.");
        }

        public async Task DisposeAsync()
        {
            CustomLogger.Log("Beginning TeamsFixture cleanup...");

            await TeamsPage.LogoutAsync();
            CustomLogger.Log("User logged out from TeamsPage.");

            await _playwright.DisposeAsync();

            CustomLogger.Log("TeamsFixture cleanup complete.");
        }
    }
}