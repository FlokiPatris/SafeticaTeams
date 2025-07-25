using SafeticaTests.Utils;

namespace SafeticaTests.Helpers
{
    public static class TestHelpers
    {
        /// <summary>
        /// Generates a random string of specified length using the provided character set.
        /// </summary>
        /// <param name="length">Length of the generated string. Default is 20.</param>
        /// <param name="chars">Character set to use. Default is alphanumeric.</param>
        /// <param name="random">Optional Random instance for deterministic output in tests.</param>
        /// <returns>Randomly generated string.</returns>
        public static string GenerateRandomText(
            int length = 20,
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            Random? random = null)
        {
            random ??= new Random();

            var result = new string([.. Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)])]);

            CustomLogger.Log($"Generated random text: {result}", CustomLogger.LogLevel.Debug);
            return result;
        }
    }
}