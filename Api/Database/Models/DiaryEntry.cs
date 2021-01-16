using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Models
{
    /// <summary>
    /// An entry in the diary
    /// </summary>
    public class DiaryEntry : BaseEntity
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
        /// The user this entry belongs to
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        /// <summary>
        /// Foreign Key for User
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// The connected diary images
        /// </summary>
        [InverseProperty(nameof(DiaryImage.DiaryEntry))]
        public ICollection<DiaryImage> DiaryImages { get; set; }
    }
}
