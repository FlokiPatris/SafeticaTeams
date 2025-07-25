namespace SafeticaTests.Assertions
{
    public static class TestAssertions
    {
        /// <summary>
        /// Compares two files byte-by-byte to check if they are identical.
        /// </summary>
        public static bool FilesAreEqual(string file1, string file2)
        {
            byte[] f1 = File.ReadAllBytes(file1);
            byte[] f2 = File.ReadAllBytes(file2);
            return f1.SequenceEqual(f2);
        }
    }
}