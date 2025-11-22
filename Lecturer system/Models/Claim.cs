using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecturer_system.Models
{
    public class Claim
    {
        public int ClaimId { get; set; } // Primary Key
        public DateTime ClaimPeriod { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // "Pending", "Approved", "Rejected"
        public DateTime DateSubmitted { get; set; }
        public string? SupportingDocPath { get; set; } // Nullable string for optional doc

        // Foreign Key relationship to User
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}