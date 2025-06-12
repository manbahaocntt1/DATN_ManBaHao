namespace TravelBackend.Models.DTOs
{
    public class VolunteerRequestDto
    {
        public int RequestId { get; set; }
        public string Content { get; set; }
        public DateTime? PreferredDate { get; set; }
        public string RequiredLanguages { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public List<VolunteerRequestAssignmentDto> Assignments { get; set; }
    }
}
