using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class BloodIssue
    {
        public int BloodIssueId { get; set; }

        [Required]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        public string BloodGroup { get; set; } = string.Empty;

        [Required]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        public int UnitsIssued { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        public string Status { get; set; } = "Issued";
    }
}