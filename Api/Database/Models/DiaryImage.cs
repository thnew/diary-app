using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Database.Models
{
    /// <summary>
    /// An image connected to a diary entry
    /// </summary>
    public class DiaryImage : BaseEntity
    {
        /// <summary>
        /// The name of the image file
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// The image file
        /// </summary>
        public byte[] ImageFile { get; set; }

        /// <summary>
        /// The diary entry this kimage is connected to
        /// </summary>
        [ForeignKey(nameof(DiaryEntryId))]
        public DiaryEntry DiaryEntry { get; set; }

        /// <summary>
        /// Teh foreign key for the DiaryEntry
        /// </summary>
        public long DiaryEntryId { get; set; }
    }
}
