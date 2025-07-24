using SafeticaTests.Pages;

namespace SafeticaTests.Tests
{
    public class TeamsTests(PlaywrightFixture fixture) : IClassFixture<PlaywrightFixture>
    {
        private readonly PlaywrightFixture _fixture = fixture;

        [Fact]
        public async Task SendMessagesAndHandleFile()
        {
            var teamsPage = new TeamsPage(_fixture.Page);
            await teamsPage.LoginAsync(_fixture.Config.Login, _fixture.Config.Password);
            await Task.Delay(30000);

            await teamsPage.SendMessageAsync("aaaa");
            await teamsPage.SendMessageAsync("bbbbb");
            await teamsPage.SendMessageAsync("ccc");

            await teamsPage.UploadFileAsync("TestData/sample.txt");
            await Task.Delay(3000);

            await teamsPage.DownloadLastFileAsync("Downloads");
            await _fixture.Page.ScreenshotAsync(new() { Path = "TeamsTest.png" });

            var count = await teamsPage.CountMessagesAsync("Hello");
            Assert.True(count >= 1);
            Assert.True(File.Exists("Downloads/sample.txt"));
        }
    }
}