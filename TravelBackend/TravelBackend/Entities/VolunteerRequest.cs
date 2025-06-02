namespace TravelBackend.Entities
{
    public class VolunteerRequest
    {

        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? PreferredDate { get; set; }
        public string Status { get; set; } = null!;

        // Navigation property
        public Users User { get; set; } = null!;
    }
}
