using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelBackend.Entities
{
    public class VolunteerProfile
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string University { get; set; }
        public string Major { get; set; }
        public string Languages { get; set; } // E.g., "en,vi"
        public string ContactInfo { get; set; }
        public bool IsApproved { get; set; }
        public Users User { get; set; }
    }
}
