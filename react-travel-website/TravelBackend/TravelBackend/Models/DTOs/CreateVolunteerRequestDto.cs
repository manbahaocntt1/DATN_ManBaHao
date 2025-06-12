namespace TravelBackend.Models.DTOs
{
    public class CreateVolunteerRequestDto
    {
        public int UserId { get; set; } // For dev/testing only!
        public string Content { get; set; }
        public DateTime? PreferredDate { get; set; }
        public string RequiredLanguages { get; set; }
    }
}
