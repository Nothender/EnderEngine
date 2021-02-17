using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine.Core;
using Pastel;
using System.IO;

namespace EnderEngine
{
    public class Logger
    {
        /// <summary>
        /// Name of the log file (static, cause every logger will log into the same file - the prefix and message will differenciate them)
        /// </summary>
        public static readonly string LogsFileName = $"logs {DateTime.Now}.log".Replace(' ', '_').Replace('/', '-').Replace(':', '-'); //TODO: Change extension handling with proper File System
        /// <summary>
        /// Path to the log file
        /// </summary>
        public static readonly string LogsFilePath = "Logs\\" + LogsFileName; //Will be changed with File System handler
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
        public static LogMethod DefaultLogMethod = LogMethod.TO_FILE_AND_CONSOLE;

        /// <summary>
        /// The name written at the beginning of the prefix (ex in the enderEngine logger : [EnderEngine] {date} [LogLevel])
        /// </summary>
        public string NamePrefix;
        
        static Logger()
        {
            System.IO.Directory.CreateDirectory("Logs/");
            if (System.IO.File.Exists(LogsFilePath))
                return;
            System.IO.File.Create(LogsFilePath).Close();
        }

        public Logger(string namePrefix)
        {
            NamePrefix = namePrefix;
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
            DefaultLogMethod = logMethod;
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
        /// <param name="doColoring">If the string returned will be colored</param>
        /// <returns>A string containing the logging prefix</returns>
        internal string GetLogPrefix(LogLevel logLevel, bool doColoring = true)
        {
            if (doColoring)
                return $"[{NamePrefix}] " + (DoWriteDateAndTime ? ("{" + DateTime.Now + "} ").Pastel(System.Drawing.Color.Gray) : "") + $"[{logLevelsStringArray[(int)logLevel]}]".Pastel(logLevelsHexColorCodes[(int)logLevel]);
            else
                return $"[{NamePrefix}] " + (DoWriteDateAndTime ? ("{" + DateTime.Now + "} ") : "") + $"[{logLevelsStringArray[(int)logLevel]}]";
        }

        /// <summary>
        /// Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        /// <param name="message">The string representing the message you want to log</param>
        /// <param name="logLevel">The level of the log (ex : "[Error]", if you are trying to log an error that occured)</param>
        /// <param name="method">Where will the log be written (Console, LogFile, or both). If null the method will be defaultLogMethod (static value)</param>
        public void Log(string message, LogLevel logLevel, LogMethod? method = null)
        {
            if (IgnoredLogLevels.Contains(logLevel))
                return;
            if (method == null)
                method = DefaultLogMethod;
            if ((int)method < 2) //If we want to log in the Console (LogMethod.TO_CONSOLE or TO_CONSOLE_AND_FILE)
                Console.WriteLine($"{GetLogPrefix(logLevel)}: { message}");
            if (method > 0) //If we want to log into the log file (LogMethod.TO_CONSOLE_AND_FILE or TO_FILE)
            {
                Task.Run(() =>
                {
                    lock(logQueue)
                        logQueue.Add($"{GetLogPrefix(logLevel, false)}: { message}");
                    
                    if (!isLogQueueOpened && logQueue.Count > maxQueueSize / 2)
                        WriteLogQueueToFile().ConfigureAwait(false);
                });
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

        // Logging to file very badly optimized, TODO : fix

        private static readonly int maxQueueSize = 10;
        private static List<string> logQueue = new List<string>(maxQueueSize);
        private static bool isLogQueueOpened = false;

        private static async Task WriteLogQueueToFile()
        {
            isLogQueueOpened = true;
            await Task.Run(() => 
            {
                do
                {
                    lock (logQueue)
                    {
                        File.AppendAllLines(LogsFilePath, logQueue);
                        logQueue.Clear();
                    }
                    Task.Delay(50);
                } while (logQueue.Count() != 0);
            });
            isLogQueueOpened = false;
        }

    }
}