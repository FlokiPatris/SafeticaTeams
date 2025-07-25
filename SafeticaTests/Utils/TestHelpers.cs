
namespace SafeticaTests.Utils
{
    public static class TestHelpers
    {
        /// <summary>
        /// Generates a random alphanumeric string of the specified length.
        /// </summary>
        public static string GenerateRandomText(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        /// <summary>
        /// Creates a sample .txt file with random content in the specified folder.
        /// </summary>
        public static string CreateSampleFile(string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            string fileName = $"sample_{GenerateRandomText(8)}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            string content = $"This is a sample txt file with random content: {GenerateRandomText(20)}";
            File.WriteAllText(filePath, content);

            return filePath;
        }
    }
}