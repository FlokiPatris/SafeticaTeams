using SafeticaTests.Fixtures;
using SafeticaTests.Assertions;
using SafeticaTests.Utils;
using SafeticaTests.Helpers;
using Microsoft.VisualBasic;

namespace SafeticaTests.Tests
{
    [Collection("Teams Collection")] 
    public class TeamsTests(TeamsFixture fixture)
    {
        [Fact]
        public async Task UploadAndDownloadFile_ShouldSucceed()
        {
            string sampleFilePath = FileHelpers.CreateSampleFile(fixture.TestDataFolder);
            string fileName = Path.GetFileName(sampleFilePath);

            await fixture.TeamsPage.UploadFileAsync(sampleFilePath);

            var downloadedPath = await fixture.TeamsPage.DownloadFileByNameAsync(fileName);
            var finalDownloadedPath = Path.Combine(fixture.DownloadFolder, fileName);

            File.Move(downloadedPath, finalDownloadedPath, overwrite: true);

            Assert.True(File.Exists(finalDownloadedPath));
            Assert.True(TestAssertions.FilesAreEqual(sampleFilePath, finalDownloadedPath));
        }

        [Fact]
        public async Task SendThreeMessages_ShouldCountCorrectly()
        {
            string prefix = $"Test_FlokiSafetica_{TestHelpers.GenerateRandomText(SharedConstants.MessagePrefixLength)}";
            var messages = new List<string>
            {
                $"{prefix}: {TestHelpers.GenerateRandomText()}",
                $"{prefix}: {TestHelpers.GenerateRandomText()}",
                $"{prefix}: {TestHelpers.GenerateRandomText()}"
            };

            foreach (var message in messages)
            {
                await fixture.TeamsPage.SendMessageAsync(message);
            }

            int count = await fixture.TeamsPage.CountMessagesWithPrefixAsync(prefix);
            Assert.Equal(messages.Count, count);
        }
    }
}