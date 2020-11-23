using System;
using System.Linq;
using EnderEngine.Core;
using Pastel;

namespace EnderEngine
{
    public class Logger
    {
        /// <summary>
        /// Name of the log file (static, cause every logger will log into the same file - the prefix and message will differenciate them)
        /// </summary>
        private static readonly string LogsFileName = $"logs {DateTime.Now}".Replace(' ', '_').Replace('/', '-').Replace(':', '-') + ".log"; //TODO: Change extension handling with proper File System
        /// <summary>
        /// Path to the log file
        /// </summary>
        private static readonly string LogsFilePath = "Logs/" + LogsFileName; //Will be changed with File System handler
        /// <summary>
        /// The log levels that the logging will be ignored of
        /// </summary>
        private static LogLevel[] IgnoredLogLevels = { };

        /// <summary>
        /// If this is true, when logging the date and time will be added in the prefix, {}
        /// </summary>
        public static bool DoWriteDateAndTime = true;
        /// <summary>
        /// The default logging method (Console, File or both), any LogMethod given in the parameters of the Log method will override this setting
        /// </summary>
        public static LogMethod defaultLogMethod = LogMethod.TO_FILE_AND_CONSOLE;

        /// <summary>
        /// The name written at the beginning of the prefix (ex in the enderEngine logger : [EnderEngine] {date} [LogLevel])
        /// </summary>
        public string NamePrefix;
        
        public Logger(string namePrefix)
        {
            NamePrefix = namePrefix;
            //TODO: Check for file created (use File System later, to make that check simpler, use event system to do it only once at startup)
            System.IO.Directory.CreateDirectory("Logs/");
            if (System.IO.File.Exists(LogsFilePath))
                return;
            System.IO.File.Create(LogsFilePath).Close();
        }

        /// <summary>
        /// Toggles whether or not the date will be written in the log prefix
        /// </summary>
        /// <param name="enable"></param>
        public static void EnableDateAndTimeWriting(bool enable)
        {
            DoWriteDateAndTime = enable;
        }
        /// <summary>
        /// Sets the defaultLogMethod to the value given in parameter
        /// </summary>
        public static void SetDefaultLoggingMethod(LogMethod logMethod = LogMethod.TO_FILE_AND_CONSOLE)
        {
            defaultLogMethod = logMethod;
        }
        /// <summary>
        /// Ignores the given logLevel (won't log a message depending on its logLevel, either in the console or the log file)
        /// </summary>
        /// <param name="logLevel">The logLevel that you wan't to ignore the logging of</param>
        public static void IgnoreLogLevel(LogLevel logLevel)
        {
            if (IgnoredLogLevels.Contains(logLevel))
                return;
            IgnoredLogLevels = IgnoredLogLevels.Append(logLevel);
        }

        /// <summary>
        /// Returns the logging prefix (ex : "[EnderEngine] {Date} [Info]:")
        /// </summary>
        /// <param name="logLevel">The logLevel corresponding to the log, concatenates at the end "[LoggerPrefixName] {Date} [LogLevel]</param>
        /// <returns>A string containing the logging prefix</returns>
        internal string GetLogPrefix(LogLevel logLevel)
        {
            return $"[{NamePrefix}] " + (DoWriteDateAndTime ? ("{" + DateTime.Now + "} ").Pastel(System.Drawing.Color.Gray) : "") + $"[{logLevelsStringArray[(int)logLevel]}]".Pastel(logLevelsHexColorCodes[(int) logLevel]);
        }

        /// <summary>
        ///  Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        /// <param name="message">The string representing the message you want to log</param>
        /// <param name="logLevel">The level of the log (ex : "[Error]", if you are trying to log an error that occured)</param>
        /// <param name="method">Where will the log be written (Console, LogFile, or both). If null the method will be defaultLogMethod (static value)</param>
        public void Log(string message, LogLevel logLevel, LogMethod? method = null)
        {
            if (IgnoredLogLevels.Contains(logLevel))
                return;
            if (method == null)
                method = defaultLogMethod;
            string prefix = GetLogPrefix(logLevel);
            if ((int)method < 2) //If we want to log in the Console (LogMethod.TO_CONSOLE or TO_CONSOLE_AND_FILE)
                Console.WriteLine($"{prefix}: {message}");
            if (method > 0) //If we want to log into the log file (LogMethod.TO_CONSOLE_AND_FILE or TO_FILE)
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(LogsFilePath)) //Write in file using the File I/O System
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

        private readonly System.Drawing.Color[] logLevelsHexColorCodes =
        {
            System.Drawing.Color.DarkRed,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Purple,
            System.Drawing.Color.Green
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