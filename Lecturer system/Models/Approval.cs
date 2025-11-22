using System.ComponentModel.DataAnnotations;
using System;

namespace Lecturer_system.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string? Notes { get; set; }

        // Foreign Key relationship to Claim
        public int ClaimId { get; set; }
        public virtual Claim Claim { get; set; }

        // Foreign Key relationship to User (who approved/rejected)
        public int ApprovedByUserId { get; set; }
        public virtual User ApprovedByUser { get; set; }
    }
}