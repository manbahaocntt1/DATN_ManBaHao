namespace TravelBackend.Models.DTOs
{
    public class MomoCreateRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        public string RedirectUrl { get; set; }
        public string IpnUrl { get; set; }
    }
   

}
