using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine.Core;
using Pastel;

namespace EnderEngine.Logging
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Name of the log file (static, cause every logger will log into the same file - the prefix and message will differenciate them)
        /// </summary>
        public static readonly string FileName = $"logs {DateTime.Now}.log".Replace(' ', '_').Replace('/', '-').Replace(':', '-'); //TODO: Change extension handling with proper File System
        /// <summary>
        /// Path to the log file
        /// </summary>
        public static string FilePath = "Logs/" + FileName;
        /// <summary>
        /// The default logging method (Console, File or both), any LogMethod given in the parameters of the Log method will override this setting
        /// </summary>
        public static LogMethod DefaultLogMethod = LogMethod.TO_FILE_AND_CONSOLE;

        public string Path { get; set; }
        public string NamePrefix { get; set; }

        public Logger(string namePrefix, string path = null)
        {
            NamePrefix = namePrefix;
            Path = path ?? FilePath;
            
            FileInfo fi = new FileInfo(Path);
            Directory.CreateDirectory(fi.DirectoryName);
            if (!fi.Exists) fi.Create().Close();
        }

        /// <summary>
        /// Returns the logging prefix (ex : "[EnderEngine] [MainMethod] {Date} [Info]: Log example")
        /// </summary>
        /// <param name="logLevel">The logLevel corresponding to the log, concatenates at the end "[LoggerPrefixName] [LoggingMethod] {Date} [LogLevel]</param>
        /// <param name="doColoring">If the string returned will be colored</param>
        /// <returns>A string containing the logging prefix</returns>
        internal string GetLogPrefix(LogLevel logLevel, bool doColoring = true)
        {
            if (doColoring)
                return $"[{NamePrefix}] [{new StackFrame(2).GetMethod().Name}] " + (ILogger.DoWriteDateAndTime ? ("{" + DateTime.Now + "} ").Pastel(System.Drawing.Color.Gray) : "") + $"[{logLevelsStrings[(int)logLevel]}]".Pastel(logLevelsColors[(int)logLevel]);
            else
                return $"[{NamePrefix}] [{new StackFrame(2).GetMethod().Name}]" + (ILogger.DoWriteDateAndTime ? ("{" + DateTime.Now + "} ") : "") + $"[{logLevelsStrings[(int)logLevel]}]";
        }

        /// <summary>
        /// Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        /// <param name="message">The string representing the message you want to log</param>
        /// <param name="logLevel">The level of the log (ex : "[Error]", if you are trying to log an error that occured)</param>
        /// <param name="method">Where will the log be written (Console, LogFile, or both). If null the method will be defaultLogMethod (static value)</param>
        public void Log(string message, LogLevel logLevel)
        {
            if (ILogger.IgnoredLogLevels.Contains(logLevel))
                return;
            if ((int)DefaultLogMethod < 2) //If we want to log in the Console (LogMethod.TO_CONSOLE or TO_CONSOLE_AND_FILE)
                Console.WriteLine($"{GetLogPrefix(logLevel)}: { message}");
            if (DefaultLogMethod > 0) //If we want to log into the log file (LogMethod.TO_CONSOLE_AND_FILE or TO_FILE)
            {
                Task.Run(() =>
                {
                    logQueue.Enqueue($"{GetLogPrefix(logLevel, false)}: { message}");

                    if (!isLogQueueOpened)
                        WriteLogQueueToFile().ConfigureAwait(false);
                }).ConfigureAwait(true);
            }
        }

        /// <summary>
        /// stores the LogLevel strings and can be picked from the LogLevel int index
        /// </summary>
        private readonly string[] logLevelsStrings =
        {
            "FATAL",
            "Error",
            "Warn",
            "Info",
            "SubInfo",
            "Debug"
        };

        private readonly System.Drawing.Color[] logLevelsColors =
        {
            System.Drawing.Color.DarkRed,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Green,
            System.Drawing.Color.RoyalBlue,
            System.Drawing.Color.Purple
        };

        // Logging to file very badly optimized, TODO : fix

        private static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string>();
        private static bool isLogQueueOpened = false;

        private static async Task WriteLogQueueToFile()
        {
            if (isLogQueueOpened)
                return;
            isLogQueueOpened = true;
            await Task.Run(() =>
            {
                //string logLines = "";
                do
                {
                    string logLine;
                    using (StreamWriter writer = new StreamWriter(FilePath, true))
                    {
                        while (logQueue.TryDequeue(out logLine))
                            writer.WriteLine(logLine);
                        //logLines += $"{logLine}\n";
                    }

                    //File.AppendAllText(LogsFilePath, logLines);
                    Task.Delay(100);
                } while (!logQueue.IsEmpty);

            });
            isLogQueueOpened = false;
        }

    }
}
