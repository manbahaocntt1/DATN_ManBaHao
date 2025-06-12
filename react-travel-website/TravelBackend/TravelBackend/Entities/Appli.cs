namespace TravelBackend.Entities
{
    public class Appli
    {

        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public string? ApplicationNote { get; set; }
        public string Status { get; set; } = null!;
        public DateTime AppliedAt { get; set; }

        // Navigation properties
        public Users User { get; set; } = null!;
        public Job Job { get; set; } = null!;
    }
}
