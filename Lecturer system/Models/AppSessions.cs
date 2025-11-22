using Lecturer_system.Models;

namespace Lecturer_system.Models
{
    public static class AppSession
    {
        /// <summary>
        /// Stores the currently logged-in user's data.
        /// Will be 'null' if no one is logged in.
        /// </summary>
        public static User? CurrentUser { get; set; }
    }
}