using SafeticaTests.Config;
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
            TestDataFolder = TestEnvironment.GetProjectPath("TestData");
            DownloadFolder = TestEnvironment.GetProjectPath("Downloads");
            LogFolder = TestEnvironment.GetProjectPath("Logs");

            Logger.Initialize(LogFolder);
            Logger.Log($"Logger initialized. Log file will be written to: {LogFolder}");

            Logger.Log("Starting TeamsFixture initialization...");
            Logger.Log($"TestDataFolder: {TestDataFolder}");
            Logger.Log($"DownloadFolder: {DownloadFolder}");
            Logger.Log($"LogFolder: {LogFolder}");

            TestEnvironment.PrepareTestFolders("TestData", "Downloads", "Logs");
            Logger.Log("Test folders prepared and cleared.");

            await _playwright.InitializeAsync();
            Logger.Log("Playwright browser launched and page context created.");

            var rawConfig = ConfigLoader.Load();
            _config = ConfigValidator.EnsureValid(rawConfig);
            Logger.Log("Configuration loaded and validated successfully.");

            TeamsPage = new TeamsPage(_playwright.Page, _config.BaseUrl);
            await TeamsPage.LoginAsync(_config.Login, _config.Password);
            Logger.Log($"Logged into TeamsPage at {_config.BaseUrl} as user '{_config.Login}'.");
        }

        public async Task DisposeAsync()
        {
            Logger.Log("Beginning TeamsFixture cleanup...");

            await TeamsPage.LogoutAsync();
            Logger.Log("User logged out from TeamsPage.");

            await _playwright.DisposeAsync();
            Logger.Log("Playwright browser and context disposed.");

            Logger.Log("TeamsFixture cleanup complete.");
        }
    }
}