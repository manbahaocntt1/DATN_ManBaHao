using System.ComponentModel.DataAnnotations;

namespace TravelBackend.Models.DTOs
{
    public class RegisterDto
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
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }

    public class SocialLoginDto
    {
        [Required]
        public required string Provider { get; set; }

        [Required]
        public required string SocialId { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? AvatarUrl { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string NewPassword { get; set; }
    }

    public class ChangePasswordDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string NewPassword { get; set; }
    }

    public class UpdateProfileDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }

        public string? Nationality { get; set; }
        public string? PreferredLang { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
