using System;
using System.Collections.Generic;

namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// Model to create a new diary entry
    /// </summary>
    public class DiaryEntryCreateModel
    {
        /// <summary>
        /// When the described event happened
        /// </summary>
        public DateTime EventAt { get; set; }

        /// <summary>
        /// What happened at this event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Images connected to this entry
        /// </summary>
        public IEnumerable<DiaryImageCreateModel> Images { get; set; }
    }
}
