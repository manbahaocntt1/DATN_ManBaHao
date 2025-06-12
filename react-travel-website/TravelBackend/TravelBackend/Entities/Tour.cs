namespace TravelBackend.Entities
{
    public class Tour
    {
        public int TourId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal PriceAdult { get; set; }
        public decimal PriceChild { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public int DurationDays { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string LangCode { get; set; }

        // Navigation properties
        public Users Creator { get; set; } = null!;
        public ICollection<TourImage> Images { get; set; } = new List<TourImage>();
        public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
        public ICollection<TourAvailability> Availabilities { get; set; } = new List<TourAvailability>();
        public ICollection<TourBooking> Bookings { get; set; } = new List<TourBooking>();
        public ICollection<TourPlace> TourPlaces { get; set; } = new List<TourPlace>();
    }
}
