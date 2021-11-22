using System;
using System.Collections.Generic;
using System.Text;

namespace EnderEngine.Logging
{
    /// <summary>
    /// The log level indicating how critical is/the importance of the log
    /// </summary>
    public enum LogLevel
    {
        FATAL = 0,
        ERROR,
        WARN,
        INFO,
        SUBINFO,
        DEBUG
    }
}
