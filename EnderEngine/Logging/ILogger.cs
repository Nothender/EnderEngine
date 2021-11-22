using EnderEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnderEngine.Logging
{
    public interface ILogger
    {

        /// <summary>
        /// If this is true, when logging the date and time will be added in the prefix, {}
        /// </summary>
        public static bool DoWriteDateAndTime = true;

        /// <summary>
        /// Log levels that will be ignored
        /// </summary>
        protected static LogLevel[] IgnoredLogLevels = { };

        /// <summary>
        /// Ignores the given logLevel (won't write to console or file logs with that level)
        /// </summary>
        /// <param name="logLevel">The logLevel that you want to Ignore</param>
        public static void IgnoreLogLevel(LogLevel logLevel)
        {
            if (IgnoredLogLevels.Contains(logLevel))
                return;
            IgnoredLogLevels = IgnoredLogLevels.Append(logLevel);
        }
        /// <summary>
        /// UnIgnores the given logLevel (will write again to console or file logs with that level)
        /// </summary>
        /// <param name="logLevel">The logLevel that you want to UnIgnore</param>
        public static void UnIgnoreLogLevel(LogLevel logLevel)
        {
            if (!IgnoredLogLevels.Contains(logLevel))
                return;
            IgnoredLogLevels = IgnoredLogLevels.Remove(logLevel);
        }


        public string Path { get; set; }
        /// <summary>
        /// The name written at the beginning of the prefix (ex in the enderEngine logger : [EnderEngine] {date} [LogLevel])
        /// </summary>
        public string NamePrefix { get; set; }

        public void Log(string message, LogLevel level);

    }
}
