using SafeticaTests.Pages;


namespace SafeticaTests.Tests
{
    public class TeamsTests(PlaywrightFixture fixture) : IClassFixture<PlaywrightFixture>
    {
        private readonly PlaywrightFixture _fixture = fixture;

        [Fact]
        public async Task SendMessagesAndHandleFile()
        {
            var teamsPage = new TeamsPage(_fixture.Page, _fixture.Config.BaseUrl);

            await teamsPage.LoginAsync(_fixture.Config.Login, _fixture.Config.Password);

            // Send three messages
            await teamsPage.SendMessageAsync("aaaa");
            await teamsPage.SendMessageAsync("bbbbb");
            await teamsPage.SendMessageAsync("ccc");

            // Upload a file
            await teamsPage.UploadFileAsync("TestData/sample.txt");
            await Task.Delay(3000); // Wait for upload to complete

            // Download the last file
            await teamsPage.DownloadLastFileAsync("Downloads");

            // Take a screenshot
            await _fixture.Page.ScreenshotAsync(new() { Path = "TeamsTest.png" });

            // Assert message count
            var count = await teamsPage.CountMessagesAsync("Hello");
            Assert.True(count >= 1);

            // Assert file download
            Assert.True(File.Exists("Downloads/sample.txt"));
        }
    }
}