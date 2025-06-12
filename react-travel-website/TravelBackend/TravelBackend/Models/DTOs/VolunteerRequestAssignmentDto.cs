namespace TravelBackend.Models.DTOs
{
    public class VolunteerRequestAssignmentDto
    {
         public int AssignmentId { get; set; }
    public int VolunteerUserId { get; set; }
    public string Status { get; set; }
    public DateTime AssignedAt { get; set; }
    public string VolunteerFullName { get; set; }
        public int RequestId { get; set; }
    }
}
