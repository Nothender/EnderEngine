using System;
using System.Collections.Generic;
using System.Text;

namespace EnderEngine.Logging
{

    /// <summary>
    /// How the message will be logged
    /// </summary>
    public enum LogMethod
    {
        TO_CONSOLE = 0,
        TO_FILE_AND_CONSOLE = 1,
        TO_FILE = 2
    }
}
