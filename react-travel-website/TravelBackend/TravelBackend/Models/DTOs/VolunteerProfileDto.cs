namespace TravelBackend.Models.DTOs
{
    public class VolunteerProfileDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } // from Users
        public string University { get; set; }
        public string Major { get; set; }
        public string Languages { get; set; }
        public string ContactInfo { get; set; }
        public bool IsApproved { get; set; }
    }
}
