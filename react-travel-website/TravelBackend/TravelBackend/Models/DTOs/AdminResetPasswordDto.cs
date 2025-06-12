namespace TravelBackend.Models.DTOs
{
    public class AdminResetPasswordDto
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
