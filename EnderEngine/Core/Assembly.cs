using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine.Core
{
    internal static class Assembly
    {

        private static bool assemblyInitialized = false;

        /// <summary>
        /// Initializes the whole assembly, call once at the start of your program. Returns true if was already initialized
        /// </summary>
        /// <returns> Returns true if the assembly was already initialized </returns>
        internal static bool Init()
        {
            if (assemblyInitialized) // Checks and ensures the Init is run only once
            {
                Engine.engineLogger.Log("Cannot initialize the assembly more than 1 time, skipping initialization", Logger.LogLevel.WARN);
                return true;
            }

            //Init code/logic, etc...
            
            assemblyInitialized = true;
            return false;
        }

    }
}
