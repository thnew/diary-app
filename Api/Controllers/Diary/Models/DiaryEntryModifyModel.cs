using System;
using System.Collections.Generic;

namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// Model to modify a diary entry
    /// </summary>
    public class DiaryEntryModifyModel
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
        /// Whether the images should be updated
        /// </summary>
        public bool ShouldUpdateImages { get; set; }

        /// <summary>
        /// Images connected to this entry
        /// </summary>
        public IEnumerable<DiaryImageModifyModel> Images { get; set; }
    }
}
