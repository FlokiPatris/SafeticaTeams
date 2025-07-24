using Microsoft.Playwright;
using SafeticaTeamsPlaywright.Utils;
using SafeticaTeamsPlaywright.Pages;
namespace SafeticaTeamsPlaywright.Tests
{
    public class TeamsTests
    {
        [Fact]
        public async Task SendMessagesAndHandleFile()
        {
            var config = ConfigLoader.Load();

            using var playwright = await Playwright.CreateAsync();
            var browser = await BrowserFactory.CreateBrowserAsync(playwright);

            var context = await browser.NewContextAsync(new() { AcceptDownloads = true });
            var page = await context.NewPageAsync();

            var teamsPage = new TeamsPage(page);
            await teamsPage.LoginAsync(config.Login, config.Password);

            // Send messages
            await teamsPage.SendMessageAsync("aaaa");
            await teamsPage.SendMessageAsync("bbbbb");
            await teamsPage.SendMessageAsync("ccc");

            // Upload file
            var filePath = "TestData/sample.txt";
            await teamsPage.UploadFileAsync(filePath);
            await Task.Delay(3000);

            // Download file
            await teamsPage.DownloadLastFileAsync("Downloads");

            // Screenshot
            await page.ScreenshotAsync(new() { Path = "TeamsTest.png" });

            // Assertions
            var count = await teamsPage.CountMessagesAsync("Hello");
            Assert.True(count >= 1);
            Assert.True(File.Exists("Downloads/sample.txt"));
        }
    }
}