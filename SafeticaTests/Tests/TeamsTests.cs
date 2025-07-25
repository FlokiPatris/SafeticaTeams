using SafeticaTests.Fixtures;
using SafeticaTests.Pages;
using SafeticaTests.Utils;

namespace SafeticaTests.Tests
{
    [Collection("Teams Collection")]
    public class TeamsTests(TeamsFixture sessionFixture)
    {
        private readonly TeamsPage _teamsPage = sessionFixture.TeamsPage;
        private readonly string _projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));

        // [Fact]
        // public async Task UploadAndDownloadFile_ShouldSucceed()
        // {
        //     var testDataFolder = Path.Combine(_projectRoot, "SafeticaTests", "TestData");
        //     var downloadFolder = Path.Combine(_projectRoot, "SafeticaTests", "Downloads");

        //     string sampleFilePath = TestHelpers.CreateSampleFile(testDataFolder);
        //     string fileName = Path.GetFileName(sampleFilePath);

        //     await _teamsPage.UploadFileAsync(sampleFilePath);

        //     Directory.CreateDirectory(downloadFolder);
        //     var downloadedPath = await _teamsPage.DownloadLastFileAsync();
        //     var finalDownloadedPath = Path.Combine(downloadFolder, fileName);

        //     File.Move(downloadedPath, finalDownloadedPath, overwrite: true);

        //     Assert.True(File.Exists(finalDownloadedPath), $"❌ File not found: {finalDownloadedPath}");
        //     Assert.True(TestHelpers.FilesAreEqual(sampleFilePath, finalDownloadedPath), $"❌ File contents do not match.");
        // }

        [Fact]
        public async Task SendThreeMessages_ShouldCountCorrectly()
        {
            string prefix = $"{"Test_Fi_Sa"}{TestHelpers.GenerateRandomText()}";
            string msg1 = $"{prefix}-{TestHelpers.GenerateRandomText()}";
            string msg2 = $"{prefix}-{TestHelpers.GenerateRandomText()}";
            string msg3 = $"{prefix}-{TestHelpers.GenerateRandomText()}";

            await _teamsPage.SendMessageAndWaitAsync(msg1);
            await _teamsPage.SendMessageAndWaitAsync(msg2);
            await _teamsPage.SendMessageAndWaitAsync(msg3);

            int count = await _teamsPage.CountMessagesWithPrefixAsync(prefix);
            Assert.Equal(3, count);
        }
    }
}