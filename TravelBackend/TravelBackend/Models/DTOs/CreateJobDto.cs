namespace TravelBackend.Models.DTOs
{
    public class CreateJobDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? JobType { get; set; }
        public string? RequiredSkills { get; set; }
        public string? Schedule { get; set; }
        public string? Location { get; set; }
        public int PostedBy { get; set; }
        public string? LangCode { get; set; }
    }
}
