using System;

namespace Lecturer_system.Models
{
    public class User
    {
        public int UserId { get; set; }

        // Making these nullable (?) stops the "Non-nullable property" warning
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }

        // THIS FIXES THE "User.Name" ERROR
        // It automatically combines First and Last name for the DataGrid
        public string Name => $"{FirstName} {LastName}";
    }
}