using Microsoft.Playwright;

namespace SafeticaTests.Utils
{
    public static class BrowserFactory
    {
        public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
        {
            var headless = Environment.GetEnvironmentVariable("HEADLESS")?.ToLower() != "false";

            Console.WriteLine($"Launching browser in {(headless ? "headless" : "headed")} mode");

            return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless
            });
        }
    }
}