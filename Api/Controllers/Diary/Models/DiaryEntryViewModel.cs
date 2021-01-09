using System;

namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// The view model for diary entries
    /// </summary>
    public class DiaryEntryViewModel
    {
        /// <summary>
        /// When the described event happened
        /// </summary>
        public DateTime EventAt { get; set; }

        /// <summary>
        /// What happened at this event
        /// </summary>
        public string Description { get; set; }
    }
}
