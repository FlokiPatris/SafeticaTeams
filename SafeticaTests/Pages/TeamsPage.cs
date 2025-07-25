using Microsoft.Playwright;
using SafeticaTests.Utils;

namespace SafeticaTests.Pages
{
    public class TeamsPage(IPage page, string baseUrl)
    {
        private readonly IPage _page = page;
        private readonly string _baseUrl = baseUrl;

        public async Task LoginAsync(string username, string password)
        {
            CustomLogger.Log("[TeamsPage] Navigating to login page");
            await _page.GotoAsync(_baseUrl);

            CustomLogger.Log("[TeamsPage] Filling in credentials");
            await _page.FillAsync("input[type='email']", username);
            await _page.ClickAsync("input[type='submit']");
            await _page.FillAsync("input[type='password']", password);
            await _page.ClickAsync("input[type='submit']");

            CustomLogger.Log("[TeamsPage] Skipping 'Stay signed in' prompt");
            await _page.ClickAsync("input#idBtn_Back[value='No']");
        }

        public async Task LogoutAsync()
        {
            CustomLogger.Log("[TeamsPage] Logging out");
            await _page.GetByRole(AriaRole.Button, new() { Name = "Your profile, status Available" }).ClickAsync();
            await _page.GetByRole(AriaRole.Menuitem, new() { Name = "Sign out from" }).ClickAsync();
            await _page.GetByRole(AriaRole.Button, new() { Name = "Sign out" }).ClickAsync();
            await _page.WaitForURLAsync(url => url.Contains("login"), new() { Timeout = 10000 });
        }

        public async Task SendMessageAsync(string message)
        {
            CustomLogger.Log($"[TeamsPage] Sending message: \"{message}\"");

            var messageBox = _page.Locator("[contenteditable='true']");
            await messageBox.ClickAsync();
            await _page.Keyboard.InsertTextAsync(message);
            await _page.Keyboard.PressAsync("Enter", new () { Delay = 100 });
        }

        public async Task<int> CountMessagesWithPrefixAsync(string prefix)
        {
            CustomLogger.Log($"[TeamsPage] Counting messages with prefix: \"{prefix}\"");

            // XPath that targets only message bubbles containing the prefix
            var messages = _page.Locator($"xpath=//div[contains(text(), '{prefix}')]");
            int count = await messages.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var text = await messages.Nth(i).InnerTextAsync();
                CustomLogger.Log($"[TeamsPage] Matched message {i}: {text}");
            }

            CustomLogger.Log($"[TeamsPage] Found {count} messages with prefix \"{prefix}\"");
            return count;
        }

        public async Task UploadFileAsync(string filePath)
        {
            CustomLogger.Log($"[TeamsPage] Uploading file: {filePath}");

            await _page.ClickAsync("button[title='Actions and apps']");
            await _page.GetByText("Attach file", new() { Exact = true }).ClickAsync();

            var fileInput = _page.Locator("input[type='file']");
            await fileInput.SetInputFilesAsync(filePath);

            await _page.GetByRole(AriaRole.Button, new() { Name = "Anyone with the link can edit" })
                       .WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 15000 });
            await _page.Keyboard.PressAsync("Enter");

            string fileName = Path.GetFileName(filePath);
            await _page.WaitForSelectorAsync($"div:has-text('{fileName}')", new() { Timeout = 10000 });

            CustomLogger.Log($"[TeamsPage] File \"{fileName}\" uploaded successfully");
        }

        public async Task<string> DownloadFileByNameAsync(string fileName)
        {
            CustomLogger.Log($"[TeamsPage] Attempting to download file: {fileName}");

            var messageGroups = _page.GetByRole(AriaRole.Group);
            int count = await messageGroups.CountAsync();

            for (int i = count - 1; i >= 0; i--)
            {
                var group = messageGroups.Nth(i);
                if (await group.InnerTextAsync() is string text && text.Contains(fileName))
                {
                    CustomLogger.Log($"[TeamsPage] Found message group with file: {fileName}");
                    await group.GetByLabel("More attachment options").ClickAsync();

                    var download = await _page.RunAndWaitForDownloadAsync(async () =>
                    {
                        await _page.GetByRole(AriaRole.Menuitem, new() { Name = "Download" }).ClickAsync();
                    });

                    var path = await download.PathAsync();
                    CustomLogger.Log($"[TeamsPage] File downloaded to: {path}");
                    return path;
                }
            }

            throw new Exception($"No message group found containing file: {fileName}");
        }
    }
}