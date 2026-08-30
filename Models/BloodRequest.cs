using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class BloodRequest
    {
        public int BloodRequestId { get; set; }

        [Required]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        public string BloodGroup { get; set; } = string.Empty;

        [Required]
        public string HospitalName { get; set; } = string.Empty;

        [Required]
        public string ContactNumber { get; set; } = string.Empty;

        public int UnitsRequired { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } = "Pending";
    }
}