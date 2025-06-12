namespace TravelBackend.Models.DTOs
{
    public class HomeTourDto
    {
        public int TourId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public decimal PriceAdult { get; set; }
        public decimal PriceChild { get; set; }
        public int DurationDays { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ImageUrl { get; set; }      // First image
        public double AverageRating { get; set; }  // Calculated
        public int TotalReviews { get; set; }      // Optional
    }

}
