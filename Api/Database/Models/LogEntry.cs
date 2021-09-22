using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models
{
    /// <summary>
    /// A log entry
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// The Id
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// The name of the machine this was logged on
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// The name of the logger
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// When the event happened
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The level of the event
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If an exception happened, this is the exception message
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// If an exception happened, this is the stracktrace for it
        /// </summary>
        public string Stacktrace { get; set; }
    }
}
