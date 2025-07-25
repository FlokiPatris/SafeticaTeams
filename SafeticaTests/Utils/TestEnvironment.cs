
namespace SafeticaTests.Utils
{
    public static class TestEnvironment
    {
        /// <summary>
        /// Deletes all files and subdirectories in the specified folder.
        /// </summary>
        public static void ClearFolder(string path)
        {
            if (!Directory.Exists(path)) return;

            foreach (var file in Directory.GetFiles(path))
                File.Delete(file);

            foreach (var dir in Directory.GetDirectories(path))
                Directory.Delete(dir, recursive: true);
        }

        /// <summary>
        /// Resolves the full path to the project root and appends subpaths.
        /// </summary>
        public static string GetProjectPath(params string[] subPaths)
        {
            var root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            return Path.Combine([root, .. subPaths]);
        }

        /// <summary>
        /// Clears multiple test folders under the project root.
        /// </summary>
        public static void PrepareTestFolders(params string[] folderNames)
        {
            foreach (var folder in folderNames)
            {
                var fullPath = GetProjectPath(folder);
                ClearFolder(fullPath);
            }
        }
    }
}