namespace Api.Controllers.Diary.Models
{
    /// <summary>
    /// Model to create a new new diary image
    /// </summary>
    public class DiaryImageCreateModel
    {
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
