using System;

namespace EnderEngine
{
    public class Logger
    {
        /// <summary>
        /// Name of the log file (static, cause every logger will log into the same file - the prefix and message will differenciate them)
        /// </summary>
        private static readonly string LogsFileName = $"logs {DateTime.Now}".Replace(' ', '_').Replace('/', '-').Replace(':', '-') + ".log"; //TODO: Change extension handling with proper FS
        /// <summary>
        /// Path to the log file
        /// </summary>
        private string LogsFilePath;
        /// <summary>
        /// If this is true, when logging the date and time will be added in the prefix, {}
        /// </summary>
        public static bool DoWriteDateAndTime = true;

        public Logger()
        {
            LogsFilePath = "mmm/" + LogsFileName;
            //TODO: Check for file created (use File System later, to make that check simpler, use event system to do it only once at startup)
            if (System.IO.File.Exists(LogsFilePath))
                return;
            System.IO.File.Create(LogsFilePath).Close();
        }

        public static void EnableDateAndTimeWriting(bool enable)
        {
            DoWriteDateAndTime = enable;
        }
        internal string GetLogPrefix(LogLevel logLevel)
        {
            return "[EnderEngine] " + (DoWriteDateAndTime ? "{" + DateTime.Now + "} " : "") + $"[{logLevelsStringArray[(int)logLevel]}]";
        }

        /// <summary>
        ///  Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        public void Log(string message, LogLevel logLevel, LogMethod method = LogMethod.TO_FILE_AND_CONSOLE)
        {
            string prefix = GetLogPrefix(logLevel);
            if ((int)method < 2) //If we want to log in the Console (LogMethod.TO_CONSOLE or TO_CONSOLE_AND_FILE)
                Console.WriteLine($"{prefix}: {message}");
            if (method > 0) //If we want to log into the log file (LogMethod.TO_CONSOLE_AND_FILE or TO_FILE)
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(this.LogsFilePath))
                {
                    text.WriteLine($"{prefix}: {message}");
                }
            }
        }

        /// <summary>
        /// The log level to be defined, how critical the log is
        /// </summary>
        public enum LogLevel
        {
            FATAL = 0,
            ERROR,
            WARN,
            DEBUG,
            INFO
        }

        /// <summary>
        /// stores the LogLevel strings and can be picked from the LogLevel int index
        /// </summary>
        private readonly string[] logLevelsStringArray =
        {
            "FATAL",
            "Error",
            "Warn",
            "Debug",
            "Info"
        };

        /// <summary>
        /// The log method is how the message will be logged
        /// </summary>
        public enum LogMethod
        {
            TO_CONSOLE = 0,
            TO_FILE_AND_CONSOLE = 1,
            TO_FILE = 2
        }
    }
}