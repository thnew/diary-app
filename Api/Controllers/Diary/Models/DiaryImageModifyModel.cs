namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// Model to modify a diary image
    /// </summary>
    public class DiaryImageModifyModel
    {
        /// <summary>
        /// The id of the image to modify
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
