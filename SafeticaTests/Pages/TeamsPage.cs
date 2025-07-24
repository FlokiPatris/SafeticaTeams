using Microsoft.Playwright;

namespace SafeticaTeamsPlaywright.Pages
{
    public class TeamsPage
    {
        private readonly IPage _page;

        public TeamsPage(IPage page) => _page = page;

        public async Task LoginAsync(string username, string password)
        {
            await _page.GotoAsync("https://teams.microsoft.com/v2/");
            await _page.FillAsync("input[type='email']", username);
            await _page.ClickAsync("input[type='submit']");
            await _page.FillAsync("input[type='password']", password);
            await _page.ClickAsync("input[type='submit']");

            try
            {
                var noButton = _page.Locator("input#idBtn_Back[value='No']");
                await noButton.WaitForAsync(new() { Timeout = 5000 });
                await noButton.ClickAsync();
            }
            catch (TimeoutException)
            {
                Console.WriteLine("⚠️ 'No' button not found — skipping.");
            }

            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        public async Task SendMessageAsync(string message)
        {
            var messageBox = _page.Locator("[contenteditable='true']");
            await messageBox.FillAsync(message);
            await _page.Keyboard.PressAsync("Enter");
        }

        public async Task<int> CountMessagesAsync(string text)
        {
            var messages = _page.Locator($"div:has-text('{text}')");
            return await messages.CountAsync();
        }

        public async Task UploadFileAsync(string filePath)
        {
            var fileInput = _page.Locator("input[type='file']");
            await fileInput.SetInputFilesAsync(filePath);
        }

        public async Task DownloadLastFileAsync(string downloadPath)
        {
            var download = await _page.RunAndWaitForDownloadAsync(async () =>
            {
                await _page.ClickAsync("div:has-text('Download')"); // Adjust selector as needed
            });

            var fileName = Path.Combine(downloadPath, download.SuggestedFilename);
            await download.SaveAsAsync(fileName);
        }
    }
}