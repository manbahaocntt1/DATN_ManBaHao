namespace TravelBackend.Entities
{
    public class UserBehavior
    {
        public int BehaviorId { get; set; }
        public int UserId { get; set; }
        public string ActionType { get; set; } = null!;
        public string? ActionDetail { get; set; }
        public DateTime ActionTime { get; set; }

        // Navigation property
        public Users User { get; set; } = null!;
    }
}
