namespace TravelBackend.Models.DTOs
{
    public class MomoIpnDto
    {
        public string OrderId { get; set; }
        public string ResultCode { get; set; }
        public string Message { get; set; }
        public string RawJson { get; set; }
        // Map other fields as needed
    }

}
