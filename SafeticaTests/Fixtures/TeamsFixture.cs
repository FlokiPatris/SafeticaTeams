using SafeticaTests.Config;
using SafeticaTests.Pages;
using SafeticaTests.Utils;

namespace SafeticaTests.Fixtures
{
    public class TeamsFixture : IAsyncLifetime
    {
        private readonly PlaywrightFixture _playwright;
        private ValidatedConfig _config = null!;
        public TeamsPage TeamsPage { get; private set; } = null!;

        public string TestDataFolder { get; private set; } = null!;
        public string DownloadFolder { get; private set; } = null!;

        public TeamsFixture()
        {
            _playwright = new PlaywrightFixture();
        }

        public async Task InitializeAsync()
        {
            TestDataFolder = TestEnvironment.GetProjectPath("SafeticaTests", "TestData");
            DownloadFolder = TestEnvironment.GetProjectPath("SafeticaTests", "Downloads");
            TestEnvironment.PrepareTestFolders("TestData", "Downloads");

            await _playwright.InitializeAsync();

            var rawConfig = ConfigLoader.Load();
            _config = ConfigValidator.EnsureValid(rawConfig);

            TeamsPage = new TeamsPage(_playwright.Page, _config.BaseUrl);
            await TeamsPage.LoginAsync(_config.Login, _config.Password);
        }

        public async Task DisposeAsync()
        {
            await TeamsPage.LogoutAsync();
            await _playwright.DisposeAsync();
        }
    }
}