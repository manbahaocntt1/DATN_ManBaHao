namespace TravelBackend.Models.DTOs
{
    public class MomoCreateResponseDto
    {
        public string PayUrl { get; set; }
        public string QrCodeUrl { get; set; } // optional
        public string Message { get; set; }
    }


}
