using EnderEngine.Core;
using System;

namespace EnderEngine
{
    public class Engine
    {

        #region StaticGlobalEngineCode
        private static bool assemblyInitialized = false;
        internal static Logger engineLogger = new Logger("EnderEngineAssembly");

        /// <summary>
        /// Initializes the whole assembly, call once at the start of your program
        /// </summary>
        public static void Init()
        {
            if (assemblyInitialized) // Checks and ensures the Init is run only once
            {
                engineLogger.Log("Cannot initialize the assembly more than 1 time, skipping initialization", Logger.LogLevel.WARN);
                return;
            }
            assemblyInitialized = true;

            engineLogger.Log("Log files can be found in $\"{ExecutionDir}/Logs/\"", Logger.LogLevel.INFO, Logger.LogMethod.TO_CONSOLE);
        }
        #endregion StaticCode

        public readonly Logger logger;
        public readonly int Id;

        /// <summary>
        /// Constructor of the engine
        /// </summary>
        /// <param name="id">
        /// The Id is to help differenciate the engines if you have multiple instances.
        /// If the Id is set -1, the engine will set it to 0 and will act as if it is the only instance (you can still have multiple engine instances without declaring IDs).
        /// If the value is negative it will be changed to its absolute value
        /// </param>
        public Engine(int id = -1)
        {
            //Constructor values
            Id = Math.Abs(id);
            //Construction logic
            if (id == -1)
            {
                Id = 0;
                logger = new Logger("EnderEngine");
            }
            else
                logger = new Logger("EnderEngine-" + Id);
        }

    }
}
