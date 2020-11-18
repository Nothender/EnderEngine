using System;
using EnderEngine;

namespace EnderEngineExampleApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Logger logg = new Logger();
            for (int i = 0; i < 42000; i++)
            {
                logg.Log("42", Logger.LogLevel.FATAL);
                logg.Log("42", Logger.LogLevel.WARN);
                logg.Log("42", Logger.LogLevel.INFO);
                Logger.DoWriteDateAndTime = false;
                
                logg.Log("42", Logger.LogLevel.DEBUG);
                logg.Log("42", Logger.LogLevel.ERROR);
                logg.Log("42", Logger.LogLevel.FATAL);
            }
        }
    }
}
