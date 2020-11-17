using System;

namespace EnderEngine
{
    public abstract class LogBase
    {
        public abstract void Log(string Message, Logger.LogLevel Flag, Logger.LogMethod method = Logger.LogMethod.ToFile);
    }

    public class Logger : LogBase
    {
        public enum LogLevel
        {
            Fatal,
            Error,
            Warn,
            Debug,
            Info
        }

        public enum LogMethod
        {
            ToConsole,
            ToFile,
            ToFileAndConsole
        }

        private string LogName { get; set; } // get and set the Name of the log file 
        private string LogPath { get; set; } // get and set the Path of the log file


        public Logger()
        {
            this.LogName = "Log.txt";
            this.LogPath = "Logs/" + this.LogName;
        }

        /// <summary>
        ///  Enables to log a given string into a file/console. By default, the method will log into a file and into the console
        /// </summary>
        public override void Log(string Message, LogLevel Flag ,LogMethod method = LogMethod.ToFileAndConsole)
        {
            string Level = "";
            switch(Flag)  // get the actual Flag that we will use in our log message. We'll also get the color for the message displayed in console logs
            {
                case LogLevel.Fatal:
                    Level = "Fatal";
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Error:
                    Level = "Error";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warn:
                    Level = "Warn";
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.Debug:
                    Level = "Debug";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Info:
                    Level = "Info";
                    break;
            }

            // checks for the Logging method that you chose
            if (method == LogMethod.ToConsole)
            {
                Console.WriteLine($"[{Level}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
            }

            if (method == LogMethod.ToFile)
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(this.LogPath))
                {
                    text.WriteLine($"[{Level}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
                }
            }
            else
            {
                using (System.IO.StreamWriter text = System.IO.File.AppendText(this.LogPath))
                {
                    text.WriteLine($"[{Level}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
                }
                Console.WriteLine($"[{Level}]", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString(), " : ", Message);
            }
        }

        
    }
}
