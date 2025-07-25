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
            var rawConfig = ConfigLoader.Load();
            Config = ConfigValidator.EnsureValid(rawConfig);

            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await BrowserFactory.CreateBrowserAsync(Playwright);
            Context = await Browser.NewContextAsync(new() { AcceptDownloads = true });
            Page = await Context.NewPageAsync();
        }

        public async Task DisposeAsync()
        {
            await Context.CloseAsync();
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}