using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class Donor
    {
        public int DonorId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string BloodGroup { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}