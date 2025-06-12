namespace TravelBackend.Entities
{
    public class VolunteerRequestAssignment
    {
        public int AssignmentId { get; set; }
        public int RequestId { get; set; }
        public int VolunteerUserId { get; set; }
        public string Status { get; set; }
        public DateTime AssignedAt { get; set; }
        public VolunteerRequest Request { get; set; }
        public Users VolunteerUser { get; set; }
    }
}
