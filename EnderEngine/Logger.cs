using System;

namespace EnderEngine
{
    public abstract class LogBase
    {
        public abstract void Log(string Message, Logger.LogLevel Flag, Logger.LogMethod method = Logger.LogMethod.TO_FILE);
    }

    public class Logger : LogBase
    {
        public enum LogLevel
        {
            FATAL,
            ERROR,
            WARN,
            DEBUG,
            INFO
        }

        public enum LogMethod
        {
            TO_CONSOLE,
            TO_FILE,
            TO_FILE_AND_CONSOLE
        }
        /// <summary>
        /// Name of the log file 
        /// </summary>
        private string LogFileName { get; set; }
        /// <summary>
        /// Path of the log file
        /// </summary>
        private string LogFilePath { get; set; }


        public Logger()
        {
            this.LogFileName = "Log.txt";
            this.LogFilePath = "Logs/" + this.LogFileName;
        }

        /// <summary>
        ///  Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        public override void Log(string Message, LogLevel Flag ,LogMethod method = LogMethod.TO_FILE_AND_CONSOLE)
        {
            string prefix = "";
            switch(Flag)  // get the actual Flag that we will use in our log message. We'll also get the color for the message displayed in console logs
            {
                case LogLevel.FATAL:
                    prefix = "Fatal";
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.ERROR:
                    prefix = "Error";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.WARN:
                    prefix = "Warn";
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.DEBUG:
                    prefix = "Debug";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.INFO:
                    prefix = "Info";
                    break;
            }

            // checks for the Logging method that you chose
            if (method == LogMethod.TO_CONSOLE)
            {
                Console.WriteLine($"[{prefix}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
            }

            if (method == LogMethod.TO_FILE)
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(this.LogFilePath))
                {
                    text.WriteLine($"[{prefix}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
                }
            }
            else
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(this.LogFilePath))
                {
                    text.WriteLine($"[{prefix}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
                }
                Console.WriteLine($"[{prefix}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
            }
        }

        
    }
}
