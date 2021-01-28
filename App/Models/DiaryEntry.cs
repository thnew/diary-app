using System;

namespace App.Models
{
    public class DiaryEntry
    {
        /// <summary>
        /// The Id of the entity
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// When the described event happened
        /// </summary>
        public DateTime EventAt { get; set; }

        /// <summary>
        /// What happened at this event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether this entry is archived
        /// </summary>
        public bool IsArchived { get; set; }
    }
}