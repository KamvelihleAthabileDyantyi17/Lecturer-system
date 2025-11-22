using System;

namespace Lecturer_system.Models
{
    public static class AppSession
    {
        // This static variable holds the logged-in user's info
        // so we can access it from ANY page in the app.
        public static User CurrentUser { get; set; }
    }
}