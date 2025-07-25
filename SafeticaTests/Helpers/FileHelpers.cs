using System.Runtime.CompilerServices;
using SafeticaTests.Utils;
using SafeticaTests.Helpers;

namespace SafeticaTests.Helpers
{
    public static class FileHelpers
    {
        public static void ClearFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                CustomLogger.Log($"Folder not found, skipping clear: {path}", CustomLogger.LogLevel.Warning);
                return;
            }

            CustomLogger.Log($"Clearing folder: {path}");
            foreach (var file in Directory.GetFiles(path))
                File.Delete(file);

            foreach (var dir in Directory.GetDirectories(path))
                Directory.Delete(dir, recursive: true);

            CustomLogger.Log($"Folder cleared: {path}");
        }

        public static string GetProjectPath(params string[] subPaths)
        {
            var root = GetProjectRoot();
            return Path.Combine([root, .. subPaths]);
        }

        private static string GetProjectRoot([CallerFilePath] string callerPath = "")
        {
            var envRoot = Environment.GetEnvironmentVariable("SAFETICA_PROJECT_ROOT");
            if (!string.IsNullOrWhiteSpace(envRoot) && Directory.Exists(envRoot))
            {
                return envRoot;
            }

            var callerDir = Path.GetDirectoryName(callerPath);
            if (string.IsNullOrWhiteSpace(callerDir))
                throw new InvalidOperationException("Unable to determine caller directory.");

            var detectedRoot = Path.GetFullPath(Path.Combine(callerDir, ".."));
            return detectedRoot;
        }

        /// <summary>
        /// Ensures that the specified folders exist inside the SafeticaTests folder.
        /// Creates them if missing. No logging is performed here to avoid dependency on logger initialization.
        /// </summary>
        public static void EnsureFoldersExist(params string[] folderNames)
        {
            foreach (var folder in folderNames)
            {
                var fullPath = GetProjectPath("SafeticaTests", folder);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
            }
        }

        /// <summary>
        /// Clears the contents of the specified folders. Assumes logger is already initialized.
        /// </summary>
        public static void ClearFolders(params string[] folderNames)
        {
            CustomLogger.Log("Clearing test folders...");
            foreach (var folder in folderNames)
            {
                var fullPath = GetProjectPath(folder);
                ClearFolder(fullPath);
            }
            CustomLogger.Log("Test folders cleared.");
        }

        
        public static string CreateSampleFile(string folderPath)
        {
            string fileName = $"sample_{TestHelpers.GenerateRandomText(8)}.txt";
            string filePath = Path.Combine(folderPath, fileName);

            string content = $"This is a sample txt file with random content: {TestHelpers.GenerateRandomText(100)}";
            File.WriteAllText(filePath, content);

            CustomLogger.Log($"Sample file created: {filePath}", CustomLogger.LogLevel.Info);
            return filePath;
        }
    }
}