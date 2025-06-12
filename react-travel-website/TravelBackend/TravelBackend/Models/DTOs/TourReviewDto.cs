namespace TravelBackend.Models.DTOs
{
    public class CreateReviewDto
    {
        public int TourId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ModerateReviewDto
    {
        public int ReviewId { get; set; }
        public bool IsApproved { get; set; }
    }
    public class TourReviewsDto
    {
        public int ReviewId { get; set; }
        public int TourId { get; set; }
        public int UserId { get; set; }
        public required string UserFullName { get; set; }
        public string? UserAvatarUrl { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}