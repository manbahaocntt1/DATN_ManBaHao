namespace TravelBackend.Models.DTOs
{
    public class CreateBookingDto
    {
        public int UserId { get; set; }
        public int TourId { get; set; }
        public DateTime TourDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string? Note { get; set; }

        public required string PaymentMethod { get; set; }

    }
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int TourId { get; set; }
        public string TourTitle { get; set; } = "";
        public string? TourImage { get; set; }
        public DateTime TourDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}

