namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// The view model for diary images
    /// </summary>
    public class DiaryImageViewModel
    {
        /// <summary>
        /// The id of the entity
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The name of the image file
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// The image file
        /// </summary>
        public byte[] ImageFile { get; set; }
    }
}
