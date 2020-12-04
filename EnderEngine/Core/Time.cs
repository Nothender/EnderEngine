using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnderEngine.Core;

namespace EnderEngine.Core
{
    
    public static class Time
    {
        /// <summary>
        /// Stores the DateTime struct, taken at the engine static init
        /// </summary>
        private static DateTime engineStart = DateTime.Now;

        /// <summary>
        /// Returns the total number of microseconds since the start
        /// </summary>
        public static double SecondsElapsedSinceStart 
        {
            get { return (DateTime.Now - engineStart).TotalSeconds; }
        }

    }
}
