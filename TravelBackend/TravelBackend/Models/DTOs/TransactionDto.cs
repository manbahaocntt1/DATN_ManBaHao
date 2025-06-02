namespace TravelBackend.Models.DTOs
{
    // DTOs/TransactionDto.cs
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string? TransactionRef { get; set; }
        public DateTime PaymentTime { get; set; }
        public string? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
    }

}
