using Api.Database.Models;

namespace Api.BusinessServices
{
    /// <summary>
    /// Offers some informations and actions for the current user
    /// </summary>
    public class UserBusinessService
    {
        /// <summary>
        /// The name of the current user
        /// </summary>
        public long CurrentUserId { get; private set; } = 1;

        /// <summary>
        /// The name the current user
        /// </summary>
        public string CurrentUserName { get; private set; } = "DummyUser";

        /// <summary>
        /// Constructor
        /// </summary>
        public UserBusinessService() { }

        /// <summary>
        /// Sets the properties for the current user
        /// </summary>
        public void SetCurrentUser(User currentUser)
        {
            CurrentUserId = currentUser.Id;
            CurrentUserName = currentUser.UserName;
        }
    }
}
