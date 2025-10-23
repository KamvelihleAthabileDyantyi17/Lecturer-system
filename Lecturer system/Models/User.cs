using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecturer_system.Models
{
    internal class User
    {
        public string Email { get; internal set; }

        public class Approval
        {
            public int ApprovalId { get; set; } // Primary Key
            public string ApprovalStatus { get; set; } // "Approved" or "Rejected"
            public DateTime ApprovalDate { get; set; }
            public string? Notes { get; set; } // Nullable string for optional notes

            // Foreign Key relationship to Claim
            public int ClaimId { get; set; }
            public virtual Claim Claim { get; set; }

            // Foreign Key relationship to User (who approved/rejected)
            public int ApprovedByUserId { get; set; }
            public virtual User ApprovedByUser { get; set; }
        }
    }
};
