

namespace SafeticaTests.Utils
{
    public static class CustomLogger
    {
        private static readonly object _lock = new();
        private static string? _logFilePath;
        private static bool _isInitialized = false;

        public static void Initialize(string logFolder)
        {
            if (_isInitialized) return;

            if (!Directory.Exists(logFolder))
                throw new DirectoryNotFoundException($"Log folder does not exist: {logFolder}");

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            _logFilePath = Path.Combine(logFolder, $"testlog_{timestamp}.log");
            _isInitialized = true;

            Log("Logger initialized.");
        }

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (_logFilePath is null)
                throw new InvalidOperationException("Logger not initialized. Call Logger.Initialize(logFolder) first.");

            string formatted = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            lock (_lock)
            {
                Console.WriteLine(formatted);
                File.AppendAllText(_logFilePath, formatted + Environment.NewLine);
            }
        }

        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Debug
        }
    }
}