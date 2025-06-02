namespace TravelBackend.Entities
{
    public class LawInfo
    {
        public int LawId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AddedBy { get; set; }
        public required string LangCode { get; set; }

        // Navigation property
        public Users AddedByUser { get; set; } = null!;
    }
}
