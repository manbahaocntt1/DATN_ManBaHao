namespace TravelBackend.Entities
{
    public class Place
    {
        public int PlaceId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Address { get; set; }
        public TimeSpan? OpenTime { get; set; }
        public TimeSpan? CloseTime { get; set; }
        public string? ImageUrl { get; set; }
        public required string LangCode { get; set; }

        // Navigation property
        public ICollection<TourPlace> TourPlaces { get; set; } = new List<TourPlace>();
    }
}
