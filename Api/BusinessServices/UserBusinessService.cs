using System;

namespace Api.BusinessServices
{
    /// <summary>
    /// Offers some informations and actions for the current user
    /// </summary>
    public class UserBusinessService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserBusinessService() { }

        /// <summary>
        /// Returns the name of the current user
        /// </summary>
        public string GetCurrentUserName()
        {
            // TODO: Implement
            return "DummyUser";
        }


        /// <summary>
        /// Returns the id of the current user
        /// </summary>
        public long GetCurrentUserId()
        {
            // TODO: Implement
            return 1;
        }
    }
}
