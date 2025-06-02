using static System.Net.Mime.MediaTypeNames;

namespace TravelBackend.Entities
{
    public class Users
    {

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Nationality { get; set; }
        public string? PreferredLang { get; set; }
        public string? AvatarUrl { get; set; }
        public string UserRole { get; set; } = null!; // traveler, admin, volunteer
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Tour> CreatedTours { get; set; } = new List<Tour>();
        public ICollection<TourReview> Reviews { get; set; } = new List<TourReview>();
        public ICollection<TourBooking> Bookings { get; set; } = new List<TourBooking>();
        public ICollection<UserBehavior> Behaviors { get; set; } = new List<UserBehavior>();
        public ICollection<VolunteerRequest> VolunteerRequests { get; set; } = new List<VolunteerRequest>();
        public ICollection<LawInfo> AddedLaws { get; set; } = new List<LawInfo>();
        public ICollection<Job> PostedJobs { get; set; } = new List<Job>();
        public ICollection<Appli> Applications { get; set; } = new List<Appli>();
        public string? SocialProvider { get; set; } // e.g., "Google", "Facebook"
        public string? SocialId { get; set; }       // The unique ID from the provider
    }
}
