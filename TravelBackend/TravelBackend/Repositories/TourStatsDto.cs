namespace TravelBackend.Repositories
{
    public class TourStatsDto
    {
        public int TourId { get; set; }
        public int TotalBookings { get; set; }
        public double AverageRating { get; set; }
        public int TotalViews { get; set; }
    }
}
