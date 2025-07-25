using SafeticaTests.Utils;

namespace SafeticaTests.Helpers
{
    public static class TestHelpers
    {
        public static string GenerateRandomText(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string([.. Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)])]);

            CustomLogger.Log($"Generated random text: {result}", CustomLogger.LogLevel.Debug);
            return result;
        }

        public static string CreateSampleFile(string folderPath)
        {
            string fileName = $"sample_{GenerateRandomText(8)}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            string content = $"This is a sample txt file with random content: {GenerateRandomText(100)}";
            File.WriteAllText(filePath, content);

            CustomLogger.Log($"Sample file created: {filePath}", CustomLogger.LogLevel.Info);
            return filePath;
        }
    }
}