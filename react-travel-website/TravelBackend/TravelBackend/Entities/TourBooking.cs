namespace TravelBackend.Entities
{
    public class TourBooking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime TourDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? Note { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Users User { get; set; } = null!;
        public Tour Tour { get; set; } = null!;
    }
}
