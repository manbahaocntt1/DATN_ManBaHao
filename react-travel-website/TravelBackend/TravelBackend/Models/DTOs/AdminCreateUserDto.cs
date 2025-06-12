using System.ComponentModel.DataAnnotations;

namespace TravelBackend.Models.DTOs
{
    public class AdminCreateUserDto
    {
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Password { get; set; }

        public string? Nationality { get; set; }
        public string? PreferredLang { get; set; }
        public string? AvatarUrl { get; set; }

        [Required]
        [StringLength(50)]
        public required string UserRole { get; set; } // "admin", "volunteer", "traveler"
    }
}
