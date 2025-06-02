namespace TravelBackend.Entities
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }
        public string FullName { get; set; } = null!;
        public string? University { get; set; }
        public string? Major { get; set; }
        public string? Languages { get; set; }
        public string? ContactInfo { get; set; }
        public bool IsApproved { get; set; }
    }
}
