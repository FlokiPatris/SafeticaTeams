using Microsoft.Playwright;

namespace SafeticaTests.Utils
{
    public static class BrowserFactory
    {
    public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
    {
        var headlessEnv = Environment.GetEnvironmentVariable("HEADLESS");
        var headless = headlessEnv?.ToLower() == "true";

        // If HEADLESS is not set, default to false (headed)
        if (headlessEnv == null)
        {
            headless = false;
        }

        Console.WriteLine($"HEADLESS = {headlessEnv ?? "not set"} → Launching browser in {(headless ? "headless" : "headed")} mode");

        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless
        });
    }
    }
}