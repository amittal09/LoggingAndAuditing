using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestas.Logging
{
    public enum LogSeverity
    {
        /// <summary>
        /// designates fine-grained informational events that are most useful to debug an application.
        /// </summary>
        Debug,

        /// <summary>
        /// designates informational messages that highlight the progress of the application at coarse-grained level.
        /// </summary>
        Info,

        /// <summary>
        /// designates potentially harmful situations.
        /// </summary>
        Warn,

        /// <summary>
        /// designates error events that might still allow the application to continue running.
        /// </summary>
        Error,

        /// <summary>
        /// designates very severe error events that will presumably lead the application to abort.
        /// </summary>
        Fatal
    }
}
