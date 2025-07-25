using Microsoft.Playwright;
using SafeticaTests.Config;
using SafeticaTests.Utils;

namespace SafeticaTests.Fixtures
{
    public class PlaywrightFixture : IAsyncLifetime
    {
        public ValidatedConfig Config { get; private set; } = null!;
        public IPlaywright Playwright { get; private set; } = null!;
        public IBrowser Browser { get; private set; } = null!;
        public IBrowserContext Context { get; private set; } = null!;
        public IPage Page { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            CustomLogger.Log("Initializing PlaywrightFixture...");
            var rawConfig = ConfigLoader.Load();
            Config = ConfigValidator.EnsureValid(rawConfig);

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            CustomLogger.Log("Playwright engine created.");

            Browser = await BrowserFactory.CreateBrowserAsync(Playwright);
            CustomLogger.Log("Browser instance launched.");

            Context = await Browser.NewContextAsync(new() { AcceptDownloads = true });
            CustomLogger.Log("Browser context created with AcceptDownloads=true.");

            Page = await Context.NewPageAsync();
            CustomLogger.Log("New page created in browser context.");
        }

        public async Task DisposeAsync()
        {
            CustomLogger.Log("Disposing PlaywrightFixture...");

            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();

            CustomLogger.Log("PlaywrightFixture disposed.");
        }
    }
}