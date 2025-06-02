namespace TravelBackend.Entities
{
    public class TourReview
    {
        public int ReviewId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Tour Tour { get; set; } = null!;
        public Users User { get; set; } = null!;
        public bool IsApproved { get; set; }
    }
}
