using System;
using System.Collections.Generic;

namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// The view model for diary entries
    /// </summary>
    public class DiaryEntryViewModel
    {
        /// <summary>
        /// The id of the entity
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

        /// <summary>
        /// Images connected to this entry
        /// </summary>
        public IEnumerable<DiaryImageViewModel> Images { get; set; }
    }
}
