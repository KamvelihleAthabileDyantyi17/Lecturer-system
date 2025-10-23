using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lecturer_system.Models;
using Microsoft.EntityFrameworkCore;

namespace Lecturer_system.Data
{
    internal class AppDbContext : DbContext // <-- Inherit from DbContext
    {
        // These properties map your C# models to database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This is where you put your database connection string.
            // Replace "Your_Server_Name" with your actual SQL Server instance name.
            // Replace "Your_Database_Name" with the name you want for your database.
            optionsBuilder.UseSqlServer(
         "Server=LabVM1846780\\SQLEXPRESS;Database=LecturerSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"
   
            );
        }
    }
}