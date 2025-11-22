using System;
// ... other usings

namespace Lecturer_system.Models
{
    public class User
    {
        public int UserId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // e.g., "Lecturer", "Manager"
    }

    // DELETE THE APPROVAL CLASS THAT WAS HERE
}