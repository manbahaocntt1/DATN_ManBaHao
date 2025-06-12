namespace TravelBackend.Entities
{
    public class VolunteerRequest
    {

        public int RequestId { get; set; }
        public int UserId { get; set; } // Traveler
        public string Content { get; set; }
        public DateTime? PreferredDate { get; set; }
        public string RequiredLanguages { get; set; } // E.g., "en,vi"
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public Users User { get; set; }
        public ICollection<VolunteerRequestAssignment> Assignments { get; set; }
    }
}
