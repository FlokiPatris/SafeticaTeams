using Microsoft.Playwright;

namespace SafeticaTests.Pages
{
    public class TeamsPage(IPage page, string baseUrl)
    {
        private readonly IPage _page = page;
        private readonly string _baseUrl = baseUrl;

        public async Task LoginAsync(string username, string password)
        {
            await _page.GotoAsync(_baseUrl);

            // Example login flow — adjust selectors as needed
            await _page.FillAsync("input[type='email']", username);
            await _page.ClickAsync("input[type='submit']");
            await _page.FillAsync("input[type='password']", password);
            await _page.ClickAsync("input[type='submit']");

            // Wait for Teams to load — adjust selector as needed
            await _page.WaitForSelectorAsync("div[data-tid='team-channel-list']");
        }

        public async Task SendMessageAsync(string message)
        {
            await _page.FillAsync("div[data-tid='new-message-textarea']", message);
            await _page.PressAsync("div[data-tid='new-message-textarea']", "Enter");
        }

        public async Task UploadFileAsync(string filePath)
        {
            await _page.SetInputFilesAsync("input[type='file']", filePath);
        }

        public async Task DownloadLastFileAsync(string downloadFolder)
        {
            var download = await _page.RunAndWaitForDownloadAsync(async () =>
            {
                await _page.ClickAsync("div[data-tid='download-button']");
            });

            await download.SaveAsAsync(Path.Combine(downloadFolder, download.SuggestedFilename));
        }

        public async Task<int> CountMessagesAsync(string keyword)
        {
            var messages = await _page.QuerySelectorAllAsync("div[data-tid='message-body']");
            int count = 0;

            foreach (var message in messages)
            {
                var text = await message.InnerTextAsync();
                if (text.Contains(keyword))
                    count++;
            }

            return count;
        }
    }
}