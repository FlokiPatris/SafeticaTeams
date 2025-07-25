using Microsoft.Playwright;

namespace SafeticaTests.Utils
{
    public static class BrowserFactory
    {
        public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
        {
            var headless = Environment.GetEnvironmentVariable("HEADLESS")?.ToLower() != "false";
            CustomLogger.Log($"[BrowserFactory] Launching browser in {(headless ? "headless" : "headed")} mode");

            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless
            });

            CustomLogger.Log("[BrowserFactory] Browser launched successfully");
            return browser;
        }
    }
}