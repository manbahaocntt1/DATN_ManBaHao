namespace TravelBackend.Models.DTOs
{
    public class CreateApplicationDto

    {
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string? ApplicationNote { get; set; }
    }
}
