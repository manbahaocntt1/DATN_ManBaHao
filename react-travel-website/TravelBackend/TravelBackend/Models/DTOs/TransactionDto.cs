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
    public class CreateTransactionDto
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public string PaymentMethod { get; set; }
        public string TransactionRef { get; set; }
        public string PaymentStatus { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string RawResponse { get; set; }
    }
    public class CreateMomoPaymentDto
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public string TourDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Note { get; set; }
    }

}
