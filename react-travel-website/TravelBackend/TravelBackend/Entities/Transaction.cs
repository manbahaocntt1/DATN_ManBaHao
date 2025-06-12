namespace TravelBackend.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!; // pending, success, failed, refunded, etc.
        public string? TransactionRef { get; set; } // Payment gateway transaction ID
        public DateTime PaymentTime { get; set; }
        public string? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public string? RawResponse { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public TourBooking Booking { get; set; } = null!;
    }
}
