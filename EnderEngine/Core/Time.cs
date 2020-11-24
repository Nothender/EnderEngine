using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine.Core
{
    public static class Time
    {
        /// <summary>
        /// Stores the DateTime struct, taken at the engine static init
        /// </summary>
        private static DateTime engineStart;

        /// <summary>
        /// Returns the total number of microseconds since the start
        /// </summary>
        public static double SecondsElapsedSinceStart 
        {
            get { return (DateTime.Now - engineStart).TotalSeconds; }
        }

        /// <summary>
        /// Initializes the class at the init of the engine assembly
        /// </summary>
        internal static void Init() //Add to start event instead of calling it from StaticAssemblyInit when event system is added
        {
            engineStart = DateTime.Now;
        }

    }
}
