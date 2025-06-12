namespace TravelBackend.Entities
{
    public class TourPlace
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int PlaceId { get; set; }

        // Navigation properties
        public Tour Tour { get; set; } = null!;
        public Place Place { get; set; } = null!;
    }
}
