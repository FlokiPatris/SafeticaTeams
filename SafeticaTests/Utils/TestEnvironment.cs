using System.Runtime.CompilerServices;

namespace SafeticaTests.Utils
{
    public static class TestEnvironment
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

        public static void PrepareTestFolders(params string[] folderNames)
        {
            CustomLogger.Log("Preparing test folders...");
            foreach (var folder in folderNames)
            {
                var fullPath = GetProjectPath(folder);

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    CustomLogger.Log($"Created missing folder: {fullPath}");
                }
                else
                {
                    ClearFolder(fullPath);
                }
            }
            CustomLogger.Log("Test folders prepared.");
        }
    }
}