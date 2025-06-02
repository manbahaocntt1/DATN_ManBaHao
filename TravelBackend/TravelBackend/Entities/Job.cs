using static System.Net.Mime.MediaTypeNames;

namespace TravelBackend.Entities
{
    public class Job
    {
        public int JobId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? JobType { get; set; }
        public string? RequiredSkills { get; set; }
        public string? Schedule { get; set; }
        public DateTime PostedDate { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
        public int PostedBy { get; set; }
        public required string LangCode { get; set; }

        // Navigation properties
        public Users Poster { get; set; } = null!;
        public ICollection<Appli> Applications { get; set; } = new List<Appli>();
    }
}
