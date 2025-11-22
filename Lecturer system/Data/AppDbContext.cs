using Microsoft.EntityFrameworkCore;
using Lecturer_system.Models;

namespace Lecturer_system.Data
{
    public class AppDbContext : DbContext
    {
        // Database Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        // Method to configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=LabVM1846780\\SQLEXPRESS;Database=LecturerSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        // Method to configure model relationships (like cascade delete)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call the base method

            // Configure the relationship between Approval and the approving User
            modelBuilder.Entity<Approval>()
                .HasOne(a => a.ApprovedByUser)     // An Approval has one approving User
                .WithMany()                      // A User can approve many Approvals (no navigation property back needed)
                .HasForeignKey(a => a.ApprovedByUserId) // Foreign key is ApprovedByUserId
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete on this path
        }
    }
}