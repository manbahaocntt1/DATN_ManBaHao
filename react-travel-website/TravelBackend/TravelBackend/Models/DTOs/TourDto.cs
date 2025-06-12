namespace TravelBackend.Models.DTOs
{
    public class SearchTourDto
    {
        public string? Keyword { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? DurationDays { get; set; }
        public string? LangCode { get; set; }
    }

    public class CreateTourDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal PriceAdult { get; set; }
        public decimal PriceChild { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public int DurationDays { get; set; }
        public int CreatedBy { get; set; }
        public string? LangCode { get; set; }
    }

    public class TourDto
    {
        public int TourId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public decimal PriceAdult { get; set; }
        public decimal PriceChild { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public int DurationDays { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TourImageDto> Images { get; set; } = new List<TourImageDto>();
        public List<TourReviewDto> Reviews { get; set; } = new();
        public List<TourAvailabilityDto> Availabilities { get; set; } = new();
        public List<PlaceDto> Places { get; set; } = new();
    }

    public class TourImageDto
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? Caption { get; set; }
    }
    public class TourReviewDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserFullName { get; set; }
        public string? UserAvatarUrl { get; set; }
    }

    public class TourAvailabilityDto
    {
        public int AvailabilityId { get; set; }
        public DateTime TourDate { get; set; }
        public int TotalSlots { get; set; }
        public int AvailableSlots { get; set; }
    }

    public class PlaceDto
    {
        public int PlaceId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public string? Address { get; set; }
    }

}