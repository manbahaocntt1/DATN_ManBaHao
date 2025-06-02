namespace TravelBackend.Entities
{
    public class TourAvailability
    {
        public int AvailabilityId { get; set; }
        public int TourId { get; set; }
        public DateTime TourDate { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }

        // Navigation property
        public Tour Tour { get; set; } = null!;
    }
}
