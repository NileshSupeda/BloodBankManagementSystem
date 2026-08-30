using System.ComponentModel.DataAnnotations;

namespace BloodBankManagementSystem.Models
{
    public class BloodUnit : IValidatableObject
    {
        public int BloodUnitId { get; set; }

        [Required]
        public string BloodGroup { get; set; } = string.Empty;

        [Required]
        public string ComponentType { get; set; } = string.Empty;

        [Required]
        public DateTime CollectionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public string Status { get; set; } = "Available";

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (ExpiryDate <= CollectionDate)
            {
                yield return new ValidationResult(
                    "Expiry Date must be after Collection Date.",
                    new[] { nameof(ExpiryDate) }
                );
            }
        }
    }
}