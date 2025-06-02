using TravelBackend.Entities;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        // Search tours with filters (keyword, location, type, price, duration, etc.)
        Task<IEnumerable<Tour>> SearchToursAsync(
            string? keyword = null,
            string? location = null,
            string? type = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? durationDays = null,
            string? lang = null); // Added lang

        // Get full tour details (with images, reviews, availability, etc.)
        Task<Tour?> GetTourDetailsAsync(int tourId);

        // Personalized tour suggestions for a user
        Task<IEnumerable<Tour>> GetPersonalizedToursAsync(int userId, int maxResults = 10, string? lang = null); // Added lang

        // Admin: add, update, delete tours (inherited from IRepository)

        // Admin: get statistics for a tour (bookings, average rating, etc.)
        Task<TourStatsDto> GetTourStatsAsync(int tourId);

        // Admin: get all tours, optionally filter by active status
        Task<IEnumerable<Tour>> GetAllToursAsync(bool? isActive = null, string? lang = null); // Added lang

        // Admin: activate/deactivate a tour
        Task ActivateTourAsync(int tourId);
        Task DeactivateTourAsync(int tourId);
        Task<PagedResult<Tour>> GetAllToursPagedAsync(bool? isActive, string? lang, int page, int pageSize);
         Task<PagedResult<Tour>> SearchToursPagedAsync(
   string? keyword, string? location, string? type, decimal? minPrice, decimal? maxPrice, int? durationDays, string? lang, int page, int pageSize);
    }
}
