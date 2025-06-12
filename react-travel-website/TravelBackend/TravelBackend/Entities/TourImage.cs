namespace TravelBackend.Entities
{
    public class TourImage
    {
        public int ImageId { get; set; }
        public int TourId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Caption { get; set; }

        // Navigation property
        public Tour Tour { get; set; } = null!;
    }
}
