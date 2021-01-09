using System.ComponentModel.DataAnnotations;

namespace Api.Database.Models
{
    /// <summary>
    /// A user in the system
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// The suername of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
