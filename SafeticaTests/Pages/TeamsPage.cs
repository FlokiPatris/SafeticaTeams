using Microsoft.Playwright;

namespace SafeticaTests.Pages
{
    public class TeamsPage(IPage page, string baseUrl)
    {
        private readonly IPage _page = page;
        private readonly string _baseUrl = baseUrl;

        private const string MessageXPath = "//div[@data-tid='messageBodyContent']";

        public async Task LoginAsync(string username, string password)
        {
            await _page.GotoAsync(_baseUrl);

            await _page.FillAsync("input[type='email']", username);
            await _page.ClickAsync("input[type='submit']");
            await _page.FillAsync("input[type='password']", password);
            await _page.ClickAsync("input[type='submit']");

            await _page.ClickAsync("input#idBtn_Back[value='No']");
        }

        public async Task LogoutAsync()
        {
            await _page.GetByRole(AriaRole.Button, new() { Name = "Your profile, status Available" }).ClickAsync();
            await _page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out from" }).ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Sign out" }).ClickAsync();
            await _page.WaitForURLAsync(url => url.Contains("login"), new() { Timeout = 10000 });
        }

        public async Task SendMessageAndWaitAsync(string message)
        {
            var messageBox = _page.Locator("[contenteditable='true']");
            await messageBox.ClickAsync(new() { Force = true });
            await messageBox.FillAsync(message);
            await _page.Keyboard.PressAsync("Enter");

            Console.WriteLine($"Waiting for message containing \"{message}\" to appear");

            var messageLocator = _page.Locator(MessageXPath).Filter(new() { HasTextString = message });
            await messageLocator.First.WaitForAsync();
        }

        public async Task<int> CountMessagesWithPrefixAsync(string prefix)
        {
            var messages = _page.Locator(MessageXPath);
            var count = 0;

            var total = await messages.CountAsync();
            for (int i = 0; i < total; i++)
            {
                var text = await messages.Nth(i).InnerTextAsync();
                if (text.StartsWith(prefix))
                    count++;
            }

            return count;
        }

        public async Task UploadFileAsync(string filePath)
        {
            await _page.ClickAsync("button[title='Actions and apps']");
            await _page.GetByText("Attach file", new() { Exact = true }).ClickAsync();

            var fileInput = _page.Locator("input[type='file']");
            await fileInput.SetInputFilesAsync(filePath);

            await _page.GetByRole(AriaRole.Button, new() { Name = "Anyone with the link can edit" })
                       .WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15000 });
            await _page.Keyboard.PressAsync("Enter");

            string fileName = Path.GetFileName(filePath);
            await _page.WaitForSelectorAsync($"div:has-text('{fileName}')", new() { Timeout = 10000 });
        }

        public async Task<string> DownloadLastFileAsync()
        {
            var messageGroup = _page.GetByRole(AriaRole.Group, new() { Name = "Safetica QA Sent The message" });
            await messageGroup.GetByLabel("More attachment options").ClickAsync();

            var download = await _page.RunAndWaitForDownloadAsync(async () =>
            {
                await _page.GetByRole(AriaRole.Menuitem, new() { Name = "Download" }).ClickAsync();
            });

            return await download.PathAsync();
        }
    }
}